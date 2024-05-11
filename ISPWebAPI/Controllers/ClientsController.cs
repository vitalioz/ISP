using System;
using Microsoft.AspNetCore.Mvc;
using ISPWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Core;

namespace ISPWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : Controller
    {
        private readonly IConfiguration m_config;
        string sConnectionString = "";

        ClientsDAL objClients = new ClientsDAL();
        ClientsAPI oClients = new ClientsAPI();
        Clients_ClientsDAL objClients_Clients = new Clients_ClientsDAL();

        ClientsBankAccounts oClientsBankAccounts = new ClientsBankAccounts();
        ClientsBankAccountsDAL oClientsBankAccountsDAL = new ClientsBankAccountsDAL();
        public ClientsController(IConfiguration config)
        {
            m_config = config;
            sConnectionString = m_config.GetConnectionString("DBConnectionString");
            Global.connStr = sConnectionString;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("[action]/{sParameters}")]
        public int GetClient_ID(string sParameters)
        {
            ClientsAPI oClients = new ClientsAPI();
            oClients.ID = objClients.GetClient_ID(sConnectionString, sParameters);

            return oClients.ID;
        }
        [HttpGet]
        [Route("[action]/{sParameters}")]
        public ClientsAPI GetClient_Data(string sParameters)
        {
            ClientsAPI oClients = new ClientsAPI();
            oClients = objClients.GetClient_Data(sConnectionString, sParameters);

            return oClients;
        }
        [HttpGet]
        [Route("[action]/{sParameters}")]
        public IEnumerable<ClientsAPI> GetClient_List(string sParameters)
        {
            List<ClientsAPI> lstClients = new List<ClientsAPI>();
            lstClients = (List<ClientsAPI>)objClients.GetClient_List(sConnectionString, sParameters);
            return lstClients.ToArray();
        }

        [HttpGet]
        [Route("[action]/{sParameters}")]
        public List<ClientsBankAccounts> GetClientBankAccountsList_Data(string sParameters)
        {

            Logger Log = new Logger();
            Log.Author_ID = 0;
            Log.DateIns = DateTime.Now;
            Log.Rec_ID = 0;
            Log.Notes = sParameters;

            LoggerDAL LoggerDAL = new LoggerDAL();
            LoggerDAL.AddLogger(sConnectionString, Log);

            List<ClientsBankAccounts> lstClientsBankAccounts = new List<ClientsBankAccounts>();

            lstClientsBankAccounts = oClientsBankAccountsDAL.GetList_Data(sConnectionString, sParameters);

            return lstClientsBankAccounts;
        }
        [HttpPost]
        [Route("[action]")]
        public int AddClients_Clients([FromHeader] Interface oInterface)
        {
            oInterface = objClients_Clients.AddRecord(sConnectionString, oInterface);
            return oInterface.ID;
        }
        [HttpPost]
        [Route("[action]")]
        public int UpdateStatusClients_Clients([FromHeader] Interface oInterface)
        {
            oInterface.ID = objClients_Clients.UpdateStatus(sConnectionString, oInterface.ID, oInterface.Status);
            return oInterface.ID;
        }
        [HttpGet]
        [Route("[action]/{sParameters}")]
        public IEnumerable<Clients_Clients>GetClients_Clients(string sParameters)
        {            
            int i = sParameters.IndexOf("=");
            int iClient_ID = Convert.ToInt32(sParameters.Substring(i+1));
            List<Clients_Clients> lstClients_Clients = new List<Clients_Clients>();
            lstClients_Clients = (List<Clients_Clients>)objClients_Clients.GetClients_Clients(sConnectionString, iClient_ID);
            return lstClients_Clients.ToArray();
        }
        [HttpPost]
        [Route("[action]")]
        public int DeleteClients_Clients([FromForm] int id)
        {
            objClients_Clients.DeleteRecord(sConnectionString, id);
            return id;
        }
    }
}
