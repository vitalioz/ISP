
namespace Options
{
    partial class ucCurrenciesList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucCurrenciesList));
            this.grpData = new System.Windows.Forms.GroupBox();
            this.toolLeft = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEdit = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panDetails = new System.Windows.Forms.Panel();
            this.cmbCurr_Convert = new System.Windows.Forms.ComboBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtKoef = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtCode_MStar = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblDet_Title2 = new System.Windows.Forms.Label();
            this.lblDet_Title1 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblListItemTitle = new System.Windows.Forms.Label();
            this.toolRight = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.grpData.SuspendLayout();
            this.toolLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.panDetails.SuspendLayout();
            this.toolRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpData
            // 
            this.grpData.Controls.Add(this.toolLeft);
            this.grpData.Controls.Add(this.fgList);
            this.grpData.Controls.Add(this.panDetails);
            this.grpData.Controls.Add(this.lblListItemTitle);
            this.grpData.Controls.Add(this.toolRight);
            this.grpData.Location = new System.Drawing.Point(7, 15);
            this.grpData.Name = "grpData";
            this.grpData.Size = new System.Drawing.Size(878, 689);
            this.grpData.TabIndex = 285;
            this.grpData.TabStop = false;
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
            this.tsbHelp});
            this.toolLeft.Location = new System.Drawing.Point(16, 40);
            this.toolLeft.Name = "toolLeft";
            this.toolLeft.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolLeft.Size = new System.Drawing.Size(139, 27);
            this.toolLeft.TabIndex = 555;
            this.toolLeft.Text = "ToolStrip1";
            // 
            // ToolStripLabel2
            // 
            this.ToolStripLabel2.Name = "ToolStripLabel2";
            this.ToolStripLabel2.Size = new System.Drawing.Size(10, 24);
            this.ToolStripLabel2.Text = " ";
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
            // ToolStripSeparator4
            // 
            this.ToolStripSeparator4.Name = "ToolStripSeparator4";
            this.ToolStripSeparator4.Size = new System.Drawing.Size(6, 27);
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
            // ToolStripSeparator5
            // 
            this.ToolStripSeparator5.Name = "ToolStripSeparator5";
            this.ToolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            this.ToolStripSeparator5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            this.toolStripSeparator2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            // 
            // tsbHelp
            // 
            this.tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbHelp.Image")));
            this.tsbHelp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(23, 24);
            this.tsbHelp.Text = "Βοήθεια";
            // 
            // fgList
            // 
            this.fgList.AllowEditing = false;
            this.fgList.ColumnInfo = "2,0,0,0,0,85,Columns:0{Width:340;Name:\"Title\";Caption:\"Τίτλος\";Style:\"TextAlign:L" +
    "eftCenter;\";}\t1{Name:\"ID\";Caption:\"ID\";Visible:False;}\t";
            this.fgList.Location = new System.Drawing.Point(16, 72);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(364, 608);
            this.fgList.TabIndex = 554;
            this.fgList.RowColChange += new System.EventHandler(this.fgList_RowColChange);
            // 
            // panDetails
            // 
            this.panDetails.Controls.Add(this.cmbCurr_Convert);
            this.panDetails.Controls.Add(this.Label3);
            this.panDetails.Controls.Add(this.txtKoef);
            this.panDetails.Controls.Add(this.Label1);
            this.panDetails.Controls.Add(this.Label2);
            this.panDetails.Controls.Add(this.txtCode_MStar);
            this.panDetails.Controls.Add(this.txtCode);
            this.panDetails.Controls.Add(this.lblDet_Title2);
            this.panDetails.Controls.Add(this.lblDet_Title1);
            this.panDetails.Controls.Add(this.txtTitle);
            this.panDetails.Location = new System.Drawing.Point(414, 72);
            this.panDetails.Name = "panDetails";
            this.panDetails.Size = new System.Drawing.Size(449, 336);
            this.panDetails.TabIndex = 377;
            // 
            // cmbCurr_Convert
            // 
            this.cmbCurr_Convert.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurr_Convert.FormattingEnabled = true;
            this.cmbCurr_Convert.Location = new System.Drawing.Point(229, 80);
            this.cmbCurr_Convert.Name = "cmbCurr_Convert";
            this.cmbCurr_Convert.Size = new System.Drawing.Size(91, 21);
            this.cmbCurr_Convert.TabIndex = 558;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(198, 85);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(25, 13);
            this.Label3.TabIndex = 563;
            this.Label3.Text = "=  1";
            // 
            // txtKoef
            // 
            this.txtKoef.Location = new System.Drawing.Point(100, 81);
            this.txtKoef.Name = "txtKoef";
            this.txtKoef.Size = new System.Drawing.Size(92, 20);
            this.txtKoef.TabIndex = 557;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(10, 85);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(84, 13);
            this.Label1.TabIndex = 562;
            this.Label1.Text = "Μετατροπέας *";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(10, 58);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(78, 13);
            this.Label2.TabIndex = 561;
            this.Label2.Text = "MStar Κωδικός";
            // 
            // txtCode_MStar
            // 
            this.txtCode_MStar.Location = new System.Drawing.Point(100, 55);
            this.txtCode_MStar.Name = "txtCode_MStar";
            this.txtCode_MStar.Size = new System.Drawing.Size(326, 20);
            this.txtCode_MStar.TabIndex = 556;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(100, 29);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(92, 20);
            this.txtCode.TabIndex = 555;
            // 
            // lblDet_Title2
            // 
            this.lblDet_Title2.AutoSize = true;
            this.lblDet_Title2.Location = new System.Drawing.Point(10, 7);
            this.lblDet_Title2.Name = "lblDet_Title2";
            this.lblDet_Title2.Size = new System.Drawing.Size(39, 13);
            this.lblDet_Title2.TabIndex = 560;
            this.lblDet_Title2.Text = "Τίτλος";
            // 
            // lblDet_Title1
            // 
            this.lblDet_Title1.AutoSize = true;
            this.lblDet_Title1.Location = new System.Drawing.Point(10, 33);
            this.lblDet_Title1.Name = "lblDet_Title1";
            this.lblDet_Title1.Size = new System.Drawing.Size(47, 13);
            this.lblDet_Title1.TabIndex = 559;
            this.lblDet_Title1.Text = "Κωδικός";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(100, 3);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(92, 20);
            this.txtTitle.TabIndex = 554;
            // 
            // lblListItemTitle
            // 
            this.lblListItemTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblListItemTitle.Location = new System.Drawing.Point(252, 16);
            this.lblListItemTitle.Name = "lblListItemTitle";
            this.lblListItemTitle.Size = new System.Drawing.Size(400, 13);
            this.lblListItemTitle.TabIndex = 286;
            this.lblListItemTitle.Text = "-";
            this.lblListItemTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toolRight
            // 
            this.toolRight.AutoSize = false;
            this.toolRight.BackColor = System.Drawing.Color.Gainsboro;
            this.toolRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolRight.Dock = System.Windows.Forms.DockStyle.None;
            this.toolRight.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolRight.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolRight.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripLabel3,
            this.tsbSave});
            this.toolRight.Location = new System.Drawing.Point(419, 42);
            this.toolRight.Name = "toolRight";
            this.toolRight.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolRight.Size = new System.Drawing.Size(40, 25);
            this.toolRight.TabIndex = 283;
            this.toolRight.Text = "ToolStrip1";
            // 
            // ToolStripLabel3
            // 
            this.ToolStripLabel3.Name = "ToolStripLabel3";
            this.ToolStripLabel3.Size = new System.Drawing.Size(10, 22);
            this.ToolStripLabel3.Text = " ";
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(23, 22);
            this.tsbSave.Text = "Αποθήκευση";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // ucCurrenciesList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpData);
            this.Name = "ucCurrenciesList";
            this.Size = new System.Drawing.Size(896, 712);
            this.Load += new System.EventHandler(this.ucCurrenciesList_Load);
            this.grpData.ResumeLayout(false);
            this.toolLeft.ResumeLayout(false);
            this.toolLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.panDetails.ResumeLayout(false);
            this.panDetails.PerformLayout();
            this.toolRight.ResumeLayout(false);
            this.toolRight.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox grpData;
        private System.Windows.Forms.Panel panDetails;
        internal System.Windows.Forms.Label lblListItemTitle;
        internal System.Windows.Forms.ToolStrip toolRight;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel3;
        internal System.Windows.Forms.ToolStripButton tsbSave;
        internal System.Windows.Forms.ComboBox cmbCurr_Convert;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox txtKoef;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtCode_MStar;
        internal System.Windows.Forms.TextBox txtCode;
        internal System.Windows.Forms.Label lblDet_Title2;
        internal System.Windows.Forms.Label lblDet_Title1;
        internal System.Windows.Forms.TextBox txtTitle;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
        internal System.Windows.Forms.ToolStrip toolLeft;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel2;
        internal System.Windows.Forms.ToolStripButton tsbAdd;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator4;
        internal System.Windows.Forms.ToolStripButton tsbEdit;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator5;
        internal System.Windows.Forms.ToolStripButton tsbDelete;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        internal System.Windows.Forms.ToolStripButton tsbHelp;
    }
}
