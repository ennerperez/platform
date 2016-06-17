using Platform.Support.Data.Attributes;
using Platform.Support.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

namespace Platform.Support.Data
{
    public static partial class Extensions
    {

        #region IDbConnection

#if DEBUG
        #region Timing
        public static bool TimeExecution { get; set; }
        private static Stopwatch _Stopwatch = new Stopwatch();
        private static long _ElapsedMilliseconds;

        #endregion
#endif

        private static readonly Random _rand = new Random();

        private static Dictionary<string, TableMapping> _mappings;
        private static Dictionary<string, TableMapping> _tables;

        private static int _transactionDepth = 0;

        public static bool StoreDateTimeAsTicks { get; private set; }

        #region Reflection

        private static Assembly _assembly;
        public static Assembly DataEngineAssembly(IDbConnection conn)
        {
            //if (_assembly == null) 
            _assembly = conn.GetAssembly();
            return _assembly;
        }

        internal static string GetNamespace(this IDbConnection conn)
        {
            switch (conn.GetEngine())
            {
                case Engines.Sql:
                    return "System.Data.SqlClient";
                case Engines.SqlCE:
                    return "System.Data.SqlServerCe";
                case Engines.MySql:
                    return "MySql.Data.MySqlClient";
                case Engines.SQLite:
                    return "System.Data.SQLite";
                default:
                    return "System.Data.OleDb";
            }
        }

        internal static object CreateObject(this IDbConnection conn, string type, object[] args = null)
        {
            Type _type;
            _type = DataEngineAssembly(conn).GetType(string.Format("{0}.{1}", GetNamespace(conn), type));
            return Activator.CreateInstance(_type, args);
        }
        internal static Engines GetEngine(this IDbConnection conn)
        {
            switch (conn.GetType().ToString().Split('.').Last())
            {
                case "SqlConnection":
                    //Return "Sql"
                    return Engines.Sql;
                case "SqlCeConnection":
                    //Return "SqlCe"
                    return Engines.SqlCE;
                case "MySqlConnection":
                    //Return "MySql"
                    return Engines.MySql;
                case "SQLiteConnection":
                    //Return "SQLite"
                    return Engines.SQLite;
                default:
                    //Return "OleDb"
                    return Engines.OleDb;
            }
        }

