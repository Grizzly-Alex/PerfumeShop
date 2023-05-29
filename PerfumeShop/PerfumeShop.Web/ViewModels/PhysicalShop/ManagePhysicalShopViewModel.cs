namespace PerfumeShop.Web.ViewModels.PhysicalShop;

public sealed class ManagePhysicalShopViewModel : EntityViewModel
{
    public AddressViewModel Address { get; set; }
    public TimeOnly OpenTime { get; set; }
    public TimeOnly CloseTime { get; set; }
    public List<CheckBoxViewModel> DayOfWeek { get; set; }

    public ManagePhysicalShopViewModel(List<CheckBoxViewModel> dayOfWeek)
    {
        DayOfWeek = dayOfWeek;
    }
}
