using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspR14.DAL.DTOs.Requests;
using AspR14.DAL.DTOs.Responses;
using AspR14.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace AspR14.BLL.Services
{
    public class AuthenticationUserService : IAuthenticationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthenticationUserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var user = request.Adapt<ApplicationUser>();
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return new RegisterResponse()
                {
                    Message = "error"
                };

            }
            return new RegisterResponse()
            {
                Message = "Success"
            };
            
        }
    }
}