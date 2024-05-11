using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISPWebAPI
{
    public class WebUsersMobile
    {
		public string email { get; set; }
		public string mobile_phone { get; set; }
		public string password { get; set; }
		public string pin { get; set; }
		public int WU_ID { get; set; }
		public string Manufacturer { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Board { get; set; }
		public string Hardware { get; set; }
		public string Android { get; set; }
		public string ScreenResolution { get; set; }
		public string ScreenDensity { get; set; }
		public string Host { get; set; }
		public string Version { get; set; }
		public string API_level { get; set; }
		public string Build_ID { get; set; }
		public string Build_Time { get; set; }
		public string Fingerprint { get; set; }
		public string PhoneType { get; set; }
		public string NetworkCountryISO { get; set; }
		public string NetworkOperatorName { get; set; }
		public string DeviceId { get; set; }
		public string DeviceSoftwareVersion { get; set; }
		public string SimCountryIso { get; set; }
		public string SimOperatorName { get; set; }
		public string SimSerialNumber { get; set; }
		public string Imei { get; set; }
		public string Meid { get; set; }
		public string MmsUAProfUrl { get; set; }
		public string MmsUserAgent { get; set; }
		public string SubscriberId { get; set; }
		public string TypeAllocationCode { get; set; }
	}
}
