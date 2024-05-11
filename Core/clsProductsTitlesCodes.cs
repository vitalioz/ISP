using System;                 
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsProductsTitlesCodes
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int _iRecord_ID;
        private DateTime _dFrom;
        private DateTime _dTo;
        private int _iShare_ID;
        private int _iShareTitle_ID;
        private int _iShareCode_ID;

        private DateTime _dToday;
        private DataTable _dtList;
        public clsProductsTitlesCodes()
        {
            this._iRecord_ID = 0;
            this._dFrom = Convert.ToDateTime("1900/01/01");
            this._dTo = Convert.ToDateTime("1900/01/01");
            this._iShare_ID = 0;
            this._iShareTitle_ID = 0;
            this._iShareCode_ID = 0;
            this._dToday = Convert.ToDateTime("1900/01/01");
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Shares_Titles_Codes"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", "ID"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._dFrom = Convert.ToDateTime(drList["DateFrom"]);
                    this._dTo = Convert.ToDateTime(drList["DateTo"]);
                    this._iShare_ID = Convert.ToInt32(drList["Share_ID"]);
                    this._iShareTitle_ID = Convert.ToInt32(drList["ShareTitles_ID"]);
                    this._iShareCode_ID = Convert.ToInt32(drList["ShareCodes_ID"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetRecord_Date()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetShareCodeTitle_Date", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Share_ID", _iShare_ID));
                cmd.Parameters.Add(new SqlParameter("@Today", _dToday));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._dFrom = Convert.ToDateTime(drList["DateFrom"]);
                    this._dTo = Convert.ToDateTime(drList["DateTo"]);
                    this._iShare_ID = Convert.ToInt32(drList["Share_ID"]);
                    this._iShareTitle_ID = Convert.ToInt32(drList["ShareTitles_ID"]);
                    this._iShareCode_ID = Convert.ToInt32(drList["ShareCodes_ID"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetRecord_Code()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetShareCodeTitle_Code", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Share_ID", _iShare_ID));
                cmd.Parameters.Add(new SqlParameter("@ShareTitle_ID", _iShareTitle_ID));
                cmd.Parameters.Add(new SqlParameter("@ShareCode_ID", _iShareCode_ID));
                cmd.Parameters.Add(new SqlParameter("@Today", _dToday));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._dFrom = Convert.ToDateTime(drList["DateFrom"]);
                    this._dTo = Convert.ToDateTime(drList["DateTo"]);
                    this._iShare_ID = Convert.ToInt32(drList["Share_ID"]);
                    this._iShareTitle_ID = Convert.ToInt32(drList["ShareTitles_ID"]);
                    this._iShareCode_ID = Convert.ToInt32(drList["ShareCodes_ID"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable("Contract_Details_Packages_List");
            dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("DateFrom", Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("DateTo", Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("Share_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ShareTitle_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ShareCode_ID", Type.GetType("System.Int32"));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Contracts_Details_Packages"));
                cmd.Parameters.Add(new SqlParameter("@Col", "Share_ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iShare_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", "ID"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = this._dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["DateFrom"] = drList["DateFrom"];
                    this.dtRow["DateTo"] = drList["DateTo"];
                    this.dtRow["Share_ID"] = drList["Share_ID"];
                    this.dtRow["ShareTitle_ID"] = drList["ShareTitle_ID"];
                    this.dtRow["ShareCode_ID"] = drList["ShareCode_ID"];
                    this._dtList.Rows.Add(dtRow);
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
                using (SqlCommand cmd = new SqlCommand("InsertShareTitleCode", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = this._dFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = this._dTo;
                    cmd.Parameters.Add("@Share_ID", SqlDbType.Int).Value = this._iShare_ID;
                    cmd.Parameters.Add("@ShareTitles_ID", SqlDbType.Int).Value = this._iShareTitle_ID;
                    cmd.Parameters.Add("@ShareCodes_ID", SqlDbType.Int).Value = this._iShareCode_ID;
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
                using (SqlCommand cmd = new SqlCommand("EditShareTitleCode", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = this._iRecord_ID;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = this._dFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = this._dTo;
                    cmd.Parameters.Add("@Share_ID", SqlDbType.Int).Value = this._iShare_ID;
                    cmd.Parameters.Add("@ShareTitles_ID", SqlDbType.Int).Value = this._iShareTitle_ID;
                    cmd.Parameters.Add("@ShareCodes_ID", SqlDbType.Int).Value = this._iShareCode_ID;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Shares_Titles_Codes";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public DateTime DateFrom { get { return this._dFrom; } set { this._dFrom = value; } }
        public DateTime DateTo { get { return this._dTo; } set { this._dTo = value; } }
        public int Share_ID { get { return this._iShare_ID; } set { this._iShare_ID = value; } }
        public int ShareTitle_ID { get { return this._iShareTitle_ID; } set { this._iShareTitle_ID = value; } }
        public int ShareCode_ID { get { return this._iShareCode_ID; } set { this._iShareCode_ID = value; } }
        public DateTime Today { get { return this._dToday; } set { this._dToday = value; } }
        public DataTable List  { get { return _dtList; } set { _dtList = value; } }
    }
}
