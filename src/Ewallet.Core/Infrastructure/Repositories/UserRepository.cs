using Ewallet.Core.Application.Users;
using Ewallet.Core.Domain.Users;
using Ewallet.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Ewallet.Core.Infrastructure.Repositories;

public class UserRepository : RepositoryBase<User, UserId, Guid>, IUserRepository
{
    public UserRepository(EwalletDbContext context) : base(context)
    {
    }

    public User? GetUserByEmail(string email)
    {
        return dbSet.SingleOrDefault(u => u.Email == email);
    }

}
