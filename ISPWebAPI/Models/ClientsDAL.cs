using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Core;

namespace ISPWebAPI.Models
{
    public class ClientsDAL
    {
        clsClients Clients = new clsClients();
         public int GetClient_ID(string connectionString, string sParameters)
        {
            string sCriteries = " WHERE Clients.ID > 0 AND (Clients.Tipos = 1 OR Clients.Tipos = 2) ";
            var ClientData = JsonConvert.DeserializeObject<ClientsAPI>(sParameters);
            ClientsAPI Clients = new ClientsAPI();
            Clients.ID = 0;

            if ((ClientData.EMail + "").Length > 0) sCriteries = sCriteries + " AND Clients.EMail = '" + ClientData.EMail + "'";
            if ((ClientData.Mobile + "").Length > 0) sCriteries = sCriteries + " AND CHARINDEX(Clients.Mobile, '" + ClientData.Mobile + "') > 0 ";
            if ((ClientData.AFM + "").Length > 0) sCriteries = sCriteries + " AND Clients.AFM = '" + ClientData.AFM + "'";
            if (ClientData.DoB.Date != Convert.ToDateTime("0001/01/01").Date) sCriteries = sCriteries + " AND Clients.DoB = '" + Convert.ToDateTime(ClientData.DoB).ToString("yyyy/MM/dd") + "'";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT ID FROM Clients " + sCriteries + " ORDER BY Clients.ID ";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Clients.ID = Convert.ToInt32(rdr["ID"]);
                }
                rdr.Close();
                con.Close();
            }
            return Clients.ID;
        }
   
        public ClientsAPI GetClient_Data(string connectionString, string sParameters)
        {
            var ClientData = JsonConvert.DeserializeObject<ClientsAPI>(sParameters);
            ClientsAPI ClientsAPI = new ClientsAPI();

            Clients.Record_ID = ClientData.ID;
            Clients.EMail = ClientData.EMail + "";
            Clients.Mobile = ClientData.Mobile + "";
            Clients.AFM = ClientData.AFM + "";
            Clients.DoB = ClientData.DoB.Date != Convert.ToDateTime("0001/01/01").Date ? ClientData.DoB : Convert.ToDateTime("1900/01/01").Date;
            Clients.GetRecord();

            ClientsAPI.ID = Clients.Record_ID;
            ClientsAPI.Tipos = Clients.Type;
            ClientsAPI.Surname = Clients.Surname;
            ClientsAPI.Firstname = Clients.Firstname;
            ClientsAPI.SurnameEng = Clients.SurnameEng;
            ClientsAPI.FirstnameEng = Clients.FirstnameEng;
            ClientsAPI.SurnameFather = Clients.SurnameFather;
            ClientsAPI.FirstnameFather = Clients.FirstnameFather;
            ClientsAPI.SurnameMother = Clients.SurnameMother;
            ClientsAPI.FirstnameMother = Clients.FirstnameMother;
            ClientsAPI.Division = Clients.Division;
            ClientsAPI.Brunch_ID = Clients.Brunch_ID;
            ClientsAPI.Spec_ID = Clients.Spec_ID;
            ClientsAPI.SpecTitle = Clients.Spec_Title;
            ClientsAPI.BrunchesTitle = Clients.Brunches_Title;
            ClientsAPI.DoB = (DateTime)Clients.DoB;
            ClientsAPI.BornPlace = Clients.BornPlace;
            ClientsAPI.Citizen_ID = Clients.Citizen_ID;
            ClientsAPI.Sex = Clients.Sex;
            ClientsAPI.Category = Clients.Category;
            ClientsAPI.Risk = Clients.Risk;
            ClientsAPI.Guardian_ID = Clients.Guardian_ID;
            ClientsAPI.ADT = Clients.ADT;
            ClientsAPI.ExpireDate = Clients.ExpireDate;
            ClientsAPI.Police = Clients.Police;
            ClientsAPI.Passport = Clients.Passport;
            ClientsAPI.Passport_ExpireDate = Clients.Passport_ExpireDate;
            ClientsAPI.Passport_Police = Clients.Passport_Police;
            ClientsAPI.DOY = Clients.DOY;
            ClientsAPI.AFM = Clients.AFM;
            ClientsAPI.AMKA = Clients.AMKA;
            ClientsAPI.Address = Clients.Address;
            ClientsAPI.City = Clients.City;
            ClientsAPI.Zip = Clients.Zip;
            ClientsAPI.Country_ID = Clients.Country_ID;
            ClientsAPI.CountryCode = Clients.CountryCode;
            ClientsAPI.Country_Title_En = Clients.Country_Title_En;
            ClientsAPI.Country_Title_Gr = Clients.Country_Title_Gr;
            ClientsAPI.CountryPhoneCode = Clients.Country_PhoneCode;
            ClientsAPI.Mobile = Clients.Mobile;
            ClientsAPI.EMail = Clients.EMail;
            ClientsAPI.CountryTaxes_ID = Clients.CountryTaxes_ID;
            ClientsAPI.CountryTaxes_Code = Clients.CountryTaxes_Code;
            ClientsAPI.CountryTaxes_Title_En = Clients.CountryTaxes_Title_En;
            ClientsAPI.CountryTaxes_Title_Gr = Clients.CountryTaxes_Title_Gr;
            ClientsAPI.CountryTaxes_PhoneCode = Clients.CountryTaxes_PhoneCode;
            ClientsAPI.SpecialCategory = Clients.SpecialCategory;
            ClientsAPI.Merida = Clients.Merida;
            ClientsAPI.LogAxion = Clients.LogAxion;
            ClientsAPI.CompanyTitle = Clients.CompanyTitle;
            ClientsAPI.CompanyDescription = Clients.CompanyDescription;
            ClientsAPI.JobPosition = Clients.JobPosition;
            ClientsAPI.JobAddress = Clients.JobAddress;
            ClientsAPI.JobCity = Clients.JobCity;
            ClientsAPI.JobZip = Clients.JobZip;
            ClientsAPI.JobCountry_ID = Clients.JobCountry_ID;
            ClientsAPI.JobTel = Clients.JobTel;
            ClientsAPI.JobMobile = Clients.JobMobile;
            ClientsAPI.JobEMail = Clients.JobEMail;
            ClientsAPI.JobURL = Clients.JobURL;
            ClientsAPI.FamilyStatus = Clients.FamilyStatus;
            ClientsAPI.Tel = Clients.Tel;
            ClientsAPI.RM_EMail = Clients.RM_EMail;
            ClientsAPI.Status = Clients.Status;
            ClientsAPI.BlockStatus = Clients.BlockStatus;

            return ClientsAPI;
        }
        
        public IEnumerable<ClientsAPI> GetClient_List(string connectionString, string sParameters)
        {
            string sCriteries = " WHERE Clients.ID > 0 AND (Clients.Tipos = 1 OR Clients.Tipos = 2) ";
            var ClientData = JsonConvert.DeserializeObject<ClientsAPI>(sParameters);

            List<ClientsAPI> lstClients = new List<ClientsAPI>();

            if ((ClientData.EMail + "").Length > 0) sCriteries = sCriteries + " AND Clients.EMail = '" + ClientData.EMail + "'";
            if ((ClientData.Mobile + "").Length > 0) sCriteries = sCriteries + " AND CHARINDEX(Clients.Mobile, '" + ClientData.Mobile + "') > 0 ";
            if ((ClientData.AFM + "").Length > 0) sCriteries = sCriteries + " AND Clients.AFM = '" + ClientData.AFM + "'";
            if (ClientData.DoB.Date != Convert.ToDateTime("0001/01/01").Date) sCriteries = sCriteries + " AND Clients.DoB = '" + Convert.ToDateTime(ClientData.DoB).ToString("yyyy/MM/dd") + "'";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT Clients.*, Countries.Code AS CountryCode, Countries.Title AS Country_Title_En, Countries.TitleGreek AS Country_Title_Gr, Countries.PhoneCode, " +
                                  "'' AS CountryTaxes_Code, '' AS CountryTaxes_Title_En, '' AS CountryTaxes_Title_Gr, '' AS CountryTaxes_PhoneCode, " +
                                  "Specials.Title AS SpecTitle FROM Clients LEFT OUTER JOIN " +
                                  "Specials ON Clients.Spec_ID = Specials.ID LEFT OUTER JOIN Countries ON Clients.Country_ID = Countries.ID " +
                                  sCriteries + " ORDER BY Clients.ID ";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ClientsAPI Clients = new ClientsAPI();

                    Clients.ID = Convert.ToInt32(rdr["ID"]);
                    Clients.Tipos = Convert.ToInt32(rdr["Tipos"]);
                    Clients.Surname = rdr["Surname"].ToString();
                    Clients.Firstname = rdr["Firstname"].ToString();
                    Clients.SurnameEng = rdr["SurnameEng"].ToString();
                    Clients.FirstnameEng = rdr["FirstnameEng"].ToString();
                    Clients.SurnameFather = rdr["SurnameFather"].ToString();
                    Clients.FirstnameFather = rdr["FirstnameFather"].ToString();
                    Clients.SurnameMother = rdr["SurnameMother"].ToString();
                    Clients.FirstnameMother = rdr["FirstnameMother"].ToString();
                    Clients.Division = Convert.ToInt32((rdr["Division"]));
                    Clients.Brunch_ID = Convert.ToInt32((rdr["Brunch_ID"]));
                    Clients.Spec_ID = Convert.ToInt32((rdr["Spec_ID"]));
                    Clients.SpecTitle = rdr["SpecTitle"].ToString();
                    Clients.DoB = (DateTime)rdr["DoB"];
                    Clients.BornPlace = rdr["BornPlace"].ToString();
                    Clients.Citizen_ID = Convert.ToInt32((rdr["Citizen_ID"]));
                    Clients.Sex = rdr["Sex"].ToString();
                    Clients.Category = Convert.ToInt32((rdr["Category"]));
                    Clients.Risk = Convert.ToInt32((rdr["Risk"]));
                    Clients.Guardian_ID = Convert.ToInt32((rdr["Guardian_ID"]));
                    Clients.ADT = rdr["ADT"].ToString();
                    Clients.ExpireDate = rdr["ExpireDate"].ToString();
                    Clients.Police = rdr["Police"].ToString();
                    Clients.Passport = rdr["Passport"].ToString();
                    Clients.Passport_ExpireDate = rdr["Passport_ExpireDate"].ToString(); 
                    Clients.Passport_Police = rdr["Passport_Police"].ToString();  
                    Clients.DOY = rdr["DOY"].ToString();
                    Clients.AFM = rdr["AFM"].ToString();
                    Clients.AMKA = rdr["AMKA"].ToString();
                    Clients.Address = rdr["Address"].ToString();
                    Clients.City = rdr["City"].ToString();
                    Clients.Zip = rdr["Zip"].ToString();
                    Clients.Country_ID = Convert.ToInt32((rdr["Country_ID"]));
                    Clients.CountryCode = rdr["CountryCode"].ToString();
                    Clients.Country_Title_En = rdr["Country_Title_En"].ToString();
                    Clients.Country_Title_Gr = rdr["Country_Title_Gr"].ToString();
                    Clients.CountryPhoneCode = rdr["PhoneCode"].ToString();
                    Clients.Mobile = rdr["Mobile"].ToString();
                    Clients.EMail = rdr["EMail"].ToString();
                    Clients.CountryTaxes_ID = Convert.ToInt32((rdr["CountryTaxes_ID"]));
                    Clients.CountryTaxes_Code = rdr["CountryTaxes_Code"].ToString();
                    Clients.CountryTaxes_Title_En = rdr["CountryTaxes_Title_En"].ToString();
                    Clients.CountryTaxes_Title_Gr = rdr["CountryTaxes_Title_Gr"].ToString();
                    Clients.CountryTaxes_PhoneCode = rdr["CountryTaxes_PhoneCode"].ToString();
                    Clients.SpecialCategory = rdr["SpecialCategory"].ToString();
                    Clients.Merida = rdr["Merida"].ToString();
                    Clients.LogAxion = rdr["LogAxion"].ToString();
                    Clients.CompanyTitle = rdr["CompanyTitle"].ToString();
                    Clients.CompanyDescription = rdr["CompanyDescription"].ToString();
                    Clients.JobPosition = rdr["JobPosition"].ToString();
                    Clients.JobAddress = rdr["JobAddress"].ToString();
                    Clients.JobCity = rdr["JobCity"].ToString();
                    Clients.JobZip = rdr["JobZip"].ToString();
                    Clients.JobCountry_ID = Convert.ToInt32((rdr["JobCountry_ID"]));
                    Clients.JobTel = rdr["JobTel"].ToString();
                    Clients.JobMobile = rdr["JobMobile"].ToString();
                    Clients.JobEMail = rdr["JobEMail"].ToString();
                    Clients.JobURL = rdr["JobURL"].ToString();
                    Clients.FamilyStatus = Convert.ToInt32((rdr["FamilyStatus"]));
                    //Clients.Password = rdr["Password"].ToString();
                    Clients.Tel = rdr["Tel"].ToString();
                    Clients.Status = Convert.ToInt16((rdr["Status"]));
                    Clients.BlockStatus = Convert.ToInt16((rdr["BlockStatus"]));

                    lstClients.Add(Clients);

                }
                rdr.Close();
                con.Close();
            }
            return lstClients;
        }

    }
}
