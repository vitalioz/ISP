using System;
using System.Data;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using C1.Win.C1FlexGrid;
using Core;

namespace ISPServer
{
    public partial class frmMain : Form
    {
        int i, iStatus;
      
        DateTime dTemp;
        bool bResult = true;
        Hashtable htTasks = new Hashtable();
        clsClients Clients = new clsClients();
        clsServerJobs ServerJobs = new clsServerJobs();
        public frmMain()
        {
            InitializeComponent();

            WindowState = FormWindowState.Minimized;
            Hide();

            htTasks.Add(1, "Create PDF");
            htTasks.Add(11, "Create Folder");
            htTasks.Add(15, "Copy File from DMSTransferPoint to DMS");
            htTasks.Add(41, "Send e-mail");
            htTasks.Add(42, "Send SMS");
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            dFrom.Value = DateTime.Now.Date;
            dTo.Value = DateTime.Now.Date;

            Global Global = new Global();
            Global.InitConnectionString();

            Timer1.Interval = 2000;
            Timer1.Start();

            i = 0;
            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgList.DrawMode = DrawModeEnum.OwnerDraw;
            //fgList.RowColChange += new EventHandler(fgList_RowColChange);
            //fgList.OwnerDrawCell += fgList_OwnerDrawCell;

            DefineList();
        }
        private void DefineList()
        {
            ServerJobs = new clsServerJobs();
            ServerJobs.DateStart = dFrom.Value.Date;
            ServerJobs.DateFinish = dTo.Value.Date;
            ServerJobs.JobType_ID = 0;
            ServerJobs.Source_ID = 0;
            ServerJobs.Status = -1;
            ServerJobs.GetList();

            ShowList();
        }

