using AlgoTrader.Core.Entities;
using AlgoTrader.Core.Entities.Bots;
using AlgoTrader.Core.Entities.Orders;
using AlgoTrader.Core.Entities.Orders.Enums;
using AlgoTrader.Core.Events.Base;
using AlgoTrader.Core.ValueObjects.Bots;
using AlgoTrader.Core.ValueObjects.Orders;

namespace AlgoTrader.Core.Events.Orders.Requests;

public sealed class OrderCreateLimitSellRequestEvent : IDomainEvent
{
    public OrderId OrderId { get; set; }
    public BotId BotId { get; set; }
    public decimal LimitPricePerAsset { get; set; }
    public decimal LimitQuantity { get; set; }
    public Ticker Ticker { get; set; }
    public OrderDirection OrderDirection { get; set; }
    public OrderType OrderType { get; set; }

    public OrderCreateLimitSellRequestEvent(
        OrderId orderId,
        BotId botId,
        decimal limitPricePerAsset,
        decimal limitQuantity,
        Ticker ticker,
        OrderDirection orderDirection,
        OrderType orderType)
    {
        OrderId = orderId;
        BotId = botId;
        LimitPricePerAsset = limitPricePerAsset;
        LimitQuantity = limitQuantity;
        Ticker = ticker;
        OrderDirection = orderDirection;
        OrderType = orderType;
    }

    public static OrderCreateLimitSellRequestEvent Create(Order order, LadderBot bot)
    {
        return new OrderCreateLimitSellRequestEvent(
            order.Id,
            bot.Id,
            order.InitialData.LimitPricePerAsset!.Value,
            order.InitialData.LimitQuantity!.Value,
            bot.Ticker,
            order.Direction,
            order.Type);
    }
}