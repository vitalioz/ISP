using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ISPWebAPI.Models
{
    public class CountriesDAL
    {
        public IEnumerable<Countries> GetCountriesList(string connectionString)
        {

            List<Countries> lstCountries = new List<Countries>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetTable", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Countries"));
                cmd.Parameters.Add(new SqlParameter("@Col", "Tipos"));
                cmd.Parameters.Add(new SqlParameter("@Value", "1"));
                cmd.Parameters.Add(new SqlParameter("@Order", "Title"));
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Countries Countries = new Countries();
                    Countries.ID = Convert.ToInt32((rdr["ID"]));
                    Countries.Tipos = Convert.ToInt32((rdr["Tipos"]));
                    Countries.Code = rdr["Code"].ToString();
                    Countries.Code3 = rdr["Code3"].ToString();
                    Countries.Title = rdr["Title"].ToString();
                    Countries.TitleGreek = rdr["TitleGreek"].ToString();
                    Countries.CountriesGroup_ID = Convert.ToInt32((rdr["CountriesGroup_ID"]));
                    Countries.InvestGeography_ID = Convert.ToInt32((rdr["InvestGeography_ID"]));
                    Countries.PhoneCode = rdr["PhoneCode"].ToString();

                    lstCountries.Add(Countries);
                }
                con.Close();
            }
            return lstCountries;
        }
    }
}