        internal static Assembly GetAssembly(this IDbConnection conn)
        {
            Assembly _return = null;
            string _file = string.Empty;
            string _asm = string.Empty;
            switch (conn.GetEngine())
            {
                case Engines.SqlCE:
                    _asm = "System.Data.SqlServerCe.dll";
                    break;
                case Engines.MySql:
                    _asm = "MySql.Data.dll";
                    break;
                case Engines.SQLite:
                    _asm = "System.Data.SQLite.dll";
                    break;
                default:
                    _asm = "System.Data";
                    //_return = Assembly.LoadWithPartialName(_asm)
                    _return = Assembly.Load("System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
                    return _return;
            }

            _file = System.IO.Path.Combine(new string[] { Assembly.GetEntryAssembly().DirectoryPath(), _asm });

            if (!string.IsNullOrEmpty(_file) && System.IO.File.Exists(_file))
            {
                _return = Assembly.LoadFile(_file);
            }
            else
            {
                if (Assembly.GetEntryAssembly() != null)
                {
                    _file = System.IO.Path.Combine(new string[] { Assembly.GetEntryAssembly().DirectoryPath(), _asm });
                    if (System.IO.File.Exists(_file))
                    {
                        _return = Assembly.LoadFile(_file);
                    }
                }
            }

            return _return;

        }

        public static DbType GetDbType(this Type typ)
        {
            Dictionary<Type, DbType> typeMap = new Dictionary<Type, DbType>();
            typeMap[typeof(byte)] = DbType.Byte;
            typeMap[typeof(sbyte)] = DbType.SByte;
            typeMap[typeof(short)] = DbType.Int16;
            typeMap[typeof(ushort)] = DbType.UInt16;
            typeMap[typeof(int)] = DbType.Int32;
            typeMap[typeof(uint)] = DbType.UInt32;
            typeMap[typeof(long)] = DbType.Int64;
            typeMap[typeof(ulong)] = DbType.UInt64;
            typeMap[typeof(float)] = DbType.Single;
            typeMap[typeof(double)] = DbType.Double;
            typeMap[typeof(decimal)] = DbType.Decimal;
            typeMap[typeof(bool)] = DbType.Boolean;
            typeMap[typeof(string)] = DbType.String;
            typeMap[typeof(char)] = DbType.StringFixedLength;
            typeMap[typeof(Guid)] = DbType.Guid;
            typeMap[typeof(DateTime)] = DbType.DateTime;
            typeMap[typeof(DateTimeOffset)] = DbType.DateTimeOffset;
            typeMap[typeof(TimeSpan)] = DbType.Time;
            typeMap[typeof(byte[])] = DbType.Binary;
            typeMap[typeof(System.Nullable<byte>)] = DbType.Byte;
            typeMap[typeof(System.Nullable<sbyte>)] = DbType.SByte;
            typeMap[typeof(System.Nullable<short>)] = DbType.Int16;
            typeMap[typeof(System.Nullable<ushort>)] = DbType.UInt16;
            typeMap[typeof(System.Nullable<int>)] = DbType.Int32;
            typeMap[typeof(System.Nullable<uint>)] = DbType.UInt32;
            typeMap[typeof(System.Nullable<long>)] = DbType.Int64;
            typeMap[typeof(System.Nullable<ulong>)] = DbType.UInt64;
            typeMap[typeof(System.Nullable<float>)] = DbType.Single;
            typeMap[typeof(System.Nullable<double>)] = DbType.Double;
            typeMap[typeof(System.Nullable<decimal>)] = DbType.Decimal;
            typeMap[typeof(System.Nullable<bool>)] = DbType.Boolean;
            typeMap[typeof(System.Nullable<char>)] = DbType.StringFixedLength;
            typeMap[typeof(System.Nullable<Guid>)] = DbType.Guid;
            typeMap[typeof(System.Nullable<DateTime>)] = DbType.DateTime;
            typeMap[typeof(System.Nullable<DateTimeOffset>)] = DbType.DateTimeOffset;
            typeMap[typeof(System.Nullable<TimeSpan>)] = DbType.Time;
            //typeMap(GetType(System.Data.Linq.Binary)) = DbType.Binary

            if (typeMap.ContainsKey(typ))
            {
                return typeMap[typ];
            }
            else
            {
                return DbType.Binary;
            }

        }
        public static object GetMaxValue(this Type typ)
        {
            Dictionary<Type, object> typeMap = new Dictionary<Type, object>();
            typeMap[typeof(byte)] = byte.MaxValue;
            typeMap[typeof(sbyte)] = sbyte.MaxValue;
            typeMap[typeof(short)] = short.MaxValue;
            typeMap[typeof(ushort)] = ushort.MaxValue;
            typeMap[typeof(int)] = int.MaxValue;
            typeMap[typeof(uint)] = uint.MaxValue;
            typeMap[typeof(long)] = long.MaxValue;
            typeMap[typeof(ulong)] = ulong.MaxValue;
            typeMap[typeof(float)] = float.MaxValue;
            typeMap[typeof(double)] = double.MaxValue;
            typeMap[typeof(decimal)] = decimal.MaxValue;
            typeMap[typeof(bool)] = 1;
            typeMap[typeof(char)] = 1;
            typeMap[typeof(Guid)] = Guid.NewGuid().ToString().Length;

            if (typeMap.ContainsKey(typ))
            {
                return typeMap[typ];
            }
            else
            {
                return 0;
            }
        }

        #endregion


        /// <summary>
        ///     Returns the mappings from types to tables that the connection
        ///     currently understands.
        /// </summary>
        public static IEnumerable<TableMapping> TableMappings
        {
            get { return _tables != null ? _tables.Values : Enumerable.Empty<TableMapping>(); }
        }

        /// <summary>
        ///     Whether <see cref="BeginTransaction" /> has been called and the database is waiting for a <see cref="Commit" />.
        /// </summary>
        public static bool IsInTransaction
        {
            get { return _transactionDepth > 0; }
        }

        /// <summary>
        ///     Retrieves the mapping that is automatically generated for the given type.
        /// </summary>
        /// <param name="type">
        ///     The type whose mapping to the database is returned.
        /// </param>
        /// <returns>
        ///     The mapping represents the schema of the columns of the database and contains
        ///     methods to set and get properties of objects.
        /// </returns>
        public static TableMapping GetMapping(this IDbConnection conn, Type type, CreateFlags createFlags = CreateFlags.None)
        {
            if (_mappings == null)
                _mappings = new Dictionary<string, Support.Data.TableMapping>();

            TableMapping map = null;
            if (!_mappings.TryGetValue(type.FullName, out map))
            {
                map = new TableMapping(conn, type, createFlags);
                _mappings[type.FullName] = map;
            }
            return map;
        }

        /// <summary>
        ///     Retrieves the mapping that is automatically generated for the given type.
        /// </summary>
        /// <returns>
        ///     The mapping represents the schema of the columns of the database and contains
        ///     methods to set and get properties of objects.
        /// </returns>
        public static TableMapping GetMapping<T>(this IDbConnection conn)
        {
            return GetMapping(conn, typeof(T));
        }


        /// <summary>
        ///     Executes a "drop schema" on the database.  This is non-recoverable.
        /// </summary>
        public static int DropSchema(this IDbConnection conn, TableMapping map)
        {
            string _tsql = null;
            switch (conn.GetEngine())
            {
                case Engines.Sql:
                case Engines.SqlCE:
                    _tsql = string.Format("IF EXISTS (Select schema_name FROM information_schema.schemata WHERE schema_name = '{0}' ) EXEC sp_executesql N'DROP SCHEMA {0}'", map.SchemaName);
                    break;
                default:
                    throw new NotSupportedException("Operation is not supported in the selected data engine.");
            }
            return conn.Execute(_tsql);
        }

        /// <summary>
        ///     Executes a "drop schema" on the database.  This is non-recoverable.
        /// </summary>
        public static int DropSchema(this IDbConnection conn, Type typ)
        {
            return conn.DropSchema(conn.GetMapping(typ));
        }

        /// <summary>
        ///     Executes a "drop schema" on the database.  This is non-recoverable.
        /// </summary>
        public static int DropSchema<T>(this IDbConnection conn)
        {
            return conn.DropSchema(typeof(T));
        }

        /// <summary>
        ///     Executes a "drop database" on the server.  This is non-recoverable.
        /// </summary>
        public static int DropDatabase(this IDbConnection conn)
        {
            string _tsql = null;
            System.Data.Common.DbConnectionStringBuilder csb;

            switch (conn.GetEngine())
            {
                case Engines.SQLite:
                    csb = (System.Data.Common.DbConnectionStringBuilder)conn.CreateObject("SQLiteConnectionStringBuilder", new object[] { conn.ConnectionString });
                    if (csb.ContainsKey("Data Source"))
                    {
                        object file;
                        csb.TryGetValue("Data Source", out file);
                        if (file != null && System.IO.File.Exists(file.ToString()))
                        {
                            if (conn.State != ConnectionState.Closed) conn.Close();
                            System.IO.File.Delete(file.ToString());
                        }
                    }
                    break;
                case Engines.MySql:
                    _tsql = String.Format("DROP DATABASE IF EXISTS {0}", conn.Database);
                    break;
                case Engines.Sql:
                    csb = (System.Data.Common.DbConnectionStringBuilder)conn.CreateObject("SqlConnectionStringBuilder", new object[] { conn.ConnectionString });
                    if (csb.ContainsKey("Initial Catalog")) csb.Remove("Initial Catalog");
                    csb.Add("Initial Catalog", "master");
                    string database = conn.Database;
                    conn = (IDbConnection)conn.CreateObject("SqlConnection", new object[] { csb.ToString() });

                    _tsql = "IF  EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'{0}') DROP DATABASE [{0}] ";
                    _tsql = string.Format(_tsql, database);

                    break;
                default:
                    throw new NotSupportedException("Operation is not supported in the selected data engine.");
            }
            return conn.Execute(_tsql);
        }

        /// <summary>
        ///     Executes a "drop table" on the database.  This is non-recoverable.
        /// </summary>
        public static int DropTable(this IDbConnection conn, TableMapping map, bool schema = false)
        {
            string _tsql = null;
            switch (conn.GetEngine())
            {
                case Engines.Sql:
                    _tsql = string.Format("IF OBJECT_ID('{0}', 'U') IS NOT NULL DROP TABLE [{0}]", map.TableName);

                    break;
                case Engines.SQLite:
                case Engines.MySql:
                    _tsql = string.Format("DROP TABLE IF EXISTS '{0}'", map.TableName);
                    break;
                default:
                    _tsql = string.Format("DROP TABLE [{0}]", map.TableName);
                    break;
            }

            int count = conn.Execute(_tsql);
            if (count != 0)
            {
                if (schema)
                    count += conn.DropSchema(map);
            }
            return count;
        }

        /// <summary>
        ///     Executes a "drop table" on the database.  This is non-recoverable.
        /// </summary>
        public static int DropTable(this IDbConnection conn, Type typ, bool schema = false)
        {
            return conn.DropTable(conn.GetMapping(typ), schema);
        }

        /// <summary>
        ///     Executes a "drop table" on the database.  This is non-recoverable.
        /// </summary>
        public static int DropTable<T>(this IDbConnection conn, bool schema = false)
        {
            return conn.DropTable(conn.GetMapping(typeof(T)), schema);
        }


        /// <summary>
        ///     Executes a "create schema if not exists" on the database.
        /// </summary>
        /// <returns>
        ///     The number of schema added to the database.
        /// </returns>
        public static int CreateSchema(this IDbConnection conn, TableMapping map)
        {
            string _tsql = null;
            switch (conn.GetEngine())
            {
                case Engines.Sql:
                case Engines.SqlCE:
                    _tsql = string.Format("IF NOT EXISTS (Select schema_name FROM information_schema.schemata WHERE schema_name = '{0}' ) EXEC sp_executesql N'CREATE SCHEMA [{0}]'", map.SchemaName);
                    break;
                default:
                    throw new NotSupportedException("Operation is not supported in the selected data engine.");
            }
            return conn.Execute(_tsql);
        }

        /// <summary>
        ///     Executes a "create schema if not exists" on the database.
        /// </summary>
        /// <returns>
        ///     The number of schema added to the database.
        /// </returns>
        public static int CreateSchema(this IDbConnection conn, Type typ)
        {
            return conn.CreateSchema(conn.GetMapping(typ));
        }

        /// <summary>
        ///     Executes a "create schema if not exists" on the database.
        /// </summary>
        /// <returns>
        ///     The number of schema added to the database.
        /// </returns>
        public static int CreateSchema<T>(this IDbConnection conn)
        {
            return conn.CreateSchema(typeof(T));
        }

        /// <summary>
        ///     Executes a "create database if not exists" on the server.
        /// </summary>
        /// <returns>
        ///     The number of databases added to the database.
        /// </returns>
        public static int CreateDatabase(this IDbConnection conn)
        {
            string _tsql = null;
            System.Data.Common.DbConnectionStringBuilder csb;

            switch (conn.GetEngine())
            {
                case Engines.SQLite:
                    csb = (System.Data.Common.DbConnectionStringBuilder)conn.CreateObject("SQLiteConnectionStringBuilder", new object[] { conn.ConnectionString });
                    if (csb.ContainsKey("Data Source"))
                    {
                        object file;
                        csb.TryGetValue("Data Source", out file);
                        if (file != null && !System.IO.File.Exists(file.ToString()))
                        {
                            conn.Open();
                            conn.Close();
                        }
                    }
                    break;
                case Engines.MySql:
                    _tsql = String.Format("CREATE DATABASE IF NOT EXISTS [{0}]", conn.Database);
                    break;
                case Engines.Sql:
                    csb = (System.Data.Common.DbConnectionStringBuilder)conn.CreateObject("SqlConnectionStringBuilder", new object[] { conn.ConnectionString });
                    if (csb.ContainsKey("Initial Catalog")) csb.Remove("Initial Catalog");
                    csb.Add("Initial Catalog", "master");
                    string database = conn.Database;
                    conn = (IDbConnection)conn.CreateObject("SqlConnection", new object[] { csb.ToString() });

                    string datapath = conn.ExecuteScalar<String>("SELECT SUBSTRING(physical_name, 1, CHARINDEX(N'master.mdf', LOWER(physical_name)) - 1) FROM master.sys.master_files WHERE database_id = 1 AND file_id = 1");

                    _tsql = "IF  NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'{0}') " +
                        "CREATE DATABASE [{0}] ON PRIMARY " +
                        "(NAME = '{0}', " +
                        "FILENAME = '{1}{0}.mdf', " +
                        "SIZE = 4096KB, FILEGROWTH = 10%) " +
                        "LOG ON (NAME = {0}_log, " +
                        "FILENAME = '{1}{0}_log.ldf', " +
                        "SIZE = 1024KB, FILEGROWTH = 10%)";
                    _tsql = string.Format(_tsql, database, datapath);

                    break;
                default:
                    throw new NotSupportedException("Operation is not supported in the selected data engine.");
            }
            return conn.Execute(_tsql);
        }

        /// <summary>
        ///     Executes a "create table if not exists" on the database. It also
        ///     creates any specified indexes on the columns of the table. It uses
        ///     a schema automatically generated from the specified type. You can
        ///     later access this schema by calling GetMapping.
        /// </summary>
        /// <param name="map">TableMapping to reflect to a database table.</param>
        /// <param name="createFlags">Optional flags allowing implicit PK and indexes based on naming conventions.</param>
        /// <returns>
        ///     The number of entries added to the database schema.
        /// </returns>
        public static int CreateTable(this IDbConnection conn, TableMapping map, CreateFlags createFlags = CreateFlags.None)
        {
            IEnumerable<string> decls = map.Columns.Select(p => Orm.SqlDecl(conn, p, StoreDateTimeAsTicks));
            string decl = null;
            string query = null;

            int count = 0;

            switch (conn.GetEngine())
            {
                case Engines.SQLite:
                case Engines.MySql:
                    decl = string.Join(",", decls.ToArray());
                    query = string.Format("CREATE TABLE IF NOT EXISTS '{0}' ({1});", map.TableName, decl);
                    conn.Execute(query);
                    count = conn.ExecuteScalar<int>(String.Format("SELECT COUNT(1) FROM sqlite_master WHERE type='table' AND name='{0}';", map.TableName));
                    break;
                case Engines.Sql:
                case Engines.SqlCE:
                    decl = string.Join(",", decls.ToArray());
                    query = String.Format("IF OBJECT_ID('{0}', 'U') IS NULL CREATE TABLE [{0}] ({1})", map.TableName, decl);

                    if (!string.IsNullOrEmpty(map.SchemaName))
                    {
                        count = conn.CreateSchema(map);
                        if (count != 0)
                            count += conn.Execute(query);
                    }
                    else
                    {
                        count += conn.Execute(query);
                    }

                    break;
                case Engines.OleDb:
                    try
                    {
                        decl = string.Join(",", decls.ToArray());
                        query = string.Format("CREATE TABLE '{0}' ({1});", map.TableName, decl);
                        count = conn.Execute(query);
                    }
                    catch (System.Data.OleDb.OleDbException e)
                    {
                        if (e.ErrorCode != 3010 && e.ErrorCode != 3012)
                            throw e;
                    }
                    break;
                default:
                    break;
            }

            if (count == 0)
            {
                //Possible bug: This always seems to return 0?
                // Table already exists, migrate it
                conn.MigrateTable(map);
            }

            Dictionary<string, IndexInfo> indexes = new Dictionary<string, IndexInfo>();
            foreach (TableMapping.Column c in map.Columns)
            {
                foreach (IndexedAttribute i in c.Indices)
                {
                    string iname = i.Name ?? map.GetTableName(false) + "_" + c.Name;
                    IndexInfo iinfo;
                    if (!indexes.TryGetValue(iname, out iinfo))
                    {
                        iinfo = new IndexInfo
                        {
                            IndexName = iname,
                            SchemaName = map.SchemaName,
                            TableName = map.GetTableName(false),
                            Unique = i.IsUnique,
                            Columns = new List<IndexedColumn>()
                        };
                        indexes.Add(iname, iinfo);
                    }

                    if (i.IsUnique != iinfo.Unique)
                    {
                        throw new Exception("All the columns in an index must have the same value for their Unique property");
                    }

                    iinfo.Columns.Add(new IndexedColumn
                    {
                        Order = i.Order,
                        ColumnName = c.Name
                    });
                }
            }

            foreach (string indexName in indexes.Keys)
            {
                IndexInfo index = indexes[indexName];
                string[] columns = index.Columns.OrderBy(i => i.Order).Select(i => i.ColumnName).ToArray();
                count += conn.CreateIndex(indexName, index.SchemaName, index.TableName, columns, index.Unique, index.NonClustered);
            }

            return count;
        }

        /// <summary>
        ///     Executes a "create table if not exists" on the database. It also
        ///     creates any specified indexes on the columns of the table. It uses
        ///     a schema automatically generated from the specified type. You can
        ///     later access this schema by calling GetMapping.
        /// </summary>
        /// <param name="typ">Type to reflect to a database table.</param>
        /// <param name="createFlags">Optional flags allowing implicit PK and indexes based on naming conventions.</param>
        /// <returns>
        ///     The number of entries added to the database schema.
        /// </returns>
        public static int CreateTable(this IDbConnection conn, Type typ, CreateFlags createFlags = CreateFlags.None)
        {
            if (_tables == null)
            {
                _tables = new Dictionary<string, TableMapping>();
            }
            TableMapping map;
            if (!_tables.TryGetValue(typ.FullName, out map))
            {
                map = conn.GetMapping(typ, createFlags);
                _tables.Add(typ.FullName, map);
            }

            return conn.CreateTable(map, createFlags);
        }

        /// <summary>
        ///     Executes a "create table if not exists" on the database. It also
        ///     creates any specified indexes on the columns of the table. It uses
        ///     a schema automatically generated from the specified type. You can
        ///     later access this schema by calling GetMapping.
        /// </summary>
        /// <returns>
        ///     The number of entries added to the database schema.
        /// </returns>
        public static int CreateTable<T>(this IDbConnection conn, CreateFlags createFlags = CreateFlags.None)
        {
            return conn.CreateTable(typeof(T), createFlags);
        }


        /// <summary>
        /// Creates an index for the specified table and columns.
        /// </summary>
        /// <param name="indexName">Name of the index to create</param>
        /// <param name="tableName">Name of the database table</param>
        /// <param name="columnNames">An array of column names to index</param>
        /// <param name="unique">Whether the index should be unique</param>
        public static int CreateIndex(this IDbConnection conn, string indexName, string schemaName, string tableName, string[] columnNames, bool unique = false, bool nonclustered = false)
        {
            string sqlFormat = null;
            string tsql = null;

            if (unique)
            {
                if (!indexName.StartsWith("PK_")) indexName = "PK_" + indexName;
            }
            else
            {
                if (!indexName.StartsWith("IX_")) indexName = "IX_" + indexName;
            }

            string columns = null;
            int count = 0;

            switch (conn.GetEngine())
            {
                case Engines.SQLite:
                case Engines.MySql:
                    sqlFormat = "CREATE {2} INDEX IF NOT EXISTS '{3}' ON '{0}'({1});";
                    columns = string.Join(", ", columnNames.Select(C => string.Format("'{0}'", C)).ToArray());
                    tsql = string.Format(sqlFormat, tableName, columns, unique ? "UNIQUE" : "", indexName);
                    conn.Execute(tsql);
                    count = conn.ExecuteScalar<int>(String.Format("SELECT COUNT(1) FROM sqlite_master WHERE type='index' AND name='{0}';", indexName));
                    break;
                case Engines.Sql:
                case Engines.SqlCE:
                    if (!string.IsNullOrEmpty(schemaName))
                        tableName = schemaName + "." + tableName;
                    sqlFormat = "IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = N'{3}') CREATE {2} " + (nonclustered ? "NONCLUSTERED" : "" + " INDEX {3} ON [{0}] ({1});");
                    columns = string.Join(", ", columnNames.Select(C => string.Format("[{0}]", C)).ToArray());
                    tsql = string.Format(sqlFormat, tableName, columns, unique ? "UNIQUE" : "", indexName);
                    count = conn.Execute(tsql);
                    break;
                case Engines.OleDb:
                    try
                    {
                        sqlFormat = "CREATE {2} INDEX '{3}' ON '{0}'({1});";
                        columns = string.Join(", ", columnNames.Select(C => string.Format("'{0}'", C)).ToArray());
                        tsql = string.Format(sqlFormat, tableName, columns, unique ? "UNIQUE" : "", indexName);
                        count = conn.Execute(tsql);
                    }
                    catch (System.Data.OleDb.OleDbException e)
                    {
                        if (e.ErrorCode != 3010 && e.ErrorCode != 3012)
                            throw e;
                    }
                    break;
                default:
                    throw new NotSupportedException("Operation is not supported in the selected data engine.");
            }

            return count;

        }

        /// <summary>
        /// Creates an index for the specified table and column.
        /// </summary>
        /// <param name="indexName">Name of the index to create</param>
        /// <param name="tableName">Name of the database table</param>
        /// <param name="columnName">Name of the column to index</param>
        /// <param name="unique">Whether the index should be unique</param>
        public static int CreateIndex(this IDbConnection conn, string indexName, string schemaName, string tableName, string columnName, bool unique = false, bool nonclustered = false)
        {
            return conn.CreateIndex(indexName, schemaName, tableName, new string[] { columnName }, unique, nonclustered);
        }

        /// <summary>
        /// Creates an index for the specified table and column.
        /// </summary>
        /// <param name="tableName">Name of the database table</param>
        /// <param name="columnName">Name of the column to index</param>
        /// <param name="unique">Whether the index should be unique</param>
        public static int CreateIndex(this IDbConnection conn, string tableName, string schemaName, string columnName, bool unique = false, bool nonclustered = false)
        {
            return conn.CreateIndex(tableName + "_" + columnName, schemaName, tableName, columnName, unique, nonclustered);
        }

        /// <summary>
        /// Creates an index for the specified table and columns.
        /// </summary>
        /// <param name="tableName">Name of the database table</param>
        /// <param name="columnNames">An array of column names to index</param>
        /// <param name="unique">Whether the index should be unique</param>
        public static int CreateIndex(this IDbConnection conn, string tableName, string schemaName, string[] columnNames, bool unique = false, bool nonclustered = false)
        {
            return conn.CreateIndex(tableName + "_" + string.Join("_", columnNames), schemaName, tableName, columnNames, unique, nonclustered);
        }

        /// <summary>
        ///     Creates an index for the specified object property.
        ///     e.g. CreateIndex{Client}(c => c.Name);
        /// </summary>
        /// <typeparam name="T">Type to reflect to a database table.</typeparam>
        /// <param name="property">Property to index</param>
        /// <param name="unique">Whether the index should be unique</param>
        public static void CreateIndex<T>(this IDbConnection conn, Expression<Func<T, object>> property, bool unique = false, bool nonclustered = false)
        {
            MemberExpression mx;
            if (property.Body.NodeType == ExpressionType.Convert)
            {
                mx = ((UnaryExpression)property.Body).Operand as MemberExpression;
            }
            else
            {
                mx = (property.Body as MemberExpression);
            }
            var propertyInfo = mx.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException("The lambda expression 'property' should point to a valid Property");
            }

            string propName = propertyInfo.Name;

            TableMapping map = conn.GetMapping<T>();
            string colName = map.FindColumnWithPropertyName(propName).Name;

            conn.CreateIndex(map.TableName, map.SchemaName, colName, unique, nonclustered);
        }


