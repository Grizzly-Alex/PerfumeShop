namespace PerfumeShop.Infrastructure.DataAccess.InitializeDb;

public sealed class ShoppingDbInitializer : IDbInitializer
{
	private readonly ShoppingDbContext _dbContext;

	public ShoppingDbInitializer(ShoppingDbContext dbContext)
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