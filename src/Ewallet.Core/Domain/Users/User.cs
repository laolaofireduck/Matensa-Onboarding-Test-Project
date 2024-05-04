using Ewallet.Core.Domain.Users.Events;
using Ewallet.SharedKernel;

namespace Ewallet.Core.Domain.Users;

public class User : AggregateRoot<UserId, Guid>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateOnly DOB { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    private User(
        UserId id,
        string firstName,
        string lastName,
        DateOnly dob,
        string email,
        string password) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        DOB = dob;
        Email = email;
        Password = password;
    }
    public static User Create(
    string firstName,
    string lastName,
    DateOnly dob,
    string email,
    string password)
    {
        User user = new(
                    UserId.CreateUnique(),
                    firstName,
                    lastName,
                    dob,
                    email,
                    password);

        user.AddDomainEvent(new UserCreated(user));

        return user;
    }
#pragma warning disable CS8618
    private User() { }
#pragma warning restore CS8618
}
