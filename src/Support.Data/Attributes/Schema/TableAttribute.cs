using System;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Support.Data.Attributes
{
    /// <summary>
    /// Specifies the database table that a class is mapped to.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false), SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes")]
    public class TableAttribute : Attribute
    {
        private readonly string _name;

        private string _schema;

        /// <summary>
        /// The name of the table the class is mapped to.
        /// </summary>
        public string Name
        {
            get
            {
                return this._name;
            }
        }

        /// <summary>
        /// The schema of the table the class is mapped to.
        /// </summary>
        public string Schema
        {
            get
            {
                return this._schema;
            }
            set
            {
                Parameter.NotEmpty(value, "value");
                this._schema = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TableAttribute" /> class.
        /// </summary>
        /// <param name="name"> The name of the table the class is mapped to. </param>
        public TableAttribute(string name)
        {
            Parameter.NotEmpty(name, "name");
            this._name = name;
        }
    }
}