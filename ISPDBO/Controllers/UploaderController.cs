using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ISPDBO.Models;

namespace ISPDBO.Controllers
{
    public class UploaderController : Controller
    {
        private IWebHostEnvironment hostingEnvironment;
        public UploaderController(IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public string Index(IList<IFormFile> files)
        {
            string sResult = "";
            string fileName = "";
            string sTransferFolder = Global.TransferFolder;

            foreach (IFormFile postedFile in files)
            {
                fileName = Path.GetFileName(postedFile.FileName);
                MemoryStream ms = new MemoryStream();
                postedFile.CopyTo(ms);
                
                if (UploadFile(Path.GetFileName(postedFile.FileName), ms, sTransferFolder) == 1) 
                    sResult = Path.GetFileName(postedFile.FileName);               
            }

            /*
            foreach (IFormFile source in files)
            {
                string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');

                filename = this.EnsureCorrectFilename(filename);

                using (FileStream output = System.IO.File.Create(this.GetPathAndFilename(filename)))
                    await source.CopyToAsync(output);
            }
            */

            return sResult;
        }

        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

        private string GetPathAndFilename(string filename)
        {
            string path = this.hostingEnvironment.WebRootPath + "\\uploads\\";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path + filename;
        }

        //--- Upload file function --------------------------------------------------------------
        private int UploadFile(string sFileName, MemoryStream ms, string sTargetFolderPath)
        {
            string ftp = Global.FTP_Host;
            string ftp_username = Global.FTP_Username;
            string ftp_password = Global.FTP_Password;

            byte[] fileBytes = ms.ToArray();
            int iResult = 0;
            
            try
            {
                //Create FTP Request.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create((ftp + "/" + (sTargetFolderPath.Length > 0 ? sTargetFolderPath + "/" : "") + sFileName));
                request.Method = WebRequestMethods.Ftp.UploadFile;

                //Enter FTP Server credentials.
                request.Credentials = new NetworkCredential(ftp_username, ftp_password);
                request.ContentLength = fileBytes.Length;
                request.UsePassive = true;
                request.UseBinary = true;
                request.ServicePoint.ConnectionLimit = fileBytes.Length;
                request.EnableSsl = false;

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(fileBytes, 0, fileBytes.Length);
                    requestStream.Close();
                }

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                response.Close();
                iResult = 1;
            }
            catch (WebException ex)
            {
                //throw new Exception((ex.Response as FtpWebResponse).StatusDescription);                
            }

            return iResult;
        }
    }
}
