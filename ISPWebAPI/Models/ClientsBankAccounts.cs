using System;

namespace ISPWebAPI.Models
{
    public class ClientsBankAccounts
    {
        public int ID { get; set; }
        public int Bank_ID { get; set; }
        public string Bank_Title { get; set; }
        public int Client_ID { get; set; }
        public string AccNumber { get; set; }
        public int AccType { get; set; }
        public string AccOwners { get; set; }
        public string Currency { get; set; }
        public float StartBalance { get; set; }
        public int Status { get; set; }
    }
}
