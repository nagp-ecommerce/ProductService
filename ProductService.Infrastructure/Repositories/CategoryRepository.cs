using Microsoft.Extensions.Logging;
using ProductService.Core.Entities;
using ProductService.Infrastructure.Data;

namespace ProductService.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<ProductCategory>
    {
        public CategoryRepository(ProductDbContext _dbContext, ILogger<CategoryRepository> _logger) 
            : base(_dbContext, _logger)
        {}

    }
}
