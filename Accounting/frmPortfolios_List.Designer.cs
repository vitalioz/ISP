namespace Accounting
{
    partial class frmPortfolios_List
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPortfolios_List));
            this.panCritiries = new System.Windows.Forms.Panel();
            this.dDateControl = new System.Windows.Forms.DateTimePicker();
            this.Label24 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lstAdvisor = new System.Windows.Forms.ComboBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.panTools = new System.Windows.Forms.Panel();
            this.picMin = new System.Windows.Forms.PictureBox();
            this.picMax = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkMiFID2 = new System.Windows.Forms.CheckBox();
            this.toolLeft = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel15 = new System.Windows.Forms.ToolStripLabel();
            this.tsbImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbView = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.panImport = new System.Windows.Forms.Panel();
            this.txtFilePath2_Import = new System.Windows.Forms.TextBox();
            this.picFilesPath2 = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnGetImport = new System.Windows.Forms.Button();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.txtFilePath_Import = new System.Windows.Forms.TextBox();
            this.picFilesPath = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.grdList = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panDetails = new System.Windows.Forms.Panel();
            this.lblContracts_Balances_ID = new System.Windows.Forms.Label();
            this.lblCustodian = new System.Windows.Forms.Label();
            this.lblXAA = new System.Windows.Forms.Label();
            this.lblMiFID_2 = new System.Windows.Forms.Label();
            this.grpNotes = new System.Windows.Forms.GroupBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.grpSpecialInstructions = new System.Windows.Forms.GroupBox();
            this.lblSpecialInstructions = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.grpComplexData = new System.Windows.Forms.GroupBox();
            this.lblComplexData = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panCritiries.SuspendLayout();
            this.panTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMax)).BeginInit();
            this.toolLeft.SuspendLayout();
            this.panImport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFilesPath2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFilesPath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panDetails.SuspendLayout();
            this.grpNotes.SuspendLayout();
            this.grpSpecialInstructions.SuspendLayout();
            this.grpComplexData.SuspendLayout();
            this.SuspendLayout();
            // 
            // panCritiries
            // 
            this.panCritiries.BackColor = System.Drawing.Color.NavajoWhite;
            this.panCritiries.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panCritiries.Controls.Add(this.dDateControl);
            this.panCritiries.Controls.Add(this.Label24);
            this.panCritiries.Controls.Add(this.btnSearch);
            this.panCritiries.Location = new System.Drawing.Point(7, 5);
            this.panCritiries.Name = "panCritiries";
            this.panCritiries.Size = new System.Drawing.Size(1355, 49);
            this.panCritiries.TabIndex = 2091;
            // 
            // dDateControl
            // 
            this.dDateControl.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dDateControl.Location = new System.Drawing.Point(89, 9);
            this.dDateControl.Name = "dDateControl";
            this.dDateControl.Size = new System.Drawing.Size(93, 20);
            this.dDateControl.TabIndex = 2;
            // 
            // Label24
            // 
            this.Label24.AutoSize = true;
            this.Label24.Location = new System.Drawing.Point(17, 12);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(66, 13);
            this.Label24.TabIndex = 1025;
            this.Label24.Text = "Ημερομηνία";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.Control;
            this.btnSearch.Location = new System.Drawing.Point(1245, 9);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(91, 27);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Αναζήτηση";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lstAdvisor
            // 
            this.lstAdvisor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstAdvisor.FormattingEnabled = true;
            this.lstAdvisor.Location = new System.Drawing.Point(220, 6);
            this.lstAdvisor.Name = "lstAdvisor";
            this.lstAdvisor.Size = new System.Drawing.Size(255, 21);
            this.lstAdvisor.TabIndex = 12;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(172, 10);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(42, 13);
            this.Label8.TabIndex = 1029;
            this.Label8.Text = "Advisor";
            // 
            // panTools
            // 
            this.panTools.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panTools.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panTools.Controls.Add(this.picMin);
            this.panTools.Controls.Add(this.picMax);
            this.panTools.Controls.Add(this.comboBox1);
            this.panTools.Controls.Add(this.label7);
            this.panTools.Controls.Add(this.chkMiFID2);
            this.panTools.Controls.Add(this.lstAdvisor);
            this.panTools.Controls.Add(this.toolLeft);
            this.panTools.Controls.Add(this.Label8);
            this.panTools.Location = new System.Drawing.Point(7, 60);
            this.panTools.Name = "panTools";
            this.panTools.Size = new System.Drawing.Size(1355, 34);
            this.panTools.TabIndex = 2092;
            // 
            // picMin
            // 
            this.picMin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picMin.Image = global::Accounting.Properties.Resources.minimise;
            this.picMin.Location = new System.Drawing.Point(1309, 9);
            this.picMin.Name = "picMin";
            this.picMin.Size = new System.Drawing.Size(17, 16);
            this.picMin.TabIndex = 1124;
            this.picMin.TabStop = false;
            this.picMin.Click += new System.EventHandler(this.picMin_Click);
            // 
            // picMax
            // 
            this.picMax.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picMax.Image = global::Accounting.Properties.Resources.maximise;
            this.picMax.Location = new System.Drawing.Point(1332, 9);
            this.picMax.Name = "picMax";
            this.picMax.Size = new System.Drawing.Size(17, 16);
            this.picMax.TabIndex = 1123;
            this.picMax.TabStop = false;
            this.picMax.Click += new System.EventHandler(this.picMax_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Όλα",
            "Μόνο Θετικά",
            "Μόνο Αρνιτικά",
            "Μόνο Μηδενικά"});
            this.comboBox1.Location = new System.Drawing.Point(573, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(127, 21);
            this.comboBox1.TabIndex = 1121;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(508, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 1122;
            this.label7.Text = "Total Value";
            // 
            // chkMiFID2
            // 
            this.chkMiFID2.AutoSize = true;
            this.chkMiFID2.Location = new System.Drawing.Point(732, 9);
            this.chkMiFID2.Name = "chkMiFID2";
            this.chkMiFID2.Size = new System.Drawing.Size(92, 17);
            this.chkMiFID2.TabIndex = 1120;
            this.chkMiFID2.Text = "Μόνο MiFID II";
            this.chkMiFID2.UseVisualStyleBackColor = true;
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
            this.toolStripSeparator2,
            this.tsbView,
            this.toolStripSeparator1,
            this.tsbExcel,
            this.toolStripSeparator15,
            this.tsbHelp});
            this.toolLeft.Location = new System.Drawing.Point(3, 3);
            this.toolLeft.Name = "toolLeft";
            this.toolLeft.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolLeft.Size = new System.Drawing.Size(139, 28);
            this.toolLeft.TabIndex = 255;
            this.toolLeft.Text = "ToolStrip1";
            // 
            // ToolStripLabel15
            // 
            this.ToolStripLabel15.Name = "ToolStripLabel15";
            this.ToolStripLabel15.Size = new System.Drawing.Size(10, 25);
            this.ToolStripLabel15.Text = " ";
            // 
            // tsbImport
            // 
            this.tsbImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbImport.Image = global::Accounting.Properties.Resources.download;
            this.tsbImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImport.Name = "tsbImport";
            this.tsbImport.Size = new System.Drawing.Size(23, 25);
            this.tsbImport.Text = "Εισαγωγή αρχείου";
            this.tsbImport.Click += new System.EventHandler(this.tsbImport_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // tsbView
            // 
            this.tsbView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbView.Image = global::Accounting.Properties.Resources.eye;
            this.tsbView.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbView.Name = "tsbView";
            this.tsbView.Size = new System.Drawing.Size(23, 25);
            this.tsbView.Text = "Portfolio View";
            this.tsbView.Click += new System.EventHandler(this.tsbView_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
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
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(6, 28);
            // 
            // tsbHelp
            // 
            this.tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbHelp.Image")));
            this.tsbHelp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(23, 25);
            this.tsbHelp.Text = "He&lp";
            // 
            // panImport
            // 
            this.panImport.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panImport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panImport.Controls.Add(this.txtFilePath2_Import);
            this.panImport.Controls.Add(this.picFilesPath2);
            this.panImport.Controls.Add(this.label11);
            this.panImport.Controls.Add(this.btnGetImport);
            this.panImport.Controls.Add(this.picClose);
            this.panImport.Controls.Add(this.txtFilePath_Import);
            this.panImport.Controls.Add(this.picFilesPath);
            this.panImport.Controls.Add(this.label6);
            this.panImport.Location = new System.Drawing.Point(8, 97);
            this.panImport.Name = "panImport";
            this.panImport.Size = new System.Drawing.Size(459, 178);
            this.panImport.TabIndex = 2093;
            this.panImport.Visible = false;
            // 
            // txtFilePath2_Import
            // 
            this.txtFilePath2_Import.Location = new System.Drawing.Point(89, 77);
            this.txtFilePath2_Import.Name = "txtFilePath2_Import";
            this.txtFilePath2_Import.Size = new System.Drawing.Size(322, 20);
            this.txtFilePath2_Import.TabIndex = 1067;
            // 
            // picFilesPath2
            // 
            this.picFilesPath2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picFilesPath2.Image = ((System.Drawing.Image)(resources.GetObject("picFilesPath2.Image")));
            this.picFilesPath2.Location = new System.Drawing.Point(414, 75);
            this.picFilesPath2.Name = "picFilesPath2";
            this.picFilesPath2.Size = new System.Drawing.Size(24, 24);
            this.picFilesPath2.TabIndex = 1068;
            this.picFilesPath2.TabStop = false;
            this.picFilesPath2.Click += new System.EventHandler(this.picFilesPath2_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 81);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 13);
            this.label11.TabIndex = 1069;
            this.label11.Text = "Αρχείο HF2S";
            // 
            // btnGetImport
            // 
            this.btnGetImport.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnGetImport.FlatAppearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.btnGetImport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnGetImport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnGetImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetImport.Image = global::Accounting.Properties.Resources.OK;
            this.btnGetImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGetImport.Location = new System.Drawing.Point(190, 131);
            this.btnGetImport.Name = "btnGetImport";
            this.btnGetImport.Size = new System.Drawing.Size(91, 25);
            this.btnGetImport.TabIndex = 1066;
            this.btnGetImport.Text = "OK";
            this.btnGetImport.UseVisualStyleBackColor = false;
            this.btnGetImport.Click += new System.EventHandler(this.btnGetImport_Click);
            // 
            // picClose
            // 
            this.picClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picClose.Image = global::Accounting.Properties.Resources.cancel;
            this.picClose.Location = new System.Drawing.Point(435, 3);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(18, 18);
            this.picClose.TabIndex = 1065;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            // 
            // txtFilePath_Import
            // 
            this.txtFilePath_Import.Location = new System.Drawing.Point(89, 45);
            this.txtFilePath_Import.Name = "txtFilePath_Import";
            this.txtFilePath_Import.Size = new System.Drawing.Size(322, 20);
            this.txtFilePath_Import.TabIndex = 2;
            // 
            // picFilesPath
            // 
            this.picFilesPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picFilesPath.Image = ((System.Drawing.Image)(resources.GetObject("picFilesPath.Image")));
            this.picFilesPath.Location = new System.Drawing.Point(414, 43);
            this.picFilesPath.Name = "picFilesPath";
            this.picFilesPath.Size = new System.Drawing.Size(24, 24);
            this.picFilesPath.TabIndex = 183;
            this.picFilesPath.TabStop = false;
            this.picFilesPath.Click += new System.EventHandler(this.picFilesPath_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 216;
            this.label6.Text = "Αρχείο ALL";
            // 
            // grdList
            // 
            this.grdList.Location = new System.Drawing.Point(8, 101);
            this.grdList.MainView = this.gridView1;
            this.grdList.Name = "grdList";
            this.grdList.Size = new System.Drawing.Size(1063, 752);
            this.grdList.TabIndex = 2096;
            this.grdList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdList;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            // 
            // panDetails
            // 
            this.panDetails.BackColor = System.Drawing.Color.Silver;
            this.panDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panDetails.Controls.Add(this.lblContracts_Balances_ID);
            this.panDetails.Controls.Add(this.lblCustodian);
            this.panDetails.Controls.Add(this.lblXAA);
            this.panDetails.Controls.Add(this.lblMiFID_2);
            this.panDetails.Controls.Add(this.grpNotes);
            this.panDetails.Controls.Add(this.grpSpecialInstructions);
            this.panDetails.Controls.Add(this.label5);
            this.panDetails.Controls.Add(this.grpComplexData);
            this.panDetails.Controls.Add(this.label4);
            this.panDetails.Controls.Add(this.label1);
            this.panDetails.Location = new System.Drawing.Point(1078, 101);
            this.panDetails.Name = "panDetails";
            this.panDetails.Size = new System.Drawing.Size(282, 752);
            this.panDetails.TabIndex = 2097;
            // 
            // lblContracts_Balances_ID
            // 
            this.lblContracts_Balances_ID.AutoSize = true;
            this.lblContracts_Balances_ID.Location = new System.Drawing.Point(4, 610);
            this.lblContracts_Balances_ID.Name = "lblContracts_Balances_ID";
            this.lblContracts_Balances_ID.Size = new System.Drawing.Size(13, 13);
            this.lblContracts_Balances_ID.TabIndex = 231;
            this.lblContracts_Balances_ID.Text = "0";
            this.lblContracts_Balances_ID.Visible = false;
            // 
            // lblCustodian
            // 
            this.lblCustodian.AutoSize = true;
            this.lblCustodian.Location = new System.Drawing.Point(80, 17);
            this.lblCustodian.Name = "lblCustodian";
            this.lblCustodian.Size = new System.Drawing.Size(10, 13);
            this.lblCustodian.TabIndex = 230;
            this.lblCustodian.Text = "-";
            // 
            // lblXAA
            // 
            this.lblXAA.AutoSize = true;
            this.lblXAA.Location = new System.Drawing.Point(80, 67);
            this.lblXAA.Name = "lblXAA";
            this.lblXAA.Size = new System.Drawing.Size(10, 13);
            this.lblXAA.TabIndex = 229;
            this.lblXAA.Text = "-";
            // 
            // lblMiFID_2
            // 
            this.lblMiFID_2.AutoSize = true;
            this.lblMiFID_2.Location = new System.Drawing.Point(80, 44);
            this.lblMiFID_2.Name = "lblMiFID_2";
            this.lblMiFID_2.Size = new System.Drawing.Size(10, 13);
            this.lblMiFID_2.TabIndex = 228;
            this.lblMiFID_2.Text = "-";
            // 
            // grpNotes
            // 
            this.grpNotes.Controls.Add(this.txtNotes);
            this.grpNotes.Location = new System.Drawing.Point(6, 96);
            this.grpNotes.Name = "grpNotes";
            this.grpNotes.Size = new System.Drawing.Size(267, 116);
            this.grpNotes.TabIndex = 227;
            this.grpNotes.TabStop = false;
            this.grpNotes.Text = "Notes";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(6, 20);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(254, 88);
            this.txtNotes.TabIndex = 219;
            this.txtNotes.LostFocus += new System.EventHandler(this.txtNotes_LostFocus);
            // 
            // grpSpecialInstructions
            // 
            this.grpSpecialInstructions.Controls.Add(this.lblSpecialInstructions);
            this.grpSpecialInstructions.Location = new System.Drawing.Point(6, 218);
            this.grpSpecialInstructions.Name = "grpSpecialInstructions";
            this.grpSpecialInstructions.Size = new System.Drawing.Size(267, 183);
            this.grpSpecialInstructions.TabIndex = 226;
            this.grpSpecialInstructions.TabStop = false;
            this.grpSpecialInstructions.Text = "Special Instructions";
            // 
            // lblSpecialInstructions
            // 
            this.lblSpecialInstructions.AllowDrop = true;
            this.lblSpecialInstructions.Location = new System.Drawing.Point(10, 22);
            this.lblSpecialInstructions.Name = "lblSpecialInstructions";
            this.lblSpecialInstructions.Size = new System.Drawing.Size(250, 152);
            this.lblSpecialInstructions.TabIndex = 223;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 225;
            this.label5.Text = "ΧΑΑ :";
            // 
            // grpComplexData
            // 
            this.grpComplexData.Controls.Add(this.lblComplexData);
            this.grpComplexData.Location = new System.Drawing.Point(6, 412);
            this.grpComplexData.Name = "grpComplexData";
            this.grpComplexData.Size = new System.Drawing.Size(267, 183);
            this.grpComplexData.TabIndex = 224;
            this.grpComplexData.TabStop = false;
            this.grpComplexData.Text = "Complex Data";
            // 
            // lblComplexData
            // 
            this.lblComplexData.AllowDrop = true;
            this.lblComplexData.Location = new System.Drawing.Point(10, 22);
            this.lblComplexData.Name = "lblComplexData";
            this.lblComplexData.Size = new System.Drawing.Size(250, 152);
            this.lblComplexData.TabIndex = 223;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 221;
            this.label4.Text = "MiFID II :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 217;
            this.label1.Text = "Custodian :";
            // 
            // frmPortfolios_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1369, 861);
            this.Controls.Add(this.panDetails);
            this.Controls.Add(this.panImport);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.panTools);
            this.Controls.Add(this.panCritiries);
            this.Name = "frmPortfolios_List";
            this.Text = "Portfolios List";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPortfolios_List_Load);
            this.panCritiries.ResumeLayout(false);
            this.panCritiries.PerformLayout();
            this.panTools.ResumeLayout(false);
            this.panTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMax)).EndInit();
            this.toolLeft.ResumeLayout(false);
            this.toolLeft.PerformLayout();
            this.panImport.ResumeLayout(false);
            this.panImport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFilesPath2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFilesPath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panDetails.ResumeLayout(false);
            this.panDetails.PerformLayout();
            this.grpNotes.ResumeLayout(false);
            this.grpNotes.PerformLayout();
            this.grpSpecialInstructions.ResumeLayout(false);
            this.grpComplexData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel panCritiries;
        internal System.Windows.Forms.Label Label24;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Button btnSearch;
        internal System.Windows.Forms.DateTimePicker dDateControl;
        internal System.Windows.Forms.Panel panTools;
        internal System.Windows.Forms.ToolStrip toolLeft;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel15;
        internal System.Windows.Forms.ToolStripButton tsbExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        internal System.Windows.Forms.ToolStripButton tsbHelp;
        internal System.Windows.Forms.ComboBox lstAdvisor;
        internal System.Windows.Forms.ToolStripButton tsbView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        internal System.Windows.Forms.ToolStripButton tsbImport;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        internal System.Windows.Forms.Panel panImport;
        internal System.Windows.Forms.TextBox txtFilePath2_Import;
        internal System.Windows.Forms.PictureBox picFilesPath2;
        internal System.Windows.Forms.Label label11;
        internal System.Windows.Forms.Button btnGetImport;
        internal System.Windows.Forms.PictureBox picClose;
        internal System.Windows.Forms.TextBox txtFilePath_Import;
        internal System.Windows.Forms.PictureBox picFilesPath;
        internal System.Windows.Forms.Label label6;
        private DevExpress.XtraGrid.GridControl grdList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Panel panDetails;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNotes;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.ComboBox comboBox1;
        internal System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkMiFID2;
        internal System.Windows.Forms.Label lblComplexData;
        private System.Windows.Forms.GroupBox grpComplexData;
        private System.Windows.Forms.GroupBox grpNotes;
        private System.Windows.Forms.GroupBox grpSpecialInstructions;
        internal System.Windows.Forms.Label lblSpecialInstructions;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label lblCustodian;
        internal System.Windows.Forms.Label lblXAA;
        internal System.Windows.Forms.Label lblMiFID_2;
        internal System.Windows.Forms.Label lblContracts_Balances_ID;
        internal System.Windows.Forms.PictureBox picMax;
        internal System.Windows.Forms.PictureBox picMin;
    }
}