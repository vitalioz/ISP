using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using C1.Win.C1FlexGrid;
using Tulpep.NotificationWindow;
using Core;

namespace Core
{
    public struct Attaches
    {
        public int Share_ID;
        public int Rec_ID;
        public int DocType_ID;
        public string DocType_Title;
        public string FileName;
        public string FullFilePath;
        public string ServerFileName;
        public string UploadFilePath;
        public string RemoteFileName;
        public int WasEdited;
    }
    public partial class frmInvestProposal : Form
    {
        DataTable dtList, dtMails, dtList4, dtEURRates;
        DataView dtView;
        DataRow dtRow;
        DataColumn dtCol;
        DataRow[] foundRows;

        int i, iAktion, iRec_ID, iShareType, iContract_ID, iContract_Details_ID, iContract_Packages_ID, iClient_ID, iProfile_ID, iInvestPolicy_ID, iMIFIDCategory_ID,
            iAdvisor_ID, iStockCompany_ID, iCodeAktion, iLine_Status, iStatus, iCustomerRecord_ID, iProductCategory_ID, iShare_ID, iMiFID_Risk, iComplexProduct,
            iAttachedFilesCount, iUploadedFilesCount, iRemotedFilesCount;
        float sgEndektikiTimi, sgGravity, sgPrice, sgQuantity, sgAmount, sgCurRate;
        string sProducts, sProviderTitle, sCostBenefits, sCostBenefits_Monetary, sCostBenefits_NonMonetary, sGeography, sAdvisor, sAdvisorEMail, sAdvisorTel, sAdvisorMobile,
               sStatementFile, sOldStatementFile, sFilePath, sPDF_FileName;
        string[] sEnergia = { "", "Αγορά", "Πώληση", "Εγγραφή", "Εξαγορά", "Διακράτηση" };
        string[] sPriceType = { "Limit", "Market", "Stop loss", "Scenario", "ATC", "ATO" };
        string[] sConstant = { "Day Order", "GTC", "GTDate" };
        bool bCheckShares, bCheckMandatoryFiles, bWasEdit, bWasSaved, bCheckSurname, bBlockedEditing, bCBA;
        DateTime dSentDate, dRTODate;
        Attaches rAtts;
        List<Attaches> stAtts = new List<Attaches>(); //  структура Attaches для хранения всех вложенных файлов, кроме PDF (это файлы-описания продуктов, файл statement, файлы телефонных разговоров):  
        //  Share_ID   - ShareCodes.ID продукта - если Share_ID > 0, то эта запись относится к продукту с ID = Share_ID; если  Share_ID, то это либо StatementFile либо CALL File; если Share_ID = -999, то это строка на удаление
        //  Rec_ID     - InvestIdees_Attachments.ID 
        //  DocType_ID - ID типа документа. Используется только для обязательных файлов. Если файл не обязательный, то DocType_ID = 0 или
        //               DocType_ID = -1 - для Statement файла, или DocType_ID = -2 - для файла телефонного разговора. Файлы с DocType_ID < 0 не загружаются на удаленный сервер                                                                                                               
        //  DocType_Title - название типа обязательного  документа
        //  FileName   - название исходного вложенного файла. Только название файла. Может измениться при загрузке, если на сервере есть такой файл
        //               Если оно пусто, то файл еще не загружался
        //  FullFilePath - полный путь исходного вложенного файла откуда он загружался. Название файла не меняется. Если он пуст, то файл еще не загружался 
        //  ServerFileName - название вложенного файла, загруженного на локальный сервер. Только название файла. 
        //               Это название  не равно FileName. Оно должно быть уникальным во всей системе. Поэтому это название формируется системой
        //               по такой формуле InvestIdees.ID + "_" + ShareCodes.ID + "_" + stAtts[j].Rec_ID 
        //               Если название пусто, то файл еще не загружался на локальный сервер
        //  UploadFilePath - полный путь вложенного файла куда он загрузился на сервер. Название файла может измениться при загрузке.
        //               Если этот путь пуст, то файл на сервер еще не загружался. Такое возможно в течение текущего сеанса   
        //  RemoteFilePath - название вложенного файла, загруженного на удаленный сервер. Только название файла.
        //  WasEdited  - флаг редактирования: = 1 если это новая запись, или была изменена, или была отмечена на удаление ; 0 - не изменялась 
        CellStyle csWarning;
        Point position;
        bool pMove;
        #region --- Start -------------------------------------------------
        public frmInvestProposal()
        {
            InitializeComponent();

            this.Width = 1064;
            this.Height = 792;

            panCode.Left = 120;
            panCode.Top = 180;

            panHeader.Top = 40;
            panHeader.Left = 8;

            panEpilogesBuy.Left = 108;
            panEpilogesBuy.Top = 222;

            panEpilogesSell.Left = 108;
            panEpilogesSell.Top = 222;
        }
        private void frmInvestProposal_Load(object sender, EventArgs e)
        {
            this.Text = "Επενδυτική Πρόταση";
            if (iRec_ID != 0) this.Text = this.Text + " (" + iRec_ID + ")";

            bCheckSurname = false;
            bCheckShares = false;
            bBlockedEditing = false;
            bWasSaved = false;
            bCheckMandatoryFiles = false;
            bWasEdit = false;
            bCBA = false;

            panProposal.Enabled = false;
            grpAttaches.Enabled = false;
            grpNotes.Enabled = false;

            iStatus = 0;
            iClient_ID = 0;
            iContract_ID = 0;
            iProfile_ID = 0;
            iInvestPolicy_ID = 0;
            iCustomerRecord_ID = 0;
            iShareType = 0;
            iProductCategory_ID = 0;
            iShare_ID = 0;
            iCodeAktion = 0;
            iMiFID_Risk = 0;
            iComplexProduct = 0;
            iStockCompany_ID = 0;
            sgEndektikiTimi = 1;
            sgGravity = 0;
            sgCurRate = 1;
            sProducts = "";
            sProviderTitle = "";
            sCostBenefits = "";
            sCostBenefits_Monetary = "";
            sCostBenefits_NonMonetary = "";
            sGeography = "00000";
            dSentDate = Convert.ToDateTime("1900/01/01");
            dRTODate = Convert.ToDateTime("1900/01/01");
            stAtts = new List<Attaches>();

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
            dtCol = dtList4.Columns.Add("Aktive", System.Type.GetType("System.Int16"));

            ucCS.StartInit(700, 400, 540, 20, 1);
            ucCS.TextOfLabelChanged += new EventHandler(ucCS_TextOfLabelChanged);
            ucCS.Filters = "Status = 1";
            ucCS.ListType = 1;

            ucPS.StartInit(700, 400, 200, 20, 1);
            ucPS.TextOfLabelChanged += new EventHandler(ucPS_TextChanged);
            ucPS.Filters = "Aktive = 1 ";
            ucPS.ListType = 2;                                                                  // iListType = 2 : dtProductsContract - list of products for current contract
            ucPS.ShowNonAccord = true;                                                          // Show NonAccordable products (oxi katallila) with red Background
            ucPS.ShowCancelled = false;                                                         // Don't show cancelled products
            ucPS.ProductsContract = dtList4;

            //-------------- Define StockExcahnges  List ------------------
            cmbStockExchanges.DataSource = Global.dtStockExchanges.Copy();
            cmbStockExchanges.DisplayMember = "Code";
            cmbStockExchanges.ValueMember = "ID";

            //-------------- Define Currencies List ------------------
            cmbCurrency.DataSource = Global.dtCurrencies.Copy();
            cmbCurrency.DisplayMember = "Title";
            cmbCurrency.ValueMember = "ID";

            //-------------- Define CC List ------------------
            dtMails = Global.dtUserList.Copy();
            foreach (DataRow dtRow in dtMails.Rows)
                if (Convert.ToInt32(dtRow["ID"]) == 0) {
                    dtRow["Title"] = "-";      // <----  "-"
                    dtRow["Aktive"] = 1;
                }

            dtView = dtMails.Copy().DefaultView;
            dtView.RowFilter = "Aktive = 1";
            cmbCC.DataSource = dtView;
            cmbCC.DisplayMember = "Title";
            cmbCC.ValueMember = "ID";

            //-------------- Define Products List ------------------
            //dtView = Global.dtProductTypes.Copy().DefaultView;
            //dtView.RowFilter = "Aktive = 1";
            cmbProducts.DataSource = Global.dtProductTypes.Copy().DefaultView;
            cmbProducts.DisplayMember = "Title";
            cmbProducts.ValueMember = "ID";

            //-------------- Define Information Methods List ------------------
            dtView = Global.dtInformMethods.Copy().DefaultView;
            dtView.RowFilter = "UseInvestIdees = 1";
            cmbInformMethods.DataSource = dtView;
            cmbInformMethods.DisplayMember = "Title";
            cmbInformMethods.ValueMember = "ID";

            DefineCustomerView(false);

            //-------------- Define InvestIdees Description List ------------------
            dtList = new DataTable("List");
            dtCol = dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dtCol = dtList.Columns.Add("Title", System.Type.GetType("System.String"));

            dtRow = dtList.NewRow();
            dtRow["ID"] = 0;
            dtRow["Title"] = "";
            dtList.Rows.Add(dtRow);


            //------- fgCodes ----------------------------
            fgCodes.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgCodes.Styles.ParseString(Global.GridStyle);
            fgCodes.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.fgCodes_CellChanged);

            csWarning = fgCodes.Styles.Add("Warning");
            csWarning.BackColor = Color.Yellow;

            //------- fgCalls ----------------------------
            fgCalls.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgCalls.Styles.ParseString(Global.GridStyle);
            fgCalls.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(fgCalls_CellButtonClick);
            fgCalls.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgCalls_BeforeEdit);

            Column col0 = fgCalls.Cols[0];
            col0.Name = "Image";
            col0.DataType = typeof(string);
            col0.ComboList = "...";

            //------- fgCodesAttaches ----------------------------
            fgCodesAttaches.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgCodesAttaches.Styles.ParseString(Global.GridStyle);

            //------- fgCodesMandatoryAttaches ----------------------------
            fgCodesMandatoryAttaches.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgCodesMandatoryAttaches.Styles.ParseString(Global.GridStyle);
            fgCodesMandatoryAttaches.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(fgCodesMandatoryAttaches_CellButtonClick);

            Column col11 = fgCodesMandatoryAttaches.Cols[1];
            col11.Name = "Image";
            col11.DataType = typeof(string);
            col11.ComboList = "...";

            if (iAktion == 0) {
                // --- NEW RECORD. IT'S EDITABLE ---------------------
                dSend.Value = DateTime.Now;
                lblUserName.Text = Global.UserName;
                bBlockedEditing = false;
                sOldStatementFile = "";

                SwitchOnOffHeader(true);
                ucCS.Enabled = true;
                fgCalls.AllowEditing = true;

                SwitchOnOffButtons(true);
                tsbSave.Enabled = true;
                tslNewEdit.Enabled = false;
            }
            else {
                //--- EDIT RECORD. Editability depends of some conditions (see at bottom of this ELSE block -------

                //--- Define InvestIdees head data ------------------
                clsInvestIdees klsInvestIdees = new clsInvestIdees();
                klsInvestIdees.Record_ID = iRec_ID;
                klsInvestIdees.GetRecord();

                dSend.Value = klsInvestIdees.AktionDate;
                txtIdeasText.Text = klsInvestIdees.IdeasText;
                sCostBenefits = klsInvestIdees.CostBenefits;
                if (sCostBenefits.Length > 0) {
                    string[] tokens = sCostBenefits.Split('~');
                    chkM1.Checked = tokens[0] == "1" ? true : false;
                    chkM2.Checked = tokens[1] == "1" ? true : false;
                    chkM3.Checked = tokens[2] == "1" ? true : false;
                    chkM4.Checked = tokens[3] == "1" ? true : false;
                    chkM5.Checked = tokens[4] == "1" ? true : false;
                    chkM6.Checked = tokens[5] == "1" ? true : false;
                    chkM7.Checked = tokens[6] == "1" ? true : false;
                    chkM8.Checked = tokens[7] == "1" ? true : false;
                    chkN1.Checked = tokens[8] == "1" ? true : false;
                    chkN2.Checked = tokens[9] == "1" ? true : false;
                    chkN3.Checked = tokens[10] == "1" ? true : false;
                    chkN4.Checked = tokens[11] == "1" ? true : false;
                    chkN5.Checked = tokens[12] == "1" ? true : false;
                    chkN6.Checked = tokens[13] == "1" ? true : false;
                    chkN7.Checked = tokens[14] == "1" ? true : false;
                    chkN8.Checked = tokens[15] == "1" ? true : false;
                    chkN9.Checked = tokens[16] == "1" ? true : false;
                    chkN10.Checked = tokens[17] == "1" ? true : false;
                    chkN11.Checked = tokens[18] == "1" ? true : false;
                    chkN12.Checked = tokens[19] == "1" ? true : false;
                    chkN13.Checked = tokens[20] == "1" ? true : false;
                    chkN14.Checked = tokens[21] == "1" ? true : false;
                    chkN15.Checked = tokens[22] == "1" ? true : false;
                    chkN16.Checked = tokens[23] == "1" ? true : false;
                    chkN17.Checked = tokens[24] == "1" ? true : false;
                    chkN18.Checked = tokens[25] == "1" ? true : false;
                    chkN19.Checked = tokens[26] == "1" ? true : false;
                }

                cmbCC.SelectedValue = klsInvestIdees.CC_ID;
                lblCC_Email.Text = klsInvestIdees.CC_EMail;
                cmbInformMethods.SelectedValue = klsInvestIdees.SendMethod;
                if (Convert.ToInt32(cmbInformMethods.SelectedValue) == 1) {
                    panCalls.Visible = true;
                    picShowCall.Visible = true;
                }
                else {
                    panCalls.Visible = false;
                    picShowCall.Visible = false;
                }
                txtAUM.Text = klsInvestIdees.AUM.ToString("0.00");
                lblCurrency.Text = klsInvestIdees.Currency;
                lblAdvisorName.Text = klsInvestIdees.AdvisorName;
                lblUserName.Text = klsInvestIdees.UserName;
                iLine_Status = klsInvestIdees.LineStatus;                

                if (klsInvestIdees.AUM != 0) {
                    panProposal.Enabled = true;
                    grpAttaches.Enabled = true;
                    grpNotes.Enabled = true;
                }

                dSentDate = klsInvestIdees.SentDate;
                dRTODate = klsInvestIdees.RTODate;
                lnkStatement.Text = klsInvestIdees.StatementFile;
                sOldStatementFile = klsInvestIdees.StatementFile;
                lnkPDF.Text = klsInvestIdees.ProposalPDFile;
                txtNotes.Text = klsInvestIdees.Notes + "";

                //--- Define InvestIdees Customers data ------------------
                clsInvestIdees_Customers InvestIdees_Customers = new clsInvestIdees_Customers();
                InvestIdees_Customers.II_ID = iRec_ID;
                InvestIdees_Customers.GetRecord();
                foreach (DataRow dtRow in InvestIdees_Customers.List.Rows) {
                    iCustomerRecord_ID = Convert.ToInt32(dtRow["ID"]);
                    iClient_ID = Convert.ToInt32(dtRow["Client_ID"]);
                    iContract_ID = Convert.ToInt32(dtRow["Contract_ID"]);
                    iContract_Details_ID = Convert.ToInt32(dtRow["Contract_Details_ID"]);
                    iContract_Packages_ID = Convert.ToInt32(dtRow["Contract_Packages_ID"]);
                    ucCS.ShowClientsList = false;
                    ucCS.txtContractTitle.Text = dtRow["ContractTitle"] + "";
                    ucCS.ShowClientsList = true;
                    iMIFIDCategory_ID = Convert.ToInt32(dtRow["MIFIDCategory_ID"]);
                    lblClientName.Text = dtRow["ClientFullName"] + ""; 
                    lblClientCode.Text = dtRow["Code"] + "";
                    lblPortfolio.Text = dtRow["Portfolio"] + "";
                    lblEMail.Text = dtRow["EMail"] + "";
                    lblMobile.Text = dtRow["Mobile"] + "";
                    iStockCompany_ID = Global.IsNumeric(Convert.ToInt32(dtRow["StockCompany_ID"])) ? Convert.ToInt32(dtRow["StockCompany_ID"]) : 0;
                    iAdvisor_ID = Global.IsNumeric(Convert.ToInt32(dtRow["Advisor_ID"])) ? Convert.ToInt32(dtRow["Advisor_ID"]) : 0;
                    sAdvisor = dtRow["AdvisorName"] + "";
                    sAdvisorEMail = dtRow["AdvisorEMail"] + "";
                    sAdvisorTel = dtRow["AdvisorTel"] + "";
                    sAdvisorMobile = dtRow["AdvisorMobile"] + "";
                    lblClientCategory.Text = Convert.ToInt32(dtRow["MIFIDCategory_ID"]) == 1 ? "Ιδιώτης Πελάτης" : (Convert.ToInt32(dtRow["MIFIDCategory_ID"]) == 2 ? "Επαγγελματίας Πελάτης" : "");
                }

                sGeography = DefineContractGeography(iContract_ID);

                DefineComplexProduct();

                //--- Define InvestPolicy ------------------
                clsContracts klsContract = new clsContracts();
                klsContract.Record_ID = iContract_ID;
                klsContract.Contract_Details_ID = iContract_Details_ID;
                klsContract.Contract_Packages_ID = iContract_Packages_ID;
                klsContract.GetRecord();
                lblEProfile.Text = klsContract.ProfileTitle;
                iProfile_ID = klsContract.Packages.Profile_ID;
                iMiFID_Risk = klsContract.MiFID_Risk;
                chkXAA.Checked = klsContract.XAA == 1 ? true : false;
                chkWorld.Checked = klsContract.Details.ChkWorld == 1 ? true : false;
                chkGreece.Checked = klsContract.Details.ChkGreece == 1 ? true : false;
                chkEurope.Checked = klsContract.Details.ChkEurope == 1 ? true : false;
                chkAmerica.Checked = klsContract.Details.ChkAmerica == 1 ? true : false;
                chkAsia.Checked = klsContract.Details.ChkAsia == 1 ? true : false;

                switch (klsContract.Packages.Service_ID) {
                    case 2:
                        iInvestPolicy_ID = klsContract.Details.InvestmentPolicy_ID;
                        lblEP.Text = klsContract.Details.InvestmentPolicy_Title;
                        lblService.Text = "Επενδυτικές Συμβουλές";
                        sProviderTitle = klsContract.BrokerageServiceProvider_Title;
                        break;
                    case 5:
                        iInvestPolicy_ID = klsContract.Details.InvestmentPolicy_ID;
                        lblEP.Text = klsContract.Details.InvestmentPolicy_Title;
                        lblService.Text = "Dealing Advisory";
                        sProviderTitle = klsContract.DealAdvisoryServiceProvider_Title;
                        break;
                }

                dtList4.Rows.Clear();
                Global.DefineContractProductsList(dtList4, iContract_ID, iContract_Details_ID, iContract_Packages_ID, false);
                dtList4.DefaultView.Sort = "CodeTitle";
                dtList4 = dtList4.DefaultView.ToTable();  

                //--- Define InvestIdees Products data ------------------
                fgCodes.Rows.Count = 1;

                clsInvestIdees_Products InvestIdees_Products = new clsInvestIdees_Products();
                InvestIdees_Products.II_ID = iRec_ID;
                InvestIdees_Products.GetList();
                foreach (DataRow dtRow in InvestIdees_Products.List.Rows)
                {
                    fgCodes.AddItem((Convert.ToInt32(dtRow["Aktion"]) == 1 ? "BUY" : "SELL") + "\t" + dtRow["Title"] + "\t" + dtRow["Code"] + "\t" +
                                        dtRow["Code2"] + "\t" + dtRow["ISIN"] + "\t" + dtRow["Curr"] + "\t" + dtRow["StockExchange_Title"] + "\t" +
                                        sConstant[Convert.ToInt32(dtRow["Constant"])] + "\t" +
                                        (Convert.ToInt32(dtRow["Type"]) == 0 ? dtRow["Price"] : sPriceType[Convert.ToInt32(dtRow["Type"])]) + "\t" +
                                        dtRow["Quantity"] + "\t" + dtRow["Amount"] + "\t" + dtRow["Weight"] + "\t" + dtRow["AttachFiles"] + "\t" +
                                        dtRow["ID"] + "\t" + dtRow["ShareCodes_ID"] + "\t" + dtRow["Product_ID"] + "\t" + dtRow["ProductCategories_ID"] + "\t" +
                                        dtRow["StockExchange_ID"] + "\t" + dtRow["Type"] + "\t" + dtRow["PriceUp"] + "\t" +
                                        dtRow["PriceDown"] + "\t" + dtRow["StockExchange_FullTitle"] + "\t" + dtRow["Constant"] + "\t" +
                                        dtRow["ConstantDate"] + "\t" + dtRow["LineStatus"] + "\t" + dtRow["Energia"] + "\t" + dtRow["Notes"] + "\t" + dtRow["URL_IR"]);
                }

                //--- Define InvestIdees ALL Attachments data ------------------
                DefineAttachFilesList();

                if (dSentDate != Convert.ToDateTime("1900/01/01")) {       // Or (cmbInformMethods.SelectedValue = 1 And fgCalls.Rows.Count > 1 And fgAttaches(2, 1) != "")) {
                    // --- was sent. Can't do nothing----------
                    bBlockedEditing = true;

                    SwitchOnOffHeader(false);
                    ucCS.Enabled = false;
                    fgCalls.AllowEditing = false;

                    SwitchOnOffButtons(false);

                    tsbSave.Enabled = false;
                    tslNewEdit.Enabled = false;
                }
                else {
                    if (lnkPDF.Text != "") {                                                                  // ProposalPDFile exists
                        // --- wasn't sent, but PDF was created. Can edit after deblocking ----------
                        bBlockedEditing = false;
                        SwitchOnOffHeader(false);
                        ucCS.Enabled = false;
                        fgCalls.AllowEditing = false;
                        SwitchOnOffButtons(false);
                        tsbSave.Enabled = false;
                        tslNewEdit.Enabled = true;
                    }
                    else {
                        // --- wasn't sent, PDF wasn't created. Can edit 
                        bBlockedEditing = false;
                        SwitchOnOffHeader(true);
                        ucCS.Enabled = true;
                        fgCalls.AllowEditing = true;
                        SwitchOnOffButtons(true);
                        tsbSave.Enabled = true;
                        tslNewEdit.Enabled = false;
                    }
                }
            }

