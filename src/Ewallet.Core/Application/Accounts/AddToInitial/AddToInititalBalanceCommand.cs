using ErrorOr;
using Ewallet.Core.Domain.Users;
using MediatR;

namespace Ewallet.Core.Application.Accounts.AddToInitial;

public record AddToInititalBalanceCommand(
    UserId UserId,
    decimal Amount
    ) : IRequest<ErrorOr<AccountResult>>;
