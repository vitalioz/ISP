namespace Accounting
{
    partial class frmAcc_ReportsMF
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAcc_ReportsMF));
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ClientData = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbYearFrom = new System.Windows.Forms.ComboBox();
            this.panCritiries = new System.Windows.Forms.Panel();
            this.Label7 = new System.Windows.Forms.Label();
            this.cmbFinanceServices = new System.Windows.Forms.ComboBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.cmbYearTo = new System.Windows.Forms.ComboBox();
            this.cmbQuartTo = new System.Windows.Forms.ComboBox();
            this.cmbQuartFrom = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.cmbChief = new System.Windows.Forms.ComboBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbServiceProviders = new System.Windows.Forms.ComboBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPrint = new System.Windows.Forms.ToolStripButton();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.toolLeft = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tscbFilter = new System.Windows.Forms.ToolStripComboBox();
            this.ToolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExcel = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuContext.SuspendLayout();
            this.panCritiries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.toolLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuContext
            // 
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClientData});
            this.mnuContext.Name = "ContextMenuStrip1";
            this.mnuContext.Size = new System.Drawing.Size(183, 26);
            // 
            // ClientData
            // 
            this.ClientData.Name = "ClientData";
            this.ClientData.Size = new System.Drawing.Size(182, 22);
            this.ClientData.Text = "Στοιχεία τού πελάτη";
            // 
            // cmbYearFrom
            // 
            this.cmbYearFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYearFrom.FormattingEnabled = true;
            this.cmbYearFrom.Location = new System.Drawing.Point(129, 10);
            this.cmbYearFrom.Name = "cmbYearFrom";
            this.cmbYearFrom.Size = new System.Drawing.Size(64, 21);
            this.cmbYearFrom.TabIndex = 4;
            // 
            // panCritiries
            // 
            this.panCritiries.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panCritiries.Controls.Add(this.cmbYearFrom);
            this.panCritiries.Controls.Add(this.Label7);
            this.panCritiries.Controls.Add(this.cmbFinanceServices);
            this.panCritiries.Controls.Add(this.Label4);
            this.panCritiries.Controls.Add(this.cmbYearTo);
            this.panCritiries.Controls.Add(this.cmbQuartTo);
            this.panCritiries.Controls.Add(this.cmbQuartFrom);
            this.panCritiries.Controls.Add(this.Label2);
            this.panCritiries.Controls.Add(this.cmbChief);
            this.panCritiries.Controls.Add(this.Label6);
            this.panCritiries.Controls.Add(this.Label1);
            this.panCritiries.Controls.Add(this.btnSearch);
            this.panCritiries.Controls.Add(this.cmbServiceProviders);
            this.panCritiries.Controls.Add(this.Label11);
            this.panCritiries.Location = new System.Drawing.Point(6, 6);
            this.panCritiries.Name = "panCritiries";
            this.panCritiries.Size = new System.Drawing.Size(1069, 85);
            this.panCritiries.TabIndex = 245;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(492, 61);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(56, 13);
            this.Label7.TabIndex = 292;
            this.Label7.Text = "Υπηρεσία";
            // 
            // cmbFinanceServices
            // 
            this.cmbFinanceServices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFinanceServices.FormattingEnabled = true;
            this.cmbFinanceServices.Location = new System.Drawing.Point(573, 58);
            this.cmbFinanceServices.Name = "cmbFinanceServices";
            this.cmbFinanceServices.Size = new System.Drawing.Size(276, 21);
            this.cmbFinanceServices.TabIndex = 16;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(204, 13);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(27, 13);
            this.Label4.TabIndex = 235;
            this.Label4.Text = "εώς";
            // 
            // cmbYearTo
            // 
            this.cmbYearTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYearTo.FormattingEnabled = true;
            this.cmbYearTo.Location = new System.Drawing.Point(294, 10);
            this.cmbYearTo.Name = "cmbYearTo";
            this.cmbYearTo.Size = new System.Drawing.Size(64, 21);
            this.cmbYearTo.TabIndex = 8;
            // 
            // cmbQuartTo
            // 
            this.cmbQuartTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuartTo.FormattingEnabled = true;
            this.cmbQuartTo.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cmbQuartTo.Location = new System.Drawing.Point(237, 10);
            this.cmbQuartTo.Name = "cmbQuartTo";
            this.cmbQuartTo.Size = new System.Drawing.Size(54, 21);
            this.cmbQuartTo.TabIndex = 6;
            // 
            // cmbQuartFrom
            // 
            this.cmbQuartFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuartFrom.FormattingEnabled = true;
            this.cmbQuartFrom.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cmbQuartFrom.Location = new System.Drawing.Point(74, 10);
            this.cmbQuartFrom.Name = "cmbQuartFrom";
            this.cmbQuartFrom.Size = new System.Drawing.Size(52, 21);
            this.cmbQuartFrom.TabIndex = 2;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(4, 13);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(68, 13);
            this.Label2.TabIndex = 3;
            this.Label2.Text = "Τρίμηνο από";
            // 
            // cmbChief
            // 
            this.cmbChief.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChief.FormattingEnabled = true;
            this.cmbChief.Location = new System.Drawing.Point(573, 10);
            this.cmbChief.Name = "cmbChief";
            this.cmbChief.Size = new System.Drawing.Size(276, 21);
            this.cmbChief.TabIndex = 12;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(492, 13);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(63, 13);
            this.Label6.TabIndex = 224;
            this.Label6.Text = "Υπεύθυνος";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(492, 37);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(50, 13);
            this.Label1.TabIndex = 213;
            this.Label1.Text = "Πελάτες";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(977, 22);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 18;
            this.btnSearch.Text = "Αναζήτηση";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // cmbServiceProviders
            // 
            this.cmbServiceProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServiceProviders.FormattingEnabled = true;
            this.cmbServiceProviders.Location = new System.Drawing.Point(75, 41);
            this.cmbServiceProviders.Name = "cmbServiceProviders";
            this.cmbServiceProviders.Size = new System.Drawing.Size(226, 21);
            this.cmbServiceProviders.TabIndex = 10;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(6, 46);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(51, 13);
            this.Label11.TabIndex = 2;
            this.Label11.Text = "Πάροχος";
            // 
            // tsbHelp
            // 
            this.tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbHelp.Image")));
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(23, 22);
            this.tsbHelp.Text = "Βοήθεια";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbPrint
            // 
            this.tsbPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPrint.Image = ((System.Drawing.Image)(resources.GetObject("tsbPrint.Image")));
            this.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Size = new System.Drawing.Size(23, 22);
            this.tsbPrint.Text = "Εκτύπωση λίστας";
            // 
            // fgList
            // 
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(5, 128);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(1070, 497);
            this.fgList.SubtotalPosition = C1.Win.C1FlexGrid.SubtotalPositionEnum.BelowData;
            this.fgList.TabIndex = 243;
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
            this.ToolStripLabel1,
            this.ToolStripSeparator3,
            this.tscbFilter,
            this.ToolStripLabel2,
            this.ToolStripSeparator2,
            this.tsbExcel,
            this.ToolStripSeparator1,
            this.tsbPrint,
            this.toolStripSeparator,
            this.tsbHelp});
            this.toolLeft.Location = new System.Drawing.Point(9, 96);
            this.toolLeft.Name = "toolLeft";
            this.toolLeft.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolLeft.Size = new System.Drawing.Size(299, 25);
            this.toolLeft.TabIndex = 244;
            this.toolLeft.Text = "ToolStrip1";
            // 
            // ToolStripLabel1
            // 
            this.ToolStripLabel1.Name = "ToolStripLabel1";
            this.ToolStripLabel1.Size = new System.Drawing.Size(0, 22);
            // 
            // ToolStripSeparator3
            // 
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tscbFilter
            // 
            this.tscbFilter.AutoCompleteCustomSource.AddRange(new string[] {
            "Όλες εγγραφές",
            "Τιμολογημένες εγγραφές",
            "Ατιμολόγητες εγγραφές"});
            this.tscbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbFilter.Items.AddRange(new object[] {
            "Όλες εγγραφές",
            "Τιμολογημένες εγγραφές",
            "Ατιμολόγητες εγγραφές"});
            this.tscbFilter.Name = "tscbFilter";
            this.tscbFilter.Size = new System.Drawing.Size(180, 25);
            // 
            // ToolStripLabel2
            // 
            this.ToolStripLabel2.Name = "ToolStripLabel2";
            this.ToolStripLabel2.Size = new System.Drawing.Size(10, 22);
            this.ToolStripLabel2.Text = " ";
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbExcel
            // 
            this.tsbExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExcel.Name = "tsbExcel";
            this.tsbExcel.Size = new System.Drawing.Size(23, 22);
            this.tsbExcel.Text = "Απολοιφή κριτηρίων";
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // frmAcc_ReportsMF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(1080, 630);
            this.Controls.Add(this.panCritiries);
            this.Controls.Add(this.fgList);
            this.Controls.Add(this.toolLeft);
            this.Name = "frmAcc_ReportsMF";
            this.Text = "frmAcc_ReportsMF";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.mnuContext.ResumeLayout(false);
            this.panCritiries.ResumeLayout(false);
            this.panCritiries.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.toolLeft.ResumeLayout(false);
            this.toolLeft.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ContextMenuStrip mnuContext;
        internal System.Windows.Forms.ToolStripMenuItem ClientData;
        internal System.Windows.Forms.ComboBox cmbYearFrom;
        internal System.Windows.Forms.Panel panCritiries;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.ComboBox cmbFinanceServices;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.ComboBox cmbYearTo;
        internal System.Windows.Forms.ComboBox cmbQuartTo;
        internal System.Windows.Forms.ComboBox cmbQuartFrom;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.ComboBox cmbChief;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button btnSearch;
        internal System.Windows.Forms.ComboBox cmbServiceProviders;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.ToolStripButton tsbHelp;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        internal System.Windows.Forms.ToolStripButton tsbPrint;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
        internal System.Windows.Forms.ToolStrip toolLeft;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel1;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
        internal System.Windows.Forms.ToolStripComboBox tscbFilter;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel2;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        internal System.Windows.Forms.ToolStripButton tsbExcel;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
    }
}