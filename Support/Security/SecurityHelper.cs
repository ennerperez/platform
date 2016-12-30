using System;
using System.Linq;
using System.Security.Cryptography;

namespace Platform.Support.Security
{
    public static class SecurityHelper
    {

        public static int CalculateStrength(string password, int min = 6)
        {
            char[] ValidLetters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            char[] ValidDigits = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            char[] ValidSpecialCharacters = { '@', '#', '$', '%', '^', '&', '+', '=' };

            int _return = 0;

            if (password.Length >= min)
                _return += 20;

            int _UpperCaseLetters = 0;
            int _LowerCaseLetters = 0;
            foreach (char item in ValidLetters)
            {
                if (password.Contains(item))
                    _UpperCaseLetters += 1;
                if (password.Contains(char.ToLower(item)))
                    _LowerCaseLetters += 1;
            }
            if (_UpperCaseLetters >= 1 & _LowerCaseLetters >= 1)
                _return += 20;

            int _Digits = 0;
            foreach (char item in ValidDigits)
            {
                if (password.Contains(item))
                    _Digits += 1;
            }
            if (_Digits >= 1)
                _return += 20;

            int _SpecialCharacters = 0;
            foreach (char item in ValidSpecialCharacters)
            {
                if (password.Contains(item))
                    _SpecialCharacters += 1;
            }
            if (_SpecialCharacters >= 1)
                _return += 20;

            if (password.Length >= (min * 2))
                _return += 20;

            return (int)_return;

        }

        public static string CalculateSHA1(string input)
        {
            string cHash;
            string cBase64 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(input));

            byte[] abBytesToHash = System.Text.Encoding.ASCII.GetBytes(cBase64);
            SHA1CryptoServiceProvider objSHA1 = new SHA1CryptoServiceProvider();
            cHash = BitConverter.ToString(objSHA1.ComputeHash(abBytesToHash));
            cHash = cHash.Replace("-", "");
            return cHash;
        }

    }
}
