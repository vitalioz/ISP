using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsNewOrders
    {
        SqlConnection conn = new SqlConnection(Global.connFIXStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataRow dtRow;

        private int    _iRecord_ID;
        private int    _iSendFlag;
        private DateTime _dCurrentTimestamp;
        private string _sMsgType;
        private int    _iSequenceNumber;
        private char   _cPossDupFlag;
        private string _sClOrdID;
        private string _sSecurityID;
        private char   _cIDSource;
        private char   _cSide;
        private int    _iOrderQty;
        private string _sSymbol;
        private string _sExDestination;
        private char   _cOrdType;
        private string _sTimeInForce;
        private string _sPrice;
        private string _sCurrency;
        private string _sAccount;
        private string _sExpireDate;
        private string _sExpireTime;
        private char   _cRule80A;
        private string _sClientID;
        private string _sExecInst;
        private char   _cNFWBestExecutionIndicator;
        private string _sSettlLocation;
        private string _sText;
        private string _sSymbolSfx;
        private string _sOrigClOrdID;
        private string _sOrderID;

        private int _iInsType;
        private DateTime _dDateFrom;
        private DateTime _dDateTo;
        private DataTable _dtList;

        public clsNewOrders()
        {
            
            this._iRecord_ID = 0;
            this._iSendFlag = 0;
            this._dCurrentTimestamp = Convert.ToDateTime("1900/01/01");
            this._sMsgType = "";
            this._iSequenceNumber = 0;
            this._cPossDupFlag = char.Parse(" ");
            this._sClOrdID = "";
            this._sSecurityID = "";
            this._cIDSource = char.Parse(" ");
            this._cSide = char.Parse(" ");
            this._iOrderQty = 0;
            this._sSymbol = "";
            this._sExDestination = "";
            this._cOrdType = char.Parse(" ");
            this._sTimeInForce = " ";
            this._sPrice = ""; 
            this._sCurrency = "";
            this._sAccount = "";
            this._sExpireDate = "";
            this._sExpireTime = "";
            this._cRule80A = char.Parse(" ");
            this._sClientID = "";
            this._sExecInst = "";
            this._cNFWBestExecutionIndicator = char.Parse(" ");
            this._sSettlLocation = "";
            this._sText = "";
            this._sSymbolSfx = "";
            this._sOrigClOrdID = "";
            this._sOrderID = "";
            this._iInsType = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn = new SqlConnection(Global.connFIXStr);
                conn.Open();
                cmd = new SqlCommand("GetNewOrders", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iSendFlag = Convert.ToInt32(drList["SendFlag"]);
                    this._dCurrentTimestamp = Convert.ToDateTime(drList["CurrentTimestamp"]);
                    this._sMsgType = drList["MsgType"] + "";
                    this._iSequenceNumber = Convert.ToInt32(drList["SequenceNumber"]);
                    this._cPossDupFlag = char.Parse(drList["PossDupFlag"] + "");
                    this._sClOrdID = drList["ClOrdID"] + "";
                    this._sSecurityID = drList["SecurityID"] + "";
                    this._cIDSource = char.Parse(drList["cIDSource"] + "");
                    this._cSide = char.Parse(drList["cIDSourceRate"] + "");
                    this._iOrderQty = Convert.ToInt32(drList["OrderQty"]);
                    this._sSymbol = drList["Symbol"] +"";
                    this._sExDestination = drList["ExDestination"] +"";
                    this._cOrdType = char.Parse(drList["OrdType"] + "");
                    this._sTimeInForce = drList["TimeInForce"] + "";
                    this._sPrice = drList["Price"] + "";                   
                    this._sCurrency = drList["Currency"] + "";
                    this._sAccount = drList["sAccount"] + "";
                    this._sExpireDate = drList["ExpireDate"] + "";
                    this._sExpireTime = drList["ExpireTime"] + "";
                    this._cRule80A = char.Parse(drList["Rule80A"] + "");
                    this._sClientID = drList["ClientID"] + " ";
                    this._sExecInst = drList["ExecInst"] + " ";
                    this._cNFWBestExecutionIndicator = char.Parse(drList["NFWBestExecutionIndicator"] + ""); ;
                    this._sSettlLocation = drList["SettlLocation"] + "";
                    this._sText = drList["Text"] + "";
                    this._sSymbolSfx = drList["SymbolSfx"] + "";
                    this._sOrigClOrdID = drList["OrigClOrdID"] + "";
                    this._sOrderID = drList["OrderID"] + "";
                }
                drList.Close();
            }

            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("SendFlag", typeof(int));
            _dtList.Columns.Add("CurrentTimestamp", typeof(DateTime));
            _dtList.Columns.Add("MsgType", typeof(string));
            _dtList.Columns.Add("SequenceNumber", typeof(int));
            _dtList.Columns.Add("PossDupFlag", typeof(char));
            _dtList.Columns.Add("ClOrdID", typeof(string));
            _dtList.Columns.Add("SecurityID", typeof(string));
            _dtList.Columns.Add("IDSource", typeof(char));
            _dtList.Columns.Add("Side", typeof(char));
            _dtList.Columns.Add("OrderQty", typeof(int));
            _dtList.Columns.Add("Symbol", typeof(string));
            _dtList.Columns.Add("ExDestination", typeof(string));
            _dtList.Columns.Add("OrdType", typeof(char));
            _dtList.Columns.Add("TimeInForce", typeof(char));
            _dtList.Columns.Add("Price", typeof(string));
            _dtList.Columns.Add("Currency", typeof(string));
            _dtList.Columns.Add("Account", typeof(string));
            _dtList.Columns.Add("ExpireDate", typeof(string));
            _dtList.Columns.Add("ExpireTime", typeof(string));
            _dtList.Columns.Add("Rule80A", typeof(char));
            _dtList.Columns.Add("ClientID", typeof(string));
            _dtList.Columns.Add("ExecInst", typeof(string));
            _dtList.Columns.Add("NFWBestExecutionIndicator", typeof(char));
            _dtList.Columns.Add("SettlLocation", typeof(string));
            _dtList.Columns.Add("Text", typeof(string));
            _dtList.Columns.Add("SymbolSfx", typeof(string));
            _dtList.Columns.Add("OrigClOrdID", typeof(string));
            _dtList.Columns.Add("OrderID", typeof(string));

            try
            {
                conn = new SqlConnection(Global.connFIXStr);
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "NewOrders"));
                cmd.Parameters.Add(new SqlParameter("@Col", ""));
                cmd.Parameters.Add(new SqlParameter("@Value", ""));
                cmd.Parameters.Add(new SqlParameter("@Order", "ID"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["SendFlag"] = drList["SendFlag"];
                    dtRow["CurrentTimestamp"] = drList["CurrentTimestamp"];
                    dtRow["MsgType"] = drList["MsgType"];
                    dtRow["SequenceNumber"] = drList["SequenceNumber"];
                    dtRow["PossDupFlag"] = drList["PossDupFlag"];
                    dtRow["ClOrdID"] = drList["ClOrdID"];
                    dtRow["SecurityID"] = drList["SecurityID"];
                    dtRow["IDSource"] = drList["IDSource"];
                    dtRow["Side"] = drList["Side"];
                    dtRow["OrderQty"] = drList["OrderQty"];
                    dtRow["Symbol"] = drList["Symbol"];
                    dtRow["ExDestination"] = drList["ExDestination"];
                    dtRow["OrdType"] = drList["OrdType"];
                    dtRow["TimeInForce"] = drList["TimeInForce"];
                    dtRow["Price"] = drList["Price"];
                    dtRow["Currency"] = drList["Currency"];
                    dtRow["Account"] = drList["Account"];
                    dtRow["ExpireDate"] = drList["ExpireDate"];
                    dtRow["ExpireTime"] = drList["ExpireTime"];
                    dtRow["Rule80A"] = drList["Rule80A"];
                    dtRow["ClientID"] = drList["ClientID"];
                    dtRow["ExecInst"] = drList["ExecInst"];
                    dtRow["NFWBestExecutionIndicator"] = drList["NFWBestExecutionIndicator"];
                    dtRow["SettlLocation"] = drList["SettlLocation"];
                    dtRow["Text"] = drList["Text"];
                    dtRow["SymbolSfx"] = drList["SymbolSfx"];
                    dtRow["OrigClOrdID"] = drList["OrigClOrdID"];
                    dtRow["OrderID"] = drList["OrderID"];
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
                conn = new SqlConnection(Global.connFIXStr);
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("InsertNewOrders", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@InsType", SqlDbType.Int).Value = _iInsType;
                    cmd.Parameters.Add("@SendFlag", SqlDbType.Int).Value = _iSendFlag;
                    cmd.Parameters.Add("@CurrentTimestamp", SqlDbType.DateTime).Value = _dCurrentTimestamp;
                    cmd.Parameters.Add("@MsgType", SqlDbType.NVarChar, 2).Value = _sMsgType;
                    cmd.Parameters.Add("@SequenceNumber", SqlDbType.Int).Value = _iSequenceNumber;
                    cmd.Parameters.Add("@PossDupFlag", SqlDbType.Char, 1).Value = _cPossDupFlag; 
                    cmd.Parameters.Add("@ClOrdID", SqlDbType.NVarChar, 32).Value = _sClOrdID;
                    cmd.Parameters.Add("@SecurityID", SqlDbType.NVarChar, 16).Value = _sSecurityID;                   
                    cmd.Parameters.Add("@IDSource", SqlDbType.Char, 1).Value = _cIDSource;
                    cmd.Parameters.Add("@Side", SqlDbType.Char, 1).Value = _cSide;                   
                    cmd.Parameters.Add("@OrderQty", SqlDbType.Int).Value = _iOrderQty;
                    cmd.Parameters.Add("@Symbol", SqlDbType.NVarChar, 16).Value = _sSymbol;
                    cmd.Parameters.Add("@ExDestination", SqlDbType.NVarChar, 16).Value = _sExDestination;
                    cmd.Parameters.Add("@OrdType", SqlDbType.Char, 1).Value = _cOrdType;
                    cmd.Parameters.Add("@TimeInForce", SqlDbType.Char, 1).Value = _sTimeInForce;   // char.Parse(_sTimeInForce); 
                    cmd.Parameters.Add("@Price", SqlDbType.NVarChar, 32).Value = _sPrice;
                    cmd.Parameters.Add("@Currency", SqlDbType.NVarChar, 4).Value = _sCurrency;
                    cmd.Parameters.Add("@Account", SqlDbType.NVarChar, 32).Value = _sAccount;
                    cmd.Parameters.Add("@ExpireDate", SqlDbType.NVarChar, 32).Value = _sExpireDate;
                    cmd.Parameters.Add("@ExpireTime", SqlDbType.NVarChar, 32).Value = _sExpireTime;
                    cmd.Parameters.Add("@Rule80A", SqlDbType.Char, 1).Value = _cRule80A;
                    cmd.Parameters.Add("@ClientID", SqlDbType.NVarChar, 32).Value = _sClientID;
                    cmd.Parameters.Add("@ExecInst", SqlDbType.NVarChar, 32).Value = _sExecInst;
                    cmd.Parameters.Add("@NFWBestExecutionIndicator", SqlDbType.Char, 1).Value = _cNFWBestExecutionIndicator;
                    cmd.Parameters.Add("@SettlLocation", SqlDbType.NVarChar, 32).Value = _sSettlLocation;
                    cmd.Parameters.Add("@Text", SqlDbType.NVarChar, 64).Value = _sText;
                    cmd.Parameters.Add("@SymbolSfx", SqlDbType.NVarChar, 16).Value = _sSymbolSfx;
                    cmd.Parameters.Add("@OrigClOrdID", SqlDbType.NVarChar, 32).Value = _sOrigClOrdID;
                    cmd.Parameters.Add("@OrderID", SqlDbType.NVarChar, 32).Value = _sOrderID;
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
                conn = new SqlConnection(Global.connFIXStr);
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("EditNewOrders", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@SendFlag", SqlDbType.Int).Value = _iSendFlag;
                    cmd.Parameters.Add("@CurrentTimestamp", SqlDbType.DateTime).Value = _dCurrentTimestamp;
                    cmd.Parameters.Add("@MsgType", SqlDbType.NVarChar, 2).Value = _sMsgType;
                    cmd.Parameters.Add("@SequenceNumber", SqlDbType.Int).Value = _iSequenceNumber;
                    cmd.Parameters.Add("@PossDupFlag", SqlDbType.Char, 1).Value = _cPossDupFlag;
                    cmd.Parameters.Add("@ClOrdID", SqlDbType.NVarChar, 32).Value = _sClOrdID;
                    cmd.Parameters.Add("@SecurityID", SqlDbType.NVarChar, 16).Value = _sSecurityID;
                    cmd.Parameters.Add("@IDSource", SqlDbType.Char, 1).Value = _cIDSource;
                    cmd.Parameters.Add("@Side", SqlDbType.Char, 1).Value = _cSide;
                    cmd.Parameters.Add("@OrderQty", SqlDbType.Int).Value = _iOrderQty;
                    cmd.Parameters.Add("@Symbol", SqlDbType.NVarChar, 16).Value = _sSymbol;
                    cmd.Parameters.Add("@ExDestination", SqlDbType.NVarChar, 16).Value = _sExDestination;
                    cmd.Parameters.Add("@OrdType", SqlDbType.Char, 1).Value = _cOrdType;
                    cmd.Parameters.Add("@TimeInForce", SqlDbType.Char, 1).Value = char.Parse(_sTimeInForce);
                    cmd.Parameters.Add("@Price", SqlDbType.NVarChar, 32).Value = _sPrice;
                    cmd.Parameters.Add("@Currency", SqlDbType.NVarChar, 4).Value = _sCurrency;
                    cmd.Parameters.Add("@Account", SqlDbType.NVarChar, 32).Value = _sAccount;
                    cmd.Parameters.Add("@ExpireDate", SqlDbType.NVarChar, 32).Value = _sExpireDate;
                    cmd.Parameters.Add("@ExpireTime", SqlDbType.NVarChar, 32).Value = _sExpireTime;
                    cmd.Parameters.Add("@Rule80A", SqlDbType.Char, 1).Value = _cRule80A;
                    cmd.Parameters.Add("@ClientID", SqlDbType.NVarChar, 32).Value = _sClientID;
                    cmd.Parameters.Add("@ExecInst", SqlDbType.NVarChar, 32).Value = _sExecInst;
                    cmd.Parameters.Add("@NFWBestExecutionIndicator", SqlDbType.Char, 1).Value = _cNFWBestExecutionIndicator;
                    cmd.Parameters.Add("@SettlLocation", SqlDbType.NVarChar, 32).Value = _sSettlLocation;
                    cmd.Parameters.Add("@Text", SqlDbType.NVarChar, 64).Value = _sText;
                    cmd.Parameters.Add("@SymbolSfx", SqlDbType.NVarChar, 16).Value = _sSymbolSfx;
                    cmd.Parameters.Add("@OrigClOrdID", SqlDbType.NVarChar, 32).Value = _sOrigClOrdID;
                    cmd.Parameters.Add("@OrderID", SqlDbType.NVarChar, 32).Value = _sOrderID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }      
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int InsType { get { return this._iInsType; } set { this._iInsType = value; } }
        public int SendFlag { get { return this._iSendFlag; } set { this._iSendFlag = value; } }
        public DateTime CurrentTimestamp { get { return this._dCurrentTimestamp; } set { this._dCurrentTimestamp = value; } }
        public string MsgType { get { return this._sMsgType; } set { this._sMsgType = value; } }
        public int SequenceNumber { get { return this._iSequenceNumber; } set { this._iSequenceNumber = value; } }
        public char PossDupFlag { get { return this._cPossDupFlag; } set { this._cPossDupFlag = value; } }
        public string ClOrdID { get { return this._sClOrdID; } set { this._sClOrdID = value; } }
        public string SecurityID { get { return this._sSecurityID; } set { this._sSecurityID = value; } }       
        public char IDSource { get { return this._cIDSource; } set { this._cIDSource = value; } }
        public char Side { get { return this._cSide; } set { this._cSide = value; } }       
        public int OrderQty { get { return this._iOrderQty; } set { this._iOrderQty = value; } }
        public string Symbol { get { return this._sSymbol; } set { this._sSymbol = value; } }
        public string ExDestination { get { return this._sExDestination; } set { this._sExDestination = value; } }
        public char OrdType { get { return this._cOrdType; } set { this._cOrdType = value; } }
        public string TimeInForce { get { return this._sTimeInForce; } set { this._sTimeInForce = value; } }
        public string Price { get { return this._sPrice; } set { this._sPrice = value; } }        
        public string Currency { get { return this._sCurrency; } set { this._sCurrency = value; } }
        public string Account { get { return this._sAccount; } set { this._sAccount = value; } }       
        public string ExpireDate { get { return this._sExpireDate; } set { this._sExpireDate = value; } }
        public string ExpireTime { get { return this._sExpireTime; } set { this._sExpireTime = value; } }
        public char Rule80A { get { return this._cRule80A; } set { this._cRule80A = value; } }
        public string ClientID { get { return this._sClientID; } set { this._sClientID = value; } }
        public string ExecInst { get { return this._sExecInst; } set { this._sExecInst = value; } }
        public char NFWBestExecutionIndicator { get { return this._cNFWBestExecutionIndicator; } set { this._cNFWBestExecutionIndicator = value; } }
        public string SettlLocation { get { return this._sSettlLocation; } set { this._sSettlLocation = value; } }
        public string Text { get { return this._sText; } set { this._sText = value; } }
        public string SymbolSfx { get { return this._sSymbolSfx; } set { this._sSymbolSfx = value; } }
        public string OrigClOrdID { get { return this._sOrigClOrdID; } set { this._sOrigClOrdID = value; } }
        public string OrderID { get { return this._sOrderID; } set { this._sOrderID = value; } }
        public DateTime DateFrom { get { return this._dDateFrom; } set { this._dDateFrom = value; } }
        public DateTime DateTo { get { return this._dDateTo; } set { this._dDateTo = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}