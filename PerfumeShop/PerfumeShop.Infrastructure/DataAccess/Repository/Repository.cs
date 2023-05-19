namespace PerfumeShop.Infrastructure.DataAccess.Repository;

public class Repository<TDbContext, TEntity> : IRepository<TDbContext, TEntity>
    where TDbContext : DbContext
    where TEntity : Entity
{
    private readonly TDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(TDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dbSet = _dbContext.Set<TEntity>();
    }

    public void Add(TEntity entity)
    {
        bool tracked = _dbContext.Entry(entity).State != EntityState.Detached;

        _dbSet.Add(entity).State = EntityState.Added;
    }

	public void Add(IEnumerable<TEntity> entityCollection)
	{
		_dbSet.AddRange(entityCollection);
	}

	public void Remove(TEntity entity)
    {
		_dbSet.Remove(entity).State = EntityState.Deleted;
    }

	public void Remove(IEnumerable<TEntity> entityCollection)
    {
        _dbSet.RemoveRange(entityCollection);
    }

	public void Update(TEntity entity)
    {
        _dbSet.Update(entity).State = EntityState.Modified;
    }

	public void Update(IEnumerable<TEntity> entityCollection)
	{
		_dbSet.UpdateRange(entityCollection);
	}

	#region GetAll
	public async Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool isTracking = false)
    {
        IQueryable<TEntity> query = _dbSet;

        if (!isTracking) { query = query.AsNoTracking(); }
        if (include is not null) { query = include(query); }
        if (predicate is not null) { query = query.Where(predicate); }

        return orderBy is not null
            ? await orderBy(query).ToListAsync()
            : await query.ToListAsync();
    }

    public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool isTracking = false)
    {
        IQueryable<TEntity> query = _dbSet;

        if (!isTracking) { query = query.AsNoTracking(); }
        if (predicate is not null) { query = query.Where(predicate); }
        if (include is not null) { query = include(query); }

        return orderBy is not null
            ? await orderBy(query).Select(selector).ToListAsync()
            : await query.Select(selector).ToListAsync();
    }
    #endregion

    #region GetFirstOrDefault
    public async Task<TEntity?> GetFirstOrDefaultAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool isTracking = false)
    {
        IQueryable<TEntity> query = _dbSet;

        if (!isTracking) { query = query.AsNoTracking(); }
        if (predicate is not null) { query = query.Where(predicate); }
        if (include is not null) { query = include(query); }

        return orderBy is not null
            ? await orderBy(query).FirstOrDefaultAsync()
            : await query.FirstOrDefaultAsync();
    }

    public async Task<TResult?> GetFirstOrDefaultAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool isTracking = false)
    {
        IQueryable<TEntity> query = _dbSet;

        if (!isTracking) { query = query.AsNoTracking(); }
        if (predicate is not null) { query = query.Where(predicate); }
        if (include is not null) { query = include(query); }

        return orderBy is not null
            ? await orderBy(query).Select(selector).FirstOrDefaultAsync()
            : await query.Select(selector).FirstOrDefaultAsync();
    }
    #endregion

    #region GetPagetList
    public async Task<IPagedList<TResult>> GetPagedListAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int pageIndex = default,
        int itemsPerPage = default,
        bool isTracking = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _dbSet;

        if (!isTracking) { query = query.AsNoTracking(); }
        if (predicate is not null) { query = query.Where(predicate); }
        if (include is not null) { query = include(query); }

        return orderBy is not null
            ? await orderBy(query).Select(selector).ToPagedListAsync(pageIndex, itemsPerPage, cancellationToken)
            : await query.Select(selector).ToPagedListAsync(pageIndex, itemsPerPage, cancellationToken);
    }
    #endregion

    #region Count
    public async Task<int> CountAsync(
    Expression<Func<TEntity, bool>>? predicate = null,
    CancellationToken cancellationToken = default) =>
    predicate is null
        ? await _dbSet.CountAsync(cancellationToken)
        : await _dbSet.CountAsync(predicate, cancellationToken);

    public async Task<int> CountAsync<TResult>(
        Expression<Func<TResult, int>> sum,
        Expression<Func<TEntity, IEnumerable<TResult>>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default) =>
        predicate is null
            ? await _dbSet.SelectMany(selector).SumAsync(sum)
            : await _dbSet.Where(predicate).SelectMany(selector).SumAsync(sum);
    #endregion

    #region Aggregations
    public async Task<TResult> MaxAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default) =>
        predicate is null
            ? await _dbSet.MaxAsync(selector, cancellationToken)
            : await _dbSet.Where(predicate).MaxAsync(selector, cancellationToken);

    public async Task<TResult> MinAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default) =>
        predicate is null
            ? await _dbSet.MinAsync(selector, cancellationToken)
            : await _dbSet.Where(predicate).MinAsync(selector, cancellationToken);

    public Task<int> SumAsync(
        Expression<Func<TEntity, int>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default) =>
        predicate is null
            ? _dbSet.SumAsync(selector, cancellationToken)
            : _dbSet.Where(predicate).SumAsync(selector, cancellationToken);
    #endregion
}
