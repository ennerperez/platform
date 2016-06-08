using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Platform.Support
{
#if PORTABLE
    namespace Core
    {
#endif
    public static class StringHelpers
    {

        public static bool IsNumeric(string expression)
        {
            bool hasDecimal = false;
            for (int i = 0; i < expression.Length; i++)
            {
                // Check for decimal
                if (expression[i] == '.')
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
                if (!char.IsNumber(expression[i]))
                    return false;
            }
            return true;
        }

        public static bool IsEmail(string expression)
        {
            return Regex.IsMatch(expression, @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
        }

        public static string ToSentence(string obj, bool capitalize = false)
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

    }
#if PORTABLE
    }
#endif
}
