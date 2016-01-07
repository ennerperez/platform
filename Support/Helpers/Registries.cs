using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support
{

    public static partial class Helpers
    {

#if (!PORTABLE)

        public static string GetRegSettings(string setting)
        {
            string _return = string.Empty;

            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(string.Format("Software\\{0}\\{1}", Company(), Product()));
            if (key != null)
            {
                _return = key.GetValue(setting, "").ToString();
                key.Close();
            }

            return _return;
        }
        public static void SetRegSettings(string setting, string value)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(string.Format("Software\\{0}\\{1}", Company(), Product()));
            key.SetValue(setting, value);
            key.Close();
        }

#endif

    }
}
