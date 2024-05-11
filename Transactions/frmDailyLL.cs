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
    public partial class frmDailyLL : Form
    {
        DataTable dtAccs;
        DataColumn dtCol;
        DataRow dtRow1;
        DataView dtView;
        int i, iClient_ID, iContract_ID, iContract_Details_ID, iContract_Packages_ID, iMode, iRightsLevel, iRow, iProvider_ID, iClientType;
        string sProviderTitle, sExtra;
        bool bCheckList;
        CellRange rng;
        CellStyle csCancel, csExecute, csWait;
        clsOrdersLL OrdersLL = new clsOrdersLL();
        public frmDailyLL()
        {
            InitializeComponent();
            panDaily.Left = 4;
            panDaily.Top = 8;
            panDaily.Visible = true;

            panSearch.Left = 4;
            panSearch.Top = 8;
            panSearch.Visible = false;

            panFilters.Top = 8;
            panFilters.Left = 908;
            panFilters.Width = 610;
            panFilters.Height = 100;

            csCancel = fgList.Styles.Add("Cancelled");
            csCancel.ForeColor = Color.Red;

            csExecute = fgList.Styles.Add("Execute");
            csExecute.BackColor = Color.Gold;

            csWait = fgList.Styles.Add("Wait");
            csWait.BackColor = Color.LightSeaGreen;

            dPeriodStart.Value = DateTime.Now;
            dPeriodEnd.Value = DateTime.Now.AddDays(30);

            //-------------- Define Currencies List ------------------
            cmbCurr.DataSource = Global.dtCurrencies.Copy();
            cmbCurr.DisplayMember = "Title";
            cmbCurr.ValueMember = "ID";

            dtAccs = new DataTable("AccsList");
            dtCol = dtAccs.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dtCol = dtAccs.Columns.Add("AccountNumber", System.Type.GetType("System.String"));
            dtCol = dtAccs.Columns.Add("Currency", System.Type.GetType("System.String"));
        }

        private void frmDailyLL_Load(object sender, EventArgs e)
        {
            bCheckList = false;           

            switch (iMode)
            {
                case 1:
                case 3:
                    panDaily.Visible = true;
                    panSearch.Visible = false;

                    panFilters.Width = 616;
                    panFilters.Height = 100;

                    ucCS.Left = 68;
                    ucCS.Top = 12;
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
                    panFilters.Height = 128;

                    ucCS.Left = 444;
                    ucCS.Top = 14;
                    ucCS.StartInit(700, 400, 440, 20, 1);
                    ucCS.TextOfLabelChanged += new EventHandler(ucCS_TextChanged);
                    ucCS.Filters = "Status = 1 And Contract_ID > 0";
                    ucCS.ListType = 2;

                    ucDC.DateFrom = DateTime.Now;
                    ucDC.DateTo = DateTime.Now;
                    break;
            }

            this.Text = "Εντολόχαρτο LL";
            lblContract.Text = Global.GetLabel("contract");
            lblCustomer.Text = Global.GetLabel("__b45");
            lblProvider.Text = Global.GetLabel("provider");
            lblAdvisor.Text = Global.GetLabel("advisor");
            lblSender.Text = Global.GetLabel("transmitter");
            lblSended.Text = Global.GetLabel("transmission");
            lblExecute.Text = Global.GetLabel("execution");

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

            fgList.Cols[0].AllowMerging = true;
            rng = fgList.GetCellRange(0, 0, 1, 0);
            rng.Data = "";

            fgList.Cols[1].AllowMerging = true;
            rng = fgList.GetCellRange(0, 1, 1, 1);
            rng.Data = Global.GetLabel("n");

            fgList.Cols[2].AllowMerging = true;
            rng = fgList.GetCellRange(0, 2, 1, 2);
            rng.Data = "Εντολέας";

            fgList.Cols[3].AllowMerging = true;
            rng = fgList.GetCellRange(0, 3, 1, 3);
            rng.Data = "Σύμβαση";

            fgList.Cols[4].AllowMerging = true;
            rng = fgList.GetCellRange(0, 4, 1, 4);
            rng.Data = Global.GetLabel("provider");

            fgList.Cols[5].AllowMerging = true;
            rng = fgList.GetCellRange(0, 5, 1, 5);
            rng.Data = Global.GetLabel("code");

            fgList.Cols[6].AllowMerging = true;
            rng = fgList.GetCellRange(0, 6, 1, 6);
            rng.Data = Global.GetLabel("subaccount");

            rng = fgList.GetCellRange(0, 7, 0, 9);
            rng.Data = Global.GetLabel("lombard_lending");

            fgList[1, 7] = Global.GetLabel("cash_account");
            fgList[1, 8] = Global.GetLabel("amount");
            fgList[1, 9] = Global.GetLabel("currency");

            fgList.Cols[10].AllowMerging = true;
            rng = fgList.GetCellRange(0, 10, 1, 10);
            rng.Data = Global.GetLabel("ltv");

            fgList.Cols[11].AllowMerging = true;
            rng = fgList.GetCellRange(0, 11, 1, 11);
            rng.Data = Global.GetLabel("lombard_lending_as_of_portfolio_ltv");

            fgList.Cols[12].AllowMerging = true;
            rng = fgList.GetCellRange(0, 12, 1, 12);
            rng.Data = Global.GetLabel("providers_rate");

            fgList.Cols[13].AllowMerging = true;
            rng = fgList.GetCellRange(0, 13, 1, 13);
            rng.Data = Global.GetLabel("additional_margin_rate");

            fgList.Cols[14].AllowMerging = true;
            rng = fgList.GetCellRange(0, 14, 1, 14);
            rng.Data = Global.GetLabel("discount");

            fgList.Cols[15].AllowMerging = true;
            rng = fgList.GetCellRange(0, 15, 1, 15);
            rng.Data = Global.GetLabel("final_additional_margin_rate");

            fgList.Cols[16].AllowMerging = true;
            rng = fgList.GetCellRange(0, 16, 1, 16);
            rng.Data = Global.GetLabel("gross_clients_rate");

            fgList.Cols[17].AllowMerging = true;
            rng = fgList.GetCellRange(0, 17, 1, 17);
            rng.Data = Global.GetLabel("period_start");

            fgList.Cols[18].AllowMerging = true;
            rng = fgList.GetCellRange(0, 18, 1, 18);
            rng.Data = Global.GetLabel("period_end");

            fgList.Cols[19].AllowMerging = true;
            rng = fgList.GetCellRange(0, 19, 1, 19);
            rng.Data = Global.GetLabel("days");

            fgList.Cols[20].AllowMerging = true;
            rng = fgList.GetCellRange(0, 20, 1, 20);
            rng.Data = Global.GetLabel("execution_date");

            fgList.Cols[21].AllowMerging = true;
            rng = fgList.GetCellRange(0, 21, 1, 21);
            rng.Data = "Διαβιβαστής";

            fgList.Styles.Fixed.TextAlign = TextAlignEnum.CenterCenter;

            bCheckList = true;
        }

        protected override void OnResize(EventArgs e)
        {
            fgList.Width = this.Width - 26;
            fgList.Height = this.Height - 212;
        }
        private void dToday_ValueChanged(object sender, EventArgs e)
        {
            DefineList();
        }
        private void DefineList()
        { 
            OrdersLL = new clsOrdersLL();
            OrdersLL.DateFrom = dToday.Value;
            OrdersLL.DateTo = dToday.Value;
            OrdersLL.StockCompany_ID = Convert.ToInt32(cmbProviders.SelectedValue);
            OrdersLL.Sent = 0; // Convert.ToInt32(cmbSent.SelectedIndex);
            OrdersLL.Actions = 0; // Convert.ToInt32(cmbActions.SelectedIndex);
            OrdersLL.User1_ID = Convert.ToInt32(cmbAdvisors.SelectedValue);
            OrdersLL.User3_ID = Convert.ToInt32(cmbUsers.SelectedValue);
            OrdersLL.Code = lblCode.Text;
            OrdersLL.GetList();

            i = 0;             
            fgList.Redraw = false;
            fgList.Rows.Count = 2;
            foreach (DataRow dtRow in OrdersLL.List.Rows)
            {
                i = i + 1;
                fgList.AddItem("" + "\t" + i + "\t" + dtRow["ClientName"] + "\t" + dtRow["ContractTitle"] + "\t" +
                                   dtRow["StockCompany_Title"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" +
                                   dtRow["AccountNumber"] + "\t" + dtRow["Amount"] + "\t" + dtRow["Curr"] + "\t" +
                                   dtRow["LTV"] + "\t" + dtRow["LL_AS"] + "\t" + dtRow["ProviderRate"] + "\t" + dtRow["AdditionalRate"] + "\t" +
                                   dtRow["Discount"] + "\t" + dtRow["FinalMargin"] + "\t" + dtRow["GrossRate"] + "\t" +
                                   dtRow["PeriodStart"] + "\t" + dtRow["PeriodEnd"] + "\t" + dtRow["Days"] + "\t" +
                                   (Convert.ToDateTime(dtRow["ExecuteDate"]) == Convert.ToDateTime("01/01/1900") ? "" : Convert.ToDateTime(dtRow["ExecuteDate"]).ToString("dd/MM/yy")) + "\t" +
                                   dtRow["AuthorName"] + "\t" + dtRow["ID"] + "\t" + dtRow["Client_ID"] + "\t" + dtRow["StockCompany_ID"] + "\t" + dtRow["Status"] + "\t" + "" + "\t" + "");
            }
            fgList.Sort(SortFlags.Descending, 1);     // 1- Num
            fgList.Redraw = true;

            if (fgList.Rows.Count > 2) {
                fgList.Row = 2;
                fgList.Focus();
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
            if (iRow > 0)
            {
                frmOrderLL locOrderLL = new frmOrderLL();
                locOrderLL.Record_ID = Convert.ToInt32(fgList[iRow, "ID"]);
                locOrderLL.Editable = 1;
                locOrderLL.Mode = 1;                                                            // 1 - from frmDailyLL, 2 - from frmAcc_InvoicesLL
                locOrderLL.ShowDialog();
                if (locOrderLL.LastAktion == 1) {
                    OrdersLL = new clsOrdersLL();
                    OrdersLL.Record_ID = Convert.ToInt32(fgList[iRow, "ID"]);
                    OrdersLL.GetRecord();
                    fgList[iRow, 8] = OrdersLL.Amount;
                    fgList[iRow, 9] = OrdersLL.Curr;
                    fgList[iRow, 10] = OrdersLL.LTV;
                    fgList[iRow, 11] = OrdersLL.LL_AS;
                    fgList[iRow, 12] = OrdersLL.ProviderRate;
                    fgList[iRow, 13] = OrdersLL.AdditionalRate;
                    fgList[iRow, 14] = OrdersLL.Discount;
                    fgList[iRow, 15] = OrdersLL.FinalMargin;
                    fgList[iRow, 16] = OrdersLL.GrossRate;
                    fgList[iRow, 17] = OrdersLL.PeriodStart;
                    fgList[iRow, 18] = OrdersLL.PeriodEnd;
                    fgList[iRow, 19] = OrdersLL.Days;
                    fgList[iRow, 20] = (OrdersLL.ExecuteDate == Convert.ToDateTime("1900/01/01")? "" : OrdersLL.ExecuteDate.ToString("dd/MM/yy"));
                    fgList[iRow, 25] = OrdersLL.Status;
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
        private void fgList_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            if (e.Row > 1)
            {
                if ((fgList[e.Row, 26] + "") != "")                                                     // 25 - Execute Date
                    if (e.Col >= 16 && e.Col <= 21)
                        e.Style = csExecute;
            }
        }
        private void fgList_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Row > 1) {
                if (e.Col == 25) {                                                                    // 25- Status
                    if (Convert.ToInt32(fgList[e.Row, "Status"]) < 0) fgList.Rows[e.Row].Style = csCancel;
                    else fgList.Rows[e.Row].Style = null;
                }
            }
        }
        private void btnCleanUp_Click(object sender, EventArgs e)
        {
            EmptyData();
            DefineList();
        }
        private void cmbCurr_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) {
                if (dtAccs.Rows.Count > 0) {
                    dtView = dtAccs.Copy().DefaultView;
                    dtView.RowFilter = "Currency = '' OR Currency = '" + cmbCurr.Text + "'";
                    cmbCashAccounts.DataSource = dtView;
                    cmbCashAccounts.DisplayMember = "AccountNumber";
                    cmbCashAccounts.ValueMember = "ID";
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbCurr.Text == "" || cmbCashAccounts.Text == "")
                MessageBox.Show("Συμπληρώστε όλα τα παιδία", "DB Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else  {
                try {
                    OrdersLL = new clsOrdersLL();
                    OrdersLL.StockCompany_ID = iProvider_ID;
                    OrdersLL.Client_ID = iClient_ID;
                    OrdersLL.Contract_ID = iContract_ID;
                    OrdersLL.Contract_Details_ID = iContract_Details_ID;
                    OrdersLL.Contract_Packages_ID = iContract_Packages_ID;
                    OrdersLL.Code = lblCode.Text;
                    OrdersLL.Portfolio = lnkPortfolio.Text;
                    OrdersLL.AktionDate = dToday.Value;
                    OrdersLL.CashAccount_ID = Convert.ToInt32(cmbCashAccounts.SelectedValue);
                    OrdersLL.Amount = Convert.ToSingle(txtAmount.Text);
                    OrdersLL.Curr = cmbCurr.Text;
                    OrdersLL.LTV = 0;
                    OrdersLL.LL_AS = 0;
                    OrdersLL.ProviderRate = 0;
                    OrdersLL.AdditionalRate = 0;
                    OrdersLL.Discount = 0;
                    OrdersLL.FinalMargin = 0;
                    OrdersLL.GrossRate = Convert.ToSingle(txtClientsGrossRate.Text);
                    OrdersLL.PeriodStart = dPeriodStart.Value;
                    OrdersLL.PeriodEnd = dPeriodEnd.Value;
                    OrdersLL.Days = Convert.ToInt32((dPeriodEnd.Value - dPeriodStart.Value).TotalDays) + 1;
                    OrdersLL.CurrRate = 0;
                    OrdersLL.RecieveDate = DateTime.Now;
                    OrdersLL.RecieveMethod_ID = 0;
                    OrdersLL.SentDate = Convert.ToDateTime("1900/01/01");
                    OrdersLL.ExecuteDate = Convert.ToDateTime("1900/01/01");
                    OrdersLL.Notes = "";
                    OrdersLL.User_ID = Global.User_ID;
                    OrdersLL.DateIns = DateTime.Now;
                    OrdersLL.Status = 0;     
                    OrdersLL.CompanyFeesPercent = 0;
                    OrdersLL.InsertRecord();
                }

                catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                finally { }
            }

            EmptyData();
            DefineList();
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
            EmptyLLData();
            ucCS.txtContractTitle.Focus();
        }
        private void EmptyLLData()
        {
            cmbCurr.Text = "";
            cmbCashAccounts.SelectedValue = 0;
            txtAmount.Text = "";
            txtClientsGrossRate.Text = "";
            dPeriodStart.Value = DateTime.Now;
            dPeriodEnd.Value = DateTime.Now.AddDays(30);
        }
        private void ucCS_TextChanged(object sender, EventArgs e)
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
                        if (klsContract_Blocks.Record_ID == 0)
                        {
                            lnkPelatis.Text = stContract.ContractTitle;
                            lblCode.Text = stContract.Code;
                            lnkPortfolio.Text = stContract.Portfolio;
                            iClient_ID = stContract.Client_ID;
                            iContract_ID = stContract.Contract_ID;
                            iContract_Details_ID = stContract.Contracts_Details_ID;
                            iContract_Packages_ID = stContract.Contracts_Packages_ID;
                            iProvider_ID = stContract.Provider_ID;
                            sProviderTitle = stContract.Provider_Title + "";
                            iClientType = stContract.ClientType;

                            //--- define ALL CasAccounts of Code lblCode.Text and Portfolio stContract.Contract_ID -----------------------
                            clsContracts_CashAccounts ClientCashAccounts = new clsContracts_CashAccounts();
                            ClientCashAccounts.Client_ID = 0;
                            ClientCashAccounts.Code = lblCode.Text;
                            ClientCashAccounts.Contract_ID = stContract.Contract_ID;
                            ClientCashAccounts.GetList_CashAccount();
                           
                            dtAccs = new DataTable("AccsList");
                            dtCol = dtAccs.Columns.Add("ID", System.Type.GetType("System.Int32"));
                            dtCol = dtAccs.Columns.Add("AccountNumber", System.Type.GetType("System.String"));
                            dtCol = dtAccs.Columns.Add("Currency", System.Type.GetType("System.String"));
                            foreach (DataRow dtRow in ClientCashAccounts.List.Rows)  {
                                dtRow1 = dtAccs.NewRow();
                                dtRow1["ID"] = dtRow["ID"]+"";
                                dtRow1["AccountNumber"] = dtRow["AccountNumber"]+"";
                                dtRow1["Currency"] = dtRow["Currency"]+"";
                                dtAccs.Rows.Add(dtRow1);
                            }

                            cmbCashAccounts.DataSource = dtAccs.Copy();
                            cmbCashAccounts.DisplayMember = "AccountNumber";
                            cmbCashAccounts.ValueMember = "ID";
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
                        iProvider_ID = stContract.Provider_ID;
                        sProviderTitle = stContract.Provider_Title;
                        iClientType = stContract.ClientType;
                        break;
                }
            }
        }
        public int Mode { get { return iMode; } set { iMode = value; } }                                                         // 1 - Dialy, 2 - Search
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
