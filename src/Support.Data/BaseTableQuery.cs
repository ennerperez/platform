using System;

namespace Platform.Support.Data
{
    [Obsolete("Use EF instead")]
    public abstract class BaseTableQuery
    {
        protected class Ordering
        {
            public string ColumnName { get; set; }
            public bool Ascending { get; set; }
        }
    }
}