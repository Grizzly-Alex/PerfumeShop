namespace PerfumeShop.Web.ViewModels.Order;

public sealed class OrderViewModel
{
    public OrderInfoViewModel OrderInfo { get; set; }
    public IList<OrderItemViewModel> OrderItems { get; set; }
}
