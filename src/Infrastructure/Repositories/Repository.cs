namespace Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : Entity
{
    protected readonly BreweryContext _breweryContext;
    public DbSet<T> DbSet { get; }
    public IUnitOfWork UnitOfWork { get { return _breweryContext; } }

    public Repository(BreweryContext breweryContext)
    {
        _breweryContext = breweryContext ?? throw new ArgumentNullException(nameof(breweryContext));
        DbSet = _breweryContext.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken) => await DbSet.FindAsync(new object?[] { id }, cancellationToken);

    public async Task<bool> CheckEntityExistByIdAsync(Guid id) => await DbSet.AnyAsync(entity => entity.Id == id);

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken) => (await DbSet.AddAsync(entity, cancellationToken)).Entity;

    public async Task AddRangeAsync(List<T> entities, CancellationToken cancellationToken) => await DbSet.AddRangeAsync(entities, cancellationToken);

    public void CreateOrUpdate(T entity) => DbSet.Update(entity);

    public void Update(T entity) => _breweryContext.Entry(entity).State = EntityState.Modified;

    public void Remove(T entity) => _breweryContext.Entry(entity).State = EntityState.Deleted;
}
