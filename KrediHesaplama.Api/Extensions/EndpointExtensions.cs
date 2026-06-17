using KrediHesaplama.Api.Features.Loans.CalculateLoan;

namespace KrediHesaplama.Api.Extensions;

public static class EndpointExtensions
{
    public static void MapApplicationEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/loan/test", () => Results.Ok("API is running and accessible."));
        
        // Feature Endpoints
        app.MapCalculateLoanEndpoint();
    }
}
