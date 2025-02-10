using Microsoft.IdentityModel.Tokens;
using ProductService.Application.DTOs;
using ProductService.Application.Interfaces;
using ProductService.Core.Entities;
using SharedService.Lib.Interfaces;
using SharedService.Lib.PubSub;

namespace ProductService.Application.Services
{
    public class CategoryService: ICategoryService
    {
        private IGenericRepository<ProductCategory> _categoryRepository;
        public CategoryService(IGenericRepository<ProductCategory> categoryRepository) {
            _categoryRepository=categoryRepository;
        }

        public async Task<ProductCategory> GetCategory(int id)
        {
            var res = await _categoryRepository.GetByIdAsync(id);
            return res!;
        }

        public async Task<Response> CreateCategory(CategoryDto categoryDto)
        {
            var category = new ProductCategory()
            {
                CategoryName = categoryDto.CategoryName,
                Description = categoryDto.Description
            };

            if (!string.IsNullOrEmpty(categoryDto?.ParentCategoryId))
            {
                var parent = GetCategory(Convert.ToInt32(categoryDto.ParentCategoryId));
                if (parent is not null)
                {
                    category.ParentCategory = parent.Result;
                }
            }
            
            var res = await _categoryRepository.CreateAsync(category);
            return res;
        }
    }
}
