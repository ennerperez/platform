using Platform.Support.Windows;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Platform.Presentation.Forms.Providers
{
    [ProvideProperty("Shield", typeof(Button))]
    [ToolboxBitmap(typeof(System.IO.FileSystemWatcher))]
    [ToolboxItem(true)]
    public sealed class UserAcountControlProvider : System.ComponentModel.Component, IExtenderProvider
    {
        

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
                User32.SendMessage(_Button.Handle, User32.BCM_SETSHIELD, (System.IntPtr)0, (System.IntPtr)1);
            }
            else
            {
                _Button.FlatStyle = FlatStyle.System;
                User32.SendMessage(_Button.Handle, User32.BCM_SETSHIELD, (System.IntPtr)0, (System.IntPtr)0);
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
            User32.SendMessage(b.Handle, User32.BCM_SETSHIELD, (System.IntPtr)0, (System.IntPtr)0xffffffff);
        }

    }
}
