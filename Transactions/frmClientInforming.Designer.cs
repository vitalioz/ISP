
namespace Transactions
{
    partial class frmClientInforming
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClientInforming));
            this.chkList = new System.Windows.Forms.CheckBox();
            this.btnInform = new System.Windows.Forms.Button();
            this.cmbInformMethods = new System.Windows.Forms.ComboBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.toolSymvoules = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.toolSymvoules.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkList
            // 
            this.chkList.AutoSize = true;
            this.chkList.Location = new System.Drawing.Point(13, 54);
            this.chkList.Name = "chkList";
            this.chkList.Size = new System.Drawing.Size(15, 14);
            this.chkList.TabIndex = 425;
            this.chkList.UseVisualStyleBackColor = true;
            this.chkList.CheckedChanged += new System.EventHandler(this.chkList_CheckedChanged);
            // 
            // btnInform
            // 
            this.btnInform.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInform.Location = new System.Drawing.Point(720, 566);
            this.btnInform.Name = "btnInform";
            this.btnInform.Size = new System.Drawing.Size(110, 28);
            this.btnInform.TabIndex = 424;
            this.btnInform.Text = "   Αποστολή";
            this.btnInform.UseVisualStyleBackColor = true;
            this.btnInform.Click += new System.EventHandler(this.btnInform_Click);
            // 
            // cmbInformMethods
            // 
            this.cmbInformMethods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInformMethods.FormattingEnabled = true;
            this.cmbInformMethods.Location = new System.Drawing.Point(529, 571);
            this.cmbInformMethods.Name = "cmbInformMethods";
            this.cmbInformMethods.Size = new System.Drawing.Size(176, 21);
            this.cmbInformMethods.TabIndex = 422;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(418, 575);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(109, 13);
            this.Label5.TabIndex = 423;
            this.Label5.Text = "Τρόπος ενημέρωσης";
            // 
            // fgList
            // 
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(7, 42);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 2;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Rows.Fixed = 2;
            this.fgList.Size = new System.Drawing.Size(1193, 508);
            this.fgList.TabIndex = 421;
            // 
            // toolSymvoules
            // 
            this.toolSymvoules.AutoSize = false;
            this.toolSymvoules.BackColor = System.Drawing.Color.Gainsboro;
            this.toolSymvoules.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolSymvoules.Dock = System.Windows.Forms.DockStyle.None;
            this.toolSymvoules.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolSymvoules.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolSymvoules.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripLabel1,
            this.tsbRefresh});
            this.toolSymvoules.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolSymvoules.Location = new System.Drawing.Point(9, 9);
            this.toolSymvoules.Name = "toolSymvoules";
            this.toolSymvoules.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolSymvoules.Size = new System.Drawing.Size(43, 25);
            this.toolSymvoules.TabIndex = 426;
            this.toolSymvoules.Text = "ToolStrip1";
            // 
            // ToolStripLabel1
            // 
            this.ToolStripLabel1.Name = "ToolStripLabel1";
            this.ToolStripLabel1.Size = new System.Drawing.Size(10, 22);
            this.ToolStripLabel1.Text = " ";
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefresh.Image")));
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(23, 22);
            this.tsbRefresh.Text = "Ανανέωση";
            // 
            // frmClientInforming
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.ClientSize = new System.Drawing.Size(1207, 604);
            this.Controls.Add(this.chkList);
            this.Controls.Add(this.btnInform);
            this.Controls.Add(this.cmbInformMethods);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.fgList);
            this.Controls.Add(this.toolSymvoules);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmClientInforming";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmClientInforming";
            this.Load += new System.EventHandler(this.frmClientInforming_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.toolSymvoules.ResumeLayout(false);
            this.toolSymvoules.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.CheckBox chkList;
        internal System.Windows.Forms.Button btnInform;
        internal System.Windows.Forms.ComboBox cmbInformMethods;
        internal System.Windows.Forms.Label Label5;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
        internal System.Windows.Forms.ToolStrip toolSymvoules;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel1;
        internal System.Windows.Forms.ToolStripButton tsbRefresh;
    }
}