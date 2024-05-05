using ErrorOr;
using Ewallet.Core.Application.Users;
using Ewallet.Core.Domain.Users;
using MediatR;

namespace Ewallet.Core.Application.Accounts.AddToInitial;

public class AddToInitialBalanceHandler : IRequestHandler<AddToInititalBalanceCommand, ErrorOr<AccountResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;

    public AddToInitialBalanceHandler(IAccountRepository accountRepository, IUserRepository userRepository)
    {
        _accountRepository = accountRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AccountResult>> Handle(AddToInititalBalanceCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(command.UserId);
        if (user is null) return Errors.User.NotFound(command.UserId);

        var account =await _accountRepository.GetByUserId(command.UserId);
        if (account is null) return Error.Unexpected();

        account.AddToInitialBalance(command.Amount);
        await _accountRepository.Update(account);


        return new AccountResult(
            user.FullName,
            user.Age,
            account.GetCurrentBalance(),
            user.Email,
            user.PhoneNumber
            );
    }
}
