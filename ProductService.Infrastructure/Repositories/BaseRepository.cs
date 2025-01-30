using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductService.Infrastructure.Data;
using SharedService.Lib.Interfaces;

namespace ProductService.Infrastructure.Repositories
{
    public class BaseRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ProductDbContext dbContext;
        private readonly ILogger<IGenericRepository<T>> logger;
        public BaseRepository(
            ProductDbContext _dbContext, 
            ILogger<IGenericRepository<T>> _logger
        ) 
        {
            dbContext = _dbContext;
            logger=_logger;
        }
        public async Task<Response> CreateAsync(T Entity)
        {
            try
            {
                await dbContext.Set<T>().AddAsync(Entity);
                await dbContext.SaveChangesAsync();
                return new Response { Success = true, Message = $"{typeof(T).Name} created succesfully" };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error occured while creating the {typeof(T).Name}");
                return new Response { Success = false, Message= $"Error occured while creating the {typeof(T).Name}" };
            }

        }

        public async Task<Response> DeleteAsync(int id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                if(entity == null)
                    return new Response { Success = false, Message = $"{typeof(T).Name} not found" };

                dbContext.Set<T>().Remove(entity);
                await dbContext.SaveChangesAsync();
                return new Response { Success = true, Message = $"{typeof(T).Name} deleted succesfully" };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error occured while deleting the {typeof(T).Name}");
                return new Response { Success = false, Message = $"Error occured while deleting the {typeof(T).Name}" };
            }
        }

        public async Task<Response> UpdateAsync(T Entity)
        {
            try
            {
                
                var res = dbContext.Set<T>().Update(Entity);
                if (res == null)
                    return new Response { 
                        Success = false, 
                        Message = $"{typeof(T).Name} not found" 
                    };

                await dbContext.SaveChangesAsync();
                return new Response { 
                    Success = true, 
                    Message = $"{typeof(T).Name} updated succesfully" 
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error occured while updating the {typeof(T).Name}");
                return new Response { Success = false, Message = $"Error occured while updating the {typeof(T).Name}" };
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await dbContext.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while fetching all Products");
                return null!;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                var product = await dbContext.Set<T>().FindAsync(id);
                return product ?? null!;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while fetching all Products");
                return null!;
            }
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var product = await dbContext.Set<T>().Where(predicate).FirstOrDefaultAsync();
                return product ?? null!;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occured while retrieving all Products");
                return null!;
            }
        }
    }
}
