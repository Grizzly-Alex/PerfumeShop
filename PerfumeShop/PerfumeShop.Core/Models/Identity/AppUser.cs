namespace PerfumeShop.Core.Models.Identity;

public class AppUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? StreetAddress { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public virtual ICollection<AppUserClaim> Claims { get; set; }
    public virtual ICollection<AppUserLogin> Logins { get; set; }
    public virtual ICollection<AppUserToken> Tokens { get; set; }
    public virtual ICollection<AppUserRole> UserRoles { get; set; }
}
