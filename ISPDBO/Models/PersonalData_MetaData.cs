using System.ComponentModel.DataAnnotations;

namespace ISPDBO.Models
{
    public class PersonalData_MetaData
    {
		public int ID { get; set; }
		[Required]
		public int Num { get; set; }
		public string Title { get; set; }
		public string Value { get; set; }
		public int Mandatory { get; set; }
		public int DocCount { get; set; }
		public int Status { get; set; }
	}
}
