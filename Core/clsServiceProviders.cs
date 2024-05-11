using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsServiceProviders
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int    _iRecord_ID;
        private int    _iProviderType;
        private string _sTitle;
        private string _sAlias;
        private string _sSeira;
        private float  _fltVAT_FP;
        private float  _fltVAT_NP;
        private string _sMainCurr;
        private int    _iInforming_Statement;
        private int    _iInforming_Misc;
        private int    _iInforming_ConvertFile;
        private int    _iSendOrders;
        private int    _iFeesMode;
        private string _sEffectCode;
        private string _sLEI;
        private string _sFIX_DB;
        private string _sHFAccount_Own;
        private string _sHFAccount_Clients;
        private int    _iBestExecution;
        private string _sPriceTable;
        private string _sDepositoryTitle;
        private int    _iAktive;

        private int    _iCustodyProvider_ID;
        private int    _iProduct_ID;
        private int    _iProductCategory_ID;
        private DateTime _dAktionDate;
        private DataTable _dtList;

        public clsServiceProviders()
        {
            this._iRecord_ID = 0;
            this._iProviderType = 0;
            this._sTitle = "";
            this._sAlias = "";
            this._sSeira = "";
            this._fltVAT_FP = 0;
            this._fltVAT_NP = 0;
            this._sMainCurr = "";
            this._iInforming_Statement = 0;
            this._iInforming_Misc = 0;
            this._iInforming_ConvertFile = 0;
            this._iSendOrders = 0;
            this._iFeesMode = 0;
            this._sEffectCode = "";
            this._sLEI = "";
            this._sFIX_DB = "";
            this._sHFAccount_Own = "";
            this._sHFAccount_Clients = "";
            this._iBestExecution = 0;
            this._sPriceTable = "";
            this._sDepositoryTitle = "";
            this._iAktive = 0;

            this._iCustodyProvider_ID = 0;
            this._iProduct_ID = 0;
            this._iProductCategory_ID = 0;
            this._dAktionDate = Convert.ToDateTime("1900/01/01");
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ServiceProviders"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iProviderType = Convert.ToInt32(drList["ProviderType"]);
                    this._sTitle = drList["Title"].ToString();
                    this._sAlias = drList["Alias"].ToString();
                    this._sSeira = drList["Seira"].ToString();
                    this._fltVAT_FP = Convert.ToSingle(drList["VAT_FP"]);
                    this._fltVAT_NP = Convert.ToSingle(drList["VAT_NP"]);
                    this._sMainCurr = drList["MainCurr"].ToString();
                    this._iInforming_Statement = Convert.ToInt32(drList["Informing_Statement"]);
                    this._iInforming_Misc = Convert.ToInt32(drList["Informing_Misc"]);
                    this._iInforming_ConvertFile = Convert.ToInt32(drList["Informing_ConvertFile"]);
                    this._iSendOrders = Convert.ToInt32(drList["SendOrders"]);
                    this._iFeesMode = Convert.ToInt32(drList["FeesMode"]);
                    this._sEffectCode = drList["EffectCode"].ToString();
                    this._sLEI = drList["LEI"].ToString();
                    this._sFIX_DB = drList["FIX_DB"].ToString();
                    this._sHFAccount_Own = drList["HFAccount_Own"].ToString();
                    this._sHFAccount_Clients = drList["HFAccount_Clients"].ToString();
                    this._iBestExecution = Convert.ToInt32(drList["BestExecution"]);
                    this._sPriceTable = drList["PriceTable"].ToString();
                    this._sDepositoryTitle = drList["DepositoryTitle"].ToString();
                    this._iAktive = Convert.ToInt32(drList["Aktive"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetRecord_Executions_Settlement()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("sp_GetCommands_Executions_Settlement", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ServiceProvider_ID", _iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@AktionDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@Product_ID", _iProduct_ID));
                cmd.Parameters.Add(new SqlParameter("@ProductCategory_ID", _iProductCategory_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _iCustodyProvider_ID = Convert.ToInt32(drList["ID"]);
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
            _dtList.Columns.Add("Alias", typeof(string));
            _dtList.Columns.Add("Seira", typeof(string));
            _dtList.Columns.Add("ProviderType", typeof(int));
            _dtList.Columns.Add("PriceTable", typeof(string));
            _dtList.Columns.Add("DepositoryTitle", typeof(string));
            _dtList.Columns.Add("FIX_DB", typeof(string));
            _dtList.Columns.Add("HFAccount_Own", typeof(string));
            _dtList.Columns.Add("HFAccount_Clients", typeof(string));
            _dtList.Columns.Add("BestExecution", typeof(int));
            _dtList.Columns.Add("Aktive", typeof(int));

            _dtList.Rows.Add(0, "Όλοι", "", "", 0, "", "", "", "", "", 0, 1);
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ServiceProviders"));
                cmd.Parameters.Add(new SqlParameter("@Col", ""));
                cmd.Parameters.Add(new SqlParameter("@Value", ""));
                cmd.Parameters.Add(new SqlParameter("@Order", "Title"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                   _dtList.Rows.Add(drList["ID"], drList["Title"], drList["Alias"], drList["Seira"], drList["ProviderType"], drList["PriceTable"], 
                       drList["DepositoryTitle"], drList["FIX_DB"], drList["HFAccount_Own"], drList["HFAccount_Clients"], drList["BestExecution"], drList["Aktive"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList_FX()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("Title", typeof(string));

            _dtList.Rows.Add(0, "-");
            try
            {
                conn.Open();
                cmd = new SqlCommand("sp_GetCommands_FX_Providers", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AktionDate", _dAktionDate));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["Title"]);
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
                using (cmd = new SqlCommand("InsertServiceProvider", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ProviderType", SqlDbType.Int).Value = _iProviderType;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 80).Value = _sTitle;
                    cmd.Parameters.Add("@Alias", SqlDbType.NVarChar, 10).Value = _sAlias;
                    cmd.Parameters.Add("@Seira", SqlDbType.NVarChar, 5).Value = _sSeira;
                    cmd.Parameters.Add("@VAT_FP", SqlDbType.Float).Value = _fltVAT_FP;
                    cmd.Parameters.Add("@VAT_NP", SqlDbType.Float).Value = _fltVAT_NP;
                    cmd.Parameters.Add("@MainCurr", SqlDbType.NVarChar, 6).Value = _sMainCurr;
                    cmd.Parameters.Add("@Informing_Statement", SqlDbType.Int).Value = _iInforming_Statement;
                    cmd.Parameters.Add("@Informing_Misc", SqlDbType.Int).Value = _iInforming_Misc;
                    cmd.Parameters.Add("@Informing_ConvertFile", SqlDbType.Int).Value = _iInforming_ConvertFile;
                    cmd.Parameters.Add("@SendOrders", SqlDbType.Int).Value = _iSendOrders;
                    cmd.Parameters.Add("@FeesMode", SqlDbType.Int).Value = _iFeesMode;
                    cmd.Parameters.Add("@EffectCode", SqlDbType.NVarChar, 20).Value = _sEffectCode;
                    cmd.Parameters.Add("@LEI", SqlDbType.NVarChar, 50).Value = _sLEI;
                    cmd.Parameters.Add("@FIX_DB", SqlDbType.NVarChar, 50).Value = _sFIX_DB;
                    cmd.Parameters.Add("@HFAccount_Own", SqlDbType.NVarChar, 50).Value = _sHFAccount_Own;
                    cmd.Parameters.Add("@HFAccount_Clients", SqlDbType.NVarChar, 50).Value = _sHFAccount_Clients;
                    cmd.Parameters.Add("@BestExecution", SqlDbType.Int).Value = _iBestExecution;
                    cmd.Parameters.Add("@PriceTable", SqlDbType.NVarChar, 50).Value = _sPriceTable;
                    cmd.Parameters.Add("@DepositoryTitle", SqlDbType.NVarChar, 80).Value = _sDepositoryTitle;
                    cmd.Parameters.Add("@Aktive", SqlDbType.Int).Value = _iAktive;

                    cmd.ExecuteNonQuery();
                    _iRecord_ID = Convert.ToInt32(outParam.Value);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return _iRecord_ID;
        }
        public int EditRecord()
        {
            try
            {
                conn.Open();
                using (cmd = new SqlCommand("EditServiceProvider", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@ProviderType", SqlDbType.Int).Value = _iProviderType;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 80).Value = _sTitle;
                    cmd.Parameters.Add("@Alias", SqlDbType.NVarChar, 10).Value = _sAlias;
                    cmd.Parameters.Add("@Seira", SqlDbType.NVarChar, 5).Value = _sSeira;
                    cmd.Parameters.Add("@VAT_FP", SqlDbType.Float).Value = _fltVAT_FP;
                    cmd.Parameters.Add("@VAT_NP", SqlDbType.Float).Value = _fltVAT_NP;
                    cmd.Parameters.Add("@MainCurr", SqlDbType.NVarChar, 6).Value = _sMainCurr;
                    cmd.Parameters.Add("@Informing_Statement", SqlDbType.Int).Value = _iInforming_Statement;
                    cmd.Parameters.Add("@Informing_Misc", SqlDbType.Int).Value = _iInforming_Misc;
                    cmd.Parameters.Add("@Informing_ConvertFile", SqlDbType.Int).Value = _iInforming_ConvertFile;
                    cmd.Parameters.Add("@SendOrders", SqlDbType.Int).Value = _iSendOrders;
                    cmd.Parameters.Add("@FeesMode", SqlDbType.Int).Value = _iFeesMode;
                    cmd.Parameters.Add("@EffectCode", SqlDbType.NVarChar, 20).Value = _sEffectCode;
                    cmd.Parameters.Add("@LEI", SqlDbType.NVarChar, 50).Value = _sLEI;
                    cmd.Parameters.Add("@FIX_DB", SqlDbType.NVarChar, 50).Value = _sFIX_DB;
                    cmd.Parameters.Add("@HFAccount_Own", SqlDbType.NVarChar, 50).Value = _sHFAccount_Own;
                    cmd.Parameters.Add("@HFAccount_Clients", SqlDbType.NVarChar, 50).Value = _sHFAccount_Clients;
                    cmd.Parameters.Add("@BestExecution", SqlDbType.Int).Value = _iBestExecution;
                    cmd.Parameters.Add("@PriceTable", SqlDbType.NVarChar, 50).Value = _sPriceTable;
                    cmd.Parameters.Add("@DepositoryTitle", SqlDbType.NVarChar, 80).Value = _sDepositoryTitle;
                    cmd.Parameters.Add("@Aktive", SqlDbType.Int).Value = _iAktive;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return _iRecord_ID;
        }
        public void DeleteRecord()
        {
            try
            {
                conn.Open();
                using (cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ServiceProviders";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return _iRecord_ID; } set { _iRecord_ID = value; } }
        public int ProviderType { get { return _iProviderType; } set { _iProviderType = value; } }
        public string Title { get { return _sTitle; } set { _sTitle = value; } }
        public string Alias { get { return _sAlias; } set { _sAlias = value; } }
        public string Seira { get { return _sSeira; } set { _sSeira = value; } }
        public float VAT_FP { get { return _fltVAT_FP; } set { _fltVAT_FP = value; } }
        public float VAT_NP { get { return _fltVAT_NP; } set { _fltVAT_NP = value; } }
        public string MainCurr { get { return _sMainCurr; } set { _sMainCurr = value; } }
        public int Informing_Statement { get { return _iInforming_Statement; } set { _iInforming_Statement = value; } }
        public int Informing_Misc { get { return _iInforming_Misc; } set { _iInforming_Misc = value; } }
        public int Informing_ConvertFile { get { return _iInforming_ConvertFile; } set { _iInforming_ConvertFile = value; } }
        public int SendOrders { get { return _iSendOrders; } set { _iSendOrders = value; } }
        public int FeesMode { get { return _iFeesMode; } set { _iFeesMode = value; } }
        public string EffectCode { get { return _sEffectCode; }  set { _sEffectCode = value; } }
        public string LEI { get { return _sLEI; } set { _sLEI = value; } }
        public string FIX_DB { get { return _sFIX_DB; } set { _sFIX_DB = value; } }
        public string HFAccount_Own { get { return _sHFAccount_Own; } set { _sHFAccount_Own = value; } }
        public string HFAccount_Clients { get { return _sHFAccount_Clients; } set { _sHFAccount_Clients = value; } }
        public int BestExecution { get { return _iBestExecution; } set { _iBestExecution = value; } }
        public string PriceTable { get { return _sPriceTable; } set { _sPriceTable = value; } }
        public string DepositoryTitle { get { return _sDepositoryTitle; } set { _sDepositoryTitle = value; } }
        public int Aktive { get { return _iAktive; } set { _iAktive = value; } }
        public int CustodyProvider_ID { get { return _iCustodyProvider_ID; } set { _iCustodyProvider_ID = value; } }
        public int Product_ID { get { return _iProduct_ID; } set { _iProduct_ID = value; } }
        public int ProductCategory_ID { get { return _iProductCategory_ID; } set { _iProductCategory_ID = value; } }        
        public DateTime AktionDate { get { return _dAktionDate; } set { _dAktionDate = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }

    }
}
