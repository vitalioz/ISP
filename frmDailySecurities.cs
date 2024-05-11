using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using C1.Win.C1FlexGrid;
using Core;

namespace Transactions
{
    public partial class frmDailySecurities : Form
    {
        DataTable dtEURRates;
        DataView dtView;
        DataRow[] foundRows;
        int i, iID, iRow, iClient_ID, iContract_ID, iCommandType_ID, iBusinessType_ID, iProvider_ID, iShare_ID, iShareTitle_ID, iShareCode_ID, iXAA, 
            iProduct_ID, iProductCategory_ID, iStockExchange_ID, iOddEvenBlock, iStyle, iClientData_ID, iMIFIDCategory_ID,  iMIFID_2, 
            iRightsLevel, iPreClient_ID, iAdvisor_ID, iProvider_ID_Param, iDivision_Param, iDiavivasi_Param, iActions_Param, iAdvisors_Param, iDiax_Param, 
            iDiavivastis_Param, iBuySell_Param, iEnterPoint_Param, iBusinessType_Param, iCommandType_Param, iSendCheck_Param, iProductType_Param, iService_Param;
        float sgPriceFrom_Param, sgPriceTo_Param, sgTemp, sgTemp1, sgTemp2;
        string sTemp, sExtra, sFileName, sProviderTitle, sOldShare, sPreCode, sPreISIN, sPriceFrom, sInvPropNotesFlag, sDPMNotesFlag, sBulkCommand, sInvestProfile, 
               sInvestPolicy, sClientFullName, sCodes_Param, sSharesList_Param, sClientPackages_Param, sCurrency_Param, sStockExchangeList_Param, sProvider_Title_Param, 
               sDiavivasi_Title_Param, sActions_Title_Param, sAdvisors_Title_Param, sDiax_Title_Param, sDivision_Title_Param, sDiavivastis_Title_Param;
        Point position;
        bool pMove;
        string[] sStatus = { "", Global.GetLabel("fixed_assets"), Global.GetLabel("fixed_assets_until") };
        string[] sConstant = { "Day Order", "GTC", "GTDate" };
        string[] sRisks = { "", "Υψηλός", "Μεσαίος", "Χαμηλός" };
        string[] sMiFID = { "-", "Ιδιώτης Πελάτης", "Επαγγελματίας Πελάτης", "Επιλέξιμοι Αντισυμβαλλόμενοι" };
        string[] sPriceType = { "Limit", "Market", "Stop loss", "Scenario", "ATC", "ATO" };

        DateTime dTemp, dFrom_Param, dTo_Param;
        bool bCheckList, bFilter, bMultiProducts, bClientChoiceMode, bCashAccounts, bCheckShareList, bCheckSurname, bShareChoiceMode, bShowCancelled, bShowCancelled_Param;
        CellRange rng;
        CellStyle csCancel, csBuy, csSell, csGroup1, csGroup2, csChecked, csThinks, csWait;
        Hashtable htStatus = new Hashtable();
        Hashtable htFile = new Hashtable();    

        #region --- Start functions -----------------------------------------------------------------------------
        public frmDailySecurities()
        {
            InitializeComponent();

            panDPM.Left = 334;
            panDPM.Top = 32;

            panSecurities.Left = 3;
            panSecurities.Top = 102;
            panSecurities.Width = 840;
            panSecurities.Height = 52;

            panFilters.Top = 52;
            panFilters.Left = 920;

            panCommandBuffer.Left = 68;
            panCommandBuffer.Top = 172;

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
            csWait.BackColor = Color.LightSeaGreen;
        }
        private void frmDailySecurities_Load(object sender, EventArgs e)
        {
            DateTime dPoint1, dPoint2;
            dPoint1 = DateTime.Now;

            this.Text = Global.GetLabel("transactions_list");
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
            bClientChoiceMode = false;
            bCashAccounts = false;
            bMultiProducts = false;
            panDPM.Visible = false;
            btnSaveTransfer.Visible = false;
            panFilters.Visible = true;
            if (iRightsLevel == 1) tsbTransfer.Enabled = false;
            cmbDiavivasi.SelectedIndex = 0;
            cmbActions.SelectedIndex = 0;
            iBusinessType_ID = 1;                             // 1 - RTO (HF), 2 - Custody (HFSS)
            lblBusinessType_ID.Text = "1";
            iCommandType_ID = 1;                             // 1 - Simple Command, 2 - Synthetic Command
            lstType.SelectedIndex = 0;
            cmbConstant.SelectedIndex = 0;
            lblClient_ID.Text = "0";
            lblProvider_ID.Text = "0";
            cmbChecked.SelectedIndex = 0;
            iMIFIDCategory_ID = 0;
            iMIFID_2 = 0;

            for (i = 0; i < imgStatus.Images.Count; i++) htStatus.Add(i, imgStatus.Images[i]);

            ucCS.StartInit(700, 400, 200, 20, 1);
            ucCS.TextOfLabelChanged += new EventHandler(ucCS_TextOfLabelChanged);
            ucCS.Filters = "Status = 1 And Contract_ID > 0";
            ucCS.ListType = 2;

            ucPS.StartInit(700, 400, 200, 20, 1);
            ucPS.TextOfLabelChanged += new EventHandler(ucPS_TextOfLabelChanged);
            ucPS.ListType = 1;
            ucPS.Filters = "Aktive = 1 ";

            dTemp = DateTime.Now;

            //-------------- Define ServiceProviders List -----------------
            dtView = Global.dtServiceProviders.Copy().DefaultView;
            dtView.RowFilter = "Aktive = 1";
            cmbProviders.DataSource = dtView;
            cmbProviders.DisplayMember = "Title";
            cmbProviders.ValueMember = "ID";
            cmbProviders.SelectedValue = 0;

            //-------------- Define cmbRecievedMethods List ------------------
            cmbRecieveMethods.DataSource = Global.dtRecieveMethods.Copy();
            cmbRecieveMethods.DisplayMember = "Title";
            cmbRecieveMethods.ValueMember = "ID";
            cmbRecieveMethods.SelectedValue = 0;

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

            //-------------- Define Products List ------------------
            cmbProductType.DataSource = Global.dtProductTypes.Copy();
            cmbProductType.DisplayMember = "Title";
            cmbProductType.ValueMember = "ID";

            //-------------- Define Currencies List ------------------
            cmbCurrency.DataSource = Global.dtCurrencies.Copy();
            cmbCurrency.DisplayMember = "Title";
            cmbCurrency.ValueMember = "ID";

            //-------------- Define Stock Exchanges ------------------
            cmbStockExchanges.DataSource = Global.dtStockExchanges.Copy();
            cmbStockExchanges.DisplayMember = "Title";
            cmbStockExchanges.ValueMember = "ID";

            //-------------- Define ServiceProviders List ------------------
            dtView = Global.dtServiceProviders.Copy().DefaultView;
            dtView.RowFilter = "ProviderType = 0 OR ProviderType = 1 OR ProviderType = 3";
            cmbServiceProviders.DataSource = dtView;
            cmbServiceProviders.DisplayMember = "Title";
            cmbServiceProviders.ValueMember = "ID";
            cmbServiceProviders.SelectedValue = 0;

            //-------------- Define SharesCodes List ------------------
            //CashTablesInitialisation("2");     // 2 - Cash ShareCodes List

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgList.RowColChange += new EventHandler(fgList_RowColChange);
            fgList.MouseDown += new MouseEventHandler(fgList_MouseDown);
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

            fgList.Cols[21].AllowMerging = true;
            rng = fgList.GetCellRange(0, 21, 1, 21);
            rng.Data = Global.GetLabel("stock_exchange");

            fgList.Cols[22].AllowMerging = true;
            rng = fgList.GetCellRange(0, 22, 1, 22);
            rng.Data = Global.GetLabel("receipt_time");

            fgList.Cols[23].AllowMerging = true;
            rng = fgList.GetCellRange(0, 23, 1, 23);
            rng.Data = Global.GetLabel("transmission_time");

            fgList.Cols[24].AllowMerging = true;
            rng = fgList.GetCellRange(0, 24, 1, 24);
            rng.Data = Global.GetLabel("execution_date");

            fgList.Cols[25].AllowMerging = true;
            rng = fgList.GetCellRange(0, 25, 1, 25);
            rng.Data = Global.GetLabel("receipt_way");

            fgList.Cols[26].AllowMerging = true;
            rng = fgList.GetCellRange(0, 26, 1, 26);
            rng.Data = "Επίσημη Ενημέρωση";

            fgList.Cols[27].AllowMerging = true;
            rng = fgList.GetCellRange(0, 27, 1, 27);
            rng.Data = Global.GetLabel("notes");

            fgList.Cols[28].AllowMerging = true;
            rng = fgList.GetCellRange(0, 28, 1, 28);
            rng.Data = Global.GetLabel("transmitter");

            fgList.Cols[29].AllowMerging = true;
            rng = fgList.GetCellRange(0, 29, 1, 29);
            rng.Data = Global.GetLabel("advisor");

            fgList.Cols[30].AllowMerging = true;
            rng = fgList.GetCellRange(0, 30, 1, 30);
            rng.Data = "Διαχειριστής";

            fgList.Cols[31].AllowMerging = true;
            rng = fgList.GetCellRange(0, 31, 1, 31);
            rng.Data = Global.GetLabel("services");

            fgList.Cols[32].AllowMerging = true;
            rng = fgList.GetCellRange(0, 32, 1, 32);
            rng.Data = "Επενδ.πολιτική";

            fgList.Cols[33].AllowMerging = true;
            rng = fgList.GetCellRange(0, 33, 1, 33);
            rng.Data = "Επενδ.Profile";

            fgList.Cols[34].AllowMerging = true;
            rng = fgList.GetCellRange(0, 34, 1, 34);
            rng.Data = "Επενδ.πρόταση";

            fgList.Cols[35].AllowMerging = true;
            rng = fgList.GetCellRange(0, 35, 1, 35);
            rng.Data = "Κίνδυνος";

            fgList.Cols[36].AllowMerging = true;
            rng = fgList.GetCellRange(0, 36, 1, 36);
            rng.Data = "Είδος Πελάτη MiFID";

            fgList.Cols[37].AllowMerging = true;
            rng = fgList.GetCellRange(0, 37, 1, 37);
            rng.Data = "Χρηματ/ριο Εκτέλεσης Τίτλος";

            fgList.Cols[38].AllowMerging = true;
            rng = fgList.GetCellRange(0, 38, 1, 38);
            rng.Data = "Προτινόμενο απο ΕΕ";

            rng = fgList.GetCellRange(0, 39, 0, 45);
            rng.Data = Global.GetLabel("commissions");

            fgList[1, 39] = Global.GetLabel("percent");
            fgList[1, 40] = Global.GetLabel("amount");
            fgList[1, 41] = Global.GetLabel("discount_in_percent");
            fgList[1, 42] = Global.GetLabel("discount_in_amount");
            fgList[1, 43] = Global.GetLabel("final_commission_percent");
            fgList[1, 44] = "Προμήθεια μετά την έκπτωση";
            fgList[1, 45] = Global.GetLabel("final_commission");    

            fgList.Cols[69].AllowMerging = true;
            rng = fgList.GetCellRange(0, 69, 1, 69);
            rng.Data = "Rate";

            fgList.Cols[70].AllowMerging = true;
            rng = fgList.GetCellRange(0, 70, 1, 70);
            rng.Data = "Αξία σε EUR";


            fgList.Styles.Fixed.TextAlign = TextAlignEnum.CenterCenter;

            //------- fgPreOrders ----------------------------
            fgPreOrders.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgPreOrders.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgPreOrders.MouseDown += new MouseEventHandler(fgPreOrders_MouseDown);

            fgPreOrders.DrawMode = DrawModeEnum.OwnerDraw;
            fgPreOrders.ShowCellLabels = true;

            if (iRightsLevel == 1) tsbInform.Enabled = false;


            tslPreOrders.Text = "Επενδυτικές Συμβουλές: " + (fgPreOrders.Rows.Count - 1) + " " + sInvPropNotesFlag;
            tslDPMOrders.Text = "DPM Orders: " + (fgPreOrders.Rows.Count - 1) + " " + sDPMNotesFlag;

            bCheckList = true;
            bCheckShareList = true;

            switch (sExtra)
            {
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
            InitLists();
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
            fgList.Height = this.Height - 238;
        }
        #endregion
        #region --- Toolbar functions -----------------------------------------------------------------------------
        private void tsbTransfer_Click(object sender, EventArgs e)
        {
            frmTransfer locTransfer = new frmTransfer();
            locTransfer.DateFrom = dFrom_Param;
            locTransfer.ShowDialog();
            DefineList();
        }
        private void tsbBasket_Click(object sender, EventArgs e)
        {
            frmOrderBasket locOrderBasket = new frmOrderBasket();
            locOrderBasket.Today = dFrom_Param;
            locOrderBasket.ShowDialog();
            DefineList();
        }
        private void tsbCreatePDF_Click(object sender, EventArgs e)
        {

        }
        private void tslFX_Click(object sender, EventArgs e)
        {
            frmDailyFX locDailyFX = new frmDailyFX();
            //locDailyFX.DateFrom = dFrom_Param;
            locDailyFX.Show();
        }

        private void tsbInform_Click(object sender, EventArgs e)
        {
            /*
            frmCommandsInforming locCommandsInforming = new frmCommandsInforming();
            locCommandsInforming.Business = 1;                             //1 - Securuties, 2 - FX, 3 - LL
            locCommandsInforming.AktionDate = dFrom_Param;
            locCommandsInforming.Provider_ID = iProvider_ID_Param;
            locCommandsInforming.User_ID = iAdvisors_Param;
            locCommandsInforming.Aktion = iActions_Param;
            locCommandsInforming.Code = sCodes_Param;
            locCommandsInforming.ShowDialog();

            DefineList();
            */
        }
        private void tslDPMOrders_Click(object sender, EventArgs e)
        {
            frmDPMBuffer locDPMBuffer = new frmDPMBuffer();
            locDPMBuffer.DateFrom = dToday.Value;
            locDPMBuffer.DateTo = dToday.Value;
            locDPMBuffer.ShowDialog();
            DefineList();
        }

        private void txtShareTitle_TextChanged(object sender, EventArgs e)
        {

        }

        private void tslPreOrders_Click(object sender, EventArgs e)
        {
            txtFilter.Text = "";
            DefinePreOrdersList();
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
            if (bCheckList) InitLists();

            if (Convert.ToDateTime(dToday.Value).ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")) btnSave.Enabled = true;
            else btnSave.Enabled = false;
        }
        private void tcBusinessTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmptyCommand();
            ucCS.Filters = "Status = 1";

            switch (Convert.ToInt32(tcBusinessTypes.SelectedIndex))
            {
                case 0:                                                          // "tpRTO":
                    panDPM.Visible = false;
                    btnSaveTransfer.Visible = false;
                    iBusinessType_ID = 1;
                    lblBusinessType_ID.Text = "1";
                    iCommandType_ID = 1;
                    ucCS.ListType = 2;
                    ucCS.Visible = true;
                    ShowBusinessType();
                    InitLists();
                    break;
                case 1:                                                            // "tpDPM":
                    ucCS.Filters = "Status = 1 AND Service_ID = 3 AND User4_ID = " + Global.User_ID;
                    bCheckList = true;
                    panDPM.Visible = true;
                    btnSaveTransfer.Visible = true;
                    iBusinessType_ID = 1;
                    lblBusinessType_ID.Text = "1";
                    iCommandType_ID = 4;
                    ucCS.ListType = 2;
                    ucCS.Visible = true;
                    ShowBusinessType();
                    InitLists();
                    break;
                case 2:                                                              //   "tpBulk":
                    panDPM.Visible = false;
                    btnSaveTransfer.Visible = false;
                    iBusinessType_ID = 1;
                    lblBusinessType_ID.Text = "1";
                    ucCS.ListType = 3;
                    ucCS.Visible = true;
                    iCommandType_ID = 3;
                    ShowBusinessType();
                    InitLists();
                    break;
                case 3:                                                            //  "tpExecution":
                    panDPM.Visible = false;
                    btnSaveTransfer.Visible = false;
                    iBusinessType_ID = 2;
                    lblBusinessType_ID.Text = "2";
                    iCommandType_ID = 2;
                    ucCS.ListType = 2;
                    ucCS.Visible = true;
                    ShowBusinessType();
                    InitLists();
                    break;
            }
        }
        private void lnkPelatis_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            iClient_ID = Convert.ToInt32(lblClient_ID.Text);
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
            locContract.Contract_Details_ID = Convert.ToInt32(lblContract_Details_ID.Text);
            locContract.Contract_Packages_ID = Convert.ToInt32(lblContract_Packages_ID.Text);
            locContract.Client_ID = Convert.ToInt32(lblClient_ID.Text);
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

            fgCommandBuffer.Rows.Count = 2;

            InitLists();
        }
        private void txtAction_GotFocus(object sender, EventArgs e)
        {
            bClientChoiceMode = false;
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
                        ucPS.Focus();
                        break;
                   default:
                        panSecurities.BackColor = Color.Silver;
                        panSecurities.Visible = true;          
                        break;
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
        private void cmbProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitLists();
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
        private void cmbAdvisors_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitLists();
        }
        private void cmbDivisions_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitLists();
        }

