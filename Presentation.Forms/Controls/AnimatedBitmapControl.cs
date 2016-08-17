// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static NativeMethods;

namespace Platform.Presentation.Forms.Controls
{
    /// <summary>
    /// Provides an animated bitmap control.
    /// </summary>
    [ToolboxBitmap(typeof(System.Windows.Forms.PictureBox))]
    [ToolboxItem(true)]
    public class AnimatedBitmapControl : Control
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
        public AnimatedBitmapControl()
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
        #endregion

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

        #endregion

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

        #endregion

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

    #region Bidi

    internal class BidiGraphics
    {
        private readonly Graphics g;
        private readonly Rectangle containerBounds;
        private readonly bool isRTL;
        private bool applyRtlFormRules;

        public BidiGraphics(Graphics g, Rectangle containerBounds, bool isRtl)
        {
            this.g = g;
            this.containerBounds = containerBounds;
            isRTL = isRtl;
        }

        public BidiGraphics(Graphics g, Rectangle containerBounds, RightToLeft rtl) : this(g, containerBounds, rtl == RightToLeft.Yes && BidiHelper.IsRightToLeft)
        {
        }

        public BidiGraphics(Graphics g, Rectangle containerBounds) : this(g, containerBounds, BidiHelper.IsRightToLeft)
        {
        }

        public BidiGraphics(Graphics g, Size containerSize) : this(g, new Rectangle(Point.Empty, containerSize))
        {
        }

        public BidiGraphics(Graphics g, Size containerSize, bool isRtl)
            : this(g, new Rectangle(Point.Empty, containerSize), isRtl)
        {
        }

        public Graphics Graphics
        {
            get { return g; }
        }

        /// <summary>
        /// Sometimes, graphics contexts that are based on Forms where RightToLeftLayout is true
        /// need to be handled differently, due to the device context having a mirrored coordinate
        /// space. Note that this is not true for child controls of such forms, in general.
        /// </summary>
        public bool ApplyRtlFormRules
        {
            get { return applyRtlFormRules; }
            set { applyRtlFormRules = value; }
        }

        private Rectangle TranslateImageRectangle(Rectangle orig, bool allowMirroring)
        {
            if (!isRTL)
                return orig;

            Rectangle rect = TranslateRectangle(orig);
            if (allowMirroring)
            {
                rect.X += rect.Width;
                rect.Width *= -1;
            }
            return rect;
        }

        public Rectangle TranslateRectangle(Rectangle orig)
        {
            if (!isRTL)
                return orig;
            int x = containerBounds.Width - (orig.Location.X + orig.Width) + (2 * containerBounds.Left);
            return new Rectangle(x, orig.Y, orig.Width, orig.Height);
        }

        private RectangleF TranslateRectangleF(RectangleF orig)
        {
            if (!isRTL)
                return orig;
            float x = containerBounds.Width - (orig.Location.X + orig.Width) + (2 * containerBounds.Left);
            return new RectangleF(x, orig.Y, orig.Width, orig.Height);
        }

        private Point TranslatePoint(Point orig)
        {
            if (!isRTL)
                return orig;
            int x = containerBounds.Width - orig.X + (2 * containerBounds.Left);
            return new Point(x, orig.Y);
        }

        public void DrawImage(bool allowMirroring, Image image, int x, int y)
        {
            //TODO: measure image to get width for high DPI
            Rectangle rect = new Rectangle(x, y, image.Width, image.Height);
            rect = TranslateImageRectangle(rect, allowMirroring);
            g.DrawImage(image, rect);
        }

        public void DrawImage(bool allowMirroring, Image image, Point point)
        {
            //TODO: measure image to get width for high DPI
            Rectangle rect = new Rectangle(point.X, point.Y, image.Width, image.Height);
            rect = TranslateImageRectangle(rect, allowMirroring);
            g.DrawImage(image, rect);
        }

        public void DrawImage(bool allowMirroring, Image image, Rectangle rect)
        {
            rect = TranslateImageRectangle(rect, allowMirroring);
            g.DrawImage(image, rect);
        }

        public void DrawImage(bool allowMirroring, Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit)
        {
            //TODO: source coordinates need to deal with mirroring
            Debug.Assert(srcUnit == GraphicsUnit.Pixel, "BidiGraphics does not support non-Pixel units");
            g.DrawImage(image,
                TranslateImageRectangle(destRect, allowMirroring),
                srcX, srcY, srcWidth, srcHeight, srcUnit);
        }

        public void DrawImage(bool allowMirroring, Image image, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit)
        {
            //TODO: source coordinates need to deal with mirroring
            Debug.Assert(srcUnit == GraphicsUnit.Pixel, "BidiGraphics does not support non-Pixel units");
            g.DrawImage(image,
                TranslateImageRectangle(destRect, allowMirroring),
                srcRect, srcUnit);
        }

