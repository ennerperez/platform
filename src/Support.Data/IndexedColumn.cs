using System;

namespace Platform.Support.Data
{
    [Obsolete("Use EF instead")]
    public struct IndexedColumn
    {
        public string ColumnName { get; set; }
        public int Order { get; set; }
    }
}