using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Core
{
    public class clsExecutionReports_Control
    {
        SqlConnection conn = new SqlConnection(Global.connFIXStr);
        SqlDataReader drList = null;

        private int _iRecord_ID;
        private int _iEX_Id;
        private DateTime _dEX_CurrentTimestamp;
        private string _sEX_ClOrdID;
        private int _iStatus;                                 
        private DataTable _dtList;

        public clsExecutionReports_Control()
        {
            this._iRecord_ID = 0;
            this._iEX_Id = 0;
            this._dEX_CurrentTimestamp = Convert.ToDateTime("1900/01/01");
            this._sEX_ClOrdID = "";
            this._iStatus = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn = new SqlConnection(Global.connFIXStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetExecutionReports_Control", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@FileName", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iEX_Id = Convert.ToInt32(drList["EX_Id"]);
                    this._dEX_CurrentTimestamp = Convert.ToDateTime(drList["EX_CurrentTimestamp"]);
                    this._sEX_ClOrdID = drList["EX_ClOrdID"] + "";
                    this._iStatus = Convert.ToInt32(drList["Status"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        } 

        public void GetList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("EX_Id", typeof(int));
            _dtList.Columns.Add("Code", typeof(string));
            _dtList.Columns.Add("PreContract_ID", typeof(int));
            _dtList.Columns.Add("Contract_ID", typeof(int));
            _dtList.Columns.Add("DocTypes", typeof(int));
            _dtList.Columns.Add("Tipos", typeof(string));
            _dtList.Columns.Add("PD_Group_ID", typeof(int));
            _dtList.Columns.Add("DMS_Files_ID", typeof(int));
            _dtList.Columns.Add("FilePath", typeof(string));
            _dtList.Columns.Add("FileName", typeof(string));
            _dtList.Columns.Add("OldFile", typeof(int));
            _dtList.Columns.Add("EX_CurrentTimestamp", typeof(DateTime));
            _dtList.Columns.Add("User_ID", typeof(int));
            _dtList.Columns.Add("Status", typeof(int));

            try
            {
                conn = new SqlConnection(Global.connFIXStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetExecutionReports_Control", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EX_Id", _iEX_Id));    
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["EX_Id"], drList["Code"], drList["PreContract_ID"], drList["Contract_ID"], drList["DocTypes_ID"],
                                     drList["Tipos"], drList["PD_Group_ID"], drList["DMS_Files_ID"],  drList["FileName"], drList["OldFile"],
                                     drList["EX_CurrentTimestamp"], drList["User_ID"], drList["Status"]);
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
                using (SqlCommand cmd = new SqlCommand("InsertExecutionReports_Control", conn))
                {
                    SqlParameter outParam1 = new SqlParameter("@ID", SqlDbType.Int);
                    outParam1.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam1);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EX_Id", SqlDbType.Int).Value = this._iEX_Id;
                    cmd.Parameters.Add("@EX_CurrentTimestamp", SqlDbType.DateTime).Value = this._dEX_CurrentTimestamp;
                    cmd.Parameters.Add("@EX_ClOrdID", SqlDbType.NVarChar, 32).Value = this._sEX_ClOrdID;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = this._iStatus;
                    cmd.ExecuteNonQuery();
                    this._iRecord_ID = Convert.ToInt32(outParam1.Value);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return this._iRecord_ID;
        }
        public void EditRecord()
        {
            try 
            { 
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("EditExecutionReports_Control", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = this._iRecord_ID;
                    cmd.Parameters.Add("@EX_Id", SqlDbType.Int).Value = this._iEX_Id;
                    cmd.Parameters.Add("@EX_CurrentTimestamp", SqlDbType.DateTime).Value = this._dEX_CurrentTimestamp;
                    cmd.Parameters.Add("@EX_ClOrdID", SqlDbType.NVarChar, 32).Value = this._sEX_ClOrdID;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = this._iStatus;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ExecutionReports_Control";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }     
        public int Record_ID { get { return _iRecord_ID; } set { _iRecord_ID = value; } }
        public int EX_Id { get { return _iEX_Id; } set { _iEX_Id = value; } }
        public DateTime EX_CurrentTimestamp { get { return _dEX_CurrentTimestamp; } set { _dEX_CurrentTimestamp = value; } }
        public string EX_ClOrdID { get { return _sEX_ClOrdID; } set { _sEX_ClOrdID = value; } }
        public int Status { get { return _iStatus; } set { _iStatus = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
