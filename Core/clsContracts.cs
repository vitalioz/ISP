using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsContracts
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;
        DataRow[] foundRows;

        private int _iRecord_ID;
        private int _iPackageType;
        private int _iClient_ID;
        private int _iClientTipos;
        private int _iContractType;
        private string _sContractTitle;
        private string _sCode;
        private string _sPortfolio;
        private string _sPortfolio_Alias;
        private string _sPortfolio_Type;
        private DateTime _dStart;
        private DateTime _dFinish;
        private string _sCurrency;
        private string _sNumberAccount;
        private int _iContracts_Details_ID;
        private int _iContracts_Packages_ID;
        private int _iCDP_ID;
        private string _sCDP_Notes;
        private int _iMiFID_2;
        private DateTime _dMiFID_2_StartDate;
        private int _iQuestionary_ID;
        private int _iXAA;
        private int _iStatus;        

        private string _sPackage_Title;
        private int _iPackageProvider_ID;
        private string _sPackageProvider;
        private string _sPackageProvider_PriceTable;

        private string _sL4;
        private float _fltVAT_FP;
        private float _fltVAT_NP;
        private int _iMiFID_Risk;
        private int _iProfile_ID;
        private string _sProfileTitle;

        private int _iService_ID;
        private string _sService_Title;
        private int _iServiceProvider_ID;
        private int _iServiceOption_ID;
        private int _iCashAccount_ID;

        private int _iAdvisor_ID;
        private string _sAdvisorFullname;
        private string _sAdvisorEMail;
        private string _sAdvisorTel;
        private string _sAdvisorMobile;

        private int _iBrokerageServiceProvider_ID;
        private int _iBrokerageOption_ID;
        private string _sBrokerageServiceProvider_Title;
        private string _sBrokerageOption_Title;

        private int _iRTOServiceProvider_ID;
        private int _iRTOOption_ID;
        private string _sRTOServiceProvider_Title;
        private string _sRTOOption_Title;

        private string _sAdvisoryServiceProvider_Title;
        private int _iAdvisoryServiceProvider_ID;
        private string _sAdvisoryOption_Title;
        private int _iAdvisoryOption_ID;
        private string _sAdvisoryInvestmentProfile_Title;
        private int _iAdvisoryInvestmentProfile_ID;
        private string _sAdvisoryInvestmentPolicy_Title;
        private int _iAdvisoryInvestmentPolicy_ID;
        private float _fltAdvisory_MonthMinAmount;
        private string _sAdvisory_MonthMinCurr;
        private float _fltAdvisory_OpenAmount;
        private string _sAdvisory_OpenCurr;
        private float _fltAdvisory_ServiceAmount;
        private string _sAdvisory_ServiceCurr;
        private float _fltAdvisory_MinAmount;
        private string _sAdvisory_MinCurr;
        private float _fltAdvisory_Month3_Discount;
        private float _fltAdvisory_Month3_Fees;
        private string _sAdvisory_AllManFees;

        private string _sDiscretServiceProvider_Title;
        private int _iDiscretServiceProvider_ID;
        private string _sDiscretOption_Title;
        private int _iDiscretOption_ID;
        private string _sDiscretInvestmentProfile_Title;
        private int _iDiscretInvestmentProfile_ID;
        private string _sDiscretInvestmentPolicy_Title;
        private int _iDiscretInvestmentPolicy_ID;
        private float _fltDiscret_MonthMinAmount;
        private string _sDiscret_MonthMinCurr;
        private float _fltDiscret_OpenAmount;
        private string _sDiscret_OpenCurr;
        private float _fltDiscret_ServiceAmount;
        private string _sDiscret_ServiceCurr;
        private float _fltDiscret_MinAmount;
        private string _sDiscret_MinCurr;
        private float _fltDiscret_Month3_Discount;
        private float _fltDiscret_Month3_Fees;
        private string _sDiscret_AllManFees;

        private string _sCustodyServiceProvider_Title;
        private int _iCustodyServiceProvider_ID;
        private string _sCustodyOption_Title;
        private int _iCustodyOption_ID;
        private float _fltCustody_MonthMinAmount;
        private string _sCustody_MonthMinCurr;
        private float _fltCustody_OpenAmount;
        private string _sCustody_OpenCurr;
        private float _fltCustody_ServiceAmount;
        private string _sCustody_ServiceCurr;
        private float _fltCustody_MinAmount;
        private string _sCustody_MinCurr;

        private string _sAdminServiceProvider_Title;
        private int _iAdminServiceProvider_ID;
        private string _sAdminOption_Title;
        private int _iAdminOption_ID;
        private float _fltAdmin_MonthMinAmount;
        private string _sAdmin_MonthMinCurr;
        private float _fltAdmin_OpenAmount;
        private string _sAdmin_OpenCurr;
        private float _fltAdmin_ServiceAmount;
        private string _sAdmin_ServiceCurr;
        private float _fltAdmin_MinAmount;
        private string _sAdmin_MinCurr;

        private string _sDealAdvisoryServiceProvider_Title;
        private int _iDealAdvisoryServiceProvider_ID;
        private string _sDealAdvisoryOption_Title;
        private int _iDealAdvisoryOption_ID;
        private string _sDealAdvisoryInvestmentPolicy_Title;
        private int _iDealAdvisoryInvestmentPolicy_ID;
        private float _fltDealAdvisory_MonthMinAmount;
        private string _sDealAdvisory_MonthMinCurr;
        private float _fltDealAdvisory_OpenAmount;
        private string _sDealAdvisory_OpenCurr;
        private float _fltDealAdvisory_ServiceAmount;
        private string _sDealAdvisory_ServiceCurr;
        private float _fltDealAdvisory_MinAmount;
        private string _sDealAdvisory_MinCurr;

        private int _iLombardOption_ID;
        private string _sLombardOption_Title;
        private int _iLombardServiceProvider_ID;
        private string _sLombardServiceProvider_Title;
        private string _sLombard_AMR;

        private int _iFXOption_ID;
        private string _sFXOption_Title;
        private int _iFXServiceProvider_ID;
        private string _sFXServiceProvider_Title;

        private int _iSettlementsOption_ID;
        private string _sSettlementsOption_Title;
        private int _iSettlementsServiceProvider_ID;
        private string _sSettlementsServiceProvider_Title;

        private string _sClientName;
        private string _sClientsList;
        private DateTime _dAktionDate;

        private int _iDivision;
        private int _iDivisionFilter;
        private int _iDet = 0, _iPack = 0;
        private int _iClientStatus;
        private string _sClientsFilter;
        private string _sSurnameGreek;
        private string _sSurnameEnglish;
        private float _fltAmount;
        private float _fltCompanyFeesPercent;
        private DataTable _dtList;

        clsContracts_Details _klsDetails = new clsContracts_Details();
        clsContracts_Packages _klsPackages = new clsContracts_Packages();
        clsContracts_Details_Packages _klsContracts_Details_Packages = new clsContracts_Details_Packages();
        public clsContracts()
        {
            this._iRecord_ID = 0;
            this._iPackageType = 0;
            this._iClient_ID = 0;
            this._iContractType = 0;
            this._sContractTitle = "";
            this._sCode = "";
            this._sPortfolio = "";
            this._sPortfolio_Alias = "";
            this._sPortfolio_Type = "";
            this._dStart = Convert.ToDateTime("1900/01/01");
            this._dFinish = Convert.ToDateTime("2070/12/31");
            this._sCurrency = "";
            this._sNumberAccount = "";
            this._iContracts_Details_ID = 0;
            this._iContracts_Packages_ID = 0;
            this._iCDP_ID = 0;
            this._sCDP_Notes = "";
            this._iMiFID_2 = 0;
            this._dMiFID_2_StartDate = Convert.ToDateTime("1900/01/01");
            this._iQuestionary_ID = 0;
            this._iXAA = 0;
            this._iStatus = 0;

            this._sPackage_Title = "";
            this._iPackageProvider_ID = 0;
            this._sPackageProvider = "";
            this._sPackageProvider_PriceTable = "";
            this._iClientTipos = 0;
            this._sL4 = "";
            this._fltVAT_FP = 0;
            this._fltVAT_NP = 0;
            this._iMiFID_Risk = 0;
            this._iProfile_ID = 0;
            this._sProfileTitle = "";

            this._iService_ID = 0;
            this._sService_Title = "";
            this._iServiceProvider_ID = 0;
            this._iServiceOption_ID = 0;
            this._iCashAccount_ID = 0;

            this._iAdvisor_ID = 0;
            this._sAdvisorFullname = "";
            this._sAdvisorEMail = "";
            this._sAdvisorTel = "";
            this._sAdvisorMobile = "";

            this._iBrokerageServiceProvider_ID = 0;
            this._iBrokerageOption_ID = 0;
            this._sBrokerageServiceProvider_Title = "";
            this._sBrokerageOption_Title = "";

            this._iRTOServiceProvider_ID = 0;
            this._iRTOOption_ID = 0;
            this._sRTOServiceProvider_Title = "";
            this._sRTOOption_Title = "";

            this._sAdvisoryServiceProvider_Title = "";
            this._iAdvisoryServiceProvider_ID = 0;
            this._sAdvisoryOption_Title = "";
            this._iAdvisoryOption_ID = 0;
            this._sAdvisoryInvestmentProfile_Title = "";
            this._iAdvisoryInvestmentProfile_ID = 0;
            this._sAdvisoryInvestmentPolicy_Title = "";
            this._iAdvisoryInvestmentPolicy_ID = 0;
            this._fltAdvisory_MonthMinAmount = 0;
            this._sAdvisory_MonthMinCurr = "";
            this._fltAdvisory_OpenAmount = 0;
            this._sAdvisory_OpenCurr = "";
            this._fltAdvisory_ServiceAmount = 0;
            this._sAdvisory_ServiceCurr = "";
            this._fltAdvisory_MinAmount = 0;
            this._sAdvisory_MinCurr = "";
            this._fltAdvisory_Month3_Discount = 0;
            this._fltAdvisory_Month3_Fees = 0;
            this._sAdvisory_AllManFees = "";

            this._sDiscretServiceProvider_Title = "";
            this._iDiscretServiceProvider_ID = 0;
            this._sDiscretOption_Title = "";
            this._iDiscretOption_ID = 0;
            this._sDiscretInvestmentProfile_Title = "";
            this._iDiscretInvestmentProfile_ID = 0;
            this._sDiscretInvestmentPolicy_Title = "";
            this._iDiscretInvestmentPolicy_ID = 0;
            this._fltDiscret_MonthMinAmount = 0;
            this._sDiscret_MonthMinCurr = "";
            this._fltDiscret_OpenAmount = 0;
            this._sDiscret_OpenCurr = "";
            this._fltDiscret_ServiceAmount = 0;
            this._sDiscret_ServiceCurr = "";
            this._fltDiscret_MinAmount = 0;
            this._sDiscret_MinCurr = "";
            this._fltDiscret_Month3_Discount = 0;
            this._fltDiscret_Month3_Fees = 0;
            this._sDiscret_AllManFees = "";

            this._sCustodyServiceProvider_Title = "";
            this._iCustodyServiceProvider_ID = 0;
            this._sCustodyOption_Title = "";
            this._iCustodyOption_ID = 0;
            this._fltCustody_MonthMinAmount = 0;
            this._sCustody_MonthMinCurr = "";
            this._fltCustody_OpenAmount = 0;
            this._sCustody_OpenCurr = "";
            this._fltCustody_ServiceAmount = 0;
            this._sCustody_ServiceCurr = "";
            this._fltCustody_MinAmount = 0;
            this._sCustody_MinCurr = "";

            this._sAdminServiceProvider_Title = "";
            this._iAdminServiceProvider_ID = 0;
            this._sAdminOption_Title = "";
            this._iAdminOption_ID = 0;
            this._fltAdmin_MonthMinAmount = 0;
            this._sAdmin_MonthMinCurr = "";
            this._fltAdmin_OpenAmount = 0;
            this._sAdmin_OpenCurr = "";
            this._fltAdmin_ServiceAmount = 0;
            this._sAdmin_ServiceCurr = "";
            this._fltAdmin_MinAmount = 0;
            this._sAdmin_MinCurr = "";

            this._sDealAdvisoryServiceProvider_Title = "";
            this._iDealAdvisoryServiceProvider_ID = 0;
            this._sDealAdvisoryOption_Title = "";
            this._iDealAdvisoryOption_ID = 0;
            this._sDealAdvisoryInvestmentPolicy_Title = "";
            this._iDealAdvisoryInvestmentPolicy_ID = 0;
            this._fltDealAdvisory_MonthMinAmount = 0;
            this._sDealAdvisory_MonthMinCurr = "";
            this._fltDealAdvisory_OpenAmount = 0;
            this._sDealAdvisory_OpenCurr = "";
            this._fltDealAdvisory_ServiceAmount = 0;
            this._sDealAdvisory_ServiceCurr = "";
            this._fltDealAdvisory_MinAmount = 0;
            this._sDealAdvisory_MinCurr = "";

            this._iLombardOption_ID = 0;
            this._sLombardOption_Title = "";
            this._iLombardServiceProvider_ID = 0;
            this._sLombardServiceProvider_Title = "";
            this._sLombard_AMR = "";

            this._iFXOption_ID = 0;
            this._sFXOption_Title = "";
            this._iFXServiceProvider_ID = 0;
            this._sFXServiceProvider_Title = "";

            this._iSettlementsOption_ID = 0;
            this._sSettlementsOption_Title = "";
            this._iSettlementsServiceProvider_ID = 0;
            this._sSettlementsServiceProvider_Title = "";

            this._iClientStatus = 0;
            this._sClientName = "";
            this._sClientsList = "";
            this._dAktionDate = Convert.ToDateTime("1900/01/01");

            this._iDet = 0;
            this._iPack = 0;
            this._fltAmount = 0;
            this._fltCompanyFeesPercent = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetContract", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Contract_ID", _iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Contract_Details_ID", _iContracts_Details_ID));
                cmd.Parameters.Add(new SqlParameter("@Contract_Packages_ID", _iContracts_Packages_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iPackageType = Convert.ToInt32(drList["PackageType"]);
                    this._sPackage_Title = drList["Package_Title"] + "";
                    this._iPackageProvider_ID = Convert.ToInt32(drList["PackageProvider_ID"]);
                    this._sPackageProvider = drList["PackageProvider_Title"] + "";
                    this._sPackageProvider_PriceTable = drList["PackageProvider_PriceTable"] + "";

                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    if (drList["ClientTipos"] + "" != "") this._iClientTipos = Convert.ToInt32(drList["ClientTipos"]);
                    else this._iClientTipos = 0;
                    if (this._iClientTipos == 1) this._sClientName = (drList["Surname"] + " " + drList["Firstname"]).Trim();
                    else this._sClientName = (drList["Surname"] + "").Trim();

                    this._iContractType = Convert.ToInt32(drList["Tipos"]);
                    this._sContractTitle = drList["ContractTitle"] + "";
                    this._iContracts_Details_ID = Convert.ToInt32(drList["locContracts_Details_ID"]);
                    this._iContracts_Packages_ID = Convert.ToInt32(drList["locContracts_Packages_ID"]);
                    this._sProfileTitle = drList["Profile_Title"] + "";

                    if (drList["Profile_ID"] + "" != "") this._iProfile_ID = Convert.ToInt32(drList["Profile_ID"]);
                    else this._iProfile_ID = 0;

                    if (drList["MIFID_Risk"] + "" != "") this._iMiFID_Risk = Convert.ToInt32(drList["MIFID_Risk"]);
                    else this._iMiFID_Risk = 0;

                    this._iMiFID_2 = Convert.ToInt32(drList["MiFID_2"]);
                    this._dMiFID_2_StartDate = Convert.ToDateTime(drList["MIFID_2_StartDate"]);
                    this._iQuestionary_ID = Convert.ToInt32(drList["Questionary_ID"]);
                    this._iXAA = Convert.ToInt32(drList["XAA"]);
                    this._sCode = drList["Code"] + "";
                    this._sPortfolio = drList["Portfolio"] + "";
                    this._sPortfolio_Alias = drList["Portfolio_Alias"] + "";
                    this._sPortfolio_Type = drList["Portfolio_Type"] + "";
                    this._dStart = Convert.ToDateTime(drList["DateStart"]);
                    this._dFinish = Convert.ToDateTime(drList["DateFinish"]);
                    this._sCurrency = drList["Currency"] + "";
                    this._sNumberAccount = drList["NumberAccount"] + "";
                    this._sL4 = drList["L4"] + "";
                    this._iService_ID = Convert.ToInt32(drList["PackageType_ID"]);
                    this._sService_Title = drList["Service_Title"] + "";

                    if (Convert.ToInt32(drList["User1_ID"]) != 0)
                    {
                        this._iAdvisor_ID = Convert.ToInt32(drList["User1_ID"]);
                        this._sAdvisorFullname = drList["AdvisorSurname"] + " " + drList["AdvisorFirstname"];
                        this._sAdvisorEMail = drList["AdvisorEMail"] + "";
                        this._sAdvisorMobile = drList["AdvisorMobile"] + "";
                        this._sAdvisorTel = drList["AdvisorTel"] + "";
                    }
                    else
                    {
                        this._iAdvisor_ID = 0;
                        this._sAdvisorFullname = "";
                        this._sAdvisorEMail = "";
                        this._sAdvisorMobile = "";
                        this._sAdvisorTel = "";
                    }

                    this._iBrokerageServiceProvider_ID = Convert.ToInt32(drList["BrokerageServiceProvider_ID"]);
                    this._iBrokerageOption_ID = Convert.ToInt32(drList["BrokerageOption_ID"]);
                    this._sBrokerageServiceProvider_Title = drList["BrokerageServiceProvider_Title"] + "";
                    this._sBrokerageOption_Title = drList["BrokerageOption_Title"] + "";

                    this._iRTOServiceProvider_ID = Convert.ToInt32(drList["RTOServiceProvider_ID"]);
                    this._iRTOOption_ID = Convert.ToInt32(drList["RTOOption_ID"]);
                    this._sRTOServiceProvider_Title = drList["RTOServiceProviders_Title"] + "";
                    this._sRTOOption_Title = drList["RTOOption_Title"] + "";

                    this._sAdvisoryServiceProvider_Title = drList["AdvisoryServiceProvider_Title"] + "";
                    this._iAdvisoryServiceProvider_ID = Convert.ToInt32(drList["AdvisoryServiceProvider_ID"]);
                    this._sAdvisoryOption_Title = drList["AdvisoryOption_Title"] + "";
                    this._iAdvisoryOption_ID = Convert.ToInt32(drList["AdvisoryOption_ID"]);
                    if (this._iAdvisoryOption_ID != 0)
                    {
                        this._sAdvisoryInvestmentProfile_Title = drList["AdvisoryInvestmentProfile_Title"] + "";
                        this._iAdvisoryInvestmentProfile_ID = Convert.ToInt32(drList["AdvisoryInvestmentProfile_ID"]);
                        this._sAdvisoryInvestmentPolicy_Title = drList["AdvisoryInvestmentPolicy_Title"] + "";
                        this._iAdvisoryInvestmentPolicy_ID = Convert.ToInt32(drList["AdvisoryInvestmentPolicy_ID"]);
                        this._fltAdvisory_MonthMinAmount = Convert.ToSingle(drList["Advisory_MonthMinAmount"]);
                        this._sAdvisory_MonthMinCurr = drList["Advisory_MonthMinCurr"] + "";
                        this._fltAdvisory_OpenAmount = Convert.ToSingle(drList["Advisory_OpenAmount"]);
                        this._sAdvisory_OpenCurr = drList["Advisory_OpenCurr"] + "";
                        this._fltAdvisory_ServiceAmount = Convert.ToSingle(drList["Advisory_ServiceAmount"]);
                        this._sAdvisory_ServiceCurr = drList["Advisory_ServiceCurr"] + "";
                        this._fltAdvisory_MinAmount = Convert.ToSingle(drList["Advisory_MinAmount"]);
                        this._sAdvisory_MinCurr = drList["Advisory_MinCurr"] + "";
                        if (drList["Advisory_Month3_Discount"] + "" != "")
                        {
                            this._fltAdvisory_Month3_Discount = Convert.ToSingle(drList["Advisory_Month3_Discount"]);
                            this._fltAdvisory_Month3_Fees = Convert.ToSingle(drList["Advisory_Month3_Fees"]);
                            this._sAdvisory_AllManFees = drList["Advisory_AllManFees"] + "";
                        }
                        else
                        {
                            this._fltAdvisory_Month3_Discount = 0;
                            this._fltAdvisory_Month3_Fees = 0;
                            this._sAdvisory_AllManFees = "";
                        }
                    }

                    this._sDiscretServiceProvider_Title = drList["DiscretServiceProvider_Title"] + "";
                    this._iDiscretServiceProvider_ID = Convert.ToInt32(drList["DiscretServiceProvider_ID"]);
                    this._sDiscretOption_Title = drList["DiscretOption_Title"] + "";
                    this._iDiscretOption_ID = Convert.ToInt32(drList["DiscretOption_ID"]);
                    if (this._iDiscretOption_ID != 0)
                    {
                        this._sDiscretInvestmentProfile_Title = drList["DiscretInvestmentProfile_Title"] + "";
                        this._iDiscretInvestmentProfile_ID = Convert.ToInt32(drList["DiscretInvestmentProfile_ID"]);
                        this._sDiscretInvestmentPolicy_Title = drList["DiscretInvestmentPolicy_Title"] + "";
                        this._iDiscretInvestmentPolicy_ID = Convert.ToInt32(drList["DiscretInvestmentPolicy_ID"]);
                        this._fltDiscret_MonthMinAmount = Convert.ToSingle(drList["Discret_MonthMinAmount"]);
                        this._sDiscret_MonthMinCurr = drList["Discret_MonthMinCurr"] + "";
                        this._fltDiscret_OpenAmount = Convert.ToSingle(drList["Discret_OpenAmount"]);
                        this._sDiscret_OpenCurr = drList["Discret_OpenCurr"] + "";
                        this._fltDiscret_ServiceAmount = Convert.ToSingle(drList["Discret_ServiceAmount"]);
                        this._sDiscret_ServiceCurr = drList["Discret_ServiceCurr"] + "";
                        this._fltDiscret_MinAmount = Convert.ToSingle(drList["Discret_MinAmount"]);
                        this._sDiscret_MinCurr = drList["Discret_MinCurr"] + "";
                        if (drList["Discret_Month3_Discount"] + "" != "")
                        {
                            this._fltDiscret_Month3_Discount = Convert.ToSingle(drList["Discret_Month3_Discount"]);
                            this._fltDiscret_Month3_Fees = Convert.ToSingle(drList["Discret_Month3_Fees"]);
                            this._sDiscret_AllManFees = drList["Discret_AllManFees"] + "";
                        }
                        else
                        {
                            this._fltDiscret_Month3_Discount = 0;
                            this._fltDiscret_Month3_Fees = 0;
                            this._sDiscret_AllManFees = "";
                        }
                    }

                    if (Convert.ToInt32(drList["CustodyServiceProvider_ID"]) != 0)
                    {
                        this._sCustodyServiceProvider_Title = drList["CustodyServiceProvider_Title"] + "";
                        this._iCustodyServiceProvider_ID = Convert.ToInt32(drList["CustodyServiceProvider_ID"]);
                    }

                    if (Convert.ToInt32(drList["CustodyOption_ID"]) != 0)
                    {
                        this._sCustodyOption_Title = drList["CustodyOption_Title"] + "";
                        this._iCustodyOption_ID = Convert.ToInt32(drList["CustodyOption_ID"]);
                        this._fltCustody_MonthMinAmount = Convert.ToSingle(drList["Custody_MonthMinAmount"]);
                        this._sCustody_MonthMinCurr = drList["Custody_MonthMinCurr"] + "";
                        this._fltCustody_OpenAmount = Convert.ToSingle(drList["Custody_OpenAmount"]);
                        this._sCustody_OpenCurr = drList["Custody_OpenCurr"] + "";
                        this._fltCustody_ServiceAmount = Convert.ToSingle(drList["Custody_ServiceAmount"]);
                        this._sCustody_ServiceCurr = drList["Custody_ServiceCurr"] + "";
                        this._fltCustody_MinAmount = Convert.ToSingle(drList["Custody_MinAmount"]);
                        this._sCustody_MinCurr = drList["Custody_MinCurr"] + "";
                    }

                    if (drList["AdministrationServiceProvider_ID"] + "" != "")
                    {
                        if (Convert.ToInt32(drList["AdministrationServiceProvider_ID"]) != 0)
                        {
                            this._sAdminServiceProvider_Title = drList["AdminServiceProvider_Title"] + "";
                            this._iAdminServiceProvider_ID = Convert.ToInt32(drList["AdminServiceProvider_ID"]);
                        }

                        if (Convert.ToInt32(drList["AdministrationOption_ID"]) != 0)
                        {
                            this._sAdminOption_Title = drList["AdminOption_Title"] + "";
                            this._iAdminOption_ID = Convert.ToInt32(drList["AdministrationOption_ID"]);
                            this._fltAdmin_MonthMinAmount = Convert.ToSingle(drList["Admin_MonthMinAmount"]);
                            this._sAdmin_MonthMinCurr = drList["Admin_MonthMinCurr"] + "";
                            this._fltAdmin_OpenAmount = Convert.ToSingle(drList["Admin_OpenAmount"]);
                            this._sAdmin_OpenCurr = drList["Admin_OpenCurr"] + "";
                            this._fltAdmin_ServiceAmount = Convert.ToSingle(drList["Admin_ServiceAmount"]);
                            this._sAdmin_ServiceCurr = drList["Admin_ServiceCurr"] + "";
                            this._fltAdmin_MinAmount = Convert.ToSingle(drList["Admin_MinAmount"]);
                            this._sAdmin_MinCurr = drList["Admin_MinCurr"] + "";
                        }
                    }

                    this._sDealAdvisoryServiceProvider_Title = drList["DealAdvisoryServiceProvider_Title"] + "";
                    this._iDealAdvisoryServiceProvider_ID = Convert.ToInt32(drList["DealAdvisoryServiceProvider_ID"]);
                    this._sDealAdvisoryOption_Title = drList["DealAdvisoryOption_Title"] + "";
                    this._iDealAdvisoryOption_ID = Convert.ToInt32(drList["DealAdvisoryOption_ID"]);
                    this._sDealAdvisoryInvestmentPolicy_Title = drList["DealAdvisoryInvestmentPolicy_Title"] + "";
                    this._iDealAdvisoryInvestmentPolicy_ID = Convert.ToInt32(drList["DealAdvisoryInvestmentPolicy_ID"]);
                    if (this._iDealAdvisoryOption_ID != 0)
                    {
                        this._fltDealAdvisory_MonthMinAmount = Convert.ToSingle(drList["DealAdvisory_MonthMinAmount"]);
                        this._sDealAdvisory_MonthMinCurr = drList["DealAdvisory_MonthMinCurr"] + "";
                        this._fltDealAdvisory_OpenAmount = Convert.ToSingle(drList["DealAdvisory_OpenAmount"]);
                        this._sDealAdvisory_OpenCurr = drList["DealAdvisory_OpenCurr"] + "";
                        this._fltDealAdvisory_ServiceAmount = Convert.ToSingle(drList["DealAdvisory_ServiceAmount"]);
                        this._sDealAdvisory_ServiceCurr = drList["DealAdvisory_ServiceCurr"] + "";
                        this._fltDealAdvisory_MinAmount = Convert.ToSingle(drList["DealAdvisory_MinAmount"]);
                        this._sDealAdvisory_MinCurr = drList["DealAdvisory_MinCurr"] + "";
                    }

                    this._iLombardOption_ID = Convert.ToInt32(drList["LombardOption_ID"]);
                    this._sLombardOption_Title = drList["LombardOption_Title"] + "";
                    this._iLombardServiceProvider_ID = Convert.ToInt32(drList["LombardServiceProvider_ID"]);
                    this._sLombardServiceProvider_Title = drList["LombardServiceProvider_Title"] + "";
                    this._sLombard_AMR = drList["Lombard_AMR"] + "";
                    this._iFXOption_ID = Convert.ToInt32(drList["FXOption_ID"]);
                    this._sFXOption_Title = drList["FXOption_Title"] + "";
                    this._iFXServiceProvider_ID = Convert.ToInt32(drList["FXServiceProvider_ID"]);
                    this._sFXServiceProvider_Title = drList["FXServiceProvider_Title"] + "";
                    this._iSettlementsOption_ID = Convert.ToInt32(drList["SettlementsOption_ID"]);
                    this._sSettlementsOption_Title = drList["SettlementsOption_Title"] + "";
                    this._iSettlementsServiceProvider_ID = Convert.ToInt32(drList["SettlementsServiceProvider_ID"]);
                    this._sSettlementsServiceProvider_Title = drList["SettlementsServiceProvider_Title"] + "";
                    switch (this._iService_ID)
                    {
                        case 1:                  // RTO
                            this._iServiceProvider_ID = this._iBrokerageServiceProvider_ID;
                            this._iServiceOption_ID = this._iBrokerageOption_ID;
                            break;
                        case 2:                  // Advisory
                            this._iServiceProvider_ID = this._iAdvisoryServiceProvider_ID;
                            this._iServiceOption_ID = this._iAdvisoryOption_ID;
                            if (this._iServiceOption_ID != 0)
                            {
                                this._fltVAT_FP = Convert.ToSingle(drList["Advisory_VAT_FP"]);
                                this._fltVAT_NP = Convert.ToSingle(drList["Advisory_VAT_NP"]);
                            }
                            break;
                        case 3:                  // Discretionary
                            this._iServiceProvider_ID = this._iDiscretServiceProvider_ID;
                            this._iServiceOption_ID = this._iDiscretOption_ID;
                            if (this._iServiceOption_ID != 0)
                            {
                                this._fltVAT_FP = Convert.ToSingle(drList["Discret_VAT_FP"]);
                                this._fltVAT_NP = Convert.ToSingle(drList["Discret_VAT_NP"]);
                            }
                            break;
                        case 4:                  // Custody
                            this._iServiceProvider_ID = this._iCustodyServiceProvider_ID;
                            this._iServiceOption_ID = this._iCustodyOption_ID;
                            break;
                        case 5:                  // Dealing Advisory
                            this._iServiceProvider_ID = this._iDealAdvisoryServiceProvider_ID;
                            this._iServiceOption_ID = this._iDealAdvisoryOption_ID;
                            if (this._iServiceOption_ID != 0)
                            {
                                this._fltVAT_FP = Convert.ToSingle(drList["DealAdvisory_VAT_FP"]);
                                this._fltVAT_NP = Convert.ToSingle(drList["DealAdvisory_VAT_NP"]);
                            }
                            break;
                        case 6:                  // Lombard Lending
                            this._iServiceProvider_ID = this._iLombardServiceProvider_ID;
                            this._iServiceOption_ID = this._iLombardOption_ID;
                            break;
                        case 7:                  // Settlements
                            this._iServiceProvider_ID = this._iSettlementsServiceProvider_ID;
                            this._iServiceOption_ID = this._iSettlementsOption_ID;
                            break;
                        case 8:                 // FX
                            this._iServiceProvider_ID = this._iFXServiceProvider_ID;
                            this._iServiceOption_ID = this._iFXOption_ID;
                            break;
                        case 9:                 // RTO
                            this._iServiceProvider_ID = this._iRTOServiceProvider_ID;
                            this._iServiceOption_ID = this._iRTOOption_ID;
                            break;
                        case 10:                  // Administration
                            this._iServiceProvider_ID = this._iAdminServiceProvider_ID;
                            this._iServiceOption_ID = this._iAdminOption_ID;
                            break;
                    }
                    this._iStatus = Convert.ToInt32(drList["Status"]);

                    _klsDetails = new clsContracts_Details();
                    _klsDetails.Contract_ID = 0;
                    _klsDetails.Record_ID = this._iContracts_Details_ID;
                    _klsDetails.GetRecord();

                    _klsPackages = new clsContracts_Packages();
                    _klsPackages.Contract_ID = 0;
                    _klsPackages.Record_ID = this._iContracts_Packages_ID;
                    _klsPackages.GetRecord();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { drList.Close(); conn.Close(); }
        }
        public void GetRecord_Code_Portfolio()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetContract_CodePortfolio", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Code", _sCode));
                cmd.Parameters.Add(new SqlParameter("@Portfolio", _sPortfolio));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iPackageType = Convert.ToInt32(drList["PackageType"]);
                    this._sPackage_Title = drList["Package_Title"] + "";
                    this._iClientTipos = Convert.ToInt32(drList["ClientTipos"]);
                    this._iContractType = Convert.ToInt32(drList["Tipos"]);
                    this._sContractTitle = drList["ContractTitle"] + "";
                    this._iContracts_Details_ID = Convert.ToInt32(drList["Contracts_Details_ID"]);
                    this._iContracts_Packages_ID = Convert.ToInt32(drList["Contracts_Packages_ID"]);
                    this._iCDP_ID = Convert.ToInt32(drList["CDP_ID"]);
                    this._sCDP_Notes = drList["CDP_Notes"] + "";

                    this._iProfile_ID = 0;
                    this._sProfileTitle = "";
                    this._iMiFID_Risk = 0;
                    if (drList["Profile_ID"] + "" != "")
                    {
                        if (Convert.ToInt32(drList["Profile_ID"]) != 0)
                        {
                            this._iProfile_ID = Convert.ToInt32(drList["Profile_ID"]);
                            this._sProfileTitle = drList["Profile_Title"] + "";
                            this._iMiFID_Risk = Convert.ToInt32(drList["MIFID_Risk"]);
                        }
                    }
                    this._iMiFID_2 = Convert.ToInt32(drList["MiFID_2"]);
                    this._dMiFID_2_StartDate = Convert.ToDateTime(drList["MiFID_2_StartDate"]);
                    this._iQuestionary_ID = Convert.ToInt32(drList["Questionary_ID"]);
                    this._iXAA = Convert.ToInt32(drList["XAA"]);
                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    this._sCode = drList["Code"] + "";
                    this._sPortfolio = drList["Portfolio"] + "";
                    this._sPortfolio_Alias = drList["Portfolio_Alias"] + "";
                    this._sPortfolio_Type = drList["Portfolio_Type"] + "";
                    this._sPackage_Title = drList["Package_Title"] + "";
                    this._klsPackages.Service_ID = Convert.ToInt32(drList["Service_ID"]);
                    this._dStart = Convert.ToDateTime(drList["DateStart"]);
                    this._dFinish = Convert.ToDateTime(drList["DateFinish"]);
                    this._sCurrency = drList["Currency"] + "";
                    this._sNumberAccount = drList["NumberAccount"] + "";
                    this._sL4 = drList["L4"] + "";
                    this._iService_ID = Convert.ToInt32(drList["Service_ID"]);
                    this._sService_Title = drList["Service_Title"] + "";
                    this._iBrokerageServiceProvider_ID = Convert.ToInt32(drList["BrokerageServiceProvider_ID"]);
                    this._sBrokerageServiceProvider_Title = drList["ServiceProviders_Title"] + "";
                    //this._sgVAT_FP = drList["VAT_FP");
                    //this._sgVAT_NP = drList["VAT_NP");
                    this._iStatus = Convert.ToInt32(drList["Status"]);

                    this._klsDetails.AgreementNotes = drList["AgreementNotes"] + "";
                    this._klsDetails.PerformanceFees = Convert.ToInt32(drList["PerformanceFees"]);
                    this._klsDetails.User1_ID = Convert.ToInt32(drList["User1_ID"]);
                    this._klsDetails.User2_ID = Convert.ToInt32(drList["User2_ID"]);
                    this._klsDetails.User3_ID = Convert.ToInt32(drList["User3_ID"]);
                    this._klsDetails.User4_ID = Convert.ToInt32(drList["User4_ID"]);
                    this._klsDetails.Surname = drList["Surname"] + "";
                    this._klsDetails.Firstname = drList["Firstname"] + "";
                    this._klsDetails.SurnameFather = drList["SurnameFather"] + "";
                    this._klsDetails.FirstnameFather = drList["FirstnameFather"] + "";
                    this._klsDetails.SurnameMother = drList["SurnameMother"] + "";
                    this._klsDetails.FirstnameMother = drList["FirstnameMother"] + "";
                    this._klsDetails.SurnameSizigo = drList["SurnameSizigo"] + "";
                    this._klsDetails.FirstnameSizigo = drList["FirstnameSizigo"] + "";
                    this._klsDetails.MIFIDCategory_ID = Convert.ToInt32(drList["MIFIDCategory_ID"]);
                    this._klsDetails.Division = Convert.ToInt32(drList["Division"]);
                    this._klsDetails.Brunch_ID = Convert.ToInt32(drList["Brunch_ID"]);
                    this._klsDetails.Spec_ID = Convert.ToInt32(drList["Spec_ID"]);
                    this._klsDetails.DoB = Convert.ToDateTime(drList["DoB"]);
                    this._klsDetails.BornPlace = drList["BornPlace"] + "";
                    this._klsDetails.Sex = drList["Sex"] + "";
                    this._klsDetails.Citizen_ID = Convert.ToInt32(drList["Citizen_ID"]);
                    this._klsDetails.ADT = drList["ADT"] + "";
                    this._klsDetails.ExpireDate = drList["ExpireDate"] + "";
                    this._klsDetails.Police = drList["Police"] + "";
                    this._klsDetails.DOY = drList["DOY"] + "";
                    this._klsDetails.AFM = drList["AFM"] + "";
                    this._klsDetails.AMKA = drList["AMKA"] + "";
                    this._klsDetails.CountryTaxes_ID = Convert.ToInt32(drList["CountryTaxes_ID"]);
                    this._klsDetails.Address = drList["Address"] + "";
                    this._klsDetails.City = drList["City"] + "";
                    this._klsDetails.Zip = drList["Zip"] + "";
                    this._klsDetails.Country_ID = Convert.ToInt32(drList["Country_ID"]);
                    this._klsDetails.Tel = drList["Tel"] + "";
                    this._klsDetails.Fax = drList["Fax"] + "";
                    this._klsDetails.Mobile = drList["Mobile"] + "";
                    this._klsDetails.SendSMS = Convert.ToInt32(drList["SendSMS"]);
                    this._klsDetails.EMail = drList["EMail"] + "";
                    this._klsDetails.InvName = drList["InvName"] + "";
                    this._klsDetails.InvAddress = drList["InvAddress"] + "";
                    this._klsDetails.InvCity = drList["InvCity"] + "";
                    this._klsDetails.InvZip = drList["InvZip"] + "";
                    this._klsDetails.InvCountry_ID = Convert.ToInt32(drList["InvCountry_ID"]);
                    this._klsDetails.Advisory_Name = (drList["AdvisorySurname"] + " " + drList["AdvisoryFirstname"]).Trim();
                    this._klsDetails.RM_Name = (drList["RMSurname"] + " " + drList["RMFirstname"]).Trim();
                    this._klsDetails.Introducer_Name = (drList["IntroSurname"] + " " + drList["IntroFirstname"]).Trim();
                    this._klsDetails.Diaxiristis_Name = (drList["DiaxSurname"] + " " + drList["DiaxFirstname"]).Trim();
                    this._klsDetails.ConnectionMethod = Convert.ToInt32(drList["ConnectionMethod"]);
                    this._klsDetails.Merida = drList["Merida"] + "";
                    this._klsDetails.LogAxion = drList["LogAxion"] + "";

                    this._klsDetails.ChkComplex = Convert.ToInt32(drList["ChkComplex"]);
                    this._klsDetails.ChkWorld = Convert.ToInt32(drList["ChkWorld"]);
                    this._klsDetails.ChkGreece = Convert.ToInt32(drList["ChkGreece"]);
                    this._klsDetails.ChkEurope = Convert.ToInt32(drList["ChkEurope"]);
                    this._klsDetails.ChkAmerica = Convert.ToInt32(drList["ChkAmerica"]);
                    this._klsDetails.ChkAsia = Convert.ToInt32(drList["ChkAsia"]);
                    this._klsDetails.IncomeProducts = drList["IncomeProducts"] + "";
                    this._klsDetails.CapitalProducts = drList["CapitalProducts"] + "";
                    this._klsDetails.ChkSpecificConstraints = Convert.ToInt32(drList["ChkSpecificConstraints"]);
                    this._klsDetails.ChkMonetaryRisk = Convert.ToInt32(drList["ChkMonetaryRisk"]);
                    this._klsDetails.ChkIndividualBonds = Convert.ToInt32(drList["ChkIndividualBonds"]);
                    this._klsDetails.ChkMutualFunds = Convert.ToInt32(drList["ChkMutualFunds"]);
                    this._klsDetails.ChkBondedETFs = Convert.ToInt32(drList["ChkBondedETFs"]);
                    this._klsDetails.ChkIndividualShares = Convert.ToInt32(drList["ChkIndividualShares"]);
                    this._klsDetails.ChkMixedFunds = Convert.ToInt32(drList["ChkMixedFunds"]);
                    this._klsDetails.ChkMixedETFs = Convert.ToInt32(drList["ChkMixedETFs"]);
                    this._klsDetails.ChkFunds = Convert.ToInt32(drList["ChkFunds"]);
                    this._klsDetails.ChkETFs = Convert.ToInt32(drList["ChkETFs"]);
                    this._klsDetails.ChkInvestmentGrade = Convert.ToInt32(drList["ChkInvestmentGrade"]);
                    this._klsDetails.MiscInstructions = drList["MiscInstructions"] + "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally { drList.Close(); conn.Close(); }
        }
        public void GetRecord_Package()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetClient_Contracts", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PackageType", 1));
                cmd.Parameters.Add(new SqlParameter("@Client_ID", _iClient_ID));
                cmd.Parameters.Add(new SqlParameter("@Package_ID", _iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@PackageVersion", "0"));
                cmd.Parameters.Add(new SqlParameter("@CFP_ID", "0"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iPackageType = Convert.ToInt32(drList["PackageType"]);
                    this._sPackage_Title = drList["Package_Title"] + "";
                    this._iClientTipos = Convert.ToInt32(drList["ClientTipos"]);
                    this._iContractType = Convert.ToInt32(drList["Tipos"]);
                    this._sContractTitle = drList["ContractTitle"] + "";
                    this._iContracts_Details_ID = Convert.ToInt32(drList["Contracts_Details_ID"]);
                    this._iContracts_Packages_ID = Convert.ToInt32(drList["Contracts_Packages_ID"]);
                    this._sProfileTitle = drList["Profile_Title"] + "";

                    if (drList["Profile_ID"] + "" != "") this._iProfile_ID = Convert.ToInt32(drList["Profile_ID"]);
                    else this._iProfile_ID = 0;

                    if (drList["MIFID_Risk"] + "" != "") this._iMiFID_Risk = Convert.ToInt32(drList["MIFID_Risk"]);
                    else this._iMiFID_Risk = 0;

                    foundRows = Global.dtCustomersProfiles.Select("ID = " + drList["Profile_ID"]);
                    if (foundRows.Length > 0)
                    {
                        this._sProfileTitle = foundRows[0]["Title"] + "";
                        this._iMiFID_Risk = Convert.ToInt32(foundRows[0]["MiFID_Risk"]);
                    }

                    this._iMiFID_2 = Convert.ToInt32(drList["MiFID_2"]);
                    this._dMiFID_2_StartDate = Convert.ToDateTime(drList["MiFID_2_StartDate"]);
                    this._iQuestionary_ID = Convert.ToInt32(drList["Questionary_ID"]);
                    this._iXAA = Convert.ToInt32(drList["XAA"]);
                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    this._sCode = drList["Code"] + "";
                    this._sPortfolio = drList["Portfolio"] + "";
                    this._sPortfolio_Alias = drList["Portfolio_Alias"] + "";
                    this._sPortfolio_Type = drList["Portfolio_Type"] + "";
                    this._sPackage_Title = drList["Package_Title"] + "";
                    this._klsPackages.Service_ID = Convert.ToInt32(drList["Service_ID"]);
                    this._dStart = Convert.ToDateTime(drList["DateStart"]);
                    this._dFinish = Convert.ToDateTime(drList["DateFinish"]);
                    this._sCurrency = drList["Currency"] + "";
                    this._sNumberAccount = drList["NumberAccount"] + "";
                    this._sL4 = drList["L4"] + "";
                    this._iService_ID = Convert.ToInt32(drList["PackageType_ID"]);
                    this._sService_Title = drList["Service_Title"] + "";
                    this._iBrokerageServiceProvider_ID = Convert.ToInt32(drList["BrokerageServiceProvider_ID"]);
                    this._sBrokerageServiceProvider_Title = drList["ServiceProviders_Title"] + "";
                    //this._sgVAT_FP = drList["VAT_FP");
                    //this._sgVAT_NP = drList["VAT_NP");
                    this._iStatus = Convert.ToInt32(drList["Status"]);

                    this._klsDetails.AgreementNotes = drList["AgreementNotes"] + "";
                    this._klsDetails.PerformanceFees = Convert.ToInt32(drList["PerformanceFees"]);
                    this._klsDetails.User1_ID = Convert.ToInt32(drList["User1_ID"]);
                    this._klsDetails.User2_ID = Convert.ToInt32(drList["User2_ID"]);
                    this._klsDetails.User3_ID = Convert.ToInt32(drList["User3_ID"]);
                    this._klsDetails.User4_ID = Convert.ToInt32(drList["User4_ID"]);
                    this._klsDetails.Surname = drList["Surname"] + "";
                    this._klsDetails.Firstname = drList["Firstname"] + "";
                    this._klsDetails.SurnameFather = drList["SurnameFather"] + "";
                    this._klsDetails.FirstnameFather = drList["FirstnameFather"] + "";
                    this._klsDetails.SurnameMother = drList["SurnameMother"] + "";
                    this._klsDetails.FirstnameMother = drList["FirstnameMother"] + "";
                    this._klsDetails.SurnameSizigo = drList["SurnameSizigo"] + "";
                    this._klsDetails.FirstnameSizigo = drList["FirstnameSizigo"] + "";
                    this._klsDetails.MIFIDCategory_ID = Convert.ToInt32(drList["MIFIDCategory_ID"]);
                    this._klsDetails.Division = Convert.ToInt32(drList["Division"]);
                    this._klsDetails.Brunch_ID = Convert.ToInt32(drList["Brunch_ID"]);
                    this._klsDetails.Spec_ID = Convert.ToInt32(drList["Spec_ID"]);
                    this._klsDetails.DoB = Convert.ToDateTime(drList["DoB"]);
                    this._klsDetails.BornPlace = drList["BornPlace"] + "";
                    this._klsDetails.Sex = drList["Sex"] + "";
                    this._klsDetails.Citizen_ID = Convert.ToInt32(drList["Citizen_ID"]);
                    this._klsDetails.ADT = drList["ADT"] + "";
                    this._klsDetails.ExpireDate = drList["ExpireDate"] + "";
                    this._klsDetails.Police = drList["Police"] + "";
                    this._klsDetails.DOY = drList["DOY"] + "";
                    this._klsDetails.AFM = drList["AFM"] + "";
                    this._klsDetails.AMKA = drList["AMKA"] + "";
                    this._klsDetails.CountryTaxes_ID = Convert.ToInt32(drList["CountryTaxes_ID"]);
                    this._klsDetails.Address = drList["Address"] + "";
                    this._klsDetails.City = drList["City"] + "";
                    this._klsDetails.Zip = drList["Zip"] + "";
                    this._klsDetails.Country_ID = Convert.ToInt32(drList["Country_ID"]);
                    this._klsDetails.Tel = drList["Tel"] + "";
                    this._klsDetails.Fax = drList["Fax"] + "";
                    this._klsDetails.Mobile = drList["Mobile"] + "";
                    this._klsDetails.SendSMS = Convert.ToInt32(drList["SendSMS"]);
                    this._klsDetails.EMail = drList["EMail"] + "";
                    this._klsDetails.InvName = drList["InvName"] + "";
                    this._klsDetails.InvAddress = drList["InvAddress"] + "";
                    this._klsDetails.InvCity = drList["InvCity"] + "";
                    this._klsDetails.InvZip = drList["InvZip"] + "";
                    this._klsDetails.InvCountry_ID = Convert.ToInt32(drList["InvCountry_ID"]);
                    this._klsDetails.Advisory_Name = (drList["AdvisorySurname"] + " " + drList["AdvisoryFirstname"]).Trim();
                    this._klsDetails.RM_Name = (drList["AdvisorySurname"] + " " + drList["AdvisoryFirstname"]).Trim();
                    this._klsDetails.Introducer_Name = (drList["AdvisorySurname"] + " " + drList["AdvisoryFirstname"]).Trim();
                    this._klsDetails.Diaxiristis_Name = (drList["AdvisorySurname"] + " " + drList["AdvisoryFirstname"]).Trim();
                    this._klsDetails.ConnectionMethod = Convert.ToInt32(drList["ConnectionMethod"]);
                    this._klsDetails.Merida = drList["Merida"] + "";
                    this._klsDetails.LogAxion = drList["LogAxion"] + "";

                    this._klsDetails.ChkComplex = Convert.ToInt32(drList["ChkComplex"]);
                    this._klsDetails.ChkWorld = Convert.ToInt32(drList["ChkWorld"]);
                    this._klsDetails.ChkGreece = Convert.ToInt32(drList["ChkGreece"]);
                    this._klsDetails.ChkEurope = Convert.ToInt32(drList["ChkEurope"]);
                    this._klsDetails.ChkAmerica = Convert.ToInt32(drList["ChkAmerica"]);
                    this._klsDetails.ChkAsia = Convert.ToInt32(drList["ChkAsia"]);
                    this._klsDetails.IncomeProducts = drList["IncomeProducts"] + "";
                    this._klsDetails.CapitalProducts = drList["CapitalProducts"] + "";
                    this._klsDetails.ChkSpecificConstraints = Convert.ToInt32(drList["ChkSpecificConstraints"]);
                    this._klsDetails.ChkMonetaryRisk = Convert.ToInt32(drList["ChkMonetaryRisk"]);
                    this._klsDetails.ChkIndividualBonds = Convert.ToInt32(drList["ChkIndividualBonds"]);
                    this._klsDetails.ChkMutualFunds = Convert.ToInt32(drList["ChkMutualFunds"]);
                    this._klsDetails.ChkBondedETFs = Convert.ToInt32(drList["ChkBondedETFs"]);
                    this._klsDetails.ChkIndividualShares = Convert.ToInt32(drList["ChkIndividualShares"]);
                    this._klsDetails.ChkMixedFunds = Convert.ToInt32(drList["ChkMixedFunds"]);
                    this._klsDetails.ChkMixedETFs = Convert.ToInt32(drList["ChkMixedETFs"]);
                    this._klsDetails.ChkFunds = Convert.ToInt32(drList["ChkFunds"]);
                    this._klsDetails.ChkETFs = Convert.ToInt32(drList["ChkETFs"]);
                    this._klsDetails.ChkInvestmentGrade = Convert.ToInt32(drList["ChkInvestmentGrade"]);
                    this._klsDetails.MiscInstructions = drList["MiscInstructions"] + "";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { drList.Close(); conn.Close(); }
        }
        public void GetRecord_Date()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetClientContracts_Date", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Contract_ID", _iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@AktionDate", _dAktionDate));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _iContracts_Packages_ID = Convert.ToInt32(drList["CFP_ID"]);
                    _sContractTitle = drList["PackageTitle"] + "   ver. " + drList["PackageVersion"];
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { drList.Close(); conn.Close(); }
        }
        public void GetRecordFX_Date()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetCommands_FX_ClientPackage", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ServiceProvider_ID", _iServiceProvider_ID));
                cmd.Parameters.Add(new SqlParameter("@AktionDate", _dAktionDate));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _iRecord_ID = Convert.ToInt32(drList["ID"]);
                    _sCode = drList["Code"] + "";
                    _sPortfolio = drList["Portfolio"] + "";
                    _iContracts_Details_ID = Convert.ToInt32(drList["Contracts_Details_ID"]);
                    _iContracts_Packages_ID = Convert.ToInt32(drList["Contracts_Packages_ID"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { drList.Close(); conn.Close(); }
        }
        public void GetRecordFX_Fees()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetServiceProviderFXFees_ClientPackage", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Contract_ID", _iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@DateFees", _dAktionDate));
                cmd.Parameters.Add(new SqlParameter("@Amount", _fltAmount));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _fltCompanyFeesPercent = Convert.ToSingle(drList["RetrosessionCompany"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { drList.Close(); conn.Close(); }
        }
        public void GetList()
        {
            string _sSurnameGreek = "", _sSurnameEnglish = "";
            Global.TranslateUserName(_sClientName, out _sSurnameGreek, out _sSurnameEnglish);

            _dtList = new DataTable();
            dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contracts_Details_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contracts_Packages_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("CDP_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("DateStart", Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("DateFinish", Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("Tipos", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ContractTitle", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Client_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ClientName", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Client_Category", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("IsMaster", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("NumberAccount", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Code", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Portfolio", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Portfolio_Alias", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Portfolio_Type", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("PackageTitle", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("PackageVersion", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("MiFID_2", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("MIFID_2_StartDate", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("CFP_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Address", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Zip", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("City", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("CountryHome_Title", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("CountryTaxes_Title", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Country_Title", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("CountryCitizen_Title", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Email", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Division_Title", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Spec_Title", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("SpecialCategory", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Risk", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Pack_DateStart", Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("Pack_DateFinish", Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("Currency", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Service_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Service_Title", Type.GetType("System.String"));
            //dtCol = _dtList.Columns.Add("SuggestedInvestmentPolicy_Title", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("InvestmentProfile_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("InvestmentProfile_Title", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("InvestmentPolicy_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("InvestmentPolicy_Title", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("SuggestedFinanceTool_Title", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("FinanceTool_Title", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("AdvisorName", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("AdvisorStatus", Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("RMName", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("RMStatus", Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("IntroName", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("IntroStatus", Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("DiaxName", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("DiaxStatus", Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("AgreementNotes", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("PackageProvider_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ServiceProvider_Title", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("BrokerageServiceProvider_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("BrokerageServiceProvider_Title", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("RTOServiceProvider_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("RTOServiceProvider_Title", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("AdvisoryServiceProvider_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("AdvisoryServiceProvider_Title", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("CustodyServiceProvider_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("CustodyServiceProvider_Title", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("AdminServiceProvider_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("AdminServiceProvider_Title", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("DiscretServiceProvider_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("DiscretServiceProvider_Title", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("DealAdvisoryServiceProvider_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("DealAdvisoryServiceProvider_Title", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("User1_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("User2_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("User3_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("User4_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Status", Type.GetType("System.Int32"));

            dtCol = _dtList.Columns.Add("ContractEMail", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ContractMobile", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ContractTel", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ContractFax", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ConnectionMethod", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("MIFID_Risk_Index", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("MIFIDCategory_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("MasterFullName", Type.GetType("System.String"));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetContracts_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PackageType", _iPackageType));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dStart));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dFinish));
                cmd.Parameters.Add(new SqlParameter("@Client_ID", _iClient_ID));
                cmd.Parameters.Add(new SqlParameter("@Advisor_ID", _iAdvisor_ID));
                cmd.Parameters.Add(new SqlParameter("@Service_ID", _iService_ID));
                cmd.Parameters.Add(new SqlParameter("@SurnameGreek", "%" + _sSurnameGreek + "%"));
                cmd.Parameters.Add(new SqlParameter("@SurnameEnglish", "%" + _sSurnameEnglish + "%"));
                cmd.Parameters.Add(new SqlParameter("@ServiceProvider_ID", _iServiceProvider_ID));
                cmd.Parameters.Add(new SqlParameter("@Division", Global.Division));
                cmd.Parameters.Add(new SqlParameter("@DivisionFilter ", Global.DivisionFilter));
                cmd.Parameters.Add(new SqlParameter("@Status", _iStatus));
                cmd.Parameters.Add(new SqlParameter("@ClientStatus", _iClientStatus));
                drList = cmd.ExecuteReader();

                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["Contracts_Details_ID"] = drList["Contracts_Details_ID"];
                    dtRow["Contracts_Packages_ID"] = drList["Contracts_Packages_ID"];
                    dtRow["CDP_ID"] = drList["CDP_ID"];
                    dtRow["Tipos"] = drList["Tipos"];
                    dtRow["DateStart"] = drList["DateStart"];
                    dtRow["DateFinish"] = drList["DateFinish"];
                    dtRow["ContractTitle"] = drList["ContractTitle"];
                    dtRow["Client_ID"] = drList["Client_ID"];
                    dtRow["ClientName"] = (drList["ClientSurname"] + " " + drList["ClientFirstname"]).Trim();
                    if (Convert.ToInt32(drList["Category"]) < 2)
                        dtRow["Client_Category"] = Convert.ToInt32(drList["Category"]) == 0 ? "Φυσικό πρόσωπο" : "Νομικό πρόσωπο";
                    else dtRow["Client_Category"] = "";
                    dtRow["IsMaster"] = drList["IsMaster"];
                    dtRow["Code"] = drList["Code"] + "";
                    dtRow["NumberAccount"] = drList["NumberAccount"] + "";
                    dtRow["Portfolio"] = drList["Portfolio"] + "";
                    dtRow["Portfolio_Alias"] = drList["Portfolio_Alias"] + "";
                    dtRow["Portfolio_Type"] = drList["Portfolio_Type"] + "";
                    dtRow["PackageTitle"] = drList["PackageTitle"] + "";
                    dtRow["PackageVersion"] = drList["PackageVersion"] + "";
                    dtRow["MIFID_2"] = drList["MIFID_2"];
                    dtRow["MIFID_2_StartDate"] = drList["MIFID_2_StartDate"];
                    dtRow["CFP_ID"] = drList["CFP_ID"];
                    dtRow["Address"] = drList["Address"] + "";
                    dtRow["Zip"] = drList["Zip"] + "";
                    dtRow["City"] = drList["City"] + "";
                    dtRow["CountryHome_Title"] = drList["CountryHome_Title"] + "";
                    dtRow["CountryTaxes_Title"] = drList["CountryTaxes_Title"] + "";
                    dtRow["CountryCitizen_Title"] = drList["CountryCitizen_Title"] + "";
                    dtRow["Country_Title"] = drList["Country_Title"] + "";
                    dtRow["Email"] = drList["Email"] + "";
                    dtRow["Division_Title"] = drList["Division_Title"] + "";
                    dtRow["Spec_Title"] = drList["Spec_Title"] + "";
                    dtRow["SpecialCategory"] = drList["SpecialCategory"];
                    dtRow["Risk"] = drList["Risk"];
                    dtRow["Pack_DateStart"] = drList["Pack_DateStart"];
                    dtRow["Pack_DateFinish"] = drList["Pack_DateFinish"];
                    dtRow["Currency"] = drList["Currency"] + "";
                    dtRow["Service_ID"] = drList["Service_ID"];
                    dtRow["Service_Title"] = drList["Service_Title"];
                    //dtRow["SuggestedInvestmentPolicy_Title"] = drList["SuggestedInvestmentPolicy_Title"] + "";
                    dtRow["InvestmentProfile_ID"] = drList["InvestmentProfile_ID"];
                    dtRow["InvestmentProfile_Title"] = drList["Profile_Title"];
                    dtRow["InvestmentPolicy_ID"] = drList["InvestmentPolicy_ID"];
                    dtRow["InvestmentPolicy_Title"] = drList["InvestmentPolicy_Title"];
                    //dtRow["SuggestedFinanceTool_Title"] = drList["SuggestedFinanceTool_Title"] + "";
                    dtRow["AdvisorName"] = drList["AdvisorName"] + "";
                    dtRow["AdvisorStatus"] = drList["AdvisorStatus"];
                    dtRow["RMName"] = drList["RMName"] + "";
                    dtRow["RMStatus"] = drList["RMStatus"];
                    dtRow["IntroName"] = drList["IntroName"] + "";
                    dtRow["IntroStatus"] = drList["IntroStatus"];
                    dtRow["DiaxName"] = drList["DiaxName"] + "";
                    dtRow["DiaxStatus"] = drList["DiaxStatus"];
                    dtRow["AgreementNotes"] = drList["AgreementNotes"] + "";
                    dtRow["PackageProvider_ID"] = drList["PackageProvider_ID"];
                    dtRow["ServiceProvider_Title"] = drList["ServiceProvider_Title"] + "";
                    dtRow["BrokerageServiceProvider_ID"] = drList["BrokerageServiceProvider_ID"];
                    dtRow["BrokerageServiceProvider_Title"] = drList["BrokerageServiceProvider_Title"] + "";
                    dtRow["RTOServiceProvider_ID"] = drList["RTOServiceProvider_ID"];
                    dtRow["RTOServiceProvider_Title"] = drList["RTOServiceProvider_Title"] + "";
                    dtRow["AdvisoryServiceProvider_ID"] = drList["AdvisoryServiceProvider_ID"];
                    dtRow["AdvisoryServiceProvider_Title"] = drList["AdvisoryServiceProvider_Title"] + "";
                    dtRow["CustodyServiceProvider_ID"] = drList["CustodyServiceProvider_ID"];
                    dtRow["CustodyServiceProvider_Title"] = drList["CustodyServiceProvider_Title"] + "";
                    dtRow["AdminServiceProvider_ID"] = drList["AdministrationServiceProvider_ID"];
                    dtRow["AdminServiceProvider_Title"] = drList["AdminServiceProvider_Title"] + "";
                    dtRow["DiscretServiceProvider_ID"] = drList["DiscretServiceProvider_ID"];
                    dtRow["DiscretServiceProvider_Title"] = drList["DiscretServiceProvider_Title"] + "";
                    if (drList["DealAdvisoryServiceProvider_ID"] + "" != "")
                    {
                        dtRow["DealAdvisoryServiceProvider_ID"] = drList["DealAdvisoryServiceProvider_ID"];
                        dtRow["DealAdvisoryServiceProvider_Title"] = drList["DealAdvisoryServiceProvider_Title"] + "";
                    }
                    else
                    {
                        dtRow["DealAdvisoryServiceProvider_ID"] = 0;
                        dtRow["DealAdvisoryServiceProvider_Title"] = "";
                    }
                    dtRow["User1_ID"] = drList["User1_ID"];
                    dtRow["User2_ID"] = drList["User2_ID"];
                    dtRow["User3_ID"] = drList["User3_ID"];
                    dtRow["User4_ID"] = drList["User4_ID"];
                    dtRow["Status"] = drList["Status"];

                    dtRow["ContractEMail"] = drList["EMail"] + "";
                    dtRow["ContractMobile"] = drList["Mobile"] + "";
                    dtRow["ContractTel"] = drList["Tel"] + "";
                    dtRow["ContractFax"] = drList["Fax"] + "";
                    dtRow["ConnectionMethod"] = "";                    
                    dtRow["MIFID_Risk_Index"] = drList["MIFID_Risk"];
                    dtRow["MIFIDCategory_ID"] = drList["MIFIDCategory_ID"];
                    dtRow["MasterFullName"] = (drList["MasterSurname"] + " " + drList["MasterFirstname"]).Trim();

                    _dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList_Provider_ID()
        {
            _dtList = new DataTable();
            dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contracts_Details_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contracts_Packages_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ContractTitle", Type.GetType("System.String"));           
            dtCol = _dtList.Columns.Add("Code", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Portfolio", Type.GetType("System.String"));           
            dtCol = _dtList.Columns.Add("ServiceProvider_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ServiceProvider_Title", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("BestExecution", Type.GetType("System.Int32"));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetContracts_Provider_ID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PackageType", _iPackageType));
                cmd.Parameters.Add(new SqlParameter("@ServiceProvider_ID", _iServiceProvider_ID));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dStart));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dFinish));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["Contracts_Details_ID"] = drList["Contracts_Details_ID"];
                    dtRow["Contracts_Packages_ID"] = drList["Contracts_Packages_ID"];                   
                    dtRow["ContractTitle"] = drList["ContractTitle"];                 
                    dtRow["Code"] = drList["Code"] + "";
                    dtRow["Portfolio"] = drList["Portfolio"] + "";            
                    dtRow["ServiceProvider_ID"] = drList["ServiceProvider_ID"];
                    dtRow["ServiceProvider_Title"] = drList["ServiceProvider_Title"] + "";
                    dtRow["BestExecution"] = drList["BestExecution"];
                    _dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetCashList()
        {
            _dtList = new DataTable();
            dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Tipos", System.Type.GetType("System.Int16")); ;
            dtCol = _dtList.Columns.Add("Category", System.Type.GetType("System.Int16")); ;
            dtCol = _dtList.Columns.Add("Fullname", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Surname", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Firstname", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("EMail", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Mobile", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ServiceProvider_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ServiceProvider_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ServiceProvider_Type", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ClientStatus", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Package_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ContractEMail", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ContractMobile", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("CFP_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("CDP_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contract_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contracts_Details_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contracts_Packages_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ContractType", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Package_DateStart", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("Package_DateFinish", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("Option_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("InvestmentProfile_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("InvestmentProfile_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("InvestmentPolicy_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("InvestmentPolicy_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Service_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Service_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("NumberAccount", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("AUM", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Is_InfluenceCenter", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("Is_Introducer", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("Is_RepresentPerson", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("IsMaster", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("DependentPersons", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("Spec_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("SpecialTitle", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("RM_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("User1_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("User2_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("User4_ID", System.Type.GetType("System.Int32"));                   // User4_ID - Diaxiristis
            dtCol = _dtList.Columns.Add("RM_Step", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("BO_Step", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("Conne", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("MIFID_Risk_Index", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("MIFIDCategory_ID", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("MIFID_2", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("XAA", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("VAT_Percent", System.Type.GetType("System.Single"));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetContracts_CashList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ClientFilter", _sClientsFilter));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];                                              // Client_ID
                    dtRow["Client_ID"] = drList["ID"];
                    dtRow["ClientStatus"] = drList["Status"];
                    dtRow["Tipos"] = drList["Tipos"];
                    dtRow["Category"] = drList["Category"];
                    if (Convert.ToInt32(drList["Tipos"]) == 1) dtRow["Fullname"] = drList["Surname"] + " " + drList["Firstname"];
                    else dtRow["Fullname"] = drList["Surname"];
                    dtRow["Surname"] = drList["Surname"] + "";
                    dtRow["Firstname"] = drList["Firstname"] + "";
                    dtRow["EMail"] = drList["EMail"] + "";
                    dtRow["Mobile"] = drList["Mobile"] + "";
                    dtRow["Code"] = drList["Code"] + "";
                    dtRow["Portfolio"] = drList["SubCode"] + "";
                    if ((drList["ServiceProvider_ID"] + "") == "") {
                        dtRow["ServiceProvider_ID"] = 0;
                        dtRow["ServiceProvider_Title"] = "";          // Service Provider Title
                        dtRow["ServiceProvider_Type"] = 0;            // Service Provider Type: 1 - CreditSuisse, 2 - HF2S, 3 - Intesa
                    }
                    else { 
                       dtRow["ServiceProvider_ID"] = drList["ServiceProvider_ID"];     // ServiceProvider_ID = Provider of Package Services 
                       dtRow["ServiceProvider_Title"] = drList["Title"] + "";          // Service Provider Title
                       dtRow["ServiceProvider_Type"] = drList["ProviderType"];         // Service Provider Type: 1 - CreditSuisse, 2 - HF2S, 3 - Intesa
                    }
                    dtRow["Package_Title"] = drList["PackageTitle"] + "";
                    dtRow["CDP_ID"] = drList["CDP_ID"];
                    dtRow["Contract_ID"] = drList["Contract_ID"];
                    dtRow["Contracts_Details_ID"] = drList["Contracts_Details_ID"];
                    dtRow["Contracts_Packages_ID"] = drList["Contracts_Packages_ID"];
                    dtRow["ContractType"] = drList["ContractType"];
                    dtRow["ContractTitle"] = drList["ContractTitle"] + "";
                    dtRow["CFP_ID"] = drList["CFP_ID"];
                    dtRow["ContractEMail"] = drList["ContractEMail"] + "";
                    dtRow["ContractMobile"] = drList["ContractMobile"] + "";
                    dtRow["Package_DateStart"] = drList["DateStart"];
                    dtRow["Package_DateFinish"] = drList["DateFinish"];
                    dtRow["Option_ID"] = drList["Option_ID"];
                    dtRow["InvestmentProfile_ID"] = ((drList["InvestmentProfile_ID"] + "") == "" ? 0 : drList["InvestmentProfile_ID"]);
                    dtRow["InvestmentProfile_Title"] = drList["InvestmentProfile_Title"];
                    dtRow["InvestmentPolicy_ID"] = ((drList["InvestmentPolicy_ID"] + "") == "" ? 0 : drList["InvestmentPolicy_ID"]);
                    dtRow["InvestmentPolicy_Title"] = drList["InvestmentPolicy_Title"];
                    dtRow["Service_ID"] = (((drList["Service_ID"] + "") == "") ? 0 : drList["Service_ID"]);
                    dtRow["Service_Title"] = drList["ServiceTitle"] + "";
                    dtRow["NumberAccount"] = drList["NumberAccount"] + "";
                    dtRow["AUM"] = 0;
                    dtRow["Status"] = drList["ContractStatus"];
                    dtRow["Currency"] = drList["Currency"];
                    dtRow["Is_InfluenceCenter"] = drList["Is_InfluenceCenter"];
                    dtRow["Is_Introducer"] = drList["Is_Introducer"];
                    dtRow["Is_RepresentPerson"] = drList["Is_RepresentPerson"];
                    dtRow["IsMaster"] = drList["IsMaster"];
                    dtRow["DependentPersons"] = drList["DependentPersons"];
                    dtRow["Spec_ID"] = drList["Spec_ID"];
                    dtRow["SpecialTitle"] = drList["SpecialTitle"] + "";
                    if (drList["RM_ID"].ToString() != "") dtRow["RM_ID"] = drList["RM_ID"];
                    else dtRow["RM_ID"] = drList["User2_ID"];
                    dtRow["User1_ID"] = drList["User1_ID"];
                    dtRow["User2_ID"] = drList["User2_ID"];
                    dtRow["User4_ID"] = drList["User4_ID"];
                    dtRow["RM_Step"] = drList["RM_Step"];
                    dtRow["BO_Step"] = drList["BO_Step"];
                    dtRow["Conne"] = drList["Conne"] + "";
                    if (drList["MIFID_Risk"].ToString() != "") dtRow["MIFID_Risk_Index"] = drList["MIFID_Risk"];
                    else dtRow["MIFID_Risk_Index"] = 0;
                    dtRow["MIFIDCategory_ID"] = drList["MIFIDCategory_ID"];
                    dtRow["MIFID_2"] = drList["MIFID_2"];
                    dtRow["XAA"] = drList["XAA"];
                    dtRow["VAT_Percent"] = drList["VAT_Percent"];
                    _dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetActualList()
        {
            _dtList = new DataTable("ActualContractsList");
            dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contracts_Details_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contracts_Packages_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ClientType", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("User1Name", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("PackageTitle", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("MiFID_2", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("MIFID_2_StartDate", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("CFP_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("DateStart", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("DateFinish", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Service_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Service_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("InvestmentProfile_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("InvestmentProfile_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("InvestmentPolicy_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("InvestmentPolicy_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("AdvisorName", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("RMName", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("IntroName", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("DiaxName", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("AgreementNotes", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("AUM", System.Type.GetType("System.Decimal"));
            dtCol = _dtList.Columns.Add("VATPercent", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("PackageProvider_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("BrokerageServiceProvider_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("RTOServiceProvider_ID", System.Type.GetType("System.Int32"));

            dtCol = _dtList.Columns.Add("AdvisoryServiceProvider_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("AdvisoryAllManFees", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Advisory_AmoiviPro", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("AdvisoryDiscount_DateFrom", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("AdvisoryDiscount_DateTo", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("Advisory_Discount_Percent", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("Advisory_AmoiviAfter", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("Advisory_Climakas", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Advisory_MonthMinAmount", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("AdvisoryMonth3_Discount", System.Type.GetType("System.Single"));

            dtCol = _dtList.Columns.Add("DiscretServiceProvider_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("DiscretAllManFees", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Discret_AmoiviPro", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("DiscretDiscount_DateFrom", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("DiscretDiscount_DateTo", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("Discret_Discount_Percent", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("Discret_AmoiviAfter", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("Discret_Climakas", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Discret_MonthMinAmount", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("DiscretMonth3_Discount", System.Type.GetType("System.Single"));

            dtCol = _dtList.Columns.Add("DealAdvisoryServiceProvider_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("DealAdvisoryFeesAmount", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("DealAdvisoryFees_Discount", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("DealAdvisoryFees", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("DealAdvisory_MonthMinAmount", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("DealAdvisoryMonth3_Discount", System.Type.GetType("System.Single"));

            dtCol = _dtList.Columns.Add("AdminServiceProvider_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("AdminFeesPercent", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("AdminFees_Discount", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("AdminFees", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("Admin_MonthMinAmount", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("AdminMonth3_Discount", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("AdminMonth3_Fees", System.Type.GetType("System.Single"));

            dtCol = _dtList.Columns.Add("User1_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("User2_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("User3_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("User4_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Address", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("City", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Zip", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Country_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("DOY", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("AFM", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int32"));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetActualContracts_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PackageType", _iPackageType));
                cmd.Parameters.Add(new SqlParameter("@DateStart", _dStart));
                cmd.Parameters.Add(new SqlParameter("@DateFinish", _dFinish));
                cmd.Parameters.Add(new SqlParameter("@Provider_ID", _iPackageProvider_ID));
                cmd.Parameters.Add(new SqlParameter("@Client_ID", _iClient_ID));
                cmd.Parameters.Add(new SqlParameter("@Advisor_ID", _iAdvisor_ID));
                cmd.Parameters.Add(new SqlParameter("@Service_ID", _iService_ID));
                cmd.Parameters.Add(new SqlParameter("@Status", _iStatus));
                drList = cmd.ExecuteReader();

                while (drList.Read())
                {
                    //if (Convert.ToInt32(drList["ID"]) == 4164)
                    //    _sL4 = _sL4;
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["Contracts_Details_ID"] = drList["Contract_Details_ID"];
                    dtRow["Contracts_Packages_ID"] = drList["Contract_Package_ID"];
                    dtRow["ContractTitle"] = drList["ContractTitle"];
                    dtRow["Client_ID"] = drList["Client_ID"];
                    dtRow["ClientType"] = drList["ClientType"];
                    dtRow["Code"] = drList["Code"] + "";
                    dtRow["Portfolio"] = drList["Portfolio"] + "";
                    dtRow["User1Name"] = drList["ClientSurname"] + " " + drList["ClientFirstname"];
                    dtRow["PackageTitle"] = drList["PackageTitle"] + "";
                    dtRow["MIFID_2"] = drList["MIFID_2"];
                    dtRow["MIFID_2_StartDate"] = drList["MIFID_2_StartDate"];
                    dtRow["CFP_ID"] = drList["CFP_ID"];
                    dtRow["Service_ID"] = drList["Service_ID"];
                    dtRow["Service_Title"] = drList["Service_Title"];
                    dtRow["InvestmentProfile_ID"] = drList["InvestmentProfile_ID"];
                    dtRow["InvestmentProfile_Title"] = drList["Profile_Title"];
                    dtRow["InvestmentPolicy_ID"] = drList["InvestmentPolicy_ID"];
                    dtRow["InvestmentPolicy_Title"] = drList["Policy_Title"];
                    dtRow["DateStart"] = drList["Pack_DateStart"];
                    dtRow["DateFinish"] = drList["Pack_DateFinish"];
                    dtRow["Currency"] = drList["Currency"] + "";
                    dtRow["AdvisorName"] = drList["AdvisorName"] + "";
                    dtRow["RMName"] = drList["RMName"] + "";
                    dtRow["PackageProvider_Title"] = drList["PackageProvider_Title"] + "";
                    dtRow["AUM"] = 0;
                    dtRow["VATPercent"] = drList["VAT_Percent"];
                    dtRow["BrokerageServiceProvider_ID"] = "0" + drList["BrokerageServiceProvider_ID"];

                    dtRow["AdvisoryServiceProvider_ID"] = drList["AdvisoryServiceProvider_ID"];
                    dtRow["AdvisoryAllManFees"] = drList["AdvisoryAllManFees"];
                    dtRow["Advisory_AmoiviPro"] = drList["Advisory_FeesPercent"];
                    if (drList["AdvisoryFees"] + "" != "")
                    {
                        dtRow["AdvisoryDiscount_DateFrom"] = drList["AdvisoryDiscount_DateFrom"];
                        dtRow["AdvisoryDiscount_DateTo"] = drList["AdvisoryDiscount_DateTo"];
                        dtRow["Advisory_Discount_Percent"] = drList["AdvisoryFees_Discount"];
                        dtRow["Advisory_AmoiviAfter"] = drList["AdvisoryFees"];
                    }
                    else
                    {
                        dtRow["AdvisoryDiscount_DateFrom"] = "1900/01/01";
                        dtRow["AdvisoryDiscount_DateTo"] = "2070/12/31";
                        dtRow["Advisory_Discount_Percent"] = 0;
                        dtRow["Advisory_AmoiviAfter"] = drList["Advisory_FeesPercent"];
                    }

                    if (Convert.ToDateTime(dtRow["AdvisoryDiscount_DateFrom"]) < _dFinish && Convert.ToDateTime(dtRow["AdvisoryDiscount_DateTo"]) > _dStart)
                    {
                        if (drList["AdvisoryFees_Discount"] + "" != "") dtRow["Advisory_Discount_Percent"] = drList["AdvisoryFees_Discount"];
                        else dtRow["Advisory_Discount_Percent"] = 0;

                        if (drList["AdvisoryFees"] + "" != "") dtRow["Advisory_AmoiviAfter"] = drList["AdvisoryFees"];
                        else dtRow["Advisory_AmoiviAfter"] = drList["Advisory_FeesPercent"];
                    }
                    else
                    {
                        dtRow["Advisory_Discount_Percent"] = 0;
                        dtRow["Advisory_AmoiviAfter"] = dtRow["Advisory_AmoiviPro"];
                    }

                    dtRow["Advisory_Climakas"] = drList["AdvisoryAllManFees"] + "";

                    if (drList["Advisory_MonthMinAmount"] + "" != "") dtRow["Advisory_MonthMinAmount"] = drList["Advisory_MonthMinAmount"];
                    else dtRow["Advisory_MonthMinAmount"] = 0;

                    if (drList["AdvisoryMonth3_Discount"] + "" != "") dtRow["AdvisoryMonth3_Discount"] = drList["AdvisoryMonth3_Discount"];
                    else dtRow["AdvisoryMonth3_Discount"] = 0;

                    dtRow["DiscretServiceProvider_ID"] = drList["DiscretServiceProvider_ID"];
                    dtRow["DiscretAllManFees"] = drList["DiscretAllManFees"];
                    dtRow["Discret_AmoiviPro"] = drList["Discret_FeesPercent"];
                    if (drList["DiscretFees"] + "" != "")
                    {
                        dtRow["DiscretDiscount_DateFrom"] = drList["DiscretDiscount_DateFrom"];
                        dtRow["DiscretDiscount_DateTo"] = drList["DiscretDiscount_DateTo"];
                        dtRow["Discret_Discount_Percent"] = drList["DiscretFees_Discount"];
                        dtRow["Discret_AmoiviAfter"] = drList["DiscretFees"];
                    }
                    else
                    {
                        dtRow["DiscretDiscount_DateFrom"] = "1900/01/01";
                        dtRow["DiscretDiscount_DateTo"] = "2070/12/31";
                        dtRow["Discret_Discount_Percent"] = 0;
                        dtRow["Discret_AmoiviAfter"] = drList["Discret_FeesPercent"];
                    }

                    if (Convert.ToDateTime(dtRow["DiscretDiscount_DateFrom"]) < _dFinish && Convert.ToDateTime(dtRow["DiscretDiscount_DateTo"]) > _dStart)
                    {
                        if (drList["DiscretFees_Discount"] + "" != "") dtRow["Discret_Discount_Percent"] = drList["DiscretFees_Discount"];
                        else dtRow["Discret_Discount_Percent"] = 0;

                        if (drList["DiscretFees"] + "" != "") dtRow["Discret_AmoiviAfter"] = drList["DiscretFees"];
                        else dtRow["Discret_AmoiviAfter"] = drList["Discret_FeesPercent"];
                    }
                    else
                    {
                        dtRow["Discret_Discount_Percent"] = 0;
                        dtRow["Discret_AmoiviAfter"] = dtRow["Discret_AmoiviPro"];
                    }

                    dtRow["Discret_Climakas"] = drList["DiscretAllManFees"] + "";

                    if (drList["Discret_MonthMinAmount"] + "" != "") dtRow["Discret_MonthMinAmount"] = drList["Discret_MonthMinAmount"];
                    else dtRow["Discret_MonthMinAmount"] = 0;

                    if (drList["DiscretMonth3_Discount"] + "" != "") dtRow["DiscretMonth3_Discount"] = drList["DiscretMonth3_Discount"];
                    else dtRow["DiscretMonth3_Discount"] = 0;


                    //--------- DealAdvisory ---------------------------------------------------------------
                    if (drList["DealAdvisoryServiceProvider_ID"] + "" != "") dtRow["DealAdvisoryServiceProvider_ID"] = drList["DealAdvisoryServiceProvider_ID"];
                    else dtRow["DealAdvisoryServiceProvider_ID"] = 0;

                    if (drList["DealAdvisoryFeesAmount"] + "" != "") dtRow["DealAdvisoryFeesAmount"] = drList["DealAdvisoryFeesAmount"];
                    else dtRow["DealAdvisoryFeesAmount"] = 0;

                    if (drList["DealAdvisoryFees_Discount"] + "" != "") dtRow["DealAdvisoryFees_Discount"] = drList["DealAdvisoryFees_Discount"];
                    else dtRow["DealAdvisoryFees_Discount"] = 0;

                    if (drList["DealAdvisoryFees"] + "" != "") dtRow["DealAdvisoryFees"] = drList["DealAdvisoryFees"];
                    else dtRow["DealAdvisoryFees"] = 0;

                    if (drList["DealAdvisory_MonthMinAmount"] + "" != "") dtRow["DealAdvisory_MonthMinAmount"] = drList["DealAdvisory_MonthMinAmount"];
                    else dtRow["DealAdvisory_MonthMinAmount"] = 0;

                    if (drList["DealAdvisoryMonth3_Discount"] + "" != "") dtRow["DealAdvisoryMonth3_Discount"] = drList["DealAdvisoryMonth3_Discount"];
                    else dtRow["DealAdvisoryMonth3_Discount"] = 0;

                    //--------------- Admin Fees ----------------------------------------------------------------
                    dtRow["AdminServiceProvider_ID"] = drList["AdministrationServiceProvider_ID"];
                    dtRow["AdminFeesPercent"] = drList["AdminFeesPercent"];
                    dtRow["AdminFees_Discount"] = drList["AdminFees_Discount"];
                    dtRow["AdminFees"] = drList["AdminFees"];
                    dtRow["Admin_MonthMinAmount"] = drList["Admin_MonthMinAmount"];
                    dtRow["AdminMonth3_Discount"] = drList["AdminMonth3_Discount"];
                    dtRow["AdminMonth3_Fees"] = drList["AdminMonth3_Fees"];
                    dtRow["User1_ID"] = drList["User1_ID"];
                    dtRow["User2_ID"] = drList["User2_ID"];
                    dtRow["User3_ID"] = drList["User3_ID"];
                    dtRow["User4_ID"] = drList["User4_ID"];
                    dtRow["Address"] = drList["InvAddress"];
                    dtRow["City"] = drList["InvCity"];
                    dtRow["Zip"] = drList["InvZip"];
                    dtRow["Country_ID"] = drList["InvCountry_ID"];
                    dtRow["DOY"] = drList["InvDOY"];
                    dtRow["AFM"] = drList["InvAFM"];
                    dtRow["Status"] = drList["Status"];
                    _dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetMiFID2List()
        {
            _dtList = new DataTable("ContractsMiFID2List");
            dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contract_Details_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contract_Packages_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ClientType", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("User1Name", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("PackageTitle", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("CFP_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("DateStart", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("DateFinish", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Service_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Service_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("InvestmentProfile_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("InvestmentPolicy_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("InvestmentPolicy_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("AdvisorName", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("RMName", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("IntroName", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("DiaxName", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("AgreementNotes", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("VATPercent", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("PackageProvider_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("BrokerageServiceProvider_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("RTOServiceProvider_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("AdvisoryServiceProvider_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("AdvisoryAllManFees", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Advisory_AmoiviPro", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("AdvisoryDiscount_DateFrom", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("AdvisoryDiscount_DateTo", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("Advisory_Discount_Percent", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("Advisory_AmoiviAfter", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("Advisory_Climakas", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Advisory_MonthMinAmount", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("AdvisoryMonth3_Discount", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("DiscretServiceProvider_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Discret_AmoiviPro", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("DiscretDiscount_DateFrom", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("DiscretDiscount_DateTo", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("Discret_Discount_Percent", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("Discret_AmoiviAfter", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("Discret_Climakas", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Discret_MonthMinAmount", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("DiscretMonth3_Discount", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("AdminServiceProvider_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("AdminFeesPercent", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("AdminFees_Discount", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("AdminFees", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("Admin_MonthMinAmount", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("AdminMonth3_Discount", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("AdminMonth3_Fees", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("DealAdvisoryServiceProvider_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("DealAdvisoryFeesAmount", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("DealAdvisoryFees_Discount", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("DealAdvisoryFees", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("DealAdvisory_MonthMinAmount", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("DealAdvisoryMonth3_Discount", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("User1_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("User2_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("User3_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("User4_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Address", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("City", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Zip", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Country_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("DOY", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("AFM", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int32"));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetMiFID2Contracts_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PackageType", _iPackageType));
                cmd.Parameters.Add(new SqlParameter("@DateStart", _dStart));
                cmd.Parameters.Add(new SqlParameter("@DateFinish", _dFinish));
                cmd.Parameters.Add(new SqlParameter("@Provider_ID", _iPackageProvider_ID));
                cmd.Parameters.Add(new SqlParameter("@Client_ID", _iClient_ID));
                cmd.Parameters.Add(new SqlParameter("@Advisor_ID", _iAdvisor_ID));
                cmd.Parameters.Add(new SqlParameter("@Service_ID", _iService_ID));
                cmd.Parameters.Add(new SqlParameter("@Status", _iStatus));
                drList = cmd.ExecuteReader();

                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["Contract_Details_ID"] = drList["Contract_Details_ID"];
                    dtRow["Contract_Packages_ID"] = drList["Contract_Package_ID"];
                    dtRow["ContractTitle"] = drList["ContractTitle"];
                    dtRow["Client_ID"] = drList["Client_ID"];
                    dtRow["ClientType"] = drList["ClientType"];
                    dtRow["Code"] = drList["Code"] + "";
                    dtRow["Portfolio"] = drList["Portfolio"] + "";
                    dtRow["User1Name"] = drList["ClientSurname"] + " " + drList["ClientFirstname"];
                    dtRow["PackageTitle"] = drList["PackageTitle"] + "";
                    dtRow["CFP_ID"] = drList["CFP_ID"];
                    dtRow["Service_ID"] = drList["Service_ID"];
                    dtRow["Service_Title"] = drList["Service_Title"];
                    dtRow["InvestmentProfile_ID"] = drList["InvestmentProfile_ID"];
                    dtRow["InvestmentPolicy_ID"] = drList["InvestmentPolicy_ID"];
                    dtRow["DateStart"] = drList["Pack_DateStart"];
                    dtRow["DateFinish"] = drList["Pack_DateFinish"];
                    dtRow["Currency"] = drList["Currency"] + "";
                    dtRow["AdvisorName"] = drList["AdvisorName"] + "";
                    dtRow["RMName"] = drList["RMName"] + "";
                    dtRow["PackageProvider_Title"] = drList["PackageProvider_Title"] + "";
                    dtRow["VATPercent"] = drList["VAT_Percent"];

                    dtRow["BrokerageServiceProvider_ID"] = "0" + drList["BrokerageServiceProvider_ID"];
                    dtRow["AdvisoryServiceProvider_ID"] = drList["AdvisoryServiceProvider_ID"];
                    dtRow["AdvisoryAllManFees"] = drList["AdvisoryAllManFees"];

                    dtRow["Advisory_AmoiviPro"] = drList["Advisory_FeesPercent"];
                    if (drList["AdvisoryFees"] + "" != "")
                    {
                        dtRow["AdvisoryDiscount_DateFrom"] = drList["AdvisoryDiscount_DateFrom"];
                        dtRow["AdvisoryDiscount_DateTo"] = drList["AdvisoryDiscount_DateTo"];
                        dtRow["Advisory_Discount_Percent"] = drList["AdvisoryFees_Discount"];
                        dtRow["Advisory_AmoiviAfter"] = drList["AdvisoryFees"];
                    }
                    else
                    {
                        dtRow["AdvisoryDiscount_DateFrom"] = "1900/01/01";
                        dtRow["AdvisoryDiscount_DateTo"] = "2070/12/31";
                        dtRow["Advisory_Discount_Percent"] = 0;
                        dtRow["Advisory_AmoiviAfter"] = drList["Advisory_FeesPercent"];
                    }
                    if (Convert.ToDateTime(dtRow["AdvisoryDiscount_DateFrom"]) < _dFinish && Convert.ToDateTime(dtRow["AdvisoryDiscount_DateTo"]) > _dStart)
                    {
                        dtRow["Advisory_Discount_Percent"] = ((drList["AdvisoryFees_Discount"] + "" != "") ? drList["AdvisoryFees_Discount"] : 0);
                        dtRow["Advisory_AmoiviAfter"] = ((drList["AdvisoryFees"] + "" != "") ? drList["AdvisoryFees"] : drList["Advisory_FeesPercent"]);
                    }
                    else
                    {
                        dtRow["Advisory_Discount_Percent"] = 0;
                        dtRow["Advisory_AmoiviAfter"] = dtRow["Advisory_AmoiviPro"];
                    }
                    dtRow["Advisory_Climakas"] = drList["AdvisoryAllManFees"] + "";
                    dtRow["Advisory_MonthMinAmount"] = drList["Advisory_MonthMinAmount"];
                    dtRow["AdvisoryMonth3_Discount"] = drList["AdvisoryMonth3_Discount"];

                    dtRow["DiscretServiceProvider_ID"] = "0" + drList["DiscretServiceProvider_ID"];
                    dtRow["Discret_AmoiviPro"] = drList["Discret_FeesPercent"];
                    if (drList["DiscretFees"] + "" != "")
                    {
                        dtRow["DiscretDiscount_DateFrom"] = drList["DiscretDiscount_DateFrom"];
                        dtRow["DiscretDiscount_DateTo"] = drList["DiscretDiscount_DateTo"];
                        dtRow["Discret_Discount_Percent"] = drList["DiscretFees_Discount"];
                        dtRow["Discret_AmoiviAfter"] = drList["DiscretFees"];
                    }
                    else
                    {
                        dtRow["DiscretDiscount_DateFrom"] = "1900/01/01";
                        dtRow["DiscretDiscount_DateTo"] = "2070/12/31";
                        dtRow["Discret_Discount_Percent"] = 0;
                        dtRow["Discret_AmoiviAfter"] = drList["Discret_FeesPercent"];
                    }
                    if (Convert.ToDateTime(dtRow["DiscretDiscount_DateFrom"]) < _dFinish && Convert.ToDateTime(dtRow["DiscretDiscount_DateTo"]) > _dStart)
                    {
                        dtRow["Discret_Discount_Percent"] = ((drList["DiscretFees_Discount"] + "" != "") ? drList["DiscretFees_Discount"] : 0);
                        dtRow["Discret_AmoiviAfter"] = ((drList["DiscretFees"] + "" != "") ? drList["DiscretFees"] : drList["Discret_FeesPercent"]);
                    }
                    else
                    {
                        dtRow["Discret_Discount_Percent"] = 0;
                        dtRow["Discret_AmoiviAfter"] = dtRow["Discret_AmoiviPro"];
                    }
                    dtRow["Discret_Climakas"] = drList["DiscretAllManFees"] + "";
                    dtRow["Discret_MonthMinAmount"] = drList["Discret_MonthMinAmount"];
                    dtRow["DiscretMonth3_Discount"] = drList["DiscretMonth3_Discount"];

                    dtRow["DealAdvisoryServiceProvider_ID"] = ((drList["DealAdvisoryServiceProvider_ID"] + "" != "") ? drList["DealAdvisoryServiceProvider_ID"] : 0);
                    dtRow["DealAdvisoryFeesAmount"] = ((drList["DealAdvisoryFeesAmount"] + "" != "") ? drList["DealAdvisoryFeesAmount"] : 0);
                    dtRow["DealAdvisoryFees_Discount"] = ((drList["DealAdvisoryFees_Discount"] + "" != "") ? drList["DealAdvisoryFees_Discount"] : 0);
                    dtRow["DealAdvisoryFees"] = ((drList["DealAdvisoryFees"] + "" != "") ? drList["DealAdvisoryFees"] : 0);

                    dtRow["DealAdvisory_MonthMinAmount"] = drList["DealAdvisory_MonthMinAmount"];
                    dtRow["DealAdvisoryMonth3_Discount"] = drList["DealAdvisoryMonth3_Discount"];
                    dtRow["AdminServiceProvider_ID"] = drList["AdministrationServiceProvider_ID"];
                    dtRow["AdminFeesPercent"] = drList["AdminFeesPercent"];
                    dtRow["AdminFees_Discount"] = drList["AdminFees_Discount"];
                    dtRow["AdminFees"] = drList["AdminFees"];
                    dtRow["Admin_MonthMinAmount"] = drList["Admin_MonthMinAmount"];
                    dtRow["AdminMonth3_Discount"] = drList["AdminMonth3_Discount"];
                    dtRow["AdminMonth3_Fees"] = drList["AdminMonth3_Fees"];

                    dtRow["User1_ID"] = drList["User1_ID"];
                    dtRow["User2_ID"] = drList["User2_ID"];
                    dtRow["User3_ID"] = drList["User3_ID"];
                    dtRow["User4_ID"] = drList["User4_ID"];

                    dtRow["Address"] = drList["InvAddress"];
                    dtRow["City"] = drList["InvCity"];
                    dtRow["Zip"] = drList["InvZip"];
                    dtRow["Country_ID"] = drList["InvCountry_ID"];
                    dtRow["DOY"] = drList["InvDOY"];
                    dtRow["AFM"] = drList["InvAFM"];

                    dtRow["Status"] = drList["Status"];
                    _dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetPeriodicalEvaluation()
        {
            DateTime dPoint1, dPoint2;
            int iOldContract_ID = 0;

            _dtList = new DataTable("PeriodicalEvaluationList");
            dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contracts_Details_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contracts_Packages_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("PackageProvider_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ServiceTitle", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("InvestProfile", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("PortfolioManager", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ContactDetails", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("DateStart", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("DateFinish", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("Days", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ExecutedCommandsCount", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contracts_PeriodicalEvaluation_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("FileName", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("DateSent", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("MiFID_Risk", System.Type.GetType("System.Int32"));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetPeriodicalEvaluationContracts_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateStart", _dStart));
                cmd.Parameters.Add(new SqlParameter("@DateFinish", _dFinish));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if (iOldContract_ID != Convert.ToInt32(drList["ID"]))
                    {
                        iOldContract_ID = Convert.ToInt32(drList["ID"]);
                        if (Convert.ToDateTime(drList["DateStart"]) < _dStart) dPoint1 = _dStart;
                        else dPoint1 = Convert.ToDateTime(drList["DateStart"]);

                        if (Convert.ToDateTime(drList["DateFinish"]) > _dFinish) dPoint2 = _dFinish;
                        else dPoint2 = Convert.ToDateTime(drList["DateFinish"]);

                        if (dPoint1 < dPoint2)            //And dPoint1 <= _dFinish And dPoint2 >= _dStart)
                        {
                            dtRow = _dtList.NewRow();
                            dtRow["ID"] = drList["ID"];
                            dtRow["Contracts_Details_ID"] = drList["Contracts_Details_ID"];
                            dtRow["Contracts_Packages_ID"] = drList["Contracts_Packages_ID"];
                            dtRow["ContractTitle"] = drList["ContractTitle"];
                            dtRow["PackageProvider_Title"] = drList["PackageProvider_Title"];
                            dtRow["Client_ID"] = drList["Client_ID"];
                            dtRow["Code"] = drList["Code"] + "";
                            dtRow["Portfolio"] = drList["Portfolio"] + "";
                            dtRow["ServiceTitle"] = drList["ServiceTitle"] + "";
                            dtRow["InvestProfile"] = drList["InvestProfile"] + "";
                            dtRow["PortfolioManager"] = drList["DiaxSurname"] + " " + drList["DiaxFirstname"];
                            dtRow["ContactDetails"] = drList["DiaxTel"] + "";
                            dtRow["DateStart"] = dPoint1;
                            dtRow["DateFinish"] = dPoint2;
                            dtRow["Days"] = Convert.ToInt32((dPoint2 - dPoint1).TotalDays) + 1;
                            dtRow["ExecutedCommandsCount"] = 0;
                            dtRow["Contracts_PeriodicalEvaluation_ID"] = 0;
                            dtRow["FileName"] = "";
                            dtRow["DateSent"] = "";
                            dtRow["MiFID_Risk"] = drList["MiFID_Risk"];
                            _dtList.Rows.Add(dtRow);
                        }
                    }
                }
                drList.Close();

                foreach (DataRow dtRow1 in _dtList.Rows)
                {
                    cmd = new SqlCommand("GetExecutedCommandsCount", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Contract_ID", dtRow1["ID"]));
                    cmd.Parameters.Add(new SqlParameter("@DateStart", dtRow1["DateStart"]));
                    cmd.Parameters.Add(new SqlParameter("@DateFinish", dtRow1["DateFinish"]));
                    drList = cmd.ExecuteReader();
                    while (drList.Read())
                    {
                        dtRow1["ExecutedCommandsCount"] = ((drList["ExecutedCommandsCount"] + "" != "") ? drList["ExecutedCommandsCount"] : 0);
                    }
                    drList.Close();
                }                

                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Contracts_PeriodicalEvaluation"));
                cmd.Parameters.Add(new SqlParameter("@Col", "Year"));
                cmd.Parameters.Add(new SqlParameter("@Value", _dStart.Year));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    foundRows = _dtList.Select("ID = " + drList["Contract_ID"]);
                    if (foundRows.Length > 0)
                    {
                        foundRows[0]["Contracts_PeriodicalEvaluation_ID"] = drList["ID"];
                        foundRows[0]["FileName"] = drList["FileName"];
                        foundRows[0]["DateSent"] = drList["DateSent"];
                    }
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetPortfolio_Code()
        {
            _dtList = new DataTable("CodePortfolioList");
            dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("BrokerageServiceProvider_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("CFP_ID", System.Type.GetType("System.Int32"));

            dtCol = _dtList.Columns.Add("AdminFeesPercent", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("AdminFees_Discount", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("AdminFees", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("Admin_MonthMinAmount", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("AdminMonth3_Discount", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("AdminMonth3_Fees", System.Type.GetType("System.Single"));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetPortfolios_Code", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Code", _sCode));
                cmd.Parameters.Add(new SqlParameter("@DateStart", _dStart));
                cmd.Parameters.Add(new SqlParameter("@DateFinish", _dFinish));
                drList = cmd.ExecuteReader();

                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["Portfolio"] = drList["Portfolio"] + "";
                    dtRow["Currency"] = drList["Currency"] + "";
                    dtRow["Status"] = drList["Status"];
                    dtRow["BrokerageServiceProvider_ID"] = drList["BrokerageServiceProvider_ID"];
                    dtRow["CFP_ID"] = drList["CFP_ID"];

                    dtRow["AdminFeesPercent"] = drList["AdminFeesPercent"];
                    dtRow["AdminFees_Discount"] = drList["AdminFees_Discount"];
                    dtRow["AdminFees"] = drList["AdminFees"];
                    dtRow["Admin_MonthMinAmount"] = drList["MinimumFees"];
                    dtRow["AdminMonth3_Discount"] = drList["MinimumFees_Discount"];
                    dtRow["AdminMonth3_Fees"] = drList["MinimumFees"];

                    _dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetOwnersList()
        {
            _dtList = new DataTable("ContractOwnersList");
            dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ClientName", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("FirstnameFather", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ADT", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Passport", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("DOY", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("AFM", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("IsMaster", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("IsOrder", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("DoB", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Special_Title", System.Type.GetType("System.String"));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetContract_OwnersList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Contract_ID", _iRecord_ID));
                drList = cmd.ExecuteReader();

                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["ClientName"] = (drList["Surname"] + " " + drList["Firstname"]).Trim();
                    dtRow["FirstnameFather"] = drList["FirstnameFather"];
                    dtRow["ADT"] = drList["ADT"];
                    dtRow["Passport"] = drList["Passport"];
                    dtRow["DOY"] = drList["DOY"];
                    dtRow["AFM"] = drList["AFM"];
                    dtRow["IsMaster"] = drList["IsMaster"];
                    dtRow["IsOrder"] = drList["IsOrder"];
                    dtRow["Client_ID"] = drList["Client_ID"];
                    dtRow["DoB"] = drList["DoB"];
                    dtRow["Special_Title"] = drList["Special_Title"];
                    _dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetAuditList()
        {
            _dtList = new DataTable("PackagesList");
            dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("DateStart", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("DateFinish", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
            //dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ServiceProvider_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Address", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Contract_Country", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Tel", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Fax", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Mobile", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("EMail", System.Type.GetType("System.String"));

            dtCol = _dtList.Columns.Add("Benef_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Benef_Address", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Benef_Country", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Benef_Tel", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Benef_Fax", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Benef_Mobile", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Benef_EMail", System.Type.GetType("System.String"));

            dtCol = _dtList.Columns.Add("Dir_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Dir_Address", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Dir_Country", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Dir_Tel", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Dir_Fax", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Dir_Mobile", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Dir_EMail", System.Type.GetType("System.String"));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetAuditsList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dStart));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dFinish));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["ContractTitle"] = drList["ContractTitle"] + "";
                    dtRow["DateStart"] = drList["DateStart"] + "";
                    dtRow["DateFinish"] = drList["DateFinish"] + "";
                    dtRow["Code"] = drList["Code"] + "";
                    //dtRow["Portfolio"] = drList["Portfolio"] + "";
                    dtRow["ServiceProvider_Title"] = drList["ServiceProvider_Title"] + "";
                    dtRow["Address"] = drList["Address"] + " " + drList["City"] + " " + drList["Zip"];
                    dtRow["Contract_Country"] = drList["Contract_Country"] + "";
                    dtRow["Tel"] = drList["Tel"] + "";
                    dtRow["Fax"] = drList["Fax"] + "";
                    dtRow["Mobile"] = drList["Mobile"] + "";
                    dtRow["EMail"] = drList["EMail"] + "";

                    dtRow["Benef_Title"] = "";
                    dtRow["Benef_Address"] = "";
                    dtRow["Benef_Country"] = "";
                    dtRow["Benef_Tel"] = "";
                    dtRow["Benef_Fax"] = "";
                    dtRow["Benef_Mobile"] = "";
                    dtRow["Benef_EMail"] = "";
                    dtRow["Dir_Title"] = "";
                    dtRow["Dir_Address"] = "";
                    dtRow["Dir_Country"] = "";
                    dtRow["Dir_Tel"] = "";
                    dtRow["Dir_Fax"] = "";
                    dtRow["Dir_Mobile"] = "";
                    dtRow["Dir_EMail"] = "";

                    if ((drList["AuthRep"] + "" != ""))
                    {
                        if (Convert.ToInt32(drList["AuthRep"]) == 1)
                        {
                            dtRow["Benef_Title"] = drList["Rep_Surname"] + " " + drList["Rep_Firstname"];
                            dtRow["Benef_Address"] = drList["Rep_Address"] + " " + drList["Rep_City"] + " " + drList["Rep_Zip"];
                            dtRow["Benef_Country"] = drList["Rep_Country"] + "";
                            dtRow["Benef_Tel"] = drList["Rep_Tel"] + "";
                            dtRow["Benef_Fax"] = drList["Rep_Fax"] + "";
                            dtRow["Benef_Mobile"] = drList["Rep_Mobile"] + "";
                            dtRow["Benef_EMail"] = drList["Rep_EMail"] + "";
                        }
                    }

                    if ((drList["Director"] + "" != ""))
                    {
                        if (Convert.ToInt32(drList["Director"]) == 1)
                        {
                            dtRow["Dir_Title"] = drList["Rep_Surname"] + " " + drList["Rep_Firstname"];
                            dtRow["Dir_Address"] = drList["Rep_Address"] + " " + drList["Rep_City"] + " " + drList["Rep_Zip"];
                            dtRow["Dir_Country"] = drList["Rep_Country"] + "";
                            dtRow["Dir_Tel"] = drList["Rep_Tel"] + "";
                            dtRow["Dir_Fax"] = drList["Rep_Fax"] + "";
                            dtRow["Dir_Mobile"] = drList["Rep_Mobile"] + "";
                            dtRow["Dir_EMail"] = drList["Rep_EMail"] + "";
                        }
                    }
                    _dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

        }
        public int InsertRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("InsertContract", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PackageType", SqlDbType.Int).Value = this._iPackageType;           // ' 1 - Client's Package Type, 2 - SecurityServicesCompanie's Package Type
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = this._iClient_ID;
                    cmd.Parameters.Add("@Tipos", SqlDbType.Int).Value = this._iContractType;
                    cmd.Parameters.Add("@ContractTitle", SqlDbType.NVarChar, 100).Value = this._sContractTitle.Trim();
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 30).Value = this._sCode.Trim();
                    cmd.Parameters.Add("@Portfolio", SqlDbType.NVarChar, 50).Value = this._sPortfolio.Trim();
                    cmd.Parameters.Add("@Portfolio_Alias", SqlDbType.NVarChar, 50).Value = this._sPortfolio_Alias.Trim();
                    cmd.Parameters.Add("@Portfolio_Type", SqlDbType.NVarChar, 20).Value = this._sPortfolio_Type.Trim();
                    cmd.Parameters.Add("@DateStart", SqlDbType.DateTime).Value = this._dStart;
                    cmd.Parameters.Add("@DateFinish", SqlDbType.DateTime).Value = this._dFinish;
                    cmd.Parameters.Add("@Currency", SqlDbType.NVarChar, 6).Value = this._sCurrency;
                    cmd.Parameters.Add("@NumberAccount", SqlDbType.NVarChar, 50).Value = this._sNumberAccount;
                    cmd.Parameters.Add("@Contracts_Details_ID", SqlDbType.Int).Value = this._iContracts_Details_ID;
                    cmd.Parameters.Add("@Contracts_Packages_ID", SqlDbType.Int).Value = this._iContracts_Packages_ID;
                    cmd.Parameters.Add("@MIFID_2", SqlDbType.Int).Value = this._iMiFID_2;
                    cmd.Parameters.Add("@MiFID_2_StartDate", SqlDbType.DateTime).Value = this._dMiFID_2_StartDate;
                    cmd.Parameters.Add("@Questionary_ID", SqlDbType.Int).Value = this._iQuestionary_ID;
                    cmd.Parameters.Add("@XAA", SqlDbType.Int).Value = this._iXAA;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = 1;
                    cmd.ExecuteNonQuery();
                    _iRecord_ID = Convert.ToInt32(outParam.Value);

                    _klsDetails.Contract_ID = _iRecord_ID;
                    _iDet = _klsDetails.InsertRecord();

                    _klsPackages.Contract_ID = _iRecord_ID;
                    _iPack = _klsPackages.InsertRecord();

                    _klsContracts_Details_Packages.DateFrom = DateTime.Now;                                // new record into table Contracts_Details_Packages always has DateFrom = Now() and
                    _klsContracts_Details_Packages.DateTo = Convert.ToDateTime("2070/12/31");              //                                                             DateTo = "2070/12/31"  ...
                    _klsContracts_Details_Packages.Contract_ID = _iRecord_ID;
                    _klsContracts_Details_Packages.Contracts_Details_ID = _iDet;
                    _klsContracts_Details_Packages.Contracts_Packages_ID = _iPack;
                    _klsContracts_Details_Packages.Notes = "";
                    _klsContracts_Details_Packages.InsertRecord();                                         // ... in InsertRecord method previous record in Contracts_Details_Packages will change    

                    using (SqlCommand cmd1 = new SqlCommand("UPDATE Contracts SET Contracts_Details_ID = " + _iDet + ", Contracts_Packages_ID = " + _iPack + " WHERE ID = " + _iRecord_ID, conn))
                    {
                        cmd1.CommandType = CommandType.Text;
                        cmd1.Parameters.Add("@ID", SqlDbType.Int).Value = this._iRecord_ID;
                        cmd1.Parameters.Add("@Contracts_Details_ID", SqlDbType.Int).Value = this._iPackageType;           // ' 1 - Client's Package Type, 2 - SecurityServicesCompanie's Package Type
                        cmd1.Parameters.Add("@Contracts_Packages_ID", SqlDbType.Int).Value = this._iClient_ID;
                        cmd1.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            Edit_ClientsList();

            return _iRecord_ID;
        }
        public void EditRecord()
        {
            _klsContracts_Details_Packages = new clsContracts_Details_Packages();
            _klsContracts_Details_Packages.Contract_ID = _iRecord_ID;
            _klsContracts_Details_Packages.Contracts_Details_ID = _iContracts_Details_ID;
            _klsContracts_Details_Packages.Contracts_Packages_ID = _iContracts_Packages_ID;
            _klsContracts_Details_Packages.GetRecord_Contract_ID();

            if (_klsContracts_Details_Packages.Record_ID == 0)
            {
                _klsContracts_Details_Packages.DateFrom = DateTime.Now;                            // new record into table Contracts_Details_Packages always has DateFrom = Now() and
                _klsContracts_Details_Packages.DateTo = Convert.ToDateTime("2070/12/31");          // DateTo = "2070/12/31"  ...
                _klsContracts_Details_Packages.Contract_ID = _iRecord_ID;
                _klsContracts_Details_Packages.Contracts_Details_ID = _iContracts_Details_ID;
                _klsContracts_Details_Packages.Contracts_Packages_ID = _iContracts_Packages_ID;
                _klsContracts_Details_Packages.Notes = "";
                _klsContracts_Details_Packages.InsertRecord();                                    // ... in InsertRecord method previous record in Contracts_Details_Packages will change
            }

            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("EditContract", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = this._iRecord_ID;
                    cmd.Parameters.Add("@PackageType", SqlDbType.Int).Value = this._iPackageType;           // ' 1 - Client's Package Type, 2 - SecurityServicesCompanie's Package Type
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = this._iClient_ID;
                    cmd.Parameters.Add("@Tipos", SqlDbType.Int).Value = this._iContractType;
                    cmd.Parameters.Add("@ContractTitle", SqlDbType.NVarChar, 100).Value = this._sContractTitle.Trim();
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 30).Value = this._sCode.Trim();
                    cmd.Parameters.Add("@Portfolio", SqlDbType.NVarChar, 50).Value = this._sPortfolio.Trim();
                    cmd.Parameters.Add("@Portfolio_Alias", SqlDbType.NVarChar, 50).Value = this._sPortfolio_Alias.Trim();
                    cmd.Parameters.Add("@Portfolio_Type", SqlDbType.NVarChar, 20).Value = this._sPortfolio_Type.Trim();
                    cmd.Parameters.Add("@DateStart", SqlDbType.DateTime).Value = this._dStart;
                    cmd.Parameters.Add("@DateFinish", SqlDbType.DateTime).Value = this._dFinish;
                    cmd.Parameters.Add("@Currency", SqlDbType.NVarChar, 6).Value = this._sCurrency;
                    cmd.Parameters.Add("@NumberAccount", SqlDbType.NVarChar, 50).Value = this._sNumberAccount;
                    cmd.Parameters.Add("@Contracts_Details_ID", SqlDbType.Int).Value = this._iContracts_Details_ID;
                    cmd.Parameters.Add("@Contracts_Packages_ID", SqlDbType.Int).Value = this._iContracts_Packages_ID;
                    cmd.Parameters.Add("@MIFID_2", SqlDbType.Int).Value = this._iMiFID_2;
                    cmd.Parameters.Add("@MiFID_2_StartDate", SqlDbType.DateTime).Value = this._dMiFID_2_StartDate;
                    cmd.Parameters.Add("@Questionary_ID", SqlDbType.Int).Value = this._iQuestionary_ID;
                    cmd.Parameters.Add("@XAA", SqlDbType.Int).Value = this._iXAA;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = 1;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            Edit_ClientsList();
        }
        public void EditRecord_Cancel()
        {
            this._klsPackages = new clsContracts_Packages();
            this._klsPackages.Contract_ID = 0;
            this._klsPackages.Record_ID = this._iContracts_Packages_ID;
            this._klsPackages.GetRecord();
            this._klsPackages.DateFinish = _dFinish;
            this._klsPackages.EditRecord();

            this._klsContracts_Details_Packages.Contract_ID = _iRecord_ID;
            this._klsContracts_Details_Packages.Contracts_Details_ID = _iContracts_Details_ID;
            this._klsContracts_Details_Packages.Contracts_Packages_ID = _iContracts_Packages_ID;
            this._klsContracts_Details_Packages.GetRecord_Contract_ID();
            this._klsContracts_Details_Packages.DateTo = _dFinish;
            this._klsContracts_Details_Packages.EditRecord();

            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("EditContract_Cancel", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = this._iRecord_ID;
                    cmd.Parameters.Add("@DateFinish", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = 0;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void EditRecord_Details()
        {
            this._klsDetails.Contract_ID = this._iRecord_ID;
            this._iDet = _klsDetails.InsertRecord();

            this._klsContracts_Details_Packages.DateFrom = DateTime.Now;                             // new record into table Contracts_Details_Packages always has DateFrom = Now() and
            this._klsContracts_Details_Packages.DateTo = Convert.ToDateTime("2070/12/31");           // DateTo = "2070/12/31"  ...
            this._klsContracts_Details_Packages.Contract_ID = _iRecord_ID;
            this._klsContracts_Details_Packages.Contracts_Details_ID = _iDet;
            this._klsContracts_Details_Packages.Contracts_Packages_ID = _iContracts_Packages_ID;
            this._klsContracts_Details_Packages.Notes = "";
            this._klsContracts_Details_Packages.InsertRecord();                                      // ... in InsertRecord method previous record in Contracts_Details_Packages will change

            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("EditContract", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = this._iRecord_ID;
                    cmd.Parameters.Add("@PackageType", SqlDbType.Int).Value = this._iPackageType;    // 1 - Client's Package Type, 2 - SecurityServicesCompanie's Package Type
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = this._iClient_ID;
                    cmd.Parameters.Add("@Tipos", SqlDbType.Int).Value = this._iContractType;
                    cmd.Parameters.Add("@ContractTitle", SqlDbType.NVarChar, 100).Value = this._sContractTitle.Trim();
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 30).Value = this._sCode.Trim();
                    cmd.Parameters.Add("@Portfolio", SqlDbType.NVarChar, 50).Value = this._sPortfolio.Trim();
                    cmd.Parameters.Add("@Portfolio_Alias", SqlDbType.NVarChar, 50).Value = this._sPortfolio_Alias.Trim();
                    cmd.Parameters.Add("@Portfolio_Type", SqlDbType.NVarChar, 20).Value = this._sPortfolio_Type.Trim();
                    cmd.Parameters.Add("@DateStart", SqlDbType.DateTime).Value = this._dStart;
                    cmd.Parameters.Add("@DateFinish", SqlDbType.DateTime).Value = this._dFinish;
                    cmd.Parameters.Add("@Currency", SqlDbType.NVarChar, 6).Value = this._sCurrency;
                    cmd.Parameters.Add("@NumberAccount", SqlDbType.NVarChar, 50).Value = this._sNumberAccount;
                    cmd.Parameters.Add("@Contracts_Details_ID", SqlDbType.Int).Value = this._iDet;
                    cmd.Parameters.Add("@Contracts_Packages_ID", SqlDbType.Int).Value = this._iContracts_Packages_ID;
                    cmd.Parameters.Add("@MIFID_2", SqlDbType.Int).Value = this._iMiFID_2;
                    cmd.Parameters.Add("@MiFID_2_StartDate", SqlDbType.DateTime).Value = this._dMiFID_2_StartDate;
                    cmd.Parameters.Add("@Questionary_ID", SqlDbType.Int).Value = this._iQuestionary_ID;
                    cmd.Parameters.Add("@XAA", SqlDbType.Int).Value = this._iXAA;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = 1;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            Edit_ClientsList();
        }
        public void EditRecord_Packages()
        {
            this._klsPackages.Contract_ID = this._iRecord_ID;
            this._iPack = this._klsPackages.InsertRecord();

            this._klsContracts_Details_Packages.DateFrom = DateTime.Now;                            // new record into table Contracts_Details_Packages always has DateFrom = Now() and
            this._klsContracts_Details_Packages.DateTo = Convert.ToDateTime("2070/12/31");          // DateTo = "2070/12/31"  ...
            this._klsContracts_Details_Packages.Contract_ID = _iRecord_ID;
            this._klsContracts_Details_Packages.Contracts_Details_ID = _iContracts_Details_ID;
            this._klsContracts_Details_Packages.Contracts_Packages_ID = _iPack;
            this._klsContracts_Details_Packages.Notes = "";
            this._klsContracts_Details_Packages.InsertRecord();                                    // ... in InsertRecord method previous record in Contracts_Details_Packages will change

            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("EditContract", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = this._iRecord_ID;
                    cmd.Parameters.Add("@PackageType", SqlDbType.Int).Value = this._iPackageType;           // ' 1 - Client's Package Type, 2 - SecurityServicesCompanie's Package Type
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = this._iClient_ID;
                    cmd.Parameters.Add("@Tipos", SqlDbType.Int).Value = this._iContractType;
                    cmd.Parameters.Add("@ContractTitle", SqlDbType.NVarChar, 100).Value = this._sContractTitle;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 30).Value = this._sCode;
                    cmd.Parameters.Add("@Portfolio", SqlDbType.NVarChar, 50).Value = this._sPortfolio;
                    cmd.Parameters.Add("@Portfolio_Alias", SqlDbType.NVarChar, 50).Value = this._sPortfolio_Alias;
                    cmd.Parameters.Add("@Portfolio_Type", SqlDbType.NVarChar, 20).Value = this._sPortfolio_Type;
                    cmd.Parameters.Add("@DateStart", SqlDbType.DateTime).Value = this._dStart;
                    cmd.Parameters.Add("@DateFinish", SqlDbType.DateTime).Value = this._dFinish;
                    cmd.Parameters.Add("@Currency", SqlDbType.NVarChar, 6).Value = this._sCurrency;
                    cmd.Parameters.Add("@NumberAccount", SqlDbType.NVarChar, 50).Value = this._sNumberAccount;
                    cmd.Parameters.Add("@Contracts_Details_ID", SqlDbType.Int).Value = this._iContracts_Details_ID;
                    cmd.Parameters.Add("@Contracts_Packages_ID", SqlDbType.Int).Value = this._iPack;
                    cmd.Parameters.Add("@MIFID_2", SqlDbType.Int).Value = this._iMiFID_2;
                    cmd.Parameters.Add("@MiFID_2_StartDate", SqlDbType.DateTime).Value = this._dMiFID_2_StartDate;
                    cmd.Parameters.Add("@Questionary_ID", SqlDbType.Int).Value = this._iQuestionary_ID;
                    cmd.Parameters.Add("@XAA", SqlDbType.Int).Value = this._iXAA;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = 1;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            Edit_ClientsList();
        }
        public void Edit_ClientsList()
        {
            int i = 0;
            string[] tokens = _sClientsList.Split('~');
            clsClients_Contracts Clients_Contracts = new clsClients_Contracts();

            for (i = 0; i <= tokens.Length - 2; i++)
            {
                string[] bokens = tokens[i].Split('^');

                if (Convert.ToInt32(bokens[5]) == 0)
                {
                    Clients_Contracts = new clsClients_Contracts();
                    Clients_Contracts.Client_ID = Convert.ToInt32(bokens[0]);
                    Clients_Contracts.Contract_ID = _iRecord_ID;
                    Clients_Contracts.DOY = bokens[1] + "";
                    Clients_Contracts.AFM = bokens[2] + "";
                    Clients_Contracts.IsMaster = Convert.ToInt32(bokens[3]);
                    Clients_Contracts.IsOrder = Convert.ToInt32(bokens[4]);
                    Clients_Contracts.InsertRecord();
                }
                else
                {
                    Clients_Contracts = new clsClients_Contracts();
                    Clients_Contracts.Record_ID = Convert.ToInt32(bokens[5]);
                    Clients_Contracts.GetRecord();
                    Clients_Contracts.Client_ID = Convert.ToInt32(bokens[0]);
                    Clients_Contracts.Contract_ID = _iRecord_ID;
                    Clients_Contracts.DOY = bokens[1] + "";
                    Clients_Contracts.AFM = bokens[2] + "";
                    Clients_Contracts.IsMaster = Convert.ToInt32(bokens[3]);
                    Clients_Contracts.IsOrder = Convert.ToInt32(bokens[4]);
                    Clients_Contracts.EditRecord();
                }
            }
        }
        public void DeleteRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Contracts";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = this._iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }

        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int PackageType { get { return this._iPackageType; } set { this._iPackageType = value; } }
        public string Package_Title { get { return this._sPackage_Title; } set { this._sPackage_Title = value; } }
        public int PackageProvider_ID { get { return this._iPackageProvider_ID; } set { this._iPackageProvider_ID = value; } }
        public string PackageProvider { get { return this._sPackageProvider; } set { this._sPackageProvider = value; } }
        public string PackageProvider_PriceTable { get { return this._sPackageProvider_PriceTable; } set { this._sPackageProvider_PriceTable = value; } }
        public int Client_ID { get { return this._iClient_ID; } set { this._iClient_ID = value; } }
        public int ClientTipos { get { return this._iClientTipos; } set { this._iClientTipos = value; } }
        public int ContractType { get { return this._iContractType; } set { this._iContractType = value; } }
        public string ContractTitle { get { return this._sContractTitle; } set { this._sContractTitle = value; } }
        public string Code { get { return this._sCode; } set { this._sCode = value; } }
        public string Portfolio { get { return this._sPortfolio; } set { this._sPortfolio = value; } }
        public string Portfolio_Alias { get { return this._sPortfolio_Alias; } set { this._sPortfolio_Alias = value; } }
        public string Portfolio_Type { get { return this._sPortfolio_Type; } set { this._sPortfolio_Type = value; } }
        public DateTime Start { get { return this._dStart; } set { this._dStart = value; } }
        public DateTime Finish { get { return this._dFinish; } set { this._dFinish = value; } }
        public string Currency { get { return this._sCurrency; } set { this._sCurrency = value; } }
        public string NumberAccount { get { return this._sNumberAccount; } set { this._sNumberAccount = value; } }
        public string L4 { get { return this._sL4; } set { this._sL4 = value; } }
        public float VAT_FP { get { return this._fltVAT_FP; } set { this._fltVAT_FP = value; } }
        public float VAT_NP { get { return this._fltVAT_NP; } set { this._fltVAT_NP = value; } }
        public int Contract_Details_ID { get { return this._iContracts_Details_ID; } set { this._iContracts_Details_ID = value; } }
        public int Contract_Packages_ID { get { return this._iContracts_Packages_ID; } set { this._iContracts_Packages_ID = value; } }
        public int CDP_ID { get { return this._iCDP_ID; } set { this._iCDP_ID = value; } }
        public string CDP_Notes { get { return this._sCDP_Notes; } set { this._sCDP_Notes = value; } }
        public int MiFID_Risk { get { return this._iMiFID_Risk; } set { this._iMiFID_Risk = value; } }
        public int Profile_ID { get { return this._iProfile_ID; } set { this._iProfile_ID = value; } }
        public string ProfileTitle { get { return this._sProfileTitle; } set { this._sProfileTitle = value; } }
        public int MiFID_2 { get { return this._iMiFID_2; } set { this._iMiFID_2 = value; } }
        public DateTime MiFID_2_StartDate { get { return this._dMiFID_2_StartDate; } set { this._dMiFID_2_StartDate = value; } }
        public int Questionary_ID { get { return this._iQuestionary_ID; } set { this._iQuestionary_ID = value; } }
        public int XAA { get { return this._iXAA; } set { this._iXAA = value; } }
        public string AdvisorFullname { get { return this._sAdvisorFullname; } set { this._sAdvisorFullname = value; } }
        public string AdvisorEMail { get { return this._sAdvisorEMail; } set { this._sAdvisorEMail = value; } }
        public string AdvisorTel { get { return this._sAdvisorEMail; } set { this._sAdvisorEMail = value; } }
        public string AdvisorMobile { get { return this._sAdvisorMobile; } set { this._sAdvisorMobile = value; } }
        public int BrokerageServiceProvider_ID { get { return this._iBrokerageServiceProvider_ID; } set { this._iBrokerageServiceProvider_ID = value; } }
        public int BrokerageOption_ID { get { return this._iBrokerageOption_ID; } set { this._iBrokerageOption_ID = value; } }
        public string BrokerageServiceProvider_Title { get { return this._sBrokerageServiceProvider_Title; } set { this._sBrokerageServiceProvider_Title = value; } }
        public string BrokerageOption_Title { get { return this._sBrokerageOption_Title; } set { this._sBrokerageOption_Title = value; } }
        public int RTOServiceProvider_ID { get { return this._iRTOServiceProvider_ID; } set { this._iRTOServiceProvider_ID = value; } }
        public int RTOOption_ID { get { return this._iRTOOption_ID; } set { this._iRTOOption_ID = value; } }
        public string RTOServiceProvider_Title { get { return this._sRTOServiceProvider_Title; } set { this._sRTOServiceProvider_Title = value; } }
        public string RTOOption_Title { get { return this._sRTOOption_Title; } set { this._sRTOOption_Title = value; } }
        public string AdvisoryServiceProvider_Title { get { return this._sAdvisoryServiceProvider_Title; } set { this._sAdvisoryServiceProvider_Title = value; } }
        public int AdvisoryServiceProvider_ID { get { return this._iAdvisoryServiceProvider_ID; } set { this._iAdvisoryServiceProvider_ID = value; } }
        public string AdvisoryOption_Title { get { return this._sAdvisoryOption_Title; } set { this._sAdvisoryOption_Title = value; } }
        public int AdvisoryOption_ID { get { return this._iAdvisoryOption_ID; } set { this._iAdvisoryOption_ID = value; } }
        public string AdvisoryInvestmentProfile_Title { get { return this._sAdvisoryInvestmentProfile_Title; } set { this._sAdvisoryInvestmentProfile_Title = value; } }
        public int AdvisoryInvestmentProfile_ID { get { return this._iAdvisoryInvestmentProfile_ID; } set { this._iAdvisoryInvestmentProfile_ID = value; } }
        public string AdvisoryInvestmentPolicy_Title { get { return this._sAdvisoryInvestmentPolicy_Title; } set { this._sAdvisoryInvestmentPolicy_Title = value; } }
        public int AdvisoryInvestmentPolicy_ID { get { return this._iAdvisoryInvestmentPolicy_ID; } set { this._iAdvisoryInvestmentPolicy_ID = value; } }
        public float Advisory_MonthMinAmount { get { return this._fltAdvisory_MonthMinAmount; } set { this._fltAdvisory_MonthMinAmount = value; } }
        public string Advisory_MonthMinCurr { get { return this._sAdvisory_MonthMinCurr; } set { this._sAdvisory_MonthMinCurr = value; } }
        public float Advisory_OpenAmount { get { return this._fltAdvisory_OpenAmount; } set { this._fltAdvisory_OpenAmount = value; } }
        public string Advisory_OpenCurr { get { return this._sAdvisory_OpenCurr; } set { this._sAdvisory_OpenCurr = value; } }
        public float Advisory_ServiceAmount { get { return this._fltAdvisory_ServiceAmount; } set { this._fltAdvisory_ServiceAmount = value; } }
        public string Advisory_ServiceCurr { get { return this._sAdvisory_ServiceCurr; } set { this._sAdvisory_ServiceCurr = value; } }
        public float Advisory_MinAmount { get { return this._fltAdvisory_MinAmount; } set { this._fltAdvisory_MinAmount = value; } }
        public string Advisory_MinCurr { get { return this._sAdvisory_MinCurr; } set { this._sAdvisory_MinCurr = value; } }
        public float Advisory_Month3_Discount { get { return this._fltAdvisory_Month3_Discount; } set { this._fltAdvisory_Month3_Discount = value; } }
        public float Advisory_Month3_Fees { get { return this._fltAdvisory_Month3_Fees; } set { this._fltAdvisory_Month3_Fees = value; } }
        public string Advisory_AllManFees { get { return this._sAdvisory_AllManFees; } set { this._sAdvisory_AllManFees = value; } }
        public string DiscretServiceProvider_Title { get { return this._sDiscretServiceProvider_Title; } set { this._sDiscretServiceProvider_Title = value; } }
        public int DiscretServiceProvider_ID { get { return this._iDiscretServiceProvider_ID; } set { this._iDiscretServiceProvider_ID = value; } }
        public string DiscretOption_Title { get { return this._sDiscretOption_Title; } set { this._sDiscretOption_Title = value; } }
        public int DiscretOption_ID { get { return this._iDiscretOption_ID; } set { this._iDiscretOption_ID = value; } }
        public string DiscretInvestmentProfile_Title { get { return this._sDiscretInvestmentProfile_Title; } set { this._sDiscretInvestmentProfile_Title = value; } }
        public int DiscretInvestmentProfile_ID { get { return this._iDiscretInvestmentProfile_ID; } set { this._iDiscretInvestmentProfile_ID = value; } }
        public string DiscretInvestmentPolicy_Title { get { return this._sDiscretInvestmentPolicy_Title; } set { this._sDiscretInvestmentPolicy_Title = value; } }
        public int DiscretInvestmentPolicy_ID { get { return this._iDiscretInvestmentPolicy_ID; } set { this._iDiscretInvestmentPolicy_ID = value; } }
        public float Discret_MonthMinAmount { get { return this._fltDiscret_MonthMinAmount; } set { this._fltDiscret_MonthMinAmount = value; } }
        public string Discret_MonthMinCurr { get { return this._sDiscret_MonthMinCurr; } set { this._sDiscret_MonthMinCurr = value; } }
        public float Discret_OpenAmount { get { return this._fltDiscret_OpenAmount; } set { this._fltDiscret_OpenAmount = value; } }
        public string Discret_OpenCurr { get { return this._sDiscret_OpenCurr; } set { this._sDiscret_OpenCurr = value; } }
        public float Discret_ServiceAmount { get { return this._fltDiscret_ServiceAmount; } set { this._fltDiscret_ServiceAmount = value; } }
        public string Discret_ServiceCurr { get { return this._sDiscret_ServiceCurr; } set { this._sDiscret_ServiceCurr = value; } }
        public float Discret_MinAmount { get { return this._fltDiscret_MinAmount; } set { this._fltDiscret_MinAmount = value; } }
        public string Discret_MinCurr { get { return this._sDiscret_MinCurr; } set { this._sDiscret_MinCurr = value; } }
        public float Discret_Month3_Discount { get { return this._fltDiscret_Month3_Discount; } set { this._fltDiscret_Month3_Discount = value; } }
        public float Discret_Month3_Fees { get { return this._fltDiscret_Month3_Fees; } set { this._fltDiscret_Month3_Fees = value; } }
        public string Discret_AllManFees { get { return this._sDiscret_AllManFees; } set { this._sDiscret_AllManFees = value; } }
        public string CustodyServiceProvider_Title { get { return this._sCustodyServiceProvider_Title; } set { this._sCustodyServiceProvider_Title = value; } }
        public int CustodyServiceProvider_ID { get { return this._iCustodyServiceProvider_ID; } set { this._iCustodyServiceProvider_ID = value; } }
        public string CustodyOption_Title { get { return this._sCustodyOption_Title; } set { this._sCustodyOption_Title = value; } }
        public int CustodyOption_ID { get { return this._iCustodyOption_ID; } set { this._iCustodyOption_ID = value; } }
        public float Custody_MonthMinAmount { get { return this._fltCustody_MonthMinAmount; } set { this._fltCustody_MonthMinAmount = value; } }
        public string Custody_MonthMinCurr { get { return this._sCustody_MonthMinCurr; } set { this._sCustody_MonthMinCurr = value; } }
        public float Custody_OpenAmount { get { return this._fltCustody_OpenAmount; } set { this._fltCustody_OpenAmount = value; } }
        public string Custody_OpenCurr { get { return this._sCustody_OpenCurr; } set { this._sCustody_OpenCurr = value; } }
        public float Custody_ServiceAmount { get { return this._fltCustody_ServiceAmount; } set { this._fltCustody_ServiceAmount = value; } }
        public string Custody_ServiceCurr { get { return this._sCustody_ServiceCurr; } set { this._sCustody_ServiceCurr = value; } }
        public float Custody_MinAmount { get { return this._fltCustody_MinAmount; } set { this._fltCustody_MinAmount = value; } }
        public string Custody_MinCurr { get { return this._sCustody_MinCurr; } set { this._sCustody_MinCurr = value; } }
        public string AdminServiceProvider_Title { get { return this._sAdminServiceProvider_Title; } set { this._sAdminServiceProvider_Title = value; } }
        public int AdminServiceProvider_ID { get { return this._iAdminServiceProvider_ID; } set { this._iAdminServiceProvider_ID = value; } }
        public string AdminOption_Title { get { return this._sAdminOption_Title; } set { this._sAdminOption_Title = value; } }
        public int AdminOption_ID { get { return this._iAdminOption_ID; } set { this._iAdminOption_ID = value; } }
        public float Admin_MonthMinAmount { get { return this._fltAdmin_MonthMinAmount; } set { this._fltAdmin_MonthMinAmount = value; } }
        public string Admin_MonthMinCurr { get { return this._sAdmin_MonthMinCurr; } set { this._sAdmin_MonthMinCurr = value; } }
        public float Admin_OpenAmount { get { return this._fltAdmin_OpenAmount; } set { this._fltAdmin_OpenAmount = value; } }
        public string Admin_OpenCurr { get { return this._sAdmin_OpenCurr; } set { this._sAdmin_OpenCurr = value; } }
        public float Admin_ServiceAmount { get { return this._fltAdmin_ServiceAmount; } set { this._fltAdmin_ServiceAmount = value; } }
        public string Admin_ServiceCurr { get { return this._sAdmin_ServiceCurr; } set { this._sAdmin_ServiceCurr = value; } }
        public float Admin_MinAmount { get { return this._fltAdmin_MinAmount; } set { this._fltAdmin_MinAmount = value; } }
        public string Admin_MinCurr { get { return this._sAdmin_MinCurr; } set { this._sAdmin_MinCurr = value; } }
        public string DealAdvisoryServiceProvider_Title { get { return this._sDealAdvisoryServiceProvider_Title; } set { this._sDealAdvisoryServiceProvider_Title = value; } }
        public int DealAdvisoryServiceProvider_ID { get { return this._iDealAdvisoryServiceProvider_ID; } set { this._iDealAdvisoryServiceProvider_ID = value; } }
        public string DealAdvisoryOption_Title { get { return this._sDealAdvisoryOption_Title; } set { this._sDealAdvisoryOption_Title = value; } }
        public int DealAdvisoryOption_ID { get { return this._iDealAdvisoryOption_ID; } set { this._iDealAdvisoryOption_ID = value; } }
        public string DealAdvisoryInvestmentPolicy_Title { get { return this._sDealAdvisoryInvestmentPolicy_Title; } set { this._sDealAdvisoryInvestmentPolicy_Title = value; } }
        public int DealAdvisoryInvestmentPolicy_ID { get { return this._iDealAdvisoryInvestmentPolicy_ID; } set { this._iDealAdvisoryInvestmentPolicy_ID = value; } }
        public float DealAdvisory_MonthMinAmount { get { return this._fltDealAdvisory_MonthMinAmount; } set { this._fltDealAdvisory_MonthMinAmount = value; } }
        public string DealAdvisory_MonthMinCurr { get { return this._sDealAdvisory_MonthMinCurr; } set { this._sDealAdvisory_MonthMinCurr = value; } }
        public float DealAdvisory_OpenAmount { get { return this._fltDealAdvisory_OpenAmount; } set { this._fltDealAdvisory_OpenAmount = value; } }
        public string DealAdvisory_OpenCurr { get { return this._sDealAdvisory_OpenCurr; } set { this._sDealAdvisory_OpenCurr = value; } }
        public float DealAdvisory_ServiceAmount { get { return this._fltDealAdvisory_ServiceAmount; } set { this._fltDealAdvisory_ServiceAmount = value; } }
        public string DealAdvisory_ServiceCurr { get { return this._sDealAdvisory_ServiceCurr; } set { this._sDealAdvisory_ServiceCurr = value; } }
        public float DealAdvisory_MinAmount { get { return this._fltDealAdvisory_MinAmount; } set { this._fltDealAdvisory_MinAmount = value; } }
        public string DealAdvisory_MinCurr { get { return this._sDealAdvisory_MinCurr; } set { this._sDealAdvisory_MinCurr = value; } }
        public int LombardOption_ID { get { return this._iLombardOption_ID; } set { this._iLombardOption_ID = value; } }
        public string LombardOption_Title { get { return this._sLombardOption_Title; } set { this._sLombardOption_Title = value; } }
        public int LombardServiceProvider_ID { get { return this._iLombardServiceProvider_ID; } set { this._iLombardServiceProvider_ID = value; } }
        public string LombardServiceProvider_Title { get { return this._sLombardServiceProvider_Title; } set { this._sLombardServiceProvider_Title = value; } }
        public string Lombard_AMR { get { return this._sLombard_AMR; } set { this._sLombard_AMR = value; } }
        public int FXOption_ID { get { return this._iFXOption_ID; } set { this._iFXOption_ID = value; } }
        public string FXOption_Title { get { return this._sFXOption_Title; } set { this._sFXOption_Title = value; } }
        public int FXServiceProvider_ID { get { return this._iFXServiceProvider_ID; } set { this._iFXServiceProvider_ID = value; } }
        public string FXServiceProvider_Title { get { return this._sFXServiceProvider_Title; } set { this._sFXServiceProvider_Title = value; } }
        public int SettlementsOption_ID { get { return this._iSettlementsOption_ID; } set { this._iSettlementsOption_ID = value; } }
        public string SettlementsOption_Title { get { return this._sSettlementsOption_Title; } set { this._sSettlementsOption_Title = value; } }
        public int SettlementsServiceProvider_ID { get { return this._iSettlementsServiceProvider_ID; } set { this._iSettlementsServiceProvider_ID = value; } }
        public string SettlementsServiceProvider_Title { get { return this._sSettlementsServiceProvider_Title; } set { this._sSettlementsServiceProvider_Title = value; } }
        public int Status { get { return this._iStatus; } set { this._iStatus = value; } }
        public int ClientStatus { get { return this._iClientStatus; } set { this._iClientStatus = value; } }
        public int Advisor_ID { get { return this._iAdvisor_ID; } set { this._iAdvisor_ID = value; } }
        public int Service_ID { get { return this._iService_ID; } set { this._iService_ID = value; } }
        public string Service_Title { get { return this._sService_Title; } set { this._sService_Title = value; } }
        public int ServiceProvider_ID { get { return this._iServiceProvider_ID; } set { this._iServiceProvider_ID = value; } }
        public int ServiceOption_ID { get { return this._iServiceOption_ID; } set { this._iServiceOption_ID = value; } }
        public int CashAccount_ID { get { return this._iCashAccount_ID; } set { this._iCashAccount_ID = value; } }
        public string ClientName { get { return this._sClientName; } set { this._sClientName = value; } }
        public string ClientsFilter { get { return this._sClientsFilter; } set { this._sClientsFilter = value; } }
        public string ClientsList { get { return this._sClientsList; } set { this._sClientsList = value; } }
        public DateTime AktionDate { get { return this._dAktionDate; } set { this._dAktionDate = value; } }
        public DateTime DateStart { get { return _dStart; } set { _dStart = value; } }
        public DateTime DateFinish { get { return _dFinish; } set { _dFinish = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
        public clsContracts_Details Details { get { return _klsDetails; } set { _klsDetails = value; } }
        public clsContracts_Packages Packages { get { return _klsPackages; } set { _klsPackages = value; } }
        public float Amount { get { return this._fltAmount; } set { this._fltAmount = value; } }
        public float CompanyFeesPercent { get { return this._fltCompanyFeesPercent; } set { this._fltCompanyFeesPercent = value; } }
        public string SurnameGreek { get { return this._sSurnameGreek; } set { this._sSurnameGreek = value; } }
        public string SurnameEnglish { get { return this._sSurnameEnglish; } set { this._sSurnameEnglish = value; } }
        public int Division { get { return this._iDivision; } set { this._iDivision = value; } }
        public int DivisionFilter { get { return this._iDivisionFilter; } set { this._iDivisionFilter = value; } }
    }
}
