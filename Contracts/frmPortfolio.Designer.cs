﻿
namespace Contracts
{
    partial class frmPortfolio
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
            this.btnGAP = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGAP
            // 
            this.btnGAP.Location = new System.Drawing.Point(24, 26);
            this.btnGAP.Name = "btnGAP";
            this.btnGAP.Size = new System.Drawing.Size(166, 23);
            this.btnGAP.TabIndex = 0;
            this.btnGAP.Text = "Γενικό Λογιστικό Σχέδιο";
            this.btnGAP.UseVisualStyleBackColor = true;
            this.btnGAP.Click += new System.EventHandler(this.btnGAP_Click);
            // 
            // frmPortfolio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 313);
            this.Controls.Add(this.btnGAP);
            this.Name = "frmPortfolio";
            this.Text = "frmPortfolio";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGAP;
    }
}