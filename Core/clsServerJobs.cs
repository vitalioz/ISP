using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Core
{
    public class clsServerJobs
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int _iRecord_ID;
        private int _iJobType_ID;
        private int _iSource_ID;
        private string _sParameters;
        private DateTime _dDateStart;
        private DateTime _dDateFinish;
        private string _sPubKey;
        private string _sPrvKey;
        private int _iAttempt;
        private int _iStatus;

        private DataTable _dtList;

        public clsServerJobs()
        {
            this._iRecord_ID = 0;
            this._iJobType_ID = 0;
            this._iSource_ID = 0;
            this._sParameters = "";
            this._dDateStart = Convert.ToDateTime("1900/01/01");
            this._dDateFinish = Convert.ToDateTime("1900/01/01");
            this._sPubKey = "";
            this._sPrvKey = "";
            this._iAttempt = 0;
            this._iStatus = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ServerJobs"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iJobType_ID = Convert.ToInt32(drList["JobType_ID"]);
                    this._iSource_ID = Convert.ToInt32(drList["Source_ID"]);
                    this._sParameters = drList["Parameters"] + "";
                    this._dDateStart = Convert.ToDateTime(drList["DateStart"]);
                    this._dDateFinish = Convert.ToDateTime(drList["DateFinish"]);
                    this._sPubKey = drList["PubKey"] + "";
                    this._sPrvKey = drList["PrvKey"] + "";
                    this._iAttempt = Convert.ToInt32(drList["Attempt"]);
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
            _dtList.Columns.Add("JobType_ID", typeof(int));
            _dtList.Columns.Add("Source_ID", typeof(int));
            _dtList.Columns.Add("Parameters", typeof(string));
            _dtList.Columns.Add("DateStart", typeof(DateTime));
            _dtList.Columns.Add("DateFinish", typeof(DateTime));
            _dtList.Columns.Add("Attempt", typeof(int));
            _dtList.Columns.Add("Status", typeof(int));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetServerJobs_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dDateStart));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dDateFinish));
                cmd.Parameters.Add(new SqlParameter("@JobType_ID", _iJobType_ID));
                cmd.Parameters.Add(new SqlParameter("@Source_ID", _iSource_ID));
                cmd.Parameters.Add(new SqlParameter("@Status", _iStatus));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["JobType_ID"], drList["Source_ID"], drList["Parameters"], drList["DateStart"], drList["DateFinish"], drList["Attempt"], drList["Status"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int InsertRecord()
        {
            _iRecord_ID = 0;
            try
            {
                conn.Open();
                using (cmd = new SqlCommand("InsertServerJobs", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@JobType_ID", SqlDbType.Int).Value = this._iJobType_ID;
                    cmd.Parameters.Add("@Source_ID", SqlDbType.Int).Value = this._iSource_ID;
                    cmd.Parameters.Add("@Parameters", SqlDbType.NVarChar, 1000).Value = this._sParameters;
                    cmd.Parameters.Add("@DateStart", SqlDbType.DateTime).Value = this._dDateStart;
                    cmd.Parameters.Add("@DateFinish", SqlDbType.DateTime).Value = this._dDateFinish;
                    cmd.Parameters.Add("@PubKey", SqlDbType.NVarChar).Value = this._sPubKey;
                    cmd.Parameters.Add("@PrvKey", SqlDbType.NVarChar).Value = this._sPrvKey;
                    cmd.Parameters.Add("@Attempt", SqlDbType.Int).Value = this._iAttempt;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = this._iStatus;

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
                using (cmd = new SqlCommand("EditServerJobs", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = this._iRecord_ID;
                    cmd.Parameters.Add("@JobType_ID", SqlDbType.Int).Value = this._iJobType_ID;
                    cmd.Parameters.Add("@Source_ID", SqlDbType.Int).Value = this._iSource_ID;
                    cmd.Parameters.Add("@Parameters", SqlDbType.NVarChar, 1000).Value = this._sParameters;
                    cmd.Parameters.Add("@DateStart", SqlDbType.DateTime).Value = this._dDateStart;
                    cmd.Parameters.Add("@DateFinish", SqlDbType.DateTime).Value = this._dDateFinish;
                    cmd.Parameters.Add("@PubKey", SqlDbType.NVarChar).Value = this._sPubKey;
                    cmd.Parameters.Add("@PrvKey", SqlDbType.NVarChar).Value = this._sPrvKey;
                    cmd.Parameters.Add("@Attempt", SqlDbType.Int).Value = this._iAttempt;
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
                using (cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ServerJobs";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return _iRecord_ID; } set { _iRecord_ID = value; } }
        public int JobType_ID { get { return _iJobType_ID; } set { _iJobType_ID = value; } }
        public int Source_ID { get { return _iSource_ID; } set { _iSource_ID = value; } }
        public string Parameters { get { return _sParameters; } set { _sParameters = value; } }
        public DateTime DateStart { get { return _dDateStart; } set { _dDateStart = value; } }
        public DateTime DateFinish { get { return _dDateFinish; } set { _dDateFinish = value; } }
        public string PubKey { get { return _sPubKey; } set { _sPubKey = value; } }
        public string PrvKey { get { return _sPrvKey; } set { _sPrvKey = value; } }
        public int Attempt { get { return _iAttempt; } set { _iAttempt = value; } }
        public int Status { get { return _iStatus; } set { _iStatus = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
