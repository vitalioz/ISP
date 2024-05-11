using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsServiceProvidersOptions
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int      _iRecord_ID;
        private int      _iServiceProvider_ID;
        private int      _iServiceType_ID;
        private string   _sTitle;
        private DateTime _dDateStart;
        private DateTime _dDateFinish;
        private float    _fltMonthMinAmount;
        private string   _sMonthMinCurr;
        private float    _fltOpenAmount;
        private string   _sOpenCurr;
        private float    _fltServiceAmount;
        private string   _sServiceCurr;
        private float    _fltMinAmount;
        private string   _sMinCurr;
        private int      _iCalcAUM;
        private int      _iCalcSecurities;
        private int      _iCalcCash;

        private DataTable _dtList;

        public clsServiceProvidersOptions()
        {
            this._iRecord_ID = 0;
            this._iServiceProvider_ID = 0;
            this._iServiceType_ID = 0;
            this._sTitle = "";
            this._dDateStart = Convert.ToDateTime("1900/01/01");
            this._dDateFinish = Convert.ToDateTime("1900/01/01");
            this._fltMonthMinAmount = 0;
            this._sMonthMinCurr = "";
            this._fltOpenAmount = 0;
            this._sOpenCurr = "";
            this._fltServiceAmount = 0;
            this._sServiceCurr = "";
            this._fltMinAmount = 0;
            this._sMinCurr = "";
            this._iCalcAUM = 0;
            this._iCalcSecurities = 0;
            this._iCalcCash = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ServiceProviderOptions"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iServiceProvider_ID = Convert.ToInt32(drList["ServiceProvider_ID"]);
                    this._iServiceType_ID = Convert.ToInt32(drList["ServiceType_ID"]);
                    this._sTitle = drList["Title"].ToString();
                    this._dDateStart = Convert.ToDateTime(drList["DateStart"]);
                    this._dDateFinish = Convert.ToDateTime(drList["DateFinish"]);
                    this._fltMonthMinAmount = Convert.ToSingle(drList["MonthMinAmount"]);
                    this._sMonthMinCurr = drList["MonthMinCurr"].ToString();
                    this._fltOpenAmount = Convert.ToSingle(drList["OpenAmount"]);
                    this._sOpenCurr = drList["OpenCurr"].ToString();
                    this._fltServiceAmount = Convert.ToSingle(drList["ServiceAmount"]);
                    this._sServiceCurr = drList["ServiceCurr"].ToString();
                    this._fltMinAmount = Convert.ToSingle(drList["MinAmount"]);
                    this._sMinCurr = drList["MinCurr"].ToString();
                    this._iCalcAUM = Convert.ToInt32(drList["CalcAUM"]);
                    this._iCalcSecurities = Convert.ToInt32(drList["CalcSecurities"]);
                    this._iCalcCash = Convert.ToInt32(drList["CalcCash"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("ServiceProvider_ID", typeof(int));
            _dtList.Columns.Add("ServiceType_ID", typeof(int));
            _dtList.Columns.Add("Title", typeof(string));
            _dtList.Columns.Add("DateStart", typeof(string));
            _dtList.Columns.Add("DateFinish", typeof(string));
            _dtList.Columns.Add("MonthMinAmount", typeof(float));
            _dtList.Columns.Add("MonthMinCurr", typeof(string));
            _dtList.Columns.Add("OpenAmount", typeof(float));
            _dtList.Columns.Add("OpenCurr", typeof(string));
            _dtList.Columns.Add("ServiceAmount", typeof(float));
            _dtList.Columns.Add("ServiceCurr", typeof(string));
            _dtList.Columns.Add("MinAmount", typeof(float));
            _dtList.Columns.Add("MinCurr", typeof(string));
            _dtList.Columns.Add("CalcAUM", typeof(int));
            _dtList.Columns.Add("CalcSecurities", typeof(int));
            _dtList.Columns.Add("CalcCash", typeof(int));
            _dtList.Columns.Add("Status", typeof(int));
            _dtList.Columns.Add("Pseudo_ID", typeof(int));

            _dtList.Rows.Add(0, 0, 0, "", "", "", 0, "", 0, "", 0, "", 0, "", 0, 0, 0, 0);
            try
            {
                conn.Open();
                cmd = new SqlCommand("sp_GetServiceProviderOptions", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ServiceProvider_ID", _iServiceProvider_ID));
                cmd.Parameters.Add(new SqlParameter("@ServiceType_ID", _iServiceType_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["ServiceProvider_ID"], drList["ServiceType_ID"], drList["Title"], drList["DateStart"], drList["DateFinish"],
                        drList["MonthMinAmount"], drList["MonthMinCurr"], drList["OpenAmount"], drList["OpenCurr"], drList["ServiceAmount"], drList["ServiceCurr"],
                        drList["MinAmount"], drList["MinCurr"], drList["CalcAUM"], drList["CalcSecurities"], drList["CalcCash"], 0, drList["ID"]);                  // ID -> Pseudo_ID
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
                using (cmd = new SqlCommand("sp_InsertServiceProviderOption", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ServiceProvider_ID", SqlDbType.Int).Value = _iServiceProvider_ID;
                    cmd.Parameters.Add("@ServiceType_ID", SqlDbType.Int).Value = _iServiceType_ID;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 100).Value = _sTitle;
                    cmd.Parameters.Add("@DateStart", SqlDbType.DateTime).Value = _dDateStart;
                    cmd.Parameters.Add("@DateFinish", SqlDbType.DateTime).Value = _dDateFinish;
                    cmd.Parameters.Add("@MonthMinAmount", SqlDbType.Float).Value = _fltMonthMinAmount;
                    cmd.Parameters.Add("@MonthMinCurr", SqlDbType.NVarChar, 6).Value = _sMonthMinCurr;
                    cmd.Parameters.Add("@OpenAmount", SqlDbType.Float).Value = _fltOpenAmount;
                    cmd.Parameters.Add("@OpenCurr", SqlDbType.NVarChar, 6).Value = _sOpenCurr;
                    cmd.Parameters.Add("@ServiceAmount", SqlDbType.Float).Value = _fltServiceAmount;
                    cmd.Parameters.Add("@ServiceCurr", SqlDbType.NVarChar, 6).Value = _sServiceCurr;
                    cmd.Parameters.Add("@MinAmount", SqlDbType.Float).Value = _fltMinAmount;
                    cmd.Parameters.Add("@MinCurr", SqlDbType.NVarChar, 6).Value = _sMinCurr;
                    cmd.Parameters.Add("@CalcAUM", SqlDbType.Int).Value = _iCalcAUM;
                    cmd.Parameters.Add("@CalcSecurities", SqlDbType.Int).Value = _iCalcSecurities;
                    cmd.Parameters.Add("@CalcCash", SqlDbType.Int).Value = _iCalcCash;

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
                using (cmd = new SqlCommand("sp_EditServiceProviderOption", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@ServiceProvider_ID", SqlDbType.Int).Value = _iServiceProvider_ID;
                    cmd.Parameters.Add("@ServiceType_ID", SqlDbType.Int).Value = _iServiceType_ID;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 100).Value = _sTitle;
                    cmd.Parameters.Add("@DateStart", SqlDbType.DateTime).Value = _dDateStart;
                    cmd.Parameters.Add("@DateFinish", SqlDbType.DateTime).Value = _dDateFinish;
                    cmd.Parameters.Add("@MonthMinAmount", SqlDbType.Float).Value = _fltMonthMinAmount;
                    cmd.Parameters.Add("@MonthMinCurr", SqlDbType.NVarChar, 6).Value = _sMonthMinCurr;
                    cmd.Parameters.Add("@OpenAmount", SqlDbType.Float).Value = _fltOpenAmount;
                    cmd.Parameters.Add("@OpenCurr", SqlDbType.NVarChar, 6).Value = _sOpenCurr;
                    cmd.Parameters.Add("@ServiceAmount", SqlDbType.Float).Value = _fltServiceAmount;
                    cmd.Parameters.Add("@ServiceCurr", SqlDbType.NVarChar, 6).Value = _sServiceCurr;
                    cmd.Parameters.Add("@MinAmount", SqlDbType.Float).Value = _fltMinAmount;
                    cmd.Parameters.Add("@MinCurr", SqlDbType.NVarChar, 6).Value = _sMinCurr;
                    cmd.Parameters.Add("@CalcAUM", SqlDbType.Int).Value = _iCalcAUM;
                    cmd.Parameters.Add("@CalcSecurities", SqlDbType.Int).Value = _iCalcSecurities;
                    cmd.Parameters.Add("@CalcCash", SqlDbType.Int).Value = _iCalcCash;

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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ServiceProviderOptions";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return _iRecord_ID; } set { _iRecord_ID = value; } }
        public int ServiceProvider_ID { get { return _iServiceProvider_ID; } set { _iServiceProvider_ID = value; } }
        public int ServiceType_ID { get { return _iServiceType_ID; } set { _iServiceType_ID = value; } }
        public string Title { get { return _sTitle; } set { _sTitle = value; } }
        public DateTime DateStart { get { return _dDateStart; } set { _dDateStart = value; } }
        public DateTime DateFinish { get { return _dDateFinish; } set { _dDateFinish = value; } }
        public float MonthMinAmount { get { return _fltMonthMinAmount; } set { _fltMonthMinAmount = value; } }
        public string MonthMinCurr { get { return _sMonthMinCurr; } set { _sMonthMinCurr = value; } }
        public float OpenAmount { get { return _fltOpenAmount; } set { _fltOpenAmount = value; } }
        public string OpenCurr { get { return _sOpenCurr; } set { _sOpenCurr = value; } }
        public float ServiceAmount { get { return _fltServiceAmount; } set { _fltServiceAmount = value; } }
        public string ServiceCurr { get { return _sServiceCurr; } set { _sServiceCurr = value; } }
        public float MinAmount { get { return _fltMinAmount; } set { _fltMinAmount = value; } }
        public string MinCurr { get { return _sMinCurr; } set { _sMinCurr = value; } }
        public int CalcAUM { get { return _iCalcAUM; } set { _iCalcAUM = value; } }
        public int CalcSecurities { get { return _iCalcSecurities; } set { _iCalcSecurities = value; } }
        public int CalcCash {get { return _iCalcCash; } set { _iCalcCash = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }

    }
}