        public static List<ColumnInfo> GetTableInfo(this IDbConnection conn, string tableName)
        {
            string tsql = null;
            switch (conn.GetEngine())
            {
                case Engines.Sql:
                    tsql = "SELECT COLUMN_NAME AS name FROM INFORMATION_SCHEMA.COLUMNS WHERE  TABLE_NAME = '" + tableName + "'";
                    break;
                case Engines.SQLite:
                    tsql = "pragma table_info('" + tableName + "')";
                    break;
                default:
                    throw new NotSupportedException("Operation is not supported in the selected data engine.");
            }
            return conn.Query<ColumnInfo>(tsql);
        }

        private static void MigrateTable(this IDbConnection conn, TableMapping map)
        {
            List<ColumnInfo> existingCols = conn.GetTableInfo(map.TableName);

            var toBeAdded = new List<TableMapping.Column>();

            foreach (TableMapping.Column p in map.Columns)
            {
                bool found = false;
                foreach (ColumnInfo c in existingCols)
                {
                    found = (string.Compare(p.Name, c.Name, StringComparison.OrdinalIgnoreCase) == 0);
                    if (found)
                    {
                        break;
                    }
                }
                if (!found)
                {
                    toBeAdded.Add(p);
                }
            }

            foreach (TableMapping.Column p in toBeAdded)
            {
                string addCol = "ALTER TABLE '" + map.TableName + "' ADD COLUMN " +
                                Orm.SqlDecl(conn, p, StoreDateTimeAsTicks); //, this.Serializer, this.ExtraTypeMappings);
                conn.Execute(addCol);
            }
        }

