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

        string demoAdminName = "admin@demo.com";
        var demoAdmin = new AppUser { UserName = demoAdminName, Email = demoAdminName };
        await userManager.CreateAsync(demoAdmin, AuthorizationConstants.DefaultPassword);
        demoAdmin = await userManager.FindByNameAsync(demoAdminName);
        await userManager.AddToRoleAsync(demoAdmin, Role.Admin.GetDisplayName());

        string demoEmployeeName = "employee@demo.com";
        var demoEmployee = new AppUser { UserName = demoEmployeeName, Email = demoEmployeeName };
        await userManager.CreateAsync(demoEmployee, AuthorizationConstants.DefaultPassword);
        demoEmployee = await userManager.FindByNameAsync(demoEmployeeName);
        await userManager.AddToRoleAsync(demoEmployee, Role.Employee.GetDisplayName());

        string demoCustomerName = "customer@demo.com";
        var demoCustomer = new AppUser { UserName = demoCustomerName, Email = demoCustomerName };
        await userManager.CreateAsync(demoCustomer, AuthorizationConstants.DefaultPassword);
        demoCustomer = await userManager.FindByNameAsync(demoCustomerName);
        await userManager.AddToRoleAsync(demoCustomer, Role.Customer.GetDisplayName());
    }
}
