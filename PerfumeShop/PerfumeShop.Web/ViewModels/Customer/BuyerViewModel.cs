namespace PerfumeShop.Web.ViewModels.Customer;

public sealed class BuyerViewModel
{
    public string Id { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "First Name")]
    public string? FirstName { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Last Name")]
    public string? LastName { get; set; }

    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Phone]
    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone Number")]
    public string? PhoneNumber { get; set; }
}