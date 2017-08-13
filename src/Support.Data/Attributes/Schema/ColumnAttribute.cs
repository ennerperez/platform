using System;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Support.Data.Attributes
{
    /// <summary>
    /// Specifies the database column that a property is mapped to.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false), SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes")]
    public class ColumnAttribute : Attribute
    {
        private readonly string _name;

        private string _typeName;

        private int _order = -1;

        /// <summary>
        /// The name of the column the property is mapped to.
        /// </summary>
        public string Name
        {
            get
            {
                return this._name;
            }
        }

        /// <summary>
        /// The zero-based order of the column the property is mapped to.
        /// </summary>
        public int Order
        {
            get
            {
                return this._order;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                this._order = value;
            }
        }

        /// <summary>
        /// The database provider specific data type of the column the property is mapped to.
        /// </summary>
        public string TypeName
        {
            get
            {
                return this._typeName;
            }
            set
            {
                Check.NotEmpty(value, "value");
                this._typeName = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ColumnAttribute" /> class.
        /// </summary>
        public ColumnAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ColumnAttribute" /> class.
        /// </summary>
        /// <param name="name"> The name of the column the property is mapped to. </param>
        public ColumnAttribute(string name)
        {
            Check.NotEmpty(name, "name");
            this._name = name;
        }
    }
}