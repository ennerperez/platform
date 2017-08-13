using Platform.Support.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Platform.Presentation.Forms.Controls
{
    [ToolboxBitmap(typeof(System.Windows.Forms.Panel))]
    [ToolboxItem(true)]
    public class MessagePanel : Panel
    {
        public MessagePanel() : base()
        {
            Resize += MessagePanel_Resize;

            colors = new Dictionary<MessageStyle, Color[]>();
            colors.Add(MessageStyle.None, new[] { Color.FromArgb(0, 56, 109), Color.FromArgb(42, 83, 132) });
            colors.Add(MessageStyle.Error, new[] { Color.FromArgb(175, 1, 0), Color.FromArgb(221, 1, 0) });
            colors.Add(MessageStyle.Stop, colors[MessageStyle.Error]);
            colors.Add(MessageStyle.Warning, new[] { Color.FromArgb(242, 177, 0), Color.FromArgb(254, 204, 70) });
            colors.Add(MessageStyle.Exclamation, colors[MessageStyle.Warning]);
            colors.Add(MessageStyle.Success, new[] { Color.FromArgb(22, 128, 20), Color.FromArgb(65, 178, 61) });
        }

        #region Properties

        private bool showIcon = true;

        [DefaultValue(true), Category("Appearance")]
        public bool ShowIcon
        {
            get
            {
                return showIcon;
            }
            set
            {
                showIcon = value;
                Invalidate();
            }
        }

        private MessageStyle style = MessageStyle.None;

        [DefaultValue(MessageStyle.None), Category("Appearance")]
        public MessageStyle Style
        {
            get
            {
                return style;
            }
            set
            {
                style = value;
                Invalidate();
            }
        }

        private IconSize iconSize = IconSize.Large;

        [DefaultValue(IconSize.Large), Category("Appearance")]
        public IconSize IconSize
        {
            get
            {
                return iconSize;
            }
            set
            {
                iconSize = value;
                Invalidate();
            }
        }

        private int gradientWidth = 20;

        [DefaultValue(20), Category("Appearance")]
        public int GradientWidth
        {
            get
            {
                return gradientWidth;
            }
            set
            {
                gradientWidth = value;
                Invalidate();
            }
        }

        #endregion Properties

        private Image imageIcon = null;
        private Color[] gradientColors = null;

        private Dictionary<MessageStyle, Color[]> colors;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var gradientRectangle = new Rectangle(new Point(0, 0), new Size(GradientWidth, Height));
            var paddingLeft = GradientWidth + DefaultMargin.Left;

            if (colors.ContainsKey(Style))
                gradientColors = colors[Style];
            else
                gradientColors = colors[MessageStyle.None];

            if (!e.ClipRectangle.IsEmpty)
                using (LinearGradientBrush lgb = new LinearGradientBrush(gradientRectangle, gradientColors[0], gradientColors[1], 90))
                {
                    lgb.WrapMode = WrapMode.TileFlipXY;
                    e.Graphics.FillRectangle(lgb, gradientRectangle);
                }

            if (ShowIcon && Style != MessageStyle.None)
                imageIcon = IconExtractor.Extract("imageres.dll", (int)Style, IconSize == IconSize.Large).ToBitmap();

            if (ShowIcon && imageIcon != null)
            {
                if (Height < imageIcon.Height + (Padding.Top + Padding.Bottom))
                    Size = new Size(Width, imageIcon.Height + (Padding.Top + Padding.Bottom));

                // Draw icon
                var point = new Point(GradientWidth + DefaultMargin.Left, Padding.Top);
                e.Graphics.DrawImage(imageIcon, point);

                // Padding left
                paddingLeft = GradientWidth + DefaultMargin.Left + imageIcon.Width + DefaultMargin.Left;
                //if (Padding.Left < paddingLeft)
                Padding = new Padding(
                    paddingLeft,
                    Padding.Top,
                    Padding.Right,
                    Padding.Bottom);
            }

            //AjustControls
            foreach (Control item in Controls.OfType<Control>().Where(c => c.Location.X < paddingLeft))
                item.Location = new Point(paddingLeft, item.Location.Y);
        }

        private void MessagePanel_Resize(object sender, System.EventArgs e)
        {
            if (ShowIcon && imageIcon != null)
                if (Height < imageIcon.Height + (Padding.Top + Padding.Bottom))
                    Size = new Size(Width, imageIcon.Height + (Padding.Top + Padding.Bottom));
        }
    }

    public enum IconSize : short
    {
        Small = 16,
        Large = 32
    }

    public enum MessageStyle
    {
        None = MessageBoxIcon.None,
        Question = 99,
        Error = 100,
        Success = 101,
        Warning = 102,
        UAC = 73,
        Windows = 1,
        Help = 94,
        Stop = 93,
        Information = 76,
        Exclamation = 79,
    }
}