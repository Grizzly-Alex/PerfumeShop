namespace PerfumeShop.Infrastructure.DataAccess.SeedsDb;

public static class IdentitySeedDb
{
    public static async Task SeedAsync(IdentityAppDbContext identityDbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {

        if (identityDbContext.Database.IsSqlServer())
        {
            identityDbContext.Database.Migrate();
        }

        await roleManager.CreateAsync(new IdentityRole(Role.Admin.GetDisplayName()));
        await roleManager.CreateAsync(new IdentityRole(Role.Employee.GetDisplayName()));
        await roleManager.CreateAsync(new IdentityRole(Role.Customer.GetDisplayName()));

        string defaultAdminName = "admin@default.com";
        var defaultAdmin = new AppUser { UserName = defaultAdminName, Email = defaultAdminName };
        await userManager.CreateAsync(defaultAdmin, AuthorizationConstants.DefaultPassword);
        defaultAdmin = await userManager.FindByNameAsync(defaultAdminName);
        await userManager.AddToRoleAsync(defaultAdmin, Role.Admin.GetDisplayName());

        string defaultEmployeeName = "employee@default.com";
        var defaultEmployee = new AppUser { UserName = defaultEmployeeName, Email = defaultEmployeeName };
        await userManager.CreateAsync(defaultEmployee, AuthorizationConstants.DefaultPassword);
        defaultEmployee = await userManager.FindByNameAsync(defaultEmployeeName);
        await userManager.AddToRoleAsync(defaultEmployee, Role.Customer.GetDisplayName());

        string defaultCustomerName = "customer@default.com";
        var defaultCustomer = new AppUser { UserName = defaultCustomerName, Email = defaultCustomerName };
        await userManager.CreateAsync(defaultCustomer, AuthorizationConstants.DefaultPassword);
        defaultCustomer = await userManager.FindByNameAsync(defaultCustomerName);
        await userManager.AddToRoleAsync(defaultCustomer, Role.Customer.GetDisplayName());
    }
}
