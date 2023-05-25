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

		if (! await _dbContext.PhysicalShops.AnyAsync())
		{
			await _dbContext.AddRangeAsync(GetPhysicalShops());
			await _dbContext.SaveChangesAsync();
		}		
	}

	private IEnumerable<PhysicalShop> GetPhysicalShops() 
		=> new List<PhysicalShop>
		{
			new (new ("Germany", "Berlin", "Rosenthaler Str. 35", "10115"), new TimeOnly(08,00,00), new TimeOnly(18,00,00)),
			new (new ("Germany", "Hamburg", "Ludwig Str. 12", "20535"), new TimeOnly(09,00,00), new TimeOnly(20,00,00)),
			new (new ("Germany", "Dresden", "Wiener Str. 21", "1099"), new TimeOnly(08,00,00), new TimeOnly(18,00,00)),
		};	
}