using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsOrders_Executions
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int _iRecord_ID;
        private int _iCommand_ID;
        private DateTime _dDateExecution;
        private string _sStockExchange_MIC;
        private string _sProviderCommandNumber;
        private decimal _decRealPrice;
        private decimal _decRealQuantity;
        private decimal _decRealAmount;
        private decimal _decAccruedInterest;

        private DataTable _dtList;
        public clsOrders_Executions()
        {
            this._iRecord_ID = 0;
            this._iCommand_ID = 0;
            this._dDateExecution = Convert.ToDateTime("1900/01/01");
            this._sStockExchange_MIC = "";
            this._sProviderCommandNumber = "";
            this._decRealPrice = 0;
            this._decRealQuantity = 0;
            this._decRealAmount = 0;
            this._decAccruedInterest = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Commands_Executions"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iCommand_ID = Convert.ToInt32(drList["Command_ID"]);
                    this._dDateExecution = Convert.ToDateTime(drList["DateExecution"]);
                    this._sStockExchange_MIC = drList["StockExchange_MIC"] + "";
                    this._sProviderCommandNumber = drList["ProviderCommandNumber"] + "";
                    this._decRealPrice = Convert.ToDecimal(drList["RealPrice"]);
                    this._decRealQuantity = Convert.ToDecimal(drList["RealPrice"]);
                    this._decRealAmount = Convert.ToDecimal(drList["RealAmount"]);
                    this._decAccruedInterest = Convert.ToDecimal(drList["AccruedInterest"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("Command_ID", typeof(int));
            _dtList.Columns.Add("DateExecution", typeof(DateTime));
            _dtList.Columns.Add("StockExchange_MIC", typeof(string));
            _dtList.Columns.Add("ProviderCommandNumber", typeof(string));
            _dtList.Columns.Add("RealPrice", typeof(decimal));
            _dtList.Columns.Add("RealQuantity", typeof(decimal));
            _dtList.Columns.Add("RealAmount", typeof(decimal));
            _dtList.Columns.Add("AccruedInterest", typeof(decimal));

            SqlDataReader drList = null;
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetCommands_Executions", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProviderCommandNumber", _sProviderCommandNumber));
                cmd.Parameters.Add(new SqlParameter("@Command_ID", this._iCommand_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["Command_ID"], drList["DateExecution"], drList["StockExchange_MIC"], drList["ProviderCommandNumber"], 
                                     drList["RealPrice"], drList["RealQuantity"], drList["RealAmount"], drList["AccruedInterest"]);
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
                using (SqlCommand cmd = new SqlCommand("sp_InsertCommand_Executions", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Command_ID", SqlDbType.Int).Value = _iCommand_ID;
                    cmd.Parameters.Add("@DateExecution", SqlDbType.DateTime).Value = _dDateExecution;
                    cmd.Parameters.Add("@StockExchange_MIC", SqlDbType.NVarChar, 50).Value = _sStockExchange_MIC;
                    cmd.Parameters.Add("@ProviderCommandNumber", SqlDbType.NVarChar, 100).Value = _sProviderCommandNumber;
                    cmd.Parameters.Add("@RealPrice", SqlDbType.Decimal).Value = _decRealPrice;
                    cmd.Parameters.Add("@RealQuantity", SqlDbType.Decimal).Value = _decRealQuantity;
                    cmd.Parameters.Add("@RealAmount", SqlDbType.Decimal).Value = _decRealAmount;
                    cmd.Parameters.Add("@AccruedInterest", SqlDbType.Decimal).Value = _decAccruedInterest;

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
                using (SqlCommand cmd = new SqlCommand("sp_EditCommand_Executions", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Command_ID", SqlDbType.Int).Value = _iCommand_ID;
                    cmd.Parameters.Add("@DateExecution", SqlDbType.DateTime).Value = _dDateExecution;
                    cmd.Parameters.Add("@StockExchange_MIC", SqlDbType.NVarChar, 50).Value = _sStockExchange_MIC;
                    cmd.Parameters.Add("@ProviderCommandNumber", SqlDbType.NVarChar, 100).Value = _sProviderCommandNumber;
                    cmd.Parameters.Add("@RealPrice", SqlDbType.Decimal).Value = _decRealPrice;
                    cmd.Parameters.Add("@RealQuantity", SqlDbType.Decimal).Value = _decRealQuantity;
                    cmd.Parameters.Add("@RealAmount", SqlDbType.Decimal).Value = _decRealAmount;
                    cmd.Parameters.Add("@AccruedInterest", SqlDbType.Decimal).Value = _decAccruedInterest;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Commands_Executions";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return _iRecord_ID; } set { _iRecord_ID = value; } }
        public int Command_ID { get { return _iCommand_ID; } set { _iCommand_ID = value; } }
        public DateTime DateExecution { get { return _dDateExecution; } set { _dDateExecution = value; } }
        public string StockExchange_MIC { get { return _sStockExchange_MIC; } set { _sStockExchange_MIC = value; } }
        public string ProviderCommandNumber { get { return _sProviderCommandNumber; } set { _sProviderCommandNumber = value; } }
        public decimal RealPrice { get { return _decRealPrice; } set { _decRealPrice = value; } }
        public decimal RealQuantity { get { return _decRealQuantity; } set { _decRealQuantity = value; } }
        public decimal RealAmount { get { return _decRealAmount; } set { _decRealAmount = value; } }
        public decimal AccruedInterest { get { return _decAccruedInterest; } set { _decAccruedInterest = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
