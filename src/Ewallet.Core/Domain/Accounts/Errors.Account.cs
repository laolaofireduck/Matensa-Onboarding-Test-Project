using ErrorOr;

namespace Ewallet.Core.Domain.Errors;

public static partial class Errors
{
    public static class Account
    {
        public static Error InsufficientBalance => Error.Conflict(
             code: "User.InsufficientBalance",
        description: "Insufficient balance to perform this action.");


    }
}
