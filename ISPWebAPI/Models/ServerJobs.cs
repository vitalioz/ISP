using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISPWebAPI.Models
{
    public class ServerJobs
    {
        public int ID { get; set; }
        public int JobType_ID { get; set; }
        public int Source_ID { get; set; }
        public string Parameters { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }
        public string PubKey { get; set; }
        public string PrvKey { get; set; }
        public int Attempt { get; set; }
        public int Status { get; set; }

    }
}
