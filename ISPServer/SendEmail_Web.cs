using System;
using System.IO;
using System.Data;
using System.Net;
//using System.Net.Mail;
using System.Threading;
using Newtonsoft.Json;
using Aspose.Email;
using Aspose.Email.Exchange;
using Aspose.Email.Mail;

namespace ISPServer
{
    public class SendEmail_Web
    {
        int iResult = 0;
        string sEMailFrom = "v.kougioumtzidis@hellasfin.gr";
        string sEMailFrom2 = "support@hellasfin.gr";
        string sPasswordFrom = "Kv_26101959";
        //string sSubject = "HF DBO";
        public int Go(DataRow dtRow)
        {
            bool bResult = true;

            var emailData = JsonConvert.DeserializeObject<emailData>(dtRow["Parameters"] + "");
            bResult = SendMail_Web(sEMailFrom2, sEMailFrom, sPasswordFrom, emailData.email, "", emailData.subject, emailData.body, "");
            if (bResult) iResult = 1;
            return iResult;
        }
        bool SendMail_Web(string strSender, string strUsername, string strPassword, string strRecipient, string strCC, string strSubject,
                      string strBody, string sAttachFiles)
        {
            bool bResult = true;
            int i = 0, j = 0;
            string sTemp = "";
            string sMessage = "";

            try
            {
                //MessageBox.Show("Point 001  " + strUsername + "   " + strPassword);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                IEWSClient client = EWSClient.GetEWSClient("https://outlook.office365.com/ews/exchange.asmx", strUsername, strPassword, "office365.com");
                //MessageBox.Show("Point 002");                
                client.Timeout = 1500000;
                //MessageBox.Show("Point 003");

                j = j + 1;
                //MessageBox.Show("Point 1");
                // Create instance of type MailMessage
                MailMessage msg = new MailMessage();

                //sAttachFiles = "";
                //MessageBox.Show("Start attach \n " + sAttachFiles);
                string[] tokens = sAttachFiles.Split('~');

                for (i = 0; i <= tokens.Length - 2; i++)
                {
                    sTemp = tokens[i] + "";
                    //MessageBox.Show(sTemp);
                    if (Path.GetFileName(sTemp) != "")
                    {
                        if (File.Exists(sTemp)) msg.AddAttachment(new Attachment(sTemp));
                        else sMessage = sMessage + "\n" + "Attach File Not Found " + tokens[i] + "\n";
                    }
                }

                j = j + 1;
                //MessageBox.Show("Point 2");
                // Send the message
                msg.From = strSender;
                msg.To = strRecipient;
                if (strCC.Length > 0) msg.CC = strCC;
                msg.Subject = strSubject;
                msg.HtmlBody = strBody;
                //MessageBox.Show("Point 3");
                j = j + 1;
                client.Send(msg);
                Thread.Sleep(2000);

                //MessageBox.Show("SENDER=" & strSender & vbCrLf & "RECIPIENT=" & strRecipient);

                //MessageBox.Show("Point 4");
                j = j + 1;
                bResult = true;
            }
            catch (Exception z)
            {
                sMessage = (sMessage + " " + z.Message).Trim();
                //MessageBox.Show(sMessages);
                j = 999;
                bResult = false;
                //MessageBox.Show("Point 5");
            }
            finally
            {
                //MessageBox.Show("j=" + j);
                switch (j)
                {
                    case 0:
                        sMessage = "Can't create EWSClient. Check username and password of e-mail account";
                        bResult = false;
                        break;
                    case 1:
                        sMessage = "Can't attach files";
                        bResult = false;
                        break;
                    case 2:
                        sMessage = "Message can't send";
                        bResult = false;
                        break;
                    case 3:
                        // sMessage = "";
                        bResult = true;
                        break;
                }
                //MessageBox.Show("Point 6");
            }
            return bResult;
        }

        class emailData
        {
            public string email { get; set; }
            public string subject { get; set; }
            public string body { get; set; }
        }

    }
}
