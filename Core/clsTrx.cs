using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsTrx
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int      _iRecord_ID;             
        private int      _iTrxType_ID;                      
        private DateTime _dTrxDate;
        private string   _sTrxJustification;
        private DateTime _dISettlementDate;
        private DateTime _dASettlementDate;
        private int      _iSingleOrder_ID;                  
        private int      _iExecutionOrder_ID;
        private string   _sExecReference_ID;
        private int      _iInvoiceType_ID;                 
        private string   _sReferenceNo;
        private string   _sD_C;
        private int      _iContract_ID;
        private int      _iContract_Details_ID;
        private int      _iContract_Packages_ID;
        private int      _iExecutionProvider_ID;                    
        private int      _iCustodian_ID;
        private string   _sTrxCurrency;
        private float    _fltTrxCurrencyRate;
        private float    _fltReverseCurrencyRate;
        private float    _fltDebitAmount_EUR;
        private float    _fltDebitAmount_Cur;
        private float    _fltCreditAmount_EUR;
        private float    _fltCreditAmount_Cur;
        private float    _fltNetDebitAmount_EUR;
        private float    _fltNetDebitAmount_Cur;
        private float    _fltNetCreditAmount_EUR;
        private float    _fltNetCreditAmount_Cur;
        private float    _fltTotalExpences_EUR;
        private float    _fltTotalExpences_Cur;
        private float    _fltAmount_EUR;
        private float    _fltAmount_Cur;
        private float    _fltNetAmount_EUR;
        private float    _fltNetAmount_Cur;
        private string   _sTrxComments;
        private int      _iShareCodes_ID;
        private float    _fltQuantity;
        private float    _fltPrice;
        private int      _iExecutionVenue_ID;
        private int      _iDepository_ID;
        private string   _sTransferCustodian;
        private string   _sTransferAccount;
        private string   _sTransferAccountName;
        private float    _fltAccruals_EUR;
        private float    _fltAccruals_Cur;
        private float    _fltExecFee_EUR;
        private float    _fltExecFee_Cur;
        private float    _fltExecFeeReturn_EUR;
        private float    _fltExecFeeReturn_Cur;
        private float    _fltExecFeeIncome_EUR;
        private float    _fltExecFeeIncome_Cur;
        private float    _fltSettleFee_EUR;
        private float    _fltSettleFee_Cur;
        private float    _fltSettleFeeReturn_EUR;
        private float    _fltSettleFeeReturn_Cur;
        private float    _fltSettleFeeIncome_EUR;
        private float    _fltSettleFeeIncome_Cur;
        private float    _fltATHEXTransferFee_EUR;
        private float    _fltATHEXTransferFee_Cur;
        private float    _fltATHEXExpences_EUR;
        private float    _fltATHEXExpences_Cur;
        private float    _fltATHEXFileExpences_EUR;
        private float    _fltATHEXFileExpences_Cur;
        private float    _fltStockXFee_EUR;
        private float    _fltStockXFee_Cur;
        private float    _fltPriSecExecFeesReturn_EUR;
        private float    _fltPriSecExecFeesReturn_Cur;
        private float    _fltPriSecSettleFeesReturn_EUR;
        private float    _fltPriSecSettleFeesReturn_Cur;
        private float    _fltManagementFee_EUR;
        private float    _fltManagementFee_Cur;
        private float    _fltManagementFeeIncome_EUR;
        private float    _fltManagementFeeIncome_Cur;
        private float    _fltSafekeepingFee_EUR;
        private float    _fltSafekeepingFee_Cur;
        private float    _fltSafekeepingFeeIncome_EUR;
        private float    _fltSafekeepingFeeIncome_Cur;
        private float    _fltPerformanceFee_EUR;
        private float    _fltPerformanceFee_Cur;
        private float    _fltPerformanceFeeIncome_EUR;
        private float    _fltPerformanceFeeIncome_Cur;
        private float    _fltSupportFee_EUR;
        private float    _fltSupportFee_Cur;
        private float    _fltSupportFeeIncome_EUR;
        private float    _fltSupportFeeIncome_Cur;
        private float    _fltFxFee_EUR;
        private float    _fltFxFee_Cur;
        private float    _fltCorpActionFee_EUR;
        private float    _fltCorpActionFee_Cur;
        private float    _fltSecTransferFee_EUR;
        private float    _fltSecTransferFee_Cur;
        private float    _fltSecTransferFeeReturn_EUR;
        private float    _fltSecTransferFeeReturn_Cur;
        private float    _fltSecTransferFeeIncome_EUR;
        private float    _fltSecTransferFeeIncome_Cur;
        private float    _fltCashTransferFee_EUR;
        private float    _fltCashTransferFee_Cur;
        private float    _fltCashTransferFeeReturn_EUR;
        private float    _fltCashTransferFeeReturn_Cur;
        private float    _fltCashTransferFeeIncome_EUR;
        private float    _fltCashTransferFeeIncome_Cur;
        private float    _fltTaxExpencesAbroad_EUR;
        private float    _fltTaxExpencesAbroad_Cur;
        private float    _fltSalesTax_EUR;
        private float    _fltSalesTax_Cur;
        private float    _fltVAT_EUR;
        private float    _fltVAT_Cur;
        private float    _fltWHTax_EUR;
        private float    _fltWHTax_Cur;
        private float    _fltGRTax_EUR;
        private float    _fltGRTax_Cur;
        private int      _iEntryUser_ID;
        private DateTime _dEntryDate;
        private int      _iStatus;

        private int      _iContractTipos;
        private string   _sContractTitle;
        private string   _sContractCode;
        private string   _sContractPortfolio;
        private DateTime _dContractDateStart;
        private DateTime _dContractDateFinish;
        private string   _sContractCurrency;
        private int      _iClientTipos;
        private string   _sClientSurname;
        private string   _sClientFirstname;
        private int      _iAdvisor_ID;
        private string   _sAdvisor_Fullname;
        private int      _iRM_ID;
        private string   _sRM_Fullname;
        private int      _iDiax_ID;
        private string   _sDiax_Fullname;
        private string   _sProduct_Title;
        private int      _iProductType_ID;
        private string   _sProductType_Title;
        private int      _iProductCategory_ID;
        private string   _sProductCategory_Title;
        private string   _sService_Title;
        private string   _sProfile_Title;
        private string   _sPolicy_Title;
        private string   _sExecutionVenue_Title;
        private string   _sProvider_Title;
        private string   _sShareCodes_Title;
        private string   _sISIN;
        private string   _sShareCodes_Code;
        private string   _sShareCodes_Code2;
        private string   _sShareCodes_Currency;
        private int      _iShareCodes_SE_ID;
        private DateTime _dDateFrom;
        private DateTime _dDateTo;

        private DataTable _dtList;

        public clsTrx()
        {
            this._iRecord_ID = 0;
            this._iTrxType_ID = 0;                      
            this._dTrxDate = Convert.ToDateTime("1900/01/01");
            this._sTrxJustification = "";
            this._dISettlementDate = Convert.ToDateTime("1900/01/01");
            this._dASettlementDate = Convert.ToDateTime("1900/01/01");
            this._iSingleOrder_ID = 0;                   
            this._iExecutionOrder_ID = 0;
            this._sExecReference_ID = "";
            this._iInvoiceType_ID = 0;                      
            this._sReferenceNo = "";
            this._sD_C = "";
            this._iContract_ID = 0;
            this._iContract_Details_ID = 0;
            this._iContract_Packages_ID = 0;
            this._iExecutionProvider_ID = 0;                     
            this._iCustodian_ID = 0;
            this._sTrxCurrency = "";
            this._fltTrxCurrencyRate = 0;
            this._fltReverseCurrencyRate = 0;
            this._fltDebitAmount_EUR = 0;
            this._fltDebitAmount_Cur = 0;
            this._fltCreditAmount_EUR = 0;
            this._fltCreditAmount_Cur = 0;
            this._fltNetDebitAmount_EUR = 0;
            this._fltNetDebitAmount_Cur = 0;
            this._fltNetCreditAmount_EUR = 0;
            this._fltNetCreditAmount_Cur = 0;
            this._fltTotalExpences_EUR = 0;
            this._fltTotalExpences_Cur = 0;
            this._fltAmount_EUR = 0;
            this._fltAmount_Cur = 0;
            this._fltNetAmount_EUR = 0;
            this._fltNetAmount_Cur = 0;
            this._sTrxComments = "";
            this._iShareCodes_ID = 0;
            this._fltQuantity = 0;
            this._fltPrice = 0;
            this._iExecutionVenue_ID = 0;
            this._iDepository_ID = 0;
            this._sTransferCustodian = "";
            this._sTransferAccount = "";
            this._sTransferAccountName = "";
            this._fltAccruals_EUR = 0;
            this._fltAccruals_Cur = 0;
            this._fltExecFee_EUR = 0;
            this._fltExecFee_Cur = 0;
            this._fltExecFeeReturn_EUR = 0;
            this._fltExecFeeReturn_Cur = 0;
            this._fltExecFeeIncome_EUR = 0;
            this._fltExecFeeIncome_Cur = 0;
            this._fltSettleFee_EUR = 0;
            this._fltSettleFee_Cur = 0;
            this._fltSettleFeeReturn_EUR = 0;
            this._fltSettleFeeReturn_Cur = 0;
            this._fltSettleFeeIncome_EUR = 0;
            this._fltSettleFeeIncome_Cur = 0;
            this._fltATHEXTransferFee_EUR = 0;
            this._fltATHEXTransferFee_Cur = 0;
            this._fltATHEXExpences_EUR = 0;
            this._fltATHEXExpences_Cur = 0;
            this._fltATHEXFileExpences_EUR = 0;
            this._fltATHEXFileExpences_Cur = 0;
            this._fltStockXFee_EUR = 0;
            this._fltStockXFee_Cur = 0;
            this._fltPriSecExecFeesReturn_EUR = 0;
            this._fltPriSecExecFeesReturn_Cur = 0;
            this._fltPriSecSettleFeesReturn_EUR = 0;
            this._fltPriSecSettleFeesReturn_Cur = 0;
            this._fltManagementFee_EUR = 0;
            this._fltManagementFee_Cur = 0;
            this._fltManagementFeeIncome_EUR = 0;
            this._fltManagementFeeIncome_Cur = 0;
            this._fltSafekeepingFee_EUR = 0;
            this._fltSafekeepingFee_Cur = 0;
            this._fltSafekeepingFeeIncome_EUR = 0;
            this._fltSafekeepingFeeIncome_Cur = 0;
            this._fltPerformanceFee_EUR = 0;
            this._fltPerformanceFee_Cur = 0;
            this._fltPerformanceFeeIncome_EUR = 0;
            this._fltPerformanceFeeIncome_Cur = 0;
            this._fltSupportFee_EUR = 0;
            this._fltSupportFee_Cur = 0;
            this._fltSupportFeeIncome_EUR = 0;
            this._fltSupportFeeIncome_Cur = 0;
            this._fltFxFee_EUR = 0;
            this._fltFxFee_Cur = 0;
            this._fltCorpActionFee_EUR = 0;
            this._fltCorpActionFee_Cur = 0;
            this._fltSecTransferFee_EUR = 0;
            this._fltSecTransferFee_Cur = 0;
            this._fltSecTransferFeeReturn_EUR = 0;
            this._fltSecTransferFeeReturn_Cur = 0;
            this._fltSecTransferFeeIncome_EUR = 0;
            this._fltSecTransferFeeIncome_Cur = 0;
            this._fltCashTransferFee_EUR = 0;
            this._fltCashTransferFee_Cur = 0;
            this._fltCashTransferFeeReturn_EUR = 0;
            this._fltCashTransferFeeReturn_Cur = 0;
            this._fltCashTransferFeeIncome_EUR = 0;
            this._fltCashTransferFeeIncome_Cur = 0;
            this._fltTaxExpencesAbroad_EUR = 0;
            this._fltTaxExpencesAbroad_Cur = 0;
            this._fltSalesTax_EUR = 0;
            this._fltSalesTax_Cur = 0;
            this._fltVAT_EUR = 0;
            this._fltVAT_Cur = 0;
            this._fltWHTax_EUR = 0;
            this._fltWHTax_Cur = 0;
            this._fltGRTax_EUR = 0;
            this._fltGRTax_Cur = 0;
            this._iEntryUser_ID = 0;
            this._dEntryDate = Convert.ToDateTime("1900/01/01");
            this._iStatus = 0;

            this._iContractTipos = 0;
            this._sContractTitle = "";
            this._sContractCode = "";
            this._sContractPortfolio = "";
            this._dContractDateStart = Convert.ToDateTime("1900/01/01");
            this._dContractDateFinish = Convert.ToDateTime("1900/01/01");
            this._sContractCurrency = "";
            this._iClientTipos = 0;
            this._sClientSurname = "";
            this._sClientFirstname = "";
            this._iAdvisor_ID = 0;
            this._sAdvisor_Fullname = "";
            this._iRM_ID = 0;
            this._sRM_Fullname = "";
            this._iDiax_ID = 0;
            this._sDiax_Fullname = "";
            this._sProduct_Title = "";
            this._iProductType_ID = 0;
            this._sProductType_Title = "";
            this._iProductCategory_ID = 0;
            this._sProductCategory_Title = "";
            this._sService_Title = "";
            this._sProfile_Title = "";
            this._sPolicy_Title = "";
            this._sExecutionVenue_Title = "";
            this._sProvider_Title = "";
            this._sShareCodes_Title = "";
            this._sISIN = "";
            this._sShareCodes_Code = "";
            this._sShareCodes_Code2 = "";
            this._sShareCodes_Currency = "";
            this._iShareCodes_SE_ID = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTrx", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iTrxType_ID = Convert.ToInt32(drList["TrxType_ID"]);
                    this._dTrxDate = Convert.ToDateTime(drList["TrxDate"]);
                    this._sTrxJustification = drList["TrxJustification"] + "";
                    this._dISettlementDate = Convert.ToDateTime(drList["ISettlementDate"]);
                    this._dASettlementDate = Convert.ToDateTime(drList["ASettlementDate"]);
                    this._iSingleOrder_ID = Convert.ToInt32(drList["SingleOrder_ID"]);
                    this._iExecutionOrder_ID = Convert.ToInt32(drList["ExecutionOrder_ID"]);
                    this._sExecReference_ID = drList["ExecReference_ID"] + "";
                    this._iInvoiceType_ID = Convert.ToInt32(drList["InvoiceType_ID"]);
                    this._sReferenceNo = drList["ReferenceNo"] + "";
                    this._sD_C = drList["D_C"] + "";
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iContract_Details_ID = Convert.ToInt32(drList["Contract_Details_ID"]);
                    this._iContract_Packages_ID = Convert.ToInt32(drList["Contract_Packages_ID"]);
                    this._iExecutionProvider_ID = Convert.ToInt32(drList["ExecutionProvider_ID"]);
                    this._iCustodian_ID = Convert.ToInt32(drList["Custodian_ID"]);
                    this._sTrxCurrency = drList["TrxCurrency"] + "";
                    this._fltTrxCurrencyRate = Convert.ToSingle(drList["TrxCurrencyRate"]);
                    if (this._fltTrxCurrencyRate != 0) this._fltReverseCurrencyRate = 1 / this._fltTrxCurrencyRate;
                    else this._fltReverseCurrencyRate = 0;
                    this._fltDebitAmount_EUR = 0;
                    this._fltDebitAmount_Cur = 0;
                    this._fltCreditAmount_EUR = 0;
                    this._fltCreditAmount_Cur = 0;
                    this._fltNetDebitAmount_EUR = 0;
                    this._fltNetDebitAmount_Cur = 0;
                    this._fltNetCreditAmount_EUR = 0;
                    this._fltNetCreditAmount_Cur = 0;
                    this._fltTotalExpences_EUR = 0;
                    this._fltTotalExpences_Cur = 0;
                    this._fltAmount_EUR = 0;
                    this._fltAmount_Cur = 0;
                    this._fltNetAmount_EUR = 0;
                    this._fltNetAmount_Cur = 0;
                    this._sTrxComments = drList["TrxComments"] + "";
                    this._iShareCodes_ID = Convert.ToInt32(drList["ShareCodes_ID"]);
                    this._fltPrice = Convert.ToSingle(drList["Price"]);
                    this._fltQuantity = Convert.ToSingle(drList["Quantity"]);
                    this._iExecutionVenue_ID = Convert.ToInt32(drList["ExecutionVenue_ID"]);
                    this._iDepository_ID = Convert.ToInt32(drList["Depository_ID"]);
                    this._sTransferCustodian = drList["TransferCustodian"] + "";
                    this._sTransferAccount = drList["TransferAccount"] + "";
                    this._sTransferAccountName = drList["TransferAccountName"] + "";
                    this._fltAccruals_EUR = 0;
                    this._fltAccruals_Cur = 0;
                    this._fltExecFee_EUR = 0;
                    this._fltExecFee_Cur = 0;
                    this._fltExecFeeReturn_EUR = 0;
                    this._fltExecFeeReturn_Cur = 0;
                    this._fltExecFeeIncome_EUR = 0;
                    this._fltExecFeeIncome_Cur = 0;
                    this._fltSettleFee_EUR = 0;
                    this._fltSettleFee_Cur = 0;
                    this._fltSettleFeeReturn_EUR = 0;
                    this._fltSettleFeeReturn_Cur = 0;
                    this._fltSettleFeeIncome_EUR = 0;
                    this._fltSettleFeeIncome_Cur = 0;
                    this._fltATHEXTransferFee_EUR = 0;
                    this._fltATHEXTransferFee_Cur = 0;
                    this._fltATHEXExpences_EUR = 0;
                    this._fltATHEXExpences_Cur = 0;
                    this._fltATHEXFileExpences_EUR = 0;
                    this._fltATHEXFileExpences_Cur = 0;
                    this._fltStockXFee_EUR = 0;
                    this._fltStockXFee_Cur = 0;
                    this._fltPriSecExecFeesReturn_EUR = 0;
                    this._fltPriSecExecFeesReturn_Cur = 0;
                    this._fltPriSecSettleFeesReturn_EUR = 0;
                    this._fltPriSecSettleFeesReturn_Cur = 0;
                    this._fltManagementFee_EUR = 0;
                    this._fltManagementFee_Cur = 0;
                    this._fltManagementFeeIncome_EUR = 0;
                    this._fltManagementFeeIncome_Cur = 0;
                    this._fltSafekeepingFee_EUR = 0;
                    this._fltSafekeepingFee_Cur = 0;
                    this._fltSafekeepingFeeIncome_EUR = 0;
                    this._fltSafekeepingFeeIncome_Cur = 0;
                    this._fltPerformanceFee_EUR = 0;
                    this._fltPerformanceFee_Cur = 0;
                    this._fltPerformanceFeeIncome_EUR = 0;
                    this._fltPerformanceFeeIncome_Cur = 0;
                    this._fltSupportFee_EUR = 0;
                    this._fltSupportFee_Cur = 0;
                    this._fltSupportFeeIncome_EUR = 0;
                    this._fltSupportFeeIncome_Cur = 0;
                    this._fltFxFee_EUR = 0;
                    this._fltFxFee_Cur = 0;
                    this._fltCorpActionFee_EUR = 0;
                    this._fltCorpActionFee_Cur = 0;
                    this._fltSecTransferFee_EUR = 0;
                    this._fltSecTransferFee_Cur = 0;
                    this._fltSecTransferFeeReturn_EUR = 0;
                    this._fltSecTransferFeeReturn_Cur = 0;
                    this._fltSecTransferFeeIncome_EUR = 0;
                    this._fltSecTransferFeeIncome_Cur = 0;
                    this._fltCashTransferFee_EUR = 0;
                    this._fltCashTransferFee_Cur = 0;
                    this._fltCashTransferFeeReturn_EUR = 0;
                    this._fltCashTransferFeeReturn_Cur = 0;
                    this._fltCashTransferFeeIncome_EUR = 0;
                    this._fltCashTransferFeeIncome_Cur = 0;
                    this._fltTaxExpencesAbroad_EUR = 0;
                    this._fltTaxExpencesAbroad_Cur = 0;
                    this._fltSalesTax_EUR = 0;
                    this._fltSalesTax_Cur = 0;
                    this._fltVAT_EUR = 0;
                    this._fltVAT_Cur = 0;
                    this._fltWHTax_EUR = 0;
                    this._fltWHTax_Cur = 0;
                    this._fltGRTax_EUR = 0;
                    this._fltGRTax_Cur = 0;
                    this._iEntryUser_ID = Convert.ToInt32(drList["EntryUser_ID"]);
                    this._dEntryDate = Convert.ToDateTime(drList["EntryDate"]);
                    this._iStatus = Convert.ToInt32(drList["Status"]);

                    this._iContractTipos = Convert.ToInt32(drList["ContractTipos"]);
                    this._sContractTitle = drList["ContractTitle"] + "";
                    this._sContractCode = drList["Code"] + "";
                    this._sContractPortfolio = drList["sContractPortfolio"] + "";
                    this._dContractDateStart = Convert.ToDateTime("1900/01/01");
                    this._dContractDateFinish = Convert.ToDateTime("1900/01/01");
                    this._sContractCurrency = "";
                    this._iClientTipos = 0;
                    this._sClientSurname = "";
                    this._sClientFirstname = "";
                    this._iAdvisor_ID = 0;
                    this._sAdvisor_Fullname = (drList["Advisor_Surname"] + " " + drList["Advisor_Firstname"]).Trim();
                    this._iRM_ID = 0;
                    this._sRM_Fullname = (drList["RM_Surname"] + " " + drList["RM_Firstname"]).Trim();
                    this._iDiax_ID = 0;
                    this._sDiax_Fullname = (drList["Diax_Surname"] + " " + drList["Diax_Firstname"]).Trim();
                    this._sProduct_Title = "";
                    this._iProductType_ID = Convert.ToInt32(drList["Product_ID"]);
                    this._sProductType_Title = drList["ProductTitle"] + "";
                    this._iProductCategory_ID = Convert.ToInt32(drList["ProductCategory_ID"]);
                    this._sProductCategory_Title = drList["ProductCategories_Title"] + "";
                    this._sService_Title = "";
                    this._sProfile_Title = "";
                    this._sPolicy_Title = "";
                    this._sExecutionVenue_Title = "";
                    this._sProvider_Title = "";
                    this._sShareCodes_Title = drList["Share_Title"] + "";
                    this._sISIN = drList["ISIN"] + "";
                    this._sShareCodes_Code = drList["ShareCode"] + "";
                    this._sShareCodes_Code2 = drList["ShareCode2"] + "";
                    this._sShareCodes_Currency = "";
                    this._iShareCodes_SE_ID = 0;
                }
                drList.Close();
            }

            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            string[] sStatus = { "-", "Εκκρεμή", "Ολοκληρωμένο" };
            try
            {
                _dtList = new DataTable("Trx_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("TrxDate_Date", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("TrxDate_Time", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Status_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("TrxType_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("TrxJustification", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ISettlementDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ASettlementDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SingleOrder_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ExecutionOrder_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ExecReference_ID", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ReferenceNo", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("D_C", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ContractCode", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ContractPortfolio", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ExecutionProvider_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Custodian_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("TrxCurrency", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("TrxCurrencyRate", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ReverseCurrencyRate", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("DebitAmount_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("DebitAmount_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("CreditAmount_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("CreditAmount_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("NetDebitAmount_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("NetDebitAmount_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("NetCreditAmount_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("NetCreditAmount_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("TotalExpences_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("TotalExpences_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Amount_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Amount_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("NetAmount_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("NetAmount_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("TrxComments", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ISIN", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ShareCodes_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ShareCodes_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ShareCodes_Code2", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Product_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProductCategory_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Quantity", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Price", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ExecutionVenue_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ExecutionVenue_MIC", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Depository_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Depository_BIC", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("TransferCustodian", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("TransferAccount", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("TransferAccountName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Advisor_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RM_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Diax_Fullname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Profile_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Policy_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Service_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Accruals_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Accruals_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ExecFee_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ExecFee_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ExecFeeReturn_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ExecFeeReturn_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ExecFeeIncome_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ExecFeeIncome_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SettleFee_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SettleFee_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SettleFeeReturn_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SettleFeeReturn_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SettleFeeIncome_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SettleFeeIncome_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ATHEXTransferFee_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ATHEXTransferFee_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ATHEXExpences_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ATHEXExpences_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ATHEXFileExpences_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ATHEXFileExpences_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("StockXFee_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("StockXFee_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("PriSecExecFeesReturn_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("PriSecExecFeesReturn_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("PriSecSettleFeesReturn_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("PriSecSettleFeesReturn_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ManagementFee_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ManagementFee_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ManagementFeeIncome_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ManagementFeeIncome_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SafekeepingFee_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SafekeepingFee_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SafekeepingFeeIncome_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SafekeepingFeeIncome_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("PerformanceFee_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("PerformanceFee_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("PerformanceFeeIncome_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("PerformanceFeeIncome_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SupportFee_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SupportFee_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SupportFeeIncome_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SupportFeeIncome_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FxFee_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FxFee_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("CorpActionFee_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("CorpActionFee_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SecTransferFee_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SecTransferFee_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SecTransferFeeReturn_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SecTransferFeeReturn_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SecTransferFeeIncome_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SecTransferFeeIncome_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("CashTransferFee_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("CashTransferFee_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("CashTransferFeeReturn_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("CashTransferFeeReturn_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("CashTransferFeeIncome_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("CashTransferFeeIncome_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("TaxExpencesAbroad_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("TaxExpencesAbroad_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SalesTax_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SalesTax_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("VAT_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("VAT_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("WHTax_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("WHTax_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("GRTax_EUR", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("GRTax_Cur", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("EntryDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("TrxType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("InvoiceType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Details_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Packages_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ContractTipos", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("ExecutionProvider_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ShareCodes_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ProductCategory_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ExecutionVenue_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Depository_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Custodian_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("EntryUser_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetTrx_List", conn);
                cmd.CommandTimeout = 6000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dDateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dDateTo));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["TrxDate_Date"] = Convert.ToDateTime(drList["TrxDate"]).ToString("dd/MM/yyyy");
                    this.dtRow["TrxDate_Time"] = Convert.ToDateTime(drList["TrxDate"]).ToString("hh:mm:ss");
                    this.dtRow["Status_Title"] = sStatus[Convert.ToInt32(drList["Status"])];
                    this.dtRow["ReferenceNo"] = drList["ReferenceNo"];
                    this.dtRow["TrxType_Title"] = drList["TrxType_Title"];
                    this.dtRow["TrxJustification"] = drList["TrxJustification"];
                    this.dtRow["ISettlementDate"] = Convert.ToDateTime(drList["ISettlementDate"]).ToString("dd/MM/yyyy");
                    this.dtRow["ASettlementDate"] = Convert.ToDateTime(drList["ASettlementDate"]).ToString("dd/MM/yyyy");
                    this.dtRow["ContractCode"] = drList["ContractCode"];
                    this.dtRow["ContractPortfolio"] = drList["ContractPortfolio"];
                    this.dtRow["ContractTitle"] = drList["ContractTitle"];
                    this.dtRow["SingleOrder_ID"] = drList["SingleOrder_ID"];
                    this.dtRow["ExecutionOrder_ID"] = drList["ExecutionOrder_ID"];
                    this.dtRow["ExecReference_ID"] = drList["ExecReference_ID"];
                    this.dtRow["D_C"] = drList["D_C"];
                    this.dtRow["ExecutionProvider_Title"] = drList["ExecutionProvider_Title"];
                    this.dtRow["Custodian_Title"] = drList["Custodian_Title"];
                    this.dtRow["TrxCurrency"] = drList["TrxCurrency"];
                    this.dtRow["TrxCurrencyRate"] = drList["TrxCurrencyRate"];
                    this.dtRow["ReverseCurrencyRate"] = drList["ReverseCurrencyRate"];
                    this.dtRow["DebitAmount_EUR"] = drList["DebitAmount_EUR"];
                    this.dtRow["DebitAmount_Cur"] = drList["DebitAmount_Cur"];
                    this.dtRow["CreditAmount_EUR"] = drList["CreditAmount_EUR"];
                    this.dtRow["CreditAmount_Cur"] = drList["CreditAmount_Cur"];
                    this.dtRow["NetDebitAmount_EUR"] = drList["NetDebitAmount_EUR"];
                    this.dtRow["NetDebitAmount_Cur"] = drList["NetDebitAmount_Cur"];
                    this.dtRow["NetCreditAmount_EUR"] = drList["NetCreditAmount_EUR"];
                    this.dtRow["NetCreditAmount_Cur"] = drList["NetCreditAmount_Cur"];
                    this.dtRow["TotalExpences_EUR"] = drList["TotalExpences_EUR"];
                    this.dtRow["TotalExpences_Cur"] = drList["TotalExpences_Cur"];
                    this.dtRow["Amount_EUR"] = drList["Amount_EUR"];
                    this.dtRow["Amount_Cur"] = drList["Amount_Cur"];
                    this.dtRow["NetAmount_EUR"] = drList["NetAmount_EUR"];
                    this.dtRow["NetAmount_Cur"] = drList["NetAmount_Cur"];
                    this.dtRow["TrxComments"] = drList["TrxComments"];
                    if (Convert.ToInt32(drList["ShareCodes_ID"]) != 0)
                    {
                        this.dtRow["ShareCodes_ID"] = drList["ShareCodes_ID"];
                        this.dtRow["ShareCodes_Title"] = drList["ShareCodes_Title"] + "";
                        this.dtRow["Product_ID"] = drList["Product_ID"];
                        this.dtRow["Product_Title"] = drList["Product_Title"] + "";
                        this.dtRow["ProductCategory_ID"] = drList["ProductCategory_ID"];
                        this.dtRow["ProductCategory_Title"] = drList["ProductCategory_Title"] + "";
                        this.dtRow["ShareCodes_Code"] = drList["ShareCodes_Code"] + "";
                        this.dtRow["ShareCodes_Code2"] = drList["ShareCodes_Code2"] + "";
                        this.dtRow["ISIN"] = drList["ISIN"] + "";
                    }
                    else
                    {
                        this.dtRow["ShareCodes_ID"] = 0;
                        this.dtRow["ShareCodes_Title"] = "";
                        this.dtRow["Product_ID"] = 0;
                        this.dtRow["Product_Title"] = "";
                        this.dtRow["ProductCategory_ID"] = 0;
                        this.dtRow["ProductCategory_Title"] = "";
                        this.dtRow["ShareCodes_Code"] = "";
                        this.dtRow["ShareCodes_Code2"] = "";
                        this.dtRow["ISIN"] = "";
                    }
                    this.dtRow["Quantity"] = drList["Quantity"];
                    this.dtRow["Price"] = drList["Price"];
                    this.dtRow["ExecutionVenue_MIC"] = drList["ExecutionVenue_MIC"] + "";
                    this.dtRow["ExecutionVenue_Title"] = drList["ExecutionVenue_Title"] + "";
                    this.dtRow["Depository_Title"] = drList["Depository_Title"] + "";
                    this.dtRow["Depository_BIC"] = drList["Depository_BIC"] + "";
                    this.dtRow["TransferCustodian"] = drList["TransferCustodian"] + "";
                    this.dtRow["TransferAccount"] = drList["TransferAccount"] + "";
                    this.dtRow["TransferAccountName"] = drList["TransferAccountName"] + "";
                    this.dtRow["Advisor_Fullname"] = (drList["Advisor_Surname"] + " " + drList["Advisor_Firstname"]).Trim();
                    this.dtRow["RM_Fullname"] = (drList["RM_Surname"] + " " + drList["RM_Firstname"]).Trim();
                    this.dtRow["Diax_Fullname"] = (drList["Diax_Surname"] + " " + drList["Diax_Firstname"]).Trim();
                    this.dtRow["Profile_Title"] = drList["Profile_Title"] + "";
                    this.dtRow["Policy_Title"] = drList["Policy_Title"] + "";
                    this.dtRow["Service_Title"] = drList["Service_Title"] + "";
                    this.dtRow["Accruals_EUR"] = drList["Accruals_EUR"];
                    this.dtRow["Accruals_Cur"] = drList["Accruals_Cur"];
                    this.dtRow["ExecFee_EUR"] = drList["ExecFee_EUR"];
                    this.dtRow["ExecFee_Cur"] = drList["ExecFee_Cur"];
                    this.dtRow["ExecFeeReturn_EUR"] = drList["ExecFeeReturn_EUR"];
                    this.dtRow["ExecFeeReturn_Cur"] = drList["ExecFeeReturn_Cur"];
                    this.dtRow["ExecFeeIncome_EUR"] = drList["ExecFeeIncome_EUR"];
                    this.dtRow["ExecFeeIncome_Cur"] = drList["ExecFeeIncome_Cur"];
                    this.dtRow["SettleFee_EUR"] = drList["SettleFee_EUR"];
                    this.dtRow["SettleFee_Cur"] = drList["SettleFee_Cur"];
                    this.dtRow["SettleFeeReturn_EUR"] = drList["SettleFeeReturn_EUR"];
                    this.dtRow["SettleFeeReturn_Cur"] = drList["SettleFeeReturn_Cur"];
                    this.dtRow["SettleFeeIncome_EUR"] = drList["SettleFeeIncome_EUR"];
                    this.dtRow["SettleFeeIncome_Cur"] = drList["SettleFeeIncome_Cur"];
                    this.dtRow["ATHEXTransferFee_EUR"] = drList["ATHEXTransferFee_EUR"];
                    this.dtRow["ATHEXTransferFee_Cur"] = drList["ATHEXTransferFee_Cur"];
                    this.dtRow["ATHEXExpences_EUR"] = drList["ATHEXExpences_EUR"];
                    this.dtRow["ATHEXExpences_Cur"] = drList["ATHEXExpences_Cur"];
                    this.dtRow["ATHEXFileExpences_EUR"] = drList["ATHEXFileExpences_EUR"];
                    this.dtRow["ATHEXFileExpences_Cur"] = drList["ATHEXFileExpences_Cur"];
                    this.dtRow["StockXFee_EUR"] = drList["StockXFee_EUR"];
                    this.dtRow["StockXFee_Cur"] = drList["StockXFee_Cur"];
                    this.dtRow["PriSecExecFeesReturn_EUR"] = drList["PriSecExecFeesReturn_EUR"];
                    this.dtRow["PriSecExecFeesReturn_Cur"] = drList["PriSecExecFeesReturn_Cur"];
                    this.dtRow["PriSecSettleFeesReturn_EUR"] = drList["PriSecSettleFeesReturn_EUR"];
                    this.dtRow["PriSecSettleFeesReturn_Cur"] = drList["PriSecSettleFeesReturn_Cur"];
                    this.dtRow["ManagementFee_EUR"] = drList["ManagementFee_EUR"];
                    this.dtRow["ManagementFee_Cur"] = drList["ManagementFee_Cur"];
                    this.dtRow["ManagementFeeIncome_EUR"] = drList["ManagementFeeIncome_EUR"];
                    this.dtRow["ManagementFeeIncome_Cur"] = drList["ManagementFeeIncome_Cur"];
                    this.dtRow["SafekeepingFee_EUR"] = drList["SafekeepingFee_EUR"];
                    this.dtRow["SafekeepingFee_Cur"] = drList["SafekeepingFee_Cur"];
                    this.dtRow["SafekeepingFeeIncome_EUR"] = drList["SafekeepingFeeIncome_EUR"];
                    this.dtRow["SafekeepingFeeIncome_Cur"] = drList["SafekeepingFeeIncome_Cur"];
                    this.dtRow["PerformanceFee_EUR"] = drList["PerformanceFee_EUR"];
                    this.dtRow["PerformanceFee_Cur"] = drList["PerformanceFee_Cur"];
                    this.dtRow["PerformanceFeeIncome_EUR"] = drList["PerformanceFeeIncome_EUR"];
                    this.dtRow["PerformanceFeeIncome_Cur"] = drList["PerformanceFeeIncome_Cur"];
                    this.dtRow["SupportFee_EUR"] = drList["SupportFee_EUR"];
                    this.dtRow["SupportFee_Cur"] = drList["SupportFee_Cur"];
                    this.dtRow["SupportFeeIncome_EUR"] = drList["SupportFeeIncome_EUR"];
                    this.dtRow["SupportFeeIncome_Cur"] = drList["SupportFeeIncome_Cur"];
                    this.dtRow["FxFee_EUR"] = drList["FxFee_EUR"];
                    this.dtRow["FxFee_Cur"] = drList["FxFee_Cur"];
                    this.dtRow["CorpActionFee_EUR"] = drList["CorpActionFee_EUR"];
                    this.dtRow["CorpActionFee_Cur"] = drList["CorpActionFee_Cur"];
                    this.dtRow["SecTransferFee_EUR"] = drList["SecTransferFee_EUR"];
                    this.dtRow["SecTransferFee_Cur"] = drList["SecTransferFee_Cur"];
                    this.dtRow["SecTransferFeeReturn_EUR"] = drList["SecTransferFeeReturn_EUR"];
                    this.dtRow["SecTransferFeeReturn_Cur"] = drList["SecTransferFeeReturn_Cur"];
                    this.dtRow["SecTransferFeeIncome_EUR"] = drList["SecTransferFeeIncome_EUR"];
                    this.dtRow["SecTransferFeeIncome_Cur"] = drList["SecTransferFeeIncome_Cur"];
                    this.dtRow["CashTransferFee_EUR"] = drList["CashTransferFee_EUR"];
                    this.dtRow["CashTransferFee_Cur"] = drList["CashTransferFee_Cur"];
                    this.dtRow["CashTransferFeeReturn_EUR"] = drList["CashTransferFeeReturn_EUR"];
                    this.dtRow["CashTransferFeeReturn_Cur"] = drList["CashTransferFeeReturn_Cur"];
                    this.dtRow["CashTransferFeeIncome_EUR"] = drList["CashTransferFeeIncome_EUR"];
                    this.dtRow["CashTransferFeeIncome_Cur"] = drList["CashTransferFeeIncome_Cur"];
                    this.dtRow["TaxExpencesAbroad_EUR"] = drList["TaxExpencesAbroad_EUR"];
                    this.dtRow["TaxExpencesAbroad_Cur"] = drList["TaxExpencesAbroad_Cur"];
                    this.dtRow["SalesTax_EUR"] = drList["SalesTax_EUR"];
                    this.dtRow["SalesTax_Cur"] = drList["SalesTax_Cur"];
                    this.dtRow["VAT_EUR"] = drList["VAT_EUR"];
                    this.dtRow["VAT_Cur"] = drList["VAT_Cur"];
                    this.dtRow["WHTax_EUR"] = drList["WHTax_EUR"];
                    this.dtRow["WHTax_Cur"] = drList["WHTax_Cur"];
                    this.dtRow["GRTax_EUR"] = drList["GRTax_EUR"];
                    this.dtRow["GRTax_Cur"] = drList["GRTax_Cur"];
                    this.dtRow["EntryDate"] = drList["EntryDate"];
                    this.dtRow["TrxType_ID"] = drList["TrxType_ID"];
                    this.dtRow["InvoiceType_ID"] = drList["InvoiceType_ID"];
                    this.dtRow["Contract_ID"] = drList["Contract_ID"];
                    this.dtRow["Contract_Details_ID"] = drList["Contract_Details_ID"];
                    this.dtRow["Contract_Packages_ID"] = drList["Contract_Packages_ID"];
                    this.dtRow["ContractTipos"] = drList["ContractTipos"];
                    this.dtRow["Status"] = drList["Status"];
                    this.dtRow["ExecutionProvider_ID"] = drList["ExecutionProvider_ID"];
                    this.dtRow["ExecutionVenue_ID"] = drList["ExecutionVenue_ID"];
                    this.dtRow["Depository_ID"] = drList["Depository_ID"];
                    this.dtRow["Custodian_ID"] = drList["Custodian_ID"];
                    this.dtRow["EntryUser_ID"] = drList["EntryUser_ID"];
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

        public int InsertRecord()
        {
            _iRecord_ID = 0;
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("InsertTrx", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@TrxType_ID", SqlDbType.Int).Value = _iTrxType_ID;
                    cmd.Parameters.Add("@TrxDate", SqlDbType.DateTime).Value = _dTrxDate;
                    cmd.Parameters.Add("@TrxJustification", SqlDbType.NVarChar, 40).Value = _sTrxJustification;
                    cmd.Parameters.Add("@ISettlementDate", SqlDbType.DateTime).Value = _dISettlementDate;
                    cmd.Parameters.Add("@ASettlementDate", SqlDbType.DateTime).Value = _dASettlementDate;
                    cmd.Parameters.Add("@SingleOrder_ID", SqlDbType.Int).Value = _iSingleOrder_ID;
                    cmd.Parameters.Add("@ExecutionOrder_ID", SqlDbType.Int).Value = _iExecutionOrder_ID;
                    cmd.Parameters.Add("@ExecReference_ID", SqlDbType.NVarChar, 50).Value = _sExecReference_ID;
                    cmd.Parameters.Add("@InvoiceType_ID", SqlDbType.Int).Value = _iInvoiceType_ID;
                    cmd.Parameters.Add("@ReferenceNo", SqlDbType.NVarChar, 40).Value = _sReferenceNo;
                    cmd.Parameters.Add("@D_C", SqlDbType.NVarChar, 1).Value = _sD_C;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Details_ID", SqlDbType.Int).Value = _iContract_Details_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@ExecutionProvider_ID", SqlDbType.Int).Value = _iExecutionProvider_ID;
                    cmd.Parameters.Add("@Custodian_ID", SqlDbType.Int).Value = _iCustodian_ID;
                    cmd.Parameters.Add("@TrxCurrency", SqlDbType.NVarChar, 6).Value = _sTrxCurrency;
                    cmd.Parameters.Add("@TrxCurrencyRate", SqlDbType.Decimal).Value = _fltTrxCurrencyRate;
                    cmd.Parameters.Add("@ReverseCurrencyRate", SqlDbType.Decimal).Value = _fltReverseCurrencyRate;
                    cmd.Parameters.Add("@DebitAmount_EUR", SqlDbType.Float).Value = _fltDebitAmount_EUR;
                    cmd.Parameters.Add("@DebitAmount_Cur", SqlDbType.Float).Value = _fltDebitAmount_Cur;
                    cmd.Parameters.Add("@CreditAmount_EUR", SqlDbType.Float).Value = _fltCreditAmount_EUR;
                    cmd.Parameters.Add("@CreditAmount_Cur", SqlDbType.Float).Value = _fltCreditAmount_Cur;
                    cmd.Parameters.Add("@NetDebitAmount_EUR", SqlDbType.Float).Value = _fltNetDebitAmount_EUR;
                    cmd.Parameters.Add("@NetDebitAmount_Cur", SqlDbType.Float).Value = _fltNetDebitAmount_Cur;
                    cmd.Parameters.Add("@NetCreditAmount_EUR", SqlDbType.Float).Value = _fltNetCreditAmount_EUR;
                    cmd.Parameters.Add("@NetCreditAmount_Cur", SqlDbType.Float).Value = _fltNetCreditAmount_Cur;
                    cmd.Parameters.Add("@TotalExpences_EUR", SqlDbType.Float).Value = _fltTotalExpences_EUR;
                    cmd.Parameters.Add("@TotalExpences_Cur", SqlDbType.Float).Value = _fltTotalExpences_Cur;
                    cmd.Parameters.Add("@Amount_EUR", SqlDbType.Float).Value = _fltAmount_EUR;
                    cmd.Parameters.Add("@Amount_Cur", SqlDbType.Float).Value = _fltAmount_Cur;
                    cmd.Parameters.Add("@NetAmount_EUR", SqlDbType.Float).Value = _fltNetAmount_EUR;
                    cmd.Parameters.Add("@NetAmount_Cur", SqlDbType.Float).Value = _fltNetAmount_Cur;
                    cmd.Parameters.Add("@TrxComments", SqlDbType.NVarChar, 1000).Value = _sTrxComments;
                    cmd.Parameters.Add("@ShareCodes_ID", SqlDbType.Int).Value = _iShareCodes_ID;
                    cmd.Parameters.Add("@Quantity", SqlDbType.Float).Value = _fltQuantity;
                    cmd.Parameters.Add("@Price", SqlDbType.Float).Value = _fltPrice;
                    cmd.Parameters.Add("@ExecutionVenue_ID", SqlDbType.Int).Value = _iExecutionVenue_ID;
                    cmd.Parameters.Add("@Depository_ID", SqlDbType.Int).Value = _iDepository_ID;
                    cmd.Parameters.Add("@TransferCustodian", SqlDbType.NVarChar, 25).Value = _sTransferCustodian;
                    cmd.Parameters.Add("@TransferAccount", SqlDbType.NVarChar, 25).Value = _sTransferAccount;
                    cmd.Parameters.Add("@TransferAccountName", SqlDbType.NVarChar, 100).Value = _sTransferAccountName;
                    cmd.Parameters.Add("@Accruals_EUR", SqlDbType.Float).Value = _fltAccruals_EUR;
                    cmd.Parameters.Add("@Accruals_Cur", SqlDbType.Float).Value = _fltAccruals_Cur;
                    cmd.Parameters.Add("@ExecFee_EUR", SqlDbType.Float).Value = _fltExecFee_EUR;
                    cmd.Parameters.Add("@ExecFee_Cur", SqlDbType.Float).Value = _fltExecFee_Cur;
                    cmd.Parameters.Add("@ExecFeeReturn_EUR", SqlDbType.Float).Value = _fltExecFeeReturn_EUR;
                    cmd.Parameters.Add("@ExecFeeReturn_Cur", SqlDbType.Float).Value = _fltExecFeeReturn_Cur;
                    cmd.Parameters.Add("@ExecFeeIncome_EUR", SqlDbType.Float).Value = _fltExecFeeIncome_EUR;
                    cmd.Parameters.Add("@ExecFeeIncome_Cur", SqlDbType.Float).Value = _fltExecFeeIncome_Cur;
                    cmd.Parameters.Add("@SettleFee_EUR", SqlDbType.Float).Value = _fltSettleFee_EUR;
                    cmd.Parameters.Add("@SettleFee_Cur", SqlDbType.Float).Value = _fltSettleFee_Cur;
                    cmd.Parameters.Add("@SettleFeeReturn_EUR", SqlDbType.Float).Value = _fltSettleFeeReturn_EUR;
                    cmd.Parameters.Add("@SettleFeeReturn_Cur", SqlDbType.Float).Value = _fltSettleFeeReturn_Cur;
                    cmd.Parameters.Add("@SettleFeeIncome_EUR", SqlDbType.Float).Value = _fltSettleFeeIncome_EUR;
                    cmd.Parameters.Add("@SettleFeeIncome_Cur", SqlDbType.Float).Value = _fltSettleFeeIncome_Cur;
                    cmd.Parameters.Add("@ATHEXTransferFee_EUR", SqlDbType.Float).Value = _fltATHEXTransferFee_EUR;
                    cmd.Parameters.Add("@ATHEXTransferFee_Cur", SqlDbType.Float).Value = _fltATHEXTransferFee_Cur;
                    cmd.Parameters.Add("@ATHEXExpences_EUR", SqlDbType.Float).Value = _fltATHEXExpences_EUR;
                    cmd.Parameters.Add("@ATHEXExpences_Cur", SqlDbType.Float).Value = _fltATHEXExpences_Cur;
                    cmd.Parameters.Add("@ATHEXFileExpences_EUR", SqlDbType.Float).Value = _fltATHEXFileExpences_EUR;
                    cmd.Parameters.Add("@ATHEXFileExpences_Cur", SqlDbType.Float).Value = _fltATHEXFileExpences_Cur;
                    cmd.Parameters.Add("@StockXFee_EUR", SqlDbType.Float).Value = _fltStockXFee_EUR;
                    cmd.Parameters.Add("@StockXFee_Cur", SqlDbType.Float).Value = _fltStockXFee_Cur;
                    cmd.Parameters.Add("@PriSecExecFeesReturn_EUR", SqlDbType.Float).Value = _fltPriSecExecFeesReturn_EUR;
                    cmd.Parameters.Add("@PriSecExecFeesReturn_Cur", SqlDbType.Float).Value = _fltPriSecExecFeesReturn_Cur;
                    cmd.Parameters.Add("@PriSecSettleFeesReturn_EUR", SqlDbType.Float).Value = _fltPriSecSettleFeesReturn_EUR;
                    cmd.Parameters.Add("@PriSecSettleFeesReturn_Cur", SqlDbType.Float).Value = _fltPriSecSettleFeesReturn_Cur;
                    cmd.Parameters.Add("@ManagementFee_EUR", SqlDbType.Float).Value = _fltManagementFee_EUR;
                    cmd.Parameters.Add("@ManagementFee_Cur", SqlDbType.Float).Value = _fltManagementFee_Cur;
                    cmd.Parameters.Add("@ManagementFeeIncome_EUR", SqlDbType.Float).Value = _fltManagementFeeIncome_EUR;
                    cmd.Parameters.Add("@ManagementFeeIncome_Cur", SqlDbType.Float).Value = _fltManagementFeeIncome_Cur;
                    cmd.Parameters.Add("@SafekeepingFee_EUR", SqlDbType.Float).Value = _fltSafekeepingFee_EUR;
                    cmd.Parameters.Add("@SafekeepingFee_Cur", SqlDbType.Float).Value = _fltSafekeepingFee_Cur;
                    cmd.Parameters.Add("@SafekeepingFeeIncome_EUR", SqlDbType.Float).Value = _fltSafekeepingFeeIncome_EUR;
                    cmd.Parameters.Add("@SafekeepingFeeIncome_Cur", SqlDbType.Float).Value = _fltSafekeepingFeeIncome_Cur;
                    cmd.Parameters.Add("@PerformanceFee_EUR", SqlDbType.Float).Value = _fltPerformanceFee_EUR;
                    cmd.Parameters.Add("@PerformanceFee_Cur", SqlDbType.Float).Value = _fltPerformanceFee_Cur;
                    cmd.Parameters.Add("@PerformanceFeeIncome_EUR", SqlDbType.Float).Value = _fltPerformanceFeeIncome_EUR;
                    cmd.Parameters.Add("@PerformanceFeeIncome_Cur", SqlDbType.Float).Value = _fltPerformanceFeeIncome_Cur;
                    cmd.Parameters.Add("@SupportFee_EUR", SqlDbType.Float).Value = _fltSupportFee_EUR;
                    cmd.Parameters.Add("@SupportFee_Cur", SqlDbType.Float).Value = _fltSupportFee_Cur;
                    cmd.Parameters.Add("@SupportFeeIncome_EUR", SqlDbType.Float).Value = _fltSupportFeeIncome_EUR;
                    cmd.Parameters.Add("@SupportFeeIncome_Cur", SqlDbType.Float).Value = _fltSupportFeeIncome_Cur;
                    cmd.Parameters.Add("@FxFee_EUR", SqlDbType.Float).Value = _fltFxFee_EUR;
                    cmd.Parameters.Add("@FxFee_Cur", SqlDbType.Float).Value = _fltFxFee_Cur;
                    cmd.Parameters.Add("@CorpActionFee_EUR", SqlDbType.Float).Value = _fltCorpActionFee_EUR;
                    cmd.Parameters.Add("@CorpActionFee_Cur", SqlDbType.Float).Value = _fltCorpActionFee_Cur;
                    cmd.Parameters.Add("@SecTransferFee_EUR", SqlDbType.Float).Value = _fltSecTransferFee_EUR;
                    cmd.Parameters.Add("@SecTransferFee_Cur", SqlDbType.Float).Value = _fltSecTransferFee_Cur;
                    cmd.Parameters.Add("@SecTransferFeeReturn_EUR", SqlDbType.Float).Value = _fltSecTransferFeeReturn_EUR;
                    cmd.Parameters.Add("@SecTransferFeeReturn_Cur", SqlDbType.Float).Value = _fltSecTransferFeeReturn_Cur;
                    cmd.Parameters.Add("@SecTransferFeeIncome_EUR", SqlDbType.Float).Value = _fltSecTransferFeeIncome_EUR;
                    cmd.Parameters.Add("@SecTransferFeeIncome_Cur", SqlDbType.Float).Value = _fltSecTransferFeeIncome_Cur;
                    cmd.Parameters.Add("@CashTransferFee_EUR", SqlDbType.Float).Value = _fltCashTransferFee_EUR;
                    cmd.Parameters.Add("@CashTransferFee_Cur", SqlDbType.Float).Value = _fltCashTransferFee_Cur;
                    cmd.Parameters.Add("@CashTransferFeeReturn_EUR", SqlDbType.Float).Value = _fltCashTransferFeeReturn_EUR;
                    cmd.Parameters.Add("@CashTransferFeeReturn_Cur", SqlDbType.Float).Value = _fltCashTransferFeeReturn_Cur;
                    cmd.Parameters.Add("@CashTransferFeeIncome_EUR", SqlDbType.Float).Value = _fltCashTransferFeeIncome_EUR;
                    cmd.Parameters.Add("@CashTransferFeeIncome_Cur", SqlDbType.Float).Value = _fltCashTransferFeeIncome_Cur;
                    cmd.Parameters.Add("@TaxExpencesAbroad_EUR", SqlDbType.Float).Value = _fltTaxExpencesAbroad_EUR;
                    cmd.Parameters.Add("@TaxExpencesAbroad_Cur", SqlDbType.Float).Value = _fltTaxExpencesAbroad_Cur;
                    cmd.Parameters.Add("@SalesTax_EUR", SqlDbType.Float).Value = _fltSalesTax_EUR;
                    cmd.Parameters.Add("@SalesTax_Cur", SqlDbType.Float).Value = _fltSalesTax_Cur;
                    cmd.Parameters.Add("@VAT_EUR", SqlDbType.Float).Value = _fltVAT_EUR;
                    cmd.Parameters.Add("@VAT_Cur", SqlDbType.Float).Value = _fltVAT_Cur;
                    cmd.Parameters.Add("@WHTax_EUR", SqlDbType.Float).Value = _fltWHTax_EUR;
                    cmd.Parameters.Add("@WHTax_Cur", SqlDbType.Float).Value = _fltWHTax_Cur;
                    cmd.Parameters.Add("@GRTax_EUR", SqlDbType.Float).Value = _fltGRTax_EUR;
                    cmd.Parameters.Add("@GRTax_Cur", SqlDbType.Float).Value = _fltGRTax_Cur;
                    cmd.Parameters.Add("@EntryUser_ID", SqlDbType.Int).Value = _iEntryUser_ID;
                    cmd.Parameters.Add("@EntryDate", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    
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
                using (SqlCommand cmd = new SqlCommand("EditTrx", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@TrxType_ID", SqlDbType.Int).Value = _iTrxType_ID;
                    cmd.Parameters.Add("@TrxDate", SqlDbType.DateTime).Value = _dTrxDate;
                    cmd.Parameters.Add("@TrxJustification", SqlDbType.NVarChar, 40).Value = _sTrxJustification;
                    cmd.Parameters.Add("@ISettlementDate", SqlDbType.DateTime).Value = _dISettlementDate;
                    cmd.Parameters.Add("@ASettlementDate", SqlDbType.DateTime).Value = _dASettlementDate;
                    cmd.Parameters.Add("@SingleOrder_ID", SqlDbType.Int).Value = _iSingleOrder_ID;
                    cmd.Parameters.Add("@ExecutionOrder_ID", SqlDbType.Int).Value = _iExecutionOrder_ID;
                    cmd.Parameters.Add("@ExecReference_ID", SqlDbType.NVarChar, 50).Value = _sExecReference_ID;
                    cmd.Parameters.Add("@InvoiceType_ID", SqlDbType.Int).Value = _iInvoiceType_ID;
                    cmd.Parameters.Add("@ReferenceNo", SqlDbType.NVarChar, 40).Value = _sReferenceNo;
                    cmd.Parameters.Add("@D_C", SqlDbType.NVarChar, 1).Value = _sD_C;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Details_ID", SqlDbType.Int).Value = _iContract_Details_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@ExecutionProvider_ID", SqlDbType.Int).Value = _iExecutionProvider_ID;
                    cmd.Parameters.Add("@Custodian_ID", SqlDbType.Int).Value = _iCustodian_ID;
                    cmd.Parameters.Add("@TrxCurrency", SqlDbType.NVarChar, 6).Value = _sTrxCurrency;
                    cmd.Parameters.Add("@TrxCurrencyRate", SqlDbType.Decimal).Value = _fltTrxCurrencyRate;
                    cmd.Parameters.Add("@ReverseCurrencyRate", SqlDbType.Decimal).Value = _fltReverseCurrencyRate;
                    cmd.Parameters.Add("@DebitAmount_EUR", SqlDbType.Float).Value = _fltDebitAmount_EUR;
                    cmd.Parameters.Add("@DebitAmount_Cur", SqlDbType.Float).Value = _fltDebitAmount_Cur;
                    cmd.Parameters.Add("@CreditAmount_EUR", SqlDbType.Float).Value = _fltCreditAmount_EUR;
                    cmd.Parameters.Add("@CreditAmount_Cur", SqlDbType.Float).Value = _fltCreditAmount_Cur;
                    cmd.Parameters.Add("@NetDebitAmount_EUR", SqlDbType.Float).Value = _fltNetDebitAmount_EUR;
                    cmd.Parameters.Add("@NetDebitAmount_Cur", SqlDbType.Float).Value = _fltNetDebitAmount_Cur;
                    cmd.Parameters.Add("@NetCreditAmount_EUR", SqlDbType.Float).Value = _fltNetCreditAmount_EUR;
                    cmd.Parameters.Add("@NetCreditAmount_Cur", SqlDbType.Float).Value = _fltNetCreditAmount_Cur;
                    cmd.Parameters.Add("@TotalExpences_EUR", SqlDbType.Float).Value = _fltTotalExpences_EUR;
                    cmd.Parameters.Add("@TotalExpences_Cur", SqlDbType.Float).Value = _fltTotalExpences_Cur;
                    cmd.Parameters.Add("@Amount_EUR", SqlDbType.Float).Value = _fltAmount_EUR;
                    cmd.Parameters.Add("@Amount_Cur", SqlDbType.Float).Value = _fltAmount_Cur;
                    cmd.Parameters.Add("@NetAmount_EUR", SqlDbType.Float).Value = _fltNetAmount_EUR;
                    cmd.Parameters.Add("@NetAmount_Cur", SqlDbType.Float).Value = _fltNetAmount_Cur;
                    cmd.Parameters.Add("@TrxComments", SqlDbType.NVarChar, 1000).Value = _sTrxComments;
                    cmd.Parameters.Add("@ShareCodes_ID", SqlDbType.Int).Value = _iShareCodes_ID;
                    cmd.Parameters.Add("@Quantity", SqlDbType.Float).Value = _fltQuantity;
                    cmd.Parameters.Add("@Price", SqlDbType.Float).Value = _fltPrice;
                    cmd.Parameters.Add("@ExecutionVenue_ID", SqlDbType.Int).Value = _iExecutionVenue_ID;
                    cmd.Parameters.Add("@Depository_ID", SqlDbType.Int).Value = _iDepository_ID;
                    cmd.Parameters.Add("@TransferCustodian", SqlDbType.NVarChar, 25).Value = _sTransferCustodian;
                    cmd.Parameters.Add("@TransferAccount", SqlDbType.NVarChar, 25).Value = _sTransferAccount;
                    cmd.Parameters.Add("@TransferAccountName", SqlDbType.NVarChar, 100).Value = _sTransferAccountName;
                    cmd.Parameters.Add("@Accruals_EUR", SqlDbType.Float).Value = _fltAccruals_EUR;
                    cmd.Parameters.Add("@Accruals_Cur", SqlDbType.Float).Value = _fltAccruals_Cur;
                    cmd.Parameters.Add("@ExecFee_EUR", SqlDbType.Float).Value = _fltExecFee_EUR;
                    cmd.Parameters.Add("@ExecFee_Cur", SqlDbType.Float).Value = _fltExecFee_Cur;
                    cmd.Parameters.Add("@ExecFeeReturn_EUR", SqlDbType.Float).Value = _fltExecFeeReturn_EUR;
                    cmd.Parameters.Add("@ExecFeeReturn_Cur", SqlDbType.Float).Value = _fltExecFeeReturn_Cur;
                    cmd.Parameters.Add("@ExecFeeIncome_EUR", SqlDbType.Float).Value = _fltExecFeeIncome_EUR;
                    cmd.Parameters.Add("@ExecFeeIncome_Cur", SqlDbType.Float).Value = _fltExecFeeIncome_Cur;
                    cmd.Parameters.Add("@SettleFee_EUR", SqlDbType.Float).Value = _fltSettleFee_EUR;
                    cmd.Parameters.Add("@SettleFee_Cur", SqlDbType.Float).Value = _fltSettleFee_Cur;
                    cmd.Parameters.Add("@SettleFeeReturn_EUR", SqlDbType.Float).Value = _fltSettleFeeReturn_EUR;
                    cmd.Parameters.Add("@SettleFeeReturn_Cur", SqlDbType.Float).Value = _fltSettleFeeReturn_Cur;
                    cmd.Parameters.Add("@SettleFeeIncome_EUR", SqlDbType.Float).Value = _fltSettleFeeIncome_EUR;
                    cmd.Parameters.Add("@SettleFeeIncome_Cur", SqlDbType.Float).Value = _fltSettleFeeIncome_Cur;
                    cmd.Parameters.Add("@ATHEXTransferFee_EUR", SqlDbType.Float).Value = _fltATHEXTransferFee_EUR;
                    cmd.Parameters.Add("@ATHEXTransferFee_Cur", SqlDbType.Float).Value = _fltATHEXTransferFee_Cur;
                    cmd.Parameters.Add("@ATHEXExpences_EUR", SqlDbType.Float).Value = _fltATHEXExpences_EUR;
                    cmd.Parameters.Add("@ATHEXExpences_Cur", SqlDbType.Float).Value = _fltATHEXExpences_Cur;
                    cmd.Parameters.Add("@ATHEXFileExpences_EUR", SqlDbType.Float).Value = _fltATHEXFileExpences_EUR;
                    cmd.Parameters.Add("@ATHEXFileExpences_Cur", SqlDbType.Float).Value = _fltATHEXFileExpences_Cur;
                    cmd.Parameters.Add("@StockXFee_EUR", SqlDbType.Float).Value = _fltStockXFee_EUR;
                    cmd.Parameters.Add("@StockXFee_Cur", SqlDbType.Float).Value = _fltStockXFee_Cur;
                    cmd.Parameters.Add("@PriSecExecFeesReturn_EUR", SqlDbType.Float).Value = _fltPriSecExecFeesReturn_EUR;
                    cmd.Parameters.Add("@PriSecExecFeesReturn_Cur", SqlDbType.Float).Value = _fltPriSecExecFeesReturn_Cur;
                    cmd.Parameters.Add("@PriSecSettleFeesReturn_EUR", SqlDbType.Float).Value = _fltPriSecSettleFeesReturn_EUR;
                    cmd.Parameters.Add("@PriSecSettleFeesReturn_Cur", SqlDbType.Float).Value = _fltPriSecSettleFeesReturn_Cur;
                    cmd.Parameters.Add("@ManagementFee_EUR", SqlDbType.Float).Value = _fltManagementFee_EUR;
                    cmd.Parameters.Add("@ManagementFee_Cur", SqlDbType.Float).Value = _fltManagementFee_Cur;
                    cmd.Parameters.Add("@ManagementFeeIncome_EUR", SqlDbType.Float).Value = _fltManagementFeeIncome_EUR;
                    cmd.Parameters.Add("@ManagementFeeIncome_Cur", SqlDbType.Float).Value = _fltManagementFeeIncome_Cur;
                    cmd.Parameters.Add("@SafekeepingFee_EUR", SqlDbType.Float).Value = _fltSafekeepingFee_EUR;
                    cmd.Parameters.Add("@SafekeepingFee_Cur", SqlDbType.Float).Value = _fltSafekeepingFee_Cur;
                    cmd.Parameters.Add("@SafekeepingFeeIncome_EUR", SqlDbType.Float).Value = _fltSafekeepingFeeIncome_EUR;
                    cmd.Parameters.Add("@SafekeepingFeeIncome_Cur", SqlDbType.Float).Value = _fltSafekeepingFeeIncome_Cur;
                    cmd.Parameters.Add("@PerformanceFee_EUR", SqlDbType.Float).Value = _fltPerformanceFee_EUR;
                    cmd.Parameters.Add("@PerformanceFee_Cur", SqlDbType.Float).Value = _fltPerformanceFee_Cur;
                    cmd.Parameters.Add("@PerformanceFeeIncome_EUR", SqlDbType.Float).Value = _fltPerformanceFeeIncome_EUR;
                    cmd.Parameters.Add("@PerformanceFeeIncome_Cur", SqlDbType.Float).Value = _fltPerformanceFeeIncome_Cur;
                    cmd.Parameters.Add("@SupportFee_EUR", SqlDbType.Float).Value = _fltSupportFee_EUR;
                    cmd.Parameters.Add("@SupportFee_Cur", SqlDbType.Float).Value = _fltSupportFee_Cur;
                    cmd.Parameters.Add("@SupportFeeIncome_EUR", SqlDbType.Float).Value = _fltSupportFeeIncome_EUR;
                    cmd.Parameters.Add("@SupportFeeIncome_Cur", SqlDbType.Float).Value = _fltSupportFeeIncome_Cur;
                    cmd.Parameters.Add("@FxFee_EUR", SqlDbType.Float).Value = _fltFxFee_EUR;
                    cmd.Parameters.Add("@FxFee_Cur", SqlDbType.Float).Value = _fltFxFee_Cur;
                    cmd.Parameters.Add("@CorpActionFee_EUR", SqlDbType.Float).Value = _fltCorpActionFee_EUR;
                    cmd.Parameters.Add("@CorpActionFee_Cur", SqlDbType.Float).Value = _fltCorpActionFee_Cur;
                    cmd.Parameters.Add("@SecTransferFee_EUR", SqlDbType.Float).Value = _fltSecTransferFee_EUR;
                    cmd.Parameters.Add("@SecTransferFee_Cur", SqlDbType.Float).Value = _fltSecTransferFee_Cur;
                    cmd.Parameters.Add("@SecTransferFeeReturn_EUR", SqlDbType.Float).Value = _fltSecTransferFeeReturn_EUR;
                    cmd.Parameters.Add("@SecTransferFeeReturn_Cur", SqlDbType.Float).Value = _fltSecTransferFeeReturn_Cur;
                    cmd.Parameters.Add("@SecTransferFeeIncome_EUR", SqlDbType.Float).Value = _fltSecTransferFeeIncome_EUR;
                    cmd.Parameters.Add("@SecTransferFeeIncome_Cur", SqlDbType.Float).Value = _fltSecTransferFeeIncome_Cur;
                    cmd.Parameters.Add("@CashTransferFee_EUR", SqlDbType.Float).Value = _fltCashTransferFee_EUR;
                    cmd.Parameters.Add("@CashTransferFee_Cur", SqlDbType.Float).Value = _fltCashTransferFee_Cur;
                    cmd.Parameters.Add("@CashTransferFeeReturn_EUR", SqlDbType.Float).Value = _fltCashTransferFeeReturn_EUR;
                    cmd.Parameters.Add("@CashTransferFeeReturn_Cur", SqlDbType.Float).Value = _fltCashTransferFeeReturn_Cur;
                    cmd.Parameters.Add("@CashTransferFeeIncome_EUR", SqlDbType.Float).Value = _fltCashTransferFeeIncome_EUR;
                    cmd.Parameters.Add("@CashTransferFeeIncome_Cur", SqlDbType.Float).Value = _fltCashTransferFeeIncome_Cur;
                    cmd.Parameters.Add("@TaxExpencesAbroad_EUR", SqlDbType.Float).Value = _fltTaxExpencesAbroad_EUR;
                    cmd.Parameters.Add("@TaxExpencesAbroad_Cur", SqlDbType.Float).Value = _fltTaxExpencesAbroad_Cur;
                    cmd.Parameters.Add("@SalesTax_EUR", SqlDbType.Float).Value = _fltSalesTax_EUR;
                    cmd.Parameters.Add("@SalesTax_Cur", SqlDbType.Float).Value = _fltSalesTax_Cur;
                    cmd.Parameters.Add("@VAT_EUR", SqlDbType.Float).Value = _fltVAT_EUR;
                    cmd.Parameters.Add("@VAT_Cur", SqlDbType.Float).Value = _fltVAT_Cur;
                    cmd.Parameters.Add("@WHTax_EUR", SqlDbType.Float).Value = _fltWHTax_EUR;
                    cmd.Parameters.Add("@WHTax_Cur", SqlDbType.Float).Value = _fltWHTax_Cur;
                    cmd.Parameters.Add("@GRTax_EUR", SqlDbType.Float).Value = _fltGRTax_EUR;
                    cmd.Parameters.Add("@GRTax_Cur", SqlDbType.Float).Value = _fltGRTax_Cur;
                    cmd.Parameters.Add("@EntryUser_ID", SqlDbType.Int).Value = _iEntryUser_ID;
                    cmd.Parameters.Add("@EntryDate", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
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
                using (SqlCommand cmd = new SqlCommand("sp_EditTrx_Status", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
       
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int TrxType_ID { get { return this._iTrxType_ID; } set { this._iTrxType_ID = value; } }
        public DateTime TrxDate { get { return this._dTrxDate; } set { this._dTrxDate = value; } }
        public string TrxJustification { get { return this._sTrxJustification; } set { this._sTrxJustification = value; } }
        public DateTime ISettlementDate { get { return this._dISettlementDate; } set { this._dISettlementDate = value; } }
        public DateTime ASettlementDate { get { return this._dASettlementDate; } set { this._dASettlementDate = value; } }
        public int SingleOrder_ID { get { return this._iSingleOrder_ID; } set { this._iSingleOrder_ID = value; } }
        public int ExecutionOrder_ID { get { return this._iExecutionOrder_ID; } set { this._iExecutionOrder_ID = value; } }
        public string ExecReference_ID { get { return this._sExecReference_ID; } set { this._sExecReference_ID = value; } }
        public int InvoiceType_ID { get { return this._iInvoiceType_ID; } set { this._iInvoiceType_ID = value; } }
        public string ReferenceNo { get { return this._sReferenceNo; } set { this._sReferenceNo = value; } }
        public string D_C { get { return this._sD_C; } set { this._sD_C = value; } }
        public int Contract_ID { get { return this._iContract_ID; } set { this._iContract_ID = value; } }
        public int Contract_Details_ID { get { return this._iContract_Details_ID; } set { this._iContract_Details_ID = value; } }
        public int Contract_Packages_ID { get { return this._iContract_Packages_ID; } set { this._iContract_Packages_ID = value; } }
        public int ExecutionProvider_ID { get { return this._iExecutionProvider_ID; } set { this._iExecutionProvider_ID = value; } }
        public int Custodian_ID { get { return this._iCustodian_ID; } set { this._iCustodian_ID = value; } }
        public string TrxCurrency { get { return this._sTrxCurrency; } set { this._sTrxCurrency = value; } }
        public float TrxCurrencyRate { get { return this._fltTrxCurrencyRate; } set { this._fltTrxCurrencyRate = value; } }
        public float ReverseCurrencyRate { get { return this._fltReverseCurrencyRate; } set { this._fltReverseCurrencyRate = value; } }
        public float DebitAmount_EUR { get { return this._fltDebitAmount_EUR; } set { this._fltDebitAmount_EUR = value; } }
        public float DebitAmount_Cur { get { return this._fltDebitAmount_Cur; } set { this._fltDebitAmount_Cur = value; } }
        public float CreditAmount_EUR { get { return this._fltCreditAmount_EUR; } set { this._fltCreditAmount_EUR = value; } }
        public float CreditAmount_Cur { get { return this._fltCreditAmount_Cur; } set { this._fltCreditAmount_Cur = value; } }
        public float NetDebitAmount_EUR { get { return this._fltNetDebitAmount_EUR; } set { this._fltNetDebitAmount_EUR = value; } }
        public float NetDebitAmount_Cur { get { return this._fltNetDebitAmount_Cur; } set { this._fltNetDebitAmount_Cur = value; } }
        public float NetCreditAmount_EUR { get { return this._fltNetCreditAmount_EUR; } set { this._fltNetCreditAmount_EUR = value; } }
        public float NetCreditAmount_Cur { get { return this._fltNetCreditAmount_Cur; } set { this._fltNetCreditAmount_Cur = value; } }
        public float TotalExpences_EUR { get { return this._fltTotalExpences_EUR; } set { this._fltTotalExpences_EUR = value; } }
        public float TotalExpences_Cur { get { return this._fltTotalExpences_Cur; } set { this._fltTotalExpences_Cur = value; } }
        public float Amount_EUR { get { return this._fltAmount_EUR; } set { this._fltAmount_EUR = value; } }
        public float Amount_Cur { get { return this._fltAmount_Cur; } set { this._fltAmount_Cur = value; } }
        public float NetAmount_EUR { get { return this._fltNetAmount_EUR; } set { this._fltNetAmount_EUR = value; } }
        public float NetAmount_Cur { get { return this._fltNetAmount_Cur; } set { this._fltNetAmount_Cur = value; } }
        public string TrxComments { get { return this._sTrxComments; } set { this._sTrxComments = value; } }
        public int ShareCodes_ID { get { return this._iShareCodes_ID; } set { this._iShareCodes_ID = value; } }
        public float Quantity { get { return this._fltQuantity; } set { this._fltQuantity = value; } }
        public float Price { get { return this._fltPrice; } set { this._fltPrice = value; } }
        public int ExecutionVenue_ID { get { return this._iExecutionVenue_ID; } set { this._iExecutionVenue_ID = value; } }
        public int Depository_ID { get { return this._iDepository_ID; } set { this._iDepository_ID = value; } }
        public string TransferCustodian { get { return this._sTransferCustodian; } set { this._sTransferCustodian = value; } }
        public string TransferAccount { get { return this._sTransferAccount; } set { this._sTransferAccount = value; } }
        public string TransferAccountName { get { return this._sTransferAccountName; } set { this._sTransferAccountName = value; } }
        public float Accruals_EUR { get { return this._fltAccruals_EUR; } set { this._fltAccruals_EUR = value; } }
        public float Accruals_Cur { get { return this._fltAccruals_Cur; } set { this._fltAccruals_Cur = value; } }
        public float ExecFee_EUR { get { return this._fltExecFee_EUR; } set { this._fltExecFee_EUR = value; } }
        public float ExecFee_Cur { get { return this._fltExecFee_Cur; } set { this._fltExecFee_Cur = value; } }
        public float ExecFeeReturn_EUR { get { return this._fltExecFeeReturn_EUR; } set { this._fltExecFeeReturn_EUR = value; } }
        public float ExecFeeReturn_Cur { get { return this._fltExecFeeReturn_Cur; } set { this._fltExecFeeReturn_Cur = value; } }
        public float ExecFeeIncome_EUR { get { return this._fltExecFeeIncome_EUR; } set { this._fltExecFeeIncome_EUR = value; } }
        public float ExecFeeIncome_Cur { get { return this._fltExecFeeIncome_Cur; } set { this._fltExecFeeIncome_Cur = value; } }
        public float SettleFee_EUR { get { return this._fltSettleFee_EUR; } set { this._fltSettleFee_EUR = value; } }
        public float SettleFee_Cur { get { return this._fltSettleFee_Cur; } set { this._fltSettleFee_Cur = value; } }
        public float SettleFeeReturn_EUR { get { return this._fltSettleFeeReturn_EUR; } set { this._fltSettleFeeReturn_EUR = value; } }
        public float SettleFeeReturn_Cur { get { return this._fltSettleFeeReturn_Cur; } set { this._fltSettleFeeReturn_Cur = value; } }
        public float SettleFeeIncome_EUR { get { return this._fltSettleFeeIncome_EUR; } set { this._fltSettleFeeIncome_EUR = value; } }
        public float SettleFeeIncome_Cur { get { return this._fltSettleFeeIncome_Cur; } set { this._fltSettleFeeIncome_Cur = value; } }
        public float ATHEXTransferFee_EUR { get { return this._fltATHEXTransferFee_EUR; } set { this._fltATHEXTransferFee_EUR = value; } }
        public float ATHEXTransferFee_Cur { get { return this._fltATHEXTransferFee_Cur; } set { this._fltATHEXTransferFee_Cur = value; } }
        public float ATHEXExpences_EUR { get { return this._fltATHEXExpences_EUR; } set { this._fltATHEXExpences_EUR = value; } }
        public float ATHEXExpences_Cur { get { return this._fltATHEXExpences_Cur; } set { this._fltATHEXExpences_Cur = value; } }
        public float ATHEXFileExpences_EUR { get { return this._fltATHEXFileExpences_EUR; } set { this._fltATHEXFileExpences_EUR = value; } }
        public float ATHEXFileExpences_Cur { get { return this._fltATHEXFileExpences_Cur; } set { this._fltATHEXFileExpences_Cur = value; } }
        public float StockXFee_EUR { get { return this._fltStockXFee_EUR; } set { this._fltStockXFee_EUR = value; } }
        public float StockXFee_Cur { get { return this._fltStockXFee_Cur; } set { this._fltStockXFee_Cur = value; } }
        public float PriSecExecFeesReturn_EUR { get { return this._fltPriSecExecFeesReturn_EUR; } set { this._fltPriSecExecFeesReturn_EUR = value; } }
        public float PriSecExecFeesReturn_Cur { get { return this._fltPriSecExecFeesReturn_Cur; } set { this._fltPriSecExecFeesReturn_Cur = value; } }
        public float PriSecSettleFeesReturn_EUR { get { return this._fltPriSecSettleFeesReturn_EUR; } set { this._fltPriSecSettleFeesReturn_EUR = value; } }
        public float PriSecSettleFeesReturn_Cur { get { return this._fltPriSecSettleFeesReturn_Cur; } set { this._fltPriSecSettleFeesReturn_Cur = value; } }
        public float ManagementFee_EUR { get { return this._fltManagementFee_EUR; } set { this._fltManagementFee_EUR = value; } }
        public float ManagementFee_Cur { get { return this._fltManagementFee_Cur; } set { this._fltManagementFee_Cur = value; } }
        public float ManagementFeeIncome_EUR { get { return this._fltManagementFeeIncome_EUR; } set { this._fltManagementFeeIncome_EUR = value; } }
        public float ManagementFeeIncome_Cur { get { return this._fltManagementFeeIncome_Cur; } set { this._fltManagementFeeIncome_Cur = value; } }
        public float SafekeepingFee_EUR { get { return this._fltSafekeepingFee_EUR; } set { this._fltSafekeepingFee_EUR = value; } }
        public float SafekeepingFee_Cur { get { return this._fltSafekeepingFee_Cur; } set { this._fltSafekeepingFee_Cur = value; } }
        public float SafekeepingFeeIncome_EUR { get { return this._fltSafekeepingFeeIncome_EUR; } set { this._fltSafekeepingFeeIncome_EUR = value; } }
        public float SafekeepingFeeIncome_Cur { get { return this._fltSafekeepingFeeIncome_Cur; } set { this._fltSafekeepingFeeIncome_Cur = value; } }
        public float PerformanceFee_EUR { get { return this._fltPerformanceFee_EUR; } set { this._fltPerformanceFee_EUR = value; } }
        public float PerformanceFee_Cur { get { return this._fltPerformanceFee_Cur; } set { this._fltPerformanceFee_Cur = value; } }
        public float PerformanceFeeIncome_EUR { get { return this._fltPerformanceFeeIncome_EUR; } set { this._fltPerformanceFeeIncome_EUR = value; } }
        public float PerformanceFeeIncome_Cur { get { return this._fltPerformanceFeeIncome_Cur; } set { this._fltPerformanceFeeIncome_Cur = value; } }
        public float SupportFee_EUR { get { return this._fltSupportFee_EUR; } set { this._fltSupportFee_EUR = value; } }
        public float SupportFee_Cur { get { return this._fltSupportFee_Cur; } set { this._fltSupportFee_Cur = value; } }
        public float SupportFeeIncome_EUR { get { return this._fltSupportFeeIncome_EUR; } set { this._fltSupportFeeIncome_EUR = value; } }
        public float SupportFeeIncome_Cur { get { return this._fltSupportFeeIncome_Cur; } set { this._fltSupportFeeIncome_Cur = value; } }
        public float FxFee_EUR { get { return this._fltFxFee_EUR; } set { this._fltFxFee_EUR = value; } }
        public float FxFee_Cur { get { return this._fltFxFee_Cur; } set { this._fltFxFee_Cur = value; } }
        public float CorpActionFee_EUR { get { return this._fltCorpActionFee_EUR; } set { this._fltCorpActionFee_EUR = value; } }
        public float CorpActionFee_Cur { get { return this._fltCorpActionFee_Cur; } set { this._fltCorpActionFee_Cur = value; } }
        public float SecTransferFee_EUR { get { return this._fltSecTransferFee_EUR; } set { this._fltSecTransferFee_EUR = value; } }
        public float SecTransferFee_Cur { get { return this._fltSecTransferFee_Cur; } set { this._fltSecTransferFee_Cur = value; } }
        public float SecTransferFeeReturn_EUR { get { return this._fltSecTransferFeeReturn_EUR; } set { this._fltSecTransferFeeReturn_EUR = value; } }
        public float SecTransferFeeReturn_Cur { get { return this._fltSecTransferFeeReturn_Cur; } set { this._fltSecTransferFeeReturn_Cur = value; } }
        public float SecTransferFeeIncome_EUR { get { return this._fltSecTransferFeeIncome_EUR; } set { this._fltSecTransferFeeIncome_EUR = value; } }
        public float SecTransferFeeIncome_Cur { get { return this._fltSecTransferFeeIncome_Cur; } set { this._fltSecTransferFeeIncome_Cur = value; } }
        public float CashTransferFee_EUR { get { return this._fltCashTransferFee_EUR; } set { this._fltCashTransferFee_EUR = value; } }
        public float CashTransferFee_Cur { get { return this._fltCashTransferFee_Cur; } set { this._fltCashTransferFee_Cur = value; } }
        public float CashTransferFeeReturn_EUR { get { return this._fltCashTransferFeeReturn_EUR; } set { this._fltCashTransferFeeReturn_EUR = value; } }
        public float CashTransferFeeReturn_Cur { get { return this._fltCashTransferFeeReturn_Cur; } set { this._fltCashTransferFeeReturn_Cur = value; } }
        public float CashTransferFeeIncome_EUR { get { return this._fltCashTransferFeeIncome_EUR; } set { this._fltCashTransferFeeIncome_EUR = value; } }
        public float CashTransferFeeIncome_Cur { get { return this._fltCashTransferFeeIncome_Cur; } set { this._fltCashTransferFeeIncome_Cur = value; } }
        public float TaxExpencesAbroad_EUR { get { return this._fltTaxExpencesAbroad_EUR; } set { this._fltTaxExpencesAbroad_EUR = value; } }
        public float TaxExpencesAbroad_Cur { get { return this._fltTaxExpencesAbroad_Cur; } set { this._fltTaxExpencesAbroad_Cur = value; } }
        public float SalesTax_EUR { get { return this._fltSalesTax_EUR; } set { this._fltSalesTax_EUR = value; } }
        public float SalesTax_Cur { get { return this._fltSalesTax_Cur; } set { this._fltSalesTax_Cur = value; } }
        public float VAT_EUR { get { return this._fltVAT_EUR; } set { this._fltVAT_EUR = value; } }
        public float VAT_Cur { get { return this._fltVAT_Cur; } set { this._fltVAT_Cur = value; } }
        public float WHTax_EUR { get { return this._fltWHTax_EUR; } set { this._fltWHTax_EUR = value; } }
        public float WHTax_Cur { get { return this._fltWHTax_Cur; } set { this._fltWHTax_Cur = value; } }
        public float GRTax_EUR { get { return this._fltGRTax_EUR; } set { this._fltGRTax_EUR = value; } }
        public float GRTax_Cur { get { return this._fltGRTax_Cur; } set { this._fltGRTax_Cur = value; } }      
        public int EntryUser_ID { get { return this._iEntryUser_ID; } set { this._iEntryUser_ID = value; } }
        public DateTime EntryDate { get { return this._dEntryDate; } set { this._dEntryDate = value; } }
        public int Status { get { return this._iStatus; } set { this._iStatus = value; } }


        public string ContractTitle { get { return this._sContractTitle; } set { this._sContractTitle = value; } }
        public int ContractTipos { get { return this._iContractTipos; } set { this._iContractTipos = value; } }           
        public string Code { get { return this._sContractCode; } set { this._sContractCode = value; } }
        public string sContractPortfolio { get { return this._sContractPortfolio; } set { this._sContractPortfolio = value; } }
        public int Product_ID { get { return this._iProductType_ID; } set { this._iProductType_ID = value; } }
        public int ProductCategory_ID { get { return this._iProductCategory_ID; } set { this._iProductCategory_ID = value; } }
        public string Currency { get { return this._sTrxCurrency; } set { this._sTrxCurrency = value; } }      
        public string Product_Title { get { return this._sProduct_Title; } set { this._sProduct_Title = value; } }
        public string ProductCategory_Title { get { return this._sProductCategory_Title; } set { this._sProductCategory_Title = value; } }
        public string ShareCodes_Code { get { return this._sShareCodes_Code; } set { this._sShareCodes_Code = value; } }
        public string ShareCodes_Code2 { get { return this._sShareCodes_Code2; } set { this._sShareCodes_Code2 = value; } }
        public string ISIN { get { return this._sISIN; } set { this._sISIN = value; } }
        public DateTime ContractDateStart { get { return this._dContractDateStart; } set { this._dContractDateStart = value; } }
        public DateTime ContractDateFinish { get { return this._dContractDateFinish; } set { this._dContractDateFinish = value; } }
        public string ContractCurrency { get { return this._sContractCurrency; } set { this._sContractCurrency = value; } }   
        public string ExecutionVenue_Title { get { return this._sExecutionVenue_Title; } set { this._sExecutionVenue_Title = value; } }
        public DateTime DateFrom { get { return this._dDateFrom; } set { this._dDateFrom = value; } }
        public DateTime DateTo { get { return this._dDateTo; } set { this._dDateTo = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}