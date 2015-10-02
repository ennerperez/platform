namespace Sample.Branding
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
            this.pictureBoxBrandBanner = new System.Windows.Forms.PictureBox();
            this.pictureBoxBrandLogo = new System.Windows.Forms.PictureBox();
            this.richTextBoxEULA = new System.Windows.Forms.RichTextBox();
            this.labelInfo = new System.Windows.Forms.Label();
            this.linkLabelURL = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBrandBanner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBrandLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxBrandBanner
            // 
            this.pictureBoxBrandBanner.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBoxBrandBanner.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxBrandBanner.Name = "pictureBoxBrandBanner";
            this.pictureBoxBrandBanner.Size = new System.Drawing.Size(130, 261);
            this.pictureBoxBrandBanner.TabIndex = 0;
            this.pictureBoxBrandBanner.TabStop = false;
            // 
            // pictureBoxBrandLogo
            // 
            this.pictureBoxBrandLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxBrandLogo.Location = new System.Drawing.Point(478, 12);
            this.pictureBoxBrandLogo.Name = "pictureBoxBrandLogo";
            this.pictureBoxBrandLogo.Size = new System.Drawing.Size(64, 64);
            this.pictureBoxBrandLogo.TabIndex = 1;
            this.pictureBoxBrandLogo.TabStop = false;
            // 
            // richTextBoxEULA
            // 
            this.richTextBoxEULA.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxEULA.Location = new System.Drawing.Point(136, 79);
            this.richTextBoxEULA.Name = "richTextBoxEULA";
            this.richTextBoxEULA.ReadOnly = true;
            this.richTextBoxEULA.Size = new System.Drawing.Size(406, 147);
            this.richTextBoxEULA.TabIndex = 2;
            this.richTextBoxEULA.Text = "";
            // 
            // labelInfo
            // 
            this.labelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelInfo.Location = new System.Drawing.Point(136, 9);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(336, 67);
            this.labelInfo.TabIndex = 3;
            // 
            // linkLabelURL
            // 
            this.linkLabelURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelURL.Location = new System.Drawing.Point(136, 229);
            this.linkLabelURL.Name = "linkLabelURL";
            this.linkLabelURL.Size = new System.Drawing.Size(406, 23);
            this.linkLabelURL.TabIndex = 4;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 261);
            this.Controls.Add(this.linkLabelURL);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.richTextBoxEULA);
            this.Controls.Add(this.pictureBoxBrandLogo);
            this.Controls.Add(this.pictureBoxBrandBanner);
            this.Name = "FormMain";
            this.Text = "Branding";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBrandBanner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBrandLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxBrandBanner;
        private System.Windows.Forms.PictureBox pictureBoxBrandLogo;
        private System.Windows.Forms.RichTextBox richTextBoxEULA;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.LinkLabel linkLabelURL;
    }
}

