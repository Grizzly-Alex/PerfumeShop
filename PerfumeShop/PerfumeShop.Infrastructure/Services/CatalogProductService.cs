namespace PerfumeShop.Infrastructure.Services;

public sealed class CatalogProductService : ICatalogProductService
{
	private readonly IUnitOfWork<CatalogDbContext> _unitOfWork;
	private readonly ILogger<CheckoutService> _logger;

	public CatalogProductService(
		IUnitOfWork<CatalogDbContext> unitOfWork,
		ILogger<CheckoutService> logger)
    {
        _unitOfWork = unitOfWork;
		_logger = logger;
    }


    public async Task<CatalogProduct> UpdateProductStockAsync(int productId, int newQuantity)
	{
		var productRepository = _unitOfWork.GetRepository<CatalogProduct>();
		var product = await productRepository.GetFirstOrDefaultAsync(predicate: i => i.Id == productId);

		if (product is not null)
		{
			product.SetStock(newQuantity);
			productRepository.Update(product);
			await _unitOfWork.SaveChangesAsync();

			_logger.LogInformation($"Has been set new quantity in stock: '{product.Stock}' for product with ID: '{product.Id}'.");
		}

		return product;
	}

	public async Task<IEnumerable<CatalogProduct>> UpdateStockAfterOrderAsync(IEnumerable<OrderItem> orderItems)
	{
		var productRepository = _unitOfWork.GetRepository<CatalogProduct>();

		var productsId = orderItems.Select(b => b.ProductId).ToList();
		var products = await productRepository.GetAllAsync(predicate: b => productsId.Contains(b.Id));

		var updatedProducts = orderItems.Select(i =>
		{
			var product = products.First(c => c.Id == i.ProductId);
			product.SetStock(product.Stock - i.Quantity);

			_logger.LogInformation($"Has been set new quantity in stock: '{product.Stock}' for product with ID: '{product.Id}'.");

			return product;

		}).ToList();

		productRepository.Update(updatedProducts);
		await _unitOfWork.SaveChangesAsync();

		_logger.LogInformation($"Quantity of products in stock updated successfully.");

		return updatedProducts;
	}
}
