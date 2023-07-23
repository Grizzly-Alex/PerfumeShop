namespace PerfumeShop.Infrastructure.Services;


public class CheckoutService : ICheckoutService
{
	private readonly IUnitOfWork<CatalogDbContext> _catalog;
    private readonly IUnitOfWork<SaleDbContext> _shopping;
	private readonly ILogger<CheckoutService> _logger;

    public CheckoutService(
		IUnitOfWork<CatalogDbContext> catalog,
        IUnitOfWork<SaleDbContext> shopping,
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

    public Cost CalculateCostAsync(IEnumerable<OrderItem> items)
    {
		decimal itemsCost = items.Sum(i => i.TotalPrice);
		decimal totalCost = itemsCost;

        return new Cost(itemsCost, totalCost);
    }
}