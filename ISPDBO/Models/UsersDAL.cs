using System;
using System.Data;
using System.Data.SqlClient;

namespace ISPDBO.Models
{
    public class UsersDAL
    {
        string _sConnectionString = Global.ConnectionString;
        string[] Categories = { "ΙΔΙΩΤΗΣ", "ΕΤΑΙΡΕΙΑ" };
        string[] Families = { "Άγαμος/η", "Έγγαμος/η" };
        public Users GetUser_Login(string username, string password)
        {
            Users Users = new Users();

            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                SqlCommand cmd = new SqlCommand("GetWebUsers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", username + "");
                cmd.Parameters.AddWithValue("@Password", password + "");

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Users.ID = Convert.ToInt32(rdr["ID"]);
                    if (Convert.ToInt32(rdr["Client_ID"]) == 0)
                    {
                        Users.Client_ID = 0;
                        Users.Category = 0;
                        Users.Category_Title = "";
                        Users.FamilyStatus = 0;
                        Users.Family_Title = "";
                        Users.Brunch_ID = 0;
                        Users.Brunch_Title = "";
                        Users.Spec_ID = 0;
                        Users.Spec_Title = "";
                        Users.DoB = ""; // Convert.ToDateTime("1900/01/01").ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        Users.Client_ID = Convert.ToInt32(rdr["Client_ID"]);
                        Users.Category = Convert.ToInt32(rdr["Category"]);
                        Users.Category_Title = Categories[Convert.ToInt32(rdr["Category"])];
                        Users.FamilyStatus = Convert.ToInt32(rdr["FamilyStatus"]);
                        Users.Family_Title = Families[Convert.ToInt32(rdr["FamilyStatus"])];
                        Users.Brunch_ID = Convert.ToInt32(rdr["Brunch_ID"]);
                        Users.Brunch_Title = rdr["Brunch_Title"].ToString();
                        Users.Spec_ID = Convert.ToInt32(rdr["Spec_ID"]);
                        Users.Spec_Title = rdr["Spec_Title"].ToString();
                        Users.DoB = Convert.ToDateTime(rdr["DoB"]) == Convert.ToDateTime("1900/01/01") ? "" : Convert.ToDateTime(rdr["DoB"]).ToString("dd/MM/yyyy");
                    }

                    Users.Surname = rdr["Surname"].ToString();
                    Users.Firstname = rdr["Firstname"].ToString();
                    Users.Fathername = rdr["FirstnameFather"].ToString();
                    Users.ADT = rdr["ADT"].ToString();
                    Users.Passport = "";
                    Users.AFM = rdr["AFM"].ToString();
                    Users.AMKA = rdr["AMKA"].ToString();
                    Users.Tel = rdr["Tel"].ToString();
                    Users.Mobile = rdr["Mobile"].ToString();
                    Users.Email = rdr["Email"].ToString();
                    Users.Address = rdr["Address"] + " " + rdr["Zip"] + " " + rdr["City"];
                    Users.LogAxion = rdr["LogAxion"].ToString();
                    Users.Merida = rdr["Merida"].ToString();                    
                    Users.Password = rdr["Password"].ToString();

                    if (Users.Surname != "" || Users.Firstname != "") Users.Folder = Users.Surname + " " + Users.Firstname;
                    else Users.Folder = "WebUser_" + Users.ID;
                    Users.Folder = Users.Folder.Replace(".", "_").Trim();
                }
            }
            return Users;
        }
        public Users GetWebUser_ID(int id)
        {
            Users Users = new Users();

            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                SqlCommand cmd = new SqlCommand("GetWebUsers_ID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Users.ID = Convert.ToInt32(rdr["ID"]);
                    if (Convert.ToInt32(rdr["Client_ID"]) == 0)
                    {
                        Users.Category = 0;
                        Users.Category_Title = "";
                        Users.FamilyStatus = 0;
                        Users.Family_Title = "";
                        Users.Brunch_ID = 0;
                        Users.Brunch_Title = "";
                        Users.Spec_ID = 0;
                        Users.Spec_Title = "";
                        Users.DoB = "";   // Convert.ToDateTime("1900/01/01").ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        Users.Category = Convert.ToInt32(rdr["Category"]);
                        Users.Category_Title = Categories[Convert.ToInt32(rdr["Category"])];
                        Users.FamilyStatus = Convert.ToInt32(rdr["FamilyStatus"]);
                        Users.Family_Title = Families[Convert.ToInt32(rdr["FamilyStatus"])];
                        Users.Brunch_ID = Convert.ToInt32(rdr["Brunch_ID"]);
                        Users.Brunch_Title = rdr["Brunch_Title"].ToString();
                        Users.Spec_ID = Convert.ToInt32(rdr["Spec_ID"]);
                        Users.Spec_Title = rdr["Spec_Title"].ToString();
                        Users.DoB = Convert.ToDateTime(rdr["DoB"]) == Convert.ToDateTime("1900/01/01") ? "" : Convert.ToDateTime(rdr["DoB"]).ToString("dd/MM/yyyy");
                    }

                    Users.Surname = rdr["Surname"].ToString();
                    Users.Firstname = rdr["Firstname"].ToString();
                    Users.Fathername = rdr["FirstnameFather"].ToString();
                    Users.ADT = rdr["ADT"].ToString();
                    Users.Passport = "";
                    Users.AFM = rdr["AFM"].ToString();
                    Users.AMKA = rdr["AMKA"].ToString();
                    Users.Tel = rdr["Tel"].ToString();
                    Users.Mobile = rdr["Mobile"].ToString();
                    Users.Email = rdr["Email"].ToString();
                    Users.Address = rdr["Address"] + " " + rdr["Zip"] + " " + rdr["City"];
                    Users.LogAxion = rdr["LogAxion"].ToString();
                    Users.Merida = rdr["Merida"].ToString();
                    Users.Password = rdr["Password"].ToString();
                }
            }
            return Users;
        }
        public Users GetClient(string sAFM, string sDoB)
        {
            Users Users = new Users();

            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT Clients.*, Countries.Code AS CountryCode, Countries.PhoneCode FROM dbo.Clients LEFT OUTER JOIN Countries ON Clients.Country_ID = Countries.ID " +
                                  " WHERE Clients.AFM = '" + sAFM + "'" + " AND Clients.DoB = '" + Convert.ToDateTime(sDoB).ToString("yyyy/MM/dd") + "' ORDER BY Clients.ID ", con);
                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.AddWithValue("@Email", sAFM);
                //cmd.Parameters.AddWithValue("@Password", sDoB);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Users.ID = Convert.ToInt32(rdr["ID"]);
                    //Users.Category = Convert.ToInt32(rdr["Category"]);
                    //Users.Category_Title = Categories[Convert.ToInt32(rdr["Category"])];
                    //Users.FamilyStatus = Convert.ToInt32(rdr["FamilyStatus"]);
                    //Users.Family_Title = Families[Convert.ToInt32(rdr["FamilyStatus"])];
                    //Users.Spec_ID = Convert.ToInt32(rdr["Spec_ID"]);
                    // Users.Spec_Title = rdr["Spec_Title"].ToString();
                    Users.DoB = Convert.ToDateTime(rdr["DoB"]).ToString("dd/MM/yyyy");
                    Users.Surname = rdr["Surname"].ToString();
                    Users.Firstname = rdr["Firstname"].ToString();
                    Users.Fathername = rdr["FirstnameFather"].ToString();
                    Users.ADT = rdr["ADT"].ToString();
                    Users.Passport = "";
                    Users.AFM = rdr["AFM"].ToString();
                    Users.AMKA = rdr["AMKA"].ToString();
                    Users.Tel = rdr["Tel"].ToString();
                    Users.Mobile = rdr["Mobile"].ToString();
                    Users.Email = rdr["Email"].ToString();
                    Users.Address = rdr["Address"] + " " + rdr["Zip"] + " " + rdr["City"];
                    Users.LogAxion = rdr["LogAxion"].ToString();
                    Users.Merida = rdr["Merida"].ToString();
                    Users.Password = rdr["Password"].ToString();
                }
            }
            return Users;
        }
        public int AddWebUsers(string sEmail, string sMobile, string sPassword, int iClient_ID)
        {
            int iID = 0;
            SqlCommand cmd;

            using (SqlConnection con = new SqlConnection(_sConnectionString))
            {
                con.Open();

                cmd = new SqlCommand("InsertWebUsers", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                outParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outParam);

                cmd.Parameters.AddWithValue("@EMail", sEmail);
                cmd.Parameters.AddWithValue("@Mobile", sMobile);
                cmd.Parameters.AddWithValue("@Password", sPassword);
                cmd.Parameters.AddWithValue("@Pin", "");
                cmd.Parameters.AddWithValue("@Client_ID", iClient_ID);
                cmd.Parameters.AddWithValue("@CurrentStep", "0");
                cmd.Parameters.AddWithValue("@TermsAgreement", "");
                cmd.Parameters.AddWithValue("@Status", "1");
                cmd.Parameters.AddWithValue("@DateIns", DateTime.Now);
                cmd.ExecuteNonQuery();

                iID = Convert.ToInt32(cmd.Parameters["@ID"].Value);

                con.Close();
            }

            return iID;
        }
    }
}
