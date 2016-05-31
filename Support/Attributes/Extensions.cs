using System;
using System.Linq;
#if PORTABLE
using Platform.Support.Core.Attributes;
#else
using Platform.Support.Attributes;
#endif

namespace Platform.Support
{

#if PORTABLE
    namespace Core
    {
#endif

    public static partial class Extensions
    {

#region AssemblyInfo

        public static ProductLevels ProductLevel(this System.Reflection.Assembly assembly)
        {
            return Helpers.GetAttribute<ProductLevelAttribute>(assembly).ProductLevel;
        }
        public static int LevelNumber(this System.Reflection.Assembly assembly)
        {
            return Helpers.GetAttribute<ProductLevelAttribute>(assembly).LevelNumber;
        }
        public static DateTime AssemblyDate(this System.Reflection.Assembly assembly)
        {
            return Helpers.GetAttribute<BuildDateAttribute>(assembly).AssemblyDate;
        }

        public static string[] DevelopersNames(this System.Reflection.Assembly assembly)
        {
            return Helpers.GetAttributes<DeveloperAttribute>(assembly).Select(d => d.DeveloperName).ToArray();
        }
        public static string[] ThirdParties(this System.Reflection.Assembly assembly)
        {
            return Helpers.GetAttributes<ThirdPartyAttribute>(assembly).Select(d => d.ThirdParty + ": " + d.Info).ToArray();
        }
#if (!PORTABLE)
        public static System.Diagnostics.Process[] ExternalReferences(this System.Reflection.Assembly assembly)
        {
            return Helpers.GetAttributes<ExternalRefAttribute>(assembly).Select(d => new System.Diagnostics.Process() { StartInfo = new System.Diagnostics.ProcessStartInfo(d.FileName, d.Arguments) }).ToArray();
        }
#else
        public static string[][] ExternalReferences(this System.Reflection.Assembly assembly)
        {
            return Helpers.GetAttributes<ExternalRefAttribute>(assembly).Select(d => new string[] { d.FileName, d.Arguments }).ToArray();
        }
#endif

        public static string CompanyID(this System.Reflection.Assembly assembly)
        {
            return Helpers.GetAttribute<IdAttribute>(assembly).CompanyID;
        }
        public static string[][] Contacts(this System.Reflection.Assembly assembly)
        {
            return Helpers.GetAttributes<ContactAttribute>(assembly).Select(d => d.Contact).ToArray();
        }
#if (!PORTABLE)
        public static System.Net.Mail.MailAddress[] CompanyEmail(this System.Reflection.Assembly assembly)
#else
        public static String[] CompanyEmail(this System.Reflection.Assembly assembly)
#endif
        {
            return Helpers.GetAttributes<MailAttribute>(assembly).Select(d => d.CompanyEmail).ToArray();
        }
        public static Uri CompanyURL(this System.Reflection.Assembly assembly)
        {
            return Helpers.GetAttribute<UrlAttribute>(assembly).CompanyUrl;
        }

#endregion

    }

#if PORTABLE
    }
#endif

}
