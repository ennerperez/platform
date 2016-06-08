using System;
using System.IO;

namespace Platform.Support.IO
{

    public static class FileInfoHelpers
    {

        public static string GetCRC32(string source)
        {
            return GetCRC32(new FileInfo(source));
        }

        public static string GetCRC32(FileInfo source)
        {
            var crc32 = new Security.Cryptography.CRC32();
            String hash = String.Empty;
            if (File.Exists(source.FullName))
            {
                try
                {
                    using (FileStream fs = File.Open(source.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                        foreach (byte b in crc32.ComputeHash(fs))
                            hash += b.ToString("x2").ToLower();
                }
                catch (Exception ex)
                {
                    ex.DebugThis();
                }
            }

            return hash;
        }

    }

}
