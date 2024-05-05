namespace Ewallet.Endpoints.Contracts.Accounts;

public record AccountResponse(
string FullName,
int Age,
decimal CurrentBalance,
string Email,
string PhoneNumber
);
