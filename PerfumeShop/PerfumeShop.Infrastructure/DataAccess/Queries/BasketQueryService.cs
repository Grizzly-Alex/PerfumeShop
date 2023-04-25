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

    public async Task<int> GetBasketId(string userName)
    {
        return await _unitOfWork.GetRepository<Basket>()
            .GetFirstOrDefaultAsync(
				predicate: i => i.BuyerId == userName,
				selector: b => b.Id);
    }

	public async Task<int> GetProductQtyAsync(string userName, int productId)
	{
		return await _unitOfWork.GetRepository<Basket>()
			.GetFirstOrDefaultAsync(
				predicate: i => i.BuyerId == userName,
				selector: b => b.Items
					.Where(b => b.ProductId == productId)
					.Select(b => b.Quantity)
					.FirstOrDefault());
	}
}
