using Microsoft.AspNetCore.Mvc;
using ISPWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace ISPWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrenciesController : Controller
    {
        private readonly IConfiguration m_config;
        string sConnectionString = "";

        CurrenciesDAL objCurrencies = new CurrenciesDAL();
        Currencies oCurrencies = new Currencies();

        public CurrenciesController(IConfiguration config)
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
        public IEnumerable<Currencies> GetCurrenciesList()
        {
            List<Currencies> lstCurrencies = new List<Currencies>();

            lstCurrencies = objCurrencies.GetCurrenciesList(sConnectionString).ToList();

            return lstCurrencies.ToArray();
        }     
    }
}
