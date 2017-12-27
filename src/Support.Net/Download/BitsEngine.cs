using Platform.Support.IO;
using Platform.Support.Net.Download.Bits;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Platform.Support.Net.Download
{
    internal class BitsEngine : DownloadEngineBase
    {
        public BitsEngine() : base("Bits", new DownloadSource[] { DownloadSource.Internet })
        {
        }

        //public BitsEngine(IServiceProvider serviceProvider) : base("Bits", new DownloadSource[] { DownloadSource.Internet })
        //{
        //    this.serviceProvider = serviceProvider;
        //    this.logger = serviceProvider.GetService<ILogger>(false);
        //}

        protected override DownloadSummary DownloadCore(Uri uri, Stream outputStream, ProgressUpdateCallback progress, CancellationToken cancellationToken, DownloadContext downloadContext)
        {
            IBackgroundCopyManager backgroundCopyManager = null;
            DownloadSummary downloadSummary = new DownloadSummary();
            try
            {
                backgroundCopyManager = this.lazyBackgroundCopyManager.Value;
            }
            catch (Exception ex)
            {
                ex.DebugThis();
                //ILogger logger = this.logger;
                //if (logger != null)
                //{
                //    logger.WriteWarning(string.Format("Skipped BITS download engine: {0}", ex.Message), new object[0]);
                //}
                throw;
            }
            string tempFileName = Path.GetTempFileName();
            Utilities.DeleteFileIfExists(tempFileName);
            try
            {
                //IServiceProvider serviceProvider = this.serviceProvider;
                IBackgroundCopyManager backgroundCopyManager2 = backgroundCopyManager;
                string filePath = tempFileName;
                string cookie;
                if (downloadContext == null)
                {
                    cookie = null;
                }
                else
                {
                    DownloadCookie cookie2 = downloadContext.Cookie;
                    cookie = (cookie2?.Value);
                }
                //using (BitsJob bitsJob = BitsJob.CreateJob(serviceProvider, backgroundCopyManager2, uri, filePath, cookie))
                using (BitsJob bitsJob = BitsJob.CreateJob(backgroundCopyManager2, uri, filePath, cookie))
                {
                    bitsJob.WaitForCompletion(progress, cancellationToken);
                }
                downloadSummary.ProxyResolution = ProxyResolution.Default.ToString();
                downloadSummary.DownloadedSize = Utilities.CopyFileToStream(tempFileName, outputStream, null, cancellationToken);
            }
            finally
            {
                Utilities.DeleteFileIfExists(tempFileName);
            }
            return downloadSummary;
        }

        private readonly Lazy<IBackgroundCopyManager> lazyBackgroundCopyManager = new Lazy<IBackgroundCopyManager>(() => new BackgroundCopyManager() as IBackgroundCopyManager);

        //private readonly IServiceProvider serviceProvider;

        //private readonly ILogger logger;
    }
}