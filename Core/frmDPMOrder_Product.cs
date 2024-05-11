using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using C1.Win.C1FlexGrid;
using Core;

namespace Core
{
    public partial class frmDPMOrder_Product : Form
    {
        DataTable dtList, dtList4, dtEURRates;
        DataView dtView;
        DataColumn dtCol;
        DataRow[] foundRows;

        int i, iID, iDPM_ID, iRec_ID, iDiaxiristis_ID, iProductAktion, iContractAktion, iClient_ID, iContract_ID, iContract_Details_ID, iContract_Packages_ID, 
            iStockCompany_ID, iInvestPolicy_ID, iMiFID_Risk, iMIFIDCategory_ID, iShare_ID, iProduct_ID, iProductCategory_ID, iComplexProduct, iLastAktion = 0;
        string sTemp, sGeography, sProviderTitle, sComplexProduct;
        float sgPrice, sgQuantity, sgAmount, sgCurRate, sgAllocationPercent;
        bool bCheckList;
        Global.ProductData stProduct = new Global.ProductData();
        Global.ContractData stContract = new Global.ContractData();
        clsProductsCodes ProductCode = new clsProductsCodes();
        clsOrders_Recieved Order_Recieved = new clsOrders_Recieved();

        DateTime dToday;
        public frmDPMOrder_Product()
        {
            InitializeComponent();

            bCheckList = false;

            panEdit.Left = 566;
            panEdit.Top = 104;

            panImport.Left = 14;
            panImport.Top = 46;

            panUnSelects.Left = 616;
            panUnSelects.Top = 150;
        }

