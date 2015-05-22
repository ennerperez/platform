using System;

namespace Platform.Support.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLengthAttribute : Attribute
    {
        public MaxLengthAttribute(int length)
        {
            this.Value = length;
        }

        public int Value { get; private set; }
    }
}