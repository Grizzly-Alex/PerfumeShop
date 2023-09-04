namespace PerfumeShop.Core.Interfaces;

public interface IUnitOfWork<TDbContext>
    where TDbContext : DbContext
{
    Task SaveChangesAsync();
    IRepository<TDbContext, TEntity> GetRepository<TEntity>()
        where TEntity : Entity;
}
