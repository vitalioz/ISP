using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Core;

namespace ISPWebAPI.Models
{
    public class WebUsersDevicesDAL
    {
        SqlConnection conn;
        int _WUD_ID = 0;
        int iResult = 0;

        //--- ADD the record into WebUsersDevices table ---------------------------------------
        public int AddRecord(string sConnectionString, Interface oInterface)
        {
            clsWebUsersDevices WebUsersDevices = new clsWebUsersDevices();
            WebUsersDevices.WU_ID = oInterface.WU_ID;
            WebUsersDevices.Manufacturer = oInterface.Manufacturer + "";
            WebUsersDevices.Brand = oInterface.Brand + "";
            WebUsersDevices.Model = oInterface.Model + "";
            WebUsersDevices.Board = oInterface.Board + "";
            WebUsersDevices.Hardware = oInterface.Hardware + "";
            WebUsersDevices.Unique_ID = oInterface.Unique_ID + "";
            WebUsersDevices.ScreenResolution = oInterface.ScreenResolution + "";
            WebUsersDevices.ScreenDensity = oInterface.ScreenDensity + "";
            WebUsersDevices.Host = oInterface.Host + "";
            WebUsersDevices.Version = oInterface.Version + "";
            WebUsersDevices.API_level = oInterface.API_level + "";
            WebUsersDevices.Build_ID = oInterface.Build_ID + "";
            WebUsersDevices.Build_Time = oInterface.Build_Time + "";
            WebUsersDevices.Fingerprint = oInterface.Fingerprint + "";
            WebUsersDevices.PhoneType = oInterface.PhoneType + "";
            WebUsersDevices.NetworkCountryISO = oInterface.NetworkCountryISO + "";
            WebUsersDevices.NetworkOperatorName = oInterface.NetworkOperatorName + "";
            WebUsersDevices.DeviceId = oInterface.DeviceId + "";
            WebUsersDevices.DeviceSoftwareVersion = oInterface.DeviceSoftwareVersion + "";
            WebUsersDevices.SimCountryIso = oInterface.SimCountryIso + "";
            WebUsersDevices.SimOperatorName = oInterface.SimOperatorName + "";
            WebUsersDevices.SimSerialNumber = oInterface.SimSerialNumber + "";
            WebUsersDevices.Imei = oInterface.Imei + "";
            WebUsersDevices.Meid = oInterface.Meid + "";
            WebUsersDevices.MmsUAProfUrl = oInterface.MmsUAProfUrl + "";
            WebUsersDevices.MmsUserAgent = oInterface.MmsUserAgent + "";
            WebUsersDevices.SubscriberId = oInterface.SubscriberId + "";
            WebUsersDevices.TypeAllocationCode = oInterface.TypeAllocationCode + "";
            WebUsersDevices.OS = oInterface.OS + "";
            WebUsersDevices.Video = oInterface.Video + "";
            WebUsersDevices.Status = oInterface.Status;
            WebUsersDevices.DateIns = DateTime.Now;
            _WUD_ID = WebUsersDevices.InsertRecord();

            return _WUD_ID;
        }
        //--- UPDATE WU_ID of the record into WebUsersDevices table ---------------------------------------
        public void UpdateWU_ID(string connectionString, int iID, int iWU_ID)
        {
            Global.connStr = connectionString;
            conn = new SqlConnection(Global.connStr);

            clsWebUsersDevices WebUsersDevices = new clsWebUsersDevices();
            WebUsersDevices.Record_ID = iID;
            WebUsersDevices.GetRecord();
            WebUsersDevices.WU_ID = iWU_ID;
            WebUsersDevices.EditRecord();
        }

