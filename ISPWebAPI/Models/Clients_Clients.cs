using System;

namespace ISPWebAPI.Models
{
    public class Clients_Clients
    {
        public int ID { get; set; }
        public int Client_ID { get; set; }
        public string Client_Name { get; set; }
        public string Client_AFM { get; set; }
        public string Client_Email { get; set; }
        public int Client2_ID { get; set; }
        public string Client2_Name { get; set; }
        public string Client2_AFM { get; set; }
        public DateTime Client2_DoB { get; set; }
        public string Client2_Email { get; set; }
        public int Status { get; set; }
        public string DateIns { get; set; }
    }
}
