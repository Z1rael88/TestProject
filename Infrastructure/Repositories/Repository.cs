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
        var newEntity = dbContext.GetDbSet<T>().Add(entity);
        await dbContext.SaveChangesAsync();
        logger.LogInformation($"Successfully created {nameof(T)} from database");
        return newEntity.Entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        logger.LogInformation($"Started updating {nameof(T)} from database");
        var updatedEntity = dbContext.GetDbSet<T>().Update(entity);
        await dbContext.SaveChangesAsync();
        logger.LogInformation($"Successfully updated {nameof(T)} from database");
        return updatedEntity.Entity;
    }

    public async Task DeleteAsync(Guid id)
    {
        logger.LogInformation($"Started deleting {nameof(T)} from database");
        var entity = await GetByIdAsync(id);
        dbContext.GetDbSet<T>().Remove(entity);
        await dbContext.SaveChangesAsync();
        logger.LogInformation($"Successfully deleted {nameof(T)} from database");
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        logger.LogInformation($"Started retrieving {nameof(T)} from database");
        var entity = await dbContext.GetDbSet<T>().FirstOrDefaultAsync(p => p.Id == id);
        if (entity == null)
        {
            throw new NotFoundException($"{nameof(T)} with {id} not found");
        }

        logger.LogInformation($"Successfully retrieved {nameof(T)} from database");
        return entity;
    }

    public async Task<IEnumerable<T>> GetAllAsync(ISpecification<T> specification)
    {
        var allEntities = dbContext.GetDbSet<T>().AsQueryable();
        var queryableResultWithIncludes = specification.Includes
            .Aggregate(allEntities,
                (current, include) => current.Include(include));

        return await queryableResultWithIncludes
            .Where(specification.Criteria)
            .ToListAsync();
    }

    public async Task<bool> IsEntityExistsAsync(Guid id)
    {
        logger.LogInformation($"Started checking if there is {nameof(T)} with {id} in database");
        var response = await dbContext.GetDbSet<T>().AnyAsync(p => p.Id == id);
        logger.LogInformation($"Successfully checked if there is {nameof(T)} with {id} in database");
        return response;
    }
}