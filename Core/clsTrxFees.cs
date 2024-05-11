using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsTrxFees
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int    _iRecord_ID;
        private int    _iTrxCategory_ID;
        private int    _iTrxType_ID;
        private int    _iTrxEtiology_ID;
        private int    _iTrxClientsFees_ID;
        private int    _iEarnings_ID;
        private int    _iRevenue_ID;
        private int    _iView_ID;
        private string _sNotes;

        private string _sTrxCategory_Title;
        private string _sTrxType_Title;
        private string _sTrxEtiology_Title;
        private string _sTrxClientsFees_Title;
        private string _sEarnings_Title;
        private string _sRevenue_Title;
        private DataTable _dtList;

        public clsTrxFees()
        {
            this._iRecord_ID = 0;
            this._iTrxCategory_ID = 0;
            this._iTrxType_ID = 0;
            this._iTrxEtiology_ID = 0;
            this._iTrxClientsFees_ID = 0;
            this._iEarnings_ID = 0;
            this._iRevenue_ID = 0;
            this._iView_ID = 0;
            this._sNotes = "";

            this._sTrxCategory_Title = "";
            this._sTrxType_Title = "";
            this._sTrxEtiology_Title = "";
            this._sTrxClientsFees_Title = "";
            this._sEarnings_Title = "";
            this._sRevenue_Title = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTrxFees_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iTrxCategory_ID = Convert.ToInt32(drList["TrxCategory_ID"]);
                    this._iTrxType_ID = Convert.ToInt32(drList["TrxType_ID"]);
                    this._iTrxEtiology_ID = Convert.ToInt32(drList["TrxEtiology_ID"]);
                    this._iTrxClientsFees_ID = Convert.ToInt32(drList["TrxClientsFees_ID"]);

                    this._sTrxCategory_Title = drList["TrxCategory_Title"] + "";
                    this._sTrxType_Title = drList["TrxType_Title"] + "";
                    this._sTrxEtiology_Title = drList["TrxEtiology_Title"] + "";
                    this._sTrxClientsFees_Title = drList["TrxClientsFees_Title"] + "";

                    this._iEarnings_ID = Convert.ToInt16(drList["Earnings_ID"]);
                    this._iRevenue_ID = Convert.ToInt16(drList["Revenue_ID"]);
                    this._iView_ID = Convert.ToInt16(drList["View_ID"]);
                    this._sNotes = drList["Notes"] + "";

                    this._sEarnings_Title = drList["Earnings_Title"] + "";
                    this._sRevenue_Title = drList["Revenue_Title"] + "";
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            try
            {
                _dtList = new DataTable("TrxFees_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("TrxCategory_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("TrxType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("TrxEtiology_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("TrxClientsFees_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("TrxCategory_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("TrxType_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("TrxEtiology_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("TrxClientsFees_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Earnings_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Revenue_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("View_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Earnings_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Revenue_Title", System.Type.GetType("System.String"));

                conn.Open();
                cmd = new SqlCommand("GetTrxFees_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", "0"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["TrxCategory_ID"] = drList["TrxCategory_ID"];
                    dtRow["TrxType_ID"] = drList["TrxType_ID"];
                    dtRow["TrxEtiology_ID"] = drList["TrxEtiology_ID"];
                    dtRow["TrxClientsFees_ID"] = drList["TrxClientsFees_ID"];
                    dtRow["TrxCategory_Title"] = drList["TrxCategory_Title"] + "";
                    dtRow["TrxType_Title"] = drList["TrxType_Title"] + "";
                    dtRow["TrxEtiology_Title"] = drList["TrxEtiology_Title"] + "";
                    dtRow["TrxClientsFees_Title"] = drList["TrxClientsFees_Title"] + "";
                    dtRow["Earnings_ID"] = drList["Earnings_ID"];
                    dtRow["Revenue_ID"] = drList["Revenue_ID"];
                    dtRow["View_ID"] = drList["View_ID"];
                    dtRow["Notes"] = drList["Notes"] + "";
                    dtRow["Earnings_Title"] = drList["Earnings_Title"] + "";
                    dtRow["Revenue_Title"] = drList["Revenue_Title"] + "";
                    _dtList.Rows.Add(dtRow);
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
                using (cmd = new SqlCommand("InsertTrx_Fees", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@TrxCategory_ID", SqlDbType.Int).Value = _iTrxCategory_ID;
                    cmd.Parameters.Add("@TrxType_ID", SqlDbType.Int).Value = _iTrxType_ID;
                    cmd.Parameters.Add("@TrxEtiology_ID", SqlDbType.Int).Value = _iTrxEtiology_ID;
                    cmd.Parameters.Add("@TrxClientsFees_ID", SqlDbType.Int).Value = _iTrxClientsFees_ID;
                    cmd.Parameters.Add("@Earnings_ID", SqlDbType.Int).Value = _iEarnings_ID;
                    cmd.Parameters.Add("@Revenue_ID", SqlDbType.Int).Value = _iRevenue_ID;
                    cmd.Parameters.Add("@View_ID", SqlDbType.Int).Value = _iView_ID;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 100).Value = _sNotes;
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
                using (cmd = new SqlCommand("EditTrx_Fees", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@TrxCategory_ID", SqlDbType.Int).Value = _iTrxCategory_ID;
                    cmd.Parameters.Add("@TrxType_ID", SqlDbType.Int).Value = _iTrxType_ID;
                    cmd.Parameters.Add("@TrxEtiology_ID", SqlDbType.Int).Value = _iTrxEtiology_ID;
                    cmd.Parameters.Add("@TrxClientsFees_ID", SqlDbType.Int).Value = _iTrxClientsFees_ID;
                    cmd.Parameters.Add("@Earnings_ID", SqlDbType.Int).Value = _iEarnings_ID;
                    cmd.Parameters.Add("@Revenue_ID", SqlDbType.Int).Value = _iRevenue_ID;
                    cmd.Parameters.Add("@View_ID", SqlDbType.Int).Value = _iView_ID;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 100).Value = _sNotes;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void Edit_View_ID()
        {
            int i = 0;
            GetList();
            conn.Open();

            i = 0;
            foreach (DataRow dtRow in _dtList.Rows)
            {
                _iRecord_ID = Convert.ToInt32(dtRow["ID"]);
                i = i + 1;
                using (cmd = new SqlCommand("EditTrx_Fees_View_ID", conn))
                {                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@View_ID", SqlDbType.Int).Value = i;
                    cmd.ExecuteNonQuery();
                }
            }
            conn.Close();
        }
        public void DeleteRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Trx_Fees";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int TrxCategory_ID { get { return this._iTrxCategory_ID; } set { this._iTrxCategory_ID = value; } }
        public int TrxType_ID { get { return this._iTrxType_ID; } set { this._iTrxType_ID = value; } }
        public int TrxEtiology_ID { get { return this._iTrxEtiology_ID; } set { this._iTrxEtiology_ID = value; } }
        public int TrxClientsFees_ID { get { return this._iTrxClientsFees_ID; } set { this._iTrxClientsFees_ID = value; } }
        public string TrxCategory_Title { get { return this._sTrxCategory_Title; } set { this._sTrxCategory_Title = value; } }
        public string TrxType_Title { get { return this._sTrxType_Title; } set { this._sTrxType_Title = value; } }
        public string TrxEtiology_Title { get { return this._sTrxEtiology_Title; } set { this._sTrxEtiology_Title = value; } }
        public string TrxClienstFees_Title { get { return this._sTrxClientsFees_Title; } set { this._sTrxClientsFees_Title = value; } }
        public int Earnings_ID { get { return this._iEarnings_ID; } set { this._iEarnings_ID = value; } }
        public int Revenue_ID { get { return this._iRevenue_ID; } set { this._iRevenue_ID = value; } }
        public int View_ID { get { return this._iView_ID; } set { this._iView_ID = value; } }
        public string Notes { get { return this._sNotes; } set { this._sNotes = value; } }
        public string Earnings_Title { get { return this._sEarnings_Title; } set { this._sEarnings_Title = value; } }
        public string Revenue_Title { get { return this._sRevenue_Title; } set { this._sRevenue_Title = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}