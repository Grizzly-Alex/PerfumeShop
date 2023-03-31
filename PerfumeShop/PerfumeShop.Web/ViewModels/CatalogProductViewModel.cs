namespace PerfumeShop.Web.ViewModels;

public sealed class CatalogProductViewModel : EntityViewModel
{
    [Required(ErrorMessage = "Value {0} must not be empty!")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Value {0} must not be empty!")]
    public string Description { get; set; }

    [ValidateNever]
    [DisplayName("Picture Uri")]
    public string PictureUri { get; set; }

    [Precision(10, 2)]
    [Range(1, 99999999, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    [Required(ErrorMessage = "Value {0} must not be empty!")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    [Required(ErrorMessage = "Value {0} must not be empty!")]
    public int Stock { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    [Required(ErrorMessage = "Value {0} must not be empty!")]
    public int Volume { get; set; }

    public DateTime DateDelivery { get; set; }

    [Required(ErrorMessage = "Value {0} from the list must be selected!")]
    [DisplayName("Category")]
    public int BrandId { get; set; }
    [ValidateNever]
    public string Brand { get; set; }

    [DisplayName("Gender")]
    public int GenderId { get; set; }
    [ValidateNever]
    public string Gender { get; set; }

    [DisplayName("Type")]
    public int TypeId { get; set; }
    [ValidateNever]
    public string Type { get; set; }

    [DisplayName("ReleaseForm")]
    public int ReleaseFormId { get; set; }
    [ValidateNever]
    public string ReleaseForm { get; set; }

    [Required(ErrorMessage = "Value {0} from the list must be selected!")]
    [DisplayName("Category")]
    public int CategoryId { get; set; }
    [ValidateNever]
    public string Category { get; set; }
}
