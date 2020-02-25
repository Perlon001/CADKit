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
            this.components = new System.ComponentModel.Container();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.rbxGroup = new System.Windows.Forms.RadioButton();
            this.rbxBlock = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.gbxOutputFormat = new System.Windows.Forms.GroupBox();
            this.btnOptions = new System.Windows.Forms.Button();
            this.flpMarks = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
            this.gbxOutputFormat.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbxGroup
            // 
            this.rbxGroup.AutoSize = true;
            this.rbxGroup.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rbxGroup.Location = new System.Drawing.Point(6, 7);
            this.rbxGroup.Name = "rbxGroup";
            this.rbxGroup.Size = new System.Drawing.Size(54, 17);
            this.rbxGroup.TabIndex = 0;
            this.rbxGroup.TabStop = true;
            this.rbxGroup.Text = "Grupa";
            this.rbxGroup.UseVisualStyleBackColor = true;
            // 
            // rbxBlock
            // 
            this.rbxBlock.AutoSize = true;
            this.rbxBlock.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rbxBlock.Location = new System.Drawing.Point(66, 7);
            this.rbxBlock.Name = "rbxBlock";
            this.rbxBlock.Size = new System.Drawing.Size(46, 17);
            this.rbxBlock.TabIndex = 1;
            this.rbxBlock.TabStop = true;
            this.rbxBlock.Text = "Blok";
            this.rbxBlock.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.flowLayoutPanel1.Controls.Add(this.gbxOutputFormat);
            this.flowLayoutPanel1.Controls.Add(this.btnOptions);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(420, 40);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // gbxOutputFormat
            // 
            this.gbxOutputFormat.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbxOutputFormat.Controls.Add(this.rbxGroup);
            this.gbxOutputFormat.Controls.Add(this.rbxBlock);
            this.gbxOutputFormat.Location = new System.Drawing.Point(3, 3);
            this.gbxOutputFormat.Name = "gbxOutputFormat";
            this.gbxOutputFormat.Size = new System.Drawing.Size(120, 28);
            this.gbxOutputFormat.TabIndex = 3;
            this.gbxOutputFormat.TabStop = false;
            // 
            // btnOptions
            // 
            this.btnOptions.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOptions.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnOptions.Image = global::CADKitElevationMarks.Properties.Resources.options;
            this.btnOptions.Location = new System.Drawing.Point(129, 3);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(33, 33);
            this.btnOptions.TabIndex = 4;
            this.btnOptions.UseVisualStyleBackColor = false;
            // 
            // flpMarks
            // 
            this.flpMarks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpMarks.Location = new System.Drawing.Point(3, 46);
            this.flpMarks.Name = "flpMarks";
            this.flpMarks.Size = new System.Drawing.Size(414, 489);
            this.flpMarks.TabIndex = 3;
            // 
            // ElevationMarksView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Controls.Add(this.flpMarks);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "ElevationMarksView";
            this.Size = new System.Drawing.Size(420, 538);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.gbxOutputFormat.ResumeLayout(false);
            this.gbxOutputFormat.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTips;
        private System.Windows.Forms.RadioButton rbxGroup;
        private System.Windows.Forms.RadioButton rbxBlock;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox gbxOutputFormat;
        private System.Windows.Forms.Button btnOptions;
        private System.Windows.Forms.FlowLayoutPanel flpMarks;
    }
}
