using AlgoTrader.Core.Entities.Orders.Enums;
using AlgoTrader.Core.ValueObjects.Orders;

namespace AlgoTrader.Core.Entities.Orders;

public sealed record OrderUpdateEvent(
    OrderId OrderId,
    DateTime CreatedAt,
    OrderDirection Direction,
    OrderType OrderType,
    OrderStatus OrderStatus,
    decimal LimitQuantity,
    decimal LimitPricePerAsset,
    ExchangeData ExchangeData,
    string? ErrorMessage = null);