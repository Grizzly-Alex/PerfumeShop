namespace PerfumeShop.Web.ViewModels;

public sealed class CatalogBrandViewModel : EntityViewModel
{
    [DisplayName("Brand")]
    [Required(ErrorMessage = "This field must not be empty!")]
    public string Brand { get; set; }
}
