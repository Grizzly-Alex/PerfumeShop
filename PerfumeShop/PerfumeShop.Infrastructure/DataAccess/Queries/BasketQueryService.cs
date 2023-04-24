namespace PerfumeShop.Infrastructure.DataAccess.Queries;

public sealed class BasketQueryService : IBasketQueryService
{
	private readonly IUnitOfWork<ShoppingDbContext> _unitOfWork;

    public BasketQueryService(IUnitOfWork<ShoppingDbContext> unitOfWork)
		=> _unitOfWork = unitOfWork;


    public async Task<int> CountTotalBasketItemsAsync(string userName)
	{
		return await _unitOfWork.GetRepository<Basket>()
			.CountAsync(
				predicate: basket => basket.BuyerId == userName,
				selector: item => item.Items,
				sum: sum => sum.Quantity);
	}
}
