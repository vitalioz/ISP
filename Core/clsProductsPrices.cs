using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsProductsPrices
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlDataReader drList = null;

        private int      _iRecord_ID;
        private int      _iShareType;
        private DateTime _dDateIns;
        private int      _iShareCodes_ID;
        private string   _sCode;
        private float    _fltOpen;
        private float    _fltHigh;
        private float    _fltLow;
        private float    _fltClose;        
        private float    _fltLast;
        private float    _fltVolume;

        private DateTime _dDateFrom;
        private DateTime _dDateTo;
        private int      _iProductType_ID;
        private int      _iProductCategory_ID;
        private int      _iProduct_ID;
        private string   _sFilter;
        private DataTable _dtList;

        public clsProductsPrices()
        {
            this._iRecord_ID = 0;
            this._iShareType = 0;
            this._dDateIns = Convert.ToDateTime("1900/01/01");
            this._iShareCodes_ID = 0;
            this._sCode = "";
            this._fltOpen = 0;
            this._fltHigh = 0;
            this._fltLow = 0;
            this._fltClose = 0;            
            this._fltLast = 0;
            this._fltVolume = 0;
            this._dDateFrom = Convert.ToDateTime("1900/01/01");
            this._dDateTo = Convert.ToDateTime("1900/01/01");
            this._iProductType_ID = 0;
            this._iProductCategory_ID = 0;
            this._iProduct_ID = 0;
            this._sFilter = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "SharePrices"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iShareType = Convert.ToInt32(drList["ShareType"]);
                    this._dDateIns = Convert.ToDateTime(drList["DateIns"]);
                    this._iShareCodes_ID = Convert.ToInt32(drList["Share_ID"]);
                    this._sCode = drList["Code"] + "";
                    this._fltOpen = Convert.ToSingle(drList["Open"]);
                    this._fltHigh = Convert.ToSingle(drList["High"]);
                    this._fltLow = Convert.ToSingle(drList["Low"]);
                    this._fltClose = Convert.ToSingle(drList["Close"]);
                    this._fltLast = Convert.ToSingle(drList["Last"]);
                    this._fltVolume = Convert.ToSingle(drList["Volume"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            int iOldShareID = -999;
            DateTime dOldDateIns = Convert.ToDateTime("1900/01/01");

            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("DateIns", typeof(DateTime));
            _dtList.Columns.Add("ShareType", typeof(int));
            _dtList.Columns.Add("ShareCodes_ID", typeof(int));
            _dtList.Columns.Add("Product_Title", typeof(string));
            _dtList.Columns.Add("ProductCategory_Title", typeof(string));
            _dtList.Columns.Add("Code", typeof(string));
            _dtList.Columns.Add("Code2", typeof(string));
            _dtList.Columns.Add("ISIN", typeof(string));
            _dtList.Columns.Add("Title", typeof(string));
            _dtList.Columns.Add("Curr", typeof(string));
            _dtList.Columns.Add("Open", typeof(float));
            _dtList.Columns.Add("High", typeof(float));
            _dtList.Columns.Add("Low", typeof(float));
            _dtList.Columns.Add("Close", typeof(float));
            _dtList.Columns.Add("Last", typeof(float));
            _dtList.Columns.Add("Volume", typeof(float));
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetSharePricesList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dDateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dDateTo));
                cmd.Parameters.Add(new SqlParameter("@ProductType_ID", _iProductType_ID));
                cmd.Parameters.Add(new SqlParameter("@ProductCategory_ID", _iProductCategory_ID));
                cmd.Parameters.Add(new SqlParameter("@Product_ID", _iProduct_ID));
                cmd.Parameters.Add(new SqlParameter("@Filter", "%" + _sFilter + "%"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if ((Convert.ToInt32(drList["ShareCodes_ID"]) != iOldShareID) || (Convert.ToDateTime(drList["DateIns"]) != dOldDateIns)) {
                        iOldShareID = Convert.ToInt32(drList["ShareCodes_ID"]);
                        dOldDateIns = Convert.ToDateTime(drList["DateIns"]);

                        _dtList.Rows.Add(drList["ID"], drList["DateIns"], drList["ShareType"], drList["ShareCodes_ID"], drList["Product_Title"], drList["ProductCategory_Title"], 
                                         drList["Code"], drList["Code2"], drList["ISIN"], drList["Title"], drList["Curr"], drList["Open"], drList["High"], drList["Low"],
                                         drList["Close"], drList["Last"], drList["Volume"]);

                    }
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
                using (SqlCommand cmd = new SqlCommand("Insert_SharePrices", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ShareType", SqlDbType.Int).Value = _iShareType;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dDateIns;
                    cmd.Parameters.Add("@Share_ID", SqlDbType.Int).Value = _iShareCodes_ID;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 50).Value = _sCode;
                    cmd.Parameters.Add("@Open", SqlDbType.Float).Value = _fltOpen;
                    cmd.Parameters.Add("@High", SqlDbType.Float).Value = _fltHigh;
                    cmd.Parameters.Add("@Low", SqlDbType.Float).Value = _fltLow;
                    cmd.Parameters.Add("@Close ", SqlDbType.Float).Value = _fltClose;
                    cmd.Parameters.Add("@Last", SqlDbType.Float).Value = _fltLast;
                    cmd.Parameters.Add("@Volume", SqlDbType.Float).Value = _fltVolume;
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
                using (SqlCommand cmd = new SqlCommand("Edit_SharePrices", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Open", SqlDbType.Float).Value = _fltOpen;
                    cmd.Parameters.Add("@High", SqlDbType.Float).Value = _fltHigh;
                    cmd.Parameters.Add("@Low", SqlDbType.Float).Value = _fltLow;
                    cmd.Parameters.Add("@Close ", SqlDbType.Float).Value = _fltClose;
                    cmd.Parameters.Add("@Last", SqlDbType.Float).Value = _fltLast;
                    cmd.Parameters.Add("@Volume", SqlDbType.Float).Value = _fltVolume;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "SharePrices";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return _iRecord_ID; } set { _iRecord_ID = value; } }
        public int ShareType { get { return _iShareType; } set { _iShareType = value; } }
        public DateTime DateIns { get { return _dDateIns; } set { _dDateIns = value; } }
        public int ShareCodes_ID { get { return _iShareCodes_ID; } set { _iShareCodes_ID = value; } }
        public string Code { get { return _sCode; } set { _sCode = value; } }
        public float Open { get { return _fltOpen; } set { _fltOpen = value; } }
        public float High { get { return _fltHigh; } set { _fltHigh = value; } }
        public float Low { get { return _fltLow; } set { _fltLow = value; } }
        public float Close { get { return _fltClose; } set { _fltClose = value; } }       
        public float Last { get { return _fltLast; } set { _fltLast = value; } }
        public float Volume { get { return _fltVolume; } set { _fltVolume = value; } }       
        public DateTime DateFrom { get { return _dDateFrom; } set { _dDateFrom = value; } }
        public DateTime DateTo { get { return _dDateTo; } set { _dDateTo = value; } }
        public int ProductType_ID { get { return _iProductType_ID; } set { _iProductType_ID = value; } }
        public int ProductCategory_ID { get { return _iProductCategory_ID; } set { _iProductCategory_ID = value; } }
        public int Product_ID { get { return _iProduct_ID; } set { _iProduct_ID = value; } }
        public string Filter { get { return _sFilter; } set { _sFilter = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
