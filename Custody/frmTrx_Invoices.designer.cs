namespace Custody
{
    partial class frmTrx_Invoices
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTrx_Invoices));
            this.chkIncomeTax = new System.Windows.Forms.CheckBox();
            this.chkSaleFees = new System.Windows.Forms.CheckBox();
            this.picClose_Import = new System.Windows.Forms.PictureBox();
            this.toolEdit = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.chkVAT = new System.Windows.Forms.CheckBox();
            this.chkTaxHome = new System.Windows.Forms.CheckBox();
            this.chkDepository = new System.Windows.Forms.CheckBox();
            this.chkCustodian = new System.Windows.Forms.CheckBox();
            this.chkExecVenue = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmTrxType = new System.Windows.Forms.ComboBox();
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.chkExecAgent = new System.Windows.Forms.CheckBox();
            this.lblGroup = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.cmbTrxCategory = new System.Windows.Forms.ComboBox();
            this.panEdit = new System.Windows.Forms.Panel();
            this.tsbExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEdit = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.ToolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolLeft = new System.Windows.Forms.ToolStrip();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            ((System.ComponentModel.ISupportInitialize)(this.picClose_Import)).BeginInit();
            this.toolEdit.SuspendLayout();
            this.panEdit.SuspendLayout();
            this.toolLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.SuspendLayout();
            // 
            // chkIncomeTax
            // 
            this.chkIncomeTax.AutoSize = true;
            this.chkIncomeTax.Location = new System.Drawing.Point(316, 258);
            this.chkIncomeTax.Name = "chkIncomeTax";
            this.chkIncomeTax.Size = new System.Drawing.Size(115, 17);
            this.chkIncomeTax.TabIndex = 1125;
            this.chkIncomeTax.Text = "Φόρος Προσόδων";
            this.chkIncomeTax.UseVisualStyleBackColor = true;
            // 
            // chkSaleFees
            // 
            this.chkSaleFees.AutoSize = true;
            this.chkSaleFees.Location = new System.Drawing.Point(316, 236);
            this.chkSaleFees.Name = "chkSaleFees";
            this.chkSaleFees.Size = new System.Drawing.Size(108, 17);
            this.chkSaleFees.TabIndex = 1124;
            this.chkSaleFees.Text = "Φορος Πώλησης";
            this.chkSaleFees.UseVisualStyleBackColor = true;
            // 
            // picClose_Import
            // 
            this.picClose_Import.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picClose_Import.Image = global::Custody.Properties.Resources.cancel;
            this.picClose_Import.Location = new System.Drawing.Point(482, 20);
            this.picClose_Import.Name = "picClose_Import";
            this.picClose_Import.Size = new System.Drawing.Size(18, 18);
            this.picClose_Import.TabIndex = 1123;
            this.picClose_Import.TabStop = false;
            // 
            // toolEdit
            // 
            this.toolEdit.AutoSize = false;
            this.toolEdit.BackColor = System.Drawing.Color.Gainsboro;
            this.toolEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolEdit.Dock = System.Windows.Forms.DockStyle.None;
            this.toolEdit.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolEdit.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripLabel1,
            this.tsbSave});
            this.toolEdit.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolEdit.Location = new System.Drawing.Point(22, 20);
            this.toolEdit.Name = "toolEdit";
            this.toolEdit.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolEdit.Size = new System.Drawing.Size(51, 25);
            this.toolEdit.TabIndex = 1122;
            this.toolEdit.Text = "ToolStrip1";
            // 
            // ToolStripLabel1
            // 
            this.ToolStripLabel1.Name = "ToolStripLabel1";
            this.ToolStripLabel1.Size = new System.Drawing.Size(10, 22);
            this.ToolStripLabel1.Text = " ";
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = global::Custody.Properties.Resources.save;
            this.tsbSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(24, 22);
            this.tsbSave.Text = "Αποθήκευση";
            // 
            // chkVAT
            // 
            this.chkVAT.AutoSize = true;
            this.chkVAT.Location = new System.Drawing.Point(316, 214);
            this.chkVAT.Name = "chkVAT";
            this.chkVAT.Size = new System.Drawing.Size(50, 17);
            this.chkVAT.TabIndex = 385;
            this.chkVAT.Text = "ΦΠΑ";
            this.chkVAT.UseVisualStyleBackColor = true;
            // 
            // chkTaxHome
            // 
            this.chkTaxHome.AutoSize = true;
            this.chkTaxHome.Location = new System.Drawing.Point(316, 192);
            this.chkTaxHome.Name = "chkTaxHome";
            this.chkTaxHome.Size = new System.Drawing.Size(169, 17);
            this.chkTaxHome.TabIndex = 384;
            this.chkTaxHome.Text = "Φορολογική κατοικία πελάτη";
            this.chkTaxHome.UseVisualStyleBackColor = true;
            // 
            // chkDepository
            // 
            this.chkDepository.AutoSize = true;
            this.chkDepository.Location = new System.Drawing.Point(54, 258);
            this.chkDepository.Name = "chkDepository";
            this.chkDepository.Size = new System.Drawing.Size(84, 17);
            this.chkDepository.TabIndex = 383;
            this.chkDepository.Text = "Αποθετήριο";
            this.chkDepository.UseVisualStyleBackColor = true;
            // 
            // chkCustodian
            // 
            this.chkCustodian.AutoSize = true;
            this.chkCustodian.Location = new System.Drawing.Point(54, 236);
            this.chkCustodian.Name = "chkCustodian";
            this.chkCustodian.Size = new System.Drawing.Size(73, 17);
            this.chkCustodian.TabIndex = 382;
            this.chkCustodian.Text = "Custodian";
            this.chkCustodian.UseVisualStyleBackColor = true;
            // 
            // chkExecVenue
            // 
            this.chkExecVenue.AutoSize = true;
            this.chkExecVenue.Location = new System.Drawing.Point(54, 214);
            this.chkExecVenue.Name = "chkExecVenue";
            this.chkExecVenue.Size = new System.Drawing.Size(107, 17);
            this.chkExecVenue.TabIndex = 379;
            this.chkExecVenue.Text = "Execution Venue";
            this.chkExecVenue.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 378;
            this.label1.Text = "Είδος Κινησης";
            // 
            // cmTrxType
            // 
            this.cmTrxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmTrxType.FormattingEnabled = true;
            this.cmTrxType.Items.AddRange(new object[] {
            "-",
            "Ταυτότητα & Διαβατήριο",
            "ΑΦΜ",
            "ΑΜΚΑ",
            "Σταθερό τηλέφωνο"});
            this.cmTrxType.Location = new System.Drawing.Point(158, 98);
            this.cmTrxType.Name = "cmTrxType";
            this.cmTrxType.Size = new System.Drawing.Size(329, 21);
            this.cmTrxType.TabIndex = 377;
            // 
            // lblTitle1
            // 
            this.lblTitle1.AutoSize = true;
            this.lblTitle1.Location = new System.Drawing.Point(54, 131);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(59, 13);
            this.lblTitle1.TabIndex = 268;
            this.lblTitle1.Text = "Αιτιολογία";
            // 
            // chkExecAgent
            // 
            this.chkExecAgent.AutoSize = true;
            this.chkExecAgent.Location = new System.Drawing.Point(54, 192);
            this.chkExecAgent.Name = "chkExecAgent";
            this.chkExecAgent.Size = new System.Drawing.Size(115, 17);
            this.chkExecAgent.TabIndex = 376;
            this.chkExecAgent.Text = "Execution agent    ";
            this.chkExecAgent.UseVisualStyleBackColor = true;
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(54, 74);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(95, 13);
            this.lblGroup.TabIndex = 366;
            this.lblGroup.Text = "Γενική Κατηγορία";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(157, 125);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(329, 20);
            this.txtTitle.TabIndex = 4;
            // 
            // cmbTrxCategory
            // 
            this.cmbTrxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrxCategory.FormattingEnabled = true;
            this.cmbTrxCategory.Items.AddRange(new object[] {
            "-",
            "Ταυτότητα & Διαβατήριο",
            "ΑΦΜ",
            "ΑΜΚΑ",
            "Σταθερό τηλέφωνο"});
            this.cmbTrxCategory.Location = new System.Drawing.Point(157, 71);
            this.cmbTrxCategory.Name = "cmbTrxCategory";
            this.cmbTrxCategory.Size = new System.Drawing.Size(329, 21);
            this.cmbTrxCategory.TabIndex = 14;
            // 
            // panEdit
            // 
            this.panEdit.BackColor = System.Drawing.Color.Moccasin;
            this.panEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panEdit.Controls.Add(this.chkIncomeTax);
            this.panEdit.Controls.Add(this.chkSaleFees);
            this.panEdit.Controls.Add(this.picClose_Import);
            this.panEdit.Controls.Add(this.toolEdit);
            this.panEdit.Controls.Add(this.chkVAT);
            this.panEdit.Controls.Add(this.chkTaxHome);
            this.panEdit.Controls.Add(this.chkDepository);
            this.panEdit.Controls.Add(this.chkCustodian);
            this.panEdit.Controls.Add(this.chkExecVenue);
            this.panEdit.Controls.Add(this.label1);
            this.panEdit.Controls.Add(this.cmTrxType);
            this.panEdit.Controls.Add(this.lblTitle1);
            this.panEdit.Controls.Add(this.chkExecAgent);
            this.panEdit.Controls.Add(this.lblGroup);
            this.panEdit.Controls.Add(this.txtTitle);
            this.panEdit.Controls.Add(this.cmbTrxCategory);
            this.panEdit.Location = new System.Drawing.Point(441, 196);
            this.panEdit.Name = "panEdit";
            this.panEdit.Size = new System.Drawing.Size(519, 334);
            this.panEdit.TabIndex = 381;
            this.panEdit.Visible = false;
            // 
            // tsbExcel
            // 
            this.tsbExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExcel.Image = global::Custody.Properties.Resources.excel;
            this.tsbExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExcel.Name = "tsbExcel";
            this.tsbExcel.Size = new System.Drawing.Size(23, 24);
            this.tsbExcel.Text = "Εξαγωγή στο Excel";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            this.toolStripSeparator2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            // 
            // tsbDelete
            // 
            this.tsbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbDelete.Image")));
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(23, 24);
            this.tsbDelete.Text = "Διαγραφή Εγγραφής";
            // 
            // ToolStripSeparator5
            // 
            this.ToolStripSeparator5.Name = "ToolStripSeparator5";
            this.ToolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            this.ToolStripSeparator5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            // 
            // tsbEdit
            // 
            this.tsbEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEdit.Image = ((System.Drawing.Image)(resources.GetObject("tsbEdit.Image")));
            this.tsbEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEdit.Name = "tsbEdit";
            this.tsbEdit.Size = new System.Drawing.Size(23, 24);
            this.tsbEdit.Text = "Διόρθωση Εγγραφής";
            // 
            // ToolStripSeparator4
            // 
            this.ToolStripSeparator4.Name = "ToolStripSeparator4";
            this.ToolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbAdd
            // 
            this.tsbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbAdd.Image")));
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(23, 24);
            this.tsbAdd.Text = "Νέα εγγραφή";
            // 
            // ToolStripLabel2
            // 
            this.ToolStripLabel2.Name = "ToolStripLabel2";
            this.ToolStripLabel2.Size = new System.Drawing.Size(10, 24);
            this.ToolStripLabel2.Text = " ";
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
            this.tsbAdd,
            this.ToolStripSeparator4,
            this.tsbEdit,
            this.ToolStripSeparator5,
            this.tsbDelete,
            this.toolStripSeparator2,
            this.tsbExcel});
            this.toolLeft.Location = new System.Drawing.Point(12, 10);
            this.toolLeft.Name = "toolLeft";
            this.toolLeft.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolLeft.Size = new System.Drawing.Size(138, 27);
            this.toolLeft.TabIndex = 380;
            this.toolLeft.Text = "ToolStrip1";
            // 
            // fgList
            // 
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(12, 43);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 2;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Rows.Fixed = 2;
            this.fgList.Size = new System.Drawing.Size(1471, 704);
            this.fgList.StyleInfo = resources.GetString("fgList.StyleInfo");
            this.fgList.SubtotalPosition = C1.Win.C1FlexGrid.SubtotalPositionEnum.BelowData;
            this.fgList.TabIndex = 379;
            // 
            // frmTrx_Invoices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.ClientSize = new System.Drawing.Size(1484, 733);
            this.Controls.Add(this.panEdit);
            this.Controls.Add(this.toolLeft);
            this.Controls.Add(this.fgList);
            this.Name = "frmTrx_Invoices";
            this.Text = "Παραστατικά ανά κίνηση";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmTrx_Invoices_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picClose_Import)).EndInit();
            this.toolEdit.ResumeLayout(false);
            this.toolEdit.PerformLayout();
            this.panEdit.ResumeLayout(false);
            this.panEdit.PerformLayout();
            this.toolLeft.ResumeLayout(false);
            this.toolLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.CheckBox chkIncomeTax;
        internal System.Windows.Forms.CheckBox chkSaleFees;
        internal System.Windows.Forms.PictureBox picClose_Import;
        internal System.Windows.Forms.ToolStrip toolEdit;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel1;
        internal System.Windows.Forms.ToolStripButton tsbSave;
        internal System.Windows.Forms.CheckBox chkVAT;
        internal System.Windows.Forms.CheckBox chkTaxHome;
        internal System.Windows.Forms.CheckBox chkDepository;
        internal System.Windows.Forms.CheckBox chkCustodian;
        internal System.Windows.Forms.CheckBox chkExecVenue;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ComboBox cmTrxType;
        internal System.Windows.Forms.Label lblTitle1;
        internal System.Windows.Forms.CheckBox chkExecAgent;
        internal System.Windows.Forms.Label lblGroup;
        internal System.Windows.Forms.TextBox txtTitle;
        internal System.Windows.Forms.ComboBox cmbTrxCategory;
        private System.Windows.Forms.Panel panEdit;
        internal System.Windows.Forms.ToolStripButton tsbExcel;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        internal System.Windows.Forms.ToolStripButton tsbDelete;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator5;
        internal System.Windows.Forms.ToolStripButton tsbEdit;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator4;
        internal System.Windows.Forms.ToolStripButton tsbAdd;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel2;
        internal System.Windows.Forms.ToolStrip toolLeft;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
    }
}