using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsInvoicesRTO_Details
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int _iRecord_ID;
        private int _iCommand_ID;
        private int _iInvoiceType;
        private int _iInvoiceTitles_ID;
        private float _sgRealQuantity;
        private string _sCurr;
        private float _sgRealPrice;
        private float _sgRealAmount;
        private float _sgFeesPercent;
        private float _sgFeesDiscountPercent;
        private float _sgFinishFeesPercent;
        private float _sgFinishFeesAmount;
        private float _sgFeesRate;
        private float _sgFeesAmountEUR;
        private float _sgMinFeesAmount;
        private float _sgMinFeesDiscountPercent;
        private float _sgMinFeesDiscountAmount;
        private float _sgFinishMinFeesAmount;
        private float _sgFeesProVAT;
        private float _sgFeesVAT;
        private float _sgCompanyFees;

        private string _sInvoice_Num;
        private string _sFileName;
        private DataTable _dtList;

        public clsInvoicesRTO_Details()
        {
            this._iRecord_ID = 0;
            _iCommand_ID = 0;
            _iInvoiceType = 0;
            _iInvoiceTitles_ID = 0;
            _sgRealQuantity = 0;
            _sCurr = "";
            _sgRealPrice = 0;
            _sgRealAmount = 0;
            _sgFeesPercent = 0;
            _sgFeesDiscountPercent = 0;
            _sgFinishFeesPercent = 0;
            _sgFinishFeesAmount = 0;
            _sgFeesRate = 0;
            _sgFeesAmountEUR = 0;
            _sgMinFeesAmount = 0;
            _sgMinFeesDiscountPercent = 0;
            _sgMinFeesDiscountAmount = 0;
            _sgFinishMinFeesAmount = 0;
            _sgFeesProVAT = 0;
            _sgFeesVAT = 0;
            _sgCompanyFees = 0;
            _sInvoice_Num = "";
            _sFileName = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetInvoicesRTO_Details", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Record_ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["Dtl_ID"]);
                    _iCommand_ID = Convert.ToInt32(drList["Dtl_Command_ID"]);
                    _iInvoiceType = Convert.ToInt32(drList["Dtl_InvoiceType"]);
                    _iInvoiceTitles_ID = Convert.ToInt32(drList["Dtl_InvoiceTitles_ID"]);
                    _sgRealQuantity = Convert.ToSingle(drList["Dtl_RealQuantity"]);
                    _sCurr = Convert.ToString(drList["Dtl_Curr"]);
                    _sgRealPrice = Convert.ToSingle(drList["Dtl_RealPrice"]);
                    _sgRealAmount = Convert.ToSingle(drList["Dtl_RealAmount"]);
                    _sgFeesPercent = Convert.ToSingle(drList["Dtl_FeesPercent"]);
                    _sgFeesDiscountPercent = Convert.ToSingle(drList["Dtl_FeesDiscountPercent"]);
                    _sgFinishFeesPercent = Convert.ToSingle(drList["Dtl_FinishFeesPercent"]);
                    _sgFinishFeesAmount = Convert.ToSingle(drList["Dtl_FinishFeesAmount"]);
                    _sgFeesRate = Convert.ToSingle(drList["Dtl_FeesRate"]);
                    _sgFeesAmountEUR = Convert.ToSingle(drList["Dtl_FeesAmountEUR"]);
                    _sgMinFeesAmount = Convert.ToSingle(drList["Dtl_MinFeesAmount"]);
                    _sgMinFeesDiscountPercent = Convert.ToSingle(drList["Dtl_MinFeesDiscountPercent"]);
                    _sgMinFeesDiscountAmount = Convert.ToSingle(drList["Dtl_MinFeesDiscountAmount"]);
                    _sgFinishMinFeesAmount = Convert.ToSingle(drList["Dtl_FinishMinFeesAmount"]);
                    _sgFeesProVAT = Convert.ToSingle(drList["Dtl_FeesProVAT"]);
                    _sgFeesVAT = Convert.ToSingle(drList["Dtl_FeesVAT"]);
                    _sgCompanyFees = Convert.ToSingle(drList["Dtl_CompanyFees"]);
                    _sInvoice_Num = drList["Code"] + " " + drList["Seira"] + " " + drList["Arithmos"];
                    _sFileName = Convert.ToString(drList["FileName"]);
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
                _dtList = new DataTable("InvoicesRTO_Details_List");
                dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Command_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("InvoiceTitles_ID", Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetInvoicesRTO_Details", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Command_ID", _iCommand_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];                                                      // ID - is SPAF_ID
                    this.dtRow["Command_ID"] = drList["Dtl_Command_ID"];
                    this.dtRow["InvoiceTitles_ID"] = drList["Dtl_InvoiceTitles_ID"];
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
                using (SqlCommand cmd = new SqlCommand("InsertInvoicesRTO_Details", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Command_ID", SqlDbType.Int).Value = _iCommand_ID;
                    cmd.Parameters.Add("@InvoiceType", SqlDbType.Int).Value = _iInvoiceType;
                    cmd.Parameters.Add("@InvoiceTitles_ID", SqlDbType.Int).Value = _iInvoiceTitles_ID;
                    cmd.Parameters.Add("@RealQuantity", SqlDbType.Int).Value = _sgRealQuantity;
                    cmd.Parameters.Add("@Curr", SqlDbType.NVarChar, 6).Value = _sCurr;
                    cmd.Parameters.Add("@RealPrice", SqlDbType.Float).Value = _sgRealPrice;
                    cmd.Parameters.Add("@RealAmount", SqlDbType.Float).Value = _sgRealAmount;
                    cmd.Parameters.Add("@FeesPercent", SqlDbType.Float).Value = _sgFeesPercent;
                    cmd.Parameters.Add("@FeesDiscountPercent", SqlDbType.Float).Value = _sgFeesDiscountPercent;
                    cmd.Parameters.Add("@FinishFeesPercent", SqlDbType.Float).Value = _sgFinishFeesPercent;
                    cmd.Parameters.Add("@FinishFeesAmount", SqlDbType.Float).Value = _sgFinishFeesAmount;
                    cmd.Parameters.Add("@FeesRate", SqlDbType.Float).Value = _sgFeesRate;
                    cmd.Parameters.Add("@FeesAmountEUR", SqlDbType.Float).Value = _sgFeesAmountEUR;
                    cmd.Parameters.Add("@MinFeesAmount", SqlDbType.Float).Value = _sgMinFeesAmount;
                    cmd.Parameters.Add("@MinFeesDiscountPercent", SqlDbType.Float).Value = _sgMinFeesDiscountPercent;
                    cmd.Parameters.Add("@MinFeesDiscountAmount", SqlDbType.Float).Value = _sgMinFeesDiscountAmount;
                    cmd.Parameters.Add("@FinishMinFeesAmount", SqlDbType.Float).Value = _sgFinishMinFeesAmount;
                    cmd.Parameters.Add("@FeesProVAT", SqlDbType.Float).Value = _sgFeesProVAT;
                    cmd.Parameters.Add("@FeesVAT", SqlDbType.Float).Value = _sgFeesVAT;
                    cmd.Parameters.Add("@CompanyFees", SqlDbType.Float).Value = _sgCompanyFees;

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
                using (SqlCommand cmd = new SqlCommand("EditInvoicesRTO_Details", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Command_ID", SqlDbType.Int).Value = _iCommand_ID;
                    cmd.Parameters.Add("@InvoiceType", SqlDbType.Int).Value = _iInvoiceType;
                    cmd.Parameters.Add("@InvoiceTitles_ID", SqlDbType.Int).Value = _iInvoiceTitles_ID;
                    cmd.Parameters.Add("@RealQuantity", SqlDbType.Int).Value = _sgRealQuantity;
                    cmd.Parameters.Add("@Curr", SqlDbType.NVarChar, 6).Value = _sCurr;
                    cmd.Parameters.Add("@RealPrice", SqlDbType.Float).Value = _sgRealPrice;
                    cmd.Parameters.Add("@RealAmount", SqlDbType.Float).Value = _sgRealAmount;
                    cmd.Parameters.Add("@FeesPercent", SqlDbType.Float).Value = _sgFeesPercent;
                    cmd.Parameters.Add("@FeesDiscountPercent", SqlDbType.Float).Value = _sgFeesDiscountPercent;
                    cmd.Parameters.Add("@FinishFeesPercent", SqlDbType.Float).Value = _sgFinishFeesPercent;
                    cmd.Parameters.Add("@FinishFeesAmount", SqlDbType.Float).Value = _sgFinishFeesAmount;
                    cmd.Parameters.Add("@FeesRate", SqlDbType.Float).Value = _sgFeesRate;
                    cmd.Parameters.Add("@FeesAmountEUR", SqlDbType.Float).Value = _sgFeesAmountEUR;
                    cmd.Parameters.Add("@MinFeesAmount", SqlDbType.Float).Value = _sgMinFeesAmount;
                    cmd.Parameters.Add("@MinFeesDiscountPercent", SqlDbType.Float).Value = _sgMinFeesDiscountPercent;
                    cmd.Parameters.Add("@MinFeesDiscountAmount", SqlDbType.Float).Value = _sgMinFeesDiscountAmount;
                    cmd.Parameters.Add("@FinishMinFeesAmount", SqlDbType.Float).Value = _sgFinishMinFeesAmount;
                    cmd.Parameters.Add("@FeesProVAT", SqlDbType.Float).Value = _sgFeesProVAT;
                    cmd.Parameters.Add("@FeesVAT", SqlDbType.Float).Value = _sgFeesVAT;
                    cmd.Parameters.Add("@CompanyFees", SqlDbType.Float).Value = _sgCompanyFees;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "InvoicesRTO_Details";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int Command_ID { get { return this._iCommand_ID; } set { this._iCommand_ID = value; } }
        public int InvoiceType { get { return this._iInvoiceType; } set { this._iInvoiceType = value; } }
        public int InvoiceTitles_ID { get { return this._iInvoiceTitles_ID; } set { this._iInvoiceTitles_ID = value; } }
        public float RealPrice { get { return this._sgRealPrice; } set { this._sgRealPrice = value; } }
        public string Curr { get { return this._sCurr; } set { this._sCurr = value; } }
        public float RealQuantity { get { return this._sgRealQuantity; } set { this._sgRealQuantity = value; } }
        public float RealAmount { get { return this._sgRealAmount; } set { this._sgRealAmount = value; } }
        public float FeesPercent { get { return this._sgFeesPercent; } set { this._sgFeesPercent = value; } }
        public float FeesDiscountPercent { get { return this._sgFeesDiscountPercent; } set { this._sgFeesDiscountPercent = value; } }
        public float FinishFeesPercent { get { return this._sgFinishFeesPercent; } set { this._sgFinishFeesPercent = value; } }
        public float FinishFeesAmount { get { return this._sgFinishFeesAmount; } set { this._sgFinishFeesAmount = value; } }
        public float FeesRate { get { return this._sgFeesRate; } set { this._sgFeesRate = value; } }
        public float FeesAmountEUR { get { return this._sgFeesAmountEUR; } set { this._sgFeesAmountEUR = value; } }
        public float MinFeesAmount { get { return this._sgMinFeesAmount; } set { this._sgMinFeesAmount = value; } }
        public float MinFeesDiscountPercent { get { return this._sgMinFeesDiscountPercent; } set { this._sgMinFeesDiscountPercent = value; } }
        public float MinFeesDiscountAmount { get { return this._sgMinFeesDiscountAmount; } set { this._sgMinFeesDiscountAmount = value; } }
        public float FinishMinFeesAmount { get { return this._sgFinishMinFeesAmount; } set { this._sgFinishMinFeesAmount = value; } }
        public float FeesProVAT { get { return this._sgFeesProVAT; } set { this._sgFeesProVAT = value; } }
        public float FeesVAT { get { return this._sgFeesVAT; } set { this._sgFeesVAT = value; } }
        public float CompanyFees { get { return this._sgCompanyFees; } set { this._sgCompanyFees = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}






