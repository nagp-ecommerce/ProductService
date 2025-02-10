using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.DTOs;
using ProductService.Application.Interfaces;
using ProductService.Application.Mappings;


namespace ProductService.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    [AllowAnonymous]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;
        public ProductController(IProductService productService) {
            _productService= productService 
                ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpGet("all")]
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
            if (productDto != null) 
            {
                var res = await _productService.AddProduct(productDto);
                return res != null ? Ok(res) : BadRequest(Request);
            }
            return BadRequest(Request);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var res = await _productService.UpdateProduct(productDto);
            return res != null ? Ok(res) : BadRequest(Request);

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromBody] ProductDto productDto)
        {
            var res = await _productService.RemoveProduct(productDto);
            return res  != null ? Ok(res) : BadRequest(Request);
        }
    }
}
