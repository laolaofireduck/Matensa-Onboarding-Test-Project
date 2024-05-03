namespace Ewallet.SharedKernel;

public abstract class AggregateRoot<TId, TIdType> : Entity<TId>, IHasDomainEvents
    where TId : AggregateRootId<TIdType>
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public new AggregateRootId<TIdType> Id { get; protected set; }

    protected AggregateRoot(TId id)
    {
        Id = id;
    }

    protected AggregateRoot()
    {
    }
    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}


