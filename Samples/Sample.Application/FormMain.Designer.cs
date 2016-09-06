namespace Sample
{
    partial class FormMain
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

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            Platform.Presentation.Forms.Controls.XMLViewer.XMLViewerSettings xmlViewerSettings1 = new Platform.Presentation.Forms.Controls.XMLViewer.XMLViewerSettings();
            this.xmlViewer1 = new Platform.Presentation.Forms.Controls.XMLViewer();
            this.SuspendLayout();
            // 
            // xmlViewer1
            // 
            this.xmlViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmlViewer1.Location = new System.Drawing.Point(0, 0);
            this.xmlViewer1.Name = "xmlViewer1";
            this.xmlViewer1.ReadOnly = true;
            xmlViewerSettings1.AttributeKey = System.Drawing.Color.Red;
            xmlViewerSettings1.AttributeValue = System.Drawing.Color.Blue;
            xmlViewerSettings1.Element = System.Drawing.Color.DarkRed;
            xmlViewerSettings1.Tag = System.Drawing.Color.Blue;
            xmlViewerSettings1.Value = System.Drawing.Color.Black;
            this.xmlViewer1.Settings = xmlViewerSettings1;
            this.xmlViewer1.Size = new System.Drawing.Size(624, 441);
            this.xmlViewer1.TabIndex = 0;
            this.xmlViewer1.Text = "";
            this.xmlViewer1.WordWrap = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.xmlViewer1);
            this.Name = "FormMain";
            this.Text = "Sample";
            this.ResumeLayout(false);

        }

        #endregion

        private Platform.Presentation.Forms.Controls.XMLViewer xmlViewer1;
    }
}

