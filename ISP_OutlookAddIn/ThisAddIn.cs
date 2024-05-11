using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using Core;

namespace ISP_OutlookAddIn
{
    public partial class ThisAddIn
    {
        Outlook.NameSpace outlookNameSpace;
        Outlook.MAPIFolder inbox;
        Outlook.Items items;
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            outlookNameSpace = this.Application.GetNamespace("MAPI");
            inbox = outlookNameSpace.GetDefaultFolder(
                    Microsoft.Office.Interop.Outlook.
                    OlDefaultFolders.olFolderInbox);

            items = inbox.Items;
            items.ItemAdd +=
                new Outlook.ItemsEvents_ItemAddEventHandler(items_ItemAdd);
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see https://go.microsoft.com/fwlink/?LinkId=506785
        }
        void items_ItemAdd(object Item)
        {
            string sTemp = "";
            Global Global = new Global();
            Global.InitConnectionString();

            Outlook.MailItem mail = (Outlook.MailItem)Item;
            if (Item != null)
            {
                sTemp = "Subject: " + mail.Subject + " from: " + mail.SenderName + " from email: " + mail.SenderEmailAddress + " body: " + mail.Body + "  body format: " + mail.BodyFormat;  
            }


            clsServerJobs ServerJobs = new clsServerJobs();
            ServerJobs.JobType_ID = 44;                                             // 44  - send e-mail from Investment Proposal Params: II_ID
            ServerJobs.Source_ID = 0;
            ServerJobs.Parameters = sTemp;
            ServerJobs.DateStart = DateTime.Now;
            ServerJobs.DateFinish = System.Convert.ToDateTime("1900/01/01");
            ServerJobs.PubKey = "";
            ServerJobs.PrvKey = "";
            ServerJobs.Attempt = 0;
            ServerJobs.Status = 0;
            ServerJobs.InsertRecord();

            mail.SaveAs(@"C:\AAA\e-mail_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".msg");
            if (mail.Attachments.Count > 0)
            {
                for (int i = 1; i <= mail.Attachments.Count; i++)
                {                    
                    mail.Attachments[i].SaveAsFile(@"C:\AAA\" + mail.Attachments[i].FileName);
                }
            }

            

        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
