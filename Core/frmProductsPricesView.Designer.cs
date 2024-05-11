
namespace Core
{
    partial class frmProductsPricesView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProductsPricesView));
            this.btnSearch = new System.Windows.Forms.Button();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.Label23 = new System.Windows.Forms.Label();
            this.dFrom = new System.Windows.Forms.DateTimePicker();
            this.Label3 = new System.Windows.Forms.Label();
            this.dTo = new System.Windows.Forms.DateTimePicker();
            this.ToolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolLeft = new System.Windows.Forms.ToolStrip();
            this.cmbProductType = new System.Windows.Forms.ComboBox();
            this.Label59 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.cmbProductCategory = new System.Windows.Forms.ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.lblCode2 = new System.Windows.Forms.Label();
            this.lblISIN = new System.Windows.Forms.Label();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tsbExcel = new System.Windows.Forms.ToolStripButton();
            this.tsbEffect = new System.Windows.Forms.ToolStripButton();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();
            this.ucPS = new Core.ucProductsSearch();
            this.toolLeft.SuspendLayout();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.Control;
            this.btnSearch.Location = new System.Drawing.Point(1086, 96);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(103, 28);
            this.btnSearch.TabIndex = 12;
            this.btnSearch.Text = "Αναζήτηση";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // ToolStripSeparator6
            // 
            this.ToolStripSeparator6.Name = "ToolStripSeparator6";
            this.ToolStripSeparator6.Size = new System.Drawing.Size(6, 28);
            // 
            // Label23
            // 
            this.Label23.AutoSize = true;
            this.Label23.Location = new System.Drawing.Point(3, 7);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(66, 13);
            this.Label23.TabIndex = 1008;
            this.Label23.Text = "Ημερομηνία";
            // 
            // dFrom
            // 
            this.dFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dFrom.Location = new System.Drawing.Point(75, 3);
            this.dFrom.Name = "dFrom";
            this.dFrom.Size = new System.Drawing.Size(92, 20);
            this.dFrom.TabIndex = 2;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(3, 80);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(87, 13);
            this.Label3.TabIndex = 1009;
            this.Label3.Text = "Κωδικός Reuters";
            // 
            // dTo
            // 
            this.dTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dTo.Location = new System.Drawing.Point(181, 3);
            this.dTo.Name = "dTo";
            this.dTo.Size = new System.Drawing.Size(92, 20);
            this.dTo.TabIndex = 4;
            // 
            // ToolStripLabel2
            // 
            this.ToolStripLabel2.Name = "ToolStripLabel2";
            this.ToolStripLabel2.Size = new System.Drawing.Size(10, 25);
            this.ToolStripLabel2.Text = " ";
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
            this.tsbExcel,
            this.ToolStripSeparator6,
            this.tsbEffect,
            this.ToolStripSeparator1,
            this.tsbExport});
            this.toolLeft.Location = new System.Drawing.Point(6, 145);
            this.toolLeft.Name = "toolLeft";
            this.toolLeft.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolLeft.Size = new System.Drawing.Size(104, 28);
            this.toolLeft.TabIndex = 1028;
            this.toolLeft.Text = "ToolStrip1";
            // 
            // cmbProductType
            // 
            this.cmbProductType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProductType.FormattingEnabled = true;
            this.cmbProductType.Location = new System.Drawing.Point(74, 27);
            this.cmbProductType.Name = "cmbProductType";
            this.cmbProductType.Size = new System.Drawing.Size(201, 21);
            this.cmbProductType.TabIndex = 6;
            // 
            // Label59
            // 
            this.Label59.AutoSize = true;
            this.Label59.Location = new System.Drawing.Point(3, 30);
            this.Label59.Name = "Label59";
            this.Label59.Size = new System.Drawing.Size(42, 13);
            this.Label59.TabIndex = 1023;
            this.Label59.Text = "Προϊόν";
            // 
            // Label12
            // 
            this.Label12.Location = new System.Drawing.Point(309, 31);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(65, 18);
            this.Label12.TabIndex = 1021;
            this.Label12.Text = "Κατηγορία";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(3, 56);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(43, 13);
            this.Label1.TabIndex = 1019;
            this.Label1.Text = "Φίλτρο";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(3, 106);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(100, 13);
            this.Label7.TabIndex = 1015;
            this.Label7.Text = "Κωδικός Bloomberg";
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this.ucPS);
            this.Panel1.Controls.Add(this.Label23);
            this.Panel1.Controls.Add(this.dFrom);
            this.Panel1.Controls.Add(this.Label3);
            this.Panel1.Controls.Add(this.btnSearch);
            this.Panel1.Controls.Add(this.cmbProductType);
            this.Panel1.Controls.Add(this.Label59);
            this.Panel1.Controls.Add(this.dTo);
            this.Panel1.Controls.Add(this.Label12);
            this.Panel1.Controls.Add(this.cmbProductCategory);
            this.Panel1.Controls.Add(this.lblTitle);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Controls.Add(this.lblCode);
            this.Panel1.Controls.Add(this.Label4);
            this.Panel1.Controls.Add(this.Label2);
            this.Panel1.Controls.Add(this.lblCode2);
            this.Panel1.Controls.Add(this.Label7);
            this.Panel1.Controls.Add(this.lblISIN);
            this.Panel1.Location = new System.Drawing.Point(6, 7);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(1216, 131);
            this.Panel1.TabIndex = 1029;
            // 
            // cmbProductCategory
            // 
            this.cmbProductCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProductCategory.FormattingEnabled = true;
            this.cmbProductCategory.Location = new System.Drawing.Point(378, 28);
            this.cmbProductCategory.Name = "cmbProductCategory";
            this.cmbProductCategory.Size = new System.Drawing.Size(256, 21);
            this.cmbProductCategory.TabIndex = 8;
            // 
            // lblTitle
            // 
            this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTitle.Location = new System.Drawing.Point(334, 103);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(456, 20);
            this.lblTitle.TabIndex = 1012;
            // 
            // lblCode
            // 
            this.lblCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCode.Location = new System.Drawing.Point(107, 77);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(160, 20);
            this.lblCode.TabIndex = 1013;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(291, 106);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(39, 13);
            this.Label4.TabIndex = 1018;
            this.Label4.Text = "Τίτλος";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(292, 80);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(28, 13);
            this.Label2.TabIndex = 1014;
            this.Label2.Text = "ISIN";
            // 
            // lblCode2
            // 
            this.lblCode2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCode2.Location = new System.Drawing.Point(107, 103);
            this.lblCode2.Name = "lblCode2";
            this.lblCode2.Size = new System.Drawing.Size(160, 20);
            this.lblCode2.TabIndex = 1017;
            // 
            // lblISIN
            // 
            this.lblISIN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblISIN.Location = new System.Drawing.Point(334, 77);
            this.lblISIN.Name = "lblISIN";
            this.lblISIN.Size = new System.Drawing.Size(169, 20);
            this.lblISIN.TabIndex = 1016;
            // 
            // fgList
            // 
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(6, 177);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(1216, 561);
            this.fgList.TabIndex = 1026;
            // 
            // tsbExcel
            // 
            this.tsbExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExcel.Image = global::Core.Properties.Resources.excel;
            this.tsbExcel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExcel.Name = "tsbExcel";
            this.tsbExcel.Size = new System.Drawing.Size(23, 25);
            this.tsbExcel.Text = "Εξαγωγή λίστας σε EXCEL-αρχείο";
            // 
            // tsbEffect
            // 
            this.tsbEffect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEffect.Image = global::Core.Properties.Resources.evernote;
            this.tsbEffect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEffect.Name = "tsbEffect";
            this.tsbEffect.Size = new System.Drawing.Size(23, 25);
            this.tsbEffect.Text = "Εξαγωγή στο Effect";
            // 
            // tsbExport
            // 
            this.tsbExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExport.Image = ((System.Drawing.Image)(resources.GetObject("tsbExport.Image")));
            this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.Size = new System.Drawing.Size(23, 25);
            this.tsbExport.Text = "Εξαγωγή λίστας";
            // 
            // ucPS
            // 
            this.ucPS.BlockNonRecommended = false;
            this.ucPS.CodesList = null;
            this.ucPS.Filters = "Shares_ID > 0";
            this.ucPS.ListType = 0;
            this.ucPS.Location = new System.Drawing.Point(73, 52);
            this.ucPS.Mode = 0;
            this.ucPS.Name = "ucPS";
            this.ucPS.ProductsContract = null;
            this.ucPS.ShowCancelled = true;
            this.ucPS.ShowHeight = 0;
            this.ucPS.ShowNonAccord = true;
            this.ucPS.ShowProductsList = true;
            this.ucPS.ShowWidth = 0;
            this.ucPS.Size = new System.Drawing.Size(202, 20);
            this.ucPS.TabIndex = 1024;
            // 
            // frmProductsPricesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightPink;
            this.ClientSize = new System.Drawing.Size(1228, 741);
            this.Controls.Add(this.toolLeft);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.fgList);
            this.Name = "frmProductsPricesView";
            this.Text = "frmProductsPricesView";
            this.Load += new System.EventHandler(this.frmProductsPricesView_Load);
            this.toolLeft.ResumeLayout(false);
            this.toolLeft.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btnSearch;
        internal System.Windows.Forms.ToolStripButton tsbExport;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripButton tsbEffect;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator6;
        internal System.Windows.Forms.Label Label23;
        internal System.Windows.Forms.DateTimePicker dFrom;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.DateTimePicker dTo;
        internal System.Windows.Forms.ToolStripButton tsbExcel;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel2;
        internal System.Windows.Forms.ToolStrip toolLeft;
        internal System.Windows.Forms.ComboBox cmbProductType;
        internal System.Windows.Forms.Label Label59;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.ComboBox cmbProductCategory;
        internal System.Windows.Forms.Label lblTitle;
        internal System.Windows.Forms.Label lblCode;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label lblCode2;
        internal System.Windows.Forms.Label lblISIN;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
        private ucProductsSearch ucPS;
    }
}