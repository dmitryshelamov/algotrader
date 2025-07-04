using AlgoTrader.Core.Entities.Orders.Enums;
using AlgoTrader.Core.ValueObjects.Bots;
using AlgoTrader.Core.ValueObjects.Orders;

namespace AlgoTrader.Core.Entities.Orders;

public sealed class Order
{
    public OrderId Id { get; set; }
    public OrderDirection Direction { get; }
    public OrderType Type { get; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; }
    public CreatedBy CreatedBy { get; }
    public DateTime? ModifiedAt { get; set; }
    public InitialOrderData InitialData { get; }
    public ExchangeData? ExchangeData { get; set; }
    public decimal FeePercentage { get; }
    public BotId BotId { get; internal set; }

    public bool IsOrderExecuted => Status == OrderStatus.PartiallyFilled ||
        Status == OrderStatus.PartiallyCanceled ||
        Status == OrderStatus.Filled;

    public bool InProgress => Status == OrderStatus.PartiallyFilled ||
        Status == OrderStatus.Draft ||
        Status == OrderStatus.New ||
        Status == OrderStatus.PendingCancel;

    public bool IsLimitSell => Type == OrderType.Limit && Direction == OrderDirection.Sell;
    public bool IsLimitBuy => Type == OrderType.Limit && Direction == OrderDirection.Buy;
    public bool IsMarketBuy => Type == OrderType.Market && Direction == OrderDirection.Buy;

    private decimal CommissionRate => FeePercentage > 0 ? decimal.Round(FeePercentage / 100, GlobalSettings.Round) : 0;
    public decimal FeeValue => ExchangeData != null ? decimal.Round(ExchangeData.Funds * CommissionRate, GlobalSettings.Round) : 0;

    internal Order(
        OrderId id,
        OrderDirection direction,
        OrderType type,
        OrderStatus status,
        DateTime createdAt,
        DateTime? modifiedAt,
        InitialOrderData initialData,
        ExchangeData? exchangeData,
        decimal feePercentage,
        BotId botId,
        CreatedBy createdBy)
    {
        Id = id;
        Direction = direction;
        Type = type;
        Status = status;
        CreatedAt = createdAt;
        ModifiedAt = modifiedAt;
        InitialData = initialData;
        ExchangeData = exchangeData;
        FeePercentage = feePercentage;
        BotId = botId;
        CreatedBy = createdBy;
    }

    private Order(CreatedBy createdBy)
    {
        CreatedBy = createdBy;
    }

    public static Order CreateMarketBuy(
        OrderId orderId,
        decimal fundsLimit,
        decimal feePercentage,
        BotId botId,
        DateTime createdAt,
        CreatedBy createdBy)
    {
        var initialData = InitialOrderData.CreateMarketBuy(fundsLimit);
        var order = new Order(
            orderId,
            OrderDirection.Buy,
            OrderType.Market,
            OrderStatus.Draft,
            createdAt,
            modifiedAt: null,
            initialData,
            exchangeData: null,
            feePercentage,
            botId,
            createdBy);

        return order;
    }

    public static Order CreateLimitBuy(
        OrderId orderId,
        decimal limitQuantity,
        decimal limitPricePerAsset,
        decimal feePercentage,
        BotId botId,
        DateTime createdAt,
        CreatedBy createdBy)
    {
        var initialData = InitialOrderData.CreateLimit(limitQuantity, limitPricePerAsset);
        var order = new Order(
            orderId,
            OrderDirection.Buy,
            OrderType.Limit,
            OrderStatus.Draft,
            createdAt,
            modifiedAt: null,
            initialData,
            exchangeData: null,
            feePercentage,
            botId,
            createdBy);

        return order;
    }

    public static Order CreateLimitSell(
        OrderId orderId,
        decimal limitQuantity,
        decimal limitPricePerAsset,
        decimal feePercentage,
        BotId botId,
        DateTime createdAt,
        CreatedBy createdBy)
    {
        var initialData = InitialOrderData.CreateLimit(limitQuantity, limitPricePerAsset);
        var order = new Order(
            orderId,
            OrderDirection.Sell,
            OrderType.Limit,
            OrderStatus.Draft,
            createdAt,
            modifiedAt: null,
            initialData,
            exchangeData: null,
            feePercentage,
            botId,
            createdBy);

        return order;
    }

    public void Update(OrderUpdateEvent orderUpdate)
    {
        if (!Equals(ExchangeData, orderUpdate.ExchangeData) || Status != orderUpdate.OrderStatus)
        {
            ExchangeData = orderUpdate.ExchangeData;
            Status = orderUpdate.OrderStatus;
            ModifiedAt = orderUpdate.CreatedAt;
        }
    }

    public void SetPendingCancel()
    {
        Status = OrderStatus.PendingCancel;
    }
}