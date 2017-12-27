using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Platform.Support.Net.Download.Bits
{
    internal enum BG_JOB_TYPE
    {
        BG_JOB_TYPE_DOWNLOAD
    }

    internal enum BG_JOB_STATE
    {
        BG_JOB_STATE_QUEUED,
        BG_JOB_STATE_CONNECTING,
        BG_JOB_STATE_TRANSFERRING,
        BG_JOB_STATE_SUSPENDED,
        BG_JOB_STATE_ERROR,
        BG_JOB_STATE_TRANSIENT_ERROR,
        BG_JOB_STATE_TRANSFERRED,
        BG_JOB_STATE_ACKNOWLEDGED,
        BG_JOB_STATE_CANCELLED,
        BG_JOB_STATE_UPDATE_AVAILABLE = 1001,
        BG_JOB_STATE_VALIDATION_SUCCESS,
        BG_JOB_STATE_VALIDATION_FAILED
    }

    [CLSCompliant(false)]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct _BG_FILE_INFO
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string RemoteName;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string LocalName;
    }

    [CLSCompliant(false)]
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    internal struct _BG_FILE_PROGRESS
    {
        public ulong BytesTotal;

        public ulong BytesTransferred;

        public int Completed;
    }

    [CLSCompliant(false)]
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    internal struct _BG_JOB_PROGRESS
    {
        public ulong BytesTotal;

        public ulong BytesTransferred;

        public uint FilesTotal;

        public uint FilesTransferred;
    }

    [CLSCompliant(false)]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct _FILETIME
    {
        public uint dwLowDateTime;

        public uint dwHighDateTime;
    }

    [CLSCompliant(false)]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct _BG_JOB_TIMES
    {
        public _FILETIME CreationTime;

        public _FILETIME ModificationTime;

        public _FILETIME TransferCompletionTime;
    }

    internal enum BG_ERROR_CONTEXT
    {
        BG_ERROR_CONTEXT_NONE,
        BG_ERROR_CONTEXT_UNKNOWN,
        BG_ERROR_CONTEXT_GENERAL_QUEUE_MANAGER,
        BG_ERROR_CONTEXT_QUEUE_MANAGER_NOTIFICATION,
        BG_ERROR_CONTEXT_LOCAL_FILE,
        BG_ERROR_CONTEXT_REMOTE_FILE,
        BG_ERROR_CONTEXT_GENERAL_TRANSPORT
    }

    internal enum BG_JOB_PRIORITY
    {
        BG_JOB_PRIORITY_FOREGROUND,
        BG_JOB_PRIORITY_HIGH,
        BG_JOB_PRIORITY_NORMAL,
        BG_JOB_PRIORITY_LOW
    }

    internal enum BG_JOB_PROXY_USAGE
    {
        BG_JOB_PROXY_USAGE_PRECONFIG,
        BG_JOB_PROXY_USAGE_NO_PROXY,
        BG_JOB_PROXY_USAGE_OVERRIDE,
        BG_JOB_PROXY_USAGE_AUTODETECT
    }

    internal enum BG_CERT_STORE_LOCATION
    {
        BG_CERT_STORE_LOCATION_CURRENT_USER,
        BG_CERT_STORE_LOCATION_LOCAL_MACHINE,
        BG_CERT_STORE_LOCATION_CURRENT_SERVICE,
        BG_CERT_STORE_LOCATION_SERVICES,
        BG_CERT_STORE_LOCATION_USERS,
        BG_CERT_STORE_LOCATION_CURRENT_USER_GROUP_POLICY,
        BG_CERT_STORE_LOCATION_LOCAL_MACHINE_GROUP_POLICY,
        BG_CERT_STORE_LOCATION_LOCAL_MACHINE_ENTERPRISE
    }

    [CLSCompliant(false)]
    internal enum BG_HTTP_SECURITY_FLAGS : uint
    {
        BG_SSL_ENABLE_CRL_CHECK = 1u,
        BG_SSL_IGNORE_CERT_CN_INVALID,
        BG_SSL_IGNORE_CERT_DATE_INVALID = 4u,
        BG_SSL_IGNORE_UNKNOWN_CA = 8u,
        BG_SSL_IGNORE_CERT_WRONG_USAGE = 16u,
        BG_HTTP_REDIRECT_POLICY_ALLOW_SILENT = 0u,
        BG_HTTP_REDIRECT_POLICY_ALLOW_REPORT = 256u,
        BG_HTTP_REDIRECT_POLICY_DISALLOW = 512u,
        BG_HTTP_REDIRECT_POLICY_MASK = 1792u,
        BG_HTTP_REDIRECT_POLICY_ALLOW_HTTPS_TO_HTTP = 2048u
    }
}