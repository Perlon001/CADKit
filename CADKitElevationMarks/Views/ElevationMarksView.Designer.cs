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
            this.pnlProperties = new System.Windows.Forms.Panel();
            this.treeComponents = new System.Windows.Forms.TreeView();
            this.cmbMarkType = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.gbxOutputFormat.SuspendLayout();
            this.pnlProperties.SuspendLayout();
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
            this.flowLayoutPanel1.Size = new System.Drawing.Size(339, 40);
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
            this.flpMarks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMarks.Location = new System.Drawing.Point(0, 40);
            this.flpMarks.Name = "flpMarks";
            this.flpMarks.Size = new System.Drawing.Size(339, 433);
            this.flpMarks.TabIndex = 3;
            // 
            // pnlProperties
            // 
            this.pnlProperties.AutoScroll = true;
            this.pnlProperties.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnlProperties.Controls.Add(this.treeComponents);
            this.pnlProperties.Controls.Add(this.cmbMarkType);
            this.pnlProperties.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlProperties.Location = new System.Drawing.Point(0, 235);
            this.pnlProperties.Name = "pnlProperties";
            this.pnlProperties.Size = new System.Drawing.Size(339, 238);
            this.pnlProperties.TabIndex = 4;
            // 
            // treeComponents
            // 
            this.treeComponents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeComponents.Location = new System.Drawing.Point(0, 30);
            this.treeComponents.Name = "treeComponents";
            this.treeComponents.Size = new System.Drawing.Size(339, 73);
            this.treeComponents.TabIndex = 3;
            // 
            // cmbMarkType
            // 
            this.cmbMarkType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMarkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMarkType.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbMarkType.FormattingEnabled = true;
            this.cmbMarkType.Location = new System.Drawing.Point(3, 3);
            this.cmbMarkType.Name = "cmbMarkType";
            this.cmbMarkType.Size = new System.Drawing.Size(333, 21);
            this.cmbMarkType.TabIndex = 2;
            // 
            // ElevationMarksView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Controls.Add(this.pnlProperties);
            this.Controls.Add(this.flpMarks);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "ElevationMarksView";
            this.Size = new System.Drawing.Size(339, 473);
            this.Resize += new System.EventHandler(this.ViewResize);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.gbxOutputFormat.ResumeLayout(false);
            this.gbxOutputFormat.PerformLayout();
            this.pnlProperties.ResumeLayout(false);
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
        private System.Windows.Forms.Panel pnlProperties;
        private System.Windows.Forms.ComboBox cmbMarkType;
        private System.Windows.Forms.TreeView treeComponents;
    }
}
