using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspR14.DAL.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using AspR14.BLL.Services;
using Microsoft.Extensions.Logging;

namespace AspR14.PL.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IAuthenticationUserService _authenticationService;

        public AccountController(IAuthenticationUserService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authenticationService.RegisterAsync(request);
            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authenticationService.LoginAsync(request);
            return Ok(result);
        }
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailRequest request)
        {
            var result = await _authenticationService.ConfirmEmailAsync(request);
            if (result)
            {
                return Ok(new { Message = "Email confirmed successfully." });
            }
            else
            {
                return BadRequest(new { Message = "Email confirmation failed." });
            }
        }
    }
}