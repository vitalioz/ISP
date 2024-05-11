
namespace Options
{
    partial class frmComissionsCategories
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmComissionsCategories));
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.fgCategories = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.toolLeft = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.lblTitles = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fgCategories)).BeginInit();
            this.toolLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // fgList
            // 
            this.fgList.AllowEditing = false;
            this.fgList.ColumnInfo = "2,0,0,0,0,85,Columns:0{Width:350;Name:\"Title\";Caption:\"Τίτλος\";}\t1{Width:38;Name:" +
    "\"ID\";Caption:\"ID\";Visible:False;}\t";
            this.fgList.Location = new System.Drawing.Point(12, 12);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(377, 837);
            this.fgList.TabIndex = 285;
            // 
            // fgCategories
            // 
            this.fgCategories.AllowEditing = false;
            this.fgCategories.ColumnInfo = "2,0,0,0,0,85,Columns:0{Width:450;Name:\"Title\";Caption:\"Τίτλος\";Style:\"TextAlign:L" +
    "eftCenter;\";}\t1{Width:38;Name:\"ID\";Caption:\"ID\";Visible:False;}\t";
            this.fgCategories.Location = new System.Drawing.Point(425, 78);
            this.fgCategories.Name = "fgCategories";
            this.fgCategories.Rows.Count = 1;
            this.fgCategories.Rows.DefaultSize = 17;
            this.fgCategories.Size = new System.Drawing.Size(474, 771);
            this.fgCategories.TabIndex = 286;
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
            this.tsbDelete,
            this.toolStripSeparator2,
            this.tsbSave});
            this.toolLeft.Location = new System.Drawing.Point(425, 47);
            this.toolLeft.Name = "toolLeft";
            this.toolLeft.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolLeft.Size = new System.Drawing.Size(106, 27);
            this.toolLeft.TabIndex = 287;
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
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = global::Options.Properties.Resources.save;
            this.tsbSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(24, 24);
            this.tsbSave.Text = "Αποθήκευση";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // lblTitles
            // 
            this.lblTitles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblTitles.Location = new System.Drawing.Point(503, 14);
            this.lblTitles.Name = "lblTitles";
            this.lblTitles.Size = new System.Drawing.Size(328, 16);
            this.lblTitles.TabIndex = 288;
            this.lblTitles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmComissionsCategories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Thistle;
            this.ClientSize = new System.Drawing.Size(912, 861);
            this.Controls.Add(this.lblTitles);
            this.Controls.Add(this.fgCategories);
            this.Controls.Add(this.toolLeft);
            this.Controls.Add(this.fgList);
            this.Name = "frmComissionsCategories";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Κατηγορίες Προμηθειών";
            this.Load += new System.EventHandler(this.frmComissionsCategories_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fgCategories)).EndInit();
            this.toolLeft.ResumeLayout(false);
            this.toolLeft.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgCategories;
        internal System.Windows.Forms.ToolStrip toolLeft;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel2;
        internal System.Windows.Forms.ToolStripButton tsbAdd;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator4;
        internal System.Windows.Forms.ToolStripButton tsbDelete;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        internal System.Windows.Forms.ToolStripButton tsbSave;
        internal System.Windows.Forms.Label lblTitles;
    }
}