using System;
using System.Data;
using System.Data.SqlClient;

namespace ISPDBO.Models
{
    public class ClientsRequestsDAL
    {
        string _sConnectionString = Global.ConnectionString;
        ClientsRequests clientRequests = new ClientsRequests();
        public ClientsRequests GetList(int iClient_ID, int iStatus)
        {
            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                SqlCommand cmd = new SqlCommand("GetClientsRequests", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Client_ID", iClient_ID));
                cmd.Parameters.Add(new SqlParameter("@Status", iStatus));
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    clientRequests = new ClientsRequests();
                    clientRequests.ID = Convert.ToInt32(rdr["ID"]);
                    clientRequests.Tipos = Convert.ToInt32(rdr["Tipos"]);
                    clientRequests.Status = Convert.ToInt32(rdr["Status"]);
                }
                rdr.Close();
                con.Close();
            }
            return clientRequests;
        }
        public int AddRecord(ClientsRequests request)
        {
            int iID = 0;
            SqlCommand cmd;

            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                con.Open();

                cmd = new SqlCommand("InsertClientsRequests", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                outParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outParam);

                cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = request.Client_ID;
                cmd.Parameters.Add("@Tipos", SqlDbType.Int).Value = request.Tipos;
                cmd.Parameters.Add("@Source_ID", SqlDbType.Int).Value = request.Source_ID;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 1000).Value = request.Description;
                cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = request.DateIns;
                cmd.Parameters.Add("@DateClose", SqlDbType.DateTime).Value = request.DateClose;
                cmd.Parameters.Add("@Status", SqlDbType.Int).Value = request.Status;
               
                cmd.ExecuteNonQuery();

                iID = Convert.ToInt32(cmd.Parameters["@ID"].Value);
                con.Close();
            }
            return iID;
        }
    }
}
