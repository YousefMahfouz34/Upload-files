using Microsoft.AspNetCore.Mvc;
using UploadFiles.DTO;
using UploadFiles.Models;

namespace UploadFiles.Services.Iservices
{
    public interface IFileServices
    {
        Task<bool> UploadFile(UploadeFileDTO uploadeFiles);
        Task<List<UploadeFile>> GetUploadedFiles();
      //  Task<FileStreamResult> DownloadFiles(string filename);

    }
}
