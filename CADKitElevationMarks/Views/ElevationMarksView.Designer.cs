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
            this.flpMarksPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.flpDrawingStandards = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.flpDrawingStandards.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpMarksPanel
            // 
            this.flpMarksPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpMarksPanel.AutoScroll = true;
            this.flpMarksPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpMarksPanel.Location = new System.Drawing.Point(3, 109);
            this.flpMarksPanel.Name = "flpMarksPanel";
            this.flpMarksPanel.Size = new System.Drawing.Size(230, 255);
            this.flpMarksPanel.TabIndex = 1;
            // 
            // flpDrawingStandards
            // 
            this.flpDrawingStandards.Controls.Add(this.button1);
            this.flpDrawingStandards.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpDrawingStandards.Location = new System.Drawing.Point(3, 3);
            this.flpDrawingStandards.Name = "flpDrawingStandards";
            this.flpDrawingStandards.Size = new System.Drawing.Size(230, 100);
            this.flpDrawingStandards.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ElevationMarksView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flpDrawingStandards);
            this.Controls.Add(this.flpMarksPanel);
            this.Name = "ElevationMarksView";
            this.Size = new System.Drawing.Size(236, 367);
            this.flpDrawingStandards.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flpMarksPanel;
        private System.Windows.Forms.FlowLayoutPanel flpDrawingStandards;
        private System.Windows.Forms.Button button1;
    }
}
