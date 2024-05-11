using System.ComponentModel.DataAnnotations;
namespace ISPDBO.Models
{
    public class ClientsRequests
    {
        public int ID { get; set; }
        [Required]
        public int Client_ID { get; set; }
        public int Tipos { get; set; }
        public int Source_ID { get; set; }
        public string Description { get; set; }
        public System.DateTime DateIns { get; set; }
        public System.DateTime DateClose { get; set; }
        public int Status { get; set; }
    }
}
