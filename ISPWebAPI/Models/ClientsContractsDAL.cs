using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ISPWebAPI.Models
{
    public class ClientsContractsDAL
    {
        public List<ClientsContracts> GetClientsContractsList(string connectionString, int iClient_ID)
        {
            List<ClientsContracts> lstClientsContracts = new List<ClientsContracts>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetContractsList_Client", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Client_ID", iClient_ID));
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ClientsContracts ClientsContracts = new ClientsContracts();
                    ClientsContracts.ID = Convert.ToInt32((rdr["ID"]));
                    ClientsContracts.Client_ID = Convert.ToInt32((rdr["Client_ID"]));
                    ClientsContracts.Contract_ID = Convert.ToInt32((rdr["Contract_ID"]));
                    ClientsContracts.IsMaster = Convert.ToInt32((rdr["IsMaster"]));
                    ClientsContracts.IsOrder = Convert.ToInt32((rdr["IsOrder"]));
                    ClientsContracts.ClientName = (rdr["Surname"] + " " + rdr["Firstname"]).Trim().ToString();
                    ClientsContracts.Code = rdr["Code"].ToString();                    
                    ClientsContracts.Portfolio = rdr["Portfolio"].ToString();

                    lstClientsContracts.Add(ClientsContracts);
                }
                con.Close();
            }
            return lstClientsContracts;
        }
    }
}
