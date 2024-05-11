
namespace Contracts
{
    partial class ucOfficialInforming_FX
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucOfficialInforming_FX));
            this.mnuCommandData = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClientData = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContractData = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuViewInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.picEmptyClient = new System.Windows.Forms.PictureBox();
            this.tsbPrint = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSend = new System.Windows.Forms.ToolStripButton();
            this.ToolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.chkRTO = new System.Windows.Forms.CheckBox();
            this.Label41 = new System.Windows.Forms.Label();
            this.Label43 = new System.Windows.Forms.Label();
            this.dTo = new System.Windows.Forms.DateTimePicker();
            this.cmbProviders = new System.Windows.Forms.ComboBox();
            this.dFrom = new System.Windows.Forms.DateTimePicker();
            this.Label42 = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.lblPortfolio = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lnkPelatis = new System.Windows.Forms.LinkLabel();
            this.toolLeft = new System.Windows.Forms.ToolStrip();
            this.Label40 = new System.Windows.Forms.Label();
            this.ucCS = new Core.ucContractsSearch();
            this.mnuContext.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEmptyClient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.toolLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuCommandData
            // 
            this.mnuCommandData.Name = "mnuCommandData";
            this.mnuCommandData.Size = new System.Drawing.Size(254, 22);
            this.mnuCommandData.Text = "Στοιχεία της εντολής";
            this.mnuCommandData.Click += new System.EventHandler(this.mnuCommandData_Click);
            // 
            // mnuClientData
            // 
            this.mnuClientData.Name = "mnuClientData";
            this.mnuClientData.Size = new System.Drawing.Size(254, 22);
            this.mnuClientData.Text = "Στοιχεία τού πελάτη";
            this.mnuClientData.Click += new System.EventHandler(this.mnuClientData_Click);
            // 
            // mnuContractData
            // 
            this.mnuContractData.Name = "mnuContractData";
            this.mnuContractData.Size = new System.Drawing.Size(254, 22);
            this.mnuContractData.Text = "Στοιχεία Σύμβασης";
            this.mnuContractData.Click += new System.EventHandler(this.mnuContractData_Click);
            // 
            // mnuContext
            // 
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContractData,
            this.mnuClientData,
            this.mnuCommandData,
            this.mnuViewInvoice});
            this.mnuContext.Name = "mnuContext";
            this.mnuContext.Size = new System.Drawing.Size(255, 92);
            // 
            // mnuViewInvoice
            // 
            this.mnuViewInvoice.Name = "mnuViewInvoice";
            this.mnuViewInvoice.Size = new System.Drawing.Size(254, 22);
            this.mnuViewInvoice.Text = "Προβολή του αρχείου Τιμολογίου";
            this.mnuViewInvoice.Click += new System.EventHandler(this.mnuViewInvoice_Click);
            // 
            // picEmptyClient
            // 
            this.picEmptyClient.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picEmptyClient.Image = global::Contracts.Properties.Resources.cleanup;
            this.picEmptyClient.Location = new System.Drawing.Point(634, 5);
            this.picEmptyClient.Name = "picEmptyClient";
            this.picEmptyClient.Size = new System.Drawing.Size(22, 21);
            this.picEmptyClient.TabIndex = 1069;
            this.picEmptyClient.TabStop = false;
            // 
            // tsbPrint
            // 
            this.tsbPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPrint.Image = global::Contracts.Properties.Resources.PrintHS;
            this.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Size = new System.Drawing.Size(23, 22);
            this.tsbPrint.Text = "Εκτύπωση";
            this.tsbPrint.Click += new System.EventHandler(this.tsbPrint_Click);
            // 
            // ToolStripSeparator15
            // 
            this.ToolStripSeparator15.Name = "ToolStripSeparator15";
            this.ToolStripSeparator15.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbSend
            // 
            this.tsbSend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSend.Image = global::Contracts.Properties.Resources.emailicon;
            this.tsbSend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSend.Name = "tsbSend";
            this.tsbSend.Size = new System.Drawing.Size(23, 22);
            this.tsbSend.Text = "Αποστολή";
            this.tsbSend.Click += new System.EventHandler(this.tsbSend_Click);
            // 
            // ToolStripLabel4
            // 
            this.ToolStripLabel4.Name = "ToolStripLabel4";
            this.ToolStripLabel4.Size = new System.Drawing.Size(10, 22);
            this.ToolStripLabel4.Text = " ";
            // 
            // chkRTO
            // 
            this.chkRTO.AutoSize = true;
            this.chkRTO.Location = new System.Drawing.Point(7, 107);
            this.chkRTO.Name = "chkRTO";
            this.chkRTO.Size = new System.Drawing.Size(15, 14);
            this.chkRTO.TabIndex = 1061;
            this.chkRTO.UseVisualStyleBackColor = true;
            this.chkRTO.CheckedChanged += new System.EventHandler(this.chkRTO_CheckedChanged);
            // 
            // Label41
            // 
            this.Label41.AutoSize = true;
            this.Label41.Location = new System.Drawing.Point(190, 10);
            this.Label41.Name = "Label41";
            this.Label41.Size = new System.Drawing.Size(27, 13);
            this.Label41.TabIndex = 1068;
            this.Label41.Text = "εώς";
            // 
            // Label43
            // 
            this.Label43.AutoSize = true;
            this.Label43.Location = new System.Drawing.Point(3, 9);
            this.Label43.Name = "Label43";
            this.Label43.Size = new System.Drawing.Size(80, 13);
            this.Label43.TabIndex = 1067;
            this.Label43.Text = "Ημερ/νίες από";
            // 
            // dTo
            // 
            this.dTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dTo.Location = new System.Drawing.Point(223, 6);
            this.dTo.Name = "dTo";
            this.dTo.Size = new System.Drawing.Size(93, 20);
            this.dTo.TabIndex = 1056;
            // 
            // cmbProviders
            // 
            this.cmbProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProviders.FormattingEnabled = true;
            this.cmbProviders.Location = new System.Drawing.Point(59, 33);
            this.cmbProviders.Name = "cmbProviders";
            this.cmbProviders.Size = new System.Drawing.Size(272, 21);
            this.cmbProviders.TabIndex = 1057;
            // 
            // dFrom
            // 
            this.dFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dFrom.Location = new System.Drawing.Point(89, 6);
            this.dFrom.Name = "dFrom";
            this.dFrom.Size = new System.Drawing.Size(93, 20);
            this.dFrom.TabIndex = 1055;
            // 
            // Label42
            // 
            this.Label42.AutoSize = true;
            this.Label42.Location = new System.Drawing.Point(3, 37);
            this.Label42.Name = "Label42";
            this.Label42.Size = new System.Drawing.Size(51, 13);
            this.Label42.TabIndex = 1060;
            this.Label42.Text = "Πάροχος";
            // 
            // lblCode
            // 
            this.lblCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCode.Location = new System.Drawing.Point(429, 30);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(110, 20);
            this.lblCode.TabIndex = 1064;
            // 
            // fgList
            // 
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(3, 104);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(1279, 566);
            this.fgList.SubtotalPosition = C1.Win.C1FlexGrid.SubtotalPositionEnum.BelowData;
            this.fgList.TabIndex = 1054;
            // 
            // lblPortfolio
            // 
            this.lblPortfolio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPortfolio.Location = new System.Drawing.Point(832, 29);
            this.lblPortfolio.Name = "lblPortfolio";
            this.lblPortfolio.Size = new System.Drawing.Size(180, 20);
            this.lblPortfolio.TabIndex = 1066;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(1177, 22);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(91, 27);
            this.btnSearch.TabIndex = 1059;
            this.btnSearch.Text = "Αναζήτηση";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lnkPelatis
            // 
            this.lnkPelatis.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lnkPelatis.Location = new System.Drawing.Point(545, 30);
            this.lnkPelatis.Name = "lnkPelatis";
            this.lnkPelatis.Size = new System.Drawing.Size(281, 20);
            this.lnkPelatis.TabIndex = 1065;
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
            this.ToolStripLabel4,
            this.tsbSend,
            this.ToolStripSeparator15,
            this.tsbPrint});
            this.toolLeft.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolLeft.Location = new System.Drawing.Point(3, 74);
            this.toolLeft.Name = "toolLeft";
            this.toolLeft.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolLeft.Size = new System.Drawing.Size(83, 25);
            this.toolLeft.TabIndex = 1062;
            this.toolLeft.Text = "ToolStrip1";
            // 
            // Label40
            // 
            this.Label40.AutoSize = true;
            this.Label40.Location = new System.Drawing.Point(373, 9);
            this.Label40.Name = "Label40";
            this.Label40.Size = new System.Drawing.Size(50, 13);
            this.Label40.TabIndex = 1063;
            this.Label40.Text = "Πελάτης";
            // 
            // ucCS
            // 
            this.ucCS.BackColor = System.Drawing.Color.Transparent;
            this.ucCS.CodesList = null;
            this.ucCS.Filters = "Client_ID > 0 AND Status = 1";
            this.ucCS.ListType = 0;
            this.ucCS.Location = new System.Drawing.Point(429, 5);
            this.ucCS.Mode = 0;
            this.ucCS.Name = "ucCS";
            this.ucCS.ShowClientsList = true;
            this.ucCS.ShowHeight = 0;
            this.ucCS.ShowWidth = 0;
            this.ucCS.Size = new System.Drawing.Size(200, 20);
            this.ucCS.TabIndex = 1058;
            // 
            // ucOfficialInforming_FX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.Controls.Add(this.ucCS);
            this.Controls.Add(this.picEmptyClient);
            this.Controls.Add(this.chkRTO);
            this.Controls.Add(this.Label41);
            this.Controls.Add(this.Label43);
            this.Controls.Add(this.dTo);
            this.Controls.Add(this.cmbProviders);
            this.Controls.Add(this.dFrom);
            this.Controls.Add(this.Label42);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.fgList);
            this.Controls.Add(this.lblPortfolio);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lnkPelatis);
            this.Controls.Add(this.toolLeft);
            this.Controls.Add(this.Label40);
            this.Name = "ucOfficialInforming_FX";
            this.Size = new System.Drawing.Size(1285, 674);
            this.Load += new System.EventHandler(this.ucOfficialInforming_FX_Load);
            this.mnuContext.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picEmptyClient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.toolLeft.ResumeLayout(false);
            this.toolLeft.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Core.ucContractsSearch ucCS;
        internal System.Windows.Forms.ToolStripMenuItem mnuCommandData;
        internal System.Windows.Forms.ToolStripMenuItem mnuClientData;
        internal System.Windows.Forms.ToolStripMenuItem mnuContractData;
        internal System.Windows.Forms.ContextMenuStrip mnuContext;
        internal System.Windows.Forms.ToolStripMenuItem mnuViewInvoice;
        internal System.Windows.Forms.PictureBox picEmptyClient;
        internal System.Windows.Forms.ToolStripButton tsbPrint;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator15;
        internal System.Windows.Forms.ToolStripButton tsbSend;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel4;
        internal System.Windows.Forms.CheckBox chkRTO;
        internal System.Windows.Forms.Label Label41;
        internal System.Windows.Forms.Label Label43;
        internal System.Windows.Forms.DateTimePicker dTo;
        internal System.Windows.Forms.ComboBox cmbProviders;
        internal System.Windows.Forms.DateTimePicker dFrom;
        internal System.Windows.Forms.Label Label42;
        internal System.Windows.Forms.Label lblCode;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
        internal System.Windows.Forms.Label lblPortfolio;
        internal System.Windows.Forms.Button btnSearch;
        internal System.Windows.Forms.LinkLabel lnkPelatis;
        internal System.Windows.Forms.ToolStrip toolLeft;
        internal System.Windows.Forms.Label Label40;
    }
}
