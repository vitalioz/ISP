using System.ComponentModel.DataAnnotations;

namespace ISPDBO.Models
{
    public class DocTypes
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
