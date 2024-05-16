using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UploadFiles.DTO;
using UploadFiles.Services.Iservices;

namespace UploadFiles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class FilesController : ControllerBase
    {
        private readonly IFileServices _fileServices;

        public FilesController(IFileServices fileServices)
        {
            _fileServices = fileServices;
        }
        [HttpGet]
        public async Task< IActionResult> GetAll()
        {
            try
           {
                var res = await _fileServices.GetUploadedFiles();
                return Ok(res); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }

        }
        [RequestSizeLimit(1000000)]
        [HttpPost]
        public async  Task<IActionResult> upload(UploadeFileDTO uploadeFile)
        {
            var res=await _fileServices.UploadFile(uploadeFile);
            if (res)
                return Ok(res);
            return BadRequest("file not uploaded the typ no allowed");

            
        }
        //[HttpGet]
        //public async Task<IActionResult> DownloadFiles(string filename)
        //{
        //    var res = await _fileServices.DownloadFiles(filename);
        //    if (res != null)
        //        return res;
        //    return NotFound("Not found");
         
           
        //}
    }
}
   

