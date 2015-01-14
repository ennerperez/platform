using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Support;

namespace Support
{
    public static class Helpers
    {

        #region AssemblyInfo

        public static object GetAttribute(Type AttributeType, System.Reflection.Assembly assembly = null)
        {
            System.Reflection.Assembly m_Assembly = System.Reflection.Assembly.GetExecutingAssembly();

            if (assembly != null) { m_Assembly = assembly; }

            object[] customAttributes = m_Assembly.GetCustomAttributes(AttributeType, true);
            if (customAttributes.Length == 0)
            {
                return null;
            }
            return customAttributes[0];
        }

        public static String CompanyName()
        {

            System.Reflection.AssemblyCompanyAttribute attribute = (System.Reflection.AssemblyCompanyAttribute)Support.Helpers.GetAttribute(typeof(System.Reflection.AssemblyCompanyAttribute));
            if (attribute != null) { return attribute.Company; }

            return "";
        }
        public static String ProductName()
        {

            System.Reflection.AssemblyProductAttribute attribute = (System.Reflection.AssemblyProductAttribute)Support.Helpers.GetAttribute(typeof(System.Reflection.AssemblyProductAttribute));
            if (attribute != null) { return attribute.Product; }

            return "";
        }

#if (!PORTABLE)
        public static String DirectoryPath()
        {
            System.IO.FileInfo fileinfo = new System.IO.FileInfo(System.Reflection.Assembly.GetEntryAssembly().GetName().FullName);
            return fileinfo.Directory.FullName;
        }
#endif

        #endregion

#if (!PORTABLE)

        public static string GetRegSettings(string setting)
        {
            string _return = string.Empty;

            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(string.Format("Software\\{0}\\{1}", CompanyName(), ProductName()));
            if (key != null)
            {
                _return = key.GetValue(setting, "").ToString();
                key.Close();
            }

            return _return;
        }
        public static void SetRegSettings(string setting, string value)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(string.Format("Software\\{0}\\{1}", CompanyName(), ProductName()));
            key.SetValue(setting, value);
            key.Close();
        }

#endif

    }
}
