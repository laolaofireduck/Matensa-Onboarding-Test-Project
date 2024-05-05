using Ewallet.SharedKernel;

namespace Ewallet.Core.Domain.Accounts;

public class AccountStatement : Entity<Guid>
{
    public DateTime Date { get; private set; } // 1st day of next month
    public decimal ClosingBalance { get; private set; } // at 1st day of next month
    public decimal TotalCredit { get; private set; } // for closed month
    public decimal TotalDebit { get; private set; } // for closed month

    private AccountStatement(
        Guid AccountStatmentId,
        DateTime date,
        decimal closingBalance,
        decimal totalCredit,
        decimal totalDebit) : base(AccountStatmentId)
    {
        Date = date;
        ClosingBalance = closingBalance;
        TotalCredit = totalCredit;
        TotalDebit = totalDebit;
    }
#pragma warning disable CS8618
    private AccountStatement() { }
#pragma warning restore CS8618
    public static AccountStatement Create(
        DateTime date,
        decimal closingBalance,
        decimal totalCredit,
        decimal totalDebit)
    {
        return new(
            Guid.NewGuid(),
            date,
            closingBalance,
            totalCredit,
            totalDebit
            );
    }

}
