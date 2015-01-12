using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SchemaAttribute : Attribute
    {

        public string Name { get; set; }

        SchemaAttribute(string name)
        {
            this.Name = name;
        }

    }
}
