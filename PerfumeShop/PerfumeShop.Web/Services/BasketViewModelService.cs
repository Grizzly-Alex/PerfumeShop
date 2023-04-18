namespace PerfumeShop.Web.Services;

public sealed class BasketViewModelService : IBasketViewMoelService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork<CatalogDbContext> _catalog;
    private readonly IUnitOfWork<ShoppingDbContext> _shopping;
    private readonly ILogger<BasketService> _logger;


    public BasketViewModelService(
        IMapper mapper,
        IUnitOfWork<ShoppingDbContext> shopping,
        IUnitOfWork<CatalogDbContext> catalog,
        ILogger<BasketService> logger)
    {
        _mapper = mapper;
        _shopping = shopping;
        _catalog = catalog;
        _logger = logger;
    }


    public async Task<int> CountTotalBasketItemsAsync(string userId)
    {
        return await _shopping.GetRepository<Basket>()
            .CountAsync(
                predicate: basket => basket.BuyerId == userId,
                selector: item => item.Items,
                sum: sum => sum.Quantity);           
    }

    public async Task<BasketViewModel> GetBasketForUserAsync(string userId)
    {
        var basketRepository = _shopping.GetRepository<Basket>();
        var basket = await basketRepository.GetFirstOrDefaultAsync(
            predicate: basket => basket.BuyerId == userId,
            include: query => query.Include(i => i.Items));

        if (basket is null)
        {
            basket = new Basket(userId);
            basketRepository.Add(basket);
            await _shopping.SaveChangesAsync();

            _logger.LogInformation($"New Basket was created for user with ID {userId}.");

            return _mapper.Map<BasketViewModel>(basket);        
        }

        _logger.LogInformation($"Get basket with ID {basket.Id}");

        return _mapper.Map<BasketViewModel>(basket);
    }

    public Task<bool> IsAvailableQuantityAsync(string userName, int productId)
    {
        throw new NotImplementedException();
    }
}
