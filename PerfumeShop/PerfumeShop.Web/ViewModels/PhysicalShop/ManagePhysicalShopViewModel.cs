namespace PerfumeShop.Web.ViewModels.PhysicalShop;

public sealed class ManagePhysicalShopViewModel
{
    public PhysicalShopViewModel Shop { get; set; }
    public List<CheckboxItem> DayOfWeek { get; set; }

    public ManagePhysicalShopViewModel()
    {
        
    }

    public ManagePhysicalShopViewModel(List<CheckboxItem> dayOfWeek)
    {
        DayOfWeek = dayOfWeek;
	}

    public ManagePhysicalShopViewModel(PhysicalShopViewModel shop,  List<CheckboxItem> dayOfWeek)
	{
		DayOfWeek = dayOfWeek;
        Shop = shop;
	}
}