        private void frmDPMOrder_Product_Load(object sender, EventArgs e)
        {
            //--- dtList4 - table of products that are valid with currenct Contract -------------------------------
            dtList4 = new DataTable("ContractProductsList");
            dtCol = dtList4.Columns.Add("CodeTitle", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("ISIN", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("Product_Title", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("ProductCategory_Title", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("HFCategory_Title", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("SecID", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("Code", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("Code2", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("Currency", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("CreditRating", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("MoodysRating", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("FitchsRating", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("SPRating", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("ICAPRating", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("CountryRisk_Title", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("InvestGeography_ID", System.Type.GetType("System.Int32"));
            dtCol = dtList4.Columns.Add("Date2", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("Maturity", System.Type.GetType("System.Single"));
            dtCol = dtList4.Columns.Add("Maturity_Date", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("CurrencyHedge", System.Type.GetType("System.Int16"));
            dtCol = dtList4.Columns.Add("CurrencyHedge2", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("SurveyedKIID", System.Type.GetType("System.Single"));
            dtCol = dtList4.Columns.Add("SurveyedKIID_Date", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("StockExchange_ID", System.Type.GetType("System.Int32"));
            dtCol = dtList4.Columns.Add("StockExchange_Code", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("Weight", System.Type.GetType("System.Single"));
            dtCol = dtList4.Columns.Add("IR_URL", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("Retail", System.Type.GetType("System.Int16"));
            dtCol = dtList4.Columns.Add("Professional", System.Type.GetType("System.Int16"));
            dtCol = dtList4.Columns.Add("ComplexProduct", System.Type.GetType("System.Int16"));
            dtCol = dtList4.Columns.Add("Distrib_ExecOnly", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("Distrib_Advice", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("Distrib_PortfolioManagment", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("MIFID_Risk", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dtCol = dtList4.Columns.Add("Shares_ID", System.Type.GetType("System.Int32"));
            dtCol = dtList4.Columns.Add("ShareTitles_ID", System.Type.GetType("System.Int32"));
            dtCol = dtList4.Columns.Add("Product_ID", System.Type.GetType("System.Int16"));
            dtCol = dtList4.Columns.Add("ProductCategory_ID", System.Type.GetType("System.Int32"));
            dtCol = dtList4.Columns.Add("OK_Flag", System.Type.GetType("System.Int16"));
            dtCol = dtList4.Columns.Add("OK_String", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("Aktive", System.Type.GetType("System.Int16"));
            dtCol = dtList4.Columns.Add("HFIC_Recom", System.Type.GetType("System.Int16"));

            dAktionDate.Value = dToday;
            txtAction.Text = "";

            ucCS.StartInit(580, 400, 480, 20, 1);
            ucCS.TextOfLabelChanged += new EventHandler(ucCS_TextChanged);
            ucCS.Filters = "Status = 1 AND Service_ID = 3 ";
            ucCS.ListType = 1;

            ucPS.StartInit(650, 350, 200, 20, 1);
            ucPS.TextOfLabelChanged += new EventHandler(ucPS_TextChanged);
            ucPS.Filters = "Aktive >= 1 ";
            ucPS.ListType = 1;                                                                  // iListType = 1 : Global.dtProducts - common list of products
            ucPS.ShowNonAccord = true;                                                          // Don't show NonAccordable products (oxi katallila) with red Background
            ucPS.ShowCancelled = false;                                                         // Don't show cancelled products
            ucPS.ProductsContract = dtList4;
          
            //-------------- Define Products List ------------------
            cmbProducts.DataSource = Global.dtProductTypes.Copy().DefaultView;
            cmbProducts.DisplayMember = "Title";
            cmbProducts.ValueMember = "ID";

            //-------------- Define StockExcahnges2 List ------------------
            cmbStockExchanges.DataSource = Global.dtStockExchanges.Copy();
            cmbStockExchanges.DisplayMember = "Code";
            cmbStockExchanges.ValueMember = "ID";

            //-------------- Define ServiceProviders List ------------------
            dtView = Global.dtServiceProviders.Copy().DefaultView;
            dtView.RowFilter = "ProviderType = 0 OR ProviderType = 1 OR ProviderType = 2";
            cmbServiceProviders.DataSource = dtView;
            cmbServiceProviders.DisplayMember = "Title";
            cmbServiceProviders.ValueMember = "ID";
            cmbServiceProviders.SelectedValue = 0;
    
            //------- fgContracts2 ----------------------------
            fgContracts.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgContracts.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgContracts.DoubleClick += new System.EventHandler(fgContracts_DoubleClick);

            if (iDPM_ID == 0) {
                iProductAktion = 0;                    // 0 - ADD
                EmptyProduct();
                panCode.Enabled = false;
                ucPS.Enabled = false;
                tsbSave.Enabled = false;
                grpContracts.Enabled = false;
                cmbServiceProviders.Focus();
            }
            else  {
                iProductAktion = 1;                    // 1 -EDIT

                clsOrdersDPM OrderDPM = new clsOrdersDPM();
                OrderDPM.Record_ID = iDPM_ID;
                OrderDPM.GetRecord();

                txtAction.Text = Convert.ToInt32(OrderDPM.Aktion) == 1 ? "BUY" : "SELL";
                dAktionDate.Value = Convert.ToDateTime(OrderDPM.AktionDate);
                cmbServiceProviders.SelectedValue = OrderDPM.StockCompany_ID;
                iShare_ID = OrderDPM.ShareCodes_ID;
                lblTitle.Text = OrderDPM.Share_Title;
                lblCode.Text = OrderDPM.Share_Code;
                lnkISIN.Text = OrderDPM.Share_ISIN;
                lblCurrency.Text = OrderDPM.Currency;
                cmbStockExchanges.SelectedValue = OrderDPM.StockExchange_ID;
                lstType.SelectedIndex = OrderDPM.PriceType;
                txtPrice.Text = OrderDPM.Price;
                txtQuantity.Text = OrderDPM.Quantity;
                if (Global.IsNumeric(txtPrice.Text) && Global.IsNumeric(txtQuantity.Text))
                    lblAmount.Text = (Convert.ToDecimal(txtPrice.Text) * Convert.ToDecimal(txtQuantity.Text)).ToString("0.00");
                if (OrderDPM.Product_ID == 2) lblAmount.Text = (Convert.ToSingle(lblAmount.Text) / 100).ToString("0.00");

                cmbConstant.SelectedIndex = OrderDPM.Constant;
                dConstant.Value = OrderDPM.ConstantDate;
                txtNotes.Text = OrderDPM.Notes;

                DefineProductData();

                i = 0;
                fgContracts.Redraw = false;
                fgContracts.Rows.Count = 1;

                clsOrdersDPM_Recs OrdersDPM_Recs = new clsOrdersDPM_Recs();
                OrdersDPM_Recs.DPM_ID = iDPM_ID;
                OrdersDPM_Recs.GetList();
                foreach (DataRow dtRow in OrdersDPM_Recs.List.Rows)
                {
                    i = i + 1;
                    fgContracts.AddItem(i + "\t" + dtRow["ClientFullName"] + "\t" + dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" +
                                            dtRow["Quantity"] + "\t" + dtRow["ID"] + "\t" + dtRow["Client_ID"] + "\t" + dtRow["Contract_ID"] + "\t" +
                                            dtRow["Contract_Details_ID"] + "\t" + dtRow["Contract_Packages_ID"] + "\t" + cmbServiceProviders.SelectedValue);
                }

                fgContracts.Redraw = true;

                DefineContractsSums();              
            }
            bCheckList = true;
        }

        private void picCopy_ISIN_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Convert.IsDBNull(Clipboard.GetText())) Clipboard.SetDataObject(lnkISIN.Text + "", true, 10, 100);
            }
            catch (Exception)
            {
            }
        }
        #region --- Left Area functions ----------------------------------------------------------
        private void dAktionDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void picClose_UnSelects_Click(object sender, EventArgs e)
        {
            panUnSelects.Visible = false;
        }

        private void cmbServiceProviders_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) { 
                if (Convert.ToInt32(cmbServiceProviders.SelectedValue) == 0)
                    MessageBox.Show("Επιλέξτε των πάροχο", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else {
                    panCode.Enabled = true;
                    ucPS.Enabled = true;
                    tsbSave.Enabled = true;
                }
            }
        }
        private void txtAction_TextChanged(object sender, EventArgs e)
        {
            if (txtAction.Text != "") {
                switch (txtAction.Text.Substring(0, 1)) {
                    case "B":
                    case "b":
                    case "Β":
                    case "β":
                    case "A":
                    case "a":
                    case "Α":
                    case "α":
                        txtAction.Text = "BUY";
                        break;
                    case "S":
                    case "s":
                    case "Σ":
                    case "σ":
                    case "ς":
                    case "P":
                    case "p":
                    case "Π":
                    case "π":
                        txtAction.Text = "SELL";
                        break;
                }

                if (txtAction.Text == "BUY") {
                    ShowProductLabels(Convert.ToInt32(cmbProducts.SelectedValue));
                    grpCode.Enabled = true;
                    grpCode.BackColor = Color.MediumAquamarine;
                    ucPS.Focus();
                }
                else  {
                    if (txtAction.Text == "SELL") {
                        ShowProductLabels(Convert.ToInt32(cmbProducts.SelectedValue));
                        grpCode.Enabled = true;
                        grpCode.BackColor = Color.LightCoral;
                        ucPS.Focus();
                    }
                    else {
                        Console.Beep();
                        grpCode.BackColor = Color.Silver;
                        grpCode.Enabled = false;
                        txtAction.Focus();
                    }
                }
            }
        }
        private void picClear_Click(object sender, EventArgs e)
        {
            EmptyProduct();
        }
        private void lstType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPrice.Visible = true;
            txtPrice.Text = "0";

            lblAmount.Visible = true;
            lblAmount.Visible = true;
            lblAmount.Text = "0";

            txtQuantity.Visible = true;
            txtQuantity.Visible = true;
            txtQuantity.Text = "0";

            switch (lstType.SelectedIndex)
            {
                case 0:                                                   // Limit
                    switch (cmbProducts.SelectedValue)
                    {
                        case 1:
                        case 4:
                            if (txtAction.Text == "SELL")
                            {
                                lblAmount.Visible = false;
                            }
                            break;
                        case 2:                                          // Bond
                            if (txtAction.Text == "SELL")
                            {
                                lblAmount.Visible = false;
                            }
                            break;
                    }
                    break;
                case 1:                                                 // Market
                    switch (cmbProducts.SelectedValue)
                    {
                        case 2:                                         // Bond
                            txtPrice.Visible = false;
                            lblAmount.Visible = false;
                            break;
                        case 6:                                         // AK
                            txtPrice.Visible = false;
                            if (txtAction.Text == "BUY")
                            {
                                txtQuantity.Visible = false;
                            }
                            break;
                    }
                    break;
                case 2:                                                 // Stop
                    switch (cmbProducts.SelectedValue)
                    {
                        case 1:
                        case 4:
                            if (txtAction.Text == "SELL")
                            {
                                lblAmount.Visible = false;
                            }
                            break;
                    }
                    break;
                case 3:                                        // Scenario
                    cmbConstant.SelectedIndex = 1;
                    break;
                case 4:
                case 5:                       // ATC, ATO
                    txtPrice.Visible = false;
                    lblAmount.Visible = false;
                    break;
            }
        }
        private void txtPrice_LostFocus(object sender, EventArgs e)
        {
            if (!Global.IsNumeric(txtPrice.Text) || txtPrice.Text.IndexOf(".") > 0) {
                txtPrice.BackColor = Color.Red;
                txtPrice.Focus();
            }
            else {
                txtPrice.BackColor = Color.White;
                DefineNums(1);
            }
        }
        private void txtQuantity_LostFocus(object sender, EventArgs e)
        {
            if (!Global.IsNumeric(txtQuantity.Text) || txtQuantity.Text.IndexOf(".") > 0) {
                txtQuantity.BackColor = Color.Red;
                txtQuantity.Focus();
            }
            else {
                txtQuantity.BackColor = Color.White;
                DefineNums(2);
            }
        }
        private void cmbConstant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConstant.SelectedIndex == 2)
            {
                dConstant.Value = DateTime.Now;
                dConstant.Visible = true;
            }
            else dConstant.Visible = false;
        }
        private void EmptyProduct()
        {
            //dAktionDate.Value = dToday;
            //txtAction.Text = "";
            cmbProducts.SelectedValue = 0;
            ucPS.ShowProductsList = false;
            ucPS.txtShareTitle.Text = "";
            ucPS.ShowProductsList = true;
            lblTitle.Text = "";
            lblCode.Text = "";
            lnkISIN.Text = "";
            lblCurrency.Text = "";
            cmbStockExchanges.SelectedValue = 0;
            cmbConstant.SelectedIndex = 0;
            lstType.SelectedValue = 0;
            txtPrice.Text = "0";
            txtQuantity.Text = "0";
            txtNotes.Text = "";
            lblAmount.Text = "0";
            dConstant.Value = Convert.ToDateTime("1900/01/01");
            iProduct_ID = 0;
            iProductCategory_ID = 0;

            ucPS.Filters = "Aktive >= 1 ";
            ucPS.ListType = 1;                                                                  // iListType = 1 : Global.dtProducts - common list of products
            ucPS.ShowNonAccord = true;                                                         // Don't show NonAccordable products (oxi katallila) with red Background
            ucPS.ShowCancelled = false;                                                         // Don't show cancelled products
            ucPS.ProductsContract = dtList4;
        }
        #endregion
        #region --- Right Area functions ----------------------------------------------------
        private void fgContracts_DoubleClick(object sender, EventArgs e)
        {
            EditContract();
        }
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            iContractAktion = 0;           // 0 - ADD, 1 - EDIT
            EmptyCustomer();
            panEdit.Visible = true;
            ucCS.Filters = "Status = 1 AND Service_ID = 3 AND ServiceProvider_ID = " + cmbServiceProviders.SelectedValue + " AND (Package_DateStart <= '" + dAktionDate.Value + "' AND Package_DateFinish >= '" + dAktionDate.Value + "') ";
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            EditContract();
        }
        private void EditContract()
        {
            if (fgContracts.Row > 0) {
                iContractAktion = 1;           // 0 - ADD, 1 - EDIT

                iContract_ID = Convert.ToInt32(fgContracts[fgContracts.Row, "Contract_ID"]);
                iClient_ID = Convert.ToInt32(fgContracts[fgContracts.Row, "Client_ID"]);

                foundRows = Global.dtContracts.Select("Contract_ID = " + iContract_ID + " AND Client_ID = " + iClient_ID);
                if (foundRows.Length > 0) {
                    ucCS.ShowClientsList = false;
                    ucCS.txtContractTitle.Text = foundRows[0]["Fullname"] + "";
                    ucCS.ShowClientsList = true;
                    lblContractTitle.Text = foundRows[0]["ContractTitle"] + "";
                    lblClientCode.Text = foundRows[0]["Code"] + "";
                    lblPortfolio.Text = foundRows[0]["Portfolio"] + "";
                    lblCurr.Text = foundRows[0]["Currency"] + "";

                    lblEP.Text = foundRows[0]["InvestmentPolicy_Title"] + "";
                    lblEProfile.Text = foundRows[0]["InvestmentProfile_Title"] + "";
                    lblService.Text = foundRows[0]["Service_Title"] + "";
                    lblEMail.Text = foundRows[0]["EMail"] + "";
                    lblMobile.Text = foundRows[0]["Mobile"] + "";
                    chkXAA.Checked = Convert.ToInt32(foundRows[0]["XAA"]) == 1 ? true : false;

                    iContract_Details_ID = Convert.ToInt32(foundRows[0]["Contracts_Details_ID"]);
                    iContract_Packages_ID = Convert.ToInt32(foundRows[0]["Contracts_Packages_ID"]);
                    iStockCompany_ID = Convert.ToInt32(foundRows[0]["ServiceProvider_ID"]);
                    iInvestPolicy_ID = Convert.ToInt32(foundRows[0]["InvestmentPolicy_ID"]);
                    sProviderTitle = foundRows[0]["ServiceProvider_Title"] + "";
                    iMIFIDCategory_ID = Convert.ToInt32(foundRows[0]["MIFIDCategory_ID"]);
                    iMiFID_Risk = Convert.ToInt32(foundRows[0]["MIFID_Risk_Index"]);
                };

                txtQuantity_Contract.Text = fgContracts[fgContracts.Row, "Quantity"] + "";

                panEdit.Visible = true;
                ucCS.Filters = "Status = 1 AND Service_ID = 3 AND ServiceProvider_ID = " + cmbServiceProviders.SelectedValue + " AND (Package_DateStart <= '" + dAktionDate.Value + "' AND Package_DateFinish >= '" + dAktionDate.Value + "') ";
            }
        }

        private void tsbDel_Click(object sender, EventArgs e)
        {
            if (fgContracts.Row > 0)
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;",
                    Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)  {

                    clsSystem System = new clsSystem();
                    System.Table = "Commands";
                    System.Record_ID = Convert.ToInt32(fgContracts[fgContracts.Row, "ID"]);
                    System.DeleteRecord();

                    fgContracts.RemoveItem(fgContracts.Row);
                    DefineContractsSums();
                }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (iContractAktion == 0)
            {
                i = fgContracts.Rows.Count - 1;
                i = i + 1;
                fgContracts.AddItem(i + "\t" + lblContractTitle.Text + "\t" + ucCS.txtContractTitle.Text + "\t" + lblClientCode.Text + "\t" + lblPortfolio.Text + "\t" +
                                     txtQuantity_Contract.Text + "\t" + "0" + "\t" + iClient_ID + "\t" + iContract_ID + "\t" + iContract_Details_ID + "\t" +
                                     iContract_Packages_ID + "\t" + iStockCompany_ID);
                fgContracts.Redraw = true;

            }
            else {
                i = fgContracts.Row;
                fgContracts[i, "ClientName"] = ucCS.txtContractTitle.Text;
                fgContracts[i, "ContractTitle"] = lblContractTitle.Text;
                fgContracts[i, "Code"] = lblCode.Text;
                fgContracts[i, "Portfolio"] = lblPortfolio.Text;
                fgContracts[i, "Quantity"] = txtQuantity_Contract.Text;
                fgContracts[i, "Client_ID"] = iClient_ID;
                fgContracts[i, "Contract_ID"] = iContract_ID;
                fgContracts[i, "Contracts_Details_ID"] = iContract_Details_ID;
                fgContracts[i, "Contracts_Packages_ID"] = iContract_Packages_ID;
                fgContracts[i, "ServiceProvider_ID"] = iStockCompany_ID;
                fgContracts.Redraw = true;
            }
            DefineContractsSums();
            panEdit.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panEdit.Visible = false;
        }
        private void picClear_Customer_Click(object sender, EventArgs e)
        {
            EmptyCustomer();
        }
        private void tsbExcel_Click(object sender, EventArgs e)
        {
            txtFilePath.Text = "";
            btnGetImport.Enabled = false;
            panImport.Visible = true;
        }
        private void picFilesPath_Click(object sender, EventArgs e)
        {
            txtFilePath.Text = Global.FileChoice(Global.DefaultFolder);
            btnGetImport.Enabled = true;
        }

        private void btnGetImport_Click(object sender, EventArgs e)
        {
            int i = 0;              // i - counter of rows from input file
            int j = 0;              // j - counter of rows into fgContracts
            int k = 0;              // k - counter of rows into fgUnSelects

            fgUnSelects.Rows.Count = 1;

            var ExApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = ExApp.Workbooks.Open(txtFilePath.Text);
            Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            clsContract_Blocks klsContract_Blocks = new clsContract_Blocks();

            while (true)
            {
                i = i + 1;

                sTemp = (xlRange.Cells[i, 2].Value + "").ToString();
                if (sTemp == "") break;


                clsContracts klsContract = new clsContracts();
                klsContract.Code = xlRange.Cells[i, 2].Value + "";
                klsContract.Portfolio = xlRange.Cells[i, 3].Value + "";
                klsContract.GetRecord_Code_Portfolio();

                klsContract_Blocks = new clsContract_Blocks();
                klsContract_Blocks.Contract_ID = klsContract.Record_ID;
                klsContract_Blocks.Record_ID = 0;
                klsContract_Blocks.GetRecord_Contract();
                if (klsContract_Blocks.Record_ID == 0) {

                    stContract = new Global.ContractData();                    
                    stContract.Contract_ID = klsContract.Record_ID;
                    stContract.Contracts_Details_ID = klsContract.Contract_Details_ID;
                    stContract.Contracts_Packages_ID = klsContract.Contract_Packages_ID;
                    iClient_ID = klsContract.Client_ID;

                    iContract_ID = stContract.Contract_ID;
                    iContract_Details_ID = stContract.Contracts_Details_ID;
                    iContract_Packages_ID = stContract.Contracts_Packages_ID;

                    foundRows = Global.dtContracts.Select("Contract_ID = " + iContract_ID + " AND Client_ID = " + iClient_ID);
                    if (foundRows.Length > 0) {
                        stContract.MIFIDCategory_ID = Convert.ToInt32(foundRows[0]["MIFIDCategory_ID"]);
                        stContract.MIFID_Risk_Index = Convert.ToInt32(foundRows[0]["MIFID_Risk_Index"]);
                    }

                    sComplexProduct = "";
                    if (klsContract.Details.ChkComplex == 1)
                    {
                        clsContracts_ComplexSigns klsContracts_ComplexSigns = new clsContracts_ComplexSigns();
                        klsContracts_ComplexSigns.Contract_ID = iContract_ID;
                        klsContracts_ComplexSigns.GetList();
                        foreach (DataRow dtRow1 in klsContracts_ComplexSigns.List.Rows)
                            sComplexProduct = sComplexProduct + "," + dtRow1["ComplexSign_ID"];

                        if (sComplexProduct.Length > 0) sComplexProduct = sComplexProduct + ",";

                        chkWorld.Checked = (klsContract.Details.ChkWorld == 1 ? true : false); ;
                        chkGreece.Checked = (klsContract.Details.ChkGreece == 1 ? true : false); ;
                        chkEurope.Checked = (klsContract.Details.ChkEurope == 1 ? true : false); ;
                        chkAmerica.Checked = (klsContract.Details.ChkAmerica == 1 ? true : false); ;
                        chkAsia.Checked = (klsContract.Details.ChkAsia == 1 ? true : false); ;
                    }
                    stContract.ComplexProduct = sComplexProduct;

                    stContract.Geography = (klsContract.Details.ChkWorld == 1 ? "1" : "0") + (klsContract.Details.ChkGreece == 1 ? "1" : "0") + (klsContract.Details.ChkEurope == 1 ? "1" : "0") +
                    (klsContract.Details.ChkAmerica == 1 ? "1" : "0") + (klsContract.Details.ChkAsia == 1 ? "1" : "0");

                    stContract.SpecRules = (klsContract.Details.ChkSpecificConstraints == 1 ? "1" : "0") + (klsContract.Details.ChkMonetaryRisk == 1 ? "1" : "0") + (klsContract.Details.ChkIndividualBonds == 1 ? "1" : "0") +
                             (klsContract.Details.ChkMutualFunds == 1 ? "1" : "0") + (klsContract.Details.ChkBondedETFs == 1 ? "1" : "0") + (klsContract.Details.ChkIndividualShares == 1 ? "1" : "0") +
                             (klsContract.Details.ChkMixedFunds == 1 ? "1" : "0") + (klsContract.Details.ChkMixedETFs == 1 ? "1" : "0") + (klsContract.Details.ChkFunds == 1 ? "1" : "0") +
                             (klsContract.Details.ChkETFs == 1 ? "1" : "0") + (klsContract.Details.ChkInvestmentGrade == 1 ? "1" : "0");

                    foundRows = Global.dtProducts.Select("ID = " + iShare_ID);
                    if (foundRows.Length > 0)
                    {
                        stProduct.InvestGeography_ID = Convert.ToInt32(foundRows[0]["InvestGeography_ID"]);
                    }

                    if (Global.AccordanceContractProduct(stContract, stProduct, out int iOK_Flag, out string sOK_String))  {
                        j = fgContracts.Rows.Count;
                        fgContracts.AddItem(j + "\t" + klsContract.ClientName + "\t" + xlRange.Cells[i, 1].Value + "\t" + xlRange.Cells[i, 2].Value + "\t" + xlRange.Cells[i, 3].Value + "\t" +
                                           xlRange.Cells[i, 4].Value + "\t" + "0" + "\t" + klsContract.Client_ID + "\t" + klsContract.Record_ID + "\t" +
                                           klsContract.Contract_Details_ID + "\t" + klsContract.Contract_Packages_ID + "\t" + klsContract.BrokerageServiceProvider_ID);
                    }
                    else  {
                        k = fgUnSelects.Rows.Count;
                        fgUnSelects.AddItem(k + "\t" + xlRange.Cells[i, 1].Value + "\t" + xlRange.Cells[i, 2].Value + "\t" + xlRange.Cells[i, 3].Value + "\t" + "Δεν είναι κατάλληλο το προϊόν");
                    }
                }
                else  {
                    k = fgUnSelects.Rows.Count;
                    fgUnSelects.AddItem(k + "\t" + xlRange.Cells[i, 1].Value + "\t" + xlRange.Cells[i, 2].Value + "\t" + xlRange.Cells[i, 3].Value + "\t" + "Σύμβαση είναι μπλοκαρισμένη");
                }
            }

            xlWorkbook.Close(true);
            ExApp.Quit();

            while (Marshal.ReleaseComObject(xlWorksheet) != 0) ;
            while (Marshal.ReleaseComObject(xlWorkbook) != 0) ;
            while (Marshal.ReleaseComObject(ExApp) != 0) ;

            GC.Collect();
            GC.WaitForPendingFinalizers();

            panImport.Visible = false;
            this.Cursor = Cursors.Default;

            DefineContractsSums();

            if (fgUnSelects.Rows.Count > 1) panUnSelects.Visible = true;
        }

        private void btnCancelImport_Click(object sender, EventArgs e)
        {
            panImport.Visible = false;
        }
        private void DefineContractsSums()
        {
            sgQuantity = 0;
            for (i = 1; i <= fgContracts.Rows.Count - 1; i++)
                sgQuantity = sgQuantity + Convert.ToSingle(fgContracts[i, "Quantity"]);
            lblSumQuantity.Text = sgQuantity.ToString("0.00");
        }
        private void EmptyCustomer()
        {
            ucCS.ShowClientsList = false;
            ucCS.txtContractTitle.Text = "";
            ucCS.ShowClientsList = true;
            lblContractTitle.Text = "";
            lblClientCode.Text = "";
            lblPortfolio.Text = "";
            lblEP.Text = "";
            //lblEProfile.Text = "";
            lblService.Text = "";
            lblEMail.Text = "";
            lblMobile.Text = "";
            chkXAA.Checked = false;
            lblAUM.Text = "0";
            lblCurr.Text = "";
            iClient_ID = 0;
            iContract_ID = 0;
            iContract_Details_ID = 0;
            iContract_Packages_ID = 0;
            txtQuantity_Contract.Text = "0";
        }
        #endregion
        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(lblSumQuantity.Text) != 0 && Convert.ToDecimal(txtQuantity.Text) != Convert.ToDecimal(lblSumQuantity.Text))
                MessageBox.Show("Ποσότητα δεν είναι ίσο με Συνολική Ποσότητα απο το Allocation", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); ;



            //--- define currency rate -------------------------------
            sgCurRate = 0;
            if (dAktionDate.Value.Date == DateTime.Now.Date) dtEURRates = Global.dtTodayEURRates.Copy();
            else
            {
                clsCurrencies klsCurrency = new clsCurrencies();
                klsCurrency.DateFrom = dAktionDate.Value.AddDays(-1);
                klsCurrency.DateTo = dAktionDate.Value.AddDays(-1);
                klsCurrency.Code = "EUR";
                klsCurrency.GetCurrencyRates_Period();
                dtEURRates = klsCurrency.List.Copy();
            }
            if (lblCurrency.Text == "EUR") sgCurRate = 1;                                                                    // CurrRate
            else  {
                foundRows = dtEURRates.Select("Currency = 'EUR" + lblCurrency.Text + "='");
                if (foundRows.Length > 0) sgCurRate = Convert.ToSingle(foundRows[0]["Rate"]);                                // CurrRate
            }

            //--- define Action in text format ----------------------------
            if (txtAction.Text == "BUY") sTemp = "Αγορά: ";
            else sTemp = "Πώληση: ";

            //--- define sgAllocationPercent ------------------------------
            sgAllocationPercent = 0;
            if (Global.IsNumeric(lblSumQuantity.Text) && Global.IsNumeric(txtQuantity.Text))
            {
                if (Convert.ToSingle(txtQuantity.Text) != 0)
                   sgAllocationPercent = Convert.ToSingle(lblSumQuantity.Text) * 100 / Convert.ToSingle(txtQuantity.Text);
            }

            if (iProductAktion == 0) {
                clsOrdersDPM OrdersDPM = new clsOrdersDPM();
                OrdersDPM.OrderType = 2;                                                                                    // DPMOrder by Product
                OrdersDPM.Client_ID = 0;
                OrdersDPM.Contract_ID = 0;
                OrdersDPM.Contract_Details_ID = 0;
                OrdersDPM.Contract_Packages_ID = 0;
                OrdersDPM.AUM = 0;
                OrdersDPM.AllocationPercent = sgAllocationPercent;
                OrdersDPM.StockCompany_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                OrdersDPM.Aktion = txtAction.Text == "BUY" ? 1 : 2;
                OrdersDPM.AktionDate = dAktionDate.Value;
                OrdersDPM.ShareCodes_ID = iShare_ID;                
                OrdersDPM.ProductsCount = 1;
                OrdersDPM.Products = sTemp + lblTitle.Text + ", Τιμη: " + txtPrice.Text + ", Ποσότητα: " + txtQuantity.Text;
                OrdersDPM.PriceType = lstType.SelectedIndex;
                OrdersDPM.Price = txtPrice.Text;
                OrdersDPM.Quantity = txtQuantity.Text;
                OrdersDPM.Constant = cmbConstant.SelectedIndex;
                OrdersDPM.ConstantDate = Convert.ToDateTime(dConstant.Value);
                OrdersDPM.SentDate = Convert.ToDateTime("1900/01/01");
                OrdersDPM.Notes = txtNotes.Text;
                OrdersDPM.Status = 0;
                OrdersDPM.User_ID = iDiaxiristis_ID;
                OrdersDPM.Author_ID = Global.User_ID;
                iDPM_ID = OrdersDPM.InsertRecord(); 
            }
            else {
                clsOrdersDPM OrdersDPM = new clsOrdersDPM();
                OrdersDPM.Record_ID = iDPM_ID;
                OrdersDPM.GetRecord();
                OrdersDPM.AllocationPercent = sgAllocationPercent;
                OrdersDPM.StockCompany_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                OrdersDPM.Aktion = txtAction.Text == "BUY" ? 1 : 2;
                OrdersDPM.AktionDate = dAktionDate.Value;
                OrdersDPM.ShareCodes_ID = iShare_ID;
                OrdersDPM.Products = sTemp + lblTitle.Text + ", Τιμη: " + txtPrice.Text + ", Ποσότητα: " + txtQuantity.Text;
                OrdersDPM.PriceType = lstType.SelectedIndex;
                OrdersDPM.Price = txtPrice.Text;
                OrdersDPM.Quantity = txtQuantity.Text;
                OrdersDPM.Constant = cmbConstant.SelectedIndex;
                OrdersDPM.ConstantDate = Convert.ToDateTime(dConstant.Value);
                OrdersDPM.Notes = txtNotes.Text;
                OrdersDPM.EditRecord();                
            }

            SaveContracts();

            iLastAktion = 1;
            this.Close();
        }
        private void SaveContracts()
        {
            clsOrdersDPM_Recs OrdersDPM_Recs = new clsOrdersDPM_Recs();
            for (i = 1; i <= fgContracts.Rows.Count - 1; i++) {
                if (Convert.ToInt32(fgContracts[i, "ID"]) == 0) {

                    OrdersDPM_Recs = new clsOrdersDPM_Recs();
                    if (Convert.ToInt32(fgContracts[i, "ID"]) != 0)
                    {
                        OrdersDPM_Recs.Record_ID = Convert.ToInt32(fgContracts[i, "ID"]);
                        OrdersDPM_Recs.GetRecord();
                    }
                    OrdersDPM_Recs.DPM_ID = iDPM_ID;
                    OrdersDPM_Recs.Client_ID = Convert.ToInt32(fgContracts[i, "Client_ID"]);
                    OrdersDPM_Recs.Contract_ID = Convert.ToInt32(fgContracts[i, "Contract_ID"]);
                    OrdersDPM_Recs.Contract_Details_ID = Convert.ToInt32(fgContracts[i, "Contracts_Details_ID"]);
                    OrdersDPM_Recs.Contract_Packages_ID = Convert.ToInt32(fgContracts[i, "Contracts_Packages_ID"]);
                    OrdersDPM_Recs.ShareCodes_ID = iShare_ID;
                    OrdersDPM_Recs.Product_ID = Convert.ToInt32(cmbProducts.SelectedValue);
                    OrdersDPM_Recs.ProductCategories_ID = iProductCategory_ID;
                    OrdersDPM_Recs.Currency = lblCurrency.Text;
                    OrdersDPM_Recs.StockExchange_ID = Convert.ToInt32(cmbStockExchanges.SelectedValue);
                    OrdersDPM_Recs.Aktion = txtAction.Text == "BUY" ? 1 : 2;
                    OrdersDPM_Recs.Constant = cmbConstant.SelectedIndex;
                    OrdersDPM_Recs.ConstantDate = Convert.ToDateTime(dConstant.Value).ToString("dd/MM/yyyy"); 
                    OrdersDPM_Recs.PriceType = lstType.SelectedIndex;
                    OrdersDPM_Recs.Price = txtPrice.Text;
                    OrdersDPM_Recs.PriceUp = "0";
                    OrdersDPM_Recs.PriceDown = "0";
                    OrdersDPM_Recs.Quantity = fgContracts[i, "Quantity"] + "";
                    OrdersDPM_Recs.Amount = (Convert.ToDecimal(txtPrice.Text) * Convert.ToDecimal(fgContracts[i, "Quantity"])).ToString("0.00");
                    OrdersDPM_Recs.TargetPrice = "";
                    OrdersDPM_Recs.CurrRate_NA = "";
                    OrdersDPM_Recs.Amount_NA = "";
                    OrdersDPM_Recs.Weight = "";
                    OrdersDPM_Recs.Status = 1;

                    if (Convert.ToInt32(fgContracts[i, "ID"]) != 0) OrdersDPM_Recs.EditRecord();
                    else iRec_ID = OrdersDPM_Recs.InsertRecord();
                }
            }
        }  
 
        private void DefineNums(int iField)
        {
            if (Convert.ToInt32(lstType.SelectedValue) != 1) {
                if (Global.IsNumeric(txtPrice.Text))  {
                    sgPrice = Convert.ToSingle(txtPrice.Text);
                    sgQuantity = (Global.IsNumeric(txtQuantity.Text) ? Convert.ToSingle(txtQuantity.Text) : 0);
                    sgAmount = (Global.IsNumeric(lblAmount.Text) ? Convert.ToSingle(lblAmount.Text) : 0);

                    if (iField == 1 || iField == 2) {
                        lblAmount.Text = (sgPrice * sgQuantity).ToString("0.00");

                        if (iProduct_ID == 2) lblAmount.Text = (Convert.ToSingle(lblAmount.Text) / 100).ToString("0.00");
                    }
                    else {
                        if (sgQuantity == 0) {
                            if (sgPrice != 0) txtQuantity.Text = Math.Round(sgAmount / sgPrice).ToString("0.00");
                            else txtQuantity.Text = "0";
                        }
                    }
                }
                else txtQuantity.Text = "0";
            }
        }
        private void ShowProductLabels(int iProductType)
        {
            //lstType.DisplayMember = "Title";
            //lstType.ValueMember = "ID";
            dtList = new DataTable("TypeList");
            dtList.Columns.Add("Title", typeof(string));
            dtList.Columns.Add("ID", typeof(int));

            switch (iProductType) {
                case 0:
                    //lblQuantity.Text = "Ποσότητα";
                    break;
                case 1:                                                  // 1-Shares                                     
                    //lblQuantity.Text = "Τεμάχια";
                    if (txtAction.Text == "BUY") {
                        dtList.Rows.Add("Limit", 0);
                        dtList.Rows.Add("Scenario", 3);
                        dtList.Rows.Add("ATC", 4);
                        dtList.Rows.Add("ATO", 5);
                    }
                    else
                    {
                        dtList.Rows.Add("Limit", 0);
                        dtList.Rows.Add("Stop", 2);
                        dtList.Rows.Add("ATC", 4);
                        dtList.Rows.Add("ATO", 5);
                    }
                    break;
                case 2:                                           // 2 - Bond
                    //lblQuantity.Text = "Ονομαστική Αξία";
                    dtList.Rows.Add("Limit", 0);
                    dtList.Rows.Add("Market", 1);
                    break;
                case 4:                                           //    4 - ETF                                     
                    //lblQuantity.Text = "Τεμάχια";
                    if (txtAction.Text == "BUY")
                    {
                        dtList.Rows.Add("Limit", 0);
                        dtList.Rows.Add("Scenario", 3);
                        dtList.Rows.Add("ATC", 4);
                        dtList.Rows.Add("ATO", 5);
                    }
                    else
                    {
                        dtList.Rows.Add("Limit", 0);
                        dtList.Rows.Add("Stop", 2);
                        dtList.Rows.Add("ATC", 4);
                        dtList.Rows.Add("ATO", 5);
                    }
                    break;
                case 6:                                           // Fund
                    //lblQuantity.Text = "Μερίδια";
                    dtList.Rows.Add("Market", 1);
                    break;
                default:
                    dtList.Rows.Add("Limit", 0);
                    dtList.Rows.Add("Market", 1);
                    dtList.Rows.Add("Stop", 2);
                    dtList.Rows.Add("Scenario", 3);
                    dtList.Rows.Add("ATC", 4);
                    dtList.Rows.Add("ATO", 5);
                    break;
            }
            //lstType.DataSource = dtList;
        }
        protected void ucCS_TextChanged(object sender, EventArgs e)
        {
            sComplexProduct = "";

            stContract = new Global.ContractData();
            stContract = ucCS.SelectedContractData;
            if (stContract.MIFID_2 == 1)  {
                iContract_ID = stContract.Contract_ID;
                iContract_Details_ID = stContract.Contracts_Details_ID;
                iContract_Packages_ID = stContract.Contracts_Packages_ID;

                clsContracts klsContract = new clsContracts();
                klsContract.Record_ID = iContract_ID;
                klsContract.Contract_Details_ID = iContract_Details_ID;
                klsContract.Contract_Packages_ID = iContract_Packages_ID;
                klsContract.GetRecord();

                sComplexProduct = "";
                if (klsContract.Details.ChkComplex == 1) {
                    clsContracts_ComplexSigns klsContracts_ComplexSigns = new clsContracts_ComplexSigns();
                    klsContracts_ComplexSigns.Contract_ID = iContract_ID;
                    klsContracts_ComplexSigns.GetList();
                    foreach (DataRow dtRow1 in klsContracts_ComplexSigns.List.Rows)
                        sComplexProduct = sComplexProduct + "," + dtRow1["ComplexSign_ID"];

                    if (sComplexProduct.Length > 0) sComplexProduct = sComplexProduct + ",";

                    chkWorld.Checked = (klsContract.Details.ChkWorld == 1 ? true : false); ;
                    chkGreece.Checked = (klsContract.Details.ChkGreece == 1 ? true : false); ;
                    chkEurope.Checked = (klsContract.Details.ChkEurope == 1 ? true : false); ;
                    chkAmerica.Checked = (klsContract.Details.ChkAmerica == 1 ? true : false); ;
                    chkAsia.Checked = (klsContract.Details.ChkAsia == 1 ? true : false); ;
                }
                stContract.ComplexProduct = sComplexProduct;

                stContract.Geography = (klsContract.Details.ChkWorld == 1 ? "1" : "0") + (klsContract.Details.ChkGreece == 1 ? "1" : "0") + (klsContract.Details.ChkEurope == 1 ? "1" : "0") +
                (klsContract.Details.ChkAmerica == 1 ? "1" : "0") + (klsContract.Details.ChkAsia == 1 ? "1" : "0");

                stContract.SpecRules = (klsContract.Details.ChkSpecificConstraints == 1 ? "1" : "0") + (klsContract.Details.ChkMonetaryRisk == 1 ? "1" : "0") + (klsContract.Details.ChkIndividualBonds == 1 ? "1" : "0") +
                         (klsContract.Details.ChkMutualFunds == 1 ? "1" : "0") + (klsContract.Details.ChkBondedETFs == 1 ? "1" : "0") + (klsContract.Details.ChkIndividualShares == 1 ? "1" : "0") +
                         (klsContract.Details.ChkMixedFunds == 1 ? "1" : "0") + (klsContract.Details.ChkMixedETFs == 1 ? "1" : "0") + (klsContract.Details.ChkFunds == 1 ? "1" : "0") +
                         (klsContract.Details.ChkETFs == 1 ? "1" : "0") + (klsContract.Details.ChkInvestmentGrade == 1 ? "1" : "0");

                if (txtAction.Text == "BUY") {
                    if (Global.AccordanceContractProduct(stContract, stProduct, out int iOK_Flag, out string sOK_String)) {

                        lblContractTitle.Text = stContract.ClientName;
                        lblClientCode.Text = stContract.Code;
                        lblPortfolio.Text = stContract.Portfolio;
                        lblEP.Text = stContract.Policy_Title;
                        //lblEProfile.Text = stContract.Profile_Title;
                        lblService.Text = stContract.Service_Title;
                        lblEMail.Text = stContract.EMail;
                        lblMobile.Text = stContract.Mobile;
                        chkXAA.Checked = stContract.XAA == 1 ? true : false;
                        lblAUM.Text = "0";               //stContract.AUMs;
                        lblCurr.Text = stContract.Currency;
                        iClient_ID = stContract.Client_ID;
                        iContract_ID = stContract.Contract_ID;
                        iContract_Details_ID = stContract.Contracts_Details_ID;
                        iContract_Packages_ID = stContract.Contracts_Packages_ID;
                        iStockCompany_ID = stContract.Provider_ID;
                        iInvestPolicy_ID = stContract.Policy_ID;
                        sProviderTitle = stContract.Provider_Title;
                        iMIFIDCategory_ID = stContract.MIFIDCategory_ID;
                        iMiFID_Risk = stContract.MIFID_Risk_Index;
                        if (stContract.Service_ID == 5) lblInvestPolicy2.Text = "Χρημα/τικά μέσα";       // 5 - DealAdvisory
                        else lblInvestPolicy2.Text = "Επενδ. Πολιτική";                                  // Else - Advisory                

                        sGeography = DefineContractGeography(iContract_ID);

                        DefineComplexProduct();

                        txtQuantity_Contract.Focus();
                    }
                    else {
                        MessageBox.Show("Δεν είναι κατάλληλο προϊόν για την επιλεγμένη σύμβαση", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        ucCS.ShowClientsList = false;
                        ucCS.txtContractTitle.Text = "";
                        ucCS.ShowClientsList = true;
                        ucCS.txtContractTitle.Focus();
                    }
                }
                else  {  // --- SELL
                   
                    lblContractTitle.Text = stContract.ClientName;
                    lblClientCode.Text = stContract.Code;
                    lblPortfolio.Text = stContract.Portfolio;
                    lblEP.Text = stContract.Policy_Title;
                    //lblEProfile.Text = stContract.Profile_Title;
                    lblService.Text = stContract.Service_Title;
                    lblEMail.Text = stContract.EMail;
                    lblMobile.Text = stContract.Mobile;
                    chkXAA.Checked = stContract.XAA == 1 ? true : false;
                    lblAUM.Text = "0";               //stContract.AUMs;
                    lblCurr.Text = stContract.Currency;
                    iClient_ID = stContract.Client_ID;
                    iContract_ID = stContract.Contract_ID;
                    iContract_Details_ID = stContract.Contracts_Details_ID;
                    iContract_Packages_ID = stContract.Contracts_Packages_ID;
                    iStockCompany_ID = stContract.Provider_ID;
                    iInvestPolicy_ID = stContract.Policy_ID;
                    sProviderTitle = stContract.Provider_Title;
                    iMIFIDCategory_ID = stContract.MIFIDCategory_ID;
                    iMiFID_Risk = stContract.MIFID_Risk_Index;
                    if (stContract.Service_ID == 5) lblInvestPolicy2.Text = "Χρημα/τικά μέσα";       // 5 - DealAdvisory
                    else lblInvestPolicy2.Text = "Επενδ. Πολιτική";                                  // Else - Advisory                

                    sGeography = DefineContractGeography(iContract_ID);                    
                }
            }
            else  {
                MessageBox.Show("Δεν είναι MIFID II Σύμβαση.", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ucCS.ShowClientsList = false;
                ucCS.txtContractTitle.Text = "";
                ucCS.ShowClientsList = true;
                ucCS.txtContractTitle.Focus();
            }
        }
        protected void ucPS_TextChanged(object sender, EventArgs e)
        {
            stProduct = new Global.ProductData();
            stProduct = ucPS.SelectedProductData;
            if (txtAction.Text == "SELL" || stProduct.HFIC_Recom == 1)
            {

                lnkISIN.Text = stProduct.ISIN;
                iShare_ID = stProduct.ShareCode_ID;
                lstType.SelectedIndex = 0;
                ShowProductLabels(stProduct.Product_ID);
                if (lnkISIN.Text != "") DefineProductData();

                grpContracts.Enabled = true;
                ucPS.ListType = 2;

                txtQuantity.Focus();
            }
            else {
                ucPS.ShowProductsList = false;
                ucPS.txtShareTitle.Text = "";
                ucPS.ShowProductsList = true;
                MessageBox.Show("Non recommended product", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void DefineProductData()
        {
            ProductCode = new clsProductsCodes();
            ProductCode.Record_ID = iShare_ID;
            ProductCode.GetRecord();
            //ProductCode.ISIN = lnkISIN.Text;
            //ProductCode.SecID = "";
            //ProductCode.GetRecord_ISIN();
            stProduct.ShareCode_ID = ProductCode.Record_ID;
            stProduct.Product_ID = ProductCode.Product_ID;
            stProduct.ProductCategory_ID = ProductCode.ProductCategory_ID;
            stProduct.StockExchange_ID = ProductCode.StockExchange_ID;
            stProduct.Title = ProductCode.CodeTitle;
            stProduct.Code = ProductCode.Code;
            stProduct.ISIN = ProductCode.ISIN;
            stProduct.Currency = ProductCode.Curr;
            stProduct.MIFID_Risk = ProductCode.MIFID_Risk;
            stProduct.Retail = ProductCode.InvestType_Retail;
            stProduct.Professional = ProductCode.InvestType_Prof;
            stProduct.Distrib_ExecOnly = ProductCode.Distrib_ExecOnly;
            stProduct.Distrib_Advice = ProductCode.Distrib_Advice;
            stProduct.Distrib_PortfolioManagment = ProductCode.Distrib_PortfolioManagment;
            stProduct.RiskCurr = ProductCode.RiskCurr;
            stProduct.CurrencyHedge2 = ProductCode.CurrencyHedge2;
            stProduct.ComplexProduct = ProductCode.ComplexProduct;
            stProduct.ComplexReasonsList = ProductCode.ComplexReasonsList;
            stProduct.Rank_Title = ProductCode.Rank_Title;
            stProduct.IsCallable = ProductCode.IsCallable;
            stProduct.IsPutable = ProductCode.IsPutable;
            stProduct.IsConvertible = ProductCode.IsConvertible;
            stProduct.IsPerpetualSecurity = ProductCode.IsPerpetualSecurity;
            stProduct.ComplexAttribute = ProductCode.ComplexAttribute;
            stProduct.Leverage = ProductCode.Leverage;
            stProduct.MiFIDInstrumentType = ProductCode.MiFIDInstrumentType;
            stProduct.AIFMD = ProductCode.AIFMD;
            stProduct.GlobalBroadCategory_Title = ProductCode.Rank_Title;
            stProduct.InvestGeography_ID = ProductCode.InvestGeography_ID;
            stProduct.RatingGroup = ProductCode.RatingGroup;
            lblCurrency.Text = stProduct.Currency;

            lblTitle.Text = stProduct.Title;
            lblCode.Text = stProduct.Code;
            lnkISIN.Text = stProduct.ISIN;

            ProductCode = new clsProductsCodes();
            ProductCode.DateIns = dAktionDate.Value;
            ProductCode.ISIN = stProduct.ISIN;
            ProductCode.Curr = stProduct.Currency;
            ProductCode.GetPrice_ISIN();
            if (txtPrice.Text == "" || txtPrice.Text == "0")
                txtPrice.Text = ProductCode.LastClosePrice + "";

            iShare_ID = stProduct.ShareCode_ID;
            cmbStockExchanges.SelectedValue = stProduct.StockExchange_ID;
            iProduct_ID = stProduct.Product_ID;
            cmbProducts.SelectedValue = iProduct_ID;
            iShare_ID = stProduct.ShareCode_ID;
            iProductCategory_ID = stProduct.ProductCategory_ID;
        }
        private string DefineContractGeography(int iContract_ID)
        {
            string sTemp = "";
            clsContracts klsContract = new clsContracts();
            klsContract.Record_ID = iContract_ID;
            klsContract.GetRecord();
            sTemp = (klsContract.Details.ChkWorld == 1 ? "1" : "0") + (klsContract.Details.ChkGreece == 1 ? "1" : "0") + (klsContract.Details.ChkEurope == 1 ? "1" : "0") +
                    (klsContract.Details.ChkAmerica == 1 ? "1" : "0") + (klsContract.Details.ChkAsia == 1 ? "1" : "0");

            return sTemp;
        }
        private void DefineComplexProduct()
        {
            iComplexProduct = 0;
            clsContracts_ComplexSigns klsContracts_ComplexSigns = new clsContracts_ComplexSigns();
            klsContracts_ComplexSigns.Contract_ID = iContract_ID;
            klsContracts_ComplexSigns.GetList();
            foreach (DataRow dtRow in klsContracts_ComplexSigns.List.Rows)
            {
                if (Convert.ToInt32(dtRow["ComplexSign_ID"]) == 2)
                {
                    iComplexProduct = 1;
                }
            }
        }
        public DateTime Today { get { return dToday; } set { dToday = value; } }
        public int DPM_ID { get { return iDPM_ID; } set { iDPM_ID = value; } }
        public int LastAktion { get { return iLastAktion; } set { iLastAktion = value; } }
        public int Diaxiristis_ID { get { return iDiaxiristis_ID; } set { iDiaxiristis_ID = value; } }

    }
}
