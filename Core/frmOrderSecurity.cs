using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace Core
{
    public partial class frmOrderSecurity : Form
    {
        int i, iRec_ID, iMode = 0, iLastAktion, iFeesEditMode = 0, iWarning = 0, iEditable, iContract_ID, iFeesCalcMode, iStatus, iRightsLevel,
            iMIFID_2 = 0, iMIFIDCategory_ID = 0, iMIFID_Risk_Index = 0, iXAA = 0, iSE_ID = 0, iCurrency_ID, iGAP_ID,
            iClient_ID, iNewBulkCommand_ID, iServiceProvider_ID, iLocProvider_ID, iNewContract_ID, iNewContract_Packages_ID, iCFP_ID, iBusinessType,
            iShare_ID, iProduct_ID, iProductCategory_ID, iNewShare_ID, iNewPriceType, iNewConstant, iStockExchange_ID, iClientTipos = 0;
        float sgCompanyFeesPercent;
        decimal decTemp, decTemp2;
        string[] sCheck = { "Δεν ελέγχθηκε", "OK", "Πρόβλημα" };
        string sTemp, sBulkCommand, sMessage, sNewAktion, sNewPrice, sNewFileName, sNewPackageTitle, sSubPath, sProvider_Code;
        bool bCheckList, bContinue, bPressedKey;
        DateTime dTemp, dRecieved, dNewConstantDate;
        CellStyle csCancel;
        SortedList lstRecieved = new SortedList();
        SortedList lstInformed = new SortedList();
        SortedList lstProblems = new SortedList();
        SortedList lstStatus = new SortedList();
        DataRow[] foundRows;
        DataView dtView;
        clsOrdersSecurity klsOrder = new clsOrdersSecurity();
        clsNewOrders NewOrders = new clsNewOrders();
        clsCommandsExecutionsDetails CommandsExecutionsDetails = new clsCommandsExecutionsDetails();

        #region --- Start functions -----------------------------------------------------------------------------
        public frmOrderSecurity()
        {
            InitializeComponent();  

            this.Width = 952;
            this.Height = 820;

            panPortfolio.Top = 90;
            panPortfolio.Left = 406;

            panShares.Top = 172;
            panShares.Left = 111;

            panQuestions.Top = 400;
            panQuestions.Left = 200;

            panFeesCalcMode.Top = 440;
            panFeesCalcMode.Left = 300;

            panFeesEdit.Top = 400;
            panFeesEdit.Left = 300;

            panNotes.Top = 31;
            panNotes.Left = 410;

            bCheckList = false;
            bPressedKey = false;

            //tsbKey.Visible = false;
            lblWarning.Visible = false;
            lblWarning.Left = 4;

            panIssuedInvoice.Visible = false;
            panWarning.Visible = false;
        }
        private void frmOrderSecurity_Load(object sender, EventArgs e)
        {
            if (iRec_ID == 0) this.Text = "Νέα παραγγελία";
            else this.Text = "Παραγγελία (" + iRec_ID + ")";

            if (Global.AllowInsertOldOrders == 0) dAktionDate.Enabled = false;
            else dAktionDate.Enabled = true;

            dSend.Value = Convert.ToDateTime("01/01/1900");
            dSend.CustomFormat = "          ";
            dSend.Format = DateTimePickerFormat.Custom;

            dExecute.MaxDate = DateTime.Now;
            dExecute.CustomFormat = "          ";
            dExecute.Format = DateTimePickerFormat.Custom;

            sSubPath = "";

            //-------------- Define Senders List ------------------   
            dtView = Global.dtUserList.Copy().DefaultView;
            dtView.RowFilter = "Sender = 1 AND Aktive = 1";
            cmbSenders.DataSource = dtView;
            cmbSenders.DisplayMember = "Title";
            cmbSenders.ValueMember = "ID";
            cmbSenders.SelectedValue = 0;
 
            ucCS.StartInit(700, 400, 200, 20, 1);
            ucCS.TextChanged += new EventHandler(ucCS_TextChanged);
            ucCS.Filters = "Status = 1";
            ucCS.ListType = 1;

            ucPS.StartInit(700, 400, 200, 20, 1);
            ucPS.TextChanged += new EventHandler(ucPS_TextChanged);
            ucPS.ListType = 1;
            ucPS.Filters = "Aktive >= 1 ";

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

            Column col1 = fgInforming.Cols[2];
            col1.Name = "Image";
            col1.DataType = typeof(String);
            col1.ComboList = "...";

            //------- fgCheck ----------------------------
            fgCheck.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgCheck.Styles.ParseString(Global.GridStyle);
            fgCheck.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(fgCheck_CellButtonClick);
            fgCheck.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(fgCheck_CellChanged);
            fgCheck.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgCheck_BeforeEdit);
            fgCheck.DrawMode = DrawModeEnum.OwnerDraw;
            fgCheck.ShowCellLabels = true;

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

            //------- fgPortfolio ---------------------------
            fgPortfolio.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgPortfolio.Styles.ParseString(Global.GridStyle);
            fgPortfolio.KeyDown += new System.Windows.Forms.KeyEventHandler(fgPortfolio_KeyDown);
            fgPortfolio.DoubleClick += new System.EventHandler(fgPortfolio_DoubleClick);

            csCancel = fgPortfolio.Styles.Add("Cancelled");
            csCancel.ForeColor = Color.Red;

            //------- fgExecutions ----------------------------
            fgCommands_ExecutionsDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgCommands_ExecutionsDetails.Styles.ParseString(Global.GridStyle);
            fgCommands_ExecutionsDetails.DrawMode = DrawModeEnum.OwnerDraw;
            fgCommands_ExecutionsDetails.ShowCellLabels = true;
            fgCommands_ExecutionsDetails.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(fgCommands_ExecutionsDetails_CellChanged);


            EmptyComiss();

            //---- Start Initialisation - Show Command --------------

            if (iRec_ID != 0)
            {                                              // iRec_ID - order exists - so it's Edit Mode
                klsOrder.Record_ID = iRec_ID;
                klsOrder.GetRecord();
                switch (klsOrder.Aktion)
                {
                    case 1:
                        pan1.BackColor = Color.MediumAquamarine;
                        pan2.BackColor = Color.MediumAquamarine;
                        pan3.BackColor = Color.MediumAquamarine;
                        pan4.BackColor = Color.MediumAquamarine;
                        pan5.BackColor = Color.MediumAquamarine;
                        pan6.BackColor = Color.MediumAquamarine;
                        tpCompanyFees.BackColor = Color.MediumAquamarine;
                        tpProviderFees.BackColor = Color.MediumAquamarine;
                        break;
                    case 2:
                        pan1.BackColor = Color.LightCoral;
                        pan2.BackColor = Color.LightCoral;
                        pan3.BackColor = Color.LightCoral;
                        pan4.BackColor = Color.LightCoral;
                        pan5.BackColor = Color.LightCoral;
                        pan6.BackColor = Color.LightCoral;
                        tpCompanyFees.BackColor = Color.LightCoral;
                        tpProviderFees.BackColor = Color.LightCoral;
                        break;
                    case 3:
                        pan1.BackColor = Color.Silver;
                        pan2.BackColor = Color.Silver;
                        pan3.BackColor = Color.Silver;
                        pan4.BackColor = Color.Silver;
                        pan5.BackColor = Color.Silver;
                        pan6.BackColor = Color.Silver;
                        tpCompanyFees.BackColor = Color.Silver;
                        tpProviderFees.BackColor = Color.Silver;
                        break;
                }
                cbChecked.BackColor = pan4.BackColor;

                iServiceProvider_ID = klsOrder.ServiceProvider_ID;
                lblStockCompany.Text = klsOrder.ServiceProvider_Title;
                lblProductStockExchange_Title.Text = klsOrder.ProductStockExchange_Title;
                if (klsOrder.ContractTipos == 1) lblContractTitle.Text = klsOrder.ContractTitle;
                else lblContractTitle.Text = klsOrder.ClientName;               
                ucCS.ShowClientsList = false;
                ucCS.txtContractTitle.Text = klsOrder.Code;
                ucCS.ShowClientsList = true;
                iClientTipos = klsOrder.ClientTipos;
                lblPortfolio.Text = klsOrder.ProfitCenter;
                txtAction.Text = (klsOrder.Aktion == 1 ? "BUY" : "SELL");
                dAktionDate.Value = klsOrder.AktionDate;

                lblProduct.Text = klsOrder.Product_Title;
                iProduct_ID = klsOrder.Product_ID;
                iProductCategory_ID = klsOrder.ProductCategory_ID;
                lblProductCategory.Text = klsOrder.ProductCategory_Title;
                lblProductStockExchange_Title.Text = klsOrder.ProductStockExchange_Title;
                iShare_ID = klsOrder.Share_ID;
                ucPS.ShowProductsList = false;
                ucPS.txtShareTitle.Text = klsOrder.Security_Code;
                ucPS.ShowProductsList = true;
                txtISIN.Text = klsOrder.Security_ISIN;
                lnkShareTitle.Text = klsOrder.Security_Title;
                cmbConstant.SelectedIndex = klsOrder.Constant;
                dConstant.Text = klsOrder.ConstantDate;
                txtPrice.Text = klsOrder.Price.ToString("0.#######");
                txtQuantity.Text = klsOrder.Quantity.ToString("0.#######");
                txtAmount.Text = klsOrder.Amount.ToString("0.##");
                lblCurr.Text = klsOrder.Curr;
                sSubPath = (klsOrder.ContractTipos == 0 ? klsOrder.ClientName : klsOrder.ContractTitle).Replace(".", "_");   // 0 - Personal Contract, 1 - Company Contract, 2 - Joint Contract
                chkBestExecution.Checked = klsOrder.BestExecution == 1 ? true : false;

                dRecieved = klsOrder.RecieveDate;

                if (Convert.ToDateTime(klsOrder.SentDate) != Convert.ToDateTime("1900/01/01"))
                {
                    dTemp = Convert.ToDateTime(klsOrder.SentDate);
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
                cbChecked.Checked = Convert.ToBoolean(klsOrder.SendCheck);

                if (klsOrder.SettlementDate == Convert.ToDateTime("1900/01/01"))
                {
                    dSettlement.CustomFormat = "          ";
                    dSettlement.Format = DateTimePickerFormat.Custom;
                }
                else dSettlement.Value = klsOrder.SettlementDate;

                if (klsOrder.RTO_InvoiceTitle_ID != 0)
                {
                    lblIssuedInvoice.Text = "Εκδόθηκε " + klsOrder.RTO_InvoiceData;
                    lblFileName.Text = klsOrder.FileName;
                    panIssuedInvoice.Visible = true;
                }

                iContract_ID = klsOrder.Contract_ID;
                lblExecStockExchange_Title.Text = klsOrder.StockExchange_Title;
                lblPackage.Text = klsOrder.Package_Title;
                iCFP_ID = klsOrder.CFP_ID;

                //------------- Define Contract's Clients List --------
                foundRows = Global.dtContracts.Select("Contract_ID = " + iContract_ID);
                if (foundRows.Length > 0)
                {
                    cmbClients.DataSource = foundRows.CopyToDataTable();
                    cmbClients.DisplayMember = "Fullname";
                    cmbClients.ValueMember = "Client_ID";
                    cmbClients.SelectedValue = klsOrder.Client_ID;
                }

                this.Height = 512;
                dExecute.Value = klsOrder.ExecuteDate;
                if ((klsOrder.RealQuantity != 0) || (klsOrder.RealPrice != 0))
                {
                    if (klsOrder.ExecuteDate != Convert.ToDateTime("01/01/1900"))
                    {
                        dTemp = klsOrder.ExecuteDate;
                        dExecute.Format = DateTimePickerFormat.Short;
                        dExecute.Value = dTemp;
                        txtExecuteHour.Text = dTemp.Hour.ToString();
                        txtExecuteMinute.Text = dTemp.Minute.ToString();
                        txtExecuteSecond.Text = dTemp.Second.ToString();
                        ucCS.Enabled = false;
                        this.Height = 820;
                    }
                    else
                    {
                        //dExecute.Value = "1900/01/01";
                        dExecute.CustomFormat = "          ";
                        dExecute.Format = DateTimePickerFormat.Custom;
                        txtExecuteHour.Text = "";
                        txtExecuteMinute.Text = "";
                        txtExecuteSecond.Text = "";
                    }

                    dExecute.Enabled = true;
                    txtExecuteHour.Enabled = true;
                    txtExecuteMinute.Enabled = true;
                    txtExecuteSecond.Enabled = true;

                    txtRealPrice.Enabled = true;
                    txtRealQuantity.Enabled = true;
                    txtRealAmount.Enabled = true;
                    txtFeesAmountEUR.Enabled = true;

                    //panExecuted.Enabled = true
                    btnExecuted.Enabled = false;
                }
                else
                {
                    //dExecute.Value = "1900/01/01"
                    dExecute.CustomFormat = "          ";
                    dExecute.Format = DateTimePickerFormat.Custom;
                    txtExecuteHour.Text = "";
                    txtExecuteMinute.Text = "";
                    txtExecuteSecond.Text = "";
                }

                txtRealQuantity.Text = klsOrder.RealQuantity.ToString("0.#########");
                txtRealPrice.Text = klsOrder.RealPrice.ToString("0.#########");
                txtRealAmount.Text = klsOrder.RealAmount.ToString("0.#########");
                txtAccruedInterest.Text = klsOrder.AccruedInterest.ToString("0.#########");
                lblInvestAmount.Text = (Convert.ToDouble(txtRealAmount.Text) + Convert.ToDouble(txtAccruedInterest.Text)).ToString("0.####");
                lblCurrRate_Title.Text = "EUR/" + lblCurr.Text;
                lblCurrRate.Text = klsOrder.CurrRate.ToString("0.####");
                lblRTO_FeesRate_Title.Text = "EUR/" + lblCurr.Text;
                lblRTO_FeesRate.Text = klsOrder.CurrRate.ToString("0.####"); ;

                //--- block 6 ------------------------------------------------------------------------------------------------
                dSettlement.Value = klsOrder.SettlementDate;
                ShowBlock6();

                txtNotes.Text = klsOrder.Notes;
                lstType.SelectedIndex = klsOrder.PriceType;
                iFeesCalcMode = klsOrder.FeesCalcMode;
                //ControlFeesCalcMode();
                sgCompanyFeesPercent = Convert.ToSingle(klsOrder.CompanyFeesPercent);
                cmbSenders.SelectedValue = klsOrder.User_ID;

                if (klsOrder.II_ID == 0) tslInvestProposals.Enabled = false;
                else
                {
                    tslInvestProposals.Enabled = true;
                    if (klsOrder.RecieveMethod_ID == 8) tslInvestProposals.Text = "DPM Order";
                    else tslInvestProposals.Text = "Επενδυτικές Προτάσεις";
                }
                if (klsOrder.Status >= 0)
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

                picBondCalc.Visible = false;
                //txtAccruedInterest.Enabled = false;

                switch (klsOrder.Product_ID)
                {
                    case 1:                                         // Shares (Metoxes)
                        lblPrice.Text = "Τιμή";
                        lblPrice.Visible = true;
                        lblCurr.Visible = true;
                        lblQuantity.Text = "Τεμάχια";
                        lblQuantity.Visible = true;
                        txtQuantity.Visible = true;
                        lblAmount.Text = "Ποσό επενδ.";
                        lblAmount.Visible = true;
                        txtAmount.Visible = true;
                        lblRealPrice.Text = "Τιμή";
                        lblRealPrice.Visible = true;
                        lblRealQuantity.Text = "Τεμάχια";
                        lblRealQuantity.Visible = true;
                        txtRealQuantity.Visible = true;
                        lblRealAmount.Text = "Ποσό επενδ.";
                        lblRealAmount.Visible = true;
                        txtRealAmount.Visible = true;
                        break;
                    case 2:                                           // Bond (Omologa)
                        lblPrice.Text = "Τιμή";
                        lblPrice.Visible = true;
                        lblCurr.Visible = true;
                        lblQuantity.Text = "Ονομ.Αξία";
                        lblQuantity.Visible = true;
                        txtQuantity.Visible = true;
                        lblAmount.Text = "Ποσό επενδ.";
                        lblAmount.Visible = true;
                        txtAmount.Visible = true;
                        lblRealPrice.Text = "Τιμή";
                        lblRealPrice.Visible = true;
                        lblRealQuantity.Text = "Ονομ.Αξία";
                        lblRealQuantity.Visible = true;
                        txtRealQuantity.Visible = true;
                        lblRealAmount.Text = "Ποσό επενδ.";
                        lblRealAmount.Visible = true;
                        txtRealAmount.Visible = true;
                        picBondCalc.Visible = true;
                        //txtAccruedInterest.Enabled = true;
                        break;
                    case 4:                 // ETF (DAK)
                        lblPrice.Text = "Τιμή";
                        lblPrice.Visible = true;
                        lblCurr.Visible = true;
                        lblQuantity.Text = "Τεμάχια";
                        lblQuantity.Visible = true;
                        txtQuantity.Visible = true;
                        lblAmount.Text = "Ποσό επενδ.";
                        lblAmount.Visible = true;
                        txtAmount.Visible = true;
                        lblRealPrice.Text = "Τιμή";
                        lblRealPrice.Visible = true;
                        lblRealQuantity.Text = "Τεμάχια";
                        lblRealQuantity.Visible = true;
                        txtRealQuantity.Visible = true;
                        lblRealAmount.Text = "Ποσό επενδ.";
                        lblRealAmount.Visible = true;
                        txtRealAmount.Visible = true;
                        break;
                    case 6:                 // FUND (AK)
                        lblPrice.Text = "Τιμή";
                        lblPrice.Visible = false;
                        lblCurr.Visible = false;
                        lblQuantity.Text = "Μερίδια";
                        lblQuantity.Visible = true;
                        txtQuantity.Visible = true;
                        lblAmount.Text = "Ποσό επενδ.";
                        lblAmount.Visible = true;
                        txtAmount.Visible = true;
                        lblRealPrice.Text = "Τιμή";
                        lblRealPrice.Visible = false;
                        lblRealQuantity.Text = "Μερίδια";
                        lblRealQuantity.Visible = true;
                        txtRealQuantity.Visible = true;
                        lblRealAmount.Text = "Ποσό επενδ.";
                        lblRealAmount.Visible = true;
                        txtRealAmount.Visible = true;
                        break;
                }

                //------------- Define History List ------------------
                clsOrdersSecurity klsOrder2 = new clsOrdersSecurity();
                klsOrder2.Record_ID = iRec_ID;
                klsOrder2.GetHistory();

                fgQuestions.Redraw = false;
                fgQuestions.Rows.Count = 1;

                foreach (DataRow dtRow in klsOrder2.List.Rows)
                    fgQuestions.AddItem(dtRow["DateIns"] + "\t" + dtRow["Authorname"] + "\t" + dtRow["Description"]);

                fgQuestions.Redraw = true;

                if (fgQuestions.Rows.Count > 1) picQuestions.Visible = true;
                else picQuestions.Visible = false;

                //-------------- Define Recieved Files List ------------------
                klsOrder2 = new clsOrdersSecurity();
                klsOrder2.Record_ID = iRec_ID;
                klsOrder2.GetRecievedFiles();

                fgRecieved.Redraw = false;
                fgRecieved.Rows.Count = 1;
                foreach (DataRow dtRow in klsOrder2.List.Rows)
                    fgRecieved.AddItem(dtRow["DateIns"] + "\t" + dtRow["Method_Title"] + "\t" + dtRow["FileName"] + "\t" +
                                       dtRow["ID"] + "\t" + dtRow["Method_ID"] + "\t" + "");                                       //drList("FilePath")

                fgRecieved.Redraw = true;

                //-------------- Define Informings List -----------------
                klsOrder2 = new clsOrdersSecurity();
                klsOrder2.Record_ID = iRec_ID;
                klsOrder2.GetInformings();

                fgInforming.Redraw = false;
                fgInforming.Rows.Count = 1;
                foreach (DataRow dtRow in klsOrder2.List.Rows)
                    fgInforming.AddItem(dtRow["DateIns"] + "\t" + dtRow["InformationMethod"] + "\t" + dtRow["FileName"] + "\t" +
                                        dtRow["DateSent"] + "\t" + dtRow["ID"] + "\t" + dtRow["InformMethod"] + "\t" + dtRow["User_ID"] + "\t" + "");

                fgInforming.Redraw = true;


                //-------------- Define Check List -----------------
                klsOrder2 = new clsOrdersSecurity();
                klsOrder2.Record_ID = iRec_ID;
                klsOrder2.GetChecks();

                fgCheck.Redraw = false;
                fgCheck.Rows.Count = 1;
                foreach (DataRow dtRow in klsOrder2.List.Rows)
                    fgCheck.AddItem(dtRow["DateIns"] + "\t" + dtRow["UserName"] + "\t" + sCheck[Convert.ToInt32(dtRow["Status"])] + "\t" +
                                            dtRow["ProblemType_Title"] + "\t" + dtRow["Notes"] + "\t" + dtRow["FileName"] + "\t" +
                                            dtRow["ReversalRequestDate"] + "\t" + dtRow["ID"] + "\t" + dtRow["User_ID"] + "\t" +
                                            dtRow["Status"] + "\t" + "" + "\t" + dtRow["ProblemType_ID"]);                // preLast Column - Empty, it's shows that it "old" file. "New" file has full path of file

                fgCheck.Redraw = true;


                //--- Define fgExecutions Grid ---------------------------------- 
                fgCommands_ExecutionsDetails.Redraw = false;
                fgCommands_ExecutionsDetails.Rows.Count = 1;

                CommandsExecutionsDetails = new clsCommandsExecutionsDetails();
                CommandsExecutionsDetails.Command_ID = iRec_ID;
                CommandsExecutionsDetails.GetList();
                foreach (DataRow dtRow in CommandsExecutionsDetails.List.Rows)
                {
                    iStockExchange_ID = 0;
                    foundRows = Global.dtStockExchanges.Select("Code = '" + dtRow["StockExchange_MIC"] + "'");
                    if (foundRows.Length > 0) iStockExchange_ID = Convert.ToInt32(foundRows[0]["ID"]);

                    fgCommands_ExecutionsDetails.AddItem(Convert.ToDateTime(dtRow["CurrentTimestamp"]).ToString("dd/MM/yyyy HH:mm:ss") + "\t" + dtRow["SecondOrdID"] + "\t" +
                                            string.Format("{0:#0.00####}", dtRow["Price"]) + "\t" + string.Format("{0:#0.0######}", dtRow["Quantity"]) + "\t" +
                                            string.Format("{0:#0.00}", Convert.ToDecimal(dtRow["Price"]) * Convert.ToDecimal(dtRow["Quantity"])) + "\t" + 
                                            dtRow["StockExchange_MIC"] + "\t" + dtRow["ID"] + "\t" + iStockExchange_ID);
                }
                fgCommands_ExecutionsDetails.Redraw = true;

                //-------------------------------------------------
                DefinePortfolioList(ucCS.txtContractTitle.Text);

                //--- calculate lblAmount_EUR -------------------------------

                lblAmount_EUR.Text = "";
                if (lstType.SelectedIndex == 1)
                {                                               // is Market 
                    clsProductsCodes klsProductsCode = new clsProductsCodes();
                    klsProductsCode.DateIns = dAktionDate.Value;
                    klsProductsCode.Code = klsOrder.Security_Code;
                    klsProductsCode.GetPrice_Code();
                    decTemp2 = Convert.ToDecimal(klsProductsCode.LastClosePrice);
                }
                else decTemp2 = Convert.ToDecimal(txtPrice.Text);

                if (Global.IsNumeric(txtQuantity.Text))
                {
                    decTemp = decTemp2 * Convert.ToDecimal(txtQuantity.Text);
                    if (klsOrder.Product_ID == 2) decTemp = decTemp / 100;                                  // for Omologa / 100
                    //txtAmount.Text = decTemp.ToString("0.##");

                    if (klsOrder.Curr != "EUR")
                        if (Convert.ToDouble(lblCurrRate.Text) != 0) decTemp = decTemp / Convert.ToDecimal(lblCurrRate.Text);
                        else decTemp = 0;
                    lblAmount_EUR.Text = decTemp.ToString("0.##") + " EUR";
                }


                panAmount_EUR.Visible = true;

                //--- define panels and buttons availability  -----------------------------------------------
                iWarning = 0;
                sBulkCommand = klsOrder.BulkCommand.Replace("<", "").Replace(">", "");
                if (sBulkCommand.Length > 0 || klsOrder.CommandType_ID > 1 || klsOrder.BusinessType_ID > 1)
                {
                    clsOrdersSecurity klsOrder3 = new clsOrdersSecurity();
                    klsOrder3.AktionDate = dAktionDate.Value;
                    klsOrder3.BulkCommand = klsOrder.BulkCommand;
                    klsOrder3.GetBulkCommand_Parent();

                    tslCancel.Enabled = false;
                    //tsbSave.Visible = false;
                    //tsbKey.Visible = true;

                    switch (klsOrder3.CommandType_ID)
                    {
                        case 2:
                            iWarning = 2;                             // 1 - Execution klsOrder. It's blocked
                            break;
                        case 3:
                            iWarning = 3;                             // 2 - Bulk klsOrder. It's blocked
                            break;
                        case 4:
                            iWarning = 4;                             // 4 - DPM klsOrder. It's blocked
                            break;
                    }
                }

                if (iWarning == 0)
                {
                    btnSend.Enabled = true;
                    panSend.Enabled = true;
                    picEmptySend.Enabled = true;
                    btnExecuted.Enabled = true;
                    picEmptyExecute.Enabled = true;
                    panWarning.Visible = false;

                    if (Convert.ToDateTime(klsOrder.SentDate) != Convert.ToDateTime("1900/01/01"))
                    {
                        pan4.Enabled = false;
                        tsbKey.Visible = true;
                    }
                    if (Convert.ToDateTime(klsOrder.ExecuteDate) != Convert.ToDateTime("1900/01/01"))
                    {
                        pan5.Enabled = false;
                        tsbKey.Visible = true;
                    }
                }
                else
                {
                    btnSend.Enabled = false;
                    panSend.Enabled = false;
                    picEmptySend.Enabled = false;
                    btnExecuted.Enabled = false;

                    //tsbSave.Visible = false;
                    tsbKey.Visible = true;
                    switch (iWarning)
                    {
                        case 1:
                            lblWarning.Text = "Order blocked. Reason - Execution order. BulkNumber = " + sBulkCommand;
                            break;
                        case 2:
                            lblWarning.Text = "Order blocked. Reason - Bulk order. BulkNumber = " + sBulkCommand;
                            break;
                        case 3:
                            lblWarning.Text = "Διαβίβαση αυτης της εντολής γίνετε απο Basket ";
                            break;
                        case 4:
                            lblWarning.Text = "Order blocked. Reason - DPM order. BulkNumber = " + sBulkCommand;
                            break;
                    }
                    lblWarning.Visible = true;
                    panWarning.Visible = true;

                    pan4.Enabled = false;
                    pan5.Enabled = false;
                    //tsbKey.Visible = true;
                }

                iClient_ID = klsOrder.Client_ID;
                if (Convert.ToDateTime(klsOrder.SentDate) != Convert.ToDateTime("1900/01/01")) pan1.Enabled = false;
                this.Refresh();

                if (iRightsLevel < 2 || iEditable == 0)
                {
                    tslCancel.Enabled = false;
                    //tsbSave.Visible = false;
                }

                if (iMode == 2) pan5.Enabled = true;

                txtNotes.Focus();
            }
            else
            {                                      // iRec_ID = 0  new order - so it's Add Mode
                this.Height = 512;
                panWarning.Visible = false;
                tslInvestProposals.Enabled = false;
                tslCancel.Enabled = false;
                tsbHistory.Enabled = false;
                //tsbSave.Enabled = true;
                klsOrder.AktionDate = DateTime.Now;

                clsProductsCodes klsProductCode = new clsProductsCodes();
                klsProductCode.Record_ID = iNewShare_ID;
                klsProductCode.GetRecord();
                iShare_ID = klsProductCode.Record_ID;
                ucPS.ShowProductsList = false;
                ucPS.txtShareTitle.Text = klsOrder.Security_Code;
                ucPS.ShowProductsList = true;
                txtISIN.Text = klsOrder.Security_ISIN;
                lnkShareTitle.Text = klsOrder.Security_Title;
                lblProduct.Text = klsProductCode.Product_Title;
                iProduct_ID = klsProductCode.Product_ID;
                iProductCategory_ID = klsProductCode.ProductCategory_ID;

                bCheckList = false;
                dtView = Global.dtProductsCategories.Copy().DefaultView;
                dtView.RowFilter = "Product_ID = " + iProduct_ID;

                bCheckList = true;

                lblProductStockExchange_Title.Text = klsProductCode.StockExchange_Code;
                iStockExchange_ID = klsProductCode.StockExchange_ID;
                lblCurr.Text = klsProductCode.Curr;

                lstType.SelectedIndex = iNewPriceType;
                txtPrice.Text = sNewPrice;

                cmbConstant.SelectedIndex = iNewConstant;
                dConstant.Value = dNewConstantDate;

                txtRealPrice.Text = "0";
                txtRealQuantity.Text = "0";
                txtRealAmount.Text = "0";
                txtAccruedInterest.Text = "0";

                panSend.Enabled = false;
                dExecute.Value = Convert.ToDateTime("1900/01/01");
                dExecute.CustomFormat = "          ";

                ucCS.Focus();
            }

            bCheckList = true;

            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Refresh();
        }               
        #endregion
        #region --- Top tooltips functions ---------------------------------------------------------------------
        private void tslInvestProposals_Click(object sender, EventArgs e)
        {
            if (klsOrder.RecieveMethod_ID == 0)
            {
                clsOrdersDPM OrdersDPM = new clsOrdersDPM();
                OrdersDPM.Record_ID = klsOrder.II_ID;
                OrdersDPM.GetRecord();
                if (OrdersDPM.Contract_ID != 0)
                {
                    frmDPMOrder_Client locDPMOrder_Client = new frmDPMOrder_Client();
                    locDPMOrder_Client.Today = OrdersDPM.AktionDate;
                    locDPMOrder_Client.II_ID = klsOrder.II_ID;
                    locDPMOrder_Client.ShowDialog();
                }
                else
                {
                    frmDPMOrder_Product locDPMOrder_Product = new frmDPMOrder_Product();
                    locDPMOrder_Product.DPM_ID = klsOrder.II_ID;
                    locDPMOrder_Product.Today = OrdersDPM.AktionDate;
                    locDPMOrder_Product.ShowDialog();
                };
            }
            else {
                frmInvestProposal locInvestProposal = new frmInvestProposal();
                locInvestProposal.Aktion = 1;                             // 0 - Edit 
                locInvestProposal.II_ID = klsOrder.II_ID;
                locInvestProposal.ShowDialog();
            }
        }

        private void tslCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(sMessage, Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                klsOrder.Status = iStatus;
                klsOrder.EditRecord();
                iLastAktion = 1;             // was saved (cancel)
                this.Close();
            }
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
            if (iWarning == 0)
            {
                picEmptySend.Enabled = true;
                panSend.Enabled = true;
                picEmptyExecute.Enabled = true;
                //tsbSave.Visible = true;                
                pan4.Enabled = true;
                pan5.Enabled = true;
            }
            tslCancel.Enabled = true;
            pan1.Enabled = true;
            bPressedKey = true;
        }
        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (bPressedKey)
            {
                sTemp = "";
                if (klsOrder.AktionDate != dAktionDate.Value) sTemp = sTemp + "Ημερομηνία κίνησης: " + klsOrder.AktionDate + " -> " + dAktionDate.Value + "\n";

                if (klsOrder.ContractTitle != lblContractTitle.Text) sTemp = sTemp + "Σύμβαση: " + klsOrder.ContractTitle + " -> " + lblContractTitle.Text + "\n";

                if (klsOrder.Code != ucCS.txtContractTitle.Text) sTemp = sTemp + "Κωδικός Πελάτη: " + klsOrder.Code + " -> " + ucCS.txtContractTitle.Text + "\n";

                if (klsOrder.ProfitCenter != lblPortfolio.Text) sTemp = sTemp + "Profit Center/SubCode: " + klsOrder.ProfitCenter + " -> " + lblPortfolio.Text + "\n";

                if (klsOrder.Aktion == 1 && txtAction.Text != "BUY") sTemp = sTemp + "Πράξη: BUY -> " + txtAction.Text + "\n";

                if (klsOrder.Aktion == 2 && txtAction.Text != "SELL") sTemp = sTemp + "Πράξη: SELL -> " + txtAction.Text + "\n";

                if (klsOrder.Product_Title != lblProduct.Text) sTemp = sTemp + "Τύπος προϊόντος: " + klsOrder.Product_Title + " -> " + lblProduct.Text + "\n";

                if (klsOrder.Security_Code != ucPS.txtShareTitle.Text) sTemp = sTemp + "Κωδικός προϊόντος: " + klsOrder.Security_Code + " -> " + ucPS.txtShareTitle.Text + "\n";

                if (klsOrder.Security_ISIN != txtISIN.Text) sTemp = sTemp + "ISIN: " + klsOrder.Security_ISIN + " -> " + txtISIN.Text + "\n";

                if (Convert.ToSingle(klsOrder.Price) != Convert.ToSingle(txtPrice.Text)) sTemp = sTemp + "Τιμή: " + klsOrder.Price + " -> " + txtPrice.Text + "\n";

                if (Convert.ToSingle(klsOrder.Quantity) != Convert.ToSingle(txtQuantity.Text)) sTemp = sTemp + "Ποσότητα: " + klsOrder.Quantity + " -> " + txtQuantity.Text + "\n";

                if (Convert.ToSingle(klsOrder.Amount) != Convert.ToSingle(txtAmount.Text)) sTemp = sTemp + "Ποσο επένδ.: " + klsOrder.Amount + " -> " + txtAmount.Text + "\n";

                if (klsOrder.Curr != lblCurr.Text) sTemp = sTemp + "Νόμισμα: " + klsOrder.Curr + " -> " + lblCurr.Text + "\n";

                if (klsOrder.Constant != cmbConstant.SelectedIndex) sTemp = sTemp + "Διάρκεια: " + klsOrder.Constant + " -> " + cmbConstant.SelectedIndex + "\n";

                if (klsOrder.Notes != txtNotes.Text) sTemp = sTemp + "Σχόλιο: " + klsOrder.Notes + " -> " + txtNotes.Text + "\n";

                if (klsOrder.ConstantDate != dConstant.Text) sTemp = sTemp + "Διάρκεια Ημερομηνία: " + klsOrder.ConstantDate + " -> " + dConstant.Text + "\n";

                if (Convert.ToDateTime(klsOrder.ExecuteDate) != Convert.ToDateTime("1900/01/01"))
                    if (Convert.ToDateTime(klsOrder.SentDate).ToString("dd/MM/yyyy") != dSend.Value.ToString("dd/MM/yyyy"))
                        sTemp = sTemp + "Ημερ.Διαβίβασης: " + klsOrder.SentDate + " -> " + dSend.Value.ToString("dd/MM/yyyy") + "\n";

                if (Convert.ToDateTime(klsOrder.ExecuteDate).ToString("dd/MM/yyyy") != dExecute.Value.ToString("dd/MM/yyyy"))
                    sTemp = sTemp + "Ημερ.Εκτέλεσης: " + klsOrder.ExecuteDate + " -> " + dExecute.Value.ToString("dd/MM/yyyy") + "\n";


                iMode = 1;                       // 1 - Save & Exit,   2 - Show only
                txtCurrentValues.Text = sTemp;
                txtHistoryNotes.Text = "";
                btnOK_Save.Enabled = false;
                panNotes.Top = 31;
                panNotes.Left = 410;
                panNotes.Visible = true;
            }
            else
            {
                SaveRecord();
                this.Close();
                iLastAktion = 1;            // 1 - was saved (added)
            }
        }
        #endregion
        #region --- Edit functions -----------------------------------------------------------------------
        private void picPortfolio_Click(object sender, EventArgs e)
        {
            panPortfolio.Visible = true;
            fgPortfolio.Focus();
        }
        private void fgPortfolio_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13:                                //  ENTER  
                    PortfolioChoice();
                    break;
                case 27:                               //   ESC     
                    panPortfolio.Visible = false;
                    break;
            }
        }
        private void fgPortfolio_DoubleClick(object sender, EventArgs e)
        {
            if (fgPortfolio.Row > 0)
            {
                PortfolioChoice();
                panPortfolio.Visible = false;
            }
        }
        private void PortfolioChoice()
        {
            if (fgPortfolio.Row > 0)
            {
                if (Convert.ToInt32(fgPortfolio[fgPortfolio.Row, 2]) == 0)
                    MessageBox.Show("Portfolio είναι ανενεργό", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                {
                    lblPortfolio.Text = fgPortfolio[fgPortfolio.Row, 0] + "";
                    klsOrder.Contract_ID = Convert.ToInt32(fgPortfolio[fgPortfolio.Row, 1]);
                    klsOrder.CFP_ID = Convert.ToInt32(fgPortfolio[fgPortfolio.Row, 4]);
                    klsOrder.ServiceProvider_ID = Convert.ToInt32(fgPortfolio[fgPortfolio.Row, 3]);
                    panPortfolio.Visible = false;
                    txtAction.Focus();

                    DefineComission();
                }
            }
        }
        private void DefinePortfolioList(string sCode)
        {
            foundRows = Global.dtContracts.Select("ID = " + klsOrder.Contract_ID + " and Package_DateStart <= '" + dAktionDate.Value + "' and Package_DateFinish = '" + dAktionDate.Value.ToString("dd/MM/yyyy") + "'");
            if (foundRows.Length > 0)
            {
                iNewContract_ID = Convert.ToInt32(foundRows[0]["ID"]);
                iNewContract_Packages_ID = Convert.ToInt32(foundRows[0]["Contracts_Packages_ID"]);
                sNewPackageTitle = foundRows[0]["Package_Title"] + "";
            }

            fgPortfolio.Redraw = false;
            fgPortfolio.Rows.Count = 1;

            dtView = Global.dtContracts.Copy().DefaultView;
            dtView.RowFilter = "Code = '" + sCode + "' and Client_ID = " + klsOrder.Client_ID;
            foreach (DataRowView dtViewRow in dtView)
                fgPortfolio.AddItem(dtViewRow["Portfolio"] + "\t" + dtViewRow["ID"] + "\t" + dtViewRow["Status"] + "\t" + dtViewRow["ServiceProvider_ID"] + "\t" + dtViewRow["CFP_ID"]);

            fgPortfolio.Redraw = true;
        }
        private void picClose_Portfolio_Click(object sender, EventArgs e)
        {
            panPortfolio.Visible = false;
            fgPortfolio.Focus();
        }
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
        private void picCopy2Clipboard_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Convert.IsDBNull(Clipboard.GetText())) Clipboard.SetDataObject(txtISIN.Text + "", true, 10, 100);
            }
            catch (Exception)
            {
            }
        }
        private void picEmptyProduct_Click(object sender, EventArgs e)
        {
            klsOrder.Share_ID = 0;
            ucPS.ShowProductsList = false;
            ucPS.txtShareTitle.Text = "";
            ucPS.ShowProductsList = true;
            txtISIN.Text = "";
            lnkShareTitle.Text = "";
            lblProduct.Text = "";
            lblProductCategory.Text = "";
            lblProductStockExchange_Title.Text = "";
            lblCurr.Text = "";
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
        private void txtPrice_LostFocus(object sender, EventArgs e)
        {
            if (!Global.IsNumeric(txtPrice.Text) || txtPrice.Text.IndexOf(".") > 0)
            {
                txtPrice.BackColor = Color.Red;
                txtPrice.Focus();
            }
            else
            {
                txtPrice.BackColor = Color.White;

                if (klsOrder.Product_ID == 2) txtAmount.Text = string.Format("{0:#0.##}", (Convert.ToDecimal(txtPrice.Text) * Convert.ToDecimal(txtQuantity.Text) / Convert.ToDecimal(100.0)));
                else txtAmount.Text = string.Format("{0:#0.##}", (Convert.ToDecimal(txtPrice.Text) * Convert.ToDecimal(txtQuantity.Text)));
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
            dSend.Value = dTemp;
            txtSendHour.Text = dTemp.Hour.ToString();
            txtSendMinute.Text = dTemp.Minute.ToString();
            txtSendSecond.Text = dTemp.Second.ToString();

            dSend.Enabled = true;
            txtSendHour.Enabled = true;
            txtSendMinute.Enabled = true;
            txtSendSecond.Enabled = true;

            dSend.Focus();

            txtRealPrice.Text = "0";
            txtRealQuantity.Text = "0";
            txtRealAmount.Text = "0";
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
        private void dSend_ValueChanged(object sender, EventArgs e)
        {
            if (bCheckList)
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

            txtRealPrice.Text = txtPrice.Text;
            txtRealPrice.Enabled = true;

            txtRealQuantity.Text = txtQuantity.Text;
            txtRealQuantity.Enabled = true;

            if (klsOrder.Product_ID == 2)
            {
                txtRealAmount.Text = (Convert.ToDecimal(txtPrice.Text) * Convert.ToDecimal(txtQuantity.Text) / 100).ToString("0.####");
                //txtAccruedInterest.Text = CallBondCalc(klsOrder.Security_Share_ID, txtRealPrice.Text, txtRealQuantity.Text);        //!!!!!!!!!!! ???????????
                txtAccruedInterest.Text = "0";
            }
            else
            {
                txtRealAmount.Text = (Convert.ToDecimal(txtPrice.Text) * Convert.ToDecimal(txtQuantity.Text)).ToString("0.####");
                txtAccruedInterest.Text = "0";
            }
            txtRealAmount.Enabled = true;
            lblInvestAmount.Text = (Convert.ToDecimal(txtRealAmount.Text) + Convert.ToDecimal(txtAccruedInterest.Text)).ToString("0.####");

            klsOrder.ExecuteDate = dExecute.Value;
            klsOrder.RealPrice = Convert.ToDecimal(txtRealPrice.Text);
            klsOrder.RealQuantity = Convert.ToDecimal(txtRealQuantity.Text);
            klsOrder.RealAmount = Convert.ToDecimal(txtRealAmount.Text);
            klsOrder.AccruedInterest = Convert.ToDecimal(txtAccruedInterest.Text);

            DefineCurrRate();
            DefineComission();

            if (klsOrder.PackageType_ID == 3)                      // 3 - Diaxeirisi          @@@ ПАРАМЕТРИЗИРОВАТЬ
                fgInforming.AddItem(DateTime.Now.ToString("dd/MM/yyyy") + "\t" + "Προσωπικά" + "\t" + "" + "\t" + "" + "\t" + "0" + "\t" + "7" + "\t" + Global.User_ID + "\t" + "", 1);

            txtRealPrice.Enabled = true;
            txtRealPrice.Focus();
        }
        private void picEmptyExecute_Click(object sender, EventArgs e)
        {
            dExecute.Value = Convert.ToDateTime("1900/01/01");
            dExecute.CustomFormat = "          ";
            dExecute.Format = DateTimePickerFormat.Custom;

            txtExecuteHour.Text = "";
            txtExecuteMinute.Text = "";
            txtExecuteSecond.Text = "";

            txtRealQuantity.Text = "0";
            txtRealPrice.Text = "0";
            txtRealAmount.Text = "0";
            lblInvestAmount.Text = "0";

            EmptyComiss();

            txtRealQuantity.Enabled = true;
            txtRealPrice.Enabled = true;
            txtRealPrice.Focus();
        }
        private void dExecute_ValueChanged(object sender, EventArgs e)
        {
            if ((dExecute.Value.ToString("yyyy/MM/dd") != "1900/01/01") && (dSend.Value != Convert.ToDateTime("1900/01/01")) && (dSend.Value > dExecute.Value))
                MessageBox.Show("Wrong Date: Ημερομηνία Εκτέλεσης δεν μπορεί να είναι μικρότερη απο Ημερομηνία Διαβίβασης", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            if (dExecute.Value.ToString("yyyy/MM/dd") != "1900/01/01")
            {
                dExecute.CustomFormat = "dd/MM/yyyy";
                if (txtExecuteHour.Text.Length == 0)
                {
                    txtExecuteHour.Text = dExecute.Value.Hour.ToString();
                    txtExecuteMinute.Text = dExecute.Value.Minute.ToString();
                    txtExecuteSecond.Text = dExecute.Value.Second.ToString();
                }

                if (bCheckList)
                {
                    clsContracts klsContract = new clsContracts();
                    klsContract.Record_ID = klsOrder.Contract_ID;
                    klsContract.AktionDate = dExecute.Value;
                    klsContract.GetRecord_Date();
                    iNewContract_ID = klsContract.Record_ID;
                    iNewContract_Packages_ID = klsContract.Contract_Packages_ID;
                    sNewPackageTitle = klsContract.ContractTitle;

                    if (iNewContract_ID != iContract_ID || iNewContract_Packages_ID != iCFP_ID)
                    {
                        sTemp = "Την ημερομηνία εκτέλεσης " + dExecute.Value.ToString("dd/MM/yyyy") + " ισχύει το πακέτο " + sNewPackageTitle + ", " + "\n" +
                               "που είναι διαφορετικό από το πακέτο " + lblPackage.Text + ", που ισχύει την ημερομηνία λήψης " + dAktionDate.Value.ToString("dd/MM/yyyy") + "." + "\n" +
                               "Θέλετε να γίνει αλλαγή προμήθειας;";
                        if (MessageBox.Show(sTemp, Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            iContract_ID = iNewContract_ID;
                            iCFP_ID = iNewContract_Packages_ID;
                            lblPackage.Text = sNewPackageTitle;
                        }
                    }
                }
            }
        }
        private void txtRealPrice_LostFocus(object sender, EventArgs e)
        {
            if (!Global.IsNumeric(txtRealPrice.Text)) txtRealPrice.Text = "0";
            if (!Global.IsNumeric(txtRealQuantity.Text)) txtRealQuantity.Text = "0";
            if (!Global.IsNumeric(txtRealAmount.Text)) txtRealAmount.Text = "0";
            if (!Global.IsNumeric(txtAccruedInterest.Text)) txtAccruedInterest.Text = "0";

            if (klsOrder.Product_ID == 2)
                txtRealAmount.Text = (Convert.ToDecimal(txtRealPrice.Text) * Convert.ToDecimal(txtRealQuantity.Text) / 100).ToString("0.####");
            else
                txtRealAmount.Text = (Convert.ToDecimal(txtRealPrice.Text) * Convert.ToDecimal(txtRealQuantity.Text)).ToString("0.####");

            txtRealAmount.Enabled = true;
            lblInvestAmount.Text = (Convert.ToDecimal(txtRealAmount.Text) + Convert.ToDecimal(txtAccruedInterest.Text)).ToString("0.####");

            klsOrder.ExecuteDate = dExecute.Value;
            klsOrder.RealPrice = Convert.ToDecimal(txtRealPrice.Text);
            klsOrder.RealQuantity = Convert.ToDecimal(txtRealQuantity.Text);
            klsOrder.RealAmount = Convert.ToDecimal(txtRealAmount.Text);
            klsOrder.AccruedInterest = Convert.ToDecimal(txtAccruedInterest.Text);

            DefineComission();
        }
        private void txtRealQuantity_LostFocus(object sender, EventArgs e)
        {
            if (!Global.IsNumeric(txtRealPrice.Text)) txtRealPrice.Text = "0";
            if (!Global.IsNumeric(txtRealQuantity.Text)) txtRealQuantity.Text = "0";
            if (!Global.IsNumeric(txtRealAmount.Text)) txtRealAmount.Text = "0";
            if (!Global.IsNumeric(txtAccruedInterest.Text)) txtAccruedInterest.Text = "0";

            if (klsOrder.Product_ID == 2)
                txtRealAmount.Text = (Convert.ToDecimal(txtRealPrice.Text) * Convert.ToDecimal(txtRealQuantity.Text)).ToString("0.####");
            else
                txtRealAmount.Text = (Convert.ToDecimal(txtRealPrice.Text) * Convert.ToDecimal(txtRealQuantity.Text)).ToString("0.####");

            txtRealAmount.Enabled = true;
            lblInvestAmount.Text = (Convert.ToDecimal(txtRealAmount.Text) + Convert.ToDecimal(txtAccruedInterest.Text)).ToString("0.####");

            klsOrder.ExecuteDate = dExecute.Value;
            klsOrder.RealPrice = Convert.ToDecimal(txtRealPrice.Text);
            klsOrder.RealQuantity = Convert.ToDecimal(txtRealQuantity.Text);
            klsOrder.RealAmount = Convert.ToDecimal(txtRealAmount.Text);
            klsOrder.AccruedInterest = Convert.ToDecimal(txtAccruedInterest.Text);

            DefineComission();
        }
        private void txtAccruedInterest_LostFocus(object sender, EventArgs e)
        {
            klsOrder.AccruedInterest = Convert.ToDecimal(txtAccruedInterest.Text);
            DefineComission();
        }
        private void picBondCalc_Click(object sender, EventArgs e)
        {
            DefineComission();
        }
        private void dSettlement_ValueChanged(object sender, EventArgs e)
        {
            dSettlement.CustomFormat = "dd/MM/yyyy";
        }
        private void btnFeesCalc_Click(object sender, EventArgs e)
        {
            DefineComission();

            CalcRTOComission();
        }
        private void btnCalcAuto_Click(object sender, EventArgs e)
        {
            if (iFeesCalcMode == 1)
            {                                   // 1 - Automatic Calculation Mode, 2 - Manually Calculation Mode  
                DefineComission();
                panFeesCalcMode.Visible = false;
            }
            else panFeesCalcMode.Visible = true;
        }
        private void btnCalcManual_Click(object sender, EventArgs e)
        {
            iFeesCalcMode = 2;
        }

        private void txtFeesDiscountPercent_DoubleClick(object sender, EventArgs e)
        {
            if (iFeesCalcMode == 2)
            {
                iFeesEditMode = 1;
                lblFeesTitle.Text = "% έκπτωσης προμήθειας";
                panFeesEdit.Visible = true;
                lblFeesEdit_Date.Text = DateTime.Now.ToString("d");
                lblFeesEdit_User.Text = Global.UserName;
                txtDikaiologia.Text = "";
                txtValue.Text = txtFeesDiscountPercent.Text;
                txtDikaiologia.Focus();
            }
        }
        private void txtFeesDiscountAmount_DoubleClick(object sender, EventArgs e)
        {
            if (iFeesCalcMode == 2)
            {
                iFeesEditMode = 1;
                lblFeesTitle.Text = "% έκπτωσης προμήθειας";
                panFeesEdit.Visible = true;
                lblFeesEdit_Date.Text = DateTime.Now.ToString("d");
                lblFeesEdit_User.Text = Global.UserName;
                txtDikaiologia.Text = "";
                txtValue.Text = txtFeesDiscountPercent.Text;
                txtDikaiologia.Focus();
            }
        }
        private void txtFeesRate_LostFocus(object sender, EventArgs e)
        {
            //klsOrder.FeesRate = Convert.ToDecimal(txtFeesRate.Text);
            DefineComission();
        }
        private void lblFinishFeesPercent_DoubleClick(object sender, EventArgs e)
        {
            if (iFeesCalcMode == 2)
            {
                iFeesEditMode = 3;
                lblFeesTitle.Text = "% τελικής προμήθεια";
                panFeesEdit.Visible = true;
                lblFeesEdit_Date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lblFeesEdit_User.Text = Global.UserName;
                txtDikaiologia.Text = ""; ;
                txtValue.Text = lblFinishFeesPercent.Text;
                txtDikaiologia.Focus();
            }
        }

        private void lblFinishFeesAmount_DoubleClick(object sender, EventArgs e)
        {
            if (iFeesCalcMode == 2)
            {
                iFeesEditMode = 4;
                lblFeesTitle.Text = "Ποσό τελικής προμήθειας";
                panFeesEdit.Visible = true;
                lblFeesEdit_Date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lblFeesEdit_User.Text = Global.UserName;
                txtDikaiologia.Text = "";
                txtValue.Text = lblFinishFeesAmount.Text;
                txtDikaiologia.Focus();
            }
        }
        private void lblFinishTicketFeesAmount_DoubleClick(object sender, EventArgs e)
        {
            if (iFeesCalcMode == 2)
            {
                iFeesEditMode = 5;
                lblFeesTitle.Text = "Τελικό Ticket Fees";
                panFeesEdit.Visible = true;
                lblFeesEdit_Date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lblFeesEdit_User.Text = Global.UserName;
                txtDikaiologia.Text = "";
                txtValue.Text = lblFinishTicketFeesAmount.Text;
                txtDikaiologia.Focus();
            }
        }
        private void txtFeesCalc_LostFocus(object sender, EventArgs e)
        {
            if (iFeesCalcMode == 2)
            {
                iFeesEditMode = 6;
                lblFeesTitle.Text = "Τελική προμήθεια";
                panFeesEdit.Visible = true;
                lblFeesEdit_Date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lblFeesEdit_User.Text = Global.UserName;
                txtDikaiologia.Text = "";
                txtValue.Text = txtFeesAmountEUR.Text;
                txtDikaiologia.Focus();
            }
        }
        private void txtMinFeesDiscountPercent_LostFocus(object sender, EventArgs e)
        {

        }

        private void txtMinFeesDiscountAmount_LostFocus(object sender, EventArgs e)
        {

        }

        private void txtMinFeesRate_LostFocus(object sender, EventArgs e)
        {

        }

        private void txtMinFeesCalc_LostFocus(object sender, EventArgs e)
        {

        }

        private void txtFinsihFees_LostFocus(object sender, EventArgs e)
        {

        }

        private void txtTicketFeesDiscountPercent_LostFocus(object sender, EventArgs e)
        {

        }

        private void txtTicketFeesDiscountAmount_LostFocus(object sender, EventArgs e)
        {

        }

        private void txtTicketFeesRate_LostFocus(object sender, EventArgs e)
        {

        }
        private void txtTicketFeesCalc_LostFocus(object sender, EventArgs e)
        {

        }

        private void txtSumMiscFees_LostFocus(object sender, EventArgs e)
        {

        }

        private void txtProviderFees_LostFocus(object sender, EventArgs e)
        {

        }
        private void txtRTO_FeesDiscountPercent_LostFocus(object sender, EventArgs e)
        {
            txtRTO_FeesDiscountAmount.Text = (Convert.ToDouble(lblRTO_FeesAmount.Text) * Convert.ToDouble(txtRTO_FeesDiscountPercent.Text) / 100.0).ToString("0.##");
            CalcRTOComission();
        }
        private void txtRTO_FeesDiscountAmount_LostFocus(object sender, EventArgs e)
        {
            CalcRTOComission();
        }
        private void txtRTO_MinFeesDiscountPercent_LostFocus(object sender, EventArgs e)
        {
            txtRTO_MinFeesDiscountAmount.Text = (Convert.ToDouble(lblRTO_MinFeesAmount.Text) * Convert.ToDouble(txtRTO_MinFeesDiscountPercent.Text) / 100.0).ToString("0.##");
            lblRTO_FinishMinFeesAmount.Text = (Convert.ToDouble(lblRTO_MinFeesAmount.Text) - Convert.ToDouble(txtRTO_MinFeesDiscountAmount.Text)).ToString("0.##");
            CalcRTOComission();
        }

        private void txtNotes_LostFocus(object sender, EventArgs e)
        {
            txtNotes.Text = txtNotes.Text.Replace("\t", "");
        }
        private void txtRTO_MinFeesDiscountAmount_LostFocus(object sender, EventArgs e)
        {
            lblRTO_FinishMinFeesAmount.Text = (Convert.ToDouble(lblRTO_MinFeesAmount.Text) - Convert.ToDouble(txtRTO_MinFeesDiscountAmount.Text)).ToString("0.##");
            CalcRTOComission();
        }


        private void txtRTO_TicketFeesDiscountPercent_LostFocus(object sender, EventArgs e)
        {
            CalcRTOComission();
        }

        private void txtRTO_TicketFeesDiscountAmount_LostFocus(object sender, EventArgs e)
        {

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
        private void picAddCheck_Click(object sender, EventArgs e)
        {
            fgCheck.AddItem(DateTime.Now.ToString("dd/MM/yyyy") + "\t" + Global.UserName + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "0" + "\t" +
                            Global.User_ID + "\t" + "0" + "\t" + "" + "\t" + "0", 1);
        }

        private void btnCurrRate_Click(object sender, EventArgs e)
        {
            DefineCurrRate();
        }

        private void picDelCheck_Click(object sender, EventArgs e)
        {
            if (fgCheck.Row > 0)
            {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) ==
                    System.Windows.Forms.DialogResult.Yes)
                {
                    clsOrders_Check Orders_Check = new clsOrders_Check();
                    Orders_Check.Record_ID = Convert.ToInt32(fgCheck[fgCheck.Row, 7]);
                    Orders_Check.DeleteRecord();
                    fgCheck.RemoveItem(fgCheck.Row);
                }
            }
        }
        private void lnkShareTitle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmProductData locProductData = new frmProductData();
            locProductData.ShareCode_ID = iShare_ID;
            locProductData.Product_ID = iProduct_ID;
            locProductData.Text = Global.GetLabel("product");
            locProductData.Show();
        }

        private void cmbClients_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList)
               iClient_ID = Convert.ToInt32(cmbClients.SelectedValue);
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
                        Global.DMS_ShowFile("Customers/" + sSubPath + "/Informing", fgCheck[fgCheck.Row, 5].ToString());      //is DMS file, so show it into Web mode
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
        }

        private void picAccouningTrx_Click(object sender, EventArgs e)
        {
            sProvider_Code = "";
            iCurrency_ID = 0;

            foundRows = Global.dtServiceProviders.Select("ID = " + iServiceProvider_ID);
            if (foundRows.Length > 0) sProvider_Code = foundRows[0]["Alias"] + "";

            foundRows = Global.dtCurrencies.Select("Title = '" + lblCurr.Text + "'");
            if (foundRows.Length > 0) iCurrency_ID = Convert.ToInt32(foundRows[0]["ID"]);

            if (klsOrder.Aktion == 1)                   // 1 - BUY
            {
                RowTreatment(1, 1, dAktionDate.Value, iServiceProvider_ID, iContract_ID, lblContractTitle.Text + "", ucCS.txtContractTitle.Text, lblPortfolio.Text, 
                             iShare_ID, txtISIN.Text, iCurrency_ID, lblCurr.Text, Convert.ToSingle(txtRealAmount.Text), 0, "Z1/" + iRec_ID, "ΑΞΙΑ ΣΥΝΑΛΛΑΓΗΣ", 0, "", sProvider_Code, 0);

            }
            else if (klsOrder.Aktion == 2)                   // 2 - SELL
            {
                RowTreatment(1, 1, dAktionDate.Value, iServiceProvider_ID, iContract_ID, lblContractTitle.Text + "", ucCS.txtContractTitle.Text, lblPortfolio.Text,
                             iShare_ID, txtISIN.Text, iCurrency_ID, lblCurr.Text, 0, Convert.ToSingle(txtRealAmount.Text),  "Z1/" + iRec_ID, "ΑΞΙΑ ΣΥΝΑΛΛΑΓΗΣ", 0, "", sProvider_Code, 0);

                RowTreatment(1, 1, dAktionDate.Value, iServiceProvider_ID, iContract_ID, lblContractTitle.Text + "", ucCS.txtContractTitle.Text, lblPortfolio.Text,
                             iShare_ID, txtISIN.Text, iCurrency_ID, lblCurr.Text, 0, Convert.ToSingle(txtAccruedInterest.Text), "Z1/" + iRec_ID, "ΔΕΔΟΥΛΕΥΜΕΝΟΙ ΤΟΚΟΙ", 0, "", sProvider_Code, 0);

                RowTreatment(1, 1, dAktionDate.Value, iServiceProvider_ID, iContract_ID, lblContractTitle.Text + "", ucCS.txtContractTitle.Text, lblPortfolio.Text,
                             iShare_ID, txtISIN.Text, iCurrency_ID, lblCurr.Text, -1, 0, "Z1/" + iRec_ID, "ΠΡΟΜΗΘΕΙΑ ΣΥΝΑΛΛΑΓΗΣ", 0, "", sProvider_Code, 0);

                RowTreatment(1, 1, dAktionDate.Value, iServiceProvider_ID, iContract_ID, lblContractTitle.Text + "", ucCS.txtContractTitle.Text, lblPortfolio.Text,
                             iShare_ID, txtISIN.Text, iCurrency_ID, lblCurr.Text, -2, 0, "Z1/" + iRec_ID, "ΦΟΡΟΣ ΕΛΛΗΝΙΚΟΥ ΔΗΜΟΣΙΟΥ", 0, "", sProvider_Code, 0);

                RowTreatment(1, 1, dAktionDate.Value, iServiceProvider_ID, iContract_ID, lblContractTitle.Text + "", ucCS.txtContractTitle.Text, lblPortfolio.Text,
                             iShare_ID, txtISIN.Text, iCurrency_ID, lblCurr.Text, -3, 0, "Z1/" + iRec_ID, "ΜΕΤΑΒΙΒΑΣΤΙΚΑ", 0, "", sProvider_Code, 0);

                RowTreatment(1, 1, dAktionDate.Value, iServiceProvider_ID, iContract_ID, lblContractTitle.Text + "", ucCS.txtContractTitle.Text, lblPortfolio.Text,
                             iShare_ID, txtISIN.Text, iCurrency_ID, lblCurr.Text, -4, 0, "Z1/" + iRec_ID, "ΕΞΟΔΑ ΧΑ", 0, "", sProvider_Code, 0);

                //---------------------------------------

                RowTreatment(2, 1, dAktionDate.Value, iServiceProvider_ID, iContract_ID, lblContractTitle.Text + "", ucCS.txtContractTitle.Text, lblPortfolio.Text,
                             iShare_ID, txtISIN.Text, iCurrency_ID, lblCurr.Text, 0, -5, "Z1/" + iRec_ID, "HF ΕΣΟΔΑ ΣΥΝΑΛΛΑΓΩΝ", 1, "", sProvider_Code, 2);

                RowTreatment(2, 1, dAktionDate.Value, iServiceProvider_ID, iContract_ID, lblContractTitle.Text + "", ucCS.txtContractTitle.Text, lblPortfolio.Text,
                             iShare_ID, txtISIN.Text, iCurrency_ID, lblCurr.Text, Convert.ToSingle(txtRealAmount.Text), 0, "Z1/" + iRec_ID, "ΑΞΙΑ ΣΥΝΑΛΛΑΓΗΣ", 1, "", sProvider_Code, 2);

                RowTreatment(2, 1, dAktionDate.Value, iServiceProvider_ID, iContract_ID, lblContractTitle.Text + "", ucCS.txtContractTitle.Text, lblPortfolio.Text,
                             iShare_ID, txtISIN.Text, iCurrency_ID, lblCurr.Text, Convert.ToSingle(txtAccruedInterest.Text), 0, "Z1/" + iRec_ID, "ΔΕΔΟΥΛΕΥΜΕΝΟΙ ΤΟΚΟΙ", 1, "", sProvider_Code, 2);

                RowTreatment(2, 1, dAktionDate.Value, iServiceProvider_ID, iContract_ID, lblContractTitle.Text + "", ucCS.txtContractTitle.Text, lblPortfolio.Text,
                             iShare_ID, txtISIN.Text, iCurrency_ID, lblCurr.Text, 0, -6, "Z1/" + iRec_ID, "ΑΠΟΔΟΣΗ PIRSEC ΠΡΟΜΗΘΕΙΑ", 1, "", sProvider_Code, 2);

                RowTreatment(2, 1, dAktionDate.Value, iServiceProvider_ID, iContract_ID, lblContractTitle.Text + "", ucCS.txtContractTitle.Text, lblPortfolio.Text,
                             iShare_ID, txtISIN.Text, iCurrency_ID, lblCurr.Text, 0, -7, "Z1/" + iRec_ID, "ΑΠΟΔΟΣΗ PRISEC ΕΞΟΔΑ", 1, "", sProvider_Code, 2);

                RowTreatment(2, 1, dAktionDate.Value, iServiceProvider_ID, iContract_ID, lblContractTitle.Text + "", ucCS.txtContractTitle.Text, lblPortfolio.Text,
                             iShare_ID, txtISIN.Text, iCurrency_ID, lblCurr.Text, 0, -8, "Z1/" + iRec_ID, "ΑΠΟΔΟΣΗ ΦΟΡΟΥ ΤΟΚΟΥ ΟΜΟΛΟΓΩΝ", 1, "", sProvider_Code, 2);

                //------------------------------

                RowTreatment(2, 1, dAktionDate.Value, iServiceProvider_ID, iContract_ID, lblContractTitle.Text + "", ucCS.txtContractTitle.Text, lblPortfolio.Text,
                             iShare_ID, txtISIN.Text, iCurrency_ID, lblCurr.Text, Convert.ToSingle(txtRealAmount.Text), 0, "Z1/" + iRec_ID, "ΔΙΑΚΑΝΟΝΙΣΜΟΣ", 1, "", sProvider_Code, 0);

                RowTreatment(2, 1, dAktionDate.Value, iServiceProvider_ID, iContract_ID, lblContractTitle.Text + "", ucCS.txtContractTitle.Text, lblPortfolio.Text,
                             iShare_ID, txtISIN.Text, iCurrency_ID, lblCurr.Text, 0, -9, "Z1/" + iRec_ID, "ΔΙΑΚΑΝΟΝΙΣΜΟΣ", 1, "", sProvider_Code, 0);
               
                RowTreatment(2, 1, dAktionDate.Value, iServiceProvider_ID, iContract_ID, lblContractTitle.Text + "", ucCS.txtContractTitle.Text, lblPortfolio.Text,
                             iShare_ID, txtISIN.Text, iCurrency_ID, lblCurr.Text, 0, Convert.ToSingle(txtRealAmount.Text), "Z1/" + iRec_ID, "ΔΙΑΚΑΝΟΝΙΣΜΟΣ", 1, "", sProvider_Code, 2);

                RowTreatment(2, 1, dAktionDate.Value, iServiceProvider_ID, iContract_ID, lblContractTitle.Text + "", ucCS.txtContractTitle.Text, lblPortfolio.Text,
                             iShare_ID, txtISIN.Text, iCurrency_ID, lblCurr.Text, 0, Convert.ToSingle(txtAccruedInterest.Text), "Z1/" + iRec_ID, "ΔΙΑΚΑΝΟΝΙΣΜΟ", 1, "", sProvider_Code, 2);

                RowTreatment(2, 1, dAktionDate.Value, iServiceProvider_ID, iContract_ID, lblContractTitle.Text + "", ucCS.txtContractTitle.Text, lblPortfolio.Text,
                             iShare_ID, txtISIN.Text, iCurrency_ID, lblCurr.Text, -6, 0, "Z1/" + iRec_ID, "ΔΙΑΚΑΝΟΝΙΣΜΟ", 1, "", sProvider_Code, 2);

                RowTreatment(2, 1, dAktionDate.Value, iServiceProvider_ID, iContract_ID, lblContractTitle.Text + "", ucCS.txtContractTitle.Text, lblPortfolio.Text,
                             iShare_ID, txtISIN.Text, iCurrency_ID, lblCurr.Text, -7, 0, "Z1/" + iRec_ID, "ΔΙΑΚΑΝΟΝΙΣΜΟ", 1, "", sProvider_Code, 2);

                RowTreatment(2, 1, dAktionDate.Value, iServiceProvider_ID, iContract_ID, lblContractTitle.Text + "", ucCS.txtContractTitle.Text, lblPortfolio.Text,
                             iShare_ID, txtISIN.Text, iCurrency_ID, lblCurr.Text, -8, 0, "Z1/" + iRec_ID, "ΔΙΑΚΑΝΟΝΙΣΜΟ", 1, "", sProvider_Code, 2);
            }
        }
        private void RowTreatment(int iLog_ID, int iType_ID, DateTime dDateIns, int iServiceProvider_ID, int iContract_ID, string sContractTitle, string sCode, string sPortfolio, 
                                  int iShare_ID, string sISIN, int iCurrency_ID, string sCurrency, float fltDebitAmount, float fltCreditAmount, string sReferenceNo, string sDescription, 
                                  int iOwner_ID, string sDeposit_Code, string sProvider_Code, int iStatus)
        {
            string sTemp = "", sTitle = "";
            iGAP_ID = 0;
            clsGAP GAP = new clsGAP();
            sTemp = Global.CreateGAPCode(iLog_ID, iType_ID,sCode, sPortfolio, sISIN, sCurrency, iOwner_ID, sDeposit_Code, sProvider_Code, iStatus);
            GAP.Code = sTemp;
            GAP.GetRecord_Code();
            if (GAP.Record_ID == 0)
            {
                GAP = new clsGAP();
                GAP.L1 = iLog_ID;
                GAP.L2 = iType_ID;
                GAP.L3 = iContract_ID;
                GAP.L4 = iOwner_ID;
                GAP.L5 = iShare_ID;
                GAP.L6 = iCurrency_ID;
                GAP.L7 = iServiceProvider_ID;
                GAP.L8 = 0;
                GAP.L9 = 0;

                sTitle = "";
                if (iLog_ID == 1) {
                    sTitle = "ΧΑΡΤΟΦΥΛΑΚΙΟ/" + (iType_ID == 1 ? "ΧΡΗΜΑΤΑ" : "ΤΙΤΛΟΙ") + "/" + sCode + "/" + sPortfolio + "/" + sCurrency + "/" + sProvider_Code + "/" + 
                             (iStatus == 0 ? "AVAILABEL" : (iStatus == 1 ? "BLOCKED" : "PENDING"));
                }
                else
                {
                    sTitle = "HF OMNIBUS/" + (iType_ID == 1 ? "ΧΡΗΜΑΤΑ" : "ΤΙΤΛΟΙ") + "/" + (iOwner_ID == 1 ? "CLIENTS" : "OWN") + "/" + sCurrency + "/" + sProvider_Code + "/" + 
                            (iStatus == 0 ? "AVAILABEL" : (iStatus == 1 ? "BLOCKED" : "PENDING"));
                }
                GAP.Title = sTitle;
                GAP.Code = sTemp;
                iGAP_ID = GAP.InsertRecord();
            }
            else iGAP_ID = GAP.Record_ID;

            clsAccountingTrx AccountingTrx = new clsAccountingTrx();
            AccountingTrx.TrxDate = dDateIns;
            AccountingTrx.Valeur = dDateIns;
            AccountingTrx.DateIns = dDateIns;
            AccountingTrx.GAP_ID = iGAP_ID;
            AccountingTrx.Debit = fltDebitAmount;
            AccountingTrx.Credit = fltCreditAmount;
            AccountingTrx.ReferenceNo = sReferenceNo;
            AccountingTrx.Description = sDescription;
            AccountingTrx.InsertRecord();
        }
        private void picAddInform_Click(object sender, EventArgs e)
        {
            fgInforming.AddItem(DateTime.Now.ToString("ddd/MM/yyyy") + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "0" + "\t" + "0" + "\t" + Global.User_ID + "\t" + "", 1);
        }
        private void picDelInform_Click(object sender, EventArgs e)
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
                    Global.DMS_ShowFile("Customers/" + sSubPath + "/Informing", fgInforming[fgInforming.Row, 2] + "");
            }
        }
        private void picIssuedInvoice_Click(object sender, EventArgs e)
        {
            Global.DMS_ShowFile("Customers/" + sSubPath + "/Invoices", lblFileName.Text);
        }
        private void txtHistoryNotes_TextChanged(object sender, EventArgs e)
        {
            if (txtHistoryNotes.Text.Length > 0) btnOK_Save.Enabled = true;
            else btnOK_Save.Enabled = false;
        }
        private void btnOK_Save_Click(object sender, EventArgs e)
        {
            if (iMode == 1)
            {                           // 1 - Save & Exit,   2 - Show only
                SaveRecord();
                this.Close();
                iLastAktion = 1;                       // 1 - was saved (added)
            }
            else panNotes.Visible = false;
        }
        private void picQuestions_Click(object sender, EventArgs e)
        {
            panQuestions.Visible = true;
        }
        private void EmptyComiss()
        {
            lblFeesPercent.Text = "0";
            lblFeesAmount.Text = "0";
            txtFeesDiscountPercent.Text = "0";
            txtFeesDiscountAmount.Text = "0";
            lblFinishFeesPercent.Text = "0";
            lblFinishFeesAmount.Text = "0";
            //lblCompanyFeesPercent.Text = "0";
            //lblCompanyFeesAmount.Text = "0";
            //lblCompanyTicketFeesPercent.Text = "0";
            //lblCompanyTicketFeesAmount.Text = "0";
            lblFeesNotes.Text = "";
        }
        private void btnOK_FeesCalcMode_Click(object sender, EventArgs e)
        {
            iFeesCalcMode = 1;
            DefineComission();
            panFeesCalcMode.Visible = false;
        }
        private void btnCancel_FeesCalcMode_Click(object sender, EventArgs e)
        {
            panFeesCalcMode.Visible = false;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            panFeesEdit.Visible = false;
        }
        private void DefineCurrRate()
        {
            if (lblCurr.Text == "EUR") lblCurrRate.Text = "1";
            else
            {
                clsProductsCodes ProductCode = new clsProductsCodes();
                ProductCode.DateIns = dAktionDate.Value;
                ProductCode.Code = "EUR" + lblCurr.Text + "=";
                ProductCode.GetPrice_Code();
                if (ProductCode.DateIns.Date <= dAktionDate.Value.Date)
                    lblCurrRate.Text = ProductCode.LastClosePrice.ToString("0.####");
            }
            lblFeesRate.Text = "1";  // lblCurrRate.Text;
            lblRTO_FeesRate.Text = lblCurrRate.Text;
        }
        private void DefineComission()
        {
            if (bCheckList)
            {
                klsOrder.FeesCalcMode = iFeesCalcMode;
                klsOrder.RTO_FeesDiscountPercent = Convert.ToDecimal(txtRTO_FeesDiscountPercent.Text);
                klsOrder.RTO_MinFeesDiscountPercent = Convert.ToDecimal(txtRTO_MinFeesDiscountPercent.Text);
                klsOrder.CalcFees();

                //if (klsOrder.FeesRate != 0) txtFeesRate.Text = klsOrder.FeesRate.ToString("0.####");
                //else txtFeesRate.Text = lblCurrRate.Text;

                txtRealAmount.Text = klsOrder.RealAmount.ToString("0.##");
                if (txtRealAmount.Text != "" && txtAccruedInterest.Text != "")
                    lblInvestAmount.Text = (Convert.ToDouble(txtRealAmount.Text) + Convert.ToDouble(txtAccruedInterest.Text)).ToString("0.##");
            }
            ShowBlock6();
        }
        private void ShowBlock6()
        {
            lblFeesCurr.Text = klsOrder.Curr;
            lblFeesRate_Title.Text = "Ισοτιμία /" + klsOrder.Curr;
            lblSums.Text = "Ποσά σε " + klsOrder.Curr;
            lblFeesPercent.Text = klsOrder.FeesPercent.ToString("0.##");
            lblFeesAmount.Text = klsOrder.FeesAmount.ToString("0.##");
            txtFeesDiscountPercent.Text = klsOrder.FeesDiscountPercent.ToString("0.##");
            txtFeesDiscountAmount.Text = klsOrder.FeesDiscountAmount.ToString("0.##");
            lblFinishFeesPercent.Text = klsOrder.FinishFeesPercent.ToString("0.##");
            lblFinishFeesAmount.Text = klsOrder.FinishFeesAmount.ToString("0.##");
            lblFeesRate.Text = "1";
            txtFeesAmountEUR.Text = klsOrder.FeesAmountEUR.ToString("0.##");

            lblMinFeesCurr.Text = klsOrder.MinFeesCurr;
            lblMinFeesAmount.Text = klsOrder.MinFeesAmount.ToString("0.##");
            txtMinFeesDiscountPercent.Text = klsOrder.MinFeesDiscountPercent.ToString("0.##");
            txtMinFeesDiscountAmount.Text = klsOrder.MinFeesDiscountAmount.ToString("0.##");
            lblFinishMinFeesAmount.Text = klsOrder.FinishMinFeesAmount.ToString("0.##");
            txtMinFeesRate.Text = klsOrder.MinFeesRate.ToString("0.####");
            txtMinAmountEUR.Text = klsOrder.MinAmountEUR.ToString("0.##");

            lblTicketFeesCurr.Text = klsOrder.TicketFeeCurr;
            lblTicketFeesAmount.Text = klsOrder.TicketFee.ToString("0.##");
            txtTicketFeesDiscountPercent.Text = klsOrder.TicketFeeDiscountPercent.ToString("0.##");
            txtTicketFeesDiscountAmount.Text = klsOrder.TicketFeeDiscountAmount.ToString("0.##");
            lblFinishTicketFeesAmount.Text = klsOrder.FinishTicketFee.ToString("0.##");
            txtTicketFeesRate.Text = klsOrder.TicketFeesRate.ToString("0.####");
            txtTicketFeesAmountEUR.Text = klsOrder.TicketFeesAmountEUR.ToString("0.##");

            lblFeesNotes.Text = klsOrder.FeesNotes;


            if (klsOrder.FeesCalc >= Convert.ToDecimal(txtMinAmountEUR.Text)) txtFinsihFees.Text = klsOrder.FeesCalc.ToString("0.##");
            else txtFinsihFees.Text = txtMinAmountEUR.Text;

            txtProviderFees.Text = klsOrder.ProviderFees.ToString("0.##");

            lblRTO_FeesCurr.Text = klsOrder.Curr;
            lblRTO_FeesPercent.Text = klsOrder.RTO_FeesPercent.ToString("0.##");
            lblRTO_FeesAmount.Text = klsOrder.RTO_FeesAmount.ToString("0.##");
            //If txtRTO_FeesDiscountPercent.Text = "" Or txtRTO_FeesDiscountPercent.Text = "0");
            txtRTO_FeesDiscountPercent.Text = klsOrder.RTO_FeesDiscountPercent.ToString("0.##");
            txtRTO_FeesDiscountAmount.Text = klsOrder.RTO_FeesDiscountAmount.ToString("0.##");
            //End If
            lblRTO_FinishFeesPercent.Text = klsOrder.RTO_FinishFeesPercent.ToString("0.##");
            lblRTO_FinishFeesAmount.Text = klsOrder.RTO_FinishFeesAmount.ToString("0.##");
            lblRTO_FeesRate.Text = klsOrder.CurrRate.ToString("0.####");
            lblRTO_FeesAmountEUR.Text = klsOrder.RTO_FeesAmountEUR.ToString("0.##");

            lblRTO_MinFeesCurr.Text = klsOrder.RTO_MinFeesCurr;
            lblRTO_MinFeesAmount.Text = klsOrder.RTO_MinFeesAmount.ToString("0.##");
            txtRTO_MinFeesDiscountPercent.Text = klsOrder.RTO_MinFeesDiscountPercent.ToString("0.##");
            txtRTO_MinFeesDiscountAmount.Text = klsOrder.RTO_MinFeesDiscountAmount.ToString("0.##");
            lblRTO_FinishMinFeesAmount.Text = klsOrder.RTO_FinishMinFeesAmount.ToString("0.##");

            lblRTO_TicketFeesCurr.Text = klsOrder.RTO_TicketFeeCurr;
            lblRTO_TicketFeesAmount.Text = klsOrder.RTO_TicketFee.ToString("0.##");
            txtRTO_TicketFeesDiscountPercent.Text = klsOrder.RTO_TicketFeeDiscountPercent.ToString("0.##");
            txtRTO_TicketFeesDiscountAmount.Text = klsOrder.RTO_TicketFeeDiscountAmount.ToString("0.##");
            lblRTO_FinishTicketFeesAmount.Text = klsOrder.RTO_FinishTicketFee.ToString("0.##");

            lblRTO_FeesProVAT.Text = klsOrder.RTO_FeesProVAT.ToString("0.##");
            lblRTO_FeesVAT.Text = klsOrder.RTO_FeesVAT.ToString("0.##");
            lblRTO_CompanyFees.Text = klsOrder.RTO_CompanyFees.ToString("0.##");
        }
        private void CalcRTOComission()
        {
            lblRTO_FeesAmount.Text = (Convert.ToDouble(lblInvestAmount.Text) * Convert.ToDouble(lblRTO_FeesPercent.Text) / Convert.ToDouble(100)).ToString("0.##");
            lblRTO_FinishFeesAmount.Text = (Convert.ToDouble(lblRTO_FeesAmount.Text) - Convert.ToDouble(txtRTO_FeesDiscountAmount.Text)).ToString("0.##");
            lblRTO_FinishFeesPercent.Text = (Convert.ToDouble(lblRTO_FeesPercent.Text) * Convert.ToDouble(lblRTO_FinishFeesAmount.Text) / Convert.ToDouble(lblRTO_FeesAmount.Text)).ToString("0.##");
            lblRTO_FeesAmountEUR.Text = (Convert.ToDouble(lblRTO_FinishFeesAmount.Text) / Convert.ToDouble(lblRTO_FeesRate.Text)).ToString("0.##");

            txtRTO_TicketFeesDiscountAmount.Text = (Convert.ToDouble(lblRTO_TicketFeesAmount.Text) * Convert.ToDouble(txtRTO_TicketFeesDiscountPercent.Text) / 100).ToString("0.##");
            lblRTO_FinishTicketFeesAmount.Text = (Convert.ToDouble(lblRTO_TicketFeesAmount.Text) - Convert.ToDouble(txtRTO_TicketFeesDiscountAmount.Text)).ToString("0.##");

            if (Convert.ToDouble(lblRTO_FeesAmountEUR.Text) > Convert.ToDouble(lblRTO_FinishMinFeesAmount.Text))
                lblRTO_FeesProVAT.Text = (Convert.ToDouble(lblRTO_FeesAmountEUR.Text) + Convert.ToDouble(lblRTO_FinishTicketFeesAmount.Text)).ToString("0.##");
            else
                lblRTO_FeesProVAT.Text = (Convert.ToDouble(lblRTO_FinishMinFeesAmount.Text) + Convert.ToDouble(lblRTO_FinishTicketFeesAmount.Text)).ToString("0.##");

            lblRTO_CompanyFees.Text = (Convert.ToDouble(lblRTO_FeesProVAT.Text) + Convert.ToDouble(lblRTO_FeesVAT.Text)).ToString("0.##");
        }
        #endregion
        #region --- fgRecieved functions ----------------------------------------------------------------
        private void picAddRecieved_Click(object sender, EventArgs e)
        {
            fgRecieved.AddItem(Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy HH:mm:ss") + "\t" + "" + "\t" + "" + "\t" + "0" + "\t" + "0" + "\t" + "", 1);
            if (Convert.ToDateTime(dSend.Value) != Convert.ToDateTime("1900/01/01"))
                if (Convert.ToDateTime(dSend.Value) <= DateTime.Now)
                    MessageBox.Show("Wrong Date: Ημερομηνία Λήψης δεν μπορεί να είναι μεγαλίτερη απο Ημερομηνία Διαβίβασης", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void picDelRecieved_Click(object sender, EventArgs e)
        {
            if (fgRecieved.Row > 0)
            {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    clsOrders_Recieved Orders_Recieved = new clsOrders_Recieved();
                    Orders_Recieved.Record_ID = Convert.ToInt32(fgRecieved[fgRecieved.Row, 3]);
                    Orders_Recieved.DeleteRecord();
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
                Global.DMS_ShowFile("Customers/" + sSubPath + "/OrdersAcception", (fgRecieved[fgRecieved.Row, 2] + ""));
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
        private void fgCommands_ExecutionsDetails_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Col == 2 || e.Col == 3) 
                fgCommands_ExecutionsDetails[e.Row, 4] = Convert.ToDecimal(fgCommands_ExecutionsDetails[e.Row, 2]) * Convert.ToDecimal(fgCommands_ExecutionsDetails[e.Row, 3]);
        }        
        private void SaveRecord()
        {
            clsOrders_Recieved Orders_Recieved = new clsOrders_Recieved();
            clsOrders_Check Orders_Check = new clsOrders_Check();
            clsInformings Informings = new clsInformings();

            bContinue = true;

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
                    else
                    {
                        if (!Global.IsNumeric(txtRealPrice.Text) || txtRealPrice.Text.IndexOf(".") > 0)
                        {
                            bContinue = false;
                            txtRealPrice.BackColor = Color.Red;
                            txtRealPrice.Focus();
                        }
                        else
                        {
                            if (!Global.IsNumeric(txtRealQuantity.Text) || txtRealQuantity.Text.IndexOf(".") > 0)
                            {
                                bContinue = false;
                                txtRealQuantity.BackColor = Color.Red;
                                txtRealQuantity.Focus();
                            }
                            else
                            {
                                if (!Global.IsNumeric(txtRealAmount.Text) || txtRealAmount.Text.IndexOf(".") > 0)
                                {
                                    bContinue = false;
                                    txtRealQuantity.BackColor = Color.Red;
                                    txtRealQuantity.Focus();
                                }
                            }
                        }
                    }
                }
            }

            if (bContinue)
            {
                if (iRec_ID == 0)
                {
                    clsOrdersSecurity klsOrder2 = new clsOrdersSecurity();
                    klsOrder2.BulkCommand = "<" + iNewBulkCommand_ID + ">";
                    klsOrder2.BusinessType_ID = 1;
                    klsOrder2.CommandType_ID = 1;
                    klsOrder2.Client_ID = iClient_ID;
                    klsOrder2.Company_ID = Global.Company_ID;
                    klsOrder2.ServiceProvider_ID = iServiceProvider_ID;
                    klsOrder2.StockExchange_ID = iStockExchange_ID;
                    klsOrder2.CustodyProvider_ID = iServiceProvider_ID;
                    klsOrder2.Depository_ID = 0;
                    klsOrder2.II_ID = 0;
                    klsOrder2.Parent_ID = 0;
                    klsOrder2.AllocationPercent = 100;
                    klsOrder2.Contract_ID = iContract_ID;
                    klsOrder2.Share_ID = iShare_ID;
                    klsOrder2.Product_ID = iProduct_ID;
                    klsOrder2.ProductCategory_ID = iProductCategory_ID;
                    klsOrder2.PriceType = lstType.SelectedIndex;
                    klsOrder2.RecieveMethod_ID = 0;
                    klsOrder2.BestExecution = chkBestExecution.Checked ? 1 : 0;
                    dRecieved = Convert.ToDateTime("2070/12/31");
                    klsOrder2.SentDate = Convert.ToDateTime("01/01/1900");
                    klsOrder2.FIX_A = -1;
                    klsOrder2.ExecuteDate = Convert.ToDateTime("01/01/1900");
                    klsOrder2.RealPrice = 0;
                    klsOrder2.RealQuantity = 0;
                    klsOrder2.RealAmount = 0;
                    klsOrder2.User_ID = Global.User_ID;
                    klsOrder2.DateIns = DateTime.Now;
                    klsOrder2.RecieveDate = dRecieved;
                    iRec_ID = klsOrder2.InsertRecord();
                }

                //--- At begining systems saves of fgRecieved, fgInforming and fgCheck records, because  names of upload files can change. So in Command record will save new file names 
                for (i = 1; i <= fgRecieved.Rows.Count - 1; i++)
                {
                    sNewFileName = (fgRecieved[i, 2] + "").Trim();
                    if ((fgRecieved[i, 5] + "") != "")
                    {
                        sNewFileName = Global.DMS_UploadFile(fgRecieved[i, 5] + "", "Customers/" + sSubPath + "/OrdersAcception", sNewFileName);
                        if (sNewFileName.Length > 0) sNewFileName = Path.GetFileName(sNewFileName);
                        else
                            MessageBox.Show("Αρχείο " + fgRecieved[i, 2] + " δεν αντιγράφτηκε στο DMS", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    if (Convert.ToInt32(fgRecieved[i, 3]) == 0)
                    {
                        Orders_Recieved = new clsOrders_Recieved();
                        Orders_Recieved.Command_ID = iRec_ID;
                        Orders_Recieved.DateIns = Convert.ToDateTime(fgRecieved[i, 0]);
                        Orders_Recieved.Method_ID = Convert.ToInt32(fgRecieved[i, 4]);
                        Orders_Recieved.FilePath = fgRecieved[i, 5] + "";
                        Orders_Recieved.FileName = sNewFileName;
                        Orders_Recieved.SourceCommand_ID = iRec_ID;
                        Orders_Recieved.InsertRecord();
                    }
                    else
                    {
                        Orders_Recieved.Record_ID = Convert.ToInt32(fgRecieved[i, 3]);
                        Orders_Recieved.GetRecord();
                        Orders_Recieved.Command_ID = iRec_ID;
                        Orders_Recieved.DateIns = Convert.ToDateTime(fgRecieved[i, 0]);
                        Orders_Recieved.Method_ID = Convert.ToInt32(fgRecieved[i, 4]);
                        Orders_Recieved.FilePath = fgRecieved[i, 5] + "";
                        Orders_Recieved.FileName = sNewFileName;
                        Orders_Recieved.EditRecord();
                    }
                }


                for (i = 1; i <= fgCheck.Rows.Count - 1; i++)
                {

                    if ((fgCheck[i, "FileFullName"] + "").Trim() != "")
                    {                                     // FileFullName - Not Empty means that it's a new file
                        sTemp = Global.DMS_UploadFile(fgCheck[i, "FileFullName"] + "", "Customers/" + sSubPath + "/Informing", fgCheck[i, 5] + "");
                        fgCheck[i, 5] = Path.GetFileName(sTemp);
                    }

                    if (Convert.ToInt32(fgCheck[i, "ID"]) == 0)
                    {
                        Orders_Check = new clsOrders_Check();
                        Orders_Check.Command_ID = iRec_ID;
                        Orders_Check.DateIns = Convert.ToDateTime(fgCheck[i, "DateIns"]);
                        Orders_Check.User_ID = Convert.ToInt32(fgCheck[i, "User_ID"]);
                        Orders_Check.Status = Convert.ToInt32(fgCheck[i, "Status"]);
                        Orders_Check.ProblemType_ID = Convert.ToInt32(fgCheck[i, "ProblemType_ID"]);
                        Orders_Check.Notes = fgCheck[i, "Notes"] + "";
                        Orders_Check.FileName = fgCheck[i, 5] + "";
                        Orders_Check.ReversalRequestDate = fgCheck[i, "ReversalRequestMailed"] + "";
                        Orders_Check.InsertRecord();
                    }
                    else
                    {
                        Orders_Check.Record_ID = Convert.ToInt32(fgCheck[i, "ID"]);
                        Orders_Check.GetRecord();
                        Orders_Check.Command_ID = iRec_ID;
                        Orders_Check.DateIns = Convert.ToDateTime(fgCheck[i, "DateIns"]);
                        Orders_Check.User_ID = Convert.ToInt32(fgCheck[i, "User_ID"]);
                        Orders_Check.Status = Convert.ToInt32(fgCheck[i, "Status"]);
                        Orders_Check.ProblemType_ID = Convert.ToInt32(fgCheck[i, "ProblemType_ID"]);
                        Orders_Check.Notes = fgCheck[i, "Notes"] + "";
                        Orders_Check.FileName = fgCheck[i, 5] + "";
                        Orders_Check.ReversalRequestDate = fgCheck[i, "ReversalRequestMailed"] + "";
                        Orders_Check.EditRecord();
                    }
                }

                for (i = 1; i <= fgInforming.Rows.Count - 1; i++)
                {
                    if ((fgInforming[i, 7] + "").Trim() != "")
                    {  // Not Empty means that it's a new file

                        sTemp = Global.DMS_UploadFile(fgInforming[i, 7] + "", "Customers/" + sSubPath + "/Informing", fgInforming[i, 2] + "");
                        fgInforming[i, 2] = Path.GetFileName(sTemp);


                        if ((fgInforming[i, 4] + "") == "0")
                            Global.AddInformingRecord(1, iRec_ID, Convert.ToInt32(fgInforming[i, 5]), 5, klsOrder.Client_ID, iContract_ID, "", "",
                                               Global.GetLabel("update_execution_command"), "", fgInforming[i, 2] + "", "", DateTime.Now.ToString(), 1, 1, "");
                        else
                        {
                            Informings.Record_ID = Convert.ToInt32(fgInforming[i, 4]);
                            Informings.InformMethod = Convert.ToInt32(fgInforming[i, 5]);
                            Informings.DateIns = Convert.ToDateTime(fgInforming[i, 0]);
                            Informings.EditRecord();
                        }
                    }
                }

                iLocProvider_ID = 0;
                CommandsExecutionsDetails = new clsCommandsExecutionsDetails();
                CommandsExecutionsDetails.Command_ID = iRec_ID;
                CommandsExecutionsDetails.GetList();
                foreach (DataRow dtRow in CommandsExecutionsDetails.List.Rows)
                      iLocProvider_ID = Convert.ToInt32(dtRow["StockCompany_ID"] + "");

                if (iLocProvider_ID == 0)
                {
                    // --- define iLocProvider_ID from Execution order ---------------- !!!!!!!!!!!!!!!
                    iLocProvider_ID = iServiceProvider_ID;
                }

                CommandsExecutionsDetails = new clsCommandsExecutionsDetails();
                CommandsExecutionsDetails.Command_ID = iRec_ID;
                CommandsExecutionsDetails.DeleteRecord_Command_ID();

                for (i = 1; i <= fgCommands_ExecutionsDetails.Rows.Count - 1; i++)
                {
                    CommandsExecutionsDetails = new clsCommandsExecutionsDetails();
                    CommandsExecutionsDetails.Command_ID = iRec_ID;
                    CommandsExecutionsDetails.CommandExecution_ID = 0;
                    CommandsExecutionsDetails.CurrentTimestamp = Convert.ToDateTime(fgCommands_ExecutionsDetails[i, "DateAktion"]);
                    CommandsExecutionsDetails.SecondOrdID = fgCommands_ExecutionsDetails[i, "RefNumber"] + "";
                    CommandsExecutionsDetails.StockExchange_ID = Convert.ToInt32(fgCommands_ExecutionsDetails[i, "StockExchange_ID"]);
                    CommandsExecutionsDetails.StockCompany_ID = iLocProvider_ID;
                    CommandsExecutionsDetails.Price = Convert.ToDecimal(fgCommands_ExecutionsDetails[i, "Price"]);
                    CommandsExecutionsDetails.Quantity = (Convert.ToDecimal(fgCommands_ExecutionsDetails[i, "Quantity"]));
                    CommandsExecutionsDetails.InsertRecord();
                }

                //--- Edit Command ----------------------------------

                klsOrder.Record_ID = iRec_ID;
                klsOrder.Code = ucCS.txtContractTitle.Text;
                klsOrder.ProfitCenter = lblPortfolio.Text;
                klsOrder.Client_ID = iClient_ID;
                klsOrder.Aktion = (txtAction.Text == "BUY" ? 1 : 2);
                klsOrder.AktionDate = dAktionDate.Value;
                klsOrder.ProductCategory_ID = iProductCategory_ID;
                klsOrder.PriceType = lstType.SelectedIndex;
                klsOrder.Price = Convert.ToDecimal(txtPrice.Text);
                klsOrder.Quantity = Convert.ToDecimal(txtQuantity.Text);
                klsOrder.Amount = Convert.ToDecimal(txtAmount.Text);
                klsOrder.Curr = lblCurr.Text;
                klsOrder.Constant = cmbConstant.SelectedIndex;
                klsOrder.ConstantDate = (cmbConstant.SelectedIndex == 2 ? dConstant.Value.ToString("dd/MM/yyyy") : "");
                klsOrder.RealPrice = Convert.ToDecimal(txtRealPrice.Text);
                klsOrder.RealQuantity = Convert.ToDecimal(txtRealQuantity.Text);
                klsOrder.RealAmount = Convert.ToDecimal(txtRealAmount.Text);
                klsOrder.AccruedInterest = Convert.ToDecimal(txtAccruedInterest.Text);
                i = 0;
                sTemp = "";
                if (fgCheck.Rows.Count > 1)
                {
                    i = Convert.ToInt32(fgCheck[1, 9]);                      // Status
                    sTemp = fgCheck[1, 5] + "";
                }
                klsOrder.Pinakidio = i;
                klsOrder.LastCheckFile = sTemp;

                klsOrder.FeesPercent = Convert.ToDecimal(lblFeesPercent.Text);
                klsOrder.FeesAmount = Convert.ToDecimal(lblFeesAmount.Text);
                klsOrder.FeesDiscountPercent = Convert.ToDecimal(txtFeesDiscountPercent.Text);
                klsOrder.FeesDiscountAmount = Convert.ToDecimal(txtFeesDiscountAmount.Text);
                klsOrder.FinishFeesPercent = Convert.ToDecimal(lblFinishFeesPercent.Text);
                klsOrder.FinishFeesAmount = Convert.ToDecimal(lblFinishFeesAmount.Text);
                //klsOrder.FeesRate = Convert.ToDecimal(txtFeesRate.Text);
                //klsOrder.FeesAmountEUR = Convert.ToDecimal(lblFeesAmountEUR.Text);

                klsOrder.MinFeesAmount = Convert.ToDecimal(lblMinFeesAmount.Text);
                klsOrder.MinFeesDiscountPercent = Convert.ToDecimal(txtMinFeesDiscountPercent.Text);
                klsOrder.MinFeesDiscountAmount = Convert.ToDecimal(txtMinFeesDiscountAmount.Text);
                klsOrder.FinishMinFeesAmount = Convert.ToDecimal(lblFinishMinFeesAmount.Text);

                klsOrder.TicketFeeCurr = lblTicketFeesCurr.Text;
                klsOrder.TicketFee = Convert.ToDecimal(lblTicketFeesAmount.Text);
                klsOrder.TicketFeeDiscountPercent = Convert.ToDecimal(txtTicketFeesDiscountPercent.Text);
                klsOrder.TicketFeeDiscountAmount = Convert.ToDecimal(txtTicketFeesDiscountAmount.Text);
                klsOrder.FinishTicketFee = Convert.ToDecimal(lblFinishTicketFeesAmount.Text);

                klsOrder.RTO_FeesDiscountPercent = Convert.ToDecimal(txtRTO_FeesDiscountPercent.Text);
                klsOrder.RTO_FeesDiscountAmount = Convert.ToDecimal(txtRTO_FeesDiscountAmount.Text);
                klsOrder.RTO_FinishFeesPercent = Convert.ToDecimal(lblRTO_FinishFeesPercent.Text);
                klsOrder.RTO_FinishFeesAmount = Convert.ToDecimal(lblRTO_FinishFeesAmount.Text);
                //klsOrder.RTO_FeesRate = Convert.ToDecimal(lblRTO_FeesRate.Text);
                klsOrder.RTO_FeesAmountEUR = Convert.ToDecimal(lblRTO_FeesAmountEUR.Text);

                klsOrder.RTO_MinFeesDiscountPercent = Convert.ToDecimal(txtRTO_MinFeesDiscountPercent.Text);
                klsOrder.RTO_MinFeesDiscountAmount = Convert.ToDecimal(txtRTO_MinFeesDiscountAmount.Text);
                klsOrder.RTO_FinishMinFeesAmount = Convert.ToDecimal(lblRTO_FinishMinFeesAmount.Text);

                klsOrder.RTO_TicketFee = Convert.ToDecimal(lblTicketFeesAmount.Text);
                klsOrder.RTO_TicketFeeCurr = lblRTO_TicketFeesCurr.Text;
                klsOrder.RTO_TicketFeeDiscountPercent = Convert.ToDecimal(txtRTO_TicketFeesDiscountPercent.Text);
                klsOrder.RTO_TicketFeeDiscountAmount = Convert.ToDecimal(txtRTO_TicketFeesDiscountAmount.Text);
                klsOrder.RTO_FinishTicketFee = Convert.ToDecimal(lblRTO_FinishTicketFeesAmount.Text);

                klsOrder.RTO_FeesProVAT = Convert.ToDecimal(lblRTO_FeesProVAT.Text);
                klsOrder.RTO_FeesVAT = Convert.ToDecimal(lblRTO_FeesVAT.Text);
                klsOrder.RTO_CompanyFees = Convert.ToDecimal(lblRTO_CompanyFees.Text);
                klsOrder.FeesNotes = lblFeesNotes.Text;

                klsOrder.FeesCalc = Convert.ToDecimal(txtFeesAmountEUR.Text);

                klsOrder.CurrRate = Convert.ToDecimal(lblCurrRate.Text != "" ? lblCurrRate.Text : "0");
                klsOrder.CompanyFeesPercent = Convert.ToDecimal(sgCompanyFeesPercent);
                dTemp = dRecieved;
                i = 0;
                if (fgRecieved.Rows.Count > 1)
                {
                    dTemp = Convert.ToDateTime(fgRecieved[1, 0]);   //   last recieved file date
                    i = Convert.ToInt32(fgRecieved[1, 4]);          //   last recieved file method
                }

                klsOrder.RecieveDate = dTemp;
                klsOrder.RecieveMethod_ID = i;
                klsOrder.BestExecution = chkBestExecution.Checked ? 1 : 0;

                if (dSend.Text.Trim() != "")
                {
                    dTemp = Convert.ToDateTime(dSend.Text.Trim());
                    sTemp = dTemp.ToString("yyyy-MM-dd") + " " + (txtSendHour.Text.Trim() == "" ? "00" : txtSendHour.Text.Trim()) + ":" +
                                                                 (txtSendMinute.Text.Trim() == "" ? "00" : txtSendMinute.Text.Trim()) + ":" +
                                                                 (txtSendSecond.Text.Trim() == "" ? "00" : txtSendSecond.Text.Trim());
                }
                else sTemp = "1900/01/01 00:00:00";

                klsOrder.SentDate = Convert.ToDateTime(sTemp);
                klsOrder.SendCheck = (cbChecked.Checked ? 1 : 0);

                dTemp = dExecute.Value;
                sTemp = dTemp.ToString("yyyy-MM-dd") + " " + (txtExecuteHour.Text.Trim() == "" ? "00" : txtExecuteHour.Text.Trim()) + ":" +
                                                             (txtExecuteMinute.Text.Trim() == "" ? "00" : txtExecuteMinute.Text.Trim()) + ":" +
                                                             (txtExecuteSecond.Text.Trim() == "" ? "00" : txtExecuteSecond.Text.Trim());

                klsOrder.ExecuteDate = Convert.ToDateTime(sTemp);
                klsOrder.SettlementDate = dSettlement.Value;
                i = 0;
                if (fgInforming.Rows.Count > 1) i = Convert.ToInt32(fgInforming[1, 5]);
                klsOrder.InformationMethod_ID = i;
                klsOrder.Notes = txtNotes.Text;
                klsOrder.FeesCalcMode = iFeesCalcMode;
                klsOrder.User_ID = Convert.ToInt32(cmbSenders.SelectedValue);
                klsOrder.EditRecord();

                if (bPressedKey)
                {
                    //--- Add History Record ---
                    clsHistory klsHistory = new clsHistory();
                    klsHistory.RecType = 10;
                    klsHistory.SrcRec_ID = iRec_ID;
                    klsHistory.Client_ID = 0;
                    klsHistory.Contract_ID = 0;
                    klsHistory.Action = 0;
                    klsHistory.CurrentValues = txtCurrentValues.Text;
                    klsHistory.DocFiles_ID = 0;
                    klsHistory.Notes = txtHistoryNotes.Text;
                    klsHistory.User_ID = Global.User_ID;
                    klsHistory.DateIns = DateTime.Now;
                    klsHistory.InsertRecord();
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            iFeesCalcMode = 2;               //   1 - Automatic Calculation Mode, 2 - Manually Calculation Mode

            switch (iFeesEditMode)
            {
                case 1:
                    txtFeesDiscountPercent.Text = txtValue.Text;
                    txtFeesDiscountAmount.Text = (Convert.ToDouble(lblFeesAmount.Text) * Convert.ToDouble(txtFeesDiscountPercent.Text) / 100).ToString("0.####");

                    lblFinishFeesAmount.Text = (Convert.ToDouble(lblFeesAmount.Text) - Convert.ToDouble(txtFeesDiscountAmount.Text)).ToString("0.####");
                    lblFinishFeesPercent.Text = (Convert.ToDouble(lblFinishFeesAmount.Text) * 100 / Convert.ToDouble(txtRealAmount.Text)).ToString("0.##");
                    break;
                case 2:
                    txtFeesDiscountAmount.Text = txtValue.Text;
                    txtFeesDiscountPercent.Text = (Convert.ToDouble(txtFeesDiscountAmount.Text) / Convert.ToDouble(lblFeesAmount.Text)).ToString("0.##");

                    lblFinishFeesAmount.Text = (Convert.ToDouble(lblFeesAmount.Text) - Convert.ToDouble(txtFeesDiscountAmount.Text)).ToString("0.####");
                    lblFinishFeesPercent.Text = (Convert.ToDouble(lblFinishFeesAmount.Text) * 100 / Convert.ToDouble(txtRealAmount.Text)).ToString("0.##");
                    txtFeesDiscountPercent.Text = (Convert.ToDouble(txtFeesDiscountAmount.Text) * 100 / Convert.ToDouble(lblFeesAmount.Text)).ToString("0.##");
                    break;
                case 3:
                    lblFinishFeesPercent.Text = txtValue.Text;
                    lblFinishFeesAmount.Text = (Convert.ToDouble(lblFinishFeesPercent.Text) * Convert.ToDouble(txtRealAmount.Text) / 100).ToString("0.####");

                    txtFeesDiscountAmount.Text = (Convert.ToDouble(lblFeesAmount.Text) - Convert.ToDouble(lblFinishFeesAmount.Text)).ToString("0.####");
                    txtFeesDiscountPercent.Text = (Convert.ToDouble(txtFeesDiscountAmount.Text) * 100 / Convert.ToDouble(lblFeesAmount.Text)).ToString("0.##");
                    break;
                case 4:
                    lblFinishFeesPercent.Text = txtValue.Text;

                    if (Convert.ToDouble(lblFinishFeesPercent.Text) != 0)
                        lblFinishFeesPercent.Text = (Convert.ToDouble(lblFinishFeesAmount.Text) * 100 / Convert.ToDouble(txtRealAmount.Text)).ToString("0.##");

                    if (Convert.ToDouble(txtFeesDiscountAmount.Text) != 0)
                        txtFeesDiscountAmount.Text = (Convert.ToDouble(lblFeesAmount.Text) - Convert.ToDouble(lblFinishFeesAmount.Text)).ToString("0.####");

                    if (Convert.ToDouble(txtFeesDiscountPercent.Text) != 0)
                        txtFeesDiscountPercent.Text = (Convert.ToDouble(txtFeesDiscountAmount.Text) * 100 / Convert.ToDouble(lblFeesAmount.Text)).ToString("0.##");

                    //lblFinishFeesPercent.Text = FormatNumber(Convert.ToDouble(lblFinishFeesAmount.Text) * 100 / Convert.ToDouble(txtRealAmount.Text), 2, TriState.true)

                    //txtFeesDiscountAmount.Text = FormatNumber(Convert.ToDouble(lblFeesAmount.Text) - Convert.ToDouble(lblFinishFeesAmount.Text), 4, TriState.true)
                    //txtFeesDiscountPercent.Text = FormatNumber(txtFeesDiscountAmount.Text * 100 / Convert.ToDouble(lblFeesAmount.Text), 2, TriState.true)
                    break;
                case 5:
                    lblFinishTicketFeesAmount.Text = txtValue.Text;
                    //lblCompanyTicketFeesAmount.Text = FormatNumber(Convert.ToDouble(lblFinishTicketFeesAmount.Text) * Convert.ToDouble(lblCompanyTicketFeesPercent.Text) / 100, 2, TriState.true)
                    break;
                case 6:
                    //txtFeesCalc.Text = txtValue.Text
                    break;
            }

            if (iFeesEditMode != 6)
            {
                //--- Define Minimum Fees --------------------
                decTemp = Convert.ToDecimal(lblFinishFeesAmount.Text);
                if (Convert.ToDouble(lblMinFeesAmount.Text) != 0)
                {
                    if (lblMinFeesCurr.Text == lblFeesCurr.Text)
                        if (decTemp < Convert.ToDecimal(lblMinFeesAmount.Text)) decTemp = Convert.ToDecimal(lblMinFeesAmount.Text);
                        else
                        {
                            decTemp2 = (decimal)Global.ConvertAmount(Convert.ToDecimal(lblMinFeesAmount.Text), lblMinFeesCurr.Text, lblFeesCurr.Text, dExecute.Value);
                            if (decTemp < decTemp2) decTemp = decTemp2;
                        }
                }
                txtFeesAmountEUR.Text = decTemp.ToString();
            }

            //--- Add History Record ---
            clsHistory klsHistory = new clsHistory();
            klsHistory.RecType = 10;
            klsHistory.SrcRec_ID = iRec_ID;
            klsHistory.Client_ID = 0;
            klsHistory.Contract_ID = 0;
            klsHistory.Action = 0;
            klsHistory.CurrentValues = txtDikaiologia.Text;
            klsHistory.DocFiles_ID = 0;
            klsHistory.Notes = txtHistoryNotes.Text;
            klsHistory.User_ID = Global.User_ID;
            klsHistory.DateIns = DateTime.Now;
            klsHistory.InsertRecord();

            panFeesEdit.Visible = false;
        }
        protected void ucCS_TextChanged(object sender, EventArgs e)
        {
            Global.ContractData stContract = new Global.ContractData();
            stContract = ucCS.SelectedContractData;
            lblContractTitle.Text = stContract.ContractTitle;
            lblPortfolio.Text = stContract.Portfolio;
            klsOrder.CFP_ID = stContract.Contracts_Packages_ID;
            lblStockCompany.Text = stContract.Provider_Title;
            iClient_ID = stContract.Client_ID;
            iContract_ID = stContract.Contract_ID;
            iServiceProvider_ID = stContract.Provider_ID;
            iClientTipos = stContract.ClientType;
            iMIFIDCategory_ID = stContract.MIFIDCategory_ID;
            iMIFID_Risk_Index = stContract.MIFID_Risk_Index;
            iMIFID_2 = stContract.MIFID_2;
            iXAA = stContract.XAA;
            klsOrder.Client_ID = stContract.Client_ID;
            klsOrder.Contract_ID = stContract.Contract_ID;
            klsOrder.ServiceProvider_ID = stContract.Provider_ID;

            txtAction.Focus();

            DefineComission();
        }
        protected void ucPS_TextChanged(object sender, EventArgs e)
        {
            Global.ProductData stProduct = new Global.ProductData();
            stProduct = ucPS.SelectedProductData;
            iShare_ID = stProduct.ShareCode_ID;
            iSE_ID = stProduct.StockExchange_ID;
            sTemp = "";
            if (txtAction.Text == "BUY") sTemp = Global.CheckCompatibility(iContract_ID, iMIFID_2, iMIFIDCategory_ID, iXAA, iShare_ID, iSE_ID);
            if (sTemp.Length == 0)
            {
                txtISIN.Text = stProduct.ISIN;
                lnkShareTitle.Text = stProduct.Title;
                lblProduct.Text = stProduct.Product_Title;
                iProductCategory_ID = stProduct.ProductCategory_ID;
                lblProductCategory.Text = stProduct.Product_Category;
                lblProductStockExchange_Title.Text = stProduct.StockExchange_Code;
                lblCurr.Text = stProduct.Currency;
            }
            else
            {
                MessageBox.Show(sTemp, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ucPS.Focus();
            }
        }

        public int Rec_ID { get { return this.iRec_ID; } set { this.iRec_ID = value; } }
        public int Mode { get { return this.iMode; } set { this.iMode = value; } }                                  // IN: 0 - Edit Mode, 2 - SecuritiesCheck mode     OUT: 1 - Save & Exit,   2 - Show only
        public int BusinessType { get { return this.iBusinessType; } set { this.iBusinessType = value; } }
        public int LastAktion { get { return this.iLastAktion; } set { this.iLastAktion = value; } }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public int Editable { get { return this.iEditable; } set { this.iEditable = value; } }
        public int NewBulkCommand_ID { get { return this.iNewBulkCommand_ID; } set { this.iNewBulkCommand_ID = value; } }
        public string NewAktion { get { return this.sNewAktion; } set { this.sNewAktion = value; } }
        public int NewShare_ID { get { return this.iNewShare_ID; } set { this.iNewShare_ID = value; } }
        public int NewPriceType { get { return this.iNewPriceType; } set { this.iNewPriceType = value; } }
        public string NewPrice { get { return this.sNewPrice; } set { this.sNewPrice = value; } }
        public int NewConstant { get { return this.iNewConstant; } set { this.iNewConstant = value; } }
        public DateTime NewConstantDate { get { return this.dNewConstantDate; } set { this.dNewConstantDate = value; } }
    }
}
