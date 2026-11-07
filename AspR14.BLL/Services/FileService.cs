using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace AspR14.BLL.Services
{
    public class FileService
    {
        public static async Task<string?> UploadFileAsync(IFormFile file)
        {
            if(file is not null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString()
                + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(), "uploads", fileName
                );
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return fileName;
            }
            return null;
            
        }
    }
}