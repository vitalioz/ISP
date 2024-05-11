using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ISPDBO.Models
{
    public class PersonalDataDAL
    {
        string _sConnectionString = Global.ConnectionString; 
        
        public IEnumerable<PersonalData> GetList(int iClient_ID)
        {
            List<PersonalData> lstPersonalData = new List<PersonalData>();
            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                SqlCommand cmd = new SqlCommand("GetPersonalData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Client_ID", iClient_ID);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    PersonalData personal_data = new PersonalData();
                    personal_data.ID = Convert.ToInt32(rdr["ID"]);
                    personal_data.Num = Convert.ToInt32(rdr["Num"]);
                    personal_data.Title = rdr["Title"] + "";
                    personal_data.Value = "";
                    personal_data.Mandatory = Convert.ToInt32(rdr["Mandatory"]);
                    personal_data.DocCount = 0;
                    personal_data.Status = Convert.ToInt32(rdr["Status"]);
                    lstPersonalData.Add(personal_data);
                }
                con.Close();
            }
            return lstPersonalData;
        }
        public void AddRecordsSet(int client_id)
        {
            for (int i=0; i<= 11; i++) AddRecord(client_id, i);
        }
        public void AddRecord(int client_id, int num)
        {
            SqlCommand cmd;

            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                con.Open();

                cmd = new SqlCommand("InsertPersonalData", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                outParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outParam);

                cmd.Parameters.AddWithValue("@Client_ID", client_id);
                cmd.Parameters.AddWithValue("@PD_MD_Num", num);
                cmd.ExecuteNonQuery();

                con.Close();
            }
        }
    }
}
