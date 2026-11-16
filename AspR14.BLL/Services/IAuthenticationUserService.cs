using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspR14.DAL.DTOs.Requests;
using AspR14.DAL.DTOs.Responses;

namespace AspR14.BLL.Services
{
    public interface IAuthenticationUserService
    {
        Task<Result<bool>> RegisterAsync(RegisterRequest request);
        Task<Result<LoginResponse>> LoginAsync(LoginRequest request);
        Task<Result<bool>> ConfirmEmailAsync(ConfirmEmailRequest request);
    }
}