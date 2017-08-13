using System;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Support.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false), SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes")]
    public class SchemaAttribute : Attribute
    {
        public string Name { get; set; }

        public SchemaAttribute(string name)
        {
            this.Name = name;
        }
    }
}