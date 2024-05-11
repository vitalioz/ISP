
namespace Custody
{
    partial class frmExecutionFilesFX
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExecutionFilesFX));
            this.panCritiries = new System.Windows.Forms.Panel();
            this.cmbServiceProviders = new System.Windows.Forms.ComboBox();
            this.dAktionDate = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.tsbEffect = new System.Windows.Forms.ToolStripButton();
            this.chkExport = new System.Windows.Forms.CheckBox();
            this.panCritiries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.toolStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panCritiries
            // 
            this.panCritiries.BackColor = System.Drawing.Color.Gainsboro;
            this.panCritiries.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panCritiries.Controls.Add(this.cmbServiceProviders);
            this.panCritiries.Controls.Add(this.dAktionDate);
            this.panCritiries.Controls.Add(this.btnSearch);
            this.panCritiries.Controls.Add(this.Label1);
            this.panCritiries.Controls.Add(this.Label11);
            this.panCritiries.Location = new System.Drawing.Point(8, 8);
            this.panCritiries.Name = "panCritiries";
            this.panCritiries.Size = new System.Drawing.Size(1458, 72);
            this.panCritiries.TabIndex = 1019;
            // 
            // cmbServiceProviders
            // 
            this.cmbServiceProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServiceProviders.FormattingEnabled = true;
            this.cmbServiceProviders.Location = new System.Drawing.Point(92, 39);
            this.cmbServiceProviders.Name = "cmbServiceProviders";
            this.cmbServiceProviders.Size = new System.Drawing.Size(207, 21);
            this.cmbServiceProviders.TabIndex = 1103;
            this.cmbServiceProviders.SelectedValueChanged += new System.EventHandler(this.cmbServiceProviders_SelectedValueChanged);
            // 
            // dAktionDate
            // 
            this.dAktionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dAktionDate.Location = new System.Drawing.Point(92, 13);
            this.dAktionDate.Name = "dAktionDate";
            this.dAktionDate.Size = new System.Drawing.Size(93, 20);
            this.dAktionDate.TabIndex = 1102;
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(1359, 35);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(91, 27);
            this.btnSearch.TabIndex = 16;
            this.btnSearch.Text = "Αναζήτηση";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 16);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(66, 13);
            this.Label1.TabIndex = 1011;
            this.Label1.Text = "Ημερομηνία";
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(12, 41);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(51, 13);
            this.Label11.TabIndex = 2;
            this.Label11.Text = "Πάροχος";
            // 
            // fgList
            // 
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(8, 117);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(1458, 538);
            this.fgList.TabIndex = 1020;
            // 
            // toolStrip3
            // 
            this.toolStrip3.AutoSize = false;
            this.toolStrip3.BackColor = System.Drawing.Color.Gainsboro;
            this.toolStrip3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip3.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel4,
            this.tsbEffect});
            this.toolStrip3.Location = new System.Drawing.Point(9, 87);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip3.Size = new System.Drawing.Size(42, 26);
            this.toolStrip3.TabIndex = 1021;
            this.toolStrip3.Text = "ToolStrip1";
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(10, 23);
            this.toolStripLabel4.Text = " ";
            // 
            // tsbEffect
            // 
            this.tsbEffect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEffect.Image = global::Custody.Properties.Resources.evernote1;
            this.tsbEffect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEffect.Name = "tsbEffect";
            this.tsbEffect.Size = new System.Drawing.Size(23, 23);
            this.tsbEffect.Text = "Εισαγωγή στο Effect";
            this.tsbEffect.Click += new System.EventHandler(this.tsbEffect_Click);
            // 
            // chkExport
            // 
            this.chkExport.AutoSize = true;
            this.chkExport.Location = new System.Drawing.Point(14, 120);
            this.chkExport.Name = "chkExport";
            this.chkExport.Size = new System.Drawing.Size(15, 14);
            this.chkExport.TabIndex = 1022;
            this.chkExport.UseVisualStyleBackColor = true;
            this.chkExport.CheckedChanged += new System.EventHandler(this.chkFinish_CheckedChanged);
            // 
            // frmExecutionFilesFX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Thistle;
            this.ClientSize = new System.Drawing.Size(1474, 705);
            this.Controls.Add(this.chkExport);
            this.Controls.Add(this.toolStrip3);
            this.Controls.Add(this.fgList);
            this.Controls.Add(this.panCritiries);
            this.Name = "frmExecutionFilesFX";
            this.Text = "frmExecutionFilesFX";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmExecutionFilesFX_Load);
            this.panCritiries.ResumeLayout(false);
            this.panCritiries.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.Panel panCritiries;
        internal System.Windows.Forms.DateTimePicker dAktionDate;
        internal System.Windows.Forms.Button btnSearch;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.ComboBox cmbServiceProviders;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
        internal System.Windows.Forms.ToolStrip toolStrip3;
        internal System.Windows.Forms.ToolStripLabel toolStripLabel4;
        internal System.Windows.Forms.ToolStripButton tsbEffect;
        internal System.Windows.Forms.CheckBox chkExport;
    }
}