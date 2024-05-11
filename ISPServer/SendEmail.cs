using System;
using System.IO;
using System.Net;
using System.Net.Mail;
namespace ISPServer
{
    class SendEmail
    {
        string sEMailSender, sEmailFrom, sPasswordFrom, sEmailRecipient, sCC, sAttachments, sSubject, sBody, sMessage = "";
        int iResult = 0;
        public int Go()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            MailMessage mailMessage = new MailMessage(sEMailSender, sEmailRecipient, sSubject, sBody);
            SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587);
            try
            {
                mailMessage.IsBodyHtml = true;
                string[] CCArray = sCC.Split(',');
                foreach (string CCEmail in CCArray)
                    if (CCEmail.Length > 0)
                        mailMessage.CC.Add(new MailAddress(CCEmail));

                string[] AttsArray = sAttachments.Split(',');
                foreach (string fileAtt in AttsArray)
                    if (fileAtt.Length > 0)
                        mailMessage.Attachments.Add(new Attachment(fileAtt.Replace("/", "\\")));


                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(sEmailFrom, sPasswordFrom);
                smtpClient.Timeout = 20000;
                smtpClient.Send(mailMessage);
                iResult = 1;
            }
            catch (SmtpFailedRecipientsException ex)
            {
                sMessage = ex.Message;
                iResult = 0;

                for (int i = 0; i < ex.InnerExceptions.Length; i++)
                {
                    SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                    if (status == SmtpStatusCode.MailboxBusy || status == SmtpStatusCode.MailboxUnavailable)
                    {
                        // Console.WriteLine("Delivery failed - retrying in 5 seconds.");
                        System.Threading.Thread.Sleep(5000);
                        smtpClient.Send(mailMessage);
                        iResult = 1;
                    }
                    else
                    {
                        //  Console.WriteLine("Failed to deliver message to {0}", ex.InnerExceptions[i].FailedRecipient);
                        sMessage = ex.Message;
                        throw ex;
                    }
                }
            }
            catch (Exception z)
            {
                sMessage = z.Message;
            }
            finally {
                    }
            return iResult;
        }

        public string EmailSender { get { return sEMailSender; } set { sEMailSender = value; } }
        public string EmailFrom { get { return sEmailFrom; } set { sEmailFrom = value; } }
        public string PasswordFrom { get { return sPasswordFrom; } set { sPasswordFrom = value; } }
        public string EmailRecipient { get { return sEmailRecipient; } set { sEmailRecipient = value; } }
        public string CC { get { return sCC; } set { sCC = value; } }
        public string Attachments { get { return sAttachments; } set { sAttachments = value; } }
        public string Subject { get { return sSubject; } set { sSubject = value; } }
        public string Body { get { return sBody; } set { sBody = value; } }
        public int Result { get { return iResult; } set { iResult = value; } }
        public string Message { get { return sMessage; } set { sMessage = value; } }
    }
}
