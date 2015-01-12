using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLengthAttribute : Attribute
    {
        public MaxLengthAttribute(int length)
        {
            Value = length;
        }

        public int Value { get; set; }
    }
}
