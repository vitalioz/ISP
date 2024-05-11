using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
namespace Core

{
    public partial class frmOrderLL : Form
    {
        DataTable dtAccs;
        DataColumn dtCol;
        DataRow dtRow1;
        int i, iLastAktion, iEditable, iContract_ID, iStatus, iRightsLevel, iClient_ID, iStockCompany_ID, iCashAccount, iRecord_ID, iMode, iTipos;
        float sgRate = 0, sgRate1 = 0;
        string[] sCheck = { "Δεν ελέγχθηκε", "OK", "Πρόβλημα" };
        string sTemp, sClientFullName, sProviderMainCurr, sMessage;
        bool bCheckList;
        DateTime dTemp, dRecieved;
        SortedList lstRecieved = new SortedList();
        SortedList lstInformed = new SortedList();
        SortedList lstProblems = new SortedList();
        SortedList lstStatus = new SortedList();
        clsOrdersLL klsOrderLL = new clsOrdersLL();
        clsOrdersLL klsOrderLL2 = new clsOrdersLL();
        clsOrdersLL_Recieved OrdersLL_Recieved = new clsOrdersLL_Recieved();      
        clsOrdersLL_Check OrdersLL_Check = new clsOrdersLL_Check();
        clsProductsCodes klsProductsCodes = new clsProductsCodes();
        public frmOrderLL()
        {
            InitializeComponent();

            bCheckList = false;
            iCashAccount = 0;

            dRecieved = Convert.ToDateTime("1900/01/01");
        }

        private void frmOrderLL_Load(object sender, EventArgs e)
        {
            this.Text = "Εντολή (" + iRecord_ID + ")";

            dSend.Value = Convert.ToDateTime("01/01/1900");
            dSend.CustomFormat = "          ";
            dSend.Format = DateTimePickerFormat.Custom;

            txtSendHour.Text = "";
            txtSendMinute.Text = "";
            txtSendSecond.Text = "";

            dExecute.MaxDate = DateTime.Now;
            dExecute.CustomFormat = "          ";
            dExecute.Format = DateTimePickerFormat.Custom;

            txtExecuteHour.Text = "";
            txtExecuteMinute.Text = "";
            txtExecuteSecond.Text = "";

            //-------------- Define Currencies List ------------------
            cmbAmountCurr.DataSource = Global.dtCurrencies.Copy();
            cmbAmountCurr.DisplayMember = "Title";
            cmbAmountCurr.ValueMember = "ID";

            //------- fgRecieved ----------------------------
            fgRecieved.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgRecieved.Styles.ParseString(Global.GridStyle);
            fgRecieved.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(fgRecieved_CellChanged);
            fgRecieved.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(fgRecieved_CellButtonClick);

            Column col2 = fgRecieved.Cols[2];
            col2.Name = "Image";
            col2.DataType = typeof(String);
            col2.ComboList = "...";

            //------- fgCheck ----------------------------
            fgCheck.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgCheck.Styles.ParseString(Global.GridStyle);
            fgCheck.DrawMode = DrawModeEnum.OwnerDraw;
            fgCheck.ShowCellLabels = true;
            fgCheck.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(fgCheck_CellButtonClick);
            fgCheck.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(fgCheck_CellChanged);
            fgCheck.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgCheck_BeforeEdit);

            Column col5 = fgCheck.Cols[5];
            col5.Name = "Image";
            col5.DataType = typeof(String);
            col5.ComboList = "...";

            //-------------- Define Recieve Methods List ------------------
            lstRecieved.Clear();
            foreach (DataRow dtRow in Global.dtRecieveMethods.Rows) lstRecieved.Add(dtRow["ID"], dtRow["Title"]);

            fgRecieved.Cols[1].DataMap = lstRecieved;

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

