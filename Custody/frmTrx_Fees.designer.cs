﻿namespace Custody
{
    partial class frmTrx_Fees
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTrx_Fees));
            this.picClose_Import = new System.Windows.Forms.PictureBox();
            this.toolEdit = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTrxType = new System.Windows.Forms.ComboBox();
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.lblGroup = new System.Windows.Forms.Label();
            this.cmbTrxCategory = new System.Windows.Forms.ComboBox();
            this.panEdit = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbRevenue = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbEarnings = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbTrxClientsFees = new System.Windows.Forms.ComboBox();
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
            this.cmbTrxEtiology = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.picClose_Import)).BeginInit();
            this.toolEdit.SuspendLayout();
            this.panEdit.SuspendLayout();
            this.toolLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.SuspendLayout();
            // 
            // picClose_Import
            // 
            this.picClose_Import.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picClose_Import.Image = global::Custody.Properties.Resources.cancel;
            this.picClose_Import.Location = new System.Drawing.Point(482, 16);
            this.picClose_Import.Name = "picClose_Import";
            this.picClose_Import.Size = new System.Drawing.Size(18, 18);
            this.picClose_Import.TabIndex = 1123;
            this.picClose_Import.TabStop = false;
            this.picClose_Import.Click += new System.EventHandler(this.picClose_Import_Click);
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
            this.toolEdit.Location = new System.Drawing.Point(22, 16);
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
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 378;
            this.label1.Text = "Τύπος Κίνησης";
            // 
            // cmbTrxType
            // 
            this.cmbTrxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrxType.FormattingEnabled = true;
            this.cmbTrxType.Location = new System.Drawing.Point(150, 98);
            this.cmbTrxType.Name = "cmbTrxType";
            this.cmbTrxType.Size = new System.Drawing.Size(329, 21);
            this.cmbTrxType.TabIndex = 16;
            // 
            // lblTitle1
            // 
            this.lblTitle1.AutoSize = true;
            this.lblTitle1.Location = new System.Drawing.Point(40, 130);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(59, 13);
            this.lblTitle1.TabIndex = 268;
            this.lblTitle1.Text = "Αιτιολογία";
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(40, 74);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(95, 13);
            this.lblGroup.TabIndex = 366;
            this.lblGroup.Text = "Γενική Κατηγορία";
            // 
            // cmbTrxCategory
            // 
            this.cmbTrxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrxCategory.FormattingEnabled = true;
            this.cmbTrxCategory.Location = new System.Drawing.Point(150, 70);
            this.cmbTrxCategory.Name = "cmbTrxCategory";
            this.cmbTrxCategory.Size = new System.Drawing.Size(329, 21);
            this.cmbTrxCategory.TabIndex = 14;
            // 
            // panEdit
            // 
            this.panEdit.BackColor = System.Drawing.Color.Moccasin;
            this.panEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panEdit.Controls.Add(this.cmbTrxEtiology);
            this.panEdit.Controls.Add(this.label5);
            this.panEdit.Controls.Add(this.txtNotes);
            this.panEdit.Controls.Add(this.label4);
            this.panEdit.Controls.Add(this.cmbRevenue);
            this.panEdit.Controls.Add(this.label3);
            this.panEdit.Controls.Add(this.cmbEarnings);
            this.panEdit.Controls.Add(this.label2);
            this.panEdit.Controls.Add(this.cmbTrxClientsFees);
            this.panEdit.Controls.Add(this.picClose_Import);
            this.panEdit.Controls.Add(this.toolEdit);
            this.panEdit.Controls.Add(this.label1);
            this.panEdit.Controls.Add(this.cmbTrxType);
            this.panEdit.Controls.Add(this.lblTitle1);
            this.panEdit.Controls.Add(this.lblGroup);
            this.panEdit.Controls.Add(this.cmbTrxCategory);
            this.panEdit.Location = new System.Drawing.Point(392, 242);
            this.panEdit.Name = "panEdit";
            this.panEdit.Size = new System.Drawing.Size(519, 320);
            this.panEdit.TabIndex = 381;
            this.panEdit.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 242);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 1131;
            this.label5.Text = "Παρατηρήσεις";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(150, 238);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(329, 57);
            this.txtNotes.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 214);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 1129;
            this.label4.Text = "Εσοδα";
            // 
            // cmbRevenue
            // 
            this.cmbRevenue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRevenue.FormattingEnabled = true;
            this.cmbRevenue.Location = new System.Drawing.Point(150, 210);
            this.cmbRevenue.Name = "cmbRevenue";
            this.cmbRevenue.Size = new System.Drawing.Size(329, 21);
            this.cmbRevenue.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 186);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 1127;
            this.label3.Text = "Αποδοσεις";
            // 
            // cmbEarnings
            // 
            this.cmbEarnings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEarnings.FormattingEnabled = true;
            this.cmbEarnings.Location = new System.Drawing.Point(150, 182);
            this.cmbEarnings.Name = "cmbEarnings";
            this.cmbEarnings.Size = new System.Drawing.Size(329, 21);
            this.cmbEarnings.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 1125;
            this.label2.Text = "Επιβαρύνσεις";
            // 
            // cmbTrxClientsFees
            // 
            this.cmbTrxClientsFees.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrxClientsFees.FormattingEnabled = true;
            this.cmbTrxClientsFees.Location = new System.Drawing.Point(150, 154);
            this.cmbTrxClientsFees.Name = "cmbTrxClientsFees";
            this.cmbTrxClientsFees.Size = new System.Drawing.Size(329, 21);
            this.cmbTrxClientsFees.TabIndex = 20;
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
            this.tsbDelete.Click += new System.EventHandler(this.tsbDelete_Click);
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
            this.tsbEdit.Click += new System.EventHandler(this.tsbEdit_Click);
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
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
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
            this.toolLeft.Location = new System.Drawing.Point(8, 8);
            this.toolLeft.Name = "toolLeft";
            this.toolLeft.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolLeft.Size = new System.Drawing.Size(138, 27);
            this.toolLeft.TabIndex = 380;
            this.toolLeft.Text = "ToolStrip1";
            // 
            // fgList
            // 
            this.fgList.AllowEditing = false;
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(8, 42);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(1216, 788);
            this.fgList.SubtotalPosition = C1.Win.C1FlexGrid.SubtotalPositionEnum.BelowData;
            this.fgList.TabIndex = 0;
            // 
            // cmbTrxEtiology
            // 
            this.cmbTrxEtiology.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrxEtiology.FormattingEnabled = true;
            this.cmbTrxEtiology.Location = new System.Drawing.Point(150, 127);
            this.cmbTrxEtiology.Name = "cmbTrxEtiology";
            this.cmbTrxEtiology.Size = new System.Drawing.Size(329, 21);
            this.cmbTrxEtiology.TabIndex = 18;
            // 
            // frmTrx_Fees
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ClientSize = new System.Drawing.Size(1241, 841);
            this.Controls.Add(this.panEdit);
            this.Controls.Add(this.toolLeft);
            this.Controls.Add(this.fgList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmTrx_Fees";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Επιβαρύνσεις ανά κίνηση";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmTrx_Fees_Load);
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
        internal System.Windows.Forms.PictureBox picClose_Import;
        internal System.Windows.Forms.ToolStrip toolEdit;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel1;
        internal System.Windows.Forms.ToolStripButton tsbSave;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ComboBox cmbTrxType;
        internal System.Windows.Forms.Label lblTitle1;
        internal System.Windows.Forms.Label lblGroup;
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
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox txtNotes;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.ComboBox cmbRevenue;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.ComboBox cmbEarnings;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.ComboBox cmbTrxClientsFees;
        internal System.Windows.Forms.ComboBox cmbTrxEtiology;
    }
}