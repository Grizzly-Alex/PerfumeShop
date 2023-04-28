namespace PerfumeShop.Web.ViewModels.UserManage;

public class IndexUserViewModel
{
    public string? StatusMessage { get; set; }
    public bool IsEmailConfirmed { get; set; }

    [Display(Name = "User Name")]
    public string? UserName { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "First Name")]
    public string? FirstName { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Last Name")]
    public string? LastName { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Street Address")]
    public string? StreetAddress { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "City")]
    public string? City { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "State")]
    public string? State { get; set; }

    [DataType(DataType.PostalCode)]
    [Display(Name = "Postal Code")]
    public string? PostalCode { get; set; }

    [Phone]
    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone Number")]
    public string? PhoneNumber { get; set; }
}
