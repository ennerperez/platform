using System;
using System.IO;
using Helpers = Platform.Support.IO.FileInfoHelper;

namespace Platform.Support.IO
{
    public static class FileInfoExtensions
    {
        
        public static void FromBytes(this FileInfo file, byte[] data)
        {

            if (data == null)
            {
                throw new ArgumentNullException("Binary Data Cannot be Null or Empty", "data");
            }

            try
            {
                FileStream fs = new FileStream(file.FullName, FileMode.OpenOrCreate, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(data);
                bw.Flush();
                bw.Close();
                bw = null;

            }
            catch
            {
            }
        }

        public static byte[] ToBytes(this FileInfo file)
        {
            byte[] _tempByte = null;
            if (string.IsNullOrEmpty(file.FullName) == true)
            {
                throw new ArgumentNullException("File Name Cannot be Null or Empty", "filepath");
                //return null;
            }
            try
            {
                long _NumBytes = file.Length;
                FileStream _FStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
                BinaryReader _BinaryReader = new BinaryReader(_FStream);
                _tempByte = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes));
                file = null;
                _NumBytes = 0;
                _BinaryReader.Close();
                return _tempByte;
            }
            catch
            {
                return null;
            }
        }

        public static string GetCRC32(this FileInfo source)
        {
            return Helpers.GetCRC32(source);
        }

        public static MemoryStream ToStream(this FileInfo file, string filepath)
        {
            if (string.IsNullOrEmpty(filepath) == true)
            {
                throw new ArgumentNullException("File Name Cannot be Null or Empty", "filepath");
                //return null;
            }
            return new MemoryStream(ToBytes(file));
        }

    }
}
