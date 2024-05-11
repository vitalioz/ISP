namespace Transactions
{
    partial class frmDPMBuffer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDPMBuffer));
            this.Label10 = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.lstType = new System.Windows.Forms.ComboBox();
            this.Label40 = new System.Windows.Forms.Label();
            this.txtPriceUp = new System.Windows.Forms.TextBox();
            this.lblCurr = new System.Windows.Forms.Label();
            this.txtPriceDown = new System.Windows.Forms.TextBox();
            this.Label39 = new System.Windows.Forms.Label();
            this.lblPre_Price = new System.Windows.Forms.Label();
            this.lblPortfolio = new System.Windows.Forms.Label();
            this.lblPre_Quantity = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.Label46 = new System.Windows.Forms.Label();
            this.Label47 = new System.Windows.Forms.Label();
            this.lblAction = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.Label51 = new System.Windows.Forms.Label();
            this.lblISIN = new System.Windows.Forms.Label();
            this.Label54 = new System.Windows.Forms.Label();
            this.lblReuters = new System.Windows.Forms.Label();
            this.Label57 = new System.Windows.Forms.Label();
            this.Label58 = new System.Windows.Forms.Label();
            this.lblTel = new System.Windows.Forms.Label();
            this.Label38 = new System.Windows.Forms.Label();
            this.lblMobile = new System.Windows.Forms.Label();
            this.Label44 = new System.Windows.Forms.Label();
            this.txtRecieveVoicePath = new System.Windows.Forms.TextBox();
            this.Label41 = new System.Windows.Forms.Label();
            this.Label42 = new System.Windows.Forms.Label();
            this.lblPre_Amount = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtRTONotes = new System.Windows.Forms.TextBox();
            this.Label14 = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.cmbConstant = new System.Windows.Forms.ComboBox();
            this.dConstant = new System.Windows.Forms.DateTimePicker();
            this.lblII_ID = new System.Windows.Forms.Label();
            this.btnAgree = new System.Windows.Forms.Button();
            this.btnNotAgree = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.cmbRecieveMethod3 = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.panPre_Data = new System.Windows.Forms.Panel();
            this.lblContractTitle = new System.Windows.Forms.Label();
            this.picPre_PriceUp = new System.Windows.Forms.PictureBox();
            this.picPre_PriceDown = new System.Windows.Forms.PictureBox();
            this.picPlayRecieveVoice = new System.Windows.Forms.PictureBox();
            this.picRecieveVoicePath = new System.Windows.Forms.PictureBox();
            this.chkList = new System.Windows.Forms.CheckBox();
            this.btnClear_Filter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.GroupBox1.SuspendLayout();
            this.panPre_Data.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPre_PriceUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPre_PriceDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlayRecieveVoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRecieveVoicePath)).BeginInit();
            this.SuspendLayout();
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(10, 13);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(29, 13);
            this.Label10.TabIndex = 1034;
            this.Label10.Text = "Filter";
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(48, 11);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(172, 20);
            this.txtFilter.TabIndex = 1033;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // fgList
            // 
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(8, 42);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(1401, 378);
            this.fgList.TabIndex = 1035;
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(179, 132);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(56, 20);
            this.txtPrice.TabIndex = 504;
            this.txtPrice.LostFocus += new System.EventHandler(this.txtPrice_LostFocus);
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(501, 132);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(65, 20);
            this.txtQuantity.TabIndex = 510;
            this.txtQuantity.LostFocus += new System.EventHandler(this.txtQuantity_LostFocus);
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
            this.lstType.Location = new System.Drawing.Point(60, 131);
            this.lstType.Name = "lstType";
            this.lstType.Size = new System.Drawing.Size(82, 21);
            this.lstType.TabIndex = 502;
            // 
            // Label40
            // 
            this.Label40.AutoSize = true;
            this.Label40.Location = new System.Drawing.Point(5, 134);
            this.Label40.Name = "Label40";
            this.Label40.Size = new System.Drawing.Size(37, 13);
            this.Label40.TabIndex = 541;
            this.Label40.Text = "Τύπος";
            // 
            // txtPriceUp
            // 
            this.txtPriceUp.Location = new System.Drawing.Point(286, 132);
            this.txtPriceUp.Name = "txtPriceUp";
            this.txtPriceUp.Size = new System.Drawing.Size(45, 20);
            this.txtPriceUp.TabIndex = 506;
            // 
            // lblCurr
            // 
            this.lblCurr.Location = new System.Drawing.Point(236, 136);
            this.lblCurr.Name = "lblCurr";
            this.lblCurr.Size = new System.Drawing.Size(30, 13);
            this.lblCurr.TabIndex = 540;
            this.lblCurr.Text = "USD";
            // 
            // txtPriceDown
            // 
            this.txtPriceDown.Location = new System.Drawing.Point(355, 132);
            this.txtPriceDown.Name = "txtPriceDown";
            this.txtPriceDown.Size = new System.Drawing.Size(45, 20);
            this.txtPriceDown.TabIndex = 508;
            // 
            // Label39
            // 
            this.Label39.AutoSize = true;
            this.Label39.Location = new System.Drawing.Point(4, 109);
            this.Label39.Name = "Label39";
            this.Label39.Size = new System.Drawing.Size(28, 13);
            this.Label39.TabIndex = 545;
            this.Label39.Text = "ISIN";
            // 
            // lblPre_Price
            // 
            this.lblPre_Price.AutoSize = true;
            this.lblPre_Price.Location = new System.Drawing.Point(148, 135);
            this.lblPre_Price.Name = "lblPre_Price";
            this.lblPre_Price.Size = new System.Drawing.Size(28, 13);
            this.lblPre_Price.TabIndex = 536;
            this.lblPre_Price.Text = "Τιμή";
            // 
            // lblPortfolio
            // 
            this.lblPortfolio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPortfolio.Location = new System.Drawing.Point(497, 28);
            this.lblPortfolio.Name = "lblPortfolio";
            this.lblPortfolio.Size = new System.Drawing.Size(236, 20);
            this.lblPortfolio.TabIndex = 529;
            // 
            // lblPre_Quantity
            // 
            this.lblPre_Quantity.AutoSize = true;
            this.lblPre_Quantity.Location = new System.Drawing.Point(437, 136);
            this.lblPre_Quantity.Name = "lblPre_Quantity";
            this.lblPre_Quantity.Size = new System.Drawing.Size(58, 13);
            this.lblPre_Quantity.TabIndex = 526;
            this.lblPre_Quantity.Text = "Ποσότητα";
            this.lblPre_Quantity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCode
            // 
            this.lblCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCode.Location = new System.Drawing.Point(62, 28);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(239, 20);
            this.lblCode.TabIndex = 547;
            // 
            // Label46
            // 
            this.Label46.AutoSize = true;
            this.Label46.Location = new System.Drawing.Point(6, 57);
            this.Label46.Name = "Label46";
            this.Label46.Size = new System.Drawing.Size(38, 13);
            this.Label46.TabIndex = 524;
            this.Label46.Text = "Πράξη";
            // 
            // Label47
            // 
            this.Label47.AutoSize = true;
            this.Label47.Location = new System.Drawing.Point(4, 8);
            this.Label47.Name = "Label47";
            this.Label47.Size = new System.Drawing.Size(52, 13);
            this.Label47.TabIndex = 522;
            this.Label47.Text = "Σύμβαση";
            // 
            // lblAction
            // 
            this.lblAction.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAction.Location = new System.Drawing.Point(62, 53);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(56, 20);
            this.lblAction.TabIndex = 549;
            // 
            // lblTitle
            // 
            this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTitle.Location = new System.Drawing.Point(61, 78);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(671, 20);
            this.lblTitle.TabIndex = 554;
            // 
            // Label51
            // 
            this.Label51.AutoSize = true;
            this.Label51.Location = new System.Drawing.Point(7, 81);
            this.Label51.Name = "Label51";
            this.Label51.Size = new System.Drawing.Size(42, 13);
            this.Label51.TabIndex = 555;
            this.Label51.Text = "Προϊόν";
            // 
            // lblISIN
            // 
            this.lblISIN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblISIN.Location = new System.Drawing.Point(60, 105);
            this.lblISIN.Name = "lblISIN";
            this.lblISIN.Size = new System.Drawing.Size(240, 20);
            this.lblISIN.TabIndex = 556;
            // 
            // Label54
            // 
            this.Label54.AutoSize = true;
            this.Label54.Location = new System.Drawing.Point(413, 106);
            this.Label54.Name = "Label54";
            this.Label54.Size = new System.Drawing.Size(87, 13);
            this.Label54.TabIndex = 557;
            this.Label54.Text = "Reuters Κωδικός";
            // 
            // lblReuters
            // 
            this.lblReuters.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblReuters.Location = new System.Drawing.Point(501, 102);
            this.lblReuters.Name = "lblReuters";
            this.lblReuters.Size = new System.Drawing.Size(231, 20);
            this.lblReuters.TabIndex = 558;
            // 
            // Label57
            // 
            this.Label57.AutoSize = true;
            this.Label57.Location = new System.Drawing.Point(5, 32);
            this.Label57.Name = "Label57";
            this.Label57.Size = new System.Drawing.Size(47, 13);
            this.Label57.TabIndex = 561;
            this.Label57.Text = "Κωδικός";
            // 
            // Label58
            // 
            this.Label58.AutoSize = true;
            this.Label58.Location = new System.Drawing.Point(446, 31);
            this.Label58.Name = "Label58";
            this.Label58.Size = new System.Drawing.Size(45, 13);
            this.Label58.TabIndex = 562;
            this.Label58.Text = "Portfolio";
            this.Label58.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTel
            // 
            this.lblTel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTel.Location = new System.Drawing.Point(852, 18);
            this.lblTel.Name = "lblTel";
            this.lblTel.Size = new System.Drawing.Size(112, 20);
            this.lblTel.TabIndex = 564;
            // 
            // Label38
            // 
            this.Label38.AutoSize = true;
            this.Label38.Location = new System.Drawing.Point(771, 22);
            this.Label38.Name = "Label38";
            this.Label38.Size = new System.Drawing.Size(58, 13);
            this.Label38.TabIndex = 565;
            this.Label38.Text = "Τηλέφωνο";
            // 
            // lblMobile
            // 
            this.lblMobile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMobile.Location = new System.Drawing.Point(1048, 15);
            this.lblMobile.Name = "lblMobile";
            this.lblMobile.Size = new System.Drawing.Size(112, 20);
            this.lblMobile.TabIndex = 566;
            // 
            // Label44
            // 
            this.Label44.AutoSize = true;
            this.Label44.Location = new System.Drawing.Point(1004, 19);
            this.Label44.Name = "Label44";
            this.Label44.Size = new System.Drawing.Size(38, 13);
            this.Label44.TabIndex = 567;
            this.Label44.Text = "Mobile";
            // 
            // txtRecieveVoicePath
            // 
            this.txtRecieveVoicePath.Location = new System.Drawing.Point(852, 79);
            this.txtRecieveVoicePath.Name = "txtRecieveVoicePath";
            this.txtRecieveVoicePath.Size = new System.Drawing.Size(249, 20);
            this.txtRecieveVoicePath.TabIndex = 516;
            // 
            // Label41
            // 
            this.Label41.AutoSize = true;
            this.Label41.Location = new System.Drawing.Point(771, 83);
            this.Label41.Name = "Label41";
            this.Label41.Size = new System.Drawing.Size(41, 13);
            this.Label41.TabIndex = 571;
            this.Label41.Text = "Αρχείο";
            // 
            // Label42
            // 
            this.Label42.AutoSize = true;
            this.Label42.Location = new System.Drawing.Point(771, 106);
            this.Label42.Name = "Label42";
            this.Label42.Size = new System.Drawing.Size(71, 13);
            this.Label42.TabIndex = 574;
            this.Label42.Text = "Παρατήρηση";
            // 
            // lblPre_Amount
            // 
            this.lblPre_Amount.Location = new System.Drawing.Point(572, 133);
            this.lblPre_Amount.Name = "lblPre_Amount";
            this.lblPre_Amount.Size = new System.Drawing.Size(92, 18);
            this.lblPre_Amount.TabIndex = 575;
            this.lblPre_Amount.Text = "Ποσό επενδυσης";
            this.lblPre_Amount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(665, 133);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(67, 20);
            this.txtAmount.TabIndex = 512;
            // 
            // txtRTONotes
            // 
            this.txtRTONotes.Location = new System.Drawing.Point(852, 139);
            this.txtRTONotes.Multiline = true;
            this.txtRTONotes.Name = "txtRTONotes";
            this.txtRTONotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRTONotes.Size = new System.Drawing.Size(308, 33);
            this.txtRTONotes.TabIndex = 518;
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(771, 141);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(61, 13);
            this.Label14.TabIndex = 577;
            this.Label14.Text = "RTO Notes";
            // 
            // lblProduct
            // 
            this.lblProduct.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProduct.Location = new System.Drawing.Point(134, 53);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(340, 20);
            this.lblProduct.TabIndex = 578;
            // 
            // Label15
            // 
            this.Label15.Location = new System.Drawing.Point(510, 57);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(51, 13);
            this.Label15.TabIndex = 579;
            this.Label15.Text = "Διάρκεια";
            // 
            // cmbConstant
            // 
            this.cmbConstant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConstant.FormattingEnabled = true;
            this.cmbConstant.Items.AddRange(new object[] {
            "Day Order",
            "GTC",
            "GTDate"});
            this.cmbConstant.Location = new System.Drawing.Point(563, 52);
            this.cmbConstant.Name = "cmbConstant";
            this.cmbConstant.Size = new System.Drawing.Size(77, 21);
            this.cmbConstant.TabIndex = 500;
            // 
            // dConstant
            // 
            this.dConstant.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dConstant.Location = new System.Drawing.Point(646, 52);
            this.dConstant.Name = "dConstant";
            this.dConstant.Size = new System.Drawing.Size(86, 20);
            this.dConstant.TabIndex = 501;
            this.dConstant.Visible = false;
            // 
            // lblII_ID
            // 
            this.lblII_ID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblII_ID.Location = new System.Drawing.Point(1340, 12);
            this.lblII_ID.Name = "lblII_ID";
            this.lblII_ID.Size = new System.Drawing.Size(20, 20);
            this.lblII_ID.TabIndex = 580;
            this.lblII_ID.Visible = false;
            // 
            // btnAgree
            // 
            this.btnAgree.BackColor = System.Drawing.Color.LightGreen;
            this.btnAgree.Location = new System.Drawing.Point(1251, 65);
            this.btnAgree.Name = "btnAgree";
            this.btnAgree.Size = new System.Drawing.Size(138, 32);
            this.btnAgree.TabIndex = 540;
            this.btnAgree.Text = "Αποδοχή εντολής";
            this.btnAgree.UseVisualStyleBackColor = false;
            this.btnAgree.Click += new System.EventHandler(this.btnAgree_Click);
            // 
            // btnNotAgree
            // 
            this.btnNotAgree.BackColor = System.Drawing.Color.LightCoral;
            this.btnNotAgree.Location = new System.Drawing.Point(1251, 108);
            this.btnNotAgree.Name = "btnNotAgree";
            this.btnNotAgree.Size = new System.Drawing.Size(138, 32);
            this.btnNotAgree.TabIndex = 544;
            this.btnNotAgree.Text = "Μην αποδοχή εντολής";
            this.btnNotAgree.UseVisualStyleBackColor = false;
            this.btnNotAgree.Click += new System.EventHandler(this.btnNotAgree_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.lblNotes);
            this.GroupBox1.Controls.Add(this.cmbRecieveMethod3);
            this.GroupBox1.Controls.Add(this.label19);
            this.GroupBox1.Controls.Add(this.panPre_Data);
            this.GroupBox1.Controls.Add(this.btnNotAgree);
            this.GroupBox1.Controls.Add(this.btnAgree);
            this.GroupBox1.Controls.Add(this.lblII_ID);
            this.GroupBox1.Controls.Add(this.Label14);
            this.GroupBox1.Controls.Add(this.txtRTONotes);
            this.GroupBox1.Controls.Add(this.Label42);
            this.GroupBox1.Controls.Add(this.Label41);
            this.GroupBox1.Controls.Add(this.picPlayRecieveVoice);
            this.GroupBox1.Controls.Add(this.picRecieveVoicePath);
            this.GroupBox1.Controls.Add(this.txtRecieveVoicePath);
            this.GroupBox1.Controls.Add(this.Label44);
            this.GroupBox1.Controls.Add(this.lblMobile);
            this.GroupBox1.Controls.Add(this.Label38);
            this.GroupBox1.Controls.Add(this.lblTel);
            this.GroupBox1.Location = new System.Drawing.Point(7, 426);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(1402, 180);
            this.GroupBox1.TabIndex = 1032;
            this.GroupBox1.TabStop = false;
            // 
            // lblNotes
            // 
            this.lblNotes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNotes.Location = new System.Drawing.Point(852, 106);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(308, 30);
            this.lblNotes.TabIndex = 1146;
            // 
            // cmbRecieveMethod3
            // 
            this.cmbRecieveMethod3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRecieveMethod3.FormattingEnabled = true;
            this.cmbRecieveMethod3.Location = new System.Drawing.Point(852, 54);
            this.cmbRecieveMethod3.Name = "cmbRecieveMethod3";
            this.cmbRecieveMethod3.Size = new System.Drawing.Size(249, 21);
            this.cmbRecieveMethod3.TabIndex = 514;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(771, 59);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(79, 13);
            this.label19.TabIndex = 1145;
            this.label19.Text = "Τρόπος Λήψης";
            // 
            // panPre_Data
            // 
            this.panPre_Data.Controls.Add(this.lblContractTitle);
            this.panPre_Data.Controls.Add(this.Label47);
            this.panPre_Data.Controls.Add(this.txtPrice);
            this.panPre_Data.Controls.Add(this.txtQuantity);
            this.panPre_Data.Controls.Add(this.lstType);
            this.panPre_Data.Controls.Add(this.dConstant);
            this.panPre_Data.Controls.Add(this.Label40);
            this.panPre_Data.Controls.Add(this.cmbConstant);
            this.panPre_Data.Controls.Add(this.txtPriceUp);
            this.panPre_Data.Controls.Add(this.Label15);
            this.panPre_Data.Controls.Add(this.lblCurr);
            this.panPre_Data.Controls.Add(this.lblProduct);
            this.panPre_Data.Controls.Add(this.txtPriceDown);
            this.panPre_Data.Controls.Add(this.picPre_PriceUp);
            this.panPre_Data.Controls.Add(this.picPre_PriceDown);
            this.panPre_Data.Controls.Add(this.txtAmount);
            this.panPre_Data.Controls.Add(this.Label39);
            this.panPre_Data.Controls.Add(this.lblPre_Amount);
            this.panPre_Data.Controls.Add(this.lblPre_Price);
            this.panPre_Data.Controls.Add(this.lblPortfolio);
            this.panPre_Data.Controls.Add(this.lblPre_Quantity);
            this.panPre_Data.Controls.Add(this.lblCode);
            this.panPre_Data.Controls.Add(this.Label46);
            this.panPre_Data.Controls.Add(this.lblAction);
            this.panPre_Data.Controls.Add(this.lblTitle);
            this.panPre_Data.Controls.Add(this.Label51);
            this.panPre_Data.Controls.Add(this.lblISIN);
            this.panPre_Data.Controls.Add(this.Label54);
            this.panPre_Data.Controls.Add(this.lblReuters);
            this.panPre_Data.Controls.Add(this.Label58);
            this.panPre_Data.Controls.Add(this.Label57);
            this.panPre_Data.Location = new System.Drawing.Point(6, 16);
            this.panPre_Data.Name = "panPre_Data";
            this.panPre_Data.Size = new System.Drawing.Size(736, 156);
            this.panPre_Data.TabIndex = 1040;
            // 
            // lblContractTitle
            // 
            this.lblContractTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblContractTitle.Location = new System.Drawing.Point(62, 3);
            this.lblContractTitle.Name = "lblContractTitle";
            this.lblContractTitle.Size = new System.Drawing.Size(670, 20);
            this.lblContractTitle.TabIndex = 581;
            // 
            // picPre_PriceUp
            // 
            this.picPre_PriceUp.Image = global::Transactions.Properties.Resources.price_up1;
            this.picPre_PriceUp.Location = new System.Drawing.Point(273, 133);
            this.picPre_PriceUp.Name = "picPre_PriceUp";
            this.picPre_PriceUp.Size = new System.Drawing.Size(13, 19);
            this.picPre_PriceUp.TabIndex = 542;
            this.picPre_PriceUp.TabStop = false;
            // 
            // picPre_PriceDown
            // 
            this.picPre_PriceDown.Image = global::Transactions.Properties.Resources.price_down;
            this.picPre_PriceDown.Location = new System.Drawing.Point(342, 133);
            this.picPre_PriceDown.Name = "picPre_PriceDown";
            this.picPre_PriceDown.Size = new System.Drawing.Size(13, 17);
            this.picPre_PriceDown.TabIndex = 543;
            this.picPre_PriceDown.TabStop = false;
            // 
            // picPlayRecieveVoice
            // 
            this.picPlayRecieveVoice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picPlayRecieveVoice.Image = global::Transactions.Properties.Resources.eye;
            this.picPlayRecieveVoice.Location = new System.Drawing.Point(1133, 81);
            this.picPlayRecieveVoice.Name = "picPlayRecieveVoice";
            this.picPlayRecieveVoice.Size = new System.Drawing.Size(20, 18);
            this.picPlayRecieveVoice.TabIndex = 570;
            this.picPlayRecieveVoice.TabStop = false;
            this.picPlayRecieveVoice.Click += new System.EventHandler(this.picPlayRecieveVoice_Click);
            // 
            // picRecieveVoicePath
            // 
            this.picRecieveVoicePath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picRecieveVoicePath.Image = global::Transactions.Properties.Resources.FindFolder;
            this.picRecieveVoicePath.Location = new System.Drawing.Point(1107, 77);
            this.picRecieveVoicePath.Name = "picRecieveVoicePath";
            this.picRecieveVoicePath.Size = new System.Drawing.Size(25, 23);
            this.picRecieveVoicePath.TabIndex = 569;
            this.picRecieveVoicePath.TabStop = false;
            this.picRecieveVoicePath.Click += new System.EventHandler(this.picRecieveVoicePath_Click);
            // 
            // chkList
            // 
            this.chkList.AutoSize = true;
            this.chkList.Location = new System.Drawing.Point(14, 45);
            this.chkList.Name = "chkList";
            this.chkList.Size = new System.Drawing.Size(15, 14);
            this.chkList.TabIndex = 1039;
            this.chkList.UseVisualStyleBackColor = true;
            this.chkList.CheckedChanged += new System.EventHandler(this.chkList_CheckedChanged);
            // 
            // btnClear_Filter
            // 
            this.btnClear_Filter.Image = global::Transactions.Properties.Resources.cleanup;
            this.btnClear_Filter.Location = new System.Drawing.Point(226, 9);
            this.btnClear_Filter.Name = "btnClear_Filter";
            this.btnClear_Filter.Size = new System.Drawing.Size(26, 23);
            this.btnClear_Filter.TabIndex = 1038;
            this.btnClear_Filter.TabStop = false;
            this.btnClear_Filter.UseVisualStyleBackColor = true;
            this.btnClear_Filter.Click += new System.EventHandler(this.btnClear_Filter_Click);
            // 
            // frmDPMBuffer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(1416, 611);
            this.Controls.Add(this.chkList);
            this.Controls.Add(this.fgList);
            this.Controls.Add(this.btnClear_Filter);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.GroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmDPMBuffer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDPMBuffer";
            this.Load += new System.EventHandler(this.frmDPMBuffer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.panPre_Data.ResumeLayout(false);
            this.panPre_Data.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPre_PriceUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPre_PriceDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlayRecieveVoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRecieveVoicePath)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.TextBox txtFilter;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
        internal System.Windows.Forms.TextBox txtPrice;
        internal System.Windows.Forms.TextBox txtQuantity;
        internal System.Windows.Forms.ComboBox lstType;
        internal System.Windows.Forms.Label Label40;
        internal System.Windows.Forms.TextBox txtPriceUp;
        internal System.Windows.Forms.Label lblCurr;
        internal System.Windows.Forms.TextBox txtPriceDown;
        internal System.Windows.Forms.PictureBox picPre_PriceUp;
        internal System.Windows.Forms.PictureBox picPre_PriceDown;
        internal System.Windows.Forms.Label Label39;
        internal System.Windows.Forms.Label lblPre_Price;
        internal System.Windows.Forms.Label lblPortfolio;
        internal System.Windows.Forms.Label lblPre_Quantity;
        internal System.Windows.Forms.Label lblCode;
        internal System.Windows.Forms.Label Label46;
        internal System.Windows.Forms.Label Label47;
        internal System.Windows.Forms.Label lblAction;
        internal System.Windows.Forms.Label lblTitle;
        internal System.Windows.Forms.Label Label51;
        internal System.Windows.Forms.Label lblISIN;
        internal System.Windows.Forms.Label Label54;
        internal System.Windows.Forms.Label lblReuters;
        internal System.Windows.Forms.Label Label57;
        internal System.Windows.Forms.Label Label58;
        internal System.Windows.Forms.Label lblTel;
        internal System.Windows.Forms.Label Label38;
        internal System.Windows.Forms.Label lblMobile;
        internal System.Windows.Forms.Label Label44;
        internal System.Windows.Forms.TextBox txtRecieveVoicePath;
        internal System.Windows.Forms.PictureBox picRecieveVoicePath;
        internal System.Windows.Forms.PictureBox picPlayRecieveVoice;
        internal System.Windows.Forms.Label Label41;
        internal System.Windows.Forms.Label Label42;
        internal System.Windows.Forms.Label lblPre_Amount;
        internal System.Windows.Forms.TextBox txtAmount;
        internal System.Windows.Forms.TextBox txtRTONotes;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.Label lblProduct;
        internal System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.ComboBox cmbConstant;
        internal System.Windows.Forms.DateTimePicker dConstant;
        internal System.Windows.Forms.Label lblII_ID;
        internal System.Windows.Forms.Button btnAgree;
        internal System.Windows.Forms.Button btnNotAgree;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.Label lblContractTitle;
        internal System.Windows.Forms.Button btnClear_Filter;
        internal System.Windows.Forms.CheckBox chkList;
        private System.Windows.Forms.Panel panPre_Data;
        internal System.Windows.Forms.ComboBox cmbRecieveMethod3;
        internal System.Windows.Forms.Label label19;
        internal System.Windows.Forms.Label lblNotes;
    }
}