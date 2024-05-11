using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsCustodyFees_Titles
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList;

        private int _iRecord_ID;
        private int _iSC_ID;
        private int _iCF_Quart;
        private int _iCF_Year;
        private DateTime _dDateIns;
        private int _iAuthor_ID;

        private DateTime _dDateFrom;
        private DateTime _dDateTo;
        private DataTable _dtList;

        public clsCustodyFees_Titles()
        {
            this._iRecord_ID = 0;
            this._iSC_ID = 0;
            this._iCF_Quart = 0;
            this._iCF_Year = 0;
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
                cmd.Parameters.Add(new SqlParameter("@Table", "CustodyFees_Titles"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iSC_ID = Convert.ToInt32(drList["SC_ID"]);
                    this._iCF_Quart = Convert.ToInt32(drList["CF_Quart"]);
                    this._iCF_Year = Convert.ToInt32(drList["CF_Year"]);
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
                cmd = new SqlCommand("GetCustodyFees_Title", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SC_ID", _iSC_ID));
                cmd.Parameters.Add(new SqlParameter("@CF_Year", _iCF_Year));
                cmd.Parameters.Add(new SqlParameter("@CF_Quart", _iCF_Quart));
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
                using (cmd = new SqlCommand("InsertCustodyFees_Title", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SC_ID", SqlDbType.Int).Value = _iSC_ID;
                    cmd.Parameters.Add("@CF_Quart", SqlDbType.Int).Value = _iCF_Quart;
                    cmd.Parameters.Add("@CF_Year", SqlDbType.Int).Value = _iCF_Year;
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
                using (cmd = new SqlCommand("EditCustodyFees_Title", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@SC_ID", SqlDbType.Int).Value = _iSC_ID;
                    cmd.Parameters.Add("@CF_Quart", SqlDbType.Int).Value = _iCF_Quart;
                    cmd.Parameters.Add("@CF_Year", SqlDbType.Int).Value = _iCF_Year;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "CustodyFees_Recs";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "FT_ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }

                using (cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "CustodyFees_Titles";
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
        public int CF_Quart { get { return this._iCF_Quart; } set { this._iCF_Quart = value; } }
        public int CF_Year { get { return this._iCF_Year; } set { this._iCF_Year = value; } }
        public DateTime DateIns { get { return this._dDateIns; } set { this._dDateIns = value; } }
        public int Author_ID { get { return this._iAuthor_ID; } set { this._iAuthor_ID = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}