namespace PerfumeShop.Web.Interfaces;

public interface IOrderViewModelService
{
    Task<OrderViewModel> GetOrderAsync(int id);
}
