using System;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using Core;

namespace ISPServer
{
    class AdvancedCopyFile
    {
        clsClientsDocFiles klsClientDocFiles = new clsClientsDocFiles();
        public int Go(DataRow dtRow)
        {
            int iResult = 0;
            string sTemp = "", sTemp1 = "";
            var copy_file = JsonConvert.DeserializeObject<Copy_File>(dtRow["Parameters"] + "");

            sTemp = Global.DMSTransferPoint + "/" + copy_file.file_name;
            sTemp1 = Global.DocFilesPath_Win + "/" + copy_file.target_folder + "/" + copy_file.file_name;

            //clsLogger Logger = new clsLogger();
            //Logger.Author_ID = Global.User_ID;
            //Logger.DateIns = DateTime.Now;
            //Logger.Rec_ID = 0;
            //Logger.Notes = " SOURCE file " + sTemp +  "  TARGET file = " + sTemp1;
            //Logger.InsertRecord();

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
                    klsClientDocFiles.Client_ID = copy_file.client_id;
                    klsClientDocFiles.ClientName = "";
                    klsClientDocFiles.ContractCode = "";
                    klsClientDocFiles.DocTypes = copy_file.file_type;
                    klsClientDocFiles.DMS_Files_ID = -1;
                    klsClientDocFiles.OldFileName = "";
                    klsClientDocFiles.NewFileName = copy_file.file_name + "";
                    klsClientDocFiles.FullFileName = copy_file.source_file_full_name + "";
                    klsClientDocFiles.DateIns = DateTime.Now;
                    klsClientDocFiles.User_ID = Global.User_ID;
                    klsClientDocFiles.Status = copy_file.status;                                  // 1 - document non confirmed, 2 - document confirmed
                    klsClientDocFiles.InsertRecord();

                    iResult = 1;
                }
                catch (Exception ex) { sTemp = ex.Message; }
                finally { }
            }
            else
            {
                //Logger = new clsLogger();
                //Logger.Author_ID = Global.User_ID;
                //Logger.DateIns = DateTime.Now;
                //Logger.Rec_ID = 0;
                //Logger.Notes = "Can't find SOURCE file " + sTemp;
                //Logger.InsertRecord();
            }
            return iResult;
        }
        public class Copy_File
        {
            public string source_file_full_name { get; set; }
            public string file_name { get; set; }
            public int file_type { get; set; }
            public string target_folder { get; set; }
            public int client_id { get; set; }
            public int status { get; set; }
        }
    }
}
