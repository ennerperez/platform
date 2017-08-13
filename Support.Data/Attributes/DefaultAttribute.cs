using System;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Support.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false), SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes")]
    public class DefaultAttribute : Attribute
    {
        public bool UseProperty { get; set; }
        public object Value { get; private set; }

        /// <summary>
        /// Used to set default value in database
        /// </summary>
        /// <param name="usePropertyValue">Will set default value to same as property. You would use proprty with backing field to set this</param>
        /// <param name="value">The value to set as default if usePropertyValue is false</param>
        public DefaultAttribute(bool usePropertyValue = true, object value = null)
        {
            this.Value = value;
        }
    }
}