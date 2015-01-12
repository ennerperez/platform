using Support.Data.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support.Data
{
    public struct ColumnInfo
    {
        [Column("name")]
        public string Name { get; set; }
        public int NotNull { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
