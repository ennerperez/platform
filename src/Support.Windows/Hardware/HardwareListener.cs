using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Support.Windows.Hardware
{
    public abstract class HardwareListener
    {
        public const int WH_KEYBOARD_LL = 13;
        public const int WH_MOUSE_LL = 14;

        public HardwareListener(int hookId)
        {
            _hookId = hookId;
            _proc = HookCallback;
            _hook = SetHook(_proc);
        }

        internal int _hookId;
        internal User32.LowLevelProc _proc;
        internal IntPtr _hook = IntPtr.Zero;

        internal IntPtr LastEvent { get; private set; }
        public bool IsHeld { get; internal set; }

        #region NativeMethods

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //internal static extern IntPtr SetWindowsHookEx(int idHook, LowLevelProc lpfn, IntPtr hMod, uint dwThreadId);

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //internal static extern bool UnhookWindowsHookEx(IntPtr hhk);

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        //[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //internal static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion NativeMethods

        //internal delegate IntPtr LowLevelProc(int nCode, IntPtr wParam, IntPtr lParam);

        internal virtual IntPtr SetHook(User32.LowLevelProc proc)
        {
            IntPtr hook = IntPtr.Zero;
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
                hook = User32.SetWindowsHookEx(_hookId, proc, User32.GetModuleHandle(Environment.OSVersion.Version < new Version(6, 2) ? "user32" : curModule.ModuleName), 0);

            if (hook == IntPtr.Zero) throw new System.ComponentModel.Win32Exception();
            return hook;
        }

        internal virtual IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (!IsHeld)
                LastEvent = wParam;
            return User32.CallNextHookEx(_hook, nCode, wParam, lParam);
        }

        [Conditional("DEBUG")]
        internal void OnDebug(EventArgs e)
        {
            Debug.Invoke(this, e);
        }

        internal event EventHandler Debug;

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue && disposing)
                User32.UnhookWindowsHookEx(_hook);
            disposedValue = true;
        }

        // ~HardwareListener() {
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
            // GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}