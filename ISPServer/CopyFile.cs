using System;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using Core;

namespace ISPServer
{
    class CopyFile
    {
        clsClientsDocFiles klsClientDocFiles = new clsClientsDocFiles();
        public int Go(DataRow dtRow)
        {
            int iResult = 0;
            string sTemp = "", sTemp1 = "";
            var copy_file = JsonConvert.DeserializeObject<Copy_File>(dtRow["Parameters"] + "");

            sTemp = Global.DMSTransferPoint + "/" + copy_file.file_name;
            sTemp1 = Global.DocFilesPath_Win + "/" + copy_file.target_folder + "/" + copy_file.file_name;

            if (File.Exists(sTemp))
            {
                if (File.Exists(sTemp1))
                    sTemp1 = Path.GetDirectoryName(sTemp1) + "/" + Path.GetFileNameWithoutExtension(sTemp1) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sTemp1);

                try
                {
                    File.Copy(sTemp, sTemp1);
                    iResult = 1;
                }
                catch (Exception ex) { sTemp = ex.Message; }
                finally { }
            }
            return iResult;
        }
        public class Copy_File
        {
            public string file_name { get; set; }
            public string target_folder { get; set; }
        }
    }
}
