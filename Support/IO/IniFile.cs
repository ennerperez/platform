using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Support.IO
{
    public class IniFile
    {
        internal string path;
        public IniFile(string path)
        {
            this.path = path;
        }
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retVal, int size, string filePath);
        public void IniWriteValue(string section, string key, string value)
        {
            IniFile.WritePrivateProfileString(section, key, value, this.path);
        }
        public string IniReadValue(string section, string key)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder(255);
            IniFile.GetPrivateProfileString(section, key, "", stringBuilder, 255, this.path);
            return stringBuilder.ToString();
        }
    }
}
