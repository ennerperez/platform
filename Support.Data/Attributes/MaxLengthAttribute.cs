using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Platform.Support.Data.Attributes
{
    /// <summary>
    /// Specifies the maximum length of array/string data allowed in a property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false), SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "We want users to be able to extend this class")]
    public class MaxLengthAttribute : ValidationAttribute
    {
        private const int MaxAllowableLength = -1;

        /// <summary>
        /// Gets the maximum allowable length of the array/string data.
        /// </summary>
        public int Length
        {
            get;
            private set;
        }

        private static string DefaultErrorMessageString
        {
            get
            {
                //return EntityRes.GetString("MaxLengthAttribute_ValidationError");
                return "The field {0} must be a string or array type with a maximum length of '{1}'.";
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MaxLengthAttribute" /> class.
        /// </summary>
        /// <param name="length"> The maximum allowable length of array/string data. Value must be greater than zero. </param>
        public MaxLengthAttribute(int length) : base(() => MaxLengthAttribute.DefaultErrorMessageString)
        {
            this.Length = length;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MaxLengthAttribute" /> class.
        /// The maximum allowable length supported by the database will be used.
        /// </summary>
        public MaxLengthAttribute() : base(() => MaxLengthAttribute.DefaultErrorMessageString)
        {
            this.Length = -1;
        }

        /// <summary>
        /// Determines whether a specified object is valid. (Overrides <see cref="M:System.ComponentModel.DataAnnotations.ValidationAttribute.IsValid(System.Object)" />)
        /// </summary>
        /// <remarks>
        /// This method returns <c>true</c> if the <paramref name="value" /> is null.
        /// It is assumed the <see cref="T:RequiredAttribute" /> is used if the value may not be null.
        /// </remarks>
        /// <param name="value"> The object to validate. </param>
        /// <returns> <c>true</c> if the value is null or less than or equal to the specified maximum length, otherwise <c>false</c> </returns>
        /// <exception cref="T:System.InvalidOperationException">Length is zero or less than negative one.</exception>
        public override bool IsValid(object value)
        {
            this.EnsureLegalLengths();
            if (value == null)
            {
                return true;
            }
            string text = value as string;
            int num = (text != null) ? text.Length : ((Array)value).Length;
            return -1 == this.Length || num <= this.Length;
        }

        /// <summary>
        /// Applies formatting to a specified error message. (Overrides <see cref="M:System.ComponentModel.DataAnnotations.ValidationAttribute.FormatErrorMessage(System.String)" />)
        /// </summary>
        /// <param name="name"> The name to include in the formatted string. </param>
        /// <returns> A localized string to describe the maximum acceptable length. </returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, base.ErrorMessageString, new object[]
            {
                name,
                this.Length
            });
        }

        /// <summary>
        /// Checks that Length has a legal value.  Throws InvalidOperationException if not.
        /// </summary>
        private void EnsureLegalLengths()
        {
            if (this.Length == 0 || this.Length < -1)
            {
                //throw Error.MaxLengthAttribute_InvalidMaxLength();
                throw new InvalidOperationException("MaxLengthAttribute must have a Length value that is greater than zero. User MaxLength() without parameters to indicate that the string or array can have the maximum allowable length.");
            }
        }
    }

    //[AttributeUsage(AttributeTargets.Property)]
    //public class MaxLengthAttribute : Attribute
    //{
    //    public MaxLengthAttribute(int length)
    //    {
    //        this.Value = length;
    //    }

    //    public int Value { get; private set; }
    //}
}