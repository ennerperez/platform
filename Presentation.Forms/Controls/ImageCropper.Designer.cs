using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Platform.Presentation.Forms.Controls
{
    partial class ImageCropper
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
                if (cImage != null)
                {
                    cImage.Dispose();
                    cImage = null;
                }
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

            this.cropBox = new PictureBox();
            ((ISupportInitialize)this.cropBox).BeginInit();
            base.SuspendLayout();
            this.cropBox.Dock = DockStyle.Fill;
            this.cropBox.Location = new Point(0, 0);
            this.cropBox.Margin = new Padding(0);
            this.cropBox.Name = "cropBox";
            this.cropBox.Size = new Size(580, 580);
            this.cropBox.TabIndex = 0;
            this.cropBox.TabStop = false;
            this.cropBox.Paint += new PaintEventHandler(this.cropBox_Paint);
            this.cropBox.MouseDown += new MouseEventHandler(this.cropBox_MouseDown);
            this.cropBox.MouseLeave += new EventHandler(this.cropBox_MouseLeave);
            this.cropBox.MouseMove += new MouseEventHandler(this.cropBox_MouseMove);
            this.cropBox.MouseUp += new MouseEventHandler(this.cropBox_MouseUp);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.cropBox);
            base.Margin = new Padding(0);
            this.MaximumSize = new Size(580, 580);
            this.MinimumSize = new Size(580, 580);
            base.Name = "imageCropper";
            base.Size = new Size(580, 580);
            ((ISupportInitialize)this.cropBox).EndInit();
            base.ResumeLayout(false);
        }

        #endregion

        private PictureBox cropBox;

    }
}
