
namespace Contracts
{
    partial class ucAFM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucAFM));
            this.grp3 = new System.Windows.Forms.GroupBox();
            this.lblDocsCount = new System.Windows.Forms.Label();
            this.lblStatus_Grp3 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.grp2 = new System.Windows.Forms.GroupBox();
            this.txtNewAFM = new System.Windows.Forms.MaskedTextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.lblStatus_Grp2 = new System.Windows.Forms.Label();
            this.grp1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblOldAFM = new System.Windows.Forms.Label();
            this.picEdit_Grp3 = new System.Windows.Forms.PictureBox();
            this.panDocs = new System.Windows.Forms.Panel();
            this.picCancel = new System.Windows.Forms.PictureBox();
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
            this.grp3.SuspendLayout();
            this.grp2.SuspendLayout();
            this.grp1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit_Grp3)).BeginInit();
            this.panDocs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCancel)).BeginInit();
            this.toolLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgDocs)).BeginInit();
            this.SuspendLayout();
            // 
            // grp3
            // 
            this.grp3.Controls.Add(this.lblDocsCount);
            this.grp3.Controls.Add(this.lblStatus_Grp3);
            this.grp3.Controls.Add(this.label28);
            this.grp3.Controls.Add(this.label29);
            this.grp3.Location = new System.Drawing.Point(1, 107);
            this.grp3.Name = "grp3";
            this.grp3.Size = new System.Drawing.Size(467, 50);
            this.grp3.TabIndex = 1129;
            this.grp3.TabStop = false;
            // 
            // lblDocsCount
            // 
            this.lblDocsCount.AutoSize = true;
            this.lblDocsCount.Location = new System.Drawing.Point(257, 15);
            this.lblDocsCount.Name = "lblDocsCount";
            this.lblDocsCount.Size = new System.Drawing.Size(0, 13);
            this.lblDocsCount.TabIndex = 1108;
            this.lblDocsCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatus_Grp3
            // 
            this.lblStatus_Grp3.AutoSize = true;
            this.lblStatus_Grp3.ForeColor = System.Drawing.Color.Red;
            this.lblStatus_Grp3.Location = new System.Drawing.Point(6, 30);
            this.lblStatus_Grp3.Name = "lblStatus_Grp3";
            this.lblStatus_Grp3.Size = new System.Drawing.Size(47, 13);
            this.lblStatus_Grp3.TabIndex = 1105;
            this.lblStatus_Grp3.Text = "Εκκρεμή";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(134, 12);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(0, 13);
            this.label28.TabIndex = 413;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label29.Location = new System.Drawing.Point(4, 11);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(67, 15);
            this.label29.TabIndex = 412;
            this.label29.Text = "Έγγραφα";
            // 
            // lblStatus
            // 
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatus.Location = new System.Drawing.Point(474, 135);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(21, 20);
            this.lblStatus.TabIndex = 1128;
            this.lblStatus.Text = "0";
            this.lblStatus.Visible = false;
            // 
            // grp2
            // 
            this.grp2.Controls.Add(this.txtNewAFM);
            this.grp2.Controls.Add(this.label30);
            this.grp2.Controls.Add(this.lblStatus_Grp2);
            this.grp2.Location = new System.Drawing.Point(3, 51);
            this.grp2.Name = "grp2";
            this.grp2.Size = new System.Drawing.Size(490, 50);
            this.grp2.TabIndex = 1126;
            this.grp2.TabStop = false;
            // 
            // txtNewAFM
            // 
            this.txtNewAFM.Location = new System.Drawing.Point(182, 14);
            this.txtNewAFM.Mask = "999999999";
            this.txtNewAFM.Name = "txtNewAFM";
            this.txtNewAFM.Size = new System.Drawing.Size(103, 20);
            this.txtNewAFM.TabIndex = 2;
            this.txtNewAFM.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.txtNewAFM_MaskInputRejected);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label30.Location = new System.Drawing.Point(4, 14);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(68, 15);
            this.label30.TabIndex = 1105;
            this.label30.Text = "Νέο ΑΦΜ";
            // 
            // lblStatus_Grp2
            // 
            this.lblStatus_Grp2.AutoSize = true;
            this.lblStatus_Grp2.ForeColor = System.Drawing.Color.Red;
            this.lblStatus_Grp2.Location = new System.Drawing.Point(6, 30);
            this.lblStatus_Grp2.Name = "lblStatus_Grp2";
            this.lblStatus_Grp2.Size = new System.Drawing.Size(47, 13);
            this.lblStatus_Grp2.TabIndex = 1104;
            this.lblStatus_Grp2.Text = "Εκκρεμή";
            // 
            // grp1
            // 
            this.grp1.Controls.Add(this.label5);
            this.grp1.Controls.Add(this.lblOldAFM);
            this.grp1.Location = new System.Drawing.Point(3, 3);
            this.grp1.Name = "grp1";
            this.grp1.Size = new System.Drawing.Size(490, 42);
            this.grp1.TabIndex = 1125;
            this.grp1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label5.Location = new System.Drawing.Point(4, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 15);
            this.label5.TabIndex = 416;
            this.label5.Text = "Παλιό ΑΦΜ";
            // 
            // lblOldAFM
            // 
            this.lblOldAFM.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblOldAFM.Location = new System.Drawing.Point(182, 11);
            this.lblOldAFM.Name = "lblOldAFM";
            this.lblOldAFM.Size = new System.Drawing.Size(271, 20);
            this.lblOldAFM.TabIndex = 415;
            // 
            // picEdit_Grp3
            // 
            this.picEdit_Grp3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picEdit_Grp3.Image = global::Contracts.Properties.Resources.ArRight;
            this.picEdit_Grp3.Location = new System.Drawing.Point(474, 114);
            this.picEdit_Grp3.Name = "picEdit_Grp3";
            this.picEdit_Grp3.Size = new System.Drawing.Size(18, 18);
            this.picEdit_Grp3.TabIndex = 1130;
            this.picEdit_Grp3.TabStop = false;
            this.picEdit_Grp3.Click += new System.EventHandler(this.picEdit_Grp3_Click);
            // 
            // panDocs
            // 
            this.panDocs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panDocs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panDocs.Controls.Add(this.picCancel);
            this.panDocs.Controls.Add(this.lblLoadDocs);
            this.panDocs.Controls.Add(this.toolLeft);
            this.panDocs.Controls.Add(this.fgDocs);
            this.panDocs.Controls.Add(this.btnSave3);
            this.panDocs.Location = new System.Drawing.Point(524, 14);
            this.panDocs.Name = "panDocs";
            this.panDocs.Size = new System.Drawing.Size(445, 249);
            this.panDocs.TabIndex = 1133;
            this.panDocs.Visible = false;
            // 
            // picCancel
            // 
            this.picCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picCancel.Image = global::Contracts.Properties.Resources.cancel1;
            this.picCancel.Location = new System.Drawing.Point(422, 3);
            this.picCancel.Name = "picCancel";
            this.picCancel.Size = new System.Drawing.Size(18, 18);
            this.picCancel.TabIndex = 1097;
            this.picCancel.TabStop = false;
            this.picCancel.Click += new System.EventHandler(this.picCancel_Click);
            // 
            // lblLoadDocs
            // 
            this.lblLoadDocs.Location = new System.Drawing.Point(107, 11);
            this.lblLoadDocs.Name = "lblLoadDocs";
            this.lblLoadDocs.Size = new System.Drawing.Size(231, 19);
            this.lblLoadDocs.TabIndex = 483;
            this.lblLoadDocs.Text = "Ανεβάστε ένα αντίγραφο του ΑΦΜ σας";
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
            this.fgDocs.Location = new System.Drawing.Point(11, 71);
            this.fgDocs.Name = "fgDocs";
            this.fgDocs.Rows.Count = 1;
            this.fgDocs.Rows.DefaultSize = 17;
            this.fgDocs.Size = new System.Drawing.Size(425, 133);
            this.fgDocs.TabIndex = 4;
            // 
            // btnSave3
            // 
            this.btnSave3.Location = new System.Drawing.Point(181, 213);
            this.btnSave3.Name = "btnSave3";
            this.btnSave3.Size = new System.Drawing.Size(91, 25);
            this.btnSave3.TabIndex = 8;
            this.btnSave3.Text = "Καταχώρηση";
            this.btnSave3.UseVisualStyleBackColor = true;
            this.btnSave3.Click += new System.EventHandler(this.btnSave3_Click);
            // 
            // ucAFM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panDocs);
            this.Controls.Add(this.grp3);
            this.Controls.Add(this.picEdit_Grp3);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.grp2);
            this.Controls.Add(this.grp1);
            this.Name = "ucAFM";
            this.Size = new System.Drawing.Size(976, 406);
            this.Load += new System.EventHandler(this.ucAFM_Load);
            this.grp3.ResumeLayout(false);
            this.grp3.PerformLayout();
            this.grp2.ResumeLayout(false);
            this.grp2.PerformLayout();
            this.grp1.ResumeLayout(false);
            this.grp1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit_Grp3)).EndInit();
            this.panDocs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCancel)).EndInit();
            this.toolLeft.ResumeLayout(false);
            this.toolLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgDocs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp3;
        private System.Windows.Forms.Label lblDocsCount;
        private System.Windows.Forms.Label lblStatus_Grp3;
        public System.Windows.Forms.Label label28;
        public System.Windows.Forms.Label label29;
        internal System.Windows.Forms.PictureBox picEdit_Grp3;
        public System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox grp2;
        public System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label lblStatus_Grp2;
        private System.Windows.Forms.GroupBox grp1;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label lblOldAFM;
        private System.Windows.Forms.Panel panDocs;
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
        internal System.Windows.Forms.PictureBox picCancel;
        public System.Windows.Forms.MaskedTextBox txtNewAFM;
    }
}
