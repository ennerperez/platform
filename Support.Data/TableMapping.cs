//using Support.Data.Attributes;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;

//namespace Support.Data
//{
//   public class TableMapping
//    {
//        public class Column
//        {
//            private readonly PropertyInfo _prop;
//            public string Name
//            {
//                get;
//                private set;
//            }
//            public string PropertyName
//            {
//                get
//                {
//                    return this._prop.Name;
//                }
//            }
//            public Type ColumnType
//            {
//                get;
//                private set;
//            }
//            public string Collation
//            {
//                get;
//                private set;
//            }
//            public bool IsAutoInc
//            {
//                get;
//                private set;
//            }
//            public bool IsAutoGuid
//            {
//                get;
//                private set;
//            }
//            public bool IsPK
//            {
//                get;
//                private set;
//            }
//            public IEnumerable<IndexedAttribute> Indices
//            {
//                get;
//                set;
//            }
//            public bool IsNullable
//            {
//                get;
//                private set;
//            }
//            public int? MaxStringLength
//            {
//                get;
//                private set;
//            }
//            public object DefaultValue
//            {
//                get;
//                private set;
//            }
//            public Column(PropertyInfo prop, CreateFlags createFlags = CreateFlags.None)
//            {
//                ColumnAttribute columnAttribute = prop.GetCustomAttributes(true).FirstOrDefault<ColumnAttribute>();
//                this._prop = prop;
//                this.Name = ((columnAttribute == null) ? prop.Name : columnAttribute.Name);
//                this.ColumnType = (Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
//                this.Collation = Orm.Collation(prop);
//                this.IsPK = (Orm.IsPK(prop) || ((createFlags & CreateFlags.ImplicitPK) == CreateFlags.ImplicitPK && string.Compare(prop.Name, "Id", StringComparison.OrdinalIgnoreCase) == 0));
//                bool flag = Orm.IsAutoInc(prop) || (this.IsPK && (createFlags & CreateFlags.AutoIncPK) == CreateFlags.AutoIncPK);
//                this.IsAutoGuid = (flag && this.ColumnType == typeof(Guid));
//                this.IsAutoInc = (flag && !this.IsAutoGuid);
//                this.DefaultValue = Orm.GetDefaultValue(prop);
//                this.Indices = Orm.GetIndices(prop);
//                if (!this.Indices.Any<IndexedAttribute>() && !this.IsPK && (createFlags & CreateFlags.ImplicitIndex) == CreateFlags.ImplicitIndex && this.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase))
//                {
//                    this.Indices = new IndexedAttribute[]
//                    {
//                        new IndexedAttribute()
//                    };
//                }
//                this.IsNullable = (!this.IsPK && !Orm.IsMarkedNotNull(prop));
//                this.MaxStringLength = Orm.MaxStringLength(prop);
//            }
//            public void SetValue(object obj, object val)
//            {
//                Type propertyType = this._prop.PropertyType;
//                if (propertyType.GetTypeInfo().IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
//                {
//                    Type[] genericTypeArguments = propertyType.GetTypeInfo().GenericTypeArguments;
//                    if (genericTypeArguments.Length != 0)
//                    {
//                        Type type = genericTypeArguments[0];
//                        if (type.GetTypeInfo().BaseType == typeof(Enum))
//                        {
//                            object value = (val == null) ? null : Enum.Parse(type, val.ToString(), false);
//                            this._prop.SetValue(obj, value, null);
//                            return;
//                        }
//                        this._prop.SetValue(obj, val, null);
//                        return;
//                    }
//                }
//                else
//                {
//                    this._prop.SetValue(obj, val, null);
//                }
//            }
//            public object GetValue(object obj)
//            {
//                return this._prop.GetValue(obj, null);
//            }
//        }
//        private readonly TableMapping.Column _autoPk;
//        private readonly ISQLitePlatform _sqlitePlatform;
//        private TableMapping.Column[] _insertColumns;
//        private PreparedSqlLiteInsertCommand _insertCommand;
//        private string _insertCommandExtra;
//        private TableMapping.Column[] _insertOrReplaceColumns;
//        public Type MappedType
//        {
//            get;
//            private set;
//        }
//        public string TableName
//        {
//            get;
//            private set;
//        }
//        public TableMapping.Column[] Columns
//        {
//            get;
//            private set;
//        }
//        public TableMapping.Column PK
//        {
//            get;
//            private set;
//        }
//        public string GetByPrimaryKeySql
//        {
//            get;
//            private set;
//        }
//        public bool HasAutoIncPK
//        {
//            get;
//            private set;
//        }
//        public TableMapping.Column[] InsertColumns
//        {
//            get
//            {
//                if (this._insertColumns == null)
//                {
//                    IEnumerable<TableMapping.Column> arg_2E_0 = this.Columns;
//                    Func<TableMapping.Column, bool> arg_2E_1;
//                    if ((arg_2E_1 = TableMapping.<>c__DisplayClass0.CS$<>9__CachedAnonymousMethodDelegate4) == null)
//                    {
//                        arg_2E_1 = (TableMapping.<>c__DisplayClass0.CS$<>9__CachedAnonymousMethodDelegate4 = new Func<TableMapping.Column, bool>(TableMapping.<>c__DisplayClass0.CS$<>9__inst.<get_InsertColumns>b__3));
//                    }
//                    this._insertColumns = arg_2E_0.Where(arg_2E_1).ToArray<TableMapping.Column>();
//                }
//                return this._insertColumns;
//            }
//        }
//        public TableMapping.Column[] InsertOrReplaceColumns
//        {
//            get
//            {
//                if (this._insertOrReplaceColumns == null)
//                {
//                    this._insertOrReplaceColumns = this.Columns.ToArray<TableMapping.Column>();
//                }
//                return this._insertOrReplaceColumns;
//            }
//        }
//        public TableMapping(ISQLitePlatform platformImplementation, Type type, CreateFlags createFlags = CreateFlags.None)
//        {
//            this._sqlitePlatform = platformImplementation;
//            this.MappedType = type;
//            IEnumerable<CustomAttributeData> arg_3E_0 = type.GetTypeInfo().CustomAttributes;
//            Func<CustomAttributeData, bool> arg_3E_1;
//            if ((arg_3E_1 = TableMapping.<>c__DisplayClass0.CS$<>9__CachedAnonymousMethodDelegate2) == null)
//            {
//                arg_3E_1 = (TableMapping.<>c__DisplayClass0.CS$<>9__CachedAnonymousMethodDelegate2 = new Func<CustomAttributeData, bool>(TableMapping.<>c__DisplayClass0.CS$<>9__inst.<.ctor>b__1));
//            }
//            CustomAttributeData customAttributeData = arg_3E_0.FirstOrDefault(arg_3E_1);
//            this.TableName = ((customAttributeData != null) ? ((string)customAttributeData.ConstructorArguments.FirstOrDefault<CustomAttributeTypedArgument>().Value) : this.MappedType.Name);
//            IEnumerable<PropertyInfo> arg_8E_0 = this._sqlitePlatform.ReflectionService.GetPublicInstanceProperties(this.MappedType);
//            List<TableMapping.Column> list = new List<TableMapping.Column>();
//            foreach (PropertyInfo current in arg_8E_0)
//            {
//                bool flag = current.GetCustomAttributes(true).Any<IgnoreAttribute>();
//                if (current.CanWrite && !flag)
//                {
//                    list.Add(new TableMapping.Column(current, createFlags));
//                }
//            }
//            this.Columns = list.ToArray();
//            TableMapping.Column[] columns = this.Columns;
//            for (int i = 0; i < columns.Length; i++)
//            {
//                TableMapping.Column column = columns[i];
//                if (column.IsAutoInc && column.IsPK)
//                {
//                    this._autoPk = column;
//                }
//                if (column.IsPK)
//                {
//                    this.PK = column;
//                }
//            }
//            this.HasAutoIncPK = (this._autoPk != null);
//            if (this.PK != null)
//            {
//                this.GetByPrimaryKeySql = string.Format("select * from \"{0}\" where \"{1}\" = ?", new object[]
//                {
//                    this.TableName,
//                    this.PK.Name
//                });
//                return;
//            }
//            this.GetByPrimaryKeySql = string.Format("select * from \"{0}\" limit 1", new object[]
//            {
//                this.TableName
//            });
//        }
//        public void SetAutoIncPK(object obj, long id)
//        {
//            if (this._autoPk != null)
//            {
//                this._autoPk.SetValue(obj, Convert.ChangeType(id, this._autoPk.ColumnType, null));
//            }
//        }
//        public TableMapping.Column FindColumnWithPropertyName(string propertyName)
//        {
//            return this.Columns.FirstOrDefault((TableMapping.Column c) => c.PropertyName == propertyName);
//        }
//        public TableMapping.Column FindColumn(string columnName)
//        {
//            return this.Columns.FirstOrDefault((TableMapping.Column c) => c.Name == columnName);
//        }
//        public PreparedSqlLiteInsertCommand GetInsertCommand(SQLiteConnection conn, string extra)
//        {
//            if (this._insertCommand == null)
//            {
//                this._insertCommand = this.CreateInsertCommand(conn, extra);
//                this._insertCommandExtra = extra;
//            }
//            else
//            {
//                if (this._insertCommandExtra != extra)
//                {
//                    this._insertCommand.Dispose();
//                    this._insertCommand = this.CreateInsertCommand(conn, extra);
//                    this._insertCommandExtra = extra;
//                }
//            }
//            return this._insertCommand;
//        }
//        private PreparedSqlLiteInsertCommand CreateInsertCommand(SQLiteConnection conn, string extra)
//        {
//            TableMapping.Column[] array = this.InsertColumns;
//            string commandText;
//            if (!array.Any<TableMapping.Column>() && this.Columns.Count<TableMapping.Column>() == 1 && this.Columns[0].IsAutoInc)
//            {
//                commandText = string.Format("insert {1} into \"{0}\" default values", new object[]
//                {
//                    this.TableName,
//                    extra
//                });
//            }
//            else
//            {
//                if (string.Compare(extra, "OR REPLACE", StringComparison.OrdinalIgnoreCase) == 0)
//                {
//                    array = this.InsertOrReplaceColumns;
//                }
//                string arg_ED_0 = "insert {3} into \"{0}\"({1}) values ({2})";
//                object[] expr_72 = new object[4];
//                expr_72[0] = this.TableName;
//                int arg_B1_1 = 1;
//                string arg_AC_0 = ",";
//                IEnumerable<TableMapping.Column> arg_A2_0 = array;
//                Func<TableMapping.Column, string> arg_A2_1;
//                if ((arg_A2_1 = TableMapping.<>c__DisplayClass0.CS$<>9__CachedAnonymousMethodDelegate10) == null)
//                {
//                    arg_A2_1 = (TableMapping.<>c__DisplayClass0.CS$<>9__CachedAnonymousMethodDelegate10 = new Func<TableMapping.Column, string>(TableMapping.<>c__DisplayClass0.CS$<>9__inst.<CreateInsertCommand>b__9));
//                }
//                expr_72[arg_B1_1] = string.Join(arg_AC_0, arg_A2_0.Select(arg_A2_1).ToArray<string>());
//                int arg_E8_1 = 2;
//                string arg_E3_0 = ",";
//                IEnumerable<TableMapping.Column> arg_D9_0 = array;
//                Func<TableMapping.Column, string> arg_D9_1;
//                if ((arg_D9_1 = TableMapping.<>c__DisplayClass0.CS$<>9__CachedAnonymousMethodDelegate12) == null)
//                {
//                    arg_D9_1 = (TableMapping.<>c__DisplayClass0.CS$<>9__CachedAnonymousMethodDelegate12 = new Func<TableMapping.Column, string>(TableMapping.<>c__DisplayClass0.CS$<>9__inst.<CreateInsertCommand>b__11));
//                }
//                expr_72[arg_E8_1] = string.Join(arg_E3_0, arg_D9_0.Select(arg_D9_1).ToArray<string>());
//                expr_72[3] = extra;
//                commandText = string.Format(arg_ED_0, expr_72);
//            }
//            return new PreparedSqlLiteInsertCommand(this._sqlitePlatform, conn)
//            {
//                CommandText = commandText
//            };
//        }
//        protected internal void Dispose()
//        {
//            if (this._insertCommand != null)
//            {
//                this._insertCommand.Dispose();
//                this._insertCommand = null;
//            }
//        }
//    }
//}