        public static IDbDataAdapter CreateAdapter(this IDbConnection conn)
        {
            string _type = null;
            switch (conn.GetEngine())
            {
                case Engines.Sql:
                    _type = "SqlDataAdapter";
                    break;
                case Engines.SqlCE:
                    _type = "SqlCeDataAdapter";
                    break;
                case Engines.MySql:
                    _type = "MySqlDataAdapter";
                    break;
                case Engines.SQLite:
                    _type = "SQLiteDataAdapter";
                    break;
                default:
                    _type = "OleDbDataAdapter";
                    break;
            }

            IDbDataAdapter _return = (IDbDataAdapter)conn.CreateObject(_type);

            return _return;
        }

        public static IDbDataParameter CreateParameter(this IDbConnection conn, string name, object value)
        {
            string _type = null;
            switch (conn.GetEngine())
            {
                case Engines.Sql:
                    _type = "SqlParameter";
                    break;
                case Engines.SqlCE:
                    _type = "SqlCeParameter";
                    break;
                case Engines.MySql:
                    _type = "MySqlParameter";
                    break;
                case Engines.SQLite:
                    _type = "SQLiteParameter";
                    break;
                default:
                    _type = "OleDbParameter";
                    break;
            }

            if (!name.StartsWith("@"))
                name = "@" + name;

            var rvalue = value == null ? DBNull.Value : value;
            IDbDataParameter _return = (IDbDataParameter)conn.CreateObject(_type, new object[] { name, rvalue });
            if (value != null)
            {
                _return.DbType = value.GetType().GetDbType();

                if (object.ReferenceEquals(value.GetType(), typeof(string)))
                {
                    _return.Size = value.ToString().Length;
                }
                else if (object.ReferenceEquals(value.GetType(), typeof(decimal)) || object.ReferenceEquals(value.GetType(), typeof(double)))
                {
                    //_return.Precision = value.ToString().Split(new string[] { System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator }, StringSplitOptions.None).Last().Length;
                    //if (_return.Precision == 0) _return.Precision = System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalDigits;
                    _return.Precision = 18;
                    _return.Scale = 8;

                }
                else if (object.ReferenceEquals(value.GetType(), typeof(Int64)) || object.ReferenceEquals(value.GetType(), typeof(UInt64)))
                {
                    _return.Size = int.MaxValue;
                }
                else if (object.ReferenceEquals(value.GetType(), typeof(Version)) || object.ReferenceEquals(value.GetType(), typeof(Guid)))
                {
                    _return.Value = value.ToString();
                    _return.DbType = DbType.String;
                    _return.Size = value.ToString().Length;
                }
                else
                {
                    try
                    {
                        _return.Size = (int)(value.GetType().GetMaxValue());
                        //CObj(value.GetType).MaxValue
                    }
                    catch //(Exception ex)
                    {
                    }
                }
            }
            else
            {
                _return.DbType = DbType.String;
                _return.Size = 4;
            }

            if (_return.Size == 0) { _return.Size = 1; }

            return _return;
        }

        public static IDbDataParameter CreateParameter(this IDbConnection conn, object[] args)
        {
            return conn.CreateParameter(args[0].ToString(), args[1]);
        }

        public static IEnumerable<IDbDataParameter> GenerateParameters(this IDbConnection conn, object[] args, string prefix = "param")
        {
            List<IDbDataParameter> _params = new List<IDbDataParameter>();
            for (int i = 0; i <= args.Length - 1; i++)
            {
                if ((args[i]) is IDbDataParameter)
                {
                    _params.Add((IDbDataParameter)args[i]);
                }
                else
                {
                    _params.Add(conn.CreateParameter(prefix + (i + 1), args[i].Value()));
                }
            }
            return _params.ToArray();
        }


        /// <summary>
        ///     Creates a new SQLiteCommand given the command text with arguments. Place a '?'
        ///     in the command text for each of the arguments.
        /// </summary>
        /// <param name="cmdText">
        ///     The fully escaped SQL.
        /// </param>
        /// <param name="args">
        ///     Arguments to substitute for the occurences of '?' in the command text.
        /// </param>
        /// <returns>
        ///     A <see cref="SQLiteCommand" />
        /// </returns>
        public static IDbCommand CreateCommand(this IDbConnection conn, string cmdText, IDbDataParameter[] args)
        {
            IDbCommand _return;
            string _type = null;
            switch (conn.GetEngine())
            {
                case Engines.Sql:
                    _type = "SqlCommand";
                    break;
                case Engines.SqlCE:
                    _type = "SqlCeCommand";
                    break;
                case Engines.MySql:
                    _type = "MySqlCommand";
                    break;
                case Engines.SQLite:
                    _type = "SQLiteCommand";
                    break;
                default:
                    _type = "OleDbCommand";
                    break;
            }

            IDbConnection _conn = (IDbConnection)conn.CreateObject(conn.GetType().ToString().Split('.').Last(), new object[] { conn.ConnectionString });
            _return = (IDbCommand)conn.CreateObject(_type, new object[] { cmdText, _conn });
            _return.Parameters.Clear();
            if (args != null)
            {
                foreach (System.Data.IDbDataParameter o in args)
                {
                    _return.Parameters.Add(o);
                }
            }
            return _return;
        }

        /// <summary>
        ///     Creates a new SQLiteCommand given the command text with arguments. Place a '?'
        ///     in the command text for each of the arguments.
        /// </summary>
        /// <param name="cmdText">
        ///     The fully escaped SQL.
        /// </param>
        /// <param name="args">
        ///     Arguments to substitute for the occurences of '?' in the command text.
        /// </param>
        /// <returns>
        ///     A <see cref="SQLiteCommand" />
        /// </returns>
        public static IDbCommand CreateCommand(this IDbConnection conn, string cmdText, params object[] args)
        {
            return conn.CreateCommand(cmdText, conn.GenerateParameters(args).ToArray());
        }


        public static object LastInsertRowPK(this IDbConnection conn, TableMapping map)
        {
            object _return = 0;
            string _tsql = null;

            if (map.HasAutoIncPK)
            {
                _tsql = string.Format("SELECT MAX({0}) FROM {1}", map.PK.Name, map.TableName);
                _return = conn.ExecuteScalar<object>(_tsql);
                return Convert.ChangeType(_return, map.PK.ColumnType);
            }

            return _return;
        }

        public static object LastInsertRowPK(this IDbConnection conn, Type ty)
        {
            return conn.LastInsertRowPK(conn.GetMapping(ty));
        }

        public static object LastInsertRowPK<T>(this IDbConnection conn)
        {
            return conn.LastInsertRowPK(typeof(T));
        }

        public static object LastInsertRowPK(this IDbConnection conn)
        {
            object _return = 0;

            string _tsql = null;
            switch (conn.GetEngine())
            {
                case Engines.Sql:
                    _tsql = "SELECT SCOPE_IDENTITY()";
                    break;
                case Engines.SQLite:
                    _tsql = "SELECT last_insert_rowid()";
                    break;
                case Engines.MySql:
                    _tsql = "SELECT LAST_INSERT_ID()";
                    break;
                default:
                    throw new NotSupportedException("Operation is not supported in the selected data engine.");
            }

            _return = conn.ExecuteScalar<object>(_tsql);

            return _return;
        }


        /// <summary>
        ///     Creates a SQLiteCommand given the command text (SQL) with arguments. Place a '?'
        ///     in the command text for each of the arguments and then executes that command.
        ///     Use this method instead of Query when you don't expect rows back. Such cases include
        ///     INSERTs, UPDATEs, and DELETEs.
        ///     You can set the Trace or TimeExecution properties of the connection
        ///     to profile execution.
        /// </summary>
        /// <param name="query">
        ///     The fully escaped SQL.
        /// </param>
        /// <param name="args">
        ///     Arguments to substitute for the occurences of '?' in the query.
        /// </param>
        /// <returns>
        ///     The number of rows modified in the database as a result of this execution.
        /// </returns>
        public static int Execute(this IDbConnection conn, string query, IDbDataParameter[] args)
        {

            if (string.IsNullOrEmpty(query))
                return 0;

            IDbCommand cmd = conn.CreateCommand(query, args);

#if DEBUG
            if (TimeExecution)
            {
                _Stopwatch.Reset();
                _Stopwatch.Start();
            }
#endif
            cmd.Connection.Open();

            try
            {
                cmd.Prepare();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("{0} -- {1}", query, ex.Message));
            }

            int _return = 0;
#if DEBUG
            try
            {
#endif
            _return = cmd.ExecuteNonQuery();
#if DEBUG
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
#endif
            cmd.Connection.Close();
#if DEBUG
            if (TimeExecution)
            {
                _Stopwatch.Stop();
                _ElapsedMilliseconds += _Stopwatch.ElapsedMilliseconds;
                Debug.WriteLine("Finished in {0} ms ({1:0.0} s total)", _Stopwatch.ElapsedMilliseconds, _ElapsedMilliseconds / 1000.0);
            }
#endif
            return _return;
        }

