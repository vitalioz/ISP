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
    public class ClientsContractsController : Controller
    {
        private readonly IConfiguration m_config;
        string sConnectionString = "";

        ClientsContracts oClientsContracts = new ClientsContracts();
        ClientsContractsDAL oClientsContractsDAL = new ClientsContractsDAL();
        public ClientsContractsController(IConfiguration config)
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
        public List<ClientsContracts> GetClientsContractsList(string sParameters)
        {
            int iClient_ID = Convert.ToInt32(sParameters);
            List<ClientsContracts> lstClientsContracts = new List<ClientsContracts>();

            lstClientsContracts = oClientsContractsDAL.GetClientsContractsList(sConnectionString, iClient_ID);

            return lstClientsContracts;
        }
        [HttpPost]
        [Route("[action]")]
        public int AddRecord([FromHeader] ClientsContracts oClientsContracts)
        {
            //oClientsContracts.AccNumber = oClientsContracts.AccNumber + "";
            //if (!Global.IsNumeric(oClientsContracts.AccType)) oClientsContracts.AccType = 0;
            //oClientsContracts.AccOwners = oClientsContracts.AccOwners + "";
            //oClientsContracts.Status = 1;
            //oClientsContracts.ID = oClientsContractsDAL.AddRecord(sConnectionString, oClientsContracts);
            return oClientsContracts.ID;
        }
        [HttpPost]
        [Route("[action]")]
        public int EditRecord([FromHeader] ClientsContracts oClientsContracts)
        {
            //oClientsContracts.ID = oClientsContractsDAL.EditRecord(sConnectionString, oClientsContracts);
            return oClientsContracts.ID;
        }
    }
}
