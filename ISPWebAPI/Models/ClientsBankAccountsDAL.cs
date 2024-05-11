using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Core;

namespace ISPWebAPI.Models
{
    public class ClientsBankAccountsDAL
    {
        clsClients_BankAccounts ClientsBankAccounts = new clsClients_BankAccounts();
        ClientsBankAccounts oClientsBankAccounts = new ClientsBankAccounts();

        public List<ClientsBankAccounts> GetList_Data(string connectionString, string sParameters)
        {
            string sCriteries = " WHERE ClientsBankAccounts.ID > 0 ";

            List<ClientsBankAccounts> lstClientsBankAccounts = new List<ClientsBankAccounts>();

            var AccData = JsonConvert.DeserializeObject<ClientsBankAccounts>(sParameters);

            if (AccData.ID != 0) sCriteries = sCriteries + " AND ClientsBankAccounts.ID = " + AccData.ID;
            if (AccData.Client_ID != 0) sCriteries = sCriteries + " AND ClientsBankAccounts.Client_ID = " + AccData.Client_ID;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT dbo.ClientsBankAccounts.*, dbo.Clients.Surname, dbo.Clients.Firstname, dbo.Banks.Title " +
                                  "FROM   dbo.ClientsBankAccounts LEFT OUTER JOIN dbo.Banks ON dbo.ClientsBankAccounts.Bank_ID = dbo.Banks.ID LEFT OUTER JOIN " +
                                  "dbo.Clients ON dbo.ClientsBankAccounts.Client_ID = dbo.Clients.ID " + sCriteries + " ORDER BY dbo.ClientsBankAccounts.ID ";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ClientsBankAccounts ClientsBankAccounts = new ClientsBankAccounts();

                    ClientsBankAccounts.ID = Convert.ToInt32(rdr["ID"]);
                    ClientsBankAccounts.Client_ID = Convert.ToInt32(rdr["Client_ID"]);
                    ClientsBankAccounts.Bank_ID = Convert.ToInt32(rdr["Bank_ID"]);
                    ClientsBankAccounts.Bank_Title = rdr["Title"].ToString();
                    ClientsBankAccounts.AccNumber = rdr["AccNumber"].ToString();
                    ClientsBankAccounts.AccType = Convert.ToInt32(rdr["AccType"]);
                    ClientsBankAccounts.AccOwners = rdr["AccOwners"].ToString();
                    ClientsBankAccounts.Currency = rdr["Curr"].ToString();
                    ClientsBankAccounts.Status = Convert.ToInt32(rdr["Status"]);
                    lstClientsBankAccounts.Add(ClientsBankAccounts);
                }
                rdr.Close();
                con.Close();
            }
            return lstClientsBankAccounts;
        }
        //--- ADD the record into WebUsers table --------------------------   
        public int AddRecord(string connectionString, ClientsBankAccounts oClientsBankAccounts)
        {
            ClientsBankAccounts.Client_ID = oClientsBankAccounts.Client_ID;
            ClientsBankAccounts.Bank_ID = oClientsBankAccounts.Bank_ID;
            ClientsBankAccounts.AccNumber = oClientsBankAccounts.AccNumber;
            ClientsBankAccounts.AccType = oClientsBankAccounts.AccType;
            ClientsBankAccounts.AccOwners = oClientsBankAccounts.AccOwners;
            ClientsBankAccounts.Currency = oClientsBankAccounts.Currency;
            ClientsBankAccounts.StartBalance = 0;
            ClientsBankAccounts.Status = oClientsBankAccounts.Status;
            oClientsBankAccounts.ID = ClientsBankAccounts.InsertRecord();
            return oClientsBankAccounts.ID;
        }
        //--- EDIT the record from WebUsers   table ---------------------------------
        public int EditRecord(string connectionString, ClientsBankAccounts oClientsBankAccounts)
        {
            ClientsBankAccounts.Record_ID = oClientsBankAccounts.ID;
            ClientsBankAccounts.GetRecord();
            //ClientsBankAccounts.Client_ID = oClientsBankAccounts.Client_ID;
            //ClientsBankAccounts.Bank_ID = oClientsBankAccounts.Bank_ID;
            //ClientsBankAccounts.AccNumber = oClientsBankAccounts.AccNumber;
            //ClientsBankAccounts.AccType = oClientsBankAccounts.AccType;
            //ClientsBankAccounts.AccOwners = oClientsBankAccounts.AccOwners;
            //ClientsBankAccounts.Currency = oClientsBankAccounts.Currency;
            //ClientsBankAccounts.StartBalance = 0;
            ClientsBankAccounts.Status = oClientsBankAccounts.Status;
            oClientsBankAccounts.ID = ClientsBankAccounts.EditRecord();
            return oClientsBankAccounts.ID;
        }
    }
}
