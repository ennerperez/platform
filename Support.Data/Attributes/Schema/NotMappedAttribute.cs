using System;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Support.Data.Attributes
{
    /// <summary>
	/// Denotes that a property or class should be excluded from database mapping.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false), SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes")]
    public class NotMappedAttribute : Attribute
    {
    }

    #region Legacy

    /// <summary>
	/// Denotes that a property or class should be excluded from database mapping.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false), SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes")]
    public class IgnoreAttribute : NotMappedAttribute
    {
    }

    #endregion

}