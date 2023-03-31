namespace PerfumeShop.Web.ViewModels;

public sealed class ManageProductViewModel
{
    public CatalogProductViewModel? ProductViewModel { get; set; }
    public IEnumerable<SelectListItem>? Categories { get; set; }
    public IEnumerable<SelectListItem>? Brands { get; set; }
    public IEnumerable<SelectListItem>? Types { get; set; }
    public IEnumerable<SelectListItem>? Genders { get; set; }
    public IEnumerable<SelectListItem>? ReleaseForms { get; set; }

    public ManageProductViewModel(
        CatalogProductViewModel? productViewModel,
        IEnumerable<SelectListItem>? categories,
        IEnumerable<SelectListItem>? brands,
        IEnumerable<SelectListItem>? types,
        IEnumerable<SelectListItem>? genders,
        IEnumerable<SelectListItem>? releaseForms)
    {
        ProductViewModel = productViewModel;
        Categories = categories;
        Brands = brands;
        Types = types;
        Genders = genders;
        ReleaseForms = releaseForms;      
    }
}
