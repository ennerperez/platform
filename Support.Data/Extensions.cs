using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Reflection;
using Support;

namespace Support.Data
{
    public static class Extensions
    {

        #region Reflection

        private static Assembly _assembly;
        public static Assembly DataEngineAssembly(IDbConnection conn)
        {
            if (_assembly == null) _assembly = conn.GetAssembly();
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
            System.Reflection.Assembly _return = null;
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
                    //_return = System.Reflection.Assembly.LoadWithPartialName(_asm)
                    _return = System.Reflection.Assembly.Load("System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
                    return _return;
            }

            _file = System.IO.Path.Combine(new string[] { Helpers.DirectoryPath(), _asm });

            if (!string.IsNullOrEmpty(_file) && System.IO.File.Exists(_file))
            {
                _return = System.Reflection.Assembly.LoadFile(_file);
            }
            else
            {
                if (System.Reflection.Assembly.GetEntryAssembly() != null)
                {
                    _file = System.IO.Path.Combine(new string[] { Helpers.DirectoryPath(), _asm });
                    if (System.IO.File.Exists(_file))
                    {
                        _return = System.Reflection.Assembly.LoadFile(_file);
                    }
                }
            }

            return _return;

        }

        #endregion

        #region Builders

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

            IDbDataParameter _return = (IDbDataParameter)conn.CreateObject(_type, new object[] { name, value.IsNull("NULL") });
            if (value != null)
            {
                _return.DbType = value.GetType().GetDbType();

                if (object.ReferenceEquals(value.GetType(), typeof(string)))
                {
                    _return.Size = value.ToString().Length;
                }
                else
                {
                    try
                    {
                        _return.Size = (int)value.GetType().GetMaxValue();
                        //CObj(value.GetType).MaxValue
                    }
                    catch
                    {
                    }
                }
            }
            else
            {
                _return.DbType = DbType.String;
                _return.Size = 4;
            }

            return _return;
        }
        public static IDbDataParameter CreateParameter(this IDbConnection conn, object[] args)
        {
            return conn.CreateParameter(args[0].ToString(), args[1]);
        }

        public static IDbCommand CreateCommand(this IDbConnection conn, string cmdText, params System.Data.IDbDataParameter[] args)
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
        public static IDbCommand CreateCommand(this IDbConnection conn, string cmdText, params object[] args)
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
                    _params.Add(conn.CreateParameter("param" + i + 1, args[i]));
                }
            }
            //_params = (From item As Object In args Select this.CreateParameter("param" & CStr(args.IndexOf(item) + 1), item)).ToList
            return conn.CreateCommand(cmdText, _params.ToArray());
        }

        #endregion

        #region Mappings

        private static Dictionary<string, TableMapping> _mappings;

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

        #endregion

        #region  Types

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

        #region  Command

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
                        try
                        {
                            Item.SetValue(obj, val);
#if DEBUG
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
#endif
                        }
                    }
                    //OnInstanceCreated(obj)
                    _return.Add(obj);

                }

#if DEBUG
            }
            catch (Exception ex)
            {
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

        public static T ExecuteScalar<T>(this IDbCommand cmd)
        {
            object _return = cmd.ExecuteScalar();
            return (T)_return;
        }

        #endregion


    }
}
