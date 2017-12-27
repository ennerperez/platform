using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support.Net.Download
{
    public class DownloadSummary
    {
        public DownloadSummary() : this(null, 0L, 0.0, TimeSpan.Zero)
        {
        }

        public DownloadSummary(long downloadSize, double bitRate, TimeSpan downloadTime) : this(null, downloadSize, bitRate, downloadTime)
        {
        }

        public DownloadSummary(string downloadEngine, long downloadSize, double bitRate, TimeSpan downloadTime)
        {
            this.DownloadEngine = downloadEngine;
            this.DownloadedSize = downloadSize;
            this.BitRate = bitRate;
            this.DownloadTime = downloadTime;
            this.ProxyResolution = null;
            this.FinalUri = null;
        }

        public long DownloadedSize { get; internal set; }

        public double BitRate { get; internal set; }

        public TimeSpan DownloadTime { get; internal set; }

        public string DownloadEngine { get; internal set; }

        public string ProxyResolution { get; internal set; }

        public string FinalUri { get; internal set; }
    }
}