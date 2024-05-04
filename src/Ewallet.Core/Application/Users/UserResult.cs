using Ewallet.Core.Domain.Users;

namespace Ewallet.Core.Application.Users;

public record UserResult(
    UserId Id,
    string FullName,
    DateOnly DOB,
    string Email,
    string PhoneNumber
    );

