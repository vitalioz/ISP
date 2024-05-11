using System;
using System.Data;
using System.Data.SqlClient;

namespace Core
{
    public class clsWebUsersStates
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataRow dtRow;

        private int    _iRecord_ID;
        private int    _iWU_ID;
        private int    _iStatus;
        private string _sEmail;
        private string _sMobile;

        private int _iClient_ID;
        private DataTable _dtList;

        public clsWebUsersStates()
        {
            this._iRecord_ID = 0;
            this._iWU_ID = 0;
            this._iClient_ID = 0;
            this._iStatus = 0;
            this._sEmail = "";
            this._sMobile = "";
        }
        public void GetRecord()
        {
            try
            {
                conn = new SqlConnection(Global.connStr);
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "WebUsersStates"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", _iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iWU_ID = Convert.ToInt32(drList["WU_ID"]);
                    this._iStatus = Convert.ToInt32(drList["Status"]);
                    this._sEmail = drList["Email"] + "";
                    this._sMobile = drList["Mobile"] + "";
                }
            }
            catch (Exception ex)
            {
                string sTemp = ex.Message;
            }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("WU_ID", typeof(int));
            _dtList.Columns.Add("Status", typeof(int));
            _dtList.Columns.Add("Email", typeof(string));
            _dtList.Columns.Add("Mobile", typeof(string));

            try
            {
                conn = new SqlConnection(Global.connStr);
                conn.Open();

                SqlCommand cmd = new SqlCommand("GetWebUsersStates", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@WU_ID", _iWU_ID));

                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["WU_ID"] = drList["WU_ID"];
                    dtRow["Status"] = Convert.ToInt32(drList["Status"]);
                    dtRow["Email"] = drList["Email"] + "";
                    dtRow["Mobile"] = drList["Mobile"] + "";
                    _dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex)
            {
                string sTemp = ex.Message;
            }
            finally { conn.Close(); }
        }

        public int InsertRecord()
        {
            try
            {
                conn = new SqlConnection(Global.connStr);
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("InsertWebUsersStates", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@WU_ID", SqlDbType.Int).Value = _iWU_ID;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 30).Value = _sEmail;
                    cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar, 20).Value = _sMobile;
                    cmd.ExecuteNonQuery();
                    _iRecord_ID = Convert.ToInt32(outParam.Value);
                }
            }
            catch (Exception ex)
            {
                string sTemp = ex.Message;
            }
            finally { conn.Close(); }

            return _iRecord_ID;
        }
        public int EditRecord()
        {
            try
            {
                conn = new SqlConnection(Global.connStr);
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("EditWebUsersStates", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@WU_ID", SqlDbType.Int).Value = _iWU_ID;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 30).Value = _sEmail;
                    cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar, 20).Value = _sMobile;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string sTemp = ex.Message;
            }
            finally { conn.Close(); }

            return _iRecord_ID;
        }
        public int EditStatus()
        {
            try
            {
                conn = new SqlConnection(Global.connStr);
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("EditWebUsersStates_Status", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string sTemp = ex.Message;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "WebUsersStates";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string sTemp = ex.Message;
            }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int WU_ID { get { return _iWU_ID; } set { _iWU_ID = value; } }
        public int Client_ID { get { return _iClient_ID; } set { _iClient_ID = value; } }
        public int Status { get { return _iStatus; } set { _iStatus = value; } }
        public string Email { get { return _sEmail; } set { _sEmail = value; } }
        public string Mobile { get { return _sMobile; } set { _sMobile = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
