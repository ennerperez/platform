using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Platform.Presentation.Forms.Controls
{

    [ToolboxBitmap(typeof(System.Windows.Forms.ImageList))]  
    [ToolboxItem(true)]
    public partial class BlinkingImage : PictureBox
    {
         

        public const int BLINK_INTERVAL_DEFAULT_VALUE = 300;

        [Description("Active or desactivate the \"blinking\" effect.")]
        [Category("Blinking effect")]
        [Browsable(true)]
        [DefaultValue(false)]
        public bool BlinkEnabled
        {
            get
            {
                return timer1.Enabled;
            }
            set
            {
                if (!DesignMode)
                {
                    if (value != timer1.Enabled && !value) 
                    {
                        this.Visible = true;
                    }
                    timer1.Enabled = value;
                }
            }
        }

        [Description("Duration (in ms) between visibility changes.")]
        [Category("Blinking effect")]
        [Browsable(true)]
        [DefaultValue(BLINK_INTERVAL_DEFAULT_VALUE)]
        public int BlinkInterval
        {
            get { return timer1.Interval; }
            set { timer1.Interval = value; }
        }

        private int blinkMaxDuration = 10000;

        [Description("Max blink total duration is ms. After this duration, the image stop to blink, becoming visible all time.")]
        [Category("Blinking effect")]
        [Browsable(true)]
        [DefaultValue(10000)]
        public int BlinkMaxDuration
        {
            get { return blinkMaxDuration; }
            set { blinkMaxDuration = value; }
        }

        private DateTime? lastAcceptableBlinkDate = null;

        public BlinkingImage()
        {
            InitializeComponent();
            timer1.Interval = BLINK_INTERVAL_DEFAULT_VALUE;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Visible = !this.Visible;
            if (!lastAcceptableBlinkDate.HasValue)
                lastAcceptableBlinkDate = DateTime.Now.AddMilliseconds(blinkMaxDuration);
            else if (lastAcceptableBlinkDate < DateTime.Now)
            {
                timer1.Enabled = false;
                this.Visible = true;
                lastAcceptableBlinkDate = null;
            }
        }
    }
}
