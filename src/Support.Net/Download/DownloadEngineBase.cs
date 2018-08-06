using Platform.Support.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Platform.Support.Net.Download
{
    public abstract class DownloadEngineBase : DisposableObject, IDownloadEngine
    {
        protected DownloadEngineBase(string name, DownloadSource[] supportedSources)
        {
            this.Name = name;
            this.supportedSources = supportedSources;
        }

        public string Name { get; private set; }

        public bool IsSupported(DownloadSource source)
        {
            return this.supportedSources.Contains(source);
        }

        public DownloadSummary Download(Uri uri, Stream outputStream, ProgressUpdateCallback progress, CancellationToken cancellationToken, DownloadContext downloadContext)
        {
            return this.DownloadWithBitRate(uri, outputStream, progress, cancellationToken, downloadContext);
        }

        protected abstract DownloadSummary DownloadCore(Uri uri, Stream outputStream, ProgressUpdateCallback progress, CancellationToken cancellationToken, DownloadContext downloadContext);

        private DownloadSummary DownloadWithBitRate(Uri uri, Stream outputStream, ProgressUpdateCallback progress, CancellationToken cancellationToken, DownloadContext downloadContext)
        {
            DateTime now = DateTime.Now;
            DateTime lastProgressUpdate = now;
            long lastReadCount = 0L;
            ProgressUpdateCallback progress2 = null;
            if (progress != null)
            {
                progress2 = delegate (ProgressUpdateStatus p)
                {
                    DateTime now2 = DateTime.Now;
                    TimeSpan timeSpan = now2 - lastProgressUpdate;
                    long num = p.BytesRead - lastReadCount;
                    double bitRate = 8.0 * (double)num / timeSpan.TotalSeconds;
                    progress(new ProgressUpdateStatus(p.BytesRead, p.TotalBytes, bitRate));
                    lastProgressUpdate = now2;
                };
            }
            DownloadSummary downloadSummary = this.DownloadCore(uri, outputStream, progress2, cancellationToken, downloadContext);
            downloadSummary.DownloadTime = DateTime.Now - now;
            downloadSummary.BitRate = 8.0 * (double)downloadSummary.DownloadedSize / downloadSummary.DownloadTime.TotalSeconds;
            return downloadSummary;
        }

        private DownloadSource[] supportedSources;
    }
}