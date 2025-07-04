using AlgoTrader.Core.Entities.Base;
using AlgoTrader.Core.Entities.Orders;
using AlgoTrader.Core.Entities.Orders.Enums;
using AlgoTrader.Core.Events.Orders.Requests;
using AlgoTrader.Core.ValueObjects;
using AlgoTrader.Core.ValueObjects.Bots;

namespace AlgoTrader.Core.Entities.Bots;

public sealed class LadderBot : AggregateRoot
{
    public BotId Id { get; internal set; }
    public DateTime CreatedAt { get; internal set; }
    public DateTime? ModifiedAt { get; internal set; }
    public Ticker Ticker { get; internal set; }
    public TickerId TickerId { get; internal set; }
    public LadderBotSettings Settings { get; internal set; }
    public List<Order> Orders { get; set; } = new List<Order>();
    public List<Order> ActiveOrders => Orders.Where(x => x.InProgress).ToList();

    internal decimal? LastBuyPrice => Orders
        .Where(x => x.Direction == OrderDirection.Buy
            && x.IsOrderExecuted
            && x.CreatedBy == CreatedBy.Bot)
        .OrderByDescending(x => x.ModifiedAt)
        .FirstOrDefault()
        ?.ExchangeData?.AverageAssetPrice;

    internal decimal MoneyEarnedOnSell
    {
        get
        {
            return Orders
                .Where(x => x.Direction == OrderDirection.Sell && x.IsOrderExecuted)
                .Sum(x => x.ExchangeData?.Funds ?? 0);
        }
    }

    public decimal MoneySpentOnBuy
    {
        get
        {
            return Orders
                .Where(x => x.Direction == OrderDirection.Buy && x.IsOrderExecuted)
                .Sum(x => x.ExchangeData?.Funds ?? 0);
        }
    }

    public decimal TradeBalance => MoneySpentOnBuy - MoneyEarnedOnSell;

    public decimal TradeAssetQuantity
    {
        get
        {
            decimal buySide = Orders
                .Where(x => x.Direction == OrderDirection.Buy
                    && x.IsOrderExecuted)
                .Sum(x => x.ExchangeData?.Quantity ?? 0);
            decimal sellSide = Orders
                .Where(x => x.Direction == OrderDirection.Sell
                    && x.IsOrderExecuted)
                .Sum(x => x.ExchangeData?.Quantity ?? 0);

            return buySide - sellSide;
        }
    }

    public decimal TradeAveragePrice
    {
        get
        {
            if (TradeBalance > 0 && TradeAssetQuantity > 0)
            {
                return Math.Round(TradeBalance / TradeAssetQuantity, GlobalSettings.Round);
            }

            return 0;
        }
    }

    public decimal FrozenInOrderAssetQuantity
    {
        get
        {
            decimal frozenInOrderQuantity = ActiveOrders
                .Where(x => x.Direction == OrderDirection.Sell)
                .Sum(x => x.InitialData.LimitQuantity ?? 0);

            return decimal.Round(frozenInOrderQuantity, GlobalSettings.Round);
        }
    }

    public decimal TradeTotalSellSum
    {
        get
        {
            return Orders
                .Where(x => x.Direction == OrderDirection.Sell
                    && x.IsOrderExecuted)
                .Sum(x => x.ExchangeData?.Funds ?? 0);
        }
    }

    public decimal TradeTotalBuySum
    {
        get
        {
            return Orders
                .Where(x => x.Direction == OrderDirection.Buy
                    && x.IsOrderExecuted)
                .Sum(x => x.ExchangeData?.Funds ?? 0);
        }
    }

    internal decimal TradeIncome => TradeTotalSellSum - TradeTotalBuySum;
    internal bool CanCreateSellOrder => decimal.Round(TradeAveragePrice * TradeAssetQuantity, decimals: 1) >= LadderBotSettings.MinimumLimitPerOrder;

    public decimal RemainingFunds => Math.Max(Settings.CalculatedLimitDeposit - TradeBalance, val2: 0);

    internal LadderBot(
        BotId id,
        TickerId tickerId,
        DateTime createdAt,
        DateTime? modifiedAt,
        LadderBotSettings settings)
    {
        Id = id;
        TickerId = tickerId;
        CreatedAt = createdAt;
        ModifiedAt = modifiedAt;
        Settings = settings;
    }

    private LadderBot()
    {
    }

