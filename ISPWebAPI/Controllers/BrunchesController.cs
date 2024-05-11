using Microsoft.AspNetCore.Mvc;
using ISPWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace ISPWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrunchesController : Controller
    {
        private readonly IConfiguration m_config;
        string sConnectionString = "";

        BrunchesDAL objBrunches = new BrunchesDAL();
        Brunches oBrunches = new Brunches();

        public BrunchesController(IConfiguration config)
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
        public IEnumerable<Brunches> GetBrunchesList()
        {
            List<Brunches> lstBrunches = new List<Brunches>();

            lstBrunches = objBrunches.GetBrunchesList(sConnectionString).ToList();

            return lstBrunches.ToArray();
        }
    }
}
