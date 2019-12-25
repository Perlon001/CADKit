namespace CADKitElevationMarks.Views
{
    partial class ElevationMarksView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabStandards = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // tabStandards
            // 
            this.tabStandards.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabStandards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabStandards.Location = new System.Drawing.Point(0, 0);
            this.tabStandards.Multiline = true;
            this.tabStandards.Name = "tabStandards";
            this.tabStandards.SelectedIndex = 0;
            this.tabStandards.Size = new System.Drawing.Size(243, 367);
            this.tabStandards.TabIndex = 0;
            // 
            // ElevationMarksView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabStandards);
            this.Name = "ElevationMarksView";
            this.Size = new System.Drawing.Size(243, 367);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabStandards;
    }
}