    public static LadderBot Create(BotId id, TickerId ticker, DateTime createdAt, LadderBotSettings settings)
    {
        var bot = new LadderBot(id, ticker, createdAt, modifiedAt: null, settings);

        return bot;
    }

    public void UpdateSettings(LadderBotSettings settings)
    {
        if (Equals(Settings, settings))
        {
            return;
        }

        Settings = settings;
    }

    private bool CanCreateBuyOrder(decimal? orderMoneyLimit)
    {
        return orderMoneyLimit <= RemainingFunds;
    }

    private decimal CalculateBuyLimitPrice()
    {
        int buyCount = Orders.Count(x => x.IsOrderExecuted && x.IsLimitBuy && x.CreatedBy == CreatedBy.Bot);

        // decimal fallPercentage = Settings.IncreaseFallPercentageAfterNBuys.HasValue
        //     && Settings.IncreaseFallPercentageAfterNBuys > 0
        //     && buyCount >= Settings.IncreaseFallPercentageAfterNBuys
        //         ? Settings.FallPercentage * 2
        //         : Settings.FallPercentage;

        decimal fallPercentage = Settings.FallPercentage;

        return LastBuyPrice != null
            ? decimal.Round(LastBuyPrice.Value - LastBuyPrice.Value * fallPercentage / 100, GlobalSettings.Round)
            : decimal.Round(TradeAveragePrice - TradeAveragePrice * fallPercentage / 100, GlobalSettings.Round);
    }

    private decimal CalculateBuyLimitQuantity(decimal buyPrice)
    {
        return decimal.Round(Settings.CalculatedLimitPerOrder / buyPrice, GlobalSettings.Round);
    }

    private decimal CalculateSellLimitQuantity()
    {
        return decimal.Round(TradeAssetQuantity - FrozenInOrderAssetQuantity, GlobalSettings.Round);
    }

    private decimal CalculateSellLimitPrice()
    {
        return decimal.Round(TradeAveragePrice + TradeAveragePrice * Settings.ProfitPercentage / 100, GlobalSettings.Round);
    }

    public Order? CreateMarketBuy(decimal? marketMoneyLimit = null, CreatedBy createdBy = CreatedBy.Bot)
    {
        marketMoneyLimit ??= Settings.CalculatedLimitPerOrder;

        if (!CanCreateBuyOrder(marketMoneyLimit))
        {
            return null;
        }

        DateTime createdAt = DateTime.UtcNow;
        var order = Order.CreateMarketBuy(
            Guid.NewGuid(),
            marketMoneyLimit.Value,
            Settings.Taker,
            Id,
            createdAt,
            createdBy);
        ModifiedAt = createdAt;
        Orders.Add(order);
        AddEvent(OrderCreateMarketBuyRequestEvent.Create(order, this));

        return order;
    }

    private Order? CreateLimitBuyOrder()
    {
        if (!CanCreateBuyOrder(Settings.CalculatedLimitPerOrder))
        {
            return null;
        }

        decimal buyPrice = CalculateBuyLimitPrice();
        decimal buyQuantity = CalculateBuyLimitQuantity(buyPrice);
        DateTime createdAt = DateTime.UtcNow;
        var order = Order.CreateLimitBuy(
            Guid.NewGuid(),
            buyQuantity,
            buyPrice,
            Settings.Taker,
            Id,
            createdAt,
            CreatedBy.Bot);
        ModifiedAt = createdAt;
        Orders.Add(order);
        AddEvent(OrderCreateLimitBuyRequestEvent.Create(order, this));

        return order;
    }

    private Order? CreateLimitSellOrder()
    {
        decimal sellQuantity = CalculateSellLimitQuantity();
        decimal sellPrice = CalculateSellLimitPrice();
        DateTime createdAt = DateTime.UtcNow;
        var order = Order.CreateLimitSell(
            Guid.NewGuid(),
            sellQuantity,
            sellPrice,
            Settings.Taker,
            Id,
            createdAt,
            CreatedBy.Bot);
        Orders.Add(order);
        ModifiedAt = createdAt;
        AddEvent(OrderCreateLimitSellRequestEvent.Create(order, this));

        return order;
    }

