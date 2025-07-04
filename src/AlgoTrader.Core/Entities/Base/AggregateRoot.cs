using AlgoTrader.Core.Events.Base;

namespace AlgoTrader.Core.Entities.Base;

public abstract class AggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;
    public long Version { get; protected set; }

    protected void AddEvent(IDomainEvent @event)
    {
        if (!_domainEvents.Any())
        {
            Version++;
        }

        _domainEvents.Add(@event);
    }

    public virtual void ClearEvents()
    {
        _domainEvents.Clear();
    }
}