using System.ComponentModel.DataAnnotations;

namespace ISPDBO.Models
{
    public class ServerJobs
    {
        public int ID { get; set; }
        [Required]
        public int JobType_ID { get; set; }
        [Required]
        public int Source_ID { get; set; }
        public string Parameters { get; set; }
        public System.DateTime DateStart { get; set; }
        public System.DateTime DateFinish { get; set; }
        public string PubKey { get; set; }
        public string PrvKey { get; set; }
        public int Attempt { get; set; }
        public int Status { get; set; }
    }

}
