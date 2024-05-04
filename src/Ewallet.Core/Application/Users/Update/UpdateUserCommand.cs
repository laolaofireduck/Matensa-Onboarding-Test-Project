using ErrorOr;
using Ewallet.Core.Domain.Users;
using MediatR;

namespace Ewallet.Core.Application.Users.Update;

public record UpdateUserCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string PhoneNumber,
    DateOnly DOB,
    string Email,
    string Password) : IRequest<ErrorOr<UserResult>>;
