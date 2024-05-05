using Ewallet.Core.Application.Accounts;
using Ewallet.Core.Domain.Accounts;

namespace Ewallet.Core.Infrastructure.Repositories;

public class AccountRepository : RepositoryBase<Account, AccountId, Guid>,IAccountRepository
{
    public AccountRepository(EwalletDbContext context) : base(context)
    {
    }
}
