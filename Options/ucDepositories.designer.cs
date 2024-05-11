namespace Options
{
    partial class ucDepositories
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDepositories));
            this.lblListItemTitle = new System.Windows.Forms.Label();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.ToolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.panDetails = new System.Windows.Forms.Panel();
            this.Label8 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.toolRight = new System.Windows.Forms.ToolStrip();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblDet_Title1 = new System.Windows.Forms.Label();
            this.txtBIC = new System.Windows.Forms.TextBox();
            this.lblDet_Title2 = new System.Windows.Forms.Label();
            this.fgAliases = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.Label3 = new System.Windows.Forms.Label();
            this.picDel_Alias = new System.Windows.Forms.PictureBox();
            this.picAdd_Alias = new System.Windows.Forms.PictureBox();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEdit = new System.Windows.Forms.ToolStripButton();
            this.grpData = new System.Windows.Forms.GroupBox();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.toolLeft = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.panDetails.SuspendLayout();
            this.toolRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgAliases)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDel_Alias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAdd_Alias)).BeginInit();
            this.grpData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.toolLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblListItemTitle
            // 
            this.lblListItemTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblListItemTitle.Location = new System.Drawing.Point(245, 16);
            this.lblListItemTitle.Name = "lblListItemTitle";
            this.lblListItemTitle.Size = new System.Drawing.Size(400, 13);
            this.lblListItemTitle.TabIndex = 286;
            this.lblListItemTitle.Text = "-";
            this.lblListItemTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // ToolStripLabel3
            // 
            this.ToolStripLabel3.Name = "ToolStripLabel3";
            this.ToolStripLabel3.Size = new System.Drawing.Size(10, 22);
            this.ToolStripLabel3.Text = " ";
            // 
            // cmbCountry
            // 
            this.cmbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.Location = new System.Drawing.Point(98, 110);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(256, 21);
            this.cmbCountry.TabIndex = 8;
            // 
            // panDetails
            // 
            this.panDetails.Controls.Add(this.cmbCountry);
            this.panDetails.Controls.Add(this.Label8);
            this.panDetails.Controls.Add(this.txtTitle);
            this.panDetails.Controls.Add(this.Label4);
            this.panDetails.Controls.Add(this.toolRight);
            this.panDetails.Controls.Add(this.txtCode);
            this.panDetails.Controls.Add(this.lblDet_Title1);
            this.panDetails.Controls.Add(this.txtBIC);
            this.panDetails.Controls.Add(this.lblDet_Title2);
            this.panDetails.Controls.Add(this.fgAliases);
            this.panDetails.Controls.Add(this.Label3);
            this.panDetails.Controls.Add(this.picDel_Alias);
            this.panDetails.Controls.Add(this.picAdd_Alias);
            this.panDetails.Location = new System.Drawing.Point(431, 40);
            this.panDetails.Name = "panDetails";
            this.panDetails.Size = new System.Drawing.Size(522, 672);
            this.panDetails.TabIndex = 549;
            // 
            // Label8
            // 
            this.Label8.Location = new System.Drawing.Point(32, 112);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(60, 19);
            this.Label8.TabIndex = 550;
            this.Label8.Text = "Χώρα";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(98, 34);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(401, 20);
            this.txtTitle.TabIndex = 2;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(31, 38);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(39, 13);
            this.Label4.TabIndex = 548;
            this.Label4.Text = "Τίτλος";
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
            this.toolRight.Location = new System.Drawing.Point(0, 0);
            this.toolRight.Name = "toolRight";
            this.toolRight.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolRight.Size = new System.Drawing.Size(40, 25);
            this.toolRight.TabIndex = 546;
            this.toolRight.Text = "ToolStrip1";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(98, 58);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(130, 20);
            this.txtCode.TabIndex = 4;
            // 
            // lblDet_Title1
            // 
            this.lblDet_Title1.AutoSize = true;
            this.lblDet_Title1.Location = new System.Drawing.Point(30, 62);
            this.lblDet_Title1.Name = "lblDet_Title1";
            this.lblDet_Title1.Size = new System.Drawing.Size(47, 13);
            this.lblDet_Title1.TabIndex = 289;
            this.lblDet_Title1.Text = "Κωδικός";
            // 
            // txtBIC
            // 
            this.txtBIC.Location = new System.Drawing.Point(98, 84);
            this.txtBIC.Name = "txtBIC";
            this.txtBIC.Size = new System.Drawing.Size(130, 20);
            this.txtBIC.TabIndex = 6;
            // 
            // lblDet_Title2
            // 
            this.lblDet_Title2.AutoSize = true;
            this.lblDet_Title2.Location = new System.Drawing.Point(30, 87);
            this.lblDet_Title2.Name = "lblDet_Title2";
            this.lblDet_Title2.Size = new System.Drawing.Size(24, 13);
            this.lblDet_Title2.TabIndex = 291;
            this.lblDet_Title2.Text = "BIC";
            // 
            // fgAliases
            // 
            this.fgAliases.ColumnInfo = resources.GetString("fgAliases.ColumnInfo");
            this.fgAliases.Location = new System.Drawing.Point(107, 427);
            this.fgAliases.Name = "fgAliases";
            this.fgAliases.Rows.Count = 1;
            this.fgAliases.Rows.DefaultSize = 17;
            this.fgAliases.Size = new System.Drawing.Size(399, 236);
            this.fgAliases.TabIndex = 10;
            this.fgAliases.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.fgAliases_CellChanged);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(27, 433);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(47, 13);
            this.Label3.TabIndex = 533;
            this.Label3.Text = " Κωδικόι";
            // 
            // picDel_Alias
            // 
            this.picDel_Alias.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDel_Alias.Image = global::Options.Properties.Resources.minus;
            this.picDel_Alias.Location = new System.Drawing.Point(79, 468);
            this.picDel_Alias.Name = "picDel_Alias";
            this.picDel_Alias.Size = new System.Drawing.Size(19, 18);
            this.picDel_Alias.TabIndex = 535;
            this.picDel_Alias.TabStop = false;
            this.picDel_Alias.Click += new System.EventHandler(this.picDel_Alias_Click);
            // 
            // picAdd_Alias
            // 
            this.picAdd_Alias.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picAdd_Alias.Image = global::Options.Properties.Resources.plus;
            this.picAdd_Alias.Location = new System.Drawing.Point(79, 446);
            this.picAdd_Alias.Name = "picAdd_Alias";
            this.picAdd_Alias.Size = new System.Drawing.Size(19, 20);
            this.picAdd_Alias.TabIndex = 534;
            this.picAdd_Alias.TabStop = false;
            this.picAdd_Alias.Click += new System.EventHandler(this.picAdd_Alias_Click);
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
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            this.ToolStripSeparator2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            // 
            // tsbDelete
            // 
            this.tsbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDelete.Image = global::Options.Properties.Resources.minus;
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(23, 24);
            this.tsbDelete.Text = "Διαγραφή Επιλεγμένου Κλάδου";
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
            this.tsbEdit.Image = global::Options.Properties.Resources.edit;
            this.tsbEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEdit.Name = "tsbEdit";
            this.tsbEdit.Size = new System.Drawing.Size(23, 24);
            this.tsbEdit.Text = "Διόρθωση Επιλεγμένου Κλάδου";
            this.tsbEdit.Click += new System.EventHandler(this.tsbEdit_Click);
            // 
            // grpData
            // 
            this.grpData.Controls.Add(this.fgList);
            this.grpData.Controls.Add(this.toolLeft);
            this.grpData.Controls.Add(this.panDetails);
            this.grpData.Controls.Add(this.lblListItemTitle);
            this.grpData.Location = new System.Drawing.Point(7, 5);
            this.grpData.Name = "grpData";
            this.grpData.Size = new System.Drawing.Size(964, 722);
            this.grpData.TabIndex = 286;
            this.grpData.TabStop = false;
            // 
            // fgList
            // 
            this.fgList.AllowEditing = false;
            this.fgList.ColumnInfo = "3,0,0,0,0,85,Columns:0{Width:250;Name:\"Title\";Caption:\"Τίτλος\";}\t1{Width:130;Name" +
    ":\"Code\";Caption:\"Κωδικός\";StyleFixed:\"ImageAlign:LeftCenter;\";}\t2{Width:38;Name:" +
    "\"ID\";Caption:\"ID\";Visible:False;}\t";
            this.fgList.Location = new System.Drawing.Point(16, 78);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(409, 674);
            this.fgList.TabIndex = 551;
            this.fgList.RowColChange += new System.EventHandler(this.fgList_RowColChange);
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
            this.ToolStripSeparator4,
            this.tsbEdit,
            this.ToolStripSeparator5,
            this.tsbDelete,
            this.ToolStripSeparator2,
            this.tsbHelp});
            this.toolLeft.Location = new System.Drawing.Point(16, 41);
            this.toolLeft.Name = "toolLeft";
            this.toolLeft.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolLeft.Size = new System.Drawing.Size(130, 27);
            this.toolLeft.TabIndex = 550;
            this.toolLeft.Text = "ToolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(13, 24);
            this.toolStripLabel1.Text = "  ";
            // 
            // tsbAdd
            // 
            this.tsbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbAdd.Image")));
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(23, 24);
            this.tsbAdd.Text = "Προσθήκη Χρηματιστηρίου I επιπέδου";
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // ToolStripSeparator4
            // 
            this.ToolStripSeparator4.Name = "ToolStripSeparator4";
            this.ToolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // ucDepositories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpData);
            this.Name = "ucDepositories";
            this.Size = new System.Drawing.Size(978, 732);
            this.panDetails.ResumeLayout(false);
            this.panDetails.PerformLayout();
            this.toolRight.ResumeLayout(false);
            this.toolRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgAliases)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDel_Alias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAdd_Alias)).EndInit();
            this.grpData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.toolLeft.ResumeLayout(false);
            this.toolLeft.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label lblListItemTitle;
        internal System.Windows.Forms.ToolStripButton tsbSave;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel3;
        internal System.Windows.Forms.ComboBox cmbCountry;
        internal System.Windows.Forms.Panel panDetails;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.TextBox txtTitle;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.ToolStrip toolRight;
        internal System.Windows.Forms.TextBox txtCode;
        internal System.Windows.Forms.Label lblDet_Title1;
        internal System.Windows.Forms.TextBox txtBIC;
        internal System.Windows.Forms.Label lblDet_Title2;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgAliases;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.PictureBox picDel_Alias;
        internal System.Windows.Forms.PictureBox picAdd_Alias;
        internal System.Windows.Forms.ToolStripButton tsbHelp;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        internal System.Windows.Forms.ToolStripButton tsbDelete;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator5;
        internal System.Windows.Forms.ToolStripButton tsbEdit;
        internal System.Windows.Forms.GroupBox grpData;
        internal System.Windows.Forms.ToolStrip toolLeft;
        internal System.Windows.Forms.ToolStripButton tsbAdd;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
    }
}
