using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace Core
{
    public class clsDMS_Files
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int _iRecord_ID;
        private int _iSource_ID;                                           // 1 - Client's personal data document,   2 - Client's package document
        private int _iDocTypes_ID;
        private string _sFileName;
        private DateTime _dDateIns;
        private int _iUser_ID;

        private string _sDocTypes_Title;
        private DataTable _dtList;

        public clsDMS_Files()
        {
            this._iRecord_ID = 0;
            this._iSource_ID = 0;
            this._iDocTypes_ID = 0;
            this._sFileName = "";
            this._dDateIns = Convert.ToDateTime("1900/01/01");
            this._iUser_ID = 0;
            this._sDocTypes_Title = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetDMS_File", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", this._iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iSource_ID = Convert.ToInt32(drList["Source_ID"]);
                    this._iDocTypes_ID = Convert.ToInt32(drList["DocTypes_ID_ID"]);
                    this._sFileName = drList["FileName"] + "";
                    this._dDateIns = Convert.ToDateTime(drList["DateIns"]);
                    this._iUser_ID = Convert.ToInt32(drList["User_ID"]);
                    this._sDocTypes_Title = drList["DocTypes_Title"] + "";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("Source_ID", typeof(int));
            _dtList.Columns.Add("DocTypes_ID", typeof(int));            
            _dtList.Columns.Add("FileName", typeof(string));
            _dtList.Columns.Add("DateIns", typeof(DateTime));
            _dtList.Columns.Add("User_ID", typeof(int));
            _dtList.Columns.Add("DocTypes_Title", typeof(string));

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetDMSFiles", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Source_ID", _iSource_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["Source_ID"], drList["DocTypes_ID"], drList["FileName"], drList["DateIns"], drList["User_ID"], drList["DocTypes_Title"]);
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
                using (cmd = new SqlCommand("InsertDMS_File", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Source_ID", SqlDbType.Int).Value = this._iSource_ID;
                    cmd.Parameters.Add("@DocTypes_ID", SqlDbType.Int).Value = this._iDocTypes_ID;
                    cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 100).Value = this._sFileName;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = this.DateIns;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = this._iUser_ID;
                    cmd.ExecuteNonQuery();
                    _iRecord_ID = Convert.ToInt32(outParam.Value);
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
                using (SqlCommand cmd = new SqlCommand("EditDMS_File", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = this._iRecord_ID;
                    cmd.Parameters.Add("@Source_ID", SqlDbType.Int).Value = this._iSource_ID;
                    cmd.Parameters.Add("@DocTypes_ID", SqlDbType.Int).Value = this._iDocTypes_ID;
                    cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 100).Value = this._sFileName;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = this.DateIns;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = this._iUser_ID;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "DMS_Files";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return _iRecord_ID; } set { _iRecord_ID = value; } }
        public int Source_ID { get { return _iSource_ID; } set { _iSource_ID = value; } }
        public int DocTypes_ID { get { return _iDocTypes_ID; } set { _iDocTypes_ID = value; } }
        public string FileName { get { return _sFileName; } set { _sFileName = value; } }
        public DateTime DateIns { get { return _dDateIns; } set { _dDateIns = value; } }
        public int User_ID { get { return _iUser_ID; } set { _iUser_ID = value; } }
        public string DocTypes_Title { get { return _sDocTypes_Title; } set { _sDocTypes_Title = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
