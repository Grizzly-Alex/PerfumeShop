namespace PerfumeShop.Infrastructure.Services;

public class CheckoutService : ICheckoutService
{
	private readonly IUnitOfWork<CatalogDbContext> _catalog;
    private readonly IUnitOfWork<ShoppingDbContext> _shopping;
	private readonly ILogger<CheckoutService> _logger;

    public CheckoutService(
		IUnitOfWork<CatalogDbContext> catalog,
        IUnitOfWork<ShoppingDbContext> shopping,
        ILogger<CheckoutService> logger)
    {
		_catalog = catalog;
		_shopping = shopping;
		_logger = logger;
	}

    public async Task<ProductAvailability> AvailabilityStockAsync(int productId, int quantity)
	{
		var availability = await _catalog.GetRepository<CatalogProduct>()
			.GetFirstOrDefaultAsync(
				predicate: p => p.Id == productId,
				selector: p => new ProductAvailability
				{
					ProductName = p.Name,
					StockQty = p.Stock,
					DesiredQty = quantity
				});

		return availability!;
	}
	
	public async Task<decimal> CalculateFinalPriceAsync(decimal productTotalPrice)
	{		
		return productTotalPrice;
	}

    public async Task<Order> CreateOrderAsync(BuyerInfo buyerInfo, int basketId)
    {
        var basketRepository = _shopping.GetRepository<Basket>();

		var basketItems = await basketRepository.GetFirstOrDefaultAsync(
			predicate: b => b.Id == basketId,
			include: b => b.Include(i => i.Items),
			selector: b => b.Items);

        var orderItems = await GetOrderItemsAsync(basketItems);
		var payablePrice = await CalculateFinalPriceAsync(orderItems.Sum(i => i.Quantity * i.Price));
		var paymentInfo = new PaymentInfo(PaymentStatuses.Pending, payablePrice);
		var order = new Order(DateTime.UtcNow, OrderStatuses.Pending, buyerInfo, paymentInfo, orderItems);

        _shopping.GetRepository<Order>().Add(order);
        await _shopping.SaveChangesAsync();

		_logger.LogInformation($"Order with ID:'{order.Id}' has been created.");

		return order;
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

            _logger.LogInformation($"Order item with ID:'{orderItem.Id}' has been created.");

            return orderItem;

		}).ToList();	

		return items;	
    }
}