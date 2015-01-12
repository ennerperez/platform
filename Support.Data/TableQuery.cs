//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;

//namespace Support.Data
//{
//   public class TableQuery<T> : BaseTableQuery, IEnumerable<T>, IEnumerable
//    {
//        private class CompileResult
//        {
//            public string CommandText
//            {
//                get;
//                set;
//            }
//            public object Value
//            {
//                get;
//                set;
//            }
//        }
//        private readonly ISQLitePlatform _sqlitePlatform;
//        private bool _deferred;
//        private BaseTableQuery _joinInner;
//        private Expression _joinInnerKeySelector;
//        private BaseTableQuery _joinOuter;
//        private Expression _joinOuterKeySelector;
//        private Expression _joinSelector;
//        private int? _limit;
//        private int? _offset;
//        private List<BaseTableQuery.Ordering> _orderBys;
//        private Expression _selector;
//        private Expression _where;
//        public SQLiteConnection Connection
//        {
//            get;
//            private set;
//        }
//        public TableMapping Table
//        {
//            get;
//            private set;
//        }
//        private TableQuery(ISQLitePlatform platformImplementation, SQLiteConnection conn, TableMapping table)
//        {
//            this._sqlitePlatform = platformImplementation;
//            this.Connection = conn;
//            this.Table = table;
//        }
//        public TableQuery(ISQLitePlatform platformImplementation, SQLiteConnection conn)
//        {
//            this._sqlitePlatform = platformImplementation;
//            this.Connection = conn;
//            this.Table = this.Connection.GetMapping(typeof(T), CreateFlags.None);
//        }
//        public IEnumerator<T> GetEnumerator()
//        {
//            if (!this._deferred)
//            {
//                return this.GenerateCommand("*").ExecuteQuery<T>().GetEnumerator();
//            }
//            return this.GenerateCommand("*").ExecuteDeferredQuery<T>().GetEnumerator();
//        }
//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return this.GetEnumerator();
//        }
//        public TableQuery<U> Clone<U>()
//        {
//            TableQuery<U> tableQuery = new TableQuery<U>(this._sqlitePlatform, this.Connection, this.Table);
//            tableQuery._where = this._where;
//            tableQuery._deferred = this._deferred;
//            if (this._orderBys != null)
//            {
//                tableQuery._orderBys = new List<BaseTableQuery.Ordering>(this._orderBys);
//            }
//            tableQuery._limit = this._limit;
//            tableQuery._offset = this._offset;
//            tableQuery._joinInner = this._joinInner;
//            tableQuery._joinInnerKeySelector = this._joinInnerKeySelector;
//            tableQuery._joinOuter = this._joinOuter;
//            tableQuery._joinOuterKeySelector = this._joinOuterKeySelector;
//            tableQuery._joinSelector = this._joinSelector;
//            tableQuery._selector = this._selector;
//            return tableQuery;
//        }
//        public TableQuery<T> Where(Expression<Func<T, bool>> predExpr)
//        {
//            if (predExpr.NodeType == ExpressionType.Lambda)
//            {
//                Expression body = predExpr.Body;
//                TableQuery<T> expr_17 = this.Clone<T>();
//                expr_17.AddWhere(body);
//                return expr_17;
//            }
//            throw new NotSupportedException("Must be a predicate");
//        }
//        public TableQuery<T> Take(int n)
//        {
//            TableQuery<T> expr_06 = this.Clone<T>();
//            expr_06._limit = new int?(n);
//            return expr_06;
//        }
//        public TableQuery<T> Skip(int n)
//        {
//            TableQuery<T> expr_06 = this.Clone<T>();
//            expr_06._offset = new int?(n);
//            return expr_06;
//        }
//        public T ElementAt(int index)
//        {
//            return this.Skip(index).Take(1).First();
//        }
//        public TableQuery<T> Deferred()
//        {
//            TableQuery<T> expr_06 = this.Clone<T>();
//            expr_06._deferred = true;
//            return expr_06;
//        }
//        public TableQuery<T> OrderBy<TValue>(Expression<Func<T, TValue>> orderExpr)
//        {
//            return this.AddOrderBy<TValue>(orderExpr, true);
//        }
//        public TableQuery<T> OrderByDescending<TValue>(Expression<Func<T, TValue>> orderExpr)
//        {
//            return this.AddOrderBy<TValue>(orderExpr, false);
//        }
//        public TableQuery<T> ThenBy<TValue>(Expression<Func<T, TValue>> orderExpr)
//        {
//            return this.AddOrderBy<TValue>(orderExpr, true);
//        }
//        public TableQuery<T> ThenByDescending<TValue>(Expression<Func<T, TValue>> orderExpr)
//        {
//            return this.AddOrderBy<TValue>(orderExpr, false);
//        }
//        private TableQuery<T> AddOrderBy<TValue>(Expression<Func<T, TValue>> orderExpr, bool asc)
//        {
//            if (orderExpr.NodeType != ExpressionType.Lambda)
//            {
//                throw new NotSupportedException("Must be a predicate");
//            }
//            UnaryExpression unaryExpression = orderExpr.Body as UnaryExpression;
//            MemberExpression memberExpression;
//            if (unaryExpression != null && unaryExpression.NodeType == ExpressionType.Convert)
//            {
//                memberExpression = (unaryExpression.Operand as MemberExpression);
//            }
//            else
//            {
//                memberExpression = (orderExpr.Body as MemberExpression);
//            }
//            if (memberExpression != null && memberExpression.Expression.NodeType == ExpressionType.Parameter)
//            {
//                TableQuery<T> tableQuery = this.Clone<T>();
//                if (tableQuery._orderBys == null)
//                {
//                    tableQuery._orderBys = new List<BaseTableQuery.Ordering>();
//                }
//                tableQuery._orderBys.Add(new BaseTableQuery.Ordering
//                {
//                    ColumnName = this.Table.FindColumnWithPropertyName(memberExpression.Member.Name).Name,
//                    Ascending = asc
//                });
//                return tableQuery;
//            }
//            throw new NotSupportedException("Order By does not support: " + orderExpr);
//        }
//        private void AddWhere(Expression pred)
//        {
//            if (this._where == null)
//            {
//                this._where = pred;
//                return;
//            }
//            this._where = Expression.AndAlso(this._where, pred);
//        }
//        public TableQuery<TResult> Join<TInner, TKey, TResult>(TableQuery<TInner> inner, Expression<Func<T, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<T, TInner, TResult>> resultSelector)
//        {
//            return new TableQuery<TResult>(this._sqlitePlatform, this.Connection, this.Connection.GetMapping(typeof(TResult), CreateFlags.None))
//            {
//                _joinOuter = this,
//                _joinOuterKeySelector = outerKeySelector,
//                _joinInner = inner,
//                _joinInnerKeySelector = innerKeySelector,
//                _joinSelector = resultSelector
//            };
//        }
//        public TableQuery<TResult> Select<TResult>(Expression<Func<T, TResult>> selector)
//        {
//            TableQuery<TResult> expr_06 = this.Clone<TResult>();
//            expr_06._selector = selector;
//            return expr_06;
//        }
//        private SQLiteCommand GenerateCommand(string selectionList)
//        {
//            if (this._joinInner != null && this._joinOuter != null)
//            {
//                throw new NotSupportedException("Joins are not supported.");
//            }
//            string text = string.Concat(new string[]
//            {
//                "select ",
//                selectionList,
//                " from \"",
//                this.Table.TableName,
//                "\""
//            });
//            List<object> list = new List<object>();
//            if (this._where != null)
//            {
//                TableQuery<T>.CompileResult compileResult = this.CompileExpr(this._where, list);
//                text = text + " where " + compileResult.CommandText;
//            }
//            if (this._orderBys != null && this._orderBys.Count > 0)
//            {
//                string arg_C9_0 = ", ";
//                IEnumerable<BaseTableQuery.Ordering> arg_BF_0 = this._orderBys;
//                Func<BaseTableQuery.Ordering, string> arg_BF_1;
//                if ((arg_BF_1 = TableQuery<T>.<>c__DisplayClass0.CS$<>9__CachedAnonymousMethodDelegate2) == null)
//                {
//                    arg_BF_1 = (TableQuery<T>.<>c__DisplayClass0.CS$<>9__CachedAnonymousMethodDelegate2 = new Func<BaseTableQuery.Ordering, string>(TableQuery<T>.<>c__DisplayClass0.CS$<>9__inst.<GenerateCommand>b__1));
//                }
//                string str = string.Join(arg_C9_0, arg_BF_0.Select(arg_BF_1).ToArray<string>());
//                text = text + " order by " + str;
//            }
//            if (this._limit.HasValue)
//            {
//                text = text + " limit " + this._limit.Value;
//            }
//            if (this._offset.HasValue)
//            {
//                if (!this._limit.HasValue)
//                {
//                    text += " limit -1 ";
//                }
//                text = text + " offset " + this._offset.Value;
//            }
//            return this.Connection.CreateCommand(text, list.ToArray());
//        }
//        private TableQuery<T>.CompileResult CompileExpr(Expression expr, List<object> queryArgs)
//        {
//            if (expr == null)
//            {
//                throw new NotSupportedException("Expression is NULL");
//            }
//            if (expr is BinaryExpression)
//            {
//                BinaryExpression binaryExpression = (BinaryExpression)expr;
//                TableQuery<T>.CompileResult compileResult = this.CompileExpr(binaryExpression.Left, queryArgs);
//                TableQuery<T>.CompileResult compileResult2 = this.CompileExpr(binaryExpression.Right, queryArgs);
//                string commandText;
//                if (compileResult.CommandText == "?" && compileResult.Value == null)
//                {
//                    commandText = this.CompileNullBinaryExpression(binaryExpression, compileResult2);
//                }
//                else
//                {
//                    if (compileResult2.CommandText == "?" && compileResult2.Value == null)
//                    {
//                        commandText = this.CompileNullBinaryExpression(binaryExpression, compileResult);
//                    }
//                    else
//                    {
//                        commandText = string.Concat(new string[]
//                        {
//                            "(",
//                            compileResult.CommandText,
//                            " ",
//                            this.GetSqlName(binaryExpression),
//                            " ",
//                            compileResult2.CommandText,
//                            ")"
//                        });
//                    }
//                }
//                return new TableQuery<T>.CompileResult
//                {
//                    CommandText = commandText
//                };
//            }
//            if (expr.NodeType == ExpressionType.Call)
//            {
//                MethodCallExpression methodCallExpression = (MethodCallExpression)expr;
//                TableQuery<T>.CompileResult[] array = new TableQuery<T>.CompileResult[methodCallExpression.Arguments.Count];
//                TableQuery<T>.CompileResult compileResult3 = (methodCallExpression.Object != null) ? this.CompileExpr(methodCallExpression.Object, queryArgs) : null;
//                for (int i = 0; i < array.Length; i++)
//                {
//                    array[i] = this.CompileExpr(methodCallExpression.Arguments[i], queryArgs);
//                }
//                string commandText2;
//                if (methodCallExpression.Method.Name == "Like" && array.Length == 2)
//                {
//                    commandText2 = string.Concat(new string[]
//                    {
//                        "(",
//                        array[0].CommandText,
//                        " like ",
//                        array[1].CommandText,
//                        ")"
//                    });
//                }
//                else
//                {
//                    if (methodCallExpression.Method.Name == "Contains" && array.Length == 2)
//                    {
//                        commandText2 = string.Concat(new string[]
//                        {
//                            "(",
//                            array[1].CommandText,
//                            " in ",
//                            array[0].CommandText,
//                            ")"
//                        });
//                    }
//                    else
//                    {
//                        if (methodCallExpression.Method.Name == "Contains" && array.Length == 1)
//                        {
//                            if (methodCallExpression.Object != null && methodCallExpression.Object.Type == typeof(string))
//                            {
//                                commandText2 = string.Concat(new string[]
//                                {
//                                    "(",
//                                    compileResult3.CommandText,
//                                    " like ('%' || ",
//                                    array[0].CommandText,
//                                    " || '%'))"
//                                });
//                            }
//                            else
//                            {
//                                commandText2 = string.Concat(new string[]
//                                {
//                                    "(",
//                                    array[0].CommandText,
//                                    " in ",
//                                    compileResult3.CommandText,
//                                    ")"
//                                });
//                            }
//                        }
//                        else
//                        {
//                            if (methodCallExpression.Method.Name == "StartsWith" && array.Length == 1)
//                            {
//                                commandText2 = string.Concat(new string[]
//                                {
//                                    "(",
//                                    compileResult3.CommandText,
//                                    " like (",
//                                    array[0].CommandText,
//                                    " || '%'))"
//                                });
//                            }
//                            else
//                            {
//                                if (methodCallExpression.Method.Name == "EndsWith" && array.Length == 1)
//                                {
//                                    commandText2 = string.Concat(new string[]
//                                    {
//                                        "(",
//                                        compileResult3.CommandText,
//                                        " like ('%' || ",
//                                        array[0].CommandText,
//                                        "))"
//                                    });
//                                }
//                                else
//                                {
//                                    if (methodCallExpression.Method.Name == "Equals" && array.Length == 1)
//                                    {
//                                        commandText2 = string.Concat(new string[]
//                                        {
//                                            "(",
//                                            compileResult3.CommandText,
//                                            " = (",
//                                            array[0].CommandText,
//                                            "))"
//                                        });
//                                    }
//                                    else
//                                    {
//                                        if (methodCallExpression.Method.Name == "ToLower")
//                                        {
//                                            commandText2 = "(lower(" + compileResult3.CommandText + "))";
//                                        }
//                                        else
//                                        {
//                                            if (methodCallExpression.Method.Name == "ToUpper")
//                                            {
//                                                commandText2 = "(upper(" + compileResult3.CommandText + "))";
//                                            }
//                                            else
//                                            {
//                                                string arg_4AE_0 = methodCallExpression.Method.Name.ToLower();
//                                                string arg_4AE_1 = "(";
//                                                string arg_4A4_0 = ",";
//                                                IEnumerable<TableQuery<T>.CompileResult> arg_49A_0 = array;
//                                                Func<TableQuery<T>.CompileResult, string> arg_49A_1;
//                                                if ((arg_49A_1 = TableQuery<T>.<>c__DisplayClass0.CS$<>9__CachedAnonymousMethodDelegate4) == null)
//                                                {
//                                                    arg_49A_1 = (TableQuery<T>.<>c__DisplayClass0.CS$<>9__CachedAnonymousMethodDelegate4 = new Func<TableQuery<T>.CompileResult, string>(TableQuery<T>.<>c__DisplayClass0.CS$<>9__inst.<CompileExpr>b__3));
//                                                }
//                                                commandText2 = arg_4AE_0 + arg_4AE_1 + string.Join(arg_4A4_0, arg_49A_0.Select(arg_49A_1).ToArray<string>()) + ")";
//                                            }
//                                        }
//                                    }
//                                }
//                            }
//                        }
//                    }
//                }
//                return new TableQuery<T>.CompileResult
//                {
//                    CommandText = commandText2
//                };
//            }
//            if (expr.NodeType == ExpressionType.Constant)
//            {
//                ConstantExpression constantExpression = (ConstantExpression)expr;
//                queryArgs.Add(constantExpression.Value);
//                return new TableQuery<T>.CompileResult
//                {
//                    CommandText = "?",
//                    Value = constantExpression.Value
//                };
//            }
//            if (expr.NodeType == ExpressionType.Convert)
//            {
//                UnaryExpression unaryExpression = (UnaryExpression)expr;
//                Type type = unaryExpression.Type;
//                TableQuery<T>.CompileResult compileResult4 = this.CompileExpr(unaryExpression.Operand, queryArgs);
//                return new TableQuery<T>.CompileResult
//                {
//                    CommandText = compileResult4.CommandText,
//                    Value = (compileResult4.Value != null) ? this.ConvertTo(compileResult4.Value, type) : null
//                };
//            }
//            if (expr.NodeType != ExpressionType.MemberAccess)
//            {
//                throw new NotSupportedException("Cannot compile: " + expr.NodeType.ToString());
//            }
//            MemberExpression memberExpression = (MemberExpression)expr;
//            if (memberExpression.Expression != null && memberExpression.Expression.NodeType == ExpressionType.Parameter)
//            {
//                string name = this.Table.FindColumnWithPropertyName(memberExpression.Member.Name).Name;
//                return new TableQuery<T>.CompileResult
//                {
//                    CommandText = "\"" + name + "\""
//                };
//            }
//            object obj = null;
//            if (memberExpression.Expression != null)
//            {
//                TableQuery<T>.CompileResult expr_5E2 = this.CompileExpr(memberExpression.Expression, queryArgs);
//                if (expr_5E2.Value == null)
//                {
//                    throw new NotSupportedException("Member access failed to compile expression");
//                }
//                if (expr_5E2.CommandText == "?")
//                {
//                    queryArgs.RemoveAt(queryArgs.Count - 1);
//                }
//                obj = expr_5E2.Value;
//            }
//            object memberValue = this._sqlitePlatform.ReflectionService.GetMemberValue(obj, expr, memberExpression.Member);
//            if (memberValue != null && memberValue is IEnumerable && !(memberValue is string) && !(memberValue is IEnumerable<byte>))
//            {
//                StringBuilder stringBuilder = new StringBuilder();
//                stringBuilder.Append("(");
//                string value = "";
//                foreach (object current in (IEnumerable)memberValue)
//                {
//                    queryArgs.Add(current);
//                    stringBuilder.Append(value);
//                    stringBuilder.Append("?");
//                    value = ",";
//                }
//                stringBuilder.Append(")");
//                return new TableQuery<T>.CompileResult
//                {
//                    CommandText = stringBuilder.ToString(),
//                    Value = memberValue
//                };
//            }
//            queryArgs.Add(memberValue);
//            return new TableQuery<T>.CompileResult
//            {
//                CommandText = "?",
//                Value = memberValue
//            };
//        }
//        private object ConvertTo(object obj, Type t)
//        {
//            Type underlyingType = Nullable.GetUnderlyingType(t);
//            if (underlyingType == null)
//            {
//                return Convert.ChangeType(obj, t, CultureInfo.CurrentCulture);
//            }
//            if (obj == null)
//            {
//                return null;
//            }
//            return Convert.ChangeType(obj, underlyingType, CultureInfo.CurrentCulture);
//        }
//        private string CompileNullBinaryExpression(BinaryExpression expression, TableQuery<T>.CompileResult parameter)
//        {
//            if (expression.NodeType == ExpressionType.Equal)
//            {
//                return "(" + parameter.CommandText + " is ?)";
//            }
//            if (expression.NodeType == ExpressionType.NotEqual)
//            {
//                return "(" + parameter.CommandText + " is not ?)";
//            }
//            throw new NotSupportedException("Cannot compile Null-BinaryExpression with type " + expression.NodeType.ToString());
//        }
//        private string GetSqlName(Expression expr)
//        {
//            ExpressionType nodeType = expr.NodeType;
//            if (nodeType == ExpressionType.GreaterThan)
//            {
//                return ">";
//            }
//            if (nodeType == ExpressionType.GreaterThanOrEqual)
//            {
//                return ">=";
//            }
//            if (nodeType == ExpressionType.LessThan)
//            {
//                return "<";
//            }
//            if (nodeType == ExpressionType.LessThanOrEqual)
//            {
//                return "<=";
//            }
//            if (nodeType == ExpressionType.And)
//            {
//                return "&";
//            }
//            if (nodeType == ExpressionType.AndAlso)
//            {
//                return "and";
//            }
//            if (nodeType == ExpressionType.Or)
//            {
//                return "|";
//            }
//            if (nodeType == ExpressionType.OrElse)
//            {
//                return "or";
//            }
//            if (nodeType == ExpressionType.Equal)
//            {
//                return "=";
//            }
//            if (nodeType == ExpressionType.NotEqual)
//            {
//                return "!=";
//            }
//            throw new NotSupportedException("Cannot get SQL for: " + nodeType);
//        }
//        public int Count()
//        {
//            return this.GenerateCommand("count(*)").ExecuteScalar<int>();
//        }
//        public int Count(Expression<Func<T, bool>> predExpr)
//        {
//            return this.Where(predExpr).Count();
//        }
//        public T First()
//        {
//            return this.Take(1).ToList<T>().First<T>();
//        }
//        public T FirstOrDefault()
//        {
//            return this.Take(1).ToList<T>().FirstOrDefault<T>();
//        }
//    }
//}
