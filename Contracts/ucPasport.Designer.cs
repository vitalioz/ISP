
namespace Contracts
{
    partial class ucPasport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPasport));
            this.lblOldNumber = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblOldExpireDate = new System.Windows.Forms.Label();
            this.picCancel3 = new System.Windows.Forms.PictureBox();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbView = new System.Windows.Forms.ToolStripButton();
            this.panDocs = new System.Windows.Forms.Panel();
            this.lblLoadDocs = new System.Windows.Forms.Label();
            this.toolLeft = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.ToolStripSeparator29 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.fgDocs = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnSave3 = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.picEdit_Grp3 = new System.Windows.Forms.PictureBox();
            this.grp1 = new System.Windows.Forms.GroupBox();
            this.lblOldPolice = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.grp3 = new System.Windows.Forms.GroupBox();
            this.lblDocsCount = new System.Windows.Forms.Label();
            this.lblStatus_Grp3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.grp2 = new System.Windows.Forms.GroupBox();
            this.txtNewPolice = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dNewExpireDate = new System.Windows.Forms.DateTimePicker();
            this.lblStatus_Grp2 = new System.Windows.Forms.Label();
            this.lblNewTitle = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNewNumber = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picCancel3)).BeginInit();
            this.panDocs.SuspendLayout();
            this.toolLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgDocs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit_Grp3)).BeginInit();
            this.grp1.SuspendLayout();
            this.grp3.SuspendLayout();
            this.grp2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblOldNumber
            // 
            this.lblOldNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblOldNumber.Location = new System.Drawing.Point(213, 11);
            this.lblOldNumber.Name = "lblOldNumber";
            this.lblOldNumber.Size = new System.Drawing.Size(102, 20);
            this.lblOldNumber.TabIndex = 415;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label5.Location = new System.Drawing.Point(4, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 15);
            this.label5.TabIndex = 412;
            this.label5.Text = "Παλαιό διαβατήριο";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(132, 64);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 13);
            this.label12.TabIndex = 1110;
            this.label12.Text = "Ημ/νία Λήξης";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(132, 13);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 13);
            this.label14.TabIndex = 1108;
            this.label14.Text = "Aριθμός";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(156, 11);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(0, 13);
            this.label15.TabIndex = 1107;
            // 
            // lblOldExpireDate
            // 
            this.lblOldExpireDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblOldExpireDate.Location = new System.Drawing.Point(213, 59);
            this.lblOldExpireDate.Name = "lblOldExpireDate";
            this.lblOldExpireDate.Size = new System.Drawing.Size(102, 20);
            this.lblOldExpireDate.TabIndex = 417;
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
            // panDocs
            // 
            this.panDocs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panDocs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panDocs.Controls.Add(this.picCancel3);
            this.panDocs.Controls.Add(this.lblLoadDocs);
            this.panDocs.Controls.Add(this.toolLeft);
            this.panDocs.Controls.Add(this.fgDocs);
            this.panDocs.Controls.Add(this.btnSave3);
            this.panDocs.Location = new System.Drawing.Point(516, 23);
            this.panDocs.Name = "panDocs";
            this.panDocs.Size = new System.Drawing.Size(445, 249);
            this.panDocs.TabIndex = 1109;
            this.panDocs.Visible = false;
            // 
            // lblLoadDocs
            // 
            this.lblLoadDocs.Location = new System.Drawing.Point(70, 15);
            this.lblLoadDocs.Name = "lblLoadDocs";
            this.lblLoadDocs.Size = new System.Drawing.Size(279, 19);
            this.lblLoadDocs.TabIndex = 483;
            this.lblLoadDocs.Text = "Ανεβάστε ένα αντίγραφο του νέου διαβατηρίου";
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
            // ToolStripSeparator29
            // 
            this.ToolStripSeparator29.Name = "ToolStripSeparator29";
            this.ToolStripSeparator29.Size = new System.Drawing.Size(6, 26);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
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
            this.btnSave3.Location = new System.Drawing.Point(171, 210);
            this.btnSave3.Name = "btnSave3";
            this.btnSave3.Size = new System.Drawing.Size(91, 25);
            this.btnSave3.TabIndex = 24;
            this.btnSave3.Text = "Καταχώρηση";
            this.btnSave3.UseVisualStyleBackColor = true;
            this.btnSave3.Click += new System.EventHandler(this.btnSave3_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatus.Location = new System.Drawing.Point(476, 214);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(20, 20);
            this.lblStatus.TabIndex = 1112;
            this.lblStatus.Text = "0";
            this.lblStatus.Visible = false;
            // 
            // picEdit_Grp3
            // 
            this.picEdit_Grp3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picEdit_Grp3.Image = global::Contracts.Properties.Resources.ArRight;
            this.picEdit_Grp3.Location = new System.Drawing.Point(476, 189);
            this.picEdit_Grp3.Name = "picEdit_Grp3";
            this.picEdit_Grp3.Size = new System.Drawing.Size(18, 18);
            this.picEdit_Grp3.TabIndex = 1111;
            this.picEdit_Grp3.TabStop = false;
            this.picEdit_Grp3.Click += new System.EventHandler(this.picEdit_Grp3_Click);
            // 
            // grp1
            // 
            this.grp1.Controls.Add(this.lblOldPolice);
            this.grp1.Controls.Add(this.label13);
            this.grp1.Controls.Add(this.lblOldExpireDate);
            this.grp1.Controls.Add(this.lblOldNumber);
            this.grp1.Controls.Add(this.label5);
            this.grp1.Controls.Add(this.label12);
            this.grp1.Controls.Add(this.label14);
            this.grp1.Controls.Add(this.label15);
            this.grp1.Location = new System.Drawing.Point(6, 5);
            this.grp1.Name = "grp1";
            this.grp1.Size = new System.Drawing.Size(490, 86);
            this.grp1.TabIndex = 1106;
            this.grp1.TabStop = false;
            // 
            // lblOldPolice
            // 
            this.lblOldPolice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblOldPolice.Location = new System.Drawing.Point(213, 35);
            this.lblOldPolice.Name = "lblOldPolice";
            this.lblOldPolice.Size = new System.Drawing.Size(269, 20);
            this.lblOldPolice.TabIndex = 1111;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(132, 38);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(79, 13);
            this.label13.TabIndex = 1112;
            this.label13.Text = "Αρχή Έκδοσης";
            // 
            // grp3
            // 
            this.grp3.Controls.Add(this.lblDocsCount);
            this.grp3.Controls.Add(this.lblStatus_Grp3);
            this.grp3.Controls.Add(this.label10);
            this.grp3.Location = new System.Drawing.Point(6, 181);
            this.grp3.Name = "grp3";
            this.grp3.Size = new System.Drawing.Size(465, 56);
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
            this.grp2.Controls.Add(this.txtNewPolice);
            this.grp2.Controls.Add(this.label7);
            this.grp2.Controls.Add(this.dNewExpireDate);
            this.grp2.Controls.Add(this.lblStatus_Grp2);
            this.grp2.Controls.Add(this.lblNewTitle);
            this.grp2.Controls.Add(this.label6);
            this.grp2.Controls.Add(this.label9);
            this.grp2.Controls.Add(this.txtNewNumber);
            this.grp2.Location = new System.Drawing.Point(6, 91);
            this.grp2.Name = "grp2";
            this.grp2.Size = new System.Drawing.Size(490, 85);
            this.grp2.TabIndex = 1107;
            this.grp2.TabStop = false;
            // 
            // txtNewPolice
            // 
            this.txtNewPolice.Location = new System.Drawing.Point(213, 36);
            this.txtNewPolice.Name = "txtNewPolice";
            this.txtNewPolice.Size = new System.Drawing.Size(269, 20);
            this.txtNewPolice.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(132, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 1108;
            this.label7.Text = "Αρχή Έκδοσης";
            // 
            // dNewExpireDate
            // 
            this.dNewExpireDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dNewExpireDate.Location = new System.Drawing.Point(213, 59);
            this.dNewExpireDate.Name = "dNewExpireDate";
            this.dNewExpireDate.Size = new System.Drawing.Size(102, 20);
            this.dNewExpireDate.TabIndex = 6;
            this.dNewExpireDate.LostFocus += new System.EventHandler(this.dNewExpireDate_LostFocus);
            // 
            // lblStatus_Grp2
            // 
            this.lblStatus_Grp2.AutoSize = true;
            this.lblStatus_Grp2.ForeColor = System.Drawing.Color.Red;
            this.lblStatus_Grp2.Location = new System.Drawing.Point(6, 32);
            this.lblStatus_Grp2.Name = "lblStatus_Grp2";
            this.lblStatus_Grp2.Size = new System.Drawing.Size(47, 13);
            this.lblStatus_Grp2.TabIndex = 1103;
            this.lblStatus_Grp2.Text = "Εκκρεμή";
            // 
            // lblNewTitle
            // 
            this.lblNewTitle.AutoSize = true;
            this.lblNewTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblNewTitle.Location = new System.Drawing.Point(4, 11);
            this.lblNewTitle.Name = "lblNewTitle";
            this.lblNewTitle.Size = new System.Drawing.Size(106, 15);
            this.lblNewTitle.TabIndex = 412;
            this.lblNewTitle.Text = "Νέο διαβατήριο";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(132, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 1106;
            this.label6.Text = "Ημ/νία Λήξης";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(132, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 13);
            this.label9.TabIndex = 1104;
            this.label9.Text = "Aριθμός";
            // 
            // txtNewNumber
            // 
            this.txtNewNumber.Location = new System.Drawing.Point(213, 13);
            this.txtNewNumber.Name = "txtNewNumber";
            this.txtNewNumber.Size = new System.Drawing.Size(102, 20);
            this.txtNewNumber.TabIndex = 2;
            this.txtNewNumber.LostFocus += new System.EventHandler(this.txtNewNumber_LostFocus);
            // 
            // ucPasport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panDocs);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.picEdit_Grp3);
            this.Controls.Add(this.grp1);
            this.Controls.Add(this.grp3);
            this.Controls.Add(this.grp2);
            this.Name = "ucPasport";
            this.Size = new System.Drawing.Size(967, 391);
            this.Load += new System.EventHandler(this.ucPasport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picCancel3)).EndInit();
            this.panDocs.ResumeLayout(false);
            this.toolLeft.ResumeLayout(false);
            this.toolLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgDocs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit_Grp3)).EndInit();
            this.grp1.ResumeLayout(false);
            this.grp1.PerformLayout();
            this.grp3.ResumeLayout(false);
            this.grp3.PerformLayout();
            this.grp2.ResumeLayout(false);
            this.grp2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Label lblOldNumber;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.Label label15;
        public System.Windows.Forms.Label lblOldExpireDate;
        internal System.Windows.Forms.PictureBox picCancel3;
        internal System.Windows.Forms.ToolStripButton tsbAdd;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.ToolStripButton tsbView;
        private System.Windows.Forms.Panel panDocs;
        private System.Windows.Forms.Label lblLoadDocs;
        internal System.Windows.Forms.ToolStrip toolLeft;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator29;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgDocs;
        private System.Windows.Forms.Button btnSave3;
        public System.Windows.Forms.Label lblStatus;
        internal System.Windows.Forms.PictureBox picEdit_Grp3;
        private System.Windows.Forms.GroupBox grp1;
        private System.Windows.Forms.GroupBox grp3;
        private System.Windows.Forms.Label lblDocsCount;
        private System.Windows.Forms.Label lblStatus_Grp3;
        public System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox grp2;
        public System.Windows.Forms.DateTimePicker dNewExpireDate;
        private System.Windows.Forms.Label lblStatus_Grp2;
        public System.Windows.Forms.Label lblNewTitle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtNewNumber;
        public System.Windows.Forms.TextBox txtNewPolice;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label lblOldPolice;
        private System.Windows.Forms.Label label13;
    }
}
