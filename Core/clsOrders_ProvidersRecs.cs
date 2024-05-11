using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsOrders_ProvidersRecs
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int _iRecord_ID;
        private int _iStockCompany_ID;
        private DateTime _dTradeDate;
        private string _sTradeTime;
        private DateTime _dSettlementDate;
        private string _sCompanyCode;
        private int _iCommand_ID;
        private string _sAktion;
        private string _sCode;
        private string _sPortfolio;
        private string _sClientName;
        private string _sContractTitle;
        private string _sSecurityCode;
        private string _sISIN;        
        private string _sSecurityDescription;
        private decimal _decQuantity;
        private decimal _decPrice;
        private string _sTradeCurrency;
        private decimal _decAccruedInterest;
        private decimal _decMarketFee;
        private int    _iStockExchange_ID;
        private string _sStockExchange_Code;
        private int     _iDepository_ID;
        private string _sDepository_Code;
        private string _sSettlementCurrency;
        private decimal _decCurrencyRate;
        private string _sNotes;
        private decimal _decFee;
        private string _sRefNumber;
        private decimal _decCommission;

        private DataTable _dtList;
        public clsOrders_ProvidersRecs()
        {
            this._iRecord_ID = 0;
            this._iStockCompany_ID = 0;
            this._dTradeDate = Convert.ToDateTime("01/01/1900");
            this._sTradeTime = "";
            this._dSettlementDate = Convert.ToDateTime("01/01/1900");
            this._sCompanyCode = "";
            this._iCommand_ID = 0;
            this._sAktion = "";
            this._sCode = "";
            this._sPortfolio = "";
            this._sClientName = "";
            this._sContractTitle = "";
            this._sSecurityCode = "";
            this._sISIN = "";
            this._sSecurityDescription = "";
            this._decQuantity = 0;
            this._decPrice = 0;
            this._sTradeCurrency = "";
            this._decAccruedInterest = 0;
            this._decMarketFee = 0;
            this._iStockExchange_ID = 0;
            this._sStockExchange_Code = "";
            this._iDepository_ID = 0;
            this._sDepository_Code = "";
            this._sSettlementCurrency = "";
            this._decCurrencyRate = 0;
            this._sNotes = "";
            this._decFee = 0;
            this._sRefNumber = "";
            this._decCommission = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();

                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Commands_ProvidersRecs"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", _iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iStockCompany_ID = Convert.ToInt32(drList["Tipos"]);
                    this._sTradeCurrency = drList["CC"] + "";
                    this._sCompanyCode = drList["CompanyCode"] + "";
                    this._sISIN = drList["ISIN"] + "";
                    this._sSecurityCode = drList["SecurityCode"] + "";
                    this._sSecurityDescription = drList["SecurityDescription"] + "";
                    this._decPrice = Convert.ToInt32(drList["InformMethod"]);
                    this._decQuantity = Convert.ToInt32(drList["Command_ID"]);
                    this._decAccruedInterest = Convert.ToInt32(drList["Source_ID"]);
                    this._decCommission = Convert.ToInt32(drList["Client_ID"]);
                    this._decFee = Convert.ToInt32(drList["Contract_ID "]);
                    this._decMarketFee = Convert.ToInt32(drList["Contract_ID"]);
                    this._decCurrencyRate = Convert.ToInt32(drList["SentAttempts"]);
                    this._sAktion = drList["Sign"] + "";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("TradeDate", typeof(DateTime));
            _dtList.Columns.Add("TradeTime", typeof(string));
            _dtList.Columns.Add("SettlementDate", typeof(DateTime));
            _dtList.Columns.Add("CompanyCode", typeof(string));
            _dtList.Columns.Add("Command_ID", typeof(int));
            _dtList.Columns.Add("Aktion", typeof(string));
            _dtList.Columns.Add("Code", typeof(string));
            _dtList.Columns.Add("Portfolio", typeof(string));
            _dtList.Columns.Add("ClientName", typeof(string));
            _dtList.Columns.Add("ContractTitle", typeof(string));
            _dtList.Columns.Add("SecurityCode", typeof(string));
            _dtList.Columns.Add("ISIN", typeof(string));
            _dtList.Columns.Add("SecurityDescription", typeof(string));
            _dtList.Columns.Add("Quantity", typeof(decimal));
            _dtList.Columns.Add("Price", typeof(decimal));
            _dtList.Columns.Add("TradeCurrency", typeof(string));
            _dtList.Columns.Add("AccruedInterest", typeof(decimal));
            _dtList.Columns.Add("MarketFee", typeof(decimal));
            _dtList.Columns.Add("StockExchange_ID", typeof(int));
            _dtList.Columns.Add("StockExchange_Code", typeof(string));
            _dtList.Columns.Add("Depository_ID", typeof(int));
            _dtList.Columns.Add("Depository_Code", typeof(string));
            _dtList.Columns.Add("SettlementCurrency", typeof(string));
            _dtList.Columns.Add("CurrencyRate", typeof(decimal));
            _dtList.Columns.Add("Notes", typeof(string));
            _dtList.Columns.Add("Fee", typeof(decimal));
            _dtList.Columns.Add("RefNumber", typeof(string));
            _dtList.Columns.Add("Commission", typeof(decimal));
            _dtList.Columns.Add("StockCompany_ID", typeof(int));
            _dtList.Columns.Add("Contract_ID", typeof(int));
            _dtList.Columns.Add("Contract_Details_ID", typeof(int));
            _dtList.Columns.Add("Contract_Packages_ID", typeof(int));
            _dtList.Columns.Add("ShareCodes_ID", typeof(int));
            _dtList.Columns.Add("Custodian_ID", typeof(int));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetCommands_ProvidersRecs", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@StockCompany_ID", _iStockCompany_ID));
                cmd.Parameters.Add(new SqlParameter("@TradeDate", _dTradeDate));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["TradeDate"], drList["TradeTime"], drList["SettlementDate"], drList["CompanyCode"], drList["Command_ID"],
                                     drList["Aktion"], drList["Code"], drList["Portfolio"], drList["ClientName"], drList["ContractTitle"],  drList["SecurityCode"],
                                     drList["ISIN"], drList["SecurityDescription"], drList["Quantity"], drList["Price"], drList["TradeCurrency"], drList["AccruedInterest"],
                                     drList["MarketFee"], drList["StockExchange_ID"], drList["StockExchange_Code"], drList["Depository_ID"], drList["Depository_Code"], 
                                     drList["SettlementCurrency"], drList["CurrencyRate"], drList["Notes"], drList["Fee"], drList["RefNumber"], drList["Commission"], 
                                     drList["StockCompany_ID"], drList["ClientPackage_ID"], drList["Contract_Details_ID"], drList["Contract_Packages_ID"], 
                                     drList["Share_ID"], drList["Custodian_ID"]);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }

        public int InsertRecord()
        {
            try
            {
                conn.Open();
                using (cmd = new SqlCommand("InsertCommands_ProvidersRecs", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@StockCompany_ID", SqlDbType.Int).Value = _iStockCompany_ID;
                    cmd.Parameters.Add("@TradeDate", SqlDbType.DateTime).Value = _dTradeDate;
                    cmd.Parameters.Add("@TradeTime", SqlDbType.NVarChar, 10).Value = _sTradeTime;
                    cmd.Parameters.Add("@SettlementDate", SqlDbType.DateTime).Value = _dSettlementDate;
                    cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar, 50).Value = _sCompanyCode;
                    cmd.Parameters.Add("@Command_ID", SqlDbType.Int).Value = _iCommand_ID;
                    cmd.Parameters.Add("@Aktion", SqlDbType.NVarChar, 10).Value = _sAktion;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 30).Value = _sCode;
                    cmd.Parameters.Add("@Portfolio", SqlDbType.NVarChar, 50).Value = _sPortfolio;
                    cmd.Parameters.Add("@ClientName", SqlDbType.NVarChar, 100).Value = _sClientName;
                    cmd.Parameters.Add("@ContractTitle", SqlDbType.NVarChar, 100).Value = _sContractTitle;
                    cmd.Parameters.Add("@SecurityCode", SqlDbType.NVarChar, 50).Value = _sSecurityCode;
                    cmd.Parameters.Add("@ISIN", SqlDbType.NVarChar, 50).Value = _sISIN;
                    cmd.Parameters.Add("@SecurityDescription", SqlDbType.NVarChar, 100).Value = _sSecurityDescription;
                    cmd.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = _decQuantity;
                    cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = _decPrice;
                    cmd.Parameters.Add("@TradeCurrency", SqlDbType.NVarChar, 6).Value = _sTradeCurrency;
                    cmd.Parameters.Add("@AccruedInterest", SqlDbType.Decimal).Value = _decAccruedInterest;
                    cmd.Parameters.Add("@MarketFee", SqlDbType.Decimal).Value = _decMarketFee;
                    cmd.Parameters.Add("@StockExchange_ID", SqlDbType.Int).Value = _iStockExchange_ID;
                    cmd.Parameters.Add("@StockExchange_Code", SqlDbType.NVarChar, 20).Value = _sStockExchange_Code;
                    cmd.Parameters.Add("@Depository_ID", SqlDbType.Int).Value = _iDepository_ID;
                    cmd.Parameters.Add("@Depository_Code", SqlDbType.NVarChar, 20).Value = _sDepository_Code;
                    cmd.Parameters.Add("@SettlementCurrency", SqlDbType.NVarChar, 6).Value = _sSettlementCurrency;                    
                    cmd.Parameters.Add("@CurrencyRate", SqlDbType.Decimal).Value = _decCurrencyRate;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 100).Value = _sNotes;
                    cmd.Parameters.Add("@Fee", SqlDbType.Decimal).Value = _decFee;
                    cmd.Parameters.Add("@RefNumber", SqlDbType.NVarChar, 50).Value = _sRefNumber;
                    cmd.Parameters.Add("@Commission", SqlDbType.Decimal).Value = _decCommission;
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
                using (cmd = new SqlCommand("EditCommands_ProvidersRecs", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@StockCompany_ID", SqlDbType.Int).Value = _iStockCompany_ID;
                    cmd.Parameters.Add("@TradeDate", SqlDbType.DateTime).Value = _dTradeDate;
                    cmd.Parameters.Add("@TradeTime", SqlDbType.NVarChar, 10).Value = _sTradeTime;
                    cmd.Parameters.Add("@SettlementDate", SqlDbType.DateTime).Value = _dSettlementDate;
                    cmd.Parameters.Add("@CompanyCode", SqlDbType.NVarChar, 50).Value = _sCompanyCode;
                    cmd.Parameters.Add("@Command_ID", SqlDbType.Int).Value = _iCommand_ID;
                    cmd.Parameters.Add("@Aktion", SqlDbType.NVarChar, 10).Value = _sAktion;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 30).Value = _sCode;
                    cmd.Parameters.Add("@Portfolio", SqlDbType.NVarChar, 50).Value = _sPortfolio;
                    cmd.Parameters.Add("@ClientName", SqlDbType.NVarChar, 100).Value = _sClientName;
                    cmd.Parameters.Add("@ContractTitle", SqlDbType.NVarChar, 100).Value = _sContractTitle;
                    cmd.Parameters.Add("@SecurityCode", SqlDbType.NVarChar, 50).Value = _sSecurityCode;
                    cmd.Parameters.Add("@ISIN", SqlDbType.NVarChar, 50).Value = _sISIN;
                    cmd.Parameters.Add("@SecurityDescription", SqlDbType.NVarChar, 100).Value = _sSecurityDescription;
                    cmd.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = _decQuantity;
                    cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = _decPrice;
                    cmd.Parameters.Add("@TradeCurrency", SqlDbType.NVarChar, 6).Value = _sTradeCurrency;
                    cmd.Parameters.Add("@AccruedInterest", SqlDbType.Decimal).Value = _decAccruedInterest;
                    cmd.Parameters.Add("@MarketFee", SqlDbType.Decimal).Value = _decMarketFee;
                    cmd.Parameters.Add("@StockExchange_ID", SqlDbType.Int).Value = _iStockExchange_ID;
                    cmd.Parameters.Add("@StockExchange_Code", SqlDbType.NVarChar, 20).Value = _sStockExchange_Code;
                    cmd.Parameters.Add("@Depository_ID", SqlDbType.Int).Value = _iDepository_ID;
                    cmd.Parameters.Add("@Depository_Code", SqlDbType.NVarChar, 20).Value = _sDepository_Code;
                    cmd.Parameters.Add("@SettlementCurrency", SqlDbType.NVarChar, 6).Value = _sSettlementCurrency;
                    cmd.Parameters.Add("@CurrencyRate", SqlDbType.Decimal).Value = _decCurrencyRate;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 100).Value = _sNotes;
                    cmd.Parameters.Add("@Fee", SqlDbType.Decimal).Value = _decFee;
                    cmd.Parameters.Add("@RefNumber", SqlDbType.NVarChar, 50).Value = _sRefNumber;
                    cmd.Parameters.Add("@Commission", SqlDbType.Decimal).Value = _decCommission;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Commands_ProvidersRecs";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return _iRecord_ID; } set { _iRecord_ID = value; } }
        public int StockCompany_ID { get { return _iStockCompany_ID; } set { _iStockCompany_ID = value; } }
        public DateTime TradeDate { get { return _dTradeDate; } set { _dTradeDate = value; } }
        public string TradeTime { get { return _sTradeTime; } set { _sTradeTime = value; } }
        public DateTime SettlementDate { get { return _dSettlementDate; } set { _dSettlementDate = value; } }
        public string CompanyCode { get { return _sCompanyCode; } set { _sCompanyCode = value; } }
        public int Command_ID { get { return _iCommand_ID; } set { _iCommand_ID = value; } }
        public string Aktion { get { return _sAktion; } set { _sAktion = value; } }
        public string Code { get { return _sCode; } set { _sCode = value; } }
        public string Portfolio { get { return _sPortfolio; } set { _sPortfolio = value; } }
        public string ClientName { get { return _sClientName; } set { _sClientName = value; } }
        public string ContractTitle { get { return _sContractTitle; } set { _sContractTitle = value; } }
        public string SecurityCode { get { return _sSecurityCode; } set { _sSecurityCode = value; } }
        public string ISIN { get { return _sISIN; } set { _sISIN = value; } }
        public string SecurityDescription { get { return _sSecurityDescription; } set { _sSecurityDescription = value; } }
        public decimal Quantity { get { return _decQuantity; } set { _decQuantity = value; } }
        public decimal Price { get { return _decPrice; } set { _decPrice = value; } }
        public string TradeCurrency { get { return _sTradeCurrency; } set { _sTradeCurrency = value; } }
        public decimal AccruedInterest { get { return _decAccruedInterest; } set { _decAccruedInterest = value; } }
        public decimal MarketFee { get { return _decMarketFee; } set { _decMarketFee = value; } }
        public int StockExchange_ID { get { return _iStockExchange_ID; } set { _iStockExchange_ID = value; } }
        public string StockExchange_Code { get { return _sStockExchange_Code; } set { _sStockExchange_Code = value; } }
        public int Depository_ID { get { return _iDepository_ID; } set { _iDepository_ID = value; } }
        public string Depository_Code { get { return _sDepository_Code; } set { _sDepository_Code = value; } }
        public string SettlementCurrency { get { return _sSettlementCurrency; } set { _sSettlementCurrency = value; } }
        public decimal CurrencyRate { get { return _decCurrencyRate; } set { _decCurrencyRate = value; } }
        public string Notes { get { return _sNotes; } set { _sNotes = value; } }
        public decimal Fee { get { return _decFee; } set { _decFee = value; } }
        public string RefNumber { get { return _sRefNumber; } set { _sRefNumber = value; } }
        public decimal Commission { get { return _decCommission; } set { _decCommission = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
