using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ISPWebAPI.Models
{
    public class SpecialsDAL
    {
        public IEnumerable<Specials> GetSpecialsList(string connectionString)
        {
            List<Specials> lstSpecials = new List<Specials>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetTable", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Specials"));
                cmd.Parameters.Add(new SqlParameter("@Col", ""));
                cmd.Parameters.Add(new SqlParameter("@Value", ""));
                cmd.Parameters.Add(new SqlParameter("@Order", "Title"));
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Specials Specials = new Specials();
                    Specials.ID = Convert.ToInt32((rdr["ID"]));
                    Specials.Title = rdr["Title"].ToString();

                    lstSpecials.Add(Specials);
                }
                con.Close();
            }
            return lstSpecials;
        }
    }
}
