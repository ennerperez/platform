using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Platform.Presentation.Forms.Components
{
    [System.Drawing.ToolboxBitmap(typeof(System.Windows.Forms.Form))]
    public partial class Extender : Component, IExtenderProvider
    {

        #region Designer
        public Extender()
        {
            InitializeComponent();
        }

        public Extender(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        #endregion

        #endregion

        #region Parent form

        public ContainerControl ContainerControl
        {
            get { return _containerControl; }
            set { _containerControl = value; }
        }

        private ContainerControl _containerControl = null;
        public override ISite Site
        {
            get { return base.Site; }
            set
            {
                base.Site = value;
                if (value != null)
                {
                    IDesignerHost host = value.GetService(typeof(IDesignerHost)) as IDesignerHost;
                    if (host != null)
                    {
                        IComponent componentHost = host.RootComponent;
                        if (componentHost is ContainerControl)
                        {
                            ContainerControl = componentHost as ContainerControl;
                        }
                    }
                }
            }
        }

        #endregion

        #region  Border

        private System.Drawing.Color _BorderColor = System.Drawing.Color.Gray;
        [DefaultValue("System.Drawing.Color.Gray")]
        [Category("Border")]
        public System.Drawing.Color BorderColor
        {
            get { return _BorderColor; }
            set
            {
                if (_BorderColor != value)
                {
                    _BorderColor = value;
                    if (this.ContainerControl != null && this.ContainerControl.FindForm() != null)
                        this.ContainerControl.FindForm().Invalidate();
                }
            }
        }

        private int _BorderWidth = 1;
        [DefaultValue(1)]
        [Category("Border")]
        public int BorderWidth
        {
            get { return _BorderWidth; }
            set
            {
                if (_BorderWidth != value)
                {
                    _BorderWidth = value;
                    if (this.ContainerControl != null && this.ContainerControl.FindForm() != null)
                        this.ContainerControl.FindForm().Invalidate();
                }
            }
        }
        private void Form_Paint(object sender, PaintEventArgs e)
        {
            if (!this.DesignMode)
            {
                if (this.ContainerControl != null && this.ContainerControl.FindForm() != null)
                {
                    System.Windows.Forms.Form _form = this.ContainerControl.FindForm();
                    System.Drawing.Rectangle _rect = new System.Drawing.Rectangle(0, 0, _form.Width - _BorderWidth, _form.Height - _BorderWidth);
                    e.Graphics.DrawRectangle(new System.Drawing.Pen(this.BorderColor, _BorderWidth), _rect);
                }
            }
        }

        #endregion

        #region  Mouse Move

        //private const long WM_SYSCOMMAND = 0x112L;
        //private const long MOUSE_MOVE = 0xf012L;

        const int WM_SYSCOMMAND = 0x112;
        const int MOUSE_MOVE = 0xF012;
                
        private void MoveForm()
        {
            NativeMethods.ReleaseCapture();
            NativeMethods.SendMessage(this.ContainerControl.FindForm().Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0);
        }


        private System.Drawing.Size _Size;

        private bool _Moving = false;
        private void Form_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _Moving = e.Button == MouseButtons.Left;
        }
        private void Form_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _Moving = false;
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            {
                //_Moving Then 
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {

                    if (_Size != null)
                    {
                        this.ContainerControl.FindForm().Size = _Size;
                        //_Size = null;
                    }

                    MoveForm();

                    Rectangle _WorkingArea =  Screen.FromControl((Control) sender).WorkingArea;
                    //Screen.GetWorkingArea(Cursor.Position)

                    _WorkingArea = new Rectangle();

                    foreach (Screen item in Screen.AllScreens)
                    {
                        if (item.WorkingArea.Location.X > _WorkingArea.Location.X)
                        {
                            _WorkingArea.Width += item.WorkingArea.Width;
                        }

                    }

                    //Dim _WorkingAreaFull As New Rectangle(0, 0, Screen.AllScreens.Sum(Function(w) w.WorkingArea.Width), Screen.AllScreens.Sum(Function(w) w.WorkingArea.Height))
                    int _s = 1;
                    //IIf(Screen.AllScreens.Count > 1, 2, 1)

                    if (this.ContainerControl.FindForm().MaximizeBox)
                    {
                        if (this.ContainerControl.FindForm().Left <= 0)
                        {
                            if (this.ContainerControl.FindForm().Size != new System.Drawing.Size((_WorkingArea.Width / (2 * _s)), _WorkingArea.Height) & this.ContainerControl.FindForm().Location != new System.Drawing.Point(0, 0))
                            {
                                _Size = this.ContainerControl.FindForm().Size;
                                this.ContainerControl.FindForm().Size = new System.Drawing.Size((_WorkingArea.Width / (2 * _s)), _WorkingArea.Height);
                                this.ContainerControl.FindForm().Location = new System.Drawing.Point(0, 0);
                            }
                        }
                        else if ((this.ContainerControl.FindForm().Left + Cursor.Position.X) >= (_WorkingArea.Width - 16))
                        {
                            if (this.ContainerControl.FindForm().Size != new System.Drawing.Size(_WorkingArea.Width / (2 * _s), _WorkingArea.Height) & this.ContainerControl.FindForm().Location != new System.Drawing.Point((_WorkingArea.Width - this.ContainerControl.FindForm().Width), 0))
                            {
                                _Size = this.ContainerControl.FindForm().Size;
                                this.ContainerControl.FindForm().Size = new System.Drawing.Size((_WorkingArea.Width / (2 * _s)), _WorkingArea.Height);
                                this.ContainerControl.FindForm().Location = new System.Drawing.Point((_WorkingArea.Width - this.ContainerControl.FindForm().Width), 0);
                            }
                        }
                        else if (this.ContainerControl.FindForm().Top <= 0)
                        {
                            this.ContainerControl.FindForm().WindowState = FormWindowState.Maximized;
                        }
                    }

                }
                else
                {
                    this.ContainerControl.FindForm().Cursor = Cursors.Default;
                }
            }
        }

        #endregion

        #region IExtenderProvider

        public class Properties
        {

            public bool Border { get; set; }

            public bool Snap { get; set; }

        }

        private Hashtable m_properties = new Hashtable();
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

        [DefaultValue(true)]
        public bool GetBorder(Form b)
        {
            return EnsurePropertiesExists(b).Border;
        }

        [DefaultValue(false)]
        public bool GetSnap(Form b)
        {
            return EnsurePropertiesExists(b).Snap;
        }


        public void SetBorder(Form b, bool value)
        {
            EnsurePropertiesExists(b).Border = value;

            if (value)
            {
                b.Paint += Form_Paint;
            }
            else
            {
                b.Paint -= Form_Paint;
            }

            b.Invalidate();
        }


        public void SetSnap(Form b, bool value)
        {
            EnsurePropertiesExists(b).Snap = value;

            if (value)
            {
                //AddHandler b.MouseDown, AddressOf Form_MouseDown
                //AddHandler b.MouseUp, AddressOf Form_MouseUp
                b.MouseMove += Form_MouseMove;
            }
            else
            {
                //RemoveHandler b.MouseDown, AddressOf Form_MouseDown
                //RemoveHandler b.MouseUp, AddressOf Form_MouseUp
                b.MouseMove -= Form_MouseMove;
            }

            b.Invalidate();
        }

        public bool CanExtend(object extendee)
        {
            return extendee is System.Windows.Forms.Form;
        }


        #endregion

    }
}


internal static partial class NativeMethods
{
    [DllImport(ExternDll.User32, CharSet = CharSet.Auto, EntryPoint = "ReleaseCapture")]
    internal static extern void ReleaseCapture();

    //[DllImport(ExternDll.User32, CharSet = CharSet.Auto, EntryPoint = "SendMessage")]
    //internal static extern void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
}
