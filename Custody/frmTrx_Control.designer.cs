namespace Custody
{
    partial class frmTrx_Control
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTrx_Control));
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpTitles = new System.Windows.Forms.TabPage();
            this.tpMoney = new System.Windows.Forms.TabPage();
            this.ucDates = new Core.ucDoubleCalendar();
            this.grdList = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grdDetails = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panMain = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbServiceProviders = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.cmbTrxCurrency = new System.Windows.Forms.ComboBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtInvoice = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.ucCS = new Core.ucContractsSearch();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.toolProtaseis = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEdit = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExcel = new System.Windows.Forms.ToolStripButton();
            this.popMain = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barMain = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.tcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.panMain.SuspendLayout();
            this.toolProtaseis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barMain)).BeginInit();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpTitles);
            this.tcMain.Controls.Add(this.tpMoney);
            this.tcMain.Location = new System.Drawing.Point(7, 5);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(1468, 24);
            this.tcMain.TabIndex = 1115;
            // 
            // tpTitles
            // 
            this.tpTitles.Location = new System.Drawing.Point(4, 22);
            this.tpTitles.Name = "tpTitles";
            this.tpTitles.Padding = new System.Windows.Forms.Padding(3);
            this.tpTitles.Size = new System.Drawing.Size(1460, 0);
            this.tpTitles.TabIndex = 0;
            this.tpTitles.Text = "Τίτλοι";
            this.tpTitles.UseVisualStyleBackColor = true;
            // 
            // tpMoney
            // 
            this.tpMoney.Location = new System.Drawing.Point(4, 22);
            this.tpMoney.Name = "tpMoney";
            this.tpMoney.Size = new System.Drawing.Size(1460, 0);
            this.tpMoney.TabIndex = 3;
            this.tpMoney.Text = "Χρήματα";
            this.tpMoney.UseVisualStyleBackColor = true;
            // 
            // ucDates
            // 
            this.ucDates.BackColor = System.Drawing.Color.Gainsboro;
            this.ucDates.DateFrom = new System.DateTime(2020, 8, 1, 16, 39, 27, 913);
            this.ucDates.DateTo = new System.DateTime(2020, 8, 1, 16, 39, 27, 913);
            this.ucDates.Location = new System.Drawing.Point(13, 6);
            this.ucDates.Name = "ucDates";
            this.ucDates.Size = new System.Drawing.Size(233, 21);
            this.ucDates.TabIndex = 1116;
            // 
            // grdList
            // 
            this.grdList.Location = new System.Drawing.Point(7, 174);
            this.grdList.MainView = this.gridView1;
            this.grdList.Name = "grdList";
            this.grdList.Size = new System.Drawing.Size(1200, 479);
            this.grdList.TabIndex = 0;
            this.grdList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdList;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            // 
            // grdDetails
            // 
            this.grdDetails.Location = new System.Drawing.Point(1215, 174);
            this.grdDetails.MainView = this.gridView2;
            this.grdDetails.Name = "grdDetails";
            this.grdDetails.Size = new System.Drawing.Size(260, 479);
            this.grdDetails.TabIndex = 1118;
            this.grdDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.grdDetails;
            this.gridView2.Name = "gridView2";
            // 
            // panMain
            // 
            this.panMain.BackColor = System.Drawing.Color.Gainsboro;
            this.panMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panMain.Controls.Add(this.btnSearch);
            this.panMain.Controls.Add(this.cmbServiceProviders);
            this.panMain.Controls.Add(this.label4);
            this.panMain.Controls.Add(this.checkBox4);
            this.panMain.Controls.Add(this.cmbTrxCurrency);
            this.panMain.Controls.Add(this.Label5);
            this.panMain.Controls.Add(this.textBox1);
            this.panMain.Controls.Add(this.label3);
            this.panMain.Controls.Add(this.txtInvoice);
            this.panMain.Controls.Add(this.label2);
            this.panMain.Controls.Add(this.Label1);
            this.panMain.Controls.Add(this.ucCS);
            this.panMain.Controls.Add(this.checkBox3);
            this.panMain.Controls.Add(this.checkBox2);
            this.panMain.Controls.Add(this.checkBox1);
            this.panMain.Controls.Add(this.ucDates);
            this.panMain.Location = new System.Drawing.Point(7, 31);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(1468, 111);
            this.panMain.TabIndex = 1119;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(1358, 66);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(92, 28);
            this.btnSearch.TabIndex = 1131;
            this.btnSearch.Text = "Αναζήτηση";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cmbServiceProviders
            // 
            this.cmbServiceProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServiceProviders.FormattingEnabled = true;
            this.cmbServiceProviders.Location = new System.Drawing.Point(922, 45);
            this.cmbServiceProviders.Name = "cmbServiceProviders";
            this.cmbServiceProviders.Size = new System.Drawing.Size(220, 21);
            this.cmbServiceProviders.TabIndex = 1130;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(863, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 1129;
            this.label4.Text = "Πάροχος";
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(36, 86);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(45, 17);
            this.checkBox4.TabIndex = 1128;
            this.checkBox4.Text = "Ολα";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // cmbTrxCurrency
            // 
            this.cmbTrxCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrxCurrency.FormattingEnabled = true;
            this.cmbTrxCurrency.Location = new System.Drawing.Point(605, 71);
            this.cmbTrxCurrency.Name = "cmbTrxCurrency";
            this.cmbTrxCurrency.Size = new System.Drawing.Size(80, 21);
            this.cmbTrxCurrency.TabIndex = 1126;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(550, 78);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(49, 13);
            this.Label5.TabIndex = 1127;
            this.Label5.Text = "Νόμισμα";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(297, 45);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(200, 20);
            this.textBox1.TabIndex = 1125;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(548, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 1124;
            this.label3.Text = "Τίτλος";
            // 
            // txtInvoice
            // 
            this.txtInvoice.Location = new System.Drawing.Point(297, 70);
            this.txtInvoice.Name = "txtInvoice";
            this.txtInvoice.Size = new System.Drawing.Size(200, 20);
            this.txtInvoice.TabIndex = 1123;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(209, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 1122;
            this.label2.Text = "Παραστατικό";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(209, 47);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(52, 13);
            this.Label1.TabIndex = 1121;
            this.Label1.Text = "Σύμβαση";
            // 
            // ucCS
            // 
            this.ucCS.BackColor = System.Drawing.Color.Transparent;
            this.ucCS.CodesList = null;
            this.ucCS.Filters = "Client_ID > 0 AND Status = 1";
            this.ucCS.ListType = 0;
            this.ucCS.Location = new System.Drawing.Point(605, 47);
            this.ucCS.Mode = 0;
            this.ucCS.Name = "ucCS";
            this.ucCS.ShowClientsList = true;
            this.ucCS.ShowHeight = 0;
            this.ucCS.ShowWidth = 0;
            this.ucCS.Size = new System.Drawing.Size(200, 20);
            this.ucCS.TabIndex = 1120;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(266, 10);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(120, 17);
            this.checkBox3.TabIndex = 1119;
            this.checkBox3.Text = "Ημ. Διακανονισμού";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(36, 65);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(74, 17);
            this.checkBox2.TabIndex = 1118;
            this.checkBox2.Text = "Πωλήσεις";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(36, 42);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(63, 17);
            this.checkBox1.TabIndex = 1117;
            this.checkBox1.Text = "Αγορές";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // toolProtaseis
            // 
            this.toolProtaseis.AutoSize = false;
            this.toolProtaseis.BackColor = System.Drawing.Color.Gainsboro;
            this.toolProtaseis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolProtaseis.Dock = System.Windows.Forms.DockStyle.None;
            this.toolProtaseis.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolProtaseis.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolProtaseis.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripLabel7,
            this.tsbAdd,
            this.ToolStripSeparator10,
            this.tsbEdit,
            this.ToolStripSeparator11,
            this.tsbExcel});
            this.toolProtaseis.Location = new System.Drawing.Point(7, 146);
            this.toolProtaseis.Name = "toolProtaseis";
            this.toolProtaseis.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolProtaseis.Size = new System.Drawing.Size(109, 25);
            this.toolProtaseis.TabIndex = 1120;
            this.toolProtaseis.Text = "ToolStrip1";
            // 
            // ToolStripLabel7
            // 
            this.ToolStripLabel7.Name = "ToolStripLabel7";
            this.ToolStripLabel7.Size = new System.Drawing.Size(10, 22);
            this.ToolStripLabel7.Text = " ";
            // 
            // tsbAdd
            // 
            this.tsbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbAdd.Image")));
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(23, 22);
            this.tsbAdd.Text = "Προσθήκη";
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // ToolStripSeparator10
            // 
            this.ToolStripSeparator10.Name = "ToolStripSeparator10";
            this.ToolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbEdit
            // 
            this.tsbEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEdit.Image = global::Custody.Properties.Resources.edit;
            this.tsbEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEdit.Name = "tsbEdit";
            this.tsbEdit.Size = new System.Drawing.Size(23, 22);
            this.tsbEdit.Text = "Διόρθωση εγγραφής";
            this.tsbEdit.Click += new System.EventHandler(this.tsbEdit_Click);
            // 
            // ToolStripSeparator11
            // 
            this.ToolStripSeparator11.Name = "ToolStripSeparator11";
            this.ToolStripSeparator11.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbExcel
            // 
            this.tsbExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExcel.Image = global::Custody.Properties.Resources.excel;
            this.tsbExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExcel.Name = "tsbExcel";
            this.tsbExcel.Size = new System.Drawing.Size(23, 22);
            this.tsbExcel.Text = "Εξαγωγή στο Excel";
            this.tsbExcel.Click += new System.EventHandler(this.tsbExcel_Click);
            // 
            // popMain
            // 
            this.popMain.Manager = this.barMain;
            this.popMain.Name = "popMain";
            // 
            // barMain
            // 
            this.barMain.DockControls.Add(this.barDockControlTop);
            this.barMain.DockControls.Add(this.barDockControlBottom);
            this.barMain.DockControls.Add(this.barDockControlLeft);
            this.barMain.DockControls.Add(this.barDockControlRight);
            this.barMain.Form = this;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barMain;
            this.barDockControlTop.Size = new System.Drawing.Size(1506, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 698);
            this.barDockControlBottom.Manager = this.barMain;
            this.barDockControlBottom.Size = new System.Drawing.Size(1506, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barMain;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 698);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1506, 0);
            this.barDockControlRight.Manager = this.barMain;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 698);
            // 
            // frmTrx_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Bisque;
            this.ClientSize = new System.Drawing.Size(1506, 698);
            this.Controls.Add(this.toolProtaseis);
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.grdDetails);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmTrx_Control";
            this.Text = "Κινήσεις";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmTrx_Control_Load);
            this.tcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.panMain.ResumeLayout(false);
            this.panMain.PerformLayout();
            this.toolProtaseis.ResumeLayout(false);
            this.toolProtaseis.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TabControl tcMain;
        internal System.Windows.Forms.TabPage tpTitles;
        internal System.Windows.Forms.TabPage tpMoney;
        private Core.ucDoubleCalendar ucDates;
        private DevExpress.XtraGrid.GridControl grdList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.GridControl grdDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.Panel panMain;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private Core.ucContractsSearch ucCS;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox textBox1;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox txtInvoice;
        internal System.Windows.Forms.ComboBox cmbTrxCurrency;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox4;
        internal System.Windows.Forms.ComboBox cmbServiceProviders;
        internal System.Windows.Forms.Button btnSearch;
        internal System.Windows.Forms.ToolStrip toolProtaseis;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel7;
        internal System.Windows.Forms.ToolStripButton tsbAdd;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator10;
        internal System.Windows.Forms.ToolStripButton tsbEdit;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator11;
        internal System.Windows.Forms.ToolStripButton tsbExcel;
        private DevExpress.XtraBars.PopupMenu popMain;
        private DevExpress.XtraBars.BarManager barMain;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}