using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspR14.DAL.DTOs.Responses;
using Microsoft.AspNetCore.Http;
namespace AspR14.BLL.Services
{
    public class FileService : IFileService
    {
        private static readonly string[] _allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        const long _maxFileSize = 5 * 1024 * 1024;
        public async Task<Result<string?>> UploadFileAsync(IFormFile file)
        {
                if (file is null || file.Length == 0)
                {
                    return new Result<string?>
                    {
                        IsSuccess = false,
                        Message = "File is null or empty."
                    };
                }
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!_allowedExtensions.Contains(extension))
                {
                    return new Result<string?>
                    {
                        IsSuccess = false,
                        Message = "File extension is not allowed."
                    };
                }
                if (file.Length > _maxFileSize)
                {
                    return new Result<string?>
                    {
                        IsSuccess = false,
                        Message = "File size exceeds the limit."
                    };
                }

                var fileName = Guid.NewGuid().ToString() + extension;
                var uploadFolder = Path.Combine(
        Directory.GetCurrentDirectory(), "wwwroot", "uploads"
    );

                Directory.CreateDirectory(uploadFolder);

                var filePath = Path.Combine(uploadFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return new Result<string?>
                {
                    IsSuccess = true,
                    Message = "File uploaded successfully.",
                    Data = fileName
                };

            
        }
    }
}