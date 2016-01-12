using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Platform.Support.Data.Attributes
{

    /// <summary>
	/// When this attribute is placed on a property it indicates that the database column to which the
	/// property is mapped has an index.
	/// </summary>
	/// <remarks>
	/// This attribute is used by Entity Framework Migrations to create indexes on mapped database columns.
	/// Multi-column indexes are created by using the same index name in multiple attributes. The information
	/// in these attributes is then merged together to specify the actual database index.
	/// </remarks>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true), SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes"), SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    public class IndexAttribute : Attribute
    {
        private string _name;

        private int _order = -1;

        private bool? _isClustered;

        private bool? _isUnique;

        /// <summary>
        /// The index name.
        /// </summary>
        /// <remarks>
        /// Multi-column indexes are created by using the same index name in multiple attributes. The information
        /// in these attributes is then merged together to specify the actual database index.
        /// </remarks>
        public virtual string Name
        {
            get
            {
                return this._name;
            }
            internal set
            {
                this._name = value;
            }
        }

        /// <summary>
        /// A number which will be used to determine column ordering for multi-column indexes. This will be -1 if no
        /// column order has been specified.
        /// </summary>
        /// <remarks>
        /// Multi-column indexes are created by using the same index name in multiple attributes. The information
        /// in these attributes is then merged together to specify the actual database index.
        /// </remarks>
        public virtual int Order
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
        /// Set this property to true to define a clustered index. Set this property to false to define a 
        /// non-clustered index.
        /// </summary>
        /// <remarks>
        /// The value of this property is only relevant if <see cref="P:System.ComponentModel.DataAnnotations.Schema.IndexAttribute.IsClusteredConfigured" /> returns true.
        /// If <see cref="P:System.ComponentModel.DataAnnotations.Schema.IndexAttribute.IsClusteredConfigured" /> returns false, then the value of this property is meaningless.
        /// </remarks>
        public virtual bool IsClustered
        {
            get
            {
                return this._isClustered.HasValue && this._isClustered.Value;
            }
            set
            {
                this._isClustered = new bool?(value);
            }
        }

        /// <summary>
        /// Returns true if <see cref="P:System.ComponentModel.DataAnnotations.Schema.IndexAttribute.IsClustered" /> has been set to a value.
        /// </summary>
        public virtual bool IsClusteredConfigured
        {
            get
            {
                return this._isClustered.HasValue;
            }
        }

        /// <summary>
        /// Set this property to true to define a unique index. Set this property to false to define a 
        /// non-unique index.
        /// </summary>
        /// <remarks>
        /// The value of this property is only relevant if <see cref="P:System.ComponentModel.DataAnnotations.Schema.IndexAttribute.IsUniqueConfigured" /> returns true.
        /// If <see cref="P:System.ComponentModel.DataAnnotations.Schema.IndexAttribute.IsUniqueConfigured" /> returns false, then the value of this property is meaningless.
        /// </remarks>
        public virtual bool IsUnique
        {
            get
            {
                return this._isUnique.HasValue && this._isUnique.Value;
            }
            set
            {
                this._isUnique = new bool?(value);
            }
        }

        /// <summary>
        /// Returns true if <see cref="P:System.ComponentModel.DataAnnotations.Schema.IndexAttribute.IsUnique" /> has been set to a value.
        /// </summary>
        public virtual bool IsUniqueConfigured
        {
            get
            {
                return this._isUnique.HasValue;
            }
        }

        /// <summary>
        /// Returns a different ID for each object instance such that type descriptors won't
        /// attempt to combine all IndexAttribute instances into a single instance.
        /// </summary>
        public override object TypeId
        {
            get
            {
                return RuntimeHelpers.GetHashCode(this);
            }
        }

        /// <summary>
        /// Creates a <see cref="T:System.ComponentModel.DataAnnotations.Schema.IndexAttribute" /> instance for an index that will be named by convention and
        /// has no column order, clustering, or uniqueness specified.
        /// </summary>
        public IndexAttribute()
        {
        }

        /// <summary>
        /// Creates a <see cref="T:System.ComponentModel.DataAnnotations.Schema.IndexAttribute" /> instance for an index with the given name and
        /// has no column order, clustering, or uniqueness specified.
        /// </summary>
        /// <param name="name">The index name.</param>
        public IndexAttribute(string name)
        {
            Check.NotEmpty(name, "name");
            this._name = name;
        }

        /// <summary>
        /// Creates a <see cref="T:System.ComponentModel.DataAnnotations.Schema.IndexAttribute" /> instance for an index with the given name and column order, 
        /// but with no clustering or uniqueness specified.
        /// </summary>
        /// <remarks>
        /// Multi-column indexes are created by using the same index name in multiple attributes. The information
        /// in these attributes is then merged together to specify the actual database index.
        /// </remarks>
        /// <param name="name">The index name.</param>
        /// <param name="order">A number which will be used to determine column ordering for multi-column indexes.</param>
        public IndexAttribute(string name, int order)
        {
            Check.NotEmpty(name, "name");
            if (order < 0)
            {
                throw new ArgumentOutOfRangeException("order");
            }
            this._name = name;
            this._order = order;
        }

        private IndexAttribute(string name, int order, bool? isClustered, bool? isUnique)
        {
            this._name = name;
            this._order = order;
            this._isClustered = isClustered;
            this._isUnique = isUnique;
        }

        /// <summary>
        /// Returns true if this attribute specifies the same name and configuration as the given attribute.
        /// </summary>
        /// <param name="other">The attribute to compare.</param>
        /// <returns>True if the other object is equal to this object; otherwise false.</returns>
        protected virtual bool Equals(IndexAttribute other)
        {
            return this._name == other._name && this._order == other._order && this._isClustered.Equals(other._isClustered) && this._isUnique.Equals(other._isUnique);
        }

        ///// <inheritdoc />
        //public override string ToString()
        //{
        //    return IndexAnnotationSerializer.SerializeIndexAttribute(this);
        //}

        /// <summary>
        /// Returns true if this attribute specifies the same name and configuration as the given attribute.
        /// </summary>
        /// <param name="obj">The attribute to compare.</param>
        /// <returns>True if the other object is equal to this object; otherwise false.</returns>
        public override bool Equals(object obj)
        {
            return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (!(obj.GetType() != base.GetType()) && this.Equals((IndexAttribute)obj)));
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            int num = base.GetHashCode();
            num = (num * 397 ^ ((this._name != null) ? this._name.GetHashCode() : 0));
            num = (num * 397 ^ this._order);
            num = (num * 397 ^ this._isClustered.GetHashCode());
            return num * 397 ^ this._isUnique.GetHashCode();
        }
    }

    #region Legacy

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true), SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes"), SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    public class IndexedAttribute : IndexAttribute
    {
        //public IndexedAttribute()
        //{
        //}

        //public IndexedAttribute(string name, int order)
        //{
        //    this.Name = name;
        //    this.Order = order;
        //}

        //public string Name { get; set; }
        //public int Order { get; set; }
        //public virtual bool Unique { get; set; }
    }

    #endregion

}