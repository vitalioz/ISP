using Microsoft.AspNetCore.Mvc;
using ISPWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace ISPWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OptionsController : Controller
    {
        private readonly IConfiguration m_config;
        string sConnectionString = "";

        OptionsDAL objOptions = new OptionsDAL();
        Options oOptions = new Options();

        public OptionsController(IConfiguration config)
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
        public Options GetOptionsData()
        {
            Options oOptions = new Options();

            oOptions = objOptions.GetRecord(sConnectionString);

            return oOptions;
        }
    }
}
