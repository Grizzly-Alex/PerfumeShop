﻿using Address = PerfumeShop.Core.Models.ValueObjects.Address;
using Customer = PerfumeShop.Core.Models.ValueObjects.Customer;
namespace PerfumeShop.Infrastructure.Services;

public sealed class OrderService : IOrderService
{
    private readonly ICheckoutService _checkoutService;
    private readonly IUnitOfWork<CatalogDbContext> _catalog;
    private readonly IUnitOfWork<SaleDbContext> _shopping;
    private readonly ILogger<OrderService> _logger;

    public OrderService(
        ICheckoutService checkoutService,
        IUnitOfWork<CatalogDbContext> catalog,
        IUnitOfWork<SaleDbContext> shopping,
        ILogger<OrderService> logger)
    {
        _checkoutService = checkoutService;
        _catalog = catalog;
        _shopping = shopping;
        _logger = logger;
    }


    public async Task<OrderHeader> CreateOrderAsync(
        PaymentMethods paymentMethod, DeliveryMethods deliveryMethod,
        Address shippingAddress, Customer customer,       
        int basketId)
    {
        var basketRepository = _shopping.GetRepository<Basket>();

        var basketItems = await basketRepository.GetFirstOrDefaultAsync(
            predicate: b => b.Id == basketId,
            include: b => b.Include(i => i.Items),
            selector: b => b.Items);

        var orderItems = await GetOrderItemsAsync(basketItems);
        var cost = _checkoutService.CalculateCostAsync(orderItems);
        var order = new OrderHeader(OrderStatuses.Pending, deliveryMethod, orderItems, cost, customer, shippingAddress);

        _shopping.GetRepository<OrderHeader>().Add(order);
        await _shopping.SaveChangesAsync();

        var payment = new PaymentDetail(PaymentStatuses.Pending, paymentMethod, order.Id);

        _shopping.GetRepository<PaymentDetail>().Add(payment);
        await _shopping.SaveChangesAsync();

        _logger.LogInformation($"Order with ID:'{order.Id}' has been created.");
        _logger.LogInformation($"Payment detail with ID:'{order.PaymentDetail.Id}' has been created.");

        return order;
    }

    public async Task UpdateOrderStatus(int orderId, OrderStatuses orderStatus)
    {
        var orderRepository = _shopping.GetRepository<OrderHeader>();
        var order = await orderRepository.GetFirstOrDefaultAsync(
            predicate: o => o.Id == orderId,
            include: o => o.Include(p => p.PaymentDetail));

        if (order is not null)
        {
            order.SetOrderStatus(orderStatus);
            orderRepository.Update(order);
            await _shopping.SaveChangesAsync();

            _logger.LogInformation($"Set new order status: '{orderStatus.GetDisplayName()}' for order with ID:'{order.Id}'");
        }
    }

    private async Task<List<OrderItem>> GetOrderItemsAsync(IReadOnlyCollection<BasketItem> basketItems)
    {
        var basketItemsId = basketItems.Select(b => b.ProductId).ToList();

        var products = await _catalog.GetRepository<CatalogProduct>()
            .GetAllAsync(predicate: b => basketItemsId.Contains(b.Id));

        var items = basketItems.Select(i =>
        {
            var product = products.First(c => c.Id == i.ProductId);
            var orderItem = new OrderItem(i.Quantity, product.Price, i.ProductId);

            _logger.LogInformation($"Order item with product ID: '{product.Id}' Name: '{product.Name}' has been created.");

            return orderItem;

        }).ToList();

        return items;
    }
}