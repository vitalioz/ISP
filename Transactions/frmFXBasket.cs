using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Core;

namespace Transactions
{
    public partial class frmFXBasket : Form
    {
        DataTable dtAccsFrom, dtAccsTo;
        DataRow dtRow;
        DataColumn dtCol;
        int i, j, k, iID, iNewContract_ID, iNewContractDetails_ID, iNewContractPackages_ID, iChoiceBusinessType_ID, iPressAction, iFirstChoice = 0;
        decimal sgTemp, sgTemp1;
        string sNewCode, sNewPortfolio;
        bool bFound, bCheckList, bCanChoice;
        DateTime dToday;
        C1.Win.C1FlexGrid.CellRange rng;
        clsOrdersFX OrderFX = new clsOrdersFX();
        clsOrdersFX OrderFX2 = new clsOrdersFX();
        clsContracts Contracts = new clsContracts();
        clsContracts_CashAccounts ClientCashAccounts = new clsContracts_CashAccounts();
        clsServiceProviders ServiceProviders = new clsServiceProviders();
        public frmFXBasket()
        {
            InitializeComponent();
            iPressAction = 0;
            cmbConstant.SelectedIndex = 0;
        }
        private void frmFXBasket_Load(object sender, EventArgs e)
        {
            bCheckList = false;
            bCanChoice = false;

            btnFinish.Text = "Διαβίβαση εντολής";
            btnFinish.Enabled = false;

            //-------------- Define Currencies List ------------------
            cmbCurrFrom.DataSource = Global.dtCurrencies.Copy();
            cmbCurrFrom.DisplayMember = "Title";
            cmbCurrFrom.ValueMember = "ID";

            //-------------- Define Currencies List ------------------
            cmbCurrTo.DataSource = Global.dtCurrencies.Copy();
            cmbCurrTo.DisplayMember = "Title";
            cmbCurrTo.ValueMember = "ID";

            //------- fgSummary ----------------------------
            fgSummary.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgSummary.Styles.ParseString(Global.GridStyle);
            //fgSummary.RowColChange += new EventHandler(fgSummary_RowColChange);
            //fgSummary.MouseDown += new MouseEventHandler(fgSummary_MouseDown);
            fgSummary.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgSummary_BeforeEdit);
            fgSummary.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgSummary_AfterEdit);
            fgSummary.OwnerDrawCell += fgSummary_OwnerDrawCell;

            fgSummary.DrawMode = DrawModeEnum.OwnerDraw;
            fgSummary.ShowCellLabels = true;

            fgSummary.Styles.Normal.WordWrap = true;
            fgSummary.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgSummary.Rows[0].AllowMerging = true;

            fgSummary.Cols[0].AllowMerging = true;
            rng = fgSummary.GetCellRange(0, 0, 1, 0);
            rng.Data = " ";

            fgSummary.Cols[1].AllowMerging = true;
            rng = fgSummary.GetCellRange(0, 1, 1, 1);
            rng.Data = Global.GetLabel("provider");

            rng = fgSummary.GetCellRange(0, 2, 0, 3);
            rng.Data = Global.GetLabel("debit");

            fgSummary[1, 2] = Global.GetLabel("amount");
            fgSummary[1, 3] = Global.GetLabel("currency");


            rng = fgSummary.GetCellRange(0, 4, 0, 5);
            rng.Data = Global.GetLabel("credit");

            fgSummary[1, 4] = Global.GetLabel("amount");
            fgSummary[1, 5] = Global.GetLabel("currency");
                      
