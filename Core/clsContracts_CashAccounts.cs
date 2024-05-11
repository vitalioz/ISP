using System;                                    //OK
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsContracts_CashAccounts
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList;
        DataColumn dtCol;
        DataRow dtRow;

        private int       _iRecord_ID;
        private int       _iContract_ID;
        private int       _iClient_ID;
        private string    _sCode;
        private string    _sPortfolio;
        private string    _sAccountNumber;
        private string    _sAccountNumber2;
        private string    _sCurrency;
        private string    _sIBAN;
        private int       _iStatus;
        private int       _iPackageType;
        private int       _iProvider_ID;

        private DataTable _dtList;

        public clsContracts_CashAccounts()
        {
            this._iRecord_ID = 0;
            this._iContract_ID = 0;
            this._iClient_ID = 0;
            this._sCode = "";
            this._sPortfolio = "";
            this._sAccountNumber = "";
            this._sAccountNumber2 = "";
            this._sCurrency = "";
            this._sIBAN = "";
            this._iStatus = 0;
            this._iPackageType = 0;
            this._iProvider_ID = 0;
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
                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    this._sCode = drList["Code"] + "";
                    this._sPortfolio = drList["Portfolio"] + "";
                    this._sAccountNumber = drList["AccountNumber"] + "";
                    this._sAccountNumber2 = drList["AccountNumber2"] + "";
                    this._sCurrency = drList["Currency"] + "";
                    this._sIBAN = drList["IBAN"] + "";
                    this._iStatus = Convert.ToInt32(drList["Status"]);
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
                _dtList = new DataTable("CashAccounts_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AccountNumber", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AccountNumber2", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("IBAN", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetClientCashAccounts", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Client_ID", _iClient_ID));
                cmd.Parameters.Add(new SqlParameter("@Contract_ID", _iContract_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["Client_ID"] = drList["Client_ID"];
                    dtRow["Contract_ID"] = drList["Contract_ID"];    
                    dtRow["Code"] = drList["Code"];
                    dtRow["Portfolio"] = drList["Portfolio"];
                    dtRow["AccountNumber"] = drList["AccountNumber"];
                    dtRow["AccountNumber2"] = drList["AccountNumber2"];
                    dtRow["Currency"] = drList["Currency"];
                    dtRow["IBAN"] = drList["IBAN"];
                    dtRow["Status"] = drList["Status"];
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList_CashAccount()
        {
            try
            {
                _dtList = new DataTable("CashAccounts_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AccountNumber", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AccountNumber2", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("IBAN", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int32"));

                dtRow = _dtList.NewRow();
                dtRow["ID"] = 0;
                dtRow["Client_ID"] = 0;
                dtRow["Contract_ID"] = 0;
                dtRow["Code"] = "";
                dtRow["Portfolio"] = "";
                dtRow["AccountNumber"] = "";
                dtRow["AccountNumber2"] = "";
                dtRow["Currency"] = "";
                dtRow["IBAN"] = "";
                dtRow["Status"] = 0;
                _dtList.Rows.Add(dtRow);

                conn.Open();
                cmd = new SqlCommand("GetContract_CashAccounts", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Code", _sCode));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if (_iContract_ID == 0 || _iContract_ID == Convert.ToInt32(drList["Contract_ID"]))  {
                        dtRow = _dtList.NewRow();
                        dtRow["ID"] = drList["ID"];
                        dtRow["Client_ID"] = drList["Client_ID"];
                        dtRow["Contract_ID"] = drList["Contract_ID"];
                        dtRow["Code"] = drList["Code"];
                        dtRow["Portfolio"] = drList["Portfolio"];
                        dtRow["AccountNumber"] = drList["AccountNumber"];
                        dtRow["AccountNumber2"] = drList["AccountNumber2"];
                        dtRow["Currency"] = drList["Currency"];
                        dtRow["IBAN"] = drList["IBAN"];
                        dtRow["Status"] = drList["Status"];
                        _dtList.Rows.Add(dtRow);
                    }
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList_PackageType()
        {
            try
            {
                _dtList = new DataTable("CashAccounts_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AccountNumber", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AccountNumber2", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("IBAN", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetClients_CashAccounts_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PackageType", _iPackageType));
                cmd.Parameters.Add(new SqlParameter("@Client_ID", _iClient_ID));
                cmd.Parameters.Add(new SqlParameter("@Provider_ID", _iProvider_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["Client_ID"] = drList["Client_ID"];
                    dtRow["Contract_ID"] = drList["Contract_ID"];
                    dtRow["Code"] = drList["Code"];
                    dtRow["Portfolio"] = drList["Portfolio"];
                    dtRow["AccountNumber"] = drList["AccountNumber"];
                    dtRow["AccountNumber2"] = drList["AccountNumber2"];
                    dtRow["Currency"] = drList["Currency"];
                    dtRow["IBAN"] = drList["IBAN"];
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
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("InsertClientCashAccount", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 30).Value = _sCode;
                    cmd.Parameters.Add("@Portfolio", SqlDbType.NVarChar, 50).Value = _sPortfolio;
                    cmd.Parameters.Add("@AccountNumber", SqlDbType.NVarChar, 50).Value = _sAccountNumber;
                    cmd.Parameters.Add("@AccountNumber2", SqlDbType.NVarChar, 50).Value = _sAccountNumber2;
                    cmd.Parameters.Add("@Currency", SqlDbType.NVarChar, 60).Value = _sCurrency;
                    cmd.Parameters.Add("@IBAN", SqlDbType.NVarChar, 50).Value = _sIBAN;
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
                using (SqlCommand cmd = new SqlCommand("EditClientCashAccount", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 30).Value = _sCode;
                    cmd.Parameters.Add("@Portfolio", SqlDbType.NVarChar, 50).Value = _sPortfolio;
                    cmd.Parameters.Add("@AccountNumber", SqlDbType.NVarChar, 50).Value = _sAccountNumber;
                    cmd.Parameters.Add("@AccountNumber2", SqlDbType.NVarChar, 50).Value = _sAccountNumber2;
                    cmd.Parameters.Add("@Currency", SqlDbType.NVarChar, 60).Value = _sCurrency;
                    cmd.Parameters.Add("@IBAN", SqlDbType.NVarChar, 50).Value = _sIBAN;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ClientsCashAccounts";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void DeleteRecord_Client_ID()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ClientsCashAccounts";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "Client_ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iClient_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int Client_ID { get { return this._iClient_ID; } set { this._iClient_ID = value; } }
        public int Contract_ID { get { return this._iContract_ID; } set { this._iContract_ID = value; } }
        public string Code { get { return this._sCode; } set { this._sCode = value; } }
        public string Portfolio { get { return this._sPortfolio; } set { this._sPortfolio = value; } }
        public string AccountNumber { get { return this._sAccountNumber; } set { this._sAccountNumber = value; } }
        public string AccountNumber2 { get { return this._sAccountNumber2; } set { this._sAccountNumber2 = value; } }
        public string Currency { get { return this._sCurrency; } set { this._sCurrency = value; } }
        public string IBAN { get { return this._sIBAN; } set { this._sIBAN = value; } }
        public int Status { get { return this._iStatus; } set { this._iStatus = value; } }
        public int PackageType { get { return this._iPackageType; } set { this._iPackageType = value; } }
        public int Provider_ID { get { return this._iProvider_ID; } set { this._iProvider_ID = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}