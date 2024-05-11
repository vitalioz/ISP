using System; 
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ISPServer
{
    class CreateFolder
    {
        public int Go(DataRow dtRow)
        {
            int iResult = 0;
            var Create_Folder = JsonConvert.DeserializeObject<Create_Folder>(dtRow["Parameters"] + "");

            if (!(Directory.Exists(Create_Folder.folder_name))) { 
                Directory.CreateDirectory(Create_Folder.folder_name);

                try
                {
                    if ((Directory.Exists(Create_Folder.folder_name))) iResult = 1;
                }
                catch (Exception ex) { string sTemp = ex.Message; }
                finally { }
            }
            else iResult = 1;

            return iResult;
        }
        public class Create_Folder
        {
            public string folder_name { get; set; }
        }
    }
}
