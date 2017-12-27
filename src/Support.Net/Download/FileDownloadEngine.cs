using Platform.Support.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Platform.Support.Net.Download
{
    internal class FileDownloadEngine : DownloadEngineBase
    {
        public FileDownloadEngine() : base("File", new DownloadSource[1])
        {
        }

        //public FileDownloadEngine(IServiceProvider serviceProvider) : base("File", new DownloadSource[1])
        //{
        //    this.serviceProvider = serviceProvider;
        //}

        protected override DownloadSummary DownloadCore(Uri uri, Stream outputStream, ProgressUpdateCallback progress, CancellationToken cancellationToken, DownloadContext downloadContext)
        {
            if (!uri.IsFile && !uri.IsUnc)
                throw new ArgumentException("Expected file or UNC path", "uri");

            return new DownloadSummary
            {
                DownloadedSize = Utilities.CopyFileToStream(uri.LocalPath, outputStream, progress, cancellationToken)
            };
        }

        //private readonly IServiceProvider serviceProvider;
    }
}