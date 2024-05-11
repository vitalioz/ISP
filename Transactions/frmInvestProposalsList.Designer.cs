namespace Transactions
{
    partial class frmInvestProposalsList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInvestProposalsList));
            this.mnuContextActions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmThink = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmNotAgree = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRestore = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.tabInvestIdees = new System.Windows.Forms.TabControl();
            this.tabProtaseis = new System.Windows.Forms.TabPage();
            this.cmbUsers = new System.Windows.Forms.ComboBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.cmbAdvisors = new System.Windows.Forms.ComboBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.chkSend = new System.Windows.Forms.CheckBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.dSend = new System.Windows.Forms.DateTimePicker();
            this.toolProtaseis = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel10 = new System.Windows.Forms.ToolStripLabel();
            this.tsbAddProposal = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEditProposal = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCancelProposal = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSend = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRTO = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExcel = new System.Windows.Forms.ToolStripButton();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tabSimvoules = new System.Windows.Forms.TabPage();
            this.ucPS = new Core.ucProductsSearch();
            this.ucCS = new Core.ucContractsSearch();
            this.chkWait = new System.Windows.Forms.CheckBox();
            this.toolSymvoules = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsbRefreshProposals = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPlayRecievedFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExcelProposals = new System.Windows.Forms.ToolStripButton();
            this.Label6 = new System.Windows.Forms.Label();
            this.lblISIN = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.lblShareTitle = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.dSendTo = new System.Windows.Forms.DateTimePicker();
            this.chkCancel = new System.Windows.Forms.CheckBox();
            this.chkYes = new System.Windows.Forms.CheckBox();
            this.chkNot = new System.Windows.Forms.CheckBox();
            this.chkThink = new System.Windows.Forms.CheckBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.chkNew = new System.Windows.Forms.CheckBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.dSendFrom = new System.Windows.Forms.DateTimePicker();
            this.fgProposals = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.picEmptyShare = new System.Windows.Forms.PictureBox();
            this.picEmptyClient = new System.Windows.Forms.PictureBox();
            this.mnuContextActions.SuspendLayout();
            this.tabInvestIdees.SuspendLayout();
            this.tabProtaseis.SuspendLayout();
            this.toolProtaseis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.tabSimvoules.SuspendLayout();
            this.toolSymvoules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgProposals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmptyShare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmptyClient)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuContextActions
            // 
            this.mnuContextActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmThink,
            this.tsmNotAgree,
            this.tsmRestore,
            this.tsmCancel});
            this.mnuContextActions.Name = "mnuContextActions";
            this.mnuContextActions.Size = new System.Drawing.Size(215, 92);
            // 
            // tsmThink
            // 
            this.tsmThink.Name = "tsmThink";
            this.tsmThink.Size = new System.Drawing.Size(214, 22);
            this.tsmThink.Text = "Σκεπτικός";
            this.tsmThink.Click += new System.EventHandler(this.tsmThink_Click);
            // 
            // tsmNotAgree
            // 
            this.tsmNotAgree.Name = "tsmNotAgree";
            this.tsmNotAgree.Size = new System.Drawing.Size(214, 22);
            this.tsmNotAgree.Text = "Μην αποδοχή συμβουλής";
            this.tsmNotAgree.Click += new System.EventHandler(this.tsmNotAgree_Click);
            // 
            // tsmRestore
            // 
            this.tsmRestore.Name = "tsmRestore";
            this.tsmRestore.Size = new System.Drawing.Size(214, 22);
            this.tsmRestore.Text = "Επαναφορά συμβουλής";
            this.tsmRestore.Click += new System.EventHandler(this.tsmRestore_Click);
            // 
            // tsmCancel
            // 
            this.tsmCancel.Name = "tsmCancel";
            this.tsmCancel.Size = new System.Drawing.Size(214, 22);
            this.tsmCancel.Text = "Άκυρο";
            this.tsmCancel.Click += new System.EventHandler(this.tsmCancel_Click);
            // 
            // tabInvestIdees
            // 
            this.tabInvestIdees.Controls.Add(this.tabProtaseis);
            this.tabInvestIdees.Controls.Add(this.tabSimvoules);
            this.tabInvestIdees.Location = new System.Drawing.Point(4, 4);
            this.tabInvestIdees.Name = "tabInvestIdees";
            this.tabInvestIdees.SelectedIndex = 0;
            this.tabInvestIdees.Size = new System.Drawing.Size(1445, 667);
            this.tabInvestIdees.TabIndex = 1;
            // 
            // tabProtaseis
            // 
            this.tabProtaseis.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tabProtaseis.Controls.Add(this.cmbUsers);
            this.tabProtaseis.Controls.Add(this.Label8);
            this.tabProtaseis.Controls.Add(this.cmbAdvisors);
            this.tabProtaseis.Controls.Add(this.Label12);
            this.tabProtaseis.Controls.Add(this.chkSend);
            this.tabProtaseis.Controls.Add(this.Label2);
            this.tabProtaseis.Controls.Add(this.dSend);
            this.tabProtaseis.Controls.Add(this.toolProtaseis);
            this.tabProtaseis.Controls.Add(this.fgList);
            this.tabProtaseis.Location = new System.Drawing.Point(4, 22);
            this.tabProtaseis.Name = "tabProtaseis";
            this.tabProtaseis.Padding = new System.Windows.Forms.Padding(3);
            this.tabProtaseis.Size = new System.Drawing.Size(1437, 641);
            this.tabProtaseis.TabIndex = 0;
            this.tabProtaseis.Text = "Επενδυτικές Προτάσεις ";
            // 
            // cmbUsers
            // 
            this.cmbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUsers.FormattingEnabled = true;
            this.cmbUsers.Location = new System.Drawing.Point(857, 9);
            this.cmbUsers.Name = "cmbUsers";
            this.cmbUsers.Size = new System.Drawing.Size(310, 21);
            this.cmbUsers.TabIndex = 395;
            this.cmbUsers.SelectedValueChanged += new System.EventHandler(this.cmbUsers_SelectedValueChanged);
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(786, 13);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(69, 13);
            this.Label8.TabIndex = 396;
            this.Label8.Text = "Αποστολέας";
            // 
            // cmbAdvisors
            // 
            this.cmbAdvisors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAdvisors.FormattingEnabled = true;
            this.cmbAdvisors.Location = new System.Drawing.Point(419, 9);
            this.cmbAdvisors.Name = "cmbAdvisors";
            this.cmbAdvisors.Size = new System.Drawing.Size(270, 21);
            this.cmbAdvisors.TabIndex = 390;
            this.cmbAdvisors.SelectedValueChanged += new System.EventHandler(this.cmbAdvisors_SelectedValueChanged);
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(371, 14);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(42, 13);
            this.Label12.TabIndex = 394;
            this.Label12.Text = "Advisor";
            // 
            // chkSend
            // 
            this.chkSend.AutoSize = true;
            this.chkSend.Location = new System.Drawing.Point(14, 73);
            this.chkSend.Name = "chkSend";
            this.chkSend.Size = new System.Drawing.Size(15, 14);
            this.chkSend.TabIndex = 393;
            this.chkSend.UseVisualStyleBackColor = true;
            this.chkSend.CheckedChanged += new System.EventHandler(this.chkSend_CheckedChanged);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(9, 13);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(205, 13);
            this.Label2.TabIndex = 391;
            this.Label2.Text = " Ημερ/νία προγραμματισμού πρότασεις";
            // 
            // dSend
            // 
            this.dSend.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dSend.Location = new System.Drawing.Point(216, 10);
            this.dSend.Name = "dSend";
            this.dSend.Size = new System.Drawing.Size(84, 20);
            this.dSend.TabIndex = 389;
            this.dSend.ValueChanged += new System.EventHandler(this.dSend_ValueChanged);
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
            this.tsbAddProposal,
            this.ToolStripSeparator22,
            this.tsbEditProposal,
            this.ToolStripSeparator23,
            this.tsbCancelProposal,
            this.ToolStripSeparator1,
            this.tsbSend,
            this.ToolStripSeparator2,
            this.tsbRTO,
            this.ToolStripSeparator3,
            this.tsbRefresh,
            this.toolStripSeparator5,
            this.tsbExcel});
            this.toolProtaseis.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolProtaseis.Location = new System.Drawing.Point(9, 42);
            this.toolProtaseis.Name = "toolProtaseis";
            this.toolProtaseis.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolProtaseis.Size = new System.Drawing.Size(234, 25);
            this.toolProtaseis.TabIndex = 392;
            this.toolProtaseis.Text = "ToolStrip1";
            // 
            // ToolStripLabel10
            // 
            this.ToolStripLabel10.Name = "ToolStripLabel10";
            this.ToolStripLabel10.Size = new System.Drawing.Size(10, 22);
            this.ToolStripLabel10.Text = " ";
            // 
            // tsbAddProposal
            // 
            this.tsbAddProposal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddProposal.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddProposal.Image")));
            this.tsbAddProposal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddProposal.Name = "tsbAddProposal";
            this.tsbAddProposal.Size = new System.Drawing.Size(23, 22);
            this.tsbAddProposal.Text = "Προσθήκη";
            this.tsbAddProposal.Click += new System.EventHandler(this.tsbAddProposal_Click);
            // 
            // ToolStripSeparator22
            // 
            this.ToolStripSeparator22.Name = "ToolStripSeparator22";
            this.ToolStripSeparator22.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbEditProposal
            // 
            this.tsbEditProposal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEditProposal.Image = ((System.Drawing.Image)(resources.GetObject("tsbEditProposal.Image")));
            this.tsbEditProposal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEditProposal.Name = "tsbEditProposal";
            this.tsbEditProposal.Size = new System.Drawing.Size(23, 22);
            this.tsbEditProposal.Text = "Διόρθωση";
            this.tsbEditProposal.Click += new System.EventHandler(this.tsbEditProposal_Click);
            // 
            // ToolStripSeparator23
            // 
            this.ToolStripSeparator23.Name = "ToolStripSeparator23";
            this.ToolStripSeparator23.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbCancelProposal
            // 
            this.tsbCancelProposal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCancelProposal.Image = ((System.Drawing.Image)(resources.GetObject("tsbCancelProposal.Image")));
            this.tsbCancelProposal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCancelProposal.Name = "tsbCancelProposal";
            this.tsbCancelProposal.Size = new System.Drawing.Size(23, 22);
            this.tsbCancelProposal.Text = "Ακύρωση";
            this.tsbCancelProposal.Click += new System.EventHandler(this.tsbCancelProposal_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbSend
            // 
            this.tsbSend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSend.Image = ((System.Drawing.Image)(resources.GetObject("tsbSend.Image")));
            this.tsbSend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSend.Name = "tsbSend";
            this.tsbSend.Size = new System.Drawing.Size(23, 22);
            this.tsbSend.Text = "Αποστολή";
            this.tsbSend.Click += new System.EventHandler(this.tsbSend_Click);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbRTO
            // 
            this.tsbRTO.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRTO.Image = ((System.Drawing.Image)(resources.GetObject("tsbRTO.Image")));
            this.tsbRTO.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRTO.Name = "tsbRTO";
            this.tsbRTO.Size = new System.Drawing.Size(23, 22);
            this.tsbRTO.Text = "Ενημερώση RTO";
            this.tsbRTO.Click += new System.EventHandler(this.tsbRTO_Click);
            // 
            // ToolStripSeparator3
            // 
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbExcel
            // 
            this.tsbExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExcel.Image = global::Transactions.Properties.Resources.excel;
            this.tsbExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExcel.Name = "tsbExcel";
            this.tsbExcel.Size = new System.Drawing.Size(23, 22);
            this.tsbExcel.Text = "Εξαγωγή στο  Excel";
            this.tsbExcel.Click += new System.EventHandler(this.tsbExcel_Click);
            // 
            // fgList
            // 
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(6, 70);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(1424, 559);
            this.fgList.TabIndex = 388;
            // 
            // tabSimvoules
            // 
            this.tabSimvoules.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tabSimvoules.Controls.Add(this.ucPS);
            this.tabSimvoules.Controls.Add(this.ucCS);
            this.tabSimvoules.Controls.Add(this.chkWait);
            this.tabSimvoules.Controls.Add(this.toolSymvoules);
            this.tabSimvoules.Controls.Add(this.Label6);
            this.tabSimvoules.Controls.Add(this.lblISIN);
            this.tabSimvoules.Controls.Add(this.Label5);
            this.tabSimvoules.Controls.Add(this.lblShareTitle);
            this.tabSimvoules.Controls.Add(this.Label7);
            this.tabSimvoules.Controls.Add(this.Label1);
            this.tabSimvoules.Controls.Add(this.dSendTo);
            this.tabSimvoules.Controls.Add(this.chkCancel);
            this.tabSimvoules.Controls.Add(this.chkYes);
            this.tabSimvoules.Controls.Add(this.chkNot);
            this.tabSimvoules.Controls.Add(this.chkThink);
            this.tabSimvoules.Controls.Add(this.Label4);
            this.tabSimvoules.Controls.Add(this.chkNew);
            this.tabSimvoules.Controls.Add(this.Label3);
            this.tabSimvoules.Controls.Add(this.dSendFrom);
            this.tabSimvoules.Controls.Add(this.fgProposals);
            this.tabSimvoules.Controls.Add(this.picEmptyShare);
            this.tabSimvoules.Controls.Add(this.picEmptyClient);
            this.tabSimvoules.Location = new System.Drawing.Point(4, 22);
            this.tabSimvoules.Name = "tabSimvoules";
            this.tabSimvoules.Padding = new System.Windows.Forms.Padding(3);
            this.tabSimvoules.Size = new System.Drawing.Size(1437, 641);
            this.tabSimvoules.TabIndex = 1;
            this.tabSimvoules.Text = "Επενδυτικές Συμβουλές";
            // 
            // ucPS
            // 
            this.ucPS.BlockNonRecommended = false;
            this.ucPS.CodesList = null;
            this.ucPS.Filters = "Shares_ID > 0";
            this.ucPS.ListType = 0;
            this.ucPS.Location = new System.Drawing.Point(78, 66);
            this.ucPS.Mode = 0;
            this.ucPS.Name = "ucPS";
            this.ucPS.ProductsContract = null;
            this.ucPS.ShowCancelled = true;
            this.ucPS.ShowHeight = 0;
            this.ucPS.ShowNonAccord = true;
            this.ucPS.ShowProductsList = true;
            this.ucPS.ShowWidth = 0;
            this.ucPS.Size = new System.Drawing.Size(202, 20);
            this.ucPS.TabIndex = 785;
            // 
            // ucCS
            // 
            this.ucCS.BackColor = System.Drawing.Color.Transparent;
            this.ucCS.CodesList = null;
            this.ucCS.Filters = "Client_ID > 0 AND Status = 1";
            this.ucCS.ListType = 0;
            this.ucCS.Location = new System.Drawing.Point(78, 41);
            this.ucCS.Mode = 0;
            this.ucCS.Name = "ucCS";
            this.ucCS.ShowClientsList = true;
            this.ucCS.ShowHeight = 0;
            this.ucCS.ShowWidth = 0;
            this.ucCS.Size = new System.Drawing.Size(200, 20);
            this.ucCS.TabIndex = 784;
            // 
            // chkWait
            // 
            this.chkWait.AutoSize = true;
            this.chkWait.BackColor = System.Drawing.Color.Thistle;
            this.chkWait.Checked = true;
            this.chkWait.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWait.Location = new System.Drawing.Point(806, 21);
            this.chkWait.Name = "chkWait";
            this.chkWait.Size = new System.Drawing.Size(130, 17);
            this.chkWait.TabIndex = 430;
            this.chkWait.Text = "Αναμονή λήψης RTO";
            this.chkWait.UseVisualStyleBackColor = false;
            this.chkWait.CheckedChanged += new System.EventHandler(this.chkWait_CheckedChanged);
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
            this.ToolStripSeparator4,
            this.tsbPlayRecievedFile,
            this.toolStripSeparator6,
            this.tsbExcelProposals});
            this.toolSymvoules.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolSymvoules.Location = new System.Drawing.Point(15, 100);
            this.toolSymvoules.Name = "toolSymvoules";
            this.toolSymvoules.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolSymvoules.Size = new System.Drawing.Size(106, 25);
            this.toolSymvoules.TabIndex = 429;
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
            this.tsbRefreshProposals.Click += new System.EventHandler(this.tsbRefreshProposals_Click);
            // 
            // ToolStripSeparator4
            // 
            this.ToolStripSeparator4.Name = "ToolStripSeparator4";
            this.ToolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbPlayRecievedFile
            // 
            this.tsbPlayRecievedFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPlayRecievedFile.Image = ((System.Drawing.Image)(resources.GetObject("tsbPlayRecievedFile.Image")));
            this.tsbPlayRecievedFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPlayRecievedFile.Name = "tsbPlayRecievedFile";
            this.tsbPlayRecievedFile.Size = new System.Drawing.Size(23, 22);
            this.tsbPlayRecievedFile.Text = "Αναπαραγωγή";
            this.tsbPlayRecievedFile.Click += new System.EventHandler(this.tsbPlayRecievedFile_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbExcelProposals
            // 
            this.tsbExcelProposals.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExcelProposals.Image = global::Transactions.Properties.Resources.excel;
            this.tsbExcelProposals.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExcelProposals.Name = "tsbExcelProposals";
            this.tsbExcelProposals.Size = new System.Drawing.Size(23, 22);
            this.tsbExcelProposals.Text = "Εξαγωγή στο  Excel";
            this.tsbExcelProposals.Click += new System.EventHandler(this.tsbExcelProposals_Click);
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(692, 69);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(28, 13);
            this.Label6.TabIndex = 426;
            this.Label6.Text = "ISIN";
            // 
            // lblISIN
            // 
            this.lblISIN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblISIN.Location = new System.Drawing.Point(724, 66);
            this.lblISIN.Name = "lblISIN";
            this.lblISIN.Size = new System.Drawing.Size(147, 20);
            this.lblISIN.TabIndex = 425;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(308, 69);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(39, 13);
            this.Label5.TabIndex = 424;
            this.Label5.Text = "Τίτλος";
            // 
            // lblShareTitle
            // 
            this.lblShareTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblShareTitle.Location = new System.Drawing.Point(351, 66);
            this.lblShareTitle.Name = "lblShareTitle";
            this.lblShareTitle.Size = new System.Drawing.Size(325, 20);
            this.lblShareTitle.TabIndex = 423;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(12, 69);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(42, 13);
            this.Label7.TabIndex = 421;
            this.Label7.Text = "Προϊον";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 43);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(50, 13);
            this.Label1.TabIndex = 419;
            this.Label1.Text = "Πελάτης";
            // 
            // dSendTo
            // 
            this.dSendTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dSendTo.Location = new System.Drawing.Point(319, 15);
            this.dSendTo.Name = "dSendTo";
            this.dSendTo.Size = new System.Drawing.Size(86, 20);
            this.dSendTo.TabIndex = 418;
            this.dSendTo.ValueChanged += new System.EventHandler(this.dSendTo_ValueChanged);
            // 
            // chkCancel
            // 
            this.chkCancel.AutoSize = true;
            this.chkCancel.BackColor = System.Drawing.Color.Orange;
            this.chkCancel.Checked = true;
            this.chkCancel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCancel.Location = new System.Drawing.Point(1157, 20);
            this.chkCancel.Name = "chkCancel";
            this.chkCancel.Size = new System.Drawing.Size(68, 17);
            this.chkCancel.TabIndex = 417;
            this.chkCancel.Text = "Άκυρο    ";
            this.chkCancel.UseVisualStyleBackColor = false;
            this.chkCancel.CheckedChanged += new System.EventHandler(this.chkCancel_CheckedChanged);
            // 
            // chkYes
            // 
            this.chkYes.AutoSize = true;
            this.chkYes.BackColor = System.Drawing.Color.LightGreen;
            this.chkYes.Checked = true;
            this.chkYes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkYes.Location = new System.Drawing.Point(1065, 20);
            this.chkYes.Name = "chkYes";
            this.chkYes.Size = new System.Drawing.Size(69, 17);
            this.chkYes.TabIndex = 416;
            this.chkYes.Text = "Αποδοχή";
            this.chkYes.UseVisualStyleBackColor = false;
            this.chkYes.CheckedChanged += new System.EventHandler(this.chkYes_CheckedChanged);
            // 
            // chkNot
            // 
            this.chkNot.AutoSize = true;
            this.chkNot.BackColor = System.Drawing.Color.LightCoral;
            this.chkNot.Checked = true;
            this.chkNot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNot.Location = new System.Drawing.Point(952, 21);
            this.chkNot.Name = "chkNot";
            this.chkNot.Size = new System.Drawing.Size(92, 17);
            this.chkNot.TabIndex = 415;
            this.chkNot.Text = "Μην αποδοχή";
            this.chkNot.UseVisualStyleBackColor = false;
            this.chkNot.CheckedChanged += new System.EventHandler(this.chkNot_CheckedChanged);
            // 
            // chkThink
            // 
            this.chkThink.AutoSize = true;
            this.chkThink.BackColor = System.Drawing.Color.Yellow;
            this.chkThink.Checked = true;
            this.chkThink.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkThink.Location = new System.Drawing.Point(707, 21);
            this.chkThink.Name = "chkThink";
            this.chkThink.Size = new System.Drawing.Size(76, 17);
            this.chkThink.TabIndex = 414;
            this.chkThink.Text = "Σκεπτικός";
            this.chkThink.UseVisualStyleBackColor = false;
            this.chkThink.CheckedChanged += new System.EventHandler(this.chkThink_CheckedChanged);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(508, 21);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(66, 13);
            this.Label4.TabIndex = 413;
            this.Label4.Text = "Κατάσταση";
            // 
            // chkNew
            // 
            this.chkNew.AutoSize = true;
            this.chkNew.BackColor = System.Drawing.Color.White;
            this.chkNew.Checked = true;
            this.chkNew.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNew.Location = new System.Drawing.Point(591, 21);
            this.chkNew.Name = "chkNew";
            this.chkNew.Size = new System.Drawing.Size(97, 17);
            this.chkNew.TabIndex = 412;
            this.chkNew.Text = "Νέα συμβουλή";
            this.chkNew.UseVisualStyleBackColor = false;
            this.chkNew.CheckedChanged += new System.EventHandler(this.chkNew_CheckedChanged);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(9, 18);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(205, 13);
            this.Label3.TabIndex = 411;
            this.Label3.Text = " Ημερ/νία προγραμματισμού πρότασεις";
            // 
            // dSendFrom
            // 
            this.dSendFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dSendFrom.Location = new System.Drawing.Point(216, 15);
            this.dSendFrom.Name = "dSendFrom";
            this.dSendFrom.Size = new System.Drawing.Size(88, 20);
            this.dSendFrom.TabIndex = 410;
            this.dSendFrom.ValueChanged += new System.EventHandler(this.dSendFrom_ValueChanged);
            // 
            // fgProposals
            // 
            this.fgProposals.AllowEditing = false;
            this.fgProposals.ColumnInfo = resources.GetString("fgProposals.ColumnInfo");
            this.fgProposals.Location = new System.Drawing.Point(9, 127);
            this.fgProposals.Name = "fgProposals";
            this.fgProposals.Rows.Count = 1;
            this.fgProposals.Rows.DefaultSize = 17;
            this.fgProposals.Size = new System.Drawing.Size(1419, 499);
            this.fgProposals.TabIndex = 409;
            // 
            // picEmptyShare
            // 
            this.picEmptyShare.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picEmptyShare.Image = ((System.Drawing.Image)(resources.GetObject("picEmptyShare.Image")));
            this.picEmptyShare.Location = new System.Drawing.Point(877, 63);
            this.picEmptyShare.Name = "picEmptyShare";
            this.picEmptyShare.Size = new System.Drawing.Size(28, 23);
            this.picEmptyShare.TabIndex = 428;
            this.picEmptyShare.TabStop = false;
            this.picEmptyShare.Click += new System.EventHandler(this.picEmptyShare_Click);
            // 
            // picEmptyClient
            // 
            this.picEmptyClient.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picEmptyClient.Image = ((System.Drawing.Image)(resources.GetObject("picEmptyClient.Image")));
            this.picEmptyClient.Location = new System.Drawing.Point(726, 44);
            this.picEmptyClient.Name = "picEmptyClient";
            this.picEmptyClient.Size = new System.Drawing.Size(28, 22);
            this.picEmptyClient.TabIndex = 427;
            this.picEmptyClient.TabStop = false;
            this.picEmptyClient.Click += new System.EventHandler(this.picEmptyClient_Click);
            // 
            // frmInvestProposalsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1465, 602);
            this.Controls.Add(this.tabInvestIdees);
            this.Name = "frmInvestProposalsList";
            this.Text = "Επενδυτικές Προτάσεις";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmInvestProposalsList_Load);
            this.mnuContextActions.ResumeLayout(false);
            this.tabInvestIdees.ResumeLayout(false);
            this.tabProtaseis.ResumeLayout(false);
            this.tabProtaseis.PerformLayout();
            this.toolProtaseis.ResumeLayout(false);
            this.toolProtaseis.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.tabSimvoules.ResumeLayout(false);
            this.tabSimvoules.PerformLayout();
            this.toolSymvoules.ResumeLayout(false);
            this.toolSymvoules.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgProposals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmptyShare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmptyClient)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.ContextMenuStrip mnuContextActions;
        internal System.Windows.Forms.ToolStripMenuItem tsmThink;
        internal System.Windows.Forms.ToolStripMenuItem tsmNotAgree;
        internal System.Windows.Forms.ToolStripMenuItem tsmRestore;
        internal System.Windows.Forms.ToolStripMenuItem tsmCancel;
        private System.Windows.Forms.TabControl tabInvestIdees;
        private System.Windows.Forms.TabPage tabProtaseis;
        private System.Windows.Forms.TabPage tabSimvoules;
        internal System.Windows.Forms.ComboBox cmbUsers;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.ComboBox cmbAdvisors;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.CheckBox chkSend;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.DateTimePicker dSend;
        internal System.Windows.Forms.ToolStrip toolProtaseis;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel10;
        internal System.Windows.Forms.ToolStripButton tsbAddProposal;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator22;
        internal System.Windows.Forms.ToolStripButton tsbEditProposal;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator23;
        internal System.Windows.Forms.ToolStripButton tsbCancelProposal;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripButton tsbSend;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        internal System.Windows.Forms.ToolStripButton tsbRTO;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
        internal System.Windows.Forms.ToolStripButton tsbRefresh;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
        internal System.Windows.Forms.CheckBox chkWait;
        internal System.Windows.Forms.ToolStrip toolSymvoules;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel1;
        internal System.Windows.Forms.ToolStripButton tsbRefreshProposals;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator4;
        internal System.Windows.Forms.ToolStripButton tsbPlayRecievedFile;
        internal System.Windows.Forms.PictureBox picEmptyShare;
        internal System.Windows.Forms.PictureBox picEmptyClient;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label lblISIN;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label lblShareTitle;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.DateTimePicker dSendTo;
        internal System.Windows.Forms.CheckBox chkCancel;
        internal System.Windows.Forms.CheckBox chkYes;
        internal System.Windows.Forms.CheckBox chkNot;
        internal System.Windows.Forms.CheckBox chkThink;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.CheckBox chkNew;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.DateTimePicker dSendFrom;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgProposals;
        private Core.ucContractsSearch ucCS;
        private Core.ucProductsSearch ucPS;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        internal System.Windows.Forms.ToolStripButton tsbExcel;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        internal System.Windows.Forms.ToolStripButton tsbExcelProposals;
    }
}