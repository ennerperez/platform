// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace Platform.Presentation.Forms.Components
{
    public class ToolTipExtended : ToolTip
    {
        public ToolTipExtended(IContainer container) : base(container)
        {
            if (BidiHelper.IsRightToLeft)
            {
                OwnerDraw = true;
                Draw += ToolTipExtended_Draw;
            }
        }

        public ToolTipExtended()
            : base()
        {
            if (BidiHelper.IsRightToLeft)
            {
                OwnerDraw = true;
                Draw += ToolTipExtended_Draw;
            }
        }

        private static void ToolTipExtended_Draw(object sender, DrawToolTipEventArgs e)
        {
            Debug.Assert(BidiHelper.IsRightToLeft, "ToolTipExtended should only be ownerdraw when running RTL");
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText(TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.RightToLeft | TextFormatFlags.Right);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (OwnerDraw)
                Draw -= ToolTipExtended_Draw;
        }
    }
}