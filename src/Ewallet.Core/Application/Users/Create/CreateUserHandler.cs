using ErrorOr;
using Ewallet.Core.Domain.Users;
using MediatR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ewallet.Core.Application.Users.Create;

public class CreateUserHandler :
    IRequestHandler<CreateUserCommand, ErrorOr<Success>>
{
        private readonly IUserRepository _userRepository;
    public CreateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<ErrorOr<Success>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(command.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        var user = User.Create(
            command.FirstName,
            command.LastName,
            command.DOB,
            command.Email,
            command.Password);

        _userRepository.Add(user);
        return Result.Success;
    }
}
