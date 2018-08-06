using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Support.Net.Download
{
    public class DownloadContext
    {
        //public IPackageIdentity PackageIdentity { get; set; }

        //public string TelemetryParentCorrelation { get; set; }

        //public string TelemetryAncestorWorkloads { get; set; }

        public DownloadCookie Cookie { get; set; }

        public string Sha256 { get; set; }
    }
}