        /// <summary>
        ///     Creates a SQLiteCommand given the command text (SQL) with arguments. Place a '?'
        ///     in the command text for each of the arguments and then executes that command.
        ///     Use this method instead of Query when you don't expect rows back. Such cases include
        ///     INSERTs, UPDATEs, and DELETEs.
        ///     You can set the Trace or TimeExecution properties of the connection
        ///     to profile execution.
        /// </summary>
        /// <param name="query">
        ///     The fully escaped SQL.
        /// </param>
        /// <param name="args">
        ///     Arguments to substitute for the occurences of '?' in the query.
        /// </param>
        /// <returns>
        ///     The number of rows modified in the database as a result of this execution.
        /// </returns>
        public static int Execute(this IDbConnection conn, string query, params object[] args)
        {
            return conn.Execute(query, conn.GenerateParameters(args).ToArray());
        }

        public static T ExecuteScalar<T>(this IDbConnection conn, string query, IDbDataParameter[] args)
        {

            if (string.IsNullOrEmpty(query))
                return default(T);

            IDbCommand cmd = conn.CreateCommand(query, args);

#if DEBUG
            if (TimeExecution)
            {
                _Stopwatch.Reset();
                _Stopwatch.Start();
            }
#endif

            var _return = cmd.ExecuteScalar<T>();

#if DEBUG
            if (TimeExecution)
            {
                _Stopwatch.Stop();
                _ElapsedMilliseconds += _Stopwatch.ElapsedMilliseconds;
                Debug.WriteLine("Finished in {0} ms ({1:0.0} s total)", _Stopwatch.ElapsedMilliseconds, _ElapsedMilliseconds / 1000.0);
            }
#endif

            return _return;
        }

        public static T ExecuteScalar<T>(this IDbConnection conn, string query, params object[] args)
        {
            return conn.ExecuteScalar<T>(query, conn.GenerateParameters(args).ToArray());
        }

        public static T ExecuteScalar<T>(this IDbCommand cmd)
        {
            cmd.Connection.Open();
            object _return = cmd.ExecuteScalar();
            cmd.Connection.Close();
            T _result = (T)_return; //.CType<T>();
            return _result;
        }

        public static DataSet Fill(this IDbConnection conn, string query, IDbDataParameter[] args)
        {

            if (string.IsNullOrEmpty(query))
                return null;

            IDbCommand cmd = conn.CreateCommand(query, args);
            IDbDataAdapter adapter = conn.CreateAdapter();

#if DEBUG
            if (TimeExecution)
            {
                _Stopwatch.Reset();
                _Stopwatch.Start();
            }
#endif
            cmd.Connection.Open();
            var _result = new DataSet();
            adapter.SelectCommand = cmd;
            adapter.Fill(_result);
            cmd.Connection.Close();

#if DEBUG
            if (TimeExecution)
            {
                _Stopwatch.Stop();
                _ElapsedMilliseconds += _Stopwatch.ElapsedMilliseconds;
                Debug.WriteLine("Finished in {0} ms ({1:0.0} s total)", _Stopwatch.ElapsedMilliseconds, _ElapsedMilliseconds / 1000.0);
            }
#endif

            return _result;
        }

        public static DataSet Fill(this IDbConnection conn, string query, params object[] args)
        {
            return Fill(conn, query, conn.GenerateParameters(args).ToArray());
        }

        public static DataSet Fill(this IDbCommand cmd)
        {
            return Fill(cmd.Connection, cmd.CommandText, cmd.Parameters);
        }

        /// <summary>
        ///     Creates a SQLiteCommand given the command text (SQL) with arguments. Place a '?'
        ///     in the command text for each of the arguments and then executes that command.
        ///     It returns each row of the result using the mapping automatically generated for
        ///     the given type.
        /// </summary>
        /// <param name="query">
        ///     The fully escaped SQL.
        /// </param>
        /// <param name="args">
        ///     Arguments to substitute for the occurences of '?' in the query.
        /// </param>
        /// <returns>
        ///     An enumerable with one result for each row returned by the query.
        /// </returns>
        public static List<T> Query<T>(this IDbConnection conn, string query, params object[] args) where T : new()
        {
            IDbCommand cmd = conn.CreateCommand(query, args);
            return cmd.ExecuteQuery<T>();
        }

        /// <summary>
        ///     Creates a SQLiteCommand given the command text (SQL) with arguments. Place a '?'
        ///     in the command text for each of the arguments and then executes that command.
        ///     It returns each row of the result using the mapping automatically generated for
        ///     the given type.
        /// </summary>
        /// <param name="query">
        ///     The fully escaped SQL.
        /// </param>
        /// <param name="args">
        ///     Arguments to substitute for the occurences of '?' in the query.
        /// </param>
        /// <returns>
        ///     An enumerable with one result for each row returned by the query.
        /// </returns>
        public static List<T> Query<T>(this IDbConnection conn, string query, IDbDataParameter[] args) where T : new()
        {
            IDbCommand cmd = conn.CreateCommand(query, args);
            return cmd.ExecuteQuery<T>();
        }

        /// <summary>
        ///     Creates a SQLiteCommand given the command text (SQL) with arguments. Place a '?'
        ///     in the command text for each of the arguments and then executes that command.
        ///     It returns each row of the result using the mapping automatically generated for
        ///     the given type.
        /// </summary>
        /// <param name="query">
        ///     The fully escaped SQL.
        /// </param>
        /// <param name="args">
        ///     Arguments to substitute for the occurences of '?' in the query.
        /// </param>
        /// <returns>
        ///     An enumerable with one result for each row returned by the query.
        ///     The enumerator will call sqlite3_step on each call to MoveNext, so the database
        ///     connection must remain open for the lifetime of the enumerator.
        /// </returns>
        public static IEnumerable<T> DeferredQuery<T>(this IDbConnection conn, string query, params object[] args) where T : new()
        {
            IDbCommand cmd = conn.CreateCommand(query, args);
            return cmd.ExecuteDeferredQuery<T>();
        }

        /// <summary>
        ///     Creates a SQLiteCommand given the command text (SQL) with arguments. Place a '?'
        ///     in the command text for each of the arguments and then executes that command.
        ///     It returns each row of the result using the specified mapping. This function is
        ///     only used by libraries in order to query the database via introspection. It is
        ///     normally not used.
        /// </summary>
        /// <param name="map">
        ///     A <see cref="TableMapping" /> to use to convert the resulting rows
        ///     into objects.
        /// </param>
        /// <param name="query">
        ///     The fully escaped SQL.
        /// </param>
        /// <param name="args">
        ///     Arguments to substitute for the occurences of '?' in the query.
        /// </param>
        /// <returns>
        ///     An enumerable with one result for each row returned by the query.
        /// </returns>
        public static List<object> Query(this IDbConnection conn, TableMapping map, string query, IDbDataParameter[] args)
        {
            IDbCommand cmd = conn.CreateCommand(query, args);
            return cmd.ExecuteQuery<object>(map);
        }
        /// <summary>
        ///     Creates a SQLiteCommand given the command text (SQL) with arguments. Place a '?'
        ///     in the command text for each of the arguments and then executes that command.
        ///     It returns each row of the result using the specified mapping. This function is
        ///     only used by libraries in order to query the database via introspection. It is
        ///     normally not used.
        /// </summary>
        /// <param name="map">
        ///     A <see cref="TableMapping" /> to use to convert the resulting rows
        ///     into objects.
        /// </param>
        /// <param name="query">
        ///     The fully escaped SQL.
        /// </param>
        /// <param name="args">
        ///     Arguments to substitute for the occurences of '?' in the query.
        /// </param>
        /// <returns>
        ///     An enumerable with one result for each row returned by the query.
        /// </returns>
        public static List<object> Query(this IDbConnection conn, TableMapping map, string query, params object[] args)
        {
            return conn.Query(map, query, conn.GenerateParameters(args).ToArray());
        }

        /// <summary>
        ///     Creates a SQLiteCommand given the command text (SQL) with arguments. Place a '?'
        ///     in the command text for each of the arguments and then executes that command.
        ///     It returns each row of the result using the specified mapping. This function is
        ///     only used by libraries in order to query the database via introspection. It is
        ///     normally not used.
        /// </summary>
        /// <param name="map">
        ///     A <see cref="TableMapping" /> to use to convert the resulting rows
        ///     into objects.
        /// </param>
        /// <param name="query">
        ///     The fully escaped SQL.
        /// </param>
        /// <param name="args">
        ///     Arguments to substitute for the occurences of '?' in the query.
        /// </param>
        /// <returns>
        ///     An enumerable with one result for each row returned by the query.
        ///     The enumerator will call sqlite3_step on each call to MoveNext, so the database
        ///     connection must remain open for the lifetime of the enumerator.
        /// </returns>
        public static IEnumerable<object> DeferredQuery(this IDbConnection conn, TableMapping map, string query, params IDbDataParameter[] args)
        {
            IDbCommand cmd = conn.CreateCommand(query, args);
            return cmd.ExecuteDeferredQuery<object>(map);
        }

        /// <summary>
        ///     Creates a SQLiteCommand given the command text (SQL) with arguments. Place a '?'
        ///     in the command text for each of the arguments and then executes that command.
        ///     It returns each row of the result using the specified mapping. This function is
        ///     only used by libraries in order to query the database via introspection. It is
        ///     normally not used.
        /// </summary>
        /// <param name="map">
        ///     A <see cref="TableMapping" /> to use to convert the resulting rows
        ///     into objects.
        /// </param>
        /// <param name="query">
        ///     The fully escaped SQL.
        /// </param>
        /// <param name="args">
        ///     Arguments to substitute for the occurences of '?' in the query.
        /// </param>
        /// <returns>
        ///     An enumerable with one result for each row returned by the query.
        ///     The enumerator will call sqlite3_step on each call to MoveNext, so the database
        ///     connection must remain open for the lifetime of the enumerator.
        /// </returns>
        public static IEnumerable<object> DeferredQuery(this IDbConnection conn, TableMapping map, string query, params object[] args)
        {
            return conn.DeferredQuery(map, query, conn.GenerateParameters(args).ToArray());
        }


