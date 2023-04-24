namespace PerfumeShop.Web.ViewModels.Basket;

public sealed class BasketViewModel
{
    public int Id { get; set; }
    public string? BuyerId { get; set; }
    public List<BasketItemViewModel> Items { get; set; }
}
