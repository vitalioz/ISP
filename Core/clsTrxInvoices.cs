using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsTrxInvoices
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int     _iRecord_ID;
        private int     _iTrxCategory_ID;
        private int     _iTrxType_ID;
        private string  _sTitle_ISP;
        private string  _sTitle_Effect;
        private int     _iProductType_ID;
        private string  _sInvoice_Template;
        private int     _iClientType1_ID;
        private string  _sClientType1_Details;
        private int     _iClientType2_ID;
        private string  _sClientType2_Details;
        private int     _sNotes;

        private string _sTrxCategory_Title;
        private string _sTrxType_Title;
        private DataTable _dtList;

        public clsTrxInvoices()
        {
            this._iRecord_ID = 0;
            this._iTrxCategory_ID = 0;
            this._iTrxType_ID = 0;
            this._sTitle_ISP = "";
            this._sTitle_Effect = "";
            this._iProductType_ID = 0;
            this._sInvoice_Template = "";
            this._iClientType1_ID = 0;
            this._sClientType1_Details = "";
            this._iClientType2_ID = 0;
            this._sClientType2_Details = "";
            this._sNotes = 0;

            this._sTrxCategory_Title = "";
            this._sTrxType_Title = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTrxInvoices_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iTrxCategory_ID = Convert.ToInt32(drList["TrxCategory_ID"]);
                    this._iTrxType_ID = Convert.ToInt32(drList["TrxType_ID"]);
                    this._sTitle_ISP = drList["Title_ISP"] + "";
                    this._sTitle_Effect = drList["Title_Effect"] + "";
                    this._iProductType_ID = Convert.ToInt32(drList["ProductType_ID"]);
                    this._sInvoice_Template = drList["Invoice_Template"] + "";
                    this._iClientType1_ID = Convert.ToInt16(drList["ClientType1_ID"]);
                    this._sClientType1_Details = drList["ClientType1_Details"] + "";
                    this._iClientType2_ID = Convert.ToInt32(drList["ClientType2_ID"]);
                    this._sClientType2_Details = drList["ClientType2_Details"] + "";
                    this._sNotes = Convert.ToInt32(drList["Notes"]);
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
                _dtList = new DataTable("TrxInvoices_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("TrxCategory_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("TrxType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Title_ISP", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Title_Effect", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProductType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Invoice_Template", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientType1_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ClientType1_Details", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientType2_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ClientType2_Details", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("TrxCategory_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("TrxType_Title", System.Type.GetType("System.String"));

                conn.Open();
                cmd = new SqlCommand("GetTrxInvoices_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", "0"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["TrxCategory_ID"] = drList["TrxCategory_ID"];
                    dtRow["TrxType_ID"] = drList["TrxType_ID"];
                    dtRow["Title_ISP"] = drList["Title_ISP"] + "";
                    dtRow["Title_Effect"] = drList["Title_Effect"] + "";
                    dtRow["ProductType_ID"] = drList["ProductType_ID"];
                    dtRow["Invoice_Template"] = drList["Invoice_Template"];
                    dtRow["ClientType1_ID"] = drList["ClientType1_ID"];
                    dtRow["ClientType1_Details"] = drList["ClientType1_Details"];
                    dtRow["ClientType2_ID"] = drList["ClientType2_ID"];
                    dtRow["ClientType2_Details"] = drList["ClientType2_Details"];
                    dtRow["Notes"] = drList["Notes"];
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
                using (cmd = new SqlCommand("InsertTrx_Invoices", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@TrxCategory_ID", SqlDbType.Int).Value = _iTrxCategory_ID;
                    cmd.Parameters.Add("@TrxType_ID", SqlDbType.Int).Value = _iTrxType_ID;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 100).Value = _sTitle_ISP;
                    cmd.Parameters.Add("@ProductType_ID", SqlDbType.Int).Value = _iProductType_ID;
                    cmd.Parameters.Add("@Invoice_Template", SqlDbType.NVarChar, 100).Value = _sInvoice_Template;
                    cmd.Parameters.Add("@ClientType1_ID", SqlDbType.Int).Value = _iClientType1_ID;
                    cmd.Parameters.Add("@ClientType1_Details", SqlDbType.NVarChar, 100).Value = _sClientType1_Details;
                    cmd.Parameters.Add("@ClientType2_ID", SqlDbType.Int).Value = _iClientType2_ID;
                    cmd.Parameters.Add("@ClientType2_Details", SqlDbType.NVarChar, 100).Value = _sClientType2_Details;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 500).Value = _sNotes;

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
                using (cmd = new SqlCommand("EditTrx_Invoices", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@TrxCategory_ID", SqlDbType.Int).Value = _iTrxCategory_ID;
                    cmd.Parameters.Add("@TrxType_ID", SqlDbType.Int).Value = _iTrxType_ID;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 100).Value = _sTitle_ISP;
                    cmd.Parameters.Add("@ProductType_ID", SqlDbType.Int).Value = _iProductType_ID;
                    cmd.Parameters.Add("@Invoice_Template", SqlDbType.NVarChar, 100).Value = _sInvoice_Template;
                    cmd.Parameters.Add("@ClientType1_ID", SqlDbType.Int).Value = _iClientType1_ID;
                    cmd.Parameters.Add("@ClientType1_Details", SqlDbType.NVarChar, 100).Value = _sClientType1_Details;
                    cmd.Parameters.Add("@ClientType2_ID", SqlDbType.Int).Value = _iClientType2_ID;
                    cmd.Parameters.Add("@ClientType2_Details", SqlDbType.NVarChar, 100).Value = _sClientType2_Details;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 500).Value = _sNotes;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Trx_Invoices";
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
        public string Title_ISP { get { return this._sTitle_ISP; } set { this._sTitle_ISP = value; } }
        public string Title_Effect { get { return this._sTitle_Effect; } set { this._sTitle_Effect = value; } }
        public int ProductType_ID { get { return this._iProductType_ID; } set { this._iProductType_ID = value; } }
        public string Invoice_Template { get { return this._sInvoice_Template; } set { this._sInvoice_Template = value; } }
        public int ClientType1_ID { get { return this._iClientType1_ID; } set { this._iClientType1_ID = value; } }
        public string ClientType1_Details { get { return this._sClientType1_Details; } set { this._sClientType1_Details = value; } }
        public int ClientType2_ID { get { return this._iClientType2_ID; } set { this._iClientType2_ID = value; } }
        public string ClientType2_Details { get { return this._sClientType2_Details; } set { this._sClientType2_Details = value; } }
        public int Notes { get { return this._sNotes; } set { this._sNotes = value; } }
        public string TrxCategory_Title { get { return this._sTrxCategory_Title; } set { this._sTrxCategory_Title = value; } }
        public string TrxType_Title { get { return this._sTrxType_Title; } set { this._sTrxType_Title = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}