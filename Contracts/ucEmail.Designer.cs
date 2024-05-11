
namespace Contracts
{
    partial class ucEmail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucEmail));
            this.txtNewValue = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.picEdit_Grp2 = new System.Windows.Forms.PictureBox();
            this.grp2 = new System.Windows.Forms.GroupBox();
            this.lnkEmail = new System.Windows.Forms.LinkLabel();
            this.lblStatus_Grp2 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.panDocs = new System.Windows.Forms.Panel();
            this.picCancel1 = new System.Windows.Forms.PictureBox();
            this.lblLoadDocs = new System.Windows.Forms.Label();
            this.toolLeft = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator29 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbView = new System.Windows.Forms.ToolStripButton();
            this.fgDocs = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnSave1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit_Grp2)).BeginInit();
            this.grp2.SuspendLayout();
            this.panDocs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCancel1)).BeginInit();
            this.toolLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgDocs)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNewValue
            // 
            this.txtNewValue.Location = new System.Drawing.Point(140, 44);
            this.txtNewValue.Name = "txtNewValue";
            this.txtNewValue.Size = new System.Drawing.Size(222, 20);
            this.txtNewValue.TabIndex = 2;
            this.txtNewValue.LostFocus += new System.EventHandler(this.txtNewValue_LostFocus);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(170, 17);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(170, 13);
            this.label34.TabIndex = 417;
            this.label34.Text = "Καταχωρήστε το νέο σας e-mail ";
            // 
            // lblStatus
            // 
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatus.Location = new System.Drawing.Point(484, 165);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(27, 20);
            this.lblStatus.TabIndex = 1121;
            this.lblStatus.Text = "0";
            this.lblStatus.Visible = false;
            // 
            // picEdit_Grp2
            // 
            this.picEdit_Grp2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picEdit_Grp2.Image = global::Contracts.Properties.Resources.ArRight;
            this.picEdit_Grp2.Location = new System.Drawing.Point(482, 135);
            this.picEdit_Grp2.Name = "picEdit_Grp2";
            this.picEdit_Grp2.Size = new System.Drawing.Size(18, 18);
            this.picEdit_Grp2.TabIndex = 1123;
            this.picEdit_Grp2.TabStop = false;
            this.picEdit_Grp2.Click += new System.EventHandler(this.picEdit_Grp2_Click);
            // 
            // grp2
            // 
            this.grp2.Controls.Add(this.lnkEmail);
            this.grp2.Controls.Add(this.lblStatus_Grp2);
            this.grp2.Controls.Add(this.label16);
            this.grp2.Location = new System.Drawing.Point(10, 129);
            this.grp2.Name = "grp2";
            this.grp2.Size = new System.Drawing.Size(468, 56);
            this.grp2.TabIndex = 1122;
            this.grp2.TabStop = false;
            // 
            // lnkEmail
            // 
            this.lnkEmail.AutoSize = true;
            this.lnkEmail.Location = new System.Drawing.Point(160, 16);
            this.lnkEmail.Name = "lnkEmail";
            this.lnkEmail.Size = new System.Drawing.Size(0, 13);
            this.lnkEmail.TabIndex = 1108;
            this.lnkEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEmail_LinkClicked);
            // 
            // lblStatus_Grp2
            // 
            this.lblStatus_Grp2.AutoSize = true;
            this.lblStatus_Grp2.ForeColor = System.Drawing.Color.Red;
            this.lblStatus_Grp2.Location = new System.Drawing.Point(8, 34);
            this.lblStatus_Grp2.Name = "lblStatus_Grp2";
            this.lblStatus_Grp2.Size = new System.Drawing.Size(47, 13);
            this.lblStatus_Grp2.TabIndex = 1104;
            this.lblStatus_Grp2.Text = "Εκκρεμή";
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
            // panDocs
            // 
            this.panDocs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panDocs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panDocs.Controls.Add(this.picCancel1);
            this.panDocs.Controls.Add(this.lblLoadDocs);
            this.panDocs.Controls.Add(this.toolLeft);
            this.panDocs.Controls.Add(this.fgDocs);
            this.panDocs.Controls.Add(this.btnSave1);
            this.panDocs.Location = new System.Drawing.Point(523, 7);
            this.panDocs.Name = "panDocs";
            this.panDocs.Size = new System.Drawing.Size(445, 249);
            this.panDocs.TabIndex = 1126;
            this.panDocs.Visible = false;
            // 
            // picCancel1
            // 
            this.picCancel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picCancel1.Image = global::Contracts.Properties.Resources.cancel1;
            this.picCancel1.Location = new System.Drawing.Point(420, 7);
            this.picCancel1.Name = "picCancel1";
            this.picCancel1.Size = new System.Drawing.Size(18, 18);
            this.picCancel1.TabIndex = 1095;
            this.picCancel1.TabStop = false;
            this.picCancel1.Click += new System.EventHandler(this.picCancel1_Click);
            // 
            // lblLoadDocs
            // 
            this.lblLoadDocs.Location = new System.Drawing.Point(62, 15);
            this.lblLoadDocs.Name = "lblLoadDocs";
            this.lblLoadDocs.Size = new System.Drawing.Size(307, 19);
            this.lblLoadDocs.TabIndex = 483;
            this.lblLoadDocs.Text = "Ανεβάστε ένα αρχείο με εντολή του πελάτη";
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
            this.fgDocs.TabIndex = 4;
            // 
            // btnSave1
            // 
            this.btnSave1.Location = new System.Drawing.Point(171, 210);
            this.btnSave1.Name = "btnSave1";
            this.btnSave1.Size = new System.Drawing.Size(91, 25);
            this.btnSave1.TabIndex = 6;
            this.btnSave1.Text = "Καταχώρηση";
            this.btnSave1.UseVisualStyleBackColor = true;
            this.btnSave1.Click += new System.EventHandler(this.btnSave1_Click);
            // 
            // ucEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panDocs);
            this.Controls.Add(this.picEdit_Grp2);
            this.Controls.Add(this.grp2);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtNewValue);
            this.Controls.Add(this.label34);
            this.Name = "ucEmail";
            this.Size = new System.Drawing.Size(977, 395);
            this.Load += new System.EventHandler(this.ucEmail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picEdit_Grp2)).EndInit();
            this.grp2.ResumeLayout(false);
            this.grp2.PerformLayout();
            this.panDocs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCancel1)).EndInit();
            this.toolLeft.ResumeLayout(false);
            this.toolLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgDocs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label34;
        public System.Windows.Forms.TextBox txtNewValue;
        public System.Windows.Forms.Label lblStatus;
        internal System.Windows.Forms.PictureBox picEdit_Grp2;
        private System.Windows.Forms.GroupBox grp2;
        private System.Windows.Forms.LinkLabel lnkEmail;
        private System.Windows.Forms.Label lblStatus_Grp2;
        public System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panDocs;
        internal System.Windows.Forms.PictureBox picCancel1;
        private System.Windows.Forms.Label lblLoadDocs;
        internal System.Windows.Forms.ToolStrip toolLeft;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        internal System.Windows.Forms.ToolStripButton tsbAdd;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator29;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbView;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgDocs;
        private System.Windows.Forms.Button btnSave1;
    }
}
