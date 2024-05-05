using Ewallet.Core.Domain.Users;

namespace Ewallet.Core.Application.Authentication;

public record AuthenticationResult(
    User User,
    string Token);
