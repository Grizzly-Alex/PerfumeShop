namespace PerfumeShop.Infrastructure.Stripe;

public static class StripeInfrastructure
{
    public static IServiceCollection AddStripeInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
		StripeConfiguration.ApiKey = configuration.GetValue<string>("StripeSettings:SecretKey");

        return services
			.Configure<StripeSettings>(configuration.GetSection(nameof(StripeSettings)))
			.AddScoped<CustomerService>()
            .AddScoped<ChargeService>()
            .AddScoped<TokenService>();
    }
}
