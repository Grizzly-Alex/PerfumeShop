namespace PerfumeShop.Web.ViewModels.PhysicalShop;

public sealed class PhysicalShopViewModel : EntityViewModel
{
    public AddressViewModel Address { get; set; }
    public TimeOnly OpenTime { get; set; }
    public TimeOnly CloseTime { get; set; }
    public string Weekends { set; get; }
}
