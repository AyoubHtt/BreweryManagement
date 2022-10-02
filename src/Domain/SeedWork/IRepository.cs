using Microsoft.EntityFrameworkCore;

namespace Domain.SeedWork;

public interface IRepository<T> where T : Entity
{
    IUnitOfWork UnitOfWork { get; }
    DbSet<T> DbSet { get; }

    Task<T?> GetByIdAsync(Guid id);
    Task<bool> CheckEntityExistByIdAsync(Guid id);
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(List<T> entities, CancellationToken cancellationToken);
    void CreateOrUpdate(T entity);
    void Update(T entity);
    void Remove(T entity);
}