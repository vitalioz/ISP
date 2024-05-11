using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Threading;
using iTextSharp.text;
using Aspose.Email.Exchange;
using Aspose.Email.Mail;

namespace Core
{
    public class Global
    {
        private static string _sConnString;
        private static string _sConnString2;
        private static string _sConnFIXString;
        private static string _sGridStyle;
        private static Color _clrGridHighlightForeColor;
        private static string _sAppTitle;
        private static int _iUser_ID;
        private static string _sUserName;
        private static string _sUserMobile;
        private static string _sUserEMail;
        private static string _sVersion;
        private static int _iCompany_ID;
        private static string _sCompanyName;
        private static int    _iClientsRequests_Status;
        private static int _iClientsFilter_ID;
        private static string _sClientsFilter;
        private static string _sLEI;
        private static string _sInvoicePrinter;
        private static string _sDBSuffix;

        private static string _sDocFilesPath_Win;
        private static string _sDocFilesPath_FTP;
        private static string _sDocFilesPath_HTTP;
        private static string _sDefaultFolder;
        private static string _sUploadFolder;
        private static string _sDMSTransferPoint;
        private static string _sDMSMapDrive;
        private static string _sDMSMapDriveAddress;

        private static string _sEMail_Sender;
        private static string _sEMail_Username;
        private static string _sEMail_Password;
        private static string _sNonReplay_Sender;
        private static string _sNonReplay_Username;
        private static string _sNonReplay_Password;
        private static string _sRequest_Sender;
        private static string _sRequest_Username;
        private static string _sRequest_Password;
        private static string _sSupport_Sender;
        private static string _sSupport_Username;
        private static string _sSupport_Password;
        private static string _sEMail_BO_Receiver;
        private static string _sFTP_Username;
        private static string _sFTP_Password;
        private static string _sRS_Address;
        private static string _sRS_Username;
        private static string _sRS_Password;
        private static string _sSMS_Username;
        private static string _sSMS_Password;
        private static string _sSMS_From;

        private static int _iUserStatus;
        private static int _iUserLocation;
        private static int _iDivision;
        private static int _iDivisionFilter;

        private static int _iLevel;
        private static int _iLanguage;
        private static int _iCompanyID;
        private static int _iDBReadStep;
        private static int _iDMSAccess;
        private static int _iAllowInsertOldOrders;
        private static string _sFIX_DB_Server_Path;

        private static int _iChief;
        private static int _iRM;
        private static int _iSender;
        private static int _iIntroducer;
        private static int _iDiaxiristis;

        public static DataTable dtBanks;
        public static DataTable dtBrunches;
        public static DataTable dtCashTables;
        public static DataTable dtCheckProblems;
        public static DataTable dtClients;
        public static DataTable dtClientsFilters;
        public static DataTable dtClientsCategories;
        public static DataTable dtContracts;
        public static DataTable dtCountries;
        public static DataTable dtCountriesGroups;
        public static DataTable dtCouponeTypes;
        public static DataTable dtCurrencies;
        public static DataTable dtCustomersProfiles;
        public static DataTable dtDepositories;
        public static DataTable dtDivisions;
        public static DataTable dtDocTypes;
        public static DataTable dtFinanceTools;
        public static DataTable dtHFCategories;
        public static DataTable dtInformMethods;
        public static DataTable dtInvestPolicies;
        public static DataTable dtInvoicesTypes;
        public static DataTable dtMandatoryFiles;
        public static DataTable dtNeeds;
        public static DataTable dtProducts;
        public static DataTable dtProductTypes;
        public static DataTable dtProductsCategories;
        public static DataTable dtRanks;
        public static DataTable dtRatingCodes;
        public static DataTable dtRecieveMethods;
        public static DataTable dtRevocationRights;
        public static DataTable dtServiceProviders;
        public static DataTable dtServices;
        //public static DataTable dtShares;
        public static DataTable dtSpecials;
        public static DataTable dtStockExchanges;
        public static DataTable dtTargetMarketList1;
        public static DataTable dtTargetMarketList2;
        public static DataTable dtTodayEURRates;
        public static DataTable dtTrxActions;
        public static DataTable dtTrxClientsFees;        
        public static DataTable dtTrxFeesTypes;
        public static DataTable dtTrxFeesSubTypes;
        public static DataTable dtTrxTypes;
        public static DataTable dtTrxInvestCategories;
        public static DataTable dtUserList;

