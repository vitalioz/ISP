using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ISPWebAPI.Models
{
    public class LoggerDAL
    {
        public void AddLogger(string sConnectionString, Logger logger)
        {
            using (SqlConnection con = new SqlConnection(sConnectionString))
            {
                SqlCommand cmd = new SqlCommand("InsertLogRecord", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Author_ID", logger.Author_ID);
                cmd.Parameters.AddWithValue("@DateIns", logger.DateIns);
                cmd.Parameters.AddWithValue("@Rec_ID", logger.Rec_ID);
                cmd.Parameters.AddWithValue("@Notes", logger.Notes + "");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
