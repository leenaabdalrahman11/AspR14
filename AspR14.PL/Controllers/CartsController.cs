using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspR14.BLL.Services;
using AspR14.DAL.Data;
using AspR14.DAL.DTOs.Requests;
using AspR14.DAL.Repositories;
using AspR14.PL.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace AspR14.PL.Controllers
{
    [Route("[controller]")]
    public class CartsController : BaseApiController
    {
        private readonly ICartService _cartService;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public CartsController(AppDbContext context,IStringLocalizer<SharedResources> localizer,ICartService cartService)
        {
                _cartService = cartService;
                _localizer =localizer;
            
        }
        [HttpPost("")]
        public async Task<IActionResult> AddToCart(CartItemRequest request)
        {
            var result = await _cartService.AddToCart(CurrentUserId , request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
            
        }
        [HttpGet("")]
         public async Task<IActionResult> GetCartItems()
        {
            var result = await _cartService.GetCartItems(CurrentUserId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }

        [HttpPut("")]
         public async Task<IActionResult> UpdateCartItem(int productId , int count)
        {
            var result = await _cartService.UpdateItem(CurrentUserId,productId,count);
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }

        [HttpDelete("")]
        public async Task<IActionResult> RemoveCartItem(int productId)
        {
            var result = await _cartService.RemoveItem(CurrentUserId ,productId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }

    }
}