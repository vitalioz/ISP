using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsAdminFees_Titles
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        IDataReader drList;
        DataColumn dtCol;
        DataRow dtRow;

        private int _iRecord_ID;
        private DateTime _dDateIns;
        private int _iAuthor_ID;
        private int _iStatus;
        private int _iSC_ID;
        private int _iAF_Quart;
        private int _iAF_Year;

        private DateTime _dDateFrom;
        private DateTime _dDateTo;
        private int _iServiceProvider_ID;
        private DataTable _dtList;

        public clsAdminFees_Titles()
        {
            this._iRecord_ID = 0;
            this._dDateIns = Convert.ToDateTime("1900/01/01");
            this._iAuthor_ID = 0;
            this._iStatus = 0;
            this._iSC_ID = 0;
            this._iAF_Quart = 0;
            this._iAF_Year = 0;

            this._dDateFrom = Convert.ToDateTime("1900/01/01");
            this._dDateTo = Convert.ToDateTime("1900/01/01");
            this._iServiceProvider_ID = 0;
        }

        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "AdminFees_Titles"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._dDateIns = Convert.ToDateTime(drList["DateIns"]);
                    this._iAuthor_ID = Convert.ToInt32(drList["Author_ID"]);
                    this._iStatus = Convert.ToInt32(drList["Status"]);
                    this._iSC_ID = Convert.ToInt32(drList["SC_ID"]);
                    this._iAF_Quart = Convert.ToInt32(drList["AF_Quart"]);
                    this._iAF_Year = Convert.ToInt32(drList["AF_Year"]);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetRecord_Title()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetAdminFees_Title", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Provider_ID", _iServiceProvider_ID));
                cmd.Parameters.Add(new SqlParameter("@AF_Year", _iAF_Year));
                cmd.Parameters.Add(new SqlParameter("@AF_Quart", _iAF_Quart));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
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
                using (SqlCommand cmd = new SqlCommand("InsertAdminFees_Title", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dDateIns;
                    cmd.Parameters.Add("@Author_ID", SqlDbType.Int).Value = _iAuthor_ID;
                    cmd.Parameters.Add("@SC_ID", SqlDbType.Int).Value = _iSC_ID;
                    cmd.Parameters.Add("@AF_Quart", SqlDbType.Int).Value = _iAF_Quart;
                    cmd.Parameters.Add("@AF_Year", SqlDbType.Int).Value = _iAF_Year;
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
                using (SqlCommand cmd = new SqlCommand("EditAdminFees_Title", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dDateIns;
                    cmd.Parameters.Add("@Author_ID", SqlDbType.Int).Value = _iAuthor_ID;
                    cmd.Parameters.Add("@SC_ID", SqlDbType.Int).Value = _iSC_ID;
                    cmd.Parameters.Add("@AF_Quart", SqlDbType.Int).Value = _iAF_Quart;
                    cmd.Parameters.Add("@AF_Year", SqlDbType.Int).Value = _iAF_Year;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "AdminFees_Recs";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "FT_ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ManagmentFees_Titles";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public DateTime DateIns { get { return this._dDateIns; } set { this._dDateIns = value; } }
        public int Author_ID { get { return this._iAuthor_ID; } set { this._iAuthor_ID = value; } }
        public int Status { get { return this._iStatus; } set { this._iStatus = value; } }
        public int SC_ID { get { return this._iSC_ID; } set { this._iSC_ID = value; } }
        public int AF_Quart { get { return this._iAF_Quart; } set { this._iAF_Quart = value; } }
        public int AF_Year { get { return this._iAF_Year; } set { this._iAF_Year = value; } }
        public int ServiceProvider_ID { get { return this._iServiceProvider_ID; } set { this._iServiceProvider_ID = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}