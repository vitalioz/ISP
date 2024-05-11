namespace Reports
{
    partial class frmTRS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTRS));
            this.dExecution = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.toolLeft_Search = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsbExcel_Search = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCSV = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.toolLeft_Search.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.SuspendLayout();
            // 
            // dExecution
            // 
            this.dExecution.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dExecution.Location = new System.Drawing.Point(139, 14);
            this.dExecution.Name = "dExecution";
            this.dExecution.Size = new System.Drawing.Size(99, 20);
            this.dExecution.TabIndex = 1037;
            this.dExecution.ValueChanged += new System.EventHandler(this.dExecution_ValueChanged);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(11, 17);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(122, 13);
            this.lblDate.TabIndex = 1036;
            this.lblDate.Text = "Ημερομηνία Εκτέλεσης";
            // 
            // toolLeft_Search
            // 
            this.toolLeft_Search.AutoSize = false;
            this.toolLeft_Search.BackColor = System.Drawing.Color.Gainsboro;
            this.toolLeft_Search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolLeft_Search.Dock = System.Windows.Forms.DockStyle.None;
            this.toolLeft_Search.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolLeft_Search.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolLeft_Search.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripLabel1,
            this.tsbExcel_Search,
            this.ToolStripSeparator8,
            this.tsbCSV,
            this.ToolStripSeparator9,
            this.ToolStripButton2});
            this.toolLeft_Search.Location = new System.Drawing.Point(11, 41);
            this.toolLeft_Search.Name = "toolLeft_Search";
            this.toolLeft_Search.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolLeft_Search.Size = new System.Drawing.Size(106, 26);
            this.toolLeft_Search.TabIndex = 1035;
            this.toolLeft_Search.Text = "ToolStrip1";
            // 
            // ToolStripLabel1
            // 
            this.ToolStripLabel1.Name = "ToolStripLabel1";
            this.ToolStripLabel1.Size = new System.Drawing.Size(10, 23);
            this.ToolStripLabel1.Text = " ";
            // 
            // tsbExcel_Search
            // 
            this.tsbExcel_Search.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExcel_Search.Image = global::Reports.Properties.Resources.excel;
            this.tsbExcel_Search.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExcel_Search.Name = "tsbExcel_Search";
            this.tsbExcel_Search.Size = new System.Drawing.Size(23, 23);
            this.tsbExcel_Search.Text = "Εκτύπωση λίστας";
            this.tsbExcel_Search.Click += new System.EventHandler(this.tsbExcel_Search_Click);
            // 
            // ToolStripSeparator8
            // 
            this.ToolStripSeparator8.Name = "ToolStripSeparator8";
            this.ToolStripSeparator8.Size = new System.Drawing.Size(6, 26);
            // 
            // tsbCSV
            // 
            this.tsbCSV.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCSV.Image = ((System.Drawing.Image)(resources.GetObject("tsbCSV.Image")));
            this.tsbCSV.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCSV.Name = "tsbCSV";
            this.tsbCSV.Size = new System.Drawing.Size(23, 23);
            this.tsbCSV.Text = "Export to CSV";
            this.tsbCSV.Click += new System.EventHandler(this.tsbCSV_Click);
            // 
            // ToolStripSeparator9
            // 
            this.ToolStripSeparator9.Name = "ToolStripSeparator9";
            this.ToolStripSeparator9.Size = new System.Drawing.Size(6, 26);
            // 
            // ToolStripButton2
            // 
            this.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButton2.Image")));
            this.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButton2.Name = "ToolStripButton2";
            this.ToolStripButton2.Size = new System.Drawing.Size(23, 20);
            this.ToolStripButton2.Text = "Βοήθεια";
            // 
            // fgList
            // 
            this.fgList.AllowEditing = false;
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(8, 74);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 3;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Rows.Fixed = 3;
            this.fgList.Size = new System.Drawing.Size(1261, 559);
            this.fgList.TabIndex = 1038;
            // 
            // frmTRS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1296, 644);
            this.Controls.Add(this.fgList);
            this.Controls.Add(this.dExecution);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.toolLeft_Search);
            this.Name = "frmTRS";
            this.Text = "TRS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmTRS_Load);
            this.toolLeft_Search.ResumeLayout(false);
            this.toolLeft_Search.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.DateTimePicker dExecution;
        internal System.Windows.Forms.Label lblDate;
        internal System.Windows.Forms.ToolStrip toolLeft_Search;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel1;
        internal System.Windows.Forms.ToolStripButton tsbExcel_Search;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator8;
        internal System.Windows.Forms.ToolStripButton tsbCSV;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator9;
        internal System.Windows.Forms.ToolStripButton ToolStripButton2;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
    }
}

