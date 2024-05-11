namespace Contracts
{
    partial class frmCandidatesSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCandidatesSearch));
            this.txtAFM = new System.Windows.Forms.TextBox();
            this.panInvestProfiles = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.chkSpecials = new System.Windows.Forms.CheckBox();
            this.picClose_InvestProfiles = new System.Windows.Forms.PictureBox();
            this.fgSpecials = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label6 = new System.Windows.Forms.Label();
            this.toolLeft = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsbExcel = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLabels = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panCritiries = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtAngelDay = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.cmbRisk = new System.Windows.Forms.ComboBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtFirstname = new System.Windows.Forms.TextBox();
            this.cmbXora = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbCitizen = new System.Windows.Forms.ComboBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.ClientData = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ucDC = new Core.ucDoubleCalendar();
            this.panInvestProfiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose_InvestProfiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fgSpecials)).BeginInit();
            this.toolLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.panCritiries.SuspendLayout();
            this.mnuContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtAFM
            // 
            this.txtAFM.Location = new System.Drawing.Point(904, 6);
            this.txtAFM.Name = "txtAFM";
            this.txtAFM.Size = new System.Drawing.Size(121, 20);
            this.txtAFM.TabIndex = 808;
            // 
            // panInvestProfiles
            // 
            this.panInvestProfiles.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panInvestProfiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panInvestProfiles.Controls.Add(this.label5);
            this.panInvestProfiles.Controls.Add(this.chkSpecials);
            this.panInvestProfiles.Controls.Add(this.picClose_InvestProfiles);
            this.panInvestProfiles.Controls.Add(this.fgSpecials);
            this.panInvestProfiles.Location = new System.Drawing.Point(1116, 154);
            this.panInvestProfiles.Name = "panInvestProfiles";
            this.panInvestProfiles.Size = new System.Drawing.Size(353, 197);
            this.panInvestProfiles.TabIndex = 2093;
            this.panInvestProfiles.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 2065;
            this.label5.Text = "Επάγγελμα";
            // 
            // chkSpecials
            // 
            this.chkSpecials.AutoSize = true;
            this.chkSpecials.Location = new System.Drawing.Point(11, 26);
            this.chkSpecials.Name = "chkSpecials";
            this.chkSpecials.Size = new System.Drawing.Size(15, 14);
            this.chkSpecials.TabIndex = 570;
            this.chkSpecials.UseVisualStyleBackColor = true;
            // 
            // picClose_InvestProfiles
            // 
            this.picClose_InvestProfiles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picClose_InvestProfiles.Image = global::Contracts.Properties.Resources.cancel1;
            this.picClose_InvestProfiles.Location = new System.Drawing.Point(328, 3);
            this.picClose_InvestProfiles.Name = "picClose_InvestProfiles";
            this.picClose_InvestProfiles.Size = new System.Drawing.Size(18, 18);
            this.picClose_InvestProfiles.TabIndex = 2062;
            this.picClose_InvestProfiles.TabStop = false;
            // 
            // fgSpecials
            // 
            this.fgSpecials.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.fgSpecials.ColumnInfo = "3,0,0,0,0,85,Columns:0{Width:21;}\t1{Width:300;Caption:\"Τίτλος\";Style:\"TextAlign:L" +
    "eftCenter;\";StyleFixed:\"TextAlign:LeftCenter;\";}\t2{Width:54;Caption:\"ID\";Visible" +
    ":False;}\t";
            this.fgSpecials.Location = new System.Drawing.Point(4, 22);
            this.fgSpecials.Name = "fgSpecials";
            this.fgSpecials.Rows.Count = 1;
            this.fgSpecials.Rows.DefaultSize = 17;
            this.fgSpecials.Size = new System.Drawing.Size(344, 168);
            this.fgSpecials.TabIndex = 569;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(857, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 807;
            this.label6.Text = "ΑΦΜ";
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
            this.tsbExcel,
            this.ToolStripSeparator2,
            this.tsbLabels,
            this.ToolStripSeparator1,
            this.tsbHelp});
            this.toolLeft.Location = new System.Drawing.Point(6, 105);
            this.toolLeft.Name = "toolLeft";
            this.toolLeft.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolLeft.Size = new System.Drawing.Size(109, 26);
            this.toolLeft.TabIndex = 2094;
            this.toolLeft.Text = "ToolStrip1";
            // 
            // ToolStripLabel2
            // 
            this.ToolStripLabel2.Name = "ToolStripLabel2";
            this.ToolStripLabel2.Size = new System.Drawing.Size(10, 23);
            this.ToolStripLabel2.Text = " ";
            // 
            // tsbExcel
            // 
            this.tsbExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExcel.Image = global::Contracts.Properties.Resources.excel;
            this.tsbExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExcel.Name = "tsbExcel";
            this.tsbExcel.Size = new System.Drawing.Size(23, 23);
            this.tsbExcel.Text = "Εξαγωγή στο Excel";
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 26);
            // 
            // tsbLabels
            // 
            this.tsbLabels.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLabels.Image = global::Contracts.Properties.Resources.Labels;
            this.tsbLabels.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLabels.Name = "tsbLabels";
            this.tsbLabels.Size = new System.Drawing.Size(23, 23);
            this.tsbLabels.Text = "Ετικέτες";
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 26);
            // 
            // tsbHelp
            // 
            this.tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHelp.Image = global::Contracts.Properties.Resources.Help;
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(23, 23);
            this.tsbHelp.Text = "Βοήθεια";
            // 
            // fgList
            // 
            this.fgList.AllowEditing = false;
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(6, 134);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(1131, 533);
            this.fgList.TabIndex = 2092;
            // 
            // panCritiries
            // 
            this.panCritiries.BackColor = System.Drawing.Color.Wheat;
            this.panCritiries.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panCritiries.Controls.Add(this.label7);
            this.panCritiries.Controls.Add(this.txtAFM);
            this.panCritiries.Controls.Add(this.label6);
            this.panCritiries.Controls.Add(this.label16);
            this.panCritiries.Controls.Add(this.txtAngelDay);
            this.panCritiries.Controls.Add(this.label3);
            this.panCritiries.Controls.Add(this.Label10);
            this.panCritiries.Controls.Add(this.cmbRisk);
            this.panCritiries.Controls.Add(this.Label4);
            this.panCritiries.Controls.Add(this.Label2);
            this.panCritiries.Controls.Add(this.txtFirstname);
            this.panCritiries.Controls.Add(this.cmbXora);
            this.panCritiries.Controls.Add(this.Label13);
            this.panCritiries.Controls.Add(this.btnSearch);
            this.panCritiries.Controls.Add(this.cmbCitizen);
            this.panCritiries.Controls.Add(this.Label12);
            this.panCritiries.Controls.Add(this.Label1);
            this.panCritiries.Controls.Add(this.txtSurname);
            this.panCritiries.Location = new System.Drawing.Point(7, 7);
            this.panCritiries.Name = "panCritiries";
            this.panCritiries.Size = new System.Drawing.Size(1131, 94);
            this.panCritiries.TabIndex = 2091;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(130, 13);
            this.label7.TabIndex = 1108;
            this.label7.Text = "Ημερ.καταχώρησεις από";
            // 
            // label16
            // 
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label16.Location = new System.Drawing.Point(627, 61);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(219, 21);
            this.label16.TabIndex = 806;
            // 
            // txtAngelDay
            // 
            this.txtAngelDay.Location = new System.Drawing.Point(372, 37);
            this.txtAngelDay.Name = "txtAngelDay";
            this.txtAngelDay.Size = new System.Drawing.Size(121, 20);
            this.txtAngelDay.TabIndex = 567;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(325, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 566;
            this.label3.Text = "Γιορτή";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(525, 67);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(62, 13);
            this.Label10.TabIndex = 568;
            this.Label10.Text = "Επάγγελμα";
            // 
            // cmbRisk
            // 
            this.cmbRisk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRisk.FormattingEnabled = true;
            this.cmbRisk.Items.AddRange(new object[] {
            "",
            "Υψηλός",
            "Μεσαίος",
            "Χαμηλός"});
            this.cmbRisk.Location = new System.Drawing.Point(372, 65);
            this.cmbRisk.Name = "cmbRisk";
            this.cmbRisk.Size = new System.Drawing.Size(121, 21);
            this.cmbRisk.TabIndex = 7;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(317, 69);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(52, 13);
            this.Label4.TabIndex = 532;
            this.Label4.Text = "Κίνδυνος";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(325, 13);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(41, 13);
            this.Label2.TabIndex = 241;
            this.Label2.Text = "Όνομα";
            // 
            // txtFirstname
            // 
            this.txtFirstname.Location = new System.Drawing.Point(372, 10);
            this.txtFirstname.Name = "txtFirstname";
            this.txtFirstname.Size = new System.Drawing.Size(121, 20);
            this.txtFirstname.TabIndex = 2;
            // 
            // cmbXora
            // 
            this.cmbXora.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbXora.FormattingEnabled = true;
            this.cmbXora.Location = new System.Drawing.Point(627, 32);
            this.cmbXora.Name = "cmbXora";
            this.cmbXora.Size = new System.Drawing.Size(219, 21);
            this.cmbXora.TabIndex = 12;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(518, 40);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(86, 13);
            this.Label13.TabIndex = 239;
            this.Label13.Text = "Χώρα κατοικίας";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(1030, 63);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(92, 26);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "Αναζήτηση";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cmbCitizen
            // 
            this.cmbCitizen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCitizen.FormattingEnabled = true;
            this.cmbCitizen.Location = new System.Drawing.Point(627, 6);
            this.cmbCitizen.Name = "cmbCitizen";
            this.cmbCitizen.Size = new System.Drawing.Size(219, 21);
            this.cmbCitizen.TabIndex = 8;
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(518, 13);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(69, 13);
            this.Label12.TabIndex = 216;
            this.Label12.Text = "Υπηκοότητα";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(4, 13);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(54, 13);
            this.Label1.TabIndex = 214;
            this.Label1.Text = "Επώνυμο ";
            // 
            // txtSurname
            // 
            this.txtSurname.Location = new System.Drawing.Point(73, 10);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(181, 20);
            this.txtSurname.TabIndex = 0;
            // 
            // ClientData
            // 
            this.ClientData.Name = "ClientData";
            this.ClientData.Size = new System.Drawing.Size(182, 22);
            this.ClientData.Text = "Στοιχεία τού πελάτη";
            // 
            // mnuContext
            // 
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClientData});
            this.mnuContext.Name = "ContextMenuStrip1";
            this.mnuContext.Size = new System.Drawing.Size(183, 26);
            // 
            // ucDC
            // 
            this.ucDC.BackColor = System.Drawing.Color.Wheat;
            this.ucDC.DateFrom = new System.DateTime(2020, 8, 1, 16, 39, 27, 913);
            this.ucDC.DateTo = new System.DateTime(2020, 8, 1, 16, 39, 27, 913);
            this.ucDC.Location = new System.Drawing.Point(53, 72);
            this.ucDC.Name = "ucDC";
            this.ucDC.Size = new System.Drawing.Size(233, 21);
            this.ucDC.TabIndex = 1107;
            // 
            // frmCandidatesSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Thistle;
            this.ClientSize = new System.Drawing.Size(1141, 666);
            this.Controls.Add(this.ucDC);
            this.Controls.Add(this.panInvestProfiles);
            this.Controls.Add(this.toolLeft);
            this.Controls.Add(this.fgList);
            this.Controls.Add(this.panCritiries);
            this.Name = "frmCandidatesSearch";
            this.Text = "Αναζήτηση Υποψηφίων";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmCandidatesSearch_Load);
            this.panInvestProfiles.ResumeLayout(false);
            this.panInvestProfiles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose_InvestProfiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fgSpecials)).EndInit();
            this.toolLeft.ResumeLayout(false);
            this.toolLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.panCritiries.ResumeLayout(false);
            this.panCritiries.PerformLayout();
            this.mnuContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TextBox txtAFM;
        internal System.Windows.Forms.Panel panInvestProfiles;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.CheckBox chkSpecials;
        internal System.Windows.Forms.PictureBox picClose_InvestProfiles;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgSpecials;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.ToolStrip toolLeft;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel2;
        internal System.Windows.Forms.ToolStripButton tsbExcel;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        internal System.Windows.Forms.ToolStripButton tsbLabels;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripButton tsbHelp;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
        internal System.Windows.Forms.Panel panCritiries;
        internal System.Windows.Forms.Button btnSearch;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txtSurname;
        internal System.Windows.Forms.ToolStripMenuItem ClientData;
        internal System.Windows.Forms.ContextMenuStrip mnuContext;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.Label label16;
        internal System.Windows.Forms.TextBox txtAngelDay;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.ComboBox cmbRisk;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtFirstname;
        internal System.Windows.Forms.ComboBox cmbXora;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.ComboBox cmbCitizen;
        internal System.Windows.Forms.Label Label12;
        private Core.ucDoubleCalendar ucDC;
    }
}