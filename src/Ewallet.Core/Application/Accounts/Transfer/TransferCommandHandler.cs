using ErrorOr;
using Ewallet.Core.Application.Users;
using Ewallet.Core.Domain.Users;
using Ewallet.Core.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Ewallet.Core.Application.Accounts.Transfer;

internal class TransferCommandHandler : IRequestHandler<TransferCommand, ErrorOr<TransferResult>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;

    public TransferCommandHandler(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<TransferResult>> Handle(TransferCommand command, CancellationToken cancellationToken)
    {
        var userPrincipal = _httpContextAccessor.HttpContext.User;

        // Retrieve the user ID from the user principal's claims
        var senderId = Guid.Parse( userPrincipal.FindFirst("userid")?.Value);

        UserId uid = UserId.Create(senderId);
        var sender = await _userRepository.Get(uid);
        if (sender == null) return Errors.User.NotFound(senderId);

        var recieverId = UserId.Create(command.RecieverId);
        var reciever = await _userRepository.Get(recieverId);
        if (reciever == null) return Errors.User.NotFound(command.RecieverId);


        var senderAccount = await _accountRepository.GetByUserId(uid);
        var recieverAccount = await _accountRepository.GetByUserId(recieverId);
        if(senderAccount is null || recieverAccount is null ) return Error.Unexpected();

        if (senderAccount.GetCurrentBalance() < command.Amount)
            return Errors.Account.InsufficientBalance;

        senderAccount.AddTransaction(Domain.Accounts.TransactionType.Transfer, command.Amount);
        recieverAccount.AddTransaction(Domain.Accounts.TransactionType.Recieve, command.Amount);

        await _accountRepository.Update(senderAccount);
        await _accountRepository.Update(recieverAccount);

        return new TransferResult(senderAccount.GetCurrentBalance());
    }
}
