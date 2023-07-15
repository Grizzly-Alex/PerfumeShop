namespace PerfumeShop.Core.Interfaces;

public interface IOrderQueryService
{
    Task<string> GetTrackingId(int orderId);
}
