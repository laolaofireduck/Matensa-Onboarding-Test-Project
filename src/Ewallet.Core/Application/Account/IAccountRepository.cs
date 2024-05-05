using Ewallet.Core.Domain.Accounts;
using Ewallet.SharedKernel;

namespace Ewallet.Core.Application.Accounts;

public interface IAccountRepository: IRepository<Account,AccountId,Guid>
{
}
