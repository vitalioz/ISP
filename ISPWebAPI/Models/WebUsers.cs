using System;

namespace ISPWebAPI
{
    public class WebUsers
    {
        //----- WebUser data -----------------------
        public int ID { get; set; }
        public int Client_ID { get; set; } = 0;
        public string Password { get; set; } = "";
        public int Status { get; set; } = 0;
        public DateTime DateIns { get; set; } = Convert.ToDateTime("1900/01/01");

        //----- Client data -----------------------
        public string Surname { get; set; } = "";
        public string Firstname { get; set; } = "";
        public DateTime DoB { get; set; } = Convert.ToDateTime("1900/01/01");
        public string BornPlace { get; set; } = "";
        public string ADT { get; set; } = "";
        public string ExpireDate { get; set; } = "";
        public string Police { get; set; } = "";
        public string DOY { get; set; } = "";
        public string AFM { get; set; } = "";
        public string Address { get; set; } = "";
        public string City { get; set; } = "";
        public string Zip { get; set; } = "";
        public int Country_ID { get; set; } = 0;
        public string EMail { get; set; } = "";
        public string Mobile { get; set; } = "";
        public string Tel { get; set; } = "";
        public string CountryCode { get; set; } = "";
        public string PhoneCode { get; set; } = "";
        public DateTime Client_DateIns { get; set; } = Convert.ToDateTime("1900/01/01");

        //----- WebUserDevice data -----------------------
        public int WUD_ID { get; set; }
    }
}
