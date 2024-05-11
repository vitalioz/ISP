using Microsoft.AspNetCore.Mvc;
using ISPWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ISPWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServerJobsController : Controller
    {
        private readonly IConfiguration m_config;
        string sConnectionString = "";

        ServerJobsDAL objServerJobs = new ServerJobsDAL();
        ServerJobs oServerJobs = new ServerJobs();

        public ServerJobsController(IConfiguration config)
        {
            m_config = config;
            sConnectionString = m_config.GetConnectionString("DBConnectionString");
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]        
        public IEnumerable<ServerJobs> GetWebUser_Auth()
        {
            List<ServerJobs> lstServerJobs = new List<ServerJobs>();

            lstServerJobs = objServerJobs.GetAllServerJobs(sConnectionString, System.DateTime.Now, System.DateTime.Now, 0, 0, -1).ToList();

            return lstServerJobs.ToArray();
        }
        [HttpGet("{iJobType_ID}")]
        public ServerJobs GetWebUser_ID(int iJobType_ID, string sParameters)
        {
            oServerJobs = new ServerJobs();

            //oServerJobs = objServerJobs.GetServerJobsData(sConnectionString, id);
            oServerJobs.JobType_ID = iJobType_ID;
            oServerJobs.Source_ID = 0;
            oServerJobs.Parameters = sParameters;
            oServerJobs.DateStart = System.DateTime.Now;
            oServerJobs.DateFinish = System.DateTime.Now;
            oServerJobs.PubKey = "";
            oServerJobs.PrvKey = "";
            oServerJobs.Attempt = 0;
            oServerJobs.Status = 0;
            objServerJobs.AddServerJob(sConnectionString, oServerJobs);

            return oServerJobs;
        }
        [Route("[action]/{sParameters}")]
        public string SendEmail(string sParameters)
        {
            oServerJobs = new ServerJobs();
            oServerJobs.JobType_ID = 41;
            oServerJobs.Source_ID = 0;
            oServerJobs.Parameters = sParameters;
            oServerJobs.DateStart = System.DateTime.Now;
            oServerJobs.DateFinish = System.Convert.ToDateTime("1900/01/01");
            oServerJobs.PubKey = "";
            oServerJobs.PrvKey = "";
            oServerJobs.Attempt = 0;
            oServerJobs.Status = 0;
            objServerJobs.AddServerJob(sConnectionString, oServerJobs);

            return "1";
        }
        [Route("[action]/{sParameters}")]
        public string SendEmail46(string sParameters)
        {
            int iAddSecs = 0;
            var Params = JsonConvert.DeserializeObject<Parameters>(sParameters);
            if ((Params.delay + "") != "") iAddSecs = System.Convert.ToInt32(Params.delay);

            oServerJobs = new ServerJobs();
            oServerJobs.JobType_ID = 46;
            oServerJobs.Source_ID = 0;
            oServerJobs.Parameters = sParameters;
            oServerJobs.DateStart = System.DateTime.Now.AddSeconds(iAddSecs);                    //### +45sec  +AddSecs 
            oServerJobs.DateFinish = System.Convert.ToDateTime("1900/01/01");
            oServerJobs.PubKey = "";
            oServerJobs.PrvKey = "";
            oServerJobs.Attempt = 0;
            oServerJobs.Status = 0;
            objServerJobs.AddServerJob(sConnectionString, oServerJobs);

            return "1";
        }
        [Route("[action]/{sParameters}")]
        public string SendSMS(string sParameters)
        {
            oServerJobs = new ServerJobs();
            oServerJobs.JobType_ID = 42;
            oServerJobs.Source_ID = 0;
            oServerJobs.Parameters = sParameters;
            oServerJobs.DateStart = System.DateTime.Now;
            oServerJobs.DateFinish = System.Convert.ToDateTime("1900/01/01");
            oServerJobs.PubKey = "";
            oServerJobs.PrvKey = "";
            oServerJobs.Attempt = 0;
            oServerJobs.Status = 0;
            objServerJobs.AddServerJob(sConnectionString, oServerJobs);

            return "1";
        }
        [Route("[action]/{sParameters}")]
        public string UploadFile(string sParameters)
        {
            oServerJobs = new ServerJobs();
            oServerJobs.JobType_ID = 16;
            oServerJobs.Source_ID = 0;
            oServerJobs.Parameters = sParameters;
            oServerJobs.DateStart = System.DateTime.Now.AddSeconds(60);                           //### +60sec                     
            oServerJobs.DateFinish = System.Convert.ToDateTime("1900/01/01");
            oServerJobs.PubKey = "";
            oServerJobs.PrvKey = "";
            oServerJobs.Attempt = 0;
            oServerJobs.Status = 0;
            objServerJobs.AddServerJob(sConnectionString, oServerJobs);

            return "1";
        }
        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public IActionResult NewServerJob([Bind] ServerJobs oServerJobs)
        {
            if (ModelState.IsValid)
            {
                objServerJobs.AddServerJob(sConnectionString, oServerJobs);
                return RedirectToAction("Index");
            }
            return View(oServerJobs);
        }
        public class Parameters
        {
            public string delay { get; set; }

        }
    }
}
