using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
#if PORTABLE
using Helpers = Platform.Support.Core.StringHelpers;
#else
using Helpers = Platform.Support.StringHelpers;
#endif

namespace Platform.Support
{
#if PORTABLE
    namespace Core
    {
#endif
    public static class StringExtensions
    {
                
        public static bool IsNumeric(this string expression)
        {
            return Helpers.IsNumeric(expression);
        }

        public static bool IsEmail(this string expression)
        {
            return Helpers.IsEmail(expression);
        }

        public static string ToSentence(this string obj, bool capitalize = false)
        {
            return Helpers.ToSentence(obj, capitalize);
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
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }

        public static IEnumerable<String> SpliceText(this string text, int lineLength)
        {
            return Regex.Matches(text, ".{1," + lineLength + "}").Cast<Match>().Select(m => m.Value).ToArray();
        }

    }
#if PORTABLE
    }
#endif
}
