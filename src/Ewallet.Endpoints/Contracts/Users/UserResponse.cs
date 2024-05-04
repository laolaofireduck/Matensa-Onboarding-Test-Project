namespace Ewallet.Endpoints.Contracts.Users;

public record UserResponse(
    Guid Id,
    string FullName,
    DateOnly DOB,
    string Email,
    string PhoneNumber);
