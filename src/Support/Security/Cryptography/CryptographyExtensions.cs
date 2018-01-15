#if !PORTABLE

using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Platform.Support.Security.Cryptography
{
    public static partial class Extensions
    {
        internal const string PublicKey = "!#$a54?3";

        internal static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

        public static string Encrypt(this string input, string key = null)
        {
            if (key == null)
                key = PublicKey;

            if (key.Length > 8)
                key = key.Substring(0, 8);
            if (key.Length < 8)
                key = key.PadLeft(8, '0');

            var DES = new DESCryptoServiceProvider
            {
                Key = Encoding.UTF8.GetBytes(key),
                IV = IV
            };

            try
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(input);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, DES.CreateEncryptor(), CryptoStreamMode.Write);
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

        public static string Decrypt(this string input, string key = null)
        {
            if (key == null)
                key = PublicKey;

            if (key.Length > 8)
                key = key.Substring(0, 8);
            if (key.Length < 8)
                key = key.PadLeft(8, '0');

            //TODO: Temporal, fix char encoding.
            input = input.Replace(" ", "+");
            byte[] inputByteArray = new byte[input.Length];

            var DES = new DESCryptoServiceProvider
            {
                Key = Encoding.UTF8.GetBytes(key),
                IV = IV
            };

            try
            {
                inputByteArray = Convert.FromBase64String(input);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, DES.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                var encoding = Encoding.UTF8;
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

#endif