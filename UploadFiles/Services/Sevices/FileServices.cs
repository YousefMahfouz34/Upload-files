using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using UploadFiles.Context;
using UploadFiles.DTO;
using UploadFiles.Models;
using UploadFiles.Services.Iservices;

namespace UploadFiles.Services.Sevices
{
    
    public class FileServices : IFileServices
    {
        private readonly UploadeFileContext context;
        private readonly IWebHostEnvironment env;
        public FileServices(UploadeFileContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }

        //public Task<FileStreamResult> DownloadFiles(string filename)
        //{
        //    var uploadfile = context.UploadeFiles.SingleOrDefault(f => f.StoredFileName == filename);
        //    if (uploadfile == null)
        //    {
        //        throw new FileNotFoundException("File not found.");
        //    }

        //    var path = Path.Combine(env.WebRootPath, "files", filename);
        //    MemoryStream stream = new();
        //    using (FileStream fileStream = new(path, FileMode.Open, FileAccess.Read))
        //    {
        //        fileStream.CopyTo(stream);
        //    }
        //    stream.Position = 0;
        //    return Task.FromResult(new FileStreamResult(stream, uploadfile.ContentType)
        //    {
        //        FileDownloadName = uploadfile.FileName
        //    });
        //}
        //public Task<UploadeFile> DownloadFiles(string filename)
        //{
        //    var uploadfile= context.UploadeFiles.SingleOrDefault(f => f.StoredFileName == filename);
        //    var path = Path.Combine(env.WebRootPath, "files", filename);
        //    MemoryStream stream = new();
        //    FileStream fileStream = new(path, FileMode.Open);
        //    fileStream.CopyTo(stream);
        //    stream.Position = 0;
        //   var res=  File(stream, uploadfile.ContentType,uploadfile.FileName);

        //}

        public  Task<List<UploadeFile>> GetUploadedFiles()
        {
            var files= context.UploadeFiles.ToListAsync();
            return files;
        }
        private readonly string[] _permittedExtensions = { ".jpg", ".jpeg", ".docx", ".pdf" };
        public async Task<bool> UploadFile(UploadeFileDTO uploadeFiles)
        {
           
           try
            {
              
                List<UploadeFile> files = new();
                foreach (var file in uploadeFiles.Files)
                {
                    var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
                    if (string.IsNullOrEmpty(ext) || !_permittedExtensions.Contains(ext))
                    {
                        return false;
                    }
                    var fackfilename = Path.GetRandomFileName();
                    UploadeFile uploadeFile = new()
                    {
                        FileName = file.FileName,
                        ContentType = file.ContentType,
                        StoredFileName = fackfilename
                    };
                    var path = Path.Combine(env.WebRootPath, "files", fackfilename);
                    FileStream fileStream = new(path, FileMode.Create);
                    file.CopyTo(fileStream);
                    files.Add(uploadeFile);
                }
                context.AddRange(files);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }

            
        }
    }
}
