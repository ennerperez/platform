using Platform.Support.Windows;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Platform.Support.Net.Download
{
    internal class Utilities
    {
        internal string UserAgent => $"{Assembly.GetExecutingAssembly().GetName().Name}/{Assembly.GetExecutingAssembly().GetName().Version}({Environment.OSVersion.VersionString})";

        public Utilities()
        {
        }

        //public DownloadHelpers(IServiceProvider serviceProvider)
        //{
        //    this.serviceProvider = serviceProvider;
        //    this.logger = serviceProvider.GetService<ILog>(false);
        //}

        public string GetResponseHeaders(IntPtr inetFileHandle)
        {
            byte[] array = new byte[32768];
            int count = 32768;
            if (!Windows.WinInet.HttpQueryInfo(inetFileHandle, 22u, array, ref count, out int num))
            {
                if (Marshal.GetLastWin32Error() == 122)
                {
                    //this.logger.WriteVerbose("Insufficient buffer size to store headers.", new object[0]);
                }
                return string.Empty;
            }
            return Encoding.Unicode.GetString(array, 0, count);
        }

        public string GetContentEncoding(IntPtr inetFileHandle)
        {
            int num = 0;
            int num2 = 100;
            byte[] array = new byte[num2];
            string result = string.Empty;
            try
            {
                if (Windows.WinInet.HttpQueryInfo(inetFileHandle, 29u, array, ref num2, out num))
                    result = Encoding.Unicode.GetString(array, 0, num2);
            }
            catch
            {
            }
            return result;
        }

        public int GetContentLength(IntPtr inetFileHandle)
        {
            int result = 1;
            try
            {
                int num = 100;
                byte[] array = new byte[num];
                if (!Windows.WinInet.HttpQueryInfo(inetFileHandle, 5u, array, ref num, out int num2))
                {
                    //this.logger.WriteVerbose("Querying HTTP information failed with error code: {0}", new object[]
                    //{
                    //    Marshal.GetLastWin32Error()
                    //});
                }
                else
                {
                    result = int.Parse(Encoding.Unicode.GetString(array, 0, num), CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {
                ex.DebugThis();
                //this.logger.WriteVerbose("Error getting content length header: " + ex.Message, new object[0]);
            }
            return result;
        }

        public void ThrowGetLastErrorException(string offendingFunction)
        {
            Win32ErrorCode lastWin32Error = (Win32ErrorCode)Marshal.GetLastWin32Error();
            int num = Win32Error.MakeHRFromErrorCode(lastWin32Error);
            throw new WrappedWebException(num, string.Format(CultureInfo.InvariantCulture, "Strings.Error_WinInet_Failure_Arg3", new object[]
            {
                offendingFunction,
                num,
                Win32Error.GetMessage(lastWin32Error)
            }));
        }

        public void ThrowWrappedWebException(int errorCode, string functionName, string message)
        {
            int num = -2147024896 | errorCode;
            if (string.IsNullOrEmpty(message))
            {
                message = "unspecified";
            }
            throw new WrappedWebException(num, string.Format(CultureInfo.InvariantCulture, "Strings.Error_WinInet_Failure_Arg3", new object[]
            {
                functionName,
                num,
                message
            }));
        }

        public void GetInetErrorInfo(string function, out int errorCode, out string message, string uri = null)
        {
            errorCode = Marshal.GetLastWin32Error();
            if (errorCode == 122)
            {
                int num = 0;
                try
                {
                    if (Windows.WinInet.InternetGetLastResponseInfo(out errorCode, null, ref num))
                    {
                        StringBuilder stringBuilder = new StringBuilder(num + 1);
                        Windows.WinInet.InternetGetLastResponseInfo(out errorCode, stringBuilder, ref num);
                        message = stringBuilder.ToString();
                    }
                    else
                    {
                        message = "Unable to get last response info.";
                    }
                    goto IL_5E;
                }
                catch (Exception ex)
                {
                    message = string.Format("Exception with InternetGetLastResponseInfo {0}", ex.Message);
                    goto IL_5E;
                }
            }
            message = Kernel32.GetMessage(errorCode);
            IL_5E:
            Win32ErrorCode win32ErrorCode = (Win32ErrorCode)errorCode;
            //ILog logger = this.logger;
            //if (logger == null)
            //{
            //    return;
            //}
            //logger.WriteVerbose(string.Format("Error in '{0}' with '{1}' - '{2}'.", function, win32ErrorCode.ToString(), message), new object[0]);
        }

        public void InternetCloseFileHandle(IntPtr fileHandle)
        {
            if (fileHandle != IntPtr.Zero && !Windows.WinInet.InternetCloseHandle(fileHandle))
            {
                //this.logger.WriteVerbose("Failed to close connection: {0}.", new object[]
                //{
                //    Marshal.GetLastWin32Error()
                //});
            }
        }

        //private readonly IServiceProvider serviceProvider;

        //private readonly ILog logger;
    }
}