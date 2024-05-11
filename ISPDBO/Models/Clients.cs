using System.ComponentModel.DataAnnotations;

namespace ISPDBO.Models
{
    public class Clients
    {
        public int ID { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Firstname { get; set; }
    }
}
