using System;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using Core;

namespace ISPServer
{
    class LocalUploadFile
    {
        int iDocType;
        clsClients Clients = new clsClients();
        clsClientsDocFiles klsClientDocFiles = new clsClientsDocFiles();
        public int Go(DataRow dtRow)
        {
            int iResult = 0;
            string sTemp = "", sTemp1 = "", sClientName = "";
            var upload_file = JsonConvert.DeserializeObject<UploadFile>(dtRow["Parameters"] + "");

            sTemp = Global.DMSTransferPoint + "/" + upload_file.file_name;
            iDocType = upload_file.doc_type;

            Clients = new clsClients();
            Clients.Record_ID = upload_file.client_id;
            Clients.EMail = "";
            Clients.Mobile = "";
            Clients.AFM = "";
            Clients.DoB = Convert.ToDateTime("1900/01/01");
            Clients.GetRecord();
            if (Clients.Type == 1) sClientName = (Clients.Surname + " " + Clients.Firstname).Trim();
            else sClientName = Clients.Surname.Trim();

            sTemp1 = (Global.DocFilesPath_Win + "/Customers/" + sClientName + "/" + Path.GetFileNameWithoutExtension(upload_file.file_name)).Replace(".", "_") + Path.GetExtension(upload_file.file_name);

            if (File.Exists(sTemp))
            {
                if (File.Exists(sTemp1))
                    sTemp1 = Path.GetDirectoryName(sTemp1) + "/" + Path.GetFileNameWithoutExtension(sTemp1) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sTemp1);
                try
                {
                    File.Copy(sTemp, sTemp1);

                    klsClientDocFiles = new clsClientsDocFiles();
                    klsClientDocFiles.PreContract_ID = 0;
                    klsClientDocFiles.Contract_ID = 0;
                    klsClientDocFiles.Client_ID = upload_file.client_id;
                    klsClientDocFiles.ClientName = sClientName;
                    klsClientDocFiles.ContractCode = "";
                    klsClientDocFiles.DocTypes = iDocType;
                    klsClientDocFiles.DMS_Files_ID = 9999;                                  // 9999 pseudo DMS_Files_ID
                    klsClientDocFiles.OldFileName = "";
                    klsClientDocFiles.NewFileName = upload_file.file_name + "";
                    klsClientDocFiles.FullFileName = "";                                    // must be ""
                    klsClientDocFiles.DateIns = DateTime.Now;
                    klsClientDocFiles.User_ID = Global.User_ID;
                    klsClientDocFiles.Status = upload_file.status;                          // 0 - deleted file, 1 - non confirmed file,  2 - document confirmed file
                    klsClientDocFiles.InsertRecord();

                    iResult = 1;
                }
                catch (Exception ex) { sTemp = ex.Message; }
                finally { }
            }
            return iResult;
        }
        public class UploadFile
        {
            public string file_name { get; set; }
            public int doc_type { get; set; }
            public int client_id { get; set; }
            public int status { get; set; }
        }
    }
}
