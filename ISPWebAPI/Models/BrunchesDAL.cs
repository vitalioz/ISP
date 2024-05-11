using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ISPWebAPI.Models
{
    public class BrunchesDAL
    {
        public IEnumerable<Brunches> GetBrunchesList(string connectionString)
        {
            List<Brunches> lstBrunches = new List<Brunches>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetTable", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Brunches"));
                cmd.Parameters.Add(new SqlParameter("@Col", ""));
                cmd.Parameters.Add(new SqlParameter("@Value", ""));
                cmd.Parameters.Add(new SqlParameter("@Order", "Title"));
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Brunches Brunches = new Brunches();
                    Brunches.ID = Convert.ToInt32((rdr["ID"]));
                    Brunches.Title = rdr["Title"].ToString();

                    lstBrunches.Add(Brunches);
                }
                con.Close();
            }
            return lstBrunches;
        }
    }
}
