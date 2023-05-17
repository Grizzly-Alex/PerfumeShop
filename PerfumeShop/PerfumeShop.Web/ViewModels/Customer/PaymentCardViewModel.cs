namespace PerfumeShop.Web.ViewModels.Customer;

public sealed class PaymentCardViewModel
{
    [Required(ErrorMessage = "Value {0} must not be empty!")]
    [Display(Name = "Name")]
    public string? NameOwner { get; set; }

    [Required(ErrorMessage = "Value {0} must not be empty!")]
    [Display(Name = "Number")]
    public string? CardNumber { get; set; }

    [Required(ErrorMessage = "Value {0} must not be empty!")]
    [Display(Name = "Year")]
    public string? ExpirationYear { get; set; }

    [Required(ErrorMessage = "Value {0} must not be empty!")]
    [Display(Name = "Month")]
    public string? ExpirationMonth { get; set; }

    [Required(ErrorMessage = "Value {0} must not be empty!")]
    [Display(Name = "Cvc")]
    public string? Cvc { get; set; }
}
