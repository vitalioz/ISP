namespace ISPWebAPI.Models
{
    public class Interface
    {
		public int    ID { get; set; } = 0;
		public int    Client_ID { get; set; } = 0;
		public int    Client2_ID { get; set; } = 0;
		public string Client_Title { get; set; } = "";
		public string Client2_Title { get; set; } = "";
		public string Surname { get; set; } = "";
		public string Firstname { get; set; } = "";
		public string AFM { get; set; } = "";
		public string EMail { get; set; } = "";
		public string Mobile_phone { get; set; } = "";
		public string Password { get; set; } = "";
		public int    WU_ID { get; set; } = 0;
		public string Manufacturer { get; set; } = "";
		public string Brand { get; set; } = "";
		public string Model { get; set; } = "";
		public string Board { get; set; } = "";
		public string Hardware { get; set; } = "";
		public string Unique_ID { get; set; } = "";
		public string ScreenResolution { get; set; } = "";
		public string ScreenDensity { get; set; } = "";
		public string Host { get; set; } = "";
		public string Version { get; set; } = "";
		public string API_level { get; set; } = "";
		public string Build_ID { get; set; } = "";
		public string Build_Time { get; set; } = "";
		public string Fingerprint { get; set; } = "";
		public string PhoneType { get; set; } = "";
		public string NetworkCountryISO { get; set; } = "";
		public string NetworkOperatorName { get; set; } = "";
		public string DeviceId { get; set; } = "";
		public string DeviceSoftwareVersion { get; set; } = "";
		public string SimCountryIso { get; set; } = "";
		public string SimOperatorName { get; set; } = "";
		public string SimSerialNumber { get; set; } = "";
		public string Imei { get; set; } = "";
		public string Meid { get; set; } = "";
		public string MmsUAProfUrl { get; set; } = "";
		public string MmsUserAgent { get; set; } = "";
		public string SubscriberId { get; set; } = "";
		public string TypeAllocationCode { get; set; } = "";
		public string OS { get; set; } = "";
		public string Video { get; set; } = "";
		public int Status { get; set; } = 0;
		public int Result { get; set; } = 0;
	}
}
