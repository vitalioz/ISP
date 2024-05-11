using System;                                    //OK
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsClientsRequests_Types
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList;
        DataColumn dtCol;
        DataRow dtRow;

        private int _iRecord_ID;
        private string _sTitle_0;
        private string _sTitle_1;
        private string _sTitle_2;
        private int _iDocType1_ID;
        private int _iDocType2_ID;

        private DataTable _dtList;

        public clsClientsRequests_Types()
        {
            this._iRecord_ID = 0;
            this._sTitle_0 = "";
            this._sTitle_1 = "";
            this._sTitle_2 = "";
            this._iDocType1_ID = 0;
            this._iDocType2_ID = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ClientsRequests_Types"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._sTitle_0 = drList["Title_0"] + "";
                    this._sTitle_1 = drList["Title_1"] + "";
                    this._sTitle_2 = drList["Title_2"] + "";
                    this._iDocType1_ID = Convert.ToInt32(drList["DocType1_ID"]);
                    this._iDocType2_ID = Convert.ToInt32(drList["DocType2_ID"]);
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
                _dtList = new DataTable("ClientsRequests_Types_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Title_0", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Title_1", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Title_2", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DocType1_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DocType2_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ClientsRequests_Types"));
                cmd.Parameters.Add(new SqlParameter("@Col", ""));
                cmd.Parameters.Add(new SqlParameter("@Value", ""));
                cmd.Parameters.Add(new SqlParameter("@Order", "Title"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["Title_0"] = drList["Title_0"];
                    dtRow["Title_1"] = drList["Title_1"];
                    dtRow["Title_2"] = drList["Title_2"];
                    dtRow["DocType1_ID"] = drList["DocType1_ID"];
                    dtRow["DocType2_ID"] = drList["DocType2_ID"];
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
                using (SqlCommand cmd = new SqlCommand("InsertClientsRequests_Types", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Title_0", SqlDbType.NVarChar, 100).Value = _sTitle_0;
                    cmd.Parameters.Add("@Title_1", SqlDbType.NVarChar, 100).Value = _sTitle_1;
                    cmd.Parameters.Add("@Title_2", SqlDbType.NVarChar, 100).Value = _sTitle_2;
                    cmd.Parameters.Add("@DocType1_ID", SqlDbType.Int).Value = _iDocType1_ID;
                    cmd.Parameters.Add("@DocType2_ID", SqlDbType.Int).Value = _iDocType2_ID;
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
                using (SqlCommand cmd = new SqlCommand("EditClientsRequests_Types", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Title_0", SqlDbType.NVarChar, 100).Value = _sTitle_0;
                    cmd.Parameters.Add("@Title_1", SqlDbType.NVarChar, 100).Value = _sTitle_1;
                    cmd.Parameters.Add("@Title_2", SqlDbType.NVarChar, 100).Value = _sTitle_2;
                    cmd.Parameters.Add("@DocType1_ID", SqlDbType.Int).Value = _iDocType1_ID;
                    cmd.Parameters.Add("@DocType2_ID", SqlDbType.Int).Value = _iDocType2_ID;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ClientsRequests_Types";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public string Title_0 { get { return this._sTitle_0; } set { this._sTitle_0 = value; } }
        public string Title_1 { get { return this._sTitle_1; } set { this._sTitle_1 = value; } }
        public string Title_2 { get { return this._sTitle_2; } set { this._sTitle_2 = value; } }
        public int DocType1_ID { get { return this._iDocType1_ID; } set { this._iDocType1_ID = value; } }
        public int DocType2_ID { get { return this._iDocType2_ID; } set { this._iDocType2_ID = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}