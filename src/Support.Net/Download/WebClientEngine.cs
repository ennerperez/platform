using Platform.Support.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading;

namespace Platform.Support.Net.Download
{
    internal class WebClientEngine : DownloadEngineBase
    {
        public WebClientEngine() : base("WebClient", new DownloadSource[] { DownloadSource.Internet })
        {
            this.helper = new DownloadHelpers();
        }

        //public WebClientEngine(IServiceProvider serviceProvider) : base("WebClient", new DownloadSource[]
        //{
        //    DownloadSource.Internet
        //})
        //{
        //    this.serviceProvider = serviceProvider;
        //    this.logger = serviceProvider.GetService<ILogger>(false);
        //    this.helper = new DownloadHelpers(serviceProvider);
        //}

        // Token: 0x0600003F RID: 63 RVA: 0x00002498 File Offset: 0x00000698
        protected override DownloadSummary DownloadCore(Uri uri, Stream stream, ProgressUpdateCallback progress, CancellationToken cancellationToken, DownloadContext downloadContext)
        {
            DownloadSummary downloadSummary = new DownloadSummary();
            using (HttpWebResponse webResponse = this.GetWebResponse(uri, downloadContext, ref downloadSummary))
            {
                if (webResponse != null)
                {
                    using (Stream responseStream = webResponse.GetResponseStream())
                    {
                        string value = webResponse.Headers["Content-Length"];
                        if (string.IsNullOrEmpty(value))
                        {
                            throw new IOException("Error: Content-length is missing from response header.");
                        }
                        long num = (long)Convert.ToInt32(value);
                        if (num.Equals(0L))
                        {
                            throw new IOException("Error: Response stream length is 0.");
                        }
                        long num2 = 0L;
                        bool flag;
                        for (; ; )
                        {
                            cancellationToken.ThrowIfCancellationRequested();
                            byte[] buffer = new byte[32768];
                            int num3 = responseStream.Read(buffer, 0, 32768);
                            flag = (num3 < 0);
                            if (num3 <= 0)
                            {
                                break;
                            }
                            num2 += (long)num3;
                            stream.Write(buffer, 0, num3);
                            progress?.Invoke(new ProgressUpdateStatus(num2, num, 0.0));
                        }
                        if (flag)
                        {
                            throw new IOException("Internal error while downloading the stream.");
                        }
                        if (num2 != num)
                        {
                            throw new IOException("Strings.Error_DownloadFailed_SizeMismatch");
                        }
                        downloadSummary.DownloadedSize = num2;
                    }
                }
            }
            return downloadSummary;
        }

