using Platform.Support.Data;
using System;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace Sample.DataModel
{
    public class Program
    {
        public static System.Data.IDbConnection SourceConnection;
        public static System.Data.IDbConnection TargetConnection;

        private static void Main(string[] args)
        {
            System.Data.Common.DbConnectionStringBuilder csb;
            if (System.Data.SqlLocalDb.SqlLocalDbApi.IsLocalDBInstalled())
            {
                csb = new SqlConnectionStringBuilder();
                (csb as SqlConnectionStringBuilder).DataSource = @"(localdb)\MSSQLLocalDB";
                (csb as SqlConnectionStringBuilder).IntegratedSecurity = true;
                (csb as SqlConnectionStringBuilder).Pooling = true;
                (csb as SqlConnectionStringBuilder).InitialCatalog = "database";

                SourceConnection = new SqlConnection(csb.ToString());
            }
            else
            {
                csb = new SQLiteConnectionStringBuilder();
                (csb as SQLiteConnectionStringBuilder).DataSource = "database.db";
                (csb as SQLiteConnectionStringBuilder).Pooling = true;
                (csb as SQLiteConnectionStringBuilder).SyncMode = SynchronizationModes.Full;
                (csb as SQLiteConnectionStringBuilder).Version = 3;
                (csb as SQLiteConnectionStringBuilder).DateTimeFormat = SQLiteDateFormats.ISO8601;

                SourceConnection = new SQLiteConnection(csb.ToString());
            }

            SourceConnection.DropDatabase();
            SourceConnection.CreateDatabase();

            GenerateData();

            CopyDatabase();

            Console.ReadKey();
        }

        private static void CopyDatabase()
        {
            var csb = new SQLiteConnectionStringBuilder();
            csb.DataSource = "database2.db";
            csb.Pooling = true;
            csb.SyncMode = SynchronizationModes.Full;
            csb.Version = 3;
            csb.DateTimeFormat = SQLiteDateFormats.ISO8601;

            TargetConnection = new SQLiteConnection(csb.ToString());

            TargetConnection.DropDatabase();
            SourceConnection.CopyTo<Models.LibraryModel>(TargetConnection, CreateFlags.AllImplicit);
        }

        private static void GenerateData()
        {
            SourceConnection.CreateTable<Models.LibraryModel>(CreateFlags.AllImplicit);

            var item1 = new Models.LibraryModel();
            item1.Id = Guid.NewGuid();
            item1.Name = "Platform Data Sample";
            item1.Version = new Version(1, 0, 0, 0);

            SourceConnection.Insert(item1);
        }
    }
}