
namespace Options
{
    partial class frmServiceProviderFees2
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.lblFeesPercent = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.cmbDistribMethods = new System.Windows.Forms.ComboBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.lblCompanyMeridio = new System.Windows.Forms.Label();
            this.txtProvider = new System.Windows.Forms.TextBox();
            this.txtAmountTo = new System.Windows.Forms.TextBox();
            this.txtAmountFrom = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.txtFees = new System.Windows.Forms.TextBox();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::Options.Properties.Resources.cancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(226, 218);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 28);
            this.btnCancel.TabIndex = 43;
            this.btnCancel.Text = "   Άκυρο";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::Options.Properties.Resources.save;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(68, 218);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 28);
            this.btnSave.TabIndex = 42;
            this.btnSave.Text = "   Αποθήκευση";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.lblFeesPercent);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Controls.Add(this.Label5);
            this.GroupBox1.Controls.Add(this.cmbDistribMethods);
            this.GroupBox1.Controls.Add(this.Label6);
            this.GroupBox1.Controls.Add(this.txtCompany);
            this.GroupBox1.Controls.Add(this.lblCompanyMeridio);
            this.GroupBox1.Controls.Add(this.txtProvider);
            this.GroupBox1.Controls.Add(this.txtAmountTo);
            this.GroupBox1.Controls.Add(this.txtAmountFrom);
            this.GroupBox1.Controls.Add(this.Label3);
            this.GroupBox1.Controls.Add(this.Label4);
            this.GroupBox1.Controls.Add(this.txtFees);
            this.GroupBox1.Location = new System.Drawing.Point(7, 3);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(377, 198);
            this.GroupBox1.TabIndex = 44;
            this.GroupBox1.TabStop = false;
            // 
            // lblFeesPercent
            // 
            this.lblFeesPercent.Location = new System.Drawing.Point(168, 60);
            this.lblFeesPercent.Name = "lblFeesPercent";
            this.lblFeesPercent.Size = new System.Drawing.Size(15, 19);
            this.lblFeesPercent.TabIndex = 182;
            this.lblFeesPercent.Text = "%";
            this.lblFeesPercent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(18, 65);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(41, 13);
            this.Label1.TabIndex = 177;
            this.Label1.Text = "Αμοιβή";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label5
            // 
            this.Label5.Location = new System.Drawing.Point(18, 129);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(113, 20);
            this.Label5.TabIndex = 158;
            this.Label5.Text = "Μερίδιο του παρόχου";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbDistribMethods
            // 
            this.cmbDistribMethods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDistribMethods.FormattingEnabled = true;
            this.cmbDistribMethods.Items.AddRange(new object[] {
            "-",
            "Σταθερής Χρέωσης",
            "Ποσοστέα Επιστροφή"});
            this.cmbDistribMethods.Location = new System.Drawing.Point(139, 105);
            this.cmbDistribMethods.Name = "cmbDistribMethods";
            this.cmbDistribMethods.Size = new System.Drawing.Size(170, 21);
            this.cmbDistribMethods.TabIndex = 20;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(18, 110);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(106, 13);
            this.Label6.TabIndex = 170;
            this.Label6.Text = "Τρόπος Επιστροφής";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCompany
            // 
            this.txtCompany.Location = new System.Drawing.Point(139, 156);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(82, 20);
            this.txtCompany.TabIndex = 24;
            // 
            // lblCompanyMeridio
            // 
            this.lblCompanyMeridio.Location = new System.Drawing.Point(14, 157);
            this.lblCompanyMeridio.Name = "lblCompanyMeridio";
            this.lblCompanyMeridio.Size = new System.Drawing.Size(119, 33);
            this.lblCompanyMeridio.TabIndex = 172;
            this.lblCompanyMeridio.Text = "Μερίδιο της HellasFin";
            this.lblCompanyMeridio.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtProvider
            // 
            this.txtProvider.Location = new System.Drawing.Point(139, 130);
            this.txtProvider.Name = "txtProvider";
            this.txtProvider.Size = new System.Drawing.Size(82, 20);
            this.txtProvider.TabIndex = 22;
            // 
            // txtAmountTo
            // 
            this.txtAmountTo.Location = new System.Drawing.Point(202, 36);
            this.txtAmountTo.Name = "txtAmountTo";
            this.txtAmountTo.Size = new System.Drawing.Size(100, 20);
            this.txtAmountTo.TabIndex = 6;
            // 
            // txtAmountFrom
            // 
            this.txtAmountFrom.Location = new System.Drawing.Point(79, 34);
            this.txtAmountFrom.Name = "txtAmountFrom";
            this.txtAmountFrom.Size = new System.Drawing.Size(83, 20);
            this.txtAmountFrom.TabIndex = 4;
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(168, 36);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(28, 19);
            this.Label3.TabIndex = 176;
            this.Label3.Text = "εώς";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label4
            // 
            this.Label4.Location = new System.Drawing.Point(17, 33);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(62, 19);
            this.Label4.TabIndex = 175;
            this.Label4.Text = "Ποσό από";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFees
            // 
            this.txtFees.Location = new System.Drawing.Point(79, 59);
            this.txtFees.Name = "txtFees";
            this.txtFees.Size = new System.Drawing.Size(82, 20);
            this.txtFees.TabIndex = 8;
            // 
            // frmServiceProviderFees2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Wheat;
            this.ClientSize = new System.Drawing.Size(390, 261);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.GroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmServiceProviderFees2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Προμήθειες";
            this.Load += new System.EventHandler(this.frmServiceProviderFees2_Load);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.Label lblFeesPercent;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.ComboBox cmbDistribMethods;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.TextBox txtCompany;
        internal System.Windows.Forms.Label lblCompanyMeridio;
        internal System.Windows.Forms.TextBox txtProvider;
        internal System.Windows.Forms.TextBox txtAmountTo;
        internal System.Windows.Forms.TextBox txtAmountFrom;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.TextBox txtFees;
    }
}