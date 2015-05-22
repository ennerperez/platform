using System;
using System.Linq;

namespace Platform.Support
{

#if !CORE
    namespace Core
    {
#endif

        public static partial class Extensions
        {

            #region AssemblyInfo

            public static ProductLevels ProductLevel(this System.Reflection.Assembly assembly)
            {
                return Helpers.GetAttribute<Attributes.AssemblyProduct.ProductLevelAttribute>(assembly).ProductLevel;
            }
            public static int LevelNumber(this System.Reflection.Assembly assembly)
            {
                return Helpers.GetAttribute<Attributes.AssemblyProduct.ProductLevelAttribute>(assembly).LevelNumber;
            }
            public static DateTime AssemblyDate(this System.Reflection.Assembly assembly)
            {
                return Helpers.GetAttribute<Attributes.AssemblyProduct.BuildDateAttribute>(assembly).AssemblyDate;
            }

            public static string[] DevelopersNames(this System.Reflection.Assembly assembly)
            {
                return Helpers.GetAttributes<Attributes.AssemblyProduct.DeveloperAttribute>(assembly).Select(d => d.DeveloperName).ToArray();
            }
            public static string[] ThirdParties(this System.Reflection.Assembly assembly)
            {
                return Helpers.GetAttributes<Attributes.AssemblyProduct.ThirdPartyAttribute>(assembly).Select(d => d.ThirdParty + ": " + d.Info).ToArray();
            }
#if (!PORTABLE)
        public static System.Diagnostics.Process[] ExternalReferences(this System.Reflection.Assembly assembly)
        {
            return Helpers.GetAttributes<Attributes.AssemblyProduct.ExternalRefAttribute>(assembly).Select(d => new System.Diagnostics.Process() { StartInfo = new System.Diagnostics.ProcessStartInfo(d.FileName, d.Arguments) }).ToArray();
        }
#else
            public static string[][] ExternalReferences(this System.Reflection.Assembly assembly)
            {
                return Helpers.GetAttributes<Attributes.AssemblyProduct.ExternalRefAttribute>(assembly).Select(d => new string[] { d.FileName, d.Arguments }).ToArray();
            }
#endif

            public static string CompanyID(this System.Reflection.Assembly assembly)
            {
                return Helpers.GetAttribute<Attributes.AssemblyCompany.IdAttribute>(assembly).CompanyID;
            }
            public static string[][] Contacts(this System.Reflection.Assembly assembly)
            {
                return Helpers.GetAttributes<Attributes.AssemblyCompany.ContactAttribute>(assembly).Select(d => d.Contact).ToArray();
            }
#if (!PORTABLE)
        public static System.Net.Mail.MailAddress[] CompanyEmail(this System.Reflection.Assembly assembly)
#else
            public static String[] CompanyEmail(this System.Reflection.Assembly assembly)
#endif
            {
                return Helpers.GetAttributes<Attributes.AssemblyCompany.MailAttribute>(assembly).Select(d => d.CompanyEmail).ToArray();
            }
            public static Uri CompanyURL(this System.Reflection.Assembly assembly)
            {
                return Helpers.GetAttribute<Attributes.AssemblyCompany.UrlAttribute>(assembly).CompanyUrl;
            }

            #endregion

        }

#if !CORE
    }
#endif

}
