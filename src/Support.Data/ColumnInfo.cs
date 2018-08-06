using Platform.Support.Data.Attributes;
using System;

namespace Platform.Support.Data
{
    [Obsolete("Use EF instead")]
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