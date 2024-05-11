namespace Accounting
{
    partial class frmAcc_InvoicesFX
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAcc_InvoicesFX));
            this.tsbPrint = new System.Windows.Forms.ToolStripButton();
            this.cmbServiceProviders = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.toolLeft = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsbFeesCalculation = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbView = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExcel = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHistory = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSettings = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.panTools = new System.Windows.Forms.Panel();
            this.cmbFilter = new System.Windows.Forms.ComboBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panCritiries = new System.Windows.Forms.Panel();
            this.mnuNeaXreosi = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAkyrotiko = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPrintInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuContractData = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClientData = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuPistotiko = new System.Windows.Forms.ToolStripMenuItem();
            this.chkPrint = new System.Windows.Forms.CheckBox();
            this.imgFiles = new System.Windows.Forms.ImageList(this.components);
            this.ucExec = new Core.ucDoubleCalendar();
            this.toolLeft.SuspendLayout();
            this.panTools.SuspendLayout();
            this.panCritiries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.mnuContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsbPrint
            // 
            this.tsbPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPrint.Image = global::Accounting.Properties.Resources.PrintHS;
            this.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Size = new System.Drawing.Size(23, 25);
            this.tsbPrint.Text = "Εκτυπώσεις";
            this.tsbPrint.Click += new System.EventHandler(this.tsbPrint_Click);
            // 
            // cmbServiceProviders
            // 
            this.cmbServiceProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServiceProviders.FormattingEnabled = true;
            this.cmbServiceProviders.Location = new System.Drawing.Point(527, 7);
            this.cmbServiceProviders.Name = "cmbServiceProviders";
            this.cmbServiceProviders.Size = new System.Drawing.Size(242, 21);
            this.cmbServiceProviders.TabIndex = 253;
            this.cmbServiceProviders.SelectedIndexChanged += new System.EventHandler(this.cmbServiceProviders_SelectedIndexChanged);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(841, 11);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(47, 13);
            this.Label2.TabIndex = 257;
            this.Label2.Text = "Κωδικός";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(894, 7);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(172, 20);
            this.txtCode.TabIndex = 254;
            this.txtCode.TextChanged += new System.EventHandler(this.txtCode_TextChanged);
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(470, 12);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(51, 13);
            this.Label12.TabIndex = 256;
            this.Label12.Text = "Πάροχος";
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
            this.ToolStripLabel2,
            this.tsbFeesCalculation,
            this.toolStripSeparator2,
            this.tsbPrint,
            this.ToolStripSeparator3,
            this.tsbView,
            this.ToolStripSeparator1,
            this.tsbExcel,
            this.ToolStripSeparator6,
            this.tsbHistory,
            this.ToolStripSeparator4,
            this.tsbSettings,
            this.ToolStripSeparator11,
            this.tsbHelp});
            this.toolLeft.Location = new System.Drawing.Point(3, 4);
            this.toolLeft.Name = "toolLeft";
            this.toolLeft.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolLeft.Size = new System.Drawing.Size(228, 28);
            this.toolLeft.TabIndex = 255;
            this.toolLeft.Text = "ToolStrip1";
            // 
            // ToolStripLabel2
            // 
            this.ToolStripLabel2.Name = "ToolStripLabel2";
            this.ToolStripLabel2.Size = new System.Drawing.Size(10, 25);
            this.ToolStripLabel2.Text = " ";
            // 
            // tsbFeesCalculation
            // 
            this.tsbFeesCalculation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFeesCalculation.Image = global::Accounting.Properties.Resources.calculator;
            this.tsbFeesCalculation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFeesCalculation.Name = "tsbFeesCalculation";
            this.tsbFeesCalculation.Size = new System.Drawing.Size(23, 25);
            this.tsbFeesCalculation.Text = "Υπολογισμός Fees";
            this.tsbFeesCalculation.Click += new System.EventHandler(this.tsbFeesCalculation_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // ToolStripSeparator3
            // 
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new System.Drawing.Size(6, 28);
            // 
            // tsbView
            // 
            this.tsbView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbView.Image = global::Accounting.Properties.Resources.eye;
            this.tsbView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbView.Name = "tsbView";
            this.tsbView.Size = new System.Drawing.Size(23, 25);
            this.tsbView.Text = "Προβολή της εντολής";
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // tsbExcel
            // 
            this.tsbExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExcel.Image = global::Accounting.Properties.Resources.excel;
            this.tsbExcel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExcel.Name = "tsbExcel";
            this.tsbExcel.Size = new System.Drawing.Size(23, 25);
            this.tsbExcel.Text = "Εξαγωγή λίστας σε EXCEL-αρχείο";
            this.tsbExcel.Click += new System.EventHandler(this.tsbExcel_Click);
            // 
            // ToolStripSeparator6
            // 
            this.ToolStripSeparator6.Name = "ToolStripSeparator6";
            this.ToolStripSeparator6.Size = new System.Drawing.Size(6, 28);
            // 
            // tsbHistory
            // 
            this.tsbHistory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHistory.Image = global::Accounting.Properties.Resources.history;
            this.tsbHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHistory.Name = "tsbHistory";
            this.tsbHistory.Size = new System.Drawing.Size(23, 25);
            this.tsbHistory.Text = "Ιστορία  εγγραφής";
            // 
            // ToolStripSeparator4
            // 
            this.ToolStripSeparator4.Name = "ToolStripSeparator4";
            this.ToolStripSeparator4.Size = new System.Drawing.Size(6, 28);
            // 
            // tsbSettings
            // 
            this.tsbSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSettings.Image = global::Accounting.Properties.Resources.settings;
            this.tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSettings.Name = "tsbSettings";
            this.tsbSettings.Size = new System.Drawing.Size(23, 25);
            this.tsbSettings.Text = "Επιλογές";
            // 
            // ToolStripSeparator11
            // 
            this.ToolStripSeparator11.Name = "ToolStripSeparator11";
            this.ToolStripSeparator11.Size = new System.Drawing.Size(6, 28);
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
            // panTools
            // 
            this.panTools.Controls.Add(this.cmbFilter);
            this.panTools.Controls.Add(this.cmbServiceProviders);
            this.panTools.Controls.Add(this.Label2);
            this.panTools.Controls.Add(this.txtCode);
            this.panTools.Controls.Add(this.Label12);
            this.panTools.Controls.Add(this.toolLeft);
            this.panTools.Location = new System.Drawing.Point(4, 66);
            this.panTools.Name = "panTools";
            this.panTools.Size = new System.Drawing.Size(1294, 34);
            this.panTools.TabIndex = 318;
            // 
            // cmbFilter
            // 
            this.cmbFilter.AutoCompleteCustomSource.AddRange(new string[] {
            "Όλες εγγραφές",
            "Τιμολογημένες εγγραφές",
            "Ατιμολόγητες εγγραφές"});
            this.cmbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilter.FormattingEnabled = true;
            this.cmbFilter.Items.AddRange(new object[] {
            "Όλες εγγραφές",
            "Τιμολογημένες εγγραφές",
            "Ατιμολόγητες εγγραφές"});
            this.cmbFilter.Location = new System.Drawing.Point(256, 8);
            this.cmbFilter.Name = "cmbFilter";
            this.cmbFilter.Size = new System.Drawing.Size(180, 21);
            this.cmbFilter.TabIndex = 258;
            this.cmbFilter.SelectedIndexChanged += new System.EventHandler(this.cmbFilter_SelectedIndexChanged);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(6, 19);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(155, 13);
            this.Label4.TabIndex = 1013;
            this.Label4.Text = " Ημερομηνίες εκτέλεσεις από";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(1187, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(91, 27);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Αναζήτηση";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panCritiries
            // 
            this.panCritiries.BackColor = System.Drawing.Color.LightGray;
            this.panCritiries.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panCritiries.Controls.Add(this.Label4);
            this.panCritiries.Controls.Add(this.btnSearch);
            this.panCritiries.Location = new System.Drawing.Point(4, 6);
            this.panCritiries.Name = "panCritiries";
            this.panCritiries.Size = new System.Drawing.Size(1294, 55);
            this.panCritiries.TabIndex = 319;
            // 
            // mnuNeaXreosi
            // 
            this.mnuNeaXreosi.Name = "mnuNeaXreosi";
            this.mnuNeaXreosi.Size = new System.Drawing.Size(226, 22);
            this.mnuNeaXreosi.Text = "Νέα χρέωση";
            // 
            // mnuAkyrotiko
            // 
            this.mnuAkyrotiko.Name = "mnuAkyrotiko";
            this.mnuAkyrotiko.Size = new System.Drawing.Size(226, 22);
            this.mnuAkyrotiko.Text = "Ειδικό ακυρωτικό σημείωμα";
            // 
            // mnuPrintInvoice
            // 
            this.mnuPrintInvoice.Name = "mnuPrintInvoice";
            this.mnuPrintInvoice.Size = new System.Drawing.Size(226, 22);
            this.mnuPrintInvoice.Text = "Εκτύπωση παραστατικού";
            // 
            // fgList
            // 
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(5, 106);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 2;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Rows.Fixed = 2;
            this.fgList.Size = new System.Drawing.Size(1296, 558);
            this.fgList.SubtotalPosition = C1.Win.C1FlexGrid.SubtotalPositionEnum.BelowData;
            this.fgList.TabIndex = 316;
            // 
            // mnuContext
            // 
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContractData,
            this.mnuClientData,
            this.ToolStripMenuItem1,
            this.mnuPistotiko,
            this.mnuAkyrotiko,
            this.mnuNeaXreosi,
            this.mnuPrintInvoice});
            this.mnuContext.Name = "mnuContext";
            this.mnuContext.Size = new System.Drawing.Size(227, 142);
            // 
            // mnuContractData
            // 
            this.mnuContractData.Name = "mnuContractData";
            this.mnuContractData.Size = new System.Drawing.Size(226, 22);
            this.mnuContractData.Text = "Στοιχεία Σύμβασης";
            this.mnuContractData.Click += new System.EventHandler(this.mnuContractData_Click);
            // 
            // mnuClientData
            // 
            this.mnuClientData.Name = "mnuClientData";
            this.mnuClientData.Size = new System.Drawing.Size(226, 22);
            this.mnuClientData.Text = "Στοιχεία τού πελάτη";
            this.mnuClientData.Click += new System.EventHandler(this.mnuClientData_Click);
            // 
            // ToolStripMenuItem1
            // 
            this.ToolStripMenuItem1.Name = "ToolStripMenuItem1";
            this.ToolStripMenuItem1.Size = new System.Drawing.Size(223, 6);
            // 
            // mnuPistotiko
            // 
            this.mnuPistotiko.Name = "mnuPistotiko";
            this.mnuPistotiko.Size = new System.Drawing.Size(226, 22);
            this.mnuPistotiko.Text = "Πιστωτικό παραστατικό";
            // 
            // chkPrint
            // 
            this.chkPrint.AutoSize = true;
            this.chkPrint.Location = new System.Drawing.Point(10, 117);
            this.chkPrint.Name = "chkPrint";
            this.chkPrint.Size = new System.Drawing.Size(15, 14);
            this.chkPrint.TabIndex = 317;
            this.chkPrint.UseVisualStyleBackColor = true;
            this.chkPrint.CheckedChanged += new System.EventHandler(this.chkPrint_CheckedChanged);
            // 
            // imgFiles
            // 
            this.imgFiles.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgFiles.ImageStream")));
            this.imgFiles.TransparentColor = System.Drawing.Color.Transparent;
            this.imgFiles.Images.SetKeyName(0, "limit.jpg");
            this.imgFiles.Images.SetKeyName(1, "Pdf-icon.png");
            // 
            // ucExec
            // 
            this.ucExec.BackColor = System.Drawing.Color.LightGray;
            this.ucExec.DateFrom = new System.DateTime(2020, 8, 14, 11, 34, 42, 853);
            this.ucExec.DateTo = new System.DateTime(2020, 8, 14, 11, 34, 42, 853);
            this.ucExec.Location = new System.Drawing.Point(170, 24);
            this.ucExec.Name = "ucExec";
            this.ucExec.Size = new System.Drawing.Size(210, 20);
            this.ucExec.TabIndex = 1014;
            // 
            // frmAcc_InvoicesFX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.BurlyWood;
            this.ClientSize = new System.Drawing.Size(1304, 676);
            this.Controls.Add(this.chkPrint);
            this.Controls.Add(this.ucExec);
            this.Controls.Add(this.panTools);
            this.Controls.Add(this.panCritiries);
            this.Controls.Add(this.fgList);
            this.Name = "frmAcc_InvoicesFX";
            this.Text = "Τιμολόγηση εντολών μετατροπής νομίσματος";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmAcc_InvoicesFX_Load);
            this.toolLeft.ResumeLayout(false);
            this.toolLeft.PerformLayout();
            this.panTools.ResumeLayout(false);
            this.panTools.PerformLayout();
            this.panCritiries.ResumeLayout(false);
            this.panCritiries.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.mnuContext.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStripButton tsbPrint;
        internal System.Windows.Forms.ComboBox cmbServiceProviders;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtCode;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.ToolStrip toolLeft;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel2;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
        internal System.Windows.Forms.ToolStripButton tsbView;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripButton tsbExcel;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator6;
        internal System.Windows.Forms.ToolStripButton tsbHistory;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator4;
        internal System.Windows.Forms.ToolStripButton tsbSettings;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator11;
        internal System.Windows.Forms.ToolStripButton tsbHelp;
        internal System.Windows.Forms.Panel panTools;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Button btnSearch;
        internal System.Windows.Forms.Panel panCritiries;
        internal System.Windows.Forms.ToolStripMenuItem mnuNeaXreosi;
        internal System.Windows.Forms.ToolStripMenuItem mnuAkyrotiko;
        internal System.Windows.Forms.ToolStripMenuItem mnuPrintInvoice;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
        internal System.Windows.Forms.ContextMenuStrip mnuContext;
        internal System.Windows.Forms.ToolStripMenuItem mnuContractData;
        internal System.Windows.Forms.ToolStripMenuItem mnuClientData;
        internal System.Windows.Forms.ToolStripSeparator ToolStripMenuItem1;
        internal System.Windows.Forms.ToolStripMenuItem mnuPistotiko;
        internal System.Windows.Forms.CheckBox chkPrint;
        internal System.Windows.Forms.ComboBox cmbFilter;
        private System.Windows.Forms.ImageList imgFiles;
        private Core.ucDoubleCalendar ucExec;
        internal System.Windows.Forms.ToolStripButton tsbFeesCalculation;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}