using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsImportData
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int _iRecord_ID;
        private string _sTitle;
        private int _iSheetNumber;       
        private int _iSourceColumnsCount;
        private int _iTargetColumnsCount;
        private int _iHeaderLines;
        private int _iTableFinish;
        private int _iDB_Table1_MD_T_ID;
        private int _iDB_Table2_MD_T_ID;
        private int _iDB_Table3_MD_T_ID;
        private char _cCSV_Delimiter;

        private DataTable _dtList;
        public clsImportData()
        {
            this._iRecord_ID = 0;
            this._sTitle = "";
            this._iSheetNumber = 0;
            this._iSourceColumnsCount = 0;
            this._iTargetColumnsCount = 0;
            this._iHeaderLines = 0;
            this._iTableFinish = 0;
            this._iDB_Table1_MD_T_ID = 0;
            this._iDB_Table2_MD_T_ID = 0;
            this._iDB_Table3_MD_T_ID = 0;
            this._cCSV_Delimiter = char.Parse(",");
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ImportData_SchemasList"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._sTitle = drList["Title"] + "";
                    this._iSheetNumber = Convert.ToInt32(drList["SheetNumber"]);
                    this._iSourceColumnsCount = Convert.ToInt32(drList["SourceColumnsCount"]);
                    this._iTargetColumnsCount = Convert.ToInt32(drList["TargetColumnsCount"]);
                    this._iHeaderLines = Convert.ToInt32(drList["HeaderLines"]);
                    this._iTableFinish = Convert.ToInt32(drList["TableFinish"]);
                    this._iDB_Table1_MD_T_ID = Convert.ToInt32(drList["DB_Table1_MD_T_ID"]);
                    this._iDB_Table2_MD_T_ID = Convert.ToInt32(drList["DB_Table2_MD_T_ID"]);
                    this._iDB_Table3_MD_T_ID = Convert.ToInt32(drList["DB_Table3_MD_T_ID"]);
                    this._cCSV_Delimiter = char.Parse(drList["CSV_Delimiter"]+""); 
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("AktionTitle", typeof(string));
            _dtList.Columns.Add("Code", typeof(string));
            _dtList.Columns.Add("Portfolio", typeof(string));
            _dtList.Columns.Add("ContractTitle", typeof(string));
            _dtList.Columns.Add("Title", typeof(string));
            _dtList.Columns.Add("FileName", typeof(string));
            _dtList.Columns.Add("TargetColumnsCount", typeof(int));
            _dtList.Columns.Add("HeaderLines", typeof(int));
            _dtList.Columns.Add("SheetNumber", typeof(int));
            _dtList.Columns.Add("SourceColumnsCount", typeof(int));
            _dtList.Columns.Add("Aktion", typeof(int));
            _dtList.Columns.Add("CurrentValues", typeof(string));
            _dtList.Columns.Add("DB_Table1_MD_T_ID", typeof(int));
            _dtList.Columns.Add("DateIns", typeof(DateTime));
            _dtList.Columns.Add("DB_Table2_MD_T_ID", typeof(int));
            _dtList.Columns.Add("UserName", typeof(string));

            SqlDataReader drList = null;
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetImportData_SchemasList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SheetNumber", _iSheetNumber));
                cmd.Parameters.Add(new SqlParameter("@SourceColumnsCount", _iSourceColumnsCount));
                cmd.Parameters.Add(new SqlParameter("@HeaderLines", _iHeaderLines));
                cmd.Parameters.Add(new SqlParameter("@TargetColumnsCount", _iTargetColumnsCount));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["AktionTitle"], drList["Code"], drList["Portfolio"], drList["ContractTitle"], drList["Title"], drList["FileName"],
                                     drList["TargetColumnsCount"], drList["HeaderLines"], drList["SheetNumber"], drList["SourceColumnsCount"],
                                     drList["Aktion"], drList["CurrentValues"], drList["DB_Table1_MD_T_ID"], drList["DateIns"], drList["DB_Table2_MD_T_ID"], drList["UserName"]);
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
                using (SqlCommand cmd = new SqlCommand("InsertImportData_SchemasList", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 2000).Value = _sTitle;
                    cmd.Parameters.Add("@SheetNumber", SqlDbType.Int).Value = _iSheetNumber;
                    cmd.Parameters.Add("@SourceColumnsCount", SqlDbType.Int).Value = _iSourceColumnsCount;
                    cmd.Parameters.Add("@TargetColumnsCount", SqlDbType.Int).Value = _iTargetColumnsCount;
                    cmd.Parameters.Add("@HeaderLines", SqlDbType.Int).Value = _iHeaderLines;
                    cmd.Parameters.Add("@Aktion", SqlDbType.Int).Value = _iTableFinish;
                    cmd.Parameters.Add("@DB_Table1_MD_T_ID", SqlDbType.Int).Value = _iDB_Table1_MD_T_ID;
                    cmd.Parameters.Add("@DB_Table2_MD_T_ID", SqlDbType.Int).Value = _iDB_Table2_MD_T_ID;
                    cmd.Parameters.Add("@DB_Table3_MD_T_ID", SqlDbType.Int).Value = _iDB_Table3_MD_T_ID;
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
                using (SqlCommand cmd = new SqlCommand("EditImportData_SchemasList", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 2000).Value = _sTitle;
                    cmd.Parameters.Add("@SheetNumber", SqlDbType.Int).Value = _iSheetNumber;
                    cmd.Parameters.Add("@SourceColumnsCount", SqlDbType.Int).Value = _iSourceColumnsCount;
                    cmd.Parameters.Add("@TargetColumnsCount", SqlDbType.Int).Value = _iTargetColumnsCount;
                    cmd.Parameters.Add("@HeaderLines", SqlDbType.Int).Value = _iHeaderLines;
                    cmd.Parameters.Add("@Aktion", SqlDbType.Int).Value = _iTableFinish;
                    cmd.Parameters.Add("@DB_Table1_MD_T_ID", SqlDbType.Int).Value = _iDB_Table1_MD_T_ID;
                    cmd.Parameters.Add("@DB_Table2_MD_T_ID", SqlDbType.Int).Value = _iDB_Table2_MD_T_ID;
                    cmd.Parameters.Add("@DB_Table3_MD_T_ID", SqlDbType.Int).Value = _iDB_Table3_MD_T_ID;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ImportData_SchemasList";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = this._iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return _iRecord_ID; } set { _iRecord_ID = value; } }
        public string Title { get { return _sTitle; } set { _sTitle = value; } }
        public int SheetNumber { get { return _iSheetNumber; } set { _iSheetNumber = value; } }
        public int SourceColumnsCount { get { return _iSourceColumnsCount; } set { _iSourceColumnsCount = value; } }
        public int TargetColumnsCount { get { return _iTargetColumnsCount; } set { _iTargetColumnsCount = value; } }
        public int HeaderLines { get { return _iHeaderLines; } set { _iHeaderLines = value; } }
        public int TableFinish { get { return _iTableFinish; } set { _iTableFinish = value; } }
        public int DB_Table1_MD_T_ID { get { return _iDB_Table1_MD_T_ID; } set { _iDB_Table1_MD_T_ID = value; } }
        public int DB_Table2_MD_T_ID { get { return _iDB_Table2_MD_T_ID; } set { _iDB_Table2_MD_T_ID = value; } }
        public int DB_Table3_MD_T_ID { get { return _iDB_Table3_MD_T_ID; } set { _iDB_Table3_MD_T_ID = value; } }
        public char CSV_Delimiter { get { return _cCSV_Delimiter; } set { _cCSV_Delimiter = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
