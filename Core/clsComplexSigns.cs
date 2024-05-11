using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsComplexSigns
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int    _iRecord_ID;
        private string _sTitle;
        private int    _iRTO;
        private int    _iAdvisory;
        private int    _iDiscretionary;

        private DataTable _dtList;

        public clsComplexSigns()
        {
            this._iRecord_ID = 0;
            this._sTitle = "";
            this._iRTO = 0;
            this._iAdvisory = 0;
            this._iDiscretionary = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ComplexSigns"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._sTitle = drList["Title"] + "";
                    this._iRTO = Convert.ToInt32(drList["RTO"]);
                    this._iAdvisory = Convert.ToInt32(drList["Advisory"]);
                    this._iDiscretionary = Convert.ToInt32(drList["Discretionary"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("Title", typeof(string));
            _dtList.Columns.Add("RTO", typeof(int));
            _dtList.Columns.Add("Advisory", typeof(int));
            _dtList.Columns.Add("Discretionary", typeof(int));

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ComplexSigns"));
                cmd.Parameters.Add(new SqlParameter("@Col", ""));
                cmd.Parameters.Add(new SqlParameter("@Value", ""));
                cmd.Parameters.Add(new SqlParameter("@Order", "Title"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["Title"], drList["RTO"], drList["Advisory"], drList["Discretionary"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void InsertRecord()
        {


            using (var conn = new SqlConnection(Global.connStr))
            using (var command = new SqlCommand("InsertComplexSign", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                command.ExecuteNonQuery();
            }


        }
        public void EditRecord()
        {

        }
        public void DeleteRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ComplexSigns";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return _iRecord_ID; } set { _iRecord_ID = value; } }
        public string Title { get { return _sTitle; } set { _sTitle = value; } }
        public int RTO { get { return _iRTO; } set { _iRTO = value; } }
        public int Advisory { get { return _iAdvisory; } set { _iAdvisory = value; } }
        public int Discretionary { get { return _iDiscretionary; } set { _iDiscretionary = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
