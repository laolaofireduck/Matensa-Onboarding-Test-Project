using Ewallet.Core.Application.Accounts;
using Ewallet.Core.Domain.Accounts;
using Ewallet.Core.Domain.Users;
using Ewallet.Core.Domain.Users.Events;
using MediatR;

namespace Ewallet.Core.Application.Users.Events;

public class UserCreatedHandler(IAccountRepository accountRepository) : INotificationHandler<UserCreated>
{
    private readonly IAccountRepository _accountRepository = accountRepository;

    public async Task Handle(UserCreated notification, CancellationToken cancellationToken)
    {
        var userAccount = Account.Create(
            userid:UserId.Create(notification.User.Id.Value)
            );

        await _accountRepository.Add(userAccount);
    }
}
