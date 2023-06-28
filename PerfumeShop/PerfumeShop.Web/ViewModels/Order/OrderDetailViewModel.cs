namespace PerfumeShop.Web.ViewModels.Order;

public sealed class OrderDetailViewModel
{
    public IEnumerable<OrderItemViewModel> OrderItems { get; set; }

    public OrderDetailViewModel(IEnumerable<OrderItemViewModel> orderItems)
    {
        OrderItems = orderItems;
    }
}
