using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support
{
    public static class IsNull
    {

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
}
