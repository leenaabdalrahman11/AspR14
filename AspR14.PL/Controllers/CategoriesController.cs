using AspR14.BLL.Services;
using AspR14.DAL.DTOs.Requests;
using AspR14.PL.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AspR14.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

            return Ok(new
            {
                message = _localizer["Success"].Value,
                data = response
            });
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(CategoryRequest request)
        {
            var response = await _categoryService.CreateCategory(request);

            return Ok(new
            {
                message = _localizer["Success"].Value,
                data = response
            });
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetCategory(c => c.Id == id);
            return Ok(category);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _categoryService.DeleteCategory(id);
            if (!delete)
            {
                return BadRequest();
            }
            return Ok();
        }
        
    }
}