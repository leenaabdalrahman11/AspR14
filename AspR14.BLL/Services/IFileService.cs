using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspR14.DAL.DTOs.Responses;
using Microsoft.AspNetCore.Http;

namespace AspR14.BLL.Services
{
    public interface IFileService
    {
        Task<Result<string?>> UploadFileAsync(IFormFile file);
    }
}