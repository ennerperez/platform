using System;

namespace Platform.Support.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CollationAttribute : Attribute
    {
        public CollationAttribute(string collation)
        {
            this.Value = collation;
        }

        public string Value { get; private set; }
    }
}