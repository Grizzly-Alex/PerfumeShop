namespace PerfumeShop.Infrastructure.DataAccess.Queries;

public sealed class BasketQueryService : IBasketQueryService
{
	private readonly IUnitOfWork<SaleDbContext> _unitOfWork;

    public BasketQueryService(IUnitOfWork<SaleDbContext> unitOfWork)
		=> _unitOfWork = unitOfWork;


    public async Task<int> CountTotalBasketItemsAsync(string userName)	
		=> await _unitOfWork.GetRepository<Basket>()
			.CountAsync(
				predicate: basket => basket.BuyerId == userName,
				selector: item => item.Items,
				sum: sum => sum.Quantity);
	

    public async Task<int> GetBasketIdAsync(string userName)   
        => await _unitOfWork.GetRepository<Basket>()
            .GetFirstOrDefaultAsync(
				predicate: i => i.BuyerId == userName,
				selector: b => b.Id);
    

	public async Task<int> GetProductQtyAsync(string userName, int productId)
		=> await _unitOfWork.GetRepository<Basket>()
			.GetFirstOrDefaultAsync(
				predicate: i => i.BuyerId == userName,
				selector: b => b.Items
					.Where(b => b.ProductId == productId)
					.Select(b => b.Quantity)
					.FirstOrDefault());
}
