using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace Core
{
    public partial class frmOrderFX : Form
    {
        int iLastAktion, iRecord_ID, iEditable, iII_ID, iClient_ID, iContract_ID, iStatus, iStockCompany_ID, iStockExchange_ID,
            iCashAccount_From = 0, iCashAccount_To = 0, iRealCashAccount_From = 0, iRealCashAccount_To = 0, iMode;
        string sTemp, sProfitCenter, sMessage;
        float sgTemp, sgTemp1;
        string[] sCheck = { "Δεν ελέγχθηκε", "OK", "Πρόβλημα" };
        DateTime dTemp, dRecieved;
        bool bCheckList, bCashAccounts, bPressedKey;
        SortedList lstRecieved = new SortedList();
        SortedList lstInformed = new SortedList();
        SortedList lstProblems = new SortedList();
        SortedList lstStatus = new SortedList();
        DataTable dtAccsFrom, dtAccsTo;
        DataColumn dtCol;
        DataRow dtRow;
        DataView dtView;

        clsOrdersFX klsOrderFX = new clsOrdersFX();
        clsOrdersFX klsOrderFX2 = new clsOrdersFX();
        clsOrdersFX_Recieved OrdersFX_Recieved = new clsOrdersFX_Recieved();
        clsOrdersFX_Check OrdersFX_Check = new clsOrdersFX_Check();
        clsInformings Informings = new clsInformings();
        public frmOrderFX()
        {
            InitializeComponent();

            this.Width = 962;
            this.Height = 800;

            panNotes.Left = 414;
            panNotes.Top = 36;
        }

        private void frmOrderFX_Load(object sender, EventArgs e)
        {
            this.Text = "Εντολή (" + iRecord_ID + ")";

            bCheckList = false;
            bCashAccounts = false;
            bPressedKey = false;

            iLastAktion = 0;
            iII_ID = 0;
            dRecieved = Convert.ToDateTime("1900/01/01");

            dSend.CustomFormat = "          ";
            dSend.Format = DateTimePickerFormat.Custom;
            dSend.Enabled = false;
            txtSendHour.Enabled = false;
            txtSendMinute.Enabled = false;
            txtSendSecond.Enabled = false;

            dExecute.MaxDate = DateTime.Now;
            dExecute.CustomFormat = "          ";
            dExecute.Format = DateTimePickerFormat.Custom;

            iCashAccount_From = 0;
            iCashAccount_To = 0;
            iRealCashAccount_From = 0;
            iRealCashAccount_To = 0;
            sProfitCenter = "";

            panExecuted.Enabled = false;

            //-------------- Define Senders List ------------------   
            dtView = Global.dtUserList.Copy().DefaultView;
            dtView.RowFilter = "Sender = 1 AND Aktive = 1";
            cmbSenders.DataSource = dtView;
            cmbSenders.DisplayMember = "Title";
            cmbSenders.ValueMember = "ID";
            cmbSenders.SelectedValue = 0;

            //-------------- Define Currencies List ------------------
            cmbCurrFrom.DataSource = Global.dtCurrencies.Copy();
            cmbCurrFrom.DisplayMember = "Title";
            cmbCurrFrom.ValueMember = "ID";

            //-------------- Define Currencies List ------------------
            cmbCurrTo.DataSource = Global.dtCurrencies.Copy();
            cmbCurrTo.DisplayMember = "Title";
            cmbCurrTo.ValueMember = "ID";

            //-------------- Define Currencies List ------------------
            cmbCurrFromReal.DataSource = Global.dtCurrencies.Copy();
            cmbCurrFromReal.DisplayMember = "Title";
            cmbCurrFromReal.ValueMember = "ID";

            //-------------- Define Currencies List ------------------
            cmbCurrToReal.DataSource = Global.dtCurrencies.Copy();
            cmbCurrToReal.DisplayMember = "Title";
            cmbCurrToReal.ValueMember = "ID";

            //-------------- Define Currencies List ------------------
            cmbCurrMain.DataSource = Global.dtCurrencies.Copy();
            cmbCurrMain.DisplayMember = "Title";
            cmbCurrMain.ValueMember = "ID";

            //-------------- Define Currencies List ------------------
            cmbCurrFees1.DataSource = Global.dtCurrencies.Copy();
            cmbCurrFees1.DisplayMember = "Title";
            cmbCurrFees1.ValueMember = "ID";

            //-------------- Define Currencies List ------------------
            cmbCurrFees2.DataSource = Global.dtCurrencies.Copy();
            cmbCurrFees2.DisplayMember = "Title";
            cmbCurrFees2.ValueMember = "ID";

            //------- fgRecieved ----------------------------
            fgRecieved.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgRecieved.Styles.ParseString(Global.GridStyle);
            fgRecieved.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(fgRecieved_CellChanged);
            fgRecieved.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(fgRecieved_CellButtonClick);

            Column col2 = fgRecieved.Cols[2];
            col2.Name = "Image";
            col2.DataType = typeof(String);
            col2.ComboList = "...";

            //------- fgInforming ----------------------------
            fgInforming.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgInforming.Styles.ParseString(Global.GridStyle);
            fgInforming.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(fgInforming_CellChanged);
            fgInforming.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(fgInforming_CellButtonClick);

            Column col21 = fgInforming.Cols[2];
            col21.Name = "Image";
            col21.DataType = typeof(String);
            col21.ComboList = "...";

            //------- fgCheck ----------------------------
            fgCheck.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgCheck.Styles.ParseString(Global.GridStyle);
            fgCheck.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(fgCheck_CellChanged);
            fgCheck.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(fgCheck_CellButtonClick);

            Column col5 = fgCheck.Cols[5];
            col5.Name = "Image";
            col5.DataType = typeof(String);
            col5.ComboList = "...";

            //-------------- Define Recieve Methods List ------------------
            lstRecieved.Clear();
            foreach (DataRow dtRow in Global.dtRecieveMethods.Rows) lstRecieved.Add(dtRow["ID"], dtRow["Title"]);

            fgRecieved.Cols[1].DataMap = lstRecieved;

            //-------------- Define Information Methods List ------------------
            lstInformed.Clear();
            foreach (DataRow dtRow in Global.dtInformMethods.Rows) lstInformed.Add(dtRow["ID"], dtRow["Title"]);

            fgInforming.Cols[1].DataMap = lstInformed;

            //-------------- Define Commands CheckProblems List ------------------
            lstProblems.Clear();
            foreach (DataRow dtRow in Global.dtCheckProblems.Rows) lstProblems.Add(dtRow["ID"], dtRow["Title"]);

            fgCheck.Cols[3].DataMap = lstProblems;

            lstStatus.Clear();
            lstStatus.Add("0", "");
            lstStatus.Add("1", sCheck[1]);
            lstStatus.Add("2", sCheck[2]);
            fgCheck.Cols[2].DataMap = lstStatus;

            //---- Start Initialisation - Show Command --------------
            klsOrderFX.Record_ID = iRecord_ID;
            klsOrderFX.GetRecord();

            if (klsOrderFX.CommandType_ID == 1) {
                iClient_ID = klsOrderFX.Client_ID;
                iContract_ID = klsOrderFX.Contract_ID;
                lblPelatis.Text = klsOrderFX.ClientName;
                lblContractTitle.Text = klsOrderFX.ContractTitle;
            }
            else {
                iClient_ID = 0;
                iContract_ID = klsOrderFX.Contract_ID;
                lblPelatis.Text = "HellasFin";
                lblContractTitle.Text = klsOrderFX.ContractTitle;
            }
            lblCode.Text = klsOrderFX.Code;
            dAktionDate.Value = klsOrderFX.AktionDate;
            iStockCompany_ID = klsOrderFX.StockCompany_ID;
            lblStockCompany.Text = klsOrderFX.StockCompany_Title;

            iStockExchange_ID = klsOrderFX.StockExchange_ID;
            sProfitCenter = klsOrderFX.Portfolio;

            iCashAccount_From = klsOrderFX.CashAccountFrom_ID;
            iCashAccount_To = klsOrderFX.CashAccountTo_ID;
            iRealCashAccount_From = klsOrderFX.RealCashAccountFrom_ID;
            iRealCashAccount_To = klsOrderFX.RealCashAccountTo_ID;
            cmbCurrMain.Text = klsOrderFX.MainCurr;

            txtAmountFrom.Text = klsOrderFX.AmountFrom;
            cmbCurrFrom.Text = klsOrderFX.CurrFrom;
            txtAmountTo.Text = klsOrderFX.AmountTo;
            cmbCurrTo.Text = klsOrderFX.CurrTo;
            cmbType.SelectedIndex = klsOrderFX.Tipos;
            cmbConstant.SelectedIndex = klsOrderFX.Constant;
            dConstant.Value = (klsOrderFX.ConstantDate + "" != "" ? Convert.ToDateTime(klsOrderFX.ConstantDate) : DateTime.Now);
            txtRate.Text = klsOrderFX.Rate.ToString("0.00##");

            txtAmountFromReal.Text = klsOrderFX.RealAmountFrom.ToString("0.00##");
            txtAmountToReal.Text = klsOrderFX.RealAmountTo.ToString("0.00##");
            lblRateReal.Text = klsOrderFX.CurrFrom + "/" + klsOrderFX.CurrTo;
            txtRateReal.Text = klsOrderFX.RealCurrRate.ToString("0.00##########");

            txtFeesPercent.Text = klsOrderFX.FeesPercent.ToString("0.00##");
            lblFeesRate.Text = cmbCurrMain.Text + "/" + cmbCurrFees1.Text;
            txtFeesRate.Text = klsOrderFX.FeesRate.ToString("0.00##########");
            txtFeesAmount.Text = klsOrderFX.FeesAmount.ToString("0.00##");

            if (cmbType.SelectedIndex != 1) {      // not Market
                if (cmbCurrFrom.Text != "EUR" && Global.IsNumeric(txtAmountFrom.Text)) {
                    lblAmount_EUR.Text = Global.ConvertAmount(Convert.ToDecimal(txtAmountFrom.Text), cmbCurrFrom.Text, "EUR", dAktionDate.Value) + " EUR";
                    panAmount_EUR.Visible = true;
                }
                else {
                    if (cmbCurrTo.Text != "EUR" && Global.IsNumeric(txtAmountTo.Text)) {
                        lblAmount_EUR.Text = Global.ConvertAmount(Convert.ToDecimal(txtAmountTo.Text), cmbCurrFrom.Text, "EUR", dAktionDate.Value) + " EUR";
                        panAmount_EUR.Visible = true;
                    }
                }
            }

            if (klsOrderFX.SentDate != Convert.ToDateTime("1900/01/01")) {
                dSend.CustomFormat = "dd/MM/yyyy";
                dTemp = klsOrderFX.SentDate;
                dSend.Value = dTemp.Date;
                txtSendHour.Text = dTemp.Hour.ToString("00");
                txtSendMinute.Text = dTemp.Minute.ToString("00");
                txtSendSecond.Text = dTemp.Second.ToString("00");
            }
            else {
                dSend.CustomFormat = "          ";
                dSend.Format = DateTimePickerFormat.Custom;
                dSend.Value = Convert.ToDateTime("1900/01/01");
                txtSendHour.Text = "";
                txtSendMinute.Text = "";
                txtSendSecond.Text = "";
            }

            if (klsOrderFX.BusinessType_ID == 1) {
                picEmptySend.Visible = true;
                dSend.Enabled = true;
                txtSendHour.Enabled = true;
                txtSendMinute.Enabled = true;
                txtSendSecond.Enabled = true;
                btnSend.Enabled = true;

                lblWarning.Visible = false;
                panWarning.Visible = false;
            }
            else {
                picEmptySend.Visible = false;
                txtSendHour.Enabled = false;
                txtSendMinute.Enabled = false;
                txtSendSecond.Enabled = false;
                btnSend.Enabled = false;
                lblWarning.Visible = true;
                panWarning.Visible = true;
            }

            dTemp = Convert.ToDateTime(klsOrderFX.ValueDate);
            if (klsOrderFX.ValueDate != "1900/01/01") {
                dValueDate.CustomFormat = "dd/MM/yyyy";
                dValueDate.Value = dTemp;
            }
            else {
                dValueDate.Value = dTemp;
                dValueDate.CustomFormat = "          ";
                dValueDate.Format = DateTimePickerFormat.Custom;
            }

            dRecieved = klsOrderFX.RecieveDate;

            dTemp = klsOrderFX.ExecuteDate;
            if (klsOrderFX.ExecuteDate.Date != Convert.ToDateTime("1900/01/01").Date) {
                dExecute.CustomFormat = "dd/MM/yyyy";
                dExecute.Value = dTemp.Date;
                txtExecuteHour.Text = dTemp.Hour.ToString("00");
                txtExecuteMinute.Text = dTemp.Minute.ToString("00");
                txtExecuteSecond.Text = dTemp.Second.ToString("00");
                dExecute.Enabled = true;
                txtExecuteHour.Enabled = true;
                txtExecuteMinute.Enabled = true;
                txtExecuteSecond.Enabled = true;
            }
            else {
                picEmptyExecute.Visible = false;
                dExecute.CustomFormat = "          ";
                dExecute.Format = DateTimePickerFormat.Custom;
                dExecute.Value = Convert.ToDateTime("1900/01/01");
                txtExecuteHour.Text = "";
                txtExecuteMinute.Text = "";
                txtExecuteSecond.Text = "";
                dExecute.Enabled = false;
                txtExecuteHour.Enabled = false;
                txtExecuteMinute.Enabled = false;
                txtExecuteSecond.Enabled = false;
            }

            if (klsOrderFX.RealAmountFrom != 0 || klsOrderFX.RealAmountTo != 0 || klsOrderFX.RealCurrRate != 0) {
                panExecuted.Enabled = true;
                if (Convert.ToDateTime(klsOrderFX.ExecuteDate) != Convert.ToDateTime("01/01/1900")) {
                    picEmptyExecute.Visible = true;
                    picEmptyExecute.Enabled = true;
                    txtAmountFromReal.Text = klsOrderFX.RealAmountFrom.ToString("0.#######");
                    txtAmountToReal.Text = klsOrderFX.RealAmountTo.ToString("0.#######");
                    txtFees1.Text = (Convert.ToDecimal(txtAmountFromReal.Text) * Convert.ToDecimal(txtFeesPercent.Text) / 100).ToString("0.00");
                    txtFees2.Text = (Convert.ToDecimal(txtAmountToReal.Text) * Convert.ToDecimal(txtFeesPercent.Text) / 100).ToString("0.00");
                    btnExecuted.Enabled = false;
                }
                else btnExecuted.Enabled = true;
            }
            else {
                panExecuted.Enabled = false;
                btnExecuted.Enabled = true;
            }

            txtNotes.Text = klsOrderFX.Notes;
            cmbSenders.SelectedValue = klsOrderFX.User_ID;

            lblRTO_Curr.Text = klsOrderFX.CurrFrom;
            lblRTO_FeesPercent.Text = klsOrderFX.RTO_FeesPercent.ToString("0.00");
            txtRTO_DiscountPercent.Text = klsOrderFX.RTO_DiscountPercent.ToString("0.00");
            lblRTO_FinishFeesPercent.Text = klsOrderFX.RTO_FinishFeesPercent.ToString("0.00");
            lblRTO_FeesAmount.Text = klsOrderFX.RTO_FeesAmount.ToString("0.00");
            lblFeesRate_Title.Text = "Ισοτιμία EUR/" + lblRTO_Curr.Text;
            if (lblRTO_Curr.Text == "EUR") klsOrderFX.RTO_FeesRate = "1";
            txtRTO_FeesCurrRate.Text = klsOrderFX.RTO_FeesRate;
            lblRTO_FeesAmountEUR.Text = klsOrderFX.RTO_FeesAmountEUR.ToString("0.00");

            if (klsOrderFX.Status >= 0) {
                tslCancel.Text = "Ακύρωση εντολής";
                sMessage = "ΠΡΟΣΟΧΗ! Ζητήσατε να ακυρωθεί η εντολή. \n Είστε σίγουρος για την ακύρωση της;";
                iStatus = -1;
            }
            else {
                tslCancel.Text = "Επαναφορά εντολής";
                sMessage = "ΠΡΟΣΟΧΗ! Ζητήσατε να επαναφερθεί η εντολή.\n Είστε σίγουρος για την επαναφορά της;";
                iStatus = 0;
            } 

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


            clsContracts_CashAccounts ClientCashAccounts = new clsContracts_CashAccounts();
            ClientCashAccounts.Client_ID = 0;
            ClientCashAccounts.Code = lblCode.Text;
            ClientCashAccounts.Contract_ID = 0;
            ClientCashAccounts.GetList_CashAccount();
            foreach (DataRow dtRow1 in ClientCashAccounts.List.Rows)
            {
                dtRow = dtAccsTo.NewRow();
                dtRow["ID"] = dtRow1["ID"];
                dtRow["AccountNumber"] = dtRow1["AccountNumber"] + " / " + dtRow1["Currency"];
                dtRow["Currency"] = dtRow1["Currency"];
                dtAccsTo.Rows.Add(dtRow);

                //if (Convert.ToInt32(dtRow1["Contract_ID"]) == iContract_ID) {
                    dtRow = dtAccsFrom.NewRow();
                    dtRow["ID"] = dtRow1["ID"];
                    dtRow["AccountNumber"] = dtRow1["AccountNumber"] + " / " + dtRow1["Currency"];
                    dtRow["Currency"] = dtRow1["Currency"];
                    dtAccsFrom.Rows.Add(dtRow);
                //}
            }

            cmbCashAccFrom.DataSource = dtAccsFrom.Copy();
            cmbCashAccFrom.DisplayMember = "AccountNumber";
            cmbCashAccFrom.ValueMember = "ID";

            cmbCashAccTo.DataSource = dtAccsTo.Copy();
            cmbCashAccTo.DisplayMember = "AccountNumber";
            cmbCashAccTo.ValueMember = "ID";

            cmbCashAccFrom.SelectedValue = iCashAccount_From;
            cmbCashAccTo.SelectedValue = iCashAccount_To;

            lblPortfolio.Text = sProfitCenter;

            fgRecieved.Redraw = false;

            //-------------- Define Recieved Files List ------------------
            klsOrderFX2 = new clsOrdersFX();
            klsOrderFX2.Record_ID = iRecord_ID;
            klsOrderFX2.GetRecievedFiles();

            fgRecieved.Redraw = false;
            fgRecieved.Rows.Count = 1;
            foreach (DataRow dtRow in klsOrderFX2.List.Rows)
                fgRecieved.AddItem(dtRow["DateIns"] + "\t" + dtRow["Method_Title"] + "\t" + dtRow["FileName"] + "\t" +
                                   dtRow["ID"] + "\t" + dtRow["Method_ID"] + "\t" + "");                                       //drList("FilePath")

            fgRecieved.Redraw = true;

            //-------------- Define Informings List -----------------
            klsOrderFX2 = new clsOrdersFX();
            klsOrderFX2.Record_ID = iRecord_ID;
            klsOrderFX2.GetInformings();

            fgInforming.Redraw = false;
            fgInforming.Rows.Count = 1;
            foreach (DataRow dtRow in klsOrderFX2.List.Rows)
                fgInforming.AddItem(dtRow["DateIns"] + "\t" + dtRow["InformationMethod"] + "\t" + dtRow["FileName"] + "\t" +
                                    dtRow["DateSent"] + "\t" + dtRow["ID"] + "\t" + dtRow["InformMethod"] + "\t" + dtRow["User_ID"] + "\t" + "");

            fgInforming.Redraw = true;


            //-------------- Define Check List -----------------
            klsOrderFX2 = new clsOrdersFX();
            klsOrderFX2.Record_ID = iRecord_ID;
            klsOrderFX2.GetChecks();

            fgCheck.Redraw = false;
            fgCheck.Rows.Count = 1;
            foreach (DataRow dtRow in klsOrderFX2.List.Rows)
                fgCheck.AddItem(dtRow["DateIns"] + "\t" + dtRow["UserName"] + "\t" + sCheck[Convert.ToInt32(dtRow["Status"])] + "\t" +
                                        dtRow["ProblemType_Title"] + "\t" + dtRow["Notes"] + "\t" + dtRow["FileName"] + "\t" +
                                        dtRow["ReversalRequestDate"] + "\t" + dtRow["ID"] + "\t" + dtRow["User_ID"] + "\t" +
                                        dtRow["Status"] + "\t" + "" + "\t" + dtRow["ProblemType_ID"]);                // preLast Column - Empty, it's shows that it "old" file. "New" file has full path of file

            fgCheck.Redraw = true;


            //-------  read relational InvestIdees_Commands --------------------
            clsInvestIdees_Commands InvestIdees_Commands = new clsInvestIdees_Commands();
            InvestIdees_Commands.Command_ID = iRecord_ID;
            InvestIdees_Commands.GetRecord();
            iII_ID = InvestIdees_Commands.II_ID;

            if (iII_ID == 0) tslInvestProposals.Enabled = false;
            else tslInvestProposals.Enabled = true;

            if (iEditable == 0) {
                pan1.Enabled = false;
                pan3.Enabled = false;
                pan4.Enabled = false;
                picEmptySend.Enabled = false;
                tslCancel.Enabled = false;
                panSend.Enabled = false;
                picEmptyExecute.Enabled = false;
                tsbSave.Visible = false;
                tsbKey.Visible = false;
            }
            else {
                if (klsOrderFX.BusinessType_ID == 2)
                {
                    pan1.Enabled = false;
                    pan3.Enabled = false;
                    pan4.Enabled = false;
                    picEmptySend.Enabled = false;
                    tslCancel.Enabled = false;
                    panSend.Enabled = false;
                    picEmptyExecute.Enabled = false;
                    tsbSave.Visible = true;
                    tsbKey.Visible = true;
                }
                else
                {
                    pan1.Enabled = true;
                    pan3.Enabled = true;
                    pan4.Enabled = true;
                    picEmptySend.Enabled = true;
                    tslCancel.Enabled = true;
                    panSend.Enabled = true;
                    picEmptyExecute.Enabled = true;
                    tsbSave.Visible = true;
                    tsbKey.Visible = false;
                }
            }

            if (iMode == 2) {                                          // 1 - from frmDailyFX, 2 - from frmAcc_InvoicesFX
                tsbSave.Visible = true;
                tsbKey.Visible = false;
                pan1.Enabled = false;
                pan2.Enabled = false;
                pan3.Enabled = false;
                pan4.Enabled = false;
                pan5.Enabled = false;
                pan6.Enabled = false;
                pan7.Enabled = false;
                pan8.Enabled = true;
                panCalcFees2.Enabled = true;
            }
            else {
                pan8.Enabled = false;
                panCalcFees2.Enabled = false;
            }
            bCheckList = true;
            bCashAccounts = true;
        }

        private void picCalcFees_Click(object sender, EventArgs e)
        {            
            CalcFees();
        }
        private void txtHistoryNotes_TextChanged(object sender, EventArgs e)
        {
            if (txtHistoryNotes.Text.Length > 0) btnOK_Save.Enabled = true;
            else btnOK_Save.Enabled = false;
        }
        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (bPressedKey) {

                txtCurrentValues.Text = sTemp;
                txtHistoryNotes.Text = "";
                btnOK_Save.Enabled = false;
                panNotes.Visible = true;
            }
            else
            {
                SaveRecord();
                this.Close();
                iLastAktion = 1;                               //1 - was saved (added)
            }
        }
        private void btnOK_Save_Click(object sender, EventArgs e)
        {
            SaveRecord();
            this.Close();
            iLastAktion = 1;                                     //1 - was saved (added)
        }
        private float DefineFeesPercent()
        {
            float sgTemp = 0;

            clsClientsFXFees ClientsFXFees = new clsClientsFXFees();
            ClientsFXFees.Contract_ID = iContract_ID;
            ClientsFXFees.AktionDate = dAktionDate.Value;
            ClientsFXFees.GetList_Contract_ID();
            foreach (DataRow dtRow in ClientsFXFees.List.Rows)
                sgTemp = Convert.ToSingle(dtRow["FinishFXFees"]);

            return sgTemp;
        }

        private void dValueDate_ValueChanged(object sender, EventArgs e)
        {
            dValueDate.CustomFormat = "dd/MM/yyyy";
        }

        private void cmbCashAccFrom_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCashAccounts)
                if (cmbCashAccFrom.SelectedValue == cmbCashAccTo.SelectedValue)
                    MessageBox.Show(Global.GetLabel("cash_account_warning"), "DB Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            lblCashAccFromReal.Text = cmbCashAccFrom.Text;
        }

        private void cmbCashAccTo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCashAccounts)
                if (cmbCashAccFrom.SelectedValue == cmbCashAccTo.SelectedValue)
                    MessageBox.Show(Global.GetLabel("cash_account_warning"), "DB Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            lblCashAccToReal.Text = cmbCashAccTo.Text;
        }

        private void CalcFees()
        {
            sgTemp = 0;

            txtFeesPercent.Text = DefineFeesPercent().ToString("0.00");

            if (Global.IsNumeric(txtAmountFromReal.Text) && Global.IsNumeric(txtFeesPercent.Text))
               txtFees1.Text = (Convert.ToSingle(txtAmountFromReal.Text) * Convert.ToSingle(txtFeesPercent.Text) / 100).ToString("0.00");

            if (Global.IsNumeric(txtAmountToReal.Text) && Global.IsNumeric(txtFeesPercent.Text))
               txtFees2.Text =(Convert.ToSingle(txtAmountToReal.Text) * Convert.ToSingle(txtFeesPercent.Text) / 100).ToString("0.00");

            if (cmbCurrFees1.Text != cmbCurrMain.Text) {
                clsSystem System = new clsSystem();
                System.AktionDate = dAktionDate.Value;
                System.CurrFrom = cmbCurrFees1.Text;
                System.CurrTo = cmbCurrMain.Text;
                System.GetConvertAmount();
                sgTemp = System.CurrencyRate;
            }
            else sgTemp = 1;

            lblFeesRate.Text = cmbCurrMain.Text + "/" + cmbCurrFees1.Text;
            txtFeesRate.Text = sgTemp.ToString("0.0000");
            txtFeesAmount.Text = (Convert.ToSingle(txtFees1.Text) * sgTemp).ToString("0.00");
        }
        private void SaveRecord()
        {
            int i;
            string sNewFileName = "";
            //--- At begining system saves fgRecieved, fgInforming and fgCheck records, because  names of upload files can change. So in Command record will save new file names 
            for (i = 1; i <= fgRecieved.Rows.Count - 1; i++) {

                sNewFileName = (fgRecieved[i, 2] + "").Trim();
                if ((fgRecieved[i, 5] + "") != "")  {
                    sNewFileName = Global.DMS_UploadFile(fgRecieved[i, 5] + "", "Customers/" + lblContractTitle.Text.Replace(".", "_") + "/OrdersAcception", sNewFileName);
                    if (sNewFileName.Length > 0) sNewFileName = Path.GetFileName(sNewFileName);
                    else
                        MessageBox.Show("Αρχείο " + fgRecieved[i, 2] + " δεν αντιγράφτηκε στο DMS", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }

                if (Convert.ToInt32(fgRecieved[i, 3]) == 0) {
                    OrdersFX_Recieved = new clsOrdersFX_Recieved();
                    OrdersFX_Recieved.CommandFX_ID = iRecord_ID;
                    OrdersFX_Recieved.DateIns = Convert.ToDateTime(fgRecieved[i, 0]);
                    OrdersFX_Recieved.Method_ID = Convert.ToInt32(fgRecieved[i, 4]);
                    OrdersFX_Recieved.FilePath = fgRecieved[i, 5] + "";
                    OrdersFX_Recieved.FileName = sNewFileName;
                    OrdersFX_Recieved.InsertRecord();
                }
                else {
                    OrdersFX_Recieved.Record_ID = Convert.ToInt32(fgRecieved[i, 3]);
                    OrdersFX_Recieved.GetRecord();
                    OrdersFX_Recieved.CommandFX_ID = iRecord_ID;
                    OrdersFX_Recieved.DateIns = Convert.ToDateTime(fgRecieved[i, 0]);
                    OrdersFX_Recieved.Method_ID = Convert.ToInt32(fgRecieved[i, 4]);
                    OrdersFX_Recieved.FilePath = fgRecieved[i, 5] + "";
                    OrdersFX_Recieved.FileName = sNewFileName;
                    OrdersFX_Recieved.EditRecord();
                }
            }


            for (i = 1; i <= fgCheck.Rows.Count - 1; i++) {

                if ((fgCheck[i, 10] + "").Trim() != "") {                                     // FileFullName - Not Empty means that it's a new file
                    sTemp = Global.DMS_UploadFile(fgCheck[i, 10] + "", "Customers/" + lblContractTitle.Text.Replace(".", "_") + "/Informing", fgCheck[i, 5] + "");
                    fgCheck[i, 5] = Path.GetFileName(sTemp);
                }

                if (Convert.ToInt32(fgCheck[i, "ID"]) == 0) {
                    OrdersFX_Check = new clsOrdersFX_Check();
                    OrdersFX_Check.CommandFX_ID = iRecord_ID;
                    OrdersFX_Check.DateIns = Convert.ToDateTime(fgCheck[i, 0]);
                    OrdersFX_Check.User_ID = Convert.ToInt32(fgCheck[i, 8]);
                    OrdersFX_Check.Status = Convert.ToInt32(fgCheck[i, 9]);
                    OrdersFX_Check.ProblemType_ID = Convert.ToInt32(fgCheck[i, 11]);
                    OrdersFX_Check.Notes = fgCheck[i, 4] + "";
                    OrdersFX_Check.FileName = fgCheck[i, 5] + "";
                    OrdersFX_Check.ReversalRequestDate = fgCheck[i, 6] + "";
                    OrdersFX_Check.InsertRecord();
                }
                else {
                    OrdersFX_Check.Record_ID = Convert.ToInt32(fgCheck[i, "ID"]);
                    OrdersFX_Check.GetRecord();
                    OrdersFX_Check.CommandFX_ID = iRecord_ID;
                    OrdersFX_Check.DateIns = Convert.ToDateTime(fgCheck[i, 0]);
                    OrdersFX_Check.User_ID = Convert.ToInt32(fgCheck[i, 8]);
                    OrdersFX_Check.Status = Convert.ToInt32(fgCheck[i, 9]);
                    OrdersFX_Check.ProblemType_ID = Convert.ToInt32(fgCheck[i, 11]);
                    OrdersFX_Check.Notes = fgCheck[i, 4] + "";
                    OrdersFX_Check.FileName = fgCheck[i, 5] + "";
                    OrdersFX_Check.ReversalRequestDate = fgCheck[i, 6] + "";
                    OrdersFX_Check.EditRecord();
                }
            }


            for (i = 1; i <= fgInforming.Rows.Count - 1; i++) {
                if ((fgInforming[i, 7] + "").Trim() != "") {  // Not Empty means that it's a new file

                    sTemp = Global.DMS_UploadFile(fgInforming[i, 7] + "", "Customers/" + lblContractTitle.Text.Replace(".", "_") + "/Informing", fgInforming[i, 2] + "");
                    fgInforming[i, 2] = Path.GetFileName(sTemp);

                    if ((fgInforming[i, 4] + "") == "0")
                        Global.AddInformingRecord(1, iRecord_ID, Convert.ToInt32(fgInforming[i, 5]), 5, klsOrderFX.Client_ID, iContract_ID, "", "",
                                           Global.GetLabel("update_execution_command"), "", fgInforming[i, 2] + "", "", DateTime.Now.ToString(), 1, 1, "");
                    else {
                        Informings.Record_ID = Convert.ToInt32(fgInforming[i, 4]);
                        Informings.InformMethod = Convert.ToInt32(fgInforming[i, 5]);
                        Informings.DateIns = Convert.ToDateTime(fgInforming[i, 0]);
                        Informings.EditRecord();
                    }
                }
            }

            //--- Edit Command ----------------------------------
            klsOrderFX.Record_ID = iRecord_ID;
            klsOrderFX.Code = lblCode.Text;
            klsOrderFX.Portfolio = lblPortfolio.Text;
            klsOrderFX.AktionDate = dAktionDate.Value;
            klsOrderFX.Tipos = cmbType.SelectedIndex;
            klsOrderFX.AmountFrom = txtAmountFrom.Text;
            klsOrderFX.CurrFrom = cmbCurrFrom.Text;
            klsOrderFX.CashAccountFrom_ID = Convert.ToInt32(cmbCashAccFrom.SelectedValue);
            klsOrderFX.AmountTo = txtAmountTo.Text;
            klsOrderFX.CurrTo = cmbCurrTo.Text;
            klsOrderFX.CashAccountTo_ID = Convert.ToInt32(cmbCashAccTo.SelectedValue);
            klsOrderFX.Rate = Convert.ToDecimal(txtRate.Text);
            klsOrderFX.Constant = cmbConstant.SelectedIndex;
            klsOrderFX.ConstantDate = (cmbConstant.SelectedIndex == 2 ? dConstant.Value.ToString("dd/MM/yyyy") : "");

            i = 0;
            sTemp = "";
            if (fgCheck.Rows.Count > 1)
            {
                i = Convert.ToInt32(fgCheck[1, 9]);                      // Status
                sTemp = fgCheck[1, 5] + "";
            }
            klsOrderFX.Pinakidio = i;
            klsOrderFX.LastCheckFile = sTemp;
            dTemp = dRecieved;
            i = 0;
            if (fgRecieved.Rows.Count > 1)
            {
                dTemp = Convert.ToDateTime(fgRecieved[1, 0]);   //   last recieved file date
                i = Convert.ToInt32(fgRecieved[1, 4]);          //   last recieved file method
            }

            klsOrderFX.RecieveDate = dTemp;
            klsOrderFX.RecieveMethod_ID = i;
            if (dSend.Value.Date != Convert.ToDateTime("1900/01/01").Date) {
                sTemp = dSend.Value.ToString("yyyy/MM/dd") + " " + (txtSendHour.Text.Trim() == "" ? "00" : txtSendHour.Text.Trim()) + ":" +
                                                                (txtSendMinute.Text.Trim() == "" ? "00" : txtSendMinute.Text.Trim()) + ":" +
                                                                (txtSendSecond.Text.Trim() == "" ? "00" : txtSendSecond.Text.Trim());
            }
            else sTemp = "1900/01/01 00:00:00";

            klsOrderFX.SentDate = Convert.ToDateTime(sTemp);

            klsOrderFX.ValueDate = dValueDate.Value.ToString("yyyy/MM/dd");

            sTemp = "1900/01/01 00:00:00";
            sTemp = dExecute.Value.ToString("yyyy/MM/dd") + " " + (txtExecuteHour.Text.Trim() == "" ? "00" : txtExecuteHour.Text.Trim()) + ":" +
                                                                  (txtExecuteMinute.Text.Trim() == "" ? "00" : txtExecuteMinute.Text.Trim()) + ":" +
                                                                  (txtExecuteSecond.Text.Trim() == "" ? "00" : txtExecuteSecond.Text.Trim());

            klsOrderFX.ExecuteDate = Convert.ToDateTime(sTemp);

            klsOrderFX.RealAmountFrom = Convert.ToDecimal(txtAmountFromReal.Text);
            klsOrderFX.RealCashAccountFrom_ID = Convert.ToInt32(cmbCashAccFrom.SelectedValue);
            klsOrderFX.RealAmountTo = Convert.ToDecimal(txtAmountToReal.Text);
            klsOrderFX.RealCashAccountTo_ID = Convert.ToInt32(cmbCashAccTo.SelectedValue);
            klsOrderFX.RealCurrRate = Convert.ToDouble(txtRateReal.Text);
            klsOrderFX.FeesPercent = Convert.ToDouble(txtFeesPercent.Text);
            klsOrderFX.FeesRate = Convert.ToDouble(txtFeesRate.Text);
            klsOrderFX.FeesAmount = Convert.ToDouble(txtFeesAmount.Text);

            RecalRTOFees();
            klsOrderFX.RTO_FeesPercent = Convert.ToSingle(lblRTO_FeesPercent.Text);
            klsOrderFX.RTO_DiscountPercent = Convert.ToSingle(txtRTO_DiscountPercent.Text);
            klsOrderFX.RTO_FinishFeesPercent = Convert.ToSingle(lblRTO_FinishFeesPercent.Text);
            klsOrderFX.RTO_FeesAmount = Convert.ToSingle(lblRTO_FeesAmount.Text);
            klsOrderFX.RTO_FeesRate = txtRTO_FeesCurrRate.Text;
            klsOrderFX.RTO_FeesAmountEUR = Convert.ToSingle(lblRTO_FeesAmountEUR.Text);

            i = 0;
            if (fgInforming.Rows.Count > 1) i = Convert.ToInt32(fgInforming[1, 5]);
            klsOrderFX.InformationMethod_ID = i;
            klsOrderFX.Notes = txtNotes.Text;
            klsOrderFX.User_ID = Convert.ToInt32(cmbSenders.SelectedValue);

            klsOrderFX.EditRecord();


            /*
            //--- Add History Record ---
            clsHistory klsHistory = new clsHistory();
            klsHistory.RecType = 10;
            klsHistory.SrcRec_ID = iRecord_ID;
            klsHistory.Client_ID = 0;
            klsHistory.Contract_ID = 0;
            klsHistory.Action = 0;
            klsHistory.CurrentValues = "";
            klsHistory.DocFiles_ID = 0;
            klsHistory.Notes = "";
            klsHistory.User_ID = Global.User_ID;
            klsHistory.DateIns = DateTime.Now;
            klsHistory.InsertRecord();
            */

            //--- define CompanyFeesPercent --------------
            sgTemp = 0;
            if (Global.IsNumeric(txtAmountFromReal.Text))
                sgTemp = Convert.ToSingle(txtAmountFromReal.Text);

            if (sgTemp == 0)
                if (Global.IsNumeric(txtAmountToReal.Text))
                    sgTemp = Convert.ToSingle(txtAmountToReal.Text);


            /*
        sgCompanyFeesPercent = 0
        With comm
            .Connection = cn
            .CommandText = "GetServiceProviderFXFees_ClientPackage"
            .CommandType = CommandType.StoredProcedure
            .Parameters.Clear()
        End With
        prmSQL = comm.Parameters.AddWithValue("@ClientPackage_ID", iClientPackage_ID)
        prmSQL = comm.Parameters.AddWithValue("@DateFees", dAktionDate.Value)
        prmSQL = comm.Parameters.AddWithValue("@Amount", sgTemp)
        drList = comm.ExecuteReader()
        While drList.Read
            sgCompanyFeesPercent = drList("RetrosessionCompany")
        End While
        drList.Close()

        prmSQL = comm.Parameters.AddWithValue("@AmountFrom", txtAmountFrom.Text)
        prmSQL = comm.Parameters.AddWithValue("@CurrFrom", cmbCurrFrom.Text)
        prmSQL = comm.Parameters.AddWithValue("@CashAccountFrom_ID", cmbCashAccFrom.SelectedValue)
        prmSQL = comm.Parameters.AddWithValue("@AmountTo", txtAmountTo.Text)
        prmSQL = comm.Parameters.AddWithValue("@CurrTo", cmbCurrTo.Text)
        prmSQL = comm.Parameters.AddWithValue("@CashAccountTo_ID", cmbCashAccTo.SelectedValue)
        prmSQL = comm.Parameters.AddWithValue("@Tipos", cmbType.SelectedIndex)
        prmSQL = comm.Parameters.AddWithValue("@Rate", CSng(txtRate.Text))
        prmSQL = comm.Parameters.AddWithValue("@ConstantDate", dConstant.Value)
*/



            this.Close();
            iLastAktion = 1;            // was saved (added)
        }
        #region --- Edit functions ------------------------------------------------------  
        private void cmbCurrFrom_SelectedValueChanged(object sender, EventArgs e)
        {
            cmbCurrFromReal.SelectedValue = cmbCurrFrom.SelectedValue;
            cmbCurrFees1.SelectedValue = cmbCurrFrom.SelectedValue;

            if (bCheckList) {
                bCashAccounts = false;
                dtView = dtAccsFrom.DefaultView;
                dtView.RowFilter = "Currency = '' OR Currency = '" + cmbCurrFrom.Text + "'";
                cmbCashAccFrom.DataSource = dtView;
                cmbCashAccFrom.DisplayMember = "AccountNumber";
                cmbCashAccFrom.ValueMember = "ID";
                bCashAccounts = true;
            }
        }
        private void cmbCurrTo_SelectedValueChanged(object sender, EventArgs e)
        {
            cmbCurrToReal.SelectedValue = cmbCurrTo.SelectedValue;
            cmbCurrFees2.SelectedValue = cmbCurrTo.SelectedValue;

            if (bCheckList) {
                bCashAccounts = false;
                dtView = dtAccsTo.DefaultView;
                dtView.RowFilter = "Currency = '' OR Currency = '" + cmbCurrTo.Text + "'";
                cmbCashAccTo.DataSource = dtView;
                cmbCashAccTo.DisplayMember = "AccountNumber";
                cmbCashAccTo.ValueMember = "ID";
                bCashAccounts = true;
            }
        }
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbType.SelectedIndex == 0) {
               cmbConstant.SelectedIndex = 0;
               dConstant.Visible = false;
            }
            else {
               cmbConstant.SelectedIndex = 0;
               dConstant.Visible = false;
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
 
        private void picAddRecieved_Click(object sender, EventArgs e)
        {
            fgRecieved.AddItem(Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy HH:mm:ss") + "\t" + "" + "\t" + "" + "\t" + "0" + "\t" + "0" + "\t" + "", 1);
            if (klsOrderFX.SentDate != Convert.ToDateTime("1900/01/01"))
                if (klsOrderFX.SentDate < DateTime.Now)
                    MessageBox.Show("Wrong Date: Ημερομηνία Λήψης δεν μπορεί να είναι μεγαλίτερη απο Ημερομηνία Διαβίβασης", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void picDeleteRecieved_Click(object sender, EventArgs e)
        {
            if (fgRecieved.Row > 0) {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    clsOrdersFX_Recieved OrdersFX_Recieved = new clsOrdersFX_Recieved();
                    OrdersFX_Recieved.Record_ID = Convert.ToInt32(fgRecieved[fgRecieved.Row, "ID"]);
                    OrdersFX_Recieved.DeleteRecord();
                    fgRecieved.RemoveItem(fgRecieved.Row);
                }
            }
        }
        private void picShowRecieved_Click(object sender, EventArgs e)
        {
            if ((fgRecieved[fgRecieved.Row, "FilePath"] + "").Trim() != "")
                System.Diagnostics.Process.Start(fgRecieved[fgRecieved.Row, "FilePath"] + "");
            else
               if ((fgRecieved[fgRecieved.Row, 2] + "").Trim() != "")
                  Global.DMS_ShowFile("Customers/" + klsOrderFX.ClientName + "/OrdersAcception", (fgRecieved[fgRecieved.Row, 2] + ""));
        }

        private void picCopyRecievedClipboard_Click(object sender, EventArgs e)
        {
            if (!Convert.IsDBNull(Clipboard.GetText()))
            {
                sTemp = "";
                if (fgRecieved[fgRecieved.Row, 2] + "" != "") sTemp = Global.DocFilesPath_FTP + "/" + "Customers/" + lblContractTitle.Text.Replace(".", "_") +
                                                                      "/OrdersAcception/" + fgRecieved[fgRecieved.Row, 2];
                Clipboard.SetText(fgRecieved[fgRecieved.Row, 0] + "~" + fgRecieved[fgRecieved.Row, 1] + "~" + fgRecieved[fgRecieved.Row, 4] + "~" +
                               fgRecieved[fgRecieved.Row, 2] + "~" + sTemp);
            }
        }

        private void picPasteRecievedClipboard_Click(object sender, EventArgs e)
        {
            string[] tokens = Clipboard.GetText().ToString().Split('~');
            if (tokens.Length > 0)
                fgRecieved.AddItem(tokens[0] + "\t" + tokens[1] + "\t" + Path.GetFileName(tokens[4]) + "\t" + "0" + "\t" + tokens[2] + "\t" + "", 1);
        }
        private void fgRecieved_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Col == 1) fgRecieved[e.Row, 4] = fgRecieved[e.Row, 1];
        }
        private void fgRecieved_CellButtonClick(object sender, RowColEventArgs e)
        {
            if (e.Col == 2)
            {
                fgRecieved[fgRecieved.Row, 5] = Global.FileChoice(Global.DefaultFolder);
                fgRecieved[fgRecieved.Row, 2] = Path.GetFileName(fgRecieved[fgRecieved.Row, 5] + "");
            }
        }
        private void picEmptySend_Click(object sender, EventArgs e)
        {
            dSend.Value = Convert.ToDateTime("1900/01/01");
            dSend.CustomFormat = "          ";
            dSend.Format = DateTimePickerFormat.Custom;

            txtSendHour.Text = "";
            txtSendMinute.Text = "";
            txtSendSecond.Text = "";
            btnSend.Enabled = true;
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            dTemp = DateTime.Now;
            dSend.CustomFormat = "dd/MM/yyyy";
            dSend.Value = dTemp;
            txtSendHour.Text = dTemp.Hour.ToString();
            txtSendMinute.Text = dTemp.Minute.ToString();
            txtSendSecond.Text = dTemp.Second.ToString();

            dSend.Enabled = true;
            txtSendHour.Enabled = true;
            txtSendMinute.Enabled = true;
            txtSendSecond.Enabled = true;

            dSend.Focus();

            picEmptyExecute.Enabled = true;
            btnExecuted.Enabled = true;
        }
        private void dSend_ValueChanged(object sender, EventArgs e)
        {
            dSend.CustomFormat = "dd/MM/yyyy";
            if (txtSendHour.Text.Length == 0)
            {
                txtSendHour.Text = dSend.Value.Hour.ToString();
                txtSendMinute.Text = dSend.Value.Minute.ToString();
                txtSendSecond.Text = dSend.Value.Second.ToString();
            }

            if (fgRecieved.Rows.Count > 1)
                if ((dSend.Value.ToString("yyyy/MM/dd") != "1900/01/01") && dSend.Value < Convert.ToDateTime(fgRecieved[1, 0]))
                    MessageBox.Show("Wrong Date: Ημερομηνία Διαβίβασης δεν μπορεί να είναι μικρότερη απο Ημερομηνία Λήψης", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            if ((dExecute.Value.ToString("yyyy/MM/dd") != "1900/01/01") && (dSend.Value != Convert.ToDateTime("1900/01/01")) && (dSend.Value > dExecute.Value))
                MessageBox.Show("Wrong Date: Ημερομηνία Διαβίβασης δεν μπορεί να είναι μεγαλίτερη απο Ημερομηνία Εκτέλεσης", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tslCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(sMessage, Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes) {
                klsOrderFX.Status = iStatus;
                klsOrderFX.EditRecord();
                iLastAktion = 1;             // was saved (cancel)
                this.Close();
            }
        }
        private void txtAmountFromReal_LostFocus(object sender, EventArgs e)
        {
            RecalcRealRate();
        }

        private void txtRTO_DiscountPercent_LostFocus(object sender, EventArgs e)
        {
            RecalRTOFees();
            txtRTO_FeesCurrRate.Focus();
        }
        private void panCalcFees2_Click(object sender, EventArgs e)
        {
            RecalRTOFees();
        }
        private void txtRTO_FeesCurrRate_LostFocus(object sender, EventArgs e)
        {
            txtRTO_FeesCurrRate.Text = txtRTO_FeesCurrRate.Text.Replace(".", ",");
            RecalRTOFees();
        }
        private void RecalRTOFees()
        {
            if (lblRTO_Curr.Text == "EUR") txtRTO_FeesCurrRate.Text = "1";
            else
                if (!(Global.IsNumeric(txtRTO_FeesCurrRate.Text)))
                {
                    if (txtRTO_FeesCurrRate.Text == "" || txtRTO_FeesCurrRate.Text == "0")
                    {
                        clsProductsCodes ProductCode = new clsProductsCodes();
                        ProductCode.DateIns = dAktionDate.Value;
                        ProductCode.Code = "EUR" + lblRTO_Curr.Text + "=";
                        ProductCode.GetPrice_Code();
                        if (ProductCode.DateIns.Date == dAktionDate.Value.Date)
                            txtRTO_FeesCurrRate.Text = ProductCode.LastClosePrice.ToString("0.####");
                    }
                }

            lblRTO_FinishFeesPercent.Text = (Convert.ToSingle(lblRTO_FeesPercent.Text) - Convert.ToSingle(lblRTO_FeesPercent.Text) * Convert.ToSingle(txtRTO_DiscountPercent.Text) / 100).ToString("0.00");
            lblRTO_FeesAmount.Text = (Convert.ToSingle(txtAmountFromReal.Text) * Convert.ToSingle(lblRTO_FinishFeesPercent.Text) / 100).ToString("0.00");
            if (Global.IsNumeric(txtRTO_FeesCurrRate.Text)) {
                if (Convert.ToDecimal(txtRTO_FeesCurrRate.Text) != 0) lblRTO_FeesAmountEUR.Text = (Convert.ToSingle(lblRTO_FeesAmount.Text) / Convert.ToSingle(txtRTO_FeesCurrRate.Text)).ToString("0.00");
                else lblRTO_FeesAmountEUR.Text = "0";
            }
        }
        private void txtAmountToReal_LostFocus(object sender, EventArgs e)
        {
            RecalcRealRate();
        }
        private void RecalcRealRate()
        {
            if (Global.IsNumeric(txtAmountFromReal.Text)) {
                if (Global.IsNumeric(txtAmountToReal.Text)) {
                    sgTemp = Convert.ToSingle(txtAmountFromReal.Text);
                    sgTemp1 = Convert.ToSingle(txtAmountToReal.Text);
                    if (sgTemp != 0) {
                        txtRateReal.Text = (sgTemp1 / sgTemp).ToString("0.00##########");
                        CalcFees();
                    }
                }
            }
        }
        private void tsbKey_Click(object sender, EventArgs e)
        {
            pan1.Enabled = true;
            pan3.Enabled = true;
            pan4.Enabled = true;
            picEmptySend.Enabled = true;
            panSend.Enabled = true;
            tslCancel.Enabled = true;
            picEmptyExecute.Enabled = true;
            tsbSave.Visible = true;
            tsbKey.Visible = false;
            bPressedKey = true;
        }
        private void picEmptyExecute_Click(object sender, EventArgs e)
        {
            dExecute.Value = Convert.ToDateTime("1900/01/01");
            dExecute.CustomFormat = "          ";
            dExecute.Format = DateTimePickerFormat.Custom;

            txtExecuteHour.Text = "";
            txtExecuteMinute.Text = "";
            txtExecuteSecond.Text = "";
        }
        private void btnExecuted_Click(object sender, EventArgs e)
        {
            dTemp = DateTime.Now;
            dExecute.MaxDate = dTemp;
            dExecute.MinDate = dSend.Value;

            dExecute.CustomFormat = "dd/MM/yyyy";
            dExecute.Value = dTemp;

            txtExecuteHour.Text = dTemp.Hour.ToString();
            txtExecuteMinute.Text = dTemp.Minute.ToString();
            txtExecuteSecond.Text = dTemp.Second.ToString();

            dExecute.Enabled = true;
            txtExecuteHour.Enabled = true;
            txtExecuteMinute.Enabled = true;
            txtExecuteSecond.Enabled = true;
            panExecuted.Enabled = true;

            klsOrderFX.ExecuteDate = dExecute.Value;

            txtAmountFromReal.Text = txtAmountFrom.Text;
            txtAmountToReal.Text = txtAmountTo.Text;

            if (iStockCompany_ID != 9)  {                // 9 - HellasFin  (FX Fees Percent for all providers except HellasFin = 0,2)
                lblRTO_Curr.Text = cmbCurrFrom.Text;
                lblRTO_FeesPercent.Text = "0,2";
                txtRTO_DiscountPercent.Text = "0";
                RecalRTOFees();
            }
        }
        private void fgCheck_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Col == 2) fgCheck[e.Row, 9] = fgCheck[e.Row, 2];
            if (e.Col == 3) fgCheck[e.Row, 11] = fgCheck[e.Row, 3];
        }
        private void fgCheck_CellButtonClick(object sender, RowColEventArgs e)
        {
            if (e.Col == 5)
            {
                fgCheck[fgCheck.Row, 10] = Global.FileChoice(Global.DefaultFolder);
                fgCheck[fgCheck.Row, 5] = Path.GetFileName(fgCheck[fgCheck.Row, 10] + "");
            }
        }
        private void picAddCheck_Click(object sender, EventArgs e)
        {
            fgCheck.AddItem(DateTime.Now.ToString("dd/MM/yyyy") + "\t" + Global.UserName + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "0" + "\t" +
                            Global.User_ID + "\t" + "0" + "\t" + "" + "\t" + "0", 1);
        }
        private void picDeleteCheck_Click(object sender, EventArgs e)
        {
            if (fgCheck.Row > 0)
            {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) ==
                    System.Windows.Forms.DialogResult.Yes)
                {
                    clsOrdersFX_Check OrdersFX_Check = new clsOrdersFX_Check();
                    OrdersFX_Check.Record_ID = Convert.ToInt32(fgCheck[fgCheck.Row, 7]);
                    OrdersFX_Check.DeleteRecord();
                    fgCheck.RemoveItem(fgCheck.Row);
                }
            }
        }
        private void picShowCheck_Click(object sender, EventArgs e)
        {
            if (fgCheck[fgCheck.Row, 5].ToString() != "")
            {
                try
                {
                    if (fgCheck[fgCheck.Row, 10].ToString() != "")
                        System.Diagnostics.Process.Start(fgCheck[fgCheck.Row, 10].ToString());                                               // isn't DMS file, so show it into Windows mode
                    else
                        Global.DMS_ShowFile("Customers/" + klsOrderFX.ContractTitle + "/Informing", fgCheck[fgCheck.Row, 5].ToString());      //is DMS file, so show it into Web mode
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
        }
        private void fgInforming_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Col == 1) fgInforming[e.Row, 5] = fgInforming[e.Row, 1];
        }
        private void fgInforming_CellButtonClick(object sender, RowColEventArgs e)
        {
            if (e.Col == 2)
            {
                fgInforming[fgInforming.Row, 7] = Global.FileChoice(Global.DefaultFolder);
                fgInforming[fgInforming.Row, 2] = Path.GetFileName(fgInforming[fgInforming.Row, 7] + "");
            }
        }
        private void picAddInform_Click(object sender, EventArgs e)
        {
            fgInforming.AddItem(DateTime.Now.ToString("dd/MM/yyyy") + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "0" + "\t" + "0" + "\t" + Global.User_ID + "\t" + "", 1);
        }
        private void picDeleteInform_Click(object sender, EventArgs e)
        {
            if (fgInforming.Row > 0)
            {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    clsInformings Informings = new clsInformings();
                    Informings.Record_ID = Convert.ToInt32(fgInforming[fgInforming.Row, 4]);
                    Informings.DeleteRecord();
                    fgInforming.RemoveItem(fgInforming.Row);
                }
            }
        }

        private void picPlayInform_Click(object sender, EventArgs e)
        {
            if ((fgInforming[fgInforming.Row, 2] + "") != "")
            {
                if ((fgInforming[fgInforming.Row, 7] + "").Trim() != "")
                    System.Diagnostics.Process.Start(fgInforming[fgInforming.Row, 7] + "");
                else
                    if ((fgInforming[fgInforming.Row, 2] + "").Trim() != "")
                    Global.DMS_ShowFile("Customers/" + klsOrderFX.ContractTitle + "/Informing", fgInforming[fgInforming.Row, 2] + "");
            }
        }     
        private void txtNotes_LostFocus(object sender, EventArgs e)
        {
            txtNotes.Text = txtNotes.Text.Replace("\t", "");
        } 
  
        #endregion    

        public int LastAktion { get { return iLastAktion; } set { iLastAktion = value; } }
        public int Record_ID { get { return iRecord_ID; } set { iRecord_ID = value; } }
        public int Editable { get { return iEditable; } set { iEditable = value; } }
        public int Mode { get { return iMode; } set { iMode = value; } }                            // 1 - from frmDailyFX, 2 - from frmAcc_InvoicesFX
    }
}
