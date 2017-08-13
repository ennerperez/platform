using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Platform.Support.Data.Attributes
{
    /// <summary>
    /// Specifies the minimum length of array/string data allowed in a property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false), SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "We want users to be able to extend this class")]
    public class MinLengthAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets the minimum allowable length of the array/string data.
        /// </summary>
        public int Length
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MinLengthAttribute" /> class.
        /// </summary>
        /// <param name="length"> The minimum allowable length of array/string data. Value must be greater than or equal to zero. </param>
        public MinLengthAttribute(int length) : base(() => "The field {0} must be a string or array type with a minimum length of '{1}'.") //EntityRes.GetString("MinLengthAttribute_ValidationError"))
        {
            this.Length = length;
        }

        /// <summary>
        /// Determines whether a specified object is valid. (Overrides <see cref="M:System.ComponentModel.DataAnnotations.ValidationAttribute.IsValid(System.Object)" />)
        /// </summary>
        /// <remarks>
        /// This method returns <c>true</c> if the <paramref name="value" /> is null.
        /// It is assumed the <see cref="T:System.ComponentModel.DataAnnotations.RequiredAttribute" /> is used if the value may not be null.
        /// </remarks>
        /// <param name="value"> The object to validate. </param>
        /// <returns> <c>true</c> if the value is null or greater than or equal to the specified minimum length, otherwise <c>false</c> </returns>
        /// <exception cref="T:System.InvalidOperationException">Length is less than zero.</exception>
        public override bool IsValid(object value)
        {
            this.EnsureLegalLengths();
            if (value == null)
            {
                return true;
            }
            string text = value as string;
            int num = (text != null) ? text.Length : ((Array)value).Length;
            return num >= this.Length;
        }

        /// <summary>
        /// Applies formatting to a specified error message. (Overrides <see cref="M:System.ComponentModel.DataAnnotations.ValidationAttribute.FormatErrorMessage(System.String)" />)
        /// </summary>
        /// <param name="name"> The name to include in the formatted string. </param>
        /// <returns> A localized string to describe the minimum acceptable length. </returns>
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
            if (this.Length < 0)
            {
                //throw Error.MinLengthAttribute_InvalidMinLength();
                throw new InvalidOperationException("MinLengthAttribute must have a Length value that is zero or greater.");
            }
        }
    }
}