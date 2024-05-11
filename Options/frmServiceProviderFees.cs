using System;
using System.Data;
using System.Windows.Forms;
using Core;

namespace Options
{
    public partial class frmServiceProviderFees : Form
    {
        DataTable dtList;
        DataView dtView;
        DataColumn dtCol;
        DataRow dtRow;
        int iAktion, iProduct_ID, iStockExchange_ID, iSettlementProviders_ID, iCategory_ID, iMode;
        string sTicketFeesCurr, sMinimumFeesCurr;
        bool bDefineList;
        public frmServiceProviderFees()
        {
            InitializeComponent();
        }

        private void frmServiceProviderFees_Load(object sender, EventArgs e)
        {
            bDefineList = false;

            lblCompanyMeridio.Text = "Μερίδιο της " + Global.CompanyName;

            switch (iMode)
            {
                case 1:
                    dtList = Global.dtStockExchanges.Copy();
                    dtList.Rows[0]["Title"] = "Όλα";

                    lblSE_Title.Text = "Χρηματιστήριο";
                    panSettlementProvider.Visible = true;
                    panBrokerage.Visible = true;
                    break;
                case 8:
                    dtList = Global.dtDepositories.Copy();
                    dtList.Rows[0]["Title"] = "Όλα";

                    lblSE_Title.Text = "Αποθετήριο";                    
                    panSettlementProvider.Visible = false;
                    panBrokerage.Visible = true;
                    break;
                default:
                    dtList = Global.dtStockExchanges.Copy();
                    dtList.Rows[0]["Title"] = "Όλα";

                    panBrokerage.Visible = false;
                    break;
            }

            //-------------- Define StockExcahnges  List ------------------

            cmbStockExchanges.DataSource = dtList;
            cmbStockExchanges.DisplayMember = "Title";
            cmbStockExchanges.ValueMember = "ID";

            //-------------- Define Products List - Fees Categories ------------------
            cmbProducts.DataSource = Global.dtProductTypes.Copy();
            cmbProducts.DisplayMember = "Title";
            cmbProducts.ValueMember = "ID";

            //-------------- Define SettlementProviders List ------------------
            cmbSettlementProviders.DataSource = Global.dtServiceProviders.Copy();
            cmbSettlementProviders.DisplayMember = "Title";
            cmbSettlementProviders.ValueMember = "ID";

            //-------------- Define Products Categories List ------------------------
            dtList = new DataTable("List");
            dtCol = dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dtCol = dtList.Columns.Add("Title", System.Type.GetType("System.String"));

            dtRow = dtList.NewRow();
            dtRow["ID"] = 0;
            dtRow["Title"] = "Όλες";
            dtList.Rows.Add(dtRow);

            cmbCategories.DataSource = dtList;
            cmbCategories.DisplayMember = "Title";
            cmbCategories.ValueMember = "ID";

            //-------------- Define Currencies List ------------------
            cmbTicketFeesCurrs.DataSource = Global.dtCurrencies.Copy();
            cmbTicketFeesCurrs.DisplayMember = "Title";
            cmbTicketFeesCurrs.ValueMember = "ID";
            cmbTicketFeesCurrs.Text = sTicketFeesCurr;

            //-------------- Define Currencies List ------------------
            cmbMinimumFeesCurrs.DataSource = Global.dtCurrencies.Copy();
            cmbMinimumFeesCurrs.DisplayMember = "Title";
            cmbMinimumFeesCurrs.ValueMember = "ID";
            cmbMinimumFeesCurrs.Text = MinimumFeesCurr;
            bDefineList = true;

            if (iAktion == 0) {
                cmbProducts.SelectedValue = 0;
                cmbCategories.SelectedValue = 0;
                cmbStockExchanges.SelectedValue = 0;
                cmbSettlementProviders.SelectedValue = 0;

            }
            else {
                cmbProducts.SelectedValue = iProduct_ID;
                cmbCategories.SelectedValue = Category_ID;
                cmbStockExchanges.SelectedValue = iStockExchange_ID;
                cmbSettlementProviders.SelectedValue = iSettlementProviders_ID;
            }
        }
        private void cmbProducts_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bDefineList) DefineCategoriesList(Convert.ToInt32(cmbProducts.SelectedValue));
        }

        private void DefineCategoriesList(int iLocalProduct_ID)
        {
            //-------------- Define Product Categories List ------------------
            dtView = Global.dtProductsCategories.Copy().DefaultView;
            dtView.RowFilter = "Product_ID = " + iLocalProduct_ID + " OR Product_ID = 0";
            cmbCategories.DataSource = dtView;
            cmbCategories.DisplayMember = "Title";
            cmbCategories.ValueMember = "ID"; ;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {            
            iAktion = 1;             // was saved (added)
            this.iProduct_ID = Convert.ToInt32(cmbProducts.SelectedValue);
            this.iCategory_ID = Convert.ToInt32(cmbCategories.SelectedValue);
            this.iStockExchange_ID = Convert.ToInt32(cmbStockExchanges.SelectedValue);
            this.iSettlementProviders_ID = Convert.ToInt32(cmbSettlementProviders.SelectedValue);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            iAktion = 0;             // don't saved (cancelled)
            this.Close();
        }
        public int Aktion { get { return this.iAktion; } set { this.iAktion = value; } }
        public int Product_ID { get { return this.iProduct_ID; } set { this.iProduct_ID = value; } }
        public int Category_ID { get { return this.iCategory_ID; } set { this.iCategory_ID = value; } }
        public int StockExchange_ID { get { return this.iStockExchange_ID; } set { this.iStockExchange_ID = value; } }        
        public string TicketFeesCurr { get { return this.sTicketFeesCurr; } set { this.sTicketFeesCurr = value; } }
        public string MinimumFeesCurr { get { return this.sMinimumFeesCurr; } set { this.sMinimumFeesCurr = value; } }
        public int SettlementProviders_ID { get { return this.iSettlementProviders_ID; } set { this.iSettlementProviders_ID = value; } }
        public int Mode { get { return this.iMode; } set { this.iMode = value; } }
    }
}
