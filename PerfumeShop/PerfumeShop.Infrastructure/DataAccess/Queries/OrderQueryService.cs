namespace PerfumeShop.Infrastructure.DataAccess.Queries;
using Customer = Core.Models.ValueObjects.Customer;

public class OrderQueryService : IOrderQueryService
{
    private readonly IUnitOfWork<SaleDbContext> _unitOfWork;

    public OrderQueryService(IUnitOfWork<SaleDbContext> unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<Customer> GetCustomerAsync(int orderId)
        => await _unitOfWork.GetRepository<OrderHeader>()
        .GetFirstOrDefaultAsync(
            predicate: order => order.Id == orderId,
            selector: order => order.Customer) ?? throw new NullReferenceException();

    public async Task<int> GetOrderIdAsync(string trackingId)
        => await _unitOfWork.GetRepository<OrderHeader>()
        .GetFirstOrDefaultAsync(
            predicate: order => order.TrackingId == trackingId,
            selector: order => order.Id);

    public async Task<decimal> GetTotalCostAsync(int orderId)
        => await _unitOfWork.GetRepository<OrderHeader>()
            .GetFirstOrDefaultAsync(
                predicate: order => order.Id == orderId,
                selector: order => order.Cost.TotalCost);

    public async Task<string> GetTrackingIdAsync(int orderId)
        => await _unitOfWork.GetRepository<OrderHeader>()
            .GetFirstOrDefaultAsync(
                predicate: order => order.Id == orderId,
                selector: order => order.TrackingId) ?? string.Empty;
}