        public void DrawImage(bool allowMirroring, Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttributes)
        {
            //TODO: source coordinates need to deal with mirroring
            Debug.Assert(srcUnit == GraphicsUnit.Pixel, "BidiGraphics does not support non-Pixel units");
            g.DrawImage(image,
                TranslateImageRectangle(destRect, allowMirroring),
                srcX, srcY, srcWidth, srcHeight, srcUnit,
                imageAttributes);
        }

        public void DrawIcon(bool allowMirroring, Icon icon, int x, int y)
        {
            //TODO: measure image to get width for high DPI
            Rectangle rect = new Rectangle(x, y, icon.Width, icon.Height);
            DrawIcon(allowMirroring, icon, rect);
        }

        public void DrawIcon(bool allowMirroring, Icon icon, Rectangle rect)
        {
            if (isRTL && (applyRtlFormRules || allowMirroring))
            {
                // This is necessary because form mirroring causes
                // icons to always draw mirrored.
                using (Bitmap bitmap = icon.ToBitmap())
                    DrawImage(allowMirroring, bitmap, rect);
            }
            else
                g.DrawIcon(icon, TranslateRectangle(rect));
        }

        [Obsolete("Please use DrawText- DrawString will cause issues with localized versions of the product.")]
        public void DrawString(string caption, Font font, Brush brush, RectangleF f)
        {
            StringFormat format = new StringFormat();
            DrawString(caption, font, brush, f, format);
        }

        [Obsolete("Please use DrawText- DrawString will cause issues with localized versions of the product.")]
        public void DrawString(string caption, Font font, Brush brush, RectangleF f, StringFormat format)
        {
            RectangleF newRectF = TranslateRectangleF(f);
            if (isRTL)
                format.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
            g.DrawString(caption, font, brush, newRectF, format);
        }

        [Obsolete("Please use DrawText- DrawString will cause issues with localized versions of the product.")]
        public void DrawString(string caption, Font font, Brush brush, int x, int y)
        {
            Point newPoint = TranslatePoint(new Point(x, y));
            StringFormat format = StringFormat.GenericDefault;
            if (isRTL)
                format.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
            g.DrawString(caption, font, brush, newPoint, format);
        }

        [Obsolete("Please use DrawText- DrawString will cause issues with localized versions of the product.")]
        public void DrawString(string caption, Font font, Brush b, Point point)
        {
            DrawString(caption, font, b, point.X, point.Y);
        }

        public void FillRectangle(Brush b, int x1, int y1, int width, int height)
        {
            Rectangle rect = TranslateRectangle(new Rectangle(x1, y1, width, height));
            g.FillRectangle(b, rect);
        }

        public void FillRectangle(Brush b, Rectangle Bounds)
        {
            Rectangle rect = TranslateRectangle(Bounds);
            g.FillRectangle(b, rect);
        }

        public void FillRectangle(Brush b, RectangleF Bounds)
        {
            RectangleF rect = TranslateRectangleF(Bounds);
            g.FillRectangle(b, rect);
        }

        public void DrawRectangle(Pen p, Rectangle Bounds)
        {
            Rectangle rect = TranslateRectangle(Bounds);
            if (isRTL)
                rect.X -= (int)Math.Round(p.Width);
            g.DrawRectangle(p, rect);
        }

        public void DrawLine(Pen p, int x1, int y1, int x2, int y2)
        {
            Point start = TranslatePoint(new Point(x1, y1));
            Point end = TranslatePoint(new Point(x2, y2));
            g.DrawLine(p, start, end);
        }

        public void IntersectClip(Rectangle region)
        {
            Rectangle rect = TranslateRectangle(region);
            g.IntersectClip(rect);
        }

        public Brush CreateLinearGradientBrush(Rectangle bounds, Color color, Color color1, LinearGradientMode mode)
        {
            Debug.Assert(((mode == LinearGradientMode.Horizontal) || (mode == LinearGradientMode.Vertical)), "CreateLinearGradientBrush only supports horizontal or vertical gradients");
            if (isRTL && mode == LinearGradientMode.Horizontal)
            {
                Color temp = color;
                color = color1;
                color1 = temp;
            }
            return new LinearGradientBrush(bounds, color, color1, mode);
        }

        public void DrawFocusRectangle(Rectangle rect)
        {
            ControlPaint.DrawFocusRectangle(g, TranslateRectangle(rect));
        }

        public void DrawFocusRectangle(Rectangle rect, Color foreColor, Color backColor)
        {
            ControlPaint.DrawFocusRectangle(g, TranslateRectangle(rect), foreColor, backColor);
        }

