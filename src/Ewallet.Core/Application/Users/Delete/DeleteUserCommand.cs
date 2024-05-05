using ErrorOr;
using MediatR;

namespace Ewallet.Core.Application.Users.Delete;

public record DeleteUserCommand(
    Guid Id) : IRequest<ErrorOr<Success>>;
