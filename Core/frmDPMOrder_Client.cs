using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Core;

namespace Core
{
    public partial class frmDPMOrder_Client : Form
    {
        DataTable dtList, dtList4, dtEURRates;
        DataColumn dtCol;
        DataRow dtRow;
        DataRow[] foundRows;

        int i, iID, iRec_ID, iDPM_ID, iClient_ID, iDiaxiristis_ID, iContract_ID, iContract_Details_ID, iContract_Packages_ID, iStockCompany_ID, iInvestPolicy_ID,
            iMiFID_Risk, iMIFIDCategory_ID, iShare_ID, iProduct_ID, iProductCategory_ID, iCustomerAktion, iCodeAktion, iLastAktion = 0;
        string sProviderTitle;
        string[] sPriceType = { "Limit", "Market", "Stop loss", "Scenario", "ATC", "ATO" };
        string[] sConstant = { "Day Order", "GTC", "GTDate" };
        float sgPrice, sgQuantity, sgAmount, sgCurRate, sgEndektikiTimi;
        bool bCheckList;
        DateTime dToday;
        clsOrders_Recieved Order_Recieved = new clsOrders_Recieved();        

        public frmDPMOrder_Client()
        {
            InitializeComponent();
            
            sgEndektikiTimi = 1;
        }

        private void frmDPMOrder_Client_Load(object sender, EventArgs e)
        {
            bCheckList = false;

            //--- define Currency Rates table -----------------------------
            dtEURRates = new DataTable("CurrenciesRatesList");
            dtCol = dtEURRates.Columns.Add("Currency", System.Type.GetType("System.String"));
            dtCol = dtEURRates.Columns.Add("Rate", System.Type.GetType("System.String"));

            foreach (DataRow dtRow1 in Global.dtProducts.Select("Product_ID = 3"))
            {
                dtRow = dtEURRates.NewRow();
                dtRow["Currency"] = dtRow1["Code"] + "";
                dtRow["Rate"] = Convert.ToSingle(dtRow1["LastClosePrice"]);
                dtEURRates.Rows.Add(dtRow);
            }

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
            dtCol = dtList4.Columns.Add("LastClosePrice", System.Type.GetType("System.Single"));
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

            ucCS.StartInit(700, 400, 540, 20, 1);
            ucCS.TextOfLabelChanged += new EventHandler(ucCS_TextChanged);
            ucCS.Filters = "Status = 1 AND Service_ID = 3 ";
            ucCS.ListType = 1;

            ucPS.StartInit(650, 350, 200, 20, 1);
            ucPS.TextOfLabelChanged += new EventHandler(ucPS_TextChanged);
            ucPS.Filters = "Aktive >= 1 ";
            ucPS.ListType = 1;                                                                  // iListType = 1 : Global.dtProducts - common list of products
            ucPS.ShowNonAccord = false;                                                         // Show NonAccordable products (oxi katallila) with red Background
            ucPS.ShowCancelled = false;                                                         // Don't show cancelled products
            ucPS.ProductsContract = dtList4;

            //-------------- Define StockExcahnges  List ------------------
            cmbStockExchanges.DataSource = Global.dtStockExchanges.Copy();
            cmbStockExchanges.DisplayMember = "Code";
            cmbStockExchanges.ValueMember = "ID";

            //-------------- Define Products List ------------------
            cmbProducts.DataSource = Global.dtProductTypes.Copy().DefaultView;
            cmbProducts.DisplayMember = "Title";
            cmbProducts.ValueMember = "ID";

            //------- fgCodes ----------------------------
            fgCodes.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgCodes.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgCodes.DoubleClick += new System.EventHandler(fgCodes_DoubleClick);

            if (iDPM_ID == 0) {
                iCustomerAktion = 0;                    // 0 - ADD
                EmptyCustomer();                
            }
            else {
                iCustomerAktion = 1;                    // 1 - EDIT existing DPM Order

                clsOrdersDPM OrderDPM = new clsOrdersDPM();
                OrderDPM.Record_ID = iDPM_ID;
                OrderDPM.GetRecord();                

                dAktionDate.Value = Convert.ToDateTime(OrderDPM.AktionDate);
                iContract_ID = OrderDPM.Contract_ID;
                iClient_ID = OrderDPM.Client_ID;
                txtAUM.Text = OrderDPM.AUM + "";
                txtNotes.Text = OrderDPM.Notes;

                foundRows = Global.dtContracts.Select("Contract_ID = " + iContract_ID + " AND Client_ID = " + iClient_ID);
                if (foundRows.Length > 0) {
                    ucCS.ShowClientsList = false;
                    ucCS.txtContractTitle.Text = foundRows[0]["ContractTitle"] + ""; 
                    ucCS.ShowClientsList = true;                    
                    lblClientCode.Text = foundRows[0]["Code"] + "";
                    lblPortfolio.Text = foundRows[0]["Portfolio"] + "";
                    lblClientName.Text = foundRows[0]["Fullname"] + "";
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

                    dtList4.Rows.Clear();
                    Global.DefineContractProductsList(dtList4, iContract_ID, iContract_Details_ID, iContract_Packages_ID, false);
                };

                //--- Define Products List --------------------------------------------
                fgCodes.Redraw = false;
                fgCodes.Rows.Count = 1;

                clsOrdersDPM_Recs OrdersDPM_Recs = new clsOrdersDPM_Recs();
                OrdersDPM_Recs.DPM_ID = iDPM_ID;
                OrdersDPM_Recs.GetList();
                foreach (DataRow dtRow in OrdersDPM_Recs.List.Rows) {

                    fgCodes.AddItem((Convert.ToInt32(dtRow["Aktion"]) == 1 ? "BUY" : "SELL") + "\t" + dtRow["Share_Title"] + "\t" + dtRow["Share_Code"] + "\t" +
                                     dtRow["Share_ISIN"] + "\t" + dtRow["Currency"] + "\t" + dtRow["SE_Title"] + "\t" + sConstant[Convert.ToInt32(dtRow["Constant"])] + "\t" +
                                     (Convert.ToInt32(dtRow["PriceType"]) == 0 ? Convert.ToDecimal(dtRow["Price"]).ToString("0.00") : sPriceType[Convert.ToInt32(dtRow["PriceType"])]) + "\t" +
                                     dtRow["Quantity"] + "\t" + dtRow["Amount"] + "\t" + dtRow["Weight"] + "\t" + dtRow["ID"] + "\t" + dtRow["ShareCodes_ID"] + "\t" +
                                     dtRow["Product_ID"] + "\t" + dtRow["ProductCategories_ID"] + "\t" + dtRow["SE_ID"] + "\t" + dtRow["PriceType"] + "\t" + dtRow["Constant"] + "\t" + 
                                     dtRow["ConstantDate"] + "\t" + dtRow["TargetPrice"] + "\t" + dtRow["CurrRate_NA"] + "\t" + dtRow["Amount_NA"] + "\t" + dtRow["Status"]); 
                }
                fgCodes.Redraw = true;

                dtList4.Rows.Clear();
                Global.DefineContractProductsList(dtList4, iContract_ID, iContract_Details_ID, iContract_Packages_ID, false);

                fgCodes.Focus();
            }
            bCheckList = true;
        }
        private void dAktionDate_ValueChanged(object sender, EventArgs e)
        {
            ucCS.Filters = "Status = 1 AND Service_ID = 3 AND (Package_DateStart <= '" + dAktionDate.Value + "' AND Package_DateFinish >= '" + dAktionDate.Value + "') ";
        }
        private void picClient_Clean_Click(object sender, EventArgs e)
        {
            EmptyCustomer();
        }

