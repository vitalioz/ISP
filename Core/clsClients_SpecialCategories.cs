using System;                                    //OK
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsClients_SpecialCategories
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int _iRecord_ID;
        private int _iClient_ID;
        private int _iSpecCategory_ID;
        private string _sFileName;

        private DataTable _dtList;

        public clsClients_SpecialCategories()
        {
            this._iRecord_ID = 0;
            this._iClient_ID = 0;
            this._iSpecCategory_ID = 0;
            this._sFileName = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Clients_SpecialCategories"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    this._iSpecCategory_ID = Convert.ToInt32(drList["SpecCategory_ID"]);
                    this._sFileName = drList["FileName"]+"";
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
            _dtList.Columns.Add("SpecCategory_ID", typeof(int));
            _dtList.Columns.Add("ClientsDocFiles_ID", typeof(int));
            _dtList.Columns.Add("FileName", typeof(string));

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetClients_SpecialCategories", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Client_ID", _iClient_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["Client_ID"], drList["SpecCategory_ID"], drList["ClientsDocFiles_ID"], drList["FileName"]);
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
                using (SqlCommand cmd = new SqlCommand("InsertClients_SpecialCategories", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@SpecCategory_ID", SqlDbType.Int).Value = _iSpecCategory_ID;
                    cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 50).Value = _sFileName;
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
                using (SqlCommand cmd = new SqlCommand("EditClients_SpecialCategories", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@SpecCategory_ID", SqlDbType.Int).Value = _iSpecCategory_ID;
                    cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 50).Value = _sFileName;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Clients_SpecialCategories";
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
        public void DeleteRecord_ClientID()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Clients_SpecialCategories";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "Client_ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iClient_ID;
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
        public int SpecCategory_ID { get { return this._iSpecCategory_ID; } set { this._iSpecCategory_ID = value; } }
        public string FileName { get { return this._sFileName; } set { this._sFileName = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}