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
            this.tabStandards = new System.Windows.Forms.TabControl();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.rbxGroup = new System.Windows.Forms.RadioButton();
            this.rbxBlock = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.gbxOutputFormat = new System.Windows.Forms.GroupBox();
            this.btnOptions = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.gbxOutputFormat.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabStandards
            // 
            this.tabStandards.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabStandards.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabStandards.Location = new System.Drawing.Point(3, 54);
            this.tabStandards.Multiline = true;
            this.tabStandards.Name = "tabStandards";
            this.tabStandards.SelectedIndex = 0;
            this.tabStandards.Size = new System.Drawing.Size(163, 310);
            this.tabStandards.TabIndex = 0;
            this.tabStandards.SelectedIndexChanged += new System.EventHandler(this.TabChange);
            this.tabStandards.TabIndexChanged += new System.EventHandler(this.TabChange);
            // 
            // rbxGroup
            // 
            this.rbxGroup.AutoSize = true;
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
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.gbxOutputFormat);
            this.flowLayoutPanel1.Controls.Add(this.btnOptions);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(163, 45);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // gbxOutputFormat
            // 
            this.gbxOutputFormat.Controls.Add(this.rbxGroup);
            this.gbxOutputFormat.Controls.Add(this.rbxBlock);
            this.gbxOutputFormat.Location = new System.Drawing.Point(3, 3);
            this.gbxOutputFormat.Name = "gbxOutputFormat";
            this.gbxOutputFormat.Size = new System.Drawing.Size(120, 30);
            this.gbxOutputFormat.TabIndex = 3;
            this.gbxOutputFormat.TabStop = false;
            // 
            // btnOptions
            // 
            this.btnOptions.Image = global::CADKitElevationMarks.Properties.Resources.options;
            this.btnOptions.Location = new System.Drawing.Point(129, 3);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(30, 30);
            this.btnOptions.TabIndex = 4;
            this.btnOptions.UseVisualStyleBackColor = true;
            // 
            // ElevationMarksView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.tabStandards);
            this.Name = "ElevationMarksView";
            this.Size = new System.Drawing.Size(169, 367);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.gbxOutputFormat.ResumeLayout(false);
            this.gbxOutputFormat.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabStandards;
        private System.Windows.Forms.ToolTip toolTips;
        private System.Windows.Forms.RadioButton rbxGroup;
        private System.Windows.Forms.RadioButton rbxBlock;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox gbxOutputFormat;
        private System.Windows.Forms.Button btnOptions;
    }
}
