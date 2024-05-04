using MediatR;
using ErrorOr;
namespace Ewallet.Core.Application.Users.Create;

public record CreateUserCommand(
    string FirstName,
    string LastName,
    DateOnly DOB,
    string Email,
    string Password) : IRequest<ErrorOr<Success>>;
