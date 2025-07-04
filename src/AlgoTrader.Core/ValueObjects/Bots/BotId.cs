using AlgoTrader.Core.ValueObjects.Base;

namespace AlgoTrader.Core.ValueObjects.Bots;

public sealed class BotId : ValueObject
{
    public Guid Value { get; }

    public BotId(Guid value)
    {
        Value = value;
    }

    public static BotId Create(Guid userId)
    {
        return new BotId(userId);
    }

    public static implicit operator BotId(Guid value)
    {
        return new BotId(value);
    }

    public static implicit operator Guid(BotId userId)
    {
        return userId.Value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}