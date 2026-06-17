namespace KrediHesaplama.Api.Features.Loans.CalculateLoan;

public interface ICalculateLoanHandler
{
    CalculateLoanResponse Handle(CalculateLoanRequest request);
}

public class CalculateLoanHandler : ICalculateLoanHandler
{
    public CalculateLoanResponse Handle(CalculateLoanRequest request)
    {
        if (request.Amount <= 0 || request.TermInMonths <= 0 || request.MonthlyInterestRate <= 0)
        {
            throw new ArgumentException("Lütfen geçerli değerler giriniz.");
        }

        decimal monthlyInterestRate = request.MonthlyInterestRate / 100;
        decimal taxFactor = 1 + request.BsmvRate + request.KkdfRate;
        decimal effectiveMonthlyInterest = monthlyInterestRate * taxFactor;

        double i = (double)effectiveMonthlyInterest;
        double n = request.TermInMonths;
        double p = (double)request.Amount;

        double installmentAmount;
        if (i == 0)
        {
            installmentAmount = p / n;
        }
        else
        {
            installmentAmount = p * (i * Math.Pow(1 + i, n)) / (Math.Pow(1 + i, n) - 1);
        }

        var schedule = new List<InstallmentDetail>();
        decimal remainingPrincipal = request.Amount;
        decimal totalInterest = 0;
        decimal totalTax = 0;

        for (int m = 1; m <= request.TermInMonths; m++)
        {
            decimal interestAmount = remainingPrincipal * monthlyInterestRate;
            decimal bsmvAmount = interestAmount * request.BsmvRate;
            decimal kkdfAmount = interestAmount * request.KkdfRate;
            decimal totalMonthlyInterestWithTax = interestAmount + bsmvAmount + kkdfAmount;

            decimal principalAmount = (decimal)installmentAmount - totalMonthlyInterestWithTax;

            if (m == request.TermInMonths)
            {
                principalAmount = remainingPrincipal;
            }

            remainingPrincipal -= principalAmount;

            schedule.Add(new InstallmentDetail(
                m,
                Math.Round((decimal)installmentAmount, 2),
                Math.Round(principalAmount, 2),
                Math.Round(interestAmount, 2),
                Math.Round(bsmvAmount, 2),
                Math.Round(kkdfAmount, 2),
                Math.Max(0, Math.Round(remainingPrincipal, 2))
            ));

            totalInterest += interestAmount;
            totalTax += (bsmvAmount + kkdfAmount);
        }

        return new CalculateLoanResponse(
            Math.Round((decimal)installmentAmount, 2),
            Math.Round(schedule.Sum(x => x.PaymentAmount), 2),
            Math.Round(totalInterest + totalTax, 2),
            Math.Round(((decimal)Math.Pow(1 + i, 12) - 1) * 100, 2),
            schedule
        );
    }
}
