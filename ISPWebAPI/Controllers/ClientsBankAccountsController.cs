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
    public class ClientsBankAccountsController : Controller
    {
        private readonly IConfiguration m_config;
        string sConnectionString = "";

        ClientsBankAccounts oClientsBankAccounts = new ClientsBankAccounts();
        ClientsBankAccountsDAL oClientsBankAccountsDAL = new ClientsBankAccountsDAL();
        public ClientsBankAccountsController(IConfiguration config)
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
        public List<ClientsBankAccounts> GetClientBankAccountsList_Data(string sParameters)
        {
            List<ClientsBankAccounts> lstClientsBankAccounts = new List<ClientsBankAccounts>();

            lstClientsBankAccounts = oClientsBankAccountsDAL.GetList_Data(sConnectionString, sParameters);

            return lstClientsBankAccounts;
        }
        [HttpPost]
        [Route("[action]")]
        public int AddRecord([FromHeader] ClientsBankAccounts oClientsBankAccounts)
        {
            oClientsBankAccounts.AccNumber = oClientsBankAccounts.AccNumber + "";
            if (!Global.IsNumeric(oClientsBankAccounts.AccType)) oClientsBankAccounts.AccType = 0;
            oClientsBankAccounts.AccOwners = oClientsBankAccounts.AccOwners + "";
            oClientsBankAccounts.Status = 1;
            oClientsBankAccounts.ID = oClientsBankAccountsDAL.AddRecord(sConnectionString, oClientsBankAccounts);
            return oClientsBankAccounts.ID;
        }
        [HttpPost]
        [Route("[action]")]
        public int EditRecord([FromHeader] ClientsBankAccounts oClientsBankAccounts)
        {
            oClientsBankAccounts.ID = oClientsBankAccountsDAL.EditRecord(sConnectionString, oClientsBankAccounts);
            return oClientsBankAccounts.ID;
        }
    }
}
