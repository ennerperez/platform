using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Platform.Support.Net.Download.Bits
{
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("37668D37-507E-4160-9316-26306D150B12")]
    [CLSCompliant(false)]
    [ComImport]
    internal interface IBackgroundCopyJob
    {
        void AddFileSet(uint cFileCount, ref _BG_FILE_INFO pFileSet);

        void AddFile([MarshalAs(UnmanagedType.LPWStr)] string remoteUrl, [MarshalAs(UnmanagedType.LPWStr)] string localName);

        void EnumFiles([MarshalAs(UnmanagedType.Interface)] out IEnumBackgroundCopyFiles pEnum);

        void Suspend();

        void Resume();

        void Cancel();

        void Complete();

        void GetId(out Guid pVal);

        void GetType(out BG_JOB_TYPE pVal);

        void GetProgress(out _BG_JOB_PROGRESS pVal);

        void GetTimes(out _BG_JOB_TIMES pVal);

        void GetState(out BG_JOB_STATE pVal);

        void GetError([MarshalAs(UnmanagedType.Interface)] out IBackgroundCopyError ppError);

        void GetOwner([MarshalAs(UnmanagedType.LPWStr)] out string pVal);

        void SetDisplayName([MarshalAs(UnmanagedType.LPWStr)] string val);

        void GetDisplayName([MarshalAs(UnmanagedType.LPWStr)] out string pVal);

        void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string val);

        void GetDescription([MarshalAs(UnmanagedType.LPWStr)] out string pVal);

        void SetPriority(BG_JOB_PRIORITY val);

        void GetPriority(out BG_JOB_PRIORITY pVal);

        void SetNotifyFlags(uint val);

        void GetNotifyFlags(out uint pVal);

        void SetNotifyInterface([MarshalAs(UnmanagedType.IUnknown)] object val);

        void GetNotifyInterface([MarshalAs(UnmanagedType.IUnknown)] out object pVal);

        void SetMinimumRetryDelay(uint seconds);

        void GetMinimumRetryDelay(out uint seconds);

        void SetNoProgressTimeout(uint seconds);

        void GetNoProgressTimeout(out uint seconds);

        void GetErrorCount(out uint errors);

        void SetProxySettings(BG_JOB_PROXY_USAGE proxyUsage, [MarshalAs(UnmanagedType.LPWStr)] string proxyList, [MarshalAs(UnmanagedType.LPWStr)] string proxyBypassList);

        void GetProxySettings(out BG_JOB_PROXY_USAGE pProxyUsage, [MarshalAs(UnmanagedType.LPWStr)] out string pProxyList, [MarshalAs(UnmanagedType.LPWStr)] out string pProxyBypassList);

        void TakeOwnership();
    }
}