namespace Core
{
    partial class frmProductData
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
            this.ucRates = new Core.ucProducts_Rates();
            this.ucFunds = new Core.ucProducts_Fund();
            this.ucETFs = new Core.ucProducts_ETF();
            this.ucBonds = new Core.ucProducts_Bond();
            this.ucShares = new Core.ucProducts_Share();
            this.ucIndexes = new Core.ucProducts_Indexes();
            this.SuspendLayout();
            // 
            // ucRates
            // 
            this.ucRates.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ucRates.Location = new System.Drawing.Point(1350, 358);
            this.ucRates.Mode = 0;
            this.ucRates.Name = "ucRates";
            this.ucRates.Size = new System.Drawing.Size(252, 162);
            this.ucRates.TabIndex = 4;
            // 
            // ucFunds
            // 
            this.ucFunds.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ucFunds.Location = new System.Drawing.Point(1338, 37);
            this.ucFunds.Mode = 0;
            this.ucFunds.Name = "ucFunds";
            this.ucFunds.RightsLevel = 0;
            this.ucFunds.Size = new System.Drawing.Size(254, 149);
            this.ucFunds.TabIndex = 3;
            // 
            // ucETFs
            // 
            this.ucETFs.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ucETFs.Location = new System.Drawing.Point(1350, 526);
            this.ucETFs.Mode = 0;
            this.ucETFs.Name = "ucETFs";
            this.ucETFs.RightsLevel = 0;
            this.ucETFs.Size = new System.Drawing.Size(275, 159);
            this.ucETFs.TabIndex = 2;
            // 
            // ucBonds
            // 
            this.ucBonds.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ucBonds.Location = new System.Drawing.Point(1350, 210);
            this.ucBonds.Mode = 0;
            this.ucBonds.Name = "ucBonds";
            this.ucBonds.RightsLevel = 0;
            this.ucBonds.Size = new System.Drawing.Size(215, 159);
            this.ucBonds.TabIndex = 1;
            // 
            // ucShares
            // 
            this.ucShares.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ucShares.Location = new System.Drawing.Point(12, 12);
            this.ucShares.Mode = 0;
            this.ucShares.Name = "ucShares";
            this.ucShares.RightsLevel = 0;
            this.ucShares.Size = new System.Drawing.Size(1324, 760);
            this.ucShares.TabIndex = 0;
            // 
            // ucIndexes
            // 
            this.ucIndexes.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ucIndexes.Location = new System.Drawing.Point(1360, 703);
            this.ucIndexes.Mode = 0;
            this.ucIndexes.Name = "ucIndexes";
            this.ucIndexes.RightsLevel = 0;
            this.ucIndexes.Size = new System.Drawing.Size(265, 245);
            this.ucIndexes.TabIndex = 5;
            // 
            // frmProductData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1650, 764);
            this.Controls.Add(this.ucIndexes);
            this.Controls.Add(this.ucRates);
            this.Controls.Add(this.ucFunds);
            this.Controls.Add(this.ucETFs);
            this.Controls.Add(this.ucBonds);
            this.Controls.Add(this.ucShares);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmProductData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmProductData";
            this.Load += new System.EventHandler(this.frmProductData_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ucProducts_Share ucShares;
        private ucProducts_Bond ucBonds;
        private ucProducts_ETF ucETFs;
        private ucProducts_Fund ucFunds;
        private ucProducts_Rates ucRates;
        private ucProducts_Indexes ucIndexes;
    }
}