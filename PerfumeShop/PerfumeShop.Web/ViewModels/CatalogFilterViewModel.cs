namespace PerfumeShop.Web.ViewModels;

public sealed class CatalogFilterViewModel
{
	public decimal? MinPrice { get; set; }
	public decimal? MaxPrice { get; set; }
    public int? BrandId { get; set; }   
    public int? GenderId { get; set;}
    public int? AromaTypeId { get; set; }
    public int? ReleaseFormId { get; set; }

    [Display(Name = "Brand")]
    public IEnumerable<SelectListItem>? Brands { get; set; }

    [Display(Name = "Gender")]
    public IEnumerable<SelectListItem>? Genders { get; set; }

    [Display(Name = "Aroma Type")]
    public IEnumerable<SelectListItem>? AromaTypes { get; set; }

    [Display(Name = "Release Form")]
    public IEnumerable<SelectListItem>? ReleaseForms { get; set; }


    public CatalogFilterViewModel()
    {       
    }

    public CatalogFilterViewModel(
        decimal? minPrice,
        decimal? maxPrice,
        IEnumerable<SelectListItem>? brands,
        IEnumerable<SelectListItem>? genders,
        IEnumerable<SelectListItem>? aromaTypes,
        IEnumerable<SelectListItem>? releaseForms)
    {
        MinPrice = minPrice;
        MaxPrice = maxPrice;
        Brands = brands;
        Genders = genders;
        AromaTypes = aromaTypes;
        ReleaseForms = releaseForms;
    }
}
