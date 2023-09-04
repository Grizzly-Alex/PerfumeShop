namespace PerfumeShop.Web.ViewModels.UserManage;

public class EditUserViewModel
{
    public string Id { get; set; }

    [Display(Name = "Email")]
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

    public string Role { get; set; }
    public IEnumerable<SelectListItem>? RoleList { get; set; }
}
