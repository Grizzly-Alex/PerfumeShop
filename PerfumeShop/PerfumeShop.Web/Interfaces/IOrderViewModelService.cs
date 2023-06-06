using PerfumeShop.Web.ViewModels.Order;

namespace PerfumeShop.Web.Interfaces;

public interface IOrderViewModelService
{
    Task<OrderInfoViewModel> GetOrderInfoModelAsync(int orderId);
    Task<OrderCreateViewModel> GetOrderCreateModelForAuthorizedUserAsync(string userName);
	Task<OrderCreateViewModel> GetOrderCreateModelForAnonymousUserAsync(string userName);
}
