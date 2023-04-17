namespace PerfumeShop.Web.Services;

public sealed class BasketViewModelService : IBasketViewMoelService
{
    private readonly IUnitOfWork<CatalogDbContext> _catalog;
    private readonly IUnitOfWork<ShoppingDbContext> _shopping;
    private readonly ILogger<BasketService> _logger;


    public BasketViewModelService(
        IUnitOfWork<ShoppingDbContext> shopping,
        IUnitOfWork<CatalogDbContext> catalog,
        ILogger<BasketService> logger)
    {
        _shopping = shopping;
        _catalog = catalog;
        _logger = logger;
    }


    public async Task<int> CountTotalBasketItemsAsync(string userName)
    {
        var totalItems = await _shopping.GetRepository<Basket>()
            .CountAsync(b => b.BuyerId == userName);
            
        throw new NotImplementedException();
    }

    public Task<BasketViewModel> GetBasketForUser(string userName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsAvailableQuantityAsync(string userName, int productId)
    {
        throw new NotImplementedException();
    }
}
