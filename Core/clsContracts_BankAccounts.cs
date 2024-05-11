using System;                                    //OK
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsContracts_BankAccounts
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int       _iRecord_ID;
        private int       _iContract_ID;
        private int       _iAccount_ID;
        private string    _sAccNumber;
        private decimal   _decStartBalance;
        private string    _sCurrency;
        private int       _iAccType;
        private string    _sAccOwners;
        private int       _iAcc_ID;
        private string    _sBankTitle;

        private DataTable _dtList;

        public clsContracts_BankAccounts()
        {
            this._iRecord_ID = 0;
            this._iContract_ID = 0;
            this._iAccount_ID = 0;
            this._sAccNumber = "";
            this._decStartBalance = 0;
            this._sCurrency = "";
            this._iAccType = 0;
            this._sAccOwners = "";
            this._iAcc_ID = 0;
            this._sBankTitle = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ClientsCashAccounts"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iAccount_ID = Convert.ToInt32(drList["Account_ID"]);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            try
            {
                _dtList = new DataTable("BankAccounts_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Account_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AccNumber", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StartBalance", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AccType", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AccOwners", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("BankTitle", System.Type.GetType("System.String"));

                conn.Open();
                cmd = new SqlCommand("GetContract_Accounts", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Contract_ID", _iContract_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["Contract_ID"] = drList["ClientPackage_ID"];
                    dtRow["Account_ID"] = drList["Account_ID"];
                    dtRow["AccNumber"] = drList["AccNumber"];
                    dtRow["Currency"] = drList["Curr"];
                    dtRow["StartBalance"] = drList["StartBalance"];
                    dtRow["AccType"] = drList["AccType"];
                    dtRow["AccOwners"] = drList["AccOwners"];
                    dtRow["BankTitle"] = drList["BankTitle"];
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
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("InsertClientsFXFees", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;

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
                using (SqlCommand cmd = new SqlCommand("EditClientsFXFees", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;

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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ClientsFXFees";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int Contract_ID { get { return this._iContract_ID; } set { this._iContract_ID = value; } }
        public int Account_ID { get { return this._iAccount_ID; } set { this._iAccount_ID = value; } }
        public string AccNumber { get { return this._sAccNumber; } set { this._sAccNumber = value; } }
        public string Currency { get { return this._sCurrency; } set { this._sCurrency = value; } }
        public string AccOwners { get { return this._sAccOwners; } set { this._sAccOwners = value; } }
        public string BankTitle { get { return this._sBankTitle; } set { this._sBankTitle = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}






