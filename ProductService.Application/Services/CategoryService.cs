using ProductService.Core.Entities;
using SharedService.Lib.Interfaces;

namespace ProductService.Application.Services
{
    public class CategoryService
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
    }
}
