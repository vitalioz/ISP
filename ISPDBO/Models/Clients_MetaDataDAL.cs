using System;
using System.Data;
using System.Data.SqlClient;

namespace ISPDBO.Models
{
    public class Clients_MetaDataDAL
    {
        string _sConnectionString = Global.ConnectionString;
        public Clients_MetaData GetRecord(int iRecord_ID)
        {
            Clients_MetaData meta_data = new Clients_MetaData();
            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Clients_MetaData WHERE ID = " + iRecord_ID, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    meta_data.ID = Convert.ToInt32(rdr["ID"]);
                    meta_data.Client_ID = Convert.ToInt32(rdr["Client_ID"]);
                    meta_data.PD_Status = Convert.ToInt16(rdr["PD_Status"]);
                    meta_data.PD_Request = rdr["PD_Request"].ToString();
                }
            }
            return meta_data;
        }
        public Clients_MetaData GetRecord_Client_ID(int iClient_ID)
        {
            Clients_MetaData meta_data = new Clients_MetaData();
            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Clients_MetaData WHERE Client_ID = " + iClient_ID, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    meta_data.ID = Convert.ToInt32(rdr["ID"]);
                    meta_data.Client_ID = Convert.ToInt32(rdr["Client_ID"]);
                    meta_data.PD_Status = Convert.ToInt32(rdr["PD_Status"]);
                    meta_data.PD_Request = rdr["PD_Request"].ToString();
                }
            }
            return meta_data;
        }
        public void AddRecord(int client_id)
        {
            SqlCommand cmd;

            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                con.Open();

                cmd = new SqlCommand("InsertClients_MetaData", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                outParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outParam);

                cmd.Parameters.AddWithValue("@Client_ID", client_id);
                cmd.Parameters.AddWithValue("@PD_Status", 0);
                cmd.Parameters.AddWithValue("@PD_Request", "");
                cmd.ExecuteNonQuery();

                con.Close();
            }
        }
        public void EditRecord(Clients_MetaData meta_data)
        {
            SqlCommand cmd;

            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                con.Open();

                cmd = new SqlCommand("EditClients_MetaData", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", meta_data.ID);
                cmd.Parameters.AddWithValue("@PD_Status", meta_data.PD_Status);
                cmd.Parameters.AddWithValue("@PD_Request", meta_data.PD_Request);
                cmd.ExecuteNonQuery();

                con.Close();
            }
        }
    }
}
