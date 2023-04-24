namespace PerfumeShop.Web.Services;

public sealed class BasketViewModelService : IBasketViewModelService
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

    //public async Task<AvailabilityViewModel> BasketToStockRatio(string userName, int productId, int quantity)
    //{
    //    var basketRepository = _shopping.GetRepository<Basket>();
    //    var basket = await basketRepository.GetFirstOrDefaultAsync(
    //        predicate: p => p.BuyerId == userName,
    //        include: i => i.Include(i => i.Items));

    //    int basketItemQty = 0;

    //    if (basket is not null)
    //    {
    //        basketItemQty = basket!.Items
    //            .Where(i => i.ProductId == productId)
    //            .Select(i => i.Quantity)
    //            .FirstOrDefault();
    //    }

    //    var availabilityView = await _catalog.GetRepository<CatalogProduct>()
    //        .GetFirstOrDefaultAsync(
    //        predicate: p => p.Id == productId,
    //        selector: p => new AvailabilityViewModel
    //        {
    //            ProductName = p.Name,
    //            StockQty = p.Stock,
    //            BasketQty = basketItemQty + quantity
    //        });

    //    return availabilityView!;
    //}

	public async Task<AvailabilityViewModel> AvailabilityStock(int productId, int quantity)
	{
        var availabilityView = await _catalog.GetRepository<CatalogProduct>()
            .GetFirstOrDefaultAsync(
                predicate: p => p.Id == productId,
                selector: p => new AvailabilityViewModel
                {
                    ProductName = p.Name,
                    StockQty = p.Stock,
                    DesiredQty = quantity
                });

		return availabilityView!;
	}

	//public async Task<int> CountTotalBasketItemsAsync(string userName)
 //   {
 //       return await _shopping.GetRepository<Basket>()
 //           .CountAsync(
 //               predicate: basket => basket.BuyerId == userName,
 //               selector: item => item.Items,
 //               sum: sum => sum.Quantity);           
 //   }

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
