using System.ComponentModel.DataAnnotations;

namespace Ewallet.Endpoints.Contracts.Users;

public record UpdateUserRequest(
    [Required] string FirstName,
    [Required] string LastName,
    [Required][Phone] string PhoneNumber,
    [Required] DateOnly DOB,
    [Required][EmailAddress] string Email,
    [Required] string Password
    );
