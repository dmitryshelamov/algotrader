using AlgoTrader.Core.Entities.Orders.Enums;

namespace AlgoTrader.Core.Entities.Orders;

public sealed class HistoryOrder
{
    public Guid Id { get; }
    public Guid TradeId { get; }
    public string Ticker { get; }
    public OrderDirection Direction { get; }
    public OrderType Type { get; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; }
    public DateTime? ModifiedAt { get; set; }
    public decimal? InitialLimitQuantity { get; }
    public decimal? InitialLimitPricePerAsset { get; }
    public decimal? InitialMarketQuantity { get; }
    public decimal? InitialMarketFunds { get; }
    public string? ExchangeOrderId { get; }
    public DateTime? ExchangeCreatedAt { get; }
    public DateTime? ExchangeModifiedAt { get; }
    public decimal? ExchangeQuantity { get; }
    public decimal? ExchangeFunds { get; }
    public decimal FeePercentage { get; }
    public Guid BotId { get; }

    public HistoryOrder(
        Guid id,
        Guid tradeId,
        string ticker,
        OrderDirection direction,
        OrderType type,
        OrderStatus status,
        DateTime createdAt,
        DateTime? modifiedAt,
        decimal? initialLimitQuantity,
        decimal? initialLimitPricePerAsset,
        decimal? initialMarketQuantity,
        decimal? initialMarketFunds,
        string? exchangeOrderId,
        DateTime? exchangeCreatedAt,
        DateTime? exchangeModifiedAt,
        decimal? exchangeQuantity,
        decimal? exchangeFunds,
        decimal feePercentage,
        Guid botId)
    {
        Id = id;
        TradeId = tradeId;
        Ticker = ticker;
        Direction = direction;
        Type = type;
        Status = status;
        CreatedAt = createdAt;
        ModifiedAt = modifiedAt;
        InitialLimitQuantity = initialLimitQuantity;
        InitialLimitPricePerAsset = initialLimitPricePerAsset;
        InitialMarketQuantity = initialMarketQuantity;
        InitialMarketFunds = initialMarketFunds;
        ExchangeOrderId = exchangeOrderId;
        ExchangeCreatedAt = exchangeCreatedAt;
        ExchangeModifiedAt = exchangeModifiedAt;
        ExchangeQuantity = exchangeQuantity;
        ExchangeFunds = exchangeFunds;
        FeePercentage = feePercentage;
        BotId = botId;
    }

    public static HistoryOrder Create(Order order, Guid tradeId, string ticker)
    {
        var historyOrder = new HistoryOrder(
            order.Id,
            tradeId,
            ticker,
            order.Direction,
            order.Type,
            order.Status,
            order.CreatedAt,
            order.ModifiedAt,
            order.InitialData.LimitQuantity,
            order.InitialData.LimitPricePerAsset,
            order.InitialData.MarketQuantity,
            order.InitialData.MarketFunds,
            order.ExchangeData?.ExchangeOrderId,
            order.ExchangeData?.CreatedAt,
            order.ExchangeData?.ModifiedAt,
            order.ExchangeData?.Quantity,
            order.ExchangeData?.Funds,
            order.FeePercentage,
            order.BotId);

        return historyOrder;
    }
}