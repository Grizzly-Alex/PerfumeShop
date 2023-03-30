namespace PerfumeShop.Web.ViewModels;

public sealed class CatalogCategoryViewModel : EntityViewModel
{
    [DisplayName("Category")]
    [Required(ErrorMessage = "This field must not be empty!")]
    public string Category { get; set; }
}
