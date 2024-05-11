using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Newtonsoft.Json;
using Core;

namespace ISPServer
{
    class FTPUploadFile
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString);

        public int Go(DataRow dtRow)
        {
            int i = 0, iResult = 0;
            string sTemp = "", sFileName = "", sSourceFullFileName = "", sTargetFullFileName = "";
            var copy_file = JsonConvert.DeserializeObject<Copy_File>(dtRow["Parameters"] + "");
            clsInvestIdees_Attachments klsInvestIdees_Attachment = new clsInvestIdees_Attachments();

            klsInvestIdees_Attachment = new clsInvestIdees_Attachments();
            klsInvestIdees_Attachment.Record_ID = copy_file.file_id;
            klsInvestIdees_Attachment.GetRecord();
            sFileName = Path.GetFileName(klsInvestIdees_Attachment.UploadFilePath);
            i = klsInvestIdees_Attachment.UploadAttempts;

            sSourceFullFileName = "Y:\\1_Investment_Proposals\\" + sFileName;

            if (File.Exists(sSourceFullFileName))
            {

            }

            sTargetFullFileName = "ftp://" + Global.RS_Address + "/Company/InvestProposals_Products/" + copy_file.target_folder + "/" + sFileName;
            //sTargetFullFileName = sTargetFullFileName.Replace("\\", "//");
            //if (DMS_CheckFileExists(Path.GetFullPath(sTargetFullFileName), Path.GetFileName(sTargetFullFileName)))
            //{
            //    copy_file.file_name = Path.GetFileNameWithoutExtension(copy_file.file_name) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(copy_file.file_name);
            //    sTargetFullFileName = Global.DocFilesPath_FTP + "/" + copy_file.target_folder + "/" + copy_file.file_name;
            //}

            sSourceFullFileName = sSourceFullFileName.Trim();
            sTargetFullFileName = sTargetFullFileName.Trim();
            // MsgBox("WEB   Source File =" & sSourceFullFileName & vbCrLf & vbCrLf & "Target File = " & sTargetFullFileName);
            while (true)
            {
                System.Net.FtpWebRequest miRequest = (System.Net.FtpWebRequest)System.Net.WebRequest.Create(sTargetFullFileName);
                miRequest.Credentials = new System.Net.NetworkCredential(Global.RS_Username, Global.RS_Password);
                miRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
                try
                {
                    var bFile = File.ReadAllBytes(sSourceFullFileName);
                    var miStream = miRequest.GetRequestStream();
                    miStream.Write(bFile, 0, bFile.Length);
                    miStream.Close();
                    miStream.Dispose();
                    iResult = 1;
                    break;
                }
                catch (Exception ex)
                {
                    sTargetFullFileName = "";
                    sTemp = ex.Message;
                }
            }

            klsInvestIdees_Attachment.UploadAttempts = i + 1;
            klsInvestIdees_Attachment.RemoteFileName = Path.GetFileName(sTargetFullFileName);
            klsInvestIdees_Attachment.EditRecord();           

            return iResult;
        }
        public class Copy_File
        {
            public int file_id { get; set; }
            public string target_folder { get; set; }
        }
    }
}
