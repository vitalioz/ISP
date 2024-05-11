namespace Contracts
{
    partial class ucOfficialInforming_Commands
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucOfficialInforming_Commands));
            this.mnuCommandData = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClientData = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContractData = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuViewStatement = new System.Windows.Forms.ToolStripMenuItem();
            this.FolderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panEditCommandData = new System.Windows.Forms.Panel();
            this.Label9 = new System.Windows.Forms.Label();
            this.lblInformingMethod = new System.Windows.Forms.Label();
            this.txtStatement_FileName = new System.Windows.Forms.TextBox();
            this.Label15 = new System.Windows.Forms.Label();
            this.picStatement_Show = new System.Windows.Forms.PictureBox();
            this.picStatement_FilePath = new System.Windows.Forms.PictureBox();
            this.lblInformingClientData = new System.Windows.Forms.Label();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.Label14 = new System.Windows.Forms.Label();
            this.txtInforming_Notes = new System.Windows.Forms.TextBox();
            this.lblInformingDate = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.chkCommands = new System.Windows.Forms.CheckBox();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.Label7 = new System.Windows.Forms.Label();
            this.dTo = new System.Windows.Forms.DateTimePicker();
            this.dFrom = new System.Windows.Forms.DateTimePicker();
            this.toolCommands = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel10 = new System.Windows.Forms.ToolStripLabel();
            this.tsbEditProposal = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSend = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPrint = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.cmbProviders = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.lblProfitCenter = new System.Windows.Forms.Label();
            this.lnkPelatis = new System.Windows.Forms.LinkLabel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblCode = new System.Windows.Forms.Label();
            this.picEmptyClient = new System.Windows.Forms.PictureBox();
            this.ucCS = new Core.ucContractsSearch();
            this.txtThema = new System.Windows.Forms.TextBox();
            this.mnuContext.SuspendLayout();
            this.panEditCommandData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStatement_Show)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStatement_FilePath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.toolCommands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEmptyClient)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuCommandData
            // 
            this.mnuCommandData.Name = "mnuCommandData";
            this.mnuCommandData.Size = new System.Drawing.Size(185, 22);
            this.mnuCommandData.Text = "Στοιχεία της εντολής";
            this.mnuCommandData.Click += new System.EventHandler(this.mnuCommandData_Click);
            // 
            // mnuClientData
            // 
            this.mnuClientData.Name = "mnuClientData";
            this.mnuClientData.Size = new System.Drawing.Size(185, 22);
            this.mnuClientData.Text = "Στοιχεία τού πελάτη";
            this.mnuClientData.Click += new System.EventHandler(this.mnuClientData_Click);
            // 
            // mnuContractData
            // 
            this.mnuContractData.Name = "mnuContractData";
            this.mnuContractData.Size = new System.Drawing.Size(185, 22);
            this.mnuContractData.Text = "Στοιχεία σύμβασης";
            this.mnuContractData.Click += new System.EventHandler(this.mnuContractData_Click);
            // 
            // mnuContext
            // 
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContractData,
            this.mnuClientData,
            this.mnuCommandData,
            this.mnuViewStatement});
            this.mnuContext.Name = "mnuContext";
            this.mnuContext.Size = new System.Drawing.Size(186, 92);
            // 
            // mnuViewStatement
            // 
            this.mnuViewStatement.Name = "mnuViewStatement";
            this.mnuViewStatement.Size = new System.Drawing.Size(185, 22);
            this.mnuViewStatement.Text = "Προβολή Πινακιδίου";
            this.mnuViewStatement.Click += new System.EventHandler(this.mnuViewStatement_Click);
            // 
            // FolderBrowserDialog1
            // 
            this.FolderBrowserDialog1.SelectedPath = "C:\\";
            // 
            // panEditCommandData
            // 
            this.panEditCommandData.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.panEditCommandData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panEditCommandData.Controls.Add(this.txtThema);
            this.panEditCommandData.Controls.Add(this.Label9);
            this.panEditCommandData.Controls.Add(this.lblInformingMethod);
            this.panEditCommandData.Controls.Add(this.txtStatement_FileName);
            this.panEditCommandData.Controls.Add(this.Label15);
            this.panEditCommandData.Controls.Add(this.picStatement_Show);
            this.panEditCommandData.Controls.Add(this.picStatement_FilePath);
            this.panEditCommandData.Controls.Add(this.lblInformingClientData);
            this.panEditCommandData.Controls.Add(this.Label13);
            this.panEditCommandData.Controls.Add(this.Label10);
            this.panEditCommandData.Controls.Add(this.btnSave);
            this.panEditCommandData.Controls.Add(this.btnCancel);
            this.panEditCommandData.Controls.Add(this.Label14);
            this.panEditCommandData.Controls.Add(this.txtInforming_Notes);
            this.panEditCommandData.Controls.Add(this.lblInformingDate);
            this.panEditCommandData.Controls.Add(this.Label8);
            this.panEditCommandData.Location = new System.Drawing.Point(422, 204);
            this.panEditCommandData.Name = "panEditCommandData";
            this.panEditCommandData.Size = new System.Drawing.Size(559, 329);
            this.panEditCommandData.TabIndex = 1051;
            this.panEditCommandData.Visible = false;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(14, 168);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(33, 13);
            this.Label9.TabIndex = 374;
            this.Label9.Text = "Θέμα";
            // 
            // lblInformingMethod
            // 
            this.lblInformingMethod.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInformingMethod.Location = new System.Drawing.Point(126, 50);
            this.lblInformingMethod.Name = "lblInformingMethod";
            this.lblInformingMethod.Size = new System.Drawing.Size(226, 21);
            this.lblInformingMethod.TabIndex = 372;
            // 
            // txtStatement_FileName
            // 
            this.txtStatement_FileName.Location = new System.Drawing.Point(125, 189);
            this.txtStatement_FileName.Name = "txtStatement_FileName";
            this.txtStatement_FileName.Size = new System.Drawing.Size(351, 20);
            this.txtStatement_FileName.TabIndex = 302;
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(14, 193);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(53, 13);
            this.Label15.TabIndex = 367;
            this.Label15.Text = "Πινακίδιο";
            // 
            // picStatement_Show
            // 
            this.picStatement_Show.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picStatement_Show.Image = global::Contracts.Properties.Resources.eye;
            this.picStatement_Show.Location = new System.Drawing.Point(509, 192);
            this.picStatement_Show.Name = "picStatement_Show";
            this.picStatement_Show.Size = new System.Drawing.Size(24, 19);
            this.picStatement_Show.TabIndex = 366;
            this.picStatement_Show.TabStop = false;
            this.picStatement_Show.Click += new System.EventHandler(this.picStatement_Show_Click);
            // 
            // picStatement_FilePath
            // 
            this.picStatement_FilePath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picStatement_FilePath.Image = global::Contracts.Properties.Resources.FindFolder;
            this.picStatement_FilePath.Location = new System.Drawing.Point(482, 187);
            this.picStatement_FilePath.Name = "picStatement_FilePath";
            this.picStatement_FilePath.Size = new System.Drawing.Size(24, 24);
            this.picStatement_FilePath.TabIndex = 365;
            this.picStatement_FilePath.TabStop = false;
            this.picStatement_FilePath.Click += new System.EventHandler(this.picStatement_FilePath_Click);
            // 
            // lblInformingClientData
            // 
            this.lblInformingClientData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInformingClientData.Location = new System.Drawing.Point(126, 78);
            this.lblInformingClientData.Name = "lblInformingClientData";
            this.lblInformingClientData.Size = new System.Drawing.Size(351, 80);
            this.lblInformingClientData.TabIndex = 363;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(14, 81);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(92, 13);
            this.Label13.TabIndex = 362;
            this.Label13.Text = "Στοιχεία επικ/ας";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(14, 53);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(109, 13);
            this.Label10.TabIndex = 360;
            this.Label10.Text = "Τρόπος Ενημέρωσης";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = global::Contracts.Properties.Resources.save;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(148, 288);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 26);
            this.btnSave.TabIndex = 308;
            this.btnSave.Text = "Αποθήκευση";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Image = global::Contracts.Properties.Resources.cancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(324, 288);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 26);
            this.btnCancel.TabIndex = 310;
            this.btnCancel.Text = "Άκυρο";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(14, 221);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(71, 13);
            this.Label14.TabIndex = 357;
            this.Label14.Text = "Παρατήρηση";
            // 
            // txtInforming_Notes
            // 
            this.txtInforming_Notes.ForeColor = System.Drawing.Color.Black;
            this.txtInforming_Notes.Location = new System.Drawing.Point(125, 219);
            this.txtInforming_Notes.Multiline = true;
            this.txtInforming_Notes.Name = "txtInforming_Notes";
            this.txtInforming_Notes.Size = new System.Drawing.Size(351, 48);
            this.txtInforming_Notes.TabIndex = 306;
            // 
            // lblInformingDate
            // 
            this.lblInformingDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInformingDate.Location = new System.Drawing.Point(126, 22);
            this.lblInformingDate.Name = "lblInformingDate";
            this.lblInformingDate.Size = new System.Drawing.Size(93, 21);
            this.lblInformingDate.TabIndex = 354;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(14, 27);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(98, 13);
            this.Label8.TabIndex = 353;
            this.Label8.Text = "Ημερ.Ενημέρωσης";
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(9, 50);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(51, 13);
            this.Label11.TabIndex = 1042;
            this.Label11.Text = "Πάροχος";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(192, 20);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(27, 13);
            this.Label2.TabIndex = 1041;
            this.Label2.Text = "εώς";
            // 
            // chkCommands
            // 
            this.chkCommands.AutoSize = true;
            this.chkCommands.Location = new System.Drawing.Point(13, 107);
            this.chkCommands.Name = "chkCommands";
            this.chkCommands.Size = new System.Drawing.Size(15, 14);
            this.chkCommands.TabIndex = 1037;
            this.chkCommands.UseVisualStyleBackColor = true;
            this.chkCommands.CheckedChanged += new System.EventHandler(this.chkCommands_CheckedChanged);
            // 
            // fgList
            // 
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(8, 104);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(1261, 568);
            this.fgList.TabIndex = 1036;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(10, 20);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(80, 13);
            this.Label7.TabIndex = 1040;
            this.Label7.Text = "Ημερ/νίες από";
            // 
            // dTo
            // 
            this.dTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dTo.Location = new System.Drawing.Point(225, 16);
            this.dTo.Name = "dTo";
            this.dTo.Size = new System.Drawing.Size(93, 20);
            this.dTo.TabIndex = 1039;
            // 
            // dFrom
            // 
            this.dFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dFrom.Location = new System.Drawing.Point(91, 16);
            this.dFrom.Name = "dFrom";
            this.dFrom.Size = new System.Drawing.Size(93, 20);
            this.dFrom.TabIndex = 1038;
            // 
            // toolCommands
            // 
            this.toolCommands.AutoSize = false;
            this.toolCommands.BackColor = System.Drawing.Color.Gainsboro;
            this.toolCommands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolCommands.Dock = System.Windows.Forms.DockStyle.None;
            this.toolCommands.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolCommands.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolCommands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripLabel10,
            this.tsbEditProposal,
            this.ToolStripSeparator23,
            this.tsbSend,
            this.ToolStripSeparator1,
            this.tsbPrint,
            this.ToolStripSeparator2,
            this.tsbRefresh});
            this.toolCommands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolCommands.Location = new System.Drawing.Point(10, 76);
            this.toolCommands.Name = "toolCommands";
            this.toolCommands.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolCommands.Size = new System.Drawing.Size(138, 25);
            this.toolCommands.TabIndex = 1050;
            this.toolCommands.Text = "ToolStrip1";
            // 
            // ToolStripLabel10
            // 
            this.ToolStripLabel10.Name = "ToolStripLabel10";
            this.ToolStripLabel10.Size = new System.Drawing.Size(10, 22);
            this.ToolStripLabel10.Text = " ";
            // 
            // tsbEditProposal
            // 
            this.tsbEditProposal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEditProposal.Image = global::Contracts.Properties.Resources.edit;
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
            // tsbSend
            // 
            this.tsbSend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSend.Image = global::Contracts.Properties.Resources.emailicon;
            this.tsbSend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSend.Name = "tsbSend";
            this.tsbSend.Size = new System.Drawing.Size(23, 22);
            this.tsbSend.Text = "Αποστολή";
            this.tsbSend.Click += new System.EventHandler(this.tsbSend_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbPrint
            // 
            this.tsbPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPrint.Image = global::Contracts.Properties.Resources.PrintHS;
            this.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Size = new System.Drawing.Size(23, 22);
            this.tsbPrint.Text = "Εκτύπωση";
            this.tsbPrint.Click += new System.EventHandler(this.tsbPrint_Click);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRefresh.Image = global::Contracts.Properties.Resources.refresh;
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(23, 22);
            this.tsbRefresh.Text = "Refresh";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // cmbProviders
            // 
            this.cmbProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProviders.FormattingEnabled = true;
            this.cmbProviders.Location = new System.Drawing.Point(89, 47);
            this.cmbProviders.Name = "cmbProviders";
            this.cmbProviders.Size = new System.Drawing.Size(253, 21);
            this.cmbProviders.TabIndex = 1043;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(382, 16);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(50, 13);
            this.Label1.TabIndex = 1044;
            this.Label1.Text = "Πελάτης";
            // 
            // lblProfitCenter
            // 
            this.lblProfitCenter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProfitCenter.Location = new System.Drawing.Point(841, 38);
            this.lblProfitCenter.Name = "lblProfitCenter";
            this.lblProfitCenter.Size = new System.Drawing.Size(180, 20);
            this.lblProfitCenter.TabIndex = 1049;
            // 
            // lnkPelatis
            // 
            this.lnkPelatis.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lnkPelatis.Location = new System.Drawing.Point(554, 39);
            this.lnkPelatis.Name = "lnkPelatis";
            this.lnkPelatis.Size = new System.Drawing.Size(281, 20);
            this.lnkPelatis.TabIndex = 1048;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(1136, 31);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(91, 27);
            this.btnSearch.TabIndex = 1046;
            this.btnSearch.Text = "Αναζήτηση";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblCode
            // 
            this.lblCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCode.Location = new System.Drawing.Point(438, 39);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(110, 20);
            this.lblCode.TabIndex = 1047;
            // 
            // picEmptyClient
            // 
            this.picEmptyClient.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picEmptyClient.Image = global::Contracts.Properties.Resources.cleanup;
            this.picEmptyClient.Location = new System.Drawing.Point(643, 12);
            this.picEmptyClient.Name = "picEmptyClient";
            this.picEmptyClient.Size = new System.Drawing.Size(22, 21);
            this.picEmptyClient.TabIndex = 1045;
            this.picEmptyClient.TabStop = false;
            this.picEmptyClient.Click += new System.EventHandler(this.picEmptyClient_Click);
            // 
            // ucCS
            // 
            this.ucCS.BackColor = System.Drawing.Color.Transparent;
            this.ucCS.CodesList = null;
            this.ucCS.Filters = "Client_ID > 0 AND Status = 1";
            this.ucCS.ListType = 0;
            this.ucCS.Location = new System.Drawing.Point(438, 13);
            this.ucCS.Mode = 0;
            this.ucCS.Name = "ucCS";
            this.ucCS.ShowClientsList = true;
            this.ucCS.ShowHeight = 0;
            this.ucCS.ShowWidth = 0;
            this.ucCS.Size = new System.Drawing.Size(200, 20);
            this.ucCS.TabIndex = 1052;
            // 
            // txtThema
            // 
            this.txtThema.Location = new System.Drawing.Point(125, 164);
            this.txtThema.Name = "txtThema";
            this.txtThema.Size = new System.Drawing.Size(351, 20);
            this.txtThema.TabIndex = 300;
            // 
            // ucOfficialInforming_Commands
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Salmon;
            this.Controls.Add(this.ucCS);
            this.Controls.Add(this.panEditCommandData);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.chkCommands);
            this.Controls.Add(this.fgList);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.dTo);
            this.Controls.Add(this.dFrom);
            this.Controls.Add(this.toolCommands);
            this.Controls.Add(this.cmbProviders);
            this.Controls.Add(this.picEmptyClient);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.lblProfitCenter);
            this.Controls.Add(this.lnkPelatis);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lblCode);
            this.Name = "ucOfficialInforming_Commands";
            this.Size = new System.Drawing.Size(1285, 674);
            this.Load += new System.EventHandler(this.ucOfficialInforming_Commands_Load);
            this.mnuContext.ResumeLayout(false);
            this.panEditCommandData.ResumeLayout(false);
            this.panEditCommandData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStatement_Show)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStatement_FilePath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.toolCommands.ResumeLayout(false);
            this.toolCommands.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEmptyClient)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStripMenuItem mnuCommandData;
        internal System.Windows.Forms.ToolStripMenuItem mnuClientData;
        internal System.Windows.Forms.ToolStripMenuItem mnuContractData;
        internal System.Windows.Forms.ContextMenuStrip mnuContext;
        internal System.Windows.Forms.ToolStripMenuItem mnuViewStatement;
        internal System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog1;
        internal System.Windows.Forms.Panel panEditCommandData;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label lblInformingMethod;
        internal System.Windows.Forms.TextBox txtStatement_FileName;
        internal System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.PictureBox picStatement_Show;
        internal System.Windows.Forms.PictureBox picStatement_FilePath;
        internal System.Windows.Forms.Label lblInformingClientData;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.TextBox txtInforming_Notes;
        internal System.Windows.Forms.Label lblInformingDate;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.CheckBox chkCommands;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.DateTimePicker dTo;
        internal System.Windows.Forms.DateTimePicker dFrom;
        internal System.Windows.Forms.ToolStrip toolCommands;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel10;
        internal System.Windows.Forms.ToolStripButton tsbEditProposal;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator23;
        internal System.Windows.Forms.ToolStripButton tsbSend;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripButton tsbPrint;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        internal System.Windows.Forms.ToolStripButton tsbRefresh;
        internal System.Windows.Forms.ComboBox cmbProviders;
        internal System.Windows.Forms.PictureBox picEmptyClient;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label lblProfitCenter;
        internal System.Windows.Forms.LinkLabel lnkPelatis;
        internal System.Windows.Forms.Button btnSearch;
        internal System.Windows.Forms.Label lblCode;
        private Core.ucContractsSearch ucCS;
        internal System.Windows.Forms.TextBox txtThema;
    }
}
