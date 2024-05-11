namespace Core
{
    partial class frmReports
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
            this.crwReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crwReport
            // 
            this.crwReport.ActiveViewIndex = -1;
            this.crwReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crwReport.Cursor = System.Windows.Forms.Cursors.Default;
            this.crwReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crwReport.Location = new System.Drawing.Point(0, 0);
            this.crwReport.Name = "crwReport";
            this.crwReport.ShowCloseButton = false;
            this.crwReport.ShowCopyButton = false;
            this.crwReport.ShowGotoPageButton = false;
            this.crwReport.ShowGroupTreeButton = false;
            this.crwReport.ShowLogo = false;
            this.crwReport.ShowParameterPanelButton = false;
            this.crwReport.Size = new System.Drawing.Size(800, 450);
            this.crwReport.TabIndex = 0;
            this.crwReport.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // frmReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.crwReport);
            this.Name = "frmReports";
            this.Text = "frmReports";
            this.Load += new System.EventHandler(this.frmReports_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crwReport;
    }
}