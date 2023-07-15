namespace PerfumeShop.Infrastructure.DataAccess.Queries;

public class OrderQueryService : IOrderQueryService
{
    private readonly IUnitOfWork<SaleDbContext> _unitOfWork;

    public OrderQueryService(IUnitOfWork<SaleDbContext> unitOfWork)
        => _unitOfWork = unitOfWork;


    public async Task<string> GetTrackingId(int orderId)
        => await _unitOfWork.GetRepository<OrderHeader>()
            .GetFirstOrDefaultAsync(
                predicate: order => order.Id == orderId,
                selector: order => order.TrackingId) ?? string.Empty;
}
