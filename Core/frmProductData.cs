using System;

using System.Windows.Forms;

namespace Core
{
    public partial class frmProductData : Form
    {
        int iMode, iProduct_ID, iShareCode_ID, iLastAktion, iRightsLevel;
        public frmProductData()
        {
            InitializeComponent();

            ucShares.lblFlagEdit.TextChanged += close_me;
            ucShares.Left = -4000;
            ucShares.Top = -4000;

            ucBonds.lblFlagEdit.TextChanged += close_me;
            ucBonds.Left = -4000;
            ucBonds.Top = -4000;

            ucETFs.lblFlagEdit.TextChanged += close_me;
            ucETFs.Left = -4000;
            ucETFs.Top = -4000;

            ucFunds.lblFlagEdit.TextChanged += close_me;
            ucFunds.Left = -4000;
            ucFunds.Top = -4000;

            ucRates.lblFlagEdit.TextChanged += close_me;
            ucRates.Left = -4000;
            ucRates.Top = -4000;

            ucIndexes.lblFlagEdit.TextChanged += close_me;
            ucIndexes.Left = -4000;
            ucIndexes.Top = -4000;
        }

        private void frmProductData_Load(object sender, EventArgs e)
        {
            ucShares.Visible = false;
            ucBonds.Visible = false;
            ucETFs.Visible = false;
            ucFunds.Visible = false;
            ucRates.Visible = false;
            ucIndexes.Visible = false;

            switch (iProduct_ID)
            {
                case 1:
                    ucShares.Left = 4;
                    ucShares.Top = 4;
                    ucShares.Mode = iMode;             // 1 - from ProductsList, 2 - from ProductsData, 3 - from SelectedProducts
                    if (iShareCode_ID > 0) ucShares.ShowRecord(0, 0, iShareCode_ID, iRightsLevel);
                    else ucShares.AddRecord();
                    ucShares.Visible = true;
                    break;
                case 2:
                    ucShares.Visible = false;
                    ucBonds.Left = 4;
                    ucBonds.Top = 4;
                    ucBonds.Mode = iMode;            // 1 - from ProductsList, 2 - from ProductsData, 3 - from SelectedProducts
                    if (iShareCode_ID > 0) ucBonds.ShowRecord(0, 0, iShareCode_ID, iRightsLevel);
                    else ucBonds.AddRecord();
                    ucBonds.Visible = true;
                    break;
                case 3:
                    ucRates.Left = 4;
                    ucRates.Top = 4;
                    if (iShareCode_ID > 0) ucRates.ShowRecord(0, 0, iShareCode_ID, iRightsLevel);
                    else ucRates.AddRecord();
                    ucRates.Visible = true;
                    break;
                case 4:
                    ucETFs.Left = 4;
                    ucETFs.Top = 4;
                    ucETFs.Mode = iMode;             // 1 - from ProductsList, 2 - from ProductsData, 3 - from SelectedProducts
                    if (iShareCode_ID > 0) ucETFs.ShowRecord(0, 0, iShareCode_ID, iRightsLevel);
                    else ucETFs.AddRecord();
                    ucETFs.Visible = true;
                    break;
                case 5:
                    ucIndexes.Left = 4;
                    ucIndexes.Top = 4;
                    ucIndexes.Mode = iMode;             // 1 - from ProductsList, 2 - from ProductsData, 3 - from SelectedProducts
                    if (iShareCode_ID > 0) ucIndexes.ShowRecord(0, 0, iShareCode_ID, iRightsLevel);
                    else ucIndexes.AddRecord();
                    ucIndexes.Visible = true;
                    break;
                case 6:
                    ucFunds.Left = 4;
                    ucFunds.Top = 4;
                    ucFunds.Mode = iMode;             // 1 - from ProductsList, 2 - from ProductsData, 3 - from SelectedProducts
                    if (iShareCode_ID > 0) ucFunds.ShowRecord(0, 0, iShareCode_ID, iRightsLevel);
                    else ucFunds.AddRecord();
                    ucFunds.Visible = true;
                    break;
            }
        }
        protected override void OnResize(EventArgs e)
        {
            ucShares.Width = this.Width - 24;
            ucShares.Height = this.Height - 48;

            ucBonds.Width = this.Width - 24;
            ucBonds.Height = this.Height - 48;

            ucETFs.Width = this.Width - 24;
            ucETFs.Height = this.Height - 48;

            ucFunds.Width = this.Width - 24;
            ucFunds.Height = this.Height - 48;

            ucRates.Width = this.Width - 24;
            ucRates.Height = this.Height - 48;

            ucIndexes.Width = this.Width - 24;
            ucIndexes.Height = this.Height - 48;
        }
        public void close_me(object sender, EventArgs e)
        {
            switch (iProduct_ID){
                case 1:
                    iLastAktion = Convert.ToInt32(ucShares.lblFlagEdit.Text);
                    break;
                case 2:
                    iLastAktion = Convert.ToInt32(ucBonds.lblFlagEdit.Text);
                    break;
                case 3:
                    iLastAktion = Convert.ToInt32(ucRates.lblFlagEdit.Text);
                    break;
                case 4:
                    iLastAktion = Convert.ToInt32(ucETFs.lblFlagEdit.Text);
                    break;
                case 5:
                    iLastAktion = Convert.ToInt32(ucIndexes.lblFlagEdit.Text);
                    break;
                case 6:
                    iLastAktion = Convert.ToInt32(ucFunds.lblFlagEdit.Text);
                    break;
            }
            this.Close();
        }
        public int Mode { get { return this.iMode; } set { this.iMode = value; } }
        public int Product_ID { get { return this.iProduct_ID; } set { this.iProduct_ID = value; } }
        public int ShareCode_ID { get { return this.iShareCode_ID; } set { this.iShareCode_ID = value; } }
        public int LastAktion { get { return this.iLastAktion; } set { this.iLastAktion = value; } }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
    }
}
