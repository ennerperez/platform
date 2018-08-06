using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif

    namespace IO
    {
        public class ProgressUpdateStatus
        {
            public ProgressUpdateStatus(long bytesRead, long totalBytes, double bitRate) : this(null, bytesRead, totalBytes, bitRate)
            {
            }

            public ProgressUpdateStatus(string downloadEngine, long bytesRead, long totalBytes, double bitRate)
            {
                this.DownloadEngine = downloadEngine;
                this.BytesRead = bytesRead;
                this.TotalBytes = totalBytes;
                this.BitRate = bitRate;
            }

            public long BytesRead { get; private set; }

            public long TotalBytes { get; private set; }

            public double BitRate { get; private set; }

            public string DownloadEngine { get; private set; }
        }

        public delegate void ProgressUpdateCallback(ProgressUpdateStatus status);
    }

#if PORTABLE
    }
#endif
}