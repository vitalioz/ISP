using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ISPServer
{
    class RenameFolder
    {
        public int Go(DataRow dtRow)
        {
            int iResult = 0;
            var rename_folder = JsonConvert.DeserializeObject<Rename_Folder>(dtRow["Parameters"] + "");

            if (Directory.Exists(rename_folder.folder1))
            {
                if (!(Directory.Exists(rename_folder.folder2)))
                {
                    Directory.Move(rename_folder.folder1, rename_folder.folder2);
                    iResult = 1;
                }   
            }
            return iResult;
        }
        public class Rename_Folder
        {
            public string folder1 { get; set; }
            public string folder2 { get; set; }
        }
    }
}
