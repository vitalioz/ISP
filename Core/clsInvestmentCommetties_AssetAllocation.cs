using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsInvestmentCommetties_AssetAllocation
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataRow dtRow;

        private int      _iRecord_ID;
        private DateTime _dDateFrom;
        private DateTime _dDateTo;
        private int      _iTipos;
        private int      _iProfile_ID;

        private float    _fltFixedIncome;
        private float    _fltEquities;
        private float    _fltCash;
        private float    _fltEUR;
        private float    _fltUSD_etc;
        private float    _fltEmergingCurrencies;
        private float    _fltDevelopedMarkets;
        private float    _fltEmergingMarkets;

        private DateTime _dDateControl;
        private string _sProfile_Title;
        private DataTable _dtList;

        public clsInvestmentCommetties_AssetAllocation()
        {
            this._iRecord_ID = 0;
            this._dDateFrom = Convert.ToDateTime("1900/01/01");
            this._dDateTo = Convert.ToDateTime("2070/12/31");
            this._iTipos = 0;
            this._iProfile_ID = 0;
            this._fltFixedIncome = 0;
            this._fltEquities = 0;
            this._fltCash = 0;
            this._fltEUR = 0;
            this._fltUSD_etc = 0;
            this._fltEmergingCurrencies = 0;
            this._fltDevelopedMarkets = 0;
            this._fltEmergingMarkets = 0;
            this._dDateControl = Convert.ToDateTime("1900/01/01");
            this._sProfile_Title = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetInvestmentCommetties_AssetAllocation", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Tipos", "0"));
                cmd.Parameters.Add(new SqlParameter("@Profile_ID", "0"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._dDateFrom = Convert.ToDateTime(drList["DateFrom"]);
                    this._dDateTo = Convert.ToDateTime(drList["DateTo"]);
                    this._iTipos = Convert.ToInt32(drList["Tipos"]);
                    this._iProfile_ID = Convert.ToInt32(drList["Profile_ID"]);                   
                    this._fltFixedIncome = Convert.ToSingle(drList["FixedIncome"]);
                    this._fltEquities = Convert.ToSingle(drList["Equities"]);
                    this._fltCash = Convert.ToSingle(drList["Cash"]);
                    this._fltEUR = Convert.ToSingle(drList["EUR"]);
                    this._fltUSD_etc = Convert.ToSingle(drList["USD_etc"]);
                    this._fltEmergingCurrencies = Convert.ToSingle(drList["EmergingCurrencies"]);
                    this._fltDevelopedMarkets = Convert.ToSingle(drList["DevelopedMarkets"]);
                    this._fltEmergingMarkets = Convert.ToSingle(drList["EmergingMarkets"]);
                    this._sProfile_Title = drList["Profile_Title"] + "";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetRecord_Tipos_Profile()
        {
            this._iRecord_ID = 0;
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetInvestmentCommetties_AssetAllocation", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", "0"));
                cmd.Parameters.Add(new SqlParameter("@Tipos", _iTipos));
                cmd.Parameters.Add(new SqlParameter("@Profile_ID", _iProfile_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._dDateFrom = Convert.ToDateTime(drList["DateFrom"]);
                    this._dDateTo = Convert.ToDateTime(drList["DateTo"]);
                    this._iTipos = Convert.ToInt32(drList["Tipos"]);
                    this._iProfile_ID = Convert.ToInt32(drList["Profile_ID"]);
                    this._fltFixedIncome = Convert.ToSingle(drList["FixedIncome"]);
                    this._fltEquities = Convert.ToSingle(drList["Equities"]);
                    this._fltCash = Convert.ToSingle(drList["Cash"]);
                    this._fltEUR = Convert.ToSingle(drList["EUR"]);
                    this._fltUSD_etc = Convert.ToSingle(drList["USD_etc"]);
                    this._fltEmergingCurrencies = Convert.ToSingle(drList["EmergingCurrencies"]);
                    this._fltDevelopedMarkets = Convert.ToSingle(drList["DevelopedMarkets"]);
                    this._fltEmergingMarkets = Convert.ToSingle(drList["EmergingMarkets"]);
                    this._sProfile_Title = drList["Profile_Title"] + "";
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
            _dtList.Columns.Add("DateFrom", typeof(string));
            _dtList.Columns.Add("DateTo", typeof(string));
            _dtList.Columns.Add("Tipos", typeof(int));
            _dtList.Columns.Add("Profile_ID", typeof(int));
            _dtList.Columns.Add("Profile_Title", typeof(string));
            _dtList.Columns.Add("FixedIncome", typeof(float));
            _dtList.Columns.Add("Equities", typeof(float));
            _dtList.Columns.Add("Cash", typeof(float));
            _dtList.Columns.Add("EUR", typeof(float));
            _dtList.Columns.Add("USD_etc", typeof(float));
            _dtList.Columns.Add("EmergingCurrencies", typeof(float));
            _dtList.Columns.Add("DevelopedMarkets", typeof(float));
            _dtList.Columns.Add("EmergingMarkets", typeof(float));
            _dtList.Columns.Add("ID", typeof(int));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetInvestmentCommetties_AssetAllocation_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateControl", _dDateControl));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    i = i + 1;
                    dtRow = _dtList.NewRow();
                    dtRow["AA"] = i;
                    dtRow["DateFrom"] = Convert.ToDateTime(drList["DateFrom"]).ToString("dd/MM/yyyy");
                    dtRow["DateTo"] = Convert.ToDateTime(drList["DateTo"]).ToString("dd/MM/yyyy");
                    dtRow["Tipos"] = drList["Tipos"];
                    dtRow["Profile_ID"] = drList["Profile_ID"];
                    dtRow["Profile_Title"] = drList["Profile_Title"];
                    dtRow["FixedIncome"] = drList["FixedIncome"];
                    dtRow["Equities"] = drList["Equities"];
                    dtRow["Cash"] = drList["Cash"];
                    dtRow["EUR"] = drList["EUR"];
                    dtRow["USD_etc"] = drList["USD_etc"];
                    dtRow["EmergingCurrencies"] = drList["EmergingCurrencies"];
                    dtRow["DevelopedMarkets"] = drList["DevelopedMarkets"];
                    dtRow["EmergingMarkets"] = drList["EmergingMarkets"];
                    dtRow["ID"] = drList["ID"];
                    _dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetAssetAllocationRecs()
        {
            int i = 0;
            _dtList = new DataTable();
            _dtList.Columns.Add("AA", typeof(int));
            _dtList.Columns.Add("DateFrom", typeof(string));
            _dtList.Columns.Add("DateTo", typeof(string));
            _dtList.Columns.Add("Tipos_Title", typeof(string));
            _dtList.Columns.Add("Profile_Title", typeof(string));
            _dtList.Columns.Add("Title", typeof(string));
            _dtList.Columns.Add("MinValue", typeof(float));
            _dtList.Columns.Add("MainValue", typeof(float));
            _dtList.Columns.Add("MaxValue", typeof(float));
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("Tipos", typeof(int));
            _dtList.Columns.Add("Profile_ID", typeof(int));
            _dtList.Columns.Add("Recs_ID", typeof(int));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetInvestmentCommetties_AssetAllocationRecs", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateControl", _dDateControl));
                cmd.Parameters.Add(new SqlParameter("@Tipos", _iTipos));
                cmd.Parameters.Add(new SqlParameter("@Profile_ID", _iProfile_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    i = i + 1;
                    dtRow = _dtList.NewRow();
                    dtRow["AA"] = i;
                    dtRow["DateFrom"] = Convert.ToDateTime(drList["DateFrom"]).ToString("dd/MM/yyyy");
                    dtRow["DateTo"] = Convert.ToDateTime(drList["DateTo"]).ToString("dd/MM/yyyy");
                    switch (Convert.ToInt16(drList["Tipos"]))
                    {
                        case 1:
                            dtRow["Tipos_Title"] = "EUR Reference Ccy";
                            break;
                        case 2:
                            dtRow["Tipos_Title"] = "USD Reference Ccy";
                            break;
                        case 3:
                            dtRow["Tipos_Title"] = "Hellenic Portfolios";
                            break;
                    }
                    dtRow["Profile_Title"] = drList["Profile_Title"];
                    dtRow["Title"] = drList["Title"];
                    dtRow["MinValue"] = drList["MinValue"];
                    dtRow["MainValue"] = drList["MainValue"];
                    dtRow["MaxValue"] = drList["MaxValue"];
                    dtRow["ID"] = drList["ID"];
                    dtRow["Tipos"] = drList["Tipos"];
                    dtRow["Profile_ID"] = drList["Profile_ID"];
                    dtRow["Recs_ID"] = drList["Recs_ID"];
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
                using (SqlCommand cmd = new SqlCommand("InsertInvestmentCommetties_AssetAllocation", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = _dDateFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = _dDateTo;
                    cmd.Parameters.Add("@Tipos", SqlDbType.Int).Value = _iTipos;
                    cmd.Parameters.Add("@Profile_ID", SqlDbType.Int).Value = _iProfile_ID;
                    cmd.Parameters.Add("@FixedIncome", SqlDbType.Float).Value = _fltFixedIncome;
                    cmd.Parameters.Add("@Equities", SqlDbType.Float).Value = _fltEquities;
                    cmd.Parameters.Add("@Cash", SqlDbType.Float).Value = _fltCash;
                    cmd.Parameters.Add("@EUR", SqlDbType.Float).Value = _fltEUR;
                    cmd.Parameters.Add("@USD_etc", SqlDbType.Float).Value = _fltUSD_etc;
                    cmd.Parameters.Add("@EmergingCurrencies", SqlDbType.Float).Value = _fltEmergingCurrencies;
                    cmd.Parameters.Add("@DevelopedMarkets", SqlDbType.Float).Value = _fltDevelopedMarkets;
                    cmd.Parameters.Add("@EmergingMarkets", SqlDbType.Float).Value = _fltEmergingMarkets;
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
                using (SqlCommand cmd = new SqlCommand("EditInvestmentCommetties_AssetAllocation", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = _dDateFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = _dDateTo;
                    cmd.Parameters.Add("@Tipos", SqlDbType.Int).Value = _iTipos;
                    cmd.Parameters.Add("@Profile_ID", SqlDbType.Int).Value = _iProfile_ID;
                    cmd.Parameters.Add("@FixedIncome", SqlDbType.Float).Value = _fltFixedIncome;
                    cmd.Parameters.Add("@Equities", SqlDbType.Float).Value = _fltEquities;
                    cmd.Parameters.Add("@Cash", SqlDbType.Float).Value = _fltCash;
                    cmd.Parameters.Add("@EUR", SqlDbType.Float).Value = _fltEUR;
                    cmd.Parameters.Add("@USD_etc", SqlDbType.Float).Value = _fltUSD_etc;
                    cmd.Parameters.Add("@EmergingCurrencies", SqlDbType.Float).Value = _fltEmergingCurrencies;
                    cmd.Parameters.Add("@DevelopedMarkets", SqlDbType.Float).Value = _fltDevelopedMarkets;
                    cmd.Parameters.Add("@EmergingMarkets", SqlDbType.Float).Value = _fltEmergingMarkets;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "InvestmentCommetties_AssetAllocation";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public DateTime DateFrom { get { return _dDateFrom; } set { _dDateFrom = value; } }
        public DateTime DateTo { get { return _dDateTo; } set { _dDateTo = value; } }
        public int Tipos { get { return _iTipos; } set { _iTipos = value; } }
        public int Profile_ID { get { return _iProfile_ID; } set { _iProfile_ID = value; } }
        public string Profile_Title { get { return _sProfile_Title; } set { _sProfile_Title = value; } }
        public float FixedIncome { get { return _fltFixedIncome; } set { _fltFixedIncome = value; } }
        public float Equities { get { return _fltEquities; } set { _fltEquities = value; } }
        public float Cash { get { return _fltCash; } set { _fltCash = value; } }
        public float EUR { get { return _fltEUR; } set { _fltEUR = value; } }
        public float USD_etc { get { return _fltUSD_etc; } set { _fltUSD_etc = value; } }
        public float EmergingCurrencies { get { return _fltEmergingCurrencies; } set { _fltEmergingCurrencies = value; } }
        public float DevelopedMarkets { get { return _fltDevelopedMarkets; } set { _fltDevelopedMarkets = value; } }
        public float EmergingMarkets { get { return _fltEmergingMarkets; } set { _fltEmergingMarkets = value; } }
        public DateTime DateControl { get { return _dDateControl; } set { _dDateControl = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }

    }
}
