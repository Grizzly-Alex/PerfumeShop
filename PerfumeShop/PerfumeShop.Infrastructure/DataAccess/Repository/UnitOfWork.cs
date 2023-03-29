namespace PerfumeShop.Infrastructure.DataAccess.Repository;

public sealed class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext>
    where TDbContext : DbContext
{
    private readonly TDbContext _db;

    public UnitOfWork(TDbContext db) => _db = db;

    public async Task SaveChangesAsync() => await _db.SaveChangesAsync();

    public IRepository<TDbContext, TEntity> GetRepository<TEntity>()
        where TEntity : Entity
        => new Repository<TDbContext, TEntity>(_db);
}
