using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support.Data
{
    public struct IndexInfo
    {
        public List<IndexedColumn> Columns { get; set; }
        public string IndexName { get; set; }
        public string SchemaName { get; set; }
        public string TableName { get; set; }
        public bool Unique { get; set; }

        public bool NonClustered { get; set; }
    }
}
