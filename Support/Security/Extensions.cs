using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Platform.Support.Security
{
    public static class Extensions
    {

        public static int CalculateStrength(this string password, int min = 6)
        {
            return Helpers.CalculateStrength(password, min);
        }

        public static string CalculateSHA1(this string input)
        {
            return Helpers.CalculateSHA1(input);
        }

    }
}
