using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ISPDBO.Models
{
    public class PersonalData_MetaDataDAL
    {
        string _sConnectionString = Global.ConnectionString;
        public IEnumerable<PersonalData_MetaData> GetList()
        {
            List<PersonalData_MetaData> lstMetaData = new List<PersonalData_MetaData>();
            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                SqlCommand cmd = new SqlCommand("GetTable", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Table", "PersonalData_MetaData");
                cmd.Parameters.AddWithValue("@Col", "");
                cmd.Parameters.AddWithValue("@Value", "");
                cmd.Parameters.AddWithValue("@Order", "ID");

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    PersonalData_MetaData meta_data = new PersonalData_MetaData();
                    meta_data.ID = Convert.ToInt32(rdr["ID"]);
                    meta_data.Num = Convert.ToInt32(rdr["Num"]);
                    meta_data.Title = rdr["Title"] + "";
                    meta_data.Value = "";
                    meta_data.Mandatory = Convert.ToInt32(rdr["Mandatory"]);
                    meta_data.DocCount = 0;
                    meta_data.Status = Convert.ToInt32(rdr["Status"]);
                    lstMetaData.Add(meta_data);
                }
                con.Close();
            }
            return lstMetaData;
        }
    }
}
