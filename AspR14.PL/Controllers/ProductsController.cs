using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspR14.BLL.Services;
using AspR14.DAL.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspR14.PL.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductRequest productRequest)
        {
            
            var result = await _productService.CreateProductAsync(productRequest);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromForm] ProductRequest productRequest)
        {
            var result = await _productService.UpdateProductAsync(productId, productRequest);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var result = await _productService.DeleteProductAsync(p => p.Id == productId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var result = await _productService.GetProductByIdAsync(p => p.Id == productId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productService.GetAllProductsAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

    }
}