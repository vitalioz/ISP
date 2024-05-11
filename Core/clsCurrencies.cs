using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsCurrencies
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataRow dtRow;

        private int       _iRecord_ID;
        private string   _sTitle;
        private string     _sCode;
        private string    _sCode_MorningStar;
        private float     _fltKoef;
        private string    _sCode_Convert;

        private DateTime  _dDateFrom;
        private DateTime  _dDateTo;
        private DataTable _dtList;

        public clsCurrencies()
        {
            this._iRecord_ID = 0;
            this._sTitle = "";
            this._sCode = "";
            this._sCode_MorningStar = "";
            this._fltKoef = 0;
            this._sCode_Convert = "";
            this._dDateFrom = Convert.ToDateTime("1900/01/01");
            this._dDateTo = Convert.ToDateTime("2070/12/31");
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Currencies"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", _iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", "Title"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._sTitle = drList["Title"] + "";
                    this._sCode = drList["Code"] + "";
                    this._sCode_MorningStar = drList["Code_MorningStar"] + "";
                    this._fltKoef = Convert.ToSingle(drList["Koef"]);
                    this._sCode_Convert = drList["Code_Convert"] + "";
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
            _dtList.Columns.Add("Koef", typeof(float));
            _dtList.Columns.Add("Code_Convert", typeof(string));
            _dtList.Columns.Add("Code_MorningStar", typeof(string));            

            dtRow = _dtList.NewRow();
            dtRow["ID"] = 0;
            dtRow["Title"] = "";
            dtRow["Koef"] = 1;
            dtRow["Code_Convert"] = "";
            dtRow["Code_MorningStar"] = "";
            _dtList.Rows.Add(dtRow);

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Currencies"));
                cmd.Parameters.Add(new SqlParameter("@Col", ""));
                cmd.Parameters.Add(new SqlParameter("@Value", ""));
                cmd.Parameters.Add(new SqlParameter("@Order", "Title"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["Title"] = drList["Title"];
                    dtRow["Koef"] = drList["Koef"];
                    dtRow["Code_Convert"] = drList["Code_Convert"];
                    dtRow["Code_MorningStar"] = drList["Code_MorningStar"];
                    _dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetCurrencyRates_Period()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("DateIns", typeof(DateTime));
            _dtList.Columns.Add("Currency", typeof(string));
            _dtList.Columns.Add("Rate", typeof(double));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetCurrencyRate_Period", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dDateFrom.Date));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dDateTo.Date));
                cmd.Parameters.Add(new SqlParameter("@Code", _sCode));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["DateIns"] = drList["DateIns"];
                    dtRow["Currency"] = drList["Code"];             // it's currency so CODE
                    dtRow["Rate"] = drList["Close"];
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
                using (SqlCommand cmd = new SqlCommand("InsertCurrencies", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 50).Value = _sTitle;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 50).Value = _sCode;
                    cmd.Parameters.Add("@Code_MorningStar", SqlDbType.NVarChar, 50).Value = _sCode_MorningStar;
                    cmd.Parameters.Add("@Koef", SqlDbType.Float).Value = _fltKoef;
                    cmd.Parameters.Add("@Code_Convert", SqlDbType.NVarChar, 50).Value = _sCode_Convert;

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
                using (SqlCommand cmd = new SqlCommand("EditCurrencies", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 50).Value = _sTitle;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 50).Value = _sCode;
                    cmd.Parameters.Add("@Code_MorningStar", SqlDbType.NVarChar, 50).Value = _sCode_MorningStar;
                    cmd.Parameters.Add("@Koef", SqlDbType.Float).Value = _fltKoef;
                    cmd.Parameters.Add("@Code_Convert", SqlDbType.NVarChar, 50).Value = _sCode_Convert;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Currencies";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public string Title  { get { return _sTitle; } set { _sTitle = value; } }
        public string Code { get { return _sCode; } set { _sCode = value; } }
        public string Code_MorningStar { get { return _sCode_MorningStar; } set { _sCode_MorningStar = value; } }        
        public float Koef { get { return _fltKoef; } set { _fltKoef = value; } }
        public string Code_Convert { get { return _sCode_Convert; } set { _sCode_Convert = value; } }
        public DateTime DateFrom { get { return _dDateFrom; } set { _dDateFrom = value; } }
        public DateTime DateTo { get { return _dDateTo; } set { _dDateTo = value; } }
        public DataTable List  { get { return _dtList; } set { _dtList = value; } }

    }
}
