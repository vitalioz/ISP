using Microsoft.AspNetCore.Mvc;
using ISPWebAPI.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Core;

namespace ISPWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebUsersController : Controller
    {
        private readonly IConfiguration m_config;
        string sConnectionString = "";

        WebUsersDAL oWebUsersDAL = new WebUsersDAL();
        WebUsers oWebUsers = new WebUsers();

        WebUsersDevicesDAL oWebUsersDevicesDAL = new WebUsersDevicesDAL();
        WebUsersDevices oWebUsersDevices = new WebUsersDevices();
        public WebUsersController(IConfiguration config)
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
        public int GetWebUser_ID(string sParameters)
        {
            WebUsers oWebUsers = new WebUsers();
            oWebUsers.ID = oWebUsersDAL.GetRecord_ID(sConnectionString, sParameters);

            return oWebUsers.ID;
        }
        [HttpGet]
        [Route("[action]/{sParameters}")]
        public WebUsers GetWebUser_Data(string sParameters)
        {
            WebUsers oWebUsers = new WebUsers();
            oWebUsers = oWebUsersDAL.GetRecord_Data(sConnectionString, sParameters);

            return oWebUsers;
        }
        [HttpPost]
        [Route("[action]")]
        public int AddWebUser([FromHeader] Interface oInterface)
        {
            oInterface = oWebUsersDAL.AddRecord(sConnectionString, oInterface);
            return oInterface.WU_ID;
        }

        [HttpPost]
        [Route("[action]")]
        public Interface Create([FromHeader] Interface oInterface)
        {
            oInterface = oWebUsersDAL.AddRecord(sConnectionString, oInterface);
            oInterface.Status = 1;
            //oWebUsersDevicesDAL.AddRecord(sConnectionString, oInterface);

            return oInterface;
        }
        [HttpPost]
        [Route("[action]")]
        public int UpdateStatus([FromHeader] Interface oInterface)
        {  
            oWebUsersDAL.UpdateStatus(sConnectionString, oInterface.ID, oInterface.Status);
            return oInterface.ID > 0 ? 1 : 0;
        }
        [HttpPost]
        [Route("[action]")]
        public int UpdatePassword([FromHeader] Interface oInterface)
        {
            oWebUsersDAL.UpdatePassword(sConnectionString, oInterface.WU_ID, oInterface.Password);
            return oInterface.WU_ID;
        }
        [HttpGet]
        [Route("[action]/{sParameters}")]
        public int MatchWUC(string sParameters)
        {
            sParameters = sParameters.Replace("{{", "{").Replace("}}", "}");
            var matchData = JsonConvert.DeserializeObject<MatchData>(sParameters);
            oWebUsersDAL.MatchWUC(sConnectionString, matchData.WU_ID, matchData.Client_ID);
            return matchData.WU_ID;
        }
        [HttpGet]
        [Route("[action]")]
        public int Delete(int id)
        {
            oWebUsersDAL.DeleteRecord(sConnectionString, id);
            return id ;
        }
        public class UserData
        {
            public string email { get; set; }
            public string mobile_phone { get; set; }
            public string password { get; set; }
            public string pin { get; set; }
        }
        public class MatchData
        {
            public int WU_ID { get; set; }
            public int Client_ID { get; set; }
        }
    }
}
