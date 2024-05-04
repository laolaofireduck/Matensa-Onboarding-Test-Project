using Ewallet.Core.Domain.Users;

namespace Ewallet.Core.Application.Users;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
}