            if (iRecord_ID != 0) {                                              // iRecord_ID - order exists - so it's Edit Mode
                klsOrderLL = new clsOrdersLL();
                klsOrderLL.Record_ID = iRecord_ID;
                klsOrderLL.GetRecord();

                iTipos = klsOrderLL.ClientTipos;
                iClient_ID = klsOrderLL.Client_ID;
                iContract_ID = klsOrderLL.Contract_ID;
                sClientFullName = klsOrderLL.ClientFullName;
                lblClientFullName.Text = sClientFullName;
                lblContractTitle.Text = klsOrderLL.ContractTitle;
                dAktionDate.Value = klsOrderLL.AktionDate;
                lblCode.Text = klsOrderLL.Code;
                lblPortfolio.Text = klsOrderLL.Portfolio;
                iStockCompany_ID = klsOrderLL.StockCompany_ID;
                lblStockCompany.Text = klsOrderLL.StockCompany_Title;
                iCashAccount = klsOrderLL.CashAccount_ID;
                txtAmount.Text = klsOrderLL.Amount.ToString();
                cmbAmountCurr.Text = klsOrderLL.Curr;
                txtLTV.Text = klsOrderLL.LTV.ToString();
                lblLL_AS.Text = klsOrderLL.LL_AS.ToString();
                txtProviderRate.Text = klsOrderLL.ProviderRate.ToString();
                txtAdditionalRate.Text = klsOrderLL.AdditionalRate.ToString();
                txtDiscount.Text = klsOrderLL.Discount.ToString();
                txtFinalMargin.Text = klsOrderLL.FinalMargin.ToString();
                txtGrossRate.Text = klsOrderLL.GrossRate.ToString();
                dPeriodStart.Value = klsOrderLL.PeriodStart;
                dPeriodEnd.Value = klsOrderLL.PeriodEnd;
                lblDays.Text = klsOrderLL.Days.ToString();
                lblCurrRate.Text = klsOrderLL.CurrRate.ToString();
                txtBasicFees.Text = klsOrderLL.BasicFees.ToString("0.##");
                sProviderMainCurr = klsOrderLL.MainCurr;
                 txtNotes.Text = klsOrderLL.Notes;

                if (Convert.ToDateTime(klsOrderLL.SentDate) != Convert.ToDateTime("1900/01/01"))
                {
                    dTemp = Convert.ToDateTime(klsOrderLL.SentDate);
                    dSend.CustomFormat = "dd/MM/yyyy";
                    dSend.Text = dTemp.ToString();
                    txtSendHour.Text = dTemp.Hour.ToString();
                    txtSendMinute.Text = dTemp.Minute.ToString();
                    txtSendSecond.Text = dTemp.Second.ToString();
                }
                else
                {
                    dSend.CustomFormat = "          ";
                    dSend.Format = DateTimePickerFormat.Custom;
                    txtSendHour.Text = "";
                    txtSendMinute.Text = "";
                    txtSendSecond.Text = "";
                }

                if (klsOrderLL.ExecuteDate != Convert.ToDateTime("01/01/1900"))
                {
                    dTemp = klsOrderLL.ExecuteDate;
                    dExecute.Format = DateTimePickerFormat.Short;
                    dExecute.Value = dTemp;
                    txtExecuteHour.Text = dTemp.Hour.ToString();
                    txtExecuteMinute.Text = dTemp.Minute.ToString();
                    txtExecuteSecond.Text = dTemp.Second.ToString();
                }
                else
                {
                    dExecute.CustomFormat = "          ";
                    dExecute.Format = DateTimePickerFormat.Custom;
                    txtExecuteHour.Text = "";
                    txtExecuteMinute.Text = "";
                    txtExecuteSecond.Text = "";
                }
            }

            if (klsOrderLL.Status >= 0)
            {
                tslCancel.Text = "Ακύρωση εντολής";
                sMessage = "ΠΡΟΣΟΧΗ! Ζητήσατε να ακυρωθεί η εντολή." + "\n" + "Είστε σίγουρος για την ακύρωση της;";
                iStatus = -1;
            }
            else
            {
                tslCancel.Text = "Επαναφορά εντολής";
                sMessage = "ΠΡΟΣΟΧΗ! Ζητήσατε να επαναφερθεί η εντολή." + "\n" + "Είστε σίγουρος για την επαναφορά της;";
                iStatus = 0;
            }

            if (iEditable == 0)
            {
                tslCancel.Enabled = false;
                tsbSave.Enabled = false;
            }
            else
            {
                tslCancel.Enabled = true;
                tsbSave.Enabled = true;
            }


