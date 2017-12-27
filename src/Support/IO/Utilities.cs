#if !PORTABLE

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Platform.Support.IO
{
    public static partial class Utilities
    {
        public static void DeleteFileIfExists(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath");
            }
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch (IOException)
                {
                    Thread.Sleep(500);
                    File.Delete(filePath);
                }
                catch (UnauthorizedAccessException)
                {
                    FileAttributes attributes = File.GetAttributes(filePath);
                    if ((attributes & FileAttributes.ReadOnly).Equals(FileAttributes.ReadOnly))
                    {
                        File.SetAttributes(filePath, attributes & ~FileAttributes.ReadOnly);
                        File.Delete(filePath);
                    }
                }
            }
        }

        public static long CopyFileToStream(string filePath, Stream outStream, ProgressUpdateCallback progress, CancellationToken cancellationToken)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }
            long num = 0L;
            byte[] array = new byte[32768];
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                for (; ; )
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    int num2 = fileStream.Read(array, 0, array.Length);
                    cancellationToken.ThrowIfCancellationRequested();
                    if (num2 <= 0)
                    {
                        break;
                    }
                    outStream.Write(array, 0, num2);
                    num += (long)num2;
                    progress?.Invoke(new ProgressUpdateStatus(num, fileStream.Length, 0.0));
                }
                if (num != fileStream.Length)
                {
                    throw new IOException("Internal error copying streams. Total read bytes does not match stream Length.");
                }
            }
            return num;
        }
    }
}

#endif