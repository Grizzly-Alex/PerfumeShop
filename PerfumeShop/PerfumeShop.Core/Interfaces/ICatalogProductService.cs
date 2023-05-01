namespace PerfumeShop.Core.Interfaces;

public interface ICatalogProductService
{
	Task<CatalogProduct> UpdateProductStockAsync(int productId, int newQuantity);
	Task<IEnumerable<CatalogProduct>> UpdateStockAfterOrderAsync(IEnumerable<OrderItem> orderItems);
}
