using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using C1.Win.C1FlexGrid;

namespace Core
{
    public partial class frmOrderBulk : Form
    {
        int i, iRec_ID, iLastAktion, iCommandType_ID, iCustody_ID, iDepository_ID, iEditable, iContract_ID, iFeesCalcMode, iStatus, iRightsLevel,
            iClient_ID, iServiceProvider_ID, iBusinessType, iStockExchange_ID, iBulcCommand_ID, iClientTipos, iShare_ID, iBulcCommand2_ID,
            iProduct_ID, iProductCategory_ID;
        decimal decKoef, decRealQuantity, decRealAmount, decInvestAmount, decTemp, decTemp2, decRealPrice;
        string[] sCheck = { "Δεν ελέγχθηκε", "OK", "Πρόβλημα" };
        string sTemp, sBulkCommand, sProviderMainCurr, sMessage, sStockExchange, sSubPath;
        bool bFound, bCheckList, bCheckShare, bContinue, bEditKatamerismos, bFIX_A;
        DateTime dTemp, dRecieved, dExecute;
        DataView dtView;
        DataRow[] foundRows;
        CellRange rng;
        SortedList lstProblems = new SortedList();
        SortedList lstStatus = new SortedList();
        clsOrdersSecurity klsOrder = new clsOrdersSecurity();
        clsOrdersSecurity Orders3 = new clsOrdersSecurity();
        clsNewOrders NewOrders = new clsNewOrders();
        clsExecutionReports ExecutionReports = new clsExecutionReports();
        clsOrders_Executions Orders_Executions = new clsOrders_Executions();
        clsCommandsExecutionsDetails CommandsExecutionsDetails = new clsCommandsExecutionsDetails();

