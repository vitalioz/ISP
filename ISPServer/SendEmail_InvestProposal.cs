using System;
using System.Data;
using System.Net;
using System.Net.Mail;
using Newtonsoft.Json;

namespace ISPServer
{
    class SendEmail_InvestProposal
    {
        int iResult = 0;
        string sEMailFrom = "v.kougioumtzidis@hellasfin.gr";
        string sPasswordFrom = "Kv_26101959";
        string sSubject = "HF DBO";

        public int Go(DataRow dtRow)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var emailData = JsonConvert.DeserializeObject<emailData>(dtRow["Parameters"].ToString());
                MailMessage mailMessage = new MailMessage(sEMailFrom, emailData.email, sSubject, emailData.body);
                mailMessage.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587);
                smtpClient.EnableSsl = true;

                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(sEMailFrom, sPasswordFrom);
                smtpClient.Timeout = 20000;
                smtpClient.Send(mailMessage);
                iResult = 1;
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
            public string body { get; set; }
        }
    }
}
