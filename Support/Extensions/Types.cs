using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support
{
    public static partial class Extensions
    {

        public static object IsNull(this object value, object replacement = null)
        {
            return Helpers.IsNull(value, replacement);
        }
        public static object IsNull<T>(this T value, object replacement = null)
        {
            return Helpers.IsNull<T>(value, replacement);
        }


    }
}
