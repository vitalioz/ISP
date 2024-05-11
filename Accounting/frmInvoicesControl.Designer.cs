
namespace Accounting
{
    partial class frmInvoicesControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInvoicesControl));
            this.panCritiries = new System.Windows.Forms.Panel();
            this.chkDateIssued = new System.Windows.Forms.CheckBox();
            this.chkDateIns = new System.Windows.Forms.CheckBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTypes = new System.Windows.Forms.ComboBox();
            this.Label18 = new System.Windows.Forms.Label();
            this.cmbServiceProviders = new System.Windows.Forms.ComboBox();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.toolLeft = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsbMyData = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExcel = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.imgFile = new System.Windows.Forms.ImageList(this.components);
            this.ucDC = new Core.ucDoubleCalendar();
            this.ucDC2 = new Core.ucDoubleCalendar();
            this.panCritiries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.toolLeft.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panCritiries
            // 
            this.panCritiries.BackColor = System.Drawing.Color.Gainsboro;
            this.panCritiries.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panCritiries.Controls.Add(this.chkDateIssued);
            this.panCritiries.Controls.Add(this.chkDateIns);
            this.panCritiries.Controls.Add(this.btnSearch);
            this.panCritiries.Location = new System.Drawing.Point(6, 6);
            this.panCritiries.Name = "panCritiries";
            this.panCritiries.Size = new System.Drawing.Size(1211, 64);
            this.panCritiries.TabIndex = 1129;
            // 
            // chkDateIssued
            // 
            this.chkDateIssued.AutoSize = true;
            this.chkDateIssued.Location = new System.Drawing.Point(5, 36);
            this.chkDateIssued.Name = "chkDateIssued";
            this.chkDateIssued.Size = new System.Drawing.Size(152, 17);
            this.chkDateIssued.TabIndex = 1110;
            this.chkDateIssued.Text = "Ημερ/νίες  στο τιμολόγιο";
            this.chkDateIssued.UseVisualStyleBackColor = true;
            // 
            // chkDateIns
            // 
            this.chkDateIns.AutoSize = true;
            this.chkDateIns.Checked = true;
            this.chkDateIns.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDateIns.Location = new System.Drawing.Point(5, 14);
            this.chkDateIns.Name = "chkDateIns";
            this.chkDateIns.Size = new System.Drawing.Size(125, 17);
            this.chkDateIns.TabIndex = 1109;
            this.chkDateIns.Text = "Ημερ/νίες έκδοσεις";
            this.chkDateIns.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(1102, 26);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(93, 27);
            this.btnSearch.TabIndex = 16;
            this.btnSearch.Text = "Αναζήτηση";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(673, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 1255;
            this.label1.Text = "Είδος εσόδων";
            // 
            // cmbTypes
            // 
            this.cmbTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypes.FormattingEnabled = true;
            this.cmbTypes.Items.AddRange(new object[] {
            "Όλα",
            "Αμοιβή Λήψης & Διαβίβασης Εντολής",
            "Αμοιβή Διαβίβασης Εντολής Μετατροπής Νομίσματος",
            "Αμοιβή Επενδυτικών Συμβουλών και Αμοιβή Διαχείρισης",
            "Αμοιβή Υποστήριξης Χαρτοφυλακίου",
            "Αμοιβή Υπεραπόδοσης",
            "Αμοιβή θεματοφυλακής"});
            this.cmbTypes.Location = new System.Drawing.Point(755, 9);
            this.cmbTypes.Name = "cmbTypes";
            this.cmbTypes.Size = new System.Drawing.Size(300, 21);
            this.cmbTypes.TabIndex = 6;
            this.cmbTypes.SelectedIndexChanged += new System.EventHandler(this.cmbTypes_SelectedIndexChanged);
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Location = new System.Drawing.Point(248, 12);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(51, 13);
            this.Label18.TabIndex = 1253;
            this.Label18.Text = "Πάροχος";
            // 
            // cmbServiceProviders
            // 
            this.cmbServiceProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServiceProviders.FormattingEnabled = true;
            this.cmbServiceProviders.Location = new System.Drawing.Point(305, 9);
            this.cmbServiceProviders.Name = "cmbServiceProviders";
            this.cmbServiceProviders.Size = new System.Drawing.Size(300, 21);
            this.cmbServiceProviders.TabIndex = 4;
            this.cmbServiceProviders.SelectedValueChanged += new System.EventHandler(this.cmbServiceProviders_SelectedValueChanged);
            // 
            // fgList
            // 
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(6, 114);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(1211, 602);
            this.fgList.SubtotalPosition = C1.Win.C1FlexGrid.SubtotalPositionEnum.BelowData;
            this.fgList.TabIndex = 0;
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
            this.tsbMyData,
            this.ToolStripSeparator3,
            this.tsbExcel,
            this.ToolStripSeparator6,
            this.tsbCancel,
            this.toolStripSeparator1,
            this.tsbHelp});
            this.toolLeft.Location = new System.Drawing.Point(4, 4);
            this.toolLeft.Name = "toolLeft";
            this.toolLeft.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolLeft.Size = new System.Drawing.Size(143, 28);
            this.toolLeft.TabIndex = 1132;
            this.toolLeft.Text = "ToolStrip1";
            // 
            // ToolStripLabel2
            // 
            this.ToolStripLabel2.Name = "ToolStripLabel2";
            this.ToolStripLabel2.Size = new System.Drawing.Size(10, 25);
            this.ToolStripLabel2.Text = " ";
            // 
            // tsbMyData
            // 
            this.tsbMyData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMyData.Image = global::Accounting.Properties.Resources.transfer;
            this.tsbMyData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMyData.Name = "tsbMyData";
            this.tsbMyData.Size = new System.Drawing.Size(23, 25);
            this.tsbMyData.Text = "Εξαγωγή λίστας  στο myData";
            this.tsbMyData.Click += new System.EventHandler(this.tsbExport_Click);
            // 
            // ToolStripSeparator3
            // 
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new System.Drawing.Size(6, 28);
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
            // tsbCancel
            // 
            this.tsbCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCancel.Image = global::Accounting.Properties.Resources.cancel1;
            this.tsbCancel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCancel.Name = "tsbCancel";
            this.tsbCancel.Size = new System.Drawing.Size(23, 25);
            this.tsbCancel.Text = "Ακύρωση παραστατικού";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // tsbHelp
            // 
            this.tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbHelp.Image")));
            this.tsbHelp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(23, 25);
            this.tsbHelp.Text = "Βοήθεια";
            this.tsbHelp.Click += new System.EventHandler(this.tsbHelp_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolLeft);
            this.panel1.Controls.Add(this.cmbServiceProviders);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Label18);
            this.panel1.Controls.Add(this.cmbTypes);
            this.panel1.Location = new System.Drawing.Point(6, 73);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1211, 35);
            this.panel1.TabIndex = 1256;
            // 
            // imgFile
            // 
            this.imgFile.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgFile.ImageStream")));
            this.imgFile.TransparentColor = System.Drawing.Color.Transparent;
            this.imgFile.Images.SetKeyName(0, "limit.jpg");
            this.imgFile.Images.SetKeyName(1, "Pdf-icon.png");
            // 
            // ucDC
            // 
            this.ucDC.BackColor = System.Drawing.Color.Gainsboro;
            this.ucDC.DateFrom = new System.DateTime(2020, 8, 1, 16, 39, 27, 913);
            this.ucDC.DateTo = new System.DateTime(2020, 8, 1, 16, 39, 27, 913);
            this.ucDC.Location = new System.Drawing.Point(166, 16);
            this.ucDC.Name = "ucDC";
            this.ucDC.Size = new System.Drawing.Size(233, 21);
            this.ucDC.TabIndex = 2;
            // 
            // ucDC2
            // 
            this.ucDC2.BackColor = System.Drawing.Color.Gainsboro;
            this.ucDC2.DateFrom = new System.DateTime(2020, 8, 1, 16, 39, 27, 913);
            this.ucDC2.DateTo = new System.DateTime(2020, 8, 1, 16, 39, 27, 913);
            this.ucDC2.Location = new System.Drawing.Point(166, 41);
            this.ucDC2.Name = "ucDC2";
            this.ucDC2.Size = new System.Drawing.Size(233, 21);
            this.ucDC2.TabIndex = 3;
            // 
            // frmInvoicesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Wheat;
            this.ClientSize = new System.Drawing.Size(1228, 723);
            this.Controls.Add(this.ucDC);
            this.Controls.Add(this.ucDC2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.fgList);
            this.Controls.Add(this.panCritiries);
            this.Name = "frmInvoicesControl";
            this.Text = "Έλεγχος Παραστατικών";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmInvoicesControl_Load);
            this.panCritiries.ResumeLayout(false);
            this.panCritiries.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.toolLeft.ResumeLayout(false);
            this.toolLeft.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panCritiries;
        private Core.ucDoubleCalendar ucDC;
        internal System.Windows.Forms.Button btnSearch;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
        internal System.Windows.Forms.ToolStrip toolLeft;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel2;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
        internal System.Windows.Forms.ToolStripButton tsbExcel;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsbMyData;
        internal System.Windows.Forms.ToolStripButton tsbHelp;
        internal System.Windows.Forms.ToolStripButton tsbCancel;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ComboBox cmbTypes;
        internal System.Windows.Forms.Label Label18;
        internal System.Windows.Forms.ComboBox cmbServiceProviders;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.ImageList imgFile;
        private Core.ucDoubleCalendar ucDC2;
        internal System.Windows.Forms.CheckBox chkDateIssued;
        internal System.Windows.Forms.CheckBox chkDateIns;
    }
}