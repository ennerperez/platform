using Helpers = Platform.Support.Security.SecurityHelpers;

namespace Platform.Support.Security
{
    public static class SecurityExtensions
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
