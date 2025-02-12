using Microsoft.AspNetCore.Mvc;
using ProductService.Application.DTOs;
using ProductService.Core.Entities;
using SharedService.Lib.Interfaces;

namespace ProductService.Application.Interfaces
{
    public interface ICategoryRepository: IGenericRepository<ProductCategory>
    {
        public Task<List<Product>> GetProductsByCategoryAsync(string categoryName);
    }
}
