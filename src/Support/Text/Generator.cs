using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif
    namespace Text
    {

        public struct Characters
        {
            public const string UpperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            public const string LowerCase = "abcdefghijklmnopqrstuvwxyz";
            public const string Numeric = "0123456789";
            public const string Special = ",.;:?!/@#$%^&()=+*-_{}[]<>|~";
            public const string Text = LowerCase + UpperCase;
            public const string Alphanumeric = Text + Numeric;
            public const string All = Alphanumeric + Special;

        }

        public class Generator
        {
            private static Random random = new Random();

            public static string RandomString(int length, string characterSet = Characters.All)
            {
                return new string(Enumerable.Repeat(characterSet, length)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
            }

            public static string RandomString(int length)
            {
                byte[] randBuffer = new byte[length];
                RandomNumberGenerator.Create().GetBytes(randBuffer);
                return Convert.ToBase64String(randBuffer).Remove(length);
            }

            public static string RandomString()
            {
                return Path.GetRandomFileName().Replace(".", "");
            }

            public static string RandomName(int length = 4)
            {
                //Random r = new Random();
                var r = random;
                string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
                string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
                string Name = "";
                Name += consonants[r.Next(consonants.Length)].ToUpper();
                Name += vowels[r.Next(vowels.Length)];
                int b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
                while (b < length)
                {
                    Name += consonants[r.Next(consonants.Length)];
                    b++;
                    Name += vowels[r.Next(vowels.Length)];
                    b++;
                }

                return Name.Substring(0, 1).ToUpper() + Name.Substring(1);
            }

            public static string RandomEmail(int length = 8)
            {
                var domains = new[] { "com", "org", "net", "int", "edu", "gov", "mil" };
                var domain = domains[random.Next(domains.Length)];
                return $"{RandomString(length)}@{RandomString()}.{domain}".ToLower();
            }
            public static string RandomEmail(string name)
            {
                var domains = new[] { "com", "org", "net", "int", "edu", "gov", "mil" };
                var domain = domains[random.Next(domains.Length)];
                return $"{name.Replace(" ", ".")}@{RandomString()}.{domain}".ToLower();
            }

            public static string RandomPasword(int length = 8, string characterSet = Characters.All)
            {
                if (length < 0)
                    throw new ArgumentException("length must not be negative", "length");
                if (length > int.MaxValue / 8) // 250 million chars ought to be enough for anybody
                    throw new ArgumentException("length is too big", "length");
                if (characterSet == null)
                    throw new ArgumentNullException("characterSet");
                var characterArray = characterSet.Distinct().ToArray();
                if (characterArray.Length == 0)
                    throw new ArgumentException("characterSet must not be empty", "characterSet");

                var bytes = new byte[length * 8];
                new RNGCryptoServiceProvider().GetBytes(bytes);
                var result = new char[length];
                for (int i = 0; i < length; i++)
                {
                    ulong value = BitConverter.ToUInt64(bytes, i * 8);
                    result[i] = characterArray[value % (uint)characterArray.Length];
                }
                return new string(result);
            }


        }

    }
#if PORTABLE
    }
#endif
}
