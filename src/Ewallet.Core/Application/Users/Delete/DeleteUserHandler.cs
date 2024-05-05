using ErrorOr;
using Ewallet.Core.Domain.Users;
using Ewallet.Core.Domain.Errors;
using MediatR;

namespace Ewallet.Core.Application.Users.Delete;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, ErrorOr<Success>>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Success>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var aggregateToDelete = await _userRepository.Get(UserId.Create(command.Id));

        if (aggregateToDelete is null)
            return Errors.User.NotFound(command.Id);

        var userid = UserId.Create(aggregateToDelete.Id.Value);
        await _userRepository.Delete(userid);

        return Result.Success;
    }
}
