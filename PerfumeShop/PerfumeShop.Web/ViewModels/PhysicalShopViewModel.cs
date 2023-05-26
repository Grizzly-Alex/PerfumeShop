namespace PerfumeShop.Web.ViewModels;

public sealed class PhysicalShopViewModel : EntityViewModel
{
    public AddressViewModel Address { get; set; }
    public TimeOnly OpenTime { get; set; }
    public TimeOnly CloseTime { get; set; }
    public List<CheckboxViewModel> DayOfWeek { get; set; }
}
