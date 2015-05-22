using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SchemaAttribute : Attribute
    {

        public string Name { get; set; }

        public SchemaAttribute(string name)
        {
            this.Name = name;
        }

    }
}
