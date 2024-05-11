using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsOrdersLL_Recieved
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int _iRecord_ID;
        private int _iCommandLL_ID;
        private DateTime _dDateIns;
        private int _iMethod_ID;
        private string _sFilePath;
        private string _sFileName;

        private DataTable _dtList;
        public clsOrdersLL_Recieved()
        {
            this._iRecord_ID = 0;
            this._iCommandLL_ID = 0;
            this._dDateIns = Convert.ToDateTime("1900/01/01");
            this._iMethod_ID = 0;
            this._sFilePath = "";
            this._sFileName = "";
        }
        public void GetRecord()
        {
            drList = null;
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "CommandsLL_Recieved"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iCommandLL_ID = Convert.ToInt32(drList["CommandLL_ID"]);
                    this._dDateIns = Convert.ToDateTime(drList["DateIns"]);
                    this._iMethod_ID = Convert.ToInt32(drList["Method_ID"]);
                    this._sFilePath = drList["FilePath"] + "";
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
            _dtList.Columns.Add("CommandLL_ID", typeof(int));
            _dtList.Columns.Add("DateIns", typeof(DateTime));
            _dtList.Columns.Add("Method_ID", typeof(int));
            _dtList.Columns.Add("FilePath", typeof(string));
            _dtList.Columns.Add("FileName", typeof(string));
            drList = null;
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "CommandsLL_Recieved"));
                cmd.Parameters.Add(new SqlParameter("@Col", "CommandLL_ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iCommandLL_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", "ID DESC"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["CommandLL_ID"], drList["DateIns"], drList["Method_ID"], drList["FilePath"], drList["FileName"]);
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
                using (SqlCommand cmd = new SqlCommand("sp_InsertCommandsLLRecieved", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CommandLL_ID", SqlDbType.Int).Value = _iCommandLL_ID;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = DateIns;
                    cmd.Parameters.Add("@Method_ID", SqlDbType.Int).Value = _iMethod_ID;
                    cmd.Parameters.Add("@FilePath", SqlDbType.NVarChar, 500).Value = _sFilePath;
                    cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 100).Value = _sFileName;

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
                using (SqlCommand cmd = new SqlCommand("sp_EditCommandsLLRecieved", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@CommandLL_ID", SqlDbType.Int).Value = _iCommandLL_ID;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = DateIns;
                    cmd.Parameters.Add("@Method_ID", SqlDbType.Int).Value = _iMethod_ID;
                    cmd.Parameters.Add("@FilePath", SqlDbType.NVarChar, 500).Value = _sFilePath;
                    cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 100).Value = _sFileName;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "CommandsLL_Recieved";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return _iRecord_ID; } set { _iRecord_ID = value; } }
        public int CommandLL_ID { get { return _iCommandLL_ID; } set { _iCommandLL_ID = value; } }
        public DateTime DateIns { get { return _dDateIns; } set { _dDateIns = value; } }
        public int Method_ID { get { return _iMethod_ID; } set { _iMethod_ID = value; } }
        public string FilePath { get { return _sFilePath; } set { _sFilePath = value; } }
        public string FileName { get { return _sFileName; } set { _sFileName = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
