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
            System.Data.SQLite.SQLiteConnectionStringBuilder csb = new System.Data.SQLite.SQLiteConnectionStringBuilder();
            csb.DataSource = "database.db";

            if (System.IO.File.Exists(csb.DataSource)) System.IO.File.Delete(csb.DataSource);

#if !PORTABLE
            Connection = new System.Data.SQLite.SQLiteConnection(csb.ConnectionString);
#else
            Connection = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.Win32.SQLitePlatformWin32(), csb.DataSource);
#endif

            Connection.CreateTable<Entities.Software>(CreateFlags.AllImplicit);
            Connection.CreateTable<Entities.Versions>(CreateFlags.AllImplicit);

            Entities.Software VisualStudio = new Entities.Software() { Name = "Visual Studio" };
            VisualStudio.Versions = new List<Entities.Versions>();
            (VisualStudio.Versions as List<Entities.Versions>).Add(new Entities.Versions() { Name = "2005", Version = "8" });
            (VisualStudio.Versions as List<Entities.Versions>).Add(new Entities.Versions() { Name = "2008", Version = "9" });
            (VisualStudio.Versions as List<Entities.Versions>).Add(new Entities.Versions() { Name = "2010", Version = "10" });
            (VisualStudio.Versions as List<Entities.Versions>).Add(new Entities.Versions() { Name = "2012", Version = "11" });
            (VisualStudio.Versions as List<Entities.Versions>).Add(new Entities.Versions() { Name = "2013", Version = "12" });

            VisualStudio.Save();

            Console.ReadKey();

        }
    }
}
