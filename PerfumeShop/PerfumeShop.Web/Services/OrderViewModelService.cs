namespace PerfumeShop.Web.Services;

public sealed class OrderViewModelService : IOrderViewModelService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork<SaleDbContext> _unitOfWork;
    private readonly ILogger<BasketService> _logger;

    public OrderViewModelService(
        IMapper mapper,
        IUnitOfWork<SaleDbContext> unitOfWork,
        ILogger<BasketService> logger)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<OrderInfoViewModel> GetOrderAsync(int id)
    {
        var orderHeader = await _unitOfWork.GetRepository<OrderHeader>()
            .GetFirstOrDefaultAsync(
                predicate: order => order.Id == id,
                include: query => query
                    .Include(order => order.DeliveryMethod)
                    .Include(order => order.OrderStatus)
                    .Include(order => order.Customer)
                    .Include(order => order.ShippingAddress)
                    .Include(order => order.PaymentDetail)
                    .ThenInclude(payment => payment.PaymentStatus)
					.Include(order => order.PaymentDetail)
					.ThenInclude(payment => payment.PaymentMethod),
				isTracking: false) ?? throw new NullReferenceException($"OrderHeader not found in database with ID: '{id}'.");

        _logger.LogInformation($"Getting Order Header with ID:'{id}' successfully.");

        return _mapper.Map<OrderInfoViewModel>(orderHeader);
    }
}
