using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsWebUsers
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataRow dtRow;

        private int _iRecord_ID;
        private int _iClient_ID;
        private string _sPassword;
        private int _iStatus;
        private DateTime _dDateIns;

        private string _sEMail;
        private string _sMobile;
        private string _sAFM;
        private string _sDoB;
        private DateTime _dDateFrom;
        private DateTime _dDateTo;
        private DataTable _dtList;

        public clsWebUsers()
        {
            this._iRecord_ID = 0;
            this._iClient_ID = 0;
            this._sPassword = "";
            this._iStatus = 0;
            this._dDateIns = Convert.ToDateTime("1900/01/01");

            this._sEMail = "";
            this._sMobile = "";
            this._sAFM = "";
            this._sDoB = "1900/01/01";
            this._dDateFrom = Convert.ToDateTime("1900/01/01");
            this._dDateTo = Convert.ToDateTime("2070/12/31");
        }
        public void GetRecord()
        {
            try
            {
                conn = new SqlConnection(Global.connStr);
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "WebUsers"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", _iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    this._sPassword = drList["Password"] + "";
                    this._iStatus = Convert.ToInt32(drList["Status"]);
                    this._dDateIns = Convert.ToDateTime(drList["DateIns"]);
                }
            }
            catch (Exception ex)
            {
                string sTemp = ex.Message;
            }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("Password", typeof(string));
            _dtList.Columns.Add("Client_ID", typeof(int));
            _dtList.Columns.Add("Klient_ID", typeof(int));

            _dtList.Columns.Add("Surname", typeof(string));
            _dtList.Columns.Add("Firstname", typeof(string));
            _dtList.Columns.Add("DoB", typeof(string));
            _dtList.Columns.Add("BornPlace", typeof(string));
            _dtList.Columns.Add("ADT", typeof(string));
            _dtList.Columns.Add("ExpireDate", typeof(string));
            _dtList.Columns.Add("Police", typeof(string));
            _dtList.Columns.Add("DOY", typeof(string));
            _dtList.Columns.Add("AFM", typeof(string));
            _dtList.Columns.Add("Address", typeof(string));
            _dtList.Columns.Add("City", typeof(string));
            _dtList.Columns.Add("Zip", typeof(string));
            _dtList.Columns.Add("Country_ID", typeof(int));
            _dtList.Columns.Add("CountryTitle", typeof(string));
            _dtList.Columns.Add("PhoneCode", typeof(string));
            _dtList.Columns.Add("Tel", typeof(string));
            _dtList.Columns.Add("Mobile", typeof(string));
            _dtList.Columns.Add("EMail", typeof(string));

            _dtList.Columns.Add("Status", typeof(int));
            _dtList.Columns.Add("DateIns", typeof(DateTime));
            _dtList.Columns.Add("Klient_DateIns", typeof(DateTime));

            try
            {
                conn = new SqlConnection(Global.connStr);
                conn.Open();

                SqlCommand cmd = new SqlCommand("GetWebUsers", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMail", _sEMail));
                cmd.Parameters.Add(new SqlParameter("@Mobile", _sMobile));
                cmd.Parameters.Add(new SqlParameter("@AFM", _sAFM));
                cmd.Parameters.Add(new SqlParameter("@DoB", _sDoB));
                cmd.Parameters.Add(new SqlParameter("@Client_ID", _iClient_ID));
                cmd.Parameters.Add(new SqlParameter("@Password", _sPassword));

                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["Password"] = drList["Password"].ToString();
                    //if (!(drList["Klient_ID"] is DBNull))
                    //{
                    dtRow["Client_ID"] = drList["Client_ID"];
                    dtRow["Klient_ID"] = drList["Klient_ID"];
                    dtRow["Surname"] = drList["Surname"].ToString();
                    dtRow["Firstname"] = drList["Firstname"].ToString();
                    dtRow["DoB"] = drList["DoB"];
                    dtRow["BornPlace"] = drList["BornPlace"].ToString();
                    dtRow["ADT"] = drList["ADT"].ToString();
                    dtRow["ExpireDate"] = drList["ExpireDate"].ToString();
                    dtRow["Police"] = drList["Police"].ToString();
                    dtRow["DOY"] = drList["DOY"].ToString();
                    dtRow["AFM"] = drList["AFM"].ToString();
                    dtRow["Address"] = drList["Address"].ToString();
                    dtRow["City"] = drList["City"].ToString();
                    dtRow["Zip"] = drList["Zip"].ToString();
                    dtRow["Country_ID"] = Convert.ToInt32(drList["Country_ID"]);
                    dtRow["CountryTitle"] = drList["CountryTitle"].ToString();
                    dtRow["PhoneCode"] = drList["PhoneCode"].ToString();
                    dtRow["Tel"] = drList["Tel"].ToString();
                    dtRow["Mobile"] = drList["Mobile"].ToString();
                    dtRow["EMail"] = drList["EMail"].ToString();
                    //}
                    //else dtRow["Klient_ID"] = 0;
                    dtRow["Status"] = Convert.ToInt32(drList["Status"]);
                    dtRow["DateIns"] = drList["DateIns"];
                    dtRow["Klient_DateIns"] = drList["Klient_DateIns"];
                    _dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex)
            {
                string sTemp = ex.Message;
            }
            finally { conn.Close(); }
        }

        public int InsertRecord()
        {
            try
            {
                conn = new SqlConnection(Global.connStr);
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("InsertWebUsers", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 50).Value = _sPassword;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dDateIns;
                    cmd.ExecuteNonQuery();
                    _iRecord_ID = Convert.ToInt32(outParam.Value);
                }
            }
            catch (Exception ex)
            {
                string sTemp = ex.Message;
            }
            finally { conn.Close(); }

            return _iRecord_ID;
        }
        public int EditRecord()
        {
            try
            {
                conn = new SqlConnection(Global.connStr);
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("EditWebUsers", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 50).Value = _sPassword;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string sTemp = ex.Message;
            }
            finally { conn.Close(); }

            return _iRecord_ID;
        }
        public void DeleteRecord()
        {
            try
            {
                conn = new SqlConnection(Global.connStr);
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "WebUsers";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string sTemp = ex.Message;
            }
            finally { conn.Close(); }
        }
        public void DeleteRecord_Client_ID()
        {
            try
            {
                conn = new SqlConnection(Global.connStr);
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "WebUsers";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "Client_ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iClient_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string sTemp = ex.Message;
            }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int Client_ID { get { return _iClient_ID; } set { _iClient_ID = value; } }
        public string Password { get { return _sPassword; } set { _sPassword = value; } }
        public int Status { get { return _iStatus; } set { _iStatus = value; } }
        public DateTime DateIns { get { return _dDateIns; } set { _dDateIns = value; } }

        public string EMail { get { return _sEMail; } set { _sEMail = value; } }
        public string Mobile { get { return _sMobile; } set { _sMobile = value; } }
        public string AFM { get { return _sAFM; } set { _sAFM = value; } }
        public string DoB { get { return _sDoB; } set { _sDoB = value; } }
        public DateTime DateFrom { get { return _dDateFrom; } set { _dDateFrom = value; } }
        public DateTime DateTo { get { return _dDateTo; } set { _dDateTo = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
