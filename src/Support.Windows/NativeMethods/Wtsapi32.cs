using System;
using System.Runtime.InteropServices;

namespace Platform.Support.Windows
{
    public class Wtsapi32
    {

        [DllImport(ExternDll.Wtsapi32, SetLastError = true)]
        public static extern int WTSEnumerateSessions(
            IntPtr hServer,
            int Reserved,
            int Version,
            ref IntPtr ppSessionInfo,
            ref int pCount);

        [DllImport(ExternDll.Wtsapi32)]
        public static extern uint WTSQueryUserToken(uint SessionId, ref IntPtr phToken);

    }

    public enum WTS_CONNECTSTATE_CLASS
    {
        WTSActive,
        WTSConnected,
        WTSConnectQuery,
        WTSShadow,
        WTSDisconnected,
        WTSIdle,
        WTSListen,
        WTSReset,
        WTSDown,
        WTSInit
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WTS_SESSION_INFO
    {
        public readonly UInt32 SessionID;

        [MarshalAs(UnmanagedType.LPStr)]
        public readonly String pWinStationName;

        public readonly WTS_CONNECTSTATE_CLASS State;
    }
}
