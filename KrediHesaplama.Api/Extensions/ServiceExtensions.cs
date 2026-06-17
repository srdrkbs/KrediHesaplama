using KrediHesaplama.Api.Features.Loans.CalculateLoan;

namespace KrediHesaplama.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAngular",
                policy => policy.WithOrigins("http://localhost:5200", "http://localhost")
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

        services.AddEndpointsApiExplorer();
        services.AddOpenApi();
        
        // Feature Handlers
        services.AddScoped<ICalculateLoanHandler, CalculateLoanHandler>();

        return services;
    }
}
