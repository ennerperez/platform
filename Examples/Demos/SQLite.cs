using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Support.Data;
using System.Collections;
using System.Windows.Forms;

namespace Examples.Demos
{
    public class SQLite
    {

        public static System.Data.Common.DbConnection Connection;

        public SQLite()
        {
        }

        public void Initialize()
        {
            System.Data.SQLite.SQLiteConnectionStringBuilder csb = new System.Data.SQLite.SQLiteConnectionStringBuilder();
            csb.DataSource = "database.db";
            if (System.IO.File.Exists(csb.DataSource)) System.IO.File.Delete(csb.DataSource);
            SQLite.Connection = new System.Data.SQLite.SQLiteConnection(csb.ToString());

            Program.Connection = SQLite.Connection;

            this.Generate();
        }

        private void Generate()
        {
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

        }


    }
}
