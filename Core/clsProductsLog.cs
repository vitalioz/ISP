using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsProductsLogger
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int _iRecord_ID;
        private int _iShareCodes_ID;
        private string _sOldMIFID_Risk;
        private string _sNewMIFID_Risk;
        private DateTime _dEditDate;
        private int _iEditMethod;

        private DataTable _dtList;

        public clsProductsLogger()
        {
            this._iRecord_ID = 0;
            this._iShareCodes_ID = 0;
            this._sOldMIFID_Risk = "";
            this._sNewMIFID_Risk = "";
            this._dEditDate = Convert.ToDateTime("1900/01/01");
            this._iEditMethod = 0;

        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Products_Log"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iShareCodes_ID = Convert.ToInt32(drList["ShareCodes_ID"]);
                    this._sOldMIFID_Risk = drList["OldMIFID_Risk"] + "";
                    this._sNewMIFID_Risk = drList["NewMIFID_Risk"] + "";
                    this._dEditDate = Convert.ToDateTime(drList["EditDate"]);
                    this._iEditMethod = Convert.ToInt32(drList["EditMethod"]);
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
                _dtList = new DataTable("ProductsLog_List");
                dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("OldMIFID_Risk", Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Products_Log"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["ShareCodes_ID"] = Convert.ToInt32(drList["ShareCodes_ID"]);
                    this.dtRow["OldMIFID_Risk"] = drList["OldMIFID_Risk"] + "";
                    this.dtRow["NewMIFID_Risk"] = drList["NewMIFID_Risk"] + "";
                    this.dtRow["EditDate"] = Convert.ToDateTime(drList["EditDate"]);
                    this.dtRow["EditMethod"] = Convert.ToInt32(drList["EditMethod"]);
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
                using (SqlCommand cmd = new SqlCommand("InsertProducts_Log", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ShareCodes_ID", SqlDbType.Int).Value = _iShareCodes_ID;
                    cmd.Parameters.Add("@OldMIFID_Risk", SqlDbType.NVarChar, 20).Value = _sOldMIFID_Risk;
                    cmd.Parameters.Add("@NewMIFID_Risk", SqlDbType.NVarChar, 20).Value = _sNewMIFID_Risk;
                    cmd.Parameters.Add("@EditDate", SqlDbType.DateTime).Value = _dEditDate;
                    cmd.Parameters.Add("@EditMethod", SqlDbType.Int).Value = _iEditMethod;

                    cmd.ExecuteNonQuery();
                    _iRecord_ID = Convert.ToInt32(outParam.Value);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return _iRecord_ID;       }
        public void EditRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("EditProducts_Log", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@ShareCodes_ID", SqlDbType.Int).Value = _iShareCodes_ID;
                    cmd.Parameters.Add("@OldMIFID_Risk", SqlDbType.NVarChar, 20).Value = _sOldMIFID_Risk;
                    cmd.Parameters.Add("@NewMIFID_Risk", SqlDbType.NVarChar, 20).Value = _sNewMIFID_Risk;
                    cmd.Parameters.Add("@EditDate", SqlDbType.DateTime).Value = _dEditDate;
                    cmd.Parameters.Add("@EditMethod", SqlDbType.Int).Value = _iEditMethod;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Products_Log";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int ShareCodes_ID { get { return this._iShareCodes_ID; } set { this._iShareCodes_ID = value; } }
        public string OldMIFID_Risk { get { return this._sOldMIFID_Risk; } set { this._sOldMIFID_Risk = value; } }
        public string NewMIFID_Risk { get { return this._sNewMIFID_Risk; } set { this._sNewMIFID_Risk = value; } }
        public DateTime EditDate { get { return this._dEditDate; } set { this._dEditDate = value; } }
        public int EditMethod { get { return this._iEditMethod; } set { this._iEditMethod = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}