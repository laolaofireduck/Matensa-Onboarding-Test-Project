using System.ComponentModel.DataAnnotations;

namespace Ewallet.Endpoints.Contracts.Users;

public record CreateUserRequest(
    [Required] string FirstName,
    [Required] string LastName,
    [Required] DateOnly DOB,
    [Required][Phone] string PhoneNumber,
    [Required][EmailAddress] string Email,
    [Required] string Password);

