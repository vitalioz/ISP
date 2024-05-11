using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsOrders_ProvidersData
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int _iRecord_ID;
        private int _iServiceProvider_ID;
        private DateTime _dAktionDate;
        private DateTime _dTradeDate;
        private string _sTradeTime;
        private DateTime _dSettlementDate;
        private string _sTradeCurrency;
        private string _sCounterparty;
        private string _sISIN;
        private string _sSecurityCode;
        private string _sSecurityDescription;
        private string _sMarket;
        private string _sSign;
        private float _fltQuantityNominal;
        private float _fltPrice;
        private float _fltAccruedInterest;
        private float _fltCommission;
        private float _fltFees;
        private float _fltMarketFee; 
        private float _fltTaxes;
        private float _fltSettlementAmount;
        private float _fltExchangeRate;
        private float _fltSettlementAmountCurr;
        private string _sOrderNumber;
        private string _sTradeID;
        private string _sCancelledTradeID;
        private string _sAgainst;
        private string _sOriginarySystem;
        private string _sSettlementPlace;
        private string _sRefNumber;
        private int _iStockExchange_ID;
        private int _iDepository_ID;
        private int _iCommand_ID;
        private int _iCommand_Execution_ID;
        private int _iExported;

        private DataTable _dtList;
        public clsOrders_ProvidersData()
        {
            this._iRecord_ID = 0;
            this._iServiceProvider_ID = 0;
            this._dAktionDate = Convert.ToDateTime("01/01/1900");
            this._dTradeDate = Convert.ToDateTime("01/01/1900");
            this._sTradeTime = "";
            this._dSettlementDate = Convert.ToDateTime("01/01/1900");
            this._sTradeCurrency = "";
            this._sCounterparty = "";
            this._sISIN = "";
            this._sSecurityCode = "";
            this._sSecurityDescription = "";
            this._sMarket = "";
            this._sSign = "";
            this._fltQuantityNominal = 0;
            this._fltPrice = 0;
            this._fltAccruedInterest = 0;
            this._fltCommission = 0;
            this._fltFees = 0;
            this._fltMarketFee = 0;
            this._fltTaxes = 0;
            this._fltSettlementAmount = 0;
            this._fltExchangeRate = 0;
            this._fltSettlementAmountCurr = 0;
            this._sOrderNumber = "";
            this._sTradeID = "";
            this._sCancelledTradeID = "";
            this._sAgainst = "";
            this._sOriginarySystem = "";
            this._sSettlementPlace = "";
            this._sRefNumber = "";
            this._iStockExchange_ID = 0;
            this._iDepository_ID = 0;
            this._iCommand_ID = 0;
            this._iCommand_Execution_ID = 0;
            this._iExported = 0;
        }
        public void xxxGetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetInforming", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Contract_ID", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Contract_Details_ID", "0"));
                cmd.Parameters.Add(new SqlParameter("@Contract_Packages_ID", "0"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iServiceProvider_ID = Convert.ToInt32(drList["Tipos"]);
                    this._sMarket = drList["Market"] + "";
                    this._sTradeCurrency = drList["CC"] + "";
                    this._sCounterparty = drList["Counterparty"] + "";
                    this._sISIN = drList["ISIN"] + "";
                    this._sSecurityCode = drList["SecurityCode"] + "";
                    this._sSecurityDescription = drList["SecurityDescription"] + "";
                    this._fltPrice = Convert.ToInt32(drList["InformMethod"]);
                    this._fltQuantityNominal = Convert.ToInt32(drList["Command_ID"]);
                    this._fltAccruedInterest = Convert.ToInt32(drList["Source_ID"]);
                    this._fltCommission = Convert.ToInt32(drList["Client_ID"]);
                    this._fltFees = Convert.ToInt32(drList["Contract_ID "]);
                    this._fltMarketFee = Convert.ToInt32(drList["Contract_ID"]);
                    this._fltTaxes = Convert.ToInt32(drList["SecurityDescriptionCount"]);
                    this._dAktionDate = Convert.ToDateTime(drList["AktionDate"]);
                    this._fltSettlementAmount = Convert.ToInt32(drList["Status"]);
                    this._fltExchangeRate = Convert.ToInt32(drList["SentAttempts"]);
                    this._sSign = drList["Sign"] + "";
                    this._iExported = Convert.ToInt32(drList["Exported"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("StockExchange_ID", typeof(int));
            _dtList.Columns.Add("Depository_ID", typeof(int));
            _dtList.Columns.Add("TradeDate", typeof(string));
            _dtList.Columns.Add("TradeTime", typeof(string));
            _dtList.Columns.Add("SettlementDate", typeof(string));
            _dtList.Columns.Add("TradeCurrency", typeof(string));
            _dtList.Columns.Add("SecurityCode", typeof(string));
            _dtList.Columns.Add("SecurityDescription", typeof(string));
            _dtList.Columns.Add("StockExchanges_Title", typeof(string));
            _dtList.Columns.Add("Sign", typeof(string));
            _dtList.Columns.Add("QuantityNominal", typeof(string));
            _dtList.Columns.Add("Price", typeof(string));
            _dtList.Columns.Add("AccruedInterest", typeof(string));
            _dtList.Columns.Add("Commission", typeof(string));
            _dtList.Columns.Add("Fees", typeof(string));
            _dtList.Columns.Add("SettlementAmount", typeof(string));
            _dtList.Columns.Add("ExchangeRate", typeof(string));
            _dtList.Columns.Add("SettlementAmountCurr", typeof(string));
            _dtList.Columns.Add("Depositories_Title", typeof(string));
            _dtList.Columns.Add("RefNumber", typeof(string));

            try
            {
                conn.Open();
                cmd = new SqlCommand("sp_GetCommands_CommandsProvidersData", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Command_ID", _iCommand_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["StockExchange_ID"], drList["Depository_ID"], drList["TradeDate"],  drList["TradeTime"], 
                                     drList["SettlementDate"],  drList["TradeCurrency"], drList["SecurityCode"],  
                                     drList["SecurityDescription"], drList["StockExchanges_Title"],  drList["Sign"],  drList["QuantityNominal"], 
                                     drList["Price"],  drList["AccruedInterest"],  drList["Commission"], drList["Fees"],  drList["SettlementAmount"],  
                                     drList["ExchangeRate"], drList["SettlementAmountCurr"],  drList["Depositories_Title"],  drList["RefNumber"] );
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int xxxInsertRecord()
        {
            try
            {
                conn.Open();
                using (cmd = new SqlCommand("sp_InsertInformings", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ServiceProvider_ID", SqlDbType.Int).Value = _iServiceProvider_ID;
                    cmd.Parameters.Add("@Command_ID", SqlDbType.Int).Value = _fltQuantityNominal;
                    cmd.Parameters.Add("@InformMethod", SqlDbType.Int).Value = _fltPrice;
                    cmd.Parameters.Add("@Source_ID", SqlDbType.Int).Value = _fltAccruedInterest;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _fltCommission;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _fltFees;
                    cmd.Parameters.Add("@Market", SqlDbType.NVarChar, 100).Value = _sMarket;
                    cmd.Parameters.Add("@CC", SqlDbType.NVarChar, 100).Value = _sTradeCurrency;
                    cmd.Parameters.Add("@Counterparty", SqlDbType.NVarChar, 100).Value = _sCounterparty;
                    cmd.Parameters.Add("@ISIN", SqlDbType.NVarChar, 100).Value = _sISIN;
                    cmd.Parameters.Add("@SecurityCode", SqlDbType.NVarChar, 100).Value = _sSecurityCode;
                    cmd.Parameters.Add("@SecurityDescription", SqlDbType.NVarChar, 100).Value = _sSecurityDescription;
                    cmd.Parameters.Add("@SecurityDescriptionCount", SqlDbType.Int).Value = _fltTaxes;
                    cmd.Parameters.Add("@AktionDate", SqlDbType.DateTime).Value = _dAktionDate;
                    cmd.Parameters.Add("@OrderNumber", SqlDbType.NVarChar, 20).Value = _sOrderNumber;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _fltSettlementAmount;
                    cmd.Parameters.Add("@SentAttempts", SqlDbType.Int).Value = _fltExchangeRate;
                    cmd.Parameters.Add("@Sign", SqlDbType.NVarChar, 100).Value = _sSign;
                    cmd.Parameters.Add("@Exported", SqlDbType.Int).Value = _iExported;
                    cmd.ExecuteNonQuery();
                    _iRecord_ID = Convert.ToInt32(outParam.Value);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return _iRecord_ID;
        }
        public void xxxEditRecord()
        {
            try
            {
                conn.Open();
                using (cmd = new SqlCommand("sp_Edit_RMJobs", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@ServiceProvider_ID", SqlDbType.Int).Value = _iServiceProvider_ID;
                    cmd.Parameters.Add("@Command_ID", SqlDbType.Int).Value = _fltQuantityNominal;
                    cmd.Parameters.Add("@InformMethod", SqlDbType.Int).Value = _fltPrice;
                    cmd.Parameters.Add("@Source_ID", SqlDbType.Int).Value = _fltAccruedInterest;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _fltCommission;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _fltFees;
                    cmd.Parameters.Add("@Market", SqlDbType.NVarChar, 100).Value = _sMarket;
                    cmd.Parameters.Add("@CC", SqlDbType.NVarChar, 100).Value = _sTradeCurrency;
                    cmd.Parameters.Add("@Counterparty", SqlDbType.NVarChar, 100).Value = _sCounterparty;
                    cmd.Parameters.Add("@ISIN", SqlDbType.NVarChar, 100).Value = _sISIN;
                    cmd.Parameters.Add("@SecurityCode", SqlDbType.NVarChar, 100).Value = _sSecurityCode;
                    cmd.Parameters.Add("@SecurityDescription", SqlDbType.NVarChar, 100).Value = _sSecurityDescription;
                    cmd.Parameters.Add("@SecurityDescriptionCount", SqlDbType.Int).Value = _fltTaxes;
                    cmd.Parameters.Add("@AktionDate", SqlDbType.DateTime).Value = _dAktionDate;
                    cmd.Parameters.Add("@OrderNumber", SqlDbType.NVarChar, 20).Value = _sOrderNumber;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _fltSettlementAmount;
                    cmd.Parameters.Add("@SentAttempts", SqlDbType.Int).Value = _fltExchangeRate;
                    cmd.Parameters.Add("@Sign", SqlDbType.NVarChar, 100).Value = _sSign;
                    cmd.Parameters.Add("@Exported", SqlDbType.Int).Value = _iExported;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void DeleteRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "RMJobs";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return _iRecord_ID; } set { _iRecord_ID = value; } }
        public int ServiceProvider_ID { get { return _iServiceProvider_ID; } set { _iServiceProvider_ID = value; } }
        public DateTime AktionDate { get { return _dAktionDate; } set { _dAktionDate = value; } }
        public DateTime TradeDate { get { return _dTradeDate; } set { _dTradeDate = value; } }
        public string TradeTime { get { return _sTradeTime; } set { _sTradeTime = value; } }
        public DateTime SettlementDate { get { return _dSettlementDate; } set { _dSettlementDate = value; } }
        public string TradeCurrency { get { return _sTradeCurrency; } set { _sTradeCurrency = value; } }
        public string Counterparty { get { return _sCounterparty; } set { _sCounterparty = value; } }
        public string ISIN { get { return _sISIN; } set { _sISIN = value; } }
        public string SecurityCode { get { return _sSecurityCode; } set { _sSecurityCode = value; } }
        public string SecurityDescription { get { return _sSecurityDescription; } set { _sSecurityDescription = value; } }
        public string Market { get { return _sMarket; } set { _sMarket = value; } }
        public string Sign { get { return _sSign; } set { _sSign = value; } }
        public float QuantityNominal { get { return _fltQuantityNominal; } set { _fltQuantityNominal = value; } }
        public float Price { get { return _fltPrice; } set { _fltPrice = value; } }
        public float AccruedInterest { get { return _fltAccruedInterest; } set { _fltAccruedInterest = value; } }
        public float Commission { get { return _fltCommission; } set { _fltCommission = value; } }
        public float Fees { get { return _fltFees; } set { _fltFees = value; } }
        public float MarketFee { get { return _fltMarketFee; } set { _fltMarketFee = value; } }
        public float Taxes { get { return _fltTaxes; } set { _fltTaxes = value; } }
        public float SettlementAmount { get { return _fltSettlementAmount; } set { _fltSettlementAmount = value; } }
        public float ExchangeRate { get { return _fltExchangeRate; } set { _fltExchangeRate = value; } }
        public float SettlementAmountCurr { get { return _fltSettlementAmountCurr; } set { _fltSettlementAmountCurr = value; } }
        public string OrderNumber { get { return _sOrderNumber; } set { _sOrderNumber = value; } }
        public string TradeID { get { return _sTradeID; } set { _sTradeID = value; } }
        public string CancelledTradeID { get { return _sCancelledTradeID; } set { _sCancelledTradeID = value; } }
        public string Against { get { return _sAgainst; } set { _sAgainst = value; } }
        public string OriginarySystem { get { return _sOriginarySystem; } set { _sOriginarySystem = value; } }
        public string SettlementPlace { get { return _sSettlementPlace; } set { _sSettlementPlace = value; } }
        public string RefNumber { get { return _sRefNumber; } set { _sRefNumber = value; } }
        public int StockExchange_ID { get { return _iStockExchange_ID; } set { _iStockExchange_ID = value; } }
        public int Command_ID { get { return _iCommand_ID; } set { _iCommand_ID = value; } }
        public int Depository_ID { get { return _iDepository_ID; } set { _iDepository_ID = value; } }
        public int Command_Execution_ID { get { return _iCommand_Execution_ID; } set { _iCommand_Execution_ID = value; } }
        public int Exported { get { return _iExported; } set { _iExported = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