        private void chkAllRecords_CheckedChanged(object sender, EventArgs e)
        {
            ShowList();
        }
        private void ShowList()
        {
            fgList.Redraw = false;
            fgList.Rows.Count = 1;

            foreach (DataRow dtRow in ServerJobs.List.Rows)
            {
                iStatus = Convert.ToInt32(dtRow["Status"]);

                if (chkAllRecords.Checked || iStatus != 1)
                {
                    i = i + 1;
                    fgList.AddItem(i + "\t" + htTasks[Convert.ToInt32(dtRow["JobType_ID"])] + "\t" + dtRow["Parameters"] + "\t" + dtRow["Source_ID"] + "\t" +
                                   dtRow["DateStart"] + "\t" + dtRow["DateFinish"] + "\t" + iStatus + "\t" + dtRow["ID"]);
                }
            }
            fgList.Redraw = true;
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            string sTemp = "", sTemp1 = "";

            clsServerJobs ServerJobs = new clsServerJobs();
            ServerJobs.DateStart = dFrom.Value;
            ServerJobs.DateFinish = dTo.Value;
            ServerJobs.JobType_ID = 0;
            ServerJobs.Source_ID = 0;
            ServerJobs.Status = 0;
            ServerJobs.GetList();

            foreach (DataRow dtRow in ServerJobs.List.Rows)
            {
                // JobType_ID - type of Server Job

                // --- Files and Folders functions--------
                // 11 - Create Folder
                // 12 - Rename Folder
                // 13 - Check DMS Folders
                // 14 - Copy File
                // 15 - Copy File from DMSTransferPoint to DMS. Parameters file_name: <source_file_name>; target_folder: <target_folder>  
                // 16 - upload file from "local" H: that really is remote server folder to local server 10.0.0.15\DMS
                // 17 - upload file to remote server http://dms.hellasfin.gr
                // 18 - download file from remote server http://dms.hellasfin.gr into DMS 

                // --- Communication function --------------
                // 41  - send e-mail    Params: <target e-mail>, <body text>
                // 42  - send SMS       Params: <mobile number>, <sms text>
                // 43  - send e-mail from Investment Proposal Params: II_ID
                // 44  - send e-mail from Contracts Monitoring Params: Contract_ID
                // 45  - send e-mail from Client Informing

                //--- Create, Read PDF files ---------------
                // 62  - RM Activities Report
                // 63  - read PDF content and upload file to server
                // 64  - create PDF for Terms Agreements
                // 65  - create PDF for Request Open Account
                // 66  - create Questionnarie Diagnose report
                // 67  - create PDF for Risks Agreements
                // 68  - create PDF for PriceListAgreements
                // 69  - create PDF for δήλωση ότι αυτός θα είναι ο εκπρόσωπος πελατών 

                dTemp = DateTime.Now;

                try
                {
                    switch (Convert.ToInt32(dtRow["JobType_ID"]))
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 12:
                            break;
                        case 15:
                            var copy_file = JsonConvert.DeserializeObject<CopyFile>(dtRow["Parameters"] + "");

                            sTemp = "\\10.0.0.3\\HF_Departments\\1_Investment_Proposals\\" + copy_file.file_name;
                            sTemp1 = "C:\\DMS\\" + copy_file.target_folder + copy_file.file_name;

                            if (File.Exists(sTemp)) {
                                if (File.Exists(sTemp1)) 
                                    sTemp1 = Path.GetDirectoryName(sTemp1) + "\\" + Path.GetFileNameWithoutExtension(sTemp1) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sTemp1);
                                File.Copy(sTemp, sTemp1);

                                ServerJob_EditStatus(Convert.ToInt32(dtRow["ID"]));
                            }
                            break;
                        case 16:
                            var upload_file = JsonConvert.DeserializeObject<UploadFile>(dtRow["Parameters"] + "");

                            sTemp = "H:\\" + upload_file.file_name;

                            Clients = new clsClients();
                            Clients.Record_ID = upload_file.client_id;
                            Clients.EMail = "";
                            Clients.Mobile = "";
                            Clients.AFM = "";
                            Clients.DoB = Convert.ToDateTime("1900/01/01");
                            Clients.GetRecord();
                            if (Clients.Type == 1) sTemp1 = (Clients.Surname + " " + Clients.Firstname).Trim();
                            else sTemp1 = Clients.Surname.Trim();

                            sTemp1 = "C:\\DMS\\Customers\\" + sTemp1 + "\\" + upload_file.file_name;

                            if (File.Exists(sTemp))
                            {
                                if (File.Exists(sTemp1))
                                    sTemp1 = Path.GetDirectoryName(sTemp1) + "\\" + Path.GetFileNameWithoutExtension(sTemp1) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sTemp1);
                                File.Copy(sTemp, sTemp1);

                                ServerJob_EditStatus(Convert.ToInt32(dtRow["ID"]));
                            }
                            break;
                        case 17:
                            break;
                        case 41:
                            var emailData = JsonConvert.DeserializeObject<EmailData>(dtRow["Parameters"] + "");
                            bResult = Global.SendMail_Web("v.kougioumtzidis@hellasfin.gr", "v.kougioumtzidis@hellasfin.gr", "Kv_26101959", emailData.email, "",
                                                          "Verify Email in DigitalOffice ", emailData.body, "", "smtp.office365.com", "DigitalBackOffice", 0, "");
                            if (bResult)
                                ServerJob_EditStatus(Convert.ToInt32(dtRow["ID"]));

                            break;
                        case 42:
                            var mobileData = JsonConvert.DeserializeObject<MobileData>(dtRow["Parameters"] + "");
                            //string URL = "http://services.yuboto.com/sms/api/smsc.asp?user=" + Global.SMS_Username + "&pass=" + Global.SMS_Password +
                            //             "&action=send&from=" + Global.SMS_From + "&to=" + mobileData.mobile + "&text=" + mobileData.message;
                            string URL = "https://services.yuboto.com/omni/v1/Send?phonenumbers=" + mobileData.mobile + "&sms.sender=HellasFin&sms.text=" + mobileData.message + "&apiKey=OTkyQjMxMjAtQzEyNi00MTU0LUJFNEItNDRFNTFEMjk0Q0VF";

                            var task = Task.Run(() => SendSMS2(URL));
                            if (task.Wait(TimeSpan.FromSeconds(3)))
                                ServerJob_EditStatus(Convert.ToInt32(dtRow["ID"]));
                            //else
                                //throw new Exception("Timed out");

                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {

                }
            }
        }
        private void SendSMS(string sURL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sURL);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        }
        private void SendSMS2(string sURL)
        {
            //var person = new Person("John Doe", "gardener");

            //var json = JsonConvert.SerializeObject(person);
            //var data = new StringContent(json, Encoding.UTF8, "application/json");
            //var url = "https://services.yuboto.com/omni/v1/Send?phonenumbers=306972288803&sms.sender=HellasFin&sms.text={Ειναι Δοκιμη}&apiKey=OTkyQjMxMjAtQzEyNi00MTU0LUJFNEItNDRFNTFEMjk0Q0VF";
            var client = new HttpClient();

            var response = client.PostAsync(sURL, null);

            string result = ""; //response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);
        }
        private void ServerJob_EditStatus(int iRecord_ID)
        {
            clsServerJobs ServerJobs = new clsServerJobs();
            ServerJobs.Record_ID = iRecord_ID;
            ServerJobs.GetRecord();
            ServerJobs.DateStart = dTemp;
            ServerJobs.DateFinish = DateTime.Now;
            ServerJobs.Status = 1;
            ServerJobs.EditRecord();
        }
   
        private string RemoteServer2_UploadFile(string sSourceFullFileName, string sTargetPath, string sNewFileName)
        {
            return "";
        }
        public class EmailData
        {
            public string email  { get; set; }
            public string body { get; set; }
        }
        public class MobileData
        {
            public string mobile { get; set; }
            public string message { get; set; }
        }
        public class CopyFile
        {
            public string file_name { get; set; }
            public string target_folder { get; set; }
        }
        public class UploadFile
        {
            public string file_name { get; set; }
            public int client_id { get; set; }
        }
    }
}