        /// <summary>
        ///     Returns a queryable interface to the table represented by the given type.
        /// </summary>
        /// <returns>
        ///     A queryable object that is able to translate Where, OrderBy, and Take
        ///     queries into native SQL.
        /// </returns>
        public static TableQuery<T> Table<T>(this IDbConnection conn) where T : new()
        {
            return new TableQuery<T>(conn);
        }


        /// <summary>
        ///     Attempts to retrieve an object with the given primary key from the table
        ///     associated with the specified type. Use of this method requires that
        ///     the given type have a designated PrimaryKey (using the PrimaryKeyAttribute).
        /// </summary>
        /// <param name="pk">
        ///     The primary key.
        /// </param>
        /// <returns>
        ///     The object with the given primary key. Throws a not found exception
        ///     if the object is not found.
        /// </returns>
        public static T Get<T>(this IDbConnection conn, object pk) where T : new()
        {
            TableMapping map = conn.GetMapping(typeof(T));
            return conn.Query<T>(map.GetByPrimaryKeySql, pk).First();
        }

        /// <summary>
        ///     Attempts to retrieve the first object that matches the predicate from the table
        ///     associated with the specified type.
        /// </summary>
        /// <param name="predicate">
        ///     A predicate for which object to find.
        /// </param>
        /// <returns>
        ///     The object that matches the given predicate. Throws a not found exception
        ///     if the object is not found.
        /// </returns>
        public static T Get<T>(this IDbConnection conn, Expression<Func<T, bool>> predicate) where T : new()
        {
            return Table<T>(conn).Where(predicate).First();
        }

        /// <summary>
        ///     Attempts to retrieve an object with the given primary key from the table
        ///     associated with the specified type. Use of this method requires that
        ///     the given type have a designated PrimaryKey (using the PrimaryKeyAttribute).
        /// </summary>
        /// <param name="pk">
        ///     The primary key.
        /// </param>
        /// <returns>
        ///     The object with the given primary key or null
        ///     if the object is not found.
        /// </returns>
        public static T Find<T>(this IDbConnection conn, object pk) where T : new()
        {
            TableMapping map = conn.GetMapping(typeof(T));
            return conn.Query<T>(map.GetByPrimaryKeySql, pk).FirstOrDefault();
        }

        /// <summary>
        ///     Attempts to retrieve an object with the given primary key from the table
        ///     associated with the specified type. Use of this method requires that
        ///     the given type have a designated PrimaryKey (using the PrimaryKeyAttribute).
        /// </summary>
        /// <param name="pk">
        ///     The primary key.
        /// </param>
        /// <param name="map">
        ///     The TableMapping used to identify the object type.
        /// </param>
        /// <returns>
        ///     The object with the given primary key or null
        ///     if the object is not found.
        /// </returns>
        public static object Find(this IDbConnection conn, object pk, TableMapping map)
        {
            return conn.Query(map, map.GetByPrimaryKeySql, pk).FirstOrDefault();
        }

        /// <summary>
        ///     Attempts to retrieve the first object that matches the predicate from the table
        ///     associated with the specified type.
        /// </summary>
        /// <param name="predicate">
        ///     A predicate for which object to find.
        /// </param>
        /// <returns>
        ///     The object that matches the given predicate or null
        ///     if the object is not found.
        /// </returns>
        public static T Find<T>(this IDbConnection conn, Expression<Func<T, bool>> predicate) where T : new()
        {
            return Table<T>(conn).Where(predicate).FirstOrDefault();
        }


        /// <summary>
        ///     Begins a new transaction. Call <see cref="Commit" /> to end the transaction.
        /// </summary>
        /// <example cref="System.InvalidOperationException">Throws if a transaction has already begun.</example>
        public static void BeginTransaction(this IDbConnection conn)
        {
            // The BEGIN command only works if the transaction stack is empty, 
            //    or in other words if there are no pending transactions. 
            // If the transaction stack is not empty when the BEGIN command is invoked, 
            //    then the command fails with an error.
            // Rather than crash with an error, we will just ignore calls to BeginTransaction
            //    that would result in an error.
            if (Interlocked.CompareExchange(ref _transactionDepth, 1, 0) == 0)
            {
                try
                {
                    conn.Execute("begin transaction");
                }
                catch //(Exception ex)
                {
                    //var sqlExp = ex as Exception;
                    //if (sqlExp != null)
                    //{
                    //    // It is recommended that applications respond to the errors listed below 
                    //    //    by explicitly issuing a ROLLBACK command.
                    //    // TODO: This rollback failsafe should be localized to all throw sites.
                    //    switch (sqlExp.Result)
                    //    {
                    //        case Result.IOError:
                    //        case Result.Full:
                    //        case Result.Busy:
                    //        case Result.NoMem:
                    //        case Result.Interrupt:
                    conn.RollbackTo(null, true);
                    //            break;
                    //    }
                    //}
                    //else
                    //{
                    // Call decrement and not VolatileWrite in case we've already 
                    //    created a transaction point in SaveTransactionPoint since the catch.
                    Interlocked.Decrement(ref _transactionDepth);
                    //}

                    throw;
                }
            }
            else
            {
                // Calling BeginTransaction on an already open transaction is invalid
                throw new InvalidOperationException("Cannot begin a transaction while already in a transaction.");
            }
        }

        /// <summary>
        ///     Creates a savepoint in the database at the current point in the transaction timeline.
        ///     Begins a new transaction if one is not in progress.
        ///     Call <see cref="RollbackTo" /> to undo transactions since the returned savepoint.
        ///     Call <see cref="Release" /> to commit transactions after the savepoint returned here.
        ///     Call <see cref="Commit" /> to end the transaction, committing all changes.
        /// </summary>
        /// <returns>A string naming the savepoint.</returns>
        public static string SaveTransactionPoint(this IDbConnection conn)
        {
            int depth = Interlocked.Increment(ref _transactionDepth) - 1;
            string retVal = "S" + _rand.Next(short.MaxValue) + "D" + depth;

            try
            {
                conn.Execute("savepoint " + retVal);
            }
            catch (Exception ex)
            {
                var sqlExp = ex as Exception;
                //if (sqlExp != null)
                //{
                //    // It is recommended that applications respond to the errors listed below 
                //    //    by explicitly issuing a ROLLBACK command.
                //    // TODO: This rollback failsafe should be localized to all throw sites.
                //    switch (sqlExp.Result)
                //    {
                //        case Result.IOError:
                //        case Result.Full:
                //        case Result.Busy:
                //        case Result.NoMem:
                //        case Result.Interrupt:
                conn.RollbackTo(null, true);
                //            break;
                //    }
                //}
                //else
                //{
                Interlocked.Decrement(ref _transactionDepth);
                //}

                throw;
            }

            return retVal;
        }

        /// <summary>
        ///     Rolls back the transaction that was begun by <see cref="BeginTransaction" /> or <see cref="SaveTransactionPoint" />
        ///     .
        /// </summary>
        public static void Rollback(this IDbConnection conn)
        {
            conn.RollbackTo(null, false);
        }

        /// <summary>
        ///     Rolls back the savepoint created by <see cref="BeginTransaction" /> or SaveTransactionPoint.
        /// </summary>
        /// <param name="savepoint">
        ///     The name of the savepoint to roll back to, as returned by <see cref="SaveTransactionPoint" />.
        ///     If savepoint is null or empty, this method is equivalent to a call to <see cref="Rollback" />
        /// </param>
        public static void RollbackTo(this IDbConnection conn, string savepoint)
        {
            conn.RollbackTo(savepoint, false);
        }

        /// <summary>
        ///     Rolls back the transaction that was begun by <see cref="BeginTransaction" />.
        /// </summary>
        /// <param name="savepoint">the savepoint name/key</param>
        /// <param name="noThrow">true to avoid throwing exceptions, false otherwise</param>
        private static void RollbackTo(this IDbConnection conn, string savepoint, bool noThrow)
        {
            // Rolling back without a TO clause rolls backs all transactions 
            //    and leaves the transaction stack empty.   
            try
            {
                if (String.IsNullOrEmpty(savepoint))
                {
                    if (Interlocked.Exchange(ref _transactionDepth, 0) > 0)
                    {
                        conn.Execute("rollback");
                    }
                }
                else
                {
                    conn.DoSavePointExecute(savepoint, "rollback to ");
                }
            }
            catch //(Exception ex)
            {
                if (!noThrow)
                {
                    throw;
                }
            }
            // No need to rollback if there are no transactions open.
        }

        /// <summary>
        ///     Releases a savepoint returned from <see cref="SaveTransactionPoint" />.  Releasing a savepoint
        ///     makes changes since that savepoint permanent if the savepoint began the transaction,
        ///     or otherwise the changes are permanent pending a call to <see cref="Commit" />.
        ///     The RELEASE command is like a COMMIT for a SAVEPOINT.
        /// </summary>
        /// <param name="savepoint">
        ///     The name of the savepoint to release.  The string should be the result of a call to
        ///     <see cref="SaveTransactionPoint" />
        /// </param>
        public static void Release(this IDbConnection conn, string savepoint)
        {
            conn.DoSavePointExecute(savepoint, "release ");
        }

        private static void DoSavePointExecute(this IDbConnection conn, string savepoint, string cmd)
        {
            // Validate the savepoint
            int firstLen = savepoint.IndexOf('D');
            if (firstLen >= 2 && savepoint.Length > firstLen + 1)
            {
                int depth;
                if (Int32.TryParse(savepoint.Substring(firstLen + 1), out depth))
                {
                    // TODO: Mild race here, but inescapable without locking almost everywhere.
                    if (0 <= depth && depth < _transactionDepth)
                    {
                        //Platform.VolatileService.Write(ref _transactionDepth, depth);
                        conn.Execute(cmd + savepoint);
                        return;
                    }
                }
            }

            throw new ArgumentException(
                "savePoint is not valid, and should be the result of a call to SaveTransactionPoint.", "savePoint");
        }

