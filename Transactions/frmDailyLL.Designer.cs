namespace Transactions
{
    partial class frmDailyLL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDailyLL));
            this.toolLeft = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsbPrint = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.imgFile = new System.Windows.Forms.ImageList(this.components);
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CustomerData = new System.Windows.Forms.ToolStripMenuItem();
            this.NewCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.btnCleanUp = new System.Windows.Forms.Button();
            this.dToday = new System.Windows.Forms.DateTimePicker();
            this.lnkPortfolio = new System.Windows.Forms.LinkLabel();
            this.lnkPelatis = new System.Windows.Forms.LinkLabel();
            this.lblCode = new System.Windows.Forms.Label();
            this.lblContract = new System.Windows.Forms.Label();
            this.dPeriodEnd = new System.Windows.Forms.DateTimePicker();
            this.Label19 = new System.Windows.Forms.Label();
            this.dPeriodStart = new System.Windows.Forms.DateTimePicker();
            this.Label38 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.cmbCashAccounts = new System.Windows.Forms.ComboBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.cmbCurr = new System.Windows.Forms.ComboBox();
            this.txtClientsGrossRate = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.panDaily = new System.Windows.Forms.Panel();
            this.panFilters = new System.Windows.Forms.Panel();
            this.cmbDiax = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.chkShowCancelled = new System.Windows.Forms.CheckBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSender = new System.Windows.Forms.Label();
            this.cmbDivisions = new System.Windows.Forms.ComboBox();
            this.lblProvider = new System.Windows.Forms.Label();
            this.cmbSent = new System.Windows.Forms.ComboBox();
            this.lblAdvisor = new System.Windows.Forms.Label();
            this.cmbProviders = new System.Windows.Forms.ComboBox();
            this.lblExecute = new System.Windows.Forms.Label();
            this.lblSended = new System.Windows.Forms.Label();
            this.cmbAdvisors = new System.Windows.Forms.ComboBox();
            this.cmbActions = new System.Windows.Forms.ComboBox();
            this.cmbUsers = new System.Windows.Forms.ComboBox();
            this.panSearch = new System.Windows.Forms.Panel();
            this.fgClientsChoiced = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.picEmptyClient = new System.Windows.Forms.PictureBox();
            this.label25 = new System.Windows.Forms.Label();
            this.cmbCurrFX_DC = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTypeFX = new System.Windows.Forms.ComboBox();
            this.Label37 = new System.Windows.Forms.Label();
            this.cmbCurrFX_C = new System.Windows.Forms.ComboBox();
            this.Label36 = new System.Windows.Forms.Label();
            this.cmbCurrFX_D = new System.Windows.Forms.ComboBox();
            this.Label35 = new System.Windows.Forms.Label();
            this.Label29 = new System.Windows.Forms.Label();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.ucCS = new Core.ucContractsSearch();
            this.ucDC = new Core.ucDoubleCalendar();
            this.toolLeft.SuspendLayout();
            this.mnuContext.SuspendLayout();
            this.panDaily.SuspendLayout();
            this.panFilters.SuspendLayout();
            this.panSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgClientsChoiced)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmptyClient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.SuspendLayout();
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
            this.toolStripButton1,
            this.ToolStripSeparator2,
            this.tsbHelp});
            this.toolLeft.Location = new System.Drawing.Point(7, 142);
            this.toolLeft.Name = "toolLeft";
            this.toolLeft.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolLeft.Size = new System.Drawing.Size(108, 26);
            this.toolLeft.TabIndex = 1035;
            this.toolLeft.Text = "ToolStrip1";
            // 
            // ToolStripLabel2
            // 
            this.ToolStripLabel2.Name = "ToolStripLabel2";
            this.ToolStripLabel2.Size = new System.Drawing.Size(10, 23);
            this.ToolStripLabel2.Text = " ";
            // 
            // tsbPrint
            // 
            this.tsbPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPrint.Image = ((System.Drawing.Image)(resources.GetObject("tsbPrint.Image")));
            this.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Size = new System.Drawing.Size(23, 23);
            this.tsbPrint.Text = "Εκτύπωση λίστας";
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 26);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::Transactions.Properties.Resources.excel;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 23);
            this.toolStripButton1.Text = "Εκτύπωση λίστας";
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 26);
            // 
            // tsbHelp
            // 
            this.tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbHelp.Image")));
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(23, 23);
            this.tsbHelp.Text = "Βοήθεια";
            // 
            // imgFile
            // 
            this.imgFile.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgFile.ImageStream")));
            this.imgFile.TransparentColor = System.Drawing.Color.Transparent;
            this.imgFile.Images.SetKeyName(0, "limit.jpg");
            this.imgFile.Images.SetKeyName(1, "pdf.jpg");
            // 
            // mnuContext
            // 
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CustomerData,
            this.NewCommand});
            this.mnuContext.Name = "ContextMenuStrip1";
            this.mnuContext.Size = new System.Drawing.Size(183, 48);
            // 
            // CustomerData
            // 
            this.CustomerData.Name = "CustomerData";
            this.CustomerData.Size = new System.Drawing.Size(182, 22);
            this.CustomerData.Text = "Στοιχεία τού πελάτη";
            // 
            // NewCommand
            // 
            this.NewCommand.Name = "NewCommand";
            this.NewCommand.Size = new System.Drawing.Size(182, 22);
            this.NewCommand.Text = "Νέα εντολή";
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Location = new System.Drawing.Point(4, 9);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(55, 13);
            this.lblCustomer.TabIndex = 1142;
            this.lblCustomer.Text = "Εντολέας";
            // 
            // btnCleanUp
            // 
            this.btnCleanUp.Image = global::Transactions.Properties.Resources.cleanup;
            this.btnCleanUp.Location = new System.Drawing.Point(849, 6);
            this.btnCleanUp.Name = "btnCleanUp";
            this.btnCleanUp.Size = new System.Drawing.Size(26, 23);
            this.btnCleanUp.TabIndex = 1143;
            this.btnCleanUp.TabStop = false;
            this.btnCleanUp.UseVisualStyleBackColor = true;
            this.btnCleanUp.Click += new System.EventHandler(this.btnCleanUp_Click);
            // 
            // dToday
            // 
            this.dToday.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dToday.Location = new System.Drawing.Point(761, 7);
            this.dToday.Name = "dToday";
            this.dToday.Size = new System.Drawing.Size(83, 20);
            this.dToday.TabIndex = 1144;
            this.dToday.ValueChanged += new System.EventHandler(this.dToday_ValueChanged);
            // 
            // lnkPortfolio
            // 
            this.lnkPortfolio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lnkPortfolio.Location = new System.Drawing.Point(220, 31);
            this.lnkPortfolio.Name = "lnkPortfolio";
            this.lnkPortfolio.Size = new System.Drawing.Size(150, 20);
            this.lnkPortfolio.TabIndex = 1148;
            // 
            // lnkPelatis
            // 
            this.lnkPelatis.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lnkPelatis.Location = new System.Drawing.Point(375, 31);
            this.lnkPelatis.Name = "lnkPelatis";
            this.lnkPelatis.Size = new System.Drawing.Size(468, 20);
            this.lnkPelatis.TabIndex = 1147;
            // 
            // lblCode
            // 
            this.lblCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCode.Location = new System.Drawing.Point(64, 31);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(150, 20);
            this.lblCode.TabIndex = 1146;
            // 
            // lblContract
            // 
            this.lblContract.AutoSize = true;
            this.lblContract.Location = new System.Drawing.Point(4, 34);
            this.lblContract.Name = "lblContract";
            this.lblContract.Size = new System.Drawing.Size(52, 13);
            this.lblContract.TabIndex = 1145;
            this.lblContract.Text = "Σύμβαση";
            // 
            // dPeriodEnd
            // 
            this.dPeriodEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dPeriodEnd.Location = new System.Drawing.Point(763, 59);
            this.dPeriodEnd.Name = "dPeriodEnd";
            this.dPeriodEnd.Size = new System.Drawing.Size(82, 20);
            this.dPeriodEnd.TabIndex = 1156;
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.Location = new System.Drawing.Point(733, 63);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(26, 13);
            this.Label19.TabIndex = 1157;
            this.Label19.Text = "End";
            this.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dPeriodStart
            // 
            this.dPeriodStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dPeriodStart.Location = new System.Drawing.Point(635, 59);
            this.dPeriodStart.Name = "dPeriodStart";
            this.dPeriodStart.Size = new System.Drawing.Size(85, 20);
            this.dPeriodStart.TabIndex = 1154;
            // 
            // Label38
            // 
            this.Label38.AutoSize = true;
            this.Label38.Location = new System.Drawing.Point(569, 63);
            this.Label38.Name = "Label38";
            this.Label38.Size = new System.Drawing.Size(62, 13);
            this.Label38.TabIndex = 1155;
            this.Label38.Text = "Period Start";
            this.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(401, 64);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(91, 13);
            this.Label9.TabIndex = 1153;
            this.Label9.Text = "Client\'s Gross rate";
            // 
            // cmbCashAccounts
            // 
            this.cmbCashAccounts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCashAccounts.FormattingEnabled = true;
            this.cmbCashAccounts.Location = new System.Drawing.Point(131, 60);
            this.cmbCashAccounts.Name = "cmbCashAccounts";
            this.cmbCashAccounts.Size = new System.Drawing.Size(188, 21);
            this.cmbCashAccounts.TabIndex = 1150;
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(321, 61);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(63, 20);
            this.txtAmount.TabIndex = 1151;
            // 
            // cmbCurr
            // 
            this.cmbCurr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurr.FormattingEnabled = true;
            this.cmbCurr.Location = new System.Drawing.Point(63, 60);
            this.cmbCurr.Name = "cmbCurr";
            this.cmbCurr.Size = new System.Drawing.Size(65, 21);
            this.cmbCurr.TabIndex = 1149;
            this.cmbCurr.SelectedValueChanged += new System.EventHandler(this.cmbCurr_SelectedValueChanged);
            // 
            // txtClientsGrossRate
            // 
            this.txtClientsGrossRate.Location = new System.Drawing.Point(496, 60);
            this.txtClientsGrossRate.Name = "txtClientsGrossRate";
            this.txtClientsGrossRate.Size = new System.Drawing.Size(58, 20);
            this.txtClientsGrossRate.TabIndex = 1152;
            // 
            // btnSave
            // 
            this.btnSave.Image = global::Transactions.Properties.Resources.save;
            this.btnSave.Location = new System.Drawing.Point(849, 58);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(26, 23);
            this.btnSave.TabIndex = 1158;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panDaily
            // 
            this.panDaily.Controls.Add(this.lblCustomer);
            this.panDaily.Controls.Add(this.lblContract);
            this.panDaily.Controls.Add(this.btnSave);
            this.panDaily.Controls.Add(this.lblCode);
            this.panDaily.Controls.Add(this.dPeriodEnd);
            this.panDaily.Controls.Add(this.lnkPelatis);
            this.panDaily.Controls.Add(this.Label19);
            this.panDaily.Controls.Add(this.lnkPortfolio);
            this.panDaily.Controls.Add(this.dPeriodStart);
            this.panDaily.Controls.Add(this.dToday);
            this.panDaily.Controls.Add(this.Label38);
            this.panDaily.Controls.Add(this.btnCleanUp);
            this.panDaily.Controls.Add(this.Label9);
            this.panDaily.Controls.Add(this.txtClientsGrossRate);
            this.panDaily.Controls.Add(this.cmbCashAccounts);
            this.panDaily.Controls.Add(this.cmbCurr);
            this.panDaily.Controls.Add(this.txtAmount);
            this.panDaily.Location = new System.Drawing.Point(4, 8);
            this.panDaily.Name = "panDaily";
            this.panDaily.Size = new System.Drawing.Size(888, 128);
            this.panDaily.TabIndex = 1159;
            this.panDaily.Visible = false;
            // 
            // panFilters
            // 
            this.panFilters.Controls.Add(this.cmbDiax);
            this.panFilters.Controls.Add(this.btnSearch);
            this.panFilters.Controls.Add(this.chkShowCancelled);
            this.panFilters.Controls.Add(this.label24);
            this.panFilters.Controls.Add(this.label2);
            this.panFilters.Controls.Add(this.lblSender);
            this.panFilters.Controls.Add(this.cmbDivisions);
            this.panFilters.Controls.Add(this.lblProvider);
            this.panFilters.Controls.Add(this.cmbSent);
            this.panFilters.Controls.Add(this.lblAdvisor);
            this.panFilters.Controls.Add(this.cmbProviders);
            this.panFilters.Controls.Add(this.lblExecute);
            this.panFilters.Controls.Add(this.lblSended);
            this.panFilters.Controls.Add(this.cmbAdvisors);
            this.panFilters.Controls.Add(this.cmbActions);
            this.panFilters.Controls.Add(this.cmbUsers);
            this.panFilters.Location = new System.Drawing.Point(910, 8);
            this.panFilters.Name = "panFilters";
            this.panFilters.Size = new System.Drawing.Size(762, 128);
            this.panFilters.TabIndex = 1160;
            // 
            // cmbDiax
            // 
            this.cmbDiax.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDiax.FormattingEnabled = true;
            this.cmbDiax.Location = new System.Drawing.Point(390, 44);
            this.cmbDiax.Name = "cmbDiax";
            this.cmbDiax.Size = new System.Drawing.Size(215, 21);
            this.cmbDiax.TabIndex = 1212;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(666, 59);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(93, 27);
            this.btnSearch.TabIndex = 1250;
            this.btnSearch.Text = "Αναζήτηση";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // chkShowCancelled
            // 
            this.chkShowCancelled.AutoSize = true;
            this.chkShowCancelled.Location = new System.Drawing.Point(390, 71);
            this.chkShowCancelled.Name = "chkShowCancelled";
            this.chkShowCancelled.Size = new System.Drawing.Size(179, 17);
            this.chkShowCancelled.TabIndex = 1137;
            this.chkShowCancelled.Text = "Προβολή ακυρομένων εντολών";
            this.chkShowCancelled.UseVisualStyleBackColor = true;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(314, 48);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(73, 13);
            this.label24.TabIndex = 1012;
            this.label24.Text = "Διαχειριστης";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 1009;
            this.label2.Text = "Υποκατάστημα";
            // 
            // lblSender
            // 
            this.lblSender.AutoSize = true;
            this.lblSender.Location = new System.Drawing.Point(314, 6);
            this.lblSender.Name = "lblSender";
            this.lblSender.Size = new System.Drawing.Size(71, 13);
            this.lblSender.TabIndex = 999;
            this.lblSender.Text = "Διαβιβαστής";
            // 
            // cmbDivisions
            // 
            this.cmbDivisions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDivisions.FormattingEnabled = true;
            this.cmbDivisions.Location = new System.Drawing.Point(89, 44);
            this.cmbDivisions.Name = "cmbDivisions";
            this.cmbDivisions.Size = new System.Drawing.Size(215, 21);
            this.cmbDivisions.TabIndex = 1204;
            // 
            // lblProvider
            // 
            this.lblProvider.AutoSize = true;
            this.lblProvider.Location = new System.Drawing.Point(4, 6);
            this.lblProvider.Name = "lblProvider";
            this.lblProvider.Size = new System.Drawing.Size(51, 13);
            this.lblProvider.TabIndex = 1003;
            this.lblProvider.Text = "Πάροχος";
            // 
            // cmbSent
            // 
            this.cmbSent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSent.FormattingEnabled = true;
            this.cmbSent.Items.AddRange(new object[] {
            "Όλες πράξεις",
            "Μόνο διαβίβασμένες πράξεις",
            "Μόνο αδιαβίβαστες πράξεις"});
            this.cmbSent.Location = new System.Drawing.Point(390, 23);
            this.cmbSent.Name = "cmbSent";
            this.cmbSent.Size = new System.Drawing.Size(215, 21);
            this.cmbSent.TabIndex = 1210;
            // 
            // lblAdvisor
            // 
            this.lblAdvisor.AutoSize = true;
            this.lblAdvisor.Location = new System.Drawing.Point(4, 28);
            this.lblAdvisor.Name = "lblAdvisor";
            this.lblAdvisor.Size = new System.Drawing.Size(61, 13);
            this.lblAdvisor.TabIndex = 1001;
            this.lblAdvisor.Text = "Σύμβουλος";
            // 
            // cmbProviders
            // 
            this.cmbProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProviders.FormattingEnabled = true;
            this.cmbProviders.Location = new System.Drawing.Point(89, 2);
            this.cmbProviders.Name = "cmbProviders";
            this.cmbProviders.Size = new System.Drawing.Size(215, 21);
            this.cmbProviders.TabIndex = 1200;
            // 
            // lblExecute
            // 
            this.lblExecute.AutoSize = true;
            this.lblExecute.Location = new System.Drawing.Point(4, 69);
            this.lblExecute.Name = "lblExecute";
            this.lblExecute.Size = new System.Drawing.Size(47, 13);
            this.lblExecute.TabIndex = 1000;
            this.lblExecute.Text = "Πράξεις";
            // 
            // lblSended
            // 
            this.lblSended.AutoSize = true;
            this.lblSended.Location = new System.Drawing.Point(314, 28);
            this.lblSended.Name = "lblSended";
            this.lblSended.Size = new System.Drawing.Size(59, 13);
            this.lblSended.TabIndex = 1002;
            this.lblSended.Text = "Διαβίβαση";
            // 
            // cmbAdvisors
            // 
            this.cmbAdvisors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAdvisors.FormattingEnabled = true;
            this.cmbAdvisors.Location = new System.Drawing.Point(89, 23);
            this.cmbAdvisors.Name = "cmbAdvisors";
            this.cmbAdvisors.Size = new System.Drawing.Size(215, 21);
            this.cmbAdvisors.TabIndex = 1202;
            // 
            // cmbActions
            // 
            this.cmbActions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActions.FormattingEnabled = true;
            this.cmbActions.Items.AddRange(new object[] {
            "Όλες πράξεις",
            "Μόνο εκτελεσμένες πράξεις",
            "Μόνο ανεκτέλεστες πράξεις"});
            this.cmbActions.Location = new System.Drawing.Point(89, 65);
            this.cmbActions.Name = "cmbActions";
            this.cmbActions.Size = new System.Drawing.Size(215, 21);
            this.cmbActions.TabIndex = 1206;
            // 
            // cmbUsers
            // 
            this.cmbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUsers.FormattingEnabled = true;
            this.cmbUsers.Location = new System.Drawing.Point(390, 2);
            this.cmbUsers.Name = "cmbUsers";
            this.cmbUsers.Size = new System.Drawing.Size(215, 21);
            this.cmbUsers.TabIndex = 1208;
            // 
            // panSearch
            // 
            this.panSearch.Controls.Add(this.fgClientsChoiced);
            this.panSearch.Controls.Add(this.label1);
            this.panSearch.Controls.Add(this.picEmptyClient);
            this.panSearch.Controls.Add(this.label25);
            this.panSearch.Controls.Add(this.cmbCurrFX_DC);
            this.panSearch.Controls.Add(this.label3);
            this.panSearch.Controls.Add(this.cmbTypeFX);
            this.panSearch.Controls.Add(this.Label37);
            this.panSearch.Controls.Add(this.cmbCurrFX_C);
            this.panSearch.Controls.Add(this.Label36);
            this.panSearch.Controls.Add(this.cmbCurrFX_D);
            this.panSearch.Controls.Add(this.Label35);
            this.panSearch.Controls.Add(this.ucDC);
            this.panSearch.Controls.Add(this.Label29);
            this.panSearch.Location = new System.Drawing.Point(910, 142);
            this.panSearch.Name = "panSearch";
            this.panSearch.Size = new System.Drawing.Size(888, 128);
            this.panSearch.TabIndex = 1161;
            this.panSearch.Visible = false;
            // 
            // fgClientsChoiced
            // 
            this.fgClientsChoiced.ColumnInfo = resources.GetString("fgClientsChoiced.ColumnInfo");
            this.fgClientsChoiced.Location = new System.Drawing.Point(440, 29);
            this.fgClientsChoiced.Name = "fgClientsChoiced";
            this.fgClientsChoiced.Rows.Count = 1;
            this.fgClientsChoiced.Rows.DefaultSize = 17;
            this.fgClientsChoiced.Size = new System.Drawing.Size(253, 91);
            this.fgClientsChoiced.TabIndex = 1153;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(381, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 1156;
            this.label1.Text = "Σύμβαση";
            // 
            // picEmptyClient
            // 
            this.picEmptyClient.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picEmptyClient.Image = global::Transactions.Properties.Resources.cleanup;
            this.picEmptyClient.Location = new System.Drawing.Point(853, 10);
            this.picEmptyClient.Name = "picEmptyClient";
            this.picEmptyClient.Size = new System.Drawing.Size(22, 21);
            this.picEmptyClient.TabIndex = 1155;
            this.picEmptyClient.TabStop = false;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(382, 12);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(50, 13);
            this.label25.TabIndex = 1154;
            this.label25.Text = "Πελάτης";
            // 
            // cmbCurrFX_DC
            // 
            this.cmbCurrFX_DC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrFX_DC.FormattingEnabled = true;
            this.cmbCurrFX_DC.Location = new System.Drawing.Point(115, 98);
            this.cmbCurrFX_DC.Name = "cmbCurrFX_DC";
            this.cmbCurrFX_DC.Size = new System.Drawing.Size(84, 21);
            this.cmbCurrFX_DC.TabIndex = 1152;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 1151;
            this.label3.Text = "Νόμισμα D/C";
            // 
            // cmbTypeFX
            // 
            this.cmbTypeFX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeFX.FormattingEnabled = true;
            this.cmbTypeFX.Items.AddRange(new object[] {
            "Spot Rate",
            "Limit"});
            this.cmbTypeFX.Location = new System.Drawing.Point(115, 35);
            this.cmbTypeFX.Name = "cmbTypeFX";
            this.cmbTypeFX.Size = new System.Drawing.Size(84, 21);
            this.cmbTypeFX.TabIndex = 1148;
            // 
            // Label37
            // 
            this.Label37.AutoSize = true;
            this.Label37.Location = new System.Drawing.Point(34, 40);
            this.Label37.Name = "Label37";
            this.Label37.Size = new System.Drawing.Size(31, 13);
            this.Label37.TabIndex = 1147;
            this.Label37.Text = "Type";
            // 
            // cmbCurrFX_C
            // 
            this.cmbCurrFX_C.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrFX_C.FormattingEnabled = true;
            this.cmbCurrFX_C.Location = new System.Drawing.Point(115, 77);
            this.cmbCurrFX_C.Name = "cmbCurrFX_C";
            this.cmbCurrFX_C.Size = new System.Drawing.Size(84, 21);
            this.cmbCurrFX_C.TabIndex = 1150;
            // 
            // Label36
            // 
            this.Label36.AutoSize = true;
            this.Label36.Location = new System.Drawing.Point(36, 81);
            this.Label36.Name = "Label36";
            this.Label36.Size = new System.Drawing.Size(75, 13);
            this.Label36.TabIndex = 1146;
            this.Label36.Text = "Νόμισμα CRD";
            // 
            // cmbCurrFX_D
            // 
            this.cmbCurrFX_D.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrFX_D.FormattingEnabled = true;
            this.cmbCurrFX_D.Location = new System.Drawing.Point(115, 56);
            this.cmbCurrFX_D.Name = "cmbCurrFX_D";
            this.cmbCurrFX_D.Size = new System.Drawing.Size(84, 21);
            this.cmbCurrFX_D.TabIndex = 1149;
            // 
            // Label35
            // 
            this.Label35.AutoSize = true;
            this.Label35.Location = new System.Drawing.Point(35, 60);
            this.Label35.Name = "Label35";
            this.Label35.Size = new System.Drawing.Size(74, 13);
            this.Label35.TabIndex = 1145;
            this.Label35.Text = "Νόμισμα DBT";
            // 
            // Label29
            // 
            this.Label29.AutoSize = true;
            this.Label29.Location = new System.Drawing.Point(3, 10);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(80, 13);
            this.Label29.TabIndex = 1143;
            this.Label29.Text = "Ημερ/νίες από";
            // 
            // fgList
            // 
            this.fgList.AllowEditing = false;
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(7, 172);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 2;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Rows.Fixed = 2;
            this.fgList.Size = new System.Drawing.Size(1205, 451);
            this.fgList.TabIndex = 1162;
            // 
            // ucCS
            // 
            this.ucCS.BackColor = System.Drawing.Color.Transparent;
            this.ucCS.CodesList = null;
            this.ucCS.Filters = "Client_ID > 0 AND Status = 1";
            this.ucCS.ListType = 0;
            this.ucCS.Location = new System.Drawing.Point(67, 14);
            this.ucCS.Mode = 0;
            this.ucCS.Name = "ucCS";
            this.ucCS.ShowClientsList = true;
            this.ucCS.ShowHeight = 0;
            this.ucCS.ShowWidth = 0;
            this.ucCS.Size = new System.Drawing.Size(200, 20);
            this.ucCS.TabIndex = 1159;
            // 
            // ucDC
            // 
            this.ucDC.BackColor = System.Drawing.Color.Transparent;
            this.ucDC.DateFrom = new System.DateTime(2020, 10, 2, 23, 59, 59, 190);
            this.ucDC.DateTo = new System.DateTime(2020, 10, 2, 23, 59, 59, 190);
            this.ucDC.Location = new System.Drawing.Point(90, 3);
            this.ucDC.Name = "ucDC";
            this.ucDC.Size = new System.Drawing.Size(210, 22);
            this.ucDC.TabIndex = 1144;
            // 
            // frmDailyLL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SandyBrown;
            this.ClientSize = new System.Drawing.Size(1317, 689);
            this.Controls.Add(this.ucCS);
            this.Controls.Add(this.panFilters);
            this.Controls.Add(this.panSearch);
            this.Controls.Add(this.fgList);
            this.Controls.Add(this.panDaily);
            this.Controls.Add(this.toolLeft);
            this.Name = "frmDailyLL";
            this.Text = "frmDailyLL";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmDailyLL_Load);
            this.toolLeft.ResumeLayout(false);
            this.toolLeft.PerformLayout();
            this.mnuContext.ResumeLayout(false);
            this.panDaily.ResumeLayout(false);
            this.panDaily.PerformLayout();
            this.panFilters.ResumeLayout(false);
            this.panFilters.PerformLayout();
            this.panSearch.ResumeLayout(false);
            this.panSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgClientsChoiced)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmptyClient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.ToolStrip toolLeft;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel2;
        internal System.Windows.Forms.ToolStripButton tsbPrint;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        internal System.Windows.Forms.ToolStripButton tsbHelp;
        internal System.Windows.Forms.ImageList imgFile;
        internal System.Windows.Forms.ContextMenuStrip mnuContext;
        internal System.Windows.Forms.ToolStripMenuItem CustomerData;
        internal System.Windows.Forms.ToolStripMenuItem NewCommand;
        internal System.Windows.Forms.Label lblCustomer;
        internal System.Windows.Forms.Button btnCleanUp;
        internal System.Windows.Forms.DateTimePicker dToday;
        internal System.Windows.Forms.LinkLabel lnkPortfolio;
        internal System.Windows.Forms.LinkLabel lnkPelatis;
        internal System.Windows.Forms.Label lblCode;
        internal System.Windows.Forms.Label lblContract;
        internal System.Windows.Forms.DateTimePicker dPeriodEnd;
        internal System.Windows.Forms.Label Label19;
        internal System.Windows.Forms.DateTimePicker dPeriodStart;
        internal System.Windows.Forms.Label Label38;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.ComboBox cmbCashAccounts;
        internal System.Windows.Forms.TextBox txtAmount;
        internal System.Windows.Forms.ComboBox cmbCurr;
        internal System.Windows.Forms.TextBox txtClientsGrossRate;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Panel panDaily;
        private Core.ucContractsSearch ucCS;
        internal System.Windows.Forms.Panel panFilters;
        internal System.Windows.Forms.ComboBox cmbDiax;
        internal System.Windows.Forms.Button btnSearch;
        internal System.Windows.Forms.CheckBox chkShowCancelled;
        internal System.Windows.Forms.Label label24;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label lblSender;
        internal System.Windows.Forms.ComboBox cmbDivisions;
        internal System.Windows.Forms.Label lblProvider;
        internal System.Windows.Forms.ComboBox cmbSent;
        internal System.Windows.Forms.Label lblAdvisor;
        internal System.Windows.Forms.ComboBox cmbProviders;
        internal System.Windows.Forms.Label lblExecute;
        internal System.Windows.Forms.Label lblSended;
        internal System.Windows.Forms.ComboBox cmbAdvisors;
        internal System.Windows.Forms.ComboBox cmbActions;
        internal System.Windows.Forms.ComboBox cmbUsers;
        private System.Windows.Forms.Panel panSearch;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgClientsChoiced;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.PictureBox picEmptyClient;
        internal System.Windows.Forms.Label label25;
        internal System.Windows.Forms.ComboBox cmbCurrFX_DC;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.ComboBox cmbTypeFX;
        internal System.Windows.Forms.Label Label37;
        internal System.Windows.Forms.ComboBox cmbCurrFX_C;
        internal System.Windows.Forms.Label Label36;
        internal System.Windows.Forms.ComboBox cmbCurrFX_D;
        internal System.Windows.Forms.Label Label35;
        private Core.ucDoubleCalendar ucDC;
        internal System.Windows.Forms.Label Label29;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
    }
}