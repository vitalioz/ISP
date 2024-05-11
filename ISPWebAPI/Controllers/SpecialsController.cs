using Microsoft.AspNetCore.Mvc;
using ISPWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace ISPWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpecialsController : Controller
    {
        private readonly IConfiguration m_config;
        string sConnectionString = "";

        SpecialsDAL objSpecials = new SpecialsDAL();
        Specials oSpecials = new Specials();

        public SpecialsController(IConfiguration config)
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
        public IEnumerable<Specials> GetSpecialsList()
        {
            List<Specials> lstSpecials = new List<Specials>();

            lstSpecials = objSpecials.GetSpecialsList(sConnectionString).ToList();

            return lstSpecials.ToArray();
        }
    }
}
