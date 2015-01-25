using System;

namespace Support
{
    public static partial class Extensions
    {

        #region Types




        #endregion

        #region AssemblyInfo

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

        public static object CType<T>(this object obj)
        {
            return Helpers.CType<T>(obj);
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

        #endregion

    }
}
