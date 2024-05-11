using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsOrdersFX
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlConnection conn1 = new SqlConnection(Global.connStr);
        SqlCommand cmd, cmd1;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;
        DataRow[] foundRows;

        private int       _iRecord_ID;
        private string    _sBulkCommand;
        private int       _iBusinessType_ID;                    // 1 - RTO, 2 - Execution
        private int       _iCommandType_ID;                     // 1 - simple command (native client command), 2 - company's command
        private int       _iClient_ID;
        private int       _iCompany_ID;                         // Company ID - executor ID. Always Global.Company_ID
        private int       _iStockCompany_ID;                    
        private int       _iStockExchange_ID;
        private int       _iCustodyProvider_ID;
        private int       _iII_ID;
        private int       _iContract_ID;
        private int       _iContract_Details_ID;
        private int       _iContract_Packages_ID;
        private string    _sCode;
        private string    _sPortfolio;
        private DateTime  _dAktionDate;
        private int       _iTipos;
        private string    _sAmountFrom;
        private string    _sCurrFrom;
        private int       _iCashAccountFrom_ID;
        private string    _sAmountTo;
        private string    _sCurrTo;
        private int       _iCashAccountTo_ID;
        private decimal   _decRate;
        private int       _iConstant;
        private string    _sConstantDate;
        private int       _iConstantContinue;
        private DateTime  _dRecieveDate;
        private int       _iRecieveMethod_ID;
        private DateTime  _dSentDate;
        private string    _sValueDate;
        private DateTime  _dExecuteDate;
        private string    _sOrder_ID;
        private decimal   _decRealAmountFrom;
        private int       _iRealCashAccountFrom_ID;
        private decimal   _decRealAmountTo;
        private int       _iRealCashAccountTo_ID;
        private double    _dblRealCurrRate;
        private double    _dblFeesRate;
        private double    _dblFeesPercent;
        private double    _dblFeesAmount;
        private string    _sNotes;
        private int       _iInformationMethod_ID;
        private string    _sOfficialInformingDate;
        private int       _iInvoiceTitle_ID;
        private int       _iPinakidio;
        private string    _sLastCheckFile;
        private float     _fltRTO_FeesPercent;
        private float     _fltRTO_DiscountPercent;
        private float     _fltRTO_FinishFeesPercent;
        private float     _fltRTO_FeesAmount;
        private string    _sRTO_FeesRate;
        private float     _fltRTO_FeesCurrRate;
        private float     _fltRTO_FeesAmountEUR;
        private int       _iStatus;
        private int       _iUser_ID;
        private DateTime  _dDateIns;

        private string    _sMainCurr;
        private string    _sClientName;
        private string    _sCompanyTitle;
        private string    _sContractTitle;
        private string    _sPackage_Title;
        private string    _sStockCompany_Title;
        private string    _sRecieveTitle;
        private string    _sInformationTitle;
        private string    _sCashAccountFrom;
        private string    _sCashAccountTo;
        private string    _sRealCashAccountFrom;
        private string    _sRealCashAccountTo;
        private int       _iActions;
        private int       _iSent;
        private int       _iUser1_ID;
        private int       _iUser4_ID;
        private int       _iDivision_ID;

        private DateTime  _dDateFrom;
        private DateTime  _dDateTo;
        private DataTable _dtList;

        public clsOrdersFX()
        {
            this._iRecord_ID = 0;
            this._sBulkCommand = "";
            this._iBusinessType_ID = 0;
            this._iCommandType_ID = 0;
            this._iClient_ID = 0;
            this._iCompany_ID = 0;
            this._iStockCompany_ID = 0;
            this._iStockExchange_ID = 0;
            this._iCustodyProvider_ID = 0;
            this._iII_ID = 0;
            this._iContract_ID = 0;
            this._iContract_Details_ID = 0;
            this._iContract_Packages_ID = 0;
            this._sCode = "";
            this._sPortfolio = "";
            this._dAktionDate = Convert.ToDateTime("1900/01/01");
            this._iTipos = 0;
            this._sAmountFrom = "";
            this._sCurrFrom = "";
            this._iCashAccountFrom_ID = 0;
            this._sAmountTo = "";
            this._sCurrTo = "";
            this._iCashAccountTo_ID = 0;
            this._decRate = 0;
            this._iConstant = 0;
            this._sConstantDate = "";
            this._iConstantContinue = 0;
            this._dRecieveDate = Convert.ToDateTime("1900/01/01");
            this._iRecieveMethod_ID = 0;
            this._dSentDate = Convert.ToDateTime("1900/01/01");
            this._sValueDate = "1900/01/01";
            this._dExecuteDate = Convert.ToDateTime("1900/01/01");
            this._sOrder_ID = "";
            this._decRealAmountFrom = 0;
            this._iRealCashAccountFrom_ID = 0;
            this._decRealAmountTo = 0;
            this._iRealCashAccountTo_ID = 0;
            this._dblRealCurrRate = 0;
            this._dblFeesRate = 0;
            this._dblFeesPercent = 0;
            this._dblFeesAmount = 0;
            this._sNotes = "";
            this._iInformationMethod_ID = 0;
            this._sOfficialInformingDate = "";
            this._iInvoiceTitle_ID = 0;
            this._iPinakidio = 0;
            this._sLastCheckFile = "";
            this._fltRTO_FeesPercent = 0;
            this._fltRTO_DiscountPercent = 0;
            this._fltRTO_FinishFeesPercent = 0;
            this._fltRTO_FeesAmount = 0;
            this._sRTO_FeesRate = "";
            this._fltRTO_FeesCurrRate = 0;
            this._fltRTO_FeesAmountEUR = 0;
            this._iStatus = 0;
            this._iUser_ID = 0;
            this._dDateIns = Convert.ToDateTime("1900/01/01");

            this._sMainCurr = "";
            this._sClientName = "";
            this._sCompanyTitle = "";
            this._sPackage_Title = "";
            this._sContractTitle = "";
            this._sStockCompany_Title = "";
            this._sRecieveTitle = "";
            this._sInformationTitle = "";
            this._iActions = 0;
            this._iSent = 0;
            this._iUser1_ID = 0;
            this._iUser4_ID = 0;
            this._iDivision_ID = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("sp_GetCommandFX", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", this._iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._sBulkCommand = drList["BulkCommand"] + "";
                    this._iBusinessType_ID = Convert.ToInt32(drList["BusinessType_ID"]);
                    this._iCommandType_ID = Convert.ToInt32(drList["CommandType_ID"]);
                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    this._iCompany_ID = Convert.ToInt32(drList["Company_ID"]);
                    this._iStockCompany_ID = Convert.ToInt32(drList["StockCompany_ID"]);
                    this._iStockExchange_ID = Convert.ToInt32(drList["StockExchange_ID"]);
                    this._iCustodyProvider_ID = Convert.ToInt32(drList["CustodyProvider_ID"]);
                    this._iII_ID = Convert.ToInt32(drList["II_ID"]);
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iContract_Details_ID = Convert.ToInt32(drList["Contract_Details_ID"]);
                    this._iContract_Packages_ID = Convert.ToInt32(drList["Contract_Packages_ID"]);
                    this._sCode = drList["Code"] + "";
                    this._sPortfolio = drList["Portfolio"] + "";
                    this._dAktionDate = Convert.ToDateTime(drList["AktionDate"]);
                    this._iTipos = Convert.ToInt32(drList["Tipos"]);
                    this._sMainCurr = drList["MainCurr"] + "";
                    this._sAmountFrom = drList["AmountFrom"] + "";
                    this._sCurrFrom = drList["CurrFrom"] + "";
                    this._iCashAccountFrom_ID = Convert.ToInt32(drList["CashAccountFrom_ID"]);
                    this._sCashAccountFrom =drList["CashAccount_From"] + "";
                    this._sAmountTo = drList["AmountTo"] + "";
                    this._sCurrTo = drList["CurrTo"] + "";
                    this._iCashAccountTo_ID = Convert.ToInt32(drList["CashAccountTo_ID"]);
                    this._sCashAccountTo = drList["CashAccount_To"] + "";
                    this._decRate = Convert.ToDecimal(drList["Rate"]);
                    this._iConstant = Convert.ToInt32(drList["Constant"]);
                    if (Convert.ToInt32(drList["Constant"]) == 2) this._sConstantDate = drList["ConstantDate"] + "";
                    else this._sConstantDate = "";
                    this._iConstantContinue = Convert.ToInt32(drList["ConstantContinue"]);
                    this._dRecieveDate = Convert.ToDateTime(drList["RecieveDate"]);
                    this._iRecieveMethod_ID = Convert.ToInt32(drList["RecieveMethod_ID"]);
                    this._sRecieveTitle = drList["RecieveTitle"] + "";
                    this._dSentDate = Convert.ToDateTime(drList["SentDate"]);
                    this._sValueDate = drList["ValueDate"] + "";
                    this._dExecuteDate = Convert.ToDateTime(drList["ExecuteDate"]); 
                    this._sOrder_ID = drList["Order_ID"] + "";
                    this._decRealAmountFrom = Convert.ToDecimal(drList["RealAmountFrom"]);
                    this._iRealCashAccountFrom_ID = Convert.ToInt32(drList["RealCashAccountFrom_ID"]);
                    this._sRealCashAccountFrom = drList["RealCashAccount_From"] + "";
                    this._decRealAmountTo = Convert.ToDecimal(drList["RealAmountTo"]);
                    this._iRealCashAccountTo_ID = Convert.ToInt32(drList["RealCashAccountTo_ID"]);
                    this._sRealCashAccountTo = drList["RealCashAccount_To"] + "";
                    this._dblRealCurrRate = Convert.ToDouble(drList["RealCurrRate"]);
                    this._dblFeesRate = Convert.ToDouble(drList["FeesRate"]);
                    this._dblFeesPercent = Convert.ToDouble(drList["FeesPercent"]);
                    this._dblFeesAmount = Convert.ToDouble(drList["FeesAmount"]);
                    this._sNotes = drList["Notes"] + "";
                    this._sInformationTitle = drList["InformationTitle"] + "";
                    this._iInformationMethod_ID = Convert.ToInt32(drList["InformationMethod_ID"]);
                    this._sOfficialInformingDate = drList["OfficialInformingDate"] + "";
                    this._iInvoiceTitle_ID = Convert.ToInt32(drList["InvoiceTitle_ID"]);
                    this._iPinakidio = Convert.ToInt32(drList["Pinakidio"]);
                    this._sLastCheckFile = drList["LastCheckFile"] + "";
                    this._fltRTO_FeesPercent = Convert.ToSingle(drList["RTO_FeesPercent"]);
                    this._fltRTO_DiscountPercent = Convert.ToSingle(drList["RTO_DiscountPercent"]);
                    this._fltRTO_FinishFeesPercent = Convert.ToSingle(drList["RTO_FinishFeesPercent"]);
                    this._fltRTO_FeesAmount = Convert.ToSingle(drList["RTO_FeesAmount"]);
                    this._sRTO_FeesRate = drList["RTO_FeesRate"] + "";
                    //this._fltRTO_FeesCurrRate = Convert.ToSingle(drList["RTO_FeesCurrRate"]);
                    this._fltRTO_FeesAmountEUR = Convert.ToSingle(drList["RTO_FeesAmountEUR"]);
                    this._iStatus = Convert.ToInt32(drList["Status"]);
                    this._iUser_ID = Convert.ToInt32(drList["User_ID"]);
                    this._dDateIns = Convert.ToDateTime(drList["DateIns"]);

                    if (Convert.ToInt32(drList["Client_ID"]) != 0) {
                        if (Convert.ToInt32(drList["ClientTipos"]) == 1)
                            this._sClientName = (drList["ClientSurname"] + " " + drList["ClientFirstname"]).Trim();
                        else
                            this._sClientName = drList["ClientSurname"].ToString();
                    }
                    else  this._sClientName = "";
                    this._sCompanyTitle = drList["Company_Title"] + "";
                    this._sContractTitle = drList["ContractTitle"] + "";
                    this._sPackage_Title = drList["Package_Title"] + "  ver." + drList["PackageVersion"];
                    this._sStockCompany_Title = drList["StockCompany_Title"] + "";
                    this._sInformationTitle = drList["InformationTitle"] + "";
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            int iOld_ID = -999;
            try
            {
                _dtList = new DataTable("OrdersFX_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ImageType", Type.GetType("System.Int16"));                
                dtCol = _dtList.Columns.Add("BulkCommand", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("BusinessType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("CommandType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Tipos", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ClientTipos", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("ClientName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AFM", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DOY", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("BornPlace", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("EMail", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Address", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("City", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ZIP", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InvAddress", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InvCity", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InvZIP", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CountryTitleGr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CountryTitleEn", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Contract_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contracts_Details_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contracts_Packages_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ServiceTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProfileTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockCompany_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StockCompanyTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockExchange_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StockExchangeTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("PackageProvider_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Company_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CashAccount_From", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AmountFrom", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CurrFrom", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CashAccount_To", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AmountTo", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CurrTo", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Constant", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("ConstantDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RealCashAccount_From", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RealAmountFrom", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RealCashAccount_To", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RealAmountTo", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RealCurrRate", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RecieveDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SentDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("ExecuteDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("RecieveTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InformationTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RTO_FeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RTO_DiscountPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RTO_FinishFeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RTO_FeesAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RTO_FeesRate", System.Type.GetType("System.String"));
                //dtCol = _dtList.Columns.Add("RTO_FeesCurrRate", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RTO_FeesAmountEUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Invoice_Type", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Invoice_Num", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Inv_DateIns", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InvoiceTitle_ID", System.Type.GetType("System.Int32"));                
                dtCol = _dtList.Columns.Add("Filename", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Author_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Advisor_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RM_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Diax_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Intro_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Contract_FeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Contract_FeesDiscount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Contract_FXFees", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Check_FileName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Contract_ConnectionMethod", System.Type.GetType("System.Int16"));                
                dtCol = _dtList.Columns.Add("OfficialInformingDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("DateIns", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("User_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetCommandsFX_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CommandType_ID", _iCommandType_ID));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dDateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dDateTo));
                cmd.Parameters.Add(new SqlParameter("@StockCompany_ID", _iStockCompany_ID));
                cmd.Parameters.Add(new SqlParameter("@Sent", _iSent));
                cmd.Parameters.Add(new SqlParameter("@Actions", _iActions));
                cmd.Parameters.Add(new SqlParameter("@User_ID", _iUser_ID));
                cmd.Parameters.Add(new SqlParameter("@User1_ID", _iUser1_ID));
                cmd.Parameters.Add(new SqlParameter("@User4_ID", _iUser4_ID));
                cmd.Parameters.Add(new SqlParameter("@Division_ID", _iDivision_ID));
                cmd.Parameters.Add(new SqlParameter("@ClientCode", _sCode));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if (iOld_ID != Convert.ToInt32(drList["ID"]))  {
                        iOld_ID = Convert.ToInt32(drList["ID"]);

                        dtRow = _dtList.NewRow();
                        if (Convert.ToInt32(drList["CommandType_ID"]) == 1)
                        {
                            if (Convert.ToInt32(drList["Client_Tipos"]) == 1)
                                _sClientName = drList["Surname"] + " " + drList["Firstname"];
                            else
                                _sClientName = drList["Surname"] + "";
                        }
                        else _sClientName = drList["StockCompanyTitle"] + "";   // it's Company_ID.Title because Client_ID = 0  for CommandsFX.CommandType_ID <> 1

                        dtRow["ID"] = drList["ID"];
                        dtRow["ImageType"] = (drList["FileName"] + "" == "") ? 0 : 1;
                        dtRow["BulkCommand"] = drList["BulkCommand"] + "";
                        dtRow["BusinessType_ID"] = drList["BusinessType_ID"];
                        dtRow["CommandType_ID"] = drList["CommandType_ID"];
                        dtRow["Tipos"] = drList["Tipos"];
                        dtRow["Client_ID"] = drList["Client_ID"];
                        dtRow["ClientTipos"] = drList["Client_Tipos"];
                        dtRow["ClientName"] = _sClientName;
                        dtRow["AFM"] = drList["InvAFM"] + "";
                        dtRow["DOY"] = drList["InvDOY"] + "";                        
                        dtRow["BornPlace"] = drList["BornPlace"] + "";
                        dtRow["Email"] = drList["EMail"] + "";
                        dtRow["Address"] = drList["Address"] + "";
                        dtRow["City"] = drList["City"] + "";
                        dtRow["ZIP"] = drList["ZIP"] + "";
                        dtRow["InvAddress"] = drList["InvAddress"] + "";
                        dtRow["InvCity"] = drList["InvCity"] + "";
                        dtRow["InvZIP"] = drList["InvZIP"] + "";
                        dtRow["CountryTitleGr"] = drList["CountryTitleGr"] + "";
                        dtRow["CountryTitleEn"] = drList["CountryTitleEn"] + "";
                        dtRow["Contract_ID"] = drList["Contract_ID"];
                        dtRow["Contracts_Details_ID"] = drList["Contracts_Details_ID"];
                        dtRow["Contracts_Packages_ID"] = drList["Contracts_Packages_ID"];
                        dtRow["ContractTitle"] = drList["ContractTitle"] + "";
                        dtRow["ServiceTitle"] = drList["ServiceTitle"] + "";
                        dtRow["ProfileTitle"] = drList["ProfileTitle"] + "";
                        dtRow["StockCompany_ID"] = drList["StockCompany_ID"];
                        dtRow["StockCompanyTitle"] = drList["StockCompanyTitle"];
                        dtRow["StockExchange_ID"] = drList["StockExchange_ID"];
                        dtRow["StockExchangeTitle"] = drList["StockExchangeTitle"];
                        dtRow["PackageProvider_Title"] = drList["PackageProvider_Title"];
                        dtRow["Company_Title"] = drList["Company_Title"];
                        dtRow["Code"] = drList["Code"];
                        dtRow["Portfolio"] = drList["Portfolio"];
                        dtRow["CashAccount_From"] = drList["CashAccount_From"];
                        dtRow["AmountFrom"] = drList["AmountFrom"];
                        dtRow["CurrFrom"] = drList["CurrFrom"];
                        dtRow["CashAccount_To"] = drList["CashAccount_To"];
                        dtRow["AmountTo"] = drList["AmountTo"];
                        dtRow["CurrTo"] = drList["CurrTo"];
                        dtRow["Constant"] = drList["Constant"];
                        dtRow["ConstantDate"] = drList["ConstantDate"] + "";
                        dtRow["RealCashAccount_From"] = drList["RealCashAccount_From"];
                        dtRow["RealAmountFrom"] = drList["RealAmountFrom"];
                        dtRow["RealCashAccount_To"] = drList["RealCashAccount_To"];
                        dtRow["RealAmountTo"] = drList["RealAmountTo"];
                        dtRow["RealCurrRate"] = drList["RealCurrRate"];
                        dtRow["RecieveDate"] = drList["RecieveDate"];
                        dtRow["SentDate"] = drList["SentDate"];
                        dtRow["ExecuteDate"] = drList["ExecuteDate"];
                        dtRow["RecieveTitle"] = drList["RecieveTitle"] + "";
                        dtRow["InformationTitle"] = drList["InformationTitle"] + "";
                        dtRow["Notes"] = drList["Notes"] + "";
                        dtRow["RTO_FeesPercent"] = drList["RTO_FeesPercent"];
                        dtRow["RTO_DiscountPercent"] = drList["RTO_DiscountPercent"];
                        dtRow["RTO_FinishFeesPercent"] = drList["RTO_FinishFeesPercent"];
                        dtRow["RTO_FeesAmount"] = drList["RTO_FeesAmount"];
                        dtRow["RTO_FeesRate"] = drList["RTO_FeesRate"];
                        //dtRow["RTO_FeesCurrRate"] = drList["RTO_FeesCurrRate"];
                        dtRow["RTO_FeesAmountEUR"] = drList["RTO_FeesAmountEUR"];
                        //dtRow["Invoice_Type"] = drList["Invoice_Type"];
                        //if (Convert.ToInt32(dtRow["Invoice_Type"]) == 0)
                        //{
                        if (Convert.ToInt32(drList["Client_ID"]) != 0) {
                            if (Convert.ToInt32(drList["Client_Tipos"]) == 1) dtRow["Invoice_Type"] = 1;
                            else dtRow["Invoice_Type"] = 2;
                        }
                        //}
                        dtRow["Invoice_Num"] = (drList["Inv_Code"] + " " + (drList["Inv_Seira"] + " " + drList["Inv_Arithmos"]).Trim()).Trim();
                        dtRow["Inv_DateIns"] = drList["Inv_DateIns"] + "";
                        dtRow["InvoiceTitle_ID"] = drList["InvoiceTitle_ID"];
                        dtRow["Filename"] = drList["Filename"] + "";
                        dtRow["Author_Fullname"] = drList["AuthorSurname"] + " " + drList["AuthorFirstname"];
                        dtRow["Advisor_Fullname"] = drList["AdvisorSurname"] + " " + drList["AdvisorFirstname"];
                        dtRow["RM_Fullname"] = drList["RMSurname"] + " " + drList["RMFirstname"];
                        dtRow["Diax_Fullname"] = drList["DiaxSurname"] + " " + drList["DiaxFirstname"];
                        dtRow["Intro_Fullname"] = drList["IntroSurname"] + " " + drList["IntroFirstname"];
                        dtRow["FeesPercent"] = drList["FeesPercent"];
                        dtRow["FeesAmount"] = drList["FeesAmount"];

                        dtRow["Contract_FeesPercent"] = Global.IsNumeric(drList["Contract_FeesPercent"]) ? Convert.ToSingle(drList["Contract_FeesPercent"]) : 0;
                        dtRow["Contract_FeesDiscount"] = Global.IsNumeric(drList["FXFees_Discount"]) ? Convert.ToSingle(drList["FXFees_Discount"]) : 0;
                        dtRow["Contract_FXFees"] = Global.IsNumeric(drList["FXFees"])? Convert.ToSingle(drList["FXFees"]) : 0;
                        
                        dtRow["Check_FileName"] = drList["Check_FileName"];
                        dtRow["Contract_ConnectionMethod"] = Convert.ToInt16(drList["ConnectionMethod"]);                        
                        dtRow["OfficialInformingDate"] = drList["OfficialInformDate"] + "";      // drList["OfficialInformingDate"]
                        dtRow["Status"] = drList["Status"];
                        dtRow["DateIns"] = drList["DateIns"];
                        dtRow["User_ID"] = drList["User_ID"];
                        _dtList.Rows.Add(dtRow);
                    }
                }
                drList.Close();
            }
            catch (Exception ex) { 
                MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetInvoicesList()
        {
            int iOld_ID = -999;
            try
            {
                _dtList = new DataTable("OrdersFX_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ImageType", Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("BulkCommand", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("BusinessType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("CommandType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Tipos", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ClientTipos", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("ClientName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AFM", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DOY", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("BornPlace", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("EMail", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Address", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("City", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ZIP", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InvAddress", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InvCity", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InvZIP", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CountryTitleGr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CountryTitleEn", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Contract_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contracts_Details_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contracts_Packages_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ServiceTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProfileTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockCompany_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StockCompanyTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockExchange_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StockExchangeTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("PackageProvider_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Company_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CashAccount_From", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AmountFrom", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CurrFrom", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CashAccount_To", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AmountTo", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CurrTo", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Constant", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("ConstantDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RealCashAccount_From", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RealAmountFrom", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RealCashAccount_To", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RealAmountTo", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RealCurrRate", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RecieveDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SentDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("ExecuteDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("RecieveTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InformationTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RTO_FeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RTO_DiscountPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RTO_FinishFeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RTO_FeesAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RTO_FeesRate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RTO_FeesCurrRate", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RTO_FeesAmountEUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Invoice_Type", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Invoice_Num", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Inv_DateIns", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InvoiceTitle_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Filename", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Author_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Advisor_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RM_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Diax_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Intro_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Contract_FeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Contract_FeesDiscount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Contract_FXFees", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Check_FileName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Contract_ConnectionMethod", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("OfficialInformingDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("DateIns", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("User_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetCommandsFX_InvoicesList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CommandType_ID", _iCommandType_ID));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dDateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dDateTo));
                cmd.Parameters.Add(new SqlParameter("@StockCompany_ID", _iStockCompany_ID));
                cmd.Parameters.Add(new SqlParameter("@Sent", _iSent));
                cmd.Parameters.Add(new SqlParameter("@Actions", _iActions));
                cmd.Parameters.Add(new SqlParameter("@User_ID", _iUser_ID));
                cmd.Parameters.Add(new SqlParameter("@User1_ID", _iUser1_ID));
                cmd.Parameters.Add(new SqlParameter("@User4_ID", _iUser4_ID));
                cmd.Parameters.Add(new SqlParameter("@Division_ID", _iDivision_ID));
                cmd.Parameters.Add(new SqlParameter("@ClientCode", _sCode));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if (iOld_ID != Convert.ToInt32(drList["ID"]))
                    {
                        iOld_ID = Convert.ToInt32(drList["ID"]);

                        dtRow = _dtList.NewRow();
                        if (Convert.ToInt32(drList["CommandType_ID"]) == 1)
                        {
                            if (Convert.ToInt32(drList["Client_Tipos"]) == 1)
                                _sClientName = drList["Surname"] + " " + drList["Firstname"];
                            else
                                _sClientName = drList["Surname"] + "";
                        }
                        else _sClientName = drList["StockCompanyTitle"] + "";   // it's Company_ID.Title because Client_ID = 0  for CommandsFX.CommandType_ID <> 1

                        dtRow["ID"] = drList["ID"];
                        dtRow["ImageType"] = (drList["FileName"] + "" == "") ? 0 : 1;
                        dtRow["BulkCommand"] = drList["BulkCommand"] + "";
                        dtRow["BusinessType_ID"] = drList["BusinessType_ID"];
                        dtRow["CommandType_ID"] = drList["CommandType_ID"];
                        dtRow["Tipos"] = drList["Tipos"];
                        dtRow["Client_ID"] = drList["Client_ID"];
                        dtRow["ClientTipos"] = drList["Client_Tipos"];
                        dtRow["ClientName"] = _sClientName;
                        dtRow["AFM"] = drList["InvAFM"] + "";
                        dtRow["DOY"] = drList["InvDOY"] + "";
                        dtRow["BornPlace"] = drList["BornPlace"] + "";
                        dtRow["Email"] = drList["EMail"] + "";
                        dtRow["Address"] = drList["Address"] + "";
                        dtRow["City"] = drList["City"] + "";
                        dtRow["ZIP"] = drList["ZIP"] + "";
                        dtRow["InvAddress"] = drList["InvAddress"] + "";
                        dtRow["InvCity"] = drList["InvCity"] + "";
                        dtRow["InvZIP"] = drList["InvZIP"] + "";
                        dtRow["CountryTitleGr"] = drList["CountryTitleGr"] + "";
                        dtRow["CountryTitleEn"] = drList["CountryTitleEn"] + "";
                        dtRow["Contract_ID"] = drList["Contract_ID"];
                        dtRow["Contracts_Details_ID"] = drList["Contracts_Details_ID"];
                        dtRow["Contracts_Packages_ID"] = drList["Contracts_Packages_ID"];
                        dtRow["ContractTitle"] = drList["ContractTitle"] + "";
                        dtRow["ServiceTitle"] = drList["ServiceTitle"] + "";
                        dtRow["ProfileTitle"] = drList["ProfileTitle"] + "";
                        dtRow["StockCompany_ID"] = drList["StockCompany_ID"];
                        dtRow["StockCompanyTitle"] = drList["StockCompanyTitle"];
                        dtRow["StockExchange_ID"] = drList["StockExchange_ID"];
                        dtRow["StockExchangeTitle"] = drList["StockExchangeTitle"];
                        dtRow["PackageProvider_Title"] = drList["PackageProvider_Title"];
                        dtRow["Company_Title"] = drList["Company_Title"];
                        dtRow["Code"] = drList["Code"];
                        dtRow["Portfolio"] = drList["Portfolio"];
                        dtRow["CashAccount_From"] = drList["CashAccount_From"];
                        dtRow["AmountFrom"] = drList["AmountFrom"];
                        dtRow["CurrFrom"] = drList["CurrFrom"];
                        dtRow["CashAccount_To"] = drList["CashAccount_To"];
                        dtRow["AmountTo"] = drList["AmountTo"];
                        dtRow["CurrTo"] = drList["CurrTo"];
                        dtRow["Constant"] = drList["Constant"];
                        dtRow["ConstantDate"] = drList["ConstantDate"] + "";
                        dtRow["RealCashAccount_From"] = drList["RealCashAccount_From"];
                        dtRow["RealAmountFrom"] = drList["RealAmountFrom"];
                        dtRow["RealCashAccount_To"] = drList["RealCashAccount_To"];
                        dtRow["RealAmountTo"] = drList["RealAmountTo"];
                        dtRow["RealCurrRate"] = drList["RealCurrRate"];
                        dtRow["RecieveDate"] = drList["RecieveDate"];
                        dtRow["SentDate"] = drList["SentDate"];
                        dtRow["ExecuteDate"] = drList["ExecuteDate"];
                        dtRow["RecieveTitle"] = drList["RecieveTitle"] + "";
                        dtRow["InformationTitle"] = drList["InformationTitle"] + "";
                        dtRow["Notes"] = drList["Notes"] + "";
                        dtRow["RTO_FeesPercent"] = drList["RTO_FeesPercent"];
                        dtRow["RTO_DiscountPercent"] = drList["RTO_DiscountPercent"];
                        dtRow["RTO_FinishFeesPercent"] = drList["RTO_FinishFeesPercent"];
                        dtRow["RTO_FeesAmount"] = drList["RTO_FeesAmount"];
                        dtRow["RTO_FeesRate"] = drList["RTO_FeesRate"] + "";
                        dtRow["RTO_FeesCurrRate"] = drList["RTO_FeesCurrRate"];
                        dtRow["RTO_FeesAmountEUR"] = drList["RTO_FeesAmountEUR"];
                        //dtRow["Invoice_Type"] = drList["Invoice_Type"];
                        //if (Convert.ToInt32(dtRow["Invoice_Type"]) == 0)
                        //{
                        if (Convert.ToInt32(drList["Client_ID"]) != 0)
                        {
                            if (Convert.ToInt32(drList["Client_Tipos"]) == 1) dtRow["Invoice_Type"] = 1;
                            else dtRow["Invoice_Type"] = 2;
                        }
                        //}
                        dtRow["Invoice_Num"] = (drList["Inv_Code"] + " " + (drList["Inv_Seira"] + " " + drList["Inv_Arithmos"]).Trim()).Trim();
                        dtRow["Inv_DateIns"] = drList["Inv_DateIns"] + "";
                        dtRow["InvoiceTitle_ID"] = drList["InvoiceTitle_ID"];
                        dtRow["Filename"] = drList["Filename"] + "";
                        dtRow["Author_Fullname"] = drList["AuthorSurname"] + " " + drList["AuthorFirstname"];
                        dtRow["Advisor_Fullname"] = drList["AdvisorSurname"] + " " + drList["AdvisorFirstname"];
                        dtRow["RM_Fullname"] = drList["RMSurname"] + " " + drList["RMFirstname"];
                        dtRow["Diax_Fullname"] = drList["DiaxSurname"] + " " + drList["DiaxFirstname"];
                        dtRow["Intro_Fullname"] = drList["IntroSurname"] + " " + drList["IntroFirstname"];
                        dtRow["FeesPercent"] = drList["FeesPercent"];
                        dtRow["FeesAmount"] = drList["FeesAmount"];

                        dtRow["Contract_FeesPercent"] = Global.IsNumeric(drList["Contract_FeesPercent"]) ? Convert.ToSingle(drList["Contract_FeesPercent"]) : 0;
                        dtRow["Contract_FeesDiscount"] = Global.IsNumeric(drList["FXFees_Discount"]) ? Convert.ToSingle(drList["FXFees_Discount"]) : 0;
                        dtRow["Contract_FXFees"] = Global.IsNumeric(drList["FXFees"]) ? Convert.ToSingle(drList["FXFees"]) : 0;

                        dtRow["Check_FileName"] = drList["Check_FileName"];
                        dtRow["Contract_ConnectionMethod"] = Convert.ToInt16(drList["ConnectionMethod"]);
                        dtRow["OfficialInformingDate"] = drList["OfficialInformDate"] + "";      // drList["OfficialInformingDate"]
                        dtRow["Status"] = drList["Status"];
                        dtRow["DateIns"] = drList["DateIns"];
                        dtRow["User_ID"] = drList["User_ID"];
                        _dtList.Rows.Add(dtRow);
                    }
                }
                drList.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally { conn.Close(); }
        }
        public void GetList_SingleOrders()
        {
            int iOld_ID = -999;
            try
            {
                _dtList = new DataTable("OrdersFX_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("BulkCommand", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("BusinessType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("CommandType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Tipos", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ClientTipos", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("ClientName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AFM", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DOY", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("EMail", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Address", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("City", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ZIP", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CountryTitleGr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Contract_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contracts_Details_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contracts_Packages_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ServiceTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProfileTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockCompany_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StockCompanyTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockExchange_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StockExchangeTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CashAccount_From", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AmountFrom", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CurrFrom", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CashAccount_To", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AmountTo", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CurrTo", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Constant", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("ConstantDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RealCashAccount_From", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RealAmountFrom", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RealCashAccount_To", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RealAmountTo", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RealCurrRate", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RecieveDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SentDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("ExecuteDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("RecieveTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InformationTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Invoice_Num", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Inv_DateIns", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Filename", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Author_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Advisor_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RM_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Diax_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Intro_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("DateIns", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("User_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("sp_GetTransactionFX_SimpleCommands", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AktionDate", _dAktionDate));
                cmd.Parameters.Add(new SqlParameter("@BulkCommand", _sBulkCommand));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if (iOld_ID != Convert.ToInt32(drList["ID"]))
                    {
                        iOld_ID = Convert.ToInt32(drList["ID"]);

                        dtRow = _dtList.NewRow();
                        if (Convert.ToInt32(drList["CommandType_ID"]) == 1)
                        {
                            if (Convert.ToInt32(drList["Tipos"]) == 1)
                                _sClientName = drList["Surname"] + " " + drList["Firstname"];
                            else
                                _sClientName = drList["Surname"] + "";
                        }
                        else _sClientName = drList["Company_Title"] + "";

                        dtRow["ID"] = drList["ID"];
                        dtRow["BulkCommand"] = drList["BulkCommand"] + "";
                        dtRow["BusinessType_ID"] = drList["BusinessType_ID"];
                        dtRow["CommandType_ID"] = drList["CommandType_ID"];
                        dtRow["Tipos"] = drList["Tipos"];
                        dtRow["Client_ID"] = drList["Client_ID"];
                        dtRow["ClientTipos"] = drList["Tipos"];
                        dtRow["ClientName"] = _sClientName;
                        dtRow["Code"] = drList["Code"];
                        dtRow["Portfolio"] = drList["Portfolio"];
                        dtRow["CashAccount_From"] = drList["CashAccount_From"];
                        dtRow["AmountFrom"] = drList["AmountFrom"];
                        dtRow["CurrFrom"] = drList["CurrFrom"];
                        dtRow["CashAccount_To"] = drList["CashAccount_To"];
                        dtRow["AmountTo"] = drList["AmountTo"];
                        dtRow["CurrTo"] = drList["CurrTo"];
                        dtRow["RealAmountFrom"] = drList["RealAmountFrom"];
                        dtRow["RealAmountTo"] = drList["RealAmountTo"];

                        /*
                        dtRow["AFM"] = drList["AFM"] + "";
                        dtRow["DOY"] = drList["DOY"] + "";
                        dtRow["Email"] = drList["EMail"] + "";
                        dtRow["Address"] = drList["Address"] + "";
                        dtRow["City"] = drList["City"] + "";
                        dtRow["ZIP"] = drList["ZIP"] + "";
                        dtRow["CountryTitleGr"] = drList["CountryTitleGr"] + "";
                        dtRow["Contract_ID"] = drList["Contract_ID"];
                        dtRow["Contracts_Details_ID"] = drList["Contract_Details_ID"];
                        dtRow["Contracts_Packages_ID"] = drList["Contract_Packages_ID"];
                        dtRow["ContractTitle"] = drList["ContractTitle"] + "";
                        dtRow["ServiceTitle"] = drList["ServiceTitle"] + "";
                        dtRow["ProfileTitle"] = drList["ProfileTitle"] + "";
                        dtRow["StockCompany_ID"] = drList["StockCompany_ID"];
                        dtRow["StockCompanyTitle"] = drList["StockCompanyTitle"];
                        dtRow["StockExchange_ID"] = drList["StockExchange_ID"];
                        dtRow["StockExchangeTitle"] = drList["StockExchangeTitle"];

                      
                        dtRow["Constant"] = drList["Constant"];
                        dtRow["ConstantDate"] = drList["ConstantDate"] + "";
                        dtRow["RealCashAccount_From"] = drList["RealCashAccount_From"];
                        dtRow["RealCashAccount_To"] = drList["RealCashAccount_To"];
                        dtRow["RealCurrRate"] = drList["RealCurrRate"];
                        dtRow["RecieveDate"] = drList["RecieveDate"];
                        dtRow["SentDate"] = drList["SentDate"];
                        dtRow["ExecuteDate"] = drList["ExecuteDate"];
                        dtRow["RecieveTitle"] = drList["RecieveTitle"] + "";
                        dtRow["InformationTitle"] = drList["InformationTitle"] + "";
                        dtRow["Notes"] = drList["Notes"] + "";
                        dtRow["Invoice_Num"] = (drList["Inv_Code"] + " " + (drList["Inv_Seira"] + " " + drList["Inv_Arithmos"]).Trim()).Trim();
                        dtRow["Inv_DateIns"] = drList["Inv_DateIns"] + "";
                        dtRow["Filename"] = drList["Filename"] + "";
                        dtRow["Author_Fullname"] = drList["AuthorSurname"] + " " + drList["AuthorFirstname"];
                        dtRow["Advisor_Fullname"] = drList["AdvisorSurname"] + " " + drList["AdvisorFirstname"];
                        dtRow["RM_Fullname"] = drList["RMSurname"] + " " + drList["RMFirstname"];
                        dtRow["Diax_Fullname"] = drList["DiaxSurname"] + " " + drList["DiaxFirstname"];
                        dtRow["Intro_Fullname"] = drList["IntroSurname"] + " " + drList["IntroFirstname"];
                        dtRow["FeesPercent"] = drList["FeesPercent"];
                        dtRow["FeesAmount"] = drList["FeesAmount"];
                        dtRow["Status"] = drList["Status"];
                        dtRow["DateIns"] = drList["DateIns"];
                        dtRow["User_ID"] = drList["User_ID"];
                        */
                        _dtList.Rows.Add(dtRow);
                    }
                }
                drList.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally { conn.Close(); }
        }
        public void GetList_Effect()
        {
            try
            {
                _dtList = new DataTable("OrdersFX_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("BulkCommand", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("BusinessType_ID", System.Type.GetType("System.Int32"));                
                dtCol = _dtList.Columns.Add("ClientName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CashAccount_From", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AmountFrom", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CurrFrom", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CashAccount_To", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AmountTo", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CurrTo", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RealCashAccount_From", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RealAmountFrom", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RealCashAccount_To", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RealAmountTo", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FeesAmount", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RealCurrRate", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("RecieveDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SentDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("ValueDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ExecuteDate", System.Type.GetType("System.DateTime"));
             
                conn.Open();
                cmd = new SqlCommand("sp_GetCommandsFX_Effect", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@StockCompany_ID", _iStockCompany_ID));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dDateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dDateTo));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                        dtRow = _dtList.NewRow();
                        if (Convert.ToInt32(drList["CommandType_ID"]) == 1) {
                            if (Convert.ToInt32(drList["Client_Tipos"]) == 1) _sClientName = drList["Surname"] + " " + drList["Firstname"];
                            else _sClientName = drList["Surname"] + "";
                        }
                        else _sClientName = drList["Company_Title"] + "";

                        dtRow["ID"] = drList["ID"];
                        dtRow["BulkCommand"] = drList["BulkCommand"] + "";
                        dtRow["BusinessType_ID"] = drList["BusinessType_ID"];
                        dtRow["ClientName"] = _sClientName;
                        dtRow["ContractTitle"] = drList["ContractTitle"] + "";                     
                        dtRow["Code"] = drList["Code"];
                        dtRow["Portfolio"] = drList["Portfolio"];
                        //dtRow["CashAccount_From"] = drList["CashAccount_From"];
                        dtRow["AmountFrom"] = drList["AmountFrom"];
                        dtRow["CurrFrom"] = drList["CurrFrom"];
                        //dtRow["CashAccount_To"] = drList["CashAccount_To"];
                        dtRow["AmountTo"] = drList["AmountTo"];
                        dtRow["CurrTo"] = drList["CurrTo"];
                        //dtRow["RealCashAccount_From"] = drList["RealCashAccount_From"];
                        dtRow["RealAmountFrom"] = drList["RealAmountFrom"];
                        //dtRow["RealCashAccount_To"] = drList["RealCashAccount_To"];
                        dtRow["RealAmountTo"] = drList["RealAmountTo"];
                        dtRow["FeesAmount"] = drList["FeesAmount"];
                        dtRow["RealCurrRate"] = drList["RealCurrRate"];
                        dtRow["RecieveDate"] = drList["RecieveDate"];
                        dtRow["SentDate"] = drList["SentDate"];
                        dtRow["ValueDate"] = drList["ValueDate"];
                        dtRow["ExecuteDate"] = drList["ExecuteDate"];                       
                        _dtList.Rows.Add(dtRow);
                   }
                drList.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally { conn.Close(); }
        }
        public void GetRecievedFiles()
        {
            try
            {
                _dtList = new DataTable("CommandsFXRecievedFilesList");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DateIns", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Method_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FileName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Method_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("sp_GetCommandsFXRecieved", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["DateIns"] = Convert.ToDateTime(drList["DateIns"]).ToString("dd/MM/yyyy HH:mm:ss");
                    this.dtRow["Method_Title"] = drList["Title"] + "";
                    this.dtRow["FileName"] = drList["FileName"] + "";
                    this.dtRow["Method_ID"] = Convert.ToInt32(drList["Method_ID"]);
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetInformings()
        {
            try
            {
                _dtList = new DataTable("CommandsFXInformingsList");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DateIns", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InformationMethod", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FileName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DateSent", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InformMethod", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("User_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("sp_GetCommandsFX_Inform", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["DateIns"] = Convert.ToDateTime(drList["InformDate"]).ToString("dd/MM/yyyy");
                    this.dtRow["InformationMethod"] = drList["Title"] + "";
                    this.dtRow["FileName"] = drList["FilePath"] + "";
                    this.dtRow["DateSent"] = Convert.ToDateTime(drList["InformDate"]);
                    this.dtRow["InformMethod"] = Convert.ToInt32(drList["InformMethod"]);
                    this.dtRow["User_ID"] = Convert.ToInt32(drList["User_ID"]);
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int GetNextBulkCommand()
        {
            int iLastBulkCommand = 0;
            try
            {
                conn.Open();
                cmd = new SqlCommand("sp_GetCommandsFX_Next_BulkCommand_ID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    iLastBulkCommand = Convert.ToInt32(drList["LastBulkCommandFX_ID"]);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return iLastBulkCommand;
        }
        public double GetFees()
        {
            _dblFeesRate = 0;
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetClientsPackages_FXData", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Contract_ID", _iContract_ID));
                cmd.Parameters.Add(new SqlParameter("@AktionDate", _dAktionDate));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dblFeesRate = Convert.ToDouble(drList["FXFees"]);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return _dblFeesRate;
        }
        public void GetChecks()
        {
            try
            {
                _dtList = new DataTable("CommandsFXInformingsList");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DateIns", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("UserName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("ProblemType_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProblemType_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FileName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ReversalRequestDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("User_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("sp_GetCommandsFX_Check", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["DateIns"] = drList["DateIns"] + "";
                    this.dtRow["UserName"] = (drList["Surname"] + " " + drList["Firstname"]).Trim();
                    this.dtRow["Status"] = Convert.ToInt16(drList["Status"]);
                    this.dtRow["ProblemType_Title"] = drList["ProblemType_Title"] + "";
                    this.dtRow["ProblemType_ID"] = Convert.ToInt16(drList["ProblemType_ID"]);
                    this.dtRow["Notes"] = drList["Notes"] + "";
                    this.dtRow["FileName"] = drList["FileName"] + "";
                    this.dtRow["ReversalRequestDate"] = drList["ReversalRequestDate"] + "";
                    this.dtRow["User_ID"] = Convert.ToInt32(drList["User_ID"]);
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList_ConstantNonContinue()
        {
            try
            {
                _dtList = new DataTable("OrdersFXList");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("BulkCommand", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("BusinessType_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("CommandType_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Company_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StockCompany_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StockExchange_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("CustodyProvider_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Details_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Packages_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("II_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AktionDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("PriceType", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AmountFrom", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("CurrFrom", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CashAccountFrom_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AmountTo", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("CurrTo", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CashAccountTo_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Rate", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("Constant", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("ConstantDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RecieveDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("RecieveMethod_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("SentDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("ValueDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ExecuteDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("Order_ID", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("User_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DateIns", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("TransferFlag", System.Type.GetType("System.Int16"));

                conn.Open();
                cmd = new SqlCommand("sp_GetCommandsFX_ConstantNonContinue", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AktionDate", _dDateFrom));
                cmd.Parameters.Add(new SqlParameter("@User_ID", _iUser_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["BulkCommand"] = drList["BulkCommand"] + "";
                    this.dtRow["BusinessType_ID"] = drList["BusinessType_ID"];
                    this.dtRow["CommandType_ID"] = drList["CommandType_ID"];
                    this.dtRow["Client_ID"] = drList["Client_ID"];
                    this.dtRow["Company_ID"] = drList["Company_ID"];
                    this.dtRow["StockCompany_ID"] = drList["StockCompany_ID"];
                    this.dtRow["StockExchange_ID"] = drList["StockExchange_ID"];
                    this.dtRow["CustodyProvider_ID"] = drList["CustodyProvider_ID"];
                    this.dtRow["II_ID"] = drList["II_ID"];
                    this.dtRow["Contract_ID"] = drList["Contract_ID"];
                    this.dtRow["Contract_Details_ID"] = drList["Contract_Details_ID"];
                    this.dtRow["Contract_Packages_ID"] = drList["Contract_Packages_ID"];
                    this.dtRow["Code"] = drList["Code"];
                    this.dtRow["Portfolio"] = drList["Portfolio"] + "";
                    this.dtRow["AktionDate"] = drList["AktionDate"];
                    this.dtRow["PriceType"] = drList["Tipos"];
                    this.dtRow["AmountFrom"] = drList["AmountFrom"];
                    this.dtRow["CurrFrom"] = drList["CurrFrom"];
                    this.dtRow["CashAccountFrom_ID"] = drList["CashAccountFrom_ID"];
                    this.dtRow["AmountTo"] = drList["AmountTo"];
                    this.dtRow["CurrTo"] = drList["CurrTo"];
                    this.dtRow["CashAccountTo_ID"] = drList["CashAccountTo_ID"];
                    this.dtRow["Rate"] = drList["Rate"];
                    this.dtRow["Constant"] = drList["Constant"];
                    this.dtRow["ConstantDate"] = drList["ConstantDate"];
                    this.dtRow["RecieveDate"] = drList["RecieveDate"];
                    this.dtRow["RecieveMethod_ID"] = drList["RecieveMethod_ID"];
                    this.dtRow["SentDate"] = drList["SentDate"];
                    this.dtRow["ValueDate"] = drList["ValueDate"];
                    this.dtRow["ExecuteDate"] = drList["ExecuteDate"];
                    this.dtRow["Order_ID"] = drList["Order_ID"];
                    this.dtRow["Notes"] = drList["Notes"];
                    this.dtRow["DateIns"] = drList["DateIns"];
                    this.dtRow["User_ID"] = drList["User_ID"];
                    this.dtRow["TransferFlag"] = 1;
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int InsertRecord()
        {
            _iRecord_ID = 0;
            try
            {
                conn.Open();
                using (cmd = new SqlCommand("InsertCommandFX", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@BulkCommand", SqlDbType.NVarChar, 20).Value = _sBulkCommand;
                    cmd.Parameters.Add("@BusinessType_ID", SqlDbType.Int).Value = _iBusinessType_ID;
                    cmd.Parameters.Add("@CommandType_ID", SqlDbType.Int).Value = _iCommandType_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Company_ID", SqlDbType.Int).Value = _iCompany_ID;
                    cmd.Parameters.Add("@StockCompany_ID", SqlDbType.Int).Value = _iStockCompany_ID;
                    cmd.Parameters.Add("@StockExchange_ID", SqlDbType.Int).Value = _iStockExchange_ID;
                    cmd.Parameters.Add("@CustodyProvider_ID", SqlDbType.Int).Value = _iCustodyProvider_ID;
                    cmd.Parameters.Add("@II_ID", SqlDbType.Int).Value = _iII_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Details_ID", SqlDbType.Int).Value = _iContract_Details_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 30).Value = _sCode;
                    cmd.Parameters.Add("@Portfolio", SqlDbType.NVarChar, 50).Value = _sPortfolio;
                    cmd.Parameters.Add("@AktionDate", SqlDbType.DateTime).Value = _dAktionDate;
                    cmd.Parameters.Add("@Tipos", SqlDbType.Int).Value = _iTipos;
                    cmd.Parameters.Add("@AmountFrom", SqlDbType.NVarChar, 20).Value = _sAmountFrom;
                    cmd.Parameters.Add("@CurrFrom", SqlDbType.NVarChar, 6).Value = _sCurrFrom;
                    cmd.Parameters.Add("@CashAccountFrom_ID", SqlDbType.Int).Value = _iCashAccountFrom_ID;
                    cmd.Parameters.Add("@AmountTo", SqlDbType.NVarChar, 20).Value = _sAmountTo;
                    cmd.Parameters.Add("@CurrTo", SqlDbType.NVarChar, 6).Value = _sCurrTo;
                    cmd.Parameters.Add("@CashAccountTo_ID", SqlDbType.Int).Value = _iCashAccountTo_ID;
                    cmd.Parameters.Add("@Rate", SqlDbType.Decimal).Value = _decRate;
                    cmd.Parameters.Add("@Constant", SqlDbType.Int).Value = _iConstant;
                    cmd.Parameters.Add("@ConstantDate", SqlDbType.NVarChar, 20).Value = _sConstantDate;
                    cmd.Parameters.Add("@ConstantContinue", SqlDbType.Int).Value = _iConstantContinue;
                    cmd.Parameters.Add("@RecieveDate", SqlDbType.DateTime).Value = _dRecieveDate;
                    cmd.Parameters.Add("@RecieveMethod_ID", SqlDbType.Int).Value = _iRecieveMethod_ID;
                    cmd.Parameters.Add("@SentDate", SqlDbType.DateTime).Value = _dSentDate;
                    cmd.Parameters.Add("@ValueDate", SqlDbType.NVarChar, 30).Value = _sValueDate;
                    cmd.Parameters.Add("@ExecuteDate", SqlDbType.DateTime).Value = _dExecuteDate;
                    cmd.Parameters.Add("@Order_ID", SqlDbType.NVarChar, 30).Value = _sOrder_ID;
                    cmd.Parameters.Add("@RealAmountFrom", SqlDbType.Decimal).Value = _decRealAmountFrom;
                    cmd.Parameters.Add("@RealCashAccountFrom_ID", SqlDbType.Int).Value = _iRealCashAccountFrom_ID;
                    cmd.Parameters.Add("@RealAmountTo", SqlDbType.Decimal).Value = _decRealAmountTo;
                    cmd.Parameters.Add("@RealCashAccountTo_ID", SqlDbType.Int).Value = _iRealCashAccountTo_ID;
                    cmd.Parameters.Add("@RealCurrRate", SqlDbType.Decimal).Value = _dblRealCurrRate;
                    cmd.Parameters.Add("@FeesRate", SqlDbType.Float).Value = _dblFeesRate;
                    cmd.Parameters.Add("@FeesPercent", SqlDbType.Float).Value = _dblFeesPercent;
                    cmd.Parameters.Add("@FeesAmount", SqlDbType.Float).Value = _dblFeesAmount;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 1000).Value = _sNotes;
                    cmd.Parameters.Add("@InformationMethod_ID", SqlDbType.Int).Value = _iInformationMethod_ID;
                    cmd.Parameters.Add("@OfficialInformingDate", SqlDbType.NVarChar, 20).Value = _sOfficialInformingDate;
                    cmd.Parameters.Add("@InvoiceTitle_ID", SqlDbType.Int).Value = _iInvoiceTitle_ID;
                    cmd.Parameters.Add("@Pinakidio", SqlDbType.Int).Value = _iPinakidio;
                    cmd.Parameters.Add("@LastCheckFile", SqlDbType.NVarChar, 100).Value = _sLastCheckFile;
                    cmd.Parameters.Add("@RTO_FeesPercent", SqlDbType.Float).Value = _fltRTO_FeesPercent;
                    cmd.Parameters.Add("@RTO_DiscountPercent", SqlDbType.Float).Value = _fltRTO_DiscountPercent;
                    cmd.Parameters.Add("@RTO_FinishFeesPercent", SqlDbType.Float).Value = _fltRTO_FinishFeesPercent;
                    cmd.Parameters.Add("@RTO_FeesAmount", SqlDbType.Float).Value = _fltRTO_FeesAmount;
                    cmd.Parameters.Add("@RTO_FeesRate", SqlDbType.NVarChar, 20).Value = _sRTO_FeesRate;
                    //cmd.Parameters.Add("@RTO_FeesCurrRate", SqlDbType.Float).Value = _fltRTO_FeesCurrRate;
                    cmd.Parameters.Add("@RTO_FeesAmountEUR", SqlDbType.Float).Value = _fltRTO_FeesAmountEUR;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = _iUser_ID;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dDateIns; 
                    
                     cmd.ExecuteNonQuery();
                    _iRecord_ID = Convert.ToInt32(outParam.Value);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return _iRecord_ID;
        }
        public void EditRecord()
        {
            try
            {
                conn.Open();
                using (cmd = new SqlCommand("EditCommandFX", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@BulkCommand", SqlDbType.NVarChar, 20).Value = _sBulkCommand;
                    cmd.Parameters.Add("@BusinessType_ID", SqlDbType.Int).Value = _iBusinessType_ID;
                    cmd.Parameters.Add("@CommandType_ID", SqlDbType.Int).Value = _iCommandType_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Company_ID", SqlDbType.Int).Value = _iCompany_ID;
                    cmd.Parameters.Add("@StockCompany_ID", SqlDbType.Int).Value = _iStockCompany_ID;
                    cmd.Parameters.Add("@StockExchange_ID", SqlDbType.Int).Value = _iStockExchange_ID;
                    cmd.Parameters.Add("@CustodyProvider_ID", SqlDbType.Int).Value = _iCustodyProvider_ID;
                    cmd.Parameters.Add("@II_ID", SqlDbType.Int).Value = _iII_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Details_ID", SqlDbType.Int).Value = _iContract_Details_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 30).Value = _sCode;
                    cmd.Parameters.Add("@Portfolio", SqlDbType.NVarChar, 50).Value = _sPortfolio;
                    cmd.Parameters.Add("@AktionDate", SqlDbType.DateTime).Value = _dAktionDate;
                    cmd.Parameters.Add("@Tipos", SqlDbType.Int).Value = _iTipos;
                    cmd.Parameters.Add("@AmountFrom", SqlDbType.NVarChar, 20).Value = _sAmountFrom;
                    cmd.Parameters.Add("@CurrFrom", SqlDbType.NVarChar, 6).Value = _sCurrFrom;
                    cmd.Parameters.Add("@CashAccountFrom_ID", SqlDbType.Int).Value = _iCashAccountFrom_ID;
                    cmd.Parameters.Add("@AmountTo", SqlDbType.NVarChar, 20).Value = _sAmountTo;
                    cmd.Parameters.Add("@CurrTo", SqlDbType.NVarChar, 6).Value = _sCurrTo;
                    cmd.Parameters.Add("@CashAccountTo_ID", SqlDbType.Int).Value = _iCashAccountTo_ID;
                    cmd.Parameters.Add("@Rate", SqlDbType.Decimal).Value = _decRate;
                    cmd.Parameters.Add("@Constant", SqlDbType.Int).Value = _iConstant;
                    cmd.Parameters.Add("@ConstantDate", SqlDbType.NVarChar, 20).Value = _sConstantDate;
                    cmd.Parameters.Add("@ConstantContinue", SqlDbType.Int).Value = _iConstantContinue;
                    cmd.Parameters.Add("@RecieveDate", SqlDbType.DateTime).Value = _dRecieveDate;
                    cmd.Parameters.Add("@RecieveMethod_ID", SqlDbType.Int).Value = _iRecieveMethod_ID;
                    cmd.Parameters.Add("@SentDate", SqlDbType.DateTime).Value = _dSentDate;
                    cmd.Parameters.Add("@ValueDate", SqlDbType.NVarChar, 30).Value = _sValueDate;
                    cmd.Parameters.Add("@ExecuteDate", SqlDbType.DateTime).Value = _dExecuteDate;
                    cmd.Parameters.Add("@Order_ID", SqlDbType.NVarChar, 30).Value = _sOrder_ID;
                    cmd.Parameters.Add("@RealAmountFrom", SqlDbType.Decimal).Value = _decRealAmountFrom;
                    cmd.Parameters.Add("@RealCashAccountFrom_ID", SqlDbType.Int).Value = _iRealCashAccountFrom_ID;
                    cmd.Parameters.Add("@RealAmountTo", SqlDbType.Decimal).Value = _decRealAmountTo;
                    cmd.Parameters.Add("@RealCashAccountTo_ID", SqlDbType.Int).Value = _iRealCashAccountTo_ID;
                    cmd.Parameters.Add("@RealCurrRate", SqlDbType.Decimal).Value = _dblRealCurrRate;
                    cmd.Parameters.Add("@FeesRate", SqlDbType.Float).Value = _dblFeesRate;
                    cmd.Parameters.Add("@FeesPercent", SqlDbType.Float).Value = _dblFeesPercent;
                    cmd.Parameters.Add("@FeesAmount", SqlDbType.Float).Value = _dblFeesAmount;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 1000).Value = _sNotes;
                    cmd.Parameters.Add("@InformationMethod_ID", SqlDbType.Int).Value = _iInformationMethod_ID;
                    cmd.Parameters.Add("@OfficialInformingDate", SqlDbType.NVarChar, 20).Value = _sOfficialInformingDate;
                    cmd.Parameters.Add("@InvoiceTitle_ID", SqlDbType.Int).Value = _iInvoiceTitle_ID;
                    cmd.Parameters.Add("@Pinakidio", SqlDbType.Int).Value = _iPinakidio;
                    cmd.Parameters.Add("@LastCheckFile", SqlDbType.NVarChar, 100).Value = _sLastCheckFile;
                    cmd.Parameters.Add("@RTO_FeesPercent", SqlDbType.Float).Value = _fltRTO_FeesPercent;
                    cmd.Parameters.Add("@RTO_DiscountPercent", SqlDbType.Float).Value = _fltRTO_DiscountPercent;
                    cmd.Parameters.Add("@RTO_FinishFeesPercent", SqlDbType.Float).Value = _fltRTO_FinishFeesPercent;
                    cmd.Parameters.Add("@RTO_FeesAmount", SqlDbType.Float).Value = _fltRTO_FeesAmount;
                    cmd.Parameters.Add("@RTO_FeesRate", SqlDbType.NVarChar, 20).Value = _sRTO_FeesRate;
                    //cmd.Parameters.Add("@RTO_FeesCurrRate", SqlDbType.Float).Value = _fltRTO_FeesCurrRate;
                    cmd.Parameters.Add("@RTO_FeesAmountEUR", SqlDbType.Float).Value = _fltRTO_FeesAmountEUR;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = _iUser_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void EditConstantContinue()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_EditRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "CommandsFX";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ConstantContinue";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 500).Value = "1";
                    cmd.Parameters.Add("@Key", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void EditBulkCommand()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_EditBulkCommandFX_ID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@BulkCommand", SqlDbType.NVarChar, 20).Value = _sBulkCommand;
                    cmd.Parameters.Add("@AktionDate", SqlDbType.DateTime).Value = _dAktionDate;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void CalcRTOFees()
        {
            decimal decRTO_FinishFeesPercent = 0, decRTO_FeesAmount = 0, decRTO_FeesAmountEUR = 0;
            string sRTO_FeesRate = "";
            //float fltRTO_FeesCurrRate = 0;

            clsProductsCodes ProductsCode = new clsProductsCodes();
            ProductsCode.DateFrom = _dDateFrom;
            ProductsCode.DateTo = _dDateTo;
            ProductsCode.Code = "EUR";
            ProductsCode.GetPrices_Period();

            try
            {
                int i = 0;
                conn.Open();
                conn1.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM CommandsFX WHERE AktionDate >= '" + _dDateFrom.ToString("yyyy/MM/dd") + "' AND AktionDate <= '" + _dDateTo.ToString("yyyy/MM/dd") + " 23:59:59" +
                         "' AND (ExecuteDate > '1900/01/01') AND (InvoiceTitle_ID = 0) ORDER BY ID DESC", conn);
                cmd.CommandType = CommandType.Text;
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if ((drList["CurrFrom"] + "") == "EUR")
                    {
                        sRTO_FeesRate = "1"; 
                        //fltRTO_FeesCurrRate = 1;
                    }
                    else
                    {
                        //if ((drList["RTO_FeesRate"]+"") == "" || (drList["RTO_FeesRate"] + "") == "0")
                        //{
                        //clsProductsCodes ProductsCode = new clsProductsCodes();
                        //ProductsCode.DateIns = Convert.ToDateTime(drList["AktionDate"]);
                        //ProductsCode.Code = "EUR" + drList["CurrFrom"] + "=";
                        //ProductsCode.GetPrice_Code();
                        foundRows = ProductsCode.List.Select("DateIns = '" + Convert.ToDateTime(drList["AktionDate"]).ToString("yyyy/MM/dd") + "' AND Code = '" + "EUR" + drList["CurrFrom"] + "='");
                        if (foundRows.Length > 0) {
                            sRTO_FeesRate = foundRows[0]["Close"].ToString();              // ProductsCode.LastClosePrice.ToString().Replace(",", ".");
                            //fltRTO_FeesCurrRate = Convert.ToSingle(foundRows[0]["Close"]); //ProductsCode.LastClosePrice;
                        }
                        //}
                    }
                    if (sRTO_FeesRate == "") sRTO_FeesRate = "0";
                    decRTO_FinishFeesPercent = (Convert.ToDecimal(drList["RTO_FeesPercent"]) - Convert.ToDecimal(drList["RTO_FeesPercent"]) * Convert.ToDecimal(drList["RTO_DiscountPercent"]) / 100);
                    decRTO_FeesAmount = (Convert.ToDecimal(drList["RealAmountFrom"]) * Convert.ToDecimal(drList["RTO_FinishFeesPercent"]) / 100);
                    if (Convert.ToDecimal(sRTO_FeesRate) != 0) decRTO_FeesAmountEUR = (Convert.ToDecimal(drList["RTO_FeesAmount"]) / Convert.ToDecimal(sRTO_FeesRate));
                    //if (Convert.ToDecimal(drList["RTO_FeesCurrRate"]) != 0) decRTO_FeesAmountEUR = (Convert.ToDecimal(drList["RTO_FeesAmount"]) / Convert.ToDecimal(drList["RTO_FeesCurrRate"]));
                    else decRTO_FeesAmountEUR = 0;

                    cmd1 = new SqlCommand("UPDATE CommandsFX SET RTO_FinishFeesPercent = " + decRTO_FinishFeesPercent.ToString().Replace(",", ".") +
                                          ", RTO_FeesAmount = " + decRTO_FeesAmount.ToString().Replace(",", ".") +
                                          ", RTO_FeesRate = '" + sRTO_FeesRate.Replace(".", ",") + "'" +
                                          //", RTO_FeesCurrRate = " + fltRTO_FeesCurrRate.ToString().Replace(",", ".") +
                                          ", RTO_FeesAmountEUR = " + decRTO_FeesAmountEUR.ToString().Replace(",", ".") +
                                          " WHERE ID = " + drList["ID"], conn1);
                    cmd1.CommandType = CommandType.Text;
                    cmd1.ExecuteNonQuery();
                    i = i + 1;
                }
                //MessageBox.Show("Records = " + i);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); conn1.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public string BulkCommand { get { return this._sBulkCommand; } set { this._sBulkCommand = value; } }
        public int BusinessType_ID { get { return this._iBusinessType_ID; } set { this._iBusinessType_ID = value; } }
        public int CommandType_ID { get { return this._iCommandType_ID; } set { this._iCommandType_ID = value; } }
        public int Client_ID { get { return this._iClient_ID; } set { this._iClient_ID = value; } }
        public int Company_ID { get { return this._iCompany_ID; } set { this._iCompany_ID = value; } }
        public int StockCompany_ID { get { return this._iStockCompany_ID; } set { this._iStockCompany_ID = value; } }
        public int StockExchange_ID { get { return this._iStockExchange_ID; } set { this._iStockExchange_ID = value; } }
        public int CustodyProvider_ID { get { return this._iCustodyProvider_ID; } set { this._iCustodyProvider_ID = value; } }
        public int II_ID { get { return this._iII_ID; } set { this._iII_ID = value; } }
        public int Contract_ID { get { return this._iContract_ID; } set { this._iContract_ID = value; } }
        public int Contract_Details_ID { get { return this._iContract_Details_ID; } set { this._iContract_Details_ID = value; } }
        public int Contract_Packages_ID { get { return this._iContract_Packages_ID; } set { this._iContract_Packages_ID = value; } }
        public string Code { get { return this._sCode; } set { this._sCode = value; } }
        public string Portfolio { get { return this._sPortfolio; } set { this._sPortfolio = value; } }
        public DateTime AktionDate { get { return this._dAktionDate; } set { this._dAktionDate = value; } }
        public int Tipos { get { return this._iTipos; } set { this._iTipos = value; } }
        public string AmountFrom { get { return this._sAmountFrom; } set { this._sAmountFrom = value; } }
        public string CurrFrom { get { return this._sCurrFrom; } set { this._sCurrFrom = value; } }
        public int CashAccountFrom_ID { get { return this._iCashAccountFrom_ID; } set { this._iCashAccountFrom_ID = value; } }
        public string AmountTo { get { return this._sAmountTo; } set { this._sAmountTo = value; } }
        public string CurrTo { get { return this._sCurrTo; } set { this._sCurrTo = value; } }
        public int CashAccountTo_ID { get { return this._iCashAccountTo_ID; } set { this._iCashAccountTo_ID = value; } }
        public decimal Rate { get { return this._decRate; } set { this._decRate = value; } }
        public int Constant { get { return this._iConstant; } set { this._iConstant = value; } }
        public string ConstantDate { get { return this._sConstantDate; } set { this._sConstantDate = value; } }
        public int ConstantContinue { get { return this._iConstantContinue; } set { this._iConstantContinue = value; } }
        public DateTime RecieveDate { get { return this._dRecieveDate; } set { this._dRecieveDate = value; } }
        public int RecieveMethod_ID { get { return this._iRecieveMethod_ID; } set { this._iRecieveMethod_ID = value; } }
        public DateTime SentDate { get { return this._dSentDate; } set { this._dSentDate = value; } }
        public string ValueDate { get { return this._sValueDate; } set { this._sValueDate = value; } }
        public DateTime ExecuteDate { get { return this._dExecuteDate; } set { this._dExecuteDate = value; } }
        public string Order_ID { get { return this._sOrder_ID; } set { this._sOrder_ID = value; } }
        public decimal RealAmountFrom { get { return this._decRealAmountFrom; } set { this._decRealAmountFrom = value; } }
        public int RealCashAccountFrom_ID { get { return this._iRealCashAccountFrom_ID; } set { this._iRealCashAccountFrom_ID = value; } }
        public decimal RealAmountTo { get { return this._decRealAmountTo; } set { this._decRealAmountTo = value; } }
        public int RealCashAccountTo_ID { get { return this._iRealCashAccountTo_ID; } set { this._iRealCashAccountTo_ID = value; } }
        public double RealCurrRate { get { return this._dblRealCurrRate; } set { this._dblRealCurrRate = value; } }
        public double FeesRate { get { return this._dblFeesRate; } set { this._dblFeesRate = value; } }
        public double FeesPercent { get { return this._dblFeesPercent; } set { this._dblFeesPercent = value; } }
        public double FeesAmount { get { return this._dblFeesAmount; } set { this._dblFeesAmount = value; } }
        public string Notes { get { return this._sNotes; } set { this._sNotes = value; } }
        public int InformationMethod_ID { get { return this._iInformationMethod_ID; } set { this._iInformationMethod_ID = value; } }
        public string OfficialInformingDate { get { return this._sOfficialInformingDate; } set { this._sOfficialInformingDate = value; } }
        public int InvoiceTitle_ID { get { return this._iInvoiceTitle_ID; } set { this._iInvoiceTitle_ID = value; } }
        public int Pinakidio { get { return this._iPinakidio; } set { this._iPinakidio = value; } }
        public string LastCheckFile { get { return this._sLastCheckFile; } set { this._sLastCheckFile = value; } }
        public float RTO_FeesPercent { get { return this._fltRTO_FeesPercent; } set { this._fltRTO_FeesPercent = value; } }
        public float RTO_DiscountPercent { get { return this._fltRTO_DiscountPercent; } set { this._fltRTO_DiscountPercent = value; } }
        public float RTO_FinishFeesPercent { get { return this._fltRTO_FinishFeesPercent; } set { this._fltRTO_FinishFeesPercent = value; } }
        public float RTO_FeesAmount { get { return this._fltRTO_FeesAmount; } set { this._fltRTO_FeesAmount = value; } }
        public string RTO_FeesRate { get { return this._sRTO_FeesRate; } set { this._sRTO_FeesRate = value; } }
        public float RTO_FeesCurrRate { get { return this._fltRTO_FeesCurrRate; } set { this._fltRTO_FeesCurrRate = value; } }
        public float RTO_FeesAmountEUR { get { return this._fltRTO_FeesAmountEUR; } set { this._fltRTO_FeesAmountEUR = value; } }
        public int Status { get { return this._iStatus; } set { this._iStatus = value; } }
        public int Actions { get { return this._iActions; } set { this._iActions = value; } }
        public int Sent { get { return this._iSent; } set { this._iSent = value; } }
        public int User_ID { get { return this._iUser_ID; } set { this._iUser_ID = value; } }
        public int User1_ID { get { return this._iUser1_ID; } set { this._iUser1_ID = value; } }
        public int User4_ID { get { return this._iUser4_ID; } set { this._iUser4_ID = value; } }
        public int Division_ID { get { return this._iDivision_ID; } set { this._iDivision_ID = value; } }
        public DateTime DateIns { get { return this._dDateIns; } set { this._dDateIns = value; } }
        public string CashAccountFrom { get { return this._sCashAccountFrom; } set { this._sCashAccountFrom = value; } }
        public string CashAccountTo { get { return this._sCashAccountTo; } set { this._sCashAccountTo = value; } }
        public string RealCashAccountFrom { get { return this._sRealCashAccountFrom; } set { this._sRealCashAccountFrom = value; } }
        public string RealCashAccountTo { get { return this._sRealCashAccountTo; } set { this._sRealCashAccountTo = value; } }
        public string MainCurr { get { return this._sMainCurr; } set { this._sMainCurr = value; } }
        public DateTime DateFrom { get { return this._dDateFrom; } set { this._dDateFrom = value; } }
        public DateTime DateTo { get { return this._dDateTo; } set { this._dDateTo = value; } }
        public string ClientName { get { return this._sClientName; } set { this._sClientName = value; } }
        public string ContractTitle { get { return this._sContractTitle; } set { this._sContractTitle = value; } }
        public string StockCompany_Title { get { return this._sStockCompany_Title; } set { this._sStockCompany_Title = value; } }
        public string RecieveTitle { get { return this._sRecieveTitle; } set { this._sRecieveTitle = value; } }
        public string InformationTitle { get { return this._sInformationTitle; } set { this._sInformationTitle = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}