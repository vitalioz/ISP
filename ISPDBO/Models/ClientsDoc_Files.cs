using System.ComponentModel.DataAnnotations;

namespace ISPDBO.Models
{
	public class ClientsDoc_Files
	{
		public int ID { get; set; }
		[Required]
		public int Client_ID { get; set; }
		[Required]
		public int PreContract_ID { get; set; }
		[Required]
		public int Contract_ID { get; set; }
		[Required]
		public int DocTypes { get; set; }
		[Required]
		public string DocTypes_Title { get; set; }
		[Required]
		public int PD_Group_ID { get; set; }
		[Required]
		public int OldFile { get; set; }
		[Required]
		public System.DateTime DateIns { get; set; }
		[Required]
		public int User_ID { get; set; }
		public int DMS_Files_ID { get; set; }
		public int Status { get; set; }
		public string FileName { get; set; }
		public string FilePath { get; set; }
	}
}