namespace Contracts
{
    partial class ucOfficialInforming_AdminFees
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucOfficialInforming_AdminFees));
            this.txtInvoice = new System.Windows.Forms.TextBox();
            this.Label18 = new System.Windows.Forms.Label();
            this.chkList = new System.Windows.Forms.CheckBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.rb4 = new System.Windows.Forms.RadioButton();
            this.rb3 = new System.Windows.Forms.RadioButton();
            this.picShowInvoice = new System.Windows.Forms.PictureBox();
            this.picAttachedInvoice = new System.Windows.Forms.PictureBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.tsbSend = new System.Windows.Forms.ToolStripButton();
            this.lblInformingMethod = new System.Windows.Forms.Label();
            this.tsbPrint = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.lblInformingClientData = new System.Windows.Forms.Label();
            this.Label23 = new System.Windows.Forms.Label();
            this.Label24 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.mnuClientData = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContractData = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuViewInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.picCleanUp = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.Label4 = new System.Windows.Forms.Label();
            this.cmbProviders = new System.Windows.Forms.ComboBox();
            this.lblInformingDate = new System.Windows.Forms.Label();
            this.rb2 = new System.Windows.Forms.RadioButton();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.rb1 = new System.Windows.Forms.RadioButton();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label27 = new System.Windows.Forms.Label();
            this.panEditData = new System.Windows.Forms.Panel();
            this.lblCode = new System.Windows.Forms.Label();
            this.lblProfitCenter = new System.Windows.Forms.Label();
            this.lnkPelatis = new System.Windows.Forms.LinkLabel();
            this.Label25 = new System.Windows.Forms.Label();
            this.toolAdminFees = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsbEdit = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ucCS = new Core.ucContractsSearch();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picShowInvoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAttachedInvoice)).BeginInit();
            this.mnuContext.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCleanUp)).BeginInit();
            this.panEditData.SuspendLayout();
            this.toolAdminFees.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtInvoice
            // 
            this.txtInvoice.Location = new System.Drawing.Point(127, 161);
            this.txtInvoice.Name = "txtInvoice";
            this.txtInvoice.Size = new System.Drawing.Size(351, 20);
            this.txtInvoice.TabIndex = 300;
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Location = new System.Drawing.Point(16, 165);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(97, 13);
            this.Label18.TabIndex = 374;
            this.Label18.Text = "Αρχείο Τιμολογίου";
            // 
            // chkList
            // 
            this.chkList.AutoSize = true;
            this.chkList.Location = new System.Drawing.Point(10, 90);
            this.chkList.Name = "chkList";
            this.chkList.Size = new System.Drawing.Size(15, 14);
            this.chkList.TabIndex = 1082;
            this.chkList.UseVisualStyleBackColor = true;
            this.chkList.CheckedChanged += new System.EventHandler(this.chkList_CheckedChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(1046, 24);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(91, 27);
            this.btnSearch.TabIndex = 1081;
            this.btnSearch.Text = "Αναζήτηση";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // fgList
            // 
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(4, 87);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(1277, 506);
            this.fgList.SubtotalPosition = C1.Win.C1FlexGrid.SubtotalPositionEnum.BelowData;
            this.fgList.TabIndex = 1080;
            // 
            // rb4
            // 
            this.rb4.AutoSize = true;
            this.rb4.Location = new System.Drawing.Point(301, 6);
            this.rb4.Name = "rb4";
            this.rb4.Size = new System.Drawing.Size(35, 17);
            this.rb4.TabIndex = 1079;
            this.rb4.TabStop = true;
            this.rb4.Text = "IV";
            this.rb4.UseVisualStyleBackColor = true;
            // 
            // rb3
            // 
            this.rb3.AutoSize = true;
            this.rb3.Location = new System.Drawing.Point(264, 6);
            this.rb3.Name = "rb3";
            this.rb3.Size = new System.Drawing.Size(34, 17);
            this.rb3.TabIndex = 1078;
            this.rb3.TabStop = true;
            this.rb3.Text = "III";
            this.rb3.UseVisualStyleBackColor = true;
            // 
            // picShowInvoice
            // 
            this.picShowInvoice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picShowInvoice.Image = global::Contracts.Properties.Resources.eye;
            this.picShowInvoice.Location = new System.Drawing.Point(510, 163);
            this.picShowInvoice.Name = "picShowInvoice";
            this.picShowInvoice.Size = new System.Drawing.Size(21, 19);
            this.picShowInvoice.TabIndex = 376;
            this.picShowInvoice.TabStop = false;
            this.picShowInvoice.Click += new System.EventHandler(this.picShowInvoice_Click);
            // 
            // picAttachedInvoice
            // 
            this.picAttachedInvoice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picAttachedInvoice.Image = global::Contracts.Properties.Resources.FindFolder;
            this.picAttachedInvoice.Location = new System.Drawing.Point(484, 159);
            this.picAttachedInvoice.Name = "picAttachedInvoice";
            this.picAttachedInvoice.Size = new System.Drawing.Size(24, 24);
            this.picAttachedInvoice.TabIndex = 375;
            this.picAttachedInvoice.TabStop = false;
            this.picAttachedInvoice.Click += new System.EventHandler(this.picAttachedInvoice_Click);
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(7, 7);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(33, 13);
            this.Label5.TabIndex = 1074;
            this.Label5.Text = "Έτος";
            // 
            // tsbSend
            // 
            this.tsbSend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSend.Image = global::Contracts.Properties.Resources.emailicon1;
            this.tsbSend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSend.Name = "tsbSend";
            this.tsbSend.Size = new System.Drawing.Size(23, 22);
            this.tsbSend.Text = "Αποστολή";
            this.tsbSend.Click += new System.EventHandler(this.tsbSend_Click);
            // 
            // lblInformingMethod
            // 
            this.lblInformingMethod.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInformingMethod.Location = new System.Drawing.Point(128, 48);
            this.lblInformingMethod.Name = "lblInformingMethod";
            this.lblInformingMethod.Size = new System.Drawing.Size(226, 21);
            this.lblInformingMethod.TabIndex = 372;
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
            // ToolStripSeparator4
            // 
            this.ToolStripSeparator4.Name = "ToolStripSeparator4";
            this.ToolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // lblInformingClientData
            // 
            this.lblInformingClientData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInformingClientData.Location = new System.Drawing.Point(128, 76);
            this.lblInformingClientData.Name = "lblInformingClientData";
            this.lblInformingClientData.Size = new System.Drawing.Size(351, 80);
            this.lblInformingClientData.TabIndex = 363;
            // 
            // Label23
            // 
            this.Label23.AutoSize = true;
            this.Label23.Location = new System.Drawing.Point(16, 79);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(92, 13);
            this.Label23.TabIndex = 362;
            this.Label23.Text = "Στοιχεία επικ/ας";
            // 
            // Label24
            // 
            this.Label24.AutoSize = true;
            this.Label24.Location = new System.Drawing.Point(16, 51);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(109, 13);
            this.Label24.TabIndex = 360;
            this.Label24.Text = "Τρόπος Ενημέρωσης";
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
            this.btnSave.Location = new System.Drawing.Point(152, 204);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 26);
            this.btnSave.TabIndex = 308;
            this.btnSave.Text = "Αποθήκευση";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // mnuClientData
            // 
            this.mnuClientData.Name = "mnuClientData";
            this.mnuClientData.Size = new System.Drawing.Size(254, 22);
            this.mnuClientData.Text = "Στοιχεία τού πελάτη";
            this.mnuClientData.Click += new System.EventHandler(this.mnuClientData_Click);
            // 
            // mnuContractData
            // 
            this.mnuContractData.Name = "mnuContractData";
            this.mnuContractData.Size = new System.Drawing.Size(254, 22);
            this.mnuContractData.Text = "Στοιχεία Σύμβασης";
            this.mnuContractData.Click += new System.EventHandler(this.mnuContractData_Click);
            // 
            // mnuContext
            // 
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContractData,
            this.mnuClientData,
            this.mnuViewInvoice});
            this.mnuContext.Name = "mnuContext";
            this.mnuContext.Size = new System.Drawing.Size(255, 70);
            // 
            // mnuViewInvoice
            // 
            this.mnuViewInvoice.Name = "mnuViewInvoice";
            this.mnuViewInvoice.Size = new System.Drawing.Size(254, 22);
            this.mnuViewInvoice.Text = "Προβολή του αρχείου Τιμολογίου";
            this.mnuViewInvoice.Click += new System.EventHandler(this.mnuViewInvoice_Click);
            // 
            // picCleanUp
            // 
            this.picCleanUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picCleanUp.Image = global::Contracts.Properties.Resources.cleanup;
            this.picCleanUp.Location = new System.Drawing.Point(640, 2);
            this.picCleanUp.Name = "picCleanUp";
            this.picCleanUp.Size = new System.Drawing.Size(22, 21);
            this.picCleanUp.TabIndex = 1085;
            this.picCleanUp.TabStop = false;
            this.picCleanUp.Click += new System.EventHandler(this.picCleanUp_Click);
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
            this.btnCancel.Location = new System.Drawing.Point(328, 204);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 26);
            this.btnCancel.TabIndex = 310;
            this.btnCancel.Text = "Άκυρο";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click_1);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(8, 33);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(51, 13);
            this.Label4.TabIndex = 1072;
            this.Label4.Text = "Πάροχος";
            // 
            // cmbProviders
            // 
            this.cmbProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProviders.FormattingEnabled = true;
            this.cmbProviders.Location = new System.Drawing.Point(62, 30);
            this.cmbProviders.Name = "cmbProviders";
            this.cmbProviders.Size = new System.Drawing.Size(272, 21);
            this.cmbProviders.TabIndex = 1071;
            // 
            // lblInformingDate
            // 
            this.lblInformingDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInformingDate.Location = new System.Drawing.Point(128, 20);
            this.lblInformingDate.Name = "lblInformingDate";
            this.lblInformingDate.Size = new System.Drawing.Size(93, 21);
            this.lblInformingDate.TabIndex = 354;
            // 
            // rb2
            // 
            this.rb2.AutoSize = true;
            this.rb2.Location = new System.Drawing.Point(227, 6);
            this.rb2.Name = "rb2";
            this.rb2.Size = new System.Drawing.Size(31, 17);
            this.rb2.TabIndex = 1077;
            this.rb2.TabStop = true;
            this.rb2.Text = "II";
            this.rb2.UseVisualStyleBackColor = true;
            // 
            // cmbYear
            // 
            this.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(62, 3);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(72, 21);
            this.cmbYear.TabIndex = 1073;
            // 
            // rb1
            // 
            this.rb1.AutoSize = true;
            this.rb1.Location = new System.Drawing.Point(193, 6);
            this.rb1.Name = "rb1";
            this.rb1.Size = new System.Drawing.Size(28, 17);
            this.rb1.TabIndex = 1076;
            this.rb1.TabStop = true;
            this.rb1.Text = "I";
            this.rb1.UseVisualStyleBackColor = true;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(147, 7);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(46, 13);
            this.Label6.TabIndex = 1075;
            this.Label6.Text = "Τρίμηνο";
            // 
            // Label27
            // 
            this.Label27.AutoSize = true;
            this.Label27.Location = new System.Drawing.Point(16, 25);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(98, 13);
            this.Label27.TabIndex = 353;
            this.Label27.Text = "Ημερ.Ενημέρωσης";
            // 
            // panEditData
            // 
            this.panEditData.BackColor = System.Drawing.Color.MediumAquamarine;
            this.panEditData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panEditData.Controls.Add(this.picShowInvoice);
            this.panEditData.Controls.Add(this.picAttachedInvoice);
            this.panEditData.Controls.Add(this.txtInvoice);
            this.panEditData.Controls.Add(this.Label18);
            this.panEditData.Controls.Add(this.lblInformingMethod);
            this.panEditData.Controls.Add(this.lblInformingClientData);
            this.panEditData.Controls.Add(this.Label23);
            this.panEditData.Controls.Add(this.Label24);
            this.panEditData.Controls.Add(this.btnSave);
            this.panEditData.Controls.Add(this.btnCancel);
            this.panEditData.Controls.Add(this.lblInformingDate);
            this.panEditData.Controls.Add(this.Label27);
            this.panEditData.Location = new System.Drawing.Point(349, 203);
            this.panEditData.Name = "panEditData";
            this.panEditData.Size = new System.Drawing.Size(554, 253);
            this.panEditData.TabIndex = 1089;
            this.panEditData.Visible = false;
            // 
            // lblCode
            // 
            this.lblCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCode.Location = new System.Drawing.Point(435, 29);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(110, 20);
            this.lblCode.TabIndex = 1086;
            // 
            // lblProfitCenter
            // 
            this.lblProfitCenter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProfitCenter.Location = new System.Drawing.Point(838, 28);
            this.lblProfitCenter.Name = "lblProfitCenter";
            this.lblProfitCenter.Size = new System.Drawing.Size(180, 20);
            this.lblProfitCenter.TabIndex = 1088;
            // 
            // lnkPelatis
            // 
            this.lnkPelatis.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lnkPelatis.Location = new System.Drawing.Point(551, 29);
            this.lnkPelatis.Name = "lnkPelatis";
            this.lnkPelatis.Size = new System.Drawing.Size(281, 20);
            this.lnkPelatis.TabIndex = 1087;
            // 
            // Label25
            // 
            this.Label25.AutoSize = true;
            this.Label25.Location = new System.Drawing.Point(379, 8);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(50, 13);
            this.Label25.TabIndex = 1084;
            this.Label25.Text = "Πελάτης";
            // 
            // toolAdminFees
            // 
            this.toolAdminFees.AutoSize = false;
            this.toolAdminFees.BackColor = System.Drawing.Color.Gainsboro;
            this.toolAdminFees.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolAdminFees.Dock = System.Windows.Forms.DockStyle.None;
            this.toolAdminFees.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolAdminFees.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolAdminFees.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripLabel1,
            this.tsbEdit,
            this.ToolStripSeparator3,
            this.tsbSend,
            this.ToolStripSeparator4,
            this.tsbPrint});
            this.toolAdminFees.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolAdminFees.Location = new System.Drawing.Point(5, 59);
            this.toolAdminFees.Name = "toolAdminFees";
            this.toolAdminFees.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolAdminFees.Size = new System.Drawing.Size(109, 25);
            this.toolAdminFees.TabIndex = 1083;
            this.toolAdminFees.Text = "ToolStrip1";
            // 
            // ToolStripLabel1
            // 
            this.ToolStripLabel1.Name = "ToolStripLabel1";
            this.ToolStripLabel1.Size = new System.Drawing.Size(10, 22);
            this.ToolStripLabel1.Text = " ";
            // 
            // tsbEdit
            // 
            this.tsbEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEdit.Image = global::Contracts.Properties.Resources.edit;
            this.tsbEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEdit.Name = "tsbEdit";
            this.tsbEdit.Size = new System.Drawing.Size(23, 22);
            this.tsbEdit.Text = "Διόρθωση";
            this.tsbEdit.Click += new System.EventHandler(this.tsbEdit_Click);
            // 
            // ToolStripSeparator3
            // 
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // ucCS
            // 
            this.ucCS.BackColor = System.Drawing.Color.Transparent;
            this.ucCS.CodesList = null;
            this.ucCS.Filters = "Client_ID > 0 AND Status = 1";
            this.ucCS.ListType = 0;
            this.ucCS.Location = new System.Drawing.Point(435, 3);
            this.ucCS.Mode = 0;
            this.ucCS.Name = "ucCS";
            this.ucCS.ShowClientsList = true;
            this.ucCS.ShowHeight = 0;
            this.ucCS.ShowWidth = 0;
            this.ucCS.Size = new System.Drawing.Size(200, 20);
            this.ucCS.TabIndex = 1091;
            // 
            // ucOfficialInforming_AdminFees
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSalmon;
            this.Controls.Add(this.panEditData);
            this.Controls.Add(this.chkList);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.fgList);
            this.Controls.Add(this.rb4);
            this.Controls.Add(this.rb3);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.ucCS);
            this.Controls.Add(this.picCleanUp);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.cmbProviders);
            this.Controls.Add(this.rb2);
            this.Controls.Add(this.cmbYear);
            this.Controls.Add(this.rb1);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.lblProfitCenter);
            this.Controls.Add(this.lnkPelatis);
            this.Controls.Add(this.Label25);
            this.Controls.Add(this.toolAdminFees);
            this.Name = "ucOfficialInforming_AdminFees";
            this.Size = new System.Drawing.Size(1285, 674);
            this.Load += new System.EventHandler(this.ucOfficialInforming_AdminFees_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picShowInvoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAttachedInvoice)).EndInit();
            this.mnuContext.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCleanUp)).EndInit();
            this.panEditData.ResumeLayout(false);
            this.panEditData.PerformLayout();
            this.toolAdminFees.ResumeLayout(false);
            this.toolAdminFees.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtInvoice;
        internal System.Windows.Forms.Label Label18;
        internal System.Windows.Forms.CheckBox chkList;
        internal System.Windows.Forms.Button btnSearch;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
        internal System.Windows.Forms.RadioButton rb4;
        internal System.Windows.Forms.RadioButton rb3;
        internal System.Windows.Forms.PictureBox picShowInvoice;
        internal System.Windows.Forms.PictureBox picAttachedInvoice;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.ToolStripButton tsbSend;
        internal System.Windows.Forms.Label lblInformingMethod;
        internal System.Windows.Forms.ToolStripButton tsbPrint;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator4;
        private Core.ucContractsSearch ucCS;
        internal System.Windows.Forms.Label lblInformingClientData;
        internal System.Windows.Forms.Label Label23;
        internal System.Windows.Forms.Label Label24;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.ToolStripMenuItem mnuClientData;
        internal System.Windows.Forms.ToolStripMenuItem mnuContractData;
        internal System.Windows.Forms.ContextMenuStrip mnuContext;
        internal System.Windows.Forms.ToolStripMenuItem mnuViewInvoice;
        internal System.Windows.Forms.PictureBox picCleanUp;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.ComboBox cmbProviders;
        internal System.Windows.Forms.Label lblInformingDate;
        internal System.Windows.Forms.RadioButton rb2;
        internal System.Windows.Forms.ComboBox cmbYear;
        internal System.Windows.Forms.RadioButton rb1;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label27;
        internal System.Windows.Forms.Panel panEditData;
        internal System.Windows.Forms.Label lblCode;
        internal System.Windows.Forms.Label lblProfitCenter;
        internal System.Windows.Forms.LinkLabel lnkPelatis;
        internal System.Windows.Forms.Label Label25;
        internal System.Windows.Forms.ToolStrip toolAdminFees;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel1;
        internal System.Windows.Forms.ToolStripButton tsbEdit;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
    }
}
