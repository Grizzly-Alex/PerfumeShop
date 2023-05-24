namespace PerfumeShop.Web.Interfaces;

public interface IOrderViewModelService
{
    Task<OrderInfoViewModel> GetOrderAsync(int id);
}
