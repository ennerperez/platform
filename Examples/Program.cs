using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if !PORTABLE
using Support.Data;
#else
using SQLite.Net.Interop;
#endif

namespace Examples
{
    public class Program
    {

#if !PORTABLE
        public static System.Data.Common.DbConnection Connection;
#else
        public static SQLite.Net.SQLiteConnection Connection;
#endif

        static void Main(string[] args)
        {

#if !PORTABLE
            
            FormDemo demo = new FormDemo();
                demo.ShowDialog();
            
            System.Data.SqlClient.SqlConnectionStringBuilder csb = new System.Data.SqlClient.SqlConnectionStringBuilder();
            csb.DataSource = "(localdb)\\CAD";
            csb.InitialCatalog = "software";

            Connection = new System.Data.SqlClient.SqlConnection(csb.ConnectionString);
#else
            System.Data.SQLite.SQLiteConnectionStringBuilder csb = new System.Data.SQLite.SQLiteConnectionStringBuilder();
            csb.DataSource = "database.db";

            if (System.IO.File.Exists(csb.DataSource)) System.IO.File.Delete(csb.DataSource);
            Connection = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.Win32.SQLitePlatformWin32(), csb.DataSource);
#endif

#if !PORTABLE

            Connection.DropDatabase();
            Connection.CreateDatabase();

#endif

            Connection.CreateTable<Entities.Software>(CreateFlags.AllImplicit);
            Connection.CreateTable<Entities.Versions>(CreateFlags.AllImplicit);

            Entities.Software VisualStudio = new Entities.Software() { Name = "Visual Studio" };
            VisualStudio.Versions = new List<Entities.Versions>();
            (VisualStudio.Versions as List<Entities.Versions>).Add(new Entities.Versions() { Name = "2005", Release = new DateTime(2004, 1, 1), Version = "8" });
            (VisualStudio.Versions as List<Entities.Versions>).Add(new Entities.Versions() { Name = "2008", Release = new DateTime(2007, 1, 1), Version = "9" });
            (VisualStudio.Versions as List<Entities.Versions>).Add(new Entities.Versions() { Name = "2010", Release = new DateTime(2010, 1, 1), Version = "10" });
            (VisualStudio.Versions as List<Entities.Versions>).Add(new Entities.Versions() { Name = "2012", Release = new DateTime(2011, 1, 1), Version = "11" });
            (VisualStudio.Versions as List<Entities.Versions>).Add(new Entities.Versions() { Name = "2013", Release = new DateTime(2013, 1, 1), Version = "12" });

            VisualStudio.Save();

            Console.ReadKey();

        }
    }
}
