using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Platform.Support.Drawing;

namespace Platform.Presentation.Forms.Controls
{

    [ToolboxBitmap(typeof(MessagePanel), "MessagePanel.bmp")]
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

        #endregion

        private Dictionary<MessageStyle, Color[]> colors;

        protected override void OnPaint(PaintEventArgs e)
        {

            Image imageIcon = null;
            int gradientRectangleWidth = 20;
            Color[] gradientColors = null;

            var gradientRectangle = new Rectangle(new Point(0, 0), new Size(gradientRectangleWidth, Height));

            int _m = (IconSize == IconSize.Small ? 30 : 50);

            if (Height < _m)
                Size = new Size(Width, _m);

            if (ShowIcon && Style != MessageStyle.None)
                imageIcon = IconExtractor.Extract("imageres.dll", (int)Style, IconSize == IconSize.Large).ToBitmap();

            if (colors.ContainsKey(Style))
                gradientColors = colors[Style];
            else
                gradientColors = colors[MessageStyle.None];


            if (!e.ClipRectangle.IsEmpty)
            {
                using (LinearGradientBrush lgb = new LinearGradientBrush(gradientRectangle, gradientColors[0], gradientColors[1], 90))
                {
                    lgb.WrapMode = WrapMode.TileFlipXY;
                    e.Graphics.FillRectangle(lgb, gradientRectangle);
                }
            }


            if (imageIcon != null)
            {
                e.Graphics.DrawImage(imageIcon, new Point(gradientRectangleWidth + 3, 3));

                if (Padding.Left < gradientRectangleWidth + 3 + imageIcon.Width)
                    Padding = new Padding(gradientRectangleWidth + 3 + imageIcon.Width, Padding.Top, Padding.Right, Padding.Bottom);

                //AjustControls
                foreach (Control item in Controls.OfType<Control>().Where(c => c.Location.X < gradientRectangleWidth + 3 + imageIcon.Width))
                    item.Location = new Point(gradientRectangleWidth + 3 + imageIcon.Width + 3, item.Location.Y);
            }
            else
            {
                //AjustControls
                foreach (Control item in Controls.OfType<Control>().Where(c => c.Location.X < Padding.Left))
                    item.Location = new Point(gradientRectangleWidth + 3, item.Location.Y);
            }

            base.OnPaint(e);

        }

        private void MessagePanel_Resize(object sender, System.EventArgs e)
        {
            int _m = (IconSize == IconSize.Small ? 30 : 50);
            if (Height < _m)
                Size = new Size(Width, _m);
        }

    }

    public enum IconSize
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
