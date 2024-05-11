using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ISPDBO.Models
{
    public class ClientsDoc_FilesDAL
    {
        string _sConnectionString = Global.ConnectionString;
        public ClientsDoc_Files GetRecord(int iRecord_ID)
        {
            ClientsDoc_Files ClientDoc_Files = new ClientsDoc_Files();

            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                SqlCommand cmd = new SqlCommand("GetClients_DocFile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", iRecord_ID);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ClientDoc_Files.ID = Convert.ToInt32(rdr["ID"]);
                    ClientDoc_Files.Client_ID = Convert.ToInt32(rdr["Client_ID"]);
                    ClientDoc_Files.PreContract_ID = Convert.ToInt32(rdr["PreContract_ID"]);
                    ClientDoc_Files.Contract_ID = Convert.ToInt32(rdr["Contract_ID"]);
                    ClientDoc_Files.DocTypes = Convert.ToInt32(rdr["DocTypes"]);
                    ClientDoc_Files.PD_Group_ID = Convert.ToInt32(rdr["PD_Group_ID"]);
                    ClientDoc_Files.OldFile = Convert.ToInt32(rdr["OldFile"]);
                    ClientDoc_Files.DateIns = Convert.ToDateTime(rdr["DateIns"]);
                    ClientDoc_Files.User_ID = Convert.ToInt32(rdr["User_ID"]);
                    ClientDoc_Files.DMS_Files_ID = Convert.ToInt32(rdr["DMS_Files_ID"]);
                    ClientDoc_Files.FileName = rdr["FileName"] + "";
                    ClientDoc_Files.FilePath = (rdr["Surname"] + " " + rdr["Firstname"]).Trim() + "/" + rdr["FileName"] + "";
                }
                con.Close();               
            }
            return ClientDoc_Files;
        }
        public IEnumerable<ClientsDoc_Files> GetList(int iClient_ID, int iGroup_ID)
        {

            List<ClientsDoc_Files> lstClientDoc_Files = new List<ClientsDoc_Files>();
            if (iClient_ID != 0)
            {
                using (SqlConnection con = new SqlConnection(_sConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("GetClient_DocFiles", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Client_ID", iClient_ID);
                    cmd.Parameters.AddWithValue("@PreContract_ID", 0);
                    cmd.Parameters.AddWithValue("@Contract_ID", 0);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        if (iGroup_ID == -1 || iGroup_ID == Convert.ToInt32(rdr["PD_Group_ID"]))
                        {
                            ClientsDoc_Files ClientDoc_Files = new ClientsDoc_Files();
                            ClientDoc_Files.ID = Convert.ToInt32(rdr["ID"]);
                            ClientDoc_Files.Client_ID = Convert.ToInt32(rdr["Client_ID"]);
                            ClientDoc_Files.PreContract_ID = Convert.ToInt32(rdr["PreContract_ID"]);
                            ClientDoc_Files.Contract_ID = Convert.ToInt32(rdr["Contract_ID"]);
                            ClientDoc_Files.DocTypes = Convert.ToInt32(rdr["DocTypes_ID"]);
                            ClientDoc_Files.DocTypes_Title = rdr["Tipos"] + "";
                            ClientDoc_Files.PD_Group_ID = Convert.ToInt32(rdr["PD_Group_ID"]);
                            ClientDoc_Files.OldFile = Convert.ToInt32(rdr["OldFile"]);
                            ClientDoc_Files.DateIns = Convert.ToDateTime(rdr["DateIns"]);
                            ClientDoc_Files.User_ID = Convert.ToInt32(rdr["User_ID"]);
                            ClientDoc_Files.DMS_Files_ID = Convert.ToInt32(rdr["DMS_Files_ID"]);
                            ClientDoc_Files.Status = Convert.ToInt32(rdr["Status"]);
                            ClientDoc_Files.FileName = rdr["FileName"] + "";
                            ClientDoc_Files.FilePath = (rdr["Surname"] + " " + rdr["Firstname"]).Trim() + "/" + rdr["FileName"] + "";

                            lstClientDoc_Files.Add(ClientDoc_Files);
                        }
                    }
                    con.Close();
                }
            }       
            return lstClientDoc_Files;
        }
        public int AddRecord(string sFileName, int iClient_ID, int iDocType, int iPD_Group_ID)
        {
            int iDMS_Files_ID = 0;
            int iRecord_ID = 0;
            SqlConnection con = new SqlConnection(_sConnectionString);

            try
            {    
                using (SqlCommand cmd = new SqlCommand("InsertDMS_File", con))
                {
                    con.Open();

                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Source_ID", SqlDbType.Int).Value = 1;
                    cmd.Parameters.Add("@DocTypes_ID", SqlDbType.Int).Value = iDocType;
                    cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 100).Value = sFileName;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = 15;                            // key.ID = 15 - пользователь online 
                    cmd.ExecuteNonQuery();
                    iDMS_Files_ID = Convert.ToInt32(outParam.Value);
                }

                using (SqlCommand cmd = new SqlCommand("InsertClientDocFile", con))
                {
                    SqlParameter outParam1 = new SqlParameter("@ID", SqlDbType.Int);
                    outParam1.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam1);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = iClient_ID;
                    cmd.Parameters.Add("@PreContract_ID", SqlDbType.Int).Value = 0;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = 0;
                    cmd.Parameters.Add("@DocTypes", SqlDbType.Int).Value = iDocType;
                    cmd.Parameters.Add("@PD_Group_ID", SqlDbType.Int).Value = iPD_Group_ID;
                    cmd.Parameters.Add("@DMS_Files_ID", SqlDbType.Int).Value = iDMS_Files_ID;
                    cmd.Parameters.Add("@OldFile", SqlDbType.Int).Value = 0;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = 15;                               // key.ID = 15 - пользователь online 
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = 0;                                 // 0 – вновь загруженный файл, 1 - файл, замещающий другой файл
                    cmd.ExecuteNonQuery();
                    iRecord_ID = Convert.ToInt32(outParam1.Value);
                 }
            }
            catch (Exception ex) { }
            finally { con.Close(); }

            return iRecord_ID;
        }
        public void EditSentStatus(int iClient_ID)
        {
            ClientsDoc_Files ClientDoc_Files = new ClientsDoc_Files();
            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE ClientsDocFiles SET Status = 2 WHERE Client_ID = " + iClient_ID + " AND (Status = 0 OR Status = 1)", con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
