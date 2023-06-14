using PerfumeShop.Web.ViewModels.Order;
namespace PerfumeShop.Web.Services;

public sealed class OrderViewModelService : IOrderViewModelService
{
	private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork<SaleDbContext> _unitOfWork;
    private readonly ILogger<OrderViewModelService> _logger;
	private readonly IBasketViewModelService _basketViewModelService;
	private readonly IViewModelService<PhysicalShop, PhysicalShopViewModel, SaleDbContext> _viewModelService;

	public OrderViewModelService(
        UserManager<AppUser> userManager,
        IMapper mapper,
        IUnitOfWork<SaleDbContext> unitOfWork,
        ILogger<OrderViewModelService> logger,
        IBasketViewModelService basketViewModelService,
		IViewModelService<PhysicalShop, PhysicalShopViewModel, SaleDbContext> viewModelService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _basketViewModelService = basketViewModelService;
        _viewModelService = viewModelService;
    }

	public async Task<OrderCreateViewModel> GetOrderCreateModelForAnonymousUserAsync(string userName)
	{
		_logger.LogInformation($"Getting Order Create Model for anonymous user.");

		var physicalShopes = await _viewModelService.GetViewModelsAsync();

		return new OrderCreateViewModel()
        {
            Basket = await _basketViewModelService.GetBasketForUserAsync(userName),
			PhysicalShopes = physicalShopes.ToSelectListItems(),
			PaymentMethos = CheckBoxHelper.GetCheckBoxList<PaymentMethods>()
		};
	}

	public async Task<OrderCreateViewModel> GetOrderCreateModelForAuthorizedUserAsync(string userName)
	{
		_logger.LogInformation($"Getting Order Create Model for authorized user.");

		var user = await _userManager.FindByNameAsync(userName);
		var physicalShopes = await _viewModelService.GetViewModelsAsync();

	    return new OrderCreateViewModel()
	    {
		    Basket = await _basketViewModelService.GetBasketForUserAsync(userName),
		    Buyer = _mapper.Map<BuyerViewModel>(user),
		    Address = _mapper.Map<AddressViewModel>(user),
		    PhysicalShopes = physicalShopes.ToSelectListItems(),
		    PaymentMethos = CheckBoxHelper.GetCheckBoxList<PaymentMethods>()
	    };
	}

	public async Task<OrderInfoViewModel> GetOrderInfoModelAsync(int orderId)
    {
        var orderHeader = await _unitOfWork.GetRepository<OrderHeader>()
            .GetFirstOrDefaultAsync(
                predicate: order => order.Id == orderId,
                include: query => query
                    .Include(order => order.DeliveryMethod)
                    .Include(order => order.OrderStatus)
                    .Include(order => order.Customer)
                    .Include(order => order.DeliveryAddress)
                    .Include(order => order.PaymentDetail)
                    .ThenInclude(payment => payment.PaymentStatus)
					.Include(order => order.PaymentDetail)
					.ThenInclude(payment => payment.PaymentMethod),
				isTracking: false) ?? throw new NullReferenceException($"OrderHeader not found in database with ID: '{orderId}'.");

        _logger.LogInformation($"Getting Order Header with ID:'{orderId}' successfully.");

        return _mapper.Map<OrderInfoViewModel>(orderHeader);
    }
}
