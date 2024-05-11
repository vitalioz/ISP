using System;
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
    public class WebUsersDevicesController : Controller
    {
        private readonly IConfiguration m_config;
        string sConnectionString = "";

        WebUsersDevices oWebUsersDevices = new WebUsersDevices();
        WebUsersDevicesDAL oWebUsersDevicesDAL = new WebUsersDevicesDAL();
        public WebUsersDevicesController(IConfiguration config)
        {
            m_config = config;
            sConnectionString = m_config.GetConnectionString("DBConnectionString");
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public int Create([FromHeader] Interface oInterface)
        {
            oInterface.ID = oWebUsersDevicesDAL.AddRecord(sConnectionString, oInterface);
            //return RedirectToAction("Index");
            return oInterface.ID;
        }
        [HttpPost]
        [Route("[action]")]
        public int Update([FromHeader] Interface oInterface)
        {
            int id = oInterface.ID;
            WebUsersDevices oWebUsersDevices = new WebUsersDevices();
            if (id != 0)
            {
                oWebUsersDevices = oWebUsersDevicesDAL.GetRecord(sConnectionString, id);

                oWebUsersDevices.WU_ID = oInterface.WU_ID;
                oWebUsersDevicesDAL.UpdateRecord(sConnectionString, oInterface);
            }
           
            return oWebUsersDevices.ID;
        }
        [HttpPost]
        [Route("[action]")]
        public int UpdateWU_ID([FromForm] int id, [FromForm] int wu_id)
        {
            oWebUsersDevicesDAL.UpdateWU_ID(sConnectionString, id, wu_id);
            return wu_id;
        }
        [HttpPost]
        [Route("[action]")]
        public int UpdateStatus([FromForm] string id, [FromForm] string unique_id, [FromForm] int status)
        {
            int result = 0;
            result = oWebUsersDevicesDAL.UpdateStatus(sConnectionString, id, unique_id+"", status);
            return result;
        }
        [HttpPost]
        [Route("[action]")]
        public int Delete([FromForm] int id)
        {
            oWebUsersDevicesDAL.DeleteRecord(sConnectionString, id);
            return id;
        }
        [HttpGet]
        [Route("[action]/{sParameters}")]
        public int GetWebUsersDevices_ID(string sParameters)
        {
            WebUsersDevices oWebUsersDevices = new WebUsersDevices();
            oWebUsersDevices.ID = oWebUsersDevicesDAL.GetRecord_ID(sConnectionString, sParameters);

            return oWebUsersDevices.ID;
        }
        [HttpGet]
        [Route("[action]/{sParameters}")]
        public WebUsersDevices GetWebUsersDevices_Data(string sParameters)
        {
            WebUsersDevices oWebUsersDevices = new WebUsersDevices();
            oWebUsersDevices = oWebUsersDevicesDAL.GetRecord_Data(sConnectionString, sParameters);

            return oWebUsersDevices;
        }
        [HttpGet]
        [Route("[action]/{sParameters}")]
        public List<int> GetWebUsersDevicesList_ID(string sParameters)
        {
            List<int> lstID = new List<int>();
            lstID = oWebUsersDevicesDAL.GetList_ID(sConnectionString, sParameters);

            return lstID;
        }
        [HttpGet]
        [Route("[action]/{sParameters}")]
        public List<WebUsersDevices> GetWebUsersDevicesList_Data(string sParameters)
        {
            List<WebUsersDevices> lstWebUsersDevices = new List<WebUsersDevices>();

            lstWebUsersDevices = oWebUsersDevicesDAL.GetList_Data(sConnectionString, sParameters);

            return lstWebUsersDevices;
        }
        [HttpGet]
        [Route("[action]/{unique_ID}")]
        public List<WebUsersDevices> GetList_Unique_ID(string unique_ID)
        {
            List<WebUsersDevices> lstWebUsersDevices = new List<WebUsersDevices>();

            lstWebUsersDevices = oWebUsersDevicesDAL.GetList_Unique_ID(sConnectionString, unique_ID);

            return lstWebUsersDevices;
        }

        [HttpGet]
        [Route("[action]/{unique_ID}")]
        public int GetDeviceStatus(string unique_ID)
        {
            int iStatus = 0;

            iStatus = oWebUsersDevicesDAL.GetWebUsersDevicesStatus(sConnectionString, unique_ID);

            return iStatus;
        }
    }
}
