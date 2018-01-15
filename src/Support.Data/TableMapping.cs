using Platform.Support.Data.Attributes;
using Platform.Support.Reflection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Platform.Support.Data
{
    [Obsolete("Use EF instead")]
    public class TableMapping
    {
        private readonly Column _autoPk;
        private Column[] _insertColumns;

        //private IDbCommand _insertCommand;
        //private string _insertCommandExtra;
        private Column[] _insertOrReplaceColumns;

        public TableMapping(System.Data.IDbConnection conn, Type type, Create createFlags = Create.None, BindingFlags bindinFlags = BindingFlags.Public)
        {
            MappedType = type;

            IEnumerable<CustomAttributeData> _cad = type.GetCustomAttributesData();

            CustomAttributeData tableAttr = _cad.Where(a => object.ReferenceEquals(a.Constructor.DeclaringType, typeof(TableAttribute))).FirstOrDefault();
            CustomAttributeData schemaAttr = _cad.Where(s => object.ReferenceEquals(s.Constructor.DeclaringType, typeof(SchemaAttribute))).FirstOrDefault();

            TableName = tableAttr != null ? Convert.ToString(tableAttr.ConstructorArguments.FirstOrDefault().Value) : MappedType.Name;

            if (conn.GetEngine() == Engine.Sql)
            {
                SchemaName = schemaAttr != null ? Convert.ToString(schemaAttr.ConstructorArguments.FirstOrDefault().Value) : "dbo";
            }

            IEnumerable<PropertyInfo> props;

            switch (bindinFlags)
            {
                case BindingFlags.Default:
                    props = MappedType.GetInstanceProperties();
                    break;

                case BindingFlags.NonPublic:
                    props = MappedType.GetNonPublicInstanceProperties();
                    break;

                case BindingFlags.Static:
                    props = MappedType.GetStaticInstanceProperties();
                    break;

                default:
                    props = MappedType.GetPublicInstanceProperties();
                    break;
            }

            var cols = new List<Column>();
            foreach (PropertyInfo p in props)
            {
                bool ignore = p.GetCustomAttributes(typeof(NotMappedAttribute), true).Any();
                bool include = p.GetCustomAttributes(typeof(ColumnAttribute), true).Any();

                if (p.CanWrite && !ignore && include)
                {
                    cols.Add(new Column(p, createFlags));
                }
            }

            if (cols == null || cols.Count == 0)
            {
                foreach (PropertyInfo p in props.Reverse())
                {
                    if (p.CanWrite)
                    {
                        cols.Add(new Column(p, createFlags));
                    }
                }
            }

            Columns = cols.ToArray();
            foreach (Column c in Columns)
            {
                if (c.IsAutoInc && c.IsPK)
                {
                    _autoPk = c;
                }
                if (c.IsPK)
                {
                    PK = c;
                }
            }

            HasAutoIncPK = _autoPk != null;

            if (PK != null)
            {
                GetByPrimaryKeySql = string.Format("SELECT {0} FROM {1} WHERE {2} = @param1", string.Join(", ", ((List<Column>)cols).Select(c => string.Format("[{0}]", c.Name)).ToArray()), TableName, PK.Name);
            }
            else
            {
                // People should not be calling Get/Find without a PK
                switch (conn.GetEngine())
                {
                    case Engine.SQLite:
                    case Engine.MySql:
                        GetByPrimaryKeySql = string.Format("SELECT {0} FROM {1} LIMIT 1", string.Join(", ", ((List<Column>)cols).Select(c => string.Format("[{0}]", c.Name)).ToArray()), TableName);
                        break;

                    default:
                        GetByPrimaryKeySql = string.Format("SELECT TOP 1 {0} FROM {1} ", string.Join(", ", ((List<Column>)cols).Select(c => string.Format("[{0}]", c.Name)).ToArray()), TableName);
                        break;
                }
            }
        }

        public Type MappedType { get; private set; }

        public string TableName { get; private set; }

        public string GetTableName(bool schema = true)
        {
            if (!string.IsNullOrEmpty(SchemaName) && schema)
            {
                return SchemaName + "." + this.TableName;
            }
            else
            {
                return this.TableName;
            }
        }

        public string SchemaName { get; private set; }

        public Column[] Columns { get; private set; }

        public Column PK { get; private set; }

        public string GetByPrimaryKeySql { get; private set; }

        public bool HasAutoIncPK { get; private set; }

        public Column[] InsertColumns
        {
            get
            {
                if (_insertColumns == null)
                {
                    _insertColumns = Columns.Where(c => !c.IsAutoInc).ToArray();
                }
                return _insertColumns;
            }
        }

        public Column[] InsertOrReplaceColumns
        {
            get
            {
                if (_insertOrReplaceColumns == null)
                {
                    _insertOrReplaceColumns = Columns.ToArray();
                }
                return _insertOrReplaceColumns;
            }
        }

        public void SetAutoIncPK(object obj, object id)
        {
            if (_autoPk != null)
            {
                _autoPk.SetValue(obj, Convert.ChangeType(id, _autoPk.ColumnType, null));
            }
        }

        public Column FindColumnWithPropertyName(string propertyName)
        {
            Column exact = Columns.FirstOrDefault(c => c.PropertyName == propertyName);
            return exact;
        }

        public Column FindColumn(string columnName)
        {
            Column exact = Columns.FirstOrDefault(c => c.Name == columnName);
            return exact;
        }

        public IDbCommand GetInsertCommand(IDbConnection conn, string extra)
        {
            //    if (_insertCommand == null)
            //    {
            //        _insertCommand = CreateInsertCommand(conn, extra);
            //        _insertCommandExtra = extra;
            //    }
            //    else if (_insertCommandExtra != extra)
            //    {
            //        _insertCommand.Dispose();
            //        _insertCommand = CreateInsertCommand(conn, extra);
            //        _insertCommandExtra = extra;
            //    }
            //    return _insertCommand;
            return CreateInsertCommand(conn, extra);
        }

        private IDbCommand CreateInsertCommand(IDbConnection conn, string extra)
        {
            Column[] cols = InsertColumns;
            string insertSql;
            if (!cols.Any() && Columns.Count() == 1 && Columns[0].IsAutoInc)
            {
                insertSql = string.Format("INSERT {1} INTO {0} DEFAULT VALUES", TableName, extra);
            }
            else
            {
                bool replacing = string.Compare(extra, "OR REPLACE", StringComparison.OrdinalIgnoreCase) == 0;

                if (replacing)
                {
                    cols = InsertOrReplaceColumns;
                }

                insertSql = string.Format("INSERT {3} INTO {0} ({1}) VALUES ({2})", TableName,
                    string.Join(",", (from c in cols
                                      select "[" + c.Name + "]").ToArray()),
                    string.Join(",", (from c in cols
                                      select "@" + c.Name).ToArray()), extra);
            }

            //var insertCommand = new PreparedInsertCommand(conn);
            //insertCommand.CommandText = insertSql;
            //return insertCommand;
            return conn.CreateCommand(insertSql, null);
        }

        protected internal void Dispose()
        {
            //if (_insertCommand != null)
            //{
            //    _insertCommand.Dispose();
            //    _insertCommand = null;
            //}
        }

        public class Column
        {
            private readonly PropertyInfo _prop;

            public Column(PropertyInfo prop, Create createFlags = Create.None)
            {
                IEnumerable<ColumnAttribute> attributes = (IEnumerable<ColumnAttribute>)prop.GetCustomAttributes(typeof(ColumnAttribute), true);
                ColumnAttribute colAttr = attributes.FirstOrDefault<ColumnAttribute>();

                _prop = prop;
                Name = colAttr == null ? prop.Name : colAttr.Name;
                //If this type is Nullable<T> then Nullable.GetUnderlyingType returns the T, otherwise it returns null, so get the actual type instead
                ColumnType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                Collation = Orm.Collation(prop);

                IsPK = Orm.IsPK(prop) ||
                (((createFlags & Create.ImplicitPK) == Create.ImplicitPK) &&
                string.Compare(prop.Name, Orm.ImplicitPkName, StringComparison.OrdinalIgnoreCase) == 0);

                bool isAuto = Orm.IsAutoInc(prop) ||
                              (IsPK && ((createFlags & Create.AutoIncPK) == Create.AutoIncPK));
                IsAutoGuid = isAuto && ColumnType == typeof(Guid);
                IsAutoInc = isAuto && !IsAutoGuid;

                DefaultValue = Orm.GetDefaultValue(prop);

                Indices = Orm.GetIndices(prop);
                if (!Indices.Any()
                    && !IsPK
                    && ((createFlags & Create.ImplicitIndex) == Create.ImplicitIndex)
                    && Name.EndsWith(Orm.ImplicitIndexSuffix, StringComparison.OrdinalIgnoreCase))
                {
                    Indices = new[] { new IndexedAttribute() };
                }
                IsNullable = !(IsPK || Orm.IsMarkedNotNull(prop));
                MaxStringLength = Orm.MaxStringLength(prop);
            }

            public string Name { get; private set; }

            public string PropertyName
            {
                get { return _prop.Name; }
            }

            public Type ColumnType { get; private set; }

            public string Collation { get; private set; }

            public bool IsAutoInc { get; private set; }

            public bool IsAutoGuid { get; private set; }

            public bool IsPK { get; private set; }

            public IEnumerable<IndexedAttribute> Indices { get; set; }

            public bool IsNullable { get; private set; }

            public int? MaxStringLength { get; private set; }
            public object DefaultValue { get; private set; }

            /// <summary>
            ///     Set column value.
            /// </summary>
            /// <param name="obj"></param>
            /// <param name="val"></param>
            /// <remarks>
            ///     Copied from: http://code.google.com/p/sqlite-net/issues/detail?id=47
            /// </remarks>
            public void SetValue(object obj, object val)
            {
                Type propType = _prop.PropertyType;
#if DEBUG
                try
                {
#endif
                    if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        Type[] typeCol = propType.GetGenericArguments();
                        if (typeCol.Length > 0)
                        {
                            Type nullableType = typeCol[0];
                            if (object.ReferenceEquals(nullableType.BaseType, typeof(Enum)))
                            {
                                object result = val == null ? null : Enum.Parse(nullableType, val.ToString(), false);
                                _prop.SetValue(obj, result, null);
                            }
                            else
                            {
                                _prop.SetValue(obj, val, null);
                            }
                        }
                    }
                    else if (propType.IsEnum)
                    {
                        _prop.SetValue(obj, Enum.Parse(propType, val.ToString()), null);
                    }
                    else if (val == null || val.GetType() == typeof(System.DBNull))
                    {
                        _prop.SetValue(obj, null, null);
                    }
                    else if (object.ReferenceEquals(propType, typeof(Int32)) & object.ReferenceEquals(val.GetType(), typeof(Int64)))
                    {
                        if (Convert.ToInt32(val) <= Int32.MaxValue)
                        {
                            _prop.SetValue(obj, Convert.ToInt32(val), null);
                        }
                    }
                    else
                    {
                        if (object.ReferenceEquals(propType, typeof(bool)))
                            val = Convert.ToInt32(val) == 1;
                        if (object.ReferenceEquals(propType, typeof(int)))
                            val = Convert.ToInt32(val);
                        //if (object.ReferenceEquals(val.GetType(), typeof(byte[])))
                        //    val = BitConverter.ToInt64((byte[])val, 0);
                        if (object.ReferenceEquals(propType, typeof(TimeSpan)))
                            val = TimeSpan.Parse(val.ToString());

                        if (object.ReferenceEquals(propType, typeof(Guid)))
                            val = Guid.Parse(val.ToString());

                        if (object.ReferenceEquals(propType, typeof(Version)))
                            val = Version.Parse(val.ToString());

                        _prop.SetValue(obj, val, null);
                    }

#if DEBUG
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
#endif
            }

            public object GetValue(object obj)
            {
                Type propType = _prop.PropertyType;

                if (propType.DeclaringType == null && propType.GetGenericArguments().Count() > 0)
                    propType = propType.GetGenericArguments().First();

                var _return = _prop.GetValue(obj, null);
                if (_return != null && (_return.GetType() == typeof(System.DateTime)))
                {
                    if ((System.DateTime)_return < new System.DateTime(1753, 1, 1)) { _return = null; }
                }
                else if (_return != null && propType.IsEnum)
                {
                    _return = Convert.ChangeType(_return, Enum.GetUnderlyingType(propType));
                }
                else if (_return != null && (_return.GetType() == typeof(Guid)))
                {
                    _return = _return.ToString().ToUpper();
                }

                return _return;
            }
        }
    }
}