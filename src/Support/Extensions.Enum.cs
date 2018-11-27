using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private static Dictionary<Type, Dictionary<Enum, string>> enumDescriptions = new Dictionary<Type, Dictionary<Enum, string>>();

        /// <summary>
        /// Get description of a enum value
        /// See DescriptionAttribute for enum element
        /// </summary>
        /// <remarks>
        /// Returns the human readable string set as the DescriptionAttribute
        /// for an enum element.
        /// If the DescriptionAttribute has not been set, returns the enum element name
        /// </remarks>
        /// <param name="value">Enum element with human readable string</param>
        /// <returns>Human readable string for enum element</returns>
        public static string GetDescription(this Enum value, bool cached = true)
        {
            var type = value.GetType();
            if (cached)
            {
                if (!enumDescriptions.ContainsKey(type))
                {
                    var description = (type.GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[]).FirstOrDefault()?.Description;
                    var item = new Dictionary<Enum, string> { { value, description } };
                    enumDescriptions.Add(type, item);
                }
                else
                {
                    var item = enumDescriptions[type];
                    if (!item.ContainsKey(value))
                    {
                        var description = (type.GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[]).FirstOrDefault()?.Description;
                        item.Add(value, description);
                    }
                }

                return enumDescriptions[type][value];
            }
            else
            {
                var description = (type.GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[]).FirstOrDefault()?.Description;
                return description;
            }
        }

        /// <summary>
        /// Get description of a enum value
        /// See DescriptionAttribute for enum element
        /// </summary>
        /// <remarks>
        /// Returns the human readable string set as the DescriptionAttribute
        /// for an enum element.
        /// If the DescriptionAttribute has not been set, returns the enum element name
        /// </remarks>
        /// <param name="type">Enum type</param>
        /// <param name="value">Enum element with human readable string</param>
        /// <returns>Human readable string for enum element</returns>
        public static string GetDescription(this Type type, Enum value, bool cached = true)
        {
            if (type.IsEnum)
            {
                var result = GetDescription(value, cached);
                return result;
            }
            return null;
        }

        /// <summary>
        /// Get descriptions of all enum values
        /// </summary>
        /// <param name="type">Enum type</param>
        /// <returns></returns>
        public static IDictionary<Enum, string> GetDescriptions(this Type type)
        {
            if (type.IsEnum)
            {
                var values = Enum.GetValues(type);
                var length = values.Length;
                var result = new Dictionary<Enum, string>();
                for (int i = 0; i < length; i++)
                {
                    var value = (Enum)values.GetValue(i);
                    var description = GetDescription(value);
                    result.Add(value, description);
                }
                return result;
            }
            return null;
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