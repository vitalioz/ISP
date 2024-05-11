using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace Core
{
    public class clsOrdersSecurity
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlConnection conn1 = new SqlConnection(Global.connStr);
        SqlCommand cmd, cmd1;
        SqlDataReader drList = null, drList1 = null;
        DataColumn dtCol;
        DataRow dtRow;
        DataRow[] foundRows;

        private int _iRecord_ID;
        private string _sBulkCommand;                  // BulkCommand
        private int _iBusinessType_ID;                 // 1 - RTO (HF), 2 - Custody (HFSS)
        private int _iCommandType_ID;                  // 1 - Single Order, 2 - Execution Order, 3 - Bulk Order, 4 - DPM Order
        private int _iClient_ID;     
        private int _iClientTipos;
        private int _iCompany_ID;                      // Company ID - executor ID. Always curComapny
        private int _iServiceProvider_ID;  
        private int _iExecutor_ID;                     // Provider that executes Execution command
        private int _iStockExchange_ID; 
        private int _iCustodyProvider_ID; 
        private int _iDepository_ID;                   
        private int _iII_ID;                           // II_ID,  but if it's DPM Order (_iCommandType_ID = 4) it is a DPMOrders.ID  that "born" this order
        private int _iParent_ID;
        private int _iContract_ID;
        private int _iContract_Details_ID;
        private int _iContract_Packages_ID;
        private int _iContractTipos;
        private string _sContractTitle;
        private string _sCode;
        private string _sProfitCenter;
        private float _fltAllocationPercent;
        private int _iAktion;
        private DateTime _dAktionDate;
        private int _iShare_ID;
        private int _iProduct_ID;
        private int _iProductCategory_ID;
        private int _iProductStockExchange_ID; 
        private int _iPriceType;
        private decimal _decPrice;
        private decimal _decQuantity;
        private decimal _decAmount;
        private string _sCurr;
        private int _iConstant;
        private string _sConstantDate;
        private int _iConstantContinue;
        private DateTime _dRecieveDate;
        private int _iRecieveMethod_ID;
        private int _iBestExecution;
        private DateTime _dSentDate;
        private int _iSendCheck;
        private int _iFIX_A;
        private DateTime _dFIX_RecievedDate;
        private DateTime _dExecuteDate;
        private decimal _decRealPrice;
        private decimal _decRealQuantity;
        private decimal _decRealAmount;
        private int _iExecutionStockExchange_ID;
        private decimal _decFeesDiff;
        private decimal _decFeesMarket;
        private decimal _decAccruedInterest;
        private decimal _decCommission;
        private decimal _decCurrRate;
        private string _sNotes;
        private string _sValueDate;
        private int _iInformationMethod_ID;
        private string _sOfficialInformingDate;
        private int _iUser_ID;
        private DateTime _dDateIns;
        private int _iStatus;
        private DateTime _dSettlementDate;
        private decimal _decFeesPercent;
        private decimal _decFeesAmount;
        private decimal _decFeesDiscountPercent;
        private decimal _decFeesDiscountAmount;
        private decimal _decFinishFeesPercent;
        private decimal _decFinishFeesAmount;
        private decimal _decFeesRate;
        private decimal _decFeesAmountEUR;
        private decimal _decMinFeesAmount;
        private decimal _decMinFeesDiscountPercent;
        private decimal _decMinFeesDiscountAmount;
        private decimal _decFinishMinFeesAmount;
        private decimal _decMinFeesRate;
        private decimal _decMinAmountEUR;
        private string _sTicketFeeCurr;
        private decimal _decTicketFee;
        private decimal _decTicketFeeDiscountPercent;
        private decimal _decTicketFeeDiscountAmount;
        private decimal _decFinishTicketFee;
        private decimal _decTicketFeesRate;
        private decimal _decTicketFeesAmountEUR;
        private decimal _decFeesCalc;
        private decimal _decProviderFees;
        private decimal _decRTO_FeesPercent;
        private decimal _decRTO_FeesAmount;
        private decimal _decRTO_FeesDiscountPercent;
        private decimal _decRTO_FeesDiscountAmount;
        private decimal _decRTO_FinishFeesPercent;
        private decimal _decRTO_FinishFeesAmount;        
        private decimal _decRTO_FeesAmountEUR;
        private string _sRTO_MinFeesCurr;
        private decimal _decRTO_MinFeesAmount;
        private decimal _decRTO_MinFeesDiscountPercent;
        private decimal _decRTO_MinFeesDiscountAmount;
        private decimal _decRTO_FinishMinFeesAmount;
        private string _sRTO_TicketFeeCurr;
        private decimal _decRTO_TicketFee;
        private decimal _decRTO_TicketFeeDiscountPercent;
        private decimal _decRTO_TicketFeeDiscountAmount;
        private decimal _decRTO_FinishTicketFee;
        private decimal _decRTO_FeesProVAT;
        private decimal _decRTO_FeesVAT;
        private decimal _decRTO_CompanyFees;
        private int _iRTO_InvoiceTitle_ID;
        private decimal _decFeesMisc;
        private string _sFeesNotes;
        private int _iFeesCalcMode;
        private decimal _decCompanyFeesPercent;
        private int _iPinakidio;
        private string _sLastCheckFile;
        private decimal _decMinimumFees;
        private decimal _decInvestAmount;
        private string _sMinFeesCurr;
        private string _sCurrency;
        private string _sClientName;
        private string _sCompanyTitle;
        private int _iCFP_ID;
        private int _iPackageType_ID;
        private string _sPackage_Title;
        private string _sProduct_Title;
        private string _sProductCategory_Title;
        private string _sProductStockExchange_MIC;
        private string _sProductStockExchange_Title;
        private string _sStockExchange_MIC;
        private string _sStockExchange_Title;
        private string _sExecutionStockExchange_MIC;
        private string _sExecutionStockExchange_Title;
        private int _iSecurity_Share_ID;
        private string _sSecurity_Code;
        private string _sSecurity_Code2;
        private string _sSecurity_ISIN;
        private string _sSecurity_Title;
        private DateTime _dSecurity_Date1;
        private DateTime _dSecurity_Date3;
        private decimal _decSecurity_Coupone;
        private int _iSecurity_FrequencyClipping;
        private string _sMainCurr;
        private string _sServiceProvider_Title;
        private int _iServiceProviderFeesMode;
        private string _sDepository_Code;
        private string _sAuthorName;
        private string _sAdvisorName;
        private string _sRecieveTitle;
        private string _sInformationTitle;
        private string _sRTO_InvoiceData;
        private int _iMethod_ID;
        private string _sFilePath;
        private string _sFileName;
        private int _iSourceCommand_ID;
        private string _sClientOrderID;
        private int _iSent;
        private int _iActions;
        private int _iUser1_ID;
        private int _iUser4_ID;
        private int _iDivision_ID;
        private int _iShowCancelled;
        private string _sExtraFilter;
        private DateTime _dDateFrom;
        private DateTime _dDateTo;
        private DateTime _dExecDateFrom;
        private DateTime _dExecDateTo;
        private DateTime _dFirstOrderDate;

        private DataTable _dtList;

        public clsOrdersSecurity()
        {
            this._iRecord_ID = 0;                   // Record ID
            this._sBulkCommand = "";                // BulkCommand
            this._iBusinessType_ID = 0;             // 1 - RTO, 2 - Execution, 3 - Bulk, 4 - DPM
            this._iCommandType_ID = 0;              // 1 - simple command (native client command), 2 - company's command
            this._iClient_ID = 0;                   // Client ID
            this._iClientTipos = 0;
            this._iCompany_ID = 0;                  // Company ID - executor ID. Always curComapny
            this._iServiceProvider_ID = 0;          // ServiceProvider ID (Executor ID)
            this._iExecutor_ID = 0;                 // Provider that executes Execution command
            this._iStockExchange_ID = 0;            // StockExchange ID
            this._iCustodyProvider_ID = 0;          // CustodyProvider_ID
            this._iDepository_ID = 0;               
            this._iII_ID = 0;
            this._iParent_ID = 0;
            this._iContract_ID = 0;
            this._iContract_Details_ID = 0;
            this._iContract_Packages_ID = 0;
            this._sContractTitle = "";
            this._sCode = "";
            this._sProfitCenter = "";
            this._fltAllocationPercent = 0;
            this._iAktion = 0;
            this._dAktionDate = Convert.ToDateTime("1900/01/01");
            this._iShare_ID = 0;
            this._iProduct_ID = 0;
            this._iProductCategory_ID = 0;
            this._iProductStockExchange_ID = 0;                     
            this._iPriceType = 0;
            this._decPrice = 0;
            this._decQuantity = 0;
            this._decAmount = 0;
            this._sCurr = "";
            this._iConstant = 0;
            this._sConstantDate = "";
            this._iConstantContinue = 0;
            this._dRecieveDate = Convert.ToDateTime("1900/01/01");
            this._iRecieveMethod_ID = 0;
            this._iBestExecution = 0;
            this._dSentDate = Convert.ToDateTime("1900/01/01");
            this._iSendCheck = 0;
            this._iFIX_A = -1;                                              // -1 - new order (unknown FIX status), 0 - unsent FIX order, 1 - sent FIX order and recieved A status       
            this._dFIX_RecievedDate = Convert.ToDateTime("1900/01/01");
            this._dExecuteDate = Convert.ToDateTime("1900/01/01");
            this._decRealPrice = 0;
            this._decRealQuantity = 0;
            this._decRealAmount = 0;
            this._iExecutionStockExchange_ID = 0;
            this._decFeesDiff = 0;
            this._decFeesMarket = 0;
            this._decAccruedInterest = 0;
            this._decCommission = 0;
            this._decCurrRate = 0;
            this._sNotes = "";
            this._sValueDate = "";
            this._iInformationMethod_ID = 0;
            this._sOfficialInformingDate = "";
            this._iUser_ID = 0;
            this._dDateIns = Convert.ToDateTime("1900/01/01");
            this._iStatus = 0;
            this._dSettlementDate = Convert.ToDateTime("1900/01/01");
            this._decFeesPercent = 0;
            this._decFeesAmount = 0;
            this._decFeesDiscountPercent = 0;
            this._decFeesDiscountAmount = 0;
            this._decFinishFeesPercent = 0;
            this._decFinishFeesAmount = 0;
            this._decFeesRate = 0;
            this._decFeesAmountEUR = 0;
            this._decMinFeesAmount = 0;
            this._decMinFeesDiscountPercent = 0;
            this._decMinFeesDiscountAmount = 0;
            this._decFinishMinFeesAmount = 0;
            this._decMinFeesRate = 0;
            this._decMinAmountEUR = 0;
            this._sTicketFeeCurr = "";
            this._decTicketFee = 0;
            this._decTicketFeeDiscountPercent = 0;
            this._decTicketFeeDiscountAmount = 0;
            this._decFinishTicketFee = 0;
            this._decTicketFeesRate = 0;
            this._decTicketFeesAmountEUR = 0;
            this._decFeesCalc = 0;
            this._decProviderFees = 0;
            this._decRTO_FeesPercent = 0;
            this._decRTO_FeesAmount = 0;
            this._decRTO_FeesDiscountPercent = 0;
            this._decRTO_FeesDiscountAmount = 0;
            this._decRTO_FinishFeesPercent = 0;
            this._decRTO_FinishFeesAmount = 0;            
            this._decRTO_FeesAmountEUR = 0;
            this._sRTO_MinFeesCurr = "";
            this._decRTO_MinFeesAmount = 0;
            this._decRTO_MinFeesDiscountPercent = 0;
            this._decRTO_MinFeesDiscountAmount = 0;
            this._decRTO_FinishMinFeesAmount = 0;
            this._sRTO_TicketFeeCurr = "";
            this._decRTO_TicketFee = 0;
            this._decRTO_TicketFeeDiscountPercent = 0;
            this._decRTO_TicketFeeDiscountAmount = 0;
            this._decRTO_FinishTicketFee = 0;
            this._decRTO_FeesProVAT = 0;
            this._decRTO_FeesVAT = 0;
            this._decRTO_CompanyFees = 0;
            this._iRTO_InvoiceTitle_ID = 0;
            this._decFeesMisc = 0;
            this._sFeesNotes = "";
            this._iFeesCalcMode = 1;                                                            // 1 - Automatical
            this._decCompanyFeesPercent = 0;
            this._iPinakidio = 0;
            this._sLastCheckFile = "";
            this._decMinimumFees = 0;
            this._decInvestAmount = 0;
            this._sMinFeesCurr = "";
            this._sCurrency = "";
            this._sClientName = "";
            this._sCompanyTitle = "";
            this._iCFP_ID = 0;
            this._iPackageType_ID = 0;
            this._sPackage_Title = "";
            this._sProduct_Title = "";
            this._sProductCategory_Title = "";
            this._sProductStockExchange_Title = "";
            this._iSecurity_Share_ID = 0;
            this._sSecurity_Code = "";
            this._sSecurity_Code2 = "";
            this._sSecurity_ISIN = "";
            this._sSecurity_Title = "";
            this._dSecurity_Date1 = Convert.ToDateTime("1900/01/01");
            this._dSecurity_Date3 = Convert.ToDateTime("1900/01/01");
            this._decSecurity_Coupone = 0;
            this._iSecurity_FrequencyClipping = 0;
            this._sMainCurr = "";
            this._sServiceProvider_Title = "";
            this._iServiceProviderFeesMode = 0;
            this._sDepository_Code = "";
            this._sStockExchange_Title = "";
            this._sAuthorName = "";
            this._sAdvisorName = "";
            this._sRecieveTitle = "";
            this._sInformationTitle = "";
            this._sRTO_InvoiceData = "";
            this._iMethod_ID = 0;
            this._sFilePath = "";
            this._sFileName = "";
            this._iSourceCommand_ID = 0;
            this._sClientOrderID = "";
            this._iSent = 0;
            this._iActions = 0;
            this._iUser1_ID = 0;
            this._iUser4_ID = 0;
            this._iDivision_ID = 0;
            this._iShowCancelled = 0;
            this._dFirstOrderDate = Convert.ToDateTime("1900/01/01");
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();

                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Commands"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", _iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iCommandType_ID = Convert.ToInt32(drList["CommandType_ID"]);
                }
                drList.Close();

                switch (_iCommandType_ID)
                {
                    case 1:
                        cmd = new SqlCommand("GetSecurityOrder", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                        drList = cmd.ExecuteReader();
                        while (drList.Read())
                        {
                            this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                            this._sBulkCommand = drList["BulkCommand"] + "";
                            this._iBusinessType_ID = Convert.ToInt32(drList["BusinessType_ID"]);
                            this._iCommandType_ID = Convert.ToInt32(drList["CommandType_ID"]);
                            this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                            this._iCompany_ID = Convert.ToInt32(drList["Company_ID"]);
                            this._iServiceProvider_ID = Convert.ToInt32(drList["StockCompany_ID"]);
                            this._iExecutor_ID = Convert.ToInt32(drList["Executor_ID"]);
                            if (Global.IsNumeric(drList["ProductStockExchange_ID"]))
                                this._iProductStockExchange_ID = Convert.ToInt32(drList["ProductStockExchange_ID"]);
                            else this._iProductStockExchange_ID = 0;
                            this._sProductStockExchange_MIC = drList["ProductStockExchange_MIC"] + ""; 
                            this._sProductStockExchange_Title = drList["ProductStockExchange_Title"] + ""; 
                            this._iStockExchange_ID = Convert.ToInt32(drList["StockExchange_ID"]);
                            this._sStockExchange_MIC = drList["StockExchange_MIC"] + "";
                            this._sStockExchange_Title = drList["StockExchange_Title"] + "";
                            this._iExecutionStockExchange_ID = Convert.ToInt32(drList["RealStockExchange_ID"]);
                            this._sExecutionStockExchange_MIC = drList["ExecutionStockExchange_MIC"] + "";
                            this._sExecutionStockExchange_Title = drList["ExecutionStockExchange_Title"] + "";
                            this._iCustodyProvider_ID = Convert.ToInt32(drList["CustodyProvider_ID"]);
                            this._iDepository_ID = Convert.ToInt32(drList["Depository_ID"]);
                            this._iII_ID = Convert.ToInt32(drList["II_ID"]);
                            this._iParent_ID = Convert.ToInt32(drList["Parent_ID"]);
                            this._sContractTitle = drList["ContractTitle"] + "";
                            this._iContract_ID = Convert.ToInt32(drList["ClientPackage_ID"]);
                            this._iContract_Details_ID = Convert.ToInt32(drList["Contract_Details_ID"]);
                            this._iContract_Packages_ID = Convert.ToInt32(drList["Contract_Packages_ID"]);
                            this._iContractTipos = Convert.ToInt32(drList["ContractTipos"]);
                            this._sCode = drList["Code"] + "";
                            this._sProfitCenter = drList["ProfitCenter"] + "";
                            this._fltAllocationPercent = Convert.ToSingle(drList["AllocationPercent"]);
                            this._iAktion = Convert.ToInt32(drList["Aktion"]);
                            this._dAktionDate = Convert.ToDateTime(drList["AktionDate"]);
                            this._iShare_ID = Convert.ToInt32(drList["Share_ID"]);
                            this._iProduct_ID = Convert.ToInt32(drList["Product_ID"]);                    // not use drList["Product_ID") because it may be changed after command inserting 
                            this._iProductCategory_ID = Convert.ToInt32(drList["ProductCategory_ID"]);    // not use drList["ProductCategory_ID") because it may be changed after command inserting
                                                                                                          // Me._iProductCategory_ID = drList["ProductType")          ' not use drList["ProductCategory_ID") because it may be changed after command inserting
                            this._iPriceType = Convert.ToInt32(drList["Type"]);
                            this._decPrice = Convert.ToDecimal(drList["Price"]);
                            this._decQuantity = Convert.ToDecimal(drList["Quantity"]);
                            this._decAmount = Convert.ToDecimal(drList["Amount"]);
                            this._sCurr = drList["Curr"] + "";
                            this._iConstant = Convert.ToInt32(drList["Constant"]);
                            this._sConstantDate = drList["ConstantDate"] + "";
                            if (this._sConstantDate == "") this._sConstantDate = "01/01/1900";

                            this._sSecurity_Code = drList["ShareCode"] + "";
                            this._sSecurity_Code2 = drList["ShareCode2"] + "";
                            this._sSecurity_ISIN = drList["ISIN"] + "";
                            this._sSecurity_Title = drList["Share_Title"] + "";
                            this._dSecurity_Date1 = Convert.ToDateTime(drList["Date1"]);
                            this._dSecurity_Date3 = Convert.ToDateTime(drList["Date3"]);

                            this._iConstantContinue = Convert.ToInt32(drList["ConstantContinue"]);
                            this._dRecieveDate = Convert.ToDateTime(drList["RecieveDate"]);
                            this._iRecieveMethod_ID = Convert.ToInt32(drList["RecieveMethod_ID"]);
                            this._iBestExecution = Convert.ToInt32(drList["BestExecution"]);
                            this._dSentDate = Convert.ToDateTime(drList["SentDate"]);
                            this._iSendCheck = Convert.ToInt32(drList["SendCheck"]);
                            this._iFIX_A = Convert.ToInt32(drList["FIX_A"]);
                            this._dFIX_RecievedDate = Convert.ToDateTime(drList["FIX_RecievedDate"]);
                            this._dExecuteDate = Convert.ToDateTime(drList["ExecuteDate"]);
                            this._decRealPrice = Convert.ToDecimal(drList["RealPrice"]);
                            this._decRealQuantity = Convert.ToDecimal(drList["RealQuantity"]);
                            this._decRealAmount = Convert.ToDecimal(drList["RealAmount"]);
                            
                            this._decFeesCalc = Convert.ToDecimal(drList["FeesCalc"]);
                            this._decProviderFees = Convert.ToDecimal(drList["ProviderFees"]);
                            this._decFeesDiff = Convert.ToDecimal(drList["FeesDiff"]);
                            this._decFeesMarket = Convert.ToDecimal(drList["FeesMarket"]);
                            this._decAccruedInterest = Convert.ToDecimal(drList["AccruedInterest"]);
                            this._decCommission = Convert.ToDecimal(drList["Commission"]);
                            this._decCurrRate = Convert.ToDecimal(drList["CurrRate"]);
                            this._sNotes = drList["Notes"] + "";
                            this._sValueDate = drList["ValueDate"] + "";
                            this._iInformationMethod_ID = Convert.ToInt32(drList["InformationMethod_ID"]);
                            this._sOfficialInformingDate = drList["OfficialInformingDate"] + "";
                            this._dSettlementDate = Convert.ToDateTime(drList["SettlementDate"]);
                            this._decFeesPercent = Convert.ToDecimal(drList["FeesPercent"]);
                            this._decFeesAmount = Convert.ToDecimal(drList["FeesAmount"]);
                            this._decFeesDiscountPercent = Convert.ToDecimal(drList["FeesDiscountPercent"]);
                            this._decFeesDiscountAmount = Convert.ToDecimal(drList["FeesDiscountAmount"]);
                            this._decFinishFeesPercent = Convert.ToDecimal(drList["FinishFeesPercent"]);
                            this._decFinishFeesAmount = Convert.ToDecimal(drList["FinishFeesAmount"]);
                            this._decFeesRate = Convert.ToDecimal(drList["FeesRate"]);
                            this._decFeesAmountEUR = Convert.ToDecimal(drList["FeesAmountEUR"]);
                            this._decMinFeesAmount = Convert.ToDecimal(drList["MinFeesAmount"]);
                            this._decMinFeesDiscountPercent = Convert.ToDecimal(drList["MinFeesDiscountPercent"]);
                            this._decMinFeesDiscountAmount = Convert.ToDecimal(drList["MinFeesDiscountAmount"]);
                            this._decFinishMinFeesAmount = Convert.ToDecimal(drList["FinishMinFeesAmount"]);
                            this._decMinFeesRate = Convert.ToDecimal(drList["MinFeesRate"]);
                            this._decMinAmountEUR = Convert.ToDecimal(drList["MinAmountEUR"]);
                            this._decTicketFee = Convert.ToDecimal(drList["TicketFee"]);
                            this._decTicketFeeDiscountPercent = Convert.ToDecimal(drList["TicketFeeDiscountPercent"]);
                            this._decTicketFeeDiscountAmount = Convert.ToDecimal(drList["TicketFeeDiscountAmount"]);
                            this._decFinishTicketFee = Convert.ToDecimal(drList["FinishTicketFee"]);
                            this._decTicketFeesRate = Convert.ToDecimal(drList["TicketFeesRate"]);
                            this._decTicketFeesAmountEUR = Convert.ToDecimal(drList["TicketFeesAmountEUR"]);
                            this._decFeesCalc = Convert.ToDecimal(drList["FeesCalc"]);
                            this._decProviderFees = Convert.ToDecimal(drList["ProviderFees"]);
                            this._decRTO_FeesPercent = Convert.ToDecimal(drList["RTO_FeesPercent"]);
                            this._decRTO_FeesAmount = Convert.ToDecimal(drList["RTO_FeesAmount"]);
                            this._decRTO_FeesDiscountPercent = Convert.ToDecimal(drList["RTO_FeesDiscountPercent"]);
                            this._decRTO_FeesDiscountAmount = Convert.ToDecimal(drList["RTO_FeesDiscountAmount"]);
                            this._decRTO_FinishFeesPercent = Convert.ToDecimal(drList["RTO_FinishFeesPercent"]);
                            this._decRTO_FinishFeesAmount = Convert.ToDecimal(drList["RTO_FinishFeesAmount"]);
                            
                            this._decRTO_FeesAmountEUR = Convert.ToDecimal(drList["RTO_FeesAmountEUR"]);
                            this._sRTO_MinFeesCurr = drList["RTO_MinFeesCurr"] + "";
                            this._decRTO_MinFeesAmount = Convert.ToDecimal(drList["RTO_MinFeesAmount"]);
                            this._decRTO_MinFeesDiscountPercent = Convert.ToDecimal(drList["RTO_MinFeesDiscountPercent"]);
                            this._decRTO_MinFeesDiscountAmount = Convert.ToDecimal(drList["RTO_MinFeesDiscountAmount"]);
                            this._decRTO_FinishMinFeesAmount = Convert.ToDecimal(drList["RTO_FinishMinFeesAmount"]);
                            this._sRTO_TicketFeeCurr = drList["RTO_TicketFeeCurr"] + "";
                            this._decRTO_TicketFee = Convert.ToDecimal(drList["RTO_TicketFee"]);
                            this._decRTO_TicketFeeDiscountPercent = Convert.ToDecimal(drList["RTO_TicketFeeDiscountPercent"]);
                            this._decRTO_TicketFeeDiscountAmount = Convert.ToDecimal(drList["RTO_TicketFeeDiscountAmount"]);
                            this._decRTO_FinishTicketFee = Convert.ToDecimal(drList["RTO_FinishTicketFee"]);
                            this._decRTO_FeesProVAT = Convert.ToDecimal(drList["RTO_FeesProVAT"]);
                            this._decRTO_FeesVAT = Convert.ToDecimal(drList["RTO_FeesVAT"]);
                            this._decRTO_CompanyFees = Convert.ToDecimal(drList["RTO_CompanyFees"]);
                            this._iRTO_InvoiceTitle_ID = Convert.ToInt32(drList["RTO_InvoiceTitle_ID"]);
                            this._decFeesMisc = Convert.ToDecimal(drList["FeesMisc"]);
                            this._sFeesNotes = drList["FeesNotes"] + "";
                            this._iFeesCalcMode = Convert.ToInt32(drList["FeesCalcMode"]);
                            this._iPinakidio = Convert.ToInt32(drList["Pinakidio"]);
                            this._sLastCheckFile = drList["LastCheckFile"] + "";
                            this._iUser_ID = Convert.ToInt32(drList["User_ID"]);
                            this._dDateIns = Convert.ToDateTime(drList["DateIns"]);
                            this._iStatus = Convert.ToInt32(drList["Status"]);

                            this._sCurrency = drList["Currency"] + "";
                            this._decMinimumFees = Convert.ToDecimal(drList["MinFeesAmount"]);
                            this._sMinFeesCurr = drList["MinFeesCurr"] + "";
                            if (Convert.ToInt32(drList["Client_ID"]) != 0)
                            {
                                if (Convert.ToInt32(drList["ClientTipos"]) == 1)
                                    this._sClientName = (drList["ClientSurname"] + " " + drList["ClientFirstname"]).Trim();
                                else
                                    this._sClientName = (drList["ClientSurname"] + "").Trim();
                            }
                            else this._sClientName = drList["Company_Title"] + "";

                            this._sCompanyTitle = drList["Company_Title"] + "";
                            this._iCFP_ID = Convert.ToInt32(drList["CFP_ID"]);
                            this._iPackageType_ID = Convert.ToInt32(drList["PackageType_ID"]);
                            this._sPackage_Title = drList["Package_Title"] + "  ver." + drList["PackageVersion"];
                            this._sProduct_Title = drList["ProductTitle"] + "";
                            this._sProductCategory_Title = drList["ProductCategories_Title"] + "";
                            this._iSecurity_Share_ID = Convert.ToInt32(drList["Share_ID"]);
                            this._sSecurity_Code = drList["ShareCode"] + "";
                            this._sSecurity_Code2 = drList["ShareCode2"] + "";
                            this._sSecurity_ISIN = drList["ISIN"] + "";
                            this._sSecurity_Title = drList["Share_Title"] + "";
                            this._dSecurity_Date1 = Convert.ToDateTime(drList["Date1"]);
                            this._dSecurity_Date3 = Convert.ToDateTime(drList["Date3"]);
                            this._decSecurity_Coupone = Convert.ToDecimal(drList["Coupone"]);
                            this._iSecurity_FrequencyClipping = Convert.ToInt32(drList["FrequencyClipping"]);
                            this._sMainCurr = drList["MainCurr"] + "";
                            this._sServiceProvider_Title = drList["StockCompany_Title"] + "";
                            this._iServiceProviderFeesMode = Convert.ToInt32(drList["FeesMode"]);
                            this._sStockExchange_Title = drList["StockExchange_MIC"] + ""; // + " / " + drList["StockExchange_Title"];
                            this._sDepository_Code = drList["Depository_Code"] + "";
                            this._sAuthorName = (drList["Author_Surname"] + " " + drList["Author_Firstname"]).Trim();
                            this._sAdvisorName = (drList["AdvisorSurname"] + " " + drList["AdvisorFirstname"]).Trim();
                            this._sRecieveTitle = drList["RecieveTitle"] + "";
                            this._sInformationTitle = drList["InformationTitle"] + "";
                            this._sRTO_InvoiceData = drList["Invoice_Code"] + " " + drList["Invoice_Seira"] + " " + drList["Invoice_Arithmos"] + " από " + drList["Invoice_DateIns"];
                            this._sFileName = drList["Invoice_FileName"] + "";

                        }
                        drList.Close();
                        break;
                    case 2:
                        cmd = new SqlCommand("sp_GetExecCommand", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                        drList = cmd.ExecuteReader();
                        while (drList.Read())
                        {
                            this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                            this._sBulkCommand = drList["BulkCommand"] + "";
                            this._iBusinessType_ID = Convert.ToInt32(drList["BusinessType_ID"]);
                            this._iCommandType_ID = Convert.ToInt32(drList["CommandType_ID"]);
                            this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                            this._iCompany_ID = Convert.ToInt32(drList["Company_ID"]);
                            this._iServiceProvider_ID = Convert.ToInt32(drList["StockCompany_ID"]);
                            this._iExecutor_ID = Convert.ToInt32(drList["Executor_ID"]);
                            this._iStockExchange_ID = Convert.ToInt32(drList["StockExchange_ID"]);
                            this._iCustodyProvider_ID = Convert.ToInt32(drList["CustodyProvider_ID"]);
                            this._iDepository_ID = Convert.ToInt32(drList["Depository_ID"]);
                            this._iII_ID = Convert.ToInt32(drList["II_ID"]);
                            this._iParent_ID = Convert.ToInt32(drList["Parent_ID"]);
                            this._sContractTitle = drList["ContractTitle"] + "";
                            this._iContract_ID = Convert.ToInt32(drList["ClientPackage_ID"]);
                            this._iContract_Details_ID = Convert.ToInt32(drList["Contract_Details_ID"]);
                            this._iContract_Packages_ID = Convert.ToInt32(drList["Contract_Packages_ID"]);
                            this._sCode = drList["Code"] + "";
                            this._sProfitCenter = drList["ProfitCenter"] + "";
                            this._fltAllocationPercent = Convert.ToSingle(drList["AllocationPercent"]);
                            this._iAktion = Convert.ToInt32(drList["Aktion"]);
                            this._dAktionDate = Convert.ToDateTime(drList["AktionDate"]);
                            this._iShare_ID = Convert.ToInt32(drList["Share_ID"]);
                            this._iProduct_ID = Convert.ToInt32(drList["Product_ID"]);                    // not use drList["Product_ID") because it may be changed after command inserting 
                            this._iProductCategory_ID = Convert.ToInt32(drList["ProductCategory_ID"]);    // not use drList["ProductCategory_ID") because it may be changed after command inserting
                                                                                                          // Me._iProductCategory_ID = drList["ProductType")          ' not use drList["ProductCategory_ID") because it may be changed after command inserting
                            if (Global.IsNumeric(drList["ProductStockExchange_ID"]))
                                this._iProductStockExchange_ID = Convert.ToInt32(drList["ProductStockExchange_ID"]);
                            else this._iProductStockExchange_ID = 0;
                            this._iPriceType = Convert.ToInt32(drList["Type"]);
                            this._decPrice = Convert.ToDecimal(drList["Price"]);
                            this._decQuantity = Convert.ToDecimal(drList["Quantity"]);
                            this._decAmount = Convert.ToDecimal(drList["Amount"]);
                            this._sCurr = drList["Curr"] + "";
                            this._iConstant = Convert.ToInt32(drList["Constant"]);
                            this._sConstantDate = drList["ConstantDate"] + "";
                            if (this._sConstantDate == "") this._sConstantDate = "01/01/1900";

                            this._sSecurity_Code = drList["ShareCode"] + "";
                            this._sSecurity_Code2 = drList["ShareCode2"] + "";
                            this._sSecurity_ISIN = drList["ISIN"] + "";
                            this._sSecurity_Title = drList["Share_Title"] + "";
                            this._dSecurity_Date1 = Convert.ToDateTime(drList["Date1"]);
                            this._dSecurity_Date3 = Convert.ToDateTime(drList["Date3"]);

                            this._iConstantContinue = Convert.ToInt32(drList["ConstantContinue"]);
                            this._dRecieveDate = Convert.ToDateTime(drList["RecieveDate"]);
                            this._iRecieveMethod_ID = Convert.ToInt32(drList["RecieveMethod_ID"]);
                            this._iBestExecution = Convert.ToInt32(drList["BestExecution"]);
                            this._dSentDate = Convert.ToDateTime(drList["SentDate"]);
                            this._iSendCheck = Convert.ToInt32(drList["SendCheck"]);
                            this._iFIX_A = Convert.ToInt32(drList["FIX_A"]);
                            this._dFIX_RecievedDate = Convert.ToDateTime(drList["FIX_RecievedDate"]);
                            this._dExecuteDate = Convert.ToDateTime(drList["ExecuteDate"]);
                            this._decRealPrice = Convert.ToDecimal(drList["RealPrice"]);
                            this._decRealQuantity = Convert.ToDecimal(drList["RealQuantity"]);
                            this._decRealAmount = Convert.ToDecimal(drList["RealAmount"]);
                            this._iExecutionStockExchange_ID = Convert.ToInt32(drList["RealStockExchange_ID"]);
                            this._decFeesCalc = Convert.ToDecimal(drList["FeesCalc"]);
                            this._decProviderFees = Convert.ToDecimal(drList["ProviderFees"]);
                            this._decFeesDiff = Convert.ToDecimal(drList["FeesDiff"]);
                            this._decFeesMarket = Convert.ToDecimal(drList["FeesMarket"]);
                            this._decAccruedInterest = Convert.ToDecimal(drList["AccruedInterest"]);
                            this._decCommission = Convert.ToDecimal(drList["Commission"]);
                            this._decCurrRate = Convert.ToDecimal(drList["CurrRate"]);
                            this._sNotes = drList["Notes"] + "";
                            this._sValueDate = drList["ValueDate"] + "";
                            this._iInformationMethod_ID = Convert.ToInt32(drList["InformationMethod_ID"]);
                            this._sOfficialInformingDate = drList["OfficialInformingDate"] + "";
                            this._dSettlementDate = Convert.ToDateTime(drList["SettlementDate"]);
                            this._decFeesPercent = Convert.ToDecimal(drList["FeesPercent"]);
                            this._decFeesAmount = Convert.ToDecimal(drList["FeesAmount"]);
                            this._decFeesDiscountPercent = Convert.ToDecimal(drList["FeesDiscountPercent"]);
                            this._decFeesDiscountAmount = Convert.ToDecimal(drList["FeesDiscountAmount"]);
                            this._decFinishFeesPercent = Convert.ToDecimal(drList["FinishFeesPercent"]);
                            this._decFinishFeesAmount = Convert.ToDecimal(drList["FinishFeesAmount"]);
                            this._decFeesRate = Convert.ToDecimal(drList["FeesRate"]);
                            this._decFeesAmountEUR = Convert.ToDecimal(drList["FeesAmountEUR"]);
                            this._decMinFeesAmount = Convert.ToDecimal(drList["MinFeesAmount"]);
                            this._decMinFeesDiscountPercent = Convert.ToDecimal(drList["MinFeesDiscountPercent"]);
                            this._decMinFeesDiscountAmount = Convert.ToDecimal(drList["MinFeesDiscountAmount"]);
                            this._decFinishMinFeesAmount = Convert.ToDecimal(drList["FinishMinFeesAmount"]);
                            this._decMinFeesRate = Convert.ToDecimal(drList["MinFeesRate"]);
                            this._decMinAmountEUR = Convert.ToDecimal(drList["MinAmountEUR"]);
                            this._decTicketFee = Convert.ToDecimal(drList["TicketFee"]);
                            this._decTicketFeeDiscountPercent = Convert.ToDecimal(drList["TicketFeeDiscountPercent"]);
                            this._decTicketFeeDiscountAmount = Convert.ToDecimal(drList["TicketFeeDiscountAmount"]);
                            this._decFinishTicketFee = Convert.ToDecimal(drList["FinishTicketFee"]);
                            this._decTicketFeesRate = Convert.ToDecimal(drList["TicketFeesRate"]);
                            this._decTicketFeesAmountEUR = Convert.ToDecimal(drList["TicketFeesAmountEUR"]);
                            this._decFeesCalc = Convert.ToDecimal(drList["FeesCalc"]);
                            this._decProviderFees = Convert.ToDecimal(drList["ProviderFees"]);
                            this._decRTO_FeesPercent = Convert.ToDecimal(drList["RTO_FeesPercent"]);
                            this._decRTO_FeesAmount = Convert.ToDecimal(drList["RTO_FeesAmount"]);
                            this._decRTO_FeesDiscountPercent = Convert.ToDecimal(drList["RTO_FeesDiscountPercent"]);
                            this._decRTO_FeesDiscountAmount = Convert.ToDecimal(drList["RTO_FeesDiscountAmount"]);
                            this._decRTO_FinishFeesPercent = Convert.ToDecimal(drList["RTO_FinishFeesPercent"]);
                            this._decRTO_FinishFeesAmount = Convert.ToDecimal(drList["RTO_FinishFeesAmount"]);
                            
                            this._decRTO_FeesAmountEUR = Convert.ToDecimal(drList["RTO_FeesAmountEUR"]);
                            this._sRTO_MinFeesCurr = drList["RTO_MinFeesCurr"] + "";
                            this._decRTO_MinFeesAmount = Convert.ToDecimal(drList["RTO_MinFeesAmount"]);
                            this._decRTO_MinFeesDiscountPercent = Convert.ToDecimal(drList["RTO_MinFeesDiscountPercent"]);
                            this._decRTO_MinFeesDiscountAmount = Convert.ToDecimal(drList["RTO_MinFeesDiscountAmount"]);
                            this._decRTO_FinishMinFeesAmount = Convert.ToDecimal(drList["RTO_FinishMinFeesAmount"]);
                            this._sRTO_TicketFeeCurr = drList["RTO_TicketFeeCurr"] + "";
                            this._decRTO_TicketFee = Convert.ToDecimal(drList["RTO_TicketFee"]);
                            this._decRTO_TicketFeeDiscountPercent = Convert.ToDecimal(drList["RTO_TicketFeeDiscountPercent"]);
                            this._decRTO_TicketFeeDiscountAmount = Convert.ToDecimal(drList["RTO_TicketFeeDiscountAmount"]);
                            this._decRTO_FinishTicketFee = Convert.ToDecimal(drList["RTO_FinishTicketFee"]);
                            this._decRTO_FeesProVAT = Convert.ToDecimal(drList["RTO_FeesProVAT"]);
                            this._decRTO_FeesVAT = Convert.ToDecimal(drList["RTO_FeesVAT"]);
                            this._decRTO_CompanyFees = Convert.ToDecimal(drList["RTO_CompanyFees"]);
                            this._iRTO_InvoiceTitle_ID = Convert.ToInt32(drList["RTO_InvoiceTitle_ID"]);
                            this._decFeesMisc = Convert.ToDecimal(drList["FeesMisc"]);
                            this._sFeesNotes = drList["FeesNotes"] + "";
                            this._iFeesCalcMode = Convert.ToInt32(drList["FeesCalcMode"]);
                            this._iPinakidio = Convert.ToInt32(drList["Pinakidio"]);
                            this._sLastCheckFile = drList["LastCheckFile"] + "";
                            this._iUser_ID = Convert.ToInt32(drList["User_ID"]);
                            this._dDateIns = Convert.ToDateTime(drList["DateIns"]);
                            this._iStatus = Convert.ToInt32(drList["Status"]);

                            this._sCurrency = drList["Currency"] + "";
                            this._decMinimumFees = Convert.ToDecimal(drList["MinFeesAmount"]);
                            this._sMinFeesCurr = drList["MinFeesCurr"] + "";
                            if (Convert.ToInt32(drList["Client_ID"]) != 0)
                            {
                                if (Convert.ToInt32(drList["ClientTipos"]) == 1)
                                    this._sClientName = (drList["ClientSurname"] + " " + drList["ClientFirstname"]).Trim();
                                else
                                    this._sClientName = (drList["ClientSurname"] + "").Trim();
                            }
                            else this._sClientName = drList["Company_Title"] + "";

                            this._sCompanyTitle = drList["Company_Title"] + "";
                            this._sPackage_Title = drList["Package_Title"] + "  ver." + drList["PackageVersion"];
                            this._sProduct_Title = drList["ProductTitle"] + "";
                            this._sProductCategory_Title = drList["ProductCategories_Title"] + "";
                            this._sProductStockExchange_Title = drList["ProductStockExchange_MIC"] + ""; // + " / " + drList["ProductStockExchange_Title"];
                            this._iSecurity_Share_ID = Convert.ToInt32(drList["Share_ID"]);
                            this._sSecurity_Code = drList["ShareCode"] + "";
                            this._sSecurity_Code2 = drList["ShareCode2"] + "";
                            this._sSecurity_ISIN = drList["ISIN"] + "";
                            this._sSecurity_Title = drList["Share_Title"] + "";
                            this._dSecurity_Date1 = Convert.ToDateTime(drList["Date1"]);
                            this._dSecurity_Date3 = Convert.ToDateTime(drList["Date3"]);
                            this._decSecurity_Coupone = Convert.ToDecimal(drList["Coupone"]);
                            this._iSecurity_FrequencyClipping = Convert.ToInt32(drList["FrequencyClipping"]);
                            this._sMainCurr = drList["MainCurr"] + "";
                            this._sServiceProvider_Title = drList["StockCompany_Title"] + "";
                            this._iServiceProviderFeesMode = Convert.ToInt32(drList["FeesMode"]);
                            this._sStockExchange_Title = drList["StockExchange_MIC"] + ""; 
                            this._sDepository_Code = drList["Depository_Code"] + "";
                            this._sAuthorName = (drList["Author_Surname"] + " " + drList["Author_Firstname"]).Trim();
                            this._sAdvisorName = (drList["AdvisorSurname"] + " " + drList["AdvisorFirstname"]).Trim();
                            this._sRecieveTitle = drList["RecieveTitle"] + "";
                            this._sInformationTitle = drList["InformationTitle"] + "";
                        }
                        drList.Close();
                        break;
                    case 3:
                        cmd = new SqlCommand("GetBulkCommand", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                        drList = cmd.ExecuteReader();
                        while (drList.Read())
                        {
                            this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                            this._sBulkCommand = drList["BulkCommand"] + "";
                            this._iBusinessType_ID = Convert.ToInt32(drList["BusinessType_ID"]);
                            this._iCommandType_ID = Convert.ToInt32(drList["CommandType_ID"]);
                            this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                            this._iCompany_ID = Convert.ToInt32(drList["Company_ID"]);
                            this._iServiceProvider_ID = Convert.ToInt32(drList["StockCompany_ID"]);
                            this._iExecutor_ID = Convert.ToInt32(drList["Executor_ID"]);
                            this._iStockExchange_ID = Convert.ToInt32(drList["StockExchange_ID"]);
                            this._iCustodyProvider_ID = Convert.ToInt32(drList["CustodyProvider_ID"]);
                            this._iDepository_ID = Convert.ToInt32(drList["Depository_ID"]);
                            this._iII_ID = Convert.ToInt32(drList["II_ID"]);
                            this._iParent_ID = Convert.ToInt32(drList["Parent_ID"]);
                            //this._sContractTitle = drList["ContractTitle"] + "";
                            this._iContract_ID = Convert.ToInt32(drList["ClientPackage_ID"]);
                            this._iContract_Details_ID = Convert.ToInt32(drList["Contract_Details_ID"]);
                            this._iContract_Packages_ID = Convert.ToInt32(drList["Contract_Packages_ID"]);
                            this._sCode = drList["Code"] + "";
                            this._sProfitCenter = drList["ProfitCenter"] + "";
                            this._fltAllocationPercent = Convert.ToSingle(drList["AllocationPercent"]);
                            this._iAktion = Convert.ToInt32(drList["Aktion"]);
                            this._dAktionDate = Convert.ToDateTime(drList["AktionDate"]);
                            this._iShare_ID = Convert.ToInt32(drList["Share_ID"]);
                            this._iProduct_ID = Convert.ToInt32(drList["Product_ID"]);                    // not use drList["Product_ID") because it may be changed after command inserting 
                            this._iProductCategory_ID = Convert.ToInt32(drList["ProductCategory_ID"]);    // not use drList["ProductCategory_ID") because it may be changed after command inserting
                                                                                                          // Me._iProductCategory_ID = drList["ProductType")          ' not use drList["ProductCategory_ID") because it may be changed after command inserting
                            this._iProductStockExchange_ID = Convert.ToInt32(drList["ProductStockExchange_ID"]);
                            this._iPriceType = Convert.ToInt32(drList["Type"]);
                            this._decPrice = Convert.ToDecimal(drList["Price"]);
                            this._decQuantity = Convert.ToDecimal(drList["Quantity"]);
                            this._decAmount = Convert.ToDecimal(drList["Amount"]);
                            this._sCurr = drList["Curr"] + "";
                            this._iConstant = Convert.ToInt32(drList["Constant"]);
                            this._sConstantDate = drList["ConstantDate"] + "";
                            if (this._sConstantDate == "") this._sConstantDate = "01/01/1900";

                            this._sSecurity_Code = drList["ShareCode"] + "";
                            this._sSecurity_Code2 = drList["ShareCode2"] + "";
                            this._sSecurity_ISIN = drList["ISIN"] + "";
                            this._sSecurity_Title = drList["Share_Title"] + "";
                            this._dSecurity_Date1 = Convert.ToDateTime(drList["Date1"]);
                            this._dSecurity_Date3 = Convert.ToDateTime(drList["Date3"]);

                            this._iConstantContinue = Convert.ToInt32(drList["ConstantContinue"]);
                            this._dRecieveDate = Convert.ToDateTime(drList["RecieveDate"]);
                            this._iRecieveMethod_ID = Convert.ToInt32(drList["RecieveMethod_ID"]);
                            this._iBestExecution = Convert.ToInt32(drList["BestExecution"]);
                            this._dSentDate = Convert.ToDateTime(drList["SentDate"]);
                            this._iSendCheck = Convert.ToInt32(drList["SendCheck"]);
                            this._iFIX_A = Convert.ToInt32(drList["FIX_A"]);
                            this._dFIX_RecievedDate = Convert.ToDateTime(drList["FIX_RecievedDate"]);
                            this._dExecuteDate = Convert.ToDateTime(drList["ExecuteDate"]);
                            this._decRealPrice = Convert.ToDecimal(drList["RealPrice"]);
                            this._decRealQuantity = Convert.ToDecimal(drList["RealQuantity"]);
                            this._decRealAmount = Convert.ToDecimal(drList["RealAmount"]);
                            this._iExecutionStockExchange_ID = Convert.ToInt32(drList["RealStockExchange_ID"]);
                            this._decFeesCalc = Convert.ToDecimal(drList["FeesCalc"]);
                            this._decProviderFees = Convert.ToDecimal(drList["ProviderFees"]);
                            this._decFeesDiff = Convert.ToDecimal(drList["FeesDiff"]);
                            this._decFeesMarket = Convert.ToDecimal(drList["FeesMarket"]);
                            this._decAccruedInterest = Convert.ToDecimal(drList["AccruedInterest"]);
                            this._decCommission = Convert.ToDecimal(drList["Commission"]);
                            this._decCurrRate = Convert.ToDecimal(drList["CurrRate"]);
                            this._sNotes = drList["Notes"] + "";
                            this._sValueDate = drList["ValueDate"] + "";
                            this._iInformationMethod_ID = Convert.ToInt32(drList["InformationMethod_ID"]);
                            this._sOfficialInformingDate = drList["OfficialInformingDate"] + "";
                            this._dSettlementDate = Convert.ToDateTime(drList["SettlementDate"]);
                            this._decFeesPercent = Convert.ToDecimal(drList["FeesPercent"]);
                            this._decFeesAmount = Convert.ToDecimal(drList["FeesAmount"]);
                            this._decFeesDiscountPercent = Convert.ToDecimal(drList["FeesDiscountPercent"]);
                            this._decFeesDiscountAmount = Convert.ToDecimal(drList["FeesDiscountAmount"]);
                            this._decFinishFeesPercent = Convert.ToDecimal(drList["FinishFeesPercent"]);
                            this._decFinishFeesAmount = Convert.ToDecimal(drList["FinishFeesAmount"]);
                            this._decFeesRate = Convert.ToDecimal(drList["FeesRate"]);
                            this._decFeesAmountEUR = Convert.ToDecimal(drList["FeesAmountEUR"]);
                            this._decMinFeesAmount = Convert.ToDecimal(drList["MinFeesAmount"]);
                            this._decMinFeesDiscountPercent = Convert.ToDecimal(drList["MinFeesDiscountPercent"]);
                            this._decMinFeesDiscountAmount = Convert.ToDecimal(drList["MinFeesDiscountAmount"]);
                            this._decFinishMinFeesAmount = Convert.ToDecimal(drList["FinishMinFeesAmount"]);
                            this._decMinFeesRate = Convert.ToDecimal(drList["MinFeesRate"]);
                            this._decMinAmountEUR = Convert.ToDecimal(drList["MinAmountEUR"]);
                            this._decTicketFee = Convert.ToDecimal(drList["TicketFee"]);
                            this._decTicketFeeDiscountPercent = Convert.ToDecimal(drList["TicketFeeDiscountPercent"]);
                            this._decTicketFeeDiscountAmount = Convert.ToDecimal(drList["TicketFeeDiscountAmount"]);
                            this._decFinishTicketFee = Convert.ToDecimal(drList["FinishTicketFee"]);
                            this._decTicketFeesRate = Convert.ToDecimal(drList["TicketFeesRate"]);
                            this._decTicketFeesAmountEUR = Convert.ToDecimal(drList["TicketFeesAmountEUR"]);
                            this._decFeesCalc = Convert.ToDecimal(drList["FeesCalc"]);
                            this._decProviderFees = Convert.ToDecimal(drList["ProviderFees"]);
                            this._decRTO_FeesPercent = Convert.ToDecimal(drList["RTO_FeesPercent"]);
                            this._decRTO_FeesAmount = Convert.ToDecimal(drList["RTO_FeesAmount"]);
                            this._decRTO_FeesDiscountPercent = Convert.ToDecimal(drList["RTO_FeesDiscountPercent"]);
                            this._decRTO_FeesDiscountAmount = Convert.ToDecimal(drList["RTO_FeesDiscountAmount"]);
                            this._decRTO_FinishFeesPercent = Convert.ToDecimal(drList["RTO_FinishFeesPercent"]);
                            this._decRTO_FinishFeesAmount = Convert.ToDecimal(drList["RTO_FinishFeesAmount"]);
                            
                            this._decRTO_FeesAmountEUR = Convert.ToDecimal(drList["RTO_FeesAmountEUR"]);
                            this._sRTO_MinFeesCurr = drList["RTO_MinFeesCurr"] + "";
                            this._decRTO_MinFeesAmount = Convert.ToDecimal(drList["RTO_MinFeesAmount"]);
                            this._decRTO_MinFeesDiscountPercent = Convert.ToDecimal(drList["RTO_MinFeesDiscountPercent"]);
                            this._decRTO_MinFeesDiscountAmount = Convert.ToDecimal(drList["RTO_MinFeesDiscountAmount"]);
                            this._decRTO_FinishMinFeesAmount = Convert.ToDecimal(drList["RTO_FinishMinFeesAmount"]);
                            this._sRTO_TicketFeeCurr = drList["RTO_TicketFeeCurr"] + "";
                            this._decRTO_TicketFee = Convert.ToDecimal(drList["RTO_TicketFee"]);
                            this._decRTO_TicketFeeDiscountPercent = Convert.ToDecimal(drList["RTO_TicketFeeDiscountPercent"]);
                            this._decRTO_TicketFeeDiscountAmount = Convert.ToDecimal(drList["RTO_TicketFeeDiscountAmount"]);
                            this._decRTO_FinishTicketFee = Convert.ToDecimal(drList["RTO_FinishTicketFee"]);
                            this._decRTO_FeesProVAT = Convert.ToDecimal(drList["RTO_FeesProVAT"]);
                            this._decRTO_FeesVAT = Convert.ToDecimal(drList["RTO_FeesVAT"]);
                            this._decRTO_CompanyFees = Convert.ToDecimal(drList["RTO_CompanyFees"]);
                            this._iRTO_InvoiceTitle_ID = Convert.ToInt32(drList["RTO_InvoiceTitle_ID"]);
                            this._decFeesMisc = Convert.ToDecimal(drList["FeesMisc"]);
                            this._sFeesNotes = drList["FeesNotes"] + "";
                            this._iFeesCalcMode = Convert.ToInt32(drList["FeesCalcMode"]);
                            this._iPinakidio = Convert.ToInt32(drList["Pinakidio"]);
                            this._sAuthorName = (drList["AuthorSurname"] + " " + drList["AuthorFirstname"]).Trim();
                            this._sLastCheckFile = drList["LastCheckFile"] + "";
                            this._iUser_ID = Convert.ToInt32(drList["User_ID"]);
                            this._dDateIns = Convert.ToDateTime(drList["DateIns"]);
                            this._iStatus = Convert.ToInt32(drList["Status"]);

                            //this._sCurrency = drList["Currency"] + "";
                            this._decMinimumFees = Convert.ToDecimal(drList["MinFeesAmount"]);
                            this._sMinFeesCurr = drList["MinFeesCurr"] + "";
                            if (Convert.ToInt32(drList["Client_ID"]) != 0)
                            {
                                if (Convert.ToInt32(drList["ClientTipos"]) == 1)
                                    this._sClientName = (drList["ClientSurname"] + " " + drList["ClientFirstname"]).Trim();
                                else
                                    this._sClientName = (drList["ClientSurname"] + "").Trim();
                            }
                            else this._sClientName = drList["Company_Title"] + "";

                            this._sCompanyTitle = drList["Company_Title"] + "";
                            this._sProduct_Title = drList["ProductTitle"] + "";
                            this._sProductCategory_Title = drList["ProductCategories_Title"] + "";
                            this._sProductStockExchange_Title = drList["ProductStockExchange_MIC"] + ""; 
                            this._iSecurity_Share_ID = Convert.ToInt32(drList["Share_ID"]);
                            this._sSecurity_Code = drList["ShareCode"] + "";
                            this._sSecurity_Code2 = drList["ShareCode2"] + "";
                            this._sSecurity_ISIN = drList["ISIN"] + "";
                            this._sSecurity_Title = drList["Share_Title"] + "";
                            this._dSecurity_Date1 = Convert.ToDateTime(drList["Date1"]);
                            this._dSecurity_Date3 = Convert.ToDateTime(drList["Date3"]);
                            this._decSecurity_Coupone = Convert.ToDecimal(drList["Coupone"]);
                            this._iSecurity_FrequencyClipping = Convert.ToInt32(drList["FrequencyClipping"]);
                            this._sServiceProvider_Title = drList["ServiceProvider_Title"] + "";
                        }
                        drList.Close();
                        break;
                    case 4:
                        cmd = new SqlCommand("sp_GetDPMCommand", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                        drList = cmd.ExecuteReader();
                        while (drList.Read())
                        {
                            this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                            this._sBulkCommand = drList["BulkCommand"] + "";
                            this._iBusinessType_ID = Convert.ToInt32(drList["BusinessType_ID"]);
                            this._iCommandType_ID = Convert.ToInt32(drList["CommandType_ID"]);
                            this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                            this._iCompany_ID = Convert.ToInt32(drList["Company_ID"]);
                            this._iServiceProvider_ID = Convert.ToInt32(drList["StockCompany_ID"]);
                            this._iExecutor_ID = Convert.ToInt32(drList["Executor_ID"]);
                            this._iStockExchange_ID = Convert.ToInt32(drList["StockExchange_ID"]);
                            this._iCustodyProvider_ID = Convert.ToInt32(drList["CustodyProvider_ID"]);
                            this._iDepository_ID = Convert.ToInt32(drList["Depository_ID"]);
                            this._iII_ID = Convert.ToInt32(drList["II_ID"]);
                            this._iParent_ID = Convert.ToInt32(drList["Parent_ID"]);
                            this._iContract_ID = Convert.ToInt32(drList["ClientPackage_ID"]);
                            this._iContract_Details_ID = Convert.ToInt32(drList["Contract_Details_ID"]);
                            this._iContract_Packages_ID = Convert.ToInt32(drList["Contract_Packages_ID"]);
                            this._sCode = drList["Code"] + "";
                            this._sProfitCenter = drList["ProfitCenter"] + "";
                            this._fltAllocationPercent = Convert.ToSingle(drList["AllocationPercent"]);
                            this._iAktion = Convert.ToInt32(drList["Aktion"]);
                            this._dAktionDate = Convert.ToDateTime(drList["AktionDate"]);
                            this._iShare_ID = Convert.ToInt32(drList["Share_ID"]);
                            this._iProduct_ID = Convert.ToInt32(drList["Product_ID"]);                    // not use drList["Product_ID") because it may be changed after command inserting 
                            this._iProductCategory_ID = Convert.ToInt32(drList["ProductCategory_ID"]);    // not use drList["ProductCategory_ID") because it may be changed after command inserting
                                                                                                          // Me._iProductCategory_ID = drList["ProductType")          ' not use drList["ProductCategory_ID") because it may be changed after command inserting
                            if (Global.IsNumeric(drList["ProductStockExchange_ID"] + ""))
                                this._iProductStockExchange_ID = Convert.ToInt32(drList["ProductStockExchange_ID"]);
                            else this._iProductStockExchange_ID = 0;
                            this._iPriceType = Convert.ToInt32(drList["Type"]);
                            this._decPrice = Convert.ToDecimal(drList["Price"]);
                            this._decQuantity = Convert.ToDecimal(drList["Quantity"]);
                            this._decAmount = Convert.ToDecimal(drList["Amount"]);
                            this._sCurr = drList["Curr"] + "";
                            this._iConstant = Convert.ToInt32(drList["Constant"]);
                            this._sConstantDate = drList["ConstantDate"] + "";
                            if (this._sConstantDate == "") this._sConstantDate = "01/01/1900";

                            this._sSecurity_Code = drList["ShareCode"] + "";
                            this._sSecurity_Code2 = drList["ShareCode2"] + "";
                            this._sSecurity_ISIN = drList["ISIN"] + "";
                            this._sSecurity_Title = drList["Share_Title"] + "";
                            this._dSecurity_Date1 = Convert.ToDateTime(drList["Date1"]);
                            this._dSecurity_Date3 = Convert.ToDateTime(drList["Date3"]);

                            this._iConstantContinue = Convert.ToInt32(drList["ConstantContinue"]);
                            this._dRecieveDate = Convert.ToDateTime(drList["RecieveDate"]);
                            this._iRecieveMethod_ID = Convert.ToInt32(drList["RecieveMethod_ID"]);
                            this._iBestExecution = Convert.ToInt32(drList["BestExecution"]);
                            this._dSentDate = Convert.ToDateTime(drList["SentDate"]);
                            this._iSendCheck = Convert.ToInt32(drList["SendCheck"]);
                            this._iFIX_A = Convert.ToInt32(drList["FIX_A"]);
                            this._dFIX_RecievedDate = Convert.ToDateTime(drList["FIX_RecievedDate"]);
                            this._dExecuteDate = Convert.ToDateTime(drList["ExecuteDate"]);
                            this._decRealPrice = Convert.ToDecimal(drList["RealPrice"]);
                            this._decRealQuantity = Convert.ToDecimal(drList["RealQuantity"]);
                            this._decRealAmount = Convert.ToDecimal(drList["RealAmount"]);
                            this._iExecutionStockExchange_ID = Convert.ToInt32(drList["RealStockExchange_ID"]);
                            this._decFeesCalc = Convert.ToDecimal(drList["FeesCalc"]);
                            this._decProviderFees = Convert.ToDecimal(drList["ProviderFees"]);
                            this._decFeesDiff = Convert.ToDecimal(drList["FeesDiff"]);
                            this._decFeesMarket = Convert.ToDecimal(drList["FeesMarket"]);
                            this._decAccruedInterest = Convert.ToDecimal(drList["AccruedInterest"]);
                            this._decCommission = Convert.ToDecimal(drList["Commission"]);
                            this._decCurrRate = Convert.ToDecimal(drList["CurrRate"]);
                            this._sNotes = drList["Notes"] + "";
                            this._sValueDate = drList["ValueDate"] + "";
                            this._iInformationMethod_ID = Convert.ToInt32(drList["InformationMethod_ID"]);
                            this._sOfficialInformingDate = drList["OfficialInformingDate"] + "";
                            this._dSettlementDate = Convert.ToDateTime(drList["SettlementDate"]);
                            this._decFeesPercent = Convert.ToDecimal(drList["FeesPercent"]);
                            this._decFeesAmount = Convert.ToDecimal(drList["FeesAmount"]);
                            this._decFeesDiscountPercent = Convert.ToDecimal(drList["FeesDiscountPercent"]);
                            this._decFeesDiscountAmount = Convert.ToDecimal(drList["FeesDiscountAmount"]);
                            this._decFinishFeesPercent = Convert.ToDecimal(drList["FinishFeesPercent"]);
                            this._decFinishFeesAmount = Convert.ToDecimal(drList["FinishFeesAmount"]);
                            this._decFeesRate = Convert.ToDecimal(drList["FeesRate"]);
                            this._decFeesAmountEUR = Convert.ToDecimal(drList["FeesAmountEUR"]);
                            this._decMinFeesAmount = Convert.ToDecimal(drList["MinFeesAmount"]);
                            this._decMinFeesDiscountPercent = Convert.ToDecimal(drList["MinFeesDiscountPercent"]);
                            this._decMinFeesDiscountAmount = Convert.ToDecimal(drList["MinFeesDiscountAmount"]);
                            this._decFinishMinFeesAmount = Convert.ToDecimal(drList["FinishMinFeesAmount"]);
                            this._decMinFeesRate = Convert.ToDecimal(drList["MinFeesRate"]);
                            this._decMinAmountEUR = Convert.ToDecimal(drList["MinAmountEUR"]);
                            this._decTicketFee = Convert.ToDecimal(drList["TicketFee"]);
                            this._decTicketFeeDiscountPercent = Convert.ToDecimal(drList["TicketFeeDiscountPercent"]);
                            this._decTicketFeeDiscountAmount = Convert.ToDecimal(drList["TicketFeeDiscountAmount"]);
                            this._decFinishTicketFee = Convert.ToDecimal(drList["FinishTicketFee"]);
                            this._decTicketFeesRate = Convert.ToDecimal(drList["TicketFeesRate"]);
                            this._decTicketFeesAmountEUR = Convert.ToDecimal(drList["TicketFeesAmountEUR"]);
                            this._decFeesCalc = Convert.ToDecimal(drList["FeesCalc"]);
                            this._decProviderFees = Convert.ToDecimal(drList["ProviderFees"]);
                            this._decRTO_FeesPercent = Convert.ToDecimal(drList["RTO_FeesPercent"]);
                            this._decRTO_FeesAmount = Convert.ToDecimal(drList["RTO_FeesAmount"]);
                            this._decRTO_FeesDiscountPercent = Convert.ToDecimal(drList["RTO_FeesDiscountPercent"]);
                            this._decRTO_FeesDiscountAmount = Convert.ToDecimal(drList["RTO_FeesDiscountAmount"]);
                            this._decRTO_FinishFeesPercent = Convert.ToDecimal(drList["RTO_FinishFeesPercent"]);
                            this._decRTO_FinishFeesAmount = Convert.ToDecimal(drList["RTO_FinishFeesAmount"]);
                            
                            this._decRTO_FeesAmountEUR = Convert.ToDecimal(drList["RTO_FeesAmountEUR"]);
                            this._sRTO_MinFeesCurr = drList["RTO_MinFeesCurr"] + "";
                            this._decRTO_MinFeesAmount = Convert.ToDecimal(drList["RTO_MinFeesAmount"]);
                            this._decRTO_MinFeesDiscountPercent = Convert.ToDecimal(drList["RTO_MinFeesDiscountPercent"]);
                            this._decRTO_MinFeesDiscountAmount = Convert.ToDecimal(drList["RTO_MinFeesDiscountAmount"]);
                            this._decRTO_FinishMinFeesAmount = Convert.ToDecimal(drList["RTO_FinishMinFeesAmount"]);
                            this._sRTO_TicketFeeCurr = drList["RTO_TicketFeeCurr"] + "";
                            this._decRTO_TicketFee = Convert.ToDecimal(drList["RTO_TicketFee"]);
                            this._decRTO_TicketFeeDiscountPercent = Convert.ToDecimal(drList["RTO_TicketFeeDiscountPercent"]);
                            this._decRTO_TicketFeeDiscountAmount = Convert.ToDecimal(drList["RTO_TicketFeeDiscountAmount"]);
                            this._decRTO_FinishTicketFee = Convert.ToDecimal(drList["RTO_FinishTicketFee"]);
                            this._decRTO_FeesProVAT = Convert.ToDecimal(drList["RTO_FeesProVAT"]);
                            this._decRTO_FeesVAT = Convert.ToDecimal(drList["RTO_FeesVAT"]);
                            this._decRTO_CompanyFees = Convert.ToDecimal(drList["RTO_CompanyFees"]);
                            this._iRTO_InvoiceTitle_ID = Convert.ToInt32(drList["RTO_InvoiceTitle_ID"]);
                            this._decFeesMisc = Convert.ToDecimal(drList["FeesMisc"]);
                            this._sFeesNotes = drList["FeesNotes"] + "";
                            this._iFeesCalcMode = Convert.ToInt32(drList["FeesCalcMode"]);
                            this._iPinakidio = Convert.ToInt32(drList["Pinakidio"]);
                            this._sLastCheckFile = drList["LastCheckFile"] + "";
                            this._iUser_ID = Convert.ToInt32(drList["User_ID"]);
                            this._dDateIns = Convert.ToDateTime(drList["DateIns"]);
                            this._iStatus = Convert.ToInt32(drList["Status"]);
                            this._sCurrency = drList["Curr"] + "";
                            this._decMinimumFees = Convert.ToDecimal(drList["MinFeesAmount"]);
                            this._sMinFeesCurr = drList["MinFeesCurr"] + "";
                            this._sClientName = "";
                            this._sAuthorName = (drList["AuthorSurname"] + " " + drList["AuthorFirstname"]).Trim();
                            this._sCompanyTitle = (drList["DiaxSurname"] + " " + drList["DiaxFirstname"]).Trim();                            
                            //this._iPackageType_ID = Convert.ToInt32(drList["PackageType_ID"]);
                            //this._sPackage_Title = drList["Package_Title"] + "  ver." + drList["PackageVersion"];
                            this._sProduct_Title = drList["ProductTitle"] + "";
                            this._sProductCategory_Title = drList["ProductCategories_Title"] + "";
                            this._sProductStockExchange_Title = drList["ProductStockExchange_MIC"] + ""; // + " / " + drList["ProductStockExchange_Title"];
                            this._sStockExchange_Title = drList["StockExchange_MIC"] + "";
                            this._iSecurity_Share_ID = Convert.ToInt32(drList["Share_ID"]);
                            this._sSecurity_Code = drList["ShareCode"] + "";
                            this._sSecurity_Code2 = drList["ShareCode2"] + "";
                            this._sSecurity_ISIN = drList["ISIN"] + "";
                            this._sSecurity_Title = drList["Share_Title"] + "";
                            this._dSecurity_Date1 = Convert.ToDateTime(drList["Date1"]);
                            this._dSecurity_Date3 = Convert.ToDateTime(drList["Date3"]);
                            this._decSecurity_Coupone = Convert.ToDecimal(drList["Coupone"]);
                            this._iSecurity_FrequencyClipping = Convert.ToInt32(drList["FrequencyClipping"]);
                            this._sRecieveTitle = drList["RecieveTitle"] + "";
                        }
                        drList.Close();
                        break;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int GetStartRecord()
        {
            try
            {
                GetRecord();
                if (_dFirstOrderDate != Convert.ToDateTime("1900/01/01"))
                {
                    _dDateFrom = _dFirstOrderDate;
                    _dDateTo = _dFirstOrderDate;
                }
                else
                {
                    _dDateFrom = Convert.ToDateTime("1900/01/01");
                    _dDateTo = _dAktionDate;
                }
                GetList();
                foreach (DataRow dtRow in _dtList.Rows) {
                    if ((dtRow["BulkCommand"] + "") == _sBulkCommand && Convert.ToInt32(dtRow["Aktion"]) == _iAktion) {
                        if (_dFirstOrderDate != Convert.ToDateTime("1900/01/01") && Convert.ToDateTime(dtRow["AktionDate"]).Date == _dFirstOrderDate.Date)
                           _iRecord_ID = Convert.ToInt32(dtRow["ID"]);
                        else
                           _iRecord_ID = Convert.ToInt32(dtRow["ID"]);
                    }
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return _iRecord_ID;
        }
        public void GetList()
        {
            try
            {
                _dtList = new DataTable("Orders_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ClientOrderID", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("BulkCommand", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CommandType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Tipos", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Parent_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("InvestPolicy_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InvestProfile_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AdvisoryInvestmentPolicy_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AdvisoryInvestmentPolicy_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DiscretInvestmentPolicy_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DiscretInvestmentPolicy_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DealAdvisoryInvestmentPolicy_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DealAdvisoryInvestmentPolicy_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientType", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("ClientFullName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientSurname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientFirstname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientSurnameEng", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientFirstnameEng", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientDoB", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientLEI", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SurnameFather", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("EMail", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("EMail_Today", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Mobile", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SendSMS", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Address", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("City", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ZIP", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Country_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Country_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CountryTax_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CountryTax_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Contract_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Details_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Packages_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("MiFIDCategory_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("StockCompanyTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ServiceProvider_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ServiceProvider_LEI", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProductStockExchange_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ProductStockExchange_MIC", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProductStockExchange_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockExchange_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StockExchange_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockExchange_MIC", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ExecutionStockExchange_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ExecutionStockExchange_MIC", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ExecutionStockExchange_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ContractTipos", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Recipient", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Aktion", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("AktionDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Product_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProductCategory_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Product_Category", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Share_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_Code2", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_ISIN", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("PriceType", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Price", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Quantity", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("Amount", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CurrRate", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Constant", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ConstantDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ExecuteDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("RealPrice", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("RealQuantity", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("RealAmount", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("RealAmount_EUR", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("FeesDiff", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesMarket", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("OfficialInformingDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RecieveDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("RecieveTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InformationTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Author_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Advisor_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Diax_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AdvisorSurname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AdvisorFirstname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AdvisorDoB", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesDiscountPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesDiscountAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishFeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishFeesAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesCalc", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ProviderFees", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesMisc", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Service_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("ServiceTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("II_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Risk", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("BusinessType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Executor_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ValueDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AccruedInterest", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Depository_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("QuantityMin", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("QuantityStep", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ConnectionMethod", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("InvoiceFileName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("LastCheckFile", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SendCheck", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("FIX_A", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("FIX_RecievedDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("SentDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("Type", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Recomend", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DateIns", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("User_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetSecurities_List", conn);
                cmd.CommandTimeout = 6000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CommandType_ID", _iCommandType_ID));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dDateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dDateTo));
                cmd.Parameters.Add(new SqlParameter("@StockCompany_ID", _iServiceProvider_ID));
                cmd.Parameters.Add(new SqlParameter("@Sent", _iSent));
                cmd.Parameters.Add(new SqlParameter("@Actions", _iActions));
                cmd.Parameters.Add(new SqlParameter("@SendCheck", _iSendCheck));
                cmd.Parameters.Add(new SqlParameter("@User_ID", _iUser_ID));
                cmd.Parameters.Add(new SqlParameter("@User1_ID", _iUser1_ID));
                cmd.Parameters.Add(new SqlParameter("@User4_ID", _iUser4_ID));
                cmd.Parameters.Add(new SqlParameter("@Division_ID", _iDivision_ID));
                cmd.Parameters.Add(new SqlParameter("@ClientCode", _sCode));
                cmd.Parameters.Add(new SqlParameter("@Product_ID", _iProduct_ID));
                cmd.Parameters.Add(new SqlParameter("@Share_ID", _iShare_ID));
                cmd.Parameters.Add(new SqlParameter("@Currency", _sCurrency));
                cmd.Parameters.Add(new SqlParameter("@ShowCancelled", _iShowCancelled));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
 //                   if (Convert.ToInt32(drList["ID"]) == 508264)
 //                       _iShare_ID = _iShare_ID;
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["BulkCommand"] = drList["BulkCommand"];
                    this.dtRow["CommandType_ID"] = drList["CommandType_ID"];
                    this.dtRow["Tipos"] = drList["Tipos"];
                    this.dtRow["Client_ID"] = drList["Client_ID"];

                    this.dtRow["ClientType"] = drList["Tipos"];
                    this.dtRow["ClientFullName"] = "";
                    switch (Convert.ToInt32(drList["CommandType_ID"]))
                    {
                        case 1:
                            if (Convert.ToInt32(drList["Tipos"]) == 1) this.dtRow["ClientFullName"] = drList["Surname"] + " " + drList["Firstname"];
                            else                                       this.dtRow["ClientFullName"] = drList["Surname"] + "";

                            this.dtRow["ClientLEI"] = "";
                                break;
                        case 2:
                            this.dtRow["ClientFullName"] = drList["Company_Title"] + "";
                            this.dtRow["ClientLEI"] = drList["FirstnameSizigo"] + "";
                            break;
                    }

                    this.dtRow["ClientSurname"] = drList["Surname"] + "";
                    this.dtRow["ClientFirstname"] = drList["Firstname"] + "";
                    this.dtRow["ClientSurnameEng"] = drList["SurnameEng"] + "";
                    this.dtRow["ClientFirstnameEng"] = drList["FirstnameEng"] + "";
                    this.dtRow["ClientDoB"] = drList["DoB"];
                    this.dtRow["SurnameFather"] = drList["SurnameFather"] + "";
                    this.dtRow["Email"] = drList["EMail"] + "";
                    this.dtRow["Email_Today"] = drList["EMail_Today"] + "";
                    this.dtRow["Mobile"] = drList["Mobile"] + "";
                    this.dtRow["SendSMS"] = drList["SendSMS"] + "";
                    this.dtRow["Address"] = drList["Address"] + "";
                    this.dtRow["City"] = drList["City"] + "";
                    this.dtRow["ZIP"] = drList["ZIP"] + "";
                    this.dtRow["Country_Code"] = drList["Country_Code"] + "";
                    this.dtRow["Country_Title"] = drList["Country_Title"] + "";
                    this.dtRow["CountryTax_Code"] = drList["CountryTax_Code"] + "";
                    this.dtRow["CountryTax_Title"] = drList["CountryTax_Title"] + "";
                    this.dtRow["Contract_ID"] = drList["ClientPackage_ID"];
                    this.dtRow["Contract_Details_ID"] = drList["Contract_Details_ID"];
                    this.dtRow["Contract_Packages_ID"] = drList["Contract_Packages_ID"];
                    this.dtRow["MiFIDCategory_ID"] = drList["MiFIDCategory_ID"];
                    this.dtRow["StockCompanyTitle"] = drList["StockCompanyTitle"] + "";
                    this.dtRow["ContractTitle"] = drList["ContractTitle"] + "";
                    this.dtRow["ServiceProvider_ID"] = drList["StockCompany_ID"];
                    this.dtRow["ServiceProvider_Title"] = drList["StockCompanyTitle"] + "";
                    this.dtRow["ServiceProvider_LEI"] = drList["StockCompanyLEI"] + "";
                    this.dtRow["ProductStockExchange_ID"] = drList["ProductStockExchange_ID"];
                    this.dtRow["ProductStockExchange_MIC"] = drList["ProductStockExchange_MIC"] + "";
                    this.dtRow["ProductStockExchange_Title"] = drList["ProductStockExchange_Title"] + "";
                    this.dtRow["StockExchange_ID"] = drList["StockExchange_ID"];
                    this.dtRow["StockExchange_MIC"] = drList["StockExchange_MIC"] + "";
                    this.dtRow["StockExchange_Title"] = drList["StockExchange_Title"] + "";
                    this.dtRow["ExecutionStockExchange_ID"] = drList["ExecutionStockExchange_ID"];
                    this.dtRow["ExecutionStockExchange_MIC"] = drList["ExecutionStockExchange_MIC"] + "";
                    this.dtRow["ExecutionStockExchange_Title"] = drList["ExecutionStockExchange_Title"] + "";

                    this.dtRow["ContractTipos"] = drList["ContractTipos"];
                    this.dtRow["Code"] = drList["Code"] + "";
                    this.dtRow["Portfolio"] = drList["ProfitCenter"] + "";
                    this.dtRow["Recipient"] = drList["Recipient"] + "";
                    this.dtRow["Aktion"] = drList["Aktion"];
                    this.dtRow["AktionDate"] = drList["AktionDate"];      // Convert.ToDateTime(drList["AktionDate"]).ToString("dd/MM/yyyy");
                    this.dtRow["Product_ID"] = drList["Product_ID"];
                    this.dtRow["Product_Title"] = drList["ProductTitle"] + "";
                    this.dtRow["ProductCategory_ID"] = drList["ProductCategory_ID"];
                    this.dtRow["Product_Category"] = drList["ProductCategory"];
                    this.dtRow["Share_ID"] = drList["Share_ID"];
                    this.dtRow["Share_Code"] = drList["ShareCode"] + "";
                    this.dtRow["Share_Code2"] = drList["ShareCode2"] + "";
                    this.dtRow["Share_Title"] = drList["ShareTitle"] + "";
                    this.dtRow["Share_ISIN"] = drList["ISIN"] + "";
                    this.dtRow["PriceType"] = drList["Type"];
                    this.dtRow["Price"] = drList["Price"];
                    this.dtRow["Quantity"] = drList["Quantity"];
                    this.dtRow["Amount"] = drList["Amount"];
                    this.dtRow["Currency"] = drList["Curr"] + "";
                    this.dtRow["CurrRate"] = drList["CurrRate"];
                    this.dtRow["Constant"] = drList["Constant"];
                    this.dtRow["ConstantDate"] = drList["ConstantDate"];
                    this.dtRow["RealPrice"] = drList["RealPrice"];
                    this.dtRow["RealQuantity"] = drList["RealQuantity"];
                    this.dtRow["RealAmount"] = drList["RealAmount"];

                    //if (drList["Curr"] + "" == "EUR") this.dtRow["RealAmount_EUR"] = drList["RealAmount"];
                    //else if (Convert.ToSingle(drList["CurrRate"]) != 0) this.dtRow["RealAmount_EUR"] = Convert.ToDecimal(drList["RealAmount"]) / Convert.ToDecimal(drList["CurrRate"]);
                    this.dtRow["RealAmount_EUR"] = 0;

                    this.dtRow["FeesDiff"] = drList["FeesDiff"];
                    this.dtRow["FeesMarket"] = drList["FeesMarket"];
                    if (Convert.ToDateTime(drList["RecieveDate"]) == Convert.ToDateTime("01/01/1900")) this.dtRow["RecieveDate"] = "01/01/1900";
                    else this.dtRow["RecieveDate"] = Convert.ToDateTime(drList["RecieveDate"]).ToString("dd/MM/yy HH:mm:ss");

                    this.dtRow["RecieveTitle"] = drList["RecieveTitle"];
                    if (Convert.ToDouble(this.dtRow["RealPrice"]) == 0) this.dtRow["ExecuteDate"] = "01/01/1900";
                    else this.dtRow["ExecuteDate"] = Convert.ToDateTime(drList["ExecuteDate"]).ToString("dd/MM/yy HH:mm:ss");

                    this.dtRow["InformationTitle"] = drList["InformationTitle"];
                    this.dtRow["OfficialInformingDate"] = drList["OfficialInformingDate"] + "";
                    this.dtRow["Notes"] = drList["Notes"];
                    this.dtRow["Author_Fullname"] = (drList["AuthorSurname"] + " " + drList["AuthorFirstname"]).Trim();
                    this.dtRow["Advisor_Fullname"] = (drList["AdvisorSurname"] + " " + drList["AdvisorFirstname"]).Trim();
                    this.dtRow["Diax_Fullname"] = (drList["DiaxSurname"] + " " + drList["DIaxFirstname"]).Trim();
                    this.dtRow["AdvisorSurname"] = drList["AdvisorSurname"] + "";
                    this.dtRow["AdvisorFirstname"] = drList["AdvisorFirstname"] + "";
                    if (this.dtRow["AdvisorSurname"] + "" != "") this.dtRow["AdvisorDoB"] =drList["AdvisorDoB"];
                    else this.dtRow["AdvisorDoB"] = "";
     
                    this.dtRow["FeesPercent"] = drList["FeesPercent"];
                    this.dtRow["FeesAmount"] = drList["FeesAmount"];
                    this.dtRow["FeesDiscountPercent"] = drList["FeesDiscountPercent"];
                    this.dtRow["FeesDiscountAmount"] = drList["FeesDiscountAmount"];
                    this.dtRow["FinishFeesPercent"] = drList["FinishFeesPercent"];
                    this.dtRow["FinishFeesAmount"] = drList["FinishFeesAmount"];
                    this.dtRow["FeesCalc"] = drList["FeesCalc"];
                    this.dtRow["ProviderFees"] = drList["ProviderFees"];
                    this.dtRow["FeesMisc"] = drList["FeesMisc"];
                    this.dtRow["Service_ID"] = drList["Service_ID"];
                    this.dtRow["ServiceTitle"] = drList["ServiceTitle"];
                    this.dtRow["II_ID"] = drList["II_ID"];
                    this.dtRow["Risk"] = drList["Risk"];
                    this.dtRow["Status"] = drList["Status"];
                    this.dtRow["BusinessType_ID"] = drList["BusinessType_ID"];
                    this.dtRow["AccruedInterest"] = drList["AccruedInterest"];
                    this.dtRow["Depository_Title"] = drList["Depository_Title"];
                    this.dtRow["QuantityMin"] = drList["QuantityMin"];
                    this.dtRow["QuantityStep"] = drList["QuantityStep"];
                    this.dtRow["ConnectionMethod"] = drList["ConnectionMethod"];
                    this.dtRow["LastCheckFile"] = drList["LastCheckFile"];
                    this.dtRow["InvoiceFileName"] = drList["FileName"];
                    this.dtRow["SendCheck"] = drList["SendCheck"];
                    this.dtRow["FIX_A"] = drList["FIX_A"];
                    this.dtRow["FIX_RecievedDate"] = drList["FIX_RecievedDate"];                    
                    this.dtRow["SentDate"] = drList["SentDate"]; 
                    this.dtRow["Type"] = drList["Type"];
                    this.dtRow["Parent_ID"] = drList["Parent_ID"];
                    this.dtRow["InvestPolicy_Title"] = drList["InvestPolicy_Title"] + "";
                    this.dtRow["InvestProfile_Title"] = drList["InvestProfile_Title"] + "";
                    this.dtRow["AdvisoryInvestmentPolicy_ID"] = drList["AdvisoryInvestmentPolicy_ID"];
                    this.dtRow["AdvisoryInvestmentPolicy_Title"] = drList["AdvisoryInvestmentPolicy_Title"] + "";
                    this.dtRow["DiscretInvestmentPolicy_ID"] = drList["DiscretInvestmentPolicy_ID"];
                    this.dtRow["DiscretInvestmentPolicy_Title"] = drList["DiscretInvestmentPolicy_Title"] + "";
                    this.dtRow["DealAdvisoryInvestmentPolicy_ID"] = drList["DealAdvisoryInvestmentPolicy_ID"];
                    this.dtRow["DealAdvisoryInvestmentPolicy_Title"] = drList["DealAdvisoryInvestmentPolicy_Title"] + "";                
                    this.dtRow["ClientOrderID"] = drList["ClientOrderID"] + "";
                    this.dtRow["Recomend"] = Convert.ToInt32(drList["HFIC_Recom"]) == 1 ? "Ναί" : "Όχι";
                    this.dtRow["DateIns"] = drList["DateIns"];
                    this.dtRow["User_ID"] = drList["User_ID"];
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
       
        public void GetList_Adapter()
        {

            SqlConnection con = new SqlConnection(Global.connStr);
            SqlDataAdapter da = new SqlDataAdapter("GetSecurities_List_Adapter", conn);
            da.SelectCommand.CommandTimeout = 6000;
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@CommandType_ID", SqlDbType.Int).Value = _iCommandType_ID;
            da.SelectCommand.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = _dDateFrom;
            da.SelectCommand.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = _dDateTo;
            da.SelectCommand.Parameters.Add("@StockCompany_ID", SqlDbType.Int).Value = _iServiceProvider_ID;
            da.SelectCommand.Parameters.Add("@Sent", SqlDbType.Int).Value = _iSent;
            da.SelectCommand.Parameters.Add("@Actions", SqlDbType.Int).Value = _iActions;
            da.SelectCommand.Parameters.Add("@SendCheck", SqlDbType.Int).Value = _iSendCheck;
            da.SelectCommand.Parameters.Add("@User_ID", SqlDbType.Int).Value = _iUser_ID;
            da.SelectCommand.Parameters.Add("@User1_ID", SqlDbType.Int).Value = _iUser1_ID;
            da.SelectCommand.Parameters.Add("@User4_ID", SqlDbType.Int).Value = _iUser4_ID;
            da.SelectCommand.Parameters.Add("@Division_ID", SqlDbType.Int).Value = Division_ID;
            da.SelectCommand.Parameters.Add("@ClientCode", SqlDbType.NVarChar, 30).Value = _sCode;
            da.SelectCommand.Parameters.Add("@Product_ID", SqlDbType.Int).Value = _iProduct_ID;
            da.SelectCommand.Parameters.Add("@Share_ID", SqlDbType.Int).Value = _iShare_ID;
            da.SelectCommand.Parameters.Add("@Currency", SqlDbType.NVarChar, 6).Value = _sCurrency;
            da.SelectCommand.Parameters.Add("@ShowCancelled", SqlDbType.Int).Value = _iShowCancelled;            
            _dtList = new DataTable();
            da.Fill(_dtList);
        }
        public void GetList_BulkCommand() 
        {
            try
            {
                _dtList = new DataTable("Orders_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("BulkCommand", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CommandType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Details_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Packages_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StockCompany_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ClientFullName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Share_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Share_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ISIN", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Aktion", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("AktionDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Price", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Quantity", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Amount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Constant", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ConstantDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ExecuteDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("RealPrice", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("RealQuantity", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("RealAmount", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("FeesDiff", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("FeesMarket", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("AccruedInterest", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("Commission", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SE_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Depository_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AllocationPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int16"));

                conn.Open();
                cmd = new SqlCommand("GetSingleCommands", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AktionDate", _dAktionDate));
                cmd.Parameters.Add(new SqlParameter("@BulkCommand", _sBulkCommand));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["BulkCommand"] = drList["BulkCommand"];
                    this.dtRow["CommandType_ID"] = drList["CommandType_ID"];
                    this.dtRow["Contract_ID"] = drList["ClientPackage_ID"];
                    this.dtRow["Contract_Details_ID"] = drList["Contract_Details_ID"];
                    this.dtRow["Contract_Packages_ID"] = drList["Contract_Packages_ID"];               
                    this.dtRow["StockCompany_ID"] = drList["StockCompany_ID"];
                    this.dtRow["ContractTitle"] = drList["ContractTitle"] + "";
                    this.dtRow["Client_ID"] = drList["Client_ID"];
                    this.dtRow["ClientFullName"] = "";
                    switch (Convert.ToInt32(drList["CommandType_ID"]))
                    {
                        case 1:
                            if (Convert.ToInt32(drList["Tipos"]) == 1) this.dtRow["ClientFullName"] = (drList["Surname"] + " " + drList["Firstname"]).Trim();
                            else this.dtRow["ClientFullName"] = drList["Surname"] + "";
                            this.dtRow["AllocationPercent"] = 100;
                            break;
                        case 2:
                            this.dtRow["ClientFullName"] = drList["Company_Title"] + "";
                            this.dtRow["AllocationPercent"] = 100;
                            break;
                        case 4:
                            this.dtRow["ClientFullName"] = (drList["DiaxSurname"] + " " + drList["DiaxFirstname"]).Trim();
                            if (drList["AllocationPercent"] + "" == "") this.dtRow["AllocationPercent"] = 100;
                            else this.dtRow["AllocationPercent"] = drList["AllocationPercent"];
                            break;
                    }

                    if (Convert.ToDouble(this.drList["RealPrice"]) == 0) this.dtRow["ExecuteDate"] = "01/01/1900";
                    else this.dtRow["ExecuteDate"] = Convert.ToDateTime(drList["ExecuteDate"]).ToString("dd/MM/yy HH:mm:ss");
                    this.dtRow["Code"] = drList["Code"] + "";
                    this.dtRow["Portfolio"] = drList["ProfitCenter"] + "";
                    this.dtRow["Product_ID"] = drList["Product_ID"];
                    this.dtRow["Share_ID"] = drList["Share_ID"];
                    this.dtRow["Share_Title"] = drList["Share_Title"];
                    this.dtRow["Share_Code"] = drList["Share_Code"];
                    this.dtRow["ISIN"] = drList["ISIN"];
                    this.dtRow["AktionDate"] = drList["AktionDate"];
                    this.dtRow["Aktion"] = drList["Aktion"];
                    this.dtRow["Price"] = drList["Price"];
                    this.dtRow["Quantity"] = drList["Quantity"];
                    this.dtRow["Amount"] = drList["Amount"];
                    this.dtRow["Currency"] = drList["Curr"] + "";
                    this.dtRow["Constant"] = drList["Constant"];
                    this.dtRow["ConstantDate"] = drList["ConstantDate"];
                    this.dtRow["RealPrice"] = drList["RealPrice"];
                    this.dtRow["RealQuantity"] = drList["RealQuantity"];
                    this.dtRow["RealAmount"] = drList["RealAmount"];
                    this.dtRow["FeesDiff"] = drList["FeesDiff"];
                    this.dtRow["FeesMarket"] = drList["FeesMarket"];
                    this.dtRow["AccruedInterest"] = drList["AccruedInterest"];
                    this.dtRow["Commission"] = drList["Commission"];
                    this.dtRow["SE_Code"] = drList["SE_Code"];
                    this.dtRow["Depository_Code"] = drList["Depository_Code"];
                    this.dtRow["AllocationPercent"] = drList["AllocationPercent"];
                    this.dtRow["Notes"] = drList["Notes"];
                    this.dtRow["Status"] = drList["Status"];
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList_Period()
        {
            try
            {
                _dtList = new DataTable("Orders_Period_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Share_ID", System.Type.GetType("System.Int32"));
               
                conn.Open();
                cmd = new SqlCommand("GetCommands_Period", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dDateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dDateTo));
                cmd.Parameters.Add(new SqlParameter("@Share_ID", _iShare_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["Share_ID"] = drList["Share_ID"];
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }

        public void GetExecutionList()
        {
            try
            {
                _dtList = new DataTable("Orders_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ClientOrderID", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("BulkCommand", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CommandType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Tipos", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Parent_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("InvestPolicy_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InvestProfile_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AdvisoryInvestmentPolicy_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AdvisoryInvestmentPolicy_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DiscretInvestmentPolicy_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DiscretInvestmentPolicy_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DealAdvisoryInvestmentPolicy_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DealAdvisoryInvestmentPolicy_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientType", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("ClientFullName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientSurname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientFirstname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientSurnameEng", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientFirstnameEng", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientDoB", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientLEI", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SurnameFather", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("EMail", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Address", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("City", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ZIP", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Country_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Country_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CountryTax_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CountryTax_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Contract_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Details_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Packages_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("MiFIDCategory_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("StockCompanyTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockCompany_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ServiceProvider_LEI", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProductStockExchange_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ProductStockExchange_MIC", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProductStockExchange_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockExchange_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StockExchange_MIC", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockExchange_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ExecutionStockExchange_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ExecutionStockExchange_MIC", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ExecutionStockExchange_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ContractTipos", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Recipient", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Aktion", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("AktionDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Product_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProductCategory_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Product_Category", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Share_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_Code2", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_ISIN", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("PriceType", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Price", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Quantity", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Amount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CurrRate", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Constant", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ConstantDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ExecuteDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("RealPrice", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RealQuantity", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RealAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesDiff", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesMarket", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("OfficialInformingDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RecieveDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("RecieveTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InformationTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Author_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Advisor_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Diax_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AdvisorSurname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AdvisorFirstname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AdvisorDoB", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesDiscountPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesDiscountAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishFeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishFeesAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesCalc", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ProviderFees", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesMisc", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Service_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("ServiceTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("II_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Risk", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("BusinessType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Executor_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ValueDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AccruedInterest", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Depository_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("QuantityMin", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("QuantityStep", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("BestExecution", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("ConnectionMethod", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("InvoiceFileName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("LastCheckFile", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SendCheck", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("FIX_A", System.Type.GetType("System.Int16"));                
                dtCol = _dtList.Columns.Add("FIX_RecievedDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("SentDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("Type", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Recomend", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DateIns", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("User_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetExecutionOrders_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CommandType_ID", _iCommandType_ID));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dDateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dDateTo));
                cmd.Parameters.Add(new SqlParameter("@StockCompany_ID", _iServiceProvider_ID));
                cmd.Parameters.Add(new SqlParameter("@Sent", _iSent));
                cmd.Parameters.Add(new SqlParameter("@Actions", _iActions));
                cmd.Parameters.Add(new SqlParameter("@User_ID", _iUser_ID));
                cmd.Parameters.Add(new SqlParameter("@User1_ID", _iUser1_ID));
                cmd.Parameters.Add(new SqlParameter("@User4_ID", _iUser4_ID));
                cmd.Parameters.Add(new SqlParameter("@Division_ID", _iDivision_ID));
                cmd.Parameters.Add(new SqlParameter("@ClientCode", _sCode));
                cmd.Parameters.Add(new SqlParameter("@ShowCancelled", _iShowCancelled));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];

                    this.dtRow["BulkCommand"] = drList["BulkCommand"];
                    this.dtRow["CommandType_ID"] = drList["CommandType_ID"];
                    this.dtRow["Tipos"] = drList["Tipos"];
                    this.dtRow["Client_ID"] = drList["Client_ID"];

                    this.dtRow["ClientType"] = drList["Tipos"];
                    this.dtRow["ClientFullName"] = "";
                    switch (Convert.ToInt32(drList["CommandType_ID"]))
                    {
                        case 1:
                            if (Convert.ToInt32(drList["Tipos"]) == 1) this.dtRow["ClientFullName"] = drList["Surname"] + " " + drList["Firstname"];
                            else this.dtRow["ClientFullName"] = drList["Surname"] + "";

                            this.dtRow["ClientLEI"] = "";
                            break;
                        case 2:
                            this.dtRow["ClientFullName"] = drList["Company_Title"] + "";
                            this.dtRow["ClientLEI"] = drList["FirstnameSizigo"] + "";
                            break;
                    }

                    this.dtRow["ClientSurname"] = drList["Surname"] + "";
                    this.dtRow["ClientFirstname"] = drList["Firstname"] + "";
                    this.dtRow["ClientSurnameEng"] = drList["SurnameEng"] + "";
                    this.dtRow["ClientFirstnameEng"] = drList["FirstnameEng"] + "";
                    this.dtRow["ClientDoB"] = drList["DoB"];
                    this.dtRow["SurnameFather"] = drList["SurnameFather"] + "";
                    this.dtRow["Email"] = drList["EMail"] + "";
                    this.dtRow["Address"] = drList["Address"] + "";
                    this.dtRow["City"] = drList["City"] + "";
                    this.dtRow["ZIP"] = drList["ZIP"] + "";
                    this.dtRow["Country_Code"] = drList["Country_Code"] + "";
                    this.dtRow["Country_Title"] = drList["Country_Title"] + "";
                    this.dtRow["CountryTax_Code"] = drList["CountryTax_Code"] + "";
                    this.dtRow["CountryTax_Title"] = drList["CountryTax_Title"] + "";
                    this.dtRow["Contract_ID"] = drList["ClientPackage_ID"];
                    this.dtRow["Contract_Details_ID"] = drList["Contract_Details_ID"];
                    this.dtRow["Contract_Packages_ID"] = drList["Contract_Packages_ID"];
                    this.dtRow["MiFIDCategory_ID"] = drList["MiFIDCategory_ID"];
                    this.dtRow["StockCompanyTitle"] = drList["StockCompanyTitle"] + "";
                    this.dtRow["ContractTitle"] = drList["ContractTitle"] + "";
                    this.dtRow["StockCompany_ID"] = drList["StockCompany_ID"];
                    this.dtRow["ServiceProvider_Title"] = drList["StockCompanyTitle"] + "";
                    this.dtRow["ServiceProvider_LEI"] = drList["StockCompanyLEI"] + "";
                    this.dtRow["ProductStockExchange_ID"] = drList["ProductStockExchange_ID"];
                    this.dtRow["ProductStockExchange_MIC"] = drList["ProductStockExchange_MIC"] + "";
                    this.dtRow["ProductStockExchange_Title"] = drList["ProductStockExchange_Title"] + "";
                    this.dtRow["StockExchange_ID"] = drList["StockExchange_ID"];
                    this.dtRow["StockExchange_MIC"] = drList["StockExchange_MIC"] + "";
                    this.dtRow["StockExchange_Title"] = drList["StockExchange_Title"] + "";
                    this.dtRow["ExecutionStockExchange_ID"] = drList["ExecutionStockExchange_ID"];                    
                    this.dtRow["ExecutionStockExchange_MIC"] = drList["ExecutionStockExchange_MIC"] + "";            
                    this.dtRow["ExecutionStockExchange_Title"] = drList["ExecutionStockExchange_Title"] + "";         
                    this.dtRow["ContractTipos"] = drList["ContractTipos"];
                    this.dtRow["Code"] = drList["Code"] + "";
                    this.dtRow["Portfolio"] = drList["ProfitCenter"] + "";
                    this.dtRow["Recipient"] = drList["Recipient"] + "";
                    this.dtRow["Aktion"] = drList["Aktion"];
                    this.dtRow["AktionDate"] = Convert.ToDateTime(drList["AktionDate"]).ToString("dd/MM/yyyy");
                    this.dtRow["Product_ID"] = drList["Product_ID"];
                    this.dtRow["Product_Title"] = drList["ProductTitle"] + "";
                    this.dtRow["ProductCategory_ID"] = drList["ProductCategory_ID"];
                    this.dtRow["Product_Category"] = drList["ProductCategory"];
                    this.dtRow["Share_ID"] = drList["Share_ID"];
                    this.dtRow["Share_Code"] = drList["ShareCode"] + "";
                    this.dtRow["Share_Code2"] = drList["ShareCode2"] + "";
                    this.dtRow["Share_Title"] = drList["ShareTitle"] + "";
                    this.dtRow["Share_ISIN"] = drList["ISIN"] + "";
                    this.dtRow["PriceType"] = drList["Type"];
                    this.dtRow["Price"] = drList["Price"];
                    this.dtRow["Quantity"] = drList["Quantity"];
                    this.dtRow["Amount"] = drList["Amount"];
                    this.dtRow["Currency"] = drList["Curr"] + "";
                    this.dtRow["CurrRate"] = drList["CurrRate"];
                    this.dtRow["Constant"] = drList["Constant"];
                    this.dtRow["ConstantDate"] = drList["ConstantDate"];
                    this.dtRow["RealPrice"] = drList["RealPrice"];
                    this.dtRow["RealQuantity"] = drList["RealQuantity"];
                    this.dtRow["RealAmount"] = drList["RealAmount"];
                    this.dtRow["FeesDiff"] = drList["FeesDiff"];
                    this.dtRow["FeesMarket"] = drList["FeesMarket"];
                    if (Convert.ToDateTime(drList["RecieveDate"]) == Convert.ToDateTime("01/01/1900")) this.dtRow["RecieveDate"] = "01/01/1900";
                    else this.dtRow["RecieveDate"] = Convert.ToDateTime(drList["RecieveDate"]).ToString("dd/MM/yy HH:mm:ss");

                    this.dtRow["RecieveTitle"] = drList["RecieveTitle"];
                    if (Convert.ToDouble(this.dtRow["RealPrice"]) == 0) this.dtRow["ExecuteDate"] = "01/01/1900";
                    else this.dtRow["ExecuteDate"] = Convert.ToDateTime(drList["ExecuteDate"]).ToString("dd/MM/yy HH:mm:ss");

                    this.dtRow["InformationTitle"] = drList["InformationTitle"];
                    this.dtRow["OfficialInformingDate"] = drList["OfficialInformingDate"] + "";
                    this.dtRow["Notes"] = drList["Notes"];
                    this.dtRow["Author_Fullname"] = (drList["AuthorSurname"] + " " + drList["AuthorFirstname"]).Trim();
                    this.dtRow["Advisor_Fullname"] = (drList["AdvisorSurname"] + " " + drList["AdvisorFirstname"]).Trim();
                    this.dtRow["Diax_Fullname"] = (drList["DiaxSurname"] + " " + drList["DIaxFirstname"]).Trim();
                    this.dtRow["AdvisorSurname"] = drList["AdvisorSurname"] + "";
                    this.dtRow["AdvisorFirstname"] = drList["AdvisorFirstname"] + "";
                    if (this.dtRow["AdvisorSurname"] + "" != "") this.dtRow["AdvisorDoB"] = drList["AdvisorDoB"];
                    else this.dtRow["AdvisorDoB"] = "";

                    this.dtRow["FeesPercent"] = drList["FeesPercent"];
                    this.dtRow["FeesAmount"] = drList["FeesAmount"];
                    this.dtRow["FeesDiscountPercent"] = drList["FeesDiscountPercent"];
                    this.dtRow["FeesDiscountAmount"] = drList["FeesDiscountAmount"];
                    this.dtRow["FinishFeesPercent"] = drList["FinishFeesPercent"];
                    this.dtRow["FinishFeesAmount"] = drList["FinishFeesAmount"];
                    this.dtRow["FeesCalc"] = drList["FeesCalc"];
                    this.dtRow["ProviderFees"] = drList["ProviderFees"];
                    this.dtRow["FeesMisc"] = drList["FeesMisc"];
                    this.dtRow["Service_ID"] = drList["Service_ID"];
                    this.dtRow["ServiceTitle"] = drList["ServiceTitle"];
                    this.dtRow["II_ID"] = drList["II_ID"];
                    this.dtRow["Risk"] = drList["Risk"];

                    this.dtRow["Status"] = drList["Status"];
                    this.dtRow["BusinessType_ID"] = drList["BusinessType_ID"];
                    this.dtRow["AccruedInterest"] = drList["AccruedInterest"];
                    this.dtRow["Depository_Title"] = drList["Depository_Title"];
                    this.dtRow["QuantityMin"] = drList["QuantityMin"];
                    this.dtRow["QuantityStep"] = drList["QuantityStep"];
                    this.dtRow["BestExecution"] = drList["BestExecution"];
                    this.dtRow["ConnectionMethod"] = drList["ConnectionMethod"];
                    this.dtRow["LastCheckFile"] = drList["LastCheckFile"];
                    //this.dtRow["InvoiceFileName"] = drList["FileName"];
                    this.dtRow["SendCheck"] = drList["SendCheck"];
                    this.dtRow["FIX_A"] = drList["FIX_A"];                    
                    this.dtRow["FIX_RecievedDate"] = drList["FIX_RecievedDate"];
                    this.dtRow["SentDate"] = drList["SentDate"];
                    this.dtRow["Type"] = drList["Type"];
                    this.dtRow["Parent_ID"] = drList["Parent_ID"];
                    this.dtRow["InvestPolicy_Title"] = drList["InvestPolicy_Title"] + "";
                    this.dtRow["InvestProfile_Title"] = drList["InvestProfile_Title"] + "";
                    this.dtRow["AdvisoryInvestmentPolicy_ID"] = drList["AdvisoryInvestmentPolicy_ID"];
                    this.dtRow["AdvisoryInvestmentPolicy_Title"] = drList["AdvisoryInvestmentPolicy_Title"] + "";
                    this.dtRow["DiscretInvestmentPolicy_ID"] = drList["DiscretInvestmentPolicy_ID"];
                    this.dtRow["DiscretInvestmentPolicy_Title"] = drList["DiscretInvestmentPolicy_Title"] + "";
                    this.dtRow["DealAdvisoryInvestmentPolicy_ID"] = drList["DealAdvisoryInvestmentPolicy_ID"];
                    this.dtRow["DealAdvisoryInvestmentPolicy_Title"] = drList["DealAdvisoryInvestmentPolicy_Title"] + "";
                    this.dtRow["ClientOrderID"] = drList["ClientOrderID"] + "";
                    this.dtRow["Recomend"] = Convert.ToInt32(drList["HFIC_Recom"]) == 1 ? "Ναί" : "Όχι";
                    this.dtRow["DateIns"] = drList["DateIns"];
                    this.dtRow["User_ID"] = drList["User_ID"];
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetDPMList()
        {
            try
            {
                _dtList = new DataTable("DPMOrders_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ClientOrderID", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("BulkCommand", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CommandType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Tipos", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Parent_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Company_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Depository_ID", System.Type.GetType("System.Int32"));                
                dtCol = _dtList.Columns.Add("InvestPolicy_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AdvisoryInvestmentPolicy_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AdvisoryInvestmentPolicy_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DiscretInvestmentPolicy_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DiscretInvestmentPolicy_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DealAdvisoryInvestmentPolicy_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DealAdvisoryInvestmentPolicy_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientType", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("ClientFullName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientSurname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientFirstname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientSurnameEng", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientFirstnameEng", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientDoB", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientLEI", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SurnameFather", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("EMail", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Address", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("City", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ZIP", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Country_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Country_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CountryTax_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CountryTax_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Contract_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Details_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Packages_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("MiFIDCategory_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("StockCompanyTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockCompany_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ServiceProvider_LEI", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProductStockExchange_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ProductStockExchange_MIC", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProductStockExchange_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockExchange_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StockExchange_MIC", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockExchange_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ExecutionStockExchange_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ExecutionStockExchange_MIC", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ExecutionStockExchange_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ContractTipos", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Recipient", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AllocationPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Aktion", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("AktionDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Product_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProductCategory_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Product_Category", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Share_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_Code2", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_ISIN", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("PriceType", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Price", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Quantity", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Amount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CurrRate", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Constant", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ConstantDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ExecuteDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("RealPrice", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RealQuantity", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RealAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesDiff", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesMarket", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("OfficialInformingDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RecieveDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("RecieveTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InformationTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Author_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Advisor_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Diax_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AdvisorSurname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AdvisorFirstname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AdvisorDoB", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesDiscountPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesDiscountAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishFeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishFeesAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesCalc", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ProviderFees", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesMisc", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Service_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("ServiceTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("II_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Risk", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("BusinessType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Executor_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ValueDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AccruedInterest", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Depository_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("QuantityMin", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("QuantityStep", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ConnectionMethod", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("InvoiceFileName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("LastCheckFile", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SendCheck", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("FIX_A", System.Type.GetType("System.Int16"));                
                dtCol = _dtList.Columns.Add("FIX_RecievedDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("SentDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("DPM_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DPM_Notes", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Type", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Recomend", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DateIns", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("User_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetDMPOrders_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CommandType_ID", _iCommandType_ID));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dDateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dDateTo));
                cmd.Parameters.Add(new SqlParameter("@StockCompany_ID", _iServiceProvider_ID));
                cmd.Parameters.Add(new SqlParameter("@User_ID", _iUser_ID));
                cmd.Parameters.Add(new SqlParameter("@Sent", _iSent));
                cmd.Parameters.Add(new SqlParameter("@Actions", _iActions));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];

                    this.dtRow["BulkCommand"] = drList["BulkCommand"];
                    this.dtRow["CommandType_ID"] = drList["CommandType_ID"];
                    this.dtRow["Tipos"] = drList["Tipos"];
                    this.dtRow["Client_ID"] = drList["Client_ID"];

                    this.dtRow["ClientType"] = drList["Tipos"];
                    this.dtRow["ClientFullName"] = "";
                    switch (Convert.ToInt32(drList["CommandType_ID"]))
                    {
                        case 1:
                            if (Convert.ToInt32(drList["Tipos"]) == 1) this.dtRow["ClientFullName"] = drList["Surname"] + " " + drList["Firstname"];
                            else this.dtRow["ClientFullName"] = drList["Surname"] + "";

                            this.dtRow["ClientLEI"] = "";
                            break;
                        case 2:
                            this.dtRow["ClientFullName"] = drList["Company_Title"] + "";
                            this.dtRow["ClientLEI"] = drList["FirstnameSizigo"] + "";
                            break;
                        case 4:
                            if (Convert.ToInt32(drList["Client_ID"]) != 0) { 
                               if (Convert.ToInt32(drList["Tipos"]) == 1) this.dtRow["ClientFullName"] = drList["Surname"] + " " + drList["Firstname"];
                               else this.dtRow["ClientFullName"] = drList["Surname"] + "";
                            }
                            break;
                    }

                    this.dtRow["ClientSurname"] = drList["Surname"] + "";
                    this.dtRow["ClientFirstname"] = drList["Firstname"] + "";
                    this.dtRow["ClientSurnameEng"] = drList["SurnameEng"] + "";
                    this.dtRow["ClientFirstnameEng"] = drList["FirstnameEng"] + "";
                    this.dtRow["ClientDoB"] = drList["DoB"];
                    this.dtRow["SurnameFather"] = drList["SurnameFather"] + "";
                    //this.dtRow["Email"] = drList["EMail"] + "";
                    //this.dtRow["Address"] = drList["Address"] + "";
                    //this.dtRow["City"] = drList["City"] + "";
                    //this.dtRow["ZIP"] = drList["ZIP"] + "";
                    //this.dtRow["Country_Code"] = drList["Country_Code"] + "";
                    //this.dtRow["Country_Title"] = drList["Country_Title"] + "";
                    //this.dtRow["CountryTax_Code"] = drList["CountryTax_Code"] + "";
                    //this.dtRow["CountryTax_Title"] = drList["CountryTax_Title"] + "";
                    this.dtRow["Contract_ID"] = drList["ClientPackage_ID"];
                    this.dtRow["Contract_Details_ID"] = drList["Contract_Details_ID"];
                    this.dtRow["Contract_Packages_ID"] = drList["Contract_Packages_ID"];
                    this.dtRow["MiFIDCategory_ID"] = (drList["MiFIDCategory_ID"]+"") == ""? 0 : Convert.ToInt32(drList["MiFIDCategory_ID"]);
                    this.dtRow["StockCompanyTitle"] = drList["StockCompanyTitle"] + "";
                    this.dtRow["ContractTitle"] = drList["ContractTitle"] + "";
                    this.dtRow["StockCompany_ID"] = drList["StockCompany_ID"];
                    this.dtRow["ServiceProvider_Title"] = drList["StockCompanyTitle"] + "";
                    this.dtRow["ServiceProvider_LEI"] = drList["StockCompanyLEI"] + "";
                    this.dtRow["ProductStockExchange_ID"] = drList["ProductStockExchange_ID"];
                    this.dtRow["ProductStockExchange_MIC"] = drList["ProductStockExchange_MIC"] + "";
                    this.dtRow["ProductStockExchange_Title"] = drList["ProductStockExchange_Title"] + "";
                    this.dtRow["StockExchange_ID"] = drList["StockExchange_ID"];
                    this.dtRow["StockExchange_MIC"] = drList["StockExchanges_MIC"] + "";
                    this.dtRow["StockExchange_Title"] = drList["StockExchangeTitle"] + "";
                    this.dtRow["ExecutionStockExchange_ID"] = drList["ProductStockExchange_ID"];
                    this.dtRow["ExecutionStockExchange_MIC"] = drList["ProductStockExchange_MIC"] + "";
                    this.dtRow["ExecutionStockExchange_Title"] = drList["ProductStockExchange_Title"] + "";
                    this.dtRow["ContractTipos"] = drList["ContractTipos"];
                    this.dtRow["Code"] = drList["Code"] + "";
                    this.dtRow["Portfolio"] = drList["ProfitCenter"] + "";
                    //this.dtRow["Recipient"] = drList["Recipient"] + "";
                    this.dtRow["AllocationPercent"] = drList["AllocationPercent"];
                    this.dtRow["Aktion"] = drList["Aktion"];
                    this.dtRow["AktionDate"] = Convert.ToDateTime(drList["AktionDate"]).ToString("dd/MM/yyyy");
                    this.dtRow["Product_ID"] = drList["Product_ID"];
                    this.dtRow["Product_Title"] = drList["ProductTitle"] + "";
                    this.dtRow["ProductCategory_ID"] = drList["ProductCategory_ID"];
                    this.dtRow["Product_Category"] = drList["ProductCategory"];
                    this.dtRow["Share_ID"] = drList["Share_ID"];
                    this.dtRow["Share_Code"] = drList["ShareCode"] + "";
                    this.dtRow["Share_Code2"] = drList["ShareCode2"] + "";
                    this.dtRow["Share_Title"] = drList["ShareTitle"] + "";
                    this.dtRow["Share_ISIN"] = drList["ISIN"] + "";
                    this.dtRow["PriceType"] = drList["Type"];
                    this.dtRow["Price"] = drList["Price"];
                    this.dtRow["Quantity"] = drList["Quantity"];
                    this.dtRow["Amount"] = drList["Amount"];
                    this.dtRow["Currency"] = drList["Curr"] + "";
                    this.dtRow["CurrRate"] = drList["CurrRate"];
                    this.dtRow["Constant"] = drList["Constant"];
                    this.dtRow["ConstantDate"] = drList["ConstantDate"];
                    this.dtRow["RealPrice"] = drList["RealPrice"];
                    this.dtRow["RealQuantity"] = drList["RealQuantity"];
                    this.dtRow["RealAmount"] = drList["RealAmount"];
                    this.dtRow["FeesDiff"] = drList["FeesDiff"];
                    this.dtRow["FeesMarket"] = drList["FeesMarket"];
                    this.dtRow["RecieveDate"] = Convert.ToDateTime(drList["RecieveDate"]);
                    this.dtRow["RecieveTitle"] = drList["RecieveTitle"];
                    this.dtRow["ExecuteDate"] = Convert.ToDateTime(drList["ExecuteDate"]);
                    this.dtRow["ValueDate"] = drList["ValueDate"] + "";
                    //this.dtRow["InformationTitle"] = drList["InformationTitle"];
                    //this.dtRow["OfficialInformingDate"] = drList["OfficialInformingDate"] + "";
                    this.dtRow["Notes"] = drList["Notes"];
                    this.dtRow["Author_Fullname"] = (drList["AuthorSurname"] + " " + drList["AuthorFirstname"]).Trim();
                    this.dtRow["Advisor_Fullname"] = (drList["AdvisorSurname"] + " " + drList["AdvisorFirstname"]).Trim();
                    this.dtRow["Diax_Fullname"] = (drList["DiaxSurname"] + " " + drList["DIaxFirstname"]).Trim();
                    this.dtRow["AdvisorSurname"] = drList["AdvisorSurname"] + "";
                    this.dtRow["AdvisorFirstname"] = drList["AdvisorFirstname"] + "";
                    if (this.dtRow["AdvisorSurname"] + "" != "") this.dtRow["AdvisorDoB"] = drList["AdvisorDoB"];
                    else this.dtRow["AdvisorDoB"] = "";

                    this.dtRow["FeesPercent"] = drList["FeesPercent"];
                    this.dtRow["FeesAmount"] = drList["FeesAmount"];
                    this.dtRow["FeesDiscountPercent"] = drList["FeesDiscountPercent"];
                    this.dtRow["FeesDiscountAmount"] = drList["FeesDiscountAmount"];
                    this.dtRow["FinishFeesPercent"] = drList["FinishFeesPercent"];
                    this.dtRow["FinishFeesAmount"] = drList["FinishFeesAmount"];
                    this.dtRow["FeesCalc"] = drList["FeesCalc"];
                    this.dtRow["ProviderFees"] = drList["ProviderFees"];
                    this.dtRow["FeesMisc"] = drList["FeesMisc"];
                    //this.dtRow["Service_ID"] = drList["Service_ID"];
                    this.dtRow["ServiceTitle"] = "Διαχείριση";
                    this.dtRow["II_ID"] = drList["II_ID"];
                    this.dtRow["Risk"] = (drList["Risk"] + "") == "" ? 0 : Convert.ToInt32(drList["Risk"]);

                    this.dtRow["Status"] = drList["Status"];
                    this.dtRow["BusinessType_ID"] = drList["BusinessType_ID"];
                    this.dtRow["AccruedInterest"] = drList["AccruedInterest"];
                    //this.dtRow["Depository_Title"] = drList["Depository_Title"];
                    this.dtRow["QuantityMin"] = drList["QuantityMin"];
                    this.dtRow["QuantityStep"] = drList["QuantityStep"];
                    //this.dtRow["ConnectionMethod"] = drList["ConnectionMethod"];
                    //this.dtRow["LastCheckFile"] = drList["LastCheckFile"];
                    //this.dtRow["InvoiceFileName"] = drList["FileName"];
                    this.dtRow["SendCheck"] = drList["SendCheck"];
                    this.dtRow["FIX_A"] = drList["FIX_A"];                    
                    this.dtRow["FIX_RecievedDate"] = drList["FIX_RecievedDate"];
                    this.dtRow["SentDate"] = drList["SentDate"];
                    this.dtRow["DPM_ID"] = drList["DPM_ID"];
                    this.dtRow["DPM_Notes"] = drList["DPM_Notes"] + "";
                    this.dtRow["Type"] = drList["Type"];
                    this.dtRow["Parent_ID"] = drList["Parent_ID"];
                    this.dtRow["Company_ID"] = drList["Company_ID"];
                    this.dtRow["Depository_ID"] = drList["Depository_ID"];

                    /*
                    this.dtRow["InvestPolicy_Title"] = drList["InvestPolicy_Title"];
                    this.dtRow["AdvisoryInvestmentPolicy_ID"] = drList["AdvisoryInvestmentPolicy_ID"];
                    this.dtRow["AdvisoryInvestmentPolicy_Title"] = drList["AdvisoryInvestmentPolicy_Title"];
                    this.dtRow["DiscretInvestmentPolicy_ID"] = drList["DiscretInvestmentPolicy_ID"];
                    this.dtRow["DiscretInvestmentPolicy_Title"] = drList["DiscretInvestmentPolicy_Title"];
                    this.dtRow["DealAdvisoryInvestmentPolicy_ID"] = drList["DealAdvisoryInvestmentPolicy_ID"];
                    this.dtRow["DealAdvisoryInvestmentPolicy_Title"] = drList["DealAdvisoryInvestmentPolicy_Title"];
                    
                    this.dtRow["ClientOrderID"] = drList["ClientOrderID"] + "";
                    this.dtRow["Recomend"] = Convert.ToInt32(drList["HFIC_Recom"]) == 1 ? "Ναί" : "Όχι";
                    */
                    this.dtRow["DateIns"] = drList["DateIns"];
                    this.dtRow["User_ID"] = drList["User_ID"];
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetDPMSources()
        {
            try
            {
                _dtList = new DataTable("DPMSources_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32")); 
                dtCol = _dtList.Columns.Add("BulkCommand", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ISIN", System.Type.GetType("System.String"));

                conn.Open();
                cmd = new SqlCommand("GetDMPSources", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@II_ID", _iII_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["BulkCommand"] = drList["BulkCommand"] + "";
                    this.dtRow["Share_ID"] = drList["Share_ID"];
                    this.dtRow["ISIN"] = drList["ISIN"] + "";
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close();}
        }
        public void GetDPMBrunch()
        {
            int i = 0;
            string sBulkCommand, sBrunchBulk;
            try
            {
                _dtList = new DataTable("DPMOrders_Brunch");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("C4_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Aktion", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ProductCategory_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ShareCodes_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Share_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_ISIN", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("PriceType", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Price", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Quantity", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Amount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Constant", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ConstantDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockExchange_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StockExchange_Title", System.Type.GetType("System.String"));

                conn.Open();
                conn1.Open();
                cmd = new SqlCommand("GetDMPSources", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@II_ID", _iII_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    sBrunchBulk = "";
                    sBulkCommand = drList["BulkCommand"] + "";
                    i = sBulkCommand.IndexOf("/");
                    if (i >= 0)  {
                        sBrunchBulk = sBulkCommand.Substring(i + 1).Replace("<", "").Replace(">", "");

                        cmd1 = new SqlCommand("GetSingleCommands", conn1);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add(new SqlParameter("@AktionDate", _dAktionDate));
                        cmd1.Parameters.Add(new SqlParameter("@BulkCommand", sBrunchBulk));
                        drList1 = cmd1.ExecuteReader();
                        while (drList1.Read())
                        {
                            if (Convert.ToInt32(drList1["CommandType_ID"]) == 1)  {
                                dtRow = _dtList.NewRow();
                                this.dtRow["ID"] = drList1["ID"];
                                this.dtRow["C4_ID"] = drList["ID"];
                                this.dtRow["Aktion"] = drList1["Aktion"];
                                this.dtRow["Product_ID"] = drList1["Product_ID"];
                                this.dtRow["ProductCategory_ID"] = drList1["ProductCategory_ID"];
                                this.dtRow["ShareCodes_ID"] = drList1["Share_ID"];
                                this.dtRow["Share_Code"] = drList1["Share_Code"] + "";
                                this.dtRow["Share_Title"] = drList1["ProductTitle"] + "";
                                this.dtRow["Share_ISIN"] = drList1["ISIN"] + "";
                                this.dtRow["PriceType"] = drList1["Type"];
                                this.dtRow["Price"] = drList1["Price"];
                                this.dtRow["Quantity"] = drList1["Quantity"];
                                this.dtRow["Amount"] = drList1["Amount"];
                                this.dtRow["Currency"] = drList1["Curr"] + "";
                                this.dtRow["Constant"] = drList1["Constant"];
                                this.dtRow["ConstantDate"] = drList1["ConstantDate"];
                                this.dtRow["StockExchange_ID"] = drList1["StockExchange_ID"];
                                this.dtRow["StockExchange_Title"] = drList1["StockExchange_Title"];
                                _dtList.Rows.Add(dtRow);
                            }
                        }
                        drList1.Close();
                    }
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); conn1.Close(); }
        }
        public void GetBulkList()
        {
            try
            {
                _dtList = new DataTable("BulkOrders_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ClientOrderID", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("BulkCommand", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CommandType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Tipos", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Parent_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Company_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Depository_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("InvestPolicy_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AdvisoryInvestmentPolicy_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AdvisoryInvestmentPolicy_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DiscretInvestmentPolicy_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DiscretInvestmentPolicy_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DealAdvisoryInvestmentPolicy_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DealAdvisoryInvestmentPolicy_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientType", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Client_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientSurname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientFirstname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientSurnameEng", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientFirstnameEng", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientDoB", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientLEI", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SurnameFather", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("EMail", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Address", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("City", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ZIP", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Country_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Country_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CountryTax_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CountryTax_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Contract_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Details_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Packages_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("MiFIDCategory_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("StockCompanyTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockCompany_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ServiceProvider_LEI", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProductStockExchange_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ProductStockExchange_MIC", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProductStockExchange_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockExchange_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StockExchange_MIC", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockExchange_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ExecutionStockExchange_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ExecutionStockExchange_MIC", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ExecutionStockExchange_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ContractTipos", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Recipient", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Aktion", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("AktionDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Product_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProductCategory_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Product_Category", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Share_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_Code2", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_ISIN", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("PriceType", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Price", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Quantity", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Amount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CurrRate", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Constant", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ConstantDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ExecuteDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("RealPrice", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RealQuantity", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RealAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesDiff", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesMarket", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("OfficialInformingDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RecieveDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("RecieveTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InformationTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Author_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Advisor_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Diax_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AdvisorSurname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AdvisorFirstname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AdvisorDoB", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesDiscountPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesDiscountAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishFeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishFeesAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesCalc", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ProviderFees", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesMisc", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Service_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("ServiceTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("II_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Risk", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("BusinessType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Executor_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ValueDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AccruedInterest", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Depository_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("QuantityMin", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("QuantityStep", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ConnectionMethod", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("InvoiceFileName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("LastCheckFile", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SendCheck", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("FIX_A", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("FIX_RecievedDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("SentDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("Type", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Recomend", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DateIns", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("User_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetBulkOrders_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CommandType_ID", _iCommandType_ID));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dDateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dDateTo));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];

                    this.dtRow["BulkCommand"] = drList["BulkCommand"];
                    this.dtRow["CommandType_ID"] = drList["CommandType_ID"];
                    //this.dtRow["Tipos"] = drList["Tipos"];
                    this.dtRow["Client_ID"] = drList["Client_ID"];
                    this.dtRow["Client_Title"] = drList["Client_Title"] + "";
                    this.dtRow["ContractTitle"] = drList["ContractTitle"] + "";
                    this.dtRow["StockCompanyTitle"] = drList["StockCompanyTitle"] + "";
                    this.dtRow["Code"] = drList["Code"] + "";
                    this.dtRow["Portfolio"] = drList["ProfitCenter"] + "";

                    /*
                    //this.dtRow["ClientType"] = drList["Tipos"];
                    this.dtRow["ClientFullName"] = "";
                    switch (Convert.ToInt32(drList["CommandType_ID"]))
                    {
                        case 1:
                            //if (Convert.ToInt32(drList["Tipos"]) == 1) 
                            //this.dtRow["ClientFullName"] = drList["Surname"] + " " + drList["Firstname"];
                            //else this.dtRow["ClientFullName"] = drList["Surname"] + "";

                            this.dtRow["ClientLEI"] = "";
                            break;
                        case 2:
                            this.dtRow["ClientFullName"] = drList["Company_Title"] + "";
                            this.dtRow["ClientLEI"] = drList["FirstnameSizigo"] + "";
                            break;
                    }

                    
                    //this.dtRow["ClientSurname"] = drList["Surname"] + "";
                    //this.dtRow["ClientFirstname"] = drList["Firstname"] + "";
                    this.dtRow["ClientSurnameEng"] = drList["SurnameEng"] + "";
                    this.dtRow["ClientFirstnameEng"] = drList["FirstnameEng"] + "";
                    this.dtRow["ClientDoB"] = drList["DoB"];
                    this.dtRow["SurnameFather"] = drList["SurnameFather"] + "";
                    //this.dtRow["Email"] = drList["EMail"] + "";
                    //this.dtRow["Address"] = drList["Address"] + "";
                    //this.dtRow["City"] = drList["City"] + "";
                    //this.dtRow["ZIP"] = drList["ZIP"] + "";
                    //this.dtRow["Country_Code"] = drList["Country_Code"] + "";
                    //this.dtRow["Country_Title"] = drList["Country_Title"] + "";
                    //this.dtRow["CountryTax_Code"] = drList["CountryTax_Code"] + "";
                    //this.dtRow["CountryTax_Title"] = drList["CountryTax_Title"] + "";
                    this.dtRow["Contract_ID"] = drList["ClientPackage_ID"];
                    this.dtRow["Contract_Details_ID"] = drList["Contract_Details_ID"];
                    this.dtRow["Contract_Packages_ID"] = drList["Contract_Packages_ID"];
                    this.dtRow["MiFIDCategory_ID"] = drList["MiFIDCategory_ID"];
 
                    this.dtRow["StockCompany_ID"] = drList["StockCompany_ID"];
                    this.dtRow["ServiceProvider_Title"] = drList["StockCompanyTitle"] + "";
                    this.dtRow["ServiceProvider_LEI"] = drList["StockCompanyLEI"] + "";

                    this.dtRow["ContractTipos"] = drList["ContractTipos"];

                    //this.dtRow["Recipient"] = drList["Recipient"] + "";
                    */

                    this.dtRow["ProductStockExchange_ID"] = drList["ProductStockExchange_ID"];
                    this.dtRow["ProductStockExchange_MIC"] = drList["ProductStockExchange_MIC"] + "";
                    this.dtRow["ProductStockExchange_Title"] = drList["ProductStockExchange_Title"] + "";
                    this.dtRow["StockExchange_ID"] = drList["StockExchange_ID"];
                    this.dtRow["StockExchange_MIC"] = drList["StockExchange_MIC"] + "";
                    this.dtRow["StockExchange_Title"] = drList["StockExchange_Title"] + "";
                    this.dtRow["ExecutionStockExchange_ID"] = drList["ProductStockExchange_ID"];
                    this.dtRow["ExecutionStockExchange_MIC"] = drList["ProductStockExchange_MIC"] + "";
                    this.dtRow["ExecutionStockExchange_Title"] = drList["ProductStockExchange_Title"] + "";

                    this.dtRow["Aktion"] = drList["Aktion"];
                    this.dtRow["AktionDate"] = Convert.ToDateTime(drList["AktionDate"]).ToString("dd/MM/yyyy");
                    this.dtRow["Product_ID"] = drList["Product_ID"];
                    this.dtRow["Product_Title"] = drList["ProductTitle"] + "";
                    this.dtRow["ProductCategory_ID"] = drList["ProductCategory_ID"];
                    this.dtRow["Product_Category"] = drList["ProductCategory"];
                    this.dtRow["Share_ID"] = drList["Share_ID"];
                    this.dtRow["Share_Code"] = drList["ShareCode"] + "";
                    this.dtRow["Share_Code2"] = drList["ShareCode2"] + "";
                    this.dtRow["Share_Title"] = drList["ShareTitle"] + "";
                    this.dtRow["Share_ISIN"] = drList["ISIN"] + "";
                    this.dtRow["PriceType"] = drList["Type"];
                    this.dtRow["Price"] = drList["Price"];
                    this.dtRow["Quantity"] = drList["Quantity"];
                    this.dtRow["Amount"] = drList["Amount"];
                    this.dtRow["Currency"] = drList["Curr"] + "";
                    this.dtRow["CurrRate"] = drList["CurrRate"];
                    this.dtRow["Constant"] = drList["Constant"];
                    this.dtRow["ConstantDate"] = drList["ConstantDate"];
                    this.dtRow["RealPrice"] = drList["RealPrice"];
                    this.dtRow["RealQuantity"] = drList["RealQuantity"];
                    this.dtRow["RealAmount"] = drList["RealAmount"];
                    this.dtRow["FeesDiff"] = drList["FeesDiff"];
                    this.dtRow["FeesMarket"] = drList["FeesMarket"];
                    if (Convert.ToDateTime(drList["RecieveDate"]) == Convert.ToDateTime("01/01/1900")) this.dtRow["RecieveDate"] = "01/01/1900";
                    else this.dtRow["RecieveDate"] = Convert.ToDateTime(drList["RecieveDate"]).ToString("dd/MM/yy HH:mm:ss");

                    //this.dtRow["RecieveTitle"] = drList["RecieveTitle"];
                    if (Convert.ToDouble(this.dtRow["RealPrice"]) == 0) this.dtRow["ExecuteDate"] = "01/01/1900";
                    else this.dtRow["ExecuteDate"] = Convert.ToDateTime(drList["ExecuteDate"]).ToString("dd/MM/yy HH:mm:ss");

                    //this.dtRow["InformationTitle"] = drList["InformationTitle"];
                    //this.dtRow["OfficialInformingDate"] = drList["OfficialInformingDate"] + "";
                    this.dtRow["Notes"] = drList["Notes"];
                    this.dtRow["Author_Fullname"] = (drList["AuthorSurname"] + " " + drList["AuthorFirstname"]).Trim();
                    //this.dtRow["Advisor_Fullname"] = (drList["AdvisorSurname"] + " " + drList["AdvisorFirstname"]).Trim();
                    //this.dtRow["Diax_Fullname"] = (drList["DiaxSurname"] + " " + drList["DIaxFirstname"]).Trim();
                    //this.dtRow["AdvisorSurname"] = drList["AdvisorSurname"] + "";
                    //this.dtRow["AdvisorFirstname"] = drList["AdvisorFirstname"] + "";
                    //if (this.dtRow["AdvisorSurname"] + "" != "") this.dtRow["AdvisorDoB"] = drList["AdvisorDoB"];
                    //else this.dtRow["AdvisorDoB"] = "";

                    this.dtRow["FeesPercent"] = drList["FeesPercent"];
                    this.dtRow["FeesAmount"] = drList["FeesAmount"];
                    this.dtRow["FeesDiscountPercent"] = drList["FeesDiscountPercent"];
                    this.dtRow["FeesDiscountAmount"] = drList["FeesDiscountAmount"];
                    this.dtRow["FinishFeesPercent"] = drList["FinishFeesPercent"];
                    this.dtRow["FinishFeesAmount"] = drList["FinishFeesAmount"];
                    this.dtRow["FeesCalc"] = drList["FeesCalc"];
                    this.dtRow["ProviderFees"] = drList["ProviderFees"];
                    this.dtRow["FeesMisc"] = drList["FeesMisc"];
                    //this.dtRow["Service_ID"] = drList["Service_ID"];
                    //this.dtRow["ServiceTitle"] = drList["ServiceTitle"];
                    //this.dtRow["II_ID"] = drList["II_ID"];
                    //this.dtRow["Risk"] = drList["Risk"];

                    this.dtRow["Status"] = drList["Status"];
                    this.dtRow["BusinessType_ID"] = drList["BusinessType_ID"];
                    this.dtRow["AccruedInterest"] = drList["AccruedInterest"];
                    //this.dtRow["Depository_Title"] = drList["Depository_Title"];
                    this.dtRow["QuantityMin"] = drList["QuantityMin"];
                    this.dtRow["QuantityStep"] = drList["QuantityStep"];
                    //this.dtRow["ConnectionMethod"] = drList["ConnectionMethod"];
                    //this.dtRow["LastCheckFile"] = drList["LastCheckFile"];
                    //this.dtRow["InvoiceFileName"] = drList["FileName"];
                    this.dtRow["SendCheck"] = drList["SendCheck"];
                    this.dtRow["FIX_A"] = drList["FIX_A"];
                    this.dtRow["FIX_RecievedDate"] = drList["FIX_RecievedDate"];
                    this.dtRow["SentDate"] = drList["SentDate"];
                    this.dtRow["Type"] = drList["Type"];
                    this.dtRow["Parent_ID"] = drList["Parent_ID"];
                    this.dtRow["Company_ID"] = drList["Company_ID"];
                    this.dtRow["Depository_ID"] = drList["Depository_ID"];

                    /*
                    this.dtRow["InvestPolicy_Title"] = drList["InvestPolicy_Title"];
                    this.dtRow["AdvisoryInvestmentPolicy_ID"] = drList["AdvisoryInvestmentPolicy_ID"];
                    this.dtRow["AdvisoryInvestmentPolicy_Title"] = drList["AdvisoryInvestmentPolicy_Title"];
                    this.dtRow["DiscretInvestmentPolicy_ID"] = drList["DiscretInvestmentPolicy_ID"];
                    this.dtRow["DiscretInvestmentPolicy_Title"] = drList["DiscretInvestmentPolicy_Title"];
                    this.dtRow["DealAdvisoryInvestmentPolicy_ID"] = drList["DealAdvisoryInvestmentPolicy_ID"];
                    this.dtRow["DealAdvisoryInvestmentPolicy_Title"] = drList["DealAdvisoryInvestmentPolicy_Title"];
                    this.dtRow["ProductStockExchange_ID"] = drList["ProductStockExchange_ID"];
                    this.dtRow["ClientOrderID"] = drList["ClientOrderID"] + "";
                    this.dtRow["Recomend"] = Convert.ToInt32(drList["HFIC_Recom"]) == 1 ? "Ναί" : "Όχι";
                    */
                    this.dtRow["DateIns"] = drList["DateIns"];
                    this.dtRow["User_ID"] = drList["User_ID"];
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetUnsentList()
        {
            string sTemp = "";
            try
            {
                _dtList = new DataTable("Unsent_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("BulkCommand", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("BusinessType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("CommandType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ClientFullName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Company_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Company_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ServiceProvider_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockExchange_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StockExchange_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProductStockExchange_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ProductStockExchange_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Aktion", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AktionDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ProductCategory_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Share_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Share_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_ISIN", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("PriceType", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Price", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Quantity", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Amount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("QuantityMin", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("QuantityStep", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RecieveDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("SentDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("Constant_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Constant", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ConstantDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DateIns", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("User_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("sp_GetTransactions_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CommandType_ID", "1"));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dDateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dDateTo));
                cmd.Parameters.Add(new SqlParameter("@StockCompany_ID", "0"));
                cmd.Parameters.Add(new SqlParameter("@Sent", "0"));
                cmd.Parameters.Add(new SqlParameter("@Actions", "0"));
                cmd.Parameters.Add(new SqlParameter("@User1_ID", "0"));
                cmd.Parameters.Add(new SqlParameter("@User4_ID", "0"));
                cmd.Parameters.Add(new SqlParameter("@Division_ID", "0"));
                cmd.Parameters.Add(new SqlParameter("@ClientCode", ""));

                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if ( ((drList["BulkCommand"] + "") == "") &&
                         (Convert.ToDateTime(drList["RecieveDate"]).Date != Convert.ToDateTime("1900/01/01")) && 
                         (Convert.ToDateTime(drList["SentDate"]).Date == Convert.ToDateTime("1900/01/01")) &&
                         (Convert.ToDateTime(drList["ExecuteDate"]).Date == Convert.ToDateTime("1900/01/01").Date) && (Convert.ToInt32(drList["Status"]) != -1))
                    {
                        dtRow = _dtList.NewRow();
                        this.dtRow["ID"] = drList["ID"];
                        this.dtRow["BulkCommand"] = drList["BulkCommand"];
                        this.dtRow["BusinessType_ID"] = drList["BusinessType_ID"];
                        this.dtRow["CommandType_ID"] = drList["CommandType_ID"];

                        dtRow["Client_ID"] = drList["Client_ID"];
                        dtRow["ClientFullName"] = "";
                        if (Convert.ToInt32(drList["Client_ID"]) != 0) {
                            if (Convert.ToInt32(drList["Tipos"]) == 1) dtRow["ClientFullName"] = (drList["Surname"] + " " + drList["Firstname"]).Trim();
                            else dtRow["ClientFullName"] = drList["Surname"] + "";
                        }

                        this.dtRow["Company_Title"] = drList["Company_Title"] + "";
                        this.dtRow["ContractTitle"] = drList["ContractTitle"];
                        this.dtRow["ServiceProvider_ID"] = drList["StockCompany_ID"];
                        this.dtRow["ServiceProvider_Title"] = drList["StockCompanyTitle"] + "";
                        if (Global.IsNumeric(drList["StockExchange_ID"])) {
                            this.dtRow["StockExchange_ID"] = drList["StockExchange_ID"];
                            this.dtRow["StockExchange_Code"] = drList["StockExchanges_MIC"] + "";
                        }
                        else {
                            this.dtRow["StockExchange_ID"] = 0;
                            this.dtRow["StockExchange_Code"] = "";
                        }
                        if (Global.IsNumeric(drList["ProductStockExchanges_ID"])) {
                            this.dtRow["ProductStockExchange_ID"] = drList["ProductStockExchanges_ID"];
                            this.dtRow["ProductStockExchange_Code"] = drList["ProductStockExchanges_MIC"] + "";
                        }
                        else  {
                            this.dtRow["ProductStockExchange_ID"] = 0;
                            this.dtRow["ProductStockExchange_Code"] =  "";
                        }
                        this.dtRow["Code"] = drList["Code"] + "";
                        this.dtRow["Portfolio"] = drList["ProfitCenter"] + "";
                        this.dtRow["Aktion"] = (Convert.ToInt32(drList["Aktion"]) == 1 ? "BUY" : "SELL");
                        this.dtRow["AktionDate"] = Convert.ToDateTime(drList["AktionDate"]).ToString("dd/MM/yyyy");
                        this.dtRow["Product_ID"] = drList["Product_ID"];
                        this.dtRow["ProductCategory_ID"] = drList["ProductCategory_ID"];
                        this.dtRow["Share_ID"] = drList["Share_ID"];
                        this.dtRow["Share_Code"] = drList["ShareCode"] + "";
                        this.dtRow["Share_Title"] = drList["ShareTitle"] + "";
                        this.dtRow["Share_ISIN"] = drList["ISIN"] + "";
                        this.dtRow["PriceType"] = drList["Type"];
                        this.dtRow["Price"] = Global.ShowPrices(Convert.ToInt32(drList["Type"]), Convert.ToSingle(drList["Price"]));
                        this.dtRow["Quantity"] = drList["Quantity"];
                        this.dtRow["Amount"] = drList["Amount"];
                        this.dtRow["Currency"] = drList["Curr"] + "";
                        this.dtRow["QuantityMin"] = drList["QuantityMin"];
                        this.dtRow["QuantityStep"] = drList["QuantityStep"];
                        if (Convert.ToDateTime(drList["RecieveDate"]) == Convert.ToDateTime("01/01/1900")) this.dtRow["RecieveDate"] = "01/01/1900";
                        else this.dtRow["RecieveDate"] = Convert.ToDateTime(drList["RecieveDate"]).ToString("dd/MM/yy HH:mm:ss");
                        this.dtRow["SentDate"] = drList["SentDate"];

                        sTemp = "";
                        switch (Convert.ToInt32(drList["Constant"])) { 
                            case 0:
                               sTemp = "Day Order";
                               break;
                            case 1:
                               sTemp = "GTC";
                               break;
                            case 2:
                               sTemp = "GTDate";
                               break;
                        }
                        dtRow["Constant_ID"] = Convert.ToInt32(drList["Constant"]);
                        dtRow["Constant"] = sTemp;
                        dtRow["ConstantDate"] = drList["ConstantDate"] + "";
                        this.dtRow["DateIns"] = drList["DateIns"];
                        this.dtRow["User_ID"] = drList["User_ID"];                

                        _dtList.Rows.Add(dtRow);
                    }
                }
                drList.Close();

                cmd = new SqlCommand("GetDMPOrders_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CommandType_ID", "4"));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dDateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dDateTo));
                cmd.Parameters.Add(new SqlParameter("@StockCompany_ID", "0"));
                cmd.Parameters.Add(new SqlParameter("@User_ID", "0"));
                cmd.Parameters.Add(new SqlParameter("@Sent", "0"));
                cmd.Parameters.Add(new SqlParameter("@Actions", "0"));

                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if (  (Convert.ToDateTime(drList["SentDate"]).Date == Convert.ToDateTime("1900/01/01")) && 
                          (Convert.ToDateTime(drList["ExecuteDate"]).Date == Convert.ToDateTime("1900/01/01")) && (Convert.ToInt32(drList["Status"]) != -1))
                    {
                        dtRow = _dtList.NewRow();
                        this.dtRow["ID"] = drList["ID"];
                        this.dtRow["BulkCommand"] = drList["BulkCommand"];
                        this.dtRow["BusinessType_ID"] = drList["BusinessType_ID"];
                        this.dtRow["CommandType_ID"] = drList["CommandType_ID"];
                        this.dtRow["Company_Title"] = (drList["AdvisorSurname"] + " " + drList["AdvisorFirstname"]).Trim(); 
                        this.dtRow["Client_ID"] = drList["Client_ID"];
                        if (Convert.ToInt32(drList["Client_ID"]) == 0)
                            this.dtRow["ClientFullName"] = (drList["DiaxSurname"] + " " + drList["DiaxFirstname"]).Trim();
                        else
                            this.dtRow["ClientFullName"] = (drList["Surname"] + " " + drList["Firstname"]).Trim();
                        this.dtRow["ServiceProvider_ID"] = drList["StockCompany_ID"];
                        this.dtRow["ServiceProvider_Title"] = drList["StockCompanyTitle"];

                        if (Global.IsNumeric(drList["StockExchange_ID"]))
                        {
                            this.dtRow["StockExchange_ID"] = drList["StockExchange_ID"];
                            this.dtRow["StockExchange_Code"] = drList["StockExchanges_MIC"] + "";
                        }
                        else
                        {
                            this.dtRow["StockExchange_ID"] = 0;
                            this.dtRow["StockExchange_Code"] = "";
                        }
                        if (Global.IsNumeric(drList["ProductStockExchange_ID"]))
                        {
                            this.dtRow["ProductStockExchange_ID"] = drList["ProductStockExchange_ID"];
                            this.dtRow["ProductStockExchange_Code"] = drList["ProductStockExchange_MIC"] + "";
                        }
                        else
                        {
                            this.dtRow["ProductStockExchange_ID"] = 0;
                            this.dtRow["ProductStockExchange_Code"] = "";
                        }

                        this.dtRow["ContractTitle"] = drList["ContractTitle"];
                        this.dtRow["Code"] = drList["Code"];
                        this.dtRow["Portfolio"] = drList["SubCode"];
                        this.dtRow["Aktion"] = (Convert.ToInt32(drList["Aktion"]) == 1 ? "BUY" : "SELL");
                        this.dtRow["AktionDate"] = Convert.ToDateTime(drList["AktionDate"]).ToString("dd/MM/yyyy");
                        this.dtRow["Product_ID"] = drList["Product_ID"];
                        this.dtRow["ProductCategory_ID"] = drList["ProductCategory_ID"];
                        this.dtRow["Share_ID"] = drList["Share_ID"];
                        this.dtRow["Share_Code"] = drList["ShareCode"];
                        this.dtRow["Share_Title"] = drList["ShareTitle"];
                        this.dtRow["Share_ISIN"] = drList["ISIN"];
                        this.dtRow["PriceType"] = drList["Type"];
                        this.dtRow["Price"] = Global.ShowPrices(Convert.ToInt32(drList["Type"]), Convert.ToSingle(drList["Price"]));
                        this.dtRow["Quantity"] = drList["Quantity"];
                        this.dtRow["Amount"] = drList["Amount"];
                        this.dtRow["Currency"] = drList["Curr"];
                        this.dtRow["QuantityMin"] = drList["QuantityMin"];
                        this.dtRow["QuantityStep"] = drList["QuantityStep"];
                        this.dtRow["RecieveDate"] = drList["RecieveDate"];
                        this.dtRow["SentDate"] = drList["SentDate"];
                        sTemp = "";
                        switch (Convert.ToInt32(drList["Constant"])) {
                            case 0:
                                sTemp = "Day Order";
                                break;
                            case 1:
                                sTemp = "GTC";
                                break;
                            case 2:
                                sTemp = "GTDate ";
                                break;
                        }
                        this.dtRow["Constant"] = sTemp;
                        this.dtRow["Constant_ID"] = Convert.ToInt32(drList["Constant"]);                        
                        this.dtRow["ConstantDate"] = drList["ConstantDate"] + "";
                        this.dtRow["DateIns"] = drList["DateIns"];
                        this.dtRow["User_ID"] = drList["User_ID"];
                        _dtList.Rows.Add(dtRow);
                    }
                }
                drList.Close();
            }
            catch (Exception ex) { 
                MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int GetBulkCommand_Parent()
        {
            _iCommandType_ID = 0;
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetCommands_BulkCommand", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AktionDate", _dAktionDate));
                cmd.Parameters.Add(new SqlParameter("@BulkCommand", _sBulkCommand));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _iCommandType_ID = Convert.ToInt32(drList["CommandType_ID"]);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return _iCommandType_ID;
        }
        public void GetExecutedCommands()
        {
            try
            {
                _dtList = new DataTable("Commands_Execution_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ClientOrder_ID", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("BulkCommand", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CommandType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Aktion", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("AktionDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Provider_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ClientFullName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Share_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Share_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ISIN", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ExecuteDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RealQuantity", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("RealPrice", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("RealAmount", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("FeesDiff", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("FeesMarket", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("AccruedInterest", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("Commission", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SE_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SE_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Depository_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int16"));

                conn.Open();
                cmd = new SqlCommand("GetCommands_Execution", conn);                                 // was sp_GetCommands_Execution
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ServiceProvider_ID", _iServiceProvider_ID));
                cmd.Parameters.Add(new SqlParameter("@ExecuteDateFrom", _dDateFrom));
                cmd.Parameters.Add(new SqlParameter("@ExecuteDateTo", _dDateTo));

                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["ClientOrder_ID"] = drList["ProviderCommandNumber"];
                    this.dtRow["BulkCommand"] = drList["BulkCommand"];
                    this.dtRow["CommandType_ID"] = drList["CommandType_ID"];
                    this.dtRow["Aktion"] = drList["Aktion"];
                    this.dtRow["AktionDate"] = drList["AktionDate"];
                    this.dtRow["Client_ID"] = drList["Client_ID"];
                    this.dtRow["Contract_ID"] = drList["Contract_ID"];
                    this.dtRow["Provider_ID"] = drList["StockCompany_ID"];
                    this.dtRow["ClientFullName"] = (drList["Surname"] + " " + drList["FirstName"]).Trim();
                    this.dtRow["ContractTitle"] = drList["ContractTitle"];
                    this.dtRow["Code"] = drList["Code"];
                    this.dtRow["Portfolio"] = drList["Portfolio"];
                    this.dtRow["Product_ID"] = drList["Product_ID"];
                    this.dtRow["Share_ID"] = drList["Share_ID"];
                    this.dtRow["Share_Title"] = drList["Share_Title"];
                    this.dtRow["Share_Code"] = drList["Share_Code"];
                    this.dtRow["ISIN"] = drList["ISIN"];
                    this.dtRow["Currency"] = drList["Curr"];
                    this.dtRow["ExecuteDate"] = drList["ExecuteDate"];
                    this.dtRow["RealQuantity"] = drList["RealQuantity"];
                    this.dtRow["RealPrice"] = drList["RealPrice"];
                    this.dtRow["RealAmount"] = drList["RealAmount"];
                    this.dtRow["FeesDiff"] = drList["FeesDiff"];
                    this.dtRow["FeesMarket"] = drList["FeesMarket"];
                    this.dtRow["AccruedInterest"] = drList["AccruedInterest"];
                    this.dtRow["Commission"] = drList["Commission"];
                    this.dtRow["SE_Code"] = drList["SE_Code"];
                    this.dtRow["SE_ID"] = drList["StockExchange_ID"];
                    this.dtRow["Depository_Code"] = drList["Depository_Code"];
                    this.dtRow["Notes"] = drList["Notes"];
                    this.dtRow["Status"] = drList["Status"];
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
                cmd = new SqlCommand("sp_GetCommands_Next_BulkCommand_ID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add(new SqlParameter("@BulkCommand", _sBulkCommand));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    iLastBulkCommand = Convert.ToInt32(drList["LastBulkCommand_ID"]);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return iLastBulkCommand;
        }
        public void GetPinakidia()
        {
            try
            {
                _dtList = new DataTable("Orders_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Type", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ClientType", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("ClientFullName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockCompany_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StockCompanyTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Contract_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Details_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Packages_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Aktion", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Product_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Product_Category", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Share_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_ISIN", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("PriceType", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Price", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Quantity", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RealPrice", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RealQuantity", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RealAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FeesNotes", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Pinakidio", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("ProblemType_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("CheckProblem_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Check_Notes", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Check_FileName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ReversalRequestDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RecieveDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("RecieveTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SentDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("InformationTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ExecuteDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Author_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Advisor_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishFeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishFeesAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ServiceTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockExchange_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StockExchange_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Parent_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Commands_Check_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("CommandType_ID", System.Type.GetType("System.Int32"));                

                conn.Open();
                cmd = new SqlCommand("GetCommands_Pinakidia", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dDateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dDateTo));
                cmd.Parameters.Add(new SqlParameter("@ExecDateFrom", _dExecDateFrom));
                cmd.Parameters.Add(new SqlParameter("@ExecDateTo", _dExecDateTo));
                cmd.Parameters.Add(new SqlParameter("@StockCompany_ID", _iServiceProvider_ID));
                cmd.Parameters.Add(new SqlParameter("@Status", _iStatus));
                cmd.Parameters.Add(new SqlParameter("@Client_ID", _iClient_ID));
                cmd.Parameters.Add(new SqlParameter("@Product_ID", _iProduct_ID));
                cmd.Parameters.Add(new SqlParameter("@Share_ID", _iShare_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["Type"] = drList["Type"];                        // Commands.Type
                    this.dtRow["Client_ID"] = drList["Client_ID"];
                    this.dtRow["ClientType"] = drList["Tipos"];                    
                    this.dtRow["ClientFullName"] = "";
                    switch (Convert.ToInt32(drList["CommandType_ID"]))
                    {
                        case 1:
                            if (Convert.ToInt32(drList["Tipos"]) == 1) this.dtRow["ClientFullName"] = (drList["Surname"] + " " + drList["Firstname"]).Trim();
                            else this.dtRow["ClientFullName"] = drList["Surname"] + "";
                            break;
                        case 2:
                            this.dtRow["ClientFullName"] = drList["Company_Title"] + "";
                            break;
                    }

                    //this.dtRow["Company_Title"] = drList["Company_Title"] + "";
                    this.dtRow["StockCompany_ID"] = drList["StockCompany_ID"];
                    this.dtRow["StockCompanyTitle"] = drList["StockCompanyTitle"] + "";
                    this.dtRow["Contract_ID"] = drList["ClientPackage_ID"];
                    this.dtRow["Contract_Details_ID"] = drList["Contract_Details_ID"];
                    this.dtRow["Contract_Packages_ID"] = drList["Contract_Packages_ID"];
                    this.dtRow["ContractTitle"] = drList["ContractTitle"] + "";
                    this.dtRow["Code"] = drList["Code"] + "";
                    this.dtRow["Portfolio"] = drList["ProfitCenter"] + "";
                    this.dtRow["Aktion"] = drList["Aktion"];
                    this.dtRow["Product_ID"] = drList["Product_ID"];
                    this.dtRow["Product_Title"] = drList["ProductTitle"] + "";
                    this.dtRow["Product_Category"] = drList["ProductCategory"];
                    this.dtRow["Share_ID"] = drList["Share_ID"];
                    this.dtRow["Share_Code"] = drList["ShareCode"] + "";
                    this.dtRow["Share_Title"] = drList["ShareTitle"] + "";
                    this.dtRow["Share_ISIN"] = drList["ISIN"] + "";
                    this.dtRow["PriceType"] = drList["Type"];
                    this.dtRow["Price"] = drList["Price"];
                    this.dtRow["Quantity"] = drList["Quantity"];
                    this.dtRow["RealPrice"] = drList["RealPrice"];
                    this.dtRow["RealQuantity"] = drList["RealQuantity"];
                    this.dtRow["RealAmount"] = drList["RealAmount"];
                    this.dtRow["Currency"] = drList["Curr"] + "";
                    this.dtRow["FeesNotes"] = drList["FeesNotes"];
                    this.dtRow["Pinakidio"] = drList["Pinakidio"];
                    this.dtRow["ProblemType_ID"] = drList["ProblemType_ID"];                    
                    this.dtRow["CheckProblem_Title"] = drList["CheckProblem_Title"] + "";
                    this.dtRow["Check_Notes"] = drList["Check_Notes"] + "";
                    this.dtRow["Check_FileName"] = drList["Check_FileName"] + "";
                    this.dtRow["ReversalRequestDate"] = drList["ReversalRequestDate"];
                    if (Convert.ToDateTime(drList["RecieveDate"]) == Convert.ToDateTime("01/01/1900")) this.dtRow["RecieveDate"] = "01/01/1900";
                    else this.dtRow["RecieveDate"] = Convert.ToDateTime(drList["RecieveDate"]).ToString("dd/MM/yy HH:mm:ss");
                    this.dtRow["RecieveTitle"] = drList["RecieveTitle"];
                    this.dtRow["InformationTitle"] = drList["InformationTitle"];
                    if (Convert.ToDouble(this.dtRow["RealPrice"]) == 0) this.dtRow["ExecuteDate"] = "01/01/1900";
                    else this.dtRow["ExecuteDate"] = Convert.ToDateTime(drList["ExecuteDate"]).ToString("dd/MM/yy HH:mm:ss");
                    this.dtRow["SentDate"] = drList["SentDate"];
                    this.dtRow["Notes"] = drList["Notes"];
                    this.dtRow["Author_Fullname"] = (drList["AuthorSurname"] + " " + drList["AuthorFirstname"]).Trim();
                    this.dtRow["Advisor_Fullname"] = (drList["AdvisorSurname"] + " " + drList["AdvisorFirstname"]).Trim();
                    this.dtRow["FeesPercent"] = drList["FeesPercent"];
                    this.dtRow["FeesAmount"] = drList["FeesAmount"];
                    this.dtRow["FinishFeesPercent"] = drList["FinishFeesPercent"];
                    this.dtRow["FinishFeesAmount"] = drList["FinishFeesAmount"];
                    this.dtRow["ServiceTitle"] = drList["ServiceTitle"];
                    this.dtRow["StockExchange_ID"] = drList["StockExchange_ID"];
                    this.dtRow["StockExchange_Title"] = drList["StockExchangeTitle"] + "";
                    this.dtRow["Status"] = drList["Status"];
                    this.dtRow["Parent_ID"] = drList["Parent_ID"];
                    this.dtRow["Commands_Check_ID"] = drList["Commands_Check_ID"];
                    this.dtRow["CommandType_ID"] = drList["CommandType_ID"];                   
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
                _dtList = new DataTable("CommandsInformingsList");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DateIns", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InformationMethod", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FileName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DateSent", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InformMethod", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("User_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("sp_GetCommands_Informings", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Command_Type", 1));
                cmd.Parameters.Add(new SqlParameter("@Command_ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["DateIns"] = Convert.ToDateTime(drList["DateIns"]).ToString("dd/MM/yyyy");
                    this.dtRow["InformationMethod"] = drList["InformationMethod"] + "";
                    this.dtRow["FileName"] = drList["FileName"] + "";
                    this.dtRow["DateSent"] = Convert.ToDateTime(drList["DateSent"]);
                    this.dtRow["InformMethod"] = Convert.ToInt32(drList["InformMethod"]);
                    this.dtRow["User_ID"] = Convert.ToInt32(drList["User_ID"]);
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetChecks()
        {
            try
            {
                _dtList = new DataTable("CommandsInformingsList");
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
                cmd = new SqlCommand("sp_GetCommands_Check", conn);
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
        public void GetRecievedFiles()
        {
            try
            {
                _dtList = new DataTable("CommandsRecievedFilesList");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DateIns", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Method_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FileName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Method_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("sp_GetCommandsRecieved", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["DateIns"] = Convert.ToDateTime(drList["DateIns"]).ToString("dd/MM/yyyy HH:mm:ss");
                    this.dtRow["Method_Title"] = drList["Method_Title"] + "";
                    this.dtRow["FileName"] = drList["FileName"] + "";
                    this.dtRow["Method_ID"] = Convert.ToInt32(drList["Method_ID"]);
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetTRSList()
        {
            try
            {
                _dtList = new DataTable("TRSList");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("BulkCommand", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CommandType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("BusinessType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Tipos", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ClientType", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("ClientSurnameEng", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientFirstnameEng", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientDoB", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientLEI", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SurnameFather", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("EMail", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Address", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("City", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ZIP", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Country_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CountryTax_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientPackage_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_LEI", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ContractTipos", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Aktion", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Share_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Share_ISIN", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ExecuteDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("RealPrice", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RealQuantity", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RealAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AdvisorSurname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AdvisorFirstname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AdvisorDoB", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DiaxSurname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DiaxFirstname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DiaxDoB", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Service_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("StockExchanges_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StockExchanges_MIC", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("UserSurname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("UserFirstname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("UserDoB", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("UserCountryCode", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Result", System.Type.GetType("System.Int16"));

                conn.Open();
                cmd = new SqlCommand("GetSecurities_TRSList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dDateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dDateTo));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    //if (Convert.ToInt32(drList["ID"]) == 504213)
                    //    _iII_ID = _iII_ID;
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["BulkCommand"] = drList["BulkCommand"] + "";
                    dtRow["CommandType_ID"] = drList["CommandType_ID"];
                    dtRow["BusinessType_ID"] = drList["BusinessType_ID"];
                    dtRow["Tipos"] = drList["Tipos"];
                    dtRow["Client_ID"] = drList["Client_ID"];
                    if (drList["Tipos"] + "" == "")
                    {
                        dtRow["ClientType"] = 0;
                        dtRow["ClientSurnameEng"] = "";
                        dtRow["ClientFirstnameEng"] = "";
                        dtRow["ClientDoB"] = "";
                        dtRow["Country_Code"] = "";
                        dtRow["CountryTax_Code"] = "";
                        dtRow["ClientPackage_ID"] = 0;
                        dtRow["ClientLEI"] = "";
                    }
                    else
                    {
                        dtRow["ClientType"] = drList["Tipos"];
                        dtRow["ClientSurnameEng"] = drList["SurnameEng"] + "";
                        dtRow["ClientFirstnameEng"] = drList["FirstnameEng"] + "";
                        dtRow["ClientDoB"] = Convert.ToDateTime(drList["DoB"]).ToString("yyyyMMdd");
                        dtRow["Country_Code"] = drList["Country_Code"] + "";
                        dtRow["CountryTax_Code"] = drList["CountryTax_Code"] + "";
                        dtRow["ClientPackage_ID"] = drList["ClientPackage_ID"];
                        dtRow["ClientLEI"] = "";
                        if (Convert.ToInt32(drList["CommandType_ID"]) == 1)
                            if (Convert.ToInt32(drList["Tipos"]) != 1)
                                dtRow["ClientLEI"] = drList["FirstnameSizigo"] + "";
                    }

                    dtRow["ServiceProvider_ID"] = drList["StockCompany_ID"];
                    dtRow["ServiceProvider_LEI"] = drList["StockCompanyLEI"] + "";
                    dtRow["ContractTipos"] = drList["ContractTipos"];
                    dtRow["Aktion"] = drList["Aktion"];
                    dtRow["Product_ID"] = drList["Product_ID"];
                    dtRow["Share_ID"] = drList["Share_ID"];
                    dtRow["Share_ISIN"] = drList["ISIN"] + "";
                    dtRow["Currency"] = drList["Curr"] + "";
                    dtRow["RealPrice"] = drList["RealPrice"];
                    dtRow["RealQuantity"] = drList["RealQuantity"];
                    dtRow["RealAmount"] = drList["RealAmount"];
                    if (Convert.ToDecimal(dtRow["RealPrice"]) == 0) dtRow["ExecuteDate"] = "01/01/1900";
                    else dtRow["ExecuteDate"] = Convert.ToDateTime(drList["ExecuteDate"]).ToString("dd/MM/yyyy HH:mm:ss");

                    dtRow["Notes"] = drList["Notes"] + "";

                    dtRow["AdvisorSurname"] = drList["AdvisorSurname"] + "";
                    dtRow["AdvisorFirstname"] = drList["AdvisorFirstname"] + "";
                    if (dtRow["AdvisorSurname"] + "" != "") dtRow["AdvisorDoB"] = Convert.ToDateTime(drList["AdvisorDoB"]).ToString("yyyyMMdd");
                    else dtRow["AdvisorDoB"] = "";

                    dtRow["DiaxSurname"] = drList["DiaxSurname"] + "";
                    dtRow["DiaxFirstname"] = drList["DiaxFirstname"] + "";
                    if (dtRow["DiaxSurname"] + "" != "") dtRow["DiaxDoB"] = Convert.ToDateTime(drList["DiaxDoB"]).ToString("yyyyMMdd");
                    else dtRow["DiaxDoB"] = "";

                    dtRow["Service_ID"] = drList["Service_ID"];
                    dtRow["StockExchanges_ID"] = drList["StockExchange_ID"];
                    dtRow["StockExchanges_MIC"] = drList["StockExchanges_MIC"] + "";

                    dtRow["UserSurname"] = drList["UserSurname"] + "";
                    dtRow["UserFirstname"] = drList["UserFirstname"] + "";
                    dtRow["UserDoB"] = Convert.ToDateTime(drList["UserDoB"]).ToString("yyyyMMdd");
                    dtRow["UserCountryCode"] = drList["UserCountryCode"] + "";

                    dtRow["Result"] = 0;

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
                _dtList = new DataTable("OrdersList");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("BulkCommand", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("BusinessType_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("CommandType_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Parent_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Company_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StockExchange_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("CustodyProvider_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("II_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Details_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Packages_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AllocationPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AktionDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Aktion", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Share_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ProductCategory_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("PriceType", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Price", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Quantity", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Amount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Curr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RealPrice", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RealQuantity", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RealAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Constant", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ConstantDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("RecieveDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RecieveMethod_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("SendOrders", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("SentDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("SendCheck", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DateIns", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("User_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("BestExecution", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("FIX_A", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("FIX_RecievedDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("TransferFlag", System.Type.GetType("System.Int16"));

                conn.Open();
                cmd = new SqlCommand("GetCommands_ConstantNonContinue", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AktionDate", _dDateFrom));
                cmd.Parameters.Add(new SqlParameter("@User_ID", _iUser_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["BulkCommand"] = drList["BulkCommand"];
                    this.dtRow["BusinessType_ID"] = drList["BusinessType_ID"];
                    this.dtRow["CommandType_ID"] = drList["CommandType_ID"];
                    this.dtRow["Parent_ID"] = drList["Parent_ID"];
                    this.dtRow["Client_ID"] = drList["Client_ID"];
                    this.dtRow["Company_ID"] = drList["Company_ID"];
                    this.dtRow["ServiceProvider_ID"] = drList["StockCompany_ID"];
                    this.dtRow["StockExchange_ID"] = drList["StockExchange_ID"];
                    this.dtRow["CustodyProvider_ID"] = drList["CustodyProvider_ID"];
                    this.dtRow["II_ID"] = drList["II_ID"];
                    this.dtRow["Contract_ID"] = drList["ClientPackage_ID"];
                    this.dtRow["Contract_Details_ID"] = drList["Contract_Details_ID"];
                    this.dtRow["Contract_Packages_ID"] = drList["Contract_Packages_ID"];
                    this.dtRow["Code"] = drList["Code"];
                    this.dtRow["Portfolio"] = this.drList["ProfitCenter"] + "";
                    this.dtRow["AllocationPercent"] = drList["AllocationPercent"];
                    this.dtRow["Aktion"] = drList["Aktion"];
                    this.dtRow["AktionDate"] = drList["AktionDate"];
                    this.dtRow["Share_ID"] = drList["Share_ID"];
                    this.dtRow["Product_ID"] = drList["Product_ID"];
                    this.dtRow["ProductCategory_ID"] = drList["ProductCategory_ID"];
                    this.dtRow["PriceType"] = drList["Type"];
                    this.dtRow["Price"] = drList["Price"];
                    this.dtRow["Quantity"] = drList["Quantity"];
                    this.dtRow["Amount"] = drList["Amount"];
                    this.dtRow["Curr"] = drList["Curr"];
                    this.dtRow["RealQuantity"] = drList["RealQuantity"];
                    this.dtRow["RealPrice"] = drList["RealPrice"];
                    this.dtRow["RealAmount"] = drList["RealAmount"];
                    this.dtRow["Constant"] = drList["Constant"];
                    if (drList["ConstantDate"]+"" != "") this.dtRow["ConstantDate"] = Convert.ToDateTime(drList["ConstantDate"]);
                    else                                 this.dtRow["ConstantDate"] = Convert.ToDateTime("1900/01/01");
                    this.dtRow["RecieveDate"] = drList["RecieveDate"];
                    this.dtRow["RecieveMethod_ID"] = drList["RecieveMethod_ID"];
                    this.dtRow["SendOrders"] = drList["SendOrders"];
                    this.dtRow["SentDate"] = drList["SentDate"];
                    this.dtRow["SendCheck"] = drList["SendCheck"];
                    this.dtRow["BestExecution"] = drList["BestExecution"];
                    this.dtRow["FIX_A"] = drList["FIX_A"];
                    this.dtRow["FIX_RecievedDate"] = drList["FIX_RecievedDate"];
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
        public void GetHistory()
        {
            try
            {
                _dtList = new DataTable("CommandsHistoryList");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DateIns", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AuthorName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Description", System.Type.GetType("System.String"));

                conn.Open();
                cmd = new SqlCommand("sp_GetCommands_History", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Command_ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["DateIns"] = Convert.ToDateTime(drList["DateIns"]).ToString("dd/MM/yyyy HH: mm : ss");
                    this.dtRow["AuthorName"] = (drList["Surname"] + " " + drList["Firstname"]).Trim();
                    this.dtRow["Description"] = drList["Description"] + "";
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList_Package_ID()
        {
            try
            {
                _dtList = new DataTable("AdvisoryFees_List");
                dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Packages_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AmountFrom", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmountTo", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AdvisoryFees", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("DiscountDateFrom", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("DiscountDateTo", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("AdvisoryFees_Discount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishAdvisoryFees", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MonthMinAmount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MonthMinCurr", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("MinimumFees_Discount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MinimumFees", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AllManFees", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SPAF_ID", Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetAdvisoryFees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Contract_Packages_ID", _iContract_Packages_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if (!String.IsNullOrEmpty(drList["ID"].ToString()))                                     // it's ClientsAdvisoryFees.ID
                    {
                        if (true)
                        {
                            dtRow = _dtList.NewRow();
                            this.dtRow["ServiceProvider_ID"] = drList["PackageProvider_ID"];
                            this.dtRow["AmountFrom"] = drList["AmountFrom"];
                            this.dtRow["AmountTo"] = drList["AmountTo"];
                            this.dtRow["AdvisoryFees"] = drList["FeesPercent"];
                            this.dtRow["ID"] = drList["ID"];
                            this.dtRow["Contract_ID"] = drList["Contract_ID"];
                            this.dtRow["Contract_Packages_ID"] = drList["Contract_Packages_ID"];
                            this.dtRow["DiscountDateFrom"] = drList["DateFrom"];
                            this.dtRow["DiscountDateTo"] = drList["DateTo"];
                            this.dtRow["AdvisoryFees_Discount"] = drList["AdvisoryFees_Discount"];
                            this.dtRow["FinishAdvisoryFees"] = drList["AdvisoryFees"];
                            this.dtRow["MonthMinAmount"] = drList["MonthMinAmount"];
                            this.dtRow["MonthMinCurr"] = drList["MonthMinCurr"];
                            this.dtRow["MinimumFees_Discount"] = drList["MinimumFees_Discount"];
                            this.dtRow["MinimumFees"] = drList["MinimumFees"];
                            this.dtRow["AllManFees"] = drList["AllManFees"] + "";
                            this.dtRow["SPAF_ID"] = drList["SPAF_ID"];
                            _dtList.Rows.Add(dtRow);
                        }
                        /*
                        else
                        {
                            dtRow = _dtList.NewRow();
                            this.dtRow["ServiceProvider_ID"] = drList["PackageProvider_ID"];
                            this.dtRow["AmountFrom"] = drList["AmountFrom"];
                            this.dtRow["AmountTo"] = drList["AmountTo"];
                            this.dtRow["AdvisoryFees"] = drList["FeesPercent"];
                            this.dtRow["ID"] = drList["ID"];
                            this.dtRow["Contract_ID"] = drList["Contract_ID"];
                            this.dtRow["Contract_Packages_ID"] = drList["Contract_Packages_ID"];
   
                            this.dtRow["FinishAdvisoryFees"] = drList["FeesPercent"];
                            this.dtRow["MonthMinAmount"] = drList["MonthMinAmount"];
                            this.dtRow["MonthMinCurr"] = drList["MonthMinCurr"];
                            this.dtRow["MinimumFees_Discount"] = drList["MinimumFees_Discount"];
                            this.dtRow["MinimumFees"] = drList["MinimumFees"];
                            this.dtRow["AllManFees"] = drList["AllManFees"] + "";
                            this.dtRow["SPAF_ID"] = drList["SPAF_ID"];
                            _dtList.Rows.Add(dtRow);
                        }
                        */
                    }
                    else
                    {
                        dtRow = _dtList.NewRow();
                        this.dtRow["ServiceProvider_ID"] = drList["PackageProvider_ID"];
                        this.dtRow["AmountFrom"] = drList["AmountFrom"];
                        this.dtRow["AmountTo"] = drList["AmountTo"];
                        this.dtRow["AdvisoryFees"] = drList["FeesPercent"];
       
                        this.dtRow["FinishAdvisoryFees"] = drList["FeesPercent"];
                        this.dtRow["MonthMinAmount"] = 0;
                        this.dtRow["MonthMinCurr"] = "EUR";
                        this.dtRow["MinimumFees_Discount"] = 0;
                        this.dtRow["MinimumFees"] = 0;
                        this.dtRow["AllManFees"] = drList["FeesPercent"] + "";
                        this.dtRow["SPAF_ID"] = drList["SPAF_ID"];
                        _dtList.Rows.Add(dtRow);
                    }
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
                using (SqlCommand cmd = new SqlCommand("InsertCommand", conn))
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
                    cmd.Parameters.Add("@StockCompany_ID", SqlDbType.Int).Value = _iServiceProvider_ID;
                    cmd.Parameters.Add("@Executor_ID", SqlDbType.Int).Value = _iExecutor_ID;
                    cmd.Parameters.Add("@StockExchange_ID", SqlDbType.Int).Value = _iStockExchange_ID;
                    cmd.Parameters.Add("@CustodyProvider_ID", SqlDbType.Int).Value = _iCustodyProvider_ID;
                    cmd.Parameters.Add("@Depository_ID", SqlDbType.Int).Value = _iDepository_ID;
                    cmd.Parameters.Add("@II_ID", SqlDbType.Int).Value = _iII_ID;
                    cmd.Parameters.Add("@Parent_ID", SqlDbType.Int).Value = _iParent_ID;
                    cmd.Parameters.Add("@ClientPackage_ID", SqlDbType.Int).Value = _iContract_ID;                               //@@@@@@ ClientPackage_ID -> Contract_ID
                    cmd.Parameters.Add("@Contract_Details_ID", SqlDbType.Int).Value = _iContract_Details_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 30).Value = _sCode;
                    cmd.Parameters.Add("@ProfitCenter", SqlDbType.NVarChar, 50).Value = _sProfitCenter;
                    cmd.Parameters.Add("@AllocationPercent", SqlDbType.Float).Value = _fltAllocationPercent;
                    cmd.Parameters.Add("@Aktion", SqlDbType.Int).Value = _iAktion;
                    cmd.Parameters.Add("@AktionDate", SqlDbType.DateTime).Value = _dAktionDate;
                    cmd.Parameters.Add("@Share_ID", SqlDbType.Int).Value = _iShare_ID;
                    cmd.Parameters.Add("@Product_ID", SqlDbType.Int).Value = _iProduct_ID;
                    cmd.Parameters.Add("@ProductCategory_ID", SqlDbType.Int).Value = _iProductCategory_ID;
                    cmd.Parameters.Add("@Type", SqlDbType.Int).Value = _iPriceType;
                    cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = _decPrice;
                    cmd.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = _decQuantity;
                    cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = _decAmount;
                    cmd.Parameters.Add("@Curr", SqlDbType.NVarChar, 6).Value = _sCurr;
                    cmd.Parameters.Add("@Constant", SqlDbType.Int).Value = _iConstant;
                    cmd.Parameters.Add("@ConstantDate", SqlDbType.NVarChar, 25).Value = _sConstantDate;
                    cmd.Parameters.Add("@ConstantContinue", SqlDbType.Int).Value = _iConstantContinue;
                    cmd.Parameters.Add("@RecieveDate", SqlDbType.DateTime).Value = _dRecieveDate;
                    cmd.Parameters.Add("@RecieveMethod_ID", SqlDbType.Int).Value = _iRecieveMethod_ID;
                    cmd.Parameters.Add("@BestExecution", SqlDbType.Int).Value = _iBestExecution;
                    cmd.Parameters.Add("@SentDate", SqlDbType.DateTime).Value = _dSentDate;
                    cmd.Parameters.Add("@SendCheck", SqlDbType.Int).Value = _iSendCheck;
                    cmd.Parameters.Add("@FIX_A", SqlDbType.Int).Value = _iFIX_A;
                    cmd.Parameters.Add("@FIX_RecievedDate", SqlDbType.DateTime).Value = _dFIX_RecievedDate;
                    cmd.Parameters.Add("@ExecuteDate", SqlDbType.DateTime).Value = _dExecuteDate;
                    cmd.Parameters.Add("@RealPrice", SqlDbType.Decimal).Value = _decRealPrice;
                    cmd.Parameters.Add("@RealQuantity", SqlDbType.Decimal).Value = _decRealQuantity;
                    cmd.Parameters.Add("@RealAmount", SqlDbType.Decimal).Value = _decRealAmount;
                    cmd.Parameters.Add("@RealStockExchange_ID", SqlDbType.Int).Value = _iExecutionStockExchange_ID;
                    cmd.Parameters.Add("@FeesDiff", SqlDbType.Decimal).Value = _decFeesDiff;
                    cmd.Parameters.Add("@FeesMarket", SqlDbType.Decimal).Value = _decFeesMarket;
                    cmd.Parameters.Add("@AccruedInterest", SqlDbType.Decimal).Value = _decAccruedInterest;
                    cmd.Parameters.Add("@Commission", SqlDbType.Decimal).Value = _decCommission;
                    cmd.Parameters.Add("@CurrRate", SqlDbType.Decimal).Value = _decCurrRate;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 1000).Value = _sNotes;
                    cmd.Parameters.Add("@ValueDate", SqlDbType.NVarChar, 20).Value = _sValueDate;
                    cmd.Parameters.Add("@InformationMethod_ID", SqlDbType.Int).Value = _iInformationMethod_ID;
                    cmd.Parameters.Add("@OfficialInformingDate", SqlDbType.NVarChar, 20).Value = _sOfficialInformingDate;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = _iUser_ID;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@SettlementDate", SqlDbType.DateTime).Value = _dSettlementDate;
                    cmd.Parameters.Add("@FeesPercent", SqlDbType.Decimal).Value = _decFeesPercent;
                    cmd.Parameters.Add("@FeesAmount", SqlDbType.Decimal).Value = _decFeesAmount;
                    cmd.Parameters.Add("@FeesDiscountPercent", SqlDbType.Decimal).Value = _decFeesDiscountPercent;
                    cmd.Parameters.Add("@FeesDiscountAmount", SqlDbType.Decimal).Value = _decFeesDiscountAmount;
                    cmd.Parameters.Add("@FinishFeesPercent", SqlDbType.Decimal).Value = _decFinishFeesPercent;
                    cmd.Parameters.Add("@FinishFeesAmount", SqlDbType.Decimal).Value = _decFinishFeesAmount;
                    cmd.Parameters.Add("@FeesRate", SqlDbType.Decimal).Value = _decFeesRate;
                    cmd.Parameters.Add("@FeesAmountEUR", SqlDbType.Decimal).Value = _decFeesAmountEUR;
                    cmd.Parameters.Add("@MinFeesCurr", SqlDbType.NVarChar, 6).Value = _sMinFeesCurr;
                    cmd.Parameters.Add("@MinFeesAmount", SqlDbType.Decimal).Value = _decMinFeesAmount;
                    cmd.Parameters.Add("@MinFeesDiscountPercent", SqlDbType.Decimal).Value = _decMinFeesDiscountPercent;
                    cmd.Parameters.Add("@MinFeesDiscountAmount", SqlDbType.Decimal).Value = _decMinFeesDiscountAmount;
                    cmd.Parameters.Add("@FinishMinFeesAmount", SqlDbType.Decimal).Value = _decFinishMinFeesAmount;
                    cmd.Parameters.Add("@MinFeesRate", SqlDbType.Decimal).Value = _decMinFeesRate;
                    cmd.Parameters.Add("@MinAmountEUR", SqlDbType.Decimal).Value = _decMinAmountEUR;
                    cmd.Parameters.Add("@TicketFeeCurr", SqlDbType.NVarChar, 6).Value = _sTicketFeeCurr;
                    cmd.Parameters.Add("@TicketFee", SqlDbType.Decimal).Value = _decTicketFee;
                    cmd.Parameters.Add("@TicketFeeDiscountPercent", SqlDbType.Decimal).Value = _decTicketFeeDiscountPercent;
                    cmd.Parameters.Add("@TicketFeeDiscountAmount", SqlDbType.Decimal).Value = _decTicketFeeDiscountAmount;
                    cmd.Parameters.Add("@FinishTicketFee", SqlDbType.Decimal).Value = _decFinishTicketFee;
                    cmd.Parameters.Add("@TicketFeesRate", SqlDbType.Decimal).Value = _decTicketFeesRate;
                    cmd.Parameters.Add("@TicketFeesAmountEUR", SqlDbType.Decimal).Value = _decTicketFeesAmountEUR;
                    cmd.Parameters.Add("@FeesCalc", SqlDbType.Decimal).Value = _decFeesCalc;
                    cmd.Parameters.Add("@ProviderFees", SqlDbType.Decimal).Value = _decProviderFees;
                    cmd.Parameters.Add("@RTO_FeesPercent", SqlDbType.Decimal).Value = _decRTO_FeesPercent;
                    cmd.Parameters.Add("@RTO_FeesAmount", SqlDbType.Decimal).Value = _decRTO_FeesAmount;
                    cmd.Parameters.Add("@RTO_FeesDiscountPercent", SqlDbType.Decimal).Value = _decRTO_FeesDiscountPercent;
                    cmd.Parameters.Add("@RTO_FeesDiscountAmount", SqlDbType.Decimal).Value = _decRTO_FeesDiscountAmount;
                    cmd.Parameters.Add("@RTO_FinishFeesPercent", SqlDbType.Decimal).Value = _decRTO_FinishFeesPercent;
                    cmd.Parameters.Add("@RTO_FinishFeesAmount", SqlDbType.Decimal).Value = _decRTO_FinishFeesAmount;                    
                    cmd.Parameters.Add("@RTO_FeesAmountEUR", SqlDbType.Decimal).Value = _decRTO_FeesAmountEUR;
                    cmd.Parameters.Add("@RTO_MinFeesCurr", SqlDbType.NVarChar, 6).Value = _sRTO_MinFeesCurr;
                    cmd.Parameters.Add("@RTO_MinFeesAmount", SqlDbType.Decimal).Value = _decRTO_MinFeesAmount;
                    cmd.Parameters.Add("@RTO_MinFeesDiscountPercent", SqlDbType.Decimal).Value = _decRTO_MinFeesDiscountPercent;
                    cmd.Parameters.Add("@RTO_MinFeesDiscountAmount", SqlDbType.Decimal).Value = _decRTO_MinFeesDiscountAmount;
                    cmd.Parameters.Add("@RTO_FinishMinFeesAmount", SqlDbType.Decimal).Value = _decRTO_FinishMinFeesAmount;
                    cmd.Parameters.Add("@RTO_TicketFeeCurr", SqlDbType.NVarChar, 6).Value = _sRTO_TicketFeeCurr;
                    cmd.Parameters.Add("@RTO_TicketFee", SqlDbType.Decimal).Value = _decRTO_TicketFee;
                    cmd.Parameters.Add("@RTO_TicketFeeDiscountPercent", SqlDbType.Decimal).Value = _decRTO_TicketFeeDiscountPercent;
                    cmd.Parameters.Add("@RTO_TicketFeeDiscountAmount", SqlDbType.Decimal).Value = _decRTO_TicketFeeDiscountAmount;
                    cmd.Parameters.Add("@RTO_FinishTicketFee", SqlDbType.Decimal).Value = _decRTO_FinishTicketFee;
                    cmd.Parameters.Add("@RTO_FeesProVAT", SqlDbType.Decimal).Value = _decRTO_FeesProVAT;
                    cmd.Parameters.Add("@RTO_FeesVAT", SqlDbType.Decimal).Value = _decRTO_FeesVAT;
                    cmd.Parameters.Add("@RTO_CompanyFees", SqlDbType.Decimal).Value = _decRTO_CompanyFees;
                    cmd.Parameters.Add("@RTO_InvoiceTitle_ID", SqlDbType.Int).Value = _iRTO_InvoiceTitle_ID;
                    cmd.Parameters.Add("@FeesMisc", SqlDbType.Decimal).Value = _decFeesMisc;
                    cmd.Parameters.Add("@FeesNotes", SqlDbType.NVarChar, 200).Value = _sFeesNotes;
                    cmd.Parameters.Add("@FeesCalcMode", SqlDbType.Int).Value = _iFeesCalcMode;
                    cmd.Parameters.Add("@CompanyFeesPercent", SqlDbType.Decimal).Value = _decCompanyFeesPercent;
                    cmd.Parameters.Add("@Pinakidio", SqlDbType.Int).Value = _iPinakidio;
                    cmd.Parameters.Add("@LastCheckFile", SqlDbType.NVarChar, 100).Value = _sLastCheckFile;
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
                using (SqlCommand cmd = new SqlCommand("EditCommand", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@BulkCommand", SqlDbType.NVarChar, 20).Value = _sBulkCommand;
                    cmd.Parameters.Add("@CommandType_ID", SqlDbType.Int).Value = _iCommandType_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Company_ID", SqlDbType.Int).Value = _iCompany_ID;
                    cmd.Parameters.Add("@StockCompany_ID", SqlDbType.Int).Value = _iServiceProvider_ID; ;
                    cmd.Parameters.Add("@Executor_ID", SqlDbType.Int).Value = _iExecutor_ID;
                    cmd.Parameters.Add("@StockExchange_ID", SqlDbType.Int).Value = _iStockExchange_ID;
                    cmd.Parameters.Add("@CustodyProvider_ID", SqlDbType.Int).Value = _iCustodyProvider_ID;
                    cmd.Parameters.Add("@Depository_ID", SqlDbType.Int).Value = _iDepository_ID;
                    cmd.Parameters.Add("@ClientPackage_ID", SqlDbType.Int).Value = _iContract_ID;                               //@@@@@@ ClientPackage_ID -> Contract_ID
                    cmd.Parameters.Add("@Contract_Details_ID", SqlDbType.Int).Value = _iContract_Details_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 30).Value = _sCode;
                    cmd.Parameters.Add("@ProfitCenter", SqlDbType.NVarChar, 50).Value = _sProfitCenter;
                    cmd.Parameters.Add("@AllocationPercent", SqlDbType.Float).Value = _fltAllocationPercent;
                    cmd.Parameters.Add("@Aktion", SqlDbType.Int).Value = _iAktion;
                    cmd.Parameters.Add("@AktionDate", SqlDbType.DateTime).Value = _dAktionDate;
                    cmd.Parameters.Add("@Share_ID", SqlDbType.Int).Value = _iShare_ID;
                    cmd.Parameters.Add("@Product_ID", SqlDbType.Int).Value = _iProduct_ID;
                    cmd.Parameters.Add("@ProductCategory_ID", SqlDbType.Int).Value = _iProductCategory_ID;
                    cmd.Parameters.Add("@Type", SqlDbType.Int).Value = _iPriceType;
                    cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = _decPrice;
                    cmd.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = _decQuantity;
                    cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = _decAmount;
                    cmd.Parameters.Add("@Curr", SqlDbType.NVarChar, 6).Value = _sCurr;
                    cmd.Parameters.Add("@Constant", SqlDbType.Int).Value = _iConstant;
                    cmd.Parameters.Add("@ConstantDate", SqlDbType.NVarChar, 25).Value = _sConstantDate;
                    //cmd.Parameters.Add("@ConstantContinue", SqlDbType.Int).Value = _iConstantContinue;                            // editing ConstantContinue field are into SP EditConstantContinue
                    cmd.Parameters.Add("@RecieveDate", SqlDbType.DateTime).Value = _dRecieveDate;
                    cmd.Parameters.Add("@RecieveMethod_ID", SqlDbType.Int).Value = _iRecieveMethod_ID;
                    cmd.Parameters.Add("@BestExecution", SqlDbType.Int).Value = _iBestExecution;
                    cmd.Parameters.Add("@SentDate", SqlDbType.DateTime).Value = _dSentDate;
                    cmd.Parameters.Add("@SendCheck", SqlDbType.Int).Value = _iSendCheck;
                    cmd.Parameters.Add("@FIX_A", SqlDbType.Int).Value = _iFIX_A;
                    cmd.Parameters.Add("@FIX_RecievedDate", SqlDbType.DateTime).Value = _dFIX_RecievedDate;
                    cmd.Parameters.Add("@ExecuteDate", SqlDbType.DateTime).Value = _dExecuteDate;
                    cmd.Parameters.Add("@RealPrice", SqlDbType.Decimal).Value = _decRealPrice;
                    cmd.Parameters.Add("@RealQuantity", SqlDbType.Decimal).Value = _decRealQuantity;
                    cmd.Parameters.Add("@RealAmount", SqlDbType.Decimal).Value = _decRealAmount;
                    cmd.Parameters.Add("@RealStockExchange_ID", SqlDbType.Int).Value = _iExecutionStockExchange_ID;
                    cmd.Parameters.Add("@FeesDiff", SqlDbType.Decimal).Value = _decFeesDiff;
                    cmd.Parameters.Add("@FeesMarket", SqlDbType.Decimal).Value = _decFeesMarket;
                    cmd.Parameters.Add("@AccruedInterest", SqlDbType.Decimal).Value = _decAccruedInterest;
                    cmd.Parameters.Add("@Commission", SqlDbType.Decimal).Value = _decCommission;
                    cmd.Parameters.Add("@CurrRate", SqlDbType.Decimal).Value = _decCurrRate;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 1000).Value = _sNotes;
                    cmd.Parameters.Add("@ValueDate", SqlDbType.NVarChar, 20).Value = _sValueDate;
                    cmd.Parameters.Add("@InformationMethod_ID", SqlDbType.Int).Value = _iInformationMethod_ID;
                    cmd.Parameters.Add("@OfficialInformingDate", SqlDbType.NVarChar, 20).Value = _sOfficialInformingDate;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@SettlementDate", SqlDbType.DateTime).Value = _dSettlementDate;
                    cmd.Parameters.Add("@FeesPercent", SqlDbType.Decimal).Value = _decFeesPercent;
                    cmd.Parameters.Add("@FeesAmount", SqlDbType.Decimal).Value = _decFeesAmount;
                    cmd.Parameters.Add("@FeesDiscountPercent", SqlDbType.Decimal).Value = _decFeesDiscountPercent;
                    cmd.Parameters.Add("@FeesDiscountAmount", SqlDbType.Decimal).Value = _decFeesDiscountAmount;
                    cmd.Parameters.Add("@FinishFeesPercent", SqlDbType.Decimal).Value = _decFinishFeesPercent;
                    cmd.Parameters.Add("@FinishFeesAmount", SqlDbType.Decimal).Value = _decFinishFeesAmount;
                    cmd.Parameters.Add("@FeesRate", SqlDbType.Decimal).Value = _decFeesRate;
                    cmd.Parameters.Add("@FeesAmountEUR", SqlDbType.Decimal).Value = _decFeesAmountEUR;
                    cmd.Parameters.Add("@MinFeesCurr", SqlDbType.NVarChar, 6).Value = _sMinFeesCurr;
                    cmd.Parameters.Add("@MinFeesAmount", SqlDbType.Decimal).Value = _decMinFeesAmount;
                    cmd.Parameters.Add("@MinFeesDiscountPercent", SqlDbType.Decimal).Value = _decMinFeesDiscountPercent;
                    cmd.Parameters.Add("@MinFeesDiscountAmount", SqlDbType.Decimal).Value = _decMinFeesDiscountAmount;
                    cmd.Parameters.Add("@FinishMinFeesAmount", SqlDbType.Decimal).Value = _decFinishMinFeesAmount;
                    cmd.Parameters.Add("@MinFeesRate", SqlDbType.Decimal).Value = _decMinFeesRate;
                    cmd.Parameters.Add("@MinAmountEUR", SqlDbType.Decimal).Value = _decMinAmountEUR;
                    cmd.Parameters.Add("@TicketFeeCurr", SqlDbType.NVarChar, 6).Value = _sTicketFeeCurr;
                    cmd.Parameters.Add("@TicketFee", SqlDbType.Decimal).Value = _decTicketFee;
                    cmd.Parameters.Add("@TicketFeeDiscountPercent", SqlDbType.Decimal).Value = _decTicketFeeDiscountPercent;
                    cmd.Parameters.Add("@TicketFeeDiscountAmount", SqlDbType.Decimal).Value = _decTicketFeeDiscountAmount;
                    cmd.Parameters.Add("@FinishTicketFee", SqlDbType.Decimal).Value = _decFinishTicketFee;
                    cmd.Parameters.Add("@TicketFeesRate", SqlDbType.Decimal).Value = _decTicketFeesRate;
                    cmd.Parameters.Add("@TicketFeesAmountEUR", SqlDbType.Decimal).Value = _decTicketFeesAmountEUR;
                    cmd.Parameters.Add("@FeesCalc", SqlDbType.Decimal).Value = _decFeesCalc;
                    cmd.Parameters.Add("@ProviderFees", SqlDbType.Decimal).Value = _decProviderFees;
                    cmd.Parameters.Add("@RTO_FeesPercent", SqlDbType.Decimal).Value = _decRTO_FeesPercent;
                    cmd.Parameters.Add("@RTO_FeesAmount", SqlDbType.Decimal).Value = _decRTO_FeesAmount;
                    cmd.Parameters.Add("@RTO_FeesDiscountPercent", SqlDbType.Decimal).Value = _decRTO_FeesDiscountPercent;
                    cmd.Parameters.Add("@RTO_FeesDiscountAmount", SqlDbType.Decimal).Value = _decRTO_FeesDiscountAmount;
                    cmd.Parameters.Add("@RTO_FinishFeesPercent", SqlDbType.Decimal).Value = _decRTO_FinishFeesPercent;
                    cmd.Parameters.Add("@RTO_FinishFeesAmount", SqlDbType.Decimal).Value = _decRTO_FinishFeesAmount;                    
                    cmd.Parameters.Add("@RTO_FeesAmountEUR", SqlDbType.Decimal).Value = _decRTO_FeesAmountEUR;
                    cmd.Parameters.Add("@RTO_MinFeesCurr", SqlDbType.NVarChar, 6).Value = _sRTO_MinFeesCurr;
                    cmd.Parameters.Add("@RTO_MinFeesAmount", SqlDbType.Decimal).Value = _decRTO_MinFeesAmount;
                    cmd.Parameters.Add("@RTO_MinFeesDiscountPercent", SqlDbType.Decimal).Value = _decRTO_MinFeesDiscountPercent;
                    cmd.Parameters.Add("@RTO_MinFeesDiscountAmount", SqlDbType.Decimal).Value = _decRTO_MinFeesDiscountAmount;
                    cmd.Parameters.Add("@RTO_FinishMinFeesAmount", SqlDbType.Decimal).Value = _decRTO_FinishMinFeesAmount;
                    cmd.Parameters.Add("@RTO_TicketFeeCurr", SqlDbType.NVarChar, 6).Value = _sRTO_TicketFeeCurr;
                    cmd.Parameters.Add("@RTO_TicketFee", SqlDbType.Decimal).Value = _decRTO_TicketFee;
                    cmd.Parameters.Add("@RTO_TicketFeeDiscountPercent", SqlDbType.Decimal).Value = _decRTO_TicketFeeDiscountPercent;
                    cmd.Parameters.Add("@RTO_TicketFeeDiscountAmount", SqlDbType.Decimal).Value = _decRTO_TicketFeeDiscountAmount;
                    cmd.Parameters.Add("@RTO_FinishTicketFee", SqlDbType.Decimal).Value = _decRTO_FinishTicketFee;
                    cmd.Parameters.Add("@RTO_FeesProVAT", SqlDbType.Decimal).Value = _decRTO_FeesProVAT;
                    cmd.Parameters.Add("@RTO_FeesVAT", SqlDbType.Decimal).Value = _decRTO_FeesVAT;
                    cmd.Parameters.Add("@RTO_CompanyFees", SqlDbType.Decimal).Value = _decRTO_CompanyFees;
                    cmd.Parameters.Add("@RTO_InvoiceTitle_ID", SqlDbType.Int).Value = _iRTO_InvoiceTitle_ID;
                    cmd.Parameters.Add("@FeesMisc", SqlDbType.Decimal).Value = _decFeesMisc;
                    cmd.Parameters.Add("@FeesNotes", SqlDbType.NVarChar, 200).Value = _sFeesNotes;
                    cmd.Parameters.Add("@FeesCalcMode", SqlDbType.Int).Value = _iFeesCalcMode;
                    cmd.Parameters.Add("@CompanyFeesPercent", SqlDbType.Decimal).Value = _decCompanyFeesPercent;
                    cmd.Parameters.Add("@Pinakidio", SqlDbType.Int).Value = _iPinakidio;
                    cmd.Parameters.Add("@LastCheckFile", SqlDbType.NVarChar, 100).Value = _sLastCheckFile;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = _iUser_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void EditStatus()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_EditSisterCommand_Status", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Parent_ID", SqlDbType.Int).Value = _iParent_ID;       
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;                  
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Commands";
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
                using (SqlCommand cmd = new SqlCommand("sp_EditBulkCommand_ID", conn))
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
        public void EditSisterCommand()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_EditSisterCommand_Status", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Parent_ID", SqlDbType.Int).Value = _iParent_ID;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void CalcFees()
        {
            decimal sgRate = 0, sgRate1 = 0, sgFeesBaseAmount = 0, decFinishFees, _decRTO_FeesRate = 1;
            int j, k;
            DataTable dtEURRates;

            clsClientsBrokerageFees klsClientsBrokerageFees = new clsClientsBrokerageFees();
            clsClientsRTOFees klsClientsRTOFees = new clsClientsRTOFees();
            clsProductsCodes klsProductsCode = new clsProductsCodes();
            clsCurrencies klsCurrency = new clsCurrencies();

            if (_dExecuteDate.Date != Convert.ToDateTime("01/01/1900").Date)
            {
                if (_iFeesCalcMode == 1)      // 1 - Automatic Calculation Mode, 2 - Manually Calculation Mode
                {
                    klsCurrency.DateFrom = _dExecuteDate;
                    klsCurrency.DateTo = _dExecuteDate;
                    klsCurrency.Code = "EUR";
                    klsCurrency.GetCurrencyRates_Period();
                    dtEURRates = klsCurrency.List.Copy();

                    //--- step 1 - Define three currency rates: _decCurrRate           - EUR / Transaction currency 
                    //                                          _decMinFeesRate        - MinFees Currency / Transaction currency
                    //                                          _dec TicketFeesRate    - TicketFees Currency / Transaction currency

                    //    _decCurrRate           - EUR / Transaction currency 
                    _decCurrRate = 0;
                    if (_sCurr == "EUR") _decCurrRate = 1;
                    else
                    {
                        foundRows = dtEURRates.Select("Currency = 'EUR" + _sCurr + "='");
                        if (foundRows.Length > 0) _decCurrRate = Convert.ToDecimal(foundRows[0]["Rate"]);
                    }

                    //   _decMinFeesRate          - MinFees Currency / Transaction currency (_sCurr)
                    _decMinFeesRate = 0;
                    if (_sMinFeesCurr.Length > 0)  {                                                                     // if _sMinFeesCurr is not Empty     
                        if (_sMinFeesCurr == "EUR") _decMinFeesRate = _decCurrRate;                                      // if _sMinFeesCurr = EUR rate MinFeesRate is equal CurrRate, that is EUR
                        else  {
                            if (_sMinFeesCurr == _sCurr) _decMinFeesRate = 1;                                            // if _sMinFeesCurr = _sCurr     MinFeesRate = 1
                            else  {
                                sgRate = 0;                                                                              // for example _sMinFeesCurr = HKD and _sCurr = USD   - in this case
                                foundRows = dtEURRates.Select("Currency = 'EUR" + _sCurr + "='");                        // rate HKD/USD calculate as (EUR/USD) / (EUR/HKD)
                                if (foundRows.Length > 0) sgRate = Convert.ToDecimal(foundRows[0]["Rate"]);

                                sgRate1 = 0;
                                foundRows = dtEURRates.Select("Currency = 'EUR" + _sMinFeesCurr + "='");
                                if (foundRows.Length > 0) sgRate1 = Convert.ToDecimal(foundRows[0]["Rate"]);

                                if (sgRate1 != 0) _decMinFeesRate = sgRate / sgRate1;
                            }
                        }
                    }

                    //   _decTicketFeesRate          - TicketFees Currency / Transaction currency (_sCurr)
                    _decTicketFeesRate = 0;
                    if (_sTicketFeeCurr.Length > 0) {                                                                     // if _sTicketFeeCurr is not Empty     
                        if (_sTicketFeeCurr == "EUR") _decTicketFeesRate = _decCurrRate;                                  // if _sTicketFeeCurr = EUR rate TicketFeesRate is equal CurrRate, that is EUR
                        else  {
                            if (_sTicketFeeCurr == _sCurr) _decTicketFeesRate = 1;                                        // if _sTicketFeeCurr = _sCurr     TicketFeesRate = 1
                            else
                            {
                                sgRate = 0;                                                                               // for example _sTicketFeeCurr = HKD and _sCurr = USD   - in this case
                                foundRows = dtEURRates.Select("Currency = 'EUR" + _sCurr + "='");                         // rate HKD/USD calculate as (EUR/USD) / (EUR/HKD)
                                if (foundRows.Length > 0) sgRate = Convert.ToDecimal(foundRows[0]["Rate"]);

                                sgRate1 = 0;
                                foundRows = dtEURRates.Select("Currency = 'EUR" + _sTicketFeeCurr + "='");
                                if (foundRows.Length > 0) sgRate1 = Convert.ToDecimal(foundRows[0]["Rate"]);

                                if (sgRate1 != 0) _decTicketFeesRate = sgRate / sgRate1;
                            }
                        }
                    }

                    //--- step 2 - Calculate  _decRealAmount ------------------
                    if (_iProduct_ID == 2)                                                                                 // Omologa (ShareType=2)
                        _decRealAmount = _decRealQuantity * _decRealPrice / 100;      
                    else
                        _decRealAmount = _decRealQuantity * _decRealPrice;

                    //--- step 3 - Calculate  _decInvestAmount -----------------
                    _decInvestAmount = _decRealAmount + _decAccruedInterest;


                    //--- step 4 - Define _iContract_ID Package's fees and calculate all fees
                    k = 0;                                                             //  k - is flag for checking if fees was found and calculated: 0 - fees wasn't found, 1 - fees found
                    klsClientsBrokerageFees.Contract_ID = _iContract_ID;
                    klsClientsBrokerageFees.Product_ID = _iProduct_ID;
                    klsClientsBrokerageFees.ProductCategory_ID = _iProductCategory_ID;
                    klsClientsBrokerageFees.Quantity = Convert.ToInt32(_decRealQuantity);
                    klsClientsBrokerageFees.StockExchange_ID = _iStockExchange_ID;
                    klsClientsBrokerageFees.GetFees();

                    foreach (DataRow dtRow in klsClientsBrokerageFees.List.Rows)
                    {
                        k = 1;                  // fees was found
                        if (_iAktion == 1)
                        {
                            _decFeesPercent = Convert.ToDecimal(dtRow["BuyFeesPercent"]);
                            _decFeesDiscountPercent = Convert.ToDecimal(dtRow["FeesDiscountPercent"]);
                            _decFinishFeesPercent = Convert.ToDecimal(dtRow["FinishBuyFeesPercent"]);

                            _sTicketFeeCurr = dtRow["TicketFeesCurr"] + "";
                            _decTicketFee = Convert.ToDecimal(dtRow["TicketFeesBuyAmount"]) * _decTicketFeesRate;
                            _decTicketFeeDiscountPercent = Convert.ToDecimal(dtRow["TicketFeesDiscountPercent"]);
                            _decFinishTicketFee = _decTicketFee * (1 - _decTicketFeeDiscountPercent / 100);
                            _decFinishTicketFee = Convert.ToDecimal(dtRow["TicketFinishBuyFeesAmount"]);                            
                        }
                        else
                        {
                            _decFeesPercent = Convert.ToDecimal(dtRow["SellFeesPercent"]);
                            _decFeesDiscountPercent = Convert.ToDecimal(dtRow["FeesDiscountPercent"]);
                            _decFinishFeesPercent = Convert.ToDecimal(dtRow["FinishSellFeesPercent"]);

                            _sTicketFeeCurr = dtRow["TicketFeesCurr"] + "";
                            _decTicketFee = Convert.ToDecimal(dtRow["TicketFeesSellAmount"]) * _decTicketFeesRate;
                            _decTicketFeeDiscountPercent = Convert.ToDecimal(dtRow["TicketFeesDiscountPercent"]);
                            _decFinishTicketFee = _decTicketFee * (1 - _decTicketFeeDiscountPercent) / 100;
                            _decFinishTicketFee = Convert.ToDecimal(dtRow["TicketFinishSellFeesAmount"]);
                        }

                        switch (_iBusinessType_ID) {
                            case 1:
                                if (_iProduct_ID == 2)   {                                     // 2 - Omologa
                                    switch (_iServiceProviderFeesMode)
                                    {
                                        case 0:
                                            sgFeesBaseAmount = _decRealQuantity;
                                            break;
                                        case 1:
                                            sgFeesBaseAmount = _decInvestAmount;
                                            break;
                                        case 2:
                                            sgFeesBaseAmount = _decInvestAmount;
                                            break;
                                    }
                                }
                                else sgFeesBaseAmount = _decInvestAmount;
                                break;
                            case 2:
                                sgFeesBaseAmount = _decInvestAmount;
                                break;
                        }

                        //--- calculate Προμήθεια rows values ---------------------
                        _decFeesAmount = _decFeesPercent * sgFeesBaseAmount / 100;
                        _decFinishFeesAmount = _decFinishFeesPercent * sgFeesBaseAmount / 100;
                        _decFeesDiscountAmount = _decFeesAmount - _decFinishFeesAmount;
                        _decFeesAmountEUR = _decFinishFeesAmount;                                       // FeesRate = 1 always, so _decFeesAmountEUR = _decFinishFeesAmount                                              

                        //--- calculate Min. Fees rows values ---------------------
                        _sMinFeesCurr = dtRow["MinFeesCurr"] + "";
                        _decMinFeesAmount = Convert.ToDecimal(dtRow["MinFeesAmount"]);
                        _decMinFeesDiscountPercent = 0;
                        _decMinFeesDiscountAmount = 0;
                        _decFinishMinFeesAmount = _decMinFeesAmount;
                        _decMinAmountEUR = _decFinishMinFeesAmount * _decMinFeesRate;

                        _sFeesNotes = "";

                        //--- calculate finish data ---------------------------------
                        decFinishFees = _decFeesAmountEUR;
                        if (_decFeesAmountEUR < _decMinAmountEUR) decFinishFees = _decMinAmountEUR;                        
                        _decProviderFees = decFinishFees + _decFinishTicketFee;
                    }

                    // --- step 5 - check if fees was found and calculated -----------
                    if (_decFinishFeesAmount == 0)                            // Fees = 0. Why?
                    {
                        if (k == 0) {
                            klsClientsBrokerageFees.Contract_ID = _iContract_ID;
                            klsClientsBrokerageFees.Product_ID = _iProduct_ID;
                            klsClientsBrokerageFees.ProductCategory_ID = _iProductCategory_ID;
                            klsClientsBrokerageFees.Quantity = Convert.ToInt32(_decRealQuantity);
                            klsClientsBrokerageFees.CheckTransactionFees();
                            j = klsClientsBrokerageFees.Error_ID;

                            switch (j) {
                                case 1:
                                    _sFeesNotes = "Προμήθεια συναλλαγής δεν βρέθηκε. Δεν καταχωρήθηκάν προμήθειες πακέτου";
                                    break;
                                case 2:
                                    _sFeesNotes = "Προμήθεια συναλλαγής δεν βρέθηκε. Δεν καταχωρήθηκάν προμήθειες του προϊοντος";
                                    break;
                                case 3:
                                    _sFeesNotes = "Προμήθεια συναλλαγής δεν βρέθηκε. Δεν καταχωρήθηκάν στην κλίμακα προμήθειες για το εκτελεσμένο ποσό";
                                    break;
                            }
                        }
                        else {
                            if (_iAktion == 2 && _iProduct_ID == 6)                                                    // @@@  ПАРАМЕТРИЗИРОВАТЬ 6-AK    πώληση των ΑΚ
                                _sFeesNotes = "Η πώληση των ΑΚ έχει προμήθεια 0";
                            else
                                if (_decRealAmount == 0)
                                _sFeesNotes = "Η προμήθεια είναι 0 γιατί ο τζίρος συναλλαγής είναι 0";
                            else
                                if (_decFinishFeesPercent == 0)
                                _sFeesNotes = "Η προμήθεια είναι 0 γιατί το ποσοστό προμήθειας είναι 0";
                            else
                                j = 0;
                        }
                    }

                    //--- step 6 - Define _iContract_ID Package's RTOfees and calculate all fees
                    k = 0;                                                  //  k - is flag for checking if fees was found and calculated: 0 - fees wasn't found, 1 - fees found
                    klsClientsRTOFees.Contract_ID = _iContract_ID;
                    klsClientsRTOFees.Product_ID = _iProduct_ID;
                    klsClientsRTOFees.ProductCategory_ID = _iProductCategory_ID;
                    klsClientsRTOFees.Quantity = Convert.ToInt32(_decRealQuantity);
                    klsClientsRTOFees.StockExchange_ID = _iStockExchange_ID;
                    klsClientsRTOFees.GetFees();

                    _decRTO_FeesRate = _decCurrRate;
                    foreach (DataRow dtRow in klsClientsRTOFees.List.Rows)
                    {
                        k = 1;   // fees was found

                        if (_iAktion == 1) {                                                                             // BUY
                            _decRTO_FeesPercent = Convert.ToDecimal(dtRow["BuyFeesPercent"]);
                            if (_decRTO_FeesDiscountPercent == 0) {
                                _decRTO_FeesDiscountPercent = Convert.ToDecimal(dtRow["FeesDiscountPercent"]);
                                _decRTO_FinishFeesPercent = Convert.ToDecimal(dtRow["FinishBuyFeesPercent"]);
                            }
                            _decRTO_TicketFee = Convert.ToDecimal(dtRow["TicketFeesBuyAmount"]) * _decRTO_FeesRate;

                        }
                        else  {                                                                                            // SELL
                            _decRTO_FeesPercent = Convert.ToDecimal(dtRow["SellFeesPercent"]);
                            if (_decRTO_FeesDiscountPercent == 0) {
                                _decRTO_FeesDiscountPercent = Convert.ToDecimal(dtRow["FeesDiscountPercent"]);
                                _decRTO_FinishFeesPercent = Convert.ToDecimal(dtRow["FinishSellFeesPercent"]);
                            }
                            _decRTO_TicketFee = Convert.ToDecimal(dtRow["TicketFeesSellAmount"]) * _decRTO_FeesRate;
                        }
                        _decRTO_TicketFeeDiscountPercent = Convert.ToDecimal(dtRow["TicketFeesDiscountPercent"]);
                        _decRTO_FinishTicketFee = _decRTO_TicketFee * (1 - _decRTO_TicketFeeDiscountPercent / 100);
                        _decRTO_TicketFeeDiscountAmount = _decRTO_TicketFee - _decRTO_FinishTicketFee;

                        _decRTO_FeesAmount = _decRTO_FeesPercent * _decRealAmount / 100;
                        _decRTO_FeesDiscountAmount = _decRTO_FeesAmount * _decRTO_FeesDiscountPercent / 100;
                        _decRTO_FinishFeesAmount = _decRTO_FeesAmount - _decRTO_FeesDiscountAmount;
                        if (_decRTO_FeesAmount != 0) _decRTO_FinishFeesPercent = _decRTO_FeesPercent * _decRTO_FinishFeesAmount / _decRTO_FeesAmount;
                        if (_decRTO_FeesRate != 0) _decRTO_FeesAmountEUR = _decRTO_FinishFeesAmount / _decRTO_FeesRate;

                        _decRTO_MinFeesAmount = Convert.ToDecimal(dtRow["MinFeesAmount"]);
                        _sRTO_MinFeesCurr = dtRow["MinFeesCurr"] + "";
                        _decRTO_MinFeesDiscountAmount = _decRTO_MinFeesAmount * _decRTO_MinFeesDiscountPercent / 100;
                        _decRTO_FinishMinFeesAmount = _decRTO_MinFeesAmount - _decRTO_MinFeesDiscountAmount;
                        _sFeesNotes = "";

                        if (_decRTO_FeesAmountEUR >= _decRTO_FinishMinFeesAmount) _decRTO_FeesProVAT = _decRTO_FeesAmountEUR;
                        else _decRTO_FeesProVAT = _decRTO_FinishMinFeesAmount;

                        _decRTO_FeesVAT = 0;
                        _decRTO_CompanyFees = _decRTO_FeesProVAT + _decRTO_FeesVAT;
                    }
                }
            }
        }
        public void CalcRTOFees()
        {
            decimal decCurrRate, decRTO_FeesAmount, decRTO_FeesAmountEUR, decRTO_FeesProVAT, decRTO_FinishFeesAmount, decRTO_FinishMinFeesAmount, 
                    decRTO_FinishTicketFeesAmount, decRTO_FeesVAT, decRTO_CompanyFees;

            clsProductsCodes ProductsCode = new clsProductsCodes();
            ProductsCode.DateFrom = _dDateFrom;
            ProductsCode.DateTo = _dDateTo;
            ProductsCode.Code = "EUR";
            ProductsCode.GetPrices_Period();

            try
            {
                conn.Open();
                conn1.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Commands WHERE AktionDate >= '" + _dDateFrom.ToString("yyyy/MM/dd") + "' AND AktionDate <= '" + _dDateTo.ToString("yyyy/MM/dd") + " 23:59:59" +
                         "' AND (CommandType_ID = 1) AND (ExecuteDate > '1900/01/01') AND (RTO_InvoiceTitle_ID = 0) ORDER BY ID DESC", conn);
                cmd.CommandType = CommandType.Text;
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if (Convert.ToInt32(drList["ID"]) == 412568)
                        decCurrRate = 0;

                    decCurrRate = Convert.ToDecimal(drList["CurrRate"]);

                    if ((drList["Curr"] + "") == "EUR")  decCurrRate = 1;
                    else
                    {
                        foundRows = ProductsCode.List.Select("DateIns = '" + Convert.ToDateTime(drList["AktionDate"]).ToString("yyyy/MM/dd") + "' AND Code = '" + "EUR" + drList["Curr"] + "='");
                        if (foundRows.Length > 0) decCurrRate = Convert.ToDecimal(foundRows[0]["Close"]);                       
                    }
                                        
                    //--- calculate Προμήθεια row ------------------------
                    decRTO_FeesAmount = Convert.ToDecimal(drList["RTO_FeesPercent"]) * (Convert.ToDecimal(drList["RealAmount"]) + Convert.ToDecimal(drList["AccruedInterest"])) / 100;
                    decRTO_FinishFeesAmount = decRTO_FeesAmount - Convert.ToDecimal(drList["RTO_FeesDiscountAmount"]);
                    if (decCurrRate != 0) decRTO_FeesAmountEUR = decRTO_FinishFeesAmount / decCurrRate;
                    else decRTO_FeesAmountEUR = 0;

                    //--- calculate Min.Fees row ------------------------
                    decRTO_FinishMinFeesAmount = Convert.ToDecimal(drList["RTO_MinFeesAmount"]) - Convert.ToDecimal(drList["RTO_MinFeesDiscountAmount"]);

                    //--- calculate Ticket Fee row ------------------------
                    decRTO_FinishTicketFeesAmount = Convert.ToDecimal(drList["RTO_TicketFee"]) - Convert.ToDecimal(drList["RTO_TicketFeeDiscountAmount"]);

                  
                    if (decRTO_FeesAmountEUR >= decRTO_FinishMinFeesAmount)
                        decRTO_FeesProVAT = decRTO_FeesAmountEUR + decRTO_FinishTicketFeesAmount;
                    else
                        decRTO_FeesProVAT = decRTO_FinishMinFeesAmount + decRTO_FinishTicketFeesAmount;

                    decRTO_FeesVAT = 0;
                    decRTO_CompanyFees = decRTO_FeesProVAT + decRTO_FeesVAT;

                    cmd1 = new SqlCommand("UPDATE Commands SET CurrRate = " + decCurrRate.ToString().Replace(",", ".") +
                                                            ", RTO_FeesAmount = " + decRTO_FeesAmount.ToString().Replace(",", ".") +
                                                            ", RTO_FinishFeesAmount = " + decRTO_FinishFeesAmount.ToString().Replace(",", ".") +
                                                            ", RTO_FeesAmountEUR = " + decRTO_FeesAmountEUR.ToString().Replace(",", ".") +
                                                            ", RTO_FinishMinFeesAmount = " + decRTO_FinishMinFeesAmount.ToString().Replace(",", ".") +
                                                            ", RTO_FinishTicketFee = " + decRTO_FinishTicketFeesAmount.ToString().Replace(",", ".") +
                                                            ", RTO_FeesProVAT = " + decRTO_FeesProVAT.ToString().Replace(",", ".") +
                                                            ", RTO_FeesVAT = 0" +
                                                            ", RTO_CompanyFees = " + decRTO_CompanyFees.ToString().Replace(",", ".") +
                                                            " WHERE ID = " + drList["ID"], conn1);
                    cmd1.CommandType = CommandType.Text;
                    cmd1.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); conn1.Close(); }
        } 
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int BusinessType_ID { get { return this._iBusinessType_ID; } set { this._iBusinessType_ID = value; } }
        public int Company_ID { get { return this._iCompany_ID; } set { this._iCompany_ID = value; } }
        public int CommandType_ID { get { return this._iCommandType_ID; } set { this._iCommandType_ID = value; } }
        public string BulkCommand { get { return this._sBulkCommand; } set { this._sBulkCommand = value; } }
        public int II_ID { get { return this._iII_ID; } set { this._iII_ID = value; } }
        public int Client_ID { get { return this._iClient_ID; } set { this._iClient_ID = value; } }
        public int ClientTipos { get { return this._iClientTipos; } set { this._iClientTipos = value; } }
        public int Parent_ID { get { return this._iParent_ID; } set { this._iParent_ID = value; } }
        public int Contract_ID { get { return this._iContract_ID; } set { this._iContract_ID = value; } }
        public int Contract_Details_ID { get { return this._iContract_Details_ID; } set { this._iContract_Details_ID = value; } }
        public int Contract_Packages_ID { get { return this._iContract_Packages_ID; } set { this._iContract_Packages_ID = value; } }
        public string ContractTitle { get { return this._sContractTitle; } set { this._sContractTitle = value; } }
        public int ContractTipos { get { return this._iContractTipos; } set { this._iContractTipos = value; } }        
        public string Code { get { return this._sCode; } set { this._sCode = value; } }
        public string ProfitCenter { get { return this._sProfitCenter; } set { this._sProfitCenter = value; } }
        public float AllocationPercent { get { return this._fltAllocationPercent; } set { this._fltAllocationPercent = value; } }
        public int Aktion { get { return this._iAktion; } set { this._iAktion = value; } }
        public DateTime AktionDate { get { return this._dAktionDate; } set { this._dAktionDate = value; } }
        public int Share_ID { get { return this._iShare_ID; } set { this._iShare_ID = value; } }
        public int Product_ID { get { return this._iProduct_ID; } set { this._iProduct_ID = value; } }
        public int ProductCategory_ID { get { return this._iProductCategory_ID; } set { this._iProductCategory_ID = value; } }
        public int PriceType { get { return this._iPriceType; } set { this._iPriceType = value; } }
        public decimal Price { get { return this._decPrice; } set { this._decPrice = value; } }
        public decimal Quantity { get { return this._decQuantity; } set { this._decQuantity = value; } }
        public decimal Amount { get { return this._decAmount; } set { this._decAmount = value; } }
        public string Curr { get { return this._sCurr; } set { this._sCurr = value; } }
        public int Constant { get { return this._iConstant; } set { this._iConstant = value; } }
        public string ConstantDate { get { return this._sConstantDate; } set { this._sConstantDate = value; } }
        public int ConstantContinue { get { return this._iConstantContinue; } set { this._iConstantContinue = value; } }
        public int ServiceProvider_ID { get { return this._iServiceProvider_ID; } set { this._iServiceProvider_ID = value; } }
        public int ProductStockExchange_ID { get { return this._iProductStockExchange_ID; } set { this._iProductStockExchange_ID = value; } }
        public string ProductStockExchange_MIC { get { return this._sProductStockExchange_MIC; } set { this._sProductStockExchange_MIC = value; } }
        public string ProductStockExchange_Title { get { return this._sProductStockExchange_Title; } set { this._sProductStockExchange_Title = value; } }
        public int StockExchange_ID { get { return this._iStockExchange_ID; } set { this._iStockExchange_ID = value; } }
        public string StockExchange_MIC { get { return this._sStockExchange_MIC; } set { this._sStockExchange_MIC = value; } }
        public string StockExchange_Title { get { return this._sStockExchange_Title; } set { this._sStockExchange_Title = value; } }
        public int ExecutionStockExchange_ID { get { return this._iExecutionStockExchange_ID; } set { this._iExecutionStockExchange_ID = value; } }
        public string ExecutionStockExchange_MIC { get { return this._sExecutionStockExchange_MIC; } set { this._sExecutionStockExchange_MIC = value; } }
        public string ExecutionStockExchange_Title { get { return this._sExecutionStockExchange_Title; } set { this._sExecutionStockExchange_Title = value; } }
        public int CustodyProvider_ID { get { return this._iCustodyProvider_ID; } set { this._iCustodyProvider_ID = value; } }
        public int Depository_ID { get { return this._iDepository_ID; } set { this._iDepository_ID = value; } }
        public DateTime RecieveDate { get { return this._dRecieveDate; } set { this._dRecieveDate = value; } }
        public int RecieveMethod_ID { get { return this._iRecieveMethod_ID; } set { this._iRecieveMethod_ID = value; } }
        public int BestExecution { get { return this._iBestExecution; } set { this._iBestExecution = value; } }        
        public DateTime SentDate { get { return this._dSentDate; } set { this._dSentDate = value; } }
        public int FIX_A { get { return this._iFIX_A; } set { this._iFIX_A = value; } }
        public DateTime FIX_RecievedDate { get { return this._dFIX_RecievedDate; } set { this._dFIX_RecievedDate = value; } }
        public int User_ID { get { return this._iUser_ID; } set { this._iUser_ID = value; } }
        public DateTime DateIns { get { return this._dDateIns; } set { this._dDateIns = value; } }
        public int Pinakidio { get { return this._iPinakidio; } set { this._iPinakidio = value; } }
        public string LastCheckFile { get { return this._sLastCheckFile; } set { this._sLastCheckFile = value; } }
        public DateTime ExecuteDate { get { return this._dExecuteDate; } set { this._dExecuteDate = value; } }
        public decimal RealPrice { get { return this._decRealPrice; } set { this._decRealPrice = value; } }
        public decimal RealQuantity { get { return this._decRealQuantity; } set { this._decRealQuantity = value; } }
        public decimal RealAmount { get { return this._decRealAmount; } set { this._decRealAmount = value; } }
        public decimal FeesDiff { get { return this._decFeesDiff; } set { this._decFeesDiff = value; } }
        public decimal FeesMarket { get { return this._decFeesMarket; } set { this._decFeesMarket = value; } }
        public decimal AccruedInterest { get { return this._decAccruedInterest; } set { this._decAccruedInterest = value; } }
        public decimal Commission { get { return this._decCommission; } set { this._decCommission = value; } }
        public string Currency { get { return this._sCurrency; } set { this._sCurrency = value; } }
        public DateTime SettlementDate { get { return this._dSettlementDate; } set { this._dSettlementDate = value; } }
        public decimal FeesPercent { get { return this._decFeesPercent; } set { this._decFeesPercent = value; } }
        public decimal FeesAmount { get { return this._decFeesAmount; } set { this._decFeesAmount = value; } }
        public decimal FeesDiscountPercent { get { return this._decFeesDiscountPercent; } set { this._decFeesDiscountPercent = value; } }
        public decimal FeesDiscountAmount { get { return this._decFeesDiscountAmount; } set { this._decFeesDiscountAmount = value; } }
        public decimal FinishFeesPercent { get { return this._decFinishFeesPercent; } set { this._decFinishFeesPercent = value; } }
        public decimal FinishFeesAmount { get { return this._decFinishFeesAmount; } set { this._decFinishFeesAmount = value; } }
        public decimal FeesRate { get { return this._decFeesRate; } set { this._decFeesRate = value; } }
        public decimal FeesAmountEUR { get { return this._decFeesAmountEUR; } set { this._decFeesAmountEUR = value; } }
        public string MinFeesCurr { get { return this._sMinFeesCurr; } set { this._sMinFeesCurr = value; } }
        public decimal MinFeesAmount { get { return this._decMinFeesAmount; } set { this._decMinFeesAmount = value; } }
        public decimal MinFeesDiscountPercent { get { return this._decMinFeesDiscountPercent; } set { this._decMinFeesDiscountPercent = value; } }
        public decimal MinFeesDiscountAmount { get { return this._decMinFeesDiscountAmount; } set { this._decMinFeesDiscountAmount = value; } }
        public decimal FinishMinFeesAmount { get { return this._decFinishMinFeesAmount; } set { this._decFinishMinFeesAmount = value; } }
        public decimal MinFeesRate { get { return this._decMinFeesRate; } set { this._decMinFeesRate = value; } }
        public decimal MinAmountEUR { get { return this._decMinAmountEUR; } set { this._decMinAmountEUR = value; } }
        public string TicketFeeCurr { get { return this._sTicketFeeCurr; } set { this._sTicketFeeCurr = value; } }
        public decimal TicketFee { get { return this._decTicketFee; } set { this._decTicketFee = value; } }
        public decimal TicketFeeDiscountPercent { get { return this._decTicketFeeDiscountPercent; } set { this._decTicketFeeDiscountPercent = value; } }
        public decimal TicketFeeDiscountAmount { get { return this._decTicketFeeDiscountAmount; } set { this._decTicketFeeDiscountAmount = value; } }
        public decimal FinishTicketFee { get { return this._decFinishTicketFee; } set { this._decFinishTicketFee = value; } }
        public decimal TicketFeesRate { get { return this._decTicketFeesRate; } set { this._decTicketFeesRate = value; } }
        public decimal TicketFeesAmountEUR { get { return this._decTicketFeesAmountEUR; } set { this._decTicketFeesAmountEUR = value; } }
        public decimal FeesCalc { get { return this._decFeesCalc; } set { this._decFeesCalc = value; } }
        public decimal ProviderFees { get { return this._decProviderFees; } set { this._decProviderFees = value; } }
        public decimal RTO_FeesPercent { get { return this._decRTO_FeesPercent; } set { this._decRTO_FeesPercent = value; } }
        public decimal RTO_FeesAmount { get { return this._decRTO_FeesAmount; } set { this._decRTO_FeesAmount = value; } }
        public decimal RTO_FeesDiscountPercent { get { return this._decRTO_FeesDiscountPercent; } set { this._decRTO_FeesDiscountPercent = value; } }
        public decimal RTO_FeesDiscountAmount { get { return this._decRTO_FeesDiscountAmount; } set { this._decRTO_FeesDiscountAmount = value; } }
        public decimal RTO_FinishFeesPercent { get { return this._decRTO_FinishFeesPercent; } set { this._decRTO_FinishFeesPercent = value; } }
        public decimal RTO_FinishFeesAmount { get { return this._decRTO_FinishFeesAmount; } set { this._decRTO_FinishFeesAmount = value; } }        
        public decimal RTO_FeesAmountEUR { get { return this._decRTO_FeesAmountEUR; } set { this._decRTO_FeesAmountEUR = value; } }
        public string RTO_MinFeesCurr { get { return this._sRTO_MinFeesCurr; } set { this._sRTO_MinFeesCurr = value; } }
        public decimal RTO_MinFeesAmount { get { return this._decRTO_MinFeesAmount; } set { this._decRTO_MinFeesAmount = value; } }
        public decimal RTO_MinFeesDiscountPercent { get { return this._decRTO_MinFeesDiscountPercent; } set { this._decRTO_MinFeesDiscountPercent = value; } }
        public decimal RTO_MinFeesDiscountAmount { get { return this._decRTO_MinFeesDiscountAmount; } set { this._decRTO_MinFeesDiscountAmount = value; } }
        public decimal RTO_FinishMinFeesAmount { get { return this._decRTO_FinishMinFeesAmount; } set { this._decRTO_FinishMinFeesAmount = value; } }
        public string RTO_TicketFeeCurr { get { return this._sRTO_TicketFeeCurr; } set { this._sRTO_TicketFeeCurr = value; } }
        public decimal RTO_TicketFee { get { return this._decRTO_TicketFee; } set { this._decRTO_TicketFee = value; } }
        public decimal RTO_TicketFeeDiscountPercent { get { return this._decRTO_TicketFeeDiscountPercent; } set { this._decRTO_TicketFeeDiscountPercent = value; } }
        public decimal RTO_TicketFeeDiscountAmount { get { return this._decRTO_TicketFeeDiscountAmount; } set { this._decRTO_TicketFeeDiscountAmount = value; } }
        public decimal RTO_FinishTicketFee { get { return this._decRTO_FinishTicketFee; } set { this._decRTO_FinishTicketFee = value; } }
        public decimal RTO_FeesProVAT { get { return this._decRTO_FeesProVAT; } set { this._decRTO_FeesProVAT = value; } }
        public decimal RTO_FeesVAT { get { return this._decRTO_FeesVAT; } set { this._decRTO_FeesVAT = value; } }
        public decimal RTO_CompanyFees { get { return this._decRTO_CompanyFees; } set { this._decRTO_CompanyFees = value; } }
        public int RTO_InvoiceTitle_ID { get { return this._iRTO_InvoiceTitle_ID; } set { this._iRTO_InvoiceTitle_ID = value; } }
        public string FeesNotes { get { return this._sFeesNotes; } set { this._sFeesNotes = value; } }
        public decimal FeesMisc { get { return this._decFeesMisc; } set { this._decFeesMisc = value; } }
        public int Executor_ID { get { return this._iExecutor_ID; } set { this._iExecutor_ID = value; } }
        public string ValueDate { get { return this._sValueDate; } set { this._sValueDate = value; } }
        public decimal CurrRate { get { return this._decCurrRate; } set { this._decCurrRate = value; } }
        public decimal CompanyFeesPercent { get { return this._decCompanyFeesPercent; } set { this._decCompanyFeesPercent = value; } }
        public int SendCheck { get { return this._iSendCheck; } set { this._iSendCheck = value; } }
        public int InformationMethod_ID { get { return this._iInformationMethod_ID; } set { this._iInformationMethod_ID = value; } }
        public string OfficialInformingDate { get { return this._sOfficialInformingDate; } set { this._sOfficialInformingDate = value; } }
        public string Notes { get { return this._sNotes; } set { this._sNotes = value; } }
        public int FeesCalcMode { get { return this._iFeesCalcMode; } set { this._iFeesCalcMode = value; } }
        public string ClientName { get { return this._sClientName; } set { this._sClientName = value; } }
        public string CompanyTitle { get { return this._sCompanyTitle; } set { this._sCompanyTitle = value; } }
        public int CFP_ID { get { return this._iCFP_ID; } set { this._iCFP_ID = value; } }
        public int PackageType_ID { get { return this._iPackageType_ID; } set { this._iPackageType_ID = value; } }
        public string Package_Title { get { return this._sPackage_Title; } set { this._sPackage_Title = value; } }
        public string Product_Title { get { return this._sProduct_Title; } set { this._sProduct_Title = value; } }
        public string ProductCategory_Title { get { return this._sProductCategory_Title; } set { this._sProductCategory_Title = value; } }
        public int Security_Share_ID { get { return this._iSecurity_Share_ID; } set { this._iSecurity_Share_ID = value; } }
        public string Security_Code { get { return this._sSecurity_Code; } set { this._sSecurity_Code = value; } }
        public string Security_Code2 { get { return this._sSecurity_Code2; } set { this._sSecurity_Code2 = value; } }
        public string Security_ISIN { get { return this._sSecurity_ISIN; } set { this._sSecurity_ISIN = value; } }
        public string Security_Title { get { return this._sSecurity_Title; } set { this._sSecurity_Title = value; } }
        public DateTime Security_Date1 { get { return this._dSecurity_Date1; } set { this._dSecurity_Date1 = value; } }
        public DateTime Security_Date3 { get { return this._dSecurity_Date3; } set { this._dSecurity_Date3 = value; } }
        public decimal Security_Coupone { get { return this._decSecurity_Coupone; } set { this._decSecurity_Coupone = value; } }
        public int Security_FrequencyClipping { get { return this._iSecurity_FrequencyClipping; } set { this._iSecurity_FrequencyClipping = value; } }
        public string MainCurr { get { return this._sMainCurr; } set { this._sMainCurr = value; } }
        public int Status { get { return this._iStatus; } set { this._iStatus = value; } }
        public string ServiceProvider_Title { get { return this._sServiceProvider_Title; } set { this._sServiceProvider_Title = value; } }
        public int ServiceProviderFeesMode { get { return this._iServiceProviderFeesMode; } set { this._iServiceProviderFeesMode = value; } }
        public string Depository_Code { get { return this._sDepository_Code; } set { this._sDepository_Code = value; } }
        public string AuthorName { get { return this._sAuthorName; } set { this._sAuthorName = value; } }
        public string AdvisorName { get { return this._sAdvisorName; } set { this._sAdvisorName = value; } }
        public string RecieveTitle { get { return this._sRecieveTitle; } set { this._sRecieveTitle = value; } }
        public string InformationTitle { get { return this._sInformationTitle; } set { this._sInformationTitle = value; } }
        public string RTO_InvoiceData { get { return this._sRTO_InvoiceData; } set { this._sRTO_InvoiceData = value; } }
        public int Method_ID { get { return this._iMethod_ID; } set { this._iMethod_ID = value; } }
        public string FilePath { get { return this._sFilePath; } set { this._sFilePath = value; } }
        public string FileName { get { return this._sFileName; } set { this._sFileName = value; } }
        public int SourceCommand_ID { get { return this._iSourceCommand_ID; } set { this._iSourceCommand_ID = value; } }
        public DateTime DateFrom { get { return this._dDateFrom; } set { this._dDateFrom = value; } }
        public DateTime DateTo { get { return this._dDateTo; } set { this._dDateTo = value; } }
        public DateTime ExecDateFrom { get { return this._dExecDateFrom; } set { this._dExecDateFrom = value; } }
        public DateTime ExecDateTo { get { return this._dExecDateTo; } set { this._dExecDateTo = value; } }
        public DateTime FirstOrderDate { get { return this._dFirstOrderDate; } set { this._dFirstOrderDate = value; } }        
        public int Sent { get { return this._iSent; } set { this._iSent = value; } }
        public int User1_ID { get { return this._iUser1_ID; } set { this._iUser1_ID = value; } }
        public int User4_ID { get { return this._iUser4_ID; } set { this._iUser4_ID = value; } }
        public int Division_ID { get { return this._iDivision_ID; } set { this._iDivision_ID = value; } }
        public int ShowCancelled { get { return this._iShowCancelled; } set { this._iShowCancelled = value; } }        
        public string ExtraFilter { get { return this._sExtraFilter; } set { this._sExtraFilter = value; } }
        public string ClientOrderID { get { return this._sClientOrderID; } set { this._sClientOrderID = value; } }
        public int Actions { get { return this._iActions; } set { this._iActions = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } } 
    }
}