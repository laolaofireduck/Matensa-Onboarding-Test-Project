using Ewallet.SharedKernel;
using System.Security.Cryptography;

namespace Ewallet.Core.Domain.Accounts;

public sealed class AccountId: AggregateRootId<Guid> 
{
    public override Guid Value { get; protected set; }

    private AccountId(Guid value)
    {
        Value = value;
    }

    public static AccountId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static AccountId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Guid(AccountId data)
        => data.Value;
}
