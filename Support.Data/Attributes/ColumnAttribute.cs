using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        public ColumnAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
