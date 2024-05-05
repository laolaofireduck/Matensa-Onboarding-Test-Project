using Ewallet.SharedKernel;

namespace Ewallet.Core.Domain.Accounts;

public class AccountTransaction : Entity<Guid>
{
    public DateTime Date { get; private set; }
    public TransactionType TransactionType { get; private set; }
    public decimal Amount { get; private set; }

    private AccountTransaction(
        Guid accountTransactionId,
        DateTime date,
        TransactionType transactionType,
        decimal amount) : base(accountTransactionId)
    {
        Date = date;
        TransactionType = transactionType;
        Amount = amount;
    }
#pragma warning disable CS8618
    private AccountTransaction() { }
#pragma warning restore CS8618
    public static AccountTransaction Create(
        TransactionType transactionType,
        decimal amount
        )
    {
        return new AccountTransaction(
            Guid.NewGuid(),
            DateTime.Now,
            transactionType,
            amount
            );
    }
}
