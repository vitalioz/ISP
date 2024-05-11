using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Globalization;
using C1.Win.C1FlexGrid;
using Core;
using System.Data.SqlClient;

namespace Transactions
{
    public partial class frmDailySecurities : Form
    {
        DataTable dtList, dtList4,  dtEURRates;
        DataView dtView;
        DataColumn dtCol;
        DataRow[] foundRows;
        int i, k, jj, iMode, iID, iRow, iClient_ID, iContract_ID, iContract_Details_ID, iContract_Packages_ID, iCommandType_ID, iBusinessType_ID, iProvider_ID, iProviderType,
            iShare_ID, iShareTitle_ID, iShareCode_ID, iXAA, iProduct_ID, iProductCategory_ID, iStockExchange_ID, iOddEvenBlock, iStyle, iClientData_ID, iCheckedRows,
            iMIFIDCategory_ID,  iMIFID_2, iRightsLevel, iPreClient_ID, iAdvisor_ID, iDiax_ID, iActions, iDiavivastis, iDivision, iSent, iCheck, iService_ID, iContractService_ID, iShowCancelled;
        float sgTemp1, sgTemp2;
        string sTemp, sExtra, sFileName, sUploadFile, sCode, sPortfolio, sProviderTitle, sProductTitle, sStockExchange_Code, sPreCode, sPreISIN, sInvPropNotesFlag, sDPMNotesFlag, 
            sBulkCommand, sInvestProfile, sInvestPolicy, sMessages;
        Point position;
        bool pMove;
        string[] sStatus = { "", Global.GetLabel("fixed_assets"), Global.GetLabel("fixed_assets_until") };
        string[] sConstant = { "Day Order", "GTC", "GTDate" };
        string[] sRisks = { "", "Υψηλός", "Μεσαίος", "Χαμηλός" };
        string[] sMiFID = { "-", "Ιδιώτης Πελάτης", "Επαγγελματίας Πελάτης", "Επιλέξιμοι Αντισυμβαλλόμενοι" };
        string[] sPriceType = { "Limit", "Market", "Stop loss", "Scenario", "ATC", "ATO" };
        DateTime dTemp;
        bool bCheckList, bFilter;
        Hashtable imgMap = new Hashtable();
        CellRange rng;
        CellStyle csCancel, csBuy, csSell, csGroup1, csGroup2, csChecked, csThinks, csWait, csGreen, csOrange;
        Hashtable htStatus = new Hashtable();
        Hashtable htFile = new Hashtable();

        clsInvestIdees_Commands InvestIdees_Commands = new clsInvestIdees_Commands();
        clsOrders_Recieved Orders_Recieved = new clsOrders_Recieved();
        clsExecutionReports ExecutionReports = new clsExecutionReports();

        #region --- Start functions -----------------------------------------------------------------------------
        public frmDailySecurities()
        {
            InitializeComponent();

            panDaily.Left = 4;
            panDaily.Top = 30;
            panDaily.Visible = true;

            panSearch.Left = 4;
            panSearch.Top = 30;
            panSearch.Visible = false;

            panFilters.Top = 30;
            panFilters.Left = 968;
            panFilters.Width = 612;
            panFilters.Height = 112;

            panDPM.Left = 330;
            panDPM.Top = 4;

            panSecurities.Left = 2;
            panSecurities.Top = 52;
            panSecurities.Width = 878;
            panSecurities.Height = 88;

            panMultiProducts.Left = 68;
            panMultiProducts.Top = 172;

            panNewProduct.Top = 76;
            panNewProduct.Left = 472;

            csCancel = fgList.Styles.Add("Cancelled");
            csCancel.ForeColor = Color.Red;

            csBuy = fgList.Styles.Add("Buy");
            csBuy.BackColor = Color.MediumAquamarine;
            csBuy.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

            csSell = fgList.Styles.Add("Sell");
            csSell.BackColor = Color.LightCoral;
            csSell.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

            csGroup1 = fgList.Styles.Add("Group1");                     // Group1 - Odd scenario records
            csGroup1.BackColor = Color.FromArgb(252, 252, 146);

            csGroup2 = fgList.Styles.Add("Group2");                     // Group2 - even scenario reords
            csGroup2.BackColor = Color.FromArgb(250, 212, 249);

            csChecked = fgList.Styles.Add("Checked");
            csChecked.BackColor = Color.Yellow;

            csThinks = fgList.Styles.Add("Thinks");
            csThinks.BackColor = Color.Yellow;

            csWait = fgList.Styles.Add("Wait");
            csWait.BackColor = Color.Thistle;

            csGreen = fgList.Styles.Add("Green");
            csGreen.BackColor = Color.MediumAquamarine;

            csOrange = fgList.Styles.Add("Orange");
            csOrange.BackColor = Color.Orange;
        }
        private void frmDailySecurities_Load(object sender, EventArgs e)
        {
            DateTime dPoint1, dPoint2;
            dPoint1 = DateTime.Now;

            if (iMode == 1)  {
                this.Text = Global.GetLabel("transactions_list");

                panDaily.Visible = true;
                panSearch.Visible = false;

                panFilters.Width = 612;
                panFilters.Height = 112;

                btnExcel.Visible = false;

                //--- dtList4 - table of products that are valid with current Contract -------------------------------
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
                dtCol = dtList4.Columns.Add("HFIC_Recom", System.Type.GetType("System.Int16"));
                dtCol = dtList4.Columns.Add("Aktive", System.Type.GetType("System.Int16"));

                ucCS.Left = 70;
                ucCS.Top = 34;
                ucCS.StartInit(700, 400, 200, 20, 1);
                ucCS.TextOfLabelChanged += new EventHandler(ucCS_TextChanged);
                ucCS.Filters = "Status = 1 And Contract_ID > 0";
                ucCS.Mode = 1;
                ucCS.ListType = 2;
                ucCS.Visible = true;

                ucPS.Left = 200;
                ucPS.Top = 86;
                ucPS.StartInit(700, 400, 280, 20, 1);
                ucPS.TextOfLabelChanged += new EventHandler(ucPS_TextChanged);
                ucPS.Mode = 1;
                ucPS.ListType = 1;
                ucPS.Filters = "Aktive >= 1 ";
                ucPS.ShowNonAccord = true;                                                          // Show NonAccordable products (oxi katallila) with red Background
                ucPS.ShowCancelled = false;                                                         // Don't show cancelled products
                ucPS.Visible = true;

                cmbDiaxiristes.Visible = false;
                cmbDiaxiristes.Left = 70;
                cmbDiaxiristes.Top = 34;

                //-------------- Define Diaxiristes List ------------------   
                dtView = Global.dtUserList.Copy().DefaultView;
                dtView.RowFilter = "Diaxiristis = 1 AND Aktive = 1";
                cmbDiaxiristes.DataSource = dtView;
                cmbDiaxiristes.DisplayMember = "Title";
                cmbDiaxiristes.ValueMember = "ID";
                cmbDiaxiristes.SelectedValue = Global.User_ID;

                dToday.Value = DateTime.Now;

                for (i = 0; i < imgStatus.Images.Count; i++) imgMap.Add(i, imgStatus.Images[i]);
            }
            else {
                this.Text = Global.GetLabel("transactions_search");

                panDaily.Visible = false;
                panSearch.Visible = true;

                panFilters.Width = 736;
                panFilters.Height = 174;

                //btnExcel.Visible = true;

                ucCS.Left = 390;
                ucCS.Top = 34;
                ucCS.StartInit(700, 400, 200, 20, 1);
                ucCS.TextOfLabelChanged += new EventHandler(ucCS_TextChanged);
                ucCS.Filters = " Contract_ID > 0";
                ucCS.Mode = 2;
                ucCS.ListType = 2;
                ucCS.Visible = true;

                ucPS.Left = 684;
                ucPS.Top = 58;
                ucPS.StartInit(700, 400, 256, 20, 1);
                ucPS.TextOfLabelChanged += new EventHandler(ucPS_TextChanged);
                ucPS.Mode = 2;
                ucPS.ListType = 1;
                ucPS.Filters = "Aktive >= 0 ";
                ucPS.ShowNonAccord = true;
                ucPS.ShowCancelled = false;
                ucPS.Visible = true;

                ucDC.DateFrom = DateTime.Now;
                ucDC.DateTo = DateTime.Now;

                //-------------- Define StockExchanges List ------------------
                fgStockExchanges.Redraw = false;
                fgStockExchanges.Rows.Count = 1;
                foreach (DataRow dtRow in Global.dtStockExchanges.Copy().Rows)
                    if (Convert.ToInt32(dtRow["ID"]) != 0)
                        fgStockExchanges.AddItem(false + "\t" + dtRow["Title"] + "\t" + dtRow["ID"]);
                fgStockExchanges.Redraw = true;

                chkStockExchanges.Checked = false;

                //-------------- Define Prooductions List ------------------
                cmbProducts.DataSource = Global.dtProductTypes.Copy();
                cmbProducts.DisplayMember = "Title";
                cmbProducts.ValueMember = "ID";
                cmbProducts.SelectedValue = 0;

                //-------------- Define Currencies List --------------------
                cmbCurrency2.DataSource = Global.dtCurrencies.Copy();
                cmbCurrency2.DisplayMember = "Title";
                cmbCurrency2.ValueMember = "ID";

                for (i = 0; i < imgFiles.Images.Count; i++) imgMap.Add(i, imgFiles.Images[i]);
            }
            
            lblContract.Text = Global.GetLabel("contract");
            lblCustomer.Text = Global.GetLabel("__b45");
            lblAction.Text = Global.GetLabel("action");
            lblProduct.Text = Global.GetLabel("product");
            lblType.Text = Global.GetLabel("type");
            lblPrice.Text = Global.GetLabel("price");
            lblQuantity.Text = Global.GetLabel("quantity");
            lblAmount.Text = Global.GetLabel("amount");
            lblConstant.Text = Global.GetLabel("duration");
            lblProvider.Text = Global.GetLabel("provider");
            lblAdvisor.Text = Global.GetLabel("advisor");
            lblSender.Text = Global.GetLabel("transmitter");
            lblSended.Text = Global.GetLabel("transmission");
            lblExecute.Text = Global.GetLabel("execution");

            bCheckList = false;         
            panDPM.Visible = false;
            btnSaveTransfer.Visible = false;
            panFilters.Visible = true;
            if (iRightsLevel == 1) tsbTransfer.Enabled = false;

            iBusinessType_ID = 1;                             // 1 - RTO (HF), 2 - Custody (HFSS)
            iCommandType_ID = 1;                              // 1 - Single Order, 2 - Execution Order, 3 - Bulk Order, 4 - DPM Order
            lstType.SelectedIndex = 0;
            cmbConstant.SelectedIndex = 0;
            dConstant.Value = Convert.ToDateTime("1900/01/01");
            chkShowCancelled.Checked = true;
            iShowCancelled = 1;
            iStockExchange_ID = 0;
            iClient_ID = 0;
            sCode = "";
            sPortfolio = "";
            iMIFIDCategory_ID = 0;
            iMIFID_2 = 0;

            iProvider_ID = 0;
            iProviderType = 1;                              // by default = 1 as iBusinessType_ID = 1
            iAdvisor_ID = 0;
            iDiax_ID = 0;
            iDivision = 0;
            iActions = 0;
            iDiavivastis = 0;
            iSent = 0;
            iCheck = 0;

            dTemp = DateTime.Now;


            //-------------- Define ServiceProviders List -----------------
            dtView = Global.dtServiceProviders.Copy().DefaultView;
            dtView.RowFilter = "Aktive = 1";
            cmbProviders.DataSource = dtView;
            cmbProviders.DisplayMember = "Title";
            cmbProviders.ValueMember = "ID";
            cmbProviders.SelectedValue = 0;

            //-------------- Define cmbRecievedMethods List ------------------
            dtList = Global.dtRecieveMethods.Copy();
            foundRows = dtList.Select("ID = 0");
            foundRows[0]["Title"] = "-";
            cmbRecieveMethod2.DataSource = dtList;
            cmbRecieveMethod2.DisplayMember = "Title";
            cmbRecieveMethod2.ValueMember = "ID";
            cmbRecieveMethod2.SelectedValue = 0;            

            //-------------- Define cmbRecievedMethods List ------------------
            cmbRecieveMethod3.DataSource = Global.dtRecieveMethods.Copy();
            cmbRecieveMethod3.DisplayMember = "Title";
            cmbRecieveMethod3.ValueMember = "ID";
            cmbRecieveMethod3.SelectedValue = 1;

            //-------------- Define Advisors List ------------------   
            dtView = Global.dtUserList.Copy().DefaultView;
            dtView.RowFilter = "Chief = 1 AND Aktive = 1";
            cmbAdvisors.DataSource = dtView;
            cmbAdvisors.DisplayMember = "Title";
            cmbAdvisors.ValueMember = "ID";
            cmbAdvisors.SelectedValue = 0;

            //-------------- Define Divisions List ------------------
            cmbDivisions.DataSource = Global.dtDivisions.Copy();
            cmbDivisions.DisplayMember = "Title";
            cmbDivisions.ValueMember = "ID";
            cmbDivisions.SelectedValue = 0;

            //-------------- Define Senders List ------------------   
            dtView = Global.dtUserList.Copy().DefaultView;
            dtView.RowFilter = "Sender = 1 AND Aktive = 1";
            cmbUsers.DataSource = dtView;
            cmbUsers.DisplayMember = "Title";
            cmbUsers.ValueMember = "ID";

            cmbUsers.Enabled = true;
            cmbUsers.SelectedValue = 0;

            //-------------- Define Diaxeiristis List ------------------
            dtView = Global.dtUserList.Copy().DefaultView;
            dtView.RowFilter = "Diaxiristis = 1 OR Diaxiristis = 2";
            cmbDiax.DataSource = dtView;
            cmbDiax.DisplayMember = "Title";
            cmbDiax.ValueMember = "ID";

            //-------------- Define SERVICES List ----------------------
            cmbServices.DataSource = Global.dtServices.Copy();
            cmbServices.DisplayMember = "Title";
            cmbServices.ValueMember = "ID";
            cmbServices.SelectedValue = 0;

            //-------------- Define Products List ------------------
            cmbProductType.DataSource = Global.dtProductTypes.Copy();
            cmbProductType.DisplayMember = "Title";
            cmbProductType.ValueMember = "ID";

            //-------------- Define Currencies List ------------------
            cmbCurrency.DataSource = Global.dtCurrencies.Copy();
            cmbCurrency.DisplayMember = "Title";
            cmbCurrency.ValueMember = "ID";

            //-------------- Define Stock Exchanges ------------------
            dtView = Global.dtStockExchanges.Copy().DefaultView;
            dtView.Sort = "Code";
            cmbStockExchanges.DataSource = dtView;
            cmbStockExchanges.DisplayMember = "Code";
            cmbStockExchanges.ValueMember = "ID";

            //-------------- Define ServiceProviders List ------------------
            dtView = Global.dtServiceProviders.Copy().DefaultView;
            dtView.RowFilter = "ProviderType = 0 OR ProviderType = 1 OR ProviderType = 2";
            cmbServiceProviders.DataSource = dtView;
            cmbServiceProviders.DisplayMember = "Title";
            cmbServiceProviders.ValueMember = "ID";
            cmbServiceProviders.SelectedValue = 0;

            cmbSent.SelectedIndex = 0;
            cmbActions.SelectedIndex = 0;
            cmbChecked.SelectedIndex = 0;

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:white; } Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; }");
            fgList.RowColChange += new EventHandler(fgList_RowColChange);
            fgList.MouseDown += new MouseEventHandler(fgList_MouseDown);
            fgList.Click += new System.EventHandler(fgList_Click);
            fgList.DoubleClick += new System.EventHandler(fgList_DoubleClick);
            fgList.OwnerDrawCell += fgList_OwnerDrawCell;

            fgList.DrawMode = DrawModeEnum.OwnerDraw;
            fgList.ShowCellLabels = true;

            fgList.Styles.Normal.WordWrap = true;
            fgList.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgList.Rows[0].AllowMerging = true;
            fgList.Cols[0].AllowMerging = true;
            rng = fgList.GetCellRange(0, 0, 1, 0);
            rng.Data = " ";

            fgList.Cols[1].AllowMerging = true;
            rng = fgList.GetCellRange(0, 1, 1, 1);
            rng.Data = Global.GetLabel("n");

            fgList.Cols[2].AllowMerging = true;
            rng = fgList.GetCellRange(0, 2, 1, 2);
            rng.Data = "Bulk N";

            fgList.Cols[3].AllowMerging = true;
            rng = fgList.GetCellRange(0, 3, 1, 3);
            rng.Data = "Εντολέας";              //Global.GetLabel("customer_name")

            fgList.Cols[4].AllowMerging = true;
            rng = fgList.GetCellRange(0, 4, 1, 4);
            rng.Data = "Σύμβαση";

            fgList.Cols[5].AllowMerging = true;
            rng = fgList.GetCellRange(0, 5, 1, 5);
            rng.Data = Global.GetLabel("provider");

            fgList.Cols[6].AllowMerging = true;
            rng = fgList.GetCellRange(0, 6, 1, 6);
            rng.Data = Global.GetLabel("code");

            fgList.Cols[7].AllowMerging = true;
            rng = fgList.GetCellRange(0, 7, 1, 7);
            rng.Data = Global.GetLabel("subaccount");

            fgList.Cols[8].AllowMerging = true;
            rng = fgList.GetCellRange(0, 8, 1, 8);
            rng.Data = Global.GetLabel("action");

            rng = fgList.GetCellRange(0, 9, 0, 12);
            rng.Data = Global.GetLabel("product");

            fgList[1, 9] = Global.GetLabel("type");
            fgList[1, 10] = Global.GetLabel("title");
            fgList[1, 11] = Global.GetLabel("bloomberg_code");
            fgList[1, 12] = Global.GetLabel("isin");

            rng = fgList.GetCellRange(0, 13, 0, 15);
            rng.Data = Global.GetLabel("order");

            fgList[1, 13] = Global.GetLabel("price");
            fgList[1, 14] = Global.GetLabel("quantity");
            fgList[1, 15] = Global.GetLabel("amount");

            rng = fgList.GetCellRange(0, 16, 0, 18);
            rng.Data = Global.GetLabel("executed_command");

            fgList[1, 16] = Global.GetLabel("price");
            fgList[1, 17] = Global.GetLabel("quantity");
            fgList[1, 18] = Global.GetLabel("amount");

            fgList.Cols[19].AllowMerging = true;
            rng = fgList.GetCellRange(0, 19, 1, 19);
            rng.Data = Global.GetLabel("currency");

            fgList.Cols[20].AllowMerging = true;
            rng = fgList.GetCellRange(0, 20, 1, 20);
            rng.Data = Global.GetLabel("duration");

            rng = fgList.GetCellRange(0, 21, 0, 22);
            rng.Data = Global.GetLabel("stock_exchange");

            fgList[1, 21] = "εντολή";
            fgList[1, 22] = "εκτέλεση";

            fgList.Cols[23].AllowMerging = true;
            rng = fgList.GetCellRange(0, 23, 1, 23);
            rng.Data = Global.GetLabel("receipt_time");

            fgList.Cols[24].AllowMerging = true;
            rng = fgList.GetCellRange(0, 24, 1, 24);
            rng.Data = Global.GetLabel("transmission_time");

            fgList.Cols[25].AllowMerging = true;
            rng = fgList.GetCellRange(0, 25, 1, 25);
            rng.Data = Global.GetLabel("execution_date");

            fgList.Cols[26].AllowMerging = true;
            rng = fgList.GetCellRange(0, 26, 1, 26);
            rng.Data = Global.GetLabel("receipt_way");

            fgList.Cols[27].AllowMerging = true;
            rng = fgList.GetCellRange(0, 27, 1, 27);
            rng.Data = "Επίσημη Ενημέρωση";

            fgList.Cols[28].AllowMerging = true;
            rng = fgList.GetCellRange(0, 28, 1, 28);
            rng.Data = Global.GetLabel("notes");

            fgList.Cols[29].AllowMerging = true;
            rng = fgList.GetCellRange(0, 29, 1, 29);
            rng.Data = Global.GetLabel("transmitter");

            fgList.Cols[30].AllowMerging = true;
            rng = fgList.GetCellRange(0, 30, 1, 30);
            rng.Data = Global.GetLabel("advisor");

            fgList.Cols[31].AllowMerging = true;
            rng = fgList.GetCellRange(0, 31, 1, 31);
            rng.Data = "Διαχειριστής";

            fgList.Cols[32].AllowMerging = true;
            rng = fgList.GetCellRange(0, 32, 1, 32);
            rng.Data = Global.GetLabel("services");

            fgList.Cols[33].AllowMerging = true;
            rng = fgList.GetCellRange(0, 33, 1, 33);
            rng.Data = "Επενδ.πολιτική";

            fgList.Cols[34].AllowMerging = true;
            rng = fgList.GetCellRange(0, 34, 1, 34);
            rng.Data = "Επενδ.Profile";

            fgList.Cols[35].AllowMerging = true;
            rng = fgList.GetCellRange(0, 35, 1, 35);
            rng.Data = "Επενδ.πρόταση";

            fgList.Cols[36].AllowMerging = true;
            rng = fgList.GetCellRange(0, 36, 1, 36);
            rng.Data = "Κίνδυνος";

            fgList.Cols[37].AllowMerging = true;
            rng = fgList.GetCellRange(0, 37, 1, 37);
            rng.Data = "Είδος Πελάτη MiFID";

            fgList.Cols[38].AllowMerging = true;
            rng = fgList.GetCellRange(0, 38, 1, 38);
            rng.Data = "Χρηματ/ριο Εκτέλεσης Τίτλος";

            fgList.Cols[39].AllowMerging = true;
            rng = fgList.GetCellRange(0, 39, 1, 39);
            rng.Data = "Προτινόμενο απο ΕΕ";

            rng = fgList.GetCellRange(0, 40, 0, 46);
            rng.Data = Global.GetLabel("commissions");

            fgList[1, 40] = Global.GetLabel("percent");
            fgList[1, 41] = Global.GetLabel("amount");
            fgList[1, 42] = Global.GetLabel("discount_in_percent");
            fgList[1, 43] = Global.GetLabel("discount_in_amount");
            fgList[1, 44] = Global.GetLabel("final_commission_percent");
            fgList[1, 45] = "Προμήθεια μετά την έκπτωση";
            fgList[1, 46] = Global.GetLabel("final_commission");    

            fgList.Cols[70].AllowMerging = true;
            rng = fgList.GetCellRange(0, 70, 1, 70);
            rng.Data = "Rate";

            fgList.Cols[71].AllowMerging = true;
            rng = fgList.GetCellRange(0, 71, 1, 71);
            rng.Data = "Αξία σε EUR";

            Column clm0 = fgList.Cols["image_map"];
            clm0.ImageMap = imgMap;
            clm0.ImageAndText = false;
            clm0.ImageAlign = ImageAlignEnum.CenterCenter;

            fgList.Styles.Fixed.TextAlign = TextAlignEnum.CenterCenter;

