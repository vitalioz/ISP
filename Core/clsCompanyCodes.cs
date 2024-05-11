using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsCompanyCodes
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int    _iRecord_ID;
        private string _sTitle;
        private string _sCode;
        private string _sPortfolio;
        private int    _iServiceProvider_ID;
        private string _sServiceProvider_Title;

        private DataTable _dtList;
        public clsCompanyCodes()
        {
            this._iRecord_ID = 0;
            this._sTitle = "";
            this._sCode = "";
            this._sPortfolio = "";
            this._iServiceProvider_ID = 0;
            this._sServiceProvider_Title = "";
        }
        public void GetRecord()
        {
            drList = null;
            try {
                conn.Open();
                cmd = new SqlCommand("sp_GetCompanyCodesList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@ServiceProvider_ID", this._iServiceProvider_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._sTitle = drList["Title"] + "";
                    this._sCode = drList["Code"] + "";
                    this._sPortfolio = drList["SubCode"] + "";                
                    this._iServiceProvider_ID = Convert.ToInt32(drList["ServiceProvider_ID"]);
                    this._sServiceProvider_Title = drList["ServiceProvider_Title"] + "";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("Title", typeof(string));
            _dtList.Columns.Add("Code", typeof(string));
            _dtList.Columns.Add("Portfolio", typeof(string));
            _dtList.Columns.Add("ServiceProvider_ID", typeof(int));
            _dtList.Columns.Add("ServiceProvider_Title", typeof(string));

            SqlDataReader drList = null;
            try
            {
                conn.Open();
                cmd = new SqlCommand("sp_GetCompanyCodesList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", 0));
                cmd.Parameters.Add(new SqlParameter("@ServiceProvider_ID", 0));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["Title"], drList["Code"], drList["SubCode"], drList["ServiceProvider_ID"], drList["ServiceProvider_Title"]);
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
                using (cmd = new SqlCommand("sp_InsertCompanyCodes", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 50).Value = _sTitle;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 50).Value = _sCode;
                    cmd.Parameters.Add("@Subcode", SqlDbType.NVarChar, 50).Value = _sPortfolio;
                    cmd.Parameters.Add("@ServiceProvider_ID", SqlDbType.Int).Value = _iServiceProvider_ID;
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
                using (cmd = new SqlCommand("sp_EditCompanyCodes", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 50).Value = _sTitle;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 50).Value = _sCode;
                    cmd.Parameters.Add("@Subcode", SqlDbType.NVarChar, 50).Value = _sPortfolio;
                    cmd.Parameters.Add("@ServiceProvider_ID", SqlDbType.Int).Value = _iServiceProvider_ID;
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
                using (cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Company_Codes";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return _iRecord_ID; } set { _iRecord_ID = value; } }
        public string Title { get { return _sTitle; } set { _sTitle = value; } }
        public string Code { get { return _sCode; } set { _sCode = value; } }
        public string Portfolio { get { return _sPortfolio; } set { _sPortfolio = value; } }
        public int ServiceProvider_ID { get { return _iServiceProvider_ID; } set { _iServiceProvider_ID = value; } }
        public string ServiceProvider_Title { get { return _sServiceProvider_Title; } set { _sServiceProvider_Title = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
