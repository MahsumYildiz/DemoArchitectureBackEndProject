using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilities.File
{
    public class FileManager : IFileService
    {
        public string FileSaveToServer(IFormFile file, string filepath)
        {
            var fileFormat = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            fileFormat = fileFormat.ToLower();
            string fileName = Guid.NewGuid().ToString() + fileFormat;
            string path = filepath + fileName;

            using (var stream = System.IO.File.Create(path))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }

        public string FileSaveToFtp(IFormFile file)
        {
            var fileFormat = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            fileFormat = fileFormat.ToLower();
            string fileName = Guid.NewGuid().ToString() + fileFormat;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("FTP Adresimiz yazılacak" + fileName);
            request.Credentials = new NetworkCredential("Kullanıcı adı", "Şifre");
            request.Method = WebRequestMethods.Ftp.UploadFile;

            using (Stream ftpStream = request.GetRequestStream())
            {
                file.CopyTo(ftpStream);
            }
            return fileName;
        }

        public void FileDeleteToServer(string path)
        {
            try
            {
                if(System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            catch (Exception)
            {

                
            }
        }

        public void FileDeleteToFtp(string path)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("Ftp adresi" + path);
                request.Credentials = new NetworkCredential("kullanıcı adı", "şifre");
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse response= (FtpWebResponse)request.GetResponse();
            }
            catch (Exception)
            {

               
            }
        }
    }
}
