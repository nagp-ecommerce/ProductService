using Microsoft.Extensions.Logging;
using ProductService.Core.Entities;
using ProductService.Infrastructure.Data;

namespace ProductService.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(ProductDbContext _dbContext, ILogger<ProductRepository> _logger) 
            : base(_dbContext, _logger)
        {}

    }
}
