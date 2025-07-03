using AlgoTrader.Core.ValueObjects.Base;

namespace AlgoTrader.Core.ValueObjects;

public sealed class TickerId : ValueObject
{
    public Guid Value { get; }

    public TickerId(Guid value)
    {
        Value = value;
    }

    public static TickerId Create(Guid value)
    {
        return new TickerId(value);
    }

    public static implicit operator TickerId(Guid tickerId)
    {
        return new TickerId(tickerId);
    }

    public static implicit operator Guid(TickerId tickerId)
    {
        return tickerId.Value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}