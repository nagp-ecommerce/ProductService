using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.DTOs;
using ProductService.Application.Interfaces;


namespace ProductService.Api.Controllers
{
    [Route("api/category")]
    [ApiController]
    [AllowAnonymous]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService) {
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            return Ok("value");
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var res = await _categoryService.CreateCategory(categoryDto);
            return Ok(res);
        }
    }
}
