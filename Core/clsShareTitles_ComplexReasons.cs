using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace Core
{
    public class clsShareTitles_ComplexReasons
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int _iRecord_ID;
        private int _iShareTitles_ID;                                           
        private int _iComplexReason_ID;

        private DataTable _dtList;

        public clsShareTitles_ComplexReasons()
        {
            this._iRecord_ID = 0;
            this._iShareTitles_ID = 0;
            this._iComplexReason_ID = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", this._iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iShareTitles_ID = Convert.ToInt32(drList["ShareTitles_ID"]);
                    this._iComplexReason_ID = Convert.ToInt32(drList["ComplexReason_ID_ID"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("ShareTitles_ID", typeof(int));
            _dtList.Columns.Add("ComplexReason_ID", typeof(int));
            _dtList.Columns.Add("FileName", typeof(string));
            _dtList.Columns.Add("DateIns", typeof(DateTime));
            _dtList.Columns.Add("User_ID", typeof(int));
            _dtList.Columns.Add("DocTypes_Title", typeof(string));

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ShareTitles_ID", _iShareTitles_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["ShareTitles_ID"], drList["ComplexReason_ID"], drList["FileName"], drList["DateIns"], drList["User_ID"], drList["DocTypes_Title"]);
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
                using (cmd = new SqlCommand("InsertShareTitle_ComplexReason", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ShareTitles_ID", SqlDbType.Int).Value = this._iShareTitles_ID;
                    cmd.Parameters.Add("@ComplexReason_ID", SqlDbType.Int).Value = this._iComplexReason_ID;
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
                using (SqlCommand cmd = new SqlCommand("EditShareTitle_ComplexReason", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = this._iRecord_ID;
                    cmd.Parameters.Add("@ShareTitles_ID", SqlDbType.Int).Value = this._iShareTitles_ID;
                    cmd.Parameters.Add("@ComplexReason_ID", SqlDbType.Int).Value = this._iComplexReason_ID;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ShareTitle_ComplexReason";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return _iRecord_ID; } set { _iRecord_ID = value; } }
        public int ShareTitles_ID { get { return _iShareTitles_ID; } set { _iShareTitles_ID = value; } }
        public int ComplexReason_ID { get { return _iComplexReason_ID; } set { _iComplexReason_ID = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
