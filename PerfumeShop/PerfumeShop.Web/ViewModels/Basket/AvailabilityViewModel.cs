namespace PerfumeShop.Web.ViewModels.Basket;

public sealed class AvailabilityViewModel
{
    public string? ProductName { get; set; }
    public int StockQty { get; set; }
    public int DesiredQty { get; set; }
    public bool IsAvailable => StockQty >= DesiredQty;
}
