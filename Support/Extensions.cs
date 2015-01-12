using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;

namespace Support
{
    public static class Extensions
    {
        #region Types

        


        #endregion

        #region Objects

        public static object IsNull(this object value, object replacement = null)
        {
            if (value == null)
            {
                return replacement;
            }
            else
            {
                return value;
            }
        }
        public static object IsNull<T>(this object value, object replacement = null)
        {
            if (value == null)
            {
                if (replacement == null)
                {
                    if (typeof(T) == typeof(System.String))
                    { return ""; }
                    if (typeof(T) == typeof(System.Int16) || typeof(T) == typeof(System.Int32) || typeof(T) == typeof(System.Int64) ||
                        typeof(T) == typeof(System.UInt16) || typeof(T) == typeof(System.UInt32) || typeof(T) == typeof(System.UInt64))
                    { return 0; }
                    if (typeof(T) == typeof(System.Decimal) || typeof(T) == typeof(System.Double))
                    { return 0.0; }

                    return null;
                }
                else
                {
                    return replacement;
                }
            }
            else
            {
                return value;
            }
        }

        public static object CType<T>(this object obj)
        {
            return (T)obj;
        }

        #endregion

        #region Enums


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

        #endregion
        
        #region Strings

        /// <summary>Devuelve un valor de tipo Boolean que indica si una expresión puede evaluarse como un número.</summary>
        /// <returns>Devuelve un valor de tipo Boolean que indica si una expresión puede evaluarse como un número.</returns>
        /// <param name="expression">Obligatorio.Expresión Object.</param>
        /// <filterpriority>1</filterpriority>
        /// <remarks>http://aspalliance.com/80_Benchmarking_IsNumeric_Options.all"/></remarks>
        public static bool IsNumeric(this string expression)
        {
            bool hasDecimal = false;
            for (int i = 0; i < expression.Length; i++)
            {
                // Check for decimal
                if (expression[i] == '.')
                {
                    if (hasDecimal) // 2nd decimal
                        return false;
                    else // 1st decimal
                    {
                        // inform loop decimal found and continue 
                        hasDecimal = true;
                        continue;
                    }
                }
                // check if number
                if (!char.IsNumber(expression[i]))
                    return false;
            }
            return true;
        }

        public static string ToSentence(this string obj, bool capitalize = false)
        {
            if (capitalize)
            {
                List<string> _return = new List<string>();
                foreach (string Item in obj.Split(' '))
                {
                    _return.Add(Item.ToSentence());
                }
                return String.Join(" ", _return);
            }
            else
            {
                return obj.Substring(0, 1).ToUpper() + obj.Substring(1).ToLower();
            }
        }

        #endregion

        #region Dates

        public static string ISO8601(System.DateTime date)
        {
            return string.Format("{0:yyyy-MM-dd HH:mm:ss}", date);
        }

        #endregion

    }
}
