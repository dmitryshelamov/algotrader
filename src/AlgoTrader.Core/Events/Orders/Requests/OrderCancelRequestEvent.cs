using AlgoTrader.Core.Entities;
using AlgoTrader.Core.Entities.Bots;
using AlgoTrader.Core.Entities.Orders;
using AlgoTrader.Core.Entities.Orders.Enums;
using AlgoTrader.Core.Events.Base;
using AlgoTrader.Core.ValueObjects.Bots;
using AlgoTrader.Core.ValueObjects.Orders;

namespace AlgoTrader.Core.Events.Orders.Requests;

public sealed class OrderCancelRequestEvent : IDomainEvent
{
    public OrderId OrderId { get; set; }
    public OrderDirection OrderDirection { get; set; }
    public OrderType OrderType { get; set; }
    public BotId BotId { get; set; }
    public Ticker Ticker { get; set; }

    public OrderCancelRequestEvent(OrderId orderId, BotId botId, Ticker ticker, OrderDirection orderDirection, OrderType orderType)
    {
        OrderId = orderId;
        BotId = botId;
        Ticker = ticker;
        OrderDirection = orderDirection;
        OrderType = orderType;
    }

    public static OrderCancelRequestEvent Create(Order order, LadderBot bot)
    {
        return new OrderCancelRequestEvent(order.Id, bot.Id, bot.Ticker, order.Direction, order.Type);
    }
}