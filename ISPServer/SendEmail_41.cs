using System;
using System.Data;
using System.Net;
using Newtonsoft.Json;
using Core;

namespace ISPServer
{
    public class SendEmail_41
    {
        int iResult = 0;
        string sEMailSender = Global.EMail_Sender;        // "support@hellasfin.gr";
        string sEMailFrom = Global.EMail_Username;        // "v.kougioumtzidis@hellasfin.gr";        
        string sPasswordFrom = Global.EMail_Password;     // "Kv_26101959";

        public int Go(DataRow dtRow)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var emailData = JsonConvert.DeserializeObject<emailData>(dtRow["Parameters"].ToString());

                SendEmail EMail = new SendEmail();
                EMail.EmailSender = sEMailSender;
                EMail.EmailFrom = sEMailFrom;
                EMail.PasswordFrom = sPasswordFrom;
                EMail.EmailRecipient = emailData.email + "";
                EMail.CC = emailData.cc + "";
                EMail.Attachments = emailData.att + "";
                EMail.Subject = emailData.subject + "";
                EMail.Body = emailData.body + "";

                iResult = EMail.Go();
            }
            catch (Exception ex)
            {
                string sMessage = ex.Message;
                iResult = 0;
            }
            finally { }
            return iResult;
        }
        public class emailData
        {
            public string email { get; set; }
            public string subject { get; set; }
            public string cc { get; set; }
            public string att { get; set; }
            public string body { get; set; }
        }
    }
}
