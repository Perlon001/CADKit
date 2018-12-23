namespace CADKitCore.Views.WF
{
    partial class SettingsView
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
            this.pnlSettings = new System.Windows.Forms.Panel();
            this.lblDrawUnit = new System.Windows.Forms.Label();
            this.cmbDrawUnit = new System.Windows.Forms.ComboBox();
            this.cmbDimUnit = new System.Windows.Forms.ComboBox();
            this.lblDimUnit = new System.Windows.Forms.Label();
            this.cmbScale = new System.Windows.Forms.ComboBox();
            this.lblScale = new System.Windows.Forms.Label();
            this.pnlSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSettings
            // 
            this.pnlSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSettings.Controls.Add(this.cmbScale);
            this.pnlSettings.Controls.Add(this.lblScale);
            this.pnlSettings.Controls.Add(this.cmbDimUnit);
            this.pnlSettings.Controls.Add(this.lblDimUnit);
            this.pnlSettings.Controls.Add(this.cmbDrawUnit);
            this.pnlSettings.Controls.Add(this.lblDrawUnit);
            this.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSettings.Location = new System.Drawing.Point(0, 0);
            this.pnlSettings.Name = "pnlSettings";
            this.pnlSettings.Size = new System.Drawing.Size(209, 314);
            this.pnlSettings.TabIndex = 0;
            // 
            // lblDrawUnit
            // 
            this.lblDrawUnit.AutoSize = true;
            this.lblDrawUnit.Location = new System.Drawing.Point(3, 6);
            this.lblDrawUnit.Name = "lblDrawUnit";
            this.lblDrawUnit.Size = new System.Drawing.Size(52, 13);
            this.lblDrawUnit.TabIndex = 0;
            this.lblDrawUnit.Text = "Jedn. rys.";
            // 
            // cmbDrawUnit
            // 
            this.cmbDrawUnit.FormattingEnabled = true;
            this.cmbDrawUnit.Location = new System.Drawing.Point(3, 22);
            this.cmbDrawUnit.Name = "cmbDrawUnit";
            this.cmbDrawUnit.Size = new System.Drawing.Size(55, 21);
            this.cmbDrawUnit.TabIndex = 1;
            // 
            // cmbDimUnit
            // 
            this.cmbDimUnit.FormattingEnabled = true;
            this.cmbDimUnit.Location = new System.Drawing.Point(67, 22);
            this.cmbDimUnit.Name = "cmbDimUnit";
            this.cmbDimUnit.Size = new System.Drawing.Size(55, 21);
            this.cmbDimUnit.TabIndex = 3;
            // 
            // lblDimUnit
            // 
            this.lblDimUnit.AutoSize = true;
            this.lblDimUnit.Location = new System.Drawing.Point(64, 6);
            this.lblDimUnit.Name = "lblDimUnit";
            this.lblDimUnit.Size = new System.Drawing.Size(60, 13);
            this.lblDimUnit.TabIndex = 2;
            this.lblDimUnit.Text = "Jedn. wym.";
            // 
            // cmbScale
            // 
            this.cmbScale.FormattingEnabled = true;
            this.cmbScale.Location = new System.Drawing.Point(128, 22);
            this.cmbScale.Name = "cmbScale";
            this.cmbScale.Size = new System.Drawing.Size(65, 21);
            this.cmbScale.TabIndex = 5;
            // 
            // lblScale
            // 
            this.lblScale.AutoSize = true;
            this.lblScale.Location = new System.Drawing.Point(125, 6);
            this.lblScale.Name = "lblScale";
            this.lblScale.Size = new System.Drawing.Size(34, 13);
            this.lblScale.TabIndex = 4;
            this.lblScale.Text = "Skala";
            // 
            // SettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.pnlSettings);
            this.Name = "SettingsView";
            this.Size = new System.Drawing.Size(209, 314);
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSettings;
        private System.Windows.Forms.ComboBox cmbScale;
        private System.Windows.Forms.Label lblScale;
        private System.Windows.Forms.ComboBox cmbDimUnit;
        private System.Windows.Forms.Label lblDimUnit;
        private System.Windows.Forms.ComboBox cmbDrawUnit;
        private System.Windows.Forms.Label lblDrawUnit;
    }
}
