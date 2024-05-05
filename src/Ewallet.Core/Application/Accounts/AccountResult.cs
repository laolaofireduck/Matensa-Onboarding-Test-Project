namespace Ewallet.Core.Application.Accounts;

public record AccountResult(
    string FullName,
    int Age,
    decimal CurrentBalance,
    string Email,
    string PhoneNumber
    );
