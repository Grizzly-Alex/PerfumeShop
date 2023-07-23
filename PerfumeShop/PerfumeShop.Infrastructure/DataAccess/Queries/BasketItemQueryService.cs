namespace PerfumeShop.Infrastructure.DataAccess.Queries;

public sealed class BasketItemQueryService : IBasketItemQueryService
{
	private readonly IUnitOfWork<SaleDbContext> _unitOfWork;

    public BasketItemQueryService(IUnitOfWork<SaleDbContext> unitOfWork)
		=> _unitOfWork = unitOfWork;

    public async Task<int> GetProductIdAsync(int basketItemId)    
        => await _unitOfWork.GetRepository<BasketItem>()
            .GetFirstOrDefaultAsync(
            predicate: i => i.Id == basketItemId,
            selector: b => b.ProductId);
    

    public async Task<int> GetQuantityAsync(int basketItemId)	
		=> await _unitOfWork.GetRepository<BasketItem>()
			.GetFirstOrDefaultAsync(
				predicate: b => b.Id == basketItemId,
				selector: b => b.Quantity);	
}