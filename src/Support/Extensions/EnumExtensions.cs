using System;
using System.Linq;

namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif

    public static partial class Extensions
    {
#if (!PORTABLE)

        /// <summary>
        /// Get description of a enum value
        /// See DescriptionAttribute for enum element
        /// </summary>    ''' <remarks>
        /// Returns the human readable string set as the DescriptionAttribute
        /// for an enum element.
        /// If the DescriptionAttribute has not been set, returns the enum element name
        /// </remarks>
        /// <param name="value">Enum element with human readable string</param>
        /// <returns>Human readable string for enum element</returns>
        public static string GetDescription(this System.Enum value)
        {
            // Get information on the enum element
            System.Reflection.FieldInfo fi = value.GetType().GetField(value.ToString());
            // Get description for elum element
            System.ComponentModel.DescriptionAttribute[] attributes = (System.ComponentModel.DescriptionAttribute[])fi.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                // DescriptionAttribute exists - return that
                return attributes.FirstOrDefault().Description;
            }
            // No Description set - return enum element name
            return value.ToString();
        }

        /// <summary>
        /// Get an enum element from its human readable string.
        /// </summary>
        /// <remarks>
        /// Returns the enum element for a human readable string
        /// If the DescriptionAttribute has not been set, throws an
        /// ArgumentException
        /// </remarks>
        /// <exception cref="ArgumentException">ArgumentException if description not found to match an enum element</exception>
        /// <seealso cref="GetDescription"/>
        /// <typeparam name="T">Enum the element will belong to</typeparam>
        /// <param name="description">Human readable string assocuiated with enum element</param>
        /// <returns>Enum element</returns>
        public static T ValueOf<T>(this string description)
        {
            Type enumType = typeof(T);
            string[] names = System.Enum.GetNames(enumType);

            foreach (string name in names)
            {
                if (GetDescription((System.Enum)System.Enum.Parse(enumType, name, true)).Equals(description, StringComparison.InvariantCultureIgnoreCase))
                {
                    // Found it!
                    return (T)System.Enum.Parse(enumType, name, true);
                }
            }
            // No such description in this enum
            throw new ArgumentException("The string is not a description or value of the specified enum.");
        }

#endif
    }

#if PORTABLE
    }

#endif
}