using System;
using System.Data;
using System.Net.Http;
using Newtonsoft.Json;
using Core;
using System.Collections.Generic;

namespace ISPServer
{
    public class SendSMS
    {
        int iResult = 0;
        string sAPIKey = "OTkyQjMxMjAtQzEyNi00MTU0LUJFNEItNDRFNTFEMjk0Q0VF";
        public int Go(DataRow dtRow)
        {
            try
            {
                var mobileData = JsonConvert.DeserializeObject<MobileData>(dtRow["Parameters"] + "");
                var url = "https://services.yuboto.com/omni/v1/Send?phonenumbers=" + mobileData.mobile + "&sms.sender=HellasFin&sms.text=" + mobileData.message + "&apiKey=" + sAPIKey;
                var client = new HttpClient();
                var response = client.PostAsync(url, null);
             

                var sTemp = response.Result.Headers;
                Global.AddLogsRecord(Global.User_ID, DateTime.Now, 4, sTemp.ToString());

                if (response.Result.ReasonPhrase == "OK") iResult = 1;
            }
            catch (Exception ex)
            {
                sAPIKey = ex.ToString();
                iResult = 0;
            }
            finally { }
            return iResult;
        }
        public class MobileData
        {
            public string mobile { get; set; }
            public string message { get; set; }
        }



        public class SendRequest { 
            public string[] phonenumbers { get; set; } 
            public int? dateinToSend { get; set; } 
            public int? timeinToSend { get; set; } 
            public bool dlr { get; set; } 
            public string callbackUrl { get; set; } 
            public string option1 { get; set; } 
            public string option2 { get; set; } 
            public SmsObj sms { get; set; } 
            public ViberObj viber { get; set; } 
        }

        public class SmsObj { 
            public string sender { get; set; } 
            public string text { get; set; } 
            public int validity { get; set; } 
            public string typesms { get; set; } 
            public bool longsms { get; set; } 
            public int priority { get; set; } 
            public TwoFaObj TwoFa { get; set; } 
        }

        public class ViberObj
        {
            public string sender { get; set; }
            public string text { get; set; }
            public int validity { get; set; }
            public string expiryText { get; set; }
            public string buttonCaption { get; set; }
            public string buttonAction { get; set; }
            public string image { get; set; }
            public int priority { get; set; }
            public TwoFaObj TwoFa { get; set; }
        }

        public class TwoFaObj { 
            public int pinLength { get; set; } // accepted values between 4-32
            public string pinType { get; set; } //> accepted values between numeric, alpha, alphanumeric 
            public bool isCaseSensitive { get; set; } 
            public int expiration { get; set; } //> accepted values between 60-600 (in seconds)
        }

        public class SendResponse { 
            public int ErrorCode { get; set; } 
            public string ErrorMessage { get; set; } 
            public List<MessageStatus> Message { get; set; } 
        }

        public class MessageStatus { 
            public string id { get; set; } 
            public string channel { get; set; } 
            public string phonenumber { get; set; } 
            public string status { get; set; } 
        }
    }
}
