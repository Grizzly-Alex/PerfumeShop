namespace PerfumeShop.Web.ViewModels.PhysicalShop;

public sealed class ManagePhysicalShopViewModel : EntityViewModel
{
    public AddressViewModel Address { get; set; }
    public TimeOnly OpenTime { get; set; }
    public TimeOnly CloseTime { get; set; }
    public List<CheckboxViewModel> DayOfWeek { get; set; }

    public ManagePhysicalShopViewModel(List<CheckboxViewModel> dayOfWeek)
    {
        DayOfWeek = dayOfWeek;
    }
}
