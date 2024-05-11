using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace ISPWebAPI.Models
{
    public class OptionsDAL
    {
        public Options GetRecord(string connectionString)
        {
            Options oOptions = new Options();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetTable", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Options"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", "1"));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    oOptions.EMail_Sender = rdr["EMail_Sender"] + "";
                    oOptions.EMail_Username = rdr["EMail_Username"] + "";
                    oOptions.EMail_Password = rdr["EMail_Password"] + "";
                    oOptions.NonReplay_Sender = rdr["NonReplay_Sender"] + "";
                    oOptions.NonReplay_Username = rdr["NonReplay_Username"] + "";
                    oOptions.NonReplay_Password = rdr["NonReplay_Password"] + "";
                    oOptions.Request_Sender = rdr["Request_Sender"] + "";
                    oOptions.Request_Username = rdr["Request_Username"] + "";
                    oOptions.Request_Password = rdr["Request_Password"] + "";
                    oOptions.Support_Sender = rdr["Support_Sender"] + "";
                    oOptions.Support_Username = rdr["Support_Username"] + "";
                    oOptions.Support_Password = rdr["Support_Password"] + "";
                    oOptions.EMail_BO_Receiver = rdr["EMail_BO_Receiver"] + "";
                    oOptions.SMS_Username = rdr["SMS_Username"] + "";
                    oOptions.SMS_Password = rdr["SMS_Password"] + "";
                    oOptions.SMS_From = rdr["SMS_From"] + "";
                    oOptions.FTP_Username = rdr["FTP_Username"] + "";
                    oOptions.FTP_Password = rdr["FTP_Password"] + "";
                    oOptions.RS_Address = rdr["RS_Address"] + "";
                    oOptions.RS_Username = rdr["RS_Username"] + "";
                    oOptions.RS_Password = rdr["RS_Password"] + "";
                    oOptions.TaxDeclarations1Year = Convert.ToInt32((rdr["TaxDeclarations1Year"]));
                    oOptions.TaxDeclarationsLastYear = Convert.ToInt32((rdr["TaxDeclarationsLastYear"]));
                    oOptions.RequestsPeriod1 = Convert.ToInt32((rdr["RequestsPeriod1"]));
                    oOptions.RequestsPeriod2 = Convert.ToInt32((rdr["RequestsPeriod2"]));
                }
                con.Close();
            }
            return oOptions;
        }  
    }
}
