namespace PerfumeShop.Web.ViewModels;

public sealed class AddressViewModel
{
    [DataType(DataType.Text)]
    [Display(Name = "State")]
    public string? State { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "City")]
    public string? City { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Street Address")]
    public string? StreetAddress { get; set; }

    [DataType(DataType.PostalCode)]
    [Display(Name = "Postal Code")]
    public string? PostalCode { get; set; }
}
