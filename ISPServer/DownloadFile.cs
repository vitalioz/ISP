using System.Data;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace ISPServer
{
    class DownloadFile
    {
        public int Go(DataRow dtRow)
        {
            int iResult = 0;
            string sTargetFileFullName = "";
            var download_file = JsonConvert.DeserializeObject<Download_File>(dtRow["Parameters"] + "");
            sTargetFileFullName = download_file.target_folder + "/" + download_file.file_name;

            WebClient client = new WebClient();
            client.Credentials = new NetworkCredential("f7ptrad3rusr", "ELhu@0XJ@UBhLp");
            client.DownloadFile(
                "ftp://867.bd4.myftpupload.com/1_Investment_Proposals/" + download_file.file_name, sTargetFileFullName);
            if (File.Exists(sTargetFileFullName)) iResult = 1;

            return iResult;
        }
        public class Download_File
        {
            public string file_name { get; set; }
            public string target_folder { get; set; }
        }
    }
}
