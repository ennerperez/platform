using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Platform.Support.Data.Attributes
{
    /// <summary>
    /// Denotes a property used as a foreign key in a relationship.
    /// The annotation may be placed on the foreign key property and specify the associated navigation property name, 
    /// or placed on a navigation property and specify the associated foreign key name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false), SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments"), SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "We want users to be able to extend this class")]
    public class ForeignKeyAttribute : Attribute
    {
        private readonly string _name;

        /// <summary>
        /// If placed on a foreign key property, the name of the associated navigation property.
        /// If placed on a navigation property, the name of the associated foreign key(s).
        /// </summary>
        public string Name
        {
            get
            {
                return this._name;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ForeignKeyAttribute" /> class.
        /// </summary>
        /// <param name="name"> If placed on a foreign key property, the name of the associated navigation property. If placed on a navigation property, the name of the associated foreign key(s). If a navigation property has multiple foreign keys, a comma separated list should be supplied. </param>
        public ForeignKeyAttribute(string name)
        {
            Check.NotEmpty(name, "name");
            this._name = name;
        }
    }
}