        private void cmbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitLists();
        }

        private void cmbDiavivasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitLists();
        }
        private void cmbActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitLists();
        }

        private void cmbChecked_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitLists();
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
            //InitLists();
            lblSEStar.Visible = true;
            cmbProductType.SelectedValue = 0;
            cmbProductType.SelectedValue = 1;
            txtCodeTitle.Text = "";
            txtCodeISIN.Text = "";
            txtReutersCode.Text = "";
            cmbStockExchanges.SelectedValue = 0;
            dFrom.Value = DateTime.Now;
            lblISIN_Warning.Text = "";

            panNewProduct.Visible = true;
        }
        private void btnAddCommand_Click(object sender, EventArgs e)
        {
            bMultiProducts = true;
            i = fgCommandBuffer.Rows.Count;
            fgCommandBuffer.AddItem((i - 1) + "\t" + txtAction.Text + "\t" + lblProductTitle.Text + "\t" + ucPS.txtShareTitle.Text + "\t" + lblShareTitle.Text + "\t" +
                                    lnkISIN.Text + "\t" + Global.ShowPrices(lstType.SelectedIndex, Convert.ToSingle((Global.IsNumeric(txtPrice.Text) ? txtPrice.Text : "0"))) + "\t" +
                                    txtQuantity.Text + "\t" + txtAmount.Text + "\t" + lblCurr.Text + "\t" + cmbConstant.Text + "\t" + lblStockExchange_Code.Text + "\t" +
                                    lblClient_ID.Text + "\t" + iStockExchange_ID + "\t" + iShare_ID + "\t" + iContract_ID + "\t" +
                                    iProduct_ID + "\t" + iProductCategory_ID + "\t" + lstType.SelectedIndex + "\t" +
                                    txtPriceUp.Text + "\t" + txtPriceDown.Text + "\t" + cmbConstant.SelectedIndex, 2);

            bCheckShareList = false;
            txtAction.Text = "";
            iShare_ID = 0;
            ucPS.ShowProductsList = false;
            ucPS.txtShareTitle.Text = "";
            ucPS.ShowProductsList = true;
            lnkISIN.Text = "";
            lblShareTitle.Text = "";
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
            //iProvider_ID = 0;
            iStockExchange_ID = 0;
            cmbRecieveMethods.SelectedValue = 0;
            panCommandBuffer.Visible = true;

            txtAction.Focus();
            bCheckShareList = true;
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
        private void btnSaveProduct_Click(object sender, EventArgs e)
        {
            if ((txtCodeTitle.Text == "") || (txtCodeISIN.Text == "") || (txtReutersCode.Text == "") ||
                ((Convert.ToInt32(cmbStockExchanges.SelectedValue) == 0) && (Convert.ToInt32(cmbProductType.SelectedValue) != 6)) || (cmbCurrency.Text == ""))
                MessageBox.Show("Συμπληρώστε όλα τα απαραίτητα παιδία", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                if (txtCodeTitle.Text + "" == "") txtCodeTitle.Text = txtCodeTitle.Text + "";
                if (txtReutersCode.Text + "" == "") txtReutersCode.Text = txtReutersCode.Text + "";


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
                ProductCode.DateFrom = DateTime.Now;
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
                    ProductCode.StockExchange_ID = 21;                 // 21 - OTC
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
                if (iProduct_ID == 6)
                {
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
            }

            /*
            klsProductTitleCode = New clsProductTitleCode;
            klsProductTitleCode.DateFrom = dFrom.Value;
            klsProductTitleCode.DateTo = "2070/12/31";
            klsProductTitleCode.Share_ID = iShare_ID;
            klsProductTitleCode.ShareTitle_ID = iShareTitle_ID;
            klsProductTitleCode.ShareCode_ID = iShareCode_ID;
            klsProductTitleCode.InsertRecord()
            */

            bCheckShareList = false;
            lblShareTitle.Text = txtReutersCode.Text;
            lnkISIN.Text = txtCodeISIN.Text;
            lblShareTitle.Text = txtCodeTitle.Text;
            lblCurr.Text = cmbCurrency.Text;
            iShare_ID = iShareCode_ID;
            iStockExchange_ID = Convert.ToInt32(cmbStockExchanges.SelectedValue);
            iProduct_ID = Convert.ToInt32(cmbProductType.SelectedValue);
            iProductCategory_ID = Convert.ToInt32(cmbProductCategory.SelectedValue);
            bCheckShareList = true;

            //EditCashTables_LastEdit_Time(2)
            //CashTablesInitialisation("2")          ' 2 - Cash ShareCodes List
            panNewProduct.Visible = false;
        }
        private void btnCancelProduct_Click(object sender, EventArgs e)
        {
            panNewProduct.Visible = false;
        }
        #endregion
        #region --- MultiProduct functions ----------------------------------------------------------------
        private void picCloseCommandBuffer_Click(object sender, EventArgs e)
        {
            bMultiProducts = false;
            panCommandBuffer.Visible = false;
        }
        private void picRecieveVoicePath_Click(object sender, EventArgs e)
        {
            txtRecieveVoicePath.Text = Global.FileChoice();
        }
        private void picPlayRecieveVoice_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(txtRecieveVoicePath.Text);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }
        #endregion
        #region --- Save functions -----------------------------------------------------------------------
        private void btnSave_Click(object sender, EventArgs e)
        {
            bMultiProducts = false;
            SaveRecord(1);                             // Depository_ID = 1 - new order
        }
        private void btnSaveTransfer_Click(object sender, EventArgs e)
        {
            bMultiProducts = false;
            SaveRecord(3);                             // Depository_ID = 3 - confirmed from RTO
        }
        private void SaveRecord(int iDepository_ID)
        {
            if (fgCommandBuffer.Rows.Count == 2) {
                if (lstType.SelectedIndex == 0 && (txtPrice.Text == "0" || txtPrice.Text == ""))                                  //Or (txtQuantity.Text = "0" Or txtQuantity.Text = "") 
                    MessageBox.Show("Συμπληρώστε όλα τα παιδία", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    if (iCommandType_ID == 4) lblProvider_ID.Text = cmbServiceProviders.SelectedValue + "";

                SaveTransaction(Convert.ToInt32(lblBusinessType_ID.Text), iCommandType_ID, Convert.ToInt32(lblClient_ID.Text), lblCode.Text, lnkPortfolio.Text, iContract_ID,
                                Convert.ToInt32(lblContract_Details_ID.Text), Convert.ToInt32(lblContract_Packages_ID.Text), lnkPelatis.Text, txtAction.Text, dToday.Value, iProduct_ID, iProductCategory_ID,
                                iShare_ID, ucPS.txtShareTitle.Text, lblShareTitle.Text, txtQuantity.Text, lstType.SelectedIndex, txtPrice.Text,
                                txtPriceUp.Text, txtPriceDown.Text, txtAmount.Text, lblCurr.Text, cmbConstant.SelectedIndex, dConstant.Value,
                                Convert.ToInt32(lblProvider_ID.Text), iStockExchange_ID, lblStockExchange_Code.Text, 0, "", lblProductTitle.Text, "", iDepository_ID);
            }
            else {
                for (i = 2; i <= fgCommandBuffer.Rows.Count - 1; i++)
                {
                    if (iCommandType_ID == 4) lblProvider_ID.Text = cmbServiceProviders.SelectedValue + "";
                    SaveTransaction(Convert.ToInt32(lblBusinessType_ID.Text), iCommandType_ID, Convert.ToInt32(fgCommandBuffer[i, 12]), lblCode.Text, lnkPortfolio.Text,
                                Convert.ToInt32(fgCommandBuffer[i, 15]), Convert.ToInt32(lblContract_Details_ID.Text), Convert.ToInt32(lblContract_Packages_ID.Text), lnkPelatis.Text,
                                fgCommandBuffer[i, 1] + "", dToday.Value, Convert.ToInt32(fgCommandBuffer[i, 16]), Convert.ToInt32(fgCommandBuffer[i, 17]), Convert.ToInt32(fgCommandBuffer[i, 14]),
                                fgCommandBuffer[i, 4] + "", fgCommandBuffer[i, 3] + "", fgCommandBuffer[i, 7] + "", Convert.ToInt32(fgCommandBuffer[i, 18]), fgCommandBuffer[i, 6] + "",
                                fgCommandBuffer[i, 19] + "", fgCommandBuffer[i, 20] + "", fgCommandBuffer[i, 8] + "", fgCommandBuffer[i, 9] + "", Convert.ToInt32(fgCommandBuffer[i, 21]),
                                dConstant.Value, Convert.ToInt32(lblProvider_ID.Text), Convert.ToInt32(fgCommandBuffer[i, 13]), fgCommandBuffer[i, 11] + "",
                                Convert.ToInt32(cmbRecieveMethods.SelectedValue), txtRecieveVoicePath.Text.Trim(), fgCommandBuffer[i, 2] + "", "", iDepository_ID);
                }
            }

            EmptyCommand();
            InitLists();                                  // 1 - Securities
            if (fgList.Rows.Count > 2) fgList.Row = 2;
            fgList.Focus();

            lblQuantity.Text = Global.GetLabel("quantity");
            lblType.Visible = true;

            fgCommandBuffer.Rows.Count = 2;
            panCommandBuffer.Visible = false;
        }
        private int SaveTransaction(int iBusinessType_ID, int iCommandType_ID, int iClient_ID, string sCode, string sProfiteCenter,
                                    int iContract_ID, int iContract_Details_ID, int iContract_Packages_ID,
                                    string sClientName, string sAction, DateTime dToday, int iProduct_ID, int iProductCategory_ID,
                                    int iShare_ID, string sShare, string sShareTitle, string sQuantity,
                                    int iPriceType, string sPrice, string sPriceUp, string sPriceDown, string sAmount, string sCurr,
                                    int iConstant, DateTime dConst, int iProvider_ID, int iStockExchange_ID, string sStockExchange_Code,
                                    int iRecieveMethod_ID, string sRecieveFile, string sProductTitle, string sNotes, int iDepository_ID)
        {
            int k, iID, iBulcCommand_ID;
            string sError;
            clsOrdersSecurity Order = new clsOrdersSecurity();
            clsOrdersSecurity Order2 = new clsOrdersSecurity();

            iID = -1;
            sError = "";

            if (iCommandType_ID == 1)
                if (sProfiteCenter.Trim() == "") sError = sError + Global.GetLabel("enter_profitCenter_subacc") + (char)13;

            if (iProduct_ID == 0 || iProductCategory_ID == 0) sError = sError + Global.GetLabel("enter_your_product") + (char)13;

            if (sError.Length > 0) MessageBox.Show(sError, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                dTemp = Convert.ToDateTime("1900/01/01");
                Order.BulkCommand = "";
                Order.BusinessType_ID = iBusinessType_ID;
                Order.CommandType_ID = iCommandType_ID;
                if (iCommandType_ID == 4) {
                    Order.Company_ID = Global.User_ID;
                    Order.Depository_ID = iDepository_ID;                                                       // ONLY for iCommandType_ID = 4: 1 - new order, 2 - send to RTO, 3 - confirmed from RTO, 4 - transfered for execution or create new execution order
                    if (iDepository_ID == 1) Order.RecieveDate = Convert.ToDateTime("1900/01/01");
                    else Order.RecieveDate = DateTime.Now;
                }
                else {
                    Order.Company_ID = Global.Company_ID;
                    Order.Client_ID = iClient_ID;
                    Order.RecieveDate = DateTime.Now;
                }
                Order.ServiceProvider_ID = iProvider_ID;
                Order.StockExchange_ID = iStockExchange_ID;
                Order.CustodyProvider_ID = iProvider_ID;
                Order.Executor_ID = 0;
                Order.II_ID = 0;
                Order.Parent_ID = 0;
                Order.Contract_ID = iContract_ID;
                Order.Contract_Details_ID = iContract_Details_ID;
                Order.Contract_Packages_ID = iContract_Packages_ID;
                Order.Code = sCode;
                Order.ProfitCenter = sProfiteCenter;
                Order.Aktion = (sAction == "BUY" ? 1 : 2);
                Order.AktionDate = dToday;
                Order.Share_ID = iShare_ID;
                Order.Product_ID = iProduct_ID;
                Order.ProductCategory_ID = iProductCategory_ID;
                Order.PriceType = iPriceType;
                Order.Price = (sPrice.Length == 0 ? 0 : Convert.ToDecimal(sPrice));
                Order.Quantity = (sQuantity.Length == 0 ? 0 : Convert.ToDecimal(sQuantity));
                Order.Amount = (sAmount.Length == 0 ? 0 : Convert.ToDecimal(sAmount));
                Order.Curr = sCurr;
                Order.Constant = iConstant;
                Order.ConstantDate = ((iConstant == 2) ? dConst.ToString("yyyy/MM/dd") : "");
                Order.RecieveMethod_ID = iRecieveMethod_ID;

                Order.SentDate = Convert.ToDateTime("1900/01/01");
                Order.ExecuteDate = Convert.ToDateTime("1900/01/01");
                Order.User_ID = Global.User_ID;
                Order.DateIns = DateTime.Now;
                iID = Order.InsertRecord();

                dTemp = Order.RecieveDate;
                if (sRecieveFile.Length > 0) AddRecievedFile(iID, iRecieveMethod_ID, sRecieveFile, true);

                if (iCommandType_ID == 4 && iClient_ID != 0)
                {
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
                    Order2.ProfitCenter = sProfiteCenter;
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
                    Order2.ConstantDate = ((iConstant == 2) ? dConst.ToString("yyyy/MM/dd") : "");
                    Order2.RecieveMethod_ID = iRecieveMethod_ID;
                    Order2.RecieveDate = dTemp;                                                  // RecieveDate - date when RTO recieved this order - day when this order was sent to RTO. 1900/01/01 - means that order wasn't sent to RTO
                    Order2.SentDate = Convert.ToDateTime("1900/01/01");
                    Order2.ExecuteDate = Convert.ToDateTime("1900/01/01");
                    Order2.RealPrice = 0;
                    Order2.RealQuantity = 0;
                    Order2.RealAmount = 0; ;
                    Order2.InformationMethod_ID = 7;                                             // 7 -  Προσωπικά for simple DMP orders
                    Order2.FeesCalcMode = 1;
                    Order2.User_ID = Global.User_ID;
                    Order2.DateIns = DateTime.Now;
                    Order2.InsertRecord();

                    Order.BulkCommand = "0/<" + (iBulcCommand_ID + "") + ">";
                    Order.EditRecord();
                }

                sOldShare = sShare;

                if (iPriceType == 3)                                             // only for Scenario
                    if (sAction == "BUY")
                    {
                        if (sPriceUp != "" && sPriceUp != "0")
                        {
                            Order.Parent_ID = iID;
                            Order.Aktion = 2;
                            Order.Price = Convert.ToDecimal(sPriceUp);
                            if (Order.Product_ID == 2) Order.Amount = Order.Price * Order.Quantity / 100;
                            else Order.Amount = Order.Price * Order.Quantity / 100;
                            k = Order.InsertRecord();

                            if (sRecieveFile.Length > 0) AddRecievedFile(k, iRecieveMethod_ID, sRecieveFile, false);
                        }

                        if (sPriceDown != "" && sPriceDown != "0")
                        {
                            Order.Parent_ID = iID;
                            Order.Aktion = 2;
                            Order.Price = Convert.ToDecimal(sPriceDown);
                            if (Order.Product_ID == 2) Order.Amount = Order.Price * Order.Quantity / 100;
                            else Order.Amount = Order.Price * Order.Quantity;
                            k = Order.InsertRecord();

                            if (sRecieveFile.Length > 0) AddRecievedFile(k, iRecieveMethod_ID, sRecieveFile, false);
                        }
                    }
            }
            return iID;
        }
        #endregion
        #region --- fgList functionality ---------------------------------------------------------------------
        private void InitLists()
        {
            if (bCheckList) {
                    ShowList(1, iBusinessType_ID, iCommandType_ID, dToday.Value, dToday.Value, Convert.ToInt32(cmbProviders.SelectedValue), cmbProviders.Text,
                                          cmbDiavivasi.SelectedIndex, cmbDiavivasi.Text, cmbActions.SelectedIndex, cmbActions.Text, Convert.ToInt32(cmbAdvisors.SelectedValue),
                                          cmbAdvisors.Text, 0, "", Convert.ToInt32(cmbDivisions.SelectedValue), cmbDivisions.Text, Convert.ToInt32(cmbUsers.SelectedValue), cmbUsers.Text,
                                          0, "", lblCode.Text, "", "", 0, "", "", "", 0, 0, "", true, cmbChecked.SelectedIndex);
            }
        }
        private void AddRecievedFile(int iCommand_ID, int iRecieveMethod_ID, string sRecieveFilePath, bool bUploadFile)
        {

        }
        #endregion
        #region --- common functions ------------------------------------------------------------------------
        private void ShowBusinessType()
        {
            switch (iCommandType_ID)
            {
                case 1:
                    ucCS.Enabled = true;
                    lblCode.Text = "";
                    lnkPelatis.Text = "";
                    lnkPelatis.Enabled = true;
                    lnkPortfolio.Text = "";
                    iContract_ID = 0;
                    lblProvider_ID.Text = "0";
                    this.BackColor = Color.PeachPuff;
                    break;
                case 2:
                    ucCS.Enabled = true;
                    lblCode.Text = "";
                    lnkPelatis.Text = "";
                    lnkPelatis.Enabled = true;
                    lnkPortfolio.Text = "";
                    lblProvider_ID.Text = "0";
                    iContract_ID = 0;
                    this.BackColor = Color.LightSteelBlue;
                    break;
                case 3:
                    ucCS.Enabled = true;
                    lblCode.Text = "";
                    lnkPelatis.Text = "";
                    lnkPelatis.Enabled = true;
                    lnkPortfolio.Text = "";
                    lblProvider_ID.Text = "0";
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
                    lblProvider_ID.Text = "0";
                    this.BackColor = Color.LightBlue;
                    break;
            }
        }
        protected void ucCS_TextOfLabelChanged(object sender, EventArgs e)
        {
            Global.ContractData stContract = new Global.ContractData();
            stContract = ucCS.SelectedContractData;
            if (ucCS.Contract_ID.Text != "0")
            {
                switch (ucCS.ListType)
                {
                    case 1:
                    case 2:
                        //klsContract_Blocks = new clsContract_Blocks;
                        //klsContract_Blocks.Contract_ID = stCustomer.Contract_ID;
                        //klsContract_Blocks.Record_ID = 0;
                        //klsContract_Blocks.GetRecord_Contract();
                        //if (klsContract_Blocks.Record_ID == 0)
                        //{ 
                        lnkPelatis.Text = stContract.ContractTitle;
                        lblCode.Text = stContract.Code;
                        lnkPortfolio.Text = stContract.Portfolio;
                        lblClient_ID.Text = stContract.Client_ID + "";
                        iContract_ID = stContract.Contract_ID;
                        lblContract_Details_ID.Text = stContract.Contracts_Details_ID + "";
                        lblContract_Packages_ID.Text = stContract.Contracts_Packages_ID + "";
                        lblProvider_ID.Text = stContract.Provider_ID + "";
                        cmbServiceProviders.SelectedValue = stContract.Provider_ID;
                        sProviderTitle = stContract.Provider_Title + "";
                        iMIFIDCategory_ID = stContract.MIFIDCategory_ID;
                        //iMIFID_Risk_Index = stContract.MIFID_Risk_Index;
                        iMIFID_2 = stContract.MIFID_2;
                        //iClientType = stContract.Category;

                        iBusinessType_ID = 1;                                                          // by default                   BusinessType = 1
                        lblBusinessType_ID.Text = "1";
                        if (stContract.Provider_ID == Global.Company_ID) iBusinessType_ID = 2;         // curCompanyID - HellasFin, so BusinessType = 2
                        lblBusinessType_ID.Text = iBusinessType_ID.ToString();

                        InitLists();

                        clsContracts_CashAccounts ClientCashAccounts = new clsContracts_CashAccounts();
                        ClientCashAccounts.Client_ID = 0;
                        ClientCashAccounts.Contract_ID = stContract.Contract_ID;
                        ClientCashAccounts.GetList();

                        txtAction.Focus();
                        bClientChoiceMode = true;
                        //}
                        //else
                        //MessageBox.Show("Contract Blocked", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        break;
                    case 3:
                        stContract = ucCS.SelectedContractData;
                        lnkPelatis.Text = stContract.ContractTitle;
                        lblCode.Text = stContract.Code;
                        lnkPortfolio.Text = stContract.Portfolio;
                        lblClient_ID.Text = stContract.Client_ID + "";
                        iContract_ID = 0;
                        lblContract_Details_ID.Text = "0";
                        lblContract_Packages_ID.Text = "0";
                        lblProvider_ID.Text = stContract.Provider_ID + "";
                        sProviderTitle = stContract.Provider_Title;
                        //iClientType = stContract.Category;
                        break;
                }

            }
        }
        protected void ucPS_TextOfLabelChanged(object sender, EventArgs e)
        {
            Global.ProductData stProduct = new Global.ProductData();
            stProduct = ucPS.SelectedProductData;
            iShare_ID = stProduct.ShareCode_ID;
            iStockExchange_ID = stProduct.StockExchange_ID;
            sTemp = "";
            if (txtAction.Text == "BUY") sTemp = Global.CheckCompatibility(iContract_ID, iMIFID_2, iMIFIDCategory_ID, iXAA, iShare_ID, iStockExchange_ID);
            if (sTemp.Length == 0)
            {
                lnkISIN.Text = stProduct.ISIN;
                lblShareTitle.Text = stProduct.Title;
                //lblProduct.Text = stProduct.Product_Title;
                iProduct_ID = stProduct.Product_ID;
                iProductCategory_ID = stProduct.ProductCategory_ID;
                iShare_ID = stProduct.ShareCode_ID;
                lblCurr.Text = stProduct.Currency;

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
            else {
                MessageBox.Show(sTemp, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ucPS.ShowProductsList = false;
                ucPS.txtShareTitle.Text = "";
                //ucPS.ShareCode_ID.Text = "-999";
                ucPS.ShowProductsList = true;
                ucPS.Focus();
            }
        }
        private void EmptyCommand()
        {
            bCheckSurname = false;
            lblClient_ID.Text = "0";
            ucCS.ShowClientsList = false;
            ucCS.txtContractTitle.Text = "";
            ucCS.ShowClientsList = true;
            lnkPortfolio.Text = "";
            sProviderTitle = "";
            lblCode.Text = "";
            lnkPelatis.Text = "";
            txtAction.Text = "";
            iShare_ID = 0;
            ucPS.ShowProductsList = false;
            ucPS.txtShareTitle.Text = "";
            ucPS.ShowProductsList = true;
            lnkISIN.Text = "";
            lblShareTitle.Text = "";
            //lblProduct.Text = "";
            iProduct_ID = 0;
            lblProductTitle.Text = "";
            iProductCategory_ID = 0;
            txtQuantity.Text = "";
            lstType.SelectedIndex = 0;
            txtPrice.Text = "";
            txtPriceUp.Text = "";
            txtPriceDown.Text = "";
            txtAmount.Text = "";
            lblCurr.Text = "";
            cmbConstant.SelectedIndex = 0;
            lblProvider_ID.Text = "0";
            iStockExchange_ID = 0;
            lblStockExchange_Code.Text = "";

            panSecurities.BackColor = Color.Transparent;
            panFilters.Visible = true;

            ucCS.txtContractTitle.Focus();
            bCheckSurname = true;

        }
        #endregion
        #region --- fgList functions -----------------------------------------------------------------------
        public void ShowList(int iEnterPoint, int iBusinessType, int iCommandType, DateTime dFrom, DateTime dTo, int iProvider_ID, string sProvider_Title,
                 int iDiavivasi, string sDiavivasi_Title, int iActions, string sActions_Title, int iAdvisors, string sAdvisors_Title,
                 int iDiax, string sDiax_Title, int iDivision, string sDivision_Title, int iDiavivastis_ID, string sDiavivastis_Title,
                 int iServices_ID, string sServices_Title, string sCodes, string sSharesList, string sClientPackages, int iBuySell,
                string sPriceFrom, string sPriceTo, string sCurrency, int iProductType, int iService, string sStockExchangeList, bool bShowCancelled, int iChecked)
        {
            iEnterPoint_Param = iEnterPoint;
            iBusinessType_Param = iBusinessType;
            iCommandType_Param = iCommandType;
            dFrom_Param = dFrom;
            dTo_Param = dTo;  //.Date.AddHours(12).AddMinutes(0).AddSeconds(-1)
            iProvider_ID_Param = iProvider_ID;
            sProvider_Title_Param = sProvider_Title;
            iDiavivasi_Param = iDiavivasi;
            sDiavivasi_Title_Param = sDiavivasi_Title;
            iActions_Param = iActions;
            sActions_Title_Param = sActions_Title;
            iAdvisors_Param = iAdvisors;
            sAdvisors_Title_Param = sAdvisors_Title;
            iDiax_Param = iDiax;
            sDiax_Title_Param = sDiax_Title;
            iDivision_Param = iDivision;
            sDivision_Title_Param = sDivision_Title;
            iDiavivastis_Param = iDiavivastis_ID;
            sDiavivastis_Title_Param = sDiavivastis_Title;
            sCodes_Param = sCodes;
            sSharesList_Param = sSharesList;
            sClientPackages_Param = sClientPackages;
            iSendCheck_Param = iChecked;
            iBuySell_Param = iBuySell;
            if (Global.IsNumeric(sPriceFrom)) sgPriceFrom_Param = Convert.ToSingle(sPriceFrom);
            else sgPriceFrom_Param = 0;

            if (Global.IsNumeric(sPriceTo)) sgPriceTo_Param = Convert.ToSingle(sPriceTo);
            else sgPriceTo_Param = 99999999;

            sCurrency_Param = sCurrency;
            iProductType_Param = iProductType;
            iService_Param = iService;
            bShowCancelled_Param = bShowCancelled;

            sStockExchangeList_Param = sStockExchangeList;

            sPreCode = "";
            sPreISIN = "";
            if (dFrom_Param.Date == DateTime.Now.Date) dtEURRates = Global.dtTodayEURRates.Copy();
            else
            {
                clsCurrencies klsCurrency = new clsCurrencies();
                klsCurrency.DateFrom = dFrom_Param;
                klsCurrency.DateTo = dTo_Param;
                klsCurrency.Code = "EUR";
                klsCurrency.GetCurrencyRates_Period();
                dtEURRates = klsCurrency.List.Copy();
            }

            switch (iCommandType_Param)
            {
                case 1:                          // 1 - RTO list  
                    toolLeft.Width = 500;
                    tsbInform.Visible = true;
                    tss1.Visible = true;
                    tslPreOrders.Visible = true;
                    tslDPMOrders.Visible = true;
                    tss2.Visible = true;
                    tsbXML.Visible = false;
                    tss3.Visible = false;
                    tss4.Visible = true;
                    tsbTransfer2RTO.Visible = false;
                    tss6.Visible = false;
                    break;
                case 2:                          // 2 - Exec List
                    toolLeft.Width = 160;
                    tsbInform.Visible = false;
                    tss1.Visible = false;
                    tslPreOrders.Visible = false;
                    tslDPMOrders.Visible = false;
                    tss2.Visible = false;
                    tsbXML.Visible = true;
                    tss3.Visible = false;
                    tss4.Visible = false;
                    tsbTransfer2RTO.Visible = false;
                    tss6.Visible = false;
                    break;
                case 3:                          // 3 - Bulk List
                    toolLeft.Width = 140;
                    tsbInform.Visible = false;
                    tss1.Visible = false;
                    tslPreOrders.Visible = false;
                    tslDPMOrders.Visible = false;
                    tss2.Visible = false;
                    tsbXML.Visible = false;
                    tss3.Visible = false;
                    tss4.Visible = false;
                    tsbTransfer2RTO.Visible = false;
                    tss6.Visible = false;
                    break;
                case 4:                          // 4 - DPM List
                    toolLeft.Width = 200;
                    tsbInform.Visible = false;
                    tss1.Visible = false;
                    tslPreOrders.Visible = false;
                    tslDPMOrders.Visible = false;
                    tss2.Visible = false;
                    tsbXML.Visible = false;
                    tss3.Visible = false;
                    tss4.Visible = false;
                    tsbTransfer2RTO.Visible = true;
                    tss6.Visible = true;
                    break;
                case 5:                         // 5 - DPM Source List
                    toolLeft.Width = 200;
                    tsbInform.Visible = false;
                    tss1.Visible = false;
                    tslPreOrders.Visible = false;
                    tslDPMOrders.Visible = false;
                    tss2.Visible = false;
                    tsbXML.Visible = false;
                    tss3.Visible = false;
                    tss4.Visible = false;
                    tsbTransfer2RTO.Visible = false;
                    break;
            }

            toolLeft.Visible = true;

            Column clm0 = fgList.Cols[0];
            clm0.ImageMap = htStatus;
            clm0.ImageAndText = false;
            clm0.ImageAlign = ImageAlignEnum.CenterCenter;

            mnuShowFile.Visible = false;
            DefineList();

        }
        public void DefineList()
        {
            clsOrdersSecurity klsOrder = new clsOrdersSecurity();

            fgList.Redraw = false;
            fgList.Rows.Count = 2;
            switch (iCommandType_Param)
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
                    fgList.Cols[25].Visible = true;

                    i = 0;
                    iOddEvenBlock = 0;              //pseudo even block
                    sInvPropNotesFlag = "";
                    sDPMNotesFlag = "";

                    klsOrder.CommandType_ID = iCommandType_Param;
                    klsOrder.DateFrom = dFrom_Param;
                    klsOrder.DateTo = dTo_Param;
                    klsOrder.ServiceProvider_ID = iProvider_ID_Param;
                    klsOrder.Sent = iDiavivasi_Param;
                    klsOrder.Actions = iActions_Param;
                    klsOrder.User1_ID = iAdvisors_Param;
                    klsOrder.User3_ID = iDiavivastis_Param;
                    klsOrder.Division_ID = iDivision_Param;
                    klsOrder.Code = sCodes_Param;
                    klsOrder.GetList();

                    foreach (DataRow dtRow in klsOrder.List.Rows)
                    {
                        bFilter = true;
                        //if (iActions_Param == 0) bFilter = true;
                        //else if (iActions_Param == 1) if (Convert.ToDateTime(dtRow["ExecuteDate"]) != Convert.ToDateTime("1900/01/01")) bFilter = true;
                        //    else if (iActions_Param == 2) if (Convert.ToDateTime(dtRow["ExecuteDate"]) == Convert.ToDateTime("1900/01/01")) bFilter = true;

                        if ((iSendCheck_Param == 1 && Convert.ToInt32(dtRow["SendCheck"]) == 0) || (iSendCheck_Param == 2 && Convert.ToInt32(dtRow["SendCheck"]) == 1)) bFilter = false;

                        if ((dtRow["BulkCommand"] + "") != "" && Convert.ToDateTime(dtRow["RecieveDate"]) == Convert.ToDateTime("1900/01/01")) bFilter = false;

                        if (bFilter) {                            

                            if (Convert.ToInt32(dtRow["Type"]) == 3 && Convert.ToInt32(dtRow["Parent_ID"]) == 0) {           // if it's scenario first command
                                if (iOddEvenBlock == 1) iOddEvenBlock = 2;                                                   // define odd/even block
                                else iOddEvenBlock = 1;
                                iStyle = iOddEvenBlock;
                            }
                            else if (Convert.ToInt32(dtRow["Parent_ID"]) == 0) iStyle = 0;                                   // it's simple command

                            if ((dtRow["Currency"] + "") == "EUR") {
                                sgTemp1 = 1;                                                                                // CurrRate
                                sgTemp2 = Convert.ToSingle(dtRow["RealAmount"]);                                            // Amount EUR 
                            }
                            else {
                                foundRows = dtEURRates.Select("DateIns = '" + Convert.ToDateTime(dtRow["ExecuteDate"]).ToString("yyyy/MM/dd") + "' AND Currency = 'EUR" + dtRow["Currency"] + "='");
                                if (foundRows.Length > 0) sgTemp1 = Convert.ToSingle(foundRows[0]["Rate"]);                 // CurrRate
                                if (sgTemp1 != 0) sgTemp2 = Convert.ToSingle(dtRow["RealAmount"]) / sgTemp1;                // Amount EUR           
                            }

                            sBulkCommand = (dtRow["BulkCommand"] + "").Replace("<", "").Replace(">", "");
                            sBulkCommand = (sBulkCommand == "0" ? "" : sBulkCommand);

                            i = i + 1;
                            fgList.AddItem(dtRow["Type"] + "\t" + i + "\t" + sBulkCommand + "\t" + dtRow["ClientFullName"] + "\t" + dtRow["ContractTitle"] + "\t" +
                                           dtRow["StockCompanyTitle"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" +
                                           (Convert.ToInt32(dtRow["Aktion"]) == 1 ? "BUY" : "SELL") + "\t" + dtRow["Product_Title"] + "/" + dtRow["Product_Category"] + "\t" +
                                           dtRow["Share_Title"] + "\t" + dtRow["Share_Code"] + "\t" + dtRow["Share_ISIN"] + "\t" + 
                                           Global.ShowPrices(Convert.ToInt16(dtRow["PriceType"]), Convert.ToSingle(dtRow["Price"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["Quantity"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["Quantity"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["Amount"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["Amount"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["RealPrice"]) == 0 ? "" : string.Format("{0:#,0.00##}", dtRow["RealPrice"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["RealQuantity"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["RealQuantity"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["RealAmount"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["RealAmount"])) + "\t" + dtRow["Currency"] + "\t" + 
                                           sConstant[Convert.ToInt16(dtRow["Constant"])].Trim() + " " + dtRow["ConstantDate"] + "\t" + dtRow["StockExchange_MIC"] + "\t" +
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
                                           dtRow["Tipos"] + "\t" + sgTemp1 + "\t" + sgTemp2);
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
                    fgList.Cols[25].Visible = false;

                    i = 0;
                    iOddEvenBlock = 0;             // pseudo even block
                    sInvPropNotesFlag = "";
                    sDPMNotesFlag = "";

                    klsOrder.CommandType_ID = iCommandType_Param;
                    klsOrder.DateFrom = dFrom_Param;
                    klsOrder.DateTo = dTo_Param;
                    klsOrder.ServiceProvider_ID = iProvider_ID_Param;
                    klsOrder.Sent = iDiavivasi_Param;
                    klsOrder.Actions = iActions_Param;
                    klsOrder.User1_ID = iAdvisors_Param;
                    klsOrder.User3_ID = iDiavivastis_Param;
                    klsOrder.Division_ID = iDivision_Param;
                    klsOrder.Code = sCodes_Param;
                    klsOrder.GetExecutionList();
                    foreach (DataRow dtRow in klsOrder.List.Rows)
                    {
                        bFilter = false;
                        if (iActions_Param == 0) bFilter = true;
                        else if (iActions_Param == 1) if (Convert.ToDateTime(dtRow["ExecuteDate"]) != Convert.ToDateTime("1900/01/01")) bFilter = true;
                            else if (iActions_Param == 2) if (Convert.ToDateTime(dtRow["ExecuteDate"]) == Convert.ToDateTime("1900/01/01")) bFilter = true;

                        if ((iSendCheck_Param == 1 && Convert.ToInt32(dtRow["SendCheck"]) == 0) || (iSendCheck_Param == 2 && Convert.ToInt32(dtRow["SendCheck"]) == 1)) bFilter = false;

                        if (iProvider_ID_Param != 0 && Convert.ToInt32(dtRow["StockCompany_ID"]) != iProvider_ID_Param) bFilter = false;

                        if (bFilter) {
                            i = i + 1;

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

                            sClientFullName = dtRow["ClientFullName"] + "";

                            sBulkCommand = (dtRow["BulkCommand"] + "").Replace("<", "").Replace(">", "");
                            sBulkCommand = (sBulkCommand == "0" ? "" : sBulkCommand);

                            if ((dtRow["Currency"] + "") == "EUR") {
                                sgTemp1 = 1;                                                                                // CurrRate
                                sgTemp2 = Convert.ToSingle(dtRow["RealAmount"]);                                            // Amount EUR 
                            }
                            else  {
                                foundRows = dtEURRates.Select("DateIns = '" + Convert.ToDateTime(dtRow["ExecuteDate"]).ToString("yyyy/MM/dd") + "' AND Currency = 'EUR" + dtRow["Currency"] + "='");
                                if (foundRows.Length > 0) sgTemp1 = Convert.ToSingle(foundRows[0]["Rate"]);                 // CurrRate
                                if (sgTemp1 != 0) sgTemp2 = Convert.ToSingle(dtRow["RealAmount"]) / sgTemp1;                // Amount EUR           
                            }

                            fgList.AddItem(dtRow["Type"] + "\t" + i + "\t" + sBulkCommand + "\t" + sClientFullName + "\t" + "" + "\t" +
                                           dtRow["StockCompanyTitle"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" +
                                           (Convert.ToInt32(dtRow["Aktion"]) == 1 ? "BUY" : "SELL") + "\t" + dtRow["Product_Title"] + "/" + dtRow["Product_Category"] + "\t" +
                                           dtRow["Share_Title"] + "\t" + dtRow["Share_Code"] + "\t" + dtRow["Share_ISIN"] + "\t" + 
                                           Global.ShowPrices(Convert.ToInt16(dtRow["PriceType"]), Convert.ToSingle(dtRow["Price"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["Quantity"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["Quantity"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["Amount"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["Amount"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["RealPrice"]) == 0 ? "" : string.Format("{0:#,0.00##}", dtRow["RealPrice"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["RealQuantity"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["RealQuantity"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["RealAmount"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["RealAmount"])) + "\t" + dtRow["Currency"] + "\t" + 
                                           sConstant[Convert.ToInt16(dtRow["Constant"])].Trim() + " " + dtRow["ConstantDate"] + "\t" + dtRow["StockExchange_MIC"] + "\t" +
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
                                           dtRow["Tipos"] + "\t" + sgTemp1 + "\t" + sgTemp2);

                        }
                    }
                    fgList.Sort(SortFlags.Descending, 22);     // 22- RecievedDate
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
                    fgList.Cols[25].Visible = false;

                    i = 0;
                    sClientFullName = Global.CompanyName;

                    klsOrder.CommandType_ID = iCommandType_Param;                               //  3 - Bulk Orders
                    klsOrder.DateFrom = dFrom_Param;
                    klsOrder.DateTo = dTo_Param;
                    klsOrder.GetBulkList();
                    foreach (DataRow dtRow in klsOrder.List.Rows)
                    {
                        i = i + 1;

                        sBulkCommand = (dtRow["BulkCommand"]+"").Replace("<", "").Replace(">", "");
                        sBulkCommand = (sBulkCommand == "0"? "": sBulkCommand);

                        if (iProvider_ID_Param == 0 || Convert.ToInt32(dtRow["StockCompany_ID"]) == iProvider_ID_Param) {

                            fgList.AddItem("0" + "\t" +i + "\t" + sBulkCommand + "\t" + dtRow["Client_Title"] + "\t" + dtRow["ContractTitle"] + "\t" + 
                                         dtRow["StockCompanyTitle"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" +
                                         ((Convert.ToInt32(dtRow["Aktion"]) == 1) ? "BUY" : "SELL") + "\t" + dtRow["Product_Title"] + "/" + dtRow["Product_Category"] + "\t" +
                                         dtRow["Share_Title"] + "\t" + dtRow["Share_Code"] + "\t" + dtRow["Share_ISIN"] + "\t" +
                                         Global.ShowPrices(Convert.ToInt16(dtRow["PriceType"]), Convert.ToSingle(dtRow["Price"])) + "\t" +
                                         (Convert.ToDecimal(dtRow["Quantity"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["Quantity"])) + "\t" +
                                         (Convert.ToDecimal(dtRow["Amount"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["Amount"])) + "\t" +
                                         (Convert.ToDecimal(dtRow["RealPrice"]) == 0 ? "" : string.Format("{0:#,0.00##}", dtRow["RealPrice"])) + "\t" +
                                         (Convert.ToDecimal(dtRow["RealQuantity"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["RealQuantity"])) + "\t" +
                                         (Convert.ToDecimal(dtRow["RealAmount"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["RealAmount"])) + "\t" + dtRow["Currency"] + "\t" + 
                                         sConstant[Convert.ToInt16(dtRow["Constant"])].Trim() + " " + dtRow["ConstantDate"] + "\t" + dtRow["StockExchange_MIC"] + "\t" +
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
                                         "0" + "\t" + "0" + "\t" + "0" + "\t" + "0" + "\t" + dtRow["RealAmount"]);
                        }
                    }
                    fgList.Sort(SortFlags.Descending, 22);     // 22- RecievedDate
                    break;
                case 4:
                    fgList.Cols[2].Width = 90;
                    fgList.Cols[3].Visible = true;
                    fgList.Cols[3].Width = 160;
                    fgList.Cols[4].Visible = false;
                    fgList.Cols[4].Width = 100;
                    fgList.Cols[6].Visible = false;

                    i = 0;
                    sClientFullName = Global.CompanyName;

                    klsOrder.CommandType_ID = iCommandType_Param;                               //  4 - DPM Orders tou RTO
                    klsOrder.DateFrom = dFrom_Param;
                    klsOrder.DateTo = dTo_Param;
                    klsOrder.User_ID = 0; // Global.User_ID;
                    klsOrder.GetDPMList();
                    foreach (DataRow dtRow in klsOrder.List.Rows)
                    {
                        bFilter = false;
                        if (iActions_Param == 0) bFilter = true;
                        else if (iActions_Param == 1) if (Convert.ToDateTime(dtRow["ExecuteDate"]) != Convert.ToDateTime("1900/01/01")) bFilter = true;
                             else if (iActions_Param == 2) if (Convert.ToDateTime(dtRow["ExecuteDate"]) == Convert.ToDateTime("1900/01/01")) bFilter = true;

                        if ((iSendCheck_Param == 1 && Convert.ToInt32(dtRow["SendCheck"]) == 0) || (iSendCheck_Param == 2 && Convert.ToInt32(dtRow["SendCheck"]) == 1)) bFilter = false;

                        if (iProvider_ID_Param != 0 && Convert.ToInt32(dtRow["StockCompany_ID"]) != iProvider_ID_Param) bFilter = false;

                        if (Convert.ToInt32(dtRow["Company_ID"]) != Global.User_ID && Convert.ToInt32(dtRow["Depository_ID"]) < 3 && Global.Sender != 1) bFilter = false;  // it's not Depository_ID - it's flag that order was confirmed from RTO (3) or transffered to execution (4)

                        if (Convert.ToDateTime(dtRow["SentDate"]) == Convert.ToDateTime("1900/01/01")) bFilter = false;                        // for DPM  orders - if SenDate = 1900/01/01 it's unvisible order


                        if ((dtRow["Currency"] + "") == "EUR")
                        {
                            sgTemp1 = 1;                                                                                // CurrRate
                            sgTemp2 = Convert.ToSingle(dtRow["RealAmount"]);                                            // Amount EUR 
                        }
                        else
                        {
                            foundRows = dtEURRates.Select("DateIns = '" + Convert.ToDateTime(dtRow["ExecuteDate"]).ToString("yyyy/MM/dd") + "' AND Currency = 'EUR" + dtRow["Currency"] + "='");
                            if (foundRows.Length > 0) sgTemp1 = Convert.ToSingle(foundRows[0]["Rate"]);                 // CurrRate
                            if (sgTemp1 != 0) sgTemp2 = Convert.ToSingle(dtRow["RealAmount"]) / sgTemp1;                // Amount EUR           
                        }

                        if (bFilter) {

                            i = i + 1;

                            sBulkCommand = (dtRow["BulkCommand"]+"").Replace("<", "").Replace(">", "");
                            sBulkCommand = (sBulkCommand == "0" ? "": sBulkCommand);
                            fgList.AddItem("0" + "\t" + i + "\t" + sBulkCommand + "\t" + dtRow["ClientFullName"] + "\t" + "" + "\t" + 
                                           dtRow["StockCompanyTitle"] + "\t" + dtRow["Code"] + "\t" +  dtRow["Portfolio"] + "\t" + 
                                           ((Convert.ToInt32(dtRow["Aktion"]) == 1) ? "BUY" : "SELL") + "\t" + dtRow["Product_Title"] + "/" + dtRow["Product_Category"] + "\t" +
                                           dtRow["Share_Title"] + "\t" + dtRow["Share_Code"] + "\t" + dtRow["Share_ISIN"] + "\t" + 
                                           Global.ShowPrices(Convert.ToInt16(dtRow["Type"]), Convert.ToSingle(dtRow["Price"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["Quantity"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["Quantity"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["Amount"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["Amount"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["RealPrice"]) == 0 ? "" : string.Format("{0:#,0.00##}", dtRow["RealPrice"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["RealQuantity"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["RealQuantity"])) + "\t" +
                                           (Convert.ToDecimal(dtRow["RealAmount"]) == 0 ? "" : string.Format("{0:#,0.00}", dtRow["RealAmount"])) + "\t" + dtRow["Currency"] + "\t" + 
                                           sConstant[Convert.ToInt16(dtRow["Constant"])].Trim() + " " + dtRow["ConstantDate"] + "\t" + dtRow["StockExchange_MIC"] + "\t" +
                                           ((Convert.ToDateTime(dtRow["RecieveDate"]) != Convert.ToDateTime("31/12/2070")) ? Convert.ToDateTime(dtRow["RecieveDate"]).ToString("yyyy/MM/dd") : "") + "\t" + 
                                           ((Convert.ToDateTime(dtRow["SentDate"]) != Convert.ToDateTime("01/01/1900")) ? Convert.ToDateTime(dtRow["SentDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                           ((Convert.ToDateTime(dtRow["ExecuteDate"]) != Convert.ToDateTime("01/01/1900")) ? Convert.ToDateTime(dtRow["ExecuteDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                           dtRow["RecieveTitle"] + "\t" + "" + "\t" + dtRow["Notes"] + "\t" + dtRow["Author_Fullname"] + "\t" + "" + "\t" + 
                                           dtRow["Diax_Fullname"] + "\t" + dtRow["ServiceTitle"] + "\t" + "" + "\t" + "" + "\t" + dtRow["II_ID"] + "\t" + sRisks[Convert.ToInt32(dtRow["Risk"])] + "\t" +
                                           sMiFID[Convert.ToInt32(dtRow["MiFIDCategory_ID"])] + "\t" + dtRow["StockExchange_Title"] + "\t" + dtRow["Recomend"] + "\t" +
                                           "0" + "\t" + "0" + "\t" + "0" + "\t" + "0" + "\t" + "0" + "\t" + "0" + "\t" + "0" + "\t" +
                                           dtRow["ID"] + "\t" + dtRow["Client_ID"] + "\t" + dtRow["StockCompany_ID"] + "\t" + dtRow["Status"] + "\t" + dtRow["ID"] + "\t" +
                                           "0" + "\t" + dtRow["Share_ID"] + "\t" + dtRow["Contract_ID"] + "\t" + dtRow["Contract_Details_ID"] + "\t" + dtRow["Contract_Packages_ID"] + "\t" +
                                           "" + "\t" + dtRow["BusinessType_ID"] + "\t" + dtRow["Product_ID"] + "\t" + dtRow["ProductCategory_ID"] + "\t" +
                                           dtRow["SendCheck"] + "\t" + dtRow["Executor_Title"] + "\t" + dtRow["ValueDate"] + "\t" + "0" + "\t" + "0" + "\t" + 
                                           "" + "\t" + "0" + "\t" + "0" + "\t" + "0" + "\t" + sgTemp1 + "\t" + sgTemp2);
                        }
                    }
                    fgList.Sort(SortFlags.Descending, 22);     // 22 - RecievedDate
                    break;
            }
            fgList.Redraw = true;
            if (fgList.Rows.Count > 2) fgList.Row = 2;
            fgList.Focus();

            DefinePreOrdersList();
        }
        private void fgList_RowColChange(object sender, EventArgs e)
        {
            if (fgList.Row > 1) iClientData_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);             
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
            if (iEnterPoint_Param == 2)                                                            //  2 - from TransactionsSearch
                if (fgList.Col == 0)
                    if ((fgList[fgList.Row, "Check_FileName"] + "") != "")
                        try
                        {
                            Global.DMS_ShowFile("Customers/" + fgList[fgList.Row, "ContractTitle"] + " / Informing", fgList[fgList.Row, "Check_FileName"] + "");     //is DMS file, so show it into Web mode
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }
        private void fgList_DoubleClick(object sender, EventArgs e)
        {
            iRow = fgList.Row;
            if (iRow > 0)
            {
                switch (iCommandType_Param)
                {
                    case 1:
                        frmOrderSecurity locOrderSecurity = new frmOrderSecurity();
                        locOrderSecurity.Rec_ID = Convert.ToInt32(fgList[iRow, "ID"]);                // Rec_ID != 0     EDIT mode
                        locOrderSecurity.BusinessType = iBusinessType_Param;
                        locOrderSecurity.RightsLevel = iRightsLevel;
                        locOrderSecurity.Editable = 1;
                        locOrderSecurity.ShowDialog();
                        if (locOrderSecurity.LastAktion == 1)
                        {                                     // Aktion=1        was saved (added)
                            clsOrdersSecurity klsOrder = new clsOrdersSecurity();
                            klsOrder.Record_ID = Convert.ToInt32(fgList[iRow, "ID"]);
                            klsOrder.CommandType_ID = iCommandType_Param;
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
                            fgList[iRow, 21] = klsOrder.StockExchange_Title;
                            fgList[iRow, 22] = (klsOrder.RecieveDate.ToString("dd/MM/yyyy") == "01/01/1900" ? "" : Convert.ToDateTime(klsOrder.RecieveDate).ToString("dd/MM/yy HH:mm:ss"));
                            if (klsOrder.SentDate == Convert.ToDateTime("1900/01/01")) fgList[iRow, 23] = "";
                            else fgList[iRow, 23] = Convert.ToDateTime(klsOrder.SentDate).ToString("dd/MM/yy HH:mm:ss");
                            fgList[iRow, 24] = (klsOrder.RealPrice == 0 ? "" : Convert.ToDateTime(klsOrder.ExecuteDate).ToString("dd/MM/yy"));
                            fgList[iRow, 25] = klsOrder.RecieveTitle;
                            fgList[iRow, 26] = klsOrder.InformationTitle;
                            fgList[iRow, "Notes"] = klsOrder.Notes;
                            fgList[iRow, 29] = klsOrder.AdvisorName;
                            fgList[iRow, 39] = klsOrder.FeesPercent;
                            fgList[iRow, 32] = klsOrder.FeesAmount;
                            fgList[iRow, 33] = klsOrder.FinishFeesPercent;
                            fgList[iRow, 34] = klsOrder.FinishFeesAmount;
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
                        locOrderExecution.CommandType_ID = iCommandType_Param;                     // 2 - Execution Order
                        locOrderExecution.RightsLevel = iRightsLevel;
                        locOrderExecution.Editable = 1;
                        locOrderExecution.ShowDialog();
                        DefineList();
                        break;
                    case 3:
                        frmOrderExecution locBulkExecution = new frmOrderExecution();
                        locBulkExecution.Rec_ID = Convert.ToInt32(fgList[iRow, "ID"]);
                        locBulkExecution.CommandType_ID = iCommandType_Param;                      // 3 - Bulk Order
                        locBulkExecution.RightsLevel = iRightsLevel;
                        locBulkExecution.Editable = 1;
                        locBulkExecution.ShowDialog();
                        DefineList();
                        break;
                    case 4:
                        frmOrderDPM locOrderDPM = new frmOrderDPM();
                        locOrderDPM.Rec_ID = Convert.ToInt32(fgList[iRow, "ID"]);
                        locOrderDPM.CommandType_ID = iCommandType_Param;                           // 4 - DPM Order
                        locOrderDPM.RightsLevel = iRightsLevel;
                        locOrderDPM.Editable = 1;
                        locOrderDPM.ShowDialog();
                        DefineList();
                        break;
                }
            }
        }
        private void fgList_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            if (e.Row > 1)
            {
                if (e.Col == 8)                                                                                // 8 - Action
                    if ((fgList[e.Row, "Aktion"] + "") == "BUY") e.Style = csBuy;
                    else e.Style = csSell;

                if (e.Col == 16 || e.Col == 17 || e.Col == 18)
                    if ((fgList[e.Row, e.Col] + "") != "")
                        if ((fgList[e.Row, e.Col] + "") != "0")
                            if ((fgList[e.Row, "Aktion"] + "") == "BUY") e.Style = csBuy;
                            else e.Style = csSell;

                if (e.Col == 23)
                    if ((fgList[e.Row, "SendCheck"] + "") == "1") e.Style = csChecked;                           // 60 - SendCheck
            }
        }
        private void fgList_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Row > 1)  {
                if (e.Col == 49)  {                                                                              // 49 - Status
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
        private void picRecieveVoiceFilePath_Click(object sender, EventArgs e)
        {

        }
        private void picRecieveVoiceShow_Click(object sender, EventArgs e)
        {

        }
        private void btnAgree_Click(object sender, EventArgs e)
        {
            if (txtPre_Quantity.Text != "")
            {
                clsInvestIdees_Commands InvestIdees_Commands = new clsInvestIdees_Commands();

                try
                {
                    i = fgPreOrders.Row;
                    iID = SaveTransaction(iBusinessType_Param, iCommandType_Param, Convert.ToInt32(fgPreOrders[i, "Client_ID"]), fgPreOrders[i, "Code"] + "", fgPreOrders[i, "Portfolio"] + "",
                                    Convert.ToInt32(fgPreOrders[i, "Contract_ID"]), Convert.ToInt32(fgPreOrders[i, "Contract_Details_ID"]), Convert.ToInt32(fgPreOrders[i, "Contract_Packages_ID"]),
                                    fgPreOrders[i, "ClientName"] + "", fgPreOrders[i, "Aktion"] + "", DateTime.Now, Convert.ToInt32(fgPreOrders[i, "Product_ID"]),
                                    Convert.ToInt32(fgPreOrders[i, "ProductCategories_ID"]), Convert.ToInt32(fgPreOrders[i, "Share_ID"]), fgPreOrders[i, "Share_Code"] + "",
                                    fgPreOrders[i, "Share_Title"] + "", fgPreOrders[i, "Quantity"] + "", Convert.ToInt32(fgPreOrders[i, "PriceType"]), fgPreOrders[i, "Price"] + "", txtPre_PriceUp.Text,
                                    txtPre_PriceDown.Text, txtPre_Amount.Text, fgPreOrders[i, "Currency"] + "", cmbPre_Constant.SelectedIndex, dPre_Constant.Value,
                                    Convert.ToInt32(fgPreOrders[i, "Provider_ID"]), Convert.ToInt32(fgPreOrders[i, "StockExchange_ID"]), fgPreOrders[i, "StockExchange_Code"] + "", 0, "", "", "", 0);

                    EmptyCommand();

                    if (iID > 0) {
                        sFileName = Path.GetFileName(txtPre_RecieveVoicePath.Text);
                        if (sFileName.Length > 0)
                            sFileName = Global.DMS_UploadFile(txtPre_RecieveVoicePath.Text, "Customers/" + lblPre_ContractTitle.Text.Replace(".", "_") + "/InvestProposals/" + fgPreOrders[fgPreOrders.Row, 0],
                                                   sFileName);
                        sFileName = Path.GetFileName(sFileName);

                        InvestIdees_Commands.Record_ID = Convert.ToInt32(fgPreOrders[fgPreOrders.Row, 24]);
                        InvestIdees_Commands.GetRecord();
                        InvestIdees_Commands.Command_ID = iID;
                        InvestIdees_Commands.RecieveDate = DateTime.Now;
                        InvestIdees_Commands.Status = 5;                        // 1-New, 2-Skeptikos, 3-Wait, 4-Mi apodoxi, 5-Apodoxi, 6-Cancel
                        InvestIdees_Commands.RTO_Notes = txtPre_RTONotes.Text;
                        InvestIdees_Commands.RecieveVoicePath = sFileName;
                        InvestIdees_Commands.EditStatus();
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

                DefineList();
                Empty_PreOrder();
            }
            else MessageBox.Show(Global.GetLabel("wrong_amount"), Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            panPreOrders.Visible = false;
        }
        private void btnThinks_Click(object sender, EventArgs e)
        {
            sFileName = Path.GetFileName(txtPre_RecieveVoicePath.Text);
            sFileName = Global.DMS_UploadFile(txtPre_RecieveVoicePath.Text, "Customers/" + lblPre_ContractTitle.Text.Replace(".", "_") + "/InvestProposals/" + fgPreOrders[fgPreOrders.Row, 0],
                                       sFileName);
            sFileName = Path.GetFileName(sFileName);

            clsInvestIdees_Commands InvestIdees_Command = new clsInvestIdees_Commands();
            InvestIdees_Command.Record_ID = Convert.ToInt32(fgPreOrders[fgPreOrders.Row, 24]);
            InvestIdees_Command.GetRecord();
            InvestIdees_Command.Command_ID = 0;
            InvestIdees_Command.RecieveDate = DateTime.Now;
            InvestIdees_Command.Status = 2;                                                   // 1-New, 2-Skeptikos, 3-Wait, 4-Mi apodoxi, 5-Apodoxi, 6-Cancel
            InvestIdees_Command.RTO_Notes = txtPre_RTONotes.Text;
            InvestIdees_Command.RecieveVoicePath = sFileName;
            InvestIdees_Command.EditRecord();

            fgPreOrders[fgPreOrders.Row, 20] = Global.GetLabel("pensive");
            fgPreOrders[fgPreOrders.Row, 21] = txtPre_RTONotes.Text;
            fgPreOrders[fgPreOrders.Row, 43] = 2;
        }
        private void btnWait_Click(object sender, EventArgs e)
        {
            sFileName = "";

            clsInvestIdees_Commands InvestIdees_Command = new clsInvestIdees_Commands();
            InvestIdees_Command.Record_ID = Convert.ToInt32(fgPreOrders[fgPreOrders.Row, 24]);
            InvestIdees_Command.GetRecord();
            InvestIdees_Command.Command_ID = 0;
            InvestIdees_Command.RecieveDate = DateTime.Now;
            InvestIdees_Command.Status = 3;                                                   // 1-New, 2-Skeptikos, 3-Wait, 4-Mi apodoxi, 5-Apodoxi, 6-Cancel
            InvestIdees_Command.RTO_Notes = txtPre_RTONotes.Text;
            InvestIdees_Command.RecieveVoicePath = sFileName;
            InvestIdees_Command.EditRecord();

            fgPreOrders[fgPreOrders.Row, 20] = Global.GetLabel("pensive");
            fgPreOrders[fgPreOrders.Row, 21] = txtPre_RTONotes.Text;
            fgPreOrders[fgPreOrders.Row, 43] = 3;
        }
        private void btnNotAgree_Click(object sender, EventArgs e)
        {
            sFileName = Path.GetFileName(txtPre_RecieveVoicePath.Text);
            sFileName = Global.DMS_UploadFile(txtPre_RecieveVoicePath.Text, "Customers/" + lblPre_ContractTitle.Text.Replace(".", "_") + "/InvestProposals/" + fgPreOrders[fgPreOrders.Row, 0],
                                       sFileName);
            sFileName = Path.GetFileName(sFileName);

            clsInvestIdees_Commands InvestIdees_Command = new clsInvestIdees_Commands();
            InvestIdees_Command.Record_ID = Convert.ToInt32(fgPreOrders[fgPreOrders.Row, 24]);
            InvestIdees_Command.GetRecord();
            InvestIdees_Command.Command_ID = 0;
            InvestIdees_Command.RecieveDate = DateTime.Now;
            InvestIdees_Command.Status = 4;                                                   // 1-New, 2-Skeptikos, 3-Wait, 4-Mi apodoxi, 5-Apodoxi, 6-Cancel
            InvestIdees_Command.RTO_Notes = txtPre_RTONotes.Text;
            InvestIdees_Command.RecieveVoicePath = sFileName;
            InvestIdees_Command.EditRecord();

            fgPreOrders.RemoveItem(fgPreOrders.Row);
            Empty_PreOrder();
            panPreOrders.Visible = false;
        }
        private void Empty_PreOrder() {
            lblPre_II_ID.Text = "";
            lblPre_ContractTitle.Text = "";
            lblPre_Code.Text = "";
            lblPre_Subcode.Text = "";
            lblPre_Action.Text = "";
            cmbPre_Constant.SelectedIndex = 0;
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
            txtPre_Notes.Text = "";
            lblPre_Tel.Text = "";
            lblPre_Mobile.Text = "";
            panButtons.Enabled = false;
        }
        private void fgPreOrders_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                fgPreOrders.ContextMenuStrip = mnuPreContext;
                fgPreOrders.Row = fgPreOrders.MouseRow;
            }
        }
        private void picPreOrders_Click(object sender, EventArgs e)
        {
            panPreOrders.Visible = false;
        }
        public void DefinePreOrdersList()
        {
            int i = 0, j = 0;

            sInvPropNotesFlag = "";
            sDPMNotesFlag = "";
            fgPreOrders.Redraw = false;
            fgPreOrders.Rows.Count = 1;
            clsInvestIdees klsInvestIdees = new clsInvestIdees();
            klsInvestIdees.AktionDate = dFrom_Param;
            klsInvestIdees.Client_ID = iPreClient_ID;
            klsInvestIdees.Code = sPreCode;
            klsInvestIdees.ISIN = sPreISIN;
            klsInvestIdees.Advisor_ID = iAdvisor_ID;
            klsInvestIdees.GetList_NonRecieved();
            foreach (DataRow dtRow in klsInvestIdees.List.Rows) {
                if ((dtRow["ClientFullName"]+"").ToUpper().IndexOf(txtFilter.Text.ToUpper()) >= 0 || (dtRow["ContractTitle"] + "").ToUpper().IndexOf(txtFilter.Text.ToUpper()) >= 0) {
                    if (Convert.ToDateTime(dtRow["RTODate"]) != Convert.ToDateTime("1900/01/01"))
                        if ((dtRow["StatusTitle"]+"") == "") sInvPropNotesFlag = "*";
                    else dtRow["RTODate"] = "";

                    i = i + 1;

                    fgPreOrders.AddItem(dtRow["II_ID"] + "\t" + dtRow["ClientFullName"] + "\t" + dtRow["ContractTitle"] + "\t" + dtRow["ServiceProviders_Title"] + "\t" +
                                   dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" + dtRow["Aktion"] + "\t" + dtRow["Products_Title"] + "/" + dtRow["Products_Categories_Title"] + "\t" +
                                   dtRow["ShareTitle"] + "\t" + dtRow["ISIN"] + "\t" + dtRow["ShareCode"] + "\t" + 
                                   (Convert.ToInt32(dtRow["PriceType"]) == 0? dtRow["Price"]: sPriceType[Convert.ToInt32(dtRow["PriceType"])]) + "\t" +
                                   dtRow["Quantity"] + "\t" + dtRow["Amount"] + "\t" + dtRow["Curr"] + "\t" + sConstant[Convert.ToInt16(dtRow["Constant"])].Trim() + "\t" +
                                   dtRow["StockExchanges_Title"] + "\t" + dtRow["DateIns"] + "\t" + dtRow["RTODate"] + "\t" + dtRow["Notes"] + "\t" +
                                   dtRow["StatusTitle"] + "\t" + dtRow["RTO_Notes"] + "\t" + dtRow["Advisor_Name"] + "\t" + dtRow["Author_Name"] + "\t" +
                                   dtRow["ID"] + "\t" + dtRow["Client_ID"] + "\t" + dtRow["StockCompany_ID"] + "\t" + dtRow["ConfirmationStatus"] + "\t" + dtRow["Share_ID"] + "\t" +
                                   dtRow["Contract_ID"] + "\t" + dtRow["Product_ID"] + "\t" + dtRow["ProductCategory_ID"] + "\t" + dtRow["StockExchange_ID"] + "\t" +
                                   dtRow["PriceType"] + "\t" + dtRow["PriceUP"] + "\t" + dtRow["PriceDown"] + "\t" + dtRow["Tel"] + "\t" + dtRow["Mobile"] + "\t" +
                                   dtRow["Advisor_ID"] + "\t" + dtRow["Author_ID"] + "\t" + dtRow["ConstantDate"] + "\t" + dtRow["ShareCode2"] + "\t" + dtRow["ProviderType"] + "\t" +
                                   dtRow["Status"] + "\t" + "" + "\t" + dtRow["Contract_Details_ID"] + "\t" + dtRow["Contract_Packages_ID"] + "\t" + dtRow["Client_Type"]);

                }
            }

            fgPreOrders.Row = 0;
            fgPreOrders.Redraw = true;

            clsOrdersSecurity klsOrder = new clsOrdersSecurity();
            klsOrder.CommandType_ID = 4;                                                   //  4 - DPM Orders tou diaxiristi
            klsOrder.DateFrom = dFrom_Param;
            klsOrder.DateTo = dTo_Param;
            klsOrder.User_ID = 0;
            klsOrder.GetDPMList();
            foreach (DataRow dtRow in klsOrder.List.Rows)
            {
                if (iProvider_ID_Param == 0 || Convert.ToInt32(dtRow["StockCompany_ID"]) == iProvider_ID_Param) {
                    if (Convert.ToInt32(dtRow["Depository_ID"]) == 2) {                   // it's not Depository_ID - it's flag that order was sent to RTO (2)
                     j = j + 1;
                    }
                }
            }

            tslPreOrders.Text = "Επενδυτικές Συμβουλές: " + i + " " + sInvPropNotesFlag;
            tslDPMOrders.Text = "DPM Orders: " + j + " " + sDPMNotesFlag;

            panPreOrders.Left = (Screen.PrimaryScreen.Bounds.Width - panPreOrders.Width) / 2;
            panPreOrders.Top = (Screen.PrimaryScreen.Bounds.Height - panPreOrders.Height) / 2;
        }
        private void mnuPreClientData_Click(object sender, EventArgs e)
        {
            frmClientData locClientData = new frmClientData();
            locClientData.Client_ID = Convert.ToInt32(fgPreOrders[fgPreOrders.Row, 25]);
            locClientData.Text = Global.GetLabel("customer_information");
            locClientData.Show();
        }
        private void mnuPreContractData_Click(object sender, EventArgs e)
        {
            frmContract locContract = new frmContract();
            locContract.Aktion = 1;
            locContract.Contract_ID = Convert.ToInt32(fgPreOrders[fgPreOrders.Row, 29]);
            locContract.Contract_Details_ID = Convert.ToInt32(fgPreOrders[fgPreOrders.Row, 45]);
            locContract.Contract_Packages_ID = Convert.ToInt32(fgPreOrders[fgPreOrders.Row, 46]);
            locContract.Client_ID = Convert.ToInt32(fgPreOrders[fgPreOrders.Row, 25]);
            locContract.ClientType = Convert.ToInt32(fgPreOrders[fgPreOrders.Row, 47]);
            locContract.ClientFullName = fgPreOrders[fgPreOrders.Row, 1] + "";
            locContract.RightsLevel = iRightsLevel;
            locContract.ShowDialog();
        }
        private void mnuPreInvestProposals_Click(object sender, EventArgs e)
        {
            frmInvestProposal locInvestProposal_Rec = new frmInvestProposal();
            locInvestProposal_Rec.Aktion = 1;              // 0 - Edit
            locInvestProposal_Rec.II_ID = Convert.ToInt32(fgPreOrders[fgPreOrders.Row, 0]);
            locInvestProposal_Rec.ShowDialog();
        }
        private void mnuPreFilterClient_Click(object sender, EventArgs e)
        {
            iPreClient_ID = Convert.ToInt32(fgPreOrders[fgPreOrders.Row, 25]);
            DefinePreOrdersList();
        }

        private void mnuPreFilterClientCode_Click(object sender, EventArgs e)
        {
            sPreCode = fgPreOrders[fgPreOrders.Row, 4] + "";
            DefinePreOrdersList();
        }
        private void mnuPreFilterISIN_Click(object sender, EventArgs e)
        {
            sPreISIN = fgPreOrders[fgPreOrders.Row, 9] + "";
            DefinePreOrdersList();
        }
        private void mnuPreFilterAdvisor_Click(object sender, EventArgs e)
        {
            iAdvisor_ID = Convert.ToInt32(fgPreOrders[fgPreOrders.Row, 38]);
            DefinePreOrdersList();
        }
        private void mnuPreNoFilters_Click(object sender, EventArgs e)
        {
            iPreClient_ID = 0;
            iAdvisor_ID = 0;
            sPreCode = "";
            sPreISIN = "";
            DefinePreOrdersList();
        }
        private void mnuPreCopyISIN_Click(object sender, EventArgs e)
        {
            if (fgPreOrders.Row >= 1)
                Clipboard.SetDataObject(fgPreOrders[fgPreOrders.Row, 9], true, 10, 100);
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

                foreach (Control parControl in this.Controls) {
                    switch (parControl.Name) {
                        case "lnkPelatis":
                            parControl.Text = fgList[fgList.Row, 4] + "";
                            break;
                        case "lblCode":
                            parControl.Text = fgList[fgList.Row, 6] + "";
                            break;
                        case "lnkPortfolio":
                            parControl.Text = fgList[fgList.Row, 7] + "";
                            break;
                        case "lblClient_ID":
                            parControl.Text = fgList[fgList.Row, 44] + "";
                            break;
                        case "lblClientPackage_ID":
                            parControl.Text = fgList[fgList.Row, 50] + "";
                            break;
                        case "lblProvider_ID":
                            parControl.Text = fgList[fgList.Row, 45] + "";
                            break;
                        case "lblBusinessType_ID":
                            parControl.Text = fgList[fgList.Row, 52] + "";
                            break;
                        case "lblContract_Details_ID":
                            parControl.Text = fgList[fgList.Row, 66] + "";
                            break;
                        case "lblContract_Packages_ID":
                            parControl.Text = fgList[fgList.Row, 67] + "";
                            break;
                    }
                }

                sCodes_Param = fgList[fgList.Row, 6] + "";
                sTemp = fgList[fgList.Row, 7] + "";
                iClient_ID = Convert.ToInt32(fgList[fgList.Row, 44]);
                iProvider_ID = Convert.ToInt32(fgList[fgList.Row, 45]);
                iBusinessType_Param = Convert.ToInt32(fgList[fgList.Row, 52]);

                DefineList();
            }
        }
        private void mnuCopyISIN_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 1) Clipboard.SetDataObject(fgList[fgList.Row, "ISIN"], true, 10, 100);
        }
        private void mnuShowFile_Click(object sender, EventArgs e)
        {

        }
        private void XXXToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion
       
        public int RightsLevel { get { return iRightsLevel; } set { iRightsLevel = value; } }
        public string Extra { get { return sExtra; } set { sExtra = value; } }
    }
}
