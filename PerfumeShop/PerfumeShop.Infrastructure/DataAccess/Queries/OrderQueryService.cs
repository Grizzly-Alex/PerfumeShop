namespace PerfumeShop.Infrastructure.DataAccess.Queries;

using System.Collections.Generic;
using Customer = Core.Models.ValueObjects.Customer;

public class OrderQueryService : IOrderQueryService
{
    private readonly IUnitOfWork<SaleDbContext> _unitOfWork;

    public OrderQueryService(IUnitOfWork<SaleDbContext> unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<Customer> GetCustomerAsync(int orderId)
        => await _unitOfWork.GetRepository<OrderHeader>()
        .GetFirstOrDefaultAsync(
            predicate: orderHeader => orderHeader.Id == orderId,
            selector: orderHeader => orderHeader.Customer) ?? throw new NullReferenceException();

    public async Task<int> GetOrderIdAsync(string trackingId)
        => await _unitOfWork.GetRepository<OrderHeader>()
        .GetFirstOrDefaultAsync(
            predicate: orderHeader => orderHeader.TrackingId == trackingId,
            selector: orderHeader => orderHeader.Id);

    public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId)
        => await _unitOfWork.GetRepository<OrderItem>()
        .GetAllAsync(predicate: orderItem => orderItem.OrderId == orderId);

    public async Task<decimal> GetTotalCostAsync(int orderId)
        => await _unitOfWork.GetRepository<OrderHeader>()
            .GetFirstOrDefaultAsync(
                predicate: orderHeader => orderHeader.Id == orderId,
                selector: orderHeader => orderHeader.Cost.TotalCost);

    public async Task<string> GetTrackingIdAsync(int orderId)
        => await _unitOfWork.GetRepository<OrderHeader>()
            .GetFirstOrDefaultAsync(
                predicate: orderHeader => orderHeader.Id == orderId,
                selector: orderHeader => orderHeader.TrackingId) ?? string.Empty;
}