            //--- Define Daile Currency Rate ----------------------------
            if (klsOrderLL.CurrRate == 0) {
                if (sProviderMainCurr != cmbAmountCurr.Text) {
                    if (sProviderMainCurr == "EUR") {

                        klsProductsCodes = new clsProductsCodes();
                        klsProductsCodes.DateIns = dAktionDate.Value;
                        klsProductsCodes.Code = "EUR" + cmbAmountCurr.Text + "=";
                        klsProductsCodes.GetPrice_Code();
                        sgRate = klsProductsCodes.LastClosePrice;
                    }
                    else {
                        klsProductsCodes = new clsProductsCodes();
                        klsProductsCodes.DateIns = dAktionDate.Value;
                        klsProductsCodes.Code = "EUR" + sProviderMainCurr + "=";
                        klsProductsCodes.GetPrice_Code();
                        sgRate = klsProductsCodes.LastClosePrice;

                        sgRate1 = 1;
                        klsProductsCodes = new clsProductsCodes();
                        klsProductsCodes.DateIns = dAktionDate.Value;
                        klsProductsCodes.Code = "EUR" + cmbAmountCurr.Text + "=";
                        klsProductsCodes.GetPrice_Code();
                        sgRate1 = klsProductsCodes.LastClosePrice;

                        if (sgRate != 0) sgRate = sgRate1 / sgRate;
                    }
                }
                else sgRate = 1;

                lblCurrRate.Text = sgRate.ToString();
                txtBasicFees.Text = "0.00";
            }
            CalcFees();
            lblRate.Text = "Ισοτιμία " + sProviderMainCurr + "/" + cmbAmountCurr.Text;

            //-------------- Define Cash Accounts List ------------------
            clsContracts_CashAccounts ClientCashAccounts = new clsContracts_CashAccounts();
            ClientCashAccounts.Client_ID = 0;
            ClientCashAccounts.Code = lblCode.Text;
            ClientCashAccounts.Contract_ID = iContract_ID;
            ClientCashAccounts.GetList_CashAccount();

            dtAccs = new DataTable("AccsList");
            dtCol = dtAccs.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dtCol = dtAccs.Columns.Add("AccountNumber", System.Type.GetType("System.String"));
            dtCol = dtAccs.Columns.Add("Currency", System.Type.GetType("System.String"));
            foreach (DataRow dtRow in ClientCashAccounts.List.Rows)
            {
                dtRow1 = dtAccs.NewRow();
                dtRow1["ID"] = dtRow["ID"] + "";
                dtRow1["AccountNumber"] = dtRow["AccountNumber"] + "";
                dtRow1["Currency"] = dtRow["Currency"] + "";
                dtAccs.Rows.Add(dtRow1);
            }

            cmbCashAccounts.DataSource = dtAccs.Copy();
            cmbCashAccounts.DisplayMember = "AccountNumber";
            cmbCashAccounts.ValueMember = "ID";
            cmbCashAccounts.SelectedValue = klsOrderLL.CashAccount_ID;

            //-------------- Define Recieved Files List ------------------
            klsOrderLL2 = new clsOrdersLL();
            klsOrderLL2.Record_ID = iRecord_ID;
            klsOrderLL2.GetRecievedFiles();

            fgRecieved.Redraw = false;
            fgRecieved.Rows.Count = 1;
            foreach (DataRow dtRow in klsOrderLL2.List.Rows)
                fgRecieved.AddItem(dtRow["DateIns"] + "\t" + dtRow["Method_Title"] + "\t" + dtRow["FileName"] + "\t" +
                                   dtRow["ID"] + "\t" + dtRow["Method_ID"] + "\t" + "");                                       //drList("FilePath")

            fgRecieved.Redraw = true;

            //-------------- Define Informings List -----------------
            /*
            klsOrderLL2 = new clsOrdersLL();
            klsOrderLL2.Record_ID = iRecord_ID;
            klsOrderLL2.GetInformings();

            fgInforming.Redraw = false;
            fgInforming.Rows.Count = 1;
            foreach (DataRow dtRow in klsOrderLL2.List.Rows)
                fgInforming.AddItem(dtRow["DateIns"] + "\t" + dtRow["InformationMethod"] + "\t" + dtRow["FileName"] + "\t" +
                                    dtRow["DateSent"] + "\t" + dtRow["ID"] + "\t" + dtRow["InformMethod"] + "\t" + dtRow["User_ID"] + "\t" + "");

            fgInforming.Redraw = true;
            */

            //-------------- Define Check List -----------------
            klsOrderLL2 = new clsOrdersLL();
            klsOrderLL2.Record_ID = iRecord_ID;
            klsOrderLL2.GetChecks();

