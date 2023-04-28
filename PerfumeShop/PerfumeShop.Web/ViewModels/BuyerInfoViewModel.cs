namespace PerfumeShop.Web.ViewModels;

public sealed class BuyerInfoViewModel
{

    [EmailAddress]
    public string? Email { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "First Name")]
    public string? BuyerName { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Last Name")]
    public string? BuyerSurname { get; set; }

    [Phone]
    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone Number")]
    public string? PhoneNumber { get; set; }

    [DataType(DataType.PostalCode)]
    [Display(Name = "Postal Code")]
    public string? PostalCode { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "State")]
    public string? State { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "City")]
    public string? City { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Street Address")]
    public string? StreetAddress { get; set; }
}