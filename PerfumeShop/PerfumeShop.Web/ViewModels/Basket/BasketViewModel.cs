namespace PerfumeShop.Web.ViewModels.Basket;

public sealed class BasketViewModel
{
    public int Id { get; set; }
    public string? BuyerId { get; set; }
    public List<BasketItemViewModel> Items { get; set; } = new();
    public decimal TotalProductsPrice => Items.Sum(i => i.TotalPrice);
    public decimal FinalPrice { get; set; }
}