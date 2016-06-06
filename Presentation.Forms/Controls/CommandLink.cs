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
                    cParams.Style |= NativeMethods.BS_COMMANDLINK;
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
                    NativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.BCM_SETSHIELD, IntPtr.Zero,
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
                NativeMethods.SendMessage(new HandleRef(this, this.Handle),
                    NativeMethods.BCM_SETNOTE,
                    IntPtr.Zero, value);
            }

            private string GetNoteText()
            {
                int length = NativeMethods.SendMessage(new HandleRef(this, this.Handle),
                    NativeMethods.BCM_GETNOTELENGTH,
                    IntPtr.Zero, IntPtr.Zero) + 1;

                StringBuilder sb = new StringBuilder(length);

                NativeMethods.SendMessage(new HandleRef(this, this.Handle),
                    NativeMethods.BCM_GETNOTE,
                    ref length, sb);

                return sb.ToString();
            }

        }

    }

    namespace VistaControls
    {


        /// <summary>
        /// Written by Marco Minerva, mailto:marco.minerva@gmail.com
        /// 
        /// This code is released under the Microsoft Community License (Ms-CL).
        /// A copy of this license is available at
        /// http://www.microsoft.com/resources/sharedsource/licensingbasics/limitedcommunitylicense.mspx
        /// </summary>
        [ToolboxBitmap(typeof(System.Windows.Forms.Button))]
        public class CommandLink : System.Windows.Forms.Button
        {
            public CommandLink()
            {
                this.FlatStyle = FlatStyle.System;
            }

            protected override CreateParams CreateParams
            {
                get
                {
                    CreateParams cp = base.CreateParams;

                    //Fix for XP provided by jonpreece (http://windowsformsaero.codeplex.com/Thread/View.aspx?ThreadId=81391)
                    if ((Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6))
                        cp.Style |= NativeMethods.BS_COMMANDLINK;
                    else
                        cp.Style |= 1;
                    return cp;
                }
            }

            private bool useicon = true; //Checks if user wants to use an icon instead of a bitmap
            private Bitmap image_;

            //Image alignment is ignored at the moment. Property overrides inherited image property
            //Supports images other than bitmap, supports transparency on .NET 2.0
            [Description("Gets or sets the image that is displayed on a button control."), Category("Appearance"), DefaultValue(null)]
            public new Bitmap Image
            {
                get
                {
                    return image_;
                }
                set
                {
                    image_ = value;
                    if (value != null)
                    {
                        this.useicon = false;
                        this.Icon = null;
                    }
                    this.SetShield(false);
                    SetImage();
                }
            }

            private Icon icon_;
            [Description("Gets or sets the icon that is displayed on a button control."), Category("Appearance"), DefaultValue(null)]
            public Icon Icon
            {
                get
                {
                    return icon_;
                }
                set
                {
                    icon_ = value;
                    if (icon_ != null)
                    {
                        this.useicon = true;
                    }
                    this.SetShield(false);
                    SetImage();
                }
            }

            [Description("Refreshes the image displayed on the button.")]
            public void SetImage()
            {
                IntPtr iconhandle = IntPtr.Zero;
                if (!this.useicon)
                {
                    if (this.image_ != null)
                    {
                        iconhandle = image_.GetHicon(); //Gets the handle of the bitmap
                    }
                }
                else
                {
                    if (this.icon_ != null)
                    {
                        iconhandle = this.Icon.Handle;
                    }
                }

                //Set the button to use the icon. If no icon or bitmap is used, no image is set.
                NativeMethods.SendMessage(this.Handle, NativeMethods.BM_SETIMAGE, 1, (int)iconhandle);
            }

            private bool showshield_ = false;
            [Description("Gets or sets whether if the control should use an elevated shield icon."), Category("Appearance"), DefaultValue(false)]
            public bool ShowShield
            {
                get
                {
                    return showshield_;
                }
                set
                {
                    showshield_ = value;
                    this.SetShield(value);
                    if (!value)
                    {
                        this.SetImage();
                    }
                }
            }

            public void SetShield(bool Value)
            {
                NativeMethods.SendMessage(this.Handle, NativeMethods.BCM_SETSHIELD, IntPtr.Zero, new IntPtr(showshield_ ? 1 : 0));
            }

            private string note_ = string.Empty;

            [Description("Gets or sets the note that is displayed on a button control."), Category("Appearance"), DefaultValue("")]
            public string Note
            {
                get
                {
                    return this.note_;
                }
                set
                {
                    this.note_ = value;
                    this.SetNote(this.note_);
                }
            }

            [Description("Sets the note displayed on the button.")]
            private void SetNote(string NoteText)
            {
                //Sets the note
                NativeMethods.SendMessage(this.Handle, NativeMethods.BCM_SETNOTE, IntPtr.Zero, NoteText);
            }
            
            [Obsolete()]
            public Size ImageScalingSize { get; set; }

        }

    }

}
