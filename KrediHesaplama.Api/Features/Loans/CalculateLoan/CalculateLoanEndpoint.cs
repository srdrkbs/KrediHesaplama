namespace KrediHesaplama.Api.Features.Loans.CalculateLoan;

public static class CalculateLoanEndpoint
{
    public static void MapCalculateLoanEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/loan/calculate", (CalculateLoanRequest request, ICalculateLoanHandler handler) =>
        {
            var response = handler.Handle(request);
            return Results.Ok(response);
        })
        .WithName("CalculateLoan");
    }
}
