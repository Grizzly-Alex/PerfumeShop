namespace PerfumeShop.Infrastructure.DataAccess.InitializeDb;

public sealed class IdentityDbInitializer : IDbInitializer
{
	private readonly UserManager<AppUser> _userManager;
	private readonly RoleManager<AppRole> _roleManager;
	private readonly IdentityAppDbContext _dbContext;

	public IdentityDbInitializer(
			UserManager<AppUser> userManager,
			RoleManager<AppRole> roleManager,
			IdentityAppDbContext dbContext)
	{
		_roleManager = roleManager;
		_userManager = userManager;
		_dbContext = dbContext;
	}

	public async Task Initialize()
	{
		if (_dbContext.Database.IsSqlServer() && _dbContext.Database.GetPendingMigrations().Any())
		{
			await _dbContext.Database.MigrateAsync();
		}

		if (!await _dbContext.Roles.AnyAsync())
		{
			await _roleManager.CreateAsync(new AppRole(Roles.Admin.GetDisplayName()));
			await _roleManager.CreateAsync(new AppRole(Roles.Employee.GetDisplayName()));
			await _roleManager.CreateAsync(new AppRole(Roles.Customer.GetDisplayName()));
		}

		if (!await _dbContext.Users.AnyAsync())
		{
			string demoAdminName = "admin@demo.com";
			var demoAdmin = new AppUser
			{
				UserName = demoAdminName,
				Email = demoAdminName,
				FirstName = "Alexander",
				LastName = "Medved",
				State = "Germany",
				City = "Berlin",
				StreetAddress = "Rosenthaler Str. 12 - 45",
				PhoneNumber = "43098605010",
				PostalCode = "10115",
			};
			await _userManager.CreateAsync(demoAdmin, AuthorizationConstants.DefaultPassword);
			demoAdmin = await _userManager.FindByNameAsync(demoAdminName);
			await _userManager.AddToRoleAsync(demoAdmin, Roles.Admin.GetDisplayName());

			string demoEmployeeName = "employee@demo.com";
			var demoEmployee = new AppUser
			{
				UserName = demoEmployeeName,
				Email = demoEmployeeName,
				FirstName = "Valeria",
				LastName = "Medved",
				State = "Germany",
				City = "Dresden",
				StreetAddress = "Wiener Str. 84 - 11",
				PhoneNumber = "43092367744",
				PostalCode = "1099",
			};
			await _userManager.CreateAsync(demoEmployee, AuthorizationConstants.DefaultPassword);
			demoEmployee = await _userManager.FindByNameAsync(demoEmployeeName);
			await _userManager.AddToRoleAsync(demoEmployee, Roles.Employee.GetDisplayName());

			string demoCustomerName = "customer@demo.com";
			var demoCustomer = new AppUser
			{
				UserName = demoCustomerName,
				Email = demoCustomerName,
				FirstName = "Sergey",
				LastName = "Medved",
				State = "Germany",
				City = "Hamburg",
				StreetAddress = "Ludwig Str. 9 - 32",
				PhoneNumber = "43093344561",
				PostalCode = "20535",
			};
			await _userManager.CreateAsync(demoCustomer, AuthorizationConstants.DefaultPassword);
			demoCustomer = await _userManager.FindByNameAsync(demoCustomerName);
			await _userManager.AddToRoleAsync(demoCustomer, Roles.Customer.GetDisplayName());
		}		
	}
}