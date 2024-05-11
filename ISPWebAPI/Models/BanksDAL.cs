using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ISPWebAPI.Models
{
    public class BanksDAL
    {
        public IEnumerable<Banks> GetBanksList(string connectionString)
        {
            List<Banks> lstBanks = new List<Banks>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetTable", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Banks"));
                cmd.Parameters.Add(new SqlParameter("@Col", ""));
                cmd.Parameters.Add(new SqlParameter("@Value", ""));
                cmd.Parameters.Add(new SqlParameter("@Order", "Title"));
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Banks Banks = new Banks();
                    Banks.ID = Convert.ToInt32((rdr["ID"]));
                    Banks.Title = rdr["Title"].ToString();

                    lstBanks.Add(Banks);
                }
                con.Close();
            }
            return lstBanks;
        }
    }
}
