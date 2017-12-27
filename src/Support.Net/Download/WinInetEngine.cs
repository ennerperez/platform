using Platform.Support.IO;
using Platform.Support.Net.Download.WinInet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Platform.Support.Net.Download
{
    internal class WinInetEngine : DownloadEngineBase
    {
        public WinInetEngine() : base("WinInet", new DownloadSource[] { DownloadSource.Internet })
        {
        }

        //public WinInetEngine(IServiceProvider serviceProvider) : base("WinInet", new DownloadSource[] { DownloadSource.Internet })
        //{
        //    this.serviceProvider = serviceProvider;
        //    this.logger = serviceProvider.GetService<ILogger>(false);
        //}

        protected override DownloadSummary DownloadCore(Uri uri, Stream stream, ProgressUpdateCallback progress, CancellationToken cancellationToken, DownloadContext downloadContext)
        {
            WrappedWebException ex = null;
            DownloadSummary downloadSummary = new DownloadSummary();
            ProxyResolution proxyResolution = ProxyResolution.Default;
            using (ManagedWinInet managedWinInet = new ManagedWinInet()) // this.serviceProvider))
            {
                do
                {
                    uint proxyConfigSetting = this.GetProxyConfigSetting(ref proxyResolution);
                    managedWinInet.InternetOpen(proxyConfigSetting);
                    try
                    {
                        downloadSummary = managedWinInet.DownloadFile(uri, stream, progress, cancellationToken, downloadContext);
                        downloadSummary.ProxyResolution = proxyResolution.ToString();
                        managedWinInet.InternetClose();
                        break;
                    }
                    catch (WrappedWebException ex2)
                    {
                        ex = (ex ?? ex2);
                        proxyResolution++;
                        if (proxyResolution == ProxyResolution.Error)
                        {
                            //ILogger logger = this.logger;
                            //if (logger != null)
                            //{
                            //    logger.WriteVerbose(string.Format("WinInet failed '{0}' with '{1}'.", uri.AbsoluteUri, ex2.Message), new object[0]);
                            //}
                            throw ex;
                        }
                        //ILogger logger2 = this.logger;
                        //if (logger2 != null)
                        //{
                        //    logger2.WriteVerbose(string.Format("WinInet error '{0}' {1} - proxy setting '{2}' - '{3}'.", new object[]
                        //    {
                        //        ex2.Status.ToString(),
                        //        ex2.Message,
                        //        proxyResolution.ToString(),
                        //        uri.AbsoluteUri
                        //    }), new object[0]);
                        //}
                    }
                    catch (OperationCanceledException)
                    {
                        throw;
                    }
                    catch (Exception ex3)
                    {
                        ex3.DebugThis();
                        //this.logger.WriteVerbose("WinInet failed to download: {0}", new object[]
                        //{
                        //    ex3.Message
                        //});
                        throw;
                    }
                    managedWinInet.ResetHandle();
                }
                while (proxyResolution != ProxyResolution.Error);
            }
            return downloadSummary;
        }

        private uint GetProxyConfigSetting(ref ProxyResolution proxyResolution)
        {
            switch (proxyResolution)
            {
                case ProxyResolution.Default:
                    return 0u;

                case ProxyResolution.DefaultCredentialsOrNoAutoProxy:
                    proxyResolution++;
                    return 4u;

                case ProxyResolution.DirectAccess:
                    return 1u;
            }
            return 0u;
        }

        //private readonly IServiceProvider serviceProvider;

        //private readonly ILogger logger;
    }
}