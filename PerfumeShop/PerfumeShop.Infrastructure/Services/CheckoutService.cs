using PerfumeShop.Core.Models.ValueObjects;

namespace PerfumeShop.Infrastructure.Services;

public class CheckoutService : ICheckoutService
{
	private readonly IUnitOfWork<CatalogDbContext> _catalog;

    public CheckoutService(IUnitOfWork<CatalogDbContext> catalog)
    {
		_catalog = catalog;
	}

    public async Task<ProductAvailability> AvailabilityStock(int productId, int quantity)
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

	public async Task<decimal> CalculateFinalPrice(decimal productTotalPrice)
	{
		
		return productTotalPrice;
	}
}