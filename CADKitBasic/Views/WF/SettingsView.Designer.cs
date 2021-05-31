namespace CADKitBasic.Views.WF
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
            this.tabCompositesSettings = new System.Windows.Forms.TabControl();
            this.tabComposites = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.trvComposites = new System.Windows.Forms.TreeView();
            this.dgvProperties = new System.Windows.Forms.DataGridView();
            this.PropertyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PropertyValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbxScale.SuspendLayout();
            this.tabCompositesSettings.SuspendLayout();
            this.tabComposites.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbScale
            // 
            this.cmbScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.cmbDimUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.cmbDrawUnit.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cmbDrawUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.gbxScale.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.gbxScale.Controls.Add(this.cmbScale);
            this.gbxScale.Controls.Add(this.lblDrawUnit);
            this.gbxScale.Controls.Add(this.lblScale);
            this.gbxScale.Controls.Add(this.cmbDrawUnit);
            this.gbxScale.Controls.Add(this.cmbDimUnit);
            this.gbxScale.Controls.Add(this.lblDimUnit);
            this.gbxScale.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbxScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gbxScale.ForeColor = System.Drawing.SystemColors.MenuBar;
            this.gbxScale.Location = new System.Drawing.Point(0, 0);
            this.gbxScale.Name = "gbxScale";
            this.gbxScale.Size = new System.Drawing.Size(398, 66);
            this.gbxScale.TabIndex = 1;
            this.gbxScale.TabStop = false;
            this.gbxScale.Text = "Jednostki";
            // 
            // tabCompositesSettings
            // 
            this.tabCompositesSettings.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabCompositesSettings.AllowDrop = true;
            this.tabCompositesSettings.Controls.Add(this.tabComposites);
            this.tabCompositesSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCompositesSettings.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tabCompositesSettings.Location = new System.Drawing.Point(0, 66);
            this.tabCompositesSettings.Multiline = true;
            this.tabCompositesSettings.Name = "tabCompositesSettings";
            this.tabCompositesSettings.SelectedIndex = 0;
            this.tabCompositesSettings.Size = new System.Drawing.Size(398, 471);
            this.tabCompositesSettings.TabIndex = 2;
            // 
            // tabComposites
            // 
            this.tabComposites.Controls.Add(this.splitContainer1);
            this.tabComposites.Location = new System.Drawing.Point(23, 4);
            this.tabComposites.Name = "tabComposites";
            this.tabComposites.Padding = new System.Windows.Forms.Padding(3);
            this.tabComposites.Size = new System.Drawing.Size(371, 463);
            this.tabComposites.TabIndex = 0;
            this.tabComposites.Text = "Ustawienia komponentów";
            this.tabComposites.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.trvComposites);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvProperties);
            this.splitContainer1.Size = new System.Drawing.Size(365, 457);
            this.splitContainer1.SplitterDistance = 228;
            this.splitContainer1.TabIndex = 2;
            // 
            // trvComposites
            // 
            this.trvComposites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvComposites.Location = new System.Drawing.Point(0, 0);
            this.trvComposites.Name = "trvComposites";
            this.trvComposites.Size = new System.Drawing.Size(365, 228);
            this.trvComposites.TabIndex = 0;
            this.trvComposites.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TrvComposites_AfterSelect);
            // 
            // dgvProperties
            // 
            this.dgvProperties.AllowUserToAddRows = false;
            this.dgvProperties.AllowUserToDeleteRows = false;
            this.dgvProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProperties.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PropertyName,
            this.PropertyValue});
            this.dgvProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProperties.Location = new System.Drawing.Point(0, 0);
            this.dgvProperties.Name = "dgvProperties";
            this.dgvProperties.ReadOnly = true;
            this.dgvProperties.RowHeadersVisible = false;
            this.dgvProperties.Size = new System.Drawing.Size(365, 225);
            this.dgvProperties.TabIndex = 1;
            this.dgvProperties.SelectionChanged += new System.EventHandler(this.DgvProperties_SelectionChanged);
            // 
            // PropertyName
            // 
            this.PropertyName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PropertyName.HeaderText = "Właściwości";
            this.PropertyName.Name = "PropertyName";
            this.PropertyName.ReadOnly = true;
            // 
            // PropertyValue
            // 
            this.PropertyValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PropertyValue.HeaderText = "";
            this.PropertyValue.Name = "PropertyValue";
            this.PropertyValue.ReadOnly = true;
            // 
            // SettingsView
            // 
            this.BackColor = System.Drawing.Color.Red;
            this.Controls.Add(this.tabCompositesSettings);
            this.Controls.Add(this.gbxScale);
            this.Name = "SettingsView";
            this.Size = new System.Drawing.Size(398, 537);
            this.gbxScale.ResumeLayout(false);
            this.gbxScale.PerformLayout();
            this.tabCompositesSettings.ResumeLayout(false);
            this.tabComposites.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProperties)).EndInit();
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
        private System.Windows.Forms.TabControl tabCompositesSettings;
        private System.Windows.Forms.TabPage tabComposites;
        private System.Windows.Forms.DataGridView dgvProperties;
        private System.Windows.Forms.TreeView trvComposites;
        private System.Windows.Forms.DataGridViewTextBoxColumn PropertyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PropertyValue;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
