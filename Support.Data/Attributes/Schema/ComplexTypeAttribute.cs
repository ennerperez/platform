using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Platform.Support.Data.Attributes
{
    /// <summary>
	/// Denotes that the class is a complex type.
	/// Complex types are non-scalar properties of entity types that enable scalar properties to be organized within entities. 
	/// Complex types do not have keys and cannot be managed by the Entity Framework apart from the parent object.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false), SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes")]
    public class ComplexTypeAttribute : Attribute
    {
    }
}
