using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using C1.Win.C1FlexGrid;
using Core;

namespace Transactions
{
    public partial class frmDailyFX : Form
    {
        DataTable dtAccsFrom, dtAccsTo, dtEURRates;
        DataView dtView;        
        int i, iMode, iOld_ID, iBusinessType_ID, iCommandType_ID, iCommands_ID, iClient_ID, iContract_ID, iContract_Details_ID, iContract_Packages_ID, 
            iRightsLevel, iRow, iProvider_ID, iStockExchange_ID;
        string sProviderTitle, sExtra, sBulkCommand, sRealCashAccount_From, sRealAmountFrom, sRealCurrFrom, sRealCashAccount_To, sRealAmountTo, sRealCurrTo, sExecuteDate, sFileName, sUploadFile;
        string[] sConstant = { "Day Order", "GTC", "GTDate" };
        string[] sPriceType = { "Spot Rate", "Limit" };
        bool bCheckList, bFilter;
        Hashtable imgMap = new Hashtable();
        CellRange rng;
        CellStyle csCancel, csExecute, csBuy, csSell, csChecked, csThinks, csWait;
        clsOrdersFX_Recieved OrdersFX_Recieved = new clsOrdersFX_Recieved();
        public frmDailyFX()
        {
            InitializeComponent();

            panDaily.Left = 4;
            panDaily.Top = 34;
            panDaily.Visible = true;

            panSearch.Left = 4;
            panSearch.Top = 34;
            panSearch.Visible = false;

            panFilters.Top = 82;
            panFilters.Left = 948;
            panFilters.Width = 610;
            panFilters.Height = 100;

            panMultiProducts.Left = 6;
            panMultiProducts.Top = 144;

            csCancel = fgList.Styles.Add("Cancelled");
            csCancel.ForeColor = Color.Red;

            csExecute = fgList.Styles.Add("Execute");
            csExecute.BackColor = Color.Gold;

            csBuy = fgList.Styles.Add("Buy");
            csBuy.BackColor = Color.MediumAquamarine;
            csBuy.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

            csSell = fgList.Styles.Add("Sell");
            csSell.BackColor = Color.LightCoral;
            csSell.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

            csChecked = fgList.Styles.Add("Checked");
            csChecked.BackColor = Color.Yellow;

            csThinks = fgList.Styles.Add("Thinks");
            csThinks.BackColor = Color.Yellow;

            csWait = fgList.Styles.Add("Wait");
            csWait.BackColor = Color.LightSeaGreen;
        }
        private void frmDailyFX_Load(object sender, EventArgs e)
        {
            bCheckList = false;
            dToday.Value = DateTime.Now;

            switch (iMode)
            {
                case 1:
                case 3:
                    panDaily.Visible = true;
                    panSearch.Visible = false;

                    panFilters.Width = 616;
                    panFilters.Height = 100;

                    ucCS.Left = 68;
                    ucCS.Top = 34;
                    ucCS.StartInit(700, 400, 500, 20, 1);
                    ucCS.TextOfLabelChanged += new EventHandler(ucCS_TextChanged);
                    ucCS.Filters = "Status = 1 And Contract_ID > 0";
                    ucCS.ListType = 2;

                    dToday.Value = DateTime.Now;
                    break;
                case 2:
                    panDaily.Visible = false;
                    panSearch.Visible = true;

                    panFilters.Width = 762;
                    panFilters.Height = 100;

                    ucCS.Left = 72;
                    ucCS.Top = 62;
                    ucCS.StartInit(700, 400, 440, 20, 1);
                    ucCS.TextOfLabelChanged += new EventHandler(ucCS_TextChanged);
                    ucCS.Filters = "Status = 1 And Contract_ID > 0";
                    ucCS.ListType = 2;

                    ucDC.DateFrom = DateTime.Now;
                    ucDC.DateTo = DateTime.Now;
                    break;
            }

            this.Text = "Εντολόχαρτο FX";
            lblContract.Text = Global.GetLabel("contract");
            lblCustomer.Text = Global.GetLabel("__b45");
            lblProvider.Text = Global.GetLabel("provider");
            lblAdvisor.Text = Global.GetLabel("advisor");
            lblSender.Text = Global.GetLabel("transmitter");
            lblSended.Text = Global.GetLabel("transmission");
            lblExecute.Text = Global.GetLabel("execution");
            
            iBusinessType_ID = 1;                               // 1 - RTO (HF), 2 - Custody (HFSS)
            iCommandType_ID = 1;                                // 1 - Simple Order, 2 - Execution Order, 3 - Bulk Order, 4 - DPM Order
            cmbType.SelectedIndex = 0;
            cmbConstant.SelectedIndex = 0;
            iStockExchange_ID = 0;
            lblFXPrice.Visible = false;
            txtRate.Visible = false;
            lblFXCurr.Visible = false;

            //-------------- Define cmbRecievedMethods List ------------------
            cmbRecieveMethod2.DataSource = Global.dtRecieveMethods.Copy();
            cmbRecieveMethod2.DisplayMember = "Title";
            cmbRecieveMethod2.ValueMember = "ID";
            cmbRecieveMethod2.SelectedValue = 0;

            //-------------- Define ServiceProviders List -----------------
            dtView = Global.dtServiceProviders.Copy().DefaultView;
            dtView.RowFilter = "Aktive = 1";
            cmbProviders.DataSource = dtView;
            cmbProviders.DisplayMember = "Title";
            cmbProviders.ValueMember = "ID";
            cmbProviders.SelectedValue = 0;

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
            cmbUsers.SelectedValue = 0;

            //-------------- Define Diaxeiristis List ------------------
            dtView = Global.dtUserList.Copy().DefaultView;
            dtView.RowFilter = "Diaxiristis = 1";
            cmbDiax.DataSource = dtView;
            cmbDiax.DisplayMember = "Title";
            cmbDiax.ValueMember = "ID";

            cmbSent.SelectedIndex = 0;
            cmbActions.SelectedIndex = 0;

            //-------------- Define Currencies List ------------------
            cmbCurrFrom.DataSource = Global.dtCurrencies.Copy();
            cmbCurrFrom.DisplayMember = "Title";
            cmbCurrFrom.ValueMember = "ID";

            cmbCurrTo.DataSource = Global.dtCurrencies.Copy();
            cmbCurrTo.DisplayMember = "Title";
            cmbCurrTo.ValueMember = "ID";

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgList.Click += new System.EventHandler(fgList_Click);
            fgList.DoubleClick += new System.EventHandler(fgList_DoubleClick);
            fgList.MouseDown += new MouseEventHandler(fgList_MouseDown);
            fgList.OwnerDrawCell += fgList_OwnerDrawCell;
            fgList.CellChanged += fgList_CellChanged;

            fgList.DrawMode = DrawModeEnum.OwnerDraw;
            fgList.ShowCellLabels = true;

            fgList.Styles.Normal.WordWrap = true;
            fgList.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgList.Rows[0].AllowMerging = true;
            rng = fgList.GetCellRange(0, 0, 1, 0);
            rng.Data = "";

            fgList.Cols[2].AllowMerging = true;
            rng = fgList.GetCellRange(0, 2, 1, 2);
            rng.Data = "Bulk N";

            fgList.Cols[3].AllowMerging = true;
            rng = fgList.GetCellRange(0, 3, 1, 3);
            rng.Data = "Εντολέας";   //Global.GetLabel("customer_name");

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

            rng = fgList.GetCellRange(0, 8, 0, 10);
            rng.Data = "Χρέωση";

            fgList[1, 8] = Global.GetLabel("cash_account");
            fgList[1, 9] = Global.GetLabel("amount");
            fgList[1, 10] = Global.GetLabel("currency");

            rng = fgList.GetCellRange(0, 11, 0, 12);
            rng.Data = "Πίστωση";

            fgList[1, 11] = Global.GetLabel("cash_account");
            fgList[1, 12] = Global.GetLabel("amount");
            fgList[1, 13] = Global.GetLabel("currency");

            fgList.Cols[14].AllowMerging = true;
            rng = fgList.GetCellRange(0, 14, 1, 14);
            rng.Data = "Τύπος Τιμής";

            fgList.Cols[15].AllowMerging = true;
            rng = fgList.GetCellRange(0, 15, 1, 15);
            rng.Data = "Διάρκεια";

            rng = fgList.GetCellRange(0, 16, 0, 18);
            rng.Data = "Εκτελεσμένη Χρέωση";

            fgList[1, 16] = Global.GetLabel("cash_account");
            fgList[1, 17] = Global.GetLabel("amount");
            fgList[1, 18] = Global.GetLabel("currency");

            rng = fgList.GetCellRange(0, 19, 0, 21);
            rng.Data = "Εκτελεσμένη Πίστωση"; ;

            fgList[1, 19] = Global.GetLabel("cash_account");
            fgList[1, 20] = Global.GetLabel("amount");
            fgList[1, 21] = Global.GetLabel("currency");

            fgList.Cols[22].AllowMerging = true;
            rng = fgList.GetCellRange(0, 22, 1, 22);
            rng.Data = Global.GetLabel("rate");

            fgList.Cols[23].AllowMerging = true;
            rng = fgList.GetCellRange(0, 23, 1, 23);
            rng.Data = Global.GetLabel("stock_exchange");

            fgList.Cols[24].AllowMerging = true;
            rng = fgList.GetCellRange(0, 24, 1, 24);
            rng.Data = Global.GetLabel("receipt_time");

            fgList.Cols[25].AllowMerging = true;
            rng = fgList.GetCellRange(0, 25, 1, 25);
            rng.Data = Global.GetLabel("transmission_time");

            fgList.Cols[26].AllowMerging = true;
            rng = fgList.GetCellRange(0, 26, 1, 26);
            rng.Data = Global.GetLabel("execution_date");

            fgList.Cols[27].AllowMerging = true;
            rng = fgList.GetCellRange(0, 27, 1, 27);
            rng.Data = Global.GetLabel("receipt_way");

            fgList.Cols[28].AllowMerging = true;
            rng = fgList.GetCellRange(0, 28, 1, 28);
            rng.Data = Global.GetLabel("informing_ways");

            fgList.Cols[29].AllowMerging = true;
            rng = fgList.GetCellRange(0, 29, 1, 29);
            rng.Data = Global.GetLabel("coment");

            fgList.Cols[30].AllowMerging = true;
            rng = fgList.GetCellRange(0, 30, 1, 30);
            rng.Data = Global.GetLabel("transmitter");

            fgList.Cols[31].AllowMerging = true;
            rng = fgList.GetCellRange(0, 31, 1, 31);
            rng.Data = Global.GetLabel("advisor");

            rng = fgList.GetCellRange(0, 32, 0, 33);
            rng.Data = Global.GetLabel("commissions");

            fgList[1, 32] = Global.GetLabel("percent");
            fgList[1, 33] = Global.GetLabel("amount");

            fgList.Styles.Fixed.TextAlign = TextAlignEnum.CenterCenter;

            for (i = 0; i < imgFiles.Images.Count; i++) imgMap.Add(i, imgFiles.Images[i]);

            Column clm0 = fgList.Cols["image_map"];
            clm0.ImageMap = imgMap;
            clm0.ImageAndText = false;
            clm0.ImageAlign = ImageAlignEnum.CenterCenter;

            ShowBusinessType();
            ShowList();
            bCheckList = true;

            if (iMode == 1) DefineList();
            if (iMode == 3)  {
                clsOrdersSecurity Orders = new clsOrdersSecurity();
                Orders.Record_ID = iCommands_ID;
                Orders.GetRecord();

                ucCS.ShowClientsList = false;
                ucCS.txtContractTitle.Text = Orders.ContractTitle;
                ucCS.Contract_ID.Text = Orders.Contract_ID.ToString();
                ucCS.ShowClientsList = true;

                lnkPelatis.Text = Orders.ClientName;
                lblCode.Text = Orders.Code;
                lnkPortfolio.Text = Orders.ProfitCenter;
                iContract_ID = Orders.Contract_ID;
                iContract_Details_ID = Orders.Contract_Details_ID;
                iContract_Packages_ID = Orders.Contract_Packages_ID;
                iClient_ID = Orders.Client_ID;
                iProvider_ID = Orders.ServiceProvider_ID;
                iBusinessType_ID = Orders.BusinessType_ID;

                //foundRows = Global.dtServiceProviders.Select("ID = " + iProvider_ID);
                //if (foundRows.Length > 0)
                //    iProviderType = Convert.ToInt32(foundRows[0]["ProviderType"]);

                DefineList();
            };

            clsContracts_CashAccounts ClientCashAccounts = new clsContracts_CashAccounts();
            ClientCashAccounts.Client_ID = 0;
            ClientCashAccounts.Code = lblCode.Text;
            ClientCashAccounts.Contract_ID = 0; // iContract_ID;
            ClientCashAccounts.GetList_CashAccount();

            dtAccsFrom = ClientCashAccounts.List;
            dtAccsTo = ClientCashAccounts.List;
        }
        protected override void OnResize(EventArgs e)
        {
            tcBusinessTypes.Width = this.Width - 25;

            fgList.Width = this.Width - 26;
            fgList.Height = this.Height - 232;
        }

