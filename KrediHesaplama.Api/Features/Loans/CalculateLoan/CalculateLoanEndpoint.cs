namespace KrediHesaplama.Api.Features.Loans.CalculateLoan;

public static class CalculateLoanEndpoint
{
    public static void MapCalculateLoanEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/loan/calculate", (CalculateLoanRequest request, ICalculateLoanHandler handler) =>
        {
            try
            {
                var response = handler.Handle(request);
                return Results.Ok(response);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Results.Problem("Hesaplama sırasında teknik bir hata oluştu: " + ex.Message);
            }
        })
        .WithName("CalculateLoan");
    }
}