            //------- fgPreOrders ----------------------------
            fgPreOrders.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgPreOrders.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgPreOrders.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgPreOrders_BeforeEdit);
            fgPreOrders.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgPreOrders_AfterEdit);
            fgPreOrders.MouseDown += new MouseEventHandler(fgPreOrders_MouseDown);
            fgPreOrders.OwnerDrawCell += fgPreOrders_OwnerDrawCell;

            fgPreOrders.DrawMode = DrawModeEnum.OwnerDraw;
            fgPreOrders.ShowCellLabels = true;

            //------- fgStockExchanges ----------------------------
            fgStockExchanges.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgStockExchanges.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

            //------- fgCommandBuffer ----------------------------
            fgCommandBuffer.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgCommandBuffer.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");

            //------- fgSelectedContracts ----------------------------
            fgSelectedContracts.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgSelectedContracts.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
                        
            if (iRightsLevel == 1) tsbInform.Enabled = false;

            bCheckList = true;

            switch (sExtra)  {
                case "1":
                    cmbAdvisors.SelectedValue = 0;
                    cmbAdvisors.Enabled = true;
                    break;
                case "2":
                    cmbAdvisors.SelectedValue = Global.User_ID;
                    if (!Global.IsNumeric(cmbAdvisors.SelectedValue)) cmbAdvisors.SelectedIndex = 0;
                    cmbAdvisors.Enabled = false;
                    break;
                case "3":
                    cmbDivisions.SelectedValue = Global.Division;
                    if (!Global.IsNumeric(cmbDivisions.SelectedValue)) cmbDivisions.SelectedIndex = 0;
                    cmbDivisions.Enabled = false;
                    break;
            }

            ShowBusinessType();                                                                                             // iCommandType_ID = 1 - at start
            ShowList();
            bCheckList = true;

            dPoint2 = DateTime.Now;
            System.TimeSpan diffResult = dPoint2.ToUniversalTime().Subtract(dPoint1.ToUniversalTime());
            lblLoadTime.Text = diffResult.ToString();
            this.Refresh();
        }
        protected override void OnResize(EventArgs e)
        {
            tcBusinessTypes.Width = this.Width - 25;

            fgList.Width = this.Width - 26;
            fgList.Height = this.Height - 250;
        }
        #endregion
        #region --- Toolbar functions -----------------------------------------------------------------------------
        private void tsbTransfer_Click(object sender, EventArgs e)
        {
            frmTransfer locTransfer = new frmTransfer();
            if (dToday.Value.Date < DateTime.Now.Date) locTransfer.DateFrom = dToday.Value;
            else locTransfer.DateFrom = dToday.Value.AddDays(-1);
            locTransfer.ShowDialog();

            frmTransmissionList locTransmissionList = new frmTransmissionList();
            locTransmissionList.Today = dToday.Value;   // DateTime.Now;
            locTransmissionList.ShowDialog();

            DefineList();
        }
        private void tsbBasket_Click(object sender, EventArgs e)
        {
            frmOrderBasket locOrderBasket = new frmOrderBasket();
            locOrderBasket.Today = dToday.Value;
            locOrderBasket.ShowDialog();
            DefineList();
        }
        private void tsbCreatePDF_Click(object sender, EventArgs e)
        {

        }
        private void tslFX_Click(object sender, EventArgs e)
        {
            frmDailyFX locDailyFX = new frmDailyFX();
            locDailyFX.Mode = 3;
            locDailyFX.RightsLevel = iRightsLevel;
            locDailyFX.Extra = "";
            locDailyFX.Show();
        }

        private void tsbInform_Click(object sender, EventArgs e)
        {
            frmClientInforming locClientInforming = new frmClientInforming();
            locClientInforming.Business = 1;                                               //  1 - Securuties, 2 - FX, 3 - LL
            locClientInforming.AktionDate = dToday.Value;
            locClientInforming.Provider_ID = Convert.ToInt32(cmbProviders.SelectedValue);
            //locClientInforming.Advisor_ID = Convert.ToInt32(cmbAdvisors.SelectedValue);
            //locClientInforming.Aktion = Convert.ToInt32(cmbActions.SelectedIndex);
            locClientInforming.Code = fgList[fgList.Row, "Code"] + "";
            locClientInforming.ShowDialog();

            //DefineList();            
        }
        private void tsbTransmission_Click(object sender, EventArgs e)
        {
            frmTransmissionList locTransmissionList = new frmTransmissionList();
            locTransmissionList.Today = dToday.Value;   // DateTime.Now;
            locTransmissionList.ShowDialog();
            DefineList();
        }

        private void tslDPMOrders_Click(object sender, EventArgs e)
        {
            int j = jj;                         
            DefinePreOrdersList();

            // so j - "old" DPMorders Count, jj - "new" DPMorders Count
            if (jj != j) MessageBox.Show("DPM Orders count was changed", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            frmDPMBuffer locDPMBuffer = new frmDPMBuffer();
            locDPMBuffer.DateFrom = dToday.Value.AddDays(-7);
            locDPMBuffer.DateTo = dToday.Value;
            locDPMBuffer.ShowDialog();
            DefineList();
        }
        private void txtShareTitle_TextChanged(object sender, EventArgs e)
        {

        }
        private void tslPreOrders_Click(object sender, EventArgs e)
        {
            iPreClient_ID = 0;
            chkPreOrders.Checked = false;
            txtFilter.Text = "";
            cmbRecieveMethod3.SelectedValue = 1;                                // 1 - Telephone
            DefinePreOrdersList();
            Empty_PreOrder();
            fgPreOrders.Row = 0;
            picPre_PriceUp.Visible = false;
            txtPre_PriceUp.Visible = false;
            picPre_PriceDown.Visible = false;
            txtPre_PriceDown.Visible = false;
            panButtons.Enabled = false;
            panPreOrders.Top = (Screen.PrimaryScreen.Bounds.Height - panPreOrders.Height) / 2;
            panPreOrders.Left = (Screen.PrimaryScreen.Bounds.Width - panPreOrders.Width) / 2;
            panPreOrders.Visible = true;
        }
        #endregion
        #region --- Header functions ----------------------------------------------------
        private void dToday_ValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) ShowList();

            if (Global.AllowInsertOldOrders == 0 || dToday.Value.Date == DateTime.Now.Date) btnSave.Enabled = true;
            else btnSave.Enabled = false;
        }
        private void tcBusinessTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmptyCommand();
            
            ucCS.Filters = "Status = 1";

            mnuContext.Items[0].Visible = false;
            mnuContext.Items[1].Visible = false;
            mnuContext.Items[6].Visible = false;

            switch (Convert.ToInt32(tcBusinessTypes.SelectedIndex))
            {
                case 0:                                                          // "tpRTO":
                    mnuContext.Items[0].Visible = true;
                    mnuContext.Items[1].Visible = true;
                    ucCS.Visible = true;
                    cmbDiaxiristes.Visible = false;
                    panDPM.Visible = false;
                    btnSaveTransfer.Visible = false;
                    iBusinessType_ID = 1;
                    iCommandType_ID = 1;
                    ucCS.ListType = 2;
                    ucCS.Visible = true;
                    ShowBusinessType();
                    ShowList();
                    break;
                case 1:                                                            // "tpDPM":
                    //ucCS.Filters = "Status = 1 AND Service_ID = 3 AND User4_ID = " + Global.User_ID;
                    cmbDiaxiristes.SelectedValue = Global.User_ID;
                    cmbDiaxiristes.Visible = true;
                    ucCS.Visible = false;
                    panDPM.Visible = true;
                    bCheckList = true;
                    btnSaveTransfer.Visible = true;
                    iBusinessType_ID = 1;
                    iCommandType_ID = 4;
                    ucCS.ListType = 2;
                    ucCS.Visible = true;
                    ShowBusinessType();
                    ShowList();
                    break;
                case 2:                                                              //   "tpBulk":
                    ucCS.Visible = true;
                    cmbDiaxiristes.Visible = false;
                    panDPM.Visible = false;
                    btnSaveTransfer.Visible = false;
                    iBusinessType_ID = 1;
                    ucCS.ListType = 3;
                    ucCS.Visible = true;
                    iCommandType_ID = 3;
                    ShowBusinessType();
                    ShowList();
                    break;
                case 3:                                                            //  "tpExecution":
                    mnuContext.Items[6].Visible = true;
                    ucCS.Visible = true;
                    cmbDiaxiristes.Visible = false;
                    panDPM.Visible = false;
                    btnSaveTransfer.Visible = false;
                    iBusinessType_ID = 2;
                    iCommandType_ID = 2;
                    ucCS.ListType = 2;
                    ucCS.Visible = true;
                    ShowBusinessType();
                    ShowList();
                    break;
            }
        }
        private void lnkPelatis_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmClientData locClientData = new frmClientData();
            locClientData.Client_ID = iClient_ID;
            locClientData.Text = Global.GetLabel("customer_information");
            locClientData.Show();
        }

        private void lnkPortfolio_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmContract locContract = new frmContract();
            locContract.Aktion = 1;
            locContract.Contract_ID = iContract_ID;
            locContract.Contract_Details_ID = Convert.ToInt32(iContract_Details_ID);
            locContract.Contract_Packages_ID = Convert.ToInt32(iContract_Packages_ID);
            locContract.Client_ID = Convert.ToInt32(iClient_ID);
            locContract.ClientFullName = lnkPelatis.Text;
            locContract.RightsLevel = iRightsLevel;
            locContract.ShowDialog();
        }
        private void btnCleanUp_Click(object sender, EventArgs e)
        {
            EmptyCommand();

            lblQuantity.Text = Global.GetLabel("quantity");
            lblType.Visible = true;
            lstType.Visible = true;

            fgCommandBuffer.Rows.Count = 1;

            ShowList();
        }  
        private void txtAction_TextChanged(object sender, EventArgs e)
        {
            if (txtAction.Text.Length > 0) {
                switch (txtAction.Text.Substring(0, 1))  {
                    case "B":
                    case "b":
                    case "Β":
                    case "β":
                    case "A":
                    case "a":
                    case "Α":
                    case "α":
                        txtAction.Text = "BUY";
                        panSecurities.BackColor = Color.PaleGreen;
                        panSecurities.Visible = true;
                        ucPS.ShowNonAccord = true;
                        if (iContractService_ID == 3)
                            ucPS.BlockNonRecommended = true;                                           // true - means Block non recommednded products selection 
                        else                            
                            ucPS.BlockNonRecommended = false;                                                   // true - means Block non recommednded products selection 
                        ucPS.Focus();
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
                        panSecurities.BackColor = Color.LightCoral;
                        panSecurities.Visible = true;
                        ucPS.ShowNonAccord = false;
                        ucPS.BlockNonRecommended = false;
                        ucPS.Focus();
                        break;
                   default:
                        panSecurities.BackColor = Color.Silver;
                        panSecurities.Visible = true;          
                        break;
                }
            }
        }
        private void txtAction_LostFocus(object sender, EventArgs e)
        {
            if (txtAction.Text.Trim() != "")
            {
                if (txtAction.Text == "BUY")
                {
                    dtList4.Rows.Clear();
                    Global.DefineContractProductsList(dtList4, iContract_ID, iContract_Details_ID, iContract_Packages_ID, false);
                    dtList4.DefaultView.Sort = "CodeTitle";
                    dtList4 = dtList4.DefaultView.ToTable();

                    ucPS.ListType = 1;                                                                 // iListType = 1 : Global.dtProducts - common list of products, iListType = 2 : dtProductsContract - list of products for current contract
                    ucPS.ShowNonAccord = true;                                                         // true - means Show NonAccordable products (oxi katallila) with red Background
                    ucPS.BlockNonRecommended = true;                                                   // true - means Block non recommednded products selection 
                    ucPS.ProductsContract = dtList4;
                    ucPS.Focus();
                }
                else
                {
                    if (txtAction.Text == "SELL")
                    {
                        ucPS.ListType = 1;                                                              // iListType = 1 : Global.dtProducts - common list of products, iListType = 2 : dtProductsContract - list of products for current contract
                        ucPS.ShowNonAccord = false;                                                     // false - means Show NonAccordable products (oxi katallila) with white Background
                        ucPS.BlockNonRecommended = false;                                               // false - means not Block non recommednded products selection 
                        ucPS.Focus();
                    }
                    else
                    {
                        Console.Beep();
                        btnSave.Enabled = false;
                        ucPS.ShowNonAccord = false;                                                     // false - means Show NonAccordable products (oxi katallila) with white Background
                        txtAction.Focus();
                    }
                }
            }
        }
        private void lstType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (lstType.SelectedIndex) {
                case 0:                          // Limit
                    lblPrice.Visible = true;
                    txtPrice.Visible = true;
                    txtPrice.Enabled = true;
                    lblCurr.Visible = true;

                    imgPriceUp.Visible = false;
                    txtPriceUp.Visible = false;
                    imgPriceDown.Visible = false;
                    txtPriceDown.Visible = false;

                    lblConstant.Visible = true;
                    cmbConstant.Visible = true;
                    break;
                case 1:                          // Market
                    lblPrice.Visible = true;
                    txtPrice.Visible = true;
                    txtPrice.Text = "0";
                    txtPrice.Enabled = false;
                    lblCurr.Visible = false;

                    imgPriceUp.Visible = false;
                    txtPriceUp.Visible = false;
                    imgPriceDown.Visible = false;
                    txtPriceDown.Visible = false;

                    lblConstant.Visible = false;
                    cmbConstant.Visible = false;
                    break;

                case 2:                          // Stop
                    lblPrice.Visible = true;
                    txtPrice.Visible = true;
                    txtPrice.Enabled = true;
                    lblCurr.Visible = true;

                    imgPriceUp.Visible = false;
                    txtPriceUp.Visible = false;
                    imgPriceDown.Visible = false;
                    txtPriceDown.Visible = false;

                    lblConstant.Visible = true;
                    cmbConstant.Visible = true;
                    break;

                case 3:                          // Scenario
                    lblPrice.Visible = true;
                    txtPrice.Visible = true;
                    txtPrice.Enabled = true;
                    lblCurr.Visible = true;
                    cmbConstant.SelectedIndex = 1;

                    if (txtAction.Text == "BUY") {
                        imgPriceUp.Visible = true;
                        txtPriceUp.Visible = true;
                        imgPriceDown.Visible = true;
                        txtPriceDown.Visible = true;

                        lblConstant.Visible = true;
                        cmbConstant.Visible = true;
                    }
                    else {
                        imgPriceUp.Visible = false;
                        txtPriceUp.Visible = false;
                        imgPriceDown.Visible = true;
                        txtPriceDown.Visible = true;

                        lblConstant.Visible = true;
                        cmbConstant.Visible = true;
                    }
                    break;

                case 4:                                            // ATC, ATO
                case 5:
                    lblPrice.Visible = true;
                    txtPrice.Visible = true;
                    txtPrice.Text = "0";
                    txtPrice.Enabled = false;
                    lblCurr.Visible = false;
                    break;
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
                if (iProduct_ID == 6)
                    if (txtQuantity.Text != "0") txtAmount.Text = "0";
            }
        }
        private void txtAmount_LostFocus(object sender, EventArgs e)
        {
            if (!Global.IsNumeric(txtAmount.Text) || txtAmount.Text.IndexOf(".") > 0) {
                txtAmount.BackColor = Color.Red;
                txtAmount.Focus();
            }
            else {
                txtAmount.BackColor = Color.White;
                if (iProduct_ID == 6)
                    if (txtAmount.Text != "0") txtAmount.Text = "0";
            }
        }
        private void cmbConstant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConstant.SelectedIndex == 2) {
                dConstant.Value = DateTime.Now;
                dConstant.Visible = true;
                dConstant.Focus();
            }
            else {
                dConstant.Visible = false;
                btnSave.Focus();
            }
        }
        #endregion
        #region --- mnuContext --------------------------------------------------
        private void CopyISIN_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 1) Clipboard.SetDataObject(fgList[fgList.Row, "ISIN"], true, 10, 100);
        }
        private void mnuShowProduct_Click(object sender, EventArgs e)
        {
            frmProductData locProductData = new frmProductData();
            locProductData.ShareCode_ID = iShare_ID;
            locProductData.Product_ID = iProduct_ID;
            locProductData.Text = Global.GetLabel("product");
            locProductData.Show();
        }
        private void CopyReuters_Click(object sender, EventArgs e)
        {

        }

        private void CopyBloomberg_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region --- Filters ---------------------------------------------------------------------
        private void cmbProviders_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) ShowList();
        }
        private void cmbAdvisors_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) ShowList();
        }
        private void cmbDivisions_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) ShowList();

        }
        private void cmbActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bCheckList) ShowList();
        }
        private void cmbUsers_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) ShowList();
        }
        private void cmbSent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bCheckList) ShowList();
        }
        private void cmbDiax_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bCheckList) ShowList();
        }
        private void cmbServices_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) {
                iService_ID = Convert.ToInt32(cmbServices.SelectedValue);
                ShowList();
            }
        }
        private void cmbChecked_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bCheckList) {
                iCheck = Convert.ToInt32(cmbChecked.SelectedIndex);
                ShowList();
            }
        }
        private void chkShowCancelled_CheckedChanged(object sender, EventArgs e)
        {
            if (bCheckList) {
                iShowCancelled = (chkShowCancelled.Checked? 1 : 0);
                ShowList();
            }
        }

        private void rbAP_CheckedChanged(object sender, EventArgs e)
        {
            if (bCheckList) ShowList();
        }

        private void rbA_CheckedChanged(object sender, EventArgs e)
        {
            if (bCheckList) ShowList();
        }

        private void rbP_CheckedChanged(object sender, EventArgs e)
        {
            if (bCheckList) ShowList();
        }
        private void lnkISIN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (iShare_ID != 0) {
                frmProductData locProductData = new frmProductData();
                locProductData.Product_ID = iProduct_ID;
                locProductData.ShareCode_ID = iShare_ID;
                locProductData.Text = Global.GetLabel("product");
                locProductData.Show();
            }
        }
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            //ShowList();
            lblSEStar.Visible = true;
            cmbProductType.SelectedValue = 0;
            cmbProductType.SelectedValue = 1;
            txtCodeTitle.Text = "";
            txtCodeISIN.Text = "";
            txtReutersCode.Text = "";
            cmbStockExchanges.SelectedValue = 0;
            dFrom.Value = Convert.ToDateTime("01/01/" + DateTime.Now.Year);
            lblISIN_Warning.Text = "";

            panNewProduct.Visible = true;
        }



        private void btnAddCommand_Click(object sender, EventArgs e)
        {
            i = fgCommandBuffer.Rows.Count;
            fgCommandBuffer.AddItem(i + "\t" + txtAction.Text + "\t" + sProductTitle + "\t" + ucPS.txtShareTitle.Text + "\t" + lblShareCode.Text + "\t" +
                             lnkISIN.Text + "\t" + Global.ShowPrices(lstType.SelectedIndex, Convert.ToSingle((Global.IsNumeric(txtPrice.Text) ? txtPrice.Text : "0"))) + "\t" +
                             txtQuantity.Text + "\t" + txtAmount.Text + "\t" + lblCurr.Text + "\t" + cmbConstant.Text + "\t" + sStockExchange_Code + "\t" +
                             iClient_ID + "\t" + iStockExchange_ID + "\t" + iShare_ID + "\t" + iContract_ID + "\t" +
                             iProduct_ID + "\t" + iProductCategory_ID + "\t" + lstType.SelectedIndex + "\t" +
                             txtPriceUp.Text + "\t" + txtPriceDown.Text + "\t" + cmbConstant.SelectedIndex + "\t" + dConstant.Value.ToString("yyyy/MM/dd"));

            txtAction.Text = "";
            iShare_ID = 0;
            ucPS.ShowProductsList = false;
            ucPS.txtShareTitle.Text = "";
            ucPS.ShowProductsList = true;
            lnkISIN.Text = "";
            lblShareCode.Text = "";
            iProduct_ID = 0;
            iProductCategory_ID = 0;
            txtQuantity.Text = "";
            lstType.SelectedIndex = 0;
            txtPrice.Text = "";
            txtPriceUp.Text = "";
            txtPriceDown.Text = "";
            txtAmount.Text = "";
            lblCurr.Text = "";
            cmbConstant.SelectedIndex = 0;
            dConstant.Value = Convert.ToDateTime("1900/01/01");
            cmbRecieveMethod2.SelectedValue = 0;
            txtRecieveVoicePath.Text = "";
            panMultiProducts.Visible = true;

            txtAction.Focus();
        }
        private void cmbServiceProviders_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) {
                foundRows = Global.dtServiceProviders.Select("ID = " + cmbServiceProviders.SelectedValue);
                if (foundRows.Length > 0) 
                    iProviderType = Convert.ToInt32(foundRows[0]["ProviderType"]);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            int j = 0;
 
            var EXL = new Microsoft.Office.Interop.Excel.Application();
            var oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            // System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("el-GR");
            EXL.Workbooks.Add();
            EXL.ScreenUpdating = false;
            EXL.Caption = Global.AppTitle;

            var loopTo = fgList.Rows.Count - 1;
            for (this.i = 1; this.i <= loopTo; this.i++) {
                j = j + 1;
                EXL.Cells[i + 1, 1].Value = fgList[i, 1];
                EXL.Cells[i + 1, 2].Value = fgList[i, 2];
                EXL.Cells[i + 1, 3].Value = fgList[i, 3];
                EXL.Cells[i + 1, 4].Value = fgList[i, 4];
                EXL.Cells[i + 1, 5].Value = fgList[i, 5];
                EXL.Cells[i + 1, 6].Value = fgList[i, 6];
                EXL.Cells[i + 1, 7].Value = fgList[i, 7];
                EXL.Cells[i + 1, 8].Value = fgList[i, 8];
                EXL.Cells[i + 1, 9].Value = fgList[i, 9];
                EXL.Cells[i + 1, 10].Value = fgList[i, 10];
                EXL.Cells[i + 1, 11].Value = fgList[i, 11];
                EXL.Cells[i + 1, 12].Value = fgList[i, 12];
                EXL.Cells[i + 1, 13].Value = (Global.IsNumeric(fgList[i, "Price"])? Convert.ToDecimal(fgList[i, "Price"]).ToString("0.00###") : fgList[i, "Price"]+"") ;  
                EXL.Cells[i + 1, 14].Value = (Global.IsNumeric(fgList[i, "Quantity"]) ? Convert.ToDecimal(fgList[i, "Quantity"]).ToString("0.00###") : "");
                EXL.Cells[i + 1, 15].Value = (Global.IsNumeric(fgList[i, "Amount"]) ? Convert.ToDecimal(fgList[i, "Amount"]).ToString("0.00###") : "");
                EXL.Cells[i + 1, 16].Value = (Global.IsNumeric(fgList[i, "RealPrice"]) ? Convert.ToDecimal(fgList[i, "RealPrice"]).ToString("0.00###") : "");
                EXL.Cells[i + 1, 17].Value = (Global.IsNumeric(fgList[i, "RealQuantity"]) ? Convert.ToDecimal(fgList[i, "RealQuantity"]).ToString("0.00###") : "");
                EXL.Cells[i + 1, 18].Value = (Global.IsNumeric(fgList[i, "RealAmount"]) ? Convert.ToDecimal(fgList[i, "RealAmount"]).ToString("0.00###") : "");
                EXL.Cells[i + 1, 19].Value = fgList[i, 19];
                EXL.Cells[i + 1, 20].Value = fgList[i, 20];
                EXL.Cells[i + 1, 21].Value = fgList[i, 21];
                EXL.Cells[i + 1, 22].Value = fgList[i, 22];
                EXL.Cells[i + 1, 23].Value = fgList[i, 23];
                EXL.Cells[i + 1, 24].Value = fgList[i, 24];
                EXL.Cells[i + 1, 25].Value = fgList[i, 25];
                EXL.Cells[i + 1, 26].Value = fgList[i, 26];
                EXL.Cells[i + 1, 27].Value = fgList[i, 27];
                EXL.Cells[i + 1, 28].Value = fgList[i, 28];
                EXL.Cells[i + 1, 29].Value = fgList[i, 29];
                EXL.Cells[i + 1, 30].Value = fgList[i, 30];
                EXL.Cells[i + 1, 31].Value = fgList[i, 31];
                EXL.Cells[i + 1, 32].Value = fgList[i, 32];
                EXL.Cells[i + 1, 33].Value = fgList[i, 33];
                EXL.Cells[i + 1, 34].Value = fgList[i, 34];
                EXL.Cells[i + 1, 35].Value = fgList[i, 35];
                EXL.Cells[i + 1, 36].Value = fgList[i, 36];
                EXL.Cells[i + 1, 37].Value = fgList[i, 37];
                EXL.Cells[i + 1, 38].Value = fgList[i, 38];
                EXL.Cells[i + 1, 39].Value = fgList[i, 39];
                EXL.Cells[i + 1, 40].Value = fgList[i, 40];
                EXL.Cells[i + 1, 41].Value = fgList[i, 41];
                EXL.Cells[i + 1, 42].Value = fgList[i, 42];
                EXL.Cells[i + 1, 43].Value = fgList[i, 43];
                EXL.Cells[i + 1, 44].Value = fgList[i, 44];
                EXL.Cells[i + 1, 45].Value = fgList[i, 45];
                EXL.Cells[i + 1, 46].Value = fgList[i, 46];
                EXL.Cells[i + 1, 47].Value = fgList[i, 47];
                EXL.Cells[i + 1, 48].Value = fgList[i, 48];
                EXL.Cells[i + 1, 49].Value = fgList[i, 49];
                EXL.Cells[i + 1, 50].Value = fgList[i, 50];
                EXL.Cells[i + 1, 51].Value = fgList[i, 51];
                EXL.Cells[i + 1, 52].Value = fgList[i, 52];
                EXL.Cells[i + 1, 53].Value = fgList[i, 53];
                EXL.Cells[i + 1, 54].Value = fgList[i, 54];
                EXL.Cells[i + 1, 55].Value = fgList[i, 55];
                EXL.Cells[i + 1, 56].Value = fgList[i, 56];
                EXL.Cells[i + 1, 57].Value = fgList[i, 57];
                EXL.Cells[i + 1, 58].Value = fgList[i, 58];
                EXL.Cells[i + 1, 59].Value = fgList[i, 59];
                EXL.Cells[i + 1, 60].Value = fgList[i, 60];
                EXL.Cells[i + 1, 61].Value = fgList[i, 61];
                EXL.Cells[i + 1, 62].Value = fgList[i, 62];
                EXL.Cells[i + 1, 63].Value = fgList[i, 63];
                EXL.Cells[i + 1, 64].Value = fgList[i, 64];
                EXL.Cells[i + 1, 65].Value = fgList[i, 65];
                EXL.Cells[i + 1, 66].Value = fgList[i, 66];
                EXL.Cells[i + 1, 67].Value = fgList[i, 67];
                EXL.Cells[i + 1, 68].Value = fgList[i, 68];
                EXL.Cells[i + 1, 69].Value = fgList[i, 69];
                EXL.Cells[i + 1, 70].Value = (Global.IsNumeric(fgList[i, "CurrencyRate"]) ? Convert.ToDecimal(fgList[i, "CurrencyRate"]).ToString("0.00####").Replace(",", ".") : "");
                EXL.Cells[i + 1, 71].Value = (Global.IsNumeric(fgList[i, "AxiaEUR"]) ? Convert.ToDecimal(fgList[i, "AxiaEUR"]).ToString("0.00").Replace(",", ".") : "");
            }

            EXL.Cells[2, 13].Value = "Τιμή";
            EXL.Cells[2, 14].Value = "Ποσότητα";
            EXL.Cells[2, 15].Value = "Αξία";
            EXL.Cells[2, 16].Value = "Τιμή";
            EXL.Cells[2, 17].Value = "Ποσότητα";
            EXL.Cells[2, 18].Value = "Αξία";
            EXL.Cells[2, 70].Value = "Ισοτιμία";
            EXL.Cells[2, 71].Value = "Αξία σε EUR";

            EXL.ScreenUpdating = true;
            EXL.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;

            this.Cursor = Cursors.Default;
        }
        private void btnExcel2_Click(object sender, EventArgs e)
        {
            sTemp = Application.StartupPath + @"\Temp\CommandsSearch_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
            fgList.SaveExcel(sTemp, "Sheet1", C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells); //  | FileFlags.IncludeMergedRanges | FileFlags.AsDisplayed | C1.Win.C1FlexGrid.FileFlags.VisibleOnly);
            Process.Start(sTemp);
        }
        #endregion

        #region --- New Product functions -------------------------------------------------------------------------
        private void cmbProductType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList)
            {
                if (Convert.ToInt32(cmbProductType.SelectedValue) != 0)
                {
                    //-------------- Define Products Categories List ------------------
                    dtView = Global.dtProductsCategories.Copy().DefaultView;
                    dtView.RowFilter = "Product_ID = " + cmbProductType.SelectedValue;
                    cmbProductCategory.DataSource = dtView;
                    cmbProductCategory.DisplayMember = "Title";
                    cmbProductCategory.ValueMember = "ID";

                    if (Convert.ToInt32(cmbProductType.SelectedValue) == 6) lblSEStar.Visible = false;
                    else lblSEStar.Visible = true;
                }
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            DefinePreOrdersList();
        }
        private void txtCodeISIN_LostFocus(object sender, EventArgs e)
        {
            if (txtCodeISIN.Text.Trim() != "") {
                clsProductsTitles klsProductTitle = new clsProductsTitles();
                klsProductTitle.ISIN = txtCodeISIN.Text;
                klsProductTitle.GetRecord_ISIN();
                if (klsProductTitle.Record_ID == 0 || klsProductTitle.Record_ID == iShare_ID) {
                    btnSaveProduct.Enabled = true;
                    lblISIN_Warning.Text = "";
                }
                else {
                    btnSaveProduct.Enabled = false;
                    lblISIN_Warning.Text = "Το ISIN υπάρχει ήδη καταχωρημένο";
                    txtCodeISIN.Focus();
                }
            }
        }

        private void mnuFIXReport_Click(object sender, EventArgs e)
        {
            frmFIXReport locFIXReport = new frmFIXReport();
            locFIXReport.ServiceProvider_ID = Convert.ToInt32(fgList[fgList.Row, "Provider_ID"]);
            locFIXReport.ClOrdID = fgList[fgList.Row, "ID"] + (Convert.ToInt32(fgList[fgList.Row, "Status"]) == -1 ?  "C" : "");
            locFIXReport.ShowDialog();
        }

        private void cmbPre_Constant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbPre_Constant.SelectedIndex) == 2) dPre_Constant.Visible = true;
            else dPre_Constant.Visible = false;
        }

        private void chkPreOrders_CheckedChanged(object sender, EventArgs e)
        {
            for (i = 1; i <= fgPreOrders.Rows.Count - 1; i++) fgPreOrders[i, 0] = chkPreOrders.Checked;

            if (chkPreOrders.Checked)
            {
                panButtons.Enabled = true;
                iCheckedRows = fgPreOrders.Rows.Count - 1;
                if (fgPreOrders.Rows.Count > 1) {
                    fgPreOrders.Row = 1;
                    fgPreOrders.Focus();
                }
            }
            else  {
                panButtons.Enabled = false;
                iCheckedRows = 0;
            }
            Empty_PreOrder();
        }

        private void chkStockExchages_CheckedChanged(object sender, EventArgs e)
        {
            for (i = 1; i <= fgStockExchanges.Rows.Count - 1; i++) fgStockExchanges[i, 0] = chkStockExchanges.Checked;
        }
        private void btnSaveProduct_Click(object sender, EventArgs e)
        {
            if ((txtCodeTitle.Text == "") || (txtCodeISIN.Text == "") || (txtReutersCode.Text == "") ||
                ((Convert.ToInt32(cmbStockExchanges.SelectedValue) == 0) && (Convert.ToInt32(cmbProductType.SelectedValue) != 6)) || (cmbCurrency.Text == ""))
                MessageBox.Show("Συμπληρώστε όλα τα απαραίτητα παιδία", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                if (txtCodeTitle.Text + "" == "") txtCodeTitle.Text = txtCodeTitle.Text + "";
                if (txtReutersCode.Text + "" == "") txtReutersCode.Text = txtReutersCode.Text + "";

                this.Refresh();
                this.Cursor = Cursors.WaitCursor;

                clsProducts klsProduct = new clsProducts();
                klsProduct.Product_ID = Convert.ToInt32(cmbProductType.SelectedValue);
                klsProduct.Aktive = 1;
                iShare_ID = klsProduct.InsertRecord();

                clsProductsTitles klsProductTitle = new clsProductsTitles();
                klsProductTitle.ProductTitle = txtCodeTitle.Text + "";
                klsProductTitle.ISIN = txtCodeISIN.Text + "";
                if (Convert.ToInt32(cmbProductType.SelectedValue) != 2) klsProductTitle.BondType = 0;    // isn't BOND, so BondType = 0
                klsProductTitle.Share_ID = iShare_ID;
                klsProductTitle.ProductCategory = Convert.ToInt32(cmbProductCategory.SelectedValue);
                klsProductTitle.LastEditDate = DateTime.Now;
                klsProductTitle.LastEditUser_ID = Global.User_ID;
                iShareTitle_ID = klsProductTitle.InsertRecord();

                clsProductsCodes ProductCode = new clsProductsCodes();
                ProductCode.Share_ID = iShare_ID;
                ProductCode.DateFrom = dFrom.Value;
                ProductCode.DateTo = Convert.ToDateTime("2070-12-31");
                ProductCode.CodeTitle = txtCodeTitle.Text + "";
                ProductCode.ISIN = txtCodeISIN.Text + "";
                ProductCode.Code = txtReutersCode.Text + "";
                ProductCode.Code3 = "";
                ProductCode.StockExchange_ID = Convert.ToInt32(cmbStockExchanges.SelectedValue);
                ProductCode.CountryAction = 0;
                ProductCode.Curr = cmbCurrency.Text;
                if (iProduct_ID == 2)
                {
                    ProductCode.StockExchange_ID = 21;                   // 21 - OTC
                    ProductCode.CountryAction = 27;                      // 27 - Global
                    ProductCode.PrimaryShare = 0;
                    ProductCode.CurrencyHedge = 0;
                    ProductCode.CurrencyHedge2 = "";
                    ProductCode.DistributionStatus = "";
                    ProductCode.FrequencyClipping = 0;
                    ProductCode.CountryIssue = 0;
                    ProductCode.StockExchange_Issue_ID = 0;
                    ProductCode.Date1 = Convert.ToDateTime("1900/01/01");
                    ProductCode.Date2 = Convert.ToDateTime("1900/01/01");
                    ProductCode.Date3 = Convert.ToDateTime("1900/01/01");
                    ProductCode.Date4 = Convert.ToDateTime("1900/01/01");
                }
                if (iProduct_ID == 6) {
                    ProductCode.StockExchange_ID = 21;                  // 21 - OTC
                    ProductCode.CountryAction = 27;                     // 27 - Global
                }

                ProductCode.MonthDays = "";
                ProductCode.BaseDays = "";
                ProductCode.CouponeType = 0;
                ProductCode.Coupone = 0;
                ProductCode.LastCoupone = 0;
                ProductCode.Price = 0;
                ProductCode.FrequencyClipping = 0;
                ProductCode.RevocationRight = 0;
                ProductCode.QuantityMin = 0;
                ProductCode.QuantityStep = 0;
                ProductCode.CoveredBond = 0;
                ProductCode.FloatingRate = "0";
                ProductCode.Limits = 0;
                ProductCode.MIFID_Risk = "";
                ProductCode.DateIPO = Convert.ToDateTime("1900/01/01");
                ProductCode.Aktive = 2;
                iShareCode_ID = ProductCode.InsertRecord();

                clsProductsTitlesCodes klsProductTitleCode = new clsProductsTitlesCodes();
                klsProductTitleCode.DateFrom = dFrom.Value;
                klsProductTitleCode.DateTo = Convert.ToDateTime("2070/12/31");
                klsProductTitleCode.Share_ID = iShare_ID;
                klsProductTitleCode.ShareTitle_ID = iShareTitle_ID;
                klsProductTitleCode.ShareCode_ID = iShareCode_ID;
                klsProductTitleCode.InsertRecord();

                clsCashTables CashTable = new clsCashTables();
                CashTable.Record_ID = 41;                                                    // ListsTables.ID = 41 - ShareCodes
                CashTable.GetRecord();
                CashTable.LastEdit_Time = DateTime.Now;
                CashTable.LastEdit_User_ID = Global.User_ID;
                CashTable.EditRecord();

                Global.GetProductsList();

                this.Cursor = Cursors.Default;

                lblShareCode.Text = txtReutersCode.Text;
                lnkISIN.Text = txtCodeISIN.Text;
                ucPS.ShowProductsList = false;
                ucPS.txtShareTitle.Text = txtCodeTitle.Text;
                ucPS.ShowProductsList = true;
                lblCurr.Text = cmbCurrency.Text;
                iShare_ID = iShareCode_ID;
                iStockExchange_ID = Convert.ToInt32(cmbStockExchanges.SelectedValue);
                iProduct_ID = Convert.ToInt32(cmbProductType.SelectedValue);
                iProductCategory_ID = Convert.ToInt32(cmbProductCategory.SelectedValue);

                panNewProduct.Visible = false;
            } 
        }
        private void btnCancelProduct_Click(object sender, EventArgs e)
        {
            panNewProduct.Visible = false;
        }
        #endregion
        #region --- MultiProduct functions ----------------------------------------------------------------
        private void picCloseCommandBuffer_Click(object sender, EventArgs e)
        {
            panMultiProducts.Visible = false;
        }
        private void picRecieveVoicePath_Click(object sender, EventArgs e)
        {
            txtRecieveVoicePath.Text = Global.FileChoice(Global.DefaultFolder);
        }
        private void picPlayRecieveVoice_Click(object sender, EventArgs e)
        {
            try {
                System.Diagnostics.Process.Start(txtRecieveVoicePath.Text);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }
        #endregion
        #region --- Save functions ----------------------------------------------------------------
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Global.AllowInsertOldOrders == 1 || dToday.Value.Date == DateTime.Now.Date)
            {
                iBusinessType_ID = 1;                                                      // by default                   BusinessType = 1
                if (iProviderType == 1) iBusinessType_ID = 1;                              // 1 - CreditSuisse  
                if (iProviderType == 2) iBusinessType_ID = 2;                              // 2 - HF2S 
 
                SaveRecord();
            }
            else MessageBox.Show("Λάθος ημερομηνία εντολής", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void btnSaveTransfer_Click(object sender, EventArgs e)
        {
            iBusinessType_ID = 1;                                                          // by default                   BusinessType = 1
            if (iProviderType == 1) iBusinessType_ID = 1;                                  // 1 - CreditSuisse  
            if (iProviderType == 2) iBusinessType_ID = 2;                                  // 2 - HF2S 

            SaveRecord();                                                                 
        }
        private void SaveRecord()
        {
            sUploadFile = "";

            if (fgCommandBuffer.Rows.Count == 1) {
                if (lstType.SelectedIndex == 0 && (txtPrice.Text == "0" || txtPrice.Text == ""))                                  //Or (txtQuantity.Text = "0" Or txtQuantity.Text = "") 
                    MessageBox.Show("Συμπληρώστε όλα τα παιδία", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    if (iCommandType_ID == 4) iProvider_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);

                SaveTransaction(iBusinessType_ID, iCommandType_ID, 0, iClient_ID, lblCode.Text, lnkPortfolio.Text, iContract_ID,
                                iContract_Details_ID, iContract_Packages_ID, lnkPelatis.Text, txtAction.Text, dToday.Value, iProduct_ID, iProductCategory_ID,
                                iShare_ID, txtQuantity.Text, lstType.SelectedIndex, txtPrice.Text, txtPriceUp.Text, txtPriceDown.Text, txtAmount.Text, 
                                lblCurr.Text, cmbConstant.SelectedIndex, dConstant.Value.ToString("yyyy/MM/dd"),
                                Convert.ToInt32(iProvider_ID), iStockExchange_ID, sStockExchange_Code, 0, "", sProductTitle, "");
            }
            else {
                if (txtRecieveVoicePath.Text.Trim().Length > 0) {
                    sFileName = Path.GetFileName(txtRecieveVoicePath.Text.Trim());
                    sUploadFile = Global.DMS_UploadFile(txtRecieveVoicePath.Text.Trim(), "Customers/" + lnkPelatis.Text.Replace(".", "_") + "/OrdersAcception", sFileName);
                }

                for (i = 1; i <= fgCommandBuffer.Rows.Count - 1; i++) {
                    if (iCommandType_ID == 4) iProvider_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                    SaveTransaction(iBusinessType_ID, iCommandType_ID, 0, Convert.ToInt32(fgCommandBuffer[i, "Client_ID"]), lblCode.Text, lnkPortfolio.Text, 
                                    Convert.ToInt32(fgCommandBuffer[i, "Contract_ID"]), iContract_Details_ID, iContract_Packages_ID, lnkPelatis.Text,
                                    fgCommandBuffer[i, "Aktion"] + "", dToday.Value, Convert.ToInt32(fgCommandBuffer[i, "Product_ID"]), Convert.ToInt32(fgCommandBuffer[i, "ProductCategory_ID"]), 
                                    Convert.ToInt32(fgCommandBuffer[i, "Share_ID"]), fgCommandBuffer[i, "Quantity"] + "", Convert.ToInt32(fgCommandBuffer[i, "PriceType"]), 
                                    fgCommandBuffer[i, "Price"] + "", fgCommandBuffer[i, "PriceUp"] + "", fgCommandBuffer[i, "PriceDown"] + "", fgCommandBuffer[i, "Amount"] + "", 
                                    fgCommandBuffer[i, "Currency"] + "", Convert.ToInt32(fgCommandBuffer[i, "Constant"]), fgCommandBuffer[i, "ConstantDate"] + "", 
                                    Convert.ToInt32(iProvider_ID), Convert.ToInt32(fgCommandBuffer[i, "StockExchnage_ID"]), fgCommandBuffer[i, "SE_Code"] + "",
                                    Convert.ToInt32(cmbRecieveMethod2.SelectedValue), txtRecieveVoicePath.Text.Trim(), fgCommandBuffer[i, "Product_Title"] + "", "");
                }
            }

            EmptyCommand();
            ShowList();                                  // 1 - Securities
            if (fgList.Rows.Count > 2) fgList.Row = 2;
            fgList.Focus();

            lblQuantity.Text = Global.GetLabel("quantity");
            lblType.Visible = true;

            fgCommandBuffer.Rows.Count = 1;
            panMultiProducts.Visible = false;
        }
        private int SaveTransaction(int iBusinessType_ID, int iCommandType_ID, int iII_ID, int iClient_ID, string sCode, string sPortfolio,
                                    int iContract_ID, int iContract_Details_ID, int iContract_Packages_ID, string sClientName, string sAction, 
                                    DateTime dToday, int iProduct_ID, int iProductCategory_ID, int iShare_ID, string sQuantity, int iPriceType, 
                                    string sPrice, string sPriceUp, string sPriceDown, string sAmount, string sCurr,
                                    int iConstant, string sConstantDate, int iProvider_ID, int iStockExchange_ID, string sStockExchange_Code,
                                    int iRecieveMethod_ID, string sRecieveFile, string sProductTitle, string sNotes)
        {
            int k, iID, iBulcCommand_ID;
            string sError;
            clsOrdersSecurity Order = new clsOrdersSecurity();
            clsOrdersSecurity Order2 = new clsOrdersSecurity();

            iID = -1;
            sError = "";

            if (iCommandType_ID == 1)
                if (sPortfolio.Trim() == "") sError = sError + Global.GetLabel("enter_profitCenter_subacc") + (char)13;

            if (iProduct_ID == 0 || iProductCategory_ID == 0) sError = sError + Global.GetLabel("enter_your_product") + (char)13;

            if (sError.Length > 0) MessageBox.Show(sError, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                //--- define daily currency rate EUR/sCurr --------------------
                sgTemp1 = 0;
                if (sCurr == "EUR") sgTemp1 = 1;                                                            // CurrRate
                else {
                    foundRows = dtEURRates.Select("Currency = 'EUR" + sCurr + "='");
                    if (foundRows.Length > 0) sgTemp1 = Convert.ToSingle(foundRows[0]["Rate"]);             // CurrRate
                }

                dTemp = Convert.ToDateTime("1900/01/01");
                Order.BulkCommand = "";
                Order.BusinessType_ID = iBusinessType_ID;
                Order.CommandType_ID = iCommandType_ID;
                if (iCommandType_ID == 4) {
                    Order.Company_ID = Convert.ToInt32(cmbDiaxiristes.SelectedValue);
                    Order.RecieveDate = DateTime.Now;
                    Order.AllocationPercent = 0;                                                            //  CommandType_ID=4 (DPM Order) when insert it has AllocationPercent = 0
                }
                else {
                    Order.Company_ID = Global.Company_ID;
                    Order.Client_ID = iClient_ID;
                    Order.RecieveDate = DateTime.Now;
                    Order.AllocationPercent = 100;                                                          //  CommandType_ID<>4 (Non DPM Order) always has AllocationPercent = 100
                }
                Order.ServiceProvider_ID = iProvider_ID;
                Order.StockExchange_ID = iStockExchange_ID;
                Order.CustodyProvider_ID = iProvider_ID;
                Order.Executor_ID = 0;
                Order.II_ID = iII_ID;
                Order.Parent_ID = 0;
                Order.Contract_ID = iContract_ID;
                Order.Contract_Details_ID = iContract_Details_ID;
                Order.Contract_Packages_ID = iContract_Packages_ID;
                Order.Code = sCode;
                Order.ProfitCenter = sPortfolio;
                Order.Aktion = (sAction == "BUY" ? 1 : 2);
                Order.AktionDate = dToday;
                Order.Share_ID = iShare_ID;
                Order.Product_ID = iProduct_ID;
                Order.ProductCategory_ID = iProductCategory_ID;
                Order.PriceType = iPriceType;
                Order.Price = (Global.IsNumeric(sPrice) ? Convert.ToDecimal(sPrice) : 0);
                Order.Quantity = (Global.IsNumeric(sQuantity) ? Convert.ToDecimal(sQuantity) : 0);
                Order.Amount = (Global.IsNumeric(sAmount) ? Convert.ToDecimal(sAmount) : 0);
                Order.Curr = sCurr;
                Order.Constant = iConstant;
                Order.ConstantDate = ((iConstant == 2) ? Convert.ToDateTime(sConstantDate).ToString("yyyy/MM/dd") : "");
                Order.CurrRate = Convert.ToDecimal(sgTemp1);
                Order.MinFeesRate = Convert.ToDecimal(sgTemp1);
                Order.Notes = (sNotes == "/" ? "" : sNotes);
                Order.RecieveMethod_ID = iRecieveMethod_ID;
                Order.BestExecution = 1;
                Order.SentDate = Convert.ToDateTime("1900/01/01");
                Order.FIX_A = -1;                
                Order.FIX_RecievedDate = Convert.ToDateTime("1900/01/01");
                Order.ExecuteDate = Convert.ToDateTime("1900/01/01");
                Order.User_ID = Global.User_ID;
                Order.DateIns = DateTime.Now;
                iID = Order.InsertRecord();

                dTemp = Order.RecieveDate;
                AddRecievedFile(iID, iRecieveMethod_ID, sRecieveFile, sUploadFile);

                if (iCommandType_ID == 4 && iClient_ID != 0) {
                    iBulcCommand_ID = Order2.GetNextBulkCommand();
                    Order2.BulkCommand = "<" + (iBulcCommand_ID + "") + ">";
                    Order2.BusinessType_ID = iBusinessType_ID;
                    Order2.CommandType_ID = 1;
                    Order2.Client_ID = iClient_ID;
                    Order2.Company_ID = 0;
                    Order2.ServiceProvider_ID = iProvider_ID;
                    Order2.StockExchange_ID = iStockExchange_ID;
                    Order2.CustodyProvider_ID = iProvider_ID;
                    Order2.Depository_ID = 0;
                    Order2.II_ID = 0;
                    Order2.Parent_ID = 0;
                    Order2.Contract_ID = iContract_ID;
                    Order2.Contract_Details_ID = iContract_Details_ID;
                    Order2.Contract_Packages_ID = iContract_Packages_ID;
                    Order2.Code = sCode;
                    Order2.ProfitCenter = sPortfolio;
                    Order2.Aktion = (sAction == "BUY" ? 1 : 2);
                    Order2.AktionDate = dToday;
                    Order2.Share_ID = iShare_ID;
                    Order2.Product_ID = iProduct_ID;
                    Order2.ProductCategory_ID = iProductCategory_ID;
                    Order2.PriceType = iPriceType;
                    Order2.Price = (sPrice.Length == 0 ? 0 : Convert.ToDecimal(sPrice));
                    Order2.Quantity = (sQuantity.Length == 0 ? 0 : Convert.ToDecimal(sQuantity));
                    Order2.Amount = (sAmount.Length == 0 ? 0 : Convert.ToDecimal(sAmount));
                    Order2.Curr = sCurr;
                    Order2.Constant = iConstant;
                    Order2.ConstantDate = ((iConstant == 2) ? Convert.ToDateTime(sConstantDate).ToString("yyyy/MM/dd") : "");
                    Order2.RecieveMethod_ID = iRecieveMethod_ID;
                    Order2.RecieveDate = dTemp;                                                  // RecieveDate - date when RTO recieved this order - day when this order was sent to RTO. 1900/01/01 - means that order wasn't sent to RTO
                    Order2.SentDate = Convert.ToDateTime("1900/01/01");
                    Order2.FIX_A = -1;
                    Order2.ExecuteDate = Convert.ToDateTime("1900/01/01");
                    Order2.RealPrice = 0;
                    Order2.RealQuantity = 0;
                    Order2.RealAmount = 0; ;
                    Order2.CurrRate = Convert.ToDecimal(sgTemp1);
                    Order2.InformationMethod_ID = 7;                                             // 7 -  Προσωπικά for simple DMP orders
                    Order2.FeesCalcMode = 1;
                    Order2.User_ID = Global.User_ID;
                    Order2.DateIns = DateTime.Now;
                    Order2.InsertRecord();

                    Order.BulkCommand = "0/<" + (iBulcCommand_ID + "") + ">";
                    Order.EditRecord();
                }


                if (iPriceType == 3)                                             // only for Scenario
                    if (sAction == "BUY")  {
                        if (sPriceUp != "" && sPriceUp != "0") {
                            Order.Parent_ID = iID;
                            Order.Aktion = 2;
                            Order.Price = Convert.ToDecimal(sPriceUp);
                            if (Order.Product_ID == 2) Order.Amount = Order.Price * Order.Quantity / 100;
                            else Order.Amount = Order.Price * Order.Quantity / 100;
                            k = Order.InsertRecord();

                            if (sRecieveFile.Length > 0) AddRecievedFile(k, iRecieveMethod_ID, sRecieveFile, sUploadFile);
                        }

                        if (sPriceDown != "" && sPriceDown != "0") {
                            Order.Parent_ID = iID;
                            Order.Aktion = 2;
                            Order.Price = Convert.ToDecimal(sPriceDown);
                            if (Order.Product_ID == 2) Order.Amount = Order.Price * Order.Quantity / 100;
                            else Order.Amount = Order.Price * Order.Quantity;
                            k = Order.InsertRecord();

                            if (sRecieveFile.Length > 0) AddRecievedFile(k, iRecieveMethod_ID, sRecieveFile, sUploadFile);
                        }
                    }
            }
            return iID;
        }
        #endregion
        #region --- fgList functionality ---------------------------------------------------------------------
        private void ShowList()
        {
            if (bCheckList && iMode == 1) {

                sPreCode = "";
                sPreISIN = "";
                if (dToday.Value.Date == DateTime.Now.Date) dtEURRates = Global.dtTodayEURRates.Copy();
                else {
                    clsCurrencies klsCurrency = new clsCurrencies();
                    klsCurrency.DateFrom = dToday.Value.AddDays(-1);
                    klsCurrency.DateTo = dToday.Value.AddDays(-1);
                    klsCurrency.Code = "EUR";
                    klsCurrency.GetCurrencyRates_Period();
                    dtEURRates = klsCurrency.List.Copy();
                }

                switch (iCommandType_ID) {
                    case 1:                          // 1 - RTO list  
                        tsbInform.Visible = true;
                        tss3.Visible = true;
                        tsbTransmission.Visible = false;
                        tss4.Visible = false;
                        break;
                    case 2:                          // 2 - Exec List
                        tsbInform.Visible = false;
                        tss3.Visible = false;
                        tsbTransmission.Visible = true;
                        tss4.Visible = true;
                        break;
                    case 3:                          // 3 - Bulk List
                        tsbInform.Visible = false;
                        tss3.Visible = false;
                        tsbTransmission.Visible = false;
                        tss4.Visible = false;
                        break;
                    case 4:                          // 4 - DPM List
                        tsbInform.Visible = false;
                        tss3.Visible = false;
                        tsbTransmission.Visible = false;
                        tss4.Visible = false;
                        break;
                }

                toolLeft.Visible = true;

                mnuShowFile.Visible = false;
                DefineList();
            }
        }
        private void AddRecievedFile(int iCommand_ID, int iRecieveMethod_ID, string sRecieveFilePath, string sUploadFile)
        {
            clsOrders_Recieved Orders_Recieved = new clsOrders_Recieved();
            Orders_Recieved = new clsOrders_Recieved();
            Orders_Recieved.Command_ID = iCommand_ID;
            Orders_Recieved.DateIns = DateTime.Now;
            Orders_Recieved.Method_ID = iRecieveMethod_ID;
            Orders_Recieved.FilePath = sRecieveFilePath;
            Orders_Recieved.FileName = Path.GetFileName(sUploadFile);
            Orders_Recieved.SourceCommand_ID = iCommand_ID;
            Orders_Recieved.InsertRecord();
        }
        #endregion
        #region --- common functions ------------------------------------------------------------------------
        private void ShowBusinessType()
        {
            switch (iCommandType_ID) {
                case 1:
                    ucCS.Enabled = true;
                    lblCode.Text = "";
                    lnkPelatis.Text = "";
                    lnkPelatis.Enabled = true;
                    lnkPortfolio.Text = "";
                    iContract_ID = 0;
                    iProvider_ID = 0;
                    this.BackColor = Color.PeachPuff;
                    break;
                case 2:
                    ucCS.Enabled = true;
                    lblCode.Text = "";
                    lnkPelatis.Text = "";
                    lnkPelatis.Enabled = true;
                    lnkPortfolio.Text = "";
                    iProvider_ID = 0;
                    iContract_ID = 0;
                    this.BackColor = Color.LightSteelBlue;
                    break;
                case 3:
                    ucCS.Enabled = true;
                    lblCode.Text = "";
                    lnkPelatis.Text = "";
                    lnkPelatis.Enabled = true;
                    lnkPortfolio.Text = "";
                    iProvider_ID = 0;
                    iContract_ID = 0;
                    this.BackColor = Color.Tan;
                    break;
                case 4:
                    ucCS.Enabled = true;
                    lblCode.Text = "";
                    lnkPelatis.Text = "";
                    lnkPelatis.Enabled = true;
                    lnkPortfolio.Text = "";
                    iContract_ID = 0;
                    iProvider_ID = 0;
                    this.BackColor = Color.LightBlue;
                    break;
            }
        }
        protected void ucCS_TextChanged(object sender, EventArgs e)
        {
            if (ucCS.Mode == 1 ) { 
                Global.ContractData stContract = new Global.ContractData();
                stContract = ucCS.SelectedContractData;
                if (ucCS.Contract_ID.Text != "0") {
                    switch (ucCS.ListType)
                    {
                        case 1:
                        case 2:
                            clsContract_Blocks klsContract_Blocks = new clsContract_Blocks();
                            klsContract_Blocks.Contract_ID = stContract.Contract_ID;
                            klsContract_Blocks.Record_ID = 0;
                            klsContract_Blocks.GetRecord_Contract();
                            if (klsContract_Blocks.Record_ID == 0) {
                                lnkPelatis.Text = stContract.ContractTitle;
                                lblCode.Text = stContract.Code;
                                sCode = stContract.Code;
                                lnkPortfolio.Text = stContract.Portfolio;
                                iClient_ID = stContract.Client_ID;
                                iContract_ID = stContract.Contract_ID;
                                iContract_Details_ID = stContract.Contracts_Details_ID;
                                iContract_Packages_ID = stContract.Contracts_Packages_ID;
                                iProvider_ID = stContract.Provider_ID;
                                iProviderType = stContract.ProviderType;
                                cmbServiceProviders.SelectedValue = stContract.Provider_ID;
                                sProviderTitle = stContract.Provider_Title + "";
                                iContractService_ID = stContract.Service_ID;
                                iMIFIDCategory_ID = stContract.MIFIDCategory_ID;
                                //iMIFID_Risk_Index = stContract.MIFID_Risk_Index;
                                iMIFID_2 = stContract.MIFID_2;
                                //iClientType = stContract.Category;

                                iBusinessType_ID = 1;                                                // by default                   BusinessType = 1
                                if (iProviderType == 1) iBusinessType_ID = 1;                        // 1 - CreditSuisse  
                                if (iProviderType == 2) iBusinessType_ID = 2;                        // 2 - HF2S 

                                ShowList();
                                txtAction.Focus();
                            }
                            else
                                MessageBox.Show("Contract Blocked", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            break;
                        case 3:
                            stContract = ucCS.SelectedContractData;
                            lnkPelatis.Text = stContract.ContractTitle;
                            lblCode.Text = stContract.Code;
                            lnkPortfolio.Text = stContract.Portfolio;
                            iClient_ID = stContract.Client_ID;
                            iContract_ID = 0;
                            iContract_Details_ID = 0;
                            iContract_Packages_ID = 0;
                            iContractService_ID = stContract.Service_ID;
                            iProvider_ID = stContract.Provider_ID;
                            iProviderType = stContract.ProviderType;
                            sProviderTitle = stContract.Provider_Title;
                            //iClientType = stContract.Category;
                            break;
                    }
                }
            }
            else {                                         // ucCS.Mode == 2
                sTemp = ucCS.CodesList;
                string[] tokens = sTemp.Split('~');

                fgSelectedContracts.Redraw = false;
                for (i = 0; i <= tokens.Length -2; i++)
                    fgSelectedContracts.AddItem(tokens[i]);
                fgSelectedContracts.Redraw = true;

                ucCS.Contract_ID.Text = "-999";
            }
            fgCommandBuffer.Rows.Count = 1;
        }
        protected void ucPS_TextChanged(object sender, EventArgs e)
        {
            if (ucPS.Mode == 1) {
                Global.ProductData stProduct = new Global.ProductData();
                stProduct = ucPS.SelectedProductData;
                iShare_ID = stProduct.ShareCode_ID;
                iStockExchange_ID = stProduct.StockExchange_ID;
                sTemp = "";
                if (txtAction.Text == "BUY") sTemp = Global.CheckCompatibility(iContract_ID, iMIFID_2, iMIFIDCategory_ID, iXAA, iShare_ID, iStockExchange_ID);

                lnkISIN.Text = stProduct.ISIN;
                lblShareCode.Text = stProduct.Code;
                //lblProduct.Text = stProduct.Product_Title;
                iProduct_ID = stProduct.Product_ID;
                iProductCategory_ID = stProduct.ProductCategory_ID;
                iShare_ID = stProduct.ShareCode_ID;
                lblCurr.Text = stProduct.Currency;
                sProductTitle = stProduct.Product_Title;
                sStockExchange_Code = stProduct.StockExchange_Code;

                if (stProduct.OK_Flag == 1 && stProduct.HFIC_Recom == 1)
                {
                    switch (iProduct_ID)
                    {
                        case 1:                                       // Shares 
                            lstType.Visible = true;
                            lstType.SelectedIndex = 0;


                            lblPrice.Text = Global.GetLabel("price");
                            lblPrice.Visible = true;
                            lblCurr.Visible = true;
                            txtPrice.Visible = true;


                            lblQuantity.Text = Global.GetLabel("pieces");
                            lblQuantity.Visible = true;
                            txtQuantity.Visible = true;


                            lblAmount.Text = Global.GetLabel("investment_amount");
                            if (txtAction.Text == "BUY")
                            {
                                lblAmount.Visible = true;
                                txtAmount.Visible = true;
                            }
                            else
                            {
                                lblAmount.Visible = false;
                                txtAmount.Visible = false;
                            }
                            break;

                        case 2:                                          // Bond (Omologa)
                            lstType.Visible = true;
                            lstType.SelectedIndex = 0;

                            lblPrice.Text = Global.GetLabel("price");
                            lblPrice.Visible = true;
                            lblCurr.Visible = true;
                            txtPrice.Visible = true;

                            lblQuantity.Text = Global.GetLabel("nomical_value");
                            lblQuantity.Visible = true;
                            txtQuantity.Visible = true;

                            lblAmount.Text = Global.GetLabel("investment_amount");
                            if (txtAction.Text == "BUY")
                            {
                                lblAmount.Visible = true;
                                txtAmount.Visible = true;
                            }
                            else
                            {
                                lblAmount.Visible = false;
                                txtAmount.Visible = false;
                            }
                            break;

                        case 4:                                         // ETF (DAK)
                            lstType.Visible = true;
                            lstType.SelectedIndex = 0;

                            lblPrice.Text = Global.GetLabel("price");
                            lblPrice.Visible = true;
                            lblCurr.Visible = true;
                            txtPrice.Visible = true;

                            lblQuantity.Text = Global.GetLabel("pieces");
                            lblQuantity.Visible = true;
                            txtQuantity.Visible = true;


                            lblAmount.Text = Global.GetLabel("investment_amount");
                            if (txtAction.Text == "BUY")
                            {
                                lblAmount.Visible = true;
                                txtAmount.Visible = true;
                            }
                            else
                            {
                                lblAmount.Visible = false;
                                txtAmount.Visible = false;
                            }
                            break;

                        case 6:                                     //   FUND (AK)
                            lstType.Visible = true;
                            lstType.SelectedIndex = 1;

                            lblPrice.Text = Global.GetLabel("price");
                            lblPrice.Visible = false;
                            lblCurr.Visible = false;
                            txtPrice.Visible = false;

                            lblQuantity.Text = Global.GetLabel("shares");
                            if (txtAction.Text == "BUY")
                            {
                                lblQuantity.Visible = false;
                                txtQuantity.Visible = false;
                            }
                            else
                            {
                                lblQuantity.Visible = true;
                                txtQuantity.Visible = true;
                            }

                            lblAmount.Text = Global.GetLabel("investment_amount");
                            lblAmount.Visible = true;
                            txtAmount.Visible = true;
                            break;
                    }
                }
                else
                {
                    sTemp = stProduct.OK_String + "";

                    sMessages = "Δεν είναι κατάλληλο λόγω:";
                    if (stProduct.HFIC_Recom == 0) sMessages = sMessages + "\n - Δεν είναι επιλεγμένο προϊόν";
                    if (sTemp.Substring(0, 1) == "0") sMessages = sMessages + "\n - Low";
                    if (sTemp.Substring(1, 1) == "0") sMessages = sMessages + "\n - Mid1";
                    if (sTemp.Substring(2, 1) == "0") sMessages = sMessages + "\n - Mid2";
                    if (sTemp.Substring(3, 1) == "0") sMessages = sMessages + "\n - High1";
                    if (sTemp.Substring(4, 1) == "0") sMessages = sMessages + "\n - High2";
                    if (sTemp.Substring(5, 1) == "0") sMessages = sMessages + "\n - High3";

                    MessageBox.Show(sMessages, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    /*
                    iShare_ID = 0;
                    iStockExchange_ID = 0;
                    cmbProducts.SelectedValue = 0;
                    ucPS.ShowProductsList = false;
                    ucPS.txtShareTitle.Text = "";
                    ucPS.ShowProductsList = true;
                    lnkISIN.Text = "";
                    lblShareCode.Text = "";                                    
                    iProduct_ID = 0;
                    iProductCategory_ID = 0;
                    lblCurr.Text = "";
                    sProductTitle = "";
                    sStockExchange_Code = "";
                    */
                }
            }
            else {                                         // ucPS.Mode == 2
                sTemp = ucPS.CodesList + "";
                string[] tokens = sTemp.Split('~');

                fgSelectedProducts.Redraw = false;
                for (i = 0; i <= tokens.Length - 2; i++)
                    fgSelectedProducts.AddItem(tokens[i]);
                fgSelectedProducts.Redraw = true;

                ucPS.ShareCode_ID.Text = "-999";

                //Global.ProductData stProduct = new Global.ProductData();
                //stProduct = ucPS.SelectedProductData;
                //iShare_ID = stProduct.ShareCode_ID;
            }
        }
        private void EmptyCommand()
        {
            iClient_ID = 0;
            ucCS.ShowClientsList = false;
            ucCS.txtContractTitle.Text = "";
            ucCS.ShowClientsList = true;
            lnkPortfolio.Text = "";
            iProvider_ID = 0;
            sProviderTitle = "";
            sCode = "";
            sPortfolio = "";
            lblCode.Text = "";
            lnkPelatis.Text = "";
            txtAction.Text = "";
            iShare_ID = 0;
            ucPS.ShowProductsList = false;
            ucPS.txtShareTitle.Text = "";
            ucPS.ShowProductsList = true;
            lnkISIN.Text = "";
            lblShareCode.Text = "";
            //lblProduct.Text = "";
            iProduct_ID = 0;
            sProductTitle = "";
            iProductCategory_ID = 0;
            txtQuantity.Text = "";
            lstType.SelectedIndex = 0;
            txtPrice.Text = "";
            txtPriceUp.Text = "";
            txtPriceDown.Text = "";
            txtAmount.Text = "";
            lblCurr.Text = "";
            cmbConstant.SelectedIndex = 0;
            cmbDiaxiristes.SelectedValue = 0;
            cmbServiceProviders.SelectedValue = 0;
            dConstant.Value = Convert.ToDateTime("1900/01/01");
            panSecurities.BackColor = Color.Transparent;
            panFilters.Visible = true;

            ucCS.txtContractTitle.Focus();
        }
        #endregion
        #region --- fgList functions -----------------------------------------------------------------------
       
        public void DefineList()
        {
            clsOrdersSecurity klsOrder = new clsOrdersSecurity();

            fgList.Redraw = false;
            fgList.Rows.Count = 2;
            fgList.Cols[27].AllowMerging = true;
            rng = fgList.GetCellRange(0, 27, 1, 27);
            rng.Data = "Επίσημη Ενημέρωση";

            switch (iCommandType_ID)
            {
                case 1:
                    fgList.Cols[2].Width = 50;
                    fgList.Cols[3].Visible = true;
                    fgList.Cols[3].Width = 80;
                    fgList.Cols[4].Visible = true;
                    fgList.Cols[4].Width = 180;
                    fgList.Cols[5].Visible = true;
                    fgList.Cols[6].Visible = true;
                    fgList.Cols[7].Visible = true;
                    fgList.Cols[26].Visible = true;

                    k = 0;
                    iOddEvenBlock = 0;              //pseudo even block
                    sInvPropNotesFlag = "";
                    sDPMNotesFlag = "";

                    klsOrder.CommandType_ID = iCommandType_ID;
                    klsOrder.DateFrom = dToday.Value;
                    klsOrder.DateTo = dToday.Value;
                    klsOrder.ServiceProvider_ID = Convert.ToInt32(cmbProviders.SelectedValue);
                    klsOrder.Sent = Convert.ToInt32(cmbSent.SelectedIndex);
                    klsOrder.Actions = Convert.ToInt32(cmbActions.SelectedIndex);
                    klsOrder.SendCheck = Convert.ToInt32(cmbChecked.SelectedIndex);
                    klsOrder.User_ID = Convert.ToInt32(cmbUsers.SelectedValue);
                    klsOrder.User1_ID = Convert.ToInt32(cmbAdvisors.SelectedValue);
                    klsOrder.User4_ID = Convert.ToInt32(cmbDiax.SelectedValue);
                    klsOrder.Division_ID = Convert.ToInt32(cmbDivisions.SelectedValue);
                    klsOrder.Code = sCode;
                    klsOrder.Product_ID = 0;
                    klsOrder.Share_ID = iShare_ID;
                    klsOrder.Currency = "";
                    klsOrder.ShowCancelled = iShowCancelled;
                    klsOrder.GetList();
                    foreach (DataRow dtRow in klsOrder.List.Rows) {
                        bFilter = true;

                        if ((dtRow["BulkCommand"] + "") != "" && Convert.ToDateTime(dtRow["RecieveDate"]) == Convert.ToDateTime("1900/01/01")) bFilter = false;

                        if (rbA.Checked && Convert.ToInt32(dtRow["Aktion"]) == 2) bFilter = false;
                        if (rbP.Checked && Convert.ToInt32(dtRow["Aktion"]) == 1) bFilter = false;

                        if (Convert.ToInt32(cmbServices.SelectedValue) != 0 && Convert.ToInt32(dtRow["Service_ID"]) != Convert.ToInt32(cmbServices.SelectedValue)) bFilter = false;

                        if (bFilter) {                            

                            if (Convert.ToInt32(dtRow["Type"]) == 3 && Convert.ToInt32(dtRow["Parent_ID"]) == 0) {           // if it's scenario first command
                                if (iOddEvenBlock == 1) iOddEvenBlock = 2;                                                   // define odd/even block
                                else iOddEvenBlock = 1;
                                iStyle = iOddEvenBlock;
                            }
                            else if (Convert.ToInt32(dtRow["Parent_ID"]) == 0) iStyle = 0;                                   // it's simple command

                            sBulkCommand = (dtRow["BulkCommand"] + "").Replace("<", "").Replace(">", "");
                            sBulkCommand = (sBulkCommand == "0" ? "" : sBulkCommand);

                            sgTemp2 = 0;
                            if ((dtRow["Currency"] + "") == "EUR") sgTemp2 = Convert.ToSingle(dtRow["RealAmount"]);         // Amount EUR 
                            else  {
                                sgTemp1 = Convert.ToSingle(dtRow["CurrRate"]);                                              // CurrRate
                                if (sgTemp1 != 0) sgTemp2 = Convert.ToSingle(dtRow["RealAmount"]) / sgTemp1;                // Amount EUR           
                            }

                            k = k + 1;
                            fgList.AddItem(dtRow["Type"] + "\t" + k + "\t" + sBulkCommand + "\t" + dtRow["ClientFullName"] + "\t" + dtRow["ContractTitle"] + "\t" +
                                           dtRow["StockCompanyTitle"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" +
                                           (Convert.ToInt32(dtRow["Aktion"]) == 1 ? "BUY" : "SELL") + "\t" + dtRow["Product_Title"] + "/" + dtRow["Product_Category"] + "\t" +
                                           dtRow["Share_Title"] + "\t" + dtRow["Share_Code"] + "\t" + dtRow["Share_ISIN"] + "\t" + 
                                           Global.ShowPrices(Convert.ToInt16(dtRow["PriceType"]), Convert.ToSingle(dtRow["Price"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["Quantity"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["Quantity"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["Amount"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["Amount"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["RealPrice"]) == 0 ? "" : string.Format("{0:#,0.00##}", dtRow["RealPrice"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["RealQuantity"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["RealQuantity"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["RealAmount"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["RealAmount"])) + "\t" + dtRow["Currency"] + "\t" + 
                                           (sConstant[Convert.ToInt16(dtRow["Constant"])] + " " + dtRow["ConstantDate"]).Trim() + "\t" + 
                                           dtRow["StockExchange_MIC"] + "\t" + dtRow["ExecutionStockExchange_MIC"] + "\t" +
                                           ((Convert.ToDateTime(dtRow["RecieveDate"]) != Convert.ToDateTime("31/12/2070")) ? Convert.ToDateTime(dtRow["RecieveDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                           ((Convert.ToDateTime(dtRow["SentDate"]) != Convert.ToDateTime("01/01/1900")) ? Convert.ToDateTime(dtRow["SentDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                           ((Convert.ToDateTime(dtRow["ExecuteDate"]) != Convert.ToDateTime("01/01/1900")) ? Convert.ToDateTime(dtRow["ExecuteDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                           dtRow["RecieveTitle"] + "\t" + dtRow["OfficialInformingDate"] + "\t" + dtRow["Notes"] + "\t" + dtRow["Author_Fullname"] + "\t" + 
                                           dtRow["Advisor_Fullname"] + "\t" + dtRow["Diax_Fullname"] + "\t" + dtRow["ServiceTitle"] + "\t" + dtRow["InvestPolicy_Title"] + "\t" + 
                                           dtRow["InvestProfile_Title"] + "\t" + dtRow["II_ID"] + "\t" + sRisks[Convert.ToInt32(dtRow["Risk"])] + "\t" +
                                           sMiFID[Convert.ToInt32(dtRow["MiFIDCategory_ID"])] + "\t" + dtRow["StockExchange_Title"] + "\t" + dtRow["Recomend"] + "\t" + 
                                           dtRow["FeesPercent"] + "\t" + dtRow["FeesAmount"] + "\t" + dtRow["FeesDiscountPercent"] + "\t" + dtRow["FeesDiscountAmount"] + "\t" + 
                                           dtRow["FinishFeesPercent"] + "\t" + dtRow["FinishFeesAmount"] + "\t" + dtRow["ProviderFees"] + "\t" + dtRow["ID"] + "\t" + 
                                           dtRow["Client_ID"] + "\t" + dtRow["ServiceProvider_ID"] + "\t" + dtRow["Status"] + "\t" + 
                                           ((Convert.ToInt32(dtRow["Parent_ID"]) == 0) ? dtRow["ID"] : dtRow["Parent_ID"]) + "\t" + iStyle + "\t" + dtRow["Share_ID"] + "\t" + 
                                           dtRow["Contract_ID"] + "\t" + dtRow["Contract_Details_ID"] + "\t" + dtRow["Contract_Packages_ID"] + "\t" + "" + "\t" +
                                           dtRow["BusinessType_ID"] + "\t" + dtRow["Product_ID"] + "\t" + dtRow["ProductCategory_ID"] + "\t" +
                                           dtRow["SendCheck"] + "\t" + dtRow["Executor_Title"] + "\t" + dtRow["ValueDate"] + "\t" + dtRow["AccruedInterest"] + "\t" +
                                           dtRow["FeesMisc"] + "\t" + dtRow["Depository_Title"] + "\t" + dtRow["QuantityMin"] + "\t" + dtRow["QuantityStep"] + "\t" +
                                           dtRow["Tipos"] + "\t" + dtRow["CurrRate"] + "\t" + sgTemp2 + "\t" + dtRow["FIX_A"]);
                        }
                    }
                    fgList.Sort(SortFlags.Descending, 1);     // 1- Num
                    break;
                case 2:
                    fgList.Cols[2].Width = 50;
                    fgList.Cols[3].Visible = true;
                    fgList.Cols[3].Width = 160;
                    fgList.Cols[4].Visible = false;
                    fgList.Cols[4].Width = 100;
                    fgList.Cols[5].Visible = true;
                    fgList.Cols[6].Visible = true;
                    fgList.Cols[7].Visible = true;
                    fgList.Cols[26].Visible = false;

                    k = 0;
                    iOddEvenBlock = 0;             // pseudo even block
                    sInvPropNotesFlag = "";
                    sDPMNotesFlag = "";

                    klsOrder.CommandType_ID = iCommandType_ID;
                    klsOrder.DateFrom = dToday.Value;
                    klsOrder.DateTo = dToday.Value;
                    klsOrder.ServiceProvider_ID = Convert.ToInt32(cmbProviders.SelectedValue);
                    klsOrder.Sent = Convert.ToInt32(cmbSent.SelectedIndex);
                    klsOrder.Actions = Convert.ToInt32(cmbActions.SelectedIndex);
                    klsOrder.SendCheck = Convert.ToInt32(cmbChecked.SelectedIndex);
                    klsOrder.User_ID = Convert.ToInt32(cmbUsers.SelectedValue);
                    klsOrder.User1_ID = Convert.ToInt32(cmbAdvisors.SelectedValue);
                    klsOrder.User4_ID = Convert.ToInt32(cmbDiax.SelectedValue);
                    klsOrder.Division_ID = Convert.ToInt32(cmbDivisions.SelectedValue);                 
                    klsOrder.Code = sCode;
                    klsOrder.ShowCancelled = iShowCancelled;
                    klsOrder.GetExecutionList();
                    foreach (DataRow dtRow in klsOrder.List.Rows)
                    {
                        bFilter = false;
                        if (iActions == 0) bFilter = true;
                        else {
                            if (iActions == 1) {
                                if (Convert.ToDateTime(dtRow["ExecuteDate"]).Date != Convert.ToDateTime("1900/01/01").Date) bFilter = true;
                            }
                            else {
                                if (iActions == 2) if (Convert.ToDateTime(dtRow["ExecuteDate"]).Date == Convert.ToDateTime("1900/01/01").Date) bFilter = true;
                            }
                        }

                        if ((iCheck == 1 && Convert.ToInt32(dtRow["SendCheck"]) == 0) || (iCheck == 2 && Convert.ToInt32(dtRow["SendCheck"]) == 1)) bFilter = false;

                        if (iProvider_ID != 0 && Convert.ToInt32(dtRow["StockCompany_ID"]) != iProvider_ID) bFilter = false;

                        if (bFilter) {
                            if (Convert.ToInt32(dtRow["Type"]) == 3 && Convert.ToInt32(dtRow["Parent_ID"]) == 0) {     // if it's scenario first command
                                if (iOddEvenBlock == 1) iOddEvenBlock = 2;                                             // define odd/even block
                                else iOddEvenBlock = 1;

                                iStyle = iOddEvenBlock;
                            }
                            else if (Convert.ToInt32(dtRow["Parent_ID"]) == 0) iStyle = 0;                             // it's simple command

                            sInvestPolicy = "";
                            if (Convert.ToInt32(dtRow["AdvisoryInvestmentPolicy_ID"]) != 0) sInvestPolicy = dtRow["AdvisoryInvestmentPolicy_Title"] + "";

                            if (Convert.ToInt32(dtRow["DiscretInvestmentPolicy_ID"]) != 0) sInvestPolicy = dtRow["DiscretInvestmentPolicy_Title"] + "";

                            if (Convert.ToInt32(dtRow["DealAdvisoryInvestmentPolicy_ID"]) != 0) sInvestPolicy = dtRow["DealAdvisoryInvestmentPolicy_Title"] + "";

                            sInvestProfile = "";
                            sBulkCommand = (dtRow["BulkCommand"] + "").Replace("<", "").Replace(">", "");
                            sBulkCommand = (sBulkCommand == "0" ? "" : sBulkCommand);

                            if ((dtRow["Currency"] + "") == "EUR") {
                                sgTemp2 = Convert.ToSingle(dtRow["RealAmount"]);                                            // Amount EUR 
                            }
                            else {
                                sgTemp1 = Convert.ToSingle(dtRow["CurrRate"]);
                                if (sgTemp1 != 0) sgTemp2 = Convert.ToSingle(dtRow["RealAmount"]) / sgTemp1;                // Amount EUR           
                            }

                            k = k + 1;
                            fgList.AddItem(dtRow["Type"] + "\t" + k + "\t" + sBulkCommand + "\t" + dtRow["ClientFullName"] + "\t" + "" + "\t" +
                                           dtRow["StockCompanyTitle"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" +
                                           (Convert.ToInt32(dtRow["Aktion"]) == 1 ? "BUY" : "SELL") + "\t" + dtRow["Product_Title"] + "/" + dtRow["Product_Category"] + "\t" +
                                           dtRow["Share_Title"] + "\t" + dtRow["Share_Code"] + "\t" + dtRow["Share_ISIN"] + "\t" + 
                                           Global.ShowPrices(Convert.ToInt16(dtRow["PriceType"]), Convert.ToSingle(dtRow["Price"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["Quantity"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["Quantity"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["Amount"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["Amount"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["RealPrice"]) == 0 ? "" : string.Format("{0:#,0.00##}", dtRow["RealPrice"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["RealQuantity"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["RealQuantity"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["RealAmount"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["RealAmount"])) + "\t" + dtRow["Currency"] + "\t" + 
                                           sConstant[Convert.ToInt16(dtRow["Constant"])].Trim() + " " + dtRow["ConstantDate"] + "\t" + 
                                           dtRow["StockExchange_MIC"] + "\t" + dtRow["ExecutionStockExchange_MIC"] + "\t" +
                                           ((Convert.ToDateTime(dtRow["RecieveDate"]) != Convert.ToDateTime("31/12/2070")) ? Convert.ToDateTime(dtRow["RecieveDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                           ((Convert.ToDateTime(dtRow["SentDate"]) != Convert.ToDateTime("01/01/1900")) ? Convert.ToDateTime(dtRow["SentDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                           ((Convert.ToDateTime(dtRow["ExecuteDate"]) != Convert.ToDateTime("01/01/1900")) ? Convert.ToDateTime(dtRow["ExecuteDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                           dtRow["RecieveTitle"] + "\t" + dtRow["OfficialInformingDate"] + "\t" + dtRow["Notes"] + "\t" + dtRow["Author_Fullname"] + "\t" + 
                                           dtRow["Advisor_Fullname"] + "\t" + dtRow["Diax_Fullname"] + "\t" + dtRow["ServiceTitle"] + "\t" + sInvestPolicy + "\t" + sInvestProfile + "\t" +
                                           dtRow["II_ID"] + "\t" + sRisks[Convert.ToInt32(dtRow["Risk"])] + "\t" + sMiFID[Convert.ToInt32(dtRow["MiFIDCategory_ID"])] + "\t" +
                                           dtRow["StockExchange_Title"] + "\t" + dtRow["Recomend"] + "\t" + dtRow["FeesPercent"] + "\t" + dtRow["FeesAmount"] + "\t" + 
                                           dtRow["FeesDiscountPercent"] + "\t" + dtRow["FeesDiscountAmount"] + "\t" + dtRow["FinishFeesPercent"] + "\t" + dtRow["FinishFeesAmount"] + "\t" +
                                           dtRow["ProviderFees"] + "\t" + dtRow["ID"] + "\t" + dtRow["Client_ID"] + "\t" + dtRow["StockCompany_ID"] + "\t" + dtRow["Status"] + "\t" + 
                                           ((Convert.ToInt32(dtRow["Parent_ID"]) == 0) ? dtRow["ID"] : dtRow["Parent_ID"]) + "\t" + iStyle + "\t" + dtRow["Share_ID"] + "\t" + 
                                           dtRow["Contract_ID"] + "\t" + dtRow["Contract_Details_ID"] + "\t" + dtRow["Contract_Packages_ID"] + "\t" +
                                           "" + "\t" + dtRow["BusinessType_ID"] + "\t" + dtRow["Product_ID"] + "\t" + dtRow["ProductCategory_ID"] + "\t" +
                                           dtRow["SendCheck"] + "\t" + dtRow["Executor_Title"] + "\t" + dtRow["ValueDate"] + "\t" + dtRow["AccruedInterest"] + "\t" +
                                           dtRow["FeesMisc"] + "\t" + dtRow["Depository_Title"] + "\t" + dtRow["QuantityMin"] + "\t" + dtRow["QuantityStep"] + "\t" +
                                           dtRow["Tipos"] + "\t" + dtRow["CurrRate"] + "\t" + sgTemp2 + "\t" + dtRow["FIX_A"]);

                        }
                    }
                    fgList.Sort(SortFlags.Descending, 1);     // 1- Num
                    break;
                case 3:
                    fgList.Cols[2].Width = 50;
                    fgList.Cols[3].Visible = true;
                    fgList.Cols[3].Width = 160;
                    fgList.Cols[4].Visible = false;
                    fgList.Cols[4].Width = 100;
                    fgList.Cols[5].Visible = true;
                    fgList.Cols[6].Visible = false;
                    fgList.Cols[7].Visible = false;
                    fgList.Cols[26].Visible = false;

                    k = 0;
                    klsOrder.CommandType_ID = iCommandType_ID;                               //  3 - Bulk Orders
                    klsOrder.DateFrom = dToday.Value;
                    klsOrder.DateTo = dToday.Value;
                    klsOrder.GetBulkList();
                    foreach (DataRow dtRow in klsOrder.List.Rows)
                    {
                        sBulkCommand = (dtRow["BulkCommand"]+"").Replace("<", "").Replace(">", "");
                        sBulkCommand = (sBulkCommand == "0" ? "": sBulkCommand);

                        if (iProvider_ID == 0 || Convert.ToInt32(dtRow["StockCompany_ID"]) == iProvider_ID) {
                            k = k + 1;

                            if ((dtRow["Currency"] + "") == "EUR") {
                                sgTemp2 = Convert.ToSingle(dtRow["RealAmount"]);                                            // Amount EUR 
                            }
                            else {
                                sgTemp1 = Convert.ToSingle(dtRow["CurrRate"]);
                                if (sgTemp1 != 0) sgTemp2 = Convert.ToSingle(dtRow["RealAmount"]) / sgTemp1;                // Amount EUR           
                            }
                            fgList.AddItem("0" + "\t" + k + "\t" + sBulkCommand + "\t" + dtRow["Client_Title"] + "\t" + dtRow["ContractTitle"] + "\t" + 
                                         dtRow["StockCompanyTitle"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" +
                                         ((Convert.ToInt32(dtRow["Aktion"]) == 1) ? "BUY" : "SELL") + "\t" + dtRow["Product_Title"] + "/" + dtRow["Product_Category"] + "\t" +
                                         dtRow["Share_Title"] + "\t" + dtRow["Share_Code"] + "\t" + dtRow["Share_ISIN"] + "\t" +
                                         Global.ShowPrices(Convert.ToInt16(dtRow["PriceType"]), Convert.ToSingle(dtRow["Price"])) + "\t" +
                                         (Convert.ToDecimal(dtRow["Quantity"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["Quantity"])) + "\t" +
                                         (Convert.ToDecimal(dtRow["Amount"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["Amount"])) + "\t" +
                                         (Convert.ToDecimal(dtRow["RealPrice"]) == 0 ? "" : string.Format("{0:#,0.00##}", dtRow["RealPrice"])) + "\t" +
                                         (Convert.ToDecimal(dtRow["RealQuantity"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["RealQuantity"])) + "\t" +
                                         (Convert.ToDecimal(dtRow["RealAmount"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["RealAmount"])) + "\t" + dtRow["Currency"] + "\t" + 
                                         sConstant[Convert.ToInt16(dtRow["Constant"])].Trim() + " " + dtRow["ConstantDate"] + "\t" + 
                                         dtRow["StockExchange_MIC"] + "\t" + dtRow["ExecutionStockExchange_MIC"] + "\t" +
                                         ((Convert.ToDateTime(dtRow["RecieveDate"]) != Convert.ToDateTime("31/12/2070")) ? Convert.ToDateTime(dtRow["RecieveDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                         ((Convert.ToDateTime(dtRow["SentDate"]) != Convert.ToDateTime("01/01/1900")) ? Convert.ToDateTime(dtRow["SentDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                         ((Convert.ToDateTime(dtRow["ExecuteDate"]) != Convert.ToDateTime("01/01/1900")) ? Convert.ToDateTime(dtRow["ExecuteDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                         "" + "\t" + "" + "\t" +dtRow["Notes"] + "\t" + dtRow["Author_Fullname"] + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + 
                                         "0" + "\t" +"" + "\t" + "" + "\t" + dtRow["StockExchange_Title"] + "\t" + dtRow["Recomend"] + "\t" +
                                         "0" + "\t" + "0" + "\t" + "0" + "\t" +"0" + "\t" +"0" + "\t" + "0" + "\t" + "0" + "\t" + dtRow["ID"] + "\t" + 
                                         dtRow["Client_ID"] + "\t" + dtRow["StockCompany_ID"] + "\t" + dtRow["Status"] + "\t" + dtRow["ID"] + "\t" +
                                         "0" + "\t" + dtRow["Share_ID"] + "\t" + dtRow["Contract_ID"] + "\t" + dtRow["Contract_Details_ID"] + "\t" + dtRow["Contract_Packages_ID"] + "\t" +
                                         "" + "\t" + dtRow["BusinessType_ID"] + "\t" + dtRow["Product_ID"] + "\t" + dtRow["ProductCategory_ID"] + "\t" +
                                         dtRow["SendCheck"] + "\t" + dtRow["Executor_Title"] + "\t" + dtRow["ValueDate"] + "\t" + "0" + "\t" + "0" + "\t" + "" + "\t" +
                                         "0" + "\t" + "0" + "\t" + "0" + "\t" + dtRow["CurrRate"] + "\t" + sgTemp2 + "\t" + dtRow["FIX_A"]);
                        }
                    }
                    fgList.Sort(SortFlags.Descending, 1);     // 1- Num
                    break;
                case 4:
                    fgList.Cols[2].Width = 90;
                    fgList.Cols[3].Visible = true;
                    fgList.Cols[3].Width = 130;
                    fgList.Cols[4].Visible = false;
                    fgList.Cols[4].Width = 130;
                    fgList.Cols[5].Visible = true;
                    fgList.Cols[6].Visible = false;
                    fgList.Cols[7].Visible = false;
                    fgList.Cols[27].AllowMerging = true;
                    rng = fgList.GetCellRange(0, 27, 1, 27);
                    rng.Data = "Allocation";

                    k = 0;
                    klsOrder.CommandType_ID = iCommandType_ID;                               //  4 - DPM Orders
                    klsOrder.DateFrom = dToday.Value;
                    klsOrder.DateTo = dToday.Value;
                    klsOrder.ServiceProvider_ID = Convert.ToInt32(cmbProviders.SelectedValue);
                    klsOrder.User_ID = 0; 
                    klsOrder.Sent = Convert.ToInt32(cmbSent.SelectedIndex);
                    klsOrder.Actions = Convert.ToInt32(cmbActions.SelectedIndex);
                    klsOrder.GetDPMList();
                    foreach (DataRow dtRow in klsOrder.List.Rows)
                    {

                        bFilter = true;

                        if ((iCheck == 1 && Convert.ToInt32(dtRow["SendCheck"]) == 0) || (iCheck == 2 && Convert.ToInt32(dtRow["SendCheck"]) == 1)) bFilter = false;

                        //if (iProvider_ID != 0 && Convert.ToInt32(dtRow["StockCompany_ID"]) != iProvider_ID) bFilter = false;

                        if (Convert.ToInt32(dtRow["Company_ID"]) != Global.User_ID && Global.Sender != 1) bFilter = false;  

                        if ((dtRow["BulkCommand"] + "") != "" && Convert.ToDateTime(dtRow["RecieveDate"]) == Convert.ToDateTime("1900/01/01")) bFilter = false;

                        if ((dtRow["Currency"] + "") == "EUR") {
                            sgTemp2 = Convert.ToSingle(dtRow["RealAmount"]);                                            // Amount EUR 
                        }
                        else {
                            sgTemp1 = Convert.ToSingle(dtRow["CurrRate"]);
                            if (sgTemp1 != 0) sgTemp2 = Convert.ToSingle(dtRow["RealAmount"]) / sgTemp1;                // Amount EUR           
                        }

                        if (bFilter) {

                            k = k + 1;
                            sBulkCommand = (dtRow["BulkCommand"]+"").Replace("<", "").Replace(">", "");
                            sBulkCommand = (sBulkCommand == "0" ? "": sBulkCommand);
                            fgList.AddItem("0" + "\t" + k + "\t" + sBulkCommand + "\t" + dtRow["Diax_Fullname"] + "\t" + dtRow["ContractTitle"] + "\t" + 
                                           dtRow["StockCompanyTitle"] + "\t" + dtRow["Code"] + "\t" +  dtRow["Portfolio"] + "\t" + 
                                           ((Convert.ToInt32(dtRow["Aktion"]) == 1) ? "BUY" : "SELL") + "\t" + dtRow["Product_Title"] + "/" + dtRow["Product_Category"] + "\t" +
                                           dtRow["Share_Title"] + "\t" + dtRow["Share_Code"] + "\t" + dtRow["Share_ISIN"] + "\t" + 
                                           Global.ShowPrices(Convert.ToInt16(dtRow["Type"]), Convert.ToSingle(dtRow["Price"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["Quantity"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["Quantity"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["Amount"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["Amount"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["RealPrice"]) == 0 ? "" : string.Format("{0:#,0.00##}", dtRow["RealPrice"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["RealQuantity"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["RealQuantity"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["RealAmount"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["RealAmount"])) + "\t" + dtRow["Currency"] + "\t" + 
                                           sConstant[Convert.ToInt16(dtRow["Constant"])].Trim() + " " + dtRow["ConstantDate"] + "\t" + 
                                           dtRow["StockExchange_MIC"] + "\t" + dtRow["ExecutionStockExchange_MIC"] + "\t" +
                                           ((Convert.ToDateTime(dtRow["RecieveDate"]) != Convert.ToDateTime("31/12/2070")) ? Convert.ToDateTime(dtRow["RecieveDate"]).ToString("yyyy/MM/dd") : "") + "\t" + 
                                           ((Convert.ToDateTime(dtRow["SentDate"]) != Convert.ToDateTime("01/01/1900")) ? Convert.ToDateTime(dtRow["SentDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                           ((Convert.ToDateTime(dtRow["ExecuteDate"]) != Convert.ToDateTime("01/01/1900")) ? Convert.ToDateTime(dtRow["ExecuteDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                           dtRow["RecieveTitle"] + "\t" + dtRow["AllocationPercent"] + "\t" + dtRow["Notes"] + "\t" + dtRow["Author_Fullname"] + "\t" + "" + "\t" + 
                                           dtRow["Diax_Fullname"] + "\t" + dtRow["ServiceTitle"] + "\t" + "" + "\t" + "" + "\t" + dtRow["II_ID"] + "\t" + sRisks[Convert.ToInt32(dtRow["Risk"])] + "\t" +
                                           sMiFID[Convert.ToInt32(dtRow["MiFIDCategory_ID"])] + "\t" + dtRow["StockExchange_Title"] + "\t" + dtRow["Recomend"] + "\t" +
                                           "0" + "\t" + "0" + "\t" + "0" + "\t" + "0" + "\t" + "0" + "\t" + "0" + "\t" + "0" + "\t" +
                                           dtRow["ID"] + "\t" + dtRow["Client_ID"] + "\t" + dtRow["StockCompany_ID"] + "\t" + dtRow["Status"] + "\t" + dtRow["ID"] + "\t" +
                                           "0" + "\t" + dtRow["Share_ID"] + "\t" + dtRow["Contract_ID"] + "\t" + dtRow["Contract_Details_ID"] + "\t" + dtRow["Contract_Packages_ID"] + "\t" +
                                           "" + "\t" + dtRow["BusinessType_ID"] + "\t" + dtRow["Product_ID"] + "\t" + dtRow["ProductCategory_ID"] + "\t" +
                                           dtRow["SendCheck"] + "\t" + dtRow["Executor_Title"] + "\t" + dtRow["ValueDate"] + "\t" + "0" + "\t" + "0" + "\t" + 
                                           "" + "\t" + "0" + "\t" + "0" + "\t" + "0" + "\t" + dtRow["CurrRate"] + "\t" + sgTemp2 + "\t" + dtRow["FIX_A"]);
                        }
                    }
                    fgList.Sort(SortFlags.Descending, 1);     // 1- Num
                    break;
            }

            fgList.Redraw = true;
            if (fgList.Rows.Count > 2) fgList.Row = 2;
            fgList.Focus();

            DefinePreOrdersList();
        }
        private void fgList_RowColChange(object sender, EventArgs e)
        {
            if (fgList.Row > 1)
            {
                iClientData_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
                if (Convert.ToInt32(tcBusinessTypes.SelectedIndex) == 3)                                        // 3 - Execution Tab
                {
                    if (Convert.ToInt32(fgList[fgList.Row, "FIX_A"]) == -1) mnuContext.Items[6].Visible = false;
                    else mnuContext.Items[6].Visible = true;
                }
            }
        }
        private void fgList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                fgList.ContextMenuStrip = mnuContext;
                fgList.Row = fgList.MouseRow;
            }
        }
        private void fgList_Click(object sender, EventArgs e)
        {
            if (iMode == 2)                                                            //  2 - Search Mode
                if (fgList.Col == 0)
                    if ((fgList[fgList.Row, "Check_FileName"] + "") != "") {
                           sTemp = fgList[fgList.Row, "ContractTitle"]+"";
                           Global.DMS_ShowFile("Customers/" + sTemp.Replace(".", "_") + "/Informing", fgList[fgList.Row, "Check_FileName"] + "");     //is DMS file, so show it into Web mode
                    }
        }
        private void fgList_DoubleClick(object sender, EventArgs e)
        {
            iRow = fgList.Row;
            if (iRow > 0)
            {
                switch (iCommandType_ID)
                {
                    case 1:
                        frmOrderSecurity locOrderSecurity = new frmOrderSecurity();
                        locOrderSecurity.Rec_ID = Convert.ToInt32(fgList[iRow, "ID"]);                // Rec_ID != 0     EDIT mode
                        locOrderSecurity.BusinessType = iBusinessType_ID;
                        locOrderSecurity.RightsLevel = iRightsLevel;
                        locOrderSecurity.Editable = 1;
                        locOrderSecurity.ShowDialog();
                        if (locOrderSecurity.LastAktion == 1)
                        {                                     // Aktion=1        was saved (added)
                            clsOrdersSecurity klsOrder = new clsOrdersSecurity();
                            klsOrder.Record_ID = Convert.ToInt32(fgList[iRow, "ID"]);
                            klsOrder.CommandType_ID = iCommandType_ID;
                            klsOrder.GetRecord();
                            fgList[iRow, "ContractTitle"] = klsOrder.ContractTitle;
                            fgList[iRow, "Provider_Title"] = klsOrder.ServiceProvider_Title;
                            fgList[iRow, "Code"] = klsOrder.Code;
                            fgList[iRow, "Portfolio"] = klsOrder.ProfitCenter;
                            fgList[iRow, "Aktion"] = (klsOrder.Aktion == 1 ? "BUY" : "SELL");
                            fgList[iRow, "Product_Title"] = klsOrder.Product_Title;
                            fgList[iRow, 10] = klsOrder.Security_Title;
                            fgList[iRow, 11] = klsOrder.Security_Code;
                            fgList[iRow, 12] = klsOrder.Security_ISIN;
                            fgList[iRow, 13] = Global.ShowPrices(klsOrder.PriceType, Convert.ToSingle(klsOrder.Price));
                            fgList[iRow, 14] = klsOrder.Quantity.ToString("0.00");
                            fgList[iRow, 15] = klsOrder.Amount.ToString("0.00");
                            fgList[iRow, 16] = (locOrderSecurity.txtRealPrice.Text != "0" ? locOrderSecurity.txtRealPrice.Text : "");
                            fgList[iRow, 17] = (locOrderSecurity.txtRealQuantity.Text != "0" ? locOrderSecurity.txtRealQuantity.Text : "");
                            fgList[iRow, 18] = (locOrderSecurity.txtRealAmount.Text != "0" ? locOrderSecurity.txtRealAmount.Text : "");
                            fgList[iRow, 19] = klsOrder.Curr;
                            fgList[iRow, 20] = (sConstant[klsOrder.Constant] + " " + klsOrder.ConstantDate).Trim();
                            fgList[iRow, "SE_Code"] = klsOrder.StockExchange_Title;
                            fgList[iRow, "RecieveDate"] = (klsOrder.RecieveDate.Date == Convert.ToDateTime("01/01/1900").Date ? "" : klsOrder.RecieveDate.ToString("yyyy/MM/dd"));
                            fgList[iRow, "SentDate"] = (klsOrder.SentDate.Date == Convert.ToDateTime("01/01/1900").Date ? "" : klsOrder.SentDate.ToString("yyyy/MM/dd"));  
                            fgList[iRow, "ExecuteDate"] = (klsOrder.ExecuteDate.Date == Convert.ToDateTime("01/01/1900").Date ? "" : klsOrder.ExecuteDate.ToString("yyyy/MM/dd"));

                            fgList[iRow, "RecieveTitle"] = klsOrder.RecieveTitle;
                            fgList[iRow, "OfficialInformingDate"] = klsOrder.OfficialInformingDate;
                            fgList[iRow, "Notes"] = klsOrder.Notes;
            
                            fgList[iRow, "Author_Name"] = klsOrder.AuthorName;                           // 28
                            fgList[iRow, "Advisor_Name"] = klsOrder.AdvisorName;
                            fgList[iRow, "SE_Title"] = klsOrder.StockExchange_Title;
                            fgList[iRow, "FeesPercent"] = klsOrder.FeesPercent;
                            fgList[iRow, "FeesAmount"] = klsOrder.FeesAmount;
                            fgList[iRow, "FeesDiscountPercent"] = klsOrder.FeesDiscountPercent;
                            fgList[iRow, "FeesDiscountAmount"] = klsOrder.FeesDiscountAmount;                                                      
                            fgList[iRow, "FinishFeesPercent"] = klsOrder.FinishFeesPercent;
                            fgList[iRow, "FinishFeesAmount"] = klsOrder.FinishFeesAmount;         
                            fgList[iRow, "ProviderFees"] = klsOrder.ProviderFees;                        // 45

                            fgList[iRow, "Client_ID"] = klsOrder.Client_ID;
                            fgList[iRow, "Provider_ID"] = klsOrder.ServiceProvider_ID;
                            fgList[iRow, "Status"] = klsOrder.Status;
                            fgList[iRow, "Share_ID"] = klsOrder.Share_ID;
                            fgList[iRow, "Contract_ID"] = klsOrder.Contract_ID;
                            fgList[iRow, "SendCheck"] = klsOrder.SendCheck;
                            fgList.Redraw = true;

                            //-------  read Command Data --------------------
                            DefinePreOrdersList();
                        }
                        break;
                    case 2:
                        frmOrderExecution locOrderExecution = new frmOrderExecution();
                        locOrderExecution.Rec_ID = Convert.ToInt32(fgList[iRow, "ID"]);
                        locOrderExecution.CommandType_ID = iCommandType_ID;                                 // 2 - Execution Order
                        locOrderExecution.RightsLevel = iRightsLevel;
                        locOrderExecution.Editable = 1;
                        locOrderExecution.ShowDialog();
                        if (locOrderExecution.LastAktion == 1) {                                            // Aktion=1        was saved (added)
                            clsOrdersSecurity klsOrder = new clsOrdersSecurity();
                            klsOrder.Record_ID = Convert.ToInt32(fgList[iRow, "ID"]);
                            klsOrder.CommandType_ID = iCommandType_ID;
                            klsOrder.GetRecord();
                            fgList[iRow, "ContractTitle"] = klsOrder.ContractTitle;
                            fgList[iRow, "Provider_Title"] = klsOrder.ServiceProvider_Title;
                            fgList[iRow, "Code"] = klsOrder.Code;
                            fgList[iRow, "Portfolio"] = klsOrder.ProfitCenter;
                            fgList[iRow, "Aktion"] = (klsOrder.Aktion == 1 ? "BUY" : "SELL");
                            fgList[iRow, "Product_Title"] = klsOrder.Product_Title;
                            fgList[iRow, 10] = klsOrder.Security_Title;
                            fgList[iRow, 11] = klsOrder.Security_Code;
                            fgList[iRow, 12] = klsOrder.Security_ISIN;
                            fgList[iRow, 13] = Global.ShowPrices(klsOrder.PriceType, Convert.ToSingle(klsOrder.Price));
                            fgList[iRow, 14] = klsOrder.Quantity.ToString("0.00");
                            fgList[iRow, 15] = klsOrder.Amount.ToString("0.00");
                            fgList[iRow, 16] = (locOrderExecution.lblSumPrice.Text != "0" ? locOrderExecution.lblSumPrice.Text : "");
                            fgList[iRow, 17] = (locOrderExecution.lblSumQuantity.Text != "0" ? locOrderExecution.lblSumQuantity.Text : "");
                            fgList[iRow, 18] = (locOrderExecution.lblSumAmount.Text != "0" ? locOrderExecution.lblSumAmount.Text : "");
                            fgList[iRow, 19] = klsOrder.Curr;
                            fgList[iRow, 20] = (sConstant[klsOrder.Constant] + " " + klsOrder.ConstantDate).Trim();
                            fgList[iRow, "SE_Code"] = klsOrder.StockExchange_Title;
                            fgList[iRow, "ExecutionSE_MIC"] = klsOrder.ExecutionStockExchange_MIC;
                            fgList[iRow, "RecieveDate"] = (klsOrder.RecieveDate.Date == Convert.ToDateTime("01/01/1900").Date ? "" : klsOrder.RecieveDate.ToString("yyyy/MM/dd"));
                            fgList[iRow, "SentDate"] = (klsOrder.SentDate.Date == Convert.ToDateTime("01/01/1900").Date ? "" : klsOrder.SentDate.ToString("yyyy/MM/dd"));
                            fgList[iRow, "ExecuteDate"] = (klsOrder.ExecuteDate.Date == Convert.ToDateTime("01/01/1900").Date ? "" : klsOrder.ExecuteDate.ToString("yyyy/MM/dd"));

                            fgList[iRow, "RecieveTitle"] = klsOrder.RecieveTitle;
                            fgList[iRow, "OfficialInformingDate"] = klsOrder.OfficialInformingDate;
                            fgList[iRow, "Notes"] = klsOrder.Notes;

                            fgList[iRow, "Author_Name"] = klsOrder.AuthorName;                           // 28
                            fgList[iRow, "Advisor_Name"] = klsOrder.AdvisorName;
                            fgList[iRow, "SE_Title"] = klsOrder.StockExchange_Title;
                            fgList[iRow, "FeesPercent"] = klsOrder.FeesPercent;
                            fgList[iRow, "FeesAmount"] = klsOrder.FeesAmount;
                            fgList[iRow, "FeesDiscountPercent"] = klsOrder.FeesDiscountPercent;
                            fgList[iRow, "FeesDiscountAmount"] = klsOrder.FeesDiscountAmount;
                            fgList[iRow, "FinishFeesPercent"] = klsOrder.FinishFeesPercent;
                            fgList[iRow, "FinishFeesAmount"] = klsOrder.FinishFeesAmount;
                            fgList[iRow, "ProviderFees"] = klsOrder.ProviderFees;                        // 45

                            fgList[iRow, "Client_ID"] = klsOrder.Client_ID;
                            fgList[iRow, "Provider_ID"] = klsOrder.ServiceProvider_ID;
                            fgList[iRow, "Status"] = klsOrder.Status;
                            fgList[iRow, "Share_ID"] = klsOrder.Share_ID;
                            fgList[iRow, "Contract_ID"] = klsOrder.Contract_ID;
                            fgList[iRow, "SendCheck"] = klsOrder.SendCheck;
                            fgList.Redraw = true;

                            //-------  read Command Data --------------------
                            DefinePreOrdersList();
                        }
                        break;
                    case 3:
                        frmOrderBulk locOrderBulk = new frmOrderBulk();
                        locOrderBulk.Rec_ID = Convert.ToInt32(fgList[iRow, "ID"]);
                        locOrderBulk.CommandType_ID = iCommandType_ID;                      // 3 - Bulk Order
                        locOrderBulk.RightsLevel = iRightsLevel;
                        locOrderBulk.Editable = 1;
                        locOrderBulk.ShowDialog();
                        if (locOrderBulk.LastAktion == 1)
                        {                                            // Aktion=1        was saved (added)
                            clsOrdersSecurity klsOrder = new clsOrdersSecurity();
                            klsOrder.Record_ID = Convert.ToInt32(fgList[iRow, "ID"]);
                            klsOrder.CommandType_ID = iCommandType_ID;
                            klsOrder.GetRecord();
                            fgList[iRow, "ContractTitle"] = klsOrder.ContractTitle;
                            fgList[iRow, "Provider_Title"] = klsOrder.ServiceProvider_Title;
                            fgList[iRow, "Code"] = klsOrder.Code;
                            fgList[iRow, "Portfolio"] = klsOrder.ProfitCenter;
                            fgList[iRow, "Aktion"] = (klsOrder.Aktion == 1 ? "BUY" : "SELL");
                            fgList[iRow, "Product_Title"] = klsOrder.Product_Title;
                            fgList[iRow, 10] = klsOrder.Security_Title;
                            fgList[iRow, 11] = klsOrder.Security_Code;
                            fgList[iRow, 12] = klsOrder.Security_ISIN;
                            fgList[iRow, 13] = Global.ShowPrices(klsOrder.PriceType, Convert.ToSingle(klsOrder.Price));
                            fgList[iRow, 14] = klsOrder.Quantity.ToString("0.00");
                            fgList[iRow, 15] = klsOrder.Amount.ToString("0.00");
                            fgList[iRow, 16] = (locOrderBulk.lblSumPrice.Text != "0" ? locOrderBulk.lblSumPrice.Text : "");
                            fgList[iRow, 17] = (locOrderBulk.lblSumQuantity.Text != "0" ? locOrderBulk.lblSumQuantity.Text : "");
                            fgList[iRow, 18] = (locOrderBulk.lblSumAmount.Text != "0" ? locOrderBulk.lblSumAmount.Text : "");
                            fgList[iRow, 19] = klsOrder.Curr;
                            fgList[iRow, 20] = (sConstant[klsOrder.Constant] + " " + klsOrder.ConstantDate).Trim();
                            fgList[iRow, "SE_Code"] = klsOrder.StockExchange_Title;
                            fgList[iRow, "RecieveDate"] = (klsOrder.RecieveDate.Date == Convert.ToDateTime("01/01/1900").Date ? "" : klsOrder.RecieveDate.ToString("yyyy/MM/dd"));
                            fgList[iRow, "SentDate"] = (klsOrder.SentDate.Date == Convert.ToDateTime("01/01/1900").Date ? "" : klsOrder.SentDate.ToString("yyyy/MM/dd"));
                            fgList[iRow, "ExecuteDate"] = (klsOrder.ExecuteDate.Date == Convert.ToDateTime("01/01/1900").Date ? "" : klsOrder.ExecuteDate.ToString("yyyy/MM/dd"));

                            fgList[iRow, "RecieveTitle"] = klsOrder.RecieveTitle;
                            fgList[iRow, "OfficialInformingDate"] = klsOrder.OfficialInformingDate;
                            fgList[iRow, "Notes"] = klsOrder.Notes;

                            fgList[iRow, "Author_Name"] = klsOrder.AuthorName;                           // 28
                            fgList[iRow, "Advisor_Name"] = klsOrder.AdvisorName;
                            fgList[iRow, "SE_Title"] = klsOrder.StockExchange_Title;
                            fgList[iRow, "FeesPercent"] = klsOrder.FeesPercent;
                            fgList[iRow, "FeesAmount"] = klsOrder.FeesAmount;
                            fgList[iRow, "FeesDiscountPercent"] = klsOrder.FeesDiscountPercent;
                            fgList[iRow, "FeesDiscountAmount"] = klsOrder.FeesDiscountAmount;
                            fgList[iRow, "FinishFeesPercent"] = klsOrder.FinishFeesPercent;
                            fgList[iRow, "FinishFeesAmount"] = klsOrder.FinishFeesAmount;
                            fgList[iRow, "ProviderFees"] = klsOrder.ProviderFees;                        // 45

                            fgList[iRow, "Client_ID"] = klsOrder.Client_ID;
                            fgList[iRow, "Provider_ID"] = klsOrder.ServiceProvider_ID;
                            fgList[iRow, "Status"] = klsOrder.Status;
                            fgList[iRow, "Share_ID"] = klsOrder.Share_ID;
                            fgList[iRow, "Contract_ID"] = klsOrder.Contract_ID;
                            fgList[iRow, "SendCheck"] = klsOrder.SendCheck;
                            fgList.Redraw = true;

                            //-------  read Command Data --------------------
                            DefinePreOrdersList();
                        }
                        break;
                    case 4:
                        frmOrderDPM locOrderDPM = new frmOrderDPM();
                        locOrderDPM.Rec_ID = Convert.ToInt32(fgList[iRow, "ID"]);
                        locOrderDPM.CommandType_ID = iCommandType_ID;                           // 4 - DPM Order
                        locOrderDPM.RightsLevel = iRightsLevel;
                        locOrderDPM.Editable = 1;
                        locOrderDPM.ShowDialog();
                        if (locOrderDPM.LastAktion == 1)
                        {                                            // Aktion=1        was saved (added)
                            clsOrdersSecurity klsOrder = new clsOrdersSecurity();
                            klsOrder.Record_ID = Convert.ToInt32(fgList[iRow, "ID"]);
                            klsOrder.CommandType_ID = iCommandType_ID;
                            klsOrder.GetRecord();
                            fgList[iRow, "ContractTitle"] = klsOrder.ContractTitle;
                            //fgList[iRow, "Provider_Title"] = klsOrder.ServiceProvider_Title;
                            fgList[iRow, "Code"] = klsOrder.Code;
                            fgList[iRow, "Portfolio"] = klsOrder.ProfitCenter;
                            fgList[iRow, "Aktion"] = (klsOrder.Aktion == 1 ? "BUY" : "SELL");
                            fgList[iRow, "Product_Title"] = klsOrder.Product_Title;
                            fgList[iRow, 10] = klsOrder.Security_Title;
                            fgList[iRow, 11] = klsOrder.Security_Code;
                            fgList[iRow, 12] = klsOrder.Security_ISIN;
                            fgList[iRow, 13] = Global.ShowPrices(klsOrder.PriceType, Convert.ToSingle(klsOrder.Price));
                            fgList[iRow, 14] = klsOrder.Quantity.ToString("0.00");
                            fgList[iRow, 15] = klsOrder.Amount.ToString("0.00");
                            fgList[iRow, 16] = (locOrderDPM.txtRealPrice.Text != "0" ? locOrderDPM.txtRealPrice.Text : "");
                            fgList[iRow, 17] = (locOrderDPM.txtRealQuantity.Text != "0" ? locOrderDPM.txtRealQuantity.Text : "");
                            fgList[iRow, 18] = (locOrderDPM.txtRealAmount.Text != "0" ? locOrderDPM.txtRealAmount.Text : "");
                            fgList[iRow, 19] = klsOrder.Curr;
                            fgList[iRow, 20] = (sConstant[klsOrder.Constant] + " " + klsOrder.ConstantDate).Trim();
                            fgList[iRow, "SE_Code"] = klsOrder.StockExchange_Title;
                            fgList[iRow, "RecieveDate"] = (klsOrder.RecieveDate.Date == Convert.ToDateTime("01/01/1900").Date ? "" : klsOrder.RecieveDate.ToString("yyyy/MM/dd"));
                            fgList[iRow, "SentDate"] = (klsOrder.SentDate.Date == Convert.ToDateTime("01/01/1900").Date ? "" : klsOrder.SentDate.ToString("yyyy/MM/dd"));
                            fgList[iRow, "ExecuteDate"] = (klsOrder.ExecuteDate.Date == Convert.ToDateTime("01/01/1900").Date ? "" : klsOrder.ExecuteDate.ToString("yyyy/MM/dd"));

                            fgList[iRow, "RecieveTitle"] = klsOrder.RecieveTitle;
                            fgList[iRow, "OfficialInformingDate"] = klsOrder.AllocationPercent;  
                            fgList[iRow, "Notes"] = klsOrder.Notes;

                            fgList[iRow, "Author_Name"] = klsOrder.AuthorName;                           // 28
                            fgList[iRow, "Advisor_Name"] = klsOrder.AdvisorName;
                            fgList[iRow, "SE_Title"] = klsOrder.StockExchange_Title;
                            fgList[iRow, "FeesPercent"] = klsOrder.FeesPercent;
                            fgList[iRow, "FeesAmount"] = klsOrder.FeesAmount;
                            fgList[iRow, "FeesDiscountPercent"] = klsOrder.FeesDiscountPercent;
                            fgList[iRow, "FeesDiscountAmount"] = klsOrder.FeesDiscountAmount;
                            fgList[iRow, "FinishFeesPercent"] = klsOrder.FinishFeesPercent;
                            fgList[iRow, "FinishFeesAmount"] = klsOrder.FinishFeesAmount;
                            fgList[iRow, "ProviderFees"] = klsOrder.ProviderFees;                        // 45

                            fgList[iRow, "Client_ID"] = klsOrder.Client_ID;
                            fgList[iRow, "Provider_ID"] = klsOrder.ServiceProvider_ID;
                            fgList[iRow, "Status"] = klsOrder.Status;
                            fgList[iRow, "Share_ID"] = klsOrder.Share_ID;
                            fgList[iRow, "Contract_ID"] = klsOrder.Contract_ID;
                            fgList[iRow, "SendCheck"] = klsOrder.SendCheck;
                            fgList.Redraw = true;

                            //-------  read Command Data --------------------
                            DefinePreOrdersList();
                        }
                        break;
                }
            }
        }
        private void fgList_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            if (e.Row > 1)
            {
                if (e.Col == 8)                                                                                 // 8 - Action
                    if ((fgList[e.Row, "Aktion"] + "") == "BUY") e.Style = csBuy;
                    else e.Style = csSell;

                if (e.Col == 16 || e.Col == 17 || e.Col == 18)
                    if ((fgList[e.Row, e.Col] + "") != "")
                        if ((fgList[e.Row, e.Col] + "") != "0")
                            if ((fgList[e.Row, "Quantity"] + "") != (fgList[e.Row, "RealQuantity"] + "")) e.Style = csOrange;
                            else
                               if ((fgList[e.Row, "Aktion"] + "") == "BUY") e.Style = csBuy;
                               else e.Style = csSell;

                if (e.Col == 24)
                {
                    if ((fgList[e.Row, "SendCheck"] + "") == "1") e.Style = csChecked;                          
                    if ((fgList[e.Row, "FIX_A"] + "") == "1") e.Style = csChecked;                               
                }
            }
        }
        private void fgList_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Row > 1)  {
                if (e.Col == 50)  {                                                                              // 49 - Status
                    if (Convert.ToInt32(fgList[e.Row, "Status"]) < 0)
                        fgList.Rows[e.Row].Style = csCancel;
                    else fgList.Rows[e.Row].Style = null;
                }

                if (e.Col == 51) {                                                                              // 51 - Styles
                    rng = fgList.GetCellRange(e.Row, 1, e.Row, 1);
                    if (Convert.ToInt32(fgList[e.Row, "Styles"]) == 1) rng.Style = csGroup1;
                    if (Convert.ToInt32(fgList[e.Row, "Styles"]) == 2) rng.Style = csGroup2;
                }
            }
        }
        #endregion
        #region --- PreOrders functions --------------------------------------------------------------
        private void btnPre_CleanUp_Click(object sender, EventArgs e)
        {
            chkPreOrders.Checked = false;
            txtFilter.Text = "";
            iPreClient_ID = 0;
            iAdvisor_ID = 0;
            sPreCode = "";
            sPreISIN = "";
            DefinePreOrdersList();
        }
        private void picRecieveVoiceFilePath_Click(object sender, EventArgs e)
        {
            txtPre_RecieveVoicePath.Text = Global.FileChoice(Global.DefaultFolder);
        }
        private void picRecieveVoiceShow_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(txtPre_RecieveVoicePath.Text);
        }
        private void txtPre_Price_LostFocus(object sender, EventArgs e)
        {
            if (Global.IsNumeric(txtPre_Price.Text) && Global.IsNumeric(txtPre_Quantity.Text)) 
                txtPre_Amount.Text = (Convert.ToDecimal(txtPre_Price.Text) * Convert.ToDecimal(txtPre_Quantity.Text)).ToString();
        }

        private void txtPre_Quantity_LostFocus(object sender, EventArgs e)
        {
            if (Global.IsNumeric(txtPre_Price.Text) && Global.IsNumeric(txtPre_Quantity.Text))
                txtPre_Amount.Text = (Convert.ToDecimal(txtPre_Price.Text) * Convert.ToDecimal(txtPre_Quantity.Text)).ToString();
        }
        private void btnAgree_Click(object sender, EventArgs e)
        {
            string sNotes = "";
            int iLocCommandType_ID = 1;                   // from InvestProposals always create orders with CommandType_ID = 1                          
            sUploadFile = "";                             // sUploadFile - name of Updated file. It must be loaded only ONE time !!!!!!!!!!!!!!

            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            try
            {
                for (i = 1; i <= fgPreOrders.Rows.Count - 1; i++)
                {
                    if (Convert.ToBoolean(fgPreOrders[i, 0]))
                    {
                        sFileName = "";
                        if (sUploadFile.Length == 0 && txtPre_RecieveVoicePath.Text.Trim().Length > 0)
                        {
                            sFileName = Path.GetFileName(txtPre_RecieveVoicePath.Text.Trim());
                            sFileName = Global.DMS_UploadFile(txtPre_RecieveVoicePath.Text.Trim(), "Customers/" + (fgPreOrders[i, "ContractTitle"]+"").Replace(".", "_") + "/OrdersAcception", sFileName);
                            sUploadFile = sFileName;
                        }

                        iBusinessType_ID = 1;
                        iProviderType = Convert.ToInt32(fgPreOrders[i, "ProviderType"]);
                        if (iProviderType == 1) iBusinessType_ID = 1;                        // 1 - CreditSuisse  
                        if (iProviderType == 2) iBusinessType_ID = 2;                        // 2 - HF2S 

                        if (iCheckedRows == 1)
                        {
                            if (lblPre_Notes.Text != "" || txtPre_RTONotes.Text != "") sNotes = lblPre_Notes.Text.Trim() + "/" + txtPre_RTONotes.Text.Trim();
                            iID = SaveTransaction(iBusinessType_ID, iLocCommandType_ID, Convert.ToInt32(fgPreOrders[i, "II_ID"]), Convert.ToInt32(fgPreOrders[i, "Client_ID"]), fgPreOrders[i, "Code"] + "", fgPreOrders[i, "Portfolio"] + "",
                                                Convert.ToInt32(fgPreOrders[i, "Contract_ID"]), Convert.ToInt32(fgPreOrders[i, "Contract_Details_ID"]), Convert.ToInt32(fgPreOrders[i, "Contract_Packages_ID"]),
                                                fgPreOrders[i, "ClientName"] + "", fgPreOrders[i, "Aktion"] + "", DateTime.Now, Convert.ToInt32(fgPreOrders[i, "Product_ID"]),
                                                Convert.ToInt32(fgPreOrders[i, "ProductCategories_ID"]), Convert.ToInt32(fgPreOrders[i, "Share_ID"]),
                                                txtPre_Quantity.Text, Convert.ToInt32(fgPreOrders[i, "PriceType"]), txtPre_Price.Text, txtPre_PriceUp.Text,
                                                txtPre_PriceDown.Text, txtPre_Amount.Text, fgPreOrders[i, "Currency"] + "", cmbPre_Constant.SelectedIndex, dPre_Constant.Value.ToString("yyyy/MM/dd"),
                                                Convert.ToInt32(fgPreOrders[i, "Provider_ID"]), Convert.ToInt32(fgPreOrders[i, "StockExchange_ID"]), fgPreOrders[i, "StockExchange_Code"] + "",
                                                Convert.ToInt32(cmbRecieveMethod3.SelectedValue), txtPre_RecieveVoicePath.Text, "", sNotes);
                        }
                        else
                        {
                            if (fgPreOrders[i, "Notes"] + "" != "" || fgPreOrders[i, "RTO_Notes"] + "" != "") sNotes = fgPreOrders[i, "Notes"] + "/" + fgPreOrders[i, "RTO_Notes"] + " " + txtPre_RTONotes.Text.Trim();
                            iID = SaveTransaction(iBusinessType_ID, iLocCommandType_ID, Convert.ToInt32(fgPreOrders[i, "II_ID"]), Convert.ToInt32(fgPreOrders[i, "Client_ID"]), fgPreOrders[i, "Code"] + "", fgPreOrders[i, "Portfolio"] + "",
                                            Convert.ToInt32(fgPreOrders[i, "Contract_ID"]), Convert.ToInt32(fgPreOrders[i, "Contract_Details_ID"]), Convert.ToInt32(fgPreOrders[i, "Contract_Packages_ID"]),
                                            fgPreOrders[i, "ClientName"] + "", fgPreOrders[i, "Aktion"] + "", DateTime.Now, Convert.ToInt32(fgPreOrders[i, "Product_ID"]),
                                            Convert.ToInt32(fgPreOrders[i, "ProductCategories_ID"]), Convert.ToInt32(fgPreOrders[i, "Share_ID"]),
                                            fgPreOrders[i, "Quantity"] + "", Convert.ToInt32(fgPreOrders[i, "PriceType"]), fgPreOrders[i, "Price"] + "", fgPreOrders[i, "PriceUp"] + "",
                                            fgPreOrders[i, "PriceDown"] + "", fgPreOrders[i, "Amount"] + "", fgPreOrders[i, "Currency"] + "", Convert.ToInt32(fgPreOrders[i, "Constant"]), fgPreOrders[i, "ConstantDate"] + "",
                                            Convert.ToInt32(fgPreOrders[i, "Provider_ID"]), Convert.ToInt32(fgPreOrders[i, "StockExchange_ID"]), fgPreOrders[i, "StockExchange_Code"] + "",
                                            Convert.ToInt32(cmbRecieveMethod3.SelectedValue), txtPre_RecieveVoicePath.Text, "", sNotes);
                        }

                        EmptyCommand();

                        if (iID > 0) {                                                                            // iID > 0 means that new record into Commands table was created
                            
                            if (sUploadFile.Length == 0 && txtPre_RecieveVoicePath.Text.Trim().Length > 0) {      // sUploadFile.Length == 0 means that yet file wasn't uploaded

                                sFileName = Path.GetFileName(txtPre_RecieveVoicePath.Text.Trim());
                                sTemp = "Customers/" + (fgPreOrders[i, "ContractTitle"]+"").Replace(".", "_") + "/InvestProposals/" + fgPreOrders[i, "II_ID"] + "/";
                                sFileName = Global.DMS_UploadFile(txtPre_RecieveVoicePath.Text.Trim(), sTemp, sFileName);
                                sUploadFile = sFileName;
                            }
                            InvestIdees_Commands.Record_ID = Convert.ToInt32(fgPreOrders[i, "ID"]);
                            InvestIdees_Commands.GetRecord();
                            InvestIdees_Commands.Command_ID = iID;
                            InvestIdees_Commands.RecieveDate = DateTime.Now;
                            InvestIdees_Commands.Status = 5;                             // 1-New, 2-Skeptikos, 3-Wait, 4-Mi apodoxi, 5-Apodoxi, 6-Cancel
                            InvestIdees_Commands.RTO_Notes = txtPre_RTONotes.Text;
                            InvestIdees_Commands.RecieveVoicePath = txtPre_RecieveVoicePath.Text;
                            InvestIdees_Commands.EditStatus();
                        }
                    }
                }

                for (i = fgPreOrders.Rows.Count - 1; i >= 1; i = i - 1)
                    if (Convert.ToBoolean(fgPreOrders[i, 0]))
                        fgPreOrders.RemoveItem(i);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

            this.Cursor = Cursors.Default;

            Empty_PreOrder();
            iPreClient_ID = 0;
            DefinePreOrdersList();
            DefineList();

            panPreOrders.Visible = false;
        }
        private void btnThinks_Click(object sender, EventArgs e)
        {
            sFileName = "";
            if (txtPre_RecieveVoicePath.Text != "") {

                if (lblPre_ContractTitle.Text.Length > 0) sTemp = lblPre_ContractTitle.Text;
                else sTemp = (fgPreOrders[fgPreOrders.Row, "ContractTitle"] + "");

                sFileName = Path.GetFileName(txtPre_RecieveVoicePath.Text);
                sFileName = Global.DMS_UploadFile(txtPre_RecieveVoicePath.Text, "Customers/" + sTemp.Replace(".", "_") + "/InvestProposals/" + fgPreOrders[fgPreOrders.Row, "II_ID"],
                                           sFileName);
                sFileName = Path.GetFileName(sFileName);
            }
            for (i = 1; i <= fgPreOrders.Rows.Count - 1; i++) {
                if (Convert.ToBoolean(fgPreOrders[i, 0]))  {
                    InvestIdees_Commands = new clsInvestIdees_Commands(); 
                    InvestIdees_Commands.Record_ID = Convert.ToInt32(fgPreOrders[i, "ID"]);
                    InvestIdees_Commands.GetRecord();
                    InvestIdees_Commands.Command_ID = 0;
                    InvestIdees_Commands.RecieveDate = DateTime.Now;
                    InvestIdees_Commands.Status = 2;                                                   // 1-New, 2-Skeptikos, 3-Wait, 4-Mi apodoxi, 5-Apodoxi, 6-Cancel
                    InvestIdees_Commands.RTO_Notes = txtPre_RTONotes.Text;
                    InvestIdees_Commands.RecieveVoicePath = sFileName;
                    InvestIdees_Commands.EditStatus();

                    fgPreOrders[i, "Status_Title"] = Global.GetLabel("pensive");
                    fgPreOrders[i, "RTO_Notes"] = txtPre_RTONotes.Text;
                    fgPreOrders[i, "Status"] = 2;
                }
            }
        }
        private void btnWait_Click(object sender, EventArgs e)
        {
            sFileName = "";
            if (txtPre_RecieveVoicePath.Text != "") {
                sFileName = Path.GetFileName(txtPre_RecieveVoicePath.Text);

                if (lblPre_ContractTitle.Text.Length > 0) sTemp = lblPre_ContractTitle.Text;
                else sTemp = (fgPreOrders[fgPreOrders.Row, "ContractTitle"] + "");

                sFileName = Global.DMS_UploadFile(txtPre_RecieveVoicePath.Text, "Customers/" + sTemp.Replace(".", "_") + "/InvestProposals/" + fgPreOrders[fgPreOrders.Row, "II_ID"],
                                           sFileName);
                sFileName = Path.GetFileName(sFileName);
            }
            for (i = 1; i <= fgPreOrders.Rows.Count - 1; i++)
            {
                if (Convert.ToBoolean(fgPreOrders[i, 0]))
                {
                    InvestIdees_Commands = new clsInvestIdees_Commands();
                    InvestIdees_Commands.Record_ID = Convert.ToInt32(fgPreOrders[i, "ID"]);
                    InvestIdees_Commands.GetRecord();
                    InvestIdees_Commands.Command_ID = 0;
                    InvestIdees_Commands.RecieveDate = DateTime.Now;
                    InvestIdees_Commands.Status = 3;                                                   // 1-New, 2-Skeptikos, 3-Wait, 4-Mi apodoxi, 5-Apodoxi, 6-Cancel
                    InvestIdees_Commands.RTO_Notes = txtPre_RTONotes.Text;
                    InvestIdees_Commands.RecieveVoicePath = sFileName;
                    InvestIdees_Commands.EditStatus();

                    fgPreOrders[i, "Status_Title"] = Global.GetLabel("pensive");
                    fgPreOrders[i, "RTO_Notes"] = txtPre_RTONotes.Text;
                    fgPreOrders[i, "Status"] = 3;
                }
            }
        }
        private void btnNotAgree_Click(object sender, EventArgs e)
        {
            sFileName = "";
            if (txtPre_RecieveVoicePath.Text != "") {

                if (lblPre_ContractTitle.Text.Length > 0) sTemp = lblPre_ContractTitle.Text;
                else sTemp = (fgPreOrders[fgPreOrders.Row, "ContractTitle"] + "");

                sFileName = Path.GetFileName(txtPre_RecieveVoicePath.Text);
                sFileName = Global.DMS_UploadFile(txtPre_RecieveVoicePath.Text, "Customers/" + sTemp.Replace(".", "_") + "/InvestProposals/" + fgPreOrders[fgPreOrders.Row, "II_ID"],
                                           sFileName);
                sFileName = Path.GetFileName(sFileName);
            }

            for (i = fgPreOrders.Rows.Count - 1; i >= 1; i = i - 1) { 
                if (Convert.ToBoolean(fgPreOrders[i, 0]))  {
                    InvestIdees_Commands = new clsInvestIdees_Commands();
                    InvestIdees_Commands.Record_ID = Convert.ToInt32(fgPreOrders[i, "ID"]);
                    InvestIdees_Commands.GetRecord();
                    InvestIdees_Commands.Command_ID = 0;
                    InvestIdees_Commands.RecieveDate = DateTime.Now;
                    InvestIdees_Commands.Status = 4;                                                   // 1-New, 2-Skeptikos, 3-Wait, 4-Mi apodoxi, 5-Apodoxi, 6-Cancel
                    InvestIdees_Commands.RTO_Notes = txtPre_RTONotes.Text;
                    InvestIdees_Commands.RecieveVoicePath = sFileName;
                    InvestIdees_Commands.EditStatus();

                    fgPreOrders.RemoveItem(i);
                }
            }
            Empty_PreOrder();
        }
        private void Empty_PreOrder() {            
            lblPre_II_ID.Text = "";
            lblPre_ContractTitle.Text = "";
            lblPre_Code.Text = "";
            lblPre_Subcode.Text = "";
            lblPre_Action.Text = "";
            cmbPre_Constant.SelectedIndex = 0;
            dPre_Constant.Value = DateTime.Now;
            dPre_Constant.Visible = false;
            lblPre_Product.Text = "";
            lblPre_Title.Text = "";
            lblPre_ISIN.Text = "";
            lblPre_Reuters.Text = "";
            txtPre_Quantity.Text = "";
            txtPre_Amount.Text = "";
            cmbPre_Type.SelectedValue = 0;
            txtPre_Price.Text = "";
            lblPre_Curr.Text = "";
            txtPre_PriceUp.Text = "";
            txtPre_PriceDown.Text = "";
            txtPre_RecieveVoicePath.Text = "";
            lblPre_Notes.Text = "";
            lblPre_Tel.Text = "";
            lblPre_Mobile.Text = "";
            txtPre_RTONotes.Text = "";
            cmbRecieveMethod3.SelectedValue = 1;                                // 1 - Telephone
            panPre_Data.Enabled = false;
        }
        public void DefinePreOrdersList()
        {
            int i = 0;

            sInvPropNotesFlag = "";
            sDPMNotesFlag = "";
            fgPreOrders.Redraw = false;
            fgPreOrders.Rows.Count = 1;
            clsInvestIdees klsInvestIdees = new clsInvestIdees();
            klsInvestIdees.AktionDate = dToday.Value;
            klsInvestIdees.Client_ID = iPreClient_ID;
            klsInvestIdees.Code = sPreCode + "";
            klsInvestIdees.ISIN = sPreISIN + "";
            klsInvestIdees.Advisor_ID = iAdvisor_ID;
            klsInvestIdees.GetList_NonRecieved();
            foreach (DataRow dtRow in klsInvestIdees.List.Rows) {
                if ((dtRow["ClientFullName"] + "").ToUpper().IndexOf(txtFilter.Text.ToUpper()) >= 0 || (dtRow["ContractTitle"] + "").ToUpper().IndexOf(txtFilter.Text.ToUpper()) >= 0) {
                    if (Convert.ToDateTime(dtRow["RTODate"]) != Convert.ToDateTime("1900/01/01")) {
                        if ((dtRow["StatusTitle"] + "") == "") sInvPropNotesFlag = "*";
                    }
                    else dtRow["RTODate"] = "";

                    i = i + 1;

                    fgPreOrders.AddItem(false + "\t" + i + "\t" + dtRow["II_ID"] + "\t" + dtRow["ClientFullName"] + "\t" + dtRow["ContractTitle"] + "\t" + dtRow["ServiceProviders_Title"] + "\t" +
                                   dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" + dtRow["Aktion"] + "\t" + dtRow["Products_Title"] + "/" + dtRow["Products_Categories_Title"] + "\t" +
                                   dtRow["ShareTitle"] + "\t" + dtRow["ISIN"] + "\t" + dtRow["ShareCode"] + "\t" +
                                   (Convert.ToInt32(dtRow["PriceType"]) == 0 ? dtRow["Price"] : sPriceType[Convert.ToInt32(dtRow["PriceType"])]) + "\t" + dtRow["Quantity"] + "\t" + dtRow["Amount"] + "\t" +
                                   dtRow["Curr"] + "\t" + (sConstant[Convert.ToInt16(dtRow["Constant"])] + " " + dtRow["ConstantDate"]).Trim() + "\t" +
                                   dtRow["StockExchanges_Title"] + "\t" + dtRow["DateIns"] + "\t" + dtRow["RTODate"] + "\t" + dtRow["Notes"] + "\t" +
                                   dtRow["StatusTitle"] + "\t" + dtRow["RTO_Notes"] + "\t" + dtRow["Advisor_Name"] + "\t" + dtRow["Author_Name"] + "\t" +
                                   dtRow["ID"] + "\t" + dtRow["Client_ID"] + "\t" + dtRow["StockCompany_ID"] + "\t" + dtRow["ConfirmationStatus"] + "\t" + dtRow["Share_ID"] + "\t" +
                                   dtRow["Contract_ID"] + "\t" + dtRow["Product_ID"] + "\t" + dtRow["ProductCategory_ID"] + "\t" + dtRow["StockExchange_ID"] + "\t" +
                                   dtRow["PriceType"] + "\t" + dtRow["PriceUP"] + "\t" + dtRow["PriceDown"] + "\t" + dtRow["Tel"] + "\t" + dtRow["Mobile"] + "\t" +
                                   dtRow["Advisor_ID"] + "\t" + dtRow["Author_ID"] + "\t" + dtRow["Constant"] + "\t" + dtRow["ConstantDate"] + "\t" + dtRow["ShareCode2"] + "\t" +
                                   dtRow["ProviderType"] + "\t" + dtRow["Status"] + "\t" + dtRow["Contract_Details_ID"] + "\t" + dtRow["Contract_Packages_ID"] + "\t" + dtRow["Client_Type"]);

                }
            }

            jj = 0;
            fgPreOrders.Row = 0;
            fgPreOrders.Redraw = true;

            if (fgPreOrders.Rows.Count > 2) chkPreOrders.Visible = true;
            else chkPreOrders.Visible = false;

            clsOrdersDPM klsOrdersDPM = new clsOrdersDPM();
            klsOrdersDPM.DateFrom = Convert.ToDateTime("1900/01/01");
            klsOrdersDPM.DateTo = dToday.Value;
            klsOrdersDPM.User_ID = 0;
            klsOrdersDPM.GetList_NewOrders();
            foreach (DataRow dtRow in klsOrdersDPM.List.Rows)
            {
                if (iProvider_ID == 0 || Convert.ToInt32(dtRow["StockCompany_ID"]) == iProvider_ID) {
                    jj = jj + 1;
                    sDPMNotesFlag = "*";
                }
            }        

            tslPreOrders.Text = "Επενδυτικές Συμβουλές: " + i + " " + sInvPropNotesFlag;
            tslDPMOrders.Text = "DPM Orders: " + jj + " " + sDPMNotesFlag;

            panPreOrders.Left = (Screen.PrimaryScreen.Bounds.Width - panPreOrders.Width) / 2;
            panPreOrders.Top = (Screen.PrimaryScreen.Bounds.Height - panPreOrders.Height) / 2;
        }
        private void fgPreOrders_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (e.Col == 0) e.Cancel = false;
            else e.Cancel = true;
        }
        private void fgPreOrders_AfterEdit(object sender, RowColEventArgs e)
        {
            int i = 0, j = 0;                                        //  i - counter, j - number of last checked row

            iCheckedRows = 0;
            for (i = 1; i <= fgPreOrders.Rows.Count - 1; i++)
                if (Convert.ToBoolean(fgPreOrders[i, 0])) {
                    iCheckedRows = iCheckedRows + 1;
                    j = i;
                }
            if (iCheckedRows != 1) Empty_PreOrder();
            else {
                panPre_Data.Enabled = true;
                lblPre_II_ID.Text = fgPreOrders[j, "II_ID"] + "";
                lblPre_ContractTitle.Text = fgPreOrders[j, "ContractTitle"] + "";
                lblPre_Tel.Text = fgPreOrders[j, "ClientTel"] + "";
                lblPre_Mobile.Text = fgPreOrders[j, "ClientMobile"] + "";
                lblPre_Code.Text = fgPreOrders[j, "Code"] + "";
                lblPre_Subcode.Text = fgPreOrders[j, "Portfolio"] + "";
                lblPre_Action.Text = fgPreOrders[j, "Aktion"] + "";
                lblPre_Product.Text = fgPreOrders[j, "Product_Title"] + "";
                lblPre_Title.Text = fgPreOrders[j, "Share_Title"] + "";
                lblPre_ISIN.Text = fgPreOrders[j, "Share_ISIN"] + "";
                lblPre_Reuters.Text = fgPreOrders[j, "Share_Code"] + "";
                txtPre_Price.Text = fgPreOrders[j, "Price"] + "";
                txtPre_Quantity.Text = fgPreOrders[j, "Quantity"] + "";
                cmbPre_Type.SelectedIndex = Convert.ToInt32(fgPreOrders[j, "PriceType"]);
                txtPre_Amount.Text = fgPreOrders[j, "Amount"] + "";
                lblPre_Curr.Text = fgPreOrders[j, "Currency"] + "";
                cmbPre_Constant.SelectedIndex = Convert.ToInt32(fgPreOrders[j, "Constant"]);
                if (Convert.ToInt32(fgPreOrders[j, "Constant"]) == 2)
                {
                    dPre_Constant.Visible = true;
                    dPre_Constant.Value = Convert.ToDateTime(fgPreOrders[j, "ConstantDate"]);
                }
                else dPre_Constant.Visible = false;
                txtPre_PriceUp.Text = fgPreOrders[j, "PriceUp"] + "";
                txtPre_PriceDown.Text = fgPreOrders[j, "PriceDown"] + "";
                txtPre_RecieveVoicePath.Text = "";
                lblPre_Notes.Text = fgPreOrders[j, "Notes"] + "";
                txtPre_RTONotes.Text = fgPreOrders[j, "RTO_Notes"] + "";
                if (Convert.ToInt32(fgPreOrders[j, "PriceType"]) == 3)
                {
                    if (fgPreOrders[j, "Aktion"] + "" == "BUY")
                    {
                        picPre_PriceUp.Visible = true;
                        txtPre_PriceUp.Visible = true;
                    }
                    else
                    {
                        picPre_PriceUp.Visible = false;
                        txtPre_PriceUp.Visible = false;
                    }
                    picPre_PriceDown.Visible = true;
                    txtPre_PriceDown.Visible = true;
                }
                else
                {
                    picPre_PriceUp.Visible = false;
                    txtPre_PriceUp.Visible = false;
                    picPre_PriceDown.Visible = false;
                    txtPre_PriceDown.Visible = false;
                }

                switch (Convert.ToInt32(fgPreOrders[j, "Product_ID"]))
                {
                    case 1:
                    case 4:
                        lblPre_Price.Visible = true;
                        txtPre_Price.Visible = true;
                        lblPre_Curr.Visible = true;
                        lblPre_Quantity.Visible = true;
                        txtPre_Quantity.Visible = true;
                        lblPre_Quantity.Text = Global.GetLabel("pieces");
                        break;
                    case 2:
                        lblPre_Price.Visible = true;
                        txtPre_Price.Visible = true;
                        lblPre_Curr.Visible = true;
                        lblPre_Quantity.Visible = true;
                        txtPre_Quantity.Visible = true;
                        lblPre_Quantity.Text = Global.GetLabel("nomical_value");
                        break;
                    case 6:
                        lblPre_Price.Visible = false;
                        txtPre_Price.Visible = false;
                        lblPre_Curr.Visible = false;
                        //'lblPre_Quantity.Visible = false;
                        //'txtPre_Quantity.Visible = false;
                        lblPre_Quantity.Text = Global.GetLabel("shares");
                        break;
                }
            }

            if (iCheckedRows == 0 ) panButtons.Enabled = false;
            else panButtons.Enabled = true;
        }
        private void fgPreOrders_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Row > 0) {
                if (e.Col == 46) {                                                                              // 46 - Status
                    if (Convert.ToInt32(fgPreOrders[e.Row, "Status"]) == 2)
                        fgPreOrders.Rows[e.Row].Style = csThinks;
                    if (Convert.ToInt32(fgPreOrders[e.Row, "Status"]) == 3)
                        fgPreOrders.Rows[e.Row].Style = csWait;
                }
            }
        }
        private void fgPreOrders_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            if (e.Row > 0) {
                if (e.Col == 8)    {                                                                             // 8 - Aktion
                    if ((fgPreOrders[e.Row, 8] + "") == "BUY") e.Style = csBuy;
                    else e.Style = csSell;
                }
            }
        }
        private void fgPreOrders_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) {
                fgPreOrders.ContextMenuStrip = mnuPreContext;
                fgPreOrders.Row = fgPreOrders.MouseRow;
            }
        }
        private void picPreOrders_Click(object sender, EventArgs e)
        {
            panPreOrders.Visible = false;

            chkPreOrders.Checked = false;
            txtFilter.Text = "";
            iPreClient_ID = 0;
            iAdvisor_ID = 0;
            sPreCode = "";
            sPreISIN = "";
            DefinePreOrdersList();
        }
        private void mnuPreClientData_Click(object sender, EventArgs e)
        {
            frmClientData locClientData = new frmClientData();
            locClientData.Client_ID = Convert.ToInt32(fgPreOrders[fgPreOrders.Row, "Client_ID"]);
            locClientData.Text = Global.GetLabel("customer_information");
            locClientData.Show();
        }
        private void mnuPreContractData_Click(object sender, EventArgs e)
        {
            frmContract locContract = new frmContract();
            locContract.Aktion = 1;
            locContract.Contract_ID = Convert.ToInt32(fgPreOrders[fgPreOrders.Row, "Contract_ID"]);
            locContract.Contract_Details_ID = Convert.ToInt32(fgPreOrders[fgPreOrders.Row, "Contract_Details_ID"]);
            locContract.Contract_Packages_ID = Convert.ToInt32(fgPreOrders[fgPreOrders.Row, "Contract_Packages_ID"]);
            locContract.Client_ID = Convert.ToInt32(fgPreOrders[fgPreOrders.Row, "Client_ID"]);
            locContract.ClientType = Convert.ToInt32(fgPreOrders[fgPreOrders.Row, "Client_Type"]);
            locContract.ClientFullName = fgPreOrders[fgPreOrders.Row, "ClientName"] + "";
            locContract.RightsLevel = iRightsLevel;
            locContract.ShowDialog();
        }
        private void mnuPreInvestProposals_Click(object sender, EventArgs e)
        {
            frmInvestProposal locInvestProposal_Rec = new frmInvestProposal();
            locInvestProposal_Rec.Aktion = 1;              // 0 - Edit
            locInvestProposal_Rec.II_ID = Convert.ToInt32(fgPreOrders[fgPreOrders.Row, "II_ID"]);
            locInvestProposal_Rec.ShowDialog();
        }
        private void mnuPreFilterClient_Click(object sender, EventArgs e)
        {
            iPreClient_ID = Convert.ToInt32(fgPreOrders[fgPreOrders.Row, "Client_ID"]);
            Empty_PreOrder();
            DefinePreOrdersList();
        }

        private void mnuPreFilterClientCode_Click(object sender, EventArgs e)
        {
            sPreCode = fgPreOrders[fgPreOrders.Row, "Code"] + "";
            Empty_PreOrder();
            DefinePreOrdersList();
        }
        private void mnuPreFilterISIN_Click(object sender, EventArgs e)
        {
            sPreISIN = fgPreOrders[fgPreOrders.Row, "ISIN"] + "";
            Empty_PreOrder();
            DefinePreOrdersList();
        }
        private void mnuPreFilterAdvisor_Click(object sender, EventArgs e)
        {
            iAdvisor_ID = Convert.ToInt32(fgPreOrders[fgPreOrders.Row, "Advisor_ID"]);
            Empty_PreOrder();
            DefinePreOrdersList();
        }
        private void mnuPreNoFilters_Click(object sender, EventArgs e)
        {
            iPreClient_ID = 0;
            iAdvisor_ID = 0;
            sPreCode = "";
            sPreISIN = "";
            Empty_PreOrder();
            DefinePreOrdersList();
        }
        private void mnuPreCopyISIN_Click(object sender, EventArgs e)
        {
            if (fgPreOrders.Row >= 1)
                Clipboard.SetDataObject(fgPreOrders[fgPreOrders.Row, "Share_ISIN"], true, 10, 100);
        }
        private void panPreOrders_MouseDown(object sender, MouseEventArgs e)
        {
            this.position = e.Location;
            this.pMove = true;
        }
        private void panPreOrders_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.pMove == true)
                {
                    this.panPreOrders.Location = new Point(this.panPreOrders.Location.X + e.X - this.position.X, this.panPreOrders.Location.Y + e.Y - this.position.Y);
                }
            }
        }
        private void panPreOrders_MouseUp(object sender, MouseEventArgs e)
        {
            this.pMove = false;
        }
        #endregion
        #region --- Search ----------------------------------------------------------
        private void btnSearch_Click(object sender, EventArgs e)
        {
            clsOrdersSecurity klsOrder = new clsOrdersSecurity();


            string sSQL, sFilter, sqlQuery = "", sSelectedContracts = "", sSelectedProducts = "";
            int iOld_ID = -999;

            SqlConnection conn = new SqlConnection(Global.connStr);
            SqlCommand cmd;
            SqlDataReader drList = null;

            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            sSQL = "SELECT DISTINCT   dbo.Commands.*, dbo.Clients.Tipos, dbo.Clients.Surname, dbo.Clients.Firstname, dbo.Clients.SurnameEng, dbo.Clients.FirstnameEng, dbo.Clients.SurnameFather, " +
                   "    dbo.Commands_Check.FileName + '' AS Check_FileName, dbo.Clients.FirstnameFather, dbo.Clients.DoB, dbo.Clients.FirstnameSizigo, dbo.Contracts_Details.Risk, " +
                   "    dbo.Contracts_Details.MiFiDCategory_ID, dbo.Contracts.ContractTitle, dbo.Contracts_Details.[Address], dbo.Contracts_Details.City, dbo.Contracts_Details.Zip, " +
                   "    dbo.Contracts_Details.EMail, dbo.Contracts_Details.ConnectionMethod, dbo.Contracts.Code, dbo.Contracts.Portfolio AS SubCode, dbo.Contracts_Details.SurnameFather AS Recipient,  " +
                   "    dbo.Contracts.Tipos AS ContractTipos, dbo.CompanyFeesPackages.PackageType_ID AS Service_ID, ServiceProviders_1.LEI AS StockCompanyLEI, ServiceProviders_1.Title AS StockCompanyTitle, " +
                   "    dbo.ShareCodes.StockExchange_ID AS ProductStockExchange_ID, dbo.StockExchanges.Title AS ProductStockExchange_Title, dbo.StockExchanges.Code AS ProductStockExchange_MIC, " +
                   "    dbo.Commands.StockExchange_ID AS StockExchange_ID, StockExchanges_1.Code AS StockExchange_MIC, StockExchanges_1.Title AS StockExchange_Title, " +
                   "    dbo.Commands.RealStockExchange_ID AS ExecutionStockExchange_ID, StockExchanges_2.Code AS ExecutionStockExchange_MIC, StockExchanges_2.Title AS ExecutionStockExchange_Title, " +
                   "    dbo.RecieveMethods.Title AS RecieveTitle, dbo.InformationMethods.Title AS InformationTitle, dbo.Keys.Surname AS Author_Surname, dbo.Keys.Firstname AS Author_Firstname, " +
                   "    dbo.ShareTitles.Title AS Share_Title, dbo.ShareCodes.ISIN AS Share_ISIN, dbo.ShareCodes.Code AS Share_Code, dbo.ShareCodes.Code2 AS Share_Code2, " +
                   "    dbo.ShareCodes.QuantityMin, dbo.ShareCodes.QuantityStep, dbo.ShareCodes.HFIC_Recom, dbo.Products.Title AS Product_Title, dbo.Products_Categories.Title AS Product_Category, " +
                   "    dbo.Contracts_Details.User1_ID, Keys_1.Surname AS Advisor_Surname, Keys_1.Firstname AS Advisor_Firstname, Keys_1.DoB AS AdvisorDoB, '' AS PC_Status, Keys_1.Surname, Keys_1.Firstname,  " +
                   "	dbo.FinanceServices.Title AS ServiceTitle, dbo.InvestIdees_Commands.II_ID AS Expr5, dbo.CompanyFeesPackages.AdvisoryInvestmentPolicy_ID,  " +
                   "    Keys_2.Surname AS Diax_Surname, Keys_2.Firstname AS Diax_Firstname, dbo.CompanyFeesPackages.DealAdvisoryInvestmentPolicy_ID, dbo.CompanyFeesPackages.DiscretInvestmentPolicy_ID, " +
                   "    dbo.InvestmentPolicy.Title AS AdvisoryInvestmentPolicy_Title, dbo.FinanceTools.Title AS DealAdvisoryInvestmentPolicy_Title, InvestmentPolicy_1.Title AS DiscretInvestmentPolicy_Title, " +
                   "    dbo.Countries.Title AS Country_Title, dbo.Countries.Code AS Country_Code, ServiceProviders_2.Title AS Company_Title, dbo.ServiceProviders.Title AS Executor_Title, " +
                   "    dbo.Depositories.Title AS Depository_Title, Countries_1.Code  AS CountryTax_Code, Countries_1.Title AS CountryTax_Title, dbo.Invoice_Titles.[FileName], " +
                   "    InvestmentPolicy_2.Title AS InvestPolicy_Title,  dbo.Commands_Executions.ProviderCommandNumber AS ClientOrderID, dbo.InvestmentProfile.Title AS InvestProfile_Title " +
                   "FROM  dbo.InvestmentPolicy AS InvestmentPolicy_1 RIGHT OUTER JOIN " +
                   "    dbo.Countries RIGHT OUTER JOIN " +
                   "    dbo.Countries AS Countries_1 RIGHT OUTER JOIN " +
                   "    dbo.Clients ON Countries_1.ID = dbo.Clients.CountryTaxes_ID ON dbo.Countries.ID = dbo.Clients.Country_ID RIGHT OUTER JOIN " +
                   "    dbo.ServiceProviders RIGHT OUTER JOIN " +
                   "    dbo.Keys AS Keys_2 RIGHT OUTER JOIN " +
                   "    dbo.InvestmentProfile RIGHT OUTER JOIN " +
                   "    dbo.Contracts_Packages ON dbo.InvestmentProfile.ID = dbo.Contracts_Packages.Profile_ID RIGHT OUTER JOIN " +
                   "    dbo.Commands_Executions RIGHT OUTER JOIN " +
                   "    dbo.Commands LEFT OUTER JOIN " +
                   "    dbo.StockExchanges AS StockExchanges_2 ON dbo.Commands.RealStockExchange_ID = StockExchanges_2.ID LEFT OUTER JOIN " +
                   "    dbo.StockExchanges AS StockExchanges_1 ON dbo.Commands.StockExchange_ID = StockExchanges_1.ID LEFT OUTER JOIN " +
                   "    dbo.Commands_Check ON dbo.Commands.ID = dbo.Commands_Check.Command_ID ON dbo.Commands_Executions.Command_ID = dbo.Commands.ID LEFT OUTER JOIN " +
                   "    dbo.Contracts_Details ON dbo.Commands.Contract_Details_ID = dbo.Contracts_Details.ID ON dbo.Contracts_Packages.ID = dbo.Commands.Contract_Packages_ID ON  " +
                   "    Keys_2.ID = dbo.Contracts_Details.User4_ID LEFT OUTER JOIN " +
                   "    dbo.Keys ON dbo.Commands.User_ID = dbo.Keys.ID LEFT OUTER JOIN " +
                   "    dbo.InvestmentPolicy AS InvestmentPolicy_2 ON dbo.Contracts_Details.InvestmentPolicy_ID = InvestmentPolicy_2.ID LEFT OUTER JOIN " +
                   "    dbo.Invoice_Titles ON dbo.Commands.RTO_InvoiceTitle_ID = dbo.Invoice_Titles.ID LEFT OUTER JOIN " +
                   "    dbo.CompanyFeesPackages LEFT OUTER JOIN " +
                   "    dbo.InvestmentPolicy ON dbo.CompanyFeesPackages.AdvisoryInvestmentPolicy_ID = dbo.InvestmentPolicy.ID ON dbo.Contracts_Packages.CFP_ID = dbo.CompanyFeesPackages.ID LEFT OUTER JOIN " +
                   "    dbo.Contracts ON dbo.Commands.ClientPackage_ID = dbo.Contracts.ID LEFT OUTER JOIN " +
                   "    dbo.Depositories ON dbo.Commands.Depository_ID = dbo.Depositories.ID ON dbo.ServiceProviders.ID = dbo.Commands.Executor_ID LEFT OUTER JOIN " +
                   "    dbo.ServiceProviders AS ServiceProviders_2 ON dbo.Commands.Company_ID = ServiceProviders_2.ID ON dbo.Clients.ID = dbo.Commands.Client_ID LEFT OUTER JOIN " +
                   "    dbo.InvestIdees_Commands ON dbo.Commands.ID = dbo.InvestIdees_Commands.Command_ID ON InvestmentPolicy_1.ID = dbo.CompanyFeesPackages.DiscretInvestmentPolicy_ID LEFT OUTER JOIN " +
                   "    dbo.FinanceTools ON dbo.CompanyFeesPackages.DealAdvisoryInvestmentPolicy_ID = dbo.FinanceTools.ID LEFT OUTER JOIN " +
                   "    dbo.FinanceServices ON dbo.CompanyFeesPackages.PackageType_ID = dbo.FinanceServices.ID LEFT OUTER JOIN " +
                   "    dbo.StockExchanges RIGHT OUTER JOIN " +
                   "    dbo.ShareTitles INNER JOIN " +
                   "    dbo.Products INNER JOIN " +
                   "    dbo.Shares ON dbo.Products.ID = dbo.Shares.ShareType INNER JOIN " +
                   "    dbo.Shares_Titles_Codes ON dbo.Shares.ID = dbo.Shares_Titles_Codes.Share_ID ON dbo.ShareTitles.ID = dbo.Shares_Titles_Codes.ShareTitles_ID INNER JOIN " +
                   "    dbo.Products_Categories ON dbo.ShareTitles.ProductType = dbo.Products_Categories.ID INNER JOIN " +
                   "    dbo.ShareCodes ON dbo.Shares_Titles_Codes.ShareCodes_ID = dbo.ShareCodes.ID ON dbo.StockExchanges.ID = dbo.ShareCodes.StockExchange_ID ON dbo.Commands.Share_ID = dbo.ShareCodes.ID AND  " +
                   "      CONVERT(varchar(10), CONVERT(datetime, dbo.Commands.AktionDate, 120), 120) >= CONVERT(varchar(10), CONVERT(datetime, dbo.Shares_Titles_Codes.DateFrom, 120), 120) LEFT OUTER JOIN " +
                   "    dbo.ServiceProviders AS ServiceProviders_1 ON dbo.Commands.StockCompany_ID = ServiceProviders_1.ID LEFT OUTER JOIN " +
                   "    dbo.RecieveMethods ON dbo.Commands.RecieveMethod_ID = dbo.RecieveMethods.ID LEFT OUTER JOIN " +
                   "    dbo.InformationMethods ON dbo.Commands.InformationMethod_ID = dbo.InformationMethods.ID LEFT OUTER JOIN " +
                   "    dbo.Keys AS Keys_1 ON dbo.Contracts_Details.User1_ID = Keys_1.ID ";

            //--- define Filter --------------------------------------------------------------------
            sFilter = "Commands.CommandType_ID = " + iCommandType_ID + " AND " +
                      " CONVERT(varchar(10), CONVERT(datetime, Commands.AktionDate, 120), 120) >= CONVERT(varchar(10), CONVERT(datetime, '" + ucDC.DateFrom.ToString("yyyy/MM/dd") + "', 120), 120) AND " +
                      " CONVERT(varchar(10), CONVERT(datetime, Commands.AktionDate, 120), 120) <= CONVERT(varchar(10), CONVERT(datetime, '" + ucDC.DateTo.ToString("yyyy/MM/dd") + "', 120), 120) AND " +
                      " CONVERT(varchar(10), CONVERT(datetime, dbo.Shares_Titles_Codes.DateFrom, 120), 120) <= CONVERT(varchar(10), CONVERT(datetime, dbo.Commands.AktionDate, 120), 120) AND " +
                      " CONVERT(varchar(10), CONVERT(datetime, dbo.Shares_Titles_Codes.DateTo, 120), 120) >= CONVERT(varchar(10), CONVERT(datetime, dbo.Commands.AktionDate, 120), 120)  ";

            if (Convert.ToInt32(cmbProviders.SelectedValue) != 0) sFilter = sFilter + " AND Commands.StockCompany_ID = " + cmbProviders.SelectedValue;
            if (Convert.ToInt32(cmbUsers.SelectedValue) != 0) sFilter = sFilter + " AND Commands.[User_ID] = " + cmbUsers.SelectedValue;
            if (Convert.ToInt32(cmbAdvisors.SelectedValue) != 0) sFilter = sFilter + " AND dbo.Contracts_Details.User1_ID = " + cmbAdvisors.SelectedValue;
            if (Convert.ToInt32(cmbDiax.SelectedValue) != 0) sFilter = sFilter + " AND dbo.Contracts_Details.User4_ID = " + cmbDiax.SelectedValue;

            if (Convert.ToInt32(cmbSent.SelectedIndex) == 1) sFilter = sFilter + " AND (CONVERT(varchar(10), CONVERT(datetime, Commands.SentDate, 120), 120) > CONVERT(varchar(10), CONVERT(datetime, '1900/01/01', 120), 120))";
            if (Convert.ToInt32(cmbSent.SelectedIndex) == 2) sFilter = sFilter + " AND (CONVERT(varchar(10), CONVERT(datetime, Commands.SentDate, 120), 120) = CONVERT(varchar(10), CONVERT(datetime, '1900/01/01', 120), 120))";

            if (Convert.ToInt32(cmbActions.SelectedIndex) == 1) sFilter = sFilter + " AND Commands.RealAmount > 0 "; //AND (CONVERT(varchar(10), CONVERT(datetime, Commands.ExecuteDate, 120), 120) > CONVERT(varchar(10), CONVERT(datetime, '1900/01/01', 120), 120))";
            if (Convert.ToInt32(cmbActions.SelectedIndex) == 2) sFilter = sFilter + " AND Commands.RealAmount = 0 "; //AND (CONVERT(varchar(10), CONVERT(datetime, Commands.ExecuteDate, 120), 120) = CONVERT(varchar(10), CONVERT(datetime, '1900/01/01', 120), 120))";

            if (Convert.ToInt32(cmbChecked.SelectedIndex) == 1) sFilter = sFilter + " AND Commands.SendCheck = 1";
            if (Convert.ToInt32(cmbChecked.SelectedIndex) == 2) sFilter = sFilter + " AND Commands.SendCheck = 0";

            if (Convert.ToInt32(cmbDivisions.SelectedValue) != 0) sFilter = sFilter + " AND Clients.Division = " + cmbDivisions.SelectedValue;
            if (Convert.ToInt32(cmbProducts.SelectedValue) != 0) sFilter = sFilter + " AND Commands.Product_ID = " + cmbProducts.SelectedValue;
            if (Convert.ToInt32(cmbServices.SelectedValue) != 0) sFilter = sFilter + " AND CompanyFeesPackages.PackageType_ID = " + cmbServices.SelectedValue;

            if (sCode != "") sFilter = sFilter + " AND Commands.Code = '" + sCode + "'";
            if (iShare_ID != 0) sFilter = sFilter + " AND Commands.Share_ID = " + iShare_ID;
            if (cmbCurrency2.Text != "") sFilter = sFilter + " AND Commands.Curr = '" + cmbCurrency2.Text + "'";
            if (!chkShowCancelled.Checked) sFilter = sFilter + " AND Commands.[Status] >= 0";

            if (fgSelectedContracts.Rows.Count == 1) sSelectedContracts = "";
            else
            {
                sSelectedContracts = "(";
                for (i = 1; i <= fgSelectedContracts.Rows.Count - 1; i++)
                {
                    if (Convert.ToInt32(fgSelectedContracts[i, "ID"]) != 0)
                        sSelectedContracts = sSelectedContracts + " OR Commands.ClientPackage_ID = " + fgSelectedContracts[i, "ID"];
                }
                sSelectedContracts = sSelectedContracts.Replace("( OR", "( ") + " )";
            }
            if (sSelectedContracts.Length > 0) sFilter = sFilter + " AND " + sSelectedContracts;

            if (fgSelectedProducts.Rows.Count == 1) sSelectedProducts = "";
            else
            {
                sSelectedProducts = "(";
                for (i = 1; i <= fgSelectedProducts.Rows.Count - 1; i++)
                {
                    if (Convert.ToInt32(fgSelectedProducts[i, "ID"]) != 0)
                        sSelectedProducts = sSelectedProducts + " OR Commands.Share_ID = " + fgSelectedProducts[i, "ID"];
                }
                sSelectedProducts = sSelectedProducts.Replace("( OR", "( ") + " )";
            }
            if (sSelectedProducts.Length > 0) sFilter = sFilter + " AND " + sSelectedProducts;


            i = 0;
            fgList.Redraw = false;
            fgList.Rows.Count = 2;
            fgList.Cols[27].AllowMerging = true;
            rng = fgList.GetCellRange(0, 27, 1, 27);
            rng.Data = "Επίσημη Ενημέρωση";


            switch (iCommandType_ID)
            {
                case 1:
                    fgList.Cols[2].Width = 50;
                    fgList.Cols[3].Visible = true;
                    fgList.Cols[3].Width = 80;
                    fgList.Cols[4].Visible = true;
                    fgList.Cols[4].Width = 180;
                    fgList.Cols[5].Visible = true;
                    fgList.Cols[6].Visible = true;
                    fgList.Cols[7].Visible = true;
                    fgList.Cols[26].Visible = true;

                    using (SqlConnection con = new SqlConnection(Global.connStr))
                    {
                        sqlQuery = sSQL + " WHERE " + sFilter + " ORDER BY Commands.ID";
                        cmd = new SqlCommand(sqlQuery, con);

                        con.Open();
                        drList = cmd.ExecuteReader();

                        while (drList.Read())
                        {
                            if (iOld_ID != Convert.ToInt32(drList["ID"]))
                            {
                                iOld_ID = Convert.ToInt32(drList["ID"]);
                                bFilter = true;

                                if (rbA.Checked && Convert.ToInt32(drList["Aktion"]) == 2) bFilter = false;
                                if (rbP.Checked && Convert.ToInt32(drList["Aktion"]) == 1) bFilter = false;

                                if (txtPriceFrom.Text != "" || txtPriceTo.Text != "")
                                {
                                    if (!Global.IsNumeric(txtPriceFrom.Text)) txtPriceFrom.Text = "0";
                                    if (!Global.IsNumeric(txtPriceTo.Text)) txtPriceTo.Text = "0";

                                    if (Convert.ToInt32(cmbActions.SelectedIndex) == 1)                                             // ektelesmenes praxis
                                        if ((Convert.ToDecimal(drList["RealPrice"]) < Convert.ToDecimal(txtPriceFrom.Text) ||
                                             Convert.ToDecimal(drList["RealPrice"]) > Convert.ToDecimal(txtPriceTo.Text))) bFilter = false;
                                        else
                                            if ((Convert.ToDecimal(drList["Price"]) < Convert.ToDecimal(txtPriceFrom.Text) ||
                                                 Convert.ToDecimal(drList["Price"]) > Convert.ToDecimal(txtPriceTo.Text))) bFilter = false;
                                }

                                if ((drList["BulkCommand"] + "") != "" && Convert.ToDateTime(drList["RecieveDate"]) == Convert.ToDateTime("1900/01/01")) bFilter = false;

                                if (bFilter)
                                {
                                    sgTemp2 = Convert.ToSingle(drList["RealAmount"]);
                                    if ((drList["Curr"] + "") != "EUR")
                                    {
                                        sgTemp1 = Convert.ToSingle(drList["CurrRate"]);                                        // Amount EUR 
                                        if (sgTemp1 != 0) sgTemp2 = Convert.ToSingle(drList["RealAmount"]) / sgTemp1;
                                    }

                                    sBulkCommand = (drList["BulkCommand"] + "").Replace("<", "").Replace(">", "");
                                    sBulkCommand = (sBulkCommand == "0" ? "" : sBulkCommand);

                                    i = i + 1;
                                    fgList.AddItem(((drList["Check_FileName"] + "") == "" ? "0" : "1") + "\t" + i + "\t" + sBulkCommand + "\t" + drList["Surname"] + "\t" + drList["ContractTitle"] + "\t" +
                                                   drList["StockCompanyTitle"] + "\t" + drList["Code"] + "\t" + drList["ProfitCenter"] + "\t" +
                                                   (Convert.ToInt32(drList["Aktion"]) == 1 ? "BUY" : "SELL") + "\t" + drList["Product_Title"] + "/" + drList["Product_Category"] + "\t" +
                                                   drList["Share_Title"] + "\t" + drList["Share_Code"] + "\t" + drList["Share_ISIN"] + "\t" +
                                                         Global.ShowPrices(Convert.ToInt16(drList["Type"]), Convert.ToSingle(drList["Price"])) + "\t" +
                                                         (Convert.ToDecimal(drList["Quantity"]) == 0 ? "" : string.Format("{0:#,0.00}", drList["Quantity"])) + "\t" +
                                                         (Convert.ToDecimal(drList["Amount"]) == 0 ? "" : string.Format("{0:#,0.00}", drList["Amount"])) + "\t" +
                                                         (Convert.ToDecimal(drList["RealPrice"]) == 0 ? "" : string.Format("{0:#,0.00##}", drList["RealPrice"])) + "\t" +
                                                         (Convert.ToDecimal(drList["RealQuantity"]) == 0 ? "" : string.Format("{0:#,0.00}", drList["RealQuantity"])) + "\t" +
                                                         (Convert.ToDecimal(drList["RealAmount"]) == 0 ? "" : string.Format("{0:#,0.00}", drList["RealAmount"])) + "\t" + drList["Curr"] + "\t" +
                                                         sConstant[Convert.ToInt16(drList["Constant"])].Trim() + " " + drList["ConstantDate"] + "\t" + 
                                                         drList["StockExchange_MIC"] + "\t" + drList["ExecutionStockExchange_MIC"] + "\t" +
                                                         ((Convert.ToDateTime(drList["RecieveDate"]) != Convert.ToDateTime("31/12/2070")) ? Convert.ToDateTime(drList["RecieveDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                                         ((Convert.ToDateTime(drList["SentDate"]) != Convert.ToDateTime("01/01/1900")) ? Convert.ToDateTime(drList["SentDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                                         ((Convert.ToDateTime(drList["ExecuteDate"]) != Convert.ToDateTime("01/01/1900")) ? Convert.ToDateTime(drList["ExecuteDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                                         drList["RecieveTitle"] + "\t" + drList["OfficialInformingDate"] + "\t" + drList["Notes"] + "\t" +
                                                         (drList["Author_Surname"] + " " + drList["Author_Firstname"]).Trim() + "\t" + (drList["Advisor_Surname"] + " " + drList["Advisor_Firstname"]).Trim() + "\t" +
                                                         (drList["Diax_Surname"] + " " + drList["Diax_Firstname"]).Trim() + "\t" + drList["ServiceTitle"] + "\t" + drList["InvestPolicy_Title"] + "\t" +
                                                         drList["InvestProfile_Title"] + "\t" + drList["II_ID"] + "\t" + sRisks[Convert.ToInt32(drList["Risk"])] + "\t" +
                                                         sMiFID[Convert.ToInt32(drList["MiFIDCategory_ID"])] + "\t" + drList["StockExchange_Title"] + "\t" +
                                                         (Convert.ToInt32(drList["HFIC_Recom"]) == 1 ? "Ναί" : "Όχι") + "\t" + drList["FeesPercent"] + "\t" + drList["FeesAmount"] + "\t" +
                                                         drList["FeesDiscountPercent"] + "\t" + drList["FeesDiscountAmount"] + "\t" + drList["FinishFeesPercent"] + "\t" + drList["FinishFeesAmount"] + "\t" +
                                                         drList["ProviderFees"] + "\t" + drList["ID"] + "\t" + drList["Client_ID"] + "\t" + drList["StockCompany_ID"] + "\t" + drList["Status"] + "\t" +
                                                         ((Convert.ToInt32(drList["Parent_ID"]) == 0) ? drList["ID"] : drList["Parent_ID"]) + "\t" + "0" + "\t" + drList["Share_ID"] + "\t" +
                                                         drList["ClientPackage_ID"] + "\t" + drList["Contract_Details_ID"] + "\t" + drList["Contract_Packages_ID"] + "\t" + drList["Check_FileName"] + "\t" +
                                                         drList["BusinessType_ID"] + "\t" + drList["Product_ID"] + "\t" + drList["ProductCategory_ID"] + "\t" +
                                                         drList["SendCheck"] + "\t" + drList["Executor_Title"] + "\t" + drList["ValueDate"] + "\t" + drList["AccruedInterest"] + "\t" +
                                                         drList["FeesMisc"] + "\t" + drList["Depository_Title"] + "\t" + drList["QuantityMin"] + "\t" + drList["QuantityStep"] + "\t" +
                                                         drList["Tipos"] + "\t" + drList["CurrRate"] + "\t" + sgTemp2 + "\t" + drList["FIX_A"]);
                                }                               
                            }
                        }
                    }
                    fgList.Sort(SortFlags.Descending, 1);     // 1- AA
                    fgList.Redraw = true;
                    if (fgList.Rows.Count > 2) fgList.Row = 2;
                    fgList.Focus();

                    this.Cursor = Cursors.Default;

                    break;
                case 2:
                    fgList.Cols[2].Width = 50;
                    fgList.Cols[3].Visible = true;
                    fgList.Cols[3].Width = 160;
                    fgList.Cols[4].Visible = false;
                    fgList.Cols[4].Width = 100;
                    fgList.Cols[5].Visible = true;
                    fgList.Cols[6].Visible = true;
                    fgList.Cols[7].Visible = true;
                    fgList.Cols[26].Visible = false;

                    using (SqlConnection con = new SqlConnection(Global.connStr))
                    {
                        sqlQuery = sSQL + " WHERE " + sFilter + " ORDER BY Commands.ID";
                        cmd = new SqlCommand(sqlQuery, con);

                        con.Open();
                        drList = cmd.ExecuteReader();

                        while (drList.Read())
                        {
                            if (iOld_ID != Convert.ToInt32(drList["ID"]))
                            {
                                iOld_ID = Convert.ToInt32(drList["ID"]);
                                bFilter = true;

                                if (rbA.Checked && Convert.ToInt32(drList["Aktion"]) == 2) bFilter = false;
                                if (rbP.Checked && Convert.ToInt32(drList["Aktion"]) == 1) bFilter = false;

                                if (txtPriceFrom.Text != "" || txtPriceTo.Text != "")
                                {
                                    if (!Global.IsNumeric(txtPriceFrom.Text)) txtPriceFrom.Text = "0";
                                    if (!Global.IsNumeric(txtPriceTo.Text)) txtPriceTo.Text = "0";

                                    if (Convert.ToInt32(cmbActions.SelectedIndex) == 1)                                             // ektelesmenes praxis
                                        if ((Convert.ToDecimal(drList["RealPrice"]) < Convert.ToDecimal(txtPriceFrom.Text) ||
                                             Convert.ToDecimal(drList["RealPrice"]) > Convert.ToDecimal(txtPriceTo.Text))) bFilter = false;
                                        else
                                            if ((Convert.ToDecimal(drList["Price"]) < Convert.ToDecimal(txtPriceFrom.Text) ||
                                                 Convert.ToDecimal(drList["Price"]) > Convert.ToDecimal(txtPriceTo.Text))) bFilter = false;
                                }

                                if ((drList["BulkCommand"] + "") != "" && Convert.ToDateTime(drList["RecieveDate"]) == Convert.ToDateTime("1900/01/01")) bFilter = false;

                                if (bFilter)
                                {
                                    sgTemp2 = Convert.ToSingle(drList["RealAmount"]);
                                    if ((drList["Curr"] + "") != "EUR")
                                    {
                                        sgTemp1 = Convert.ToSingle(drList["CurrRate"]);                                        // Amount EUR 
                                        if (sgTemp1 != 0) sgTemp2 = Convert.ToSingle(drList["RealAmount"]) / sgTemp1;
                                    }

                                    sBulkCommand = (drList["BulkCommand"] + "").Replace("<", "").Replace(">", "");
                                    sBulkCommand = (sBulkCommand == "0" ? "" : sBulkCommand);

                                    i = i + 1;
                                    fgList.AddItem(((drList["Check_FileName"] + "") == "" ? "0" : "1") + "\t" + i + "\t" + sBulkCommand + "\t" + drList["Company_Title"] + "\t" + 
                                                   drList["ContractTitle"] + "\t" + drList["StockCompanyTitle"] + "\t" + drList["Code"] + "\t" + drList["ProfitCenter"] + "\t" +
                                                   (Convert.ToInt32(drList["Aktion"]) == 1 ? "BUY" : "SELL") + "\t" + drList["Product_Title"] + "/" + drList["Product_Category"] + "\t" +
                                                   drList["Share_Title"] + "\t" + drList["Share_Code"] + "\t" + drList["Share_ISIN"] + "\t" +
                                                         Global.ShowPrices(Convert.ToInt16(drList["Type"]), Convert.ToSingle(drList["Price"])) + "\t" +
                                                         (Convert.ToDecimal(drList["Quantity"]) == 0 ? "" : string.Format("{0:#,0.00}", drList["Quantity"])) + "\t" +
                                                         (Convert.ToDecimal(drList["Amount"]) == 0 ? "" : string.Format("{0:#,0.00}", drList["Amount"])) + "\t" +
                                                         (Convert.ToDecimal(drList["RealPrice"]) == 0 ? "" : string.Format("{0:#,0.00##}", drList["RealPrice"])) + "\t" +
                                                         (Convert.ToDecimal(drList["RealQuantity"]) == 0 ? "" : string.Format("{0:#,0.00}", drList["RealQuantity"])) + "\t" +
                                                         (Convert.ToDecimal(drList["RealAmount"]) == 0 ? "" : string.Format("{0:#,0.00}", drList["RealAmount"])) + "\t" + drList["Curr"] + "\t" +
                                                         sConstant[Convert.ToInt16(drList["Constant"])].Trim() + " " + drList["ConstantDate"] + "\t" +
                                                         drList["StockExchange_MIC"] + "\t" + drList["ExecutionStockExchange_MIC"] + "\t" +
                                                         ((Convert.ToDateTime(drList["RecieveDate"]) != Convert.ToDateTime("31/12/2070")) ? Convert.ToDateTime(drList["RecieveDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                                         ((Convert.ToDateTime(drList["SentDate"]) != Convert.ToDateTime("01/01/1900")) ? Convert.ToDateTime(drList["SentDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                                         ((Convert.ToDateTime(drList["ExecuteDate"]) != Convert.ToDateTime("01/01/1900")) ? Convert.ToDateTime(drList["ExecuteDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                                         drList["RecieveTitle"] + "\t" + drList["OfficialInformingDate"] + "\t" + drList["Notes"] + "\t" +
                                                         (drList["Author_Surname"] + " " + drList["Author_Firstname"]).Trim() + "\t" + (drList["Advisor_Surname"] + " " + drList["Advisor_Firstname"]).Trim() + "\t" +
                                                         (drList["Diax_Surname"] + " " + drList["Diax_Firstname"]).Trim() + "\t" + drList["ServiceTitle"] + "\t" + drList["InvestPolicy_Title"] + "\t" +
                                                         drList["InvestProfile_Title"] + "\t" + drList["II_ID"] + "\t" + sRisks[Convert.ToInt32(0)] + "\t" +
                                                         sMiFID[Convert.ToInt32(0)] + "\t" + drList["StockExchange_Title"] + "\t" +
                                                         (Convert.ToInt32(drList["HFIC_Recom"]) == 1 ? "Ναί" : "Όχι") + "\t" + drList["FeesPercent"] + "\t" + drList["FeesAmount"] + "\t" +
                                                         drList["FeesDiscountPercent"] + "\t" + drList["FeesDiscountAmount"] + "\t" + drList["FinishFeesPercent"] + "\t" + drList["FinishFeesAmount"] + "\t" +
                                                         drList["ProviderFees"] + "\t" + drList["ID"] + "\t" + drList["Client_ID"] + "\t" + drList["StockCompany_ID"] + "\t" + drList["Status"] + "\t" +
                                                         ((Convert.ToInt32(drList["Parent_ID"]) == 0) ? drList["ID"] : drList["Parent_ID"]) + "\t" + "0" + "\t" + drList["Share_ID"] + "\t" +
                                                         drList["ClientPackage_ID"] + "\t" + drList["Contract_Details_ID"] + "\t" + drList["Contract_Packages_ID"] + "\t" + drList["Check_FileName"] + "\t" +
                                                         drList["BusinessType_ID"] + "\t" + drList["Product_ID"] + "\t" + drList["ProductCategory_ID"] + "\t" +
                                                         drList["SendCheck"] + "\t" + drList["Executor_Title"] + "\t" + drList["ValueDate"] + "\t" + drList["AccruedInterest"] + "\t" +
                                                         drList["FeesMisc"] + "\t" + drList["Depository_Title"] + "\t" + drList["QuantityMin"] + "\t" + drList["QuantityStep"] + "\t" +
                                                         drList["Tipos"] + "\t" + drList["CurrRate"] + "\t" + sgTemp2 + "\t" + drList["FIX_A"]);
                                }                               
                            }
                        }
                    }
                    fgList.Sort(SortFlags.Descending, 1);     // 1- AA
                    fgList.Redraw = true;
                    if (fgList.Rows.Count > 2) fgList.Row = 2;
                    fgList.Focus();

                    this.Cursor = Cursors.Default;
                    break;
                case 3:
                    fgList.Cols[2].Width = 50;
                    fgList.Cols[3].Visible = true;
                    fgList.Cols[3].Width = 160;
                    fgList.Cols[4].Visible = false;
                    fgList.Cols[4].Width = 100;
                    fgList.Cols[5].Visible = true;
                    fgList.Cols[6].Visible = true;
                    fgList.Cols[7].Visible = true;
                    fgList.Cols[26].Visible = false;

                    using (SqlConnection con = new SqlConnection(Global.connStr))
                    {
                        sqlQuery = sSQL + " WHERE " + sFilter + " ORDER BY Commands.ID";
                        cmd = new SqlCommand(sqlQuery, con);

                        con.Open();
                        drList = cmd.ExecuteReader();

                        while (drList.Read())
                        {
                            if (iOld_ID != Convert.ToInt32(drList["ID"]))
                            {
                                iOld_ID = Convert.ToInt32(drList["ID"]);
                                bFilter = true;

                                if (rbA.Checked && Convert.ToInt32(drList["Aktion"]) == 2) bFilter = false;
                                if (rbP.Checked && Convert.ToInt32(drList["Aktion"]) == 1) bFilter = false;

                                if (txtPriceFrom.Text != "" || txtPriceTo.Text != "")
                                {
                                    if (!Global.IsNumeric(txtPriceFrom.Text)) txtPriceFrom.Text = "0";
                                    if (!Global.IsNumeric(txtPriceTo.Text)) txtPriceTo.Text = "0";

                                    if (Convert.ToInt32(cmbActions.SelectedIndex) == 1)                                             // ektelesmenes praxis
                                        if ((Convert.ToDecimal(drList["RealPrice"]) < Convert.ToDecimal(txtPriceFrom.Text) ||
                                             Convert.ToDecimal(drList["RealPrice"]) > Convert.ToDecimal(txtPriceTo.Text))) bFilter = false;
                                        else
                                            if ((Convert.ToDecimal(drList["Price"]) < Convert.ToDecimal(txtPriceFrom.Text) ||
                                                 Convert.ToDecimal(drList["Price"]) > Convert.ToDecimal(txtPriceTo.Text))) bFilter = false;
                                }

                                if ((drList["BulkCommand"] + "") != "" && Convert.ToDateTime(drList["RecieveDate"]) == Convert.ToDateTime("1900/01/01")) bFilter = false;

                                if (bFilter)
                                {
                                    sgTemp2 = Convert.ToSingle(drList["RealAmount"]);
                                    if ((drList["Curr"] + "") != "EUR")
                                    {
                                        sgTemp1 = Convert.ToSingle(drList["CurrRate"]);                                        // Amount EUR 
                                        if (sgTemp1 != 0) sgTemp2 = Convert.ToSingle(drList["RealAmount"]) / sgTemp1;
                                    }

                                    sBulkCommand = (drList["BulkCommand"] + "").Replace("<", "").Replace(">", "");
                                    sBulkCommand = (sBulkCommand == "0" ? "" : sBulkCommand);

                                    i = i + 1;
                                    fgList.AddItem(((drList["Check_FileName"] + "") == "" ? "0" : "1") + "\t" + i + "\t" + sBulkCommand + "\t" + drList["Surname"] + "\t" + drList["ContractTitle"] + "\t" +
                                                   drList["StockCompanyTitle"] + "\t" + drList["Code"] + "\t" + drList["ProfitCenter"] + "\t" +
                                                   (Convert.ToInt32(drList["Aktion"]) == 1 ? "BUY" : "SELL") + "\t" + drList["Product_Title"] + "/" + drList["Product_Category"] + "\t" +
                                                   drList["Share_Title"] + "\t" + drList["Share_Code"] + "\t" + drList["Share_ISIN"] + "\t" +
                                                         Global.ShowPrices(Convert.ToInt16(drList["Type"]), Convert.ToSingle(drList["Price"])) + "\t" +
                                                         (Convert.ToDecimal(drList["Quantity"]) == 0 ? "" : string.Format("{0:#,0.00}", drList["Quantity"])) + "\t" +
                                                         (Convert.ToDecimal(drList["Amount"]) == 0 ? "" : string.Format("{0:#,0.00}", drList["Amount"])) + "\t" +
                                                         (Convert.ToDecimal(drList["RealPrice"]) == 0 ? "" : string.Format("{0:#,0.00##}", drList["RealPrice"])) + "\t" +
                                                         (Convert.ToDecimal(drList["RealQuantity"]) == 0 ? "" : string.Format("{0:#,0.00}", drList["RealQuantity"])) + "\t" +
                                                         (Convert.ToDecimal(drList["RealAmount"]) == 0 ? "" : string.Format("{0:#,0.00}", drList["RealAmount"])) + "\t" + drList["Curr"] + "\t" +
                                                         sConstant[Convert.ToInt16(drList["Constant"])].Trim() + " " + drList["ConstantDate"] + "\t" +
                                                         drList["StockExchange_MIC"] + "\t" + drList["ExecutionStockExchange_MIC"] + "\t" +
                                                         ((Convert.ToDateTime(drList["RecieveDate"]) != Convert.ToDateTime("31/12/2070")) ? Convert.ToDateTime(drList["RecieveDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                                         ((Convert.ToDateTime(drList["SentDate"]) != Convert.ToDateTime("01/01/1900")) ? Convert.ToDateTime(drList["SentDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                                         ((Convert.ToDateTime(drList["ExecuteDate"]) != Convert.ToDateTime("01/01/1900")) ? Convert.ToDateTime(drList["ExecuteDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                                         drList["RecieveTitle"] + "\t" + drList["OfficialInformingDate"] + "\t" + drList["Notes"] + "\t" +
                                                         (drList["Author_Surname"] + " " + drList["Author_Firstname"]).Trim() + "\t" + (drList["Advisor_Surname"] + " " + drList["Advisor_Firstname"]).Trim() + "\t" +
                                                         (drList["Diax_Surname"] + " " + drList["Diax_Firstname"]).Trim() + "\t" + drList["ServiceTitle"] + "\t" + drList["InvestPolicy_Title"] + "\t" +
                                                         drList["InvestProfile_Title"] + "\t" + drList["II_ID"] + "\t" + sRisks[Convert.ToInt32(0)] + "\t" +
                                                         sMiFID[Convert.ToInt32(0)] + "\t" + drList["StockExchange_Title"] + "\t" +
                                                         (Convert.ToInt32(drList["HFIC_Recom"]) == 1 ? "Ναί" : "Όχι") + "\t" + drList["FeesPercent"] + "\t" + drList["FeesAmount"] + "\t" +
                                                         drList["FeesDiscountPercent"] + "\t" + drList["FeesDiscountAmount"] + "\t" + drList["FinishFeesPercent"] + "\t" + drList["FinishFeesAmount"] + "\t" +
                                                         drList["ProviderFees"] + "\t" + drList["ID"] + "\t" + drList["Client_ID"] + "\t" + drList["StockCompany_ID"] + "\t" + drList["Status"] + "\t" +
                                                         ((Convert.ToInt32(drList["Parent_ID"]) == 0) ? drList["ID"] : drList["Parent_ID"]) + "\t" + "0" + "\t" + drList["Share_ID"] + "\t" +
                                                         drList["ClientPackage_ID"] + "\t" + drList["Contract_Details_ID"] + "\t" + drList["Contract_Packages_ID"] + "\t" + drList["Check_FileName"] + "\t" +
                                                         drList["BusinessType_ID"] + "\t" + drList["Product_ID"] + "\t" + drList["ProductCategory_ID"] + "\t" +
                                                         drList["SendCheck"] + "\t" + drList["Executor_Title"] + "\t" + drList["ValueDate"] + "\t" + drList["AccruedInterest"] + "\t" +
                                                         drList["FeesMisc"] + "\t" + drList["Depository_Title"] + "\t" + drList["QuantityMin"] + "\t" + drList["QuantityStep"] + "\t" +
                                                         drList["Tipos"] + "\t" + drList["CurrRate"] + "\t" + sgTemp2 + "\t" + drList["FIX_A"]);
                                }
                            }
                        }
                    }
                    fgList.Sort(SortFlags.Descending, 1);     // 1- AA
                    fgList.Redraw = true;
                    if (fgList.Rows.Count > 2) fgList.Row = 2;
                    fgList.Focus();

                    this.Cursor = Cursors.Default;
                    break;
                case 4:
                    fgList.Cols[2].Width = 90;
                    fgList.Cols[3].Visible = true;
                    fgList.Cols[3].Width = 130;
                    fgList.Cols[4].Visible = false;
                    fgList.Cols[4].Width = 130;
                    fgList.Cols[6].Visible = false;
                    fgList.Cols[27].AllowMerging = true;
                    rng = fgList.GetCellRange(0, 27, 1, 27);
                    rng.Data = "Allocation";

                    using (SqlConnection con = new SqlConnection(Global.connStr))
                    {
                        sqlQuery = sSQL + " WHERE " + sFilter + " ORDER BY Commands.ID";
                        cmd = new SqlCommand(sqlQuery, con);

                        con.Open();
                        drList = cmd.ExecuteReader();

                        while (drList.Read())
                        {
                            if (iOld_ID != Convert.ToInt32(drList["ID"]))
                            {
                                iOld_ID = Convert.ToInt32(drList["ID"]);
                                bFilter = true;

                                if (rbA.Checked && Convert.ToInt32(drList["Aktion"]) == 2) bFilter = false;
                                if (rbP.Checked && Convert.ToInt32(drList["Aktion"]) == 1) bFilter = false;

                                if (txtPriceFrom.Text != "" || txtPriceTo.Text != "")
                                {
                                    if (!Global.IsNumeric(txtPriceFrom.Text)) txtPriceFrom.Text = "0";
                                    if (!Global.IsNumeric(txtPriceTo.Text)) txtPriceTo.Text = "0";

                                    if (Convert.ToInt32(cmbActions.SelectedIndex) == 1)                                             // ektelesmenes praxis
                                        if ((Convert.ToDecimal(drList["RealPrice"]) < Convert.ToDecimal(txtPriceFrom.Text) ||
                                             Convert.ToDecimal(drList["RealPrice"]) > Convert.ToDecimal(txtPriceTo.Text))) bFilter = false;
                                        else
                                            if ((Convert.ToDecimal(drList["Price"]) < Convert.ToDecimal(txtPriceFrom.Text) ||
                                                 Convert.ToDecimal(drList["Price"]) > Convert.ToDecimal(txtPriceTo.Text))) bFilter = false;
                                }

                                if ((drList["BulkCommand"] + "") != "" && Convert.ToDateTime(drList["RecieveDate"]) == Convert.ToDateTime("1900/01/01")) bFilter = false;

                                if (bFilter)
                                {
                                    sgTemp2 = Convert.ToSingle(drList["RealAmount"]);
                                    if ((drList["Curr"] + "") != "EUR")
                                    {
                                        sgTemp1 = Convert.ToSingle(drList["CurrRate"]);                                        // Amount EUR 
                                        if (sgTemp1 != 0) sgTemp2 = Convert.ToSingle(drList["RealAmount"]) / sgTemp1;
                                    }

                                    sBulkCommand = (drList["BulkCommand"] + "").Replace("<", "").Replace(">", "");
                                    sBulkCommand = (sBulkCommand == "0" ? "" : sBulkCommand);

                                    i = i + 1;
                                    fgList.AddItem(((drList["Check_FileName"] + "") == "" ? "0" : "1") + "\t" + i + "\t" + sBulkCommand + "\t" + (drList["Author_Surname"] + " " + drList["Author_Firstname"]).Trim() + "\t" + 
                                                   drList["ContractTitle"] + "\t" + drList["StockCompanyTitle"] + "\t" + drList["Code"] + "\t" + drList["ProfitCenter"] + "\t" +
                                                   (Convert.ToInt32(drList["Aktion"]) == 1 ? "BUY" : "SELL") + "\t" + drList["Product_Title"] + "/" + drList["Product_Category"] + "\t" +
                                                   drList["Share_Title"] + "\t" + drList["Share_Code"] + "\t" + drList["Share_ISIN"] + "\t" +
                                                         Global.ShowPrices(Convert.ToInt16(drList["Type"]), Convert.ToSingle(drList["Price"])) + "\t" +
                                                         (Convert.ToDecimal(drList["Quantity"]) == 0 ? "" : string.Format("{0:#,0.00}", drList["Quantity"])) + "\t" +
                                                         (Convert.ToDecimal(drList["Amount"]) == 0 ? "" : string.Format("{0:#,0.00}", drList["Amount"])) + "\t" +
                                                         (Convert.ToDecimal(drList["RealPrice"]) == 0 ? "" : string.Format("{0:#,0.00##}", drList["RealPrice"])) + "\t" +
                                                         (Convert.ToDecimal(drList["RealQuantity"]) == 0 ? "" : string.Format("{0:#,0.00}", drList["RealQuantity"])) + "\t" +
                                                         (Convert.ToDecimal(drList["RealAmount"]) == 0 ? "" : string.Format("{0:#,0.00}", drList["RealAmount"])) + "\t" + drList["Curr"] + "\t" +
                                                         sConstant[Convert.ToInt16(drList["Constant"])].Trim() + " " + drList["ConstantDate"] + "\t" +
                                                         drList["StockExchange_MIC"] + "\t" + drList["ExecutionStockExchange_MIC"] + "\t" +
                                                         ((Convert.ToDateTime(drList["RecieveDate"]) != Convert.ToDateTime("31/12/2070")) ? Convert.ToDateTime(drList["RecieveDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                                         ((Convert.ToDateTime(drList["SentDate"]) != Convert.ToDateTime("01/01/1900")) ? Convert.ToDateTime(drList["SentDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                                         ((Convert.ToDateTime(drList["ExecuteDate"]) != Convert.ToDateTime("01/01/1900")) ? Convert.ToDateTime(drList["ExecuteDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                                         drList["RecieveTitle"] + "\t" + drList["OfficialInformingDate"] + "\t" + drList["Notes"] + "\t" +
                                                         (drList["Author_Surname"] + " " + drList["Author_Firstname"]).Trim() + "\t" + (drList["Advisor_Surname"] + " " + drList["Advisor_Firstname"]).Trim() + "\t" +
                                                         (drList["Diax_Surname"] + " " + drList["Diax_Firstname"]).Trim() + "\t" + drList["ServiceTitle"] + "\t" + drList["InvestPolicy_Title"] + "\t" +
                                                         drList["InvestProfile_Title"] + "\t" + drList["II_ID"] + "\t" + sRisks[Convert.ToInt32(0)] + "\t" +
                                                         sMiFID[Convert.ToInt32(0)] + "\t" + drList["StockExchange_Title"] + "\t" +
                                                         (Convert.ToInt32(drList["HFIC_Recom"]) == 1 ? "Ναί" : "Όχι") + "\t" + drList["FeesPercent"] + "\t" + drList["FeesAmount"] + "\t" +
                                                         drList["FeesDiscountPercent"] + "\t" + drList["FeesDiscountAmount"] + "\t" + drList["FinishFeesPercent"] + "\t" + drList["FinishFeesAmount"] + "\t" +
                                                         drList["ProviderFees"] + "\t" + drList["ID"] + "\t" + drList["Client_ID"] + "\t" + drList["StockCompany_ID"] + "\t" + drList["Status"] + "\t" +
                                                         ((Convert.ToInt32(drList["Parent_ID"]) == 0) ? drList["ID"] : drList["Parent_ID"]) + "\t" + "0" + "\t" + drList["Share_ID"] + "\t" +
                                                         drList["ClientPackage_ID"] + "\t" + drList["Contract_Details_ID"] + "\t" + drList["Contract_Packages_ID"] + "\t" + drList["Check_FileName"] + "\t" +
                                                         drList["BusinessType_ID"] + "\t" + drList["Product_ID"] + "\t" + drList["ProductCategory_ID"] + "\t" +
                                                         drList["SendCheck"] + "\t" + drList["Executor_Title"] + "\t" + drList["ValueDate"] + "\t" + drList["AccruedInterest"] + "\t" +
                                                         drList["FeesMisc"] + "\t" + drList["Depository_Title"] + "\t" + drList["QuantityMin"] + "\t" + drList["QuantityStep"] + "\t" +
                                                         drList["Tipos"] + "\t" + drList["CurrRate"] + "\t" + sgTemp2 + "\t" + drList["FIX_A"]);
                                }                            
                            }
                        }
                    }
                    fgList.Sort(SortFlags.Descending, 1);     // 1- AA
                    fgList.Redraw = true;
                    if (fgList.Rows.Count > 2) fgList.Row = 2;
                    fgList.Focus();

                    this.Cursor = Cursors.Default;

                    break;
            }
            fgList.Redraw = true;
            if (fgList.Rows.Count > 2) fgList.Row = 2;
            fgList.Focus();
        }
      
        private void picEmptyClient_Click(object sender, EventArgs e)
        {
            ucCS.ShowClientsList = false;
            ucCS.txtContractTitle.Text = "";
            ucCS.ShowClientsList = true;

            fgSelectedContracts.Rows.Count = 1;
        }

        private void picEmptyProduct_Click(object sender, EventArgs e)
        {
            ucPS.ShowProductsList = false;
            ucPS.txtShareTitle.Text = "";
            ucPS.ShowProductsList = true;

            fgSelectedProducts.Rows.Count = 1;
        }
        private void lnkClean_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ucCS.ShowClientsList = false;
            ucCS.txtContractTitle.Text = "";
            ucCS.ShowClientsList = true;

            fgSelectedContracts.Rows.Count = 1;

            ucPS.ShowProductsList = false;
            ucPS.txtShareTitle.Text = "";
            ucPS.ShowProductsList = true;

            fgSelectedProducts.Rows.Count = 1;
        }
        #endregion
        #region --- menuContext functions ----------------------------------------------------
        private void mnuClientData_Click(object sender, EventArgs e)
        {
            frmClientData locClientData = new frmClientData();
            locClientData.Client_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
            locClientData.Text = Global.GetLabel("customer_information");
            locClientData.Show();
        }
        private void mnuContractData_Click(object sender, EventArgs e)
        {
            frmContract locContract = new frmContract();
            locContract.Aktion = 1;
            locContract.Contract_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_ID"]);
            locContract.Contract_Details_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_Details_ID"]);
            locContract.Contract_Packages_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_Packages_ID"]);
            locContract.Client_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
            locContract.ClientType = 1;
            locContract.ClientFullName = fgList[fgList.Row, "ClientFullName"] + "";
            locContract.RightsLevel = iRightsLevel;
            locContract.ShowDialog();
        }
        private void mnuProductData_Click(object sender, EventArgs e)
        {
            frmProductData locProductData = new frmProductData();
            locProductData.Product_ID = Convert.ToInt32(fgList[fgList.Row, "Product_ID"]);
            locProductData.ShareCode_ID = Convert.ToInt32(fgList[fgList.Row, "Share_ID"]);
            locProductData.Text = Global.GetLabel("product");
            locProductData.Show();
        }
        private void mnuNewCommand_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 0) {

                ucCS.ShowClientsList = false;
                ucCS.txtContractTitle.Text = fgList[fgList.Row, "ContractTitle"] + "";
                ucCS.ShowClientsList = true;
                lnkPelatis.Text = fgList[fgList.Row, "ClientFullName"] + "";
                lblCode.Text = fgList[fgList.Row, "Code"] + "";
                lnkPortfolio.Text = fgList[fgList.Row, "Portfolio"] + "";
                iContract_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_ID"]);
                iContract_Details_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_Details_ID"]);
                iContract_Packages_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_Packages_ID"]);

                sCode = fgList[fgList.Row, "Code"] + "";
                sPortfolio = fgList[fgList.Row, "Portfolio"] + "";
                iClient_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
                iProvider_ID = Convert.ToInt32(fgList[fgList.Row, "Provider_ID"]);
                iBusinessType_ID = Convert.ToInt32(fgList[fgList.Row, "BusinessType_ID"]);

                foundRows = Global.dtServiceProviders.Select("ID = " + iProvider_ID);
                if (foundRows.Length > 0)
                    iProviderType = Convert.ToInt32(foundRows[0]["ProviderType"]);

                DefineList();
            }
        }
        private void mnuCopyISIN_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 1) {

                try {
                    if (!Convert.IsDBNull(Clipboard.GetText())) Clipboard.SetDataObject(fgList[fgList.Row, "ISIN"], true, 10, 100);
                }
                catch (Exception) {
                }
            }
        }
        private void mnuShowFile_Click(object sender, EventArgs e)
        {

        }
        #endregion
        public int Mode { get { return iMode; } set { iMode = value; } }                                    // 1 - Dialy, 2 - Search
        public int RightsLevel { get { return iRightsLevel; } set { iRightsLevel = value; } }
        public string Extra { get { return sExtra; } set { sExtra = value; } }
    }
}
