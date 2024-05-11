using System;

namespace ISPDBO.Models
{
    public class WebUsers
    {
        //----- WebUser data -----------------------
        public int ID { get; set; }
        public string EMail { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string Pin { get; set; }
        public int Client_ID { get; set; }
        public int CurrentStep { get; set; }
        public string TermsAgreement { get; set; }
        public int Status { get; set; }
        public DateTime DateIns { get; set; }

        //----- Client data -----------------------
        public int Category { get; set; }        
        public string Category_Title { get; set; }        
        public string Surname { get; set; }        
        public string Firstname { get; set; }        
        public string Fathername { get; set; }        
        public string ADT { get; set; }        
        public string ExpireDate { get; set; }
        public string Police { get; set; }
        public string DOY { get; set; }
        public string AFM { get; set; }        
        public string AMKA { get; set; }        
        public string Tel { get; set; }   
        public string Email { get; set; }        
        public string Address { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public int Country_ID { get; set; }
        public string CompanyTitle { get; set; }
        public string CompanyDescription { get; set; }
        public string JobPosition { get; set; }
        public string JobAddress { get; set; }
        public string JobCity { get; set; }
        public string JobZip { get; set; }
        public int JobCountry_ID { get; set; }
        public string JobTel { get; set; }
        public string JobMobile { get; set; }
        public string JobEmail { get; set; }
        public string JobURL { get; set; }
        public string LogAxion { get; set; }        
        public string Merida { get; set; }        
        public int Brunch_ID { get; set; }        
        public string Brunch_Title { get; set; }        
        public int Spec_ID { get; set; }        
        public string Spec_Title { get; set; }
        public string BornPlace { get; set; }
        public string DoB { get; set; }        
        public int FamilyStatus { get; set; }        
        public string Family_Title { get; set; }
        public int SpecialCategory { get; set; }        
        public string Folder { get; set; }
        public string CountryTitle { get; set; }
        public string CountryCode { get; set; }
        public string PhoneCode { get; set; }

    }
}
