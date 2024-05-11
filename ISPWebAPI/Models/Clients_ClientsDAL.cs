using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Core;

namespace ISPWebAPI.Models
{
    public class Clients_ClientsDAL
    {
        clsClients_Clients Clients_Clients = new clsClients_Clients();
        SqlConnection conn;

        //--- ADD the record into Clients_Clients table --------------------------   
        public Interface AddRecord(string connectionString, Interface oInterface)
        {

            Global.connStr = connectionString;
            conn = new SqlConnection(Global.connStr);

            Clients_Clients = new clsClients_Clients();
            Clients_Clients.Client_ID = oInterface.Client_ID;
            Clients_Clients.Client2_ID = oInterface.Client2_ID;
            Clients_Clients.Status = 0;
            oInterface.ID = Clients_Clients.InsertRecord();

            return oInterface;
        }
        //--- DELETE the record from Clients_Clients   table ---------------------------------
        public int UpdateStatus(string connectionString, int iID, int iStatus)
        {
            Global.connStr = connectionString;
            conn = new SqlConnection(Global.connStr);

            Clients_Clients = new clsClients_Clients();
            Clients_Clients.Record_ID = iID;
            Clients_Clients.GetRecord();
            Clients_Clients.Status = iStatus;
            Clients_Clients.EditRecord();

            return iID;
        }
        //--- DELETE the record from Clients_Clients   table ---------------------------------
        public void DeleteRecord(string connectionString, int iID)
        {
            Global.connStr = connectionString;
            conn = new SqlConnection(Global.connStr);

            Clients_Clients = new clsClients_Clients();
            Clients_Clients.Record_ID = iID;
            Clients_Clients.DeleteRecord();
        }
        //--- GET Clients_Clients list with e-mail & password ----------------------------------
        public IEnumerable<Clients_Clients> GetClients_Clients(string connectionString, int _iClient_ID)
        {
            Global.connStr = connectionString;

            List<Clients_Clients> lstClients_Clients = new List<Clients_Clients>();
            Clients_Clients Clients_Clients = new Clients_Clients();

            clsClients_Clients oClients_Clients = new clsClients_Clients();
            oClients_Clients.Client_ID = _iClient_ID;
            oClients_Clients.GetList();
            foreach (DataRow dtRow in oClients_Clients.List.Rows)
            {
                Clients_Clients = new Clients_Clients();

                Clients_Clients.ID = Convert.ToInt32((dtRow["ID"]));
                Clients_Clients.Client_ID = Convert.ToInt32(dtRow["Client_ID"]);
                Clients_Clients.Client_Name = dtRow["Client_Fullname"].ToString();
                Clients_Clients.Client_AFM = dtRow["Client_AFM"].ToString();
                Clients_Clients.Client_Email = dtRow["Client_Email"].ToString();
                Clients_Clients.Client2_ID = Convert.ToInt32(dtRow["Client2_ID"]);
                Clients_Clients.Client2_Name = dtRow["Client2_Fullname"].ToString();
                Clients_Clients.Client2_AFM = dtRow["Client2_AFM"].ToString();
                Clients_Clients.Client2_DoB = Convert.ToDateTime(dtRow["Client2_DoB"].ToString());
                Clients_Clients.Client2_Email = dtRow["Client2_Email"].ToString();
                Clients_Clients.Status = Convert.ToInt16((dtRow["Status"]));
                Clients_Clients.DateIns = dtRow["DateIns"] + "";

                lstClients_Clients.Add(Clients_Clients);
            }

            return lstClients_Clients;
        }

        //--- GET Clients_Clients.ID with any critiries ------------------------------------------------------- 
        public int GetRecord_ID(string connectionString, string sParameters)
        {
            Global.connStr = connectionString;

            Clients_Clients WebUser = new Clients_Clients();
            WebUser.ID = 0;
            var WebUserData = JsonConvert.DeserializeObject<Clients_Clients>(sParameters);

            clsClients_Clients Clients_Clients = new clsClients_Clients();
           
            Clients_Clients.GetList();
            foreach (DataRow dtRow in Clients_Clients.List.Rows)
                WebUser.ID = Convert.ToInt32(dtRow["ID"]);

            return WebUser.ID;
        }
        //--- GET Clients_Clients data with any critiries ------------------------------------------------------- 
        public Clients_Clients GetRecord_Data(string connectionString, string sParameters)
        {
            Global.connStr = connectionString;

            Clients_Clients WebUser = new Clients_Clients();
            WebUser.ID = 0;
            var WebUserData = JsonConvert.DeserializeObject<Clients_Clients>(sParameters);

            clsClients_Clients Clients_Clients = new clsClients_Clients();

            Clients_Clients.GetList();
            foreach (DataRow dtRow in Clients_Clients.List.Rows)
            {
                WebUser.ID = Convert.ToInt32(dtRow["ID"]);              
            }

            return WebUser;
        }
    }
}
