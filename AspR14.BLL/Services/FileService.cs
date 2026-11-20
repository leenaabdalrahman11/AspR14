using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspR14.DAL.DTOs.Responses;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
namespace AspR14.BLL.Services
{
    public class FileService : IFileService
    {
        private static readonly string[] _allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly Cloudinary _cloudinary;
        public FileService(IConfiguration configuration)
        {
            var account = new Account(
                configuration["CloudinarySettings:CloudName"],
                configuration["CloudinarySettings:ApiKey"],
                configuration["CloudinarySettings:ApiSecret"]
            );
            _cloudinary = new Cloudinary(account);
        }

        const long _maxFileSize = 5 * 1024 * 1024;
        public async Task<Result<FileResult>> UploadFileAsync(IFormFile file)
        {
            if (file is null || file.Length == 0)
            {
                return Result<FileResult>.Fail("No file provided for upload.");
            }
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(extension))
            {
                return Result<FileResult>.Fail("Invalid file type. Only image files are allowed.");
            }
            if (file.Length > _maxFileSize)
            {
                return Result<FileResult>.Fail("File size exceeds the limit.");
            }
            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = "AspR14"
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                if (uploadResult.Error != null)
                {
                    return Result<FileResult>.Fail(uploadResult.Error.Message);
                }
                return Result<FileResult>.Ok(
     new FileResult
     {
         FileUrl = uploadResult.SecureUrl.ToString(),
         PublicId = uploadResult.PublicId
     },
     "File uploaded successfully."
 );
            }
        }

        public async Task<Result<bool>> DeleteAsync(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);

            var result = await _cloudinary.DestroyAsync(deletionParams);

            if (result.Result != "ok")
            {
                return Result<bool>.Fail(
                    "Failed to delete image from Cloudinary");
            }

            return Result<bool>.Ok(true);
        }
    }
}