        /// <summary>
        ///     Commits the transaction that was begun by <see cref="BeginTransaction" />.
        /// </summary>
        public static void Commit(this IDbConnection conn)
        {
            if (Interlocked.Exchange(ref _transactionDepth, 0) != 0)
            {
                conn.Execute("commit");
            }
            // Do nothing on a commit with no open transaction
        }

        /// <summary>
        ///     Executes
        ///     <paramref name="action" />
        ///     within a (possibly nested) transaction by wrapping it in a SAVEPOINT. If an
        ///     exception occurs the whole transaction is rolled back, not just the current savepoint. The exception
        ///     is rethrown.
        /// </summary>
        /// <param name="action">
        ///     The <see cref="Action" /> to perform within a transaction.
        ///     <paramref name="action" />
        ///     can contain any number
        ///     of operations on the connection but should never call <see cref="BeginTransaction" /> or
        ///     <see cref="Commit" />.
        /// </param>
        public static void RunInTransaction(this IDbConnection conn, Action action)
        {
            try
            {
                string savePoint = conn.SaveTransactionPoint();
                action();
                conn.Release(savePoint);
            }
            catch (Exception)
            {
                conn.Rollback();
                throw;
            }
        }


        /// <summary>
        ///     Inserts all specified objects.
        /// </summary>
        /// <param name="objects">
        ///     An <see cref="IEnumerable" /> of the objects to insert.
        /// </param>
        /// <returns>
        ///     The number of rows added to the table.
        /// </returns>
        public static int InsertAll(this IDbConnection conn, IEnumerable objects)
        {
            int c = 0;
            conn.RunInTransaction(() =>
            {
                foreach (object r in objects)
                {
                    c += conn.Insert(r);
                }
            });
            return c;
        }

        /// <summary>
        ///     Inserts all specified objects.
        /// </summary>
        /// <param name="objects">
        ///     An <see cref="IEnumerable" /> of the objects to insert.
        /// </param>
        /// <param name="extra">
        ///     Literal SQL code that gets placed into the command. INSERT {extra} INTO ...
        /// </param>
        /// <returns>
        ///     The number of rows added to the table.
        /// </returns>
        public static int InsertAll(this IDbConnection conn, IEnumerable objects, string extra)
        {
            int c = 0;
            conn.RunInTransaction(() =>
            {
                foreach (object r in objects)
                {
                    c += conn.Insert(r, extra);
                }
            });
            return c;
        }

        /// <summary>
        ///     Inserts all specified objects.
        /// </summary>
        /// <param name="objects">
        ///     An <see cref="IEnumerable" /> of the objects to insert.
        /// </param>
        /// <param name="objType">
        ///     The type of object to insert.
        /// </param>
        /// <returns>
        ///     The number of rows added to the table.
        /// </returns>
        public static int InsertAll(this IDbConnection conn, IEnumerable objects, Type objType)
        {
            int c = 0;
            conn.RunInTransaction(() =>
            {
                foreach (object r in objects)
                {
                    c += conn.Insert(r, objType);
                }
            });
            return c;
        }


        /// <summary>
        ///     Inserts the given object and retrieves its
        ///     auto incremented primary key if it has one.
        /// </summary>
        /// <param name="obj">
        ///     The object to insert.
        /// </param>
        /// <returns>
        ///     The number of rows added to the table.
        /// </returns>
        public static int Insert(this IDbConnection conn, object obj)
        {
            if (obj == null)
            {
                return 0;
            }
            return conn.Insert(obj, "", obj.GetType());
        }

        /// <summary>
        ///     Inserts the given object and retrieves its
        ///     auto incremented primary key if it has one.
        ///     If a UNIQUE constraint violation occurs with
        ///     some pre-existing object, this function deletes
        ///     the old object.
        /// </summary>
        /// <param name="obj">
        ///     The object to insert.
        /// </param>
        /// <returns>
        ///     The number of rows modified.
        /// </returns>
        public static int InsertOrReplace(this IDbConnection conn, object obj)
        {
            if (obj == null)
            {
                return 0;
            }
            return conn.Insert(obj, "OR REPLACE", obj.GetType());
        }

        /// <summary>
        ///     Inserts all specified objects.
        ///     For each insertion, if a UNIQUE 
        ///     constraint violation occurs with
        ///     some pre-existing object, this function
        ///     deletes the old object.
        /// </summary>
        /// <param name="objects">
        ///     An <see cref="IEnumerable" /> of the objects to insert or replace.
        /// </param>
        /// <returns>
        ///     The total number of rows modified.
        /// </returns>
        public static int InsertOrReplaceAll(this IDbConnection conn, IEnumerable objects)
        {
            int c = 0;
            conn.RunInTransaction(() =>
            {
                foreach (object r in objects)
                {
                    c += conn.InsertOrReplace(r);
                }
            });
            return c;
        }

        /// <summary>
        ///     Inserts the given object and retrieves its
        ///     auto incremented primary key if it has one.
        /// </summary>
        /// <param name="obj">
        ///     The object to insert.
        /// </param>
        /// <param name="objType">
        ///     The type of object to insert.
        /// </param>
        /// <returns>
        ///     The number of rows added to the table.
        /// </returns>
        public static int Insert(this IDbConnection conn, object obj, Type objType)
        {
            return conn.Insert(obj, "", objType);
        }

        /// <summary>
        ///     Inserts the given object and retrieves its
        ///     auto incremented primary key if it has one.
        ///     If a UNIQUE constraint violation occurs with
        ///     some pre-existing object, this function deletes
        ///     the old object.
        /// </summary>
        /// <param name="obj">
        ///     The object to insert.
        /// </param>
        /// <param name="objType">
        ///     The type of object to insert.
        /// </param>
        /// <returns>
        ///     The number of rows modified.
        /// </returns>
        public static int InsertOrReplace(this IDbConnection conn, object obj, Type objType)
        {
            return conn.Insert(obj, "OR REPLACE", objType);
        }

        /// <summary>
        ///     Inserts all specified objects.
        ///     For each insertion, if a UNIQUE 
        ///     constraint violation occurs with
        ///     some pre-existing object, this function
        ///     deletes the old object.
        /// </summary>
        /// <param name="objects">
        ///     An <see cref="IEnumerable" /> of the objects to insert or replace.
        /// </param>
        /// <param name="objType">
        ///     The type of objects to insert or replace.
        /// </param>
        /// <returns>
        ///     The total number of rows modified.
        /// </returns>
        public static int InsertOrReplaceAll(this IDbConnection conn, IEnumerable objects, Type objType)
        {
            int c = 0;
            conn.RunInTransaction(() =>
            {
                foreach (object r in objects)
                {
                    c += conn.InsertOrReplace(r, objType);
                }
            });
            return c;
        }


