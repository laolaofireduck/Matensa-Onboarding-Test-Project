using Ewallet.Core.Domain.Users;
using Ewallet.SharedKernel;

namespace Ewallet.Core.Application.Users;

public interface IUserRepository : IRepository<User, UserId, Guid>
{
    User? GetUserByEmail(string email);
}
