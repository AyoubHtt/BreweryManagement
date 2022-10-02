namespace Domain.SeedWork;

public interface IRepository<T> where T : Entity
{
    IUnitOfWork UnitOfWork { get; }
    DbSet<T> DbSet { get; }

    void Remove(T entity);
    void Update(T entity);
    void CreateOrUpdate(T entity);
    Task<T> AddAsync(T entity);
    Task<bool> CheckEntityExistByIdAsync(Guid id);
    Task<T?> GetByIdWithNoTrackingAsync(Guid id);
    Task AddRangeAsync(List<T> entities, CancellationToken cancellationToken);
    Task<T?> GetByIdAsync(Guid id);
    void RemoveRange(List<T> entities);
    void UpdateRange(IEnumerable<T> entities);
}