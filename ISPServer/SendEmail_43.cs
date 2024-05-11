using System;
using System.Data;
using System.Net;
using Newtonsoft.Json;
using Core;

namespace ISPServer
{
    class SendEmail_43
    {
        int i = 0, iResult = 0;
        string[] tmpBrray;
        string sClientName;
        string sAttachFiles;
        string sDocFilesPath = Global.DocFilesPath_Win;   // "C:\DMS"
        string sEMailSender = Global.EMail_Sender;        // "backoffice@hellasfin.gr";
        string sEMailFrom = Global.EMail_Username;        // "v.kougioumtzidis@hellasfin.gr";        
        string sPasswordFrom = Global.EMail_Password;     // "Kv_26101959";
        public int Go(DataRow dtRow)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var recordID = JsonConvert.DeserializeObject<recordID>(dtRow["Parameters"].ToString());

                clsInformings Informings = new clsInformings();
                Informings.Record_ID = recordID.informing_id;
                Informings.GetRecord();
                sClientName = Informings.ClientName.Replace(".", "_");
                tmpBrray = Informings.AttachedFiles.Split('~');

                sAttachFiles = "";
                switch (Informings.Source_ID)
                {
                    case 2:               // 2-DailyInform
                        if (Informings.FileName != "")
                            sAttachFiles = sDocFilesPath + "/Customers/" + sClientName + "/Informing/" + Informings.FileName + ",";

                        for (i = 0; i <= tmpBrray.Length - 1; i++)
                            if ((tmpBrray[i] + "").Trim().Length > 0)
                                sAttachFiles = sAttachFiles + sDocFilesPath + "/Customers/" + sClientName + "/Informing/" + tmpBrray[i] + ",";

                        break;
                    case 3:               // 3-ManFeesInform 
                        if (Informings.FileName != "")
                            sAttachFiles = sDocFilesPath + "/Customers/" + sClientName + "/Invoices/" + Informings.FileName + ",";

                        for (i = 0; i <= tmpBrray.Length - 1; i++)
                            if ((tmpBrray[i] + "").Trim().Length > 0)
                                sAttachFiles = sAttachFiles + sDocFilesPath + "/Customers/" + sClientName + "/Informing/" + tmpBrray[i] + ",";

                        break;
                    case 4:              // 4 - MiscInform                        
                        for (i = 0; i <= tmpBrray.Length - 1; i++)
                            if ((tmpBrray[i] + "").Trim().Length > 0)
                                sAttachFiles = sAttachFiles + sDocFilesPath + "/Customers/" + sClientName + "/Informing/" + tmpBrray[i] + ",";
                        //                                                                                  it isn't FileName is Code           
                        break;
                    case 6:              // 6 - InvoiceRTO
                        if (Informings.FileName != "")
                            sAttachFiles = sDocFilesPath + "/Customers/" + sClientName + "/Invoices/" + Informings.FileName + ",";

                        for (i = 0; i <= tmpBrray.Length - 1; i++)
                            if ((tmpBrray[i] + "").Trim().Length > 0)
                                sAttachFiles = sAttachFiles + sDocFilesPath + "/Customers/" + sClientName + "/Invoices/" + tmpBrray[i] + ",";

                        break;
                    case 7:               // 7-AdminFeesInform
                        break;
                    case 8:               // 8-PeriodicalEvaluation Inform
                        if (Informings.FileName != "")
                            sAttachFiles = sDocFilesPath + "/Customers/" + sClientName + "/Informing/" + Informings.FileName + ",";

                        break;
                    case 9:               // 9 - ExPostCost                        
                        for (i = 0; i <= tmpBrray.Length - 1; i++)
                            if ((tmpBrray[i] + "").Trim().Length > 0)
                                sAttachFiles = sAttachFiles + sDocFilesPath + "/Customers/" + sClientName + "/Informing/" + tmpBrray[i] + ",";
                        //                                                                                   it isn't FileName is Code
                        break;
                    case 10:              // 10 - Custody Fees
                        if (Informings.FileName != "")
                            sAttachFiles = sDocFilesPath + "/Customers/" + sClientName + "/Invoices/" + Informings.FileName + ",";

                        for (i = 0; i <= tmpBrray.Length - 1; i++)
                            if ((tmpBrray[i] + "").Trim().Length > 0)
                                sAttachFiles = sAttachFiles + sDocFilesPath + "/Customers/" + sClientName + "/Invoices/" + tmpBrray[i] + ",";

                        break;
                    default:
                        sAttachFiles = Informings.AttachedFiles.Replace("~", ",");
                        break;
                }

                SendEmail EMail = new SendEmail();
                EMail.EmailSender = sEMailSender;
                EMail.EmailFrom = sEMailFrom;
                EMail.PasswordFrom = sPasswordFrom;
                EMail.EmailRecipient = Informings.ClientData + "";
                EMail.CC = Informings.CC + "";
                EMail.Attachments = sAttachFiles;
                EMail.Subject = Informings.Subject + "";
                EMail.Body = Informings.Body + "";

                iResult = EMail.Go();

                //--- edit Informings record --------------------------
                Informings.SentAttempts = Informings.SentAttempts + 1;
                if (iResult == 1)
                {
                    Informings.Status = 1;
                    Informings.DateSent = DateTime.Now.ToString();
                    Informings.SentMessage = "OK";
                }
                Informings.EditRecord();
            }
            catch (Exception ex)
            {
                string sMessage = ex.Message;
                iResult = 0;
            }
            finally { }
            return iResult;
        }
        public class recordID
        {
            public int informing_id { get; set; }
        }
    }
}
