namespace Reports
{
    partial class frmPeriodicalEvaluation
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPeriodicalEvaluation));
            this.chkList = new System.Windows.Forms.CheckBox();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripLabel15 = new System.Windows.Forms.ToolStripLabel();
            this.panCritiries = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.Label26 = new System.Windows.Forms.Label();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.mnuShowPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.panFinish = new System.Windows.Forms.Panel();
            this.lblFinish = new System.Windows.Forms.Label();
            this.mnuContractData = new System.Windows.Forms.ToolStripMenuItem();
            this.toolLeft = new System.Windows.Forms.ToolStrip();
            this.tsbCreatePDF = new System.Windows.Forms.ToolStripButton();
            this.tsbExcel = new System.Windows.Forms.ToolStripButton();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panCritiries.SuspendLayout();
            this.panFinish.SuspendLayout();
            this.toolLeft.SuspendLayout();
            this.mnuContext.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.SuspendLayout();
            // 
            // chkList
            // 
            this.chkList.AutoSize = true;
            this.chkList.Location = new System.Drawing.Point(12, 91);
            this.chkList.Name = "chkList";
            this.chkList.Size = new System.Drawing.Size(15, 14);
            this.chkList.TabIndex = 2103;
            this.chkList.UseVisualStyleBackColor = true;
            this.chkList.CheckedChanged += new System.EventHandler(this.chkList_CheckedChanged);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // ToolStripSeparator5
            // 
            this.ToolStripSeparator5.Name = "ToolStripSeparator5";
            this.ToolStripSeparator5.Size = new System.Drawing.Size(6, 28);
            // 
            // ToolStripLabel15
            // 
            this.ToolStripLabel15.Name = "ToolStripLabel15";
            this.ToolStripLabel15.Size = new System.Drawing.Size(10, 25);
            this.ToolStripLabel15.Text = " ";
            // 
            // panCritiries
            // 
            this.panCritiries.BackColor = System.Drawing.Color.Gainsboro;
            this.panCritiries.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panCritiries.Controls.Add(this.btnSearch);
            this.panCritiries.Controls.Add(this.Label26);
            this.panCritiries.Controls.Add(this.cmbYear);
            this.panCritiries.Location = new System.Drawing.Point(5, 4);
            this.panCritiries.Name = "panCritiries";
            this.panCritiries.Size = new System.Drawing.Size(1515, 46);
            this.panCritiries.TabIndex = 2101;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.Control;
            this.btnSearch.Location = new System.Drawing.Point(1410, 11);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(91, 27);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Αναζήτηση";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // Label26
            // 
            this.Label26.AutoSize = true;
            this.Label26.Location = new System.Drawing.Point(20, 18);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(33, 13);
            this.Label26.TabIndex = 1105;
            this.Label26.Text = "Έτος";
            // 
            // cmbYear
            // 
            this.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(59, 13);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(72, 21);
            this.cmbYear.TabIndex = 4;
            // 
            // mnuShowPDF
            // 
            this.mnuShowPDF.Name = "mnuShowPDF";
            this.mnuShowPDF.Size = new System.Drawing.Size(195, 22);
            this.mnuShowPDF.Text = "Προβολή PDF-αρχείου";
            this.mnuShowPDF.Click += new System.EventHandler(this.mnuShowPDF_Click);
            // 
            // panFinish
            // 
            this.panFinish.BackColor = System.Drawing.Color.Pink;
            this.panFinish.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panFinish.Controls.Add(this.lblFinish);
            this.panFinish.Location = new System.Drawing.Point(602, 310);
            this.panFinish.Name = "panFinish";
            this.panFinish.Size = new System.Drawing.Size(330, 94);
            this.panFinish.TabIndex = 2104;
            this.panFinish.Visible = false;
            // 
            // lblFinish
            // 
            this.lblFinish.AutoSize = true;
            this.lblFinish.Location = new System.Drawing.Point(131, 40);
            this.lblFinish.Name = "lblFinish";
            this.lblFinish.Size = new System.Drawing.Size(74, 13);
            this.lblFinish.TabIndex = 565;
            this.lblFinish.Text = "Create PDF ...";
            // 
            // mnuContractData
            // 
            this.mnuContractData.Name = "mnuContractData";
            this.mnuContractData.Size = new System.Drawing.Size(195, 22);
            this.mnuContractData.Text = "Στοιχεία Σύμβασης";
            this.mnuContractData.Click += new System.EventHandler(this.mnuContractData_Click);
            // 
            // toolLeft
            // 
            this.toolLeft.AutoSize = false;
            this.toolLeft.BackColor = System.Drawing.Color.Gainsboro;
            this.toolLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolLeft.Dock = System.Windows.Forms.DockStyle.None;
            this.toolLeft.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolLeft.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolLeft.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripLabel15,
            this.tsbCreatePDF,
            this.ToolStripSeparator5,
            this.tsbExcel,
            this.ToolStripSeparator2,
            this.tsbHelp});
            this.toolLeft.Location = new System.Drawing.Point(5, 56);
            this.toolLeft.Name = "toolLeft";
            this.toolLeft.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolLeft.Size = new System.Drawing.Size(111, 28);
            this.toolLeft.TabIndex = 2102;
            this.toolLeft.Text = "ToolStrip1";
            // 
            // tsbCreatePDF
            // 
            this.tsbCreatePDF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCreatePDF.Image = global::Reports.Properties.Resources.pdf_icon_16x16_14;
            this.tsbCreatePDF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCreatePDF.Name = "tsbCreatePDF";
            this.tsbCreatePDF.Size = new System.Drawing.Size(23, 25);
            this.tsbCreatePDF.Text = "Create PDF";
            this.tsbCreatePDF.Click += new System.EventHandler(this.tsbCreatePDF_Click);
            // 
            // tsbExcel
            // 
            this.tsbExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExcel.Image = global::Reports.Properties.Resources.excel;
            this.tsbExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExcel.Name = "tsbExcel";
            this.tsbExcel.Size = new System.Drawing.Size(23, 25);
            this.tsbExcel.Text = "Εξαγωγή στο Excel";
            this.tsbExcel.Click += new System.EventHandler(this.tsbExcel_Click);
            // 
            // tsbHelp
            // 
            this.tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbHelp.Image")));
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(23, 25);
            this.tsbHelp.Text = "Βοήθεια";
            // 
            // mnuContext
            // 
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContractData,
            this.mnuShowPDF});
            this.mnuContext.Name = "mnuContext";
            this.mnuContext.Size = new System.Drawing.Size(196, 48);
            // 
            // fgList
            // 
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(5, 87);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(1521, 707);
            this.fgList.SubtotalPosition = C1.Win.C1FlexGrid.SubtotalPositionEnum.BelowData;
            this.fgList.TabIndex = 2105;
            // 
            // frmPeriodicalEvaluation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Beige;
            this.ClientSize = new System.Drawing.Size(1537, 689);
            this.Controls.Add(this.chkList);
            this.Controls.Add(this.panCritiries);
            this.Controls.Add(this.panFinish);
            this.Controls.Add(this.toolLeft);
            this.Controls.Add(this.fgList);
            this.Name = "frmPeriodicalEvaluation";
            this.Text = "Περιοδική Αξιολόγηση Καταλληλότητας";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPeriodicalEvaluation_Load);
            this.panCritiries.ResumeLayout(false);
            this.panCritiries.PerformLayout();
            this.panFinish.ResumeLayout(false);
            this.panFinish.PerformLayout();
            this.toolLeft.ResumeLayout(false);
            this.toolLeft.PerformLayout();
            this.mnuContext.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.CheckBox chkList;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator5;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel15;
        internal System.Windows.Forms.Panel panCritiries;
        internal System.Windows.Forms.Button btnSearch;
        internal System.Windows.Forms.Label Label26;
        internal System.Windows.Forms.ComboBox cmbYear;
        internal System.Windows.Forms.ToolStripMenuItem mnuShowPDF;
        internal System.Windows.Forms.Panel panFinish;
        internal System.Windows.Forms.Label lblFinish;
        internal System.Windows.Forms.ToolStripMenuItem mnuContractData;
        internal System.Windows.Forms.ToolStrip toolLeft;
        internal System.Windows.Forms.ToolStripButton tsbExcel;
        internal System.Windows.Forms.ToolStripButton tsbCreatePDF;
        internal System.Windows.Forms.ToolStripButton tsbHelp;
        internal System.Windows.Forms.ContextMenuStrip mnuContext;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
    }
}