namespace Core
{
    partial class frmShowHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShowHistory));
            this.panFileName = new System.Windows.Forms.Panel();
            this.Label2 = new System.Windows.Forms.Label();
            this.cmbDocTypes = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.Label11 = new System.Windows.Forms.Label();
            this.picFileName = new System.Windows.Forms.PictureBox();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.toolLeft = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tslElectronicDocument = new System.Windows.Forms.ToolStripLabel();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tslPrototypeDocument = new System.Windows.Forms.ToolStripLabel();
            this.tslAddDocument = new System.Windows.Forms.ToolStripLabel();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panFileName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFileName)).BeginInit();
            this.toolLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.SuspendLayout();
            // 
            // panFileName
            // 
            this.panFileName.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panFileName.Controls.Add(this.Label2);
            this.panFileName.Controls.Add(this.cmbDocTypes);
            this.panFileName.Controls.Add(this.btnCancel);
            this.panFileName.Controls.Add(this.btnSave);
            this.panFileName.Controls.Add(this.Label11);
            this.panFileName.Controls.Add(this.picFileName);
            this.panFileName.Controls.Add(this.txtFileName);
            this.panFileName.Location = new System.Drawing.Point(206, 169);
            this.panFileName.Name = "panFileName";
            this.panFileName.Size = new System.Drawing.Size(523, 127);
            this.panFileName.TabIndex = 212;
            this.panFileName.Visible = false;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(13, 24);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(91, 13);
            this.Label2.TabIndex = 252;
            this.Label2.Text = "Τύπος εγγράφου";
            // 
            // cmbDocTypes
            // 
            this.cmbDocTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDocTypes.FormattingEnabled = true;
            this.cmbDocTypes.Location = new System.Drawing.Point(120, 21);
            this.cmbDocTypes.Name = "cmbDocTypes";
            this.cmbDocTypes.Size = new System.Drawing.Size(386, 21);
            this.cmbDocTypes.TabIndex = 247;
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::Core.Properties.Resources.cancel1;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(288, 85);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(104, 28);
            this.btnCancel.TabIndex = 253;
            this.btnCancel.Text = "   Άκυρο";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::Core.Properties.Resources.OK;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(150, 85);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(104, 28);
            this.btnSave.TabIndex = 251;
            this.btnSave.Text = "OK";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(13, 52);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(54, 13);
            this.Label11.TabIndex = 247;
            this.Label11.Text = "Έγγραφο";
            // 
            // picFileName
            // 
            this.picFileName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picFileName.Image = global::Core.Properties.Resources.FindFolder;
            this.picFileName.Location = new System.Drawing.Point(478, 46);
            this.picFileName.Name = "picFileName";
            this.picFileName.Size = new System.Drawing.Size(28, 25);
            this.picFileName.TabIndex = 246;
            this.picFileName.TabStop = false;
            this.picFileName.Click += new System.EventHandler(this.picFileName_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(120, 48);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(352, 20);
            this.txtFileName.TabIndex = 249;
            // 
            // toolLeft
            // 
            this.toolLeft.AutoSize = false;
            this.toolLeft.BackColor = System.Drawing.Color.Gainsboro;
            this.toolLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolLeft.Dock = System.Windows.Forms.DockStyle.None;
            this.toolLeft.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolLeft.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolLeft.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripLabel2,
            this.tslElectronicDocument,
            this.ToolStripSeparator2,
            this.tslPrototypeDocument,
            this.tslAddDocument});
            this.toolLeft.Location = new System.Drawing.Point(7, 6);
            this.toolLeft.Name = "toolLeft";
            this.toolLeft.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolLeft.Size = new System.Drawing.Size(298, 25);
            this.toolLeft.TabIndex = 211;
            this.toolLeft.Text = "ToolStrip1";
            // 
            // ToolStripLabel2
            // 
            this.ToolStripLabel2.Name = "ToolStripLabel2";
            this.ToolStripLabel2.Size = new System.Drawing.Size(10, 22);
            this.ToolStripLabel2.Text = " ";
            // 
            // tslElectronicDocument
            // 
            this.tslElectronicDocument.IsLink = true;
            this.tslElectronicDocument.Name = "tslElectronicDocument";
            this.tslElectronicDocument.Size = new System.Drawing.Size(127, 22);
            this.tslElectronicDocument.Text = "Ηλεκτρονικό Έγγραφο";
            this.tslElectronicDocument.Click += new System.EventHandler(this.tslElectronicDocument_Click);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tslPrototypeDocument
            // 
            this.tslPrototypeDocument.IsLink = true;
            this.tslPrototypeDocument.Name = "tslPrototypeDocument";
            this.tslPrototypeDocument.Size = new System.Drawing.Size(127, 22);
            this.tslPrototypeDocument.Text = "Πρωτότυπο Έγγραφο";
            this.tslPrototypeDocument.Click += new System.EventHandler(this.tslPrototypeDocument_Click);
            // 
            // tslAddDocument
            // 
            this.tslAddDocument.IsLink = true;
            this.tslAddDocument.Name = "tslAddDocument";
            this.tslAddDocument.Size = new System.Drawing.Size(122, 15);
            this.tslAddDocument.Text = "Προσθήκη Εγγράφου";
            this.tslAddDocument.Click += new System.EventHandler(this.tslAddDocument_Click);
            // 
            // fgList
            // 
            this.fgList.AllowEditing = false;
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(7, 36);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(908, 464);
            this.fgList.TabIndex = 210;
            // 
            // frmShowHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.BurlyWood;
            this.ClientSize = new System.Drawing.Size(919, 506);
            this.Controls.Add(this.panFileName);
            this.Controls.Add(this.toolLeft);
            this.Controls.Add(this.fgList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmShowHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ιστορία";
            this.Load += new System.EventHandler(this.frmShowHistory_Load);
            this.panFileName.ResumeLayout(false);
            this.panFileName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFileName)).EndInit();
            this.toolLeft.ResumeLayout(false);
            this.toolLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel panFileName;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.ComboBox cmbDocTypes;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.PictureBox picFileName;
        internal System.Windows.Forms.TextBox txtFileName;
        internal System.Windows.Forms.ToolStrip toolLeft;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel2;
        internal System.Windows.Forms.ToolStripLabel tslElectronicDocument;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        internal System.Windows.Forms.ToolStripLabel tslPrototypeDocument;
        internal System.Windows.Forms.ToolStripLabel tslAddDocument;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
    }
}