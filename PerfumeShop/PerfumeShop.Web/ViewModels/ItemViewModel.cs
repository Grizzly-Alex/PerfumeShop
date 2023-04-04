namespace PerfumeShop.Web.ViewModels;

public sealed class ItemViewModel : EntityViewModel
{
    [DisplayName("Name")]
    [Required(ErrorMessage = "This field must not be empty!")]
    public string Name { get; set; }
}
