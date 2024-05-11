using Microsoft.AspNetCore.Mvc;
using ISPWebAPI.Models;
using Microsoft.Extensions.Configuration;

namespace ISPWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebUsersStatesController : Controller
    {
        private readonly IConfiguration m_config;
        string sConnectionString = "";

        WebUsersStatesDAL oWebUsersStatesDAL = new WebUsersStatesDAL();
        WebUsersStates oWebUsersStates = new WebUsersStates();

        public WebUsersStatesController(IConfiguration config)
        {
            m_config = config;
            sConnectionString = m_config.GetConnectionString("DBConnectionString");
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("[action]/{wu_id}")]
        public int GetStatus(int wu_id)
        {
            WebUsersStates oWebUsersStates = new WebUsersStates();
            oWebUsersStates.Status = oWebUsersStatesDAL.GetStatus(sConnectionString, wu_id);

            return oWebUsersStates.Status;
        }

        [HttpGet]
        [Route("[action]/{wu_id}")]
        public WebUsersStates GetData(int wu_id)
        {
            WebUsersStates oWebUsersStates = new WebUsersStates();
            oWebUsersStates = oWebUsersStatesDAL.GetData(sConnectionString, wu_id);

            return oWebUsersStates;
        }

        [HttpPost]
        [Route("[action]")]
        public int SaveData([FromHeader] Interface oInterface)
        {
            oInterface = oWebUsersStatesDAL.SaveRecord(sConnectionString, oInterface);
            return oInterface.Result;
        }

        [HttpGet]
        [Route("[action]")]
        public int Delete(int id)
        {
            oWebUsersStatesDAL.DeleteRecord(sConnectionString, id);
            return id;
        }
        public class WUS
        {
            public int WU_ID { get; set; }
            public int Status { get; set; }
        }
    }
}
