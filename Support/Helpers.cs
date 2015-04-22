using System;
using System.Collections.Generic;
using System.Linq;

namespace Support
{
    public static class Helpers
    {

        #region Object

        public static object IsNull(object value, object replacement = null)
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
        public static object IsNull<T>(T value, object replacement = null)
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

        public static object CType<T>(object obj)
        {
            if (obj.GetType() == typeof(Int64))
            {
                return Convert.ToInt64(obj);
            }
            if (obj.GetType() == typeof(Int32))
            {
                return Convert.ToInt32(obj);
            }
            else if (obj.GetType() == typeof(Int16))
            {
                return Convert.ToInt16(obj);
            }
            //else if (obj.GetType() == typeof(Int16) && (Int64)obj <= Int32.MaxValue)
            //{
            //}
            else
            {
#if (!PORTABLE)
                return (T)Convert.ChangeType(obj, typeof(T));
#else
                return (T)Convert.ChangeType(obj, typeof(T), null );
#endif
                //return (T)obj;
            }
        }

        #endregion

        #region AssemblyInfo

#if (!PORTABLE)
        internal static System.Reflection.Assembly m_Assembly = System.Reflection.Assembly.GetEntryAssembly();
#else
        internal static System.Reflection.Assembly m_Assembly = System.Reflection.Assembly.GetCallingAssembly();
#endif


        public static T GetAttribute<T>(System.Reflection.Assembly assembly = null) where T : System.Attribute
        {
            return (T)GetAttribute(typeof(T), assembly);
        }
        public static T[] GetAttributes<T>(System.Reflection.Assembly assembly = null) where T : System.Attribute
        {
            return (T[])GetAttributes(typeof(T), assembly);
        }

        public static object GetAttribute(Type AttributeType, System.Reflection.Assembly assembly = null)
        {
            return GetAttributes(AttributeType, assembly)[0];
        }
        public static object[] GetAttributes(Type AttributeType, System.Reflection.Assembly assembly = null)
        {
            if (assembly == null) { assembly = m_Assembly; }

            object[] customAttributes = assembly.GetCustomAttributes(AttributeType, true);
            if (customAttributes.Length == 0)
            {
                return null;
            }
            return customAttributes;
        }

        public static String Title(System.Reflection.Assembly assembly = null)
        {
            if (assembly == null) { assembly = m_Assembly; }
            return GetAttribute<System.Reflection.AssemblyTitleAttribute>(assembly).Title;
        }
        public static String Description(System.Reflection.Assembly assembly = null)
        {
            if (assembly == null) { assembly = m_Assembly; }
            return GetAttribute<System.Reflection.AssemblyDescriptionAttribute>(assembly).Description;
        }
        public static String Company(System.Reflection.Assembly assembly = null)
        {
            if (assembly == null) { assembly = m_Assembly; }
            return GetAttribute<System.Reflection.AssemblyCompanyAttribute>(assembly).Company;
        }
        public static String Product(System.Reflection.Assembly assembly = null)
        {
            if (assembly == null) { assembly = m_Assembly; }
            return GetAttribute<System.Reflection.AssemblyProductAttribute>(assembly).Product;
        }
        public static String Copyright(System.Reflection.Assembly assembly = null)
        {
            if (assembly == null) { assembly = m_Assembly; }
            return GetAttribute<System.Reflection.AssemblyCopyrightAttribute>(assembly).Copyright;
        }
        public static String Trademark(System.Reflection.Assembly assembly = null)
        {
            if (assembly == null) { assembly = m_Assembly; }
            return GetAttribute<System.Reflection.AssemblyTrademarkAttribute>(assembly).Trademark;
        }
        public static Version Version(System.Reflection.Assembly assembly = null)
        {
            if (assembly == null) { assembly = m_Assembly; }
            return new Version(GetAttribute<System.Reflection.AssemblyVersionAttribute>(assembly).Version);
        }
        public static Version FileVersion(System.Reflection.Assembly assembly = null)
        {
            if (assembly == null) { assembly = m_Assembly; }
            return new Version(GetAttribute<System.Reflection.AssemblyFileVersionAttribute>(assembly).Version);
        }

#if (!PORTABLE)
        public static Guid GUID(System.Reflection.Assembly assembly = null)
        {
            if (assembly == null) { assembly = m_Assembly; }
            return new Guid(GetAttribute<System.Runtime.InteropServices.GuidAttribute>(assembly).Value);
        }

        public static String DirectoryPath(System.Reflection.Assembly assembly = null)
        {
            if (assembly == null) { assembly = m_Assembly; }
            return new System.IO.FileInfo(assembly.Location).Directory.FullName;
        }

        public static String ExecutablePath(System.Reflection.Assembly assembly = null)
        {
            if (assembly == null) { assembly = m_Assembly; }
            return assembly.Location;
        }


#endif
        #endregion

        public static UInt64 ReverseBytes(UInt64 value)
        {
            return (value & 0x00000000000000FFUL) << 56 | (value & 0x000000000000FF00UL) << 40 |
                (value & 0x0000000000FF0000UL) << 24 | (value & 0x00000000FF000000UL) << 8 |
                (value & 0x000000FF00000000UL) >> 8 | (value & 0x0000FF0000000000UL) >> 24 |
                (value & 0x00FF000000000000UL) >> 40 | (value & 0xFF00000000000000UL) >> 56;
        }

#if (!PORTABLE)

        public static string GetRegSettings(string setting)
        {
            string _return = string.Empty;

            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(string.Format("Software\\{0}\\{1}", Company(), Product()));
            if (key != null)
            {
                _return = key.GetValue(setting, "").ToString();
                key.Close();
            }

            return _return;
        }
        public static void SetRegSettings(string setting, string value)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(string.Format("Software\\{0}\\{1}", Company(), Product()));
            key.SetValue(setting, value);
            key.Close();
        }

#endif

        #region Enum

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
        public static string GetDescription(System.Enum value)
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
        public static T ValueOf<T>(string description)
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
        public static bool IsNumeric(string expression)
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

        public static string ToSentence(string obj, bool capitalize = false)
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
