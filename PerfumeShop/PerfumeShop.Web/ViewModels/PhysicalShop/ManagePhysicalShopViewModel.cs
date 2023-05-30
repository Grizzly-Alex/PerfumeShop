namespace PerfumeShop.Web.ViewModels.PhysicalShop;

public sealed class ManagePhysicalShopViewModel
{
    public PhysicalShopViewModel Shop { get; set; }
    public List<CheckBoxViewModel> DayOfWeek { get; set; }

    public ManagePhysicalShopViewModel()
    {
        
    }

    public ManagePhysicalShopViewModel(List<CheckBoxViewModel> dayOfWeek)
    {
        DayOfWeek = dayOfWeek;
    }
}