            fgCheck.Redraw = false;
            fgCheck.Rows.Count = 1;
            foreach (DataRow dtRow in klsOrderLL2.List.Rows)
                fgCheck.AddItem(dtRow["DateIns"] + "\t" + dtRow["UserName"] + "\t" + sCheck[Convert.ToInt32(dtRow["Status"])] + "\t" +
                                        dtRow["ProblemType_Title"] + "\t" + dtRow["Notes"] + "\t" + dtRow["FileName"] + "\t" +
                                        dtRow["ReversalRequestDate"] + "\t" + dtRow["ID"] + "\t" + dtRow["User_ID"] + "\t" +
                                        dtRow["Status"] + "\t" + "" + "\t" + dtRow["ProblemType_ID"]);                // preLast Column - Empty, it's shows that it "old" file. "New" file has full path of file

            fgCheck.Redraw = true;

            iMode = 1;
            bCheckList = true;
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
            dSend.Focus();

            picEmptyExecute.Enabled = true;
            btnExecuted.Enabled = true;
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
                    OrdersLL_Check = new clsOrdersLL_Check();
                    OrdersLL_Check.Record_ID = Convert.ToInt32(fgCheck[fgCheck.Row, 7]);
                    OrdersLL_Check.DeleteRecord();
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
                        Global.DMS_ShowFile("Customers/" + klsOrderLL.ContractTitle + "/Informing", fgCheck[fgCheck.Row, 5].ToString());      //is DMS file, so show it into Web mode
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
        }

        private void txtAmount_LostFocus(object sender, EventArgs e)
        {
            if (Global.IsNumeric(txtAmount.Text))
                 lblLL_AS.Text =(Convert.ToSingle(txtAmount.Text) * 100 / Convert.ToSingle(txtLTV.Text)).ToString();
            else txtAmount.Text = "0";
            CalcFees();
        }
        private void txtLTV_LostFocus(object sender, EventArgs e)
        {
            if (Global.IsNumeric(txtAmount.Text))
                lblLL_AS.Text = (Convert.ToSingle(txtAmount.Text) * 100 / Convert.ToSingle(txtLTV.Text)).ToString();
            else txtAmount.Text = "0";
            CalcFees();
        }

        private void txtProviderRate_LostFocus(object sender, EventArgs e)
        {
            CalcRates();
        }

        private void txtAdditionalRate_LostFocus(object sender, EventArgs e)
        {
            CalcRates();
        }

        private void txtDiscount_LostFocus(object sender, EventArgs e)
        {
            CalcRates();
        }