        public IDisposable Container(int xOffset, int yOffset)
        {
            GraphicsContainer gc = g.BeginContainer();
            g.TranslateTransform(xOffset, yOffset);
            return new GraphicsContainerDisposer(g, gc);
        }

        public Size MeasureText(string text, Font font)
        {
            return TextRenderer.MeasureText(g, text, font, Size.Empty, FixupTextFormatFlags(0));
        }

        public Size MeasureText(string text, Font font, Size size, TextFormatFlags flags)
        {
            return TextRenderer.MeasureText(g, text, font, size, FixupTextFormatFlags(flags));
        }

        public void DrawText(string text, Font font, Rectangle bounds, Color color, TextFormatFlags textFormatFlags)
        {
            textFormatFlags = FixupTextFormatFlags(textFormatFlags);
            TextRenderer.DrawText(
                g,
                text,
                font,
                TranslateRectangle(bounds),
                color,
                textFormatFlags
                );
        }

        private TextFormatFlags FixupTextFormatFlags(TextFormatFlags textFormatFlags)
        {
            if (isRTL)
            {
                textFormatFlags |= TextFormatFlags.RightToLeft;
                if ((textFormatFlags & TextFormatFlags.HorizontalCenter) == 0)
                    textFormatFlags ^= TextFormatFlags.Right;
            }
            return textFormatFlags;
        }

        public void DrawText(string text, Font font, Rectangle bounds, Color color, Color backgroundColor, TextFormatFlags textFormatFlags)
        {
            textFormatFlags = FixupTextFormatFlags(textFormatFlags);
            TextRenderer.DrawText(
                g,
                text,
                font,
                TranslateRectangle(bounds),
                color,
                backgroundColor,
                textFormatFlags
                );
        }

        public void DrawText(string text, Font font, Rectangle bounds, Color textColor)
        {
            TextFormatFlags textFormatFlags = 0;
            if (isRTL)
            {
                textFormatFlags |= TextFormatFlags.Right | TextFormatFlags.RightToLeft;
            }
            TextRenderer.DrawText(
                g,
                text,
                font,
                TranslateRectangle(bounds),
                textColor,
                textFormatFlags);
        }

        public const int DI_MASK = 0x0001;
        public const int DI_IMAGE = 0x0002;
        public const int DI_NORMAL = 0x0003;
        public const int DI_COMPAT = 0x0004;
        public const int DI_DEFAULTSIZE = 0x0008;
        public const int DI_NOMIRROR = 0x0010;

        [DllImport("User32.dll", SetLastError = true)]
        private static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon,
            int cxWidth, int cyHeight, int istepIfAniCur, IntPtr hbrFlickerFreeDraw,
            int diFlags);

        public void FillPolygon(Brush b, Point[] points)
        {
            for (int i = 0; i < points.Length; i++)
                points[i] = TranslatePoint(points[i]);
            g.FillPolygon(b, points);
        }

