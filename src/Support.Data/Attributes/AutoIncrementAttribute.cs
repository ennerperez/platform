using System;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Support.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false), SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes")]
    public class AutoIncrementAttribute : Attribute
    {
    }
}