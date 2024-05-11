using System.ComponentModel.DataAnnotations;

namespace ISPDBO.Models
{
    public class Clients_MetaData
    {
        public int ID { get; set; }
        [Required]
        public int Client_ID { get; set; }
        public int PD_Status { get; set; }
        public string PD_Request { get; set; }
    }
}
