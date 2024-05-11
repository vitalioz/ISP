using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ISPDBO.Models
{
    public class DocTypesDAL
    {
        string _sConnectionString = Global.ConnectionString;
        public IEnumerable<DocTypes> GetList(int iGroup_ID)
        {
            string[] lstGroupsDocTypes = { "0,2,9267,", "0,1,3924,", "0,9419,", "0,3,9418,", "0,3,9418,", "0,9079,9315,", "0,3,517,", "0,", "0,9081,9299,", "0,", "0,4,9410,9411,", "0,55," };
            List<DocTypes> lstDocTypes = new List<DocTypes>();
            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                SqlCommand cmd = new SqlCommand("GetTable", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Table", "DocTypes");
                cmd.Parameters.AddWithValue("@Col", "");
                cmd.Parameters.AddWithValue("@Value", "");
                cmd.Parameters.AddWithValue("@Order", "Title");

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (iGroup_ID < 0 || lstGroupsDocTypes[iGroup_ID].IndexOf(","+rdr["ID"]+",") >= 0)
                    {
                        DocTypes DocTypes = new DocTypes();
                        DocTypes.ID = Convert.ToInt32(rdr["ID"]);
                        DocTypes.Title = rdr["Title"] + "";
                        lstDocTypes.Add(DocTypes);
                    }                    
                }
                con.Close();
            }
            return lstDocTypes;
        }
    }
}
