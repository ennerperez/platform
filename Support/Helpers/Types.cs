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

        /// <summary>
        /// Converts an array of bytes to a string Hex representation
        /// </summary>
        public static string ToHex(byte[] ba)
        {
            if (ba == null || ba.Length == 0)
            {
                return "";
            }
            const string HexFormat = "{0:X2}";
            StringBuilder sb = new StringBuilder();
            foreach (byte b in ba)
            {
                sb.Append(string.Format(HexFormat, b));
            }
            return sb.ToString();
        }

        /// <summary>
        /// converts from a string Hex representation to an array of bytes
        /// </summary>
        public static byte[] FromHex(string hexEncoded)
        {
            if (hexEncoded == null || hexEncoded.Length == 0)
            {
                return null;
            }
            try
            {
                int l = Convert.ToInt32(hexEncoded.Length / 2);
                byte[] b = new byte[l - 1];
                for (int i = 0; i <= l - 1; i++)
                {
                    b[i] = Convert.ToByte(hexEncoded.Substring(i * 2, 2), 16);
                }
                return b;
            }
            catch (Exception ex)
            {
                throw new System.FormatException("The provided string does not appear to be Hex encoded:" + Environment.NewLine + hexEncoded + Environment.NewLine, ex);
            }
        }

        /// <summary>
        /// converts from a string Base64 representation to an array of bytes
        /// </summary>
        public static byte[] FromBase64(string base64Encoded)
        {
            if (base64Encoded == null || base64Encoded.Length == 0)
            {
                return null;
            }
            try
            {
                return Convert.FromBase64String(base64Encoded);
            }
            catch (System.FormatException ex)
            {
                throw new System.FormatException("The provided string does not appear to be Base64 encoded:" + Environment.NewLine + base64Encoded + Environment.NewLine, ex);
            }
        }

        /// <summary>
        /// converts from an array of bytes to a string Base64 representation
        /// </summary>
        public static string ToBase64(byte[] b)
        {
            if (b == null || b.Length == 0)
            {
                return "";
            }
            return Convert.ToBase64String(b);
        }

    }
}
