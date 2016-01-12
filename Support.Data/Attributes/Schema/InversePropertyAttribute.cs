using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Platform.Support.Data.Attributes
{
    /// <summary>
	/// Specifies the inverse of a navigation property that represents the other end of the same relationship.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false), SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments"), SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "We want users to be able to extend this class")]
    public class InversePropertyAttribute : Attribute
    {
        private readonly string _property;

        /// <summary>
        /// The navigation property representing the other end of the same relationship.
        /// </summary>
        public string Property
        {
            get
            {
                return this._property;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:InversePropertyAttribute" /> class.
        /// </summary>
        /// <param name="property"> The navigation property representing the other end of the same relationship. </param>
        public InversePropertyAttribute(string property)
        {
            Check.NotEmpty(property, "property");
            this._property = property;
        }
    }
}
