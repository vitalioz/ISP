
namespace Options
{
    partial class ucStockExchangesList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucStockExchangesList));
            this.grpData = new System.Windows.Forms.GroupBox();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.toolLeft = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsbExtend = new System.Windows.Forms.ToolStripButton();
            this.tsbCollapse = new System.Windows.Forms.ToolStripButton();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAdd2 = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEdit = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.panDetails = new System.Windows.Forms.Panel();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.toolRight = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.txtReutersCode = new System.Windows.Forms.TextBox();
            this.lblDet_Title1 = new System.Windows.Forms.Label();
            this.txtMIC = new System.Windows.Forms.TextBox();
            this.lblDet_Title2 = new System.Windows.Forms.Label();
            this.fgAliases = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.Label3 = new System.Windows.Forms.Label();
            this.picDel_Alias = new System.Windows.Forms.PictureBox();
            this.picAdd_Alias = new System.Windows.Forms.PictureBox();
            this.txtTitle_Bloomberg = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtTitle_MStar = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.lblListItemTitle = new System.Windows.Forms.Label();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.grpData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.toolLeft.SuspendLayout();
            this.panDetails.SuspendLayout();
            this.toolRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgAliases)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDel_Alias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAdd_Alias)).BeginInit();
            this.SuspendLayout();
            // 
            // grpData
            // 
            this.grpData.Controls.Add(this.fgList);
            this.grpData.Controls.Add(this.toolLeft);
            this.grpData.Controls.Add(this.panDetails);
            this.grpData.Controls.Add(this.lblListItemTitle);
            this.grpData.Location = new System.Drawing.Point(4, 4);
            this.grpData.Name = "grpData";
            this.grpData.Size = new System.Drawing.Size(964, 722);
            this.grpData.TabIndex = 285;
            this.grpData.TabStop = false;
            // 
            // fgList
            // 
            this.fgList.AllowEditing = false;
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(16, 71);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(406, 640);
            this.fgList.TabIndex = 551;
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
            this.tsbExtend,
            this.tsbCollapse,
            this.tsbAdd,
            this.ToolStripSeparator4,
            this.tsbAdd2,
            this.ToolStripSeparator1,
            this.tsbEdit,
            this.ToolStripSeparator5,
            this.tsbDelete,
            this.ToolStripSeparator2,
            this.tsbHelp});
            this.toolLeft.Location = new System.Drawing.Point(16, 41);
            this.toolLeft.Name = "toolLeft";
            this.toolLeft.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolLeft.Size = new System.Drawing.Size(213, 27);
            this.toolLeft.TabIndex = 550;
            this.toolLeft.Text = "ToolStrip1";
            // 
            // ToolStripLabel2
            // 
            this.ToolStripLabel2.Name = "ToolStripLabel2";
            this.ToolStripLabel2.Size = new System.Drawing.Size(10, 24);
            this.ToolStripLabel2.Text = " ";
            // 
            // tsbExtend
            // 
            this.tsbExtend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExtend.Image = global::Options.Properties.Resources.tree_plus;
            this.tsbExtend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExtend.Name = "tsbExtend";
            this.tsbExtend.Size = new System.Drawing.Size(23, 24);
            this.tsbExtend.Text = "Extend";
            this.tsbExtend.Click += new System.EventHandler(this.tsbExtend_Click);
            // 
            // tsbCollapse
            // 
            this.tsbCollapse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCollapse.Image = global::Options.Properties.Resources.tree_minus;
            this.tsbCollapse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCollapse.Name = "tsbCollapse";
            this.tsbCollapse.Size = new System.Drawing.Size(23, 24);
            this.tsbCollapse.Text = "Collapse";
            this.tsbCollapse.Click += new System.EventHandler(this.tsbCollapse_Click);
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
            // tsbAdd2
            // 
            this.tsbAdd2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAdd2.Image = ((System.Drawing.Image)(resources.GetObject("tsbAdd2.Image")));
            this.tsbAdd2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd2.Name = "tsbAdd2";
            this.tsbAdd2.Size = new System.Drawing.Size(23, 24);
            this.tsbAdd2.Text = "Προσθήκη Χρηματιστηρίου I επιπέδου";
            this.tsbAdd2.Click += new System.EventHandler(this.tsbAdd2_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 27);
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
            // ToolStripSeparator5
            // 
            this.ToolStripSeparator5.Name = "ToolStripSeparator5";
            this.ToolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            this.ToolStripSeparator5.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
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
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            this.ToolStripSeparator2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            // 
            // panDetails
            // 
            this.panDetails.Controls.Add(this.cmbCountry);
            this.panDetails.Controls.Add(this.Label8);
            this.panDetails.Controls.Add(this.txtTitle);
            this.panDetails.Controls.Add(this.Label4);
            this.panDetails.Controls.Add(this.toolRight);
            this.panDetails.Controls.Add(this.txtReutersCode);
            this.panDetails.Controls.Add(this.lblDet_Title1);
            this.panDetails.Controls.Add(this.txtMIC);
            this.panDetails.Controls.Add(this.lblDet_Title2);
            this.panDetails.Controls.Add(this.fgAliases);
            this.panDetails.Controls.Add(this.Label3);
            this.panDetails.Controls.Add(this.picDel_Alias);
            this.panDetails.Controls.Add(this.picAdd_Alias);
            this.panDetails.Controls.Add(this.txtTitle_Bloomberg);
            this.panDetails.Controls.Add(this.Label2);
            this.panDetails.Controls.Add(this.txtTitle_MStar);
            this.panDetails.Controls.Add(this.Label1);
            this.panDetails.Location = new System.Drawing.Point(431, 40);
            this.panDetails.Name = "panDetails";
            this.panDetails.Size = new System.Drawing.Size(522, 672);
            this.panDetails.TabIndex = 549;
            // 
            // cmbCountry
            // 
            this.cmbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.Location = new System.Drawing.Point(98, 179);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(256, 21);
            this.cmbCountry.TabIndex = 12;
            // 
            // Label8
            // 
            this.Label8.Location = new System.Drawing.Point(8, 181);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(60, 19);
            this.Label8.TabIndex = 550;
            this.Label8.Text = "Χώρα";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(98, 75);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(401, 20);
            this.txtTitle.TabIndex = 4;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(7, 79);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(30, 13);
            this.Label4.TabIndex = 548;
            this.Label4.Text = "Ttitle";
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
            // txtReutersCode
            // 
            this.txtReutersCode.Location = new System.Drawing.Point(98, 99);
            this.txtReutersCode.Name = "txtReutersCode";
            this.txtReutersCode.Size = new System.Drawing.Size(130, 20);
            this.txtReutersCode.TabIndex = 6;
            // 
            // lblDet_Title1
            // 
            this.lblDet_Title1.AutoSize = true;
            this.lblDet_Title1.Location = new System.Drawing.Point(6, 103);
            this.lblDet_Title1.Name = "lblDet_Title1";
            this.lblDet_Title1.Size = new System.Drawing.Size(72, 13);
            this.lblDet_Title1.TabIndex = 289;
            this.lblDet_Title1.Text = "Reuters Code";
            // 
            // txtMIC
            // 
            this.txtMIC.Location = new System.Drawing.Point(98, 49);
            this.txtMIC.Name = "txtMIC";
            this.txtMIC.Size = new System.Drawing.Size(130, 20);
            this.txtMIC.TabIndex = 2;
            // 
            // lblDet_Title2
            // 
            this.lblDet_Title2.AutoSize = true;
            this.lblDet_Title2.Location = new System.Drawing.Point(6, 52);
            this.lblDet_Title2.Name = "lblDet_Title2";
            this.lblDet_Title2.Size = new System.Drawing.Size(26, 13);
            this.lblDet_Title2.TabIndex = 291;
            this.lblDet_Title2.Text = "MIC";
            // 
            // fgAliases
            // 
            this.fgAliases.ColumnInfo = resources.GetString("fgAliases.ColumnInfo");
            this.fgAliases.Location = new System.Drawing.Point(107, 427);
            this.fgAliases.Name = "fgAliases";
            this.fgAliases.Rows.Count = 1;
            this.fgAliases.Rows.DefaultSize = 17;
            this.fgAliases.Size = new System.Drawing.Size(401, 236);
            this.fgAliases.TabIndex = 12;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(12, 433);
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
            // txtTitle_Bloomberg
            // 
            this.txtTitle_Bloomberg.Location = new System.Drawing.Point(98, 125);
            this.txtTitle_Bloomberg.Name = "txtTitle_Bloomberg";
            this.txtTitle_Bloomberg.Size = new System.Drawing.Size(130, 20);
            this.txtTitle_Bloomberg.TabIndex = 8;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(8, 128);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(85, 13);
            this.Label2.TabIndex = 538;
            this.Label2.Text = "Bloomberg Code";
            // 
            // txtTitle_MStar
            // 
            this.txtTitle_MStar.Location = new System.Drawing.Point(98, 151);
            this.txtTitle_MStar.Name = "txtTitle_MStar";
            this.txtTitle_MStar.Size = new System.Drawing.Size(401, 20);
            this.txtTitle_MStar.TabIndex = 10;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(7, 155);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(61, 13);
            this.Label1.TabIndex = 537;
            this.Label1.Text = "MStar Ttitle";
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
            // ucStockExchangesList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpData);
            this.Name = "ucStockExchangesList";
            this.Size = new System.Drawing.Size(978, 732);
            this.Load += new System.EventHandler(this.ucStockExchangesList_Load);
            this.grpData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.toolLeft.ResumeLayout(false);
            this.toolLeft.PerformLayout();
            this.panDetails.ResumeLayout(false);
            this.panDetails.PerformLayout();
            this.toolRight.ResumeLayout(false);
            this.toolRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgAliases)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDel_Alias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAdd_Alias)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox grpData;
        internal System.Windows.Forms.Label lblListItemTitle;
        internal System.Windows.Forms.Panel panDetails;
        internal System.Windows.Forms.ComboBox cmbCountry;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.TextBox txtTitle;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.ToolStrip toolRight;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel3;
        internal System.Windows.Forms.ToolStripButton tsbSave;
        internal System.Windows.Forms.TextBox txtReutersCode;
        internal System.Windows.Forms.Label lblDet_Title1;
        internal System.Windows.Forms.TextBox txtMIC;
        internal System.Windows.Forms.Label lblDet_Title2;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgAliases;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.PictureBox picDel_Alias;
        internal System.Windows.Forms.PictureBox picAdd_Alias;
        internal System.Windows.Forms.TextBox txtTitle_Bloomberg;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtTitle_MStar;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.ToolStrip toolLeft;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel2;
        internal System.Windows.Forms.ToolStripButton tsbExtend;
        internal System.Windows.Forms.ToolStripButton tsbCollapse;
        internal System.Windows.Forms.ToolStripButton tsbAdd;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator4;
        internal System.Windows.Forms.ToolStripButton tsbAdd2;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripButton tsbEdit;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator5;
        internal System.Windows.Forms.ToolStripButton tsbDelete;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
        internal System.Windows.Forms.ToolStripButton tsbHelp;
    }
}
