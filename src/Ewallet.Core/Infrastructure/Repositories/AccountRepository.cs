using Ewallet.Core.Application.Accounts;
using Ewallet.Core.Domain.Accounts;
using Ewallet.Core.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Ewallet.Core.Infrastructure.Repositories;

public class AccountRepository : RepositoryBase<Account, AccountId, Guid>,IAccountRepository
{
    public AccountRepository(EwalletDbContext context) : base(context)
    {
    }
    public async Task<Account?> GetByUserId(UserId id)
    {
        return await dbSet.SingleOrDefaultAsync(u => u.UserId == id.Value);
    }
}
