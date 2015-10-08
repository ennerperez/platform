using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Platform.Support
{
    public static partial class Extensions
    {

        #region Types
        
        

        #endregion

        #region AssemblyInfo

        #region C#

        public static T GetAttribute<T>(this System.Reflection.Assembly assembly) where T : System.Attribute
        {
            return Helpers.GetAttribute<T>(assembly);
        }

        public static object GetAttribute(this System.Reflection.Assembly assembly, Type AttributeType)
        {
            return Helpers.GetAttribute(AttributeType, assembly);
        }

        public static String Title(this System.Reflection.Assembly assembly)
        {
            return Helpers.Title(assembly);
        }
        public static String Description(this System.Reflection.Assembly assembly)
        {
            return Helpers.Description(assembly);
        }
        public static String Company(this System.Reflection.Assembly assembly)
        {
            return Helpers.Company(assembly);
        }
        public static String Product(this System.Reflection.Assembly assembly)
        {
            return Helpers.Product(assembly);
        }
        public static String Copyright(this System.Reflection.Assembly assembly)
        {
            return Helpers.Copyright(assembly);
        }
        public static String Trademark(this System.Reflection.Assembly assembly)
        {
            return Helpers.Trademark(assembly);
        }
        public static Version Version(this System.Reflection.Assembly assembly)
        {
            return Helpers.Version(assembly);
        }
        public static Version FileVersion(this System.Reflection.Assembly assembly)
        {
            return Helpers.FileVersion(assembly);
        }

        #endregion

        #region VB


        #endregion

#if (!PORTABLE)
        public static Guid GUID(this System.Reflection.Assembly assembly)
        {
            return Helpers.GUID(assembly);
        }
        public static String DirectoryPath(this System.Reflection.Assembly assembly)
        {
            return Helpers.DirectoryPath(assembly);
        }
#endif

        #endregion

        #region Objects

        public static object IsNull(this object value, object replacement = null)
        {
            return Helpers.IsNull(value, replacement);
        }
        public static object IsNull<T>(this T value, object replacement = null)
        {
            return Helpers.IsNull<T>(value, replacement);
        }

        //public static T CType<T>(this object obj)
        //{
        //    return (T)Helpers.CType<T>(obj);
        //}

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

        #endregion

        #region Strings

        /// <summary>Devuelve un valor de tipo Boolean que indica si una expresión puede evaluarse como un número.</summary>
        /// <returns>Devuelve un valor de tipo Boolean que indica si una expresión puede evaluarse como un número.</returns>
        /// <param name="expression">Obligatorio.Expresión Object.</param>
        /// <filterpriority>1</filterpriority>
        /// <remarks>http://aspalliance.com/80_Benchmarking_IsNumeric_Options.all"/></remarks>
        public static bool IsNumeric(this string expression)
        {
            return Helpers.IsNumeric(expression);
        }

        public static string ToSentence(this string obj, bool capitalize = false)
        {
            return Helpers.ToSentence(obj, capitalize);
        }

        public static string TrimFromEnd(this string source, string suffix)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(suffix))
            {
                return source;
            }
            int num = source.LastIndexOf(suffix, StringComparison.OrdinalIgnoreCase);
            if (num <= 0)
            {
                return source;
            }
            return source.Substring(0, num);
        }

        public static IEnumerable<String> SplitInParts(this String s, Int32 partLength)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", "partLength");

            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }

        public static IEnumerable<String> SpliceText(this string text, int lineLength)
        {
            return Regex.Matches(text, ".{1," + lineLength + "}").Cast<Match>().Select(m => m.Value).ToArray();
        }

        #endregion

        #region DateTimes

        public static string ISO8601(this System.DateTime date)
        {
            return Helpers.ISO8601(date);
        }

        public static bool IsMin(this System.DateTime date)
        {
            return date.Date == DateTime.MinValue.Date;
        }

        public static bool IsNow(this System.DateTime date)
        {
            return date.Date == DateTime.Now.Date;
        }

        //		public static System.DateTime IsNull (this System.DateTime? value, System.DateTime replacement)
        //		{
        //			if (value != null && value.GetType () == typeof(DateTime)) {
        //				if (Convert.ToDateTime (value).ToString ("d") != Convert.ToDateTime (null).ToString ("d")) {
        //					return value;
        //				}
        //			}
        //			return replacement;
        //		}
        //
        //		public static System.DateTime IsNull (this System.DateTime? value)
        //		{
        //			System.DateTime replacement = System.DateTime.MinValue;
        //			return IsNull (value, replacement);
        //		}

        #endregion

    }
}
