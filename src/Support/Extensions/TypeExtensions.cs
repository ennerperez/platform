#if PORTABLE

using Helpers = Platform.Support.Core.TypeHelper;

#else

using Helpers = Platform.Support.TypeHelper;

#endif

namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif

        public static class TypeExtensions
        {
            public static object IsNull(this object value, object replacement = null)
            {
                return Helpers.IsNull(value, replacement);
            }

            public static object IsNull<T>(this T value, object replacement = null)
            {
                return Helpers.IsNull<T>(value, replacement);
            }

            /// <summary>
            /// Converts an array of bytes to a string Hex representation
            /// </summary>
            public static string ToHex(this byte[] ba)
            {
                return Helpers.ToHex(ba);
            }

            /// <summary>
            /// converts from a string Hex representation to an array of bytes
            /// </summary>
            public static byte[] FromHex(this string hexEncoded)
            {
                return Helpers.FromHex(hexEncoded);
            }

            /// <summary>
            /// converts from a string Base64 representation to an array of bytes
            /// </summary>
            public static byte[] FromBase64(this string base64Encoded)
            {
                return Helpers.FromBase64(base64Encoded);
            }

            /// <summary>
            /// converts from an array of bytes to a string Base64 representation
            /// </summary>
            public static string ToBase64(this byte[] b)
            {
                return Helpers.ToBase64(b);
            }
        }

#if PORTABLE
    }

#endif
}