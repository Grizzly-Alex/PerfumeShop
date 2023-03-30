namespace PerfumeShop.Web.ViewModels;

public sealed class CatalogReleaseFormViewModel : EntityViewModel
{
    [DisplayName("Release Form")]
    [Required(ErrorMessage = "This field must not be empty!")]
    public string ReleaseForm { get; set; }
}
