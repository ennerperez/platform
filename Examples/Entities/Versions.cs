using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if PORTABLE
using SQLite.Net.Attributes;
#else
using Support.Data;
using Support.Data.Attributes;
#endif

namespace Examples.Entities
{

    [Table("versions")]
    public class Versions : Model.Entity
    {

        [Column("id"), PrimaryKey, AutoIncrement, Unique]
        public override int Id { get; set; }

        [Column("software")]
        public int Software { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("version")]
        public string Version { get; set; }

        public override void Save()
        {
            if (this.Software != 0)
            {
                if (this.Id != 0)
                {
                    Program.Connection.Update(this);
                }
                else
                {
                    Program.Connection.Insert(this);
                }
            }
        }

    }
}
