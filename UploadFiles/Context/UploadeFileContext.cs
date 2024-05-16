using Microsoft.EntityFrameworkCore;
using UploadFiles.Models;

namespace UploadFiles.Context
{
    public class UploadeFileContext:DbContext
    {
        public UploadeFileContext(DbContextOptions<UploadeFileContext>options):base(options) { }
      public DbSet<UploadeFile> UploadeFiles { get; set;}
    }
}
