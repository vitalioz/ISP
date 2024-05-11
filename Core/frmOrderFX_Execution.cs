using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace Core
{
    public partial class frmOrderFX_Execution : Form
    {
        DataTable dtAccsFrom, dtAccsTo;
        DataColumn dtCol;
        DataRow dtRow;
        DataView dtView;
        int i, iLastAktion, iRecord_ID, iEditable, iII_ID, iClient_ID, iContract_ID, iStatus, iStockCompany_ID, iStockExchange_ID,
             iCashAccount_From = 0, iCashAccount_To = 0, iRealCashAccount_From = 0, iRealCashAccount_To = 0;
        string sTemp, sPortfolio, sMessage, sBulkCommand;
        float sgTemp;
        decimal decTemp, decTemp1;
        string[] sCheck = { "Δεν ελέγχθηκε", "OK", "Πρόβλημα" };
        DateTime dTemp, dRecieved;
        bool bCheckList, bCashAccounts, bPressedKey;
        SortedList lstRecieved = new SortedList();
        SortedList lstInformed = new SortedList();
        SortedList lstProblems = new SortedList();
        SortedList lstStatus = new SortedList();
        clsOrdersFX klsOrderFX = new clsOrdersFX();
        clsOrdersFX klsOrderFX2 = new clsOrdersFX();
        clsOrdersFX_Recieved OrdersFX_Recieved = new clsOrdersFX_Recieved();
        clsOrdersFX_Check OrdersFX_Check = new clsOrdersFX_Check();
        clsInformings Informings = new clsInformings();
        CellRange rng;
        public frmOrderFX_Execution()
        {
            InitializeComponent();
            this.Width = 954;
            this.Height = 656;
        } 
        private void frmOrderFX_Execution_Load(object sender, EventArgs e)
        {
            this.Text = "Εντολή (" + iRecord_ID + ")";

            bCheckList = false;
            bCashAccounts = false;
            bPressedKey = false;

            iLastAktion = 0;
            iII_ID = 0;
            dRecieved = Convert.ToDateTime("1900/01/01");
            dExecute.MaxDate = DateTime.Now;
            dExecute.CustomFormat = "          ";
            dExecute.Format = DateTimePickerFormat.Custom;
            dSend.CustomFormat = "          ";
            dSend.Format = DateTimePickerFormat.Custom;

            iCashAccount_From = 0;
            iCashAccount_To = 0;
            iRealCashAccount_From = 0;
            iRealCashAccount_To = 0;
            sPortfolio = "";

            dSend.Enabled = false;
            txtSendHour.Enabled = false;
            txtSendMinute.Enabled = false;
            txtSendSecond.Enabled = false;

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

            lstStatus.Clear();
            lstStatus.Add("0", "");
            lstStatus.Add("1", sCheck[1]);
            lstStatus.Add("2", sCheck[2]);
            fgCheck.Cols[2].DataMap = lstStatus;


            //------- fgCheck ----------------------------
            fgCheck.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgCheck.Styles.ParseString(Global.GridStyle);
            //fgCheck.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(fgCheck_CellChanged);
            //fgCheck.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(fgCheck_CellButtonClick);

            //------- fgSimpleCommands ----------------------------
            fgSimpleCommands.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgSimpleCommands.Styles.ParseString(Global.GridStyle);
            fgSimpleCommands.DrawMode = DrawModeEnum.OwnerDraw;
            fgSimpleCommands.ShowCellLabels = true;

            fgSimpleCommands.Styles.Normal.WordWrap = true;
            fgSimpleCommands.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgSimpleCommands.Rows[0].AllowMerging = true;
            fgSimpleCommands.Cols[0].AllowMerging = true;
            rng = fgSimpleCommands.GetCellRange(0, 0, 1, 0);
            rng.Data = "ΑΑ";

            fgSimpleCommands.Cols[1].AllowMerging = true;
            rng = fgSimpleCommands.GetCellRange(0, 1, 1, 1);
            rng.Data = "Εντολέας";

            fgSimpleCommands.Cols[2].AllowMerging = true;
            rng = fgSimpleCommands.GetCellRange(0, 2, 1, 2);
            rng.Data = "Κωδικός";

            fgSimpleCommands.Cols[3].AllowMerging = true;
            rng = fgSimpleCommands.GetCellRange(0, 3, 1, 3);
            rng.Data = "Portfolio";

            rng = fgSimpleCommands.GetCellRange(0, 4, 0, 6);
            rng.Data = Global.GetLabel("execute_debit");

            fgSimpleCommands[1, 4] = Global.GetLabel("cash_account");
            fgSimpleCommands[1, 5] = Global.GetLabel("amount");
            fgSimpleCommands[1, 6] = Global.GetLabel("currency");

            rng = fgSimpleCommands.GetCellRange(0, 7, 0, 9);
            rng.Data = Global.GetLabel("execute_credit");

            fgSimpleCommands[1, 7] = Global.GetLabel("cash_account");
            fgSimpleCommands[1, 8] = Global.GetLabel("amount");
            fgSimpleCommands[1, 9] = Global.GetLabel("currency");

            //---- Start Initialisation - Show Command --------------
            klsOrderFX.Record_ID = iRecord_ID;
            klsOrderFX.GetRecord();

            sBulkCommand = klsOrderFX.BulkCommand;
            if (klsOrderFX.CommandType_ID == 1) {
                iClient_ID = klsOrderFX.Client_ID;
                iContract_ID = klsOrderFX.Contract_ID;
                lblPelatis.Text = klsOrderFX.ClientName;
                lblContractTitle.Text = klsOrderFX.ContractTitle;
            }
            else {
                iClient_ID = 0;
                iContract_ID = klsOrderFX.Contract_ID;
                lblPelatis.Text = Global.CompanyName;
                lblContractTitle.Text = klsOrderFX.ContractTitle;
            }
            lblCode.Text = klsOrderFX.Code;
            dAktionDate.Value = klsOrderFX.AktionDate;
            iStockCompany_ID = klsOrderFX.StockCompany_ID;
            lblStockCompany.Text = klsOrderFX.StockCompany_Title;

            iStockExchange_ID = klsOrderFX.StockExchange_ID;
            sPortfolio = klsOrderFX.Portfolio;
            lblPortfolio.Text = sPortfolio;            

            iCashAccount_From = klsOrderFX.CashAccountFrom_ID;
            iCashAccount_To = klsOrderFX.CashAccountTo_ID;
            iRealCashAccount_From = klsOrderFX.RealCashAccountFrom_ID;
            iRealCashAccount_To = klsOrderFX.RealCashAccountTo_ID;
            cmbCurrMain.Text = klsOrderFX.MainCurr;

            txtOrder_ID.Text = klsOrderFX.Order_ID;

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

            dTemp = klsOrderFX.SentDate;
            if (klsOrderFX.SentDate != Convert.ToDateTime("1900/01/01")) {
                dSend.CustomFormat = "dd/MM/yyyy";
                dSend.Value = dTemp.Date;
                txtSendHour.Text = dTemp.Hour.ToString("00");
                txtSendMinute.Text = dTemp.Minute.ToString("00");
                txtSendSecond.Text = dTemp.Second.ToString("00");

                dSend.Enabled = true;
                txtSendHour.Enabled = true;
                txtSendMinute.Enabled = true;
                txtSendSecond.Enabled = true;

                btnSend.Enabled = false;
            }
            else  {
                dSend.CustomFormat = "          ";
                dSend.Format = DateTimePickerFormat.Custom;
                dSend.Value = dTemp.Date;
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
            if (klsOrderFX.ExecuteDate != Convert.ToDateTime("1900/01/01"))
            {
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
            else
            {
                dExecute.CustomFormat = "          ";
                dExecute.Format = DateTimePickerFormat.Custom;
                dExecute.Value = dTemp.Date;
                dExecute.Enabled = false;
                txtExecuteHour.Enabled = false;
                txtExecuteMinute.Enabled = false;
                txtExecuteSecond.Enabled = false;
            }

            txtNotes.Text = klsOrderFX.Notes;

            if (klsOrderFX.Status >= 0)
            {
                tslCancel.Text = "Ακύρωση εντολής";
                sMessage = "ΠΡΟΣΟΧΗ! Ζητήσατε να ακυρωθεί η εντολή. \n Είστε σίγουρος για την ακύρωση της;";
                iStatus = -1;
            }
            else
            {
                tslCancel.Text = "Επαναφορά εντολής";
                sMessage = "ΠΡΟΣΟΧΗ! Ζητήσατε να επαναφερθεί η εντολή.\n Είστε σίγουρος για την επαναφορά της;";
                iStatus = 0;
            }

            if (iEditable == 0) {               // || (klsOrderFX.SentDate != Convert.ToDateTime("1900/01/01")))  {
                pan1.Enabled = false;
                tslCancel.Enabled = false;
            }
            else  {
                pan1.Enabled = true;
                tslCancel.Enabled = true;
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
            ClientCashAccounts.Contract_ID = iContract_ID;
            ClientCashAccounts.GetList();
            foreach (DataRow dtRow1 in ClientCashAccounts.List.Rows)
            {
                dtRow = dtAccsTo.NewRow();
                dtRow["ID"] = dtRow1["ID"];
                dtRow["AccountNumber"] = dtRow1["AccountNumber"] + " / " + dtRow1["Currency"];
                dtRow["Currency"] = dtRow1["Currency"];
                dtAccsTo.Rows.Add(dtRow);

                if (Convert.ToInt32(dtRow1["Contract_ID"]) == iContract_ID)
                {
                    dtRow = dtAccsFrom.NewRow();
                    dtRow["ID"] = dtRow1["ID"];
                    dtRow["AccountNumber"] = dtRow1["AccountNumber"] + " / " + dtRow1["Currency"];
                    dtRow["Currency"] = dtRow1["Currency"];
                    dtAccsFrom.Rows.Add(dtRow);
                }
            }

            cmbCashAccFrom.DataSource = dtAccsFrom.Copy();
            cmbCashAccFrom.DisplayMember = "AccountNumber";
            cmbCashAccFrom.ValueMember = "ID";

            cmbCashAccTo.DataSource = dtAccsTo.Copy();
            cmbCashAccTo.DisplayMember = "AccountNumber";
            cmbCashAccTo.ValueMember = "ID";

            cmbCashAccFrom.SelectedValue = iCashAccount_From;
            cmbCashAccTo.SelectedValue = iCashAccount_To;

            cmbCashAccFromReal.DataSource = dtAccsFrom.Copy();
            cmbCashAccFromReal.DisplayMember = "AccountNumber";
            cmbCashAccFromReal.ValueMember = "ID";            

            cmbCashAccToReal.DataSource = dtAccsTo.Copy();
            cmbCashAccToReal.DisplayMember = "AccountNumber";
            cmbCashAccToReal.ValueMember = "ID";           

            if (klsOrderFX.RealAmountFrom != 0 || klsOrderFX.RealAmountTo != 0 || klsOrderFX.RealCurrRate != 0) {
                if (Convert.ToDateTime(klsOrderFX.ExecuteDate) != Convert.ToDateTime("01/01/1900")) {
                    txtAmountFromReal.Text = klsOrderFX.RealAmountFrom.ToString("0.#######");
                    txtAmountToReal.Text = klsOrderFX.RealAmountTo.ToString("0.#######");
                    txtFees1.Text = (Convert.ToDecimal(txtAmountFromReal.Text) * Convert.ToDecimal(txtFeesPercent.Text) / 100).ToString("0.00");
                    txtFees2.Text = (Convert.ToDecimal(txtAmountToReal.Text) * Convert.ToDecimal(txtFeesPercent.Text) / 100).ToString("0.00");
                    btnExecuted.Enabled = false;
                    picEmptyExecute.Enabled = true;

                    cmbCurrFromReal.Text = klsOrderFX.CurrFrom;
                    cmbCurrToReal.Text = klsOrderFX.CurrTo;

                    cmbCashAccFromReal.SelectedValue = klsOrderFX.RealCashAccountFrom_ID;
                    cmbCashAccToReal.SelectedValue = klsOrderFX.RealCashAccountTo_ID;
                }
                else {
                    btnExecuted.Enabled = true;
                    picEmptyExecute.Enabled = false;
                }
            }

            RecalcRealRate();
            CalcFees();

            DefineSimpleCommandsList();

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

            bCheckList = true;
            bCashAccounts = true;
        }
        private void tslCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(sMessage, Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                clsOrdersFX klsOrderFX = new clsOrdersFX();
                klsOrderFX.Record_ID = iRecord_ID;
                klsOrderFX.GetRecord();
                klsOrderFX.SentDate = Convert.ToDateTime("1900/01/01");
                klsOrderFX.ExecuteDate = Convert.ToDateTime("1900/01/01");
                klsOrderFX.RealAmountFrom = 0;
                klsOrderFX.RealCashAccountFrom_ID = 0;
                klsOrderFX.RealAmountTo = 0;
                klsOrderFX.RealCashAccountTo_ID = 0;
                klsOrderFX.RealCurrRate = 0;
                klsOrderFX.FeesRate = 0;
                klsOrderFX.FeesPercent = 0;
                klsOrderFX.FeesAmount = 0;                
                klsOrderFX.Status = iStatus;
                sBulkCommand = klsOrderFX.BulkCommand.Replace("<", "").Replace(">", "");
                klsOrderFX.EditRecord();

                if (iStatus < 0 && sBulkCommand.Trim().Length > 0)
                {                              // only for Order Cancelation, not for Order Recovery (επαναφορά) 
                    clsOrdersFX klsOrderFX2 = new clsOrdersFX();
                    klsOrderFX2.BulkCommand = sBulkCommand;
                    klsOrderFX2.AktionDate = dAktionDate.Value;
                    klsOrderFX2.EditBulkCommand();
                }

                for (i = 2; i <= fgSimpleCommands.Rows.Count - 1; i++)
                {
                    if (Convert.ToInt32(fgSimpleCommands[i, "ID"]) != 0)
                    {
                        clsOrdersFX klsOrderFX2 = new clsOrdersFX();
                        klsOrderFX2.Record_ID = Convert.ToInt32(fgSimpleCommands[i, "ID"]);
                        klsOrderFX2.GetRecord();
                        klsOrderFX2.BulkCommand = "";
                        klsOrderFX2.SentDate = Convert.ToDateTime("1900/01/01");
                        klsOrderFX2.ExecuteDate = Convert.ToDateTime("1900/01/01");
                        klsOrderFX2.EditRecord();
                    }
                }
                iLastAktion = 1;                                                  // was saved (cancel)
                this.Close();
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
        private void cmbCurrFrom_SelectedValueChanged(object sender, EventArgs e)
        {
            cmbCurrFromReal.SelectedValue = cmbCurrFrom.SelectedValue;
            cmbCurrFees1.SelectedValue = cmbCurrFrom.SelectedValue;

            if (bCheckList)
            {
                dtView = dtAccsFrom.DefaultView;
                dtView.RowFilter = "Currency = '' OR Currency = '" + cmbCurrFrom.Text + "'";
                cmbCashAccFrom.DataSource = dtView;
                cmbCashAccFrom.DisplayMember = "AccountNumber";
                cmbCashAccFrom.ValueMember = "ID";
            }
        }
        private void cmbCurrTo_SelectedValueChanged(object sender, EventArgs e)
        {
            cmbCurrToReal.SelectedValue = cmbCurrTo.SelectedValue;
            cmbCurrFees2.SelectedValue = cmbCurrTo.SelectedValue;

            if (bCheckList)
            {
                dtView = dtAccsTo.DefaultView;
                dtView.RowFilter = "Currency = '' OR Currency = '" + cmbCurrTo.Text + "'";
                cmbCashAccTo.DataSource = dtView;
                cmbCashAccTo.DisplayMember = "AccountNumber";
                cmbCashAccTo.ValueMember = "ID";
            }
        }
        private void cmbConstant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbConstant.SelectedIndex) == 2)
            {
                dConstant.Value = DateTime.Now;
                dConstant.Visible = true;
            }
            else dConstant.Visible = false;
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
        private void dValueDate_ValueChanged(object sender, EventArgs e)
        {
            dValueDate.CustomFormat = "dd/MM/yyyy";
        }
        private void btnExecuted_Click(object sender, EventArgs e)
        {
            dTemp = DateTime.Now;
            dExecute.CustomFormat = "dd/MM/yyyy";
            dExecute.Value = dTemp.Date;
            txtExecuteHour.Text = dTemp.Hour.ToString("00");
            txtExecuteMinute.Text = dTemp.Minute.ToString("00");
            txtExecuteSecond.Text = dTemp.Second.ToString("00");
            dExecute.Enabled = true;
            txtExecuteHour.Enabled = true;
            txtExecuteMinute.Enabled = true;
            txtExecuteSecond.Enabled = true;

            txtAmountFromReal.Text = txtAmountFrom.Text;
            txtAmountToReal.Text = txtAmountTo.Text;

            cmbCurrFromReal.SelectedValue = cmbCurrFrom.SelectedValue;
            cmbCurrToReal.SelectedValue = cmbCurrTo.SelectedValue;

            cmbCashAccFromReal.SelectedValue = cmbCashAccFrom.SelectedValue;
            cmbCashAccToReal.SelectedValue = cmbCashAccTo.SelectedValue;

            RecalcRealRate();
            CalcFees();
        }
        private void picEmptyExecute_Click(object sender, EventArgs e)
        {
            dExecute.Value = Convert.ToDateTime("1900/01/01");
            dExecute.CustomFormat = "          ";
            dExecute.Format = DateTimePickerFormat.Custom;

            txtExecuteHour.Text = "";
            txtExecuteMinute.Text = "";
            txtExecuteSecond.Text = "";
            txtOrder_ID.Text = "";

            lblRateReal.Text = "0";
            lblFeesRate.Text = "0";
            txtAmountFromReal.Text = "0";
            txtAmountToReal.Text = "0";
            txtRateReal.Text = "0";
            txtFeesPercent.Text = "0";
            txtFees1.Text = "0";
            txtFees2.Text = "0";
            txtFeesAmount.Text = "0";
            txtFeesRate.Text = "0";

            cmbCurrFromReal.SelectedValue = 0;
            cmbCurrToReal.SelectedValue = 0;

            cmbCashAccFromReal.SelectedValue = 0;
            cmbCashAccToReal.SelectedValue = 0;

            cmbCurrFees1.SelectedValue = 0;
            cmbCurrFees2.SelectedValue = 0;

            btnExecuted.Enabled = true;
        }
        private void txtAmountFromReal_LostFocus(object sender, EventArgs e)
        {
            RecalcRealRate();
        }

        private void txtAmountToReal_LostFocus(object sender, EventArgs e)
        {
            RecalcRealRate();
        }

        private void txtFeesPercent_LostFocus(object sender, EventArgs e)
        {
            CalcFees();
        }
        private void RecalcRealRate()
        {
            if (Global.IsNumeric(txtAmountFromReal.Text))
                if (Global.IsNumeric(txtAmountToReal.Text)) {
                    decTemp = Convert.ToDecimal(txtAmountFromReal.Text);
                    decTemp1 = Convert.ToDecimal(txtAmountToReal.Text);
                    if (decTemp != 0)
                        txtRateReal.Text = Convert.ToDecimal(decTemp1 / decTemp).ToString("0.00##########");
                }
        }
        private void CalcFees()
        {
            sgTemp = 0;
            if (cmbCurrFees1.Text != cmbCurrMain.Text) {
                clsSystem System = new clsSystem();
                System.AktionDate = dAktionDate.Value;
                System.CurrFrom = cmbCurrFees1.Text;
                System.CurrTo = cmbCurrMain.Text;
                System.GetConvertAmount();
                sgTemp = System.CurrencyRate;
            }
            else sgTemp = 1;

            txtFees1.Text = (Convert.ToDecimal(txtAmountFromReal.Text) * Convert.ToDecimal(txtFeesPercent.Text) / 100).ToString("0.00") ;
            txtFees2.Text = (Convert.ToDecimal(txtAmountToReal.Text) * Convert.ToDecimal(txtFeesPercent.Text) / 100).ToString("0.00");

            sgTemp = 1 / sgTemp;
            lblFeesRate.Text = cmbCurrMain.Text + "/" + cmbCurrFees1.Text;
            txtFeesRate.Text = sgTemp.ToString("0.00##########");
            txtFeesAmount.Text = (Convert.ToDecimal(txtFees1.Text) * Convert.ToDecimal(sgTemp)).ToString("0.00");
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
        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveRecord();
            this.Close();
            iLastAktion = 1;            // 1 - was saved (added)
        }

        private void SaveRecord()
        {
            //--- define CompanyFeesPercent --------------------

            sgTemp = 0;
            if (Global.IsNumeric(txtAmountFromReal.Text)) 
                sgTemp = Convert.ToSingle(txtAmountFromReal.Text);

            if (sgTemp == 0)
                if (Global.IsNumeric(txtAmountToReal.Text))
                    sgTemp = Convert.ToSingle(txtAmountToReal.Text);

            clsContracts Contracts = new clsContracts();
            Contracts.Record_ID = iContract_ID;
            Contracts.AktionDate = dAktionDate.Value;
            Contracts.Amount = sgTemp;
            Contracts.GetRecordFX_Fees();

            //--- save fgCheck records -------------------------
            for (i = 1; i <= fgCheck.Rows.Count - 1; i++)
            {

                if ((fgCheck[i, 10] + "").Trim() != "")
                {                                     // FileFullName - Not Empty means that it's a new file
                    sTemp = Global.DMS_UploadFile(fgCheck[i, 10] + "", "Customers/" + klsOrderFX.ContractTitle + "/Informing", fgCheck[i, 5] + "");
                    fgCheck[i, 5] = Path.GetFileName(sTemp);
                }

                if (Convert.ToInt32(fgCheck[i, "ID"]) == 0)
                {
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
                else
                {
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

            //--- Edit CommandFX ----------------------------------
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
            klsOrderFX.RecieveDate = dTemp;
            klsOrderFX.RecieveMethod_ID = i;
            if (dSend.Value.Date != Convert.ToDateTime("1900/01/01").Date)
            {
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

            klsOrderFX.Order_ID = txtOrder_ID.Text;
            klsOrderFX.RealAmountFrom = Convert.ToDecimal(txtAmountFromReal.Text);
            klsOrderFX.RealCashAccountFrom_ID = Convert.ToInt32(cmbCashAccFromReal.SelectedValue);
            klsOrderFX.RealAmountTo = Convert.ToDecimal(txtAmountToReal.Text);
            klsOrderFX.RealCashAccountTo_ID = Convert.ToInt32(cmbCashAccToReal.SelectedValue);
            klsOrderFX.RealCurrRate = Convert.ToDouble(txtRateReal.Text);
            klsOrderFX.FeesRate = Convert.ToDouble(0);
            klsOrderFX.FeesPercent = Convert.ToDouble(txtFeesPercent.Text);
            klsOrderFX.FeesAmount = Convert.ToDouble(0);

            i = 0;
            klsOrderFX.InformationMethod_ID = i;
            klsOrderFX.Notes = txtNotes.Text;
            klsOrderFX.EditRecord();

            /*
            With comm
                .Connection = cn
                .CommandText = "sp_GetTransactionFX_SimpleCommands"
                .CommandType = CommandType.StoredProcedure
                .Parameters.Clear()
            End With
            prmSQL = comm.Parameters.AddWithValue("@AktionDate", dAktionDate.Value)
            prmSQL = comm.Parameters.AddWithValue("@BulkCommand", OrderFX.BulkCommand)
            drList = comm.ExecuteReader()
            While drList.Read
                SimpleOrderFX = New clsOrderFX
                SimpleOrderFX.Record_ID = vbTab & drList("ID")
                SimpleOrderFX.GetRecord()
                SimpleOrderFX.ExecuteDate = OrderFX.ExecuteDate
                SimpleOrderFX.SentDate = OrderFX.SentDate
                SimpleOrderFX.ValueDate = OrderFX.ValueDate
                SimpleOrderFX.EditRecord()
            End While
            drList.Close()
            */

            this.Close();
            iLastAktion = 1;             // was saved (added)
        }
        private void lnkSubmit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsOrdersFX SimpleOrderFX = new clsOrdersFX();

            sgTemp = 1;
            sTemp = dTemp.ToString("yyyy-MM-dd") + " " + (txtExecuteHour.Text.Trim() == "" ? "00" : txtExecuteHour.Text.Trim()) + ":" +
                         (txtExecuteMinute.Text.Trim() == "" ? "00" : txtExecuteMinute.Text.Trim()) + ":" +
                         (txtExecuteSecond.Text.Trim() == "" ? "00" : txtExecuteSecond.Text.Trim());        // sgTemp - koef. katamerismou  

            if (fgSimpleCommands.Rows.Count > 2) {
                if (fgSimpleCommands.Rows.Count == 3) {                                                    // 1 order Ληψη -> 1 order Execution

                    SimpleOrderFX.Record_ID = Convert.ToInt32(fgSimpleCommands[2, 10]);
                    SimpleOrderFX.GetRecord();

                    SimpleOrderFX.ValueDate = dValueDate.Value.ToString("yyyy/MM/dd");
                    SimpleOrderFX.ExecuteDate = Convert.ToDateTime(sTemp);
                    SimpleOrderFX.RealCashAccountFrom_ID = SimpleOrderFX.CashAccountFrom_ID;
                    SimpleOrderFX.RealAmountFrom = Convert.ToDecimal(txtAmountFromReal.Text);
                    SimpleOrderFX.RealCashAccountTo_ID = SimpleOrderFX.CashAccountTo_ID;
                    SimpleOrderFX.RealAmountTo = Convert.ToDecimal(txtAmountToReal.Text);
                    SimpleOrderFX.RealCurrRate = Convert.ToDouble(txtRateReal.Text);

                    SimpleOrderFX.AktionDate = dExecute.Value;
                    SimpleOrderFX.Contract_ID = SimpleOrderFX.Contract_ID;
                    SimpleOrderFX.FeesPercent = SimpleOrderFX.GetFees();  

                    //sgTemp = DefineFeesPercent(SimpleOrderFX.Contract_ID);
                    //SimpleOrderFX.FeesPercent = sgTemp;

                    if (SimpleOrderFX.AmountFrom == "" || SimpleOrderFX.AmountFrom == "0")
                        SimpleOrderFX.FeesAmount = Convert.ToDouble(SimpleOrderFX.RealAmountFrom) * SimpleOrderFX.FeesPercent / 100;
                    else
                        if (SimpleOrderFX.AmountTo == "" || SimpleOrderFX.AmountTo == "0")
                            SimpleOrderFX.FeesAmount = Convert.ToDouble(SimpleOrderFX.RealAmountTo) * SimpleOrderFX.FeesPercent / 100;

                    SimpleOrderFX.EditRecord();
                }
                else {
                    for (i = 2; i <= fgSimpleCommands.Rows.Count - 1; i++) {
                        SimpleOrderFX.Record_ID = Convert.ToInt32(fgSimpleCommands[i, 10]);
                        SimpleOrderFX.GetRecord();
                        SimpleOrderFX.ValueDate = dValueDate.Value.ToString("yyyy/MM/dd");
                        SimpleOrderFX.ExecuteDate = Convert.ToDateTime(sTemp);
                        SimpleOrderFX.RealCashAccountFrom_ID = SimpleOrderFX.CashAccountFrom_ID;
                        SimpleOrderFX.RealCashAccountTo_ID = SimpleOrderFX.CashAccountTo_ID;
                        if (Convert.ToDecimal(txtAmountFrom.Text) != 0) {
                            SimpleOrderFX.RealAmountFrom = Convert.ToDecimal(SimpleOrderFX.AmountFrom);
                            SimpleOrderFX.RealAmountTo = Convert.ToDecimal(SimpleOrderFX.AmountFrom) * Convert.ToDecimal(txtRateReal.Text);    // 1st CUR -> 2nd CUR  *
                        }
                        else {
                            SimpleOrderFX.RealAmountFrom = Convert.ToDecimal(SimpleOrderFX.AmountTo) / Convert.ToDecimal(txtRateReal.Text);    // 2nd CUR -> 1st CUR  /   
                            SimpleOrderFX.RealAmountTo = Convert.ToDecimal(SimpleOrderFX.AmountTo);
                        }
                        SimpleOrderFX.RealCurrRate = Convert.ToDouble(txtRateReal.Text);


                        SimpleOrderFX.AktionDate = dExecute.Value;
                        SimpleOrderFX.Contract_ID = SimpleOrderFX.Contract_ID;
                        SimpleOrderFX.FeesPercent = SimpleOrderFX.GetFees();

                        //sgTemp = DefineFeesPercent(SimpleOrderFX.Contract_ID);
                        //SimpleOrderFX.FeesPercent = Convert.ToDouble(sgTemp);

                        if (SimpleOrderFX.AmountFrom == "" || SimpleOrderFX.AmountFrom == "0")
                            SimpleOrderFX.FeesAmount = Convert.ToDouble(SimpleOrderFX.RealAmountFrom) * Convert.ToDouble(SimpleOrderFX.FeesPercent) / 100;
             
                        if (SimpleOrderFX.AmountTo == "" || SimpleOrderFX.AmountTo == "0")
                            SimpleOrderFX.FeesAmount = Convert.ToDouble(SimpleOrderFX.RealAmountTo) * Convert.ToDouble(SimpleOrderFX.FeesPercent) / 100;

                        SimpleOrderFX.EditRecord();
                    }
                }
            }

            DefineSimpleCommandsList();
        }
        private float DefineFeesPercent(int iContract_ID)
        {
            float sgTemp = 0;
            /*
        cn.Open()
        With comm
            .Connection = cn
            .CommandText = "GetClientsPackages_FXData"
            .CommandType = CommandType.StoredProcedure
            .Parameters.Clear()
        End With
        prmSQL = comm.Parameters.AddWithValue("@Contract_ID", iContract_ID)
        prmSQL = comm.Parameters.AddWithValue("@AktionDate", dAktionDate.Value)
        drList = comm.ExecuteReader()
        While drList.Read
            sgTemp = drList("FXFees");
        End While
        drList.Close();
        cn.Close();
            */
        return sgTemp;

        }
        private void DefineSimpleCommandsList()
        {
            decimal sgTemp = 0, sgTemp2 = 0;

            i = 0;
            fgSimpleCommands.Redraw = false;
            fgSimpleCommands.Rows.Count = 2;

            clsOrdersFX OrdersFX3 = new clsOrdersFX();
            OrdersFX3.AktionDate = dAktionDate.Value.Date;
            OrdersFX3.BulkCommand = sBulkCommand;
            OrdersFX3.GetList_SingleOrders();
            foreach (DataRow dtRow in OrdersFX3.List.Rows) {
                if (Convert.ToInt32(dtRow["CommandType_ID"]) == 1) {
                    i = i + 1;
                    fgSimpleCommands.AddItem(i + "\t" + dtRow["ClientName"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" +
                                     dtRow["CashAccount_From"] + "\t" + dtRow["RealAmountFrom"] + "\t" + dtRow["CurrFrom"] + "\t" +
                                     dtRow["CashAccount_To"] + "\t" + dtRow["RealAmountTo"] + "\t" + dtRow["CurrTo"] + "\t" + dtRow["ID"]);

                    sgTemp = sgTemp + Convert.ToDecimal(dtRow["RealAmountFrom"]);
                    sgTemp2 = sgTemp2 + Convert.ToDecimal(dtRow["RealAmountTo"]);
                }
            }

            lblSumDebit.Text = sgTemp.ToString("0.00");
            lblSumCredit.Text = sgTemp2.ToString("0.00");

            fgSimpleCommands.Redraw = true;
        }
        public int Record_ID { get { return this.iRecord_ID; } set { this.iRecord_ID = value; } }
        public int Editable { get { return this.iEditable; } set { this.iEditable = value; } }
    }
}
