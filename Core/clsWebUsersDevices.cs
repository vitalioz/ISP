using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsWebUsersDevices
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int    _iRecord_ID;
        private int    _iWU_ID;
        private string _sManufacturer;
        private string _sBrand;
        private string _sModel;
        private string _sBoard;
        private string _sHardware;
        private string _sUnique_ID;
        private string _sScreenResolution;
        private string _sScreenDensity;
        private string _sHost;
        private string _sVersion;
        private string _sAPI_level;
        private string _sBuild_ID;
        private string _sBuild_Time;
        private string _sFingerprint;
        private string _sPhoneType;
        private string _sNetworkCountryISO;
        private string _sNetworkOperatorName;
        private string _sDeviceId;
        private string _sDeviceSoftwareVersion;
        private string _sSimCountryIso;
        private string _sSimOperatorName;
        private string _sSimSerialNumber;
        private string _sImei;
        private string _sMeid;
        private string _sMmsUAProfUrl;
        private string _sMmsUserAgent;
        private string _sSubscriberId;
        private string _sTypeAllocationCode;
        private string _sOS;
        private string _sVideo;
        private int    _iStatus;       
        private DateTime _dDateIns;

        private string _sEMail;
        private string _sMobile;
        private string _sAFM;
        private string _sDoB;
        private int    _iClient_ID;
        private string _sPassword;

        private DataTable _dtList;

        public clsWebUsersDevices()
        {
            this._iRecord_ID = 0;
            this._iWU_ID = 0;
            this._sManufacturer = "";
            this._sBrand = "";
            this._sModel = "";
            this._sBoard = "";
            this._sHardware = "";
            this._sUnique_ID = "";
            this._sScreenResolution = "";
            this._sScreenDensity = "";
            this._sHost = "";
            this._sVersion = "";
            this._sAPI_level = "";
            this._sBuild_ID = "";
            this._sBuild_Time = "";
            this._sFingerprint = "";
            this._sPhoneType = "";
            this._sNetworkCountryISO = "";
            this._sNetworkOperatorName = "";
            this._sDeviceId = "";
            this._sDeviceSoftwareVersion = "";
            this._sSimCountryIso = "";
            this._sSimOperatorName = "";
            this._sSimSerialNumber = "";
            this._sImei = "";
            this._sMeid = "";
            this._sMmsUAProfUrl = "";
            this._sMmsUserAgent = "";
            this._sSubscriberId = "";
            this._sTypeAllocationCode = "";
            this._sOS = "";
            this._sVideo = "";
            this._iStatus = 0;      
            this._dDateIns = Convert.ToDateTime("1900/01/01");

            this._sEMail = "";
            this._sMobile = "";
            this._sAFM = "";
            this._sDoB = "";
            this._iClient_ID = 0;
            this._sPassword = "";
        }
        public void GetRecord()
        {
            try
            {
                conn = new SqlConnection(Global.connStr);
                conn.Open();

                cmd = new SqlCommand("GetWebUsersDevices", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Unique_ID", this._sUnique_ID));
                cmd.Parameters.Add(new SqlParameter("@OS", this._sOS));
                cmd.Parameters.Add(new SqlParameter("@Video", this._sVideo));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iWU_ID = Convert.ToInt32(drList["WU_ID"]);
                    this._sManufacturer = drList["Manufacturer"] + "";
                    this._sBrand = drList["Brand"] + "";
                    this._sModel = drList["Model"] + "";
                    this._sBoard = drList["Board"] + "";
                    this._sHardware = drList["Hardware"] + "";
                    this._sUnique_ID = drList["Unique_ID"] + "";
                    this._sScreenResolution = drList["ScreenResolution"] + "";
                    this._sScreenDensity = drList["ScreenDensity"] + "";
                    this._sHost = drList["Host"] + "";
                    this._sVersion = drList["Version"] + "";
                    this._sAPI_level = drList["API_level"] + "";
                    this._sBuild_ID = drList["Build_ID"] + "";
                    this._sBuild_Time = drList["Build_Time"] + "";
                    this._sFingerprint = drList["Fingerprint"] + "";
                    this._sPhoneType = drList["PhoneType"] + "";
                    this._iStatus = Convert.ToInt32(drList["Status"]);
                    this._sNetworkCountryISO = drList["NetworkCountryISO"] + "";
                    this._sNetworkOperatorName = drList["NetworkOperatorName"] + "";
                    this._sDeviceId = drList["DeviceId"] + "";
                    this._sDeviceSoftwareVersion = drList["DeviceSoftwareVersion"] + "";
                    this._sSimCountryIso = drList["SimCountryIso"] + "";
                    this._sSimOperatorName = drList["SimOperatorName"] + "";
                    this._sImei = drList["Imei"] + "";
                    this._sMeid = drList["Meid"] + "";
                    this._sSimSerialNumber = drList["SimSerialNumber"] + "";
                    this._sMmsUAProfUrl = drList["MmsUAProfUrl"] + "";
                    this._sMmsUserAgent = drList["MmsUserAgent"] + "";
                    this._sSubscriberId = drList["SubscriberId"] + "";
                    this._sTypeAllocationCode = drList["TypeAllocationCode"] + "";
                    this._sOS = drList["OS"] + "";
                    this._sVideo = drList["Video"] + "";
                    this._iStatus = Convert.ToInt32(drList["Status"]);
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
            _dtList.Columns.Add("WU_ID", typeof(int));
            _dtList.Columns.Add("Manufacturer", typeof(string));
            _dtList.Columns.Add("Brand", typeof(string));
            _dtList.Columns.Add("Model", typeof(string));
            _dtList.Columns.Add("Board", typeof(string));
            _dtList.Columns.Add("Hardware", typeof(string));
            _dtList.Columns.Add("Unique_ID", typeof(string));
            _dtList.Columns.Add("ScreenResolution", typeof(string));
            _dtList.Columns.Add("ScreenDensity", typeof(string));
            _dtList.Columns.Add("Host", typeof(string));
            _dtList.Columns.Add("Version", typeof(string));
            _dtList.Columns.Add("API_level", typeof(string));
            _dtList.Columns.Add("Build_ID", typeof(string));
            _dtList.Columns.Add("Build_Time", typeof(string));
            _dtList.Columns.Add("Fingerprint", typeof(string));
            _dtList.Columns.Add("PhoneType", typeof(string));
            _dtList.Columns.Add("NetworkCountryISO", typeof(string));
            _dtList.Columns.Add("NetworkOperatorName", typeof(string));
            _dtList.Columns.Add("DeviceId", typeof(string));
            _dtList.Columns.Add("DeviceSoftwareVersion", typeof(string));
            _dtList.Columns.Add("SimCountryIso", typeof(string));
            _dtList.Columns.Add("SimOperatorName", typeof(string));
            _dtList.Columns.Add("SimSerialNumber", typeof(string));
            _dtList.Columns.Add("Imei", typeof(string));
            _dtList.Columns.Add("Meid", typeof(string));
            _dtList.Columns.Add("MmsUAProfUrl", typeof(string));
            _dtList.Columns.Add("MmsUserAgent", typeof(string));
            _dtList.Columns.Add("SubscriberId", typeof(string));
            _dtList.Columns.Add("TypeAllocationCode", typeof(string));
            _dtList.Columns.Add("OS", typeof(string));
            _dtList.Columns.Add("Video", typeof(string));
            _dtList.Columns.Add("Status", typeof(int));
            _dtList.Columns.Add("DateIns", typeof(DateTime));
            _dtList.Columns.Add("Client_ID", typeof(int));
            _dtList.Columns.Add("CountryCode", typeof(string));
            _dtList.Columns.Add("EMail", typeof(string));
            _dtList.Columns.Add("Mobile", typeof(string));
            _dtList.Columns.Add("PhoneCode", typeof(string));
            _dtList.Columns.Add("Password", typeof(string));

            try
            {
                conn = new SqlConnection(Global.connStr);
                conn.Open();
                cmd = new SqlCommand("GetWebUsersDevices_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@WU_ID", _iWU_ID));
                cmd.Parameters.Add(new SqlParameter("@EMail", _sEMail));
                cmd.Parameters.Add(new SqlParameter("@Mobile", _sMobile));
                cmd.Parameters.Add(new SqlParameter("@AFM", _sAFM));
                cmd.Parameters.Add(new SqlParameter("@DoB", _sDoB));
                cmd.Parameters.Add(new SqlParameter("@Client_ID", _iClient_ID));
                cmd.Parameters.Add(new SqlParameter("@Password", _sPassword));
                drList = cmd.ExecuteReader();

                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["WU_ID"], drList["Manufacturer"], drList["Brand"], drList["Model"], drList["Board"], drList["Hardware"], drList["Unique_ID"],
                                     drList["ScreenResolution"], drList["ScreenDensity"], drList["Host"], drList["Version"], drList["API_level"], drList["Build_ID"],
                                     drList["Build_Time"], drList["Fingerprint"], drList["PhoneType"], drList["NetworkCountryISO"], drList["NetworkOperatorName"], 
                                     drList["DeviceId"], drList["DeviceSoftwareVersion"], drList["SimCountryIso"], drList["SimOperatorName"], drList["SimSerialNumber"], 
                                     drList["Imei"], drList["Meid"], drList["MmsUAProfUrl"], drList["MmsUserAgent"], drList["SubscriberId"], drList["TypeAllocationCode"], 
                                     drList["OS"], drList["Video"], drList["Status"], drList["DateIns"], drList["Client_ID"], drList["CountryCode"], drList["EMail"], 
                                     drList["Mobile"], drList["PhoneCode"], drList["Password"]);
                }
                //_dtList.Load(drList);
            }
            catch (Exception ex) {
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
                using (cmd = new SqlCommand("InsertWebUsersDevices", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@WU_ID", SqlDbType.Int).Value = _iWU_ID;
                    cmd.Parameters.Add("@Manufacturer", SqlDbType.NVarChar, 100).Value = _sManufacturer.Trim();
                    cmd.Parameters.Add("@Brand", SqlDbType.NVarChar, 100).Value = _sBrand.Trim();
                    cmd.Parameters.Add("@Model", SqlDbType.NVarChar, 100).Value = _sModel.Trim();
                    cmd.Parameters.Add("@Board", SqlDbType.NVarChar, 100).Value = _sBoard.Trim();
                    cmd.Parameters.Add("@Hardware", SqlDbType.NVarChar, 100).Value = _sHardware.Trim();
                    cmd.Parameters.Add("@Unique_ID", SqlDbType.NVarChar, 100).Value = _sUnique_ID.Trim();
                    cmd.Parameters.Add("@ScreenResolution", SqlDbType.NVarChar, 100).Value = _sScreenResolution.Trim();
                    cmd.Parameters.Add("@ScreenDensity", SqlDbType.NVarChar, 100).Value = _sScreenDensity.Trim();
                    cmd.Parameters.Add("@Host", SqlDbType.NVarChar, 100).Value = _sHost.Trim();
                    cmd.Parameters.Add("@Version", SqlDbType.NVarChar, 100).Value = _sVersion.Trim();
                    cmd.Parameters.Add("@API_level", SqlDbType.NVarChar, 100).Value = _sAPI_level;
                    cmd.Parameters.Add("@Build_ID", SqlDbType.NVarChar, 100).Value = _sBuild_ID;
                    cmd.Parameters.Add("@Build_Time", SqlDbType.NVarChar, 100).Value = _sBuild_Time;
                    cmd.Parameters.Add("@Fingerprint", SqlDbType.NVarChar, 100).Value = _sFingerprint.Trim();
                    cmd.Parameters.Add("@PhoneType", SqlDbType.NVarChar, 100).Value = _sPhoneType.Trim();
                    cmd.Parameters.Add("@NetworkCountryISO", SqlDbType.NVarChar, 100).Value = _sNetworkCountryISO.Trim();
                    cmd.Parameters.Add("@NetworkOperatorName", SqlDbType.NVarChar, 100).Value = _sNetworkOperatorName.Trim();
                    cmd.Parameters.Add("@DeviceId", SqlDbType.NVarChar, 100).Value = _sDeviceId.Trim();
                    cmd.Parameters.Add("@DeviceSoftwareVersion", SqlDbType.NVarChar, 100).Value = _sDeviceSoftwareVersion.Trim();
                    cmd.Parameters.Add("@SimCountryIso", SqlDbType.NVarChar, 100).Value = _sSimCountryIso.Trim();
                    cmd.Parameters.Add("@SimOperatorName", SqlDbType.NVarChar, 100).Value = _sSimOperatorName.Trim();
                    cmd.Parameters.Add("@SimSerialNumber", SqlDbType.NVarChar, 100).Value = _sSimSerialNumber.Trim();
                    cmd.Parameters.Add("@Imei", SqlDbType.NVarChar, 100).Value = _sImei.Trim();
                    cmd.Parameters.Add("@Meid", SqlDbType.NVarChar, 100).Value = _sMeid.Trim();
                    cmd.Parameters.Add("@MmsUAProfUrl", SqlDbType.NVarChar, 100).Value = _sMmsUAProfUrl.Trim();
                    cmd.Parameters.Add("@MmsUserAgent", SqlDbType.NVarChar, 100).Value = _sMmsUserAgent.Trim();
                    cmd.Parameters.Add("@SubscriberId", SqlDbType.NVarChar, 100).Value = _sSubscriberId.Trim();
                    cmd.Parameters.Add("@TypeAllocationCode", SqlDbType.NVarChar, 100).Value = _sTypeAllocationCode.Trim();
                    cmd.Parameters.Add("@OS", SqlDbType.NVarChar, 100).Value = _sOS.Trim();
                    cmd.Parameters.Add("@Video", SqlDbType.NVarChar, 100).Value = _sVideo.Trim();
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.ExecuteNonQuery();
                    _iRecord_ID = Convert.ToInt32(outParam.Value);
                }
            }
            catch (Exception ex) {
                string sTemp = ex.Message;
            }
            finally { conn.Close(); }

            return _iRecord_ID;
        }
        public void EditRecord()
        {
            try
            {
                conn = new SqlConnection(Global.connStr);
                conn.Open();
                using (cmd = new SqlCommand("EditWebUsersDevices", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@WU_ID", SqlDbType.Int).Value = _iWU_ID;
                    cmd.Parameters.Add("@Manufacturer", SqlDbType.NVarChar, 100).Value = _sManufacturer.Trim();
                    cmd.Parameters.Add("@Brand", SqlDbType.NVarChar, 100).Value = _sBrand.Trim();
                    cmd.Parameters.Add("@Model", SqlDbType.NVarChar, 100).Value = _sModel.Trim();
                    cmd.Parameters.Add("@Board", SqlDbType.NVarChar, 100).Value = _sBoard.Trim();
                    cmd.Parameters.Add("@Hardware", SqlDbType.NVarChar, 100).Value = _sHardware.Trim();
                    cmd.Parameters.Add("@Unique_ID", SqlDbType.NVarChar, 100).Value = _sUnique_ID.Trim();
                    cmd.Parameters.Add("@ScreenResolution", SqlDbType.NVarChar, 100).Value = _sScreenResolution.Trim();
                    cmd.Parameters.Add("@ScreenDensity", SqlDbType.NVarChar, 100).Value = _sScreenDensity.Trim();
                    cmd.Parameters.Add("@Host", SqlDbType.NVarChar, 100).Value = _sHost.Trim();
                    cmd.Parameters.Add("@Version", SqlDbType.NVarChar, 100).Value = _sVersion.Trim();
                    cmd.Parameters.Add("@API_level", SqlDbType.NVarChar, 100).Value = _sAPI_level;
                    cmd.Parameters.Add("@Build_ID", SqlDbType.NVarChar, 100).Value = _sBuild_ID;
                    cmd.Parameters.Add("@Build_Time", SqlDbType.NVarChar, 100).Value = _sBuild_Time;
                    cmd.Parameters.Add("@Fingerprint", SqlDbType.NVarChar, 100).Value = _sFingerprint.Trim();
                    cmd.Parameters.Add("@PhoneType", SqlDbType.NVarChar, 100).Value = _sPhoneType.Trim();
                    cmd.Parameters.Add("@NetworkCountryISO", SqlDbType.NVarChar, 100).Value = _sNetworkCountryISO.Trim();
                    cmd.Parameters.Add("@NetworkOperatorName", SqlDbType.NVarChar, 100).Value = _sNetworkOperatorName.Trim();
                    cmd.Parameters.Add("@DeviceId", SqlDbType.NVarChar, 100).Value = _sDeviceId.Trim();
                    cmd.Parameters.Add("@DeviceSoftwareVersion", SqlDbType.NVarChar, 100).Value = _sDeviceSoftwareVersion.Trim();
                    cmd.Parameters.Add("@SimCountryIso", SqlDbType.NVarChar, 100).Value = _sSimCountryIso.Trim();
                    cmd.Parameters.Add("@SimOperatorName", SqlDbType.NVarChar, 100).Value = _sSimOperatorName.Trim();
                    cmd.Parameters.Add("@SimSerialNumber", SqlDbType.NVarChar, 100).Value = _sSimSerialNumber.Trim();
                    cmd.Parameters.Add("@Imei", SqlDbType.NVarChar, 100).Value = _sImei.Trim();                    
                    cmd.Parameters.Add("@Meid", SqlDbType.NVarChar, 100).Value = _sMeid.Trim();
                    cmd.Parameters.Add("@MmsUAProfUrl", SqlDbType.NVarChar, 100).Value = _sMmsUAProfUrl.Trim();
                    cmd.Parameters.Add("@MmsUserAgent", SqlDbType.NVarChar, 100).Value = _sMmsUserAgent.Trim();
                    cmd.Parameters.Add("@SubscriberId", SqlDbType.NVarChar, 100).Value = _sSubscriberId.Trim();
                    cmd.Parameters.Add("@TypeAllocationCode", SqlDbType.NVarChar, 100).Value = _sTypeAllocationCode.Trim();
                    cmd.Parameters.Add("@OS", SqlDbType.NVarChar, 100).Value = _sOS.Trim();
                    cmd.Parameters.Add("@Video", SqlDbType.NVarChar, 100).Value = _sVideo.Trim();
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;                  
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string sTemp = ex.Message;
            }
            finally { conn.Close(); }
        }
        public int EditStatus()
        {
            int iResult = 0;
            if (_iStatus == 1 || _iStatus == 2)
            {
                try
                {
                    conn = new SqlConnection(Global.connStr);
                    conn.Open();
                    using (cmd = new SqlCommand("EditWebUsersDevices_Status", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                        cmd.Parameters.Add("@Unique_ID", SqlDbType.NVarChar, 100).Value = _sUnique_ID.Trim();
                        cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    iResult = 0;
                    string sTemp = ex.Message;
                }
                finally { conn.Close(); iResult = _iStatus; }
            }

            return iResult;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "WebUsersDevices";
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
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int WU_ID { get { return this._iWU_ID; } set { this._iWU_ID = value; } }
        public string Manufacturer { get { return _sManufacturer; } set { _sManufacturer = value; } }
        public string Brand { get { return _sBrand; } set { _sBrand = value; } }
        public string Model { get { return _sModel; } set { _sModel = value; } }
        public string Board { get { return _sBoard; } set { _sBoard = value; } }
        public string Hardware { get { return _sHardware; } set { _sHardware = value; } }
        public string Unique_ID { get { return _sUnique_ID; } set { _sUnique_ID = value; } }
        public string ScreenResolution { get { return _sScreenResolution; } set { _sScreenResolution = value; } }
        public string ScreenDensity { get { return _sScreenDensity; } set { _sScreenDensity = value; } }
        public string Host { get { return _sHost; } set { _sHost = value; } }
        public string Version { get { return _sVersion; } set { _sVersion = value; } }
        public string API_level { get { return this._sAPI_level; } set { this._sAPI_level = value; } }
        public string Build_ID { get { return this._sBuild_ID; } set { this._sBuild_ID = value; } }
        public string Build_Time { get { return this._sBuild_Time; } set { this._sBuild_Time = value; } }
        public string Fingerprint { get { return _sFingerprint; } set { _sFingerprint = value; } }
        public string PhoneType { get { return _sPhoneType; } set { _sPhoneType = value; } }
        public string NetworkCountryISO { get { return _sNetworkCountryISO; } set { _sNetworkCountryISO = value; } }
        public string NetworkOperatorName { get { return _sNetworkOperatorName; } set { _sNetworkOperatorName = value; } }
        public string DeviceId { get { return _sDeviceId; } set { _sDeviceId = value; } }
        public string DeviceSoftwareVersion { get { return _sDeviceSoftwareVersion; } set { _sDeviceSoftwareVersion = value; } }
        public string SimCountryIso { get { return _sSimCountryIso; } set { _sSimCountryIso = value; } }
        public string SimOperatorName { get { return _sSimOperatorName; } set { _sSimOperatorName = value; } }
        public string SimSerialNumber { get { return _sSimSerialNumber; } set { _sSimSerialNumber = value; } }
        public string Imei { get { return _sImei; } set { _sImei = value; } }
        public string Meid { get { return _sMeid; } set { _sMeid = value; } }
        public string MmsUAProfUrl { get { return _sMmsUAProfUrl; } set { _sMmsUAProfUrl = value; } }
        public string MmsUserAgent { get { return _sMmsUserAgent; } set { _sMmsUserAgent = value; } }
        public string SubscriberId { get { return _sSubscriberId; } set { _sSubscriberId = value; } }
        public string TypeAllocationCode { get { return _sTypeAllocationCode; } set { _sTypeAllocationCode = value; } }
        public string OS { get { return _sOS; } set { _sOS = value; } }
        public string Video { get { return _sVideo; } set { _sVideo = value; } }       
        public int Status { get { return this._iStatus; } set { this._iStatus = value; } }
        public DateTime DateIns { get { return this._dDateIns; } set { this._dDateIns = value; } }

        public string EMail { get { return _sEMail; } set { _sEMail = value; } }
        public string Mobile { get { return _sMobile; } set { _sMobile = value; } }
        public string AFM { get { return _sAFM; } set { _sAFM = value; } }
        public string DoB { get { return _sDoB; } set { _sDoB = value; } }
        public int Client_ID { get { return this._iClient_ID; } set { this._iClient_ID = value; } }
        public string Password { get { return _sPassword; } set { _sPassword = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
