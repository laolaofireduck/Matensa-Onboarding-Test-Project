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
        string email) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        DOB = dob;
        Email = email;
    }
    public static User Create(
    string firstName,
    string lastName,
    DateOnly dob,
    string email)
    {
        User user = new(
                    UserId.CreateUnique(),
                    firstName,
                    lastName,
                    dob,
                    email);

        user.AddDomainEvent(new UserCreated(user));

        return user;
    }
#pragma warning disable CS8618
    private User() { }
#pragma warning restore CS8618
    public void SetPassword(string password)
    {
        Password = password;
    }
}