        #region --- Start functions -----------------------------------------------------------------------------
        public frmOrderBulk()
        {
            InitializeComponent();

            this.Width = 936;
            this.Height = 786;

            bEditKatamerismos = false;

            panEMail.Left = 110;
            panEMail.Top = 364;

            panEdit.Left = 302;
            panEdit.Top = 466;

            bCheckList = false;
            bCheckShare = false;

            lblWarning.Visible = false;
            lblWarning.Left = 4;

            panWarning.Visible = false;
            iClientTipos = 0;
            iLastAktion = 0;
            iCustody_ID = 0;
            iDepository_ID = 0;
            dRecieved = Convert.ToDateTime("1900/01/01");
        }
        private void frmOrderBulk_Load(object sender, EventArgs e)
        {
            this.Text = "Εντολή (" + iRec_ID + ")";

            iContract_ID = 0;
            iClient_ID = 0;
            sSubPath = "";

            if (iCommandType_ID == 2)
            {             // 2 - ExecutionOrder, 3 - BulkOrder
                panPackage.Visible = true;
                panCustody.Visible = true;
            }
            else
            {
                panPackage.Visible = false;
                panCustody.Visible = false;
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

            ucCS.StartInit(700, 400, 200, 20, 1);
            ucCS.TextOfLabelChanged += new EventHandler(ucCS_TextChange);
            ucCS.Filters = "Status = 1";
            ucCS.ListType = 1;

            ucPS.StartInit(700, 400, 200, 20, 1);
            ucPS.TextOfLabelChanged += new EventHandler(ucPS_TextChange);
            ucPS.ListType = 1;
            ucPS.Filters = "Aktive >= 1 ";

            dSend.CustomFormat = "          ";
            dSend.Format = DateTimePickerFormat.Custom;
            dSend.Enabled = false;
            txtSendHour.Enabled = false;
            txtSendMinute.Enabled = false;
            txtSendSecond.Enabled = false;

            //-------------- Define Advisors List ------------------   
            dtView = Global.dtUserList.Copy().DefaultView;
            dtView.RowFilter = "Sender = 1 AND Aktive = 1";
            cmbSenders.DataSource = dtView;
            cmbSenders.DisplayMember = "Title";
            cmbSenders.ValueMember = "ID";
            cmbSenders.SelectedValue = 0;

            //----- initialize StockExchanges List -------
            cmbStockExchanges.DataSource = Global.dtStockExchanges.Copy();
            cmbStockExchanges.DisplayMember = "Code";                                    // Code = Title / MIC
            cmbStockExchanges.ValueMember = "ID";
            cmbStockExchanges.SelectedValue = 0;

            //----- initialize SettlementProviders List -------
            cmbSettlementProviders.DataSource = Global.dtServiceProviders.Copy();
            cmbSettlementProviders.DisplayMember = "Title";
            cmbSettlementProviders.ValueMember = "ID";
            cmbSettlementProviders.SelectedValue = 0;

            //----- initialize SettlementProviders List -------
            cmbServiceProvider.DataSource = Global.dtServiceProviders.Copy();
            cmbServiceProvider.DisplayMember = "Title";
            cmbServiceProvider.ValueMember = "ID";
            cmbServiceProvider.SelectedValue = 0;

            //----- initialize Depositories List -------
            cmbDepositories.DataSource = Global.dtDepositories.Copy();
            cmbDepositories.DisplayMember = "Title";
            cmbDepositories.ValueMember = "ID";
            cmbDepositories.SelectedValue = 0;

            //'------- fgSimpleCommands ----------------------------
            fgSingleOrders.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgSingleOrders.Styles.ParseString(Global.GridStyle);
            fgSingleOrders.DrawMode = DrawModeEnum.OwnerDraw;
            fgSingleOrders.ShowCellLabels = true;
            fgSingleOrders.DoubleClick += new System.EventHandler(fgSimpleCommands_DoubleClick);

            fgSingleOrders.Styles.Normal.WordWrap = true;
            fgSingleOrders.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgSingleOrders.Rows[0].AllowMerging = true;
            fgSingleOrders.Cols[0].AllowMerging = true;
            rng = fgSingleOrders.GetCellRange(0, 0, 1, 0);
            rng.Data = "ΑΑ";

            fgSingleOrders.Cols[1].AllowMerging = true;
            rng = fgSingleOrders.GetCellRange(0, 1, 1, 1);
            rng.Data = "Σύμβαση / Εντολέας";

            fgSingleOrders.Cols[2].AllowMerging = true;
            rng = fgSingleOrders.GetCellRange(0, 2, 1, 2);
            rng.Data = "Κωδικός";

            fgSingleOrders.Cols[3].AllowMerging = true;
            rng = fgSingleOrders.GetCellRange(0, 3, 1, 3);
            rng.Data = "Portfolio";

            fgSingleOrders.Cols[4].AllowMerging = true;
            rng = fgSingleOrders.GetCellRange(0, 4, 1, 4);
            rng.Data = "Ημερομηνία Εκτέλεσης";

            rng = fgSingleOrders.GetCellRange(0, 5, 0, 7);
            rng.Data = Global.GetLabel("order");

            fgSingleOrders[1, 5] = Global.GetLabel("price");
            fgSingleOrders[1, 6] = Global.GetLabel("quantity");
            fgSingleOrders[1, 7] = Global.GetLabel("amount");

            rng = fgSingleOrders.GetCellRange(0, 8, 0, 10);
            rng.Data = Global.GetLabel("executed_command");

            fgSingleOrders[1, 8] = Global.GetLabel("price");
            fgSingleOrders[1, 9] = Global.GetLabel("quantity");
            fgSingleOrders[1, 10] = Global.GetLabel("amount");


            //------- fgExecutions ----------------------------
            fgExecutions.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgExecutions.Styles.ParseString(Global.GridStyle);
            fgExecutions.DrawMode = DrawModeEnum.OwnerDraw;
            fgExecutions.ShowCellLabels = true;
            fgExecutions.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(fgExecutions_CellChanged);

            //------- fgCheck ----------------------------
            fgCheck.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgCheck.Styles.ParseString(Global.GridStyle);
            fgCheck.DrawMode = DrawModeEnum.OwnerDraw;
            fgCheck.ShowCellLabels = true;
            fgCheck.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(fgCheck_CellButtonClick);
            fgCheck.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(fgCheck_CellChanged);
            fgCheck.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgCheck_BeforeEdit);

            lstStatus.Clear();
            lstStatus.Add("0", "");
            lstStatus.Add("1", sCheck[1]);
            lstStatus.Add("2", sCheck[2]);
            fgCheck.Cols[2].DataMap = lstStatus;

            lstProblems.Clear();
            foreach (DataRow dtRow in Global.dtCheckProblems.Rows)
                lstProblems.Add(dtRow["ID"], dtRow["Title"]);

            fgCheck.Cols[3].DataMap = lstProblems;

            Column col5 = fgCheck.Cols[5];
            col5.Name = "Image";
            col5.DataType = typeof(String);
            col5.ComboList = "...";

            //---- Start Initialisation - Show Command --------------

            if (iRec_ID != 0)
            {
                this.Width = 936;
                this.Height = 786;

                lblServiceProvider.Visible = false;
                cmbServiceProvider.Visible = false;

                klsOrder.Record_ID = iRec_ID;
                klsOrder.GetRecord();

                switch (klsOrder.Aktion)
                {
                    case 1:
                        pan1.BackColor = Color.MediumAquamarine;
                        pan2.BackColor = Color.MediumAquamarine;
                        pan3.BackColor = Color.MediumAquamarine;
                        tcExecution.TabPages[0].BackColor = Color.MediumAquamarine;
                        tcExecution.TabPages[1].BackColor = Color.MediumAquamarine;
                        tcExecution.TabPages[2].BackColor = Color.MediumAquamarine;
                        break;
                    case 2:
                        pan1.BackColor = Color.LightCoral;
                        pan2.BackColor = Color.LightCoral;
                        pan3.BackColor = Color.LightCoral;
                        tcExecution.TabPages[0].BackColor = Color.LightCoral;
                        tcExecution.TabPages[1].BackColor = Color.LightCoral;
                        tcExecution.TabPages[2].BackColor = Color.LightCoral;
                        break;
                    case 3:
                        pan1.BackColor = Color.Silver;
                        pan2.BackColor = Color.Silver;
                        pan3.BackColor = Color.Silver;
                        tcExecution.TabPages[0].BackColor = Color.Silver;
                        tcExecution.TabPages[1].BackColor = Color.Silver;
                        tcExecution.TabPages[2].BackColor = Color.Silver;
                        break;
                }
                sBulkCommand = klsOrder.BulkCommand.Replace("<", "").Replace(">", "");
                cmbStockExchanges.SelectedValue = klsOrder.StockExchange_ID;
                iCustody_ID = klsOrder.CustodyProvider_ID;
                cmbSettlementProviders.SelectedValue = iCustody_ID;
                iDepository_ID = klsOrder.Depository_ID;
                cmbDepositories.SelectedValue = iDepository_ID;
                sStockExchange = klsOrder.StockExchange_Title;
                iServiceProvider_ID = klsOrder.ServiceProvider_ID;
                lblStockCompany.Text = klsOrder.ServiceProvider_Title;
                lblContractTitle.Text = klsOrder.CompanyTitle;
                ucCS.ShowClientsList = false;
                ucCS.txtContractTitle.Text = klsOrder.Code;
                ucCS.ShowClientsList = true;
                txtPortfolio.Text = klsOrder.ProfitCenter;
                txtAction.Text = (klsOrder.Aktion == 1 ? "BUY" : "SELL");
                dAktionDate.Value = klsOrder.AktionDate;
                iProduct_ID = klsOrder.Product_ID;

                if (iProduct_ID == 2) decKoef = 100;           // 2 - Omologo, Bond                    
                else decKoef = 1;

                sSubPath = (klsOrder.ContractTipos == 0 ? klsOrder.ClientName : klsOrder.ContractTitle).Replace(".", "_");   // 0 - Personal Contract, 1 - Company Contract, 2 - Joint Contract
                lblProduct.Text = klsOrder.Product_Title;
                iProductCategory_ID = klsOrder.ProductCategory_ID;
                lblProductCategory.Text = klsOrder.ProductCategory_Title;
                lblProductStockExchange_Title.Text = klsOrder.ProductStockExchange_Title;
                iShare_ID = klsOrder.Share_ID;
                ucPS.ShowProductsList = false;
                ucPS.txtShareTitle.Text = klsOrder.Security_Code;
                ucPS.ShowProductsList = true;
                lnkISIN.Text = klsOrder.Security_ISIN;
                lblShareTitle.Text = klsOrder.Security_Title;
                cmbConstant.SelectedIndex = klsOrder.Constant;
                dConstant.Text = klsOrder.ConstantDate;
                txtPrice.Text = klsOrder.Price.ToString("0.#######");
                txtQuantity.Text = klsOrder.Quantity.ToString("0.#######");
                txtAmount.Text = klsOrder.Amount.ToString("0.##");
                lblCurr.Text = klsOrder.Curr;
                lblCurrRate_Title.Text = "EUR/" + klsOrder.Curr;
                if (klsOrder.Curr == "EUR") lblCurrRate.Text = "1";
                lblCurrRate.Text = klsOrder.CurrRate.ToString("0.#######");

                if (Convert.ToDateTime(klsOrder.SentDate) != Convert.ToDateTime("1900/01/01"))
                {
                    dTemp = Convert.ToDateTime(klsOrder.SentDate);
                    dSend.Value = dTemp;
                    dSend.CustomFormat = "dd/MM/yyyy";
                    dSend.Format = System.Windows.Forms.DateTimePickerFormat.Short;
                    txtSendHour.Text = dTemp.Hour.ToString();
                    txtSendMinute.Text = dTemp.Minute.ToString();
                    txtSendSecond.Text = dTemp.Second.ToString();
                    cbChecked.Checked = (klsOrder.SendCheck == 0 ? false : true);

                    dSend.Enabled = true;
                    txtSendHour.Enabled = true;
                    txtSendMinute.Enabled = true;
                    txtSendSecond.Enabled = true;

                    btnSend.Enabled = false;
                }
                else
                {
                    btnSend.Enabled = true;
                    cbChecked.Checked = false;
                    btnExecuted.Enabled = false;
                }

                dRecieved = klsOrder.RecieveDate;
                chkBestExecution.Checked = klsOrder.BestExecution == 1 ? true : false;

                bFIX_A = klsOrder.FIX_A == 1 ? true : false;
                decRealPrice = klsOrder.RealPrice;
                decRealQuantity = klsOrder.RealQuantity;
                decRealAmount = klsOrder.RealAmount;
                lblProviderFees.Text = klsOrder.ProviderFees.ToString("0.00##");
                txtAccruedInterest.Text = klsOrder.AccruedInterest.ToString("0.00##");
                txtFeesDiff.Text = klsOrder.FeesDiff.ToString("0.00##");
                txtFeesMarket.Text = klsOrder.FeesMarket.ToString("0.00##");

                if (Convert.ToDateTime(klsOrder.ExecuteDate) == Convert.ToDateTime("1900/01/01"))
                {
                    btnExecuted.Enabled = true;
                    toolExecutions.Enabled = false;
                }
                else
                {
                    btnExecuted.Enabled = false;
                    toolExecutions.Enabled = true;
                    fgExecutions.AllowEditing = true;
                }

                lblPackage.Text = klsOrder.Package_Title;
                lblFeesCurr.Text = klsOrder.Curr;
                lblFeesPercent.Text = klsOrder.FeesPercent.ToString("0.00##");
                lblFeesAmount.Text = klsOrder.FeesAmount.ToString("0.00##");
                lblFeesDiscountPercent.Text = klsOrder.FeesDiscountPercent.ToString("0.00##");
                lblFeesDiscountAmount.Text = klsOrder.FeesDiscountAmount.ToString("0.00##");
                lblFinishFeesPercent.Text = klsOrder.FinishFeesPercent.ToString("0.00##");
                lblFinishFeesAmount.Text = klsOrder.FinishFeesAmount.ToString("0.00##");
                lblMinFees.Text = klsOrder.MinFeesAmount.ToString("0.00##");
                lblMinFeesCurr.Text = klsOrder.MinFeesCurr;
                lblFeesNotes.Text = klsOrder.FeesNotes;

                sProviderMainCurr = klsOrder.MainCurr;
                txtNotes.Text = klsOrder.Notes;
                lstType.SelectedIndex = klsOrder.PriceType;
                if (lstType.SelectedIndex == 1) txtPrice.Text = "M";                        // 1 - Market
                iFeesCalcMode = klsOrder.FeesCalcMode;

                switch (klsOrder.Product_ID)
                {
                    case 1:
                        lblQuantity.Text = "Τεμάχια";
                        fgExecutions[0, 3] = "Τεμάχια";
                        break;
                    case 2:                                                                // Bond (Omologa)
                        lblQuantity.Text = "Ονομ.Αξία";
                        fgExecutions[0, 3] = "Ονομ.Αξία";
                        break;

                    case 4:                                                                // ETF (DAK)
                        lblQuantity.Text = "Τεμάχια";
                        fgExecutions[0, 3] = "Τεμάχια";
                        break;
                    case 6:                                                                // FUND (AK)
                        lblQuantity.Text = "Μερίδια";
                        fgExecutions[0, 3] = "Μερίδια";
                        break;
                }
                cmbSenders.SelectedValue = klsOrder.User_ID;

                decRealQuantity = 0;
                decRealAmount = 0;
                //--- Define fgExecutions Grid ---------------------------------- 
                fgExecutions.Redraw = false;
                fgExecutions.Rows.Count = 1;

                clsOrders_Executions Orders_Executions = new clsOrders_Executions();
                Orders_Executions.Command_ID = iRec_ID;
                Orders_Executions.GetList();
                foreach (DataRow dtRow in Orders_Executions.List.Rows)
                {
                    iStockExchange_ID = 0;
                    foundRows = Global.dtStockExchanges.Select("Code = '" + dtRow["StockExchange_MIC"] + "'");
                    if (foundRows.Length > 0) iStockExchange_ID = Convert.ToInt32(foundRows[0]["ID"]);

                    fgExecutions.AddItem(Convert.ToDateTime(dtRow["DateExecution"]).ToString("dd/MM/yyyy HH:mm:ss") + "\t" + dtRow["ProviderCommandNumber"] + "\t" +
                                            string.Format("{0:#0.00##}", dtRow["RealPrice"]) + "\t" + string.Format("{0:#0.0######}", dtRow["RealQuantity"]) + "\t" +
                                            string.Format("{0:#0.00}", dtRow["RealAmount"]) + "\t" + dtRow["StockExchange_MIC"] + "\t" + dtRow["ID"] + "\t" +
                                            iStockExchange_ID);
                    decRealQuantity = decRealQuantity + Convert.ToDecimal(dtRow["RealQuantity"]);
                    decRealAmount = decRealAmount + Convert.ToDecimal(dtRow["RealAmount"]);
                }
                fgExecutions.Redraw = true;
                if (decRealQuantity > 0)
                {
                    lblSumPrice.Text = (decRealAmount * decKoef / decRealQuantity).ToString("0.00##");
                    lblSumQuantity.Text = decRealQuantity.ToString("0.00");
                    lblSumAmount.Text = decRealAmount.ToString("0.00");
                }

                decInvestAmount = decRealAmount;
                if (fgExecutions.Rows.Count > 1) btnExecuted.Enabled = false;
                else btnExecuted.Enabled = true;

                if (klsOrder.Status >= 0)
                {
                    tslCancel.Text = "Ακύρωση εντολής";
                    sMessage = "ΠΡΟΣΟΧΗ! Ζητήσατε να ακυρωθεί η εντολή. \n\n Είστε σίγουρος για την ακύρωση της;";
                    iStatus = -1;
                }
                else
                {
                    tslCancel.Enabled = false;
                    tsbSave.Enabled = false;
                    btnSend.Enabled = false;
                    btnExecuted.Enabled = false;
                }

                //-------  read Commands CheckStatus --------------------
                fgCheck.Redraw = false;
                clsOrders_Check Orders_Check = new clsOrders_Check();
                Orders_Check.Command_ID = iRec_ID;
                Orders_Check.GetList();
                foreach (DataRow dtRow in Orders_Check.List.Rows)
                {
                    fgCheck.AddItem(dtRow["DateIns"] + "\t" + dtRow["Surname"] + " " + dtRow["Firstname"] + "\t" +
                                    sCheck[Convert.ToInt32(dtRow["Status"])] + "\t" + dtRow["ProblemType_Title"] + "\t" + dtRow["Notes"] + "\t" + dtRow["FileName"] + "\t" +
                                    dtRow["ReversalRequestDate"] + "\t" + dtRow["ID"] + "\t" + dtRow["User_ID"] + "\t" + dtRow["Status"] + "\t" + "\t" +
                                    dtRow["ProblemType_ID"]);                                           // preLast Column - Empty, it's shows that it "old" file. "New" file has full path of file

                }
                fgCheck.Redraw = true;

                DefineCommandsProvidersData();
                DefineSimpleCommandsList();

                if (iRightsLevel < 2)
                {
                    tslCancel.Enabled = false;
                    tsbSave.Enabled = false;
                }

                txtNotes.Focus();
            }
            else
            {
                this.Width = 936;
                this.Height = 340;
                dAktionDate.Value = DateTime.Now;
                ucCS.Enabled = false;
                lblServiceProvider.Visible = true;
                cmbServiceProvider.Visible = true;
                lstType.SelectedIndex = 0;
                lstType.Enabled = true;
                lblContractTitle.Text = "HellasFin";
                cmbConstant.SelectedIndex = 0;
                txtAction.Enabled = true;
                txtAction.Focus();
            }

            bCheckShare = true;
            btnExecuted.Enabled = true;

            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Refresh();
        }
        #endregion

