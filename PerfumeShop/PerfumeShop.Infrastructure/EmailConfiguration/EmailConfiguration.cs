namespace PerfumeShop.Infrastructure.EmailConfiguration;

public static class EmailConfiguration
{
    public static IServiceCollection AddEmailSettings(this IServiceCollection services, IConfiguration configuration)
        => services
            .Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)))
            .AddSingleton<EmailSettings>();   
}
