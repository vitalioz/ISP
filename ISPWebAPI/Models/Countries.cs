using System;

namespace ISPWebAPI.Models
{
    public class Countries
    {
        public int ID { get; set; }
        public int Tipos { get; set; }
		public string Code { get; set; }
		public string Code3 { get; set; }
		public string Title { get; set; }
		public string TitleGreek { get; set; }
		public int CountriesGroup_ID { get; set; }
		public int InvestGeography_ID { get; set; }
		public string PhoneCode { get; set; }
    }
}
