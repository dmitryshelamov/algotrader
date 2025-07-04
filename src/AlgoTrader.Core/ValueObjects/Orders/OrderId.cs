using AlgoTrader.Core.ValueObjects.Base;

namespace AlgoTrader.Core.ValueObjects.Orders;

public sealed class OrderId : ValueObject
{
    public Guid Value { get; internal set; }

    public OrderId(Guid value)
    {
        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator OrderId(Guid value)
    {
        return new OrderId(value);
    }

    public static implicit operator Guid(OrderId orderId)
    {
        return orderId.Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}