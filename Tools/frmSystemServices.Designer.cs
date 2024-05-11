namespace Tools
{
    partial class frmSystemServices
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtCommandsID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRestordeCommandsRec = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSourceDB = new System.Windows.Forms.ComboBox();
            this.cmbTargetDB = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBackUp = new System.Windows.Forms.Button();
            this.btnCurrRates = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnRecievedDate = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.dAktionDate = new System.Windows.Forms.DateTimePicker();
            this.btnConvertSendDate = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.button5 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.Label35 = new System.Windows.Forms.Label();
            this.txtDocFilesPath = new System.Windows.Forms.TextBox();
            this.button9 = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.txtDMSFolder = new System.Windows.Forms.TextBox();
            this.btnExcel = new System.Windows.Forms.Button();
            this.picDocFilesPath = new System.Windows.Forms.PictureBox();
            this.button10 = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.button11 = new System.Windows.Forms.Button();
            this.ucDC = new Core.ucDoubleCalendar();
            this.button12 = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDocFilesPath)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Restore Commands Record";
            // 
            // txtCommandsID
            // 
            this.txtCommandsID.Location = new System.Drawing.Point(918, 22);
            this.txtCommandsID.Name = "txtCommandsID";
            this.txtCommandsID.Size = new System.Drawing.Size(100, 20);
            this.txtCommandsID.TabIndex = 1;
            this.txtCommandsID.Text = "363774";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(885, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "ID =";
            // 
            // btnRestordeCommandsRec
            // 
            this.btnRestordeCommandsRec.Location = new System.Drawing.Point(1069, 18);
            this.btnRestordeCommandsRec.Name = "btnRestordeCommandsRec";
            this.btnRestordeCommandsRec.Size = new System.Drawing.Size(75, 23);
            this.btnRestordeCommandsRec.TabIndex = 3;
            this.btnRestordeCommandsRec.Text = "Start";
            this.btnRestordeCommandsRec.UseVisualStyleBackColor = true;
            this.btnRestordeCommandsRec.Click += new System.EventHandler(this.btnRestordeCommandsRec_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(202, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Source DB";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(521, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Target DB";
            // 
            // cmbSourceDB
            // 
            this.cmbSourceDB.FormattingEnabled = true;
            this.cmbSourceDB.Items.AddRange(new object[] {
            "LocalDB",
            "10.0.0.15"});
            this.cmbSourceDB.Location = new System.Drawing.Point(267, 20);
            this.cmbSourceDB.Name = "cmbSourceDB";
            this.cmbSourceDB.Size = new System.Drawing.Size(226, 21);
            this.cmbSourceDB.TabIndex = 6;
            // 
            // cmbTargetDB
            // 
            this.cmbTargetDB.FormattingEnabled = true;
            this.cmbTargetDB.Items.AddRange(new object[] {
            "10.0.0.15",
            "LocalDB"});
            this.cmbTargetDB.Location = new System.Drawing.Point(583, 21);
            this.cmbTargetDB.Name = "cmbTargetDB";
            this.cmbTargetDB.Size = new System.Drawing.Size(226, 21);
            this.cmbTargetDB.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "BackUp";
            // 
            // btnBackUp
            // 
            this.btnBackUp.Location = new System.Drawing.Point(85, 74);
            this.btnBackUp.Name = "btnBackUp";
            this.btnBackUp.Size = new System.Drawing.Size(75, 23);
            this.btnBackUp.TabIndex = 9;
            this.btnBackUp.Text = "Start";
            this.btnBackUp.UseVisualStyleBackColor = true;
            this.btnBackUp.Click += new System.EventHandler(this.btnBackUp_Click);
            // 
            // btnCurrRates
            // 
            this.btnCurrRates.Location = new System.Drawing.Point(773, 74);
            this.btnCurrRates.Name = "btnCurrRates";
            this.btnCurrRates.Size = new System.Drawing.Size(75, 23);
            this.btnCurrRates.TabIndex = 11;
            this.btnCurrRates.Text = "Start";
            this.btnCurrRates.UseVisualStyleBackColor = true;
            this.btnCurrRates.Click += new System.EventHandler(this.btnCurrRates_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(634, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Insert Commands.CurrRate";
            // 
            // btnRecievedDate
            // 
            this.btnRecievedDate.Location = new System.Drawing.Point(868, 103);
            this.btnRecievedDate.Name = "btnRecievedDate";
            this.btnRecievedDate.Size = new System.Drawing.Size(75, 23);
            this.btnRecievedDate.TabIndex = 13;
            this.btnRecievedDate.Text = "Start";
            this.btnRecievedDate.UseVisualStyleBackColor = true;
            this.btnRecievedDate.Click += new System.EventHandler(this.btnRecievedDate_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(638, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Restore RecievedDate";
            // 
            // dAktionDate
            // 
            this.dAktionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dAktionDate.Location = new System.Drawing.Point(773, 104);
            this.dAktionDate.Name = "dAktionDate";
            this.dAktionDate.Size = new System.Drawing.Size(89, 20);
            this.dAktionDate.TabIndex = 14;
            // 
            // btnConvertSendDate
            // 
            this.btnConvertSendDate.Location = new System.Drawing.Point(799, 138);
            this.btnConvertSendDate.Name = "btnConvertSendDate";
            this.btnConvertSendDate.Size = new System.Drawing.Size(75, 23);
            this.btnConvertSendDate.TabIndex = 16;
            this.btnConvertSendDate.Text = "Start";
            this.btnConvertSendDate.UseVisualStyleBackColor = true;
            this.btnConvertSendDate.Click += new System.EventHandler(this.btnConvertSendDate_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(638, 143);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(155, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Convert SendDate to SentDate";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(799, 169);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(638, 174);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(122, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Restore LastClosePrices";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(799, 203);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 20;
            this.button2.Text = "Start";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(638, 208);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(129, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Recal CommandsFX Fees";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(184, 113);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 22;
            this.button3.Text = "Start";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(23, 118);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(147, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Copy Products Prices to Cash";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(888, 239);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 24;
            this.button4.Text = "Start";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(638, 249);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(108, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "Check Invoice PDFs ";
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(799, 241);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(75, 20);
            this.txtYear.TabIndex = 25;
            this.txtYear.Text = "2021";
            // 
            // fgList
            // 
            this.fgList.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Rows;
            this.fgList.ColumnInfo = "10,0,0,0,0,85,Columns:";
            this.fgList.Location = new System.Drawing.Point(152, 348);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 20;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(914, 245);
            this.fgList.TabIndex = 26;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(799, 280);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 28;
            this.button5.Text = "Start";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(638, 285);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(119, 13);
            this.label13.TabIndex = 27;
            this.label13.Text = "Check Client Docs Files";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(799, 319);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 30;
            this.button6.Text = "Start";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(638, 324);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(93, 13);
            this.label14.TabIndex = 29;
            this.label14.Text = "Recalc RTO Fees";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(403, 170);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 32;
            this.button7.Text = "Start";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(23, 180);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(158, 13);
            this.label15.TabIndex = 31;
            this.label15.Text = "Recalc Χρέωση της εταιρείας";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(978, 238);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(88, 23);
            this.button8.TabIndex = 34;
            this.button8.Text = "Invoice_RTO";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // Label35
            // 
            this.Label35.AutoSize = true;
            this.Label35.Location = new System.Drawing.Point(160, 602);
            this.Label35.Name = "Label35";
            this.Label35.Size = new System.Drawing.Size(99, 13);
            this.Label35.TabIndex = 182;
            this.Label35.Text = "Φάκελος με  PDFs";
            // 
            // txtDocFilesPath
            // 
            this.txtDocFilesPath.Location = new System.Drawing.Point(267, 599);
            this.txtDocFilesPath.Name = "txtDocFilesPath";
            this.txtDocFilesPath.Size = new System.Drawing.Size(419, 20);
            this.txtDocFilesPath.TabIndex = 181;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(740, 621);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(152, 23);
            this.button9.TabIndex = 184;
            this.button9.Text = "Upload Invoice PDFs";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(160, 627);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(63, 13);
            this.label16.TabIndex = 186;
            this.label16.Text = "DMS Folder";
            // 
            // txtDMSFolder
            // 
            this.txtDMSFolder.Location = new System.Drawing.Point(267, 624);
            this.txtDMSFolder.Name = "txtDMSFolder";
            this.txtDMSFolder.Size = new System.Drawing.Size(419, 20);
            this.txtDMSFolder.TabIndex = 185;
            this.txtDMSFolder.Text = "C:\\DMS";
            // 
            // btnExcel
            // 
            this.btnExcel.Image = global::Tools.Properties.Resources.excel;
            this.btnExcel.Location = new System.Drawing.Point(152, 324);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(26, 23);
            this.btnExcel.TabIndex = 1253;
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // picDocFilesPath
            // 
            this.picDocFilesPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDocFilesPath.Image = global::Tools.Properties.Resources.FindFolder;
            this.picDocFilesPath.Location = new System.Drawing.Point(692, 596);
            this.picDocFilesPath.Name = "picDocFilesPath";
            this.picDocFilesPath.Size = new System.Drawing.Size(24, 24);
            this.picDocFilesPath.TabIndex = 183;
            this.picDocFilesPath.TabStop = false;
            this.picDocFilesPath.Click += new System.EventHandler(this.picDocFilesPath_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(267, 244);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 1255;
            this.button10.Text = "Start";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(32, 253);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(223, 13);
            this.label17.TabIndex = 1254;
            this.label17.Text = "Restore Contracts_Details_Packages.DateTo";
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(12, 617);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(75, 23);
            this.button11.TabIndex = 1256;
            this.button11.Text = "Toolpep Test";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // ucDC
            // 
            this.ucDC.BackColor = System.Drawing.Color.Transparent;
            this.ucDC.DateFrom = new System.DateTime(2021, 2, 17, 12, 3, 28, 75);
            this.ucDC.DateTo = new System.DateTime(2021, 2, 17, 12, 3, 28, 75);
            this.ucDC.Location = new System.Drawing.Point(187, 171);
            this.ucDC.Name = "ucDC";
            this.ucDC.Size = new System.Drawing.Size(210, 22);
            this.ucDC.TabIndex = 33;
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(1069, 74);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(75, 23);
            this.button12.TabIndex = 1258;
            this.button12.Text = "Start";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(908, 79);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(158, 13);
            this.label18.TabIndex = 1257;
            this.label18.Text = "Restore Commands.SendCheck";
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(959, 622);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(152, 23);
            this.button13.TabIndex = 1259;
            this.button13.Text = "Find Invoice PDFs";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(403, 305);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(152, 23);
            this.button14.TabIndex = 1260;
            this.button14.Text = "commandsll";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // frmSystemServices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1168, 651);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtDMSFolder);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.picDocFilesPath);
            this.Controls.Add(this.Label35);
            this.Controls.Add(this.txtDocFilesPath);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.ucDC);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.fgList);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnConvertSendDate);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dAktionDate);
            this.Controls.Add(this.btnRecievedDate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnCurrRates);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnBackUp);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbTargetDB);
            this.Controls.Add(this.cmbSourceDB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRestordeCommandsRec);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCommandsID);
            this.Controls.Add(this.label1);
            this.Name = "frmSystemServices";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmSystemServices";
            this.Load += new System.EventHandler(this.frmSystemServices_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDocFilesPath)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCommandsID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRestordeCommandsRec;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSourceDB;
        private System.Windows.Forms.ComboBox cmbTargetDB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnBackUp;
        private System.Windows.Forms.Button btnCurrRates;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnRecievedDate;
        private System.Windows.Forms.Label label7;
        internal System.Windows.Forms.DateTimePicker dAktionDate;
        private System.Windows.Forms.Button btnConvertSendDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtYear;
        private C1.Win.C1FlexGrid.C1FlexGrid fgList;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label15;
        private Core.ucDoubleCalendar ucDC;
        private System.Windows.Forms.Button button8;
        public System.Windows.Forms.PictureBox picDocFilesPath;
        public System.Windows.Forms.Label Label35;
        public System.Windows.Forms.TextBox txtDocFilesPath;
        private System.Windows.Forms.Button button9;
        public System.Windows.Forms.Label label16;
        public System.Windows.Forms.TextBox txtDMSFolder;
        internal System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
    }
}