        private void dToday_ValueChanged(object sender, EventArgs e)
        {
            DefineList();
        }

        private void mnuClientData_Click(object sender, EventArgs e)
        {
            frmClientData locClientData = new frmClientData();
            locClientData.Client_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
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

        private void lnkPelatis_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmClientData locClientData = new frmClientData();
            locClientData.Client_ID = iClient_ID;
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
            locContract.ClientFullName = fgList[fgList.Row, "ClientName"] + "";
            locContract.RightsLevel = iRightsLevel;
            locContract.ShowDialog();
        }

        private void mnuNewCommand_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 0) {

                ucCS.ShowClientsList = false;
                ucCS.txtContractTitle.Text = fgList[fgList.Row, "ContractTitle"] + "";
                ucCS.ShowClientsList = true;
                lnkPelatis.Text = fgList[fgList.Row, "ClientName"] + "";
                lblCode.Text = fgList[fgList.Row, "Code"] + "";
                lnkPortfolio.Text = fgList[fgList.Row, "Portfolio"] + "";
                iContract_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_ID"]);
                iContract_Details_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_Details_ID"]);
                iContract_Packages_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_Packages_ID"]);
                iClient_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
                iProvider_ID = Convert.ToInt32(fgList[fgList.Row, "Provider_ID"]);
                iBusinessType_ID = Convert.ToInt32(fgList[fgList.Row, "BusinessType_ID"]);

                //foundRows = Global.dtServiceProviders.Select("ID = " + iProvider_ID);
                //if (foundRows.Length > 0)
                //    iProviderType = Convert.ToInt32(foundRows[0]["ProviderType"]);

                DefineList();

                clsContracts_CashAccounts ClientCashAccounts = new clsContracts_CashAccounts();
                ClientCashAccounts.Client_ID = 0;
                ClientCashAccounts.Code = lblCode.Text;
                ClientCashAccounts.Contract_ID = 0;     // iContract_ID;
                ClientCashAccounts.GetList_CashAccount();

                dtAccsFrom = ClientCashAccounts.List;
                dtAccsTo = ClientCashAccounts.List;
            }
        }

        private void tcBusinessTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmptyData();
            fgCommandBuffer.Rows.Count = 1;
            panMultiProducts.Visible = false;
            ucCS.Filters = "Status = 1";

            switch (Convert.ToInt32(tcBusinessTypes.SelectedIndex))
            {
                case 0:                                                          // "tpRTO":
                    iBusinessType_ID = 1;
                    iCommandType_ID = 1;
                    ucCS.ListType = 2;
                    ucCS.Visible = true;
                    ShowBusinessType();
                    ShowList();
                    break;
                case 1:                                                            // "tpDPM":
                    ucCS.Filters = "Status = 1 AND Service_ID = 3 AND User4_ID = " + Global.User_ID;
                    bCheckList = true;
                    iBusinessType_ID = 1;
                    iCommandType_ID = 4;
                    ucCS.ListType = 2;
                    ucCS.Visible = true;
                    ShowBusinessType();
                    ShowList();
                    break;
                case 2:                                                              //   "tpBulk":
                    iBusinessType_ID = 1;
                    ucCS.ListType = 3;
                    ucCS.Visible = true;
                    iCommandType_ID = 3;
                    ShowBusinessType();
                    ShowList();
                    break;
                case 3:                                                            //  "tpExecution":
                    iBusinessType_ID = 2;
                    iCommandType_ID = 2;
                    ucCS.ListType = 2;
                    ucCS.Visible = true;
                    ShowBusinessType();
                    ShowList();
                    break;
            }
        }

        private void picRecieveVoicePath_Click(object sender, EventArgs e)
        {
            txtRecieveVoicePath.Text = Global.FileChoice(Global.DefaultFolder);
        }

        private void picCloseCommandBuffer_Click(object sender, EventArgs e)
        {
            panMultiProducts.Visible = false;
        }

        private void btnAddCommand_Click(object sender, EventArgs e)
        {
            cmbRecieveMethod2.SelectedValue = 0;
            txtRecieveVoicePath.Text = "";

            i = fgCommandBuffer.Rows.Count;
            fgCommandBuffer.AddItem(i + "\t" + cmbCurrFrom.Text + "\t" + cmbCashAccFrom.Text + "\t" + txtAmountFrom.Text + "\t" + cmbCurrTo.Text + "\t" +
                                    cmbCashAccTo.Text + "\t" + txtAmountTo.Text + "\t" + cmbType.Text + "\t" + txtRate.Text + "\t" + cmbConstant.Text + "\t" + 
                                    cmbCashAccFrom.SelectedValue + "\t" + cmbCashAccTo.SelectedValue + "\t" + cmbType.SelectedIndex + "\t" + 
                                    cmbConstant.SelectedIndex + "\t" + dConstant.Value.ToString("dd/MM/yyyy"), 1);
            EmptyFXData();
            cmbConstant.SelectedIndex = 0;
            cmbRecieveMethod2.SelectedValue = 0;
            panMultiProducts.Visible = true;

            cmbCurrFrom.Focus();
        }

        private void ShowList()
        {
            if (bCheckList && (iMode == 1 || iMode ==3))
            {
                if (dToday.Value.Date == DateTime.Now.Date) dtEURRates = Global.dtTodayEURRates.Copy();
                else
                {
                    clsCurrencies klsCurrency = new clsCurrencies();
                    klsCurrency.DateFrom = dToday.Value.AddDays(-1);
                    klsCurrency.DateTo = dToday.Value.AddDays(-1);
                    klsCurrency.Code = "EUR";
                    klsCurrency.GetCurrencyRates_Period();
                    dtEURRates = klsCurrency.List.Copy();
                }
                toolLeft.Visible = true;

                DefineList();
            }
        }

        private void cmbProviders_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) {
                iProvider_ID = Convert.ToInt32(cmbProviders.SelectedValue);
                ShowList();
            }
        }
        private void cmbActions_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList)  ShowList();
        }
        private void cmbAdvisors_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) ShowList();
        }

        private void cmbDivisions_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) ShowList();
        }
        private void cmbUsers_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) ShowList();
        }

        private void cmbSent_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) ShowList();
        }

        private void cmbDiax_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) ShowList();
        }
        private void cmbCurrFrom_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) {
                if (dtAccsFrom.Rows.Count > 0) {
                    dtView = dtAccsFrom.Copy().DefaultView;
                    dtView.RowFilter = "Currency = '' OR Currency = '" + cmbCurrFrom.Text + "'";
                    cmbCashAccFrom.DataSource = dtView;
                    cmbCashAccFrom.DisplayMember = "AccountNumber";
                    cmbCashAccFrom.ValueMember = "ID";
                    if (dtView.Count > 1) cmbCashAccFrom.SelectedIndex = 1;
                }
            }
        }
        private void cmbCurrTo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) {
                if (dtAccsTo.Rows.Count > 0) {
                    dtView = dtAccsTo.Copy().DefaultView;
                    dtView.RowFilter = "Currency = '' OR Currency = '" + cmbCurrTo.Text + "'";
                    cmbCashAccTo.DataSource = dtView;
                    cmbCashAccTo.DisplayMember = "AccountNumber";
                    cmbCashAccTo.ValueMember = "ID";
                    if (dtView.Count > 1) cmbCashAccTo.SelectedIndex = 1;
                }
            }
        }
        private void txtAmountFrom_TextChanged(object sender, EventArgs e)
        {
            if (txtAmountFrom.Text != "") txtAmountTo.Text = "";
        }
        private void txtAmountTo_TextChanged(object sender, EventArgs e)
        {
            if (txtAmountTo.Text != "") txtAmountFrom.Text = "";
        }
        private void cmbFXType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbType.SelectedIndex) == 0) {
                cmbConstant.SelectedIndex = 0;
                dConstant.Visible = false;
                lblFXPrice.Visible = false;
                txtRate.Visible = false;
                lblFXCurr.Visible = false;
            }
            else {
                cmbConstant.SelectedIndex = 0;
                dConstant.Visible = false;
                lblFXPrice.Visible = true;
                txtRate.Visible = true;
                lblFXCurr.Visible = true;
           }
        }
        private void cmbFXConstant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConstant.SelectedIndex == 2) dConstant.Visible = true;
            else dConstant.Visible = false;
        }
        private void tsbBasket_Click(object sender, EventArgs e)
        {
            frmFXBasket locFXBasket = new frmFXBasket();
            locFXBasket.Today = dToday.Value;
            locFXBasket.ShowDialog();
        }
        private void btnCleanUp_Click(object sender, EventArgs e)
        {
            EmptyData();
            DefineList();

            fgCommandBuffer.Rows.Count = 1;
            panMultiProducts.Visible = false;
        }
        private void DefineList()
        {
            if (bCheckList) {
                clsOrdersFX OrdersFX = new clsOrdersFX();
                fgList.Redraw = false;
                fgList.Rows.Count = 2;
                fgList.Cols[27].Visible = true;
                i = 0;
                iOld_ID = -999;
                switch (iCommandType_ID)
                {
                    case 1:
                        OrdersFX.CommandType_ID = 1;
                        OrdersFX.DateFrom = dToday.Value;
                        OrdersFX.DateTo = dToday.Value;
                        OrdersFX.StockCompany_ID = Convert.ToInt32(cmbProviders.SelectedValue);
                        OrdersFX.Actions = Convert.ToInt32(cmbActions.SelectedIndex);
                        OrdersFX.Sent = Convert.ToInt32(cmbSent.SelectedIndex);
                        OrdersFX.User_ID = Convert.ToInt32(cmbUsers.SelectedValue);
                        OrdersFX.User1_ID = Convert.ToInt32(cmbAdvisors.SelectedValue);
                        OrdersFX.User4_ID = Convert.ToInt32(cmbDiax.SelectedValue);
                        OrdersFX.Division_ID = Convert.ToInt32(cmbDivisions.SelectedValue);
                        OrdersFX.Code = lblCode.Text;
                        OrdersFX.GetList();

                        foreach (DataRow dtRow in OrdersFX.List.Rows)
                        {
                            if (iOld_ID != Convert.ToInt32(dtRow["ID"]))
                            {
                                iOld_ID = Convert.ToInt32(dtRow["ID"]);

                                if (Convert.ToDateTime(dtRow["ExecuteDate"]).Date == Convert.ToDateTime("1900/01/01").Date)
                                {
                                    sRealCashAccount_From = "";
                                    sRealAmountFrom = "0";
                                    sRealCurrFrom = "";
                                    sRealCashAccount_To = "";
                                    sRealAmountTo = "0";
                                    sRealCurrTo = "";
                                    sExecuteDate = "";
                                }
                                else
                                {
                                    sRealCashAccount_From = dtRow["RealCashAccount_From"] + "";
                                    sRealAmountFrom = Convert.ToDecimal(dtRow["RealAmountFrom"]).ToString("0.00");
                                    sRealCurrFrom = dtRow["CurrFrom"] + "";
                                    sRealCashAccount_To = dtRow["RealCashAccount_To"] + "";
                                    sRealAmountTo = Convert.ToDecimal(dtRow["RealAmountTo"]).ToString("0.00");
                                    sRealCurrTo = dtRow["CurrTo"] + "";
                                    sExecuteDate = Convert.ToDateTime(dtRow["ExecuteDate"]).ToString("yyyy/MM/dd");
                                }

                                i = i + 1;
                                sBulkCommand = (dtRow["BulkCommand"] + "").Replace("<", "").Replace(">", "");
                                fgList.AddItem("" + "\t" + i + "\t" + sBulkCommand + "\t" + dtRow["ClientName"] + "\t" +
                                               dtRow["ContractTitle"] + "\t" + dtRow["Company_Title"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" +
                                               dtRow["CashAccount_From"] + "\t" + dtRow["AmountFrom"] + "\t" + dtRow["CurrFrom"] + "\t" +
                                               dtRow["CashAccount_To"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["CurrTo"] + "\t" +
                                               sPriceType[Convert.ToInt32(dtRow["Tipos"])] + "\t" + (sConstant[Convert.ToInt16(dtRow["Constant"])] + " " + dtRow["ConstantDate"]).Trim() + "\t" +
                                               sRealCashAccount_From + "\t" + sRealAmountFrom + "\t" + sRealCurrFrom + "\t" +
                                               sRealCashAccount_To + "\t" + sRealAmountTo + "\t" + sRealCurrTo + "\t" +
                                               Convert.ToDecimal(dtRow["RealCurrRate"]).ToString("0.00##") + "\t" + dtRow["StockExchangeTitle"] + "\t" +
                                               ((Convert.ToDateTime(dtRow["RecieveDate"]).Date != Convert.ToDateTime("2070/12/31").Date) ? Convert.ToDateTime(dtRow["RecieveDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                               ((Convert.ToDateTime(dtRow["SentDate"]).Date != Convert.ToDateTime("1900/01/01").Date) ? Convert.ToDateTime(dtRow["SentDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                               sExecuteDate + "\t" +
                                               dtRow["RecieveTitle"] + "\t" + dtRow["InformationTitle"] + "\t" + dtRow["Notes"] + "\t" + dtRow["Author_Fullname"] + "\t" + dtRow["Advisor_Fullname"] + "\t" +
                                               dtRow["FeesPercent"] + "\t" + dtRow["FeesAmount"] + "\t" + dtRow["ID"] + "\t" + dtRow["Client_ID"] + "\t" +
                                               dtRow["StockCompany_ID"] + "\t" + dtRow["Status"] + "\t" + dtRow["Contract_ID"] + "\t" +
                                               dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"] + "\t" + dtRow["BusinessType_ID"] + "\t" + "");

                            }
                        }
                        break;
                    case 2:
                        fgList.Cols[27].Visible = false;

                        OrdersFX.CommandType_ID = 2;
                        OrdersFX.DateFrom = dToday.Value;
                        OrdersFX.DateTo = dToday.Value;
                        OrdersFX.StockCompany_ID = Convert.ToInt32(cmbProviders.SelectedValue);
                        OrdersFX.Actions = Convert.ToInt32(cmbActions.SelectedIndex);
                        OrdersFX.Sent = Convert.ToInt32(cmbSent.SelectedIndex);
                        OrdersFX.User_ID = Convert.ToInt32(cmbUsers.SelectedValue);
                        OrdersFX.User1_ID = Convert.ToInt32(cmbAdvisors.SelectedValue);
                        OrdersFX.User4_ID = Convert.ToInt32(cmbDiax.SelectedValue);
                        OrdersFX.Division_ID = Convert.ToInt32(cmbDivisions.SelectedValue);
                        OrdersFX.Code = lblCode.Text;
                        OrdersFX.GetList();

                        foreach (DataRow dtRow in OrdersFX.List.Rows)
                        {
                            if (iOld_ID != Convert.ToInt32(dtRow["ID"]))
                            {
                                iOld_ID = Convert.ToInt32(dtRow["ID"]);

                                i = i + 1;
                                sBulkCommand = (dtRow["BulkCommand"] + "").Replace("<", "").Replace(">", "");
                                fgList.AddItem("" + "\t" + i + "\t" + sBulkCommand + "\t" + dtRow["ClientName"] + "\t" +
                                               dtRow["ContractTitle"] + "\t" + dtRow["Company_Title"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" +
                                               dtRow["CashAccount_From"] + "\t" + dtRow["AmountFrom"] + "\t" + dtRow["CurrFrom"] + "\t" +
                                               dtRow["CashAccount_To"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["CurrTo"] + "\t" +
                                               sPriceType[Convert.ToInt32(dtRow["Tipos"])] + "\t" + (sConstant[Convert.ToInt16(dtRow["Constant"])] + " " + dtRow["ConstantDate"]).Trim() + "\t" +
                                               dtRow["RealCashAccount_From"] + "\t" + Convert.ToDecimal(dtRow["RealAmountFrom"]).ToString("0.00") + "\t" + dtRow["CurrFrom"] + "\t" +
                                               dtRow["RealCashAccount_To"] + "\t" + Convert.ToDecimal(dtRow["RealAmountTo"]).ToString("0.00") + "\t" + dtRow["CurrTo"] + "\t" +
                                               Convert.ToDecimal(dtRow["RealCurrRate"]).ToString("0.00##") + "\t" + dtRow["StockExchangeTitle"] + "\t" +
                                               ((Convert.ToDateTime(dtRow["RecieveDate"]).Date != Convert.ToDateTime("2070/12/31").Date) ? Convert.ToDateTime(dtRow["RecieveDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                               ((Convert.ToDateTime(dtRow["SentDate"]).Date != Convert.ToDateTime("1900/01/01").Date) ? Convert.ToDateTime(dtRow["SentDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                               ((Convert.ToDateTime(dtRow["ExecuteDate"]).Date != Convert.ToDateTime("1900/01/01").Date) ? Convert.ToDateTime(dtRow["ExecuteDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                               dtRow["RecieveTitle"] + "\t" + dtRow["InformationTitle"] + "\t" + dtRow["Notes"] + "\t" + dtRow["Author_Fullname"] + "\t" + dtRow["Advisor_Fullname"] + "\t" +
                                               dtRow["FeesPercent"] + "\t" + dtRow["FeesAmount"] + "\t" + dtRow["ID"] + "\t" + dtRow["Client_ID"] + "\t" +
                                               dtRow["StockCompany_ID"] + "\t" + dtRow["Status"] + "\t" + dtRow["Contract_ID"] + "\t" +
                                               dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"] + "\t" + dtRow["BusinessType_ID"] + "\t" + "");
                            }
                        }
                        break;
                }  
                fgList.Sort(SortFlags.Descending, 0);
                fgList.Redraw = true;
            }
        }

        private void fgList_Click(object sender, EventArgs e)
        {  
            if (iMode == 2)                                                                 //  2 - Search Mode
                if (fgList.Col == 0)
                    if ((fgList[fgList.Row, "Check_FileName"] + "") != "")
                       Global.DMS_ShowFile("Customers/" + fgList[fgList.Row, "ContractTitle"] + "/Informing", fgList[fgList.Row, "Check_FileName"] + "");     //is DMS file, so show it into Web mode
        }
        private void fgList_DoubleClick(object sender, EventArgs e)
        {
            iRow = fgList.Row;
            if (iRow > 0) {
                if (iCommandType_ID == 1) {
                    frmOrderFX locOrderFX = new frmOrderFX();
                    locOrderFX.Record_ID = Convert.ToInt32(fgList[iRow, "ID"]);                 
                    locOrderFX.Editable = 1;
                    locOrderFX.Mode = 1;                                                            // 1 - from frmDailyFX, 2 - from frmAcc_InvoicesFX
                    locOrderFX.ShowDialog();
                    if (Convert.ToInt32(locOrderFX.LastAktion) == 1) {                             // Aktion=1        was saved (added)
                        //-------  read Command Data --------------------
                        clsOrdersFX OrderFX = new clsOrdersFX();
                        OrderFX.Record_ID = Convert.ToInt32(fgList[iRow, "ID"]);
                        OrderFX.GetRecord();

                        fgList[iRow, "ClientName"] = OrderFX.ClientName;
                        fgList[iRow, "ContractTitle"] = OrderFX.ContractTitle;
                        fgList[iRow, "StockCompany_Title"] = OrderFX.StockCompany_Title;
                        fgList[iRow, "Code"] = OrderFX.Code;
                        fgList[iRow, "Portfolio"] = OrderFX.Portfolio;
                        fgList[iRow, "CashAccountFrom"] = OrderFX.CashAccountFrom;
                        fgList[iRow, "AmountFrom"] = OrderFX.AmountFrom;
                        fgList[iRow, "CurrFrom"] = OrderFX.CurrFrom;
                        fgList[iRow, "CashAccountTo"] = OrderFX.CashAccountTo;
                        fgList[iRow, "AmountTo"] = OrderFX.AmountTo;
                        fgList[iRow, "CurrTo"] = OrderFX.CurrTo;
                        fgList[iRow, "RealAccountFrom"] = OrderFX.RealCashAccountFrom;
                        fgList[iRow, "RealAmountFrom"] = OrderFX.RealAmountFrom.ToString("0.####");
                        fgList[iRow, "RealCurrFrom"] = OrderFX.CurrFrom;
                        fgList[iRow, "RealAccountTo"] = OrderFX.RealCashAccountTo;
                        fgList[iRow, "RealAmountTo"] = OrderFX.RealAmountTo.ToString("0.####");
                        fgList[iRow, "RealCurrTo"] = OrderFX.CurrTo;
                        fgList[iRow, "Rate"] = OrderFX.RealCurrRate.ToString("0.00##");

                        fgList[iRow, "RecieveDate"] = (OrderFX.RecieveDate == Convert.ToDateTime("01 /01/1900") ? "" : OrderFX.RecieveDate.ToString("yyyy/MM/dd"));
                        if ((OrderFX.SentDate + "") == "") fgList[iRow, "SentDate"] = "";
                        else fgList[iRow, "SentDate"] = Convert.ToDateTime(OrderFX.SentDate).ToString("yyyy/MM/dd");

                        fgList[iRow, "ExecuteDate"] = (OrderFX.RealCurrRate == 0 ? "" : OrderFX.ExecuteDate.ToString("yyyy/MM/dd"));
                        fgList[iRow, "RecieveMethod"] = OrderFX.RecieveTitle;
                        fgList[iRow, "InformationMethod"] = OrderFX.InformationTitle;
                        fgList[iRow, "Status"] = OrderFX.Status;
                        fgList.Redraw = true;
                    }
                    else {
                        if (locOrderFX.LastAktion == 2) {                                                // LastAktion=2        created EXEC command - show it
                            DefineList();
                            OpenTransactionFXExecution(locOrderFX.Record_ID);
                        }
                    }
                }
                else {
                    OpenTransactionFXExecution(Convert.ToInt32(fgList[iRow, "ID"]));
                    if (iMode == 1 || iMode == 3) {                                                  // 1 - Entoloxarto Mode
                        if (iCommandType_ID == 1) toolLeft.Width = 354;                              // 1 - RTO list

                        else toolLeft.Width = 260;                                                   // 2 - Exec list
                        toolLeft.Visible = true;
                        toolLeft.Visible = false;
                        DefineList();
                    }
                    else {                                                                           // else - TransactionsSearch
                        toolLeft.Visible = false;
                        toolLeft.Visible = true;
                        DefineList_Search();
                    }
                }
            }
        }
        private void fgList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) {
                fgList.ContextMenuStrip = mnuContext;
                fgList.Row = fgList.MouseRow;
            }
        }
        private void fgList_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            if (e.Row > 1) {
                if ((fgList[e.Row, 26]+"") != "")                                                     // 25 - Execute Date
                    if (e.Col >= 16 && e.Col <= 21)
                             e.Style = csExecute;
            }
        }
        private void fgList_CellChanged(object sender, RowColEventArgs e)
        {  
            if (e.Row > 1) {
                if (e.Col == 37) {                                                                    // 36 - Status
                    if (Convert.ToInt32(fgList[e.Row, "Status"]) < 0) fgList.Rows[e.Row].Style = csCancel;
                    else fgList.Rows[e.Row].Style = null;
                }
            }
        }
        private void OpenTransactionFXExecution(int iRec_ID)
        {
            frmOrderFX_Execution locOrderFX_Execution = new frmOrderFX_Execution();
            locOrderFX_Execution.Record_ID = iRec_ID;
            locOrderFX_Execution.Editable = 1;
            locOrderFX_Execution.ShowDialog();
        }
        protected void ucCS_TextChanged(object sender, EventArgs e)
        {
            Global.ContractData stContract = new Global.ContractData();
            stContract = ucCS.SelectedContractData;
            if (ucCS.Contract_ID.Text != "0")
            {
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
                            lnkPortfolio.Text = stContract.Portfolio;
                            iClient_ID = stContract.Client_ID;
                            iContract_ID = stContract.Contract_ID;
                            iContract_Details_ID = stContract.Contracts_Details_ID;
                            iContract_Packages_ID = stContract.Contracts_Packages_ID;
                            iProvider_ID = stContract.Provider_ID;
                            sProviderTitle = stContract.Provider_Title + "";

                            iBusinessType_ID = 1;                                                          // by default                   BusinessType = 1
                            if (stContract.Provider_ID == Global.Company_ID) iBusinessType_ID = 2;         // curCompanyID - HellasFin, so BusinessType = 2

                            DefineList();

                            clsContracts_CashAccounts ClientCashAccounts = new clsContracts_CashAccounts();
                            ClientCashAccounts.Client_ID = 0;
                            ClientCashAccounts.Code = lblCode.Text;
                            ClientCashAccounts.Contract_ID = 0; // stContract.Contract_ID;
                            ClientCashAccounts.GetList_CashAccount();

                            dtAccsFrom = ClientCashAccounts.List;
                            dtAccsTo = ClientCashAccounts.List;

                            cmbCurrFrom.Focus();
                        }
                        else
                            MessageBox.Show("Contract Blocked", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        break;
                    case 3:
                        stContract = ucCS.SelectedContractData;
                        lnkPelatis.Text = stContract.ContractTitle;
                        lblCode.Text = stContract.Code;
                        lnkPortfolio.Text = stContract.Portfolio;
                        iClient_ID = stContract.Client_ID ;
                        iContract_ID = 0;
                        iContract_Details_ID = 0;
                        iContract_Packages_ID = 0;
                        iProvider_ID = stContract.Provider_ID;
                        sProviderTitle = stContract.Provider_Title;
                        break;
                }
            }
            fgCommandBuffer.Rows.Count = 1;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DefineList_Search();
        }
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
                    iProvider_ID = 0;
                    this.BackColor = Color.MediumAquamarine;
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            sUploadFile = "";

            if ((cmbCurrFrom.Text == "" || cmbCashAccFrom.Text == "" || cmbCurrTo.Text == "" || cmbCashAccTo.Text == "") && fgCommandBuffer.Rows.Count == 1)
                MessageBox.Show("Συμπληρώστε όλα τα παιδία", "DB Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                if (fgCommandBuffer.Rows.Count == 1) SaveTransaction(iBusinessType_ID, iCommandType_ID, iClient_ID, Global.Company_ID, iProvider_ID, iStockExchange_ID,
                                     iContract_ID, iContract_Details_ID, iContract_Packages_ID, lblCode.Text, lnkPortfolio.Text,
                                     dToday.Value, cmbType.SelectedIndex, txtAmountFrom.Text, cmbCurrFrom.Text, Convert.ToInt32(cmbCashAccFrom.SelectedValue),
                                     txtAmountTo.Text, cmbCurrTo.Text, Convert.ToInt32(cmbCashAccTo.SelectedValue), txtRate.Text, cmbConstant.SelectedIndex, 
                                     dConstant.Value, 0, "");
                else
                {
                    if (txtRecieveVoicePath.Text.Trim().Length > 0) {
                        sFileName = Path.GetFileName(txtRecieveVoicePath.Text.Trim());
                        sUploadFile = Global.DMS_UploadFile(txtRecieveVoicePath.Text.Trim(), "Customers/" + lnkPelatis.Text.Replace(".", "_") + "/OrdersAcception", sFileName);
                    }

                    for (i = 1; i <= fgCommandBuffer.Rows.Count - 1; i++)
                        SaveTransaction(iBusinessType_ID, iCommandType_ID, iClient_ID, Global.Company_ID, iProvider_ID, iStockExchange_ID,
                                     iContract_ID, iContract_Details_ID, iContract_Packages_ID, lblCode.Text, lnkPortfolio.Text,
                                     dToday.Value, Convert.ToInt32(fgCommandBuffer[i, "Type"]), fgCommandBuffer[i, "AmountFrom"]+"", fgCommandBuffer[i, "CurrFrom"] + "", 
                                     Convert.ToInt32(fgCommandBuffer[i, "CashAccFrom_ID"]), fgCommandBuffer[i, "AmountTo"] + "", fgCommandBuffer[i, "CurrTo"] + "",
                                     Convert.ToInt32(fgCommandBuffer[i, "CashAccTo_ID"]), fgCommandBuffer[i, "Rate"] + "", Convert.ToInt32(fgCommandBuffer[i, "Constant"]),
                                     Convert.ToDateTime(fgCommandBuffer[i, "ConstantDate"]), Convert.ToInt32(cmbRecieveMethod2.SelectedValue), txtRecieveVoicePath.Text);
                }

                EmptyData();
                DefineList();
                if (fgList.Rows.Count > 2) fgList.Row = 1;
                fgList.Focus();

                fgCommandBuffer.Rows.Count = 1;
                panMultiProducts.Visible = false;
            }
        }
        private void SaveTransaction(int iBusinessType_ID, int iCommandType_ID, int iClient_ID, int iCompany_ID, int iProvider_ID, int iStockExchange_ID,
                                     int iContract_ID, int iContract_Details_ID, int iContract_Packages_ID, string sCode, string sPortfolio, DateTime dToday, 
                                     int iType, string sAmountFrom, string sCurrFrom, int iCashAccFrom_ID, string sAmountTo, string sCurrTo, int iCashAccTo_ID, 
                                     string sRate, int iConstant, DateTime dConstant, int iRecieveMethod_ID, string sRecieveFile)
        {
            decimal decRate;
            int iID = 0;

            clsOrdersFX OrderFX = new clsOrdersFX();
            OrderFX.BulkCommand = "";
            OrderFX.BusinessType_ID = iBusinessType_ID;
            OrderFX.CommandType_ID = iCommandType_ID;
            OrderFX.Client_ID = iClient_ID;
            OrderFX.Company_ID = iCompany_ID;
            OrderFX.StockCompany_ID = iProvider_ID;
            OrderFX.StockExchange_ID = iStockExchange_ID;
            OrderFX.CustodyProvider_ID = iProvider_ID;
            OrderFX.II_ID = 0;
            OrderFX.Contract_ID = iContract_ID;
            OrderFX.Contract_Details_ID = iContract_Details_ID;
            OrderFX.Contract_Packages_ID = iContract_Packages_ID;
            OrderFX.Code = sCode;
            OrderFX.Portfolio = sPortfolio;
            OrderFX.AktionDate = dToday;
            OrderFX.Tipos = iType;
            OrderFX.AmountFrom = Global.IsNumeric(sAmountFrom) ? sAmountFrom : "0";
            OrderFX.CurrFrom = sCurrFrom;
            OrderFX.CashAccountFrom_ID = iCashAccFrom_ID;
            OrderFX.AmountTo = Global.IsNumeric(sAmountTo) ? sAmountTo : "0"; 
            OrderFX.CurrTo = sCurrTo;
            OrderFX.CashAccountTo_ID = iCashAccTo_ID;
            decRate = (Global.IsNumeric(txtRate.Text) ? Convert.ToDecimal(txtRate.Text) : 0);
            OrderFX.Rate = decRate;  
            OrderFX.Constant = iConstant;
            OrderFX.ConstantDate = dConstant.ToString("dd/MM/yyyy");  // dConstant.Value.ToString("dd/MM/yyyy");
            OrderFX.RecieveDate = DateTime.Now;
            OrderFX.RecieveMethod_ID = iRecieveMethod_ID;
            OrderFX.SentDate = Convert.ToDateTime("1900/01/01");
            OrderFX.ValueDate = "1900/01/01";
            OrderFX.ExecuteDate = Convert.ToDateTime("1900/01/01");
            OrderFX.Order_ID = "";
            OrderFX.RealAmountFrom = 0;
            OrderFX.RealCashAccountFrom_ID = 0;
            OrderFX.RealAmountTo = 0;
            OrderFX.RealCashAccountTo_ID = 0;
            OrderFX.RealCurrRate = 0;
            OrderFX.InformationMethod_ID = 0;
            OrderFX.Notes = "";
            OrderFX.User_ID = Global.User_ID;
            OrderFX.DateIns = DateTime.Now;
            iID = OrderFX.InsertRecord();

            AddRecievedFile(iID, iRecieveMethod_ID, sRecieveFile);
        }
        private void AddRecievedFile(int iCommand_ID, int iRecieveMethod_ID, string sRecieveFilePath)
        {
            clsOrdersFX_Recieved OrdersFX_Recieved = new clsOrdersFX_Recieved();
            OrdersFX_Recieved = new clsOrdersFX_Recieved();
            OrdersFX_Recieved.CommandFX_ID = iCommand_ID;
            OrdersFX_Recieved.DateIns = DateTime.Now;
            OrdersFX_Recieved.Method_ID = iRecieveMethod_ID;
            OrdersFX_Recieved.FilePath = sRecieveFilePath;
            OrdersFX_Recieved.FileName = Path.GetFileName(sUploadFile);
            OrdersFX_Recieved.InsertRecord();
        }
        private void EmptyData()
        {
            iClient_ID = 0;
            ucCS.ShowClientsList = false;
            ucCS.txtContractTitle.Text = "";
            ucCS.ShowClientsList = true;
            lnkPortfolio.Text = "";
            sProviderTitle = "";
            lblCode.Text = "";
            lnkPelatis.Text = "";
            EmptyFXData();
            ucCS.txtContractTitle.Focus();
        }
        private void EmptyFXData()
        {
            cmbCurrFrom.Text = "";
            cmbCashAccFrom.SelectedValue = 0;
            txtAmountFrom.Text = "";
            cmbCurrTo.Text = "";
            cmbCashAccTo.SelectedValue = 0;
            txtAmountTo.Text = "";
            cmbType.SelectedIndex = 0;
            txtRate.Text = "";
            cmbConstant.SelectedIndex = 0;
        }
        private void DefineList_Search()
        {
            fgList.Redraw = false;
            fgList.Rows.Count = 2;
            i = 0;
            iOld_ID = -999;

            clsOrdersFX OrdersFX = new clsOrdersFX();
            OrdersFX.CommandType_ID = 1;
            OrdersFX.DateFrom = ucDC.DateFrom;
            OrdersFX.DateTo = ucDC.DateTo;
            OrdersFX.StockCompany_ID = Convert.ToInt32(cmbProviders.SelectedValue);
            OrdersFX.Actions = Convert.ToInt32(cmbActions.SelectedIndex);
            OrdersFX.Sent = Convert.ToInt32(cmbSent.SelectedIndex);
            OrdersFX.User_ID = Convert.ToInt32(cmbUsers.SelectedValue);
            OrdersFX.User1_ID = Convert.ToInt32(cmbAdvisors.SelectedValue);
            OrdersFX.User4_ID = Convert.ToInt32(cmbDiax.SelectedValue);
            OrdersFX.Division_ID = Convert.ToInt32(cmbDivisions.SelectedValue);
            OrdersFX.Code = lblCode.Text;
            OrdersFX.GetList();

            foreach (DataRow dtRow in OrdersFX.List.Rows) {
                if (iOld_ID != Convert.ToInt32(dtRow["ID"])) {
                    iOld_ID = Convert.ToInt32(dtRow["ID"]);

                    bFilter = true;
                    if (Convert.ToInt32(cmbActions.SelectedIndex) == 1)                                                         // mono ektelesmenes
                        if (Convert.ToDateTime(dtRow["ExecuteDate"]) == Convert.ToDateTime("1900/01/01")) bFilter = false;

                    if (bFilter) {
                        i = i + 1;
                        sBulkCommand = (dtRow["BulkCommand"] + "").Replace("<", "").Replace(">", "");
                        fgList.AddItem(((dtRow["Check_FileName"] + "") == "" ? "0" : "1") + "\t" + i + "\t" + sBulkCommand + "\t" + dtRow["ClientName"] + "\t" + dtRow["ContractTitle"] + "\t" + 
                                       dtRow["Company_Title"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" + dtRow["CashAccount_From"] + "\t" + dtRow["AmountFrom"] + "\t" + 
                                       dtRow["CurrFrom"] + "\t" + dtRow["CashAccount_To"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["CurrTo"] + "\t" +
                                       sPriceType[Convert.ToInt32(dtRow["Tipos"])] + "\t" + (sConstant[Convert.ToInt16(dtRow["Constant"])] + " " + dtRow["ConstantDate"]).Trim() + "\t" +
                                       dtRow["RealCashAccount_From"] + "\t" + Convert.ToDecimal(dtRow["RealAmountFrom"]).ToString("0.00") + "\t" + dtRow["CurrFrom"] + "\t" +
                                       dtRow["RealCashAccount_To"] + "\t" + Convert.ToDecimal(dtRow["RealAmountTo"]).ToString("0.00") + "\t" + dtRow["CurrTo"] + "\t" +
                                       Convert.ToDecimal(dtRow["RealCurrRate"]).ToString("0.00##") + "\t" + dtRow["StockExchangeTitle"] + "\t" +
                                       ((Convert.ToDateTime(dtRow["RecieveDate"]) != Convert.ToDateTime("31/12/2070")) ? Convert.ToDateTime(dtRow["RecieveDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                       ((Convert.ToDateTime(dtRow["SentDate"]) != Convert.ToDateTime("01/01/1900")) ? Convert.ToDateTime(dtRow["SentDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                       ((Convert.ToDateTime(dtRow["ExecuteDate"]) != Convert.ToDateTime("01/01/1900")) ? Convert.ToDateTime(dtRow["ExecuteDate"]).ToString("yyyy/MM/dd") : "") + "\t" +
                                       dtRow["RecieveTitle"] + "\t" + dtRow["InformationTitle"] + "\t" + dtRow["Notes"] + "\t" + dtRow["Author_Fullname"] + "\t" + dtRow["Advisor_Fullname"] + "\t" +
                                       dtRow["FeesPercent"] + "\t" + dtRow["FeesAmount"] + "\t" + dtRow["ID"] + "\t" + dtRow["Client_ID"] + "\t" +
                                       dtRow["StockCompany_ID"] + "\t" + dtRow["Status"] + "\t" + dtRow["Contract_ID"] + "\t" + 
                                       dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"] + "\t" + dtRow["BusinessType_ID"] + "\t" + dtRow["Check_FileName"]);
                    }
                }
            }

            fgList.Sort(SortFlags.Descending, 1);
            fgList.Redraw = true;
        }
        public int Mode { get { return iMode; } set { iMode = value; } }                                    // 1 - Dialy, 2 - Search, 3 - from DailSecurities
        public int Commands_ID { get { return iCommands_ID; } set { iCommands_ID = value; } }
        public int RightsLevel { get { return iRightsLevel; } set { iRightsLevel = value; } }
        public string Extra { get { return sExtra; } set { sExtra = value; } }
    }
}
