namespace Core
{
    partial class frmOwnersEdit
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
            this.btnSave = new System.Windows.Forms.Button();
            this.cmbAFM = new System.Windows.Forms.ComboBox();
            this.cmbDOY = new System.Windows.Forms.ComboBox();
            this.chkOrder = new System.Windows.Forms.CheckBox();
            this.chkMaster = new System.Windows.Forms.CheckBox();
            this.lblSpecial = new System.Windows.Forms.Label();
            this.lblBorn = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.txtADT = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.txtFather = new System.Windows.Forms.TextBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.Label8 = new System.Windows.Forms.Label();
            this.grpData = new System.Windows.Forms.GroupBox();
            this.ucCS = new Core.ucClientSearch();
            this.txtPassport = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpData.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Image = global::Core.Properties.Resources.save;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(116, 268);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(113, 28);
            this.btnSave.TabIndex = 159;
            this.btnSave.Text = "      Αποθήκευση";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbAFM
            // 
            this.cmbAFM.FormattingEnabled = true;
            this.cmbAFM.Location = new System.Drawing.Point(107, 181);
            this.cmbAFM.Name = "cmbAFM";
            this.cmbAFM.Size = new System.Drawing.Size(199, 21);
            this.cmbAFM.TabIndex = 8;
            // 
            // cmbDOY
            // 
            this.cmbDOY.FormattingEnabled = true;
            this.cmbDOY.Location = new System.Drawing.Point(107, 145);
            this.cmbDOY.Name = "cmbDOY";
            this.cmbDOY.Size = new System.Drawing.Size(199, 21);
            this.cmbDOY.TabIndex = 6;
            // 
            // chkOrder
            // 
            this.chkOrder.AutoSize = true;
            this.chkOrder.Location = new System.Drawing.Point(231, 211);
            this.chkOrder.Name = "chkOrder";
            this.chkOrder.Size = new System.Drawing.Size(74, 17);
            this.chkOrder.TabIndex = 66;
            this.chkOrder.Text = "Εντολέας";
            this.chkOrder.UseVisualStyleBackColor = true;
            // 
            // chkMaster
            // 
            this.chkMaster.AutoSize = true;
            this.chkMaster.Location = new System.Drawing.Point(107, 211);
            this.chkMaster.Name = "chkMaster";
            this.chkMaster.Size = new System.Drawing.Size(58, 17);
            this.chkMaster.TabIndex = 65;
            this.chkMaster.Text = "Master";
            this.chkMaster.UseVisualStyleBackColor = true;
            // 
            // lblSpecial
            // 
            this.lblSpecial.AutoSize = true;
            this.lblSpecial.Location = new System.Drawing.Point(366, 89);
            this.lblSpecial.Name = "lblSpecial";
            this.lblSpecial.Size = new System.Drawing.Size(0, 13);
            this.lblSpecial.TabIndex = 64;
            this.lblSpecial.Visible = false;
            // 
            // lblBorn
            // 
            this.lblBorn.AutoSize = true;
            this.lblBorn.Location = new System.Drawing.Point(366, 63);
            this.lblBorn.Name = "lblBorn";
            this.lblBorn.Size = new System.Drawing.Size(0, 13);
            this.lblBorn.TabIndex = 63;
            this.lblBorn.Visible = false;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(11, 148);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(29, 13);
            this.Label4.TabIndex = 62;
            this.Label4.Text = "ΔΟΥ";
            // 
            // txtADT
            // 
            this.txtADT.Enabled = false;
            this.txtADT.Location = new System.Drawing.Point(107, 86);
            this.txtADT.Name = "txtADT";
            this.txtADT.Size = new System.Drawing.Size(199, 20);
            this.txtADT.TabIndex = 4;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(10, 29);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(89, 13);
            this.Label6.TabIndex = 33;
            this.Label6.Text = "Ονοματεπώνυμο";
            // 
            // txtFather
            // 
            this.txtFather.Enabled = false;
            this.txtFather.Location = new System.Drawing.Point(107, 56);
            this.txtFather.Name = "txtFather";
            this.txtFather.Size = new System.Drawing.Size(199, 20);
            this.txtFather.TabIndex = 2;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(11, 89);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(27, 13);
            this.Label9.TabIndex = 35;
            this.Label9.Text = "ΑΔΤ";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(11, 184);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(32, 13);
            this.Label1.TabIndex = 56;
            this.Label1.Text = "ΑΦΜ";
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::Core.Properties.Resources.cancel1;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(261, 268);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(114, 28);
            this.btnCancel.TabIndex = 160;
            this.btnCancel.Text = "   Άκυρο";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(10, 59);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(81, 13);
            this.Label8.TabIndex = 34;
            this.Label8.Text = "Όνομα πατρός";
            // 
            // grpData
            // 
            this.grpData.Controls.Add(this.txtPassport);
            this.grpData.Controls.Add(this.label2);
            this.grpData.Controls.Add(this.cmbAFM);
            this.grpData.Controls.Add(this.cmbDOY);
            this.grpData.Controls.Add(this.chkOrder);
            this.grpData.Controls.Add(this.chkMaster);
            this.grpData.Controls.Add(this.lblSpecial);
            this.grpData.Controls.Add(this.lblBorn);
            this.grpData.Controls.Add(this.Label4);
            this.grpData.Controls.Add(this.txtADT);
            this.grpData.Controls.Add(this.Label6);
            this.grpData.Controls.Add(this.txtFather);
            this.grpData.Controls.Add(this.Label8);
            this.grpData.Controls.Add(this.Label9);
            this.grpData.Controls.Add(this.Label1);
            this.grpData.Location = new System.Drawing.Point(12, 12);
            this.grpData.Name = "grpData";
            this.grpData.Size = new System.Drawing.Size(466, 243);
            this.grpData.TabIndex = 162;
            this.grpData.TabStop = false;
            // 
            // ucCS
            // 
            this.ucCS.Filters = "ID > 0";
            this.ucCS.ListType = 0;
            this.ucCS.Location = new System.Drawing.Point(117, 37);
            this.ucCS.Mode = 0;
            this.ucCS.Name = "ucCS";
            this.ucCS.ShowClientsList = true;
            this.ucCS.ShowHeight = 0;
            this.ucCS.ShowWidth = 0;
            this.ucCS.Size = new System.Drawing.Size(200, 20);
            this.ucCS.TabIndex = 0;
            // 
            // txtPassport
            // 
            this.txtPassport.Enabled = false;
            this.txtPassport.Location = new System.Drawing.Point(107, 116);
            this.txtPassport.Name = "txtPassport";
            this.txtPassport.Size = new System.Drawing.Size(199, 20);
            this.txtPassport.TabIndex = 67;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 68;
            this.label2.Text = "Διαβατήριο";
            // 
            // frmOwnersEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Thistle;
            this.ClientSize = new System.Drawing.Size(487, 302);
            this.Controls.Add(this.ucCS);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grpData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmOwnersEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmOwnersEdit";
            this.Load += new System.EventHandler(this.frmOwnersEdit_Load);
            this.grpData.ResumeLayout(false);
            this.grpData.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.ComboBox cmbAFM;
        internal System.Windows.Forms.ComboBox cmbDOY;
        internal System.Windows.Forms.CheckBox chkOrder;
        internal System.Windows.Forms.CheckBox chkMaster;
        internal System.Windows.Forms.Label lblSpecial;
        internal System.Windows.Forms.Label lblBorn;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.TextBox txtADT;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.TextBox txtFather;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.GroupBox grpData;
        public ucClientSearch ucCS;
        internal System.Windows.Forms.TextBox txtPassport;
        internal System.Windows.Forms.Label label2;
    }
}