using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

#if !PORTABLE

using System.Security.Cryptography;

#endif

namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif

    public static partial class Extensions
    {
        public static bool IsNumeric(this string value)
        {
            bool hasDecimal = false;
            for (int i = 0; i < value.Length; i++)
            {
                // Check for decimal
                if (value[i] == '.')
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
                if (!char.IsNumber(value[i]))
                    return false;
            }
            return true;
        }

        public static bool IsEmail(this string value)
        {
            return Regex.IsMatch(value, @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
        }

        public static string ToSentence(this string obj, bool capitalize = false)
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

        public static string TrimFromEnd(this string source, string suffix)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(suffix))
            {
                return source;
            }
            int num = source.LastIndexOf(suffix, StringComparison.OrdinalIgnoreCase);
            if (num <= 0)
            {
                return source;
            }
            return source.Substring(0, num);
        }

        public static IEnumerable<String> SplitInParts(this String s, Int32 partLength)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", "partLength");

            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, System.Math.Min(partLength, s.Length - i));
        }

        public static IEnumerable<String> SpliceText(this string text, int lineLength)
        {
            return Regex.Matches(text, ".{1," + lineLength + "}").Cast<Match>().Select(m => m.Value).ToArray();
        }

        public static string Last(this string text, int chars = 1)
        {
            return text.Substring(text.Length - chars, chars);
        }

#if !PORTABLE

        public static string SHA256(this string source, string key = null)
        {
            string result;
            var expr_05 = new UTF8Encoding();
            byte[] bytes2 = expr_05.GetBytes(source);
            var hMAC = new HMACSHA256();
            if (!string.IsNullOrEmpty(key))
            {
                byte[] bytes = expr_05.GetBytes(key);
                hMAC = new HMACSHA256(bytes);
            }

            result = BitConverter.ToString(hMAC.ComputeHash(bytes2)).Replace("-", "").ToLower();

            hMAC.Dispose();

            return result;
        }

        public static string MD5(this string source, string key = null)
        {
            string result;
            var expr_05 = new UTF8Encoding();
            byte[] bytes2 = expr_05.GetBytes(source);
            var hMAC = new HMACMD5();
            if (!string.IsNullOrEmpty(key))
            {
                byte[] bytes = expr_05.GetBytes(key);
                hMAC = new HMACMD5(bytes);
            }

            result = BitConverter.ToString(hMAC.ComputeHash(bytes2)).Replace("-", "").ToLower();

            hMAC.Dispose();

            return result;
        }

#endif
    }

#if PORTABLE
    }

#endif
}