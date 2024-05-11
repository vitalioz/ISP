using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ISPWebAPI.Models
{
    public class CurrenciesDAL
    {
        public IEnumerable<Currencies> GetCurrenciesList(string connectionString)
        {

            List<Currencies> lstCurrencies = new List<Currencies>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetTable", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Currencies"));
                cmd.Parameters.Add(new SqlParameter("@Col", "Koef"));
                cmd.Parameters.Add(new SqlParameter("@Value", "1"));
                cmd.Parameters.Add(new SqlParameter("@Order", "Title"));
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Currencies Currencies = new Currencies();
                    Currencies.ID = Convert.ToInt32((rdr["ID"]));
                    Currencies.Title = rdr["Title"].ToString();
                    Currencies.Code = rdr["Code"].ToString();
                  
                    lstCurrencies.Add(Currencies);
                }
                con.Close();
            }
            return lstCurrencies;
        }
    }
}
