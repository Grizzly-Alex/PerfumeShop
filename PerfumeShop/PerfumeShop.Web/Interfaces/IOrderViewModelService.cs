namespace PerfumeShop.Web.Interfaces;

public interface IOrderViewModelService
{
    Task<IList<OrderItemViewModel>> GetOrderItemModelCollectionAsync(int orderId);
    Task<OrderInfoViewModel> GetOrderInfoModelAsync(int orderId);
    Task<IList<OrderInfoViewModel>> GetOrderInfoModelCollectionAsync(string userId);
    Task<OrderCreateViewModel> GetOrderCreateModelForAuthorizedUserAsync(string userName);
	Task<OrderCreateViewModel> GetOrderCreateModelForAnonymousUserAsync(string userName);
}
