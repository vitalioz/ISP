using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsContracts_Monitoring
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataRow dtRow;

        private int      _iRecord_ID;
        private int      _iCDP_ID;
        private int      _iYear;
        private int      _iMonth;
        private DateTime _dDateIns;
        private string   _sResults;
        private string   _sNotes;
        private string   _sTotalNotes;
        private string   _sStatementFile;
        private string   _sAssetAllocationFile;
        private string   _sRisksFile;
        private string   _sMonitoringPDFile;
        private int      _iStatus;
        private string   _sSentDate;
        private int      _iSendAttemptsCount;
        private string   _sSendMessage;

        private int _iYearFrom;
        private int _iMonthFrom;
        private int _iYearTo;
        private int _iMonthTo;
        private int _iAdvisor_ID;
        private int _iService_ID;
        private string _sCode;
        private string _sPortfolio;
        private string _sContractTitle;
        private string _sCurrency;
        private string _sProfile_Title;

        private DataTable _dtList;

        public clsContracts_Monitoring()
        {
            this._iRecord_ID = 0;
            this._dDateIns = Convert.ToDateTime("1900/01/01");
            this._iCDP_ID = 0;
            this._iYear = 0;
            this._iMonth = 0;
            this._sNotes = "";
            this._sTotalNotes = "";
            this._sStatementFile = "";
            this._sAssetAllocationFile = "";
            this._sRisksFile = "";
            this._sMonitoringPDFile = "";
            this._iStatus = 0;
            this._sSentDate = "";
            this._iSendAttemptsCount = 0;
            this._sSendMessage = "";
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
                cmd = new SqlCommand("GetContracts_Monitoring", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iCDP_ID = Convert.ToInt32(drList["CDP_ID"]);
                    this._sCode = drList["Code"] + "";
                    this._sPortfolio = drList["Portfolio"] + "";
                    this._sContractTitle = drList["ContractTitle"] + "";
                    this._sCurrency = drList["Currency"] + "";
                    this._sProfile_Title = drList["Profile_Title"] + "";
                    this._iYear = Convert.ToInt32(drList["Year"]);
                    this._iMonth = Convert.ToInt32(drList["Month"]);
                    this._dDateIns = Convert.ToDateTime(drList["DateIns"]);
                    this._sResults = drList["Results"] + "";
                    this._sNotes = drList["Notes"] + "";
                    this._sTotalNotes = drList["TotalNotes"] + "";
                    this._sStatementFile = drList["StatementFile"] + "";
                    this._sAssetAllocationFile = drList["AssetAllocationFile"] + "";
                    this._sRisksFile = drList["RisksFile"] + "";
                    this._sMonitoringPDFile = drList["MonitoringPDFile"] + "";
                    this._iStatus = Convert.ToInt32(drList["Status"]);
                    this._sSentDate = drList["SentDate"] + "";
                    this._iSendAttemptsCount = Convert.ToInt32(drList["SendAttemptsCount"]);
                    this._sSendMessage = drList["SendMessage"] + "";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            int i = 0;
            _dtList = new DataTable();
            _dtList = new DataTable("Recs");
            _dtList.Columns.Add("AA", typeof(int));
            _dtList.Columns.Add("CDP_ID", typeof(int));
            _dtList.Columns.Add("Code", typeof(string));
            _dtList.Columns.Add("Portfolio", typeof(string));
            _dtList.Columns.Add("ContractTitle", typeof(string));
            _dtList.Columns.Add("Profile_Title", typeof(string));
            _dtList.Columns.Add("Currency", typeof(string));
            _dtList.Columns.Add("Year", typeof(int));
            _dtList.Columns.Add("Month", typeof(int));
            _dtList.Columns.Add("DateIns", typeof(DateTime));  
            _dtList.Columns.Add("Results", typeof(string));
            _dtList.Columns.Add("Notes", typeof(string));
            _dtList.Columns.Add("TotalNotes", typeof(string));
            _dtList.Columns.Add("StatementFile", typeof(string));
            _dtList.Columns.Add("AssetAllocationFile", typeof(string));
            _dtList.Columns.Add("RisksFile", typeof(string));
            _dtList.Columns.Add("MonitoringPDFile", typeof(string));
            _dtList.Columns.Add("Status", typeof(int));
            _dtList.Columns.Add("SentDate", typeof(string));
            _dtList.Columns.Add("SendAttemptsCount", typeof(int));
            _dtList.Columns.Add("SendMessage", typeof(string));
            _dtList.Columns.Add("Email", typeof(string));
            _dtList.Columns.Add("ID", typeof(int));
            
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetContracts_Monitoring_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@YearFrom", _iYearFrom));
                cmd.Parameters.Add(new SqlParameter("@MonthFrom", _iMonthFrom));
                cmd.Parameters.Add(new SqlParameter("@YearTo", _iYearTo));
                cmd.Parameters.Add(new SqlParameter("@MonthTo", _iMonthTo));
                cmd.Parameters.Add(new SqlParameter("@Advisor_ID", _iAdvisor_ID));
                cmd.Parameters.Add(new SqlParameter("@Service_ID", _iService_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    i = i + 1;
                    dtRow = _dtList.NewRow();
                    dtRow["AA"] = i;
                    dtRow["CDP_ID"] = drList["CDP_ID"];
                    dtRow["Code"] = drList["Code"] + "";
                    dtRow["Portfolio"] = drList["Portfolio"] + "";
                    dtRow["ContractTitle"] = drList["ContractTitle"] + "";
                    dtRow["Profile_Title"] = drList["Profile_Title"] + "";
                    dtRow["Currency"] = drList["Currency"] + "";
                    dtRow["Year"] = drList["Year"];
                    dtRow["Month"] = drList["Month"];
                    dtRow["DateIns"] = Convert.ToDateTime(drList["DateIns"]).ToString("dd/MM/yyyy");
                    dtRow["Results"] = drList["Results"] + "";
                    dtRow["Notes"] = drList["Notes"] + "";
                    dtRow["TotalNotes"] = drList["TotalNotes"] + "";
                    dtRow["StatementFile"] = drList["StatementFile"] + "";
                    dtRow["AssetAllocationFile"] = drList["AssetAllocationFile"] + "";
                    dtRow["RisksFile"] = drList["RisksFile"] + "";
                    dtRow["MonitoringPDFile"] = drList["MonitoringPDFile"] + "";
                    dtRow["Status"] = drList["Status"];
                    dtRow["SentDate"] = drList["SentDate"] + "";
                    dtRow["SendAttemptsCount"] = drList["SendAttemptsCount"];
                    dtRow["SendMessage"] = drList["SendMessage"] + "";
                    dtRow["Email"] = drList["Email"] + "";
                    dtRow["ID"] = drList["ID"];
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
                using (SqlCommand cmd = new SqlCommand("InsertContracts_Monitoring", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CDP_ID", SqlDbType.Int).Value = _iCDP_ID;
                    cmd.Parameters.Add("@Year", SqlDbType.Int).Value = _iYear;
                    cmd.Parameters.Add("@Month", SqlDbType.Int).Value = _iMonth;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dDateIns;
                    cmd.Parameters.Add("@Results", SqlDbType.NVarChar, 20).Value = _sResults;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 1000).Value = _sNotes;
                    cmd.Parameters.Add("@TotalNotes", SqlDbType.NVarChar, 1000).Value = _sTotalNotes;
                    cmd.Parameters.Add("@StatementFile", SqlDbType.NVarChar, 200).Value = _sStatementFile;
                    cmd.Parameters.Add("@AssetAllocationFile", SqlDbType.NVarChar, 200).Value = _sAssetAllocationFile;
                    cmd.Parameters.Add("@RisksFile", SqlDbType.NVarChar, 200).Value = _sRisksFile;
                    cmd.Parameters.Add("@MonitoringPDFile", SqlDbType.NVarChar, 200).Value = _sMonitoringPDFile;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@SentDate", SqlDbType.NVarChar, 20).Value = _sSentDate;
                    cmd.Parameters.Add("@SendMessage", SqlDbType.NVarChar, 100).Value = _sSendMessage;
                    cmd.ExecuteNonQuery();
                    _iRecord_ID = Convert.ToInt32(outParam.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally { conn.Close(); }

            return _iRecord_ID;
        }
        public int EditRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("EditContracts_Monitoring", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@CDP_ID", SqlDbType.Int).Value = _iCDP_ID;
                    cmd.Parameters.Add("@Year", SqlDbType.Int).Value = _iYear;
                    cmd.Parameters.Add("@Month", SqlDbType.Int).Value = _iMonth;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dDateIns;
                    cmd.Parameters.Add("@Results", SqlDbType.NVarChar, 20).Value = _sResults;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 1000).Value = _sNotes;
                    cmd.Parameters.Add("@TotalNotes", SqlDbType.NVarChar, 1000).Value = _sTotalNotes;
                    cmd.Parameters.Add("@StatementFile", SqlDbType.NVarChar, 200).Value = _sStatementFile;
                    cmd.Parameters.Add("@AssetAllocationFile", SqlDbType.NVarChar, 200).Value = _sAssetAllocationFile;
                    cmd.Parameters.Add("@RisksFile", SqlDbType.NVarChar, 200).Value = _sRisksFile;
                    cmd.Parameters.Add("@MonitoringPDFile", SqlDbType.NVarChar, 200).Value = _sMonitoringPDFile;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@SentDate", SqlDbType.NVarChar, 20).Value = _sSentDate;
                    cmd.Parameters.Add("@SendMessage", SqlDbType.NVarChar, 100).Value = _sSendMessage;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Contracts_Monitoring";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int CDP_ID { get { return _iCDP_ID; } set { _iCDP_ID = value; } }
        public int Year { get { return _iYear; } set { _iYear = value; } }
        public int Month { get { return _iMonth; } set { _iMonth = value; } }
        public DateTime DateIns { get { return _dDateIns; } set { _dDateIns = value; } }
        public string Results { get { return this._sResults; } set { this._sResults = value; } }
        public string Notes { get { return _sNotes; } set { _sNotes = value; } }
        public string TotalNotes { get { return _sTotalNotes; } set { _sTotalNotes = value; } }
        public string StatementFile { get { return _sStatementFile; } set { _sStatementFile = value; } }
        public string AssetAllocationFile { get { return _sAssetAllocationFile; } set { _sAssetAllocationFile = value; } }
        public string RisksFile { get { return _sRisksFile; } set { _sRisksFile = value; } }
        public string MonitoringPDFile { get { return _sMonitoringPDFile; } set { _sMonitoringPDFile = value; } }
        public int Status { get { return _iStatus; } set { _iStatus = value; } }
        public string SentDate { get { return _sSentDate; } set { _sSentDate = value; } }
        public int SendAttemptsCount { get { return _iSendAttemptsCount; } set { _iSendAttemptsCount = value; } }
        public string SendMessage { get { return _sSendMessage; } set { _sSendMessage = value; } }
        public string Code { get { return _sCode; } set { _sCode = value; } }
        public string Portfolio { get { return _sPortfolio; } set { _sPortfolio = value; } }
        public string ContractTitle { get { return _sContractTitle; } set { _sContractTitle = value; } }
        public string Currency { get { return _sCurrency; } set { _sCurrency = value; } }
        public string Profile_Title { get { return _sProfile_Title; } set { _sProfile_Title = value; } }
        public int YearFrom { get { return _iYearFrom; } set { _iYearFrom = value; } }
        public int MonthFrom { get { return _iMonthFrom; } set { _iMonthFrom = value; } }
        public int YearTo { get { return _iYearTo; } set { _iYearTo = value; } }
        public int MonthTo { get { return _iMonthTo; } set { _iMonthTo = value; } }
        public int Advisor_ID { get { return _iAdvisor_ID; } set { _iAdvisor_ID = value; } }
        public int Service_ID { get { return _iService_ID; } set { _iService_ID = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
