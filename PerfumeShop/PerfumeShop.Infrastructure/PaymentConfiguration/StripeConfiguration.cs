namespace PerfumeShop.Infrastructure.PaymentConfiguration;

public static class StripeConfiguration
{
    public static IServiceCollection AddStripeSettings(this IServiceCollection services, IConfiguration configuration)
    {
        Stripe.StripeConfiguration.ApiKey = configuration.GetValue<string>("StripeSettings:SecretKey");

        return services
			.Configure<StripeSettings>(configuration.GetSection(nameof(StripeSettings)))
			.AddScoped<CustomerService>()
            .AddScoped<ChargeService>()
            .AddScoped<TokenService>();
    }
}
