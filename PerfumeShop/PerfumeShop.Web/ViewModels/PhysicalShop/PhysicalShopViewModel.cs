namespace PerfumeShop.Web.ViewModels.PhysicalShop;

public sealed class PhysicalShopViewModel : EntityViewModel
{
    public AddressViewModel Address { get; set; }
    public TimeOnly OpenTime { get; set; }
    public TimeOnly CloseTime { get; set; }
    [ValidateNever]
    public List<DayOfWeek> Weekends { get; set; } = new();
    public string? WeekendsStr 
    {
        get => string.Join(", ", Weekends.ToArray());
    }
}
