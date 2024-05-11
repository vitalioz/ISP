namespace Transactions
{
    partial class frmOrderBasket
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrderBasket));
            this.btnFinish = new System.Windows.Forms.Button();
            this.lblAmount_EUR = new System.Windows.Forms.Label();
            this.lblEUR = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblAmountCurr = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.dConstant = new System.Windows.Forms.DateTimePicker();
            this.Label4 = new System.Windows.Forms.Label();
            this.cmbConstant = new System.Windows.Forms.ComboBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.lblServiceProvider = new System.Windows.Forms.Label();
            this.cmbServiceProviders = new System.Windows.Forms.ComboBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.lblConstant = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.lblProductTitle = new System.Windows.Forms.Label();
            this.lblAction = new System.Windows.Forms.Label();
            this.lblProductCode = new System.Windows.Forms.Label();
            this.panExecutors = new System.Windows.Forms.Panel();
            this.Label8 = new System.Windows.Forms.Label();
            this.lblProductISIN = new System.Windows.Forms.Label();
            this.fgSimpleCommands = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.fgSummary = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.picCopy2Clipboard = new System.Windows.Forms.PictureBox();
            this.picExcel = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lstType = new System.Windows.Forms.ComboBox();
            this.cmbStockExchange = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.chkBestExecution = new System.Windows.Forms.CheckBox();
            this.panExecutors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgSimpleCommands)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fgSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCopy2Clipboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picExcel)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFinish
            // 
            this.btnFinish.BackColor = System.Drawing.SystemColors.Control;
            this.btnFinish.Location = new System.Drawing.Point(991, 635);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(155, 46);
            this.btnFinish.TabIndex = 6;
            this.btnFinish.Text = "Create Order";
            this.btnFinish.UseVisualStyleBackColor = false;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // lblAmount_EUR
            // 
            this.lblAmount_EUR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAmount_EUR.Location = new System.Drawing.Point(767, 659);
            this.lblAmount_EUR.Name = "lblAmount_EUR";
            this.lblAmount_EUR.Size = new System.Drawing.Size(78, 20);
            this.lblAmount_EUR.TabIndex = 1099;
            this.lblAmount_EUR.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEUR
            // 
            this.lblEUR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEUR.Location = new System.Drawing.Point(720, 658);
            this.lblEUR.Name = "lblEUR";
            this.lblEUR.Size = new System.Drawing.Size(44, 21);
            this.lblEUR.TabIndex = 1101;
            this.lblEUR.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label9
            // 
            this.Label9.Location = new System.Drawing.Point(717, 609);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(96, 17);
            this.Label9.TabIndex = 1097;
            this.Label9.Text = "Ποσό Επενδύσεις";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAmount
            // 
            this.lblAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAmount.Location = new System.Drawing.Point(767, 632);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(78, 20);
            this.lblAmount.TabIndex = 1096;
            this.lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAmountCurr
            // 
            this.lblAmountCurr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAmountCurr.Location = new System.Drawing.Point(720, 632);
            this.lblAmountCurr.Name = "lblAmountCurr";
            this.lblAmountCurr.Size = new System.Drawing.Size(44, 21);
            this.lblAmountCurr.TabIndex = 1100;
            this.lblAmountCurr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(71, 558);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(42, 13);
            this.Label6.TabIndex = 1089;
            this.Label6.Text = "Προϊόν";
            // 
            // dConstant
            // 
            this.dConstant.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dConstant.Location = new System.Drawing.Point(620, 607);
            this.dConstant.Name = "dConstant";
            this.dConstant.Size = new System.Drawing.Size(80, 20);
            this.dConstant.TabIndex = 26;
            this.dConstant.Visible = false;
            // 
            // Label4
            // 
            this.Label4.Location = new System.Drawing.Point(727, 558);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(63, 17);
            this.Label4.TabIndex = 1087;
            this.Label4.Text = "Ποσότητα";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbConstant
            // 
            this.cmbConstant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConstant.FormattingEnabled = true;
            this.cmbConstant.Items.AddRange(new object[] {
            "Day Order",
            "GTC",
            "GTDate"});
            this.cmbConstant.Location = new System.Drawing.Point(536, 607);
            this.cmbConstant.Name = "cmbConstant";
            this.cmbConstant.Size = new System.Drawing.Size(81, 21);
            this.cmbConstant.TabIndex = 24;
            this.cmbConstant.SelectedIndexChanged += new System.EventHandler(this.cmbConstant_SelectedIndexChanged);
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(534, 558);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(28, 13);
            this.Label5.TabIndex = 1088;
            this.Label5.Text = "Τιμή";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(618, 578);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(82, 20);
            this.txtPrice.TabIndex = 22;
            this.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrice.LostFocus += new System.EventHandler(this.txtPrice_LostFocus);
            // 
            // lblServiceProvider
            // 
            this.lblServiceProvider.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblServiceProvider.Location = new System.Drawing.Point(68, 632);
            this.lblServiceProvider.Name = "lblServiceProvider";
            this.lblServiceProvider.Size = new System.Drawing.Size(377, 20);
            this.lblServiceProvider.TabIndex = 1091;
            // 
            // cmbServiceProviders
            // 
            this.cmbServiceProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServiceProviders.FormattingEnabled = true;
            this.cmbServiceProviders.Location = new System.Drawing.Point(61, 1);
            this.cmbServiceProviders.Name = "cmbServiceProviders";
            this.cmbServiceProviders.Size = new System.Drawing.Size(249, 21);
            this.cmbServiceProviders.TabIndex = 30;
            this.cmbServiceProviders.SelectedValueChanged += new System.EventHandler(this.cmbServiceProviders_SelectedValueChanged);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(479, 558);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(38, 13);
            this.Label3.TabIndex = 1086;
            this.Label3.Text = "Πράξη";
            // 
            // lblConstant
            // 
            this.lblConstant.Location = new System.Drawing.Point(479, 610);
            this.lblConstant.Name = "lblConstant";
            this.lblConstant.Size = new System.Drawing.Size(51, 13);
            this.lblConstant.TabIndex = 1094;
            this.lblConstant.Text = "Διάρκεια";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(246, 606);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(28, 13);
            this.Label2.TabIndex = 1085;
            this.Label2.Text = "ISIN";
            // 
            // lblQuantity
            // 
            this.lblQuantity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblQuantity.Location = new System.Drawing.Point(720, 578);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(81, 20);
            this.lblQuantity.TabIndex = 1079;
            this.lblQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(13, 634);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(51, 13);
            this.Label7.TabIndex = 1090;
            this.Label7.Text = "Πάροχος";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(13, 606);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(47, 13);
            this.Label1.TabIndex = 1084;
            this.Label1.Text = "Κωδικός";
            // 
            // lblProductTitle
            // 
            this.lblProductTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProductTitle.Location = new System.Drawing.Point(68, 578);
            this.lblProductTitle.Name = "lblProductTitle";
            this.lblProductTitle.Size = new System.Drawing.Size(377, 20);
            this.lblProductTitle.TabIndex = 1080;
            // 
            // lblAction
            // 
            this.lblAction.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAction.Location = new System.Drawing.Point(474, 578);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(48, 20);
            this.lblAction.TabIndex = 1083;
            // 
            // lblProductCode
            // 
            this.lblProductCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProductCode.Location = new System.Drawing.Point(68, 603);
            this.lblProductCode.Name = "lblProductCode";
            this.lblProductCode.Size = new System.Drawing.Size(168, 20);
            this.lblProductCode.TabIndex = 1081;
            // 
            // panExecutors
            // 
            this.panExecutors.Controls.Add(this.cmbServiceProviders);
            this.panExecutors.Controls.Add(this.Label8);
            this.panExecutors.Location = new System.Drawing.Point(880, 572);
            this.panExecutors.Name = "panExecutors";
            this.panExecutors.Size = new System.Drawing.Size(316, 24);
            this.panExecutors.TabIndex = 1095;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(4, 6);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(51, 13);
            this.Label8.TabIndex = 1036;
            this.Label8.Text = "Πάροχος";
            // 
            // lblProductISIN
            // 
            this.lblProductISIN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProductISIN.Location = new System.Drawing.Point(278, 603);
            this.lblProductISIN.Name = "lblProductISIN";
            this.lblProductISIN.Size = new System.Drawing.Size(167, 20);
            this.lblProductISIN.TabIndex = 1082;
            // 
            // fgSimpleCommands
            // 
            this.fgSimpleCommands.ColumnInfo = resources.GetString("fgSimpleCommands.ColumnInfo");
            this.fgSimpleCommands.Location = new System.Drawing.Point(7, 351);
            this.fgSimpleCommands.Name = "fgSimpleCommands";
            this.fgSimpleCommands.Rows.Count = 1;
            this.fgSimpleCommands.Rows.DefaultSize = 17;
            this.fgSimpleCommands.Size = new System.Drawing.Size(1189, 199);
            this.fgSimpleCommands.TabIndex = 4;
            // 
            // fgSummary
            // 
            this.fgSummary.ColumnInfo = resources.GetString("fgSummary.ColumnInfo");
            this.fgSummary.Location = new System.Drawing.Point(7, 33);
            this.fgSummary.Name = "fgSummary";
            this.fgSummary.Rows.Count = 2;
            this.fgSummary.Rows.DefaultSize = 17;
            this.fgSummary.Rows.Fixed = 2;
            this.fgSummary.Size = new System.Drawing.Size(1189, 312);
            this.fgSummary.TabIndex = 2;
            // 
            // picCopy2Clipboard
            // 
            this.picCopy2Clipboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picCopy2Clipboard.Image = global::Transactions.Properties.Resources.clipboard_sign;
            this.picCopy2Clipboard.Location = new System.Drawing.Point(449, 606);
            this.picCopy2Clipboard.Name = "picCopy2Clipboard";
            this.picCopy2Clipboard.Size = new System.Drawing.Size(16, 17);
            this.picCopy2Clipboard.TabIndex = 1098;
            this.picCopy2Clipboard.TabStop = false;
            this.picCopy2Clipboard.Click += new System.EventHandler(this.picCopy2Clipboard_Click);
            // 
            // picExcel
            // 
            this.picExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picExcel.Image = ((System.Drawing.Image)(resources.GetObject("picExcel.Image")));
            this.picExcel.Location = new System.Drawing.Point(33, 9);
            this.picExcel.Name = "picExcel";
            this.picExcel.Size = new System.Drawing.Size(18, 18);
            this.picExcel.TabIndex = 1076;
            this.picExcel.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 581);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 13);
            this.label10.TabIndex = 1103;
            this.label10.Text = "Τίτλος";
            // 
            // lstType
            // 
            this.lstType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstType.FormattingEnabled = true;
            this.lstType.Items.AddRange(new object[] {
            "Limit",
            "Market",
            "Stop",
            "Scenario",
            "ATC",
            "ATO"});
            this.lstType.Location = new System.Drawing.Point(537, 578);
            this.lstType.Name = "lstType";
            this.lstType.Size = new System.Drawing.Size(76, 21);
            this.lstType.TabIndex = 20;
            // 
            // cmbStockExchange
            // 
            this.cmbStockExchange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStockExchange.FormattingEnabled = true;
            this.cmbStockExchange.Location = new System.Drawing.Point(1108, 604);
            this.cmbStockExchange.Name = "cmbStockExchange";
            this.cmbStockExchange.Size = new System.Drawing.Size(81, 21);
            this.cmbStockExchange.TabIndex = 34;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(1051, 607);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 13);
            this.label11.TabIndex = 1106;
            this.label11.Text = "Χρημ/ριο";
            // 
            // chkBestExecution
            // 
            this.chkBestExecution.AutoSize = true;
            this.chkBestExecution.Location = new System.Drawing.Point(880, 607);
            this.chkBestExecution.Name = "chkBestExecution";
            this.chkBestExecution.Size = new System.Drawing.Size(97, 17);
            this.chkBestExecution.TabIndex = 32;
            this.chkBestExecution.Text = "Best Execution";
            this.chkBestExecution.UseVisualStyleBackColor = true;
            // 
            // frmOrderBasket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSalmon;
            this.ClientSize = new System.Drawing.Size(1201, 695);
            this.Controls.Add(this.chkBestExecution);
            this.Controls.Add(this.cmbStockExchange);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lstType);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.lblAmount_EUR);
            this.Controls.Add(this.lblEUR);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.lblAmountCurr);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.dConstant);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.cmbConstant);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.lblServiceProvider);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.lblConstant);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.picCopy2Clipboard);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.lblProductTitle);
            this.Controls.Add(this.lblAction);
            this.Controls.Add(this.lblProductCode);
            this.Controls.Add(this.panExecutors);
            this.Controls.Add(this.lblProductISIN);
            this.Controls.Add(this.fgSimpleCommands);
            this.Controls.Add(this.fgSummary);
            this.Controls.Add(this.picExcel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmOrderBasket";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmOrderBasket";
            this.Load += new System.EventHandler(this.frmOrderBasket_Load);
            this.panExecutors.ResumeLayout(false);
            this.panExecutors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgSimpleCommands)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fgSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCopy2Clipboard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picExcel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnFinish;
        internal System.Windows.Forms.Label lblAmount_EUR;
        internal System.Windows.Forms.Label lblEUR;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label lblAmount;
        internal System.Windows.Forms.Label lblAmountCurr;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.DateTimePicker dConstant;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.ComboBox cmbConstant;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox txtPrice;
        internal System.Windows.Forms.Label lblServiceProvider;
        internal System.Windows.Forms.ComboBox cmbServiceProviders;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label lblConstant;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label lblQuantity;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.PictureBox picCopy2Clipboard;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label lblProductTitle;
        internal System.Windows.Forms.Label lblAction;
        internal System.Windows.Forms.Label lblProductCode;
        internal System.Windows.Forms.Panel panExecutors;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label lblProductISIN;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgSimpleCommands;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgSummary;
        internal System.Windows.Forms.PictureBox picExcel;
        internal System.Windows.Forms.Label label10;
        internal System.Windows.Forms.ComboBox lstType;
        internal System.Windows.Forms.ComboBox cmbStockExchange;
        internal System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkBestExecution;
    }
}