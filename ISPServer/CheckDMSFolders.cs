using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace ISPServer
{
    public class CheckDMSFolders
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString);
        SqlConnection conn2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString);
        SqlCommand cmd, cmd2;
        string sTemp, sClientFullName, sDocFilesPath;
        public int Go()
        {
            SqlDataReader drList = null;
            SqlDataReader drList2 = null;

            sDocFilesPath = @"C:\DMS";

            conn.Open();
            conn2.Open();
            cmd = new SqlCommand("SELECT ID, Tipos, Surname, Firstname, Status  FROM Clients WHERE (Tipos = 1 or Tipos = 2) " +  
                                 " UNION " +
                                 "SELECT ID, 3 AS Tipos, ContractTitle AS Surname, '' AS Firstname, 0 AS Status  FROM Contracts WHERE Tipos = 1 ", conn);
            cmd.CommandType = CommandType.Text;
            drList = cmd.ExecuteReader();
            while (drList.Read())
            {
                switch (Convert.ToInt32(drList["Tipos"]))
                {
                    case 1:
                        sClientFullName = (drList["Surname"] + " " + drList["Firstname"]).Trim();
                        break;
                    case 2:
                        sClientFullName = drList["Surname"] + "";
                        break;
                }
                sClientFullName = sClientFullName.Replace(".", "_").Trim();

                sTemp = sDocFilesPath + @"\Customers\" + sClientFullName;
                if (Directory.Exists(sTemp))
                    System.IO.Directory.CreateDirectory(sTemp);

                if (Convert.ToInt32(drList["Status"]) >= 0) {

                    sTemp = sDocFilesPath + @"\Customers\" + sClientFullName + @"\AdvisoryPortofolioMonitoring";
                    if (Directory.Exists(sTemp))
                        System.IO.Directory.CreateDirectory(sTemp);

                    sTemp = sDocFilesPath + @"\Customers\" + sClientFullName + @"\Compliance";
                    if (Directory.Exists(sTemp))
                        System.IO.Directory.CreateDirectory(sTemp);

                    sTemp = sDocFilesPath + @"\Customers\" + sClientFullName + @"\CooperationProposals";
                    if (Directory.Exists(sTemp))
                        System.IO.Directory.CreateDirectory(sTemp);

                    sTemp = sDocFilesPath + @"\Customers\" + sClientFullName + @"\Informing";
                    if (Directory.Exists(sTemp))
                        System.IO.Directory.CreateDirectory(sTemp);

                    sTemp = sDocFilesPath + @"\Customers\" + sClientFullName + @"\InvestProposals";
                    if (Directory.Exists(sTemp))
                        System.IO.Directory.CreateDirectory(sTemp);

                    sTemp = sDocFilesPath + @"\Customers\" + sClientFullName + @"\Invoices";
                    if (Directory.Exists(sTemp))
                        System.IO.Directory.CreateDirectory(sTemp);

                    sTemp = sDocFilesPath + @"\Customers\" + sClientFullName + @"\OrdersAcception";
                    if (Directory.Exists(sTemp))
                        System.IO.Directory.CreateDirectory(sTemp);

                    sTemp = sDocFilesPath + @"\Customers\" + sClientFullName.Replace(".", "_") + @"\Movements";
                    if (Directory.Exists(sTemp))
                        System.IO.Directory.CreateDirectory(sTemp);
                }
                else
                {
                    sTemp = sDocFilesPath + @"\Customers\" + sClientFullName + @"\CooperationProposals";
                    if (Directory.Exists(sTemp))
                        System.IO.Directory.CreateDirectory(sTemp);
                }

                cmd2 = new SqlCommand("SELECT * FROM Contracts", conn2);
                cmd2.CommandType = CommandType.Text;
                drList2 = cmd2.ExecuteReader();
                while (drList2.Read())
                {
                    sClientFullName = drList2["ContractTitle"] + "";
                    sTemp = drList2["Code"] + "";
                    sTemp = sDocFilesPath + @"\Customers\" + sClientFullName.Replace(".", "_") + @"\" + sTemp.Replace(".", "_");
                    if (Directory.Exists(sTemp))
                        System.IO.Directory.CreateDirectory(sTemp);
                }
                drList2.Close();
            }
            drList.Close();
            conn2.Close();
            conn.Close();

            return 1;
        }
    }
}
