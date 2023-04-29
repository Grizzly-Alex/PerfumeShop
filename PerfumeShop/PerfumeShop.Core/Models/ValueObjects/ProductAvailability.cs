namespace PerfumeShop.Core.Models.ValueObjects;

public class ProductAvailability
{
    public string? ProductName { get; set; }
    public int StockQty { get; set; }
    public int DesiredQty { get; set; }
    public bool IsAvailable => StockQty >= DesiredQty;
}
