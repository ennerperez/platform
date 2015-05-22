using Platform.Support.Windows;
using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Presentation.Windows.Forms.Providers
{
    [ProvideProperty("Shield", typeof(Button)), ToolboxItem(true), System.Drawing.ToolboxBitmap(typeof(UserAcountControlProvider), "UserAcountControl.bmp")]
    public sealed class UserAcountControlProvider : System.ComponentModel.Component, IExtenderProvider
    {

        //Private Declare Ansi Function SendMessage Lib "user32.dll" Alias "SendMessageA" ( hwnd As Integer,  wMsg As Integer,  wParam As Integer,  lParam As String) As Integer
        private const uint BCM_FIRST = 0x1600;

        private const uint BCM_SETSHIELD = (BCM_FIRST + 0xc);

        private Hashtable m_properties = new Hashtable();
        public bool CanExtend(object sender)
        {
            return sender is Button;
        }

        private class Properties
        {
            public bool Shield { get; set; }
        }
        private Properties EnsurePropertiesExists(object key)
        {
            Properties p = (Properties)m_properties[key];

            if (p == null)
            {
                p = new Properties();

                m_properties[key] = p;
            }
            return p;
        }


        [DefaultValue(false)]
        public bool GetShield(Button b)
        {
            return EnsurePropertiesExists(b).Shield;
        }


        public void SetShield(Button b, bool value)
        {
            EnsurePropertiesExists(b).Shield = value;
            b.VisibleChanged += CheckHaveShield;

            b.Invalidate();
        }

        private void CheckHaveShield(object sender, EventArgs e)
        {
            Button _Button = (Button)sender;

            Properties ctrlProperties;
            ctrlProperties = (Properties)m_properties[_Button as Control];

            if (ctrlProperties.Shield)
            {
                _Button.FlatStyle = FlatStyle.System;
                NativeMethods.SendMessage(_Button.Handle, BCM_SETSHIELD, (System.IntPtr)0, (System.IntPtr)1);
            }
            else
            {
                _Button.FlatStyle = FlatStyle.System;
                NativeMethods.SendMessage(_Button.Handle, BCM_SETSHIELD, (System.IntPtr)0, (System.IntPtr)0);
            }
        }


        public static bool IsAdmin()
        {
            System.Security.Principal.WindowsIdentity id = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal p = new System.Security.Principal.WindowsPrincipal(id);
            return p.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }


        public static void AddShieldToButton(ref Button b)
        {
            b.FlatStyle = FlatStyle.System;
            NativeMethods.SendMessage(b.Handle, BCM_SETSHIELD, (System.IntPtr)0, (System.IntPtr)0xffffffff);
        }

    }
}
