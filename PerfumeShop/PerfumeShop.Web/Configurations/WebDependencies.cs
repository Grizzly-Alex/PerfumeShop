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

    public static void SetServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddCoreServices();
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

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    }
}
