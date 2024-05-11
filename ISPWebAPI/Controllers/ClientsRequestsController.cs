using System;
using Microsoft.AspNetCore.Mvc;
using ISPWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ISPWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsRequestsController : Controller
    {
        private readonly IConfiguration m_config;
        string sConnectionString = "";

        ClientsRequestsDAL oClientsRequestsDAL = new ClientsRequestsDAL();
        ClientsRequests oClientsRequests = new ClientsRequests();

        ClientsDAL oClientsDAL = new ClientsDAL();
        ClientsAPI oClients = new ClientsAPI();
        public ClientsRequestsController(IConfiguration config)
        {
            m_config = config;
            sConnectionString = m_config.GetConnectionString("DBConnectionString");
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("[action]")]
        public ClientsRequests GetClientsRequest_Data(int id) {

            ClientsRequests oClientsRequests = new ClientsRequests();
            oClientsRequests = oClientsRequestsDAL.GetRecord_Data(sConnectionString, id);

            return oClientsRequests;
        }
        [HttpGet]
        [Route("[action]")]
        public List<ClientsRequests> GetClientsRequest_ClientID(int client_id, string status)
        {
            List<ClientsRequests> lstClientsRequests = new List<ClientsRequests>();
            lstClientsRequests = oClientsRequestsDAL.GetRecord_ClientID(sConnectionString, client_id, status+"");

            return lstClientsRequests;
        }
        [HttpPost]
        [Route("[action]")]
        public int AddClientsRequests([FromHeader] ClientsRequests oClientsRequests)
        {
            ClientsAPI oClients = new ClientsAPI();
            oClients = oClientsDAL.GetClient_Data(sConnectionString, "{'ID': '" + oClientsRequests.Client_ID + "'}");

            switch (oClientsRequests.Tipos)
            {
                case 1:
                    oClientsRequests.Description = "{'old_number' : '" + oClients.ADT + "'}~{'old_police' : '" + oClients.Police + "'}~{'old_expiredate' : '" + oClients.ExpireDate +
                                                   "'}~{'new_number' : ''}~{'new_police' : ''}~{'new_expiredate' : ''}~" + oClientsRequests.Description;
                    break;
                case 2:
                    oClientsRequests.Description = "{'old_number' : '" + oClients.Mobile + "'}~{'source_email' : ''}~" + oClientsRequests.Description;
                    break;
                case 3:
                    oClientsRequests.Description = "{'old_number' : '" + oClients.Tel + "'}~{'source_email' : ''}~" + oClientsRequests.Description;
                    break;
                case 4:
                    oClientsRequests.Description = oClientsRequests.Description +"~{'source_email' : ''}~";
                    break;
                case 5:
                    oClientsRequests.Description = "{'old_address' : '" + oClients.Address + "'}~{'old_city' : '" + oClients.City + "'}~{'old_zip' : '" + oClients.Zip +
                                                   "'}~{'old_country' : '" + oClients.Country_Title_En + "'}~{'new_address' : ''}~{'new_city' : ''}~{'new_zip' : ''}~" +
                                                   "{'new_country_id' : '0'}~{'source_email' : ''}~" + oClientsRequests.Description;
                    break;
                case 6:
                    oClientsRequests.Description = "{'old_afm' : '" + oClients.AFM + "'}~{'new_afm' : ''}~" + oClientsRequests.Description;
                    break;
                case 7:    // ekkatharistiko
                    //- nothing
                    break;
                case 8:
                    oClientsRequests.Description = "{ 'old_spec' : '" + oClients.SpecTitle + "'}~{'source_email' : ''}~" + oClientsRequests.Description;
                    break;
                case 9:
                    oClientsRequests.Description = "{ 'old_country' : '" + oClients.CountryTaxes_Title_En + "'}~{'source_email' : ''}~" + oClientsRequests.Description;
                    break;
                case 10:    // Αίτημα αλλαγής ειδικής κατηγορίας προσώπου
                    //- nothing
                    break;
                case 11:   //  τραπεζικος λογαριασμος  
                    if (oClientsRequests.Aktion == 0)   // 0 - Αίτημα προσθήκης νέου τραπεζικού λογαριασμού
                        oClientsRequests.Description = "{ 'acc_number' : ''}~{ 'currency' : ''}~{ 'type' : '0'}~{'owners' : ''}~{'source_email' : ''}~" + oClientsRequests.Description;
                    if (oClientsRequests.Aktion == 2)   // 2 - Αίτημα διαγραφής (ακύρωσεις) τραπεζικού λογαριασμού
                        oClientsRequests.Description = "{'source_email' : ''}~" + oClientsRequests.Description;
                    break;
                case 12:    // Αίτημα διαγραφής τραπεζικού λογαριασμού
                    oClientsRequests.Description = oClientsRequests.Description;
                    break;
                case 13:    // Αίτημα αλλαγής  W8 ΒΕΝ
                    break;
                case 14:
                    oClientsRequests.Description = "{'old_number' : '" + oClients.Passport + "'}~{'old_police' : '" + oClients.Passport_Police + "'}~" +
                                                   "{'old_expiredate' : '" + oClients.Passport_ExpireDate + "'}~{'new_number' : ''}~{'new_police' : ''}~" +
                                                   "{'new_expiredate' : ''}~" + oClientsRequests.Description;
                    break;
                case 15:
                    oClientsRequests.Description = "{'old_Merida' : '" + oClients.Merida + "'}~{'new_Merida' : ''}~" + oClientsRequests.Description;
                    break;
                case 16:
                    oClientsRequests.Description = "{'old_LogAxion' : '" + oClients.LogAxion + "'}~{'new_LogAxion' : ''}~" + oClientsRequests.Description;
                    break;
                case 17:
                    oClientsRequests.Description = "{'old_AMKA' : '" + oClients.Merida + "'}~{'new_AMKA' : ''}~" + oClientsRequests.Description;
                    break;
            }

            oClientsRequests.DateIns = DateTime.Now;
            oClientsRequests.DateWarning = Convert.ToDateTime("1900/01/01");
            oClientsRequests.DateClose = Convert.ToDateTime("1900/01/01");
            oClientsRequests.ID = oClientsRequestsDAL.AddRecord(sConnectionString, oClientsRequests);
            return oClientsRequests.ID;
        }
        [HttpPost]
        [Route("[action]")]
        public int UpdateClientsRequests([FromHeader] ClientsRequests oClientsRequests)
        {
            string sDescription = "";
            ClientsRequests locClientsRequests;

            ClientsAPI oClients = new ClientsAPI();
            oClients = oClientsDAL.GetClient_Data(sConnectionString, "{'ID': '" + oClientsRequests.Client_ID + "'}");

            locClientsRequests = oClientsRequestsDAL.GetRecord_Data(sConnectionString, oClientsRequests.ID);

            sDescription = oClientsRequests.Description + "";
            switch (locClientsRequests.Tipos)
            {
                case 1:
                   sDescription = "{'old_number' : '" + oClients.ADT + "'}~{'old_police' : '" + oClients.Police + "'}~{'old_expiredate' : '" + oClients.ExpireDate +
                                                   "'}~{'new_number' : ''}~{'new_police' : ''}~{'new_expiredate' : ''}~" + oClientsRequests.Description;
                    break;
                case 2:
                    sDescription = "{'old_number' : '" + oClients.Mobile + "'}~{'source_email' : ''}~" + oClientsRequests.Description;
                    break;
                case 3:
                    sDescription = "{'old_number' : '" + oClients.Tel + "'}~{'source_email' : ''}~" + oClientsRequests.Description;
                    break;
                case 4:
                    sDescription = oClientsRequests.Description + "~{'source_email' : ''}~";
                    break;
                case 5:
                    sDescription = "{'old_address' : '" + oClients.Address + "'}~{'old_city' : '" + oClients.City + "'}~{'old_zip' : '" + oClients.Zip +
                                                   "'}~{'old_country' : '" + oClients.Country_Title_En + "'}~{'new_address' : ''}~{'new_city' : ''}~{'new_zip' : ''}~" +
                                                   "{'new_country_id' : '0'}~{'source_email' : ''}~" + oClientsRequests.Description;
                    break;
                case 6:
                    sDescription = "{'old_afm' : '" + oClients.AFM + "'}~{'new_afm' : ''}~" + oClientsRequests.Description;
                    break;
                case 7:    // ekkatharistiko
                    //- nothing
                    break;
                case 8:
                    sDescription = "{ 'old_spec' : '" + oClients.SpecTitle + "'}~{'source_email' : ''}~" + oClientsRequests.Description;
                    break;
                case 9:
                    sDescription = "{ 'old_country' : '" + oClients.CountryTaxes_Title_En + "'}~{'source_email' : ''}~" + oClientsRequests.Description;
                    break;
                case 10:    // Αίτημα αλλαγής ειδικής κατηγορίας προσώπου
                    //- nothing
                    break;
                case 11:    // Αίτημα προσθήκης νέου τραπεζικού λογαριασμού
                    oClientsRequests.Description = "{ 'acc_number' : ''}~{ 'currency' : ''}~{ 'type' : '0'}~{'owners' : ''}~{'source_email' : ''}~" + oClientsRequests.Description;
                    break;
                case 12:    // Αίτημα διαγραφής τραπεζικού λογαριασμού
                    sDescription = "{'source_email' : ''}~" + oClientsRequests.Description;
                    break;
                case 13:    // Αίτημα αλλαγής  W8 ΒΕΝ
                    break;
                case 14:
                    sDescription = "{'old_number' : '" + oClients.Passport + "'}~{'old_police' : '" + oClients.Passport_Police + "'}~" +
                                                   "{'old_expiredate' : '" + oClients.Passport_ExpireDate + "'}~{'new_number' : ''}~{'new_police' : ''}~" +
                                                   "{'new_expiredate' : ''}~" + oClientsRequests.Description;
                    break;
                case 15:
                    sDescription = "{'old_Merida' : '" + oClients.Merida + "'}~{'new_Merida' : ''}~" + oClientsRequests.Description;
                    break;
                case 16:
                    sDescription = "{'old_LogAxion' : '" + oClients.LogAxion + "'}~{'new_LogAxion' : ''}~" + oClientsRequests.Description;
                    break;
                case 17:
                    sDescription = "{'old_AMKA' : '" + oClients.Merida + "'}~{'new_AMKA' : ''}~" + oClientsRequests.Description;
                    break;
            }
            locClientsRequests.Description = sDescription;

            locClientsRequests.Group_ID = oClientsRequests.Group_ID + "";
            locClientsRequests.DateIns = DateTime.Now;
            if (locClientsRequests.DateClose < Convert.ToDateTime("2000/01/01")) locClientsRequests.DateClose = Convert.ToDateTime("1900/01/01");
            if (oClientsRequests.Status != 0) locClientsRequests.Status = oClientsRequests.Status;
            if (oClientsRequests.VideoChatStatus != 0) locClientsRequests.VideoChatStatus = oClientsRequests.VideoChatStatus;
            locClientsRequests.VideoChatFile = "";

            oClientsRequestsDAL.UpdateRecord(sConnectionString, locClientsRequests);
            return oClientsRequests.ID;
        }
        [HttpPost]
        [Route("[action]")]
        public int UpdateStatus([FromForm] int id, [FromForm] int status)
        {
            oClientsRequestsDAL.UpdateStatus(sConnectionString, id, status);
            return status;
        }
    }
}
