using System;
using System.Data;
using System.Data.SqlClient;

namespace ISPDBO.Models
{
    public class ServerJobsDAL
    {
        string _sConnectionString = Global.ConnectionString;
        public void AddRecord(ServerJobs server_job)
        {
            SqlCommand cmd;

            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                con.Open();

                cmd = new SqlCommand("InsertServerJobs", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                outParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outParam);

                cmd.Parameters.AddWithValue("@JobType_ID", server_job.JobType_ID);
                cmd.Parameters.AddWithValue("@Source_ID", server_job.Source_ID);
                cmd.Parameters.AddWithValue("@Parameters", server_job.Parameters);
                cmd.Parameters.AddWithValue("@DateStart", server_job.DateStart);
                cmd.Parameters.AddWithValue("@DateFinish", server_job.DateFinish);
                cmd.Parameters.AddWithValue("@PubKey", server_job.PubKey);
                cmd.Parameters.AddWithValue("@PrvKey", server_job.PrvKey);
                cmd.Parameters.AddWithValue("@Attempt", server_job.Attempt);
                cmd.Parameters.AddWithValue("@Status", server_job.Status);               
                cmd.ExecuteNonQuery();

                con.Close();
            }
        }
    }
}
