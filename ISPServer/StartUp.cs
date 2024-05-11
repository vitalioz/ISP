using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Core;

namespace ISPServer
{
    public class StartUp
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString);
        SqlCommand cmd;
        DataTable dtList;
        int i = 0, iMaxID = 0, iResult = 0, iFirstID = 0;
        clsExecutionReports ExecutionReports = new clsExecutionReports();
        public void MainPoint(bool b1min, bool bCheckDMSFolders)
        {
            SqlDataReader drList = null;

            Global Global = new Global();
            Global.User_ID = 45;
            Global.InitConnectionString();

            string s = Global.DMSTransferPoint;
            try
            {
                Global.GetServiceProvidersList();

                dtList = new DataTable();
                dtList.Columns.Add("ID", typeof(int));
                dtList.Columns.Add("JobType_ID", typeof(int));
                dtList.Columns.Add("Source_ID", typeof(int));
                dtList.Columns.Add("Parameters", typeof(string));
                dtList.Columns.Add("Attempt", typeof(int));
                dtList.Columns.Add("Status", typeof(int));
                
                conn.Open();
                cmd = new SqlCommand("SELECT * FROM ServerJobs WHERE ID > " + iMaxID + " AND DateStart < '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' AND Status = 0 AND Attempt < 3 ORDER BY ID", conn);
                cmd.CommandType = CommandType.Text;
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtList.Rows.Add(drList["ID"], drList["JobType_ID"], drList["Source_ID"], drList["Parameters"], drList["Attempt"], drList["Status"]);
                    //iMaxID = Convert.ToInt32(drList["ID"]);
                }
                conn.Close();

                //--- each 1 min check ExecutionReports tables for all ServiceProviders that have FIX protocol -------
                if (b1min)
                    foreach (DataRow dtRow in Global.dtServiceProviders.Rows)
                    {
                        if ((dtRow["FIX_DB"] + "").Trim() != "")
                        {
                            Global.connFIXStr = Global.FIX_DB_Server_Path + "database=" + dtRow["FIX_DB"];
                            ExecutionReports = new clsExecutionReports();
                            ExecutionReports.GetUncheckedList(DateTime.Now.AddDays(-4));
                            if (ExecutionReports.List.Rows.Count > 0)
                                dtList.Rows.Add(0, 81, 0, "", 0, 0);
                        }
                    }

                if (bCheckDMSFolders)
                    dtList.Rows.Add(0, 13, 0, "", 0, 0);                       // 13 - check DMS folders

                // JobType_ID - type of Server Job

                // --- Files and Folders functions--------
                // 11 - Create Folder
                // 12 - Rename Folder
                // 13 - Check DMS Folders
                // 14 - Copy File
                // 15 - Copy File from DMSTransferPoint to DMS. Parameters file_name: <source_file_name>; target_folder: <target_folder>  
                // 16 - upload file to local server 10.0.0.15
                // 17 - upload file to remote server http://dms.hellasfin.gr
                // 18 - download file from remote server http://dms.hellasfin.gr into DMS 
                // 19 - Advanced Copy File from DMSTransferPoint to DMS. Copy file with adding record into ClientsDocFiles & DMS_Files tables.
                //      Parameters file_name: <source_file_name>; target_folder: <source_full_file_name> <file_name> <file_type>  <target_folder> <client_id>

                // --- Communication function --------------
                // 41  - send e-mail    Params: <target e-mail>, <body text>
                // 42  - send SMS       Params: <mobile number>, <sms text>
                // 43  - send e-mail from Informings table
                // 44  - send e-mail from Investment Proposal Params: II_ID
                // 45  - send e-mail from Contracts Monitoring Params: Contract_ID
                // 46  - send e-mail from Αιτήματα Πελατών Params: <target e-mail>, <number of email template (subject and body text)>, <att>

                //--- Create, Read PDF files ---------------
                // 61  - create PDF for Invest.Proposal
                // 62  - RM Activities Report
                // 63  - read PDF content and upload file to server
                // 64  - create PDF for Terms Agreements
                // 65  - create PDF for Request Open Account
                // 66  - create Questionnarie Diagnose report
                // 67  - create PDF for Risks Agreements
                // 68  - create PDF for PriceListAgreements
                // 69  - create PDF for δήλωση ότι αυτός θα είναι ο εκπρόσωπος πελατών 

                //--- External Applications ---------------
                // 81  - read FIX-data (ExecutionReports)
                // 82  - check DMS folders

                clsLogger Log = new clsLogger();

                while (true)
                {
                    i = 0;
                    //Console.WriteLine("Start Loop. i = " + i);                    
                    foreach (DataRow dtRow in dtList.Select("Status = 0"))
                    {
                        iResult = -999;
                        i = i + 1;
                        switch (Convert.ToInt32(dtRow["JobType_ID"]))
                        {
                            case 1:
                                i = 0;
                                break;
                            case 11:                                                        // 11 - Create Folder
                                CreateFolder CreateF = new CreateFolder();
                                iResult = CreateF.Go(dtRow);
                                break;
                            case 12:                                                        // 12 - Rename Folder
                                RenameFolder RenameF = new RenameFolder();
                                iResult = RenameF.Go(dtRow);
                                break;
                            case 13:
                                //*************** UNDER CONSTRACTION **************//
                                CheckDMSFolders CheckDMSF = new CheckDMSFolders();
                                iResult = CheckDMSF.Go();
                                break;
                            case 15:
                                CopyFile CopyF = new CopyFile();
                                iResult = CopyF.Go(dtRow);
                                break;
                            case 16:
                                LocalUploadFile LocalUploadF = new LocalUploadFile();
                                iResult = LocalUploadF.Go(dtRow);
                                if (iResult != 1) dtRow["Attempt"] = "0";
                                break;
                            case 17:
                                FTPUploadFile FTPUploadF = new FTPUploadFile();
                                iResult = FTPUploadF.Go(dtRow);
                                break;
                            case 18:
                                DownloadFile DownloadF = new DownloadFile();
                                iResult = DownloadF.Go(dtRow);
                                break;
                            case 19:
                                AdvancedCopyFile AdvancedCopyF = new AdvancedCopyFile();
                                iResult = AdvancedCopyF.Go(dtRow);
                                break;
                            case 41:
                                //SendEmail_Web EMail_Web = new SendEmail_Web();
                                //iResult = EMail_Web.Go(dtRow);
                                //break;

                                SendEmail_41 EMail_41 = new SendEmail_41();
                                iResult = EMail_41.Go(dtRow);
                                break;
                            case 42:
                                SendSMS SMS = new SendSMS();
                                iResult = SMS.Go(dtRow);
                                break;
                            case 43:
                                SendEmail_43 EMail_43 = new SendEmail_43();
                                iResult = EMail_43.Go(dtRow);
                                break;
                            case 44:
                                SendEmail_44 EMail_44 = new SendEmail_44();
                                iResult = EMail_44.Go(dtRow);
                                break;
                            case 46:
                                SendEmail_46 EMail_46 = new SendEmail_46();
                                iResult = EMail_46.Go(dtRow);
                                break;
                            case 61:
                                iResult = 0;
                                iFirstID = Convert.ToInt32(dtRow["ID"]);
                                Process myProcess = new Process();
                                try
                                {
                                    myProcess.StartInfo.UseShellExecute = false;
                                    // You can start any process, HelloWorld is a do-nothing example.
                                    myProcess.StartInfo.FileName = "C:/Scripts/ISPServer/ISP_IPPDF.exe";
                                    myProcess.StartInfo.CreateNoWindow = true;
                                    myProcess.Start();
                                    System.Threading.Thread.Sleep(3000);
                                    // This code assumes the process you are starting will terminate itself.
                                    // Given that is is started without a window so you cannot terminate it
                                    // on the desktop, it must terminate itself or you can do it programmatically
                                    // from this application using the Kill method.
                                }
                                catch (Exception e)
                                {
                                    Global.AddLogsRecord(0, DateTime.Now, 1, "ISPServer.StartUp -> JobType_ID = 61. Error = " + e.Message + ".     ID = " + dtRow["ID"] + "   " + DateTime.Now);
                                }
                                finally
                                {
                                    conn.Open();
                                    cmd = new SqlCommand("SELECT Status FROM ServerJobs WHERE ID = " + iFirstID, conn);
                                    cmd.CommandType = CommandType.Text;
                                    drList = cmd.ExecuteReader();
                                    while (drList.Read())
                                    {
                                        if (Convert.ToInt32(drList["Status"]) == 1) iResult = 1;
                                    }
                                    conn.Close();
                                }
                                break;
                            case 81:                                                           // 81  - read FIX-data (ExecutionReports)
                                ReadFIX ReadFIX = new ReadFIX();
                                iResult = ReadFIX.Go(20, DateTime.Now.AddDays(-4));            // 20 - BNP Arbitrage   
                                iResult = ReadFIX.Go(19, DateTime.Now.AddDays(-4));            // 19 - Intesa
                                dtRow["Status"] = 1;
                                iResult = -999;
                                break;
                        }

                        if (iResult != -999)
                        {
                            if (iResult == 1)
                            {
                                dtRow["Attempt"] = Convert.ToInt32(dtRow["Attempt"]) + 1;
                                dtRow["Status"] = 1;
                                ServerJob_EditStatus(Convert.ToInt32(dtRow["ID"]), Convert.ToInt32(dtRow["Attempt"]), 1);
                            }
                            else
                            {
                                if (Convert.ToInt32(dtRow["Attempt"]) < 2) dtRow["Attempt"] = Convert.ToInt32(dtRow["Attempt"]) + 1;
                                else
                                {
                                    dtRow["Attempt"] = Convert.ToInt32(dtRow["Attempt"]) + 1;
                                    dtRow["Status"] = 2;
                                    ServerJob_EditStatus(Convert.ToInt32(dtRow["ID"]), Convert.ToInt32(dtRow["Attempt"]), 2);

                                    clsServerJobs ServerJobs = new clsServerJobs();
                                    ServerJobs.JobType_ID = 41;
                                    ServerJobs.Source_ID = 0;
                                    ServerJobs.Parameters = "{'email': 'g.katakalos@hellasfin.gr', 'subject' : 'ISP Server Message', 'cc' : 'v.kougioumtzidis@hellasfin.gr', 'body': 'Cannot execute ServerJobs.ID = " + dtRow["ID"] + "'}";
                                    ServerJobs.DateStart = DateTime.Now;
                                    ServerJobs.DateFinish = System.Convert.ToDateTime("1900/01/01");
                                    ServerJobs.PubKey = "";
                                    ServerJobs.PrvKey = "";
                                    ServerJobs.Attempt = 0;
                                    ServerJobs.Status = 0;
                                    ServerJobs.InsertRecord();
                                }
                            }
                        }
                    }
                    //Console.WriteLine("Finish Loop. i = " + i);
                    if (i == 0) break;
                }
            }
            finally { }
        }        
        private void ServerJob_EditStatus(int iRecord_ID, int iAttempt, int iStatus)
        {
            conn.Open();
            cmd = new SqlCommand("UPDATE  ServerJobs SET DateFinish = '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "', Attempt = " + iAttempt + ", Status = " + iStatus + " WHERE ID = " + iRecord_ID, conn);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            conn.Close();

            /*
            clsServerJobs ServerJobs = new clsServerJobs();
            ServerJobs.Record_ID = iRecord_ID;
            ServerJobs.GetRecord();
            ServerJobs.DateFinish = DateTime.Now;
            ServerJobs.Attempt = ServerJobs.Attempt + 1;
            ServerJobs.Status = iStatus;
            ServerJobs.EditRecord();
            */
        }
        public int MaxID { get { return iMaxID; } set { iMaxID = value; } }
    }
}
