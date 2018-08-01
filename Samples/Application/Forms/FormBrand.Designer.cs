namespace Platform.Samples.Forms
{
    partial class FormBrand
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
            this.richTextBoxLicense = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBrandBanner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBrandLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxBrandBanner
            // 
            this.pictureBoxBrandBanner.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBoxBrandBanner.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxBrandBanner.Name = "pictureBoxBrandBanner";
            this.pictureBoxBrandBanner.Size = new System.Drawing.Size(130, 441);
            this.pictureBoxBrandBanner.TabIndex = 0;
            this.pictureBoxBrandBanner.TabStop = false;
            // 
            // pictureBoxBrandLogo
            // 
            this.pictureBoxBrandLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxBrandLogo.Location = new System.Drawing.Point(548, 12);
            this.pictureBoxBrandLogo.Name = "pictureBoxBrandLogo";
            this.pictureBoxBrandLogo.Size = new System.Drawing.Size(64, 64);
            this.pictureBoxBrandLogo.TabIndex = 1;
            this.pictureBoxBrandLogo.TabStop = false;
            // 
            // richTextBoxEULA
            // 
            this.richTextBoxEULA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxEULA.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxEULA.Name = "richTextBoxEULA";
            this.richTextBoxEULA.ReadOnly = true;
            this.richTextBoxEULA.Size = new System.Drawing.Size(473, 162);
            this.richTextBoxEULA.TabIndex = 2;
            this.richTextBoxEULA.Text = "";
            // 
            // labelInfo
            // 
            this.labelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelInfo.Location = new System.Drawing.Point(136, 9);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(406, 67);
            this.labelInfo.TabIndex = 3;
            // 
            // linkLabelURL
            // 
            this.linkLabelURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelURL.Location = new System.Drawing.Point(136, 409);
            this.linkLabelURL.Name = "linkLabelURL";
            this.linkLabelURL.Size = new System.Drawing.Size(476, 23);
            this.linkLabelURL.TabIndex = 4;
            // 
            // richTextBoxLicense
            // 
            this.richTextBoxLicense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxLicense.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxLicense.Name = "richTextBoxLicense";
            this.richTextBoxLicense.ReadOnly = true;
            this.richTextBoxLicense.Size = new System.Drawing.Size(473, 158);
            this.richTextBoxLicense.TabIndex = 2;
            this.richTextBoxLicense.Text = "";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(139, 82);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.richTextBoxEULA);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.richTextBoxLicense);
            this.splitContainer1.Size = new System.Drawing.Size(473, 324);
            this.splitContainer1.SplitterDistance = 162;
            this.splitContainer1.TabIndex = 5;
            // 
            // FormBrand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.linkLabelURL);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.pictureBoxBrandLogo);
            this.Controls.Add(this.pictureBoxBrandBanner);
            this.Name = "FormBrand";
            this.Text = "Branding";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBrandBanner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBrandLogo)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxBrandBanner;
        private System.Windows.Forms.PictureBox pictureBoxBrandLogo;
        private System.Windows.Forms.RichTextBox richTextBoxEULA;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.LinkLabel linkLabelURL;
        private System.Windows.Forms.RichTextBox richTextBoxLicense;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

