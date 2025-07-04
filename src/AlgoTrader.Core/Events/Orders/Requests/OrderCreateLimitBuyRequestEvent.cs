using AlgoTrader.Core.Entities;
using AlgoTrader.Core.Entities.Bots;
using AlgoTrader.Core.Entities.Orders;
using AlgoTrader.Core.Events.Base;
using AlgoTrader.Core.ValueObjects.Bots;
using AlgoTrader.Core.ValueObjects.Orders;

namespace AlgoTrader.Core.Events.Orders.Requests;

public sealed class OrderCreateLimitBuyRequestEvent : IDomainEvent
{
    public OrderId OrderId { get; set; }
    public BotId BotId { get; set; }
    public decimal LimitPricePerAsset { get; set; }
    public decimal LimitQuantity { get; set; }
    public Ticker Ticker { get; set; }

    public OrderCreateLimitBuyRequestEvent(
        OrderId orderId,
        BotId botId,
        decimal limitPricePerAsset,
        decimal limitQuantity,
        Ticker ticker)
    {
        OrderId = orderId;
        BotId = botId;
        LimitPricePerAsset = limitPricePerAsset;
        LimitQuantity = limitQuantity;
        Ticker = ticker;
    }

    public static OrderCreateLimitBuyRequestEvent Create(Order order, LadderBot bot)
    {
        return new OrderCreateLimitBuyRequestEvent(
            order.Id,
            bot.Id,
            order.InitialData.LimitPricePerAsset!.Value,
            order.InitialData.LimitQuantity!.Value,
            bot.Ticker);
    }
}