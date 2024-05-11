using Microsoft.AspNetCore.Mvc;
using ISPWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace ISPWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountriesController : Controller
    {
        private readonly IConfiguration m_config;
        string sConnectionString = "";

        CountriesDAL objCountries = new CountriesDAL();
        Countries oCountries = new Countries();

        public CountriesController(IConfiguration config)
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
        public IEnumerable<Countries> GetCountriesList()
        {
            List<Countries> lstCountries = new List<Countries>();

            lstCountries = objCountries.GetCountriesList(sConnectionString).ToList();

            return lstCountries.ToArray();
        }
    }
}
