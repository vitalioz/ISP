using System;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Core;

namespace Transactions
{
    public partial class frmLLSearch : Form
    {
        int iMode, iRightsLevel;
        string sExtra;
        public frmLLSearch()
        {
            InitializeComponent();
        }

        private void frmLLSearch_Load(object sender, EventArgs e)
        {

            // AddHandler ucCustomerChoice.Client_ID.TextChanged, AddressOf AfterCustomerChoiced
            //ucCustomerChoice.Filters = "Client_ID > 0"
            //ucCustomerChoice.Mode = 1   ' 1 - One record choiced mode,  2 - Multiple records choiced mode
            //ucCustomerChoice.ListType = 4


            /*
            AddHandler ucContractChoice.Client_ID.TextChanged, AddressOf AfterContractChoiced
        ucContractChoice.Filters = "Client_ID > 0"
        ucContractChoice.Mode = 2   ' 1-One record choiced mode,  2-Multiple records choiced mode
        ucContractChoice.ListType = 1

        AddHandler ucCustomerChoiceFX.Client_ID.TextChanged, AddressOf AfterCustomerChoicedFX
        ucCustomerChoiceFX.Filters = "Status >= 0 AND Client_ID > 0"
        ucCustomerChoiceFX.Mode = 2   ' 1-One record choiced mode,  2-Multiple records choiced mode
        ucCustomerChoiceFX.ListType = 1

        AddHandler ucCustomerChoice_LL.Client_ID.TextChanged, AddressOf AfterCustomerChoicedLL
        ucCustomerChoice_LL.Filters = "Status >= 0 AND Client_ID > 0"
        ucCustomerChoice_LL.Mode = 2   ' 1-One record choiced mode,  2-Multiple records choiced mode
        ucCustomerChoice_LL.ListType = 1
            */

            cmbServiceProviders_LL.DataSource = Global.dtServiceProviders.Copy();
            cmbServiceProviders_LL.DisplayMember = "Title";
            cmbServiceProviders_LL.ValueMember = "ID";       

            cmbCurrency_LL.DataSource = Global.dtCurrencies.Copy();
            cmbCurrency_LL.DisplayMember = "Title";
            cmbCurrency_LL.ValueMember = "ID";
        }
        protected override void OnResize(EventArgs e)
        {
            panLombardLending.Top = 60;
            panLombardLending.Left = 2;
            panLombardLending.Width = this.Width - 22;
            panLombardLending.Height = this.Height - 102;

            //ucLL.Top = 2;
            //ucLL.Width = this.Width - 374;
            //ucLL.Height = this.Height - 106;
        }
        protected void ucContractChoice_TextOfLabelChanged(object sender, EventArgs e)
        {

        }  
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public int Mode { get { return this.iMode; } set { this.iMode = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