        private void cmbProducts_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) {
                //lstType.SelectedValue = 0;
                ShowProductLabels(Convert.ToInt32(cmbProducts.SelectedValue));               

                //ucPS.ListType = 1;                                                                  // iListType = 1 : Global.dtProducts - common list of products
                ucPS.ShowNonAccord = true;                                                          // Show NonAccordable products (oxi katallila) with red Background
                ucPS.ShowCancelled = false;                                                         // Don't show cancelled products
                if (Convert.ToInt32(cmbProducts.SelectedValue) == 0) ucPS.Filters = "Aktive = 1";
                else ucPS.Filters = "Aktive = 1 AND Product_ID = " + cmbProducts.SelectedValue;

                ucPS.Focus();
            }
        }

        private void tsbCodeAdd_Click(object sender, EventArgs e)
        {
            EmptyCodeRec();
            
            btnSave.Visible = true;
            btnSave.Enabled = false;
            btnCancel.Visible = true;
            btnCancel.Enabled = true;

            panCodeDetails.Enabled = true;
            panCode.Visible = true;
            txtAction.Focus();

            txtAction.Text = "";
            txtPrice.Text = "0";
            txtQuantity.Text = "0";
            txtAmount.Text = "0";
            txtWeight.Text = "0";

            EmptyCodeRec();

            panCode.Visible = true;
            btnSave.Visible = true;
            btnSave.Enabled = false;
            btnCancel.Visible = true;
            btnCancel.Enabled = true;
        }
        private void fgCodes_DoubleClick(object sender, EventArgs e)
        {
            EditCode();
        }
        private void tsbCodeEdit_Click(object sender, EventArgs e)
        {
            EditCode();
        }
        private void EditCode()
        {
            if (fgCodes.Row > 0) {
                iCodeAktion = 1;
                i = fgCodes.Row;

                cmbProducts.SelectedValue = fgCodes[i, "Product_ID"];
                lstType.SelectedValue = fgCodes[i, "PriceType"];
                ShowProductLabels(Convert.ToInt32(cmbProducts.SelectedValue));
                lblTitle.Text = fgCodes[i, "Title"] + "";
                lblCode.Text = fgCodes[i, "Code"] + "";
                lblISIN.Text = fgCodes[i, "ISIN"] + "";
                lblCurrency.Text = fgCodes[i, "Currency"] + "";
                DefineCurRate();
                cmbStockExchanges.SelectedValue = Convert.ToInt32(fgCodes[i, "StockExchange_ID"]);
                cmbConstant.SelectedIndex = Convert.ToInt32(fgCodes[i, "Constant"]);
                if (cmbConstant.SelectedIndex == 2)
                    if ((fgCodes[i, "ConstantDate"] + "") != "")
                        dConstant.Value = Convert.ToDateTime(fgCodes[i, "ConstantDate"]);
                txtAction.Text = fgCodes[i, "Aktion"] + "";                
                txtPrice.Text = fgCodes[i, "Price"] + "";
                txtQuantity.Text = fgCodes[i, "Quantity"] + "";
                txtAmount.Text = fgCodes[i, "Amount"] + "";
                txtWeight.Text = fgCodes[i, "Weight"] + "";
                lblTargetPrice.Text = fgCodes[i, "TargetPrice"] + "";
                lblCurrRate_NA.Text = fgCodes[i, "CurrRate_NA"] + "";
                lblAmount_NA.Text = fgCodes[i, "Amount_NA"] + "";
                lblCurrency_NA.Text = lblCurr.Text;
                iShare_ID = Convert.ToInt32(fgCodes[i, "Share_ID"]);
                iProduct_ID = Convert.ToInt32(fgCodes[i, "Product_ID"]);
                iProductCategory_ID = Convert.ToInt32(fgCodes[i, "ProductCategory_ID"]);

                btnSave.Visible = true;
                btnSave.Enabled = true;
                btnCancel.Visible = true;
                btnCancel.Enabled = true;

                txtAction.Focus();
                panCodeDetails.Enabled = true;
                panCode.Visible = true;
            }
        }
        private void tsbCodeDelete_Click(object sender, EventArgs e)
        {
            if (fgCodes.Row > 0)
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;",
                    Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes) {

                    if (Convert.ToInt32(fgCodes[fgCodes.Row, "ID"]) != 0) {

                        clsOrdersDPM_Recs OrdersDPM_Recs = new clsOrdersDPM_Recs();
                        OrdersDPM_Recs.Record_ID = Convert.ToInt32(fgCodes[fgCodes.Row, "ID"]);
                        OrdersDPM_Recs.DeleteRecord();
                    }
                    fgCodes.RemoveItem(fgCodes.Row);
                }
        }

        private void picCopy2Clipboard_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Convert.IsDBNull(Clipboard.GetText())) Clipboard.SetDataObject(lblISIN.Text + "", true, 10, 100);
            }
            catch (Exception)
            {
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            int i = 0 , j = 1, k = 0;
            string sProducts = "";

            DefineCurRate();

            if (fgCodes.Rows.Count > 2) {
                j = 0;                                                // agores
                k = 0;                                                // poliseis
                for (i = 1; i <= fgCodes.Rows.Count - 1; i++) {
                    if ((fgCodes[i, 0] + "") == "BUY") j = j + 1;
                    else k = k + 1;
                }
                sProducts = "Αγορές :  " + j + ". Πωλήσεις: " + k;
            }
            else
                if (fgCodes.Rows.Count == 2) sProducts = (((fgCodes[1, 0] + "") == "BUY") ? "Αγορά: " : "Πώληση: ") + fgCodes[1, 1] + ", Τιμη: " + fgCodes[1, "Price"] + ", Ποσότητα: " + fgCodes[1, "Quantity"];

            if (iCustomerAktion == 0) {                                                 // 0 - ADD, 1 - EDIT
                clsOrdersDPM OrdersDPM = new clsOrdersDPM();
                OrdersDPM.OrderType = 1;                                                // DPMOrder Client
                OrdersDPM.Client_ID = iClient_ID;
                OrdersDPM.Contract_ID = iContract_ID;
                OrdersDPM.Contract_Details_ID = iContract_Details_ID;
                OrdersDPM.Contract_Packages_ID = iContract_Packages_ID;
                OrdersDPM.AllocationPercent = 100;
                OrdersDPM.StockCompany_ID = iStockCompany_ID;
                OrdersDPM.AUM = Convert.ToSingle(txtAUM.Text);
                OrdersDPM.Aktion = 0;
                OrdersDPM.AktionDate = dAktionDate.Value;
                OrdersDPM.ShareCodes_ID = 0;                
                OrdersDPM.ProductsCount = j + k;
                OrdersDPM.Products = sProducts;
                OrdersDPM.PriceType = 0;
                OrdersDPM.Price = "0";
                OrdersDPM.Quantity = "0";
                OrdersDPM.Constant = 0;
                OrdersDPM.ConstantDate = Convert.ToDateTime("1900/01/01");
                OrdersDPM.SentDate = Convert.ToDateTime("1900/01/01");
                OrdersDPM.Notes = txtNotes.Text;
                OrdersDPM.Status = 0;
                OrdersDPM.User_ID = iDiaxiristis_ID;
                OrdersDPM.Author_ID = Global.User_ID;
                iID = OrdersDPM.InsertRecord();

                SaveRecs();
            }
            else {                                                                     // EDIT
                clsOrdersDPM OrdersDPM = new clsOrdersDPM();
                OrdersDPM.Record_ID = iDPM_ID;
                OrdersDPM.GetRecord();
                OrdersDPM.Client_ID = iClient_ID;
                OrdersDPM.Contract_ID = iContract_ID;
                OrdersDPM.Contract_Details_ID = iContract_Details_ID;
                OrdersDPM.Contract_Packages_ID = iContract_Packages_ID;
                OrdersDPM.AllocationPercent = 100;
                OrdersDPM.StockCompany_ID = iStockCompany_ID;
                OrdersDPM.AUM = Convert.ToSingle(txtAUM.Text);
                OrdersDPM.AktionDate = dAktionDate.Value;
                OrdersDPM.ProductsCount = j + k;
                OrdersDPM.Products = sProducts;
                OrdersDPM.Notes = txtNotes.Text;
                OrdersDPM.EditRecord();

                iID = iDPM_ID;
                SaveRecs();
            }

            iLastAktion = 1;
            this.Close();
        }
        private void SaveRecs()
        {
            clsOrdersDPM_Recs OrdersDPM_Recs = new clsOrdersDPM_Recs();

            for (i = 1; i <= fgCodes.Rows.Count - 1; i++) {
                
                OrdersDPM_Recs = new clsOrdersDPM_Recs();
                if (Convert.ToInt32(fgCodes[i, "ID"]) != 0) {
                    OrdersDPM_Recs.Record_ID = Convert.ToInt32(fgCodes[i, "ID"]);
                    OrdersDPM_Recs.GetRecord();
                }
                OrdersDPM_Recs.DPM_ID = iID;
                OrdersDPM_Recs.Client_ID = iClient_ID;
                OrdersDPM_Recs.Contract_ID = iContract_ID;
                OrdersDPM_Recs.Contract_Details_ID = iContract_Details_ID;
                OrdersDPM_Recs.Contract_Packages_ID = iContract_Packages_ID;
                OrdersDPM_Recs.ShareCodes_ID = Convert.ToInt32(fgCodes[i, "Share_ID"]);
                OrdersDPM_Recs.Product_ID = Convert.ToInt32(fgCodes[i, "Product_ID"]);
                OrdersDPM_Recs.ProductCategories_ID = Convert.ToInt32(fgCodes[i, "ProductCategory_ID"]);
                OrdersDPM_Recs.Currency = fgCodes[i, "Currency"] + "";
                OrdersDPM_Recs.StockExchange_ID = Convert.ToInt32(fgCodes[i, "StockExchange_ID"]);
                OrdersDPM_Recs.Aktion = ((fgCodes[i, "Aktion"] + "") == "BUY" ? 1 : 2);
                OrdersDPM_Recs.Constant = Convert.ToInt32(fgCodes[i, "Constant"]);
                if (Global.IsDate(fgCodes[i, "ConstantDate"] + "")) OrdersDPM_Recs.ConstantDate = Convert.ToDateTime(fgCodes[i, "ConstantDate"] + "").ToString("dd/MM/yyyy");
                else OrdersDPM_Recs.ConstantDate = "";
                OrdersDPM_Recs.PriceType = Convert.ToInt32(fgCodes[i, "PriceType"]);
                OrdersDPM_Recs.Price = Global.IsNumeric(fgCodes[i, "Price"]) ? fgCodes[i, "Price"]+"" : "0";
                OrdersDPM_Recs.PriceUp = "0";
                OrdersDPM_Recs.PriceDown = "0";
                OrdersDPM_Recs.Quantity = fgCodes[i, "Quantity"]+"";
                OrdersDPM_Recs.Amount = fgCodes[i, "Amount"]+"";
                OrdersDPM_Recs.TargetPrice = fgCodes[i, "TargetPrice"] + "";
                OrdersDPM_Recs.CurrRate_NA = fgCodes[i, "CurrRate_NA"] + "";
                OrdersDPM_Recs.Amount_NA = fgCodes[i, "Amount_NA"] + "";
                OrdersDPM_Recs.Weight = fgCodes[i, "Weight"] + "";
                OrdersDPM_Recs.Status = Convert.ToInt32(fgCodes[i, "Status"]);

                if (Convert.ToInt32(fgCodes[i, "ID"]) != 0) OrdersDPM_Recs.EditRecord();
                else iRec_ID = OrdersDPM_Recs.InsertRecord();
            }
        }
        private void EmptyCustomer()
        {
            dAktionDate.Value = dToday;
            tsCodes.Enabled = false;
            ucCS.ShowClientsList = false;
            ucCS.txtContractTitle.Text = "";
            ucCS.ShowClientsList = true;
            lblClientName.Text = "";

            lblClientCode.Text = "";
            lblPortfolio.Text = "";
            lblEP.Text = "";
            lblEProfile.Text = "";
            lblService.Text = "";
            lblEMail.Text = "";
            lblMobile.Text = "";
            lblClientCategory.Text = "";
            lblComplexProduct.Text = "";
            chkXAA.Checked = false;
            chkWorld.Checked = false;
            chkEurope.Checked = false;
            chkAsia.Checked = false;
            chkGreece.Checked = false;
            chkAmerica.Checked = false;
            lblIncomeProducts.Text = "";
            lblCapitalProducts.Text = "";
            txtAUM.Text = "0";
            lblCurr.Text = "";
            iClient_ID = 0;
            iContract_ID = 0;
            iContract_Details_ID = 0;
            iContract_Packages_ID = 0;
            iStockCompany_ID = 0;
            iInvestPolicy_ID = 0;
            sProviderTitle = "";
            iMIFIDCategory_ID = 0;
            iMiFID_Risk = 0;
            lblInvestPolicy.Text = "";
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
                        ucPS.ListType = 2;                                                         // iListType = 2 : dtProductsContract - list of products for current contract
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
                        ucPS.ListType = 1;                                                         // iListType = 1 : Global.dtProducts - common list of products
                        break;
                }

                if (txtAction.Text == "BUY")  {
                    ShowProductLabels(Convert.ToInt32(cmbProducts.SelectedValue));
                    panCodeDetails.Enabled = true;
                    panCode.BackColor = Color.MediumAquamarine;
                    btnSave.Enabled = true;
                    ucPS.ShowNonAccord = true;                                                         // Show NonAccordable products (oxi katallila) with red Background
                    ucPS.Focus();
                }
                else {
                    if (txtAction.Text == "SELL") {
                        ShowProductLabels(Convert.ToInt32(cmbProducts.SelectedValue));
                        panCodeDetails.Enabled = true;
                        panCode.BackColor = Color.LightCoral;
                        btnSave.Enabled = true;
                        ucPS.ShowNonAccord = false;                                                     // Show NonAccordable products (oxi katallila) with red Background
                        ucPS.Focus();
                    }
                    else {
                        Console.Beep();
                        panCode.BackColor = Color.Silver;
                        panCodeDetails.Enabled = false;
                        btnSave.Enabled = false;
                        ucPS.ShowNonAccord = false;                                                     // Show NonAccordable products (oxi katallila) with red Background
                        txtAction.Focus();
                    }
                }
            }
        }
        private void picCode_Clean_Click(object sender, EventArgs e)
        {
            EmptyCodeRec();
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
                txtQuantity.Text = "0";
                txtQuantity.BackColor = Color.Red;
                txtQuantity.Focus();
            }
            else  {
                txtQuantity.BackColor = Color.White;
                DefineNums(2);
            }
        }
        private void txtAmount_LostFocus(object sender, EventArgs e)
        {
            if (!Global.IsNumeric(txtAmount.Text) || txtAmount.Text.IndexOf(".") > 0) {
                txtAmount.Text = "0";
                txtAmount.BackColor = Color.Red;
                txtAmount.Focus();
            }
            else {
                txtAmount.BackColor = Color.White;
                DefineNums(3);
            }
        }
        private void cmbConstant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConstant.SelectedIndex == 2) {
                dConstant.Value = DateTime.Now;
                dConstant.Visible = true;
            }
            else dConstant.Visible = false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            int i;
            string sTemp = "", sError = "";
            bool bError = false;

            if (iShare_ID == 0 || lblTitle.Text == "") {
                bError = true;
                sError = "Επιλέξτε ένα προϊόν \n";
            }

            if (((Convert.ToInt32(cmbProducts.SelectedValue) == 1) || (Convert.ToInt32(cmbProducts.SelectedValue) == 2) || (Convert.ToInt32(cmbProducts.SelectedValue) == 4)) &&
                (Convert.ToInt32(lstType.SelectedValue) == 0) && (txtPrice.Text == "0")) {
                bError = true;
                sError = "Το πεδίο Τιμή δεν πρέπει να είναι κενό. Καταχωρίστε εναν αριθμό μεγαλύτερο του 0. \n";
            }
            else {
                if (Convert.ToInt32(lstType.SelectedValue) == 2) {                                       // 2 - Stop
                    if (!Global.IsNumeric(txtPrice.Text) || txtPrice.Text == "0") {
                        bError = true;
                        sError = sError + "Το πεδίο Τιμή δεν πρέπει να είναι κενό. Καταχωρίστε εναν αριθμό μεγαλύτερο του 0 \n";
                    }
                }
            }

            if (Convert.ToInt32(cmbProducts.SelectedValue) == 6) {                                      // 6 - AK
                if (txtAction.Text == "SELL")  {
                    if (txtQuantity.Text != "0" && txtAmount.Text != "0") {
                        bError = true;
                        sError = "Καταχωρείστε Μερίδια ή Ποσό Επένδυσης, οχι και τα δυο \n";
                    }
                    else {
                        if (txtQuantity.Text == "0" && txtAmount.Text == "0") {
                            bError = true;
                            sError = "Τα Μερίδια, ή το Ποσό Επένδυσης πρέπει να καταχωρυθεί και να είναι μεγαλύτερο του 0. \n";
                        }
                    }
                }
                else {
                    if (Convert.ToInt32(lstType.SelectedValue) != 1) {                                          // isn't Market  
                        if (!Global.IsNumeric(txtAmount.Text) || txtAmount.Text == "0") {
                            bError = true;
                            sError = sError + "Το πεδίο " + lblAmount.Text + " πρέπει να είναι μεγαλύτερο του 0. \n";
                        }
                    }
                }
            }
            else {
                if (txtQuantity.Visible) {
                    if (!Global.IsNumeric(txtQuantity.Text) || txtQuantity.Text == "0") {
                        bError = true;
                        sError = sError + "Το πεδίο " + lblQuantity.Text + " πρέπει να είναι μεγαλύτερο του 0. \n";
                    }
                }
                if (txtQuantity.Visible) {
                    if (Convert.ToInt32(lstType.SelectedValue) != 1) {                                          // isn't Market                                         
                        if (!Global.IsNumeric(txtAmount.Text) || txtAmount.Text == "0") {
                            bError = true;
                            sError = sError + "Το πεδίο " + lblAmount.Text + " πρέπει να είναι μεγαλύτερο του 0 \n";
                        }
                    }
                }
            }


            if (!bError) {
                if (cmbConstant.SelectedIndex == 2) sTemp = dConstant.Value.ToString("dd/MM/yyyy");
                else sTemp = "";

                if (iCodeAktion == 0)  {
                    fgCodes.AddItem(txtAction.Text + "\t" + lblTitle.Text + "\t" + lblCode.Text + "\t" + lblISIN.Text + "\t" + lblCurrency.Text + "\t" +
                                    cmbStockExchanges.Text + "\t" + cmbConstant.Text + "\t" +
                                    (Convert.ToInt32(lstType.SelectedValue) == 0 ? Convert.ToDecimal(txtPrice.Text).ToString("0.00") : sPriceType[Convert.ToInt32(lstType.SelectedValue)]) + "\t" +
                                    txtQuantity.Text + "\t" + txtAmount.Text + "\t" + txtWeight.Text + "\t" + "0" + "\t" + iShare_ID + "\t" + iProduct_ID + "\t" + iProductCategory_ID + "\t" +
                                    cmbStockExchanges.SelectedValue + "\t" + lstType.SelectedValue + "\t" + cmbConstant.SelectedIndex + "\t" + dConstant.Value.Date + "\t" +
                                    lblTargetPrice.Text + "\t" + lblCurrRate_NA.Text  + "\t" + lblAmount_NA.Text + "\t" + "0", 1);
                    fgCodes.Row = 1;
                }
                else  {
                    i = fgCodes.Row;
                    fgCodes[i, "Aktion"] = txtAction.Text;
                    fgCodes[i, "Title"] = lblTitle.Text;
                    fgCodes[i, "Code"] = lblCode.Text;
                    fgCodes[i, "ISIN"] = lblISIN.Text;
                    fgCodes[i, "Currency"] = lblCurrency.Text;
                    fgCodes[i, "StockExchange_Title"] = cmbStockExchanges.Text;
                    fgCodes[i, "Duration"] = cmbConstant.Text;
                    fgCodes[i, "Price"] = (Convert.ToInt32(lstType.SelectedValue) == 0 ? Convert.ToDecimal(txtPrice.Text).ToString("0.00") : sPriceType[Convert.ToInt32(lstType.SelectedValue)]);
                    fgCodes[i, "Quantity"] = txtQuantity.Text;
                    fgCodes[i, "Amount"] = txtAmount.Text;
                    fgCodes[i, "Weight"] = txtWeight.Text;
                    fgCodes[i, "Share_ID"] = iShare_ID;
                    fgCodes[i, "Product_ID"] = iProduct_ID;
                    fgCodes[i, "ProductCategory_ID"] = iProductCategory_ID;
                    fgCodes[i, "StockExchange_ID"] = cmbStockExchanges.SelectedValue;
                    fgCodes[i, "PriceType"] = lstType.SelectedValue;
                    fgCodes[i, "Constant"] = cmbConstant.SelectedIndex;
                    fgCodes[i, "ConstantDate"] = dConstant.Value.Date;
                    fgCodes[i, "TargetPrice"] = lblTargetPrice.Text;
                    fgCodes[i, "CurrRate_NA"] = lblCurrRate_NA.Text;
                    fgCodes[i, "Amount_NA"] = lblAmount_NA.Text;
                }

                panCode.Visible = false;
            }
            else MessageBox.Show(sError, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            panCode.Visible = false;
        }
        private void EmptyCodeRec()
        {
            iCodeAktion = 0;
            txtAction.Text = "";
            cmbProducts.SelectedValue = 0;
            ucPS.ShowProductsList = false;
            ucPS.txtShareTitle.Text = "";
            ucPS.ShowProductsList = true;
            lblTitle.Text = "";
            lblCode.Text = "";
            lblISIN.Text = "";
            lblCurrency.Text = "";
            cmbStockExchanges.SelectedValue = 0;
            cmbConstant.SelectedIndex = 0;
            lstType.SelectedValue = 0;
            lblCurrency_NA.Text = lblCurr.Text;
            lblTargetPrice.Text = "";
            lblCurrRate_NA.Text = "";
            lblAmount_NA.Text = "";
            txtPrice.Text = "0";
            txtQuantity.Text = "0";
            txtAmount.Text = "0";
            txtWeight.Text = "0";
            iProduct_ID = 0;
            iProductCategory_ID = 0;
            panCode.BackColor = Color.Silver;
        }
        private void ShowProductLabels(int iProductType)
        {
            lstType.DisplayMember = "Title";
            lstType.ValueMember = "ID";
            dtList = new DataTable("TypeList");
            dtList.Columns.Add("Title", typeof(string));
            dtList.Columns.Add("ID", typeof(int));

            switch (iProductType)
            {
                case 0:
                    lblQuantity.Text = "Ποσότητα";
                    break;
                case 1:                                                  // 1-Shares                                     
                    lblQuantity.Text = "Τεμάχια";
                    if (txtAction.Text == "BUY") {
                        dtList.Rows.Add("Limit", 0);
                        dtList.Rows.Add("Scenario", 3);
                        dtList.Rows.Add("ATC", 4);
                        dtList.Rows.Add("ATO", 5);
                    }
                    else {
                        dtList.Rows.Add("Limit", 0);
                        dtList.Rows.Add("Stop", 2);
                        dtList.Rows.Add("ATC", 4);
                        dtList.Rows.Add("ATO", 5);
                    }
                    break;
                case 2:                                           // 2 - Bond
                    lblQuantity.Text = "Ονομαστική Αξία";
                    dtList.Rows.Add("Limit", 0);
                    dtList.Rows.Add("Market", 1);
                    break;
                case 4:                                           //    4 - ETF                                     
                    lblQuantity.Text = "Τεμάχια";
                    if (txtAction.Text == "BUY") {
                        dtList.Rows.Add("Limit", 0);
                        dtList.Rows.Add("Scenario", 3);
                        dtList.Rows.Add("ATC", 4);
                        dtList.Rows.Add("ATO", 5);
                    }
                    else {
                        dtList.Rows.Add("Limit", 0);
                        dtList.Rows.Add("Stop", 2);
                        dtList.Rows.Add("ATC", 4);
                        dtList.Rows.Add("ATO", 5);
                    }
                    break;
                case 6:                                           // Fund
                    lblQuantity.Text = "Μερίδια";
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
            lstType.DataSource = dtList;
        }
        private void lstType_SelectedValueChanged(object sender, EventArgs e)
        {
            lblPrice.Visible = true;
            txtPrice.Visible = true;
            txtPrice.Text = "0";

            lblAmount.Visible = true;
            txtAmount.Visible = true;
            txtAmount.Text = "0";

            lblQuantity.Visible = true;
            txtQuantity.Visible = true;
            txtQuantity.Text = "0";

            switch (Convert.ToInt32(lstType.SelectedValue))
            {
                case 0:                                                 // Limit
                    switch (Convert.ToInt32(cmbProducts.SelectedValue))
                    {
                        case 1:
                        case 4:
                            if (txtAction.Text == "SELL") {
                                lblAmount.Visible = false;
                                txtAmount.Visible = false;
                            }
                            break;
                        case 2:                                          // Bond
                            if (txtAction.Text == "SELL") {
                                lblAmount.Visible = false;
                                txtAmount.Visible = false;
                            }
                            break;
                    }
                    break;
                case 1:                                                 // Market
                    switch (Convert.ToInt32(cmbProducts.SelectedValue))
                    {
                        case 2:                                         // Bond
                            lblPrice.Visible = false;
                            txtPrice.Visible = false;
                            lblAmount.Visible = false;
                            txtAmount.Visible = false;
                            break;
                        case 6:                                         // AK
                            lblPrice.Visible = false;
                            txtPrice.Visible = false;
                            if (txtAction.Text == "BUY") {
                                lblQuantity.Visible = false;
                                txtQuantity.Visible = false;
                            }
                            break;
                    }
                    break;
                case 2:                                                  // Stop
                    switch (Convert.ToInt32(cmbProducts.SelectedValue))
                    {
                        case 1:
                        case 4:
                            if (txtAction.Text == "SELL") {
                                lblAmount.Visible = false;
                                txtAmount.Visible = false;
                            }
                            break;
                    }
                    break;
                case 3:                                                    // Scenario
                    cmbConstant.SelectedIndex = 1;
                    break;
                case 4:
                case 5:                                                    // ATC, ATO
                    lblPrice.Visible = false;
                    txtPrice.Visible = false;
                    lblAmount.Visible = false;
                    txtAmount.Visible = false;
                    break;
            }
        }
        private void DefineNums(int iField)
        {
            if (Convert.ToInt32(lstType.SelectedValue) != 1)
            {
                if (Global.IsNumeric(txtPrice.Text))
                {

                    sgPrice = (Global.IsNumeric(txtPrice.Text) ? Convert.ToSingle(txtPrice.Text) : 0);
                    sgQuantity = (Global.IsNumeric(txtQuantity.Text) ? Convert.ToSingle(txtQuantity.Text) : 0);
                    sgAmount = (Global.IsNumeric(txtAmount.Text) ? Convert.ToSingle(txtAmount.Text) : 0);

                    if (iField == 1 || iField == 2)
                    {
                        txtAmount.Text = (sgPrice * sgQuantity).ToString("0.00");
                        lblAmount_NA.Text = (Convert.ToSingle(txtAmount.Text) / sgCurRate).ToString("0.00");

                        if (iProduct_ID == 2)
                        {
                            txtAmount.Text = (Convert.ToSingle(txtAmount.Text) / 100).ToString("0.00");
                            lblAmount_NA.Text = (Convert.ToSingle(lblAmount_NA.Text) / 100).ToString("0.00");
                        }

                        txtWeight.Text = (Convert.ToSingle(lblAmount_NA.Text) * Convert.ToSingle(100) / Convert.ToSingle(txtAUM.Text)).ToString("0.00");
                    }
                    else
                    {
                        if (sgQuantity == 0)
                        {
                            if (sgPrice != 0) txtQuantity.Text = Math.Round(sgAmount / sgPrice).ToString("0.00");
                            else txtQuantity.Text = "0";
                        }
                    }
                }
                else
                {
                    txtQuantity.Text = "0";

                    lblAmount_NA.Text = (Convert.ToSingle(txtAmount.Text) / sgCurRate).ToString("0.00");
                    if (iProduct_ID == 2) lblAmount_NA.Text = (Convert.ToSingle(lblAmount_NA.Text) / 100).ToString("0.00");                // Omologa (ShareType=2)

                    txtWeight.Text = (Convert.ToSingle(lblAmount_NA.Text) * Convert.ToSingle(100) / Convert.ToSingle(txtAUM.Text)).ToString("0.00");
                }
            }
            else
            {
                if (Convert.ToSingle(txtAmount.Text) != 0)
                    txtWeight.Text = (Convert.ToSingle(txtAmount.Text) / sgCurRate * Convert.ToSingle(100) / Convert.ToSingle(txtAUM.Text)).ToString("0.00");
                else
                {
                    lblAmount_NA.Text = Math.Round(Convert.ToSingle(txtQuantity.Text) * sgEndektikiTimi / sgCurRate).ToString("0.00");
                    if (iProduct_ID == 2) lblAmount_NA.Text = (Convert.ToSingle(lblAmount_NA.Text) / 100).ToString("0.00");                 //Omologa (ShareType=2)

                    txtWeight.Text = (Convert.ToSingle(lblAmount_NA.Text) * Convert.ToSingle(100) / Convert.ToSingle(txtAUM.Text)).ToString("0.00");
                }
            }
        }
        private void DefineNums2(int iField)
        {

            sgPrice = (Global.IsNumeric(txtPrice.Text) ? Convert.ToSingle(txtPrice.Text) : 0);
            sgQuantity = (Global.IsNumeric(txtQuantity.Text) ? Convert.ToSingle(txtQuantity.Text) : 0);
            sgAmount = (Global.IsNumeric(txtAmount.Text) ? Convert.ToSingle(txtAmount.Text) : 0);

            if (sgPrice != 0 && sgCurRate != 0)
            {
                if (Convert.ToInt32(lstType.SelectedValue) != 1)
                {
                    if (Global.IsNumeric(txtPrice.Text))
                    {
                        if (iField == 1 || iField == 2)
                        {
                            txtAmount.Text = (sgPrice * sgQuantity).ToString("0.00");
                            if (iProduct_ID == 2) txtAmount.Text = (Convert.ToSingle(txtAmount.Text) / 100).ToString("0.00");

                            lblAmount_NA.Text = (Convert.ToSingle(txtAmount.Text) / sgCurRate).ToString("0.00");                            
                            //lblAmount_NomismaAnaforas.Text = (Convert.ToSingle(lblAmount_NomismaAnaforas.Text) / 100).ToString("0.00");

                            txtWeight.Text = (Convert.ToSingle(lblAmount_NA.Text) * Convert.ToSingle(100) / Convert.ToSingle(txtAUM.Text)).ToString("0.00");
                        }
                        else
                        {
                            if (sgQuantity == 0)
                            {
                                if (sgPrice != 0) txtQuantity.Text = Math.Round(sgAmount / sgPrice).ToString("0.00");
                                else txtQuantity.Text = "0";
                            }
                        }
                    }
                    else
                    {
                        txtQuantity.Text = "0";

                        lblAmount_NA.Text = (Convert.ToSingle(txtAmount.Text) / sgCurRate).ToString("0.00");
                        if (iProduct_ID == 2) lblAmount_NA.Text = (Convert.ToSingle(lblAmount_NA.Text) / 100).ToString("0.00");                // Omologa (ShareType=2)

                        txtWeight.Text = (Convert.ToSingle(lblAmount_NA.Text) * Convert.ToSingle(100) / Convert.ToSingle(txtAUM.Text)).ToString("0.00");
                    }
                }
                else
                {
                    if (Convert.ToSingle(txtAmount.Text) != 0) 
                        txtWeight.Text = (Convert.ToSingle(txtAmount.Text) / sgCurRate * Convert.ToSingle(100) / Convert.ToSingle(txtAUM.Text)).ToString("0.00");
                    else
                    {
                        lblAmount_NA.Text = Math.Round(Convert.ToSingle(txtQuantity.Text) * sgEndektikiTimi / sgCurRate).ToString("0.00");
                        if (iProduct_ID == 2) lblAmount_NA.Text = (Convert.ToSingle(lblAmount_NA.Text) / 100).ToString("0.00");                 //Omologa (ShareType=2)

                        txtWeight.Text = (Convert.ToSingle(lblAmount_NA.Text) * Convert.ToSingle(100) / Convert.ToSingle(txtAUM.Text)).ToString("0.00");
                    }
                }
            }
        }

        protected void ucCS_TextChanged(object sender, EventArgs e)
        {
            Global.ContractData stContract = new Global.ContractData();
            stContract = ucCS.SelectedContractData;
            if (stContract.MIFID_2 == 1) {                
                lblClientCode.Text = stContract.Code;
                lblPortfolio.Text = stContract.Portfolio;
                lblClientName.Text = stContract.ClientName;
                lblEP.Text = stContract.Policy_Title;
                lblEProfile.Text = stContract.Profile_Title;
                lblService.Text = stContract.Service_Title;
                lblEMail.Text = stContract.EMail;
                lblMobile.Text = stContract.Mobile;
                lblClientCategory.Text = stContract.MIFIDCategory_Title;
                chkXAA.Checked = stContract.XAA == 1 ? true : false;
                chkWorld.Checked = stContract.World == 1 ? true : false;
                chkEurope.Checked = stContract.Europe == 1 ? true : false;
                chkAsia.Checked = stContract.Asia == 1 ? true : false;
                chkGreece.Checked = stContract.Greece == 1 ? true : false;
                chkAmerica.Checked = stContract.America == 1 ? true : false;

                txtAUM.Text = "0"; 
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
                if (stContract.Service_ID == 5) lblInvestPolicy.Text = "Χρημα/τικά μέσα";       // 5 - DealAdvisory
                else lblInvestPolicy.Text = "Επενδ. Πολιτική";                                  // Else - Advisory                

                clsContracts klsContract = new clsContracts();
                klsContract.Record_ID = iContract_ID;
                klsContract.Contract_Details_ID = iContract_Details_ID;
                klsContract.Contract_Packages_ID = iContract_Packages_ID;
                klsContract.GetRecord();

                chkWorld.Checked = (klsContract.Details.ChkWorld == 1 ? true : false); ;
                chkGreece.Checked = (klsContract.Details.ChkGreece == 1 ? true : false); ;
                chkEurope.Checked = (klsContract.Details.ChkEurope == 1 ? true : false); ;
                chkAmerica.Checked = (klsContract.Details.ChkAmerica == 1 ? true : false); ;
                chkAsia.Checked = (klsContract.Details.ChkAsia == 1 ? true : false); ;
               
                lblIncomeProducts.Text = klsContract.Details.IncomeProducts;
                lblCapitalProducts.Text = klsContract.Details.CapitalProducts;

                DefineComplexProduct();

                dtList4.Rows.Clear();
                Global.DefineContractProductsList(dtList4, iContract_ID, iContract_Details_ID, iContract_Packages_ID, false);

                tsCodes.Enabled = true;
                txtAUM.Focus();
            }
            else {
                MessageBox.Show("Δεν είναι MIFID II Σύμβαση.", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ucCS.ShowClientsList = false;
                ucCS.txtContractTitle.Text = "";
                ucCS.ShowClientsList = true;
                ucCS.txtContractTitle.Focus();
            }
        }
        protected void ucPS_TextChanged(object sender, EventArgs e)
        {
            string sTemp = "", sMessages = "";

            Global.ProductData stProduct = new Global.ProductData();
            stProduct = ucPS.SelectedProductData;
            if (stProduct.OK_Flag == 1 || txtAction.Text == "SELL") {
                lblTitle.Text = stProduct.Title;
                lblCode.Text = stProduct.Code;
                lblISIN.Text = stProduct.ISIN;
                lblCurrency.Text = stProduct.Currency;
                DefineCurRate();                
                iShare_ID = stProduct.ShareCode_ID;
                iProduct_ID = stProduct.Product_ID;
                iProductCategory_ID = stProduct.ProductCategory_ID;
                if(Convert.ToInt32(cmbProducts.SelectedValue) == 0) cmbProducts.SelectedValue = iProduct_ID;
                cmbStockExchanges.SelectedValue = stProduct.StockExchange_ID;
                txtPrice.Text = stProduct.LastClosePrice.ToString();
                sgEndektikiTimi = stProduct.LastClosePrice;
                lblTargetPrice.Text = stProduct.LastClosePrice.ToString();
                lblCurrRate_NA.Text = lblCurr.Text + " / " + lblCurrency.Text + "  = " + sgCurRate.ToString("0.########");
                //ucPS.ListType = 1;                                                                  // iListType = 1 : Global.dtProducts - common list of products

                //lstType.SelectedIndex = 0;
                //ShowProductLabels(iProduct_ID);


                txtQuantity.Focus();
            }
            else  {
                sTemp = stProduct.OK_String;

                sMessages = "Δεν είναι κατάλληλο λόγω:";
                if (sTemp.Substring(0, 1) == "0") sMessages = sMessages + "\n - Risk profile";
                if (sTemp.Substring(1, 1) == "0") sMessages = sMessages + "\n - Retail/Professional";
                if (sTemp.Substring(2, 1) == "0") sMessages = sMessages + "\n - Distribution channel";
                if (sTemp.Substring(3, 1) == "0") sMessages = sMessages + "\n - Currency risk";
                if (sTemp.Substring(4, 1) == "0") sMessages = sMessages + "\n - Complex";
                if (sTemp.Substring(5, 1) == "0") sMessages = sMessages + "\n - Γεωγραφικης κατανομης";
                if (sTemp.Substring(6, 1) == "0") sMessages = sMessages + "\n - Ειδικες οδηγιες";

                MessageBox.Show(sMessages, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                EmptyCodeRec();
            }

        }
        private void DefineCurRate()
        {
            if (lblCurr.Text == "EUR") {                                            // Nomisma Anaforas
                if (lblCurrency.Text == "EUR") sgCurRate = 1;                       // Nomisma Proiontos
                else {
                    foundRows = dtEURRates.Select("Currency = 'EUR" + lblCurrency.Text + "='");
                    if (foundRows.Length > 0) sgCurRate = Convert.ToSingle(foundRows[0]["Rate"]);     // CurrRate
                    else sgCurRate = 1;                                             // Cur Rate not found 
                }
            }
            else
            {
                if (lblCurrency.Text == "EUR")
                {                                    // Nomisma Proiontos
                    foundRows = dtEURRates.Select("Currency = 'EUR" + lblCurrency.Text + "='");
                    if (foundRows.Length > 0) sgCurRate = 1 / Convert.ToSingle(foundRows[0]["Rate"]);   // CurrRate;
                    else sgCurRate = 1;                                             // Cur Rate not found 
                }
                else  {
                    foundRows = dtEURRates.Select("Currency = 'EUR" + lblCurrency.Text + "='");
                    if (foundRows.Length > 0) sgCurRate = 1 / Convert.ToSingle(foundRows[0]["Rate"]);   // CurrRate
                    else sgCurRate = 1;                                             // Cur Rate not found 

                    foundRows = dtEURRates.Select("Currency = 'EUR" + lblCurrency.Text + "='");
                    if (foundRows.Length > 0) sgPrice = 1 / Convert.ToSingle(foundRows[0]["Rate"]);   // CurrRate
                    else sgPrice = 1;                                               // Cur Rate not found 

                    sgCurRate = sgCurRate / sgPrice;
                }
            }
        }
        private void DefineComplexProduct()
        {
            lblComplexProduct.Text = "No";
            clsContracts_ComplexSigns klsContracts_ComplexSigns = new clsContracts_ComplexSigns();
            klsContracts_ComplexSigns.Contract_ID = iContract_ID;
            klsContracts_ComplexSigns.GetList();
            foreach (DataRow dtRow in klsContracts_ComplexSigns.List.Rows) {
                if (Convert.ToInt32(dtRow["ComplexSign_ID"]) == 2) {
                    lblComplexProduct.Text = "Yes";
                }
            }
        }
        public DateTime Today { get { return dToday; } set { dToday = value; } }
        public int II_ID { get { return iDPM_ID; } set { iDPM_ID = value; } }
        public int LastAktion { get { return iLastAktion; } set { iLastAktion = value; } }
        public int Diaxiristis_ID { get { return iDiaxiristis_ID; } set { iDiaxiristis_ID = value; } }
    }
}
