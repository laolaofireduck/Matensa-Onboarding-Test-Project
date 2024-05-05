using System.ComponentModel.DataAnnotations;

namespace Ewallet.Endpoints.Contracts.Authentication;

public record LoginRequest(
 [EmailAddress]string Email,
 string Password);
