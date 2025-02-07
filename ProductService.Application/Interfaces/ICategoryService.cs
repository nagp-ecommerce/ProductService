using ProductService.Application.DTOs;
using ProductService.Core.Entities;

namespace ProductService.Application.Interfaces
{
    public interface ICategoryService
    {
        public Task GetCategory(int id);
        public Task CreateCategory(CategoryDto dto);
    }
}
