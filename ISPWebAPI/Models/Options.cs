using System;

namespace ISPWebAPI.Models
{
	public class Options
	{
		public string EMail_Sender { get; set; }
		public string EMail_Username { get; set; }
		public string EMail_Password { get; set; }
		public string NonReplay_Sender { get; set; }
		public string NonReplay_Username { get; set; }
		public string NonReplay_Password { get; set; }
		public string Request_Sender { get; set; }
		public string Request_Username { get; set; }
		public string Request_Password { get; set; }
		public string Support_Sender { get; set; }
		public string Support_Username { get; set; }
		public string Support_Password { get; set; }
		public string EMail_BO_Receiver { get; set; }
		public string SMS_Username { get; set; }
		public string SMS_Password { get; set; }
		public string SMS_From { get; set; }
		public string FTP_Username { get; set; }
		public string FTP_Password { get; set; }
		public string RS_Address { get; set; }
		public string RS_Username { get; set; }
		public string RS_Password { get; set; }
		public int TaxDeclarations1Year { get; set; }
		public int TaxDeclarationsLastYear { get; set; }
		public int RequestsPeriod1 { get; set; }
		public int RequestsPeriod2 { get; set; }

	}
}

