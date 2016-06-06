using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Platform.Support.Reflection
{
    public static class Helpers
    {

        private static string _GetLocalUserAppDataPath;
        public static string GetLocalUserAppDataPath()
        {
            if (string.IsNullOrEmpty(_GetLocalUserAppDataPath))
            {
                var assembly = Assembly.GetEntryAssembly();
                var companyName = assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), true).OfType<AssemblyCompanyAttribute>().First().Company;
                var productName = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), true).OfType<AssemblyTitleAttribute>().First().Title;
                var version = assembly.GetName().Version;
                _GetLocalUserAppDataPath = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), companyName, productName, version.ToString(3));
            }
            return _GetLocalUserAppDataPath;
        }

    }
}
