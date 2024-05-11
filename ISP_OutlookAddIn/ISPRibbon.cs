using Microsoft.Office.Tools.Ribbon;
using System;
using System.Data.SqlClient;
using Core;

namespace ISP_OutlookAddIn
{
    public partial class ISPRibbon
    {
        //SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString);
        SqlConnection conn = new SqlConnection("server=DESKTOP-9O6E46D/SQLEXPRESS;uid=sa;password=26101959;database=Trader");
        SqlCommand cmd;
        private void ISPRibbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RibbonControlEventArgs e)
        {
            Global Global = new Global();
            Global.InitConnectionString();

            clsServerJobs ServerJobs = new clsServerJobs();
            ServerJobs.JobType_ID = 44;                                             // 44  - send e-mail from Investment Proposal Params: II_ID
            ServerJobs.Source_ID = 0;
            ServerJobs.Parameters = "{'ii_id': '" + "XXX" + "'}";
            ServerJobs.DateStart = DateTime.Now;
            ServerJobs.DateFinish = System.Convert.ToDateTime("1900/01/01");
            ServerJobs.PubKey = "";
            ServerJobs.PrvKey = "";
            ServerJobs.Attempt = 0;
            ServerJobs.Status = 0;
            ServerJobs.InsertRecord();
        }
    }
}
