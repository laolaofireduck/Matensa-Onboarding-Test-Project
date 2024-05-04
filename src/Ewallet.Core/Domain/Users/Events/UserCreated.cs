using Ewallet.SharedKernel;

namespace Ewallet.Core.Domain.Users.Events;

public record UserCreated(User User) : IDomainEvent;

