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
        await roleManager.CreateAsync(new IdentityRole(Role.User.GetDisplayName()));

        string defaultAdminName = "admin@default.com";
        var defaultAdmin = new AppUser { UserName = defaultAdminName, Email = defaultAdminName };
        await userManager.CreateAsync(defaultAdmin, AuthorizationConstants.DefaultPassword);
        defaultAdmin = await userManager.FindByNameAsync(defaultAdminName);
        await userManager.AddToRoleAsync(defaultAdmin, Role.Admin.GetDisplayName());

        string defaultUserName = "user@default.com";
        var defaultUser = new AppUser { UserName = defaultUserName, Email = defaultUserName };
        await userManager.CreateAsync(defaultUser, AuthorizationConstants.DefaultPassword);
        defaultUser = await userManager.FindByNameAsync(defaultUserName);
        await userManager.AddToRoleAsync(defaultUser, Role.User.GetDisplayName());
    }
}