        /// <summary>
        ///     Inserts the given object and retrieves its
        ///     auto incremented primary key if it has one.
        /// </summary>
        /// <param name="obj">
        ///     The object to insert.
        /// </param>
        /// <param name="extra">
        ///     Literal SQL code that gets placed into the command. INSERT {extra} INTO ...
        /// </param>
        /// <param name="objType">
        ///     The type of object to insert.
        /// </param>
        /// <returns>
        ///     The number of rows added to the table.
        /// </returns>
        public static int Insert(this IDbConnection conn, object obj, string extra, Type objType)
        {
            if (obj == null || objType == null) return 0;

            TableMapping map = conn.GetMapping(objType);

            if (map.PK != null && map.PK.IsAutoGuid)
            {
                PropertyInfo prop = objType.GetProperty(map.PK.PropertyName);
                if (prop != null)
                {
                    if (prop.GetValue(obj, null).Equals(Guid.Empty))
                    {
                        prop.SetValue(obj, Guid.NewGuid(), null);
                    }
                }
            }

            bool replacing = string.Compare(extra, "OR REPLACE", StringComparison.OrdinalIgnoreCase) == 0;

            TableMapping.Column[] cols = replacing ? map.InsertOrReplaceColumns : map.InsertColumns;
            object[] vals = new object[cols.Length];
            List<IDbDataParameter> _params = new List<IDbDataParameter>();
            int i = 0;
            while (i < vals.Length)
            {
                try
                {
                    vals[i] = cols[i].GetValue(obj);

                    _params.Add(conn.CreateParameter(cols[i].Name, vals[i]));
                    System.Math.Max(System.Threading.Interlocked.Increment(ref i), i - 1);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            }

            IDbCommand insertCmd = (IDbCommand)map.GetInsertCommand(conn, extra);
            foreach (IDbDataParameter Item in _params)
            {
                if (Item.Value == null || Item.Value.ToString() == "NULL")
                {
                    insertCmd.CommandText = insertCmd.CommandText.Replace(Item.ParameterName, "NULL");
                }
                else
                {
                    insertCmd.Parameters.Add(Item);
                }
            }

            int count;

            try
            {
                insertCmd.Connection.Open();
                count = insertCmd.ExecuteNonQuery();
                if (map.HasAutoIncPK)
                {
                    var id = conn.LastInsertRowPK(map);
                    map.SetAutoIncPK(obj, id);
                }

            }
            catch (Exception ex)
            {
                //If Platform.SQLiteApi.ExtendedErrCode(Me.Handle) = ExtendedResult.ConstraintNotNull Then
                //    Throw NotNullConstraintViolationException.[New](ex.Result, ex.Message, map, obj)
                //End If
                throw ex;
            }
            finally
            {
                insertCmd.Connection.Close();
            }

            return count;
        }

        /// <summary>
        ///     Inserts the given object and retrieves its
        ///     auto incremented primary key if it has one.
        /// </summary>
        /// <param name="obj">
        ///     The object to insert.
        /// </param>
        /// <param name="extra">
        ///     Literal SQL code that gets placed into the command. INSERT {extra} INTO ...
        /// </param>
        /// <returns>
        ///     The number of rows added to the table.
        /// </returns>
        public static int Insert(this IDbConnection conn, object obj, string extra)
        {
            if (obj == null)
            {
                return 0;
            }
            return conn.Insert(obj, extra, obj.GetType());
        }


        /// <summary>
        ///     Updates all of the columns of a table using the specified object
        ///     except for its primary key.
        ///     The object is required to have a primary key.
        /// </summary>
        /// <param name="obj">
        ///     The object to update. It must have a primary key designated using the PrimaryKeyAttribute.
        /// </param>
        /// <param name="objType">
        ///     The type of object to insert.
        /// </param>
        /// <returns>
        ///     The number of rows updated.
        /// </returns>
        public static int Update(this IDbConnection conn, object obj, Type objType)
        {
            int rowsAffected = 0;
            if (obj == null || objType == null) { return 0; }

            TableMapping map = conn.GetMapping(objType);

            TableMapping.Column pk = map.PK;

            if (pk == null)
            {
                throw new NotSupportedException("Cannot update " + map.TableName + ": it has no PK");
            }

            IEnumerable<TableMapping.Column> cols = from p in map.Columns
                                                    where p != pk
                                                    select p;
            IEnumerable<object> vals = from c in cols
                                       select c.GetValue(obj);
            var ps = new List<object>(vals);
            ps.Add(pk.GetValue(obj));

            var keyvalue = pk.GetValue(obj);
            //if (keyvalue.GetType() == typeof(string))
            //    keyvalue = string.Format("'{0}'", keyvalue.ToString());

            string q = string.Format("UPDATE {0} SET {1} WHERE {2} = @param" + (cols.ToList().Count + 1).ToString(),
                                        map.TableName,
                                        string.Join(",", (from c in cols select "[" + c.Name + "] = @param" + (cols.ToList().IndexOf(c) + 1)).ToArray()),
                                        pk.Name);
            try
            {
                rowsAffected = conn.Execute(q, ps.ToArray());
            }
            catch (Exception ex)
            {
                //if (ex.Result == Result.Constraint && Platform.SQLiteApi.ExtendedErrCode(Handle) == ExtendedResult.ConstraintNotNull)
                //{
                //    throw NotNullConstraintViolationException.New(ex, map, obj);
                //}

                throw ex;
            }

            return rowsAffected;
        }

        /// <summary>
        ///     Updates all of the columns of a table using the specified object
        ///     except for its primary key.
        ///     The object is required to have a primary key.
        /// </summary>
        /// <param name="obj">
        ///     The object to update. It must have a primary key designated using the PrimaryKeyAttribute.
        /// </param>
        /// <returns>
        ///     The number of rows updated.
        /// </returns>
        public static int Update(this IDbConnection conn, object obj)
        {
            if (obj == null)
            {
                return 0;
            }
            return conn.Update(obj, obj.GetType());
        }

        /// <summary>
        ///     Updates all specified objects.
        /// </summary>
        /// <param name="objects">
        ///     An <see cref="IEnumerable" /> of the objects to insert.
        /// </param>
        /// <returns>
        ///     The number of rows modified.
        /// </returns>
        public static int UpdateAll(this IDbConnection conn, IEnumerable objects)
        {
            int c = 0;
            conn.RunInTransaction(() =>
            {
                foreach (object r in objects)
                {
                    c += conn.Update(r);
                }
            });
            return c;
        }


        /// <summary>
        ///     Deletes the given object from the database using its primary key.
        /// </summary>
        /// <param name="objectToDelete">
        ///     The object to delete. It must have a primary key designated using the PrimaryKeyAttribute.
        /// </param>
        /// <returns>
        ///     The number of rows deleted.
        /// </returns>
        public static int Delete(this IDbConnection conn, object objectToDelete)
        {
            TableMapping map = conn.GetMapping(objectToDelete.GetType());
            TableMapping.Column pk = map.PK;
            if (pk == null)
            {
                throw new NotSupportedException("Cannot delete " + map.TableName + ": it has no PK");
            }
            string q = string.Format("DELETE FROM {0} WHERE {1} = @param1", map.TableName, pk.Name);
            return conn.Execute(q, pk.GetValue(objectToDelete));
        }

        /// <summary>
        ///     Deletes the object with the specified primary key.
        /// </summary>
        /// <param name="primaryKey">
        ///     The primary key of the object to delete.
        /// </param>
        /// <returns>
        ///     The number of objects deleted.
        /// </returns>
        /// <typeparam name='T'>
        ///     The type of object.
        /// </typeparam>
        public static int Delete<T>(this IDbConnection conn, object primaryKey)
        {
            TableMapping map = conn.GetMapping(typeof(T));
            TableMapping.Column pk = map.PK;
            if (pk == null)
            {
                throw new NotSupportedException("Cannot delete " + map.TableName + ": it has no PK");
            }
            string q = string.Format("DELETE FROM {0} WHERE {1} = @param1", map.TableName, pk.Name);
            return conn.Execute(q, primaryKey);
        }

        /// <summary>
        ///     Deletes all the objects from the specified table.
        ///     WARNING WARNING: Let me repeat. It deletes ALL the objects from the
        ///     specified table. Do you really want to do that?
        /// </summary>
        /// <returns>
        ///     The number of objects deleted.
        /// </returns>
        /// <typeparam name='T'>
        ///     The type of objects to delete.
        /// </typeparam>
        public static int DeleteAll<T>(this IDbConnection conn)
        {
            TableMapping map = conn.GetMapping(typeof(T));
            string query = string.Format("DELETE FROM {0}", map.TableName);
            return conn.Execute(query);
        }

        #endregion

        public static void CopyTo<T>(this IDbConnection conn, IDbConnection target, CreateFlags createFlags = CreateFlags.AllImplicit)
        {
            TableMapping map = conn.GetMapping(typeof(T));
            TableQuery<T> table = new TableQuery<T>(conn);

            target.CreateTable<T>(createFlags);
            foreach (T item in table)
            {
                target.Insert(item);
            }


        }

        #region IDbCommand

        /// <summary>
        ///     Creates a SQLiteCommand given the command text (SQL) with arguments. Place a '?'
        ///     in the command text for each of the arguments and then executes that command.
        ///     It returns each row of the result using the mapping automatically generated for
        ///     the given type.
        /// </summary>
        /// <param name="query">
        ///     The fully escaped SQL.
        /// </param>
        /// <param name="args">
        ///     Arguments to substitute for the occurences of '?' in the query.
        /// </param>
        /// <returns>
        ///     An enumerable with one result for each row returned by the query.
        ///     The enumerator will call sqlite3_step on each call to MoveNext, so the database
        ///     connection must remain open for the lifetime of the enumerator.
        /// </returns>
        public static IEnumerable<T> ExecuteDeferredQuery<T>(this IDbCommand cmd, TableMapping map)
        {

            List<T> _return = new List<T>();
            IDataReader reader = null;

            try
            {
                cmd.Connection.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    T obj = (T)Activator.CreateInstance(map.MappedType);


                    foreach (TableMapping.Column Item in map.Columns)
                    {
                        Type colType = Item.ColumnType;
                        object val = reader[Item.Name];
#if DEBUG
                        try
                        {
#endif
                        Item.SetValue(obj, val);
#if DEBUG
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
#endif
                    }
                    //OnInstanceCreated(obj)
                    _return.Add(obj);

                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
                throw ex;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                cmd.Connection.Close();
            }

            return _return.ToArray();

        }

        /// <summary>
        ///     Creates a SQLiteCommand given the command text (SQL) with arguments. Place a '?'
        ///     in the command text for each of the arguments and then executes that command.
        ///     It returns each row of the result using the mapping automatically generated for
        ///     the given type.
        /// </summary>
        /// <param name="query">
        ///     The fully escaped SQL.
        /// </param>
        /// <param name="args">
        ///     Arguments to substitute for the occurences of '?' in the query.
        /// </param>
        /// <returns>
        ///     An enumerable with one result for each row returned by the query.
        ///     The enumerator will call sqlite3_step on each call to MoveNext, so the database
        ///     connection must remain open for the lifetime of the enumerator.
        /// </returns>
        public static IEnumerable<T> ExecuteDeferredQuery<T>(this IDbCommand cmd)
        {
            return ExecuteDeferredQuery<T>(cmd, cmd.Connection.GetMapping(typeof(T)));
        }

        public static List<T> ExecuteQuery<T>(this IDbCommand cmd)
        {
            return ExecuteDeferredQuery<T>(cmd, cmd.Connection.GetMapping(typeof(T))).ToList();
        }
        public static List<T> ExecuteQuery<T>(this IDbCommand cmd, TableMapping map)
        {
            return ExecuteDeferredQuery<T>(cmd, map).ToList();
        }


        #endregion

        private static object Value(this object obj)
        {
            if (obj != null && (obj.GetType() == typeof(System.DateTime)))
            {
                if ((System.DateTime)obj < new System.DateTime(1753, 1, 1)) { obj = null; }
            }

            return obj;
        }

        #region DataSets && DataTables

        public static bool HasTables(this DataSet ds)
        {
            return ds != null && ds.Tables.Count > 0;
        }

        public static bool HasRows(this DataTable dt)
        {
            return dt != null && dt.Rows.Count > 0;
        }

        public static bool HasTablesWithRows(this DataSet ds)
        {
            return ds != null && ds.Tables.OfType<DataTable>().Count(item => item.HasRows()) > 0;
        }

        #endregion

    }
}
