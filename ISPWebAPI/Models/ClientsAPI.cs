using System;

namespace ISPWebAPI.Models
{
    public class ClientsAPI
    {
        public int ID { get; set; }
        public int Tipos { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string SurnameEng { get; set; }
        public string FirstnameEng { get; set; }
        public string SurnameFather { get; set; }
        public string FirstnameFather { get; set; }
        public string SurnameMother { get; set; }
        public string FirstnameMother { get; set; }
        public string SurnameSizigo { get; set; }
        public string FirstnameSizigo { get; set; }
        public int Division { get; set; }
        public int Brunch_ID { get; set; }
        public int Spec_ID { get; set; }
        public DateTime DoB { get; set; }
        public string BornPlace { get; set; }
        public int Citizen_ID { get; set; }
        public string Sex { get; set; }
        public int Category { get; set; }
        public int Risk { get; set; }
        public int Guardian_ID { get; set; }
        public string ADT { get; set; }
        public string ExpireDate { get; set; }
        public string Police { get; set; }
        public string Passport { get; set; }
        public string Passport_ExpireDate { get; set; }
        public string Passport_Police { get; set; }
        public string DOY { get; set; }
        public string AFM { get; set; }
        public string DOY2 { get; set; }
        public string AFM2 { get; set; }
        public string AMKA { get; set; }
        public int CountryTaxes_ID { get; set; }
        public string CountryTaxes_Code { get; set; }
        public string CountryTaxes_Title_En { get; set; }
        public string CountryTaxes_Title_Gr { get; set; }
        public string CountryTaxes_PhoneCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public int Country_ID { get; set; }
        public string CountryCode { get; set; }
        public string Country_Title_En { get; set; }
        public string Country_Title_Gr { get; set; }
        public string CountryPhoneCode { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public int SendSMS { get; set; }
        public string EMail { get; set; }
        public string Advisor_EMail { get; set; }
        public string RM_EMail { get; set; }
        public int ConnectionMethod { get; set; }
        public int LogSxedio_ID { get; set; }
        public float VAT_Percent { get; set; }
        public string SpecialCategory  { get; set; }
        public string Merida { get; set; }
        public string LogAxion { get; set; }
        public string Notes { get; set; }
        public string CompanyTitle { get; set; }
        public string CompanyDescription { get; set; }
        public string JobPosition { get; set; }
        public string JobAddress { get; set; }
        public string JobCity { get; set; }
        public string JobZip { get; set; }
        public int JobCountry_ID { get; set; }
        public string JobTel  { get; set; }
        public string JobMobile { get; set; }
        public string JobEMail { get; set; }
        public string JobURL { get; set; }
        public int FamilyStatus { get; set; }
        public string Password { get; set; }
        public string SpecTitle { get; set; }
        public string BrunchesTitle { get; set; }
        public string NewMobile { get; set; }
        public int Status { get; set; }
        public int BlockStatus { get; set; }
    }
}
