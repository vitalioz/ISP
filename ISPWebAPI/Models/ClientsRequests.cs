using System;

namespace ISPWebAPI.Models
{
    public class ClientsRequests
    {
        public int ID { get; set; }
		public int Client_ID { get; set; }
		public string Group_ID { get; set; }
		public int Tipos { get; set; }
		public int Aktion { get; set; }
		public int Source_ID { get; set; }
		public string Description { get; set; }
		public string Warning { get; set; }
		public DateTime DateIns { get; set; }
		public DateTime DateWarning { get; set; }
		public DateTime DateClose { get; set; }
		public int User_ID { get; set; }
		public int Status { get; set; }
		public int VideoChatStatus { get; set; }
		public string VideoChatFile { get; set; }
	}
}

