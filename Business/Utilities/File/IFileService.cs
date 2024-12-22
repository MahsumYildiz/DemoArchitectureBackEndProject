using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilities.File
{
    public interface IFileService
    {
        string FileSaveToServer(IFormFile file, string filepath);
        string FileSaveToFtp(IFormFile file);
        //byte[] FileCOnvertByteArrayToDataBase(IFormFile file);
        void FileDeleteToServer(string path);
        void FileDeleteToFtp(string path);

    }
}
