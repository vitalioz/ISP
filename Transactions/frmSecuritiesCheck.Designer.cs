namespace Transactions
{
    partial class frmSecuritiesCheck
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSecuritiesCheck));
            this.panEMail = new System.Windows.Forms.Panel();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtThema = new System.Windows.Forms.TextBox();
            this.Label32 = new System.Windows.Forms.Label();
            this.txtEMail = new System.Windows.Forms.TextBox();
            this.btnCancelMail = new System.Windows.Forms.Button();
            this.btnSendMail = new System.Windows.Forms.Button();
            this.Label9 = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.cmbProducts = new System.Windows.Forms.ComboBox();
            this.btnCleanUp = new System.Windows.Forms.Button();
            this.lnkPelatis = new System.Windows.Forms.LinkLabel();
            this.Label48 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.cmbProblemTypes = new System.Windows.Forms.ComboBox();
            this.chkDateExec = new System.Windows.Forms.CheckBox();
            this.chkDateIns = new System.Windows.Forms.CheckBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.dExecTo = new System.Windows.Forms.DateTimePicker();
            this.dExecFrom = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.chkList = new System.Windows.Forms.CheckBox();
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCustomerData = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.ToolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.imgFiles = new System.Windows.Forms.ImageList(this.components);
            this.toolLeft = new System.Windows.Forms.ToolStrip();
            this.tsbPrint = new System.Windows.Forms.ToolStripButton();
            this.tsbSend = new System.Windows.Forms.ToolStripButton();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.Label5 = new System.Windows.Forms.Label();
            this.cmbStockCompanies = new System.Windows.Forms.ComboBox();
            this.dTo = new System.Windows.Forms.DateTimePicker();
            this.Label12 = new System.Windows.Forms.Label();
            this.dFrom = new System.Windows.Forms.DateTimePicker();
            this.Label11 = new System.Windows.Forms.Label();
            this.panCritiries = new System.Windows.Forms.Panel();
            this.lnkShareTitle = new System.Windows.Forms.LinkLabel();
            this.lnkISIN = new System.Windows.Forms.LinkLabel();
            this.ucCS = new Core.ucContractsSearch();
            this.ucPS = new Core.ucProductsSearch();
            this.panEMail.SuspendLayout();
            this.mnuContext.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.toolLeft.SuspendLayout();
            this.panCritiries.SuspendLayout();
            this.SuspendLayout();
            // 
            // panEMail
            // 
            this.panEMail.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panEMail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panEMail.Controls.Add(this.Label2);
            this.panEMail.Controls.Add(this.txtThema);
            this.panEMail.Controls.Add(this.Label32);
            this.panEMail.Controls.Add(this.txtEMail);
            this.panEMail.Controls.Add(this.btnCancelMail);
            this.panEMail.Controls.Add(this.btnSendMail);
            this.panEMail.Controls.Add(this.Label9);
            this.panEMail.Controls.Add(this.txtNotes);
            this.panEMail.Location = new System.Drawing.Point(47, 253);
            this.panEMail.Name = "panEMail";
            this.panEMail.Size = new System.Drawing.Size(548, 263);
            this.panEMail.TabIndex = 1027;
            this.panEMail.Visible = false;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(12, 18);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(33, 13);
            this.Label2.TabIndex = 519;
            this.Label2.Text = "Θέμα";
            // 
            // txtThema
            // 
            this.txtThema.Location = new System.Drawing.Point(64, 15);
            this.txtThema.Name = "txtThema";
            this.txtThema.Size = new System.Drawing.Size(467, 20);
            this.txtThema.TabIndex = 500;
            // 
            // Label32
            // 
            this.Label32.AutoSize = true;
            this.Label32.Location = new System.Drawing.Point(12, 44);
            this.Label32.Name = "Label32";
            this.Label32.Size = new System.Drawing.Size(35, 13);
            this.Label32.TabIndex = 259;
            this.Label32.Text = "E-mail";
            // 
            // txtEMail
            // 
            this.txtEMail.Location = new System.Drawing.Point(64, 41);
            this.txtEMail.Name = "txtEMail";
            this.txtEMail.Size = new System.Drawing.Size(467, 20);
            this.txtEMail.TabIndex = 504;
            // 
            // btnCancelMail
            // 
            this.btnCancelMail.Image = global::Transactions.Properties.Resources.cancel1;
            this.btnCancelMail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelMail.Location = new System.Drawing.Point(294, 225);
            this.btnCancelMail.Name = "btnCancelMail";
            this.btnCancelMail.Size = new System.Drawing.Size(100, 26);
            this.btnCancelMail.TabIndex = 518;
            this.btnCancelMail.Text = "   Άκυρο";
            this.btnCancelMail.UseVisualStyleBackColor = true;
            this.btnCancelMail.Click += new System.EventHandler(this.btnCancelMail_Click);
            // 
            // btnSendMail
            // 
            this.btnSendMail.Image = global::Transactions.Properties.Resources.OK;
            this.btnSendMail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSendMail.Location = new System.Drawing.Point(156, 225);
            this.btnSendMail.Name = "btnSendMail";
            this.btnSendMail.Size = new System.Drawing.Size(100, 26);
            this.btnSendMail.TabIndex = 516;
            this.btnSendMail.Text = "OK";
            this.btnSendMail.UseVisualStyleBackColor = true;
            this.btnSendMail.Click += new System.EventHandler(this.btnSendMail_Click);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(12, 70);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(40, 13);
            this.Label9.TabIndex = 126;
            this.Label9.Text = "Σχόλιο";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(64, 67);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(467, 149);
            this.txtNotes.TabIndex = 506;
            // 
            // cmbProducts
            // 
            this.cmbProducts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProducts.FormattingEnabled = true;
            this.cmbProducts.Location = new System.Drawing.Point(66, 82);
            this.cmbProducts.Name = "cmbProducts";
            this.cmbProducts.Size = new System.Drawing.Size(200, 21);
            this.cmbProducts.TabIndex = 22;
            // 
            // btnCleanUp
            // 
            this.btnCleanUp.Image = global::Transactions.Properties.Resources.cleanup;
            this.btnCleanUp.Location = new System.Drawing.Point(833, 5);
            this.btnCleanUp.Name = "btnCleanUp";
            this.btnCleanUp.Size = new System.Drawing.Size(26, 23);
            this.btnCleanUp.TabIndex = 1009;
            this.btnCleanUp.TabStop = false;
            this.btnCleanUp.UseVisualStyleBackColor = true;
            this.btnCleanUp.Click += new System.EventHandler(this.btnCleanUp_Click);
            // 
            // lnkPelatis
            // 
            this.lnkPelatis.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lnkPelatis.Location = new System.Drawing.Point(272, 61);
            this.lnkPelatis.Name = "lnkPelatis";
            this.lnkPelatis.Size = new System.Drawing.Size(428, 20);
            this.lnkPelatis.TabIndex = 1008;
            // 
            // Label48
            // 
            this.Label48.AutoSize = true;
            this.Label48.Location = new System.Drawing.Point(13, 89);
            this.Label48.Name = "Label48";
            this.Label48.Size = new System.Drawing.Size(42, 13);
            this.Label48.TabIndex = 1004;
            this.Label48.Text = "Προϊόν";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(277, 108);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(28, 13);
            this.Label6.TabIndex = 1003;
            this.Label6.Text = "ISIN";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(10, 62);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(50, 13);
            this.Label3.TabIndex = 999;
            this.Label3.Text = "Πελάτης";
            // 
            // cmbProblemTypes
            // 
            this.cmbProblemTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProblemTypes.FormattingEnabled = true;
            this.cmbProblemTypes.Location = new System.Drawing.Point(706, 34);
            this.cmbProblemTypes.Name = "cmbProblemTypes";
            this.cmbProblemTypes.Size = new System.Drawing.Size(153, 21);
            this.cmbProblemTypes.TabIndex = 18;
            this.cmbProblemTypes.Visible = false;
            // 
            // chkDateExec
            // 
            this.chkDateExec.AutoSize = true;
            this.chkDateExec.Checked = true;
            this.chkDateExec.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDateExec.Location = new System.Drawing.Point(13, 36);
            this.chkDateExec.Name = "chkDateExec";
            this.chkDateExec.Size = new System.Drawing.Size(161, 17);
            this.chkDateExec.TabIndex = 10;
            this.chkDateExec.Text = " Ημερ/νίες εκτέλεσεις από";
            this.chkDateExec.UseVisualStyleBackColor = true;
            // 
            // chkDateIns
            // 
            this.chkDateIns.AutoSize = true;
            this.chkDateIns.Location = new System.Drawing.Point(13, 9);
            this.chkDateIns.Name = "chkDateIns";
            this.chkDateIns.Size = new System.Drawing.Size(175, 17);
            this.chkDateIns.TabIndex = 2;
            this.chkDateIns.Text = "Ημερ/νίες καταχώρησεις από";
            this.chkDateIns.UseVisualStyleBackColor = true;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(276, 39);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(27, 13);
            this.Label1.TabIndex = 296;
            this.Label1.Text = "εώς";
            // 
            // dExecTo
            // 
            this.dExecTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dExecTo.Location = new System.Drawing.Point(308, 35);
            this.dExecTo.Name = "dExecTo";
            this.dExecTo.Size = new System.Drawing.Size(87, 20);
            this.dExecTo.TabIndex = 14;
            this.dExecTo.ValueChanged += new System.EventHandler(this.dExecTo_ValueChanged);
            // 
            // dExecFrom
            // 
            this.dExecFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dExecFrom.Location = new System.Drawing.Point(190, 35);
            this.dExecFrom.Name = "dExecFrom";
            this.dExecFrom.Size = new System.Drawing.Size(84, 20);
            this.dExecFrom.TabIndex = 12;
            this.dExecFrom.ValueChanged += new System.EventHandler(this.dExecFrom_ValueChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(876, 98);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(91, 27);
            this.btnSearch.TabIndex = 30;
            this.btnSearch.Text = "Αναζήτηση";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // chkList
            // 
            this.chkList.AutoSize = true;
            this.chkList.Location = new System.Drawing.Point(18, 185);
            this.chkList.Name = "chkList";
            this.chkList.Size = new System.Drawing.Size(15, 14);
            this.chkList.TabIndex = 1028;
            this.chkList.UseVisualStyleBackColor = true;
            this.chkList.CheckedChanged += new System.EventHandler(this.chkList_CheckedChanged);
            // 
            // mnuContext
            // 
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCustomerData,
            this.mnuShowProduct});
            this.mnuContext.Name = "ContextMenuStrip1";
            this.mnuContext.Size = new System.Drawing.Size(183, 48);
            // 
            // mnuCustomerData
            // 
            this.mnuCustomerData.Name = "mnuCustomerData";
            this.mnuCustomerData.Size = new System.Drawing.Size(182, 22);
            this.mnuCustomerData.Text = "Στοιχεία τού πελάτη";
            this.mnuCustomerData.Click += new System.EventHandler(this.mnuCustomerData_Click);
            // 
            // mnuShowProduct
            // 
            this.mnuShowProduct.Name = "mnuShowProduct";
            this.mnuShowProduct.Size = new System.Drawing.Size(182, 22);
            this.mnuShowProduct.Text = "Στοιχεία Προϊόντος";
            this.mnuShowProduct.Click += new System.EventHandler(this.mnuShowProduct_Click);
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "Όλες εντολές",
            "Δεν ελέγχθηκε",
            "ΟΚ",
            "Πρόβλημα",
            ""});
            this.cmbStatus.Location = new System.Drawing.Point(501, 34);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(201, 21);
            this.cmbStatus.TabIndex = 16;
            // 
            // fgList
            // 
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(11, 173);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 2;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Rows.Fixed = 2;
            this.fgList.Size = new System.Drawing.Size(972, 484);
            this.fgList.TabIndex = 30;
            this.fgList.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.fgList_CellChanged);
            // 
            // ToolStripLabel2
            // 
            this.ToolStripLabel2.Name = "ToolStripLabel2";
            this.ToolStripLabel2.Size = new System.Drawing.Size(10, 22);
            this.ToolStripLabel2.Text = " ";
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // imgFiles
            // 
            this.imgFiles.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgFiles.ImageStream")));
            this.imgFiles.TransparentColor = System.Drawing.Color.Transparent;
            this.imgFiles.Images.SetKeyName(0, "limit.jpg");
            this.imgFiles.Images.SetKeyName(1, "Pdf-icon.png");
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
            this.tsbPrint,
            this.ToolStripSeparator1,
            this.tsbSend,
            this.ToolStripSeparator2,
            this.tsbHelp});
            this.toolLeft.Location = new System.Drawing.Point(11, 143);
            this.toolLeft.Name = "toolLeft";
            this.toolLeft.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolLeft.Size = new System.Drawing.Size(105, 25);
            this.toolLeft.TabIndex = 1026;
            this.toolLeft.Text = "ToolStrip1";
            // 
            // tsbPrint
            // 
            this.tsbPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPrint.Image = global::Transactions.Properties.Resources.PrintHS;
            this.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Size = new System.Drawing.Size(23, 22);
            this.tsbPrint.Text = "Εκτύπωση λίστας";
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
            // tsbHelp
            // 
            this.tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHelp.Image = global::Transactions.Properties.Resources.Help;
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(23, 22);
            this.tsbHelp.Text = "Βοήθεια";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(277, 10);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(27, 13);
            this.Label5.TabIndex = 215;
            this.Label5.Text = "εώς";
            // 
            // cmbStockCompanies
            // 
            this.cmbStockCompanies.FormattingEnabled = true;
            this.cmbStockCompanies.Location = new System.Drawing.Point(501, 5);
            this.cmbStockCompanies.Name = "cmbStockCompanies";
            this.cmbStockCompanies.Size = new System.Drawing.Size(201, 21);
            this.cmbStockCompanies.TabIndex = 8;
            // 
            // dTo
            // 
            this.dTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dTo.Location = new System.Drawing.Point(310, 6);
            this.dTo.Name = "dTo";
            this.dTo.Size = new System.Drawing.Size(84, 20);
            this.dTo.TabIndex = 6;
            this.dTo.ValueChanged += new System.EventHandler(this.dTo_ValueChanged);
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(432, 37);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(66, 13);
            this.Label12.TabIndex = 3;
            this.Label12.Text = "Κατάσταση";
            // 
            // dFrom
            // 
            this.dFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dFrom.Location = new System.Drawing.Point(190, 6);
            this.dFrom.Name = "dFrom";
            this.dFrom.Size = new System.Drawing.Size(84, 20);
            this.dFrom.TabIndex = 4;
            this.dFrom.ValueChanged += new System.EventHandler(this.dFrom_ValueChanged);
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(432, 9);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(51, 13);
            this.Label11.TabIndex = 2;
            this.Label11.Text = "Πάροχος";
            // 
            // panCritiries
            // 
            this.panCritiries.BackColor = System.Drawing.Color.LightGray;
            this.panCritiries.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panCritiries.Controls.Add(this.lnkShareTitle);
            this.panCritiries.Controls.Add(this.cmbProblemTypes);
            this.panCritiries.Controls.Add(this.cmbStatus);
            this.panCritiries.Controls.Add(this.lnkISIN);
            this.panCritiries.Controls.Add(this.cmbProducts);
            this.panCritiries.Controls.Add(this.btnCleanUp);
            this.panCritiries.Controls.Add(this.lnkPelatis);
            this.panCritiries.Controls.Add(this.Label48);
            this.panCritiries.Controls.Add(this.Label6);
            this.panCritiries.Controls.Add(this.Label3);
            this.panCritiries.Controls.Add(this.chkDateExec);
            this.panCritiries.Controls.Add(this.chkDateIns);
            this.panCritiries.Controls.Add(this.Label1);
            this.panCritiries.Controls.Add(this.dExecTo);
            this.panCritiries.Controls.Add(this.dExecFrom);
            this.panCritiries.Controls.Add(this.btnSearch);
            this.panCritiries.Controls.Add(this.Label5);
            this.panCritiries.Controls.Add(this.cmbStockCompanies);
            this.panCritiries.Controls.Add(this.dTo);
            this.panCritiries.Controls.Add(this.Label12);
            this.panCritiries.Controls.Add(this.dFrom);
            this.panCritiries.Controls.Add(this.Label11);
            this.panCritiries.Location = new System.Drawing.Point(10, 6);
            this.panCritiries.Name = "panCritiries";
            this.panCritiries.Size = new System.Drawing.Size(972, 130);
            this.panCritiries.TabIndex = 1025;
            // 
            // lnkShareTitle
            // 
            this.lnkShareTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lnkShareTitle.Location = new System.Drawing.Point(445, 105);
            this.lnkShareTitle.Name = "lnkShareTitle";
            this.lnkShareTitle.Size = new System.Drawing.Size(414, 20);
            this.lnkShareTitle.TabIndex = 1011;
            // 
            // lnkISIN
            // 
            this.lnkISIN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lnkISIN.Location = new System.Drawing.Point(311, 105);
            this.lnkISIN.Name = "lnkISIN";
            this.lnkISIN.Size = new System.Drawing.Size(128, 20);
            this.lnkISIN.TabIndex = 1010;
            // 
            // ucCS
            // 
            this.ucCS.BackColor = System.Drawing.Color.Transparent;
            this.ucCS.CodesList = null;
            this.ucCS.Filters = "Client_ID > 0 AND Status = 1";
            this.ucCS.ListType = 0;
            this.ucCS.Location = new System.Drawing.Point(77, 66);
            this.ucCS.Mode = 0;
            this.ucCS.Name = "ucCS";
            this.ucCS.ShowClientsList = true;
            this.ucCS.ShowHeight = 0;
            this.ucCS.ShowWidth = 0;
            this.ucCS.Size = new System.Drawing.Size(201, 20);
            this.ucCS.TabIndex = 20;
            // 
            // ucPS
            // 
            this.ucPS.CodesList = null;
            this.ucPS.Filters = "Shares_ID > 0";
            this.ucPS.ListType = 0;
            this.ucPS.Location = new System.Drawing.Point(76, 112);
            this.ucPS.Mode = 0;
            this.ucPS.Name = "ucPS";
            this.ucPS.ProductsContract = null;
            this.ucPS.ShowCancelled = true;
            this.ucPS.ShowHeight = 0;
            this.ucPS.ShowNonAccord = true;
            this.ucPS.ShowProductsList = true;
            this.ucPS.ShowWidth = 0;
            this.ucPS.Size = new System.Drawing.Size(202, 20);
            this.ucPS.TabIndex = 24;
            // 
            // frmSecuritiesCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Moccasin;
            this.ClientSize = new System.Drawing.Size(992, 662);
            this.Controls.Add(this.panEMail);
            this.Controls.Add(this.ucCS);
            this.Controls.Add(this.ucPS);
            this.Controls.Add(this.chkList);
            this.Controls.Add(this.fgList);
            this.Controls.Add(this.toolLeft);
            this.Controls.Add(this.panCritiries);
            this.Name = "frmSecuritiesCheck";
            this.Text = "Έλεγχος πινακιδίων";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSecuritiesCheck_Load);
            this.panEMail.ResumeLayout(false);
            this.panEMail.PerformLayout();
            this.mnuContext.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.toolLeft.ResumeLayout(false);
            this.toolLeft.PerformLayout();
            this.panCritiries.ResumeLayout(false);
            this.panCritiries.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Panel panEMail;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtThema;
        internal System.Windows.Forms.Label Label32;
        internal System.Windows.Forms.TextBox txtEMail;
        internal System.Windows.Forms.Button btnCancelMail;
        internal System.Windows.Forms.Button btnSendMail;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.TextBox txtNotes;
        internal System.Windows.Forms.ComboBox cmbProducts;
        internal System.Windows.Forms.Button btnCleanUp;
        internal System.Windows.Forms.LinkLabel lnkPelatis;
        internal System.Windows.Forms.Label Label48;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.ComboBox cmbProblemTypes;
        internal System.Windows.Forms.CheckBox chkDateExec;
        internal System.Windows.Forms.CheckBox chkDateIns;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.DateTimePicker dExecTo;
        internal System.Windows.Forms.DateTimePicker dExecFrom;
        internal System.Windows.Forms.Button btnSearch;
        internal System.Windows.Forms.CheckBox chkList;
        internal System.Windows.Forms.ContextMenuStrip mnuContext;
        internal System.Windows.Forms.ToolStripMenuItem mnuCustomerData;
        internal System.Windows.Forms.ToolStripMenuItem mnuShowProduct;
        internal System.Windows.Forms.ComboBox cmbStatus;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel2;
        internal System.Windows.Forms.ToolStripButton tsbPrint;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripButton tsbSend;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        internal System.Windows.Forms.ToolStripButton tsbHelp;
        internal System.Windows.Forms.ImageList imgFiles;
        internal System.Windows.Forms.ToolStrip toolLeft;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.ComboBox cmbStockCompanies;
        internal System.Windows.Forms.DateTimePicker dTo;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.DateTimePicker dFrom;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.Panel panCritiries;
        private Core.ucContractsSearch ucCS;
        private Core.ucProductsSearch ucPS;
        internal System.Windows.Forms.LinkLabel lnkISIN;
        internal System.Windows.Forms.LinkLabel lnkShareTitle;
    }
}