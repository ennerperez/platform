using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support.Security.Cryptography
{
    public static class Extensions
    {

       public static string Encrypt(this string input, string key = null)
        {
            return Helpers.Encrypt(input, key);
        }

       public static string Decrypt(this string input, string key = null)
       {
           return Helpers.Decrypt(input, key);
       }

    }
}
