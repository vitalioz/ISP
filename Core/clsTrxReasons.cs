using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsTrxReasons
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int _iRecord_ID;
        private int _iTrxCategory_ID;
        private int _iTrxType_ID;
        private string _sTitle;
        private int _iExecutionAgent;
        private int _iExecutionVenue;
        private int _iCustodian;
        private int _iDepository;
        private int _iTaxHome;
        private int _iVAT;
        private int _iSalesFees;
        private int _iIncomeTax;

        private string _sTrxCategory_Title;
        private string _sTrxType_Title;
        private DataTable _dtList;

        public clsTrxReasons()
        {
            this._iRecord_ID = 0;
            this._iTrxCategory_ID = 0;
            this._iTrxType_ID = 0;
            this._sTitle = "";
            this._iExecutionAgent = 0;
            this._iExecutionVenue = 0;
            this._iCustodian = 0;
            this._iDepository = 0;
            this._iTaxHome = 0;
            this._iVAT = 0;
            this._iSalesFees = 0;
            this._iIncomeTax = 0;

            this._sTrxCategory_Title = "";
            this._sTrxType_Title = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTrxReasons_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iTrxCategory_ID = Convert.ToInt32(drList["TrxCategory_ID"]);
                    this._iTrxType_ID = Convert.ToInt32(drList["TrxType_ID"]);
                    this._sTitle = drList["Title"] + "";
                    this._iExecutionAgent = Convert.ToInt32(drList["ExecutionAgent"]);
                    this._iExecutionVenue = Convert.ToInt16(drList["ExecutionVenue"]);
                    this._iCustodian = Convert.ToInt16(drList["Custodian"]);
                    this._iDepository = Convert.ToInt32(drList["Depository"]);
                    this._iTaxHome = Convert.ToInt32(drList["TaxHome"]);
                    this._iVAT = Convert.ToInt32(drList["VAT"]);
                    this._iSalesFees = Convert.ToInt32(drList["SalesFees"]);
                    this._iIncomeTax = Convert.ToInt32(drList["IncomeTax"]);
                    this._sTrxCategory_Title = "";
                    this._sTrxType_Title = "";
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
                _dtList = new DataTable("TrxReasons_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("TrxCategory_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("TrxType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ExecutionAgent", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ExecutionVenue", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Custodian", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Depository", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("TaxHome", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("VAT", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("SalesFees", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("IncomeTax", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("TrxCategory_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("TrxType_Title", System.Type.GetType("System.String"));

                conn.Open();
                cmd = new SqlCommand("GetTrxReasons_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", "0"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["TrxCategory_ID"] = drList["TrxCategory_ID"];
                    dtRow["TrxType_ID"] = drList["TrxType_ID"];
                    dtRow["Title"] = drList["Title"] + "";
                    dtRow["ExecutionAgent"] = drList["ExecutionAgent"];
                    dtRow["ExecutionVenue"] = drList["ExecutionVenue"];
                    dtRow["Custodian"] = drList["Custodian"];
                    dtRow["Depository"] = drList["Depository"];
                    dtRow["TaxHome"] = drList["TaxHome"];
                    dtRow["SalesFees"] = drList["SalesFees"];                 
                    dtRow["IncomeTax"] = drList["IncomeTax"];
                    dtRow["TrxCategory_Title"] = drList["TrxCategory_Title"] + "";
                    dtRow["TrxType_Title"] = drList["TrxType_Title"] + "";
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
                using (cmd = new SqlCommand("InsertTrx_Reasons", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@TrxCategory_ID", SqlDbType.Int).Value = _iTrxCategory_ID;
                    cmd.Parameters.Add("@TrxType_ID", SqlDbType.Int).Value = _iTrxType_ID;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 100).Value = _sTitle;
                    cmd.Parameters.Add("@ExecutionAgent", SqlDbType.Int).Value = _iExecutionAgent;
                    cmd.Parameters.Add("@ExecutionVenue", SqlDbType.Int).Value = _iExecutionVenue;
                    cmd.Parameters.Add("@Custodian", SqlDbType.Int).Value = _iCustodian;
                    cmd.Parameters.Add("@Depository", SqlDbType.Int).Value = _iDepository;
                    cmd.Parameters.Add("@TaxHome", SqlDbType.Int).Value = _iTaxHome;
                    cmd.Parameters.Add("@VAT", SqlDbType.Int).Value = _iVAT;
                    cmd.Parameters.Add("@SalesFees", SqlDbType.Int).Value = _iSalesFees;
                    cmd.Parameters.Add("@IncomeTax", SqlDbType.DateTime).Value = _iIncomeTax;

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
                using (cmd = new SqlCommand("EditTrx_Reasons", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@TrxCategory_ID", SqlDbType.Int).Value = _iTrxCategory_ID;
                    cmd.Parameters.Add("@TrxType_ID", SqlDbType.Int).Value = _iTrxType_ID;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 100).Value = _sTitle;
                    cmd.Parameters.Add("@ExecutionAgent", SqlDbType.Int).Value = _iExecutionAgent;
                    cmd.Parameters.Add("@ExecutionVenue", SqlDbType.Int).Value = _iExecutionVenue;
                    cmd.Parameters.Add("@Custodian", SqlDbType.Int).Value = _iCustodian;
                    cmd.Parameters.Add("@Depository", SqlDbType.Int).Value = _iDepository;
                    cmd.Parameters.Add("@TaxHome", SqlDbType.Int).Value = _iTaxHome;
                    cmd.Parameters.Add("@VAT", SqlDbType.Int).Value = _iVAT;
                    cmd.Parameters.Add("@SalesFees", SqlDbType.Int).Value = _iSalesFees;
                    cmd.Parameters.Add("@IncomeTax", SqlDbType.DateTime).Value = _iIncomeTax;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Trx_Reasons";
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
        public string Title { get { return this._sTitle; } set { this._sTitle = value; } }
        public int ExecutionAgent { get { return this._iExecutionAgent; } set { this._iExecutionAgent = value; } }
        public int ExecutionVenue { get { return this._iExecutionVenue; } set { this._iExecutionVenue = value; } }
        public int Custodian { get { return this._iCustodian; } set { this._iCustodian = value; } }
        public int Depository { get { return this._iDepository; } set { this._iDepository = value; } }
        public int TaxHome { get { return this._iTaxHome; } set { this._iTaxHome = value; } }
        public int VAT { get { return this._iVAT; } set { this._iVAT = value; } }
        public int SalesFees { get { return this._iSalesFees; } set { this._iSalesFees = value; } }
        public int IncomeTax { get { return this._iIncomeTax; } set { this._iIncomeTax = value; } }
        public string TrxCategory_Title { get { return this._sTrxCategory_Title; } set { this._sTrxCategory_Title = value; } }
        public string TrxType_Title { get { return this._sTrxType_Title; } set { this._sTrxType_Title = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}