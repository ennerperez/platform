using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IndexedAttribute : Attribute
    {
        public IndexedAttribute()
        {
        }

        public IndexedAttribute(string name, int order)
        {
            this.Name = name;
            this.Order = order;
        }

        public string Name { get; set; }
        public int Order { get; set; }
        public virtual bool Unique { get; set; }
    }
}
