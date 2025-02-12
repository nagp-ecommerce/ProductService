using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductService.Application.Interfaces;
using ProductService.Core.Entities;
using ProductService.Infrastructure.Data;

namespace ProductService.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<ProductCategory>, ICategoryRepository
    {

        public CategoryRepository(ProductDbContext _dbContext, ILogger<CategoryRepository> _logger) 
            : base(_dbContext, _logger)
        {}

        public async Task<List<Product>> GetProductsByCategoryAsync(string categoryName)
        {
            var products =  await dbContext.Categories
                .Where(c => c.CategoryName == categoryName)
                .Include(c => c.Products)
                     //  loads related entities along with the main entity in a single database query.
                     // also called eager loading
                    .SelectMany(c => c.Products)
                        .ToListAsync();
            return products;

        }
    }
}
