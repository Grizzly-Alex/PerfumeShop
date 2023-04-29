namespace PerfumeShop.Web.Services;

public sealed class BasketViewModelService : IBasketViewModelService
{
    private readonly IMapper _mapper;
    private readonly ICheckoutService _checkoutService;
    private readonly IUnitOfWork<CatalogDbContext> _catalog;
    private readonly IUnitOfWork<ShoppingDbContext> _shopping;
    private readonly ILogger<BasketService> _logger;


    public BasketViewModelService(IMapper mapper,
        IUnitOfWork<ShoppingDbContext> shopping,
        IUnitOfWork<CatalogDbContext> catalog,
		ICheckoutService checkoutService,
	    ILogger<BasketService> logger)
    {
        _mapper = mapper;
        _shopping = shopping;
        _catalog = catalog;
        _checkoutService = checkoutService;
		_logger = logger;
    }

    public async Task<BasketViewModel> GetBasketForUserAsync(string userName)
    {
        var basketRepository = _shopping.GetRepository<Basket>();
        var basket = await basketRepository.GetFirstOrDefaultAsync(
            predicate: basket => basket.BuyerId == userName,
            include: query => query.Include(i => i.Items));

        if (basket is null)
        {
            basket = new Basket(userName);
            basketRepository.Add(basket);
            await _shopping.SaveChangesAsync();

            _logger.LogInformation($"New Basket was created for user with ID {userName}.");

            return _mapper.Map<BasketViewModel>(basket);
		}  
        
        var basketVM = _mapper.Map<BasketViewModel>(basket);
        basketVM.Items = await GetBasketItemsAsync(basket.Items);
        basketVM.FinalPrice = await _checkoutService.CalculateFinalPriceAsync(basketVM.TotalProductsPrice);

		_logger.LogInformation($"Get basket with ID {basket.Id}");

		return basketVM;
	}

	private async Task<List<BasketItemViewModel>> GetBasketItemsAsync(IReadOnlyCollection<BasketItem> basketItems)
    {
        var basketItemsId = basketItems.Select(b => b.ProductId).ToList();

        var products = await _catalog.GetRepository<CatalogProduct>()
            .GetAllAsync(predicate: b => basketItemsId.Contains(b.Id),
                        include: b => b.Include(b => b.Brand));

        var items = basketItems.Select(basketItem =>
        {
			var product = products.First(c => c.Id == basketItem.ProductId);
            var basketItemViewModel = new BasketItemViewModel
            {
                Id = basketItem.Id,
                ProductId = basketItem.ProductId,
                Brand = product.Brand.Name,
                Name = product.Name,
                Volume = product.Volume,
                PictureUri = product.PictureUri,
                UnitPrice = product.Price,
				Quantity = basketItem.Quantity,
                Stock = product.Stock,
            };
            return basketItemViewModel;
		}).ToList();

        return items;   
	}
}