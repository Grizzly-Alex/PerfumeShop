namespace PerfumeShop.Web.ViewModels.Basket;

public sealed class BasketItemViewModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string? PictureUrl { get; set; }
    public decimal UnitPrice { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be bigger than 0")]
    public int Quantity { get; set; }
}
