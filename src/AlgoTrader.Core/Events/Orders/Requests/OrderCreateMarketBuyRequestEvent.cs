using AlgoTrader.Core.Entities;
using AlgoTrader.Core.Entities.Bots;
using AlgoTrader.Core.Entities.Orders;
using AlgoTrader.Core.Entities.Orders.Enums;
using AlgoTrader.Core.Events.Base;
using AlgoTrader.Core.ValueObjects.Bots;
using AlgoTrader.Core.ValueObjects.Orders;

namespace AlgoTrader.Core.Events.Orders.Requests;

public sealed class OrderCreateMarketBuyRequestEvent : IDomainEvent
{
    public OrderId OrderId { get; set; }
    public BotId BotId { get; set; }
    public decimal MarketMoneyLimit { get; set; }
    public Ticker Ticker { get; set; }
    public OrderDirection OrderDirection { get; set; }
    public OrderType OrderType { get; set; }

    public OrderCreateMarketBuyRequestEvent(
        OrderId orderId,
        BotId botId,
        decimal marketMoneyLimit,
        Ticker ticker,
        OrderDirection orderDirection,
        OrderType orderType)
    {
        OrderId = orderId;
        BotId = botId;
        MarketMoneyLimit = marketMoneyLimit;
        Ticker = ticker;
        OrderDirection = orderDirection;
        OrderType = orderType;
    }

    public static OrderCreateMarketBuyRequestEvent Create(Order order, LadderBot bot)
    {
        return new OrderCreateMarketBuyRequestEvent(
            order.Id,
            bot.Id,
            order.InitialData.MarketFunds!.Value,
            bot.Ticker,
            order.Direction,
            order.Type);
    }
}