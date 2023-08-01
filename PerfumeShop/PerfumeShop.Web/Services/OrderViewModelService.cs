namespace PerfumeShop.Web.Services;

public sealed class OrderViewModelService : IOrderViewModelService
{
	private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork<SaleDbContext> _sale;
    private readonly IUnitOfWork<CatalogDbContext> _catalog;
    private readonly ILogger<OrderViewModelService> _logger;
	private readonly IBasketViewModelService _basketViewModelService;
	private readonly IViewModelService<PhysicalShop, PhysicalShopViewModel, SaleDbContext> _physicalShopViewModelService;
    private readonly IViewModelService<PaymentMethod, ItemViewModel, SaleDbContext> _paymentMethodViewModelService;

    public OrderViewModelService(
        UserManager<AppUser> userManager,
        IMapper mapper,
        IUnitOfWork<SaleDbContext> sale,
        IUnitOfWork<CatalogDbContext> catalog,
        ILogger<OrderViewModelService> logger,
        IBasketViewModelService basketViewModelService,
		IViewModelService<PhysicalShop, PhysicalShopViewModel, SaleDbContext> physicalShopViewModelService,
        IViewModelService<PaymentMethod, ItemViewModel, SaleDbContext> paymentMethodViewModelService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _sale = sale;
        _catalog = catalog;
        _logger = logger;
        _basketViewModelService = basketViewModelService;
        _physicalShopViewModelService = physicalShopViewModelService;
        _paymentMethodViewModelService = paymentMethodViewModelService;
    }

	public async Task<OrderCreateViewModel> GetOrderCreateModelForAnonymousUserAsync(string userName)
	{
		_logger.LogInformation($"Getting Order Create Model for anonymous user.");

		var physicalShopes = await _physicalShopViewModelService.GetViewModelsAsync();
        var paymentMethods = await _paymentMethodViewModelService.GetViewModelsAsync();

        return new OrderCreateViewModel()
        {
            Basket = await _basketViewModelService.GetBasketForUserAsync(userName),
			PickupPoints = physicalShopes.ToSelectListItems(),
            PaymentMethods = paymentMethods.ToSelectListItems(),
        };
	}

	public async Task<OrderCreateViewModel> GetOrderCreateModelForAuthorizedUserAsync(string userName)
	{
		_logger.LogInformation($"Getting Order Create Model for authorized user.");

		var user = await _userManager.FindByNameAsync(userName);
		var physicalShopes = await _physicalShopViewModelService.GetViewModelsAsync();
        var paymentMethods = await _paymentMethodViewModelService.GetViewModelsAsync();

        return new OrderCreateViewModel()
	    {
		    Basket = await _basketViewModelService.GetBasketForUserAsync(userName),
		    Buyer = _mapper.Map<BuyerViewModel>(user),
		    Address = _mapper.Map<AddressViewModel>(user),
		    PickupPoints = physicalShopes.ToSelectListItems(),
		    PaymentMethods = paymentMethods.ToSelectListItems(),
        };
	}

    public async Task<OrderViewModel> GetOrderViewModelAsync(int orderId)
    {
        return new OrderViewModel()
        {
            OrderInfo = await GetOrderInfoModelAsync(orderId),
            OrderItems = await GetOrderItemModelCollectionAsync(orderId)
        };
    }

    public async Task<OrderInfoViewModel> GetOrderInfoModelAsync(int orderId)
    {
        var orderHeader = await _sale.GetRepository<OrderHeader>()
            .GetFirstOrDefaultAsync(
                predicate: order => order.Id == orderId,
                include: query => query
                    .Include(order => order.OrderStatus)
					.Include(order => order.PaymentDetail)
					.ThenInclude(payment => payment.PaymentMethod)
                    .Include(order => order.PaymentDetail)
                    .ThenInclude(payment => payment.PaymentStatus)
                    .Include(order => order.DeliveryDetail)
					.ThenInclude(delivery => delivery.DeliveryMethod), 
				isTracking: false) ?? throw new NullReferenceException($"OrderHeader not found in database with ID: '{orderId}'.");

        _logger.LogInformation($"Getting Order Header with ID:'{orderId}' successfully.");

        return _mapper.Map<OrderInfoViewModel>(orderHeader);
    }

    public async Task<IList<OrderInfoViewModel>> GetOrderInfoModelCollectionAsync(string userId)
    {
        var orderHeaders = await _sale.GetRepository<OrderHeader>()
            .GetAllAsync(
                predicate: order => order.Customer.UserId == userId,
                include: query => query
                    .Include(order => order.OrderStatus)
                    .Include(order => order.PaymentDetail)
                    .ThenInclude(payment => payment.PaymentMethod)
                    .Include(order => order.PaymentDetail)
                    .ThenInclude(payment => payment.PaymentStatus)
                    .Include(order => order.DeliveryDetail)
                    .ThenInclude(delivery => delivery.DeliveryMethod),
                isTracking: false) ?? throw new NullReferenceException($"OrderHeader not found in database for user with ID: '{userId}'.");

        orderHeaders.ToList().ForEach(orderHeader => _logger.LogInformation($"Getting Order Header with ID:'{orderHeader.Id}' successfully."));

        return _mapper.Map<IList<OrderInfoViewModel>>(orderHeaders.ToList()); 
    }

    public async Task<IList<OrderItemViewModel>> GetOrderItemModelCollectionAsync(int orderId)
    {
        var orderItems = await _sale.GetRepository<OrderHeader>()
            .GetFirstOrDefaultAsync(
            predicate: order => order.Id == orderId,
            selector: i => i.OrderItems);

        var orderItemsId = orderItems.Select(b => b.ProductId).ToList();

        var products = await _catalog.GetRepository<CatalogProduct>()
            .GetAllAsync(
                predicate: b => orderItemsId.Contains(b.Id),
                include: product => product.Include(product => product.Brand));
    

        var orderItemViewModel = orderItems.Select(orderItem =>
        {
            var product = products.First(product => product.Id == orderItem.ProductId);
            var item = new OrderItemViewModel
            {
                Quantity = orderItem.Quantity,
                Price = orderItem.Price,
                TotalPrice = orderItem.TotalPrice,
                Brand = product.Brand.Name,
                Name = product.Name,
                PictureUri = product.PictureUri,
                ProductId = orderItem.ProductId,
            };

            _logger.LogInformation($"Get Order item with ID: '{orderItem.Id}' Name: '{item.Name}'.");

            return item;

        }).ToList();

        return orderItemViewModel;
    }

}