        private void txtFinalMargin_LostFocus(object sender, EventArgs e)
        {
            if (Global.IsNumeric(txtAdditionalRate.Text)) {
                sgRate = Convert.ToSingle(txtAdditionalRate.Text);

                if (Global.IsNumeric(txtFinalMargin.Text))
                    txtDiscount.Text = (100 - Convert.ToSingle(txtFinalMargin.Text) * 100 / sgRate).ToString();
                else
                {
                    txtDiscount.Text = "0";
                    txtFinalMargin.Text = "0";
                }
            }
            else {
                txtDiscount.Text = "0";
                txtFinalMargin.Text = "0";
            }
            CalcRates();
        }       
        #region --- fgRecieved functions ----------------------------------------------------------------
        private void picAddRecieved_Click(object sender, EventArgs e)
        {
            fgRecieved.AddItem(Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy HH:mm:ss") + "\t" + "" + "\t" + "" + "\t" + "0" + "\t" + "0" + "\t" + "", 1);
            if (Convert.ToDateTime(dSend.Value) != Convert.ToDateTime("1900/01/01"))
                if (Convert.ToDateTime(dSend.Value) <= DateTime.Now)
                    MessageBox.Show("Wrong Date: Ημερομηνία Λήψης δεν μπορεί να είναι μεγαλίτερη απο Ημερομηνία Διαβίβασης", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tslCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(sMessage, Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                klsOrderLL.Status = iStatus;
                klsOrderLL.EditRecord();
                iLastAktion = 1;             // was saved (cancel)
                this.Close();
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            string sNewFileName = "";
            //--- At begining systems saves of fgRecieved, fgInforming and fgCheck records, because  names of upload files can change. So in Command record will save new file names 
            for (i = 1; i <= fgRecieved.Rows.Count - 1; i++)
            {
                sNewFileName = (fgRecieved[i, 2] + "").Trim();
                if ((fgRecieved[i, 5] + "") != "")
                {
                    sNewFileName = Global.DMS_UploadFile(fgRecieved[i, 5] + "", "Customers/" + lblContractTitle.Text.Replace(".", "_") + "/OrdersAcception", sNewFileName);
                    if (sNewFileName.Length > 0) sNewFileName = Path.GetFileName(sNewFileName);
                    else
                        MessageBox.Show("Αρχείο " + fgRecieved[i, 2] + " δεν αντιγράφτηκε στο DMS", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }

                if (Convert.ToInt32(fgRecieved[i, 3]) == 0)
                {
                    OrdersLL_Recieved = new clsOrdersLL_Recieved();
                    OrdersLL_Recieved.CommandLL_ID = iRecord_ID;
                    OrdersLL_Recieved.DateIns = Convert.ToDateTime(fgRecieved[i, 0]);
                    OrdersLL_Recieved.Method_ID = Convert.ToInt32(fgRecieved[i, 4]);
                    OrdersLL_Recieved.FilePath = fgRecieved[i, 5] + "";
                    OrdersLL_Recieved.FileName = sNewFileName;
                    OrdersLL_Recieved.InsertRecord();
                }
                else
                {
                    OrdersLL_Recieved.Record_ID = Convert.ToInt32(fgRecieved[i, 3]);
                    OrdersLL_Recieved.GetRecord();
                    OrdersLL_Recieved.CommandLL_ID = iRecord_ID;
                    OrdersLL_Recieved.DateIns = Convert.ToDateTime(fgRecieved[i, 0]);
                    OrdersLL_Recieved.Method_ID = Convert.ToInt32(fgRecieved[i, 4]);
                    OrdersLL_Recieved.FilePath = fgRecieved[i, 5] + "";
                    OrdersLL_Recieved.FileName = sNewFileName;
                    OrdersLL_Recieved.EditRecord();
                }
            }


            for (i = 1; i <= fgCheck.Rows.Count - 1; i++)
            {

                if ((fgCheck[i, "FileFullName"] + "").Trim() != "")
                {                                     // FileFullName - Not Empty means that it's a new file
                    sTemp = Global.DMS_UploadFile(fgCheck[i, "FileFullName"] + "", "Customers/" + lblContractTitle.Text.Replace(".", "_") + "/Informing", fgCheck[i, 5] + "");
                    fgCheck[i, 5] = Path.GetFileName(sTemp);
                }

                if (Convert.ToInt32(fgCheck[i, "ID"]) == 0)
                {
                    OrdersLL_Check = new clsOrdersLL_Check();
                    OrdersLL_Check.CommandLL_ID = iRecord_ID;
                    OrdersLL_Check.DateIns = Convert.ToDateTime(fgCheck[i, "DateIns"]);
                    OrdersLL_Check.User_ID = Convert.ToInt32(fgCheck[i, "User_ID"]);
                    OrdersLL_Check.Status = Convert.ToInt32(fgCheck[i, "Status"]);
                    OrdersLL_Check.ProblemType_ID = Convert.ToInt32(fgCheck[i, "ProblemType_ID"]);
                    OrdersLL_Check.Notes = fgCheck[i, "Notes"] + "";
                    OrdersLL_Check.FileName = fgCheck[i, 5] + "";
                    OrdersLL_Check.ReversalRequestDate = fgCheck[i, "ReversalRequestMailed"] + "";
                    OrdersLL_Check.InsertRecord();
                }
                else
                {
                    OrdersLL_Check.Record_ID = Convert.ToInt32(fgCheck[i, "ID"]);
                    OrdersLL_Check.GetRecord();
                    OrdersLL_Check.CommandLL_ID = iRecord_ID;
                    OrdersLL_Check.DateIns = Convert.ToDateTime(fgCheck[i, "DateIns"]);
                    OrdersLL_Check.User_ID = Convert.ToInt32(fgCheck[i, "User_ID"]);
                    OrdersLL_Check.Status = Convert.ToInt32(fgCheck[i, "Status"]);
                    OrdersLL_Check.ProblemType_ID = Convert.ToInt32(fgCheck[i, "ProblemType_ID"]);
                    OrdersLL_Check.Notes = fgCheck[i, "Notes"] + "";
                    OrdersLL_Check.FileName = fgCheck[i, 5] + "";
                    OrdersLL_Check.ReversalRequestDate = fgCheck[i, "ReversalRequestMailed"] + "";
                    OrdersLL_Check.EditRecord();
                }
            }
                  

            //--- Edit Command ----------------------------------
            klsOrderLL = new clsOrdersLL();
            klsOrderLL.Record_ID = iRecord_ID;
            klsOrderLL.GetRecord();

            klsOrderLL.AktionDate = dAktionDate.Value;
            klsOrderLL.CashAccount_ID = Convert.ToInt32(cmbCashAccounts.SelectedValue);
            klsOrderLL.Amount = Convert.ToSingle(txtAmount.Text);
            klsOrderLL.Curr = cmbAmountCurr.Text;
            klsOrderLL.LTV = Convert.ToSingle(txtLTV.Text);
            klsOrderLL.LL_AS = Convert.ToSingle(lblLL_AS.Text);
            klsOrderLL.ProviderRate = Convert.ToSingle(txtProviderRate.Text);
            klsOrderLL.AdditionalRate = Convert.ToSingle(txtAdditionalRate.Text);
            klsOrderLL.Discount = Convert.ToSingle(txtDiscount.Text);
            klsOrderLL.FinalMargin = Convert.ToSingle(txtFinalMargin.Text);
            klsOrderLL.GrossRate = Convert.ToSingle(txtGrossRate.Text);
            klsOrderLL.PeriodStart = dPeriodStart.Value;
            klsOrderLL.PeriodEnd = dPeriodEnd.Value;
            klsOrderLL.Days = Convert.ToInt32(lblDays.Text);
            klsOrderLL.CurrRate = Convert.ToDecimal(lblCurrRate.Text != "" ? lblCurrRate.Text : "0");
            klsOrderLL.RecieveDate = DateTime.Now;
            klsOrderLL.SentDate = Convert.ToDateTime("1900/01/01");
            klsOrderLL.ExecuteDate = Convert.ToDateTime("1900/01/01");
            //klsOrderLL.InformationMethod_ID = 0;
            klsOrderLL.Notes = txtNotes.Text;
            //klsOrderLL.User_ID = Global.User_ID;
            klsOrderLL.DateIns = DateTime.Now;
            //klsOrderLL.Status = 0;
            //klsOrderLL.Pinakidio = 0;
            //klsOrderLL.LastCheckFile = "";
            klsOrderLL.CompanyFeesPercent = 50;
            
            dTemp = dRecieved;
            i = 0;
            if (fgRecieved.Rows.Count > 1)
            {
                dTemp = Convert.ToDateTime(fgRecieved[1, 0]);   //   last recieved file date
                i = Convert.ToInt32(fgRecieved[1, 4]);          //   last recieved file method
            }

            klsOrderLL.RecieveDate = dTemp;
            klsOrderLL.RecieveMethod_ID = i;
            if (dSend.Text.Trim() != "")
            {
                dTemp = Convert.ToDateTime(dSend.Text.Trim());
                sTemp = dTemp.ToString("yyyy-MM-dd") + " " + (txtSendHour.Text.Trim() == "" ? "00" : txtSendHour.Text.Trim()) + ":" +
                                                             (txtSendMinute.Text.Trim() == "" ? "00" : txtSendMinute.Text.Trim()) + ":" +
                                                             (txtSendSecond.Text.Trim() == "" ? "00" : txtSendSecond.Text.Trim());
            }
            else sTemp = "1900/01/01 00:00:00";

            klsOrderLL.SentDate = Convert.ToDateTime(sTemp);

            if (dExecute.Text.Trim() != "")
            {
                dTemp = Convert.ToDateTime(dExecute.Text.Trim());
                sTemp = dTemp.ToString("yyyy-MM-dd") + " " + (txtExecuteHour.Text.Trim() == "" ? "00" : txtExecuteHour.Text.Trim()) + ":" +
                                                             (txtExecuteMinute.Text.Trim() == "" ? "00" : txtExecuteMinute.Text.Trim()) + ":" +
                                                             (txtExecuteSecond.Text.Trim() == "" ? "00" : txtExecuteSecond.Text.Trim());
            }
            else sTemp = "1900/01/01 00:00:00";

            klsOrderLL.ExecuteDate = Convert.ToDateTime(sTemp);

            klsOrderLL.Notes = txtNotes.Text;
            klsOrderLL.EditRecord();

            this.Close();
            iLastAktion = 1;            // 1 - was saved (added)
        }

        private void dPeriodStart_ValueChanged(object sender, EventArgs e)
        {
            if (dPeriodEnd.Value > dPeriodStart.Value)
                lblDays.Text = (Convert.ToInt32((dPeriodEnd.Value - dPeriodStart.Value).TotalDays) + 1).ToString();
        }

        private void dPeriodEnd_ValueChanged(object sender, EventArgs e)
        {
            if (dPeriodEnd.Value > dPeriodStart.Value)
                lblDays.Text = (Convert.ToInt32((dPeriodEnd.Value - dPeriodStart.Value).TotalDays) + 1).ToString();
        }

        private void picDelRecieved_Click(object sender, EventArgs e)
        {
            if (fgRecieved.Row > 0)
            {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    clsOrdersLL_Recieved OrdersLL_Recieved = new clsOrdersLL_Recieved();
                    OrdersLL_Recieved.Record_ID = Convert.ToInt32(fgRecieved[fgRecieved.Row, 3]);
                    OrdersLL_Recieved.DeleteRecord();
                    fgRecieved.RemoveItem(fgRecieved.Row);
                }
            }
        }
        private void picShowRecieved_Click(object sender, EventArgs e)
        {
            if ((fgRecieved[fgRecieved.Row, 5] + "").Trim() != "")
                System.Diagnostics.Process.Start(fgRecieved[fgRecieved.Row, 5] + "");
            else
               if ((fgRecieved[fgRecieved.Row, 2] + "").Trim() != "")
                Global.DMS_ShowFile("Customers/" + sClientFullName.Replace(".", "_") + "/OrdersAcception", (fgRecieved[fgRecieved.Row, 2] + ""));
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
        #endregion
        #region --- fgCheck functions ----------------------------------------------------------------
        private void fgCheck_CellButtonClick(object sender, RowColEventArgs e)
        {
            if (e.Col == 5)
            {
                fgCheck[fgCheck.Row, 10] = Global.FileChoice(Global.DefaultFolder);
                fgCheck[fgCheck.Row, 5] = Path.GetFileName(fgCheck[fgCheck.Row, 10] + "");
            }
        }
        private void fgCheck_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Col == 2) fgCheck[e.Row, 9] = fgCheck[e.Row, 2];
            if (e.Col == 3) fgCheck[e.Row, 11] = fgCheck[e.Row, 3];
        }
        private void fgCheck_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (bCheckList)
            {
                if (e.Col == 0 || e.Col == 1) e.Cancel = true;
                else e.Cancel = false;
            }
        }
        #endregion
        private void CalcFees()
        {
            txtBasicFees.Text = "0.00";
            if (Global.IsNumeric(lblCurrRate.Text))
            {
                if (Convert.ToDecimal(lblCurrRate.Text) != 0)
                {
                    if (Global.IsNumeric(txtAmount.Text) && Global.IsNumeric(txtAdditionalRate.Text) && Global.IsNumeric(lblDays.Text))
                    {
                        txtBasicFees.Text = (Convert.ToDecimal(txtAmount.Text) / Convert.ToDecimal(lblCurrRate.Text) * Convert.ToDecimal(txtAdditionalRate.Text) / 100 * Convert.ToDecimal(lblDays.Text) / 360).ToString("0.##");
                    }
                }
            }
        }
        private void CalcRates()
        {
            if (!Global.IsNumeric(txtProviderRate.Text)) txtProviderRate.Text = "0.00";

            if (Global.IsNumeric(txtAdditionalRate.Text)) sgRate = Convert.ToSingle(txtAdditionalRate.Text);
            else sgRate = 0;

            if (Global.IsNumeric(txtDiscount.Text)) sgRate1 = Convert.ToSingle(txtDiscount.Text);
            else sgRate1 = 0;

            txtFinalMargin.Text = (sgRate - sgRate * sgRate1 / 100).ToString();

            txtGrossRate.Text = (Convert.ToSingle(txtProviderRate.Text) + Convert.ToSingle(txtFinalMargin.Text)).ToString("0.####");

            CalcFees();
        }
        public int LastAktion { get { return iLastAktion; } set { iLastAktion = value; } }
        public int Record_ID { get { return iRecord_ID; } set { iRecord_ID = value; } }
        public int RightsLevel { get { return iRightsLevel; } set { iRightsLevel = value; } }
        public int Editable { get { return iEditable; } set { iEditable = value; } }
        public int Mode { get { return iMode; } set { iMode = value; } }                            // 1 - from frmDailyFX, 2 - from frmAcc_InvoicesFX
    }
}
