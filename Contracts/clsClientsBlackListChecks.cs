using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsClientsBlackListChecks
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int _iRecord_ID;
        private int _iClient_ID;
        private int _iUser_ID;
        private int _iCheckStatus;
        private int _iStatus;
        private string _sNotes;
        private string _sFileName;

        private DataTable _dtList;
        public clsClientsBlackListChecks()
        {
            this._iRecord_ID = 0;
            this._iClient_ID = 0;
            this._iUser_ID = 0;
            this._iCheckStatus = 0;
            this._iStatus = 0;
            this._sNotes = "";
            this._sFileName = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ClientsBlackList_Check"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", _iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    this._iUser_ID = Convert.ToInt32(drList["User_ID"]);
                    this._iCheckStatus = Convert.ToInt32(drList["CheckStatus"]);
                    this._iStatus = Convert.ToInt32(drList["Status"]);
                    this._sNotes = drList["Notes"] + "";
                    this._sFileName = drList["FileName"] + "";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("Surname", typeof(string));
            _dtList.Columns.Add("Firstname", typeof(string));
            _dtList.Columns.Add("CheckStatus", typeof(int));
            _dtList.Columns.Add("Status", typeof(int));
            _dtList.Columns.Add("Notes", typeof(string));
            _dtList.Columns.Add("FileName", typeof(string));
            _dtList.Columns.Add("User_ID", typeof(int));

            try
            {
                conn.Open();
                cmd = new SqlCommand("sp_GetClientsBlackList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Client_ID", _iClient_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["Surname"], drList["Firstname"], drList["CheckStatus"], drList["Status"], drList["Notes"], drList["FileName"], drList["User_ID"]);
                }
                _dtList.Load(drList);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int InsertRecord()
        {
            try
            {
                conn.Open();
                using (cmd = new SqlCommand("sp_InsertClientBlackList_Check", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = _iUser_ID;
                    cmd.Parameters.Add("@CheckStatus", SqlDbType.Int).Value = _iCheckStatus;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 100).Value = _sNotes.Trim();
                    cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 100).Value = _sFileName.Trim();
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
                using (cmd = new SqlCommand("sp_EditClientBlackList_Check", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = _iUser_ID;
                    cmd.Parameters.Add("@CheckStatus", SqlDbType.Int).Value = _iCheckStatus;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 100).Value = _sNotes.Trim();
                    cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 100).Value = _sFileName.Trim();
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ClientsBlackList_Check";
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
        public int User_ID { get { return this._iUser_ID; } set { this._iUser_ID = value; } }
        public int CheckStatus { get { return this._iCheckStatus; } set { this._iCheckStatus = value; } }
        public int Status { get { return this._iStatus; } set { this._iStatus = value; } }
        public string Notes { get { return _sNotes; } set { _sNotes = value; } }
        public string FileName { get { return _sFileName; } set { _sFileName = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
