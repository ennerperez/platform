using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Support.Windows.Hardware
{
    public enum MouseButtons
    {
        //
        // Summary:
        //     No mouse button was pressed.
        None = 0,

        //
        // Summary:
        //     The left mouse button was pressed.
        Left = 1048576,

        //
        // Summary:
        //     The right mouse button was pressed.
        Right = 2097152,

        //
        // Summary:
        //     The middle mouse button was pressed.
        Middle = 4194304,

        //
        // Summary:
        //     The first XButton was pressed.
        XButton1 = 8388608,

        //
        // Summary:
        //     The second XButton was pressed.
        XButton2 = 16777216
    }

    //
    // Summary:
    //     Provides data for the System.Windows.Forms.Control.MouseUp, System.Windows.Forms.Control.MouseDown,
    //     and System.Windows.Forms.Control.MouseMove events.
    [ComVisible(true)]
    public class MouseEventArgs : EventArgs
    {
        //
        // Summary:
        //     Initializes a new instance of the System.Windows.Forms.MouseEventArgs class.
        //
        // Parameters:
        //   button:
        //     One of the System.Windows.Forms.MouseButtons values that indicate which mouse
        //     button was pressed.
        //
        //   clicks:
        //     The number of times a mouse button was pressed.
        //
        //   x:
        //     The x-coordinate of a mouse click, in pixels.
        //
        //   y:
        //     The y-coordinate of a mouse click, in pixels.
        //
        //   delta:
        //     A signed count of the number of detents the wheel has rotated.
        public MouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta)
        {
            Button = button;
            Location = new POINT() { x = x, y = y };
            Delta = delta;
        }

        //
        // Summary:
        //     Gets which mouse button was pressed.
        //
        // Returns:
        //     One of the System.Windows.Forms.MouseButtons values.
        public MouseButtons Button { get; }

        //
        // Summary:
        //     Gets the number of times the mouse button was pressed and released.
        //
        // Returns:
        //     An System.Int32 that contains the number of times the mouse button was pressed
        //     and released.
        public int Clicks { get; }

        //
        // Summary:
        //     Gets the x-coordinate of the mouse during the generating mouse event.
        //
        // Returns:
        //     The x-coordinate of the mouse, in pixels.
        public int X { get; }

        //
        // Summary:
        //     Gets the y-coordinate of the mouse during the generating mouse event.
        //
        // Returns:
        //     The y-coordinate of the mouse, in pixels.
        public int Y { get; }

        //
        // Summary:
        //     Gets a signed count of the number of detents the mouse wheel has rotated, multiplied
        //     by the WHEEL_DELTA constant. A detent is one notch of the mouse wheel.
        //
        // Returns:
        //     A signed count of the number of detents the mouse wheel has rotated, multiplied
        //     by the WHEEL_DELTA constant.
        public int Delta { get; }

        //
        // Summary:
        //     Gets the location of the mouse during the generating mouse event.
        //
        // Returns:
        //     A System.Drawing.Point that contains the x- and y- mouse coordinates, in pixels,
        //     relative to the upper-left corner of the form.
        public POINT Location { get; }
    }

    //
    // Summary:
    //     Represents the method that will handle the MouseDown, MouseUp, or MouseMove event
    //     of a form, control, or other component.
    //
    // Parameters:
    //   sender:
    //     The source of the event.
    //
    //   e:
    //     A System.Windows.Forms.MouseEventArgs that contains the event data.
    public delegate void MouseEventHandler(object sender, MouseEventArgs e);

    public class MouseListener : HardwareListener
    {
        public MouseListener() : base(WH_MOUSE_LL)
        {
        }

        private const uint WM_MOVE = 0x0200;
        private const uint WM_LEFTDOWN = 0x0201;
        private const uint WM_LEFTUP = 0x0202;
        private const uint WM_RIGHTDOWN = 0x0204;
        private const uint WM_RIGHTUP = 0x0205;
        private const uint WM_MIDDLEDOWN = 0x0207;
        private const uint WM_MIDDLEUP = 0x0208;
        private const uint WM_WHEEL = 0x020A;

        #region NativeMethods

        //[DllImport("user32.dll")]
        //internal static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

        [StructLayout(LayoutKind.Sequential)]
        internal struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;

            public override string ToString()
            {
                return $"pt:({pt.x},{pt.y}), mouseData:{mouseData}, flags:{flags}, time:{time}, dwExtraInfo:{dwExtraInfo}";
            }
        }

        #endregion NativeMethods

        internal override IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var button = MouseButtons.None;
                if (wParam == (IntPtr)WM_LEFTDOWN || wParam == (IntPtr)WM_LEFTUP)
                    button = MouseButtons.Left;
                else if (wParam == (IntPtr)WM_MIDDLEDOWN || wParam == (IntPtr)WM_MIDDLEUP)
                    button = MouseButtons.Middle;
                else if (wParam == (IntPtr)WM_RIGHTDOWN || wParam == (IntPtr)WM_RIGHTUP)
                    button = MouseButtons.Right;

                IsHeld = wParam == (IntPtr)WM_MOVE && (LastEvent == (IntPtr)WM_LEFTDOWN || LastEvent == (IntPtr)WM_MIDDLEDOWN || LastEvent == (IntPtr)WM_RIGHTDOWN);

                var hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                //OnDebug( new EventArgs<string>($"held: {IsHeld}, button:{button}, nCode:{nCode}, wParam:{wParam}, lParam:{lParam}, " + hookStruct.ToString()));

                if (wParam == (IntPtr)WM_LEFTDOWN || wParam == (IntPtr)WM_MIDDLEDOWN || wParam == (IntPtr)WM_RIGHTDOWN)
                {
                    MouseDown?.Invoke(null, new MouseEventArgs(button, 1, hookStruct.pt.x, hookStruct.pt.y, IsHeld ? 1 : 0));
                }
                else if (wParam == (IntPtr)WM_LEFTUP || wParam == (IntPtr)WM_MIDDLEUP || wParam == (IntPtr)WM_RIGHTUP)
                {
                    MouseUp?.Invoke(null, new MouseEventArgs(button, 1, hookStruct.pt.x, hookStruct.pt.y, IsHeld ? 1 : 0));
                    MousePress?.Invoke(null, new MouseEventArgs(button, 1, hookStruct.pt.x, hookStruct.pt.y, IsHeld ? 1 : 0));
                }
                else if (wParam == (IntPtr)WM_MOVE)
                {
                    MouseMove?.Invoke(null, new MouseEventArgs(button, 0, hookStruct.pt.x, hookStruct.pt.y, IsHeld ? 1 : 0));
                }
                else if (wParam == (IntPtr)WM_WHEEL)
                {
                    MouseWhell?.Invoke(null, new MouseEventArgs(button, 0, hookStruct.pt.x, hookStruct.pt.y, hookStruct.mouseData == 7864320 ? 1 : 0));
                }
            }
            return base.HookCallback(nCode, wParam, lParam);
        }

        public event MouseEventHandler MouseDown;

        public event MouseEventHandler MouseUp;

        public event MouseEventHandler MousePress;

        public event MouseEventHandler MouseMove;

        public event MouseEventHandler MouseWhell;
    }
}