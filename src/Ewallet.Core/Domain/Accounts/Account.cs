using Ewallet.Core.Domain.Users;
using Ewallet.SharedKernel;

namespace Ewallet.Core.Domain.Accounts;

public class Account : AggregateRoot<AccountId, Guid>
{
    private readonly List<AccountTransaction> _transactions = new();
    private readonly List<AccountStatement> _statements = new();

    public decimal Initial { get; private set; }
    public UserId UserId { get; private set; }

    public IReadOnlyList<AccountTransaction> Transactions => _transactions.AsReadOnly();
    public IReadOnlyList<AccountStatement> Statements => _statements.AsReadOnly();

    private Account(
        AccountId accountId,
        UserId userid,
        decimal initial = 0
        )
      : base(accountId)
    {
        UserId = userid;
        Initial = initial;
    }

    public static Account Create(UserId userid,
        decimal initial = 0
        )
    {
        return new Account(
            AccountId.CreateUnique(),
            userid,
            initial);
    }
#pragma warning disable CS8618
    private Account() { }
#pragma warning restore CS8618

    public decimal GetCurrentBalance()
    {
        // Find the closing balance of the previous month
        decimal previousClosingBalance = GetPreviousMonthClosingBalance(DateTime.Now);

        // Calculate the sum of credit and debit transactions for the current month
        decimal totalCredit = GetTotalCredit(DateTime.Now);
        decimal totalDebit = GetTotalDebit(DateTime.Now);

        // Calculate the current balance
        decimal currentBalance = previousClosingBalance + totalCredit - totalDebit;

        return currentBalance;
    }
    public void AddTransaction(TransactionType type, decimal amount)
    {
        var transaction = AccountTransaction.Create(type, amount);
        _transactions.Add(transaction);
    }
    public void GenerateAccountStatement(DateTime statementDate)
    {
        // Find the closing balance of the previous month
        decimal previousClosingBalance = GetPreviousMonthClosingBalance(statementDate);

        // Calculate the sum of credit and debit transactions for the current month
        decimal totalCredit = GetTotalCredit(statementDate);
        decimal totalDebit = GetTotalDebit(statementDate);

        // Calculate the current balance
        decimal currentBalance = previousClosingBalance + totalCredit - totalDebit;

        // Create a new account statement
        var accountStatement = AccountStatement.Create(statementDate, currentBalance, totalCredit, totalDebit);

        // Save or publish the account statement
        _statements.Add(accountStatement);
    }
    private decimal GetTotalCredit(DateTime statementDate)
    {
        decimal totalCredit = _transactions
            .Where(t => t.Date.Year == statementDate.Year &&
                        t.Date.Month == statementDate.Month)
            .Where(t => t.TransactionType == TransactionType.Deposit || t.TransactionType == TransactionType.Recieve)
            .Sum(t => t.Amount);

        return totalCredit;
    }

    private decimal GetTotalDebit(DateTime statementDate)
    {
        decimal totalDebit = _transactions
            .Where(t => t.Date.Year == statementDate.Year &&
                        t.Date.Month == statementDate.Month)
            .Where(t => t.TransactionType == TransactionType.Withdrawal || t.TransactionType == TransactionType.Transfer)
            .Sum(t => t.Amount);

        return totalDebit;
    }
    private decimal GetPreviousMonthClosingBalance(DateTime statementDate)
    {
        DateTime previousMonthDate = statementDate.AddMonths(-1);

        var previousStatement = _statements
            .OrderByDescending(s => s.Date)
            .FirstOrDefault(s => s.Date.Year == previousMonthDate.Year && s.Date.Month == previousMonthDate.Month);

        if (previousStatement is null && _statements.Count == 0)
            return Initial;

        return previousStatement.ClosingBalance;
    }

    private decimal GetTransactionsSum(DateTime statementDate)
    {
        decimal sum = _transactions
            .Where(t => t.Date.Year == statementDate.Year && t.Date.Month == statementDate.Month)
            .Sum(t => t.Amount);

        return sum;
    }

    internal void AddToInitialBalance(decimal amount)
    {
        Initial += amount;
    }
}