        //--- UPDATE the record into WebUsersDevices table ---------------------------------------
        public void UpdateRecord(string connectionString, Interface oInterface)
        {
            clsWebUsersDevices WebUsersDevices = new clsWebUsersDevices();
            WebUsersDevices.Record_ID = oInterface.ID;
            WebUsersDevices.GetRecord();

            WebUsersDevices.WU_ID = oInterface.WU_ID;
            WebUsersDevices.Manufacturer = oInterface.Manufacturer + "";
            WebUsersDevices.Brand = oInterface.Brand + "";
            WebUsersDevices.Model = oInterface.Model + "";
            WebUsersDevices.Board = oInterface.Board + "";
            WebUsersDevices.Hardware = oInterface.Hardware + "";
            WebUsersDevices.Unique_ID = oInterface.Unique_ID + "";
            WebUsersDevices.ScreenResolution = oInterface.ScreenResolution + "";
            WebUsersDevices.ScreenDensity = oInterface.ScreenDensity + "";
            WebUsersDevices.Host = oInterface.Host + "";
            WebUsersDevices.Version = oInterface.Version + "";
            WebUsersDevices.API_level = oInterface.API_level + "";
            WebUsersDevices.Build_ID = oInterface.Build_ID + "";
            WebUsersDevices.Build_Time = oInterface.Build_Time + "";
            WebUsersDevices.Fingerprint = oInterface.Fingerprint + "";
            WebUsersDevices.PhoneType = oInterface.PhoneType + "";
            WebUsersDevices.NetworkCountryISO = oInterface.NetworkCountryISO + "";
            WebUsersDevices.NetworkOperatorName = oInterface.NetworkOperatorName + "";
            WebUsersDevices.DeviceId = oInterface.DeviceId + "";
            WebUsersDevices.DeviceSoftwareVersion = oInterface.DeviceSoftwareVersion + "";
            WebUsersDevices.SimCountryIso = oInterface.SimCountryIso + "";
            WebUsersDevices.SimOperatorName = oInterface.SimOperatorName + "";
            WebUsersDevices.SimSerialNumber = oInterface.SimSerialNumber + "";
            WebUsersDevices.Imei = oInterface.Imei + "";
            WebUsersDevices.Meid = oInterface.Meid + "";
            WebUsersDevices.MmsUAProfUrl = oInterface.MmsUAProfUrl + "";
            WebUsersDevices.MmsUserAgent = oInterface.MmsUserAgent + "";
            WebUsersDevices.SubscriberId = oInterface.SubscriberId + "";
            WebUsersDevices.TypeAllocationCode = oInterface.TypeAllocationCode + "";
            WebUsersDevices.OS = oInterface.OS + "";
            WebUsersDevices.Video = oInterface.Video + "";
            WebUsersDevices.Status = oInterface.Status;
            WebUsersDevices.EditRecord();
        }
        //--- UPDATE Status of the record into WebUsersDevices table ---------------------------------------
        public int UpdateStatus(string connectionString, string sID, string sUnique_ID, int iStatus)
        {
            iResult = 0;
            Global.connStr = connectionString;
            conn = new SqlConnection(Global.connStr);

            clsWebUsersDevices WebUsersDevices = new clsWebUsersDevices();
            WebUsersDevices.Record_ID = Global.IsNumeric(sID+"")?  Convert.ToInt32(sID) : 0;
            WebUsersDevices.Unique_ID = sUnique_ID;
            WebUsersDevices.Status = iStatus;
            iResult = WebUsersDevices.EditStatus();

            return iResult;
        }
       
