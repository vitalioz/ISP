using System;                                    
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsServiceProvidersCashAccounts
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int    _iRecord_ID;
        private int    _iServiceProvider_ID;
        private string _sCode;
        private string _sPortfolio;
        private string _sReutersCode;
        private string _sAccountNumber;
        private string _sCurrency;
        private string _sIBAN;

        private DataTable _dtList;

        public clsServiceProvidersCashAccounts()
        {
            this._iRecord_ID = 0;
            this._iServiceProvider_ID = 0;
            this._sCode = "";
            this._sPortfolio = "";
            this._sReutersCode = "";
            this._sAccountNumber = "";
            this._sCurrency = "";
            this._sIBAN = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ServiceProvidersCashAccounts"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iServiceProvider_ID = Convert.ToInt32(drList["ServiceProvider_ID"]);
                    this._sCode = drList["Code"] + "";
                    this._sPortfolio = "";
                    this._sReutersCode = "";
                    this._sAccountNumber = "";
                    this._sCurrency = "";
                    this._sIBAN = "";
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
                _dtList = new DataTable("ServiceProvidersCashAccounts_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AccountNumber", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("IBAN", System.Type.GetType("System.String"));

                dtRow = _dtList.NewRow();
                dtRow["ID"] = 0;
                dtRow["ServiceProvider_ID"] = 0;
                dtRow["Code"] = "";
                dtRow["Portfolio"] = "";
                dtRow["AccountNumber"] = "";
                dtRow["Currency"] = "";
                dtRow["IBAN"] = "";
                _dtList.Rows.Add(dtRow);

                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ServiceProvidersCashAccounts"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ServiceProvider_ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iServiceProvider_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", "Code"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["ServiceProvider_ID"] = drList["ServiceProvider_ID"];
                    dtRow["Code"] = drList["Code"];
                    dtRow["Portfolio"] = drList["Portfolio"];
                    dtRow["ReutersCode"] = drList["ReutersCode"];
                    dtRow["AccountNumber"] = drList["AccountNumber"];
                    dtRow["Currency"] = drList["Currency"];
                    dtRow["IBAN"] = drList["IBAN"];
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
                using (SqlCommand cmd = new SqlCommand("InsertServiceProvidersCashAccounts", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ServiceProvider_ID", SqlDbType.Int).Value = _iServiceProvider_ID;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 30).Value = _sCode;
                    cmd.Parameters.Add("@Portfolio", SqlDbType.NVarChar, 50).Value = _sPortfolio;
                    cmd.Parameters.Add("@AccountNumber", SqlDbType.NVarChar, 50).Value = _sAccountNumber;
                    cmd.Parameters.Add("@Currency", SqlDbType.NVarChar, 6).Value = _sReutersCode;
                    cmd.Parameters.Add("@IBAN", SqlDbType.NVarChar, 50).Value = _sIBAN;
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
                using (SqlCommand cmd = new SqlCommand("EditServiceProvidersCashAccounts", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@ServiceProvider_ID", SqlDbType.Int).Value = _iServiceProvider_ID;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 30).Value = _sCode;
                    cmd.Parameters.Add("@Portfolio", SqlDbType.NVarChar, 50).Value = _sPortfolio;
                    cmd.Parameters.Add("@AccountNumber", SqlDbType.NVarChar, 50).Value = _sAccountNumber;
                    cmd.Parameters.Add("@Currency", SqlDbType.NVarChar, 6).Value = _sReutersCode;
                    cmd.Parameters.Add("@IBAN", SqlDbType.NVarChar, 50).Value = _sIBAN;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ServiceProvidersCashAccounts";
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
        public string Code { get { return this._sCode; } set { this._sCode = value; } }
        public string Portfolio { get { return this._sPortfolio; } set { this._sPortfolio = value; } }
        public string AccountNumber { get { return this._sAccountNumber; } set { this._sAccountNumber = value; } }
        public string Currency { get { return this._sCurrency; } set { this._sCurrency = value; } }
        public string IBAN { get { return this._sIBAN; } set { this._sIBAN = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}






