using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support
{
    public static partial class Helpers
    {

        /// <summary>Devuelve un valor de tipo Boolean que indica si una expresión puede evaluarse como un número.</summary>
        /// <returns>Devuelve un valor de tipo Boolean que indica si una expresión puede evaluarse como un número.</returns>
        /// <param name="expression">Obligatorio.Expresión Object.</param>
        /// <filterpriority>1</filterpriority>
        /// <remarks>http://aspalliance.com/80_Benchmarking_IsNumeric_Options.all"/></remarks>
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
}
