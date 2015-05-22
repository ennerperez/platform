using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Platform.Support.Data
{
    public class TableQuery<T> : BaseTableQuery, IEnumerable<T>
    {
        private bool _deferred;

        private BaseTableQuery _joinInner;
        private Expression _joinInnerKeySelector;
        private BaseTableQuery _joinOuter;
        private Expression _joinOuterKeySelector;
        private Expression _joinSelector;
        private int? _limit;
        private int? _offset;
        private List<Ordering> _orderBys;

        private Expression _selector;
        private Expression _where;

        private TableQuery(IDbConnection conn, TableMapping table)
        {
            Connection = conn;
            Table = table;
        }

        public TableQuery(IDbConnection conn)
        {
            Connection = conn;
            Table = Connection.GetMapping(typeof(T));
        }

        public IDbConnection Connection { get; private set; }

        public TableMapping Table { get; private set; }

        public IEnumerator<T> GetEnumerator()
        {
            if (!_deferred)
            {
                return GenerateCommand("*").ExecuteQuery<T>().GetEnumerator();
            }

            return GenerateCommand("*").ExecuteDeferredQuery<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public TableQuery<U> Clone<U>()
        {
            var q = new TableQuery<U>(Connection, Table);
            q._where = _where;
            q._deferred = _deferred;
            if (_orderBys != null)
            {
                q._orderBys = new List<Ordering>(_orderBys);
            }
            q._limit = _limit;
            q._offset = _offset;
            q._joinInner = _joinInner;
            q._joinInnerKeySelector = _joinInnerKeySelector;
            q._joinOuter = _joinOuter;
            q._joinOuterKeySelector = _joinOuterKeySelector;
            q._joinSelector = _joinSelector;
            q._selector = _selector;
            return q;
        }

        public TableQuery<T> Where(Expression<Func<T, bool>> predExpr)
        {
            if (predExpr.NodeType == ExpressionType.Lambda)
            {
                var lambda = (LambdaExpression)predExpr;
                Expression pred = lambda.Body;
                TableQuery<T> q = Clone<T>();
                q.AddWhere(pred);
                return q;
            }
            else
            {
                throw new NotSupportedException("Must be a predicate");
            }
        }

        public TableQuery<T> Take(int n)
        {
            TableQuery<T> q = Clone<T>();
            q._limit = n;
            return q;
        }

        public TableQuery<T> Skip(int n)
        {
            TableQuery<T> q = Clone<T>();
            q._offset = n;
            return q;
        }

        public T ElementAt(int index)
        {
            return Skip(index).Take(1).First();
        }

        public TableQuery<T> Deferred()
        {
            TableQuery<T> q = Clone<T>();
            q._deferred = true;
            return q;
        }

        public TableQuery<T> OrderBy<TValue>(Expression<Func<T, TValue>> orderExpr)
        {
            return AddOrderBy(orderExpr, true);
        }

        public TableQuery<T> OrderByDescending<TValue>(Expression<Func<T, TValue>> orderExpr)
        {
            return AddOrderBy(orderExpr, false);
        }

        public TableQuery<T> ThenBy<TValue>(Expression<Func<T, TValue>> orderExpr)
        {
            return AddOrderBy<TValue>(orderExpr, true);
        }

        public TableQuery<T> ThenByDescending<TValue>(Expression<Func<T, TValue>> orderExpr)
        {
            return AddOrderBy<TValue>(orderExpr, false);
        }

        private TableQuery<T> AddOrderBy<TValue>(Expression<Func<T, TValue>> orderExpr, bool asc)
        {
            if (orderExpr.NodeType == ExpressionType.Lambda)
            {
                var lambda = (LambdaExpression)orderExpr;

                MemberExpression mem = null;

                var unary = lambda.Body as UnaryExpression;
                if (unary != null && unary.NodeType == ExpressionType.Convert)
                {
                    mem = unary.Operand as MemberExpression;
                }
                else
                {
                    mem = lambda.Body as MemberExpression;
                }

                if (mem != null && (mem.Expression.NodeType == ExpressionType.Parameter))
                {
                    TableQuery<T> q = Clone<T>();
                    if (q._orderBys == null)
                    {
                        q._orderBys = new List<Ordering>();
                    }
                    q._orderBys.Add(new Ordering
                    {
                        ColumnName = Table.FindColumnWithPropertyName(mem.Member.Name).Name,
                        Ascending = asc
                    });
                    return q;
                }
                else
                {
                    throw new NotSupportedException("Order By does not support: " + orderExpr);
                }
            }
            else
            {
                throw new NotSupportedException("Must be a predicate");
            }
        }

        private void AddWhere(Expression pred)
        {
            if (_where == null)
            {
                _where = pred;
            }
            else
            {
                _where = Expression.AndAlso(_where, pred);
            }
        }

        public TableQuery<TResult> Join<TInner, TKey, TResult>(
            TableQuery<TInner> inner,
            Expression<Func<T, TKey>> outerKeySelector,
            Expression<Func<TInner, TKey>> innerKeySelector,
            Expression<Func<T, TInner, TResult>> resultSelector)
        {
            var q = new TableQuery<TResult>(Connection, Connection.GetMapping(typeof(TResult)))
            {
                _joinOuter = this,
                _joinOuterKeySelector = outerKeySelector,
                _joinInner = inner,
                _joinInnerKeySelector = innerKeySelector,
                _joinSelector = resultSelector,
            };
            return q;
        }

        public TableQuery<TResult> Select<TResult>(Expression<Func<T, TResult>> selector)
        {
            TableQuery<TResult> q = Clone<TResult>();
            q._selector = selector;
            return q;
        }

        private IDbCommand GenerateCommand(string selectionList)
        {
            if (_joinInner != null && _joinOuter != null)
            {
                throw new NotSupportedException("Joins are not supported.");
            }
            else
            {
                if (selectionList == "*")
                {
                    TableMapping map = new TableMapping(Connection, typeof(T));
                    selectionList = string.Join(", ", (from item in map.Columns select string.Format("[{0}]", item.Name)).ToArray());
                    if (string.IsNullOrEmpty(selectionList))
                        selectionList = "*";
                }

                string cmdText = "SELECT " + selectionList + " FROM " + Table.TableName + "";
                List<IDbDataParameter> args = new List<IDbDataParameter>();
                if (_where != null)
                {
                    CompileResult w = CompileExpr(_where, args);
                    cmdText += " WHERE " + w.CommandText;
                }
                if ((_orderBys != null) && (_orderBys.Count > 0))
                {
                    string t = string.Join(", ", _orderBys.Select(o => "[" + o.ColumnName + "]" + (o.Ascending ? "" : " DESC")).ToArray());
                    cmdText += " ORDER BY " + t;
                }

                switch (Connection.GetEngine())
                {
                    case Engines.SQLite:
                    case Engines.MySql:
                        if (_limit.HasValue)
                        {
                            cmdText += " LIMIT " + _limit.Value;
                        }
                        if (_offset.HasValue)
                        {
                            if (!_limit.HasValue)
                            {
                                cmdText += " LIMIT -1 ";
                            }
                            cmdText += " OFFSET " + _offset.Value;
                        }
                        break;
                    default:
                        if (_limit.HasValue)
                        {
                            cmdText = cmdText.Substring(0, 6) + " TOP " + _limit.Value + " " + cmdText.Substring(7);
                        }

                        break;
                }

                return Connection.CreateCommand( cmdText, args.ToArray());
            }
        }

        private CompileResult CompileExpr(Expression expr, List<IDbDataParameter> queryArgs)
        {

            if (expr == null)
            {
                throw new NotSupportedException("Expression is NULL");
            }
            else if (expr is BinaryExpression)
            {
                var bin = (BinaryExpression)expr;

                CompileResult leftr = CompileExpr(bin.Left, queryArgs);
                CompileResult rightr = CompileExpr(bin.Right, queryArgs);

                //If either side is a parameter and is null, then handle the other side specially (for "is null"/"is not null")
                string text = string.Empty;
                if (leftr.CommandText == null && leftr.Value == null)
                {
                    text = CompileNullBinaryExpression(bin, rightr);
                }
                else if (rightr.CommandText == null && rightr.Value == null)
                {
                    text = CompileNullBinaryExpression(bin, leftr);
                }
                else if (leftr.Value == null && rightr.Value == null)
                {
                    text = string.Format("({0} {1} {2}) ", leftr.CommandText, GetSqlName(bin), rightr.CommandText);
                }
                else
                {
                    rightr.CommandText = "@" + leftr.CommandText;
                    queryArgs.Add(Connection.CreateParameter(rightr.CommandText, rightr.Value));
                    text = string.Format("([{0}] {1} {2}) ", leftr.CommandText, GetSqlName(bin), rightr.CommandText);
                }

                return new CompileResult(text);
            }
            else if (expr.NodeType == ExpressionType.Call)
            {
                MethodCallExpression call = (MethodCallExpression)expr;
                List<CompileResult> args = new List<CompileResult>();
                CompileResult obj = call.Object != null ? CompileExpr(call.Object, queryArgs) : null;

                foreach (Expression exp in call.Arguments)
                {
                    args.Add(CompileExpr(exp, queryArgs));
                }

                string sqlCall = "";

                if (call.Method.Name.ToString().StartsWith("Like") && args.Count == 3)
                {
                    sqlCall = string.Format("([{0}] LIKE @{1})", args[0].CommandText, args[0].CommandText);
                    queryArgs.Add(Connection.CreateParameter(args[0].CommandText, args[1].Value));

                }
                else if (call.Method.Name == "CONTAINS" && args.Count == 2)
                {
                    sqlCall = string.Format("([{0}] IN ({1}))", args[1].CommandText, args[1].Value);
                }
                else if (call.Method.Name == "CONTAINS" && args.Count == 1)
                {
                    if (call.Object != null && call.Object.Type == typeof(string))
                    {
                        switch (Connection.GetEngine())
                        {
                            case Engines.Sql:
                            case Engines.SqlCE:
                                queryArgs.Add(Connection.CreateParameter(obj.CommandText, args[0].Value));
                                sqlCall = string.Format("(CONTAINS([{0}], @{0}))", obj.CommandText);
                                break;
                            default:
                                sqlCall = string.Format("([{0}] LIKE '%{1}%')", obj.CommandText, args[0].CommandText);
                                break;
                        }
                    }
                    else
                    {
                        sqlCall = string.Format("([{0}] IN '{1}')", args[0].CommandText, obj.CommandText);
                    }

                }
                else if (call.Method.Name == "StartsWith" && args.Count == 1)
                {
                    sqlCall = string.Format("([{0}] LIKE '{1}%')", obj.CommandText, args[0].CommandText);
                }
                else if (call.Method.Name == "EndsWith" && args.Count == 1)
                {
                    sqlCall = string.Format("([{0}] LIKE '%{1}')", obj.CommandText, args[0].CommandText);
                }
                else if (call.Method.Name == "Equals" && args.Count == 1)
                {
                    object val = args[0].Value;
                    queryArgs.Add(Connection.CreateParameter(obj.CommandText, args[0].Value));
                    sqlCall = string.Format("([{0}] = @{0})", obj.CommandText);

                }


                return new CompileResult(sqlCall);
            }
            else if (expr.NodeType == ExpressionType.Constant)
            {
                ConstantExpression c = (ConstantExpression)expr;

                return new CompileResult(null, c.Value);
            }
            else if (expr.NodeType == ExpressionType.Convert)
            {
                UnaryExpression u = (UnaryExpression)expr;
                Type ty = u.Type;
                CompileResult valr = CompileExpr(u.Operand, queryArgs);

                return new CompileResult(valr.CommandText, valr.Value != null ? ConvertTo(valr.Value, ty) : null);
            }
            else if (expr.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression mem = (MemberExpression)expr;

                if (mem.Expression != null && mem.Expression.NodeType == ExpressionType.Parameter)
                {
                    string columnName = Table.FindColumnWithPropertyName(mem.Member.Name).Name;
                    return new CompileResult(columnName);
                }
                else
                {
                    object obj = null;
                    if (mem.Expression != null)
                    {
                        CompileResult r = CompileExpr(mem.Expression, queryArgs);
                        if (r.Value == null)
                            throw new NotSupportedException("Member access failed to compile expression");
                        obj = r.Value;
                    }

                    //Get the member value
                    Support.Reflection.ReflectionService ReflectionService = new Support.Reflection.ReflectionService();
                    object val = ReflectionService.GetMemberValue(obj, expr, mem.Member);

                    //Work special magic for enumerables
                    if (val != null && val is System.Collections.IEnumerable && !(val is string) && !(val is System.Collections.Generic.IEnumerable<byte>))
                    {
                        StringBuilder sb = new StringBuilder();
                        return new CompileResult(sb.ToString(), val);
                    }
                    else
                    {
                        return new CompileResult(null, val);
                    }
                }
            }
            throw new NotSupportedException("Cannot compile: " + expr.NodeType.ToString());
        }

        private object ConvertTo(object obj, Type t)
        {
            Type nut = Nullable.GetUnderlyingType(t);

            if (nut != null)
            {
                if (obj == null)
                {
                    return null;
                }
                return Convert.ChangeType(obj, nut, CultureInfo.CurrentCulture);
            }
            else
            {
                return Convert.ChangeType(obj, t, CultureInfo.CurrentCulture);
            }
        }

        /// <summary>
        ///     Compiles a BinaryExpression where one of the parameters is null.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="parameter">The non-null parameter</param>
        private string CompileNullBinaryExpression(BinaryExpression expression, CompileResult parameter)
        {
            if (expression.NodeType == ExpressionType.Equal)
            {
                return "(" + parameter.CommandText + " IS NULL)";
            }
            else if (expression.NodeType == ExpressionType.NotEqual)
            {
                return "(" + parameter.CommandText + " IS NOT NULL)";
            }
            else
            {
                throw new NotSupportedException("Cannot compile Null-BinaryExpression with type " + expression.NodeType.ToString());
            }
        }

        private string GetSqlName(Expression expr)
        {
            ExpressionType n = expr.NodeType;
            if (n == ExpressionType.GreaterThan)
            {
                return ">";
            }
            else if (n == ExpressionType.GreaterThanOrEqual)
            {
                return ">=";
            }
            else if (n == ExpressionType.LessThan)
            {
                return "<";
            }
            else if (n == ExpressionType.LessThanOrEqual)
            {
                return "<=";
            }
            else if (n == ExpressionType.And)
            {
                //if (Connection.GetEngine() == Engines.Sql || Connection.GetEngine() == Engines.SqlCE)
                //{
                    return "and";
                //}
                //else
                //{
                //    return "&";
                //}
            }
            else if (n == ExpressionType.AndAlso)
            {
                return "and";
            }
            else if (n == ExpressionType.Or)
            {
                //Return "|"
                return "or";
            }
            else if (n == ExpressionType.OrElse)
            {
                return "or";
            }
            else if (n == ExpressionType.Equal)
            {
                return "=";
            }
            else if (n == ExpressionType.NotEqual)
            {
                //Return "!="
                return "<>";
            }
            else
            {
                throw new NotSupportedException("Cannot get SQL for: " + n);
            }
        }

        public int Count()
        {
            return GenerateCommand("COUNT(1)").ExecuteScalar<int>();
        }

        public int Count(Expression<Func<T, bool>> predExpr)
        {
            return Where(predExpr).Count();
        }

        public T First()
        {
            TableQuery<T> query = Take(1);
            return query.ToList<T>().First();
        }

        public T FirstOrDefault()
        {
            TableQuery<T> query = Take(1);
            return query.ToList<T>().FirstOrDefault();
        }

        private class CompileResult
        {
            public string CommandText { get; set; }

            public object Value { get; set; }

            public CompileResult()
            {
            }

            public CompileResult(string c, object v = null)
            {
                CommandText = c;
                Value = v;
            }

        }
    }
}