using System.Collections;

namespace Domain.Interfaces;

public interface IRepository<T>
{
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync(ISpecification<T> specification);
    Task<bool> IsEntityExistsAsync(Guid id);
}