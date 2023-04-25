namespace PerfumeShop.Infrastructure.DataAccess.Queries;

public sealed class ProductQueryService : IProductQueryService
{
	private readonly IUnitOfWork<CatalogDbContext> _unitOfWork;

    public ProductQueryService(IUnitOfWork<CatalogDbContext> unitOfWork)
		=> _unitOfWork = unitOfWork;


    public async Task<int> GetProductStockAsync(int productId)
	{
		return await _unitOfWork.GetRepository<CatalogProduct>()
			.GetFirstOrDefaultAsync(
				predicate: p => p.Id == productId,
				selector: p => p.Stock);
	}

	public async Task<string?> GetProductNameAsync(int productId)
	{
		return await _unitOfWork.GetRepository<CatalogProduct>()
			.GetFirstOrDefaultAsync(
				predicate: p => p.Id == productId,
				selector: p => p.Name);
	}
}
