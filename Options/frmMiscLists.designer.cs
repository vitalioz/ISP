namespace Options
{
    partial class frmMiscLists
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMiscLists));
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.ucCountriesList = new Options.ucCountriesList();
            this.ucCurrenciesList = new Options.ucCurrenciesList();
            this.ucSL = new Options.ucSectorsList();
            this.ucSE = new Options.ucStockExchangesList();
            this.ucDL = new Core.ucDefaultList();
            this.ucDepositories = new Options.ucDepositories();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.SuspendLayout();
            // 
            // fgList
            // 
            this.fgList.AllowEditing = false;
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(12, 11);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(434, 674);
            this.fgList.TabIndex = 284;
            // 
            // ucCountriesList
            // 
            this.ucCountriesList.Location = new System.Drawing.Point(1149, 512);
            this.ucCountriesList.Name = "ucCountriesList";
            this.ucCountriesList.RightsLevel = 0;
            this.ucCountriesList.Size = new System.Drawing.Size(160, 119);
            this.ucCountriesList.TabIndex = 289;
            // 
            // ucCurrenciesList
            // 
            this.ucCurrenciesList.Location = new System.Drawing.Point(1149, 330);
            this.ucCurrenciesList.Name = "ucCurrenciesList";
            this.ucCurrenciesList.RightsLevel = 0;
            this.ucCurrenciesList.Size = new System.Drawing.Size(177, 131);
            this.ucCurrenciesList.TabIndex = 288;
            // 
            // ucSL
            // 
            this.ucSL.Location = new System.Drawing.Point(1129, 183);
            this.ucSL.Name = "ucSL";
            this.ucSL.RightsLevel = 0;
            this.ucSL.Size = new System.Drawing.Size(165, 122);
            this.ucSL.TabIndex = 287;
            // 
            // ucSE
            // 
            this.ucSE.Location = new System.Drawing.Point(1129, 11);
            this.ucSE.Name = "ucSE";
            this.ucSE.RightsLevel = 0;
            this.ucSE.Size = new System.Drawing.Size(180, 152);
            this.ucSE.TabIndex = 286;
            // 
            // ucDL
            // 
            this.ucDL.Location = new System.Drawing.Point(454, 2);
            this.ucDL.Name = "ucDL";
            this.ucDL.Size = new System.Drawing.Size(232, 162);
            this.ucDL.TabIndex = 285;
            // 
            // ucDepositories
            // 
            this.ucDepositories.Location = new System.Drawing.Point(930, 28);
            this.ucDepositories.Name = "ucDepositories";
            this.ucDepositories.RightsLevel = 0;
            this.ucDepositories.Size = new System.Drawing.Size(164, 135);
            this.ucDepositories.TabIndex = 290;
            // 
            // frmMiscLists
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Thistle;
            this.ClientSize = new System.Drawing.Size(1338, 692);
            this.Controls.Add(this.ucDepositories);
            this.Controls.Add(this.ucCountriesList);
            this.Controls.Add(this.ucCurrenciesList);
            this.Controls.Add(this.ucSL);
            this.Controls.Add(this.ucSE);
            this.Controls.Add(this.ucDL);
            this.Controls.Add(this.fgList);
            this.Name = "frmMiscLists";
            this.Text = "frmMiscList";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMiscList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
        private Core.ucDefaultList ucDL;
        private ucStockExchangesList ucSE;
        private ucSectorsList ucSL;
        private ucCurrenciesList ucCurrenciesList;
        private ucCountriesList ucCountriesList;
        private ucDepositories ucDepositories;
    }
}