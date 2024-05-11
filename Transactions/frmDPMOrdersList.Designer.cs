namespace Transactions
{
    partial class frmDPMOrdersList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDPMOrdersList));
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.toolProtaseis = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel10 = new System.Windows.Forms.ToolStripLabel();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEdit = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCancel = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSend = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.Label2 = new System.Windows.Forms.Label();
            this.panCritiries = new System.Windows.Forms.Panel();
            this.dToday = new System.Windows.Forms.DateTimePicker();
            this.cmbDiaxiristes = new System.Windows.Forms.ComboBox();
            this.Label25 = new System.Windows.Forms.Label();
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuClientData = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContractData = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProductData = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCopyISIN = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowFile = new System.Windows.Forms.ToolStripMenuItem();
            this.XXXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panOrderType = new System.Windows.Forms.Panel();
            this.lnkProduct = new System.Windows.Forms.LinkLabel();
            this.lnkCustomer = new System.Windows.Forms.LinkLabel();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkList = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ucCS = new Core.ucContractsSearch();
            this.ucPS = new Core.ucProductsSearch();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ucDates = new Core.ucDoubleCalendar();
            this.toolSymvoules = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsbRefreshProposals = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPlayRecievedFile = new System.Windows.Forms.ToolStripButton();
            this.Label6 = new System.Windows.Forms.Label();
            this.lblISIN = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.lblShareTitle = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.chkCancel = new System.Windows.Forms.CheckBox();
            this.chkYes = new System.Windows.Forms.CheckBox();
            this.chkNot = new System.Windows.Forms.CheckBox();
            this.chkThink = new System.Windows.Forms.CheckBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.chkNew = new System.Windows.Forms.CheckBox();
            this.fgRecs = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.picEmptyShare = new System.Windows.Forms.PictureBox();
            this.picEmptyClient = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.toolProtaseis.SuspendLayout();
            this.panCritiries.SuspendLayout();
            this.mnuContext.SuspendLayout();
            this.panOrderType.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.toolSymvoules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgRecs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmptyShare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmptyClient)).BeginInit();
            this.SuspendLayout();
            // 
            // fgList
            // 
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(6, 84);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 2;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Rows.Fixed = 2;
            this.fgList.Size = new System.Drawing.Size(1245, 609);
            this.fgList.TabIndex = 0;
            // 
            // toolProtaseis
            // 
            this.toolProtaseis.AutoSize = false;
            this.toolProtaseis.BackColor = System.Drawing.Color.Gainsboro;
            this.toolProtaseis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolProtaseis.Dock = System.Windows.Forms.DockStyle.None;
            this.toolProtaseis.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolProtaseis.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolProtaseis.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripLabel10,
            this.tsbAdd,
            this.ToolStripSeparator22,
            this.tsbEdit,
            this.ToolStripSeparator23,
            this.tsbCancel,
            this.ToolStripSeparator1,
            this.tsbSend,
            this.toolStripSeparator7,
            this.tsbRefresh});
            this.toolProtaseis.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolProtaseis.Location = new System.Drawing.Point(6, 56);
            this.toolProtaseis.Name = "toolProtaseis";
            this.toolProtaseis.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolProtaseis.Size = new System.Drawing.Size(163, 25);
            this.toolProtaseis.TabIndex = 1151;
            this.toolProtaseis.Text = "ToolStrip1";
            // 
            // ToolStripLabel10
            // 
            this.ToolStripLabel10.Name = "ToolStripLabel10";
            this.ToolStripLabel10.Size = new System.Drawing.Size(10, 22);
            this.ToolStripLabel10.Text = " ";
            // 
            // tsbAdd
            // 
            this.tsbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAdd.Image = global::Transactions.Properties.Resources.plus;
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(23, 22);
            this.tsbAdd.Text = "Προσθήκη";
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // ToolStripSeparator22
            // 
            this.ToolStripSeparator22.Name = "ToolStripSeparator22";
            this.ToolStripSeparator22.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbEdit
            // 
            this.tsbEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEdit.Image = global::Transactions.Properties.Resources.edit;
            this.tsbEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEdit.Name = "tsbEdit";
            this.tsbEdit.Size = new System.Drawing.Size(23, 22);
            this.tsbEdit.Text = "Διόρθωση";
            this.tsbEdit.Click += new System.EventHandler(this.tsbEdit_Click);
            // 
            // ToolStripSeparator23
            // 
            this.ToolStripSeparator23.Name = "ToolStripSeparator23";
            this.ToolStripSeparator23.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbCancel
            // 
            this.tsbCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCancel.Image = global::Transactions.Properties.Resources.cancel1;
            this.tsbCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCancel.Name = "tsbCancel";
            this.tsbCancel.Size = new System.Drawing.Size(23, 22);
            this.tsbCancel.Text = "Ακύρωση";
            this.tsbCancel.Click += new System.EventHandler(this.tsbCancel_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbSend
            // 
            this.tsbSend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSend.Image = global::Transactions.Properties.Resources.emailicon;
            this.tsbSend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSend.Name = "tsbSend";
            this.tsbSend.Size = new System.Drawing.Size(23, 22);
            this.tsbSend.Text = "Αποστολή";
            this.tsbSend.Click += new System.EventHandler(this.tsbSend_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefresh.Image")));
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(23, 22);
            this.tsbRefresh.Text = "Ανανέωση";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(18, 19);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(129, 13);
            this.Label2.TabIndex = 1153;
            this.Label2.Text = " Ημερ/νία καταχώρησης";
            // 
            // panCritiries
            // 
            this.panCritiries.BackColor = System.Drawing.Color.Gainsboro;
            this.panCritiries.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panCritiries.Controls.Add(this.dToday);
            this.panCritiries.Controls.Add(this.cmbDiaxiristes);
            this.panCritiries.Controls.Add(this.Label25);
            this.panCritiries.Controls.Add(this.Label2);
            this.panCritiries.Location = new System.Drawing.Point(6, 4);
            this.panCritiries.Name = "panCritiries";
            this.panCritiries.Size = new System.Drawing.Size(1245, 48);
            this.panCritiries.TabIndex = 2085;
            // 
            // dToday
            // 
            this.dToday.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dToday.Location = new System.Drawing.Point(153, 16);
            this.dToday.Name = "dToday";
            this.dToday.Size = new System.Drawing.Size(84, 20);
            this.dToday.TabIndex = 0;
            this.dToday.ValueChanged += new System.EventHandler(this.dToday_ValueChanged);
            // 
            // cmbDiaxiristes
            // 
            this.cmbDiaxiristes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDiaxiristes.FormattingEnabled = true;
            this.cmbDiaxiristes.Location = new System.Drawing.Point(437, 15);
            this.cmbDiaxiristes.Name = "cmbDiaxiristes";
            this.cmbDiaxiristes.Size = new System.Drawing.Size(274, 21);
            this.cmbDiaxiristes.TabIndex = 2;
            this.cmbDiaxiristes.SelectedValueChanged += new System.EventHandler(this.cmbDiaxiristes_SelectedValueChanged);
            // 
            // Label25
            // 
            this.Label25.AutoSize = true;
            this.Label25.Location = new System.Drawing.Point(361, 19);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(73, 13);
            this.Label25.TabIndex = 1103;
            this.Label25.Text = "Διαχειριστής";
            // 
            // mnuContext
            // 
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuClientData,
            this.mnuContractData,
            this.mnuProductData,
            this.mnuCopyISIN,
            this.mnuNewCommand,
            this.mnuShowFile,
            this.XXXToolStripMenuItem});
            this.mnuContext.Name = "ContextMenuStrip1";
            this.mnuContext.Size = new System.Drawing.Size(183, 158);
            // 
            // mnuClientData
            // 
            this.mnuClientData.Name = "mnuClientData";
            this.mnuClientData.Size = new System.Drawing.Size(182, 22);
            this.mnuClientData.Text = "Στοιχεία τού πελάτη";
            // 
            // mnuContractData
            // 
            this.mnuContractData.Name = "mnuContractData";
            this.mnuContractData.Size = new System.Drawing.Size(182, 22);
            this.mnuContractData.Text = "Στοιχεία Σύμβασης";
            // 
            // mnuProductData
            // 
            this.mnuProductData.Name = "mnuProductData";
            this.mnuProductData.Size = new System.Drawing.Size(182, 22);
            this.mnuProductData.Text = "Στοιχεία Προϊόντος";
            // 
            // mnuCopyISIN
            // 
            this.mnuCopyISIN.Name = "mnuCopyISIN";
            this.mnuCopyISIN.Size = new System.Drawing.Size(182, 22);
            this.mnuCopyISIN.Text = "Αντιγραφή ISIN";
            // 
            // mnuNewCommand
            // 
            this.mnuNewCommand.Name = "mnuNewCommand";
            this.mnuNewCommand.Size = new System.Drawing.Size(182, 22);
            this.mnuNewCommand.Text = "Νέα εντολή";
            // 
            // mnuShowFile
            // 
            this.mnuShowFile.Name = "mnuShowFile";
            this.mnuShowFile.Size = new System.Drawing.Size(182, 22);
            this.mnuShowFile.Text = "Προβολή αρχείου";
            // 
            // XXXToolStripMenuItem
            // 
            this.XXXToolStripMenuItem.Name = "XXXToolStripMenuItem";
            this.XXXToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.XXXToolStripMenuItem.Text = "XXX";
            // 
            // panOrderType
            // 
            this.panOrderType.BackColor = System.Drawing.Color.Gainsboro;
            this.panOrderType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panOrderType.Controls.Add(this.lnkProduct);
            this.panOrderType.Controls.Add(this.lnkCustomer);
            this.panOrderType.Location = new System.Drawing.Point(7, 85);
            this.panOrderType.Name = "panOrderType";
            this.panOrderType.Size = new System.Drawing.Size(162, 90);
            this.panOrderType.TabIndex = 2087;
            this.panOrderType.Visible = false;
            // 
            // lnkProduct
            // 
            this.lnkProduct.AutoSize = true;
            this.lnkProduct.Location = new System.Drawing.Point(18, 50);
            this.lnkProduct.Name = "lnkProduct";
            this.lnkProduct.Size = new System.Drawing.Size(103, 13);
            this.lnkProduct.TabIndex = 1;
            this.lnkProduct.TabStop = true;
            this.lnkProduct.Text = "Εντολή ανα Προϊόν";
            this.lnkProduct.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkProduct_LinkClicked);
            // 
            // lnkCustomer
            // 
            this.lnkCustomer.AutoSize = true;
            this.lnkCustomer.Location = new System.Drawing.Point(18, 21);
            this.lnkCustomer.Name = "lnkCustomer";
            this.lnkCustomer.Size = new System.Drawing.Size(105, 13);
            this.lnkCustomer.TabIndex = 0;
            this.lnkCustomer.TabStop = true;
            this.lnkCustomer.Text = "Εντολή ανα Πελάτη";
            this.lnkCustomer.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCustomer_LinkClicked);
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPage1);
            this.tabMain.Controls.Add(this.tabPage2);
            this.tabMain.Location = new System.Drawing.Point(4, 4);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1265, 725);
            this.tabMain.TabIndex = 2088;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Thistle;
            this.tabPage1.Controls.Add(this.panOrderType);
            this.tabPage1.Controls.Add(this.chkList);
            this.tabPage1.Controls.Add(this.fgList);
            this.tabPage1.Controls.Add(this.panCritiries);
            this.tabPage1.Controls.Add(this.toolProtaseis);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1257, 699);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Εντολές Διαχειριστή";
            // 
            // chkList
            // 
            this.chkList.AutoSize = true;
            this.chkList.Location = new System.Drawing.Point(12, 96);
            this.chkList.Name = "chkList";
            this.chkList.Size = new System.Drawing.Size(15, 14);
            this.chkList.TabIndex = 2088;
            this.chkList.UseVisualStyleBackColor = true;
            this.chkList.CheckedChanged += new System.EventHandler(this.chkList_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Thistle;
            this.tabPage2.Controls.Add(this.ucCS);
            this.tabPage2.Controls.Add(this.ucPS);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.ucDates);
            this.tabPage2.Controls.Add(this.toolSymvoules);
            this.tabPage2.Controls.Add(this.Label6);
            this.tabPage2.Controls.Add(this.lblISIN);
            this.tabPage2.Controls.Add(this.Label5);
            this.tabPage2.Controls.Add(this.lblShareTitle);
            this.tabPage2.Controls.Add(this.Label7);
            this.tabPage2.Controls.Add(this.Label1);
            this.tabPage2.Controls.Add(this.chkCancel);
            this.tabPage2.Controls.Add(this.chkYes);
            this.tabPage2.Controls.Add(this.chkNot);
            this.tabPage2.Controls.Add(this.chkThink);
            this.tabPage2.Controls.Add(this.Label4);
            this.tabPage2.Controls.Add(this.chkNew);
            this.tabPage2.Controls.Add(this.fgRecs);
            this.tabPage2.Controls.Add(this.picEmptyShare);
            this.tabPage2.Controls.Add(this.picEmptyClient);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1257, 699);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Εντολόχαρτο";
            // 
            // ucCS
            // 
            this.ucCS.BackColor = System.Drawing.Color.Transparent;
            this.ucCS.CodesList = null;
            this.ucCS.Filters = "Client_ID > 0 AND Status = 1";
            this.ucCS.ListType = 0;
            this.ucCS.Location = new System.Drawing.Point(75, 37);
            this.ucCS.Mode = 0;
            this.ucCS.Name = "ucCS";
            this.ucCS.ShowClientsList = true;
            this.ucCS.ShowHeight = 0;
            this.ucCS.ShowWidth = 0;
            this.ucCS.Size = new System.Drawing.Size(200, 20);
            this.ucCS.TabIndex = 806;
            // 
            // ucPS
            // 
            this.ucPS.BlockNonRecommended = false;
            this.ucPS.CodesList = null;
            this.ucPS.Filters = "Shares_ID > 0";
            this.ucPS.ListType = 0;
            this.ucPS.Location = new System.Drawing.Point(75, 64);
            this.ucPS.Mode = 0;
            this.ucPS.Name = "ucPS";
            this.ucPS.ProductsContract = null;
            this.ucPS.ShowCancelled = true;
            this.ucPS.ShowHeight = 0;
            this.ucPS.ShowNonAccord = true;
            this.ucPS.ShowProductsList = true;
            this.ucPS.ShowWidth = 0;
            this.ucPS.Size = new System.Drawing.Size(202, 20);
            this.ucPS.TabIndex = 807;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(562, 42);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 13);
            this.label10.TabIndex = 1109;
            this.label10.Text = "Portfolio";
            // 
            // label9
            // 
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label9.Location = new System.Drawing.Point(613, 38);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(200, 20);
            this.label9.TabIndex = 1108;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(297, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 1107;
            this.label3.Text = "Κωδικός";
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Location = new System.Drawing.Point(347, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(200, 20);
            this.label8.TabIndex = 1106;
            // 
            // ucDates
            // 
            this.ucDates.BackColor = System.Drawing.Color.Transparent;
            this.ucDates.DateFrom = new System.DateTime(2020, 8, 1, 16, 39, 27, 913);
            this.ucDates.DateTo = new System.DateTime(2020, 8, 1, 16, 39, 27, 913);
            this.ucDates.Location = new System.Drawing.Point(74, 9);
            this.ucDates.Name = "ucDates";
            this.ucDates.Size = new System.Drawing.Size(233, 21);
            this.ucDates.TabIndex = 1105;
            // 
            // toolSymvoules
            // 
            this.toolSymvoules.AutoSize = false;
            this.toolSymvoules.BackColor = System.Drawing.Color.Gainsboro;
            this.toolSymvoules.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolSymvoules.Dock = System.Windows.Forms.DockStyle.None;
            this.toolSymvoules.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolSymvoules.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolSymvoules.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripLabel1,
            this.tsbRefreshProposals,
            this.toolStripSeparator2,
            this.tsbPlayRecievedFile});
            this.toolSymvoules.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolSymvoules.Location = new System.Drawing.Point(12, 94);
            this.toolSymvoules.Name = "toolSymvoules";
            this.toolSymvoules.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolSymvoules.Size = new System.Drawing.Size(74, 25);
            this.toolSymvoules.TabIndex = 804;
            this.toolSymvoules.Text = "ToolStrip1";
            // 
            // ToolStripLabel1
            // 
            this.ToolStripLabel1.Name = "ToolStripLabel1";
            this.ToolStripLabel1.Size = new System.Drawing.Size(10, 22);
            this.ToolStripLabel1.Text = " ";
            // 
            // tsbRefreshProposals
            // 
            this.tsbRefreshProposals.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRefreshProposals.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefreshProposals.Image")));
            this.tsbRefreshProposals.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefreshProposals.Name = "tsbRefreshProposals";
            this.tsbRefreshProposals.Size = new System.Drawing.Size(23, 22);
            this.tsbRefreshProposals.Text = "Ανανέωση";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbPlayRecievedFile
            // 
            this.tsbPlayRecievedFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPlayRecievedFile.Image = global::Transactions.Properties.Resources.AudioHS;
            this.tsbPlayRecievedFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPlayRecievedFile.Name = "tsbPlayRecievedFile";
            this.tsbPlayRecievedFile.Size = new System.Drawing.Size(23, 22);
            this.tsbPlayRecievedFile.Text = "Αναπαραγωγή";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(757, 68);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(28, 13);
            this.Label6.TabIndex = 801;
            this.Label6.Text = "ISIN";
            // 
            // lblISIN
            // 
            this.lblISIN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblISIN.Location = new System.Drawing.Point(791, 66);
            this.lblISIN.Name = "lblISIN";
            this.lblISIN.Size = new System.Drawing.Size(147, 20);
            this.lblISIN.TabIndex = 800;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(305, 67);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(39, 13);
            this.Label5.TabIndex = 799;
            this.Label5.Text = "Τίτλος";
            // 
            // lblShareTitle
            // 
            this.lblShareTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblShareTitle.Location = new System.Drawing.Point(347, 64);
            this.lblShareTitle.Name = "lblShareTitle";
            this.lblShareTitle.Size = new System.Drawing.Size(404, 20);
            this.lblShareTitle.TabIndex = 798;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(9, 67);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(42, 13);
            this.Label7.TabIndex = 797;
            this.Label7.Text = "Προϊον";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(9, 39);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(50, 13);
            this.Label1.TabIndex = 796;
            this.Label1.Text = "Πελάτης";
            // 
            // chkCancel
            // 
            this.chkCancel.AutoSize = true;
            this.chkCancel.BackColor = System.Drawing.Color.Orange;
            this.chkCancel.Checked = true;
            this.chkCancel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCancel.Location = new System.Drawing.Point(1032, 14);
            this.chkCancel.Name = "chkCancel";
            this.chkCancel.Size = new System.Drawing.Size(68, 17);
            this.chkCancel.TabIndex = 794;
            this.chkCancel.Text = "Άκυρο    ";
            this.chkCancel.UseVisualStyleBackColor = false;
            // 
            // chkYes
            // 
            this.chkYes.AutoSize = true;
            this.chkYes.BackColor = System.Drawing.Color.LightGreen;
            this.chkYes.Checked = true;
            this.chkYes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkYes.Location = new System.Drawing.Point(940, 14);
            this.chkYes.Name = "chkYes";
            this.chkYes.Size = new System.Drawing.Size(69, 17);
            this.chkYes.TabIndex = 793;
            this.chkYes.Text = "Αποδοχή";
            this.chkYes.UseVisualStyleBackColor = false;
            // 
            // chkNot
            // 
            this.chkNot.AutoSize = true;
            this.chkNot.BackColor = System.Drawing.Color.LightCoral;
            this.chkNot.Checked = true;
            this.chkNot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNot.Location = new System.Drawing.Point(822, 15);
            this.chkNot.Name = "chkNot";
            this.chkNot.Size = new System.Drawing.Size(92, 17);
            this.chkNot.TabIndex = 792;
            this.chkNot.Text = "Μην αποδοχή";
            this.chkNot.UseVisualStyleBackColor = false;
            // 
            // chkThink
            // 
            this.chkThink.AutoSize = true;
            this.chkThink.BackColor = System.Drawing.Color.Yellow;
            this.chkThink.Checked = true;
            this.chkThink.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkThink.Location = new System.Drawing.Point(703, 14);
            this.chkThink.Name = "chkThink";
            this.chkThink.Size = new System.Drawing.Size(94, 17);
            this.chkThink.TabIndex = 791;
            this.chkThink.Text = "Απεσταλμένη";
            this.chkThink.UseVisualStyleBackColor = false;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(505, 15);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(66, 13);
            this.Label4.TabIndex = 790;
            this.Label4.Text = "Κατάσταση";
            // 
            // chkNew
            // 
            this.chkNew.AutoSize = true;
            this.chkNew.BackColor = System.Drawing.Color.White;
            this.chkNew.Checked = true;
            this.chkNew.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNew.Location = new System.Drawing.Point(588, 15);
            this.chkNew.Name = "chkNew";
            this.chkNew.Size = new System.Drawing.Size(84, 17);
            this.chkNew.TabIndex = 789;
            this.chkNew.Text = "Νέα εντολή";
            this.chkNew.UseVisualStyleBackColor = false;
            // 
            // fgRecs
            // 
            this.fgRecs.AllowEditing = false;
            this.fgRecs.ColumnInfo = resources.GetString("fgRecs.ColumnInfo");
            this.fgRecs.Location = new System.Drawing.Point(6, 121);
            this.fgRecs.Name = "fgRecs";
            this.fgRecs.Rows.Count = 1;
            this.fgRecs.Rows.DefaultSize = 17;
            this.fgRecs.Size = new System.Drawing.Size(1245, 572);
            this.fgRecs.TabIndex = 786;
            // 
            // picEmptyShare
            // 
            this.picEmptyShare.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picEmptyShare.Image = global::Transactions.Properties.Resources.cleanup;
            this.picEmptyShare.Location = new System.Drawing.Point(944, 62);
            this.picEmptyShare.Name = "picEmptyShare";
            this.picEmptyShare.Size = new System.Drawing.Size(28, 23);
            this.picEmptyShare.TabIndex = 803;
            this.picEmptyShare.TabStop = false;
            // 
            // picEmptyClient
            // 
            this.picEmptyClient.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picEmptyClient.Image = global::Transactions.Properties.Resources.cleanup;
            this.picEmptyClient.Location = new System.Drawing.Point(822, 38);
            this.picEmptyClient.Name = "picEmptyClient";
            this.picEmptyClient.Size = new System.Drawing.Size(28, 23);
            this.picEmptyClient.TabIndex = 802;
            this.picEmptyClient.TabStop = false;
            // 
            // frmDPMOrdersList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1271, 732);
            this.Controls.Add(this.tabMain);
            this.Name = "frmDPMOrdersList";
            this.Text = "frmDPMOrdersList";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmDPMOrdersList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.toolProtaseis.ResumeLayout(false);
            this.toolProtaseis.PerformLayout();
            this.panCritiries.ResumeLayout(false);
            this.panCritiries.PerformLayout();
            this.mnuContext.ResumeLayout(false);
            this.panOrderType.ResumeLayout(false);
            this.panOrderType.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.toolSymvoules.ResumeLayout(false);
            this.toolSymvoules.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgRecs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmptyShare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmptyClient)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
        internal System.Windows.Forms.ToolStrip toolProtaseis;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel10;
        internal System.Windows.Forms.ToolStripButton tsbAdd;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator22;
        internal System.Windows.Forms.ToolStripButton tsbEdit;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator23;
        internal System.Windows.Forms.ToolStripButton tsbCancel;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.ToolStripButton tsbSend;
        internal System.Windows.Forms.Panel panCritiries;
        internal System.Windows.Forms.ComboBox cmbDiaxiristes;
        internal System.Windows.Forms.Label Label25;
        internal System.Windows.Forms.ContextMenuStrip mnuContext;
        internal System.Windows.Forms.ToolStripMenuItem mnuClientData;
        internal System.Windows.Forms.ToolStripMenuItem mnuContractData;
        internal System.Windows.Forms.ToolStripMenuItem mnuProductData;
        internal System.Windows.Forms.ToolStripMenuItem mnuCopyISIN;
        internal System.Windows.Forms.ToolStripMenuItem mnuNewCommand;
        internal System.Windows.Forms.ToolStripMenuItem mnuShowFile;
        internal System.Windows.Forms.ToolStripMenuItem XXXToolStripMenuItem;
        internal System.Windows.Forms.DateTimePicker dToday;
        private System.Windows.Forms.Panel panOrderType;
        private System.Windows.Forms.LinkLabel lnkProduct;
        private System.Windows.Forms.LinkLabel lnkCustomer;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        internal System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Core.ucProductsSearch ucPS;
        private Core.ucContractsSearch ucCS;
        internal System.Windows.Forms.ToolStrip toolSymvoules;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel1;
        internal System.Windows.Forms.ToolStripButton tsbRefreshProposals;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        internal System.Windows.Forms.ToolStripButton tsbPlayRecievedFile;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label lblISIN;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label lblShareTitle;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.CheckBox chkCancel;
        internal System.Windows.Forms.CheckBox chkYes;
        internal System.Windows.Forms.CheckBox chkNot;
        internal System.Windows.Forms.CheckBox chkThink;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.CheckBox chkNew;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgRecs;
        internal System.Windows.Forms.PictureBox picEmptyShare;
        internal System.Windows.Forms.PictureBox picEmptyClient;
        private Core.ucDoubleCalendar ucDates;
        internal System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Label label9;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label8;
        internal System.Windows.Forms.CheckBox chkList;
    }
}