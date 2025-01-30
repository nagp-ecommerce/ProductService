using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.DTOs;
using ProductService.Application.Interfaces;
using ProductService.Application.Mappings;


namespace ProductService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;
        public ProductController(IProductService productService) {
            _productService= productService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetProductList()
        {
            var res = await _productService.GetAllProducts();
            var productDtos = res.ToList().Select(p => p.FromEntity());
            return productDtos;
        }

        [HttpGet("{id}")]
        public string GetProduct(int id)
        {
            return "value";
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _productService.AddProduct(productDto);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _productService.UpdateProduct(productDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromBody] ProductDto productDto)
        {
            await _productService.RemoveProduct(productDto);
            return Ok();
        }
    }
}
