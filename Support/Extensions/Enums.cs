using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support
{
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
            return Helpers.GetDescription(value);
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
            return Helpers.ValueOf<T>(description);
        }

#endif

    }
}
