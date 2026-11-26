using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AspR14.BLL.Common;
using AspR14.DAL.DTOs.Requests;
using AspR14.DAL.DTOs.Responses;
using AspR14.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AspR14.BLL.Services
{
    public class AuthenticationUserService : IAuthenticationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _config;
        public AuthenticationUserService(UserManager<ApplicationUser> userManager, IEmailSender emailSender, IConfiguration config)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _config = config;
        }
        public async Task<Result<bool>> ConfirmEmailAsync(ConfirmEmailRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return Result<bool>.Fail("User not found");
            }
            request.Token = Uri.UnescapeDataString(request.Token);
            var result = await _userManager.ConfirmEmailAsync(user, request.Token);
            if (!result.Succeeded)
            {
                return Result<bool>.Fail("Email confirmation failed. The token may be invalid or expired.");
            }
            return Result<bool>.Ok(true, "Email confirmed successfully.");


        }
        public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return Result<LoginResponse>.Fail("User not found");
            }
            if (await _userManager.IsEmailConfirmedAsync(user) == false)
            {
                return Result<LoginResponse>.Fail("Email not confirmed");
            }
            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                return Result<LoginResponse>.Fail("Invalid password");
            }
            return Result<LoginResponse>.Ok(
                new LoginResponse
                {
                    AccessToken = await GenerateJWTToken(user)
                }, "Login successful"
            );
        }

        public async Task<Result<bool>> RegisterAsync(RegisterRequest request)
        {


            var user = request.Adapt<ApplicationUser>();
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return Result<bool>.Fail("User registration failed");
            }

            var Token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            Token = Uri.EscapeDataString(Token);
            var emailUrl = $"https://localhost:5106/Account/ConfirmEmail?token={Token}&userId={user.Id}";
            await _emailSender.SendEmailAsync(request.Email, "confirm email", "<div><h1>Welcome to our application</h1>" +
            $"<a href='{emailUrl}'>Click here to confirm your email</a>" +
            "</div>");
            return Result<bool>.Ok(true, "User registered successfully. Please check your email to confirm your account.");

        }

        private async Task<string> GenerateJWTToken(ApplicationUser user)
        {
            var Roles = await _userManager.GetRolesAsync(user);
            var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, string.Join(",", Roles))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}