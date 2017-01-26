using Platform.Support.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Platform.Presentation.Forms
{

    namespace Controls
    {

        /// <summary>
        /// Vista Command Link Control with C# / Windows Forms
        /// https://blogs.msdn.microsoft.com/knom/2007/03/12/vista-command-link-control-with-c-windows-forms/
        /// </summary>
        [ToolboxBitmap(typeof(System.Windows.Forms.Button))]
        [ToolboxItem(true)]
        public class CommandLink : Button
        {

            public CommandLink()
            {
                this.FlatStyle = FlatStyle.System;
            }

            protected override System.Drawing.Size DefaultSize
            {
                get
                {
                    return new Size(180, 60);
                }
            }

            protected override CreateParams CreateParams
            {
                get
                {
                    CreateParams cParams = base.CreateParams;
                    cParams.Style |= User32.BS_COMMANDLINK;
                    return cParams;
                }
            }

            private bool useElevationIcon = false;

            [Category("Command Link")]
            [Description("Gets or sets the shield icon visibility of the command link.")]
            [DefaultValue(false)]
            public bool UseElevationIcon
            {
                get { return useElevationIcon; }
                set
                {
                    useElevationIcon = value;
                    User32.SendMessage(new HandleRef(this, this.Handle), User32.BCM_SETSHIELD, IntPtr.Zero,
                        useElevationIcon);
                }
            }

            [Category("Command Link")]
            [Description("Gets or sets the note text of the command link.")]
            [DefaultValue("")]
            public string Note
            {
                get
                {
                    return GetNoteText();
                }
                set
                {
                    SetNoteText(value);
                }
            }

            [Obsolete()]
            public Size ImageScalingSize { get; set; }

            private void SetNoteText(string value)
            {
                User32.SendMessage(new HandleRef(this, this.Handle),
                    User32.BCM_SETNOTE,
                    IntPtr.Zero, value);
            }

            private string GetNoteText()
            {
                int length = User32.SendMessage(new HandleRef(this, this.Handle),
                    User32.BCM_GETNOTELENGTH,
                    IntPtr.Zero, IntPtr.Zero) + 1;

                StringBuilder sb = new StringBuilder(length);

                User32.SendMessage(new HandleRef(this, this.Handle),
                    User32.BCM_GETNOTE,
                    ref length, sb);

                return sb.ToString();
            }

        }

    }
    
}
