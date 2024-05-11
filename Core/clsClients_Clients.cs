using System;                                    //OK
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsClients_Clients
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int      _iRecord_ID;
        private int      _iClient_ID;
        private int      _iClient2_ID;
        private int      _iStatus;
        private DateTime _dDateIns;

        private DataTable _dtList;

        public clsClients_Clients()
        {
            this._iRecord_ID = 0;
            this._iClient_ID = 0;
            this._iClient2_ID = 0;
            this._iStatus = 0;
            this._dDateIns = Convert.ToDateTime("1900/01/01");
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Clients_Clients"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    this._iClient2_ID = Convert.ToInt32(drList["Client2_ID"]);
                    this._iStatus = Convert.ToInt16(drList["Status"]);
                    this._dDateIns = Convert.ToDateTime(drList["DateIns"]);
                }
                drList.Close();
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
            _dtList.Columns.Add("Client_ID", typeof(int));
            _dtList.Columns.Add("Client_Fullname", typeof(string));
            _dtList.Columns.Add("Client_AFM", typeof(string));
            _dtList.Columns.Add("Client_Email", typeof(string));
            _dtList.Columns.Add("Client2_ID", typeof(int));
            _dtList.Columns.Add("Client2_Fullname", typeof(string));
            _dtList.Columns.Add("Client2_AFM", typeof(string));
            _dtList.Columns.Add("Client2_DoB", typeof(DateTime));
            _dtList.Columns.Add("Client2_Email", typeof(string));
            _dtList.Columns.Add("Status", typeof(int));
            _dtList.Columns.Add("DateIns", typeof(string));

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetClients_Clients", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Client_ID", _iClient_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["Client_ID"], drList["Client_Fullname"], drList["Client_AFM"], drList["Client_Email"],
                                                   drList["Client2_ID"], drList["Client2_Fullname"], drList["Client2_AFM"],
                                                   ((drList["Client2_DoB"] + "") == "") ? Convert.ToDateTime("1900/01/01") : drList["Client2_DoB"],
                                                   drList["Client2_Email"], drList["Status"], drList["DateIns"]);
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
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("InsertClients_Clients", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Client2_ID", SqlDbType.Int).Value = _iClient2_ID;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = DateTime.Now;
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
        public void EditRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("EditClients_Clients", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Client2_ID", SqlDbType.Int).Value = _iClient2_ID;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string sTemp = ex.Message;
            }
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Clients_Clients";
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
        public int Client_ID { get { return this._iClient_ID; } set { this._iClient_ID = value; } }
        public int Client2_ID { get { return this._iClient2_ID; } set { this._iClient2_ID = value; } }
        public int Status { get { return this._iStatus; } set { this._iStatus = value; } }
        public DateTime DateIns { get { return this._dDateIns; } set { this._dDateIns = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}