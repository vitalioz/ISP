using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsCountries
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int       _iRecord_ID;
        private string    _sCode;
        private string    _sCode3;
        private string    _sTitle;
        private string    _sTitle_MorningStar;
        private string    _sTitle_Greek;
        private string    _sTitle_Alias;
        private int       _iTipos;
        private int       _iCountriesGroup_ID;
        private int       _iInvestGeography_ID;
        private string    _sPhoneCode;
        private DataTable _dtList;

        public clsCountries()
        {
            this._iRecord_ID = 0;
            this._sCode = "";
            this._sCode3 = "";
            this._sTitle = "";
            this._sTitle_MorningStar = "";
            this._sTitle_Greek = "";
            this._sTitle_Alias = "";
            this._iTipos = 0;
            this._iCountriesGroup_ID = 0;
            this._iInvestGeography_ID = 0;
            this._sPhoneCode = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Countries"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._sCode = drList["Code"] + "";
                    this._sCode3 = drList["Code3"] + "";
                    this._sTitle = drList["Title"] + "";
                    this._sTitle_MorningStar = drList["Title_MorningStar"] + "";
                    this._sTitle_Greek = drList["TitleGreek"] + "";
                    this._sTitle_Alias = drList["Title_Alias"] + "";
                    this._iTipos = Convert.ToInt32(drList["Tipos"]);
                    this._iCountriesGroup_ID = Convert.ToInt32(drList["CountriesGroup_ID"]);
                    this._iInvestGeography_ID = Convert.ToInt32(drList["InvestGeography_ID"]);
                    this._sPhoneCode = drList["PhoneCode"] + ""; 
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("Tipos", typeof(int));
            _dtList.Columns.Add("Title", typeof(string));
            _dtList.Columns.Add("TitleGreek", typeof(string));
            _dtList.Columns.Add("Code", typeof(string));
            _dtList.Columns.Add("CountriesGroup_ID", typeof(int));
            _dtList.Columns.Add("InvestGeography_ID", typeof(int));
            _dtList.Columns.Add("PhoneCode", typeof(string));

            _dtList.Rows.Add(0, 0, "-", "-", "-", 0, 0, "-");

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Countries"));
                cmd.Parameters.Add(new SqlParameter("@Col", ""));
                cmd.Parameters.Add(new SqlParameter("@Value", ""));
                cmd.Parameters.Add(new SqlParameter("@Order", "Title"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["Tipos"], drList["Title"], drList["TitleGreek"], drList["Code"], drList["CountriesGroup_ID"], drList["InvestGeography_ID"], drList["PhoneCode"]);
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
                using (SqlCommand cmd = new SqlCommand("InsertCountries", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Tipos", SqlDbType.Int).Value = _iTipos;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 10).Value = _sCode;
                    cmd.Parameters.Add("@Code3", SqlDbType.NVarChar, 10).Value = _sCode3;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 60).Value = _sTitle;
                    cmd.Parameters.Add("@Title_MorningStar", SqlDbType.NVarChar, 60).Value = _sTitle_MorningStar;
                    cmd.Parameters.Add("@TitleGreek", SqlDbType.NVarChar, 60).Value = _sTitle_Greek;
                    cmd.Parameters.Add("@Title_Alias", SqlDbType.NVarChar, 60).Value = _sTitle_Alias;
                    cmd.Parameters.Add("@CountriesGroup_ID", SqlDbType.Int).Value = _iCountriesGroup_ID;
                    cmd.Parameters.Add("@InvestGeography_ID", SqlDbType.Int).Value = _iInvestGeography_ID;
                    cmd.Parameters.Add("@PhoneCode", SqlDbType.NVarChar, 10).Value = _sPhoneCode;

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
                using (SqlCommand cmd = new SqlCommand("EditCountries", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Tipos", SqlDbType.Int).Value = _iTipos;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 10).Value = _sCode;
                    cmd.Parameters.Add("@Code3", SqlDbType.NVarChar, 10).Value = _sCode3;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 60).Value = _sTitle;
                    cmd.Parameters.Add("@Title_MorningStar", SqlDbType.NVarChar, 60).Value = _sTitle_MorningStar;
                    cmd.Parameters.Add("@TitleGreek", SqlDbType.NVarChar, 60).Value = _sTitle_Greek;
                    cmd.Parameters.Add("@Title_Alias", SqlDbType.NVarChar, 60).Value = _sTitle_Alias;
                    cmd.Parameters.Add("@CountriesGroup_ID", SqlDbType.Int).Value = _iCountriesGroup_ID;
                    cmd.Parameters.Add("@InvestGeography_ID", SqlDbType.Int).Value = _iInvestGeography_ID;
                    cmd.Parameters.Add("@PhoneCode", SqlDbType.NVarChar, 10).Value = _sPhoneCode;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Countries";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = this._iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return _iRecord_ID; } set { _iRecord_ID = value; } }
        public string Code { get { return _sCode; } set { _sCode = value; } }
        public string Code3 { get { return _sCode3; } set { _sCode3 = value; } }
        public string Title { get { return _sTitle; } set { _sTitle = value; } }
        public string Title_MorningStar { get { return _sTitle_MorningStar; } set { _sTitle_MorningStar = value; } }
        public string Title_Greek { get { return _sTitle_Greek; } set { _sTitle_Greek = value; } }
        public string Title_Alias { get { return _sTitle_Alias; } set { _sTitle_Alias = value; } }
        public int Tipos { get { return _iTipos; } set { _iTipos = value; } }
        public int CountriesGroup_ID { get { return _iCountriesGroup_ID; } set { _iCountriesGroup_ID = value; } }
        public int InvestGeography_ID { get { return _iInvestGeography_ID; } set { _iInvestGeography_ID = value; } }
        public string PhoneCode { get { return _sPhoneCode; } set { _sPhoneCode = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
