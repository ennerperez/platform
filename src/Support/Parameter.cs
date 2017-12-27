using System;
using System.Runtime.CompilerServices;

namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif

        public static class Parameter
        {
            public static void ThrowIfNull<T>(T value, string paramName) where T : class
            {
                if (Parameter.IsNull<T>(value))
                    throw new ArgumentNullException(paramName);
            }

            public static void ThrowIfNotOfType<T>(object value, string paramName, bool allowNull = false)
            {
                if (Parameter.IsNotOfType<T>(value, allowNull))
                    throw new ArgumentException(string.Empty, paramName);
            }

            public static bool IsNull<T>(T value) where T : class
            {
                return value == null;
            }

            public static bool IsNotOfType<T>(object value, bool allowNull)
            {
                return (value == null && !allowNull) || (value != null && !(value is T));
            }

#if PORTABLE

            public static T NotNull<T>(T value, string parameterName = "") where T : class
#else
            public static T NotNull<T>(T value, [CallerMemberName] string parameterName = "") where T : class
#endif
            {
                if (value == null)
                    throw new ArgumentNullException(parameterName);
                return value;
            }

#if PORTABLE

            public static T? NotNull<T>(T? value, string parameterName = "") where T : struct
#else
            public static T? NotNull<T>(T? value, [CallerMemberName] string parameterName = "") where T : struct
#endif
            {
                if (!value.HasValue)
                    throw new ArgumentNullException(parameterName);
                return value;
            }

#if PORTABLE

            public static string NotEmpty(string value, string parameterName = "")
#else
            public static string NotEmpty(string value, [CallerMemberName] string parameterName = "")
#endif
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format("The argument '{0}' cannot be null, empty or contain only white space.", parameterName));
                }
                return value;
            }

            public static string Cast(object value, string replacement = "")
            {
                if (value != null && value.GetType() == typeof(DateTime))
                {
                    if (Convert.ToDateTime(value).ToString("d") != Convert.ToDateTime(null).ToString("d"))
                        return Convert.ToDateTime(value).ToString("d");
                }

                if (value == null)
                    return replacement;

                return value.ToString();
            }

            public static string Cast(DateTime? value, DateTime? replacement = null)
            {
                if (value != null && value.GetType() == typeof(DateTime))
                {
                    if (Convert.ToDateTime(value).ToString("d") != Convert.ToDateTime(null).ToString("d"))
                        return Convert.ToDateTime(value).ToString("d");
                }

                if (value != null)
                    return Convert.ToDateTime(value).ToString("d");
                else if (replacement != null)
                    return Convert.ToDateTime(replacement).ToString("d");

                return DateTime.MinValue.ToString("d");
            }
        }

#if PORTABLE
    }

#endif
}