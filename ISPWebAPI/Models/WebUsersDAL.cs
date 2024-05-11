using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Core;

namespace ISPWebAPI.Models
{
    public class WebUsersDAL
    {
        int _iClient_ID = 0, _WU_ID = 0;
        SqlConnection conn;

        //--- ADD the record into WebUsers table --------------------------   
        public Interface AddRecord(string connectionString, Interface oInterface)
        {
            string sTemp = "";
            _iClient_ID = 0;

            //Global.connStr = connectionString;
            conn = new SqlConnection(Global.connStr);

            if (oInterface.AFM + "" != "")
            {
                clsClients Clients = new clsClients();
                Clients.AFM = oInterface.AFM + "";
                Clients.GetRecord();
                if (Clients.Record_ID == 0)
                {
                    Clients.Type = 1;
                    Clients.Surname = (oInterface.Surname + "").ToUpper();
                    Clients.Firstname = (oInterface.Firstname + "").ToUpper();
                    Clients.AFM = oInterface.AFM + "";
                    Clients.EMail = (oInterface.EMail + "").ToLower();
                    Clients.Mobile = oInterface.Mobile_phone + "";
                    Clients.Status = -1;                                                        // -1 - Ypopsifios Pelatis
                    Clients.BlockStatus = 0;
                    _iClient_ID = Clients.InsertRecord();

                    //--- edit LastEdit_Time for Clients table -----------------
                    clsCashTables CashTable = new clsCashTables();
                    CashTable.CashTables_ID = 1;                                               // ListsTables.CashTables_ID = 1 - Clients
                    CashTable.LastEdit_Time = DateTime.Now;
                    CashTable.LastEdit_User_ID = 45;                                            // 45 - ISP - MobileApp
                    CashTable.Edit_LastEdit_Time();

                    //--- define DocFilesPath_Win value-----------------
                    clsOptions Options = new clsOptions();
                    Options.GetRecord();
                    sTemp = Options.DocFilesPath_Win;

                    //--- create client's folder 
                    ServerJobs oServerJobs = new ServerJobs();
                    oServerJobs.JobType_ID = 11;
                    oServerJobs.Source_ID = 0;
                    oServerJobs.Parameters = "{'folder_name': '" + sTemp + "/Customers/" + (Clients.Surname + " " + Clients.Firstname).Trim().Replace(".", "_") + "'}";
                    oServerJobs.DateStart = DateTime.Now;
                    oServerJobs.DateFinish = Convert.ToDateTime("1900/01/01");
                    oServerJobs.PubKey = "";
                    oServerJobs.PrvKey = "";
                    oServerJobs.Attempt = 0;
                    oServerJobs.Status = 0;
                    ServerJobsDAL oServerJobsDAL = new ServerJobsDAL();
                    oServerJobsDAL.AddServerJob(connectionString, oServerJobs);
                }
                else
                    _iClient_ID = Clients.Record_ID;
            }

            clsWebUsers WebUsers = new clsWebUsers();
            WebUsers.Client_ID = _iClient_ID;
            WebUsers.Password = oInterface.Password + "";
            WebUsers.Status = 1;
            WebUsers.DateIns = DateTime.Now; 
            _WU_ID = WebUsers.InsertRecord();

            oInterface.WU_ID = _WU_ID;
            oInterface.Client_ID = _iClient_ID;

            return oInterface;
        }
        //--- UPDATE Status of the record into WebUsers table ---------------------------------------
        public int UpdateStatus(string connectionString, int iID, int iStatus)
        {
            int iResult = 0;

            Global.connStr = connectionString;
            conn = new SqlConnection(Global.connStr);

            clsWebUsers WebUsers = new clsWebUsers();
            WebUsers.Record_ID = iID;
            WebUsers.GetRecord();
            WebUsers.Status = iStatus;
            iResult = WebUsers.EditRecord();

            return iResult;
        }
        //--- UPDATE Password of the record into WebUsers table ---------------------------------------
        public void UpdatePassword(string connectionString, int iID, string sPassword)
        {
            Global.connStr = connectionString;
            conn = new SqlConnection(Global.connStr);

            clsWebUsers WebUsers = new clsWebUsers();
            WebUsers.Record_ID = iID;
            WebUsers.GetRecord();
            WebUsers.Password = sPassword;
            WebUsers.EditRecord();

        }
        //--- UPDATE Client_ID into WebUsers - it will match WebUsers & Clients record -------------------
        public void MatchWUC(string connectionString, int iWU_ID, int iClient_ID)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "UPDATE WebUsers SET Client_ID = " + iClient_ID + " WHERE ID = " + iWU_ID;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        //--- DELETE the record from WebUsers   table ---------------------------------
        public void DeleteRecord(string connectionString, int iID)
        {
            Global.connStr = connectionString;
            clsWebUsers WebUsers = new clsWebUsers();
            WebUsers.Record_ID = iID;
            WebUsers.DeleteRecord();
        }
        //--- GET WebUsers list with e-mail & password ----------------------------------
        public IEnumerable<WebUsers> GetAllWebUsers(string connectionString, string sEMail, string sPassword)
        {
            Global.connStr = connectionString;

            List<WebUsers> lstWebUsers = new List<WebUsers>();

            clsWebUsers WebUsers = new clsWebUsers();
            WebUsers.EMail = sEMail;
            WebUsers.Password = sPassword;
            WebUsers.GetList();
            foreach (DataRow dtRow in WebUsers.List.Rows)
            {
                WebUsers WebUser = new WebUsers();

                WebUser.ID = Convert.ToInt32((dtRow["ID"]));
                WebUser.Password = dtRow["Password"].ToString();
                WebUser.Surname = dtRow["Surname"].ToString();
                WebUser.Firstname = dtRow["Firstname"].ToString();
                WebUser.DoB = (DateTime)dtRow["DoB"];
                WebUser.BornPlace = dtRow["BornPlace"].ToString();
                WebUser.ADT = dtRow["ADT"].ToString();
                WebUser.ExpireDate = dtRow["ExpireDate"].ToString();
                WebUser.Police = dtRow["Police"].ToString();
                WebUser.DOY = dtRow["DOY"].ToString();
                WebUser.AFM = dtRow["AFM"].ToString();
                WebUser.Address = dtRow["Address"].ToString();
                WebUser.City = dtRow["City"].ToString();
                WebUser.Zip = dtRow["Zip"].ToString();
                WebUser.Country_ID = Convert.ToInt32((dtRow["Country_ID"]));
                WebUser.Tel = dtRow["Tel"].ToString();
                WebUser.Mobile = dtRow["Mobile"].ToString();
                WebUser.EMail = dtRow["EMail"].ToString();
                WebUser.Client_ID = Convert.ToInt32(dtRow["Client_ID"]);
                WebUser.Status = Convert.ToInt16((dtRow["Status"]));

                lstWebUsers.Add(WebUser);
            } 

            return lstWebUsers;
        }  

 
        //--- GET WebUsers.ID with any critiries ------------------------------------------------------- 
        public int GetRecord_ID(string connectionString, string sParameters)
        {
            Global.connStr = connectionString;

            WebUsers WebUser = new WebUsers();
            WebUser.ID = 0;
            var WebUserData = JsonConvert.DeserializeObject<WebUsers>(sParameters);
           
            clsWebUsers WebUsers = new clsWebUsers();
            WebUsers.EMail = WebUserData.EMail;
            WebUsers.Mobile = WebUserData.Mobile;
            WebUsers.AFM = WebUserData.AFM;
            WebUsers.DoB = WebUserData.DoB.ToString("dd/MM/yyyy");
            WebUsers.Client_ID = WebUserData.Client_ID;
            WebUsers.Password = WebUserData.Password;
            WebUsers.GetList();
            foreach (DataRow dtRow in WebUsers.List.Rows)
                if (Global.IsNumeric(dtRow["ID"])) WebUser.ID = Convert.ToInt32(dtRow["ID"]);
                else WebUser.ID = 0;

            return WebUser.ID;
        }
        //--- GET WebUsers data with any critiries ------------------------------------------------------- 
        public WebUsers GetRecord_Data(string connectionString, string sParameters)
        {
            Global.connStr = connectionString;

            WebUsers WebUser = new WebUsers();
            WebUser.ID = 0;
            var WebUserData = JsonConvert.DeserializeObject<WebUsers>(sParameters);

            clsWebUsers WebUsers = new clsWebUsers();
            WebUsers.EMail = WebUserData.EMail;
            WebUsers.Mobile = WebUserData.Mobile;
            WebUsers.AFM = WebUserData.AFM;
            WebUsers.DoB = WebUserData.DoB.ToString("dd/MM/yyyy");
            WebUsers.Client_ID = WebUserData.Client_ID;
            WebUsers.Password = WebUserData.Password;
            WebUsers.GetList();
            foreach (DataRow dtRow in WebUsers.List.Rows)
            {
                WebUser.ID = Global.IsNumeric(dtRow["ID"]) ? Convert.ToInt32(dtRow["ID"]) : 0;
                WebUser.Client_ID = Global.IsNumeric(dtRow["Klient_ID"]) ? Convert.ToInt32(dtRow["Klient_ID"]) : 0;
                WebUser.Password = dtRow["Password"].ToString();
                WebUser.Surname = dtRow["Surname"].ToString();
                WebUser.Firstname = dtRow["Firstname"].ToString();
                if (dtRow["DoB"] + "" == "") WebUser.DoB = Convert.ToDateTime("1900/01/01");
                else WebUser.DoB = Convert.ToDateTime(dtRow["DoB"]+"");
                WebUser.BornPlace = dtRow["BornPlace"].ToString();
                WebUser.ADT = dtRow["ADT"].ToString();
                WebUser.ExpireDate = dtRow["ExpireDate"].ToString();
                WebUser.Police = dtRow["Police"].ToString();
                WebUser.DOY = dtRow["DOY"].ToString();
                WebUser.AFM = dtRow["AFM"].ToString();
                WebUser.Address = dtRow["Address"].ToString();
                WebUser.City = dtRow["City"].ToString();
                WebUser.Zip = dtRow["Zip"].ToString();
                if (Global.IsNumeric(dtRow["Country_ID"])) WebUser.Country_ID = Convert.ToInt32((dtRow["Country_ID"]));
                else WebUser.Country_ID = 0;
                WebUser.Tel = dtRow["Tel"].ToString();                
                WebUser.EMail = dtRow["EMail"].ToString();
                WebUser.Mobile = dtRow["Mobile"].ToString();
                WebUser.PhoneCode = dtRow["PhoneCode"].ToString();
                WebUser.DateIns = Convert.ToDateTime(dtRow["DateIns"]);
                WebUser.Client_DateIns = Convert.ToDateTime(dtRow["Klient_DateIns"]);
            }

            return WebUser;
        }
    }
}