        public void Initialization()
        {
            _sConnString = System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString + Global.DBSuffix;
            _sConnString2 = System.Configuration.ConfigurationManager.ConnectionStrings["connStr2"].ConnectionString + Global.DBSuffix;
            _sConnFIXString = System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString + Global.DBSuffix;
            _sVersion = "2.1";
            _sGridStyle = "Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}";
            _clrGridHighlightForeColor = Color.Black;
            _sAppTitle = "ISP";

            _sDefaultFolder = "";
            _sUploadFolder = "";
            _iLevel = 0;
            _iCompanyID = 0;
            _iDBReadStep = 0;

            //--- define general options -----------------------------
            clsOptions Options = new clsOptions();
            Options.GetRecord();
            _iCompany_ID = Options.Company_ID;
            _sCompanyName = Options.Title;
            _sVersion = Options.Version;
            _sDocFilesPath_Win = Options.DocFilesPath_Win;
            _sLEI = Options.LEI;
            _sInvoicePrinter = Options.InvoicePrinter;
            _sFTP_Username = Options.FTP_Username;
            _sFTP_Password = Options.FTP_Password;
            _sRS_Address = Options.RS_Address;
            _sRS_Username = Options.RS_Username;
            _sRS_Password = Options.RS_Password;
            _sNonReplay_Sender = Options.NonReplay_Sender;
            _sNonReplay_Username = Options.NonReplay_Username;
            _sNonReplay_Password = Options.NonReplay_Password;
            _sRequest_Sender = Options.Request_Sender;
            _sRequest_Username = Options.Request_Username;
            _sRequest_Password = Options.Request_Password;
            _sSupport_Sender = Options.Support_Sender;
            _sSupport_Username = Options.Support_Username;
            _sSupport_Password = Options.Support_Password;
            _sEMail_BO_Receiver = Options.EMail_BO_Receiver;
            _iAllowInsertOldOrders = Options.AllowInsertOldOrders;
            _sFIX_DB_Server_Path = Options.FIX_DB_Server_Path;

            //--- define user and his data --------------------------
            GetUserData();          

            //--- define cash data ----------------------------------
            GetBanksList();
            GetBrunchesList();
            GetCashTables();
            GetCheckProblemsList();
            //GetClientsFiltersList();
            GetClientsList();
            GetContractsList();
            GetCountriesList();
            GetCountriesGroupsList();
            GetCouponeTypesList();
            GetCurrenciesList();
            GetCustomersProfilesList();
            GetDepositoriesList();
            GetDivisionsList();
            GetDocTypes();
            GetFinanceToolsList();
            GetHFCategoriesList();
            GetInformMethods();
            GetInvestPoliciesList();
            GetInvoicesTypesList();
            GetMandatoryFilesList();
            GetNeedsList();
            GetProductsList();
            GetProductTypes();
            GetProductCategories();
            GetRanksList();
            GetRatingCodes();
            GetRecieveMethods();
            GetRevocationRights();
            GetServiceProvidersList();
            GetServicesList();
            GetSpecialsList();
            GetStockExchanges();
            GetTargetMarketList1();
            GetTargetMarketList2();
            GetTodayEURRates();
            GetTrxActions();
            GetTrxClientsFees();
            GetTrxFeesTypes();
            GetTrxFeesSubTypes();
            GetTrxTypes();
            GetTrxInvestCategories();
            GetUsersList();
        }
        public void InitConnectionString()
        {
            _sConnString = System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            _sConnString2 = System.Configuration.ConfigurationManager.ConnectionStrings["connStr2"].ConnectionString;
            _sConnFIXString = System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            //--- define general options -----------------------------
            clsOptions Options = new clsOptions();
            Options.GetRecord();
            _sDocFilesPath_Win = Options.DocFilesPath_Win;
            _sEMail_Sender = Options.EMail_Sender;
            _sEMail_Username = Options.EMail_Username;
            _sEMail_Password = Options.EMail_Password;
            _sNonReplay_Sender = Options.NonReplay_Sender;
            _sNonReplay_Username = Options.NonReplay_Username;
            _sNonReplay_Password = Options.NonReplay_Password;
            _sRequest_Sender = Options.Request_Sender;
            _sRequest_Username = Options.Request_Username;
            _sRequest_Password = Options.Request_Password;
            _sSupport_Sender = Options.Support_Sender;
            _sSupport_Username = Options.Support_Username;
            _sSupport_Password = Options.Support_Password;
            _sEMail_BO_Receiver = Options.EMail_BO_Receiver;
            _sSMS_Username = Options.SMS_Username;
            _sSMS_Password = Options.SMS_Password;
            _sSMS_From = Options.SMS_From;
            _sRS_Address = Options.RS_Address;
            _sRS_Username = Options.RS_Username;
            _sRS_Password = Options.RS_Password;
            _iAllowInsertOldOrders = Options.AllowInsertOldOrders;
            _sFIX_DB_Server_Path = Options.FIX_DB_Server_Path;

            GetUserData();
        }
        private void GetUserData()
        {
            clsUsers Users = new clsUsers();
            Users.Record_ID = _iUser_ID;
            Users.GetRecord();
            _sUserName = (Users.Surname + " " + Users.Firstname).Trim();
            _sUserMobile = Users.Mobile;
            _sUserEMail = Users.EMail;
            _iUserStatus = Users.Status;
            _iUserLocation = Users.Location;
            _iDMSAccess = Users.DMSAccess;           
            _iLanguage = Users.Language;
            _iChief = Users.Chief;
            _iRM = Users.RM;
            _iSender = Users.Sender;
            _iIntroducer = Users.Introducer;
            _iDiaxiristis = Users.Diaxiristis;
            _iDivision = Users.Division;
            _iClientsRequests_Status = Users.ClientsRequests_Status;
            _iDivisionFilter = Users.DivisionFilter;
            _iClientsFilter_ID = Users.ClientsFilter_ID;
            _sClientsFilter = Users.ClientsFilter;
            _sDefaultFolder = Users.DefaultFolder;
            _sUploadFolder = Users.UploadFolder;
            _sDMSTransferPoint = Users.DMSTransferPoint;
            _sDocFilesPath_HTTP = Users.DMSDownloadPath;

            switch (_iDMSAccess)
            {
                case 1:                                                 // 1 - Windows TrasferPoint
                    Global.DMSMapDrive = _sDocFilesPath_Win;
                    Global.DMSMapDriveAddress = _sDocFilesPath_Win;
                    break;
                case 2:                                                 // 2 - Mapping
                    Global.DMSMapDrive = "Q:";
                    Global.DMSMapDriveAddress = _sDocFilesPath_HTTP;  // "\\\\hf-hq-trader\\DMS";
                    break;
                case 3:                                                 // 3 - FTP
                    break;
                case 4:                                                 // 4 - Windows Native
                    break;
            }
            //clsClients_Filters Client_Filter = new clsClients_Filters();
            //Client_Filter.Record_ID = _iClientsFilter_ID;
            //Client_Filter.GetRecord();
            if (_iClientsFilter_ID == 4)
                _sClientsFilter = _sClientsFilter + _iDivision;
            else
            {
                _sClientsFilter = _sClientsFilter.Replace("@User_ID", _iUser_ID.ToString());
                _sClientsFilter = _sClientsFilter.Replace("#Clients.", "Clients.");
            }
            _sClientsFilter = _sClientsFilter.Replace("#", "");
        }
        public static void GetBanksList()
        {
            clsBanks Banks = new clsBanks();
            Banks.GetList();
            dtBanks = Banks.List.Copy();
        }
        public static void GetBrunchesList()
        {
            clsBrunches Brunches = new clsBrunches();
            Brunches.GetList();
            dtBrunches = Brunches.List.Copy();
        }
        public static void GetCashTables()
        {
            clsCashTables CashTables = new clsCashTables();
            CashTables.GetList();
            dtCashTables = CashTables.List.Copy();
        }
        public static void GetCheckProblemsList()
        {
            clsCheckProblems CheckProblems = new clsCheckProblems();
            CheckProblems.GetList();
            dtCheckProblems = CheckProblems.List.Copy();
        }
        public static void GetClientsList()
        {
            clsClients Clients = new clsClients();
            Clients.GetCashList();
            dtClients = Clients.List.Copy();
        }
        public static void GetClientsFiltersList()
        {
            clsClients_Filters Clients_Filters = new clsClients_Filters();
            Clients_Filters.GetList();
            dtClientsFilters = Clients_Filters.List.Copy();
        }
        public static void GetContractsList()
        {
            clsContracts Contracts = new clsContracts();
            Contracts.Status = -1;                                    //  -1 - all contracts, 0 - only cancelled contracts, 1 - only actual contracts
            Contracts.ClientsFilter = _sClientsFilter;
            Contracts.GetCashList();
            dtContracts = Contracts.List.Copy();
        }
        public static void GetCountriesList()
        {
            clsCountries Countries = new clsCountries();
            Countries.GetList();
            dtCountries = Countries.List.Copy();
        }
        public static void GetCountriesGroupsList()
        {
            clsCountriesGroups CountriesGroups = new clsCountriesGroups();
            CountriesGroups.GetList();
            dtCountriesGroups = CountriesGroups.List.Copy();
        }
        public static void GetCouponeTypesList()
        {
            clsCouponeTypes CouponeTypes = new clsCouponeTypes();
            CouponeTypes.GetList();
            dtCouponeTypes = CouponeTypes.List.Copy();
        }
        public static void GetCurrenciesList()
        {
            clsCurrencies Currencies = new clsCurrencies();
            Currencies.GetList();
            dtCurrencies = Currencies.List.Copy();
        }
        public static void GetCustomersProfilesList()
        {
            clsCustomersProfiles CustomersProfiles = new clsCustomersProfiles();
            CustomersProfiles.GetList();
            dtCustomersProfiles = CustomersProfiles.List.Copy();
        }
        public static void GetDepositoriesList()
        {
            clsDepositories Depositories = new clsDepositories();
            Depositories.GetList();
            dtDepositories = Depositories.List.Copy();
        }
        public static void GetDivisionsList()
        {
            clsDivisions Divisions = new clsDivisions();
            Divisions.GetList();
            dtDivisions = Divisions.List.Copy();
        }
        public static void GetDocTypes()
        {
            clsDocTypes DocTypes = new clsDocTypes();
            DocTypes.GetList();
            dtDocTypes = DocTypes.List.Copy();
        }
        public static void GetFinanceToolsList()
        {
            clsFinanceTools FinanceTools = new clsFinanceTools();
            FinanceTools.GetList();
            dtFinanceTools = FinanceTools.List.Copy();
        }
        public static void GetHFCategoriesList()
        {
            clsHFCategories HFCategories = new clsHFCategories();
            HFCategories.GetList();
            dtHFCategories = HFCategories.List.Copy();
        }
        public static void GetInformMethods()
        {
            clsInformationMethods InformationMethods = new clsInformationMethods();
            InformationMethods.GetList();
            dtInformMethods = InformationMethods.List.Copy();
        }
        public static void GetInvestPoliciesList()
        {
            clsInvestPolicies InvestPolicies = new clsInvestPolicies();
            InvestPolicies.GetList();
            dtInvestPolicies = InvestPolicies.List.Copy();
        }
        public static void GetInvoicesTypesList()
        {
            clsInvoicesTypes InvoicesTypes = new clsInvoicesTypes();
            InvoicesTypes.GetList();
            dtInvoicesTypes = InvoicesTypes.List.Copy();
        }
        public static void GetMandatoryFilesList()
        {
            clsSystem System = new clsSystem();
            System.GetList_MandatoryFiles();
            dtMandatoryFiles = System.List.Copy();
        }
        public static void GetNeedsList()
        {
            clsNeeds Needs = new clsNeeds();
            Needs.GetList();
            dtNeeds = Needs.List.Copy();
        }
        public static void GetProductsList()
        {
            clsProductsCodes Products = new clsProductsCodes();
            Products.GetCashList();
            dtProducts = Products.List.Copy();
        }
        public static void GetProductTypes()
        {
            clsProducts ProductTypes = new clsProducts();
            ProductTypes.GetProductTypes();
            dtProductTypes = ProductTypes.List.Copy();
        }
        public static void GetProductCategories()
        {
            clsProductsCategories ProductsCategories = new clsProductsCategories();
            ProductsCategories.GetList();
            dtProductsCategories = ProductsCategories.List.Copy();
        }
        public static void GetRanksList()
        {
            clsRanks Ranks = new clsRanks();
            Ranks.GetList();
            dtRanks = Ranks.List.Copy();
        }
        public static void GetRatingCodes()
        {
            clsRatingCodes RatingCodes = new clsRatingCodes();
            RatingCodes.GetList();
            dtRatingCodes = RatingCodes.List.Copy();
        }
        public static void GetRecieveMethods()
        {
            clsRecieveMethods RecieveMethods = new clsRecieveMethods();
            RecieveMethods.GetList();
            dtRecieveMethods = RecieveMethods.List.Copy();
        }
        public static void GetRevocationRights()
        {
            clsRevocationRights RevocationRights = new clsRevocationRights();
            RevocationRights.GetList();
            dtRevocationRights = RevocationRights.List.Copy();
        }        
        public static void GetServiceProvidersList()
        {
            clsServiceProviders ServiceProviders = new clsServiceProviders();
            ServiceProviders.GetList();
            dtServiceProviders = ServiceProviders.List.Copy();
        }
        public static void GetServicesList()
        {
            clsServices Services = new clsServices();
            Services.GetList();
            dtServices = Services.List.Copy();
        }
        public static void GetSpecialsList()
        {
            clsSpecials Specials = new clsSpecials();
            Specials.GetList();
            dtSpecials = Specials.List.Copy();
        }
        public static void GetStockExchanges()
        {
            clsStockExchanges StockExchanges = new clsStockExchanges();
            StockExchanges.GetList();
            dtStockExchanges = StockExchanges.List.Copy();
        }
        public static void GetTargetMarketList1()
        {
            clsSystem System = new clsSystem();
            System.GetList_TargetMarketList1();
            dtTargetMarketList1 = System.List.Copy();
        }
        public static void GetTargetMarketList2()
        {
            clsSystem System = new clsSystem();
            System.GetList_TargetMarketList2();
            dtTargetMarketList2 = System.List.Copy();
        }
        public static void GetTodayEURRates()
        {
            clsCurrencies klsCurrency = new clsCurrencies();
            klsCurrency.DateFrom = DateTime.Now.AddDays(-1);
            klsCurrency.DateTo = DateTime.Now.AddDays(-1);
            klsCurrency.Code = "EUR";
            klsCurrency.GetCurrencyRates_Period();
            dtTodayEURRates = klsCurrency.List.Copy();
        }
        public static void GetTrxActions()
        {
            clsTrxActions TrxActions = new clsTrxActions();
            TrxActions.GetList();
            dtTrxActions = TrxActions.List.Copy();
        }        
        public static void GetTrxClientsFees()
        {
            clsTrxClientsFees TrxClientsFees = new clsTrxClientsFees();
            TrxClientsFees.GetList();
            dtTrxClientsFees = TrxClientsFees.List.Copy();
        }
        public static void GetTrxFeesTypes()
        {
            clsTrxFeesTypes TrxFeesTypes = new clsTrxFeesTypes();
            TrxFeesTypes.GetList();
            dtTrxFeesTypes = TrxFeesTypes.List.Copy();
        }
        public static void GetTrxFeesSubTypes()
        {
            clsTrxFeesSubTypes TrxFeesSubTypes = new clsTrxFeesSubTypes();
            TrxFeesSubTypes.GetList();
            dtTrxFeesSubTypes = TrxFeesSubTypes.List.Copy();
        }
        public static void GetTrxTypes()
        {
            clsTrxTypes TrxTypes = new clsTrxTypes();
            TrxTypes.GetList();
            dtTrxTypes = TrxTypes.List.Copy();
        }
        public static void GetTrxInvestCategories()
        {
            clsTrxInvestCategories TrxInvestCategories = new clsTrxInvestCategories();
            TrxInvestCategories.GetList();
            dtTrxInvestCategories = TrxInvestCategories.List.Copy();
        }
        public static void GetUsersList()
        {
            clsUsers Users = new clsUsers();
            Users.GetList();
            dtUserList = Users.List.Copy();
        }
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
        public struct ContractData
        {
            public string ContractTitle;
            public string Code;
            public string Portfolio;
            public string ClientName;
            public string Service_Title;
            public string Profile_Title;
            public string Policy_Title;
            public string Provider_Title;
            public string Package_Title;
            public string MIFIDCategory_Title;
            public string Currency;
            public string EMail;
            public string Mobile;
            public string NumberAccount;
            public int Contract_ID;
            public int ContractType;
            public int Client_ID;
            public int Provider_ID;
            public int ProviderType;
            public int Policy_ID;
            public int Profile_ID;
            public int Service_ID;
            public int Status;
            public int ClientType;
            public float VAT_Percent;
            public int CFP_ID;
            public int Contracts_Details_ID;
            public int Contracts_Packages_ID;
            public int MIFID_Risk_Index;
            public int MIFIDCategory_ID;
            public int MIFID_2;
            public int XAA;
            public int World;
            public int Europe;
            public int Asia;
            public int Greece;
            public int America;
            public string Geography;
            public string SpecRules;
            public string ComplexProduct;
        }
        public struct ProductData
        {
            public string Title;
            public string Code;
            public string Code2;
            public string ISIN;
            public string SecID;
            public string Product_Title;
            public string Product_Category;
            public string StockExchange_Code;
            public string Currency;
            public int Product_ID;
            public int ProductCategory_ID;
            public int Shares_ID;
            public int ShareCode_ID;
            public int StockExchange_ID;
            public float Weight;
            public float LastClosePrice;
            public string URL_ID;
            public string MIFID_Risk;                   // ShareCodes.MIFID_Risk
            public int Retail;                          // ShareTitles.InvestType_Retail
            public int Professional;                    // ShareTitles.InvestType_Prof
            public int Distrib_ExecOnly;                // ShareTitles.Distrib_ExecOnly
            public int Distrib_Advice;                  // ShareTitles.Distrib_Advice
            public int Distrib_PortfolioManagment;      // ShareTitles.Distrib_PortfolioManagment
            public string RiskCurr;                     // ShareTitles.RiskCurr
            public string CurrencyHedge2;               // ShareCodes.CurrencyHedge2
            public int ComplexProduct;                  // ShareTitles.ComplexProduct
            public string Rank_Title;
            public int IsCallable;
            public int IsPutable;
            public int Leverage;
            public int MiFIDInstrumentType;
            public int AIFMD;
            public int IsConvertible;
            public int IsPerpetualSecurity;
            public string GlobalBroadCategory_Title;
            public string ComplexAttribute;
            public int InvestGeography_ID;              //ShareTitles.CountryRisk_ID -> Countries.InvestGeography_ID
            public int RatingGroup;
            public string ComplexReasonsList;
            public int OK_Flag;
            public string OK_String;
            public int HFIC_Recom;
        }
        public static void SyncExec_SingleOrder(int iCommandExec_ID, int iCommand_ID, decimal decRealPrice, decimal RealQuantity, bool bEditKatamerismos)
        {
            decimal decKoef = 0;

            clsOrdersSecurity OrderExec = new clsOrdersSecurity();
            OrderExec.Record_ID = iCommandExec_ID;
            OrderExec.GetRecord();
            if (OrderExec.Quantity != 0) decKoef = OrderExec.RealQuantity / OrderExec.Quantity;
            else if (OrderExec.Amount != 0) decKoef = OrderExec.RealAmount / OrderExec.Amount;
            else decKoef = 1;

            clsOrdersSecurity Order = new clsOrdersSecurity();
            Order.Record_ID = iCommand_ID;
            Order.GetRecord();
            Order.StockExchange_ID = OrderExec.StockExchange_ID;
            Order.ExecuteDate = OrderExec.ExecuteDate;
            Order.RealPrice = OrderExec.RealPrice;
            if (!bEditKatamerismos)                         // automatos katamerismos
            {
                if (Convert.ToDecimal(Order.Quantity) != 0)
                {
                    Order.RealQuantity = Convert.ToDecimal(Order.Quantity) * decKoef;
                    Order.RealAmount = Order.RealPrice * Order.RealQuantity;
                }
                else if (Convert.ToDecimal(Order.Amount) != 0)
                {
                    Order.RealAmount = Convert.ToDecimal(Order.Amount) * decKoef;
                    if (Order.RealPrice != 0) Order.RealQuantity = Convert.ToDecimal(Order.Amount / Order.RealPrice) * decKoef;
                    else Order.RealQuantity = 0;
                }
                else
                {
                    Order.RealQuantity = Convert.ToDecimal(OrderExec.RealQuantity) * decKoef;
                    Order.RealAmount = Order.RealPrice * Order.RealQuantity;
                }
            }
            if (Order.RealAmount == 0 && Order.RealQuantity == 0)
            {
                Order.RealPrice = 0;
                Order.ExecuteDate = Convert.ToDateTime("1900/01/01");
            }          

            Order.SentDate = OrderExec.SentDate;
            Order.SendCheck = OrderExec.SendCheck;
            Order.CalcFees();

            Order.CurrRate = OrderExec.CurrRate;
            if (Order.PackageType_ID == 3)
            {                                                                                                                 // 3 - Diaxeirisi
                if (Order.InformationMethod_ID == 0)
                {
                    Order.InformationMethod_ID = 7;                                                                           // 7 - Προσωπικά 
                    Global.AddInformingRecord(1, iCommand_ID, 7, 5, Order.Client_ID, Order.Contract_ID, "", "",
                           Global.GetLabel("update_execution_command"), "", "", "", DateTime.Now.ToString(), 1, 1, "");
                }
            }
            Order.EditRecord();
        }
        public static void SyncExec_DPM(int iCommandExec_ID, int iCommandDPM_ID, decimal decRealPrice, decimal RealQuantity)
        {
            decimal decKoef = 0;

            clsOrdersSecurity OrderExec = new clsOrdersSecurity();
            OrderExec.Record_ID = iCommandExec_ID;
            OrderExec.GetRecord();
            if (OrderExec.Quantity != 0) decKoef = OrderExec.RealQuantity / OrderExec.Quantity;

            clsOrdersSecurity OrderDPM = new clsOrdersSecurity();
            OrderDPM.Record_ID = iCommandDPM_ID;
            OrderDPM.GetRecord();
            OrderDPM.StockExchange_ID = OrderExec.StockExchange_ID;
            OrderDPM.ExecuteDate = OrderExec.ExecuteDate;
            OrderDPM.CurrRate = OrderExec.CurrRate;
            OrderDPM.RealPrice = OrderExec.RealPrice;
            OrderDPM.RealQuantity = Convert.ToDecimal(OrderDPM.Quantity) * decKoef;
            OrderDPM.RealAmount = OrderDPM.RealPrice * OrderDPM.RealQuantity;
            OrderDPM.SentDate = OrderExec.SentDate;
            OrderDPM.SendCheck = OrderExec.SendCheck;
            //OrderDPM.CalcFees();

            if (OrderDPM.PackageType_ID == 3) {                                                                                  // 3 - Diaxeirisi
                if (OrderDPM.InformationMethod_ID == 0) {
                    OrderDPM.InformationMethod_ID = 7;                                                                           // 7 - Προσωπικά 
                    Global.AddInformingRecord(1, iCommandDPM_ID, 7, 5, OrderDPM.Client_ID, OrderDPM.Contract_ID, "", "",
                           Global.GetLabel("update_execution_command"), "", "", "", DateTime.Now.ToString(), 1, 1, "");
                }
            }
            OrderDPM.EditRecord();
        }
        public static void SyncDPM_SingleOrder(int iCommand_ID, decimal decRealPrice, decimal RealQuantity)
        {
            int j, iBulcCommand_ID = 0, iBulcCommand2_ID = 0, iRecieveMethod_ID, iDPM_ID;
            string sBulkCommand = "", sCode = "", sPortfolio = "", sNewFileName = "", sTemp = "";
            decimal decPrice = 0, decQuantity = 0, decAmount = 0, decKoef = 0, decSumQuantity = 0, decSumRealQuantity = 0;
            float fltAllocationPercent = 0;

            DateTime dTemp;
            clsOrdersSecurity klsOrder = new clsOrdersSecurity();
            clsOrdersSecurity klsOrder2 = new clsOrdersSecurity();
            clsOrdersSecurity klsOrder3 = new clsOrdersSecurity();
            clsOrdersSecurity NewOrder = new clsOrdersSecurity();

            klsOrder.Record_ID = iCommand_ID;
            klsOrder.GetRecord();

            iDPM_ID = klsOrder.II_ID;
            if (klsOrder.Quantity != 0) decKoef = klsOrder.RealQuantity / klsOrder.Quantity;

            //--- define BulkCommand  ---------------------------------------------------------------------------------
            sBulkCommand = klsOrder.BulkCommand.Replace("<", "").Replace(">", "");
            if (sBulkCommand.Length > 0)
            {
                string[] tokens = sBulkCommand.Split('/');
                if (tokens.Length > 0)
                {
                    iBulcCommand_ID = Convert.ToInt32(tokens[0]);
                    if (tokens.Length > 1) iBulcCommand2_ID = Convert.ToInt32(tokens[1]);
                }
            }

            if (iBulcCommand2_ID == 0) {
                klsOrder2 = new clsOrdersSecurity();
                iBulcCommand2_ID = klsOrder2.GetNextBulkCommand();

                sBulkCommand = "<" + iBulcCommand_ID + ">/<" + iBulcCommand2_ID + ">";
                klsOrder.BulkCommand = sBulkCommand;
                klsOrder.EditRecord();
            }

            //--- define RecieveMethod_ID -----------------------------------------------------------------------------
            iRecieveMethod_ID = 0;
            dTemp = DateTime.Now;

            klsOrder2 = new clsOrdersSecurity();
            klsOrder2.Record_ID = iCommand_ID;
            klsOrder2.GetRecievedFiles();
            foreach (DataRow dtRow in klsOrder2.List.Rows)
            {
                iRecieveMethod_ID = Convert.ToInt32(dtRow["Method_ID"]);
                dTemp = Convert.ToDateTime(dtRow["DateIns"]);
            }

            //--- define and save Allocation records ---------------------------------------------------------------------
            if (iDPM_ID == 0) {                                                       // iDPM_ID = 0 means that this DPM Order was created in RTO and it's Allocations is in Commands table

                klsOrder2 = new clsOrdersSecurity();
                klsOrder2.AktionDate = klsOrder.AktionDate;
                klsOrder2.BulkCommand = iBulcCommand2_ID.ToString();
                klsOrder2.GetList_BulkCommand();
                foreach (DataRow dtRow in klsOrder2.List.Rows) {
                    if (Convert.ToInt32(dtRow["CommandType_ID"]) == 1) {
                        klsOrder3 = new clsOrdersSecurity();
                        klsOrder3.Record_ID = Convert.ToInt32(dtRow["ID"]);
                        klsOrder3.GetRecord();
                        klsOrder3.SentDate = klsOrder.SentDate;
                        klsOrder3.SendCheck = klsOrder.SendCheck;
                        klsOrder3.ExecuteDate = klsOrder.ExecuteDate;
                        klsOrder3.CurrRate = klsOrder.CurrRate;
                        klsOrder3.RealPrice = klsOrder.RealPrice;
                        klsOrder3.RealQuantity = Convert.ToDecimal(dtRow["Quantity"]) * decKoef;
                        klsOrder3.RealAmount = klsOrder3.RealPrice * klsOrder3.RealQuantity;
                        klsOrder3.CalcFees();
                        klsOrder3.CurrRate = klsOrder.CurrRate;
                        klsOrder3.EditRecord();

                        decSumQuantity = decSumRealQuantity + klsOrder3.Quantity;
                        decSumRealQuantity = decSumRealQuantity + klsOrder3.RealQuantity;
                    }
                }
            }
            else {                                                                  // iDPM_ID != 0 means that this DPM Order was created from DPM Tools and it's Allocations is in OrdersDPM_Recs table            
                //--- save Allocation records ----------------------------------------------------------------------------
                clsOrdersDPM_Recs OrdersDPM_Recs = new clsOrdersDPM_Recs();
                OrdersDPM_Recs.DPM_ID = iDPM_ID;
                OrdersDPM_Recs.GetList();
                foreach (DataRow dtRow in OrdersDPM_Recs.List.Rows)
                {
                    sCode = dtRow["Code"] + "";
                    sPortfolio = dtRow["Portfolio"] + "";
                    decPrice = Convert.ToDecimal(dtRow["Price"]);
                    decQuantity = Convert.ToDecimal(dtRow["Quantity"]);
                    decAmount = Convert.ToDecimal(dtRow["Amount"]);

                    clsContracts klsContract = new clsContracts();
                    klsContract.Code = sCode;
                    klsContract.Portfolio = sPortfolio;
                    klsContract.GetRecord_Code_Portfolio();

                    NewOrder = new clsOrdersSecurity();
                    NewOrder.BulkCommand = "<" + iBulcCommand2_ID + ">";
                    NewOrder.BusinessType_ID = 1;
                    NewOrder.CommandType_ID = 1;
                    NewOrder.Client_ID = klsContract.Client_ID;
                    NewOrder.Company_ID = klsOrder.Company_ID;
                    NewOrder.ServiceProvider_ID = klsOrder.ServiceProvider_ID;
                    NewOrder.StockExchange_ID = klsOrder.StockExchange_ID;
                    NewOrder.CustodyProvider_ID = klsOrder.ServiceProvider_ID;
                    NewOrder.Depository_ID = klsOrder.Depository_ID;
                    NewOrder.II_ID = 0;
                    NewOrder.Parent_ID = 0;
                    NewOrder.Contract_ID = klsContract.Record_ID;
                    NewOrder.CFP_ID = klsContract.Packages.CFP_ID;
                    NewOrder.Contract_Details_ID = klsContract.Contract_Details_ID;
                    NewOrder.Contract_Packages_ID = klsContract.Contract_Packages_ID;
                    NewOrder.Code = klsContract.Code;
                    NewOrder.ProfitCenter = klsContract.Portfolio;
                    NewOrder.Aktion = klsOrder.Aktion;
                    NewOrder.AktionDate = klsOrder.AktionDate;
                    NewOrder.Share_ID = klsOrder.Share_ID;
                    NewOrder.Product_ID = klsOrder.Product_ID;
                    NewOrder.ProductCategory_ID = klsOrder.ProductCategory_ID;
                    NewOrder.PriceType = klsOrder.PriceType;
                    NewOrder.Price = decPrice;
                    NewOrder.Quantity = decQuantity;
                    NewOrder.Amount = decAmount;
                    NewOrder.Curr = klsOrder.Curr;
                    NewOrder.CurrRate = klsOrder.CurrRate;
                    NewOrder.Constant = klsOrder.Constant;
                    NewOrder.ConstantDate = klsOrder.ConstantDate;
                    NewOrder.SentDate = klsOrder.SentDate;
                    NewOrder.SendCheck = klsOrder.SendCheck;
                    NewOrder.FIX_A = -1;
                    NewOrder.ExecuteDate = klsOrder.ExecuteDate;
                    NewOrder.RealPrice = klsOrder.RealPrice;
                    NewOrder.RealQuantity = Convert.ToDecimal(dtRow["Quantity"]) * decKoef;
                    NewOrder.RealAmount = NewOrder.RealPrice * NewOrder.RealQuantity;
                    NewOrder.InformationMethod_ID = 7;                                 // 7 -  Προσωπικά for simple DMP orders
                    NewOrder.MainCurr = klsOrder.Curr;
                    NewOrder.FeesCalcMode = 1;
                    NewOrder.CalcFees();
                    NewOrder.RecieveDate = dTemp;
                    NewOrder.RecieveMethod_ID = iRecieveMethod_ID;
                    NewOrder.User_ID = Global.User_ID;
                    NewOrder.DateIns = DateTime.Now;
                    NewOrder.CalcFees();
                    j = NewOrder.InsertRecord();

                    decSumQuantity = decSumQuantity + NewOrder.Quantity;
                    decSumRealQuantity = decSumRealQuantity + NewOrder.RealQuantity;

                    Global.AddInformingRecord(1, j, 7, 5, NewOrder.Client_ID, klsContract.Record_ID, "", "", Global.GetLabel("update_execution_command"),
                        "", "", "", DateTime.Now.ToString(), 1, 1, "");       // 7 - Προσωπικά 

                    //--- add recieved file in each of Single Order that was created above ---------------------------------------
                    klsOrder2 = new clsOrdersSecurity();
                    klsOrder2.Record_ID = iCommand_ID;
                    klsOrder2.GetRecievedFiles();
                    foreach (DataRow dtRow2 in klsOrder2.List.Rows)
                    {

                        sNewFileName = dtRow2["FileName"] + "";
                        if (sNewFileName != "")
                        {
                            if ((dtRow2["FilePath"] + "") != "") sTemp = dtRow2["FilePath"] + "";
                            else
                            {
                                sTemp = "/Customers/OrdersAcception/" + sNewFileName;                                    // path is without client's name
                                sTemp = Global.DMSMapDrive + sTemp.Replace("//", "/");                                   // was curDocFilesPath_FTP & sTemp.Replace("//", "/")
                                Global.DMS_DownloadFile(sTemp, Application.StartupPath + "/Temp/" + sNewFileName);
                            }

                            sNewFileName = Global.DMS_UploadFile(Application.StartupPath + "/Temp/" + sNewFileName, "Customers/OrdersAcception", sNewFileName);
                            //sNewFileName = DMS_UploadFile(sTemp, "Customers/" & sContractTitle.Replace(".", "_") & "/OrdersAcception", sNewFileName)
                            if (sNewFileName.Length > 0) sNewFileName = Path.GetFileName(sNewFileName);
                            else MessageBox.Show("Αρχείο " + dtRow2["FileName"] + " δεν αντιγράφτηκε στο DMS", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                        clsOrders_Recieved Order_Recieved = new clsOrders_Recieved();
                        Order_Recieved.Command_ID = j;
                        Order_Recieved.DateIns = dTemp;
                        Order_Recieved.Method_ID = Convert.ToInt32(dtRow2["Method_ID"]);
                        Order_Recieved.FilePath = "";   //@@@@dtRow2["FilePath"] + "";
                        Order_Recieved.FileName = sNewFileName;
                        Order_Recieved.SourceCommand_ID = iCommand_ID;
                        Order_Recieved.InsertRecord();
                    }
                }
            }
            //--- calculate AllocationPercent for current DPM order ---------------------------------------------
            if (Convert.ToSingle(decSumQuantity) != 0) fltAllocationPercent = 100 * Convert.ToSingle(klsOrder.Quantity) / Convert.ToSingle(decSumQuantity);
            else fltAllocationPercent = 0;
            klsOrder.AllocationPercent = fltAllocationPercent;
            klsOrder.EditRecord();
        }
        public static int AddNewOrders(int iClient_ID, int iRec_ID, string sAktion, int iServiceProvider_ID, string sISIN, int iType, string sCurrency, 
                                       decimal decPrice, decimal decQuantity, int iBestExecution, int sProductStockExchange_ID)
        {
            DataRow[] foundRows;
            int i = 0;
            string sOrdType = "", sTimeInForce = "", sHFAccount_Own = "", sHFAccount_Clients = "", sStockExchange_Title = "";

            //--- define Servic Provider data -----------------------------------
            foundRows = Global.dtServiceProviders.Select("ID = " + iServiceProvider_ID);
            if (foundRows.Length > 0 && (foundRows[0]["FIX_DB"] + "") != "")
            {
                Global.connFIXStr = Global.FIX_DB_Server_Path + "database=" + foundRows[0]["FIX_DB"];
                sHFAccount_Own = foundRows[0]["HFAccount_Own"] + "";
                sHFAccount_Clients = foundRows[0]["HFAccount_Clients"] + "";
            }

            //--- define StockExchange code -----------------------------------
            i = 0;
            foundRows = Global.dtStockExchanges.Select("ID = " + sProductStockExchange_ID);
            if (foundRows.Length > 0)
            {
                sStockExchange_Title = foundRows[0]["Code"] + "";
                i = Convert.ToInt32(foundRows[0]["SortIndex"]);
            }

            if (iServiceProvider_ID == 19)                                      // only for INTESA - change executed StockExchange to main StockExchange
            {
                if (i > 0)
                {
                    foundRows = Global.dtStockExchanges.Select("ID = " + i);
                    if (foundRows.Length > 0)
                        sStockExchange_Title = foundRows[0]["Code"] + "";
                }
            }

            i = 0;
            //--- add record into NewOrders table ------------------------
            clsNewOrders NewOrders = new clsNewOrders();
            NewOrders.SendFlag = 0;
            NewOrders.CurrentTimestamp = DateTime.Now;
            NewOrders.SequenceNumber = 0;
            NewOrders.MsgType = "D";
            NewOrders.PossDupFlag = char.Parse("N");
            NewOrders.ClOrdID = iRec_ID.ToString(); ;
            NewOrders.SecurityID = sISIN;
            NewOrders.IDSource = char.Parse("4");                                    // 4 - ISIN Number
            NewOrders.Symbol = sISIN;
            NewOrders.Currency = sCurrency;
            NewOrders.Account = iClient_ID == 3227 ? sHFAccount_Own : sHFAccount_Clients;
            NewOrders.Side = char.Parse(sAktion);
            switch (iType)
            {
                case 0:
                    sOrdType = "2";             // 2 = Limit
                    break;
                case 1:
                    sOrdType = "1";             // 1 = Market
                    break;
                case 2:
                    sOrdType = "3";             // 3 = Stop
                    break;
            }
            NewOrders.OrdType = char.Parse(sOrdType);
            if (Global.IsNumeric(decPrice.ToString().Replace(",", ".")))
                NewOrders.Price = decPrice.ToString().Replace(",", ".");
            NewOrders.OrderQty = Convert.ToInt32(decQuantity);
            NewOrders.ClientID = "HELLASFIN";
            if (iBestExecution == 1) NewOrders.ExDestination = "X00";
            else NewOrders.ExDestination = sStockExchange_Title;
            sTimeInForce = "0";                         // 0 = Day
            NewOrders.InsType = 1;                      // 1 - Insert Entoli
            NewOrders.TimeInForce = sTimeInForce;
            i = NewOrders.InsertRecord();

            return i;
        }
        public static int CheckISIN(string sISIN)
        {
            clsProductsTitles klsProductTitle = new clsProductsTitles();
            klsProductTitle.ISIN = sISIN;
            klsProductTitle.GetRecord_ISIN();
            return klsProductTitle.Record_ID;
        }
        public static string GetLabel(String _sLabel)
        {
            String sTemp = "";
            switch (_iLanguage) {
                case 1:
                    sTemp = Resources.Greek.ResourceManager.GetObject(_sLabel).ToString();
                    break;
                case 2:
                    sTemp = Resources.English.ResourceManager.GetObject(_sLabel).ToString();
                    break;
            }
            return sTemp;
        }
        public static string RecalcRiskProfile(int iShareCode_ID)
        {
            string sTemp, sLow, sMid1, sMid2, sHigh1, sHigh2, sHigh3, sGlobalBroad, sRatingClass, sResult;
            int iCountryRisk_ID, iCountriesGroup, iInvestAreaEuro;
            float sgMaturity;
            bool bCalc = true;
            DataRow[] foundRows;
            clsProductsCodes klsProductsCodes = new clsProductsCodes();

            sLow = "0";
            sMid1 = "0";
            sMid2 = "0";
            sHigh1 = "0";
            sHigh2 = "0";
            sHigh3 = "0";
            sGlobalBroad = "";
            iInvestAreaEuro = 0;

            klsProductsCodes = new clsProductsCodes();
            klsProductsCodes.Record_ID = iShareCode_ID;
            klsProductsCodes.GetRecord();

            try
            {
                iCountriesGroup = 0;
                iCountryRisk_ID = klsProductsCodes.CountryRisk_ID;
                foundRows = dtCountries.Select("ID=" + klsProductsCodes.CountryRisk_ID);
                if (foundRows.Length > 0) iCountriesGroup = Convert.ToInt32(foundRows[0]["CountriesGroup_ID"]);

                iInvestAreaEuro = 0;
                if ((iCountryRisk_ID == 9) ||                   // 9 - Germany
                    (iCountryRisk_ID == 44) ||                  // 44 - Austria 
                    (iCountryRisk_ID == 52) ||                  // 52 - Belgium
                    (iCountryRisk_ID == 4) ||                   // 4 - France
                    (iCountryRisk_ID == 14) ||                  // 14 - Italy
                    (iCountryRisk_ID == 1) ||                   // 1 - Greece
                    (iCountryRisk_ID == 18) ||                  // 18 - Netherlands 
                    (iCountryRisk_ID == 17) ||                  // 17 - Spain
                    (iCountryRisk_ID == 96))                    // 96 - Euroland 
                    iInvestAreaEuro = 1;

                //--- calculate sRatingClass -----------------------------------------------------
                sRatingClass = "";
                sTemp = klsProductsCodes.SPRating + "";
                if (sTemp != "" && sTemp != "NULL")
                    if (String.Compare(sRatingClass, sTemp) < 0) sRatingClass = sTemp.Substring(0, 1);

                sTemp = klsProductsCodes.FitchsRating + "";
                if (sTemp != "" && sTemp != "NULL")
                    if (String.Compare(sRatingClass, sTemp) < 0) sRatingClass = sTemp.Substring(0, 1);

                sTemp = klsProductsCodes.SPRating + "";
                if (sTemp != "" && sTemp != "NULL")
                    if (String.Compare(sRatingClass, sTemp) < 0) sRatingClass = sTemp.Substring(0, 1);

                switch (klsProductsCodes.Product_ID) {
                    case 1:                            // 1 - Share
                        sHigh2 = "1";
                        sHigh3 = "1";
                        break;

                    case 2:                           // 2 - Bond
                        if (klsProductsCodes.ProductCategory_ID == 44)                      // 44 - ΕΝΤΟΚΑ ΓΡΑΜΜΑΤΙΑ
                        {
                            sLow = "1";
                            sMid1 = "1";
                            sMid2 = "1";
                            sHigh1 = "1";
                            sHigh2 = "1";
                            sHigh3 = "1";
                        }
                        else
                        {
                            TimeSpan t = Convert.ToDateTime(klsProductsCodes.Date2) - Convert.ToDateTime(DateTime.Now);
                            sgMaturity = Convert.ToSingle(t.TotalDays / 365);

                            if (klsProductsCodes.RatingGroup == 1)
                            {
                                if (sgMaturity <= 3)
                                    if ((klsProductsCodes.BondType == 1 && sRatingClass == "A") || (klsProductsCodes.BondType == 2)) sLow = "1";

                                if (sgMaturity <= 7) sMid1 = "1";

                                if (sgMaturity <= 7) sMid2 = "1";

                            }
                            sHigh1 = "1";
                            sHigh2 = "1";
                            sHigh3 = "0";
                        }
                        break;
                    case 4:
                    case 6:                                  // 4 - ETF, 6 - Fund
                        sgMaturity = klsProductsCodes.Maturity;
                        sGlobalBroad = klsProductsCodes.GlobalBroadCategory_Title.ToUpper();

                        if (sGlobalBroad == "" || klsProductsCodes.SurveyedKIID <= 0 || klsProductsCodes.CountryGroup_ID == 0 || klsProductsCodes.CountryRisk_ID == 0)
                            bCalc = false;

                        if (sGlobalBroad == "FIXED INCOME" || sGlobalBroad == "MONEY MARKET" || sGlobalBroad == "MIXED")
                            if (sgMaturity < 0 || klsProductsCodes.RatingGroup == 0)
                                bCalc = false;

                        if (bCalc) {
                            //--- LOW Risk ------------
                            switch (sGlobalBroad) {
                                case "FIXED INCOME":
                                    if (klsProductsCodes.RatingGroup == 1 && sgMaturity <= 3 && (klsProductsCodes.SurveyedKIID <= 2) &&
                                       (klsProductsCodes.CountryGroup_ID == 1 || klsProductsCodes.CountryGroup_ID == 8 || klsProductsCodes.CountryGroup_ID == 9 || klsProductsCodes.CountryGroup_ID == 11))
                                        sLow = "1";
                                    break;

                                case "MONEY MARKET":
                                    if (klsProductsCodes.RatingGroup == 1 && sgMaturity <= 3 && (klsProductsCodes.SurveyedKIID <= 1) &&
                                       (klsProductsCodes.CountryGroup_ID == 1 || klsProductsCodes.CountryGroup_ID == 8 || klsProductsCodes.CountryGroup_ID == 9 || klsProductsCodes.CountryGroup_ID == 11))
                                        sLow = "1";
                                    break;
                            }

                            //--- MID1 Risk ------------
                            switch (sGlobalBroad) {
                                case "FIXED INCOME":
                                    if (klsProductsCodes.RatingGroup == 1 && sgMaturity <= 7 && (klsProductsCodes.SurveyedKIID >= 1 && klsProductsCodes.SurveyedKIID <= 5))
                                        sMid1 = "1";
                                    break;

                                case "MONEY MARKET":
                                    if (klsProductsCodes.RatingGroup == 1 && sgMaturity <= 1 && klsProductsCodes.SurveyedKIID <= 1)
                                        sMid1 = "1";
                                    break;
                            }

                            //--- MID2 Risk ------------
                            switch (sGlobalBroad) {
                                case "FIXED INCOME":
                                    if (klsProductsCodes.RatingGroup == 1 && sgMaturity <= 7 && (klsProductsCodes.SurveyedKIID >= 1 && klsProductsCodes.SurveyedKIID <= 5))
                                        sMid2 = "1";
                                    break;

                                case "MONEY MARKET":
                                    if (klsProductsCodes.RatingGroup == 1 && sgMaturity <= 1 && klsProductsCodes.SurveyedKIID <= 1)
                                        sMid2 = "1";
                                    break;

                                case "EQUITY":
                                    if (klsProductsCodes.SurveyedKIID >= 2 && klsProductsCodes.SurveyedKIID <= 5 &&
                                       (klsProductsCodes.CountryGroup_ID == 1 || klsProductsCodes.CountryGroup_ID == 8 || klsProductsCodes.CountryGroup_ID == 9 || klsProductsCodes.CountryGroup_ID == 11))
                                        sMid2 = "1";
                                    break;

                                case "ALLOCATION":
                                    if (klsProductsCodes.RatingGroup == 1 && sgMaturity <= 7 && klsProductsCodes.SurveyedKIID >= 2 && klsProductsCodes.SurveyedKIID <= 5)
                                        sMid2 = "1";
                                    break;
                            }

                            //--- HIGH1 Risk ------------
                            if ((sGlobalBroad == "FIXED INCOME" || sGlobalBroad == "MONEY MARKET" || sGlobalBroad == "MIXED") && (klsProductsCodes.SurveyedKIID >= 1 && klsProductsCodes.SurveyedKIID <= 7))
                                sHigh1 = "1";

                            //--- HIGH2 Risk ------------
                            if (klsProductsCodes.SurveyedKIID >= 1 && klsProductsCodes.SurveyedKIID <= 7)
                                sHigh2 = "1";


                            //--- HIGH3 Risk ------------
                            if (sGlobalBroad == "EQUITY" || sGlobalBroad == "MONEY MARKET" || sGlobalBroad == "COMMODITIES")
                                sHigh3 = "1";
                        }
                        break;
                }
            }

            catch (Exception z) { MessageBox.Show(klsProductsCodes.CodeTitle + "\n\n" + z.Message, "DB Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { };

            sResult = sLow + sMid1 + sMid2 + sHigh1 + sHigh2 + sHigh3;
            if (sResult == "000000") sResult = "";
            return sResult;
        }
        public decimal CallBondCalc(int iShare_ID, decimal sgPrice, decimal sgQuantity)
        {
            return 0;
        }
        public static System.Boolean IsNumeric(System.Object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            try
            {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            }
            catch { } // just dismiss errors but return false
            return false;
        }
        public static System.Boolean IsDate(String date)
        {
            DateTime Temp;

            if (DateTime.TryParse(date, out Temp) == true)
                return true;
            else
                return false;
        }
        public static void PrintPDF(string sPrintFile)
        {
            System.Diagnostics.ProcessStartInfo p = new System.Diagnostics.ProcessStartInfo();
            p.Verb = "print";
            p.WindowStyle = ProcessWindowStyle.Hidden;
            p.FileName = sPrintFile;
            p.UseShellExecute = true;
            System.Diagnostics.Process.Start(p);
        }
        public static bool MergePdfFiles(string[] pdfFiles, string outputPath, string authorName = "", string creatorName = "", string subject = "", string title = "", string keywords = "")
        {
            bool result = false;
            int pdfCount = 0;     // total input pdf file count
            int f = 0;            // pointer to current input pdf file
            string fileName = string.Empty;   // current input pdf filename
            iTextSharp.text.pdf.PdfReader reader = default;
            int pageCount = 0;    // cureent input pdf page count
            iTextSharp.text.Document pdfDoc = default;    // the output pdf document
            iTextSharp.text.pdf.PdfWriter writer = default;
            iTextSharp.text.pdf.PdfContentByte cb = default;
            // Declare a variable to hold the imported pages
            iTextSharp.text.pdf.PdfImportedPage page = default;
            int rotation = 0;
            // Declare a font to used for the bookmarks
            iTextSharp.text.Font bookmarkFont = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE);
            try
            {
                pdfCount = pdfFiles.Length;
                if (pdfCount > 1)
                {
                    // Open the 1st pad using PdfReader object
                    fileName = pdfFiles[f];
                    reader = new iTextSharp.text.pdf.PdfReader(fileName);
                    // Get page count
                    pageCount = reader.NumberOfPages;
                    // pageCount = GetNumberOfPdfPages(fileName)

                    // Instantiate an new instance of pdf document and set its margins. This will be the output pdf.
                    // NOTE: bookmarks will be added at the 1st page of very original pdf file using its filename. The location
                    // of this bookmark will be placed at the upper left hand corner of the document. So you'll need to adjust
                    // the margin left and margin top values such that the bookmark won't overlay on the merged pdf page. The
                    // unit used is "points" (72 points = 1 inch), thus in this example, the bookmarks' location is at 1/4 inch from
                    // left and 1/4 inch from top of the page. 
                    // reader.GetPageSizeWithRotation(1), 18, 18, 18, 18
                    pdfDoc = new iTextSharp.text.Document(PageSize.A4, 18, 18, 18, 18);
                    // Instantiate a PdfWriter that listens to the pdf document
                    writer = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, new FileStream(outputPath, FileMode.Create));
                    // Set metadata and open the document
                    pdfDoc.AddAuthor(authorName);
                    pdfDoc.AddCreationDate();
                    pdfDoc.AddCreator(creatorName);
                    pdfDoc.AddProducer();
                    pdfDoc.AddSubject(subject);
                    pdfDoc.AddTitle(title);
                    pdfDoc.AddKeywords(keywords);
                    pdfDoc.Open();
                    // Instantiate a PdfContentByte object
                    cb = writer.DirectContent;
                    // Now loop thru the input pdfs

                    // pbProgress.Value = 0
                    // pbProgress.Maximum = pdfCount - 1

                    while (f < pdfCount)
                    {
                        Application.DoEvents();
                        // pbProgress.Value = pbProgress.Value + 1
                        // lblStatus.Text = "Status: Merging " & New System.IO.FileInfo(fileName).Name

                        // Declare a page counter variable
                        int i = 0;
                        // Loop thru the current input pdf's pages starting at page 1
                        while (i < pageCount)
                        {
                            i += 1;
                            // Get the input page size
                            pdfDoc.SetPageSize(reader.GetPageSizeWithRotation(i));
                            // Create a new page on the output document
                            pdfDoc.NewPage();

                            // If it is the 1st page, we add bookmarks to the page

                            // REMOVE FILENAME HEADER
                            // If i = 1 Then
                            // 'First create a paragraph using the filename as the heading
                            // Dim para As New iTextSharp.text.Paragraph(IO.Path.GetFileName(fileName).ToUpper(), bookmarkFont)
                            // 'Then create a chapter from the above paragraph
                            // Dim chpter As New iTextSharp.text.Chapter(para, f + 1)
                            // 'Finally add the chapter to the document
                            // pdfDoc.Add(chpter)
                            // End If
                            // ----------------
                            // Now we get the imported page
                            page = writer.GetImportedPage(reader, i);
                            // Read the imported page's rotation
                            rotation = reader.GetPageRotation(i);
                            // Then add the imported page to the PdfContentByte object as a template based on the page's rotation
                            if (rotation == 90)
                            {
                                cb.AddTemplate(page, 0, -1.0F, 1.0F, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                            }
                            else if (rotation == 270)
                            {
                                cb.AddTemplate(page, 0, 1.0F, -1.0F, 0, reader.GetPageSizeWithRotation(i).Width + 60, -30);
                            }
                            else
                            {
                                cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, 0, 0);
                            }
                        }
                        // Increment f and read the next input pdf file
                        f += 1;
                        if (f < pdfCount)
                        {
                            fileName = pdfFiles[f];
                            reader = new iTextSharp.text.pdf.PdfReader(fileName);
                            pageCount = reader.NumberOfPages;
                            // pageCount = GetNumberOfPdfPages(fileName)
                        }
                    }
                    // When all done, we close the document so that the pdfwriter object can write it to the output file
                    pdfDoc.Close();
                    result = true;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

            return result;
        }
        public static void DefineContractProductsList(DataTable dtClientsProducts, int iContract_ID, int iContract_Details_ID, int iContract_Packages_ID, bool bAddOnlyOKRecords)
        {
            int iMiFID_Risk = 0;
            clsContracts klsContract = new clsContracts();

            klsContract.Record_ID = iContract_ID;
            klsContract.Contract_Details_ID = iContract_Details_ID;
            klsContract.Contract_Packages_ID = iContract_Packages_ID;
            klsContract.GetRecord();
            iMiFID_Risk = klsContract.MiFID_Risk;                                 // from 1 to 6

            if (iMiFID_Risk > 0) {
                DefineKatalilotita(iContract_ID, iContract_Details_ID, iContract_Packages_ID, 1, dtClientsProducts, bAddOnlyOKRecords);
                DefineKatalilotita(iContract_ID, iContract_Details_ID, iContract_Packages_ID, 2, dtClientsProducts, bAddOnlyOKRecords);
                DefineKatalilotita(iContract_ID, iContract_Details_ID, iContract_Packages_ID, 4, dtClientsProducts, bAddOnlyOKRecords);
                DefineKatalilotita(iContract_ID, iContract_Details_ID, iContract_Packages_ID, 6, dtClientsProducts, bAddOnlyOKRecords);
            }
            else
                if (klsContract.Service_ID != 1)            // 1 - Λήψη Διαβίβαση 
                   MessageBox.Show("Warning!!! Check Profile of Contract " + klsContract.ContractTitle);
        }
        private static void DefineKatalilotita(int iContract_ID, int iContract_Details_ID, int iContract_Packages_ID, int iProduct_ID, DataTable dtClientsProducts, bool bAddOnlyOKRecords)
        {
            DataRow dtRow;
            int iOK_Flag = 0;
            string sComplexProduct;
            string[] sDistrib = { "", "Both", "Professional", "Neither", "Retail" };
            string sOK_String = "";            // 1234567  -   1 - MiFID_Risk, 2 - Retail/Profi, 3 - Service_ID,  4 - Currency Risk, 5 - Complex, 6 - Geography, 7 - Special Rules  
            clsContracts klsContract = new clsContracts();
            clsContracts_ComplexSigns klsContracts_ComplexSigns = new clsContracts_ComplexSigns();
            ContractData Contract = new ContractData();
            ProductData Product = new ProductData();

            //--- read Contract's Data -----------------------------------------------------
            klsContract.Record_ID = iContract_ID;
            klsContract.Contract_Details_ID = iContract_Details_ID;
            klsContract.Contract_Packages_ID = iContract_Packages_ID;
            klsContract.GetRecord();
            Contract.Service_ID = klsContract.Service_ID;                                        // 2-Advisory, 3-Discretionary, 5-DealAdvisory
            Contract.Profile_ID = klsContract.Profile_ID;
            Contract.MIFID_Risk_Index = klsContract.MiFID_Risk;                                  // from 1 to 6
            Contract.MIFIDCategory_ID = klsContract.Details.MIFIDCategory_ID;                    // 1 - idiotis, 2 - professional
            Contract.Currency = klsContract.Currency;

            Contract.Geography = (klsContract.Details.ChkWorld == 1 ? "1" : "0") + (klsContract.Details.ChkGreece == 1 ? "1" : "0") + (klsContract.Details.ChkEurope == 1 ? "1" : "0") +
                         (klsContract.Details.ChkAmerica == 1 ? "1" : "0") + (klsContract.Details.ChkAsia == 1 ? "1" : "0");

            Contract.SpecRules = (klsContract.Details.ChkSpecificConstraints == 1 ? "1" : "0") + (klsContract.Details.ChkMonetaryRisk == 1 ? "1" : "0") + (klsContract.Details.ChkIndividualBonds == 1 ? "1" : "0") +
                     (klsContract.Details.ChkMutualFunds == 1 ? "1" : "0") + (klsContract.Details.ChkBondedETFs == 1 ? "1" : "0") + (klsContract.Details.ChkIndividualShares == 1 ? "1" : "0") +
                     (klsContract.Details.ChkMixedFunds == 1 ? "1" : "0") + (klsContract.Details.ChkMixedETFs == 1 ? "1" : "0") + (klsContract.Details.ChkFunds == 1 ? "1" : "0") +
                     (klsContract.Details.ChkETFs == 1 ? "1" : "0") + (klsContract.Details.ChkInvestmentGrade == 1 ? "1" : "0");

            sComplexProduct = "";
            if (klsContract.Details.ChkComplex == 1)
            {
                klsContracts_ComplexSigns = new clsContracts_ComplexSigns();
                klsContracts_ComplexSigns.Contract_ID = iContract_ID;
                klsContracts_ComplexSigns.GetList();
                foreach (DataRow dtRow1 in klsContracts_ComplexSigns.List.Rows)
                    sComplexProduct = sComplexProduct + "," + dtRow1["ComplexSign_ID"];

                if (sComplexProduct.Length > 0) sComplexProduct = sComplexProduct + ",";
            }
            Contract.ComplexProduct = sComplexProduct;

            //--- read Products List & Data and define dtClientsProducts table ---------------------------------------
            clsProductsCodes klsProductsCodes = new clsProductsCodes();
            klsProductsCodes.Product_ID = iProduct_ID;
            klsProductsCodes.GetList_WishList();
            foreach (DataRow dtRow1 in klsProductsCodes.List.Rows) {

                Product.Title = dtRow1["CodeTitle"] + "";
                Product.Code = dtRow1["Code"] + "";
                Product.Code2 = dtRow1["Code2"] + "";
                Product.ISIN = dtRow1["ISIN"] + "";
                Product.SecID = dtRow1["SecID"] + "";
                Product.Product_Title = dtRow1["Product_Title"] + "";
                Product.Product_Category = dtRow1["ProductCategory_Title"] + "";
                Product.StockExchange_Code = dtRow1["StockExchange_Code"] + "";
                Product.Currency = dtRow1["Currency"] + "";
                Product.Product_ID = Convert.ToInt32(dtRow1["Product_ID"]);
                Product.ProductCategory_ID = Convert.ToInt32(dtRow1["ProductCategory_ID"]);
                Product.Shares_ID = Convert.ToInt32(dtRow1["Shares_ID"]);
                Product.ShareCode_ID = Convert.ToInt32(dtRow1["ID"]);
                Product.StockExchange_ID = Convert.ToInt32(dtRow1["StockExchange_ID"]);
                Product.Weight = 0;
                Product.LastClosePrice = Convert.ToSingle(dtRow1["LastClosePrice"]);
                Product.URL_ID = dtRow1["IR_URL"] + "";
                Product.MIFID_Risk = dtRow1["MIFID_Risk"] + "";
                Product.Retail = Convert.ToInt32(dtRow1["Retail"]);
                Product.Professional = Convert.ToInt32(dtRow1["Professional"]);
                Product.Distrib_ExecOnly = Convert.ToInt32(dtRow1["Distrib_ExecOnly"]);
                Product.Distrib_Advice = Convert.ToInt32(dtRow1["Distrib_Advice"]);
                Product.Distrib_PortfolioManagment = Convert.ToInt32(dtRow1["Distrib_PortfolioManagment"]);
                Product.RiskCurr = dtRow1["RiskCurr"] + "";
                Product.CurrencyHedge2 = dtRow1["CurrencyHedge2"] + "";
                Product.ComplexProduct = Convert.ToInt32(dtRow1["ComplexProduct"]);
                Product.Rank_Title = dtRow1["Rank_Title"] + "";
                Product.IsCallable = Convert.ToInt32(dtRow1["IsCallable"]);
                Product.IsPutable = Convert.ToInt32(dtRow1["IsPutable"]);
                Product.Leverage = Convert.ToInt32(dtRow1["Leverage"]);
                Product.MiFIDInstrumentType = Convert.ToInt32(dtRow1["MiFIDInstrumentType"]);
                Product.AIFMD = Convert.ToInt32(dtRow1["AIFMD"]);
                Product.IsConvertible = Convert.ToInt32(dtRow1["IsConvertible"]);
                Product.IsPerpetualSecurity = Convert.ToInt32(dtRow1["IsPerpetualSecurity"]);
                Product.GlobalBroadCategory_Title = dtRow1["GlobalBroadCategory_Title"] + "";
                Product.ComplexAttribute = dtRow1["ComplexAttribute"] + "";
                Product.InvestGeography_ID = Convert.ToInt32(dtRow1["InvestGeography_ID"]);
                Product.RatingGroup = Convert.ToInt32(dtRow1["RatingGroup"]);
                Product.ComplexReasonsList = dtRow1["ComplexReasonsList"] + "";
                //if ((dtRow1["ISIN"]+"") == "ES00000124H4")
                //    iOK_Flag = 1;

                iOK_Flag = 1;
                sOK_String = "";
                if (AccordanceContractProduct(Contract, Product, out iOK_Flag, out sOK_String) || !bAddOnlyOKRecords) {
                    dtRow = dtClientsProducts.NewRow();
                    dtRow["ID"] = dtRow1["ID"];
                    dtRow["CodeTitle"] = dtRow1["CodeTitle"];
                    dtRow["ISIN"] = dtRow1["ISIN"];
                    dtRow["Product_Title"] = dtRow1["Product_Title"];
                    dtRow["ProductCategory_Title"] = dtRow1["ProductCategory_Title"];
                    dtRow["HFCategory_Title"] = dtRow1["HFCategory_Title"];
                    dtRow["SecID"] = dtRow1["SecID"];
                    dtRow["Code"] = dtRow1["Code"];
                    dtRow["Code2"] = dtRow1["Code2"];
                    dtRow["Currency"] = dtRow1["Currency"];
                    dtRow["CreditRating"] = dtRow1["CreditRating"];
                    dtRow["MoodysRating"] = dtRow1["MoodysRating"];
                    dtRow["FitchsRating"] = dtRow1["FitchsRating"];
                    dtRow["SPRating"] = dtRow1["SPRating"];
                    dtRow["ICAPRating"] = dtRow1["ICAPRating"];
                    dtRow["CountryRisk_Title"] = dtRow1["CountryRisk_Title"];
                    dtRow["Date2"] = dtRow1["Date2"];
                    dtRow["Maturity"] = dtRow1["Maturity"];
                    dtRow["Maturity_Date"] = dtRow1["Maturity_Date"];
                    dtRow["CurrencyHedge"] = dtRow1["CurrencyHedge"];
                    dtRow["CurrencyHedge2"] = dtRow1["CurrencyHedge2"];
                    dtRow["SurveyedKIID"] = dtRow1["SurveyedKIID"];
                    dtRow["SurveyedKIID_Date"] = dtRow1["SurveyedKIID_Date"];
                    dtRow["StockExchange_ID"] = dtRow1["StockExchange_ID"];
                    dtRow["StockExchange_Code"] = dtRow1["StockExchange_Code"];
                    dtRow["Weight"] = dtRow1["Weight"];
                    dtRow["LastClosePrice"] = dtRow1["LastClosePrice"];
                    dtRow["IR_URL"] = dtRow1["IR_URL"];
                    dtRow["Retail"] = dtRow1["Retail"];
                    dtRow["Professional"] = dtRow1["Professional"];
                    dtRow["ComplexProduct"] = dtRow1["ComplexProduct"];
                    dtRow["Distrib_ExecOnly"] = sDistrib[Convert.ToInt32(dtRow1["Distrib_ExecOnly"])];
                    dtRow["Distrib_Advice"] = sDistrib[Convert.ToInt32(dtRow1["Distrib_Advice"])];
                    dtRow["Distrib_PortfolioManagment"] = sDistrib[Convert.ToInt32(dtRow1["Distrib_PortfolioManagment"])];
                    dtRow["MIFID_Risk"] = dtRow1["MIFID_Risk"];
                    dtRow["ID"] = dtRow1["ID"];
                    dtRow["Shares_ID"] = dtRow1["Shares_ID"];
                    dtRow["ShareTitles_ID"] = dtRow1["ShareTitles_ID"];
                    dtRow["Product_ID"] = iProduct_ID;
                    dtRow["ProductCategory_ID"] = dtRow1["ProductCategory_ID"];
                    dtRow["OK_Flag"] = iOK_Flag;
                    dtRow["OK_String"] = sOK_String;
                    dtRow["Aktive"] = dtRow1["Aktive"];
                    dtRow["HFIC_Recom"] = dtRow1["HFIC_Recom"];
                    dtClientsProducts.Rows.Add(dtRow);
                }
            }
        }
        public static bool AccordanceContractProduct(ContractData Contract, ProductData Product, out int iOK_Flag, out string sOK_String)
        {
            // sOK_String = 1234567  -   1 - MiFID_Risk, 2 - Retail/Profi, 3 - Service_ID,  4 - Currency Risk, 5 - Complex, 6 - Geography, 7 - Special Rules 
            bool bOK_Flag = false;
            bool bSpecRules, bCheckComplex = false;
            int i = 0;
            string sTemp = "";

            //if (Product.ISIN == "ES00000124H4") i = i;

            //--- 1 - compare Contract.MIFID_Risk_Index with Product.MIFID_Risk ---------------------------------------------
            iOK_Flag = 1;
            sOK_String = "";

            sTemp = Product.MIFID_Risk;
            if (sTemp.Length == 0 || sTemp == "000000") {
                iOK_Flag = 0;
                sOK_String = "0";
            }
            else if (Contract.MIFID_Risk_Index == 0 || sTemp.Substring(Contract.MIFID_Risk_Index - 1, 1) != "1") {
                iOK_Flag = 0;
                sOK_String = "0";
            }
            else sOK_String = "1";

            //--- 2 - compare Contract MiFIDCategory_ID(idiotis or professional) With Product parameter -----------------------
            if (Contract.MIFIDCategory_ID == 1) {                                        // MIFIDCategory_ID = 1   - Idiotis 
                if (Product.Retail != 2) {                                               // Retail != 2  Not equal Idiotis
                    iOK_Flag = 0;
                    sOK_String = sOK_String + "0";
                }
                else sOK_String = sOK_String + "1";
            }
            else {
                if (Contract.MIFIDCategory_ID == 2) {                                   //  iMIFIDCategory_ID = 2   - Profissional
                    if (Product.Professional != 2) {                                    //  Professional!= 2 not equal Professional
                        iOK_Flag = 0;
                        sOK_String = sOK_String + "0";
                    }
                    else sOK_String = sOK_String + "1";
                }
            }

            //--- 3 - check Distribution Strategy ---------------------------------------------------------------------
            if (Contract.MIFIDCategory_ID == 1) {                                       // iMIFIDCategory_ID = 1   - Idiotis 
                switch (Contract.Service_ID) {
                    case 1:                                                             // 1 - Execution
                        if (Product.Distrib_ExecOnly != 1 && Product.Distrib_ExecOnly != 4) {
                            iOK_Flag = 0;
                            sOK_String = sOK_String + "0";
                        }
                        else sOK_String = sOK_String + "1";
                        break;
                    case 2:                                                            // 2 - Advisory
                        if (Product.Distrib_Advice != 1 && Product.Distrib_Advice != 4)
                        {
                            iOK_Flag = 0;
                            sOK_String = sOK_String + "0";
                        }
                        else sOK_String = sOK_String + "1";
                        break;
                    case 3:                                                           // 3 - Discretionary
                        if (Product.Distrib_PortfolioManagment != 1 && Product.Distrib_PortfolioManagment != 4)
                        {
                            iOK_Flag = 0;
                            sOK_String = sOK_String + "0";
                        }
                        else sOK_String = sOK_String + "1";
                        break;
                }

                if (Contract.MIFIDCategory_ID == 2) {                                //  iMIFIDCategory_ID = 2   - Profissional 
                    switch (Contract.Service_ID)
                    {
                        case 1:                                  // 1 - Execution
                            if (Product.Distrib_ExecOnly != 1 && Product.Distrib_ExecOnly != 2) {
                                iOK_Flag = 0;
                                sOK_String = sOK_String + "0";
                            }
                            else sOK_String = sOK_String + "1";
                            break;
                        case 2:                                  // 2 - Advisory
                            if (Product.Distrib_Advice != 1 && Product.Distrib_Advice != 2) {
                                iOK_Flag = 0;
                                sOK_String = sOK_String + "0";
                            }
                            else sOK_String = sOK_String + "1";
                            break;
                        case 3:                                  // 3 - Discretionary
                            if (Product.Distrib_PortfolioManagment != 1 && Product.Distrib_PortfolioManagment != 4) {
                                iOK_Flag = 0;
                                sOK_String = sOK_String + "0";
                            }
                            else sOK_String = sOK_String + "1";
                            break;
                    }
                }

                //--- 4 - check currency Risk ----------------------------------------------------------------------
                if (Contract.MIFID_Risk_Index <= 3)
                {                                  // 3 - Low, Mid1, Mid2
                    if (Contract.Currency == Product.Currency) {
                        if (Contract.Currency != Product.RiskCurr && Contract.Currency != Product.CurrencyHedge2)
                        {
                            iOK_Flag = 0;
                            sOK_String = sOK_String + "0";
                        }
                        else sOK_String = sOK_String + "1";
                    }
                    else
                    {
                        iOK_Flag = 0;
                        sOK_String = sOK_String + "0";
                    }
                }
                else sOK_String = sOK_String + "1";

                //--- 5 - check complex ------------------------------------------------------------------
                switch (Product.ComplexProduct) {
                    case 0:                                                                 // 0 - Product's Complexility is unknown, so we should check Contract Complexility
                        if (Contract.ComplexProduct.Length == 0) {                          // Contract Complexility is NO ...
                            sOK_String = sOK_String + "1";                                  // ...so this Product is OK for this Contract
                        }
                        else {                                                              // Contract Complexility is YES ...
                            iOK_Flag = 0;                                                   // ...so this Product is NOT OK for this Contract
                            sOK_String = sOK_String + "0";
                        }
                        break;
                    case 1:                                                                 // 1 - this product is ΝοnComplex - so it's OK
                        sOK_String = sOK_String + "1";
                        break;
                    case 2:
                        //MsgBox(Product.ComplexAttribute"] & "   " & Product.ComplexReasonsList"] & "  <-> " & sComplexProduct)
                        bCheckComplex = true;

                        if (Contract.MIFIDCategory_ID == 1)
                        {                                                                   // 1 - Idiotis.      Professional is OK
                            if (Product.ComplexReasonsList != "") {                               // != "" means that this product has Complex Reasons (from Reuters)
                                string[] tmpArray = Product.ComplexReasonsList.Split(',');
                                for (i = 0; i <= tmpArray.Length - 2; i++) {
                                    switch (tmpArray[i])
                                    {
                                        case "1":          // 1 - Complex Guarantee Bond
                                            bCheckComplex = false;
                                            break;
                                        case "2":          // 2 - Complex Mechanism to Calculate Return
                                            bCheckComplex = false;
                                            break;
                                        case "3":          // 3 - Complex Returns On Principal
                                            bCheckComplex = false;
                                            break;
                                        case "4":          // 4 - Instrument Embedding a Derivative
                                                           //bCheckComplex = false;
                                            break;
                                        case "5":          // 5 - Instrument Issued via SPV
                                            if (Contract.ComplexProduct.IndexOf(",13,") < 0)                    // 13 - Assets.SPV
                                                bCheckComplex = false;
                                            break;
                                        case "6":          // 6 - Instrument Lacking Redemption Or Maturity date
                                            bCheckComplex = false;
                                            break;
                                        case "7":          // 7 - Instrument With Leverage Features
                                            bCheckComplex = false;
                                            break;
                                        case "8":          // 8 - Issuer Discretion To Modify Cash Flows
                                            bCheckComplex = false;
                                            break;
                                        case "9":          // 9 - Subordinated
                                            if (Contract.ComplexProduct.IndexOf(",12,") < 0)
                                            {                    // 12 - Assets.Subordinated debt instruments
                                                sTemp = Product.Rank_Title.ToUpper();
                                                if (sTemp.IndexOf("SUBORDINATED") >= 0)
                                                {
                                                    if (Contract.ComplexProduct.IndexOf(",12,") < 0)
                                                        bCheckComplex = false;
                                                }
                                            }
                                            break;
                                    }
                                }
                            }

                            if (Product.IsCallable == 2 || Product.IsPutable == 2 ||                                   // 2 - Yes
                                (Product.ComplexAttribute + "").ToUpper() == ("Callable-Derv").ToUpper() ||
                                (Product.ComplexAttribute + "").ToUpper() == ("Putable-Derv").ToUpper() ||
                                (Product.ComplexAttribute + "").ToUpper() == ("Make Whole Call").ToUpper())
                            {
                                if (Contract.ComplexProduct.IndexOf(",3,") < 0) bCheckComplex = false;
                            }

                            if (Product.Leverage == 1)                                                                       // 1 - Yes
                                if (Contract.ComplexProduct.IndexOf(",4,") < 0)
                                    bCheckComplex = false;


                            if (Product.MiFIDInstrumentType == 2)                                                            // 2 - Non UCITS
                                if (Contract.ComplexProduct.IndexOf(",5,") < 0)
                                    bCheckComplex = false;


                            if (Product.AIFMD == 1)                                                                          // 1 - Yes 
                                if (Contract.ComplexProduct.IndexOf(",6,") < 0)
                                    bCheckComplex = false;


                            if (Product.GlobalBroadCategory_Title.ToUpper() == "COMMODITIES")
                                if (Contract.ComplexProduct.IndexOf(",7,") < 0 && Contract.ComplexProduct.IndexOf(",8,") < 0)
                                    bCheckComplex = false;


                            if (Product.IsConvertible == 2)                                                                  // 2 - Yes
                                if (Contract.ComplexProduct.IndexOf(",10,") < 0)
                                    bCheckComplex = false;

                            if (Product.IsPerpetualSecurity == 2)                                                            // 2 - Yes
                                if (Contract.ComplexProduct.IndexOf(",11,") < 0)
                                    bCheckComplex = false;
                        }
                        if (bCheckComplex) sOK_String = sOK_String + "1";
                        else {
                            iOK_Flag = 0;
                            sOK_String = sOK_String + "0";
                        }
                        break;
                }
            }

            //--- 6 - check contract geography------------------------------------------------------------------
            if (Contract.Geography.Substring(0, 1) == "1") {                      // = 1  -  World
                sOK_String = sOK_String + "1";
            }
            else {
                if (Contract.Geography.Substring(Product.InvestGeography_ID - 1, 1) != "1")
                {
                    iOK_Flag = 0;
                    sOK_String = sOK_String + "0";
                }
                else sOK_String = sOK_String + "1";
            }

            //--- 7 - check contract special rules (Ειδικές Οδηγίες)--------------------------------------------
            bSpecRules = true;
            if (Contract.SpecRules.Substring(0, 1) == "1") {

                if (Contract.SpecRules.Substring(1, 1) == "1")                                                   //  2 - Monetary Risk          Δεν επιθυμεί να αναλάβει νομισματικό κίνδυνο
                    if (Contract.Currency != Product.RiskCurr && Contract.Currency != Product.CurrencyHedge2)
                        bSpecRules = false;


                if (Contract.SpecRules.Substring(2, 1) == "1" && Convert.ToInt32(Product.Product_ID) == 2)                     //  3 - Individual Bonds      Δεν επιθυμεί την επένδυση στα :  Μεμονωμένα ομόλογα
                    bSpecRules = false;

                if (Contract.SpecRules.Substring(3, 1) == "1" && Convert.ToInt32(Product.Product_ID) == 6 &&
                        Convert.ToInt32(Product.ProductCategory_ID) == 12)                                             //  4 - MutualFunds   Δεν επιθυμεί την επένδυση στα :  Ομολογιακά Αμοιβαία Κεφάλαια
                    bSpecRules = false;

                if (Contract.SpecRules.Substring(4, 1) == "1" && Convert.ToInt32(Product.Product_ID) == 4 &&
                    Convert.ToInt32(Product.ProductCategory_ID) == 3)                                                   //  5 - BondedETFs             Δεν επιθυμεί την επένδυση στα :  Ομολογιακά Διαπραγματεύσιμα Αμοιβαία Κεφάλαια
                    bSpecRules = false;

                if (Contract.SpecRules.Substring(5, 1) == "1" && Convert.ToInt32(Product.Product_ID) == 1)                      //  6 - IndividualShares       Δεν επιθυμεί την επένδυση στα :  Μεμονωμένες Μετοχές 
                    bSpecRules = false;

                if (Contract.SpecRules.Substring(6, 1) == "1" && Convert.ToInt32(Product.Product_ID) == 6 &&
                   (Convert.ToInt32(Product.ProductCategory_ID) == 11 || Convert.ToInt32(Product.ProductCategory_ID) == 14 ||
                   Convert.ToInt32(Product.ProductCategory_ID) == 15 || Convert.ToInt32(Product.ProductCategory_ID) == 16))      // 7 - MixedFunds             Δεν επιθυμεί την επένδυση στα :  Μετοχικά και Μεικτά Αμοιβαία Κεφάλαια  
                    bSpecRules = false;

                if (Contract.SpecRules.Substring(7, 1) == "1" && Convert.ToInt32(Product.Product_ID) == 4 &&
                   (Convert.ToInt32(Product.ProductCategory_ID) == 6 || Convert.ToInt32(Product.ProductCategory_ID) == 8))      //  8 - MixedETFs             Δεν επιθυμεί την επένδυση στα :  Μετοχικά και Μεικτά Διαπραγματεύσιμα Αμοιβαία Κεφάλαια 
                    bSpecRules = false;

                if (Contract.SpecRules.Substring(8, 1) == "1" && Convert.ToInt32(Product.Product_ID) == 6)                     //  9 - Funds                 Δεν επιθυμεί την επένδυση στα : Αμοιβαία Κεφάλαια   6 - it's MuturalFund
                    if (Convert.ToInt32(Product.AIFMD) == 2 && (Convert.ToInt32(Product.MiFIDInstrumentType) == 0 || Convert.ToInt32(Product.MiFIDInstrumentType) == 1))
                        bSpecRules = false;

                if (Contract.SpecRules.Substring(9, 1) == "1" && Convert.ToInt32(Product.Product_ID) == 4)                    // 10 - ETFs                  Δεν επιθυμεί την επένδυση στα : Διαπραγματεύσιμα Αμοιβαία Κεφάλαια   4 - it's ETF
                    if (Convert.ToInt32(Product.AIFMD) == 2 && (Convert.ToInt32(Product.MiFIDInstrumentType) == 0 || Convert.ToInt32(Product.MiFIDInstrumentType) == 1))
                        bSpecRules = false;


                if (Contract.SpecRules.Substring(10, 1) == "1")                                                 // 11 - Mono Investment Grade        Επιθυμεί Mono Investment Grade
                    if (Convert.ToInt32(Product.RatingGroup) != 1)
                        bSpecRules = false;

            }
            if (!bSpecRules) {
                iOK_Flag = 0;
                sOK_String = sOK_String + "0";
            }
            else sOK_String = sOK_String + "1";

            if (iOK_Flag == 1) bOK_Flag = true;

            return bOK_Flag;
        }
        private static void DefineKatalilotitaOLD(int iProduct_ID, DataTable dtClientsProducts, int iService_ID, int iMiFID_Risk, int iMIFIDCategory_ID,
                                        string sComplexProduct, string sCurrency, string sGeography, string sSpecRules, bool bAddOnlyOKRecords)
        {
            DataRow dtRow;
            int i, iOK_Flag;
            string sTemp;
            string sOK_String;                    // 1234567  -   1 - MiFID_Risk, 2 - Retail/Profi, 3 - Service_ID,  4 - Currency Risk, 5 - Complex, 6 - Geography, 7 - Special Rules  
            string[] tmpArray;
            string[] sDistrib = { "", "Both", "Professional", "Neither", "Retail" };
            bool bSpecRules, bCheckComplex = false;

            //--- define Recommended Products List -----------------------------------
            clsProductsCodes klsProductsCodes = new clsProductsCodes();
            klsProductsCodes.Product_ID = iProduct_ID;
            klsProductsCodes.GetList_WishList();
            foreach (DataRow dtRow1 in klsProductsCodes.List.Rows) {

                //--- 1 - compare Contract Profile with Products Risk Profile ---------------------------------------------
                iOK_Flag = 1;
                sOK_String = "";

                sTemp = dtRow1["MIFID_Risk"] + "";
                if (sTemp.Substring(iMiFID_Risk, 1) != "1") {
                    iOK_Flag = 0;
                    sOK_String = "0";
                }
                else sOK_String = "1";

                //--- 2 - compare Contract MiFIDCategory_ID(idiotis or professional) With Product parameter -----------------------
                if (iMIFIDCategory_ID == 1) {                                          // iMIFIDCategory_ID = 1   - Idiotis 
                    if (Convert.ToInt32(dtRow1["Retail"]) != 2) {                      // dtRow1["Retail"] != 2  Not equal Idiotis
                        iOK_Flag = 0;
                        sOK_String = sOK_String + "0";
                    }
                    else sOK_String = sOK_String + "1";
                }
                else {
                    if (iMIFIDCategory_ID == 2) {                                     //  iMIFIDCategory_ID = 2   - Profissional
                        if (Convert.ToInt32(dtRow1["Professional"]) != 2) {           //  dtRow1["Professional"] != 2  not equal Professional
                            iOK_Flag = 0;
                            sOK_String = sOK_String + "0";
                        }
                        else sOK_String = sOK_String + "1";
                    }
                }

                //--- 3 - check Distribution Strategy ---------------------------------------------------------------------
                if (iMIFIDCategory_ID == 1) {                                         // iMIFIDCategory_ID = 1   - Idiotis 
                    switch (iService_ID) {
                        case 1:                                             // 1 - Execution
                            if (Convert.ToInt32(dtRow1["Distrib_ExecOnly"]) != 1 && Convert.ToInt32(dtRow1["Distrib_ExecOnly"]) != 4) {
                                iOK_Flag = 0;
                                sOK_String = sOK_String + "0";
                            }
                            else sOK_String = sOK_String + "1";
                            break;
                        case 2:                                              // 2 - Advisory
                            if (Convert.ToInt32(dtRow1["Distrib_Advice"]) != 1 && Convert.ToInt32(dtRow1["Distrib_Advice"]) != 4)
                            {
                                iOK_Flag = 0;
                                sOK_String = sOK_String + "0";
                            }
                            else sOK_String = sOK_String + "1";
                            break;
                        case 3:                                              // 3 - Discretionary
                            if (Convert.ToInt32(dtRow1["Distrib_PortfolioManagment"]) != 1 && Convert.ToInt32(dtRow1["Distrib_PortfolioManagment"]) != 4)
                            {
                                iOK_Flag = 0;
                                sOK_String = sOK_String + "0";
                            }
                            else sOK_String = sOK_String + "1";
                            break;
                    }

                    if (iMIFIDCategory_ID == 2) {                                //  iMIFIDCategory_ID = 2   - Profissional 
                        switch (iService_ID) {
                            case 1:                                  // 1 - Execution
                                if (Convert.ToInt32(dtRow1["Distrib_ExecOnly"]) != 1 && Convert.ToInt32(dtRow1["Distrib_ExecOnly"]) != 2) {
                                    iOK_Flag = 0;
                                    sOK_String = sOK_String + "0";
                                }
                                else sOK_String = sOK_String + "1";
                                break;
                            case 2:                                  // 2 - Advisory
                                if (Convert.ToInt32(dtRow1["Distrib_Advice"]) != 1 && Convert.ToInt32(dtRow1["Distrib_Advice"]) != 2) {
                                    iOK_Flag = 0;
                                    sOK_String = sOK_String + "0";
                                }
                                else sOK_String = sOK_String + "1";
                                break;
                            case 3:                                  // 3 - Discretionary
                                if (Convert.ToInt32(dtRow1["Distrib_PortfolioManagment"]) != 1 && Convert.ToInt32(dtRow1["Distrib_PortfolioManagment"]) != 4) {
                                    iOK_Flag = 0;
                                    sOK_String = sOK_String + "0";
                                }
                                else sOK_String = sOK_String + "1";
                                break;
                        }
                    }

                    //--- 4 - check currency Risk ----------------------------------------------------------------------
                    if (iMiFID_Risk <= 3) {                                  // 3 - Low, Mid1, Mid2
                        if (sCurrency == (dtRow1["Currency"] + "")) {
                            if (sCurrency != (dtRow1["RiskCurr"] + "") && sCurrency != (dtRow1["CurrencyHedge2"] + "")) {
                                iOK_Flag = 0;
                                sOK_String = sOK_String + "0";
                            }
                            else sOK_String = sOK_String + "1";
                        }
                        else {
                            iOK_Flag = 0;
                            sOK_String = sOK_String + "0";
                        }
                    }
                    else sOK_String = sOK_String + "1";

                    //--- 5 - check complex ------------------------------------------------------------------
                    switch (dtRow1["ComplexProduct"]) {
                        case 0:                                                                 // 0 - Product's Complexility is unknown, so we should check Contract Complexility
                            if (sComplexProduct.Length == 0) {                                  // Contract Complexility is NO ...
                                sOK_String = sOK_String + "1";                                  // ...so this Product is OK for this Contract
                            }
                            else {                                                              // Contract Complexility is YES ...
                                iOK_Flag = 0;                                                   // ...so this Product is NOT OK for this Contract
                                sOK_String = sOK_String + "0";
                            }
                            break;
                        case 1:          // 1 - this product is ΝοnComplex - so it's OK
                            sOK_String = sOK_String + "1";
                            break;
                        case 2:
                            //MsgBox(dtRow1["ComplexAttribute"] & "   " & dtRow1["ComplexReasonsList"] & "  <-> " & sComplexProduct)
                            bCheckComplex = true;

                            if (iMIFIDCategory_ID == 1) {                                          // 1 - Idiotis.      Professional is OK
                                if ((dtRow1["ComplexReasonsList"] + "") != "") {                   // != "" means that this product has Complex Reasons (from Reuters)
                                    tmpArray = (dtRow1["ComplexReasonsList"] + "").Split(',');
                                    for (i = 0; i <= tmpArray.Length - 2; i++) {
                                        switch (tmpArray[i]) {
                                            case "1":          // 1 - Complex Guarantee Bond
                                                bCheckComplex = false;
                                                break;
                                            case "2":          // 2 - Complex Mechanism to Calculate Return
                                                bCheckComplex = false;
                                                break;
                                            case "3":          // 3 - Complex Returns On Principal
                                                bCheckComplex = false;
                                                break;
                                            case "4":          // 4 - Instrument Embedding a Derivative
                                                               //bCheckComplex = false;
                                                break;
                                            case "5":          // 5 - Instrument Issued via SPV
                                                if (sComplexProduct.IndexOf(",13,") < 0)                    // 13 - Assets.SPV
                                                    bCheckComplex = false;
                                                break;
                                            case "6":          // 6 - Instrument Lacking Redemption Or Maturity date
                                                bCheckComplex = false;
                                                break;
                                            case "7":          // 7 - Instrument With Leverage Features
                                                bCheckComplex = false;
                                                break;
                                            case "8":          // 8 - Issuer Discretion To Modify Cash Flows
                                                bCheckComplex = false;
                                                break;
                                            case "9":          // 9 - Subordinated
                                                if (sComplexProduct.IndexOf(",12,") < 0) {                    // 12 - Assets.Subordinated debt instruments
                                                    sTemp = (dtRow1["Rank_Title"] + "").ToUpper();
                                                    if (sTemp.IndexOf("SUBORDINATED") >= 0) {
                                                        if (sComplexProduct.IndexOf(",12,") < 0)
                                                            bCheckComplex = false;
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                }

                                if (Convert.ToInt32(dtRow1["IsCallable"]) == 2 || Convert.ToInt32(dtRow1["IsPutable"]) == 2 ||                       // 2 - Yes
                                    (dtRow1["ComplexAttribute"] + "").ToUpper() == ("Callable-Derv").ToUpper() ||
                                    (dtRow1["ComplexAttribute"] + "").ToUpper() == ("Putable-Derv").ToUpper() ||
                                    (dtRow1["ComplexAttribute"] + "").ToUpper() == ("Make Whole Call").ToUpper()) {
                                    if (sComplexProduct.IndexOf(",3,") < 0) bCheckComplex = false;
                                }

                                if (Convert.ToInt32(dtRow1["Leverage"]) == 1)                                                                       // 1 - Yes
                                    if (sComplexProduct.IndexOf(",4,") < 0)
                                        bCheckComplex = false;


                                if (Convert.ToInt32(dtRow1["MiFIDInstrumentType"]) == 2)                                                            // 2 - Non UCITS
                                    if (sComplexProduct.IndexOf(",5,") < 0)
                                        bCheckComplex = false;


                                if (Convert.ToInt32(dtRow1["AIFMD"]) == 1)                                                                          // 1 - Yes 
                                    if (sComplexProduct.IndexOf(",6,") < 0)
                                        bCheckComplex = false;


                                if ((dtRow1["GlobalBroadCategory_Title"] + "").ToUpper() == "COMMODITIES")
                                    if (sComplexProduct.IndexOf(",7,") < 0 && sComplexProduct.IndexOf(",8,") < 0)
                                        bCheckComplex = false;


                                if (Convert.ToInt32(dtRow1["IsConvertible"]) == 2)                                                                 // 2 - Yes
                                    if (sComplexProduct.IndexOf(",10,") < 0)
                                        bCheckComplex = false;

                                if (Convert.ToInt32(dtRow1["IsPerpetualSecurity"]) == 2)                                                            // 2 - Yes
                                    if (sComplexProduct.IndexOf(",11,") < 0)
                                        bCheckComplex = false;
                            }
                            break;
                    }

                    if (bCheckComplex) sOK_String = sOK_String + "1";
                    else {
                        iOK_Flag = 0;
                        sOK_String = sOK_String + "0";
                    }
                }

                //--- 6 - check contract geography------------------------------------------------------------------
                if (sGeography.Substring(1, 1) == "1") {                      // = 1  -  World
                    sOK_String = sOK_String + "1";
                }
                else {
                    if (sGeography.Substring(Convert.ToInt32(dtRow1["InvestGeography_ID"]) - 1, 1) != "1") {
                        iOK_Flag = 0;
                        sOK_String = sOK_String + "0";
                    }
                    else sOK_String = sOK_String + "1";
                }

                //--- 7 - check contract special rules (Ειδικές Οδηγίες)--------------------------------------------
                bSpecRules = true;
                if (sSpecRules.Substring(1, 1) == "1") {

                    if (sSpecRules.Substring(2, 1) == "1")                                                   //  2 - Monetary Risk          Δεν επιθυμεί να αναλάβει νομισματικό κίνδυνο
                        if (sCurrency != (dtRow1["RiskCurr"] + "") && sCurrency != (dtRow1["CurrencyHedge2"] + ""))
                            bSpecRules = false;


                    if (sSpecRules.Substring(3, 1) == "1" && Convert.ToInt32(dtRow1["Product_ID"]) == 2)                     //  3 - Individual Bonds      Δεν επιθυμεί την επένδυση στα :  Μεμονωμένα ομόλογα
                        bSpecRules = false;

                    if (sSpecRules.Substring(4, 1) == "1" && Convert.ToInt32(dtRow1["Product_ID"]) == 6 &&
                            Convert.ToInt32(dtRow1["ProductCategory_ID"]) == 12)                                            //  4 - MutualFunds   Δεν επιθυμεί την επένδυση στα :  Ομολογιακά Αμοιβαία Κεφάλαια
                        bSpecRules = false;

                    if (sSpecRules.Substring(5, 1) == "1" && Convert.ToInt32(dtRow1["Product_ID"]) == 4 &&
                        Convert.ToInt32(dtRow1["ProductCategory_ID"]) == 3)                                            //  5 - BondedETFs             Δεν επιθυμεί την επένδυση στα :  Ομολογιακά Διαπραγματεύσιμα Αμοιβαία Κεφάλαια
                        bSpecRules = false;

                    if (sSpecRules.Substring(6, 1) == "1" && Convert.ToInt32(dtRow1["Product_ID"]) == 1)                    //  6 - IndividualShares       Δεν επιθυμεί την επένδυση στα :  Μεμονωμένες Μετοχές 
                        bSpecRules = false;

                    if (sSpecRules.Substring(7, 1) == "1" && Convert.ToInt32(dtRow1["Product_ID"]) == 6 &&
                       (Convert.ToInt32(dtRow1["ProductCategory_ID"]) == 11 || Convert.ToInt32(dtRow1["ProductCategory_ID"]) == 14 ||
                       Convert.ToInt32(dtRow1["ProductCategory_ID"]) == 15 || Convert.ToInt32(dtRow1["ProductCategory_ID"]) == 16))      // 7 - MixedFunds             Δεν επιθυμεί την επένδυση στα :  Μετοχικά και Μεικτά Αμοιβαία Κεφάλαια  
                        bSpecRules = false;

                    if (sSpecRules.Substring(8, 1) == "1" && Convert.ToInt32(dtRow1["Product_ID"]) == 4 &&
                       (Convert.ToInt32(dtRow1["ProductCategory_ID"]) == 6 || Convert.ToInt32(dtRow1["ProductCategory_ID"]) == 8))      //  8 - MixedETFs             Δεν επιθυμεί την επένδυση στα :  Μετοχικά και Μεικτά Διαπραγματεύσιμα Αμοιβαία Κεφάλαια 
                        bSpecRules = false;

                    if (sSpecRules.Substring(9, 1) == "1" && Convert.ToInt32(dtRow1["Product_ID"]) == 6)                     //  9 - Funds                 Δεν επιθυμεί την επένδυση στα : Αμοιβαία Κεφάλαια   6 - it's MuturalFund
                        if (Convert.ToInt32(dtRow1["AIFMD"]) == 2 && (Convert.ToInt32(dtRow1["MiFIDInstrumentType"]) == 0 || Convert.ToInt32(dtRow1["MiFIDInstrumentType"]) == 1))
                            bSpecRules = false;

                    if (sSpecRules.Substring(10, 1) == "1" && Convert.ToInt32(dtRow1["Product_ID"]) == 4)                    // 10 - ETFs                  Δεν επιθυμεί την επένδυση στα : Διαπραγματεύσιμα Αμοιβαία Κεφάλαια   4 - it's ETF
                        if (Convert.ToInt32(dtRow1["AIFMD"]) == 2 && (Convert.ToInt32(dtRow1["MiFIDInstrumentType"]) == 0 || Convert.ToInt32(dtRow1["MiFIDInstrumentType"]) == 1))
                            bSpecRules = false;


                    if (sSpecRules.Substring(11, 1) == "1")                                                 // 11 - Mono Investment Grade        Επιθυμεί Mono Investment Grade
                        if (Convert.ToInt32(dtRow1["RatingGroup"]) != 1)
                            bSpecRules = false;

                }
                if (!bSpecRules) {
                    iOK_Flag = 0;
                    sOK_String = sOK_String + "0";
                }
                else sOK_String = sOK_String + "1";

                //--- result -------------------------------------------------------------------------------------------
                if (iOK_Flag == 1 || !bAddOnlyOKRecords) {
                    dtRow = dtClientsProducts.NewRow();
                    dtRow["ID"] = dtRow1["ID"];
                    dtRow["CodeTitle"] = dtRow1["CodeTitle"];
                    dtRow["ISIN"] = dtRow1["ISIN"];
                    dtRow["Product_Title"] = dtRow1["Product_Title"];
                    dtRow["ProductCategory_Title"] = dtRow1["ProductCategory_Title"];
                    dtRow["HFCategory_Title"] = dtRow1["HFCategory_Title"];
                    dtRow["SecID"] = dtRow1["SecID"];
                    dtRow["Code"] = dtRow1["Code"];
                    dtRow["Code2"] = dtRow1["Code2"];
                    dtRow["Currency"] = dtRow1["Currency"];
                    dtRow["CreditRating"] = dtRow1["CreditRating"];
                    dtRow["MoodysRating"] = dtRow1["MoodysRating"];
                    dtRow["FitchsRating"] = dtRow1["FitchsRating"];
                    dtRow["SPRating"] = dtRow1["SPRating"];
                    dtRow["ICAPRating"] = dtRow1["ICAPRating"];
                    dtRow["CountryRisk_Title"] = dtRow1["CountryRisk_Title"];
                    dtRow["Date2"] = dtRow1["Date2"];
                    dtRow["Maturity"] = dtRow1["Maturity"];
                    dtRow["Maturity_Date"] = dtRow1["Maturity_Date"];
                    dtRow["CurrencyHedge"] = dtRow1["CurrencyHedge"];
                    dtRow["CurrencyHedge2"] = dtRow1["CurrencyHedge2"];
                    dtRow["SurveyedKIID"] = dtRow1["SurveyedKIID"];
                    dtRow["SurveyedKIID_Date"] = dtRow1["SurveyedKIID_Date"];
                    dtRow["StockExchange_ID"] = dtRow1["StockExchange_ID"];
                    dtRow["StockExchange_Code"] = dtRow1["StockExchange_Code"];
                    dtRow["Weight"] = dtRow1["Weight"];
                    dtRow["IR_URL"] = dtRow1["IR_URL"];
                    dtRow["Retail"] = dtRow1["Retail"];
                    dtRow["Professional"] = dtRow1["Professional"];
                    dtRow["ComplexProduct"] = dtRow1["ComplexProduct"];
                    dtRow["Distrib_ExecOnly"] = sDistrib[Convert.ToInt32(dtRow1["Distrib_ExecOnly"])];
                    dtRow["Distrib_Advice"] = sDistrib[Convert.ToInt32(dtRow1["Distrib_Advice"])];
                    dtRow["Distrib_PortfolioManagment"] = sDistrib[Convert.ToInt32(dtRow1["Distrib_PortfolioManagment"])];
                    dtRow["MIFID_Risk"] = dtRow1["MIFID_Risk"];
                    dtRow["ID"] = dtRow1["ID"];
                    dtRow["Shares_ID"] = dtRow1["Shares_ID"];
                    dtRow["ShareTitles_ID"] = dtRow1["ShareTitles_ID"];
                    dtRow["Product_ID"] = iProduct_ID;
                    dtRow["ProductCategory_ID"] = dtRow1["ProductCategory_ID"];
                    dtRow["OK_Flag"] = iOK_Flag;
                    dtRow["OK_String"] = sOK_String;
                    dtRow["Aktive"] = dtRow1["Aktive"];
                    dtClientsProducts.Rows.Add(dtRow);
                }
            }
        }
        public static void AddLogsRecord(int iAuthor_ID, DateTime dDateIns, int iSource_ID, string sMessage)
        {
            clsLogger Log = new clsLogger();
            Log.Author_ID = iAuthor_ID;
            Log.DateIns = dDateIns;
            Log.Source_ID = iSource_ID;
            Log.Message = sMessage;
            Log.InsertRecord();
        }
        public static void SaveHistory(int iRecType, int iSrcRec_ID, int iClient_ID, int iContract_ID, int iAktion, string sValue,
                                       int iDocFiles_ID, string sNotes, DateTime dIns, int iUserID)
        {
            //--- Add History Record ---
            clsHistory klsHistory = new clsHistory();
            klsHistory.RecType = iRecType;
            klsHistory.SrcRec_ID = iSrcRec_ID;
            klsHistory.Client_ID = iClient_ID;
            klsHistory.Contract_ID = iContract_ID;
            klsHistory.Action = iAktion;
            klsHistory.CurrentValues = sValue;
            klsHistory.DocFiles_ID = iDocFiles_ID;
            klsHistory.Notes = sNotes;
            klsHistory.User_ID = iUserID;
            klsHistory.DateIns = dIns;
            klsHistory.InsertRecord();
        }
        public static string CheckCompatibility(int iContract_ID, int iMIFID_2, int iMIFIDCategory_ID, int iXAA, int iShare_ID, int iSE_ID)
        {
            string sMessage = "";
            DataRow[] foundRows;

            if (iMIFID_2 == 1)
            {
                if (iContract_ID != 0)
                    if (iMIFIDCategory_ID == 0)
                    {
                        foundRows = Global.dtContracts.Select("Contract_ID = " + iContract_ID);
                        iMIFIDCategory_ID = Convert.ToInt32(foundRows[0]["MIFIDCategory_ID"]);
                    }

                foundRows = Global.dtProducts.Select("ID = " + iShare_ID);
                if (Convert.ToInt32(foundRows[0]["HFIC_Recom"]) == 1)
                {
                    if ((iMIFIDCategory_ID == 1 && Convert.ToInt32(foundRows[0]["InvestType_Retail"]) == 2) ||
                        (iMIFIDCategory_ID == 2 && Convert.ToInt32(foundRows[0]["InvestType_Prof"]) == 2))                 //  = 2  is = Yes
                        sMessage = "";
                    else sMessage = "Problem Retail - Profi";
                }
                else sMessage = "Not Recommended Product";
            }

            if (iXAA != 1)
                if (iSE_ID == 8) sMessage = sMessage + "\n Problem with NEO SXHMA";                                                    // 8 - XATH


            return sMessage;
        }
        public static string ShowPrices(int iPriceType, float sgPrice)
        {
            string sPrice = "";

            switch (iPriceType) {
                case 0:
                    sPrice = sgPrice.ToString("0.00##");
                    break;
                case 1:
                    sPrice = "M";
                    break;
                case 2:
                    sPrice = sgPrice.ToString("0.00##");
                    break;
                case 3:
                    sPrice = sgPrice.ToString("0.00##");
                    break;
                case 4:
                    sPrice = "ATC";
                    break;
                case 5:
                    sPrice = "ATO";
                    break;
            }
            return sPrice;
        }
        public static bool DeleteTableRecord(string sTable, int iID)
        {
            bool bResult = false;
            if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                clsSystem System = new clsSystem();
                System.Table = sTable;
                System.Record_ID = iID;
                System.DeleteRecord();

                bResult = true;
            }
            return bResult;
        }
        public static void CreateClientFolders(string sClientName)
        {
            sClientName = sClientName.Replace(".", "_");
            DMS_CreateDirectory("Customers/" + sClientName);
            DMS_CreateDirectory("Customers/" + sClientName + "/AdvisoryPortofolioMonitoring");
            DMS_CreateDirectory("Customers/" + sClientName + "/Compliance");
            DMS_CreateDirectory("Customers/" + sClientName + "/CooperationProposals");
            DMS_CreateDirectory("Customers/" + sClientName + "/Informing");
            DMS_CreateDirectory("Customers/" + sClientName + "/InvestProposals");
            DMS_CreateDirectory("Customers/" + sClientName + "/Invoices");
            DMS_CreateDirectory("Customers/" + sClientName + "/OrdersAcception");
            DMS_CreateDirectory("Customers/" + sClientName + "/Movements");
        }
        //--- DMS functions -------------------------------------------------------------
        public static bool DMS_CheckDirectoryExists(string sDir)
        {
            bool bResult = true;

            sDir = sDir.Replace(".", "_") + "/";

            switch (Global.DMSAccess)
            {
                case 1:                                                              // ' iMethod = 1 - Windows, 2 - Web
                    if (!System.IO.Directory.Exists(Global.DMSMapDrive + "/" + sDir)) 
                        bResult = false;
                    break;

                case 2:

                    string sNewDir = "ftp://10.0.0.54:2121" + "/" + sDir.Replace(".", "_");

                    FtpWebRequest reqFTP = null;
                    Stream ftpStream = null;
                    try
                    {
                        reqFTP = (FtpWebRequest)FtpWebRequest.Create(sNewDir);
                        reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                        reqFTP.UseBinary = true;
                        reqFTP.Credentials = new NetworkCredential(Global.FTP_Username, Global.FTP_Password);
                        FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                        ftpStream = response.GetResponseStream();
                        ftpStream.Close();
                        response.Close();
                    }
                    catch
                    {
                        if (ftpStream != null)
                        {
                            ftpStream.Close();
                            ftpStream.Dispose();
                        }
                        bResult = false;
                    }
                    break;
            }
            return bResult;
        }
        public static bool DMS_CreateDirectory(string sDir)
        {
            bool bResult = true;
            string sNewDir = "";

            switch (Global.DMSAccess)
            {
                case 1:
                    sNewDir = Global.DMSMapDrive + "/" + sDir.Replace(".", "_");
                    sNewDir = sNewDir.Replace("\\", "/");

                    clsServerJobs ServerJob = new clsServerJobs();
                    ServerJob.JobType_ID = 11;                                          // 11 - Create Folder in ISPServer
                    ServerJob.Source_ID = 0;
                    ServerJob.Parameters = "{'folder_name': '" + sNewDir.Trim() + "'}";
                    ServerJob.DateStart = DateTime.Now;
                    ServerJob.DateFinish = Convert.ToDateTime("1900/01/01");
                    ServerJob.PubKey = "";
                    ServerJob.PrvKey = "";
                    ServerJob.Attempt = 0;
                    ServerJob.Status = 0;
                    ServerJob.InsertRecord();
                    break;
                case 22:
                    sNewDir = "C:/DMS/" + sDir.Replace(".", "_");

                    clsServerJobs ServerJob1 = new clsServerJobs();
                    ServerJob1.JobType_ID = 11;                                          // 11 - Create Folder in ISPServer
                    ServerJob1.Source_ID = 0;
                    ServerJob1.Parameters = "{'folder_name': '" + sNewDir + "'}";
                    ServerJob1.DateStart = DateTime.Now;
                    ServerJob1.DateFinish = Convert.ToDateTime("1900/01/01");
                    ServerJob1.PubKey = "";
                    ServerJob1.PrvKey = "";
                    ServerJob1.Attempt = 0;
                    ServerJob1.Status = 0;
                    ServerJob1.InsertRecord();
                    break;
                case 222:
                    DisconnectDrive(Global.DMSMapDrive);
                    //MessageBox.Show(Global.DMSMapDrive + "   " + Global.DMSMapDriveAddress + "   " + Global.FTP_Username + "   " + Global.FTP_Password);
                    MapDrive(Global.DMSMapDrive, Global.DMSMapDriveAddress, Global.FTP_Username, Global.FTP_Password);

                    sNewDir = Global.DMSMapDrive + "/" + sDir.Replace(".", "_");
                    sNewDir = sNewDir.Replace("/", "\\");
                    //MessageBox.Show(sNewDir);
                    if (!System.IO.Directory.Exists(sNewDir))
                        try
                        {
                            System.IO.Directory.CreateDirectory(sNewDir);
                            bResult = true;
                        }
                        catch { bResult = false; }

                    DisconnectDrive(Global.DMSMapDrive);
                    break;
                case 2:
                    sNewDir = "ftp://10.0.0.54:2121" + "/" + sDir.Replace(".", "_");

                    FtpWebRequest reqFTP = null;
                    Stream ftpStream = null;
                    try
                    {
                        reqFTP = (FtpWebRequest)FtpWebRequest.Create(sNewDir);
                        reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                        reqFTP.UseBinary = true;
                        reqFTP.Credentials = new NetworkCredential(Global.FTP_Username, Global.FTP_Password);
                        FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                        ftpStream = response.GetResponseStream();
                        ftpStream.Close();
                        response.Close();
                    }
                    catch {
                        if (ftpStream != null) {
                            ftpStream.Close();
                            ftpStream.Dispose();
                        }
                        bResult = false;
                    }
                    break;

                case 4:
                    sNewDir = Global.DMSMapDrive + "/" + sDir.Replace(".", "_");
                    //sNewDir = sNewDir.Replace("/", "\");
                    //sTemp = InputBox("Target File", "Wind", sNewDir);
                    if (!System.IO.Directory.Exists(sNewDir))
                        try
                        {
                            System.IO.Directory.CreateDirectory(sNewDir);
                            bResult = true;
                        }
                        catch { bResult = false; }
                    break;
            }
            return bResult;
        }
        public static void DMS_RenameFolderName(string currentFolderName, string newFolderName)
        {
            switch (Global.DMSAccess)
            {
                case 1:
                    clsServerJobs ServerJob = new clsServerJobs();
                    ServerJob.JobType_ID = 12;
                    ServerJob.Source_ID = 0;
                    ServerJob.Parameters = "{ 'folder1': 'C:/DMS/Customers/" + currentFolderName + "', 'folder2': 'C:/DMS/Customers/" + newFolderName + "'}";
                    ServerJob.DateStart = DateTime.Now;
                    ServerJob.DateFinish = Convert.ToDateTime("1900/01/01");
                    ServerJob.PubKey = "";
                    ServerJob.PrvKey = "";
                    ServerJob.Attempt = 0;
                    ServerJob.Status = 0;
                    ServerJob.InsertRecord();
                    break;
                case 2:
                    FtpWebRequest reqFTP = null;
                    Stream ftpStream = null;
                    try {
                        reqFTP = (FtpWebRequest)FtpWebRequest.Create(Global.DocFilesPath_FTP + "/" + currentFolderName);
                        reqFTP.Method = WebRequestMethods.Ftp.Rename;
                        reqFTP.UseBinary = true;
                        reqFTP.Credentials = new NetworkCredential(Global.FTP_Username, Global.FTP_Password);
                        reqFTP.RenameTo = Global.DocFilesPath_FTP + "/" + newFolderName;
                        FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                        ftpStream = response.GetResponseStream();
                        ftpStream.Close();
                        response.Close();
                    }
                    catch {
                        if (ftpStream != null) {
                            ftpStream.Close();
                            ftpStream.Dispose();
                        }
                    }
                    break;
                case 3:
                    DisconnectDrive(Global.DMSMapDrive);
                    MapDrive(Global.DMSMapDrive, Global.DMSMapDriveAddress, Global.FTP_Username, Global.FTP_Password);

                    if (!System.IO.Directory.Exists(Global.DMSMapDrive + "/" + newFolderName))
                        System.IO.Directory.Move(Global.DMSMapDrive + "/" + currentFolderName, Global.DMSMapDrive + "/" + newFolderName);

                    DisconnectDrive(Global.DMSMapDrive);
                    break;
                case 4:
                    if (!System.IO.Directory.Exists(Global.DMSMapDrive + "/" + newFolderName))
                        System.IO.Directory.Move(Global.DMSMapDrive + "/" + currentFolderName, Global.DMSMapDrive + "/" + newFolderName);
                    break;
            }
        }
        public static void DMS_ShowFile(string sFilePath, string sFileName)
        {
            string sTemp = "";
            switch (Global.DMSAccess) {
                case 1:
                    if (sFilePath.Length > 0 && sFileName.Length > 0)
                    {
                        sTemp = Global.DocFilesPath_HTTP + "//" + sFilePath.Replace(".", "_") + "//" + sFileName;
                        sTemp = sTemp.Replace("\\", "//");
                        // sTemp = InputBox("Enter", "Wind", sTemp)
                        Process.Start(sTemp);
                    }
                    else
                    {
                        sTemp = sFileName;
                        sTemp = sTemp.Replace("\\", "//");
                        Process.Start(sTemp);
                    }
                    break;
                case 2:
                    if (sFilePath.Length > 0)
                    {
                        sTemp = Global.DocFilesPath_HTTP + "//" + sFilePath.Replace(".", "_") + "//" + sFileName;
                        sTemp = sTemp.Replace("\\", "/");
                        // sTemp = InputBox("Enter", "Wind", sTemp)
                        Process.Start(sTemp);
                    }
                    else
                    {
                        sTemp = sFileName;
                        sTemp = sTemp.Replace("\\", "//");
                        Process.Start(sTemp);
                    }
                    break;
                case 3:
                    break;
                case 4:
                    if (sFilePath.Length > 0)
                    {
                        sTemp = Global.DMSMapDrive + "//" + sFilePath.Replace(".", "_") + "//" + sFileName;
                        sTemp = sTemp.Replace("\\", "//");
                        // sTemp = InputBox("Enter", "Wind", sTemp)
                        Process.Start(sTemp);
                    }
                    break;
            }
        }
        public static string DMS_UploadFile(string sSourceFileFullPath, string sTargetFolder, string sNewFileName)
        {
            switch (Global.DMSAccess)
            {
                case 1:  // iMethod = 1 - Mapping, 2 - Web, 3 - ServerJob, 4 - Windows
                    sTargetFolder = sTargetFolder.Replace(".", "_").Trim();

                    if (Global.DMSTransferPoint.Length == 0)                                                     // DMS TransferPoint is Empty
                        sNewFileName = Global.DMS_UploadFile(sSourceFileFullPath, sTargetFolder, sNewFileName);
                    else
                    {
                        sNewFileName = Path.GetFileNameWithoutExtension(sNewFileName) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sNewFileName);
                        //if (!Path.GetDirectoryName(sSourceFileFullPath).Contains(Global.DMSTransferPoint))
                        if (Path.GetDirectoryName(sSourceFileFullPath) != Global.DMSTransferPoint)
                        {              // Source file isn't DMS TransferPoint folder, so ...
                            if (File.Exists(Global.DMSTransferPoint + "\\" + sNewFileName))
                                sNewFileName = Path.GetFileNameWithoutExtension(sNewFileName) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sNewFileName);
                            File.Copy(sSourceFileFullPath, Global.DMSTransferPoint + "\\" + sNewFileName);         // ... copy this file into DMS TransferPoint folder
                        }

                        clsServerJobs ServerJobs = new clsServerJobs();
                        ServerJobs.JobType_ID = 15;
                        ServerJobs.Source_ID = 0;
                        ServerJobs.Parameters = "{'file_name': '" + sNewFileName.Replace("\\", "/") + "', 'target_folder':'" + sTargetFolder.Replace("\\", "/") + "/'}";
                        ServerJobs.DateStart = DateTime.Now;
                        ServerJobs.DateFinish = Convert.ToDateTime("1900/01/01");
                        ServerJobs.PubKey = "";
                        ServerJobs.PrvKey = "";
                        ServerJobs.Attempt = 0;
                        ServerJobs.Status = 0;
                        ServerJobs.InsertRecord();

                        sNewFileName = "Q:/" + sTargetFolder + "/" + sNewFileName;
                    }
                    break;
                case 2:
                    int iAttemps = 0;
                    string sTargetFullFileName;
                    sTargetFullFileName = "";
                    clsLogger Logger = new clsLogger();

                    sTargetFullFileName = "ftp://10.0.0.54:2121" + " / " + sTargetFolder + "/" + sNewFileName;
                    sTargetFullFileName = sTargetFullFileName.Replace("\\", "//");
                    if (DMS_CheckFileExists(sTargetFolder, sNewFileName))
                    {
                        sNewFileName = Path.GetFileNameWithoutExtension(sNewFileName) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sNewFileName);
                        sTargetFullFileName = Global.DocFilesPath_FTP + "/" + sTargetFolder + "/" + sNewFileName;
                    }

                    sSourceFileFullPath = (sSourceFileFullPath + "").Trim();
                    sTargetFullFileName = "ftp://10.0.0.54:2121" + "/" + sTargetFullFileName.Trim();
                    // InputBox("Enter", "Wind", sSourceFileFullPath)
                    // InputBox("Enter", "Wind", sTargetFullFileName)
                    // MsgBox("WEB   Source File =" & sSourceFileFullPath & vbCrLf & vbCrLf & "Target File = " & sTargetFullFileName)
                    while (true)
                    {
                        System.Net.FtpWebRequest miRequest = (System.Net.FtpWebRequest)System.Net.WebRequest.Create(sTargetFullFileName);
                        miRequest.Credentials = new System.Net.NetworkCredential(Global.FTP_Username, Global.FTP_Password);
                        miRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
                        try
                        {
                            var bFile = File.ReadAllBytes(sSourceFileFullPath);
                            var miStream = miRequest.GetRequestStream();
                            miStream.Write(bFile, 0, bFile.Length);
                            miStream.Close();
                            miStream.Dispose();
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

                        if (DMS_CheckFileExists(sTargetFolder, sNewFileName)) break;
                        else
                        {
                            Global.AddLogsRecord(Global.User_ID, DateTime.Now, 2, "Source File = " + sSourceFileFullPath + "   Target File = " + sTargetFullFileName);

                            iAttemps = iAttemps + 1;
                            if (iAttemps == 3)
                            {
                                sTargetFullFileName = "";
                                break;
                            }
                        }
                    }
                    break;
            }

            return sNewFileName;
        }
        public static string DMS1_UploadFile(string sSourceFullFileName, string sTargetPath, string sNewFileName)
        {
            int iAttemps;
            string sTargetFullFileName, sStartFileName, sTemp;
            sTemp = "";
            sTargetFullFileName = "";
            if (sSourceFullFileName.Length > 0)
            {
                sStartFileName = Path.GetFileName(sSourceFullFileName);
                iAttemps = 0;
                sTargetPath = sTargetPath.Replace(".", "_");
                clsLogger Logger = new clsLogger();

                switch (Global.DMSAccess) {
                    case 1:  // iMethod = 1 - Mapping, 2 - Web, 3 - ServerJob, 4 - Windows
                        Global.DisconnectDrive(Global.DMSMapDrive);
                        Global.MapDrive(Global.DMSMapDrive, Global.DMSMapDriveAddress, Global.FTP_Username, Global.FTP_Password);
                        sTargetFullFileName = Global.DMSMapDrive + @"\" + sTargetPath + @"\" + sNewFileName;
                        if (File.Exists(sTargetFullFileName))
                        {
                            sNewFileName = Path.GetFileNameWithoutExtension(sStartFileName) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sStartFileName);
                            sTargetFullFileName = Global.DMSMapDrive + "\\" + sTargetPath + "\\" + sNewFileName;
                        }
                        sTargetFullFileName = sTargetFullFileName.Replace("\\", "/");

                        MessageBox.Show("SOURCE=" + sSourceFullFileName + "\t" + "TARGET=" + sTargetFullFileName, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        while (true)
                        {
                            try
                            {
                                //MessageBox.Show("SOURCE="+sSourceFullFileName + "\t" + "TARGET="+sTargetFullFileName, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                                
                                File.Copy(sSourceFullFileName, sTargetFullFileName);
                                sTemp = "OK";
                            }
                            catch (Exception ex) { sTemp = ex.Message; }


                            if (File.Exists(sTargetFullFileName))
                            {
                                sTemp = sTemp + " ### File Was Copied";
                                break;
                            }
                            else
                            {
                                iAttemps = iAttemps + 1;
                                if (iAttemps > 3)
                                {
                                    sTargetFullFileName = "";
                                    break;
                                }
                            }
                        }

                        Global.DisconnectDrive(Global.DMSMapDrive);

                        Global.AddLogsRecord(Global.User_ID, DateTime.Now, 2, "Source File = " + sSourceFullFileName + "   Target File = " + sTargetFullFileName + " --- Result -> " + sTemp);

                        break;
                    case 2:
                        sTargetFullFileName = Global.DocFilesPath_FTP + "/" + sTargetPath + "/" + sNewFileName;
                        sTargetFullFileName = sTargetFullFileName.Replace("\\", "//");
                        if (DMS_CheckFileExists(sTargetPath, sNewFileName))
                        {
                            sNewFileName = Path.GetFileNameWithoutExtension(sNewFileName) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sNewFileName);
                            sTargetFullFileName = Global.DocFilesPath_FTP + "/" + sTargetPath + "/" + sNewFileName;
                        }

                        sSourceFullFileName = sSourceFullFileName.Trim();
                        sTargetFullFileName = sTargetFullFileName.Trim();
                        // InputBox("Enter", "Wind", sSourceFullFileName)
                        // InputBox("Enter", "Wind", sTargetFullFileName)
                        // MsgBox("WEB   Source File =" & sSourceFullFileName & vbCrLf & vbCrLf & "Target File = " & sTargetFullFileName)
                        while (true)
                        {
                            System.Net.FtpWebRequest miRequest = (System.Net.FtpWebRequest)System.Net.WebRequest.Create(sTargetFullFileName);
                            miRequest.Credentials = new System.Net.NetworkCredential(Global.FTP_Username, Global.FTP_Password);
                            miRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
                            try
                            {
                                var bFile = File.ReadAllBytes(sSourceFullFileName);
                                var miStream = miRequest.GetRequestStream();
                                miStream.Write(bFile, 0, bFile.Length);
                                miStream.Close();
                                miStream.Dispose();
                            }
                            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

                            if (DMS_CheckFileExists(sTargetPath, sNewFileName)) break;
                            else
                            {
                                Global.AddLogsRecord(Global.User_ID, DateTime.Now, 2, "Source File = " + sSourceFullFileName + "   Target File = " + sTargetFullFileName);

                                iAttemps = iAttemps + 1;
                                if (iAttemps == 3)
                                {
                                    sTargetFullFileName = "";
                                    break;
                                }
                            }
                        }
                        break;
                    case 3:
                        sSourceFullFileName = sSourceFullFileName.Replace("/", "\\");
                        //sTargetFullFileName = Global.DocFilesPath_Win + @"\" + sTargetPath + @"\" + sStartFileName;
                        sTargetFullFileName = sTargetFullFileName.Replace("/", "\\");
                        //conn.Open();
                        sTemp = "INSERT INTO ServerJobs (JobType_ID, Source_ID, Parameters, DateStart, DateFinish, Status) VALUES (5, 0, '" + sSourceFullFileName + "~" + sTargetFullFileName + "', '1900/01/01', '1900/01/01', 0)";
                        {
                            //var withBlock2 = cmd;
                            //withBlock2.CommandType = CommandType.Text;
                            //withBlock2.Connection = conn;
                            //withBlock2.CommandText = sTemp;
                        }

                        //cmd.ExecuteNonQuery();
                        //conn.Close();
                        break;
                    case 4:
                        sTargetFullFileName = Global.DMSMapDrive + @"\" + sTargetPath + @"\" + sNewFileName;
                        if (File.Exists(sTargetFullFileName))
                        {
                            sNewFileName = Path.GetFileNameWithoutExtension(sStartFileName) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sStartFileName);
                            sTargetFullFileName = Global.DMSMapDrive + "\\" + sTargetPath + "\\" + sNewFileName;
                        }
                        sTargetFullFileName = sTargetFullFileName.Replace("\\", "/");

                        while (true)
                        {
                            try
                            {
                                File.Copy(sSourceFullFileName, sTargetFullFileName);
                                sTemp = "OK";
                            }
                            catch (Exception ex) { sTemp = ex.Message; }

                            if (File.Exists(sTargetFullFileName))
                            {
                                sTemp = sTemp + " ### File Was Copied";
                                break;
                            }
                            else
                            {
                                iAttemps = iAttemps + 1;
                                if (iAttemps > 3)
                                {
                                    sTargetFullFileName = "";
                                    break;
                                }
                            }
                        }
                        break;
                }
                Global.AddLogsRecord(Global.User_ID, DateTime.Now, 2, "Source File = " + sSourceFullFileName + "   Target File = " + sTargetFullFileName + " --- Result -> " + sTemp);
            }

            return sTargetFullFileName;
        }
        public static void DMS_DownloadFile(string sSource, string sTarget)
        {
        }
        public static void DMS_PrintFile(string sFilePath, string sFileName)
        {
            string sTemp = "";
            //sFilePath = "";
            //sFileName = "";
            Process myProcess = new Process();

            switch (Global.DMSAccess)
            {
                case 1:
                    sTemp = Global.DocFilesPath_HTTP + "/" + sFilePath.Replace(".", "_") + "/" + sFileName;
                    myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    myProcess.StartInfo.FileName = sTemp;
                    myProcess.StartInfo.Verb = "";
                    myProcess.StartInfo.UseShellExecute = true;
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.Start();
                    myProcess.WaitForInputIdle();

                    if (myProcess.Responding) myProcess.CloseMainWindow();
                    else myProcess.Kill();
                    break;
                case 2:  // iMethod = 1 - Mapping, 2 - Web, 3 - ServerJob, 4 - Windows
                    Global.DisconnectDrive(Global.DMSMapDrive);
                    Global.MapDrive(Global.DMSMapDrive, Global.DMSMapDriveAddress, Global.FTP_Username, Global.FTP_Password);
                    sTemp = Global.DMSMapDrive + "/" + sFilePath.Replace(".", "_") + "/" + sFileName;

                    myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    myProcess.StartInfo.FileName = sTemp;
                    myProcess.StartInfo.Verb = "Print";
                    myProcess.StartInfo.UseShellExecute = true;
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.Start();
                    myProcess.WaitForInputIdle();

                    if (myProcess.Responding) myProcess.CloseMainWindow();
                    else myProcess.Kill();

                    Global.DisconnectDrive(Global.DMSMapDrive);
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
        }
        //----------------------------------------------------------------------------------------
        public static int AddInformingRecord(int iCommand_Type, int iCommand_ID, int iInform_Method, int iSource_ID, int iClient_ID,
                                              int iContract_ID, string sClient_Data, string sCC, string sSubject, string sBody,
                                              string sFileName, string sAttachedFiles, string sDateSent, int iStatus, int iSentAttempt, string sSentMessage)
        {
            int iRec_ID = 0, i = 0;

            if (sAttachedFiles.Length > 0)
            {
                string[] tokens = sAttachedFiles.Split('~');
                i = tokens.Length - 1;
                if (i < 0) i = 0;
            }

            clsInformings klsInforming = new clsInformings();
            klsInforming.Command_Type = iCommand_Type;          // 0-not Command,1-Securities,2-FX,3-LL 
            klsInforming.Command_ID = iCommand_ID;
            klsInforming.InformMethod = iInform_Method;         // 1-Τηλέφωνο,4-SMS,5-e-mail,6-fax,7-Personal,8-Post,9-EAMNet
            klsInforming.Source_ID = iSource_ID;                // 1-TransactionCheck,2-DailyInform,3-ManFeesInform,4-Others Inform,5-RTOInform,6-InvoiceRTO,7-AdminFees,8-PeriodicalEvaluation,9-ExPostCost,10-CustodyFees
            klsInforming.Client_ID = iClient_ID;
            klsInforming.Contract_ID = iContract_ID;
            klsInforming.ClientData = sClient_Data;             // client's(recipient) e - mail address or mobile number  or post address
            klsInforming.CC = sCC;                              // only for e-mail
            klsInforming.Subject = sSubject + "";               // SMS subject
            klsInforming.Body = sBody;                          // text
            klsInforming.FileName = sFileName;                  // Main FileName
            klsInforming.AttachedFiles = sAttachedFiles;        // attachments
            klsInforming.AttachedFilesCount = i;                // attachments count
            klsInforming.DateIns = DateTime.Now;
            klsInforming.DateSent = sDateSent;
            klsInforming.Status = iStatus;
            klsInforming.SentAttempts = iSentAttempt;
            klsInforming.SentMessage = sSentMessage;
            klsInforming.User_ID = Global.User_ID;
            iRec_ID = klsInforming.InsertRecord();

            return iRec_ID;

        }
        public static bool DMS_CheckFileExists(string sSourceFullFileName, string sTargetPath)
        {
            return true;
        }
        public static decimal ConvertAmount(decimal decConvertAmount, string sConvertCurr, string sCurr, DateTime dAktion)
        {
            decimal decRate, decRate1, decAmount;
            clsProductsCodes klsProductsCode = new clsProductsCodes();

            decRate = 0;
            decRate1 = 0;
            decAmount = 0;
            if (sConvertCurr == sCurr) decAmount = decConvertAmount;
            else {

                if (sCurr == "EUR")
                {
                    klsProductsCode.DateIns = dAktion;
                    klsProductsCode.Code = "EUR" + sConvertCurr + "=";
                    klsProductsCode.GetPrice_Code();
                    decRate = Convert.ToDecimal(klsProductsCode.LastClosePrice);
                    if (decRate != 0) decAmount = Math.Round(decConvertAmount / decRate, 2);
                }
                else {
                    if (sConvertCurr == "EUR")
                    {
                        klsProductsCode.DateIns = dAktion;
                        klsProductsCode.Code = "EUR" + sCurr + "=";
                        klsProductsCode.GetPrice_Code();
                        decRate = Convert.ToDecimal(klsProductsCode.LastClosePrice);
                        if (decRate != 0) decAmount = Math.Round(decConvertAmount / decRate, 2);
                    }
                    else {
                        klsProductsCode.DateIns = dAktion;
                        klsProductsCode.Code = "EUR" + sConvertCurr + "=";
                        klsProductsCode.GetPrice_Code();
                        decRate = Convert.ToDecimal(klsProductsCode.LastClosePrice);
                        if (decRate != 0) decAmount = Math.Round(decConvertAmount / decRate, 2);

                        klsProductsCode.DateIns = dAktion;
                        klsProductsCode.Code = "EUR" + sCurr + "=";
                        klsProductsCode.GetPrice_Code();
                        decRate1 = Convert.ToDecimal(klsProductsCode.LastClosePrice);
                        if (decRate1 != 0) decAmount = Math.Round(decAmount * decRate1, 2);
                    }
                }
            }
            return decAmount;
        }
        public static string FileChoice(string sFolderPath)
        {
            string sFilePath;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            sFilePath = "";
            openFileDialog1.InitialDirectory = sFolderPath;
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) sFilePath = openFileDialog1.FileName;

            return sFilePath;
        }
        public static string CreateGAPCode(int iLog_ID, int iType_ID, string sCode, string sPortfolio, string sISIN, string sCurrency, 
                                           int iOwner, string sDeposit_Code, string sProvider_Code, int iStatus)
        {
            string sGAPCode = "";

            if (iLog_ID == 1)
            {
                if (iType_ID == 1)
                {
                    if (sCode != "" && sPortfolio != "" && sProvider_Code != "")
                        sGAPCode = iLog_ID.ToString() + "." + (iType_ID == 1 ? "X" : "T") + "." + sCode + "." + sPortfolio + "." + sCurrency + "." + sProvider_Code + "." + iStatus.ToString();
                }
                else
                {
                    if (sCode != "" && sPortfolio != "" && sISIN != "" && sDeposit_Code != "")
                        sGAPCode = iLog_ID.ToString() + "." + (iType_ID == 1 ? "X" : "T") + "." + sCode + "." + sPortfolio + "." + sISIN + "." + sCurrency + "." + sDeposit_Code + "." + 
                                   iStatus.ToString();
                }
            }
            else
            {
                if (iType_ID == 1)
                {
                    if (iOwner != 0 && sProvider_Code != "")
                        sGAPCode = iLog_ID.ToString() + "." + (iType_ID == 1 ? "X" : "T") + "." + (iOwner == 1 ? "CLIENTS" : "OWN") + "." + sCurrency + "." + sProvider_Code + "." + iStatus.ToString();
                }
                else
                {
                    if (iOwner != 0 && sISIN != "" && sDeposit_Code != "" && sProvider_Code != "")
                        sGAPCode = iLog_ID.ToString() + "." + (iType_ID == 1 ? "X" : "T") + "." + (iOwner == 1 ? "CLIENTS" : "OWN") + "." + sISIN + "." + sCurrency + "." + sDeposit_Code + "." +
                                   sProvider_Code + "." + iStatus.ToString();
                }
            }
            return sGAPCode;
        }
        public static void MapDrive1(string DriveLetter, string UNCPath, string strUsername, string strPassword)
        {
            try
            {
                var p = new Process();
                p.StartInfo.FileName = "net.exe";
                p.StartInfo.Arguments = " use " + DriveLetter + " " + UNCPath + " " + strPassword + " /USER:hellasfin" + ((char)92) + strUsername;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.Start();
                p.WaitForExit();
            }
            catch (Exception z)
            {
                MessageBox.Show(z.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public static void MapDrive(string DriveLetter, string UNCPath, string strUsername, string strPassword)
        {
            try
            {
                // Map Network drive
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();

                // Notes:
                //      Use /C To carry out the command specified by string and then terminates
                //      You can omit the passord or username and password
                //      Use /PERSISTENT:YES to keep the mapping when the machine is restarted

                StringBuilder sb = new StringBuilder(@"/C net use ");
                sb.Append(" " + DriveLetter);
                sb.Append(@" " + UNCPath);
                sb.Append(@" /USER:hellasfin\" + strUsername + " " + strPassword);
                sb.Append("  /PERSISTENT:YES");

                psi.FileName = "cmd.exe";
                psi.Arguments = sb.ToString();      // @"/C net use Q: \\hf-hq-trader\DMS  /USER:hellasfin\traderfull Trad_QWE_123! /PERSISTENT:YES";
                psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                //psi.UseShellExecute = false;
                //psi.CreateNoWindow = true;
                process.StartInfo = psi;

                process.Start();
                System.Threading.Thread.Sleep(3000);
            }
            catch (Exception z)
            {
                MessageBox.Show(z.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public static void DisconnectDrive(string DriveLetter)
        {
            var p = new Process();
            p.StartInfo.FileName = "net.exe";
            p.StartInfo.Arguments = " use " + DriveLetter + " /delete  /yes ";
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();
        }
        public static string GenerateCode()
        {
            int i, j, k;
            string s1 = "", s2 = "", s3 = "", s4 = "", s5 = "", s6 = "", sTemp = "";
            Random random = new Random();

            i = DateTime.Now.Second * DateTime.Now.Month + Convert.ToInt32(random.Next(0, 1) * 100);
            j = DateTime.Now.Minute + Convert.ToInt32(random.Next(0, 1) * 100);
            k = (i + j) / 2;

            while (s1 == "") {
                if (i >= 65 && i <= 90) s1 = Convert.ToChar(i).ToString();
                else {
                    if (i < 65) i = i + 10;
                    else i = i - 10;
                }
            }

            while (s2 == "")
            {
                if (i >= 65 && i <= 90) s2 = Convert.ToChar(i).ToString();
                else
                {
                    if (i < 65) i = i + 10;
                    else i = i - 10;
                }
            }

            while (s3 == "")
            {
                if (i >= 65 && i <= 90) s3 = Convert.ToChar(i).ToString();
                else
                {
                    if (i < 65) i = i + 10;
                    else i = i - 10;
                }
            }


            sTemp = i + "";
            s4 = sTemp.Substring(sTemp.Length - 1, 1);
            sTemp = j + "";
            s5 = sTemp.Substring(sTemp.Length - 1, 1);
            sTemp = k + "";
            s6 = sTemp.Substring(sTemp.Length - 1, 1);
            if (Convert.ToInt32(s4) < 5) sTemp = s1 + s4 + s2 + s5 + s3 + s6;
            else sTemp = s4 + s1 + s5 + s2 + s6 + s3;

            return sTemp;

        }
        public static void TranslateUserName(string sWord, out string sWordGreek, out string sWordEnglish)
        {
            string sEnglish, sGreek, sChar;
            int i, j, k;
            sEnglish = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrsstuvwxyz- ./\_0123456789";
            sGreek = @"ΑΒΣΔΕΦΓΗΙΞΚΛΜΝΟΠ:ΡΣΤΘΩ΅ΧΥΖαβσδεφγηιξκλμνοπ;ρςστθωςχυζ- ./\_0123456789";
            sChar = "";
            sWordGreek = "";
            sWordEnglish = "";

            i = sWord.Length;
            for (j = 0; j < i; j++)
            {
                sChar = sWord.Substring(j, 1);
                k = sGreek.IndexOf(sChar);
                if (k >= 0)
                {
                    sWordGreek = sWordGreek + sChar;
                    sWordEnglish = sWordEnglish + sEnglish.Substring(k, 1);
                }
                else
                {
                    k = sEnglish.IndexOf(sChar);
                    if (k >= 0)
                    {
                        sWordGreek = sWordGreek + sGreek.Substring(k, 1);
                        sWordEnglish = sWordEnglish + sChar;
                    }
                }
            }
        }
        public static int DefineRatingGroup(string sMoodysRating, string sFitchsRating, string sSPRating, string sICAPRating, string sMorningStarRating) {
            int i = 0;
            int iRating_Group = 0;
            DataRow[] foundRows;

            if (sMoodysRating != "" && sMoodysRating != "NULL" && sMoodysRating != "'NULL'") {
                foundRows = Global.dtRatingCodes.Select("RatingAgency_ID = 1 AND Code = '" + sMoodysRating + "'");
                if (foundRows.Length > 0) {
                    i = Convert.ToInt32(foundRows[0]["RatingGroup"]);
                    if (i <= 3 && i > iRating_Group) iRating_Group = i;
                }
            }

            if (sFitchsRating != "" && sFitchsRating != "NULL" && sFitchsRating != "'NULL'") {
                foundRows = Global.dtRatingCodes.Select("RatingAgency_ID = 2 AND Code = '" + sFitchsRating + "'");
                if (foundRows.Length > 0) {
                    i = Convert.ToInt32(foundRows[0]["RatingGroup"]);
                    if (i <= 3 && i > iRating_Group) iRating_Group = i;
                }
            }

            if (sSPRating != "" && sSPRating != "NULL" && sSPRating != "'NULL'") {
                foundRows = Global.dtRatingCodes.Select("RatingAgency_ID = 3 AND Code = '" + sSPRating + "'");
                if (foundRows.Length > 0) {
                    i = Convert.ToInt32(foundRows[0]["RatingGroup"]);
                    if (i <= 3 && i > iRating_Group) iRating_Group = i;
                }
            }

            if (sICAPRating != "" && sICAPRating != "NULL" && sICAPRating != "'NULL'")
            {
                foundRows = Global.dtRatingCodes.Select("RatingAgency_ID = 4 AND Code = '" + sICAPRating + "'");
                if (foundRows.Length > 0)
                {
                    i = Convert.ToInt32(foundRows[0]["RatingGroup"]);
                    if (i <= 3 && i > iRating_Group) iRating_Group = i;
                }
            }

            if (sMorningStarRating != "" && sMorningStarRating != "NULL" && sMorningStarRating != "'NULL'") {
                foundRows = Global.dtRatingCodes.Select("RatingAgency_ID = 5 AND Code = '" + sMorningStarRating + "'");
                if (foundRows.Length > 0) {
                    i = Convert.ToInt32(foundRows[0]["RatingGroup"]);
                    if (i <= 3 && i > iRating_Group) iRating_Group = i;
                }
            }

            if (iRating_Group == 0) iRating_Group = 4;

            return iRating_Group;
        }
        
        public static bool SendMail_Web(string strSender, string strUsername, string strPassword, string strRecipient, string strCC, string strSubject,
                         string strBody, string sAttachFiles, string sSMTP, string sPrefix, int iSrc_ID, string sMailSource)
        {
            bool bResult = true;
            int i = 0, j = 0;
            string sTemp = "";
            string sMessage = "";

            try
            {
                //MessageBox.Show("Point 001  " + strUsername + "   " + strPassword);
                IEWSClient client = EWSClient.GetEWSClient("https://outlook.office365.com/ews/exchange.asmx", strUsername, strPassword, "office365.com");
                //MessageBox.Show("Point 002");
                client.Timeout = 1500000;
                //MessageBox.Show("Point 003");

                j = j + 1;
                //MessageBox.Show("Point 1");
                // Create instance of type MailMessage
                MailMessage msg = new MailMessage();

                //sAttachFiles = "";
                //MessageBox.Show("Start attach \n " + sAttachFiles);
                string[] tokens = sAttachFiles.Split('~');

                for (i = 0; i <= tokens.Length - 2; i++)
                {
                    sTemp = tokens[i] + "";
                    //MessageBox.Show(sTemp);
                    if (Path.GetFileName(sTemp) != "")
                    {
                        if (File.Exists(sTemp)) msg.AddAttachment(new Attachment(sTemp));
                        else sMessage = sMessage + "\n" + "Attach File Not Found " + tokens[i] + "\n";
                    }
                }

                j = j + 1;
                //MessageBox.Show("Point 2");
                // Send the message
                msg.From = strSender;
                msg.To = strRecipient;
                if (strCC.Length > 0) msg.CC = strCC;
                msg.Subject = strSubject;
                msg.HtmlBody = strBody;
                //MessageBox.Show("Point 3");
                j = j + 1;
                client.Send(msg);
                Thread.Sleep(2000);

                //MessageBox.Show("SENDER=" & strSender & vbCrLf & "RECIPIENT=" & strRecipient);

                //MessageBox.Show("Point 4");
                j = j + 1;
                bResult = true;
            }
            catch (Exception z)
            {
                sMessage = (sMessage + " " + z.Message).Trim();
                //MessageBox.Show(sMessages);
                j = 999;
                bResult = false;
                //MessageBox.Show("Point 5");
            }
            finally
            {
                //MessageBox.Show("j=" + j);
                switch (j)
                {
                    case 0:
                        sMessage = "Can't create EWSClient. Check username and password of e-mail account";
                        bResult = false;
                        break;
                    case 1:
                        sMessage = "Can't attach files";
                        bResult = false;
                        break;
                    case 2:
                        sMessage = "Message can't send";
                        bResult = false;
                        break;
                    case 3:
                        // sMessage = "";
                        bResult = true;
                        break;
                }
                //MessageBox.Show("Point 6");
            }

            return bResult;
        }
      
        public static string connStr { get { return _sConnString; } set { _sConnString = value; } }
        public static string connStr2 { get { return _sConnString2; } set { _sConnString2 = value; } }
        public static string connFIXStr { get { return _sConnFIXString; } set { _sConnFIXString = value; } }
        public static string Version { get { return _sVersion; } set { _sVersion = value; } }
        public static int Company_ID { get { return _iCompany_ID; } set { _iCompany_ID = value; } }
        public static int ClientsFilter_ID { get { return _iClientsFilter_ID; } set { _iClientsFilter_ID = value; } }
        public static string ClientsFilter { get { return _sClientsFilter; } set { _sClientsFilter = value; } }
        public static string CompanyName { get { return _sCompanyName; } set { _sCompanyName = value; } }        
        public static string DefaultFolder { get { return _sDefaultFolder; } set { _sDefaultFolder = value; } }
        public static string UploadFolder { get { return _sUploadFolder; } set { _sUploadFolder = value; } }
        public static string DocFilesPath_HTTP { get { return _sDocFilesPath_HTTP; } set { _sDocFilesPath_HTTP = value; } }
        public static string GridStyle  { get { return _sGridStyle; } set { _sGridStyle = value; } }
        public static Color GridHighlightForeColor { get { return _clrGridHighlightForeColor; } set { _clrGridHighlightForeColor = value; } }        
        public static string AppTitle  { get { return _sAppTitle; }  set { _sAppTitle = value; } }
        public static string LEI { get { return _sLEI; } set { _sLEI = value; } }
        public static string InvoicePrinter { get { return _sInvoicePrinter; } set { _sInvoicePrinter = value; } }
        public static int Division { get { return _iDivision; } set { _iDivision = value; } }        
        public static int ClientsRequests_Status { get { return _iClientsRequests_Status; } set { _iClientsRequests_Status = value; } }
        public static int DivisionFilter { get { return _iDivisionFilter; } set { _iDivisionFilter = value; } }
        public static int DMSAccess { get { return _iDMSAccess; } set { _iDMSAccess = value; } }
        public static int AllowInsertOldOrders { get { return _iAllowInsertOldOrders; } set { _iAllowInsertOldOrders = value; } }
        public static string FIX_DB_Server_Path { get { return _sFIX_DB_Server_Path; } set { _sFIX_DB_Server_Path = value; } }        
        public static int Chief { get { return _iChief; } set { _iChief = value; } }
        public static int RM { get { return _iRM; } set { _iRM = value; } }
        public static int Sender { get { return _iSender; } set { _iSender = value; } }
        public static int Introducer { get { return _iIntroducer; } set { _iIntroducer = value; } }
        public static int Diaxiristis { get { return _iDiaxiristis; } set { _iDiaxiristis = value; } }
        public static string DMSTransferPoint { get { return _sDMSTransferPoint; } set { _sDMSTransferPoint = value; } }
        public static string DMSMapDrive { get { return _sDMSMapDrive; } set { _sDMSMapDrive = value; } }
        public static string DMSMapDriveAddress { get { return _sDMSMapDriveAddress; } set { _sDMSMapDriveAddress = value; } }
        public static string DocFilesPath_Win { get { return _sDocFilesPath_Win; } set { _sDocFilesPath_Win = value; } }
        public static string DocFilesPath_FTP { get { return _sDocFilesPath_FTP; } set { _sDocFilesPath_FTP = value; } }        
        public static string EMail_Sender { get { return _sEMail_Sender; } set { _sEMail_Sender = value; } }
        public static string EMail_Username { get { return _sEMail_Username; } set { _sEMail_Username = value; } }
        public static string EMail_Password { get { return _sEMail_Password; } set { _sEMail_Password = value; } }
        public static string NonReplay_Sender { get { return _sNonReplay_Sender; } set { _sNonReplay_Sender = value; } }
        public static string NonReplay_Username { get { return _sNonReplay_Username; } set { _sNonReplay_Username = value; } }
        public static string NonReplay_Password { get { return _sNonReplay_Password; } set { _sNonReplay_Password = value; } }
        public static string Request_Sender { get { return _sRequest_Sender; } set { _sRequest_Sender = value; } }
        public static string Request_Username { get { return _sRequest_Username; } set { _sRequest_Username = value; } }
        public static string Request_Password { get { return _sRequest_Password; } set { _sRequest_Password = value; } }
        public static string Support_Sender { get { return _sSupport_Sender; } set { _sSupport_Sender = value; } }
        public static string Support_Username { get { return _sSupport_Username; } set { _sSupport_Username = value; } }
        public static string Support_Password { get { return _sSupport_Password; } set { _sSupport_Password = value; } }
        public static string EMail_BO_Receiver { get { return _sEMail_BO_Receiver; } set { _sEMail_BO_Receiver = value; } }
        public static string FTP_Username { get { return _sFTP_Username; } set { _sFTP_Username = value; } }
        public static string FTP_Password { get { return _sFTP_Password; } set { _sFTP_Password = value; } }
        public static string RS_Address { get { return _sRS_Address; } set { _sRS_Address = value; } }
        public static string RS_Username { get { return _sRS_Username; } set { _sRS_Username = value; } }
        public static string RS_Password { get { return _sRS_Password; } set { _sRS_Password = value; } }
        public static string SMS_Username { get { return _sSMS_Username; } set { _sSMS_Username = value; } }
        public static string SMS_Password { get { return _sSMS_Password; } set { _sSMS_Password = value; } }
        public static string SMS_From { get { return _sSMS_From; } set { _sSMS_From = value; } }
        public static int User_ID  { get { return _iUser_ID; } set { _iUser_ID = value; } }
        public static int UserStatus { get { return _iUserStatus; } set { _iUserStatus = value; } }
        public static string DBSuffix { get { return _sDBSuffix; } set { _sDBSuffix = value; } }
        public static string UserName  { get { return _sUserName; } set { _sUserName = value; } }
        public static string UserMobile { get { return _sUserMobile; } set { _sUserMobile = value; } }
        public static string UserEMail { get { return _sUserEMail; } set { _sUserEMail = value; } }
        public static int UserLocation { get { return _iUserLocation; } set { _iUserLocation = value; } }        
    }
    public static class PrinterClass
    {
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
    }
}
