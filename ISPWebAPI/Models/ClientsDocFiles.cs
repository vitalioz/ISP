using System;

namespace ISPWebAPI.Models
{
    public class ClientsDocFiles
    {
        public int ID { get; set; }
        public int Client_ID { get; set; } = 0;
        public int Contract_ID { get; set; } = 0;
        public int PreContract_ID { get; set; } = 0;
        public int DocTypes_ID { get; set; } = 0;
        public int DMS_Files_ID { get; set; } = 0;
        public string FileName { get; set; } = "";
        public int TaxYear { get; set; } = 0;
        public int Status { get; set; } = 0;
    }
}
