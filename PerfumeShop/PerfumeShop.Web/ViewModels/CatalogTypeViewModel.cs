namespace PerfumeShop.Web.ViewModels;

public sealed class CatalogTypeViewModel : EntityViewModel
{
    [DisplayName("Type")]
    [Required(ErrorMessage = "This field must not be empty!")]
    public string Type { get; set; }
}