            //------- fgSimpleCommands ----------------------------
            fgSimpleCommands.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgSimpleCommands.Styles.ParseString(Global.GridStyle);
            fgSimpleCommands.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgSimpleCommands_BeforeEdit);
            fgSimpleCommands.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgSimpleCommands_AfterEdit);

            //--- Define Unsent Commands List (only Single Orders) ----------------------
            OrderFX.CommandType_ID = 1;
            OrderFX.DateFrom = dToday;
            OrderFX.DateTo = dToday;
            OrderFX.StockCompany_ID = 0;
            OrderFX.GetList();

            foreach (DataRow dtRow in OrderFX.List.Rows) {

                if  ((Convert.ToInt32(dtRow["Status"]) >= 0)  &&
                    (Convert.ToDateTime(dtRow["RecieveDate"]).Date != Convert.ToDateTime("1900/01/01").Date) &&
                    (Convert.ToDateTime(dtRow["ExecuteDate"]).Date == Convert.ToDateTime("1900/01/01").Date) &&
                    (Convert.ToDateTime(dtRow["SentDate"]).Date == Convert.ToDateTime("1900/01/01").Date)) {

                    bFound = false;
                    for (j = 2; j <= fgSummary.Rows.Count - 1; j++)
                        if ((dtRow["CurrFrom"].ToString() == fgSummary[j, "DebitCurr"].ToString()) && 
                            (dtRow["CurrTo"].ToString() == fgSummary[j, "CreditCurr"].ToString()) && 
                            (Convert.ToInt32(dtRow["StockCompany_ID"]) == Convert.ToInt32(fgSummary[j, "Provider_ID"])) &&
                            (CreateUniqueCode(Convert.ToDecimal(dtRow["AmountFrom"]), dtRow["CurrFrom"] + "", dtRow["CurrTo"] + "") == (fgSummary[j, "ixxxyyy"] + "")) )
                        {
                            bFound = true;
                            break;
                        }
                            

                    if (bFound) {         
                            sgTemp = (Global.IsNumeric(dtRow["AmountFrom"]) ? Convert.ToDecimal(dtRow["AmountFrom"]) : 0);
                            fgSummary[j, 2] = (Convert.ToDecimal(fgSummary[j, 2]) + sgTemp);

                            sgTemp = (Global.IsNumeric(dtRow["AmountTo"]) ? Convert.ToDecimal(dtRow["AmountTo"]) : 0);
                            fgSummary[j, 4] = (Convert.ToDecimal(fgSummary[j, 4]) + sgTemp);
                    }
                    else
                       fgSummary.AddItem(false + "\t" + dtRow["Company_Title"] + "\t" + Convert.ToDecimal(dtRow["AmountFrom"]) + "\t" + dtRow["CurrFrom"] + "\t" + 
                                         Convert.ToDecimal(dtRow["AmountTo"]) + "\t" + dtRow["CurrTo"] + "\t" +
                                         CreateUniqueCode(Convert.ToDecimal(dtRow["AmountFrom"]), dtRow["CurrFrom"] + "", dtRow["CurrTo"] + "") + "\t" +
                                         dtRow["StockCompany_ID"] + "\t" + dtRow["BusinessType_ID"]);

                }
            }
            fgSummary.Sort(SortFlags.Ascending, 1);
            fgSummary.Redraw = true;

            bCheckList = true;
        }         
        private void fgSummary_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (bCheckList)  {
                if (e.Col == 0) e.Cancel = false;
                else e.Cancel = true;

                DefineFinishButton();
            }
        }
        private void fgSummary_AfterEdit(object sender, RowColEventArgs e)
        {     
            if (bCheckList) {
                if (e.Col == 0) {

                    if (iFirstChoice == 0) {                                        // it's first choice from fgSummary List
                        cmbCurrFrom.Text = fgSummary[fgSummary.Row, "DebitCurr"] + "";
                        txtAmountFrom.Text = fgSummary[fgSummary.Row, "Debit"] + "";
                        cmbCurrTo.Text = fgSummary[fgSummary.Row, "CreditCurr"] + "";
                        txtAmountTo.Text = fgSummary[fgSummary.Row, "Credit"] + "";
                        iChoiceBusinessType_ID = Convert.ToInt32(fgSummary[fgSummary.Row, "BusinessType_ID"]);
                        cmbConstant.SelectedIndex = 0;

                        sNewCode = "";
                        sNewPortfolio = "";
                        iNewContract_ID = 0;
                        iNewContractDetails_ID = 0;
                        iNewContractPackages_ID = 0;

                        if (iChoiceBusinessType_ID == 2) {
                            bCheckList = false;
                            ServiceProviders.AktionDate = DateTime.Now;
                            ServiceProviders.GetList_FX();
                            k = ServiceProviders.List.Rows.Count-1;

                            cmbServiceProviders.DataSource = ServiceProviders.List.Copy(); ;
                            cmbServiceProviders.DisplayMember = "Title";
                            cmbServiceProviders.ValueMember = "ID";

                            bCheckList = true;
                            //cmbServiceProviders.SelectedIndex = k;
                        }
                        btnFinish.Visible = true;
                        bCanChoice = true;
                        iFirstChoice = 1;
                    }
                }
                else {                                                                                // it isn't first choice from fgSummary
                    if (iChoiceBusinessType_ID != 0 && iChoiceBusinessType_ID != Convert.ToInt32(fgSummary[fgSummary.Row, 7])) {
                        fgSummary[fgSummary.Row, 0] = false;
                        MessageBox.Show("Δεν γίνεται ομαδοποίηση εντολών με διαφορετικά νομίσματα, πάροχο ή πράξη", "DB Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        bCanChoice = false;
                    }
                    else bCanChoice = true;
                }

                if (bCanChoice) {
                    fgSimpleCommands.Redraw = false;
                    fgSimpleCommands.Rows.Count = 1;

                    k = 0;
                    sgTemp = 0;
                    for (j = 2; j <= fgSummary.Rows.Count - 1; j++) {
                        if (Convert.ToBoolean(fgSummary[j, 0])) {
                            k = k + 1;

                            foreach (DataRow dtRow in OrderFX.List.Rows) {
                                if (Convert.ToInt32(dtRow["Status"]) >= 0) {                                                                // Status >= 0 - not cancelled
                                    //                                                                                     fgList(i, 36) - Provider_ID         fgList(i, 25) - SendDate
                                    if (CreateUniqueCode(Convert.ToDecimal(dtRow["AmountFrom"]), (dtRow["CurrFrom"] + ""), (dtRow["CurrTo"] + "")) == (fgSummary[j, "ixxxyyy"]+"") &&
                                                         (Convert.ToInt32(dtRow["StockCompany_ID"]) == Convert.ToInt32(fgSummary[j, "Provider_ID"])) &&
                                                         (Convert.ToDateTime(dtRow["SentDate"]).Date == Convert.ToDateTime("1900/01/01").Date)) {
                                       fgSimpleCommands.AddItem(true + "\t" + dtRow["ID"] + "\t" + dtRow["ClientName"] + "\t" + dtRow["Company_Title"] + "\t" + 
                                                                dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" + dtRow["CashAccount_From"] + "\t" + dtRow["AmountFrom"] + "\t" + dtRow["CurrFrom"] + "\t" +
                                                                dtRow["CashAccount_To"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["CurrTo"] + "\t" +
                                                                dtRow["ID"] + "\t" + CreateUniqueCode(Convert.ToDecimal(dtRow["AmountFrom"]), dtRow["CurrFrom"] + "", dtRow["CurrTo"] + ""));

                                        //sgTemp = sgTemp + ConvertText2Numeric(fgList(i, 13))
                                    }
                               }
                            }
                        }
                    }
                    if (k == 0) {                        // not exists checked record
                        cmbCurrFrom.Text = "";
                        txtAmountFrom.Text = "0";
                        cmbCurrTo.Text = "";
                        txtAmountTo.Text = "0";
                        btnFinish.Visible = false;
                        bCheckList = false;
                        cmbServiceProviders.SelectedValue = 0;
                        bCheckList = true;
                        iChoiceBusinessType_ID = 0;
                        iFirstChoice = 0;
                    }
                    else {
                        sgTemp = 0;
                        sgTemp1 = 0;
                        for (j = 1; j <= fgSimpleCommands.Rows.Count - 1; j++) {
                            if (Global.IsNumeric(fgSimpleCommands[j, "AmountFrom"]))
                                sgTemp = sgTemp + Convert.ToDecimal(fgSimpleCommands[j, "AmountFrom"]);
                            if (Global.IsNumeric(fgSimpleCommands[j, "AmountTo"]))
                               sgTemp1 = sgTemp1 + Convert.ToDecimal(fgSimpleCommands[j, "AmountTo"]);
                        }
                        txtAmountFrom.Text = sgTemp.ToString("0.00");
                        txtAmountTo.Text = sgTemp1.ToString("0.00");
                    }
                    fgSimpleCommands.Redraw = true;
                  
                }
                DefineFinishButton();
                //if (fgSimpleCommands.Rows.Count > 1) btnFinish.Enabled = true;
                //else DefineFinishButton();
            }
        }
        private void cmbServiceProviders_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) {

                Contracts.ServiceProvider_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                Contracts.AktionDate = DateTime.Now;
                Contracts.GetRecordFX_Date();   
                iNewContract_ID = Contracts.Record_ID;
                iNewContractDetails_ID = Contracts.Contract_Details_ID;
                iNewContractPackages_ID = Contracts.Contract_Packages_ID;
                sNewCode = Contracts.Code;
                sNewPortfolio = Contracts.Portfolio;

                //-------------- Define Cash Accounts List ------------------
                dtAccsFrom = new DataTable("AccsFrom");
                dtCol = dtAccsFrom.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = dtAccsFrom.Columns.Add("AccountNumber", System.Type.GetType("System.String"));
                dtCol = dtAccsFrom.Columns.Add("Currency", System.Type.GetType("System.String"));

                dtAccsTo = new DataTable("AccsTo");
                dtCol = dtAccsTo.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = dtAccsTo.Columns.Add("AccountNumber", System.Type.GetType("System.String"));
                dtCol = dtAccsTo.Columns.Add("Currency", System.Type.GetType("System.String"));

                dtRow = dtAccsFrom.NewRow();
                dtRow["ID"] = 0;
                dtRow["AccountNumber"] = "";
                dtRow["Currency"] = "";
                dtAccsFrom.Rows.Add(dtRow);

                dtRow = dtAccsTo.NewRow();
                dtRow["ID"] = 0;
                dtRow["AccountNumber"] = "";
                dtRow["Currency"] = "";
                dtAccsTo.Rows.Add(dtRow);
                                
                ClientCashAccounts.PackageType = 2;
                ClientCashAccounts.Client_ID = Convert.ToInt32(fgSummary[fgSummary.Row, "Provider_ID"]);
                ClientCashAccounts.Provider_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                ClientCashAccounts.GetList_PackageType();
                foreach (DataRow dtRow1 in ClientCashAccounts.List.Rows) {
                    dtRow = dtAccsTo.NewRow();
                    dtRow["ID"] = dtRow1["ID"];
                    dtRow["AccountNumber"] = dtRow1["AccountNumber"] + " / " + dtRow1["Currency"];
                    dtRow["Currency"] = dtRow1["Currency"];
                    dtAccsTo.Rows.Add(dtRow);

                    dtRow = dtAccsFrom.NewRow();
                    dtRow["ID"] = dtRow1["ID"];
                    dtRow["AccountNumber"] = dtRow1["AccountNumber"] + " / " + dtRow1["Currency"];
                    dtRow["Currency"] = dtRow1["Currency"];
                    dtAccsFrom.Rows.Add(dtRow);
                }

                cmbCashAccFrom.DataSource = dtAccsFrom.Copy();
                cmbCashAccFrom.DisplayMember = "AccountNumber";
                cmbCashAccFrom.ValueMember = "ID";
                cmbCashAccFrom.SelectedValue = 0;

                cmbCashAccTo.DataSource = dtAccsTo.Copy();
                cmbCashAccTo.DisplayMember = "AccountNumber";
                cmbCashAccTo.ValueMember = "ID";
                cmbCashAccTo.SelectedValue = 0;

                if (iNewContract_ID == 0) {
                    btnFinish.Enabled = false;
                    MessageBox.Show(cmbServiceProviders.Text + " contract data missing");
                }
            }
        }
        private void cmbConstant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbConstant.SelectedIndex) == 2) {
                dConstant.Value = DateTime.Now;
                dConstant.Visible = true;
            }
            else dConstant.Visible = false;
        }
        private void fgSummary_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {

        }
        private void fgSimpleCommands_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (bCheckList)
            {
                if (e.Col == 0) e.Cancel = false;
                else e.Cancel = true;
            }
        }
        private void fgSimpleCommands_AfterEdit(object sender, RowColEventArgs e)
        {
            if (bCheckList)
            {
                sgTemp = 0;                                                                                // <----- Debit
                sgTemp1 = 0;                                                                               // <----- Credit
                for (i = 1; i <= fgSimpleCommands.Rows.Count - 1; i++)
                {
                    if (Convert.ToBoolean(fgSimpleCommands[i, 0])) {
                        if (Global.IsNumeric(fgSimpleCommands[i, "AmountFrom"]))
                            sgTemp = sgTemp + Convert.ToDecimal(fgSimpleCommands[i, "AmountFrom"]);

                        if (Global.IsNumeric(fgSimpleCommands[i, "AmountTo"]))
                            sgTemp1 = sgTemp1 + Convert.ToDecimal(fgSimpleCommands[i, "AmountTo"]);
                    }
                }

                txtAmountFrom.Text = sgTemp.ToString("0.00");
                txtAmountTo.Text = sgTemp1.ToString("0.00");

                if (sgTemp == 0 || sgTemp1 == 0)  btnFinish.Enabled = false;
                else btnFinish.Enabled = true;

                DefineFinishButton();
            }
        }
        private void DefineFinishButton()
        {
            int j = 0;                                                              // j - selected data rows count into fgSimpleCommands grid
            for (i = 1; i <= fgSimpleCommands.Rows.Count - 1; i++) {
                if (Convert.ToBoolean(fgSimpleCommands[i, 0]))
                    j = j + 1;
            }

            switch (Convert.ToInt32(fgSummary[fgSummary.Row, "BusinessType_ID"]))
            {
                case 1:
                    if (j == 1) {
                        btnFinish.Enabled = true;
                        btnFinish.Text = "Διαβίβαση εντολής";
                        panExecutors.Visible = false;
                        panDetails.Visible = false;
                        iPressAction = 1;
                    }
                    else if (j > 1) {
                        btnFinish.Enabled = true;
                        btnFinish.Text = "Create Bulk Order";
                        panExecutors.Visible = true;
                        panDetails.Visible = true;
                        iPressAction = 3;
                    }
                    else {
                        Empty_Selection();
                        btnFinish.Enabled = false;
                        btnFinish.Text = "Διαβίβαση εντολής";
                        iPressAction = 0;
                        panExecutors.Visible = false;
                        panDetails.Visible = false;
                        cmbServiceProviders.DataSource = null;
                    }
                    break;
                case 2:
                    if (j > 0) {
                        btnFinish.Enabled = true;
                        btnFinish.Text = "Create Execution Order";
                        panExecutors.Visible = true;
                        panDetails.Visible = true;
                        iPressAction = 2;
                    }
                    else {
                        Empty_Selection();
                        btnFinish.Enabled = false;
                        btnFinish.Text = "Create Execution Order";
                        iPressAction = 0;
                        panExecutors.Visible = false;
                        panDetails.Visible = false;
                        cmbServiceProviders.DataSource = null;
                    }
                    break;
            }
        }
        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbServiceProviders.SelectedValue) == 0)
                MessageBox.Show("Επιλέξτε έναν πάροχο", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                clsOrdersFX OrderFX = new clsOrdersFX();
                if (iPressAction == 1) {                                                                           // Only Diavivasi
                    for (i = 1; i <= fgSimpleCommands.Rows.Count - 1; i++) {
                        if (Convert.ToBoolean(fgSimpleCommands[i,0])) { 
                            OrderFX = new clsOrdersFX();
                            OrderFX.Record_ID = Convert.ToInt32(fgSimpleCommands[i, "ID"]);
                            OrderFX.GetRecord();
                            OrderFX.SentDate = DateTime.Now;
                            OrderFX.EditRecord();
                        }
                    }
                }
                else {                                                                                              // Create Bulk or Execution Command
                    iID = 0;
                    k = OrderFX.GetNextBulkCommand();
                    k = k + 1;

                    for (i = 1; i <= fgSimpleCommands.Rows.Count - 1; i++) {
                        if (Convert.ToBoolean(fgSimpleCommands[i, 0]))  {
                            OrderFX = new clsOrdersFX();
                            OrderFX.Record_ID = Convert.ToInt32(fgSimpleCommands[i, "ID"]);
                            OrderFX.GetRecord();
                            OrderFX.BulkCommand = "<" + k + ">";
                            OrderFX.Constant = cmbConstant.SelectedIndex;
                            OrderFX.ConstantDate = dConstant.Value.ToString("dd/MM/yyyy");
                            OrderFX.SentDate = DateTime.Now;
                            OrderFX.EditRecord();
                        }
                    }

                    //--- add new Bulk or Execution Command - depend on iChoiceBusinessType_ID ----------------------------------
                    OrderFX = new clsOrdersFX();
                    OrderFX.BulkCommand = "<" + k + ">";
                    OrderFX.BusinessType_ID = 2;

                    if (iChoiceBusinessType_ID == 2) OrderFX.CommandType_ID = 2;    // 2 - Execution
                    else OrderFX.CommandType_ID = 3;                                // 3 - Bulk 

                    OrderFX.Client_ID = 0;
                    OrderFX.Company_ID = Global.Company_ID;
                    OrderFX.StockCompany_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                    OrderFX.StockExchange_ID = 0;
                    OrderFX.CustodyProvider_ID = Global.Company_ID;
                    OrderFX.II_ID = 0;
                    OrderFX.Contract_ID = iNewContract_ID;  
                    OrderFX.Contract_Details_ID = iNewContractDetails_ID;                 
                    OrderFX.Contract_Packages_ID = iNewContractPackages_ID;                
                    OrderFX.Code = sNewCode;
                    OrderFX.Portfolio = sNewPortfolio;
                    OrderFX.AktionDate = DateTime.Now;
                    OrderFX.Tipos = 0;
                    OrderFX.AmountFrom = txtAmountFrom.Text;
                    OrderFX.CurrFrom = cmbCurrFrom.Text;
                    OrderFX.CashAccountFrom_ID = Convert.ToInt32(cmbCashAccFrom.SelectedValue);
                    OrderFX.AmountTo = txtAmountTo.Text;
                    OrderFX.CurrTo = cmbCurrTo.Text;
                    OrderFX.CashAccountTo_ID = Convert.ToInt32(cmbCashAccTo.SelectedValue);
                    OrderFX.Rate = 0;
                    OrderFX.Constant = cmbConstant.SelectedIndex;
                    OrderFX.ConstantDate = dConstant.Value.ToString("dd/MM/yyyy");
                    OrderFX.RecieveDate = DateTime.Now;
                    OrderFX.RecieveMethod_ID = 0;
                    OrderFX.SentDate = Convert.ToDateTime("1900/01/01");
                    OrderFX.ValueDate = "1900/01/01";
                    OrderFX.ExecuteDate = Convert.ToDateTime("1900/01/01");
                    OrderFX.Order_ID = "";
                    OrderFX.InformationMethod_ID = 0;
                    OrderFX.Notes = "";
                    OrderFX.User_ID = Global.User_ID;
                    OrderFX.DateIns = DateTime.Now;
                    OrderFX.Status = 0;
                    iID = OrderFX.InsertRecord();

                    this.Close();
                    frmOrderFX_Execution locOrderFX_Execution = new frmOrderFX_Execution();
                    if (iChoiceBusinessType_ID == 2) {                                    //open  Execution order                    
                        locOrderFX_Execution.Record_ID = iID;
                        //locOrderFX_Execution.CommandType_ID = 2;                        //fgSimpleCommands[fgSimpleCommands.Row, 14)                       ' 2 - Execution Order
                        //locOrderFX_Execution.RightsLevel = 2;
                        locOrderFX_Execution.Editable = 1;
                        locOrderFX_Execution.ShowDialog();
                    }
                    else {                                                                // open Bulk Order
                        locOrderFX_Execution.Record_ID = iID;
                        //locOrderFX_Execution.CommandType_ID = 3;                        //fgSimpleCommands[fgSimpleCommands.Row, 14)                        ' 3 - Bulk Order
                        //locOrderFX_Execution.RightsLevel = 2;
                        locOrderFX_Execution.Editable = 1;
                        locOrderFX_Execution.ShowDialog();
                    }
                }
                
            }
        }
        private void DefineProviderData()
        {
            if (bCheckList)
            {
                clsContracts klsContract = new clsContracts();
                klsContract.PackageType = 2;
                klsContract.DateStart = Convert.ToDateTime("1900/01/01");
                klsContract.DateFinish = Convert.ToDateTime("2071/12/31");
                klsContract.Client_ID = 0;
                klsContract.Advisor_ID = 0;
                klsContract.Service_ID = 0;
                klsContract.Status = -1;
                klsContract.ClientStatus = -1;
                klsContract.GetList();
                foreach (DataRow dtRow in klsContract.List.Rows)
                {
                    if (Convert.ToInt32(dtRow["BrokerageServiceProvider_ID"]) == Convert.ToInt32(cmbServiceProviders.SelectedValue))
                    {
                        sNewCode = dtRow["Code"] + "";
                        sNewPortfolio = dtRow["Portfolio"] + "";
                        iNewContract_ID = Convert.ToInt32(dtRow["ID"]);
                    }
                }
            }
        }
        private string CreateUniqueCode(decimal decDebitAmount, string sDebitCurr, string sCreditAmount)
        {
            //--- create unique code ixxxyyy : i=1 if DebitAmount <> 0, i= 2 if CreditAmount <> 0    xxx - DebitCurr,  yyy - CreditCurr
            return (decDebitAmount != 0 ? "1" : "2") + sDebitCurr + sCreditAmount;
        }
        private void Empty_Selection()
        {
            iChoiceBusinessType_ID = 0;
            cmbServiceProviders.SelectedValue = 0;
        }
        public DateTime Today { get { return dToday; } set { dToday  = value; } }
    }
}
