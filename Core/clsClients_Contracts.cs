using System;                                    //OK
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsClients_Contracts
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int    _iRecord_ID;
        private int    _iClient_ID;
        private int    _iContract_ID;
        private string _sDOY;
        private string _sAFM;
        private int    _iIsMaster;
        private int    _iIsOrder;
           
        private DataTable _dtList;

        public clsClients_Contracts()
        {
            this._iRecord_ID = 0;
            this._iContract_ID = 0;
            this._iClient_ID = 0;
            this._sDOY = "";
            this._sAFM = "";
            this._iIsMaster = 0;
            this._iIsOrder = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Clients_Contracts"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    this._sDOY = drList["DOY"] + "";
                    this._sAFM = drList["AFM"] + "";
                    this._iIsMaster = Convert.ToInt32(drList["IsMaster"]);
                    this._iIsOrder = Convert.ToInt32(drList["IsOrder"]);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
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
                                                   drList["Client2_ID"], drList["Client2_Fullname"], drList["Client2_AFM"], drList["Client2_Email"], drList["Status"], drList["DateIns"]);
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
                using (SqlCommand cmd = new SqlCommand("InsertClients_Contracts", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@DOY", SqlDbType.NVarChar, 50).Value = _sDOY;
                    cmd.Parameters.Add("@AFM", SqlDbType.NVarChar, 20).Value = _sAFM;
                    cmd.Parameters.Add("@IsMaster", SqlDbType.Int).Value = _iIsMaster;
                    cmd.Parameters.Add("@IsOrder", SqlDbType.Int).Value = _iIsOrder;
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
                using (SqlCommand cmd = new SqlCommand("EditClients_Contracts", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@DOY", SqlDbType.NVarChar, 50).Value = _sDOY;
                    cmd.Parameters.Add("@AFM", SqlDbType.NVarChar, 20).Value = _sAFM;
                    cmd.Parameters.Add("@IsMaster", SqlDbType.Int).Value = _iIsMaster;
                    cmd.Parameters.Add("@IsOrder", SqlDbType.Int).Value = _iIsOrder;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Clients_Contracts";
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
        public int Contract_ID { get { return this._iContract_ID; } set { this._iContract_ID = value; } }
        public string DOY { get { return this._sDOY; } set { this._sDOY = value; } }
        public string AFM { get { return this._sAFM; } set { this._sAFM = value; } }
        public int IsMaster { get { return this._iIsMaster; } set { this._iIsMaster = value; } }
        public int IsOrder { get { return this._iIsOrder; } set { this._iIsOrder = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}