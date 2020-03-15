namespace Platform.Samples.Forms
{
    partial class FormControls
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.propertyGridControl = new System.Windows.Forms.PropertyGrid();
            this.comboBoxControls = new System.Windows.Forms.ComboBox();
            this.labelControls = new System.Windows.Forms.Label();
            this.panelPreview = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // propertyGridControl
            // 
            this.propertyGridControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.propertyGridControl.Location = new System.Drawing.Point(406, 0);
            this.propertyGridControl.Name = "propertyGridControl";
            this.propertyGridControl.Size = new System.Drawing.Size(218, 441);
            this.propertyGridControl.TabIndex = 3;
            // 
            // comboBoxControls
            // 
            this.comboBoxControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxControls.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxControls.FormattingEnabled = true;
            this.comboBoxControls.Location = new System.Drawing.Point(12, 25);
            this.comboBoxControls.Name = "comboBoxControls";
            this.comboBoxControls.Size = new System.Drawing.Size(379, 21);
            this.comboBoxControls.TabIndex = 1;
            this.comboBoxControls.SelectedIndexChanged += new System.EventHandler(this.comboBoxControls_SelectedIndexChanged);
            // 
            // labelControls
            // 
            this.labelControls.AutoSize = true;
            this.labelControls.Location = new System.Drawing.Point(12, 9);
            this.labelControls.Name = "labelControls";
            this.labelControls.Size = new System.Drawing.Size(43, 13);
            this.labelControls.TabIndex = 0;
            this.labelControls.Text = "&Control:";
            // 
            // panelPreview
            // 
            this.panelPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPreview.Location = new System.Drawing.Point(12, 52);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(379, 377);
            this.panelPreview.TabIndex = 2;
            // 
            // FormControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.panelPreview);
            this.Controls.Add(this.labelControls);
            this.Controls.Add(this.comboBoxControls);
            this.Controls.Add(this.propertyGridControl);
            this.Name = "FormControls";
            this.Text = "Controls";
            this.Load += new System.EventHandler(this.FormControls_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGridControl;
        private System.Windows.Forms.ComboBox comboBoxControls;
        private System.Windows.Forms.Label labelControls;
        private System.Windows.Forms.Panel panelPreview;
    }
}