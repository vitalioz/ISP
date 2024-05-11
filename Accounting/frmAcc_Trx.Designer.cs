namespace Accounting
{
    partial class frmAcc_Trx
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAcc_Trx));
            this.grdList = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panCritiries = new System.Windows.Forms.Panel();
            this.dDateControl = new System.Windows.Forms.DateTimePicker();
            this.Label24 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panTools = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lstAdvisor = new System.Windows.Forms.ComboBox();
            this.toolLeft = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel15 = new System.Windows.Forms.ToolStripLabel();
            this.tsbImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbView = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.Label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panCritiries.SuspendLayout();
            this.panTools.SuspendLayout();
            this.toolLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdList
            // 
            this.grdList.Location = new System.Drawing.Point(8, 102);
            this.grdList.MainView = this.gridView1;
            this.grdList.Name = "grdList";
            this.grdList.Size = new System.Drawing.Size(1354, 752);
            this.grdList.TabIndex = 0;
            this.grdList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdList;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            // 
            // panCritiries
            // 
            this.panCritiries.BackColor = System.Drawing.Color.NavajoWhite;
            this.panCritiries.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panCritiries.Controls.Add(this.dDateControl);
            this.panCritiries.Controls.Add(this.Label24);
            this.panCritiries.Controls.Add(this.btnSearch);
            this.panCritiries.Location = new System.Drawing.Point(7, 6);
            this.panCritiries.Name = "panCritiries";
            this.panCritiries.Size = new System.Drawing.Size(1355, 49);
            this.panCritiries.TabIndex = 2102;
            // 
            // dDateControl
            // 
            this.dDateControl.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dDateControl.Location = new System.Drawing.Point(89, 9);
            this.dDateControl.Name = "dDateControl";
            this.dDateControl.Size = new System.Drawing.Size(93, 20);
            this.dDateControl.TabIndex = 2;
            // 
            // Label24
            // 
            this.Label24.AutoSize = true;
            this.Label24.Location = new System.Drawing.Point(17, 12);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(66, 13);
            this.Label24.TabIndex = 1025;
            this.Label24.Text = "Ημερομηνία";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.Control;
            this.btnSearch.Location = new System.Drawing.Point(1245, 9);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(91, 27);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Αναζήτηση";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panTools
            // 
            this.panTools.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panTools.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panTools.Controls.Add(this.comboBox1);
            this.panTools.Controls.Add(this.label7);
            this.panTools.Controls.Add(this.lstAdvisor);
            this.panTools.Controls.Add(this.toolLeft);
            this.panTools.Controls.Add(this.Label8);
            this.panTools.Location = new System.Drawing.Point(7, 61);
            this.panTools.Name = "panTools";
            this.panTools.Size = new System.Drawing.Size(1355, 34);
            this.panTools.TabIndex = 2103;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Όλα",
            "Μόνο Θετικά",
            "Μόνο Αρνιτικά",
            "Μόνο Μηδενικά"});
            this.comboBox1.Location = new System.Drawing.Point(573, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(127, 21);
            this.comboBox1.TabIndex = 1121;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(508, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 1122;
            this.label7.Text = "Total Value";
            // 
            // lstAdvisor
            // 
            this.lstAdvisor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstAdvisor.FormattingEnabled = true;
            this.lstAdvisor.Location = new System.Drawing.Point(220, 6);
            this.lstAdvisor.Name = "lstAdvisor";
            this.lstAdvisor.Size = new System.Drawing.Size(255, 21);
            this.lstAdvisor.TabIndex = 12;
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
            this.ToolStripLabel15,
            this.tsbImport,
            this.toolStripSeparator2,
            this.tsbView,
            this.toolStripSeparator1,
            this.tsbExcel,
            this.toolStripSeparator15,
            this.tsbHelp});
            this.toolLeft.Location = new System.Drawing.Point(3, 3);
            this.toolLeft.Name = "toolLeft";
            this.toolLeft.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolLeft.Size = new System.Drawing.Size(139, 28);
            this.toolLeft.TabIndex = 255;
            this.toolLeft.Text = "ToolStrip1";
            // 
            // ToolStripLabel15
            // 
            this.ToolStripLabel15.Name = "ToolStripLabel15";
            this.ToolStripLabel15.Size = new System.Drawing.Size(10, 25);
            this.ToolStripLabel15.Text = " ";
            // 
            // tsbImport
            // 
            this.tsbImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbImport.Image = global::Accounting.Properties.Resources.download;
            this.tsbImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImport.Name = "tsbImport";
            this.tsbImport.Size = new System.Drawing.Size(23, 25);
            this.tsbImport.Text = "Εισαγωγή αρχείου";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // tsbView
            // 
            this.tsbView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbView.Image = global::Accounting.Properties.Resources.eye;
            this.tsbView.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbView.Name = "tsbView";
            this.tsbView.Size = new System.Drawing.Size(23, 25);
            this.tsbView.Text = "Portfolio View";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // tsbExcel
            // 
            this.tsbExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExcel.Image = global::Accounting.Properties.Resources.excel;
            this.tsbExcel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExcel.Name = "tsbExcel";
            this.tsbExcel.Size = new System.Drawing.Size(23, 25);
            this.tsbExcel.Text = "Εξαγωγή λίστας σε EXCEL-αρχείο";
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(6, 28);
            // 
            // tsbHelp
            // 
            this.tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbHelp.Image")));
            this.tsbHelp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(23, 25);
            this.tsbHelp.Text = "He&lp";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(172, 10);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(42, 13);
            this.Label8.TabIndex = 1029;
            this.Label8.Text = "Advisor";
            // 
            // frmAcc_Trx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1369, 861);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.panCritiries);
            this.Controls.Add(this.panTools);
            this.Name = "frmAcc_Trx";
            this.Text = "frmAcc_Trx";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmAcc_Trx_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panCritiries.ResumeLayout(false);
            this.panCritiries.PerformLayout();
            this.panTools.ResumeLayout(false);
            this.panTools.PerformLayout();
            this.toolLeft.ResumeLayout(false);
            this.toolLeft.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        internal System.Windows.Forms.Panel panCritiries;
        internal System.Windows.Forms.DateTimePicker dDateControl;
        internal System.Windows.Forms.Label Label24;
        internal System.Windows.Forms.Button btnSearch;
        internal System.Windows.Forms.Panel panTools;
        internal System.Windows.Forms.ComboBox lstAdvisor;
        internal System.Windows.Forms.ToolStrip toolLeft;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel15;
        internal System.Windows.Forms.ToolStripButton tsbImport;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        internal System.Windows.Forms.ToolStripButton tsbView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        internal System.Windows.Forms.ToolStripButton tsbExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        internal System.Windows.Forms.ToolStripButton tsbHelp;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.ComboBox comboBox1;
        internal System.Windows.Forms.Label label7;
    }
}