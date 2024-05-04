using System.ComponentModel.DataAnnotations;

namespace Ewallet.Endpoints.Contracts.Users;

public record UpdateUserRequest(
    [Required] string FirstName,
    [Required] string LastName,
    [Required] string PhoneNumber,
    [Required] DateOnly DOB,
    [Required] string Email,
    [Required] string Password
    );
