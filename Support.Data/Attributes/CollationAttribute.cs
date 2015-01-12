using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CollationAttribute : Attribute
    {
        public CollationAttribute(string collation)
        {
            Value = collation;
        }

        public string Value { get; set; }
    }
}
