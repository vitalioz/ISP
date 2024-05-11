using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsContracts_Balances
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataRow dtRow;

        private int _iRecord_ID;
        private DateTime _dDateIns;
        private int _iContract_ID;
        private int _iContract_Details_ID;
        private int _iContract_Packages_ID;
        private decimal _decTotalSecurutiesValue;
        private decimal _decTotalCashValue;
        private decimal _decTotalValue;
        private float  _fltHF_FixedIncome;
        private float  _fltHF_Equities;
        private float  _fltHF_Cash;
        private float  _fltHF_EUR;
        private float  _fltHF_USD_etc;
        private float  _fltHF_EmergingCurrencies;
        private float _fltHF_DevelopedMarkets;
        private float  _fltHF_EmergingMarkets;
        private float  _fltFixedIncome;
        private float  _fltEquities;
        private float  _fltCash;
        private float  _fltEUR;
        private float  _fltUSD_etc;
        private float  _fltEmergingCurrencies;
        private float  _fltDevelopedMarkets;
        private float  _fltEmergingMarkets;
        private string _sCustodian;
        private int    _iXAA;
        private string _sNotes;
        private string _sSpecialInstructions;
        private string _sComplexSigns;

        private DateTime _dDateFrom;
        private DateTime _dDateTo;
        private string _sCode;
        private string _sPortfolio;
        private string _sContractTitle;
        private string _sCurrency;
        private string _sProfile_Title;
        private DataTable _dtList;

        public clsContracts_Balances()
        {
            this._iRecord_ID = 0;
            this._dDateIns = Convert.ToDateTime("1900/01/01");
            this._iContract_ID = 0;
            this._iContract_Details_ID = 0;
            this._iContract_Packages_ID = 0;
            this._decTotalSecurutiesValue = 0;
            this._decTotalCashValue = 0;
            this._decTotalValue = 0;
            this._fltHF_FixedIncome = 0;
            this._fltHF_Equities = 0;
            this._fltHF_Cash = 0;
            this._fltHF_EUR = 0;
            this._fltHF_USD_etc = 0;
            this._fltHF_EmergingCurrencies = 0;
            this._fltHF_DevelopedMarkets = 0;
            this._fltHF_EmergingMarkets = 0;
            this._fltFixedIncome = 0;
            this._fltEquities = 0;
            this._fltCash = 0;
            this._fltEUR = 0;
            this._fltUSD_etc = 0;
            this._fltEmergingCurrencies = 0;
            this._fltDevelopedMarkets = 0;
            this._fltEmergingMarkets = 0;
            this._sCustodian = "";
            this._iXAA = 0;
            this._sNotes = "";
            this._sSpecialInstructions = "";
            this._sComplexSigns = "";

            this._dDateFrom = Convert.ToDateTime("1900/01/01");
            this._dDateTo = Convert.ToDateTime("2070/12/31");
            this._sCode = "";
            this._sPortfolio = "";
            this._sContractTitle = "";
            this._sCurrency = "";
            this._sProfile_Title = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetContracts_Balances", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._dDateIns = Convert.ToDateTime(drList["DateIns"]);
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iContract_Details_ID = Convert.ToInt32(drList["Contract_Details_ID"]);
                    this._iContract_Packages_ID = Convert.ToInt32(drList["Contract_Packages_ID"]);
                    this._sCode = drList["Code"] + "";
                    this._sPortfolio = drList["Portfolio"] + "";
                    this._sContractTitle = drList["ContractTitle"] + "";
                    this._sCurrency = drList["Currency"] + "";
                    this._sProfile_Title = drList["Profile_Title"] + "";
                    this._decTotalSecurutiesValue = Convert.ToDecimal(drList["TotalSecurutiesValue"]);
                    this._decTotalCashValue = Convert.ToDecimal(drList["TotalCashValue"]);
                    this._decTotalValue = Convert.ToDecimal(drList["TotalValue"]);
                    this._fltHF_FixedIncome = Convert.ToSingle(drList["HF_FixedIncome"]);
                    this._fltHF_Equities = Convert.ToSingle(drList["HF_Equities"]);
                    this._fltHF_Cash = Convert.ToSingle(drList["HF_Cash"]);
                    this._fltHF_EUR = Convert.ToSingle(drList["HF_EUR"]);
                    this._fltHF_USD_etc = Convert.ToSingle(drList["HF_USD_etc"]);
                    this._fltHF_EmergingCurrencies = Convert.ToSingle(drList["HF_EmergingCurrencies"]);
                    this._fltHF_DevelopedMarkets = Convert.ToSingle(drList["HF_DevelopedMarkets"]);
                    this._fltHF_EmergingMarkets = Convert.ToSingle(drList["HF_EmergingMarkets"]);
                    this._fltFixedIncome = Convert.ToSingle(drList["FixedIncome"]);
                    this._fltEquities = Convert.ToSingle(drList["Equities"]);
                    this._fltCash = Convert.ToSingle(drList["Cash"]);
                    this._fltEUR = Convert.ToSingle(drList["EUR"]);
                    this._fltUSD_etc = Convert.ToSingle(drList["USD_etc"]);
                    this._fltEmergingCurrencies = Convert.ToSingle(drList["EmergingCurrencies"]);
                    this._fltDevelopedMarkets = Convert.ToSingle(drList["DevelopedMarkets"]);
                    this._fltEmergingMarkets = Convert.ToSingle(drList["EmergingMarkets"]);
                    this._sCustodian = "";
                    this._iXAA = Convert.ToInt32(drList["XAA"]);
                    this._sNotes = drList["Notes"] + "";
                    this._sSpecialInstructions = "";
                    this._sComplexSigns = ""; ;                    
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            int i = 0;
            _dtList = new DataTable();
            _dtList.Columns.Add("AA", typeof(int));
            _dtList.Columns.Add("DateIns", typeof(string));
            _dtList.Columns.Add("Code", typeof(string));
            _dtList.Columns.Add("Portfolio", typeof(string));
            _dtList.Columns.Add("ContractTitle", typeof(string));
            _dtList.Columns.Add("Profile_Title", typeof(string));
            _dtList.Columns.Add("Currency", typeof(string));
            _dtList.Columns.Add("TotalSecurutiesValue", typeof(decimal));
            _dtList.Columns.Add("TotalCashValue", typeof(decimal));
            _dtList.Columns.Add("TotalValue", typeof(decimal));
            _dtList.Columns.Add("HF_FixedIncome", typeof(float));
            _dtList.Columns.Add("HF_Equities", typeof(float));
            _dtList.Columns.Add("HF_Cash", typeof(float));
            _dtList.Columns.Add("HF_EUR", typeof(float));
            _dtList.Columns.Add("HF_USD_etc", typeof(float));
            _dtList.Columns.Add("HF_EmergingCurrencies", typeof(float));
            _dtList.Columns.Add("HF_DevelopedMarkets", typeof(float));
            _dtList.Columns.Add("HF_EmergingMarkets", typeof(float));
            _dtList.Columns.Add("FixedIncome", typeof(float));
            _dtList.Columns.Add("Equities", typeof(float));
            _dtList.Columns.Add("Cash", typeof(float));
            _dtList.Columns.Add("EUR", typeof(float));
            _dtList.Columns.Add("USD_etc", typeof(float));
            _dtList.Columns.Add("EmergingCurrencies", typeof(float));
            _dtList.Columns.Add("DevelopedMarkets", typeof(float));
            _dtList.Columns.Add("EmergingMarkets", typeof(float));
            _dtList.Columns.Add("Custodian", typeof(string));
            _dtList.Columns.Add("MiFID_2", typeof(string));
            _dtList.Columns.Add("XAA", typeof(string));
            _dtList.Columns.Add("Notes", typeof(string));
            _dtList.Columns.Add("SpecialInstructions", typeof(string));
            _dtList.Columns.Add("ComplexSigns", typeof(string));
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("Contract_ID", typeof(int));
            _dtList.Columns.Add("Profile_ID", typeof(int));
            _dtList.Columns.Add("Client_ID", typeof(int));
            _dtList.Columns.Add("Tipos", typeof(int));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetContracts_Balances_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateIns", _dDateIns));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    i = i + 1;
                    dtRow = _dtList.NewRow();
                    dtRow["AA"] = i;
                    dtRow["DateIns"] = Convert.ToDateTime(drList["DateIns"]).ToString("dd/MM/yyyy");
                    dtRow["Code"] = drList["Code"];
                    dtRow["Portfolio"] = drList["Portfolio"];
                    dtRow["ContractTitle"] = drList["ContractTitle"];
                    dtRow["Profile_Title"] = drList["Profile_Title"];
                    dtRow["Currency"] = drList["Currency"];
                    dtRow["TotalSecurutiesValue"] = drList["TotalSecurutiesValue"];
                    dtRow["TotalCashValue"] = drList["TotalCashValue"];
                    dtRow["TotalValue"] = drList["TotalValue"];
                    dtRow["HF_FixedIncome"] = drList["HF_FixedIncome"];
                    dtRow["HF_Equities"] = drList["HF_Equities"];
                    dtRow["HF_Cash"] = drList["HF_Cash"];
                    dtRow["HF_EUR"] = drList["HF_EUR"];
                    dtRow["HF_USD_etc"] = drList["HF_USD_etc"];
                    dtRow["HF_EmergingCurrencies"] = drList["HF_EmergingCurrencies"];
                    dtRow["HF_DevelopedMarkets"] = drList["HF_DevelopedMarkets"];
                    dtRow["HF_EmergingMarkets"] = drList["HF_EmergingMarkets"];
                    dtRow["FixedIncome"] = drList["FixedIncome"];
                    dtRow["Equities"] = drList["Equities"];
                    dtRow["Cash"] = drList["Cash"];
                    dtRow["EUR"] = drList["EUR"];
                    dtRow["USD_etc"] = drList["USD_etc"];
                    dtRow["EmergingCurrencies"] = drList["EmergingCurrencies"];
                    dtRow["DevelopedMarkets"] = drList["DevelopedMarkets"];
                    dtRow["EmergingMarkets"] = drList["EmergingMarkets"];
                    dtRow["Custodian"] = drList["Custodian"];
                    dtRow["Notes"] = drList["Notes"];
                    dtRow["SpecialInstructions"] = drList["SpecialInstructions"];
                    dtRow["MiFID_2"] = Convert.ToInt32(drList["MIFID_2"]) == 1 ? "Yes": "";
                    dtRow["XAA"] = Convert.ToInt32(drList["XAA"]) == 1 ? "Yes" : "";
                    dtRow["ComplexSigns"] = drList["ComplexSigns"];
                    dtRow["ID"] = drList["ID"];
                    dtRow["Contract_ID"] = drList["Contract_ID"];
                    dtRow["Profile_ID"] = drList["Profile_ID"];
                    dtRow["Client_ID"] = drList["Client_ID"];
                    dtRow["Tipos"] = drList["Tipos"];
                    _dtList.Rows.Add(dtRow);
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
                using (SqlCommand cmd = new SqlCommand("InsertContracts_Balances", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dDateIns;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Details_ID", SqlDbType.Int).Value = _iContract_Details_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@TotalSecurutiesValue", SqlDbType.Decimal).Value = _decTotalSecurutiesValue;
                    cmd.Parameters.Add("@TotalCashValue", SqlDbType.Decimal).Value = _decTotalCashValue;
                    cmd.Parameters.Add("@TotalValue", SqlDbType.Decimal).Value = _decTotalValue;
                    cmd.Parameters.Add("@HF_FixedIncome", SqlDbType.Float).Value = _fltHF_FixedIncome;
                    cmd.Parameters.Add("@HF_Equities", SqlDbType.Float).Value = _fltHF_Equities;
                    cmd.Parameters.Add("@HF_Cash", SqlDbType.Float).Value = _fltHF_Cash;
                    cmd.Parameters.Add("@HF_EUR", SqlDbType.Float).Value = _fltHF_EUR;
                    cmd.Parameters.Add("@HF_USD_etc", SqlDbType.Float).Value = _fltHF_USD_etc;
                    cmd.Parameters.Add("@HF_EmergingCurrencies", SqlDbType.Float).Value = _fltHF_EmergingCurrencies;
                    cmd.Parameters.Add("@HF_DevelopedMarkets", SqlDbType.Float).Value = _fltHF_DevelopedMarkets;
                    cmd.Parameters.Add("@HF_EmergingMarkets", SqlDbType.Float).Value = _fltHF_EmergingMarkets;
                    cmd.Parameters.Add("@FixedIncome", SqlDbType.Float).Value = _fltFixedIncome;
                    cmd.Parameters.Add("@Equities", SqlDbType.Float).Value = _fltEquities;
                    cmd.Parameters.Add("@Cash", SqlDbType.Float).Value = _fltCash;
                    cmd.Parameters.Add("@EUR", SqlDbType.Float).Value = _fltEUR;
                    cmd.Parameters.Add("@USD_etc", SqlDbType.Float).Value = _fltUSD_etc;
                    cmd.Parameters.Add("@EmergingCurrencies", SqlDbType.Float).Value = _fltEmergingCurrencies;
                    cmd.Parameters.Add("@DevelopedMarkets", SqlDbType.Float).Value = _fltDevelopedMarkets;
                    cmd.Parameters.Add("@EmergingMarkets", SqlDbType.Float).Value = _fltEmergingMarkets;
                    cmd.Parameters.Add("@Custodian", SqlDbType.NVarChar, 100).Value = _sCustodian;
                    cmd.Parameters.Add("@XAA", SqlDbType.Int).Value = _iXAA;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 1000).Value = _sNotes;
                    cmd.Parameters.Add("@SpecialInstructions", SqlDbType.NVarChar, 1000).Value = _sSpecialInstructions;
                    cmd.Parameters.Add("@ComplexSigns", SqlDbType.NVarChar, 1000).Value = _sComplexSigns;
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
                using (SqlCommand cmd = new SqlCommand("EditContracts_Balances", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dDateIns;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Details_ID", SqlDbType.Int).Value = _iContract_Details_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@TotalSecurutiesValue", SqlDbType.Decimal).Value = _decTotalSecurutiesValue;
                    cmd.Parameters.Add("@TotalCashValue", SqlDbType.Decimal).Value = _decTotalCashValue;
                    cmd.Parameters.Add("@TotalValue", SqlDbType.Decimal).Value = _decTotalValue;
                    cmd.Parameters.Add("@HF_FixedIncome", SqlDbType.Float).Value = _fltHF_FixedIncome;
                    cmd.Parameters.Add("@HF_Equities", SqlDbType.Float).Value = _fltHF_Equities;
                    cmd.Parameters.Add("@HF_Cash", SqlDbType.Float).Value = _fltHF_Cash;
                    cmd.Parameters.Add("@HF_EUR", SqlDbType.Float).Value = _fltHF_EUR;
                    cmd.Parameters.Add("@HF_USD_etc", SqlDbType.Float).Value = _fltHF_USD_etc;
                    cmd.Parameters.Add("@HF_EmergingCurrencies", SqlDbType.Float).Value = _fltHF_EmergingCurrencies;
                    cmd.Parameters.Add("@HF_DevelopedMarkets", SqlDbType.Float).Value = _fltHF_DevelopedMarkets;
                    cmd.Parameters.Add("@HF_EmergingMarkets", SqlDbType.Float).Value = _fltHF_EmergingMarkets;
                    cmd.Parameters.Add("@FixedIncome", SqlDbType.Float).Value = _fltFixedIncome;
                    cmd.Parameters.Add("@Equities", SqlDbType.Float).Value = _fltEquities;
                    cmd.Parameters.Add("@Cash", SqlDbType.Float).Value = _fltCash;
                    cmd.Parameters.Add("@EUR", SqlDbType.Float).Value = _fltEUR;
                    cmd.Parameters.Add("@USD_etc", SqlDbType.Float).Value = _fltUSD_etc;
                    cmd.Parameters.Add("@EmergingCurrencies", SqlDbType.Float).Value = _fltEmergingCurrencies;
                    cmd.Parameters.Add("@DevelopedMarkets", SqlDbType.Float).Value = _fltDevelopedMarkets;
                    cmd.Parameters.Add("@EmergingMarkets", SqlDbType.Float).Value = _fltEmergingMarkets;
                    cmd.Parameters.Add("@Custodian", SqlDbType.NVarChar, 100).Value = _sCustodian;
                    cmd.Parameters.Add("@XAA", SqlDbType.Int).Value = _iXAA;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 1000).Value = _sNotes;
                    cmd.Parameters.Add("@SpecialInstructions", SqlDbType.NVarChar, 1000).Value = _sSpecialInstructions;
                    cmd.Parameters.Add("@ComplexSigns", SqlDbType.NVarChar, 1000).Value = _sComplexSigns;
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
                using (SqlCommand cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Contracts_Balances";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public DateTime DateIns { get { return _dDateIns; } set { _dDateIns = value; } }
        public int Contract_ID { get { return _iContract_ID; } set { _iContract_ID = value; } }
        public int Contract_Details_ID { get { return _iContract_Details_ID; } set { _iContract_Details_ID = value; } }
        public int Contract_Packages_ID { get { return _iContract_Packages_ID; } set { _iContract_Packages_ID = value; } }
        public string Code { get { return _sCode; } set { _sCode = value; } }
        public string Portfolio { get { return _sPortfolio; } set { _sPortfolio = value; } }
        public string ContractTitle { get { return _sContractTitle; } set { _sContractTitle = value; } }
        public string Currency { get { return _sCurrency; } set { _sCurrency = value; } }
        public string Profile_Title { get { return _sProfile_Title; } set { _sProfile_Title = value; } }
        public decimal TotalSecurutiesValue { get { return _decTotalSecurutiesValue; } set { _decTotalSecurutiesValue = value; } }
        public decimal TotalCashValue { get { return _decTotalCashValue; } set { _decTotalCashValue = value; } }
        public decimal TotalValue { get { return _decTotalValue; } set { _decTotalValue = value; } }
        public float HF_FixedIncome { get { return _fltHF_FixedIncome; } set { _fltHF_FixedIncome = value; } }
        public float HF_Equities { get { return _fltHF_Equities; } set { _fltHF_Equities = value; } }
        public float HF_Cash { get { return _fltHF_Cash; } set { _fltHF_Cash = value; } }
        public float HF_EUR { get { return _fltHF_EUR; } set { _fltHF_EUR = value; } }
        public float HF_USD_etc { get { return _fltHF_USD_etc; } set { _fltHF_USD_etc = value; } }
        public float HF_EmergingCurrencies { get { return _fltHF_EmergingCurrencies; } set { _fltHF_EmergingCurrencies = value; } }
        public float HF_DevelopedMarkets { get { return _fltHF_DevelopedMarkets; } set { _fltHF_DevelopedMarkets = value; } }
        public float HF_EmergingMarkets { get { return _fltHF_EmergingMarkets; } set { _fltHF_EmergingMarkets = value; } }
        public float FixedIncome { get { return _fltFixedIncome; } set { _fltFixedIncome = value; } }
        public float Equities { get { return _fltEquities; } set { _fltEquities = value; } }
        public float Cash { get { return _fltCash; } set { _fltCash = value; } }
        public float EUR { get { return _fltEUR; } set { _fltEUR = value; } }
        public float USD_etc { get { return _fltUSD_etc; } set { _fltUSD_etc = value; } }
        public float EmergingCurrencies { get { return _fltEmergingCurrencies; } set { _fltEmergingCurrencies = value; } }
        public float DevelopedMarkets { get { return _fltDevelopedMarkets; } set { _fltDevelopedMarkets = value; } }
        public float EmergingMarkets { get { return _fltEmergingMarkets; } set { _fltEmergingMarkets = value; } }
        public string Custodian { get { return _sCustodian; } set { _sCustodian = value; } }
        public int XAA { get { return _iXAA; } set { _iXAA = value; } }
        public string Notes { get { return _sNotes; } set { _sNotes = value; } }
        public string SpecialInstructions { get { return _sSpecialInstructions; } set { _sSpecialInstructions = value; } }
        public string ComplexSigns { get { return _sComplexSigns; } set { _sComplexSigns = value; } }
        public DateTime DateFrom { get { return _dDateFrom; } set { _dDateFrom = value; } }
        public DateTime DateTo { get { return _dDateTo; } set { _dDateTo = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }

    }
}
