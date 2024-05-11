using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace ISPWebAPI.Models
{
    public class ClientsRequestsDAL
    {
        SqlCommand cmd;
        int _iRecord_ID = 0;

        //--- ADD the record into ClientsRequests table ---------------------------------------
        public int AddRecord(string sConnectionString, ClientsRequests oClientsRequests)
        {
            using (SqlConnection con = new SqlConnection(sConnectionString))
            {
                con.Open();
                cmd = new SqlCommand("InsertClientsRequests", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                outParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outParam);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = oClientsRequests.Client_ID;
                cmd.Parameters.Add("@Group_ID", SqlDbType.NVarChar, 50).Value = oClientsRequests.Group_ID + "";
                cmd.Parameters.Add("@Tipos", SqlDbType.Int).Value = oClientsRequests.Tipos;
                cmd.Parameters.Add("@Aktion", SqlDbType.Int).Value = oClientsRequests.Aktion;
                cmd.Parameters.Add("@Source_ID", SqlDbType.Int).Value = oClientsRequests.Source_ID;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 1000).Value = oClientsRequests.Description + "";
                cmd.Parameters.Add("@Warning", SqlDbType.NVarChar, 500).Value = oClientsRequests.Warning + "";
                cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = oClientsRequests.DateIns;
                cmd.Parameters.Add("@DateWarning", SqlDbType.DateTime).Value = oClientsRequests.DateWarning;
                cmd.Parameters.Add("@DateClose", SqlDbType.DateTime).Value = oClientsRequests.DateClose;
                cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = oClientsRequests.User_ID;
                cmd.Parameters.Add("@Status", SqlDbType.Int).Value = oClientsRequests.Status;
                cmd.Parameters.Add("@VideoChatStatus", SqlDbType.Int).Value = oClientsRequests.VideoChatStatus;
                cmd.Parameters.Add("@VideoChatFile", SqlDbType.NVarChar, 50).Value = ""; // oClientsRequests.VideoChatFile;
                cmd.ExecuteNonQuery();
                _iRecord_ID = Convert.ToInt32(outParam.Value);
            }          
            return _iRecord_ID;
        }


        //--- UPDATE the record into ClientsRequests table ---------------------------------------
        public void UpdateRecord(string connectionString, ClientsRequests oClientsRequests)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("EditClientsRequests", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", oClientsRequests.ID);
                cmd.Parameters.Add("@Group_ID", SqlDbType.NVarChar, 50).Value = oClientsRequests.Group_ID;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 1000).Value = oClientsRequests.Description + "";
                cmd.Parameters.Add("@Warning", SqlDbType.NVarChar, 500).Value = oClientsRequests.Warning+"";
                cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = oClientsRequests.DateIns;
                cmd.Parameters.Add("@DateWarning", SqlDbType.DateTime).Value = oClientsRequests.DateWarning;
                cmd.Parameters.Add("@DateClose", SqlDbType.DateTime).Value = oClientsRequests.DateClose;
                cmd.Parameters.Add("@Status", SqlDbType.Int).Value = oClientsRequests.Status;
                cmd.Parameters.Add("@VideoChatStatus", SqlDbType.Int).Value = oClientsRequests.VideoChatStatus;
                cmd.Parameters.Add("@VideoChatFile", SqlDbType.NVarChar, 50).Value = oClientsRequests.VideoChatFile;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        //--- UPDATE Status of the record into WebUsersDevices table ---------------------------------------
        public void UpdateStatus(string connectionString, int iID, int iStatus)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("EditClientsRequests_Status", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", iID);
                cmd.Parameters.AddWithValue("@Status", iStatus);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        //--- GET the record from ClientsRequests table --------------------------------------- 
        public ClientsRequests GetRecord_Data(string connectionString, int iRecord_ID)
        {
            ClientsRequests ClientsRequests = new ClientsRequests();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM ClientsRequests WHERE ID = " + iRecord_ID ;

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ClientsRequests.ID = Convert.ToInt32(rdr["ID"]);
                    ClientsRequests.Client_ID = Convert.ToInt32(rdr["Client_ID"]);
                    ClientsRequests.Group_ID = rdr["Group_ID"] + "";
                    ClientsRequests.Tipos = Convert.ToInt32(rdr["Tipos"]);
                    ClientsRequests.Source_ID = Convert.ToInt32(rdr["Source_ID"]);
                    ClientsRequests.Description = rdr["Description"] + "";
                    ClientsRequests.Warning = rdr["Warning"] + "";
                    ClientsRequests.DateIns = Convert.ToDateTime(rdr["DateIns"]);
                    ClientsRequests.DateWarning = Convert.ToDateTime(rdr["DateWarning"]);
                    ClientsRequests.DateClose = Convert.ToDateTime(rdr["DateClose"]);
                    ClientsRequests.User_ID = Convert.ToInt32(rdr["User_ID"]);
                    ClientsRequests.Status = Convert.ToInt32(rdr["Status"]);
                    ClientsRequests.VideoChatStatus = Convert.ToInt32(rdr["VideoChatStatus"]);
                    ClientsRequests.VideoChatFile = rdr["VideoChatFile"] + "";
                }
                rdr.Close();
                con.Close();
            }
            return ClientsRequests;
        }
        //--- GET the record from ClientsRequests table --------------------------------------- 
        public List<ClientsRequests> GetRecord_ClientID(string connectionString, int iClient_ID, string sStatus)
        {
            ClientsRequests ClientsRequests = new ClientsRequests();
            List<ClientsRequests> lstClientsRequests = new List<ClientsRequests>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM ClientsRequests WHERE Client_ID = " + iClient_ID;
                if (sStatus.Trim().Length > 0) sqlQuery = sqlQuery + " AND Status = " + sStatus;

                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ClientsRequests = new ClientsRequests();
                    ClientsRequests.ID = Convert.ToInt32(rdr["ID"]);
                    ClientsRequests.Client_ID = Convert.ToInt32(rdr["Client_ID"]);
                    ClientsRequests.Group_ID = rdr["Group_ID"] + "";
                    ClientsRequests.Tipos = Convert.ToInt32(rdr["Tipos"]);
                    ClientsRequests.Source_ID = Convert.ToInt32(rdr["Source_ID"]);
                    ClientsRequests.Description = rdr["Description"] + "";
                    ClientsRequests.Warning = rdr["Warning"] + "";
                    ClientsRequests.DateIns = Convert.ToDateTime(rdr["DateIns"]);
                    ClientsRequests.DateWarning = Convert.ToDateTime(rdr["DateWarning"]);
                    ClientsRequests.DateClose = Convert.ToDateTime(rdr["DateClose"]);
                    ClientsRequests.User_ID = Convert.ToInt32(rdr["User_ID"]);
                    ClientsRequests.Status = Convert.ToInt32(rdr["Status"]);
                    ClientsRequests.VideoChatStatus = Convert.ToInt32(rdr["VideoChatStatus"]);
                    ClientsRequests.VideoChatFile = rdr["VideoChatFile"] + "";

                    lstClientsRequests.Add(ClientsRequests);
                }
                rdr.Close();
                con.Close();
            }
            return lstClientsRequests;
        }
    }
}
