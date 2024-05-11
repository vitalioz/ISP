namespace Core
{
    partial class frmDocFilesEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDocFilesEdit));
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.chkOldFiles = new System.Windows.Forms.CheckBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.cmbDocTypes = new System.Windows.Forms.ComboBox();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.picShow = new System.Windows.Forms.PictureBox();
            this.picDocFilesPath = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picShow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDocFilesPath)).BeginInit();
            this.SuspendLayout();
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.chkOldFiles);
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Controls.Add(this.cmbDocTypes);
            this.GroupBox1.Controls.Add(this.txtFileName);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Controls.Add(this.picShow);
            this.GroupBox1.Controls.Add(this.picDocFilesPath);
            this.GroupBox1.Location = new System.Drawing.Point(4, 3);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(596, 138);
            this.GroupBox1.TabIndex = 159;
            this.GroupBox1.TabStop = false;
            // 
            // chkOldFiles
            // 
            this.chkOldFiles.AutoSize = true;
            this.chkOldFiles.Location = new System.Drawing.Point(116, 104);
            this.chkOldFiles.Name = "chkOldFiles";
            this.chkOldFiles.Size = new System.Drawing.Size(60, 17);
            this.chkOldFiles.TabIndex = 8;
            this.chkOldFiles.Text = "Αρχείο";
            this.chkOldFiles.UseVisualStyleBackColor = true;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(10, 37);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(91, 13);
            this.Label2.TabIndex = 181;
            this.Label2.Text = "Τύπος εγγράφου";
            // 
            // cmbDocTypes
            // 
            this.cmbDocTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDocTypes.FormattingEnabled = true;
            this.cmbDocTypes.Location = new System.Drawing.Point(116, 34);
            this.cmbDocTypes.Name = "cmbDocTypes";
            this.cmbDocTypes.Size = new System.Drawing.Size(412, 21);
            this.cmbDocTypes.TabIndex = 4;
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(116, 63);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(412, 20);
            this.txtFileName.TabIndex = 6;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(10, 67);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(84, 13);
            this.Label1.TabIndex = 179;
            this.Label1.Text = "Όνομα αρχείου";
            // 
            // picShow
            // 
            this.picShow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picShow.Image = ((System.Drawing.Image)(resources.GetObject("picShow.Image")));
            this.picShow.Location = new System.Drawing.Point(555, 63);
            this.picShow.Name = "picShow";
            this.picShow.Size = new System.Drawing.Size(24, 24);
            this.picShow.TabIndex = 177;
            this.picShow.TabStop = false;
            this.picShow.Click += new System.EventHandler(this.picShow_Click);
            // 
            // picDocFilesPath
            // 
            this.picDocFilesPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDocFilesPath.Image = ((System.Drawing.Image)(resources.GetObject("picDocFilesPath.Image")));
            this.picDocFilesPath.Location = new System.Drawing.Point(531, 63);
            this.picDocFilesPath.Name = "picDocFilesPath";
            this.picDocFilesPath.Size = new System.Drawing.Size(24, 24);
            this.picDocFilesPath.TabIndex = 176;
            this.picDocFilesPath.TabStop = false;
            this.picDocFilesPath.Click += new System.EventHandler(this.picDocFilesPath_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(320, 162);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 28);
            this.btnCancel.TabIndex = 158;
            this.btnCancel.Text = "   Άκυρο";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(178, 162);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 28);
            this.btnSave.TabIndex = 157;
            this.btnSave.Text = "   Αποθήκευση";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmDocFilesEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(604, 201);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmDocFilesEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDocFilesEdit";
            this.Load += new System.EventHandler(this.frmDocFilesEdit_Load);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picShow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDocFilesPath)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.CheckBox chkOldFiles;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.ComboBox cmbDocTypes;
        internal System.Windows.Forms.TextBox txtFileName;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.PictureBox picShow;
        internal System.Windows.Forms.PictureBox picDocFilesPath;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Button btnSave;
    }
}