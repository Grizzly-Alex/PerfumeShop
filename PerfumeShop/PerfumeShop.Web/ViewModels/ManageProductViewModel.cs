namespace PerfumeShop.Web.ViewModels;


public sealed class ManageProductViewModel
{
    public CatalogProductViewModel? ProductViewModel { get; set; }
    public IEnumerable<SelectListItem>? Brands { get; set; }
    public IEnumerable<SelectListItem>? AromaTypes { get; set; }
    public IEnumerable<SelectListItem>? Genders { get; set; }
    public IEnumerable<SelectListItem>? ReleaseForms { get; set; }

	public ManageProductViewModel()
	{
	}

	public ManageProductViewModel(
        CatalogProductViewModel? productViewModel,
        IEnumerable<SelectListItem>? brands,
        IEnumerable<SelectListItem>? aromaTypes,
        IEnumerable<SelectListItem>? genders,
        IEnumerable<SelectListItem>? releaseForms)
    {
        ProductViewModel = productViewModel;
        Brands = brands;
        AromaTypes = aromaTypes;
        Genders = genders;
        ReleaseForms = releaseForms;      
    }
}
