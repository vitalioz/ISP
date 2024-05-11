using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Core
{
    public class clsSectors
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlDataReader drList = null;

        private int _iRecord_ID;
        private int _iL1;
        private int _iL2;
        private int _iL3;
        private int _iL4;
        private int _iL5;
        private string _sTitle;

        private DataTable _dtList;

        public clsSectors()
        {
            this._iRecord_ID = 0;
            this._iL1 = 0;
            this._iL2 = 0;
            this._iL3 = 0;
            this._iL4 = 0;
            this._iL5 = 0;
            this._sTitle = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Sectors"));
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
                    this._sTitle = drList["Title"] + "";
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
            _dtList.Columns.Add("Title", typeof(string));

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetSectors", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@L1", _iL1));
                cmd.Parameters.Add(new SqlParameter("@L2", _iL2));
                cmd.Parameters.Add(new SqlParameter("@L3", _iL3));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["L1"], drList["L2"], drList["L3"], drList["L4"], drList["L5"], drList["Title"]);
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

                using (SqlCommand cmd1 = new SqlCommand("InsertSectors", conn))
                {
                    SqlParameter outParam1 = new SqlParameter("@ID", SqlDbType.Int);
                    outParam1.Direction = ParameterDirection.Output;
                    cmd1.Parameters.Add(outParam1);

                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add("@L1", SqlDbType.Int).Value = this._iL1;
                    cmd1.Parameters.Add("@L2", SqlDbType.Int).Value = this._iL2;
                    cmd1.Parameters.Add("@L3", SqlDbType.Int).Value = this._iL3;
                    cmd1.Parameters.Add("@L4", SqlDbType.Int).Value = this._iL4;
                    cmd1.Parameters.Add("@L5", SqlDbType.Int).Value = this._iL5;
                    cmd1.Parameters.Add("@Title", SqlDbType.Int).Value = this._sTitle;
                    cmd1.ExecuteNonQuery();
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
                 using (SqlCommand cmd1 = new SqlCommand("EditSectors", conn))
                {
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add("@ID", SqlDbType.Int).Value = this._iRecord_ID;
                    cmd1.Parameters.Add("@L1", SqlDbType.Int).Value = this._iL1;
                    cmd1.Parameters.Add("@L2", SqlDbType.Int).Value = this._iL2;
                    cmd1.Parameters.Add("@L3", SqlDbType.Int).Value = this._iL3;
                    cmd1.Parameters.Add("@L4", SqlDbType.Int).Value = this._iL4;
                    cmd1.Parameters.Add("@L5", SqlDbType.Int).Value = this._iL5;
                    cmd1.Parameters.Add("@Title", SqlDbType.Int).Value = this._sTitle;
                    cmd1.ExecuteNonQuery();
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Sectors";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
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
        public string Title { get { return _sTitle; } set { _sTitle = value; } }
       public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
