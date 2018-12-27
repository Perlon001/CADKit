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
            this.cmbScale = new System.Windows.Forms.ComboBox();
            this.lblScale = new System.Windows.Forms.Label();
            this.cmbDimUnit = new System.Windows.Forms.ComboBox();
            this.lblDimUnit = new System.Windows.Forms.Label();
            this.cmbDrawUnit = new System.Windows.Forms.ComboBox();
            this.lblDrawUnit = new System.Windows.Forms.Label();
            this.gbxScale = new System.Windows.Forms.GroupBox();
            this.gbxLayers = new System.Windows.Forms.GroupBox();
            this.trvLayers = new System.Windows.Forms.TreeView();
            this.gbxScale.SuspendLayout();
            this.gbxLayers.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbScale
            // 
            this.cmbScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cmbScale.FormattingEnabled = true;
            this.cmbScale.Location = new System.Drawing.Point(128, 35);
            this.cmbScale.Name = "cmbScale";
            this.cmbScale.Size = new System.Drawing.Size(75, 21);
            this.cmbScale.TabIndex = 5;
            // 
            // lblScale
            // 
            this.lblScale.AutoSize = true;
            this.lblScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblScale.Location = new System.Drawing.Point(125, 16);
            this.lblScale.Name = "lblScale";
            this.lblScale.Size = new System.Drawing.Size(34, 13);
            this.lblScale.TabIndex = 4;
            this.lblScale.Text = "Skala";
            // 
            // cmbDimUnit
            // 
            this.cmbDimUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cmbDimUnit.FormattingEnabled = true;
            this.cmbDimUnit.Location = new System.Drawing.Point(67, 35);
            this.cmbDimUnit.Name = "cmbDimUnit";
            this.cmbDimUnit.Size = new System.Drawing.Size(55, 21);
            this.cmbDimUnit.TabIndex = 3;
            // 
            // lblDimUnit
            // 
            this.lblDimUnit.AutoSize = true;
            this.lblDimUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDimUnit.Location = new System.Drawing.Point(64, 16);
            this.lblDimUnit.Name = "lblDimUnit";
            this.lblDimUnit.Size = new System.Drawing.Size(60, 13);
            this.lblDimUnit.TabIndex = 2;
            this.lblDimUnit.Text = "Jedn. wym.";
            // 
            // cmbDrawUnit
            // 
            this.cmbDrawUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cmbDrawUnit.FormattingEnabled = true;
            this.cmbDrawUnit.Location = new System.Drawing.Point(6, 35);
            this.cmbDrawUnit.Name = "cmbDrawUnit";
            this.cmbDrawUnit.Size = new System.Drawing.Size(55, 21);
            this.cmbDrawUnit.TabIndex = 1;
            // 
            // lblDrawUnit
            // 
            this.lblDrawUnit.AutoSize = true;
            this.lblDrawUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDrawUnit.Location = new System.Drawing.Point(6, 16);
            this.lblDrawUnit.Name = "lblDrawUnit";
            this.lblDrawUnit.Size = new System.Drawing.Size(52, 13);
            this.lblDrawUnit.TabIndex = 0;
            this.lblDrawUnit.Text = "Jedn. rys.";
            // 
            // gbxScale
            // 
            this.gbxScale.Controls.Add(this.cmbScale);
            this.gbxScale.Controls.Add(this.lblDrawUnit);
            this.gbxScale.Controls.Add(this.lblScale);
            this.gbxScale.Controls.Add(this.cmbDrawUnit);
            this.gbxScale.Controls.Add(this.cmbDimUnit);
            this.gbxScale.Controls.Add(this.lblDimUnit);
            this.gbxScale.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbxScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gbxScale.Location = new System.Drawing.Point(0, 0);
            this.gbxScale.Name = "gbxScale";
            this.gbxScale.Size = new System.Drawing.Size(209, 66);
            this.gbxScale.TabIndex = 1;
            this.gbxScale.TabStop = false;
            this.gbxScale.Text = "Jednostki";
            // 
            // gbxLayers
            // 
            this.gbxLayers.Controls.Add(this.trvLayers);
            this.gbxLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxLayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gbxLayers.Location = new System.Drawing.Point(0, 66);
            this.gbxLayers.Name = "gbxLayers";
            this.gbxLayers.Size = new System.Drawing.Size(209, 248);
            this.gbxLayers.TabIndex = 2;
            this.gbxLayers.TabStop = false;
            this.gbxLayers.Text = "Warstwy";
            // 
            // trvLayers
            // 
            this.trvLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvLayers.Location = new System.Drawing.Point(3, 16);
            this.trvLayers.Name = "trvLayers";
            this.trvLayers.Size = new System.Drawing.Size(203, 229);
            this.trvLayers.TabIndex = 0;
            // 
            // SettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.gbxLayers);
            this.Controls.Add(this.gbxScale);
            this.Name = "SettingsView";
            this.Size = new System.Drawing.Size(209, 314);
            this.gbxScale.ResumeLayout(false);
            this.gbxScale.PerformLayout();
            this.gbxLayers.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbScale;
        private System.Windows.Forms.Label lblScale;
        private System.Windows.Forms.ComboBox cmbDimUnit;
        private System.Windows.Forms.Label lblDimUnit;
        private System.Windows.Forms.ComboBox cmbDrawUnit;
        private System.Windows.Forms.Label lblDrawUnit;
        private System.Windows.Forms.GroupBox gbxScale;
        private System.Windows.Forms.GroupBox gbxLayers;
        private System.Windows.Forms.TreeView trvLayers;
    }
}
