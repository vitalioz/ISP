using Microsoft.AspNetCore.Mvc;
using ISPWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace ISPWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BanksController : Controller
    {
        private readonly IConfiguration m_config;
        string sConnectionString = "";

        BanksDAL objBanks = new BanksDAL();
        Banks oBanks = new Banks();

        public BanksController(IConfiguration config)
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
        public IEnumerable<Banks> GetBanksList()
        {
            List<Banks> lstBanks = new List<Banks>();

            lstBanks = objBanks.GetBanksList(sConnectionString).ToList();

            return lstBanks.ToArray();
        }
    }
}
