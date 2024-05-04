using ErrorOr;
using Ewallet.Core.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace Ewallet.Core.Application.Users.Create;

public class CreateUserHandler :
    IRequestHandler<CreateUserCommand, ErrorOr<UserResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _hasher;

    public CreateUserHandler(
        IUserRepository userRepository,
        IPasswordHasher<User> hasher)
    {
        _userRepository = userRepository;
        _hasher = hasher;
    }
    public async Task<ErrorOr<UserResult>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(command.Email) is not null)
            return Errors.User.DuplicateEmail;

        var user = User.Create(
            command.FirstName,
            command.LastName,
            command.DOB,
            command.PhoneNumber,
            command.Email);
        var hashedPass = _hasher.HashPassword(user,command.Password);
        user.SetPassword(hashedPass);

        await _userRepository.Add(user);

        return new UserResult(
            Id: UserId.Create(user.Id.Value),
            FullName: user.FullName,
            DOB: user.DOB,
            Email: user.Email,
            PhoneNumber: user.PhoneNumber
            );
    }
}
