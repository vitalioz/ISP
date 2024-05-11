namespace Core
{
    partial class frmClientData
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
            this.ucCD = new Core.ucClientData();
            this.SuspendLayout();
            // 
            // ucCD
            // 
            this.ucCD.CheckTrack = false;
            this.ucCD.Client_ID = 0;
            this.ucCD.Location = new System.Drawing.Point(2, 2);
            this.ucCD.Name = "ucCD";
            this.ucCD.Record_ID = 0;
            this.ucCD.Size = new System.Drawing.Size(924, 794);
            this.ucCD.TabIndex = 0;
            this.ucCD.Users_List = null;
            // 
            // frmClientData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 798);
            this.Controls.Add(this.ucCD);
            this.Name = "frmClientData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmClientData";
            this.Load += new System.EventHandler(this.frmClientData_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Core.ucClientData ucCD;
    }
}