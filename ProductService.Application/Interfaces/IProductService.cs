using ProductService.Application.DTOs;
using ProductService.Core.Entities;
using SharedService.Lib.Interfaces;

namespace ProductService.Application.Interfaces
{
    public interface IProductService
    {
        public Task<Response> AddProduct(ProductDto product);
        public Task<Response> UpdateProduct(ProductDto product);
        public Task<Response> RemoveProduct(ProductDto product);
        public Task<IEnumerable<ProductDto>> GetAllProducts();
        public Task<ProductDto> GetProductById(int id);

    }
}
