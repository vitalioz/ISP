
namespace Accounting
{
    partial class frmPortfoliosMenu
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
            this.btnPortfolio = new System.Windows.Forms.Button();
            this.btnGAP = new System.Windows.Forms.Button();
            this.btnPortfolio_Monitoring = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.btnAccTrx = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPortfolio
            // 
            this.btnPortfolio.Location = new System.Drawing.Point(303, 41);
            this.btnPortfolio.Name = "btnPortfolio";
            this.btnPortfolio.Size = new System.Drawing.Size(166, 23);
            this.btnPortfolio.TabIndex = 4;
            this.btnPortfolio.Text = "Portfolio";
            this.btnPortfolio.UseVisualStyleBackColor = true;
            this.btnPortfolio.Click += new System.EventHandler(this.btnPortfolio_Click);
            // 
            // btnGAP
            // 
            this.btnGAP.Location = new System.Drawing.Point(12, 12);
            this.btnGAP.Name = "btnGAP";
            this.btnGAP.Size = new System.Drawing.Size(166, 23);
            this.btnGAP.TabIndex = 3;
            this.btnGAP.Text = "Γενικό Λογιστικό Σχέδιο";
            this.btnGAP.UseVisualStyleBackColor = true;
            this.btnGAP.Click += new System.EventHandler(this.btnGAP_Click);
            // 
            // btnPortfolio_Monitoring
            // 
            this.btnPortfolio_Monitoring.Location = new System.Drawing.Point(303, 12);
            this.btnPortfolio_Monitoring.Name = "btnPortfolio_Monitoring";
            this.btnPortfolio_Monitoring.Size = new System.Drawing.Size(166, 23);
            this.btnPortfolio_Monitoring.TabIndex = 5;
            this.btnPortfolio_Monitoring.Text = "Portofolio Monitoring";
            this.btnPortfolio_Monitoring.UseVisualStyleBackColor = true;
            this.btnPortfolio_Monitoring.Click += new System.EventHandler(this.btnPortfolio_Monitoring_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(303, 70);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(166, 23);
            this.button7.TabIndex = 11;
            this.button7.Text = "Portfolios Planning";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // btnAccTrx
            // 
            this.btnAccTrx.Location = new System.Drawing.Point(12, 41);
            this.btnAccTrx.Name = "btnAccTrx";
            this.btnAccTrx.Size = new System.Drawing.Size(166, 23);
            this.btnAccTrx.TabIndex = 12;
            this.btnAccTrx.Text = "Accounting Transactions";
            this.btnAccTrx.UseVisualStyleBackColor = true;
            this.btnAccTrx.Click += new System.EventHandler(this.btnAccTrx_Click);
            // 
            // frmPortfoliosMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnAccTrx);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.btnPortfolio_Monitoring);
            this.Controls.Add(this.btnPortfolio);
            this.Controls.Add(this.btnGAP);
            this.Name = "frmPortfoliosMenu";
            this.Text = "Κινήσεις αιτιολογίες";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPortfolio;
        private System.Windows.Forms.Button btnGAP;
        private System.Windows.Forms.Button btnPortfolio_Monitoring;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button btnAccTrx;
    }
}