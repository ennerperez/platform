using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support
{
    public static partial class Helpers
    {

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

        public static UInt64 ReverseBytes(UInt64 value)
        {
            return (value & 0x00000000000000FFUL) << 56 | (value & 0x000000000000FF00UL) << 40 |
                (value & 0x0000000000FF0000UL) << 24 | (value & 0x00000000FF000000UL) << 8 |
                (value & 0x000000FF00000000UL) >> 8 | (value & 0x0000FF0000000000UL) >> 24 |
                (value & 0x00FF000000000000UL) >> 40 | (value & 0xFF00000000000000UL) >> 56;
        }

    }
}
