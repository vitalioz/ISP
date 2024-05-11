using System;

namespace ISPWebAPI.Models
{
    public class Contracts
    {
        public int ID { get; set; }
        public int Author_ID { get; set; }
        public DateTime DateIns { get; set; }
        public int Rec_ID { get; set; }
        public string Notes { get; set; }
    }
}
