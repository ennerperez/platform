using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support.Net.Download
{
    public class DownloadFailureInformation
    {
        public Exception Exception { get; set; }

        public string Engine { get; set; }
    }
}