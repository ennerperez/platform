using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support
{
    public static class IsNull
    {

        #region Nullable

        public static string Cast(object value, string replacement = "")
        {
            if (value != null && value.GetType() == typeof(DateTime))
            {
                if (Convert.ToDateTime(value).ToString("d") != Convert.ToDateTime(null).ToString("d"))
                {
                    return Convert.ToDateTime(value).ToString("d");
                }
            }
            return replacement;
        }

        public static string Cast(DateTime? value, DateTime? replacement = null)
        {
            if (value != null && value.GetType() == typeof(DateTime))
            {
                if (Convert.ToDateTime(value).ToString("d") != Convert.ToDateTime(null).ToString("d"))
                {
                    return Convert.ToDateTime(value).ToString("d");
                }
            }
            return Convert.ToDateTime(replacement).ToString("d");
        }

        #endregion
    }
}
