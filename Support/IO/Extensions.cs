using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support.IO
{
    public static class Extensions
    {

        public static void CopyTo(this System.IO.Stream from, ref System.IO.Stream destination)
        {
            Helpers.CopyStream(from, destination);
        }


        public static void FromBytes(this System.IO.FileInfo file, byte[] data)
        {

            if (data == null)
            {
                throw new ArgumentNullException("Binary Data Cannot be Null or Empty", "data");
            }

            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(file.FullName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
                System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
                bw.Write(data);
                bw.Flush();
                bw.Close();
                bw = null;

            }
            catch
            {
            }
        }

        public static byte[] ToBytes(this System.IO.FileInfo file)
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
                System.IO.FileStream _FStream = new System.IO.FileStream(file.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FStream);
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

        public static System.IO.MemoryStream ToStream(this System.IO.FileInfo file, string filepath)
        {
            if (string.IsNullOrEmpty(filepath) == true)
            {
                throw new ArgumentNullException("File Name Cannot be Null or Empty", "filepath");
                //return null;
            }
            return new System.IO.MemoryStream(ToBytes(file));
        }

    }
}
