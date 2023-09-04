namespace PerfumeShop.Web.ViewModels.Order;

public sealed class OrderItemViewModel
{
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public string PictureUri { get; set; }
    public int ProductId { get; set; }  
}
