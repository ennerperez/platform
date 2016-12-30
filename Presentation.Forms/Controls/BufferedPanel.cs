using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Platform.Presentation.Forms.Controls
{

    [ToolboxBitmap(typeof(System.Windows.Forms.Panel))]
    [ToolboxItem(true)]
    public class BufferedPanel : Panel
    {
        #region Public Constructors

        public BufferedPanel()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
        }

        #endregion
    }

}
