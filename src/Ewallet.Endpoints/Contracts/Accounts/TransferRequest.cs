using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Ewallet.Endpoints.Contracts.Accounts;

public record TransferRequest
(
    Guid RecieverId,
    decimal Acount);
