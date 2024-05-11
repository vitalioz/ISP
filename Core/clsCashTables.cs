using System;                                   
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsCashTables
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList;
        DataColumn dtCol;
        DataRow dtRow;

        private int       _iRecord_ID;
        private int       _iCashTables_ID;
        private string    _sListGroup;
        private string    _sListGroupEng;
        private string    _sListTitle;
        private string    _sListTitleEng;
        private string    _sParams;
        private string    _sTableName;
        private DateTime  _dLastEdit_Time;
        private int       _iLastEdit_User_ID;
        private DataTable _dtList;

        public clsCashTables()
        {
            this._iRecord_ID = 0;
            this._iCashTables_ID = 0;
            this._sListGroup = "";
            this._sListGroupEng = "";
            this._sListTitle = "";
            this._sListTitleEng = "";
            this._sParams = "";
            this._sTableName = "";
            this._dLastEdit_Time = Convert.ToDateTime("1900/01/01");
            this._iLastEdit_User_ID = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ListsTables"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iCashTables_ID = Convert.ToInt32(drList["CashTables_ID"]);
                    this._sListGroup = drList["ListGroup"] + "";
                    this._sListGroupEng = drList["ListGroupEng"] + "";
                    this._sListTitle = drList["ListTitle"] + "";
                    this._sListTitleEng = drList["ListTitleEng"] + "";
                    this._sParams = drList["Params"] + "";
                    this._sTableName = drList["TableName"] + "";
                    this._dLastEdit_Time = Convert.ToDateTime(drList["LastEdit_Time"]);
                    this._iLastEdit_User_ID = Convert.ToInt32(drList["LastEdit_User_ID"]);
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
                _dtList = new DataTable("ListsTables_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("CashTables_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ListGroup", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ListGroupEng", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ListTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ListTitleEng", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Params", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("TableName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("LastEdit_Time", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("LastEdit_User_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Edit_Flag", System.Type.GetType("System.Int16"));

                conn.Open();
                cmd = new SqlCommand("GetListsTables", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["CashTables_ID"] = drList["CashTables_ID"];
                    dtRow["ListGroup"] = drList["ListGroup"];
                    dtRow["ListGroupEng"] = drList["ListGroupEng"];
                    dtRow["ListTitle"] = drList["ListTitle"];
                    dtRow["ListTitleEng"] = drList["ListTitleEng"];
                    dtRow["Params"] = drList["Params"];
                    dtRow["TableName"] = drList["TableName"];
                    dtRow["LastEdit_Time"] = drList["LastEdit_Time"];
                    dtRow["LastEdit_User_ID"] = drList["LastEdit_User_ID"];
                    dtRow["Edit_Flag"] = 0;
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
                using (SqlCommand cmd = new SqlCommand("InsertListsTables", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CashTables_ID", SqlDbType.Int).Value = _iCashTables_ID;
                    cmd.Parameters.Add("@ListGroup", SqlDbType.NVarChar, 50).Value = _sListGroup;
                    cmd.Parameters.Add("@ListGroupEng", SqlDbType.NVarChar, 50).Value = _sListGroupEng;
                    cmd.Parameters.Add("@ListTitle", SqlDbType.NVarChar, 100).Value = _sListTitle;
                    cmd.Parameters.Add("@ListTitleEng", SqlDbType.NVarChar, 100).Value = _sListTitleEng;
                    cmd.Parameters.Add("@Params", SqlDbType.NVarChar, 200).Value = _sParams;
                    cmd.Parameters.Add("@TableName", SqlDbType.NVarChar, 50).Value = _sTableName;
                    cmd.Parameters.Add("@LastEdit_Time", SqlDbType.DateTime).Value = _dLastEdit_Time;
                    cmd.Parameters.Add("@LastEdit_User_ID", SqlDbType.Int).Value = _iLastEdit_User_ID;
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
                using (SqlCommand cmd = new SqlCommand("EditListsTables", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@CashTables_ID", SqlDbType.Int).Value = _iCashTables_ID;
                    cmd.Parameters.Add("@ListGroup", SqlDbType.NVarChar, 50).Value = _sListGroup;
                    cmd.Parameters.Add("@ListGroupEng", SqlDbType.NVarChar, 50).Value = _sListGroupEng;
                    cmd.Parameters.Add("@ListTitle", SqlDbType.NVarChar, 100).Value = _sListTitle;
                    cmd.Parameters.Add("@ListTitleEng", SqlDbType.NVarChar, 100).Value = _sListTitleEng;
                    cmd.Parameters.Add("@Params", SqlDbType.NVarChar, 200).Value = _sParams;
                    cmd.Parameters.Add("@TableName", SqlDbType.NVarChar, 50).Value = _sTableName;
                    cmd.Parameters.Add("@LastEdit_Time", SqlDbType.DateTime).Value = _dLastEdit_Time;
                    cmd.Parameters.Add("@LastEdit_User_ID", SqlDbType.Int).Value = _iLastEdit_User_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void Edit_LastEdit_Time()
        {
            try
            {
                conn = new SqlConnection(Global.connStr);
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_EditListsTables_Edit", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CashTables_ID", SqlDbType.Int).Value = _iCashTables_ID;
                    cmd.Parameters.Add("@LastEdit_Time", SqlDbType.DateTime).Value = _dLastEdit_Time;
                    cmd.Parameters.Add("@LastEdit_User_ID", SqlDbType.Int).Value = _iLastEdit_User_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) {
                string sTemp = ex.Message;
                //MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ListsTables";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int CashTables_ID { get { return this._iCashTables_ID; } set { this._iCashTables_ID = value; } }
        public string ListGroup { get { return this._sListGroup; } set { this._sListGroup = value; } }
        public string ListGroupEng { get { return this._sListGroupEng; } set { this._sListGroupEng = value; } }
        public string ListTitle { get { return this._sListTitle; } set { this._sListTitle = value; } }
        public string ListTitleEng { get { return this._sListTitleEng; } set { this._sListTitleEng = value; } }
        public string Params { get { return this._sParams; } set { this._sParams = value; } }
        public string TableName { get { return this._sTableName; } set { this._sTableName = value; } }
        public DateTime LastEdit_Time { get { return this._dLastEdit_Time; } set { this._dLastEdit_Time = value; } }
        public int LastEdit_User_ID { get { return this._iLastEdit_User_ID; } set { this._iLastEdit_User_ID = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}