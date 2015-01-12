using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Support.Data
{
    public static class Extensions
    {

        private static System.Reflection.Assembly _DataEngineAssembly;
        public static System.Reflection.Assembly DataEngineAssembly(IDbConnection conn)
        {
            if (_DataEngineAssembly == null) _DataEngineAssembly = conn.GetAssembly();
            return _DataEngineAssembly;
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

        internal static System.Reflection.Assembly GetAssembly(this IDbConnection conn)
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





    }
}
