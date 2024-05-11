namespace Transactions
{
    partial class frmFXBasket
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFXBasket));
            this.panDetails = new System.Windows.Forms.Panel();
            this.dConstant = new System.Windows.Forms.DateTimePicker();
            this.cmbConstant = new System.Windows.Forms.ComboBox();
            this.lblConstant = new System.Windows.Forms.Label();
            this.cmbCurrFrom = new System.Windows.Forms.ComboBox();
            this.cmbCashAccTo = new System.Windows.Forms.ComboBox();
            this.panExecutors = new System.Windows.Forms.Panel();
            this.cmbServiceProviders = new System.Windows.Forms.ComboBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.cmbCashAccFrom = new System.Windows.Forms.ComboBox();
            this.txtAmountFrom = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.txtAmountTo = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.cmbCurrTo = new System.Windows.Forms.ComboBox();
            this.fgSimpleCommands = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.fgSummary = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnFinish = new System.Windows.Forms.Button();
            this.panDetails.SuspendLayout();
            this.panExecutors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fgSimpleCommands)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fgSummary)).BeginInit();
            this.SuspendLayout();
            // 
            // panDetails
            // 
            this.panDetails.Controls.Add(this.dConstant);
            this.panDetails.Controls.Add(this.cmbConstant);
            this.panDetails.Controls.Add(this.lblConstant);
            this.panDetails.Controls.Add(this.cmbCurrFrom);
            this.panDetails.Controls.Add(this.cmbCashAccTo);
            this.panDetails.Controls.Add(this.panExecutors);
            this.panDetails.Controls.Add(this.cmbCashAccFrom);
            this.panDetails.Controls.Add(this.txtAmountFrom);
            this.panDetails.Controls.Add(this.pictureBox2);
            this.panDetails.Controls.Add(this.Label4);
            this.panDetails.Controls.Add(this.txtAmountTo);
            this.panDetails.Controls.Add(this.Label3);
            this.panDetails.Controls.Add(this.cmbCurrTo);
            this.panDetails.Location = new System.Drawing.Point(10, 496);
            this.panDetails.Name = "panDetails";
            this.panDetails.Size = new System.Drawing.Size(730, 102);
            this.panDetails.TabIndex = 1052;
            // 
            // dConstant
            // 
            this.dConstant.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dConstant.Location = new System.Drawing.Point(641, 79);
            this.dConstant.Name = "dConstant";
            this.dConstant.Size = new System.Drawing.Size(80, 20);
            this.dConstant.TabIndex = 1096;
            this.dConstant.Visible = false;
            // 
            // cmbConstant
            // 
            this.cmbConstant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConstant.FormattingEnabled = true;
            this.cmbConstant.Items.AddRange(new object[] {
            "Day Order",
            "GTC",
            "GTDate"});
            this.cmbConstant.Location = new System.Drawing.Point(558, 78);
            this.cmbConstant.Name = "cmbConstant";
            this.cmbConstant.Size = new System.Drawing.Size(81, 21);
            this.cmbConstant.TabIndex = 1095;
            this.cmbConstant.SelectedIndexChanged += new System.EventHandler(this.cmbConstant_SelectedIndexChanged);
            // 
            // lblConstant
            // 
            this.lblConstant.Location = new System.Drawing.Point(510, 80);
            this.lblConstant.Name = "lblConstant";
            this.lblConstant.Size = new System.Drawing.Size(51, 13);
            this.lblConstant.TabIndex = 1097;
            this.lblConstant.Text = "Διάρκεια";
            // 
            // cmbCurrFrom
            // 
            this.cmbCurrFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrFrom.FormattingEnabled = true;
            this.cmbCurrFrom.Location = new System.Drawing.Point(4, 52);
            this.cmbCurrFrom.Name = "cmbCurrFrom";
            this.cmbCurrFrom.Size = new System.Drawing.Size(63, 21);
            this.cmbCurrFrom.TabIndex = 1043;
            // 
            // cmbCashAccTo
            // 
            this.cmbCashAccTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCashAccTo.FormattingEnabled = true;
            this.cmbCashAccTo.Location = new System.Drawing.Point(438, 53);
            this.cmbCashAccTo.Name = "cmbCashAccTo";
            this.cmbCashAccTo.Size = new System.Drawing.Size(200, 21);
            this.cmbCashAccTo.TabIndex = 1047;
            // 
            // panExecutors
            // 
            this.panExecutors.Controls.Add(this.cmbServiceProviders);
            this.panExecutors.Controls.Add(this.Label8);
            this.panExecutors.Location = new System.Drawing.Point(1, 3);
            this.panExecutors.Name = "panExecutors";
            this.panExecutors.Size = new System.Drawing.Size(320, 24);
            this.panExecutors.TabIndex = 1040;
            // 
            // cmbServiceProviders
            // 
            this.cmbServiceProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServiceProviders.FormattingEnabled = true;
            this.cmbServiceProviders.Location = new System.Drawing.Point(61, 1);
            this.cmbServiceProviders.Name = "cmbServiceProviders";
            this.cmbServiceProviders.Size = new System.Drawing.Size(249, 21);
            this.cmbServiceProviders.TabIndex = 1035;
            this.cmbServiceProviders.SelectedValueChanged += new System.EventHandler(this.cmbServiceProviders_SelectedValueChanged);
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
            // cmbCashAccFrom
            // 
            this.cmbCashAccFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCashAccFrom.FormattingEnabled = true;
            this.cmbCashAccFrom.Location = new System.Drawing.Point(69, 52);
            this.cmbCashAccFrom.Name = "cmbCashAccFrom";
            this.cmbCashAccFrom.Size = new System.Drawing.Size(200, 21);
            this.cmbCashAccFrom.TabIndex = 1044;
            // 
            // txtAmountFrom
            // 
            this.txtAmountFrom.Location = new System.Drawing.Point(270, 53);
            this.txtAmountFrom.Name = "txtAmountFrom";
            this.txtAmountFrom.Size = new System.Drawing.Size(80, 20);
            this.txtAmountFrom.TabIndex = 1045;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(354, 54);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(17, 19);
            this.pictureBox2.TabIndex = 1051;
            this.pictureBox2.TabStop = false;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(150, 35);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(47, 13);
            this.Label4.TabIndex = 1049;
            this.Label4.Text = "Χρέωση";
            // 
            // txtAmountTo
            // 
            this.txtAmountTo.Location = new System.Drawing.Point(641, 54);
            this.txtAmountTo.Name = "txtAmountTo";
            this.txtAmountTo.Size = new System.Drawing.Size(80, 20);
            this.txtAmountTo.TabIndex = 1048;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(527, 35);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(51, 13);
            this.Label3.TabIndex = 1050;
            this.Label3.Text = "Πίστωση";
            // 
            // cmbCurrTo
            // 
            this.cmbCurrTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrTo.FormattingEnabled = true;
            this.cmbCurrTo.Location = new System.Drawing.Point(373, 53);
            this.cmbCurrTo.Name = "cmbCurrTo";
            this.cmbCurrTo.Size = new System.Drawing.Size(63, 21);
            this.cmbCurrTo.TabIndex = 1046;
            // 
            // fgSimpleCommands
            // 
            this.fgSimpleCommands.ColumnInfo = resources.GetString("fgSimpleCommands.ColumnInfo");
            this.fgSimpleCommands.Location = new System.Drawing.Point(12, 324);
            this.fgSimpleCommands.Name = "fgSimpleCommands";
            this.fgSimpleCommands.Rows.Count = 1;
            this.fgSimpleCommands.Rows.DefaultSize = 17;
            this.fgSimpleCommands.Size = new System.Drawing.Size(976, 161);
            this.fgSimpleCommands.TabIndex = 1042;
            // 
            // fgSummary
            // 
            this.fgSummary.ColumnInfo = resources.GetString("fgSummary.ColumnInfo");
            this.fgSummary.Location = new System.Drawing.Point(12, 30);
            this.fgSummary.Name = "fgSummary";
            this.fgSummary.Rows.Count = 2;
            this.fgSummary.Rows.DefaultSize = 17;
            this.fgSummary.Rows.Fixed = 2;
            this.fgSummary.Size = new System.Drawing.Size(976, 259);
            this.fgSummary.TabIndex = 1041;
            // 
            // btnFinish
            // 
            this.btnFinish.BackColor = System.Drawing.SystemColors.Control;
            this.btnFinish.Location = new System.Drawing.Point(846, 569);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(138, 28);
            this.btnFinish.TabIndex = 1016;
            this.btnFinish.Text = "Δημιουργία εντολής";
            this.btnFinish.UseVisualStyleBackColor = false;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // frmFXBasket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.BurlyWood;
            this.ClientSize = new System.Drawing.Size(996, 602);
            this.Controls.Add(this.panDetails);
            this.Controls.Add(this.fgSimpleCommands);
            this.Controls.Add(this.fgSummary);
            this.Controls.Add(this.btnFinish);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmFXBasket";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmFXBasket";
            this.Load += new System.EventHandler(this.frmFXBasket_Load);
            this.panDetails.ResumeLayout(false);
            this.panDetails.PerformLayout();
            this.panExecutors.ResumeLayout(false);
            this.panExecutors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fgSimpleCommands)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fgSummary)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.Panel panDetails;
        internal System.Windows.Forms.ComboBox cmbCurrFrom;
        internal System.Windows.Forms.ComboBox cmbCashAccTo;
        internal System.Windows.Forms.Panel panExecutors;
        internal System.Windows.Forms.ComboBox cmbServiceProviders;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.ComboBox cmbCashAccFrom;
        internal System.Windows.Forms.TextBox txtAmountFrom;
        internal System.Windows.Forms.PictureBox pictureBox2;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.TextBox txtAmountTo;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.ComboBox cmbCurrTo;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgSimpleCommands;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgSummary;
        internal System.Windows.Forms.Button btnFinish;
        internal System.Windows.Forms.DateTimePicker dConstant;
        internal System.Windows.Forms.ComboBox cmbConstant;
        internal System.Windows.Forms.Label lblConstant;
    }
}