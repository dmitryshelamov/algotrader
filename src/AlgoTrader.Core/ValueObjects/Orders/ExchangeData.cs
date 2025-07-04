using AlgoTrader.Core.ValueObjects.Base;

namespace AlgoTrader.Core.ValueObjects.Orders;

public sealed class ExchangeData : ValueObject
{
    public string? ExchangeOrderId { get; internal set; }
    public DateTime? CreatedAt { get; internal set; }
    public DateTime? ModifiedAt { get; internal set; }
    public decimal Quantity { get; internal set; }
    public decimal Funds { get; internal set; }

    public decimal AverageAssetPrice
    {
        get
        {
            if (Quantity > 0 && Funds > 0)
            {
                return decimal.Round(Funds / Quantity, GlobalSettings.Round);
            }

            return 0;
        }
    }

    public ExchangeData(string? exchangeOrderId, DateTime? createdAt, DateTime? modifiedAt, decimal quantity, decimal funds)
    {
        ExchangeOrderId = exchangeOrderId;
        CreatedAt = createdAt;
        ModifiedAt = modifiedAt;
        Quantity = quantity;
        Funds = funds;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Quantity;
        yield return Funds;
        yield return ExchangeOrderId;
        yield return CreatedAt;
        yield return ModifiedAt;
    }

    public override string ToString()
    {
        return
            $"ExchangeOrderId:{ExchangeOrderId ?? string.Empty} - CreatedAt:{CreatedAt} - ModifiedAt:{ModifiedAt} - Quantity:{Quantity} - Funds:{Funds} - AverageAssetPrice:{AverageAssetPrice}";
    }
}