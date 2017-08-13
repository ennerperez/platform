using System;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Support.Data.Attributes
{
    #region Legacy

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true), SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes"), SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    public class UniqueAttribute : IndexedAttribute
    {
        public bool Unique
        {
            get { return IsUnique; }
            set { IsUnique = value; }
        }
    }

    #endregion Legacy
}