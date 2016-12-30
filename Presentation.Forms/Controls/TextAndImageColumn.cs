using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace Platform.Presentation.Forms.Controls
{

    [ToolboxBitmap(typeof(System.Windows.Forms.DataGridViewTextBoxColumn))]
    [ToolboxItem(false)]
    public class DataGridViewTextAndImageColumn : DataGridViewTextBoxColumn
    {

        private Image imageValue;
        private Size imageSize;

        public DataGridViewTextAndImageColumn()
        {
        }

        private DataGridViewCell cellTemplate = new TextAndImageCell();

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return cellTemplate;
            }

            set
            {
                cellTemplate = value;
            }
        }



        public override object Clone()
        {
            DataGridViewTextAndImageColumn c = base.Clone() as DataGridViewTextAndImageColumn;
            c.imageValue = this.imageValue;
            c.imageSize = this.imageSize;
            return c;
        }



        public Image Image
        {
            get { return this.imageValue; }
            set
            {
                if (this.Image != value
                    && value != null)
                {
                    this.imageValue = value;
                    this.imageSize = value.Size;

                    if (this.InheritedStyle != null)
                    {
                        Padding inheritedPadding = this.InheritedStyle.Padding;
                        this.DefaultCellStyle.Padding = new Padding(imageSize.Width,
                                inheritedPadding.Top, inheritedPadding.Right,
                                inheritedPadding.Bottom);
                    }
                }
            }
        }

        private TextAndImageCell TextAndImageCellTemplate
        {
            get { return this.CellTemplate as TextAndImageCell; }
        }

        internal Size ImageSize
        {
            get { return imageSize; }
        }
    }

    public class TextAndImageCell : DataGridViewTextBoxCell
    {
        private Image imageValue;
        private Size imageSize;

        public override object Clone()
        {
            TextAndImageCell c = base.Clone() as TextAndImageCell;
            c.imageValue = this.imageValue;
            c.imageSize = this.imageSize;
            return c;
        }

        public Image Image
        {
            get
            {
                if (this.OwningColumn == null ||
                            this.OwningTextAndImageColumn == null)
                {
                    return imageValue;
                }

                else if (this.imageValue != null)
                {
                    return this.imageValue;
                }
                else
                {
                    return this.OwningTextAndImageColumn.Image;
                }
            }

            set
            {
                if (this.imageValue != value
                    && value != null)
                {
                    this.imageValue = value;
                    this.imageSize = value.Size;
                    Padding inheritedPadding = this.InheritedStyle.Padding;
                    this.Style.Padding = new Padding(imageSize.Width,
                                inheritedPadding.Top, inheritedPadding.Right,
                                inheritedPadding.Bottom);
                }
            }
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds,
                        Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState,
                        object value, object formattedValue, string errorText,
                        DataGridViewCellStyle cellStyle,
                        DataGridViewAdvancedBorderStyle advancedBorderStyle,
                        DataGridViewPaintParts paintParts)
        {
            // Paint the base content
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState,
                        value, formattedValue, errorText, cellStyle,
                        advancedBorderStyle, paintParts);

            if (this.Image != null)
            {
                // Draw the image clipped to the cell.
                System.Drawing.Drawing2D.GraphicsContainer container = graphics.BeginContainer();
                graphics.SetClip(cellBounds);
                graphics.DrawImageUnscaled(this.Image, cellBounds.Location);
                graphics.EndContainer(container);
            }
        }

        private DataGridViewTextAndImageColumn OwningTextAndImageColumn
        {
            get { return this.OwningColumn as DataGridViewTextAndImageColumn; }
        }

    }

}