        //--- DELETE the record from WebUsersDevices ---------------------------------------------------------
        public void DeleteRecord(string connectionString, int iID)
        {
            Global.connStr = connectionString;
            conn = new SqlConnection(Global.connStr);

            clsWebUsersDevices WebUsersDevices = new clsWebUsersDevices();
            WebUsersDevices.Record_ID = iID;
            WebUsersDevices.DeleteRecord();
        }
        public WebUsersDevices GetRecord(string connectionString, int id)
        {            
            WebUsersDevices WebUsersDevices = new WebUsersDevices();

            string sqlQuery = "SELECT * FROM dbo.WebUsersDevices WHERE WebUsersDevices.ID = " + id;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    WebUsersDevices.ID = Convert.ToInt32(rdr["ID"]);
                    WebUsersDevices.WU_ID = Convert.ToInt32(rdr["WU_ID"]);
                    WebUsersDevices.Manufacturer = rdr["Manufacturer"].ToString();
                    WebUsersDevices.Brand = rdr["Brand"].ToString();
                    WebUsersDevices.Model = rdr["Model"].ToString();
                    WebUsersDevices.Board = rdr["Board"].ToString();
                    WebUsersDevices.Hardware = rdr["Hardware"].ToString();
                    WebUsersDevices.Unique_ID = rdr["Unique_ID"].ToString();
                    WebUsersDevices.ScreenResolution = rdr["ScreenResolution"].ToString();
                    WebUsersDevices.ScreenDensity = rdr["ScreenDensity"].ToString();
                    WebUsersDevices.Host = rdr["Host"].ToString();
                    WebUsersDevices.Version = rdr["Version"].ToString();
                    WebUsersDevices.API_level = rdr["API_level"].ToString();
                    WebUsersDevices.Build_ID = rdr["Build_ID"].ToString();
                    WebUsersDevices.Build_Time = rdr["Build_Time"].ToString();
                    WebUsersDevices.Fingerprint = rdr["Fingerprint"].ToString();
                    WebUsersDevices.PhoneType = rdr["PhoneType"].ToString();
                    WebUsersDevices.NetworkCountryISO = rdr["NetworkCountryISO"].ToString();
                    WebUsersDevices.NetworkOperatorName = rdr["NetworkOperatorName"].ToString();
                    WebUsersDevices.DeviceId = rdr["DeviceId"].ToString();
                    WebUsersDevices.DeviceSoftwareVersion = rdr["DeviceSoftwareVersion"].ToString();
                    WebUsersDevices.SimCountryIso = rdr["SimCountryIso"].ToString();
                    WebUsersDevices.SimOperatorName = rdr["SimOperatorName"].ToString();
                    WebUsersDevices.SimSerialNumber = rdr["SimSerialNumber"].ToString();
                    WebUsersDevices.Imei = rdr["Imei"].ToString();
                    WebUsersDevices.Meid = rdr["Meid"].ToString();
                    WebUsersDevices.MmsUAProfUrl = rdr["MmsUAProfUrl"].ToString();
                    WebUsersDevices.MmsUserAgent = rdr["MmsUserAgent"].ToString();
                    WebUsersDevices.SubscriberId = rdr["SubscriberId"].ToString();
                    WebUsersDevices.TypeAllocationCode = rdr["TypeAllocationCode"].ToString();
                    WebUsersDevices.OS = rdr["OS"].ToString();
                    WebUsersDevices.Video = rdr["Video"].ToString();
                    WebUsersDevices.Status = Convert.ToInt16((rdr["Status"]));
                }
                rdr.Close();
                con.Close();
            }             
            return WebUsersDevices;
        }
        //--- GET the record from WebUsersDevices table --------------------------------------- 
        public int GetRecord_ID(string connectionString, string sParameters)
        {
            Global.connStr = connectionString;

            WebUsersDevices WebUsersDevices = new WebUsersDevices();
            WebUsersDevices.ID = 0;
            var WebUserData = JsonConvert.DeserializeObject<WebUsers>(sParameters);

            clsWebUsersDevices WebUsersDevice = new clsWebUsersDevices();
            WebUsersDevice.Record_ID = WebUserData.WUD_ID;
            WebUsersDevice.WU_ID = WebUserData.ID;
            WebUsersDevice.EMail = WebUserData.EMail;
            WebUsersDevice.Mobile = WebUserData.Mobile;
            WebUsersDevice.AFM = WebUserData.AFM;
            WebUsersDevice.DoB = WebUserData.DoB.ToString("yyyy/MM/dd");
            WebUsersDevice.Client_ID = WebUserData.Client_ID;
            WebUsersDevice.Password = WebUserData.Password;
            WebUsersDevice.GetList();
            foreach (DataRow dtRow in WebUsersDevice.List.Rows)
                WebUsersDevices.ID = Convert.ToInt32(dtRow["ID"]);

            return WebUsersDevices.ID;
        }
        public WebUsersDevices GetRecord_Data(string connectionString, string sParameters)
        {
            Global.connStr = connectionString;

            WebUsersDevices WebUsersDevices = new WebUsersDevices();
            WebUsersDevices.ID = 0;
            var WebUserData = JsonConvert.DeserializeObject<WebUsers>(sParameters);

            clsWebUsersDevices WebUsersDevice = new clsWebUsersDevices();
            WebUsersDevice.Record_ID = WebUserData.WUD_ID;
            WebUsersDevice.WU_ID = WebUserData.ID;
            WebUsersDevice.EMail = WebUserData.EMail;
            WebUsersDevice.Mobile = WebUserData.Mobile;
            WebUsersDevice.AFM = WebUserData.AFM;
            WebUsersDevice.DoB = WebUserData.DoB.ToString("yyyy/MM/dd");
            WebUsersDevice.Client_ID = WebUserData.Client_ID;
            WebUsersDevice.Password = WebUserData.Password;
            WebUsersDevice.GetList();
            foreach (DataRow dtRow in WebUsersDevice.List.Rows)
            {
                WebUsersDevices.ID = Convert.ToInt32(dtRow["ID"]);

                WebUsersDevices.ID = Convert.ToInt32(dtRow["ID"]);
                WebUsersDevices.WU_ID = Convert.ToInt32(dtRow["WU_ID"]);
                WebUsersDevices.Manufacturer = dtRow["Manufacturer"].ToString();
                WebUsersDevices.Brand = dtRow["Brand"].ToString();
                WebUsersDevices.Model = dtRow["Model"].ToString();
                WebUsersDevices.Board = dtRow["Board"].ToString();
                WebUsersDevices.Hardware = dtRow["Hardware"].ToString();
                WebUsersDevices.Unique_ID = dtRow["Unique_ID"].ToString();
                WebUsersDevices.ScreenResolution = dtRow["ScreenResolution"].ToString();
                WebUsersDevices.ScreenDensity = dtRow["ScreenDensity"].ToString();
                WebUsersDevices.Host = dtRow["Host"].ToString();
                WebUsersDevices.Version = dtRow["Version"].ToString();
                WebUsersDevices.API_level = dtRow["API_level"].ToString();
                WebUsersDevices.Build_ID = dtRow["Build_ID"].ToString();
                WebUsersDevices.Build_Time = dtRow["Build_Time"].ToString();
                WebUsersDevices.Fingerprint = dtRow["Fingerprint"].ToString();
                WebUsersDevices.PhoneType = dtRow["PhoneType"].ToString();
                WebUsersDevices.NetworkCountryISO = dtRow["NetworkCountryISO"].ToString();
                WebUsersDevices.NetworkOperatorName = dtRow["NetworkOperatorName"].ToString();
                WebUsersDevices.DeviceId = dtRow["DeviceId"].ToString();
                WebUsersDevices.DeviceSoftwareVersion = dtRow["DeviceSoftwareVersion"].ToString();
                WebUsersDevices.SimCountryIso = dtRow["SimCountryIso"].ToString();
                WebUsersDevices.SimOperatorName = dtRow["SimOperatorName"].ToString();
                WebUsersDevices.SimSerialNumber = dtRow["SimSerialNumber"].ToString();
                WebUsersDevices.Imei = dtRow["Imei"].ToString();
                WebUsersDevices.Meid = dtRow["Meid"].ToString();
                WebUsersDevices.MmsUAProfUrl = dtRow["MmsUAProfUrl"].ToString();
                WebUsersDevices.MmsUserAgent = dtRow["MmsUserAgent"].ToString();
                WebUsersDevices.SubscriberId = dtRow["SubscriberId"].ToString();
                WebUsersDevices.TypeAllocationCode = dtRow["TypeAllocationCode"].ToString();
                WebUsersDevices.OS = dtRow["OS"].ToString();
                WebUsersDevices.Video = dtRow["Video"].ToString();
                WebUsersDevices.Status = Convert.ToInt16((dtRow["Status"]));

                WebUsersDevices.EMail = dtRow["Email"].ToString();
                WebUsersDevices.Mobile = dtRow["Mobile"].ToString();
                WebUsersDevices.Password = dtRow["Password"].ToString();
                WebUsersDevices.Client_ID = Convert.ToInt32(dtRow["Client_ID"]);
                WebUsersDevices.CountryCode = dtRow["CountryCode"].ToString();
                WebUsersDevices.PhoneCode = dtRow["PhoneCode"].ToString();
            }        

            return WebUsersDevices;
        }
        //--- GET the record from WebUsersDevices table --------------------------------------- 
        public List<int> GetList_ID(string connectionString, string sParameters)
        {
            int i = 0;
            List<int> lstID = new List<int>();

            Global.connStr = connectionString;

            WebUsersDevices WebUsersDevices = new WebUsersDevices();
            WebUsersDevices.ID = 0;
            var WebUserData = JsonConvert.DeserializeObject<WebUsers>(sParameters);

            clsWebUsersDevices WebUsersDevice = new clsWebUsersDevices();
            WebUsersDevice.Record_ID = WebUserData.WUD_ID;
            WebUsersDevice.WU_ID = WebUserData.ID;
            WebUsersDevice.EMail = WebUserData.EMail;
            WebUsersDevice.Mobile = WebUserData.Mobile;
            WebUsersDevice.AFM = WebUserData.AFM;
            WebUsersDevice.DoB = WebUserData.DoB.ToString("yyyy/MM/dd");
            WebUsersDevice.Client_ID = WebUserData.Client_ID;
            WebUsersDevice.Password = WebUserData.Password;
            WebUsersDevice.GetList();
            foreach (DataRow dtRow in WebUsersDevice.List.Rows)
            {
                i = Convert.ToInt32(dtRow["ID"]);
                lstID.Add(i);                
            }
            return lstID;
        }
        public List<WebUsersDevices> GetList_Data(string connectionString, string sParameters)
        {
            List<WebUsersDevices> lstWebUsersDevices = new List<WebUsersDevices>();
            var WebUserData = JsonConvert.DeserializeObject<WebUsers>(sParameters);

            Global.connStr = connectionString;

            clsWebUsersDevices WebUsersDevice = new clsWebUsersDevices();
            WebUsersDevice.Record_ID = WebUserData.WUD_ID;
            WebUsersDevice.WU_ID = WebUserData.ID;
            WebUsersDevice.Record_ID = WebUserData.ID;
            WebUsersDevice.EMail = WebUserData.EMail;
            WebUsersDevice.Mobile = WebUserData.Mobile;
            WebUsersDevice.AFM = WebUserData.AFM;
            WebUsersDevice.DoB = WebUserData.DoB.Date.ToString("yyyy/MM/dd");
            WebUsersDevice.Client_ID = WebUserData.Client_ID;
            WebUsersDevice.Password = WebUserData.Password;
            WebUsersDevice.GetList();
            foreach (DataRow dtRow in WebUsersDevice.List.Rows)
            {
                WebUsersDevices WebUsersDevices = new WebUsersDevices();
                WebUsersDevices.ID = Convert.ToInt32(dtRow["ID"]);
                WebUsersDevices.WU_ID = Convert.ToInt32(dtRow["WU_ID"]);
                WebUsersDevices.Manufacturer = dtRow["Manufacturer"].ToString();
                WebUsersDevices.Brand = dtRow["Brand"].ToString();
                WebUsersDevices.Model = dtRow["Model"].ToString();
                WebUsersDevices.Board = dtRow["Board"].ToString();
                WebUsersDevices.Hardware = dtRow["Hardware"].ToString();
                WebUsersDevices.Unique_ID = dtRow["Unique_ID"].ToString();
                WebUsersDevices.ScreenResolution = dtRow["ScreenResolution"].ToString();
                WebUsersDevices.ScreenDensity = dtRow["ScreenDensity"].ToString();
                WebUsersDevices.Host = dtRow["Host"].ToString();
                WebUsersDevices.Version = dtRow["Version"].ToString();
                WebUsersDevices.API_level = dtRow["API_level"].ToString();
                WebUsersDevices.Build_ID = dtRow["Build_ID"].ToString();
                WebUsersDevices.Build_Time = dtRow["Build_Time"].ToString();
                WebUsersDevices.Fingerprint = dtRow["Fingerprint"].ToString();
                WebUsersDevices.PhoneType = dtRow["PhoneType"].ToString();
                WebUsersDevices.NetworkCountryISO = dtRow["NetworkCountryISO"].ToString();
                WebUsersDevices.NetworkOperatorName = dtRow["NetworkOperatorName"].ToString();
                WebUsersDevices.DeviceId = dtRow["DeviceId"].ToString();
                WebUsersDevices.DeviceSoftwareVersion = dtRow["DeviceSoftwareVersion"].ToString();
                WebUsersDevices.SimCountryIso = dtRow["SimCountryIso"].ToString();
                WebUsersDevices.SimOperatorName = dtRow["SimOperatorName"].ToString();
                WebUsersDevices.SimSerialNumber = dtRow["SimSerialNumber"].ToString();
                WebUsersDevices.Imei = dtRow["Imei"].ToString();
                WebUsersDevices.Meid = dtRow["Meid"].ToString();
                WebUsersDevices.MmsUAProfUrl = dtRow["MmsUAProfUrl"].ToString();
                WebUsersDevices.MmsUserAgent = dtRow["MmsUserAgent"].ToString();
                WebUsersDevices.SubscriberId = dtRow["SubscriberId"].ToString();
                WebUsersDevices.TypeAllocationCode = dtRow["TypeAllocationCode"].ToString();
                WebUsersDevices.OS = dtRow["OS"].ToString();
                WebUsersDevices.Video = dtRow["Video"].ToString();
                WebUsersDevices.Status = Convert.ToInt16((dtRow["Status"]));

                WebUsersDevices.EMail = dtRow["EMail"].ToString();
                WebUsersDevices.Mobile = dtRow["Mobile"].ToString();
                WebUsersDevices.Password = dtRow["Password"].ToString();
                WebUsersDevices.Client_ID = Convert.ToInt32(dtRow["Client_ID"]);
                WebUsersDevices.CountryCode = dtRow["CountryCode"].ToString();
                WebUsersDevices.PhoneCode = dtRow["PhoneCode"].ToString();

                lstWebUsersDevices.Add(WebUsersDevices);
            }
            return lstWebUsersDevices;
        }
        public List<WebUsersDevices> GetList_Unique_ID(string connectionString, string sUnique_ID)
        {
            var WebUsersDevice = JsonConvert.DeserializeObject<WebUsersDevices>(sUnique_ID);

            WebUsersDevices WebUsersDevices = new WebUsersDevices();
            List<WebUsersDevices> lstWebUsersDevices = new List<WebUsersDevices>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM dbo.WebUsersDevices WHERE Unique_ID = '" + WebUsersDevice.Unique_ID + "' ORDER BY dbo.WebUsersDevices.ID ";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    WebUsersDevices = new WebUsersDevices();
                    WebUsersDevices.ID = Convert.ToInt32(rdr["ID"]);
                    WebUsersDevices.WU_ID = Convert.ToInt32(rdr["WU_ID"]);
                    WebUsersDevices.Manufacturer = rdr["Manufacturer"].ToString();
                    WebUsersDevices.Brand = rdr["Brand"].ToString();
                    WebUsersDevices.Model = rdr["Model"].ToString();
                    WebUsersDevices.Board = rdr["Board"].ToString();
                    WebUsersDevices.Hardware = rdr["Hardware"].ToString();
                    WebUsersDevices.Unique_ID = rdr["Unique_ID"].ToString();
                    WebUsersDevices.ScreenResolution = rdr["ScreenResolution"].ToString();
                    WebUsersDevices.ScreenDensity = rdr["ScreenDensity"].ToString();
                    WebUsersDevices.Host = rdr["Host"].ToString();
                    WebUsersDevices.Version = rdr["Version"].ToString();
                    WebUsersDevices.API_level = rdr["API_level"].ToString();
                    WebUsersDevices.Build_ID = rdr["Build_ID"].ToString();
                    WebUsersDevices.Build_Time = rdr["Build_Time"].ToString();
                    WebUsersDevices.Fingerprint = rdr["Fingerprint"].ToString();
                    WebUsersDevices.PhoneType = rdr["PhoneType"].ToString();
                    WebUsersDevices.NetworkCountryISO = rdr["NetworkCountryISO"].ToString();
                    WebUsersDevices.NetworkOperatorName = rdr["NetworkOperatorName"].ToString();
                    WebUsersDevices.DeviceId = rdr["DeviceId"].ToString();
                    WebUsersDevices.DeviceSoftwareVersion = rdr["DeviceSoftwareVersion"].ToString();
                    WebUsersDevices.SimCountryIso = rdr["SimCountryIso"].ToString();
                    WebUsersDevices.SimOperatorName = rdr["SimOperatorName"].ToString();
                    WebUsersDevices.SimSerialNumber = rdr["SimSerialNumber"].ToString();
                    WebUsersDevices.Imei = rdr["Imei"].ToString();
                    WebUsersDevices.Meid = rdr["Meid"].ToString();
                    WebUsersDevices.MmsUAProfUrl = rdr["MmsUAProfUrl"].ToString();
                    WebUsersDevices.MmsUserAgent = rdr["MmsUserAgent"].ToString();
                    WebUsersDevices.SubscriberId = rdr["SubscriberId"].ToString();
                    WebUsersDevices.TypeAllocationCode = rdr["TypeAllocationCode"].ToString();
                    WebUsersDevices.OS = rdr["OS"].ToString();
                    WebUsersDevices.Video = rdr["Video"].ToString();
                    WebUsersDevices.Status = Convert.ToInt16((rdr["Status"]));

                    lstWebUsersDevices.Add(WebUsersDevices);
                }
                rdr.Close();
                con.Close();
            }
            return lstWebUsersDevices;
        }
        public int GetWebUsersDevicesStatus(string connectionString, string sUnique_ID)
        {
            int iStatus = 0;
            var WebUsersDevice = JsonConvert.DeserializeObject<WebUsersDevices>(sUnique_ID);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT TOP 1 Status FROM dbo.WebUsersDevices WHERE Unique_ID = '" + WebUsersDevice.Unique_ID + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    iStatus = Convert.ToInt16((rdr["Status"]));
                }
                rdr.Close();
                con.Close();
            }
            return iStatus;
        }
    }
}
