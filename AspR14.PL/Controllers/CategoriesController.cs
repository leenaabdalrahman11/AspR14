using System.Security.Claims;
using AspR14.BLL.Services;
using AspR14.DAL.DTOs.Requests;
using AspR14.PL.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AspR14.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ICategoryService _categoryService;

        public CategoriesController(
            IStringLocalizer<SharedResources> localizer,
            ICategoryService categoryService)
        {
            _localizer = localizer;
            _categoryService = categoryService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var response = await _categoryService.GetAllCategories();

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(CategoryRequest request)
        {   
            var response = await _categoryService.CreateCategory(request);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetCategory(c => c.Id == id);
            return category.IsSuccess ? Ok(category) : NotFound(category);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _categoryService.DeleteCategory(id);
            return delete.IsSuccess ? Ok(delete) : NotFound(delete);
        }
    }
}