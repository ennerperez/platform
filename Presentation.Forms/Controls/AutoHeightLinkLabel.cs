using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Platform.Presentation.Forms.Controls
{

    [ToolboxBitmap(typeof(System.Windows.Forms.LinkLabel))]
    [ToolboxItem(true)]
    public class AutoHeightLinkLabel : System.Windows.Forms.LinkLabel
    {
        // Methods
        public AutoHeightLinkLabel()
        {
            base.LinkBehavior = LinkBehavior.HoverUnderline;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.ResetHeight();
        }

        private void ResetHeight()
        {
            Size preferredSize = this.GetPreferredSize(base.Size);
            base.Size = new Size(base.Width, preferredSize.Height);
        }


        // Properties
        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                this.ResetHeight();
            }
        }

    }

}
