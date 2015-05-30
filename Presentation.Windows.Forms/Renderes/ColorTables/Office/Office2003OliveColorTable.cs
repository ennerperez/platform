using Platform.Presentation.Windows.Forms.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Platform.Presentation.Windows.Forms.Renderers.ColorTables
{
    public class Office2003OliveColorTable : ProfessionalColorTable
    {
        public override Color ButtonSelectedHighlight
        {
            get { return Color.FromArgb(223, 227, 213); }
        }

        public override Color ButtonSelectedHighlightBorder
        {
            get { return Color.FromArgb(63, 93, 56); }
        }

        public override Color ButtonPressedHighlight
        {
            get { return Color.FromArgb(200, 206, 182); }
        }

        public override Color ButtonPressedHighlightBorder
        {
            get { return Color.FromArgb(147, 160, 112); }
        }

        public override Color ButtonCheckedHighlight
        {
            get { return Color.FromArgb(223, 227, 213); }
        }

        public override Color ButtonCheckedHighlightBorder
        {
            get { return Color.FromArgb(147, 160, 112); }
        }

        public override Color ButtonPressedBorder
        {
            get { return Color.FromArgb(63, 93, 56); }
        }

        public override Color ButtonSelectedBorder
        {
            get { return Color.FromArgb(63, 93, 56); }
        }

        public override Color ButtonCheckedGradientBegin
        {
            get { return Color.FromArgb(255, 223, 154); }
        }

        public override Color ButtonCheckedGradientMiddle
        {
            get { return Color.FromArgb(255, 195, 116); }
        }

        public override Color ButtonCheckedGradientEnd
        {
            get { return Color.FromArgb(255, 166, 76); }
        }

        public override Color ButtonSelectedGradientBegin
        {
            get { return Color.FromArgb(255, 255, 222); }
        }

        public override Color ButtonSelectedGradientMiddle
        {
            get { return Color.FromArgb(255, 225, 172); }
        }

        public override Color ButtonSelectedGradientEnd
        {
            get { return Color.FromArgb(255, 203, 136); }
        }

        public override Color ButtonPressedGradientBegin
        {
            get { return Color.FromArgb(254, 128, 62); }
        }

        public override Color ButtonPressedGradientMiddle
        {
            get { return Color.FromArgb(255, 177, 109); }
        }

        public override Color ButtonPressedGradientEnd
        {
            get { return Color.FromArgb(255, 223, 154); }
        }

        public override Color CheckBackground
        {
            get { return Color.FromArgb(255, 192, 111); }
        }

        public override Color CheckSelectedBackground
        {
            get { return Color.FromArgb(254, 128, 62); }
        }

        public override Color CheckPressedBackground
        {
            get { return Color.FromArgb(254, 128, 62); }
        }

        public override Color GripDark
        {
            get { return Color.FromArgb(81, 94, 51); }
        }

        public override Color GripLight
        {
            get { return Color.FromArgb(255, 255, 255); }
        }

        public override Color ImageMarginGradientBegin
        {
            get { return Color.FromArgb(255, 255, 237); }
        }

        public override Color ImageMarginGradientMiddle
        {
            get { return Color.FromArgb(206, 220, 167); }
        }

        public override Color ImageMarginGradientEnd
        {
            get { return Color.FromArgb(181, 196, 143); }
        }

        public override Color ImageMarginRevealedGradientBegin
        {
            get { return Color.FromArgb(230, 230, 209); }
        }

        public override Color ImageMarginRevealedGradientMiddle
        {
            get { return Color.FromArgb(186, 201, 143); }
        }

        public override Color ImageMarginRevealedGradientEnd
        {
            get { return Color.FromArgb(160, 177, 116); }
        }

        public override Color MenuStripGradientBegin
        {
            get { return Color.FromArgb(217, 217, 167); }
        }

        public override Color MenuStripGradientEnd
        {
            get { return Color.FromArgb(242, 241, 228); }
        }

        public override Color MenuItemSelected
        {
            get { return Color.FromArgb(255, 238, 194); }
        }

        public override Color MenuItemBorder
        {
            get { return Color.FromArgb(63, 93, 56); }
        }

        public override Color MenuBorder
        {
            get { return Color.FromArgb(117, 141, 94); }
        }

        public override Color MenuItemSelectedGradientBegin
        {
            get { return Color.FromArgb(255, 255, 222); }
        }

        public override Color MenuItemSelectedGradientEnd
        {
            get { return Color.FromArgb(255, 203, 136); }
        }

        public override Color MenuItemPressedGradientBegin
        {
            get { return Color.FromArgb(237, 240, 214); }
        }

        public override Color MenuItemPressedGradientMiddle
        {
            get { return Color.FromArgb(186, 201, 143); }
        }

        public override Color MenuItemPressedGradientEnd
        {
            get { return Color.FromArgb(181, 196, 143); }
        }

        public override Color RaftingContainerGradientBegin
        {
            get { return Color.FromArgb(217, 217, 167); }
        }

        public override Color RaftingContainerGradientEnd
        {
            get { return Color.FromArgb(242, 241, 228); }
        }

        public override Color SeparatorDark
        {
            get { return Color.FromArgb(96, 128, 88); }
        }

        public override Color SeparatorLight
        {
            get { return Color.FromArgb(244, 247, 222); }
        }

        public override Color StatusStripGradientBegin
        {
            get { return Color.FromArgb(217, 217, 167); }
        }

        public override Color StatusStripGradientEnd
        {
            get { return Color.FromArgb(242, 241, 228); }
        }

        public override Color ToolStripBorder
        {
            get { return Color.FromArgb(96, 128, 88); }
        }

        public override Color ToolStripDropDownBackground
        {
            get { return Color.FromArgb(244, 244, 238); }
        }

        public override Color ToolStripGradientBegin
        {
            get { return Color.FromArgb(255, 255, 237); }
        }

        public override Color ToolStripGradientMiddle
        {
            get { return Color.FromArgb(206, 220, 167); }
        }

        public override Color ToolStripGradientEnd
        {
            get { return Color.FromArgb(181, 196, 143); }
        }

        public override Color ToolStripContentPanelGradientBegin
        {
            get { return Color.FromArgb(217, 217, 167); }
        }

        public override Color ToolStripContentPanelGradientEnd
        {
            get { return Color.FromArgb(242, 241, 228); }
        }

        public override Color ToolStripPanelGradientBegin
        {
            get { return Color.FromArgb(217, 217, 167); }
        }

        public override Color ToolStripPanelGradientEnd
        {
            get { return Color.FromArgb(242, 241, 228); }
        }

        public override Color OverflowButtonGradientBegin
        {
            get { return Color.FromArgb(186, 204, 150); }
        }

        public override Color OverflowButtonGradientMiddle
        {
            get { return Color.FromArgb(141, 160, 107); }
        }

        public override Color OverflowButtonGradientEnd
        {
            get { return Color.FromArgb(96, 119, 107); }
        }


    }

}
