using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif

    namespace IO
    {
        public static class StreamExtensions
        {
            public static void CopyTo(this Stream input, ref Stream output)
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

            public static long Transfer(this Stream inStream, Stream outStream)
            {
                return Transfer(inStream, outStream, 8192, true);
            }

            public static long Transfer(this Stream inStream, Stream outStream, int bufferSize, bool flush)
            {
                long totalBytes = 0;
                byte[] buffer = new byte[bufferSize];
                int bytesRead;

                while (0 != (bytesRead = inStream.Read(buffer, 0, bufferSize)))
                {
                    if (bytesRead < 0)
                    {
#if !PORTABLE
                        Debug.Fail("bytesRead was negative! " + bytesRead);
#else
                            Debug.WriteLine("bytesRead was negative! " + bytesRead);
#endif
                        break;
                    }
                    outStream.Write(buffer, 0, bytesRead);
                    totalBytes += bytesRead;
                }

                if (flush)
                    outStream.Flush();

                return totalBytes;
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="inStream"></param>
            /// <param name="outStream"></param>
            /// <param name="buffer"></param>
            /// <returns>True if something was transferred, false if transferring is complete.</returns>
            public static bool TransferIncremental(this Stream inStream, Stream outStream, ref byte[] buffer, out long byteCount)
            {
                if (buffer == null)
                    buffer = new byte[8192];

                int bytesRead;
                bytesRead = inStream.Read(buffer, 0, buffer.Length);
                if (bytesRead != 0)
                {
                    outStream.Write(buffer, 0, bytesRead);
                    byteCount = bytesRead;
                    return true;
                }
                else
                {
                    byteCount = 0;
                    return false;
                }
            }

            /// <summary>
            /// Read and discard the contents of the stream until EOF.
            /// </summary>
            public static void DiscardRest(this Stream stream)
            {
                if (stream.CanSeek)
                    stream.Seek(0, SeekOrigin.End);
                else
                {
                    byte[] buf = new byte[4096];
                    while (stream.Read(buf, 0, buf.Length) != 0)
                        ;  // do nothing
                }
            }

            public static byte[] AsBytes(this Stream inStream)
            {
                MemoryStream outStr = new MemoryStream();
                Transfer(inStream, outStr);
                return outStr.ToArray();
            }

            public static string AsString(this Stream inStream, Encoding enc)
            {
                using (StreamReader reader = new StreamReader(inStream, enc))
                {
                    return reader.ReadToEnd();
                }
            }

            /// <summary>
            /// Return up to <c>count</c> bytes from the stream.
            ///
            /// If EOF has been reached, returns null. Otherwise,
            /// returns a byte array exactly long enough to hold
            /// the actual number of bytes read (will not exceed
            /// <c>count</c>).
            /// </summary>
            public static byte[] AsBytes(this Stream inStream, int count)
            {
                byte[] buf = new byte[count];
                int bytesRead = inStream.Read(buf, 0, count);

                if (bytesRead == buf.Length)
                {
                    return buf;
                }
                else if (bytesRead == -1)
                {
                    return null;
                }
                else
                {
                    byte[] shortBuf = new byte[bytesRead];
                    Array.Copy(buf, shortBuf, shortBuf.Length);
                    return shortBuf;
                }
            }

            public static Stream AsStream(this byte[] data)
            {
                MemoryStream inStr = new MemoryStream(data);
                return inStr;
            }

            public static Stream CopyToMemoryStream(this Stream s)
            {
                if (s.CanSeek)
                {
                    // If we can find out the stream length, we can copy it in a way
                    // that only results in a single byte array getting instantiated.
                    return new MemoryStream(AsBytes(s, checked((int)s.Length)));
                }
                else
                {
                    MemoryStream memStream = new MemoryStream();
                    Transfer(s, memStream);
                    memStream.Seek(0, SeekOrigin.Begin);
                    return memStream;
                }
            }

            /// <summary>
            /// Returns the set of bytes between a sequence of start and end bytes (including the start/end bytes).
            /// </summary>
            /// <param name="startBytes"></param>
            /// <param name="endBytes"></param>
            /// <param name="s"></param>
            /// <returns></returns>
            public static byte[] ExtractByteRegion(this byte[] startBytes, byte[] endBytes, Stream s)
            {
                int b = s.ReadByte();
                long startIndex = -1;
                while (b != -1 && startIndex == -1)
                {
                    if (b == startBytes[0])
                    {
                        long position = s.Position;
                        bool tokenMaybeFound = true;
                        for (int i = 1; tokenMaybeFound && i < startBytes.Length; i++)
                        {
                            b = s.ReadByte();
                            tokenMaybeFound = b == startBytes[i];
                        }
                        if (tokenMaybeFound)
                        {
                            b = s.ReadByte(); //move past the last byte in the startBytes
                            startIndex = position;
                        }
                        else
                        {
                            //move back to the last known good position
                            s.Seek(position, SeekOrigin.Begin);
                            b = s.ReadByte(); //advance to the next byte
                        }
                    }
                    else
                        b = s.ReadByte();
                }
                if (startIndex == -1)
                    return new byte[0];

                MemoryStream memStream = new MemoryStream();
                memStream.Write(startBytes, 0, startBytes.Length);
                while (b != -1)
                {
                    memStream.WriteByte((byte)b);
                    if (b == endBytes[0])
                    {
                        bool tokenMaybeFound = true;
                        for (int i = 1; tokenMaybeFound && i < endBytes.Length; i++)
                        {
                            b = s.ReadByte();
                            memStream.WriteByte((byte)b);
                            tokenMaybeFound = b == endBytes[i];
                        }
                        if (tokenMaybeFound)
                            break;
                    }
                    b = s.ReadByte();
                }
                return memStream.ToArray();
            }

            public static string ConvertToBase64(this Stream stream)
            {
                if (stream == null)
                    throw new ArgumentNullException("stream");
                long length = stream.Length;
                if (length > 2147483647L)
                    throw new IOException(string.Format("Cannot convert a stream longer than ${0} bytes to Base 64 string. Stream length: ${1}", new object[]
                    {
                                int.MaxValue,
                                length
                    }));
                int num = (int)length;
                byte[] array = new byte[num];
                stream.Read(array, 0, num);
                return Convert.ToBase64String(array);
            }

            public static void WriteBase64(this Stream stream, string base64Data)
            {
                if (stream == null)
                    throw new ArgumentNullException("stream");
                byte[] array = Convert.FromBase64String(base64Data);
                stream.Write(array, 0, array.Length);
            }
        }
    }

#if PORTABLE
    }

#endif
}