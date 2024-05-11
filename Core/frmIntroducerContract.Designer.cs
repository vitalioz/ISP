namespace Core
{
    partial class frmIntroducerContract
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
            this.btnCancel_Member = new System.Windows.Forms.Button();
            this.btnOK_Member = new System.Windows.Forms.Button();
            this.Label11 = new System.Windows.Forms.Label();
            this.ucDoubleCalendar1 = new Core.ucDoubleCalendar();
            this.ucContractsSearch1 = new Core.ucContractsSearch();
            this.SuspendLayout();
            // 
            // btnCancel_Member
            // 
            this.btnCancel_Member.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel_Member.Location = new System.Drawing.Point(291, 284);
            this.btnCancel_Member.Name = "btnCancel_Member";
            this.btnCancel_Member.Size = new System.Drawing.Size(100, 26);
            this.btnCancel_Member.TabIndex = 453;
            this.btnCancel_Member.Text = "Άκυρο";
            this.btnCancel_Member.UseVisualStyleBackColor = true;
            // 
            // btnOK_Member
            // 
            this.btnOK_Member.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK_Member.Location = new System.Drawing.Point(154, 284);
            this.btnOK_Member.Name = "btnOK_Member";
            this.btnOK_Member.Size = new System.Drawing.Size(100, 26);
            this.btnOK_Member.TabIndex = 452;
            this.btnOK_Member.Text = "OK";
            this.btnOK_Member.UseVisualStyleBackColor = true;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(12, 59);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(89, 13);
            this.Label11.TabIndex = 454;
            this.Label11.Text = "Ονοματεπώνυμο";
            // 
            // ucDoubleCalendar1
            // 
            this.ucDoubleCalendar1.BackColor = System.Drawing.Color.Transparent;
            this.ucDoubleCalendar1.DateFrom = new System.DateTime(2020, 8, 30, 17, 44, 13, 9);
            this.ucDoubleCalendar1.DateTo = new System.DateTime(2020, 8, 30, 17, 44, 13, 9);
            this.ucDoubleCalendar1.Location = new System.Drawing.Point(15, 12);
            this.ucDoubleCalendar1.Name = "ucDoubleCalendar1";
            this.ucDoubleCalendar1.Size = new System.Drawing.Size(210, 22);
            this.ucDoubleCalendar1.TabIndex = 455;
            // 
            // ucContractsSearch1
            // 
            this.ucContractsSearch1.BackColor = System.Drawing.Color.Transparent;
            this.ucContractsSearch1.CodesList = null;
            this.ucContractsSearch1.Filters = "Client_ID > 0";
            this.ucContractsSearch1.ListType = 0;
            this.ucContractsSearch1.Location = new System.Drawing.Point(124, 59);
            this.ucContractsSearch1.Mode = 0;
            this.ucContractsSearch1.Name = "ucContractsSearch1";
            this.ucContractsSearch1.ShowClientsList = true;
            this.ucContractsSearch1.ShowHeight = 0;
            this.ucContractsSearch1.ShowWidth = 0;
            this.ucContractsSearch1.Size = new System.Drawing.Size(198, 17);
            this.ucContractsSearch1.TabIndex = 456;
            // 
            // frmIntroducerContract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 326);
            this.Controls.Add(this.ucContractsSearch1);
            this.Controls.Add(this.ucDoubleCalendar1);
            this.Controls.Add(this.btnCancel_Member);
            this.Controls.Add(this.btnOK_Member);
            this.Controls.Add(this.Label11);
            this.Name = "frmIntroducerContract";
            this.Text = "frmIntroducerContract";
            this.Load += new System.EventHandler(this.frmIntroducerContract_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnCancel_Member;
        internal System.Windows.Forms.Button btnOK_Member;
        internal System.Windows.Forms.Label Label11;
        private ucDoubleCalendar ucDoubleCalendar1;
        private ucContractsSearch ucContractsSearch1;
    }
}