    public void UpdateOrder(OrderUpdateEvent orderUpdate)
    {
        Order? order = Orders.FirstOrDefault(x => x.Id.Value == orderUpdate.OrderId.Value);

        if (order == null)
        {
            return;
        }

        order.Update(orderUpdate);

        // if (orderUpdate.ErrorType.HasValue)
        // {
        //     if (orderUpdate.ErrorType == ErrorType.ApiKeyInvalid)
        //     {
        //         if (Status != BotStatus.Stopped)
        //         {
        //             AddEvent(UserApiKeyInvalidEvent.Create(Id, UserId));
        //             StopBot();
        //         }
        //
        //         return;
        //     }
        // }

        // if (Status == BotStatus.Stopped)
        // {
        //     return;
        // }

        if (ActiveOrders.Count == 0)
        {
            switch (order.Status)
            {
                case OrderStatus.Filled:
                    if (order.IsMarketBuy || order.IsLimitBuy)
                    {
                        CreateLimitBuyOrder();
                        Order? newSellOrder = CreateLimitSellOrder();
                        // AddEvent(BuyOrderFilledEvent.Create(order, newSellOrder, this));
                    }
                    else if (order.IsLimitSell)
                    {
                        // AddEvent(SellOrderFilledEvent.Create(order, this, TradeIncome));
                        Settings.UpdateTotalIncome(TradeIncome);
                        MoveOrdersToHistoryAndClear();
                        CreateMarketBuy();
                    }

                    break;
                case OrderStatus.Error:
                case OrderStatus.Canceled:
                case OrderStatus.PartiallyCanceled:
                    if (order.Direction == OrderDirection.Buy
                        || Orders.Any(
                            x => x.Direction == OrderDirection.Sell
                                && x.Status == OrderStatus.Filled))
                    {
                        MoveOrdersToHistoryAndClear();
                        CreateMarketBuy();
                    }
                    else if (CanCreateSellOrder)
                    {
                        CreateLimitSellOrder();
                        CreateLimitBuyOrder();
                    }

                    break;
            }
        }
        else if (ActiveOrders.Count == 1)
        {
            switch (order.Status)
            {
                case OrderStatus.Filled:
                    if (order.IsLimitBuy || order.IsMarketBuy)
                    {
                        SetCancelPendingToAllActiveOrders();
                        // AddEvent(
                        //     BuyOrderFilledEvent.Create(
                        //         order,
                        //         TradeAssetQuantity,
                        //         CalculateSellLimitPrice(),
                        //         UserId,
                        //         Id,
                        //         Ticker));
                    }
                    else if (order.IsLimitSell)
                    {
                        SetCancelPendingToAllActiveOrders();
                        // AddEvent(SellOrderFilledEvent.Create(order, this, TradeIncome));
                        Settings.UpdateTotalIncome(TradeIncome);
                    }

                    break;

                case OrderStatus.Error:
                case OrderStatus.Canceled:
                case OrderStatus.PartiallyCanceled:
                    Order activeOrder = ActiveOrders.First();

                    if (activeOrder.IsLimitSell && CanCreateBuyOrder(Settings.CalculatedLimitPerOrder))
                    {
                        CreateLimitBuyOrder();
                    }
                    else if (activeOrder.IsLimitBuy && CanCreateSellOrder)
                    {
                        CreateLimitSellOrder();
                    }

                    break;
            }
        }
        else if (ActiveOrders.Count >= 2)
        {
            switch (order.Status)
            {
                case OrderStatus.PartiallyFilled:
                    foreach (Order toCancel in ActiveOrders.Where(x => !Equals(x.Id, order.Id) && x.Status != OrderStatus.PendingCancel))
                    {
                        toCancel.SetPendingCancel();
                        AddEvent(OrderCancelRequestEvent.Create(toCancel, this));
                    }

                    break;
                case OrderStatus.Filled:
                    if (order.IsMarketBuy)
                    {
                        SetCancelPendingToAllActiveOrders();
                        // AddEvent(
                        //     BuyOrderFilledEvent.Create(
                        //         order,
                        //         TradeAssetQuantity,
                        //         CalculateSellLimitPrice(),
                        //         UserId,
                        //         Id,
                        //         Ticker));
                    }

                    break;
            }
        }
    }

    private void MoveOrdersToHistoryAndClear()
    {
        var tradeId = Guid.NewGuid();
        // AddEvent(OrdersToHistoryEvent.Create(this, Orders.ToList(), tradeId));

        Orders.Clear();
    }

    private void SetCancelPendingToAllActiveOrders()
    {
        foreach (Order toCancel in ActiveOrders.Where(x => x.Status != OrderStatus.PendingCancel))
        {
            toCancel.SetPendingCancel();
            // AddEvent(OrderCancelRequestEvent.Create(toCancel, this));
        }
    }
}