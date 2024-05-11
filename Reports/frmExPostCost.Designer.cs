namespace Reports
{
    partial class frmExPostCost
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExPostCost));
            this.btnSearch = new System.Windows.Forms.Button();
            this.Label26 = new System.Windows.Forms.Label();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.panFinish = new System.Windows.Forms.Panel();
            this.lblFinish = new System.Windows.Forms.Label();
            this.chkList = new System.Windows.Forms.CheckBox();
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuContractData = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.panImport = new System.Windows.Forms.Panel();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.panCritiries = new System.Windows.Forms.Panel();
            this.ToolStripLabel15 = new System.Windows.Forms.ToolStripLabel();
            this.ToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolLeft = new System.Windows.Forms.ToolStrip();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.btnGetImport = new System.Windows.Forms.Button();
            this.picFilesPath = new System.Windows.Forms.PictureBox();
            this.tsbImport = new System.Windows.Forms.ToolStripButton();
            this.tsbDel = new System.Windows.Forms.ToolStripButton();
            this.tsbExcel = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbCreatePDF = new System.Windows.Forms.ToolStripButton();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.panFinish.SuspendLayout();
            this.mnuContext.SuspendLayout();
            this.panImport.SuspendLayout();
            this.panCritiries.SuspendLayout();
            this.toolLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFilesPath)).BeginInit();
            this.SuspendLayout();
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
            // panFinish
            // 
            this.panFinish.BackColor = System.Drawing.Color.Pink;
            this.panFinish.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panFinish.Controls.Add(this.lblFinish);
            this.panFinish.Location = new System.Drawing.Point(605, 314);
            this.panFinish.Name = "panFinish";
            this.panFinish.Size = new System.Drawing.Size(330, 94);
            this.panFinish.TabIndex = 2098;
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
            // chkList
            // 
            this.chkList.AutoSize = true;
            this.chkList.Location = new System.Drawing.Point(15, 95);
            this.chkList.Name = "chkList";
            this.chkList.Size = new System.Drawing.Size(15, 14);
            this.chkList.TabIndex = 2097;
            this.chkList.UseVisualStyleBackColor = true;
            this.chkList.CheckedChanged += new System.EventHandler(this.chkList_CheckedChanged);
            // 
            // mnuContext
            // 
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContractData,
            this.mnuShowPDF});
            this.mnuContext.Name = "mnuContext";
            this.mnuContext.Size = new System.Drawing.Size(196, 48);
            // 
            // mnuContractData
            // 
            this.mnuContractData.Name = "mnuContractData";
            this.mnuContractData.Size = new System.Drawing.Size(195, 22);
            this.mnuContractData.Text = "Στοιχεία Σύμβασης";
            this.mnuContractData.Click += new System.EventHandler(this.mnuContractData_Click);
            // 
            // mnuShowPDF
            // 
            this.mnuShowPDF.Name = "mnuShowPDF";
            this.mnuShowPDF.Size = new System.Drawing.Size(195, 22);
            this.mnuShowPDF.Text = "Προβολή PDF-αρχείου";
            this.mnuShowPDF.Click += new System.EventHandler(this.mnuShowPDF_Click);
            // 
            // panImport
            // 
            this.panImport.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panImport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panImport.Controls.Add(this.picClose);
            this.panImport.Controls.Add(this.btnGetImport);
            this.panImport.Controls.Add(this.txtFilePath);
            this.panImport.Controls.Add(this.picFilesPath);
            this.panImport.Controls.Add(this.Label1);
            this.panImport.Location = new System.Drawing.Point(9, 95);
            this.panImport.Name = "panImport";
            this.panImport.Size = new System.Drawing.Size(555, 67);
            this.panImport.TabIndex = 2099;
            this.panImport.Visible = false;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(66, 22);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(322, 20);
            this.txtFilePath.TabIndex = 20;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(19, 27);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(41, 13);
            this.Label1.TabIndex = 216;
            this.Label1.Text = "Αρχείο";
            // 
            // panCritiries
            // 
            this.panCritiries.BackColor = System.Drawing.Color.Gainsboro;
            this.panCritiries.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panCritiries.Controls.Add(this.btnSearch);
            this.panCritiries.Controls.Add(this.Label26);
            this.panCritiries.Controls.Add(this.cmbYear);
            this.panCritiries.Location = new System.Drawing.Point(8, 8);
            this.panCritiries.Name = "panCritiries";
            this.panCritiries.Size = new System.Drawing.Size(1515, 46);
            this.panCritiries.TabIndex = 2095;
            // 
            // ToolStripLabel15
            // 
            this.ToolStripLabel15.Name = "ToolStripLabel15";
            this.ToolStripLabel15.Size = new System.Drawing.Size(10, 25);
            this.ToolStripLabel15.Text = " ";
            // 
            // ToolStripSeparator5
            // 
            this.ToolStripSeparator5.Name = "ToolStripSeparator5";
            this.ToolStripSeparator5.Size = new System.Drawing.Size(6, 28);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // ToolStripSeparator7
            // 
            this.ToolStripSeparator7.Name = "ToolStripSeparator7";
            this.ToolStripSeparator7.Size = new System.Drawing.Size(6, 28);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 28);
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
            this.tsbImport,
            this.ToolStripSeparator5,
            this.tsbDel,
            this.tsbExcel,
            this.ToolStripSeparator2,
            this.tsbSave,
            this.ToolStripSeparator7,
            this.tsbCreatePDF,
            this.ToolStripSeparator1,
            this.tsbHelp});
            this.toolLeft.Location = new System.Drawing.Point(8, 60);
            this.toolLeft.Name = "toolLeft";
            this.toolLeft.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolLeft.Size = new System.Drawing.Size(190, 28);
            this.toolLeft.TabIndex = 2096;
            this.toolLeft.Text = "ToolStrip1";
            // 
            // fgList
            // 
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(8, 92);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(1521, 707);
            this.fgList.SubtotalPosition = C1.Win.C1FlexGrid.SubtotalPositionEnum.BelowData;
            this.fgList.TabIndex = 2100;
            // 
            // picClose
            // 
            this.picClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picClose.Image = global::Reports.Properties.Resources.cancel;
            this.picClose.Location = new System.Drawing.Point(530, 3);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(18, 18);
            this.picClose.TabIndex = 1065;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            // 
            // btnGetImport
            // 
            this.btnGetImport.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnGetImport.FlatAppearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.btnGetImport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnGetImport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnGetImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetImport.Image = global::Reports.Properties.Resources.OK;
            this.btnGetImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGetImport.Location = new System.Drawing.Point(421, 21);
            this.btnGetImport.Name = "btnGetImport";
            this.btnGetImport.Size = new System.Drawing.Size(91, 25);
            this.btnGetImport.TabIndex = 22;
            this.btnGetImport.Text = "OK";
            this.btnGetImport.UseVisualStyleBackColor = false;
            this.btnGetImport.Click += new System.EventHandler(this.btnGetImport_Click);
            // 
            // picFilesPath
            // 
            this.picFilesPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picFilesPath.Image = ((System.Drawing.Image)(resources.GetObject("picFilesPath.Image")));
            this.picFilesPath.Location = new System.Drawing.Point(391, 21);
            this.picFilesPath.Name = "picFilesPath";
            this.picFilesPath.Size = new System.Drawing.Size(24, 24);
            this.picFilesPath.TabIndex = 183;
            this.picFilesPath.TabStop = false;
            this.picFilesPath.Click += new System.EventHandler(this.picFilesPath_Click);
            // 
            // tsbImport
            // 
            this.tsbImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbImport.Image = ((System.Drawing.Image)(resources.GetObject("tsbImport.Image")));
            this.tsbImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImport.Name = "tsbImport";
            this.tsbImport.Size = new System.Drawing.Size(23, 25);
            this.tsbImport.Text = "Εισαγωγή AUMs";
            this.tsbImport.Click += new System.EventHandler(this.tsbImport_Click);
            // 
            // tsbDel
            // 
            this.tsbDel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDel.Image = global::Reports.Properties.Resources.minus;
            this.tsbDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDel.Name = "tsbDel";
            this.tsbDel.Size = new System.Drawing.Size(23, 25);
            this.tsbDel.Text = "Διαγραφή";
            this.tsbDel.Click += new System.EventHandler(this.tsbDel_Click);
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
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = global::Reports.Properties.Resources.save;
            this.tsbSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(24, 25);
            this.tsbSave.Text = "Αποθήκευση λίστας";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tsbCreatePDF
            // 
            this.tsbCreatePDF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCreatePDF.Image = global::Reports.Properties.Resources.pdf;
            this.tsbCreatePDF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCreatePDF.Name = "tsbCreatePDF";
            this.tsbCreatePDF.Size = new System.Drawing.Size(23, 25);
            this.tsbCreatePDF.Text = "Create PDF";
            this.tsbCreatePDF.Click += new System.EventHandler(this.tsbCreatePDF_Click);
            // 
            // tsbHelp
            // 
            this.tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbHelp.Image")));
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(23, 20);
            this.tsbHelp.Text = "Βοήθεια";
            // 
            // frmExPostCost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(1537, 689);
            this.Controls.Add(this.panImport);
            this.Controls.Add(this.toolLeft);
            this.Controls.Add(this.panFinish);
            this.Controls.Add(this.chkList);
            this.Controls.Add(this.panCritiries);
            this.Controls.Add(this.fgList);
            this.Name = "frmExPostCost";
            this.Text = "Ex post Cost";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmExPostCost_Load);
            this.panFinish.ResumeLayout(false);
            this.panFinish.PerformLayout();
            this.mnuContext.ResumeLayout(false);
            this.panImport.ResumeLayout(false);
            this.panImport.PerformLayout();
            this.panCritiries.ResumeLayout(false);
            this.panCritiries.PerformLayout();
            this.toolLeft.ResumeLayout(false);
            this.toolLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFilesPath)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnSearch;
        internal System.Windows.Forms.Label Label26;
        internal System.Windows.Forms.ComboBox cmbYear;
        internal System.Windows.Forms.Panel panFinish;
        internal System.Windows.Forms.Label lblFinish;
        internal System.Windows.Forms.CheckBox chkList;
        internal System.Windows.Forms.ContextMenuStrip mnuContext;
        internal System.Windows.Forms.ToolStripMenuItem mnuContractData;
        internal System.Windows.Forms.ToolStripMenuItem mnuShowPDF;
        internal System.Windows.Forms.PictureBox picFilesPath;
        internal System.Windows.Forms.Panel panImport;
        internal System.Windows.Forms.PictureBox picClose;
        internal System.Windows.Forms.Button btnGetImport;
        internal System.Windows.Forms.TextBox txtFilePath;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Panel panCritiries;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel15;
        internal System.Windows.Forms.ToolStripButton tsbImport;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator5;
        internal System.Windows.Forms.ToolStripButton tsbExcel;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        internal System.Windows.Forms.ToolStripButton tsbSave;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator7;
        internal System.Windows.Forms.ToolStripButton tsbCreatePDF;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripButton tsbHelp;
        internal System.Windows.Forms.ToolStrip toolLeft;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
        internal System.Windows.Forms.ToolStripButton tsbDel;
    }
}