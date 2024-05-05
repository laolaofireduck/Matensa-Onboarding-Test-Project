using ErrorOr;
using Ewallet.Core.Domain.Users;
using MediatR;

namespace Ewallet.Core.Application.Accounts.Transfer;

public record TransferCommand
(
    Guid RecieverId,
    decimal Amount
    ):IRequest<ErrorOr<TransferResult>>;
