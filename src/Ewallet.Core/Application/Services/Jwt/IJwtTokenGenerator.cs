
using Ewallet.Core.Domain.Users;

namespace Ewallet.Core.Application.Services.Jwt;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}