using System;                                    //OK
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsClients_BankAccounts
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int       _iRecord_ID;
        private int       _iClient_ID;
        private int       _iBank_ID;
        private string    _sAccNumber;
        private decimal   _decStartBalance;
        private string    _sCurrency;
        private int       _iAccType;
        private string    _sAccOwners;
        private int       _iStatus;

        private string _sBank_Title;
        private DataTable _dtList;

        public clsClients_BankAccounts()
        {
            this._iRecord_ID = 0;
            this._iClient_ID = 0;
            this._iBank_ID = 0;
            this._sAccNumber = "";
            this._decStartBalance = 0;
            this._sCurrency = "";
            this._iAccType = 0;
            this._sAccOwners = "";
            this._iStatus = 0;
            this._sBank_Title = "";
        }
        public void GetRecord()
        {
            try
            {
                conn = new SqlConnection(Global.connStr);
                conn.Open();
                cmd = new SqlCommand("GetClient_BankAccount", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Record_ID", this._iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    this._iBank_ID = Convert.ToInt32(drList["Bank_ID"]);
                    this._sBank_Title = drList["Bank_Title"] + "";
                    this._sAccNumber = drList["AccNumber"] + "";
                    this._decStartBalance = Convert.ToDecimal(drList["StartBalance"]);
                    this._sCurrency = drList["Curr"] + "";
                    this._iAccType = Convert.ToInt32(drList["AccType"]);
                    this._sAccOwners = drList["AccOwners"] + "";
                    this._iStatus = Convert.ToInt32(drList["Status"]);
                }
                drList.Close();
            }
            catch (Exception ex) { string sTemp = ex.Message; /*MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);*/ }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            try
            {
                _dtList = new DataTable("BankAccounts_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Bank_ID", System.Type.GetType("System.Int32"));                
                dtCol = _dtList.Columns.Add("Account_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AccNumber", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AccType", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AccOwners", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StartBalance", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("BankTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int32"));

                /*
                dtRow = _dtList.NewRow();
                dtRow["ID"] = 0;
                dtRow["Client_ID"] = _iClient_ID;
                dtRow["Bank_ID"] = 0;
                dtRow["Account_ID"] = 0;
                dtRow["AccNumber"] = "";
                dtRow["AccType"] = 0;
                dtRow["AccOwners"] = "";
                dtRow["Currency"] = "";
                dtRow["StartBalance"] = 0;
                dtRow["BankTitle"] = "";
                dtRow["Status"] = 0;
                _dtList.Rows.Add(dtRow);
                */

                conn = new SqlConnection(Global.connStr);
                conn.Open();
                cmd = new SqlCommand("GetClient_BankAccounts", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Client_ID", _iClient_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["Client_ID"] = drList["Client_ID"];
                    dtRow["Bank_ID"] = drList["Bank_ID"];
                    dtRow["Account_ID"] = drList["Account_ID"];
                    dtRow["AccNumber"] = drList["AccNumber"];
                    dtRow["AccType"] = drList["AccType"];
                    dtRow["AccOwners"] = drList["AccOwners"];
                    dtRow["Currency"] = drList["Curr"];
                    dtRow["StartBalance"] = drList["StartBalance"];
                    dtRow["BankTitle"] = drList["BankTitle"];
                    dtRow["Status"] = drList["Status"];
                    _dtList.Rows.Add(dtRow);
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
                conn = new SqlConnection(Global.connStr);
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("InsertClientsBankAccounts", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Bank_ID", SqlDbType.Int).Value = _iBank_ID;
                    cmd.Parameters.Add("@AccNumber", SqlDbType.NVarChar, 50).Value = _sAccNumber;
                    cmd.Parameters.Add("@AccType", SqlDbType.Int).Value = _iAccType;
                    cmd.Parameters.Add("@AccOwners", SqlDbType.NVarChar, 500).Value = _sAccOwners;
                    cmd.Parameters.Add("@Curr", SqlDbType.NVarChar, 6).Value = _sCurrency;
                    cmd.Parameters.Add("@StartBalance", SqlDbType.Float).Value = _decStartBalance;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.ExecuteNonQuery();
                    _iRecord_ID = Convert.ToInt32(outParam.Value);
                }
            }
            catch (Exception ex) { string sTemp = ex.Message; /*MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);*/ }
            finally { conn.Close(); }

            return _iRecord_ID;
        }
        public int EditRecord()
        {
            try
            {
                conn = new SqlConnection(Global.connStr);
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("EditClientsBankAccounts", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Bank_ID", SqlDbType.Int).Value = _iBank_ID;
                    cmd.Parameters.Add("@AccNumber", SqlDbType.NVarChar, 50).Value = _sAccNumber;
                    cmd.Parameters.Add("@AccType", SqlDbType.Int).Value = _iAccType;
                    cmd.Parameters.Add("@AccOwners", SqlDbType.NVarChar, 500).Value = _sAccOwners;
                    cmd.Parameters.Add("@Curr", SqlDbType.NVarChar, 6).Value = _sCurrency;
                    cmd.Parameters.Add("@StartBalance", SqlDbType.Float).Value = _decStartBalance;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) {
                   string sTemp = ex.Message;
                   _iRecord_ID = 0; 
                   /*MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);*/ 
                }
            finally { conn.Close(); }

            return _iRecord_ID;
        }

        public void DeleteRecord()
        {
            try
            {
                conn = new SqlConnection(Global.connStr);
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ClientsBankAccounts";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int Client_ID { get { return this._iClient_ID; } set { this._iClient_ID = value; } }
        public int Bank_ID { get { return this._iBank_ID; } set { this._iBank_ID = value; } }
        public string AccNumber { get { return this._sAccNumber; } set { this._sAccNumber = value; } }        
        public decimal StartBalance { get { return this._decStartBalance; } set { this._decStartBalance = value; } }
        public string Currency { get { return this._sCurrency; } set { this._sCurrency = value; } }
        public int AccType { get { return this._iAccType; } set { this._iAccType = value; } }
        public string AccOwners { get { return this._sAccOwners; } set { this._sAccOwners = value; } }
        public int Status { get { return this._iStatus; } set { this._iStatus = value; } }
        public string Bank_Title { get { return this._sBank_Title; } set { this._sBank_Title = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}






