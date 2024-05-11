using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace ISPWebAPI.Models
{
    public class ClientsDocFilesDAL
    {
        int i = 0;
        string sTemp = "";
        public IEnumerable<ClientsDocFiles> GetClientsDocFilesList(string connectionString, string sParameters)
        {
            var ClientDocFile = JsonConvert.DeserializeObject<ClientsDocFiles>(sParameters);

            List<ClientsDocFiles> lstClientsDocFiles = new List<ClientsDocFiles>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetClient_DocFiles", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Client_ID", ClientDocFile.Client_ID));
                cmd.Parameters.Add(new SqlParameter("@PreContract_ID", ClientDocFile.PreContract_ID));
                cmd.Parameters.Add(new SqlParameter("@Contract_ID", ClientDocFile.Contract_ID));
                cmd.Parameters.Add(new SqlParameter("@DocTypes_ID", ClientDocFile.DocTypes_ID));
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ClientsDocFiles ClientsDocFiles = new ClientsDocFiles();
                    ClientsDocFiles.ID = Convert.ToInt32((rdr["ID"]));
                    ClientsDocFiles.Client_ID = Convert.ToInt32((rdr["Client_ID"]));
                    ClientsDocFiles.Contract_ID = Convert.ToInt32((rdr["Contract_ID"]));
                    ClientsDocFiles.DocTypes_ID = Convert.ToInt32((rdr["DocTypes_ID"]));
                    ClientsDocFiles.DMS_Files_ID = Convert.ToInt32((rdr["DMS_Files_ID"]));
                    ClientsDocFiles.FileName = rdr["FileName"].ToString();

                    lstClientsDocFiles.Add(ClientsDocFiles);
                }
                con.Close();
            }
            return lstClientsDocFiles;
        }
        public IEnumerable<ClientsDocFiles> GetClientTaxDeclarationsList(string connectionString, string sParameters)
        {
            var ClientDocFile = JsonConvert.DeserializeObject<ClientsDocFiles>(sParameters);

            ClientsDocFiles ClientsDocFiles = new ClientsDocFiles();
            List<ClientsDocFiles> lstClientsDocFiles = new List<ClientsDocFiles>();
            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetClient_DocFiles", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Client_ID", ClientDocFile.Client_ID));
                cmd.Parameters.Add(new SqlParameter("@PreContract_ID", ClientDocFile.PreContract_ID));
                cmd.Parameters.Add(new SqlParameter("@Contract_ID", ClientDocFile.Contract_ID));
                cmd.Parameters.Add(new SqlParameter("@DocTypes_ID", ClientDocFile.DocTypes_ID));
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if ((rdr["FileName"] + "").Trim() != "")
                    {
                        sTemp = rdr["FileName"] + "";
                        i = sTemp.IndexOf("ΕΚΚΑΘΑΡΙΣΤΙΚΟ ");
                        if (i >= 0)
                        {
                            sTemp = sTemp.Substring(14, 4);

                            ClientsDocFiles = new ClientsDocFiles();
                            ClientsDocFiles.ID = Convert.ToInt32((rdr["ID"]));
                            ClientsDocFiles.Client_ID = Convert.ToInt32((rdr["Client_ID"]));
                            ClientsDocFiles.Contract_ID = Convert.ToInt32((rdr["Contract_ID"]));
                            ClientsDocFiles.DocTypes_ID = Convert.ToInt32((rdr["DocTypes_ID"]));
                            ClientsDocFiles.DMS_Files_ID = Convert.ToInt32((rdr["DMS_Files_ID"]));
                            ClientsDocFiles.FileName = rdr["FileName"].ToString();
                            ClientsDocFiles.TaxYear = Convert.ToInt32(sTemp);
                            ClientsDocFiles.Status = Convert.ToInt32((rdr["Status"]));

                            lstClientsDocFiles.Add(ClientsDocFiles);
                        }
                    }
                }
                con.Close();
            }
            return lstClientsDocFiles;
        }
    }
}
