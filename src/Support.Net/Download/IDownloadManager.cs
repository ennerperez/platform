using Platform.Support.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Platform.Support.Net.Download
{
    public interface IDownloadManager
    {
        int MaxDownloads { get; set; }

        IEnumerable<string> DefaultEngines { get; set; }

        IEnumerable<string> AllEngines { get; }

        Task<DownloadSummary> Download(Uri uri, Stream outputStream, ProgressUpdateCallback progress, CancellationToken cancellationToken, DownloadContext downloadContext = null, bool verifySignature = false);
    }
}