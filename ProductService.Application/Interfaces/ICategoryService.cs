using Microsoft.AspNetCore.Mvc;
using ProductService.Application.DTOs;
using ProductService.Core.Entities;
using SharedService.Lib.Interfaces;

namespace ProductService.Application.Interfaces
{
    public interface ICategoryService
    {
        public Task<ProductCategory> GetCategory(int id);
        public Task<Response> CreateCategory(CategoryDto dto);
        public Task<IEnumerable<ProductCategory>> GetAllCategories();
        public Task<IEnumerable<ProductDto>> GetProductsByCategory(string categoryName);

    }
}
