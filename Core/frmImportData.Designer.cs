namespace Core
{
    partial class frmImportData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportData));
            this.grpSchema = new System.Windows.Forms.GroupBox();
            this.txtFinishColumn = new System.Windows.Forms.TextBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.txtSheetNumber = new System.Windows.Forms.TextBox();
            this.cmbSchemas = new System.Windows.Forms.ComboBox();
            this.picSchemasList = new System.Windows.Forms.PictureBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.txtHeaderLines = new System.Windows.Forms.TextBox();
            this.txtTargetColumns = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.txtSourceColumns = new System.Windows.Forms.TextBox();
            this.panOK = new System.Windows.Forms.Panel();
            this.Label5 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.picFilesPath = new System.Windows.Forms.PictureBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.cmbFileType = new System.Windows.Forms.ComboBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.grpSchema.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSchemasList)).BeginInit();
            this.panOK.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFilesPath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSchema
            // 
            this.grpSchema.Controls.Add(this.txtFinishColumn);
            this.grpSchema.Controls.Add(this.Label9);
            this.grpSchema.Controls.Add(this.Label4);
            this.grpSchema.Controls.Add(this.txtSheetNumber);
            this.grpSchema.Controls.Add(this.cmbSchemas);
            this.grpSchema.Controls.Add(this.picSchemasList);
            this.grpSchema.Controls.Add(this.Label8);
            this.grpSchema.Controls.Add(this.txtHeaderLines);
            this.grpSchema.Controls.Add(this.txtTargetColumns);
            this.grpSchema.Controls.Add(this.Label1);
            this.grpSchema.Controls.Add(this.Label7);
            this.grpSchema.Controls.Add(this.Label6);
            this.grpSchema.Controls.Add(this.txtSourceColumns);
            this.grpSchema.Location = new System.Drawing.Point(515, 5);
            this.grpSchema.Name = "grpSchema";
            this.grpSchema.Size = new System.Drawing.Size(360, 161);
            this.grpSchema.TabIndex = 265;
            this.grpSchema.TabStop = false;
            this.grpSchema.Text = "Μετατροπή";
            // 
            // txtFinishColumn
            // 
            this.txtFinishColumn.Location = new System.Drawing.Point(182, 136);
            this.txtFinishColumn.Name = "txtFinishColumn";
            this.txtFinishColumn.Size = new System.Drawing.Size(55, 20);
            this.txtFinishColumn.TabIndex = 260;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(9, 139);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(72, 13);
            this.Label9.TabIndex = 261;
            this.Label9.Text = "Finish Column";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(9, 49);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(75, 13);
            this.Label4.TabIndex = 259;
            this.Label4.Text = "Sheet Number";
            // 
            // txtSheetNumber
            // 
            this.txtSheetNumber.Location = new System.Drawing.Point(182, 46);
            this.txtSheetNumber.Name = "txtSheetNumber";
            this.txtSheetNumber.Size = new System.Drawing.Size(55, 20);
            this.txtSheetNumber.TabIndex = 6;
            // 
            // cmbSchemas
            // 
            this.cmbSchemas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSchemas.FormattingEnabled = true;
            this.cmbSchemas.Location = new System.Drawing.Point(57, 19);
            this.cmbSchemas.Name = "cmbSchemas";
            this.cmbSchemas.Size = new System.Drawing.Size(260, 21);
            this.cmbSchemas.TabIndex = 4;
            this.cmbSchemas.SelectedValueChanged += new System.EventHandler(this.cmbSchemas_SelectedValueChanged);
            // 
            // picSchemasList
            // 
            this.picSchemasList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picSchemasList.Image = ((System.Drawing.Image)(resources.GetObject("picSchemasList.Image")));
            this.picSchemasList.Location = new System.Drawing.Point(321, 21);
            this.picSchemasList.Name = "picSchemasList";
            this.picSchemasList.Size = new System.Drawing.Size(19, 19);
            this.picSchemasList.TabIndex = 250;
            this.picSchemasList.TabStop = false;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(9, 94);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(136, 13);
            this.Label8.TabIndex = 257;
            this.Label8.Text = "Στύλες στο τελικό αρχείο";
            // 
            // txtHeaderLines
            // 
            this.txtHeaderLines.Location = new System.Drawing.Point(182, 113);
            this.txtHeaderLines.Name = "txtHeaderLines";
            this.txtHeaderLines.Size = new System.Drawing.Size(55, 20);
            this.txtHeaderLines.TabIndex = 12;
            // 
            // txtTargetColumns
            // 
            this.txtTargetColumns.Location = new System.Drawing.Point(182, 91);
            this.txtTargetColumns.Name = "txtTargetColumns";
            this.txtTargetColumns.Size = new System.Drawing.Size(55, 20);
            this.txtTargetColumns.TabIndex = 10;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(9, 116);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(85, 13);
            this.Label1.TabIndex = 217;
            this.Label1.Text = "Γραμμές τίτλου";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(9, 71);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(166, 13);
            this.Label7.TabIndex = 255;
            this.Label7.Text = "Στύλες στο εισαγώμενο αρχείο";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(12, 24);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(39, 13);
            this.Label6.TabIndex = 254;
            this.Label6.Text = "Σχήμα";
            // 
            // txtSourceColumns
            // 
            this.txtSourceColumns.Location = new System.Drawing.Point(182, 68);
            this.txtSourceColumns.Name = "txtSourceColumns";
            this.txtSourceColumns.Size = new System.Drawing.Size(55, 20);
            this.txtSourceColumns.TabIndex = 8;
            // 
            // panOK
            // 
            this.panOK.Controls.Add(this.Label5);
            this.panOK.Controls.Add(this.btnOK);
            this.panOK.Location = new System.Drawing.Point(9, 172);
            this.panOK.Name = "panOK";
            this.panOK.Size = new System.Drawing.Size(216, 28);
            this.panOK.TabIndex = 263;
            this.panOK.Visible = false;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(6, 7);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(117, 13);
            this.Label5.TabIndex = 217;
            this.Label5.Text = "Εάν είναι ΟΚ πατήστε";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(129, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.txtFilePath);
            this.GroupBox1.Controls.Add(this.picFilesPath);
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Controls.Add(this.Label3);
            this.GroupBox1.Controls.Add(this.cmbFileType);
            this.GroupBox1.Location = new System.Drawing.Point(11, 4);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(488, 162);
            this.GroupBox1.TabIndex = 264;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Αρχείο δεδομένων";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(73, 23);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(382, 20);
            this.txtFilePath.TabIndex = 0;
            // 
            // picFilesPath
            // 
            this.picFilesPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picFilesPath.Image = ((System.Drawing.Image)(resources.GetObject("picFilesPath.Image")));
            this.picFilesPath.Location = new System.Drawing.Point(458, 21);
            this.picFilesPath.Name = "picFilesPath";
            this.picFilesPath.Size = new System.Drawing.Size(24, 24);
            this.picFilesPath.TabIndex = 248;
            this.picFilesPath.TabStop = false;
            this.picFilesPath.Click += new System.EventHandler(this.picFilesPath_Click);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(13, 26);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(54, 13);
            this.Label2.TabIndex = 251;
            this.Label2.Text = "Διαδρομή";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(13, 52);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(34, 13);
            this.Label3.TabIndex = 252;
            this.Label3.Text = "Τίπος";
            // 
            // cmbFileType
            // 
            this.cmbFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFileType.FormattingEnabled = true;
            this.cmbFileType.Items.AddRange(new object[] {
            ".xlsx (Excel 2007)",
            ".xls (Excel 2003)",
            ".csv",
            ".docx (Word 2007)",
            ".doc (Word 2003)",
            ".txt"});
            this.cmbFileType.Location = new System.Drawing.Point(73, 50);
            this.cmbFileType.Name = "cmbFileType";
            this.cmbFileType.Size = new System.Drawing.Size(260, 21);
            this.cmbFileType.TabIndex = 2;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(897, 73);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(80, 25);
            this.btnImport.TabIndex = 261;
            this.btnImport.Text = "Εισαγωγή";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // fgList
            // 
            this.fgList.AllowEditing = false;
            this.fgList.ColumnInfo = "0,0,0,0,0,85,Columns:";
            this.fgList.Location = new System.Drawing.Point(6, 206);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(973, 388);
            this.fgList.TabIndex = 262;
            // 
            // frmImportData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Salmon;
            this.ClientSize = new System.Drawing.Size(985, 601);
            this.Controls.Add(this.grpSchema);
            this.Controls.Add(this.panOK);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.fgList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmImportData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmImportData";
            this.Load += new System.EventHandler(this.frmImportData_Load);
            this.grpSchema.ResumeLayout(false);
            this.grpSchema.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSchemasList)).EndInit();
            this.panOK.ResumeLayout(false);
            this.panOK.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFilesPath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox grpSchema;
        internal System.Windows.Forms.TextBox txtFinishColumn;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.TextBox txtSheetNumber;
        internal System.Windows.Forms.ComboBox cmbSchemas;
        internal System.Windows.Forms.PictureBox picSchemasList;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.TextBox txtHeaderLines;
        internal System.Windows.Forms.TextBox txtTargetColumns;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.TextBox txtSourceColumns;
        internal System.Windows.Forms.Panel panOK;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Button btnOK;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.TextBox txtFilePath;
        internal System.Windows.Forms.PictureBox picFilesPath;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Button btnImport;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
        public System.Windows.Forms.ComboBox cmbFileType;
    }
}