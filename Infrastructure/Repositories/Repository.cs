using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

public class Repository<T>(IApplicationDbContext dbContext, ILogger<Repository<T>> logger)
    : IRepository<T> where T : BaseModel
{
    public async Task<T> CreateAsync(T entity)
    {
        logger.LogInformation($"Started creating {nameof(T)} from database");
        var newEntity = dbContext.GetTable<T>().Add(entity);
        await dbContext.SaveChangesAsync();
        logger.LogInformation($"Successfully created {typeof(T).Name} from database");
        return newEntity.Entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        logger.LogInformation($"Started updating {typeof(T).Name} from database");
        var updatedEntity = dbContext.GetTable<T>().Update(entity);
        await dbContext.SaveChangesAsync();
        logger.LogInformation($"Successfully updated {typeof(T).Name} from database");
        return updatedEntity.Entity;
    }

    public async Task DeleteAsync(Guid id)
    {
        logger.LogInformation($"Started deleting {typeof(T).Name} from database");
        var entity = await GetByIdAsync(id);
        dbContext.GetTable<T>().Remove(entity);
        await dbContext.SaveChangesAsync();
        logger.LogInformation($"Successfully deleted {typeof(T).Name} from database");
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        logger.LogInformation($"Started retrieving {typeof(T).Name} from database");
        var entity = await dbContext.GetTable<T>().FirstOrDefaultAsync(p => p.Id == id);
        if (entity == null)
        {
            throw new NotFoundException($"{typeof(T).Name} with {id} not found");
        }

        logger.LogInformation($"Successfully retrieved {typeof(T).Name} from database");
        return entity;
    }

    public async Task<IEnumerable<T>> GetAllAsync(ISpecification<T> specification)
    {
        var allEntities = dbContext.GetTable<T>().AsQueryable();
        var queryableResultWithIncludes = specification.Includes
            .Aggregate(allEntities,
                (current, include) => current.Include(include));

        return await queryableResultWithIncludes
            .Where(specification.Criteria)
            .ToListAsync();
    }

    public async Task<bool> IsEntityExistsAsync(Guid id)
    {
        logger.LogInformation($"Started checking if there is {typeof(T).Name} with {id} in database");
        var response = await dbContext.GetTable<T>().AnyAsync(p => p.Id == id);
        logger.LogInformation($"Successfully found {typeof(T).Name} with {id} in database");
        return response;
    }
}