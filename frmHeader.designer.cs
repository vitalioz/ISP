namespace ISP
{
    partial class frmHeader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHeader));
            this.Label1 = new System.Windows.Forms.Label();
            this.lblToday = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnServices = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.btnEntry = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.Label1.Location = new System.Drawing.Point(313, 285);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(209, 16);
            this.Label1.TabIndex = 20;
            this.Label1.Text = "Copyright ® 2008-2022 KVANT Software";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblToday
            // 
            this.lblToday.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblToday.Location = new System.Drawing.Point(418, 153);
            this.lblToday.Name = "lblToday";
            this.lblToday.Size = new System.Drawing.Size(84, 20);
            this.lblToday.TabIndex = 19;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Verdana", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(76)))), ((int)(((byte)(109)))));
            this.lblTitle.Location = new System.Drawing.Point(343, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(148, 90);
            this.lblTitle.TabIndex = 18;
            this.lblTitle.Text = "Stock Trader";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnServices
            // 
            this.btnServices.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnServices.BackgroundImage")));
            this.btnServices.Location = new System.Drawing.Point(470, 224);
            this.btnServices.Name = "btnServices";
            this.btnServices.Size = new System.Drawing.Size(35, 35);
            this.btnServices.TabIndex = 17;
            // 
            // btnQuit
            // 
            this.btnQuit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuit.BackgroundImage")));
            this.btnQuit.Location = new System.Drawing.Point(408, 224);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(35, 35);
            this.btnQuit.TabIndex = 14;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnEntry
            // 
            this.btnEntry.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEntry.BackgroundImage")));
            this.btnEntry.Location = new System.Drawing.Point(341, 224);
            this.btnEntry.Name = "btnEntry";
            this.btnEntry.Size = new System.Drawing.Size(35, 35);
            this.btnEntry.TabIndex = 12;
            this.btnEntry.Click += new System.EventHandler(this.btnEntry_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(418, 177);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(84, 20);
            this.txtPassword.TabIndex = 11;
            this.txtPassword.Text = "TextBox1";
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckEnterKeyPress);
            // 
            // lblCode
            // 
            this.lblCode.Location = new System.Drawing.Point(347, 177);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(62, 16);
            this.lblCode.TabIndex = 16;
            this.lblCode.Text = "Password";
            this.lblCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDate
            // 
            this.lblDate.Location = new System.Drawing.Point(338, 153);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(72, 16);
            this.lblDate.TabIndex = 15;
            this.lblDate.Text = "Date";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblVersion
            // 
            this.lblVersion.Location = new System.Drawing.Point(324, 103);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(196, 17);
            this.lblVersion.TabIndex = 13;
            this.lblVersion.Text = "Version 2.0.1";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ISP.Properties.Resources.intro;
            this.pictureBox1.Location = new System.Drawing.Point(4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(302, 303);
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // frmHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(528, 309);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.lblToday);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnServices);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnEntry);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblVersion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmHeader";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmHeader";
            this.Load += new System.EventHandler(this.frmHeader_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label lblToday;
        internal System.Windows.Forms.Label lblTitle;
        internal System.Windows.Forms.Button btnServices;
        internal System.Windows.Forms.Button btnQuit;
        internal System.Windows.Forms.Button btnEntry;
        internal System.Windows.Forms.TextBox txtPassword;
        internal System.Windows.Forms.Label lblCode;
        internal System.Windows.Forms.Label lblDate;
        internal System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}