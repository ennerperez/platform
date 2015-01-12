using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {

        public string Name { get; set; }

        TableAttribute(string name)
        {
            this.Name = name;
        }

    }
}
