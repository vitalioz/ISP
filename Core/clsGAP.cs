using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsGAP
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int _iRecord_ID;
        private int _iL1;      
        private int _iL2;
        private int _iL3;
        private int _iL4;
        private int _iL5;
        private int _iL6;
        private int _iL7;
        private int _iL8;
        private int _iL9;
        private string _sTitle;
        private string _sCode;

        private DataTable _dtList;
        public clsGAP()
        {
            this._iRecord_ID = 0;
            this._iL1 = 0;
            this._iL2 = 0;
            this._iL3 = 0;
            this._iL4 = 0;
            this._iL5 = 0;
            this._iL6 = 0;
            this._iL7 = 0;
            this._iL8 = 0;
            this._iL9 = 0;
            this._sTitle = "";
            this._sCode = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "GAP"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iL1 = Convert.ToInt32(drList["L1"]);
                    this._iL2 = Convert.ToInt32(drList["L2"]);
                    this._iL3 = Convert.ToInt32(drList["L3"]);
                    this._iL4 = Convert.ToInt32(drList["L4"]);
                    this._iL5 = Convert.ToInt32(drList["L5"]);
                    this._iL6 = Convert.ToInt32(drList["L6"]);
                    this._iL7 = Convert.ToInt32(drList["L7"]);
                    this._iL8 = Convert.ToInt32(drList["L8"]);
                    this._iL9 = Convert.ToInt32(drList["L9"]);
                    this._sTitle = drList["Title"] + "";
                    this._sCode = drList["Code"] + "";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetRecord_Code()
        {
            this._iRecord_ID = 0;
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "GAP"));
                cmd.Parameters.Add(new SqlParameter("@Col", "Code"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._sCode));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iL1 = Convert.ToInt32(drList["L1"]);
                    this._iL2 = Convert.ToInt32(drList["L2"]);
                    this._iL3 = Convert.ToInt32(drList["L3"]);
                    this._iL4 = Convert.ToInt32(drList["L4"]);
                    this._iL5 = Convert.ToInt32(drList["L5"]);
                    this._iL6 = Convert.ToInt32(drList["L6"]);
                    this._iL7 = Convert.ToInt32(drList["L7"]);
                    this._iL8 = Convert.ToInt32(drList["L8"]);
                    this._iL9 = Convert.ToInt32(drList["L9"]);
                    this._sTitle = drList["Title"] + "";
                    this._sCode = drList["Code"] + "";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("L1", typeof(int));
            _dtList.Columns.Add("L2", typeof(int));
            _dtList.Columns.Add("L3", typeof(int));
            _dtList.Columns.Add("L4", typeof(int));
            _dtList.Columns.Add("L5", typeof(int));
            _dtList.Columns.Add("L6", typeof(int));
            _dtList.Columns.Add("L7", typeof(int));
            _dtList.Columns.Add("L8", typeof(int));
            _dtList.Columns.Add("L9", typeof(int));
            _dtList.Columns.Add("Title", typeof(string));
            _dtList.Columns.Add("Code", typeof(string));

            SqlDataReader drList = null;
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetGAP", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@L1", _iL1));
                cmd.Parameters.Add(new SqlParameter("@L2", _iL2));
                cmd.Parameters.Add(new SqlParameter("@L3", _iL3));
                cmd.Parameters.Add(new SqlParameter("@L4", _iL4));
                cmd.Parameters.Add(new SqlParameter("@L5", _iL5));
                cmd.Parameters.Add(new SqlParameter("@L6", _iL6));
                cmd.Parameters.Add(new SqlParameter("@L7", _iL7));
                cmd.Parameters.Add(new SqlParameter("@L8", _iL8));
                cmd.Parameters.Add(new SqlParameter("@L9", _iL9));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["L1"], drList["L2"], drList["L3"], drList["L4"], drList["L5"], drList["L6"], drList["L7"],
                                     drList["L8"], drList["L9"], drList["Title"], drList["Code"]);
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
                using (SqlCommand cmd = new SqlCommand("InsertGAP", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@L1", SqlDbType.Int).Value = _iL1;
                    cmd.Parameters.Add("@L2", SqlDbType.Int).Value = _iL2;
                    cmd.Parameters.Add("@L3", SqlDbType.Int).Value = _iL3;
                    cmd.Parameters.Add("@L4", SqlDbType.Int).Value = _iL4;
                    cmd.Parameters.Add("@L5", SqlDbType.Int).Value = _iL5;
                    cmd.Parameters.Add("@L6", SqlDbType.Int).Value = _iL6;
                    cmd.Parameters.Add("@L7", SqlDbType.Int).Value = _iL7;
                    cmd.Parameters.Add("@L8", SqlDbType.Int).Value = _iL8;
                    cmd.Parameters.Add("@L9", SqlDbType.Int).Value = _iL9;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 100).Value = _sTitle; 
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 100).Value = _sCode;
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
                using (SqlCommand cmd = new SqlCommand("EditGAP", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@L1", SqlDbType.Int).Value = _iL1;
                    cmd.Parameters.Add("@L2", SqlDbType.Int).Value = _iL2;
                    cmd.Parameters.Add("@L3", SqlDbType.Int).Value = _iL3;
                    cmd.Parameters.Add("@L4", SqlDbType.Int).Value = _iL4;
                    cmd.Parameters.Add("@L5", SqlDbType.Int).Value = _iL5;
                    cmd.Parameters.Add("@L6", SqlDbType.Int).Value = _iL6;
                    cmd.Parameters.Add("@L7", SqlDbType.Int).Value = _iL7;
                    cmd.Parameters.Add("@L8", SqlDbType.Int).Value = _iL8;
                    cmd.Parameters.Add("@L9", SqlDbType.Int).Value = _iL9;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 100).Value = _sTitle;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 100).Value = _sCode;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "GAP";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = this._iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return _iRecord_ID; } set { _iRecord_ID = value; } }
        public int L1 { get { return _iL1; } set { _iL1 = value; } }
        public int L2 { get { return _iL2; } set { _iL2 = value; } }
        public int L3 { get { return _iL3; } set { _iL3 = value; } }
        public int L4 { get { return _iL4; } set { _iL4 = value; } }
        public int L5 { get { return _iL5; } set { _iL5 = value; } }
        public int L6 { get { return _iL6; } set { _iL6 = value; } }
        public int L7 { get { return _iL7; } set { _iL7 = value; } }
        public int L8 { get { return _iL8; } set { _iL8 = value; } }
        public int L9 { get { return _iL9; } set { _iL9 = value; } }
        public string Title { get { return _sTitle; } set { _sTitle = value; } }
        public string Code { get { return _sCode; } set { _sCode = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