        // Token: 0x06000040 RID: 64 RVA: 0x000025C8 File Offset: 0x000007C8
        private HttpWebResponse GetWebResponse(Uri uri, DownloadContext downloadContext, ref DownloadSummary downloadSummary)
        {
            ProxyResolution proxyResolution = ProxyResolution.Default;
            while (proxyResolution != ProxyResolution.Error)
            {
                HttpWebResponse httpWebResponse = null;
                bool flag = true;
                try
                {
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                    httpWebRequest.UserAgent = helper.UserAgent;
                    httpWebRequest.KeepAlive = true;
                    httpWebRequest.Timeout = 120000;
                    string text;
                    if (downloadContext == null)
                    {
                        text = null;
                    }
                    else
                    {
                        DownloadCookie cookie = downloadContext.Cookie;
                        text = (cookie?.Value);
                    }
                    string value = text;
                    if (!string.IsNullOrEmpty(value))
                    {
                        httpWebRequest.Headers.Add(HttpRequestHeader.Cookie, value);
                    }
                    HttpRequestCachePolicy cachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                    httpWebRequest.CachePolicy = cachePolicy;
                    switch (proxyResolution)
                    {
                        case ProxyResolution.DefaultCredentialsOrNoAutoProxy:
                            httpWebRequest.UseDefaultCredentials = true;
                            break;

                        case ProxyResolution.NetworkCredentials:
                            httpWebRequest.UseDefaultCredentials = false;
                            httpWebRequest.Proxy = WebRequest.GetSystemWebProxy();
                            httpWebRequest.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
                            break;

                        case ProxyResolution.DirectAccess:
                            httpWebRequest.Proxy = null;
                            break;
                    }
                    httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    string text2 = httpWebResponse.ResponseUri.ToString();
                    if (!string.IsNullOrEmpty(text2) && !uri.ToString().Equals(text2, StringComparison.InvariantCultureIgnoreCase))
                    {
                        downloadSummary.FinalUri = text2;
                        //ILogger logger = this.logger;
                        //if (logger != null)
                        //{
                        //    logger.WriteVerbose(string.Format("Uri '{0}' redirected to '{1}'", uri.ToString(), text2), new object[0]);
                        //}
                    }
                    HttpStatusCode statusCode = httpWebResponse.StatusCode;
                    if (statusCode <= HttpStatusCode.UseProxy)
                    {
                        if (statusCode == HttpStatusCode.OK)
                        {
                            downloadSummary.ProxyResolution = proxyResolution.ToString();
                            flag = false;
                            return httpWebResponse;
                        }
                        if (statusCode != HttpStatusCode.UseProxy)
                        {
                            goto IL_221;
                        }
                    }
                    else if (statusCode != HttpStatusCode.ProxyAuthenticationRequired && statusCode != HttpStatusCode.GatewayTimeout)
                    {
                        goto IL_221;
                    }
                    proxyResolution++;
                    if (proxyResolution == ProxyResolution.Error)
                    {
                        //ILogger logger2 = this.logger;
                        //if (logger2 != null)
                        //{
                        //    logger2.WriteVerbose(string.Format("WebResponse error '{0}' with '{1}'.", httpWebResponse.StatusCode, uri.ToString()), new object[0]);
                        //}
                        this.helper.ThrowWrappedWebException((int)httpWebResponse.StatusCode, "WebRequest.GetResponse", downloadSummary.FinalUri);
                        continue;
                    }
                    //ILogger logger3 = this.logger;
                    //if (logger3 == null)
                    //{
                    //    continue;
                    //}
                    //logger3.WriteVerbose(string.Format("WebResponse error '{0}' - '{1}'. Reattempt with proxy set to '{2}'", httpWebResponse.StatusCode, uri.AbsoluteUri, proxyResolution.ToString()), new object[0]);
                    continue;
                    IL_221:
                    proxyResolution = ProxyResolution.Error;
                    //ILogger logger4 = this.logger;
                    //if (logger4 != null)
                    //{
                    //    logger4.WriteVerbose(string.Format("WebResponse error '{0}'  - '{1}'.", httpWebResponse.StatusCode, uri.AbsoluteUri), new object[0]);
                    //}
                    this.helper.ThrowWrappedWebException((int)httpWebResponse.StatusCode, "WebRequest.GetResponse", downloadSummary.FinalUri);
                }
                catch (WrappedWebException ex)
                {
                    if (proxyResolution == ProxyResolution.Error)
                    {
                        ex.DebugThis();
                        //ILogger logger5 = this.logger;
                        //if (logger5 != null)
                        //{
                        //    logger5.WriteVerbose(string.Format("WebResponse exception '{0}' with '{1}'.", ex.Status, uri.ToString()), new object[0]);
                        //}
                        throw;
                    }
                }
                catch (WebException ex2)
                {
                    //ILogger logger6 = this.logger;
                    //if (logger6 != null)
                    //{
                    //    logger6.WriteVerbose(string.Format("WebClient error '{0}' - proxy setting '{1}' - '{2}'.", ex2.Status.ToString(), proxyResolution.ToString(), uri.AbsoluteUri), new object[0]);
                    //}
                    WebExceptionStatus status = ex2.Status;
                    switch (status)
                    {
                        case WebExceptionStatus.NameResolutionFailure:
                        case WebExceptionStatus.ConnectFailure:
                        case WebExceptionStatus.SendFailure:
                        case WebExceptionStatus.ProtocolError:
                            break;

                        case WebExceptionStatus.ReceiveFailure:
                        case WebExceptionStatus.PipelineFailure:
                        case WebExceptionStatus.RequestCanceled:
                            goto IL_33D;
                        default:
                            if (status != WebExceptionStatus.ProxyNameResolutionFailure)
                            {
                                goto IL_33D;
                            }
                            break;
                    }
                    proxyResolution++;
                    goto IL_33F;
                    IL_33D:
                    proxyResolution = ProxyResolution.Error;
                    IL_33F:
                    if (proxyResolution == ProxyResolution.Error)
                    {
                        //ILogger logger7 = this.logger;
                        //if (logger7 != null)
                        //{
                        //    logger7.WriteVerbose(string.Format("WebClient failed in '{0}' with '{1}' - '{2}'.", uri.AbsoluteUri, ex2.Message, uri.AbsoluteUri), new object[0]);
                        //}
                        throw;
                    }
                }
                catch (Exception ex3)
                {
                    ex3.DebugThis();
                    //ILogger logger8 = this.logger;
                    //if (logger8 != null)
                    //{
                    //    logger8.WriteError(ex3, "General exception error in web client.", new object[0]);
                    //}
                    throw;
                }
                finally
                {
                    if (httpWebResponse != null && flag)
                    {
                        httpWebResponse.Close();
                    }
                }
            }
            return null;
        }

        //private readonly IServiceProvider serviceProvider;

        //private readonly ILogger logger;

        private readonly DownloadHelpers helper;
    }
}