using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Dynamic;
using ISPDBO.Models;

namespace ISPDBO.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration m_config;
        string sConnectionString = "";
        string sDMSFolder = "";
        string sTransferFolder = "";
        string sFTP_Host = "";
        string sFTP_Username = "";
        string sFTP_Password = "";

        WebUsersDAL oWebUsersDAL = new WebUsersDAL();
        public HomeController(IConfiguration config)
        {
            m_config = config;
            sConnectionString = m_config.GetConnectionString("DBConnectionString");

            sDMSFolder = m_config.GetSection("DMSFolder:FolderName").Value;
            sTransferFolder = m_config.GetSection("DMSFolder:TransferFolder").Value;
            sFTP_Host = m_config.GetSection("FTPServer:FTP_Host").Value;
            sFTP_Username = m_config.GetSection("FTPServer:FTP_Username").Value;
            sFTP_Password = m_config.GetSection("FTPServer:FTP_Password").Value;

            if (Global.ConnectionString != sConnectionString)
                Global.StartInit(sConnectionString, sDMSFolder, sTransferFolder, sFTP_Host, sFTP_Username, sFTP_Password);
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("[action]")]
        public IActionResult Registry()
        {
            return View();
        }
        //--- entrance in Login form --------------------------------------------------------------------------------------------------
        [Route("[action]")]
        public IActionResult Login()
        {
            WebUsers webuser = new WebUsers();
            webuser.ID = 0;
            return View(webuser);
        }
        //--- exit from Login form : register new user or define existing user data ----------------------------------------------------
        [HttpPost]
        [Route("[action]")]
        public IActionResult Login(string Email, string Mobile, string Password, string Client_ID)
        {
            int iClient_ID = 0;
            int iWebUser_ID = 0;
           
            WebUsers webuser = new WebUsers();
            webuser.ID = 0;

            WebUsersDAL oWebUsers = new WebUsersDAL();
            ClientsDAL oClientsDAL = new ClientsDAL();

            // Client_ID is null in Login mode, Client_ID isn't null in Registry mode
            if (!(Client_ID is null)) {                                                             // Registry mode           

                if (Client_ID == "0")
                {
                    Clients oClient = new Clients();
                    oClient.Surname = "";
                    iClient_ID = oClientsDAL.AddRecord(oClient);
                }
                else iClient_ID = Convert.ToInt32(Client_ID);

                if (iClient_ID != 0)
                {
                    //--- add new Web User into  WebUsers table ------------------------------
                    webuser.Email = Email;
                    webuser.Mobile = Mobile;
                    webuser.Password = Password;
                    webuser.Client_ID = iClient_ID;
                    iWebUser_ID = oWebUsers.AddWebUsers(webuser);

                    //--- create user folder with temporary name iClient_ID
                    ServerJobs oServerJobs = new ServerJobs();
                    oServerJobs.JobType_ID = 11;
                    oServerJobs.Source_ID = 0;
                    oServerJobs.Parameters = "{'folder_name': 'C:/DMS/Customers/" + iClient_ID + "'}";
                    oServerJobs.DateStart = DateTime.Now;
                    oServerJobs.DateFinish = Convert.ToDateTime("1900/01/01");
                    oServerJobs.PubKey = "";
                    oServerJobs.PrvKey = "";
                    oServerJobs.Attempt = 0;
                    oServerJobs.Status = 0;
                    ServerJobsDAL oServerJobsDAL = new ServerJobsDAL();
                    oServerJobsDAL.AddRecord(oServerJobs);

                    Clients_MetaDataDAL oClients_MetaDataDAL = new Clients_MetaDataDAL();
                    oClients_MetaDataDAL.AddRecord(iClient_ID);
                }
            }

            webuser = new WebUsers();                                                               // Login mode   
            webuser = oWebUsers.GetAllWebUsers(Email, Password);
            if (webuser.ID != 0)
            {
                Global.WebUser_ID = webuser.ID;
                Global.Client_ID = webuser.Client_ID;
                return RedirectToAction("Dashboard");
            }
            else
            {
                webuser.ID = -1;
                return View(webuser);
            };
        }
        [HttpPost]
        public int CheckClient(string AFM, string DoB)
        {
            UsersDAL oUsers = new UsersDAL();
            Users user = oUsers.GetClient(AFM, DoB);
            return user.ID;
        }
        [HttpPost]
        public int CheckIfEmailExists(string email, string code)
        {
            int iID = 0;
            WebUsersDAL oWebUsers = new WebUsersDAL();
            iID = oWebUsers.GetWebUser_ID("{'email' : '" + email + "'}");

            if (iID == 0) {

                //--- send email with verification code -------------------------------------------------------
                ServerJobs oServerJobs = new ServerJobs();
                oServerJobs.JobType_ID = 41;                                                // 41 - send e-mail
                oServerJobs.Source_ID = 0;
                oServerJobs.Parameters = "{'email' : '" + email + "', 'subject': 'HF DBO', 'body': 'Your e-mail Verification code is " + code + "'}";
                oServerJobs.DateStart = DateTime.Now;
                oServerJobs.DateFinish = Convert.ToDateTime("1900/01/01");
                oServerJobs.PubKey = "";
                oServerJobs.PrvKey = "";
                oServerJobs.Attempt = 0;
                oServerJobs.Status = 0;
                ServerJobsDAL oServerJobsDAL = new ServerJobsDAL();
                oServerJobsDAL.AddRecord(oServerJobs);
            }
            return iID;
        }
        [HttpPost]
        public int CheckIfMobileExists(string mobile, string code)
        {
            int iID = 0;
            WebUsersDAL oWebUsers = new WebUsersDAL();
            iID = oWebUsers.GetWebUser_ID("{'mobile' : '" + mobile + "'}");

            if (iID == 0)
            {
                //--- send email with verification code -------------------------------------------------------
                ServerJobs oServerJobs = new ServerJobs();
                oServerJobs.JobType_ID = 42;                                                // 42 - send SMS
                oServerJobs.Source_ID = 0;
                oServerJobs.Parameters = "{'mobile': '" + mobile + "', 'message': 'Your mobile Verification code is " + code + "'}";
                oServerJobs.DateStart = DateTime.Now;
                oServerJobs.DateFinish = Convert.ToDateTime("1900/01/01");
                oServerJobs.PubKey = "";
                oServerJobs.PrvKey = "";
                oServerJobs.Attempt = 0;
                oServerJobs.Status = 0;
                ServerJobsDAL oServerJobsDAL = new ServerJobsDAL();
                oServerJobsDAL.AddRecord(oServerJobs);
            }
            return iID;
        }
        //--- main entrance -------------------------------------------------------------------------------------------------
        [HttpGet]
        [Route("[action]")]
        public IActionResult Dashboard()
        {
            dynamic mymodel = new ExpandoObject();
            WebUsers webuser = oWebUsersDAL.GetWebUser_Data("{'ID' : '" + Global.WebUser_ID.ToString() + "'}");

            Clients_MetaDataDAL oClients_MetaDataDAL = new Clients_MetaDataDAL();
            ViewBag.Clients_MetaData = oClients_MetaDataDAL.GetRecord_Client_ID(Global.Client_ID);

            mymodel.Clients_MetaData = ViewBag.Clients_MetaData;
            return View(webuser);
        }

        //--- call MyOffice page - prepare all data (user, Clients_MetaData, Doc_Files, DocTypes, PersonalData ) ----------
        [HttpGet]
        [Route("[action]")]
        public IActionResult MyOffice()
        {
            int i = 0;
            dynamic mymodel = new ExpandoObject();

            WebUsers webuser = oWebUsersDAL.GetWebUser_Data("{'ID' : '" + Global.WebUser_ID.ToString() + "'}");

            Clients_MetaDataDAL oClients_MetaDataDAL = new Clients_MetaDataDAL();
            ViewBag.Clients_MetaData = oClients_MetaDataDAL.GetRecord_Client_ID(Global.Client_ID);

            ClientsDoc_FilesDAL oClientsDoc_FilesDAL = new ClientsDoc_FilesDAL();
            ViewBag.Doc_Files = oClientsDoc_FilesDAL.GetList(Global.Client_ID, -1);          // -1 - group_id - means for ALL groups

            DocTypesDAL oDocTypesDAL = new DocTypesDAL();
            ViewBag.DocTypes = oDocTypesDAL.GetList(-1);

            ClientsRequests clientRequests = new ClientsRequests();
            ClientsRequestsDAL ClientRequestsDAL = new ClientsRequestsDAL();
            clientRequests = ClientRequestsDAL.GetList(Global.Client_ID, 1);
            ViewBag.PersonalData[2].Value = webuser.ADT;
            ViewBag.PersonalData[3].Value = webuser.AFM;
            ViewBag.PersonalData[4].Value = webuser.AMKA;
            ViewBag.PersonalData[5].Value = webuser.Tel;
            ViewBag.PersonalData[6].Value = webuser.Mobile;
            ViewBag.PersonalData[7].Value = webuser.Email;
            ViewBag.PersonalData[8].Value = webuser.Address;
            ViewBag.PersonalData[9].Value = webuser.Category_Title;
            ViewBag.PersonalData[10].Value = webuser.Spec_Title;
            ViewBag.PersonalData[11].Value = webuser.Family_Title;

            foreach (ClientsDoc_Files doc_file in ViewBag.Doc_Files)
            {
                i = doc_file.PD_Group_ID;
                ViewBag.PersonalData[i].DocCount = ViewBag.PersonalData[i].DocCount + 1;
            }


            mymodel.Clients_MetaData = ViewBag.Clients_MetaData;
            mymodel.PersonalData = ViewBag.PersonalData;
            mymodel.Doc_Files = ViewBag.Doc_Files;
            mymodel.DocTypes = ViewBag.DocTypes;

            return View("MyOffice", webuser);
        }
        //--- TEMPORARY call PersonalData page - prepare all data (user, Clients_MetaData, Doc_Files, DocTypes, PersonalData ) ----------
        [HttpGet]
        [Route("[action]")]
        public IActionResult TEST()
        {
            int i = 0;
            dynamic mymodel = new ExpandoObject();

            Global.WebUser_ID = 1;
            Global.Client_ID = 12963;



            WebUsers webuser = oWebUsersDAL.GetWebUser_Data("{'ID' : '" + Global.WebUser_ID.ToString() + "'}");

            Clients_MetaDataDAL oClients_MetaDataDAL = new Clients_MetaDataDAL();
            ViewBag.Clients_MetaData = oClients_MetaDataDAL.GetRecord_Client_ID(Global.Client_ID);

            ClientsDoc_FilesDAL oClientsDoc_FilesDAL = new ClientsDoc_FilesDAL();
            ViewBag.Doc_Files = oClientsDoc_FilesDAL.GetList(Global.Client_ID, -1);          // -1 - group_id - means for ALL groups

            DocTypesDAL oDocTypesDAL = new DocTypesDAL();
            ViewBag.DocTypes = oDocTypesDAL.GetList(-1);

            PersonalData_MetaDataDAL oPersonalData_MetaDataDAL = new PersonalData_MetaDataDAL();
            ViewBag.PersonalData = oPersonalData_MetaDataDAL.GetList();
            ViewBag.PersonalData[0].Value = webuser.ADT;
            ViewBag.PersonalData[1].Value = webuser.AFM;
            ViewBag.PersonalData[2].Value = webuser.AMKA;
            ViewBag.PersonalData[3].Value = webuser.Tel;
            ViewBag.PersonalData[4].Value = webuser.Mobile;
            ViewBag.PersonalData[5].Value = webuser.Email;
            ViewBag.PersonalData[6].Value = webuser.Address;
            ViewBag.PersonalData[7].Value = webuser.LogAxion;
            ViewBag.PersonalData[8].Value = webuser.Merida;
            ViewBag.PersonalData[9].Value = webuser.Category_Title;
            ViewBag.PersonalData[10].Value = webuser.Spec_Title;
            ViewBag.PersonalData[11].Value = webuser.Family_Title;

            foreach (ClientsDoc_Files doc_file in ViewBag.Doc_Files)
            {
                i = doc_file.PD_Group_ID;
                ViewBag.PersonalData[i].DocCount = ViewBag.PersonalData[i].DocCount + 1;
            }


            mymodel.Clients_MetaData = ViewBag.Clients_MetaData;
            mymodel.PersonalData = ViewBag.PersonalData;
            mymodel.Doc_Files = ViewBag.Doc_Files;
            mymodel.DocTypes = ViewBag.DocTypes;

            return View("BanDevice", webuser);
        }
        //--- download uploaded file into DMS folder -----------------------------------------------------------------------
        [HttpPost]
        public void DownloadUploadedFile2DMS(string file_name)
        {
            //--- 18 - Download file from remote server http://dms.hellasfin.gr to DMS. Parameters:   file_name: <source_file_name>; target_folder: <target_folder>
            //                                                                                        - source_file_name are on remote server in folder /1_Investment_Proposals
            //                                                                                        - target_folder is client's folder into DMS folder
            ServerJobs oServerJobs = new ServerJobs();
            oServerJobs.JobType_ID = 18;
            oServerJobs.Source_ID = 0;
            oServerJobs.Parameters = "{'file_name': '" + file_name + "', 'target_folder': 'C:/DMS/Customers/" + Global.Client_ID + "'}";
            oServerJobs.DateStart = DateTime.Now;
            oServerJobs.DateFinish = Convert.ToDateTime("1900/01/01");
            oServerJobs.PubKey = "";
            oServerJobs.PrvKey = "";
            oServerJobs.Attempt = 0;
            oServerJobs.Status = 0;
            ServerJobsDAL oServerJobsDAL = new ServerJobsDAL();
            oServerJobsDAL.AddRecord(oServerJobs);
        }
        //--- define DocTypes list for group_id ----------------------------------------------------------------------------
        [HttpPost]
        public List<DocTypes> DefineDocTypesList(int group_id)
        {
            DocTypesDAL oDocTypesDAL = new DocTypesDAL();
            ViewBag.DocTypes = oDocTypesDAL.GetList(group_id);

            return ViewBag.DocTypes;
        }

        //--- add document into DMS tables ---------------------------------------------------------------------------------
        [HttpPost]
        public List<ClientsDoc_Files> AddFile2DMS(int doc_type, int group_id, int aktion, string file_name, int oldfile_id)
        {
            int file_id;
            //--- define (refresh) ViewBag.Doc_Web - client's documents states -------------------
            ClientsDoc_FilesDAL oClientsDoc_FilesDAL = new ClientsDoc_FilesDAL();
            file_id = oClientsDoc_FilesDAL.AddRecord(file_name, Global.Client_ID, doc_type, group_id);

            //--- add action into Clients_MetaData.PD_Requests list --------------------------------
            string sAction = "'action' : " + aktion + ", 'file_id' : " + file_id;
            if (oldfile_id != 0) sAction = sAction + ", 'oldfile_id' : " + oldfile_id;

            Clients_MetaData oClients_MetaData = new Clients_MetaData();
            Clients_MetaDataDAL oClients_MetaDataDAL = new Clients_MetaDataDAL();
            oClients_MetaData = oClients_MetaDataDAL.GetRecord_Client_ID(Global.Client_ID);
            oClients_MetaData.PD_Request = oClients_MetaData.PD_Request + "{" + sAction + "}~";
            oClients_MetaDataDAL.EditRecord(oClients_MetaData);

            ViewBag.Doc_Files =  DefineDocumentsList(group_id);

            return ViewBag.Doc_Files;
        }
        //--- define Documents list for group_id ----------------------------------------------------------------------------
        [HttpPost]
        public List<ClientsDoc_Files> DefineDocumentsList(int group_id)
        {
            ClientsDoc_FilesDAL oClientsDoc_FilesDAL = new ClientsDoc_FilesDAL();
            ViewBag.Doc_Files = oClientsDoc_FilesDAL.GetList(Global.Client_ID, group_id);

            return ViewBag.Doc_Files;
        }

        //--- send Clients Request -------------------------------------------------------------------------------------------
        public IActionResult SendClientsRequests()
        {
            //--- define Clients_MetaData.PD_Request value --------------------------------------
            Clients_MetaData oClients_MetaData = new Clients_MetaData();
            Clients_MetaDataDAL oClients_MetaDataDAL = new Clients_MetaDataDAL();
            oClients_MetaData = oClients_MetaDataDAL.GetRecord_Client_ID(Global.Client_ID);

            //--- add Clients Request where Description = oClients_MetaData.PD_Request ----------
            ClientsRequests oRequests = new ClientsRequests();
            oRequests.Client_ID = Global.Client_ID;
            oRequests.Tipos = 2;
            oRequests.Source_ID = 0;
            oRequests.Description = oClients_MetaData.PD_Request;
            oRequests.DateIns = DateTime.Now;
            oRequests.DateClose = Convert.ToDateTime("1900/01/01");
            oRequests.Status = 1;                                                                        // 1 - new request - request was sent

            ClientsRequestsDAL oClientsRequestsDAL = new ClientsRequestsDAL();
            oClientsRequestsDAL.AddRecord(oRequests);

            //--- after Sent Request value of Clients_MetaData.PD_Request must be empty -----------
            oClients_MetaData.PD_Status = 1;
            oClients_MetaData.PD_Request = "";
            oClients_MetaDataDAL.EditRecord(oClients_MetaData);

            //--- change status of all ClientsDoc_Files with .Status= 0 or .Status= 1 to .Status= 2
            ClientsDoc_FilesDAL oClientsDoc_FilesDAL = new ClientsDoc_FilesDAL();
            oClientsDoc_FilesDAL.EditSentStatus(Global.Client_ID); 

            return RedirectToAction("PersonalData");
        }





        [HttpPost]
        public System.Dynamic.ExpandoObject AddFilesDMS(int doc_index, int doc_subindex, string file_name)
        {
            int id = (int)HttpContext.Session.GetInt32("WebUser_ID");
            int client_id = (int)HttpContext.Session.GetInt32("Client_ID");
            dynamic mymodel = new ExpandoObject();

            //--- define (refresh) ViewBag.Doc_Web - client's documents states -------------------
            ClientsDoc_FilesDAL oClientsDoc_FilesDAL = new ClientsDoc_FilesDAL();
            //ClientsDoc_WebDAL oClientDoc_WebDAL = new ClientsDoc_WebDAL();
            //ViewBag.Doc_Web = oClientDoc_WebDAL.GetRecord_Client_ID(sConnectionString, client_id);

            if (doc_index != 0) {
                string sSQL = "";
                int iNew_File_ID = 0;
                int iStatus = 0;

                //--- 15 - Copy File from DMSTransferFolder to DMS. Parameters file_name: <source_file_name>; target_folder: <target_folder>
                ServerJobs oServerJobs = new ServerJobs();
                oServerJobs.JobType_ID = 15;
                oServerJobs.Source_ID = 0;
                oServerJobs.Parameters = "{'file_name': '" + file_name + "', 'target_folder': C:/DMS/Customers/" + client_id + "'}";
                oServerJobs.DateStart = DateTime.Now;
                oServerJobs.DateFinish = Convert.ToDateTime("1900/01/01");
                oServerJobs.PubKey = "";
                oServerJobs.PrvKey = "";
                oServerJobs.Attempt = 0;
                oServerJobs.Status = 0;
                ServerJobsDAL oServerJobsDAL = new ServerJobsDAL();
                oServerJobsDAL.AddRecord(oServerJobs);

                //--- add file with file_name into DMS_Files table and ClientsDocFiles table                
                iNew_File_ID = oClientsDoc_FilesDAL.AddRecord(file_name, client_id, 0, doc_index);
              

                //oClientDoc_WebDAL = new ClientsDoc_WebDAL();
                //ViewBag.Doc_Web = oClientDoc_WebDAL.GetRecord_Client_ID(sConnectionString, client_id);
            }

            oClientsDoc_FilesDAL = new ClientsDoc_FilesDAL();
            ViewBag.Doc_Files = oClientsDoc_FilesDAL.GetList(client_id, -1);

            DocTypesDAL oDocTypesDAL = new DocTypesDAL();
            ViewBag.DocTypes = oDocTypesDAL.GetList(-1);

            mymodel.Doc_Web = ViewBag.Doc_Web;
            mymodel.Doc_Files = ViewBag.Doc_Files;
            mymodel.DocTypes = ViewBag.DocTypes;

            return mymodel; // was return ViewBag.Doc_Web;
        }
    

        public void DeleteClientDocFile(int file_id)
        {
            DocTypesDAL oDocTypesDAL = new DocTypesDAL();
        }
        public IActionResult Portfolios()
        {
            return View();
        }
        [Route("[action]")]
        public IActionResult NewPortfolio()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveFiles(IList<IFormFile> files)
        {
            int client_id = (int)HttpContext.Session.GetInt32("Client_ID");
            string sFileName = "";
            int group_id = 0;
            foreach (IFormFile postedFile in files)
                sFileName = Path.GetFileName(postedFile.FileName);                

            ClientsDoc_FilesDAL ClientDoc_FilesDAL = new ClientsDoc_FilesDAL();
            ClientDoc_FilesDAL.AddRecord(sFileName, client_id,0,  group_id);
            return this.View();
        }
        [HttpPost]
        public string DefineDocumentName(int iFile_ID)
        {
            string sDMSFolder = HttpContext.Session.GetString("DMSFolder");

            ClientsDoc_Files ClientDoc_Files = new ClientsDoc_Files();

            ClientsDoc_FilesDAL ClientDoc_FilesDAL = new ClientsDoc_FilesDAL();
            ClientDoc_Files = ClientDoc_FilesDAL.GetRecord(iFile_ID);

            return sDMSFolder + "/Customers/" + ClientDoc_Files.FilePath;
        }
        [HttpPost]
        public int CheckWebUsersDevices(string dev_os, string dev_video)
        {
            WebUsersDevices oWebUsersDevices = new WebUsersDevices();
            oWebUsersDevices.OS = dev_os;
            oWebUsersDevices.Video = dev_video;

            WebUsersDevicesDAL oWebUsersDevicesDAL = new WebUsersDevicesDAL();
            oWebUsersDevices = oWebUsersDevicesDAL.GetRecord(oWebUsersDevices);
            if (oWebUsersDevices.ID == 0) oWebUsersDevicesDAL.AddWebUsersDevices(oWebUsersDevices);
            Global.WebUserDevice_ID = oWebUsersDevices.ID;

            return oWebUsersDevices.Status;
        }
        [HttpPost]
        public IActionResult Ban()
        {
            return RedirectToAction("Index");
        }
        public IActionResult Test_Hardware()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DeleteAccount(int id)
        {
            WebUsersDAL oWebUsersDAL = new WebUsersDAL();
            oWebUsersDAL.DeleteWebUser(id);
            return View();
        }
    }
}
