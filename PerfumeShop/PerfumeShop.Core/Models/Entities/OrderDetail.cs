namespace PerfumeShop.Core.Models.Entities;

public sealed class OrderDetail : Entity
{
	public int Quantity { get; set; }
	public decimal Price { get; set; }

	public int OrderId { get; set; }

    [ValidateNever]
    public Order Order { get; set; }

    public int ProductId { get; set; }

	[ValidateNever]
	public CatalogProduct Product { get; set; }
}
