using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using CrystalDecisions.CrystalReports.Engine;
using Core;

namespace ISP_IPPDF
{
    public partial class frmMain : Form
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString);
        SqlCommand cmd;
        DataTable dtProductList;
        DataColumn dtCol;
        DataRow dtRow;
        int i, j, m, k, iRec_ID, iError = 1, iSJ_ID=0, iAttempt=0, iMiFID_Risk, iMiFIDCategory_ID, iService_ID, iAttachedFilesCount, iUploadedFilesCount, 
            iClientPackage_ID, iContract_Details_ID, iContract_Packages_ID, iAdvisorID, iYear, iMonth;
        int iNewRows = 0;
        float sgSurveyedKIID;
        bool bUploadError;
        string sClientName, sCode, sPortfolio, sContract, sAdvisor, sInvestPolicy, sService, sProviderTitle, sProviderTitle_PriceTable, sAdvisorTel, sAdvisorEMail, sAdvisorMobile,
               sToposDiapagmatevsis, sAuthor, sAuthorMobile, sAuthorEMail, sIdeasText, sProducts, sInvestProfile, sInvestProfileCustomer, sInvestGoal, sInvestHorisont, sInvestRisk,
               sInvestCurr, sCurrency, sInvestPolicy_Header, sComplexProduct, sGeography, sSpecRules, sPrice, sPDF_FileName, sPDF_FullPath, sCBA_Title, sCBA_Text, sDisclimer_Title,
               sNotes_Title, sContent, sIdeaText, sLink, sCostBenefits, sCostBenefitsM, sCostBenefits_Monetary, sCostBenefitsNM, sCostBenefits_NonMonetary, sDMS_Path, sRatings,
               sRatingReport, sComplexDetails="", sNewRow, sTemp, sStartTime, sFinishTime, sTargetMail;
        string[] sConstant = { "Day Order", "GTC", "GTDate" };
        string[] sEnergia = { "", "Αγορά", "Πώληση", "Εγγραφή", "Εξαγορά", "Διακράτηση" };
        string[] sCategoryMiFID = { "", "Ιδιώτης πελάτης", "Επαγγελματίας πελάτης" };
        string[] sDistrib = { "", "Both", "Professional", "Neither", "Retail" };
        string[] sBondType = { "", "Εταιρικό", "Κρατικό", "Υπερεθνικό" };
        string[] sEkthesiKatalilotitas = { "", "", "", "", "", "", "", "", "", "" };
        Attaches rAtts;
        List<Attaches> stAtts = new List<Attaches>(); //  структура Attaches для хранения всех вложенных файлов, кроме PDF: это файлы-описания продуктов, файл statement, файлы телефонных разговоров):  
                                                      //  Share_ID   - ShareCodes.ID продукта - если Share_ID > 0, то эта запись относится к продукту с ID = Share_ID; если  Share_ID = 0, то это либо StatementFile либо CALL File; если Share_ID = -999, то это строка на удаление
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
        clsInvestIdees InvestIdees = new clsInvestIdees();
        clsInvestIdees_Products InvestIdees_Products = new clsInvestIdees_Products();
        clsProductTitles_ComplexReasons ProductTitles_ComplexReasons = new clsProductTitles_ComplexReasons();
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Global Global = new Global();
            Global.connStr = System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            clsServerJobs ServerJobs = new clsServerJobs();
            ServerJobs.DateStart = DateTime.Now;
            ServerJobs.DateFinish = DateTime.Now;
            ServerJobs.JobType_ID = 61;
            ServerJobs.Source_ID = 0;
            ServerJobs.Status = 0;
            ServerJobs.GetList();

            foreach (DataRow dtRow in ServerJobs.List.Rows)
            {
                iSJ_ID = Convert.ToInt32(dtRow["ID"]);
                iAttempt = Convert.ToInt32(dtRow["Attempt"]);
                CreateInvestProposalPDF(Convert.ToInt32(dtRow["Source_ID"]));
            }
            Thread.Sleep(5000);

            this.Close();

        }
        private void CreateInvestProposalPDF(int iRec_ID)
        {
            //--- create PDF -----------------------------------------------------------    

            sPDF_FullPath = "";
            sPDF_FileName = "";
            sClientName = "";
            sCode = "";
            sPortfolio = "";
            sContract = "";
            sAdvisor = "";
            sAuthor = "";
            sAuthorMobile = "";
            sAuthorEMail = "";
            sProducts = "";
            sInvestProfile = "";
            sInvestProfileCustomer = "";
            sInvestGoal = "";
            sInvestHorisont = "";
            sInvestRisk = "";
            sInvestCurr = "";
            sCurrency = "";
            sRatings = "";
            sRatingReport = "";
            sComplexDetails = "";
            sNewRow = "";
            sInvestPolicy_Header = "";
            sToposDiapagmatevsis = "";
            sComplexProduct = "";
            sGeography = "";
            sSpecRules = "";
            sInvestPolicy = "";
            sService = "";
            sProviderTitle = "";
            sProviderTitle_PriceTable = "";
            sAdvisorTel = "";
            sAdvisorEMail = "";
            sAdvisorMobile = "";
            sIdeasText = "";
            sCostBenefits = "";
            sCostBenefits_Monetary = "";
            sCostBenefits_NonMonetary = "";
            sCBA_Title = "";
            sCBA_Text = "";
            sDisclimer_Title = "";
            sNotes_Title = "";
            sContent = "";
            sDMS_Path = "C:\\Scripts\\ISPServer";   // "C:\\DMS";
            sIdeaText = "";
            sComplexProduct = "";

            iMiFID_Risk = 1;
            iMiFIDCategory_ID = 0;
            iService_ID = 0;
            iAttachedFilesCount = 0;
            iUploadedFilesCount = 0;
            sgSurveyedKIID = 0;
            iClientPackage_ID = 0;
            iContract_Details_ID = 0;
            iContract_Packages_ID = 0;

            bUploadError = false;

            //------------------------------------------------------------------------------
            dtProductList = new DataTable("ProductsList");
            dtCol = dtProductList.Columns.Add("f1", System.Type.GetType("System.String"));          // 1- "ΑΓΟΡΑ", "ΠΩΛΗΣΗ"
            dtCol = dtProductList.Columns.Add("f2", System.Type.GetType("System.String"));          // 2 - Title 
            dtCol = dtProductList.Columns.Add("f3", System.Type.GetType("System.String"));          // 3 - ISIN 
            dtCol = dtProductList.Columns.Add("f4", System.Type.GetType("System.String"));          // 4 - Curr 
            dtCol = dtProductList.Columns.Add("f5", System.Type.GetType("System.String"));          // 5 - Product different parameters in HTML mode 
            dtCol = dtProductList.Columns.Add("f6", System.Type.GetType("System.String"));          // 6 - was Price, now is Product_ID (1-Share, 2-Bond, 4-ETF, 6-Fund)
            dtCol = dtProductList.Columns.Add("f7", System.Type.GetType("System.String"));          // 7 - Quantity 
            dtCol = dtProductList.Columns.Add("f8", System.Type.GetType("System.String"));          // 8 - SRRI  
            dtCol = dtProductList.Columns.Add("f9", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f10", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f11", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f12", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f13", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f14", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f15", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f16", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f17", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f18", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f19", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f20", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f21", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f22", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f23", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f24", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f25", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f26", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f27", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f28", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f29", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f30", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f31", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f32", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f33", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f34", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f35", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f36", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f37", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f38", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f39", System.Type.GetType("System.String"));
            dtCol = dtProductList.Columns.Add("f40", System.Type.GetType("System.String"));

            //--- create file name with name sPDFFileName ----
            sPDF_FileName = "Επενδυτικη Πρόταση " + iRec_ID + ".pdf";

            //--- check Temp folder  -------------
            sPDF_FullPath = Application.StartupPath + "\\Temp";
            //if (!Directory.Exists(sPDF_FullPath)) Directory.CreateDirectory(sPDF_FullPath);

            //--- upload attached files -------------------------
            i = -1;
            stAtts = new List<Attaches>();

            clsInvestIdees_Attachments klsInvestIdees_Attachment = new clsInvestIdees_Attachments();
            klsInvestIdees_Attachment.II_ID = iRec_ID;
            klsInvestIdees_Attachment.GetList();
            foreach (DataRow dtRow1 in klsInvestIdees_Attachment.List.Rows)
            {
                if ((Convert.ToInt32(dtRow1["DocType_ID"]) >= 0) && ((dtRow1["FileName"] + "") != ""))
                {

                    i = i + 1;
                    stAtts.Insert(i, new Attaches
                    {
                        Rec_ID = Convert.ToInt32(dtRow1["ID"]),
                        Share_ID = Convert.ToInt32(dtRow1["Share_ID"]),
                        DocType_Title = dtRow1["DocType_Title"] + "",
                        DocType_ID = Convert.ToInt32(dtRow1["DocType_ID"]),
                        FileName = dtRow1["FileName"] + "",
                        FullFilePath = dtRow1["FileFullPath"] + "",
                        ServerFileName = dtRow1["ServerFileName"] + "",
                        UploadFilePath = dtRow1["UploadFilePath"] + "",
                        RemoteFileName = dtRow1["RemoteFileName"] + "",
                        WasEdited = 0
                    });

                    iAttachedFilesCount = iAttachedFilesCount + 1;
                }
            }
            label3.Text = "Label 2";
            bUploadError = false;
            sStartTime = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");

            for (j = 0; j <= i; j++)
            {
                if (stAtts[j].UploadFilePath != "")
                {
                    m = 0;                                      // upload attempts count                                      // Convert.ToInt32(stAtts[j].UploadAttempts);                                                     
                    if (stAtts[j].RemoteFileName == "")
                    {
                        if (m < 3)
                        {
                            sTemp = stAtts[j].UploadFilePath;

                            klsInvestIdees_Attachment = new clsInvestIdees_Attachments();
                            klsInvestIdees_Attachment.Record_ID = stAtts[j].Rec_ID;
                            klsInvestIdees_Attachment.GetRecord();

                            rAtts = stAtts[j];
                            rAtts.RemoteFileName = RemoteServer2_UploadFile(sTemp.Replace("Q:", "C:\\DMS"), "/Company/InvestProposals_Products", stAtts[j].ServerFileName);
                            stAtts[j] = rAtts;

                            //lblMess2.Text = RemoteServer_UploadFile(sTemp.Replace("Q:\", "C:\DMS\"), "/Company/InvestProposals_Products", stAtts(j).ServerFileName);
                            if (stAtts[j].RemoteFileName != "")
                            {
                                klsInvestIdees_Attachment.RemoteFileName = Path.GetFileName(stAtts[j].RemoteFileName);
                                iUploadedFilesCount = iUploadedFilesCount + 1;
                            }

                            m = m + 1;
                            klsInvestIdees_Attachment.UploadAttempts = m;
                            klsInvestIdees_Attachment.EditRecord();
                        }

                        if (m == 3 && stAtts[j].RemoteFileName == "")
                        {
                            bUploadError = true;
                            Global.SendMail_Web("v.kougioumtzidis@hellasfin.gr", "v.kougioumtzidis@hellasfin.gr", "Kv!26101959", sTargetMail, "",
                                            "Can't upload file " + stAtts[j].UploadFilePath, "Can't upload file " + stAtts[j].UploadFilePath + ". Was done 3 attempts <br />" +
                                            "<br /><br />Finish " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), "", "smtp.office365.com", "", 0, "");
                        }
                    }
                    else iUploadedFilesCount = iUploadedFilesCount + 1;
                }
                else bUploadError = true;
            }
            sFinishTime = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");

            if (iAttachedFilesCount != iUploadedFilesCount) bUploadError = true;

            if (bUploadError)
            {
                iError = 1;          // 1 - Files Upload Error
            }
            else
            {
                InvestIdees = new clsInvestIdees();
                InvestIdees.Record_ID = iRec_ID;
                InvestIdees.GetRecord();
                iYear = InvestIdees.AktionDate.Year;
                iMonth = InvestIdees.AktionDate.Month;
                sIdeasText = InvestIdees.IdeasText + "";

                InvestIdees.AttachedFilesCount = iAttachedFilesCount;
                InvestIdees.UploadedFilesCount = iUploadedFilesCount;
                InvestIdees.UploadStartTime = sStartTime;
                InvestIdees.UploadFinishTime = sFinishTime;
                InvestIdees.EditRecord();

                k = 0;
                clsInvestIdees_Customers InvestIdees_Customers = new clsInvestIdees_Customers();
                InvestIdees_Customers.II_ID = iRec_ID;
                InvestIdees_Customers.GetRecord();
                foreach (DataRow dtRow1 in InvestIdees_Customers.List.Rows)
                {
                    iClientPackage_ID = Convert.ToInt32(dtRow1["Contract_ID"]);
                    iContract_Details_ID = Convert.ToInt32(dtRow1["Contract_Details_ID"]);
                    iContract_Packages_ID = Convert.ToInt32(dtRow1["Contract_Packages_ID"]);
                    sAdvisor = dtRow1["AdvisorName"] + "";
                    sAdvisorEMail = dtRow1["AdvisorEMail"] + "";
                    sAdvisorTel = dtRow1["AdvisorTel"] + "";
                    sAdvisorMobile = dtRow1["AdvisorMobile"] + "";
                    sCostBenefits = dtRow1["CostBenefits"] + "";
                }


                clsContracts klsContract = new clsContracts();
                klsContract.Record_ID = iClientPackage_ID;
                klsContract.Contract_Details_ID = iContract_Details_ID;
                klsContract.Contract_Packages_ID = iContract_Packages_ID;
                klsContract.GetRecord();
                sProviderTitle = klsContract.PackageProvider;
                sProviderTitle_PriceTable = klsContract.PackageProvider_PriceTable;
                sContract = klsContract.ContractTitle;
                sCode = klsContract.Code;
                sPortfolio = klsContract.Portfolio;
                iService_ID = klsContract.Service_ID;
                sService = klsContract.Service_Title;
                sCurrency = klsContract.Currency;
                iMiFIDCategory_ID = klsContract.Details.MIFIDCategory_ID;
                iMiFID_Risk = klsContract.MiFID_Risk;
                iAdvisorID = klsContract.Details.User1_ID;
                sGeography = (klsContract.Details.ChkWorld == 1 ? "Παγκόσμια (όλες οι χώρες του κόσμου)" : "") +
                             (klsContract.Details.ChkGreece == 1 ? "Ελλάδα" : "") +
                             (klsContract.Details.ChkEurope == 1 ? "Ευρώπη" : "") +
                             (klsContract.Details.ChkAmerica == 1 ? "Αμερική" : "") +
                             (klsContract.Details.ChkAsia == 1 ? "Ασία" : "");

                sComplexProduct = "1";                                                                  // 1 - Μη πολύπλοκα χρημ/κά μέσα
                if (klsContract.Details.ChkComplex == 1) sComplexProduct = sComplexProduct + ", 2";     // 2 - Πολύπλοκα χρημ/κά μέσα

                switch (iMiFID_Risk)
                {
                    case 1:
                        sInvestProfile = "Δημιουργία εισοδήματος";
                        sInvestHorisont = "1,5-3 έτη";
                        sInvestRisk = "Χαμηλός";
                        break;
                    case 2:
                        sInvestProfile = "Δημιουργία εισοδήματος";
                        sInvestHorisont = "3-5 έτη";
                        sInvestRisk = "Μεσαίος";
                        break;
                    case 3:
                        sInvestProfile = "Δημιουργία εισοδήματος και επίτευξη κεφαλαιακής ανάπτυξης";
                        sInvestHorisont = "3-5 έτη";
                        sInvestRisk = "Μεσαίος";
                        break;
                    case 4:
                        sInvestProfile = "Δημιουργία εισοδήματος";
                        sInvestHorisont = "5+ έτη";
                        sInvestRisk = "Υψηλός";
                        break;
                    case 5:
                        sInvestProfile = "Δημιουργία εισοδήματος και επίτευξη κεφαλαιακής ανάπτυξης";
                        sInvestHorisont = "5+ έτη";
                        sInvestRisk = "Υψηλός";
                        break;
                    case 6:
                        sInvestProfile = "Επίτευξη κεφαλαιακής ανάπτυξης";
                        sInvestHorisont = "5+ έτη";
                        sInvestRisk = "Υψηλός";
                        break;
                }
                sInvestCurr = "Νόμισμα Αναφοράς ή Ξένο Νόμισμα με αντιστάθμιση στο Νόμισμα Αναφοράς";
                sInvestProfileCustomer = klsContract.ProfileTitle;

                sSpecRules = "";
                if (klsContract.Details.ChkSpecificConstraints == 1)
                {
                    sSpecRules = ((klsContract.Details.ChkMonetaryRisk == 1) ? "Δεν επιθυμεί να αναλάβει νομισματικό κίνδυνο" : "") +
                                  (klsContract.Details.ChkIndividualBonds == 1 ? "Δεν επιθυμεί Μεμονωμένα ομόλογα" : "") +
                      (Convert.ToBoolean(klsContract.Details.ChkMutualFunds) ? "Δεν επιθυμεί Ομολογιακά Αμοιβαία Κεφάλαια" : "") +
                      (Convert.ToBoolean(klsContract.Details.ChkBondedETFs) ? "Δεν επιθυμεί Ομολογιακά Διαπραγματεύσιμα Αμοιβαία Κεφάλαια" : "") +
                      (Convert.ToBoolean(klsContract.Details.ChkIndividualShares) ? "Δεν επιθυμεί Μεμονωμένες Μετοχές" : "") +
                      (Convert.ToBoolean(klsContract.Details.ChkMixedFunds) ? "Δεν επιθυμεί Μετοχικά και Μεικτά Αμοιβαία Κεφάλαια" : "") +
                      (Convert.ToBoolean(klsContract.Details.ChkMixedETFs) ? "Δεν επιθυμεί Μετοχικά και Μεικτά Διαπραγματεύσιμα Αμοιβαία Κεφάλαια" : "") +
                      (Convert.ToBoolean(klsContract.Details.ChkFunds) ? "Δεν επιθυμεί Αμοιβαία Κεφάλαια" : "") +
                      (Convert.ToBoolean(klsContract.Details.ChkETFs) ? "Δεν επιθυμεί Διαπραγματεύσιμα Αμοιβαία Κεφάλαια" : "") +
                      (Convert.ToBoolean(klsContract.Details.ChkInvestmentGrade) ? "Επιθυμεί Mono Investment Grade" : "");
                }

                InvestIdees_Products = new clsInvestIdees_Products();
                InvestIdees_Products.II_ID = iRec_ID;
                InvestIdees_Products.GetList();
                foreach (DataRow dtRow1 in InvestIdees_Products.List.Rows)
                {
                    dtRow = dtProductList.NewRow();
                    k = k + 1;
                    dtRow["f16"] = k;
                    dtRow["f1"] = (Convert.ToInt32(dtRow1["Aktion"]) == 1 ? "ΑΓΟΡΑ" : "ΠΩΛΗΣΗ");
                    dtRow["f2"] = dtRow1["Title"] + "";
                    dtRow["f3"] = dtRow1["ISIN"] + "";
                    dtRow["f4"] = dtRow1["Curr"] + "";
                    dtRow["f6"] = dtRow1["ShareType"];                // was dtRow1["Price")
                    dtProductList.Rows.Add(dtRow);

                    sPrice = "";
                    switch (Convert.ToInt32(dtRow1["Type"]))
                    {
                        case 0:
                            sPrice = dtRow1["Price"] + "";
                            break;
                        case 1:
                            sPrice = "Τρέχουσα Τιμή" + "";
                            break;
                        case 2:
                            sPrice = dtRow1["Price"] + "";
                            break;
                        case 3:
                            sPrice = dtRow1["Price"] + "";
                            break;
                        case 4:
                            sPrice = "ATC";
                            break;
                        case 5:
                            sPrice = "ATO";
                            break;
                    }

                    if (dtRow1["StockExchange_FullTitle"] + "" != "")
                        sToposDiapagmatevsis = "Ενδεικτικός τόπος διαπραγμάτευσης:    " + dtRow1["StockExchange_FullTitle"] + "<br>";
                    else
                        sToposDiapagmatevsis = "";

                    dtRow["f18"] = sInvestProfile;

                    sTemp = "";
                    if (Convert.ToInt32(dtRow1["InvestType_Retail"]) == 2) sTemp = "Ιδιώτης πελάτης";

                    if (Convert.ToInt32(dtRow1["InvestType_Prof"]) == 2)
                        if (sTemp.Length > 0) sTemp = sTemp + ", " + "Επαγγελματίας πελάτης";
                        else sTemp = "Επαγγελματίας πελάτης";

                    dtRow["f27"] = sTemp;

                    dtRow["f28"] = "";
                    sTemp = "";
                    if (Convert.ToInt32(dtRow1["Distrib_ExecOnly"]) != 3) sTemp = "Execution-only";
                    if (Convert.ToInt32(dtRow1["Distrib_Advice"]) != 3)
                        if (sTemp.Length > 0) sTemp = sTemp + ", Investment Advisory";
                        else sTemp = "Investment Advisory";

                    if (Convert.ToInt32(dtRow1["Distrib_PortfolioManagment"]) != 3)
                        if (sTemp.Length > 0) sTemp = sTemp + ", Portfolio Management";
                        else sTemp = "Portfolio Management";

                    dtRow["f28"] = sTemp;

                    dtRow["f29"] = "";
                    switch (sComplexProduct)
                    {
                        case "1":
                            dtRow["f29"] = "Μη πολύπλοκα χρημ/κά μέσα";
                            break;
                        case "2":
                            dtRow["f29"] = "Πολύπλοκα χρημ/κά μέσα";
                            break;
                        case "1, 2":
                            dtRow["f29"] = "Μη πολύπλοκα χρημ/κά μέσα, Πολύπλοκα χρημ/κά μέσα";
                            break;
                    }

                    dtRow["f30"] = "";
                    if (Convert.ToInt32(dtRow1["ComplexProduct"]) == 1) dtRow["f30"] = "Μή πολύπλοκα χρημ/κά μέσα ";
                    else
                        if (Convert.ToInt32(dtRow1["ComplexProduct"]) == 2) dtRow["f30"] = "Πολύπλοκα χρημ/κά μέσα ";

                    switch (Convert.ToInt32(dtRow1["ShareType"]))
                    {
                        case 1:
                            if (Global.IsNumeric(dtRow1["Quantity"])) dtRow["f7"] = Convert.ToDecimal(dtRow1["Quantity"]).ToString("###.00####");
                            else dtRow["f7"] = dtRow1["Quantity"];

                            dtRow["f8"] = "-";
                            dtRow["f9"] = dtRow["f7"];
                            dtRow["f10"] = dtRow1["Amount"];
                            dtRow["f11"] = "Επενδυτική Πρόταση επί Μετοχής";

                            dtRow["f12"] = "<br>" + "ΣΤΟΙΧΕΙΑ ΠΡΟΪΟΝΤΟΣ" + "<br>" + "Τίτλος:    " + dtRow1["Title"] + "<br>" +
                                           "ISIN:    " + dtRow1["ISIN"] + "<br>" +
                                           (dtRow1["SectorTitle"] + "" != "" ? "Κλάδος:    " + dtRow1["SectorTitle"] + "<br>" : "") +
                                           "Exchange Ticker:    " + dtRow1["Code3"] + "<br>" +
                                           "Νόμισμα:    " + dtRow1["Curr"] + "<br>" +
                                           "Χώρα Έδρας:    " + dtRow1["CountryTitle"] + "<br>" +
                                           sToposDiapagmatevsis +
                                           "Αγορά-στόχος (είδος πελάτη): " + dtRow["f27"] + "<br>" +
                                           "Αγορά-στόχος (στρατηγική διανομής): " + dtRow["f28"];
                            dtRow["f5"] = (dtRow1["DescriptionEn"]+"" != "" ? "ΠΕΡΙΓΡΑΦΗ (όπως παρέχεται από Bloomberg, Morning Star, Reuters)" + "<br>" + dtRow1["DescriptionEn"] + "<br>" : "") + "<br>" +
                                           "ΣΤΟΙΧΕΙΑ ΠΡΟΤΕΙΝΟΜΕΝΗΣ ΣΥΝΑΛΛΑΓΗΣ" + "<br>" +
                                           "Ενέργεια:    " + sEnergia[Convert.ToInt16(dtRow1["Energia"])] + "<br>" +
                                           "Πράξη:    " + (Convert.ToInt32(dtRow1["Aktion"]) == 1 ? "ΑΓΟΡΑ" : "ΠΩΛΗΣΗ") + "<br>" +
                                           (dtRow1["Price"]+"" != "-" ? "Τιμή:    " + sPrice + " " + dtRow1["Curr"] + "<br>" : "") +
                                           (dtRow1["Quantity"]+"" != "-" ? "Τεμάχια:    " + dtRow["f7"] + "<br>" : "") +
                                           (dtRow1["Amount"]+"" != "-" ? "Αξία:    " + Convert.ToDecimal(dtRow1["Amount"]).ToString("###.00") + " " + dtRow1["Curr"] + "<br>" : "") +
                                           "Διάρκεια συναλλαγής:    " + sConstant[Convert.ToInt16(dtRow1["Constant"])] + "   " + dtRow1["ConstantDate"] + "<br>" +
                                           ((dtRow1["PriceUp"]+"" != "-" && dtRow1["PriceUp"]+"" != "0") ? "Τιμή Στόχου:    " + dtRow1["PriceUp"] + "<br>" : "") +
                                           ((dtRow1["PriceDown"]+"" != "-" && dtRow1["PriceDown"]+"" != "0") ? "Stop Loss:    " + dtRow1["PriceDown"] + "<br>" : "") +
                                           (sProviderTitle_PriceTable.Length == 0 ?
                                               "Στην αξία προτεινόμενης συναλλαγής δεν συμπεριλαμβάνονται κόστη συναλλαγών, φόροι και έξοδα τρίτων" :
                                               "Στην αξία προτεινόμενης συναλλαγής δεν συμπεριλαμβάνονται <font color='#3366cc'><a href='http://dms.hellasfin.gr/Company/" + sProviderTitle_PriceTable + "'>κόστη συναλλαγών</a></font>, φόροι και έξοδα τρίτων");

                            dtRow["f15"] = "Μετοχή";
                            dtRow["f13"] = dtRow1["InvestGoal"];
                            dtRow["f19"] = sInvestHorisont;
                            dtRow["f20"] = "Ληκτότητα: Δεν υπάρχει";
                            dtRow["f21"] = sInvestRisk;
                            dtRow["f22"] = "Υψηλός";
                            dtRow["f23"] = dtRow1["RiskCurr"];
                            dtRow["f24"] = sGeography;
                            dtRow["f25"] = dtRow1["InvestmentAreaTitle"];
                            dtRow["f26"] = sCategoryMiFID[iMiFIDCategory_ID];

                            break;
                        case 2:
                            if (Global.IsNumeric(dtRow1["Quantity"])) dtRow["f7"] = Convert.ToDecimal(dtRow1["Quantity"]).ToString("###.00####");
                            else dtRow["f7"] = dtRow1["Quantity"];

                            dtRow["f8"] = "-";
                            dtRow["f9"] = dtRow["f7"];
                            dtRow["f10"] = dtRow1["Amount"];
                            dtRow["f11"] = "Επενδυτική Πρόταση επί Ομολόγου";

                            if (dtRow1["MoodysRating"] + "" != "" || dtRow1["FitchsRating"] + "" != "" || dtRow1["SPRating"] + "" != "" || dtRow1["ICAPRating"] + "" != "") {
                                sRatings = "Moodys :  " + dtRow1["MoodysRating"] + ",   Fitch: " + dtRow1["FitchsRating"] + ",   S&P: " + dtRow1["SPRating"] + ",   ICAP: " + dtRow1["ICAPRating"];
                                sRatingReport = "Moodys :  " + dtRow1["MoodysRating"] + ",   Fitch: " + dtRow1["FitchsRating"] + ",   S&P: " + dtRow1["SPRating"] + ",   ICAP: " + dtRow1["ICAPRating"];
                            }
                            else {
                                sRatings = "Δεν υπάρχει";
                                sRatingReport = "Υψηλός";
                            }

                            sComplexDetails = "";
                            sNewRow = "";
                            iNewRows = 0;
                            if (Convert.ToInt16(dtRow1["ComplexProduct"]) == 2) {                             // 2 - Yes
                                if (dtRow1["BBG_ComplexAttribute"]+"" != "") {
                                    iNewRows = iNewRows + 1;
                                    sComplexDetails = (dtRow1["BBG_ComplexAttribute"] + "").Trim();
                                }

                                ProductTitles_ComplexReasons = new clsProductTitles_ComplexReasons();
                                ProductTitles_ComplexReasons.ShareTitles_ID = Convert.ToInt32(dtRow1["ShareTitles_ID"]);
                                ProductTitles_ComplexReasons.GetList();
                                foreach(DataRow dtRow2 in ProductTitles_ComplexReasons.List.Rows)
                                {
                                    iNewRows = iNewRows + 1;
                                    if  (sComplexDetails.Length == 0) sComplexDetails = dtRow2["ComplexReason_Title"] +"";
                                    else sComplexDetails = sComplexDetails + ", " + dtRow2["ComplexReason_Title"];
                                }

                                sComplexDetails = "Είδος πολυπλοκότητας: " + sComplexDetails;

                                iNewRows = (int)(iNewRows / 3);
                                for (k=1; k <= iNewRows; k++) sNewRow = sNewRow + "<br>";
                            }

                            dtRow["f12"] = "ΣΤΟΙΧΕΙΑ ΠΡΟΪΟΝΤΟΣ" + "<br>" +
                                           "Εκδότης:    " + dtRow1["Title"] + "<br>" +
                                           "Τύπος ομολόγου:    " + sBondType[Convert.ToInt16(dtRow1["BondType"])] + "<br>" +
                                           "ISIN:    " + dtRow1["ISIN"] + "<br>" +
                                           (dtRow1["SectorTitle"] + "" != "" ? "Κλάδος:    " + dtRow1["SectorTitle"] + "<br>" : "") +
                                           "Ημερομηνία λήξης:    " + dtRow1["Date2"] + "<br>" +
                                           "Νόμισμα:    " + dtRow1["Curr"] + "<br>" +
                                           "Κουπόνι:    " + dtRow1["Coupone"] + " &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Είδος κουπονιού: " + dtRow1["CouponeTypes_Title"] +
                                                            " &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Συχνότητα αποκοπής κουπονιού    " + dtRow1["FrequencyClipping"] + "(φορές/έτος)<br>" +
                                           "Πιστοληπτική αξιολόγηση: " + sRatings + "<br>" +
                                           "Πολύπλοκο προϊόν:   " + (Convert.ToInt16(dtRow1["ComplexProduct"]) == 1 ? "Όχι" : "Ναι") + "<br>" +
                                           sComplexDetails;
                            dtRow["f5"] = sNewRow + (dtRow1["InvestmentAreaTitle"]+"" != "" ? "Investment Area:    " + dtRow1["InvestmentAreaTitle"] : "") + "<br>" +
                                           "Χώρα Έδρας:    " + dtRow1["CountryTitle"] + "<br>" +
                                           "Αγορά-στόχος (είδος πελάτη): " + dtRow["f27"] + "<br>" +
                                           "Αγορά-στόχος (στρατηγική διανομής): " + dtRow["f28"] + "<br>" +
                                           (dtRow1["DescriptionEn"]+"" != "" ? "ΠΕΡΙΓΡΑΦΗ (όπως παρέχεται από Bloomberg, Morning Star, Reuters)" + "<br>" + dtRow1["DescriptionEn"] + "" + "<br>" : "") + "<br>" +
                                           "ΣΤΟΙΧΕΙΑ ΠΡΟΤΕΙΝΟΜΕΝΗΣ ΣΥΝΑΛΛΑΓΗΣ" + "<br>" +
                                           "Ενέργεια:    " + sEnergia[Convert.ToInt16(dtRow1["Energia"])] + "<br>" +
                                           "Πράξη:    " + (Convert.ToInt16(dtRow1["Aktion"]) == 1 ? "ΑΓΟΡΑ" : "ΠΩΛΗΣΗ") + "<br>" +
                                           (dtRow1["Quantity"]+"" != "-" ? "Ονομαστική αξία:    " + dtRow["f7"] + " " + dtRow1["Curr"] + "<br>" : "") +
                                           (dtRow1["Price"]+"" != "-" ? "Τιμή:    " + sPrice + (Convert.ToInt32(dtRow1["Type"]) == 1 || Convert.ToInt32(dtRow1["Type"]) == 4 || Convert.ToInt32(dtRow1["Type"]) == 5 ? "" : " %") + "<br>" +
                                           (Convert.ToDecimal(dtRow1["Amount"]) != 0 ? (dtRow1["Amount"]+"" != "-" ? "Αξία συναλλαγής:    " + Convert.ToDecimal(dtRow1["Amount"]).ToString("###.00") + " " + dtRow1["Curr"] + "<br>": "") : "Αξία συναλλαγής: " + dtRow["f7"] + " " + dtRow1["Curr"] + " x " + "Τρέχουσα Τιμή" + "<br>") : "") +
                                           "Διάρκεια συναλλαγής:    " + sConstant[Convert.ToInt16(dtRow1["Constant"])] + "   " + dtRow1["ConstantDate"] + "<br>" +
                                           ((dtRow1["PriceUp"] + "" != "-" && dtRow1["PriceUp"] + "" != "0") ? "Τιμή Στόχου:    " + dtRow1["PriceUp"] + "<br>" : "") +
                                           ((dtRow1["PriceDown"] + "" != "-" && dtRow1["PriceDown"] + "" != "0") ? "Stop Loss:    " + dtRow1["PriceDown"] + "<br>" : "") +
                                           (sProviderTitle_PriceTable.Length == 0 ? "Στην αξία προτεινόμενης συναλλαγής δεν συμπεριλαμβάνονται κόστη συναλλαγών, φόροι και έξοδα τρίτων" :
                                               "Στην αξία προτεινόμενης συναλλαγής δεν συμπεριλαμβάνονται <font color='#3366cc'><a href='http://dms.hellasfin.gr/Company/" + sProviderTitle_PriceTable + "'>κόστη συναλλαγών</a></font>, φόροι και έξοδα τρίτων");

                            dtRow["f15"] = "Ομόλoγο";
                            dtRow["f13"] = dtRow1["InvestGoal"];
                            dtRow["f19"] = sInvestHorisont;
                            dtRow["f20"] = "Ημερομηνία λήξης: " + Convert.ToDateTime(dtRow1["Date2"]).ToString("dd/MM/yyyy");
                            dtRow["f21"] = sInvestRisk;
                            dtRow["f22"] = sRatingReport;
                            dtRow["f23"] = dtRow1["RiskCurr"];
                            dtRow["f24"] = sGeography;
                            dtRow["f25"] = dtRow1["InvestmentAreaTitle"];
                            dtRow["f26"] = sCategoryMiFID[iMiFIDCategory_ID];

                            break;
                        case 4:
                            if (Global.IsNumeric(dtRow1["Quantity"])) dtRow["f7"] = Convert.ToDecimal(dtRow1["Quantity"]).ToString("###.00####");
                            else dtRow["f7"] = dtRow1["Quantity"];

                            dtRow["f8"] = sDMS_Path + "\\InvestProposals\\SRRI_" + dtRow1["SurveyedKIID"] + ".jpg";
                            dtRow["f9"] = dtRow["f7"];
                            dtRow["f10"] = dtRow1["Amount"];
                            dtRow["f11"] = "Επενδυτική Πρόταση επί Διαπραγματεύσιμου Αμοιβαίου Κεφαλαίου";

                            sTemp = "";
                            if (Convert.ToInt32(dtRow1["Distrib_ExecOnly"]) != 3) sTemp = "Execution-only";
                            if (Convert.ToInt32(dtRow1["Distrib_Advice"]) != 3) {
                                if (sTemp.Length > 0) sTemp = sTemp + ", Investment Advisory";
                                else sTemp = "Investment Advisory";
                            }
                            if (Convert.ToInt32(dtRow1["Distrib_PortfolioManagment"]) != 3) {
                                if (sTemp.Length > 0) sTemp = sTemp + ", Portfolio Management";
                                else sTemp = "Portfolio Management";
                            }
                            dtRow["f28"] = sTemp;

                            dtRow["f12"] = "ΣΤΟΙΧΕΙΑ ΠΡΟΪΟΝΤΟΣ" + "<br>" + "Τίτλος:    " + dtRow1["Title"] + "<br>" +
                                           "ISIN:    " + dtRow1["ISIN"] + "<br>" + 
                                           "Exchange Ticker:    " + dtRow1["Code3"] + "<br>" +
                                           "Νόμισμα:    " + dtRow1["Curr"] + "<br>" +
                                           "Κατηγορία Διαπραγματεύσιμου Αμοιβαίου Κεφαλαίου:    " + dtRow1["ProductsCategories_Title"] + "<br>" +
                                           ((dtRow1["InvestmentAreaTitle"] + "" != "") ? "Investment Area:    " + dtRow1["InvestmentAreaTitle"] + "<br>" : "") +
                                           "Χώρα Έδρας:    " + dtRow1["CountryTitle"] + "<br>" +
                                           sToposDiapagmatevsis +
                                           "Αγορά-στόχος (είδος πελάτη): " + dtRow["f27"] + "<br>" +
                                           "Αγορά-στόχος (στρατηγική διανομής): " + dtRow["f28"] + "<br>" +
                                           "SRRI (Synthetic Risk Reward Indicator): " + dtRow1["SurveyedKIID"] + "<br>";
                            dtRow["f5"] = "<br><br><br><br><br>" +
                                           (dtRow1["DescriptionEn"]+"" != "" ? "<br>" + "Στόχοι και επενδυτική πολιτική KIID (όπως παρέχεται από Bloomberg, Morning Star, Reuters)" + "<br>" + dtRow1["DescriptionEn"] + "<br>" : "") + "<br>" +
                                           "ΣΤΟΙΧΕΙΑ ΠΡΟΤΕΙΝΟΜΕΝΗΣ ΣΥΝΑΛΛΑΓΗΣ" + "<br>" +
                                           "Ενέργεια:    " + sEnergia[Convert.ToInt16(dtRow1["Energia"])] + "<br>" +
                                           "Πράξη:    " + (Convert.ToInt16(dtRow1["Aktion"]) == 1 ? "ΑΓΟΡΑ" : "ΠΩΛΗΣΗ") + "<br>" +
                                           (dtRow1["Price"]+"" != "-" ? "Τιμή:    " + sPrice + " " + dtRow1["Curr"] + "<br>" : "") +
                                           (dtRow1["Quantity"]+"" != "-" ? "Τεμάχια:    " + dtRow["f7"] + "<br>" : "") +
                                           (dtRow1["Amount"]+"" != "-" ? "Αξία:    " + Convert.ToDecimal(dtRow1["Amount"]).ToString("###.00") + " " + dtRow1["Curr"] + "<br>" : "") +
                                           "Διάρκεια συναλλαγής:    " + sConstant[Convert.ToInt16(dtRow1["Constant"])] + "   " + dtRow1["ConstantDate"] + "<br>" +
                                           ((dtRow1["PriceUp"] + "" != "-" && dtRow1["PriceUp"] + "" != "0") ? "Τιμή Στόχου:    " + dtRow1["PriceUp"] + "<br>" : "") +
                                           ((dtRow1["PriceDown"] + "" != "-" && dtRow1["PriceDown"] + "" != "0") ? "Stop Loss:    " + dtRow1["PriceDown"] + "<br>" : "") +
                                (sProviderTitle_PriceTable.Length == 0 ? "Στην αξία προτεινόμενης συναλλαγής δεν συμπεριλαμβάνονται κόστη συναλλαγών, φόροι και έξοδα τρίτων" :
                                               "Στην αξία προτεινόμενης συναλλαγής δεν συμπεριλαμβάνονται <font color='#3366cc'><a href='http://dms.hellasfin.gr/Company/" + sProviderTitle_PriceTable + "'>κόστη συναλλαγών</a></font>, φόροι και έξοδα τρίτων");

                            dtRow["f15"] = "Διαπραγματεύσιμο Αμοιβαίο Κεφάλαιο";
                            dtRow["f13"] = dtRow1["InvestGoal"];
                            dtRow["f19"] = sInvestHorisont;
                            if (Convert.ToSingle(dtRow1["Maturity"]) == 0) dtRow["f20"] = "Ληκτότητα : Δεν υπάρχει ";
                            else dtRow["f20"] = "Ληκτότητα = " + dtRow1["Maturity"] + " έτη";
                            dtRow["f21"] = sInvestRisk;
                            dtRow["f22"] = "SRRI (Synthetic Risk Reward Indicator) = " + dtRow1["SurveyedKIID"];
                            dtRow["f23"] = dtRow1["RiskCurr"];
                            dtRow["f24"] = sGeography;
                            dtRow["f25"] = dtRow1["InvestmentAreaTitle"];
                            dtRow["f26"] = sCategoryMiFID[iMiFIDCategory_ID];

                            break;
                        case 6:
                            if (Global.IsNumeric(dtRow1["Quantity"])) dtRow["f7"] = Convert.ToDecimal(dtRow1["Quantity"]).ToString("###.00####");
                            else dtRow["f7"] = dtRow1["Quantity"];

                            dtRow["f8"] = sDMS_Path + "\\InvestProposals\\SRRI_" + dtRow1["SurveyedKIID"] + ".jpg";
                            dtRow["f9"] = dtRow["f7"];
                            dtRow["f10"] = dtRow1["Amount"];
                            dtRow["f11"] = "Επενδυτική Πρόταση επί Αμοιβαίου Κεφαλαίου";

                            dtRow["f12"] = "<br>" + "ΣΤΟΙΧΕΙΑ ΠΡΟΪΟΝΤΟΣ" + "<br>" + "Τίτλος:    " + dtRow1["Title"] + "<br>" +
                                           "ISIN:    " + dtRow1["ISIN"] + "<br>" +
                                           "Νόμισμα:    " + dtRow1["Curr"] + "<br>" +
                                           "Κατηγορία Αμοιβαίου Κεφαλαίου:    " + dtRow1["ProductsCategories_Title"] + "<br><br>" +
                                           (dtRow1["InvestmentAreaTitle"]+"" != "" ? "Investment Area:    " + dtRow1["InvestmentAreaTitle"] + "<br>" : "") +
                                           "Χώρα Έδρας:    " + dtRow1["CountryTitle"] + "<br>" +
                                           "Αγορά-στόχος (είδος πελάτη): " + dtRow["f27"] + "<br>" +
                                           "Αγορά-στόχος (στρατηγική διανομής): " + dtRow["f28"] + "<br>" +
                                           "SRRI (Synthetic Risk Reward Indicator): " + dtRow1["SurveyedKIID"] + "<br>";
                            dtRow["f5"] = "<br><br><br><br><br>" +
                                           (dtRow1["DescriptionEn"]+"" != ""? "<br>" + "Στόχοι και επενδυτική πολιτική KIID (όπως παρέχεται από Bloomberg, Morning Star, Reuters) " + "<br>" + dtRow1["DescriptionEn"] + "<br>": "") + "<br>" +
                                           "ΣΤΟΙΧΕΙΑ ΠΡΟΤΕΙΝΟΜΕΝΗΣ ΣΥΝΑΛΛΑΓΗΣ" + "<br>" +
                                           "Ενέργεια:    " + sEnergia[Convert.ToInt32(dtRow1["Energia"])] + "<br>" +
                                           "Πράξη:    " + (Convert.ToInt16(dtRow1["Aktion"]) == 1 ? "ΑΓΟΡΑ" : "ΠΩΛΗΣΗ") + "<br>";

                            if (Convert.ToSingle(dtRow["f7"]) != 0)
                                dtRow["f5"] = dtRow["f5"] + ((dtRow1["Quantity"]+"" != "-") ? "Τεμάχια:    " + dtRow["f7"] + "<br>" + "Αξία: " + dtRow["f7"] + " x NAV price" + "<br>" : "");
                            else
                                dtRow["f5"] = dtRow["f5"] + ((dtRow1["Amount"]+"" != "-") ? "Αξία:    " + Convert.ToDecimal(dtRow1["Amount"]).ToString("###.00") + " " + dtRow1["Curr"] + "<br>" : "");

                            dtRow["f5"] = dtRow["f5"] + ((dtRow1["PriceUp"] + "" != "-" && dtRow1["PriceUp"] + "" != "0") ? "Τιμή Στόχου:    " + dtRow1["PriceUp"] + "<br>" : "") +
                                                        ((dtRow1["PriceDown"] + "" != "-" && dtRow1["PriceDown"] + "" != "0") ? "Stop Loss:    " + dtRow1["PriceDown"] + "<br>" : "") +
                                          (sProviderTitle_PriceTable.Length == 0 ? "Στην αξία προτεινόμενης συναλλαγής δεν συμπεριλαμβάνονται κόστη συναλλαγών, φόροι και έξοδα τρίτων" :
                                               "Στην αξία προτεινόμενης συναλλαγής δεν συμπεριλαμβάνονται <font color='#3366cc'><a href='http://dms.hellasfin.gr/Company/" + sProviderTitle_PriceTable + "'>κόστη συναλλαγών</a></font>, φόροι και έξοδα τρίτων");

                            dtRow["f15"] = "Αμοιβαίο Κεφάλαιο";
                            dtRow["f13"] = dtRow1["InvestGoal"];
                            dtRow["f19"] = sInvestHorisont;
                            if (Convert.ToSingle(dtRow1["Maturity"]) == 0) dtRow["f20"] = "Ληκτότητα : Δεν υπάρχει ";
                            else dtRow["f20"] = "Ληκτότητα = " + dtRow1["Maturity"] + " έτη";
                            dtRow["f21"] = sInvestRisk;
                            dtRow["f22"] = "SRRI (Synthetic Risk Reward Indicator) = " + dtRow1["SurveyedKIID"];
                            dtRow["f23"] = dtRow1["RiskCurr"];
                            dtRow["f24"] = sGeography;
                            dtRow["f25"] = dtRow1["InvestmentAreaTitle"];
                            dtRow["f26"] = sCategoryMiFID[iMiFIDCategory_ID];
                            break;
                    }

                    sTemp = "";
                    if (dtRow1["Notes"] + "" != "")
                    {
                        sTemp = dtRow1["Notes"] + "";
                        sTemp = "ΣΚΟΠΟΣ ΠΡΟΤΕΙΝΟΜΕΝΗΣ ΣΥΝΑΛΛΑΓΗΣ <br>" + sTemp.Replace("\n", "<br>");
                    }

                    sTemp = sTemp + "<br><br>ΚΙΝΔΥΝΟΙ ΕΠΕΝΔΥΣΗΣ<br>" +
                                   "Η επένδυση σε χρηματοπιστωτικά μέσα ενέχει κινδύνους, όπως αυτοί περιγράφονται στη σχετική ενότητα της παρούσας επενδυτικής πρότασης και στη σύμβαση παροχής επενδυτικών συμβουλών που έχετε υπογράψει με την Εταιρία. Οι κίνδυνοι αυτοί συνίστανται, γενικώς, στη µείωση της  αξίας της επένδυσης ή, ακόµη, και στην οριστική απώλεια  του επενδυόµενου ποσού.";
                    switch (Convert.ToInt32(dtRow1["ShareType"]))
                    {
                        case 4:    // ONLY for DAK Agora 
                            if (Convert.ToInt32(dtRow1["Aktion"]) == 1)
                                sTemp = sTemp + "<br><br>Η επενδυτική πρόταση αφορά κατασκευασμένο προϊόν εκδότη, το οποίο διαπραγματεύεται σε οργανωμένη αγορά." +
                                        " Επισυνάπτεται το έγγραφο βασικών πληροφοριών και λοιπή αναγκαία πληροφόρηση, όπου περιγράφονται αναλυτικά τα στοιχεία και τα δεδομένα του χρηματοπιστωτικού μέσου. Πριν λάβετε την επενδυτική σας απόφαση, καλείστε να τα μελετήσετε προσεκτικά. Για επιπλέον διευκρινίσεις, θα πρέπει να απευθυνθείτε στον επενδυτικό σας σύμβουλο.";
                            break;
                        case 6:    // ONLY for AK Agora 
                            if (Convert.ToInt32(dtRow1["Aktion"]) == 1)
                                sTemp = sTemp + "<br><br>Επισυνάπτεται το έγγραφο βασικών πληροφοριών και λοιπή αναγκαία πληροφόρηση, όπου περιγράφονται αναλυτικά τα στοιχεία και τα δεδομένα του χρηματοπιστωτικού μέσου. Πριν λάβετε την επενδυτική σας απόφαση, καλείστε να τα μελετήσετε προσεκτικά. Για επιπλέον διευκρινίσεις, θα πρέπει να απευθυνθείτε στον επενδυτικό σας σύμβουλο.";
                            break;
                    }

                    sTemp = sTemp + "<br><br>Επιπλέον και λεπτομερέστερη πληροφόρηση για τα χρηματοπιστωτικά μέσα που αναγράφονται στην επενδυτική πρόταση και τους Εκδότες/Εταιρίες Διαχείρισης αυτών, είναι διαθέσιμη στις ιστοσελίδες των Εκδοτών/Εταιριών Διαχείρισης. Επιπλέον, μπορείτε να απευθυνθείτε στον επενδυτικό σας σύμβουλο.";

                    dtRow["f5"] = dtRow["f5"] + "<br>" + sTemp;

                    sTemp = "";
                    if (dtRow1["URL"] + "" != "")
                    {
                        sLink = dtRow1["URL"] + "";
                        if (sLink.IndexOf("http://") < 0 && sLink.IndexOf("https://") < 0) sLink = "http://" + sLink;
                        sTemp = sTemp + "URL:&nbsp;<font color='#3366cc'><a href='" + sLink + "'>" + sLink + "</a></font><br>";
                    }
                    if (dtRow1["URL_IR"] + "" != "")
                    {
                        sLink = dtRow1["URL_IR"] + "";
                        if (sLink.IndexOf("http://") < 0 && sLink.IndexOf("https://") < 0) sLink = "http://" + sLink;
                        sTemp = sTemp + "URL IR :&nbsp;<font color='#3366cc'><a href='" + sLink + "'>Investor Relations</a></font><br>";
                    }
                    sLink = "";
                    for (j = 0; j < stAtts.Count; j++)
                    {

                        if (stAtts[j].Share_ID == Convert.ToInt32(dtRow1["ShareCodes_ID"]))
                            if (Path.GetFileName(stAtts[j].UploadFilePath + "") != "")
                                sLink = sLink + "<a href='http://dms.hellasfin.gr/Company/InvestProposals_Products/" + iYear + "/" + iMonth + "/" +
                                        Path.GetFileName(stAtts[j].UploadFilePath) + "'>" + stAtts[j].DocType_Title + "</a><br>";
                    }
                    sLink = sLink.Trim();

                    if (sLink.Length > 0) sTemp = sTemp + "<br>Συνημμένα αρχεία<p><font color='#3366cc'>" + sLink + "</font>";
                    dtRow["f17"] = sTemp;

                    sLink = sLink.Replace("'", "`");
                    InvestIdees_Products = new clsInvestIdees_Products();
                    InvestIdees_Products.Record_ID = Convert.ToInt32(dtRow1["ID"]);
                    InvestIdees_Products.GetRecord();
                    InvestIdees_Products.SummaryLink = sLink;
                    InvestIdees_Products.EditRecord();
                }

                sIdeaText = "";
                if (sIdeasText != "") sIdeaText = "<strong>Σημειώσεις / Παρατηρήσεις Συμβούλου </strong><br><br>" + sIdeasText;

                sContent = "1. Στοιχεία σύμβασης πελάτη\n" +
                           "2. Επενδυτικό προφίλ πελάτη\n" +
                           "3. Πίνακας επενδυτικών προτάσεων\n" +
                           "4. Στοιχεία επενδυτικών προτάσεων (με επισυναπτόμενα αρχεία σχετικά με τα προτεινόμενα χρηματοπιστωτικά μέσα, όπου υφίστανται) ";

                if (sCostBenefits.Length > 0)
                {
                    string[] tokens = sCostBenefits.Split('~');

                    if (tokens[0] == "1")
                    {
                        sCostBenefitsM = "1~0~0~0~0~0~0~0~";
                        sCostBenefits_Monetary = "Monetary (Χρηματικά οφέλη) :  Δεν υπάρχουν";
                    }
                    else
                    {
                        sCostBenefitsM = "0~";
                        sCostBenefits_Monetary = "Monetary (Χρηματικά οφέλη) :  ";
                        if (tokens[1] == "1" || tokens[2] == "1" || tokens[3] == "1" || tokens[4] == "1")
                        {
                            sCostBenefits_Monetary = "Monetary (Χρηματικά οφέλη) : \n" + "      Προοπτική επίτευξης θετικής απόδοσης μεγαλύτερης του κόστους αλλαγής με:";

                            if (tokens[1] == "1")
                            {
                                sCostBenefitsM = sCostBenefitsM + "1~";
                                sCostBenefits_Monetary = sCostBenefits_Monetary + "\n            με προσδοκώμενη τιμή στόχου";
                            }
                            else sCostBenefitsM = sCostBenefitsM + "0~";

                            if (tokens[2] == "1")
                            {
                                sCostBenefitsM = sCostBenefitsM + "1~";
                                sCostBenefits_Monetary = sCostBenefits_Monetary + "\n            με προσδοκώμενο Yield";
                            }
                            else sCostBenefitsM = sCostBenefitsM + "0~";

                            if (tokens[3] == "1")
                            {
                                sCostBenefitsM = sCostBenefitsM + "1~";
                                sCostBenefits_Monetary = sCostBenefits_Monetary + "\n            με προσδοκώμενη μερισματική απόδοση";
                            }
                            else sCostBenefitsM = sCostBenefitsM + "0~";

                            if (tokens[4] == "1")
                            {
                                sCostBenefitsM = sCostBenefitsM + "1~";
                                sCostBenefits_Monetary = sCostBenefits_Monetary + "\n            με προσδοκώμενη νομισματική απόδοση";
                            }
                            else sCostBenefitsM = sCostBenefitsM + "0~";
                        }
                        else sCostBenefitsM = sCostBenefitsM + "0~0~0~0~";



                        if (tokens[5] == "1")
                        {
                            sCostBenefitsM = sCostBenefitsM + "1~";
                            sCostBenefits_Monetary = sCostBenefits_Monetary + "\n            με προσδοκώμενη νομισματική απόδοση";
                        }
                        else sCostBenefitsM = sCostBenefitsM + "0~";

                        if (tokens[6] == "1")
                        {
                            sCostBenefitsM = sCostBenefitsM + "1~";
                            sCostBenefits_Monetary = sCostBenefits_Monetary + "\n      Αλλαγή λόγω μικρότερου κόστους διατήρησης προϊόντος";
                        }
                        else sCostBenefitsM = sCostBenefitsM + "0~";

                        if (tokens[7] == "1")
                        {
                            sCostBenefitsM = sCostBenefitsM + "1~";
                            sCostBenefits_Monetary = sCostBenefits_Monetary + "\n      Λόγω μείωσης φορολογίας";
                        }
                        else sCostBenefitsM = sCostBenefitsM + "0~";
                    }

                    if (tokens[8] == "1")
                        {
                            sCostBenefitsNM = "1~0~0~0~0~0~0~0~0~0~0~0~0~0~0~0~0~0~0~";
                            sCostBenefits_NonMonetary = "Non Monetary (Μη χρηματικά οφέλη) :  Δεν υπάρχουν";
                        }
                    else {
                        sCostBenefitsNM = "0~";

                        sCostBenefits_NonMonetary = "Non Monetary (Μη χρηματικά οφέλη) :";

                        if (tokens[9] == "1" || tokens[10] == "1" || tokens[11] == "1" || tokens[12] == "1" || tokens[13] == "1" || tokens[14] == "1" ||
                            tokens[15] == "1" || tokens[16] == "1" || tokens[17] == "1" || tokens[18] == "1")
                        {

                            sCostBenefits_NonMonetary = "Non Monetary (Μη χρηματικά οφέλη) :\n      Μείωση κινδύνων:";

                            if (tokens[9] == "1")
                            {
                                sCostBenefitsNM = sCostBenefitsNM + "1~";
                                sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n        Επενδυτικού κινδύνου (risk off)";
                            }
                            else sCostBenefitsNM = sCostBenefitsNM + "0~";

                            if (tokens[10] == "1")
                            {
                                sCostBenefitsNM = sCostBenefitsNM + "1~";
                                sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n      Πιστωτικού κινδύνου (credit risk)";
                            }
                            else sCostBenefitsNM = sCostBenefitsNM + "0~";

                            if (tokens[11] == "1")
                            {
                                sCostBenefitsNM = sCostBenefitsNM + "1~";
                                sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n            Κίνδυνος υπερσυγκέντρωσης σε κλάδο";
                            }
                            else sCostBenefitsNM = sCostBenefitsNM + "0~";

                            if (tokens[12] == "1")
                            {
                                sCostBenefitsNM = sCostBenefitsNM + "1~";
                                sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n            Κίνδυνος υπερσυγκέντρωσης σε χώρα";
                            }
                            else sCostBenefitsNM = sCostBenefitsNM + "0~";

                            if (tokens[13] == "1")
                            {
                                sCostBenefitsNM = sCostBenefitsNM + "1~";
                                sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n            Κίνδυνος υπερσυγκέντρωσης σε εκδότη";
                            }
                            else sCostBenefitsNM = sCostBenefitsNM + "0~";

                            if (tokens[14] == "1")
                            {
                                sCostBenefitsNM = sCostBenefitsNM + "1~";
                                sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + " \n           Νομισματικού κινδύνου";
                            }
                            else sCostBenefitsNM = sCostBenefitsNM + "0~";

                            if (tokens[5] == "1")
                            {
                                sCostBenefitsNM = sCostBenefitsNM + "1~";
                                sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n            Κίνδυνος μεταβλητότητας";
                            }
                            else sCostBenefitsNM = sCostBenefitsNM + "0~";

                            if (tokens[16] == "1")
                            {
                                sCostBenefitsNM = sCostBenefitsNM + "1~";
                                sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n            Κίνδυνος επιτοκίου";
                            }
                            else sCostBenefitsNM = sCostBenefitsNM + "0~";

                            if (tokens[17] == "1")
                            {
                                sCostBenefitsNM = sCostBenefitsNM + "1~";
                                sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n            Κίνδυνος πολιτικός";
                            }
                            else sCostBenefitsNM = sCostBenefitsNM + "0~";

                            if (tokens[18] == "1")
                            {
                                sCostBenefitsNM = sCostBenefitsNM + "1~";
                                sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n            Κίνδυνος συστημικός";
                            }
                            else sCostBenefitsNM = sCostBenefitsNM + "0~";
                        }
                        else 
                            sCostBenefitsNM = sCostBenefitsNM + "0~0~0~0~0~0~0~0~0~0~";
               
                        if (tokens[19] == "1" || tokens[20] == "1" || tokens[21] == "1" || tokens[22] == "1" || tokens[23] == "1")
                        {
                            sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n      Αύξηση διασποράς σε:";
                            if (tokens[19] == "1")
                            {
                                sCostBenefitsNM = sCostBenefitsNM + "1~";
                                sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n            χώρα";
                            }
                            else sCostBenefitsNM = sCostBenefitsNM + "0~";

                            if (tokens[20] == "1")
                            {
                                sCostBenefitsNM = sCostBenefitsNM + "1~";
                                sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n            κλάδο";
                            }
                            else sCostBenefitsNM = sCostBenefitsNM + "0~";

                            if (tokens[21] == "1")
                            {
                                sCostBenefitsNM = sCostBenefitsNM + "1~";
                                sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n            νόμισμα";
                            }
                            else sCostBenefitsNM = sCostBenefitsNM + "0~";

                            if (tokens[22] == "1")
                            {
                                sCostBenefitsNM = sCostBenefitsNM + "1~";
                                sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n            αριθμό προϊόντων";
                            }
                            else sCostBenefitsNM = sCostBenefitsNM + "0~";

                            if (tokens[23] == "1")
                            {
                                sCostBenefitsNM = sCostBenefitsNM + "1~";
                                sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n            εκδότη";
                            }
                            else sCostBenefitsNM = sCostBenefitsNM + "0~";
                        }
                        else sCostBenefitsNM = sCostBenefitsNM + "0~0~0~0~0~";

                        if (tokens[24] == "1")
                        {
                            sCostBenefitsNM = sCostBenefitsNM + "1~";
                            sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n      Προσαρμογή στη καταλληλότητα των χρηματοπιστωτικών μέσων";
                        }
                        else sCostBenefitsNM = sCostBenefitsNM + "0~";

                        if (tokens[25] == "1")
                        {
                            sCostBenefitsNM = sCostBenefitsNM + "1~";
                            sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n      Λόγω μείωσης φορολογίας";
                        }
                        else sCostBenefitsNM = sCostBenefitsNM + "0~";

                        if (tokens[26] == "1")
                        {
                            sCostBenefitsNM = sCostBenefitsNM + "1~";
                            sCostBenefits_NonMonetary = sCostBenefits_NonMonetary + "\n      Αύξηση ρευστότητας";
                        }
                        else sCostBenefitsNM = sCostBenefitsNM + "0~";
                    }
                }

                if (sCostBenefits_Monetary.Length == 0 && sCostBenefits_NonMonetary.Length == 0)
                {
                    sCBA_Title = "";
                    sCBA_Text = "";
                    sDisclimer_Title = "5. Κίνδυνοι Χρηματοπιστωτικών Μέσων";
                    sNotes_Title = "6.  Επισημάνσεις";

                    sContent = sContent + "\n" +
                               "5. Κίνδυνοι Χρηματοπιστωτικών Μέσων" + "\n" +
                               "6. Επισημάνσεις";
                }
                else
                {
                    sCBA_Title = "5. Cost - Benefit Analysis";
                    sCBA_Text = "Κόστος " + "\n" +
                                "Οι παρεχόμενες επενδυτικές προτάσεις, σε περίπτωση που αποφασίσετε να τις υλοποιήσετε, θα σας επιφέρουν ένα κόστος συναλλαγής επί των υποκείμενων στην παρούσα επενδυτική πρόταση χρηματοπιστωτικών μέσων, όπως αυτό αναγράφεται στον τιμοκατάλογο της σύμβασης παροχής επενδυτικών συμβουλών που έχετε υπογράψει με την Εταιρία. " + "\n" +
                                "Όφελος " + "\n" +
                                "Οι παρεχόμενες επενδυτικές προτάσεις, σε περίπτωση που αποφασίσετε να τις υλοποιήσετε, θα σας επιφέρουν όφελος/οφέλη, όπως παρουσιάζεται/ονται συνοπτικά παρακάτω.";
                    sDisclimer_Title = "6. Κίνδυνοι Χρηματοπιστωτικών Μέσων";
                    sNotes_Title = "7.  Επισημάνσεις";

                    sContent = sContent + "\n" +
                               "5. Cost - Benefit Analysis \n" +
                               "6. Κίνδυνοι Χρηματοπιστωτικών Μέσων \n" +
                               "7. Επισημάνσεις";
                }

                //-----------------------------------------------------------
                switch (iMiFID_Risk)
                {
                    case 1:
                        sEkthesiKatalilotitas[1] = "Η Εταιρία χρησιμοποιεί τις πληροφορίες που λαμβάνει από τους κατασκευαστές  των χρηματοπιστωτικών μέσων και τις πληροφορίες που της έχουν παράσχει οι πελάτες για να αξιολογήσει ότι τα χρηματοπιστωτικά μέσα που προτείνονται εξυπηρετούν τις ανάγκες, τα χαρακτηριστικά και  τους  στόχους της προσδιορισμένης αγοράς-στόχου (target market). Οι παρεχόμενες επενδυτικές προτάσεις του παρόντος εντύπου είναι κατάλληλες προς το επενδυτικό σας προφίλ κινδύνου, καθώς, βάσει των απαντήσεών σας στο ειδικά διαμορφωμένο ερωτηματολόγιο αξιολόγησης καταλληλότητας της Εταιρίας, και των πληροφοριών που έχει λάβει από τους κατασκευαστές των χρηματοπιστωτικών μέσων, ανταποκρίνονται στους επενδυτικούς σας σκοπούς, τον επενδυτικό σας ορίζοντα, την ανοχή σας απέναντι στους επενδυτικούς κινδύνους και τη δυνατότητα ζημίας σας στα πλαίσια της επένδυσής σας.";
                        sEkthesiKatalilotitas[2] = "Προφίλ Χαμηλού Κινδύνου (Low Risk) - Εισοδήματος";
                        sEkthesiKatalilotitas[3] = "Ιδιώτης, Επαγγελματίας";
                        sEkthesiKatalilotitas[4] = "Ο αποκλειστικός σκοπός είναι η δημιουργία εισοδήματος μέσω της επένδυσης σε χρηματοπιστωτικά μέσα εισοδήματος (ομόλογα, ομολογιακά αμοιβαία κεφάλαια και διαπραγματεύσιμα αμοιβαία κεφάλαια, μέσα χρηματαγοράς και λοιπά χρηματοπιστωτικά μέσα εισοδήματος). Η μέγιστη διάρκεια των χρηματοπιστωτικών μέσων εισοδήματος είναι 3 έτη.";
                        sEkthesiKatalilotitas[5] = "Νόμισμα Αναφοράς (" + sCurrency + ") ή Ξένο Νόμισμα με αντιστάθμιση στο Νόμισμα Αναφοράς";
                        sEkthesiKatalilotitas[6] = "Τουλάχιστον 1 ½ έτος ";
                        sEkthesiKatalilotitas[7] = "έως 100% ανά χρηματοπιστωτικό μέσο";
                        sEkthesiKatalilotitas[8] = "έως 2";
                        sEkthesiKatalilotitas[9] = sDMS_Path + "\\InvestProposals\\EK_1.png";
                        break;
                    case 2:
                        sEkthesiKatalilotitas[1] = "Η Εταιρία χρησιμοποιεί τις πληροφορίες που λαμβάνει από τους κατασκευαστές  των χρηματοπιστωτικών μέσων και τις πληροφορίες που της έχουν παράσχει οι πελάτες για να αξιολογήσει ότι τα χρηματοπιστωτικά μέσα που προτείνονται εξυπηρετούν τις ανάγκες, τα χαρακτηριστικά και  τους  στόχους της προσδιορισμένης αγοράς-στόχου (target market). Οι παρεχόμενες επενδυτικές προτάσεις του παρόντος εντύπου είναι κατάλληλες προς το επενδυτικό σας προφίλ κινδύνου, καθώς, βάσει των απαντήσεών σας στο ειδικά διαμορφωμένο ερωτηματολόγιο αξιολόγησης καταλληλότητας της Εταιρίας, και των πληροφοριών που έχει λάβει από τους κατασκευαστές των χρηματοπιστωτικών μέσων, ανταποκρίνονται στους επενδυτικούς σας σκοπούς, τον επενδυτικό σας ορίζοντα, την ανοχή σας απέναντι στους επενδυτικούς κινδύνους και τη δυνατότητα ζημίας σας στα πλαίσια της επένδυσής σας.";
                        sEkthesiKatalilotitas[2] = "Προφίλ Μεσαίου Κινδύνου (Medium Risk) - Εισοδήματος";
                        sEkthesiKatalilotitas[3] = "Ιδιώτης, Επαγγελματίας";
                        sEkthesiKatalilotitas[4] = "Ο αποκλειστικός σκοπός είναι η δημιουργία εισοδήματος μέσω της επένδυσης σε χρηματοπιστωτικά μέσα εισοδήματος (ομόλογα, ομολογιακά αμοιβαία κεφάλαια και διαπραγματεύσιμα αμοιβαία κεφάλαια, μέσα χρηματαγοράς και λοιπά χρηματοπιστωτικά μέσα εισοδήματος). Η μέγιστη διάρκεια των χρηματοπιστωτικών μέσων εισοδήματος είναι 7 έτη.";
                        sEkthesiKatalilotitas[5] = "Νόμισμα Αναφοράς (" + sCurrency + ")  ή Ξένο Νόμισμα με αντιστάθμιση στο Νόμισμα Αναφοράς";
                        sEkthesiKatalilotitas[6] = "Τουλάχιστον 3 έτη";
                        sEkthesiKatalilotitas[7] = "έως 100% ανά χρηματοπιστωτικό μέσο";
                        sEkthesiKatalilotitas[8] = "έως 4";
                        sEkthesiKatalilotitas[9] = sDMS_Path + "\\InvestProposals\\EK_2.png";
                        break;
                    case 3:
                        sEkthesiKatalilotitas[1] = "Η Εταιρία χρησιμοποιεί τις πληροφορίες που λαμβάνει από τους κατασκευαστές  των χρηματοπιστωτικών μέσων και τις πληροφορίες που της έχουν παράσχει οι πελάτες για να αξιολογήσει ότι τα χρηματοπιστωτικά μέσα που προτείνονται εξυπηρετούν τις ανάγκες, τα χαρακτηριστικά και  τους  στόχους της προσδιορισμένης αγοράς-στόχου (target market). Οι παρεχόμενες επενδυτικές προτάσεις του παρόντος εντύπου είναι κατάλληλες προς το επενδυτικό σας προφίλ κινδύνου, καθώς, βάσει των απαντήσεών σας στο ειδικά διαμορφωμένο ερωτηματολόγιο αξιολόγησης καταλληλότητας της Εταιρίας, και των πληροφοριών που έχει λάβει από τους κατασκευαστές των χρηματοπιστωτικών μέσων, ανταποκρίνονται στους επενδυτικούς σας σκοπούς, τον επενδυτικό σας ορίζοντα, την ανοχή σας απέναντι στους επενδυτικούς κινδύνους και τη δυνατότητα ζημίας σας στα πλαίσια της επένδυσής σας.";
                        sEkthesiKatalilotitas[2] = "Προφίλ Μεσαίου Κινδύνου (Medium Risk) – Εισοδήματος και Κεφαλαιακής Ανάπτυξης";
                        sEkthesiKatalilotitas[3] = "Ιδιώτης, Επαγγελματίας";
                        sEkthesiKatalilotitas[4] = "Ο σκοπός είναι η δημιουργία εισοδήματος μέσω της επένδυσης σε χρηματοπιστωτικά μέσα εισοδήματος (ομόλογα, ομολογιακά αμοιβαία κεφάλαια και διαπραγματεύσιμα αμοιβαία κεφάλαια, μέσα χρηματαγοράς και λοιπά χρηματοπιστωτικά μέσα εισοδήματος) αλλά και η επίτευξη κεφαλαιακής ανάπτυξης μέσω της επένδυσης σε χρηματοπιστωτικά μέσα όπως μετοχικά αμοιβαία κεφάλαια και διαπραγματεύσιμα αμοιβαία κεφάλαια, μικτά αμοιβαία κεφάλαια και διαπραγματεύσιμα αμοιβαία κεφάλαια. Η μέγιστη διάρκεια των χρηματοπιστωτικών μέσων εισοδήματος είναι 7 έτη.";
                        sEkthesiKatalilotitas[5] = "Νόμισμα Αναφοράς (" + sCurrency + ")  ή Ξένο Νόμισμα με αντιστάθμιση στο Νόμισμα Αναφοράς";
                        sEkthesiKatalilotitas[6] = "Τουλάχιστον 5 έτη";
                        sEkthesiKatalilotitas[7] = "έως 100% ανά χρηματοπιστωτικό μέσο";
                        sEkthesiKatalilotitas[8] = "έως 5";
                        sEkthesiKatalilotitas[9] = sDMS_Path + "\\InvestProposals\\EK_3.png";
                        break;
                    case 4:
                        sEkthesiKatalilotitas[1] = "Η Εταιρία χρησιμοποιεί τις πληροφορίες που λαμβάνει από τους κατασκευαστές  των χρηματοπιστωτικών μέσων και τις πληροφορίες που της έχουν παράσχει οι πελάτες για να αξιολογήσει ότι τα χρηματοπιστωτικά μέσα που προτείνονται εξυπηρετούν τις ανάγκες, τα χαρακτηριστικά και  τους  στόχους της προσδιορισμένης αγοράς-στόχου (target market). Οι παρεχόμενες επενδυτικές προτάσεις του παρόντος εντύπου είναι κατάλληλες προς το επενδυτικό σας προφίλ κινδύνου, καθώς, βάσει των απαντήσεών σας στο ειδικά διαμορφωμένο ερωτηματολόγιο αξιολόγησης καταλληλότητας της Εταιρίας, και των πληροφοριών που έχει λάβει από τους κατασκευαστές των χρηματοπιστωτικών μέσων, ανταποκρίνονται στους επενδυτικούς σας σκοπούς, τον επενδυτικό σας ορίζοντα, την ανοχή σας απέναντι στους επενδυτικούς κινδύνους και τη δυνατότητα ζημίας σας στα πλαίσια της επένδυσής σας.";
                        sEkthesiKatalilotitas[2] = "Προφίλ Υψηλού Κινδύνου (High Risk) - Εισοδήματος";
                        sEkthesiKatalilotitas[3] = "Ιδιώτης, Επαγγελματίας";
                        sEkthesiKatalilotitas[4] = "Ο αποκλειστικός σκοπός είναι η δημιουργία εισοδήματος και κεφαλαιακής ανάπτυξης μέσω της επένδυσης σε χρηματοπιστωτικά μέσα εισοδήματος (ομόλογα, ομολογιακά αμοιβαία κεφάλαια και διαπραγματεύσιμα αμοιβαία κεφάλαια, μέσα χρηματαγοράς και λοιπά χρηματοπιστωτικά μέσα εισοδήματος). Δεν υπάρχει μέγιστη διάρκεια χρηματοπιστωτικών μέσων εισοδήματος.";
                        sEkthesiKatalilotitas[5] = "Νόμισμα Αναφοράς (" + sCurrency + ")  και άλλα νομίσματα";
                        sEkthesiKatalilotitas[6] = "Τουλάχιστον 7 έτη";
                        sEkthesiKatalilotitas[7] = "έως 100% ανά χρηματοπιστωτικό μέσο";
                        sEkthesiKatalilotitas[8] = "έως 7";
                        sEkthesiKatalilotitas[9] = sDMS_Path + "\\InvestProposals\\EK_4.png";
                        break;
                    case 5:
                        sEkthesiKatalilotitas[1] = "Η Εταιρία χρησιμοποιεί τις πληροφορίες που λαμβάνει από τους κατασκευαστές  των χρηματοπιστωτικών μέσων και τις πληροφορίες που της έχουν παράσχει οι πελάτες για να αξιολογήσει ότι τα χρηματοπιστωτικά μέσα που προτείνονται εξυπηρετούν τις ανάγκες, τα χαρακτηριστικά και  τους  στόχους της προσδιορισμένης αγοράς-στόχου (target market). Οι παρεχόμενες επενδυτικές προτάσεις του παρόντος εντύπου είναι κατάλληλες προς το επενδυτικό σας προφίλ κινδύνου, καθώς, βάσει των απαντήσεών σας στο ειδικά διαμορφωμένο ερωτηματολόγιο αξιολόγησης καταλληλότητας της Εταιρίας, και των πληροφοριών που έχει λάβει από τους κατασκευαστές των χρηματοπιστωτικών μέσων, ανταποκρίνονται στους επενδυτικούς σας σκοπούς, τον επενδυτικό σας ορίζοντα, την ανοχή σας απέναντι στους επενδυτικούς κινδύνους και τη δυνατότητα ζημίας σας στα πλαίσια της επένδυσής σας.";
                        sEkthesiKatalilotitas[2] = "Προφίλ Υψηλού Κινδύνου (High Risk) - Εισοδήματος και Κεφαλαιακής Ανάπτυξης";
                        sEkthesiKatalilotitas[3] = "Ιδιώτης, Επαγγελματίας";
                        sEkthesiKatalilotitas[4] = "Ο σκοπός είναι η δημιουργία εισοδήματος μέσω της επένδυσης σε χρηματοπιστωτικά μέσα εισοδήματος (ομόλογα, ομολογιακά αμοιβαία κεφάλαια και διαπραγματεύσιμα αμοιβαία κεφάλαια, μέσα χρηματαγοράς και λοιπά χρηματοπιστωτικά μέσα εισοδήματος) αλλά και η επίτευξη κεφαλαιακής ανάπτυξης μέσω της επένδυσης σε χρηματοπιστωτικά μέσα όπως μετοχές, μετοχικά αμοιβαία κεφάλαια και διαπραγματεύσιμα αμοιβαία κεφάλαια, μικτά αμοιβαία κεφάλαια και διαπραγματεύσιμα αμοιβαία κεφάλαια. Δεν υπάρχει μέγιστη διάρκεια χρηματοπιστωτικών μέσων εισοδήματος.";
                        sEkthesiKatalilotitas[5] = "Νόμισμα Αναφοράς (" + sCurrency + ")  και άλλα νομίσματα";
                        sEkthesiKatalilotitas[6] = "Τουλάχιστον 7 έτη";
                        sEkthesiKatalilotitas[7] = "έως 100% ανά χρηματοπιστωτικό μέσο";
                        sEkthesiKatalilotitas[8] = "έως 7";
                        sEkthesiKatalilotitas[9] = sDMS_Path + "\\InvestProposals\\EK_5.png";
                        break;
                    case 6:
                        sEkthesiKatalilotitas[1] = "Η Εταιρία χρησιμοποιεί τις πληροφορίες που λαμβάνει από τους κατασκευαστές  των χρηματοπιστωτικών μέσων και τις πληροφορίες που της έχουν παράσχει οι πελάτες για να αξιολογήσει ότι τα χρηματοπιστωτικά μέσα που προτείνονται εξυπηρετούν τις ανάγκες, τα χαρακτηριστικά και  τους  στόχους της προσδιορισμένης αγοράς-στόχου (target market). Οι παρεχόμενες επενδυτικές προτάσεις του παρόντος εντύπου είναι κατάλληλες προς το επενδυτικό σας προφίλ κινδύνου, καθώς, βάσει των απαντήσεών σας στο ειδικά διαμορφωμένο ερωτηματολόγιο αξιολόγησης καταλληλότητας της Εταιρίας, και των πληροφοριών που έχει λάβει από τους κατασκευαστές των χρηματοπιστωτικών μέσων, ανταποκρίνονται στους επενδυτικούς σας σκοπούς, τον επενδυτικό σας ορίζοντα, την ανοχή σας απέναντι στους επενδυτικούς κινδύνους και τη δυνατότητα ζημίας σας στα πλαίσια της επένδυσής σας.";
                        sEkthesiKatalilotitas[2] = "Προφίλ Υψηλού Κινδύνου (High Risk) - Κεφαλαιακής Ανάπτυξης";
                        sEkthesiKatalilotitas[3] = "Ιδιώτης, Επαγγελματίας";
                        sEkthesiKatalilotitas[4] = "Ο αποκλειστικός σκοπός είναι επίτευξη κεφαλαιακής ανάπτυξης μέσω της επένδυσης σε χρηματοπιστωτικά μέσα όπως μετοχές, μετοχικά αμοιβαία κεφάλαια και διαπραγματεύσιμα αμοιβαία κεφάλαια, και μέσα χρηματαγοράς.";
                        sEkthesiKatalilotitas[5] = "Νόμισμα Αναφοράς (" + sCurrency + ")  και άλλα νομίσματα";
                        sEkthesiKatalilotitas[6] = "Τουλάχιστον 7 έτη";
                        sEkthesiKatalilotitas[7] = "έως 100% ανά χρηματοπιστωτικό μέσο";
                        sEkthesiKatalilotitas[8] = "έως 7";
                        sEkthesiKatalilotitas[9] = sDMS_Path + "\\InvestProposals\\EK_6.png";
                        break;
                }

                //--- create new PDF file --------
                try
                {
                    ReportDocument rptInvestProposal = new ReportDocument();
                    rptInvestProposal.Load("repInvestProposal.rpt");              // Application.StartupPath +
                    //rptInvestProposal = new repInvestProposal();
                    rptInvestProposal.Database.Tables[0].SetDataSource(dtProductList);
                    rptInvestProposal.SetParameterValue(0, sContract);
                    rptInvestProposal.SetParameterValue(1, sCode);
                    rptInvestProposal.SetParameterValue(2, sPortfolio);
                    rptInvestProposal.SetParameterValue(3, sAdvisor);
                    rptInvestProposal.SetParameterValue(4, DateTime.Now.ToString("dd/MM/yyyy"));  // Date
                    rptInvestProposal.SetParameterValue(5, sInvestPolicy);
                    rptInvestProposal.SetParameterValue(6, sService);
                    rptInvestProposal.SetParameterValue(7, sIdeaText);
                    rptInvestProposal.SetParameterValue(8, iRec_ID);
                    rptInvestProposal.SetParameterValue(9, sProviderTitle);
                    rptInvestProposal.SetParameterValue(10, sAdvisorTel);
                    rptInvestProposal.SetParameterValue(11, sAdvisorEMail);
                    rptInvestProposal.SetParameterValue(12, "2310 515100");
                    rptInvestProposal.SetParameterValue(13, "210 3387711");
                    rptInvestProposal.SetParameterValue(14, "");
                    rptInvestProposal.SetParameterValue(15, sAuthor);
                    rptInvestProposal.SetParameterValue(16, sAuthorMobile);
                    rptInvestProposal.SetParameterValue(17, sAuthorEMail);
                    rptInvestProposal.SetParameterValue(18, "");
                    rptInvestProposal.SetParameterValue(19, 1);                                  // i = 0 - not editable, i = 1 - editable
                    rptInvestProposal.SetParameterValue(20, sProducts);
                    rptInvestProposal.SetParameterValue(21, sInvestProfileCustomer);             // InvestProfile
                    rptInvestProposal.SetParameterValue(22, sCostBenefits_Monetary);
                    rptInvestProposal.SetParameterValue(23, sCostBenefits_NonMonetary);
                    rptInvestProposal.SetParameterValue(24, sCurrency);
                    rptInvestProposal.SetParameterValue(25, sInvestPolicy_Header);               // InvestPolicy_Header
                    rptInvestProposal.SetParameterValue(26, sEkthesiKatalilotitas[1]);
                    rptInvestProposal.SetParameterValue(27, sEkthesiKatalilotitas[2]);
                    rptInvestProposal.SetParameterValue(28, sEkthesiKatalilotitas[3]);
                    rptInvestProposal.SetParameterValue(29, sEkthesiKatalilotitas[4]); ;
                    rptInvestProposal.SetParameterValue(30, sEkthesiKatalilotitas[5]);
                    rptInvestProposal.SetParameterValue(31, sEkthesiKatalilotitas[6]);
                    rptInvestProposal.SetParameterValue(32, sEkthesiKatalilotitas[7]);
                    rptInvestProposal.SetParameterValue(33, sEkthesiKatalilotitas[8]);
                    rptInvestProposal.SetParameterValue(34, sEkthesiKatalilotitas[9]);
                    rptInvestProposal.SetParameterValue(35, sCBA_Title);
                    rptInvestProposal.SetParameterValue(36, sCBA_Text);
                    rptInvestProposal.SetParameterValue(37, sDisclimer_Title);
                    rptInvestProposal.SetParameterValue(38, sNotes_Title);
                    rptInvestProposal.SetParameterValue(39, sContent);
                    rptInvestProposal.SetParameterValue(40, sAdvisorMobile);      // AdvisorMobile
                    CrystalDecisions.Shared.ExportOptions rptExportOptions = new CrystalDecisions.Shared.ExportOptions();
                    CrystalDecisions.Shared.DiskFileDestinationOptions rptPath = new CrystalDecisions.Shared.DiskFileDestinationOptions();
                    CrystalDecisions.Shared.PdfRtfWordFormatOptions rptFormatOptions = new CrystalDecisions.Shared.PdfRtfWordFormatOptions();
                    rptPath.DiskFileName = sPDF_FullPath + "\\" + sPDF_FileName;
                    rptExportOptions = rptInvestProposal.ExportOptions;
                    rptExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                    rptExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                    rptExportOptions.DestinationOptions = rptPath;
                    rptExportOptions.FormatOptions = rptFormatOptions;
                    rptInvestProposal.Export();

                    //--- upload new PDF file onto server ---
                    sTemp = "C:/DMS/Customers/" + sContract.Replace(".", "_") + "/InvestProposals/" + iRec_ID;

                    if (!Directory.Exists(sTemp)) Directory.CreateDirectory(sTemp);

                    if (File.Exists(sTemp + "/" + sPDF_FileName)) File.Delete(sTemp + "/" + sPDF_FileName);

                    File.Copy(sPDF_FullPath + "/" + sPDF_FileName, sTemp + "/" + sPDF_FileName);
                    //sTemp = Global.DMS_UploadFile(sPDF_FullPath + "\\" + sPDF_FileName, sTemp, sPDF_FileName);
                    sPDF_FileName = Path.GetFileName(sTemp);
                    conn.Open();
                    iAttempt = iAttempt + 1;
                    cmd = new SqlCommand("UPDATE  ServerJobs SET DateFinish = '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "', Attempt = " + iAttempt + ", Status = 1 WHERE ID = " + iSJ_ID, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    iError = 0;                      // 0 - No errors
                }
                catch (Exception ex)
                {
                    sTemp = ex.Message;
                    Global.AddLogsRecord(0, DateTime.Now, 3, "ISP_IPPDF -> PDF Created - Error = " + iError + ".   ID = " + iRec_ID + ".     Time = " + DateTime.Now);
                }
                finally { }
            }            
        }
        private string RemoteServer2_UploadFile(string sSourceFullFileName, string sTargetPath, string sNewFileName)
        {
            return "";
        }
    }
}
