using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsManagmentFees_Titles
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList;

        private int _iRecord_ID;
        private int _iSC_ID;
        private int _iMF_Quart;
        private int _iMF_Year;
        private DateTime _dDateIns;
        private int _iAuthor_ID;

        private DateTime _dDateFrom;
        private DateTime _dDateTo;
        private DataTable _dtList;

        public clsManagmentFees_Titles()
        {
            this._iRecord_ID = 0;
            this._iSC_ID = 0;
            this._iMF_Quart = 0;
            this._iMF_Year = 0;
            this._dDateIns = Convert.ToDateTime("1900/01/01");
            this._iAuthor_ID = 0;

            this._dDateFrom = Convert.ToDateTime("1900/01/01");
            this._dDateTo = Convert.ToDateTime("1900/01/01");
        }
  
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ManagmentFees_Titles"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iSC_ID = Convert.ToInt32(drList["SC_ID"]);
                    this._iMF_Quart = Convert.ToInt32(drList["MF_Quart"]);
                    this._iMF_Year = Convert.ToInt32(drList["MF_Year"]);
                    this._dDateIns = Convert.ToDateTime(drList["DateIns"]);
                    this._iAuthor_ID = Convert.ToInt32(drList["Author_ID"]);
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
                cmd = new SqlCommand("GetManagmentFees_Title", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SC_ID", _iSC_ID));
                cmd.Parameters.Add(new SqlParameter("@MF_Year", _iMF_Year));
                cmd.Parameters.Add(new SqlParameter("@MF_Quart", _iMF_Quart));
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
                using (cmd = new SqlCommand("InsertManagmentFees_Title", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SC_ID", SqlDbType.Int).Value = _iSC_ID;
                    cmd.Parameters.Add("@MF_Quart", SqlDbType.Int).Value = _iMF_Quart;
                    cmd.Parameters.Add("@MF_Year", SqlDbType.Int).Value = _iMF_Year;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dDateIns;
                    cmd.Parameters.Add("@Author_ID", SqlDbType.Int).Value = _iAuthor_ID;
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
                using (cmd = new SqlCommand("EditManagmentFees_Title", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@SC_ID", SqlDbType.Int).Value = _iSC_ID;
                    cmd.Parameters.Add("@MF_Quart", SqlDbType.Int).Value = _iMF_Quart;
                    cmd.Parameters.Add("@MF_Year", SqlDbType.Int).Value = _iMF_Year;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dDateIns;
                    cmd.Parameters.Add("@Author_ID", SqlDbType.Int).Value = _iAuthor_ID;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ManagmentFees_Recs";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "FT_ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }

                using (cmd = new SqlCommand("DeleteRecord", conn))
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
        public int SC_ID { get { return this._iSC_ID; } set { this._iSC_ID = value; } }
        public int MF_Quart { get { return this._iMF_Quart; } set { this._iMF_Quart = value; } }
        public int MF_Year { get { return this._iMF_Year; } set { this._iMF_Year = value; } }
        public DateTime DateIns { get { return this._dDateIns; } set { this._dDateIns = value; } }
        public int Author_ID { get { return this._iAuthor_ID; } set { this._iAuthor_ID = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}