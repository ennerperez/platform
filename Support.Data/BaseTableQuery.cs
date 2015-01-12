using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support.Data
{
    public abstract class BaseTableQuery
    {
        protected class Ordering
        {
            public string ColumnName { get; set; }
            public bool Ascending { get; set; }
        }
    }
}
