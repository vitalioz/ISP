using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsDepositories
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int _iRecord_ID;
        private string _sTitle;
        private string _sCode;
        private string _sBIC;
        private int _iCountry_ID;

        private DataTable _dtList;

        public clsDepositories()
        {
            this._iRecord_ID = 0;
            this._sTitle = "";
            this._sCode = "";
            this._sBIC = "";
            this._iCountry_ID = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Depositories"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._sTitle = drList["Title"] + "";
                    this._sCode = drList["Code"] + "";
                    this._sBIC = drList["BIC"] + "";
                    this._iCountry_ID = Convert.ToInt32(drList["Country_ID"]);
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
                _dtList = new DataTable("Depositories_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("BIC", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Country_ID", System.Type.GetType("System.Int32"));

                dtRow = _dtList.NewRow();
                dtRow["ID"] = 0;
                dtRow["Title"] = "";
                dtRow["Code"] = "";
                dtRow["BIC"] = "";
                dtRow["Country_ID"] = 0;
                _dtList.Rows.Add(dtRow);

                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Depositories"));
                cmd.Parameters.Add(new SqlParameter("@Col", ""));
                cmd.Parameters.Add(new SqlParameter("@Value", ""));
                cmd.Parameters.Add(new SqlParameter("@Order", "Code"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["Title"] = drList["Title"] + "";
                    dtRow["Code"] = drList["Code"] + "";
                    dtRow["BIC"] = drList["BIC"] + "";
                    dtRow["Country_ID"] = drList["Country_ID"];
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
                using (SqlCommand cmd = new SqlCommand("InsertDepositories", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 50).Value = _sTitle;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 50).Value = _sCode;
                    cmd.Parameters.Add("@BIC", SqlDbType.NVarChar, 20).Value = _sBIC;
                    cmd.Parameters.Add("@Country_ID", SqlDbType.Int).Value = _iCountry_ID;
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
                using (SqlCommand cmd = new SqlCommand("EditDepositories", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 50).Value = _sTitle;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 50).Value = _sCode;
                    cmd.Parameters.Add("@BIC", SqlDbType.NVarChar, 20).Value = _sBIC;
                    cmd.Parameters.Add("@Country_ID", SqlDbType.Int).Value = _iCountry_ID;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Depositories";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public string Title { get { return this._sTitle; } set { this._sTitle = value; } }
        public string Code { get { return this._sCode; } set { this._sCode = value; } }
        public string BIC { get { return this._sBIC; } set { this._sBIC = value; } }
        public int Country_ID { get { return this._iCountry_ID; } set { this._iCountry_ID = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}
