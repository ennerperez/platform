namespace Platform.Samples.Forms
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
            this.commandLinkConsole = new Platform.Presentation.Forms.Controls.CommandLink();
            this.commandLinkBrand = new Platform.Presentation.Forms.Controls.CommandLink();
            this.commandLinkReports = new Platform.Presentation.Forms.Controls.CommandLink();
            this.SuspendLayout();
            // 
            // commandLinkConsole
            // 
            this.commandLinkConsole.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.commandLinkConsole.ImageScalingSize = new System.Drawing.Size(0, 0);
            this.commandLinkConsole.Location = new System.Drawing.Point(12, 12);
            this.commandLinkConsole.Name = "commandLinkConsole";
            this.commandLinkConsole.Note = "Starts a new console instance";
            this.commandLinkConsole.Size = new System.Drawing.Size(256, 64);
            this.commandLinkConsole.TabIndex = 0;
            this.commandLinkConsole.Text = "Launch Console";
            this.commandLinkConsole.Click += new System.EventHandler(this.commandLinkConsole_Click);
            // 
            // commandLinkBrand
            // 
            this.commandLinkBrand.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.commandLinkBrand.ImageScalingSize = new System.Drawing.Size(0, 0);
            this.commandLinkBrand.Location = new System.Drawing.Point(12, 82);
            this.commandLinkBrand.Name = "commandLinkBrand";
            this.commandLinkBrand.Note = "Shows the branding information";
            this.commandLinkBrand.Size = new System.Drawing.Size(256, 64);
            this.commandLinkBrand.TabIndex = 0;
            this.commandLinkBrand.Text = "Show branding";
            this.commandLinkBrand.Click += new System.EventHandler(this.commandLinkBrand_Click);
            // 
            // commandLinkReports
            // 
            this.commandLinkReports.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.commandLinkReports.ImageScalingSize = new System.Drawing.Size(0, 0);
            this.commandLinkReports.Location = new System.Drawing.Point(12, 152);
            this.commandLinkReports.Name = "commandLinkReports";
            this.commandLinkReports.Note = "Shows the report engine viewer";
            this.commandLinkReports.Size = new System.Drawing.Size(256, 64);
            this.commandLinkReports.TabIndex = 0;
            this.commandLinkReports.Text = "Show Reports";
            this.commandLinkReports.Click += new System.EventHandler(this.commandLinkReports_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.commandLinkReports);
            this.Controls.Add(this.commandLinkBrand);
            this.Controls.Add(this.commandLinkConsole);
            this.Name = "FormMain";
            this.Text = "Sample";
            this.ResumeLayout(false);

        }


        #endregion

        private Platform.Presentation.Forms.Controls.CommandLink commandLinkConsole;
        private Presentation.Forms.Controls.CommandLink commandLinkBrand;
        private Presentation.Forms.Controls.CommandLink commandLinkReports;
    }
}

