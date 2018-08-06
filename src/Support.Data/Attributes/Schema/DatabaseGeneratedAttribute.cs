using System;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Support.Data.Attributes
{
    /// <summary>
    /// The pattern used to generate values for a property in the database.
    /// </summary>
    public enum DatabaseGeneratedOption
    {
        /// <summary>
        /// The database does not generate values.
        /// </summary>
        None,

        /// <summary>
        /// The database generates a value when a row is inserted.
        /// </summary>
        Identity,

        /// <summary>
        /// The database generates a value when a row is inserted or updated.
        /// </summary>
        Computed
    }

    /// <summary>
    /// Specifies how the database generates values for a property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false), SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes")]
    public class DatabaseGeneratedAttribute : Attribute
    {
        /// <summary>
        /// The pattern used to generate values for the property in the database.
        /// </summary>
        public DatabaseGeneratedOption DatabaseGeneratedOption
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DatabaseGeneratedAttribute" /> class.
        /// </summary>
        /// <param name="databaseGeneratedOption"> The pattern used to generate values for the property in the database. </param>
        public DatabaseGeneratedAttribute(DatabaseGeneratedOption databaseGeneratedOption)
        {
            if (!Enum.IsDefined(typeof(DatabaseGeneratedOption), databaseGeneratedOption))
            {
                throw new ArgumentOutOfRangeException("databaseGeneratedOption");
            }
            this.DatabaseGeneratedOption = databaseGeneratedOption;
        }
    }
}