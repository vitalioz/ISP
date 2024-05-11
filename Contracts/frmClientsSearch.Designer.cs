namespace Contracts
{
    partial class frmClientsSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClientsSearch));
            this.chkAktive = new System.Windows.Forms.CheckBox();
            this.cmbRisk = new System.Windows.Forms.ComboBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.txtFirstname = new System.Windows.Forms.TextBox();
            this.cmbXora = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.ClientData = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbCitizen = new System.Windows.Forms.ComboBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panCritiries = new System.Windows.Forms.Panel();
            this.cmbSpecial = new System.Windows.Forms.ComboBox();
            this.cmbTypos = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAFM = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.chkSpecials = new System.Windows.Forms.CheckBox();
            this.fgSpecials = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panInvestProfiles = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.picClose_InvestProfiles = new System.Windows.Forms.PictureBox();
            this.toolLeft = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsbExcel = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLabels = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.mnuContext.SuspendLayout();
            this.panCritiries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgSpecials)).BeginInit();
            this.panInvestProfiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose_InvestProfiles)).BeginInit();
            this.toolLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkAktive
            // 
            this.chkAktive.AutoSize = true;
            this.chkAktive.Checked = true;
            this.chkAktive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAktive.Location = new System.Drawing.Point(879, 68);
            this.chkAktive.Name = "chkAktive";
            this.chkAktive.Size = new System.Drawing.Size(139, 17);
            this.chkAktive.TabIndex = 22;
            this.chkAktive.Text = "Μόνο ενεργοί πελάτες";
            this.chkAktive.UseVisualStyleBackColor = true;
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
            this.cmbRisk.Location = new System.Drawing.Point(318, 65);
            this.cmbRisk.Name = "cmbRisk";
            this.cmbRisk.Size = new System.Drawing.Size(121, 21);
            this.cmbRisk.TabIndex = 10;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(263, 69);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(52, 13);
            this.Label4.TabIndex = 532;
            this.Label4.Text = "Κίνδυνος";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Items.AddRange(new object[] {
            "ΙΔΙΩΤΗΣ",
            "ΕΤΑΙΡΕΙΑ",
            "ΘΕΣΜΙΚΟΣ",
            "-"});
            this.cmbCategory.Location = new System.Drawing.Point(73, 61);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(181, 21);
            this.cmbCategory.TabIndex = 8;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(4, 63);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(60, 13);
            this.Label8.TabIndex = 243;
            this.Label8.Text = "Κατηγορία";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(271, 36);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(41, 13);
            this.Label2.TabIndex = 241;
            this.Label2.Text = "Όνομα";
            // 
            // fgList
            // 
            this.fgList.AllowEditing = false;
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(3, 131);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(1131, 533);
            this.fgList.TabIndex = 0;
            // 
            // txtFirstname
            // 
            this.txtFirstname.Location = new System.Drawing.Point(318, 33);
            this.txtFirstname.Name = "txtFirstname";
            this.txtFirstname.Size = new System.Drawing.Size(121, 20);
            this.txtFirstname.TabIndex = 6;
            // 
            // cmbXora
            // 
            this.cmbXora.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbXora.FormattingEnabled = true;
            this.cmbXora.Location = new System.Drawing.Point(585, 32);
            this.cmbXora.Name = "cmbXora";
            this.cmbXora.Size = new System.Drawing.Size(219, 21);
            this.cmbXora.TabIndex = 14;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(476, 40);
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
            this.btnSearch.TabIndex = 24;
            this.btnSearch.Text = "Αναζήτηση";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // ClientData
            // 
            this.ClientData.Name = "ClientData";
            this.ClientData.Size = new System.Drawing.Size(182, 22);
            this.ClientData.Text = "Στοιχεία τού πελάτη";
            // 
            // cmbCitizen
            // 
            this.cmbCitizen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCitizen.FormattingEnabled = true;
            this.cmbCitizen.Location = new System.Drawing.Point(585, 6);
            this.cmbCitizen.Name = "cmbCitizen";
            this.cmbCitizen.Size = new System.Drawing.Size(219, 21);
            this.cmbCitizen.TabIndex = 12;
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(476, 13);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(69, 13);
            this.Label12.TabIndex = 216;
            this.Label12.Text = "Υπηκοότητα";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(4, 36);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(54, 13);
            this.Label1.TabIndex = 214;
            this.Label1.Text = "Επώνυμο ";
            // 
            // txtSurname
            // 
            this.txtSurname.Location = new System.Drawing.Point(73, 33);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(181, 20);
            this.txtSurname.TabIndex = 4;
            // 
            // mnuContext
            // 
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClientData});
            this.mnuContext.Name = "ContextMenuStrip1";
            this.mnuContext.Size = new System.Drawing.Size(183, 26);
            // 
            // panCritiries
            // 
            this.panCritiries.BackColor = System.Drawing.Color.Wheat;
            this.panCritiries.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panCritiries.Controls.Add(this.cmbSpecial);
            this.panCritiries.Controls.Add(this.cmbTypos);
            this.panCritiries.Controls.Add(this.label7);
            this.panCritiries.Controls.Add(this.txtAFM);
            this.panCritiries.Controls.Add(this.label6);
            this.panCritiries.Controls.Add(this.Label10);
            this.panCritiries.Controls.Add(this.chkAktive);
            this.panCritiries.Controls.Add(this.cmbRisk);
            this.panCritiries.Controls.Add(this.Label4);
            this.panCritiries.Controls.Add(this.cmbCategory);
            this.panCritiries.Controls.Add(this.Label8);
            this.panCritiries.Controls.Add(this.Label2);
            this.panCritiries.Controls.Add(this.txtFirstname);
            this.panCritiries.Controls.Add(this.cmbXora);
            this.panCritiries.Controls.Add(this.Label13);
            this.panCritiries.Controls.Add(this.btnSearch);
            this.panCritiries.Controls.Add(this.cmbCitizen);
            this.panCritiries.Controls.Add(this.Label12);
            this.panCritiries.Controls.Add(this.Label1);
            this.panCritiries.Controls.Add(this.txtSurname);
            this.panCritiries.Location = new System.Drawing.Point(4, 4);
            this.panCritiries.Name = "panCritiries";
            this.panCritiries.Size = new System.Drawing.Size(1131, 94);
            this.panCritiries.TabIndex = 212;
            // 
            // cmbSpecial
            // 
            this.cmbSpecial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpecial.FormattingEnabled = true;
            this.cmbSpecial.Location = new System.Drawing.Point(585, 61);
            this.cmbSpecial.Name = "cmbSpecial";
            this.cmbSpecial.Size = new System.Drawing.Size(219, 21);
            this.cmbSpecial.TabIndex = 16;
            // 
            // cmbTypos
            // 
            this.cmbTypos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypos.FormattingEnabled = true;
            this.cmbTypos.Items.AddRange(new object[] {
            "Γενικά Στοιχεία",
            "Λίστα Compliance"});
            this.cmbTypos.Location = new System.Drawing.Point(73, 6);
            this.cmbTypos.Name = "cmbTypos";
            this.cmbTypos.Size = new System.Drawing.Size(181, 21);
            this.cmbTypos.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 810;
            this.label7.Text = "Τύπος";
            // 
            // txtAFM
            // 
            this.txtAFM.Location = new System.Drawing.Point(877, 6);
            this.txtAFM.Name = "txtAFM";
            this.txtAFM.Size = new System.Drawing.Size(121, 20);
            this.txtAFM.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(830, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 807;
            this.label6.Text = "ΑΦΜ";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(483, 67);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(62, 13);
            this.Label10.TabIndex = 568;
            this.Label10.Text = "Επάγγελμα";
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
            // panInvestProfiles
            // 
            this.panInvestProfiles.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panInvestProfiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panInvestProfiles.Controls.Add(this.label5);
            this.panInvestProfiles.Controls.Add(this.chkSpecials);
            this.panInvestProfiles.Controls.Add(this.picClose_InvestProfiles);
            this.panInvestProfiles.Controls.Add(this.fgSpecials);
            this.panInvestProfiles.Location = new System.Drawing.Point(1113, 151);
            this.panInvestProfiles.Name = "panInvestProfiles";
            this.panInvestProfiles.Size = new System.Drawing.Size(353, 197);
            this.panInvestProfiles.TabIndex = 2089;
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
            this.toolLeft.Location = new System.Drawing.Point(3, 102);
            this.toolLeft.Name = "toolLeft";
            this.toolLeft.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolLeft.Size = new System.Drawing.Size(109, 26);
            this.toolLeft.TabIndex = 2090;
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
            this.tsbExcel.Click += new System.EventHandler(this.tsbExcel_Click);
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
            // frmClientsSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(1141, 666);
            this.Controls.Add(this.toolLeft);
            this.Controls.Add(this.panInvestProfiles);
            this.Controls.Add(this.fgList);
            this.Controls.Add(this.panCritiries);
            this.Name = "frmClientsSearch";
            this.Text = "Αναζήτηση &Πελατών";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmClientsSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.mnuContext.ResumeLayout(false);
            this.panCritiries.ResumeLayout(false);
            this.panCritiries.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgSpecials)).EndInit();
            this.panInvestProfiles.ResumeLayout(false);
            this.panInvestProfiles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose_InvestProfiles)).EndInit();
            this.toolLeft.ResumeLayout(false);
            this.toolLeft.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.CheckBox chkAktive;
        internal System.Windows.Forms.ComboBox cmbRisk;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.ComboBox cmbCategory;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label Label2;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
        internal System.Windows.Forms.TextBox txtFirstname;
        internal System.Windows.Forms.ComboBox cmbXora;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Button btnSearch;
        internal System.Windows.Forms.ToolStripMenuItem ClientData;
        internal System.Windows.Forms.ComboBox cmbCitizen;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txtSurname;
        internal System.Windows.Forms.ContextMenuStrip mnuContext;
        internal System.Windows.Forms.Panel panCritiries;
        internal System.Windows.Forms.CheckBox chkSpecials;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgSpecials;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Panel panInvestProfiles;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.PictureBox picClose_InvestProfiles;
        internal System.Windows.Forms.TextBox txtAFM;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.ToolStrip toolLeft;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel2;
        internal System.Windows.Forms.ToolStripButton tsbExcel;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        internal System.Windows.Forms.ToolStripButton tsbLabels;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripButton tsbHelp;
        internal System.Windows.Forms.ComboBox cmbTypos;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.ComboBox cmbSpecial;
    }
}