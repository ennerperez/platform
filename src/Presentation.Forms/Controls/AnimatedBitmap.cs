// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

using Platform.Support.Windows;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Platform.Presentation.Forms.Controls
{
    /// <summary>
    /// Provides an animated bitmap control.
    /// </summary>
    [ToolboxBitmap(typeof(System.Windows.Forms.PictureBox))]
    [ToolboxItem(true)]
    public class AnimatedBitmap : Control
    {
        #region Private Member Variables

        /// <summary>
        /// The bitmap list.
        /// </summary>
        private Bitmap[] bitmaps;

        /// <summary>
        /// The current bitmap index.
        /// </summary>
        private int currentBitmapIndex;

        /// <summary>
        /// The timer that runs the animation.
        /// </summary>
        private Timer timerAnimation;

        private bool useVirtualTransparency;

        /// <summary>
        /// Components.
        /// </summary>
        private IContainer components;

        #endregion Private Member Variables

        #region Class Initialization & Termination

        /// <summary>
        /// Initializes a new instance of the AnimatedBitmapControl class.
        /// </summary>
        public AnimatedBitmap()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            //	Turn on double buffered painting.
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            //	Redraw the control on resize.
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //	Stop the animation timer.
                timerAnimation.Stop();
                timerAnimation.Tick -= new EventHandler(this.timerAnimation_Tick);
                timerAnimation.Dispose();
                timerAnimation = null;

                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #endregion Class Initialization & Termination

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerAnimation = new System.Windows.Forms.Timer(this.components);
            //
            // timerAnimation
            //
            this.timerAnimation.Tick += new System.EventHandler(this.timerAnimation_Tick);
            //
            // AnimatedBitmapControl
            //
            this.Name = "AnimatedBitmapControl";
            this.Size = new System.Drawing.Size(64, 48);
        }

        #endregion Component Designer generated code

        #region Public Properties

        /// <summary>
        /// Gets or sets the animation
        /// </summary>
        public int Interval
        {
            get
            {
                return timerAnimation.Interval;
            }
            set
            {
                if (timerAnimation.Interval != value)
                {
                    bool running = timerAnimation.Enabled;
                    if (running)
                        timerAnimation.Stop();
                    timerAnimation.Interval = value;
                    if (running)
                        timerAnimation.Start();
                }
            }
        }

        /// <summary>
        /// Gets or sets the bitmaps.
        /// </summary>
        public Bitmap[] Bitmaps
        {
            get
            {
                return bitmaps;
            }
            set
            {
                bitmaps = value;
            }
        }

        /// <summary>
        ///	Gets or sets the animation running state.
        /// </summary>
        public bool Running
        {
            get
            {
                return timerAnimation.Enabled;
            }
            set
            {
                if (timerAnimation.Enabled != value)
                    timerAnimation.Enabled = value;
            }
        }

        public bool UseVirtualTransparency
        {
            get { return useVirtualTransparency; }
            set { useVirtualTransparency = value; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Starts the animation.
        /// </summary>
        public void Start()
        {
            Enabled = true;
            timerAnimation.Start();
        }

        /// <summary>
        /// Stops the animation.
        /// </summary>
        public void Stop()
        {
            Enabled = false;
            timerAnimation.Stop();
        }

        #endregion Public Methods

        #region Protected Event Overrides

        /// <summary>
        /// Raises the Paint event.
        /// </summary>
        /// <param name="e">A PaintEventArgs that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //	Call the base class's method so that registered delegates receive the event.
            base.OnPaint(e);

            Graphics g = e.Graphics;

            //	Obtain the bitmap to draw and, if there is one, draw it.
            Bitmap bitmap = BitmapToDraw;
            if (bitmap != null)
            {
                //	Fill the offscreen graphics context with the background color.
                if (UseVirtualTransparency)
                {
                    VirtualTransparency.VirtualPaint(this, new PaintEventArgs(g, ClientRectangle));
                }
                else
                {
                    using (SolidBrush solidBrush = new SolidBrush(BackColor))
                        g.FillRectangle(solidBrush, ClientRectangle);
                }

                BidiGraphics bg = new BidiGraphics(g, ClientRectangle, RightToLeft);
                Rectangle bounds = new Rectangle(Point.Empty, bitmap.Size);
                bg.DrawImage(true, bitmap, bounds);
            }
        }

        #endregion Protected Event Overrides

        #region Private Event Handlers

        /// <summary>
        /// timerAnimation_Tick event handler.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        private void timerAnimation_Tick(object sender, EventArgs e)
        {
            currentBitmapIndex = (bitmaps != null && bitmaps.Length > 0)
                                     ? (currentBitmapIndex + 1) % bitmaps.Length
                                     : 0;
            Invalidate();
            Update();
        }

        #endregion Private Event Handlers

        #region Private Methods

        /// <summary>
        /// Gets the bitmap to draw.
        /// </summary>
        /// <returns>The Bitmap to draw, or null.</returns>
        private Bitmap BitmapToDraw
        {
            get
            {
                //	If there are no bitmaps, return null.
                if (bitmaps == null || bitmaps.Length == 0)
                    return null;

                //	Return the bitmap to draw, and advance the frame as needed.
                return bitmaps[currentBitmapIndex];
            }
        }

        #endregion Private Methods
    }

    #region VirtualTransparency

    internal interface IVirtualTransparencyHost
    {
        void Paint(PaintEventArgs args);
    }

    internal class VirtualTransparency
    {
        public static IVirtualTransparencyHost VirtualParent(Control child)
        {
            for (Control parent = child.Parent; parent != null; parent = parent.Parent)
            {
                if (parent is IVirtualTransparencyHost)
                {
                    return (IVirtualTransparencyHost)parent;
                }
            }
            return null;
        }

        public static void VirtualPaint(Control child, PaintEventArgs args)
        {
            IVirtualTransparencyHost parent = VirtualParent(child);
            Debug.Assert(parent != null, "No virtual transparency host found in ancestor chain of " + child.Name);
            VirtualPaint(parent, child, args);
        }

        public static void VirtualPaint(IVirtualTransparencyHost host, Control child, PaintEventArgs args)
        {
            Control hostControl = (Control)host;
            bool rtl = host is Form && ((Form)host).RightToLeft == RightToLeft.Yes && ((Form)host).RightToLeftLayout;

            Point childLocation = child.PointToScreen(new Point(0, 0));
            Point hostLocation = hostControl.PointToScreen(new Point(rtl ? hostControl.ClientSize.Width : 0, 0));
            Point relativeChildLocation = new Point(childLocation.X - hostLocation.X, childLocation.Y - hostLocation.Y);

            if (relativeChildLocation == Point.Empty)
            {
                // no translation transform required
                host.Paint(args);
                return;
            }

            Rectangle relativeClipRectangle = args.ClipRectangle;
            relativeClipRectangle.Offset(relativeChildLocation);
            args.Graphics.SetClip(relativeClipRectangle, CombineMode.Replace);

            // Global transformations applied in GDI+ land don't apply to
            // GDI calls. So we need to drop down to GDI, apply a transformation
            // there, then wrap with GDI+.

            IntPtr hdc = args.Graphics.GetHdc();
            try
            {
                Gdi32.GraphicsMode oldGraphicsMode = Gdi32.SetGraphicsMode(hdc, Gdi32.GraphicsMode.Advanced);

                Gdi32.XFORM xformOrig;
                Gdi32.GetWorldTransform(hdc, out xformOrig);
                try
                {
                    Gdi32.XFORM xform = xformOrig;
                    xform.eDx -= relativeChildLocation.X;
                    xform.eDy -= relativeChildLocation.Y;
                    Gdi32.SetWorldTransform(hdc, ref xform);

                    using (Graphics g = Graphics.FromHdc(hdc))
                    {
                        host.Paint(new PaintEventArgs(g, relativeClipRectangle));
                    }
                }
                finally
                {
                    Gdi32.SetWorldTransform(hdc, ref xformOrig);
                    Gdi32.SetGraphicsMode(hdc, oldGraphicsMode);
                }
            }
            finally
            {
                args.Graphics.ReleaseHdc(hdc);
            }
        }
    }

    #endregion VirtualTransparency
}