using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if PORTABLE
using SQLite.Net.Attributes;
#else
using Platform.Support.Data;
using Platform.Support.Data.Attributes;
#endif

namespace Examples.Entities
{
    [Table("software")]
    public class Software : Platform.Model.Entity<int>, Platform.Model.MVC.IModel
    {

        [Column("id"), PrimaryKey, AutoIncrement, Unique]
        public override int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }
                
        [Ignore]
        public IEnumerable<Versions> Versions { get; set; }

        public override void Save()
        {
            if (this.Id != 0)
            {
                Program.Connection.Update(this);
            }
            else
            {
                Program.Connection.Insert(this);
            }

            if (this.Id != 0)
            {
                foreach (Versions item in this.Versions)
                {
                    item.Software = this.Id;
                    item.Save();
                }
            }

        }

    }
}
