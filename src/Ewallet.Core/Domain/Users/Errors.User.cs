using ErrorOr;

namespace Ewallet.Core.Domain.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: "User.DuplicateEmail",
            description: "Email is already in use.");

        public static Error NotFound(Guid userId) => Error.NotFound(
            code: "User.NotFound",
            description: $"User with ID {userId} not found.");
    }
}
