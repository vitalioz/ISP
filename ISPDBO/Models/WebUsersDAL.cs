using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace ISPDBO.Models
{
    public class WebUsersDAL
    {
        SqlCommand cmd;
        
        WebUsers webuser = new WebUsers();
        int _WU_ID = 0;
        string sTemp = "";
        string _sConnectionString = Global.ConnectionString;
        string[] Categories = { "ΙΔΙΩΤΗΣ", "ΕΤΑΙΡΕΙΑ" };
        string[] Families = { "Άγαμος/η", "Έγγαμος/η" };
        public WebUsers GetAllWebUsers(string sEMail, string sPassword)
        {
            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                SqlCommand cmd = new SqlCommand("GetWebUsers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EMail", sEMail));
                cmd.Parameters.Add(new SqlParameter("@Password", sPassword));
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    webuser = new WebUsers(); 
                    webuser.ID = Convert.ToInt32(rdr["ID"]);
                    webuser.Email = rdr["Email"].ToString();
                    webuser.Mobile = rdr["Mobile"].ToString();
                    webuser.Password = rdr["Password"].ToString();
                    webuser.Pin = rdr["Pin"].ToString();
                    webuser.Client_ID = Convert.ToInt32(rdr["Client_ID"]);
                    webuser.CurrentStep = Convert.ToInt16(rdr["CurrentStep"]);
                    webuser.TermsAgreement = rdr["TermsAgreement"].ToString();
                    webuser.DateIns = Convert.ToDateTime(rdr["DateIns"]);
                    webuser.Status = Convert.ToInt16(rdr["Status"]);

                    if (webuser.Client_ID == 0)
                    {
                        webuser.Category = 0;
                        webuser.Category_Title = "";
                        webuser.FamilyStatus = 0;
                        webuser.Family_Title = "";
                        webuser.Brunch_ID = 0;
                        webuser.Brunch_Title = "";
                        webuser.Spec_ID = 0;
                        webuser.Spec_Title = "";
                        webuser.BornPlace = "";
                        webuser.DoB = "";
                    }
                    else {                        
                        webuser.Category = Convert.ToInt32(rdr["Category"]);
                        webuser.Category_Title = Categories[Convert.ToInt32(rdr["Category"])];
                        webuser.FamilyStatus = Convert.ToInt32(rdr["FamilyStatus"]);
                        webuser.Family_Title = Families[Convert.ToInt32(rdr["FamilyStatus"])];
                        webuser.Brunch_ID = Convert.ToInt32(rdr["Brunch_ID"]);
                        webuser.Brunch_Title = rdr["Brunch_Title"].ToString();
                        webuser.Spec_ID = Convert.ToInt32(rdr["Spec_ID"]);
                        webuser.Spec_Title = rdr["Spec_Title"].ToString();
                        webuser.BornPlace = rdr["BornPlace"].ToString();
                        webuser.DoB = Convert.ToDateTime(rdr["DoB"]) == Convert.ToDateTime("1900/01/01") ? "" : Convert.ToDateTime(rdr["DoB"]).ToString("dd/MM/yyyy");
                    }

                    webuser.Surname = rdr["Surname"].ToString();
                    webuser.Firstname = rdr["Firstname"].ToString();
                    webuser.Fathername = rdr["FirstnameFather"].ToString();
                    webuser.ADT = rdr["ADT"].ToString();
                    webuser.ExpireDate = rdr["ExpireDate"].ToString();
                    webuser.Police = rdr["Police"].ToString();
                    webuser.DOY = rdr["DOY"].ToString();
                    webuser.AFM = rdr["AFM"].ToString();
                    webuser.AMKA = rdr["AMKA"].ToString();
                    webuser.Tel = rdr["Tel"].ToString();
                    webuser.Address = rdr["Address"].ToString();
                    webuser.City = rdr["City"].ToString();
                    webuser.Zip = rdr["Zip"].ToString();
                    webuser.Country_ID = Convert.ToInt32(rdr["Country_ID"]);
                    webuser.CompanyTitle = rdr["CompanyTitle"].ToString();
                    webuser.CompanyDescription = rdr["CompanyDescription"].ToString();
                    webuser.JobPosition = rdr["JobPosition"].ToString();
                    webuser.JobAddress = rdr["JobAddress"].ToString();
                    webuser.JobCity = rdr["JobCity"].ToString();
                    webuser.JobZip = rdr["JobZip"].ToString();
                    webuser.JobCountry_ID = Convert.ToInt32(rdr["JobCountry_ID"]);
                    webuser.JobTel = rdr["JobTel"].ToString();
                    webuser.JobMobile = rdr["JobMobile"].ToString();
                    webuser.JobEmail = rdr["JobEmail"].ToString();
                    sTemp = rdr["JobURL"].ToString();
                    webuser.JobURL = sTemp.IndexOf("http://") >= 0 ? sTemp : "http://" + sTemp;
                    webuser.LogAxion = rdr["LogAxion"].ToString();
                    webuser.Merida = rdr["Merida"].ToString();
                    webuser.SpecialCategory = Convert.ToInt32(rdr["SpecialCategory"]);

                    if (webuser.Surname != "" || webuser.Firstname != "") webuser.Folder = webuser.Surname + " " + webuser.Firstname;
                    else webuser.Folder = "WebUser_" + webuser.ID;
                    webuser.Folder = webuser.Folder.Replace(".", "_").Trim();
                    webuser.CountryCode = rdr["CountryCode"].ToString();
                    webuser.CountryTitle = rdr["CountryTitle"].ToString();
                    webuser.PhoneCode = rdr["PhoneCode"].ToString();
                }
                rdr.Close();
                con.Close();
            }
            return webuser;
        }

        //To Add new WebUsers record    
        public int AddWebUsers(WebUsers WebUsers)
        {
            _WU_ID = 0;
            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                con.Open();

                cmd = new SqlCommand("InsertWebUsers", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                outParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outParam);

                cmd.Parameters.AddWithValue("@EMail", WebUsers.EMail);
                cmd.Parameters.AddWithValue("@Mobile", WebUsers.Mobile);
                cmd.Parameters.AddWithValue("@Password", WebUsers.Password);
                cmd.Parameters.AddWithValue("@Pin", WebUsers.Pin);
                cmd.Parameters.AddWithValue("@Client_ID", WebUsers.Client_ID);
                cmd.Parameters.AddWithValue("@CurrentStep", WebUsers.CurrentStep);
                cmd.Parameters.AddWithValue("@TermsAgreement", WebUsers.TermsAgreement);
                cmd.Parameters.AddWithValue("@Status", "1");
                cmd.Parameters.AddWithValue("@DateIns", DateTime.Now);

                con.Open();
                cmd.ExecuteNonQuery();
                _WU_ID = Convert.ToInt32(outParam.Value);
                con.Close();
            }
            return _WU_ID;
        }
       
        //To Update the records of a particluar WebUsers  
        public void MatchWUC(int iWU_ID, int iClient_ID)
        {
            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                string sqlQuery = "UPDATE WebUsers SET Client_ID = " + iClient_ID + " WHERE ID = " + iWU_ID;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        //To Update the records of a particluar WebUsers  
        public void UpdateWebUsers(WebUsers WebUsers)
        {
            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateWebUsers", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", WebUsers.ID);
                cmd.Parameters.AddWithValue("@EMail", WebUsers.EMail);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        //Get the details of a particular WebUsers  
        public int GetWebUser_ID(string sParameters)
        {
            string sCriteries = " WHERE WebUsers.ID > 0 ";
            WebUsers WebUsers = new WebUsers();

            var WebUserData = JsonConvert.DeserializeObject<WebUsers>(sParameters);
            WebUsers.ID = 0;

            if ((WebUserData.ID + "").Length > 0) sCriteries = sCriteries + " AND WebUsers.ID = '" + WebUserData.ID + "'";
            if ((WebUserData.EMail + "").Length > 0) sCriteries = sCriteries + " AND WebUsers.EMail = '" + WebUserData.EMail + "'";
            if ((WebUserData.Mobile + "").Length > 0) sCriteries = sCriteries + " AND WebUsers.Mobile = '" + WebUserData.Mobile + "'";
            if (WebUserData.Client_ID != 0) sCriteries = sCriteries + " AND WebUsers.Client_ID = " + WebUserData.Client_ID;
            if ((WebUserData.AFM + "").Length > 0) sCriteries = sCriteries + " AND Clients.AFM = '" + WebUserData.AFM + "'";
            if (Convert.ToDateTime(WebUserData.DoB).Date != Convert.ToDateTime("0001/01/01").Date) 
                sCriteries = sCriteries + " AND Clients.DoB = '" + Convert.ToDateTime(WebUserData.DoB).ToString("yyyy/MM/dd") + "'";

            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                string sqlQuery = "SELECT dbo.WebUsers.ID " +
                                  "FROM dbo.Countries RIGHT OUTER JOIN dbo.Clients ON dbo.Countries.ID = dbo.Clients.Country_ID RIGHT OUTER JOIN " +
                                  " dbo.WebUsers ON dbo.Clients.ID = dbo.WebUsers.Client_ID " + sCriteries + " ORDER BY dbo.WebUsers.ID ";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    WebUsers.ID = Convert.ToInt32(rdr["ID"]);
                }
                rdr.Close();
                con.Close();
            }
            return WebUsers.ID;
        }
        public WebUsers GetWebUser_Data(string sParameters)
        {
            string sCriteries = " WHERE WebUsers.ID > 0 ";
            WebUsers WebUsers = new WebUsers();

            var WebUserData = JsonConvert.DeserializeObject<WebUsers>(sParameters);
            WebUsers.ID = 0;

            if ((WebUserData.ID + "").Length > 0) sCriteries = sCriteries + " AND WebUsers.ID = '" + WebUserData.ID + "'";
            if ((WebUserData.EMail + "").Length > 0) sCriteries = sCriteries + " AND WebUsers.EMail = '" + WebUserData.EMail + "'";
            if ((WebUserData.Mobile + "").Length > 0) sCriteries = sCriteries + " AND WebUsers.Mobile = '" + WebUserData.Mobile + "'";
            if (WebUserData.Client_ID != 0) sCriteries = sCriteries + " AND WebUsers.Client_ID = " + WebUserData.Client_ID;
            if ((WebUserData.AFM + "").Length > 0) sCriteries = sCriteries + " AND Clients.AFM = '" + WebUserData.AFM + "'";
            if (Convert.ToDateTime(WebUserData.DoB).Date != Convert.ToDateTime("0001/01/01").Date) sCriteries = sCriteries + " AND Clients.DoB = '" + Convert.ToDateTime(WebUserData.DoB).ToString("yyyy/MM/dd") + "'";

            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                string sqlQuery = "SELECT dbo.WebUsers.*, dbo.Clients.*, dbo.Clients.ID AS Klient_ID, dbo.Countries.Code AS CountryCode, dbo.Countries.TitleGreek AS CountryTitle, " +
                                  "dbo.Countries.PhoneCode, dbo.Specials.Title AS Spec_Title,  dbo.Brunches.Title AS Brunch_Title " +
                                  "FROM dbo.Brunches RIGHT OUTER JOIN dbo.Clients ON dbo.Brunches.ID = dbo.Clients.Brunch_ID LEFT OUTER JOIN " +
                                  "dbo.Countries ON dbo.Clients.Country_ID = dbo.Countries.ID RIGHT OUTER JOIN " +
                                  "dbo.WebUsers ON dbo.Clients.ID = dbo.WebUsers.Client_ID LEFT OUTER JOIN " +
                                  "dbo.Specials ON dbo.Clients.Spec_ID = dbo.Specials.ID " + sCriteries + " ORDER BY dbo.WebUsers.ID ";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    webuser = new WebUsers();
                    webuser.ID = Convert.ToInt32(rdr["ID"]);
                    webuser.Email = rdr["Email"].ToString();
                    webuser.Mobile = rdr["Mobile"].ToString();
                    webuser.Password = rdr["Password"].ToString();
                    webuser.Pin = rdr["Pin"].ToString();
                    webuser.Client_ID = Convert.ToInt32(rdr["Client_ID"]);
                    webuser.CurrentStep = Convert.ToInt16(rdr["CurrentStep"]);
                    webuser.TermsAgreement = rdr["TermsAgreement"].ToString();
                    webuser.DateIns = Convert.ToDateTime(rdr["DateIns"]);
                    webuser.Status = Convert.ToInt16(rdr["Status"]);

                    if (webuser.Client_ID == 0)
                    {
                        webuser.Category = 0;
                        webuser.Category_Title = "";
                        webuser.FamilyStatus = 0;
                        webuser.Family_Title = "";
                        webuser.Brunch_ID = 0;
                        webuser.Brunch_Title = "";
                        webuser.Spec_ID = 0;
                        webuser.Spec_Title = "";
                        webuser.BornPlace = "";
                        webuser.DoB = "";
                    }
                    else
                    {
                        webuser.Category = Convert.ToInt32(rdr["Category"]);
                        webuser.Category_Title = Categories[Convert.ToInt32(rdr["Category"])];
                        webuser.FamilyStatus = Convert.ToInt32(rdr["FamilyStatus"]);
                        webuser.Family_Title = Families[Convert.ToInt32(rdr["FamilyStatus"])];
                        webuser.Brunch_ID = Convert.ToInt32(rdr["Brunch_ID"]);
                        webuser.Brunch_Title = rdr["Brunch_Title"].ToString();
                        webuser.Spec_ID = Convert.ToInt32(rdr["Spec_ID"]);
                        webuser.Spec_Title = rdr["Spec_Title"].ToString();
                        webuser.BornPlace = rdr["BornPlace"].ToString();
                        webuser.DoB = Convert.ToDateTime(rdr["DoB"]) == Convert.ToDateTime("1900/01/01") ? "" : Convert.ToDateTime(rdr["DoB"]).ToString("dd/MM/yyyy");
                    }

                    webuser.Surname = rdr["Surname"].ToString();
                    webuser.Firstname = rdr["Firstname"].ToString();
                    webuser.Fathername = rdr["FirstnameFather"].ToString();
                    webuser.ADT = rdr["ADT"].ToString();
                    webuser.ExpireDate = rdr["ExpireDate"].ToString();
                    webuser.Police = rdr["Police"].ToString();
                    webuser.DOY = rdr["DOY"].ToString();
                    webuser.AFM = rdr["AFM"].ToString();
                    webuser.AMKA = rdr["AMKA"].ToString();
                    webuser.Tel = rdr["Tel"].ToString();
                    webuser.Address = rdr["Address"].ToString();
                    webuser.City = rdr["City"].ToString();
                    webuser.Zip = rdr["Zip"].ToString();
                    webuser.Country_ID = Convert.ToInt32(rdr["Country_ID"]);
                    webuser.CompanyTitle = rdr["CompanyTitle"].ToString();
                    webuser.CompanyDescription = rdr["CompanyDescription"].ToString();
                    webuser.JobPosition = rdr["JobPosition"].ToString();
                    webuser.JobAddress = rdr["JobAddress"].ToString();
                    webuser.JobCity = rdr["JobCity"].ToString();
                    webuser.JobZip = rdr["JobZip"].ToString();
                    webuser.JobCountry_ID = Convert.ToInt32(rdr["JobCountry_ID"]);
                    webuser.JobTel = rdr["JobTel"].ToString();
                    webuser.JobMobile = rdr["JobMobile"].ToString();
                    webuser.JobEmail = rdr["JobEmail"].ToString();
                    sTemp = rdr["JobURL"].ToString();
                    webuser.JobURL = sTemp.IndexOf("http://") >= 0 ? sTemp : "http://" + sTemp;
                    webuser.LogAxion = rdr["LogAxion"].ToString();
                    webuser.Merida = rdr["Merida"].ToString();
                    webuser.SpecialCategory = Convert.ToInt32(rdr["SpecialCategory"]);

                    if (webuser.Surname != "" || webuser.Firstname != "") webuser.Folder = webuser.Surname + " " + webuser.Firstname;
                    else webuser.Folder = "WebUser_" + webuser.ID;
                    webuser.Folder = webuser.Folder.Replace(".", "_").Trim();
                    webuser.CountryCode = rdr["CountryCode"].ToString();
                    webuser.CountryTitle = rdr["CountryTitle"].ToString();
                    webuser.PhoneCode = rdr["PhoneCode"].ToString();
                }
                rdr.Close();
                con.Close();
            }
            return webuser;
        }

        //To Delete the record on a particular WebUsers  
        public void DeleteWebUser(int? id)
        {
            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                string sqlQuery = "DELETE WebUsers WHERE ID = " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