        public void DrawCurve(Pen p, Point[] points)
        {
            for (int i = 0; i < points.Length; i++)
                points[i] = TranslatePoint(points[i]);
            g.DrawCurve(p, points);
        }
    }

    internal class GraphicsContainerDisposer : IDisposable
    {
        private readonly Graphics g;
        private readonly GraphicsContainer gc;
        private bool disposed = false;

        public GraphicsContainerDisposer(Graphics g, GraphicsContainer gc)
        {
            this.g = g;
            this.gc = gc;
        }

        public void Dispose()
        {
            if (!disposed)
            {
                disposed = true;
                g.EndContainer(gc);
            }
        }
    }

    internal interface IRtlAware
    {
        void Layout();
    }

    internal class BidiHelper
    {
        public static bool IsRightToLeft
        {
            get
            {
                CultureInfo currentUICulture = System.Threading.Thread.CurrentThread.CurrentUICulture;

                if (currentUICulture.IetfLanguageTag.ToUpperInvariant() == "PRS-AF")
                {
                    // WinLive 393987 - The "prs-af" Writer build isn't mirrored to RTL
                    // .NET 2.0 and 3.5 incorrectly define IsRightToLeft=false in the "prs-af" culture (though this is
                    // fixed in .NET 4.0). We need to override the call to .NET to return the correct value of true.
                    return true;
                }

                return currentUICulture.TextInfo.IsRightToLeft;
            }
        }

        public static MessageBoxOptions RTLMBOptions
        {
            get
            {
                if (IsRightToLeft)
                    return (MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                return (MessageBoxOptions)0;
            }
        }

        public static void RtlLayoutFixup(Control control)
        {
            RtlLayoutFixup(control, true);
        }

        public static void RtlLayoutFixup(Control control, bool recursive)
        {
            Control[] childControls = ToArray(control.Controls);
            RtlLayoutFixup(control, recursive, childControls);
        }

        private static Control[] ToArray(IList controls)
        {
            Control[] childControls = new Control[controls.Count];
            for (int i = 0; i < childControls.Length; i++)
                childControls[i] = (Control)controls[i];
            return childControls;
        }

        public static void RtlLayoutFixup(Control control, bool recursive, params Control[] childControls)
        {
            RtlLayoutFixup(control, recursive, false, childControls);
        }

        public static void RtlLayoutFixup(Control control, bool recursive, bool forceAutoLayout, IList childControls)
        {
            RtlLayoutFixup(control, recursive, forceAutoLayout, ToArray(childControls));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="control">The control to fixup.</param>
        /// <param name="recursive">Whether or not to fixup child controls as well.</param>
        /// <param name="forceAutoLayout">If true, ignores IRtlAware interface on the control param and lays out as normal. (IRtlAware will be used for children of control, regardless of this setting.)</param>
        /// <param name="childControls">The child controls to reposition (doesn't have to include all the children of control).</param>
        public static void RtlLayoutFixup(Control control, bool recursive, bool forceAutoLayout, params Control[] childControls)
        {
            if (IsRightToLeft && control.RightToLeft != RightToLeft.No)
            {
                if (!forceAutoLayout && control is IRtlAware)
                {
                    ((IRtlAware)control).Layout();
                }
                else
                {
                    bool isMirroredForm = control is Form
                                          && ((Form)control).RightToLeftLayout;

                    foreach (Control childControl in childControls)
                    {
                        if (!isMirroredForm)
                        {
                            childControl.Left = control.Width - childControl.Right;

                            switch (childControl.Dock)
                            {
                                case DockStyle.Left:
                                    childControl.Dock = DockStyle.Right;
                                    break;
                                case DockStyle.Right:
                                    childControl.Dock = DockStyle.Left;
                                    break;
                            }

                            if (childControl.Dock == DockStyle.None)
                            {
                                switch (childControl.Anchor & (AnchorStyles.Left | AnchorStyles.Right))
                                {
                                    case AnchorStyles.Left:
                                        childControl.Anchor &= ~AnchorStyles.Left;
                                        childControl.Anchor |= AnchorStyles.Right;
                                        break;
                                    case AnchorStyles.Right:
                                        childControl.Anchor &= ~AnchorStyles.Right;
                                        childControl.Anchor |= AnchorStyles.Left;
                                        break;
                                    case AnchorStyles.Left | AnchorStyles.Right:
                                        // do nothing
                                        break;
                                }
                            }
                        }

                        if (recursive)
                            RtlLayoutFixup(childControl);
                    }

                    if (!isMirroredForm)
                    {
                        int leftMargin = control.Margin.Left;
                        int rightMargin = control.Margin.Right;
                        if (leftMargin != rightMargin)
                        {
                            control.Margin = new Padding(
                                rightMargin,
                                control.Margin.Top,
                                leftMargin,
                                control.Margin.Bottom);
                        }

                        // NOTE: This handles ScrollableControl.DockPadding as well!
                        int leftPadding = control.Padding.Left;
                        int rightPadding = control.Padding.Right;
                        if (leftPadding != rightPadding)
                        {
                            control.Padding = new Padding(
                                rightPadding,
                                control.Padding.Top,
                                leftPadding,
                                control.Padding.Bottom);
                        }
                    }
                }
            }
        }

        public static Bitmap Mirror(Bitmap bitmap)
        {
            if (!IsRightToLeft)
                return bitmap;
            Bitmap mirrored = new Bitmap(bitmap);
            mirrored.RotateFlip(RotateFlipType.RotateNoneFlipX);
            return mirrored;
        }


    }

    #endregion

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
                GraphicsMode oldGraphicsMode = NativeMethods.SetGraphicsMode(hdc, GraphicsMode.Advanced);

                XFORM xformOrig;
                NativeMethods.GetWorldTransform(hdc, out xformOrig);
                try
                {
                    XFORM xform = xformOrig;
                    xform.eDx -= relativeChildLocation.X;
                    xform.eDy -= relativeChildLocation.Y;
                    NativeMethods.SetWorldTransform(hdc, ref xform);

                    using (Graphics g = Graphics.FromHdc(hdc))
                    {
                        host.Paint(new PaintEventArgs(g, relativeClipRectangle));
                    }
                }
                finally
                {
                    NativeMethods.SetWorldTransform(hdc, ref xformOrig);
                    NativeMethods.SetGraphicsMode(hdc, oldGraphicsMode);
                }
            }
            finally
            {
                args.Graphics.ReleaseHdc(hdc);
            }
        }
    }

    #endregion

    #region Gdi32


    

    #endregion

}
