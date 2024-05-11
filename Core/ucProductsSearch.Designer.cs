namespace Core
{
    partial class ucProductsSearch
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucProductsSearch));
            this.ShareCode_ID = new System.Windows.Forms.Label();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.panList = new System.Windows.Forms.Panel();
            this.btnChoice = new System.Windows.Forms.Button();
            this.chkSelect = new System.Windows.Forms.CheckBox();
            this.lblFoundRecords = new System.Windows.Forms.Label();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.txtShareTitle = new System.Windows.Forms.TextBox();
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuProductData = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCopyISIN = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            this.panList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.mnuContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // ShareCode_ID
            // 
            this.ShareCode_ID.AutoSize = true;
            this.ShareCode_ID.Location = new System.Drawing.Point(747, 2);
            this.ShareCode_ID.Name = "ShareCode_ID";
            this.ShareCode_ID.Size = new System.Drawing.Size(28, 13);
            this.ShareCode_ID.TabIndex = 410;
            this.ShareCode_ID.Text = "-999";
            this.ShareCode_ID.Visible = false;
            this.ShareCode_ID.TextChanged += new System.EventHandler(this.ShareCode_ID_TextChanged);
            // 
            // picClose
            // 
            this.picClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picClose.Image = global::Core.Properties.Resources.cancel;
            this.picClose.Location = new System.Drawing.Point(754, 6);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(18, 18);
            this.picClose.TabIndex = 405;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            // 
            // panList
            // 
            this.panList.BackColor = System.Drawing.Color.LightSalmon;
            this.panList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panList.Controls.Add(this.btnChoice);
            this.panList.Controls.Add(this.chkSelect);
            this.panList.Controls.Add(this.lblFoundRecords);
            this.panList.Controls.Add(this.fgList);
            this.panList.Controls.Add(this.picClose);
            this.panList.Location = new System.Drawing.Point(0, 22);
            this.panList.Name = "panList";
            this.panList.Size = new System.Drawing.Size(779, 378);
            this.panList.TabIndex = 411;
            // 
            // btnChoice
            // 
            this.btnChoice.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnChoice.FlatAppearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.btnChoice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnChoice.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnChoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChoice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChoice.Location = new System.Drawing.Point(346, 346);
            this.btnChoice.Name = "btnChoice";
            this.btnChoice.Size = new System.Drawing.Size(91, 25);
            this.btnChoice.TabIndex = 707;
            this.btnChoice.Text = "Επιλογή";
            this.btnChoice.UseVisualStyleBackColor = false;
            this.btnChoice.Click += new System.EventHandler(this.btnChoice_Click);
            // 
            // chkSelect
            // 
            this.chkSelect.AutoSize = true;
            this.chkSelect.Location = new System.Drawing.Point(13, 32);
            this.chkSelect.Name = "chkSelect";
            this.chkSelect.Size = new System.Drawing.Size(15, 14);
            this.chkSelect.TabIndex = 706;
            this.chkSelect.UseVisualStyleBackColor = true;
            this.chkSelect.CheckedChanged += new System.EventHandler(this.chkSelect_CheckedChanged);
            // 
            // lblFoundRecords
            // 
            this.lblFoundRecords.AutoSize = true;
            this.lblFoundRecords.BackColor = System.Drawing.Color.Transparent;
            this.lblFoundRecords.Location = new System.Drawing.Point(10, 9);
            this.lblFoundRecords.Name = "lblFoundRecords";
            this.lblFoundRecords.Size = new System.Drawing.Size(0, 13);
            this.lblFoundRecords.TabIndex = 705;
            // 
            // fgList
            // 
            this.fgList.AllowEditing = false;
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(6, 29);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(765, 311);
            this.fgList.TabIndex = 704;
            this.fgList.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.fgList_CellChanged);
            // 
            // txtShareTitle
            // 
            this.txtShareTitle.Location = new System.Drawing.Point(0, 0);
            this.txtShareTitle.Name = "txtShareTitle";
            this.txtShareTitle.Size = new System.Drawing.Size(200, 20);
            this.txtShareTitle.TabIndex = 409;
            this.txtShareTitle.TextChanged += new System.EventHandler(this.txtShareTitle_TextChanged);
            // 
            // mnuContext
            // 
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuProductData,
            this.mnuCopyISIN});
            this.mnuContext.Name = "ContextMenuStrip1";
            this.mnuContext.Size = new System.Drawing.Size(179, 48);
            // 
            // mnuProductData
            // 
            this.mnuProductData.Name = "mnuProductData";
            this.mnuProductData.Size = new System.Drawing.Size(178, 22);
            this.mnuProductData.Text = "Στοιχεία Προϊόντος";
            this.mnuProductData.Click += new System.EventHandler(this.mnuProductData_Click);
            // 
            // mnuCopyISIN
            // 
            this.mnuCopyISIN.Name = "mnuCopyISIN";
            this.mnuCopyISIN.Size = new System.Drawing.Size(178, 22);
            this.mnuCopyISIN.Text = "Αντιγραφή ISIN";
            this.mnuCopyISIN.Click += new System.EventHandler(this.mnuCopyISIN_Click);
            // 
            // ucProductsSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ShareCode_ID);
            this.Controls.Add(this.panList);
            this.Controls.Add(this.txtShareTitle);
            this.Name = "ucProductsSearch";
            this.Size = new System.Drawing.Size(780, 400);
            this.Load += new System.EventHandler(this.ucProductsSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            this.panList.ResumeLayout(false);
            this.panList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.mnuContext.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label ShareCode_ID;
        internal System.Windows.Forms.PictureBox picClose;
        private System.Windows.Forms.Panel panList;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
        public System.Windows.Forms.TextBox txtShareTitle;
        public System.Windows.Forms.Label lblFoundRecords;
        internal System.Windows.Forms.ContextMenuStrip mnuContext;
        internal System.Windows.Forms.ToolStripMenuItem mnuProductData;
        private System.Windows.Forms.ToolStripMenuItem mnuCopyISIN;
        internal System.Windows.Forms.CheckBox chkSelect;
        internal System.Windows.Forms.Button btnChoice;
    }
}
