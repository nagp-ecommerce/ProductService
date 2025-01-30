using ProductService.Application.DTOs;
using ProductService.Core.Entities;

namespace ProductService.Application.Interfaces
{
    public interface IProductService
    {
        public Task AddProduct(ProductDto product);
        public Task UpdateProduct(ProductDto product);
        public Task RemoveProduct(ProductDto product);
        public Task<IEnumerable<Product>> GetAllProducts();
    }
}
