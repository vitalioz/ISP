using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsCustodyCommands
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int       _iRecord_ID;
        private int       _iServiceProvider_ID;
        private DateTime  _dAktionDate;
        private DateTime  _dTradeDate;
        private string    _sTradeTime;
        private DateTime  _dSettlementDate;
        private string    _sTradeCurrency;
        private string    _sCounterparty;
        private string    _sISIN;
        private string    _sSecurityCode;
        private string    _sSecurityDescription;
        private string    _sMarket;
        private string    _sSign;
        private decimal   _decQuantityNominal;
        private decimal   _decPrice;
        private decimal   _decAccruedInterest;
        private decimal   _decCommission;
        private decimal   _decFees;
        private decimal   _decMarketFee;
        private decimal   _decTaxes;
        private decimal   _decSettlementAmount;
        private decimal   _decExchangeRate;
        private decimal   _decSettlementAmountCurr;
        private string    _sOrderNumber;
        private string    _sTradeID;
        private string    _sCancelledTradeID;
        private string    _sAgainst;
        private string    _sOriginarySystem;
        private string    _sSettlementPlace;
        private string    _sRefNumber;
        private string    _sMIC_Code;
        private string    _sPSET;
        private int       _iStockExchange_ID;
        private int       _iDepository_ID;
        private int       _iCommand_ID;
        private int       _iCommand_Execution_ID;
        private int       _iExported;
      
        private DateTime  _dDateFrom;
        private DateTime  _dDateTo;
        private DataTable _dtList;

        public clsCustodyCommands()
        {
            this._iRecord_ID = 0;
            this._iServiceProvider_ID = 0;
            this._dAktionDate = Convert.ToDateTime("1900/01/01");
            this._dTradeDate = Convert.ToDateTime("1900/01/01");
            this._sTradeTime = "";
            this._dSettlementDate = Convert.ToDateTime("1900/01/01");
            this._sTradeCurrency = "";
            this._sCounterparty = "";
            this._sISIN = "";
            this._sSecurityCode = "";
            this._sSecurityDescription = "";
            this._sMarket = "";
            this._sSign = "";
            this._decQuantityNominal = 0;
            this._decPrice = 0;
            this._decAccruedInterest = 0;
            this._decCommission = 0;
            this._decFees = 0;
            this._decMarketFee = 0;
            this._decTaxes = 0;
            this._decSettlementAmount = 0;
            this._decExchangeRate = 0;
            this._decSettlementAmountCurr = 0;
            this._sOrderNumber = "";
            this._sTradeID = "";
            this._sCancelledTradeID = "";
            this._sAgainst = "";
            this._sOriginarySystem = "";
            this._sSettlementPlace = "";
            this._sRefNumber = "";
            this._sMIC_Code = "";
            this._sPSET = "";
            this._iStockExchange_ID = 0;
            this._iDepository_ID = 0;
            this._iCommand_ID = 0;
            this._iCommand_Execution_ID = 0;
            this._iExported = 0;
            this._dDateFrom = Convert.ToDateTime("1900/01/01");
            this._dDateTo = Convert.ToDateTime("1900/01/01");           
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Commands_ProvidersData"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", _iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iServiceProvider_ID = Convert.ToInt32(drList["ServiceProvider_ID"]);
                    this._dAktionDate = Convert.ToDateTime(drList["AktionDate"]);
                    this._dTradeDate = Convert.ToDateTime(drList["TradeDate"]);
                    this._sTradeTime = drList["TradeTime"] + "";
                    this._dSettlementDate = Convert.ToDateTime(drList["SettlementDate"]);
                    this._sTradeCurrency = drList["TradeCurrency"] + "";
                    this._sCounterparty = drList["Counterparty"] + "";
                    this._sISIN = drList["ISIN"] + "";
                    this._sSecurityCode = drList["SecurityCode"] + "";
                    this._sSecurityDescription = drList["SecurityDescription"] + "";
                    this._sMarket = drList["Market"] + "";
                    this._sSign = drList["Sign"] + "";
                    this._decQuantityNominal = Convert.ToDecimal(drList["QuantityNominal"]);
                    this._decPrice = Convert.ToDecimal(drList["Price"]);
                    this._decAccruedInterest = Convert.ToDecimal(drList["AccruedInterest"]);
                    this._decCommission = Convert.ToDecimal(drList["Commission"]);
                    this._decFees = Convert.ToDecimal(drList["Fees"]);
                    this._decMarketFee = Convert.ToDecimal(drList["MarketFee"]);
                    this._decTaxes = Convert.ToDecimal(drList["Taxes"]);
                    this._decSettlementAmount = Convert.ToDecimal(drList["SettlementAmount"]);
                    this._decExchangeRate = Convert.ToDecimal(drList["ExchangeRate"]);
                    this._decSettlementAmountCurr = Convert.ToDecimal(drList["SettlementAmountCurr"]);
                    this._sOrderNumber = drList["OrderNumber"] + "";
                    this._sTradeID = drList["TradeID"] + "";
                    this._sCancelledTradeID = drList["CancelledTradeID"] + "";
                    this._sAgainst = drList["Against"] + "";
                    this._sOriginarySystem = drList["OriginarySystem"] + "";
                    this._sSettlementPlace = drList["SettlementPlace"] + "";
                    this._sRefNumber = drList["RefNumber"] + "";
                    this._sMIC_Code = drList["MIC_Code"] + "";
                    this._sPSET = drList["PSET"] + "";
                    this._iStockExchange_ID = Convert.ToInt32(drList["StockExchange_ID"]); ;
                    this._iDepository_ID = Convert.ToInt32(drList["Depository_ID"]); ;
                    this._iCommand_ID = Convert.ToInt32(drList["Command_ID"]); ;
                    this._iCommand_Execution_ID = Convert.ToInt32(drList["Command_Execution_ID"]); ;
                    this._iExported = Convert.ToInt32(drList["Exported"]); ;                  
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            try {
                _dtList = new DataTable("CustodyCommands_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AktionDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("TradeDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("TradeTime", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SettlementDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("TradeCurrency", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ShareCodes_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Counterparty", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ISIN", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SecurityCode", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SecurityDescription", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Market", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Sign", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("QuantityNominal", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("Price", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("AccruedInterest", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("Commission", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("Fees", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("MarketFee", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("Taxes", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("SettlementAmount", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("ExchangeRate", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("SettlementAmountCurr", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("OrderNumber", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("TradeID", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CancelledTradeID", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Against", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("OriginarySystem", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SettlementPlace", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RefNumber", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("MIC_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("PSET", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockExchange_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Depository_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Exported", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StockExchange_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Depository_Code", System.Type.GetType("System.String"));              

                conn.Open();
                cmd = new SqlCommand("GetCustodyCommands_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ServiceProvider_ID", _iServiceProvider_ID));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dDateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dDateTo));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["ServiceProvider_ID"] = drList["ServiceProvider_ID"];
                    dtRow["AktionDate"] = drList["AktionDate"];
                    dtRow["TradeDate"] = drList["TradeDate"];
                    dtRow["TradeTime"] = drList["TradeTime"];
                    dtRow["SettlementDate"] = drList["SettlementDate"];
                    dtRow["TradeCurrency"] = drList["TradeCurrency"];
                    dtRow["ShareCodes_ID"] = drList["ShareCodes_ID"];
                    dtRow["Counterparty"] = drList["Counterparty"];
                    dtRow["ISIN"] = drList["ISIN"];
                    dtRow["SecurityCode"] = drList["SecurityCode"];
                    dtRow["SecurityDescription"] = drList["SecurityDescription"];
                    dtRow["Market"] = drList["Market"];
                    dtRow["Sign"] = drList["Sign"];
                    dtRow["QuantityNominal"] = drList["QuantityNominal"];
                    dtRow["Price"] = drList["Price"];
                    dtRow["AccruedInterest"] = drList["AccruedInterest"];
                    dtRow["Commission"] = drList["Commission"];
                    dtRow["Fees"] = drList["Fees"];
                    dtRow["MarketFee"] = drList["MarketFee"];
                    dtRow["Taxes"] = drList["Taxes"];
                    dtRow["SettlementAmount"] = drList["SettlementAmount"];
                    dtRow["ExchangeRate"] = drList["ExchangeRate"];
                    dtRow["SettlementAmountCurr"] = drList["SettlementAmountCurr"];
                    dtRow["OrderNumber"] = drList["OrderNumber"];
                    dtRow["TradeID"] = drList["TradeID"];
                    dtRow["CancelledTradeID"] = drList["CancelledTradeID"];
                    dtRow["Against"] = drList["Against"];
                    dtRow["OriginarySystem"] = drList["OriginarySystem"];
                    dtRow["SettlementPlace"] = drList["SettlementPlace"];
                    dtRow["RefNumber"] = drList["RefNumber"];
                    dtRow["MIC_Code"] = drList["MIC_Code"];
                    dtRow["PSET"] = drList["PSET"];
                    dtRow["StockExchange_ID"] = drList["StockExchange_ID"];
                    dtRow["Depository_ID"] = drList["Depository_ID"];
                    dtRow["Exported"] = drList["Exported"];
                    dtRow["StockExchange_Code"] = drList["StockExchange_Code"];
                    dtRow["Depository_Code"] = drList["Depositories_Code"];
                    _dtList.Rows.Add(dtRow);
                 }
                drList.Close();
            }
            catch (Exception ex) {MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
  
        public int InsertRecord()
        {
            _iRecord_ID = 0;
            try
            {
                conn.Open();
                using (cmd = new SqlCommand("InsertCommands_ProvidersData", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ServiceProvider_ID", SqlDbType.Int).Value = _iServiceProvider_ID;
                    cmd.Parameters.Add("@AktionDate", SqlDbType.DateTime).Value = _dAktionDate;
                    cmd.Parameters.Add("@TradeDate", SqlDbType.DateTime).Value = _dTradeDate;
                    cmd.Parameters.Add("@TradeTime", SqlDbType.NVarChar, 10).Value = _sTradeTime;
                    cmd.Parameters.Add("@SettlementDate", SqlDbType.DateTime).Value = _dSettlementDate;
                    cmd.Parameters.Add("@TradeCurrency", SqlDbType.NVarChar, 60).Value = _sTradeCurrency;
                    cmd.Parameters.Add("@Counterparty", SqlDbType.NVarChar, 50).Value = _sCounterparty;
                    cmd.Parameters.Add("@ISIN", SqlDbType.NVarChar, 50).Value = _sISIN;
                    cmd.Parameters.Add("@SecurityCode", SqlDbType.NVarChar, 50).Value = _sSecurityCode;
                    cmd.Parameters.Add("@SecurityDescription", SqlDbType.NVarChar, 50).Value = _sSecurityDescription;
                    cmd.Parameters.Add("@Market", SqlDbType.NVarChar, 50).Value = _sMarket;
                    cmd.Parameters.Add("@Sign", SqlDbType.NVarChar, 10).Value = _sSign;
                    cmd.Parameters.Add("@QuantityNominal", SqlDbType.Decimal).Value = _decQuantityNominal;
                    cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = _decPrice;
                    cmd.Parameters.Add("@AccruedInterest", SqlDbType.Decimal).Value = _decAccruedInterest;
                    cmd.Parameters.Add("@Commission", SqlDbType.Decimal).Value = _decCommission;
                    cmd.Parameters.Add("@Fees", SqlDbType.Decimal).Value = _decFees;
                    cmd.Parameters.Add("@MarketFee", SqlDbType.Decimal).Value = _decMarketFee;
                    cmd.Parameters.Add("@Taxes", SqlDbType.Decimal).Value = _decTaxes;
                    cmd.Parameters.Add("@SettlementAmount", SqlDbType.Decimal).Value = _decSettlementAmount;
                    cmd.Parameters.Add("@ExchangeRate", SqlDbType.Decimal).Value = _decExchangeRate;
                    cmd.Parameters.Add("@SettlementAmountCurr", SqlDbType.Decimal).Value = _decSettlementAmountCurr;
                    cmd.Parameters.Add("@OrderNumber", SqlDbType.NVarChar, 20).Value = _sOrderNumber;
                    cmd.Parameters.Add("@TradeID", SqlDbType.NVarChar, 20).Value = _sTradeID;
                    cmd.Parameters.Add("@CancelledTradeID", SqlDbType.NVarChar, 20).Value = _sCancelledTradeID;
                    cmd.Parameters.Add("@Against", SqlDbType.NVarChar, 10).Value = _sAgainst;
                    cmd.Parameters.Add("@OriginarySystem", SqlDbType.NVarChar, 20).Value = _sOriginarySystem;
                    cmd.Parameters.Add("@SettlementPlace", SqlDbType.NVarChar, 20).Value = _sSettlementPlace;
                    cmd.Parameters.Add("@RefNumber", SqlDbType.NVarChar, 50).Value = _sRefNumber;
                    cmd.Parameters.Add("@MIC_Code", SqlDbType.NVarChar, 20).Value = _sMIC_Code;
                    cmd.Parameters.Add("@PSET", SqlDbType.NVarChar, 20).Value = _sPSET;
                    cmd.Parameters.Add("@StockExchange_ID", SqlDbType.Int).Value = _iStockExchange_ID;
                    cmd.Parameters.Add("@Depository_ID", SqlDbType.Int).Value = _iDepository_ID;
                    cmd.Parameters.Add("@Command_Execution_ID", SqlDbType.Int).Value = _iCommand_Execution_ID;
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
                using (SqlCommand cmd = new SqlCommand("EditCommands_ProvidersData", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@ServiceProvider_ID", SqlDbType.Int).Value = _iServiceProvider_ID;
                    cmd.Parameters.Add("@AktionDate", SqlDbType.DateTime).Value = _dAktionDate;
                    cmd.Parameters.Add("@TradeDate", SqlDbType.DateTime).Value = _dTradeDate;
                    cmd.Parameters.Add("@TradeTime", SqlDbType.NVarChar, 10).Value = _sTradeTime;
                    cmd.Parameters.Add("@SettlementDate", SqlDbType.DateTime).Value = _dSettlementDate;
                    cmd.Parameters.Add("@TradeCurrency", SqlDbType.NVarChar, 60).Value = _sTradeCurrency;
                    cmd.Parameters.Add("@Counterparty", SqlDbType.NVarChar, 50).Value = _sCounterparty;
                    cmd.Parameters.Add("@ISIN", SqlDbType.NVarChar, 50).Value = _sISIN;
                    cmd.Parameters.Add("@SecurityCode", SqlDbType.NVarChar, 50).Value = _sSecurityCode;
                    cmd.Parameters.Add("@SecurityDescription", SqlDbType.NVarChar, 50).Value = _sSecurityDescription;
                    cmd.Parameters.Add("@Market", SqlDbType.NVarChar, 50).Value = _sMarket;
                    cmd.Parameters.Add("@Sign", SqlDbType.NVarChar, 10).Value = _sSign;
                    cmd.Parameters.Add("@QuantityNominal", SqlDbType.Decimal).Value = _decQuantityNominal;
                    cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = _decPrice;
                    cmd.Parameters.Add("@AccruedInterest", SqlDbType.Decimal).Value = _decAccruedInterest;
                    cmd.Parameters.Add("@Commission", SqlDbType.Decimal).Value = _decCommission;
                    cmd.Parameters.Add("@Fees", SqlDbType.Decimal).Value = _decFees;
                    cmd.Parameters.Add("@MarketFee", SqlDbType.Decimal).Value = _decMarketFee;
                    cmd.Parameters.Add("@Taxes", SqlDbType.Decimal).Value = _decTaxes;
                    cmd.Parameters.Add("@SettlementAmount", SqlDbType.Decimal).Value = _decSettlementAmount;
                    cmd.Parameters.Add("@ExchangeRate", SqlDbType.Decimal).Value = _decExchangeRate;
                    cmd.Parameters.Add("@SettlementAmountCurr", SqlDbType.Decimal).Value = _decSettlementAmountCurr;
                    cmd.Parameters.Add("@OrderNumber", SqlDbType.NVarChar, 20).Value = _sOrderNumber;
                    cmd.Parameters.Add("@TradeID", SqlDbType.NVarChar, 20).Value = _sTradeID;
                    cmd.Parameters.Add("@CancelledTradeID", SqlDbType.NVarChar, 20).Value = _sCancelledTradeID;
                    cmd.Parameters.Add("@Against", SqlDbType.NVarChar, 10).Value = _sAgainst;
                    cmd.Parameters.Add("@OriginarySystem", SqlDbType.NVarChar, 20).Value = _sOriginarySystem;
                    cmd.Parameters.Add("@SettlementPlace", SqlDbType.NVarChar, 20).Value = _sSettlementPlace;
                    cmd.Parameters.Add("@RefNumber", SqlDbType.NVarChar, 50).Value = _sRefNumber;
                    cmd.Parameters.Add("@MIC_Code", SqlDbType.NVarChar, 20).Value = _sMIC_Code;
                    cmd.Parameters.Add("@PSET", SqlDbType.NVarChar, 20).Value = _sPSET;
                    cmd.Parameters.Add("@StockExchange_ID", SqlDbType.Int).Value = _iStockExchange_ID;
                    cmd.Parameters.Add("@Depository_ID", SqlDbType.Int).Value = _iDepository_ID;
                    cmd.Parameters.Add("@Command_Execution_ID", SqlDbType.Int).Value = _iCommand_Execution_ID;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Commands_ProvidersData";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int ServiceProvider_ID { get { return this._iServiceProvider_ID; } set { this._iServiceProvider_ID = value; } }
        public DateTime AktionDate { get { return this._dAktionDate; } set { this._dAktionDate = value; } }
        public DateTime TradeDate { get { return this._dTradeDate; } set { this._dTradeDate = value; } }
        public string TradeTime { get { return this._sTradeTime; } set { this._sTradeTime = value; } }
        public DateTime SettlementDate { get { return this._dSettlementDate; } set { this._dSettlementDate = value; } }
        public string TradeCurrency { get { return this._sTradeCurrency; } set { this._sTradeCurrency = value; } }
        public string Counterparty { get { return this._sCounterparty; } set { this._sCounterparty = value; } }
        public string ISIN { get { return this._sISIN; } set { this._sISIN = value; } }
        public string SecurityCode { get { return this._sSecurityCode; } set { this._sSecurityCode = value; } }
        public string SecurityDescription { get { return this._sSecurityDescription; } set { this._sSecurityDescription = value; } }
        public string Market { get { return this._sMarket; } set { this._sMarket = value; } }
        public string Sign { get { return this._sSign; } set { this._sSign = value; } }
        public Decimal QuantityNominal { get { return this._decQuantityNominal; } set { this._decQuantityNominal = value; } }
        public Decimal Price { get { return this._decPrice; } set { this._decPrice = value; } }
        public Decimal AccruedInterest { get { return this._decAccruedInterest; } set { this._decAccruedInterest = value; } }
        public Decimal Commission { get { return this._decCommission; } set { this._decCommission = value; } }
        public Decimal Fees { get { return this._decFees; } set { this._decFees = value; } }
        public Decimal MarketFee { get { return this._decMarketFee; } set { this._decMarketFee = value; } }
        public Decimal Taxes { get { return this._decTaxes; } set { this._decTaxes = value; } }
        public Decimal SettlementAmount { get { return this._decSettlementAmount; } set { this._decSettlementAmount = value; } }
        public Decimal ExchangeRate { get { return this._decExchangeRate; } set { this._decExchangeRate = value; } }
        public Decimal SettlementAmountCurr { get { return this._decSettlementAmountCurr; } set { this._decSettlementAmountCurr = value; } }
        public string OrderNumber { get { return this._sOrderNumber; } set { this._sOrderNumber = value; } }
        public string TradeID { get { return this._sTradeID; } set { this._sTradeID = value; } }
        public string CancelledTradeID { get { return this._sCancelledTradeID; } set { this._sCancelledTradeID = value; } }
        public string Against { get { return this._sAgainst; } set { this._sAgainst = value; } }
        public string OriginarySystem { get { return this._sOriginarySystem; } set { this._sOriginarySystem = value; } }
        public string SettlementPlace { get { return this._sSettlementPlace; } set { this._sSettlementPlace = value; } }
        public string RefNumber { get { return this._sRefNumber; } set { this._sRefNumber = value; } }
        public string MIC_Code { get { return this._sMIC_Code; } set { this._sMIC_Code = value; } }
        public string PSET { get { return this._sPSET; } set { this._sPSET = value; } }
        public int StockExchange_ID { get { return this._iStockExchange_ID; } set { this._iStockExchange_ID = value; } }
        public int Depository_ID { get { return this._iDepository_ID; } set { this._iDepository_ID = value; } }
        public int Command_ID { get { return this._iCommand_ID; } set { this._iCommand_ID = value; } }
        public int Command_Execution_ID { get { return this._iCommand_Execution_ID; } set { this._iCommand_Execution_ID = value; } }
        public int Exported { get { return this._iExported; } set { this._iExported = value; } }
        public DateTime DateFrom{ get { return this._dDateFrom; } set { this._dDateFrom = value; } }
        public DateTime DateTo { get { return this._dDateTo; } set { this._dDateTo = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}






