using Ewallet.Core.Domain.Accounts;
using Ewallet.Core.Domain.Users;
using Ewallet.SharedKernel;

namespace Ewallet.Core.Application.Accounts;

public interface IAccountRepository : IRepository<Account, AccountId, Guid>
{
    Task<Account?> GetByUserId(UserId id);
}
