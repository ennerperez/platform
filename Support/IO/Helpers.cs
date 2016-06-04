using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support.IO
{

    public static class Helpers
    {

        public static void CopyStream(System.IO.Stream input, System.IO.Stream output)
        {
            //' assert these are the right kind of streams
            //If input Is Nothing Then Throw New ArgumentNullException("input")
            //If output Is Nothing Then Throw New ArgumentNullException("output")
            //If Not input.CanRead Then Throw New ArgumentException("Input stream must support CanRead")
            //If Not output.CanWrite Then Throw New ArgumentException("Output stream must support CanWrite")

            //' skip if the input stream is empty (if seeking is supported)
            //If input.CanSeek Then If input.Length = 0 Then Exit Sub

            //' allocate buffer (if all pre-conditions are met)
            //Dim buffer(1023) As Byte
            //Dim count As Integer = buffer.Length

            //' iterate read/writes between streams
            //Do
            //    count = input.Read(buffer, 0, count)
            //    If count = 0 Then Exit Do
            //    output.Write(buffer, 0, count)
            //Loop
            const int bufSize = 0x1000;
            byte[] buf = new byte[bufSize - 1];
            int bytesRead = 0;
            bytesRead = input.Read(buf, 0, bufSize);
            while (bytesRead > 0)
            {
                output.Write(buf, 0, bytesRead);
                bytesRead = input.Read(buf, 0, bufSize);
            }


        }

        public static string GetCRC32(string source)
        {
            return GetCRC32(new System.IO.FileInfo(source));
        }

        public static string GetCRC32(System.IO.FileInfo source)
        {
            var crc32 = new Platform.Support.Security.Cryptography.CRC32();
            String hash = String.Empty;
            if (System.IO.File.Exists(source.FullName))
            {
                try
                {
                    using (System.IO.FileStream fs = System.IO.File.Open(source.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
                    {
                        foreach (byte b in crc32.ComputeHash(fs))
                        {
                            hash += b.ToString("x2").ToLower();
                        }
                    }
                }
                catch
                {
                }
            }

            return hash;
        }

    }

}
