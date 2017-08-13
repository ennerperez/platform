using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Platform.Presentation.Forms.Controls
{
    [ToolboxBitmap(typeof(System.Windows.Forms.Label))]
    [ToolboxItem(true)]
    public class AutoHeightLabel : Label
    {
        // Methods
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