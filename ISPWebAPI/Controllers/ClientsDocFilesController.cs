using Microsoft.AspNetCore.Mvc;
using ISPWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;


namespace ISPWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsDocFilesController : Controller
    {
        private readonly IConfiguration m_config;
        string sConnectionString = "";

        ClientsDocFilesDAL objClientsDocFiles = new ClientsDocFilesDAL();
        ClientsDocFiles oClientsDocFiles = new ClientsDocFiles();

        public ClientsDocFilesController(IConfiguration config)
        {
            m_config = config;
            sConnectionString = m_config.GetConnectionString("DBConnectionString");
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("[action]/{sParameters}")]
        public IEnumerable<ClientsDocFiles> GetClientsDocFilesList(string sParameters)
        {
            List<ClientsDocFiles> lstClientsDocFiles = new List<ClientsDocFiles>();

            lstClientsDocFiles = objClientsDocFiles.GetClientsDocFilesList(sConnectionString, sParameters).ToList();

            return lstClientsDocFiles.ToArray();
        }
        [HttpGet]
        [Route("[action]/{sParameters}")]
        public IEnumerable<ClientsDocFiles> GetClientTaxDeclarationsList(string sParameters)
        {
            List<ClientsDocFiles> lstClientsDocFiles = new List<ClientsDocFiles>();

            lstClientsDocFiles = objClientsDocFiles.GetClientTaxDeclarationsList(sConnectionString, sParameters).ToList();

            return lstClientsDocFiles.ToArray();
        }
    }
}
