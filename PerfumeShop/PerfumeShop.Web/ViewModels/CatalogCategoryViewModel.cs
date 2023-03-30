namespace PerfumeShop.Web.ViewModels;

public sealed class CatalogGenderViewModel : EntityViewModel
{
    [DisplayName("Gender")]
    [Required(ErrorMessage = "This field must not be empty!")]
    public string Gender { get; set; }
}
