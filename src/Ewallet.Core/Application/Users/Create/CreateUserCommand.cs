using MediatR;
using ErrorOr;
namespace Ewallet.Core.Application.Users.Create;

public record CreateUserCommand(
    Guid Id,
    string FirstName,
    string LastName,
    DateOnly DOB,
    string PhoneNumber,
    string Email,
    string Password) : IRequest<ErrorOr<UserResult>>;
