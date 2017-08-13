using Helpers = Platform.Support.Security.Cryptography.CryptographyHelper;

namespace Platform.Support.Security.Cryptography
{
    public static class CryptographyExtensions
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