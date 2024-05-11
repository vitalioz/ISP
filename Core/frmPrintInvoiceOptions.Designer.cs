namespace Core
{
    partial class frmPrintInvoiceOptions
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
            this.Label3 = new System.Windows.Forms.Label();
            this.dIssueDate = new System.Windows.Forms.DateTimePicker();
            this.Label11 = new System.Windows.Forms.Label();
            this.cmbPrinters = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.numCopies = new System.Windows.Forms.NumericUpDown();
            this.panIssueDate = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numCopies)).BeginInit();
            this.panIssueDate.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(1, 5);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(66, 13);
            this.Label3.TabIndex = 209;
            this.Label3.Text = "Ημερομηνία";
            // 
            // dIssueDate
            // 
            this.dIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dIssueDate.Location = new System.Drawing.Point(80, 1);
            this.dIssueDate.Name = "dIssueDate";
            this.dIssueDate.Size = new System.Drawing.Size(88, 20);
            this.dIssueDate.TabIndex = 6;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(23, 39);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(62, 13);
            this.Label11.TabIndex = 207;
            this.Label11.Text = "Εκτυπωτής";
            // 
            // cmbPrinters
            // 
            this.cmbPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrinters.FormattingEnabled = true;
            this.cmbPrinters.Location = new System.Drawing.Point(102, 36);
            this.cmbPrinters.Name = "cmbPrinters";
            this.cmbPrinters.Size = new System.Drawing.Size(340, 21);
            this.cmbPrinters.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(272, 140);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(79, 25);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Άκυρο";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(149, 140);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(79, 25);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(26, 70);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(63, 13);
            this.Label2.TabIndex = 203;
            this.Label2.Text = "Αντίγραφα";
            // 
            // numCopies
            // 
            this.numCopies.Location = new System.Drawing.Point(102, 65);
            this.numCopies.Name = "numCopies";
            this.numCopies.Size = new System.Drawing.Size(43, 20);
            this.numCopies.TabIndex = 4;
            // 
            // panIssueDate
            // 
            this.panIssueDate.Controls.Add(this.dIssueDate);
            this.panIssueDate.Controls.Add(this.Label3);
            this.panIssueDate.Location = new System.Drawing.Point(22, 90);
            this.panIssueDate.Name = "panIssueDate";
            this.panIssueDate.Size = new System.Drawing.Size(172, 23);
            this.panIssueDate.TabIndex = 210;
            // 
            // frmPrintInvoiceOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.ClientSize = new System.Drawing.Size(470, 184);
            this.Controls.Add(this.panIssueDate);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.cmbPrinters);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.numCopies);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmPrintInvoiceOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Επιλογές εκτύπωσης";
            this.Load += new System.EventHandler(this.frmPrintInvoiceOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numCopies)).EndInit();
            this.panIssueDate.ResumeLayout(false);
            this.panIssueDate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.DateTimePicker dIssueDate;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.ComboBox cmbPrinters;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Button btnOK;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.NumericUpDown numCopies;
        private System.Windows.Forms.Panel panIssueDate;
    }
}