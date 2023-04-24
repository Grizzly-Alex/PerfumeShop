namespace PerfumeShop.Web.ViewModels.Basket;

public sealed class BasketItemViewModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public string? Brand { get; set; }
    public string? PictureUri { get; set; }
	public int? Volume { get; set; }
	public decimal UnitPrice { get; set; }
    public decimal TotalPrice => UnitPrice * Quantity;

	[Range(0, int.MaxValue, ErrorMessage = "Quantity must be bigger than 0")]
    public int Quantity { get; set; }
    public int Stock { get; set; }
}
