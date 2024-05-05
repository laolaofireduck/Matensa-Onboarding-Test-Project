using Ewallet.Core.Domain.Accounts;
using Ewallet.Core.Domain.Users.Events;
using Ewallet.SharedKernel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ewallet.Core.Domain.Users;

public class User : AggregateRoot<UserId, Guid>, ISoftDeletable
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateOnly DOB { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public bool IsDeleted { get; private set; }

    #region Calculated props
    [NotMapped]
    public string FullName
        => $"{FirstName} {LastName}";
    [NotMapped]
    public int Age =>
         DateTime.Now.Year - DOB.Year;
    #endregion

    private User(
        UserId id,
        string firstName,
        string lastName,
        DateOnly dob,
        string phoneNumber,
        string email) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        DOB = dob;
        PhoneNumber = phoneNumber;
        Email = email;
    }
    public static User Create(
    string firstName,
    string lastName,
    DateOnly dob,
    string phoneNumber,
    string email)
    {
        User user = new(
                    UserId.CreateUnique(),
                    firstName,
                    lastName,
                    dob,
                    phoneNumber,
                    email);

        user.AddDomainEvent(new UserCreated(user));

        return user;
    }
#pragma warning disable CS8618
    private User() { }
#pragma warning restore CS8618

    #region setters
    public void SetPassword(string password)
    {
        Password = password;
    }
    public void SetFirstName(string firstName)
    {
        FirstName = firstName;
    }

    public void SetLastName(string lastName)
    {
        LastName = lastName;
    }

    public void SetDOB(DateOnly dob)
    {
        DOB = dob;
    }

    public void SetPhoneNumber(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }

    public void SetEmail(string email)
    {
        Email = email;
    }
    #endregion

    public void Delete()
    {
        IsDeleted = true;

        // we can publish a domain event on delete
    }
}
