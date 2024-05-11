using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ISPWebAPI.Models
{
    public class ServerJobsDAL
    {
        public IEnumerable<ServerJobs> GetAllServerJobs(string connectionString, DateTime dStart, DateTime dFinish, int iJobType_ID, int iSource_ID, int iStatus)
        {

            List<ServerJobs> lstServerJobs = new List<ServerJobs>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetServerJobs_List", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateFrom", dStart));
                cmd.Parameters.Add(new SqlParameter("@DateTo", dFinish));
                cmd.Parameters.Add(new SqlParameter("@JobType_ID", iJobType_ID));
                cmd.Parameters.Add(new SqlParameter("@Source_ID", iSource_ID));
                cmd.Parameters.Add(new SqlParameter("@Status", iStatus));
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ServerJobs ServerJobs = new ServerJobs();

                    ServerJobs.ID = Convert.ToInt32((rdr["ID"]));
                    ServerJobs.JobType_ID = Convert.ToInt32((rdr["JobType_ID"]));
                    ServerJobs.Source_ID = Convert.ToInt32(rdr["Source_ID"]);
                    ServerJobs.Parameters = rdr["Parameters"].ToString();
                    ServerJobs.DateStart = (DateTime)rdr["DateStart"];
                    ServerJobs.DateFinish = (DateTime)rdr["DateFinish"];
                    ServerJobs.PubKey = rdr["PubKey"].ToString();
                    ServerJobs.PrvKey = rdr["PrvKey"].ToString();
                    ServerJobs.Attempt = Convert.ToInt16((rdr["Attempt"]));
                    ServerJobs.Status = Convert.ToInt16((rdr["Status"]));                   

                    lstServerJobs.Add(ServerJobs);
                }
                con.Close();
            }
            return lstServerJobs;
        }

        //To Add new ServerJobs record    
        public void AddServerJob(string connectionString, ServerJobs ServerJobs)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("InsertServerJobs", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@JobType_ID", ServerJobs.JobType_ID);
                cmd.Parameters.AddWithValue("@Source_ID", ServerJobs.Source_ID);
                cmd.Parameters.AddWithValue("@Parameters", ServerJobs.Parameters);
                cmd.Parameters.AddWithValue("@DateStart", ServerJobs.DateStart);
                cmd.Parameters.AddWithValue("@DateFinish", ServerJobs.DateFinish);
                cmd.Parameters.AddWithValue("@PubKey", ServerJobs.PubKey);
                cmd.Parameters.AddWithValue("@PrvKey", ServerJobs.PrvKey);
                cmd.Parameters.AddWithValue("@Attempt", ServerJobs.Attempt);
                cmd.Parameters.AddWithValue("@Status", ServerJobs.Status);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //To Update the records of a particluar ServerJobs  
        public void UpdateServerJob(string connectionString, ServerJobs ServerJobs)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("EditServerJobs", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", ServerJobs.ID);
                cmd.Parameters.AddWithValue("@JobType_ID", ServerJobs.JobType_ID);
                cmd.Parameters.AddWithValue("@Source_ID", ServerJobs.Source_ID);
                cmd.Parameters.AddWithValue("@Parameters", ServerJobs.Parameters);
                cmd.Parameters.AddWithValue("@DateStart", ServerJobs.DateStart);
                cmd.Parameters.AddWithValue("@DateFinish", ServerJobs.DateFinish);
                cmd.Parameters.AddWithValue("@PubKey", ServerJobs.PubKey);
                cmd.Parameters.AddWithValue("@PrvKey", ServerJobs.PrvKey);
                cmd.Parameters.AddWithValue("@Attempt", ServerJobs.Attempt);
                cmd.Parameters.AddWithValue("@Status", ServerJobs.Status);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //Get the details of a particular ServerJobs  
        public ServerJobs GetServerJobsData(string connectionString, int? id)
        {
            ServerJobs ServerJobs = new ServerJobs();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM ServerJobs WHERE ID= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ServerJobs.ID = Convert.ToInt32((rdr["ID"]));
                    ServerJobs.JobType_ID = Convert.ToInt32((rdr["JobType_ID"]));
                    ServerJobs.Source_ID = Convert.ToInt32(rdr["Source_ID"]);
                    ServerJobs.Parameters = rdr["Parameters"].ToString();
                    ServerJobs.DateStart = (DateTime)rdr["DateStart"];
                    ServerJobs.DateFinish = (DateTime)rdr["DateFinish"];
                    ServerJobs.PubKey = rdr["PubKey"].ToString();
                    ServerJobs.PrvKey = rdr["PrvKey"].ToString();
                    ServerJobs.Attempt = Convert.ToInt16((rdr["Attempt"]));
                    ServerJobs.Status = Convert.ToInt16((rdr["Status"]));
                }
            }
            return ServerJobs;
        }

        //To Delete the record on a particular ServerJobs  
        public void DeleteServerJob(string connectionString, int? id)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteRecord", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ServerJobs";
                cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = id;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
