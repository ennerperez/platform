using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Platform.Support.IO
{
    public class IniFile
    {
        internal string path;
        public IniFile(string path)
        {
            this.path = path;
        }
       
        public void IniWriteValue(string section, string key, string value)
        {
            NativeMethods.WritePrivateProfileString(section, key, value, this.path);
        }
        public string IniReadValue(string section, string key)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder(255);
            NativeMethods.GetPrivateProfileString(section, key, "", stringBuilder, 255, this.path);
            return stringBuilder.ToString();
        }
    }
}
