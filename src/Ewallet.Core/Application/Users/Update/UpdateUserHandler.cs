using ErrorOr;
using Ewallet.Core.Domain.Users;
using Ewallet.Core.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ewallet.Core.Application.Users.Update;

public class UpdateUserHandler :
    IRequestHandler<UpdateUserCommand, ErrorOr<UserResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _hasher;

    public UpdateUserHandler(
        IUserRepository userRepository,
        IPasswordHasher<User> hasher)
    {
        _userRepository = userRepository;
        _hasher = hasher;
    }
    public async Task<ErrorOr<UserResult>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(command.Email) is not null)
            return Errors.User.DuplicateEmail;

        var user = await _userRepository.Get(UserId.Create(command.Id));

        if (user is null)
            return Errors.User.NotFound(command.Id);

        user.SetFirstName(command.FirstName);
        user.SetLastName(command.LastName);
        user.SetDOB(command.DOB);
        user.SetPhoneNumber(command.PhoneNumber);
        user.SetEmail(command.Email);

        await _userRepository.Update(user);

        return new UserResult(
            Id: UserId.Create(user.Id.Value),
            FullName: user.FullName,
            DOB: user.DOB,
            Email: user.Email,
            PhoneNumber: user.PhoneNumber
            );
    }
}

