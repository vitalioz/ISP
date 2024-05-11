namespace Contracts
{
    partial class ucCoOwner
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucCoOwner));
            this.lblStatus = new System.Windows.Forms.Label();
            this.panDocs = new System.Windows.Forms.Panel();
            this.picCancel2 = new System.Windows.Forms.PictureBox();
            this.lblLoadDocs = new System.Windows.Forms.Label();
            this.toolLeft = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator29 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbView = new System.Windows.Forms.ToolStripButton();
            this.fgDocs = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnSave2 = new System.Windows.Forms.Button();
            this.lblDocsCount = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblNewTitle = new System.Windows.Forms.Label();
            this.lblStatus_Grp2 = new System.Windows.Forms.Label();
            this.picEdit_Grp2 = new System.Windows.Forms.PictureBox();
            this.grp1 = new System.Windows.Forms.GroupBox();
            this.txtAFM = new System.Windows.Forms.MaskedTextBox();
            this.lblClient2_ID = new System.Windows.Forms.Label();
            this.lblStatus_Grp1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ucCS = new Core.ucClientSearch();
            this.grp2 = new System.Windows.Forms.GroupBox();
            this.panDocs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCancel2)).BeginInit();
            this.toolLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgDocs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit_Grp2)).BeginInit();
            this.grp1.SuspendLayout();
            this.grp2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatus.Location = new System.Drawing.Point(482, 164);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(18, 20);
            this.lblStatus.TabIndex = 1120;
            this.lblStatus.Text = "0";
            this.lblStatus.Visible = false;
            // 
            // panDocs
            // 
            this.panDocs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panDocs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panDocs.Controls.Add(this.picCancel2);
            this.panDocs.Controls.Add(this.lblLoadDocs);
            this.panDocs.Controls.Add(this.toolLeft);
            this.panDocs.Controls.Add(this.fgDocs);
            this.panDocs.Controls.Add(this.btnSave2);
            this.panDocs.Location = new System.Drawing.Point(512, 20);
            this.panDocs.Name = "panDocs";
            this.panDocs.Size = new System.Drawing.Size(445, 249);
            this.panDocs.TabIndex = 1117;
            this.panDocs.Visible = false;
            // 
            // picCancel2
            // 
            this.picCancel2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picCancel2.Image = global::Contracts.Properties.Resources.cancel1;
            this.picCancel2.Location = new System.Drawing.Point(420, 7);
            this.picCancel2.Name = "picCancel2";
            this.picCancel2.Size = new System.Drawing.Size(18, 18);
            this.picCancel2.TabIndex = 1095;
            this.picCancel2.TabStop = false;
            this.picCancel2.Click += new System.EventHandler(this.picCancel2_Click);
            // 
            // lblLoadDocs
            // 
            this.lblLoadDocs.Location = new System.Drawing.Point(56, 17);
            this.lblLoadDocs.Name = "lblLoadDocs";
            this.lblLoadDocs.Size = new System.Drawing.Size(347, 19);
            this.lblLoadDocs.TabIndex = 483;
            this.lblLoadDocs.Text = "Ανεβάστε αντίγραφο του νέου συνδεδεμένου προσώπου (ενήλικο)";
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
            this.fgDocs.TabIndex = 520;
            // 
            // btnSave2
            // 
            this.btnSave2.Location = new System.Drawing.Point(178, 210);
            this.btnSave2.Name = "btnSave2";
            this.btnSave2.Size = new System.Drawing.Size(91, 25);
            this.btnSave2.TabIndex = 24;
            this.btnSave2.Text = "Καταχώρηση";
            this.btnSave2.UseVisualStyleBackColor = true;
            this.btnSave2.Click += new System.EventHandler(this.btnSave2_Click);
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
            // lblNewTitle
            // 
            this.lblNewTitle.AutoSize = true;
            this.lblNewTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblNewTitle.Location = new System.Drawing.Point(4, 11);
            this.lblNewTitle.Name = "lblNewTitle";
            this.lblNewTitle.Size = new System.Drawing.Size(134, 15);
            this.lblNewTitle.TabIndex = 412;
            this.lblNewTitle.Text = "Συνδέστε το ενήλικο";
            // 
            // lblStatus_Grp2
            // 
            this.lblStatus_Grp2.AutoSize = true;
            this.lblStatus_Grp2.ForeColor = System.Drawing.Color.Red;
            this.lblStatus_Grp2.Location = new System.Drawing.Point(19, 34);
            this.lblStatus_Grp2.Name = "lblStatus_Grp2";
            this.lblStatus_Grp2.Size = new System.Drawing.Size(47, 13);
            this.lblStatus_Grp2.TabIndex = 1104;
            this.lblStatus_Grp2.Text = "Εκκρεμή";
            // 
            // picEdit_Grp2
            // 
            this.picEdit_Grp2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picEdit_Grp2.Image = global::Contracts.Properties.Resources.ArRight;
            this.picEdit_Grp2.Location = new System.Drawing.Point(482, 138);
            this.picEdit_Grp2.Name = "picEdit_Grp2";
            this.picEdit_Grp2.Size = new System.Drawing.Size(18, 18);
            this.picEdit_Grp2.TabIndex = 1119;
            this.picEdit_Grp2.TabStop = false;
            this.picEdit_Grp2.Click += new System.EventHandler(this.picEdit_Grp2_Click);
            // 
            // grp1
            // 
            this.grp1.Controls.Add(this.txtAFM);
            this.grp1.Controls.Add(this.lblClient2_ID);
            this.grp1.Controls.Add(this.lblStatus_Grp1);
            this.grp1.Controls.Add(this.label1);
            this.grp1.Controls.Add(this.lblNewTitle);
            this.grp1.Controls.Add(this.label7);
            this.grp1.Location = new System.Drawing.Point(10, 6);
            this.grp1.Name = "grp1";
            this.grp1.Size = new System.Drawing.Size(490, 123);
            this.grp1.TabIndex = 1115;
            this.grp1.TabStop = false;
            // 
            // txtAFM
            // 
            this.txtAFM.Location = new System.Drawing.Point(120, 62);
            this.txtAFM.Mask = "999999999";
            this.txtAFM.Name = "txtAFM";
            this.txtAFM.Size = new System.Drawing.Size(103, 20);
            this.txtAFM.TabIndex = 1126;
            // 
            // lblClient2_ID
            // 
            this.lblClient2_ID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClient2_ID.Location = new System.Drawing.Point(457, 55);
            this.lblClient2_ID.Name = "lblClient2_ID";
            this.lblClient2_ID.Size = new System.Drawing.Size(24, 20);
            this.lblClient2_ID.TabIndex = 1124;
            this.lblClient2_ID.Visible = false;
            // 
            // lblStatus_Grp1
            // 
            this.lblStatus_Grp1.AutoSize = true;
            this.lblStatus_Grp1.ForeColor = System.Drawing.Color.Red;
            this.lblStatus_Grp1.Location = new System.Drawing.Point(18, 96);
            this.lblStatus_Grp1.Name = "lblStatus_Grp1";
            this.lblStatus_Grp1.Size = new System.Drawing.Size(47, 13);
            this.lblStatus_Grp1.TabIndex = 1104;
            this.lblStatus_Grp1.Text = "Εκκρεμή";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 511;
            this.label1.Text = "ΑΦΜ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 502;
            this.label7.Text = "Επώνυμο";
            // 
            // ucCS
            // 
            this.ucCS.Filters = "ID > 0";
            this.ucCS.ListType = 0;
            this.ucCS.Location = new System.Drawing.Point(130, 38);
            this.ucCS.Mode = 0;
            this.ucCS.Name = "ucCS";
            this.ucCS.ShowClientsList = true;
            this.ucCS.ShowHeight = 0;
            this.ucCS.ShowWidth = 0;
            this.ucCS.Size = new System.Drawing.Size(200, 20);
            this.ucCS.TabIndex = 1124;
            // 
            // grp2
            // 
            this.grp2.Controls.Add(this.lblDocsCount);
            this.grp2.Controls.Add(this.lblStatus_Grp2);
            this.grp2.Controls.Add(this.label10);
            this.grp2.Location = new System.Drawing.Point(10, 130);
            this.grp2.Name = "grp2";
            this.grp2.Size = new System.Drawing.Size(468, 56);
            this.grp2.TabIndex = 1116;
            this.grp2.TabStop = false;
            // 
            // ucCoOwner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panDocs);
            this.Controls.Add(this.ucCS);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.picEdit_Grp2);
            this.Controls.Add(this.grp1);
            this.Controls.Add(this.grp2);
            this.Name = "ucCoOwner";
            this.Size = new System.Drawing.Size(967, 391);
            this.Load += new System.EventHandler(this.ucCoOwner_Load);
            this.panDocs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCancel2)).EndInit();
            this.toolLeft.ResumeLayout(false);
            this.toolLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgDocs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit_Grp2)).EndInit();
            this.grp1.ResumeLayout(false);
            this.grp1.PerformLayout();
            this.grp2.ResumeLayout(false);
            this.grp2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panDocs;
        internal System.Windows.Forms.PictureBox picCancel2;
        private System.Windows.Forms.Label lblLoadDocs;
        internal System.Windows.Forms.ToolStrip toolLeft;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        internal System.Windows.Forms.ToolStripButton tsbAdd;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator29;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbView;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgDocs;
        private System.Windows.Forms.Button btnSave2;
        private System.Windows.Forms.Label lblDocsCount;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.Label lblNewTitle;
        private System.Windows.Forms.Label lblStatus_Grp2;
        internal System.Windows.Forms.PictureBox picEdit_Grp2;
        private System.Windows.Forms.GroupBox grp1;
        internal System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox grp2;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStatus_Grp1;
        public System.Windows.Forms.Label lblClient2_ID;
        public Core.ucClientSearch ucCS;
        public System.Windows.Forms.MaskedTextBox txtAFM;
    }
}
