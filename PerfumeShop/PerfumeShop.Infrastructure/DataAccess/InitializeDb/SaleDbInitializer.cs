namespace PerfumeShop.Infrastructure.DataAccess.InitializeDb;

public sealed class SaleDbInitializer : IDbInitializer
{
	private readonly SaleDbContext _dbContext;

	public SaleDbInitializer(SaleDbContext dbContext)
	{
		_dbContext = dbContext;
	}


	public async Task Initialize()
	{
		if (_dbContext.Database.IsSqlServer() && _dbContext.Database.GetPendingMigrations().Any())
		{
			await _dbContext.Database.MigrateAsync();
		}		
	}
}