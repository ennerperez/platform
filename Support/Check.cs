using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Platform.Support
{
    public class Check
    {
        public static T NotNull<T>(T value, string parameterName) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
            return value;
        }

        public static T? NotNull<T>(T? value, string parameterName) where T : struct
        {
            if (!value.HasValue)
            {
                throw new ArgumentNullException(parameterName);
            }
            return value;
        }

#if NETFX_45
        public static string NotEmpty(string value, [CallerMemberName] string parameterName = "")
#else
        public static string NotEmpty(string value, string parameterName = "")
#endif
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(string.Format("The argument '{0}' cannot be null, empty or contain only white space.", parameterName));
            }
            return value;
        }


    }
}
