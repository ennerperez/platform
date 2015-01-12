using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Support;

namespace Support.Attributes
{

    public static class Extensions
    {

//#if (!PORTABLE)

//        public static Support.ProductLevel ProductLevel(this global::System.Configuration.ApplicationSettingsBase obj)
//        {
//            return System.Reflection.Assembly.GetCallingAssembly().GetCustomAttributes(false).OfType<Support.Attributes.AssemblyProduct.ProductLevelAttribute>().FirstOrDefault().ProductLevel;
//        }
//        public static int LevelNumber(this global::System.Configuration.ApplicationSettingsBase obj)
//        {
//            return System.Reflection.Assembly.GetCallingAssembly().GetCustomAttributes(false).OfType<Support.Attributes.AssemblyProduct.ProductLevelAttribute>().FirstOrDefault().LevelNumber;
//        }

//        public static System.DateTime AssemblyDate(this global::System.Configuration.ApplicationSettingsBase obj)
//        {
//            return System.Reflection.Assembly.GetCallingAssembly().GetCustomAttributes(false).OfType<Support.Attributes.AssemblyProduct.BuildDateAttribute>().FirstOrDefault().AssemblyDate;
//        }

//        public static string CompanyID(this global::System.Configuration.ApplicationSettingsBase obj)
//        {
//            Support.Attributes.AssemblyCompany.IdAttribute _return = System.Reflection.Assembly.GetCallingAssembly().GetCustomAttributes(false).OfType<Support.Attributes.AssemblyCompany.IdAttribute>().FirstOrDefault();
//            if (_return != null)
//            {
//                return _return.CompanyID;
//            }
//            else
//            {
//                return null;
//            }
//        }
//        public static System.Net.Mail.MailAddress[] CompanyEmails(this global::System.Configuration.ApplicationSettingsBase obj)
//        {
//            return System.Reflection.Assembly.GetCallingAssembly().GetCustomAttributes(false).OfType<Support.Attributes.AssemblyCompany.MailAttribute>().Select(e => e.CompanyEmail).ToArray();
//        }
//        public static Uri CompanyUrl(this global::System.Configuration.ApplicationSettingsBase obj)
//        {
//            return System.Reflection.Assembly.GetCallingAssembly().GetCustomAttributes(false).OfType<Support.Attributes.AssemblyCompany.UrlAttribute>().FirstOrDefault().CompanyUrl;
//        }
//        public static string[] CompanyPhones(this global::System.Configuration.ApplicationSettingsBase obj)
//        {
//            return System.Reflection.Assembly.GetCallingAssembly().GetCustomAttributes(false).OfType<Support.Attributes.AssemblyCompany.ContactAttribute>().Where(w => w.Contact[0] == "number").Select(p => p.Contact[1]).ToArray();
//        }
//        public static string[] CompanyContacts(this global::System.Configuration.ApplicationSettingsBase obj)
//        {
//            return System.Reflection.Assembly.GetCallingAssembly().GetCustomAttributes(false).OfType<Support.Attributes.AssemblyCompany.ContactAttribute>().Select(c => String.Join(",", c.Contact)).ToArray();
//        }
//        public static string[] ThirdParty(this global::System.Configuration.ApplicationSettingsBase obj)
//        {
//            return System.Reflection.Assembly.GetCallingAssembly().GetCustomAttributes(false).OfType<Support.Attributes.AssemblyProduct.ThirdPartyAttribute>().Select(c => String.Join(",", new string[] { c.ThirdParty, c.Info })).ToArray();
//        }
//        public static string[] Developers(this global::System.Configuration.ApplicationSettingsBase obj)
//        {
//            return System.Reflection.Assembly.GetCallingAssembly().GetCustomAttributes(false).OfType<Support.Attributes.AssemblyProduct.DeveloperAttribute>().Select(c => String.Join(",", new string[] { c.DeveloperName, c.AditionalInfo })).ToArray();
//        }

//        public static Support.Attributes.AssemblyProduct.ExternalRefAttribute[] Externals(this global::System.Configuration.ApplicationSettingsBase obj)
//        {
//            return System.Reflection.Assembly.GetCallingAssembly().GetCustomAttributes(false).OfType<Support.Attributes.AssemblyProduct.ExternalRefAttribute>().ToArray();
//        }
//        public static Support.Attributes.AssemblyProduct.ExternalRefAttribute Externals(this global::System.Configuration.ApplicationSettingsBase obj, string key)
//        {
//            return System.Reflection.Assembly.GetCallingAssembly().GetCustomAttributes(false).OfType<Support.Attributes.AssemblyProduct.ExternalRefAttribute>().FirstOrDefault(k => k.Description == key);
//        }
               

//#endif

    }

}
