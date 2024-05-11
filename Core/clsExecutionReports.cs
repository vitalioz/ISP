using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsExecutionReports
    {
        SqlConnection conn = new SqlConnection(Global.connFIXStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataRow dtRow;

        private int      _iRecord_ID;
        private DateTime _dCurrentTimestamp;
        private string   _sMsgType;
        private int      _iSequenceNumber;
        private string   _sClOrdID;
        private string   _sOrigClOrdID;
        private char     _cOrdStatus;
        private string   _sText;
        private string   _sAccount;
        private char _cSide;
        private int _iOrderQty;
        private string _sPrice;
        private string _sCurrency;
        private char _cOrdType;
        private string _sOrderID;
        private char _cExecTransType;
        private char _cExecType;
        private string _sExecID;
        private string _sLastPx;
        private int _iLastQty;
        private int _iCumQty;
        private string _sExecInst;
        private int _iLeavesQty;
        private char _cRule80A;
        private char _cSettlType;
        private string _sSettlDate;
        private int _iOrdRejReason;
        private string _sTransactTime;
        private string _sLastMkt;
        private string _sTradeDate;
        private char _cNFWInternalizationIndicator;
        private string _sClientID;
        private string _sCommission;
        private string _sMiscFeeAmt;
        private string _sSettlCurrency;
        private string _sSettlLocation;
        private int    _iCxlRejResponseTo;
        private string _sCxlRejReason;

        private DateTime _dDateFrom;
        private DateTime _dDateTo;
        private DataTable _dtList;

        public clsExecutionReports()
        {
            this._iRecord_ID = 0;
            this._dCurrentTimestamp = Convert.ToDateTime("1900/01/01");
            this._sMsgType = "";
            this._iSequenceNumber = 0;
            this._sClOrdID = "";
            this._sOrigClOrdID = "";
            this._cOrdStatus = char.Parse(" ");
            this._sText = "";
            this._sAccount = "";
            this._cSide = char.Parse(" ");
            this._iOrderQty = 0;
            this._sPrice = "";
            this._sCurrency = "";
            this._cOrdType = char.Parse(" ");
            this._sOrderID = "";
            this._cExecTransType = char.Parse(" ");
            this._cExecType = char.Parse(" ");
            this._sExecID = "";
            this._sLastPx = "";
            this._iLastQty = 0;
            this._iCumQty = 0;
            this._sExecInst = "";
            this._iLeavesQty = 0;
            this._cRule80A = char.Parse(" ");
            this._cSettlType = char.Parse(" ");
            this._sSettlDate = "";
            this._iOrdRejReason = 0;
            this._sTransactTime = "";
            this._sLastMkt = "";
            this._sTradeDate = "";
            this._cNFWInternalizationIndicator = char.Parse(" ");
            this._sClientID = "";
            this._sCommission = "";
            this._sMiscFeeAmt = "";
            this._sSettlCurrency = "";
            this._sSettlLocation = "";
            this._iCxlRejResponseTo = 0;
            this._sCxlRejReason = "";
        }
        public void GetRecord()
        {
            try
            {
                conn = new SqlConnection(Global.connFIXStr);
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._dCurrentTimestamp = Convert.ToDateTime(drList["CurrentTimestamp"]);
                    this._sMsgType = drList["MsgType"] + "";
                    this._iSequenceNumber = Convert.ToInt32(drList["SequenceNumber"]);
                    this._sClOrdID = drList["ClOrdID"] + "";
                    this._sOrigClOrdID = drList["OrigClOrdID"] + "";
                    this._cOrdStatus = char.Parse(drList["OrdStatus"] + "");
                    this._sText = drList["Text"] + "";
                    this._sAccount = drList["sAccount"] + "";
                    this._cSide = char.Parse(drList["cIDSourceRate"] + "");
                    this._iOrderQty = Convert.ToInt32(drList["OrderQty"]);
                    this._sPrice = drList["Price"] + "";
                    this._sCurrency = drList["Currency"] + "";
                    this._cOrdType = char.Parse(drList["OrdType"] + "");
                    this._sOrderID = drList["OrderID"] + "";
                    this._cExecTransType = char.Parse(drList["ExecTransType"] + "");
                    this._cExecType = char.Parse(drList["ExecType"] + "");
                    this._sExecID = drList["ExecID"] + "";
                    this._sLastPx = drList["LastPx"] + "";
                    this._iLastQty = Convert.ToInt32(drList["LastQty"]);
                    this._iCumQty = Convert.ToInt32(drList["CumQty"]);
                    this._sExecInst = drList["ExecInst"] + "";
                    this._iLeavesQty = Convert.ToInt32(drList["LeavesQty"]);
                    this._cRule80A = char.Parse(drList["Rule80A"] + "");
                    this._cSettlType = char.Parse(drList["SellType"] + "");
                    this._sSettlDate = drList["SettlDate"] + "";
                    this._iOrdRejReason = Convert.ToInt32(drList["OrdRejReason"]);
                    this._sTransactTime = drList["TransactTime"] + "";
                    this._sLastMkt = drList["LastMkt"] + "";
                    this._sTradeDate = drList["TradeDate"] + "";
                    this._cNFWInternalizationIndicator = char.Parse(drList["NFWInternalizationIndicator"] + ""); ;
                    this._sClientID = drList["ClientID"] + " ";
                    this._sCommission = drList["Commission"] + "";
                    this._sMiscFeeAmt = drList["MiscFeeAmt"] + "";
                    this._sSettlCurrency = drList["SettlSettlCurrency"] + "";
                    this._sSettlLocation = drList["SettlLocation"] + "";
                    this._iCxlRejResponseTo = Convert.ToInt32(drList["CxlRejResponseTo"]);
                    this._sCxlRejReason = drList["CxlRejReason"] + "";
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
            _dtList.Columns.Add("CurrentTimestamp", typeof(DateTime));
            _dtList.Columns.Add("MsgType", typeof(string));
            _dtList.Columns.Add("SequenceNumber", typeof(int));
            _dtList.Columns.Add("ClOrdID", typeof(string));
            _dtList.Columns.Add("OrigClOrdID", typeof(string));
            _dtList.Columns.Add("OrdStatus", typeof(char));
            _dtList.Columns.Add("Text", typeof(string));
            _dtList.Columns.Add("Account", typeof(string));
            _dtList.Columns.Add("Side", typeof(char));
            _dtList.Columns.Add("OrderQty", typeof(int));
            _dtList.Columns.Add("Price", typeof(string));
            _dtList.Columns.Add("Currency", typeof(string));
            _dtList.Columns.Add("OrdType", typeof(char));
            _dtList.Columns.Add("OrderID", typeof(string));
            _dtList.Columns.Add("ExecTransType", typeof(char));
            _dtList.Columns.Add("ExecType", typeof(char));
            _dtList.Columns.Add("ExecID", typeof(string));
            _dtList.Columns.Add("LastPx", typeof(string));
            _dtList.Columns.Add("LastQty", typeof(int));
            _dtList.Columns.Add("CumQty", typeof(int));
            _dtList.Columns.Add("ExecInst", typeof(string));
            _dtList.Columns.Add("LeavesQty", typeof(int));
            _dtList.Columns.Add("Rule80A", typeof(char));
            _dtList.Columns.Add("SettlType", typeof(char));
            _dtList.Columns.Add("SettlDate", typeof(string));
            _dtList.Columns.Add("OrdRejReason", typeof(int));
            _dtList.Columns.Add("TransactTime", typeof(string));
            _dtList.Columns.Add("LastMkt", typeof(string));
            _dtList.Columns.Add("TradeDate", typeof(string));
            _dtList.Columns.Add("NFWInternalizationIndicator", typeof(char));
            _dtList.Columns.Add("ClientID", typeof(string));
            _dtList.Columns.Add("Commission", typeof(string));
            _dtList.Columns.Add("MiscFeeAmt", typeof(string));
            _dtList.Columns.Add("SettlCurrency", typeof(string));
            _dtList.Columns.Add("SettlLocation", typeof(string));
            _dtList.Columns.Add("CxlRejResponseTo", typeof(int));
            _dtList.Columns.Add("CxlRejReason", typeof(string));

            try
            {
                conn = new SqlConnection(Global.connFIXStr);
                conn.Open();
                cmd = new SqlCommand("GetExecutionReports", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ClOrdID", "%" + _sClOrdID.Replace("C", "") + "%"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["CurrentTimestamp"] = Convert.ToDateTime(drList["CurrentTimestamp"]);
                    dtRow["MsgType"] = drList["MsgType"];
                    dtRow["SequenceNumber"] = drList["SequenceNumber"];
                    dtRow["ClOrdID"] = drList["ClOrdID"];
                    dtRow["OrigClOrdID"] = drList["OrigClOrdID"];
                    dtRow["OrdStatus"] = drList["OrdStatus"];
                    dtRow["Text"] = drList["Text"];
                    dtRow["Account"] = drList["Account"];
                    dtRow["Side"] = drList["Side"];
                    dtRow["OrderQty"] = drList["OrderQty"];
                    dtRow["Price"] = drList["Price"];
                    dtRow["Currency"] = drList["Currency"];
                    dtRow["OrdType"] = drList["OrdType"];
                    dtRow["OrderID"] = drList["OrderID"];
                    dtRow["ExecTransType"] = drList["ExecTransType"];
                    dtRow["ExecType"] = drList["ExecType"];
                    dtRow["ExecID"] = drList["ExecID"];
                    dtRow["LastPx"] = drList["LastPx"];
                    dtRow["LastQty"] = drList["LastQty"];
                    dtRow["CumQty"] = drList["CumQty"];
                    dtRow["ExecInst"] = drList["ExecInst"];
                    dtRow["LeavesQty"] = drList["LeavesQty"];
                    dtRow["Rule80A"] = drList["Rule80A"];
                    dtRow["SettlType"] = drList["SettlType"];
                    dtRow["SettlDate"] = drList["SettlDate"];
                    dtRow["OrdRejReason"] = drList["OrdRejReason"];
                    dtRow["TransactTime"] = drList["TransactTime"];
                    dtRow["LastMkt"] = drList["LastMkt"];
                    dtRow["TradeDate"] = drList["TradeDate"];
                    dtRow["NFWInternalizationIndicator"] = drList["NFWInternalizationIndicator"];
                    dtRow["ClientID"] = drList["ClientID"];
                    dtRow["Commission"] = drList["Commission"];
                    dtRow["MiscFeeAmt"] = drList["MiscFeeAmt"];
                    dtRow["SettlCurrency"] = drList["SettlCurrency"];
                    dtRow["SettlLocation"] = drList["SettlLocation"];
                    dtRow["CxlRejResponseTo"] = drList["CxlRejResponseTo"];
                    dtRow["CxlRejReason"] = drList["CxlRejReason"];
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }      

        public void GetUncheckedList(DateTime dAktionDate)
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("CurrentTimestamp", typeof(DateTime));
            _dtList.Columns.Add("MsgType", typeof(string));
            _dtList.Columns.Add("SequenceNumber", typeof(int));
            _dtList.Columns.Add("ClOrdID", typeof(string));
            _dtList.Columns.Add("OrigClOrdID", typeof(string));
            _dtList.Columns.Add("OrdStatus", typeof(char));
            _dtList.Columns.Add("Text", typeof(string));
            _dtList.Columns.Add("Account", typeof(string));
            _dtList.Columns.Add("Side", typeof(char));
            _dtList.Columns.Add("OrderQty", typeof(int));
            _dtList.Columns.Add("Price", typeof(string));
            _dtList.Columns.Add("Currency", typeof(string));
            _dtList.Columns.Add("OrdType", typeof(char));
            _dtList.Columns.Add("OrderID", typeof(string));
            _dtList.Columns.Add("ExecTransType", typeof(char));
            _dtList.Columns.Add("ExecType", typeof(char));
            _dtList.Columns.Add("ExecID", typeof(string));
            _dtList.Columns.Add("LastPx", typeof(string));
            _dtList.Columns.Add("LastQty", typeof(int));
            _dtList.Columns.Add("CumQty", typeof(int));
            _dtList.Columns.Add("ExecInst", typeof(string));
            _dtList.Columns.Add("LeavesQty", typeof(int));
            _dtList.Columns.Add("Rule80A", typeof(char));
            _dtList.Columns.Add("SettlType", typeof(char));
            _dtList.Columns.Add("SettlDate", typeof(string));
            _dtList.Columns.Add("OrdRejReason", typeof(int));
            _dtList.Columns.Add("TransactTime", typeof(string));
            _dtList.Columns.Add("LastMkt", typeof(string));
            _dtList.Columns.Add("TradeDate", typeof(string));
            _dtList.Columns.Add("NFWInternalizationIndicator", typeof(char));
            _dtList.Columns.Add("ClientID", typeof(string));
            _dtList.Columns.Add("Commission", typeof(string));
            _dtList.Columns.Add("MiscFeeAmt", typeof(string));
            _dtList.Columns.Add("SettlCurrency", typeof(string));
            _dtList.Columns.Add("SettlLocation", typeof(string));
            _dtList.Columns.Add("CxlRejResponseTo", typeof(int));
            _dtList.Columns.Add("CxlRejReason", typeof(string));
            _dtList.Columns.Add("SecondOrdID", typeof(string));

            try
            {
                conn = new SqlConnection(Global.connFIXStr);
                conn.Open();
                cmd = new SqlCommand("GetExecutionReports_UncheckedList", conn);     
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AktionDate", dAktionDate));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["Id"];
                    dtRow["CurrentTimestamp"] = drList["CurrentTimestamp"];
                    dtRow["MsgType"] = drList["MsgType"];
                    dtRow["SequenceNumber"] = drList["SequenceNumber"];
                    dtRow["ClOrdID"] = drList["ClOrdID"];
                    dtRow["OrigClOrdID"] = drList["OrigClOrdID"];
                    dtRow["OrdStatus"] = drList["OrdStatus"];
                    dtRow["Text"] = drList["Text"];
                    dtRow["Account"] = drList["Account"];
                    dtRow["Side"] = drList["Side"];
                    dtRow["OrderQty"] = drList["OrderQty"];
                    dtRow["Price"] = drList["Price"];
                    dtRow["Currency"] = drList["Currency"];
                    dtRow["OrdType"] = drList["OrdType"];
                    dtRow["OrderID"] = drList["OrderID"];
                    dtRow["ExecTransType"] = drList["ExecTransType"];
                    dtRow["ExecType"] = drList["ExecType"];
                    dtRow["ExecID"] = drList["ExecID"];
                    dtRow["LastPx"] = drList["LastPx"];
                    dtRow["LastQty"] = drList["LastQty"];
                    dtRow["CumQty"] = drList["CumQty"];
                    dtRow["ExecInst"] = drList["ExecInst"];
                    dtRow["LeavesQty"] = drList["LeavesQty"];
                    dtRow["Rule80A"] = drList["Rule80A"];
                    dtRow["SettlType"] = drList["SettlType"];
                    dtRow["SettlDate"] = drList["SettlDate"];
                    dtRow["OrdRejReason"] = drList["OrdRejReason"];
                    dtRow["TransactTime"] = drList["TransactTime"];
                    dtRow["LastMkt"] = drList["LastMkt"];
                    dtRow["TradeDate"] = drList["TradeDate"];
                    dtRow["NFWInternalizationIndicator"] = drList["NFWInternalizationIndicator"];
                    dtRow["ClientID"] = drList["ClientID"];
                    dtRow["Commission"] = drList["Commission"];
                    dtRow["MiscFeeAmt"] = drList["MiscFeeAmt"];
                    dtRow["SettlCurrency"] = drList["SettlCurrency"];
                    dtRow["SettlLocation"] = drList["SettlLocation"];
                    dtRow["CxlRejResponseTo"] = drList["CxlRejResponseTo"];
                    dtRow["CxlRejReason"] = drList["CxlRejReason"];
                    dtRow["SecondOrdID"] = drList["SecondOrdID"] + "";                    
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
                using (SqlCommand cmd = new SqlCommand("InsertExecutionReports", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CurrentTimestamp", SqlDbType.DateTime).Value = _dCurrentTimestamp;
                    cmd.Parameters.Add("@MsgType", SqlDbType.NVarChar, 2).Value = _sMsgType;
                    cmd.Parameters.Add("@SequenceNumber", SqlDbType.Int).Value = _iSequenceNumber;
                    cmd.Parameters.Add("@ClOrdID", SqlDbType.NVarChar, 32).Value = _sClOrdID;
                    cmd.Parameters.Add("@OrigClOrdID", SqlDbType.NVarChar, 32).Value = _sOrigClOrdID;
                    cmd.Parameters.Add("@OrdStatus", SqlDbType.Char, 1).Value = _cOrdStatus;
                    cmd.Parameters.Add("@Text", SqlDbType.NVarChar, 64).Value = _sText;
                    cmd.Parameters.Add("@Account", SqlDbType.NVarChar, 32).Value = _sAccount;
                    cmd.Parameters.Add("@Side", SqlDbType.Char, 1).Value = _cSide;
                    cmd.Parameters.Add("@OrderQty", SqlDbType.Int).Value = _iOrderQty;
                    cmd.Parameters.Add("@Price", SqlDbType.NVarChar, 32).Value = _sPrice;
                    cmd.Parameters.Add("@Currency", SqlDbType.NVarChar, 4).Value = _sCurrency;
                    cmd.Parameters.Add("@OrdType", SqlDbType.Char, 1).Value = _cOrdType;
                    cmd.Parameters.Add("@OrderID", SqlDbType.NVarChar, 32).Value = _sOrderID;
                    cmd.Parameters.Add("@ExecTransType", SqlDbType.Char, 1).Value = _cExecTransType;
                    cmd.Parameters.Add("@ExecType", SqlDbType.Char, 1).Value = _cExecType;
                    cmd.Parameters.Add("@ExecID", SqlDbType.NVarChar, 32).Value = _sExecID;
                    cmd.Parameters.Add("@LastPx", SqlDbType.NVarChar, 32).Value = _sLastPx;
                    cmd.Parameters.Add("@LastQty", SqlDbType.Int).Value = _iLastQty;
                    cmd.Parameters.Add("@CumQty", SqlDbType.Int).Value = _iCumQty;
                    cmd.Parameters.Add("@ExecInst", SqlDbType.NVarChar, 32).Value = _sExecInst;
                    cmd.Parameters.Add("@LeavesQty", SqlDbType.Int).Value = _iLeavesQty;
                    cmd.Parameters.Add("@Rule80A", SqlDbType.Char, 1).Value = _cRule80A;
                    cmd.Parameters.Add("@SettlType", SqlDbType.Char, 1).Value = _cSettlType;
                    cmd.Parameters.Add("@SettlDate", SqlDbType.NVarChar, 8).Value = _sSettlDate;
                    cmd.Parameters.Add("@OrdRejReason", SqlDbType.Int).Value = _iOrdRejReason;
                    cmd.Parameters.Add("@TransactTime", SqlDbType.NVarChar, 32).Value = _sTransactTime;
                    cmd.Parameters.Add("@LastMkt", SqlDbType.NVarChar, 32).Value = _sLastMkt;
                    cmd.Parameters.Add("@TradeDate", SqlDbType.NVarChar, 8).Value = _sTradeDate;
                    cmd.Parameters.Add("@NFWInternalizationIndicator", SqlDbType.Char, 1).Value = _cNFWInternalizationIndicator;
                    cmd.Parameters.Add("@ClientID", SqlDbType.NVarChar, 32).Value = _sClientID;
                    cmd.Parameters.Add("@Commission", SqlDbType.NVarChar, 32).Value = _sCommission;
                    cmd.Parameters.Add("@MiscFeeAmt", SqlDbType.NVarChar, 32).Value = _sMiscFeeAmt;
                    cmd.Parameters.Add("@SettlCurrency", SqlDbType.NVarChar, 32).Value = _sSettlCurrency;
                    cmd.Parameters.Add("@SettlLocation", SqlDbType.NVarChar, 32).Value = _sSettlLocation;
                    cmd.Parameters.Add("@CxlRejResponseTo", SqlDbType.Int).Value = _iCxlRejResponseTo;
                    cmd.Parameters.Add("@CxlRejReason", SqlDbType.NVarChar, 32).Value = _sCxlRejReason;
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
                using (SqlCommand cmd = new SqlCommand("EditExecutionReports", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@CurrentTimestamp", SqlDbType.DateTime).Value = _dCurrentTimestamp;
                    cmd.Parameters.Add("@MsgType", SqlDbType.NVarChar, 2).Value = _sMsgType;
                    cmd.Parameters.Add("@SequenceNumber", SqlDbType.Int).Value = _iSequenceNumber;
                    cmd.Parameters.Add("@ClOrdID", SqlDbType.NVarChar, 32).Value = _sClOrdID;
                    cmd.Parameters.Add("@OrigClOrdID", SqlDbType.NVarChar, 32).Value = _sOrigClOrdID;
                    cmd.Parameters.Add("@OrdStatus", SqlDbType.Char, 1).Value = _cOrdStatus;
                    cmd.Parameters.Add("@Text", SqlDbType.NVarChar, 64).Value = _sText;
                    cmd.Parameters.Add("@Account", SqlDbType.NVarChar, 32).Value = _sAccount;
                    cmd.Parameters.Add("@Side", SqlDbType.Char, 1).Value = _cSide;
                    cmd.Parameters.Add("@OrderQty", SqlDbType.Int).Value = _iOrderQty;
                    cmd.Parameters.Add("@Price", SqlDbType.NVarChar, 32).Value = _sPrice;
                    cmd.Parameters.Add("@Currency", SqlDbType.NVarChar, 4).Value = _sCurrency;
                    cmd.Parameters.Add("@OrdType", SqlDbType.Char, 1).Value = _cOrdType;
                    cmd.Parameters.Add("@OrderID", SqlDbType.NVarChar, 32).Value = _sOrderID;
                    cmd.Parameters.Add("@ExecTransType", SqlDbType.Char, 1).Value = _cExecTransType;
                    cmd.Parameters.Add("@ExecType", SqlDbType.Char, 1).Value = _cExecType;
                    cmd.Parameters.Add("@ExecID", SqlDbType.NVarChar, 32).Value = _sExecID;
                    cmd.Parameters.Add("@LastPx", SqlDbType.NVarChar, 32).Value = _sLastPx;
                    cmd.Parameters.Add("@LastQty", SqlDbType.Int).Value = _iLastQty;
                    cmd.Parameters.Add("@CumQty", SqlDbType.Int).Value = _iCumQty;
                    cmd.Parameters.Add("@ExecInst", SqlDbType.NVarChar, 32).Value = _sExecInst;
                    cmd.Parameters.Add("@LeavesQty", SqlDbType.Int).Value = _iLeavesQty;
                    cmd.Parameters.Add("@Rule80A", SqlDbType.Char, 1).Value = _cRule80A;
                    cmd.Parameters.Add("@SettlType", SqlDbType.Char, 1).Value = _cSettlType;
                    cmd.Parameters.Add("@SettlDate", SqlDbType.NVarChar, 8).Value = _sSettlDate;
                    cmd.Parameters.Add("@OrdRejReason", SqlDbType.Int).Value = _iOrdRejReason;
                    cmd.Parameters.Add("@TransactTime", SqlDbType.NVarChar, 32).Value = _sTransactTime;
                    cmd.Parameters.Add("@LastMkt", SqlDbType.NVarChar, 32).Value = _sLastMkt;
                    cmd.Parameters.Add("@TradeDate", SqlDbType.NVarChar, 8).Value = _sTradeDate;
                    cmd.Parameters.Add("@NFWInternalizationIndicator", SqlDbType.Char, 1).Value = _cNFWInternalizationIndicator;
                    cmd.Parameters.Add("@ClientID", SqlDbType.NVarChar, 32).Value = _sClientID;
                    cmd.Parameters.Add("@Commission", SqlDbType.NVarChar, 32).Value = _sCommission;
                    cmd.Parameters.Add("@MiscFeeAmt", SqlDbType.NVarChar, 32).Value = _sMiscFeeAmt;
                    cmd.Parameters.Add("@SettlCurrency]", SqlDbType.NVarChar, 32).Value = _sSettlCurrency;
                    cmd.Parameters.Add("@SettlLocation", SqlDbType.NVarChar, 32).Value = _sSettlLocation;
                    cmd.Parameters.Add("@CxlRejResponseTo", SqlDbType.Int).Value = _iCxlRejResponseTo;
                    cmd.Parameters.Add("@CxlRejReason", SqlDbType.NVarChar, 32).Value = _sCxlRejReason;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public DateTime CurrentTimestamp { get { return this._dCurrentTimestamp; } set { this._dCurrentTimestamp = value; } }
        public string MsgType { get { return this._sMsgType; } set { this._sMsgType = value; } }
        public int SequenceNumber { get { return this._iSequenceNumber; } set { this._iSequenceNumber = value; } }
        public string ClOrdID { get { return this._sClOrdID; } set { this._sClOrdID = value; } }
        public string OrigClOrdID { get { return this._sOrigClOrdID; } set { this._sOrigClOrdID = value; } }
        public char OrdStatus { get { return this._cOrdStatus; } set { this._cOrdStatus = value; } }
        public string Text { get { return this._sText; } set { this._sText = value; } }
        public string Account { get { return this._sAccount; } set { this._sAccount = value; } }
        public char Side { get { return this._cSide; } set { this._cSide = value; } }
        public int OrderQty { get { return this._iOrderQty; } set { this._iOrderQty = value; } }
        public string Price { get { return this._sPrice; } set { this._sPrice = value; } }
        public string Currency { get { return this._sCurrency; } set { this._sCurrency = value; } }
        public char OrdType { get { return this._cOrdType; } set { this._cOrdType = value; } }
        public string OrderID { get { return this._sOrderID; } set { this._sOrderID = value; } }
        public char ExecTransType { get { return this._cExecTransType; } set { this._cExecTransType = value; } }
        public char ExecType { get { return this._cExecType; } set { this._cExecType = value; } }
        public string ExecID { get { return this._sExecID; } set { this._sExecID = value; } }
        public string LastPx { get { return this._sLastPx; } set { this._sLastPx = value; } }
        public int LastQty { get { return this._iLastQty; } set { this._iLastQty = value; } }
        public int CumQty { get { return this._iCumQty; } set { this._iCumQty = value; } }
        public string ExecInst { get { return this._sExecInst; } set { this._sExecInst = value; } }
        public int LeavesQty { get { return this._iLeavesQty; } set { this._iLeavesQty = value; } }
        public char Rule80A { get { return this._cRule80A; } set { this._cRule80A = value; } }
        public char SettlType { get { return this._cSettlType; } set { this._cSettlType = value; } }
        public string SettlDate { get { return this._sSettlDate; } set { this._sSettlDate = value; } }
        public int OrdRejReason { get { return this._iOrdRejReason; } set { this._iOrdRejReason = value; } }
        public string TransactTime { get { return this._sTransactTime; } set { this._sTransactTime = value; } }
        public string LastMkt { get { return this._sLastMkt; } set { this._sLastMkt = value; } }
        public string TradeDate { get { return this._sTradeDate; } set { this._sTradeDate = value; } }
        public char NFWInternalizationIndicator { get { return this._cNFWInternalizationIndicator; } set { this._cNFWInternalizationIndicator = value; } }
        public string ClientID { get { return this._sClientID; } set { this._sClientID = value; } }
        public string Commission { get { return this._sCommission; } set { this._sCommission = value; } }
        public string MiscFeeAmt { get { return this._sMiscFeeAmt; } set { this._sMiscFeeAmt = value; } }
        public string SettlCurrency { get { return this._sSettlCurrency; } set { this._sSettlCurrency = value; } }
        public string SettlLocation { get { return this._sSettlLocation; } set { this._sSettlLocation = value; } }
        public int CxlRejResponseTo { get { return this._iCxlRejResponseTo; } set { this._iCxlRejResponseTo = value; } }
        public string CxlRejReason { get { return this._sCxlRejReason; } set { this._sCxlRejReason = value; } } 
        public DateTime DateFrom { get { return this._dDateFrom; } set { this._dDateFrom = value; } }
        public DateTime DateTo { get { return this._dDateTo; } set { this._dDateTo = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}