namespace PerfumeShop.Infrastructure.DataAccess.Queries;

public sealed class BasketItemQueryService : IBasketItemQueryService
{
	private readonly IUnitOfWork<ShoppingDbContext> _unitOfWork;

    public BasketItemQueryService(IUnitOfWork<ShoppingDbContext> unitOfWork)
		=> _unitOfWork = unitOfWork;

	public async Task<int> GetQuantityAsync(int productId)
	{
		return await _unitOfWork.GetRepository<BasketItem>()
			.GetFirstOrDefaultAsync(
				predicate: b => b.ProductId == productId,
				selector: b => b.Quantity);
	}
}
