using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AspR14.BLL.Services;
using AspR14.DAL.DTOs.Requests;

namespace AspR14.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest request)
        {
            var category = await _categoryService.CreateCategoryAsync(request);

            return Ok(category);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            return Ok(categories);
        }
    }
}