            bCheckSurname = true;
            bCheckShares = true;
        }
        protected override void OnResize(EventArgs e)
        {
        }
        private void DefineAttachFilesList()
        {   // данный блок вызываеется при каждом активном действии в данной форме. Смысл этого в том, чтобы постоянно отслеживать загружаемость AttachedFiles на удаленный сервер
            // iAttachedFilesCount - общее количество AttachedFiles
            // iRemotedFilesCount  - количество загруженных на удаленный сервер AttachedFiles

            iAttachedFilesCount = 0;
            iRemotedFilesCount = 0;
            i = -1;
            
            fgCalls.Redraw = false;
            fgCalls.Rows.Count = 1;

            clsInvestIdees_Attachments klsInvestIdees_Attachments = new clsInvestIdees_Attachments();
            klsInvestIdees_Attachments.II_ID = iRec_ID;
            klsInvestIdees_Attachments.GetList();
            foreach (DataRow dtRow in klsInvestIdees_Attachments.List.Rows)
            {
                if (Convert.ToInt32(dtRow["Share_ID"]) > 0) {
                    iAttachedFilesCount = iAttachedFilesCount + 1;
                    if ((dtRow["RemoteFileName"] + "") != "") iRemotedFilesCount = iRemotedFilesCount + 1;
                }
                else {  // Share_ID = 0 means that it STATEMENT (DocType_ID=-1) or CALL(DocType_ID=-2 !!! < 0 - means that this file mustn't upload on remote server)
                    switch (dtRow["DocType_ID"])
                    {
                        case -1:
                            sStatementFile = dtRow["UploadFilePath"] + "";
                            lnkStatement.Text = dtRow["ServerFileName"] + "";
                            sOldStatementFile = dtRow["ServerFileName"] + "";
                            break;
                        case -2:
                            fgCalls.AddItem(dtRow["FileName"] + "\t" + dtRow["ID"] + "\t" + dtRow["FileFullPath"] + "\t" + dtRow["ServerFileName"] + "\t" + dtRow["UploadFilePath"]);
                            break;
                    }
                }
      
                i = i + 1;
                stAtts.Insert(i, new Attaches
                {
                    Rec_ID = Convert.ToInt32(dtRow["ID"]),
                    Share_ID = Convert.ToInt32(dtRow["Share_ID"]),
                    DocType_Title = dtRow["DocType_Title"] + "",
                    DocType_ID = Convert.ToInt32(dtRow["DocType_ID"]),
                    FileName = dtRow["FileName"] + "",
                    FullFilePath = dtRow["FileFullPath"] + "",
                    ServerFileName = dtRow["ServerFileName"] + "",
                    UploadFilePath = dtRow["UploadFilePath"] + "",
                    RemoteFileName = dtRow["RemoteFileName"] + "",
                    WasEdited = 0
                });
            }
            fgCalls.Redraw = true;

            lblAttFilesStatistics.Text = iAttachedFilesCount + " / " + iRemotedFilesCount;
        }

        private void frmInvestProposal_Closing(object sender, EventArgs e)
        {
            if (bWasEdit)
                if (MessageBox.Show("Η πρόταση σας δεν έχει αποθηκευτεί. Θέλετε να την Αποθηκεύσετε;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                }
        }
        #endregion

        #region ---- Header functions ------------------------------------------------
        private void picDown_Click(object sender, EventArgs e)
        {
            DefineCustomerView(true);
        }
        private void picUp_Click(object sender, EventArgs e)
        {
            DefineCustomerView(false);
        }
        private void dSend_ValueChanged(object sender, EventArgs e)
        {
            clsCurrencies klsCurrency = new clsCurrencies();
            klsCurrency.DateFrom = dSend.Value.AddDays(-5);
            klsCurrency.DateTo = dSend.Value;
            klsCurrency.Code = "EUR";
            klsCurrency.GetCurrencyRates_Period();
            dtEURRates = klsCurrency.List.Copy();
            ucCS.Filters = "Status = 1 AND (Service_ID = 2 OR Service_ID = 5) AND (Package_DateStart <= '" + dSend.Value + "' AND Package_DateFinish >= '" + dSend.Value + "') ";
        }
        private void picClient_Clean_Click(object sender, EventArgs e)
        {
            ucCS.ShowClientsList = false;
            ucCS.txtContractTitle.Text = "";          // client name
            ucCS.ShowClientsList = true;
            lblClientName.Text = "";                  // ClientName
            lblClientCode.Text = "";                  // client code
            lblPortfolio.Text = "";                   // client SubAccount
            lblEP.Text = "";                          // Ependitiki politiki
            iProfile_ID = 0;                          // Ependitiki profile ID
            lblEProfile.Text = "";                    // Ependitiki profile
            lblService.Text = "";                     // Service
            iClient_ID = 0;
            iStockCompany_ID = 0;
            iContract_ID = 0;
            iInvestPolicy_ID = 0;                     // investment policy
            lblEMail.Text = "";                       // eMail
            lblMobile.Text = "";                      // mobile
            txtAUM.Text = "";
            lblCurrency.Text = "";
            sGeography = "";
            chkWorld.Checked = false;
            chkGreece.Checked = false;
            chkEurope.Checked = false;
            chkAmerica.Checked = false;
            chkAsia.Checked = false;
        }
        private void txtAUM_LostFocus(object sender, EventArgs e)
        {
            if (!Global.IsNumeric(txtAUM.Text)) txtAUM.Text = "0";
            txtAUM.Text = Convert.ToDecimal(txtAUM.Text).ToString("0.00");
            CheckAUM();
        }
        private void cmbCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bCheckSurname) {
                if (Convert.ToInt32(cmbCC.SelectedValue) == 0) 
                    lblCC_Email.Text = "";
                else {
                    foundRows = Global.dtUserList.Select("ID=" + cmbCC.SelectedValue);
                    if (foundRows.Length > 0) lblCC_Email.Text = foundRows[0]["EMail"] + "";     
                }
            }
        }
        private void cmbInformMethods_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckSurname) {
                if (Convert.ToInt32(cmbInformMethods.SelectedValue) == 1) {
                    panCalls.Visible = true;
                    picShowCall.Visible = true;
                    DefineCustomerView(true);
                }
                else {
                    panCalls.Visible = false;
                    picShowCall.Visible = false;
                    DefineCustomerView(false);
                }
            }
        }
        private void picAddCall_Click(object sender, EventArgs e)
        {
            sFilePath = Global.FileChoice();
            fgCalls.AddItem(Path.GetFileName(sFilePath) + "\t" + "0" + "\t" + sFilePath + "\t" + "" + "\t" + "");
        }

        private void picDelCall_Click(object sender, EventArgs e)
        {
            if (fgCalls.Row > 0)
                if (DeleteTableRecord("InvestIdees_Attachments", Convert.ToInt32(fgCalls[fgCalls.Row, "ID"]))) 
                    fgCalls.RemoveItem(fgCalls.Row);
        }

        private void picShowCall_Click(object sender, EventArgs e)
        {
            if (fgCalls.Row > 0) {
                if ((fgCalls[fgCalls.Row, "FileName"] + "").Trim() != "") {
                    if ((fgCalls[fgCalls.Row, "FileFullPath"] + "") != "")
                        System.Diagnostics.Process.Start(fgCalls[fgCalls.Row, "FileFullPath"] + "");
                    else
                        Global.DMS_ShowFile(Global.DocFilesPath_HTTP + "/Customers/" + ucCS.txtContractTitle.Text + "/InvestProposals/" + iRec_ID, fgCalls[fgCalls.Row, "FileName"]+"");
                }
            }
        }
        private void tslNewEdit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ΠΡΟΣΟΧΗ!!! Για την τροποποίηση στοιχείων της Επενδυτικης Πρότασης πρέπει να διαγραφεί το αρχείο Πρόταση PDF.\n" +
                                "Είστε σίγουρος για τη διαγραφή του;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes) {

                lnkPDF.Text = "";

                clsInvestIdees klsInvestIdees = new clsInvestIdees();
                klsInvestIdees.Record_ID = iRec_ID;
                klsInvestIdees.GetRecord();
                klsInvestIdees.ProposalPDFile = "";
                klsInvestIdees.EditRecord();

                bBlockedEditing = false;

                SwitchOnOffHeader(true);
                fgCalls.AllowEditing = true;

                SwitchOnOffButtons(true);

                tsbSave.Enabled = true;
                tslNewEdit.Enabled = false;
            }
        }
        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (CheckAUM()) {
                SaveInvestmentProposal();
                if (bWasSaved) {
                    bWasEdit = false;
                    this.Close();
                }
            }
        }
        #endregion
        #region --- fgList functions (records List) -------------------------------------------------
        private void tabRecs_SelectedIndexChanged(Object sender, EventArgs e)
        {
            switch (tabRecs.TabPages[tabRecs.SelectedIndex].Name)
            {
                case "tpProducts":
                    break;
                case "tpRights":
                    break;
                case "tpIPO":
                    break;
                case "tpCBA":
                    break;
                case "tpNotes":
                    break;
                case "tpUpload":
                    ShowUploadFiles();
                    break;
            }
            DefineAttachFilesList();
        }
        private void tsbCodeAdd_Click(object sender, EventArgs e)
        {
            if (CheckAUM()) {
                SaveTitle();

                ucPS.Filters = "Aktive = 1";
                bCheckShares = false;
                chkRights.Visible = false;
                iCodeAktion = 0;
                iLine_Status = 0;
                txtAction.Text = "";
                txtPrice.Text = "0";
                txtQuantity.Text = "0";
                txtAmount.Text = "0";
                txtWeight.Text = "0";
                txtURL_IR.Text = "";

                EmptyCodeRec();

                fgCodesMandatoryAttaches.Rows.Count = 1;
                fgCodesAttaches.Rows.Count = 1;

                panMandatoryAttaches.Visible = false;
                bCheckMandatoryFiles = false;
                panCode.Top = 100;
                panCode.Visible = true;
                btnSave.Visible = true;
                btnSave.Enabled = false;
                btnCancel.Visible = true;
                btnCancel.Enabled = true;
                picBondCalc.Visible = false;

                bCheckShares = true;
                bWasEdit = true;

                txtAction.Focus();
                panCodeDetails.Enabled = true;
            }
        }
        private void tsbCodeEdit_Click(object sender, EventArgs e)
        {
            EditProduct();
        }
        private void fgCodes_DoubleClick(object sender, EventArgs e)
        {
            EditProduct();
        }
        private void fgCodes_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Col == 24)
                if (Convert.ToInt32(fgCodes[e.Row, e.Col]) == 0)
                   fgCodes.Rows[e.Row].Style = csWarning;
        }
        private void lstType_SelectedValueChanged(object sender, EventArgs e)
        {
            lblPrice.Visible = true;
            txtPrice.Visible = true;
            txtPrice.Text = "0";

            lblAmount.Visible = true;
            txtAmount.Visible = true;
            txtAmount.Text = "0";

            lblQuantity.Visible = true;
            txtQuantity.Visible = true;
            txtQuantity.Text = "0";

            imgPriceUp.Visible = false;
            txtPriceUp.Visible = false;
            imgPriceDown.Visible = false;
            txtPriceDown.Visible = false;

            switch (lstType.SelectedValue) {
                case 0:                                         // Limit
                    switch (cmbProducts.SelectedValue) {
                        case 1:
                        case 4:
                            if (txtAction.Text == "SELL") {
                                lblAmount.Visible = false;
                                txtAmount.Visible = false;
                            }
                            break;
                        case 2:                                          // Bond
                            if (txtAction.Text == "SELL") {
                                lblAmount.Visible = false;
                                txtAmount.Visible = false;
                            }
                            break;
                    }
                    break;
                case 1:                                        // Market
                    switch (cmbProducts.SelectedValue)
                    {
                        case 2:                                         // Bond
                            lblPrice.Visible = false;
                            txtPrice.Visible = false;
                            lblAmount.Visible = false;
                            txtAmount.Visible = false;
                            break;
                        case 6:                                         // AK
                            lblPrice.Visible = false;
                            txtPrice.Visible = false;
                            if (txtAction.Text == "BUY")
                            {
                                lblQuantity.Visible = false;
                                txtQuantity.Visible = false;
                            }
                            break;
                    }
                    break;
                case 2:                                        // Stop
                    switch (cmbProducts.SelectedValue) {
                        case 1:
                        case 4:
                            if (txtAction.Text == "SELL") {
                                lblAmount.Visible = false;
                                txtAmount.Visible = false;
                            }
                            break;
                    }
                    break;
                case 3:                                        // Scenario
                    cmbConstant.SelectedIndex = 1;

                    if (txtAction.Text == "BUY") {
                        imgPriceUp.Visible = true;
                        txtPriceUp.Visible = true;
                        imgPriceDown.Visible = true;
                        txtPriceDown.Visible = true;
                    }
                    else {
                        imgPriceUp.Visible = false;
                        txtPriceUp.Visible = false;
                        imgPriceDown.Visible = true;
                        txtPriceDown.Visible = true;
                    }
                    break;
                case 4:
                case 5:                       // ATC, ATO
                    lblPrice.Visible = false;
                    txtPrice.Visible = false;
                    lblAmount.Visible = false;
                    txtAmount.Visible = false;
                    break;
            }
        }
        private void fgList_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Col == 24)
            {
                if (Convert.ToInt32(fgCodes[e.Row, e.Col]) == 0)
                    fgCodes.Rows[e.Row].Style = csWarning;
            }
        }
        private void tsbCodeDelete_Click(object sender, EventArgs e)
        {
            if (fgCodes.Row > 0) {
                if (DeleteTableRecord("InvestIdees_Products", Convert.ToInt32(fgCodes[fgCodes.Row, 13]))) {
                    fgCodes.RemoveItem(fgCodes.Row);
                    DefineAttachFilesList();
                    bWasEdit = true;
                }
            }
        }
        private void picCopy2Clipboard_Click(object sender, EventArgs e)
        {
            if (!Convert.IsDBNull(Clipboard.GetText())) Clipboard.SetText(lblISIN.Text + "");
        }
        private void picAddStatement_Click(object sender, EventArgs e)
        {
            sStatementFile = Global.FileChoice();
            lnkStatement.Text = Path.GetFileName(sStatementFile);
            sOldStatementFile = "";                                         // empty sOldStatementFile means that Statement file was changed and after Save sStatementFile must be uploaded
            DefineAttachFilesList();
        }

        private void lnkStatement_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lnkStatement.Text != "")
                Global.DMS_ShowFile("Customers/" + ucCS.txtContractTitle.Text + "/InvestProposals/" + iRec_ID, lnkStatement.Text);
        }
        private void picCleanStatement_Click(object sender, EventArgs e)
        {
            sStatementFile = "";
            lnkStatement.Text = "";
        }
        private void picAddPDF_Click(object sender, EventArgs e)
        {
            string sMessage = "";
            clsServerJobs klsServerJob = new clsServerJobs();
            klsServerJob.DateStart = dSend.Value;
            klsServerJob.DateFinish = dSend.Value;
            klsServerJob.JobType_ID = 1;
            klsServerJob.Source_ID = iRec_ID;
            klsServerJob.Status = 0;
            klsServerJob.GetList();
            if (klsServerJob.List.Rows.Count == 0) {
                sMessage = "";
                if (sStatementFile == "") sMessage = "Λείπει συνημμένο αρχείο Statement";

                iLine_Status = 1;
                for (i = 1; i <= fgCodes.Rows.Count - 1; i++)
                    if (Convert.ToInt32(fgCodes[i, "LineStatus"]) == 0) iLine_Status = 0;

                if (iLine_Status == 0) sMessage = "\n" + sMessage + "Καταχωρίστε όλα τα Υποχρεωτικά αρχεία";

                if (sMessage == "") {
                    if (CheckAUM()) {
                        bCBA = DefineCBA();
                        if (!bCBA || (bCBA && sCostBenefits.Length > 0))
                        {

                            for (i = 1; i <= fgCodes.Rows.Count - 1; i++)
                            {
                                iShare_ID = Convert.ToInt32(fgCodes[i, "Share_ID"]);
                                SaveRecord(i);
                            }
                            SaveInvestmentProposal();

                            klsServerJob = new clsServerJobs();
                            klsServerJob.DateStart = Convert.ToDateTime("1900/01/01");
                            klsServerJob.DateFinish = Convert.ToDateTime("1900/01/01");
                            klsServerJob.JobType_ID = 1;                                                  // 1 -  create PDF for Investment Proposal
                            klsServerJob.Source_ID = iRec_ID;
                            klsServerJob.Parameters = "";
                            klsServerJob.Status = 0;
                            klsServerJob.InsertRecord();

                            Timer1.Interval = 1000;
                            Timer1.Start();

                            SwitchOnOffHeader(false);
                            fgCalls.AllowEditing = false;

                            SwitchOnOffButtons(false);
                            tslNewEdit.Enabled = true;
                        }
                        else
                           if (bCBA) MessageBox.Show("Υποχρέωση CBA", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                }
                else MessageBox.Show("ΠΡΟΣΟΧΗ !" + sMessage, Global.AppTitle, MessageBoxButtons.OK);
            }
            else MessageBox.Show("Η εντολή δημιουργίας του PDF-αρχείου έχει ήδη σταλεί", Global.AppTitle, MessageBoxButtons.OK);
        }
        private void lnkPDF_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lnkPDF.Text != "")
                Global.DMS_ShowFile("Customers/" + ucCS.txtContractTitle.Text + "/InvestProposals/" + iRec_ID, lnkPDF.Text);
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {

            int i = -999;

            clsServerJobs ServerJobs = new clsServerJobs();
            ServerJobs.DateStart = Convert.ToDateTime("1900/01/01");
            ServerJobs.DateFinish = DateTime.Now;
            ServerJobs.JobType_ID = 1;
            ServerJobs.Source_ID = iRec_ID;
            ServerJobs.Status = -999;
            ServerJobs.GetList();
            foreach (DataRow dtRow in ServerJobs.List.Rows) {
                i = Convert.ToInt32(dtRow["Status"]);
            }

            if (i != -999) {
                if (i > 0) {
                    sPDF_FileName = "Επενδυτικη Πρόταση " + iRec_ID + ".pdf";

                    lnkPDF.Text = sPDF_FileName;        // only file name;

                    clsInvestIdees klsInvestIdees = new clsInvestIdees();
                    klsInvestIdees.Record_ID = iRec_ID;
                    klsInvestIdees.GetRecord();

                    klsInvestIdees.ProposalPDFile = sPDF_FileName;
                    klsInvestIdees.EditRecord();
                    Timer1.Stop();

                    PopupNotifier popup = new PopupNotifier();
                    popup.ContentText = "Επενδυτικη Πρόταση " + iRec_ID + " είναι έτοιμη";
                    popup.Popup();
                    //Dim locMessage As New frmMessage;
                    //locMessage.lblMessage.Text = "Επενδυτικη Πρόταση " + iRec_ID + " είναι έτοιμη";
                    //locMessage.ShowDialog();
                }
            }
        }
        public bool DeleteTableRecord(string sTable, int iID)
        {
            bool bResult = false;

            if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;",
                Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes) {

                clsSystem System = new clsSystem();
                System.Table = sTable;
                System.Record_ID = iID;
                System.DeleteRecord();

                bResult = true;
            }

            return bResult;
        }
        #endregion
        #region --- Edit Record functions --------------------------------------------------------
        private void EditProduct()
        {
            int j = 0, k = 0;

            if (CheckAUM()) {
                i = fgCodes.Row;
                if (i > 0) {
                    iCodeAktion = 1;
                    bCheckShares = false;

                    chkRights.Visible = false;
                    cmbProducts.SelectedValue = fgCodes[i, 15];
                    ShowProductLabels(Convert.ToInt32(cmbProducts.SelectedValue));
                    lblTitle.Text = fgCodes[i, 1] + "";
                    lblCode.Text = fgCodes[i, 2] + "";
                    lblCode2.Text = fgCodes[i, 3] + "";
                    lblISIN.Text = fgCodes[i, 4] + "";
                    cmbCurrency.Text = fgCodes[i, 5] + "";
                    cmbStockExchanges.SelectedValue = Convert.ToInt32(fgCodes[i, 17]);
                    cmbConstant.SelectedIndex = Convert.ToInt32(fgCodes[i, 22]);
                    if (cmbConstant.SelectedIndex == 2){
                        if ((fgCodes[i, 23] + "") != "")
                            dConstant.Value = Convert.ToDateTime(fgCodes[i, 2]);
                    }
                    txtAction.Text = fgCodes[i, 0] + "";
                    lstType.SelectedValue = fgCodes[i, 18];
                    txtPrice.Text = fgCodes[i, 8] + "";
                    txtPriceUp.Text = fgCodes[i, 19] + "";
                    txtPriceDown.Text = fgCodes[i, 20] + "";
                    txtQuantity.Text = fgCodes[i, 9] + "";
                    txtAmount.Text = fgCodes[i, 10] + "";
                    txtWeight.Text = fgCodes[i, 11] + "";
                    iShare_ID = Convert.ToInt32(fgCodes[i, 14]);
                    iShareType = Convert.ToInt32(fgCodes[i, 15]);
                    iProductCategory_ID = Convert.ToInt32(fgCodes[i, 16]);
                    iLine_Status = Convert.ToInt32(fgCodes[i, 24]);
                    DefineEnergia();
                    cmbEnergia.SelectedValue = fgCodes[i, "Energia"];
                    txtProductNotes.Text = fgCodes[i, "ProductNotes"] + "";
                    txtURL_IR.Text = fgCodes[i, "URL_IR"] + "";


                    sgGravity = Convert.ToSingle(fgCodes[i, "Weight"]);
                    foundRows = Global.dtProducts.Select("ISIN = '" + lblISIN.Text + "' AND Code = '" + lblCode.Text + "'");
                    if (foundRows.Length > 0) sgGravity = Convert.ToSingle(foundRows[0]["Gravity"]);

                    //--- show all upload files in fgCodesAttaches and in fgCodesMandatoryAttaches grids
                    fgCodesAttaches.Rows.Count = 1;
                    for (j = 0; j <= stAtts.Count - 1; j++)  {
                        if (stAtts[j].Share_ID == iShare_ID) {
                            if (stAtts[j].DocType_ID == 0)   {                                                                                // дополнительный вложенный файл - не ОБЯЗАТЕЛЬНЫЙ файл
                                fgCodesAttaches.AddItem(stAtts[j].FileName + "\t" + stAtts[j].FullFilePath + "\t" + stAtts[j].Rec_ID + "\t" + stAtts[j].ServerFileName + "\t" + 
                                                        stAtts[j].UploadFilePath + "\t" + stAtts[j].RemoteFileName + "\t" + j + "\t" + stAtts[j].WasEdited);        // j = stAtt_j
                            }
                            else {  
                                for (k = 1; k <= fgCodesMandatoryAttaches.Rows.Count - 1; k++) {
                                    if (Convert.ToInt32(fgCodesMandatoryAttaches[k, "DocType_ID"]) == Convert.ToInt32(stAtts[j].DocType_ID)) {
                                        fgCodesMandatoryAttaches[k, 1] = stAtts[j].FileName;
                                        fgCodesMandatoryAttaches[k, 2] = stAtts[j].FullFilePath;
                                        fgCodesMandatoryAttaches[k, "ID"] = stAtts[j].Rec_ID;
                                        fgCodesMandatoryAttaches[k, "DocType_ID"] = stAtts[j].DocType_ID;
                                        fgCodesMandatoryAttaches[k, "ServerFileName"] = stAtts[j].ServerFileName;
                                        fgCodesMandatoryAttaches[k, "UploadFilePath"] = stAtts[j].UploadFilePath;
                                        fgCodesMandatoryAttaches[k, "RemoteFileName"] = stAtts[j].RemoteFileName;
                                        fgCodesMandatoryAttaches[k, "stAtt_i"] = j;
                                        fgCodesMandatoryAttaches[k, "WasEdited"] = stAtts[j].WasEdited;
                                    }
                                }
                            }
                        }
                    }

                    if (bBlockedEditing) {
                        picAddCode.Visible = false;
                        picDeleteCode.Visible = false;
                        btnSave.Visible = false;
                    }
                    else {
                        picAddCode.Visible = true;
                        picDeleteCode.Visible = true;
                        btnSave.Visible = true;
                    }

                    bCheckShares = true;
                    panCodeDetails.Enabled = true;
                    panCode.Top = 100;
                    panCode.Visible = true;
                }
                bWasEdit = true;
            }
        }
        private void txtAction_TextChanged(object sender, EventArgs e)
        {
            if (txtAction.Text != "")
            {
                lblGravity.Visible = true;
                txtWeight.Visible = true;

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
                        break;
                }


                if (txtAction.Text == "BUY")
                {
                    ShowProductLabels(Convert.ToInt32(cmbProducts.SelectedValue));
                    panCodeDetails.Enabled = true;
                    panCode.BackColor = Color.MediumAquamarine;
                    btnSave.Enabled = true;
                    lblGravity.Visible = true;
                    lblGravityMax.Visible = true;
                    txtWeight.Visible = true;
                    DefineEnergia();
                    ucPS.Focus();
                }
                else
                {
                    if (txtAction.Text == "SELL")
                    {
                        ShowProductLabels(Convert.ToInt32(cmbProducts.SelectedValue));
                        panCodeDetails.Enabled = true;
                        panCode.BackColor = Color.LightCoral;
                        lblGravity.Visible = false;
                        lblGravityMax.Visible = false;
                        txtWeight.Visible = false;
                        btnSave.Enabled = true;
                        DefineEnergia();
                        ucPS.Focus();
                    }
                    else
                    {
                        Console.Beep();
                        panCode.BackColor = Color.Silver;
                        panCodeDetails.Enabled = false;
                        btnSave.Enabled = false;
                        txtAction.Focus();
                    }
                }
            }
        }
        private void cmbEnergia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bCheckSurname) {
                switch (Convert.ToInt32(cmbEnergia.SelectedValue)) {
                    case 1:
                        txtAction.Text = "BUY";
                        break;
                    case 2:
                        txtAction.Text = "SELL";
                        break;
                    case 3:
                        txtAction.Text = "BUY";
                        break;
                    case 4:
                        txtAction.Text = "BUY";
                        break;
                    case 5:
                        txtAction.Text = "";
                        break;
                }
            }
        }
        private void picCode_Clean_Click(object sender, EventArgs e)
        {
            int j = 0;
            bCheckShares = false;

            EmptyCodeRec();

            for (j = 0; j <= stAtts.Count - 1; j++)
                if (stAtts[j].Share_ID == iShare_ID) {
                    rAtts = stAtts[j];
                    rAtts.Share_ID = -999;                             // -999 означает, что данная запись при сохранении InvestProposal долна быть проигнорирована
                    rAtts.WasEdited = 1;
                    stAtts[j] = rAtts;                                 // в данном случае потому, что была нажата кнопка очистки и все раннее добавленные в stAtts файлы должны быть проигнорированы
                }
            fgCodesAttaches.Rows.Count = 1;
            bCheckShares = true;

            iShare_ID = 0;                                             // must be here
        }

        private void EmptyCodeRec()
        {
            cmbProducts.SelectedValue = 0;
            ucPS.ShowProductsList = false;
            ucPS.txtShareTitle.Text = "";
            ucPS.ShowProductsList = true;
            lblTitle.Text = "";
            lblCode.Text = "";
            lblCode2.Text = "";
            lblISIN.Text = "";
            cmbEnergia.SelectedValue = 0;
            cmbCurrency.SelectedValue = 0;
            cmbStockExchanges.SelectedValue = 0;
            cmbConstant.SelectedIndex = 0;
            lstType.SelectedValue = 0;
            lblAmount_NomismaAnaforas.Text = "0";
            lblCurrency_NomismaAnaforas.Text = lblCurrency.Text;
            lblEndektikiTimi.Text = "";
            lblCurrRate_NomismaAnaforas.Text = "";
            lblAmount_NomismaAnaforas.Text = "";
            lblCurrency_NomismaAnaforas.Text = "";
            txtPrice.Text = "0";
            txtPriceUp.Text = "0";
            txtPriceDown.Text = "0";
            txtQuantity.Text = "0";
            txtAmount.Text = "0";
            txtWeight.Text = "0";
            txtProductNotes.Text = "";
            iShareType = 0;
            iProductCategory_ID = 0;
            txtURL_IR.Text = "";

            chk1_Buy.Checked = false;
            chk2_Buy.Checked = false;
            chk3_Buy.Checked = false;
            chk4_Buy.Checked = false;
            chk5_Buy.Checked = false;
            chk6_Buy.Checked = false;
            chk7_Buy.Checked = false;
            chk8_Buy.Checked = false;
            chk9_Buy.Checked = false;
            chk10_Buy.Checked = false;

            chk1_Sell.Checked = false;
            chk2_Sell.Checked = false;
            chk3_Sell.Checked = false;
            chk4_Sell.Checked = false;
            chk5_Sell.Checked = false;
            chk6_Sell.Checked = false;
            chk7_Sell.Checked = false;
            chk8_Sell.Checked = false;

            fgCodesMandatoryAttaches.Rows.Count = 1;
            fgCodesAttaches.Rows.Count = 1;
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
                DefineNums(1);
            }
        }
        private void txtQuantity_LostFocus(object sender, EventArgs e)
        {
            if (!Global.IsNumeric(txtQuantity.Text) || txtQuantity.Text.IndexOf(".") > 0)
            {
                txtQuantity.Text = "0";
                txtQuantity.BackColor = Color.Red;
                txtQuantity.Focus();
            }
            else
            {
                txtQuantity.BackColor = Color.White;
                DefineNums(2);
            }
        }
        private void txtAmount_LostFocus(object sender, EventArgs e)
        {
            if (!Global.IsNumeric(txtAmount.Text) || txtAmount.Text.IndexOf(".") > 0)
            {
                txtAmount.Text = "0";
                txtAmount.BackColor = Color.Red;
                txtAmount.Focus();
            }
            else
            {
                txtAmount.BackColor = Color.White;
                DefineNums(3);
            }
        }
        private void picBondCalc_Click(object sender, EventArgs e)
        {
            //Global.CallBondCalc(iShare_ID, Convert.ToDecimal(txtPrice.Text), Convert.ToDecimal(txtQuantity.Text));
        }
        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            if (txtWeight.Text != "0") {
                if (txtAction.Text == "BUY") {
                    if (CheckGravity())
                        MessageBox.Show("Λάθος Βαρύτητα", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else {
                lblGravityMax.Text = "";
                txtWeight.Text = "0";
            }
        }
        private bool DefineCBA()
        {
            bool bCBAMust, bBuy, bSell;

            bCBAMust = false;
            bBuy = false;
            bSell = false;

            for (i = 1; i <= fgCodes.Rows.Count - 1; i++)
            {
                if ((fgCodes[i, 0] + "") == "BUY") bBuy = true;
                else
                   if ((fgCodes[i, 0] + "") == "SELL") bSell = true;
            }

            if (bBuy && bSell) bCBAMust = true;

            return bCBAMust;
        }
        private void DefineNums(int iField)
        {

            if (Convert.ToInt32(lstType.SelectedValue) != 1)
            {
                if (Global.IsNumeric(txtPrice.Text))
                {
                    sgPrice = Convert.ToSingle(txtPrice.Text);
                    sgQuantity = (Global.IsNumeric(txtQuantity.Text) ? Convert.ToSingle(txtQuantity.Text) : 0);
                    sgAmount = (Global.IsNumeric(txtAmount.Text) ? Convert.ToSingle(txtAmount.Text) : 0);

                    if (iField == 1 || iField == 2)
                    {
                        txtAmount.Text = (sgPrice * sgQuantity).ToString("0.00");
                        lblAmount_NomismaAnaforas.Text = (Convert.ToSingle(txtAmount.Text) / sgCurRate).ToString("0.00");

                        if (iShareType == 2) txtAmount.Text = (Convert.ToSingle(txtAmount.Text) / 100).ToString("0.00");
                        lblAmount_NomismaAnaforas.Text = (Convert.ToSingle(lblAmount_NomismaAnaforas.Text) / 100).ToString("0.00");

                        txtWeight.Text = (Convert.ToSingle(lblAmount_NomismaAnaforas.Text) * Convert.ToSingle(100) / Convert.ToSingle(txtAUM.Text)).ToString("0.0");
                    }
                    else
                    {
                        if (sgQuantity == 0)
                        {
                            if (sgPrice != 0) txtQuantity.Text = Math.Round(sgAmount / sgPrice).ToString("0.00");
                            else txtQuantity.Text = "0";
                        }
                    }
                }
                else
                {
                    txtQuantity.Text = "0";

                    lblAmount_NomismaAnaforas.Text = (Convert.ToSingle(txtAmount.Text) / sgCurRate).ToString("0.00");
                    if (iShareType == 2) lblAmount_NomismaAnaforas.Text = (Convert.ToSingle(lblAmount_NomismaAnaforas.Text) / 100).ToString("0.00");                // Omologa (ShareType=2)

                    txtWeight.Text = (Convert.ToSingle(lblAmount_NomismaAnaforas.Text) * Convert.ToSingle(100) / Convert.ToSingle(txtAUM.Text)).ToString("0.0");
                }
            }
            else
            {
                if (Convert.ToSingle(txtAmount.Text) != 0) txtWeight.Text = (Convert.ToSingle(txtAmount.Text) / sgCurRate * Convert.ToSingle(100) / Convert.ToSingle(txtAUM.Text)).ToString("0.0");
                else
                {
                    lblAmount_NomismaAnaforas.Text = Math.Round(Convert.ToSingle(txtQuantity.Text) * sgEndektikiTimi / sgCurRate).ToString("0.00");
                    if (iShareType == 2) lblAmount_NomismaAnaforas.Text = (Convert.ToSingle(lblAmount_NomismaAnaforas.Text) / 100).ToString("0.00");                 //Omologa (ShareType=2)

                    txtWeight.Text = (Convert.ToSingle(lblAmount_NomismaAnaforas.Text) * Convert.ToSingle(100) / Convert.ToSingle(txtAUM.Text)).ToString("0.0");
                }
            }
        }
        private void ShowUploadFiles()
        {
            int j = 0;

            fgAttachedFiles.Redraw = false;
            fgAttachedFiles.Rows.Count = 1;

            for (j = 0; j <= stAtts.Count - 1; j++)
                if  (stAtts[j].DocType_ID >= 0)
                    fgAttachedFiles.AddItem(stAtts[j].FileName + "\t" + stAtts[j].ServerFileName + "\t" + stAtts[j].UploadFilePath + "\t" + 
                                            stAtts[j].RemoteFileName + "\t" + stAtts[j].Rec_ID + "\t" + stAtts[j].Share_ID);

            fgAttachedFiles.Redraw = true;
        }
        private string DefineContractGeography(int iContract_ID)
        {
            string sTemp = "";
            clsContracts klsContract = new clsContracts();
            klsContract.Record_ID = iContract_ID;
            klsContract.GetRecord();
            sTemp = (klsContract.Details.ChkWorld == 1 ? "1" : "0") + (klsContract.Details.ChkGreece == 1 ? "1" : "0") + (klsContract.Details.ChkEurope == 1 ? "1" : "0") +
                    (klsContract.Details.ChkAmerica == 1 ? "1" : "0") + (klsContract.Details.ChkAsia == 1 ? "1" : "0");

            return sTemp;
        }
        private void DefineComplexProduct()
        {
            iComplexProduct = 0;
            lblComplexProduct.Text = "No";
            clsContracts_ComplexSigns klsContracts_ComplexSigns = new clsContracts_ComplexSigns();
            klsContracts_ComplexSigns.Contract_ID = iContract_ID;
            klsContracts_ComplexSigns.GetList();
            foreach (DataRow dtRow in klsContracts_ComplexSigns.List.Rows)
            {
                if (Convert.ToInt32(dtRow["ComplexSign_ID"]) == 2)
                {
                    iComplexProduct = 1;
                    lblComplexProduct.Text = "Yes";
                }
            }
        }
        private bool CheckGravity()
        {
            bool bOK = false;
            if (sgGravity != 0 && Convert.ToSingle(txtWeight.Text) > sgGravity) bOK = true;

            return bOK;
        }
        private void cmbProducts_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckSurname) {
                ShowProductLabels(Convert.ToInt32(cmbProducts.SelectedValue));
                lstType.SelectedValue = 0;
                fgCodesMandatoryAttaches.Rows.Count = 1;
                fgCodesAttaches.Rows.Count = 1;

                ucPS.ListType = 2;                                                                  // iListType = 2 : dtProductsContract - list of products for current contract
                ucPS.ShowNonAccord = true;                                                          // Show NonAccordable products (oxi katallila) with red Background
                ucPS.ShowCancelled = false;                                                         // Don't show cancelled products
                if (Convert.ToInt32(cmbProducts.SelectedValue) == 0) ucPS.Filters = "Aktive = 1";
                else ucPS.Filters = "Aktive = 1 AND Product_ID = " + cmbProducts.SelectedValue;

                cmbCurrency.Focus();
            }
        }
        private void fgCodesMandatoryAttaches_CellButtonClick(object sender, RowColEventArgs e)
        {
            if (e.Col == 1)
            {
                sFilePath = Global.FileChoice();
                fgCodesMandatoryAttaches[fgCodesMandatoryAttaches.Row, 1] = Path.GetFileName(sFilePath);
                fgCodesMandatoryAttaches[fgCodesMandatoryAttaches.Row, "FileFullPath"] = sFilePath;
                fgCodesMandatoryAttaches[fgCodesMandatoryAttaches.Row, "ServerFileName"] = "";
                fgCodesMandatoryAttaches[fgCodesMandatoryAttaches.Row, "UploadFilePath"] = "";
                fgCodesMandatoryAttaches[fgCodesMandatoryAttaches.Row, "RemoteFileName"] = "";
                fgCodesMandatoryAttaches[fgCodesMandatoryAttaches.Row, "stAtt_i"] = "0";
            }
        }
        private void picAddCode_Click(object sender, EventArgs e)
        {
            sFilePath = Global.FileChoice();
            fgCodesAttaches.AddItem(Path.GetFileName(sFilePath) + "\t" + sFilePath + "\t" + "0" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "0" + "\t" + "1");
        }

        private void picDeleteCode_Click(object sender, EventArgs e)
        {
            if (fgCodesAttaches.Row > 0) 
               if (DeleteTableRecord("InvestIdees_Attachments", Convert.ToInt32(fgCodesAttaches[fgCodesAttaches.Row, "ID"]))) 
                fgCodesAttaches.RemoveItem(fgCodesAttaches.Row);        
        }
        private void picShowCode_Click(object sender, EventArgs e)
        {
            if ((fgCodesAttaches[fgCodesAttaches.Row, "FileName"]+"").Trim() != "") {
                if (Convert.ToInt32(fgCodesAttaches[fgCodesAttaches.Row, "ID"]) == 0)
                    System.Diagnostics.Process.Start(fgCodesAttaches[fgCodesAttaches.Row, "FileFullPath"]+"");
                else
                    Global.DMS_ShowFile("Customers/" + ucCS.txtContractTitle.Text + "/InvestProposals/" + iRec_ID, fgCodesAttaches[fgCodesAttaches.Row, "ServerFileName"]+"");
            }
        }
        private void fgCalls_CellButtonClick(object sender, RowColEventArgs e)
        {
            if (e.Col == 0)
            {
                sFilePath = Global.FileChoice();
                fgCalls[fgCalls.Row, "FileName"] = Path.GetFileName(sFilePath);
                fgCalls[fgCalls.Row, "FileFullPath"] = sFilePath;
                fgCalls[fgCalls.Row, "ServerFileName"] = "";
                fgCalls[fgCalls.Row, "UploadFilePath"] = "";
            }
        }
        private void fgCalls_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (e.Row > 0) {
                if ((fgCalls[e.Row, 0] +"") == "") e.Cancel = false;
                else e.Cancel = true;
            }
            else e.Cancel = true;
        }
        private void cmbConstant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConstant.SelectedIndex == 2)
            {
                dConstant.Value = DateTime.Now;
                dConstant.Visible = true;
            }
            else dConstant.Visible = false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            int i, j, k, m;
            string sTemp = "", sError = "", sWarning = "";
            bool bError = false;

            if (iShare_ID == 0 || lblTitle.Text == "")  {
                bError = true;
                sError = "Επιλέξτε ένα προϊόν \n";
            }

            if (((Convert.ToInt32(cmbProducts.SelectedValue) == 1) || (Convert.ToInt32(cmbProducts.SelectedValue) == 2) || (Convert.ToInt32(cmbProducts.SelectedValue) == 4)) &&
                (Convert.ToInt32(lstType.SelectedValue) == 0) && (txtPrice.Text == "0")) {
                bError = true;
                sError = "Το πεδίο Τιμή δεν πρέπει να είναι κενό. Καταχωρίστε εναν αριθμό μεγαλύτερο του 0. \n";
            }
            else  {
                if (Convert.ToInt32(lstType.SelectedValue) == 2) {                                       // 2 - Stop
                    if (!Global.IsNumeric(txtPrice.Text) || txtPrice.Text == "0") {
                        bError = true;
                        sError = sError + "Το πεδίο Τιμή δεν πρέπει να είναι κενό. Καταχωρίστε εναν αριθμό μεγαλύτερο του 0 \n";
                    }
                }
            }

            if ((Convert.ToInt32(cmbProducts.SelectedValue) == 1) || (Convert.ToInt32(cmbProducts.SelectedValue) == 4)) {
                if (!IsInt(txtQuantity.Text)) {
                    bError = true;
                    sError = "Το πεδίο Τεμάχια πρέπει να είναι Αριθμός χωρίς δεκαδικά. \n";
                }
            }

            if (Convert.ToInt32(cmbProducts.SelectedValue) == 6) {                                   // 6 - AK
                if (txtAction.Text == "SELL") {
                    if (txtQuantity.Text != "0" && txtAmount.Text != "0") {
                        bError = true;
                        sError = "Καταχωρείστε Μερίδια ή Ποσό Επένδυσης, οχι και τα δυο \n";
                    }
                    else {
                        if (txtQuantity.Text == "0" && txtAmount.Text == "0")  {
                            bError = true;
                            sError = "Τα Μερίδια, ή το Ποσό Επένδυσης πρέπει να καταχωρυθεί και να είναι μεγαλύτερο του 0. \n";
                        }
                    }
                }
                else {
                    if (!Global.IsNumeric(txtAmount.Text) || txtAmount.Text == "0") {
                        bError = true;
                        sError = sError + "Το πεδίο " + lblAmount.Text + " πρέπει να είναι μεγαλύτερο του 0. \n";
                    }
                }
            }
            else {
                if (txtQuantity.Visible) {
                    if (!Global.IsNumeric(txtQuantity.Text) || txtQuantity.Text == "0") {
                        bError = true;
                        sError = sError + "Το πεδίο " + lblQuantity.Text + " πρέπει να είναι μεγαλύτερο του 0. \n";
                    }
                }

                if (txtQuantity.Visible) {
                    if (!Global.IsNumeric(txtAmount.Text) || txtAmount.Text == "0") {
                        bError = true;
                        sError = sError + "Το πεδίο " + lblAmount.Text + " πρέπει να είναι μεγαλύτερο του 0 \n";
                    }
                }
            }

            if (txtAction.Text == "BUY" && (!Global.IsNumeric(txtWeight.Text) || txtWeight.Text == "0")) {
                bError = true;
                sError = sError + "Το πεδίο Βαρύτητα πρέπει να είναι μεγαλύτερο  του 0 \n";
            }

            if (Convert.ToInt32(lstType.SelectedValue) == 3) {                                                     // 3 - Scenario
                if (!Global.IsNumeric(txtPriceUp.Text) || txtPriceUp.Text == "0") {
                    bError = true;
                    sError = sError + "Το πεδίο Target πρέπει να είναι μεγαλύτερο του 0 \n";
                }
                if (!Global.IsNumeric(txtPriceDown.Text) || txtPriceDown.Text == "0") {
                    bError = true;
                    sError = sError + "Το πεδίο Stop πρέπει να είναι μεγαλύτερο του 0 \n";
                }
            }

            m = 0;                                              // m - флаг указания файла. Если m = 1, то хоть в одной строке не указан файл
            k = (fgCodesAttaches.Rows.Count - 1);                        

            if (bCheckMandatoryFiles) {
                for (i = 1; i <= fgCodesMandatoryAttaches.Rows.Count - 1; i++) {
                    if ((fgCodesMandatoryAttaches[i, 1] + "") != "") k = k + 1;
                    else m = 1;
                }
            }

            if (m == 1) sWarning = sWarning + "Καταχωρίστε όλα τα Υποχρεωτικά αρχεία \n";

            if (txtAction.Text == "BUY" && txtURL_IR.Text == "")
                sWarning = sWarning + "Καταχωρίστε Investors Relations \n";


            if (CheckGravity()) {
                bError = true;
                sError = sError + "Λάθος Βαρύτητα \n";
            }

            if (!bError) {
                if (sWarning.Length != 0) {
                    iLine_Status = 0;
                    MessageBox.Show(sWarning, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else iLine_Status = 1;


                if (cmbConstant.SelectedIndex == 2) sTemp = dConstant.Value.ToString("dd/MM/yyyy");
                else sTemp = "";

                if (iCodeAktion == 0) {

                    fgCodes.AddItem(txtAction.Text + "\t" + lblTitle.Text + "\t" + lblCode.Text + "\t" + lblCode2.Text + "\t" + lblISIN.Text + "\t" +
                                cmbCurrency.Text + "\t" + cmbStockExchanges.Text + "\t" + cmbConstant.Text + "\t" +
                                (Convert.ToInt32(lstType.SelectedValue) == 0 ? txtPrice.Text : sPriceType[Convert.ToInt32(lstType.SelectedValue)]) + "\t" +
                                txtQuantity.Text + "\t" + txtAmount.Text + "\t" + txtWeight.Text + "\t" + k + "\t" + "0" + "\t" + iShare_ID + "\t" +
                                iShareType + "\t" + iProductCategory_ID + "\t" + cmbStockExchanges.SelectedValue + "\t" + lstType.SelectedValue + "\t" +
                                txtPriceUp.Text + "\t" + txtPriceDown.Text + "\t" + "" + "\t" + cmbConstant.SelectedIndex + "\t" +
                                sTemp + "\t" + iLine_Status + "\t" + cmbEnergia.SelectedValue + "\t" + txtProductNotes.Text + "\t" + txtURL_IR.Text, 1);
                    fgCodes.Row = 1;
                }
                else {
                    i = fgCodes.Row;
                    fgCodes[i, 0] = txtAction.Text;
                    fgCodes[i, 1] = lblTitle.Text;
                    fgCodes[i, 2] = lblCode.Text;
                    fgCodes[i, 3] = lblCode2.Text;
                    fgCodes[i, 4] = lblISIN.Text;
                    fgCodes[i, 5] = cmbCurrency.Text;
                    fgCodes[i, 6] = cmbStockExchanges.Text;
                    fgCodes[i, 7] = cmbConstant.Text;
                    fgCodes[i, 8] = txtPrice.Text;
                    fgCodes[i, 9] = txtQuantity.Text;
                    fgCodes[i, 10] = txtAmount.Text;
                    fgCodes[i, 11] = txtWeight.Text;
                    fgCodes[i, "AttachedFiles"] = k;
                    fgCodes[i, 14] = iShare_ID;
                    fgCodes[i, 15] = iShareType;
                    fgCodes[i, 16] = iProductCategory_ID;
                    fgCodes[i, 17] = cmbStockExchanges.SelectedValue;
                    fgCodes[i, 18] = lstType.SelectedValue;
                    fgCodes[i, 19] = txtPriceUp.Text;
                    fgCodes[i, 20] = txtPriceDown.Text;
                    fgCodes[i, 22] = cmbConstant.SelectedIndex;
                    fgCodes[i, 23] = sTemp;
                    fgCodes[i, 24] = iLine_Status;
                    fgCodes[i, "Energia"] = cmbEnergia.SelectedValue;
                    fgCodes[i, "ProductNotes"] = txtProductNotes.Text;
                    fgCodes[i, "URL_IR"] = txtURL_IR.Text;

                    /*
                    // все attached files данного продукта должны быть помечены как удаленные (Share_ID = -999), а на их место чуть ниже будут добавлены новые записи
                    for (j = 0; j <= stAtts.Count - 1; j++)  {
                        if (stAtts[j].Share_ID == iShare_ID) { 
                            rAtts = stAtts[j];
                            rAtts.Share_ID = -999;                       // -999 означает, что данная запись при сохранении InvestProposal долна быть проигнорирована
                            stAtts[j] = rAtts;                           //  в данном случае потому, что чуть ниже все записи с attached file будут добавлены в stAtts вновь
                        }
                    }
                    */
                }

                // сохраняем записи в stAtts из fgCodesAttaches
                i = stAtts.Count - 1;
                for (j = 1; j <= fgCodesAttaches.Rows.Count - 1; j++) {
                    if ((fgCodesAttaches[j, "FileName"] + "") != "") {
                        if (Convert.ToInt32(fgCodesAttaches[j, "stAtt_i"]) == 0) {                         // stAtt_j = 0 - it's new attached file.So...
                            i = i + 1;                                                                     // ...add new record
                            stAtts.Insert(i, new Attaches
                            {
                                Rec_ID = Convert.ToInt32(fgCodesAttaches[j, "ID"]),
                                Share_ID = iShare_ID,
                                DocType_Title = "",
                                DocType_ID = 0,
                                FileName = fgCodesAttaches[j, "FileName"] + "",
                                FullFilePath = fgCodesAttaches[j, "FileFullPath"] + "",
                                ServerFileName = fgCodesAttaches[j, "ServerFileName"] + "",
                                UploadFilePath = fgCodesAttaches[j, "UploadFilePath"] + "",
                                RemoteFileName = fgCodesAttaches[j, "RemoteFileName"] + "",
                                WasEdited = Convert.ToInt32(fgCodesAttaches[j, "WasEdited"])
                            });
                        }
                        else{
                            k = Convert.ToInt32(fgCodesAttaches[j, "stAtt_i"]);                              // ... edit existing record
                            rAtts = stAtts[k];
                            rAtts.Rec_ID = Convert.ToInt32(fgCodesAttaches[j, "ID"]);
                            rAtts.Share_ID = iShare_ID;
                            rAtts.DocType_Title = "";
                            rAtts.DocType_ID = 0;
                            rAtts.FileName = fgCodesAttaches[j, "FileName"] + "";
                            rAtts.FullFilePath = fgCodesAttaches[j, "FileFullPath"] + "";
                            rAtts.ServerFileName = fgCodesAttaches[j, "ServerFileName"] + "";
                            rAtts.UploadFilePath = fgCodesAttaches[j, "UploadFilePath"] + "";
                            rAtts.RemoteFileName = fgCodesAttaches[j, "RemoteFileName"] + "";
                            rAtts.WasEdited = Convert.ToInt32(fgCodesAttaches[j, "WasEdited"]);
                            stAtts[k] = rAtts;
                        }
                    }
                    else {
                        k = Convert.ToInt32(fgCodesAttaches[j, "stAtt_i"]);                              // поскольку FileName = "", то эту запись надо прогнорировать
                        rAtts = stAtts[k];
                        rAtts.Share_ID = -999;                                                           // -999 означает, что данная запись при сохранении InvestProposal должна быть проигнорирована
                        rAtts.WasEdited = 1;
                        stAtts[k] = rAtts;
                    }
                }

                // сохраняем записи в stAtts из fgCodesMandatoryAttaches
                for (j = 1; j <= fgCodesMandatoryAttaches.Rows.Count - 1; j++) {
                    if (Convert.ToInt32(fgCodesMandatoryAttaches[j, "stAtt_i"]) == 0) {                       // stAtt_j = 0 - it's new attached file.So...
                        i = i + 1;                                                                            // ... add new record   
                        stAtts.Insert(i, new Attaches
                        {
                            Rec_ID = Convert.ToInt32(fgCodesMandatoryAttaches[j, "ID"]),
                            Share_ID = iShare_ID,
                            DocType_Title = fgCodesMandatoryAttaches[j, "DocType"] + "",
                            DocType_ID = Convert.ToInt32(fgCodesMandatoryAttaches[j, "DocType_ID"]),
                            FileName = fgCodesMandatoryAttaches[j, 1] + "",
                            FullFilePath = fgCodesMandatoryAttaches[j, 2] + "",
                            ServerFileName = fgCodesMandatoryAttaches[j, "ServerFileName"] + "",
                            UploadFilePath = fgCodesMandatoryAttaches[j, "UploadFilePath"] + "",
                            RemoteFileName = fgCodesMandatoryAttaches[j, "RemoteFileName"] + "",
                            WasEdited = Convert.ToInt32(fgCodesMandatoryAttaches[j, "WasEdited"]),
                        });
                    }
                    else {
                        k = Convert.ToInt32(fgCodesMandatoryAttaches[j, "stAtt_i"]);                          // ... edit existing record  
                        rAtts = stAtts[k];
                        rAtts.Rec_ID = Convert.ToInt32(fgCodesMandatoryAttaches[j, "ID"]);
                        rAtts.Share_ID = iShare_ID;
                        rAtts.DocType_Title = fgCodesMandatoryAttaches[j, "DocType"] + "";
                        rAtts.DocType_ID = Convert.ToInt32(fgCodesMandatoryAttaches[j, "DocType_ID"]);
                        rAtts.FileName = fgCodesMandatoryAttaches[j, 1] + "";
                        rAtts.FullFilePath = fgCodesMandatoryAttaches[j, 2] + "";
                        rAtts.ServerFileName = fgCodesMandatoryAttaches[j, "ServerFileName"] + "";
                        rAtts.UploadFilePath = fgCodesMandatoryAttaches[j, "UploadFilePath"] + "";
                        rAtts.RemoteFileName = fgCodesMandatoryAttaches[j, "RemoteFileName"] + "";
                        rAtts.WasEdited = Convert.ToInt32(fgCodesMandatoryAttaches[j, "WasEdited"]);
                        stAtts[k] = rAtts;
                    }
                }
                i = fgCodes.Row;

                SaveRecord(i);
                panCode.Visible = false;
            }
            else MessageBox.Show(sError, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            DefineAttachFilesList();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            panCode.Visible = false;

            DefineAttachFilesList();
        }
        #endregion
        #region --- Save functions ----------------------------------------
        private void SaveInvestmentProposal()
        {
            int i = 0, j = 0;
            string sTemp = "";

            this.Refresh();
            this.Cursor = Cursors.WaitCursor;
            bWasSaved = false;

            //--- Define Proposal Status : 0 - new (wasn't sent yet - white),  1 - wait(was sent from user, but not from server - yellow), 2- sent from server(green), 3 - can't sent (red)
            iStatus = 0;                                                        // Proposal can be save only if it wasn't sent, so Proposal Status = 0
            if (Convert.ToInt32(cmbInformMethods.SelectedValue) == 1)
            {         // Only one exception: if it's a telephone proposal and it has one CALL file and PDF file Status = 2 - was sent from Server
                if (fgCalls.Rows.Count > 1) iStatus = 2;
            }

            SaveTitle();

            clsInvestIdees klsInvestIdees = new clsInvestIdees();
            clsInvestIdees_Attachments klsInvestIdees_Attachment = new clsInvestIdees_Attachments();
            //--- Сохранение в БД и загрузка ВСЕХ незагруженных файлов на локальный сервер с новыми уникальными именами --------------------------------------

            //--- Statement File -----------
            if (sOldStatementFile.Length == 0 && sStatementFile.Length > 0) {
                sTemp = iRec_ID + "_Statement" + Path.GetExtension(sStatementFile);              // sTemp - new name (XXXX_XXXX_XXXX) of Statement File           
                sFilePath = Global.DMS_UploadFile(sStatementFile, "Customers/" + ucCS.txtContractTitle.Text + "/InvestProposals/" + iRec_ID, sTemp);  // sFilePath - UploadFilePath

                //--- сохраняем название Statement file  в таблице InvestIdees -----------------
                klsInvestIdees.Record_ID = iRec_ID;
                klsInvestIdees.GetRecord();
                klsInvestIdees.StatementFile = sTemp;
                klsInvestIdees.EditRecord();

                //--- сохраняем данные Statement file  в таблице InvestIdees_Attachment. Все данные берутся из массива stAtts -----------------
                for (j = 0; j <= stAtts.Count - 1; j++)  {
                    if (stAtts[j].DocType_ID == -1) {                                             //  -1 - it's a Statement File, and mustn't be uploaded onto Remote server

                        rAtts = stAtts[j];
                        rAtts.FileName = lnkStatement.Text;
                        rAtts.FullFilePath = sStatementFile;
                        rAtts.ServerFileName = sTemp;
                        rAtts.UploadFilePath = sFilePath;
                        stAtts[j] = rAtts;

                        if (stAtts[j].Rec_ID != 0) {                                              // Rec_ID = 0 - новый не заруженный ранее файл, Rec_ID <> 0 - уже загруженный файл
                            klsInvestIdees_Attachment.Record_ID = stAtts[j].Rec_ID;               // читаем данные о загруженном ранее Statement File
                            klsInvestIdees_Attachment.GetRecord();
                        }
                        klsInvestIdees_Attachment.II_ID = iRec_ID;
                        klsInvestIdees_Attachment.Share_ID = 0;
                        klsInvestIdees_Attachment.DocType_ID = -1;                                // -1 - it's a Statement File, and mustn't be uploaded onto Remote server
                        klsInvestIdees_Attachment.FileName = stAtts[j].FileName;
                        klsInvestIdees_Attachment.FileFullPath = stAtts[j].FullFilePath;
                        klsInvestIdees_Attachment.ServerFileName = stAtts[j].ServerFileName;
                        klsInvestIdees_Attachment.UploadFilePath = stAtts[j].UploadFilePath;
                        if (stAtts[j].Rec_ID == 0) {                                              // Rec_ID = 0 - новый не заруженный ранее файл, Rec_ID <> 0 - уже загруженный файл  
                            i = klsInvestIdees_Attachment.InsertRecord();
                            rAtts = stAtts[j];
                            rAtts.Rec_ID = i;                  
                            stAtts[j] = rAtts;
                        }
                        else klsInvestIdees_Attachment.EditRecord();
                    }
                }
            }            

            //--- Phone Files -----------
            for (i = 1; i <= fgCalls.Rows.Count - 1; i++)
            {
                if ((fgCalls[i, 0] + "") != "")
                {
                    if ((fgCalls[i, 3] + "") == "")
                    {                                 // new upload file - not uploaded 
                        sTemp = Global.DMS_UploadFile(fgCalls[i, 2] + "", "Customers/" + ucCS.txtContractTitle.Text + "/InvestProposals/" + iRec_ID, iRec_ID + "_Call_" + "_" + fgCalls[i, 0]);
                        fgCalls[i, 3] = Path.GetFileName(sTemp);               // only file name
                        fgCalls[i, 4] = sTemp;                                 // upload file path
                    }

                    klsInvestIdees_Attachment = new clsInvestIdees_Attachments();
                    if (Convert.ToInt32(fgCalls[i, "ID"]) != 0)
                    {
                        klsInvestIdees_Attachment.Record_ID = Convert.ToInt32(fgCalls[i, "ID"]);
                        klsInvestIdees_Attachment.GetRecord();
                    }

                    klsInvestIdees_Attachment.II_ID = iRec_ID;
                    klsInvestIdees_Attachment.Share_ID = 0;                                  // Share_ID = 0 means that it CALL file - its't an attach file
                    klsInvestIdees_Attachment.DocType_ID = -2;                               // -2 - it's a Call File, and mustn't be uploaded onto Remote server
                    klsInvestIdees_Attachment.FileName = fgCalls[i, 0] + "";
                    klsInvestIdees_Attachment.FileFullPath = fgCalls[i, 2] + "";
                    klsInvestIdees_Attachment.ServerFileName = fgCalls[i, 3] + "";
                    klsInvestIdees_Attachment.UploadFilePath = fgCalls[i, 4] + "";
                    if (Convert.ToInt32(fgCalls[i, "ID"]) == 0) fgCalls[i, "ID"] = klsInvestIdees_Attachment.InsertRecord();
                    else klsInvestIdees_Attachment.EditRecord();
                }
            }

            //--- Codes Files ----------------
            iAttachedFilesCount = 0;
            iUploadedFilesCount = 0;
            iRemotedFilesCount = 0;
            for (j = 0; j <= stAtts.Count - 1; j++) {
                if (stAtts[j].FileName != "" && stAtts[j].Share_ID != -999) { 
                     // -999 -это все записи в stAtts, которые не должны быть сохранены 
                     //  есть 2 причины для этого: если продукт редактировался, то все его "старые" записи из stAtts помечаются -999 и вместо них сохраняются новые
                     //  или если нажималась кнопка очистки и все "старые" записи из stAtts должны быть потеряны

                    iAttachedFilesCount = iAttachedFilesCount + 1;
                    if (stAtts[j].UploadFilePath == "") {
                        sTemp = iRec_ID + "_" + stAtts[j].Share_ID + "_" + stAtts[j].Rec_ID + Path.GetExtension(stAtts[j].FullFilePath);
                        sTemp = Global.DMS_UploadFile(stAtts[j].FullFilePath, "Customers/" + ucCS.txtContractTitle.Text + "/InvestProposals/" + iRec_ID, sTemp);
                        if (sTemp.Length > 0) {                                              // sTemp.Length > 0 means that file was uploaded seccessfully
                            rAtts = stAtts[j];
                            rAtts.ServerFileName = Path.GetFileName(sTemp);                  // only file name
                            rAtts.UploadFilePath = sTemp;                                    // upload file path
                            stAtts[j] = rAtts;
                            iUploadedFilesCount = iUploadedFilesCount + 1;
                            iRemotedFilesCount = iRemotedFilesCount + 1;
                        }
                    }

                    klsInvestIdees_Attachment = new clsInvestIdees_Attachments();
                    if (stAtts[j].Rec_ID != 0) {
                        klsInvestIdees_Attachment.Record_ID = stAtts[j].Rec_ID;
                        klsInvestIdees_Attachment.GetRecord();
                    }

                    klsInvestIdees_Attachment.II_ID = iRec_ID;
                    klsInvestIdees_Attachment.Share_ID = stAtts[j].Share_ID;
                    klsInvestIdees_Attachment.DocType_ID = stAtts[j].DocType_ID;
                    klsInvestIdees_Attachment.FileName = stAtts[j].FileName + "";
                    klsInvestIdees_Attachment.FileFullPath = stAtts[j].FullFilePath + "";
                    klsInvestIdees_Attachment.ServerFileName = stAtts[j].ServerFileName + "";
                    klsInvestIdees_Attachment.UploadFilePath = stAtts[j].UploadFilePath + "";
                    if (stAtts[j].Rec_ID == 0) {
                        i = klsInvestIdees_Attachment.InsertRecord();
                        rAtts = stAtts[j];
                        rAtts.Rec_ID = i;
                        stAtts[j] = rAtts;
                    }
                    else klsInvestIdees_Attachment.EditRecord();


                    if (stAtts[j].WasEdited == 1) {                                            // WasEdited = 1, so ...
                        if (stAtts[j].UploadFilePath != "") {                                  // ... if UploadFilePath is not Empty ... 

                            clsServerJobs klsServerJob = new clsServerJobs();                  // ... add ServerJob for RemoteUpload 
                            klsServerJob.DateStart = Convert.ToDateTime("1900/01/01");
                            klsServerJob.DateFinish = Convert.ToDateTime("1900/01/01");
                            klsServerJob.JobType_ID = 17;                                       // 17   - upload file to remote server (http://rds.hfswiss.ch:2121)
                            klsServerJob.Source_ID = iRec_ID;
                            klsServerJob.Parameters = stAtts[j].UploadFilePath + "~http://rds.hfswiss.ch:2121~" +
                                                      stAtts[j].ServerFileName + "~" + stAtts[j].ServerFileName + "~" + stAtts[j].Rec_ID;   //<SourceFullFileName>~<TargetPath>~sNewFileName~<InvestIdees_Attachment.ID>
                            klsServerJob.Status = 0;
                            klsServerJob.InsertRecord();
                        }
                    }

                }
            }

            klsInvestIdees = new clsInvestIdees();
            klsInvestIdees.Record_ID = iRec_ID;
            klsInvestIdees.GetRecord();
            klsInvestIdees.AttachedFilesCount = iAttachedFilesCount;
            klsInvestIdees.UploadedFilesCount = iUploadedFilesCount;
            klsInvestIdees.RemotedFilesCount = iRemotedFilesCount;
            klsInvestIdees.EditRecord();

            if (iStatus == 2)                     // iStatus = 2 only in one case - if it's a telephone proposal and it has one or more CALL files and PDF file was created.Status = 2 - was sent from Server
                Insert_InvestIdees_Commands_Telephone();

            iAktion = 1;
            bWasSaved = true;

            this.Refresh();
            this.Cursor = Cursors.Default;

        }
        private void SaveTitle()
        {
            int i = 0, j = 0, k = 0;

            //--- Define Proposal LineStatus: 0 - one or more lines in fgCodes has problem with documents, 1 - all lines in fgCodes are OK
            iLine_Status = 1;
            for (i = 1; i <= fgCodes.Rows.Count - 1; i++)
                if (Convert.ToInt32(fgCodes[i, "LineStatus"]) == 0)
                {
                    iLine_Status = 0;
                    break;
                }

            sProducts = "";
            if (fgCodes.Rows.Count > 1)
            {
                if (fgCodes.Rows.Count > 2)
                {
                    j = 0;                                                // agores
                    k = 0;                                                // poliseis
                    for (i = 1; i <= fgCodes.Rows.Count - 1; i++)
                    {
                        if ((fgCodes[i, 0] + "") == "BUY") j = j + 1;
                        else k = k + 1;
                    }
                    sProducts = "Αγορές :  " + j + ". Πωλήσεις: " + k;
                }
                else sProducts = fgCodes[1, 1] + "";
            }

            clsInvestIdees klsInvestIdees = new clsInvestIdees();
            if (iAktion != 0)
            {
                klsInvestIdees.Record_ID = iRec_ID;
                klsInvestIdees.GetRecord();
            }
            klsInvestIdees.SendMethod = Convert.ToInt32(cmbInformMethods.SelectedValue);
            klsInvestIdees.Description_ID = 0;
            klsInvestIdees.AktionDate = dSend.Value;
            //klsInvestIdees.PeriodNumber = iPeriodNumber;
            klsInvestIdees.Client_ID = iClient_ID;
            klsInvestIdees.CC_ID = Convert.ToInt32(cmbCC.SelectedValue);
            klsInvestIdees.AUM = Convert.ToSingle(txtAUM.Text);
            klsInvestIdees.Currency = lblCurrency.Text;
            klsInvestIdees.Products = sProducts;
            klsInvestIdees.IdeasText = txtIdeasText.Text;
            klsInvestIdees.CostBenefits = sCostBenefits;
            klsInvestIdees.StatementFile = lnkStatement.Text + "";
            klsInvestIdees.ProposalPDFile = lnkPDF.Text + "";
            klsInvestIdees.SentDate = dSentDate;
            klsInvestIdees.Status = iStatus;
            klsInvestIdees.LineStatus = iLine_Status;
            klsInvestIdees.Notes = txtNotes.Text;
            if (iAktion == 0) {
                klsInvestIdees.Advisor_ID = iAdvisor_ID;
                klsInvestIdees.User_ID = Global.User_ID;
                klsInvestIdees.WebPassword = Global.GenerateCode();
                klsInvestIdees.RecievedOrder = 0;
                iRec_ID = klsInvestIdees.InsertRecord();
                Global.DMS_CreateDirectory("Customers/" + ucCS.txtContractTitle.Text + "/InvestProposals/" + iRec_ID);
            }
            else klsInvestIdees.EditRecord();

            clsInvestIdees_Customers klsInvestIdees_Customer = new clsInvestIdees_Customers();
            if (iAktion != 0) {
                klsInvestIdees_Customer.II_ID = iRec_ID;
                klsInvestIdees_Customer.GetRecord_II_ID();
            }
            klsInvestIdees_Customer.II_ID = iRec_ID;
            klsInvestIdees_Customer.Client_ID = iClient_ID;
            klsInvestIdees_Customer.Contract_ID = iContract_ID;
            klsInvestIdees_Customer.Contract_Details_ID = iContract_Details_ID;
            klsInvestIdees_Customer.Contract_Packages_ID = iContract_Packages_ID;
            klsInvestIdees_Customer.StockCompany_ID = iStockCompany_ID;
            klsInvestIdees_Customer.Code = lblClientCode.Text;
            klsInvestIdees_Customer.Portfolio = lblPortfolio.Text;
            if (iAktion == 0) klsInvestIdees_Customer.InsertRecord();
            else klsInvestIdees_Customer.EditRecord();
            iAktion = 1;
        }
        private void SaveRecord(int i)              // i - номер строки в гриде fgCodes
        {
            int j = 0, k = 0;

            clsInvestIdees_Attachments klsInvestIdees_Attachment = new clsInvestIdees_Attachments();
            for (j = 0; j <= stAtts.Count - 1; j++) {
                if (stAtts[j].Share_ID == iShare_ID) {

                    klsInvestIdees_Attachment = new clsInvestIdees_Attachments();
                    if (stAtts[j].Rec_ID != 0) {
                        klsInvestIdees_Attachment.Record_ID = stAtts[j].Rec_ID;
                        klsInvestIdees_Attachment.GetRecord();
                    }

                    klsInvestIdees_Attachment.II_ID = iRec_ID;
                    klsInvestIdees_Attachment.Share_ID = stAtts[j].Share_ID;
                    klsInvestIdees_Attachment.DocType_ID = stAtts[j].DocType_ID;
                    klsInvestIdees_Attachment.FileName = stAtts[j].FileName + "";
                    klsInvestIdees_Attachment.FileFullPath = stAtts[j].FullFilePath + "";
                    klsInvestIdees_Attachment.ServerFileName = stAtts[j].ServerFileName + "";
                    klsInvestIdees_Attachment.UploadFilePath = stAtts[j].UploadFilePath + "";
                    if (stAtts[j].Rec_ID == 0) {
                        k = klsInvestIdees_Attachment.InsertRecord();
                        rAtts = stAtts[j];
                        rAtts.Rec_ID = k;
                        stAtts[j] = rAtts;
                    }
                    else klsInvestIdees_Attachment.EditRecord();


                    if (stAtts[j].WasEdited == 1) {                                            // WasEdited = 1, so ...
                        if (stAtts[j].UploadFilePath != "") {                                  // ... if UploadFilePath is not Empty ... 

                            clsServerJobs klsServerJob = new clsServerJobs();                  // ... add ServerJob for RemoteUpload 
                            klsServerJob.DateStart = Convert.ToDateTime("1900/01/01");
                            klsServerJob.DateFinish = Convert.ToDateTime("1900/01/01");
                            klsServerJob.JobType_ID = 17;                                       // 17   - upload file to remote server (http://rds.hfswiss.ch:2121)
                            klsServerJob.Source_ID = iRec_ID;
                            klsServerJob.Parameters = stAtts[j].UploadFilePath + "~http://rds.hfswiss.ch:2121~" +
                                                      stAtts[j].ServerFileName + "~" + stAtts[j].ServerFileName + "~" + stAtts[j].Rec_ID;   //<SourceFullFileName>~<TargetPath>~sNewFileName~<InvestIdees_Attachment.ID>
                            klsServerJob.Status = 0;
                            klsServerJob.InsertRecord();

                            rAtts = stAtts[j];
                            rAtts.WasEdited = 0;
                            stAtts[j] = rAtts;
                        }
                    }
                }
            }

            clsInvestIdees_Products klsInvestIdees_Product = new clsInvestIdees_Products();
            if (Convert.ToInt32(fgCodes[i, "ID"]) != 0) {
                klsInvestIdees_Product.Record_ID = Convert.ToInt32(fgCodes[i, "ID"]);
                klsInvestIdees_Product.GetRecord();
            }
            klsInvestIdees_Product.II_ID = iRec_ID;
            klsInvestIdees_Product.ShareCodes_ID = Convert.ToInt32(fgCodes[i, "Share_ID"]);
            klsInvestIdees_Product.Product_ID = Convert.ToInt32(fgCodes[i, "Product_ID"]);
            klsInvestIdees_Product.ProductCategories_ID = Convert.ToInt32(fgCodes[i, "ProductCategory_ID"]);
            klsInvestIdees_Product.Currency = fgCodes[i, "Currency"] + "";
            klsInvestIdees_Product.StockExchange_ID = Convert.ToInt32(fgCodes[i, "StockExchange_ID"]);
            klsInvestIdees_Product.Energia = Convert.ToInt32(fgCodes[i, "Energia"]);
            klsInvestIdees_Product.Aktion = ((fgCodes[i, "Aktion"] + "") == "BUY" ? 1 : 2);
            klsInvestIdees_Product.Constant = Convert.ToInt32(fgCodes[i, "Constant"]);
            klsInvestIdees_Product.ConstantDate = fgCodes[i, "ConstantDate"] + "";
            klsInvestIdees_Product.Type = Convert.ToInt32(fgCodes[i, "PriceType"]);
            klsInvestIdees_Product.Price = fgCodes[i, "Price"] + "";
            klsInvestIdees_Product.PriceUp = fgCodes[i, "Stop"] + "";
            klsInvestIdees_Product.PriceDown = fgCodes[i, "Loss"] + "";
            klsInvestIdees_Product.Quantity = fgCodes[i, "Quantity"] + "";
            klsInvestIdees_Product.Amount = fgCodes[i, "Axia"] + "";
            klsInvestIdees_Product.Weight = fgCodes[i, "Weight"] + "";
            klsInvestIdees_Product.AttachFiles = Convert.ToInt32(fgCodes[i, "AttachedFiles"]);
            klsInvestIdees_Product.LineStatus = Convert.ToInt32(fgCodes[i, "LineStatus"]);
            klsInvestIdees_Product.Notes = fgCodes[i, "ProductNotes"] + "";
            klsInvestIdees_Product.URL_IR = fgCodes[i, "URL_IR"] + "";
            klsInvestIdees_Product.SummaryLink = "";
            if (Convert.ToInt32(fgCodes[i, "ID"]) == 0) fgCodes[i, "ID"] = klsInvestIdees_Product.InsertRecord();
            else klsInvestIdees_Product.EditRecord();
        }
        private void Insert_InvestIdees_Commands_Telephone()
        {
            int i = 0;
            clsInvestIdees_Commands InvestIdees_Commands = new clsInvestIdees_Commands();
            for (i = 1; i <= fgCodes.Rows.Count - 1; i++) {
                InvestIdees_Commands = new clsInvestIdees_Commands();
                InvestIdees_Commands.DateIns = DateTime.Now;
                InvestIdees_Commands.II_ID = iRec_ID;
                InvestIdees_Commands.Contract_ID = iContract_ID;
                InvestIdees_Commands.Contract_Details_ID = iContract_Details_ID;
                InvestIdees_Commands.Contract_Packages_ID = iContract_Packages_ID;
                InvestIdees_Commands.Client_ID = iClient_ID;
                InvestIdees_Commands.Code = lblClientCode.Text;
                InvestIdees_Commands.Portfolio = lblPortfolio.Text;
                InvestIdees_Commands.Aktion = ((fgCodes[i, 0] + "") == "BUY" ? 1 : 2);
                InvestIdees_Commands.Share_ID = Convert.ToInt32(fgCodes[i, 14]);
                InvestIdees_Commands.Product_ID = Convert.ToInt32(fgCodes[i, 15]);
                InvestIdees_Commands.ProductCategory_ID = Convert.ToInt32(fgCodes[i, 16]);
                InvestIdees_Commands.Quantity = fgCodes[i, 9] + "";
                InvestIdees_Commands.Amount = fgCodes[i, 10] + "";
                InvestIdees_Commands.PriceType = Convert.ToInt32(fgCodes[i, 18]);
                InvestIdees_Commands.Price = fgCodes[i, 8] + "";
                InvestIdees_Commands.PriceUp = fgCodes[i, 19] + "";
                InvestIdees_Commands.PriceDown = fgCodes[i, 20] + "";
                InvestIdees_Commands.Curr = fgCodes[i, 5] + "";
                InvestIdees_Commands.Constant = Convert.ToInt32(fgCodes[i, 22]);
                InvestIdees_Commands.ConstantDate = fgCodes[i, 23] + "";
                InvestIdees_Commands.StockCompany_ID = Convert.ToInt32(iStockCompany_ID);
                InvestIdees_Commands.StockExchange_ID = Convert.ToInt32(fgCodes[i, 17]);
                InvestIdees_Commands.ConfirmationStatus = 0;
                InvestIdees_Commands.ConfirmationDate = Convert.ToDateTime("1900/01/01");
                InvestIdees_Commands.Command_ID = 0;
                InvestIdees_Commands.RecieveDate = Convert.ToDateTime("1900/01/01");
                InvestIdees_Commands.RecieveMethod_ID = 0;
                InvestIdees_Commands.Status = 1;
                InvestIdees_Commands.InsertRecord();
            }
        }
        #endregion
        #region --- common functions --------------------------------------
        private void lnkEpiloges_LinkClicked(Object sender, LinkLabelLinkClickedEventArgs e)
        {
            switch (txtAction.Text) {
                case "BUY":
                    chk1_Buy.Checked = false;
                    chk2_Buy.Checked = false;
                    chk3_Buy.Checked = false;
                    chk4_Buy.Checked = false;
                    chk5_Buy.Checked = false;
                    chk6_Buy.Checked = false;
                    chk7_Buy.Checked = false;
                    chk8_Buy.Checked = false;
                    chk9_Buy.Checked = false;
                    chk10_Buy.Checked = false;
                    panEpilogesBuy.Visible = true;
                    break;
                case "SELL":
                    chk1_Sell.Checked = false;
                    chk2_Sell.Checked = false;
                    chk3_Sell.Checked = false;
                    chk4_Sell.Checked = false;
                    chk5_Sell.Checked = false;
                    chk6_Sell.Checked = false;
                    chk7_Sell.Checked = false;
                    chk8_Sell.Checked = false;
                    panEpilogesSell.Visible = true;
                    break;
            }
        }
        private void btnOK_EpilogesBuy_Click(object sender, EventArgs e)
        {
            if (chk1_Buy.Checked) {
                if (txtProductNotes.Text.Length > 0) txtProductNotes.Text = txtProductNotes.Text + "\n" + chk1_Buy.Text;
                else txtProductNotes.Text = chk1_Buy.Text;
            }

            if (chk2_Buy.Checked) {
                if (txtProductNotes.Text.Length > 0) txtProductNotes.Text = txtProductNotes.Text + "\n" + chk2_Buy.Text;
                else txtProductNotes.Text = chk2_Buy.Text;
            }

            if (chk3_Buy.Checked) {
                if (txtProductNotes.Text.Length > 0) txtProductNotes.Text = txtProductNotes.Text + "\n" + chk3_Buy.Text;
                else txtProductNotes.Text = chk3_Buy.Text;
            }
            if (chk4_Buy.Checked) {
                if (txtProductNotes.Text.Length > 0) txtProductNotes.Text = txtProductNotes.Text + "\n" + chk4_Buy.Text;
                else txtProductNotes.Text = chk4_Buy.Text;
            }
            if (chk5_Buy.Checked)
            {
                if (txtProductNotes.Text.Length > 0) txtProductNotes.Text = txtProductNotes.Text + "\n" + chk5_Buy.Text;
                else txtProductNotes.Text = chk5_Buy.Text;
            }
            if (chk6_Buy.Checked)
            {
                if (txtProductNotes.Text.Length > 0) txtProductNotes.Text = txtProductNotes.Text + "\n" + chk6_Buy.Text;
                else txtProductNotes.Text = chk6_Buy.Text;
            }
            if (chk7_Buy.Checked)
            {
                if (txtProductNotes.Text.Length > 0) txtProductNotes.Text = txtProductNotes.Text + "\n" + chk7_Buy.Text;
                else txtProductNotes.Text = chk7_Buy.Text;
            }
            if (chk8_Buy.Checked)
            {
                if (txtProductNotes.Text.Length > 0) txtProductNotes.Text = txtProductNotes.Text + "\n" + chk8_Buy.Text;
                else txtProductNotes.Text = chk8_Buy.Text;
            }
            if (chk9_Buy.Checked)
            {
                if (txtProductNotes.Text.Length > 0) txtProductNotes.Text = txtProductNotes.Text + "\n" + chk9_Buy.Text;
                else txtProductNotes.Text = chk9_Buy.Text;
            }
            if (chk10_Buy.Checked)
            {
                if (txtProductNotes.Text.Length > 0) txtProductNotes.Text = txtProductNotes.Text + "\n" + chk10_Buy.Text;
                else txtProductNotes.Text = chk10_Buy.Text;
            }

            txtProductNotes.Text = txtProductNotes.Text.Trim();
            panEpilogesBuy.Visible = false;
        }

        private void btnCancel_EpilogesBuy_Click(object sender, EventArgs e)
        {
            panEpilogesBuy.Visible = false;
        }
        private void btnOK_EpilogesSell_Click(object sender, EventArgs e)
        {
            if (chk1_Sell.Checked) {
                if (txtProductNotes.Text.Length > 0) txtProductNotes.Text = txtProductNotes.Text + "\n" + chk1_Sell.Text;
                else txtProductNotes.Text = chk1_Sell.Text;
            }

            if (chk2_Sell.Checked) {
                if (txtProductNotes.Text.Length > 0) txtProductNotes.Text = txtProductNotes.Text + "\n" + chk2_Sell.Text;
                else txtProductNotes.Text = chk2_Sell.Text;
            }
            if (chk3_Sell.Checked) {
                if (txtProductNotes.Text.Length > 0) txtProductNotes.Text = txtProductNotes.Text + "\n" + chk3_Sell.Text;
                else txtProductNotes.Text = chk3_Sell.Text;
            }
            if (chk4_Sell.Checked) {
                if (txtProductNotes.Text.Length > 0) txtProductNotes.Text = txtProductNotes.Text + "\n" + chk4_Sell.Text;
                else txtProductNotes.Text = chk4_Sell.Text;
            }
            if (chk5_Sell.Checked) {
                if (txtProductNotes.Text.Length > 0) txtProductNotes.Text = txtProductNotes.Text + "\n" + chk5_Sell.Text;
                else txtProductNotes.Text = chk5_Sell.Text;
            }
            if (chk6_Sell.Checked) {
                if (txtProductNotes.Text.Length > 0) txtProductNotes.Text = txtProductNotes.Text + "\n" + chk6_Sell.Text;
                else txtProductNotes.Text = chk6_Sell.Text;
            }
            if (chk7_Sell.Checked) {
                if (txtProductNotes.Text.Length > 0) txtProductNotes.Text = txtProductNotes.Text + "\n" + chk7_Sell.Text;
                else txtProductNotes.Text = chk7_Sell.Text;
            }
            if (chk8_Sell.Checked) {
                if (txtProductNotes.Text.Length > 0) txtProductNotes.Text = txtProductNotes.Text + "\n" + chk8_Sell.Text;
                else txtProductNotes.Text = chk8_Sell.Text;
            }

            txtProductNotes.Text = txtProductNotes.Text.Trim();
            panEpilogesSell.Visible = false;
        }

        private void btnCancel_EpilogesSell_Click(object sender, EventArgs e)
        {
            panEpilogesSell.Visible = false;
        }
        private bool CheckAUM()
        {
            bool bOK = true;
            decimal sgAmount = 0;

            if (txtAUM.Text == "0" || !Global.IsNumeric(txtAUM.Text)) {
                MessageBox.Show("Λάθος καταχώριση AUM. Στο πεδίο πρέπει να καταχωρύσετε μόνο αριθμούς.", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                bOK = false;
            }
            else
            {
                sgAmount = Convert.ToDecimal(txtAUM.Text);
                if (sgAmount < 1000)
                {
                    MessageBox.Show("Δεν συμπληρώθηκε σωστά το AUM. Το πεδίο AUM πρέπει να είναι μεγαλύτερο απο 1000.", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    bOK = false;
                    //txtAUM.Focus();
                }
                else
                {
                    panProposal.Enabled = true;
                    grpAttaches.Enabled = true;
                    grpNotes.Enabled = true;
                }
            }

            return bOK;
        }
        private void btnOK_CBA_Click(object sender, EventArgs e)
        {
            string sCostBenefitsM = "", sCostBenefitsNM = "";
            sCostBenefits = "";
            sCostBenefits_Monetary = "";
            sCostBenefits_NonMonetary = "";

            if (chkM1.Checked) {
                sCostBenefitsM = "1~0~0~0~0~0~0~0~";
                sCostBenefits_Monetary = "Monetary (Χρηματικά οφέλη) :  Δεν υπάρχουν";
            }
            else { 
                sCostBenefitsM = "0~";
                sCostBenefits_Monetary = "Monetary (Χρηματικά οφέλη) :  ";
                if (chkM2.Checked || chkM3.Checked || chkM4.Checked || chkM5.Checked) {
                    sCostBenefits_Monetary = "Monetary (Χρηματικά οφέλη) :" + "\n" + "      Προοπτική επίτευξης θετικής απόδοσης μεγαλύτερης του κόστους αλλαγής με:";

                    if (chkM2.Checked) {
                        sCostBenefitsM = sCostBenefitsM + "1~";
                        sCostBenefits_Monetary = sCostBenefits_Monetary + "\n" + "            με προσδοκώμενη τιμή στόχου";
                    }
                    else sCostBenefitsM = sCostBenefitsM + "0~";

                    if (chkM3.Checked) {
                        sCostBenefitsM = sCostBenefitsM + "1~";
                        sCostBenefits_Monetary = sCostBenefits_Monetary + "\n" + "            με προσδοκώμενο Yield";
                    }
                    else sCostBenefitsM = sCostBenefitsM + "0~";

                    if (chkM4.Checked) {
                        sCostBenefitsM = sCostBenefitsM + "1~";
                        sCostBenefits_Monetary = sCostBenefits_Monetary + "\n" + "            με προσδοκώμενη μερισματική απόδοση";
                    }
                    else sCostBenefitsM = sCostBenefitsM + "0~";

                    if (chkM5.Checked) {
                        sCostBenefitsM = sCostBenefitsM + "1~";
                        sCostBenefits_Monetary = sCostBenefits_Monetary + "\n" + "            με προσδοκώμενη νομισματική απόδοση";
                    }
                    else sCostBenefitsM = sCostBenefitsM + "0~";
                }
                else sCostBenefitsM = sCostBenefitsM + "0~0~0~0~";

                if (chkM6.Checked) {
                    sCostBenefitsM = sCostBenefitsM + "1~";
                    sCostBenefits_Monetary = sCostBenefits_Monetary + "\n" + "      Προοπτική νομισματικής απόδοσης , με αλλαγή share class προϊόντος (ίδιου/άλλου/όμοιου)";
                }
                else sCostBenefitsM = sCostBenefitsM + "0~";

                if (chkM7.Checked) {
                    sCostBenefitsM = sCostBenefitsM + "1~";
                    sCostBenefits_Monetary = sCostBenefits_Monetary + "\n" + "      Αλλαγή λόγω μικρότερου κόστους διατήρησης προϊόντος";
                }
                else sCostBenefitsM = sCostBenefitsM + "0~";

                if (chkM8.Checked) {
                    sCostBenefitsM = sCostBenefitsM + "1~";
                    sCostBenefits_Monetary = sCostBenefits_Monetary + "\n" + "      Λόγω μείωσης φορολογίας";
                }
                else sCostBenefitsM = sCostBenefitsM + "0~";
            }

            if (chkN1.Checked) {
                sCostBenefitsNM = "1~0~0~0~0~0~0~0~0~0~0~0~0~0~0~0~0~0~0~";
                sCostBenefits_NonMonetary = "Non Monetary (Μη χρηματικά οφέλη) :  Δεν υπάρχουν";
            }
            else {
                sCostBenefitsNM = "0~";
                sCostBenefits_NonMonetary = "Non Monetary (Μη χρηματικά οφέλη) :  ";

                if (chkN2.Checked || chkN3.Checked || chkN4.Checked || chkN5.Checked || chkN6.Checked || chkN7.Checked || chkN8.Checked || chkN9.Checked || chkN10.Checked || chkN11.Checked || chkN12.Checked) {
                    sCostBenefits_NonMonetary = "Non Monetary (Μη χρηματικά οφέλη) :" + "\n" + "      Μείωση κινδύνων:";
                    if (chkN2.Checked) {
                        sCostBenefitsNM = sCostBenefitsNM + "1~";
                        sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n" + "            Επενδυτικού κινδύνου (risk off)";
                    }
                    else sCostBenefitsNM = sCostBenefitsNM + "0~";

                    if (chkN3.Checked) {
                        sCostBenefitsNM = sCostBenefitsNM + "1~";
                        sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n" + "            Πιστωτικού κινδύνου (credit risk)";
                    }
                    else sCostBenefitsNM = sCostBenefitsNM + "0~";

                    if (chkN4.Checked) {
                        sCostBenefitsNM = sCostBenefitsNM + "1~";
                        sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n" + "            Κίνδυνος υπερσυγκέντρωσης σε κλάδο";
                    }
                    else sCostBenefitsNM = sCostBenefitsNM + "0~";

                    if (chkN5.Checked) {
                        sCostBenefitsNM = sCostBenefitsNM + "1~";
                        sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n" + "            Κίνδυνος υπερσυγκέντρωσης σε χώρα";
                    }
                    else sCostBenefitsNM = sCostBenefitsNM + "0~";

                    if (chkN6.Checked) {
                        sCostBenefitsNM = sCostBenefitsNM + "1~";
                        sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n" + "            Κίνδυνος υπερσυγκέντρωσης σε εκδότη";
                    }
                    else sCostBenefitsNM = sCostBenefitsNM + "0~";

                    if (chkN7.Checked) {
                        sCostBenefitsNM = sCostBenefitsNM + "1~";
                        sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n" + "            Νομισματικού κινδύνου";
                    }
                    else sCostBenefitsNM = sCostBenefitsNM + "0~";

                    if (chkN8.Checked) {
                        sCostBenefitsNM = sCostBenefitsNM + "1~";
                        sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n" + "            Κίνδυνος μεταβλητότητας";
                    }
                    else sCostBenefitsNM = sCostBenefitsNM + "0~";

                    if (chkN9.Checked) {
                        sCostBenefitsNM = sCostBenefitsNM + "1~";
                        sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n" + "            Κίνδυνος επιτοκίου";
                    }
                    else sCostBenefitsNM = sCostBenefitsNM + "0~";

                    if (chkN10.Checked) {
                        sCostBenefitsNM = sCostBenefitsNM + "1~";
                        sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n" + "            Κίνδυνος πολιτικός";
                    }
                    else sCostBenefitsNM = sCostBenefitsNM + "0~";

                    if (chkN11.Checked) {
                        sCostBenefitsNM = sCostBenefitsNM + "1~";
                        sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n" + "            Κίνδυνος συστημικός";
                    }
                    else sCostBenefitsNM = sCostBenefitsNM + "0~";
                }
                else sCostBenefitsNM = sCostBenefitsNM + "0~0~0~0~0~0~0~0~0~0~";


                if (chkN12.Checked || chkN13.Checked || chkN14.Checked || chkN15.Checked || chkN16.Checked) {
                    sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n" + "      Αύξηση διασποράς σε:";
                    if (chkN12.Checked) {
                        sCostBenefitsNM = sCostBenefitsNM + "1~";
                        sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n" + "            χώρα";
                    }
                    else sCostBenefitsNM = sCostBenefitsNM + "0~";

                    if (chkN13.Checked) {
                        sCostBenefitsNM = sCostBenefitsNM + "1~";
                        sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n" + "            κλάδο";
                    }
                    else
                        sCostBenefitsNM = sCostBenefitsNM + "0~";

                    if (chkN14.Checked) {
                        sCostBenefitsNM = sCostBenefitsNM + "1~";
                        sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n" + "            νόμισμα";
                    }
                    else sCostBenefitsNM = sCostBenefitsNM + "0~";

                    if (chkN15.Checked) {
                        sCostBenefitsNM = sCostBenefitsNM + "1~";
                        sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n" + "            αριθμό προϊόντων";
                    }
                    else sCostBenefitsNM = sCostBenefitsNM + "0~";

                    if (chkN16.Checked) {
                        sCostBenefitsNM = sCostBenefitsNM + "1~";
                        sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n" + "            εκδότη";
                    }
                    else sCostBenefitsNM = sCostBenefitsNM + "0~";
                }
                else sCostBenefitsNM = sCostBenefitsNM + "0~0~0~0~0~";

                if (chkN17.Checked) {
                    sCostBenefitsNM = sCostBenefitsNM + "1~";
                    sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n" + "      Προσαρμογή στη καταλληλότητα των χρηματοπιστωτικών μέσων";
                }
                else sCostBenefitsNM = sCostBenefitsNM + "0~";

                if (chkN18.Checked) {
                    sCostBenefitsNM = sCostBenefitsNM + "1~";
                    sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n" + "      Λόγω μείωσης φορολογίας";
                }
                else sCostBenefitsNM = sCostBenefitsNM + "0~";

                if (chkN19.Checked) {
                    sCostBenefitsNM = sCostBenefitsNM + "1~";
                    sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n" + "      Αύξηση ρευστότητας";
                }
                else sCostBenefitsNM = sCostBenefitsNM + "0~";
            }

            sCostBenefits = sCostBenefitsM + sCostBenefitsNM;

            if (sCostBenefitsM.IndexOf("1") < 0 || sCostBenefitsNM.IndexOf("1") < 0)                                   // ничего не отмечено
               MessageBox.Show("Wrong CBA", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else tabRecs.SelectedIndex = 0;
        }
        private void btnCancel_CBA_Click(object sender, EventArgs e)
        {
            chkM1.Checked = false;
            chkM2.Checked = false;
            chkM3.Checked = false;
            chkM4.Checked = false;
            chkM5.Checked = false;
            chkM6.Checked = false;
            chkM7.Checked = false;
            chkM8.Checked = false;
            chkN1.Checked = false;
            chkN2.Checked = false;
            chkN3.Checked = false;
            chkN4.Checked = false;
            chkN5.Checked = false;
            chkN6.Checked = false;
            chkN7.Checked = false;
            chkN8.Checked = false;
            chkN9.Checked = false;
            chkN10.Checked = false;
            chkN11.Checked = false;
            chkN12.Checked = false;
            chkN13.Checked = false;
            chkN14.Checked = false;
            chkN15.Checked = false;
            chkN16.Checked = false;
            chkN17.Checked = false;
            chkN18.Checked = false;
            chkN19.Checked = false;
        }
        private void DefineEnergia()
        {
            if (iShareType != 0) {
                //--- define ENERGIA List ------------
                dtList = new DataTable("TypeList");
                dtList.Columns.Add("Title", typeof(string));
                dtList.Columns.Add("ID", typeof(int));

                switch (iShareType) {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        dtList.Rows.Add("", 0);
                        dtList.Rows.Add(sEnergia[1], 1);      // "Αγορά"
                        dtList.Rows.Add(sEnergia[2], 2);      // "Πώληση"
                        dtList.Rows.Add(sEnergia[5], 5);      // "Διακράτηση"
                        break;
                    case 6:
                        dtList.Rows.Add("", 0);
                        dtList.Rows.Add(sEnergia[3], 3);      // "Εγγραφή" 
                        dtList.Rows.Add(sEnergia[4], 4);      // "Εξαγορά"
                        dtList.Rows.Add(sEnergia[5], 5);      // "Διακράτηση"
                        break;
                }

                bCheckSurname = false;
                cmbEnergia.DataSource = dtList;
                cmbEnergia.DisplayMember = "Title";
                cmbEnergia.ValueMember = "ID";

                if (txtAction.Text != "") {
                    switch (iShareType) {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            if (txtAction.Text == "BUY") cmbEnergia.SelectedValue = 1;
                            else
                                if (txtAction.Text == "SELL") cmbEnergia.SelectedValue = 2;
                            else cmbEnergia.SelectedValue = 5;
                            break;
                        case 6:
                            if (txtAction.Text == "BUY") cmbEnergia.SelectedValue = 3;
                            else
                            if (txtAction.Text == "SELL") cmbEnergia.SelectedValue = 4;
                            else cmbEnergia.SelectedValue = 5;
                            break;
                    }
                }
                bCheckSurname = true;
            }
        }
        private void ShowProductLabels(int iProductType)
        {
            lstType.DisplayMember = "Title";
            lstType.ValueMember = "ID";
            dtList = new DataTable("TypeList");
            dtList.Columns.Add("Title", typeof(string));
            dtList.Columns.Add("ID", typeof(int));
            picBondCalc.Visible = false;

            switch (iProductType) {
                case 0:
                    lblQuantity.Text = "Ποσότητα";
                    break;
                case 1:                                                  // 1-Shares                                     
                    lblQuantity.Text = "Τεμάχια";
                    if (txtAction.Text == "BUY") {
                        dtList.Rows.Add("Limit", 0);
                        dtList.Rows.Add("Scenario", 3);
                        dtList.Rows.Add("ATC", 4);
                        dtList.Rows.Add("ATO", 5);
                    }
                    else {
                        dtList.Rows.Add("Limit", 0);
                        dtList.Rows.Add("Stop", 2);
                        dtList.Rows.Add("ATC", 4);
                        dtList.Rows.Add("ATO", 5);
                    }
                    panMandatoryAttaches.Visible = false;
                    bCheckMandatoryFiles = false;
                    break;
                case 2:                                           // 2 - Bond
                    lblQuantity.Text = "Ονομαστική Αξία";
                    dtList.Rows.Add("Limit", 0);
                    dtList.Rows.Add("Market", 1);
                    picBondCalc.Visible = true;
                    panMandatoryAttaches.Visible = false;
                    bCheckMandatoryFiles = false;
                    break;
                case 4:                                           //    4 - ETF                                     
                    lblQuantity.Text = "Τεμάχια";
                    if (txtAction.Text == "BUY") {
                        dtList.Rows.Add("Limit", 0);
                        dtList.Rows.Add("Scenario", 3);
                        dtList.Rows.Add("ATC", 4);
                        dtList.Rows.Add("ATO", 5);
                    }
                    else {
                        dtList.Rows.Add("Limit", 0);
                        dtList.Rows.Add("Stop", 2);
                        dtList.Rows.Add("ATC", 4);
                        dtList.Rows.Add("ATO", 5);
                    }

                    if (txtAction.Text == "BUY") {
                        fgCodesMandatoryAttaches.Rows.Count = 1;
                        dtView = Global.dtMandatoryFiles.DefaultView;
                        dtView.RowFilter = "ProductType_ID = 4 AND Status = 1";
                        foreach (DataRowView dtViewRow in dtView)
                            fgCodesMandatoryAttaches.AddItem(dtViewRow["Title"] + "\t" + "" + "\t" + "" + "\t" + "0" + "\t" + dtViewRow["ID"] + "\t" + 
                                                             "" + "\t" + "" + "\t" + "" + "\t" + "0" + "\t" + "1");

                        panMandatoryAttaches.Visible = true;
                        bCheckMandatoryFiles = true;
                    }
                    else {
                        fgCodesMandatoryAttaches.Rows.Count = 1;
                        dtView = Global.dtMandatoryFiles.DefaultView;
                        dtView.RowFilter = "ProductType_ID = 4 AND Status = 1 AND ID = 7";
                        foreach (DataRowView dtViewRow in dtView)
                            fgCodesMandatoryAttaches.AddItem(dtViewRow["Title"] + "\t" + "" + "\t" + "" + "\t" + "0" + "\t" + dtViewRow["ID"] + "\t" + 
                                                             "" + "\t" + "" + "\t" + "" + "\t" + "0" + "\t" + "1");

                        panMandatoryAttaches.Visible = true;
                        bCheckMandatoryFiles = true;
                    }
                    break;
                case 6:                                           // Fund
                    lblQuantity.Text = "Μερίδια";
                    dtList.Rows.Add("Market", 1);

                    if (txtAction.Text == "BUY") {
                        fgCodesMandatoryAttaches.Rows.Count = 1;
                        dtView = Global.dtMandatoryFiles.DefaultView;
                        dtView.RowFilter = "ProductType_ID = 6 AND Status = 1";
                        foreach (DataRowView dtViewRow in dtView)
                            fgCodesMandatoryAttaches.AddItem(dtViewRow["Title"] + "\t" + "" + "\t" + "" + "\t" + "0" + "\t" + dtViewRow["ID"] + "\t" +
                                                             "" + "\t" + "" + "\t" + "" + "\t" + "0" + "\t" + "1");

                        panMandatoryAttaches.Visible = true;
                        bCheckMandatoryFiles = true;
                    }
                    else {
                        fgCodesMandatoryAttaches.Rows.Count = 1;
                        dtView = Global.dtMandatoryFiles.DefaultView;
                        dtView.RowFilter = "ProductType_ID = 6 AND Status = 1 AND ID = 1";
                        foreach (DataRowView dtViewRow in dtView)
                            fgCodesMandatoryAttaches.AddItem(dtViewRow["Title"] + "\t" + "" + "\t" + "" + "\t" + "0" + "\t" + dtViewRow["ID"] + "\t" +
                                                             "" + "\t" + "" + "\t" + "" + "\t" + "0" + "\t" + "1");

                        panMandatoryAttaches.Visible = true;
                        bCheckMandatoryFiles = true;
                    }
                    break;
                default:
                    dtList.Rows.Add("Limit", 0);
                    dtList.Rows.Add("Market", 1);
                    dtList.Rows.Add("Stop", 2);
                    dtList.Rows.Add("Scenario", 3);
                    dtList.Rows.Add("ATC", 4);
                    dtList.Rows.Add("ATO", 5);
                    panMandatoryAttaches.Visible = false;
                    bCheckMandatoryFiles = false;
                    break;
            }
            lstType.DataSource = dtList;
        }
        private void DefineCustomerView(bool bOnOff)
        {
            if (bOnOff) {
                panHeader.Height = 424;
                picDown.Visible = false;
                panCustomerDetails.Visible = true;
                lblCC.Top = 394;
                cmbCC.Top = 394;
                lblCC_EMail_Title.Top = 394;
                lblCC_Email.Top = 394;
            }
            else {
                panHeader.Height = 162;
                picDown.Visible = true;
                panCustomerDetails.Visible = false;
                lblCC.Top = 136;
                cmbCC.Top = 136;
                lblCC_EMail_Title.Top = 136;
                lblCC_Email.Top = 136;
            }
        }
        private bool IsInt(string sVal)
        {
            foreach (char c in sVal)
            {
                int iN = (int)c;
                if ((iN > 57) || (iN < 48))
                    return false;
            }
            return true;
        }    
        private void SwitchOnOffHeader(bool bOnOff) {
            ucCS.Enabled = bOnOff;
            txtAUM.Enabled = bOnOff;
            cmbInformMethods.Enabled = bOnOff;
            panCustomerDetails.Enabled = bOnOff;
            cmbCC.Enabled = bOnOff;
        }
        private void SwitchOnOffButtons(bool bOnOff) {
            tsCodes.Enabled = bOnOff;
            picAddCall.Visible = bOnOff;
            picDelCall.Visible = bOnOff;
        }
        protected void ucCS_TextOfLabelChanged(object sender, EventArgs e)
        {
            Global.ContractData stContract = new Global.ContractData();
            stContract = ucCS.SelectedContractData;
            if (stContract.MIFID_2 == 1) {               
                
                lblClientName.Text = stContract.ClientName;
                lblClientCode.Text = stContract.Code;
                lblPortfolio.Text = stContract.Portfolio;
                lblEP.Text = stContract.Policy_Title;
                lblEProfile.Text = stContract.Profile_Title;
                lblService.Text = stContract.Service_Title;
                lblEMail.Text = stContract.EMail;
                lblMobile.Text = stContract.Mobile;
                chkXAA.Checked = stContract.XAA == 1 ? true : false;
                txtAUM.Text = "0"; //stContract.AUMs;
                lblCurrency.Text = stContract.Currency;
                iClient_ID = stContract.Client_ID;
                iContract_ID = stContract.Contract_ID;
                iContract_Details_ID = stContract.Contracts_Details_ID;
                iContract_Packages_ID = stContract.Contracts_Packages_ID;
                iStockCompany_ID = stContract.Provider_ID;
                iInvestPolicy_ID = stContract.Policy_ID;
                sProviderTitle = stContract.Provider_Title;
                iMIFIDCategory_ID = stContract.MIFIDCategory_ID;
                iMiFID_Risk = stContract.MIFID_Risk_Index;
                if (stContract.Service_ID == 5) lblInvestPolicy.Text = "Χρημα/τικά μέσα";       // 5 - DealAdvisory
                else lblInvestPolicy.Text = "Επενδ. Πολιτική";                                  // Else - Advisory                

                sGeography = DefineContractGeography(iContract_ID);
        
                DefineComplexProduct();

                if (iAdvisor_ID == 0) {
                    clsContracts klsContract = new clsContracts();
                    klsContract.Record_ID = iContract_ID;
                    klsContract.Contract_Details_ID = iContract_Details_ID;
                    klsContract.Contract_Packages_ID = iContract_Packages_ID;
                    klsContract.GetRecord();
                    iAdvisor_ID = klsContract.Details.User1_ID;
                    sAdvisor = klsContract.AdvisorFullname;
                    sAdvisorEMail = klsContract.AdvisorEMail + "";
                    sAdvisorTel = klsContract.AdvisorTel + "";
                    sAdvisorMobile = klsContract.AdvisorMobile + "";

                    chkWorld.Checked = (klsContract.Details.ChkWorld == 1 ? true : false); ;
                    chkGreece.Checked = (klsContract.Details.ChkGreece == 1 ? true : false); ;
                    chkEurope.Checked = (klsContract.Details.ChkEurope == 1 ? true : false); ;
                    chkAmerica.Checked = (klsContract.Details.ChkAmerica == 1 ? true : false); ;
                    chkAsia.Checked = (klsContract.Details.ChkAsia == 1 ? true : false); ;
                }

                if (iAdvisor_ID == 0 )
                        MessageBox.Show("Δεν έχουν καταχωρυθεί τα στοιχεία του Συμβούλου στη συγκεκρινμένη Σύμβαση.", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
   
                dtList4.Rows.Clear();
                Global.DefineContractProductsList(dtList4, iContract_ID, iContract_Details_ID, iContract_Packages_ID, false);

                txtAction.Focus();
            }
            else {
                MessageBox.Show("Δεν είναι MIFID II Σύμβαση.", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ucCS.txtContractTitle.Focus();
            }
        }
        protected void ucPS_TextChanged(object sender, EventArgs e)
        {
            string sTemp = "", sMessages = "";

            Global.ProductData stProduct = new Global.ProductData();
            stProduct = ucPS.SelectedProductData;
            if (stProduct.OK_Flag == 1) { 
                lblTitle.Text = stProduct.Title;
                lblCode.Text = stProduct.Code;
                lblCode2.Text = stProduct.Code2;
                lblISIN.Text = stProduct.ISIN;
                cmbCurrency.Text = stProduct.Currency;
                cmbStockExchanges.SelectedValue = stProduct.StockExchange_ID;
                iShareType = stProduct.Product_ID;
                cmbProducts.SelectedValue = iShareType;
                iShare_ID = stProduct.ShareCode_ID;
                iProductCategory_ID = stProduct.ProductCategory_ID;
                lblGravityMax.Text = "Max Weigth = " + stProduct.Weight;
                sgGravity = stProduct.Weight; ;
                txtURL_IR.Text = stProduct.URL_ID;

                if (lblCurrency.Text == "EUR") {                                        // Nomisma Anaforas
                    if (cmbCurrency.Text == "EUR") sgCurRate = 1;                       // Nomisma Proiontos
                    else {
                        foundRows = dtEURRates.Select("DateIns <= '" + dSend.Value.ToString("yyyy/MM/dd") + "' AND Currency = 'EUR" + cmbCurrency.Text + "='");
                        if (foundRows.Length > 0) sgCurRate = Convert.ToSingle(foundRows[0]["Rate"]);     // CurrRate
                        else sgCurRate = 1;                                             // Cur Rate not found 
                    }
                }
                else {
                    if (cmbCurrency.Text == "EUR") {                // Nomisma Proiontos
                        foundRows = dtEURRates.Select("DateIns <= '" + dSend.Value.ToString("yyyy/MM/dd") + "' AND Currency = 'EUR" + lblCurrency.Text + "='");
                        if (foundRows.Length > 0) sgCurRate = 1 / Convert.ToSingle(foundRows[0]["Rate"]);   // CurrRate;
                        else sgCurRate = 1;                                              // Cur Rate not found 
                    }
                    else {
                        foundRows = dtEURRates.Select("DateIns <= '" + dSend.Value.ToString("yyyy/MM/dd") + "' AND Currency = 'EUR" + lblCurrency.Text + "='");
                        if (foundRows.Length > 0) sgCurRate = 1 / Convert.ToSingle(foundRows[0]["Rate"]);   // CurrRate
                        else sgCurRate = 1;                          // Cur Rate not found 

                        foundRows = dtEURRates.Select("DateIns <= '" + dSend.Value.ToString("yyyy/MM/dd") + "' AND Currency = 'EUR" + cmbCurrency.Text + "='");
                        if (foundRows.Length > 0) sgPrice = 1 / Convert.ToSingle(foundRows[0]["Rate"]);   // CurrRate
                        else sgPrice = 1;                          // Cur Rate not found 

                        sgCurRate = sgCurRate / sgPrice;
                    }
                }

                lblCurrRate_NomismaAnaforas.Text = lblCurrency.Text + " / " + cmbCurrency.Text + "  = " + sgCurRate.ToString("0.########");
                ShowProductLabels(iShareType);
                lstType.SelectedIndex = 0;

                sgEndektikiTimi = 0;
                lblEndektikiTimi.Text = "";
                if (lblISIN.Text != "") {
                    clsProductsCodes ProductCode = new clsProductsCodes();
                    ProductCode.DateIns = dSend.Value;
                    ProductCode.ISIN = lblISIN.Text;
                    ProductCode.Curr = cmbCurrency.Text + "";
                    ProductCode.GetPrice_ISIN();
                    sgEndektikiTimi = Convert.ToSingle(ProductCode.ClosePrice);
                    lblEndektikiTimi.Text = ProductCode.ClosePrice + "";
                    txtPrice.Text = ProductCode.ClosePrice + "";
                }

                DefineEnergia();
            }
            else  {
                sTemp = stProduct.OK_String + "";

                sMessages = "Δεν είναι κατάλληλο λόγω:";
                if (sTemp.Substring(0, 1) == "0") sMessages = sMessages + "\n - Risk profile";
                if (sTemp.Substring(1, 1) == "0") sMessages = sMessages + "\n - Retail/Professional";
                if (sTemp.Substring(2, 1) == "0") sMessages = sMessages + "\n - Distribution channel";
                if (sTemp.Substring(3, 1) == "0") sMessages = sMessages + "\n - Currency risk";
                if (sTemp.Substring(4, 1) == "0") sMessages = sMessages + "\n - Complex";
                if (sTemp.Substring(5, 1) == "0") sMessages = sMessages + "\n - Γεωγραφικης κατανομης";
                if (sTemp.Substring(6, 1) == "0") sMessages = sMessages + "\n - Ειδικες οδηγιες";

                MessageBox.Show(sMessages, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                EmptyCodeRec();
            }

            bCheckShares = true;
        }
        #endregion
        private void panCode_MouseDown(object sender, MouseEventArgs e)
        {
            this.position = e.Location;
            this.pMove = true;
        }
        private void panCode_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.pMove == true)
                {
                    this.panCode.Location = new Point(this.panCode.Location.X + e.X - this.position.X, this.panCode.Location.Y + e.Y - this.position.Y);
                }
            }
        }
        private void panCode_MouseUp(object sender, MouseEventArgs e)
        {
            this.pMove = false;
        }
        public int Rec_ID { get { return this.iRec_ID; } set { this.iRec_ID = value; } }
        public int Aktion { get { return this.iAktion; } set { this.iAktion = value; } }
    }
}
