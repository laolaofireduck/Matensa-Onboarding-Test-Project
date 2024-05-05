using ErrorOr;
using Ewallet.Core.Domain.Users;
using MediatR;

namespace Ewallet.Core.Application.Users.List;

public class ListUserHandler : IRequestHandler<ListUserQuery, ErrorOr<IEnumerable<UserResult>>>
{
    private readonly IUserRepository _userRepository;

    public ListUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<IEnumerable<UserResult>>> Handle(ListUserQuery request, CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetAll(request.Skip, request.Take);
        var users = result.Select(u => new UserResult(
             Id: UserId.Create(u.Id.Value),
             FullName: u.FullName,
             DOB: u.DOB,
             Email: u.Email,
             PhoneNumber: u.PhoneNumber
            ));

        return ErrorOrFactory.From(users);
    }
}
