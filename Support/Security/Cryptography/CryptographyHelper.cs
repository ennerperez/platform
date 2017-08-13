using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Platform.Support.Security.Cryptography
{
    public static class CryptographyHelper
    {
        public const string PublicKey = "!#$a54?3";
        private static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

        public static string Encrypt(string input, string key = null)
        {
            if (key == null)
                key = PublicKey;

            if (key.Length > 8)
                key = key.Substring(0, 8);
            if (key.Length < 8)
                key = key.PadLeft(8, '0');

            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = Encoding.UTF8.GetBytes(key);
            DES.IV = IV;

            try
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(input);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, DES.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                ex.DebugThis();
                return null;
            }
        }

        public static string Decrypt(string input, string key = null)
        {
            if (key == null)
                key = PublicKey;

            if (key.Length > 8)
                key = key.Substring(0, 8);
            if (key.Length < 8)
                key = key.PadLeft(8, '0');

            //TODO: Provisional, corregir la codificación de caracteres.
            input = input.Replace(" ", "+");
            byte[] inputByteArray = new byte[input.Length];

            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = Encoding.UTF8.GetBytes(key);
            DES.IV = IV;

            try
            {
                inputByteArray = Convert.FromBase64String(input);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, DES.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = Encoding.UTF8;
                return encoding.GetString(ms.ToArray(), 0, (int)ms.Length);
            }
            catch (Exception ex)
            {
                ex.DebugThis();
                return null;
            }
        }
    }
}