using ErrorOr;
using MediatR;

namespace Ewallet.Core.Application.Authentication.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;
