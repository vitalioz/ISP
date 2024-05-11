namespace Contracts
{
    partial class ucBankAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucBankAccount));
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.cmbCurrencies = new System.Windows.Forms.ComboBox();
            this.txtAccNumber = new System.Windows.Forms.TextBox();
            this.cmbBanks = new System.Windows.Forms.ComboBox();
            this.txtOwners = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.panDocs = new System.Windows.Forms.Panel();
            this.picCancel3 = new System.Windows.Forms.PictureBox();
            this.lblLoadDocs = new System.Windows.Forms.Label();
            this.toolLeft = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator29 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbView = new System.Windows.Forms.ToolStripButton();
            this.fgDocs = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnSave3 = new System.Windows.Forms.Button();
            this.picEdit_Grp4 = new System.Windows.Forms.PictureBox();
            this.grp4 = new System.Windows.Forms.GroupBox();
            this.lnkEmail = new System.Windows.Forms.LinkLabel();
            this.lblStatus_Grp4 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.picEdit_Grp3 = new System.Windows.Forms.PictureBox();
            this.grp3 = new System.Windows.Forms.GroupBox();
            this.lblDocsCount = new System.Windows.Forms.Label();
            this.lblStatus_Grp3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.grp2 = new System.Windows.Forms.GroupBox();
            this.lblNewTitle = new System.Windows.Forms.Label();
            this.lblEmail_ID = new System.Windows.Forms.Label();
            this.panDocs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCancel3)).BeginInit();
            this.toolLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgDocs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit_Grp4)).BeginInit();
            this.grp4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit_Grp3)).BeginInit();
            this.grp3.SuspendLayout();
            this.grp2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "ΟΧΙ",
            "ΝΑΙ"});
            this.cmbType.Location = new System.Drawing.Point(120, 117);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(92, 21);
            this.cmbType.TabIndex = 508;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // cmbCurrencies
            // 
            this.cmbCurrencies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrencies.FormattingEnabled = true;
            this.cmbCurrencies.Location = new System.Drawing.Point(120, 87);
            this.cmbCurrencies.Name = "cmbCurrencies";
            this.cmbCurrencies.Size = new System.Drawing.Size(92, 21);
            this.cmbCurrencies.TabIndex = 506;
            this.cmbCurrencies.SelectedValueChanged += new System.EventHandler(this.cmbCurrencies_SelectedValueChanged);
            // 
            // txtAccNumber
            // 
            this.txtAccNumber.Location = new System.Drawing.Point(120, 33);
            this.txtAccNumber.Name = "txtAccNumber";
            this.txtAccNumber.Size = new System.Drawing.Size(360, 20);
            this.txtAccNumber.TabIndex = 500;
            this.txtAccNumber.LostFocus += new System.EventHandler(this.txtAccNumber_LostFocus);
            // 
            // cmbBanks
            // 
            this.cmbBanks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBanks.FormattingEnabled = true;
            this.cmbBanks.Location = new System.Drawing.Point(120, 59);
            this.cmbBanks.Name = "cmbBanks";
            this.cmbBanks.Size = new System.Drawing.Size(360, 21);
            this.cmbBanks.TabIndex = 502;
            this.cmbBanks.SelectedValueChanged += new System.EventHandler(this.cmbBanks_SelectedValueChanged);
            // 
            // txtOwners
            // 
            this.txtOwners.Location = new System.Drawing.Point(120, 147);
            this.txtOwners.Name = "txtOwners";
            this.txtOwners.Size = new System.Drawing.Size(362, 20);
            this.txtOwners.TabIndex = 510;
            this.txtOwners.LostFocus += new System.EventHandler(this.txtOwners_LostFocus);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 502;
            this.label7.Text = "Συνδικαιούχοι";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 83;
            this.label3.Text = "Αρ.Λογαριασμόυ";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(16, 62);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(50, 13);
            this.label36.TabIndex = 82;
            this.label36.Text = "Τράπεζα";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(16, 90);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(49, 13);
            this.label56.TabIndex = 85;
            this.label56.Text = "Νόμισμα";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(16, 119);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(40, 13);
            this.label68.TabIndex = 87;
            this.label68.Text = "Κοινός";
            // 
            // panDocs
            // 
            this.panDocs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panDocs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panDocs.Controls.Add(this.picCancel3);
            this.panDocs.Controls.Add(this.lblLoadDocs);
            this.panDocs.Controls.Add(this.toolLeft);
            this.panDocs.Controls.Add(this.fgDocs);
            this.panDocs.Controls.Add(this.btnSave3);
            this.panDocs.Location = new System.Drawing.Point(505, 9);
            this.panDocs.Name = "panDocs";
            this.panDocs.Size = new System.Drawing.Size(445, 249);
            this.panDocs.TabIndex = 1109;
            this.panDocs.Visible = false;
            // 
            // picCancel3
            // 
            this.picCancel3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picCancel3.Image = global::Contracts.Properties.Resources.cancel1;
            this.picCancel3.Location = new System.Drawing.Point(420, 7);
            this.picCancel3.Name = "picCancel3";
            this.picCancel3.Size = new System.Drawing.Size(18, 18);
            this.picCancel3.TabIndex = 1095;
            this.picCancel3.TabStop = false;
            this.picCancel3.Click += new System.EventHandler(this.picCancel3_Click);
            // 
            // lblLoadDocs
            // 
            this.lblLoadDocs.Location = new System.Drawing.Point(66, 12);
            this.lblLoadDocs.Name = "lblLoadDocs";
            this.lblLoadDocs.Size = new System.Drawing.Size(279, 19);
            this.lblLoadDocs.TabIndex = 483;
            this.lblLoadDocs.Text = "Ανεβάστε αντίγραφο του τραπεζικού λογαρισμού";
            this.lblLoadDocs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.toolStripLabel1,
            this.tsbAdd,
            this.ToolStripSeparator29,
            this.tsbDelete,
            this.toolStripSeparator1,
            this.tsbView});
            this.toolLeft.Location = new System.Drawing.Point(11, 39);
            this.toolLeft.Name = "toolLeft";
            this.toolLeft.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolLeft.Size = new System.Drawing.Size(111, 26);
            this.toolLeft.TabIndex = 484;
            this.toolLeft.Text = "ToolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(13, 23);
            this.toolStripLabel1.Text = "  ";
            // 
            // tsbAdd
            // 
            this.tsbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAdd.Image = global::Contracts.Properties.Resources.plus;
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(23, 23);
            this.tsbAdd.Text = "Προσθήκη";
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // ToolStripSeparator29
            // 
            this.ToolStripSeparator29.Name = "ToolStripSeparator29";
            this.ToolStripSeparator29.Size = new System.Drawing.Size(6, 26);
            // 
            // tsbDelete
            // 
            this.tsbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDelete.Image = global::Contracts.Properties.Resources.minus;
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(23, 23);
            this.tsbDelete.Text = "Διαγραφή";
            this.tsbDelete.Click += new System.EventHandler(this.tsbDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
            // 
            // tsbView
            // 
            this.tsbView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbView.Image = global::Contracts.Properties.Resources.eye;
            this.tsbView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbView.Name = "tsbView";
            this.tsbView.Size = new System.Drawing.Size(23, 23);
            this.tsbView.Text = "Προβολή αρχείου";
            this.tsbView.Click += new System.EventHandler(this.tsbView_Click);
            // 
            // fgDocs
            // 
            this.fgDocs.ColumnInfo = resources.GetString("fgDocs.ColumnInfo");
            this.fgDocs.Location = new System.Drawing.Point(8, 71);
            this.fgDocs.Name = "fgDocs";
            this.fgDocs.Rows.Count = 1;
            this.fgDocs.Rows.DefaultSize = 17;
            this.fgDocs.Size = new System.Drawing.Size(425, 133);
            this.fgDocs.TabIndex = 20;
            // 
            // btnSave3
            // 
            this.btnSave3.Location = new System.Drawing.Point(178, 210);
            this.btnSave3.Name = "btnSave3";
            this.btnSave3.Size = new System.Drawing.Size(91, 25);
            this.btnSave3.TabIndex = 24;
            this.btnSave3.Text = "Καταχώρηση";
            this.btnSave3.UseVisualStyleBackColor = true;
            this.btnSave3.Click += new System.EventHandler(this.btnSave3_Click);
            // 
            // picEdit_Grp4
            // 
            this.picEdit_Grp4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picEdit_Grp4.Image = global::Contracts.Properties.Resources.ArRight;
            this.picEdit_Grp4.Location = new System.Drawing.Point(475, 246);
            this.picEdit_Grp4.Name = "picEdit_Grp4";
            this.picEdit_Grp4.Size = new System.Drawing.Size(18, 18);
            this.picEdit_Grp4.TabIndex = 1114;
            this.picEdit_Grp4.TabStop = false;
            this.picEdit_Grp4.Click += new System.EventHandler(this.picEdit_Grp4_Click);
            // 
            // grp4
            // 
            this.grp4.Controls.Add(this.lnkEmail);
            this.grp4.Controls.Add(this.lblStatus_Grp4);
            this.grp4.Controls.Add(this.label16);
            this.grp4.Location = new System.Drawing.Point(3, 240);
            this.grp4.Name = "grp4";
            this.grp4.Size = new System.Drawing.Size(468, 56);
            this.grp4.TabIndex = 1113;
            this.grp4.TabStop = false;
            // 
            // lnkEmail
            // 
            this.lnkEmail.AutoSize = true;
            this.lnkEmail.Location = new System.Drawing.Point(160, 16);
            this.lnkEmail.Name = "lnkEmail";
            this.lnkEmail.Size = new System.Drawing.Size(0, 13);
            this.lnkEmail.TabIndex = 1108;
            this.lnkEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEmail_LinkClicked_1);
            // 
            // lblStatus_Grp4
            // 
            this.lblStatus_Grp4.AutoSize = true;
            this.lblStatus_Grp4.ForeColor = System.Drawing.Color.Red;
            this.lblStatus_Grp4.Location = new System.Drawing.Point(8, 34);
            this.lblStatus_Grp4.Name = "lblStatus_Grp4";
            this.lblStatus_Grp4.Size = new System.Drawing.Size(47, 13);
            this.lblStatus_Grp4.TabIndex = 1104;
            this.lblStatus_Grp4.Text = "Εκκρεμή";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label16.Location = new System.Drawing.Point(6, 11);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(125, 15);
            this.label16.TabIndex = 1102;
            this.label16.Text = "Εντολή του πελάτη";
            // 
            // lblStatus
            // 
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatus.Location = new System.Drawing.Point(477, 217);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(18, 20);
            this.lblStatus.TabIndex = 1112;
            this.lblStatus.Text = "0";
            this.lblStatus.Visible = false;
            // 
            // picEdit_Grp3
            // 
            this.picEdit_Grp3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picEdit_Grp3.Image = global::Contracts.Properties.Resources.ArRight;
            this.picEdit_Grp3.Location = new System.Drawing.Point(475, 191);
            this.picEdit_Grp3.Name = "picEdit_Grp3";
            this.picEdit_Grp3.Size = new System.Drawing.Size(18, 18);
            this.picEdit_Grp3.TabIndex = 1111;
            this.picEdit_Grp3.TabStop = false;
            this.picEdit_Grp3.Click += new System.EventHandler(this.picEdit_Grp3_Click);
            // 
            // grp3
            // 
            this.grp3.Controls.Add(this.lblDocsCount);
            this.grp3.Controls.Add(this.lblStatus_Grp3);
            this.grp3.Controls.Add(this.label10);
            this.grp3.Location = new System.Drawing.Point(3, 183);
            this.grp3.Name = "grp3";
            this.grp3.Size = new System.Drawing.Size(468, 56);
            this.grp3.TabIndex = 1108;
            this.grp3.TabStop = false;
            // 
            // lblDocsCount
            // 
            this.lblDocsCount.AutoSize = true;
            this.lblDocsCount.Location = new System.Drawing.Point(160, 16);
            this.lblDocsCount.Name = "lblDocsCount";
            this.lblDocsCount.Size = new System.Drawing.Size(0, 13);
            this.lblDocsCount.TabIndex = 1107;
            this.lblDocsCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatus_Grp3
            // 
            this.lblStatus_Grp3.AutoSize = true;
            this.lblStatus_Grp3.ForeColor = System.Drawing.Color.Red;
            this.lblStatus_Grp3.Location = new System.Drawing.Point(8, 34);
            this.lblStatus_Grp3.Name = "lblStatus_Grp3";
            this.lblStatus_Grp3.Size = new System.Drawing.Size(47, 13);
            this.lblStatus_Grp3.TabIndex = 1104;
            this.lblStatus_Grp3.Text = "Εκκρεμή";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label10.Location = new System.Drawing.Point(6, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 15);
            this.label10.TabIndex = 1102;
            this.label10.Text = "Έγγραφα";
            // 
            // grp2
            // 
            this.grp2.Controls.Add(this.lblNewTitle);
            this.grp2.Controls.Add(this.txtAccNumber);
            this.grp2.Controls.Add(this.cmbType);
            this.grp2.Controls.Add(this.label68);
            this.grp2.Controls.Add(this.cmbCurrencies);
            this.grp2.Controls.Add(this.label56);
            this.grp2.Controls.Add(this.label36);
            this.grp2.Controls.Add(this.cmbBanks);
            this.grp2.Controls.Add(this.label3);
            this.grp2.Controls.Add(this.txtOwners);
            this.grp2.Controls.Add(this.label7);
            this.grp2.Location = new System.Drawing.Point(3, 3);
            this.grp2.Name = "grp2";
            this.grp2.Size = new System.Drawing.Size(490, 174);
            this.grp2.TabIndex = 1107;
            this.grp2.TabStop = false;
            // 
            // lblNewTitle
            // 
            this.lblNewTitle.AutoSize = true;
            this.lblNewTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblNewTitle.Location = new System.Drawing.Point(4, 11);
            this.lblNewTitle.Name = "lblNewTitle";
            this.lblNewTitle.Size = new System.Drawing.Size(199, 15);
            this.lblNewTitle.TabIndex = 412;
            this.lblNewTitle.Text = "Νέος τραπεζικός λογαριασμός";
            // 
            // lblEmail_ID
            // 
            this.lblEmail_ID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEmail_ID.Location = new System.Drawing.Point(477, 276);
            this.lblEmail_ID.Name = "lblEmail_ID";
            this.lblEmail_ID.Size = new System.Drawing.Size(19, 20);
            this.lblEmail_ID.TabIndex = 1115;
            this.lblEmail_ID.Visible = false;
            // 
            // ucBankAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblEmail_ID);
            this.Controls.Add(this.panDocs);
            this.Controls.Add(this.picEdit_Grp4);
            this.Controls.Add(this.grp4);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.picEdit_Grp3);
            this.Controls.Add(this.grp3);
            this.Controls.Add(this.grp2);
            this.Name = "ucBankAccount";
            this.Size = new System.Drawing.Size(967, 391);
            this.panDocs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCancel3)).EndInit();
            this.toolLeft.ResumeLayout(false);
            this.toolLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgDocs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit_Grp4)).EndInit();
            this.grp4.ResumeLayout(false);
            this.grp4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit_Grp3)).EndInit();
            this.grp3.ResumeLayout(false);
            this.grp3.PerformLayout();
            this.grp2.ResumeLayout(false);
            this.grp2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.ComboBox cmbType;
        internal System.Windows.Forms.ComboBox cmbCurrencies;
        internal System.Windows.Forms.TextBox txtAccNumber;
        internal System.Windows.Forms.ComboBox cmbBanks;
        internal System.Windows.Forms.TextBox txtOwners;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label36;
        internal System.Windows.Forms.Label label56;
        internal System.Windows.Forms.Label label68;
        private System.Windows.Forms.Panel panDocs;
        internal System.Windows.Forms.PictureBox picCancel3;
        private System.Windows.Forms.Label lblLoadDocs;
        internal System.Windows.Forms.ToolStrip toolLeft;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        internal System.Windows.Forms.ToolStripButton tsbAdd;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator29;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbView;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgDocs;
        private System.Windows.Forms.Button btnSave3;
        internal System.Windows.Forms.PictureBox picEdit_Grp4;
        private System.Windows.Forms.GroupBox grp4;
        private System.Windows.Forms.Label lblStatus_Grp4;
        public System.Windows.Forms.Label label16;
        public System.Windows.Forms.Label lblStatus;
        internal System.Windows.Forms.PictureBox picEdit_Grp3;
        private System.Windows.Forms.GroupBox grp3;
        private System.Windows.Forms.Label lblDocsCount;
        private System.Windows.Forms.Label lblStatus_Grp3;
        public System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox grp2;
        public System.Windows.Forms.Label lblNewTitle;
        public System.Windows.Forms.Label lblEmail_ID;
        public System.Windows.Forms.LinkLabel lnkEmail;
    }
}
