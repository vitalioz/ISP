namespace Core
{
    partial class frmFIXReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFIXReport));
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.lblClOrdID = new System.Windows.Forms.Label();
            this.Label24 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.SuspendLayout();
            // 
            // fgList
            // 
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(7, 48);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(1143, 555);
            this.fgList.TabIndex = 1105;
            // 
            // lblClOrdID
            // 
            this.lblClOrdID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClOrdID.Location = new System.Drawing.Point(65, 14);
            this.lblClOrdID.Name = "lblClOrdID";
            this.lblClOrdID.Size = new System.Drawing.Size(127, 20);
            this.lblClOrdID.TabIndex = 1106;
            // 
            // Label24
            // 
            this.Label24.AutoSize = true;
            this.Label24.Location = new System.Drawing.Point(15, 17);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(44, 13);
            this.Label24.TabIndex = 1107;
            this.Label24.Text = "ClOrdID";
            // 
            // frmFIXReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1156, 610);
            this.Controls.Add(this.Label24);
            this.Controls.Add(this.lblClOrdID);
            this.Controls.Add(this.fgList);
            this.Name = "frmFIXReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmFIXReport";
            this.Load += new System.EventHandler(this.frmFIXReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
        internal System.Windows.Forms.Label lblClOrdID;
        internal System.Windows.Forms.Label Label24;
    }
}