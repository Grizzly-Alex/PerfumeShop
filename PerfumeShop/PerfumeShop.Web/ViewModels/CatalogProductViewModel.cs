namespace PerfumeShop.Web.ViewModels;

public sealed class CatalogProductViewModel : EntityViewModel
{
    [Required(ErrorMessage = "Value {0} must not be empty!")]
    public string Name { get; private set; }

    [Required(ErrorMessage = "Value {0} must not be empty!")]
    public string Description { get; private set; }

    [ValidateNever]
    [DisplayName("Picture Uri")]
    public string PictureUri { get; private set; }

    [Precision(10, 2)]
    [Range(1, 99999999, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    [Required(ErrorMessage = "Value {0} must not be empty!")]
    public decimal Price { get; private set; }

    [Range(0, int.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    [Required(ErrorMessage = "Value {0} must not be empty!")]
    public int Stock { get; private set; }

    [Range(0, int.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    [Required(ErrorMessage = "Value {0} must not be empty!")]
    public int Volume { get; private set; }

    public DateTime DateDelivery { get; set; }

    [Required(ErrorMessage = "Value {0} from the list must be selected!")]
    [DisplayName("Category")]
    public int BrandId { get; private set; }
    public CatalogBrand Brand { get; private set; }

    public int GenderId { get; private set; }
    public CatalogGender Gender { get; private set; }

    public int TypeId { get; private set; }
    public CatalogType Type { get; private set; }

    public int ReleaseFormId { get; private set; }
    public CatalogReleaseForm ReleaseForm { get; private set; }

    [Required(ErrorMessage = "Value {0} from the list must be selected!")]
    [DisplayName("Category")]
    public int CategoryId { get; private set; }
    public CatalogCategory Category { get; private set; }
}
