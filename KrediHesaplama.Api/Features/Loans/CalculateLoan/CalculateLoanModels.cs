namespace KrediHesaplama.Api.Features.Loans.CalculateLoan;

public record CalculateLoanRequest(
    decimal Amount,
    int TermInMonths,
    decimal MonthlyInterestRate,
    decimal BsmvRate = 0.15m,
    decimal KkdfRate = 0.15m
);

public record CalculateLoanResponse(
    decimal MonthlyInstallment,
    decimal TotalPayment,
    decimal TotalInterest,
    decimal AnnualEffectiveRate,
    List<InstallmentDetail> Schedule
);

public record InstallmentDetail(
    int InstallmentNumber,
    decimal PaymentAmount,
    decimal Principal,
    decimal Interest,
    decimal Bsmv,
    decimal Kkdf,
    decimal RemainingPrincipal
);
