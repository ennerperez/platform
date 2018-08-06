using Platform.Support.IO;
using Platform.Support.Windows;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Platform.Support.Net.Download.WinInet
{
    internal class ManagedWinInet : DisposableObject
    {
        public ManagedWinInet()
        {
            this.helper = new Utilities();
        }

        //public ManagedWinInet(IServiceProvider serviceProvider)
        //{
        //    this.serviceProvider = serviceProvider;
        //    this.logger = serviceProvider.GetService(false);
        //    this.helper = new DownloadHelpers(serviceProvider);
        //}

        public void InternetOpen(uint internetAccessType)
        {
            this.inetHandle = Windows.WinInet.InternetOpen(helper.UserAgent, internetAccessType, null, null, 0u);
            if (this.inetHandle.IsInvalid)
                this.helper.ThrowGetLastErrorException("InternetOpen");
        }

        public void InternetClose()
        {
            if (this.inetHandle != null)
            {
                this.inetHandle.Dispose();
                this.inetHandle.SetHandleAsInvalid();
            }
        }

        public void ResetHandle()
        {
            if (this.inetHandle != null)
            {
                this.inetHandle.Dispose();
                this.inetHandle = null;
            }
        }

        internal DownloadSummary DownloadFile(Uri uri, Stream outStream, ProgressUpdateCallback progressCallback, CancellationToken cancellationToken, DownloadContext downloadContext)
        {
            DownloadSummary downloadSummary = new DownloadSummary();
            if (this.inetHandle.IsInvalid)
            {
                throw new InvalidOperationException("InternetOpen has not been called yet");
            }
            IntPtr zero = IntPtr.Zero;
            DownloadSummary result;
            try
            {
                Uri uri2 = this.OpenUrlAndFollowRedirects((downloadContext != null) ? downloadContext.Cookie : null, uri, ref zero, ref downloadSummary);
                this.SetInternetTimeout(zero);
                long num = 0L;
                byte[] buffer = new byte[32768];
                downloadSummary.DownloadedSize = (long)this.helper.GetContentLength(zero);
                using (Stream internetStream = this.GetInternetStream(zero))
                {
                    int num2;
                    while ((num2 = internetStream.Read(buffer, 0, 32768)) != 0)
                    {
                        num += (long)num2;
                        cancellationToken.ThrowIfCancellationRequested();
                        outStream.Write(buffer, 0, num2);
                        progressCallback?.Invoke(new ProgressUpdateStatus(num, downloadSummary.DownloadedSize, 0.0));
                    }
                }
                if (!uri.Equals(uri2))
                {
                    downloadSummary.FinalUri = uri2.AbsoluteUri;
                }
                result = downloadSummary;
            }
            catch
            {
                throw;
            }
            finally
            {
                this.helper.InternetCloseFileHandle(zero);
                zero = IntPtr.Zero;
            }
            return result;
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing && this.inetHandle != null && !this.inetHandle.IsInvalid)
                this.inetHandle.Dispose();
            base.Dispose(disposing);
        }

        private void SetInternetTimeout(IntPtr fileHandle)
        {
            IntPtr intPtr = IntPtr.Zero;
            try
            {
                intPtr = Marshal.AllocHGlobal(4);
                Marshal.WriteInt32(intPtr, 120000);
                //if (!NativeMethods.InternetSetOption(fileHandle, 2u, intPtr, 4))
                //{
                //    this.logger.WriteVerbose("Failed to set connect timeout option.", new object[0]);
                //}
                //if (!NativeMethods.InternetSetOption(fileHandle, 5u, intPtr, 4))
                //{
                //    this.logger.WriteVerbose("Failed to set send timeout option.", new object[0]);
                //}
                //if (!NativeMethods.InternetSetOption(fileHandle, 6u, intPtr, 4))
                //{
                //    this.logger.WriteVerbose("Failed to set receive timeout option.", new object[0]);
                //}
            }
            finally
            {
                if (intPtr != IntPtr.Zero)
                    Marshal.FreeHGlobal(intPtr);
            }
        }

        private Stream GetInternetStream(IntPtr inetFile)
        {
            if (this.helper.GetContentEncoding(inetFile).IndexOf("gzip", StringComparison.OrdinalIgnoreCase) != -1)
            {
                return new GZipStream(this.NewWinInetInternetReadStreamForGZipStream(inetFile), CompressionMode.Decompress, false);
            }
            return new WinInetInternetReadStream(inetFile); // this.serviceProvider, inetFile);
        }

        private Stream NewWinInetInternetReadStreamForGZipStream(IntPtr inetFile)
        {
            return new WinInetInternetReadStream(inetFile); // this.serviceProvider, inetFile);
        }

        private Uri OpenUrlAndFollowRedirects(DownloadCookie cookie, Uri uri, ref IntPtr fileHandle, ref DownloadSummary downloadSummary)
        {
            byte[] array = new byte[32768];
            string text = "Accept-Encoding: gzip";
            Uri uri2 = new Uri(uri.AbsoluteUri);
            int num = 0;
            while (num++ < 20)
            {
                if (!string.IsNullOrEmpty((cookie != null) ? cookie.Url : null) && !string.IsNullOrEmpty((cookie != null) ? cookie.Value : null) && !Windows.WinInet.InternetSetCookie((cookie != null) ? cookie.Url : null, null, (cookie != null) ? cookie.Value : null))
                {
                    //this.logger.WriteVerbose("Cannot set cookie: {0}:{1}", new object[]
                    //{
                    //    cookie.Url,
                    //    cookie.Value
                    //});
                    throw new InvalidOperationException(string.Format("Internal error: unable to set the cookie. {0}:{1}", cookie.Url, cookie.Value));
                }
                int errorCode = 0;
                fileHandle = Windows.WinInet.InternetOpenUrl(this.inetHandle, uri2.AbsoluteUri, text, text.Length, 2097152, IntPtr.Zero);
                if (fileHandle == IntPtr.Zero)
                {
                    string message;
                    this.helper.GetInetErrorInfo("InternetOpenUrl", out errorCode, out message, null);
                    this.helper.ThrowWrappedWebException(errorCode, "InternetOpenUrl", message);
                }
                int count = array.Length;
                int num2 = 0;
                if (!Windows.WinInet.HttpQueryInfo(fileHandle, 19u, array, ref count, out num2))
                {
                    string message;
                    this.helper.GetInetErrorInfo("HttpQueryInfo", out errorCode, out message, null);
                    this.helper.InternetCloseFileHandle(fileHandle);
                    this.helper.ThrowWrappedWebException(errorCode, "HttpQueryInfo", message);
                }
                string @string = Encoding.Unicode.GetString(array, 0, count);
                if (string.IsNullOrEmpty(@string) || string.IsNullOrEmpty(@string.Trim()))
                {
                    throw new IOException(string.Format(CultureInfo.CurrentCulture, "Strings.Error_InvalidResponseCode_Arg1", new object[]
                    {
                        @string
                    }));
                }
                if (string.Equals(@string, "200", StringComparison.Ordinal))
                {
                    break;
                }
                if (string.Equals(@string, "301", StringComparison.Ordinal) || string.Equals(@string, "302", StringComparison.Ordinal))
                {
                    count = array.Length;
                    num2 = 0;
                    if (!Windows.WinInet.HttpQueryInfo(fileHandle, 33u, array, ref count, out num2))
                    {
                        string message;
                        this.helper.GetInetErrorInfo("HttpQueryInfo", out errorCode, out message, null);
                        this.helper.InternetCloseFileHandle(fileHandle);
                        this.helper.ThrowWrappedWebException(errorCode, "HttpQueryInfo-Redirect", message);
                    }
                    if (!Windows.WinInet.InternetCloseHandle(fileHandle))
                    {
                        //this.logger.WriteVerbose("Failed to close connection: {0}.", new object[]
                        //{
                        //    Marshal.GetLastWin32Error()
                        //});
                    }
                    fileHandle = IntPtr.Zero;
                    Uri uri3;
                    if (Uri.TryCreate(Encoding.Unicode.GetString(array, 0, count), UriKind.RelativeOrAbsolute, out uri3))
                    {
                        if (!uri3.IsAbsoluteUri)
                        {
                            uri2 = new Uri(uri2, uri3);
                        }
                        else
                        {
                            uri2 = uri3;
                        }
                    }
                }
                else
                {
                    int errorCode2 = Convert.ToInt32(@string);
                    this.helper.ThrowWrappedWebException(errorCode2, "HttpQueryInfo-ResponseCode", string.Format("Url '{0}'", uri2.AbsoluteUri));
                }
            }
            if (num > 20)
            {
                throw new InvalidOperationException("Unable to download file. Maximum number of redirects exceeded");
            }
            return uri2;
        }

        //private readonly IServiceProvider serviceProvider;

        //private readonly ILogger logger;

        private readonly Utilities helper;

        private SafeInetHandle inetHandle;
    }
}