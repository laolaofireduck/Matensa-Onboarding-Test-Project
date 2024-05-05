using ErrorOr;
using Ewallet.Core.Application.Services.Jwt;
using Ewallet.Core.Application.Users;
using Ewallet.Core.Domain.Users;
using Ewallet.Core.Domain.Errors;

using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ewallet.Core.Application.Authentication.Login;


public class LoginQueryHandler :
    IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _hasher;


    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, IPasswordHasher<User> hasher)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _hasher = hasher;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_userRepository.GetUserByEmail(request.Email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        if (_hasher.VerifyHashedPassword(user,user.Password, request.Password) == PasswordVerificationResult.Failed)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}