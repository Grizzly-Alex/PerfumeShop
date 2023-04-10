using PerfumeShop.Infrastructure.DataAccess.SeedsDb;
using ILogger = Serilog.ILogger;

namespace PerfumeShop.Web.Configurations;


public static class WebDependencies
{
    public static ILogger SetLogger(IConfiguration configuration, ILoggingBuilder logging)
    {
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        logging.ClearProviders();
        logging.AddSerilog(logger);

        return logger;
    }

    public static async Task SetSeedsAsync(IServiceProvider serviceProvider, ILogger logger)
    {
        logger.Information("Seeding Database...");

        using (var scope = serviceProvider.CreateScope())
        {
            var scopedProvider = scope.ServiceProvider;
            try
            {
                var userManager = scopedProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var identityContext = scopedProvider.GetRequiredService<IdentityAppDbContext>();
                await IdentitySeedDb.SeedAsync(identityContext, userManager, roleManager);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred seeding the DB.");
            }
        }
    }

    public static void SetServices(IServiceCollection services)
    {
        services.AddCookieSettings();
        services.AddAuthenticationSettings();        
        services.AddControllersWithViews();
        services.AddCoreServices();
        services.AddWebServices();       
    }

    public static void SetMiddleware(WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseRequestLocalization("en-US", "en-US");
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapRazorPages();

        app.MapAreaControllerRoute(
            name: "ShopDefault",
            areaName: "Shop",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapAreaControllerRoute(
            name: "AdminDefault",
            areaName: "Admin",
            pattern: "Admin/{controller}/{action}/{id?}");
    }
}