        #region --- Top toolbar functions -----------------------------------------------------------------------
        private void tslCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(sMessage, Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                clsOrdersSecurity klsOrderSecurity = new clsOrdersSecurity();
                klsOrderSecurity.Record_ID = iRec_ID;                               // iRec_ID - current record's ID
                klsOrderSecurity.FirstOrderDate = Convert.ToDateTime("1900/01/01");
                klsOrderSecurity.GetStartRecord();
                iRec_ID = klsOrderSecurity.Record_ID;                               // iRec_ID - GrandFather's ID
                klsOrderSecurity.GetRecord();
                sBulkCommand = klsOrderSecurity.BulkCommand.Replace("<", "").Replace(">", "").Trim();
                klsOrderSecurity.ExecuteDate = Convert.ToDateTime("1900/01/01");
                klsOrderSecurity.RealPrice = 0;
                klsOrderSecurity.RealQuantity = 0;
                klsOrderSecurity.RealAmount = 0;
                //klsOrderSecurity.SentDate = Convert.ToDateTime("1900/01/01");
                klsOrderSecurity.Status = iStatus;
                klsOrderSecurity.EditRecord();

                if (sBulkCommand != "")
                {
                    clsOrdersSecurity klsOrder2 = new clsOrdersSecurity();
                    klsOrder2.BulkCommand = sBulkCommand;
                    klsOrder2.AktionDate = dAktionDate.Value;
                    klsOrder2.EditBulkCommand();

                    for (i = 2; i <= fgSingleOrders.Rows.Count - 1; i++)
                    {
                        if (Convert.ToInt32(fgSingleOrders[i, "ID"]) != 0)
                        {
                            klsOrder2 = new clsOrdersSecurity();
                            klsOrder2.Record_ID = Convert.ToInt32(fgSingleOrders[i, 11]);
                            klsOrder2.GetRecord();
                            klsOrder2.BulkCommand = "";
                            klsOrder2.ExecuteDate = Convert.ToDateTime("1900/01/01");
                            klsOrder2.RealPrice = 0;
                            klsOrder2.RealQuantity = 0;
                            klsOrder2.RealAmount = 0;
                            klsOrder2.SentDate = Convert.ToDateTime("1900/01/01");
                            klsOrder2.EditRecord();
                        }
                    }
                }
                iLastAktion = 1;                                                  // was saved (cancel)
                this.Close();
            }
        }
        private void tsbCopyID_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(iRec_ID.ToString());
        }
        private void tsbHistory_Click(object sender, EventArgs e)
        {
            frmShowHistory locShowHistory = new frmShowHistory();
            locShowHistory.RecType = 10;                                                     // 10 - OrdersSecurity
            locShowHistory.SrcRec_ID = iRec_ID;
            locShowHistory.ShowDialog();
        }
        private void tsbKey_Click(object sender, EventArgs e)
        {
            tsbSave.Enabled = true;
        }
        private void tsbSave_Click(object sender, EventArgs e)
        {
            int i, j;
            List<ExecCommandClient> Commands_ID = new List<ExecCommandClient>();
            string sTemp = "";
            bContinue = true;

            if (txtPrice.Text == "M") txtPrice.Text = "0";

            if (!Global.IsNumeric(txtPrice.Text) || txtPrice.Text.IndexOf(".") > 0)
            {
                bContinue = false;
                txtPrice.BackColor = Color.Red;
                txtPrice.Focus();
            }
            else
            {
                if (!Global.IsNumeric(txtQuantity.Text) || txtQuantity.Text.IndexOf(".") > 0)
                {
                    bContinue = false;
                    txtQuantity.BackColor = Color.Red;
                    txtQuantity.Focus();
                }
                else
                {
                    if (!Global.IsNumeric(txtAmount.Text) || txtAmount.Text.IndexOf(".") > 0)
                    {
                        bContinue = false;
                        txtAmount.BackColor = Color.Red;
                        txtAmount.Focus();
                    }
                }
            }

            if (bContinue)
            {
                if (bEditKatamerismos)
                    if (MessageBox.Show("Έχετε κάνει αλλαγές στο Καταμερισμό\nΘέλετε να αποθηκευτούν αυτές της αλλαγές;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        bEditKatamerismos = true;
                    else bEditKatamerismos = false;

                if (iRec_ID == 0)
                {
                    clsOrdersSecurity klsOrder2 = new clsOrdersSecurity();
                    iBulcCommand_ID = klsOrder2.GetNextBulkCommand();

                    if (klsOrder.BulkCommand == "") klsOrder.BulkCommand = "<" + iBulcCommand_ID + ">";
                    else klsOrder.BulkCommand = klsOrder.BulkCommand + "/<" + iBulcCommand_ID + ">";

                    klsOrder.BusinessType_ID = 1;
                    klsOrder.CommandType_ID = iCommandType_ID;
                    klsOrder.Client_ID = 0;
                    klsOrder.Company_ID = Global.Company_ID;
                    klsOrder.ServiceProvider_ID = Convert.ToInt32(cmbServiceProvider.SelectedValue);
                    klsOrder.StockExchange_ID = Convert.ToInt32(cmbStockExchanges.SelectedValue);
                    klsOrder.CustodyProvider_ID = 0;
                    klsOrder.Depository_ID = 0;
                    klsOrder.II_ID = 0;
                    klsOrder.Parent_ID = 0;
                    klsOrder.Contract_ID = 0;
                    klsOrder.Code = "";
                    klsOrder.ProfitCenter = "";
                    klsOrder.AllocationPercent = 100;
                    klsOrder.Aktion = txtAction.Text == "BUY" ? 1 : 2;
                    klsOrder.AktionDate = dAktionDate.Value;
                    klsOrder.Share_ID = iShare_ID;
                    klsOrder.Product_ID = iProduct_ID;
                    klsOrder.ProductCategory_ID = iProductCategory_ID;
                    klsOrder.PriceType = lstType.SelectedIndex;
                    klsOrder.Price = (!Global.IsNumeric(txtPrice.Text) ? 0 : Convert.ToDecimal(txtPrice.Text));
                    klsOrder.Quantity = (!Global.IsNumeric(txtQuantity.Text) ? 0 : Convert.ToDecimal(txtQuantity.Text));
                    klsOrder.Amount = (!Global.IsNumeric(txtAmount.Text) ? 0 : Convert.ToDecimal(txtAmount.Text));
                    klsOrder.Curr = lblCurr.Text;
                    klsOrder.CurrRate = Convert.ToDecimal(lblCurrRate.Text);
                    klsOrder.Constant = cmbConstant.SelectedIndex;
                    klsOrder.ConstantDate = cmbConstant.SelectedIndex == 2 ? dConstant.Value.ToString() : "";
                    klsOrder.RecieveMethod_ID = 0;
                    klsOrder.BestExecution = chkBestExecution.Checked ? 1 : 0;
                    klsOrder.RecieveDate = Convert.ToDateTime("1900/01/01");
                    klsOrder.SentDate = Convert.ToDateTime("1900/01/01");
                    klsOrder.SendCheck = cbChecked.Checked ? 1 : 0;
                    klsOrder.FIX_A = -1;
                    klsOrder.FIX_RecievedDate = Convert.ToDateTime("1900/01/01");
                    klsOrder.ExecuteDate = Convert.ToDateTime("1900/01/01");
                    klsOrder.RealPrice = 0;
                    klsOrder.RealQuantity = 0;
                    klsOrder.RealAmount = 0;
                    klsOrder.User_ID = Convert.ToInt32(cmbSenders.SelectedValue);
                    klsOrder.DateIns = DateTime.Now;
                    iRec_ID = klsOrder.InsertRecord();
                }
                else
                {

                    //--- save fgCheck data ----------------------------------------------------------
                    for (i = 1; i <= fgCheck.Rows.Count - 1; i++)
                    {

                        if ((fgCheck[i, 10] + "").Trim() != "")
                        {                                           // FileFullName - Not Empty means that it's a new file
                            sTemp = Global.DMS_UploadFile(fgCheck[i, 10] + "", "Customers/" + sSubPath + "/Informing", fgCheck[i, 5] + "");
                            fgCheck[i, 5] = Path.GetFileName(sTemp);
                        }

                        clsOrders_Check Orders_Check = new clsOrders_Check();
                        Orders_Check.Record_ID = Convert.ToInt32(fgCheck[i, 7]);
                        Orders_Check.Command_ID = iRec_ID;
                        Orders_Check.DateIns = Convert.ToDateTime(fgCheck[i, 0]);
                        Orders_Check.User_ID = Convert.ToInt32(fgCheck[i, 8]);
                        Orders_Check.Status = Convert.ToInt32(fgCheck[i, 9]);
                        Orders_Check.ProblemType_ID = Convert.ToInt32(fgCheck[i, 11]);
                        Orders_Check.Notes = fgCheck[i, 4] + "";
                        Orders_Check.FileName = fgCheck[i, 5] + "";
                        Orders_Check.ReversalRequestDate = fgCheck[i, 6] + "";

                        if ((fgCheck[i, 7] + "") == "0") Orders_Check.InsertRecord();
                        else Orders_Check.EditRecord();
                    }

                    //--- save Commands data ----------------------------------------------------------
                    klsOrder.Record_ID = iRec_ID;
                    klsOrder.GetRecord();
                    klsOrder.Client_ID = klsOrder.Client_ID;
                    klsOrder.Company_ID = Global.Company_ID;
                    klsOrder.ServiceProvider_ID = klsOrder.ServiceProvider_ID;
                    klsOrder.StockExchange_ID = Convert.ToInt32(cmbStockExchanges.SelectedValue);
                    klsOrder.Depository_ID = Convert.ToInt32(cmbDepositories.SelectedValue);
                    klsOrder.Contract_ID = klsOrder.Contract_ID;
                    klsOrder.Code = ucCS.txtContractTitle.Text + "";
                    klsOrder.ProfitCenter = txtPortfolio.Text;
                    klsOrder.Aktion = txtAction.Text == "BUY" ? 1 : 2;
                    klsOrder.AktionDate = dAktionDate.Value;
                    klsOrder.PriceType = lstType.SelectedIndex;
                    klsOrder.Price = Convert.ToDecimal(txtPrice.Text);
                    klsOrder.Quantity = Convert.ToDecimal(txtQuantity.Text);
                    klsOrder.Amount = Convert.ToDecimal(txtAmount.Text);
                    klsOrder.Curr = lblCurr.Text;
                    klsOrder.CurrRate = Convert.ToDecimal(lblCurrRate.Text);
                    klsOrder.Constant = cmbConstant.SelectedIndex;
                    klsOrder.ConstantDate = Convert.ToInt32(cmbConstant.SelectedIndex) == 2 ? dConstant.Value.ToString("dd/MM/yyyy") : "";

                    i = 0;
                    sTemp = "";
                    if (fgCheck.Rows.Count > 1)
                    {
                        i = Convert.ToInt32(fgCheck[1, 9]);                                                        // Status
                        sTemp = fgCheck[1, 5] + "";
                    }
                    klsOrder.Pinakidio = i;
                    klsOrder.FeesNotes = lblFeesNotes.Text;
                    klsOrder.FeesPercent = Convert.ToDecimal(lblFeesPercent.Text);
                    klsOrder.FeesAmount = Convert.ToDecimal(lblFeesAmount.Text);
                    klsOrder.FeesDiscountPercent = Convert.ToDecimal(lblFeesDiscountPercent.Text);
                    klsOrder.FeesDiscountAmount = Convert.ToDecimal(lblFeesDiscountAmount.Text);
                    klsOrder.FinishFeesPercent = Convert.ToDecimal(lblFinishFeesPercent.Text);
                    klsOrder.FinishFeesAmount = Convert.ToDecimal(lblFinishFeesAmount.Text);
                    klsOrder.TicketFee = 0;
                    klsOrder.TicketFeeDiscountPercent = 0;
                    klsOrder.TicketFeeDiscountAmount = 0;
                    klsOrder.FinishTicketFee = 0;
                    klsOrder.CompanyFeesPercent = 0;
                    klsOrder.RecieveMethod_ID = 0;
                    klsOrder.BestExecution = chkBestExecution.Checked ? 1 : 0;
                    if (dSend.Text.Trim() != "")
                    {
                        dTemp = Convert.ToDateTime(dSend.Text);
                        sTemp = dTemp.ToString("d") + " " + txtSendHour.Text + ":" + txtSendMinute.Text + ":" + txtSendSecond.Text;
                    }
                    else sTemp = "1900/01/01 00:00:00";
                    klsOrder.SentDate = Convert.ToDateTime(sTemp);
                    klsOrder.SendCheck = cbChecked.Checked ? 1 : 0;
                    if (fgExecutions.Rows.Count > 1) dTemp = Convert.ToDateTime(fgExecutions[1, 0]);
                    else dTemp = Convert.ToDateTime("1900/01/01 00:00:00");
                    klsOrder.ExecuteDate = dTemp;

                    if (Global.IsNumeric(lblSumPrice.Text) && Convert.ToDecimal(lblSumPrice.Text) != 0)
                        klsOrder.RealPrice = Convert.ToDecimal(lblSumPrice.Text);
                    else klsOrder.RealPrice = decRealPrice;
                    klsOrder.RealQuantity = decRealQuantity;
                    klsOrder.RealAmount = decRealAmount;
                    klsOrder.ProviderFees = Convert.ToDecimal(lblProviderFees.Text);
                    klsOrder.AccruedInterest = Convert.ToDecimal(txtAccruedInterest.Text);
                    klsOrder.FeesDiff = Convert.ToDecimal(txtFeesDiff.Text);
                    klsOrder.FeesMarket = Convert.ToDecimal(txtFeesMarket.Text);

                    klsOrder.InformationMethod_ID = 0;
                    klsOrder.Notes = txtNotes.Text;
                    klsOrder.FeesCalcMode = iFeesCalcMode;
                    klsOrder.User_ID = Convert.ToInt32(cmbSenders.SelectedValue);
                    klsOrder.EditRecord();

                    //--- save fgExecutions data -----------------------------------------------
                    clsOrders_Executions Orders_Executions = new clsOrders_Executions();
                    Orders_Executions = new clsOrders_Executions();
                    for (i = 1; i <= fgExecutions.Rows.Count - 1; i++)
                    {
                        Orders_Executions.Record_ID = Convert.ToInt32(fgExecutions[i, 6]);
                        Orders_Executions.Command_ID = iRec_ID;
                        Orders_Executions.DateExecution = Convert.ToDateTime(fgExecutions[i, 0]);
                        Orders_Executions.StockExchange_MIC = fgExecutions[i, 5] + "";
                        Orders_Executions.ProviderCommandNumber = fgExecutions[i, 1] + "";
                        Orders_Executions.RealPrice = Convert.ToDecimal(fgExecutions[i, 2]);
                        Orders_Executions.RealQuantity = Convert.ToDecimal(fgExecutions[i, 3]);
                        Orders_Executions.RealAmount = Convert.ToDecimal(fgExecutions[i, 4]);
                        Orders_Executions.AccruedInterest = 0;
                        if ((fgExecutions[i, 6] + "") == "0") Orders_Executions.InsertRecord();
                        else Orders_Executions.EditRecord();
                    }

                    //--- save records into Commands_ExecutionsDetails table (only for non-FIX providers  ----------------------------------------
                    if ((iServiceProvider_ID == 17 || iServiceProvider_ID == 19))                          // 17 - PIRAEUS SECURITIES, 19 - INTESA
                    {
                        i = 0;
                        Commands_ID = new List<ExecCommandClient>();

                        for (j = 2; j <= fgSingleOrders.Rows.Count - 1; j++)
                        {
                            switch (Convert.ToInt32(fgSingleOrders[j, "CommandType_ID"]))
                            {
                                case 1:
                                    Commands_ID.Insert(i, new ExecCommandClient
                                    {
                                        Command_ID = Convert.ToInt32(fgSingleOrders[j, "ID"]),
                                        Quantity = Convert.ToDecimal(fgSingleOrders[j, "Quantity"]),
                                        Koef = Convert.ToDecimal(fgSingleOrders[j, "Quantity"]) / Convert.ToDecimal(lblQuantity_Sum.Text)
                                    });
                                    i = i + 1;
                                    break;
                                case 3:
                                    break;
                                case 4:
                                    iBulcCommand2_ID = 0;
                                    Orders3 = new clsOrdersSecurity();
                                    Orders3.Record_ID = Convert.ToInt32(fgSingleOrders[j, "ID"]);
                                    Orders3.GetRecord();
                                    sTemp = Orders3.BulkCommand;
                                    if (sTemp.Length > 0)
                                    {
                                        string[] tokens = sTemp.Replace("<", "").Replace(">", "").Split('/');
                                        if (tokens.Length > 0)
                                        {
                                            iBulcCommand_ID = Convert.ToInt32(tokens[0]);
                                            if (tokens.Length > 1) iBulcCommand2_ID = Convert.ToInt32(tokens[1]);
                                        }
                                    }

                                    if (iBulcCommand2_ID != 0)
                                    {
                                        Orders3 = new clsOrdersSecurity();
                                        Orders3.AktionDate = dAktionDate.Value;
                                        Orders3.BulkCommand = iBulcCommand2_ID + "";
                                        Orders3.GetList_BulkCommand();
                                        foreach (DataRow dtRow in Orders3.List.Rows)
                                        {
                                            Commands_ID.Insert(i, new ExecCommandClient
                                            {
                                                Command_ID = Convert.ToInt32(dtRow["ID"]),
                                                Quantity = Convert.ToDecimal(dtRow["Quantity"]),
                                                Koef = Convert.ToDecimal(dtRow["Quantity"]) / Convert.ToDecimal(lblQuantity_Sum.Text)
                                            });
                                            i = i + 1;
                                        }
                                    }
                                    break;
                            }
                        }

                        for (i = 1; i <= fgExecutions.Rows.Count - 1; i++)
                        {
                            for (j = 0; j <= Commands_ID.Count - 1; j++)
                            {
                                CommandsExecutionsDetails = new clsCommandsExecutionsDetails();
                                CommandsExecutionsDetails.Command_ID = Commands_ID[j].Command_ID;
                                CommandsExecutionsDetails.DeleteRecord_Command_ID();

                                CommandsExecutionsDetails = new clsCommandsExecutionsDetails();
                                CommandsExecutionsDetails.Command_ID = Commands_ID[j].Command_ID;
                                CommandsExecutionsDetails.CommandExecution_ID = 0;
                                CommandsExecutionsDetails.CurrentTimestamp = Convert.ToDateTime(fgExecutions[i, "DateAktion"]);
                                CommandsExecutionsDetails.SecondOrdID = fgExecutions[i, "RefNumber"] + "";
                                CommandsExecutionsDetails.StockExchange_ID = Convert.ToInt32(fgExecutions[i, "StockExchange_ID"]);
                                CommandsExecutionsDetails.StockCompany_ID = iServiceProvider_ID; //ZZZ
                                CommandsExecutionsDetails.Price = Convert.ToDecimal(fgExecutions[i, "Price"]);
                                CommandsExecutionsDetails.Quantity = (Convert.ToDecimal(fgExecutions[i, "Quantity"]) * Commands_ID[j].Koef);
                                CommandsExecutionsDetails.InsertRecord();
                            }
                        }
                    }
                }

                DefineComission();

                Orders3 = new clsOrdersSecurity();
                Orders3.AktionDate = dAktionDate.Value;
                Orders3.BulkCommand = klsOrder.BulkCommand.Replace("<", "").Replace(">", "");
                Orders3.GetList_BulkCommand();
                foreach (DataRow dtRow in Orders3.List.Rows)
                {
                    if (Convert.ToInt32(dtRow["CommandType_ID"]) == 1)
                    {
                        Global.SyncExec_SingleOrder(iRec_ID, Convert.ToInt32(dtRow["ID"]), Convert.ToDecimal(dtRow["RealPrice"]), Convert.ToDecimal(dtRow["RealQuantity"]), bEditKatamerismos);
                    }
                    else
                    {
                        Global.SyncExec_DPM(iRec_ID, Convert.ToInt32(dtRow["ID"]), Convert.ToDecimal(dtRow["RealPrice"]), Convert.ToDecimal(dtRow["RealQuantity"]));
                        if (Convert.ToSingle(dtRow["AllocationPercent"]) < 100)
                            Global.SyncDPM_SingleOrder(Convert.ToInt32(dtRow["ID"]), Convert.ToDecimal(dtRow["RealPrice"]), Convert.ToDecimal(dtRow["RealQuantity"]));
                    }
                }

                this.Close();
                iLastAktion = 1;             // was saved (added)
            }
        }
        #endregion
        #region --- Edit functions -----------------------------------------------------------------------
        private void txtAction_TextChanged(object sender, EventArgs e)
        {
            switch (txtAction.Text.Substring(0, 1))
            {
                case "B":
                case "b":
                case "Β":
                case "β":
                case "A":
                case "a":
                case "Α":
                case "α":
                    txtAction.Text = "BUY";
                    ucPS.txtShareTitle.Focus();
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
                    ucPS.txtShareTitle.Focus();
                    break;
                default:
                    txtAction.Text = "";
                    txtAction.Focus();
                    break;
            }
        }
        private void lstType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Convert.ToInt32(lstType.SelectedIndex))
            {
                case 0:
                    txtPrice.Enabled = true;
                    break;
                case 1:
                    txtPrice.Text = "0";
                    txtPrice.Enabled = false;
                    break;
                case 2:
                    txtPrice.Enabled = true;
                    break;
                case 3:
                    txtPrice.Enabled = true;
                    break;
                case 4:
                    txtPrice.Text = "0";
                    txtPrice.Enabled = false;
                    break;
                case 5:
                    txtPrice.Text = "0";
                    txtPrice.Enabled = false;
                    break;
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
        private void picCopy2Clipboard_Click(object sender, EventArgs e)
        {
            if (!Convert.IsDBNull(Clipboard.GetText())) Clipboard.SetText(lnkISIN.Text + "");
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
        }
        private void picEmptyProduct_Click(object sender, EventArgs e)
        {
            klsOrder.Share_ID = 0;
            ucPS.ShowProductsList = false;
            ucPS.txtShareTitle.Text = "";
            ucPS.ShowProductsList = true;
            lnkISIN.Text = "";
            lblShareTitle.Text = "";
            lblProduct.Text = "";
            lblProductCategory.Text = "";
            lblProductStockExchange_Title.Text = "";
            lblCurr.Text = "";
        }
        private void btnExecuted_Click(object sender, EventArgs e)
        {
            bContinue = true;
            for (i = 2; i <= fgSingleOrders.Rows.Count - 1; i++)
                if (Convert.ToInt32(fgSingleOrders[i, "CommandType_ID"]) == 4 && Convert.ToSingle(fgSingleOrders[i, "Allocation"]) != 100) bContinue = false;

            if (!bContinue)
                if (MessageBox.Show("Θέλετε να προχωρήσετε χωρίς Allocation;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes) bContinue = true;

            if (bContinue)
            {
                dExecute = DateTime.Now;

                if (lblCurr.Text != "EUR")
                {
                    clsProductsCodes ProductCode = new clsProductsCodes();
                    ProductCode.DateIns = dAktionDate.Value;
                    ProductCode.Code = "EUR" + lblCurr.Text + "=";
                    ProductCode.GetPrice_Code();
                    lblCurrRate.Text = ProductCode.LastClosePrice.ToString("0.####");
                }
                else lblCurrRate.Text = "1";

                btnExecuted.Enabled = false;
                toolExecutions.Enabled = true;

                if (Global.IsNumeric(txtPrice.Text) && Global.IsNumeric(txtQuantity.Text))
                {
                    decTemp = Convert.ToDecimal(txtPrice.Text);
                    decTemp2 = decTemp * Convert.ToDecimal(txtQuantity.Text);
                    if (Convert.ToInt32(klsOrder.Product_ID) == 2) decTemp2 = decTemp2 / 100;

                    fgExecutions.Rows.Count = 1;
                    fgExecutions.AddItem(dExecute.ToString("dd/MM/yyyy HH:mm:ss") + "\t" + "" + "\t" + decTemp + "\t" + txtQuantity.Text + "\t" +
                                         decTemp2.ToString("0.00##") + "\t" + "" + "\t" + "0");
                }
                else
                {
                    fgExecutions.Rows.Count = 1;
                    fgExecutions.AddItem(dExecute.ToString("dd/MM/yyyy HH:mm:ss") + "\t" + "" + "\t" + txtPrice.Text + "\t" + txtQuantity.Text + "\t" +
                                        "0" + "\t" + "" + "\t" + "0");
                }
            }
        }
        private void picShowPackage_Click(object sender, EventArgs e)
        {
            frmContract locContract = new frmContract();
            locContract.Aktion = 1;
            locContract.Contract_ID = klsOrder.Contract_ID;
            locContract.Contract_Details_ID = klsOrder.Contract_Details_ID;
            locContract.Contract_Packages_ID = klsOrder.Contract_Packages_ID;
            locContract.Client_ID = klsOrder.Client_ID;
            locContract.ClientType = iClientTipos;
            locContract.ClientFullName = klsOrder.ClientName;
            locContract.RightsLevel = 0;
            locContract.Show();
        }
        private void tsbAddCheck_Click(object sender, EventArgs e)
        {
            fgCheck.AddItem(DateTime.Now.ToString("dd/MM/yyyy") + "\t" + Global.UserName + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" +
                            "\t" + "" + "\t" + "0" + "\t" + Global.User_ID + "\t" + "0" + "\t" + "" + "\t" + "0", 1);
        }
        private void tsbDelCheck_Click(object sender, EventArgs e)
        {
            if (fgCheck.Row > 0)
            {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    clsOrders_Check Orders_Check = new clsOrders_Check();
                    Orders_Check.Record_ID = Convert.ToInt32(fgCheck[fgCheck.Row, 7]);
                    Orders_Check.DeleteRecord();

                    fgCheck.RemoveItem(fgCheck.Row);
                }
            }
        }
        private void tsbViewCheck_Click(object sender, EventArgs e)
        {
            if ((fgCheck[fgCheck.Row, 5] + "") != "")
            {

                if ((fgCheck[fgCheck.Row, "FileFullName"] + "") != "")
                    System.Diagnostics.Process.Start((fgCheck[fgCheck.Row, "FileFullName"] + ""));                                            // isn't DMS file, so show it into Windows mode
                else
                    Global.DMS_ShowFile("Customers/" + sSubPath + "/Informing", (fgCheck[fgCheck.Row, 5] + ""));           // is DMS file, so show it into Web mode
            }
        }
        private void fgCheck_CellButtonClick(object sender, RowColEventArgs e)
        {
            if (e.Col == 5)
            {                                                                                                                  // 5 - File Name
                fgCheck[fgCheck.Row, "FileFullName"] = Global.FileChoice(Global.DefaultFolder);
                fgCheck[fgCheck.Row, 5] = Path.GetFileName(fgCheck[fgCheck.Row, "FileFullName"] + "");
            }
        }

        private void fgCheck_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Col == 2) fgCheck[e.Row, "Status"] = fgCheck[e.Row, "Status_Title"];                              // 2 - Status
            if (e.Col == 3) fgCheck[e.Row, "ProblemType_ID"] = fgCheck[e.Row, "Problem_Type"];                      // 3 - Problem_Type
        }
        private void fgCheck_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (bCheckList)
            {
                if (e.Col == 0 || e.Col == 1) e.Cancel = false;
                else e.Cancel = true;
            }
        }  
        private void txtNotes_LostFocus(object sender, EventArgs e)
        {
            txtNotes.Text = txtNotes.Text.Replace("\t", "");
        }
        private void lnkISIN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmProductData locProductData = new frmProductData();
            locProductData.ShareCode_ID = iShare_ID;
            locProductData.Product_ID = iProduct_ID;
            locProductData.Text = Global.GetLabel("product");
            locProductData.Show();
        }
        private void fgExecutions_CellChanged(object sender, RowColEventArgs e)
        {
            if (bCheckShare)
            {
                decTemp = 0;

                if (e.Row > 0)
                {
                    switch (e.Col)
                    {
                        case 1:
                            sTemp = fgExecutions[e.Row, 1] + "";
                            fgExecutions[e.Row, 1] = System.Text.RegularExpressions.Regex.Replace(sTemp, "[^\\w\\-]", "");
                            break;
                        case 2:
                            if ((fgExecutions[e.Row, 2] + "" != "") && (fgExecutions[e.Row, 3] + "" != ""))
                                decTemp = Convert.ToDecimal(fgExecutions[e.Row, 2]) * Convert.ToDecimal(fgExecutions[e.Row, 3]) / Convert.ToDecimal(decKoef);
                            fgExecutions[e.Row, 4] = decTemp.ToString("0.0000");
                            break;
                        case 3:
                            if ((fgExecutions[e.Row, 2] + "" != "") && (fgExecutions[e.Row, 3] + "" != ""))
                                decTemp = Convert.ToDecimal(fgExecutions[e.Row, 2]) * Convert.ToDecimal(fgExecutions[e.Row, 3]) / Convert.ToDecimal(decKoef);
                            fgExecutions[e.Row, 4] = decTemp.ToString("0.0000");

                            decRealQuantity = 0;
                            for (i = 1; i <= fgExecutions.Rows.Count - 1; i++)
                                decRealQuantity = decRealQuantity + Convert.ToDecimal(fgExecutions[i, 3]);

                            lblSumQuantity.Text = string.Format("{0:#0.00##}", decRealQuantity);
                            break;
                        case 4:
                            decRealAmount = 0;
                            for (i = 1; i <= fgExecutions.Rows.Count - 1; i++)
                                decRealAmount = decRealAmount + Convert.ToDecimal(fgExecutions[i, 4]);

                            lblSumAmount.Text = decRealAmount.ToString("0.0000");
                            decInvestAmount = decRealAmount;
                            break;
                        case 5:
                            iStockExchange_ID = 0;
                            foundRows = Global.dtStockExchanges.Select("Code = '" + fgExecutions[e.Row, 5] + "'");
                            if (foundRows.Length > 0) iStockExchange_ID = Convert.ToInt32(foundRows[0]["ID"]);
                            fgExecutions[e.Row, 7] = iStockExchange_ID;
                            break;
                    }

                    if ((lblSumAmount.Text != "") && (lblSumQuantity.Text != ""))
                    {
                        if (fgExecutions.Rows.Count == 2)
                        {
                            lblSumPrice.Text = fgExecutions[1, 2] + "";
                            lblSumQuantity.Text = fgExecutions[1, 3] + "";
                            lblSumAmount.Text = fgExecutions[1, 4] + "";
                        }
                        else
                        {
                            if (lblSumQuantity.Text != "")
                            {
                                if (Convert.ToDecimal(lblSumQuantity.Text) != 0)
                                    lblSumPrice.Text = (Convert.ToDecimal(lblSumAmount.Text) / Convert.ToDecimal(lblSumQuantity.Text)).ToString("0.0000");
                            }
                            else lblSumPrice.Text = "0";
                        }
                    }

                    if (lblSumPrice.Text != "") decRealPrice = Convert.ToDecimal(lblSumPrice.Text);
                    if (lblSumQuantity.Text != "") decRealQuantity = Convert.ToDecimal(lblSumQuantity.Text);
                    if (lblSumAmount.Text != "") decRealAmount = Convert.ToDecimal(lblSumAmount.Text);
                }
            }
        }
        private void txtPrice_LostFocus(object sender, EventArgs e)
        {
            if (!Global.IsNumeric(txtPrice.Text) || txtPrice.Text.IndexOf(".") > 0)
            {
                txtPrice.Focus();
            }
            else
            {
                txtPrice.BackColor = Color.White;
                if (klsOrder.Product_ID == 2)
                {
                    if (!Global.IsNumeric(txtPrice.Text) && Global.IsNumeric(txtQuantity.Text))
                        txtAmount.Text = string.Format("{0:#0.##}", (Convert.ToDecimal(txtPrice.Text) * Convert.ToDecimal(txtQuantity.Text) / Convert.ToDecimal(100.0)));
                }
                else
                {
                    if (Global.IsNumeric(txtPrice.Text) && Global.IsNumeric(txtQuantity.Text))
                        txtAmount.Text = string.Format("{0:#0.##}", (Convert.ToDecimal(txtPrice.Text) * Convert.ToDecimal(txtQuantity.Text)));
                }
            }
        }
        private void txtQuantity_LostFocus(object sender, EventArgs e)
        {
            if (lstType.SelectedIndex != 1)
            {                                                   // != 1 - isn't Market
                if (!Global.IsNumeric(txtQuantity.Text) || txtQuantity.Text.IndexOf(".") > 0)
                {
                    txtQuantity.BackColor = Color.Red;
                    txtQuantity.Focus();
                }
                else
                {
                    txtQuantity.BackColor = Color.White;

                    if (klsOrder.Product_ID == 2) txtAmount.Text = string.Format("{0:#0.##}", (Convert.ToDecimal(txtPrice.Text) * Convert.ToDecimal(txtQuantity.Text) / Convert.ToDecimal(100.0)));
                    else txtAmount.Text = string.Format("{0:#0.##}", (Convert.ToDecimal(txtPrice.Text) * Convert.ToDecimal(txtQuantity.Text)));
                }
            }
        }
        private void txtAmount_LostFocus(object sender, EventArgs e)
        {
            if (!Global.IsNumeric(txtAmount.Text) || txtAmount.Text.IndexOf(".") > 0)
            {
                txtAmount.BackColor = Color.Red;
                txtAmount.Focus();
            }
            else txtAmount.BackColor = Color.White;
        }
        private void lnkInvestProposals_Click(object sender, EventArgs e)
        {
            frmInvestProposal locInvestProposal = new frmInvestProposal();
            locInvestProposal.Aktion = 1;                             // 0 - Edit 
            locInvestProposal.II_ID = klsOrder.II_ID;
            locInvestProposal.ShowDialog();
        }
        private void picCompare_Click(object sender, EventArgs e)
        {
            DefineCommandsProvidersData();
        }

        private void DefineCommandsProvidersData()
        {
            fgProviderData.Redraw = false;
            fgProviderData.Rows.Count = 1;

            clsOrders_ProvidersData Orders_ProvidersData = new clsOrders_ProvidersData();
            Orders_ProvidersData.Command_ID = iRec_ID;
            Orders_ProvidersData.GetList();
            foreach (DataRow dtRow in Orders_ProvidersData.List.Rows)
            {
                if (iDepository_ID == 0) iDepository_ID = Convert.ToInt32(dtRow["Depository_ID"]);

                fgProviderData.AddItem(dtRow["TradeDate"] + "\t" + dtRow["TradeTime"] + "\t" + dtRow["SettlementDate"] + "\t" + dtRow["TradeCurrency"] + "\t" +
                     dtRow["SecurityCode"] + "\t" + dtRow["SecurityDescription"] + "\t" +
                     dtRow["StockExchanges_Title"] + "\t" + dtRow["Sign"] + "\t" + dtRow["QuantityNominal"] + "\t" +
                     dtRow["Price"] + "\t" + dtRow["AccruedInterest"] + "\t" + dtRow["Commission"] + "\t" +
                     dtRow["Fees"] + "\t" + dtRow["SettlementAmount"] + "\t" + dtRow["ExchangeRate"] + "\t" +
                     dtRow["SettlementAmountCurr"] + "\t" + dtRow["Depositories_Title"] + "\t" + dtRow["RefNumber"] + "\t" +
                     dtRow["ID"] + "\t" + dtRow["StockExchange_ID"] + "\t" + dtRow["Depository_ID"]);
            }
            fgProviderData.Redraw = true;


            clsServiceProviderSettlementsFees ServiceProviderSettlementsFees = new clsServiceProviderSettlementsFees();
            ServiceProviderSettlementsFees.ServiceProvider_ID = iCustody_ID;
            ServiceProviderSettlementsFees.Product_ID = iProduct_ID;
            ServiceProviderSettlementsFees.ProductCategory_ID = iProductCategory_ID;
            ServiceProviderSettlementsFees.Quantity = Convert.ToSingle(lblSumAmount.Text);
            ServiceProviderSettlementsFees.Depositories_ID = Convert.ToInt32(cmbDepositories.SelectedValue);
            ServiceProviderSettlementsFees.GetRecord_Fees();
            txtSettlmentFees.Text = string.Format("{0:#0.00##}", ServiceProviderSettlementsFees.SettlmentFees);
            lblSettlmentCurr.Text = ServiceProviderSettlementsFees.SettlmentCurr;
        }

        private void DefineComission()
        {
            if (fgExecutions.Rows.Count > 1)
            {

                klsOrder.ExecuteDate = Convert.ToDateTime(fgExecutions[1, 0]);
                klsOrder.RealQuantity = Convert.ToDecimal(lblSumQuantity.Text);
                klsOrder.RealAmount = Convert.ToDecimal(lblSumAmount.Text);
                klsOrder.CalcFees();

                //--- Fees Line --------------
                lblFeesPercent.Text = klsOrder.FeesPercent.ToString("0.00##");
                lblFeesAmount.Text = klsOrder.FeesAmount.ToString("0.0000");

                lblFeesDiscountPercent.Text = klsOrder.FeesDiscountPercent.ToString("0.00##");
                lblFeesDiscountAmount.Text = klsOrder.FeesDiscountAmount.ToString("0.0000");

                lblFinishFeesPercent.Text = klsOrder.FinishFeesPercent.ToString("0.00##");
                lblFinishFeesAmount.Text = klsOrder.FinishFeesAmount.ToString("0.0000");

                lblMinFees.Text = klsOrder.MinFeesAmount.ToString("0.00##");
                lblMinFeesCurr.Text = klsOrder.MinFeesCurr + "";

                lblProviderFees.Text = klsOrder.ProviderFees.ToString("0.00##");
                txtAccruedInterest.Text = klsOrder.AccruedInterest.ToString("0.00##");
                txtFeesDiff.Text = klsOrder.FeesDiff.ToString("0.00##");
                txtFeesMarket.Text = klsOrder.FeesMarket.ToString("0.00##");

                //--- Fees Notes Line --------------
                lblFeesNotes.Text = klsOrder.FeesNotes;
            }
        }
        #endregion
        private void DefineSimpleCommandsList()
        {
            int i, n;
            string sTemp, sTemp1;

            if (klsOrder.BulkCommand != "")
            {
                decTemp = 0;
                decTemp2 = 0;
                i = 0;

                sTemp1 = klsOrder.BulkCommand + "";
                n = sTemp1.IndexOf("/");
                if (n >= 0) sTemp = sTemp1.Substring(n + 1).Replace("<", "").Replace(">", "");
                else sTemp = sTemp1.Replace("<", "").Replace(">", "");

                fgSingleOrders.Redraw = false;
                fgSingleOrders.Rows.Count = 2;

                clsOrdersSecurity Orders3 = new clsOrdersSecurity();
                Orders3.AktionDate = dAktionDate.Value;
                Orders3.BulkCommand = sTemp;
                Orders3.GetList_BulkCommand();
                foreach (DataRow dtRow in Orders3.List.Rows)
                {
                    sTemp = "";
                    if (Convert.ToDateTime(dtRow["ExecuteDate"]) != Convert.ToDateTime("1900/01/01")) sTemp = Convert.ToDateTime(dtRow["ExecuteDate"]).ToString("dd/MM/yyyy");
                    i = i + 1;
                    fgSingleOrders.AddItem(i + "\t" + dtRow["ClientFullName"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" + sTemp + "\t" +
                                              string.Format("{0:#0.00##}", dtRow["Price"]) + "\t" + string.Format("{0:#0.0######}", dtRow["Quantity"]) + "\t" +
                                              string.Format("{0:#0.00}", dtRow["Amount"]) + "\t" + string.Format("{0:#0.00##}", dtRow["RealPrice"]) + "\t" +
                                              string.Format("{0:#0.0######}", dtRow["RealQuantity"]) + "\t" + string.Format("{0:#0.00}", dtRow["RealAmount"]) + "\t" +
                                              dtRow["ID"] + "\t" + dtRow["CommandType_ID"] + "\t" + dtRow["AllocationPercent"]);
                    decTemp = decTemp + Convert.ToDecimal(dtRow["Quantity"]);
                    decTemp2 = decTemp2 + Convert.ToDecimal(dtRow["RealQuantity"]);

                }

                fgSingleOrders.Redraw = true;
                DefineSums();
            }
        }
        private void DefineSums()
        {
            decimal decTemp = 0, decTemp1 = 0, decTemp2 = 0, decTemp3 = 0;
            for (i = 2; i <= fgSingleOrders.Rows.Count - 1; i++)
            {
                decTemp = decTemp + Convert.ToDecimal(fgSingleOrders[i, "Quantity"]);
                decTemp1 = decTemp1 + Convert.ToDecimal(fgSingleOrders[i, "Amount"]);
                decTemp2 = decTemp2 + Convert.ToDecimal(fgSingleOrders[i, "RealQuantity"]);
                decTemp3 = decTemp3 + Convert.ToDecimal(fgSingleOrders[i, "RealAmount"]);
            }
            lblQuantity_Sum.Text = string.Format("{0:#0.00##}", decTemp);
            lblAmount_Sum.Text = string.Format("{0:#0.00##}", decTemp1);
            lblRealQuantity_Sum.Text = string.Format("{0:#0.00##}", decTemp2);
            lblRealAmount_Sum.Text = string.Format("{0:#0.00##}", decTemp3);
        }
        private void tsbAddExecutions_Click(object sender, EventArgs e)
        {
            fgExecutions.AddItem(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "\t" + "" + "\t" + "0" + "\t" + "0" + "\t" + "0" + "\t" +
                                 lblProductStockExchange_Title.Text + "\t" + "0");
        }
        private void tsbDelExecutions_Click(object sender, EventArgs e)
        {
            if (fgExecutions.Row > 0)
            {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    i = Convert.ToInt32(fgExecutions[fgExecutions.Row, 6]);

                    if (i != 0)
                    {
                        clsOrders_Executions Orders_Executions = new clsOrders_Executions();
                        Orders_Executions.Record_ID = i;
                        Orders_Executions.DeleteRecord();
                    }

                    fgExecutions.RemoveItem(fgExecutions.Row);

                    decRealQuantity = 0;
                    decRealAmount = 0;
                    for (i = 1; i <= fgExecutions.Rows.Count - 1; i++)
                    {
                        decRealQuantity = decRealQuantity + Convert.ToDecimal(fgExecutions[i, 3]);
                        decRealAmount = decRealAmount + Convert.ToDecimal(fgExecutions[i, 4]);
                    }
                    lblSumQuantity.Text = decRealQuantity.ToString("0.00");
                    lblSumAmount.Text = decRealAmount.ToString("0.00");
                    decInvestAmount = decRealAmount;

                }
            }
        }
        private void cmbStockExchanges_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckShare)
            {
                //--- check if this StockExchange may be choiced ----
                bFound = false;

                clsServiceProviderBrokerageFees ServiceProviderBrokerageFees = new clsServiceProviderBrokerageFees();
                ServiceProviderBrokerageFees.ServiceProvider_ID = iServiceProvider_ID;
                ServiceProviderBrokerageFees.GetList();
                foreach (DataRow dtRow in ServiceProviderBrokerageFees.List.Rows)
                {
                    if ((Convert.ToInt32(dtRow["Product_ID"]) == iProduct_ID) &&
                        (Convert.ToInt32(dtRow["ProductCategory_ID"]) == iProductCategory_ID) &&
                        (Convert.ToInt32(dtRow["StockExchange_ID"]) == Convert.ToInt32(cmbStockExchanges.SelectedValue)))
                        bFound = true;
                }
                if (!bFound)
                {
                    MessageBox.Show("Δεν μπορεί να επιλεγεί το χρηματιστήριο " + cmbStockExchanges.Text, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cmbStockExchanges.SelectedValue = klsOrder.StockExchange_ID;
                }
            }
        }

        private void tsbAddSimpleCommand_Click(object sender, EventArgs e)
        {
            if (iBulcCommand_ID == 0)
            {

                clsOrdersSecurity klsOrder2 = new clsOrdersSecurity();
                iBulcCommand_ID = klsOrder2.GetNextBulkCommand();

                if (klsOrder.BulkCommand == "") klsOrder.BulkCommand = "<" + iBulcCommand_ID + ">";
                else klsOrder.BulkCommand = klsOrder.BulkCommand + "/<" + iBulcCommand_ID + ">";
                klsOrder.EditRecord();
            }

            frmOrderSecurity locOrderSecurity = new frmOrderSecurity();
            locOrderSecurity.Rec_ID = 0;
            locOrderSecurity.BusinessType = 1;
            locOrderSecurity.Editable = 1;
            locOrderSecurity.NewBulkCommand_ID = iBulcCommand_ID;
            locOrderSecurity.NewAktion = txtAction.Text;
            locOrderSecurity.NewShare_ID = iShare_ID;
            locOrderSecurity.NewPriceType = Convert.ToInt32(lstType.SelectedIndex);
            locOrderSecurity.NewPrice = txtPrice.Text;
            locOrderSecurity.NewConstant = cmbConstant.SelectedIndex;
            locOrderSecurity.NewConstantDate = dConstant.Value;
            locOrderSecurity.ShowDialog();
            if (locOrderSecurity.LastAktion == 1)
            {
                DefineSimpleCommandsList();                            //Aktion=1        was saved (added)
                bEditKatamerismos = true;
            }
        }
        private void tsbEditSimpleCommand_Click(object sender, EventArgs e)
        {
            EditSimpleCommand();
        }
        private void fgSimpleCommands_DoubleClick(object sender, EventArgs e)
        {
            EditSimpleCommand();
        }
        private void tsbDelSimpleCommand_Click(object sender, EventArgs e)
        {
            if (fgSingleOrders.Row > 0)
            {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    i = Convert.ToInt32(fgSingleOrders[fgSingleOrders.Row, "ID"]);

                    if (i != 0)
                    {
                        clsOrders_Executions Orders_Executions = new clsOrders_Executions();
                        Orders_Executions.Record_ID = i;
                        Orders_Executions.DeleteRecord();

                        fgSingleOrders.RemoveItem(fgSingleOrders.Row);
                        DefineSums();
                        bEditKatamerismos = true;
                    }
                }
            }
        }
        private void tsbEMail_Click(object sender, EventArgs e)
        {
            sTemp = "";
            for (i = 1; i <= fgExecutions.Rows.Count - 1; i++)
            {
                if (sTemp.Length == 0) sTemp = fgExecutions[i, 1] + "";
                else sTemp = sTemp + ", " + fgExecutions[i, 1];
            }
            txtThema.Text = "Allocation " + lblShareTitle.Text + " " + lnkISIN.Text + " " + sTemp;
            txtEMail.Text = "";
            sTemp = "";
            if (txtAction.Text == "SELL") sTemp = "SOLD";
            else if (txtAction.Text == "BUY") sTemp = "BOUGHT";

            txtBody.Text = "Dear all, " + "\t" + "Please allocate as follows: " + "\t" + "Order type: " + sTemp + "\t" +
                       "Product / ISIN : " + lblShareTitle.Text + " " + lnkISIN.Text + "\t" +
                       "Nominal: " + txtQuantity.Text + "\t" + "Execution Price: " + lblSumPrice.Text;
            panEMail.Visible = true;
        }
        private void btnSendMail_Click(object sender, EventArgs e)
        {

            sTemp = txtBody.Text.Replace("\t", "<br/>") + "\t" + "<br/><br/><table width='600' border='1'><tr><td>N</td><td>CIF</td><td>Subacc</td><td>nominal</td></tr>";
            for (i = 2; i <= fgSingleOrders.Rows.Count - 1; i++)
                sTemp = sTemp + "<tr><td>" + (i - 1) + "</td><td>" + fgSingleOrders[i, 2] + "</td><td>" + fgSingleOrders[i, 3] + "</td><td>" + fgSingleOrders[i, 9] + "</td></tr>";

            sTemp = sTemp + "</table><br/><br/><br/>";
            sTemp = sTemp + Global.UserName + "<br/><br/>" +
                            "<strong>HellasFin</strong><br/>" +
                            "<strong>Global Wealth Management</strong><br/><br/>" +
                            "90, 26th Oktovriou Str. Office 507<br/>" +
                            "P.C.546 27, Thessaloniki, Greece<br/>" +
                            "T. +30 2310 517800<br/>" +
                            "F. +30 2310 515053<br/>" +
                            "E. " + Global.UserEMail + "<br/>" +
                            "W.www.hellasfin.gr</p>";

            Global.AddInformingRecord(0, 0, 5, 1, 0, 0, txtEMail.Text, "rto@hellasfin.gr", txtThema.Text, sTemp, "", "", "", 0, 0, "");                       // 5 - e-mail
            panEMail.Visible = false;
        }

        private void btnCancelMail_Click(object sender, EventArgs e)
        {
            panEMail.Visible = false;
        }
        private void EditSimpleCommand()
        {
            if (fgSingleOrders.Row > 1)
            {
                lblClientName.Text = fgSingleOrders[fgSingleOrders.Row, 1] + "";
                txtRealPrice_Edit.Text = fgSingleOrders[fgSingleOrders.Row, 8] + "";
                txtRealQuantity_Edit.Text = fgSingleOrders[fgSingleOrders.Row, 9] + "";
                txtRealAmount_Edit.Text = fgSingleOrders[fgSingleOrders.Row, 10] + "";

                panEdit.Visible = true;
                bEditKatamerismos = true;
            }
        }
        private void txtRealPrice_Edit_LostFocus(object sender, EventArgs e)
        {
            if (Global.IsNumeric(txtRealPrice_Edit.Text) && Global.IsNumeric(txtRealQuantity_Edit.Text))
            {
                if (iProduct_ID == 2) txtRealAmount_Edit.Text = (Convert.ToDecimal(txtRealPrice_Edit.Text) * Convert.ToDecimal(txtRealQuantity_Edit.Text) / Convert.ToDecimal(100)).ToString("0.0000");
                else txtRealAmount_Edit.Text = (Convert.ToDecimal(txtRealPrice_Edit.Text) * Convert.ToDecimal(txtRealQuantity_Edit.Text)).ToString("0.0000");
            }
        }
        private void txtRealQuantity_Edit_LostFocus(object sender, EventArgs e)
        {
            if (Global.IsNumeric(txtRealPrice_Edit.Text) && Global.IsNumeric(txtRealQuantity_Edit.Text))
            {
                if (iProduct_ID == 2) txtRealAmount_Edit.Text = (Convert.ToDecimal(txtRealPrice_Edit.Text) * Convert.ToDecimal(txtRealQuantity_Edit.Text) / Convert.ToDecimal(100)).ToString("0.0000");
                else txtRealAmount_Edit.Text = (Convert.ToDecimal(txtRealPrice_Edit.Text) * Convert.ToDecimal(txtRealQuantity_Edit.Text)).ToString("0.0000");
            }
        }

        private void btnOK_Edit_Click(object sender, EventArgs e)
        {
            fgSingleOrders[fgSingleOrders.Row, 8] = txtRealPrice_Edit.Text;
            fgSingleOrders[fgSingleOrders.Row, 9] = txtRealQuantity_Edit.Text;
            fgSingleOrders[fgSingleOrders.Row, 10] = txtRealAmount_Edit.Text;
            DefineSums();

            if (Convert.ToDecimal(fgSingleOrders[fgSingleOrders.Row, 9]) == 0 && Convert.ToDecimal(fgSingleOrders[fgSingleOrders.Row, 10]) == 0) fgSingleOrders[fgSingleOrders.Row, 8] = 0;
            clsOrdersSecurity klsOrder2 = new clsOrdersSecurity();
            klsOrder2.Record_ID = Convert.ToInt32(fgSingleOrders[fgSingleOrders.Row, 11]);
            klsOrder2.GetRecord();
            klsOrder2.RealPrice = Global.IsNumeric(fgSingleOrders[fgSingleOrders.Row, 8]) ? Convert.ToDecimal(fgSingleOrders[fgSingleOrders.Row, 8]) : 0;
            klsOrder2.RealQuantity = Convert.ToDecimal(fgSingleOrders[fgSingleOrders.Row, 9]);
            klsOrder2.RealAmount = Convert.ToDecimal(fgSingleOrders[fgSingleOrders.Row, 10]);
            klsOrder2.EditRecord();

            panEdit.Visible = false;
        }
        private void btnCancel_Edit_Click(object sender, EventArgs e)
        {
            panEdit.Visible = false;
        }


        protected void ucCS_TextChange(object sender, EventArgs e)
        {
            Global.ContractData stContract = new Global.ContractData();
            stContract = ucCS.SelectedContractData;
            lblContractTitle.Text = stContract.ContractTitle;
            txtPortfolio.Text = stContract.Portfolio;
            klsOrder.CFP_ID = stContract.Contracts_Packages_ID;
            lblStockCompany.Text = stContract.Provider_Title;
            iClient_ID = stContract.Client_ID;
            iContract_ID = stContract.Contract_ID;
            iServiceProvider_ID = stContract.Provider_ID;
            iClientTipos = stContract.ClientType;
            klsOrder.Client_ID = stContract.Client_ID;
            klsOrder.Contract_ID = stContract.Contract_ID;
            klsOrder.ServiceProvider_ID = stContract.Provider_ID;

            txtAction.Focus();

            DefineComission();
        }
        protected void ucPS_TextChange(object sender, EventArgs e)
        {
            Global.ProductData stProduct = new Global.ProductData();
            stProduct = ucPS.SelectedProductData;
            lnkISIN.Text = stProduct.ISIN;
            lblShareTitle.Text = stProduct.Title;
            lblProduct.Text = stProduct.Product_Title;
            iProductCategory_ID = stProduct.ProductCategory_ID;
            lblProductCategory.Text = stProduct.Product_Category;
            lblProductStockExchange_Title.Text = stProduct.StockExchange_Code;
            iShare_ID = stProduct.ShareCode_ID;
            lblCurr.Text = stProduct.Currency;
        }

        public int Rec_ID { get { return this.iRec_ID; } set { this.iRec_ID = value; } }
        public int CommandType_ID { get { return this.iCommandType_ID; } set { this.iCommandType_ID = value; } }
        public int BusinessType { get { return this.iBusinessType; } set { this.iBusinessType = value; } }
        public int LastAktion { get { return this.iLastAktion; } set { this.iLastAktion = value; } }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public int Editable { get { return this.iEditable; } set { this.iEditable = value; } }
    }
}
