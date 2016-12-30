using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Platform.Presentation.Forms.Controls
{
    partial class ImageFilter
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.fName = new Label();
            this.fImage = new PictureBox();
            ((ISupportInitialize)this.fImage).BeginInit();
            base.SuspendLayout();
            this.fName.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.fName.Location = new Point(10, 106);
            this.fName.Name = "fName";
            this.fName.Size = new Size(110, 23);
            this.fName.TabIndex = 1;
            this.fName.Text = "JUNO";
            this.fName.TextAlign = ContentAlignment.MiddleCenter;
            this.fName.Click += new EventHandler(this.fName_Click);
            this.fImage.Location = new Point(15, 0);
            this.fImage.Name = "fImage";
            this.fImage.Size = new Size(100, 100);
            this.fImage.SizeMode = PictureBoxSizeMode.Zoom;
            this.fImage.TabIndex = 0;
            this.fImage.TabStop = false;
            this.fImage.Click += new EventHandler(this.fImage_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.fName);
            base.Controls.Add(this.fImage);
            this.Cursor = Cursors.Hand;
            base.Margin = new Padding(0);
            base.Name = "imgFilter";
            base.Size = new Size(130, 130);
            base.Click += new EventHandler(this.imgFilter_Click);
            ((ISupportInitialize)this.fImage).EndInit();
            base.ResumeLayout(false);
        }

        #endregion

        private PictureBox fImage;
        private Label fName;

    }
}
